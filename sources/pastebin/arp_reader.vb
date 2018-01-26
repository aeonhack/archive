Imports System.Net
Imports System.Runtime.InteropServices

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 4/24/2012
'Changed: 4/24/2012
'Version: 1.0.0
'------------------

Class ARP

    Private Delegate Function DnsHandler(address As IPAddress) As IPHostEntry
    Shared Function ResolveName(address As IPAddress, timeOut As Integer) As String
        Try
            Dim CB As New DnsHandler(AddressOf Dns.GetHostEntry)
            Dim R As IAsyncResult = CB.BeginInvoke(address, Nothing, Nothing)

            If R.AsyncWaitHandle.WaitOne(timeOut, False) Then
                Return CB.EndInvoke(R).HostName
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Shared Function ConnectionCache() As IPAddress()
        Dim Size As Integer
        If Not GetIpNetTable(IntPtr.Zero, Size, False) = 122 Then Return New IPAddress() {}

        Dim Items As New List(Of IPAddress)
        Dim Handle As IntPtr = Marshal.AllocCoTaskMem(Size)

        If GetIpNetTable(Handle, Size, False) = 0 Then
            Dim Pointer As IntPtr
            Dim Count As Integer = Marshal.ReadInt32(Handle)

            For I As Integer = 0 To Count - 1
                Pointer = New IntPtr((Handle.ToInt32 + 4) + (I * 24))

                '3 = Dynamic, 4 = Static
                If Marshal.ReadInt32(Pointer, -4) = 3 Then
                    Dim Address As Integer = Marshal.ReadInt32(Pointer, -8)
                    Items.Add(New IPAddress(Int32ToUInt32(Address)))
                End If
            Next
        End If

        Marshal.FreeCoTaskMem(Handle)
        Return Items.ToArray()
    End Function

    Private Shared Function Int32ToUInt32(value As Integer) As UInteger
        Return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0)
    End Function

    <DllImport("iphlpapi.dll", EntryPoint:="GetIpNetTable")> _
    Private Shared Function GetIpNetTable( _
    ByVal table As IntPtr, _
    ByRef length As Integer, _
    ByVal order As Boolean) As UInteger
    End Function

End Class