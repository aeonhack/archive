Imports System.Runtime.InteropServices

Public Class Main

    'TODO: Change Accept and Return buttons when appropriate.
    'TODO: Allow users to delete / edit manually added servers.
    'TODO: Show, hide, and/or populate user box in top right when appropriate.

#Region " State Helpers "

    Private Sub ShowLoginScreen()
        HiddenTab1.SelectedIndex = 0
    End Sub

    Private Sub ShowCreateAccount()
        'TODO: Populate Recaptcha.
        HiddenTab1.SelectedIndex = 3
    End Sub

    Private Sub ShowRecoverAccount()
        'TODO: Populate Recaptcha.
        HiddenTab1.SelectedIndex = 4
    End Sub

    Private Sub ShowServerBrowser()
        ClearServerBrowser()
        HiddenTab1.SelectedIndex = 1
        StartScanningServers()
    End Sub

    Private Sub ShowChannel()
        ClearChannel()
        HiddenTab1.SelectedIndex = 2
    End Sub

    Private Sub ClearServerBrowser()
        Label9.Text = "Channels Found: 0, Users Online: 0"

        While FlowLayoutPanel1.Controls.Count > 0
            Dim C As Control = FlowLayoutPanel1.Controls(0)
            FlowLayoutPanel1.Controls.RemoveAt(0)
            C.Dispose()
        End While
    End Sub

    Private Sub ClearChannel()
        RichTextBox1.Clear()
        TextBox3.Clear()

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

        Button1.Enabled = Not CheckBox2.Checked
        Button2.Enabled = Not CheckBox2.Checked
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'TODO: Remember login details..

        '_Username = TextBox1.Text
        '_Password.SetValue(CryptoHelper.HashPassword(_Username, TextBox2.Text))

        '_BypassMasterServer = CheckBox2.Checked

        LoadServerList()

        If CheckBox2.Checked Then
            ShowServerBrowser()
        Else
            'TODO: Authenticate with server
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
        'TODO: Connect to selected channel.
        ShowChannel()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'TODO: Add channel
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'TODO: Refresh server browser
    End Sub

    Private Sub FlowLayoutPanel1_SizeChanged(sender As Object, e As EventArgs) Handles FlowLayoutPanel1.SizeChanged
        For Each C As Control In FlowLayoutPanel1.Controls
            C.Width = FlowLayoutPanel1.Width - (SystemInformation.VerticalScrollBarWidth * 2)
        Next
    End Sub

#End Region

#Region " Channel "

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.A Then
            e.SuppressKeyPress = True
            TextBox3.SelectAll()
        ElseIf e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.SuppressKeyPress = True

            'TODO: Send message..
            RichTextBox1.AppendText(Color.Blue, "Aeonhack: ")
            RichTextBox1.AppendText(RichTextBox1.ForeColor, TextBox3.Text & Environment.NewLine)

            TextBox3.Clear()
        End If
    End Sub

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

    Private Sub HandleChatNotification()
        If Not IsForegroundWindow() Then Return

        NativeMethods.FlashWindow(Handle, True)
        'TODO: Play audio cue with SoundPlayer object.
        'TODO: Show toast notification
    End Sub

#End Region

End Class