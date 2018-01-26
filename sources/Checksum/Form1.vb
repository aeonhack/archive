Imports System.IO, System.Security.Cryptography

Public Class Form1

    Enum Hasher As Byte
        MD5
        SHA1
        SHA256
        SHA384
        SHA512
        RIPEMD160
    End Enum

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        Base64 = RadioButton2.Checked
        Results(Base64)
    End Sub

    Private Sub Label8_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Label8.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub Label8_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles Label8.DragDrop
        Dim F As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())(0)
        Dim I As New FileInfo(F)
        If Not I.Exists Then Return

        TextBox7.Clear()
        Label8.Text = "Filename: " & I.Name & Environment.NewLine & "File Size: " & Format(I.Length)

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        Label8.Enabled = False

        Dim T As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf Process))
        T.Start(F)
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Label8.Text = String.Empty
        Dim H As HashAlgorithm = Nothing

        For I As Integer = 0 To 5
            H = HashAlgorithm.Create([Enum].GetName(GetType(Hasher), I))
            HT(I) = H.ComputeHash(System.Text.Encoding.UTF8.GetBytes(TextBox7.Text))
        Next

        H.Clear()

        Results(Base64)
    End Sub

    Dim HT(5)() As Byte, Base64 As Boolean
    Sub Process(ByVal path As Object)
        Dim T As New FileStream(DirectCast(path, String), FileMode.Open, FileAccess.Read)
        Dim H As HashAlgorithm = Nothing

        For I As Integer = 0 To 5
            H = HashAlgorithm.Create([Enum].GetName(GetType(Hasher), I))
            HT(I) = H.ComputeHash(T)
            T.Position = 0
            CB(I)
        Next

        T.Close()
        H.Clear()

        Results(Base64)
    End Sub

    Sub CB(ByVal progress As Integer)
        If InvokeRequired Then
            Invoke(New Action(Of Integer)(AddressOf CB), progress)
        Else
            ProgressBar1.Value = progress + 1
            If progress = 5 Then Label8.Enabled = True
        End If
    End Sub

    Sub Results(ByVal base64 As Boolean)
        If HT(0) Is Nothing Then Return

        If InvokeRequired Then
            Invoke(New Action(Of Boolean)(AddressOf Results), base64)
        Else
            TextBox1.Text = Format(HT(0), base64)
            TextBox2.Text = Format(HT(1), base64)
            TextBox3.Text = Format(HT(2), base64)
            TextBox4.Text = Format(HT(3), base64)
            TextBox5.Text = Format(HT(4), base64)
            TextBox6.Text = Format(HT(5), base64)
        End If
    End Sub
    Function Format(ByVal data As Byte(), ByVal base64 As Boolean) As String
        If base64 Then Return Convert.ToBase64String(data) Else Return BitConverter.ToString(data).Replace("-", String.Empty)
    End Function
    Function Format(ByVal size As Double) As String
        If size >= 2 ^ 30 Then Return FormatNumber(size / 2 ^ 30, 2) & " GB (" & FormatNumber(size, 0) & " Bytes)"
        If size >= 2 ^ 20 Then Return FormatNumber(size / 2 ^ 20, 2) & " MB (" & FormatNumber(size, 0) & " Bytes)"
        Return FormatNumber(size / 2 ^ 10, 2) & " KB (" & FormatNumber(size, 0) & " Bytes)"
    End Function
End Class
