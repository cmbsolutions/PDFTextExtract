Public Class CapturedDataView
    Property exportData As List(Of PDFTextExtract.ExtractedData)
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub CapturedDataView_Load(sender As Object, e As EventArgs) Handles Me.Load
        dgv1.DataSource = exportData.ToArray
    End Sub


End Class