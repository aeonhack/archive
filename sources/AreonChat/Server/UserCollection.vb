Public Class UserCollection
    Inherits Hashtable
    Public Shadows Sub AddUser(ByVal ID As Guid)
        If ContainsKey(ID) Then Remove(ID)
        Dim X As New User(ID)
        X.Username = GenerateName() : Add(ID, X)
    End Sub
    Public Sub RemoveUser(ByVal ID As Guid)
        If ContainsKey(ID) Then Remove(ID)
    End Sub
    Public Function FindUser(ByVal Name As String) As Guid
        For Each X In Values
            If X.UserName.ToLower = Name.ToLower Then Return X.UserID
        Next
        Return Guid.Empty
    End Function
    Private Function GenerateName() As String
        Dim I As Integer = 100
        Do Until Not HasName("Guest" & I) : I += 1 : Loop
        Return "Guest" & I
    End Function
    Public Function HasName(ByVal Name As String) As Boolean
        For Each X In Values
            If X.UserName.ToLower = Name.ToLower Then Return True
        Next
        Return False
    End Function
End Class
Public Class User
    Dim _Username, _Signature, _IP, _LastMessage As String
    Dim _Rank, _FloodCount As Short : Dim _UserID As Guid
    Dim _LoggedIn As Boolean : Dim _LastSend As DateTime
    Dim _Hidden As Boolean
    Property Hidden() As Boolean
        Get
            Return _Hidden
        End Get
        Set(ByVal Value As Boolean)
            _Hidden = Value
        End Set
    End Property
    Property LastMessage() As String
        Get
            Return _LastMessage
        End Get
        Set(ByVal Value As String)
            _LastMessage = Value
        End Set
    End Property
    Property LastSend() As DateTime
        Get
            Return _LastSend
        End Get
        Set(ByVal Value As DateTime)
            _LastSend = Value
        End Set
    End Property
    Property FloodCount() As Short
        Get
            Return _FloodCount
        End Get
        Set(ByVal Value As Short)
            _FloodCount = Value
        End Set
    End Property
    Property IP() As String
        Get
            Return _IP
        End Get
        Set(ByVal Value As String)
            _IP = Value
        End Set
    End Property
    Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal Value As String)
            _Username = Value
        End Set
    End Property
    Property Rank() As Short
        Get
            Return _Rank
        End Get
        Set(ByVal Value As Short)
            _Rank = Value
        End Set
    End Property
    ReadOnly Property UserID() As Guid
        Get
            Return _UserID
        End Get
    End Property
    ReadOnly Property Signature() As String
        Get
            Return _Signature
        End Get
    End Property
    ReadOnly Property LoggedIn() As Boolean
        Get
            Return _LoggedIn
        End Get
    End Property
    Public Sub New(ByVal ID As Guid)
        _UserID = ID : _LastSend = Now
    End Sub
    Public Sub Login(ByVal Name As String, ByVal Signature As String)
        _Username = Name : _Signature = Signature : _LoggedIn = True
    End Sub
End Class
