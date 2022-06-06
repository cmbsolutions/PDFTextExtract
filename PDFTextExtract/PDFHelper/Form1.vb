Imports PDFiumSharp
Imports PDFiumSharp.Types

Public Class Form1
    Private currentPdf As String
    Private doc As PdfDocument
    Private pageIndex As Integer = 0
    Private pageSize As FS_SIZEF
    Private scale As Integer = 2
    Private RC As Rectangle
    Private screenPtA, screenPtB As Point
    Private boxPta, boxPtb As Point
    Private _localImg As Image

    Private _currentZoomFactor As Double = 1.0

    Private Sub pPdf_MouseDown(sender As Object, e As MouseEventArgs) Handles pPdf.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim pb As PictureBox = CType(sender, PictureBox)
            Cursor.Clip = pb.RectangleToScreen(pb.ClientRectangle)
            screenPtA = Cursor.Position
            screenPtB = screenPtA
            RC = NormalizedRC(screenPtA, screenPtB)
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)

            Exit Sub
        End If
    End Sub

    Private Sub pPdf_MouseMove(sender As Object, e As MouseEventArgs) Handles pPdf.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
            screenPtB = Cursor.Position
            RC = NormalizedRC(screenPtA, screenPtB)
            ControlPaint.DrawReversibleFrame(RC, Color.Black, FrameStyle.Dashed)
        End If
    End Sub

    Private Sub pPdf_MouseUp(sender As Object, e As MouseEventArgs) Handles pPdf.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Cursor.Clip = Nothing
            Dim pb As PictureBox = CType(sender, PictureBox)
            RC = pb.RectangleToClient(RC)

            Dim rect As New Rectangle
            rect.X = Convert.ToInt32(RC.X * _currentZoomFactor)
            rect.Y = Convert.ToInt32(RC.Y * _currentZoomFactor)
            rect.Width = Convert.ToInt32(RC.Width * _currentZoomFactor)
            rect.Height = Convert.ToInt32(RC.Height * _currentZoomFactor)

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

            pageSize = New FS_SIZEF(CSng(doc.Pages(pageIndex).Width), CSng(doc.Pages(pageIndex).Height))
            LoadPage()
        End If
    End Sub

    Private Sub LoadPage()
        Dim width = CInt(pageSize.Width * scale)
        Dim height = CInt(pageSize.Height * scale)

        Using bm As New PDFiumBitmap(width, height, Enums.BitmapFormats.BGRA, IntPtr.Zero, 0)
            bm.Fill(New Types.FPDF_COLOR(255, 255, 255))

            doc.Pages(pageIndex).Render(bm, (0, 0, width, height))

            Dim g As Graphics = pPdf.CreateGraphics
            Dim b As New Bitmap(bm.AsBmpStream(300, 300))

            _localImg = b
            g.DrawImage(b, pPdf.ClientRectangle, New Rectangle(0, 0, width, height), GraphicsUnit.Pixel)
        End Using
    End Sub

    Private Sub ResizeAndCenterCanvas()
        Try
            If _localImg Is Nothing Then Exit Sub
            Dim cx As Integer = Convert.ToInt32(SplitContainer1.Panel1.Width / 2)
            Dim cy As Integer = Convert.ToInt32(SplitContainer1.Panel1.Height / 2)

            pPdf.Width = Convert.ToInt32(_localImg.Width * _currentZoomFactor)
            pPdf.Height = Convert.ToInt32(_localImg.Height * _currentZoomFactor)

            Dim px As Integer = Convert.ToInt32(pPdf.Width / 2)
            Dim py As Integer = Convert.ToInt32(pPdf.Height / 2)

            pPdf.Left = cx - px
            pPdf.Top = cy - py

            'canvasShadow.Width = canvas.Width
            'canvasShadow.Height = canvas.Height
            'canvasShadow.Left = canvas.Left + 5
            'canvasShadow.Top = canvas.Top + 5

            If SplitContainer1.Panel1.HorizontalScroll.Visible Or SplitContainer1.Panel1.VerticalScroll.Visible Then
                SplitContainer1.Panel1.AutoScrollPosition = New Point(px - cx, py - cy)
            End If

            'txtZoom.Text = String.Format("{0}%", Convert.ToInt32(100 * _currentZoomFactor))

        Catch ex As Exception

        End Try
    End Sub
    Private Sub tsbZoomIn_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            _currentZoomFactor += 0.1
            ResizeAndCenterCanvas()
            LoadPage()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub tsbZoomOut_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            _currentZoomFactor -= 0.1
            ResizeAndCenterCanvas()
            LoadPage()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tsbZoomReset_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            _currentZoomFactor = 1.0
            ResizeAndCenterCanvas()
            LoadPage()

        Catch ex As Exception

        End Try
    End Sub
End Class
