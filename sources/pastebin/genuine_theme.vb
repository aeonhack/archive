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
Class GenuineTheme
    Inherits ThemeContainer152

    Sub New()
        MoveHeight = 30
        BackColor = Color.FromArgb(41, 41, 41)
        TransparencyKey = Color.Fuchsia

        SetColor("Back", 41, 41, 41)
        SetColor("Gradient1", 25, 25, 25)
        SetColor("Gradient2", 41, 41, 41)
        SetColor("Line1", 25, 25, 25)
        SetColor("Line2", 58, 58, 58)
        SetColor("Text", Color.White)
        SetColor("Border1", 58, 58, 58)
        SetColor("Border2", Color.Black)
    End Sub

    Private C1, C2, C3 As Color
    Private P1, P2, P3, P4 As Pen
    Private B1 As SolidBrush

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Back")
        C2 = GetColor("Gradient1")
        C3 = GetColor("Gradient2")

        P1 = New Pen(GetColor("Line1"))
        P2 = New Pen(GetColor("Line2"))
        P3 = New Pen(GetColor("Border1"))
        P4 = New Pen(GetColor("Border2"))

        B1 = New SolidBrush(GetColor("Text"))

        BackColor = C1
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(C1)

        DrawGradient(C2, C3, 0, 0, Width, 28)

        G.DrawLine(P1, 0, 28, Width, 28)
        G.DrawLine(P2, 0, 29, Width, 29)

        DrawText(B1, HorizontalAlignment.Left, 7, 0)

        DrawBorders(P3, 1)
        DrawBorders(P4)

        DrawCorners(TransparencyKey)
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
Class GenuineButton
    Inherits ThemeControl152

    Sub New()
        SetColor("DownGradient1", 41, 41, 41)
        SetColor("DownGradient2", 51, 51, 51)
        SetColor("NoneGradient1", 51, 51, 51)
        SetColor("NoneGradient2", 41, 41, 41)
        SetColor("Text", Color.White)
        SetColor("Border1", 12, Color.White)
        SetColor("Border2", 25, 25, 25)
    End Sub

    Private C1, C2, C3, C4 As Color
    Private B1 As SolidBrush
    Private P1, P2 As Pen

    Protected Overrides Sub ColorHook()
        C1 = GetColor("DownGradient1")
        C2 = GetColor("DownGradient2")
        C3 = GetColor("NoneGradient1")
        C4 = GetColor("NoneGradient2")

        B1 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("Border1"))
        P2 = New Pen(GetColor("Border2"))
    End Sub

    Protected Overrides Sub PaintHook()
        If State = MouseState.Down Then
            DrawGradient(C1, C2, ClientRectangle, 90.0F)
        Else
            DrawGradient(C3, C4, ClientRectangle, 90.0F)
        End If

        DrawText(B1, HorizontalAlignment.Center, 0, 0)

        DrawBorders(P1, 1)
        DrawBorders(P2)

        DrawCorners(BackColor)
    End Sub

End Class