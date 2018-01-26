Imports System.Security.Cryptography, System.Text, System.Text.RegularExpressions
Public Class ServerForm
#Region " About "
    '=========================================================
    '****680 Lines of code; Friday, May 1st, 2009; 3.1; Areon Inc.****
    '
    'I won't lie, I REALLY do not feel like commenting on all the server code
    'but I will because I love you guys in the straightest way possible. <3
    'Where to start... firstly let me point out one of our global variables.
    '
    'Friend WithEvents WC As New Orcas.WinsockCollection(True)
    '
    'This is a collection that stores all of our connection information;
    'In a format similar to this.
    '
    'GUID - Status
    '
    'What is a GUID? - Globally Unique Identifier (Ex. 54929-AS234-FGY23-HE123)
    'When a connection is made to our server, we add it to WC (Winsock Collection)
    'WC then auto assigns said item with a GUID.
    '
    'Please, please, please~ notice our DataArival and Disconnected events are handled
    'by WC and NOT our server - this was a major issue that took me forever to figure out (Silly me).
    '
    'We also have our Users collection, all of our users share they same GUID that the WC collection
    'assigns to them, this collection stores valuable information such as..
    '
    'Username
    'Logged in state
    'Signature (Machine ID)
    'IP
    'Rank (Moderators are 1, normal users are 0)
    'FloodCount/LastSend/LastMessage (Flood Protection)
    '
    'Registering, Banning, Moderators are all stored in files within the same directory as our
    'server executable.
    '
    'Message(ID, Data) will send a message to a single user.
    'Relay(Data) will send a message to everyone in the server.
    'RelayX(Data, Sender) will message everyone excluding the Sender.
    '
    'Please keep in mind that Relay, and RelayX both use the Message(ID, Data) functions, and
    'it does not auto apply any command. If you wish to send all users a message you would
    'do something such as: Relay("/C|Server: Hello everyone!"), if you were to send:
    'Relay("/M") it would activate everyone's moderator menu's so please be aware of this.
    '
    'If you see something such as Users(ID) or Users(X), this is the same as saying User.
    '
    'That is about all I can say, experiment around - keep a backup of your project folder 
    'since Visual Basic .Net 2008 auto saves, trial and error! Good luck and I hope you enjoy
    'this little treasure of mine. =)
#End Region
#Region " Global Variables "
    Private Users As New UserCollection
    Friend WithEvents WC As New Orcas.WinsockCollection(True)
    Dim ConnectionStart As DateTime
