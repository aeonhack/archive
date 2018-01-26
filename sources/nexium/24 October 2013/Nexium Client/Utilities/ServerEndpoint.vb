Public Class ServerEndpoint

    Private _Host As String
    Public ReadOnly Property Host() As String
        Get
            Return _Host
        End Get
    End Property

    Private _Port As UShort
    Public ReadOnly Property Port() As UShort
        Get
            Return _Port
        End Get
    End Property

    Sub New(host As String, port As UShort)
        _Host = host.Trim().ToLower()
        _Port = port
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim T As ServerEndpoint = DirectCast(obj, ServerEndpoint)
        Return (T.Host = Host) AndAlso (T.Port = Port)
    End Function

    Public Overrides Function ToString() As String
        Return String.Format("{0}:{1}", Host, Port)
    End Function

    Public Shared Function Parse(value As String) As ServerEndpoint
        If Not value.Contains(":") Then Return Nothing

        Dim Index As Integer = value.LastIndexOf(":"c)
        Dim _Host As String = value.Remove(Index)
        Dim _Port As String = value.Substring(Index + 1)

        Dim Port As UShort
        If Not UShort.TryParse(_Port, Port) Then Return Nothing

        Return New ServerEndpoint(_Host, Port)
    End Function

End Class
