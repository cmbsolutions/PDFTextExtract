﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.components = New System.ComponentModel.Container()
        Me.bPdf = New System.Windows.Forms.Button()
        Me.ofdPdf = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CanvasX = New System.Windows.Forms.Panel()
        Me.Canvas = New System.Windows.Forms.PictureBox()
        Me.CanvasShadow = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.sc1 = New System.Windows.Forms.SplitContainer()
        Me.rResult = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mpRegion = New System.Windows.Forms.ComboBox()
        Me.tRegex = New System.Windows.Forms.TextBox()
        Me.cMatching = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.bMailpackIndicator = New System.Windows.Forms.Button()
        Me.lRegions = New System.Windows.Forms.ListView()
        Me.cIdx = New System.Windows.Forms.ColumnHeader()
        Me.cLeft = New System.Windows.Forms.ColumnHeader()
        Me.ctop = New System.Windows.Forms.ColumnHeader()
        Me.cright = New System.Windows.Forms.ColumnHeader()
        Me.cBottom = New System.Windows.Forms.ColumnHeader()
        Me.bClear = New System.Windows.Forms.Button()
        Me.bDel = New System.Windows.Forms.Button()
        Me.lScale = New System.Windows.Forms.Label()
        Me.bSaveTest = New System.Windows.Forms.Button()
        Me.tScale = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.bTest = New System.Windows.Forms.Button()
        Me.pWorkers = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.bView = New System.Windows.Forms.Button()
        Me.bExport = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nWorkers = New System.Windows.Forms.NumericUpDown()
        Me.bExtractAll = New System.Windows.Forms.Button()
        Me.tPages = New System.Windows.Forms.TextBox()
        Me.bLast = New System.Windows.Forms.Button()
        Me.bNext = New System.Windows.Forms.Button()
        Me.bPrev = New System.Windows.Forms.Button()
        Me.bFirst = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lZoom = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tLog = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog()
        Me.tt = New System.Windows.Forms.ToolTip(Me.components)
        Me.CanvasX.SuspendLayout()
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.sc1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc1.Panel1.SuspendLayout()
        Me.sc1.Panel2.SuspendLayout()
        Me.sc1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.tScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.nWorkers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
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
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.PDFHelper.My.Resources.Resources.zoom_in
        Me.Button1.Location = New System.Drawing.Point(447, 4)
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
        Me.Button2.Location = New System.Drawing.Point(472, 4)
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
        Me.Button3.Location = New System.Drawing.Point(497, 4)
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
        Me.CanvasX.Size = New System.Drawing.Size(472, 486)
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
        Me.Panel1.Controls.Add(Me.sc1)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.pWorkers)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(472, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(252, 486)
        Me.Panel1.TabIndex = 11
        '
        'sc1
        '
        Me.sc1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc1.IsSplitterFixed = True
        Me.sc1.Location = New System.Drawing.Point(0, 122)
        Me.sc1.Margin = New System.Windows.Forms.Padding(0)
        Me.sc1.Name = "sc1"
        '
        'sc1.Panel1
        '
        Me.sc1.Panel1.Controls.Add(Me.rResult)
        '
        'sc1.Panel2
        '
        Me.sc1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.sc1.Panel2Collapsed = True
        Me.sc1.Size = New System.Drawing.Size(252, 101)
        Me.sc1.SplitterDistance = 84
        Me.sc1.SplitterWidth = 1
        Me.sc1.TabIndex = 19
        '
        'rResult
        '
        Me.rResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rResult.Location = New System.Drawing.Point(0, 0)
        Me.rResult.Name = "rResult"
        Me.rResult.ReadOnly = True
        Me.rResult.Size = New System.Drawing.Size(252, 101)
        Me.rResult.TabIndex = 11
        Me.rResult.Text = ""
        Me.rResult.WordWrap = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.mpRegion, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tRegex, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cMatching, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(96, 100)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'Label1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 2)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 40)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Enter a regular expression to indicate the first page of a multipage mailpack"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'mpRegion
        '
        Me.mpRegion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mpRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mpRegion.FormattingEnabled = True
        Me.mpRegion.Location = New System.Drawing.Point(48, 40)
        Me.mpRegion.Margin = New System.Windows.Forms.Padding(0)
        Me.mpRegion.Name = "mpRegion"
        Me.mpRegion.Size = New System.Drawing.Size(48, 23)
        Me.mpRegion.Sorted = True
        Me.mpRegion.TabIndex = 4
        Me.tt.SetToolTip(Me.mpRegion, "Select the id of the region to match the regex against")
        '
        'tRegex
        '
        Me.tRegex.AutoCompleteCustomSource.AddRange(New String() {"\d", "\D", "\w", "\W", "\s", "\S", "[0-9A-F]", "[^0-9a-f]"})
        Me.tRegex.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.tRegex.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.TableLayoutPanel1.SetColumnSpan(Me.tRegex, 2)
        Me.tRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tRegex.Location = New System.Drawing.Point(0, 68)
        Me.tRegex.Margin = New System.Windows.Forms.Padding(0)
        Me.tRegex.Multiline = True
        Me.tRegex.Name = "tRegex"
        Me.tRegex.PlaceholderText = "Eg. ^\d{8}$"
        Me.tRegex.Size = New System.Drawing.Size(96, 32)
        Me.tRegex.TabIndex = 1
        '
        'cMatching
        '
        Me.cMatching.AutoSize = True
        Me.cMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cMatching.Location = New System.Drawing.Point(0, 40)
        Me.cMatching.Margin = New System.Windows.Forms.Padding(0)
        Me.cMatching.Name = "cMatching"
        Me.cMatching.Size = New System.Drawing.Size(48, 28)
        Me.cMatching.TabIndex = 5
        Me.cMatching.Text = "Use matching? ID:"
        Me.cMatching.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.bMailpackIndicator)
        Me.Panel4.Controls.Add(Me.lRegions)
        Me.Panel4.Controls.Add(Me.bClear)
        Me.Panel4.Controls.Add(Me.bDel)
        Me.Panel4.Controls.Add(Me.lScale)
        Me.Panel4.Controls.Add(Me.bSaveTest)
        Me.Panel4.Controls.Add(Me.tScale)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.bTest)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(252, 122)
        Me.Panel4.TabIndex = 18
        '
        'bMailpackIndicator
        '
        Me.bMailpackIndicator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bMailpackIndicator.Image = Global.PDFHelper.My.Resources.Resources.clip_cut
        Me.bMailpackIndicator.Location = New System.Drawing.Point(96, 94)
        Me.bMailpackIndicator.Margin = New System.Windows.Forms.Padding(0)
        Me.bMailpackIndicator.Name = "bMailpackIndicator"
        Me.bMailpackIndicator.Size = New System.Drawing.Size(24, 24)
        Me.bMailpackIndicator.TabIndex = 21
        Me.tt.SetToolTip(Me.bMailpackIndicator, "Multipage mailpacks")
        Me.bMailpackIndicator.UseVisualStyleBackColor = True
        '
        'lRegions
        '
        Me.lRegions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lRegions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cIdx, Me.cLeft, Me.ctop, Me.cright, Me.cBottom})
        Me.lRegions.Dock = System.Windows.Forms.DockStyle.Top
        Me.lRegions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lRegions.FullRowSelect = True
        Me.lRegions.GridLines = True
        Me.lRegions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lRegions.LabelWrap = False
        Me.lRegions.Location = New System.Drawing.Point(0, 0)
        Me.lRegions.Margin = New System.Windows.Forms.Padding(0)
        Me.lRegions.Name = "lRegions"
        Me.lRegions.ShowGroups = False
        Me.lRegions.Size = New System.Drawing.Size(248, 94)
        Me.lRegions.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lRegions.TabIndex = 20
        Me.lRegions.UseCompatibleStateImageBehavior = False
        Me.lRegions.View = System.Windows.Forms.View.Details
        '
        'cIdx
        '
        Me.cIdx.Text = "Id"
        Me.cIdx.Width = 25
        '
        'cLeft
        '
        Me.cLeft.Text = "X"
        Me.cLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cLeft.Width = 50
        '
        'ctop
        '
        Me.ctop.Text = "Y"
        Me.ctop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ctop.Width = 50
        '
        'cright
        '
        Me.cright.Text = "Width"
        Me.cright.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cright.Width = 50
        '
        'cBottom
        '
        Me.cBottom.Text = "Height"
        Me.cBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cBottom.Width = 50
        '
        'bClear
        '
        Me.bClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bClear.Image = Global.PDFHelper.My.Resources.Resources.cancel
        Me.bClear.Location = New System.Drawing.Point(72, 94)
        Me.bClear.Margin = New System.Windows.Forms.Padding(0)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(24, 24)
        Me.bClear.TabIndex = 19
        Me.tt.SetToolTip(Me.bClear, "Remove all clippingpaths")
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bDel
        '
        Me.bDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bDel.Image = Global.PDFHelper.My.Resources.Resources.remove
        Me.bDel.Location = New System.Drawing.Point(48, 94)
        Me.bDel.Margin = New System.Windows.Forms.Padding(0)
        Me.bDel.Name = "bDel"
        Me.bDel.Size = New System.Drawing.Size(24, 24)
        Me.bDel.TabIndex = 18
        Me.tt.SetToolTip(Me.bDel, "Remove selected clippingpath")
        Me.bDel.UseVisualStyleBackColor = True
        '
        'lScale
        '
        Me.lScale.AutoSize = True
        Me.lScale.Location = New System.Drawing.Point(234, 99)
        Me.lScale.Name = "lScale"
        Me.lScale.Size = New System.Drawing.Size(13, 15)
        Me.lScale.TabIndex = 16
        Me.lScale.Text = "4"
        '
        'bSaveTest
        '
        Me.bSaveTest.Image = Global.PDFHelper.My.Resources.Resources.save
        Me.bSaveTest.Location = New System.Drawing.Point(24, 94)
        Me.bSaveTest.Margin = New System.Windows.Forms.Padding(0)
        Me.bSaveTest.Name = "bSaveTest"
        Me.bSaveTest.Size = New System.Drawing.Size(24, 24)
        Me.bSaveTest.TabIndex = 11
        Me.tt.SetToolTip(Me.bSaveTest, "Export all (or selected) clippingpath images")
        Me.bSaveTest.UseVisualStyleBackColor = True
        '
        'tScale
        '
        Me.tScale.AutoSize = False
        Me.tScale.LargeChange = 1
        Me.tScale.Location = New System.Drawing.Point(151, 96)
        Me.tScale.Minimum = 1
        Me.tScale.Name = "tScale"
        Me.tScale.Size = New System.Drawing.Size(85, 22)
        Me.tScale.TabIndex = 14
        Me.tt.SetToolTip(Me.tScale, "Level of enlargement. Level 4 is best for most.")
        Me.tScale.Value = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(121, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Scale:"
        '
        'bTest
        '
        Me.bTest.Image = Global.PDFHelper.My.Resources.Resources.spell_check
        Me.bTest.Location = New System.Drawing.Point(0, 94)
        Me.bTest.Margin = New System.Windows.Forms.Padding(0)
        Me.bTest.Name = "bTest"
        Me.bTest.Size = New System.Drawing.Size(24, 24)
        Me.bTest.TabIndex = 9
        Me.tt.SetToolTip(Me.bTest, "Test all (or selected) clippingpaths")
        Me.bTest.UseVisualStyleBackColor = True
        '
        'pWorkers
        '
        Me.pWorkers.AutoScroll = True
        Me.pWorkers.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.pWorkers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pWorkers.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pWorkers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.pWorkers.Location = New System.Drawing.Point(0, 223)
        Me.pWorkers.Name = "pWorkers"
        Me.pWorkers.Size = New System.Drawing.Size(252, 263)
        Me.pWorkers.TabIndex = 11
        Me.pWorkers.WrapContents = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.bView)
        Me.Panel2.Controls.Add(Me.bExport)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.nWorkers)
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
        Me.Panel2.Size = New System.Drawing.Size(724, 32)
        Me.Panel2.TabIndex = 12
        '
        'bView
        '
        Me.bView.Enabled = False
        Me.bView.Location = New System.Drawing.Point(355, 4)
        Me.bView.Name = "bView"
        Me.bView.Size = New System.Drawing.Size(55, 23)
        Me.bView.TabIndex = 18
        Me.bView.Text = "View"
        Me.bView.UseVisualStyleBackColor = True
        '
        'bExport
        '
        Me.bExport.Enabled = False
        Me.bExport.Location = New System.Drawing.Point(294, 4)
        Me.bExport.Name = "bExport"
        Me.bExport.Size = New System.Drawing.Size(55, 23)
        Me.bExport.TabIndex = 17
        Me.bExport.Text = "Export"
        Me.bExport.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(181, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 15)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Workers:"
        '
        'nWorkers
        '
        Me.nWorkers.Location = New System.Drawing.Point(240, 5)
        Me.nWorkers.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nWorkers.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nWorkers.Name = "nWorkers"
        Me.nWorkers.Size = New System.Drawing.Size(48, 23)
        Me.nWorkers.TabIndex = 15
        Me.nWorkers.Value = New Decimal(New Integer() {4, 0, 0, 0})
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
        Me.tPages.Location = New System.Drawing.Point(593, 5)
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
        Me.bLast.Location = New System.Drawing.Point(695, 5)
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
        Me.bNext.Location = New System.Drawing.Point(670, 5)
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
        Me.bPrev.Location = New System.Drawing.Point(566, 4)
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
        Me.bFirst.Location = New System.Drawing.Point(541, 4)
        Me.bFirst.Margin = New System.Windows.Forms.Padding(0)
        Me.bFirst.Name = "bFirst"
        Me.bFirst.Size = New System.Drawing.Size(24, 24)
        Me.bFirst.TabIndex = 4
        Me.bFirst.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lZoom, Me.tLog})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 518)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(724, 24)
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
        'tLog
        '
        Me.tLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tLog.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite
        Me.tLog.Name = "tLog"
        Me.tLog.Size = New System.Drawing.Size(30, 19)
        Me.tLog.Text = "Log:"
        '
        'sfd
        '
        Me.sfd.DefaultExt = "csv"
        Me.sfd.Filter = "CSV Files|*.csv"
        Me.sfd.SupportMultiDottedExtensions = True
        Me.sfd.Title = "Save extracted data"
        '
        'fbd
        '
        Me.fbd.Description = "Select save folder"
        Me.fbd.UseDescriptionForTitle = True
        '
        'tt
        '
        Me.tt.BackColor = System.Drawing.Color.Khaki
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(724, 542)
        Me.Controls.Add(Me.CanvasX)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PDF Xtracktor"
        Me.CanvasX.ResumeLayout(False)
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.sc1.Panel1.ResumeLayout(False)
        Me.sc1.Panel2.ResumeLayout(False)
        CType(Me.sc1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.tScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.nWorkers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents tPages As TextBox
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
End Class
