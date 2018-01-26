Public Class Main
    Friend HistoryC As New Collection
    Private Sub Shockwave_FSCommand(ByVal sender As System.Object, ByVal e As AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent)
        HistoryC.Add("Command$" & e.command & " = " & e.args)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        CurFrame.Text = "Current Frame  (" & Shockwave.CurrentFrame & ")"
        MaxFrames.Text = "Maximum Frames  (" & Shockwave.TotalFrames & ")"
    End Sub
    Private Sub CloseMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseMS.Click
        Me.Close()
    End Sub
    Private Sub OpenMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenMS.Click
        Open.ShowDialog()
    End Sub
    Private Sub HistoryMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HistoryMS.Click
        If History.Visible = False Then
            History.Show()
        Else
            History.BringToFront()
            History.Activate()
        End If
    End Sub
    Private Sub ReloadMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadMS.Click
        Dim Movie As String = Shockwave.Movie
        Shockwave.Hide()
        Shockwave.Rewind()
        Shockwave.Stop()
        Shockwave.Show()
        Shockwave.Movie = Movie
        Shockwave.Play()
        HistoryC.Add("Reloaded$" & Movie)
    End Sub
    Private Sub SetFrameMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetFrameMS.Click
        If Frame.Visible = False Then
            Frame.Show()
        Else
            Frame.BringToFront()
            Frame.Activate()
        End If
    End Sub
    Private Sub CurrentFrame() Handles Shockwave.OnReadyStateChange
        If Shockwave.TotalFrames > 0 Then
            ReloadMS.Enabled = True
        Else
            ReloadMS.Enabled = False
        End If
    End Sub
    Public Sub SetView(ByVal Type As Integer)
        Shockwave.ScaleMode = Type
        Select Case Type
            Case 0
                ToolStripMenuItem1.Checked = True
                ToolStripMenuItem2.Checked = False
                ExactFitToolStripMenuItem.Checked = False
                CenteredToolStripMenuItem.Checked = False
            Case 1
                ToolStripMenuItem1.Checked = False
                ToolStripMenuItem2.Checked = True
                ExactFitToolStripMenuItem.Checked = False
                CenteredToolStripMenuItem.Checked = False
            Case 2
                ToolStripMenuItem1.Checked = False
                ToolStripMenuItem2.Checked = False
                ExactFitToolStripMenuItem.Checked = True
                CenteredToolStripMenuItem.Checked = False
            Case 3
                ToolStripMenuItem1.Checked = False
                ToolStripMenuItem2.Checked = False
                ExactFitToolStripMenuItem.Checked = False
                CenteredToolStripMenuItem.Checked = True
        End Select
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        SetView(0)
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        SetView(1)
    End Sub
    Private Sub ExactFitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactFitToolStripMenuItem.Click
        SetView(2)
    End Sub
    Private Sub CenteredToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CenteredToolStripMenuItem.Click
        SetView(3)
    End Sub
    Private Sub ToolStripDropDownButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton4.Click
        Select Case Me.FormBorderStyle
            Case Windows.Forms.FormBorderStyle.Sizable
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.WindowState = FormWindowState.Maximized
                ToolStripDropDownButton4.Text = "Normal"
            Case Windows.Forms.FormBorderStyle.None
                Me.WindowState = FormWindowState.Normal
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                ToolStripDropDownButton4.Text = "Full Screen"
        End Select
    End Sub
    Private Sub ManipulationMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManipulationMS.Click
        If Manipulation.Visible = False Then
            Manipulation.Show()
        Else
            Manipulation.BringToFront()
            Manipulation.Activate()
        End If
    End Sub
    Private Sub ToolStripDropDownButton1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton1.MouseEnter
        ToolStripDropDownButton1.PerformClick()
    End Sub
End Class
