Imports System.Runtime.InteropServices
Imports System.Net

Public Class ClientMain

#Region " Fake ScrollBars "

    Private ServerScroll, UserScroll As VScrollBar

    Private Sub InitializeFakeScrolls()
        ServerScroll = New VScrollBar()
        TabPage1.Controls.Add(ServerScroll)

        ServerScroll.Enabled = False
        ServerScroll.Height = FlowLayoutPanel1.Height - 2
        ServerScroll.Anchor = DirectCast(11, AnchorStyles)
        ServerScroll.Location = New Point(FlowLayoutPanel1.Right - ServerScroll.Width - 1, FlowLayoutPanel1.Top + 1)
        ServerScroll.BringToFront()

        UserScroll = New VScrollBar()
        TabPage3.Controls.Add(UserScroll)

        UserScroll.Enabled = False
        UserScroll.Height = FlowLayoutPanel2.Height
        UserScroll.Anchor = DirectCast(11, AnchorStyles)
        UserScroll.Location = New Point(TabPage3.Width - UserScroll.Width - 1, 2)
        UserScroll.BringToFront()
    End Sub

#End Region

    Private Sub ClientMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        InitializeFakeScrolls()
        InitializeClient()
    End Sub

    'TODO: Change Accept and Return buttons when appropriate.
    'TODO: Allow users to delete / edit manually added servers.
    'TODO: Show, hide, and/or populate user box in top right when appropriate.
    'TODO: Run some validation on server hosts

#Region " State Helpers "

    Public Sub ShowLoginScreen()
        Panel1.Visible = False
        Label16.Visible = True

        HiddenTab1.SelectedIndex = 0
    End Sub

    Public Sub ShowCreateAccount()
        ReCaptcha1.GenerateCaptcha()
        HiddenTab1.SelectedIndex = 3
    End Sub

    Public Sub ShowRecoverAccount()
        ReCaptcha2.GenerateCaptcha()
        HiddenTab1.SelectedIndex = 4
    End Sub

    Public Sub ShowServerBrowser()
        Label16.Visible = False
        Panel1.Visible = True
        Label8.Text = String.Format("Hi {0},", Username)

        LinkLabel2.Enabled = Not BypassMaster

        ClearServerBrowser()
        HiddenTab1.SelectedIndex = 1
        StartScanningServers()
    End Sub

    Public Sub ShowChannel()
        ClearChannel()
        HiddenTab1.SelectedIndex = 2
    End Sub

    Public Sub ClearServerBrowser()
        Label9.Text = "Channels Found: 0, Users Online: 0"

        While FlowLayoutPanel1.Controls.Count > 0
            Dim C As Control = FlowLayoutPanel1.Controls(0)
            FlowLayoutPanel1.Controls.RemoveAt(0)
            C.Dispose()
        End While
    End Sub

    Public Sub ClearChannel()
        RichTextBox1.Clear()
        TextBox3.Clear()
        ClearUserList()
    End Sub

    Private Sub ClearUserList()
        While FlowLayoutPanel2.Controls.Count > 0
            Dim C As Control = FlowLayoutPanel2.Controls(0)
            FlowLayoutPanel2.Controls.RemoveAt(0)
            C.Dispose()
        End While
    End Sub

#End Region

