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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Wallet = New System.Windows.Forms.Label
        Me.Pull = New System.Windows.Forms.Button
        Me.Dollars = New System.Windows.Forms.TextBox
        Me.Cents = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Scores = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Quit = New System.Windows.Forms.Button
        Me.Slot1 = New System.Windows.Forms.PictureBox
        Me.Slot2 = New System.Windows.Forms.PictureBox
        Me.Slot3 = New System.Windows.Forms.PictureBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Slot1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Slot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Slot3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.ImageLocation = ""
        Me.PictureBox1.Location = New System.Drawing.Point(10, 125)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(42, 42)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Wallet
        '
        Me.Wallet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Wallet.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wallet.Location = New System.Drawing.Point(55, 149)
        Me.Wallet.Name = "Wallet"
        Me.Wallet.Size = New System.Drawing.Size(123, 18)
        Me.Wallet.TabIndex = 4
        Me.Wallet.Text = "$3.00"
        '
        'Pull
        '
        Me.Pull.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pull.Image = CType(resources.GetObject("Pull.Image"), System.Drawing.Image)
        Me.Pull.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Pull.Location = New System.Drawing.Point(184, 125)
        Me.Pull.Name = "Pull"
        Me.Pull.Padding = New System.Windows.Forms.Padding(5)
        Me.Pull.Size = New System.Drawing.Size(85, 42)
        Me.Pull.TabIndex = 0
        Me.Pull.Text = "   Pull"
        Me.Pull.UseVisualStyleBackColor = True
        '
        'Dollars
        '
        Me.Dollars.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dollars.Location = New System.Drawing.Point(55, 125)
        Me.Dollars.Margin = New System.Windows.Forms.Padding(1)
        Me.Dollars.MaxLength = 10
        Me.Dollars.Name = "Dollars"
        Me.Dollars.Size = New System.Drawing.Size(74, 21)
        Me.Dollars.TabIndex = 2
        Me.Dollars.Text = "0"
        Me.Dollars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Cents
        '
        Me.Cents.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cents.Location = New System.Drawing.Point(141, 125)
        Me.Cents.Margin = New System.Windows.Forms.Padding(1)
        Me.Cents.MaxLength = 2
        Me.Cents.Name = "Cents"
        Me.Cents.Size = New System.Drawing.Size(37, 21)
        Me.Cents.TabIndex = 1
        Me.Cents.Text = "25"
        Me.Cents.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(130, 132)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "."
        '
        'Scores
        '
        Me.Scores.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Scores.Location = New System.Drawing.Point(10, 2)
        Me.Scores.Name = "Scores"
        Me.Scores.Size = New System.Drawing.Size(129, 23)
        Me.Scores.TabIndex = 4
        Me.Scores.Text = "Info..."
        Me.Scores.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 170)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(259, 26)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "HighScore"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Quit
        '
        Me.Quit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Quit.Location = New System.Drawing.Point(145, 2)
        Me.Quit.Name = "Quit"
        Me.Quit.Size = New System.Drawing.Size(124, 23)
        Me.Quit.TabIndex = 3
        Me.Quit.Text = "Quit"
        Me.Quit.UseVisualStyleBackColor = True
        '
        'Slot1
        '
        Me.Slot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Slot1.Image = CType(resources.GetObject("Slot1.Image"), System.Drawing.Image)
        Me.Slot1.Location = New System.Drawing.Point(10, 29)
        Me.Slot1.Margin = New System.Windows.Forms.Padding(1)
        Me.Slot1.Name = "Slot1"
        Me.Slot1.Size = New System.Drawing.Size(85, 90)
        Me.Slot1.TabIndex = 12
        Me.Slot1.TabStop = False
        '
        'Slot2
        '
        Me.Slot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Slot2.Image = CType(resources.GetObject("Slot2.Image"), System.Drawing.Image)
        Me.Slot2.Location = New System.Drawing.Point(97, 29)
        Me.Slot2.Margin = New System.Windows.Forms.Padding(1)
        Me.Slot2.Name = "Slot2"
        Me.Slot2.Size = New System.Drawing.Size(85, 90)
        Me.Slot2.TabIndex = 13
        Me.Slot2.TabStop = False
        '
        'Slot3
        '
        Me.Slot3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Slot3.Image = CType(resources.GetObject("Slot3.Image"), System.Drawing.Image)
        Me.Slot3.Location = New System.Drawing.Point(184, 29)
        Me.Slot3.Margin = New System.Windows.Forms.Padding(1)
        Me.Slot3.Name = "Slot3"
        Me.Slot3.Size = New System.Drawing.Size(85, 90)
        Me.Slot3.TabIndex = 14
        Me.Slot3.TabStop = False
        '
        'Form1
        '
        Me.AcceptButton = Me.Pull
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(281, 195)
        Me.Controls.Add(Me.Slot3)
        Me.Controls.Add(Me.Slot2)
        Me.Controls.Add(Me.Slot1)
        Me.Controls.Add(Me.Quit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Scores)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Cents)
        Me.Controls.Add(Me.Dollars)
        Me.Controls.Add(Me.Pull)
        Me.Controls.Add(Me.Wallet)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Areon Slots"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Slot1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Slot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Slot3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Wallet As System.Windows.Forms.Label
    Friend WithEvents Pull As System.Windows.Forms.Button
    Friend WithEvents Dollars As System.Windows.Forms.TextBox
    Friend WithEvents Cents As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Scores As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Quit As System.Windows.Forms.Button
    Friend WithEvents Slot1 As System.Windows.Forms.PictureBox
    Friend WithEvents Slot2 As System.Windows.Forms.PictureBox
    Friend WithEvents Slot3 As System.Windows.Forms.PictureBox

End Class
