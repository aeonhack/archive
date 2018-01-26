Imports System.Reflection

Module ServerMain

    Private Function ResolveAssembly(sender As Object, args As ResolveEventArgs) As Assembly
        Return Assembly.Load(My.Resources.Interop_NATUPNPLib)
    End Function

    Sub Main()
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssembly

        InitializeServer()

        If EnableUPnP Then HandleUPnP()

        Server.Listen(ListeningPort)

        Console.WriteLine()
        Console.WriteLine("Press any key to shutdown the server.")
        Console.ReadKey()

        If EnableUPnP Then CleanUPnP()
    End Sub

    Private Sub HandleUPnP()
        If UPnP.UPnPEnabled Then
            CleanUPnP()

            If UPnP.MappingExists(ListeningPort) Then
                Console.WriteLine("ERROR: Port {0} is in use by {1}.", ListeningPort, UPnP.IPv4Address)
            Else
                Console.WriteLine("Mapping port {0} to {1}..", ListeningPort, UPnP.IPv4Address)
                UPnP.AddMapping(ListeningPort, UPNP_DESCRIPTION)
            End If
        Else
            Console.WriteLine("ERROR: Failed to map port {0} to {1}, is UPnP enabled?", ListeningPort, UPnP.IPv4Address)
        End If
    End Sub

    Private Sub CleanUPnP()
        UPnP.RemoveMapping(ListeningPort, UPNP_DESCRIPTION)
    End Sub

End Module
