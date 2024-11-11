Imports PDFiumSharp
Imports PDFiumSharp.Types
Imports ImageMagick
Imports System.IO

Public Class Imager
    Implements IDisposable

    Private disposedValue As Boolean
    Private scale As Integer = 4
    Private DPI As Integer = 150

    Public ReadOnly Property outputImage As TesseractOCR.Pix.Image

    Public Property pageSize As FS_SIZEF

    Public ReadOnly Property clippingPath As ClippingPath

    Public Sub SetPageSize(ps As FS_SIZEF)
        pageSize = ps
    End Sub

    Public Sub SetClippingPath(path As ClippingPath)
        _clippingPath = path
    End Sub

    Public Sub ResetClippingPath()
        _clippingPath = Nothing
    End Sub

    Public Sub SetScale(s As Integer)
        scale = s
    End Sub

    Public Sub SetDPI(renderDPI As Integer)
        DPI = renderDPI
    End Sub

    Public Function RenderCurrentPage(pdfPage As PdfPage) As IO.Stream
        Try
            Dim width = CInt(Math.Round(pageSize.Width * scale))
            Dim height = CInt(Math.Round(pageSize.Height * scale))

            Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)
                bm.Fill(New FPDF_COLOR(255, 255, 255))
                pdfPage.Render(bm, (0, 0, width, height), GetOrientation(width, height), Enums.RenderingFlags.None)

                Dim renderedPage As New IO.MemoryStream
                bm.Save(renderedPage, DPI, DPI)
                renderedPage.Position = 0
                Return renderedPage
            End Using
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        Return Nothing
    End Function

    Public Function ConvertPage(pdfPage As PDFiumSharp.PdfPage) As ImagerOutput
        Try
            Dim width = CInt(Math.Round(pageSize.Width * scale))
            Dim height = CInt(Math.Round(pageSize.Height * scale))

            Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)

                bm.Fill(New FPDF_COLOR(255, 255, 255))

                pdfPage.Render(bm, (0, 0, width, height), GetOrientation(width, height), Enums.RenderingFlags.None)

                Using ms1 As New IO.MemoryStream
                    bm.Save(ms1, DPI, DPI)
                    ms1.Position = 0
                    Using img As New ImageMagick.MagickImage(ms1)

                        If clippingPath IsNot Nothing Then
                            img.Crop(clippingPath.region)
                            'img.Chop(clippingPath.region)
                            'img.RePage()
                        End If

                        Dim imageroutput As New ImagerOutput

                        Using ms As New IO.MemoryStream
                            img.Write(ms, MagickFormat.Png32)

                            imageroutput.BarcodeImage = New IO.MemoryStream
                            img.Write(imageroutput.BarcodeImage, MagickFormat.Png32)
                            imageroutput.BarcodeImage.Position = 0
                            imageroutput.OcrImage = TesseractOCR.Pix.Image.LoadFromMemory(ms)
                            Return imageroutput
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        Return Nothing
    End Function



    Public Function ConvertRegion() As ImagerOutput
        Try
            If RenderedPageMemoryStream IsNot Nothing Then
                RenderedPageMemoryStream.Position = 0

                Using img As New ImageMagick.MagickImage(RenderedPageMemoryStream)

                    If clippingPath IsNot Nothing Then
                        img.Crop(clippingPath.region)
                        'img.RePage()
                    End If

                    Dim imageroutput As New ImagerOutput

                    Using ms As New IO.MemoryStream
                        img.Write(ms, MagickFormat.Png32)

                        imageroutput.BarcodeImage = New IO.MemoryStream
                        img.Write(imageroutput.BarcodeImage, MagickFormat.Png32)
                        imageroutput.BarcodeImage.Position = 0
                        imageroutput.OcrImage = TesseractOCR.Pix.Image.LoadFromMemory(ms)
                        Return imageroutput
                    End Using
                End Using
            Else
                Throw New MagickStreamErrorException("Memorystream is empty.")
            End If
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        Return Nothing
    End Function

    Private RenderedPageMemoryStream As IO.MemoryStream
    Public Function LoadPage(pdfPage As PdfPage) As Boolean
        Try
            Dim width = CInt(Math.Round(pageSize.Width * scale))
            Dim height = CInt(Math.Round(pageSize.Height * scale))

            Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)

                'If pdfPage.HasTransparency Then
                bm.Fill(New FPDF_COLOR(255, 255, 255))

                pdfPage.Render(bm, (0, 0, width, height), GetOrientation(width, height), Enums.RenderingFlags.None)

                If RenderedPageMemoryStream IsNot Nothing Then
                    RenderedPageMemoryStream.Close()
                    RenderedPageMemoryStream.Dispose()
                    'GC.Collect()
                End If

                RenderedPageMemoryStream = New IO.MemoryStream
                bm.Save(RenderedPageMemoryStream, DPI, DPI)
            End Using
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        Return True
    End Function

    Private Function GetOrientation(width As Integer, height As Integer) As Enums.PageOrientations
        'If width <= height Then ' Portrait
        Return Enums.PageOrientations.Normal
        'Else ' Landscape
        'Return Enums.PageOrientations.Rotated90CCW
        'End If
    End Function

#Region "Dispose"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If RenderedPageMemoryStream IsNot Nothing Then
                    RenderedPageMemoryStream.Close()
                    RenderedPageMemoryStream.Dispose()
                End If
                If outputImage IsNot Nothing Then outputImage.Dispose()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

Public Class ImagerOutput
    Property OcrImage As TesseractOCR.Pix.Image
    Property BarcodeImage As MemoryStream
End Class