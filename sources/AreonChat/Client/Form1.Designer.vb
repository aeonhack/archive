<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Client = New Orcas.Winsock
        Me.Output = New System.Windows.Forms.TextBox
        Me.ULContainer = New System.Windows.Forms.Panel
        Me.UserList = New System.Windows.Forms.ListBox
        Me.CM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CMWhisper = New System.Windows.Forms.ToolStripMenuItem
        Me.CMS1 = New System.Windows.Forms.ToolStripSeparator
        Me.CMBoot = New System.Windows.Forms.ToolStripMenuItem
        Me.CMTrace = New System.Windows.Forms.ToolStripMenuItem
        Me.CMS2 = New System.Windows.Forms.ToolStripSeparator
        Me.CMQuickBan = New System.Windows.Forms.ToolStripMenuItem
        Me.CMBan = New System.Windows.Forms.ToolStripMenuItem
        Me.Input = New System.Windows.Forms.RichTextBox
        Me.TS = New System.Windows.Forms.ToolStrip
        Me.TSDisconnect = New System.Windows.Forms.ToolStripButton
        Me.TSScroll = New System.Windows.Forms.ToolStripButton
        Me.TSClear = New System.Windows.Forms.ToolStripButton
        Me.TSTimestamp = New System.Windows.Forms.ToolStripButton
        Me.TSColor = New System.Windows.Forms.ToolStripButton
        Me.TSUser = New System.Windows.Forms.ToolStripLabel
        Me.TSHide = New System.Windows.Forms.ToolStripButton
        Me.CD = New System.Windows.Forms.ColorDialog
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.AB4 = New AreonControls.AreonButton
        Me.AB3 = New AreonControls.AreonButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ATB4 = New AreonControls.AreonTextBox
        Me.ATB3 = New AreonControls.AreonTextBox
        Me.ATB2 = New AreonControls.AreonTextBox
        Me.ATB1 = New AreonControls.AreonTextBox
        Me.AB2 = New AreonControls.AreonButton
        Me.AB1 = New AreonControls.AreonButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.ULContainer.SuspendLayout()
        Me.CM.SuspendLayout()
        Me.TS.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Client
        '
        Me.Client.BufferSize = 8192
        Me.Client.LegacySupport = False
        Me.Client.LocalPort = 8080
        Me.Client.MaxPendingConnections = 1
        Me.Client.Protocol = Orcas.WinsockProtocol.Tcp
        Me.Client.RemoteHost = "localhost"
        Me.Client.RemotePort = 8080
        '
        'Output
        '
        Me.Output.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Output.BackColor = System.Drawing.Color.White
        Me.Output.Enabled = False
        Me.Output.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Output.ForeColor = System.Drawing.Color.Black
        Me.Output.Location = New System.Drawing.Point(12, 385)
        Me.Output.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Output.MaxLength = 200
        Me.Output.Name = "Output"
        Me.Output.Size = New System.Drawing.Size(568, 21)
        Me.Output.TabIndex = 7
        '
        'ULContainer
        '
        Me.ULContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ULContainer.BackColor = System.Drawing.Color.White
        Me.ULContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ULContainer.Controls.Add(Me.UserList)
        Me.ULContainer.Enabled = False
        Me.ULContainer.ForeColor = System.Drawing.Color.Black
        Me.ULContainer.Location = New System.Drawing.Point(460, 30)
        Me.ULContainer.Name = "ULContainer"
        Me.ULContainer.Size = New System.Drawing.Size(120, 349)
        Me.ULContainer.TabIndex = 10
        '
        'UserList
        '
        Me.UserList.BackColor = System.Drawing.Color.White
        Me.UserList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UserList.ContextMenuStrip = Me.CM
        Me.UserList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserList.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.UserList.ForeColor = System.Drawing.Color.Black
        Me.UserList.FormattingEnabled = True
        Me.UserList.Location = New System.Drawing.Point(0, 0)
        Me.UserList.Name = "UserList"
        Me.UserList.Size = New System.Drawing.Size(116, 338)
        Me.UserList.TabIndex = 9
        '
        'CM
        '
        Me.CM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CMWhisper, Me.CMS1, Me.CMBoot, Me.CMTrace, Me.CMS2, Me.CMQuickBan, Me.CMBan})
        Me.CM.Name = "CM"
        Me.CM.Size = New System.Drawing.Size(133, 126)
        '
        'CMWhisper
        '
        Me.CMWhisper.Name = "CMWhisper"
        Me.CMWhisper.Size = New System.Drawing.Size(132, 22)
        Me.CMWhisper.Text = "Whisper"
        '
        'CMS1
        '
        Me.CMS1.Name = "CMS1"
        Me.CMS1.Size = New System.Drawing.Size(129, 6)
        Me.CMS1.Visible = False
        '
        'CMBoot
        '
        Me.CMBoot.Name = "CMBoot"
        Me.CMBoot.Size = New System.Drawing.Size(132, 22)
        Me.CMBoot.Text = "Boot"
        Me.CMBoot.Visible = False
        '
        'CMTrace
        '
        Me.CMTrace.Name = "CMTrace"
        Me.CMTrace.Size = New System.Drawing.Size(132, 22)
        Me.CMTrace.Text = "Trace"
        Me.CMTrace.Visible = False
        '
        'CMS2
        '
        Me.CMS2.Name = "CMS2"
        Me.CMS2.Size = New System.Drawing.Size(129, 6)
        Me.CMS2.Visible = False
        '
        'CMQuickBan
        '
        Me.CMQuickBan.Name = "CMQuickBan"
        Me.CMQuickBan.Size = New System.Drawing.Size(132, 22)
        Me.CMQuickBan.Text = "Quick Ban"
        Me.CMQuickBan.Visible = False
        '
        'CMBan
        '
        Me.CMBan.Name = "CMBan"
        Me.CMBan.Size = New System.Drawing.Size(132, 22)
        Me.CMBan.Text = "Ban..."
        Me.CMBan.Visible = False
        '
        'Input
        '
        Me.Input.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Input.BackColor = System.Drawing.Color.White
        Me.Input.Enabled = False
        Me.Input.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Input.ForeColor = System.Drawing.Color.Black
        Me.Input.Location = New System.Drawing.Point(12, 30)
        Me.Input.Name = "Input"
        Me.Input.ReadOnly = True
        Me.Input.Size = New System.Drawing.Size(442, 349)
        Me.Input.TabIndex = 8
        Me.Input.Text = ""
        '
        'TS
        '
        Me.TS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSDisconnect, Me.TSScroll, Me.TSClear, Me.TSTimestamp, Me.TSColor, Me.TSUser, Me.TSHide})
        Me.TS.Location = New System.Drawing.Point(0, 0)
        Me.TS.Name = "TS"
        Me.TS.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.TS.Size = New System.Drawing.Size(592, 25)
        Me.TS.TabIndex = 11
        Me.TS.Visible = False
        '
        'TSDisconnect
        '
        Me.TSDisconnect.AutoToolTip = False
        Me.TSDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSDisconnect.ForeColor = System.Drawing.Color.Black
        Me.TSDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSDisconnect.Name = "TSDisconnect"
        Me.TSDisconnect.Size = New System.Drawing.Size(63, 22)
        Me.TSDisconnect.Text = "Disconnect"
        '
        'TSScroll
        '
        Me.TSScroll.AutoToolTip = False
        Me.TSScroll.Checked = True
        Me.TSScroll.CheckOnClick = True
        Me.TSScroll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSScroll.ForeColor = System.Drawing.Color.Black
        Me.TSScroll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSScroll.Name = "TSScroll"
        Me.TSScroll.Size = New System.Drawing.Size(36, 22)
        Me.TSScroll.Text = "Scroll"
        '
        'TSClear
        '
        Me.TSClear.AutoToolTip = False
        Me.TSClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSClear.ForeColor = System.Drawing.Color.Black
        Me.TSClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSClear.Name = "TSClear"
        Me.TSClear.Size = New System.Drawing.Size(36, 22)
        Me.TSClear.Text = "Clear"
        '
        'TSTimestamp
        '
        Me.TSTimestamp.AutoToolTip = False
        Me.TSTimestamp.CheckOnClick = True
        Me.TSTimestamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSTimestamp.ForeColor = System.Drawing.Color.Black
        Me.TSTimestamp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSTimestamp.Name = "TSTimestamp"
        Me.TSTimestamp.Size = New System.Drawing.Size(62, 22)
        Me.TSTimestamp.Text = "Timestamp"
        '
        'TSColor
        '
        Me.TSColor.AutoToolTip = False
        Me.TSColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSColor.ForeColor = System.Drawing.Color.Black
        Me.TSColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSColor.Name = "TSColor"
        Me.TSColor.Size = New System.Drawing.Size(36, 22)
        Me.TSColor.Text = "Color"
        Me.TSColor.Visible = False
        '
        'TSUser
        '
        Me.TSUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSUser.ForeColor = System.Drawing.Color.DarkGray
        Me.TSUser.Name = "TSUser"
        Me.TSUser.Size = New System.Drawing.Size(24, 22)
        Me.TSUser.Text = "Null"
        '
        'TSHide
        '
        Me.TSHide.AutoToolTip = False
        Me.TSHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSHide.ForeColor = System.Drawing.Color.Black
        Me.TSHide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSHide.Name = "TSHide"
        Me.TSHide.Size = New System.Drawing.Size(32, 22)
        Me.TSHide.Text = "Hide"
        Me.TSHide.Visible = False
        '
        'CD
        '
        Me.CD.AnyColor = True
        Me.CD.SolidColorOnly = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel2.Controls.Add(Me.AB4)
        Me.Panel2.Controls.Add(Me.AB3)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.ATB4)
        Me.Panel2.Controls.Add(Me.ATB3)
        Me.Panel2.Controls.Add(Me.ATB2)
        Me.Panel2.Controls.Add(Me.ATB1)
        Me.Panel2.Controls.Add(Me.AB2)
        Me.Panel2.Controls.Add(Me.AB1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(592, 416)
        Me.Panel2.TabIndex = 4
        '
        'AB4
        '
        Me.AB4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AB4.BackColor = System.Drawing.Color.Black
        Me.AB4.BackgroundImage = Nothing
        Me.AB4.BackgroundImageLayout = Nothing
        Me.AB4.DownImage = CType(resources.GetObject("AB4.DownImage"), System.Drawing.Image)
        Me.AB4.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.AB4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.AB4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.AB4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AB4.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AB4.ForeColor = System.Drawing.Color.White
        Me.AB4.Location = New System.Drawing.Point(485, 367)
        Me.AB4.Name = "AB4"
        Me.AB4.OverImage = CType(resources.GetObject("AB4.OverImage"), System.Drawing.Image)
        Me.AB4.Size = New System.Drawing.Size(80, 26)
        Me.AB4.TabIndex = 7
        Me.AB4.Text = "Settings..."
        Me.AB4.UpImage = CType(resources.GetObject("AB4.UpImage"), System.Drawing.Image)
        Me.AB4.UseVisualStyleBackColor = False
        '
        'AB3
        '
        Me.AB3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AB3.BackColor = System.Drawing.Color.Black
        Me.AB3.BackgroundImage = Nothing
        Me.AB3.BackgroundImageLayout = Nothing
        Me.AB3.DownImage = CType(resources.GetObject("AB3.DownImage"), System.Drawing.Image)
        Me.AB3.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.AB3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.AB3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.AB3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AB3.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AB3.ForeColor = System.Drawing.Color.White
        Me.AB3.Location = New System.Drawing.Point(399, 367)
        Me.AB3.Name = "AB3"
        Me.AB3.OverImage = CType(resources.GetObject("AB3.OverImage"), System.Drawing.Image)
        Me.AB3.Size = New System.Drawing.Size(80, 26)
        Me.AB3.TabIndex = 6
        Me.AB3.Text = "Connect"
        Me.AB3.UpImage = CType(resources.GetObject("AB3.UpImage"), System.Drawing.Image)
        Me.AB3.UseVisualStyleBackColor = False
        Me.AB3.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(156, 223)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 14)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "E-Mail"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(155, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Password"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(155, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Password"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(153, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Username"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Visible = False
        '
        'ATB4
        '
        Me.ATB4.Allowed = Nothing
        Me.ATB4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ATB4.DefaultColor = System.Drawing.Color.Gray
        Me.ATB4.DefaultText = ""
        Me.ATB4.DisallowInput = " `|"
        Me.ATB4.DisallowLetters = False
        Me.ATB4.DisallowNumbers = False
        Me.ATB4.DisallowPunctuation = False
        Me.ATB4.DisallowSymbols = False
        Me.ATB4.Location = New System.Drawing.Point(222, 142)
        Me.ATB4.Name = "ATB4"
        Me.ATB4.Size = New System.Drawing.Size(166, 20)
        Me.ATB4.TabIndex = 0
        Me.ATB4.Visible = False
        '
        'ATB3
        '
        Me.ATB3.Allowed = Nothing
        Me.ATB3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ATB3.DefaultColor = System.Drawing.Color.Gray
        Me.ATB3.DefaultText = ""
        Me.ATB3.DisallowInput = " `|"
        Me.ATB3.DisallowLetters = False
        Me.ATB3.DisallowNumbers = False
        Me.ATB3.DisallowPunctuation = False
        Me.ATB3.DisallowSymbols = False
        Me.ATB3.Location = New System.Drawing.Point(222, 168)
        Me.ATB3.Name = "ATB3"
        Me.ATB3.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ATB3.Size = New System.Drawing.Size(166, 20)
        Me.ATB3.TabIndex = 1
        Me.ATB3.Visible = False
        '
        'ATB2
        '
        Me.ATB2.Allowed = Nothing
        Me.ATB2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ATB2.DefaultColor = System.Drawing.Color.Gray
        Me.ATB2.DefaultText = ""
        Me.ATB2.DisallowInput = " `|"
        Me.ATB2.DisallowLetters = False
        Me.ATB2.DisallowNumbers = False
        Me.ATB2.DisallowPunctuation = False
        Me.ATB2.DisallowSymbols = False
        Me.ATB2.Location = New System.Drawing.Point(222, 194)
        Me.ATB2.Name = "ATB2"
        Me.ATB2.Size = New System.Drawing.Size(166, 20)
        Me.ATB2.TabIndex = 2
        Me.ATB2.Visible = False
        '
        'ATB1
        '
        Me.ATB1.Allowed = Nothing
        Me.ATB1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ATB1.DefaultColor = System.Drawing.Color.Gray
        Me.ATB1.DefaultText = ""
        Me.ATB1.DisallowInput = Nothing
        Me.ATB1.DisallowLetters = False
        Me.ATB1.DisallowNumbers = False
        Me.ATB1.DisallowPunctuation = False
        Me.ATB1.DisallowSymbols = False
        Me.ATB1.Location = New System.Drawing.Point(222, 220)
        Me.ATB1.Name = "ATB1"
        Me.ATB1.Size = New System.Drawing.Size(166, 20)
        Me.ATB1.TabIndex = 3
        Me.ATB1.Visible = False
        '
        'AB2
        '
        Me.AB2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AB2.BackColor = System.Drawing.Color.Black
        Me.AB2.BackgroundImage = Nothing
        Me.AB2.BackgroundImageLayout = Nothing
        Me.AB2.DownImage = CType(resources.GetObject("AB2.DownImage"), System.Drawing.Image)
        Me.AB2.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.AB2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.AB2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.AB2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AB2.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AB2.ForeColor = System.Drawing.Color.White
        Me.AB2.Location = New System.Drawing.Point(308, 260)
        Me.AB2.Name = "AB2"
        Me.AB2.OverImage = CType(resources.GetObject("AB2.OverImage"), System.Drawing.Image)
        Me.AB2.Size = New System.Drawing.Size(80, 26)
        Me.AB2.TabIndex = 5
        Me.AB2.Text = "Register"
        Me.AB2.UpImage = CType(resources.GetObject("AB2.UpImage"), System.Drawing.Image)
        Me.AB2.UseVisualStyleBackColor = False
        Me.AB2.Visible = False
        '
        'AB1
        '
        Me.AB1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AB1.BackColor = System.Drawing.Color.Black
        Me.AB1.BackgroundImage = Nothing
        Me.AB1.BackgroundImageLayout = Nothing
        Me.AB1.DownImage = CType(resources.GetObject("AB1.DownImage"), System.Drawing.Image)
        Me.AB1.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.AB1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.AB1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.AB1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AB1.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AB1.ForeColor = System.Drawing.Color.White
        Me.AB1.Location = New System.Drawing.Point(222, 260)
        Me.AB1.Name = "AB1"
        Me.AB1.OverImage = CType(resources.GetObject("AB1.OverImage"), System.Drawing.Image)
        Me.AB1.Size = New System.Drawing.Size(80, 26)
        Me.AB1.TabIndex = 4
        Me.AB1.Text = "Login"
        Me.AB1.UpImage = CType(resources.GetObject("AB1.UpImage"), System.Drawing.Image)
        Me.AB1.UseVisualStyleBackColor = False
        Me.AB1.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Eras Demi ITC", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(22, 369)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 24)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Intializing..."
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(592, 416)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Input)
        Me.Controls.Add(Me.ULContainer)
        Me.Controls.Add(Me.Output)
        Me.Controls.Add(Me.TS)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(380, 240)
        Me.Name = "Form1"
        Me.Text = "Areon Chat"
        Me.ULContainer.ResumeLayout(False)
        Me.CM.ResumeLayout(False)
        Me.TS.ResumeLayout(False)
        Me.TS.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Client As Orcas.Winsock
    Friend WithEvents Output As System.Windows.Forms.TextBox
    Friend WithEvents ULContainer As System.Windows.Forms.Panel
    Friend WithEvents UserList As System.Windows.Forms.ListBox
    Friend WithEvents Input As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AB1 As AreonControls.AreonButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ATB4 As AreonControls.AreonTextBox
    Friend WithEvents ATB3 As AreonControls.AreonTextBox
    Friend WithEvents ATB2 As AreonControls.AreonTextBox
    Friend WithEvents ATB1 As AreonControls.AreonTextBox
    Friend WithEvents AB2 As AreonControls.AreonButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AB3 As AreonControls.AreonButton
    Friend WithEvents CM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CMWhisper As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMS1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMQuickBan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMBan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMBoot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TS As System.Windows.Forms.ToolStrip
    Friend WithEvents TSDisconnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSScroll As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents AB4 As AreonControls.AreonButton
    Friend WithEvents TSTimestamp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CD As System.Windows.Forms.ColorDialog
    Friend WithEvents TSColor As System.Windows.Forms.ToolStripButton
    Friend WithEvents CMS2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMTrace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSUser As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TSHide As System.Windows.Forms.ToolStripButton

End Class
