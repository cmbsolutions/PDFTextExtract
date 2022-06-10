Imports System.ComponentModel
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

    Private bgWorkers As List(Of BackgroundWorker)
    Private CapturedData As List(Of ExtractedData)

    Private startTime As Date
    Private stopTime As Date

    Public ReadOnly Property runningTime As TimeSpan

    Public Event WorkerProgressChanged(sender As Object, e As ProgressChangedEventArgs)
    Public Event WorkersCompleted(sender As Object, data As List(Of ExtractedData), workingTime As TimeSpan)

    Sub New()
        imageHandler = New Imager

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

    Public Sub BeginExtractAllData(regio As FS_RECTF, workers As Integer)
        imageHandler.SetClippingPath(CInt(regio.Left), CInt(regio.Top), CInt(regio.Right), CInt(regio.Bottom))

        CapturedData = New List(Of ExtractedData)

        startTime = Now
        StartWorkers(workers)
    End Sub

    Public Sub StartWorkers(workers As Integer)
        bgWorkers = New List(Of BackgroundWorker)

        For i As Integer = 0 To workers - 1
            Dim worker As New BackgroundWorker With {
                .WorkerReportsProgress = True,
                .WorkerSupportsCancellation = True
            }

            AddHandler worker.ProgressChanged, AddressOf bgWorkerProgressChangedEventHandler
            AddHandler worker.RunWorkerCompleted, AddressOf bgWorkerRunWorkerCompletedEventHandler
            AddHandler worker.DoWork, AddressOf bgWorkerDoWorkHandler

            worker.RunWorkerAsync(New workerInfo With {.start = i, .skip = workers, .ref = worker})
            bgWorkers.Add(worker)
        Next
    End Sub

    Public Sub CancelWorkers()
        If bgWorkers Is Nothing Then Exit Sub

        For Each worker In bgWorkers
            If Not worker.CancellationPending Then worker.CancelAsync()
        Next
    End Sub

    Private Sub bgWorkerDoWorkHandler(sender As Object, e As DoWorkEventArgs)
        Dim worker = DirectCast(e.Argument, workerInfo).ref
        Dim startPage As Integer = DirectCast(e.Argument, workerInfo).start
        Dim skip As Integer = DirectCast(e.Argument, workerInfo).skip
        Dim LocalCapturedData As New List(Of ExtractedData)
        Dim proc As Double = 0.0

        Dim pagesToProcess = Math.Ceiling(GetPageCount() / skip)

        Using eng = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.LstmOnly)
            For i As Integer = startPage To GetPageCount() - 1 Step skip
                If worker.CancellationPending Then
                    LocalCapturedData = Nothing
                    worker.ReportProgress(100, startPage)
                    e.Cancel = True
                    Exit Sub
                End If

                Using p = eng.Process(imageHandler.ConvertPage(currentDocument.Pages(i)))
                    LocalCapturedData.Add(New ExtractedData(p.MeanConfidence, p.Text, i))
                End Using

                proc += 100 / pagesToProcess
                worker.ReportProgress(CInt(proc), startPage)
            Next
        End Using
        worker.ReportProgress(100, startPage)

        e.Result = LocalCapturedData
    End Sub

    Private Sub bgWorkerRunWorkerCompletedEventHandler(sender As Object, e As RunWorkerCompletedEventArgs)
        If Not e.Cancelled And e.Error Is Nothing Then

            CapturedData.AddRange(DirectCast(e.Result, List(Of ExtractedData)))

            If CapturedData.Count = GetPageCount() Then
                stopTime = Now

                _runningTime = stopTime.Subtract(startTime)

                RaiseEvent WorkersCompleted(Me, CapturedData, runningTime)

                For Each worker In bgWorkers
                    worker.Dispose()
                Next
            End If
        End If
    End Sub

    Private Sub bgWorkerProgressChangedEventHandler(sender As Object, e As ProgressChangedEventArgs)
        RaiseEvent WorkerProgressChanged(sender, e)
    End Sub

    Private Class workerInfo
        Property start As Integer
        Property skip As Integer
        Property ref As BackgroundWorker
    End Class
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
