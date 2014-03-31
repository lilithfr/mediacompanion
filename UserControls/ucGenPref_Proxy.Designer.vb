<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucGenPref_Proxy
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cb_prxyEnabled = New System.Windows.Forms.CheckBox()
        Me.tb_prxyIP = New System.Windows.Forms.TextBox()
        Me.tb_prxyPort = New System.Windows.Forms.TextBox()
        Me.tb_prxyUsername = New System.Windows.Forms.TextBox()
        Me.tb_prxyPassword = New System.Windows.Forms.TextBox()
        Me.btnProxySaveChanges = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 348!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.cb_prxyEnabled, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_prxyIP, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_prxyPort, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_prxyUsername, 2, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_prxyPassword, 2, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.btnProxySaveChanges, 2, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 2, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 11
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(621, 476)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(155, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 38)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Proxy Server Settings:"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 113)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Enable Proxy Server"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(43, 144)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Proxy IP address:"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(80, 179)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 29)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Proxy Port:"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(45, 214)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 29)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Proxy UserName:"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(49, 249)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 29)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Proxy Password:"
        '
        'cb_prxyEnabled
        '
        Me.cb_prxyEnabled.AutoSize = true
        Me.cb_prxyEnabled.Location = New System.Drawing.Point(155, 112)
        Me.cb_prxyEnabled.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cb_prxyEnabled.Name = "cb_prxyEnabled"
        Me.cb_prxyEnabled.Size = New System.Drawing.Size(15, 14)
        Me.cb_prxyEnabled.TabIndex = 6
        Me.cb_prxyEnabled.UseVisualStyleBackColor = true
        '
        'tb_prxyIP
        '
        Me.tb_prxyIP.Location = New System.Drawing.Point(155, 141)
        Me.tb_prxyIP.Name = "tb_prxyIP"
        Me.tb_prxyIP.Size = New System.Drawing.Size(303, 20)
        Me.tb_prxyIP.TabIndex = 7
        '
        'tb_prxyPort
        '
        Me.tb_prxyPort.Location = New System.Drawing.Point(155, 176)
        Me.tb_prxyPort.Name = "tb_prxyPort"
        Me.tb_prxyPort.Size = New System.Drawing.Size(100, 20)
        Me.tb_prxyPort.TabIndex = 8
        '
        'tb_prxyUsername
        '
        Me.tb_prxyUsername.Location = New System.Drawing.Point(155, 211)
        Me.tb_prxyUsername.Name = "tb_prxyUsername"
        Me.tb_prxyUsername.Size = New System.Drawing.Size(303, 20)
        Me.tb_prxyUsername.TabIndex = 9
        '
        'tb_prxyPassword
        '
        Me.tb_prxyPassword.Location = New System.Drawing.Point(155, 246)
        Me.tb_prxyPassword.Name = "tb_prxyPassword"
        Me.tb_prxyPassword.Size = New System.Drawing.Size(303, 20)
        Me.tb_prxyPassword.TabIndex = 10
        '
        'btnProxySaveChanges
        '
        Me.btnProxySaveChanges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProxySaveChanges.Location = New System.Drawing.Point(155, 438)
        Me.btnProxySaveChanges.Name = "btnProxySaveChanges"
        Me.btnProxySaveChanges.Size = New System.Drawing.Size(342, 27)
        Me.btnProxySaveChanges.TabIndex = 11
        Me.btnProxySaveChanges.Text = "Apply Changes"
        Me.btnProxySaveChanges.UseVisualStyleBackColor = true
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(155, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(272, 39)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Please ensure your settings are correct before enabling."
        '
        'ucGenPref_Proxy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ucGenPref_Proxy"
        Me.Size = New System.Drawing.Size(621, 476)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cb_prxyEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents tb_prxyIP As System.Windows.Forms.TextBox
    Friend WithEvents tb_prxyPort As System.Windows.Forms.TextBox
    Friend WithEvents tb_prxyUsername As System.Windows.Forms.TextBox
    Friend WithEvents tb_prxyPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnProxySaveChanges As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
