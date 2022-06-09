<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.bPdf = New System.Windows.Forms.Button()
        Me.ofdPdf = New System.Windows.Forms.OpenFileDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tX = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tH = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tW = New System.Windows.Forms.TextBox()
        Me.tY = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CanvasX = New System.Windows.Forms.Panel()
        Me.Canvas = New System.Windows.Forms.PictureBox()
        Me.CanvasShadow = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tConf = New System.Windows.Forms.TextBox()
        Me.rResult = New System.Windows.Forms.RichTextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.bExtractAll = New System.Windows.Forms.Button()
        Me.tPages = New System.Windows.Forms.TextBox()
        Me.bLast = New System.Windows.Forms.Button()
        Me.bNext = New System.Windows.Forms.Button()
        Me.bPrev = New System.Windows.Forms.Button()
        Me.bFirst = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lZoom = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.tScale = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CanvasX.SuspendLayout()
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.tScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bPdf
        '
        Me.bPdf.Location = New System.Drawing.Point(3, 4)
        Me.bPdf.Name = "bPdf"
        Me.bPdf.Size = New System.Drawing.Size(75, 23)
        Me.bPdf.TabIndex = 1
        Me.bPdf.Text = "Load PDF"
        Me.bPdf.UseVisualStyleBackColor = True
        '
        'ofdPdf
        '
        Me.ofdPdf.DefaultExt = "pdf"
        Me.ofdPdf.Filter = "PDF files|*.pdf"
        Me.ofdPdf.Title = "Select PDF file"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 15)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Height:"
        '
        'tX
        '
        Me.tX.Location = New System.Drawing.Point(86, 6)
        Me.tX.Name = "tX"
        Me.tX.ReadOnly = True
        Me.tX.Size = New System.Drawing.Size(100, 23)
        Me.tX.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Width:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(63, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "X:"
        '
        'tH
        '
        Me.tH.Location = New System.Drawing.Point(86, 93)
        Me.tH.Name = "tH"
        Me.tH.ReadOnly = True
        Me.tH.Size = New System.Drawing.Size(100, 23)
        Me.tH.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Y:"
        '
        'tW
        '
        Me.tW.Location = New System.Drawing.Point(86, 64)
        Me.tW.Name = "tW"
        Me.tW.ReadOnly = True
        Me.tW.Size = New System.Drawing.Size(100, 23)
        Me.tW.TabIndex = 5
        '
        'tY
        '
        Me.tY.Location = New System.Drawing.Point(86, 35)
        Me.tY.Name = "tY"
        Me.tY.ReadOnly = True
        Me.tY.Size = New System.Drawing.Size(100, 23)
        Me.tY.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.PDFHelper.My.Resources.Resources.zoom_in
        Me.Button1.Location = New System.Drawing.Point(359, 4)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(24, 24)
        Me.Button1.TabIndex = 1
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Image = Global.PDFHelper.My.Resources.Resources.zoom_out
        Me.Button2.Location = New System.Drawing.Point(384, 4)
        Me.Button2.Margin = New System.Windows.Forms.Padding(0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 24)
        Me.Button2.TabIndex = 2
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Image = Global.PDFHelper.My.Resources.Resources.zoom_fit
        Me.Button3.Location = New System.Drawing.Point(409, 4)
        Me.Button3.Margin = New System.Windows.Forms.Padding(0)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(24, 24)
        Me.Button3.TabIndex = 3
        Me.Button3.UseVisualStyleBackColor = True
        '
        'CanvasX
        '
        Me.CanvasX.AutoScroll = True
        Me.CanvasX.AutoScrollMargin = New System.Drawing.Size(10, 10)
        Me.CanvasX.AutoSize = True
        Me.CanvasX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CanvasX.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.CanvasX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CanvasX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CanvasX.CausesValidation = False
        Me.CanvasX.Controls.Add(Me.Canvas)
        Me.CanvasX.Controls.Add(Me.CanvasShadow)
        Me.CanvasX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CanvasX.Location = New System.Drawing.Point(0, 32)
        Me.CanvasX.Margin = New System.Windows.Forms.Padding(0)
        Me.CanvasX.Name = "CanvasX"
        Me.CanvasX.Size = New System.Drawing.Size(409, 536)
        Me.CanvasX.TabIndex = 4
        '
        'Canvas
        '
        Me.Canvas.BackColor = System.Drawing.Color.White
        Me.Canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Canvas.Location = New System.Drawing.Point(26, 49)
        Me.Canvas.Name = "Canvas"
        Me.Canvas.Size = New System.Drawing.Size(292, 410)
        Me.Canvas.TabIndex = 1
        Me.Canvas.TabStop = False
        '
        'CanvasShadow
        '
        Me.CanvasShadow.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.CanvasShadow.CausesValidation = False
        Me.CanvasShadow.Enabled = False
        Me.CanvasShadow.Location = New System.Drawing.Point(35, 56)
        Me.CanvasShadow.Name = "CanvasShadow"
        Me.CanvasShadow.Size = New System.Drawing.Size(292, 410)
        Me.CanvasShadow.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.tScale)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.tConf)
        Me.Panel1.Controls.Add(Me.rResult)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.tX)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.tY)
        Me.Panel1.Controls.Add(Me.tW)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.tH)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(409, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(227, 536)
        Me.Panel1.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Confidence:"
        '
        'tConf
        '
        Me.tConf.Location = New System.Drawing.Point(86, 181)
        Me.tConf.Name = "tConf"
        Me.tConf.ReadOnly = True
        Me.tConf.Size = New System.Drawing.Size(100, 23)
        Me.tConf.TabIndex = 12
        '
        'rResult
        '
        Me.rResult.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rResult.Location = New System.Drawing.Point(0, 210)
        Me.rResult.Name = "rResult"
        Me.rResult.ReadOnly = True
        Me.rResult.Size = New System.Drawing.Size(227, 326)
        Me.rResult.TabIndex = 11
        Me.rResult.Text = ""
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(47, 122)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(148, 23)
        Me.Button4.TabIndex = 9
        Me.Button4.Text = "Test"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.bExtractAll)
        Me.Panel2.Controls.Add(Me.tPages)
        Me.Panel2.Controls.Add(Me.bLast)
        Me.Panel2.Controls.Add(Me.bNext)
        Me.Panel2.Controls.Add(Me.bPrev)
        Me.Panel2.Controls.Add(Me.bFirst)
        Me.Panel2.Controls.Add(Me.bPdf)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(636, 32)
        Me.Panel2.TabIndex = 12
        '
        'bExtractAll
        '
        Me.bExtractAll.Enabled = False
        Me.bExtractAll.Location = New System.Drawing.Point(80, 4)
        Me.bExtractAll.Name = "bExtractAll"
        Me.bExtractAll.Size = New System.Drawing.Size(95, 23)
        Me.bExtractAll.TabIndex = 14
        Me.bExtractAll.Text = "Extract data"
        Me.bExtractAll.UseVisualStyleBackColor = True
        '
        'tPages
        '
        Me.tPages.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tPages.Location = New System.Drawing.Point(505, 5)
        Me.tPages.Name = "tPages"
        Me.tPages.ReadOnly = True
        Me.tPages.Size = New System.Drawing.Size(74, 23)
        Me.tPages.TabIndex = 8
        Me.tPages.Text = "-/-"
        Me.tPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'bLast
        '
        Me.bLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bLast.Image = Global.PDFHelper.My.Resources.Resources.action_goto_last
        Me.bLast.Location = New System.Drawing.Point(607, 5)
        Me.bLast.Margin = New System.Windows.Forms.Padding(0)
        Me.bLast.Name = "bLast"
        Me.bLast.Size = New System.Drawing.Size(24, 24)
        Me.bLast.TabIndex = 7
        Me.bLast.UseVisualStyleBackColor = True
        '
        'bNext
        '
        Me.bNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bNext.Image = Global.PDFHelper.My.Resources.Resources.action_goto_next
        Me.bNext.Location = New System.Drawing.Point(582, 5)
        Me.bNext.Margin = New System.Windows.Forms.Padding(0)
        Me.bNext.Name = "bNext"
        Me.bNext.Size = New System.Drawing.Size(24, 24)
        Me.bNext.TabIndex = 6
        Me.bNext.UseVisualStyleBackColor = True
        '
        'bPrev
        '
        Me.bPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bPrev.Image = Global.PDFHelper.My.Resources.Resources.action_goto_previous
        Me.bPrev.Location = New System.Drawing.Point(478, 4)
        Me.bPrev.Margin = New System.Windows.Forms.Padding(0)
        Me.bPrev.Name = "bPrev"
        Me.bPrev.Size = New System.Drawing.Size(24, 24)
        Me.bPrev.TabIndex = 5
        Me.bPrev.UseVisualStyleBackColor = True
        '
        'bFirst
        '
        Me.bFirst.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bFirst.Image = Global.PDFHelper.My.Resources.Resources.action_goto_first
        Me.bFirst.Location = New System.Drawing.Point(453, 4)
        Me.bFirst.Margin = New System.Windows.Forms.Padding(0)
        Me.bFirst.Name = "bFirst"
        Me.bFirst.Size = New System.Drawing.Size(24, 24)
        Me.bFirst.TabIndex = 4
        Me.bFirst.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lZoom})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 568)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(636, 24)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lZoom
        '
        Me.lZoom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.lZoom.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.lZoom.Name = "lZoom"
        Me.lZoom.Size = New System.Drawing.Size(77, 19)
        Me.lZoom.Text = "Zoom: 100%"
        '
        'sfd
        '
        Me.sfd.DefaultExt = "csv"
        Me.sfd.Filter = "CSV Files|*.csv"
        Me.sfd.SupportMultiDottedExtensions = True
        Me.sfd.Title = "Save extracted data"
        '
        'tScale
        '
        Me.tScale.AutoSize = False
        Me.tScale.LargeChange = 1
        Me.tScale.Location = New System.Drawing.Point(86, 153)
        Me.tScale.Minimum = 1
        Me.tScale.Name = "tScale"
        Me.tScale.Size = New System.Drawing.Size(100, 22)
        Me.tScale.TabIndex = 14
        Me.tScale.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tScale.Value = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 160)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Scale:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 592)
        Me.Controls.Add(Me.CanvasX)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.CanvasX.ResumeLayout(False)
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.tScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bPdf As Button
    Friend WithEvents ofdPdf As OpenFileDialog
    Friend WithEvents Label4 As Label
    Friend WithEvents tX As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tH As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tW As TextBox
    Friend WithEvents tY As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents CanvasX As Panel
    Friend WithEvents Canvas As PictureBox
    Friend WithEvents CanvasShadow As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lZoom As ToolStripStatusLabel
    Friend WithEvents tPages As TextBox
    Friend WithEvents bLast As Button
    Friend WithEvents bNext As Button
    Friend WithEvents bPrev As Button
    Friend WithEvents bFirst As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents rResult As RichTextBox
    Friend WithEvents bExtractAll As Button
    Friend WithEvents sfd As SaveFileDialog
    Friend WithEvents Label5 As Label
    Friend WithEvents tConf As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents tScale As TrackBar
End Class
