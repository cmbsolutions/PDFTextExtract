Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class Form1
    Private currentPdf As String

    Private pdfScale As Integer = 3
    Private RC As Rectangle
    Private screenPtA, screenPtB As Point
    Private boxPta, boxPtb As Point
    Private dragging As Boolean = False
    Private _localImg As Image
    Private WithEvents pdfHandler As PDFTextExtract.PdfHandler
    Private Delegate Sub pdfHandler_WorkerProgressChangedDelegate(sender As Object, e As ProgressChangedEventArgs)

    Private exportData As List(Of PDFTextExtract.ExtractedData)

    Private _currentZoomFactor As Double = 1.0

    Dim lt As New Point(0, 0)
    Dim rb As New Point(0, 0)

#Region "selection rectangle"
    Private Sub Canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles Canvas.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim pb As PictureBox = CType(sender, PictureBox)
            Cursor.Clip = pb.RectangleToScreen(pb.ClientRectangle)
            screenPtA = Cursor.Position
            screenPtB = screenPtA
            RC = NormalizedRC(screenPtA, screenPtB)
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
            dragging = True
        End If
    End Sub

    Private Sub Canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles Canvas.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left And dragging Then
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
            screenPtB = Cursor.Position
            RC = NormalizedRC(screenPtA, screenPtB)
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
        End If
    End Sub

    Private Sub Canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles Canvas.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left And dragging Then
            Cursor.Clip = Nothing
            Dim pb As PictureBox = CType(sender, PictureBox)
            RC = pb.RectangleToClient(RC)

            Dim rect As New Rectangle
            rect.X = Convert.ToInt32(RC.X / _currentZoomFactor)
            rect.Y = Convert.ToInt32(RC.Y / _currentZoomFactor)
            rect.Width = Convert.ToInt32(RC.Width / _currentZoomFactor)
            rect.Height = Convert.ToInt32(RC.Height / _currentZoomFactor)

            lt.X = rect.X
            lt.Y = rect.Y
            rb.X = rect.Width
            rb.Y = rect.Height

            tX.Text = rect.X.ToString
            tY.Text = rect.Y.ToString
            tW.Text = rect.Width.ToString
            tH.Text = rect.Height.ToString

            RedrawCanvas()
        End If


        dragging = False
    End Sub
    Public Function NormalizedRC(ByVal ptA As Point, ByVal ptB As Point) As Rectangle
        Return New Rectangle(Math.Min(ptA.X, ptB.X), Math.Min(ptA.Y, ptB.Y), Math.Abs(ptA.X - ptB.X), Math.Abs(ptA.Y - ptB.Y))
    End Function
