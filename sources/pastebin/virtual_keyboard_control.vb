Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 03/25/2013
'Changed: 03/28/2013
'Version: 1.1.0
'------------------

Class VirtualKeyboard
    Inherits Control

    Private TextBitmap As Bitmap
    Private TextGraphics As Graphics

    Const LowerKeys As String = "1234567890-=qwertyuiop[]asdfghjkl\;'zxcvbnm,./`"
    Const UpperKeys As String = "!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL|:""ZXCVBNM<>?~"

    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)

        Font = New Font("Verdana", 8.25F)

        TextBitmap = New Bitmap(1, 1)
        TextGraphics = Graphics.FromImage(TextBitmap)

        MinimumSize = New Size(386, 162)
        MaximumSize = New Size(386, 162)

        Lower = LowerKeys.ToCharArray()
        Upper = UpperKeys.ToCharArray()

        PrepareCache()
    End Sub

    Public Target As Control
    Public Sub AssignControl(c As Control)
        Target = c
    End Sub

    Private Shift As Boolean

    Private Pressed As Integer = -1
    Private Buttons As Rectangle()

    Private Lower As Char()
    Private Upper As Char()
    Private Other As String() = {"Shift", "Space", "Back"}

    Private UpperCache As PointF()
    Private LowerCache As PointF()

    Private Sub PrepareCache()
        Buttons = New Rectangle(50) {}
        UpperCache = New PointF(Upper.Length - 1) {}
        LowerCache = New PointF(Lower.Length - 1) {}

        Dim I As Integer

        Dim S As SizeF
        Dim R As Rectangle

        For Y As Integer = 0 To 3
            For X As Integer = 0 To 11
                I = (Y * 12) + X
                R = New Rectangle(X * 32, Y * 32, 32, 32)

                Buttons(I) = R

                If Not I = 47 AndAlso Not Char.IsLetter(Upper(I)) Then
                    S = TextGraphics.MeasureString(Upper(I), Font)
                    UpperCache(I) = New PointF(R.X + (R.Width \ 2 - S.Width / 2), R.Y + R.Height - S.Height - 2)

                    S = TextGraphics.MeasureString(Lower(I), Font)
                    LowerCache(I) = New PointF(R.X + (R.Width \ 2 - S.Width / 2), R.Y + R.Height - S.Height - 2)
                End If
            Next
        Next

        Buttons(48) = New Rectangle(0, 4 * 32, 2 * 32, 32)
        Buttons(49) = New Rectangle(Buttons(48).Right, 4 * 32, 8 * 32, 32)
        Buttons(50) = New Rectangle(Buttons(49).Right, 4 * 32, 2 * 32, 32)
    End Sub

    Private G As Graphics
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        G = e.Graphics
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        G.Clear(SystemColors.Control)

        Dim S As SizeF
        Dim P As PointF
        Dim R As Rectangle

        Dim Offset As Integer

        G.DrawRectangle(SystemPens.ControlDarkDark, 0, 0, (12 * 32) + 1, (5 * 32) + 1)

        For I As Integer = 0 To Buttons.Length - 1
            R = Buttons(I)

            Offset = 0
            If I = Pressed Then Offset = 1

            Select Case I
                Case 48, 49, 50
                    S = G.MeasureString(Other(I - 48), Font)
                    G.DrawString(Other(I - 48), Font, SystemBrushes.ControlText, R.X + (R.Width \ 2 - S.Width / 2) + Offset, R.Y + (R.Height \ 2 - S.Height / 2) + Offset)
                Case 47
                    DrawArrow(R.X + Offset, R.Y + Offset)
                Case Else
                    If Shift Then
                        G.DrawString(Upper(I), Font, SystemBrushes.ControlText, R.X + 3 + Offset, R.Y + 2 + Offset)

                        If Not Char.IsLetter(Lower(I)) Then
                            P = LowerCache(I)
                            G.DrawString(Lower(I), Font, SystemBrushes.ControlDark, P.X + Offset, P.Y + Offset)
                        End If
                    Else
                        G.DrawString(Lower(I), Font, SystemBrushes.ControlText, R.X + 3 + Offset, R.Y + 2 + Offset)

                        If Not Char.IsLetter(Upper(I)) Then
                            P = UpperCache(I)
                            G.DrawString(Upper(I), Font, SystemBrushes.ControlDark, P.X + Offset, P.Y + Offset)
                        End If
                    End If
            End Select

            G.DrawRectangle(SystemPens.ControlLightLight, R.X + 1 + Offset, R.Y + 1 + Offset, R.Width - 2, R.Height - 2)
            G.DrawRectangle(SystemPens.ControlDark, R.X + Offset, R.Y + Offset, R.Width, R.Height)

            If I = Pressed Then
                G.DrawLine(SystemPens.ControlDarkDark, R.X, R.Y, R.Right, R.Y)
                G.DrawLine(SystemPens.ControlDarkDark, R.X, R.Y, R.X, R.Bottom)
            End If
        Next
    End Sub

    Private Sub DrawArrow(rx As Integer, ry As Integer)
        Dim R As New Rectangle(rx + 8, ry + 8, 16, 16)
        G.SmoothingMode = SmoothingMode.AntiAlias

        Dim P As New Pen(SystemColors.ControlText, 1)
        Dim C As New AdjustableArrowCap(3, 2)
        P.CustomEndCap = C

        G.DrawArc(P, R, 0.0F, 290.0F)

        P.Dispose()
        C.Dispose()
        G.SmoothingMode = SmoothingMode.None
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Dim Index As Integer = ((e.Y \ 32) * 12) + (e.X \ 32)

        If Index > 47 Then
            For I As Integer = 48 To Buttons.Length - 1
                If Buttons(I).Contains(e.X, e.Y) Then
                    Pressed = I
                    Exit For
                End If
            Next
        Else
            Pressed = Index
        End If

        HandleKey()
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        Pressed = -1
        Invalidate()
    End Sub

    Private Sub HandleKey()
        If Target Is Nothing Then Return
        If Pressed = -1 Then Return

        Select Case Pressed
            Case 47
                Target.Text = String.Empty
            Case 48
                Shift = Not Shift
            Case 49
                Target.Text &= " "
            Case 50
                If Not Target.Text.Length = 0 Then
                    Target.Text = Target.Text.Remove(Target.Text.Length - 1)
                End If
            Case Else
                If Shift Then
                    Target.Text &= Upper(Pressed)
                Else
                    Target.Text &= Lower(Pressed)
                End If
        End Select
    End Sub

End Class