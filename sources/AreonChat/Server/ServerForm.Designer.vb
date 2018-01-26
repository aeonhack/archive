<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerForm))
        Me.Server = New Orcas.Winsock
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
        Me.CMBanIP = New System.Windows.Forms.ToolStripMenuItem
        Me.CMS3 = New System.Windows.Forms.ToolStripSeparator
        Me.CMPromote = New System.Windows.Forms.ToolStripMenuItem
        Me.Input = New System.Windows.Forms.RichTextBox
        Me.NI = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Uptime = New System.Windows.Forms.Timer(Me.components)
        Me.SS = New System.Windows.Forms.StatusStrip
        Me.SSStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.SSS1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.SSUptime = New System.Windows.Forms.ToolStripStatusLabel
        Me.SSS2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.SSUsers = New System.Windows.Forms.ToolStripStatusLabel
        Me.TS = New System.Windows.Forms.ToolStrip
        Me.TSConnect = New System.Windows.Forms.ToolStripButton
        Me.TSClear = New System.Windows.Forms.ToolStripButton
        Me.TSColor = New System.Windows.Forms.ToolStripButton
        Me.TSScroll = New System.Windows.Forms.ToolStripButton
        Me.TSRecord = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.TSPort = New System.Windows.Forms.ToolStripTextBox
        Me.TSExternal = New System.Windows.Forms.ToolStripLabel
        Me.CD = New System.Windows.Forms.ColorDialog
        Me.ULContainer.SuspendLayout()
        Me.CM.SuspendLayout()
        Me.SS.SuspendLayout()
        Me.TS.SuspendLayout()
        Me.SuspendLayout()
        '
        'Server
        '
        Me.Server.BufferSize = 8192
        Me.Server.LegacySupport = False
        Me.Server.LocalPort = 4304
        Me.Server.MaxPendingConnections = 1
        Me.Server.Protocol = Orcas.WinsockProtocol.Tcp
        Me.Server.RemoteHost = "localhost"
        Me.Server.RemotePort = 4304
        '
        'Output
        '
        Me.Output.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Output.BackColor = System.Drawing.Color.White
        Me.Output.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Output.ForeColor = System.Drawing.Color.Black
        Me.Output.Location = New System.Drawing.Point(12, 321)
        Me.Output.Name = "Output"
        Me.Output.Size = New System.Drawing.Size(498, 21)
        Me.Output.TabIndex = 0
        '
        'ULContainer
        '
        Me.ULContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ULContainer.BackColor = System.Drawing.Color.White
        Me.ULContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ULContainer.Controls.Add(Me.UserList)
        Me.ULContainer.Location = New System.Drawing.Point(393, 28)
        Me.ULContainer.Name = "ULContainer"
        Me.ULContainer.Size = New System.Drawing.Size(117, 287)
        Me.ULContainer.TabIndex = 1
        '
        'UserList
        '
        Me.UserList.BackColor = System.Drawing.Color.White
        Me.UserList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UserList.ContextMenuStrip = Me.CM
        Me.UserList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserList.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserList.ForeColor = System.Drawing.Color.Black
        Me.UserList.FormattingEnabled = True
        Me.UserList.Location = New System.Drawing.Point(0, 0)
        Me.UserList.Name = "UserList"
        Me.UserList.Size = New System.Drawing.Size(113, 273)
        Me.UserList.TabIndex = 0
        '
        'CM
        '
        Me.CM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CMWhisper, Me.CMS1, Me.CMBoot, Me.CMTrace, Me.CMS2, Me.CMQuickBan, Me.CMBan, Me.CMBanIP, Me.CMS3, Me.CMPromote})
        Me.CM.Name = "ContextMenuStrip1"
        Me.CM.Size = New System.Drawing.Size(133, 176)
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
        '
        'CMBoot
        '
        Me.CMBoot.Name = "CMBoot"
        Me.CMBoot.Size = New System.Drawing.Size(132, 22)
        Me.CMBoot.Text = "Boot"
        '
        'CMTrace
        '
        Me.CMTrace.Name = "CMTrace"
        Me.CMTrace.Size = New System.Drawing.Size(132, 22)
        Me.CMTrace.Text = "Trace"
        '
        'CMS2
        '
        Me.CMS2.Name = "CMS2"
        Me.CMS2.Size = New System.Drawing.Size(129, 6)
        '
        'CMQuickBan
        '
        Me.CMQuickBan.Name = "CMQuickBan"
        Me.CMQuickBan.Size = New System.Drawing.Size(132, 22)
        Me.CMQuickBan.Text = "Quick Ban"
        '
        'CMBan
        '
        Me.CMBan.Name = "CMBan"
        Me.CMBan.Size = New System.Drawing.Size(132, 22)
        Me.CMBan.Text = "Ban..."
        '
        'CMBanIP
        '
        Me.CMBanIP.Name = "CMBanIP"
        Me.CMBanIP.Size = New System.Drawing.Size(132, 22)
        Me.CMBanIP.Text = "Ban IP..."
        '
        'CMS3
        '
        Me.CMS3.Name = "CMS3"
        Me.CMS3.Size = New System.Drawing.Size(129, 6)
        '
        'CMPromote
        '
        Me.CMPromote.Name = "CMPromote"
        Me.CMPromote.Size = New System.Drawing.Size(132, 22)
        Me.CMPromote.Text = "Promote"
        '
        'Input
        '
        Me.Input.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Input.BackColor = System.Drawing.Color.White
        Me.Input.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Input.ForeColor = System.Drawing.Color.Black
        Me.Input.Location = New System.Drawing.Point(12, 28)
        Me.Input.Name = "Input"
        Me.Input.ReadOnly = True
        Me.Input.Size = New System.Drawing.Size(375, 287)
        Me.Input.TabIndex = 2
        Me.Input.Text = ""
        '
        'NI
        '
        Me.NI.Icon = CType(resources.GetObject("NI.Icon"), System.Drawing.Icon)
        Me.NI.Visible = True
        '
        'Uptime
        '
        Me.Uptime.Interval = 1000
        '
        'SS
        '
        Me.SS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SSStatus, Me.SSS1, Me.SSUptime, Me.SSS2, Me.SSUsers})
        Me.SS.Location = New System.Drawing.Point(0, 346)
        Me.SS.Name = "SS"
        Me.SS.Size = New System.Drawing.Size(522, 22)
        Me.SS.TabIndex = 3
        '
        'SSStatus
        '
        Me.SSStatus.ForeColor = System.Drawing.Color.DarkGray
        Me.SSStatus.Name = "SSStatus"
        Me.SSStatus.Size = New System.Drawing.Size(77, 17)
        Me.SSStatus.Text = "Status: Closed"
        '
        'SSS1
        '
        Me.SSS1.Name = "SSS1"
        Me.SSS1.Size = New System.Drawing.Size(146, 17)
        Me.SSS1.Spring = True
        Me.SSS1.Text = " "
        '
        'SSUptime
        '
        Me.SSUptime.ForeColor = System.Drawing.Color.DarkGray
        Me.SSUptime.Name = "SSUptime"
        Me.SSUptime.Size = New System.Drawing.Size(91, 17)
        Me.SSUptime.Text = "Uptime: 00:00:00"
        '
        'SSS2
        '
        Me.SSS2.Name = "SSS2"
        Me.SSS2.Size = New System.Drawing.Size(146, 17)
        Me.SSS2.Spring = True
        Me.SSS2.Text = " "
        '
        'SSUsers
        '
        Me.SSUsers.Name = "SSUsers"
        Me.SSUsers.Size = New System.Drawing.Size(47, 17)
        Me.SSUsers.Text = "Users: 0"
        '
        'TS
        '
        Me.TS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSConnect, Me.TSClear, Me.TSColor, Me.TSScroll, Me.TSRecord, Me.ToolStripSeparator1, Me.TSPort, Me.TSExternal})
        Me.TS.Location = New System.Drawing.Point(0, 0)
        Me.TS.Name = "TS"
        Me.TS.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.TS.Size = New System.Drawing.Size(522, 25)
        Me.TS.TabIndex = 4
        '
        'TSConnect
        '
        Me.TSConnect.AutoToolTip = False
        Me.TSConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSConnect.Name = "TSConnect"
        Me.TSConnect.Size = New System.Drawing.Size(51, 22)
        Me.TSConnect.Text = "Connect"
        '
        'TSClear
        '
        Me.TSClear.AutoToolTip = False
        Me.TSClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSClear.Name = "TSClear"
        Me.TSClear.Size = New System.Drawing.Size(36, 22)
        Me.TSClear.Text = "Clear"
        '
        'TSColor
        '
        Me.TSColor.AutoToolTip = False
        Me.TSColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSColor.Name = "TSColor"
        Me.TSColor.Size = New System.Drawing.Size(36, 22)
        Me.TSColor.Text = "Color"
        '
        'TSScroll
        '
        Me.TSScroll.AutoToolTip = False
        Me.TSScroll.Checked = True
        Me.TSScroll.CheckOnClick = True
        Me.TSScroll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSScroll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSScroll.Name = "TSScroll"
        Me.TSScroll.Size = New System.Drawing.Size(36, 22)
        Me.TSScroll.Text = "Scroll"
        '
        'TSRecord
        '
        Me.TSRecord.AutoToolTip = False
        Me.TSRecord.CheckOnClick = True
        Me.TSRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSRecord.Name = "TSRecord"
        Me.TSRecord.Size = New System.Drawing.Size(45, 22)
        Me.TSRecord.Text = "Record"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TSPort
        '
        Me.TSPort.BackColor = System.Drawing.Color.White
        Me.TSPort.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TSPort.Name = "TSPort"
        Me.TSPort.Size = New System.Drawing.Size(40, 25)
        Me.TSPort.Text = "4304"
        Me.TSPort.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TSPort.ToolTipText = "Open port to listen on."
        '
        'TSExternal
        '
        Me.TSExternal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSExternal.ForeColor = System.Drawing.Color.DarkGray
        Me.TSExternal.Name = "TSExternal"
        Me.TSExternal.Size = New System.Drawing.Size(56, 22)
        Me.TSExternal.Text = "Loading..."
        '
        'CD
        '
        Me.CD.AnyColor = True
        Me.CD.SolidColorOnly = True
        '
        'ServerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(522, 368)
        Me.Controls.Add(Me.Input)
        Me.Controls.Add(Me.ULContainer)
        Me.Controls.Add(Me.SS)
        Me.Controls.Add(Me.TS)
        Me.Controls.Add(Me.Output)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(380, 240)
        Me.Name = "ServerForm"
        Me.Text = "Areon Server"
        Me.ULContainer.ResumeLayout(False)
        Me.CM.ResumeLayout(False)
        Me.SS.ResumeLayout(False)
        Me.SS.PerformLayout()
        Me.TS.ResumeLayout(False)
        Me.TS.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Server As Orcas.Winsock
    Friend WithEvents Output As System.Windows.Forms.TextBox
    Friend WithEvents ULContainer As System.Windows.Forms.Panel
    Friend WithEvents UserList As System.Windows.Forms.ListBox
    Friend WithEvents Input As System.Windows.Forms.RichTextBox
    Friend WithEvents NI As System.Windows.Forms.NotifyIcon
    Friend WithEvents CM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CMQuickBan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMBoot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMPromote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMWhisper As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMS1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMS3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMBan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Uptime As System.Windows.Forms.Timer
    Friend WithEvents SS As System.Windows.Forms.StatusStrip
    Friend WithEvents SSStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SSS1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SSUptime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SSS2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SSUsers As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TS As System.Windows.Forms.ToolStrip
    Friend WithEvents TSScroll As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSPort As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents TSExternal As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TSColor As System.Windows.Forms.ToolStripButton
    Friend WithEvents CD As System.Windows.Forms.ColorDialog
    Friend WithEvents CMS2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMBanIP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMTrace As System.Windows.Forms.ToolStripMenuItem
End Class
