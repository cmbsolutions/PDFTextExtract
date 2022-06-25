Public Class ExtractedData
    Public ReadOnly Property pageIndex As Integer
    Public ReadOnly Property confidence As Single
    Public ReadOnly Property clipIdx As Integer
    Public ReadOnly Property text As String

    Sub New(c As Single, t As String, p As Integer, i As Integer)
        _confidence = c
        _text = t
        _pageIndex = p
        _clipIdx = i
    End Sub
End Class
