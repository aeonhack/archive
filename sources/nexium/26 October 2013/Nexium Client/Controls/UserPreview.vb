Public Class UserPreview

    Private Sub UserPreview_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        AddHandler e.Control.MouseDown, AddressOf CaptureFocus
    End Sub

    Private Sub CaptureFocus(sender As Object, e As MouseEventArgs)
        Focus()
    End Sub

    Private Sub UserPreview_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        BackColor = Color.FromArgb(160, 245, 255)
    End Sub

    Private Sub UserPreview_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        BackColor = Color.White
    End Sub

End Class
