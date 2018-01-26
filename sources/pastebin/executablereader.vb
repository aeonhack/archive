Imports System.IO
Imports System.ComponentModel
Imports System.Runtime.InteropServices

'------------------
'Creator: aeonhack
'Site: nimoru.com
'Created: 05/06/2014
'Changed: 05/06/2014
'Version: 1.0.0.0
'------------------

Public NotInheritable Class ExecutableReader
    Implements IDisposable

#Region " Properties "

    Private _Is32Bit As Boolean
    ReadOnly Property Is32Bit() As Boolean
        Get
            Return _Is32Bit
        End Get
    End Property

    Private _DosHeader As Win32.IMAGE_DOS_HEADER
    ReadOnly Property DosHeader As Win32.IMAGE_DOS_HEADER
        Get
            Return _DosHeader
        End Get
    End Property

    Private _Signature As UInteger
    ReadOnly Property Signature As UInteger
        Get
            Return _Signature
        End Get
    End Property

    Private _FileHeader As Win32.IMAGE_FILE_HEADER
    ReadOnly Property FileHeader As Win32.IMAGE_FILE_HEADER
        Get
            Return _FileHeader
        End Get
    End Property

    Private _OptionalHeader32 As Win32.IMAGE_OPTIONAL_HEADER32
    ReadOnly Property OptionalHeader32 As Win32.IMAGE_OPTIONAL_HEADER32
        Get
            Return _OptionalHeader32
        End Get
    End Property

    Private _OptionalHeader64 As Win32.IMAGE_OPTIONAL_HEADER64
    ReadOnly Property OptionalHeader64 As Win32.IMAGE_OPTIONAL_HEADER64
        Get
            Return _OptionalHeader64
        End Get
    End Property

    Private _DataDirectory As Win32.IMAGE_DATA_DIRECTORY()
    ReadOnly Property DataDirectory As Win32.IMAGE_DATA_DIRECTORY()
        Get
            Return _DataDirectory
        End Get
    End Property

    Private _SectionHeaders As Win32.IMAGE_SECTION_HEADER()
    ReadOnly Property SectionHeaders() As Win32.IMAGE_SECTION_HEADER()
        Get
            Return _SectionHeaders
        End Get
    End Property

    Private _IsDisposed As Boolean
    ReadOnly Property IsDisposed As Boolean
        Get
            Return _IsDisposed
        End Get
    End Property

    Private _IsLoaded As Boolean
    ReadOnly Property IsLoaded As Boolean
        Get
            Return _IsLoaded
        End Get
    End Property

#End Region


#Region " Constants "

    Private Const PROCESS_VM_OPERATION As Integer = 8
    Private Const PROCESS_VM_READ As Integer = 16

    Private Const PAGE_NOACCESS As Integer = 1
    Private Const PAGE_EXECUTE_READWRITE As Integer = 64

#End Region


#Region " Members "

    Private _IsRuntime As Boolean

    Private _ProcessHandle As IntPtr
    Private _FileStream As FileStream

    Private _Address As IntPtr
    Private _BaseAddress As IntPtr

#End Region


#Region " Initialization "

    Public Sub LoadFromProcess(process As Process)
        ThrowOnDisposed()
        ThrowOnLoaded()

        _IsLoaded = True
        _IsRuntime = True

        _BaseAddress = process.MainModule.BaseAddress
        _Address = _BaseAddress

        _ProcessHandle = Win32.OpenProcess(PROCESS_VM_READ Or PROCESS_VM_OPERATION, False, process.Id)

        If _ProcessHandle = IntPtr.Zero Then
            Throw New Win32Exception(Marshal.GetLastWin32Error())
        End If

        ProcessExecutable()
    End Sub

    Public Sub LoadFromFile(fileName As String)
        ThrowOnDisposed()
        ThrowOnLoaded()

        _IsLoaded = True
        _FileStream = File.OpenRead(fileName)

        ProcessExecutable()
    End Sub

#End Region


