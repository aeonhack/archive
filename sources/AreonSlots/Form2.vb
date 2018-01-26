Public Class Form2
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Then Exit Sub
        Form1.User = Replace(TextBox1.Text.Trim, "|", "|")
        Me.Close()
    End Sub
    Private Sub Form2_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Form1.User = Nothing Then Form1.Close()
    End Sub
End Class