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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbTitle = New System.Windows.Forms.CheckBox()
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbXtraFanart = New System.Windows.Forms.CheckBox()
        Me.CheckBox18 = New System.Windows.Forms.CheckBox()
        Me.CheckBox17 = New System.Windows.Forms.CheckBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.gbOther = New System.Windows.Forms.GroupBox()
        Me.cbDlTrailer = New System.Windows.Forms.CheckBox()
        Me.cbTagsFromKeywords = New System.Windows.Forms.CheckBox()
        Me.cbRenameFolders = New System.Windows.Forms.CheckBox()
        Me.cbFrodo_Fanart_Thumbs = New System.Windows.Forms.CheckBox()
        Me.cbFrodo_Poster_Thumbs = New System.Windows.Forms.CheckBox()
        Me.cbRenameFiles = New System.Windows.Forms.CheckBox()
        Me.CheckBox16 = New System.Windows.Forms.CheckBox()
        Me.CheckBox15 = New System.Windows.Forms.CheckBox()
        Me.CheckBox19 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ttBatchUpdateWizard = New System.Windows.Forms.ToolTip(Me.components)
        Me.cb_ScrapeEmptyTags = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.gbOther.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbTitle)
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
        Me.GroupBox1.Size = New System.Drawing.Size(456, 134)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Select Main Tags to Rescrape"
        '
        'cbTitle
        '
        Me.cbTitle.AutoSize = true
        Me.cbTitle.Location = New System.Drawing.Point(226, 110)
        Me.cbTitle.Name = "cbTitle"
        Me.cbTitle.Size = New System.Drawing.Size(46, 17)
        Me.cbTitle.TabIndex = 28
        Me.cbTitle.Text = "Title"
        Me.cbTitle.UseVisualStyleBackColor = true
        '
        'cbTmdbSetName
        '
        Me.cbTmdbSetName.AutoSize = true
        Me.cbTmdbSetName.Location = New System.Drawing.Point(116, 110)
        Me.cbTmdbSetName.Name = "cbTmdbSetName"
        Me.cbTmdbSetName.Size = New System.Drawing.Size(102, 17)
        Me.cbTmdbSetName.TabIndex = 27
        Me.cbTmdbSetName.Text = "TMDb set name"
        Me.cbTmdbSetName.UseVisualStyleBackColor = true
        '
        'CheckBox20
        '
        Me.CheckBox20.AutoSize = true
        Me.CheckBox20.Location = New System.Drawing.Point(6, 111)
        Me.CheckBox20.Name = "CheckBox20"
        Me.CheckBox20.Size = New System.Drawing.Size(48, 17)
        Me.CheckBox20.TabIndex = 26
        Me.CheckBox20.Text = "Year"
        Me.CheckBox20.UseVisualStyleBackColor = true
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = true
        Me.CheckBox2.Location = New System.Drawing.Point(336, 88)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(50, 17)
        Me.CheckBox2.TabIndex = 25
        Me.CheckBox2.Text = "Stars"
        Me.CheckBox2.UseVisualStyleBackColor = true
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = true
        Me.CheckBox1.Location = New System.Drawing.Point(336, 65)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(62, 17)
        Me.CheckBox1.TabIndex = 21
        Me.CheckBox1.Text = "Country"
        Me.CheckBox1.UseVisualStyleBackColor = true
        '
        'CheckBox22
        '
        Me.CheckBox22.AutoSize = true
        Me.CheckBox22.Location = New System.Drawing.Point(116, 19)
        Me.CheckBox22.Name = "CheckBox22"
        Me.CheckBox22.Size = New System.Drawing.Size(63, 17)
        Me.CheckBox22.TabIndex = 11
        Me.CheckBox22.Text = "Top250"
        Me.CheckBox22.UseVisualStyleBackColor = true
        '
        'CheckBox21
        '
        Me.CheckBox21.AutoSize = true
        Me.CheckBox21.Location = New System.Drawing.Point(226, 42)
        Me.CheckBox21.Name = "CheckBox21"
        Me.CheckBox21.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox21.TabIndex = 16
        Me.CheckBox21.Text = "Trailer"
        Me.CheckBox21.UseVisualStyleBackColor = true
        '
        'CheckBox14
        '
        Me.CheckBox14.AutoSize = true
        Me.CheckBox14.Location = New System.Drawing.Point(336, 42)
        Me.CheckBox14.Name = "CheckBox14"
        Me.CheckBox14.Size = New System.Drawing.Size(56, 17)
        Me.CheckBox14.TabIndex = 17
        Me.CheckBox14.Text = "Studio"
        Me.CheckBox14.UseVisualStyleBackColor = true
        '
        'CheckBox13
        '
        Me.CheckBox13.AutoSize = true
        Me.CheckBox13.Location = New System.Drawing.Point(336, 19)
        Me.CheckBox13.Name = "CheckBox13"
        Me.CheckBox13.Size = New System.Drawing.Size(73, 17)
        Me.CheckBox13.TabIndex = 13
        Me.CheckBox13.Text = "Premiered"
        Me.CheckBox13.UseVisualStyleBackColor = true
        '
        'CheckBox12
        '
        Me.CheckBox12.AutoSize = true
        Me.CheckBox12.Location = New System.Drawing.Point(226, 65)
        Me.CheckBox12.Name = "CheckBox12"
        Me.CheckBox12.Size = New System.Drawing.Size(63, 17)
        Me.CheckBox12.TabIndex = 20
        Me.CheckBox12.Text = "Director"
        Me.CheckBox12.UseVisualStyleBackColor = true
        '
        'CheckBox11
        '
        Me.CheckBox11.AutoSize = true
        Me.CheckBox11.Location = New System.Drawing.Point(226, 88)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(58, 17)
        Me.CheckBox11.TabIndex = 24
        Me.CheckBox11.Text = "Credits"
        Me.CheckBox11.UseVisualStyleBackColor = true
        '
        'CheckBox10
        '
        Me.CheckBox10.AutoSize = true
        Me.CheckBox10.Location = New System.Drawing.Point(7, 88)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox10.TabIndex = 22
        Me.CheckBox10.Text = "Genre"
        Me.CheckBox10.UseVisualStyleBackColor = true
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = true
        Me.CheckBox9.Location = New System.Drawing.Point(7, 65)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox9.TabIndex = 18
        Me.CheckBox9.Text = "MPAA/Cert"
        Me.CheckBox9.UseVisualStyleBackColor = true
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = true
        Me.CheckBox8.Location = New System.Drawing.Point(226, 19)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(65, 17)
        Me.CheckBox8.TabIndex = 12
        Me.CheckBox8.Text = "Runtime"
        Me.CheckBox8.UseVisualStyleBackColor = true
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = true
        Me.CheckBox7.Location = New System.Drawing.Point(116, 88)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(61, 17)
        Me.CheckBox7.TabIndex = 23
        Me.CheckBox7.Text = "Tagline"
        Me.CheckBox7.UseVisualStyleBackColor = true
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = true
        Me.CheckBox6.Location = New System.Drawing.Point(116, 65)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(44, 17)
        Me.CheckBox6.TabIndex = 19
        Me.CheckBox6.Text = "Plot"
        Me.CheckBox6.UseVisualStyleBackColor = true
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = true
        Me.CheckBox5.Location = New System.Drawing.Point(116, 42)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(59, 17)
        Me.CheckBox5.TabIndex = 15
        Me.CheckBox5.Text = "Outline"
        Me.CheckBox5.UseVisualStyleBackColor = true
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = true
        Me.CheckBox4.Location = New System.Drawing.Point(7, 42)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(53, 17)
        Me.CheckBox4.TabIndex = 14
        Me.CheckBox4.Text = "Votes"
        Me.CheckBox4.UseVisualStyleBackColor = true
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = true
        Me.CheckBox3.Location = New System.Drawing.Point(7, 19)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(57, 17)
        Me.CheckBox3.TabIndex = 10
        Me.CheckBox3.Text = "Rating"
        Me.CheckBox3.UseVisualStyleBackColor = true
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(14, 390)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 50
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = true
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbXtraFanart)
        Me.GroupBox3.Controls.Add(Me.CheckBox18)
        Me.GroupBox3.Controls.Add(Me.CheckBox17)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 296)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(455, 88)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Fanart && Posters"
        '
        'cbXtraFanart
        '
        Me.cbXtraFanart.AutoSize = true
        Me.cbXtraFanart.Location = New System.Drawing.Point(7, 65)
        Me.cbXtraFanart.Name = "cbXtraFanart"
        Me.cbXtraFanart.Size = New System.Drawing.Size(342, 17)
        Me.cbXtraFanart.TabIndex = 43
        Me.cbXtraFanart.Text = "Attempt to Download Extra Thumbs/Fanart if Movies are in Folders."
        Me.cbXtraFanart.UseVisualStyleBackColor = true
        '
        'CheckBox18
        '
        Me.CheckBox18.AutoSize = true
        Me.CheckBox18.Location = New System.Drawing.Point(7, 43)
        Me.CheckBox18.Name = "CheckBox18"
        Me.CheckBox18.Size = New System.Drawing.Size(392, 17)
        Me.CheckBox18.TabIndex = 42
        Me.CheckBox18.Text = "Attempt To Locate && Download Posters For Movies That Are Missing A Poster"
        Me.CheckBox18.UseVisualStyleBackColor = true
        '
        'CheckBox17
        '
        Me.CheckBox17.AutoSize = true
        Me.CheckBox17.Location = New System.Drawing.Point(7, 20)
        Me.CheckBox17.Name = "CheckBox17"
        Me.CheckBox17.Size = New System.Drawing.Size(403, 17)
        Me.CheckBox17.TabIndex = 41
        Me.CheckBox17.Text = "Attempt To Locate && Download Fanart For Movies That Are Missing A Backdrop"
        Me.CheckBox17.UseVisualStyleBackColor = true
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(394, 390)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 60
        Me.btnStart.Text = "Start Update"
        Me.btnStart.UseVisualStyleBackColor = true
        '
        'gbOther
        '
        Me.gbOther.Controls.Add(Me.cbDlTrailer)
        Me.gbOther.Controls.Add(Me.cbTagsFromKeywords)
        Me.gbOther.Controls.Add(Me.cbRenameFolders)
        Me.gbOther.Controls.Add(Me.cbFrodo_Fanart_Thumbs)
        Me.gbOther.Controls.Add(Me.cbFrodo_Poster_Thumbs)
        Me.gbOther.Controls.Add(Me.cbRenameFiles)
        Me.gbOther.Controls.Add(Me.CheckBox16)
        Me.gbOther.Controls.Add(Me.CheckBox15)
        Me.gbOther.Controls.Add(Me.CheckBox19)
        Me.gbOther.Location = New System.Drawing.Point(15, 205)
        Me.gbOther.Name = "gbOther"
        Me.gbOther.Size = New System.Drawing.Size(456, 85)
        Me.gbOther.TabIndex = 0
        Me.gbOther.TabStop = false
        Me.gbOther.Text = "Other"
        '
        'cbDlTrailer
        '
        Me.cbDlTrailer.AutoSize = true
        Me.cbDlTrailer.Location = New System.Drawing.Point(291, 64)
        Me.cbDlTrailer.Name = "cbDlTrailer"
        Me.cbDlTrailer.Size = New System.Drawing.Size(159, 17)
        Me.cbDlTrailer.TabIndex = 39
        Me.cbDlTrailer.Text = "Download Trailer if Url exists"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbDlTrailer, "Will only download trailer if"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Valid URL is already present")
        Me.cbDlTrailer.UseVisualStyleBackColor = true
        '
        'cbTagsFromKeywords
        '
        Me.cbTagsFromKeywords.AutoSize = true
        Me.cbTagsFromKeywords.Location = New System.Drawing.Point(123, 64)
        Me.cbTagsFromKeywords.Name = "cbTagsFromKeywords"
        Me.cbTagsFromKeywords.Size = New System.Drawing.Size(158, 17)
        Me.cbTagsFromKeywords.TabIndex = 38
        Me.cbTagsFromKeywords.Text = "Scrape Tags from keywords"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbTagsFromKeywords, "Will scrape keywords from current selected scraper"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"and populate them into movie "& _ 
        "Tags.")
        Me.cbTagsFromKeywords.UseVisualStyleBackColor = true
        '
        'cbRenameFolders
        '
        Me.cbRenameFolders.AutoSize = true
        Me.cbRenameFolders.Location = New System.Drawing.Point(7, 64)
        Me.cbRenameFolders.Name = "cbRenameFolders"
        Me.cbRenameFolders.Size = New System.Drawing.Size(103, 17)
        Me.cbRenameFolders.TabIndex = 37
        Me.cbRenameFolders.Text = "Rename Folders"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbRenameFolders, "Renaming of Movie Folders as per 'Folder Renaming Pattern'"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Option to rename fold"& _ 
        "ers during Autoscrape does not need to"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"be enabled, but Folder name pattern MUST"& _ 
        " be valid.")
        Me.cbRenameFolders.UseVisualStyleBackColor = true
        '
        'cbFrodo_Fanart_Thumbs
        '
        Me.cbFrodo_Fanart_Thumbs.AutoSize = true
        Me.cbFrodo_Fanart_Thumbs.Location = New System.Drawing.Point(291, 41)
        Me.cbFrodo_Fanart_Thumbs.Name = "cbFrodo_Fanart_Thumbs"
        Me.cbFrodo_Fanart_Thumbs.Size = New System.Drawing.Size(127, 17)
        Me.cbFrodo_Fanart_Thumbs.TabIndex = 36
        Me.cbFrodo_Fanart_Thumbs.Text = "Frodo Fanart Thumbs"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbFrodo_Fanart_Thumbs, "Frodo support must be enabled before this option can be checked")
        Me.cbFrodo_Fanart_Thumbs.UseVisualStyleBackColor = true
        '
        'cbFrodo_Poster_Thumbs
        '
        Me.cbFrodo_Poster_Thumbs.AutoSize = true
        Me.cbFrodo_Poster_Thumbs.Location = New System.Drawing.Point(123, 41)
        Me.cbFrodo_Poster_Thumbs.Name = "cbFrodo_Poster_Thumbs"
        Me.cbFrodo_Poster_Thumbs.Size = New System.Drawing.Size(127, 17)
        Me.cbFrodo_Poster_Thumbs.TabIndex = 35
        Me.cbFrodo_Poster_Thumbs.Text = "Frodo Poster Thumbs"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbFrodo_Poster_Thumbs, "Frodo support must be enabled before this option can be checked")
        Me.cbFrodo_Poster_Thumbs.UseVisualStyleBackColor = true
        '
        'cbRenameFiles
        '
        Me.cbRenameFiles.AutoSize = true
        Me.cbRenameFiles.Location = New System.Drawing.Point(7, 41)
        Me.cbRenameFiles.Name = "cbRenameFiles"
        Me.cbRenameFiles.Size = New System.Drawing.Size(90, 17)
        Me.cbRenameFiles.TabIndex = 34
        Me.cbRenameFiles.Text = "Rename Files"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cbRenameFiles, "Renames movie files to match the selected 'Name Mode' and your 'Movie Renaming' p"& _ 
        "attern (Must be enabled). "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Conflicting options 'Save files as 'movie.nfo...' (A"& _ 
        "dvanced tab) must be disabled.")
        Me.cbRenameFiles.UseVisualStyleBackColor = true
        '
        'CheckBox16
        '
        Me.CheckBox16.AutoSize = true
        Me.CheckBox16.Location = New System.Drawing.Point(291, 19)
        Me.CheckBox16.Name = "CheckBox16"
        Me.CheckBox16.Size = New System.Drawing.Size(141, 17)
        Me.CheckBox16.TabIndex = 33
        Me.CheckBox16.Text = "Rescrape nfo poster urls"
        Me.CheckBox16.UseVisualStyleBackColor = true
        '
        'CheckBox15
        '
        Me.CheckBox15.AutoSize = true
        Me.CheckBox15.Location = New System.Drawing.Point(7, 19)
        Me.CheckBox15.Name = "CheckBox15"
        Me.CheckBox15.Size = New System.Drawing.Size(105, 17)
        Me.CheckBox15.TabIndex = 31
        Me.CheckBox15.Text = "Rescrape Actors"
        Me.CheckBox15.UseVisualStyleBackColor = true
        '
        'CheckBox19
        '
        Me.CheckBox19.AutoSize = true
        Me.CheckBox19.Location = New System.Drawing.Point(123, 19)
        Me.CheckBox19.Name = "CheckBox19"
        Me.CheckBox19.Size = New System.Drawing.Size(131, 17)
        Me.CheckBox19.TabIndex = 32
        Me.CheckBox19.Text = "Rescrape Media Tags"
        Me.CheckBox19.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(440, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This form can be used to rescrape all movies, you can select which tags are updat"& _ 
    "ed below,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"all other tags will remain unchanged."
        '
        'ttBatchUpdateWizard
        '
        Me.ttBatchUpdateWizard.AutoPopDelay = 10000
        Me.ttBatchUpdateWizard.InitialDelay = 500
        Me.ttBatchUpdateWizard.ReshowDelay = 100
        Me.ttBatchUpdateWizard.ShowAlways = true
        '
        'cb_ScrapeEmptyTags
        '
        Me.cb_ScrapeEmptyTags.AutoSize = true
        Me.cb_ScrapeEmptyTags.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cb_ScrapeEmptyTags.Location = New System.Drawing.Point(48, 181)
        Me.cb_ScrapeEmptyTags.Name = "cb_ScrapeEmptyTags"
        Me.cb_ScrapeEmptyTags.Size = New System.Drawing.Size(380, 17)
        Me.cb_ScrapeEmptyTags.TabIndex = 29
        Me.cb_ScrapeEmptyTags.Text = "Scrape only empty tags (Excludes TMDB set name and Trailer)"
        Me.ttBatchUpdateWizard.SetToolTip(Me.cb_ScrapeEmptyTags, "Selecting will re-scrape all selected movies, but ONLY"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"filling fields that are e"& _ 
        "mpty."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"This excludes TMDB set name.  Select this separately if"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"required.")
        Me.cb_ScrapeEmptyTags.UseVisualStyleBackColor = true
        '
        'frmBatchScraper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 421)
        Me.ControlBox = false
        Me.Controls.Add(Me.cb_ScrapeEmptyTags)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.gbOther)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = true
        Me.MaximumSize = New System.Drawing.Size(493, 455)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(493, 455)
        Me.Name = "frmBatchScraper"
        Me.Text = "Batch Update Wizard"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.gbOther.ResumeLayout(false)
        Me.gbOther.PerformLayout
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
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox18 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox17 As System.Windows.Forms.CheckBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents gbOther As System.Windows.Forms.GroupBox
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
    Friend WithEvents cbFrodo_Fanart_Thumbs As System.Windows.Forms.CheckBox
    Friend WithEvents cbFrodo_Poster_Thumbs As System.Windows.Forms.CheckBox
    Friend WithEvents cbTitle As System.Windows.Forms.CheckBox
    Friend WithEvents cbXtraFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbRenameFolders As System.Windows.Forms.CheckBox
    Friend WithEvents cb_ScrapeEmptyTags As System.Windows.Forms.CheckBox
    Friend WithEvents cbTagsFromKeywords As System.Windows.Forms.CheckBox
    Friend WithEvents cbDlTrailer As System.Windows.Forms.CheckBox
End Class
