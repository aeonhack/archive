Imports System.Net.Sockets, System.Threading, System.Net

Public Class Form1
    Private Function Resolve(ByVal host As String) As IPAddress
        Return Dns.GetHostEntry(host).AddressList(0)
    End Function
    Private Function Scan(ByVal host As IPAddress, ByVal port As Integer, ByVal timeout As Integer) As Boolean
        Using T As New Socket(2, 1, 6)
            Try
                T.BeginConnect(host, port, Nothing, 0)
                Dim I = Now.AddMilliseconds(timeout)
                While Now < I
                    Thread.Sleep(1)
                    If T.Connected Then Return True
                End While
            Catch : End Try
        End Using
    End Function

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Label1.Text = "Threads: " & TrackBar1.Value
    End Sub

    Dim Active As Boolean
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Active Then
            Toggle(True)
        Else
            Toggle(False)

            ListBox1.Items.Clear()
            ProgressBar1.Maximum = NumericUpDown2.Value + 1
            ProgressBar1.Minimum = NumericUpDown1.Value
            ProgressBar1.Value = NumericUpDown1.Value

            Try
                Dim Host = Resolve(TextBox1.Text)
                For O = 0 To TrackBar1.Value - 1
                    Dim T As New Thread(AddressOf DoScan)
                    T.Start(New Object() {NumericUpDown1.Value + O, NumericUpDown2.Value, TrackBar1.Value, Host, NumericUpDown3.Value})
                Next
            Catch: End Try
        End If
    End Sub
    Private Sub Toggle(ByVal b As Boolean)
        NumericUpDown1.Enabled = b
        NumericUpDown2.Enabled = b
        NumericUpDown3.Enabled = b
        TrackBar1.Enabled = b
        Button1.Text = If(b, "Begin", "Cancel")
        Active = Not b
    End Sub

    Private Sub DoScan(ByVal data As Object)
        For I = data(0) To data(1) Step data(2)
            If Not Active Then Exit For
            Output(Scan(data(3), I, data(4)), I)
        Next
    End Sub
    Delegate Sub OD(ByVal open As Boolean, ByVal port As Integer)
    Private Sub Output(ByVal open As Boolean, ByVal port As Integer)
        If InvokeRequired Then
            Invoke(New OD(AddressOf Output), open, port)
        Else
            If open Then ListBox1.Items.Add(port)
            ProgressBar1.Value += 1
            If ProgressBar1.Value = ProgressBar1.Maximum Then Toggle(True)
        End If
    End Sub
End Class
