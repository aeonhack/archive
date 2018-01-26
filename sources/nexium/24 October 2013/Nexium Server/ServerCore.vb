Imports System.Security.Cryptography
Imports System.IO
Imports System.Threading

Module ServerCore

#Region " Constant Values "

    Public Const UPNP_DESCRIPTION As String = "Nexium Chat Server"

    Private Const SERVER_SETTINGS_FILENAME As String = "ServerSettings.xml"

    Private Const DEFAULT_SERVERNAME As String = "1519 Chat Server"
    Private Const DEFAULT_SERVERMOTTO As String = "Check out AlecNunn.com for more information!"

    Private Const DEFAULT_BUFFERSIZE As Integer = 8192
    Private Const DEFAULT_MAXPACKETSIZE As Integer = 8192

    Private Const DEFAULT_MAXCONNECTIONS As Integer = 2048

#End Region

#Region " Server Properties "

    Public ServerGUID As Guid

    Public ServerName As String
    Public ServerMotto As String

    Public BypassMaster As Boolean

    Public BufferSize As UShort
    Public MaxPacketSize As Integer
    Public MaxConnections As UShort

    Public EnableUPnP As Boolean
    Public ListeningPort As UShort

#End Region

#Region " Server Members "

    Public RsaPublic As Byte()
    Public RsaProvider As RSACryptoServiceProvider

    Public UPnP As UPnPHelper
    Public Settings As SettingsHelper

    Public WithEvents Server As ServerListener

#End Region

#Region " Initialization "

    Public Sub InitializeServer()
        Settings = New SettingsHelper()

        If Not File.Exists(SERVER_SETTINGS_FILENAME) Then
            GenerateDefaultSettings()
        End If

        LoadServerSettings()

        Server = New ServerListener()
        Server.BufferSize = BufferSize
        Server.MaxPacketSize = MaxPacketSize
        Server.MaxConnections = MaxConnections
        'Server.SingleInstanceClients = True

        UPnP = New UPnPHelper()

        RsaProvider = New RSACryptoServiceProvider(RSA_KEYSIZE)
        RsaPublic = RsaProvider.ExportCspBlob(False)

        AuthenticatedUsers = New List(Of User)
    End Sub

#End Region

#Region " Settings "

    Private Sub GenerateDefaultSettings()
        Settings.FileSettings("ServerName") = DEFAULT_SERVERNAME
        Settings.FileSettings("ServerMotto") = DEFAULT_SERVERMOTTO
        Settings.FileSettings("ServerGUID") = Guid.NewGuid.ToString()
        Settings.FileSettings("BypassMaster") = "False"
        Settings.FileSettings("BufferSize") = DEFAULT_BUFFERSIZE.ToString()
        Settings.FileSettings("MaxPacketSize") = DEFAULT_MAXPACKETSIZE.ToString()
        Settings.FileSettings("MaxConnections") = DEFAULT_MAXCONNECTIONS.ToString()
        Settings.FileSettings("ListeningPort") = "6565"
        Settings.FileSettings("EnableUPnP") = "True"

        Settings.SaveFileSettings(SERVER_SETTINGS_FILENAME)
    End Sub

    Private Sub LoadServerSettings()
        Settings.LoadFileSettings(SERVER_SETTINGS_FILENAME)

        ServerName = SanitizeMessage(Settings.FileSettings("ServerName"))
        ServerMotto = SanitizeMessage(Settings.FileSettings("ServerMotto"))

        ServerGUID = New Guid(Settings.FileSettings("ServerGUID"))

        BypassMaster = Boolean.Parse(Settings.FileSettings("BypassMaster"))

        BufferSize = UShort.Parse(Settings.FileSettings("BufferSize"))
        MaxPacketSize = Integer.Parse(Settings.FileSettings("MaxPacketSize"))
        MaxConnections = UShort.Parse(Settings.FileSettings("MaxConnections"))

        ListeningPort = UShort.Parse(Settings.FileSettings("ListeningPort"))
        EnableUPnP = Boolean.Parse(Settings.FileSettings("EnableUPnP"))

        If ServerName.Length < 4 OrElse ServerName.Length > 44 Then
            Console.WriteLine("ERROR: ServerName must be between 4 and 44 characters long.")
            Console.WriteLine("ServerName: {0}", ServerName)
            ServerName = DEFAULT_SERVERNAME
        End If

        If ServerMotto.Length > 120 Then
            Console.WriteLine("ERROR: ServerMotto must not exceed 120 characters.")
            Console.WriteLine("ServerMotto: {0}", ServerMotto)
            ServerMotto = DEFAULT_SERVERMOTTO
        End If

        If BufferSize < 1024 Then
            Console.WriteLine("ERROR: BufferSize must be at least 1024 bytes.")
            Console.WriteLine("BufferSize: {0:N0}", BufferSize)
            BufferSize = DEFAULT_BUFFERSIZE
        End If

        If MaxPacketSize < 1024 OrElse MaxPacketSize > 104857600 Then
            Console.WriteLine("ERROR: MaxPacketSize must be between 1KB and 100MB.")
            Console.WriteLine("MaxPacketSize: {0:N0}", MaxPacketSize)
            MaxPacketSize = DEFAULT_MAXPACKETSIZE
        End If

        If MaxConnections < 1 OrElse MaxConnections > 200 Then
            Console.WriteLine("ERROR: MaxConnections must be between 1 and 200.")
            Console.WriteLine("MaxConnections: {0:N0}", MaxConnections)
            MaxConnections = DEFAULT_MAXCONNECTIONS
        End If
    End Sub

