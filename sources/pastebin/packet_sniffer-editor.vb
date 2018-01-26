Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Collections.Generic

'Installation:
'-----------------
'1.) Navigate to Windows Control Panel-> Device Manager.
'2.) Select the top most item (ex. Name-PC), right click, and Add Legacy Hardware.
'3.) When prompted choose to install the hardware you select from a list (Advanced).
'4.) Install Network Adapter-> Microsoft-> Microsoft Loopback Adapter.

'Configuration:
'-----------------
'1.) Navigate to Windows Control Panel-> Network and Sharing Center.
'2.) Find the option 'Change adapter settings', usually found on the top left.
'3.) Once in Network Connections, right click the Loopback Adapater and choose Properties.
'4.) (Optional) Disable everything but Internet Protocol Version 4 (TCP/IPv4).
'5.) Select Internet Protocol Version 4 and click on Properties.
'6.) Set the IP address to your target ip address and press OK.

'Important:
'-----------------
'Disable the adapter when not in use. Follow Configuration steps 1 to 3
'but instead of choosing Properties, click Enable/Disable instead.


'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 7/10/2010
'Changed: 1/15/2012
'Version: 2.0.0
'------------------
Class Carrier

    Delegate Sub RecieveDG(ByVal connection As Interceptor, ByVal data As Byte())
    Delegate Sub ConnectionDG(ByVal connection As Interceptor)
    Delegate Sub StateChangeDG()

    Public ServerToClient As RecieveDG
    Public ClientToServer As RecieveDG
    Public ConnectEvent As ConnectionDG
    Public DisconnectEvent As ConnectionDG
    Public StateChange As StateChangeDG

    Private DisconnectFilter As ConnectionDG

    Private Master As Socket
    Private LocalEndPoint As IPEndPoint
    Private RemoteEndPoint As IPEndPoint

    Private ConnectionPool As Stack(Of Interceptor)

    Private _Connections As Dictionary(Of Guid, Interceptor)
    ReadOnly Property Connections As Dictionary(Of Guid, Interceptor)
        Get
            Return _Connections
        End Get
    End Property

    Private _Listening As Boolean
    ReadOnly Property Listening As Boolean
        Get
            Return _Listening
        End Get
    End Property

    Sub New()
        _Connections = New Dictionary(Of Guid, Interceptor)
        ConnectionPool = New Stack(Of Interceptor)

        ServerToClient = New RecieveDG(AddressOf RecieveHandler)
        ClientToServer = New RecieveDG(AddressOf RecieveHandler)
        ConnectEvent = New ConnectionDG(AddressOf ConnectionHandler)
        DisconnectEvent = New ConnectionDG(AddressOf ConnectionHandler)
        StateChange = New StateChangeDG(AddressOf StateChangeHandler)

        DisconnectFilter = New ConnectionDG(AddressOf DisconnectFilterHandler)
    End Sub

#Region " Handlers "
    Private Sub RecieveHandler(ByVal connection As Interceptor, ByVal data As Byte())
        Return
    End Sub
    Private Sub ConnectionHandler(ByVal connection As Interceptor)
        Return
    End Sub
    Private Sub StateChangeHandler()
        Return
    End Sub

    Private Sub DisconnectFilterHandler(ByVal connection As Interceptor)
        SyncLock Me
            _Connections.Remove(connection.GUID)
            ConnectionPool.Push(connection)
        End SyncLock

        DisconnectEvent(connection)
    End Sub

