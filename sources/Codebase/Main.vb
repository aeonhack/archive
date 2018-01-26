Imports System.IO
Public Class Main

    '+-------------------------------+
    '+ +---------------------------+ |
    '| |      CodeBase V1.3        | |
    '| |      Creator: StarZ       | |
    '| |      Editor: Aeonhack     | |
    '| |                           | |
    '| | http://www.LeetCoders.org | |
    '| +---------------------------+ |
    '+-------------------------------+

#Region " Declarations "
    Dim Path As String = Application.StartupPath & "\Documents"
    Class Document
        Public Tag, Name, FullName As String
        Sub New(ByVal path As String)
            Dim T = My.Computer.FileSystem.GetName(path)
            If T.Contains("-") Then
                Tag = T.Substring(0, T.IndexOf("-")).Trim
                Name = T.Substring(T.IndexOf("-") + 1, T.Length - T.IndexOf("-") - 1).Trim
            Else
                Tag = "Other"
                Name = T
            End If
            Name = Name.Substring(0, Name.Length - 4)
            FullName = path
        End Sub
    End Class
#End Region

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists(Path) Then
            For Each F In My.Computer.FileSystem.GetFiles(Path, 3, "*.txt")
                CreateDocument(F)
            Next
        Else
            My.Computer.FileSystem.CreateDirectory(Path)
        End If
        Calculate()
        FSW1.Path = Path
    End Sub
    Private Sub ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CType(sender, ListView).SelectedItems.Count > 0 Then
            Dim T = CType(TabControl1.SelectedTab.Controls(0), SplitContainer).Panel2.Controls(0)
            T.Text = File.ReadAllText(sender.SelectedItems(0).Tag.FullName)
        End If
    End Sub
    Private Sub TextBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.A Then
            sender.SelectAll()
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        About.ShowDialog()
    End Sub
    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        Using O As New OpenFileDialog
            O.Multiselect = True
            If O.ShowDialog = 1 Then
                For Each F In O.FileNames
                    My.Computer.FileSystem.CopyFile(F, Path & "\" & My.Computer.FileSystem.GetName(F), FileIO.UIOption.AllDialogs)
                Next
            End If
        End Using
    End Sub
    Private Sub WordWrapToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordWrapToolStripMenuItem.Click
        For Each T In TabControl1.TabPages
            CType(CType(T.Controls(0), SplitContainer).Panel2.Controls(0), TextBox).WordWrap = WordWrapToolStripMenuItem.Checked
        Next
    End Sub
    Private Sub StatusStripToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusStripToolStripMenuItem.Click
        SS1.Visible = StatusStripToolStripMenuItem.Checked
    End Sub
    Private Sub DocumentFontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentFontToolStripMenuItem.Click
        Using F As New FontDialog
            If F.ShowDialog = 6 Then
                For Each T In TabControl1.TabPages
                    CType(CType(T.Controls(0), SplitContainer).Panel2.Controls(0), TextBox).Font = F.Font
                Next
            End If
        End Using
    End Sub
    Private Sub ListFontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListFontToolStripMenuItem.Click
        Using F As New FontDialog
            If F.ShowDialog = 6 Then
                For Each T In TabControl1.TabPages
                    FindPageList(T.Text).Font = F.Font
                Next
            End If
        End Using
    End Sub
    Sub Calculate()
        Dim Total As Integer
        For Each T In TabControl1.TabPages
            Total += FindPageList(T.Text).Items.Count
        Next
        TSL1.Text = String.Format("{0} Documents     {1} Categories", Total, TabControl1.TabPages.Count)
    End Sub

#Region " Document "
    Sub CreateDocument(ByVal path As String)
        Dim T = New Document(path)
        If FindPage(T.Tag) Is Nothing Then CreatePage(T.Tag)
        Dim I As New ListViewItem(T.Name)
        I.Tag = T
        FindPageList(T.Tag).Items.Add(I)
    End Sub
    Sub RemoveDocument(ByVal path As String)
        Dim I = FindDocumentItem(path)
        Dim V = CType(I.Tag.Tag, String)
        I.Remove()
        If FindPageList(V).Items.Count = 0 Then TabControl1.Controls.Remove(FindPage(V))
    End Sub
    Sub RenameDocument(ByVal path As String, ByVal newpath As String)
        Dim I = FindDocumentItem(path), T = New Document(newpath)
        If I.Tag.Tag <> T.Tag Then
            CreateDocument(newpath)
            Dim V = CType(I.Tag.Tag, String)
            I.Remove()
            If FindPageList(V).Items.Count = 0 Then TabControl1.Controls.Remove(FindPage(V))
        Else
            I.Tag = T
            I.Text = T.Name
        End If
    End Sub
    Function FindDocumentItem(ByVal path As String) As ListViewItem
        Dim T = New Document(path)
        For Each I In CType(CType(FindPage(T.Tag).Controls(0), SplitContainer).Panel1.Controls(0), ListView).Items
            If I.Text.ToLower = T.Name.ToLower Then Return I
        Next
        Return Nothing
    End Function
#End Region
#Region " Page "
    Function FindPage(ByVal tag As String) As TabPage
        For Each T In TabControl1.TabPages
            If T.Text.ToLower = tag.ToLower Then Return T
        Next
        Return Nothing
    End Function
    Function FindPageList(ByVal tag As String) As ListView
        For Each T In TabControl1.TabPages
            If T.Text.ToLower = tag.ToLower Then Return T.Controls(0).Panel1.Controls(0)
        Next
        Return Nothing
    End Function
    Sub CreatePage(ByVal tag As String)
        Dim T As New TabPage(tag)

        Dim V As New SplitContainer
        V.Dock = 5
        V.FixedPanel = FixedPanel.Panel1
        V.IsSplitterFixed = True

        Dim I As New ListView
        I.Dock = 5
        I.View = 3
        I.BorderStyle = 0
        I.Font = New Font("Verdana", 8)
        I.ContextMenuStrip = CMS1
        AddHandler I.SelectedIndexChanged, AddressOf ListView_SelectedIndexChanged
        V.Panel1.Controls.Add(I)

        Dim O As New TextBox
        O.Multiline = True
        O.Dock = 5
        O.BorderStyle = 0
        O.Font = New Font("Verdana", 8)
        O.ScrollBars = 2
        AddHandler O.KeyDown, AddressOf TextBox_KeyDown
        V.Panel2.Controls.Add(O)


        T.Controls.Add(V)
        V.SplitterDistance = 200
        TabControl1.TabPages.Add(T)
    End Sub
#End Region
#Region " File Monitor "
    Private Sub FSW1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FSW1.Created
        CreateDocument(e.FullPath)
        Calculate()
    End Sub
    Private Sub FSW1_Deleted(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FSW1.Deleted
        RemoveDocument(e.FullPath)
        Calculate()
    End Sub
    Private Sub FSW1_Renamed(ByVal sender As System.Object, ByVal e As System.IO.RenamedEventArgs) Handles FSW1.Renamed
        RenameDocument(e.OldFullPath, e.FullPath)
        Calculate()
    End Sub
#End Region
#Region " Context Menu "
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim T = FindPageList(TabControl1.SelectedTab.Text)
        If T.SelectedItems.Count > 0 Then
            Dim I = T.SelectedItems(0).Tag
            If MessageBox.Show("Do you really want to delete " & I.Name & ".txt?", Text, MessageBoxButtons.YesNo) = 6 Then File.Delete(I.FullName)
        End If
    End Sub
    Private Sub RenameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        Dim T = FindPageList(TabControl1.SelectedTab.Text)
        If T.SelectedItems.Count > 0 Then
            Dim I = T.SelectedItems(0).Tag
            Dim Name = InputBox("Please enter a new name for this file.", Text, I.Tag & " - " & I.Name)
            If Name.ToLower <> I.Name.ToLower And Name <> "" Then My.Computer.FileSystem.RenameFile(I.FullName, Name & ".txt")
        End If
    End Sub
    Private Sub CreateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateToolStripMenuItem.Click
        Dim T = FindPageList(TabControl1.SelectedTab.Text)
        Dim Name = InputBox("Please enter a name for the file being created.", Text, "Tag - Name")
        If Name <> "" Then File.WriteAllText(Path & "\" & Name & ".txt", String.Empty)
    End Sub
    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim T = FindPageList(TabControl1.SelectedTab.Text)
        If T.SelectedItems.Count > 0 Then Process.Start(T.SelectedItems(0).Tag.FullName)
    End Sub
#End Region
End Class