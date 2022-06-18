Imports System.ComponentModel
Imports System.Text
Imports System.Text.RegularExpressions
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
    Private pdfScale As Integer = 4

    Private bgWorkers As List(Of workerState)
    Private CapturedData As List(Of ExtractedData)

    Private startTime As Date
    Private stopTime As Date

    Public ReadOnly Property runningTime As TimeSpan

    Public ReadOnly Property clippingPaths As New List(Of ClippingPath)
    Private AutoResetClippingPaths As Boolean = False

    Public Property useMatching As Boolean = False
    Public Property firstPageRegex As String
    Public Property firstPageRegion As ClippingPath

    Public Event WorkerProgressChanged(sender As Object, e As ProgressChangedEventArgs)
    Public Event WorkersCompleted(sender As Object, data As List(Of ExtractedData), workingTime As TimeSpan)

    Sub New()
        imageHandler = New Imager
        SetScale(4)

        engine = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.LstmOnly)
    End Sub

    Public Sub LoadDocument(file As String)
        If IO.File.Exists(file) Then
            If currentDocument IsNot Nothing Then currentDocument.Close()
            _currentDocument = New PdfDocument(file)
            _currentPageIdx = 0
            _pageSize = New FS_SIZEF(CSng(currentDocument.Pages(currentPageIdx).Width), CSng(currentDocument.Pages(currentPageIdx).Height))
            imageHandler.SetPageSize(pageSize)
            imageHandler.ResetClippingPath()
            ResetClippingPaths()
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
        If currentDocument IsNot Nothing Then
            Return imageHandler.RenderCurrentPage(currentDocument.Pages(currentPageIdx))
        Else
            Return Nothing
        End If
    End Function

    Public Sub SetScale(scale As Integer)
        imageHandler.SetScale(scale)
        pdfScale = scale
    End Sub

#Region "Navigation"
    Public Sub FirstPage()
        _currentPageIdx = 0
    End Sub

    Public Sub PreviousPage()
        If currentPageIdx > 0 Then _currentPageIdx -= 1
    End Sub

    Public Sub NextPage()
        If currentDocument IsNot Nothing AndAlso currentPageIdx <= currentDocument.Pages.Count - 1 Then _currentPageIdx += 1
    End Sub

    Public Sub LastPage()
        If currentDocument IsNot Nothing Then _currentPageIdx = currentDocument.Pages.Count - 1
    End Sub

    Public Sub GotoPage(pageNumber As Integer)
        ' pages are counted from 0, so when you ask page 10 we need the 9th index
        If pageNumber > 0 Then pageNumber -= 1

        If currentDocument IsNot Nothing AndAlso pageNumber > 0 AndAlso pageNumber <= currentDocument.Pages.Count - 1 Then _currentPageIdx = pageNumber
    End Sub
#End Region

    Public Sub ExtractDataWithImage(imageLocation As String)
        Dim data As New StringBuilder
        Dim filename As String = $"{Now:yyyyMMddhhmmss}"

        data.AppendLine("PageIndex;RegionIndex;Region;Accuracy;CapturedText")
        InitClippingPath()

        For Each clippingPath In clippingPaths
            imageHandler.SetClippingPath(clippingPath)

            Dim image = imageHandler.ConvertPage(currentDocument.Pages(currentPageIdx))
            image.Save(IO.Path.Combine(imageLocation, $"{filename}_{clippingPath.idx}_{clippingPath.region}.png"), TesseractOCR.Enums.ImageFormat.Png)

            Using page = engine.Process(image)
                data.AppendLine($"{currentPageIdx};{clippingPath.idx};{clippingPath.region};{page.MeanConfidence};{Regex.Replace(page.Text, "(?:\r\n|\r|\n)", "\n", RegexOptions.IgnoreCase Or RegexOptions.Singleline)}")
            End Using
        Next
        IO.File.WriteAllText(IO.Path.Combine(imageLocation, $"{filename}.txt"), data.ToString)
        If AutoResetClippingPaths Then ResetClippingPaths()
    End Sub

    Public Function extractData(path As ClippingPath) As ExtractedData
        imageHandler.SetClippingPath(path)

        Using page = engine.Process(imageHandler.ConvertPage(currentDocument.Pages(currentPageIdx)))
            Return New ExtractedData(page.MeanConfidence, page.Text.Trim, currentPageIdx, path.idx)
        End Using

    End Function

    Public Function extractData() As List(Of ExtractedData)
        Dim data As New List(Of ExtractedData)

        InitClippingPath()

        For Each clippingPath In clippingPaths
            imageHandler.SetClippingPath(clippingPath)

            Using page = engine.Process(imageHandler.ConvertPage(currentDocument.Pages(currentPageIdx)))
                data.Add(New ExtractedData(page.MeanConfidence, page.Text.Trim, currentPageIdx, clippingPath.idx))
            End Using
            imageHandler.ResetClippingPath()
        Next

        If AutoResetClippingPaths Then ResetClippingPaths()

        Return data
    End Function

    Public Sub BeginExtractAllData(workers As Integer)
        CapturedData = New List(Of ExtractedData)

        InitClippingPath()

        startTime = Now
        StartWorkers(workers)
    End Sub

