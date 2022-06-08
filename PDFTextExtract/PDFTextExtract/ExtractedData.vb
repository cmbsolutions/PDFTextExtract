Public Class ExtractedData
    Public ReadOnly Property confidence As Single
    Public ReadOnly Property text As String
    Public ReadOnly Property pageIndex As Integer

    Sub New(c As Single, t As String, p As Integer)
        _confidence = c
        _text = t
        _pageIndex = p
    End Sub
End Class