#Region " Processing "

    Private Sub ProcessExecutable()
        _DosHeader = Read(Of Win32.IMAGE_DOS_HEADER)()
        Seek(_DosHeader.e_lfanew)

        _Signature = Read(Of UInteger)()
        _FileHeader = Read(Of Win32.IMAGE_FILE_HEADER)()
        _Is32Bit = (_FileHeader.Machine = 332)

        If _Is32Bit Then
            _OptionalHeader32 = Read(Of Win32.IMAGE_OPTIONAL_HEADER32)()
        Else
            _OptionalHeader64 = Read(Of Win32.IMAGE_OPTIONAL_HEADER64)()
        End If

        _DataDirectory = New Win32.IMAGE_DATA_DIRECTORY(16 - 1) {}
        For I As Integer = 0 To _DataDirectory.Length - 1
            _DataDirectory(I) = Read(Of Win32.IMAGE_DATA_DIRECTORY)()
        Next

        SeekToSectionHeaders()

        _SectionHeaders = New Win32.IMAGE_SECTION_HEADER(_FileHeader.NumberOfSections - 1) {}
        For I As Integer = 0 To _FileHeader.NumberOfSections - 1
            _SectionHeaders(I) = Read(Of Win32.IMAGE_SECTION_HEADER)()
        Next
    End Sub

    Private Function Read(Of T As Structure)() As T
        Dim Data As Byte() = Read(Marshal.SizeOf(GetType(T)))

        Dim Item As New T()
        Dim Handle As IntPtr = Marshal.AllocCoTaskMem(Data.Length)

        Marshal.Copy(Data, 0, Handle, Data.Length)
        Item = DirectCast(Marshal.PtrToStructure(Handle, GetType(T)), T)
        Marshal.FreeCoTaskMem(Handle)

        Return Item
    End Function

    Private Function Read(length As Integer) As Byte()
        Dim Data(length - 1) As Byte

        If _IsRuntime Then
            Dim BytesRead As Integer

            If Not Win32.ReadProcessMemory(_ProcessHandle, _Address, Data, Data.Length, BytesRead) Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If

            _Address = New IntPtr(_Address.ToInt64() + Data.Length)
        Else
            _FileStream.Read(Data, 0, Data.Length)
        End If

        Return Data
    End Function

    Private Sub Seek(offset As Integer)
        If _IsRuntime Then
            _Address = New IntPtr(_BaseAddress.ToInt64() + offset)
        Else
            _FileStream.Seek(offset, SeekOrigin.Begin)
        End If
    End Sub

    Private Sub SeekToSectionHeaders()
        Dim Offset As Integer = _DosHeader.e_lfanew

        Offset += Marshal.SizeOf(_Signature)
        Offset += Marshal.SizeOf(_FileHeader)
        Offset += _FileHeader.SizeOfOptionalHeader

        Seek(Offset)
    End Sub

#End Region


