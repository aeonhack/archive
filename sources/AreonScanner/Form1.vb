Public Class Form1
    Private Function CheckPort(ByVal Address As String, ByVal Port As Integer, ByVal Timeout As Integer)
        Dim Client = New Net.Sockets.TcpClient
        Try
            Client.BeginConnect(Address, Port, Nothing, Nothing)
            For I = 0 To Timeout
                Application.DoEvents()
                Threading.Thread.Sleep(1)
                If Client.Connected Then
                    Client.Close()
                    Return True
                End If
            Next
            Client.Close()
            Return False
        Catch Ex As Exception
            Client.Close()
            Return False
        End Try
    End Function
    Dim Elapsed As DateTime
    Private Sub ToggleState(ByVal State As Boolean)
        Working = Not State
        Elapsed = TimeOfDay
        TextBox1.Enabled = State
        TextBox2.Enabled = State
        TextBox3.Enabled = State
        TextBox4.Enabled = State
        Timer1.Enabled = Not State
        Select Case State
            Case True
                Button1.Text = "Begin Scan"
            Case False
                Button1.Text = "Cancel Scan"
        End Select
    End Sub
    Public Shared Function ResolveDNS(ByVal Host As String) As String
        Try
            Dim Entry As Net.IPHostEntry = Net.Dns.GetHostEntry(Host)
            Dim Address As Net.IPAddress() = Entry.AddressList
            Return Address.GetValue(0).ToString
        Catch
            Return Nothing
        End Try
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Select Case Working
            Case True
                ToggleState(True)
            Case False
                TextBox1.Text = ResolveDNS(TextBox1.Text)
                If TextBox1.Text = Nothing Then
                    MessageBox.Show("You have entered an invalid host.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If Val(TextBox2.Text) > Val(TextBox3.Text) Then
                    MessageBox.Show("Starting port must be smaller then the end port.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If Val(TextBox3.Text) > 65535 Then TextBox3.Text = "65535"
                ToggleState(False)
                Label6.Visible = True
                ListView1.Items.Clear()
                Worker1.RunWorkerAsync()
                Worker2.RunWorkerAsync()
                Worker3.RunWorkerAsync()
                Worker4.RunWorkerAsync()
        End Select
    End Sub
    Dim Working As Boolean
    Private Delegate Sub PortStatusHandler(ByVal Port As Integer)
    Private Sub PortStatus(ByVal Port As Integer)
        ListView1.Items.Add(Port.ToString, 0)
        ListView1.EnsureVisible(ListView1.Items.Count - 1)
    End Sub
    Private Sub InvokeChange(ByVal Port As Integer)
        Invoke(New PortStatusHandler(AddressOf PortStatus), Port)
    End Sub
    Private Sub Worker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker1.DoWork
        For I = Val(TextBox2.Text) To Val(TextBox3.Text)
            If Not Working Then Exit Sub
            If CheckPort(TextBox1.Text, I, Val(TextBox4.Text)) Then InvokeChange(I)
            I += 3
        Next
    End Sub
    Private Sub Worker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker2.DoWork
        For I = (Val(TextBox2.Text) + 1) To Val(TextBox3.Text)
            If Not Working Then Exit Sub
            If CheckPort(TextBox1.Text, I, Val(TextBox4.Text)) Then InvokeChange(I)
            I += 3
        Next
    End Sub
    Private Sub Worker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker3.DoWork
        For I = (Val(TextBox2.Text) + 2) To Val(TextBox3.Text)
            If Not Working Then Exit Sub
            If CheckPort(TextBox1.Text, I, Val(TextBox4.Text)) Then InvokeChange(I)
            I += 3
        Next
    End Sub
    Private Sub Worker4_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker4.DoWork
        For I = (Val(TextBox2.Text) + 3) To Val(TextBox3.Text)
            If Not Working Then Exit Sub
            If CheckPort(TextBox1.Text, I, Val(TextBox4.Text)) Then InvokeChange(I)
            I += 3
        Next
    End Sub
    Private Sub WorkComplete()
        If Not Worker1.IsBusy And Not Worker2.IsBusy And Not Worker3.IsBusy And Not Worker4.IsBusy Then
            ToggleState(True)
            MessageBox.Show("Scanning is complete." & vbCrLf & vbCrLf & FormatNumber(ListView1.Items.Count, 0) & " ports accessible", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub Worker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker1.RunWorkerCompleted
        WorkComplete()
    End Sub
    Private Sub Worker2_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker2.RunWorkerCompleted
        WorkComplete()
    End Sub
    Private Sub Worker3_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker3.RunWorkerCompleted
        WorkComplete()
    End Sub
    Private Sub Worker4_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker4.RunWorkerCompleted
        WorkComplete()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label6.Text = "Elapsed: " & (TimeOfDay.TimeOfDay - Elapsed.TimeOfDay).ToString
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Char.IsWhiteSpace(e.KeyChar) Then e.Handled = True
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not Asc(e.KeyChar) = 8 Then e.Handled = True
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not Asc(e.KeyChar) = 8 Then e.Handled = True
    End Sub
    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not Asc(e.KeyChar) = 8 Then e.Handled = True
    End Sub
End Class