#Region " Login Screen "

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        'TODO: Warn user that they will not have access to global stats or public servers.
        TextBox2.Clear()
        TextBox2.Enabled = Not CheckBox2.Checked

        Button1.Enabled = Not CheckBox2.Checked
        Button2.Enabled = Not CheckBox2.Checked
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Username = SanitizeUsername(TextBox1.Text)
        Password = CryptoHelper.HashPassword(Username, TextBox2.Text)

        BypassMaster = CheckBox2.Checked

        If Not ValidateUsername(Username) Then
            MessageBox.Show("Username must be between 1 and 14 characters long.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        LoadServerList()
        If BypassMaster Then
            ShowServerBrowser()
        Else
            'TODO: Authenticate with master server
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ShowCreateAccount()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ShowRecoverAccount()
    End Sub

#End Region

#Region " Server Browser "

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Server As ServerPreview = GetSelectedServer()
        If Server Is Nothing Then Return

        If BypassMaster AndAlso Not Server.BypassMaster Then
            MessageBox.Show("The channel you have selected requires authentication with the master server.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not Server.Compatible Then
            MessageBox.Show("The channel you have selected is not compatible with the client you are using.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        StopScanningServers()
        Client.Connect(Server.EndPoint.Address.ToString(), CUShort(Server.EndPoint.Port))

        ShowChannel()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim T As New AddChannel()
        If Not T.ShowDialog() = Windows.Forms.DialogResult.OK Then Return

        If Not Servers.Contains(T.EndPoint) Then
            Servers.Add(T.EndPoint)
        End If

        SaveServerList()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        StopScanningServers()
        ShowServerBrowser()
    End Sub

    Private Sub FlowLayoutPanel1_SizeChanged(sender As Object, e As EventArgs) Handles FlowLayoutPanel1.SizeChanged
        If Not HiddenTab1.SelectedIndex = 1 Then Return
        ServerScroll.Visible = Not FlowLayoutPanel1.VerticalScroll.Visible

        For Each C As Control In FlowLayoutPanel1.Controls
            C.Width = FlowLayoutPanel1.Width - (SystemInformation.VerticalScrollBarWidth * 2)
        Next
    End Sub

    Public Sub AddToServerBrowser(endPoint As IPEndPoint, protocol As Version, bypass As Boolean, name As String, motto As String, users As UShort, maxUsers As UShort)
        Dim T As New ServerPreview()
        T.EndPoint = endPoint
        T.ServerProtocol = protocol
        T.BypassMaster = bypass
        T.ServerName = name
        T.ServerMotto = motto
        T.Users = users
        T.MaxUsers = maxUsers

        FlowLayoutPanel1.Controls.Add(T)

        Label9.Text = String.Format("Channels Found: {0}, Users Online: {1}", FlowLayoutPanel1.Controls.Count, UsersOnline)
    End Sub

    Private Function GetSelectedServer() As ServerPreview
        For Each C As Control In FlowLayoutPanel1.Controls
            If TypeOf C Is ServerPreview AndAlso DirectCast(C, ServerPreview).Selected Then
                Return DirectCast(C, ServerPreview)
            End If
        Next

        Return Nothing
    End Function

#End Region

#Region " Channel "

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.A Then
            e.SuppressKeyPress = True
            TextBox3.SelectAll()
        ElseIf e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.SuppressKeyPress = True

            TextBox3.Text = SanitizeMultilineMessage(TextBox3.Text).Trim()
            If TextBox3.TextLength = 0 Then Return

            SendChanelChatPacket(TextBox3.Text)
            AppendChatMessage(Username, TextBox3.Text)

            TextBox3.Clear()
        End If
    End Sub

    Public Sub AppendChatMessage(name As String, message As String)
        RichTextBox1.AppendText(Color.Blue, String.Format("{0}: ", name))
        RichTextBox1.AppendText(RichTextBox1.ForeColor, message & Environment.NewLine)
    End Sub

    Public Sub InvalidateUserBrowser()
        SyncLock FlowLayoutPanel2.Controls
            ClearUserList()

            For Each U As User In Users.Values
                FlowLayoutPanel2.Controls.Add(New UserPreview(U))
            Next
        End SyncLock
    End Sub

    Public Sub AddToUserBrowser(user As User)
        FlowLayoutPanel2.Controls.Add(New UserPreview(user))
    End Sub

    Public Sub RemoveFromUserBrowser(id As Integer)
        SyncLock FlowLayoutPanel2.Controls
            For Each C As Control In FlowLayoutPanel2.Controls
                If TypeOf C Is UserPreview AndAlso DirectCast(C, UserPreview).User.ID = id Then
                    FlowLayoutPanel2.Controls.Remove(C)
                    Return
                End If
            Next
        End SyncLock
    End Sub

    Private Function GetSelectedUser() As UserPreview
        For Each C As Control In FlowLayoutPanel2.Controls
            If TypeOf C Is UserPreview AndAlso DirectCast(C, UserPreview).Selected Then
                Return DirectCast(C, UserPreview)
            End If
        Next

        Return Nothing
    End Function

#End Region

#Region " Create Account "

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'TODO: Create account.
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ShowLoginScreen()
    End Sub

#End Region

#Region " Recover Account "

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'TODO: Recover account
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ShowLoginScreen()
    End Sub

#End Region

#Region " Notifications "

    Private Function IsForegroundWindow() As Boolean
        Return Handle = NativeMethods.GetForegroundWindow()
    End Function

    Public Sub HandleChatNotification()
        If Not IsForegroundWindow() Then Return

        NativeMethods.FlashWindow(Handle, True)
        'TODO: Play audio cue with SoundPlayer object.
        'TODO: Show toast notification
    End Sub

#End Region

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        StopScanningServers()
        Client.Disconnect()
        ShowLoginScreen()
    End Sub

    Private Sub FlowLayoutPanel2_SizeChanged(sender As Object, e As EventArgs) Handles FlowLayoutPanel2.SizeChanged
        If Not HiddenTab1.SelectedIndex = 2 Then Return
        UserScroll.Visible = Not FlowLayoutPanel2.VerticalScroll.Visible
    End Sub

End Class