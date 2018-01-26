Imports System.IO

Public Class Main
    Friend Shortcuts(7) As Shortcut, System As Boolean
    Dim Songs As New List(Of String), M As New Media, Filter As String
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each T In My.Resources.Extensions.Split("$")
            Filter &= "*." & T & ";"
        Next
        Filter = Filter.Substring(0, Filter.Length - 1)
    End Sub
    Private Declare Sub keybd_event Lib "user32" (ByVal key As Byte, ByVal scan As Byte, ByVal flags As Integer, ByVal data As Integer)
    Sub Shortcut_Pressed(ByVal sender As Shortcut)
        Select Case Array.IndexOf(Shortcuts, sender)
            Case 0 : Button1.PerformClick()
            Case 1 : Button2.PerformClick()
            Case 2 : Button4.PerformClick()
            Case 3 : Button3.PerformClick()
            Case 4
                If System Then
                    keybd_event(Keys.VolumeUp, 0, 0, 0)
                Else
                    M.Volume += 5 : TrackBar2.Value = M.Volume
                End If
            Case 5
                If System Then
                    keybd_event(Keys.VolumeDown, 0, 0, 0)
                Else
                    M.Volume -= 5 : TrackBar2.Value = M.Volume
                End If
            Case 6
                If System Then keybd_event(Keys.VolumeMute, 0, 0, 0) Else Button5.PerformClick()
        End Select
    End Sub
    Class Shortcut : Inherits NativeWindow
        Private Declare Auto Function RegisterHotKey Lib "user32" (ByVal handle As IntPtr, ByVal id As Integer, ByVal modifier As Integer, ByVal key As Integer) As Integer
        Private Declare Auto Function UnregisterHotKey Lib "user32" (ByVal handle As IntPtr, ByVal id As Integer) As Integer
        Enum Modifier : None = 0 : Alt = 1 : Ctrl = 2 : Shift = 4 : End Enum
        Event Pressed(ByVal sender As Shortcut) : Dim ID As Integer
        Sub New()
            CreateHandle(New CreateParams) : ID = GetHashCode()
        End Sub
        Sub Register(ByVal modifier As Modifier, ByVal key As Keys)
            RegisterHotKey(Handle, ID, modifier, key)
        End Sub
        Sub Unregister()
            UnregisterHotKey(Handle, ID)
        End Sub
        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = 786 Then RaiseEvent Pressed(Me)
            MyBase.WndProc(m)
        End Sub
    End Class

#Region " Song List "
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        Label1.Hide()
        TextBox1.Focus()
    End Sub
    Private Sub TextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If TextBox1.Text = "" Then Label1.Show()
    End Sub
    Private Sub ListBox1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick
        LoadSong()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Build()
    End Sub
    Sub Build()
        ListBox1.BeginUpdate()
        ListBox1.Items.Clear()
        For Each F In Songs
            If F.ToLower.Contains(TextBox1.Text.ToLower) Then ListBox1.Items.Add(Path.GetFileNameWithoutExtension(F))
        Next
        ListBox1.EndUpdate()
    End Sub
    Sub LoadList(ByVal path As String)
        Dim T As String
        Using R As New StringReader(File.ReadAllText(path))
            While R.Peek > -1
                T = R.ReadLine : If T <> "" Then Songs.Add(T)
            End While
        End Using
    End Sub
    Sub LoadSong()
        Dim P = Find(ListBox1.SelectedItem)
        Try
            M.Open(P)
            TrackBar1.Value = 0
            TrackBar1.Maximum = M.Length
            Button3.Image = My.Resources.Pause
            ToolStripLabel1.Text = ListBox1.SelectedItem
            M.Play()
            Timer1.Start()
        Catch
            RemoveSong(P)
            Button4.PerformClick()
        End Try
    End Sub
    Sub RemoveSong(ByVal path As String)
        Songs.Remove(path)
        Build()
    End Sub
    Function Find(ByVal name As String) As String
        For Each F In Songs
            If Path.GetFileNameWithoutExtension(F).ToLower = name.ToLower Then Return F
        Next : Return String.Empty
    End Function
#End Region
#Region " Media Controls "
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        M.Stop() : Timer1.Stop() : Button3.Image = My.Resources.Play
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.Items.Count = 0 Or ToolStripButton1.Checked Then Return
        If ListBox1.SelectedIndex = 0 Then
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        Else
            ListBox1.SelectedIndex -= 1
        End If
        LoadSong()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ListBox1.Items.Count = 0 Then Return
        If ToolStripButton1.Checked Then
            RandomSong()
        Else
            If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
                ListBox1.SelectedIndex = 0
            Else
                ListBox1.SelectedIndex += 1
            End If
        End If
        LoadSong()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If M.Status <> 0 Then
            If M.Status = 1 Then
                Button3.Image = My.Resources.Play
                M.Pause() : Timer1.Stop()
            Else
                Button3.Image = My.Resources.Pause
                M.Play() : Timer1.Start()
            End If
        Else
            Button4.PerformClick()
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        M.Mute = Not M.Mute
        Button5.Image = If(M.Mute, My.Resources.Mute, My.Resources.Unmute)
    End Sub
    Sub RandomSong()
        If ListBox1.Items.Count = 1 Then
            ListBox1.SelectedIndex = 0
        Else
            Dim Seed As Integer, Current = ListBox1.SelectedIndex
            For Each I In Guid.NewGuid.ToByteArray : Seed += I : Next
            Do Until ListBox1.SelectedIndex <> Current
                ListBox1.SelectedIndex = (New Random).Next(ListBox1.Items.Count)
            Loop
        End If
    End Sub
    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        M.Volume = TrackBar2.Value
    End Sub
