Imports System.Security.Cryptography, System.Text
Public Class Form1
#Region " About "
    '=========================================================
    '****276 Lines of code; Friday, May 1st, 2009; 3.0; Areon Inc.****
    '
    'I don't usually comment my code, but I will make an exception
    'here since I feel that working with Winsock is a bit advanced
    'for some programmers. Allow me to go over a few things real
    'quick that will get you started!
    '
    'Firstly the MessageServer() command, this obviously sends
    'data across the network to our hosted server. Within this
    'code you will see things such as MessageServer("/L|"), now
    'L is our packet command and | is our seperator to help the
    'server with parsing this information.
    '
    'Login Command (4 Arguments):
    'MessageServer("/L|USERNAME|MACHINE_ID|ENCRYPTED_PASSWORD|ENCRYPTED_MACHINE_ID)
    '
    'Register Command (3 Arguments):
    'MessageServer("/R|USERNAME|ENCRYPTED_PASSWORD|EMAIL)
    '
    'The following commands are setup as such ((Note: Full Name is never actually used in code
    'it's only purpose is to help you familiarize yourself with the command.)):
    '
    'Command - Full name (Send or Receive) - Definition
    '
    'Other Commands:
    'U - Update(R) - Contains the names of every user logged into the server. ("/U|User1|User2|Etc.")
    'E - Error(R) - Alerts you when the server confirms - logins, errors, etc..
    'B - Boot(R/S) - Informs the client the user is no longer connected or boots another member.
    'C - Chat(R/S) - Sends and receives chat to and from the server.
    'M - Moderator(R) - Activates the moderator menu if the use is a registered moderator.
    'H - Hide(R) - Simply updates a moderators UI if they are hidden or not.
    'T - Trace(S) - Requests an IP trace on a user from the server. ("/T|User")
    'CC - ColoredChat(R/S) - Sends and receives color coded chat. ("/CC|R|G|B|Chat")
    'BN - Ban(S) - Requests the server to ban a user. ((Machine_ID))
    'BNI - BanIP(S) - Requests the server to ban a user's IP address.
    '
    'Notes:
    '- MessageServer already applies an encryption to all outgoing packets.
    '
    '- Applying ' /w username message ' to a C or CC command, will whisper a user.
    '
    '- In its current, unaltered state this program does require the following libraries:
    '  Winsock Orcas.dll (4.0), Areon Library.dll (1.8), Areon Controls.dll (1.2) 
    '
    '- That about wraps it up! Thank you for supporting my work I hope you enjoy
    '  and find some use in this application. =)
    '
    '- I suggest you merge all dependencies of this application to its respective
    '  executable using a tool such as Gilma.
    '
    '- Moderators commands are checked server side before going though, so even if
    '  someone were to modify this client or hack it; the server determines who
    '  is and is not allowed to ban/trace/boot/hide/etc.
    '
    '- In the login command, where MACHINE_ID is used I would like to point out that
    '  using the Machine ID is *NOT* necessary you can use whatever you like, it is 
    '  simply to improve security.
    '=========================================================
#End Region
#Region " Global Variables "
    Dim MI, Username As String
