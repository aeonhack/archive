Module Main

    Private WithEvents Server As New ServerListener(Of User)

    Sub Main()
        Server.Listen(2333)
        Console.ReadKey()
    End Sub

End Module
