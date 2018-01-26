Public Class ServerPreview

    Private _SecureConnection As Boolean
    Public Property SecureConnection() As Boolean
        Get
            Return _SecureConnection
        End Get
        Set(ByVal value As Boolean)
            _SecureConnection = value

            If value Then
                PictureBox1.BackgroundImage = My.Resources.secure3
            Else
                PictureBox1.BackgroundImage = My.Resources.unsecure
            End If
        End Set
    End Property

    Private Sub ServerPreview_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        AddHandler e.Control.MouseDown, AddressOf CaptureFocus
    End Sub

    Private Sub CaptureFocus(sender As Object, e As MouseEventArgs)
        Focus()
    End Sub

    Private Sub ServerPreview_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        BackColor = Color.FromArgb(160, 245, 255)
    End Sub

    Private Sub ServerPreview_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        BackColor = Color.White
    End Sub

End Class
