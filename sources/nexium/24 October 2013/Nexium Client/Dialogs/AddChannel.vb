Public Class AddChannel

    Property EndPoint As ServerEndpoint

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        EndPoint = New ServerEndpoint(TextBox1.Text, CUShort(NumericUpDown1.Value))
        DialogResult = Windows.Forms.DialogResult.OK
        Close()
    End Sub

End Class