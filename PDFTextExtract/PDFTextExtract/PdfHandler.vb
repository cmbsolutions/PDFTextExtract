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

            _engine = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.TesseractAndLstm)
        End If
    End Sub

    Public Function extractData(pageIdx As Integer) As ExtractedData
        Using page = _engine.Process(imageHandler.ResetClippingPath.ConvertPage(currentDocument.Pages(pageIdx)).outputImage)
            Return New ExtractedData(page.MeanConfidence, page.Text, pageIdx)
        End Using
    End Function

    Public Function extractData(regio As FS_RECTF, pageIdx As Integer) As ExtractedData
        Using page = _engine.Process(imageHandler.SetClippingPath(CInt(regio.Left), CInt(regio.Top), CInt(regio.Right), CInt(regio.Bottom)).ConvertPage(currentDocument.Pages(pageIdx)).outputImage)
            Return New ExtractedData(page.MeanConfidence, page.Text, pageIdx)
        End Using
    End Function

    Public Function ExtractAllData(regio As FS_RECTF) As List(Of ExtractedData)
        Dim data As New List(Of ExtractedData)
        imageHandler.SetClippingPath(CInt(regio.Left), CInt(regio.Top), CInt(regio.Right), CInt(regio.Bottom))

        For Each page In currentDocument.Pages
            Using p = _engine.Process(imageHandler.ConvertPageBulk(page))
                data.Add(New ExtractedData(p.MeanConfidence, p.Text, page.Index))
            End Using
        Next

        Return data
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
