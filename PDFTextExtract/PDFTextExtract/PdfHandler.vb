Imports PDFiumSharp
Imports PDFiumSharp.Types

Public Class PdfHandler
    Implements IDisposable

    Private imageHandler As Imager = Nothing
    Public ReadOnly Property currentDocument As PdfDocument = Nothing
    Public ReadOnly Property currentPageIdx As Integer = 0
    Public ReadOnly Property pageSize As FS_SIZEF
    Private engine As TesseractOCR.Engine = Nothing
    Private disposedValue As Boolean

    Sub New(initialScale As Integer)
        imageHandler = New Imager
        imageHandler.SetScale(initialScale)

        engine = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.LstmOnly)
    End Sub

    Public Sub LoadDocument(file As String)
        If IO.File.Exists(file) Then
            If currentDocument IsNot Nothing Then currentDocument.Close()
            _currentDocument = New PdfDocument(file)
            _pageSize = New FS_SIZEF(CSng(currentDocument.Pages(currentPageIdx).Width), CSng(currentDocument.Pages(currentPageIdx).Height))
            imageHandler.SetPageSize(pageSize)
        End If
    End Sub

    Public Function GetPageCount() As Integer
        If currentDocument IsNot Nothing Then
            Return currentDocument.Pages.Count
        Else
            Return 0
        End If
    End Function

    Public Function GetRenderedPage() As IO.Stream
        imageHandler.ResetClippingPath()
        If currentDocument IsNot Nothing Then
            Return imageHandler.RenderCurrentPage(currentDocument.Pages(currentPageIdx))
        Else
            Return Nothing
        End If
    End Function

    Public Sub SetScale(scale As Integer)
        imageHandler.SetScale(scale)
    End Sub

#Region "Navigation"
    Public Sub FirstPage()
        _currentPageIdx = 1
    End Sub

    Public Sub PreviousPage()
        If currentPageIdx > 1 Then _currentPageIdx -= 1
    End Sub

    Public Sub NextPage()
        If currentDocument IsNot Nothing AndAlso currentPageIdx <= currentDocument.Pages.Count Then _currentPageIdx += 1
    End Sub

    Public Sub LastPage()
        If currentDocument IsNot Nothing Then _currentPageIdx = currentDocument.Pages.Count - 1
    End Sub

    Public Sub GotoPage(pageNumber As Integer)
        If currentDocument IsNot Nothing AndAlso pageNumber > 0 AndAlso pageNumber <= currentDocument.Pages.Count Then _currentPageIdx = pageNumber
    End Sub
#End Region


    Public Function extractData() As ExtractedData
        imageHandler.ResetClippingPath()

        Using page = engine.Process(imageHandler.ConvertPage(currentDocument.Pages(currentPageIdx)))
            Return New ExtractedData(page.MeanConfidence, page.Text, currentPageIdx)
        End Using
    End Function

    Public Function extractData(regio As FS_RECTF) As ExtractedData
        imageHandler.SetClippingPath(CInt(regio.Left), CInt(regio.Top), CInt(regio.Right), CInt(regio.Bottom))

        Using page = engine.Process(imageHandler.ConvertPage(currentDocument.Pages(currentPageIdx)))
            Return New ExtractedData(page.MeanConfidence, page.Text, currentPageIdx)
        End Using
    End Function

    Public Function ExtractAllData(regio As FS_RECTF) As List(Of ExtractedData)
        Dim data As New List(Of ExtractedData)
        imageHandler.SetClippingPath(CInt(regio.Left), CInt(regio.Top), CInt(regio.Right), CInt(regio.Bottom))

        For Each page In currentDocument.Pages
            Using p = engine.Process(imageHandler.ConvertPage(page))
                data.Add(New ExtractedData(p.MeanConfidence, p.Text, page.Index))
            End Using
        Next

        Return data
    End Function
#Region "Dispose"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If engine IsNot Nothing Then engine.Dispose()
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
