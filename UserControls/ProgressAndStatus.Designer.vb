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
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightGreen
        Me.Panel1.Controls.Add(Me.LabelStatus)
        Me.Panel1.Controls.Add(Me.TextBoxStatus)
        Me.Panel1.Location = New System.Drawing.Point(19, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(474, 161)
        Me.Panel1.TabIndex = 0
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.BackColor = System.Drawing.Color.LightGreen
        Me.TextBoxStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxStatus.Location = New System.Drawing.Point(16, 26)
        Me.TextBoxStatus.Multiline = True
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(447, 117)
        Me.TextBoxStatus.TabIndex = 0
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStatus.Location = New System.Drawing.Point(13, 6)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(46, 17)
        Me.LabelStatus.TabIndex = 1
        Me.LabelStatus.Text = "Status"
        '
        'ProgressAndStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ProgressAndStatus"
        Me.Size = New System.Drawing.Size(511, 199)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LabelStatus As System.Windows.Forms.Label
    Public WithEvents TextBoxStatus As System.Windows.Forms.TextBox

End Class
