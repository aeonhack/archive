Imports System.Security.Cryptography

Public Class User

    Public ID As Integer
    Public Name As String

    Public Rank As Byte
    Public Authenticated As Boolean

    Public Sub Authenticate(_id As Integer, _name As String)
        ID = _id
        Name = _name
        Authenticated = True
    End Sub

#Region " Encryption / Decryption "

    Private Encryptor As ICryptoTransform
    Private Decryptor As ICryptoTransform

    Sub New(key As Byte(), iv As Byte())
        Dim AesProvider As New RijndaelManaged()
        AesProvider.BlockSize = AES_BLOCKSIZE

        AesProvider.Key = key
        AesProvider.IV = iv

        Encryptor = AesProvider.CreateEncryptor()
        Decryptor = AesProvider.CreateDecryptor()
    End Sub

    Public Function Encrypt(data As Byte()) As Byte()
        Return Encryptor.TransformFinalBlock(data, 0, data.Length)
    End Function

    Public Function Decrypt(data As Byte()) As Byte()
        Return Decryptor.TransformFinalBlock(data, 0, data.Length)
    End Function

#End Region

End Class
