Imports PDFiumSharp
Imports PDFiumSharp.Types
Imports ImageMagick

Public Class Imager
    Implements IDisposable

    Private disposedValue As Boolean
    Private scale As Integer = 2

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

    Public Function RenderCurrentPage(pdfPage As PdfPage) As IO.Stream
        Dim width = CInt(Math.Round(pageSize.Width * scale))
        Dim height = CInt(Math.Round(pageSize.Height * scale))

        Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)
            If pdfPage.HasTransparency Then bm.Fill(New FPDF_COLOR(255, 255, 255))
            pdfPage.Render(bm, (0, 0, width, height), Enums.PageOrientations.Normal, Enums.RenderingFlags.None)

            Dim renderedPage As New IO.MemoryStream
            bm.Save(renderedPage, 300, 300)
            renderedPage.Position = 0
            Return renderedPage
        End Using
    End Function

    Public Function ConvertPage(pdfPage As PDFiumSharp.PdfPage) As TesseractOCR.Pix.Image
        Dim width = CInt(Math.Round(pageSize.Width * scale))
        Dim height = CInt(Math.Round(pageSize.Height * scale))

        Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)

            'If pdfPage.HasTransparency Then
            bm.Fill(New FPDF_COLOR(255, 255, 255))

            pdfPage.Render(bm, (0, 0, width, height), Enums.PageOrientations.Normal, Enums.RenderingFlags.None)

            Using ms1 As New IO.MemoryStream
                bm.Save(ms1, 300, 300)
                ms1.Position = 0
                Using img As New ImageMagick.MagickImage(ms1)

                    If clippingPath IsNot Nothing Then
                        img.Crop(clippingPath.region)
                        'img.RePage()
                    End If

                    Using ms As New IO.MemoryStream
                        img.Write(ms, MagickFormat.Png32)

                        Return TesseractOCR.Pix.Image.LoadFromMemory(ms)
                    End Using
                End Using
            End Using
        End Using
    End Function

    Public Function ConvertRegion() As TesseractOCR.Pix.Image
        RenderedPageMemoryStream.Position = 0
        Using img As New ImageMagick.MagickImage(RenderedPageMemoryStream)

            If clippingPath IsNot Nothing Then
                img.Crop(clippingPath.region)
                'img.RePage()
            End If

            Using ms As New IO.MemoryStream
                img.Write(ms, MagickFormat.Png32)

                Return TesseractOCR.Pix.Image.LoadFromMemory(ms)
            End Using
        End Using
    End Function

    Private RenderedPageMemoryStream As IO.MemoryStream
    Public Function LoadPage(pdfPage As PdfPage) As Boolean
        Dim width = CInt(Math.Round(pageSize.Width * scale))
        Dim height = CInt(Math.Round(pageSize.Height * scale))

        Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)

            'If pdfPage.HasTransparency Then
            bm.Fill(New FPDF_COLOR(255, 255, 255))

            pdfPage.Render(bm, (0, 0, width, height), Enums.PageOrientations.Normal, Enums.RenderingFlags.None)

            If RenderedPageMemoryStream IsNot Nothing Then RenderedPageMemoryStream.Close()

            RenderedPageMemoryStream = New IO.MemoryStream
            bm.Save(RenderedPageMemoryStream, 300, 300)
        End Using

        Return True
    End Function

#Region "Dispose"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If RenderedPageMemoryStream IsNot Nothing Then RenderedPageMemoryStream.Close()
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