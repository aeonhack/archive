Imports System.Text.RegularExpressions

Public Class StringHandler

    Public Function ValidateUsername(value As String) As Boolean
        Return Regex.IsMatch(value, "[a-zA-Z0-9-_'\.]{1,14}")
    End Function

    Public Function SanitizeUsername(value As String) As String
        Return Regex.Replace(value, "[^a-zA-Z0-9-_'\.]", String.Empty)
    End Function

    Public Function ValidateMessage(value As String, minLength As Integer, maxLength As Integer) As Boolean
        If value.Length < minLength Then Return False
        If value.Length > maxLength Then Return False

        Return Regex.IsMatch(value, "[\s\w\x20-\x7E]+")
    End Function

    Public Function SanitizeMessage(value As String) As String
        Return Regex.Replace(value, "[^\s\w\x20-\x7E]", String.Empty)
    End Function

End Class