#End Region
#Region " Omni Functions"
    Private Function MachineID() As String
        Dim Identifier As String = "" : Dim Manager As Object
        Dim Management As Object = GetObject("WinMgmts:")
        Manager = Management.InstancesOf("Win32_BaseBoard")
        For Each Serial In Manager : Identifier += Serial.SerialNumber : Next
        Manager = Management.InstancesOf("Win32_Processor")
        For Each Serial In Manager : Identifier += Serial.ProcessorId : Next
        Identifier += GetObject("WinMgmts:Win32_LogicalDisk='" & Environment.SystemDirectory.Substring(0, 1) & ":'").VolumeSerialNumber
        Manager = Management.InstancesOf("Win32_SystemEnclosure")
        For Each Serial In Manager : Identifier += Serial.SerialNumber : Next
        Return Identifier.Trim
    End Function
    Private Function TripleDES(ByVal Text As String, ByVal Password As String, Optional ByVal Decrypt As Boolean = False) As String
        Dim Crypt As String = ""
        Dim DES As New TripleDESCryptoServiceProvider
        Dim Hash As New MD5CryptoServiceProvider
        DES.Key = Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Password))
        DES.Mode = 2
        Select Case Decrypt
            Case True
                Dim DESDecrypter As ICryptoTransform = DES.CreateDecryptor
                Dim Buffer As Byte() = Convert.FromBase64String(Text)
                Crypt = ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Case False
                Dim DESEncrypter As ICryptoTransform = DES.CreateEncryptor
                Dim Buffer As Byte() = ASCIIEncoding.ASCII.GetBytes(Text)
                Crypt = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        End Select
        Return Crypt
    End Function
    Private Sub Sleep(ByVal Delay As Integer)
        For I = 1 To Delay : Windows.Forms.Application.DoEvents() : Threading.Thread.Sleep(1) : Next
    End Sub
    Enum Algorithm
        SHA1 = 0 : SHA256 = 1 : SHA384 = 2 : SHA512 = 3 : MD5 = 4
    End Enum
    Private Function Hash(ByVal Data As String, Optional ByVal HashAlgorithm As Algorithm = 4, Optional ByVal Hexadecimal As Boolean = False, Optional ByVal Salt As Byte() = Nothing) As String
        Return ComputeHash(Data, HashAlgorithm, Hexadecimal, Salt)
    End Function
    Private Function ComputeHash(ByVal Data As Object, ByVal Method As Integer, ByVal Hexadecimal As Boolean, ByVal Salt As Byte()) As String
        If Not Salt Is Nothing Then
            If Salt(0) = 0 Then
                Dim Random As New Random()
                Salt = New Byte(Random.Next(4, 10)) {}
                Dim RNG As New RNGCryptoServiceProvider()
                RNG.GetNonZeroBytes(Salt)
            End If
        End If
        Dim Flavor As Byte()
        If TypeOf Data Is String Then Flavor = Merge(Encoding.UTF8.GetBytes(Data), Salt) Else Flavor = Data
        Dim Hash As HashAlgorithm = Nothing
        Select Case Method
            Case 0
                Hash = New SHA1Managed()
            Case 1
                Hash = New SHA256Managed()
            Case 2
                Hash = New SHA384Managed()
            Case 3
                Hash = New SHA512Managed()
            Case 4
                Hash = New MD5CryptoServiceProvider()
        End Select
        If Hexadecimal Then Return BitConverter.ToString(Hash.ComputeHash(Flavor)).Replace("-", "") Else Return Convert.ToBase64String(Hash.ComputeHash(Flavor))
    End Function
    Private Function Merge(ByVal Primary As Object, ByRef Secondary As Object) As Object
        Try
            If Primary Is Nothing Then Return Secondary
            If Secondary Is Nothing Then Return Primary
            Dim X As New List(Of Object) : X.AddRange(Primary) : X.AddRange(Secondary) : Return X.ToArray
        Catch
            Throw New Exception("Unable to merge objects, this function is intended for arrays.")
            Return Nothing
        End Try
    End Function
#End Region
#Region " Client "
    Private Sub Client_DataArrival(ByVal sender As System.Object, ByVal e As Orcas.WinsockDataArrivalEventArgs) Handles Client.DataArrival
        Dim Data As String = TripleDES(CStr(Client.Get()), 4304, True)
        Dim Packet = New DataPacket(Data)
        Select Case Packet.Command
            Case "U"
                UserList.Items.Clear() : Panel2.Hide()
                If Data.Contains("|") Then
                    For Each X In Data.Remove(0, 3).Split("|") : UserList.Items.Add(X) : Next
                Else : UserList.Items.Clear() : End If
            Case "E"
                Dim PA() As String = Packet.Arguments(1)
                If Panel2.Visible Then
                    Label1.Text = PA(0)
                    Select Case PA(0)
                        Case "Account successfully created!"
                            ATB1.Clear() : ATB2.Clear() : ATB3.Clear() : ATB4.Clear() : ShowStart(False)
                        Case "Successfully logged in."
                            Username = ATB2.Text : ATB1.Clear() : ATB2.Clear()
                        Case "You have been banned."
                            Client.Close() : AB3.Visible = True : ShowStart(False, True)
                    End Select
                Else
                    Log(PA(0))
                End If
            Case "B"
                If Client.State <> 0 Then Client.Close()
                Panel2.Show() : If Label1.Text <> "You have been banned." Then Label1.Text = "You have been booted."
                Moderator(False)
            Case "C"
                Dim PA() As String = Packet.Arguments(1)
                Log(PA(0))
            Case "M"
                Moderator(True)
            Case "CC"
                Dim PA() As String = Packet.Arguments(4)
                LogColor(PA(0), PA(1), PA(2), PA(3))
            Case "H"
                If TSHide.Text = "Hide" Then TSHide.Text = "Unhide" Else TSHide.Text = "Hide"
        End Select
    End Sub
    Private Sub Client_Disconnected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Client.Disconnected
        Panel2.Show() : If Label1.Text <> "You have been banned." Then Label1.Text = "Disconnected" : AB3.Visible = True : ShowStart(False, True)
    End Sub
    Private Sub Connect()
        AB3.Visible = False
        Label1.Text = "Connecting..."
        ShowStart(False, True)
        Try
            Client.Connect(My.Settings.RHost, My.Settings.RPort)
            Do Until Client.State = 5
                If Client.State <> 4 Then
                    Label1.Text = "No Connection" : AB3.Visible = True : Exit Sub
                End If
                Sleep(1)
            Loop
            If Label1.Text <> "You have been banned." Then Label1.Text = "Connected"
        Catch : Label1.Text = "Disconnected" : AB3.Visible = True : End Try
        ShowStart(False)
    End Sub
    Private Sub MessageServer(ByVal Data As String)
        Client.Send(TripleDES(Data, 4304))
    End Sub
