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
        components = New ComponentModel.Container()
        bPdf = New Button()
        ofdPdf = New OpenFileDialog()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        CanvasX = New Panel()
        Canvas = New PictureBox()
        CanvasShadow = New Panel()
        Panel1 = New Panel()
        sc1 = New SplitContainer()
        rResult = New RichTextBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        Label1 = New Label()
        mpRegion = New ComboBox()
        tRegex = New TextBox()
        cMatching = New CheckBox()
        Panel4 = New Panel()
        bMailpackIndicator = New Button()
        lRegions = New ListView()
        cIdx = New ColumnHeader()
        cLeft = New ColumnHeader()
        ctop = New ColumnHeader()
        cright = New ColumnHeader()
        cBottom = New ColumnHeader()
        bClear = New Button()
        bDel = New Button()
        lScale = New Label()
        bSaveTest = New Button()
        tScale = New TrackBar()
        Label6 = New Label()
        bTest = New Button()
        pWorkers = New FlowLayoutPanel()
        Panel2 = New Panel()
        bView = New Button()
        bExport = New Button()
        Label8 = New Label()
        nWorkers = New NumericUpDown()
        bExtractAll = New Button()
        bLast = New Button()
        bNext = New Button()
        bPrev = New Button()
        bFirst = New Button()
        StatusStrip1 = New StatusStrip()
        lZoom = New ToolStripStatusLabel()
        tLog = New ToolStripStatusLabel()
        sfd = New SaveFileDialog()
        fbd = New FolderBrowserDialog()
        tt = New ToolTip(components)
        cbPages = New ComboBox()
        CanvasX.SuspendLayout()
        CType(Canvas, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(sc1, ComponentModel.ISupportInitialize).BeginInit()
        sc1.Panel1.SuspendLayout()
        sc1.Panel2.SuspendLayout()
        sc1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel4.SuspendLayout()
        CType(tScale, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        CType(nWorkers, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' bPdf
        ' 
        bPdf.Location = New Point(3, 4)
        bPdf.Name = "bPdf"
        bPdf.Size = New Size(75, 23)
        bPdf.TabIndex = 1
        bPdf.Text = "Load PDF"
        bPdf.UseVisualStyleBackColor = True
        ' 
        ' ofdPdf
        ' 
        ofdPdf.DefaultExt = "pdf"
        ofdPdf.Filter = "PDF files|*.pdf"
        ofdPdf.Title = "Select PDF file"
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button1.Image = My.Resources.Resources.zoom_in
        Button1.Location = New Point(414, 4)
        Button1.Margin = New Padding(0)
        Button1.Name = "Button1"
        Button1.Size = New Size(24, 24)
        Button1.TabIndex = 1
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button2.Image = My.Resources.Resources.zoom_out
        Button2.Location = New Point(439, 4)
        Button2.Margin = New Padding(0)
        Button2.Name = "Button2"
        Button2.Size = New Size(24, 24)
        Button2.TabIndex = 2
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button3.Image = My.Resources.Resources.zoom_fit
        Button3.Location = New Point(464, 4)
        Button3.Margin = New Padding(0)
        Button3.Name = "Button3"
        Button3.Size = New Size(24, 24)
        Button3.TabIndex = 3
        Button3.UseVisualStyleBackColor = True
        ' 
        ' CanvasX
        ' 
        CanvasX.AutoScroll = True
        CanvasX.AutoScrollMargin = New Size(10, 10)
        CanvasX.AutoSize = True
        CanvasX.AutoSizeMode = AutoSizeMode.GrowAndShrink
        CanvasX.BackColor = SystemColors.AppWorkspace
        CanvasX.BackgroundImageLayout = ImageLayout.None
        CanvasX.BorderStyle = BorderStyle.Fixed3D
        CanvasX.CausesValidation = False
        CanvasX.Controls.Add(Canvas)
        CanvasX.Controls.Add(CanvasShadow)
        CanvasX.Dock = DockStyle.Fill
        CanvasX.Location = New Point(0, 32)
        CanvasX.Margin = New Padding(0)
        CanvasX.Name = "CanvasX"
        CanvasX.Size = New Size(469, 486)
        CanvasX.TabIndex = 4
        ' 
        ' Canvas
        ' 
        Canvas.BackColor = Color.White
        Canvas.BackgroundImageLayout = ImageLayout.None
        Canvas.Location = New Point(26, 49)
        Canvas.Name = "Canvas"
        Canvas.Size = New Size(292, 410)
        Canvas.TabIndex = 1
        Canvas.TabStop = False
        ' 
        ' CanvasShadow
        ' 
        CanvasShadow.BackColor = SystemColors.WindowFrame
        CanvasShadow.CausesValidation = False
        CanvasShadow.Enabled = False
        CanvasShadow.Location = New Point(35, 56)
        CanvasShadow.Name = "CanvasShadow"
        CanvasShadow.Size = New Size(292, 410)
        CanvasShadow.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(sc1)
        Panel1.Controls.Add(Panel4)
        Panel1.Controls.Add(pWorkers)
        Panel1.Dock = DockStyle.Right
        Panel1.Location = New Point(469, 32)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(252, 486)
        Panel1.TabIndex = 11
        ' 
        ' sc1
        ' 
        sc1.Dock = DockStyle.Fill
        sc1.IsSplitterFixed = True
        sc1.Location = New Point(0, 122)
        sc1.Margin = New Padding(0)
        sc1.Name = "sc1"
        ' 
        ' sc1.Panel1
        ' 
        sc1.Panel1.Controls.Add(rResult)
        ' 
        ' sc1.Panel2
        ' 
        sc1.Panel2.Controls.Add(TableLayoutPanel1)
        sc1.Panel2Collapsed = True
        sc1.Size = New Size(252, 101)
        sc1.SplitterDistance = 84
        sc1.SplitterWidth = 1
        sc1.TabIndex = 19
        ' 
        ' rResult
        ' 
        rResult.Dock = DockStyle.Fill
        rResult.Location = New Point(0, 0)
        rResult.Name = "rResult"
        rResult.ReadOnly = True
        rResult.Size = New Size(252, 101)
        rResult.TabIndex = 11
        rResult.Text = ""
        rResult.WordWrap = False
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(Label1, 0, 0)
        TableLayoutPanel1.Controls.Add(mpRegion, 1, 1)
        TableLayoutPanel1.Controls.Add(tRegex, 0, 2)
        TableLayoutPanel1.Controls.Add(cMatching, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 28F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(96, 100)
        TableLayoutPanel1.TabIndex = 5
        ' 
        ' Label1
        ' 
        TableLayoutPanel1.SetColumnSpan(Label1, 2)
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(90, 40)
        Label1.TabIndex = 3
        Label1.Text = "Enter a regular expression to indicate the first page of a multipage mailpack"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' mpRegion
        ' 
        mpRegion.Dock = DockStyle.Fill
        mpRegion.DropDownStyle = ComboBoxStyle.DropDownList
        mpRegion.FormattingEnabled = True
        mpRegion.Location = New Point(48, 40)
        mpRegion.Margin = New Padding(0)
        mpRegion.Name = "mpRegion"
        mpRegion.Size = New Size(48, 23)
        mpRegion.Sorted = True
        mpRegion.TabIndex = 4
        tt.SetToolTip(mpRegion, "Select the id of the region to match the regex against")
        ' 
        ' tRegex
        ' 
        tRegex.AutoCompleteCustomSource.AddRange(New String() {"\d", "\D", "\w", "\W", "\s", "\S", "[0-9A-F]", "[^0-9a-f]"})
        tRegex.AutoCompleteMode = AutoCompleteMode.Suggest
        tRegex.AutoCompleteSource = AutoCompleteSource.CustomSource
        TableLayoutPanel1.SetColumnSpan(tRegex, 2)
        tRegex.Dock = DockStyle.Fill
        tRegex.Location = New Point(0, 68)
        tRegex.Margin = New Padding(0)
        tRegex.Multiline = True
        tRegex.Name = "tRegex"
        tRegex.PlaceholderText = "Eg. ^\d{8}$"
        tRegex.Size = New Size(96, 32)
        tRegex.TabIndex = 1
        ' 
        ' cMatching
        ' 
        cMatching.AutoSize = True
        cMatching.Dock = DockStyle.Fill
        cMatching.Location = New Point(0, 40)
        cMatching.Margin = New Padding(0)
        cMatching.Name = "cMatching"
        cMatching.Size = New Size(48, 28)
        cMatching.TabIndex = 5
        cMatching.Text = "Use matching? ID:"
        cMatching.UseVisualStyleBackColor = True
        ' 
        ' Panel4
        ' 
        Panel4.BorderStyle = BorderStyle.Fixed3D
        Panel4.Controls.Add(bMailpackIndicator)
        Panel4.Controls.Add(lRegions)
        Panel4.Controls.Add(bClear)
        Panel4.Controls.Add(bDel)
        Panel4.Controls.Add(lScale)
        Panel4.Controls.Add(bSaveTest)
        Panel4.Controls.Add(tScale)
        Panel4.Controls.Add(Label6)
        Panel4.Controls.Add(bTest)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(252, 122)
        Panel4.TabIndex = 18
        ' 
        ' bMailpackIndicator
        ' 
        bMailpackIndicator.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bMailpackIndicator.Image = My.Resources.Resources.clip_cut
        bMailpackIndicator.Location = New Point(96, 94)
        bMailpackIndicator.Margin = New Padding(0)
        bMailpackIndicator.Name = "bMailpackIndicator"
        bMailpackIndicator.Size = New Size(24, 24)
        bMailpackIndicator.TabIndex = 21
        tt.SetToolTip(bMailpackIndicator, "Multipage mailpacks")
        bMailpackIndicator.UseVisualStyleBackColor = True
        ' 
        ' lRegions
        ' 
        lRegions.BorderStyle = BorderStyle.FixedSingle
        lRegions.Columns.AddRange(New ColumnHeader() {cIdx, cLeft, ctop, cright, cBottom})
        lRegions.Dock = DockStyle.Top
        lRegions.Font = New Font("Segoe UI", 8.25F)
        lRegions.FullRowSelect = True
        lRegions.GridLines = True
        lRegions.HeaderStyle = ColumnHeaderStyle.Nonclickable
        lRegions.LabelWrap = False
        lRegions.Location = New Point(0, 0)
        lRegions.Margin = New Padding(0)
        lRegions.Name = "lRegions"
        lRegions.ShowGroups = False
        lRegions.Size = New Size(248, 94)
        lRegions.Sorting = SortOrder.Ascending
        lRegions.TabIndex = 20
        lRegions.UseCompatibleStateImageBehavior = False
        lRegions.View = View.Details
        ' 
        ' cIdx
        ' 
        cIdx.Text = "Id"
        cIdx.Width = 25
        ' 
        ' cLeft
        ' 
        cLeft.Text = "X"
        cLeft.TextAlign = HorizontalAlignment.Center
        cLeft.Width = 50
        ' 
        ' ctop
        ' 
        ctop.Text = "Y"
        ctop.TextAlign = HorizontalAlignment.Center
        ctop.Width = 50
        ' 
        ' cright
        ' 
        cright.Text = "Width"
        cright.TextAlign = HorizontalAlignment.Center
        cright.Width = 50
        ' 
        ' cBottom
        ' 
        cBottom.Text = "Height"
        cBottom.TextAlign = HorizontalAlignment.Center
        cBottom.Width = 50
        ' 
        ' bClear
        ' 
        bClear.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bClear.Image = My.Resources.Resources.cancel
        bClear.Location = New Point(72, 94)
        bClear.Margin = New Padding(0)
        bClear.Name = "bClear"
        bClear.Size = New Size(24, 24)
        bClear.TabIndex = 19
        tt.SetToolTip(bClear, "Remove all clippingpaths")
        bClear.UseVisualStyleBackColor = True
        ' 
        ' bDel
        ' 
        bDel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bDel.Image = My.Resources.Resources.remove
        bDel.Location = New Point(48, 94)
        bDel.Margin = New Padding(0)
        bDel.Name = "bDel"
        bDel.Size = New Size(24, 24)
        bDel.TabIndex = 18
        tt.SetToolTip(bDel, "Remove selected clippingpath")
        bDel.UseVisualStyleBackColor = True
        ' 
        ' lScale
        ' 
        lScale.AutoSize = True
        lScale.Location = New Point(234, 99)
        lScale.Name = "lScale"
        lScale.Size = New Size(13, 15)
        lScale.TabIndex = 16
        lScale.Text = "4"
        ' 
        ' bSaveTest
        ' 
        bSaveTest.Image = My.Resources.Resources.save
        bSaveTest.Location = New Point(24, 94)
        bSaveTest.Margin = New Padding(0)
        bSaveTest.Name = "bSaveTest"
        bSaveTest.Size = New Size(24, 24)
        bSaveTest.TabIndex = 11
        tt.SetToolTip(bSaveTest, "Export all (or selected) clippingpath images")
        bSaveTest.UseVisualStyleBackColor = True
        ' 
        ' tScale
        ' 
        tScale.AutoSize = False
        tScale.LargeChange = 1
        tScale.Location = New Point(151, 96)
        tScale.Minimum = 1
        tScale.Name = "tScale"
        tScale.Size = New Size(85, 22)
        tScale.TabIndex = 14
        tt.SetToolTip(tScale, "Level of enlargement. Level 4 is best for most.")
        tScale.Value = 4
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(121, 99)
        Label6.Name = "Label6"
        Label6.Size = New Size(37, 15)
        Label6.TabIndex = 15
        Label6.Text = "Scale:"
        ' 
        ' bTest
        ' 
        bTest.Image = My.Resources.Resources.spell_check
        bTest.Location = New Point(0, 94)
        bTest.Margin = New Padding(0)
        bTest.Name = "bTest"
        bTest.Size = New Size(24, 24)
        bTest.TabIndex = 9
        tt.SetToolTip(bTest, "Test all (or selected) clippingpaths")
        bTest.UseVisualStyleBackColor = True
        ' 
        ' pWorkers
        ' 
        pWorkers.AutoScroll = True
        pWorkers.BackColor = SystemColors.WindowFrame
        pWorkers.BorderStyle = BorderStyle.Fixed3D
        pWorkers.Dock = DockStyle.Bottom
        pWorkers.FlowDirection = FlowDirection.TopDown
        pWorkers.Location = New Point(0, 223)
        pWorkers.Name = "pWorkers"
        pWorkers.Size = New Size(252, 263)
        pWorkers.TabIndex = 11
        pWorkers.WrapContents = False
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(cbPages)
        Panel2.Controls.Add(bView)
        Panel2.Controls.Add(bExport)
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(nWorkers)
        Panel2.Controls.Add(bExtractAll)
        Panel2.Controls.Add(bLast)
        Panel2.Controls.Add(bNext)
        Panel2.Controls.Add(bPrev)
        Panel2.Controls.Add(bFirst)
        Panel2.Controls.Add(bPdf)
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(Button2)
        Panel2.Controls.Add(Button3)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(721, 32)
        Panel2.TabIndex = 12
        ' 
        ' bView
        ' 
        bView.Enabled = False
        bView.Location = New Point(355, 4)
        bView.Name = "bView"
        bView.Size = New Size(55, 23)
        bView.TabIndex = 18
        bView.Text = "View"
        bView.UseVisualStyleBackColor = True
        ' 
        ' bExport
        ' 
        bExport.Enabled = False
        bExport.Location = New Point(294, 4)
        bExport.Name = "bExport"
        bExport.Size = New Size(55, 23)
        bExport.TabIndex = 17
        bExport.Text = "Export"
        bExport.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(181, 8)
        Label8.Name = "Label8"
        Label8.Size = New Size(53, 15)
        Label8.TabIndex = 16
        Label8.Text = "Workers:"
        ' 
        ' nWorkers
        ' 
        nWorkers.Location = New Point(240, 5)
        nWorkers.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        nWorkers.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nWorkers.Name = "nWorkers"
        nWorkers.Size = New Size(48, 23)
        nWorkers.TabIndex = 15
        nWorkers.Value = New Decimal(New Integer() {4, 0, 0, 0})
        ' 
        ' bExtractAll
        ' 
        bExtractAll.Enabled = False
        bExtractAll.Location = New Point(80, 4)
        bExtractAll.Name = "bExtractAll"
        bExtractAll.Size = New Size(95, 23)
        bExtractAll.TabIndex = 14
        bExtractAll.Text = "Extract data"
        bExtractAll.UseVisualStyleBackColor = True
        ' 
        ' bLast
        ' 
        bLast.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bLast.Image = My.Resources.Resources.action_goto_last
        bLast.Location = New Point(692, 5)
        bLast.Margin = New Padding(0)
        bLast.Name = "bLast"
        bLast.Size = New Size(24, 24)
        bLast.TabIndex = 7
        bLast.UseVisualStyleBackColor = True
        ' 
        ' bNext
        ' 
        bNext.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bNext.Image = My.Resources.Resources.action_goto_next
        bNext.Location = New Point(667, 5)
        bNext.Margin = New Padding(0)
        bNext.Name = "bNext"
        bNext.Size = New Size(24, 24)
        bNext.TabIndex = 6
        bNext.UseVisualStyleBackColor = True
        ' 
        ' bPrev
        ' 
        bPrev.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bPrev.Image = My.Resources.Resources.action_goto_previous
        bPrev.Location = New Point(533, 4)
        bPrev.Margin = New Padding(0)
        bPrev.Name = "bPrev"
        bPrev.Size = New Size(24, 24)
        bPrev.TabIndex = 5
        bPrev.UseVisualStyleBackColor = True
        ' 
        ' bFirst
        ' 
        bFirst.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bFirst.Image = My.Resources.Resources.action_goto_first
        bFirst.Location = New Point(508, 4)
        bFirst.Margin = New Padding(0)
        bFirst.Name = "bFirst"
        bFirst.Size = New Size(24, 24)
        bFirst.TabIndex = 4
        bFirst.UseVisualStyleBackColor = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {lZoom, tLog})
        StatusStrip1.Location = New Point(0, 518)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(721, 24)
        StatusStrip1.TabIndex = 13
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lZoom
        ' 
        lZoom.BorderSides = ToolStripStatusLabelBorderSides.Right
        lZoom.BorderStyle = Border3DStyle.Bump
        lZoom.Name = "lZoom"
        lZoom.Size = New Size(77, 19)
        lZoom.Text = "Zoom: 100%"
        ' 
        ' tLog
        ' 
        tLog.DisplayStyle = ToolStripItemDisplayStyle.Text
        tLog.LiveSetting = Automation.AutomationLiveSetting.Polite
        tLog.Name = "tLog"
        tLog.Size = New Size(30, 19)
        tLog.Text = "Log:"
        ' 
        ' sfd
        ' 
        sfd.DefaultExt = "csv"
        sfd.Filter = "CSV Files|*.csv"
        sfd.SupportMultiDottedExtensions = True
        sfd.Title = "Save extracted data"
        ' 
        ' fbd
        ' 
        fbd.Description = "Select save folder"
        fbd.UseDescriptionForTitle = True
        ' 
        ' tt
        ' 
        tt.BackColor = Color.Khaki
        ' 
        ' cbPages
        ' 
        cbPages.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cbPages.FormattingEnabled = True
        cbPages.Location = New Point(560, 5)
        cbPages.Name = "cbPages"
        cbPages.Size = New Size(104, 23)
        cbPages.TabIndex = 19
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(721, 542)
        Controls.Add(CanvasX)
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Controls.Add(StatusStrip1)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "PDF Xtracktor"
        CanvasX.ResumeLayout(False)
        CType(Canvas, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        sc1.Panel1.ResumeLayout(False)
        sc1.Panel2.ResumeLayout(False)
        CType(sc1, ComponentModel.ISupportInitialize).EndInit()
        sc1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        CType(tScale, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(nWorkers, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents bPdf As Button
    Friend WithEvents ofdPdf As OpenFileDialog
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
    Friend WithEvents bLast As Button
    Friend WithEvents bNext As Button
    Friend WithEvents bPrev As Button
    Friend WithEvents bFirst As Button
    Friend WithEvents bTest As Button
    Friend WithEvents rResult As RichTextBox
    Friend WithEvents bExtractAll As Button
    Friend WithEvents sfd As SaveFileDialog
    Friend WithEvents Label6 As Label
    Friend WithEvents tScale As TrackBar
    Friend WithEvents lScale As Label
    Friend WithEvents pWorkers As FlowLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents nWorkers As NumericUpDown
    Friend WithEvents bExport As Button
    Friend WithEvents bSaveTest As Button
    Friend WithEvents fbd As FolderBrowserDialog
    Friend WithEvents bClear As Button
    Friend WithEvents bDel As Button
    Friend WithEvents lRegions As ListView
    Friend WithEvents cIdx As ColumnHeader
    Friend WithEvents cLeft As ColumnHeader
    Friend WithEvents ctop As ColumnHeader
    Friend WithEvents cright As ColumnHeader
    Friend WithEvents cBottom As ColumnHeader
    Friend WithEvents tt As ToolTip
    Friend WithEvents sc1 As SplitContainer
    Friend WithEvents bMailpackIndicator As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tRegex As TextBox
    Friend WithEvents mpRegion As ComboBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents cMatching As CheckBox
    Friend WithEvents bView As Button
    Friend WithEvents tLog As ToolStripStatusLabel
    Friend WithEvents cbPages As ComboBox
End Class
