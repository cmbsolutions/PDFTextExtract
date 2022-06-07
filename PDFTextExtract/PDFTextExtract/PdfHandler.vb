Imports PDFiumSharp
Imports PDFiumSharp.Types

Public Class PdfHandler
    Implements IDisposable

    Private imageHandler As Imager = Nothing
    Private currentDocument As PdfDocument = Nothing
    Private currentPageIdx As Integer = 0
    Private _engine As TesseractOCR.Engine = Nothing
    Private disposedValue As Boolean

    Sub New(file As String)
        If IO.File.Exists(file) Then
            currentDocument = New PdfDocument(file)

            imageHandler = New Imager
            imageHandler.SetPageSize(currentDocument, 0)

            _engine = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.Default)
        End If
    End Sub

    Sub extractData()
        Try
            Using engine = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.Default)
                For Each p In currentDocument.Pages
                    Dim img = imageHandler.ConvertPage(p).outputImage

                    Using page = engine.Process(img)
                        Console.WriteLine($"Mean confidence: {page.MeanConfidence}")
                        Console.WriteLine($"Text: \r\n{page.Text}")
                    End Using
                Next
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Function extractData(regio As FS_RECTF, pageIdx As Integer) As String
        Dim img = imageHandler.SetClippingPath(CInt(regio.Left), CInt(regio.Top), CInt(regio.Right), CInt(regio.Bottom)).ConvertPage(currentDocument.Pages(pageIdx)).outputImage

        Using page = _engine.Process(img)
            Debug.WriteLine($"Mean confidence: {page.MeanConfidence}")
            Debug.WriteLine($"Text: \r\n{page.Text}")

            Return page.Text
        End Using
    End Function

#Region "Dispose"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _engine IsNot Nothing Then _engine.Dispose()
                If currentDocument IsNot Nothing Then currentDocument.Close()
                If imageHandler IsNot Nothing Then imageHandler.Dispose()
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
