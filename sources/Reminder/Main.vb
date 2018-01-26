Public Class Main
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.None)
        NotifyIcon1.Text = Text
        LoadContent()
    End Sub
    Private Sub Form1_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        ListView1.Columns(1).Width = ListView1.Width - 30 - ListView1.Columns(0).Width
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim T As New TimeSpan(NumericUpDown3.Value, NumericUpDown2.Value, NumericUpDown1.Value, 0)
        If DateTimePicker1.Checked Then T = DateTimePicker1.Value - Now
        If T.TotalSeconds > 0 And TextBox1.Text <> "" Then
            ListView1.Items.Add(Now.Add(T).ToString("MM/dd hh:mm tt")).SubItems.Add(TextBox1.Text)
            SaveContent()

            NumericUpDown1.Value = 0
            NumericUpDown2.Value = 0
            NumericUpDown3.Value = 0
            TextBox1.Clear()
        End If
    End Sub

    Dim O As Boolean
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        For Each I As ListViewItem In ListView1.Items
            If Date.Parse(I.Text.Insert(5, "/" & Now.Year)) < Now Then
                Dim N As New Notice(I.SubItems(1).Text)
                N.Show()
                I.Remove()
                O = True
            End If
        Next
        If O Then
            SaveContent()
            O = False
        End If
    End Sub

    Sub SaveContent()
        Dim T As New System.Text.StringBuilder
        For I = 0 To ListView1.Items.Count - 1
            T.Append(ListView1.Items(I).Text & "ˌ" & ListView1.Items(I).SubItems(1).Text & "̩")
        Next
        IO.File.WriteAllText(Application.StartupPath & "\data.dat", T.ToString)
    End Sub
    Sub LoadContent()
        Dim T = Application.StartupPath & "\data.dat"
        If IO.File.Exists(t) Then
            Dim U = IO.File.ReadAllText(T)
            If U.Contains("̩") Then
                For Each I In U.Split("̩")
                    If I.Contains("ˌ") Then
                        ListView1.Items.Add(I.Split("ˌ")(0)).SubItems.Add(I.Split("ˌ")(1))
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        e.Cancel = ListView1.SelectedItems.Count = 0
    End Sub
    Private Sub Main_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.ShowBalloonTip(1500)
        End If
    End Sub
    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Application.Exit()
    End Sub
    Private Sub NotifyIcon1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then Return
        Show()
        NotifyIcon1.Visible = False
    End Sub

    Private Sub ListView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode <> Keys.Delete Then Return
        If MessageBox.Show("Do you really want to delete the selected item(s)?", Text, MessageBoxButtons.YesNo) = DialogResult.No Then Return
        For Each I As ListViewItem In ListView1.SelectedItems
            I.Remove()
        Next
        SaveContent()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        NumericUpDown1.Enabled = Not DateTimePicker1.Checked
        NumericUpDown2.Enabled = Not DateTimePicker1.Checked
        NumericUpDown3.Enabled = Not DateTimePicker1.Checked
    End Sub
End Class
