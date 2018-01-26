Public Class Form1

    Private Active As Boolean
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Text = Now.ToString("h:mm tt")

        If Active And Now >= DateTimePicker1.Value Then
            Button1.Enabled = True
            My.Computer.Audio.Play(My.Resources.Sound, 1)
        End If

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Active = Not Active

        Timer1.Enabled = Active
        Button1.Enabled = Active
        DateTimePicker1.Enabled = Not Active

        Button2.Text = If(Active, "Disable", "Enable")
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        DateTimePicker1.Value = DateTimePicker1.Value.AddMinutes(10)
        Button1.Enabled = False
    End Sub
    Private Sub Form1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        NotifyIcon1.Visible = (WindowState = 1)
        ShowInTaskbar = Not (WindowState = 1)
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NotifyIcon1.Icon = Icon
        NotifyIcon1.Text = Text
    End Sub
    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        WindowState = 0
    End Sub
End Class