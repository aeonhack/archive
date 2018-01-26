Imports System.ComponentModel
Imports System.Drawing
<ToolboxBitmap(GetType(AreonButton), "Button")> Public Class AreonButton : Inherits Windows.Forms.Button
    Private _OverImage As Image = My.Resources.Up
    Private _UpImage As Image = My.Resources.Up
    Private _DownImage As Image = My.Resources.Down
    <Description("Mouse over image.")> <Category("StateAppearance")> Property OverImage() As Image
        Get
            Return _OverImage
        End Get
        Set(ByVal Value As Image)
            _OverImage = Value
        End Set
    End Property
    <Description("Mouse up image.")> <Category("StateAppearance")> Property UpImage() As Image
        Get
            Return _UpImage
        End Get
        Set(ByVal Value As Image)
            _UpImage = Value
            If BackgroundImage Is Nothing Then BackgroundImage = Value
        End Set
    End Property
    <Description("Mouse down image.")> <Category("StateAppearance")> Property DownImage() As Image
        Get
            Return _DownImage
        End Get
        Set(ByVal Value As Image)
            _DownImage = Value
        End Set
    End Property
    Private Sub AB_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave
        BackgroundImage = UpImage
    End Sub
    Private Sub AB_MouseDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseDown
        BackgroundImage = DownImage
    End Sub
    Private Sub AB_MouseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseUp
        BackgroundImage = OverImage
    End Sub
    Private Sub AB_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        BackgroundImage = OverImage
    End Sub
    Public Sub New()
        BackColor = Color.Transparent
        FlatStyle = Windows.Forms.FlatStyle.Flat
        FlatAppearance.BorderColor = Color.Silver
        FlatAppearance.MouseDownBackColor = Color.Transparent
        FlatAppearance.MouseOverBackColor = Color.Transparent
        BackgroundImageLayout = Windows.Forms.ImageLayout.Stretch
        BackgroundImage = My.Resources.Up
        Size = New Point(80, 26)
        SetStyle(Windows.Forms.ControlStyles.Selectable, False)
    End Sub
End Class
<ToolboxBitmap(GetType(AreonTextBox), "TextBox")> Public Class AreonTextBox : Inherits Windows.Forms.TextBox
    Private _Symbols As Boolean
    Private _Numbers As Boolean
    Private _Letters As Boolean
    Private _Punctuation As Boolean
    Private _Forbidden As String
    <Description("Disallow symbols from being input.")> <Category("Restrictions")> Property DisallowSymbols() As Boolean
        Get
            Return _Symbols
        End Get
        Set(ByVal Value As Boolean)
            _Symbols = Value
        End Set
    End Property
    <Description("Disallow numbers from being input.")> <Category("Restrictions")> Property DisallowNumbers() As Boolean
        Get
            Return _Numbers
        End Get
        Set(ByVal Value As Boolean)
            _Numbers = Value
        End Set
    End Property
    <Description("Disallow letters from being input.")> <Category("Restrictions")> Property DisallowLetters() As Boolean
        Get
            Return _Letters
        End Get
        Set(ByVal Value As Boolean)
            _Letters = Value
        End Set
    End Property
    <Description("Disallow specified characters from being input.")> <Category("Restrictions")> Property DisallowInput() As String
        Get
            Return _Forbidden
        End Get
        Set(ByVal Value As String)
            _Forbidden = Value
        End Set
    End Property
    <Description("Disallow punctuation from being input.")> <Category("Restrictions")> Property DisallowPunctuation() As Boolean
        Get
            Return _Punctuation
        End Get
        Set(ByVal Value As Boolean)
            _Punctuation = Value
        End Set
    End Property
    Private Sub AreonTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If _Symbols And Char.IsSymbol(e.KeyChar) Then e.Handled = True
        If _Numbers And Char.IsNumber(e.KeyChar) Then e.Handled = True
        If _Letters And Char.IsLetter(e.KeyChar) Then e.Handled = True
        If _Punctuation And Char.IsPunctuation(e.KeyChar) Then e.Handled = True
        If _Forbidden <> Nothing Then If _Forbidden.Contains(e.KeyChar) Then e.Handled = True
    End Sub
