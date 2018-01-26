Imports System.IO

Public Class Form1
    WithEvents Ghost As New Ghost

    Shadows Location As String
    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        Dim T As New OpenFileDialog
        T.Filter = "Visual Basic .Net File|*.vb"
        If T.ShowDialog = Windows.Forms.DialogResult.OK Then
            Location = T.FileName
            TextBox1.Text = Location

            BeginProcessing()
            Dim L As New Threading.Thread(AddressOf Ghost.Load)
            L.Start(File.ReadAllText(Location))
        End If
        T.Dispose()
        Button1.Enabled = Not String.IsNullOrEmpty(Location) And Not String.IsNullOrEmpty(Destination)
    End Sub

    Dim Destination As String
    Private Sub TextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        Dim T As New SaveFileDialog
        T.Filter = "Visual Basic .Net File|*.vb"
        If T.ShowDialog = Windows.Forms.DialogResult.OK Then
            Destination = T.FileName
            TextBox2.Text = Destination
        End If
        T.Dispose()
        Button1.Enabled = Not String.IsNullOrEmpty(Location) And Not String.IsNullOrEmpty(Destination)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Names, Strings As New List(Of String)

        For Each I As ListViewItem In ListView1.Items
            If I.Checked Then Names.Add(I.Text)
        Next

        For Each I As ListViewItem In ListView2.Items
            If I.Checked Then Strings.Add(I.Text)
        Next

        Ghost.NameExclusion = Names.ToArray
        Ghost.StringExclusion = Strings.ToArray

        Ghost.Options(0) = CheckBox1.Checked
        Ghost.Options(1) = CheckBox2.Checked
        Ghost.Options(2) = CheckBox3.Checked
        Ghost.Options(3) = CheckBox4.Checked
        Ghost.Options(4) = CheckBox5.Checked

        BeginProcessing()
        Dim T As New Threading.Thread(AddressOf Ghost.Process)
        T.Start()
    End Sub

    Private Sub Ghost_Complete(ByVal code As String, ByVal mode As Integer) Handles Ghost.Complete
        If mode = 0 Then
            Invoke(Sub()
                       Timer1.Stop()
                       TextBox1.Enabled = True
                       Button1.Enabled = Not String.IsNullOrEmpty(Location) And Not String.IsNullOrEmpty(Destination)
                       ListView1.BeginUpdate()
                       ListView1.Items.Clear()
                       For Each M As String In Ghost.NameMatches
                           ListView1.Items.Add(M)
                       Next
                       ListView1.EndUpdate()
                       Label3.Text = "Names Found: " & Ghost.NameMatches.Count.ToString("N0")

                       ListView2.BeginUpdate()
                       ListView2.Items.Clear()
                       For Each M As String In Ghost.StringMatches
                           ListView2.Items.Add(M)
                       Next
                       ListView2.EndUpdate()
                       Label4.Text = "Strings Found: " & Ghost.StringMatches.Count.ToString("N0")
                   End Sub)
        Else
            File.WriteAllText(Destination, code)
            Invoke(Sub()
                       Timer1.Stop()
                       TextBox1.Enabled = True
                       Button1.Enabled = True
                       Label6.Text = "Numbers Found: " & Ghost.Numbers.ToString("N0")
                       Label7.Text = "Equations Tried: " & Ghost.Equations.ToString("N0")
                   End Sub)
        End If
    End Sub

    Private Sub Ghost_ProgressCurrent(ByVal current As Integer) Handles Ghost.ProgressCurrent
        ProgressBar1.Invoke(Sub(x As Integer)
                                ProgressBar1.Value = x
                            End Sub, current)
    End Sub
    Private Sub Ghost_ProgressMaximum(ByVal maximum As Integer) Handles Ghost.ProgressMaximum
        ProgressBar1.Invoke(Sub(x As Integer)
                                ProgressBar1.Value = 0
                                ProgressBar1.Maximum = x
                            End Sub, maximum)
    End Sub
    Private Sub Ghost_Status(ByVal status As String) Handles Ghost.Status
        Label5.Invoke(Sub(x As String)
                          Label5.Text = "Status: " & x
                      End Sub, status)
    End Sub

    Sub BeginProcessing()
        TextBox1.Enabled = False
        Button1.Enabled = False
        SetTime(0, "Second")
        Begin = Date.Now
        Timer1.Start()
    End Sub

    Private Begin As Date, TS As TimeSpan
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TS = Date.Now - Begin
        If TS.TotalDays >= 1 Then
            SetTime(TS.TotalDays, "Day")
        ElseIf TS.TotalHours >= 1 Then
            SetTime(TS.TotalHours, "Hour")
        ElseIf TS.TotalMinutes >= 1 Then
            SetTime(TS.TotalMinutes, "Minute")
        Else
            SetTime(TS.TotalSeconds, "Second")
        End If
        Label6.Text = "Numbers Found: " & Ghost.Numbers.ToString("N0")
        Label7.Text = "Equations Tried: " & Ghost.Equations.ToString("N0")
    End Sub

    Sub SetTime(ByVal count As Double, ByVal type As String)
        Label8.Text = String.Format("Processing Time: {0} {1}(s)", CInt(count), type)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
    End Sub
End Class
