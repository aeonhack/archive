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
Class FusionTheme
    Inherits ThemeContainer152

    Private Path As GraphicsPath
    Private Blend As ColorBlend

    Sub New()
        MoveHeight = 34
        BackColor = Color.White
        TransparencyKey = Color.Fuchsia

        SetColor("Sides", 47, 47, 50)
        SetColor("Gradient1", 52, 52, 55)
        SetColor("Gradient2", 47, 47, 50)
        SetColor("Text", Color.White)
        SetColor("Back", Color.White)
        SetColor("Border1", Color.Black)
        SetColor("Border2", 60, 60, 63)
        SetColor("Border3", 60, 60, 63)
        SetColor("Border4", Color.Black)
        SetColor("Blend1", Color.Transparent)
        SetColor("Blend2", 60, 60, 63)

        Path = New GraphicsPath

        Blend = New ColorBlend
        Blend.Positions = New Single() {0.0F, 0.5F, 1.0F}
    End Sub

    Private P1, P2, P3, P4, P5 As Pen
    Private C1, C2, C3 As Color
    Private B1, B2 As SolidBrush

    Protected Overrides Sub ColorHook()
        P1 = New Pen(TransparencyKey, 3)
        P2 = New Pen(GetColor("Border1"))
        P3 = New Pen(GetColor("Border2"))
        P4 = New Pen(GetColor("Border3"))
        P5 = New Pen(GetColor("Border4"))

        C1 = GetColor("Sides")
        C2 = GetColor("Gradient1")
        C3 = GetColor("Gradient2")

        B1 = New SolidBrush(GetColor("Text"))
        B2 = New SolidBrush(GetColor("Back"))

        Blend.Colors = New Color() {GetColor("Blend1"), GetColor("Blend2"), GetColor("Blend1")}

        BackColor = B2.Color
    End Sub

    Private RT1 As Rectangle

    Protected Overrides Sub PaintHook()
        G.DrawRectangle(P1, ClientRectangle)
        G.SetClip(Path)

        G.Clear(C1)

        DrawGradient(C2, C3, 0, 0, 16, Height)
        DrawGradient(C2, C3, Width - 16, 0, 16, Height)

        DrawText(B1, HorizontalAlignment.Left, 12, 0)

        RT1 = New Rectangle(12, 34, Width - 24, Height - 34 - 12)

        G.FillRectangle(B2, RT1)
        DrawBorders(P2, RT1, 1)
        DrawBorders(P3, RT1)

        DrawBorders(P4, 1)
        DrawGradient(Blend, 1, 1, Width - 2, 2, 0.0F)

        G.ResetClip()
        G.DrawPath(P5, Path)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Path.Reset()
        Path.AddLines(New Point() { _
              New Point(2, 0), _
              New Point(Width - 3, 0), _
              New Point(Width - 1, 2), _
              New Point(Width - 1, Height - 3), _
              New Point(Width - 3, Height - 1), _
              New Point(2, Height - 1), _
              New Point(0, Height - 3), _
              New Point(0, 2), _
              New Point(2, 0)})

        MyBase.OnResize(e)
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
Class FusionButton
    Inherits ThemeControl152

    Sub New()
        SetColor("DownGradient1", 255, 127, 1)
        SetColor("DownGradient2", 255, 175, 12)
        SetColor("NoneGradient1", 255, 175, 12)
        SetColor("NoneGradient2", 255, 127, 1)
        SetColor("TextShade", 30, Color.Black)
        SetColor("Text", Color.White)
        SetColor("Border1", 255, 197, 19)
        SetColor("Border2", 254, 133, 0)
    End Sub

    Private C1, C2, C3, C4 As Color
    Private B1, B2 As SolidBrush
    Private P1, P2 As Pen

    Protected Overrides Sub ColorHook()
        C1 = GetColor("DownGradient1")
        C2 = GetColor("DownGradient2")
        C3 = GetColor("NoneGradient1")
        C4 = GetColor("NoneGradient2")

        B1 = New SolidBrush(GetColor("TextShade"))
        B2 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("Border1"))
        P2 = New Pen(GetColor("Border2"))
    End Sub

    Protected Overrides Sub PaintHook()
        If State = MouseState.Down Then
            DrawGradient(C1, C2, ClientRectangle, 90.0F)
        Else
            DrawGradient(C3, C4, ClientRectangle, 90.0F)
        End If

        DrawText(B1, HorizontalAlignment.Center, 1, 1)
        DrawText(B2, HorizontalAlignment.Center, 0, 0)

        DrawBorders(P1, 1)
        DrawBorders(P2)

        DrawCorners(BackColor)
    End Sub

End Class