#Region "Workers"
    Public Sub StartWorkers(workers As Integer)
        bgWorkers = New List(Of workerState)

        For i As Integer = 0 To workers - 1
            Dim ws As New workerState With {
                .worker = New BackgroundWorker With {
                    .WorkerReportsProgress = True,
                    .WorkerSupportsCancellation = True
                },
                .completed = False,
                .workerId = i
            }

            AddHandler ws.worker.ProgressChanged, AddressOf bgWorkerProgressChangedEventHandler
            AddHandler ws.worker.RunWorkerCompleted, AddressOf bgWorkerRunWorkerCompletedEventHandler
            AddHandler ws.worker.DoWork, AddressOf bgWorkerDoWorkHandler

            ws.worker.RunWorkerAsync(New workerInfo With {.workerId = i, .startPage = i, .skip = workers, .ref = ws.worker})
            bgWorkers.Add(ws)
        Next
    End Sub

    Public Sub CancelWorkers()
        If bgWorkers Is Nothing Then Exit Sub

        For Each ws In bgWorkers
            If Not ws.worker.CancellationPending Then ws.worker.CancelAsync()
        Next
    End Sub

    Private Sub bgWorkerDoWorkHandler(sender As Object, e As DoWorkEventArgs)
        Dim info As workerInfo = DirectCast(e.Argument, workerInfo)
        Dim worker = info.ref
        Dim LocalCapturedData As New List(Of ExtractedData)
        Dim proc As Double = 0.0
        Dim skippedPages As Integer = 0

        Dim pagesToProcess = CInt(Math.Ceiling(GetPageCount() / info.skip))

        Dim matcher As Regex = Nothing

        Using eng = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.LstmOnly)
            Using imgHandler As New Imager
                imgHandler.SetPageSize(pageSize)
                imgHandler.SetScale(pdfScale)

                For i As Integer = info.startPage To GetPageCount() - 1 Step info.skip
                    If worker.CancellationPending Then
                        LocalCapturedData = Nothing
                        worker.ReportProgress(100, info.workerId)
                        e.Cancel = True
                        Exit Sub
                    End If

                    ' We probably have multipage mailpacks, so only capture the first pages.
                    If useMatching Then
                        If matcher Is Nothing Then matcher = New Regex(firstPageRegex)

                        imgHandler.SetClippingPath(firstPageRegion)

                        Using p = eng.Process(imgHandler.ConvertPage(currentDocument.Pages(i)))

                            If Not matcher.IsMatch(p.Text.Trim) Then
                                ' no match found so we are probably not on a first page, just go to the next step
                                imgHandler.ResetClippingPath()
                                Continue For
                            Else
                                LocalCapturedData.Add(New ExtractedData(p.MeanConfidence, p.Text.Trim, i, firstPageRegion.idx))
                            End If
                        End Using

                        imgHandler.ResetClippingPath()
                    End If

                    For Each clippingPath In _clippingPaths

                        If useMatching And clippingPath.idx = firstPageRegion.idx Then Continue For

                        imgHandler.SetClippingPath(clippingPath)

                        Using p = eng.Process(imgHandler.ConvertPage(currentDocument.Pages(i)))
                            LocalCapturedData.Add(New ExtractedData(p.MeanConfidence, p.Text.Trim, i, clippingPath.idx))
                        End Using
                        imgHandler.ResetClippingPath()
                    Next

                    proc += 100 / pagesToProcess
                    worker.ReportProgress(CInt(proc), info.workerId)
                Next

            End Using
        End Using
        worker.ReportProgress(100, info.workerId)

        e.Result = New workerResult With {
            .datas = LocalCapturedData,
            .workerId = info.workerId,
            .ref = info.ref
            }
    End Sub

    Private Sub bgWorkerRunWorkerCompletedEventHandler(sender As Object, e As RunWorkerCompletedEventArgs)
        If Not e.Cancelled And e.Error Is Nothing Then
            Dim results = DirectCast(e.Result, workerResult)

            CapturedData.AddRange(results.datas)

            bgWorkers.First(Function(c) c.workerId = results.workerId).completed = True

            If bgWorkers.Where(Function(c) c.completed).Count = bgWorkers.Count Then
                stopTime = Now

                _runningTime = stopTime.Subtract(startTime)

                If AutoResetClippingPaths Then ResetClippingPaths()
                RaiseEvent WorkersCompleted(Me, CapturedData, runningTime)
            End If
        Else
            CapturedData = Nothing
        End If

        For Each ws In bgWorkers
            ws.worker.Dispose()
        Next
    End Sub

    Private Sub bgWorkerProgressChangedEventHandler(sender As Object, e As ProgressChangedEventArgs)
        RaiseEvent WorkerProgressChanged(sender, e)
    End Sub

    Private Class workerInfo
        Property workerId As Integer
        Property startPage As Integer
        Property endPage As Integer

        Property skip As Integer
        Property ref As BackgroundWorker
    End Class

    Private Class workerResult
        Property workerId As Integer
        Property datas As List(Of ExtractedData)
        Property ref As BackgroundWorker
    End Class

    Private Class workerState
        Property workerId As Integer
        Property worker As BackgroundWorker
        Property completed As Boolean = False
    End Class
#End Region

#Region "ClippingPaths"
    Private Sub InitClippingPath()
        If _clippingPaths.Count = 0 Then
            AddClippingPath(0, 0, CInt(Math.Ceiling(pageSize.Width)), CInt(Math.Ceiling(pageSize.Height)))
            AutoResetClippingPaths = True
        End If
    End Sub
    Public Function AddClippingPath(left As Integer, top As Integer, right As Integer, bottom As Integer) As Integer
        _clippingPaths.Add(New ClippingPath(left, top, right, bottom))
        Return clippingPaths.Last.idx
    End Function

    Public Sub RemoveClippingPath(item As ClippingPath)
        _clippingPaths.Remove(item)
    End Sub

    Public Sub ResetClippingPaths()
        _clippingPaths.Clear()
    End Sub
#End Region



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
