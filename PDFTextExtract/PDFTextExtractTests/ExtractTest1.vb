Imports System.Text.RegularExpressions
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PDFTextExtract

Namespace PDFTextExtractTests
    <TestClass>
    Public Class ExtractTest1
        <TestMethod>
        Sub Match1()
            Using p As New PdfHandler
                p.SetScale(4)

                p.LoadDocument("./Assets/250.pdf")

                p.AddClippingPath(490, 601, 1030, 295)

                Dim data = p.extractData()

                Dim expect As String = "Abdinabi Amsalem Kapellenberglaan 42 6891AG ROZENDAAL" 'normalized string, CrLf|Cr|Lf is replaced by a space and then trimmed
                Dim actual As String = Regex.Replace(data.First.text, "(?:\r\n|\r|\n)", " ", RegexOptions.IgnoreCase Or RegexOptions.Singleline).Trim

                Assert.AreEqual(expect, actual)
            End Using
        End Sub

        <TestMethod>
        Sub Match2()
            Using p As New PdfHandler
                p.SetScale(4)

                p.LoadDocument("./Assets/101-200.pdf")

                p.GotoPage(14)

                Dim path As New ClippingPath(359, 446, 1181, 708)

                Dim data = p.extractData(path)

                Dim expect As String = $"Amitav Kogak Paganinistraat 5 3906BC VEENENDAAL"
                Dim actual As String = Regex.Replace(data.text, "(?:\r\n|\r|\n)", " ", RegexOptions.IgnoreCase Or RegexOptions.Singleline).Trim
                Assert.AreEqual(expect, actual)
            End Using
        End Sub
    End Class
End Namespace