#End Region
#Region " Areon Functions "
    Public Shared Function Secure(ByVal Text As String, ByVal Password As String, Optional ByVal Decrypt As Boolean = False) As String
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
    Public Shared Function Lines(ByVal Text As String) As String()
        Dim Output As New List(Of String)
        For Each Line In Text.Split(Environment.NewLine)
            If Line <> "" Then Output.Add(Line.Replace(Chr(10), "").Replace(Chr(13), ""))
        Next
        Return Output.ToArray
    End Function
    Public Shared Function ValidateEmail(ByVal Email As String) As Boolean
        Return Regex.IsMatch(Email, "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
    End Function
    Public Shared Function ParseIP(ByVal Text As String) As String
        Dim IP As String = Regex.Match(Text, "\b(?:\d{1,3}\.){3}\d{1,3}\b").Value
        If Net.IPAddress.TryParse(IP, Nothing) Then Return IP
        Return Nothing
    End Function
    Public Shared Function ExternalIP() As String
        Dim Request As Net.WebRequest
        Dim SR As IO.StreamReader
        For Each Host In Lines(My.Resources.Hosts)
            Try
                Request = Net.WebRequest.Create(Host)
                Request.Timeout = 7000
                SR = New IO.StreamReader(Request.GetResponse.GetResponseStream)
                ExternalIP = ParseIP(SR.ReadToEnd)
                SR.Close()
                If ExternalIP <> Nothing Then Return ExternalIP
            Catch : End Try
        Next
        Return "Unavailable"
    End Function
#End Region
#Region " Server "
    Private Sub Server_ConnectionRequest(ByVal Sender As System.Object, ByVal E As Orcas.WinsockConnectionRequestEventArgs) Handles Server.ConnectionRequest
        Dim ID As Guid = WC.Accept(E.Client)
        If IsBanned(E.ClientIP) Then : Message(ID, "/E|You have been banned.") : E.Cancel = True : WC.Remove(ID) : Exit Sub : End If
        Users.AddUser(ID) : Users(ID).IP = E.ClientIP
    End Sub
    Private Sub Users_Disconnected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WC.Disconnected
        Dim ID As Guid = WC.FindGID(CType(sender, Orcas.Winsock))
        Dim X As User = Users(ID)
        On Error Resume Next
        If X.LoggedIn Then Log(Users(ID).Username & " has logged out.")
        Users.RemoveUser(ID) : UpdateUsers()
    End Sub
    Private Sub Users_DataArrival(ByVal Sender As Object, ByVal E As Orcas.WinsockDataArrivalEventArgs) Handles WC.DataArrival
        Try
            Dim ID As Guid = WC.FindGID(CType(Sender, Orcas.Winsock))
            If Not Users.ContainsKey(ID) Then Exit Sub
            Dim X As User = Users(ID)
            Dim Data As String = WC.Item(ID).Get()
            Try
                Data = Secure(Data, 4304, True)
            Catch
                Log()
                Log(X.Username & " has sent a corrupt packet.")
                BanUser(ID, 10)
                Log()
                Exit Sub
            End Try
            Dim Packet As New DataPacket(Data)
            If Not X.LoggedIn Then
                If Packet.Command = "L" Then
                    Dim PA() As String = Packet.Arguments(4)
                    If PA.Length <> 4 Then
                        Message(ID, "/E|Corrupt packet structure.") : Exit Sub
                    End If
                    Try
                        If PA(1) <> Secure(PA(3), 9000, True) Then
                            Message(ID, "/E|Failed to validate credentials.") : Exit Sub
                        End If
                    Catch
                        Log()
                        Log(X.Username & " has sent a corrupt packet.")
                        Message(ID, "/E|Corrupt packet, server has been notified.")
                        Log() : Exit Sub
                    End Try
                    If IsBanned(PA(1)) Then
                        Message(ID, "/E|You have been banned.")
                        BootUser(ID) : Exit Sub
                    End If
                    If Users.HasName(PA(0)) Then
                        Message(ID, "/E|That username is already in use.") : Exit Sub
                    End If
                    If IsNothing(LoadUser(PA(0))) Then
                        Message(ID, "/E|That username does not exist.") : Exit Sub
                    End If
                    If PA(2) <> LoadUser(PA(0)).GetValue(0).ToString Then
                        Message(ID, "/E|Incorrect password.") : Exit Sub
                    End If
                    X.Login(PA(0), PA(1))
                    If X.LoggedIn Then
                        Message(ID, "/E|Successfully logged in.")
                    Else
                        Message(ID, "/E|Failed to logon.") : Exit Sub
                    End If
                    If IsStaff(X.Username, X.Signature) Then
                        X.Rank = 1 : Message(ID, "/M")
                    End If
                    Log(X.Username & " has logged in.") : UpdateUsers()
                ElseIf Packet.Command = "R" Then
                    Dim PA() As String = Packet.Arguments(3)
                    If PA.Length <> 3 Then
                        Message(ID, "/E|Corrupt packet structure.") : Exit Sub
                    End If
                    If PA(0).Length < 4 Then
                        Message(ID, "/E|That username is too short, 4 characters minimum.") : Exit Sub
                    End If
                    If PA(0).Contains(" ") Or PA(0).Contains("`") Then
                        Message(ID, "/E|Username contains invalid characters.") : Exit Sub
                    End If
                    If Not ValidateEmail(PA(2)) Then
                        Message(ID, "/E|You have entered an invalid email.") : Exit Sub
                    End If
                    Try
                        If My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Users.dat").ToLower.Contains(PA(2).ToLower) Then
                            Message(ID, "/E|That email address is already in use.") : Exit Sub
                        End If
                    Catch
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Users.dat", "", True)
                    End Try
                    If Not IsNothing(LoadUser(PA(0))) Then
                        Message(ID, "/E|That username is already in use.") : Exit Sub
                    End If
                    If PA(0).ToLower = "server" Then
                        Message(ID, "/E|That username is reserved.") : Exit Sub
                    End If
                    RegisterUser(PA(0), PA(1), PA(2))
                    If My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Users.dat").Contains(PA(0) & "`" & PA(1) & "`" & PA(2)) Then
                        Log(PA(0) & " registered an account.")
                        Message(ID, "/E|Account successfully created!")
                    Else
                        Message(ID, "/E|Account failed to create.")
                    End If
                Else
                    Message(ID, "/E|You must login.")
                End If
            Else
                If Packet.Command = "C" Then
                    If X.Rank = 0 Then
                        If IsFlooding(ID, Data) Then
                            If Users(ID).FloodCount = 3 Then
                                Message(ID, "/CC|255|0|0|Flooding will result in a ban.")
                                If Users(ID).LoggedIn Then LogColor(255, 0, 0, Users(ID).Username & " is flooding.")
                            End If
                            If Users(ID).FloodCount >= 6 Then
                                Dim U As String = Users(ID).Username
                                Message(ID, "/CC|255|0|0|You have been banned for flooding.")
                                BanUser(ID, 5)
                                LogColor(255, 0, 0, U & " has been banned for flooding.")
                                Relay("/CC|255|0|0|" & U & " has been banned for flooding.")
                                Exit Sub
                            End If
                        End If
                    End If
                    Dim PA() As String = Packet.Arguments(1)
                    If PA(0).Length > 2 And PA(0).StartsWith("/") Then
                        If PA(0).Substring(0, 2).ToLower = "/w" Then
                            Dim SendTo As String = PA(0).Split(" ").GetValue(1)
                            If Not Users.HasName(SendTo) Or Users(Users.FindUser(SendTo)).Hidden Then
                                Message(ID, "/C|" & SendTo & " is offline.")
                            Else
                                Message(Users.FindUser(SendTo), "/C|" & X.Username & " Whispers: " & PA(0).Remove(0, 4 + SendTo.Length))
                                Log(X.Username & " Whispering " & SendTo & ": " & PA(0).Remove(0, 4 + SendTo.Length))
                            End If
                        Else
                            Message(ID, "/E|Unrecognized command.")
                        End If
                    Else
                        RelayX("/C|" & X.Username & ": " & PA(0), ID)
                        Log(X.Username & ": " & PA(0))
                    End If
                ElseIf Packet.Command = "B" And X.Rank = 1 Then
                    Dim PA() As String = Packet.Arguments(1)
                    If Users.HasName(PA(0)) Then
                        BootUser(Users.FindUser(PA(0)))
                        Message(ID, "/C|" & PA(0) & " has been booted.")
                        Log(X.Username & " has booted " & PA(0) & ".")
                    Else
                        Message(ID, "/C|" & PA(0) & " is offline.")
                    End If
                ElseIf Packet.Command = "BN" And X.Rank = 1 Then
                    Dim PA() As String = Packet.Arguments(2)
                    If Users.HasName(PA(0)) Then
                        If PA.Length = 1 Then
                            BanUser(Users.FindUser(PA(0)))
                            Message(ID, "/C|" & PA(0) & " has been banned.")
                            Log(X.Username & " has banned " & PA(0) & ".")
                        Else
                            BanUser(Users.FindUser(PA(0)), CInt(PA(1)))
                            Message(ID, "/C|" & PA(0) & " has been banned for " & CInt(PA(1)) & " minutes.")
                            Log(X.Username & " has banned " & PA(0) & " for " & CInt(PA(1)) & " minutes.")
                        End If
                    Else
                        Message(ID, "/C|" & PA(0) & " is offline.")
                    End If
                ElseIf Packet.Command = "H" And X.Rank = 1 Then
                    If X.Hidden Then X.Hidden = False Else X.Hidden = True
                    Message(ID, "/H") : UpdateUsers()
                ElseIf Packet.Command = "T" And X.Rank = 1 Then
                    Dim PA() As String = Packet.Arguments(1)
                    Message(ID, "/C|" & PA(0) & ": " & Users(Users.FindUser(PA(0))).IP)
                    Log(X.Username & " traced " & PA(0) & ": " & Users(Users.FindUser(PA(0))).IP)
                ElseIf Packet.Command = "CC" And X.Rank = 1 Then
                    Dim PA() As String = Packet.Arguments(4)
                    If PA.Length <> 4 Then
                        Message(ID, "/E|Corrupt packet structure.") : Exit Sub
                    End If
                    RelayX("/CC|" & PA(0) & "|" & PA(1) & "|" & PA(2) & "|" & X.Username & ": " & PA(3), ID)
                    LogColor(PA(0), PA(1), PA(2), X.Username & ": " & PA(3))
                End If
            End If
        Catch : End Try
    End Sub

    Private Sub RegisterUser(ByVal Username As String, ByVal Password As String, ByVal Email As String)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Users.dat", Username & "`" & Password & "`" & Email & Environment.NewLine, True)
    End Sub
    Private Function LoadUser(ByVal Username As String) As String()
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "/Users.dat") Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Users.dat", Nothing, False)
        Dim Information(3) As String
        For Each User In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Users.dat"))
            If User.Split("`").GetValue(0).ToLower = Username.ToLower Then
                Information(0) = User.Split("`").GetValue(1)
                Information(1) = User.Split("`").GetValue(2)
                Return Information
            End If
        Next
        Return Nothing
    End Function
    Private Function IsBanned(ByVal Data As String) As Boolean
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "/Ban.dat") Then Return False
        Dim X As Integer
        If ParseIP(Data) <> "" Then X = 1
        For Each User In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Ban.dat"))
            If User.Split("`").GetValue(X) = Data Then
                If Date.Parse(User.Split("`").GetValue(3)) > Now Then Return True Else 
                Dim NewList As String = Replace(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Ban.dat"), User, "")
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Ban.dat", NewList, False)
                Return False
            End If
        Next
        Return False
    End Function
    Private Function IsStaff(ByVal Username As String, ByVal Clearance As String) As Boolean
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "/Staff.dat") Then Return False
        For Each User In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Staff.dat"))
            If User.ToLower = Username.ToLower & "`" & Clearance.ToLower Then Return True
        Next
        Return False
    End Function
    Private Function IsFlooding(ByVal ID As Guid, ByVal Data As String) As Boolean
        If Users(ID).LastSend.AddSeconds(1) > Now Or Users(ID).LastMessage = Data Then : Users(ID).FloodCount += 1 : Return True : End If
        Users(ID).LastSend = Now : Users(ID).LastMessage = Data
        If Now > Users(ID).LastSend.AddSeconds(1) And Users(ID).LastMessage <> Data Then Users(ID).FloodCount -= 1
        If Users(ID).FloodCount < 0 Then Users(ID).FloodCount = 0
        Return False
    End Function
    Private Sub Message(ByVal ID As Guid, ByVal Data As String)
        On Error Resume Next
        WC.Item(ID).Send(Secure(Data, 4304))
    End Sub
    Private Sub Relay(ByVal Data As String)
        For Each X In Users.Keys : If Users(X).LoggedIn Then Message(X, Data)
        Next
    End Sub
    Private Sub RelayX(ByVal Data As String, ByVal Sender As Guid)
        For Each X In Users.Keys : If Users(X).LoggedIn And Users(X).UserID <> Sender Then Message(X, Data)
        Next
    End Sub
    Private Sub UpdateUsers()
        Dim ULO As String = "/U" : UserList.Items.Clear()
        For Each X In Users.Values
            If X.LoggedIn Then
                If Not X.Hidden Then ULO += "|" & X.Username
                If Not X.Hidden Then UserList.Items.Add(X.Username) Else UserList.Items.Add(X.Username & " (H)")
            End If
        Next
        Relay(ULO) : SSUsers.Text = "Users: " & UserList.Items.Count
    End Sub

    Private Sub UnbanUser(ByVal Username As String)
        For Each Line In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Ban.dat"))
            If Line.Split("`").GetValue(1).ToLower = Username.ToLower Then
                Dim Output As String = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Ban.dat").Replace(Line, "")
                My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Ban.dat", Output, False)
                Exit Sub
            End If
        Next
    End Sub
    Private Sub BanIP(ByVal ID As Guid, ByVal Minutes As Integer, Optional ByVal Boot As Boolean = True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/IPBan.dat", Users(ID).IP & "`" & Date.Now.AddMinutes(Minutes) & Environment.NewLine, True)
        If Boot Then BootUser(ID)
    End Sub
    Private Sub BanUser(ByVal ID As Guid, Optional ByVal Minutes As Integer = 10)
        Dim X As User = Users(ID)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Ban.dat", X.Signature & "`" & X.IP & "`" & X.Username.ToLower & "`" & Now.AddMinutes(Minutes) & Environment.NewLine, True)
        BootUser(ID)
    End Sub
    Private Sub BootUser(ByVal ID As Guid)
        Dim IP As String = Users(ID).IP : Dim Matches As New List(Of Guid)
        For Each X In Users.Keys : If Users(X).IP = IP Then Matches.Add(X)
        Next
        For Each X In Matches
            Message(X, "/B") : WC.Remove(X) : Users.RemoveUser(X)
        Next
    End Sub
    Private Sub BootAll()
        Relay("/B")
        Users.Clear() : WC.Clear()
    End Sub
#End Region
#Region " GUI "
    Private Sub Log(Optional ByVal Data As String = "")
        If Data <> "" Then
            Input.AppendText("(" & Now.ToShortTimeString & ")" & " " & Data & vbCrLf)
        Else
            Input.AppendText(vbCrLf)
        End If
        If TSScroll.Checked Then : Input.SelectionStart = Input.TextLength - 1 : Input.ScrollToCaret() : End If
        If My.Settings.Log Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\" & Now.ToShortDateString.Replace("/", "-") & ".log", "(" & Now.ToShortTimeString & ")" & " " & Data & vbCrLf, True)
    End Sub
    Private Sub LogColor(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer, ByVal Data As String)
        Input.SelectionStart = Input.TextLength : Input.SelectionColor = Color.FromArgb(R, G, B)
        Input.SelectedText = "(" & Now.ToShortTimeString & ")" & " " & Data : Input.SelectionColor = Color.Black : Input.AppendText(vbCrLf)
        If TSScroll.Checked Then : Input.SelectionStart = Input.TextLength - 1 : Input.ScrollToCaret() : End If
        If My.Settings.Log Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\" & Now.ToShortDateString.Replace("/", "-") & ".log", "(" & Now.ToShortTimeString & ")" & " " & Data & vbCrLf, True)
    End Sub
    Private Sub OutputEnter()
        If Output.Text = "" Then Exit Sub
        If Output.Text.StartsWith("/") Then
            Dim CA As String() = Output.Text.Split(" ")
            Select Case CA(0).ToLower
                Case "/unban"
                    If CA.Length > 1 Then
                        UnbanUser(CA(1))
                        Log(CA(1) & " has been unbanned.")
                    End If
                Case "/w"
                    If CA.Length > 2 Then
                        If Users.HasName(CA(1)) Then
                            If CD.Color <> Color.Black Then
                                LogColor(CD.Color.R, CD.Color.G, CD.Color.B, "Whispering " & CA(1) & ": " & Output.Text.Remove(0, 4 + CA(1).Length))
                                Message(Users.FindUser(CA(1)), "/CC|" & CD.Color.R & "|" & CD.Color.G & "|" & CD.Color.B & "|" & "Server Whispers: " & Output.Text.Remove(0, 4 + CA(1).Length))
                            Else
                                Log("Whispering " & CA(1) & ": " & Output.Text.Remove(0, 4 + CA(1).Length))
                                Message(Users.FindUser(CA(1)), "/C|Server Whispers: " & Output.Text.Remove(0, 4 + CA(1).Length))
                            End If
                        Else
                            Log(CA(1).Replace(" (H)", "") & " is offline.")
                        End If
                    End If
                Case Else
                    Log("You have entered an invalid command.")
            End Select
        Else
            If Server.State = Orcas.WinsockStates.Listening Then
                If CD.Color <> Color.Black Then
                    LogColor(CD.Color.R, CD.Color.G, CD.Color.B, "Server: " & Output.Text)
                    Relay("/CC|" & CD.Color.R & "|" & CD.Color.G & "|" & CD.Color.B & "|Server: " & Output.Text)
                Else
                    Log("Server: " & Output.Text)
                    Relay("/C|Server: " & Output.Text)
                End If
            Else
                Log("Please intiate listening mode to chat.")
            End If
        End If
        Output.Text = Nothing
    End Sub
    Private Sub Output_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Output.KeyDown
        If e.KeyCode = Keys.Enter Then
            OutputEnter()
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub Server_StateChanged(ByVal sender As System.Object, ByVal e As Orcas.WinsockStateChangedEventArgs) Handles Server.StateChanged
        SSStatus.Text = "Status: " & e.New_State.ToString
        If e.New_State = 1 Then
            TSConnect.Text = "Disconnect"
            SSStatus.ForeColor = Color.Black : SSUptime.ForeColor = Color.Black
            ConnectionStart = Now : Uptime.Enabled = True : TSPort.Enabled = False
        Else
            TSConnect.Text = "Connect"
            SSStatus.ForeColor = Color.DarkGray : SSUptime.ForeColor = Color.DarkGray
            Uptime.Enabled = False : TSPort.Enabled = True
        End If
        If Server.State = 1 Then NI.Icon = My.Resources.ServerOn Else NI.Icon = My.Resources.ServerOff
    End Sub
    Private Sub ServerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TSRecord.Checked = My.Settings.Log
    End Sub
    Private Sub Input_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles Input.LinkClicked
        If MessageBox.Show("Clicking this link may harm your computer, proceed?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = 6 Then Process.Start(e.LinkText)
    End Sub
    Private Sub NI_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NI.MouseDoubleClick
        If WindowState <> FormWindowState.Minimized Then AppActivate(Text) Else ShowInTaskbar = True : WindowState = 0
    End Sub
    Private Sub ServerForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim CL As String = Environment.CommandLine.Split(" ").GetValue(Environment.CommandLine.Split(" ").Length - 1)
        If CL.ToLower = "-r" Then TSConnect.PerformClick()
        Application.DoEvents()
        TSExternal.Text = ExternalIP()
    End Sub
    Private Sub ServerForm_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If WindowState = 1 Then ShowInTaskbar = False
    End Sub
    Private Sub CM_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CM.Opening
        If UserList.SelectedItems.Count = 0 Then : e.Cancel = True : Exit Sub : End If
        Dim X As User = Users(Users.FindUser(UserList.SelectedItem.Replace(" (H)", "")))
        If IsStaff(X.Username, X.Signature) Then CMPromote.Text = "Demote" Else CMPromote.Text = "Promote"
    End Sub
    Private Sub CMWhisper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMWhisper.Click
        Output.Text = "/w " & UserList.SelectedItem.Replace(" (H)", "") & " " : Output.Focus() : Output.SelectionStart = Output.TextLength
    End Sub
    Private Sub CMQuickBan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMQuickBan.Click
        BanUser(Users.FindUser(UserList.SelectedItem.Replace(" (H)", "")))
        Log(UserList.SelectedItem.Replace(" (H)", "") & " was banned for 10 minutes.")
    End Sub
    Private Sub CMBan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBan.Click
        Try
            Dim I As Integer = InputBox("Duration in minutes to ban the user.", Text, 10) : If I < 1 Then I = 1
            BanUser(Users.FindUser(UserList.SelectedItem.Replace(" (H)", "")), I)
            Log(UserList.SelectedItem.Replace(" (H)", "") & " was banned for " & I & " minutes.")
        Catch : End Try
    End Sub
    Private Sub CMBanIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBanIP.Click
        Try
            Dim I As Integer = InputBox("Duration in minutes to ban the IP.", Text, 10) : If I < 1 Then I = 1
            BanIP(Users.FindUser(UserList.SelectedItem.Replace(" (H)", "")), I)
            Log(UserList.SelectedItem.Replace(" (H)", "") & "'s IP was banned for " & I & " minutes.")
        Catch : End Try
    End Sub
    Private Sub CMBoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBoot.Click
        BootUser(Users.FindUser(UserList.SelectedItem.Replace(" (H)", "")))
        Log(UserList.SelectedItem.Replace(" (H)", "") & " was booted.")
    End Sub
    Private Sub CMTrace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMTrace.Click
        Log(UserList.SelectedItem.Replace(" (H)", "") & ": " & Users(Users.FindUser(UserList.SelectedItem.Replace(" (H)", ""))).IP)
    End Sub
    Private Sub CMPromote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPromote.Click
        Dim X As User = Users(Users.FindUser(UserList.SelectedItem.Replace(" (H)", "")))
        If CMPromote.Text = "Promote" Then
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Staff.dat", X.Username & "`" & X.Signature & vbCrLf, True)
            Log(X.Username & " now has moderator status.")
            BootUser(X.UserID)
        Else
            Dim Output As String = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Staff.dat").Replace(X.Username & "`" & X.Signature, "")
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Staff.dat", Output, False)
            Log(X.Username & " has been demoted.") : BootUser(X.UserID)
        End If
    End Sub
    Private Sub TSClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSClear.Click
        If MessageBox.Show("Do you really want to clear all the chat?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = 6 Then Input.Clear()
    End Sub
    Private Sub TSRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSRecord.Click
        My.Settings.Log = TSRecord.Checked
    End Sub
    Private Sub Uptime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Uptime.Tick
        Dim X As TimeSpan = (Now.TimeOfDay - ConnectionStart.TimeOfDay)
        SSUptime.Text = "Uptime: " & X.Days & ":" & X.Hours & ":" & X.Minutes & ":" & X.Seconds
    End Sub
    Private Sub ScrubData()
        Dim Output As String = ""
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "/Users.dat") Then
            For Each X In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Users.dat"))
                If X <> "" Then Output += X.ToString & vbCrLf
            Next
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Users.dat", Output, False)
        End If
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "/Ban.dat") Then
            Output = ""
            For Each X In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Ban.dat"))
                If X <> "" Then Output += X.ToString & vbCrLf
            Next
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Ban.dat", Output, False)
        End If
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "/Staff.dat") Then
            Output = ""
            For Each X In Lines(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/Staff.dat"))
                If X <> "" Then Output += X.ToString & vbCrLf
            Next
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "/Staff.dat", Output, False)
        End If
    End Sub
    Private Sub ServerForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ScrubData()
    End Sub
    Private Sub TSConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSConnect.Click
        If Server.State <> 1 Then
            Server.LocalPort = CInt(TSPort.Text)
            Server.RemotePort = CInt(TSPort.Text)
            Server.Listen()
        Else
            Server.Close()
        End If
    End Sub
    Private Sub TSP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TSPort.KeyPress
        If Not Char.IsNumber(e.KeyChar) Then e.Handled = True
    End Sub
    Private Sub TSColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSColor.Click
        CD.ShowDialog()
    End Sub
#End Region
End Class