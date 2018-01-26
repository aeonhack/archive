<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.FSW1 = New System.IO.FileSystemWatcher
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TS1 = New System.Windows.Forms.ToolStrip
        Me.SS1 = New System.Windows.Forms.StatusStrip
        Me.CMS1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RenameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TSL1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.WordWrapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStripToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ListFontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DocumentFontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.FSW1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TS1.SuspendLayout()
        Me.SS1.SuspendLayout()
        Me.CMS1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FSW1
        '
        Me.FSW1.EnableRaisingEvents = True
        Me.FSW1.Filter = "*.txt"
        Me.FSW1.IncludeSubdirectories = True
        Me.FSW1.NotifyFilter = System.IO.NotifyFilters.FileName
        Me.FSW1.SynchronizingObject = Me
        '
        'TabControl1
        '
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(706, 438)
        Me.TabControl1.TabIndex = 5
        '
        'TS1
        '
        Me.TS1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripDropDownButton1})
        Me.TS1.Location = New System.Drawing.Point(0, 0)
        Me.TS1.Name = "TS1"
        Me.TS1.Size = New System.Drawing.Size(706, 25)
        Me.TS1.TabIndex = 6
        Me.TS1.Text = "ToolStrip1"
        '
        'SS1
        '
        Me.SS1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSL1})
        Me.SS1.Location = New System.Drawing.Point(0, 463)
        Me.SS1.Name = "SS1"
        Me.SS1.Size = New System.Drawing.Size(706, 22)
        Me.SS1.TabIndex = 3
        '
        'CMS1
        '
        Me.CMS1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.RenameToolStripMenuItem, Me.CreateToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.CMS1.Name = "CMS1"
        Me.CMS1.Size = New System.Drawing.Size(153, 114)
        '
        'CreateToolStripMenuItem
        '
        Me.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem"
        Me.CreateToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.CreateToolStripMenuItem.Text = "Create"
        '
        'RenameToolStripMenuItem
        '
        Me.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        Me.RenameToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.RenameToolStripMenuItem.Text = "Rename"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'TSL1
        '
        Me.TSL1.Image = Global.My.Resources.Resources.Monitor_2_48
        Me.TSL1.Name = "TSL1"
        Me.TSL1.Size = New System.Drawing.Size(179, 16)
        Me.TSL1.Text = "  0 Documents     0 Categories"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportToolStripMenuItem, Me.ToolStripSeparator1, Me.AboutToolStripMenuItem})
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.ShowDropDownArrow = False
        Me.ToolStripButton1.Size = New System.Drawing.Size(42, 22)
        Me.ToolStripButton1.Text = "Menu"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ImportToolStripMenuItem.Text = "Import.."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WordWrapToolStripMenuItem, Me.StatusStripToolStripMenuItem, Me.ToolStripSeparator2, Me.ListFontToolStripMenuItem, Me.DocumentFontToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.ShowDropDownArrow = False
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(53, 22)
        Me.ToolStripDropDownButton1.Text = "Settings"
        '
        'WordWrapToolStripMenuItem
        '
        Me.WordWrapToolStripMenuItem.Checked = True
        Me.WordWrapToolStripMenuItem.CheckOnClick = True
        Me.WordWrapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.WordWrapToolStripMenuItem.Name = "WordWrapToolStripMenuItem"
        Me.WordWrapToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.WordWrapToolStripMenuItem.Text = "Word Wrap"
        '
        'StatusStripToolStripMenuItem
        '
        Me.StatusStripToolStripMenuItem.Checked = True
        Me.StatusStripToolStripMenuItem.CheckOnClick = True
        Me.StatusStripToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.StatusStripToolStripMenuItem.Name = "StatusStripToolStripMenuItem"
        Me.StatusStripToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.StatusStripToolStripMenuItem.Text = "Status Strip"
        '
        'ListFontToolStripMenuItem
        '
        Me.ListFontToolStripMenuItem.Name = "ListFontToolStripMenuItem"
        Me.ListFontToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.ListFontToolStripMenuItem.Text = "List Font.."
        '
        'DocumentFontToolStripMenuItem
        '
        Me.DocumentFontToolStripMenuItem.Name = "DocumentFontToolStripMenuItem"
        Me.DocumentFontToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.DocumentFontToolStripMenuItem.Text = "Document Font.."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(160, 6)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(706, 485)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.SS1)
        Me.Controls.Add(Me.TS1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "Codebase"
        CType(Me.FSW1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TS1.ResumeLayout(False)
        Me.TS1.PerformLayout()
        Me.SS1.ResumeLayout(False)
        Me.SS1.PerformLayout()
        Me.CMS1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FSW1 As System.IO.FileSystemWatcher
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TS1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SS1 As System.Windows.Forms.StatusStrip
    Friend WithEvents TSL1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents CMS1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RenameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents WordWrapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStripToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListFontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DocumentFontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