End Class
<ToolboxBitmap(GetType(AreonProgressBar), "ProgressBar")> Public Class AreonProgressBar : Inherits Windows.Forms.Control
    Private _PrimaryColor As Color = Color.FromArgb(85, 202, 222)
    Private _SecondaryColor As Color = Color.FromArgb(121, 231, 254)
    Private _BorderColor As Color = Color.Silver
    Private _GradientStyle As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical
    Private _BorderStyle As Windows.Forms.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
    Private _ShowPercent As Boolean = True
    Private _ShowGloss As Boolean = True
    Private _Value As Double
    Private _GlossOpacity As Integer = 90
    Private _MaximumValue As Double = 100
    Private _TextAlign As Alignment = 1
    Private _Text As String
    Private _Orientation As Direction = 1
    Private _SegmentSize As Integer = 0
    Private _SegmentGap As Integer = 1
    Enum Direction
        Vertical = 1 : Horizontal = 0
    End Enum
    Enum GradientOrientation
        Horizontal = 0 : Vertical = 1 : ForwardDiagnal = 2 : BackwardDiagnal = 3 : Highlight = 4
    End Enum
    Enum Alignment
        Left = 0 : Center = 1 : Right = 2 : Trace = 3
    End Enum
    Overrides Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value : Invalidate()
        End Set
    End Property
    <Description("Primary color to be used in gradient.")> <Category("Style")> Property PrimaryColor() As Color
        Get
            Return _PrimaryColor
        End Get
        Set(ByVal Value As Color)
            _PrimaryColor = Value : Invalidate()
        End Set
    End Property
    <Description("Indicates the appearance and behavior of the border.")> <Category("Style")> Property BorderStyle() As Windows.Forms.BorderStyle
        Get
            Return _BorderStyle
        End Get
        Set(ByVal Value As Windows.Forms.BorderStyle)
            _BorderStyle = Value : Invalidate()
        End Set
    End Property
    <Description("Secondary color to be used in gradient.")> <Category("Style")> Property SecondaryColor() As Color
        Get
            Return _SecondaryColor
        End Get
        Set(ByVal Value As Color)
            _SecondaryColor = Value : Invalidate()
        End Set
    End Property
    <Description("Color to use when using the border style FixedSingle.")> <Category("Style")> Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal Value As Color)
            _BorderColor = Value : Invalidate()
        End Set
    End Property
    <Description("Gradient mode to be applied to the progress bar.")> <Category("Style")> Property GradientStyle() As GradientOrientation
        Get
            Return _GradientStyle
        End Get
        Set(ByVal Value As GradientOrientation)
            _GradientStyle = Value : Invalidate()
        End Set
    End Property
    <Description("Toggle the display of current progress.")> <Category("Style")> Property ShowPercent() As Boolean
        Get
            Return _ShowPercent
        End Get
        Set(ByVal Value As Boolean)
            _ShowPercent = Value : Invalidate()
        End Set
    End Property
    <Description("Direction to display progress.")> <Category("Style")> Property Orientation() As Direction
        Get
            Return _Orientation
        End Get
        Set(ByVal Value As Direction)
            _Orientation = Value : Invalidate()
        End Set
    End Property
    <Description("Toggle the gloss effect drawn on the control.")> <Category("Style")> Property ShowGloss() As Boolean
        Get
            Return _ShowGloss
        End Get
        Set(ByVal Value As Boolean)
            _ShowGloss = Value : Invalidate()
        End Set
    End Property
    <Description("Transparency for the gloss displayed on the control.")> <Category("Style")> Property GlossOpacity() As Integer
        Get
            Return _GlossOpacity
        End Get
        Set(ByVal Value As Integer)
            If Value > 255 Then Value = 255 : If Value < 0 Then Value = 0
            _GlossOpacity = Value : Invalidate()
        End Set
    End Property
    <Description("Current value.")> <Category("Stats")> Property Value() As Double
        Get
            Return _Value
        End Get
        Set(ByVal Value As Double)
            If Value > MaximumValue Then Value = MaximumValue
            _Value = CInt(Value) : Invalidate()
        End Set
    End Property
    <Description("Maximum value.")> <Category("Stats")> Property MaximumValue() As Double
        Get
            Return _MaximumValue
        End Get
        Set(ByVal Value As Double)
            _MaximumValue = CInt(Value) : Invalidate()
        End Set
    End Property
    <Description("Width, or height of segments on the progress bar.")> <Category("Style")> Property SegmentSize() As Integer
        Get
            Return _SegmentSize
        End Get
        Set(ByVal Value As Integer)
            _SegmentSize = Value : Invalidate()
        End Set
    End Property
    <Description("Gap size between segments on the progress bar.")> <Category("Style")> Property SegmentGap() As Integer
        Get
            Return _SegmentGap
        End Get
        Set(ByVal Value As Integer)
            If Value < 1 Then Value = 1
            _SegmentGap = Value : Invalidate()
        End Set
    End Property
    <Description("Position to align the text displayed on the control.")> <Category("Style")> Property TextAlign() As Alignment
        Get
            Return _TextAlign
        End Get
        Set(ByVal Value As Alignment)
            _TextAlign = Value : Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Invalidate()
    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As Windows.Forms.PaintEventArgs)
        Dim VWidth As Integer = ClientRectangle.Width * Value / MaximumValue
        Dim VHeight As Integer = ClientRectangle.Height * Value / MaximumValue
        Dim Buffer As New Bitmap(Width, Height)
        Dim GFX As Graphics = Graphics.FromImage(Buffer) : GFX.Clear(BackColor)
        If Not (BackgroundImage Is Nothing) Then DrawBackground(GFX)
        If Orientation = 1 And VWidth >= 1 Or Orientation = 0 And VHeight >= 1 Then
            Dim BarBrush As Drawing2D.LinearGradientBrush
            If GradientStyle < 4 Then
                BarBrush = New Drawing2D.LinearGradientBrush(ClientRectangle, PrimaryColor, SecondaryColor, CType(GradientStyle, Drawing2D.LinearGradientMode))
                If Orientation = 1 Then GFX.FillRectangle(BarBrush, 0, 0, VWidth, Height) Else GFX.FillRectangle(BarBrush, 0, Height - VHeight, Width, VHeight)
            Else
                BarBrush = New Drawing2D.LinearGradientBrush(ClientRectangle, SecondaryColor, PrimaryColor, CType(Orientation, Drawing2D.LinearGradientMode))
                If Orientation = 1 Then GFX.FillRectangle(BarBrush, 0, 0, VWidth, CInt(Height / 2) + 1) Else GFX.FillRectangle(BarBrush, 0, Height - VHeight, CInt(Width / 2) + 1, VHeight)
                BarBrush = New Drawing2D.LinearGradientBrush(ClientRectangle, PrimaryColor, SecondaryColor, CType(Orientation, Drawing2D.LinearGradientMode))
                If Orientation = 1 Then GFX.FillRectangle(BarBrush, 0, Height - CInt(Height / 2), VWidth, CInt(Height / 2)) Else GFX.FillRectangle(BarBrush, Width - CInt(Width / 2), Height - VHeight, CInt(Width / 2), VHeight)
            End If
            BarBrush.Dispose()
        End If
        If _SegmentSize > 0 Then
            Dim Brush As New SolidBrush(BackColor) : Dim X As Integer = _SegmentSize
            While X < VWidth Or X < VHeight
                Dim Area As Rectangle
                If Orientation = 1 Then Area = New Rectangle(X, 0, SegmentGap, Height) Else Area = New Rectangle(0, Height - X, Width, SegmentGap)
                GFX.FillRectangle(Brush, Area) : X += _SegmentSize + SegmentGap
            End While : Brush.Dispose()
        End If
        If ShowGloss Then If Orientation = 1 Then GFX.FillRectangle(New SolidBrush(Color.FromArgb(_GlossOpacity, 255, 255, 255)), 0, 0, Width, CInt(Height / 5)) Else GFX.FillRectangle(New SolidBrush(Color.FromArgb(_GlossOpacity, 255, 255, 255)), 0, 0, CInt(Width / 5), Height)
        If BorderStyle > 0 Then Border(GFX)
        DrawText(GFX, ShowPercent) : e.Graphics.DrawImage(Buffer.Clone, 0, 0) : Buffer.Dispose() : GFX.Dispose()
    End Sub
    Private Sub DrawText(ByVal GFX As Graphics, ByVal DrawPercent As Boolean)
        Dim DrawText As String
        If DrawPercent Then DrawText = Text & Math.Round(100 * (Value / MaximumValue), 0) & "%" Else If Text <> "" Then DrawText = Text Else Exit Sub
        Dim TextBrush As New SolidBrush(ForeColor)
        Dim TextSize As Size = GFX.MeasureString(DrawText, Font).ToSize
        Dim TextLocation As Integer = 5 : If TextAlign = 3 Then TextLocation = (ClientRectangle.Width * Value / MaximumValue) - TextSize.Width - 5
        If TextAlign = 1 Then TextLocation = (Width / 2) - (TextSize.Width / 2)
        If TextAlign = 2 Then TextLocation = Width - TextSize.Width - 5
        GFX.DrawString(DrawText, Font, TextBrush, TextLocation, (Height / 2) - (TextSize.Height / 2)) : TextBrush.Dispose()
    End Sub
    Private Sub Border(ByVal GFX As Graphics)
        Dim Main As New Pen(BorderColor)
        If BorderStyle = 1 Then
            GFX.DrawRectangle(Main, 0, 0, Width - 1, Height - 1)
        Else
            Main = New Pen(BC(False))
            GFX.DrawRectangle(Main, 0, 0, Width - 1, Height - 1)
            Dim Shadow As New Pen(BC(True))
            GFX.DrawLine(Shadow, 2, 1, Width - 2, 1)
            GFX.DrawLine(Shadow, 1, 1, 1, Height - 2)
            Shadow.Dispose()
        End If
        Main.Dispose()
    End Sub
    Private Function BC(ByVal Shadow As Boolean) As Color
        Dim Main As Color = BackColor : Dim R, G, B As Integer : R = CInt(Main.R) : G = CInt(Main.G) : B = CInt(Main.B)
        If R + G + B > 385 Then
            If Shadow Then Return Color.FromArgb(120, Color.Black) Else Return Color.FromArgb(70, Color.Black)
        Else
            If Shadow Then Return Color.FromArgb(120, Color.White) Else Return Color.FromArgb(70, Color.White)
        End If
    End Function
    Private Sub DrawBackground(ByVal GFX As Graphics)
        Select Case BackgroundImageLayout
            Case Windows.Forms.ImageLayout.Center
                GFX.DrawImage(BackgroundImage, CInt((Width / 2) - (BackgroundImage.Width / 2)), CInt((Height / 2) - (BackgroundImage.Height / 2)), BackgroundImage.Width, BackgroundImage.Height)
            Case Windows.Forms.ImageLayout.None
                GFX.DrawImage(BackgroundImage, 0, 0, BackgroundImage.Width, BackgroundImage.Height)
            Case Windows.Forms.ImageLayout.Stretch
                GFX.DrawImage(BackgroundImage, 0, 0, Width, Height)
            Case Windows.Forms.ImageLayout.Tile
                Dim TB As TextureBrush = New TextureBrush(BackgroundImage, Drawing2D.WrapMode.Tile)
                GFX.FillRectangle(TB, ClientRectangle) : TB.Dispose()
            Case Windows.Forms.ImageLayout.Zoom
                Dim NewWidth As Integer = Width : Dim NewHeight As Integer
                If BackgroundImage.Width < Width Then NewWidth = BackgroundImage.Width
                NewHeight = BackgroundImage.Height * NewWidth / BackgroundImage.Width
                If (NewHeight > Height) Then
                    NewWidth = BackgroundImage.Width * Height / BackgroundImage.Height
                    NewHeight = Height
                End If
                GFX.DrawImage(BackgroundImage, CInt((Width / 2) - (NewWidth / 2)), CInt((Height / 2) - (NewHeight / 2)), NewWidth, NewHeight)
        End Select
    End Sub
    Public Sub New()
        BackColor = Color.White : Size = New Point(170, 23)
    End Sub
End Class