#End Region
#Region " Song Tracking "
    Private Sub TrackBar1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar1.MouseDown
        Seeking = True
    End Sub
    Private Sub TrackBar1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar1.MouseUp
        M.Seek(TrackBar1.Value) : Seeking = False
    End Sub
    Dim Seeking As Boolean
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim P = M.Position : If Not Seeking Then TrackBar1.Value = P
        If M.Status = 1 And P = M.Length Then Button4.PerformClick()
    End Sub
#End Region
#Region " Menu Strip "
    Private Sub BrowseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilesToolStripMenuItem.Click
        Using O As New OpenFileDialog
            O.Filter = "Audio Files|" & Filter
            O.Multiselect = True
            If O.ShowDialog = 1 Then
                Songs.AddRange(O.FileNames)
                Build()
            End If
        End Using
    End Sub
    Private Sub FolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FolderToolStripMenuItem.Click
        Using F As New FolderBrowserDialog
            If F.ShowDialog = 1 Then
                Dim Files = Directory.GetFiles(F.SelectedPath, "*.*", 1)
                Dim Extensions = My.Resources.Extensions.Split("$")
                For Each S In Files
                    For Each T In Extensions
                        If Path.GetExtension(S) = "." & T Then : Songs.Add(S) : Exit For : End If
                    Next
                Next
                Build()
            End If
        End Using
    End Sub
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If ListBox1.Items.Count = 0 Then Return
        Using S As New SaveFileDialog
            S.Filter = "Media Feather Song List|*.mfsl"
            If S.ShowDialog = 1 Then
                For Each F In Songs
                    My.Computer.FileSystem.WriteAllText(S.FileName, F & vbCrLf, True)
                Next
            End If
        End Using
    End Sub
    Private Sub LoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        Using O As New OpenFileDialog
            O.Multiselect = True
            O.Filter = "Media Feather Song List|*.mfsl"
            If O.ShowDialog = 1 Then
                For Each F In O.FileNames : LoadList(F) : Next
                Build()
            End If
        End Using
    End Sub
#End Region

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        Settings.Show()
    End Sub

End Class

Class Media
#Region " Declerations "
    Private Declare Function mciSendStringA Lib "winmm" (ByVal command As String, ByVal buffer As String, ByVal length As Integer, ByVal handle As Integer) As Integer
    Private Declare Function mciGetErrorStringA Lib "winmm" (ByVal id As Integer, ByVal buffer As String, ByVal length As Integer) As Integer
    Enum State : Closed = 0 : Playing = 1 : Paused = 2 : Stopped = 3 : End Enum
    Private ID As String, EB As String = Space(128), SB As String = Space(128)
#End Region
    Sub New()
        ID = Guid.NewGuid.ToString.Substring(0, 4)
    End Sub
#Region " Methods "
    Sub Open(ByVal path As String)
        Close() : Message("open """ & path & """ alias " & ID)
        Message("status " & ID & " length") : _Length = Val(SB) : _State = 3
        'This is just a lazy work around to preserve volume after song change.
        If _Mute Then
            Mute = False
            Mute = True
        Else
            Volume = _Volume
        End If
    End Sub
    Sub Close()
        If _State <> 0 Then : _State = 0 : Message("close " & ID) : End If
    End Sub
    Sub Play()
        If _State <> 0 Then : Message("play " & ID) : _State = 1 : End If
    End Sub
    Sub [Stop]()
        If _State <> 0 Then : Message("stop " & ID) : _State = 3 : Seek(0) : End If
    End Sub
    Sub Pause()
        If _State <> 0 Then : Message("pause " & ID) : _State = 2 : End If
    End Sub
    Sub [Resume]()
        If _State <> 0 Then : Message("resume " & ID) : _State = 1 : End If
    End Sub
    Sub Seek(ByVal position As Integer)
        If _State <> 0 Then If _State = 1 Then Message("play " & ID & " from " & position) Else Message("seek " & ID & " to " & position)
    End Sub
#End Region
#Region " Properties "
    Private _Volume As Double = 100, MuteVolume As Double = 100
    Property Volume() As Double
        Get
            Return _Volume
        End Get
        Set(ByVal v As Double)
            If v < 0 Then v = 0 Else If v > 100 Then v = 100
            If _Mute Then
                MuteVolume = v
            Else
                If _State <> 0 Then
                    Message("setaudio " & ID & " volume to " & CInt(v * 10)) : _Volume = v
                End If
            End If
        End Set
    End Property
    Private _Mute As Boolean
    Property Mute() As Boolean
        Get
            Return _Mute
        End Get
        Set(ByVal v As Boolean)
            If v Then
                MuteVolume = _Volume
                Volume = 0
            End If
            _Mute = v
            If Not v Then Volume = MuteVolume
        End Set
    End Property
    Private _Length As Integer
    ReadOnly Property Length() As Integer
        Get
            Return _Length
        End Get
    End Property
    ReadOnly Property LengthString() As String
        Get
            Return New Date(New TimeSpan(0, 0, 0, 0, _Length).Ticks).ToString("mm:ss")
        End Get
    End Property
    ReadOnly Property Position() As Integer
        Get
            If _State <> 0 Then : Message("status " & ID & " position") : Return Val(SB) : Else : Return 0 : End If
        End Get
    End Property
    Private _State As State
    ReadOnly Property Status() As State
        Get
            Return _State
        End Get
    End Property
#End Region
    Private Sub Message(ByVal command As String)
        Dim I = mciSendStringA(command, SB, 128, 0)
        If I <> 0 Then : mciGetErrorStringA(I, EB, 128) : Throw (New Exception(EB.Trim)) : End If
    End Sub
End Class