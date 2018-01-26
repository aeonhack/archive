Public Class WebFileDownloader
    Public Event AmountDownloadedChanged(ByVal iNewProgress As Long)
    Public Event FileDownloadSizeObtained(ByVal iFileSize As Long)
    Public Event FileDownloadComplete()
    Public Event FileDownloadFailed(ByVal ex As Exception)
    Private mCurrentFile As String = String.Empty
    Public ReadOnly Property CurrentFile() As String
        Get
            Return mCurrentFile
        End Get
    End Property
    Public Function DownloadFile(ByVal URL As String, ByVal Location As String) As Boolean
        Try
            mCurrentFile = GetFileName(URL)
            Dim WC As New Net.WebClient
            WC.DownloadFile(URL, Location)
            RaiseEvent FileDownloadComplete()
            Return True
        Catch ex As Exception
            RaiseEvent FileDownloadFailed(ex)
            Return False
        End Try
    End Function
    Private Function GetFileName(ByVal URL As String) As String
        Try
            Return URL.Substring(URL.LastIndexOf("/") + 1)
        Catch ex As Exception
            Return URL
        End Try
    End Function
    Public Function DownloadFileWithProgress(ByVal URL As String, ByVal Location As String) As Boolean
        Dim FS As IO.FileStream = Nothing
        Try
            mCurrentFile = GetFileName(URL)
            Dim wRemote As Net.WebRequest
            Dim bBuffer As Byte()
            ReDim bBuffer(256)
            Dim iBytesRead As Integer
            Dim iTotalBytesRead As Integer
            FS = New IO.FileStream(Location, IO.FileMode.Create, IO.FileAccess.Write)
            wRemote = Net.WebRequest.Create(URL)
            Dim myWebResponse As Net.WebResponse = wRemote.GetResponse
            RaiseEvent FileDownloadSizeObtained(myWebResponse.ContentLength)
            Dim sChunks As IO.Stream = myWebResponse.GetResponseStream
            Do
                If Main.State = 50 Then Return False
                iBytesRead = sChunks.Read(bBuffer, 0, 256)
                FS.Write(bBuffer, 0, iBytesRead)
                iTotalBytesRead += iBytesRead
                If myWebResponse.ContentLength < iTotalBytesRead Then
                    RaiseEvent AmountDownloadedChanged(myWebResponse.ContentLength)
                Else
                    RaiseEvent AmountDownloadedChanged(iTotalBytesRead)
                End If
            Loop While Not iBytesRead = 0
            sChunks.Close()
            FS.Close()
            RaiseEvent FileDownloadComplete()
            Return True
        Catch Ex As Exception
            If Not (FS Is Nothing) Then
                FS.Close()
                FS = Nothing
            End If
            RaiseEvent FileDownloadFailed(Ex)
            Return False
        End Try
    End Function
    Public Shared Function FormatFileSize(ByVal Size As Long) As String
        Dim KB As Integer = 1024
        Dim MB As Integer = KB * KB
        If Size < KB Then
            Return FormatNumber(Size, 0) & " bytes"
        Else
            Select Case Size / KB
                Case Is < 1000
                    Return FormatNumber(Math.Round(Size / KB), 0) & "KB"
                Case Is < 1000000
                    Return FormatNumber(Math.Round(Size / MB), 0) & "MB"
                Case Is < 10000000
                    Return FormatNumber(Math.Round(Size / MB / KB), 0) & "GB"
            End Select
        End If
        Return FormatNumber(Size)
    End Function
End Class
