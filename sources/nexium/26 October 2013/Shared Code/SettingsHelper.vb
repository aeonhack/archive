Imports Microsoft.Win32
Imports System.Collections.Specialized
Imports System.Xml
Imports System.IO

Public NotInheritable Class SettingsHelper

    Private Const RegistryBase As String = "Software\AppSettings\"

    Private _ApplicationName As String = "Default"
    Public ReadOnly Property ApplicationName() As String
        Get
            Return _ApplicationName
        End Get
    End Property

    Private _FileSettings As New NameValueCollection()
    Public ReadOnly Property FileSettings() As NameValueCollection
        Get
            Return _FileSettings
        End Get
    End Property

    Public Property FileSettings(name As String) As String
        Get
            If _FileSettings(name) Is Nothing Then
                Return String.Empty
            Else
                Return _FileSettings(name)
            End If
        End Get
        Set(ByVal value As String)
            _FileSettings(name) = value
        End Set
    End Property


    Sub New(applicationName As String)
        _ApplicationName = applicationName
    End Sub


    Public Sub WriteRegistrySetting(name As String, value As String)
        Registry.CurrentUser.CreateSubKey(RegistryBase & ApplicationName).SetValue(name, value)
    End Sub

    Public Function ReadRegistrySetting(name As String) As String
        Return CStr(Registry.CurrentUser.CreateSubKey(RegistryBase & ApplicationName).GetValue(name))
    End Function

    Public Sub RemoveRegistrySetting(name As String)
        Registry.CurrentUser.CreateSubKey(RegistryBase & ApplicationName).DeleteValue(name)
    End Sub


    Public Sub LoadFileSettings(fileName As String)
        _FileSettings.Clear()
        If Not File.Exists(fileName) Then Return

        Dim XML As New XmlDocument()
        XML.Load(fileName)

        For Each N As XmlNode In XML.SelectSingleNode("Settings").ChildNodes
            _FileSettings(N.Name) = N.InnerText
        Next
    End Sub

    Public Sub SaveFileSettings(fileName As String)
        Dim Settings As New XmlWriterSettings()
        Settings.NewLineOnAttributes = True
        Settings.Indent = True

        Dim Writer As XmlWriter = XmlWriter.Create(fileName, Settings)

        Writer.WriteStartDocument()
        Writer.WriteRaw(Environment.NewLine)
        Writer.WriteRaw(Environment.NewLine)
        Writer.WriteStartElement("Settings")

        For Each K As String In _FileSettings.AllKeys
            If _FileSettings(K) Is Nothing Then Continue For

            Writer.WriteStartElement(K)
            Writer.WriteValue(_FileSettings(K))
            Writer.WriteEndElement()
        Next

        Writer.WriteEndElement()
        Writer.Close()
    End Sub

    Public Sub ClearFileSettings()
        _FileSettings.Clear()
    End Sub

End Class
