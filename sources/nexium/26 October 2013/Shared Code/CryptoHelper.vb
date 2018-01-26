Imports System.Security.Cryptography

Public Class CryptoHelper

    Private Shared RandomGUID As New Guid("90B89667-E86E-44F4-9EAD-5CA822E5FFE6")

    Public Shared Function HashPassword(uid As String, pass As String) As String
        Dim R As New Rfc2898DeriveBytes(uid.ToLower() & pass, RandomGUID.ToByteArray())
        Return BitConverter.ToString(R.GetBytes(32)).Replace("-", String.Empty)
    End Function

End Class
