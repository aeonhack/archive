Imports System.Runtime.InteropServices

'------------------
'Creator: aeonhack
'Site: nimoru.com
'Created: 6/14/2013
'Changed: 6/14/2013
'Version: 1.0.0.0
'------------------
Public Class BetterRichTextBox
    Inherits RichTextBox

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage( _
    ByVal handle As IntPtr, _
    ByVal message As Integer, _
    ByVal wParam As Integer, _
    ByVal lParam As Integer) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="GetScrollInfo")> _
    Private Shared Function GetScrollInfo(
    ByVal handle As IntPtr, _
    ByVal bar As Integer, _
    ByRef info As SCROLLINFO) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SCROLLINFO
        Public size As UInteger
        Public mask As UInteger
        Public min As Integer
        Public max As Integer
        Public page As Integer
        Public position As Integer
        Public trackPosition As Integer
    End Structure

    Private Scroll As SCROLLINFO
    Private Function ScrolledToBottom() As Boolean
        Scroll = New SCROLLINFO()
        Scroll.size = CUInt(Marshal.SizeOf(Scroll))
        Scroll.mask = 7

        If Not GetScrollInfo(Handle, 1, Scroll) Then Return True
        Return (Scroll.page = 0) OrElse ((Scroll.page + Scroll.position) >= Scroll.max)
    End Function

    Private Sub ScrollToBottom()
        SendMessage(Handle, 277, 7, 0)
    End Sub

    Public Overloads Sub AppendText(ByVal color As Color, ByVal text As String)
        Dim Focus As Boolean = Focused

        Dim SelectionStart As Integer = Me.SelectionStart
        Dim SelectionLength As Integer = Me.SelectionLength

        Dim AutoScroll As Boolean = ScrolledToBottom()

        If Not AutoScroll Then
            If Focus Then Parent.Focus()
            SendMessage(Handle, 1087, 1, 0) 'Hide selection, prevents auto-scrolling
        End If

        Me.SelectionStart = TextLength
        Me.SelectionLength = 0
        Me.SelectionColor = color

        Me.AppendText(text)

        Me.SelectionColor = ForeColor
        Me.SelectionStart = SelectionStart
        Me.SelectionLength = SelectionLength

        If AutoScroll Then
            ScrollToBottom()
        Else
            SendMessage(Handle, 1087, 0, 0) 'Unhide selection
            If Focus Then Me.Focus()
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        If ScrolledToBottom() Then
            ScrollToBottom()
        End If

        MyBase.OnSizeChanged(e)
    End Sub

End Class
