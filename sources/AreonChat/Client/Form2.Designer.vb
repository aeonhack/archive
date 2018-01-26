<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.ATB5 = New AreonControls.AreonTextBox
        Me.ATB6 = New AreonControls.AreonTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AreonButton1 = New AreonControls.AreonButton
        Me.AB3 = New AreonControls.AreonButton
        Me.SuspendLayout()
        '
        'ATB5
        '
        Me.ATB5.Allowed = Nothing
        Me.ATB5.BackColor = System.Drawing.Color.White
        Me.ATB5.DefaultColor = System.Drawing.Color.Gray
        Me.ATB5.DefaultText = ""
        Me.ATB5.DisallowInput = "!@#%&*()_{}:""?,/;'[]\"
        Me.ATB5.DisallowLetters = True
        Me.ATB5.DisallowNumbers = False
        Me.ATB5.DisallowPunctuation = False
        Me.ATB5.DisallowSymbols = True
        Me.ATB5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ATB5.Location = New System.Drawing.Point(97, 12)
        Me.ATB5.MaxLength = 15
        Me.ATB5.Name = "ATB5"
        Me.ATB5.Size = New System.Drawing.Size(144, 21)
        Me.ATB5.TabIndex = 5
        Me.ATB5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ATB6
        '
        Me.ATB6.Allowed = Nothing
        Me.ATB6.BackColor = System.Drawing.Color.White
        Me.ATB6.DefaultColor = System.Drawing.Color.Gray
        Me.ATB6.DefaultText = ""
        Me.ATB6.DisallowInput = Nothing
        Me.ATB6.DisallowLetters = True
        Me.ATB6.DisallowNumbers = False
        Me.ATB6.DisallowPunctuation = True
        Me.ATB6.DisallowSymbols = True
        Me.ATB6.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.ATB6.Location = New System.Drawing.Point(97, 39)
        Me.ATB6.MaxLength = 5
        Me.ATB6.Name = "ATB6"
        Me.ATB6.Size = New System.Drawing.Size(144, 21)
        Me.ATB6.TabIndex = 6
        Me.ATB6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(20, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Remote Port"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Remote Host"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AreonButton1
        '
        Me.AreonButton1.BackColor = System.Drawing.Color.Black
        Me.AreonButton1.BackgroundImage = Nothing
        Me.AreonButton1.BackgroundImageLayout = Nothing
        Me.AreonButton1.DownImage = CType(resources.GetObject("AreonButton1.DownImage"), System.Drawing.Image)
        Me.AreonButton1.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.AreonButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.AreonButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.AreonButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AreonButton1.Font = New System.Drawing.Font("Eras Demi ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AreonButton1.ForeColor = System.Drawing.Color.White
        Me.AreonButton1.Location = New System.Drawing.Point(21, 81)
        Me.AreonButton1.Name = "AreonButton1"
        Me.AreonButton1.OverImage = CType(resources.GetObject("AreonButton1.OverImage"), System.Drawing.Image)
        Me.AreonButton1.Size = New System.Drawing.Size(107, 26)
        Me.AreonButton1.TabIndex = 4
        Me.AreonButton1.Text = "Cancel"
        Me.AreonButton1.UpImage = CType(resources.GetObject("AreonButton1.UpImage"), System.Drawing.Image)
        Me.AreonButton1.UseVisualStyleBackColor = False
        '
        'AB3
        '
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
        Me.AB3.Location = New System.Drawing.Point(134, 81)
        Me.AB3.Name = "AB3"
        Me.AB3.OverImage = CType(resources.GetObject("AB3.OverImage"), System.Drawing.Image)
        Me.AB3.Size = New System.Drawing.Size(107, 26)
        Me.AB3.TabIndex = 3
        Me.AB3.Text = "Save and Close"
        Me.AB3.UpImage = CType(resources.GetObject("AB3.UpImage"), System.Drawing.Image)
        Me.AB3.UseVisualStyleBackColor = False
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(263, 118)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ATB6)
        Me.Controls.Add(Me.ATB5)
        Me.Controls.Add(Me.AreonButton1)
        Me.Controls.Add(Me.AB3)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "Form2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AB3 As AreonControls.AreonButton
    Friend WithEvents AreonButton1 As AreonControls.AreonButton
    Friend WithEvents ATB5 As AreonControls.AreonTextBox
    Friend WithEvents ATB6 As AreonControls.AreonTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
