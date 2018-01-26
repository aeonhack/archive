Imports Microsoft.VisualBasic.FileIO.FileSystem
Public Class Form1
#Region " Navigation "
    Private Sub Navigate(ByVal Path As String)
        If Not DirectoryExists(Path) Then Exit Sub
        LV.Items.Clear()
        IL.Images.Clear()
        IL.Images.Add(My.Resources.Folder)
        IL.Images.Add("B", My.Resources.Blank)
        IL.Images.Add(".exe", My.Resources.Executable)
        On Error Resume Next
        LV.Visible = False
        Cursor.Current = Cursors.WaitCursor
        ProgressBar1.Visible = True
        ProgressBar1.Maximum = GetFiles(Path).Count + GetDirectories(Path).Count
        For Each F In GetDirectories(Path)
            LV.Items.Add(GetDirectoryInfo(F).Name, 0)
            ProgressBar1.Value += 1
            ProgressBar1.Invalidate()
        Next
        For Each F In GetFiles(Path)
            If IL.Images.IndexOfKey(GetFileInfo(F).Extension) < 0 Then
                IL.Images.Add(GetFileInfo(F).Extension, Icon.ExtractAssociatedIcon(F))
            End If
            If GetFileInfo(F).Extension = Nothing Then
                LV.Items.Add(GetName(F), "B")
            Else
                LV.Items.Add(GetName(F), GetFileInfo(F).Extension)
            End If
            ProgressBar1.Value += 1
            ProgressBar1.Invalidate()
        Next
        Address.Text = Path
        ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        LV.Visible = True
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Browse.Click
        FB.ShowDialog() : Navigate(FB.SelectedPath)
    End Sub
    Private Sub Go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Go.Click
        Navigate(Address.Text.Trim)
    End Sub
    Private Sub Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Up.Click
        If Address.TextLength > 3 Then Navigate(GetParentPath(Address.Text.Trim))
    End Sub
    Private Sub LV_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LV.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            On Error Resume Next
            If LV.SelectedItems(0).ImageKey = Nothing Then
                If Address.Text.EndsWith("\") Then Address.Text = Address.Text.Remove(Address.TextLength - 1)
                Navigate(Address.Text & "\" & LV.SelectedItems(0).Text)
            Else
                Process.Start(Address.Text & "\" & LV.SelectedItems(0).Text)
            End If
        End If
    End Sub
#End Region
#Region " Tools "
    Private Sub RenameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        LV.SelectedItems(0).BeginEdit()
    End Sub
    Private Sub LV_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles LV.AfterLabelEdit
        If e.Label = Nothing Then e.CancelEdit = True : Exit Sub
        Try
            If LV.Items(e.Item).ImageKey = Nothing Then
                RenameDirectory(Address.Text & "\" & LV.SelectedItems(0).Text, e.Label)
            Else
                RenameFile(Address.Text & "\" & LV.SelectedItems(0).Text, e.Label)
            End If
        Catch : e.CancelEdit = True : End Try
    End Sub
    Private Sub CloneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloneToolStripMenuItem.Click
        For Each Item In LV.SelectedItems
            Dim Current As String = Address.Text & "\" & Item.Text
            Dim Cloned As String = Current.Insert(Current.Length - GetFileInfo(Current).Extension.Length, " Clone")
            Try
                If Item.ImageKey = Nothing Then
                    CopyDirectory(Current, Cloned)
                    LV.Items.Add(GetName(Cloned), 0)
                Else
                    CopyFile(Current, Cloned)
                    LV.Items.Add(GetName(Cloned), GetFileInfo(Current).Extension)
                End If
            Catch : End Try
        Next
    End Sub
    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Go.PerformClick()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If LV.SelectedItems.Count = 0 Then Exit Sub
        If MessageBox.Show("Please confirm the deletion process.", Me.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        For Each Item In LV.SelectedItems
            Try
                If Item.ImageKey = Nothing Then
                    DeleteDirectory(Address.Text & "\" & Item.Text, FileIO.DeleteDirectoryOption.DeleteAllContents)
                Else
                    DeleteFile(Address.Text & "\" & Item.Text, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
                Item.Remove()
            Catch : End Try
        Next
    End Sub
#End Region
    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Navigate(FileIO.SpecialDirectories.MyDocuments)
    End Sub
    Private Sub CM_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CM.Opening
        If LV.SelectedItems.Count = 0 Then e.Cancel = True
    End Sub
End Class
