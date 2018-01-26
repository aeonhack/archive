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
Class PrimeTheme
    Inherits ThemeContainer152

    Sub New()
        MoveHeight = 32
        BackColor = Color.White
        TransparencyKey = Color.Fuchsia

        SetColor("Sides", 232, 232, 232)
        SetColor("Gradient1", 252, 252, 252)
        SetColor("Gradient2", 242, 242, 242)
        SetColor("TextShade", Color.White)
        SetColor("Text", 80, 80, 80)
        SetColor("Back", Color.White)
        SetColor("Border1", 180, 180, 180)
        SetColor("Border2", Color.White)
        SetColor("Border3", Color.White)
        SetColor("Border4", 150, 150, 150)
    End Sub

    Private C1, C2, C3 As Color
    Private B1, B2, B3 As SolidBrush
    Private P1, P2, P3, P4 As Pen

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Sides")
        C2 = GetColor("Gradient1")
        C3 = GetColor("Gradient2")

        B1 = New SolidBrush(GetColor("TextShade"))
        B2 = New SolidBrush(GetColor("Text"))
        B3 = New SolidBrush(GetColor("Back"))

        P1 = New Pen(GetColor("Border1"))
        P2 = New Pen(GetColor("Border2"))
        P3 = New Pen(GetColor("Border3"))
        P4 = New Pen(GetColor("Border4"))

        BackColor = B3.Color
    End Sub

    Private RT1 As Rectangle

    Protected Overrides Sub PaintHook()
        G.Clear(C1)

        DrawGradient(C2, C3, 0, 0, Width, 15)

        DrawText(B1, HorizontalAlignment.Left, 13, 1)
        DrawText(B2, HorizontalAlignment.Left, 12, 0)

        RT1 = New Rectangle(12, 30, Width - 24, Height - 42)

        G.FillRectangle(B3, RT1)
        DrawBorders(P1, RT1, 1)
        DrawBorders(P2, RT1)

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
Class PrimeButton
    Inherits ThemeControl152

    Sub New()
        SetColor("DownGradient1", 215, 215, 215)
        SetColor("DownGradient2", 235, 235, 235)
        SetColor("NoneGradient1", 235, 235, 235)
        SetColor("NoneGradient2", 215, 215, 215)
        SetColor("NoneGradient3", 252, 252, 252)
        SetColor("NoneGradient4", 242, 242, 242)
        SetColor("Glow", 50, Color.White)
        SetColor("TextShade", Color.White)
        SetColor("Text", 80, 80, 80)
        SetColor("Border1", Color.White)
        SetColor("Border2", 180, 180, 180)
    End Sub

    Private C1, C2, C3, C4, C5, C6 As Color
    Private B1, B2, B3 As SolidBrush
    Private P1, P2 As Pen

    Protected Overrides Sub ColorHook()
        C1 = GetColor("DownGradient1")
        C2 = GetColor("DownGradient2")
        C3 = GetColor("NoneGradient1")
        C4 = GetColor("NoneGradient2")
        C5 = GetColor("NoneGradient3")
        C6 = GetColor("NoneGradient4")

        B1 = New SolidBrush(GetColor("Glow"))
        B2 = New SolidBrush(GetColor("TextShade"))
        B3 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("Border1"))
        P2 = New Pen(GetColor("Border2"))
    End Sub

    Protected Overrides Sub PaintHook()

        If State = MouseState.Down Then
            DrawGradient(C1, C2, ClientRectangle, 90)
        Else
            DrawGradient(C3, C4, ClientRectangle, 90)
            DrawGradient(C5, C6, 0, 0, Width, Height \ 2, 90.0F)
        End If

        If State = MouseState.Over Then
            G.FillRectangle(B1, ClientRectangle)
        End If

        If State = MouseState.Down Then
            DrawText(B2, HorizontalAlignment.Center, 2, 2)
            DrawText(B3, HorizontalAlignment.Center, 1, 1)
        Else
            DrawText(B2, HorizontalAlignment.Center, 1, 1)
            DrawText(B3, HorizontalAlignment.Center, 0, 0)
        End If

        DrawBorders(P1, 1)
        DrawBorders(P2)

        DrawCorners(BackColor)
    End Sub
End Class