#End Region

#Region " Socket Handlers "

    Private Sub Server_ClientReadPacket(sender As ServerListener, client As ServerClient, data() As Byte) Handles Server.ClientReadPacket
        Try
            If client.UserState IsNot Nothing Then
                data = client.UserState.Decrypt(data)
            End If

            Dim Values As Object() = Pack.Deserialize(data)
            If Values.Length = 0 Then
                client.Disconnect()
                Return
            End If

            Select Case DirectCast(Values(0), PacketHeader)
                Case PacketHeader.ServerState
                    HandleServerStatePacket(client)
                Case PacketHeader.Initialize
                    HandleInitializePacket(client)
                Case PacketHeader.Exchange
                    HandleExchangePacket(client, Values)
                Case PacketHeader.Authenticate
                    HandleAuthenticatePacket(client, Values)
                Case PacketHeader.ChannelChat
                    HandleChannelChatPacket(client, Values)
                Case Else
                    client.Disconnect()
            End Select
        Catch ex As Exception
            client.Disconnect()
        End Try
    End Sub

    Private Sub Server_ClientStateChanged(sender As ServerListener, client As ServerClient, connected As Boolean) Handles Server.ClientStateChanged
        If connected Then
            Console.WriteLine("New connection from {0}", client.EndPoint.Address)
            'TODO: Check client.EndPoint for IP bans.
        Else
            If client.UserState Is Nothing Then Return

            If client.UserState.Authenticated Then
                SyncLock AuthenticatedUsers
                    AuthenticatedUsers.Remove(client.UserState)
                End SyncLock

                SendUserLeavePacket(client.UserState.ID)
            End If
        End If
    End Sub

    Private Sub Server_ExceptionThrown(sender As ServerListener, ex As Exception) Handles Server.ExceptionThrown
        Console.WriteLine("Server Error: {0}", ex)
    End Sub

    Private Sub Server_StateChanged(sender As ServerListener, listening As Boolean) Handles Server.StateChanged
        If listening Then
            Console.WriteLine("The server is listening on port {0}", ListeningPort)
        Else
            Console.WriteLine("The server is no longer listening for connections.")
        End If
    End Sub

#End Region

    Public RunningUserID As Integer
    Public AuthenticatedUsers As List(Of User)

    Public Function FindUserSocket(name As String) As ServerClient
        Dim NameToLower As String = name.ToLower()

        For Each C As ServerClient In Server.Clients
            If C.UserState.Authenticated AndAlso C.UserState.Name.ToLower() = NameToLower Then
                Return C
            End If
        Next

        Return Nothing
    End Function

    Public Sub AuthenticateUser(client As ServerClient, name As String)
        Dim ID As Integer = Interlocked.Increment(RunningUserID)
        client.UserState.Authenticate(ID, name)

        AuthenticatedUsers.Add(client.UserState)

        SendUserListPacket(client)
        SendUserJoinPacket(client, client.UserState)
    End Sub

End Module
