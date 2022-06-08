Imports PDFiumSharp
Imports PDFiumSharp.Types
Imports ImageMagick
Imports PDFiumCore

Public Class Imager
    Implements IDisposable

    Private disposedValue As Boolean
    Private scale As Integer = 3

    Public ReadOnly Property outputImage As TesseractOCR.Pix.Image
    Public ReadOnly Property pageIndex As Integer

    Public Property pageSize As FS_SIZEF

    Public ReadOnly Property clippingPath As MagickGeometry = Nothing

    Public Function SetPageSize(document As PdfDocument, pageIdx As Integer) As Imager
        _pageIndex = pageIdx
        pageSize = New FS_SIZEF(CSng(document.Pages(pageIndex).Width), CSng(document.Pages(pageIndex).Height))
        Return Me
    End Function

    Public Function SetClippingPath(x As Integer, y As Integer, w As Integer, h As Integer) As Imager
        _clippingPath = New MagickGeometry(x, y, w, h)
        Return Me
    End Function

    Public Function ResetClippingPath() As Imager
        _clippingPath = Nothing
        Return Me
    End Function

    Public Sub SetScale(s As Integer)
        scale = s
    End Sub

    Public Function ConvertPage(pdfPage As PDFiumSharp.PdfPage) As Imager
        Dim width = CInt(Math.Round(pageSize.Width * scale))
        Dim height = CInt(Math.Round(pageSize.Height * scale))

        Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)

            bm.Fill(New Types.FPDF_COLOR(255, 255, 255))

            pdfPage.Render(bm, (0, 0, width, height), Enums.PageOrientations.Normal, Enums.RenderingFlags.None)

            Using ms1 As New IO.MemoryStream
                bm.Save(ms1, 300, 300)
                ms1.Position = 0
                Using img As New ImageMagick.MagickImage(ms1)

                    If clippingPath IsNot Nothing Then
                        img.Crop(clippingPath)
                        img.RePage()
                    End If

                    Using ms As New IO.MemoryStream
                        img.Write(ms, MagickFormat.Png32)

                        _outputImage = TesseractOCR.Pix.Image.LoadFromMemory(ms)
                    End Using
                End Using
            End Using
        End Using

        Return Me
    End Function

    Public Function ConvertPageBulk(pdfPage As PDFiumSharp.PdfPage) As TesseractOCR.Pix.Image
        Return ConvertPage(pdfPage).outputImage
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