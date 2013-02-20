<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTVShowBrowser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTVShowBrowser))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioButton8 = New System.Windows.Forms.RadioButton()
        Me.RadioButton7 = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(332, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "The TV Show Search Will Be Based On The Text Entry" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Below, Edit if Neccesary And " & _
    "Press The Search Button"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.Location = New System.Drawing.Point(15, 63)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(260, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(281, 63)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(63, 20)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Control
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 16
        Me.ListBox1.Location = New System.Drawing.Point(15, 89)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(329, 244)
        Me.ListBox1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 432)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(354, 64)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.CheckBox3)
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Location = New System.Drawing.Point(360, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(413, 514)
        Me.Panel1.TabIndex = 5
        Me.Panel1.Visible = False
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(203, 396)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(195, 17)
        Me.CheckBox3.TabIndex = 11
        Me.CheckBox3.Text = "Download Season Posters/Banners"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(203, 419)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(107, 17)
        Me.CheckBox2.TabIndex = 10
        Me.CheckBox2.Text = "Download Fanart"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(203, 373)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(146, 17)
        Me.CheckBox1.TabIndex = 9
        Me.CheckBox1.Text = "Download Poster/Banner"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.RadioButton8)
        Me.GroupBox4.Controls.Add(Me.RadioButton7)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(11, 366)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(185, 70)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Art Style"
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Location = New System.Drawing.Point(6, 44)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(69, 20)
        Me.RadioButton8.TabIndex = 1
        Me.RadioButton8.TabStop = True
        Me.RadioButton8.Text = "Banner"
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Location = New System.Drawing.Point(6, 21)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(65, 20)
        Me.RadioButton7.TabIndex = 0
        Me.RadioButton7.TabStop = True
        Me.RadioButton7.Text = "Poster"
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 331)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(373, 32)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "If you wish, you can also change the following settings from the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "default below"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.ListBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(405, 325)
        Me.Panel2.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RadioButton6)
        Me.GroupBox3.Controls.Add(Me.RadioButton5)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(208, 237)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(185, 70)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "episode.nfo actor source"
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(7, 44)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(59, 20)
        Me.RadioButton6.TabIndex = 1
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "IMDB"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(7, 22)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(63, 20)
        Me.RadioButton5.TabIndex = 0
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "TVDB"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.RadioButton4)
        Me.GroupBox2.Controls.Add(Me.RadioButton3)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(208, 161)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(185, 70)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "tvshow.nfo actor source"
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(6, 44)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(59, 20)
        Me.RadioButton4.TabIndex = 1
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "IMDB"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(6, 21)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(63, 20)
        Me.RadioButton3.TabIndex = 0
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "TVDB"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.BackColor = System.Drawing.SystemColors.Control
        Me.ListBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 16
        Me.ListBox2.Location = New System.Drawing.Point(3, 79)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(188, 228)
        Me.ListBox2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(208, 85)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(185, 70)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Episode Sort Order"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(6, 42)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(55, 20)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "DVD"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(6, 21)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(68, 20)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Default"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(371, 32)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "The options in this panel can be changed from the default and" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  will be applied " & _
    "only to this TV Show and any episodes it has"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 16)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "available]"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(222, 16)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Default Language for TV Shows is :- "
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(110, 481)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(190, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Scrape Show with Selected Options"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(15, 339)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(329, 90)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'frmTVShowBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 521)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTVShowBrowser"
        Me.Text = "TVShowBrowser"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton6 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents RadioButton8 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton7 As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
End Class
