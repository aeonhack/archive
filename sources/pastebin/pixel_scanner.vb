'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 11/24/2011
'Changed: 11/24/2011
'Version: 1.0.1
'------------------
Class ScreenSearch

    Private G As Graphics
    Private B As Bitmap
    Private R As Rectangle
    Private BD As BitmapData

    Private X, Y As Integer
    Private Row, Column As Integer
    Private Count As Integer
    Private _GetPoint As Byte()

    Sub New()
        B = New Bitmap(1, 1)
        G = Graphics.FromImage(B)
    End Sub

    Sub GetScreen(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        B.Dispose()
        G.Dispose()

        R = New Rectangle(0, 0, width, height)
        B = New Bitmap(width, height, PixelFormat.Format24bppRgb)
        G = Graphics.FromImage(B)

        G.CopyFromScreen(x, y, 0, 0, R.Size, CopyPixelOperation.SourceCopy)
    End Sub

    Function GetPixel(ByVal x As Integer, ByVal y As Integer) As Color
        Return B.GetPixel(x, y)
    End Function

    Function GetPoint(ByVal c As Color, ByVal offset As Integer) As Point
        Count = 0

        BD = B.LockBits(R, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
        _GetPoint = New Byte((BD.Stride * R.Height) - 1) {}

        Marshal.Copy(BD.Scan0, _GetPoint, 0, _GetPoint.Length)
        B.UnlockBits(BD)

        For Y = 0 To R.Height - 1
            Row = Y * BD.Stride
            For X = 0 To R.Width - 1
                Column = Row + X * 3
                If _GetPoint(Column) = c.B AndAlso _
                    _GetPoint(Column + 1) = c.G AndAlso _
                    _GetPoint(Column + 2) = c.R Then

                    If Count = offset Then
                        Return New Point(X, Y)
                    Else
                        Count += 1
                    End If
                End If
            Next
        Next

        Return New Point(-1, -1)
    End Function

    Function Contains(ByVal c As Color) As Boolean
        Return Not (GetPoint(c, 0).X = -1)
    End Function

End Class