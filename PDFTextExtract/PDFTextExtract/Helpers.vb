Public Class Helpers
    Public Shared Sub dumpException(ByRef _ex As Exception)
        Try
            Trace.IndentSize = 4
            Trace.WriteLine(_ex.Message)
            Trace.Indent()

            Dim st As StackTrace = New StackTrace(_ex, True)
            For Each sf As StackFrame In st.GetFrames

                If sf.GetFileLineNumber > 0 OrElse sf.GetFileColumnNumber > 0 Then
                    Trace.WriteLine($"Trace line:{sf.GetFileLineNumber}, column:{sf.GetFileColumnNumber}, file:{sf.GetFileName}, method:{sf.GetMethod.Name}")
                End If

            Next
            Trace.Unindent()
        Catch ex As Exception
            Trace.WriteLine("Can't process error")
        End Try
    End Sub
End Class