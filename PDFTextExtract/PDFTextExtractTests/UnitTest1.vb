Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace PDFTextExtractTests
    <TestClass>
    Public Class UnitTest1
        <TestMethod>
        Sub TestSub()
            Dim p As New PDFTextExtract.PdfHandler("E:\My Documents\localRepos\PDFTextExtract\TestData\test.pdf")
            p.extractData()

        End Sub
    End Class
End Namespace

