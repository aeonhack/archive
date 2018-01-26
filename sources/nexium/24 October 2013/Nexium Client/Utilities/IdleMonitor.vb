Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Threading

Public Class IdleMonitor

    Public Event IdleEnter(sender As Object)
    Public Event IdleLeave(sender As Object)

    <DllImport("user32.dll")> _
    Private Shared Function GetLastInputInfo(ByRef lastInput As LASTINPUTINFO) As Boolean
    End Function

    Private _SecondsBeforeIdle As Integer = 600
    Public Property SecondsBeforeIdle() As Integer
        Get
            Return _SecondsBeforeIdle
        End Get
        Set(ByVal value As Integer)
            If value < 10 Then
                Throw New Exception("SecondsBeforeIdle must be at least 10.")
            End If
            _SecondsBeforeIdle = value
        End Set
    End Property

    Private Timer As Timer
    Private Operation As AsyncOperation

    Private UserIsIdle As Boolean
    Private LastInput As LASTINPUTINFO

    Sub New()
        LastInput = New LASTINPUTINFO()
        LastInput.Size = Marshal.SizeOf(GetType(LASTINPUTINFO))

        Operation = AsyncOperationManager.CreateOperation(Nothing)
        Timer = New Timer(AddressOf TimerCallback, Nothing, Timeout.Infinite, Timeout.Infinite)
    End Sub

    Public Sub Start()
        GetLastInputInfo(LastInput)
        Timer.Change(0, 2000)
    End Sub

    Public Sub [Stop]()
        Timer.Change(Timeout.Infinite, Timeout.Infinite)
    End Sub

    Private Sub TimerCallback(state As Object)
        If Not GetLastInputInfo(LastInput) Then Return

        Dim InputTime As Long = LastInput.Time
        Dim SystemTime As Long = Environment.TickCount

        If Math.Abs(SystemTime - InputTime) >= (1000 * _SecondsBeforeIdle) Then
            If UserIsIdle Then Return

            UserIsIdle = True
            Operation.Post(Sub() RaiseEvent IdleEnter(Me), Nothing)
        ElseIf UserIsIdle Then
            UserIsIdle = False
            Operation.Post(Sub() RaiseEvent IdleLeave(Me), Nothing)
        End If
    End Sub

    Private Structure LASTINPUTINFO
        Public Size As Integer
        Public Time As UInteger
    End Structure

End Class
