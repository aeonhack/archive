Module Protocol

    Public Const AES_BLOCKSIZE As Integer = 256
    Public Const RSA_KEYSIZE As Integer = 4096

    Public ProtocolVersion As New Version("1.0.0.0")

    Enum PacketHeader As Byte
        ServerState = 0
        Initialize = 1
        Exchange = 2
        Authenticate = 3
        UserList = 4
        UserLeave = 5
        UserJoin = 6
        ChannelChat = 7
    End Enum

    Enum ResponseCode As Byte
        UsernameInUse = 0
        FailedToAuthenticate = 1
    End Enum

End Module
