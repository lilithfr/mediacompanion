<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExceptions
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
        Me.pictSadFace = New System.Windows.Forms.PictureBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.txtExceptionTrace = New System.Windows.Forms.TextBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.lblExceptionTrace = New System.Windows.Forms.Label()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.lnkCodeplex = New System.Windows.Forms.LinkLabel()
        Me.lblMessageEnd = New System.Windows.Forms.Label()
        CType(Me.pictSadFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictSadFace
        '
        Me.pictSadFace.Image = Global.Media_Companion.My.Resources.Resources.Sad
        Me.pictSadFace.Location = New System.Drawing.Point(290, 37)
        Me.pictSadFace.Name = "pictSadFace"
        Me.pictSadFace.Size = New System.Drawing.Size(245, 225)
        Me.pictSadFace.TabIndex = 0
        Me.pictSadFace.TabStop = False
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(23, 282)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(728, 48)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Something went wrong and application needs to close. Why not file a bug to help u" & _
            "s improve? Create a new issue at "
        '
        'txtExceptionTrace
        '
        Me.txtExceptionTrace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExceptionTrace.Location = New System.Drawing.Point(25, 362)
        Me.txtExceptionTrace.Multiline = True
        Me.txtExceptionTrace.Name = "txtExceptionTrace"
        Me.txtExceptionTrace.Size = New System.Drawing.Size(726, 85)
        Me.txtExceptionTrace.TabIndex = 3
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(121, 337)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(75, 23)
        Me.btnCopy.TabIndex = 2
        Me.btnCopy.Text = "&Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'lblExceptionTrace
        '
        Me.lblExceptionTrace.AutoSize = True
        Me.lblExceptionTrace.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExceptionTrace.Location = New System.Drawing.Point(22, 342)
        Me.lblExceptionTrace.Name = "lblExceptionTrace"
        Me.lblExceptionTrace.Size = New System.Drawing.Size(94, 15)
        Me.lblExceptionTrace.TabIndex = 4
        Me.lblExceptionTrace.Text = "Exception trace:"
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(631, 486)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(97, 23)
        Me.btnQuit.TabIndex = 4
        Me.btnQuit.Text = "&Quit Application"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'lnkCodeplex
        '
        Me.lnkCodeplex.AutoSize = True
        Me.lnkCodeplex.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCodeplex.Location = New System.Drawing.Point(119, 294)
        Me.lnkCodeplex.Name = "lnkCodeplex"
        Me.lnkCodeplex.Size = New System.Drawing.Size(355, 12)
        Me.lnkCodeplex.TabIndex = 1
        Me.lnkCodeplex.TabStop = True
        Me.lnkCodeplex.Text = "http://mediacompanion.codeplex.com/WorkItem/Create"
        '
        'lblMessageEnd
        '
        Me.lblMessageEnd.AutoSize = True
        Me.lblMessageEnd.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessageEnd.Location = New System.Drawing.Point(480, 294)
        Me.lblMessageEnd.Name = "lblMessageEnd"
        Me.lblMessageEnd.Size = New System.Drawing.Size(257, 12)
        Me.lblMessageEnd.TabIndex = 5
        Me.lblMessageEnd.Text = "and paste the exception trace below."
        '
        'frmExceptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(776, 521)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblMessageEnd)
        Me.Controls.Add(Me.lnkCodeplex)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.lblExceptionTrace)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.txtExceptionTrace)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pictSadFace)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExceptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ooops!!"
        CType(Me.pictSadFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pictSadFace As System.Windows.Forms.PictureBox
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents txtExceptionTrace As System.Windows.Forms.TextBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents lblExceptionTrace As System.Windows.Forms.Label
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents lnkCodeplex As System.Windows.Forms.LinkLabel
    Friend WithEvents lblMessageEnd As System.Windows.Forms.Label
End Class
