Public Class DataPacket
    Private _Command As String
    Public ReadOnly Property Command() As String
        Get
            Return _Command
        End Get
    End Property
    Private _Data As String
    Public ReadOnly Property Arguments(ByVal Count As Integer) As String()
        Get
            Dim Seperator() As Char = {CType("|", Char)}
            Return _Data.Split(Seperator, Count)
        End Get
    End Property
    Public Sub New(ByVal Data As String)
        Data = Data.Trim()
        If Data.StartsWith("/") Then
            Dim Split As Integer = Data.IndexOf("|")
            If Split = -1 Then
                _Command = Data.Substring(1) : _Data = ""
            Else
                _Command = Data.Substring(1, Split - 1) : _Data = Data.Substring(Split + 1)
            End If
        End If
    End Sub
End Class
