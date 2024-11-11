Imports ImageMagick

Public Class ClippingPath
    Private Shared NextIdx As Integer = 0

    Public ReadOnly Property idx As Integer
    Public ReadOnly Property region As MagickGeometry

    Sub New(left As Integer, top As Integer, right As Integer, bottom As Integer)
        _idx = NextIdx
        NextIdx += 1
        _region = New MagickGeometry(left, top, CUInt(right), CUInt(bottom))
    End Sub
End Class

