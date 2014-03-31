<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovPosterCrop
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
        Me.PicBox = New System.Windows.Forms.PictureBox()
        Me.btn_CropAccept = New System.Windows.Forms.Button()
        Me.btn_CropCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tb_cropmovtitle = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.PicBox,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'PicBox
        '
        Me.PicBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.PicBox.Location = New System.Drawing.Point(20, 15)
        Me.PicBox.Name = "PicBox"
        Me.PicBox.Size = New System.Drawing.Size(441, 525)
        Me.PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicBox.TabIndex = 0
        Me.PicBox.TabStop = false
        '
        'btn_CropAccept
        '
        Me.btn_CropAccept.Location = New System.Drawing.Point(266, 612)
        Me.btn_CropAccept.Name = "btn_CropAccept"
        Me.btn_CropAccept.Size = New System.Drawing.Size(95, 29)
        Me.btn_CropAccept.TabIndex = 1
        Me.btn_CropAccept.Text = "Accept"
        Me.btn_CropAccept.UseVisualStyleBackColor = true
        '
        'btn_CropCancel
        '
        Me.btn_CropCancel.Location = New System.Drawing.Point(74, 612)
        Me.btn_CropCancel.Name = "btn_CropCancel"
        Me.btn_CropCancel.Size = New System.Drawing.Size(95, 29)
        Me.btn_CropCancel.TabIndex = 2
        Me.btn_CropCancel.Text = "Cancel"
        Me.btn_CropCancel.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(45, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Poster for : "
        '
        'tb_cropmovtitle
        '
        Me.tb_cropmovtitle.BackColor = System.Drawing.SystemColors.Menu
        Me.tb_cropmovtitle.Location = New System.Drawing.Point(102, 6)
        Me.tb_cropmovtitle.Name = "tb_cropmovtitle"
        Me.tb_cropmovtitle.Size = New System.Drawing.Size(259, 20)
        Me.tb_cropmovtitle.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PicBox)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(490, 553)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = false
        '
        'frmMovPosterCrop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 673)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tb_cropmovtitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_CropCancel)
        Me.Controls.Add(Me.btn_CropAccept)
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(500, 700)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(500, 700)
        Me.Name = "frmMovPosterCrop"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Movie Poster Cropping"
        CType(Me.PicBox,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PicBox As System.Windows.Forms.PictureBox
    Friend WithEvents btn_CropAccept As System.Windows.Forms.Button
    Friend WithEvents btn_CropCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_cropmovtitle As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
