Public Class Form2
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ATB5.Text = My.Settings.RHost
        ATB6.Text = My.Settings.RPort
    End Sub
    Private Sub AreonButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AreonButton1.Click
        Close()
    End Sub
    Private Sub AB3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AB3.Click
        My.Settings.RHost = ATB5.Text
        My.Settings.RPort = ATB6.Text
        My.Settings.Save()
        Close()
    End Sub
    Private Sub Me_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ATB5.KeyDown, ATB6.KeyDown
        If e.Control And e.KeyCode = Keys.A Then : sender.SelectAll() : e.SuppressKeyPress = True : End If
    End Sub
End Class