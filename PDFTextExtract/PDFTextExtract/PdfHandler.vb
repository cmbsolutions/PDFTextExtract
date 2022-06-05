Imports PDFiumSharp
Imports PDFiumSharp.Types

Public Class PdfHandler
    Private imageHandler As Imager
    Private currentDocument As PdfDocument
    Private currentPageIdx As Integer = 0

    Sub New(file As String)
        If IO.File.Exists(file) Then
            currentDocument = New PdfDocument(file)

            imageHandler = New Imager
            imageHandler.SetPageSize(currentDocument, 0)
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

End Class
