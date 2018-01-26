Imports System.Text

Public Class SecureProperty

    Private Key As Byte()
    Private Data As Byte()

    Sub New()
        Key = Guid.NewGuid.ToByteArray()
    End Sub

    Public Function GetValue() As String
        If Data Is Nothing Then Return String.Empty
        Return Encoding.UTF8.GetString(RXOR(Data, Key))
    End Function

    Public Sub SetValue(value As String)
        Data = RXOR(Encoding.UTF8.GetBytes(value), Key)
    End Sub

    Private Shared Function RXOR(data As Byte(), key As Byte()) As Byte()
        Dim N1 As Integer = 11
        Dim N2 As Integer = 13
        Dim NS As Integer = 257

        For I As Integer = 0 To key.Length - 1
            NS += NS Mod (key(I) + 1)
        Next

        Dim T(data.Length - 1) As Byte
        For I As Integer = 0 To data.Length - 1
            NS = key(I Mod key.Length) + NS
            N1 = (NS + 5) * (N1 And 255) + (N1 >> 8)
            N2 = (NS + 7) * (N2 And 255) + (N2 >> 8)
            NS = ((N1 << 8) + N2) And 255

            T(I) = data(I) Xor CByte(NS)
        Next

        Return T
    End Function

End Class