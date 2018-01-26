Public Class Settings
    Dim T As New KeysConverter
    Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.ShiftKey, Keys.ControlKey, Keys.Menu
                Return
            Case Else
                sender.Text = T.ConvertToString(e.KeyCode)
        End Select
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each C In Controls
            If TypeOf C Is TextBox Then AddHandler DirectCast(C, TextBox).KeyDown, AddressOf TextBox_KeyDown
        Next
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Hide()
    End Sub
    'Don't even bother trying to understand this.
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Main.System = CheckBox22.Checked
        For Each C In Controls
            If TypeOf C Is TextBox Then
                If C.Text <> "" Then
                    Dim ID = Val(C.Name.Substring(7)) - 1, M(3) As Main.Shortcut.Modifier
                    For I = 0 To 2
                        If GetControl((ID + 1) * 3 - I).Checked Then
                            Select Case I
                                Case 0 : M(0) = 2
                                Case 1 : M(1) = 4
                                Case 2 : M(2) = 1
                            End Select
                        Else
                            M(I) = 0
                        End If
                    Next
                    If Main.Shortcuts(ID) IsNot Nothing Then Main.Shortcuts(ID).Unregister()
                    Dim X As New Main.Shortcut
                    X.Register(M(0) Or M(1) Or M(2), T.ConvertFromString(C.Text))
                    AddHandler X.Pressed, AddressOf Main.Shortcut_Pressed
                    Main.Shortcuts(ID) = X
                End If
            End If
        Next
        Hide()
    End Sub
    Function GetControl(ByVal name As String) As CheckBox
        For Each C In Controls
            If C.Name = "CheckBox" & name Then Return C
        Next : Return Nothing
    End Function
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each C In Controls
            If TypeOf C Is TextBox Then DirectCast(C, TextBox).Clear()
            If TypeOf C Is CheckBox Then DirectCast(C, CheckBox).Checked = False
        Next
    End Sub
    Private Sub Settings_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Hide()
    End Sub
    Private Sub Settings_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Visible Then
            For Each S In Main.Shortcuts
                If S IsNot Nothing Then S.Unregister()
            Next
        End If
    End Sub
End Class