Imports PDFiumSharp
Imports PDFiumSharp.Types

Public Class Form1
    Private currentPdf As String
    Private doc As PdfDocument
    Private pageIndex As Integer = 0
    Private pageCount As Integer = 0
    Private pageSize As FS_SIZEF
    Private pdfScale As Integer = 3
    Private RC As Rectangle
    Private screenPtA, screenPtB As Point
    Private boxPta, boxPtb As Point
    Private _localImg As Image

    Private _currentZoomFactor As Double = 1.0

    Private Sub Canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles Canvas.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim pb As PictureBox = CType(sender, PictureBox)
            Cursor.Clip = pb.RectangleToScreen(pb.ClientRectangle)
            screenPtA = Cursor.Position
            screenPtB = screenPtA
            RC = NormalizedRC(screenPtA, screenPtB)
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
        End If
    End Sub

    Private Sub Canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles Canvas.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
            screenPtB = Cursor.Position
            RC = NormalizedRC(screenPtA, screenPtB)
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
        End If
    End Sub

    Private Sub Canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles Canvas.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Cursor.Clip = Nothing
            Dim pb As PictureBox = CType(sender, PictureBox)
            RC = pb.RectangleToClient(RC)

            Dim rect As New Rectangle
            rect.X = Convert.ToInt32(RC.X / _currentZoomFactor)
            rect.Y = Convert.ToInt32(RC.Y / _currentZoomFactor)
            rect.Width = Convert.ToInt32(RC.Width / _currentZoomFactor)
            rect.Height = Convert.ToInt32(RC.Height / _currentZoomFactor)

            tX.Text = rect.X.ToString
            tY.Text = rect.Y.ToString
            tW.Text = rect.Width.ToString
            tH.Text = rect.Height.ToString
        End If
    End Sub
    Public Function NormalizedRC(ByVal ptA As Point, ByVal ptB As Point) As Rectangle
        Return New Rectangle(Math.Min(ptA.X, ptB.X), Math.Min(ptA.Y, ptB.Y), Math.Abs(ptA.X - ptB.X), Math.Abs(ptA.Y - ptB.Y))
    End Function

    Private Sub bPdf_Click(sender As Object, e As EventArgs) Handles bPdf.Click
        If ofdPdf.ShowDialog = DialogResult.OK Then
            currentPdf = ofdPdf.FileName
            doc = New PdfDocument(currentPdf)

            pageCount = doc.Pages.Count
            pageIndex = 1
            pageSize = New FS_SIZEF(CSng(doc.Pages(pageIndex).Width), CSng(doc.Pages(pageIndex).Height))
            handleButtons()
            LoadPage()
        End If
    End Sub

    Private Sub LoadPage()
        Dim width = CInt(pageSize.Width * pdfScale)
        Dim height = CInt(pageSize.Height * pdfScale)

        Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)
            bm.Fill(New Types.FPDF_COLOR(255, 255, 255))

            doc.Pages(pageIndex - 1).Render(bm, (0, 0, width, height))

            _localImg = Image.FromStream(bm.AsBmpStream(300, 300))
        End Using

        If _localImg IsNot Nothing Then
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
        End If

        RedrawCanvas()
        ResizeAndCenterCanvas()
    End Sub

    Private Sub RedrawCanvas()
        If _localImg Is Nothing Then Exit Sub
        Dim zbmp As New Bitmap(_localImg, Convert.ToInt32(_localImg.Width * _currentZoomFactor), Convert.ToInt32(_localImg.Height * _currentZoomFactor))
        Dim g = Graphics.FromImage(zbmp)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
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
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Sub tsbZoomReset_Click(sender As Object, e As EventArgs) Handles Button3.Click
        _currentZoomFactor = 1.0
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Sub bFirst_Click(sender As Object, e As EventArgs) Handles bFirst.Click
        pageIndex = 1
        handleButtons()
        LoadPage()
    End Sub

    Private Sub bPrev_Click(sender As Object, e As EventArgs) Handles bPrev.Click
        pageIndex -= 1
        handleButtons()
        LoadPage()
    End Sub

    Private Sub bNext_Click(sender As Object, e As EventArgs) Handles bNext.Click
        pageIndex += 1
        handleButtons()
        LoadPage()
    End Sub

    Private Sub bLast_Click(sender As Object, e As EventArgs) Handles bLast.Click
        pageIndex = pageCount
        handleButtons()
        LoadPage()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeAndCenterCanvas()
        RedrawCanvas()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim data As String = ""

        Using pe = New PDFTextExtract.PdfHandler(currentPdf)
            data = pe.extractData(New FS_RECTF(CSng(tX.Text), CSng(tY.Text), CSng(tW.Text), CSng(tH.Text)), pageIndex - 1)
        End Using

        rResult.Text = data
    End Sub

    Private Sub handleButtons()
        If pageIndex <= 1 Then
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

        If pageIndex >= pageCount Then
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

        tPages.Text = $"{pageIndex}/{pageCount}"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        handleButtons()
    End Sub
End Class
