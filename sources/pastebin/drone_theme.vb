'THIS IS NOT THE FINAL VERSION.

Imports System, System.Collections
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 9/2/2011
'Changed: 9/2/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------
Class DroneTheme
    Inherits ThemeContainer153

    Sub New()
        Header = 24
        TransparencyKey = Color.Fuchsia
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(24, 24, 24))

        DrawGradient(Color.FromArgb(0, 55, 90), Color.FromArgb(0, 70, 128), 11, 8, Width - 22, 17)
        G.FillRectangle(New SolidBrush(Color.FromArgb(0, 55, 90)), 11, 3, Width - 22, 5)

        Dim P As New Pen(Color.FromArgb(13, Color.White))
        G.DrawLine(P, 10, 1, 10, Height)
        G.DrawLine(P, Width - 11, 1, Width - 11, Height)
        G.DrawLine(P, 11, Height - 11, Width - 12, Height - 11)
        G.DrawLine(P, 11, 29, Width - 12, 29)
        G.DrawLine(P, 11, 25, Width - 12, 25)

        G.FillRectangle(New SolidBrush(Color.FromArgb(13, Color.White)), 0, 2, Width, 6)
        G.FillRectangle(New SolidBrush(Color.FromArgb(13, Color.White)), 0, Height - 6, Width, 4)

        G.FillRectangle(New SolidBrush(Color.FromArgb(24, 24, 24)), 11, Height - 6, Width - 22, 4)

        Dim T As New HatchBrush(HatchStyle.Trellis, Color.FromArgb(24, 24, 24), Color.FromArgb(8, 8, 8))
        G.FillRectangle(T, 11, 30, Width - 22, Height - 41)

        DrawText(Brushes.White, HorizontalAlignment.Left, 15, 2)

        DrawBorders(New Pen(Color.FromArgb(58, 58, 58)), 1)
        DrawBorders(Pens.Black)

        P = New Pen(Color.FromArgb(25, Color.White))
        G.DrawLine(P, 11, 3, Width - 12, 3)
        G.DrawLine(P, 12, 2, 12, 7)
        G.DrawLine(P, Width - 13, 2, Width - 13, 7)

        G.DrawLine(Pens.Black, 11, 0, 11, Height)
        G.DrawLine(Pens.Black, Width - 12, 0, Width - 12, Height)

        G.DrawRectangle(Pens.Black, 11, 2, Width - 23, 22)
        G.DrawLine(Pens.Black, 11, Height - 12, Width - 12, Height - 12)
        G.DrawLine(Pens.Black, 11, 30, Width - 12, 30)

        DrawCorners(Color.Fuchsia)
    End Sub

End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 9/2/2011
'Changed: 9/2/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------
Class DroneButton
    Inherits ThemeControl153

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        DrawBorders(New Pen(Color.FromArgb(32, 32, 32)), 1)
        G.FillRectangle(New SolidBrush(Color.FromArgb(62, 62, 62)), 0, 0, Width, 8)
        DrawBorders(Pens.Black, 2)
        DrawBorders(Pens.Black)

        If State = MouseState.Over Then
            G.FillRectangle(New SolidBrush(Color.FromArgb(0, 55, 90)), 3, 3, Width - 6, Height - 6)
            DrawBorders(New Pen(Color.FromArgb(0, 66, 108)), 3)
        ElseIf State = MouseState.Down Then
            G.FillRectangle(New SolidBrush(Color.FromArgb(0, 44, 72)), 3, 3, Width - 6, Height - 6)
            DrawBorders(New Pen(Color.FromArgb(0, 55, 90)), 3)
        Else
            G.FillRectangle(New SolidBrush(Color.FromArgb(24, 24, 24)), 3, 3, Width - 6, Height - 6)
            DrawBorders(New Pen(Color.FromArgb(38, 38, 38)), 3)
        End If

        G.FillRectangle(New SolidBrush(Color.FromArgb(13, Color.White)), 3, 3, Width - 6, 8)

        If State = MouseState.Down Then
            DrawText(Brushes.White, HorizontalAlignment.Center, 1, 1)
        Else
            DrawText(Brushes.White, HorizontalAlignment.Center, 0, 0)
        End If

    End Sub

