Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO
Imports System.ComponentModel
Imports System
Imports System.Diagnostics

Module Module1

    <DllImport("kernel32.dll", EntryPoint:="QueryDosDevice")> _
    Private Function DosDevice( _
    ByVal name As String, _
    ByVal path As StringBuilder, _
    ByVal length As UInteger) As UInteger
    End Function

    <DllImport("psapi.dll", EntryPoint:="GetProcessImageFileName")> _
    Private Function ProcessFileName( _
    ByVal handle As IntPtr, _
    ByVal name As StringBuilder, _
    ByVal size As UInteger) As UInteger
    End Function

    Private Function ProcessLocation(ByVal handle As IntPtr) As String
        Dim T As New StringBuilder(512)
        If ProcessFileName(handle, T, 512) > 0 Then
            Dim P As String = T.ToString
            For Each D As String In Environment.GetLogicalDrives
                If DosDevice(D.Substring(0, 2), T, 512) > 0 Then
                    If P.StartsWith(T.ToString) Then Return Path.GetFullPath(D & P.Remove(0, T.Length)).ToLower
                End If
            Next
        End If
        Return String.Empty
    End Function

    <DllImport("kernel32.dll", EntryPoint:="SetProcessWorkingSetSize")> _
    Private Function SetWorkingSet( _
    ByVal handle As IntPtr, _
    ByVal minimum As Integer, _
    ByVal maximum As Integer) As Boolean
    End Function

    Function Kill() As Integer
        Dim T As Integer
        Dim Exclusions As String() = {Environment.SystemDirectory.ToLower, Path.GetDirectoryName(Environment.SystemDirectory).ToLower}
        Dim Current As String = ProcessLocation(Process.GetCurrentProcess.Handle)

        For Each P As Process In Process.GetProcesses
            Try

                Dim Location As String = ProcessLocation(P.Handle)
                If Not String.IsNullOrEmpty(Location) And Not Location = Current Then

                    If Array.IndexOf(Exclusions, Path.GetDirectoryName(Location)) = -1 Then
                        P.Kill()
                        T += 1
                    End If

                End If
            Catch
                'Do nothing
            End Try
        Next

        Return T
    End Function

    Function Free() As Integer
        Dim T As Integer

        For Each P As Process In Process.GetProcesses
            Try
                If SetWorkingSet(P.Handle, -1, -1) Then T += 1
            Catch
                'Do nothing
            End Try
        Next

        Return T
    End Function


    Sub Write(ByVal data As String)
        Console.CursorLeft = 0
        Console.Write(data & New String(" "c, 10))
    End Sub

    Sub Main()
        Console.Title = Reflection.Assembly.GetExecutingAssembly.GetName.Name
        Dim Result As String = String.Empty

        Do
            Dim Maximum As ULong = My.Computer.Info.TotalPhysicalMemory + My.Computer.Info.AvailableVirtualMemory
            Dim Current As ULong = My.Computer.Info.AvailablePhysicalMemory + My.Computer.Info.AvailableVirtualMemory
            Dim Usage As Double = (Maximum - Current) / 1024 / 1024

            Console.Clear()
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("----------------------------")
            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine("Mem Usage: " & Usage.ToString("N0") & " MB")
            Console.WriteLine("Processes: " & Process.GetProcesses.Length)
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("----------------------------")
            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine("[K]    -  Ends non-vital processes")
            Console.WriteLine("[F]    -  Releases unused memory")
            Console.WriteLine("[Esc]  -  Closes the console")
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("----------------------------")
            Console.ForegroundColor = ConsoleColor.White
            Console.Write(Result)

            Select Case Console.ReadKey.Key
                Case ConsoleKey.K
                    Write("[K]: Processing..")
                    Result = "[K]: " & Kill() & " processes killed."
                Case ConsoleKey.F
                    Write("[F]: Processing..")
                    Result = "[F]: " & Free() & " processes freed."
                Case ConsoleKey.Escape
                    Exit Do
            End Select
        Loop

    End Sub

End Module
