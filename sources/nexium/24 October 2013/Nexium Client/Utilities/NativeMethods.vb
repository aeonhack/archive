Imports System.Runtime.InteropServices

Public Class NativeMethods

    <DllImport("user32.dll", EntryPoint:="FlashWindow")> _
    Public Shared Function FlashWindow( _
    ByVal handle As IntPtr, _
    ByVal invert As Boolean) As Boolean
    End Function

    <DllImport("user32.dll", EntryPoint:="GetForegroundWindow")> _
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function

End Class
