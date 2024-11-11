Imports System.ComponentModel
Imports System.Text
Imports System.Text.RegularExpressions
Imports ImageMagick
Imports PDFiumSharp
Imports PDFiumSharp.Types
Imports ZXing.Magick

Public Class PdfHandler
    Implements IDisposable

    Private ImageHandler As Imager = Nothing
    Public ReadOnly Property CurrentDocument As PdfDocument = Nothing
    Public ReadOnly Property CurrentPageIdx As Integer = 0
    Public ReadOnly Property PageSize As FS_SIZEF
    Private Engine As TesseractOCR.Engine = Nothing

    Private BarcodeEngine As BarcodeReader

    Private DisposedValue As Boolean
    Private PdfScale As Integer = 4
    Private RenderDPI As Integer = 150
    Private CurrentFilename As String

    Private BgWorkers As List(Of workerState)
    Private CapturedData As Concurrent.ConcurrentBag(Of ExtractedData)

    Private StartTime As Date
    Private StopTime As Date

    Public ReadOnly Property RunningTime As TimeSpan

    Public ReadOnly Property ClippingPaths As New List(Of ClippingPath)
    Private AutoResetClippingPaths As Boolean = False

    Public Property UseMatching As Boolean = False
    Public Property FirstPageRegex As String
    Public Property FirstPageRegion As ClippingPath

    Public Event WorkerProgressChanged(sender As Object, e As ProgressChangedEventArgs)
    Public Event WorkersCompleted(sender As Object, data As List(Of ExtractedData), workingTime As TimeSpan)

    Sub New()
        Try
            ImageHandler = New Imager
            SetScale(PdfScale)
            ImageHandler.SetDPI(RenderDPI)

            Engine = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.LstmOnly)
            BarcodeEngine = New BarcodeReader

        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
    End Sub

    Public Sub LoadDocument(file As String)
        Try
            If IO.File.Exists(file) Then
                If CurrentDocument IsNot Nothing Then CurrentDocument.Close()
                _CurrentDocument = New PdfDocument(file)
                _CurrentPageIdx = 0
                _PageSize = New FS_SIZEF(CSng(CurrentDocument.Pages(CurrentPageIdx).Width), CSng(CurrentDocument.Pages(CurrentPageIdx).Height))
                ImageHandler.SetPageSize(PageSize)
                ImageHandler.ResetClippingPath()
                ResetClippingPaths()
                CurrentFilename = file
            End If
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
    End Sub

    Public Function GetPageCount() As Integer
        If CurrentDocument IsNot Nothing Then
            Return CurrentDocument.Pages.Count
        Else
            Return 0
        End If
    End Function

    Public Function GetRenderedPage() As IO.Stream
        If CurrentDocument IsNot Nothing Then
            ImageHandler.SetPageSize(GetCurrentPageSize)
            Return ImageHandler.RenderCurrentPage(CurrentDocument.Pages(CurrentPageIdx))
        Else
            Return Nothing
        End If
    End Function

    Public Function GetCurrentPageSize() As FS_SIZEF
        If CurrentDocument IsNot Nothing Then
            Return New FS_SIZEF(CSng(CurrentDocument.Pages(CurrentPageIdx).Width), CSng(CurrentDocument.Pages(CurrentPageIdx).Height))
        Else
            Return Nothing
        End If
    End Function

    Public Sub SetScale(scale As Integer)
        imageHandler.SetScale(scale)
        pdfScale = scale
    End Sub

    Public Sub SetDPI(dpi As Integer)
        renderDPI = dpi
        imageHandler.SetDPI(dpi)
    End Sub

#Region "Navigation"
    Public Sub FirstPage()
        _CurrentPageIdx = 0
    End Sub

    Public Sub PreviousPage()
        If CurrentPageIdx > 0 Then _CurrentPageIdx -= 1
    End Sub

    Public Sub NextPage()
        If CurrentDocument IsNot Nothing AndAlso CurrentPageIdx <= CurrentDocument.Pages.Count - 1 Then _CurrentPageIdx += 1
    End Sub

    Public Sub LastPage()
        If CurrentDocument IsNot Nothing Then _CurrentPageIdx = CurrentDocument.Pages.Count - 1
    End Sub

    Public Sub GotoPage(pageNumber As Integer)
        ' pages are counted from 0, so when you ask page 10 we need the 9th index
        If pageNumber > 0 Then pageNumber -= 1

        If CurrentDocument IsNot Nothing AndAlso pageNumber >= 0 AndAlso pageNumber <= CurrentDocument.Pages.Count - 1 Then _CurrentPageIdx = pageNumber
    End Sub