End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 9/24/2011
'Changed: 9/24/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------
Class DroneProgressBar
    Inherits ThemeControl153

    Private Blend As ColorBlend


    Sub New()
        Blend = New ColorBlend
        Blend.Colors = New Color() {Color.FromArgb(0, 55, 90), Color.FromArgb(0, 66, 108), Color.FromArgb(0, 66, 108), Color.FromArgb(0, 55, 90)}
        Blend.Positions = New Single() {0.0F, 0.4F, 0.6F, 1.0F}
    End Sub

    Protected Overrides Sub OnCreation()
        If Not DesignMode Then
            Dim T As New Threading.Thread(AddressOf MoveGlow)
            T.IsBackground = True
            T.Start()
        End If
    End Sub

    Private GlowPosition As Single = -1.0F
    Private Sub MoveGlow()
        While True
            GlowPosition += 0.01F
            If GlowPosition >= 1.0F Then GlowPosition = -1.0F
            Invalidate()
            Threading.Thread.Sleep(25)
        End While
    End Sub

    Private _Value As Integer
    Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            If value > _Maximum Then value = _Maximum
            If value < 0 Then value = 0

            _Value = value
            Invalidate()
        End Set
    End Property

    Private _Maximum As Integer = 100
    Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then value = 1
            If _Value > value Then _Value = value

            _Maximum = value
            Invalidate()
        End Set
    End Property

    Sub Increment(ByVal amount As Integer)
        Value += amount
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Private Progress As Integer
    Protected Overrides Sub PaintHook()
        DrawBorders(New Pen(Color.FromArgb(32, 32, 32)), 1)
        G.FillRectangle(New SolidBrush(Color.FromArgb(50, 50, 50)), 0, 0, Width, 8)

        DrawGradient(Color.FromArgb(8, 8, 8), Color.FromArgb(23, 23, 23), 2, 2, Width - 4, Height - 4, 90.0F)

        Progress = CInt((_Value / _Maximum) * Width)

        If Not Progress = 0 Then
            G.SetClip(New Rectangle(3, 3, Progress - 6, Height - 6))
            G.FillRectangle(New SolidBrush(Color.FromArgb(0, 55, 90)), 0, 0, Progress, Height)

            DrawGradient(Blend, CInt(GlowPosition * Progress), 0, Progress, Height, 0.0F)
            DrawBorders(New Pen(Color.FromArgb(15, Color.White)), 3, 3, Progress - 6, Height - 6)

            G.FillRectangle(New SolidBrush(Color.FromArgb(13, Color.White)), 3, 3, Width - 6, 5)

            G.ResetClip()
        End If

        DrawBorders(Pens.Black, 2)
        DrawBorders(Pens.Black)
    End Sub
End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 10/25/2011
'Changed: 10/25/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------
Class DroneGroupBox
    Inherits ThemeContainer153

    Sub New()
        ControlMode = True
        Header = 26
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(24, 24, 24))

        DrawGradient(Color.FromArgb(0, 55, 90), Color.FromArgb(0, 70, 128), 5, 5, Width - 10, 26)
        G.DrawLine(New Pen(Color.FromArgb(20, Color.White)), 7, 7, Width - 8, 7)

        DrawBorders(Pens.Black, 5, 5, Width - 10, 26, 1)
        DrawBorders(New Pen(Color.FromArgb(36, 36, 36)), 5, 5, Width - 10, 26)

        '???
        DrawBorders(New Pen(Color.FromArgb(8, 8, 8)), 5, 34, Width - 10, Height - 39, 1)
        DrawBorders(New Pen(Color.FromArgb(36, 36, 36)), 5, 34, Width - 10, Height - 39)

        DrawBorders(New Pen(Color.FromArgb(36, 36, 36)), 1)
        DrawBorders(Pens.Black)

        G.DrawLine(New Pen(Color.FromArgb(48, 48, 48)), 1, 1, Width - 2, 1)

        DrawText(Brushes.White, HorizontalAlignment.Left, 9, 5)
    End Sub
End Class

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 10/25/2011
'Changed: 10/25/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------
Class DroneSeperator
    Inherits ThemeControl153

    Private _Orientation As Orientation
    Property Orientation() As Orientation
        Get
            Return _Orientation
        End Get
        Set(ByVal value As Orientation)
            _Orientation = value

            If value = Windows.Forms.Orientation.Vertical Then
                LockHeight = 0
                LockWidth = 14
            Else
                LockHeight = 14
                LockWidth = 0
            End If

            Invalidate()
        End Set
    End Property

    Sub New()
        Transparent = True
        BackColor = Color.Transparent

        LockHeight = 14
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        Dim BL1, BL2 As New ColorBlend
        BL1.Positions = New Single() {0.0F, 0.15F, 0.85F, 1.0F}
        BL2.Positions = New Single() {0.0F, 0.15F, 0.5F, 0.85F, 1.0F}

        BL1.Colors = New Color() {Color.Transparent, Color.Black, Color.Black, Color.Transparent}
        BL2.Colors = New Color() {Color.Transparent, Color.FromArgb(35, 35, 35), Color.FromArgb(45, 45, 45), Color.FromArgb(35, 35, 35), Color.Transparent}

        If _Orientation = Windows.Forms.Orientation.Vertical Then
            DrawGradient(BL1, 6, 0, 1, Height)
            DrawGradient(BL2, 7, 0, 1, Height)
        Else
            DrawGradient(BL1, 0, 6, Width, 1, 0.0F)
            DrawGradient(BL2, 0, 7, Width, 1, 0.0F)
        End If

    End Sub

End Class
