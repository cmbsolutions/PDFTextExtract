Imports PDFiumSharp
Imports PDFiumSharp.Types
Imports ImageMagick

Public Class Imager
    Implements IDisposable

    Private disposedValue As Boolean
    Private scale As Integer = 2

    Public ReadOnly Property outputImage As TesseractOCR.Pix.Image
    Public ReadOnly Property pageIndex As Integer

    Public Property pageSize As FS_SIZEF

    Public Function SetPageSize(document As PdfDocument, pageIdx As Integer) As Imager
        _pageIndex = pageIdx
        pageSize = New FS_SIZEF(CSng(document.Pages(pageIndex).Width), CSng(document.Pages(pageIndex).Height))
        Return Me
    End Function

    Public Function ConvertPage(pdfPage As PDFiumSharp.PdfPage) As Imager
        Try
            Dim width = CInt(Math.Round(pageSize.Width * scale))
            Dim height = CInt(Math.Round(pageSize.Height * scale))

            Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)

                bm.Fill(New Types.FPDF_COLOR(255, 255, 255))

                Dim matrix As New FS_MATRIX(scale, 0, 0, scale, 0, 0)
                Dim clipping As New FS_RECTF(0, 0, width, height)

                'PDFium.FPDF_RenderPageBitmapWithMatrix(bm.Handle, pdfPage.Handle, matrix, clipping, Enums.RenderingFlags.Annotations)
                PDFium.FPDF_RenderPageBitmap(bm.Handle, pdfPage.Handle, 0, 0, width, height, Enums.PageOrientations.Normal, Enums.RenderingFlags.None)

                Using ms1 As New IO.MemoryStream
                    bm.Save(ms1, 300, 300)
                    ms1.Position = 0
                    Using img As New ImageMagick.MagickImage(ms1)

                        Using ms As New IO.MemoryStream
                            img.Write(ms, MagickFormat.Png32)

                            _outputImage = TesseractOCR.Pix.Image.LoadFromMemory(ms)
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        Return Me
    End Function

#Region "Dispose"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
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