#End Region

    Public Sub ExtractDataWithImage(imageLocation As String)
        Try
            Dim data As New StringBuilder
            Dim filename As String = $"{Now:yyyyMMddhhmmss}"

            data.AppendLine("PageIndex;RegionIndex;Region;Accuracy;CapturedText")
            InitClippingPath()

            For Each clippingPath In clippingPaths
                imageHandler.SetClippingPath(clippingPath)

                Dim image = imageHandler.ConvertPage(CurrentDocument.Pages(CurrentPageIdx))
                image.OcrImage.Save(IO.Path.Combine(imageLocation, $"{filename}_{clippingPath.idx}_{clippingPath.region}.png"), TesseractOCR.Enums.ImageFormat.Png)

                Using page = Engine.Process(image.OcrImage)
                    data.AppendLine($"{CurrentPageIdx};{clippingPath.idx};{clippingPath.region};{page.MeanConfidence};{Regex.Replace(page.Text, "(?:\r\n|\r|\n)", "\n", RegexOptions.IgnoreCase Or RegexOptions.Singleline)}")
                End Using
            Next

            IO.File.WriteAllText(IO.Path.Combine(imageLocation, $"{filename}.txt"), data.ToString)
            If AutoResetClippingPaths Then ResetClippingPaths()
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
    End Sub

    Public Function ExtractData(path As ClippingPath) As ExtractedData
        Try
            ImageHandler.SetClippingPath(path)

            Dim bresult = BarcodeEngine.Decode(New MagickImage(ImageHandler.ConvertPage(CurrentDocument.Pages(CurrentPageIdx)).BarcodeImage))

            Return New ExtractedData(100, bresult.Text, CurrentPageIdx, path.idx)

            Using page = Engine.Process(ImageHandler.ConvertPage(CurrentDocument.Pages(CurrentPageIdx)).OcrImage)
                Return New ExtractedData(page.MeanConfidence, page.Text.Trim, CurrentPageIdx, path.idx)
            End Using
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        Return Nothing
    End Function

    Public Function ExtractData() As List(Of ExtractedData)
        Try
            Dim data As New List(Of ExtractedData)

            InitClippingPath()

            For Each clippingPath In ClippingPaths
                ImageHandler.SetClippingPath(clippingPath)

                Using page = Engine.Process(ImageHandler.ConvertPage(CurrentDocument.Pages(CurrentPageIdx)).OcrImage)
                    data.Add(New ExtractedData(page.MeanConfidence, page.Text.Trim, CurrentPageIdx, clippingPath.idx))
                End Using
                ImageHandler.ResetClippingPath()
            Next

            'Engine.ClearAdaptiveClassifier()
            If AutoResetClippingPaths Then ResetClippingPaths()

            Return data
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        Return Nothing
    End Function

    Public Sub BeginExtractAllData(workers As Integer)
        CapturedData = New Concurrent.ConcurrentBag(Of ExtractedData)

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

            bgWorkers.Add(ws)
            ws.worker.RunWorkerAsync(New workerInfo With {.workerId = i, .startPage = i, .skip = workers, .ref = ws.worker})
        Next
    End Sub

    Public Sub CancelWorkers()
        If bgWorkers Is Nothing Then Exit Sub

        For Each ws In bgWorkers
            If Not ws.worker.CancellationPending Then ws.worker.CancelAsync()
        Next
    End Sub

    Public Function WorkersCancelled() As Boolean
        If bgWorkers Is Nothing Then Return True

        For Each ws In bgWorkers
            If ws.worker.IsBusy Then Return False
        Next

        Return True
    End Function

    Private Sub BgWorkerDoWorkHandler(sender As Object, e As DoWorkEventArgs)
        Dim info As workerInfo = DirectCast(e.Argument, workerInfo)
        Dim worker = info.ref
        Dim LocalCapturedData As New List(Of ExtractedData)
        Dim proc As Double = 0.0

        Dim pagesToProcess = CInt(Math.Ceiling(GetPageCount() / info.skip))

        Dim matcher As Regex = Nothing

        Try
            Using eng = New TesseractOCR.Engine("./tessdata", TesseractOCR.Enums.Language.Dutch, TesseractOCR.Enums.EngineMode.LstmOnly)
                Using imgHandler As New Imager
                    imgHandler.SetPageSize(PageSize)
                    imgHandler.SetScale(PdfScale)
                    imgHandler.SetDPI(RenderDPI)

                    For i As Integer = info.startPage To GetPageCount() - 1 Step info.skip
                        If worker.CancellationPending Then
                            LocalCapturedData = Nothing
                            worker.ReportProgress(100, info.workerId)
                            imgHandler.Dispose()
                            eng.Dispose()
                            e.Cancel = True
                            Exit Sub
                        End If

                        proc += 100 / pagesToProcess
                        worker.ReportProgress(CInt(proc), info.workerId)

                        imgHandler.LoadPage(CurrentDocument.Pages(i))

                        ' We probably have multipage mailpacks, so only capture the first pages.
                        If UseMatching Then
                            If matcher Is Nothing Then matcher = New Regex(FirstPageRegex)

                            imgHandler.SetClippingPath(FirstPageRegion)

                            Using p = eng.Process(imgHandler.ConvertRegion().OcrImage)

                                If Not matcher.IsMatch(p.Text.Trim) Then
                                    ' no match found so we are probably not on a first page, just go to the next step
                                    imgHandler.ResetClippingPath()
                                    Continue For
                                Else
                                    CapturedData.Add(New ExtractedData(p.MeanConfidence, p.Text.Trim, i, FirstPageRegion.idx))
                                End If
                            End Using

                            imgHandler.ResetClippingPath()
                        End If

                        For Each clippingPath In _ClippingPaths
                            If UseMatching AndAlso clippingPath.idx = FirstPageRegion.idx Then Continue For

                            imgHandler.SetClippingPath(clippingPath)

                            Using p = eng.Process(imgHandler.ConvertRegion().OcrImage)
                                CapturedData.Add(New ExtractedData(p.MeanConfidence, p.Text.Trim, i, clippingPath.idx))
                            End Using
                            imgHandler.ResetClippingPath()
                        Next
                    Next
                End Using
            End Using
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
        worker.ReportProgress(100, info.workerId)

        e.Result = New workerResult With {
            .datas = LocalCapturedData,
            .workerId = info.workerId,
            .ref = info.ref
            }
    End Sub

    Private Sub bgWorkerRunWorkerCompletedEventHandler(sender As Object, e As RunWorkerCompletedEventArgs)
        Try
            If Not e.Cancelled Then
                Dim results = DirectCast(e.Result, workerResult)
                bgWorkers.First(Function(c) c.workerId = results.workerId).completed = True


                If bgWorkers.Where(Function(c) c.completed).Count = bgWorkers.Count Then
                    stopTime = Now

                    _runningTime = stopTime.Subtract(startTime)

                    If AutoResetClippingPaths Then ResetClippingPaths()

                    RaiseEvent WorkersCompleted(Me, CapturedData.ToList, runningTime)
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Helpers.dumpException(ex)
        End Try
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
        Property cancelled As Boolean = False
    End Class
