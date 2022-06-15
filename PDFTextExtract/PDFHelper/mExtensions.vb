Imports System.Runtime.CompilerServices

Module mExtensions
    <Extension>
    Public Sub AppendBoldColoredText(rtb As RichTextBox, input As String, color As Color)
        rtb.SuspendLayout()
        Dim appendBoxFont As New Font(rtb.Font, FontStyle.Bold)

        rtb.SelectionStart = rtb.TextLength
        rtb.SelectionLength = 0
        rtb.SelectionFont = appendBoxFont
        rtb.SelectionColor = color
        rtb.SelectedText = input
        rtb.ResumeLayout()
    End Sub

    <Extension>
    Public Sub AppendColoredText(rtb As RichTextBox, input As String, color As Color)
        rtb.SuspendLayout()
        Dim appendBoxFont As New Font(rtb.Font, FontStyle.Regular)

        rtb.SelectionStart = rtb.TextLength
        rtb.SelectionLength = 0
        rtb.SelectionFont = appendBoxFont
        rtb.SelectionColor = color
        rtb.SelectedText = input
        rtb.ResumeLayout()
    End Sub

    <Extension>
    Public Sub AppendLineColoredText(rtb As RichTextBox, input As String, color As Color)
        rtb.AppendColoredText(input, color)
        rtb.AppendText(vbCrLf)
    End Sub

    <Extension>
    Public Sub AppendLineBoldColoredText(rtb As RichTextBox, input As String, color As Color)
        rtb.AppendBoldColoredText(input, color)
        rtb.AppendText(vbCrLf)
    End Sub
End Module
