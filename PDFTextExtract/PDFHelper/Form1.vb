﻿Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports PDFTextExtract

Public Class Form1
    Private currentPdf As String

    Private pdfScale As Integer = 4
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

            Dim idx = pdfHandler.AddClippingPath(lt.X, lt.Y, rb.X, rb.Y)
            Dim itm As New ListViewItem
            itm.SubItems(0).Text = idx.ToString
            itm.SubItems.Add(lt.X.ToString)
            itm.SubItems.Add(lt.Y.ToString)
            itm.SubItems.Add(rb.X.ToString)
            itm.SubItems.Add(rb.Y.ToString)

            lRegions.Items.Add(itm)

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

            For Each itm As ListViewItem In lRegions.Items
                Dim x = CInt(itm.SubItems.Item(1).Text)
                Dim y = CInt(itm.SubItems.Item(2).Text)
                Dim w = CInt(itm.SubItems.Item(3).Text)
                Dim h = CInt(itm.SubItems.Item(4).Text)

                g.DrawRectangle(New Pen(RandomColor, 2), CInt(x * _currentZoomFactor), CInt(y * _currentZoomFactor), CInt(w * _currentZoomFactor), CInt(h * _currentZoomFactor))
            Next
        End Using
        Canvas.Image = zbmp
    End Sub

    Private rnd As New Random
    Private Function RandomColor() As Color
        Return Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255))
    End Function

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

    Private Async Sub bTest_Click(sender As Object, e As EventArgs) Handles bTest.Click
        Dim datas As List(Of ExtractedData)

        Dim renderTask = Task.Run(Function()
                                      Return pdfHandler.extractData()
                                  End Function)
        datas = Await renderTask.ConfigureAwait(True)

        Dim data As New ExtractedData(datas.Average(Function(c) c.confidence), Strings.Join(datas.Select(Of String)(Function(c) c.text).ToArray), datas.First.pageIndex, 0)

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

        pdfHandler.BeginExtractAllData(CInt(nWorkers.Value))
    End Sub

    Private Sub tScale_Scroll(sender As Object, e As EventArgs) Handles tScale.Scroll
        pdfScale = tScale.Value
        pdfHandler.SetScale(pdfScale)
        lScale.Text = pdfScale.ToString

        LoadPage(True)

        pdfHandler.ResetClippingPaths()
        lRegions.Items.Clear()
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
                    sw.WriteLine("PageIndex;RegionIndex;Region;Accuracy;CapturedText")
                    For Each d In exportData
                        sw.WriteLine($"{d.pageIndex};{d.clipIdx};{pdfHandler.clippingPaths.First(Function(c) c.idx = d.clipIdx).region};{d.confidence};{Regex.Replace(d.text, "(?:\r\n|\r|\n)", "\n", RegexOptions.IgnoreCase Or RegexOptions.Singleline)}")
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

    Private Sub bAdd_Click(sender As Object, e As EventArgs) Handles bAdd.Click
        Dim idx = pdfHandler.AddClippingPath(lt.X, lt.Y, rb.X, rb.Y)
        Dim itm As New ListViewItem
        itm.SubItems(0).Text = idx.ToString
        itm.SubItems.Add(lt.X.ToString)
        itm.SubItems.Add(lt.Y.ToString)
        itm.SubItems.Add(rb.X.ToString)
        itm.SubItems.Add(rb.Y.ToString)

        lRegions.Items.Add(itm)
    End Sub

    Private Sub bClear_Click(sender As Object, e As EventArgs) Handles bClear.Click
        If MessageBox.Show("Weet je zeker dat je alle regio's wilt verwijderen?", "Verwijderen", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            pdfHandler.ResetClippingPaths()
            lRegions.Items.Clear()
        End If
    End Sub

    Private Sub bDel_Click(sender As Object, e As EventArgs) Handles bDel.Click
        If lRegions.SelectedItems.Count = 0 Then Exit Sub

        Dim idx As Integer = CInt(lRegions.SelectedItems(0).Text)
        Dim itm = pdfHandler.clippingPaths.FirstOrDefault(Function(c) c.idx = idx)

        If itm IsNot Nothing Then
            pdfHandler.RemoveClippingPath(itm)
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
