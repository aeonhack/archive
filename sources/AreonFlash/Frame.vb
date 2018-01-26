Public Class Frame
    Private Sub Frame_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = Main.Shockwave.CurrentFrame
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then e.Handled = True
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Main.Shockwave.GotoFrame(Val(TextBox1.Text))
        Main.HistoryC.Add("Set Frame$" & Val(TextBox1.Text))
    End Sub
End Class