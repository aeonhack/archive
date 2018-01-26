Public Class Notice
    Private W As Size = My.Computer.Screen.WorkingArea.Size
    Sub New(ByVal notice As String)
        InitializeComponent()
        Label1.Text = notice
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex <> 0 Then
            Dim T = Val(ComboBox1.SelectedItem)
            If ComboBox1.SelectedIndex > 2 Then T *= 60
            Main.ListView1.Items.Add(Now.AddMinutes(T).ToString("MM/dd hh:mm tt")).SubItems.Add(Label1.Text)
            Main.SaveContent()
        End If
        Close()
    End Sub
    Private Sub Notice_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Location = New Point(W.Width - Width, W.Height - Height)
        Dim T As New Threading.Thread(AddressOf Sound)
        T.Start()
    End Sub
    Private Sub Sound()
        Console.Beep(1700, 300)
        Console.Beep(1700, 300)
        Threading.Thread.Sleep(500)
        Console.Beep(1700, 300)
        Console.Beep(1700, 300)
    End Sub
End Class