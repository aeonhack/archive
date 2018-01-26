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
        Me.CurrentTime = New System.Windows.Forms.Label
        Me.AlarmHour = New System.Windows.Forms.TextBox
        Me.AlarmMin = New System.Windows.Forms.TextBox
        Me.AlarmND = New System.Windows.Forms.DomainUpDown
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.AlarmSet = New System.Windows.Forms.Button
        Me.AlarmIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMS = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MSToggle = New System.Windows.Forms.ToolStripMenuItem
        Me.MSSeperator = New System.Windows.Forms.ToolStripSeparator
        Me.MSClose = New System.Windows.Forms.ToolStripMenuItem
        Me.Label2 = New System.Windows.Forms.Label
        Me.ContextMS.SuspendLayout()
        Me.SuspendLayout()
        '
        'CurrentTime
        '
        Me.CurrentTime.BackColor = System.Drawing.SystemColors.Control
        Me.CurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CurrentTime.Dock = System.Windows.Forms.DockStyle.Top
        Me.CurrentTime.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrentTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CurrentTime.Location = New System.Drawing.Point(0, 0)
        Me.CurrentTime.Name = "CurrentTime"
        Me.CurrentTime.Size = New System.Drawing.Size(209, 23)
        Me.CurrentTime.TabIndex = 5
        Me.CurrentTime.Text = "12:00 PM"
        Me.CurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AlarmHour
        '
        Me.AlarmHour.BackColor = System.Drawing.SystemColors.Window
        Me.AlarmHour.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlarmHour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AlarmHour.Location = New System.Drawing.Point(12, 32)
        Me.AlarmHour.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.AlarmHour.MaxLength = 2
        Me.AlarmHour.Name = "AlarmHour"
        Me.AlarmHour.Size = New System.Drawing.Size(29, 23)
        Me.AlarmHour.TabIndex = 1
        Me.AlarmHour.Text = "7"
        Me.AlarmHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AlarmMin
        '
        Me.AlarmMin.BackColor = System.Drawing.SystemColors.Window
        Me.AlarmMin.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlarmMin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AlarmMin.Location = New System.Drawing.Point(58, 32)
        Me.AlarmMin.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.AlarmMin.MaxLength = 2
        Me.AlarmMin.Name = "AlarmMin"
        Me.AlarmMin.Size = New System.Drawing.Size(29, 23)
        Me.AlarmMin.TabIndex = 2
        Me.AlarmMin.Text = "00"
        Me.AlarmMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AlarmND
        '
        Me.AlarmND.BackColor = System.Drawing.SystemColors.Window
        Me.AlarmND.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlarmND.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AlarmND.Items.Add("AM")
        Me.AlarmND.Items.Add("PM")
        Me.AlarmND.Location = New System.Drawing.Point(93, 32)
        Me.AlarmND.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.AlarmND.Name = "AlarmND"
        Me.AlarmND.Size = New System.Drawing.Size(40, 23)
        Me.AlarmND.TabIndex = 3
        Me.AlarmND.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 1000
        '
        'AlarmSet
        '
        Me.AlarmSet.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlarmSet.Location = New System.Drawing.Point(139, 31)
        Me.AlarmSet.Name = "AlarmSet"
        Me.AlarmSet.Size = New System.Drawing.Size(59, 24)
        Me.AlarmSet.TabIndex = 0
        Me.AlarmSet.Text = "Turn On"
        Me.AlarmSet.UseVisualStyleBackColor = True
        '
        'AlarmIcon
        '
        Me.AlarmIcon.ContextMenuStrip = Me.ContextMS
        Me.AlarmIcon.Icon = CType(resources.GetObject("AlarmIcon.Icon"), System.Drawing.Icon)
        Me.AlarmIcon.Text = "Alarm: Not Set"
        '
        'ContextMS
        '
        Me.ContextMS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MSToggle, Me.MSSeperator, Me.MSClose})
        Me.ContextMS.Name = "ContextMS"
        Me.ContextMS.Size = New System.Drawing.Size(125, 54)
        '
        'MSToggle
        '
        Me.MSToggle.Name = "MSToggle"
        Me.MSToggle.Size = New System.Drawing.Size(124, 22)
        Me.MSToggle.Text = "Turn On"
        '
        'MSSeperator
        '
        Me.MSSeperator.Name = "MSSeperator"
        Me.MSSeperator.Size = New System.Drawing.Size(121, 6)
        '
        'MSClose
        '
        Me.MSClose.Name = "MSClose"
        Me.MSClose.Size = New System.Drawing.Size(124, 22)
        Me.MSClose.Text = "Close"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(43, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = ":"
        '
        'Main
        '
        Me.AcceptButton = Me.AlarmSet
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(209, 61)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CurrentTime)
        Me.Controls.Add(Me.AlarmND)
        Me.Controls.Add(Me.AlarmMin)
        Me.Controls.Add(Me.AlarmSet)
        Me.Controls.Add(Me.AlarmHour)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.Text = "Areon Alarm"
        Me.ContextMS.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CurrentTime As System.Windows.Forms.Label
    Friend WithEvents AlarmHour As System.Windows.Forms.TextBox
    Friend WithEvents AlarmMin As System.Windows.Forms.TextBox
    Friend WithEvents AlarmND As System.Windows.Forms.DomainUpDown
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents AlarmSet As System.Windows.Forms.Button
    Friend WithEvents AlarmIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMS As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MSToggle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MSSeperator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MSClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
