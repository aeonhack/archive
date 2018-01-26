Imports System, System.Collections
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms


'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 9/23/2011
'Changed: 9/23/2011
'Version: 1.0.0
'Theme Base: 1.5.2
'------------------
Class FutureTheme
    Inherits ThemeContainer152

    Sub New()
        MoveHeight = 24
        BackColor = Color.FromArgb(34, 34, 34)

        SetColor("Back", 34, 34, 34)
        SetColor("Gradient1", 34, 34, 34)
        SetColor("Gradient2", 23, 23, 23)
        SetColor("Border1", 34, 34, 34)
        SetColor("Border2", 49, 49, 49)
        SetColor("Border3", Color.Black)
        SetColor("Line1", Color.Black)
        SetColor("Line2", Color.Black)
        SetColor("Hatch1", Color.Black)
        SetColor("Hatch2", 34, 34, 34)
        SetColor("Shade1", 70, Color.Black)
        SetColor("Shade2", Color.Transparent)
        SetColor("Text", Color.White)
    End Sub

    Private C1, C2, C3, C4, C5 As Color
    Private P1, P2, P3, P4, P5 As Pen
    Private B1 As HatchBrush
    Private B2 As SolidBrush

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Back")
        C2 = GetColor("Gradient1")
        C3 = GetColor("Gradient2")
        C4 = GetColor("Shade1")
        C5 = GetColor("Shade2")

        P1 = New Pen(GetColor("Border1"))
        P2 = New Pen(GetColor("Line1"))
        P3 = New Pen(GetColor("Line2"))
        P4 = New Pen(GetColor("Border2"))
        P5 = New Pen(GetColor("Border3"))

        B1 = New HatchBrush(HatchStyle.DarkUpwardDiagonal, GetColor("Hatch1"), GetColor("Hatch2"))
        B2 = New SolidBrush(GetColor("Text"))

        BackColor = C1
    End Sub

    Private RT1 As Rectangle

    Protected Overrides Sub PaintHook()
        G.Clear(C1)

        RT1 = New Rectangle(1, 1, Width - 2, 22)
        DrawGradient(C2, C3, RT1, 90.0F)
        DrawBorders(P1, RT1)

        G.DrawLine(P2, 0, 23, Width, 23)

        G.FillRectangle(B1, 0, 24, Width, 13)

        DrawGradient(C4, C5, 0, 24, Width, 6)

        G.DrawLine(P3, 0, 37, Width, 37)
        DrawBorders(P4, 1, 38, Width - 2, Height - 39)

        DrawText(B2, HorizontalAlignment.Left, 5, 0)

        DrawBorders(P5)
    End Sub

End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 9/23/2011
'Changed: 9/23/2011
'Version: 1.0.0
'Theme Base: 1.5.2
'------------------
Class FutureButton
    Inherits ThemeControl152

    Private Blend As ColorBlend

    Sub New()
        SetColor("Blend1", 28, 28, 28)
        SetColor("Blend2", 32, 32, 32)
        SetColor("Blend3", 24, 24, 24)
        SetColor("Border1A", 40, 40, 40)
        SetColor("Border1B", 48, 48, 48)
        SetColor("Border2", Color.Black)
        SetColor("Border3", 24, 24, 24)
        SetColor("Line1", 44, 44, 44)
        SetColor("TextShade", Color.Black)
        SetColor("Text", Color.White)
        SetColor("Corners", 40, 40, 40)

        Blend = New ColorBlend
        Blend.Positions = New Single() {0.0F, 0.5F, 1.0F}
    End Sub

    Private P1, P2, P3, P4 As Pen
    Private B1, B2 As SolidBrush
    Private C1, C2, C3 As Color

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Border1A")
        C2 = GetColor("Border1B")
        C3 = GetColor("Corners")

        P2 = New Pen(GetColor("Border2"))
        P3 = New Pen(GetColor("Border3"))
        P4 = New Pen(GetColor("Line1"))

        B1 = New SolidBrush(GetColor("TextShade"))
        B2 = New SolidBrush(GetColor("Text"))

        Blend.Colors = New Color() {GetColor("Blend1"), GetColor("Blend2"), GetColor("Blend3")}
    End Sub

    Private GB1 As LinearGradientBrush

    Protected Overrides Sub PaintHook()
        DrawGradient(Blend, ClientRectangle, 90.0F)

        GB1 = New LinearGradientBrush(ClientRectangle, C1, C2, 90.0F)
        P1 = New Pen(GB1)

        DrawBorders(P1)
        DrawBorders(P2, 1)

        If State = MouseState.Down Then
            DrawBorders(P3, 2)

            DrawText(B1, HorizontalAlignment.Center, 2, 2)
            DrawText(B2, HorizontalAlignment.Center, 1, 1)
        Else
            G.DrawLine(P4, 2, 2, Width - 3, 2)

            DrawText(B1, HorizontalAlignment.Center, 1, 1)
            DrawText(B2, HorizontalAlignment.Center, 0, 0)
        End If

        DrawCorners(C3, 1, 1, Width - 2, Height - 2)
        DrawCorners(BackColor)
    End Sub
End Class