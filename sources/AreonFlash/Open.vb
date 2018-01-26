Public Class Open
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text <> Nothing Then
            Try
                Main.Shockwave.Movie = TextBox1.Text.Trim
                Main.Shockwave.Play()
                If Main.Shockwave.TotalFrames > 0 Then
                    Main.HistoryC.Add("Loaded$" & TextBox1.Text.Trim)
                    Me.Close()
                End If
            Catch Ex As Exception
                Main.HistoryC.Add("Error$" & Ex.Message)
            End Try
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> Nothing Then
            TextBox1.Text = OpenFileDialog1.FileName.ToString
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Length > 6 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub
    Private Sub Open_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TopLevel = True
    End Sub
    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        If TextBox1.Text <> Nothing Then TextBox1.SelectAll()
    End Sub
End Class