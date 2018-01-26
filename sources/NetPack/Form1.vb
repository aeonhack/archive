Imports System.Reflection
Public Class Form1

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim C As String() = Environment.GetCommandLineArgs

        If C.Length > 1 Then
            Try
                'Make sure we have a location and destination
                If C.Length < 3 Then End

                TextBox1.Text = C(1) 'Location
                TextBox2.Text = C(2) 'Destination

                'Make sure the file is a .net application
                If Not Managed(TextBox1.Text) Then End

                'Check if an icon has been specified
                If C.Length > 3 Then TextBox17.Text = C(3) 'Icon 

                Button3.PerformClick()
            Catch
            End Try
            End
        End If

        WindowState = FormWindowState.Normal
        ShowInTaskbar = True
    End Sub

#Region " Don't Bother "
    Function Managed(ByVal path As String) As Boolean

        Using T As New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
            T.Seek(60, IO.SeekOrigin.Begin)
            Managed = T.ReadByte = 128
            T.Close()
            If Not Managed Then Return Managed
        End Using

        Try
            LoadAttributes(Assembly.LoadFile(path))
        Catch
            Return False
        End Try
    End Function
    Sub LoadAttributes(ByVal assembly As Assembly)
        Dim P As New Properties(assembly)
        TextBox3.Text = P.Title
        TextBox4.Text = P.Description
        TextBox5.Text = P.Company
        TextBox6.Text = P.Product
        TextBox7.Text = P.Copyright
        TextBox8.Text = P.Trademark
        TextBox9.Text = P.Version.Major.ToString
        TextBox10.Text = P.Version.Minor.ToString
        TextBox11.Text = P.Version.Build.ToString
        TextBox12.Text = P.Version.Revision.ToString
        TextBox13.Text = P.FileVersion.Major.ToString
        TextBox14.Text = P.FileVersion.Minor.ToString
        TextBox15.Text = P.FileVersion.Build.ToString
        TextBox16.Text = P.FileVersion.Revision.ToString
    End Sub
    Function GetAttributes() As String
        Dim P As New Properties()
        P.Title = TextBox3.Text
        P.Description = TextBox4.Text
        P.Company = TextBox5.Text
        P.Product = TextBox6.Text
        P.Copyright = TextBox7.Text
        P.Trademark = TextBox8.Text
        P.Version = New Version(CInt(TextBox9.Text), CInt(TextBox10.Text), CInt(TextBox11.Text), CInt(TextBox12.Text))
        P.FileVersion = New Version(CInt(TextBox13.Text), CInt(TextBox14.Text), CInt(TextBox15.Text), CInt(TextBox16.Text))
        Return P.ToString
    End Function
    Structure Properties
        Dim Title, Description, Company, Product, Copyright, Trademark As String, Version, FileVersion As Version
        Private H As Assembly
        Sub New(ByVal assembly As Assembly)
            H = assembly
            Title = Search(Of AssemblyTitleAttribute).Title
            Description = Search(Of AssemblyDescriptionAttribute).Description
            Company = Search(Of AssemblyCompanyAttribute).Company
            Product = Search(Of AssemblyProductAttribute).Product
            Copyright = Search(Of AssemblyCopyrightAttribute).Copyright
            Trademark = Search(Of AssemblyTrademarkAttribute).Trademark
            Version = New Version(assembly.GetName.Version.ToString)
            Dim T As AssemblyFileVersionAttribute = Search(Of AssemblyFileVersionAttribute)()
            FileVersion = New Version(If(T Is Nothing, "0.0.0.0", T.Version))
        End Sub
        Private Function Search(Of T)() As T
            Try
                Return DirectCast(H.GetCustomAttributes(GetType(T), False)(0), T)
            Catch
                Return Nothing
            End Try
        End Function
        Public Overrides Function ToString() As String
            Dim S As New Text.StringBuilder, B As String = "<Assembly: Assembly", E As String = """)>"
            If Title <> "" Then S.AppendLine(B & "Title(""" & Title & E)
            If Description <> "" Then S.AppendLine(B & "Description(""" & Description & E)
            If Company <> "" Then S.AppendLine(B & "Company(""" & Company & E)
            If Product <> "" Then S.AppendLine(B & "Product(""" & Product & E)
            If Copyright <> "" Then S.AppendLine(B & "Copyright(""" & Copyright & E)
            If Trademark <> "" Then S.AppendLine(B & "Trademark(""" & Trademark & E)
            If Version.ToString <> "0.0.0.0" Then S.AppendLine(B & "Version(""" & Version.ToString & E)
            If FileVersion.ToString <> "0.0.0.0" Then S.AppendLine(B & "FileVersion(""" & FileVersion.ToString & E)
            Return S.ToString
        End Function
    End Structure
#End Region

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged
        Button3.Enabled = TextBox1.Text <> "" And TextBox2.Text <> ""
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Using T As New OpenFileDialog
            T.Filter = ".Net Executable|*.exe"
            If T.ShowDialog = Windows.Forms.DialogResult.OK Then
                If Managed(T.FileName) Then
                    TextBox1.Text = T.FileName
                Else
                    MessageBox.Show("Please select a .Net executable.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End Using
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Using T As New SaveFileDialog
            T.Filter = ".Net Executable|*.exe"
            If T.ShowDialog = Windows.Forms.DialogResult.OK Then TextBox2.Text = T.FileName
        End Using
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Using T As New OpenFileDialog
            T.Filter = "Icon|*.ico;*.icon"
            If T.ShowDialog = Windows.Forms.DialogResult.OK Then TextBox17.Text = T.FileName
        End Using
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim X As String = Application.StartupPath & "\M.resources"
        Dim I As String = Environment.SystemDirectory(0) & ":\M" & IO.Path.GetExtension(TextBox17.Text)
        Dim A As Byte() = IO.File.ReadAllBytes(TextBox1.Text)

        Using R As New Resources.ResourceWriter(X)
            R.AddResource("H", My.Resources.Archiver)
            R.AddResource("A", Assembly.Load(My.Resources.Archiver).GetType("Archive.Helper").GetMethod("Compress").Invoke(Nothing, New Object() {A}))
            R.Generate()
        End Using

        Dim C As New CodeDom.Compiler.CompilerParameters
        With C
            .WarningLevel = 0
            .EmbeddedResources.Add(X)
            .GenerateExecutable = True
            .GenerateInMemory = False
            .IncludeDebugInformation = False
            .OutputAssembly = TextBox2.Text
            .MainClass = "X"
            .CompilerOptions = "/optimize /removeintchecks /t:winexe" ' /noconfig /nostdlib  | /define:DEBUG;TRACE;WindowsCE
            If TextBox17.Text <> "" Then
                IO.File.Copy(TextBox17.Text, I)
                .CompilerOptions &= " /win32icon:" & I
            End If
            .ReferencedAssemblies.Add("System.dll")
        End With

        Dim D As New Dictionary(Of String, String)
        D.Add("CompilerVersion", "v2.0")
        Using T As New VBCodeProvider(D)
            Dim Code As String = My.Resources.Data, Data As String = GetAttributes()
            Data &= "<Assembly: Runtime.CompilerServices.SuppressIldasm()>" & Environment.NewLine
            Code = Code.Replace("Module X", Data & "Module X")
            T.CompileAssemblyFromSource(C, Code)
        End Using

        IO.File.Delete(X)
        IO.File.Delete(I)
        For Each V As Control In Controls
            If TypeOf V Is TextBox Then DirectCast(V, TextBox).Clear()
        Next
    End Sub
End Class
