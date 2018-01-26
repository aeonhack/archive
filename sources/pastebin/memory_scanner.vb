Imports System.Runtime.InteropServices
Imports System.Reflection

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 11/9/2011
'Changed: 3/17/2012
'Version: 1.2.0
'------------------
Class Scanner

    Public ProgressChangeEvent As ProgressChangeDG
    Delegate Sub ProgressChangeDG(ByVal value As Integer)

    Sub New(progressChange As ProgressChangeDG)
        ProgressChangeEvent = progressChange
    End Sub

#Region " Properties "

    Property Handle() As IntPtr

    Private _Pages As New List(Of PAGE)
    ReadOnly Property Pages() As PAGE()
        Get
            Return _Pages.ToArray
        End Get
    End Property

    Private _Results As New List(Of Integer)
    Property Results As Integer()
        Get
            Return _Results.ToArray
        End Get
        Set(value As Integer())
            _Results = New List(Of Integer)(value)
        End Set
    End Property

#End Region

#Region " Process "

    Private PID As Integer
    Public Sub OpenProcess(ByVal processId As Integer)
        PID = processId
        _Handle = OpenProcess(1080, False, processId)
        If _Handle = IntPtr.Zero Then
            Dim Win32Error As Integer = Marshal.GetLastWin32Error

            If Win32Error = 5 Then
                'Requires elevation.
            End If

            Throw New Exception(CStr(Win32Error))
        End If
    End Sub

    Public Sub CloseProcess()
        CloseHandle(_Handle)

        _Handle = IntPtr.Zero
        _Pages.Clear()
        _Results.Clear()

        _Mask = Nothing
        _Data = Nothing
        _Search = Nothing
    End Sub

#End Region

#Region " Scanning "

    Public Sub ScanPages()
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException

        _Pages.Clear()
        Dim Current As Integer

        While True
            Dim T As New INFORMATION
            If QueryEx(_Handle, Current, T, 28) = 0 Then Exit While

            If T.State = 4096 AndAlso T.Protect = 4 AndAlso Not T.RegionSize = 0 Then
                _Pages.Add(New PAGE(T.BaseAddress, T.RegionSize))
            End If

            If (CUInt(T.BaseAddress.ToInt32) + CUInt(T.RegionSize)) > Integer.MaxValue Then Exit While
            Current = T.BaseAddress.ToInt32 + T.RegionSize
        End While
    End Sub

    Public Sub FirstScan(ByVal search As Byte(), Optional ByVal index As Integer = 0, Optional ByVal [step] As Integer = 4, Optional ByVal mask As Byte() = Nothing)
        CheckParameters(search, mask)
        If [step] = 0 Then Throw New ArgumentOutOfRangeException

        _Results.Clear()
        ProgressChangeEvent(0)

        Dim Count As Integer
        Dim Size As Integer
        Dim Base As Integer

        For I As Integer = 0 To Pages.Length - 1
            Size = Pages(I).Size
            If Size >= (search.Length + index) Then

                Base = Pages(I).Base.ToInt32
                _Data = New Byte(Size - 1) {}

                If ReadMem(_Handle, Base, _Data, _Data.Length, Count) Then
                    For O As Integer = index To Count - search.Length Step [step]
                        If ScanData(O) Then _Results.Add(Base + O)
                    Next
                End If
            End If

            ProgressChangeEvent(CInt(((I + 1) / Pages.Length) * 100))
        Next

        ProgressChangeEvent(100)
    End Sub

    Public Sub NextScan(ByVal search As Byte(), Optional ByVal mask As Byte() = Nothing)
        CheckParameters(search, mask)
        If _Results.Count = 0 Then Throw New ArgumentOutOfRangeException
        ProgressChangeEvent(0)

        Dim Clean As Boolean
        Dim Count As Integer
        Dim Index As Integer

        Dim Current As Integer
        Dim Maximum As Integer = _Results.Count

        _Data = New Byte(search.Length - 1) {}
        Do Until Clean OrElse _Results.Count = 0
            Clean = True
            For R As Integer = Index To _Results.Count - 1
                Index = R
                Current += 1

                If ReadMem(_Handle, _Results(R), _Data, _Data.Length, Count) Then
                    Clean = ScanData(0)
                Else
                    Clean = False
                End If

                If Not Clean Then
                    _Results.RemoveAt(R)
                    Exit For
                End If

                ProgressChangeEvent(CInt((Current / Maximum) * 100))
            Next
        Loop

        ProgressChangeEvent(100)
    End Sub

    Private _Mask As Byte()
    Private _Data As Byte()
    Private _Search As Byte()
    Private HandleMask As Boolean
    Private MaskIndex As Integer

    Private Function ScanData(ByVal offset As Integer) As Boolean
        If HandleMask Then
            For I As Integer = MaskIndex To _Search.Length - 1
                If _Mask(I) = 255 AndAlso Not _Data(offset + I) = _Search(I) Then Return False
            Next
        Else
            For I As Integer = 0 To _Search.Length - 1
                If Not _Data(offset + I) = _Search(I) Then Return False
            Next
        End If

        Return True
    End Function

#End Region

#Region " Validation "

    Private Sub CheckParameters(ByVal search As Byte(), ByVal mask As Byte())
        _Search = search
        _Mask = mask

        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException
        If search.Length = 0 Then Throw New ArgumentOutOfRangeException

        If mask IsNot Nothing Then
            If Not search.Length = mask.Length Then Throw New ArgumentOutOfRangeException
            If Not CheckMask(mask) Then Throw New FormatException
            HandleMask = True
        Else
            HandleMask = False
        End If
    End Sub

    Private Function CheckMask(ByVal mask As Byte()) As Boolean
        For I As Integer = 0 To mask.Length - 1
            If mask(I) = 255 Then
                MaskIndex = I
                Return True
            End If
        Next

        Return False
    End Function

