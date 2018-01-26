Imports System.Collections.Generic
Imports System.Net.Sockets
Imports System.Net
Imports System.ComponentModel
Imports System.IO

'------------------
'Creator: aeonhack
'Site: nimoru.com
'Created: 9/12/2012
'Changed: 6/13/2013
'Version: 1.2.0.3
'------------------

NotInheritable Class UserClient
    Implements IDisposable

    'TODO: Lock objects where needed.
    'TODO: Create and handle ReadQueue?
    'TODO: Provide option to disable buffering.

#Region " Events "

    Public Event ExceptionThrown As ExceptionThrownEventHandler
    Public Delegate Sub ExceptionThrownEventHandler(sender As UserClient, ex As Exception)

    Private Sub OnExceptionThrown(ex As Exception)
        RaiseEvent ExceptionThrown(Me, ex)
    End Sub

    Public Event StateChanged As StateChangedEventHandler
    Public Delegate Sub StateChangedEventHandler(sender As UserClient, connected As Boolean)

    Private Sub OnStateChanged(connected As Boolean)
        RaiseEvent StateChanged(Me, connected)
    End Sub

    Public Event ReadPacket As ReadPacketEventHandler
    Public Delegate Sub ReadPacketEventHandler(sender As UserClient, data As Byte())

    Private Sub OnReadPacket(data As Byte())
        RaiseEvent ReadPacket(Me, data)
    End Sub

    Public Event ReadProgressChanged As ReadProgressChangedEventHandler
    Public Delegate Sub ReadProgressChangedEventHandler(sender As UserClient, progress As Double, bytesRead As Integer, bytesToRead As Integer)

    Private Sub OnReadProgressChanged(progress As Double, bytesRead As Integer, bytesToRead As Integer)
        RaiseEvent ReadProgressChanged(Me, progress, bytesRead, bytesToRead)
    End Sub

    Public Event WritePacket As WritePacketEventHandler
    Public Delegate Sub WritePacketEventHandler(sender As UserClient, size As Integer)

    Private Sub OnWritePacket(size As Integer)
        RaiseEvent WritePacket(Me, size)
    End Sub

    Public Event WriteProgressChanged As WriteProgressChangedEventHandler
    Public Delegate Sub WriteProgressChangedEventHandler(sender As UserClient, progress As Double, bytesWritten As Integer, bytesToWrite As Integer)

    Private Sub OnWriteProgressChanged(progress As Double, bytesWritten As Integer, bytesToWrite As Integer)
        RaiseEvent WriteProgressChanged(Me, progress, bytesWritten, bytesToWrite)
    End Sub

#End Region

#Region " Properties "

    Private _BufferSize As UShort = 8192
    Public Property BufferSize() As UShort
        Get
            Return _BufferSize
        End Get
        Set(value As UShort)
            If value < 1 Then
                Throw New Exception("Value must be greater than 0.")
            Else
                _BufferSize = value
            End If
        End Set
    End Property

    Private _MaxPacketSize As Integer = 10485760
    Public Property MaxPacketSize() As Integer
        Get
            Return _MaxPacketSize
        End Get
        Set(value As Integer)
            If value < 1 Then
                Throw New Exception("Value must be greater than 0.")
            Else
                _MaxPacketSize = value
            End If
        End Set
    End Property

    Private _KeepAlive As Boolean = True
    Public Property KeepAlive() As Boolean
        Get
            Return _KeepAlive
        End Get
        Set(value As Boolean)
            If _Connected Then
                Throw New Exception("Unable to change this option while connected.")
            Else
                _KeepAlive = value
            End If
        End Set
    End Property

    Private _UserState As Object
    Public Property UserState() As Object
        Get
            Return _UserState
        End Get
        Set(value As Object)
            _UserState = value
        End Set
    End Property

    Private _EndPoint As IPEndPoint
    Public ReadOnly Property EndPoint() As IPEndPoint
        Get
            If _EndPoint IsNot Nothing Then
                Return _EndPoint
            Else
                Return New IPEndPoint(IPAddress.None, 0)
            End If
        End Get
    End Property

    Private _Connected As Boolean
    Public ReadOnly Property Connected() As Boolean
        Get
            Return _Connected
        End Get
    End Property

