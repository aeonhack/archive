Public Class Manipulation
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Main.Shockwave.SetVariable(TextBox1.Text, "")
            Main.HistoryC.Add("Set Variable$" & TextBox1.Text & " - " & TextBox2.Text)
        Catch Ex As Exception
            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Main.HistoryC.Add("Error$" & Ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Main.Shockwave.CallFunction(TextBox3.Text)
            Main.HistoryC.Add("Called Function$" & TextBox3.Text)
        Catch Ex As Exception
            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Main.HistoryC.Add("Error$" & Ex.Message)
        End Try
    End Sub
    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        Me.AcceptButton = Button1
    End Sub
    Private Sub TextBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Enter
        Me.AcceptButton = Button1
    End Sub
    Private Sub TextBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.Enter
        Me.AcceptButton = Button2
    End Sub
End Class