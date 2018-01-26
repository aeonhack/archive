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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.OpenMS = New System.Windows.Forms.ToolStripMenuItem
        Me.ReloadMS = New System.Windows.Forms.ToolStripMenuItem
        Me.HistoryMS = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseMS = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton
        Me.SetFrameMS = New System.Windows.Forms.ToolStripMenuItem
        Me.ManipulationMS = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripDropDownButton3 = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ExactFitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CenteredToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripDropDownButton4 = New System.Windows.Forms.ToolStripDropDownButton
        Me.SplitterLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.CurFrame = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.MaxFrames = New System.Windows.Forms.ToolStripStatusLabel
        Me.Shockwave = New AxShockwaveFlashObjects.AxShockwaveFlash
        Me.StatusStrip2.SuspendLayout()
        CType(Me.Shockwave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'StatusStrip2
        '
        Me.StatusStrip2.AutoSize = False
        Me.StatusStrip2.BackgroundImage = CType(resources.GetObject("StatusStrip2.BackgroundImage"), System.Drawing.Image)
        Me.StatusStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStrip2.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip2.GripMargin = New System.Windows.Forms.Padding(0)
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripDropDownButton2, Me.ToolStripDropDownButton3, Me.ToolStripDropDownButton4, Me.SplitterLabel, Me.CurFrame, Me.ToolStripStatusLabel1, Me.MaxFrames})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(472, 33)
        Me.StatusStrip2.SizingGrip = False
        Me.StatusStrip2.TabIndex = 5
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenMS, Me.ReloadMS, Me.HistoryMS, Me.ToolStripSeparator1, Me.CloseMS})
        Me.ToolStripDropDownButton1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripDropDownButton1.ForeColor = System.Drawing.Color.DimGray
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.ShowDropDownArrow = False
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(37, 31)
        Me.ToolStripDropDownButton1.Text = "Menu"
        '
        'OpenMS
        '
        Me.OpenMS.Name = "OpenMS"
        Me.OpenMS.Size = New System.Drawing.Size(152, 22)
        Me.OpenMS.Text = "Open..."
        '
        'ReloadMS
        '
        Me.ReloadMS.Enabled = False
        Me.ReloadMS.Name = "ReloadMS"
        Me.ReloadMS.Size = New System.Drawing.Size(152, 22)
        Me.ReloadMS.Text = "Reload"
        '
        'HistoryMS
        '
        Me.HistoryMS.Name = "HistoryMS"
        Me.HistoryMS.Size = New System.Drawing.Size(152, 22)
        Me.HistoryMS.Text = "History..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'CloseMS
        '
        Me.CloseMS.Name = "CloseMS"
        Me.CloseMS.Size = New System.Drawing.Size(152, 22)
        Me.CloseMS.Text = "Close"
        '
        'ToolStripDropDownButton2
        '
        Me.ToolStripDropDownButton2.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetFrameMS, Me.ManipulationMS})
        Me.ToolStripDropDownButton2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripDropDownButton2.ForeColor = System.Drawing.Color.DimGray
        Me.ToolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
        Me.ToolStripDropDownButton2.ShowDropDownArrow = False
        Me.ToolStripDropDownButton2.Size = New System.Drawing.Size(46, 31)
        Me.ToolStripDropDownButton2.Text = "Actions"
        '
        'SetFrameMS
        '
        Me.SetFrameMS.Name = "SetFrameMS"
        Me.SetFrameMS.Size = New System.Drawing.Size(157, 22)
        Me.SetFrameMS.Text = "Set Frame..."
        '
        'ManipulationMS
        '
        Me.ManipulationMS.Name = "ManipulationMS"
        Me.ManipulationMS.Size = New System.Drawing.Size(157, 22)
        Me.ManipulationMS.Text = "Manipulation..."
        '
        'ToolStripDropDownButton3
        '
        Me.ToolStripDropDownButton3.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ExactFitToolStripMenuItem, Me.CenteredToolStripMenuItem})
        Me.ToolStripDropDownButton3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripDropDownButton3.ForeColor = System.Drawing.Color.DimGray
        Me.ToolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton3.Name = "ToolStripDropDownButton3"
        Me.ToolStripDropDownButton3.ShowDropDownArrow = False
        Me.ToolStripDropDownButton3.Size = New System.Drawing.Size(33, 31)
        Me.ToolStripDropDownButton3.Text = "View"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(133, 22)
        Me.ToolStripMenuItem1.Text = "Show All"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(133, 22)
        Me.ToolStripMenuItem2.Text = "No Border"
        '
        'ExactFitToolStripMenuItem
        '
        Me.ExactFitToolStripMenuItem.Checked = True
        Me.ExactFitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ExactFitToolStripMenuItem.Name = "ExactFitToolStripMenuItem"
        Me.ExactFitToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.ExactFitToolStripMenuItem.Text = "Exact Fit"
        '
        'CenteredToolStripMenuItem
        '
        Me.CenteredToolStripMenuItem.Name = "CenteredToolStripMenuItem"
        Me.CenteredToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.CenteredToolStripMenuItem.Text = "Centered"
        '
        'ToolStripDropDownButton4
        '
        Me.ToolStripDropDownButton4.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripDropDownButton4.ForeColor = System.Drawing.Color.DimGray
        Me.ToolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton4.Name = "ToolStripDropDownButton4"
        Me.ToolStripDropDownButton4.ShowDropDownArrow = False
        Me.ToolStripDropDownButton4.Size = New System.Drawing.Size(63, 31)
        Me.ToolStripDropDownButton4.Text = "Full Screen"
        '
        'SplitterLabel
        '
        Me.SplitterLabel.BackColor = System.Drawing.Color.Transparent
        Me.SplitterLabel.Name = "SplitterLabel"
        Me.SplitterLabel.Size = New System.Drawing.Size(30, 28)
        Me.SplitterLabel.Spring = True
        '
        'CurFrame
        '
        Me.CurFrame.BackColor = System.Drawing.Color.Transparent
        Me.CurFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CurFrame.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurFrame.ForeColor = System.Drawing.Color.DimGray
        Me.CurFrame.Name = "CurFrame"
        Me.CurFrame.Size = New System.Drawing.Size(97, 28)
        Me.CurFrame.Text = "Current Frame  (0)"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(11, 28)
        Me.ToolStripStatusLabel1.Text = "|"
        '
        'MaxFrames
        '
        Me.MaxFrames.BackColor = System.Drawing.Color.Transparent
        Me.MaxFrames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.MaxFrames.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaxFrames.ForeColor = System.Drawing.Color.DimGray
        Me.MaxFrames.Name = "MaxFrames"
        Me.MaxFrames.Size = New System.Drawing.Size(109, 28)
        Me.MaxFrames.Text = "Maximum Frames  (0)"
        '
        'Shockwave
        '
        Me.Shockwave.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Shockwave.Enabled = True
        Me.Shockwave.Location = New System.Drawing.Point(0, 33)
        Me.Shockwave.Margin = New System.Windows.Forms.Padding(0)
        Me.Shockwave.Name = "Shockwave"
        Me.Shockwave.OcxState = CType(resources.GetObject("Shockwave.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Shockwave.Size = New System.Drawing.Size(472, 333)
        Me.Shockwave.TabIndex = 8
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(472, 366)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.Shockwave)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(460, 68)
        Me.Name = "Main"
        Me.Text = "Areon Flash"
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        CType(Me.Shockwave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents OpenMS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReloadMS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HistoryMS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseMS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton2 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents SetFrameMS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManipulationMS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitterLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MaxFrames As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents CurFrame As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Shockwave As AxShockwaveFlashObjects.AxShockwaveFlash
    Friend WithEvents ToolStripDropDownButton3 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripDropDownButton4 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExactFitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CenteredToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
