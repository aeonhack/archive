Public Class Form1
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Areon Manual " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor
        On Error Resume Next
        My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\Virtual-Key Codes.html", My.Resources.Virtual_Key_Codes, False)
        My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\Ascii Codes.html", My.Resources.Ascii_Codes, False)
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        On Error Resume Next
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Temp & "\Virtual-Key Codes.html") Then My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\Virtual-Key Codes.html")
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Temp & "\Ascii Codes.html") Then My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\Ascii Codes.html")
    End Sub
    Private Sub Listbox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Listbox1.SelectedIndexChanged
        Select Case Listbox1.SelectedItem
            Case "Ascii Codes"
                Browser.Show()
                Browser.WebBrowser1.Navigate(My.Computer.FileSystem.SpecialDirectories.Temp & "\Ascii Codes.html")
            Case "Virtual-Key Codes"
                Browser.Show()
                Browser.WebBrowser1.Navigate(My.Computer.FileSystem.SpecialDirectories.Temp & "\Virtual-Key Codes.html")
            Case "Add Startup"
                RichTextBox1.Rtf = My.Resources.AddStartup
            Case "Binary"
                RichTextBox1.Rtf = My.Resources.Binary
            Case "Block Input"
                RichTextBox1.Rtf = My.Resources.BlockInput
            Case "Borderless Form"
                RichTextBox1.Rtf = My.Resources.BorderlessForm
            Case "Chance Success"
                RichTextBox1.Rtf = My.Resources.ChanceSuccess
            Case "Convert IP"
                RichTextBox1.Rtf = My.Resources.ConvertIP
            Case "Count Words"
                RichTextBox1.Rtf = My.Resources.CountWords
            Case "Data Types"
                RichTextBox1.Rtf = My.Resources.DataTypes
            Case "End Process"
                RichTextBox1.Rtf = My.Resources.EndProcess
            Case "Fade"
                RichTextBox1.Rtf = My.Resources.Fade
            Case "Generate Serial"
                RichTextBox1.Rtf = My.Resources.GenerateSerial
            Case "Get Key"
                RichTextBox1.Rtf = My.Resources.GetKey
            Case "Get Percent"
                RichTextBox1.Rtf = My.Resources.GetPercent
            Case "Machine ID"
                RichTextBox1.Rtf = My.Resources.MachineID
            Case "Math"
                RichTextBox1.Rtf = My.Resources.Math
            Case "MD5"
                RichTextBox1.Rtf = My.Resources.MD5
            Case "Object Type"
                RichTextBox1.Rtf = My.Resources.ObjectType
            Case "Parse IP"
                RichTextBox1.Rtf = My.Resources.ParseIP
            Case "Parse Lines"
                RichTextBox1.Rtf = My.Resources.ParseLines
            Case "Random Output"
                RichTextBox1.Rtf = My.Resources.RandomOutput
            Case "Resolve DNS"
                RichTextBox1.Rtf = My.Resources.ResolveDNS
            Case "Scan Port"
                RichTextBox1.Rtf = My.Resources.ScanPort
            Case "Scramble"
                RichTextBox1.Rtf = My.Resources.Scramble
            Case "Send Gmail"
                RichTextBox1.Rtf = My.Resources.SendGmail
            Case "Set Wallpaper"
                RichTextBox1.Rtf = My.Resources.SetWallpaper
            Case "Sleep"
                RichTextBox1.Rtf = My.Resources.Sleep
            Case "Swap"
                RichTextBox1.Rtf = My.Resources.Swap
            Case "Text To Speech"
                RichTextBox1.Rtf = My.Resources.TextToSpeech
            Case "TripleDES"
                RichTextBox1.Rtf = My.Resources.TripleDES
            Case "UTF8"
                RichTextBox1.Rtf = My.Resources.UTF8
            Case "Validate Connection"
                RichTextBox1.Rtf = My.Resources.ValidateConnection
            Case "Validate Email"
                RichTextBox1.Rtf = My.Resources.ValidateEmail
            Case "Validate File"
                RichTextBox1.Rtf = My.Resources.ValidateFile
            Case "Validate Password"
                RichTextBox1.Rtf = My.Resources.ValidatePassword
            Case Else
                RichTextBox1.Text = Nothing
        End Select
        RichTextBox1.SelectAll()
        RichTextBox1.SelectionFont = Me.Font
        RichTextBox1.DeselectAll()
    End Sub
    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Listbox1.SelectedIndex = 0
    End Sub
End Class
