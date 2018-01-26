Public Class UserPreview

    Private _User As User
    Public ReadOnly Property User() As User
        Get
            Return _User
        End Get
    End Property

    Sub New(user As User)
        InitializeComponent()

        _User = user
        Label1.Text = user.Name
    End Sub

    Private _Selected As Boolean
    Public ReadOnly Property Selected() As Boolean
        Get
            Return _Selected
        End Get
    End Property

    Private Sub ServerPreview_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        AddHandler e.Control.MouseDown, AddressOf CaptureFocus
    End Sub

    Private Sub CaptureFocus(sender As Object, e As MouseEventArgs)
        Focus()
    End Sub

    Private Sub ServerPreview_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        _Selected = True

        For Each C As Control In Parent.Controls
            If TypeOf C Is UserPreview AndAlso C IsNot Me Then
                Dim U As UserPreview = DirectCast(C, UserPreview)
                U._Selected = False
                U.BackColor = Color.White
            End If
        Next

        BackColor = Color.FromArgb(160, 245, 255)
    End Sub

End Class
