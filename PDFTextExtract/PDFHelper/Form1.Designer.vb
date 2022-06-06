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
        Me.pPdf = New System.Windows.Forms.PictureBox()
        Me.bPdf = New System.Windows.Forms.Button()
        Me.ofdPdf = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
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
        CType(Me.pPdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pPdf
        '
        Me.pPdf.Location = New System.Drawing.Point(20, 14)
        Me.pPdf.Name = "pPdf"
        Me.pPdf.Size = New System.Drawing.Size(331, 495)
        Me.pPdf.TabIndex = 0
        Me.pPdf.TabStop = False
        '
        'bPdf
        '
        Me.bPdf.Location = New System.Drawing.Point(3, 2)
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
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 31)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.AutoScrollMargin = New System.Drawing.Size(10, 10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pPdf)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tX)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tH)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tW)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tY)
        Me.SplitContainer1.Size = New System.Drawing.Size(555, 532)
        Me.SplitContainer1.SplitterDistance = 375
        Me.SplitContainer1.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 15)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Height:"
        '
        'tX
        '
        Me.tX.Location = New System.Drawing.Point(65, 25)
        Me.tX.Name = "tX"
        Me.tX.ReadOnly = True
        Me.tX.Size = New System.Drawing.Size(100, 23)
        Me.tX.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Width:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "X:"
        '
        'tH
        '
        Me.tH.Location = New System.Drawing.Point(65, 112)
        Me.tH.Name = "tH"
        Me.tH.ReadOnly = True
        Me.tH.Size = New System.Drawing.Size(100, 23)
        Me.tH.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(42, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Y:"
        '
        'tW
        '
        Me.tW.Location = New System.Drawing.Point(65, 83)
        Me.tW.Name = "tW"
        Me.tW.ReadOnly = True
        Me.tW.Size = New System.Drawing.Size(100, 23)
        Me.tW.TabIndex = 5
        '
        'tY
        '
        Me.tY.Location = New System.Drawing.Point(65, 54)
        Me.tY.Name = "tY"
        Me.tY.ReadOnly = True
        Me.tY.Size = New System.Drawing.Size(100, 23)
        Me.tY.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(97, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Zoom in"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(178, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Zoom out"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(259, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Zoom reset"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 565)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.bPdf)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.pPdf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pPdf As PictureBox
    Friend WithEvents bPdf As Button
    Friend WithEvents ofdPdf As OpenFileDialog
    Friend WithEvents SplitContainer1 As SplitContainer
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
End Class