#End Region
#Region " GUI "
    Private Sub Log(Optional ByVal Data As String = "")
        If Data <> "" Then
            If TSTimestamp.Checked Then
                Input.AppendText("(" & Now.ToShortTimeString & ")" & " " & Data & Environment.NewLine)
            Else : Input.AppendText(Data & Environment.NewLine)
            End If
        Else : Input.AppendText(Environment.NewLine)
        End If
        If TSScroll.Checked Then : Input.SelectionStart = Input.TextLength - 1 : Input.ScrollToCaret() : End If
    End Sub
    Private Sub LogColor(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer, ByVal Data As String)
        Input.SelectionStart = Input.TextLength : Input.SelectionColor = Color.FromArgb(R, G, B)
        If TSTimestamp.Checked Then
            Input.SelectedText = "(" & Now.ToShortTimeString & ")" & " " & Data
        Else : Input.SelectedText = Data : End If
        Input.SelectionColor = Color.Black : Input.AppendText(Environment.NewLine)
        If TSScroll.Checked Then : Input.SelectionStart = Input.TextLength - 1 : Input.ScrollToCaret() : End If
    End Sub
    Private Function UserLoggedIn(ByVal Name As String) As Boolean
        For Each X In UserList.Items
            If X.ToLower = Name.ToLower Then Return True
        Next
        Return False
    End Function
    Private Sub Output_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Output.KeyDown
        If e.Control And e.KeyCode = 65 Then : Output.SelectAll() : e.SuppressKeyPress = True : End If
        If Output.Text = "" Then Exit Sub
        If e.KeyCode = Keys.Enter Then
            If Output.TextLength > 2 Then
                If Output.Text.Substring(0, 2) = "/w" Then
                    Try
                        Dim SendTo As String = Output.Text.Split(" ").GetValue(1)
                        If UserLoggedIn(SendTo) Then
                            Log("Whispering " & SendTo & ": " & Output.Text.Remove(0, 3 + Output.Text.Split(" ").GetValue(1).Length).Trim)
                            MessageServer("/C|" & Output.Text.Trim())
                        Else
                            Log(SendTo & " is offline.")
                        End If
                    Catch : End Try
                    Output.Clear() : e.SuppressKeyPress = True : Exit Sub
                End If
            End If
            If CD.Color <> Color.Black Then
                MessageServer("/CC|" & CD.Color.R & "|" & CD.Color.G & "|" & CD.Color.B & "|" & Output.Text.Trim())
                LogColor(CD.Color.R, CD.Color.G, CD.Color.B, Username & ": " & Output.Text.Trim())
            Else
                MessageServer("/C|" & Output.Text.Trim())
                Log(Username & ": " & Output.Text.Trim())
            End If
            Output.Clear() : e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Application.DoEvents()
        MI = MachineID()
        If MI = "" Then Close()
        Connect()
    End Sub
    Private Sub ShowStart(ByVal Register As Boolean, Optional ByVal Hide As Boolean = False)
        If Hide Then
            AB1.Visible = False : AB2.Visible = False
            Label5.Visible = False : Label4.Visible = False
            Label3.Visible = False : Label2.Visible = False
            ATB1.Visible = False : ATB2.Visible = False
            ATB3.Visible = False : ATB4.Visible = False
        Else
            ATB1.Clear() : ATB2.Clear() : ATB3.Clear() : ATB4.Clear()
            AB1.Visible = True : AB2.Visible = True
            ATB1.Visible = True : ATB2.Visible = True
            Label5.Visible = True : Label4.Visible = True
            If Register Then
                Label5.Text = "E-Mail" : ATB1.PasswordChar = ""
                Label4.Text = "Password" : ATB2.PasswordChar = "*"
                Label2.Visible = True : Label3.Visible = True
                ATB3.Visible = True : ATB4.Visible = True
                ATB4.Focus()
            Else
                Label5.Text = "Password" : ATB1.PasswordChar = "*"
                Label4.Text = "Username" : ATB2.PasswordChar = ""
                Label2.Visible = False : Label3.Visible = False
                ATB3.Visible = False : ATB4.Visible = False
                ATB2.Focus()
            End If
        End If
    End Sub
    Private Sub AB2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AB2.Click
        If ATB4.Visible Then
            If ATB4.Text <> "" And ATB3.Text <> "" And ATB2.Text <> "" And ATB1.Text <> "" Then
                If ATB2.Text <> ATB3.Text Then : Label1.Text = "Passwords do not match." : Exit Sub : End If
                MessageServer("/R|" & ATB4.Text & "|" & Hash(ATB2.Text, 0) & "|" & ATB1.Text) 'Register
            Else
                Label1.Text = "Please fill in all information."
            End If
        Else
            ShowStart(True)
        End If
    End Sub
    Private Sub AB1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AB1.Click
        If ATB4.Visible Then
            ShowStart(False)
        Else
            If ATB2.Text <> "" And ATB1.Text <> "" Then
                MessageServer("/L|" & ATB2.Text & "|" & MI & "|" & Hash(ATB1.Text, 0) & "|" & TripleDES(MI, 9000)) 'Login
                ATB1.Clear()
            Else
                Label1.Text = "Please fill in all information."
            End If
        End If
    End Sub
    Private Sub Panel2_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel2.VisibleChanged
        If Panel2.Visible Then
            TS.Hide()
            Size = New Point(598, 448)
            MaximizeBox = False : FormBorderStyle = 1
            Input.Enabled = False : Output.Enabled = False : ULContainer.Enabled = False
        Else
            MaximizeBox = True : FormBorderStyle = 4
            Input.Enabled = True : Output.Enabled = True : ULContainer.Enabled = True
            TS.Show()
            TSUser.Text = Username
        End If
    End Sub
    Private Sub AB3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AB3.Click
        Connect()
    End Sub
    Private Sub CM_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CM.Opening
        If UserList.SelectedItems.Count = 0 Then e.Cancel = True
    End Sub
    Private Sub CMWhisper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMWhisper.Click
        Output.Text = "/w " & UserList.SelectedItem & " " : Output.Focus() : Output.SelectionStart = Output.TextLength
    End Sub
    Private Sub CMQuickBan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMQuickBan.Click
        MessageServer("/BN|" & UserList.SelectedItem)
    End Sub
    Private Sub CMBan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBan.Click
        Try
            Dim I As Integer = InputBox("Duration in minutes to ban the user.", Text, 10) : If I < 1 Then I = 1
            MessageServer("/BN|" & UserList.SelectedItem & "|" & I)
        Catch : End Try
    End Sub
    Private Sub CMBoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBoot.Click
        MessageServer("/B|" & UserList.SelectedItem)
    End Sub
    Private Sub TSClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSClear.Click
        If MessageBox.Show("Do you really want to clear all the chat?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = 6 Then Input.Clear()
    End Sub
    Private Sub TSDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSDisconnect.Click
        Client.Close()
    End Sub
    Private Sub AB4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AB4.Click
        Form2.ShowDialog()
    End Sub
    Private Sub Input_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles Input.LinkClicked
        If MessageBox.Show("Clicking this link may harm your computer, proceed?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = 6 Then Process.Start(e.LinkText)
    End Sub
    Private Sub Me_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ATB1.KeyDown, ATB2.KeyDown, ATB3.KeyDown, ATB4.KeyDown, Input.KeyDown
        If e.Control And e.KeyCode = 65 Then : sender.SelectAll() : e.SuppressKeyPress = True : End If
    End Sub
    Private Sub TSColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSColor.Click
        CD.ShowDialog()
    End Sub
    Private Sub CMTrace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMTrace.Click
        MessageServer("/T|" & UserList.SelectedItem)
    End Sub
    Private Sub TSHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSHide.Click
        MessageServer("/H")
    End Sub
    Private Sub Moderator(ByVal Setting As Boolean)
        CMS1.Visible = Setting : CMBan.Visible = Setting
        CMS2.Visible = Setting : CMTrace.Visible = Setting
        CMQuickBan.Visible = Setting : CMBoot.Visible = Setting
        TSColor.Visible = Setting : CD.Color = Color.Black
        TSHide.Visible = Setting : TSHide.Text = "Hide"
    End Sub
#End Region
End Class
