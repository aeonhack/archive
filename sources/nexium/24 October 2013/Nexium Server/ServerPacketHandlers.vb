Module ServerPacketHandlers

    Public Sub HandleServerStatePacket(client As ServerClient)
        SendServerStatePacket(client)
    End Sub

    Public Sub HandleInitializePacket(client As ServerClient)
        SendInitializePacket(client)
    End Sub

    Public Sub HandleExchangePacket(client As ServerClient, values As Object())
        Dim Aes As Byte() = RsaProvider.Decrypt(DirectCast(values(1), Byte()), True)
        Dim Params As Object() = Pack.Deserialize(Aes)

        Dim AesKey As Byte() = DirectCast(Params(0), Byte())
        Dim AesIV As Byte() = DirectCast(Params(1), Byte())

        client.UserState = New User(AesKey, AesIV)

        SendExchangePacket(client)
    End Sub

    Public Sub HandleAuthenticatePacket(client As ServerClient, values As Object())
        Dim Username As String = DirectCast(values(1), String)

        If Not ValidateUsername(Username) Then
            client.Disconnect()
            Return
        End If

        If FindUserSocket(Username) IsNot Nothing Then
            SendAuthenticatePacket(client, ResponseCode.UsernameInUse)
            client.Disconnect()
            Return
        End If

        If BypassMaster Then
            AuthenticateUser(client, Username)
        Else
            'TODO: Spawn ThreadPool.Thread and authenticate user with server.
        End If
    End Sub

    Public Sub HandleChannelChatPacket(client As ServerClient, values As Object())
        Dim Message As String = DirectCast(values(1), String)

        If Not ValidateMultilineMessage(Message, 1, 512) Then
            client.Disconnect()
            Return
        End If

        SendChanelChatPacket(client, client.UserState.ID, Message)
    End Sub

End Module
