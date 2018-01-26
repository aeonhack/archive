'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 4/10/2011
'Changed: 11/9/2011
'Version: 1.0.1
'------------------
<Obfuscation(ApplyToMembers:=True, Exclude:=True)> _
Class PE

#Region " Properties "

    Private _MACHINE_I386 As Boolean
    ReadOnly Property MACHINE_I386() As Boolean
        Get
            Return _MACHINE_I386
        End Get
    End Property

    Private _DOS_HEADER As IMAGE_DOS_HEADER
    ReadOnly Property DOS_HEADER As IMAGE_DOS_HEADER
        Get
            Return _DOS_HEADER
        End Get
    End Property

    Private _NT_SIGNATURE As UInteger
    ReadOnly Property NT_SIGNATURE As UInteger
        Get
            Return _NT_SIGNATURE
        End Get
    End Property

    Private _FILE_HEADER As IMAGE_FILE_HEADER
    ReadOnly Property FILE_HEADER As IMAGE_FILE_HEADER
        Get
            Return _FILE_HEADER
        End Get
    End Property

    Private _OPTIONAL_HEADER32 As IMAGE_OPTIONAL_HEADER32
    ReadOnly Property OPTIONAL_HEADER32 As IMAGE_OPTIONAL_HEADER32
        Get
            Return _OPTIONAL_HEADER32
        End Get
    End Property
    Private _OPTIONAL_HEADER64 As IMAGE_OPTIONAL_HEADER64
    ReadOnly Property OPTIONAL_HEADER64 As IMAGE_OPTIONAL_HEADER64
        Get
            Return _OPTIONAL_HEADER64
        End Get
    End Property

    Private _DATA_DIRECTORY As IMAGE_DATA_DIRECTORY()
    ReadOnly Property DATA_DIRECTORY As IMAGE_DATA_DIRECTORY()
        Get
            Return _DATA_DIRECTORY
        End Get
    End Property

    Private _SECTION_HEADERS As IMAGE_SECTION_HEADER()
    ReadOnly Property SECTION_HEADERS() As IMAGE_SECTION_HEADER()
        Get
            Return _SECTION_HEADERS
        End Get
    End Property

#End Region

#Region " Offsets "

    Function OS(Of T)() As Integer
        Dim Base As Integer = CInt(_DOS_HEADER.e_lfanew)

        Select Case GetType(T).GUID
            Case GetType(IMAGE_DOS_HEADER).GUID
                Base = 0
            Case GetType(IMAGE_FILE_HEADER).GUID
                Base += 4
            Case GetType(IMAGE_OPTIONAL_HEADER32).GUID
                Base += 24
            Case GetType(IMAGE_OPTIONAL_HEADER64).GUID
                Base += 24
            Case GetType(IMAGE_DATA_DIRECTORY).GUID
                Base += 24 + _FILE_HEADER.SizeOfOptionalHeader - Length
            Case GetType(IMAGE_SECTION_HEADER).GUID
                Base += 24 + _FILE_HEADER.SizeOfOptionalHeader - Length + (_DATA_DIRECTORY.Length * 8)
        End Select

        Return Base
    End Function

    Function OS(Of T)(ByVal name As String) As Integer
        If name = "Signature" Then Return CInt(_DOS_HEADER.e_lfanew)
        Return OS(Of T)() + Marshal.OffsetOf(GetType(T), name).ToInt32()
    End Function

    Function OS(Of T)(ByVal index As Integer, ByVal name As String) As Integer
        Return OS(Of T)() + (index * (Marshal.SizeOf(GetType(T))) + Marshal.OffsetOf(GetType(T), name).ToInt32())
    End Function

#End Region

