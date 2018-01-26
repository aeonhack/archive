
'Theme Base (1.5.0): http://pastebin.com/9v4AFzh8


'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 8/13/2011
'Changed: 8/13/2011
'Version: 1.0.0
'Theme Base: 1.5.0
'------------------
Class StormTheme
    Inherits ThemeContainer

    Private _TopHeight As Integer = 40
    Property TopHeight() As Integer
        Get
            Return _TopHeight
        End Get
        Set(ByVal value As Integer)
            _TopHeight = value
            Invalidate()
        End Set
    End Property

    Private _BottomHeight As Integer = 40
    Property BottomHeight() As Integer
        Get
            Return _BottomHeight
        End Get
        Set(ByVal value As Integer)
            _BottomHeight = value
            Invalidate()
        End Set
    End Property

    Sub New()
        Sizable = False
        Movable = False

        BorderStyle = FormBorderStyle.Sizable
        BackColor = Color.FromArgb(90, 90, 110)

        SetColor("BackColor", 90, 90, 110)
        SetColor("Gradient1", 175, 175, 190)
        SetColor("Gradient2", 140, 140, 155)
        SetColor("TopBorder", 70, 70, 90)
        SetColor("TopLight", 105, 105, 120)
        SetColor("Hatch1", 35, 35, 45)
        SetColor("Hatch2", 40, 40, 50)
        SetColor("BottomBorder", 25, 25, 30)
    End Sub

    Protected Overrides Sub ColorHook()
        C1 = GetColor("BackColor")
        C2 = GetColor("Gradient1")
        C3 = GetColor("Gradient2")

        P1 = New Pen(GetColor("TopBorder"))
        P2 = New Pen(GetColor("TopLight"))
        P3 = New Pen(GetColor("BottomBorder"))

        B1 = New HatchBrush(HatchStyle.DarkUpwardDiagonal, GetColor("Hatch1"), GetColor("Hatch2"))

        BackColor = C1
    End Sub

    Private C1, C2, C3 As Color
    Private P1, P2, P3 As Pen
    Private B1 As HatchBrush

    Protected Overrides Sub PaintHook()
        G.Clear(C1)

        If Not _TopHeight = 0 Then
            DrawGradient(C2, C3, 0, 0, Width, _TopHeight)
            G.DrawLine(P1, 0, _TopHeight - 1, Width, _TopHeight - 1)
            G.DrawLine(P2, 0, _TopHeight, Width, _TopHeight)
        End If

        If Not _BottomHeight = 0 Then
            G.FillRectangle(B1, 0, Height - _BottomHeight, Width, _BottomHeight)
            G.DrawLine(P3, 0, Height - _BottomHeight, Width, Height - _BottomHeight)
        End If

    End Sub

End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 8/13/2011
'Changed: 8/13/2011
'Version: 1.0.0
'Theme Base: 1.5.0
'------------------
Class StormButtonA
    Inherits ThemeControl

    Private Blend As ColorBlend

    Sub New()

        Blend = New ColorBlend
        Blend.Positions = {0, 0.5, 1}

        SetColor("Blend1", 150, 150, 165)
        SetColor("Blend2", 120, 120, 140)
        SetColor("Blend3", 150, 150, 165)
        SetColor("Shine", 20, Color.White)
        SetColor("Glow", 30, Color.White)
        SetColor("Text", Color.White)
        SetColor("Light", 150, 150, 165)
        SetColor("Border", 70, 70, 90)

    End Sub

    Protected Overrides Sub ColorHook()
        B1 = New SolidBrush(GetColor("Shine"))
        B2 = New SolidBrush(GetColor("Glow"))
        B3 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("Light"))
        P2 = New Pen(GetColor("Border"))

        Blend.Colors = {GetColor("Blend1"), GetColor("Blend2"), GetColor("Blend3")}
    End Sub

    Private B1, B2, B3 As SolidBrush
    Private P1, P2 As Pen

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        DrawGradient(Blend, ClientRectangle, 90)

        G.FillRectangle(B1, 0, 0, Width, Height \ 2)

        If State = MouseState.Over Then
            G.FillRectangle(B2, ClientRectangle)
        End If

        DrawText(B3, HorizontalAlignment.Center, 0, 0)

        DrawBorders(P1, 1)
        DrawBorders(P2)
    End Sub
End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 8/13/2011
'Changed: 8/13/2011
'Version: 1.0.0
'Theme Base: 1.5.0
'------------------
Class StormButtonB
    Inherits ThemeControl

    Sub New()
        SetColor("DownGradient1", 170, 180, 0)
        SetColor("DownGradient2", 220, 230, 0)
        SetColor("UpGradient1", 220, 230, 0)
        SetColor("UpGradient2", 170, 180, 0)
        SetColor("Text", Color.Black)
        SetColor("Light", 65, Color.White)
        SetColor("Border", 70, 70, 90)
        SetColor("OuterLight", 105, 105, 120)
        SetColor("Corners", 125, 130, 70)
    End Sub

    Protected Overrides Sub ColorHook()
        C1 = GetColor("DownGradient1")
        C2 = GetColor("DownGradient2")
        C3 = GetColor("UpGradient1")
        C4 = GetColor("UpGradient2")
        C5 = GetColor("Corners")

        P1 = New Pen(GetColor("Light"))
        P2 = New Pen(GetColor("Border"))
        P3 = New Pen(GetColor("OuterLight"))

        B1 = New SolidBrush(GetColor("Text"))
    End Sub

    Private C1, C2, C3, C4, C5 As Color
    Private P1, P2, P3 As Pen
    Private B1 As SolidBrush

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        If State = MouseState.Down Then
            DrawGradient(C1, C2, 0, 0, Width, Height - 1, 90)
        Else
            DrawGradient(C3, C4, 0, 0, Width, Height - 1, 90)
        End If

        DrawText(B1, HorizontalAlignment.Center, 0, 0)

        DrawBorders(P1, 1, 1, Width - 2, Height - 3)
        DrawBorders(P2, 0, 0, Width, Height - 1)

        If NoRounding Then
            G.DrawLine(P3, 0, Height - 1, Width, Height - 1)
        Else
            G.DrawLine(P3, 2, Height - 1, Width - 3, Height - 1)
        End If

        DrawCorners(C5, 1, 1, Width - 2, Height - 3)
        DrawCorners(BackColor, 0, 0, Width, Height - 1)
    End Sub
End Class