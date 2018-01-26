Imports System.Net

Public Class ServerPreview

    Public Property EndPoint As IPEndPoint
    Public Property ServerProtocol As Version

    Public ReadOnly Property Compatible() As Boolean
        Get
            Return (ServerProtocol = ProtocolVersion)
        End Get
    End Property

    Public Property BypassMaster As Boolean
    Public Property ServerName As String
    Public Property ServerMotto As String
    Public Property Users As UShort
    Public Property MaxUsers As UShort

    Private _Selected As Boolean
    Public ReadOnly Property Selected() As Boolean
        Get
            Return _Selected
        End Get
    End Property

    Private Sub InvalidateUsers()
        Dim Maximum As Integer = Math.Min(MaxUsers, 999)
        Dim Current As Integer = Math.Min(Users, Maximum)

        Label3.Text = String.Format("{0} / {1}", Current, Maximum)
    End Sub

    Private Sub InvalidateLabels()
        If Compatible Then
            Label1.Text = ServerName
            Label2.Text = ServerMotto
        Else
            Label1.ForeColor = Color.Orange
            Label1.Text = "Server is using a different protocol version."
            Label2.Text = String.Format("{0}, Protocol: {1}", EndPoint, ServerProtocol)
        End If
    End Sub

    Private Sub InvalidateIcons()
        If BypassMaster Then
            PictureBox1.BackgroundImage = My.Resources.privateServer
        Else
            PictureBox1.BackgroundImage = My.Resources.publicServer
        End If
    End Sub

    Private Sub ServerPreview_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        InvalidateUsers()
        InvalidateLabels()
        InvalidateIcons()
    End Sub

    Private Sub ServerPreview_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        AddHandler e.Control.MouseDown, AddressOf CaptureFocus
    End Sub

    Private Sub CaptureFocus(sender As Object, e As MouseEventArgs)
        Focus()
    End Sub

    Private Sub ServerPreview_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        _Selected = True

        For Each C As Control In Parent.Controls
            If TypeOf C Is ServerPreview AndAlso C IsNot Me Then
                Dim S As ServerPreview = DirectCast(C, ServerPreview)
                S._Selected = False
                S.BackColor = Color.White
            End If
        Next

        BackColor = Color.FromArgb(160, 245, 255)
    End Sub

End Class
