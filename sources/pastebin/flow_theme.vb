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
Class FlowTheme
    Inherits ThemeContainer152

    Sub New()
        MoveHeight = 24
        BackColor = Color.FromArgb(35, 35, 35)
        TransparencyKey = Color.Fuchsia

        SetColor("Sides", 40, 40, 40)
        SetColor("Gradient", 18, 18, 18)
        SetColor("Text", 0, 132, 255)
        SetColor("Border1", 40, 40, 40)
        SetColor("Border2", 22, 22, 22)
        SetColor("Border3", 65, 65, 65)
        SetColor("Border4", Color.Black)
        SetColor("Hatch1", 39, 39, 39)
        SetColor("Hatch2", 35, 35, 35)
        SetColor("Hatch3", 29, 29, 29)
        SetColor("Hatch4", 26, 26, 26)
        SetColor("Shade1", 50, 7, 7, 7)
        SetColor("Shade2", Color.Transparent)
    End Sub

    Private C1, C2 As Color
    Private B1 As SolidBrush
    Private P1, P2, P3, P4 As Pen

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Sides")
        C2 = GetColor("Gradient")

        B1 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("Border1"))
        P2 = New Pen(GetColor("Border2"))
        P3 = New Pen(GetColor("Border3"))
        P4 = New Pen(GetColor("Border4"))

        CreateTile()
        CreateShade()

        BackColor = GetColor("Hatch2")
    End Sub

    Private RT1 As Rectangle

    Protected Overrides Sub PaintHook()
        G.Clear(C1)

        DrawGradient(C2, C1, 0, 0, Width, 24)
        DrawText(B1, HorizontalAlignment.Left, 8, 0)

        RT1 = New Rectangle(8, 24, Width - 16, Height - 32)
        G.FillRectangle(Tile, RT1)

        For I As Integer = 0 To Shade.Length - 1
            DrawBorders(Shade(I), RT1, I)
        Next

        RT1 = New Rectangle(8, 24, Width - 16, Height - 32)
        DrawBorders(P1, RT1, 1)
        DrawBorders(P2, RT1)
        DrawCorners(C1, RT1)

        DrawBorders(P3, 1)
        DrawBorders(P4)

        DrawCorners(TransparencyKey)
    End Sub


    Private Tile As TextureBrush
    Private TileData As Byte() = Convert.FromBase64String("AgIBAQEBAwMBAQEBAAABAQEBAQEBAgIBAQEBAwMBAQEBAAAB")
    Private Sub CreateTile()
        Dim TileImage As New Bitmap(6, 6)
        Dim TileColors As Color() = New Color() {GetColor("Hatch1"), GetColor("Hatch2"), GetColor("Hatch3"), GetColor("Hatch4")}

        For I As Integer = 0 To 35
            TileImage.SetPixel(I Mod 6, I \ 6, TileColors(TileData(I)))
        Next

        Tile = New TextureBrush(TileImage)
        TileImage.Dispose()
    End Sub

    Private Shade As Pen()
    Private Sub CreateShade()
        If Shade IsNot Nothing Then
            For I As Integer = 0 To Shade.Length - 1
                Shade(I).Dispose()
            Next
        End If

        Dim ShadeImage As New Bitmap(1, 20)
        Dim ShadeGraphics As Graphics = Graphics.FromImage(ShadeImage)

        Dim ShadeBounds As New Rectangle(0, 0, 1, 20)
        Dim Gradient As New LinearGradientBrush(ShadeBounds, GetColor("Shade1"), GetColor("Shade2"), 90.0F)

        Shade = New Pen(19) {}
        ShadeGraphics.FillRectangle(Gradient, ShadeBounds)

        For I As Integer = 0 To Shade.Length - 1
            Shade(I) = New Pen(ShadeImage.GetPixel(0, I))
        Next

        Gradient.Dispose()
        ShadeGraphics.Dispose()
        ShadeImage.Dispose()
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
Class FlowButton
    Inherits ThemeControl152

    Sub New()
        SetColor("DownGradient1", 24, 24, 24)
        SetColor("DownGradient2", 38, 38, 38)
        SetColor("NoneGradient1", 38, 38, 38)
        SetColor("NoneGradient2", 24, 24, 24)
        SetColor("Text", 0, 132, 255)
        SetColor("Border1", 22, 22, 22)
        SetColor("Border2A", 60, 60, 60)
        SetColor("Border2B", 36, 36, 36)
    End Sub

    Private C1, C2, C3, C4, C5, C6 As Color
    Private B1 As SolidBrush
    Private B2 As LinearGradientBrush
    Private P1, P2 As Pen

    Protected Overrides Sub ColorHook()
        C1 = GetColor("DownGradient1")
        C2 = GetColor("DownGradient2")
        C3 = GetColor("NoneGradient1")
        C4 = GetColor("NoneGradient2")
        C5 = GetColor("Border2A")
        C6 = GetColor("Border2B")

        B1 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("Border1"))
    End Sub

    Protected Overrides Sub PaintHook()
        If State = MouseState.Down Then
            DrawGradient(C1, C2, ClientRectangle, 90.0F)
        Else
            DrawGradient(C3, C4, ClientRectangle, 90.0F)
        End If

        DrawText(B1, HorizontalAlignment.Center, 0, 0)

        B2 = New LinearGradientBrush(ClientRectangle, C5, C6, 90.0F)
        P2 = New Pen(B2)

        DrawBorders(P1)
        DrawBorders(P2, 1)
    End Sub
End Class