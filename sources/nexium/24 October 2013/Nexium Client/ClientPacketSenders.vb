Module ClientPacketSenders

    Private Sub SendPacketEx(client As UserClient, ParamArray params As Object())
        Dim Data As Byte() = Pack.Serialize(params)
        client.Send(Data)
    End Sub

    Private Sub SendPacket(ParamArray params As Object())
        Dim Data As Byte() = Pack.Serialize(params)

        If SecureConnection Then
            Data = Encrypt(Data)
        End If

        Client.Send(Data)
    End Sub

    Public Sub SendServerStatePacket(client As UserClient)
        SendPacketEx(client, PacketHeader.ServerState)
    End Sub

    Public Sub SendInitializePacket()
        SendPacket(PacketHeader.Initialize)
    End Sub

    Public Sub SendExchangePacket(aes As Byte())
        SendPacket(PacketHeader.Exchange, aes)
    End Sub

    Public Sub SendAuthenticatePacket()
        SendPacket(PacketHeader.Authenticate, Username)
    End Sub

    Public Sub SendChanelChatPacket(message As String)
        SendPacket(PacketHeader.ChannelChat, message)
    End Sub

End Module