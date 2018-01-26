Imports System.Text
Imports System.Net.NetworkInformation
Imports System.Management

Class Machine
    Const PN As UShort = 4129

    Private ID(3) As String
    Private Table(255) As UShort
    Private UTF8 As New UTF8Encoding

    Private _HardwareID As String
    ReadOnly Property HardwareID() As String
        Get
            Return _HardwareID
        End Get
    End Property

    Private _Seed As UShort
    Sub New(Optional ByVal seed As UShort = 0)
        Dim R As String = String.Format(" WHERE DeviceID='{0}:'", Environment.SystemDirectory(0))
        ID(0) = Search("Win32_BaseBoard", "SerialNumber")
        ID(1) = Search("Win32_Processor", "ProcessorID")
        ID(2) = Search("Win32_LogicalDisk" & R, "VolumeSerialNumber")
        ID(3) = Search()
        _Seed = seed
        Generate()
    End Sub

    Private Function Search(ByVal element As String, ByVal item As String) As String
        element = String.Format("SELECT {0} FROM {1}", item, element)

        Dim T As New StringBuilder
        Dim U As New ManagementObjectSearcher(element)
        For Each O As ManagementObject In U.Get
            If Not O(item) Is Nothing Then T.Append(O(item))
        Next
        U.Dispose()
        Return T.ToString
    End Function
    Private Function Search() As String
        For Each O As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces
            If O.OperationalStatus = OperationalStatus.Up Then Return O.GetPhysicalAddress.ToString
        Next
        Return String.Empty
    End Function

    Private Function CRC16(ByVal data As Byte()) As UShort
        CRC16 = _Seed
        For I As Integer = 0 To data.Length - 1
            CRC16 = CRC16 << 8 Xor Table(CRC16 >> 8 Xor data(I))
        Next
        _Seed = CRC16
    End Function
    Private Sub CreateTable()
        Dim V, T As UShort
        For I As UShort = 0 To 255
            V = 0
            T = I
            For U As Byte = 0 To 7
                If ((V Xor T) And 1) <> 0 Then V = V >> 1 Xor PN Else V >>= 1
                T >>= 1
            Next
            Table(I) = V
        Next
    End Sub

    Private Sub Generate()
        CreateTable()
        Dim T As New List(Of Byte)
        For I As Byte = 0 To 3
            T.AddRange(BitConverter.GetBytes(CRC16(UTF8.GetBytes(ID(I)))))
        Next
        _HardwareID = BitConverter.ToString(T.ToArray).Replace(Convert.ToChar(45), String.Empty)
    End Sub

    Function Check(ByVal id As String) As Boolean
        If Not id.Length = 16 Then Return False
        id = id.Replace(Convert.ToChar(45), String.Empty).ToUpper

        Dim M As Integer
        For I As Byte = 0 To 15 Step 4
            If Not id.Substring(I, 4) = _HardwareID.Substring(I, 4) Then M += 1
        Next

        If M > 1 Then Return False Else Return True
    End Function
End Class