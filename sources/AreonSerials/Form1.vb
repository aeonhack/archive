Public Class Form1
    Dim Elapsed As DateTime
    Dim Quantity As Integer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Val(TextBox3.Text) < 5 Then TextBox3.Text = "5"
        If Val(TextBox3.Text) > 32 Then TextBox3.Text = "32"
        If Math.Pow(36, Val(TextBox3.Text)) < Val(TextBox1.Text) Then
            MessageBox.Show("There is not enough variation available to create the desired ammount of serials" & vbCrLf & "please lower the quantity or increase the length.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Intialize()
        TextBox1.Enabled = False
        TextBox3.Enabled = False
        Do Until ProgressBar1.Value = ProgressBar1.Maximum
            TextBox2.AppendText(GenSerial(Val(TextBox3.Text)) & vbCrLf)
            ProgressBar1.Value += 1
            Label4.Text = Math.Round((100 / ProgressBar1.Maximum) * ProgressBar1.Value, 0) & "%"
            Label3.Text = "Elapsed: " & (TimeOfDay.TimeOfDay - Elapsed.TimeOfDay).ToString
            Label2.Text = "Serials: " & ProgressBar1.Value
            Application.DoEvents()
        Loop
        TextBox2.Text = TextBox2.Text.Trim()
        TextBox1.Enabled = True
        TextBox3.Enabled = True
        MessageBox.Show(ProgressBar1.Value & " serials successfully generated.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Intialize()
        If TextBox1.Text < 1 Then TextBox1.Text = 1
        Quantity = TextBox1.Text
        ProgressBar1.Maximum = Quantity
        ProgressBar1.Value = 0
        TextBox2.Text = Nothing
        Elapsed = TimeOfDay
        Label4.Text = "0%"
        Label3.Text = "00.00.00"
        Label2.Text = "Serials: 0"
    End Sub
    Private Function GenSerial(Optional ByVal Length As Integer = 15) As String
        If Length > 32 Then Length = 32
        Return System.Guid.NewGuid().ToString.Replace("-", Nothing).ToUpper.Substring(0, Length)
    End Function
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = Nothing Then Exit Sub
        My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox2.Text, False)
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then e.Handled = True
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then e.Handled = True
    End Sub
End Class
