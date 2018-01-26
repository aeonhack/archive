Imports NATUPNPLib
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets

Public NotInheritable Class UPnPHelper
    Implements IDisposable

    Private UPnPNAT As UPnPNAT
    Private Mapping As IStaticPortMappingCollection

    Private _IPv4Address As IPAddress
    Public ReadOnly Property IPv4Address As IPAddress
        Get
            Return _IPv4Address
        End Get
    End Property

    Private _UPnPEnabled As Boolean = True
    Public ReadOnly Property UPnPEnabled As Boolean
        Get
            Return _UPnPEnabled
        End Get
    End Property

    Sub New()
        UPnPNAT = New UPnPNAT()

        Try
            Mapping = UPnPNAT.StaticPortMappingCollection()
            If Mapping Is Nothing Then _UPnPEnabled = False
        Catch ex As NotImplementedException
            _UPnPEnabled = False
        End Try

        _IPv4Address = IPv4Addresses()(0)
    End Sub

    Public Sub AddMapping(port As UShort, description As String)
        If MappingExists(port) Then Throw New Exception(String.Format("Port {0} has already been mapped.", port))
        Mapping.Add(port, "TCP", port, IPv4Address.ToString(), True, description)
    End Sub

    Public Sub RemoveMapping(port As UShort, Optional matchDescription As String = "")
        If Not UPnPEnabled Then Return

        If MappingExists(port) Then
            If Not String.IsNullOrEmpty(matchDescription) Then
                If Not Sanitize(Mapping(port, "TCP").Description) = Sanitize(matchDescription) Then Return
            End If

            Mapping.Remove(port, "TCP")
        End If
    End Sub

    Public Function MappingExists(port As UShort) As Boolean
        If Not UPnPEnabled Then Throw New Exception("UPnP is not enabled or initialization failed.")

        For Each M As IStaticPortMapping In Mapping
            If Sanitize(M.Protocol) = "TCP" AndAlso M.ExternalPort = port Then Return True
        Next

        Return False
    End Function

    Private Function Sanitize(protocol As String) As String
        Return protocol.Trim().ToUpper()
    End Function

    Private Function IPv4Addresses() As IPAddress()
        Dim T As New List(Of IPAddress)

        For Each I As IPAddress In Dns.GetHostEntry(Dns.GetHostName).AddressList
            If IsPrivateIPv4(I) Then T.Add(I)
        Next

        Return T.ToArray()
    End Function

    Private Function IsPrivateIPv4(ByVal address As IPAddress) As Boolean
        If Not address.AddressFamily = AddressFamily.InterNetwork Then Return False

        Dim I As Byte() = address.GetAddressBytes
        Return I(0) = 10 OrElse (I(0) = 172 AndAlso I(1) > 15 AndAlso I(1) < 32) OrElse (I(0) = 192 AndAlso I(1) = 168)
    End Function

    Private Sub Dispose(disposing As Boolean)
        Marshal.ReleaseComObject(Mapping)
        Marshal.ReleaseComObject(UPnPNAT)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class