#Region " Extraction "

    Public Function DumpSection(sectionHeader As Win32.IMAGE_SECTION_HEADER) As Byte()
        ThrowOnDisposed()
        ThrowOnNotLoaded()

        If _IsRuntime Then
            Seek(sectionHeader.VirtualAddress)
        Else
            Seek(sectionHeader.PointerToRawData)
        End If

        Return GetDump(sectionHeader.VirtualSize)
    End Function

    Public Function DumpDataDirectory(dataDirectory As Win32.IMAGE_DATA_DIRECTORY) As Byte()
        ThrowOnDisposed()
        ThrowOnNotLoaded()

        If dataDirectory.VirtualAddress = 0 Then
            Return New Byte() {}
        End If

        If _IsRuntime Then
            Seek(dataDirectory.VirtualAddress)
        Else
            Seek(GetPointerToRawData(dataDirectory.VirtualAddress))
        End If

        Return GetDump(dataDirectory.Size)
    End Function

    Public Function DumpUnmappedData() As Byte()
        ThrowOnDisposed()
        ThrowOnNotLoaded()

        If _IsRuntime Then
            Return New Byte() {}
        Else
            Dim HighestOffset As Integer

            For Each Section As Win32.IMAGE_SECTION_HEADER In _SectionHeaders
                If Section.PointerToRawData + Section.SizeOfRawData > HighestOffset Then
                    HighestOffset = Section.PointerToRawData + Section.SizeOfRawData
                End If
            Next

            For Each Directory As Win32.IMAGE_DATA_DIRECTORY In _DataDirectory
                Dim PointerToRawData As Integer = GetPointerToRawData(Directory.VirtualAddress)

                If PointerToRawData + Directory.Size > HighestOffset Then
                    HighestOffset = PointerToRawData + Directory.Size
                End If
            Next

            Seek(HighestOffset)

            Return Read(CInt(_FileStream.Length) - HighestOffset)
        End If
    End Function

    Private Function GetPointerToRawData(virtualAddress As Integer) As Integer
        For Each Section As Win32.IMAGE_SECTION_HEADER In _SectionHeaders
            If (virtualAddress < Section.VirtualAddress) OrElse (virtualAddress > (Section.VirtualAddress + Section.SizeOfRawData)) Then
                Continue For
            End If

            Return Section.PointerToRawData + (virtualAddress - Section.VirtualAddress)
        Next

        Return -1
    End Function

    Private Function GetDump(size As Integer) As Byte()
        If size = 0 Then
            Return New Byte() {}
        End If

        Dim Address As IntPtr = _Address
        Dim Protection As Integer = SetMemProtection(Address, size, PAGE_EXECUTE_READWRITE)

        If Protection = -1 Then
            Return New Byte() {}
        End If

        Dim Data As Byte() = Read(size)
        SetMemProtection(Address, size, Protection)

        Return Data
    End Function

    Private Function SetMemProtection(address As IntPtr, size As Integer, protection As Integer) As Integer
        If Not _IsRuntime Then Return 0

        Dim OldProtection As Integer

        If Win32.VirtualProtectEx(_ProcessHandle, address, size, protection, OldProtection) = 0 Then
            If OldProtection = PAGE_NOACCESS Then
                Return -1
            Else
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If
        End If

        Return OldProtection
    End Function

#End Region


#Region " State Handling "

    Private Sub ThrowOnDisposed()
        If _IsDisposed Then
            Throw New ObjectDisposedException([GetType]().FullName)
        End If
    End Sub

    Private Sub ThrowOnLoaded()
        If _IsLoaded Then
            Throw New Exception("An executable has already been loaded.")
        End If
    End Sub

    Private Sub ThrowOnNotLoaded()
        If Not _IsLoaded Then
            Throw New Exception("No executable has been loaded.")
        End If
    End Sub

#End Region


#Region " IDisposable "

    Protected Sub Dispose(disposing As Boolean)
        If Not _IsDisposed Then
            If disposing Then

                If _FileStream IsNot Nothing Then
                    _FileStream.Close()
                End If

            End If

            Win32.CloseHandle(_ProcessHandle)
        End If

        _IsDisposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Sub Close()
        Dispose()
    End Sub

#End Region

End Class

Public NotInheritable Class Win32

