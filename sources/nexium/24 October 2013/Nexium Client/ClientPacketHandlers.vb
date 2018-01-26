Imports System.Security.Cryptography

Module ClientPacketHandlers

    Public Sub HandleServerStatePacket(client As UserClient, values As Object())
        Dim ServerProtocol As Version = New Version(DirectCast(values(1), String))

        If ProtocolVersion = ServerProtocol Then
            Dim BypassMaster As Boolean = DirectCast(values(2), Boolean)

            Dim ServerName As String = SanitizeMessage(DirectCast(values(3), String))
            Dim ServerMotto As String = SanitizeMessage(DirectCast(values(4), String))

            Dim Users As UShort = DirectCast(values(5), UShort)
            Dim MaxUsers As UShort = DirectCast(values(6), UShort)

            If ServerName.Length < 4 OrElse ServerName.Length > 44 Then Return
            If ServerMotto.Length > 120 Then Return

            If MaxUsers < 1 OrElse MaxUsers > 200 Then Return
            If Users < 0 OrElse Users > MaxUsers Then Return

            UsersOnline += Users

            ClientMain.AddToServerBrowser(client.EndPoint, ServerProtocol, BypassMaster, ServerName, ServerMotto, Users, MaxUsers)
        Else
            ClientMain.AddToServerBrowser(client.EndPoint, ServerProtocol, True, String.Empty, String.Empty, 0, 0)
        End If
    End Sub

    Public Sub HandleInitializePacket(values As Object())
        Dim RsaPublic As Byte() = DirectCast(values(1), Byte())
        Dim MaxPacketSize As Integer = DirectCast(values(2), Integer)

        Client.MaxPacketSize = Math.Min(Math.Max(MaxPacketSize, 1024), 104857600)

        Dim RsaProvider As New RSACryptoServiceProvider(RSA_KEYSIZE)
        RsaProvider.ImportCspBlob(RsaPublic)

        Dim Aes As Byte() = RsaProvider.Encrypt(Pack.Serialize(AesKey, AesIV), True)

        SendExchangePacket(Aes)
        SecureConnection = True
    End Sub

    Public Sub HandleExchangePacket()
        SendAuthenticatePacket()
    End Sub

    Public Sub HandleAuthenticatePacket(values As Object())
        Select Case DirectCast(values(1), ResponseCode)
            Case ResponseCode.UsernameInUse
                MessageBox.Show(String.Format("Unable to enter channel as the name '{0}' is already logged in.", Username), "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Client.Disconnect()
        End Select
    End Sub

    Public Sub HandleUserListPacket(values As Object())
        Users.Clear()
        Dim Count As Integer = DirectCast(values(1), Integer)

        For I As Integer = 0 To Count - 1
            Dim Index As Integer = 2 + (I * 3)

            Dim ID As Integer = DirectCast(values(Index), Integer)
            Dim Name As String = DirectCast(values(Index + 1), String)
            Dim Rank As Byte = DirectCast(values(Index + 2), Byte)

            Users.Add(ID, New User(ID, Name, Rank))
        Next

        ClientMain.InvalidateUserBrowser()
    End Sub

    Public Sub HandleUserLeavePacket(values As Object())
        Dim ID As Integer = DirectCast(values(1), Integer)
        Users.Remove(ID)

        ClientMain.RemoveFromUserBrowser(ID)
    End Sub

    Public Sub HandleUserJoinPacket(values As Object())
        Dim ID As Integer = DirectCast(values(1), Integer)
        Dim Name As String = DirectCast(values(2), String)
        Dim Rank As Byte = DirectCast(values(3), Byte)

        Dim User As New User(ID, Name, Rank)
        Users.Add(ID, User)

        ClientMain.AddToUserBrowser(User)
    End Sub

    Public Sub HandleChannelChatPacket(values As Object())
        Dim Username As String = Users(DirectCast(values(1), Integer)).Name
        Dim Message As String = DirectCast(values(2), String)

        ClientMain.AppendChatMessage(Username, Message)
    End Sub

End Module