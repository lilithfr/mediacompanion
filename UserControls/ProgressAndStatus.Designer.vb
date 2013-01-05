<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressAndStatus
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBoxProgresstext = New System.Windows.Forms.TextBox()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Green
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.TextBoxProgresstext)
        Me.Panel1.Controls.Add(Me.LabelStatus)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(530, 183)
        Me.Panel1.TabIndex = 0
        '
        'TextBoxProgresstext
        '
        Me.TextBoxProgresstext.BackColor = System.Drawing.Color.LightGreen
        Me.TextBoxProgresstext.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxProgresstext.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TextBoxProgresstext.Location = New System.Drawing.Point(16, 26)
        Me.TextBoxProgresstext.Multiline = True
        Me.TextBoxProgresstext.Name = "TextBoxProgresstext"
        Me.TextBoxProgresstext.Size = New System.Drawing.Size(497, 60)
        Me.TextBoxProgresstext.TabIndex = 2
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStatus.ForeColor = System.Drawing.Color.LightGreen
        Me.LabelStatus.Location = New System.Drawing.Point(13, 6)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(89, 17)
        Me.LabelStatus.TabIndex = 1
        Me.LabelStatus.Text = "Computing..."
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.LightGreen
        Me.ButtonCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCancel.Location = New System.Drawing.Point(423, 147)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(90, 31)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(19, 102)
        Me.ProgressBar1.Maximum = 1000
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(493, 32)
        Me.ProgressBar1.TabIndex = 4
        '
        'ProgressAndStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ProgressAndStatus"
        Me.Size = New System.Drawing.Size(532, 185)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LabelStatus As System.Windows.Forms.Label
    Friend WithEvents TextBoxProgresstext As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

End Class
