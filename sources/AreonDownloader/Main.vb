Public Class Main
#Region " Global Variables "
    Dim Point As New System.Drawing.Point()
    Dim X, Y As Integer
    Dim CurrentFile As String
    Dim Divider As Double
    Dim Maximum As Long
    Dim Directory As String
    Dim Connected As Boolean
    Friend State As Integer
    Dim Files As String()
    Private WithEvents Downloader As WebFileDownloader
#End Region
#Region " GUI "
    Private Sub Button7_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button7.MouseDown
        Button7.BackgroundImage = My.Resources.YellowDOWN
    End Sub
    Private Sub Button7_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button7.MouseUp
        Button7.BackgroundImage = My.Resources.YellowUP
    End Sub
    Private Sub Button1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown
        Button1.BackgroundImage = My.Resources.YellowDOWN
    End Sub
    Private Sub Button1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseUp
        Button1.BackgroundImage = My.Resources.YellowUP
    End Sub
    Private Sub Button2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button2.MouseDown
        Button2.BackgroundImage = My.Resources.YellowDOWN
    End Sub
    Private Sub Button5_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button5.MouseDown
        Button5.BackgroundImage = My.Resources.YellowDOWN
    End Sub
    Private Sub Button3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button3.MouseDown
        Button3.BackgroundImage = My.Resources.YellowDOWN
    End Sub
    Private Sub Button2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button2.MouseUp
        Button2.BackgroundImage = My.Resources.YellowUP
    End Sub
    Private Sub Button5_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button5.MouseUp
        Button5.BackgroundImage = My.Resources.YellowUP
    End Sub
    Private Sub Button3_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button3.MouseUp
        Button3.BackgroundImage = My.Resources.YellowUP
    End Sub
    Private Sub Button6_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button6.MouseDown
        Button6.BackgroundImage = My.Resources.YellowDOWN
    End Sub
    Private Sub Button6_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button6.MouseUp
        Button6.BackgroundImage = My.Resources.YellowUP
    End Sub
    Private Sub Main_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = MouseButtons.Left Then
            Point = Control.MousePosition
            Point.X = Point.X - (X)
            Point.Y = Point.Y - (Y)
            Me.Location = Point
        End If
    End Sub
    Private Sub Main_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        X = Control.MousePosition.X - Me.Location.X
        Y = Control.MousePosition.Y - Me.Location.Y
    End Sub
    Private Sub ListView1_ItemChecked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles ListView1.ItemChecked
        If ListView1.CheckedItems.Count > 0 And Directory <> Nothing Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub
#End Region
#Region " Buttons Functions "
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Hide()
        NotifyIcon1.Visible = True
    End Sub
    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        NotifyIcon1.Visible = False
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Process.Start("http://www.youtube.com/aeonhack")
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Help.ShowDialog()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button7.Enabled = False
        For Each File As ListViewItem In ListView1.CheckedItems
            Try
                If State = 50 Then Exit Sub
                My.Application.DoEvents()
                Downloader = New WebFileDownloader
                CurrentFile = File.Text
                Label1.Text = "Status: Downloading " & CurrentFile
                Downloader.DownloadFileWithProgress(File.Tag, Directory & "\" & File.Text)
            Catch EX As Exception
                MessageBox.Show(EX.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        Next
        Button1.Enabled = True
        Button7.Enabled = True
        Label1.Text = "Status: Download Complete"
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath = Nothing Then Exit Sub
        Directory = FolderBrowserDialog1.SelectedPath
        If ListView1.CheckedItems.Count > 0 Then Button1.Enabled = True
    End Sub
#End Region
#Region " Other Functions "
    Private Sub Main_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Label1.Text = "Status: Verifying Connection"
        Worker.RunWorkerAsync()
        RectangleShape1.Width = 0
        For I = 1 To 10000
            If State >= 1 Then
                RectangleShape1.Width = 228
                Exit For
            End If
            My.Application.DoEvents()
            RectangleShape1.Width = Math.Round((I / 43.89), 0)
            Threading.Thread.Sleep(1)
        Next
    End Sub
    Private Sub GetFiles()
        Dim Browser As New System.Net.WebClient
        On Error Resume Next
        Files = Browser.DownloadString("http://gymjunnky.com/downloads/dowloads-our/Aeon/AreonDownloader.txt").Split(Constants.vbLf.ToCharArray)
        State = 2
    End Sub
    Private Function ValidateConnection() As Boolean
        Dim Request As System.Net.WebRequest = System.Net.WebRequest.Create("http://www.Microsoft.com/")
        Request.Timeout = 10000
        Try
            Dim Response As System.Net.WebResponse = Request.GetResponse
            Response.Close()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function
    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Select Case State
            Case 0
                Connected = ValidateConnection()
                State = 1
            Case 1
                GetFiles()
            Case 2
                State = 3
        End Select
    End Sub
    Private Sub Worker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Select Case State
            Case 1
                Select Case Connected
                    Case True
                        Label1.Text = "Status: Retrieving Files"
                        Button6.Enabled = True
                        RectangleShape1.Width = 0
                        Worker.RunWorkerAsync()
                        For I = 1 To 10000
                            If State >= 2 Then
                                RectangleShape1.Width = 228
                                Exit For
                            End If
                            If I = 10000 Then
                                Worker.CancelAsync()
                                Label1.Text = "Status: Failed File Retrieval"
                                RectangleShape1.Width = 228
                                Exit For
                            End If
                            RectangleShape1.Width = Math.Round((I / 43.89), 0)
                            My.Application.DoEvents()
                            Threading.Thread.Sleep(1)
                        Next
                    Case False
                        Label1.Text = "Status: Disconnected"
                End Select
            Case 2
                If Files.Length > 0 Then
                    ListView1.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.None)
                    ListView1.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.None)
                    For Each File In Files
                        My.Application.DoEvents()
                        Dim Item As New ListViewItem
                        Item.Tag = File.Split("*").GetValue(1)
                        Item.Text = File.Split("*").GetValue(0)
                        Item.SubItems.Add(File.Split("*").GetValue(2))
                        Item.ImageIndex = 0
                        ListView1.Items.Add(Item)
                    Next
                    Label1.Text = "Status: Files Successfully Retrieved"
                Else
                    Label1.Text = "Status: No Files Found"
                End If
        End Select
    End Sub
    Private Sub Main_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        State = 50
        Me.Dispose()
    End Sub
    Private Sub _Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles Downloader.FileDownloadSizeObtained
        RectangleShape1.Width = 0
        Maximum = iFileSize
        Divider = Maximum / 228
    End Sub
    Private Sub Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles Downloader.AmountDownloadedChanged
        RectangleShape1.Width = Math.Round(iNewProgress / Divider, 0)
        Label1.Text = "Status: " & CurrentFile & " " & WebFileDownloader.FormatFileSize(iNewProgress) & "/" & WebFileDownloader.FormatFileSize(Maximum) & "(" & Math.Round((100 / Maximum) * iNewProgress, 0) & "%)"
        My.Application.DoEvents()
    End Sub
    Private Sub Downloader_FileDownloadComplete() Handles Downloader.FileDownloadComplete
        RectangleShape1.Width = 228
        Label1.Text = "Status: Complete"
    End Sub
#End Region
End Class
