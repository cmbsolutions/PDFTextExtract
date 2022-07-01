Imports System
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.Threading
Imports PDFTextExtract

Module Program
    Private pdfHandler As PdfHandler
    Private pb As CmbConsoleControls.CmbMultiBar
    Private cts As CancellationTokenSource
    Private ctsDone As CancellationTokenSource
    Private inFile As String = ""
    Private outFile As String = ""
    Private customScale As Integer = 4
    Private workerCount As Integer = 4
    Private customDpi As Integer = 150
    Private overwrite As Boolean = False
    Private quiet As Boolean = False
    Private verbose As Boolean = False
    Private regions As New List(Of ClippingPath)

    Sub Main(args As String())
        Try
            Console.Clear()

            If args.Length = 0 Then
                ShowHelp()
                Exit Sub
            End If

            For i As Integer = 0 To args.Length - 1
                Select Case args(i)
                    Case "-in"
                        inFile = args(i + 1)
                        If Not IO.File.Exists(inFile) Then
                            Console.WriteLine("Invalid input. File does not exist.")
                            Environment.Exit(1)
                        End If
                        Dim fileBegin(3) As Byte ' first 4 bytes must be %PDF
                        Dim fileEnd(3) As Byte ' last 4 bytes must be %EOF
                        Using fs As New IO.FileStream(inFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
                            fs.Read(fileBegin)
                            fs.Position = fs.Length - 5
                            fs.Read(fileEnd)
                        End Using
                        If Not fileBegin.SequenceEqual({&H25, &H50, &H44, &H46}) Or Not fileEnd.SequenceEqual({&H25, &H45, &H4F, &H46}) Then
                            Console.WriteLine("Invalid input. File is not a complete PDF file.")
                            Environment.Exit(2)
                        End If
                        i += 1
                    Case "-out"
                        outFile = args(i + 1)
                        If Not overwrite AndAlso IO.File.Exists(outFile) Then
                            Console.WriteLine("Output already exists. Use -o option to force overwrite.")
                            Environment.Exit(3)
                        End If
                        i += 1
                    Case "-w"
                        If Integer.TryParse(args(i + 1), workerCount) Then
                            workerCount = Math.Min(12, Math.Max(1, workerCount))
                        Else
                            Console.WriteLine("Invalid value given for workers (-w). Using default instead.")
                            workerCount = 4
                        End If
                        i += 1
                    Case "-s"
                        If Integer.TryParse(args(i + 1), customScale) Then
                            customScale = Math.Min(8, Math.Max(1, customScale))
                        Else
                            Console.WriteLine("Invalid value given for scale (-s). Using default instead.")
                            customScale = 2
                        End If
                        i += 1
                    Case "-d"
                        If Integer.TryParse(args(i + 1), customDpi) Then
                            customDpi = Math.Min(600, Math.Max(72, customDpi))
                        Else
                            Console.WriteLine("Invalid value given for Dpi (-d). Using default instead.")
                            customDpi = 72
                        End If
                        i += 1
                    Case "-c"
                        Dim cps = args(i + 1).Split(";")

                        For Each cp In cps
                            Dim c = cp.Split(",")

                            Dim l, t, r, b As Integer

                            If Integer.TryParse(c(0), l) AndAlso Integer.TryParse(c(1), t) AndAlso Integer.TryParse(c(2), r) AndAlso Integer.TryParse(c(3), b) Then
                                regions.Add(New ClippingPath(l, t, r, b))
                            Else
                                Console.WriteLine("Invalid clippingpath.")
                                Environment.Exit(7)
                            End If
                        Next
                        i += 1
                    Case "-o"
                        overwrite = True
                    Case "-q"
                        quiet = True
                    Case "-v"
                        verbose = True
                    Case "-h"
                        ShowHelp()
                        Environment.Exit(0)
                    Case Else
                        Console.WriteLine("Unknown argument given. Use -h to show help.")
                        Environment.Exit(4)
                End Select
            Next

            pb = New CmbConsoleControls.CmbMultiBar
            cts = New CancellationTokenSource
            ctsDone = New CancellationTokenSource
            Dim token = cts.Token
            Dim tokenDone = ctsDone.Token

            Console.WriteLine("Press C to cancel")
            Console.WriteLine()

            For y As Integer = 0 To workerCount - 1
                pb.Add($"Worker {y}", True, True)
            Next

            pdfHandler = New PdfHandler

            AddHandler pdfHandler.WorkersCompleted, AddressOf workersCompleted
            AddHandler pdfHandler.WorkerProgressChanged, AddressOf workerProgressChanged

            pdfHandler.SetScale(customScale)
            pdfHandler.SetDPI(customDpi)

            pdfHandler.LoadDocument(inFile)
            pdfHandler.AddClippingPaths(regions.ToArray)

            pdfHandler.BeginExtractAllData(workerCount)

            Dim mytask As Task = Task.Run(Sub()
                                              Do
                                                  While Not Console.KeyAvailable
                                                      If tokenDone.IsCancellationRequested Then Exit Sub
                                                      Thread.Sleep(50)
                                                  End While
                                              Loop While Console.ReadKey(True).KeyChar.ToString.ToUpperInvariant <> "C"
                                              pdfHandler.CancelWorkers()
                                          End Sub)
            mytask.Wait(tokenDone)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub workerProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        pb.Report(e.ProgressPercentage, CInt(e.UserState))
    End Sub

    Private Sub workersCompleted(sender As Object, data As List(Of ExtractedData), workingTime As TimeSpan)
        Dim accuracy = data.Average(Function(c) c.confidence)
        If pdfHandler IsNot Nothing Then
            RemoveHandler pdfHandler.WorkerProgressChanged, AddressOf workerProgressChanged
            RemoveHandler pdfHandler.WorkersCompleted, AddressOf workersCompleted
            Console.SetCursorPosition(0, pb.MaxRow + 2)
            Console.WriteLine($"All {pdfHandler.GetPageCount} pages are processed with {workerCount} workers with an accuracy of {accuracy}% in {workingTime.Hours} hours, {workingTime.Minutes} minutes and {workingTime.Seconds} seconds.")
            ExportData(data)
            pdfHandler.Dispose()
        End If
        ctsDone.Cancel()
    End Sub

    Private Sub ExportData(data As List(Of ExtractedData))
        Using fs As New IO.FileStream(outFile, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
            Using sw As New IO.StreamWriter(fs, System.Text.Encoding.Unicode)
                sw.WriteLine("PageIndex;RegionIndex;Region;Accuracy;CapturedText")
                For Each d In data
                    sw.WriteLine($"{d.pageIndex};{d.clipIdx};{pdfHandler.clippingPaths.First(Function(c) c.idx = d.clipIdx).region};{d.confidence};{Regex.Replace(d.text, "(?:\r\n|\r|\n)", "\n", RegexOptions.IgnoreCase Or RegexOptions.Singleline)}")
                Next
            End Using
        End Using
    End Sub

    Private Sub ShowHelp()
        Console.WriteLine(My.Resources.help)
    End Sub
End Module
