Imports System.Security.Cryptography

Public Class User

    Private _ID As Integer
    Public ReadOnly Property ID() As Integer
        Get
            Return _ID
        End Get
    End Property

    Private _Name As String
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Private _Rank As Byte
    Public ReadOnly Property Rank() As Byte
        Get
            Return _Rank
        End Get
    End Property

    Sub New(id As Integer, name As String, rank As Byte)
        _ID = id
        _Name = name
        _Rank = rank
    End Sub

End Class
