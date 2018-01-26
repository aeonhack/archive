Public Class History
    Private Sub History_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For I = 1 To Main.HistoryC.Count
            Dim Text As String = Main.HistoryC.Item(I)
            ListView1.Items.Add(Text.Split("$").GetValue(0)).SubItems.Add(Text.Split("$").GetValue(1))
        Next
    End Sub
End Class