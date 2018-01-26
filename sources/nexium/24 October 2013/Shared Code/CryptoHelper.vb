Imports System.Security.Cryptography

Public Class CryptoHelper

    Private Shared RandomGUID As New Guid("BEEDD630-8D10-4F53-AC66-29782C3EE955")

    Public Shared Function HashPassword(uid As String, pass As String) As String
        Dim R As New Rfc2898DeriveBytes(uid.ToLower() & pass, RandomGUID.ToByteArray(), 32)
        Return BitConverter.ToString(R.GetBytes(32)).Replace("-", String.Empty)
    End Function

End Class
