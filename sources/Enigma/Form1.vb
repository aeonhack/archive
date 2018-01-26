Imports System.Text.RegularExpressions, System.Windows.Forms
Public Class Form1
    Function Strength(ByVal data As String) As Integer
        Dim S = New String() {"[a-z]", "[A-Z]", "\W", "\d"}
        For Each M In S
            If Regex.IsMatch(data, M) Then Strength += 22.5
        Next
        Return Strength + data.Length
    End Function
    Function Random(ByVal i As Integer)
        Dim T = New String() {"0123456789", "abcdefgijkmnopqrstwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "`~!@#$%^&*()_-+={[}]\:;""'<,>.?/"}
        Return T(i)(New Random(Seed).Next(T(i).Length))
    End Function
    Function Bad(ByVal password As String, ByVal ParamArray check As Boolean()) As Boolean
        Dim Flags(3) As Boolean
        For Each C In password
            If Char.IsNumber(C) Then Flags(0) = True
            If Char.IsLetter(C) Then Flags(1) = True
            If Char.IsSymbol(C) Then Flags(2) = True
            If Char.IsPunctuation(C) Then Flags(2) = True
        Next
        For I = 0 To 2
            If Flags(I) <> check(I) Then Return True
        Next
    End Function
    Function Seed() As Integer
        Seed = Guid.NewGuid.GetHashCode : Return If(Seed < 0, Seed * -1, Seed)
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CheckBox1.Checked Or CheckBox2.Checked Or CheckBox3.Checked Then
            Dim P = String.Empty

            Do

                P = String.Empty
                While P.Length < NumericUpDown1.Value
                    Select Case New Random(Seed).Next(3)
                        Case 0 : If CheckBox1.Checked Then P &= Random(0)
                        Case 1 : If CheckBox2.Checked Then P &= Random(1)
                        Case 2 : If CheckBox3.Checked Then P &= Random(2)
                    End Select
                End While

            Loop While Bad(P, CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked)

            TextBox1.Text = P
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim V = Strength(TextBox1.Text)
        ProgressBar1.Value = If(V > 100, 100, V)
    End Sub
    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        TextBox1.UseSystemPasswordChar = CheckBox4.Checked
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = TextBox1.Text.ToLower
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Using T As New SaveFileDialog
            T.Filter = "Text Document|*.txt"
            If T.ShowDialog = Windows.Forms.DialogResult.OK Then
                IO.File.WriteAllText(T.FileName, TextBox1.Text)
            End If
        End Using
    End Sub
End Class
