Imports System, System.Security.Cryptography, System.Text, System.ComponentModel, System.Runtime.Serialization.Formatters.Binary, System.IO _
, System.Windows.Forms, System.Threading, System.Drawing, System.Text.RegularExpressions, System.Net, System.Environment, Microsoft.Win32 _
, System.Security.Principal, System.Drawing.Imaging, Omni.Shift, Omni.Perform, Omni.General, System.Runtime.InteropServices, System.Drawing.Drawing2D _
, System.Collections.Generic, System.Diagnostics, Microsoft.VisualBasic, System.Globalization, Omni.Native, System.Net.Sockets, System.Runtime.Serialization

<Assembly: CLSCompliant(True)> 
'Created: Friday, October 10th, 2008 -
'Updated: Monday, March 15th, 2010.
'
'  Omni, provides developers easy-to-use methods, and classes for their software.
'Copyright(C) 2009 Taylor M. Huddleston
'
'  This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'  This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details:
'
'www.gnu.org/licenses
Public NotInheritable Class Guard
    Private Sub New()
    End Sub
    <EditorBrowsable(1)> Enum Securer : Rijndael = 0 : RC2 = 1 : Des = 2 : TripleDes = 3 : End Enum
    <EditorBrowsable(1)> Enum Hasher : MD5 = 0 : Sha1 = 1 : Sha256 = 2 : Sha384 = 3 : Sha512 = 4 : End Enum
    <EditorBrowsable(1)> Enum Emblem : Letters = 0 : Numbers = 1 : Symbols = 2 : End Enum
    Shared Function Scramble(ByVal data As String) As String
        For I = data.Length To 1 Step -2 : Scramble &= Mid(data, I, 1) : Next
        For I = data.Length - 1 To 1 Step -2 : Scramble &= Mid(data, I, 1) : Next
    End Function
    Shared Function Unscramble(ByVal data As String) As String
        Dim T = data.Length Mod 2, X = data.Length \ 2
        For I = X + T To 1 Step -1 : If T = 0 Then Unscramble &= Mid(data, I + X, 1) : Unscramble &= Mid(data, I, 1)
            If T = 1 And I <> 1 Then Unscramble &= Mid(data, I + X, 1)
        Next
    End Function
    Shared Function Swap(ByVal data As String) As String
        For Each C In data : Swap &= Chr(Asc(C) Xor 128) : Next
    End Function
    Shared Function Secure(ByVal data As String, ByVal password As String, Optional ByVal method As Securer = 3) As String
        Return New Shift.Matrix(Secure(New Omni.Shift.Matrix(data).Value, New Shift.Matrix(password).Value, method)).ToString(True)
    End Function
    Shared Function Secure(ByVal data As Byte(), ByVal password As Byte(), Optional ByVal method As Securer = 3) As Byte()
        Using T = SymmetricAlgorithm.Create(method.ToString)
            T.IV = If(method = 0, Hash(password), Truncate(Hash(password), 8))
            T.Key = If(method = 2, Truncate(Hash(password), 8), Hash(password))
            Return T.CreateEncryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function
    Shared Function Unsecure(ByVal data As String, ByVal password As String, Optional ByVal method As Securer = 3) As String
        Return New Shift.Matrix(Unsecure(New Omni.Shift.Matrix(data, True).Value, New Shift.Matrix(password).Value, method)).ToString()
    End Function
    Shared Function Unsecure(ByVal data As Byte(), ByVal password As Byte(), Optional ByVal method As Securer = 3) As Byte()
        Using S = SymmetricAlgorithm.Create(method.ToString)
            S.IV = If(method = 0, Hash(password), Truncate(Hash(password), 8))
            S.Key = If(method = 2, Truncate(Hash(password), 8), Hash(password))
            Return S.CreateDecryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function
    Shared Function Hash(ByVal data As String, Optional ByVal method As Hasher = 0) As String
        Return BitConverter.ToString(DoHash(New Shift.Matrix(data).Value, method)).Replace("-", "")
    End Function
    Shared Function Hash(ByVal data As Byte(), Optional ByVal method As Hasher = 0) As Byte()
        Return DoHash(data, method)
    End Function
    Shared Function HashFile(ByVal path As String, Optional ByVal method As Hasher = 0) As String
        Using T As New FileStream(path, FileMode.Open, FileAccess.Read)
            Return BitConverter.ToString(DoHash(T, method)).Replace("-", "")
        End Using
    End Function
    Private Shared Function DoHash(ByRef data As Object, ByRef method As Hasher) As Byte()
        Using H = HashAlgorithm.Create(method.ToString) : Return H.ComputeHash(data) : End Using
    End Function
    Shared Function Random(Optional ByVal length As Integer = 16, Optional ByVal type As Emblem = 0) As String
        Dim T = New String() {"abcdefgijkmnopqrstwxyz", "0123456789", "`~!@#$%^&*()_-+={[}]\:;""'<,>.?/"}
        While Random.Length < length : Random &= T(type)(New Random(Seed).Next(T(type).Length)) : End While
    End Function
    'TODO Apparently this needs elevation to work. (probably because virtualization)
    Shared Function Machine() As String
        Dim I = "", M = GetObject("WinMgmts:"), T = M.InstancesOf("Win32_BaseBoard")
        For Each S In T : I &= S.SerialNumber : Next : T = M.InstancesOf("Win32_Processor") : For Each S In T : I &= S.ProcessorId : Next
        I &= GetObject("WinMgmts:Win32_LogicalDisk='" & SystemDirectory.Substring(0, 1) & ":'").VolumeSerialNumber
        T = M.InstancesOf("Win32_SystemEnclosure") : For Each S In T : I &= S.SerialNumber : Next : Return I.Trim
    End Function
    Shared Function Serial(Optional ByVal length As Integer = 12) As String
        While Serial.Length < length : Serial &= Guid.NewGuid.ToString.Replace("-", "") : End While
        Return Serial.Substring(0, length)
    End Function
    Class Protector
        Event Changed(ByVal sender As Object, ByVal e As UpdateEvent) : Event Starting(ByVal sender As Object, ByVal e As UpdateEvent)
        Event Complete(ByVal sender As Object, ByVal e As UpdateEvent) : Event Failed(ByVal sender As Object, ByVal e As ErrorEventArgs)
        Private X, T As FileStream, W As CryptoStream, Data(2560) As Byte, Maximum, Current As Long
        Sub Cancel()
            W.Close() : X.Close() : T.Close() : Current = 0
        End Sub
        Sub Protect(ByVal path As String, ByVal password As String, Optional ByVal method As Securer = 0)
            Process(path, New Shift.Matrix(password).Value, method, 0)
        End Sub
        Sub Protect(ByVal path As String, ByVal password As Byte(), Optional ByVal method As Securer = 0)
            Process(path, password, method, 0)
        End Sub
        Sub Unprotect(ByVal path As String, ByVal password As String, Optional ByVal method As Securer = 0)
            Process(path, New Shift.Matrix(password).Value, method, 1)
        End Sub
        Sub Unprotect(ByVal path As String, ByVal password As Byte(), Optional ByVal method As Securer = 0)
            Process(path, password, method, 1)
        End Sub
        Private Sub Process(ByVal path As String, ByVal password As Byte(), ByVal m As Securer, ByVal u As Boolean)
            X = New FileStream(IO.Path.ChangeExtension(path, m.ToString), FileMode.Create, FileAccess.Write)
            Using A = SymmetricAlgorithm.Create(m.ToString)
                A.IV = If(m = 0, Hash(password), Truncate(Hash(password), 8))
                A.Key = If(m = 2, Truncate(Hash(password), 8), Hash(password))
                W = New CryptoStream(X, If(u, A.CreateDecryptor(), A.CreateEncryptor()), CryptoStreamMode.Write)
            End Using
            T = New FileStream(path, FileMode.Open, FileAccess.Read) : Maximum = T.Length
            Raise(StartingEvent, Me, New UpdateEvent(path, 0, Maximum))
            T.BeginRead(Data, 0, Data.Length, AddressOf Read, 0)
        End Sub
        Private Sub Read(ByVal r As IAsyncResult)
            Try
                Dim I = T.EndRead(r)
                If I > 0 Then
                    W.Write(Data, 0, I) : Current += I
                    Raise(ChangedEvent, Me, New UpdateEvent(T.Name, Current, Maximum))
                    T.BeginRead(Data, 0, Data.Length, AddressOf Read, 0)
                Else
                    Cancel() : My.Computer.FileSystem.MoveFile(X.Name, T.Name, True)
                    Raise(CompleteEvent, Me, New UpdateEvent(T.Name, Maximum, Maximum))
                End If
            Catch e As Exception : If Current > 0 Then : Fail(e) : End If : End Try
        End Sub
        Private Sub Fail(ByVal e As Exception)
            Try : Cancel() : IO.File.Delete(T.Name) : Catch : End Try : Raise(FailedEvent, Me, New ErrorEventArgs(e))
        End Sub
        <EditorBrowsable(1)> Class UpdateEvent
            Private _File As String, _Current, _Maximum
            ReadOnly Property Progress() As Double
                Get
                    Return 100 * _Current / _Maximum
                End Get
            End Property
            ReadOnly Property File() As String
                Get
                    Return _File
                End Get
            End Property
            ReadOnly Property FileName() As String
                Get
                    Return _File.Remove(0, _File.LastIndexOf("\") + 1)
                End Get
            End Property
            ReadOnly Property Current() As Long
                Get
                    Return _Current
                End Get
            End Property
            ReadOnly Property Maximum() As Long
                Get
                    Return _Maximum
                End Get
            End Property
            Sub New(ByVal file As String, ByVal current As Long, ByVal maximum As Long)
                _File = file : _Current = current : _Maximum = maximum
            End Sub
        End Class
    End Class
End Class
Public NotInheritable Class Check
    Private Sub New()
    End Sub
    Shared Function Password(ByVal data As String) As Integer
        Dim S = New String() {"[a-z]", "[A-Z]", "\W", "\d"}, I As Double
        For Each M In S : If Regex.IsMatch(data, M) Then I += 22.5
        Next : Return I + data.Length
    End Function
    Shared Function Password(ByVal data As String, ByVal minimum As Integer) As Boolean
        Return Password(data) >= minimum
    End Function
    Shared Function Email(ByVal data As String) As Boolean
        Return Regex.IsMatch(data, "^[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", 1)
    End Function
    Shared Function FileName(ByVal data As String) As Boolean
        Return Regex.IsMatch(data, "^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\""|/<>]+$")
    End Function
    Shared Function Connection() As Boolean
        If Not My.Computer.Network.IsAvailable Then Return False
        Using W = WebRequest.Create("http://www.Microsoft.com/").GetResponse : Return True : End Using
    End Function
    'TODO Don't use Assembly.LoadFile        
    Shared Function Managed(ByVal path As String) As Boolean
        Try : Reflection.Assembly.LoadFile(path) : Catch : Return False : End Try : Return True
    End Function
End Class
Public NotInheritable Class Nexus
    Private Sub New()
    End Sub
    Shared Sub Email(ByVal host As String, ByVal port As Integer, ByVal [from] As String, ByVal password As String, ByVal [to] As String, ByVal body As String, Optional ByVal ssl As Boolean = True, Optional ByVal subject As String = "None", Optional ByVal timeout As Integer = 60000, Optional ByVal files As String() = Nothing)
        Using M As New Mail.MailMessage()
            With M
                .From = New Mail.MailAddress([from]) : .To.Add([to]) : .Subject = subject : .Body = body
                If files IsNot Nothing Then For Each F In files : .Attachments.Add(New Mail.Attachment(F)) : Next
            End With
            Dim C = New Mail.SmtpClient(host, port) : With C
                .EnableSsl = ssl : .Timeout = timeout : .Credentials = New NetworkCredential([from], password) : .Send(M)
            End With
        End Using
    End Sub
    'TODO Implement timeout
    Shared Function Scan(ByVal host As String, ByVal port As Integer) As Boolean
        Using T As New Socket(2, 1, 6)
            Try : T.BeginConnect(host, port, Nothing, 0)
            Catch : Scan = False : End Try : T.Close()
        End Using : Scan = True
    End Function
    Shared Function Resolve(ByVal host As String) As String()
        Dim I As New List(Of String) : For Each A In Dns.GetHostEntry(host).AddressList : I.Add(A.ToString) : Next : Return I.ToArray
    End Function
    Shared Function HostName(ByVal host As String) As String
        Return Dns.GetHostEntry(host).HostName
    End Function
    Shared Function External() As String
        For Each H In My.Resources.Hosts.Split("|")
            Try : Dim T = WebRequest.Create(New Uri(H)) : T.Timeout = 3000
                Using SR As New StreamReader(T.GetResponse.GetResponseStream)
                    Return Regex.Match(SR.ReadToEnd, "(?:\d{1,3}\.){3}\d{1,3}").Value
                End Using
            Catch : End Try
        Next : Return String.Empty
    End Function
    Shared Function Internal() As String
        Return Dns.GetHostByName(Dns.GetHostName).AddressList(0).ToString
    End Function
    Shared Function Local() As String
        Return IPAddress.Loopback.ToString
    End Function
    Public Class Server
        Event Incoming(ByVal c As Client, ByVal d As Byte()) : Event Status(ByVal c As Client, ByVal state As State, ByRef decline As Boolean)
        Enum State : Connected = 1 : Disconnected = 2 : End Enum
        Private H As Socket, _Listening As Boolean, _Connections As New Dictionary(Of String, Client)
        Public ReadOnly Property Connections() As Dictionary(Of String, Client)
            Get
                Return _Connections
            End Get
        End Property
        ReadOnly Property Listening()
            Get
                Return _Listening
            End Get
        End Property
        Sub Listen(ByVal port As Integer)
            H = New Socket(2, 1, 6) : H.Bind(New IPEndPoint(IPAddress.Any, port))
            H.Listen(10) : _Listening = True : H.BeginAccept(AddressOf Accept, 0)
        End Sub
        Sub Disconnect()
            H.Close() : _Connections.Clear() : _Listening = False
        End Sub
        Private Sub Accept(ByVal r As IAsyncResult)
            Try
                OnStatus(New Client(H.EndAccept(r)), 1)
                H.BeginAccept(AddressOf Accept, 0)
            Catch : End Try
        End Sub
        Private Sub OnStatus(ByVal s As Client, ByVal e As State)
            If e = 1 Then
                _Connections.Add(s.ID, s)
                Dim D As Boolean : Raise(StatusEvent, s, e, D)
                If D Then : s.Disconnect() : _Connections.Remove(s.ID) : Return : End If
                AddHandler s.Status, AddressOf OnStatus : AddHandler s.Incoming, AddressOf OnIncoming
            Else
                _Connections.Remove(s.ID)
            End If
        End Sub
        Private Sub OnIncoming(ByVal s As Client, ByVal data As Byte())
            Raise(IncomingEvent, s, data)
        End Sub
    End Class
    Public Class Client
        Event Incoming(ByVal c As Client, ByVal d As Byte()) : Event Status(ByVal c As Client, ByVal state As State)
        Enum State : Connected = 1 : Disconnected = 2 : End Enum
        Private H As Socket, Data(8192) As Byte, _ID As String, _EP As EndPoint
        ReadOnly Property Connected() As Boolean
            Get
                If H IsNot Nothing Then Return H.Connected
            End Get
        End Property
        ReadOnly Property ID() As String
            Get
                Return _ID
            End Get
        End Property
        ReadOnly Property EndPoint() As EndPoint
            Get
                Return _EP
            End Get
        End Property
        Sub New()
        End Sub
        Sub New(ByVal socket As Socket)
            H = socket : Read(False)
        End Sub
        Sub Connect(ByVal host As String, ByVal port As Integer)
            H = New Socket(2, 1, 6) : H.BeginConnect(host, port, New AsyncCallback(AddressOf Connect), 0)
        End Sub
        Private Sub Connect(ByVal r As IAsyncResult)
            Try : H.EndConnect(r) : Read(True) : Catch : Raise(StatusEvent, Me, 2) : End Try
        End Sub
        Sub Disconnect()
            H.Close()
        End Sub
        Private Sub Read(ByVal r As Boolean)
            _ID = Guid.NewGuid.ToString : _EP = H.RemoteEndPoint : If r Then Raise(StatusEvent, Me, 1)
            H.BeginReceive(Data, 0, Data.Length, 0, New AsyncCallback(AddressOf Read), 0)
        End Sub
        Private Sub Read(ByVal r As IAsyncResult)
            Try
                Dim T = H.EndReceive(r)
                If T > 0 Then
                    Raise(IncomingEvent, Me, Truncate(Data, T))
                    H.BeginReceive(Data, 0, Data.Length, 0, New AsyncCallback(AddressOf Read), 0)
                Else : Disconnect() : Raise(StatusEvent, Me, 2) : End If
            Catch : Raise(StatusEvent, Me, 2) : End Try
        End Sub
        Sub Send(ByVal data As Byte())
            Try : H.BeginSend(data, 0, data.Length, 0, New AsyncCallback(AddressOf Send), 0) : Catch : End Try
        End Sub
        Private Sub Send(ByVal r As IAsyncResult)
            H.EndSend(r)
        End Sub
    End Class
End Class
Public NotInheritable Class Render
    Private Sub New()
    End Sub
    Shared Function Resize(ByVal image As Image, ByVal percent As Double) As Image
        Return New Bitmap(image, image.Width * percent * 0.01, image.Height * percent * 0.01).Clone
    End Function
    Shared Function Resize(ByVal image As Image, ByVal width As Integer, ByVal height As Integer) As Image
        Return New Bitmap(image, width, height).Clone
    End Function
    Shared Function Crop(ByVal image As Image, ByVal width As Integer, ByVal height As Integer, Optional ByVal x As Integer = 0, Optional ByVal y As Integer = 0) As Bitmap
        Using B As New Bitmap(width, height)
            Using G = Graphics.FromImage(B)
                G.DrawImage(image, New Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel) : Return B.Clone
            End Using
        End Using
    End Function
    Shared Function Capture(ByVal width As Integer, ByVal height As Integer, Optional ByVal x As Integer = 0, Optional ByVal y As Integer = 0) As Bitmap
        Using B As New Bitmap(width, height)
            Using G = Graphics.FromImage(B)
                G.CopyFromScreen(New Point(x, y), New Point(0, 0), B.Size) : Return B.Clone
            End Using
        End Using
    End Function
    Shared Function Compare(ByVal image As Image, ByVal comparison As Image) As Boolean
        If image.Size <> comparison.Size Then Return False
        Dim T As New ImageConverter
        Dim I1 As Byte() = T.ConvertTo(image, GetType(Byte())), I2 As Byte() = T.ConvertTo(comparison, GetType(Byte()))
        If I1.Length <> I2.Length Then Return False
        For I = 50 To I1.Length Step 12 : If I1(I) <> I2(I) Then : Return False : End If : Next : Return True
    End Function
    Shared Function Pixel(ByVal x As Integer, ByVal y As Integer) As Color
        Using B As New Bitmap(1, 1)
            Using G = Graphics.FromImage(B)
                G.CopyFromScreen(New Point(x, y), New Point(0, 0), B.Size) : Return B.GetPixel(0, 0)
            End Using
        End Using
    End Function
    Shared Sub Save(ByVal image As Image, ByVal path As String, Optional ByVal compression As Integer = 100)
        Dim T = New EncoderParameters(1) : T.Param(0) = New EncoderParameter(Imaging.Encoder.Quality, compression)
        For Each I In ImageCodecInfo.GetImageEncoders : If I.MimeType = "image/jpeg" Then : image.Save(path, I, T) : Return : End If : Next
    End Sub
    Shared Sub Wallpaper(ByVal image As Image)
        image.Save(SystemDirectory & "\Wallpaper.bmp", ImageFormat.Bmp) : DoWallpaper(SystemDirectory & "\Wallpaper.bmp")
    End Sub
    Shared Sub Wallpaper(ByVal path As String)
        Image.FromFile(path).Save(SystemDirectory & "\Wallpaper.bmp", ImageFormat.Bmp) : DoWallpaper(SystemDirectory & "\Wallpaper.bmp")
    End Sub
    Private Shared Function DoWallpaper(ByVal Path As String) As Integer
        Return SystemParametersInfo(20, 0, Path, 1 Or 2)
    End Function
    'TODO ....fml..
    Shared Function Gradient(ByVal value As Double, ByVal ParamArray colors As Color()) As Color
        Dim T As New ColorBlend(colors.Length), M As New List(Of Single), X = CInt(100 / (colors.Length - 1))
        M.Add(0.0) : For I = X To 100 - X Step X : M.Add(I * 0.01) : Next : M.Add(1.0)
        T.Colors = colors : T.Positions = M.ToArray : Using V As New Bitmap(1001, 1)
            Using I As New LinearGradientBrush(New Point(0, 0), V.Size, Color.Black, Color.Black)
                I.InterpolationColors = T : Graphics.FromImage(V).FillRectangle(I, 0, 0, 1001, 1)
                Return V.GetPixel(value * 10, 0) : End Using : End Using
    End Function
End Class
Public NotInheritable Class Shift
    Private Sub New()
    End Sub
    'TODO: get rid of this or something..
    Class Matrix
        Public Value As Byte()
        Sub New(ByVal data As String, Optional ByVal base64 As Boolean = False)
            If base64 Then Value = Convert.FromBase64String(data) Else Value = Encoding.ASCII.GetBytes(data)
        End Sub
        Sub New(ByVal data As Byte())
            Value = data
        End Sub
        Shadows Function ToString(Optional ByVal base64 As Boolean = False) As String
            If base64 Then Return Convert.ToBase64String(Value) Else Return Encoding.ASCII.GetString(Value)
        End Function
    End Class
    Shared Function Hexadecimal(ByVal data As String, Optional ByVal reverse As Boolean = False) As String
        If reverse Then
            For I = 0 To data.Length - 2 Step 2
                Hexadecimal &= Chr(Convert.ToByte(data.Substring(I, 2), 16))
            Next
        Else : Return BitConverter.ToString(Encoding.ASCII.GetBytes(data)).Replace("-", "") : End If
    End Function
    Shared Function Serialize(ByVal data As Object) As Byte()
        If TypeOf data Is Byte() Then Return data
        Using M As New MemoryStream
            Dim F As New BinaryFormatter() : F.Serialize(M, data) : Return M.ToArray()
        End Using
    End Function
    Shared Function Deserialize(ByVal data As Byte()) As Object
        Using M As New MemoryStream(data, False)
            Return (New BinaryFormatter).Deserialize(M)
        End Using
    End Function
    Shared Function Protocol(ByVal address As String) As Long
        Dim Data = Split(address, ".") : Return CLng(Data(3) + Data(2) * 256 + Data(1) * 256 ^ 2 + Data(0) * 256 ^ 3)
    End Function
    Shared Function Protocol(ByVal address As Long) As String
        Dim Data = New IPAddress(address).ToString.Split(".") : Return Data(3) & "." & Data(2) & "." & Data(1) & "." & Data(0)
    End Function
End Class
Public NotInheritable Class Core
    Private Sub New()
    End Sub
    <EditorBrowsable(1)> Enum Key : Dynamic = 0 : Configuration = 1 : Performance = 2 : Users = 3 : Machine = 4 : Current = 5 : Classes = 6 : End Enum
    <EditorBrowsable(1)> Enum State : Hide = 0 : Focus = 1 : Maximize = 3 : Minimize = 6 : Show = 8 : Restore = 9 : End Enum
    Shared Function Disable(ByVal setting As Boolean) As Boolean
        Return Native.BlockInput(setting)
    End Function
    Shared Function Window(ByVal handle As IntPtr, ByVal state As State) As Integer
        Return Native.ShowWindow(handle, state)
    End Function
    Shared Function Handle(ByVal name As String) As IntPtr
        Handle = Process.GetProcessesByName(name)(0).MainWindowHandle
    End Function
    Shared Sub Register(ByVal name As String, ByVal path As String, Optional ByVal key As Key = 5)
        Root(key).OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True).SetValue(name, path, 1)
    End Sub
    Shared Sub Unregister(ByVal name As String, Optional ByVal key As Key = 5)
        Root(key).OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(name)
    End Sub
    Shared Function HasRegistration(ByVal name As String, Optional ByVal key As Key = 5) As Boolean
        Return Root(key).OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run").GetValue(name) <> Nothing
    End Function
    Shared Sub Associate(ByVal extension As String, ByVal path As String, Optional ByVal description As String = "None", Optional ByVal icon As String = "")
        Root(6).CreateSubKey(extension).SetValue("", description, 1)
        Root(6).CreateSubKey(extension & "\Shell\Open\Command").SetValue("", path & " ""%l""", 1)
        If icon <> "" Then Root(0).CreateSubKey(extension & "\DefaultIcon").SetValue("", icon, 1)
    End Sub
    Shared Sub Dissociate(ByVal extension As String)
        Root(6).DeleteSubKeyTree(extension)
    End Sub
    Shared Function HasAssociation(ByVal extension As String) As Boolean
        Return Root(6).OpenSubKey(extension).GetValue("") <> Nothing
    End Function
    Shared Sub GetPermission(ByVal file As String, Optional ByVal arguments As String = "")
        Dim I = New ProcessStartInfo(file, arguments) : I.Verb = "runas" : Process.Start(I)
    End Sub
    Shared Sub GetPermission()
        Dim I = New ProcessStartInfo(Application.ExecutablePath) : I.Verb = "runas" : Process.Start(I) : Application.Exit()
    End Sub
    Shared Function HasPermission() As Boolean
        Return New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole("BUILTIN\Administrators")
    End Function
    Shared Function Mass(ByVal directory As String, Optional ByVal children As Boolean = True) As Double
        For Each T In My.Computer.FileSystem.GetDirectoryInfo(directory).GetFiles("*", If(children, 1, 0)) : Mass += T.Length : Next
    End Function
    Shared Function Root(ByVal k As Key) As RegistryKey
        Return RegistryKey.OpenRemoteBaseKey(DirectCast(-2147483642 - k, RegistryHive), String.Empty)
    End Function
    Shared Function Current() As Keys
        For I = 8 To 222 : If GetAsyncKeyState(I) = -32767 Then : Return I : End If : Next : Return -1
    End Function
    Shared Sub Execute(ByVal data As Byte())
        Dim T As New Thread(AddressOf DoExecute) : T.Start(New Object() {data})
    End Sub
    Private Shared Sub DoExecute(ByVal data As Object())
        Reflection.Assembly.Load(data(0)).EntryPoint.Invoke(Nothing, New Object() {New String() {}})
    End Sub
    Class Mouse
        Private Delegate Function HP(ByVal code As Integer, ByVal flags As Integer, ByRef data As MouseData) As Integer
        Private Structure MouseData : Dim Point As Point, Code, Flags, Time, Info As Integer : End Structure
        Event Up(ByVal sender As Object, ByRef e As MouseEvent) : Event Down(ByVal sender As Object, ByRef e As MouseEvent)
        Event Click(ByVal sender As Object, ByRef e As MouseEvent) : Event DoubleClick(ByVal sender As Object, ByRef e As MouseEvent)
        Event Wheel(ByVal sender As Object, ByRef e As MouseEvent) : Event Move(ByVal sender As Object, ByRef e As MouseEvent)
        Dim MH As Integer, Prior As Point, MD As HP
        Sub Install()
            MD = New HP(AddressOf Process)
            MH = SetWindowsHookExA(14, MD, Runtime.InteropServices.Marshal.GetHINSTANCE(Reflection.Assembly.GetExecutingAssembly.GetModules()(0)), 0)
        End Sub
        Function Uninstall() As Integer
            Return UnhookWindowsHookEx(MH)
        End Function
        Private Function Process(ByVal Code As Integer, ByVal Flags As Integer, ByRef Data As MouseData) As Integer
            If Code = 0 Then
                Dim T As MouseButtons, M, Delta As Integer, Down, Up As Boolean
                Select Case Flags
                    Case 513 : Down = True : T = 1048576 : M = 1 : Case 514 : Up = True : T = 1048576 : M = 1
                    Case 515 : T = 1048576 : M = 2 : Case 516 : Down = True : T = 2097152 : M = 1
                    Case 517 : Up = True : T = 2097152 : M = 1 : Case 518 : T = 2097152 : M = 2
                    Case 519 : Down = True : T = 4194304 : M = 1 : Case 520 : Up = True : T = 4194304 : M = 1
                    Case 521 : T = 4194304 : M = 2 : Case 522 : Delta = Data.Code >> 16
                End Select
                Dim E As New MouseEvent(T, M, Delta, Data.Point)
                If Up Then RaiseEvent Up(Me, E)
                If Down Then RaiseEvent Down(Me, E)
                If M > 0 Then RaiseEvent Click(Me, E)
                If M = 2 Then RaiseEvent DoubleClick(Me, E)
                If Data.Point <> Prior Then : Prior = Data.Point : RaiseEvent Move(Me, E) : End If
                If Delta <> 0 Then RaiseEvent Wheel(Me, E)
                If E.Handled Then Return -1
            End If
            Return CallNextHookEx(MH, Code, Flags, Data)
        End Function
        <EditorBrowsable(1)> Structure MouseEvent
            Private _Button As MouseButtons, _Clicks, _Delta As Integer, _Location As Point, _Handled As Boolean
            Public ReadOnly Property Button() As MouseButtons
                Get
                    Return _Button
                End Get
            End Property
            Public ReadOnly Property Clicks() As Integer
                Get
                    Return _Clicks
                End Get
            End Property
            Public ReadOnly Property Delta() As Integer
                Get
                    Return _Delta
                End Get
            End Property
            Public ReadOnly Property Location() As Point
                Get
                    Return _Location
                End Get
            End Property
            Public Property Handled() As Boolean
                Get
                    Return _Handled
                End Get
                Set(ByVal v As Boolean)
                    _Handled = v
                End Set
            End Property
            Sub New(ByVal button As MouseButtons, ByVal clicks As Integer, ByVal delta As Integer, ByVal location As Point)
                _Button = button : _Clicks = clicks : _Delta = delta : _Location = location
            End Sub
        End Structure
    End Class
    'TODO Support Modifiers
    Class KeyBoard
        Private Structure KeyData : Dim VK, Code As Integer, Flags As Integer, Time As Integer, Info As IntPtr : End Structure
        Private Delegate Function HP(ByVal code As Integer, ByVal flags As Integer, ByRef data As KeyData) As Integer
        Event Down(ByVal sender As Object, ByRef e As KeyBoardEvent) : Event Up(ByVal sender As Object, ByRef e As KeyBoardEvent)
        Private KH As Integer, KD As HP
        Sub Install()
            KD = New HP(AddressOf Process)
            KH = SetWindowsHookExA(13, KD, Runtime.InteropServices.Marshal.GetHINSTANCE(Reflection.Assembly.GetExecutingAssembly.GetModules()(0)), 0)
        End Sub
        Function Uninstall() As Integer
            Return UnhookWindowsHookEx(KH)
        End Function
        Private Function Process(ByVal code As Integer, ByVal flags As Integer, ByRef data As KeyData) As Integer
            If code = 0 Then
                Dim Down, Up As Boolean
                Select Case flags
                    Case 256, 260 : Down = True : Case 257, 261 : Up = True
                End Select
                Dim E As New KeyBoardEvent(data.VK)
                If Up Then RaiseEvent Up(Me, E)
                If Down Then RaiseEvent Down(Me, E)
                If E.Handled Then Return -1
            End If
            Return CallNextHookEx(KH, code, flags, data)
        End Function
        <EditorBrowsable(1)> Structure KeyBoardEvent
            Private _KeyCode As Keys, _Handled As Boolean
            Public ReadOnly Property KeyCode() As Keys
                Get
                    Return _KeyCode
                End Get
            End Property
            Public Property Handled() As Boolean
                Get
                    Return _Handled
                End Get
                Set(ByVal v As Boolean)
                    _Handled = v
                End Set
            End Property
            Sub New(ByVal keycode As Keys)
                _KeyCode = keycode
            End Sub
        End Structure
    End Class
    'TODO Support multiple hotkeys
    Class Shortcut : Inherits NativeWindow
        Enum Modifier : None = 0 : Alt = 1 : Ctrl = 2 : Shift = 4 : End Enum
        Event Pressed(ByVal ID As Integer)
        Sub New()
            CreateHandle(New CreateParams)
        End Sub
        Sub Register(ByVal ID As Integer, ByVal Modifier As Modifier, ByVal Key As Keys)
            RegisterHotKey(Handle, ID, Modifier, Key)
        End Sub
        Sub Unregister(ByVal ID As Integer)
            UnregisterHotKey(Handle, ID)
        End Sub
        Protected Overrides Sub WndProc(ByRef M As Message)
            If M.Msg = 786 Then RaiseEvent Pressed(M.WParam.ToInt32)
            MyBase.WndProc(M)
        End Sub
    End Class
End Class
Public NotInheritable Class Perform
    Private Sub New()
    End Sub
    <EditorBrowsable(1)> Enum Button : Left = 0 : Right = 1 : Middle = 2 : End Enum
    Shared Function Truncate(Of T)(ByVal data() As T, ByVal length As Integer) As T()
        Array.Resize(data, length) : Return data
    End Function
    Shared Function Merge(Of T)(ByVal origin() As T, ByVal destination() As T) As T()
        Dim Buffer(destination.Length + origin.Length - 1) As T : Array.Copy(destination, Buffer, destination.Length)
        Array.Copy(origin, 0, Buffer, destination.Length, origin.Length) : Return Buffer
    End Function
    Shared Sub Speak(ByVal text As String, Optional ByVal rate As Integer = 0)
        Dim T As New Thread(AddressOf DoSpeak) : T.Start(New Object() {text, rate})
    End Sub
    Private Shared Sub DoSpeak(ByVal data As Object())
        Dim T = CreateObject("sapi.spvoice") : T.Rate = data(1) : T.Speak(data(0))
    End Sub
    Shared Sub Sleep(ByVal delay As Integer)
        Dim Start = Now.AddMilliseconds(delay) : While Now < Start : Application.DoEvents() : Thread.Sleep(1) : End While
    End Sub
    Shared Sub Fade(ByVal form As Form, ByVal target As Integer, Optional ByVal rate As Integer = 20)
        Dim T = If(target > form.Opacity * 100, 1, -1) * 0.01
        While CInt(form.Opacity * 100) <> target : form.Opacity += T : Sleep(rate) : End While
    End Sub
    Shared Sub Shake(ByVal item As Control, Optional ByVal factor As Integer = 2, Optional ByVal count As Integer = 5, Optional ByVal rate As Integer = 20)
        For I = 1 To count + If(count Mod 2 = 1, 1, 0)
            item.Location = New Point(item.Location.X - factor, item.Location.Y) : factor *= -1 : Sleep(rate)
        Next
    End Sub
    Shared Sub Click(ByVal x As Integer, ByVal y As Integer, Optional ByVal button As Button = 0)
        Click(New Point(x, y), button)
    End Sub
    Shared Sub Click(Optional ByVal button As Button = 0)
        Click(Nothing, button)
    End Sub
    Shared Sub Click(ByVal point As Point, Optional ByVal button As Button = 0)
        If point <> Nothing Then Cursor.Position = point
        Select Case button
            Case 0 : mouse_event(6, 0, 0, 0, 0) : Case 1 : mouse_event(24, 0, 0, 0, 0) : Case 2 : mouse_event(96, 0, 0, 0, 0)
        End Select
    End Sub
    Shared Sub Kill(ByVal name As String, Optional ByVal all As Boolean = False)
        For Each I In Process.GetProcesses : If I.ProcessName.ToLower(CultureInfo.InvariantCulture) = name.ToLower(CultureInfo.InvariantCulture) Then : I.Kill() : If Not all Then : Exit For : End If : End If : Next
    End Sub
End Class
Public NotInheritable Class Stats
    Private Sub New()
    End Sub
    'TODO I am sure this can be done in 1 line.
    Shared Function Memory(ByVal size As Double, Optional ByVal decimals As Integer = 2) As String
        If size >= 2 ^ 30 Then Return FormatNumber(size / 2 ^ 30, decimals) & " GB"
        If size >= 2 ^ 20 Then Return FormatNumber(size / 2 ^ 20, decimals) & " MB"
        Return FormatNumber(size / 2 ^ 10, decimals) & " KB"
    End Function
    Shared Function Duration(ByVal start As Date, ByVal total As Double, ByVal current As Double, ByVal format As String) As String
        If current = 0 Then Return Date.MinValue.ToString(format, CultureInfo.CurrentCulture)
        Return New Date(TimeSpan.FromMilliseconds((Now - start).TotalMilliseconds / current * total - current).Ticks).ToString(format, CultureInfo.CurrentCulture)
    End Function
    Shared Function Duration(ByVal start As Date, ByVal total As Double, ByVal current As Double) As TimeSpan
        If current = 0 Then Return TimeSpan.MinValue
        Return TimeSpan.FromMilliseconds((Now - start).TotalMilliseconds / current * total - current)
    End Function
    Shared Function Speed(ByVal start As Date, ByVal current As Double) As Double
        Return If(current = 0, 0, current / (Now - start).TotalSeconds)
    End Function
    Shared Function Progress(ByVal total As Double, ByVal current As Double, Optional ByVal decimals As Integer = 0) As Double
        Return If(total = 0, 100, Math.Round(100 / total * current, decimals))
    End Function
    Shared Function Chance(ByVal probability As Double) As Boolean
        Return New Random(Seed).Next(1, 100) >= 100 - probability
    End Function
End Class
Public NotInheritable Class General
    Private Sub New()
    End Sub
    <Serializable()> Class Storage
        Public Data As Object()
        Sub New(ByVal ParamArray arguments As Object())
            Data = arguments
        End Sub
    End Class
    Shared Function Seed() As Integer
        For Each I In Guid.NewGuid.ToByteArray : Seed += I : Next
    End Function
    Shared Function Enumerable(Of T)(ByVal index As Integer) As String
        Return [Enum].ToObject(GetType(T), index).ToString
    End Function
    Shared Function Enumerable(Of T)(ByVal name As String) As Integer
        Return Array.IndexOf([Enum].GetNames(GetType(T)), name)
    End Function
    Shared Function Words(ByVal data As String) As String()
        Dim I As New List(Of String) : For Each M In Regex.Matches(data, "\w+") : I.Add(M.Value) : Next : Return I.ToArray
    End Function
    Shared Function Lines(ByVal text As String) As String()
        Dim T As String, Output As New List(Of String) : Using SR As New StringReader(text)
            While SR.Peek > -1 : T = SR.ReadLine : If T <> "" Then : Output.Add(T) : End If : End While
        End Using : Return Output.ToArray
    End Function
    Shared Sub Raise(ByVal [event] As [Delegate], ByVal ParamArray data As Object())
        If [event] IsNot Nothing Then
            For Each C In [event].GetInvocationList
                If TypeOf C.Target Is ISynchronizeInvoke Then
                    Dim T = DirectCast(C.Target, ISynchronizeInvoke)
                    If T.InvokeRequired Then T.BeginInvoke(C, data) Else C.DynamicInvoke(data)
                Else
                    C.DynamicInvoke(data)
                End If
            Next
        End If
    End Sub
    Shared Function Clone(Of T)(ByVal data As T) As T
        Using M As New MemoryStream
            Dim F As New BinaryFormatter(Nothing, New StreamingContext(64))
            F.Serialize(M, data) : M.Position = 0 : Return F.Deserialize(M)
        End Using
    End Function
    Class Media
#Region " Declerations "
        Private Declare Function mciSendStringA Lib "winmm" (ByVal command As String, ByVal buffer As String, ByVal length As Integer, ByVal handle As Integer) As Integer
        Private Declare Function mciGetErrorStringA Lib "winmm" (ByVal id As Integer, ByVal buffer As String, ByVal length As Integer) As Integer
        Enum State : Closed = 0 : Playing = 1 : Paused = 2 : Stopped = 3 : End Enum
        Private ID As String, EB As String = Space(128), SB As String = Space(128)
#End Region
        Sub New()
            ID = Guid.NewGuid.ToString.Substring(0, 4)
        End Sub
#Region " Methods "
        Sub Open(ByVal path As String)
            Close() : Message("open """ & path & """ alias " & ID)
            Message("status " & ID & " length") : _Length = Val(SB) : _State = 3
        End Sub
        Sub Close()
            If _State <> 0 Then : _State = 0 : Message("close " & ID) : End If
        End Sub
        Sub Play()
            If _State <> 0 Then : Message("play " & ID) : _State = 1 : End If
        End Sub
        Sub [Stop]()
            If _State <> 0 Then : Message("stop " & ID) : _State = 3 : Seek(0) : End If
        End Sub
        Sub Pause()
            If _State <> 0 Then : Message("pause " & ID) : _State = 2 : End If
        End Sub
        Sub [Resume]()
            If _State <> 0 Then : Message("resume " & ID) : _State = 1 : End If
        End Sub
        Sub Seek(ByVal position As Integer)
            If _State <> 0 Then If _State = 1 Then Message("play " & ID & " from " & position) Else Message("seek " & ID & " to " & position)
        End Sub
#End Region
#Region " Properties "
        Private _Volume As Double = 100, MuteVolume As Double = 100
        Property Volume() As Double
            Get
                Return _Volume
            End Get
            Set(ByVal v As Double)
                If v < 0 Then v = 0 Else If v > 100 Then v = 100
                If _Mute Then
                    MuteVolume = v
                Else
                    If _State <> 0 Then
                        Message("setaudio " & ID & " volume to " & CInt(v * 10)) : _Volume = v
                    End If
                End If
            End Set
        End Property
        Private _Mute As Boolean
        Property Mute() As Boolean
            Get
                Return _Mute
            End Get
            Set(ByVal v As Boolean)
                If v Then
                    MuteVolume = _Volume
                    Volume = 0
                End If
                _Mute = v
                If Not v Then Volume = MuteVolume
            End Set
        End Property
        Private _Length As Integer
        ReadOnly Property Length() As Integer
            Get
                Return _Length
            End Get
        End Property
        ReadOnly Property Position() As Integer
            Get
                If _State <> 0 Then : Message("status " & ID & " position") : Return Val(SB) : Else : Return 0 : End If
            End Get
        End Property
        Private _State As State
        ReadOnly Property Status() As State
            Get
                Return _State
            End Get
        End Property
#End Region
        Private Sub Message(ByVal command As String)
            Dim I = mciSendStringA(command, SB, 128, 0)
            If I <> 0 Then : mciGetErrorStringA(I, EB, 128) : Throw (New Exception(EB.Trim)) : End If
        End Sub
    End Class
End Class
Friend NotInheritable Class Native
    Private Sub New()
    End Sub
    Friend Declare Auto Function BlockInput Lib "user32" (ByVal setting As Boolean) As Boolean
    Friend Declare Auto Function SystemParametersInfo Lib "user32" (ByVal action As Integer, ByVal parameters As Integer, ByVal other As String, ByVal ini As Integer) As Integer
    Friend Declare Auto Sub mouse_event Lib "user32" (ByVal flags As Integer, ByVal x As Integer, ByVal y As Integer, ByVal buttons As Integer, ByVal extra As IntPtr)
    Friend Declare Auto Function ShowWindow Lib "user32" (ByVal handle As Integer, ByVal appearance As Integer) As Integer
    Friend Declare Auto Function UnhookWindowsHookEx Lib "user32" (ByVal handle As Integer) As Integer
    Friend Declare Auto Function SetWindowsHookExA Lib "user32" (ByVal type As Integer, ByVal pointer As [Delegate], ByVal handle As IntPtr, ByVal thread As Integer) As Integer
    Friend Declare Auto Function CallNextHookEx Lib "user32" (ByVal handle As IntPtr, ByVal code As Integer, ByVal flags As Integer, ByVal data As Object) As Integer
    Friend Declare Auto Function GetAsyncKeyState Lib "user32" (ByVal key As Integer) As Short
    Friend Declare Auto Function RegisterHotKey Lib "user32" (ByVal handle As IntPtr, ByVal id As Integer, ByVal modifier As Integer, ByVal key As Integer) As Integer
    Friend Declare Auto Function UnregisterHotKey Lib "user32" (ByVal handle As IntPtr, ByVal id As Integer) As Integer
End Class