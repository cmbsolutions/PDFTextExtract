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

                Dim expect As String = $"Abdinabi Amsalem{vbLf}Kapellenberglaan 42{vbLf}6891AG ROZENDAAL{vbLf}{vbLf}"
                Assert.AreEqual(expect, data.First.text)


            End Using

        End Sub

        '<TestMethod>
        'Sub Match2()
        '    Using p As New PdfHandler
        '        p.SetScale(4)

        '        p.LoadDocument("./Assets/101-200.pdf")
        '        p.GotoPage(13)
        '        Dim data = p.extractData(359, 446, 1181, 708)

        '        Dim expect As String = $"Amitav Kogak{vbLf}Paganinistraat 5{vbLf}3906BC VEENENDAAL{vbLf}{vbLf}"
        '        Assert.AreEqual(expect, data.text)

        '    End Using
        'End Sub
    End Class
End Namespace

