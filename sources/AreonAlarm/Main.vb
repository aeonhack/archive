Public Class Main
#Region " Declerations "
    Dim State As Boolean
    Dim Ringing As Boolean
#End Region
#Region " Alarm Functions "
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Dim Time As String = TimeOfDay
        Dim Alarm As String = AlarmHour.Text & ":" & AlarmMin.Text & " " & AlarmND.Text
        CurrentTime.Text = Replace(Time, Time.Substring(Time.Length - 6), "") & " " & Time.Substring(Time.Length - 2)
        If State And CurrentTime.Text = Alarm Then Ringing = True
        If Ringing Then Ring()
    End Sub
    Private Sub Ring()
        My.Computer.Audio.Play(My.Resources.Ring, AudioPlayMode.Background)
    End Sub
#End Region
#Region " Interface "
    Private Sub Form1_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Visible = False
            AlarmIcon.Visible = True
        End If
    End Sub
    Private Sub NotifyIcon1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmIcon.DoubleClick
        AlarmIcon.Visible = False
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Time As String = TimeOfDay
        CurrentTime.Text = Replace(Time, Time.Substring(Time.Length - 6), "") & " " & Time.Substring(Time.Length - 2)
        AlarmHour.Text = CurrentTime.Text.Split(":").GetValue(0)
        AlarmMin.Text = CurrentTime.Text.Split(":").GetValue(1).ToString.Substring(0, 2)
        Select Case Time.Substring(Time.Length - 2)
            Case "AM"
                AlarmND.SelectedIndex = 0
            Case "PM"
                AlarmND.SelectedIndex = 1
        End Select
    End Sub
    Private Sub Toggle(ByVal Setting As Boolean)
        Select Case Setting
            Case True
                AlarmSet.Text = "Turn On"
                MSToggle.Text = "Turn On"
                AlarmHour.Enabled = True
                AlarmMin.Enabled = True
                AlarmND.Enabled = True
                AlarmIcon.Text = "Alarm: Not Set"
                State = False
                Ringing = False
            Case False
                AlarmSet.Text = "Turn Off"
                MSToggle.Text = "Turn Off"
                AlarmHour.Enabled = False
                AlarmMin.Enabled = False
                AlarmND.Enabled = False
                AlarmIcon.Text = "Alarm: " & AlarmHour.Text & ":" & AlarmMin.Text & " " & AlarmND.Text
                State = True
        End Select
    End Sub
    Private Sub AlarmSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmSet.Click
        Toggle(State)
    End Sub
    Private Sub MSToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MSToggle.Click
        Toggle(State)
    End Sub
    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MSClose.Click
        Me.Close()
    End Sub
#End Region
#Region " Restrictions "
    Private Sub AlarmHour_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmHour.Leave
        If AlarmHour.Text = Nothing Then AlarmHour.Text = "12"
        If AlarmHour.Text > 12 Then AlarmHour.Text = "12"
        If AlarmHour.Text < 1 Then AlarmHour.Text = "1"
    End Sub
    Private Sub AlarmMin_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmMin.Leave
        If AlarmMin.Text = Nothing Then AlarmMin.Text = "00"
        If AlarmMin.Text > 59 Then AlarmMin.Text = "59"
        If AlarmMin.Text.Length = 1 Then AlarmMin.Text = "0" & AlarmMin.Text
    End Sub
    Private Sub AlarmHour_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles AlarmHour.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then
            e.Handled = True
        End If
    End Sub
    Private Sub AlarmMin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles AlarmMin.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then
            e.Handled = True
        End If
    End Sub
    Private Sub AlarmND_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles AlarmND.KeyPress
        e.Handled = True
    End Sub
#End Region
End Class