#End Region

    Private O As AsyncOperation
    Private Handle As Socket

    Private SendIndex As Integer
    Private SendBuffer As Byte()

    Private ReadIndex As Integer
    Private ReadBuffer As Byte()

    Private SendQueue As Queue(Of Byte())

    Private Items As SocketAsyncEventArgs()
    Private Processing As Boolean() = New Boolean(1) {}

    Public Sub New()
        O = AsyncOperationManager.CreateOperation(Nothing)
    End Sub

    Public Sub Connect(host As String, port As UShort)
        Try
            Disconnect()
            Initialize()

            Dim IP As IPAddress = IPAddress.None
            If IPAddress.TryParse(host, IP) Then
                DoConnect(IP, port)
            Else
                Dns.BeginGetHostEntry(host, AddressOf EndGetHostEntry, port)
            End If
        Catch ex As Exception
            O.Post(Sub(x) OnExceptionThrown(DirectCast(x, Exception)), ex)
            Disconnect()
        End Try
    End Sub

    Private Sub EndGetHostEntry(r As IAsyncResult)
        Try
            DoConnect(Dns.EndGetHostEntry(r).AddressList(0), DirectCast(r.AsyncState, UShort))
        Catch ex As Exception
            O.Post(Sub(x) OnExceptionThrown(DirectCast(x, Exception)), ex)
            Disconnect()
        End Try
    End Sub

    Private Sub DoConnect(ip As IPAddress, port As UShort)
        Try
            Handle = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            Handle.NoDelay = True

            If _KeepAlive Then
                Handle.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 20000)
            End If

            Items(0).RemoteEndPoint = New IPEndPoint(ip, port)
            If Not Handle.ConnectAsync(Items(0)) Then
                Process(Nothing, Items(0))
            End If
        Catch ex As Exception
            O.Post(Sub(x) OnExceptionThrown(DirectCast(x, Exception)), ex)
            Disconnect()
        End Try
    End Sub

    Private Sub Initialize()
        Processing = New Boolean(1) {}

        SendIndex = 0
        ReadIndex = 0

        SendBuffer = New Byte(-1) {}
        ReadBuffer = New Byte(-1) {}

        SendQueue = New Queue(Of Byte())()

        Items = New SocketAsyncEventArgs(1) {}

        Items(0) = New SocketAsyncEventArgs()
        Items(1) = New SocketAsyncEventArgs()
        AddHandler Items(0).Completed, AddressOf Process
        AddHandler Items(1).Completed, AddressOf Process
    End Sub

    Private Sub Process(s As Object, e As SocketAsyncEventArgs)
        Try
            If e.SocketError = SocketError.Success Then
                Select Case e.LastOperation
                    Case SocketAsyncOperation.Connect
                        _EndPoint = DirectCast(Handle.RemoteEndPoint, IPEndPoint)
                        _Connected = True
                        Items(0).SetBuffer(New Byte(_BufferSize - 1) {}, 0, _BufferSize)

                        O.Post(Sub() OnStateChanged(True), Nothing)
                        If Not Handle.ReceiveAsync(e) Then
                            Process(Nothing, e)
                        End If
                    Case SocketAsyncOperation.Receive
                        If Not _Connected Then Return

                        If Not e.BytesTransferred = 0 Then
                            HandleRead(e.Buffer, 0, e.BytesTransferred)

                            If Not Handle.ReceiveAsync(e) Then
                                Process(Nothing, e)
                            End If
                        Else
                            Disconnect()
                        End If
                    Case SocketAsyncOperation.Send
                        If Not _Connected Then Return

                        Dim EOS As Boolean
                        SendIndex += e.BytesTransferred

                        O.Post(AddressOf WriteProgressChangedCallback, New Object() {(SendIndex / SendBuffer.Length) * 100, SendIndex, SendBuffer.Length})

                        If (SendIndex >= SendBuffer.Length) Then
                            EOS = True
                            O.Post(Sub(x) OnWritePacket(DirectCast(x, Integer)), SendBuffer.Length - 4)
                        End If

                        If SendQueue.Count = 0 AndAlso EOS Then
                            Processing(1) = False
                        Else
                            HandleSendQueue()
                        End If
                End Select
            Else
                O.Post(Sub(x) OnExceptionThrown(DirectCast(x, SocketException)), New SocketException(e.SocketError))
                Disconnect()
            End If
        Catch ex As Exception
            O.Post(Sub(x) OnExceptionThrown(DirectCast(x, Exception)), ex)
            Disconnect()
        End Try
    End Sub

    Public Sub Disconnect()
        If Processing(0) Then
            Return
        Else
            Processing(0) = True
        End If

        Dim Raise As Boolean = _Connected
        _Connected = False

        If Handle IsNot Nothing Then
            Handle.Close()
        End If

        If SendQueue IsNot Nothing Then
            SendQueue.Clear()
        End If

        SendBuffer = New Byte(-1) {}
        ReadBuffer = New Byte(-1) {}

        If Raise Then
            O.Post(Sub() OnStateChanged(False), Nothing)
        End If

        If Items IsNot Nothing Then
            Items(0).Dispose()
            Items(1).Dispose()
        End If

        _EndPoint = Nothing
    End Sub

    Public Sub Send(data As Byte())
        If Not _Connected Then Return

        SendQueue.Enqueue(data)

        If Not Processing(1) Then
            Processing(1) = True
            HandleSendQueue()
        End If
    End Sub

    Private Sub HandleSendQueue()
        Try
            If SendIndex >= SendBuffer.Length Then
                SendIndex = 0
                SendBuffer = Header(SendQueue.Dequeue())
            End If

            Dim Write As Integer = Math.Min(SendBuffer.Length - SendIndex, _BufferSize)
            Items(1).SetBuffer(SendBuffer, SendIndex, Write)

            If Not Handle.SendAsync(Items(1)) Then
                Process(Nothing, Items(1))
            End If
        Catch ex As Exception
            O.Post(Sub(x) OnExceptionThrown(DirectCast(x, Exception)), ex)
            Disconnect()
        End Try
    End Sub

    Private Shared Function Header(data As Byte()) As Byte()
        Dim T As Byte() = New Byte(data.Length + 3) {}
        Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, T, 0, 4)
        Buffer.BlockCopy(data, 0, T, 4, data.Length)
        Return T
    End Function

    Private Sub HandleRead(data As Byte(), index As Integer, length As Integer)
        If ReadIndex >= ReadBuffer.Length Then
            ReadIndex = 0
            If data.Length < 4 Then
                O.Post(Sub() OnExceptionThrown(New Exception("Missing or corrupt packet header.")), Nothing)
                Disconnect()
                Return
            End If

            Dim PacketSize As Integer = BitConverter.ToInt32(data, index)
            If PacketSize > _MaxPacketSize Then
                O.Post(Sub() OnExceptionThrown(New Exception("Packet size exceeds MaxPacketSize.")), Nothing)
                Disconnect()
                Return
            End If

            Array.Resize(ReadBuffer, PacketSize)
            index += 4
        End If

        Dim Read As Integer = Math.Min(ReadBuffer.Length - ReadIndex, length - index)
        Buffer.BlockCopy(data, index, ReadBuffer, ReadIndex, Read)
        ReadIndex += Read

        O.Post(AddressOf ReadProgressChangedCallback, New Object() {(ReadIndex / ReadBuffer.Length) * 100, ReadIndex, ReadBuffer.Length})

        If ReadIndex >= ReadBuffer.Length Then
            Dim BufferClone(ReadBuffer.Length - 1) As Byte 'Race condition fail-safe.
            Buffer.BlockCopy(ReadBuffer, 0, BufferClone, 0, ReadBuffer.Length)

            O.Post(Sub(x) OnReadPacket(DirectCast(x, Byte())), BufferClone)
        End If

        If Read < (length - index) Then
            HandleRead(data, index + Read, length)
        End If
    End Sub

    Private Sub ReadProgressChangedCallback(arg As Object)
        Dim Params As Object() = DirectCast(arg, Object())
        OnReadProgressChanged(DirectCast(Params(0), Double), DirectCast(Params(1), Integer), DirectCast(Params(2), Integer))
    End Sub

    Private Sub WriteProgressChangedCallback(arg As Object)
        Dim Params As Object() = DirectCast(arg, Object())
        OnWriteProgressChanged(DirectCast(Params(0), Double), DirectCast(Params(1), Integer), DirectCast(Params(2), Integer))
    End Sub

#Region " IDisposable Support "

    Private DisposedValue As Boolean

    Private Sub Dispose(disposing As Boolean)
        If Not DisposedValue AndAlso disposing Then Disconnect()
        DisposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class