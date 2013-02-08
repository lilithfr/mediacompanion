<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchScraper
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBatchScraper))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbTmdbSetName = New System.Windows.Forms.CheckBox()
        Me.CheckBox20 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox22 = New System.Windows.Forms.CheckBox()
        Me.CheckBox21 = New System.Windows.Forms.CheckBox()
        Me.CheckBox14 = New System.Windows.Forms.CheckBox()
        Me.CheckBox13 = New System.Windows.Forms.CheckBox()
        Me.CheckBox12 = New System.Windows.Forms.CheckBox()
        Me.CheckBox11 = New System.Windows.Forms.CheckBox()
        Me.CheckBox10 = New System.Windows.Forms.CheckBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBox18 = New System.Windows.Forms.CheckBox()
        Me.CheckBox17 = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbRenameFiles = New System.Windows.Forms.CheckBox()
        Me.CheckBox16 = New System.Windows.Forms.CheckBox()
        Me.CheckBox15 = New System.Windows.Forms.CheckBox()
        Me.CheckBox19 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ttBatchUpdateWizard = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbTmdbSetName)
        Me.GroupBox1.Controls.Add(Me.CheckBox20)
        Me.GroupBox1.Controls.Add(Me.CheckBox2)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.CheckBox22)
        Me.GroupBox1.Controls.Add(Me.CheckBox21)
        Me.GroupBox1.Controls.Add(Me.CheckBox14)
        Me.GroupBox1.Controls.Add(Me.CheckBox13)
        Me.GroupBox1.Controls.Add(Me.CheckBox12)
        Me.GroupBox1.Controls.Add(Me.CheckBox11)
        Me.GroupBox1.Controls.Add(Me.CheckBox10)
        Me.GroupBox1.Controls.Add(Me.CheckBox9)
        Me.GroupBox1.Controls.Add(Me.CheckBox8)
        Me.GroupBox1.Controls.Add(Me.CheckBox7)
        Me.GroupBox1.Controls.Add(Me.CheckBox6)
        Me.GroupBox1.Controls.Add(Me.CheckBox5)
        Me.GroupBox1.Controls.Add(Me.CheckBox4)
        Me.GroupBox1.Controls.Add(Me.CheckBox3)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(456, 133)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Select Main Tags to Rescrape"
        '
        'cbTmdbSetName
        '
        Me.cbTmdbSetName.AutoSize = true
        Me.cbTmdbSetName.Location = New System.Drawing.Point(116, 110)
        Me.cbTmdbSetName.Name = "cbTmdbSetName"
        Me.cbTmdbSetName.Size = New System.Drawing.Size(102, 17)
        Me.cbTmdbSetName.TabIndex = 19
        Me.cbTmdbSetName.Text = "TMDb set name"
        Me.cbTmdbSetName.UseVisualStyleBackColor = true
        '
        'CheckBox20
        '
        Me.CheckBox20.AutoSize = true
        Me.CheckBox20.Location = New System.Drawing.Point(6, 111)
        Me.CheckBox20.Name = "CheckBox20"
        Me.CheckBox20.Size = New System.Drawing.Size(48, 17)
        Me.CheckBox20.TabIndex = 18
        Me.CheckBox20.Text = "Year"
        Me.CheckBox20.UseVisualStyleBackColor = true
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = true
        Me.CheckBox2.Location = New System.Drawing.Point(336, 88)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(50, 17)
        Me.CheckBox2.TabIndex = 17
        Me.CheckBox2.Text = "Stars"
        Me.CheckBox2.UseVisualStyleBackColor = true
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = true
        Me.CheckBox1.Location = New System.Drawing.Point(336, 65)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(62, 17)
        Me.CheckBox1.TabIndex = 16
        Me.CheckBox1.Text = "Country"
        Me.CheckBox1.UseVisualStyleBackColor = true
        '
        'CheckBox22
        '
        Me.CheckBox22.AutoSize = true
        Me.CheckBox22.Location = New System.Drawing.Point(116, 19)
        Me.CheckBox22.Name = "CheckBox22"
        Me.CheckBox22.Size = New System.Drawing.Size(63, 17)
        Me.CheckBox22.TabIndex = 15
        Me.CheckBox22.Text = "Top250"
        Me.CheckBox22.UseVisualStyleBackColor = true
        '
        'CheckBox21
        '
        Me.CheckBox21.AutoSize = true
        Me.CheckBox21.Location = New System.Drawing.Point(226, 42)
        Me.CheckBox21.Name = "CheckBox21"
        Me.CheckBox21.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox21.TabIndex = 14
        Me.CheckBox21.Text = "Trailer"
        Me.CheckBox21.UseVisualStyleBackColor = true
        '
        'CheckBox14
        '
        Me.CheckBox14.AutoSize = true
        Me.CheckBox14.Location = New System.Drawing.Point(336, 42)
        Me.CheckBox14.Name = "CheckBox14"
        Me.CheckBox14.Size = New System.Drawing.Size(56, 17)
        Me.CheckBox14.TabIndex = 13
        Me.CheckBox14.Text = "Studio"
        Me.CheckBox14.UseVisualStyleBackColor = true
        '
        'CheckBox13
        '
        Me.CheckBox13.AutoSize = true
        Me.CheckBox13.Location = New System.Drawing.Point(336, 19)
        Me.CheckBox13.Name = "CheckBox13"
        Me.CheckBox13.Size = New System.Drawing.Size(73, 17)
        Me.CheckBox13.TabIndex = 12
        Me.CheckBox13.Text = "Premiered"
        Me.CheckBox13.UseVisualStyleBackColor = true
        '
        'CheckBox12
        '
        Me.CheckBox12.AutoSize = true
        Me.CheckBox12.Location = New System.Drawing.Point(226, 65)
        Me.CheckBox12.Name = "CheckBox12"
        Me.CheckBox12.Size = New System.Drawing.Size(63, 17)
        Me.CheckBox12.TabIndex = 11
        Me.CheckBox12.Text = "Director"
        Me.CheckBox12.UseVisualStyleBackColor = true
        '
        'CheckBox11
        '
        Me.CheckBox11.AutoSize = true
        Me.CheckBox11.Location = New System.Drawing.Point(226, 88)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(58, 17)
        Me.CheckBox11.TabIndex = 10
        Me.CheckBox11.Text = "Credits"
        Me.CheckBox11.UseVisualStyleBackColor = true
        '
        'CheckBox10
        '
        Me.CheckBox10.AutoSize = true
        Me.CheckBox10.Location = New System.Drawing.Point(7, 88)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox10.TabIndex = 9
        Me.CheckBox10.Text = "Genre"
        Me.CheckBox10.UseVisualStyleBackColor = true
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = true
        Me.CheckBox9.Location = New System.Drawing.Point(7, 65)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox9.TabIndex = 8
        Me.CheckBox9.Text = "MPAA/Cert"
        Me.CheckBox9.UseVisualStyleBackColor = true
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = true
        Me.CheckBox8.Location = New System.Drawing.Point(226, 19)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(65, 17)
        Me.CheckBox8.TabIndex = 7
        Me.CheckBox8.Text = "Runtime"
        Me.CheckBox8.UseVisualStyleBackColor = true
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = true
        Me.CheckBox7.Location = New System.Drawing.Point(116, 88)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(61, 17)
        Me.CheckBox7.TabIndex = 6
        Me.CheckBox7.Text = "Tagline"
        Me.CheckBox7.UseVisualStyleBackColor = true
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = true
        Me.CheckBox6.Location = New System.Drawing.Point(116, 65)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(44, 17)
        Me.CheckBox6.TabIndex = 5
        Me.CheckBox6.Text = "Plot"
        Me.CheckBox6.UseVisualStyleBackColor = true
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = true
        Me.CheckBox5.Location = New System.Drawing.Point(116, 42)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(59, 17)
        Me.CheckBox5.TabIndex = 4
        Me.CheckBox5.Text = "Outline"
        Me.CheckBox5.UseVisualStyleBackColor = true
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = true
        Me.CheckBox4.Location = New System.Drawing.Point(7, 42)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(53, 17)
        Me.CheckBox4.TabIndex = 3
        Me.CheckBox4.Text = "Votes"
        Me.CheckBox4.UseVisualStyleBackColor = true
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = true
        Me.CheckBox3.Location = New System.Drawing.Point(7, 19)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(57, 17)
        Me.CheckBox3.TabIndex = 2
        Me.CheckBox3.Text = "Rating"
        Me.CheckBox3.UseVisualStyleBackColor = true
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 326)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 32
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckBox18)
        Me.GroupBox3.Controls.Add(Me.CheckBox17)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 253)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(455, 67)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Fanart && Posters"
        '
        'CheckBox18
        '
        Me.CheckBox18.AutoSize = true
        Me.CheckBox18.Location = New System.Drawing.Point(7, 43)
        Me.CheckBox18.Name = "CheckBox18"
        Me.CheckBox18.Size = New System.Drawing.Size(392, 17)
        Me.CheckBox18.TabIndex = 1
        Me.CheckBox18.Text = "Attempt To Locate && Download Posters For Movies That Are Missing A Poster"
        Me.CheckBox18.UseVisualStyleBackColor = true
        '
        'CheckBox17
        '
        Me.CheckBox17.AutoSize = true
        Me.CheckBox17.Location = New System.Drawing.Point(7, 20)
        Me.CheckBox17.Name = "CheckBox17"
        Me.CheckBox17.Size = New System.Drawing.Size(403, 17)
        Me.CheckBox17.TabIndex = 0
        Me.CheckBox17.Text = "Attempt To Locate && Download Fanart For Movies That Are Missing A Backdrop"
        Me.CheckBox17.UseVisualStyleBackColor = true
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(395, 326)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 33
        Me.Button2.Text = "Start Update"
        Me.Button2.UseVisualStyleBackColor = true
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cbRenameFiles)
        Me.GroupBox4.Controls.Add(Me.CheckBox16)
        Me.GroupBox4.Controls.Add(Me.CheckBox15)
        Me.GroupBox4.Controls.Add(Me.CheckBox19)
        Me.GroupBox4.Location = New System.Drawing.Point(14, 180)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(456, 67)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = false
        Me.GroupBox4.Text = "Other"
        '
        'cbRenameFiles
        '
        Me.cbRenameFiles.AutoSize = true
        Me.cbRenameFiles.Location = New System.Drawing.Point(7, 41)
        Me.cbRenameFiles.Name = "cbRenameFiles"
        Me.cbRenameFiles.Size = New System.Drawing.Size(90, 17)
        Me.cbRenameFiles.TabIndex = 16
        Me.cbRenameFiles.Text = "Rename Files"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbRenameFiles, resources.GetString("cbRenameFiles.ToolTip"))
        Me.cbRenameFiles.UseVisualStyleBackColor = true
        '
        'CheckBox16
        '
        Me.CheckBox16.AutoSize = true
        Me.CheckBox16.Location = New System.Drawing.Point(297, 19)
        Me.CheckBox16.Name = "CheckBox16"
        Me.CheckBox16.Size = New System.Drawing.Size(141, 17)
        Me.CheckBox16.TabIndex = 15
        Me.CheckBox16.Text = "Rescrape nfo poster urls"
        Me.CheckBox16.UseVisualStyleBackColor = true
        '
        'CheckBox15
        '
        Me.CheckBox15.AutoSize = true
        Me.CheckBox15.Location = New System.Drawing.Point(7, 19)
        Me.CheckBox15.Name = "CheckBox15"
        Me.CheckBox15.Size = New System.Drawing.Size(105, 17)
        Me.CheckBox15.TabIndex = 14
        Me.CheckBox15.Text = "Rescrape Actors"
        Me.CheckBox15.UseVisualStyleBackColor = true
        '
        'CheckBox19
        '
        Me.CheckBox19.AutoSize = true
        Me.CheckBox19.Location = New System.Drawing.Point(139, 19)
        Me.CheckBox19.Name = "CheckBox19"
        Me.CheckBox19.Size = New System.Drawing.Size(131, 17)
        Me.CheckBox19.TabIndex = 0
        Me.CheckBox19.Text = "Rescrape Media Tags"
        Me.CheckBox19.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(440, 26)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "This form can be used to rescrape all movies, you can select which tags are updat"& _ 
    "ed below,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"all other tags will remain unchanged."
        '
        'frmBatchScraper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 361)
        Me.ControlBox = false
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Label1)
        Me.MaximumSize = New System.Drawing.Size(493, 400)
        Me.MinimumSize = New System.Drawing.Size(493, 400)
        Me.Name = "frmBatchScraper"
        Me.Text = "Batch Update Wizard"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox22 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox21 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox14 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox13 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox12 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox18 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox17 As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox19 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox15 As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox16 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox20 As System.Windows.Forms.CheckBox
    Friend WithEvents cbTmdbSetName As System.Windows.Forms.CheckBox
    Friend WithEvents cbRenameFiles As System.Windows.Forms.CheckBox
    Friend WithEvents ttBatchUpdateWizard As System.Windows.Forms.ToolTip
End Class
