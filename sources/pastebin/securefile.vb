Imports System.IO
Imports System.Security.Cryptography

'------------------
'Creator: aeonhack
'Site: elitevs.net
'Created: 4/16/2011
'Changed: 4/24/2012
'Version: 1.0.0
'------------------

Class SecureFile

    Enum Status As Byte
        Success = 0
        Failed = 1
        Progress = 2
    End Enum

    Private Current As Long
    Private Maximum As Long

    Private Data As Byte()
    Private Destination As String

    Private SR, SW As Stream
    Private CS As CryptoStream
    Private FSR, FSW As FileStream
    Private SA As SymmetricAlgorithm

    Private Callback As StatusCB
    Public Delegate Sub StatusCB(status As Status, value As Integer)

    Sub New(_sa As SymmetricAlgorithm, _callback As StatusCB)
        SA = _sa
        Callback = _callback
        Data = New Byte(5242880 - 1) {}
    End Sub

    Public Sub Secure([in] As String, out As String)
        Try
            Initialize([in], out)
            CS = New CryptoStream(FSW, SA.CreateEncryptor(), CryptoStreamMode.Write)

            SR = FSR
            SW = CS
            BeginProcess()
        Catch
            HandleFailure()
        End Try
    End Sub

    Public Sub Unsecure([in] As String, out As String)
        Try
            Initialize([in], out)
            CS = New CryptoStream(FSR, SA.CreateDecryptor(), CryptoStreamMode.Read)

            SR = CS
            SW = FSW
            BeginProcess()
        Catch
            HandleFailure()
        End Try
    End Sub

    Private Sub Initialize([in] As String, out As String)
        Destination = out
        FSW = New FileStream(out, FileMode.Create, FileAccess.Write)
        FSR = New FileStream([in], FileMode.Open, FileAccess.Read)
    End Sub

    Private Sub BeginProcess()
        Current = 0
        Maximum = FSR.Length

        Dim O As IAsyncResult = SR.BeginRead(Data, 0, Data.Length, AddressOf Process, Nothing)
        If O.CompletedSynchronously Then Process(O)
    End Sub

    Private Sub Process(r As IAsyncResult)
        If _Cancel Then
            HandleFailure()
            Return
        End If

        Try
            Dim I As Integer = SR.EndRead(r)
            Current += I

            If Not I = 0 Then
                SW.Write(Data, 0, I)
                Callback(Status.Progress, CInt(Current / Maximum * 100))

                Dim O As IAsyncResult = SR.BeginRead(Data, 0, Data.Length, AddressOf Process, Nothing)
                If O.CompletedSynchronously Then Process(O)
            Else
                CS.Close()
                FSW.Close()
                FSR.Close()

                Callback(Status.Progress, 100)
                Callback(Status.Success, 0)
            End If
        Catch  'CryptoException for pass mismatch
            HandleFailure()
        End Try
    End Sub

    Private Sub HandleFailure()
        Try
            If CS IsNot Nothing Then CS.Close()
        Catch
            'Do nothing
        End Try

        If FSR IsNot Nothing Then FSR.Close()
        If FSW IsNot Nothing Then FSW.Close()

        Try
            File.Delete(Destination)
        Catch
            'Do nothing.
        End Try

        Callback(Status.Failed, 0)
    End Sub

    Private _Cancel As Boolean
    Public Sub Cancel()
        _Cancel = True
    End Sub

End Class