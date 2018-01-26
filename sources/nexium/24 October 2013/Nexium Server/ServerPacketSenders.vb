Module ServerPacketSenders

    Private Sub SendPacketToClient(client As ServerClient, ParamArray params As Object())
        Dim Data As Byte() = Pack.Serialize(params)

        If client.UserState IsNot Nothing Then
            Data = client.UserState.Encrypt(Data)
        End If

        client.Send(Data)
    End Sub

    Private Sub BroadcastPacketToClients(ParamArray params As Object())
        Dim Data As Byte() = Pack.Serialize(params)

        For Each C As ServerClient In Server.Clients
            If C.UserState IsNot Nothing AndAlso C.UserState.Authenticated Then
                C.Send(C.UserState.Encrypt(Data))
            End If
        Next
    End Sub

    Private Sub BroadcastPacketToClientsExclusive(client As ServerClient, ParamArray params As Object())
        Dim Data As Byte() = Pack.Serialize(params)

        For Each C As ServerClient In Server.Clients
            If C IsNot client AndAlso C.UserState IsNot Nothing AndAlso C.UserState.Authenticated Then
                C.Send(C.UserState.Encrypt(Data))
            End If
        Next
    End Sub

    Public Sub SendServerStatePacket(client As ServerClient)
        Dim CurrentUsers As UShort = Math.Min(CUShort(AuthenticatedUsers.Count), MaxConnections)
        SendPacketToClient(client, PacketHeader.ServerState, ProtocolVersion.ToString(), BypassMaster, ServerName, ServerMotto, CurrentUsers, MaxConnections)
    End Sub

    Public Sub SendInitializePacket(client As ServerClient)
        SendPacketToClient(client, PacketHeader.Initialize, RsaPublic, maxPacketSize)
    End Sub

    Public Sub SendExchangePacket(client As ServerClient)
        SendPacketToClient(client, PacketHeader.Exchange)
    End Sub

    Public Sub SendAuthenticatePacket(client As ServerClient, code As ResponseCode)
        SendPacketToClient(client, PacketHeader.Authenticate, code)
    End Sub

    Public Sub SendUserListPacket(client As ServerClient)
        Dim T As New List(Of Object)
        T.Add(PacketHeader.UserList)

        SyncLock AuthenticatedUsers
            Dim Current As User

            T.Add(AuthenticatedUsers.Count)
            For I As Integer = 0 To AuthenticatedUsers.Count - 1
                Current = AuthenticatedUsers(I)
                T.Add(Current.ID)
                T.Add(Current.Name)
                T.Add(Current.Rank)
            Next
        End SyncLock

        SendPacketToClient(client, T.ToArray())
    End Sub

    Public Sub SendUserLeavePacket(id As Integer)
        BroadcastPacketToClients(PacketHeader.UserLeave, id)
    End Sub

    Public Sub SendUserJoinPacket(client As ServerClient, user As User)
        BroadcastPacketToClientsExclusive(client, PacketHeader.UserJoin, user.ID, user.Name, user.Rank)
    End Sub

    Public Sub SendChanelChatPacket(client As ServerClient, id As Integer, message As String)
        BroadcastPacketToClientsExclusive(client, PacketHeader.ChannelChat, id, message)
    End Sub

End Module
