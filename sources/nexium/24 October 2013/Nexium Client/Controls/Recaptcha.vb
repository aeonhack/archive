Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions

Public Class ReCaptcha
    Inherits Control

    Public Event ChallengeChange(ByVal sender As Object)

    Private _PublicKey As String
    Property PublicKey() As String
        Get
            Return _PublicKey
        End Get
        Set(ByVal value As String)
            _PublicKey = value
        End Set
    End Property

    Private _Challenge As String
    ReadOnly Property Challenge As String
        Get
            Return _Challenge
        End Get
    End Property

    Private P1 As Pen = Pens.Black
    Property BorderColor() As Color
        Get
            Return P1.Color
        End Get
        Set(ByVal v As Color)
            P1 = New Pen(v)
            Invalidate()
        End Set
    End Property

    Private Processing As Boolean
    Private ChallengeImage As Image

    Private Client As New CookieWebClient

    Private Const RC_Challenge As String = "http://www.google.com/recaptcha/api/challenge?k="
    Private Const RC_Location As String = "http://www.google.com/recaptcha/api/image?c="

    Private Sub StringDownloaded(ByVal s As Object, ByVal e As DownloadStringCompletedEventArgs)
        _Challenge = Regex.Match(e.Result, "nge : '(.+)'").Groups(1).Value
        Client.DownloadDataAsync(New Uri(RC_Location & _Challenge))
    End Sub

    Private Sub DataDownloaded(ByVal s As Object, ByVal e As DownloadDataCompletedEventArgs)
        Dim M As New MemoryStream(e.Result)
        ChallengeImage = Image.FromStream(M)
        M.Close()

        Processing = False
        Invalidate()

        RaiseEvent ChallengeChange(Me)
    End Sub

    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)

        AddHandler Client.DownloadStringCompleted, AddressOf StringDownloaded
        AddHandler Client.DownloadDataCompleted, AddressOf DataDownloaded

        Size = New Size(302, 59)
    End Sub

    Public Sub GenerateCaptcha()
        If String.IsNullOrEmpty(_PublicKey) OrElse Processing Then Return
        Processing = True

        Client.DownloadStringAsync(New Uri(RC_Challenge & _PublicKey))
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        GenerateCaptcha()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.Clear(BackColor)

        If ChallengeImage IsNot Nothing Then
            e.Graphics.DrawImage(ChallengeImage, 0, 0, Width, Height)
        End If

        e.Graphics.DrawRectangle(P1, 0, 0, Width - 1, Height - 1)
    End Sub

End Class