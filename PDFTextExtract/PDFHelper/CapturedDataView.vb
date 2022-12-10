Imports System.Text.RegularExpressions

Public Class CapturedDataView
    Property exportData As List(Of PDFTextExtract.ExtractedData)
    Private bd As BindingSource


    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub CapturedDataView_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim data = (From x In exportData Select New With {
                                             .PageIndex = x.pageIndex,
                                             .Confidence = x.confidence,
                                             .Text = Regex.Replace(x.text, "(?:\r\n|\r|\n)", "\n", RegexOptions.IgnoreCase Or RegexOptions.Singleline)})

        bd = New BindingSource
        bd.DataSource = data.ToArray

        dgv1.DataSource = bd
        dgv1.Refresh()
    End Sub

    Private Sub dgv1_ColumnHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv1.ColumnHeaderMouseDoubleClick
        Select Case e.ColumnIndex
            Case 0 'pageidx
                bd.DataSource = exportData.OrderBy(Function(c) c.pageIndex).ToArray
            Case 1 ' confidence
                bd.DataSource = exportData.OrderBy(Function(c) c.confidence).ToArray
            Case 3 'text
                bd.DataSource = exportData.OrderBy(Function(c) c.text).ToArray
        End Select
    End Sub
End Class