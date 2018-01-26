Imports System.IO
Imports System.Security.Cryptography

Module ClientCore

#Region " Constant Values "

    Private Const CLIENT_SETTINGS_FILENAME As String = "ClientSettings.xml"
    Private Const CLIENT_CHANNELS_FILENAME As String = "RemoteChannels.xml"

#End Region

#Region " Client Properties "

    Public RememberMe As Boolean
    Public BypassMaster As Boolean

    Public Username As String
    Public Password As String

    Public BufferSize As UShort
    Public ServerScanners As Byte

#End Region

#Region " Client Members "

    Public AesKey, AesIV As Byte()
    Private Encryptor As ICryptoTransform
    Private Decryptor As ICryptoTransform

    Public Settings As SettingsHelper

    Public WithEvents Client As New UserClient()

#End Region

    'TODO: Do something with these..

    Public SecureConnection As Boolean

    Public Function Encrypt(data As Byte()) As Byte()
        Return Encryptor.TransformFinalBlock(data, 0, data.Length)
    End Function

    Public Function Decrypt(data As Byte()) As Byte()
        Return Decryptor.TransformFinalBlock(data, 0, data.Length)
    End Function

    Public UsersOnline As Integer

    Public Servers As New List(Of ServerEndpoint)
    Private ConnectionScanners As New List(Of UserClient)

    Public Users As New Dictionary(Of Integer, User)

#Region " Initialization "

    Public Sub InitializeClient()
        Settings = New SettingsHelper()

        If Not File.Exists(CLIENT_SETTINGS_FILENAME) Then
            GenerateDefaultSettings()
        End If

        LoadClientSettings()

        Client = New UserClient()
        Client.BufferSize = BufferSize
        Client.MaxPacketSize = 8192

        InitializeAes()
    End Sub

    Private Sub InitializeAes()
        Dim AesProvider As New RijndaelManaged()

        AesProvider.BlockSize = AES_BLOCKSIZE
        AesProvider.GenerateKey()
        AesProvider.GenerateIV()

        AesKey = AesProvider.Key
        AesIV = AesProvider.IV

        Encryptor = AesProvider.CreateEncryptor()
        Decryptor = AesProvider.CreateDecryptor()
    End Sub

#End Region

#Region " Settings "

    Private Sub GenerateDefaultSettings()
        Settings.FileSettings("RememberMe") = "False"
        Settings.FileSettings("BypassMaster") = "False"
        Settings.FileSettings("Username") = String.Empty
        Settings.FileSettings("Password") = String.Empty
        Settings.FileSettings("BufferSize") = "8192"
        Settings.FileSettings("ServerScanners") = "5"

        Settings.SaveFileSettings(CLIENT_SETTINGS_FILENAME)
    End Sub

    Private Sub LoadClientSettings()
        Settings.LoadFileSettings(CLIENT_SETTINGS_FILENAME)

        RememberMe = Boolean.Parse(Settings.FileSettings("RememberMe"))
        BypassMaster = Boolean.Parse(Settings.FileSettings("BypassMaster"))

        If RememberMe Then
            Username = SanitizeUsername(Settings.FileSettings("Username"))
            Password = Settings.FileSettings("Password")

            If Not ValidateUsername(Username) Then
                Username = String.Empty
                Password = String.Empty
                RememberMe = False
            End If
        End If

        'TODO: Validate BufferSize and ServerScanners
        BufferSize = UShort.Parse(Settings.FileSettings("BufferSize"))
        ServerScanners = Byte.Parse(Settings.FileSettings("ServerScanners"))
    End Sub

#End Region


#Region " Server Browser "

    Public Sub LoadServerList()
        Servers.Clear()
        Settings.LoadFileSettings(CLIENT_CHANNELS_FILENAME)

        For I As Integer = 0 To Settings.FileSettings.AllKeys.Length - 1
            Dim Setting As String = Settings.FileSettings("Server" & I)
            Dim EndPoint As ServerEndpoint = ServerEndpoint.Parse(Setting)

            If Not Servers.Contains(EndPoint) Then
                Servers.Add(EndPoint)
            End If
        Next
    End Sub

    Public Sub SaveServerList()
        Settings.ClearFileSettings()

        For I As Integer = 0 To Servers.Count - 1
            Settings.FileSettings(String.Format("Server{0}", I)) = Servers(I).ToString()
        Next

        Settings.SaveFileSettings(CLIENT_CHANNELS_FILENAME)
    End Sub

    Public Sub StartScanningServers()
        UsersOnline = 0

        For I As Integer = 0 To Math.Min(Servers.Count - 1, ServerScanners - 1)
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
        Dim Index As Integer = DirectCast(sender.UserState, Integer) + ServerScanners
        If Index > Servers.Count Then Return

        Dim EndPoint As ServerEndpoint = Servers(Index)
        sender.Connect(EndPoint.Host, EndPoint.Port)
    End Sub

    Public Sub StopScanningServers()
        For Each C As UserClient In ConnectionScanners
            C.Disconnect()
        Next

        ConnectionScanners.Clear()
    End Sub

    Private Sub ConnectionScanner_StateChanged(sender As UserClient, connected As Boolean)
        If connected Then
            SendServerStatePacket(sender)
        Else
            ScanNextServer(sender)
        End If
    End Sub

    Private Sub ConnectionScanner_ReadPacket(sender As UserClient, data As Byte())
        Try
            Dim Values As Object() = Pack.Deserialize(data)

            If DirectCast(Values(0), Byte) = PacketHeader.ServerState Then
                HandleServerStatePacket(sender, Values)
            End If
        Catch
            'Do nothing.
        End Try

        sender.Disconnect()
    End Sub

#End Region

#Region " Socket Handlers "

    Private Sub Client_ReadPacket(sender As UserClient, data() As Byte) Handles Client.ReadPacket
        If SecureConnection Then
            data = Decrypt(data)
        End If

        Dim Values As Object() = Pack.Deserialize(data)
        If Values.Length = 0 Then Return

        Select Case DirectCast(Values(0), PacketHeader)
            Case PacketHeader.Initialize
                HandleInitializePacket(Values)
            Case PacketHeader.Exchange
                HandleExchangePacket()
            Case PacketHeader.Authenticate
                HandleAuthenticatePacket(Values)
            Case PacketHeader.UserList
                HandleUserListPacket(Values)
            Case PacketHeader.UserLeave
                HandleUserLeavePacket(Values)
            Case PacketHeader.UserJoin
                HandleUserJoinPacket(Values)
            Case PacketHeader.ChannelChat
                HandleChannelChatPacket(Values)
        End Select
    End Sub

    Private Sub Client_StateChanged(sender As UserClient, connected As Boolean) Handles Client.StateChanged
        If connected Then
            SendInitializePacket()
        Else
            ClientMain.ShowServerBrowser()

            InitializeAes()
            SecureConnection = False
        End If
    End Sub

    Private Sub Client_ExceptionThrown(sender As UserClient, ex As Exception) Handles Client.ExceptionThrown
        'TODO: Write exceptions to error log.
    End Sub

    'Private Sub Client_ReadProgressChanged(sender As UserClient, progress As Double, bytesRead As Integer, bytesToRead As Integer) Handles Client.ReadProgressChanged
    '    'TODO: Show file download progress.
    'End Sub

    'Private Sub Client_WriteProgressChanged(sender As UserClient, progress As Double, bytesWritten As Integer, bytesToWrite As Integer) Handles Client.WriteProgressChanged
    '    'TODO: Show file upload progress.
    'End Sub

#End Region

End Module
