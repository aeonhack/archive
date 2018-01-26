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
        Me.LV = New System.Windows.Forms.ListView
        Me.CM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.CloneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RenameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IL = New System.Windows.Forms.ImageList(Me.components)
        Me.FB = New System.Windows.Forms.FolderBrowserDialog
        Me.TS = New System.Windows.Forms.ToolStrip
        Me.Up = New System.Windows.Forms.ToolStripButton
        Me.Go = New System.Windows.Forms.ToolStripButton
        Me.Browse = New System.Windows.Forms.ToolStripButton
        Me.Address = New System.Windows.Forms.TextBox
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.CM.SuspendLayout()
        Me.TS.SuspendLayout()
        Me.SuspendLayout()
        '
        'LV
        '
        Me.LV.ContextMenuStrip = Me.CM
        Me.LV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LV.GridLines = True
        Me.LV.LabelEdit = True
        Me.LV.LargeImageList = Me.IL
        Me.LV.Location = New System.Drawing.Point(0, 28)
        Me.LV.Name = "LV"
        Me.LV.Size = New System.Drawing.Size(571, 376)
        Me.LV.TabIndex = 0
        Me.LV.TileSize = New System.Drawing.Size(175, 36)
        Me.LV.UseCompatibleStateImageBehavior = False
        Me.LV.View = System.Windows.Forms.View.Tile
        '
        'CM
        '
        Me.CM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.ToolStripSeparator2, Me.CloneToolStripMenuItem, Me.RenameToolStripMenuItem, Me.ToolStripSeparator1, Me.DeleteToolStripMenuItem})
        Me.CM.Name = "ContextMenuStrip1"
        Me.CM.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.CM.ShowItemToolTips = False
        Me.CM.Size = New System.Drawing.Size(125, 104)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(121, 6)
        '
        'CloneToolStripMenuItem
        '
        Me.CloneToolStripMenuItem.Image = CType(resources.GetObject("CloneToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CloneToolStripMenuItem.Name = "CloneToolStripMenuItem"
        Me.CloneToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.CloneToolStripMenuItem.Text = "Clone"
        '
        'RenameToolStripMenuItem
        '
        Me.RenameToolStripMenuItem.Image = CType(resources.GetObject("RenameToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        Me.RenameToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.RenameToolStripMenuItem.Text = "Rename"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(121, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = CType(resources.GetObject("DeleteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'IL
        '
        Me.IL.ImageStream = CType(resources.GetObject("IL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IL.TransparentColor = System.Drawing.Color.Transparent
        Me.IL.Images.SetKeyName(0, "Folder.png")
        '
        'FB
        '
        Me.FB.Description = "Please select a path you would like to navigate to."
        '
        'TS
        '
        Me.TS.AutoSize = False
        Me.TS.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.TS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Up, Me.Go, Me.Browse})
        Me.TS.Location = New System.Drawing.Point(0, 0)
        Me.TS.Name = "TS"
        Me.TS.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.TS.Size = New System.Drawing.Size(571, 28)
        Me.TS.TabIndex = 1
        '
        'Up
        '
        Me.Up.AutoToolTip = False
        Me.Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Up.Image = CType(resources.GetObject("Up.Image"), System.Drawing.Image)
        Me.Up.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Up.Name = "Up"
        Me.Up.Size = New System.Drawing.Size(28, 25)
        Me.Up.Text = "ToolStripButton1"
        Me.Up.ToolTipText = "Parent Directory"
        '
        'Go
        '
        Me.Go.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Go.AutoToolTip = False
        Me.Go.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Go.Image = CType(resources.GetObject("Go.Image"), System.Drawing.Image)
        Me.Go.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Go.Name = "Go"
        Me.Go.Size = New System.Drawing.Size(28, 25)
        Me.Go.Text = "ToolStripButton2"
        Me.Go.ToolTipText = "Navigate"
        '
        'Browse
        '
        Me.Browse.AutoToolTip = False
        Me.Browse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Browse.Image = CType(resources.GetObject("Browse.Image"), System.Drawing.Image)
        Me.Browse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(28, 25)
        Me.Browse.Text = "ToolStripButton1"
        Me.Browse.ToolTipText = "Browse.."
        '
        'Address
        '
        Me.Address.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Address.Location = New System.Drawing.Point(66, 4)
        Me.Address.Name = "Address"
        Me.Address.Size = New System.Drawing.Size(475, 20)
        Me.Address.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(66, 4)
        Me.ProgressBar1.Maximum = 10000
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(475, 20)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 3
        Me.ProgressBar1.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(571, 404)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Address)
        Me.Controls.Add(Me.LV)
        Me.Controls.Add(Me.TS)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Areon Explorer"
        Me.CM.ResumeLayout(False)
        Me.TS.ResumeLayout(False)
        Me.TS.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LV As System.Windows.Forms.ListView
    Friend WithEvents FB As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents IL As System.Windows.Forms.ImageList
    Friend WithEvents TS As System.Windows.Forms.ToolStrip
    Friend WithEvents CM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RenameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Up As System.Windows.Forms.ToolStripButton
    Friend WithEvents Go As System.Windows.Forms.ToolStripButton
    Friend WithEvents Browse As System.Windows.Forms.ToolStripButton
    Friend WithEvents Address As System.Windows.Forms.TextBox
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

End Class
