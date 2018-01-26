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

End Class
