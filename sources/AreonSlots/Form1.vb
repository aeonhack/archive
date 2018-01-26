Public Class Form1
    Dim Cash As Double = 3
    Friend User As String
    Private Sub Pull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pull.Click
        Dim Bet As Double
        Dim S1 As Integer, S2 As Integer, S3 As Integer
        Bet = Val(Dollars.Text) + (Cents.Text / 100)
        If Bet < 0.25 Then
            MessageBox.Show("You must bet a minimum of 25¢.", "Minimum", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If Bet > Cash Then
            MessageBox.Show("You can't bet more then you have.", "Insufficient Funds", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Cash -= Bet
        Pull.Enabled = False
        Dollars.Enabled = False
        Cents.Enabled = False
        Wallet.Text = FormatCurrency(Cash)
        Me.Refresh()
        For I = 0 To 17
            S1 = RandomSlot()
            My.Computer.Audio.Play(My.Resources.Spin, AudioPlayMode.WaitToComplete)
            AssignSlot(S1, 1)
            Slot1.Refresh()
            Threading.Thread.Sleep(I)
        Next
        For I = 0 To 17
            S2 = RandomSlot()
            My.Computer.Audio.Play(My.Resources.Spin, AudioPlayMode.WaitToComplete)
            AssignSlot(S2, 2)
            Slot2.Refresh()
            Threading.Thread.Sleep(I)
        Next
        For I = 0 To 17
            S3 = RandomSlot()
            My.Computer.Audio.Play(My.Resources.Spin, AudioPlayMode.WaitToComplete)
            AssignSlot(S3, 3)
            Slot3.Refresh()
            Threading.Thread.Sleep(I)
        Next
        If S1 = 4 And S2 = 4 And S3 = 4 And Cash >= 10 Then Cash = 0
        Dim Payout As Double = GetPayout(S1, S2, S3, Bet)
        If Payout > 0 Then
            My.Computer.Audio.Play(My.Resources.Win, AudioPlayMode.Background)
            Cash += Payout
        End If
        Wallet.Text = FormatCurrency(Cash)
        Pull.Enabled = True
        Dollars.Enabled = True
        Cents.Enabled = True
        If Cash < 0.25 Then EndGame()
    End Sub
    Private Function ChanceSuccess(ByVal Chance As Double) As Boolean
        Dim Random As New Random
        Return Random.Next(1, 1000000) / 10000 >= 100 - Chance
    End Function
    Private Function GetPayout(ByVal S1 As Integer, ByVal S2 As Integer, ByVal S3 As Integer, ByVal Bet As Double) As Double
        Dim Payout As Double
        If S1 = 1 And S2 = 1 And S3 = 1 Then
            Payout += 4 + (Bet * 0.06)
        ElseIf S1 = 1 And S2 = 1 Then
            Payout += (Bet * 0.03) + 0.2
        ElseIf S2 = 1 And S3 = 1 Then
            Payout += (Bet * 0.03) + 0.2
        ElseIf S3 = 1 And S1 = 1 Then
            Payout += (Bet * 0.03) + 0.2
        End If
        If S1 = 2 And S2 = 2 And S3 = 2 Then
            Payout += 8 + (Bet * 0.12)
        ElseIf S1 = 2 And S2 = 2 Then
            Payout += (Bet * 0.06) + 0.4
        ElseIf S2 = 2 And S3 = 2 Then
            Payout += (Bet * 0.06) + 0.4
        ElseIf S3 = 2 And S1 = 2 Then
            Payout += (Bet * 0.06) + 0.4
        End If
        If S1 = 3 And S2 = 3 And S3 = 3 Then
            Payout += 25 + (Bet * 0.24)
        ElseIf S1 = 3 Or S2 = 3 Or S3 = 3 Then
            If S1 = 3 Then Payout += (Bet * 0.03) + 0.8
            If S2 = 3 Then Payout += (Bet * 0.03) + 0.8
            If S3 = 3 Then Payout += (Bet * 0.03) + 0.8
        End If
        If S1 = 5 And S2 = 5 And S3 = 5 Then Payout += 1000 + (Bet * 5)
        Return Math.Round(Payout, 2)
    End Function
    Private Sub AssignSlot(ByVal ID As Integer, ByVal Slot As Integer)
        Select Case ID
            Case 1
                Select Case Slot
                    Case 1
                        Slot1.Image = My.Resources.Cherry
                    Case 2
                        Slot2.Image = My.Resources.Cherry
                    Case 3
                        Slot3.Image = My.Resources.Cherry
                End Select
            Case 2
                Select Case Slot
                    Case 1
                        Slot1.Image = My.Resources.Orange
                    Case 2
                        Slot2.Image = My.Resources.Orange
                    Case 3
                        Slot3.Image = My.Resources.Orange
                End Select
            Case 3
                Select Case Slot
                    Case 1
                        Slot1.Image = My.Resources.Diamond
                    Case 2
                        Slot2.Image = My.Resources.Diamond
                    Case 3
                        Slot3.Image = My.Resources.Diamond
                End Select
            Case 4
                Select Case Slot
                    Case 1
                        Slot1.Image = My.Resources.Skull
                    Case 2
                        Slot2.Image = My.Resources.Skull
                    Case 3
                        Slot3.Image = My.Resources.Skull
                End Select
            Case 5
                Select Case Slot
                    Case 1
                        Slot1.Image = My.Resources.Coins
                    Case 2
                        Slot2.Image = My.Resources.Coins
                    Case 3
                        Slot3.Image = My.Resources.Coins
                End Select
        End Select
    End Sub
    Private Function RandomSlot() As Integer
        Dim Random As New Random
        Dim Value As Integer = Random.Next(1, 6)
        Return Value
    End Function
    Private Sub SetScore()
        Dim Score As String = User & "|" & Cash
        If My.Settings.HighScore = Nothing Then My.Settings.HighScore = Score
        If Val(My.Settings.HighScore.Split("|").GetValue(1)) < Cash Then
            My.Settings.HighScore = Score
        End If
        My.Settings.Save()
    End Sub
    Private Sub EndGame()
        MessageBox.Show("Game Over." & vbCrLf & vbCrLf & "You have either gone bankrupt or quit.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information)
        SetScore()
        Cash = 3
        Wallet.Text = "$3.00"
        AssignSlot(5, 3)
        AssignSlot(5, 2)
        AssignSlot(5, 1)
        If My.Settings.HighScore = Nothing Then
            Me.Height = 208
        Else
            Label1.Text = My.Settings.HighScore.Split("|").GetValue(0) & " is the best player with " & FormatCurrency(My.Settings.HighScore.Split("|").GetValue(1)) & "."
            Me.Height = 230
        End If
        User = Nothing
        Dim NewUser As New Form2
        NewUser.ShowDialog()
        NewUser.TopLevel = False
        NewUser.Parent = Me
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Settings.HighScore = Nothing Then
            Me.Height = 206
        Else
            Label1.Text = My.Settings.HighScore.Split("|").GetValue(0) & " is the best player with " & FormatCurrency(My.Settings.HighScore.Split("|").GetValue(1)) & "."
            Me.Height = 228
        End If
    End Sub
    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim NewUser As New Form2
        NewUser.ShowDialog()
        NewUser.TopLevel = False
        NewUser.Parent = Me
    End Sub
    Private Sub Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Quit.Click
        EndGame()
    End Sub
    Private Sub Cents_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cents.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then e.Handled = True
    End Sub
    Private Sub Dollars_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dollars.KeyPress
        If Char.IsNumber(e.KeyChar) = False And Asc(e.KeyChar) = 8 = False Then e.Handled = True
    End Sub
    Private Sub Scores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Scores.Click
        Dim NewUser As New Form3
        NewUser.ShowDialog()
        NewUser.TopLevel = False
        NewUser.Parent = Me
    End Sub
End Class
