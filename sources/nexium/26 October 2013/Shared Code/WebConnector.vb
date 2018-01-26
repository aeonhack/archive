Imports System.Collections.Specialized
Imports System.Net
Imports System.Threading

Public NotInheritable Class WebConnector

    Public Event RequestCompleted(sender As Object, success As Boolean, response As Byte(), userState As Object)

    Private _NumberOfRetries As Integer = 3
    Public Property NumberOfRetries() As Integer
        Get
            Return _NumberOfRetries
        End Get
        Set(ByVal value As Integer)
            _NumberOfRetries = value
        End Set
    End Property

    Private Cookies As New CookieContainer

    Public Sub BeginGetWebResponse(url As String, nameValues As NameValueCollection, userState As Object)
        ThreadPool.QueueUserWorkItem(AddressOf DoGetWebResponse, New Object() {url, nameValues, userState})
    End Sub

    Private Sub DoGetWebResponse(param As Object)
        Dim Params As Object() = DirectCast(param, Object())

        Dim URL As String = DirectCast(Params(0), String)
        Dim NameValues As NameValueCollection = DirectCast(Params(1), NameValueCollection)

        Dim Data As Byte() = Nothing
        For I As Integer = 1 To NumberOfRetries
            Data = TryGetWebResponse(URL, NameValues)
            If Data IsNot Nothing Then Exit For
        Next

        RaiseEvent RequestCompleted(Me, (Data Is Nothing), Data, Params(2))
    End Sub

    Private Function TryGetWebResponse(url As String, nameValues As NameValueCollection) As Byte()
        Try
            Dim W As New CookieWebClient()
            W.Cookies = Cookies

            Return W.UploadValues(url, nameValues)
        Catch
            Return Nothing
        End Try
    End Function

End Class

Public NotInheritable Class CookieWebClient
    Inherits WebClient

    Public Cookies As New CookieContainer

    Private Request As HttpWebRequest
    Protected Overrides Function GetWebRequest(address As Uri) As WebRequest
        Request = DirectCast(MyBase.GetWebRequest(address), HttpWebRequest)
        Request.Timeout = 10000
        Request.ReadWriteTimeout = 5000
        Request.KeepAlive = False
        Request.CookieContainer = Cookies
        Request.Proxy = Nothing
        Return Request
    End Function

End Class