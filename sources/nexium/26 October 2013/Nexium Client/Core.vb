Module Core

#Region " Private Members "

    Private WithEvents Client As New UserClient()

    Private Settings As New SettingsHelper("Nexium")

    Private NumberofConnectionScanners As Integer = 5
    Private ConnectionScanners As New List(Of UserClient)

#End Region

#Region " Properties "

    Private _Username As String
    Public ReadOnly Property Username() As String
        Get
            Return _Username
        End Get
    End Property

    Private _Password As New SecureProperty()
    Public ReadOnly Property Password() As String
        Get
            Return _Password.GetValue()
        End Get
    End Property

    Private _BypassMasterServer As Boolean
    Public ReadOnly Property BypassMasterServer() As Boolean
        Get
            Return _BypassMasterServer
        End Get
    End Property

    Private _Servers As New List(Of ServerEndpoint)
    Public ReadOnly Property Servers() As ServerEndpoint()
        Get
            Return _Servers.ToArray()
        End Get
    End Property

#End Region

#Region " Server Browser "

    Public Sub LoadServerList()
        _Servers.Clear()
        Settings.LoadFileSettings("servers.xml")

        Dim Count As Integer = Settings.FileSettings().AllKeys.Length
        If Not (Count Mod 2 = 0) Then Return

        For I As Integer = 0 To Count - 1 Step 2
            Dim HostKey As String = String.Format("Server{0}_Host", I)
            Dim PortKey As String = String.Format("Server{0}_Port", I)

            Dim Host As String = Settings.FileSettings(HostKey)
            Dim Port As UShort

            If Not UShort.TryParse(Settings.FileSettings(PortKey), Port) Then Continue For
            Dim EndPoint As New ServerEndpoint(Host, Port)

            If Not _Servers.Contains(EndPoint) Then
                _Servers.Add(EndPoint)
            End If
        Next
    End Sub

    Public Sub SaveServerList()
        Settings.ClearFileSettings()

        For I As Integer = 0 To Servers.Length - 1
            Settings.FileSettings(String.Format("Server{0}_Host", I)) = Servers(I).Host
            Settings.FileSettings(String.Format("Server{0}_Port", I)) = Servers(I).Port.ToString()
        Next

        Settings.SaveFileSettings("servers.xml")
    End Sub

    Public Sub StartScanningServers()
        For I As Integer = 0 To Math.Min(Servers.Length - 1, NumberofConnectionScanners - 1)
            Dim C As New UserClient()
            ConnectionScanners.Add(C)

            C.UserState = I
            AddHandler C.StateChanged, AddressOf ConnectionScanner_StateChanged
            AddHandler C.ReadPacket, AddressOf ConnectionScanner_ReadPacket

            Dim EndPoint As ServerEndpoint = Servers(I)
            C.Connect(EndPoint.Host, EndPoint.Port)
        Next
    End Sub

    Private Sub ScanNextServer(sender As UserClient)
        Dim Index As Integer = DirectCast(sender.UserState, Integer) + NumberofConnectionScanners
        If Index > Servers.Length Then Return

        Dim EndPoint As ServerEndpoint = Servers(Index)
        sender.Connect(EndPoint.Host, EndPoint.Port)
    End Sub

    Private Sub StopScanningServers()
        For Each C As UserClient In ConnectionScanners
            C.Disconnect()
        Next

        ConnectionScanners.Clear()
    End Sub

    Private Sub ConnectionScanner_StateChanged(sender As UserClient, connected As Boolean)
        If connected Then
            'TODO: Request server details
        Else
            ScanNextServer(sender)
        End If
    End Sub

    Private Sub ConnectionScanner_ReadPacket(sender As UserClient, data As Byte())
        'TODO: Read server details and add to server browser with sender.EndPoint

        sender.Disconnect()
    End Sub

#End Region

End Module