#End Region

#Region "ClippingPaths"
    Private Sub InitClippingPath()
        If _clippingPaths.Count = 0 Then
            AddClippingPath(0, 0, CInt(Math.Ceiling(PageSize.Width)), CInt(Math.Ceiling(PageSize.Height)))
            AutoResetClippingPaths = True
        End If
    End Sub
    Public Function AddClippingPath(left As Integer, top As Integer, right As Integer, bottom As Integer) As Integer
        _clippingPaths.Add(New ClippingPath(left, top, right, bottom))
        Return clippingPaths.Last.idx
    End Function

    Public Function AddClippingPaths(paths As ClippingPath()) As Integer
        _clippingPaths.AddRange(paths)
        Return clippingPaths.Last.idx
    End Function

    Public Sub RemoveClippingPath(item As ClippingPath)
        _clippingPaths.Remove(item)
    End Sub

    Public Sub ResetClippingPaths()
        _clippingPaths.Clear()
    End Sub
#End Region

    Public Sub WaitOnGarbageCollect()
        Dim w As Integer = 10

        Do While w > 0
            GC.Collect()
            GC.WaitForPendingFinalizers()
            w -= 1
        Loop
    End Sub


#Region "Dispose"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If Engine IsNot Nothing Then
                    'Engine.ClearAdaptiveClassifier()
                    'Engine.ClearPersistentCache()
                    Engine.Dispose()
                End If
                If CurrentDocument IsNot Nothing Then CurrentDocument.Close()
                If imageHandler IsNot Nothing Then imageHandler.Dispose()
                If bgWorkers IsNot Nothing Then
                    For Each ws In bgWorkers
                        ws.worker.Dispose()
                    Next
                End If
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