#End Region

    Private Function IPv4Addresses() As IPAddress()
        Dim T As New List(Of IPAddress)

        For Each I As IPAddress In Dns.GetHostEntry(Dns.GetHostName).AddressList
            If IsPrivateIPv4(I) Then T.Add(I)
        Next

        Return T.ToArray()
    End Function
    Private Function IsPrivateIPv4(ByVal address As IPAddress) As Boolean
        If Not address.AddressFamily = AddressFamily.InterNetwork Then Return False

        Dim I As Byte() = address.GetAddressBytes
        Return I(0) = 10 OrElse (I(0) = 172 AndAlso I(1) > 15 AndAlso I(1) < 32) OrElse (I(0) = 192 AndAlso I(1) = 168)
    End Function

    Sub Listen(ByVal remoteHost As String, ByVal remotePort As UShort)
        Try
            SyncLock Me
                If _Listening Then Return
                _Listening = True

                StateChange()

                LocalEndPoint = New IPEndPoint(IPv4Addresses(0), 0)
                RemoteEndPoint = New IPEndPoint(IPAddress.Parse(remoteHost), remotePort)

                For I As Integer = 1 To 20 'Change this for more concurrent connections.
                    ConnectionPool.Push(New Interceptor(LocalEndPoint, RemoteEndPoint, ServerToClient, ClientToServer, ConnectEvent, DisconnectFilter))
                Next

                Master = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                Master.Bind(RemoteEndPoint)
                Master.Listen(3)

                Dim E As New SocketAsyncEventArgs
                AddHandler E.Completed, AddressOf AcceptAsync

                If Not Master.AcceptAsync(E) Then
                    AcceptAsync(Master, E)
                End If
            End SyncLock
        Catch
            Close()
        End Try
    End Sub

    Private Sub AcceptAsync(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        Try
            If e.LastOperation = SocketAsyncOperation.Accept AndAlso e.SocketError = SocketError.Success Then
                SyncLock Me
                    Dim Interceptor As Interceptor = ConnectionPool.Pop
                    _Connections.Add(Interceptor.GUID, Interceptor)
                    Interceptor.Initialize(e.AcceptSocket)
                End SyncLock

                e.AcceptSocket = Nothing
                If Not Master.AcceptAsync(e) Then
                    AcceptAsync(Master, e)
                End If
            Else
                Close()
            End If
        Catch
            Close()
        End Try
    End Sub

    Sub Close()
        SyncLock Me
            If Not _Listening Then Return
            _Listening = False

            Try
                Master.Close()
            Catch
                'Do Nothing
            End Try

            Dim E As Dictionary(Of Guid, Interceptor).Enumerator = _Connections.GetEnumerator
            While E.MoveNext
                E.Current.Value.Close()
            End While

            _Connections.Clear()
        End SyncLock
    End Sub

End Class

Class Interceptor

    Private Closing As Boolean

    Private ClientSocket As Socket
    Private ServerSocket As Socket

    Private LocalEndPoint As IPEndPoint
    Private RemoteEndPoint As IPEndPoint

    Private ConnectEventArgs As SocketAsyncEventArgs
    Private ServerRead As SocketAsyncEventArgs
    Private ClientRead As SocketAsyncEventArgs

    Private ServerRecvEvent As Carrier.RecieveDG
    Private ClientRecvEvent As Carrier.RecieveDG
    Private ConnectEvent As Carrier.ConnectionDG
    Private DisconnectEvent As Carrier.ConnectionDG

    Private Const BufferSize As Integer = 65535 'Change this to increase maximum packet size.

    Private _GUID As Guid
    ReadOnly Property GUID As Guid
        Get
            Return _GUID
        End Get
    End Property

    Sub New(ByVal local As IPEndPoint, ByVal remote As IPEndPoint, _
            ByVal serverRecv As Carrier.RecieveDG, _
            ByVal clientRecv As Carrier.RecieveDG, _
            ByVal connect As Carrier.ConnectionDG, _
            ByVal disconnect As Carrier.ConnectionDG)

        _GUID = GUID.NewGuid

        LocalEndPoint = local
        RemoteEndPoint = remote

        ServerRecvEvent = serverRecv
        ClientRecvEvent = clientRecv
        ConnectEvent = connect
        DisconnectEvent = disconnect

        ConnectEventArgs = New SocketAsyncEventArgs
        ConnectEventArgs.RemoteEndPoint = remote

        ServerRead = New SocketAsyncEventArgs
        ClientRead = New SocketAsyncEventArgs

        ServerRead.SetBuffer(New Byte(BufferSize - 1) {}, 0, BufferSize)
        ClientRead.SetBuffer(New Byte(BufferSize - 1) {}, 0, BufferSize)

        AddHandler ConnectEventArgs.Completed, AddressOf ConnectAsync
        AddHandler ServerRead.Completed, AddressOf ServerReadAsync
        AddHandler ClientRead.Completed, AddressOf ClientReadAsync
    End Sub

    Sub Initialize(ByVal socket As Socket)
        Try
            Closing = False
            ClientSocket = socket

            ServerSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            ServerSocket.Bind(LocalEndPoint)

            If Not ServerSocket.ConnectAsync(ConnectEventArgs) Then
                ConnectAsync(ServerSocket, ConnectEventArgs)
            End If
        Catch
            Close()
        End Try
    End Sub

    Private Sub ConnectAsync(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        Try
            If e.LastOperation = SocketAsyncOperation.Connect AndAlso e.SocketError = SocketError.Success Then
                ConnectEvent(Me)

                Dim T As New Thread(AddressOf GuaranteeClientRead)
                T.IsBackground = True
                T.Start()

                If Not ServerSocket.ReceiveAsync(ServerRead) Then
                    ServerReadAsync(ServerSocket, ServerRead)
                End If
            Else
                Close()
            End If
        Catch
            Close()
        End Try
    End Sub

    Private Sub GuaranteeClientRead()
        If Not ClientSocket.ReceiveAsync(ClientRead) Then
            ClientReadAsync(ClientSocket, ClientRead)
        End If
    End Sub

    Private Sub ServerReadAsync(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        Try
            If e.LastOperation = SocketAsyncOperation.Receive AndAlso e.SocketError = SocketError.Success AndAlso e.BytesTransferred > 0 Then
                Dim Data(e.BytesTransferred - 1) As Byte
                Buffer.BlockCopy(e.Buffer, 0, Data, 0, e.BytesTransferred)
                ServerRecvEvent(Me, Data)

                If Not ServerSocket.ReceiveAsync(e) Then
                    ServerReadAsync(ServerSocket, e)
                End If
            Else
                Close()
            End If
        Catch
            Close()
        End Try
    End Sub

    Private Sub ClientReadAsync(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        Try
            If e.LastOperation = SocketAsyncOperation.Receive AndAlso e.SocketError = SocketError.Success AndAlso e.BytesTransferred > 0 Then
                Dim Data(e.BytesTransferred - 1) As Byte
                Buffer.BlockCopy(e.Buffer, 0, Data, 0, e.BytesTransferred)
                ClientRecvEvent(Me, Data)

                If Not ClientSocket.ReceiveAsync(e) Then
                    ClientReadAsync(ClientSocket, e)
                End If
            Else
                Close()
            End If
        Catch
            Close()
        End Try
    End Sub

    Private Sub SendAsync(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        If Not e.LastOperation = SocketAsyncOperation.Send AndAlso e.SocketError = SocketError.Success Then
            Close()
        End If
    End Sub

    Sub Close()
        SyncLock Me
            If Closing Then Return
            Closing = True

            Try
                ServerSocket.Close()
            Catch
                'Do nothing
            End Try

            Try
                ClientSocket.Close()
            Catch
                'Do nothing
            End Try

            ServerSocket = Nothing
            ClientSocket = Nothing

            DisconnectEvent(Me)
        End SyncLock
    End Sub

    Sub SendToServer(ByVal data As Byte())
        Try
            Dim E As New SocketAsyncEventArgs
            E.SetBuffer(data, 0, data.Length)
            AddHandler E.Completed, AddressOf SendAsync

            If Not ServerSocket.SendAsync(E) Then
                SendAsync(ServerSocket, E)
            End If
        Catch
            Close()
        End Try
    End Sub

    Sub SendToClient(ByVal data As Byte())
        Try
            Dim E As New SocketAsyncEventArgs
            E.SetBuffer(data, 0, data.Length)
            AddHandler E.Completed, AddressOf SendAsync

            If Not ClientSocket.SendAsync(E) Then
                SendAsync(ClientSocket, E)
            End If
        Catch
            Close()
        End Try
    End Sub

End Class