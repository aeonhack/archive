Class NSSeperator
    Inherits Control

    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)

        Height = 10
    End Sub

    Private G As Graphics

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        G = e.Graphics
        G.Clear(BackColor)

        G.DrawLine(SystemPens.ButtonShadow, 0, 5, Width, 5)
        G.DrawLine(SystemPens.ButtonHighlight, 0, 6, Width, 6)
    End Sub

End Class