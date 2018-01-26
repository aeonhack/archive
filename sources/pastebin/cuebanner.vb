Imports System.ComponentModel
Imports System.Runtime.InteropServices

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 10/15/2011
'Changed: 10/15/2011
'Version: 1.0.0
'------------------
<ProvideProperty("CueBanner", GetType(TextBox))> _
Class CueBanner
    Inherits Component
    Implements IExtenderProvider

    <DllImport("user32.dll", CharSet:=CharSet.Auto, EntryPoint:="SendMessage")>
    Private Shared Function SetCueBanner(
        ByVal handle As IntPtr,
        ByVal message As UInteger,
        ByVal wparam As IntPtr,
        ByVal hint As String) As IntPtr
    End Function

    Private children As New Hashtable
    Private Function CanExtend(ByVal c As Object) As Boolean Implements IExtenderProvider.CanExtend
        If Not TypeOf c Is TextBox Then Return False

        If Not children.Contains(c) Then
            children.Add(c, String.Empty)
            HookCreation(DirectCast(c, TextBox))
        End If

        Return True
    End Function

    Private Sub HandleCreated(ByVal s As Object, ByVal e As EventArgs)
        SetBanner(DirectCast(s, TextBox))
    End Sub

    Function GetCueBanner(ByVal c As TextBox) As String
        Return DirectCast(children(c), String)
    End Function
    Sub SetCueBanner(ByVal c As TextBox, ByVal v As String)
        children(c) = v

        HookCreation(c)
        SetBanner(c)
    End Sub

    Private Sub HookCreation(ByVal c As TextBox)
        RemoveHandler c.HandleCreated, AddressOf HandleCreated
        AddHandler c.HandleCreated, AddressOf HandleCreated
    End Sub

    Private Sub SetBanner(ByVal c As TextBox)
        If Not c.IsHandleCreated Then Return
        SetCueBanner(c.Handle, 5377, IntPtr.Zero, GetCueBanner(c))
    End Sub

End Class