#Region " Structures "

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Structure IMAGE_DOS_HEADER
        ReadOnly e_magic As UInt16
        ReadOnly e_cblp As UInt16
        ReadOnly e_cp As UInt16
        ReadOnly e_crlc As UInt16
        ReadOnly e_cparhdr As UInt16
        ReadOnly e_minalloc As UInt16
        ReadOnly e_maxalloc As UInt16
        ReadOnly e_ss As UInt16
        ReadOnly e_sp As UInt16
        ReadOnly e_csum As UInt16
        ReadOnly e_ip As UInt16
        ReadOnly e_cs As UInt16
        ReadOnly e_lfarlc As UInt16
        ReadOnly e_ovno As UInt16
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        ReadOnly e_res As UInt16()
        ReadOnly e_oemid As UInt16
        ReadOnly e_oeminfo As UInt16
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        ReadOnly e_res2 As UInt16()
        ReadOnly e_lfanew As UInt32
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Structure IMAGE_FILE_HEADER
        ReadOnly Machine As UInt16
        ReadOnly NumberOfSections As UInt16
        ReadOnly TimeDateStamp As UInt32
        ReadOnly PointerToSymbolTable As UInt32
        ReadOnly NumberOfSymbols As UInt32
        ReadOnly SizeOfOptionalHeader As UInt16
        ReadOnly Characteristics As UInt16
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Structure IMAGE_OPTIONAL_HEADER32
        ReadOnly Magic As UInt16
        ReadOnly MajorLinkerVersion As Byte
        ReadOnly MinorLinkerVersion As Byte
        ReadOnly SizeOfCode As UInt32
        ReadOnly SizeOfInitializedData As UInt32
        ReadOnly SizeOfUninitializedData As UInt32
        ReadOnly AddressOfEntryPoint As UInt32
        ReadOnly BaseOfCode As UInt32
        ReadOnly BaseOfData As UInt32
        ReadOnly ImageBase As UInt32
        ReadOnly SectionAlignment As UInt32
        ReadOnly FileAlignment As UInt32
        ReadOnly MajorOperatingSystemVersion As UInt16
        ReadOnly MinorOperatingSystemVersion As UInt16
        ReadOnly MajorImageVersion As UInt16
        ReadOnly MinorImageVersion As UInt16
        ReadOnly MajorSubsystemVersion As UInt16
        ReadOnly MinorSubsystemVersion As UInt16
        ReadOnly Win32VersionValue As UInt32
        ReadOnly SizeOfImage As UInt32
        ReadOnly SizeOfHeaders As UInt32
        ReadOnly CheckSum As UInt32
        ReadOnly Subsystem As UInt16
        ReadOnly DllCharacteristics As UInt16
        ReadOnly SizeOfStackReserve As UInt32
        ReadOnly SizeOfStackCommit As UInt32
        ReadOnly SizeOfHeapReserve As UInt32
        ReadOnly SizeOfHeapCommit As UInt32
        ReadOnly LoaderFlags As UInt32
        ReadOnly NumberOfRvaAndSizes As UInt32
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Structure IMAGE_OPTIONAL_HEADER64
        ReadOnly Magic As UInt16
        ReadOnly MajorLinkerVersion As Byte
        ReadOnly MinorLinkerVersion As Byte
        ReadOnly SizeOfCode As UInt32
        ReadOnly SizeOfInitializedData As UInt32
        ReadOnly SizeOfUninitializedData As UInt32
        ReadOnly AddressOfEntryPoint As UInt32
        ReadOnly BaseOfCode As UInt32
        ReadOnly ImageBase As UInt64
        ReadOnly SectionAlignment As UInt32
        ReadOnly FileAlignment As UInt32
        ReadOnly MajorOperatingSystemVersion As UInt16
        ReadOnly MinorOperatingSystemVersion As UInt16
        ReadOnly MajorImageVersion As UInt16
        ReadOnly MinorImageVersion As UInt16
        ReadOnly MajorSubsystemVersion As UInt16
        ReadOnly MinorSubsystemVersion As UInt16
        ReadOnly Win32VersionValue As UInt32
        ReadOnly SizeOfImage As UInt32
        ReadOnly SizeOfHeaders As UInt32
        ReadOnly CheckSum As UInt32
        ReadOnly Subsystem As UInt16
        ReadOnly DllCharacteristics As UInt16
        ReadOnly SizeOfStackReserve As UInt64
        ReadOnly SizeOfStackCommit As UInt64
        ReadOnly SizeOfHeapReserve As UInt64
        ReadOnly SizeOfHeapCommit As UInt64
        ReadOnly LoaderFlags As UInt32
        ReadOnly NumberOfRvaAndSizes As UInt32
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Structure IMAGE_DATA_DIRECTORY
        ReadOnly VirtualAddress As UInt32
        ReadOnly Size As UInt32
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Structure IMAGE_SECTION_HEADER
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)> _
        ReadOnly Name As String
        ReadOnly Misc As UInt32
        ReadOnly VirtualAddress As UInt32
        ReadOnly SizeOfRawData As UInt32
        ReadOnly PointerToRawData As UInt32
        ReadOnly PointerToRelocations As UInt32
        ReadOnly PointerToLinenumbers As UInt32
        ReadOnly NumberOfRelocations As UInt16
        ReadOnly NumberOfLinenumbers As UInt16
        ReadOnly Characteristics As UInt32
    End Structure

#End Region

    Private Stream As FileStream, Length As Integer
    Sub Process(ByVal path As String)
        Stream = New FileStream(path, FileMode.Open, FileAccess.Read)

        Try
            _DOS_HEADER = Scan(Of IMAGE_DOS_HEADER)()
            Stream.Seek(_DOS_HEADER.e_lfanew, SeekOrigin.Begin)

            _NT_SIGNATURE = Scan(Of UInt32)()
            _FILE_HEADER = Scan(Of IMAGE_FILE_HEADER)()
            _MACHINE_I386 = _FILE_HEADER.Machine = 332

            Length = _FILE_HEADER.SizeOfOptionalHeader

            If _MACHINE_I386 Then
                _OPTIONAL_HEADER32 = Scan(Of IMAGE_OPTIONAL_HEADER32)()
                Length -= 96
            Else
                _OPTIONAL_HEADER64 = Scan(Of IMAGE_OPTIONAL_HEADER64)()
                Length -= 112
            End If

            Dim U1 As New List(Of IMAGE_DATA_DIRECTORY)
            For I As Integer = 1 To Length \ 8
                U1.Add(Scan(Of IMAGE_DATA_DIRECTORY))
            Next
            _DATA_DIRECTORY = U1.ToArray


            Stream.Seek(OS(Of IMAGE_SECTION_HEADER), SeekOrigin.Begin)

            Dim U2 As New List(Of IMAGE_SECTION_HEADER)
            For I As UShort = 1 To _FILE_HEADER.NumberOfSections
                U2.Add(Scan(Of IMAGE_SECTION_HEADER))
            Next
            _SECTION_HEADERS = U2.ToArray

        Finally
            Stream.Close()
        End Try
    End Sub

    Private Function Scan(Of T As Structure)() As T
        Dim Data(Marshal.SizeOf(GetType(T)) - 1) As Byte
        Stream.Read(Data, 0, Data.Length)
        Return Push(Of T)(Data)
    End Function
    Private Function Push(Of T As Structure)(ByVal data As Byte()) As T
        Dim Item As New T
        Dim U As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(GetType(T)))

        If Not U = IntPtr.Zero Then
            Marshal.Copy(data, 0, U, data.Length)
            Item = CType(Marshal.PtrToStructure(U, GetType(T)), T)
            Marshal.FreeCoTaskMem(U)
        End If

        Return Item
    End Function

End Class