#Region " Methods "

    <DllImport("kernel32.dll", EntryPoint:="OpenProcess", SetLastError:=True)> _
    Public Shared Function OpenProcess( _
    ByVal access As Integer, _
    ByVal inherit As Boolean, _
    ByVal processId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", EntryPoint:="VirtualProtectEx", SetLastError:=True)> _
    Public Shared Function VirtualProtectEx( _
    ByVal handle As IntPtr, _
    ByVal address As IntPtr, _
    ByVal size As Integer, _
    ByVal newProtection As Integer, _
    ByRef oldProtection As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="ReadProcessMemory", SetLastError:=True)> _
    Public Shared Function ReadProcessMemory( _
    ByVal handle As IntPtr, _
    ByVal address As IntPtr, _
    ByVal data As Byte(), _
    ByVal dataLength As Integer, _
    ByRef length As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", EntryPoint:="CloseHandle")> _
    Public Shared Function CloseHandle( _
    ByVal handle As IntPtr) As Boolean
    End Function

#End Region

#Region " Structures "

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure IMAGE_DOS_HEADER
        Public e_magic As UShort
        Public e_cblp As UShort
        Public e_cp As UShort
        Public e_crlc As UShort
        Public e_cparhdr As UShort
        Public e_minalloc As UShort
        Public e_maxalloc As UShort
        Public e_ss As UShort
        Public e_sp As UShort
        Public e_csum As UShort
        Public e_ip As UShort
        Public e_cs As UShort
        Public e_lfarlc As UShort
        Public e_ovno As UShort
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public e_res As UShort()
        Public e_oemid As UShort
        Public e_oeminfo As UShort
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public e_res2 As UShort()
        Public e_lfanew As Integer 'NOTE: Should be unsigned
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure IMAGE_FILE_HEADER
        Public Machine As UShort
        Public NumberOfSections As UShort
        Public TimeDateStamp As Integer
        Public PointerToSymbolTable As Integer
        Public NumberOfSymbols As Integer
        Public SizeOfOptionalHeader As UShort
        Public Characteristics As UShort
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure IMAGE_OPTIONAL_HEADER32
        Public Magic As UShort
        Public MajorLinkerVersion As Byte
        Public MinorLinkerVersion As Byte
        Public SizeOfCode As Integer
        Public SizeOfInitializedData As Integer
        Public SizeOfUninitializedData As Integer
        Public AddressOfEntryPoint As Integer
        Public BaseOfCode As Integer
        Public BaseOfData As Integer
        Public ImageBase As Integer
        Public SectionAlignment As Integer
        Public FileAlignment As Integer
        Public MajorOperatingSystemVersion As UShort
        Public MinorOperatingSystemVersion As UShort
        Public MajorImageVersion As UShort
        Public MinorImageVersion As UShort
        Public MajorSubsystemVersion As UShort
        Public MinorSubsystemVersion As UShort
        Public Win32VersionValue As Integer
        Public SizeOfImage As Integer
        Public SizeOfHeaders As Integer
        Public CheckSum As Integer
        Public Subsystem As UShort
        Public DllCharacteristics As UShort
        Public SizeOfStackReserve As Integer
        Public SizeOfStackCommit As Integer
        Public SizeOfHeapReserve As Integer
        Public SizeOfHeapCommit As Integer
        Public LoaderFlags As Integer
        Public NumberOfRvaAndSizes As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure IMAGE_OPTIONAL_HEADER64
        Public Magic As UShort
        Public MajorLinkerVersion As Byte
        Public MinorLinkerVersion As Byte
        Public SizeOfCode As Integer
        Public SizeOfInitializedData As Integer
        Public SizeOfUninitializedData As Integer
        Public AddressOfEntryPoint As Integer
        Public BaseOfCode As Integer
        Public ImageBase As Long
        Public SectionAlignment As Integer
        Public FileAlignment As Integer
        Public MajorOperatingSystemVersion As UShort
        Public MinorOperatingSystemVersion As UShort
        Public MajorImageVersion As UShort
        Public MinorImageVersion As UShort
        Public MajorSubsystemVersion As UShort
        Public MinorSubsystemVersion As UShort
        Public Win32VersionValue As Integer
        Public SizeOfImage As Integer
        Public SizeOfHeaders As Integer
        Public CheckSum As Integer
        Public Subsystem As UShort
        Public DllCharacteristics As UShort
        Public SizeOfStackReserve As Long
        Public SizeOfStackCommit As Long
        Public SizeOfHeapReserve As Long
        Public SizeOfHeapCommit As Long
        Public LoaderFlags As Integer
        Public NumberOfRvaAndSizes As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure IMAGE_DATA_DIRECTORY
        Public VirtualAddress As Integer
        Public Size As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure IMAGE_SECTION_HEADER
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)> _
        Public Name As String
        Public VirtualSize As Integer 'NOTE: Alias of Misc
        Public VirtualAddress As Integer
        Public SizeOfRawData As Integer
        Public PointerToRawData As Integer
        Public PointerToRelocations As Integer
        Public PointerToLineNumbers As Integer
        Public NumberOfRelocations As UShort
        Public NumberOfLineNumbers As UShort
        Public Characteristics As Integer
    End Structure

#End Region


End Class