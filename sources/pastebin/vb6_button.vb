'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 9/24/2011
'Changed: 9/24/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------
Class VB6Button
    Inherits ThemeControl153

    Sub New()
        SetColor("Back", 240, 240, 240)
        SetColor("Border1", Color.White)
        SetColor("Border2", 227, 227, 227)
        SetColor("Border3", 160, 160, 160)
        SetColor("Border4", 105, 105, 105)
        SetColor("Text", Color.Black)
        SetColor("Focus", Color.Black)
    End Sub

    Private C1 As Color
    Private P1, P2, P3, P4, P5 As Pen
    Private B1 As SolidBrush

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Back")

        B1 = GetBrush("Text")

        P1 = GetPen("Border1")
        P2 = GetPen("Border2")
        P3 = GetPen("Border3")
        P4 = GetPen("Border4")
        P5 = GetPen("Focus")

        P5.DashStyle = DashStyle.Dot
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(C1)
        G.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit

        If State = MouseState.Down Then
            DrawBorders(P3, 1)
            DrawBorders(P4)

            DrawText(B1, HorizontalAlignment.Center, 1, 1)
        Else
            DrawBorders(P1)
            DrawBorders(P2, 1)

            G.DrawLine(P4, 0, Height - 1, Width, Height - 1)
            G.DrawLine(P4, Width - 1, 0, Width - 1, Height)

            G.DrawLine(P3, 1, Height - 2, Width - 2, Height - 2)
            G.DrawLine(P3, Width - 2, 1, Width - 2, Height - 2)

            DrawText(B1, HorizontalAlignment.Center, 0, 0)
        End If

        If Focused Then
            DrawBorders(P5, 4)
        End If

    End Sub

    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        Focus()
        MyBase.OnClick(e)
    End Sub

    Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
        Invalidate()
        MyBase.OnGotFocus(e)
    End Sub

    Protected Overrides Sub OnLostFocus(ByVal e As EventArgs)
        Invalidate()
        MyBase.OnLostFocus(e)
    End Sub

End Class