#End Region

    Private Sub bPdf_Click(sender As Object, e As EventArgs) Handles bPdf.Click
        If ofdPdf.ShowDialog = DialogResult.OK Then
            currentPdf = ofdPdf.FileName

            If pdfHandler Is Nothing Then pdfHandler = New PDFTextExtract.PdfHandler

            pdfHandler.SetScale(pdfScale)

            pdfHandler.LoadDocument(currentPdf)
            handleButtons()
            LoadPage(True)
        End If
    End Sub

    Private Sub LoadPage(calculateZoom As Boolean)
        _localImg = Image.FromStream(pdfHandler.GetRenderedPage)

        If _localImg IsNot Nothing AndAlso calculateZoom Then
            Dim lw = _localImg.Width
            Dim lh = _localImg.Height

            Dim cw = CanvasX.ClientSize.Width
            Dim ch = CanvasX.ClientSize.Height

            If lw >= cw And lh >= ch Then
                If lw >= lh Then
                    _currentZoomFactor = Math.Round(cw / lw, 2, MidpointRounding.ToZero)
                Else
                    _currentZoomFactor = Math.Round(ch / lh, 2, MidpointRounding.ToZero)
                End If
            ElseIf lw < cw And lh >= ch Then
                _currentZoomFactor = Math.Round(ch / lh, 2, MidpointRounding.ToZero)
            ElseIf lw >= cw And lh < ch Then
                _currentZoomFactor = Math.Round(cw / lw, 2, MidpointRounding.ToZero)
            Else
                _currentZoomFactor = 1.0
            End If

            RedrawCanvas()
            ResizeAndCenterCanvas()
        Else
            RedrawCanvas()
        End If


    End Sub

    Private Sub RedrawCanvas()
        If _localImg Is Nothing Then Exit Sub
        Dim zbmp As New Bitmap(_localImg, Convert.ToInt32(_localImg.Width * _currentZoomFactor), Convert.ToInt32(_localImg.Height * _currentZoomFactor))
        Using g = Graphics.FromImage(zbmp)
            g.DrawRectangle(New Pen(Color.Red, 2), CInt(lt.X * _currentZoomFactor), CInt(lt.Y * _currentZoomFactor), CInt(rb.X * _currentZoomFactor), CInt(rb.Y * _currentZoomFactor))
        End Using
        Canvas.Image = zbmp
    End Sub

    Private Sub ResizeAndCenterCanvas()
        If _localImg Is Nothing Then Exit Sub

        Dim cx As Integer = Convert.ToInt32(CanvasX.Width / 2)
        Dim cy As Integer = Convert.ToInt32(CanvasX.Height / 2)

        Canvas.Width = Convert.ToInt32(_localImg.Width * _currentZoomFactor)
        Canvas.Height = Convert.ToInt32(_localImg.Height * _currentZoomFactor)

        Dim px As Integer = Convert.ToInt32(Canvas.Width / 2)
        Dim py As Integer = Convert.ToInt32(Canvas.Height / 2)

        Canvas.Left = Math.Max(cx - px, 10)
        Canvas.Top = Math.Max(cy - py, 10)

        CanvasShadow.Width = Canvas.Width
        CanvasShadow.Height = Canvas.Height
        CanvasShadow.Left = Canvas.Left + 5
        CanvasShadow.Top = Canvas.Top + 5

        If CanvasX.HorizontalScroll.Visible Or CanvasX.VerticalScroll.Visible Then
            CanvasX.AutoScrollPosition = New Point(px - cx, py - cy)
        End If

        lZoom.Text = $"Zoom {Convert.ToInt32(100 * _currentZoomFactor)}%"
    End Sub

    Private Sub tsbZoomIn_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _currentZoomFactor += 0.1
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Sub tsbZoomOut_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _currentZoomFactor -= 0.1
        If _currentZoomFactor < 0.1 Then _currentZoomFactor = 0.1
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Sub tsbZoomReset_Click(sender As Object, e As EventArgs) Handles Button3.Click
        _currentZoomFactor = 1.0
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Sub bFirst_Click(sender As Object, e As EventArgs) Handles bFirst.Click
        pdfHandler.FirstPage()
        handleButtons()
        LoadPage(False)
    End Sub

    Private Sub bPrev_Click(sender As Object, e As EventArgs) Handles bPrev.Click
        pdfHandler.PreviousPage()
        handleButtons()
        LoadPage(False)
    End Sub

    Private Sub bNext_Click(sender As Object, e As EventArgs) Handles bNext.Click
        pdfHandler.NextPage()
        handleButtons()
        LoadPage(False)
    End Sub

    Private Sub bLast_Click(sender As Object, e As EventArgs) Handles bLast.Click
        pdfHandler.LastPage()
        handleButtons()
        LoadPage(False)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Async Sub Button4_Click(sender As Object, e As EventArgs) Handles bTest.Click
        Dim data As PDFTextExtract.ExtractedData

        Dim renderTask = Task.Run(Function()
                                      Return pdfHandler.extractData(lt.X, lt.Y, rb.X, rb.Y)
                                  End Function)
        data = Await renderTask.ConfigureAwait(True)

        If data.confidence < 0.5 Then
            tConf.BackColor = Color.Salmon
        ElseIf data.confidence >= 0.5 And data.confidence < 0.75 Then
            tConf.BackColor = Color.Orange
        ElseIf data.confidence >= 0.75 And data.confidence < 0.85 Then
            tConf.BackColor = Color.Yellow
        Else
            tConf.BackColor = Color.LightGreen
        End If

        tConf.Text = $"{data.confidence * 100}%"
        rResult.Text = data.text

        bExtractAll.Enabled = True
    End Sub

    Private Sub bExtractAll_Click(sender As Object, e As EventArgs) Handles bExtractAll.Click
        ManageWorkers()

        pdfHandler.BeginExtractAllData(lt.X, lt.Y, rb.X, rb.Y, CInt(nWorkers.Value))
    End Sub

    Private Sub tScale_Scroll(sender As Object, e As EventArgs) Handles tScale.Scroll
        pdfScale = tScale.Value
        pdfHandler.SetScale(pdfScale)
        lScale.Text = pdfScale.ToString

        LoadPage(True)

        tX.Text = "0"
        tY.Text = "0"
        tW.Text = "0"
        tH.Text = "0"
    End Sub

    Private Sub handleButtons()
        If pdfHandler Is Nothing Then Exit Sub

        If pdfHandler.currentPageIdx = 0 Then
            bFirst.Enabled = False
            bFirst.Image = My.Resources.dis_action_goto_first
            bPrev.Enabled = False
            bPrev.Image = My.Resources.dis_action_goto_previous
        Else
            bFirst.Enabled = True
            bFirst.Image = My.Resources.action_goto_first
            bPrev.Enabled = True
            bPrev.Image = My.Resources.action_goto_previous
        End If

        If pdfHandler.currentPageIdx >= pdfHandler.GetPageCount - 1 Then
            bLast.Enabled = False
            bLast.Image = My.Resources.dis_action_goto_last
            bNext.Enabled = False
            bNext.Image = My.Resources.dis_action_goto_next
        Else
            bLast.Enabled = True
            bLast.Image = My.Resources.action_goto_last
            bNext.Enabled = True
            bNext.Image = My.Resources.action_goto_next
        End If

        tPages.Text = $"{pdfHandler.currentPageIdx + 1}/{pdfHandler.GetPageCount}"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        handleButtons()

    End Sub

    Private Sub ManageWorkers()
        pWorkers.Controls.Clear()

        For i As Integer = 0 To CInt(nWorkers.Value) - 1
            Dim l As New Label With {
                .ForeColor = Color.Yellow,
                .Location = New Point(3, 0),
                .Name = $"wLabel{i}",
                .Size = New Size(250, 15),
                .TabIndex = 1,
                .Text = $"Worker {i}",
                .TextAlign = ContentAlignment.MiddleCenter,
                .Visible = True,
                .Tag = i
            }

            Dim p As New ProgressBar With {
                .Location = New Point(1, 16),
                .Margin = New Padding(1),
                .Name = $"pbWorker{i}",
                .Size = New Size(250, 15),
                .Step = 1,
                .Style = ProgressBarStyle.Continuous,
                .TabIndex = 0,
                .Value = 0,
                .Visible = True,
                .Tag = i
            }

            pWorkers.Controls.AddRange({l, p})
        Next

        Dim b As New Button With {
            .Name = "bCancelWorkers",
            .ForeColor = Color.Red,
            .Location = New Point(3, 0),
            .Size = New Size(250, 32),
            .Text = "!!Cancel all workers!!",
            .TextAlign = ContentAlignment.MiddleCenter,
            .Visible = True
        }

        AddHandler b.Click, AddressOf bCancel_Click

        pWorkers.Controls.Add(b)
    End Sub

    Private Sub bCancel_Click(sender As Object, e As EventArgs)
        pdfHandler.CancelWorkers()
        Dim b As Button = DirectCast(sender, Button)

        b.Enabled = False
    End Sub

    Private Sub bExport_Click(sender As Object, e As EventArgs) Handles bExport.Click
        If exportData.Count > 0 AndAlso sfd.ShowDialog = DialogResult.OK Then
            Using fs As New IO.FileStream(sfd.FileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
                Using sw As New IO.StreamWriter(fs, System.Text.Encoding.Unicode)
                    sw.WriteLine("id;confidence;text")
                    For Each d In exportData
                        sw.WriteLine($"{d.pageIndex};{d.confidence};{Regex.Replace(d.text, "(?:\r\n|\r|\n)", "\n", RegexOptions.IgnoreCase Or RegexOptions.Singleline)}")
                    Next
                End Using
            End Using
        End If

        bExport.Enabled = False
        exportData = Nothing
        MessageBox.Show($"Captured data exported!")
    End Sub

    Private Sub bSaveTest_Click(sender As Object, e As EventArgs) Handles bSaveTest.Click
        If fbd.ShowDialog() = DialogResult.OK Then
            pdfHandler.ExtractDataWithImage(fbd.SelectedPath)

            MessageBox.Show("Done")
        End If
    End Sub

    Private Sub pdfHandler_WorkerProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles pdfHandler.WorkerProgressChanged
        If InvokeRequired Then
            Dim d As pdfHandler_WorkerProgressChangedDelegate = AddressOf pdfHandler_WorkerProgressChanged

            Invoke(d)
        Else
            Dim ctrl As ProgressBar = DirectCast(pWorkers.Controls.Find($"pbWorker{e.UserState}", True).First, ProgressBar)
            ctrl.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub pdfHandler_WorkersCompleted(sender As Object, data As List(Of PDFTextExtract.ExtractedData), workingTime As TimeSpan) Handles pdfHandler.WorkersCompleted
        exportData = data
        bExport.Enabled = True

        Dim accuracy = exportData.Average(Function(c) c.confidence)

        MessageBox.Show($"All {pdfHandler.GetPageCount} pages are processed with {nWorkers.Value} workers with an accuracy of {accuracy}% in {workingTime.Hours} hours, {workingTime.Minutes} minutes and {workingTime.Seconds} seconds. You can now export the results.")
    End Sub
End Class
