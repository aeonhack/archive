Imports System.IO
Imports System.Threading

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 4/17/2011
'Changed: 4/24/2012
'Version: 1.1.0
'------------------

Class FileSearch

    Private Callback As CallbackCB
    Public Delegate Sub CallbackCB()

    Sub New(_callback As CallbackCB)
        Callback = _callback
        _Files = New List(Of String)
    End Sub

    Private _Files As List(Of String)
    ReadOnly Property Files As String()
        Get
            SyncLock _Files
                Return _Files.ToArray()
            End SyncLock
        End Get
    End Property

    Private _Pattern As String
    ReadOnly Property Pattern As String
        Get
            Return _Pattern
        End Get
    End Property

    Private _Searching As Boolean
    ReadOnly Property Searching As Boolean
        Get
            Return _Searching
        End Get
    End Property

    Private _ShowHiddenFiles As Boolean = True
    Property ShowHiddenFiles() As Boolean
        Get
            Return _ShowHiddenFiles
        End Get
        Set(ByVal value As Boolean)
            _ShowHiddenFiles = value
        End Set
    End Property

    Public Sub Search(path As String, pattern As String)
        If _Searching Then Return

        Track = 0
        _Searching = True
        _Pattern = pattern
        _Files = New List(Of String)
        _Cancel = False

        Dim T As New Thread(AddressOf Initialize)
        T.IsBackground = True
        T.Start(path)
    End Sub

    Private Sub Initialize(data As Object)
        Process(DirectCast(data, String))
    End Sub

    Private Track As Integer
    Private Sub Search(data As Object)
        If _Cancel Then Return
        Dim Path As String = DirectCast(data, String)

        Try
            For Each P As String In Directory.GetDirectories(Path)
                If _ShowHiddenFiles Then
                    Process(P)
                ElseIf Not IsHidden((New DirectoryInfo(P)).Attributes) Then
                    Process(P)
                End If
            Next
        Catch
            'Do nothing
        End Try

        Try
            SyncLock _Files
                If _ShowHiddenFiles Then
                    _Files.AddRange(Directory.GetFiles(Path, _Pattern))
                Else
                    For Each F As String In Directory.GetFiles(Path, _Pattern)
                        If Not IsHidden(File.GetAttributes(F)) Then _Files.Add(F)
                    Next
                End If
            End SyncLock
        Catch
            'Do nothing.
        End Try

        SyncLock Me
            Track -= 1
            If Track = 0 Then
                _Searching = False
                If Callback IsNot Nothing Then Callback()
            End If
        End SyncLock
    End Sub

    Private Sub Process(path As String)
        SyncLock Me
            Track += 1
        End SyncLock

        If Not ThreadPool.QueueUserWorkItem(AddressOf Search, path) Then Search(path)
    End Sub

    Private _Cancel As Boolean
    Public Sub Cancel()
        _Cancel = True
        _Searching = False
    End Sub

    Public Sub Clear()
        _Files.Clear()
    End Sub

    Private Function IsHidden(a As FileAttributes) As Boolean
        If ((a And FileAttributes.Hidden) = FileAttributes.Hidden) Then Return True
        If ((a And FileAttributes.System) = FileAttributes.System) Then Return True
        Return False
    End Function

End Class