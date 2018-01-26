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
Class StudioTheme
    Inherits ThemeContainer152

    Private Path As GraphicsPath

    Sub New()
        MoveHeight = 30
        BackColor = Color.FromArgb(20, 40, 70)
        TransparencyKey = Color.Fuchsia

        SetColor("Sides", 50, 70, 100)
        SetColor("Gradient1", 65, 85, 115)
        SetColor("Gradient2", 50, 70, 100)
        SetColor("Hatch1", 20, 40, 70)
        SetColor("Hatch2", 40, 60, 90)
        SetColor("Shade1", 15, Color.Black)
        SetColor("Shade2", Color.Transparent)
        SetColor("Border1", 12, 32, 62)
        SetColor("Border2", 20, Color.Black)
        SetColor("Border3", 30, Color.White)
        SetColor("Border4", Color.Black)
        SetColor("Text", Color.White)

        Path = New GraphicsPath
    End Sub

    Private C1, C2, C3, C4, C5 As Color
    Private P1, P2, P3, P4, P5 As Pen
    Private B1 As HatchBrush
    Private B2 As SolidBrush

    Protected Overrides Sub ColorHook()
        P1 = New Pen(TransparencyKey, 3)
        P2 = New Pen(GetColor("Border1"))
        P3 = New Pen(GetColor("Border2"), 2)
        P4 = New Pen(GetColor("Border3"))
        P5 = New Pen(GetColor("Border4"))

        C1 = GetColor("Sides")
        C2 = GetColor("Gradient1")
        C3 = GetColor("Gradient2")
        C4 = GetColor("Shade1")
        C5 = GetColor("Shade2")

        B1 = New HatchBrush(HatchStyle.LightDownwardDiagonal, GetColor("Hatch1"), GetColor("Hatch2"))
        B2 = New SolidBrush(GetColor("Text"))

        BackColor = GetColor("Hatch2")
    End Sub

    Private RT1 As Rectangle

    Protected Overrides Sub PaintHook()
        G.DrawRectangle(P1, ClientRectangle)
        G.SetClip(Path)

        G.Clear(C1)
        DrawGradient(C2, C3, 0, 0, Width, 30)

        RT1 = New Rectangle(12, 30, Width - 24, Height - 12 - 30)
        G.FillRectangle(B1, RT1)

        DrawGradient(C4, C5, 12, 30, Width - 24, 30)

        DrawBorders(P2, RT1)
        DrawBorders(P3, 14, 32, Width - 26, Height - 12 - 32)

        DrawText(B2, HorizontalAlignment.Left, 12, 0)

        DrawBorders(P4, 1)

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
Class StudioButton
    Inherits ThemeControl152

    Sub New()
        Transparent = True
        BackColor = Color.Transparent

        SetColor("DownGradient1", 45, 65, 95)
        SetColor("DownGradient2", 65, 85, 115)
        SetColor("NoneGradient1", 65, 85, 115)
        SetColor("NoneGradient2", 45, 65, 95)
        SetColor("Shine1", 30, Color.White)
        SetColor("Shine2A", 30, Color.White)
        SetColor("Shine2B", Color.Transparent)
        SetColor("Shine3", 20, Color.White)
        SetColor("TextShade", 50, Color.Black)
        SetColor("Text", Color.White)
        SetColor("Glow", 10, Color.White)
        SetColor("Border", 20, 40, 70)
        SetColor("Corners", 20, 40, 70)
    End Sub

    Private C1, C2, C3, C4, C5, C6, C7 As Color
    Private P1, P2, P3 As Pen
    Private B1, B2, B3 As SolidBrush

    Protected Overrides Sub ColorHook()
        C1 = GetColor("DownGradient1")
        C2 = GetColor("DownGradient2")
        C3 = GetColor("NoneGradient1")
        C4 = GetColor("NoneGradient2")
        C5 = GetColor("Shine2A")
        C6 = GetColor("Shine2B")
        C7 = GetColor("Corners")

        P1 = New Pen(GetColor("Shine1"))
        P2 = New Pen(GetColor("Shine3"))
        P3 = New Pen(GetColor("Border"))

        B1 = New SolidBrush(GetColor("TextShade"))
        B2 = New SolidBrush(GetColor("Text"))
        B3 = New SolidBrush(GetColor("Glow"))
    End Sub

    Protected Overrides Sub PaintHook()

        If State = MouseState.Down Then
            DrawGradient(C1, C2, ClientRectangle, 90.0F)
        Else
            DrawGradient(C3, C4, ClientRectangle, 90.0F)
        End If

        G.DrawLine(P1, 1, 1, Width, 1)
        DrawGradient(C5, C6, 1, 1, 1, Height)
        DrawGradient(C5, C6, Width - 2, 1, 1, Height)
        G.DrawLine(P2, 1, Height - 2, Width, Height - 2)

        If State = MouseState.Down Then
            DrawText(B1, HorizontalAlignment.Center, 2, 2)
            DrawText(B2, HorizontalAlignment.Center, 1, 1)
        Else
            DrawText(B1, HorizontalAlignment.Center, 1, 1)
            DrawText(B2, HorizontalAlignment.Center, 0, 0)
        End If

        If State = MouseState.Over Then
            G.FillRectangle(B3, ClientRectangle)
        End If

        DrawBorders(P3)
        DrawCorners(C7, 1, 1, Width - 2, Height - 2)

        DrawCorners(BackColor)
    End Sub
End Class