#End Region

#Region " Read / Write "

    Public Function ReadMemory(ByVal address As Integer, ByVal length As Integer) As Byte()
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException
        If address < 0 Then Throw New ArgumentOutOfRangeException
        If length < 1 Then Throw New ArgumentOutOfRangeException

        Dim Count As Integer
        Dim Data(length - 1) As Byte

        If Not ReadMem(_Handle, address, Data, Data.Length, Count) Then Throw New Exception(CStr(Marshal.GetLastWin32Error))

        Return Data
    End Function

    Public Sub WriteMemory(ByVal address As Integer, ByVal data As Byte())
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException
        If address < 0 Then Throw New ArgumentOutOfRangeException
        If data.Length = 0 Then Throw New ArgumentOutOfRangeException

        Dim Count As Integer
        If Not WriteMem(_Handle, address, data, data.Length, Count) Then Throw New Exception(CStr(Marshal.GetLastWin32Error))
    End Sub

#End Region

#Region " Alloc / Free "

    Public Function Alloc(ByVal length As Integer) As Integer
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException
        If length < 1 Then Throw New ArgumentOutOfRangeException

        Dim T As Integer = AllocEx(_Handle, 0, length, 12288, 4).ToInt32
        If T = 0 Then Throw New Exception(CStr(Marshal.GetLastWin32Error))

        Return T
    End Function

    Public Sub Free(ByVal address As Integer)
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException
        If address < 0 Then Throw New ArgumentOutOfRangeException

        If Not FreeEx(_Handle, address, 0, 32768) Then Throw New Exception(CStr(Marshal.GetLastWin32Error))
    End Sub

#End Region

#Region " Suspend / Resume "

    Public Sub Suspend()
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException

        Dim Handle As IntPtr
        Dim P As Process = Process.GetProcessById(PID)

        For Each T As ProcessThread In P.Threads
            Handle = OpenThread(2, False, T.Id)

            If Not Handle = IntPtr.Zero Then
                SuspendThread(Handle)
            End If
        Next
    End Sub

    Public Sub [Resume]()
        If _Handle = IntPtr.Zero Then Throw New InvalidOperationException

        Dim Handle As IntPtr
        Dim P As Process = Process.GetProcessById(PID)

        For Each T As ProcessThread In P.Threads
            Handle = OpenThread(2, False, T.Id)

            If Not Handle = IntPtr.Zero Then
                ResumeThread(Handle)
            End If
        Next
    End Sub

#End Region

#Region " Win32 Calls "

    <DllImport("kernel32.dll", EntryPoint:="OpenProcess", SetLastError:=True)> _
    Private Shared Function OpenProcess( _
    ByVal access As UInteger, _
    ByVal inherit As Boolean, _
    ByVal process As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", EntryPoint:="OpenThread")> _
    Private Shared Function OpenThread( _
    ByVal access As UInteger, _
    ByVal inherit As Boolean, _
    ByVal thread As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", EntryPoint:="SuspendThread")> _
    Private Shared Function SuspendThread( _
    ByVal handle As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="ResumeThread")> _
    Private Shared Function ResumeThread( _
    ByVal handle As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="VirtualQueryEx")> _
    Private Shared Function QueryEx( _
    ByVal handle As IntPtr, _
    ByVal base As Integer, _
    ByRef information As INFORMATION, _
    ByVal length As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="VirtualAllocEx", SetLastError:=True)> _
    Private Shared Function AllocEx( _
    ByVal handle As IntPtr, _
    ByVal address As Integer, _
    ByVal length As Integer, _
    ByVal type As Integer, _
    ByVal protect As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", EntryPoint:="VirtualFreeEx", SetLastError:=True)> _
    Private Shared Function FreeEx( _
    ByVal handle As IntPtr, _
    ByVal address As Integer, _
    ByVal length As Integer, _
    ByVal type As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", EntryPoint:="ReadProcessMemory", SetLastError:=True)> _
    Shared Function ReadMem( _
    ByVal handle As IntPtr, _
    ByVal base As Integer, _
    ByVal data As Byte(), _
    ByVal dataLength As Integer, _
    ByRef length As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", EntryPoint:="WriteProcessMemory", SetLastError:=True)> _
    Shared Function WriteMem( _
    ByVal handle As IntPtr, _
    ByVal base As Integer, _
    ByVal data As Byte(), _
    ByVal dataLength As Integer, _
    ByRef length As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", EntryPoint:="CloseHandle")> _
    Private Shared Function CloseHandle( _
    ByVal handle As IntPtr) As Boolean
    End Function

#End Region

#Region " Structures "

    Structure PAGE
        Private _Base As IntPtr
        ReadOnly Property Base() As IntPtr
            Get
                Return _Base
            End Get
        End Property

        Private _Size As Integer
        ReadOnly Property Size() As Integer
            Get
                Return _Size
            End Get
        End Property

        Sub New(ByVal base As IntPtr, ByVal size As Integer)
            _Base = base
            _Size = size
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Private Structure INFORMATION
        Public BaseAddress As IntPtr
        Public AllocationBase As IntPtr
        Public AllocationProtect As UInteger
        Public RegionSize As Integer
        Public State As UInteger
        Public Protect As UInteger
        Public Type As UInteger
    End Structure

#End Region

End Class