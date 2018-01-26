<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits Windows.Forms.Form

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
        Me.TrackBar1 = New Windows.Forms.TrackBar
        Me.TrackBar2 = New Windows.Forms.TrackBar
        Me.ListBox1 = New Windows.Forms.ListBox
        Me.ToolStrip1 = New Windows.Forms.ToolStrip
        Me.ToolStripDropDownButton1 = New Windows.Forms.ToolStripDropDownButton
        Me.SettingsToolStripMenuItem = New Windows.Forms.ToolStripMenuItem
        Me.FilesToolStripMenuItem = New Windows.Forms.ToolStripMenuItem
        Me.FolderToolStripMenuItem = New Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New Windows.Forms.ToolStripSeparator
        Me.LoadToolStripMenuItem = New Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New Windows.Forms.ToolStripMenuItem
        Me.ToolStripLabel1 = New Windows.Forms.ToolStripLabel
        Me.ToolStripButton1 = New Windows.Forms.ToolStripButton
        Me.TextBox1 = New Windows.Forms.TextBox
        Me.Timer1 = New Windows.Forms.Timer(Me.components)
        Me.Button1 = New Windows.Forms.Button
        Me.Button2 = New Windows.Forms.Button
        Me.Button3 = New Windows.Forms.Button
        Me.Button4 = New Windows.Forms.Button
        Me.Button5 = New Windows.Forms.Button
        Me.Label1 = New Windows.Forms.Label
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TrackBar1
        '
        Me.TrackBar1.Anchor = CType(((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Left) _
                    Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.TrackBar1.AutoSize = False
        Me.TrackBar1.Location = New Drawing.Point(48, 239)
        Me.TrackBar1.Maximum = 0
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New Drawing.Size(150, 24)
        Me.TrackBar1.TabIndex = 2
        Me.TrackBar1.TabStop = False
        Me.TrackBar1.TickStyle = Windows.Forms.TickStyle.None
        '
        'TrackBar2
        '
        Me.TrackBar2.Anchor = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.TrackBar2.AutoSize = False
        Me.TrackBar2.Location = New Drawing.Point(312, 239)
        Me.TrackBar2.Maximum = 100
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New Drawing.Size(105, 24)
        Me.TrackBar2.TabIndex = 3
        Me.TrackBar2.TickStyle = Windows.Forms.TickStyle.None
        Me.TrackBar2.Value = 100
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType((((Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Bottom) _
                    Or Windows.Forms.AnchorStyles.Left) _
                    Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.ListBox1.Font = New Drawing.Font("Verdana", 8.25!, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New Drawing.Point(12, 54)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New Drawing.Size(441, 173)
        Me.ListBox1.Sorted = True
        Me.ListBox1.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New Drawing.Font("Verdana", 8.25!, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripLabel1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New Drawing.Point(0, 0)
        Me.ToolStrip1.Margin = New Windows.Forms.Padding(0, 0, 0, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New Drawing.Size(465, 25)
        Me.ToolStrip1.TabIndex = 13
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.FilesToolStripMenuItem, Me.FolderToolStripMenuItem, Me.ToolStripSeparator1, Me.LoadToolStripMenuItem, Me.SaveToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.ShowDropDownArrow = False
        Me.ToolStripDropDownButton1.Size = New Drawing.Size(41, 22)
        Me.ToolStripDropDownButton1.Text = "Menu"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New Drawing.Size(134, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'FilesToolStripMenuItem
        '
        Me.FilesToolStripMenuItem.Name = "FilesToolStripMenuItem"
        Me.FilesToolStripMenuItem.Size = New Drawing.Size(134, 22)
        Me.FilesToolStripMenuItem.Text = "Files.."
        '
        'FolderToolStripMenuItem
        '
        Me.FolderToolStripMenuItem.Name = "FolderToolStripMenuItem"
        Me.FolderToolStripMenuItem.Size = New Drawing.Size(134, 22)
        Me.FolderToolStripMenuItem.Text = "Folder.."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New Drawing.Size(131, 6)
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Size = New Drawing.Size(134, 22)
        Me.LoadToolStripMenuItem.Text = "Load List.."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New Drawing.Size(134, 22)
        Me.SaveToolStripMenuItem.Text = "Save List.."
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.BackColor = Drawing.Color.Transparent
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New Drawing.Size(0, 22)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.CheckOnClick = True
        Me.ToolStripButton1.DisplayStyle = Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New Drawing.Size(58, 22)
        Me.ToolStripButton1.Text = "Random"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Left) _
                    Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.TextBox1.Font = New Drawing.Font("Verdana", 8.25!, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New Drawing.Point(12, 31)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Drawing.Size(441, 21)
        Me.TextBox1.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Button1
        '
        Me.Button1.Anchor = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Left), Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.My.Resources.Resources._Stop
        Me.Button1.Location = New Drawing.Point(12, 233)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New Drawing.Size(30, 30)
        Me.Button1.TabIndex = 14
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.Button2.Image = Global.My.Resources.Resources.Backward
        Me.Button2.Location = New Drawing.Point(204, 233)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New Drawing.Size(30, 30)
        Me.Button2.TabIndex = 15
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.Button3.Image = Global.My.Resources.Resources.Play
        Me.Button3.Location = New Drawing.Point(240, 233)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New Drawing.Size(30, 30)
        Me.Button3.TabIndex = 16
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.Button4.Image = Global.My.Resources.Resources.Forward
        Me.Button4.Location = New Drawing.Point(276, 233)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New Drawing.Size(30, 30)
        Me.Button4.TabIndex = 17
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Anchor = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.Button5.Image = Global.My.Resources.Resources.Unmute
        Me.Button5.Location = New Drawing.Point(423, 233)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New Drawing.Size(30, 30)
        Me.Button5.TabIndex = 18
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Left) _
                    Or Windows.Forms.AnchorStyles.Right), Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = Drawing.SystemColors.Window
        Me.Label1.Font = New Drawing.Font("Verdana", 8.25!, Drawing.FontStyle.Italic, Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = Drawing.Color.LightGray
        Me.Label1.Location = New Drawing.Point(13, 32)
        Me.Label1.Margin = New Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Drawing.Size(439, 19)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Search"
        Me.Label1.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '
        'Main
        '
        Me.AutoScaleDimensions = New Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = Windows.Forms.AutoScaleMode.Font
        Me.BackColor = Drawing.SystemColors.Control
        Me.ClientSize = New Drawing.Size(465, 271)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TrackBar2)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ListBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), Drawing.Icon)
        Me.MinimumSize = New Drawing.Size(324, 195)
        Me.Name = "Main"
        Me.Text = "Media Feather"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TrackBar1 As Windows.Forms.TrackBar
    Friend WithEvents TrackBar2 As Windows.Forms.TrackBar
    Friend WithEvents ListBox1 As Windows.Forms.ListBox
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents SettingsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilesToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents Timer1 As Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents Button4 As Windows.Forms.Button
    Friend WithEvents Button5 As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents FolderToolStripMenuItem As Windows.Forms.ToolStripMenuItem

End Class
