<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucMusicVideo
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
        Me.components = New System.ComponentModel.Container()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.tPMainMV = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSearchNew = New System.Windows.Forms.Button()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.PcBxPoster = New System.Windows.Forms.PictureBox()
        Me.txtFullpath = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lstBxMainList = New System.Windows.Forms.ListBox()
        Me.PcBxMusicVideoScreenShot = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtArtist = New System.Windows.Forms.TextBox()
        Me.txtDirector = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAlbum = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStudio = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtGenre = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.btnMVPlay = New System.Windows.Forms.Button()
        Me.tPScreenshotMV = New System.Windows.Forms.TabPage()
        Me.btnGoogleSearch = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnPasteFromClipboard = New System.Windows.Forms.Button()
        Me.btnSaveCrop = New System.Windows.Forms.Button()
        Me.btnCropReset = New System.Windows.Forms.Button()
        Me.btnCrop = New System.Windows.Forms.Button()
        Me.txtScreenshotTime = New System.Windows.Forms.MaskedTextBox()
        Me.btnScreenshotMinus = New System.Windows.Forms.Button()
        Me.btnScreenshotPlus = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnCreateScreenshot = New System.Windows.Forms.Button()
        Me.pcBxScreenshot = New System.Windows.Forms.PictureBox()
        Me.tPPosterScrape = New System.Windows.Forms.TabPage()
        Me.btnGoogleSearchPoster = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnPosterPaste = New System.Windows.Forms.Button()
        Me.btnPosterSave = New System.Windows.Forms.Button()
        Me.btnPosterReset = New System.Windows.Forms.Button()
        Me.btnPosterCrop = New System.Windows.Forms.Button()
        Me.pcBxSinglePoster = New System.Windows.Forms.PictureBox()
        Me.tPManualScrape = New System.Windows.Forms.TabPage()
        Me.chkBxOverWriteArt = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnManualScrape = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.tPPref = New System.Windows.Forms.TabPage()
        Me.chlstBoxFolders = New System.Windows.Forms.CheckedListBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnRemoveFolder = New System.Windows.Forms.Button()
        Me.btnAddFolderPath = New System.Windows.Forms.Button()
        Me.tbFolderPath = New System.Windows.Forms.TextBox()
        Me.btnBrowseFolders = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lstBoxFolders = New System.Windows.Forms.ListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControlMain.SuspendLayout
        Me.tPMainMV.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.PcBxPoster,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PcBxMusicVideoScreenShot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tPScreenshotMV.SuspendLayout
        CType(Me.pcBxScreenshot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tPPosterScrape.SuspendLayout
        CType(Me.pcBxSinglePoster,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tPManualScrape.SuspendLayout
        Me.tPPref.SuspendLayout
        Me.SuspendLayout
        '
        'TabControlMain
        '
        Me.TabControlMain.Controls.Add(Me.tPMainMV)
        Me.TabControlMain.Controls.Add(Me.tPScreenshotMV)
        Me.TabControlMain.Controls.Add(Me.tPPosterScrape)
        Me.TabControlMain.Controls.Add(Me.tPManualScrape)
        Me.TabControlMain.Controls.Add(Me.tPPref)
        Me.TabControlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlMain.Location = New System.Drawing.Point(0, 0)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(975, 604)
        Me.TabControlMain.TabIndex = 0
        '
        'tPMainMV
        '
        Me.tPMainMV.BackColor = System.Drawing.Color.LightGray
        Me.tPMainMV.Controls.Add(Me.TableLayoutPanel1)
        Me.tPMainMV.ForeColor = System.Drawing.Color.Black
        Me.tPMainMV.Location = New System.Drawing.Point(4, 22)
        Me.tPMainMV.Name = "tPMainMV"
        Me.tPMainMV.Padding = New System.Windows.Forms.Padding(3)
        Me.tPMainMV.Size = New System.Drawing.Size(967, 578)
        Me.tPMainMV.TabIndex = 0
        Me.tPMainMV.Text = "Main Browser"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 14
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnSearchNew, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtFilter, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.PcBxPoster, 5, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtFullpath, 6, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.btnRefresh, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSave, 12, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 5, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPlot, 6, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 5, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.txtTitle, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lstBxMainList, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.PcBxMusicVideoScreenShot, 10, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 5, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 5, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.txtArtist, 6, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.txtDirector, 6, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 5, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.txtYear, 6, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 8, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAlbum, 9, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 8, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.txtStudio, 9, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.Label11, 8, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.txtGenre, 9, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 8, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.txtRuntime, 9, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.btnMVPlay, 11, 14)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 18
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(961, 572)
        Me.TableLayoutPanel1.TabIndex = 53
        '
        'btnSearchNew
        '
        Me.btnSearchNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnSearchNew.Image = Global.Media_Companion.My.Resources.Resources.new2
        Me.btnSearchNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchNew.Location = New System.Drawing.Point(11, 11)
        Me.btnSearchNew.Name = "btnSearchNew"
        Me.btnSearchNew.Size = New System.Drawing.Size(122, 33)
        Me.btnSearchNew.TabIndex = 25
        Me.btnSearchNew.Text = "Search  "
        Me.btnSearchNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnSearchNew, "Search Folders For New"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"     Music Videos and"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"         Scrape Data")
        Me.btnSearchNew.UseVisualStyleBackColor = true
        '
        'txtFilter
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtFilter, 3)
        Me.txtFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilter.Location = New System.Drawing.Point(11, 59)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(269, 26)
        Me.txtFilter.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.txtFilter, "Text Filter")
        '
        'PcBxPoster
        '
        Me.PcBxPoster.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.PcBxPoster, 5)
        Me.PcBxPoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PcBxPoster.Location = New System.Drawing.Point(299, 59)
        Me.PcBxPoster.Name = "PcBxPoster"
        Me.TableLayoutPanel1.SetRowSpan(Me.PcBxPoster, 4)
        Me.PcBxPoster.Size = New System.Drawing.Size(387, 219)
        Me.PcBxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PcBxPoster.TabIndex = 52
        Me.PcBxPoster.TabStop = false
        '
        'txtFullpath
        '
        Me.txtFullpath.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtFullpath, 2)
        Me.txtFullpath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFullpath.Location = New System.Drawing.Point(384, 415)
        Me.txtFullpath.Name = "txtFullpath"
        Me.txtFullpath.ReadOnly = true
        Me.txtFullpath.Size = New System.Drawing.Size(202, 26)
        Me.txtFullpath.TabIndex = 45
        '
        'btnRefresh
        '
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnRefresh.Image = Global.Media_Companion.My.Resources.Resources.RefreshAll
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(161, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(123, 33)
        Me.btnRefresh.TabIndex = 49
        Me.btnRefresh.Text = "Refresh  "
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Reload all nfo's from"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Music Video Folders")
        Me.btnRefresh.UseVisualStyleBackColor = true
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.Media_Companion.My.Resources.Resources.Save
        Me.btnSave.Location = New System.Drawing.Point(915, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(34, 31)
        Me.btnSave.TabIndex = 51
        Me.btnSave.Text = "Button2"
        Me.ToolTip1.SetToolTip(Me.btnSave, "Save Manual Edits")
        Me.btnSave.UseVisualStyleBackColor = true
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(309, 377)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 27)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Director:"
        '
        'txtPlot
        '
        Me.txtPlot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtPlot, 6)
        Me.txtPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPlot.Location = New System.Drawing.Point(384, 456)
        Me.txtPlot.Multiline = true
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(525, 105)
        Me.txtPlot.TabIndex = 32
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(338, 459)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 105)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Plot:"
        '
        'txtTitle
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtTitle, 6)
        Me.txtTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTitle.Location = New System.Drawing.Point(299, 11)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(460, 31)
        Me.txtTitle.TabIndex = 27
        '
        'lstBxMainList
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.lstBxMainList, 3)
        Me.lstBxMainList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstBxMainList.FormattingEnabled = true
        Me.lstBxMainList.Location = New System.Drawing.Point(11, 100)
        Me.lstBxMainList.Name = "lstBxMainList"
        Me.TableLayoutPanel1.SetRowSpan(Me.lstBxMainList, 11)
        Me.lstBxMainList.Size = New System.Drawing.Size(274, 350)
        Me.lstBxMainList.TabIndex = 24
        '
        'PcBxMusicVideoScreenShot
        '
        Me.PcBxMusicVideoScreenShot.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.PcBxMusicVideoScreenShot, 2)
        Me.PcBxMusicVideoScreenShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PcBxMusicVideoScreenShot.Location = New System.Drawing.Point(692, 59)
        Me.PcBxMusicVideoScreenShot.Name = "PcBxMusicVideoScreenShot"
        Me.TableLayoutPanel1.SetRowSpan(Me.PcBxMusicVideoScreenShot, 4)
        Me.PcBxMusicVideoScreenShot.Size = New System.Drawing.Size(217, 219)
        Me.PcBxMusicVideoScreenShot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PcBxMusicVideoScreenShot.TabIndex = 26
        Me.PcBxMusicVideoScreenShot.TabStop = false
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(328, 295)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 27)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Artist:"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(300, 418)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 27)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Filename:"
        '
        'txtArtist
        '
        Me.txtArtist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtArtist, 2)
        Me.txtArtist.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtArtist.Location = New System.Drawing.Point(384, 292)
        Me.txtArtist.Name = "txtArtist"
        Me.txtArtist.Size = New System.Drawing.Size(202, 26)
        Me.txtArtist.TabIndex = 28
        '
        'txtDirector
        '
        Me.txtDirector.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtDirector, 2)
        Me.txtDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtDirector.Location = New System.Drawing.Point(384, 374)
        Me.txtDirector.Name = "txtDirector"
        Me.txtDirector.Size = New System.Drawing.Size(202, 26)
        Me.txtDirector.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(331, 336)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 27)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Year:"
        '
        'txtYear
        '
        Me.txtYear.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtYear, 2)
        Me.txtYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtYear.Location = New System.Drawing.Point(384, 333)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(202, 26)
        Me.txtYear.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(608, 295)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 27)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Album:"
        '
        'txtAlbum
        '
        Me.txtAlbum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtAlbum, 3)
        Me.txtAlbum.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtAlbum.Location = New System.Drawing.Point(672, 292)
        Me.txtAlbum.Name = "txtAlbum"
        Me.txtAlbum.Size = New System.Drawing.Size(237, 26)
        Me.txtAlbum.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(607, 336)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 27)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Studio:"
        '
        'txtStudio
        '
        Me.txtStudio.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtStudio, 3)
        Me.txtStudio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtStudio.Location = New System.Drawing.Point(672, 333)
        Me.txtStudio.Name = "txtStudio"
        Me.txtStudio.Size = New System.Drawing.Size(237, 26)
        Me.txtStudio.TabIndex = 31
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(608, 377)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 27)
        Me.Label11.TabIndex = 48
        Me.Label11.Text = "Genre:"
        '
        'txtGenre
        '
        Me.txtGenre.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtGenre, 3)
        Me.txtGenre.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtGenre.Location = New System.Drawing.Point(672, 374)
        Me.txtGenre.Name = "txtGenre"
        Me.txtGenre.Size = New System.Drawing.Size(237, 26)
        Me.txtGenre.TabIndex = 47
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(593, 418)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 27)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Runtime:"
        '
        'txtRuntime
        '
        Me.txtRuntime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtRuntime, 2)
        Me.txtRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtRuntime.Location = New System.Drawing.Point(672, 415)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.ReadOnly = true
        Me.txtRuntime.Size = New System.Drawing.Size(89, 26)
        Me.txtRuntime.TabIndex = 34
        '
        'btnMVPlay
        '
        Me.btnMVPlay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMVPlay.Image = Global.Media_Companion.My.Resources.Resources.Movie
        Me.btnMVPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMVPlay.Location = New System.Drawing.Point(774, 415)
        Me.btnMVPlay.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.btnMVPlay.Name = "btnMVPlay"
        Me.btnMVPlay.Size = New System.Drawing.Size(95, 27)
        Me.btnMVPlay.TabIndex = 53
        Me.btnMVPlay.Text = "Play"
        Me.btnMVPlay.UseVisualStyleBackColor = true
        '
        'tPScreenshotMV
        '
        Me.tPScreenshotMV.BackColor = System.Drawing.Color.LightGray
        Me.tPScreenshotMV.Controls.Add(Me.btnGoogleSearch)
        Me.tPScreenshotMV.Controls.Add(Me.Label17)
        Me.tPScreenshotMV.Controls.Add(Me.Label16)
        Me.tPScreenshotMV.Controls.Add(Me.Label15)
        Me.tPScreenshotMV.Controls.Add(Me.Label14)
        Me.tPScreenshotMV.Controls.Add(Me.btnPasteFromClipboard)
        Me.tPScreenshotMV.Controls.Add(Me.btnSaveCrop)
        Me.tPScreenshotMV.Controls.Add(Me.btnCropReset)
        Me.tPScreenshotMV.Controls.Add(Me.btnCrop)
        Me.tPScreenshotMV.Controls.Add(Me.txtScreenshotTime)
        Me.tPScreenshotMV.Controls.Add(Me.btnScreenshotMinus)
        Me.tPScreenshotMV.Controls.Add(Me.btnScreenshotPlus)
        Me.tPScreenshotMV.Controls.Add(Me.Label12)
        Me.tPScreenshotMV.Controls.Add(Me.btnCreateScreenshot)
        Me.tPScreenshotMV.Controls.Add(Me.pcBxScreenshot)
        Me.tPScreenshotMV.Location = New System.Drawing.Point(4, 22)
        Me.tPScreenshotMV.Name = "tPScreenshotMV"
        Me.tPScreenshotMV.Padding = New System.Windows.Forms.Padding(3)
        Me.tPScreenshotMV.Size = New System.Drawing.Size(967, 578)
        Me.tPScreenshotMV.TabIndex = 2
        Me.tPScreenshotMV.Text = "Screenshot"
        '
        'btnGoogleSearch
        '
        Me.btnGoogleSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnGoogleSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnGoogleSearch.Location = New System.Drawing.Point(218, 405)
        Me.btnGoogleSearch.Name = "btnGoogleSearch"
        Me.btnGoogleSearch.Size = New System.Drawing.Size(126, 32)
        Me.btnGoogleSearch.TabIndex = 16
        Me.btnGoogleSearch.Text = "Google Search"
        Me.btnGoogleSearch.UseVisualStyleBackColor = true
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = true
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.Location = New System.Drawing.Point(62, 441)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(66, 20)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Label17"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = true
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.Location = New System.Drawing.Point(62, 408)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 20)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Label16"
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = true
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.Location = New System.Drawing.Point(2, 441)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(60, 20)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Height:"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = true
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.Location = New System.Drawing.Point(2, 408)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 20)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Width:"
        '
        'btnPasteFromClipboard
        '
        Me.btnPasteFromClipboard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnPasteFromClipboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPasteFromClipboard.Location = New System.Drawing.Point(350, 405)
        Me.btnPasteFromClipboard.Name = "btnPasteFromClipboard"
        Me.btnPasteFromClipboard.Size = New System.Drawing.Size(165, 32)
        Me.btnPasteFromClipboard.TabIndex = 11
        Me.btnPasteFromClipboard.Text = "Paste from Clipboard"
        Me.btnPasteFromClipboard.UseVisualStyleBackColor = true
        '
        'btnSaveCrop
        '
        Me.btnSaveCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnSaveCrop.Enabled = false
        Me.btnSaveCrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnSaveCrop.Location = New System.Drawing.Point(521, 481)
        Me.btnSaveCrop.Name = "btnSaveCrop"
        Me.btnSaveCrop.Size = New System.Drawing.Size(131, 32)
        Me.btnSaveCrop.TabIndex = 10
        Me.btnSaveCrop.Text = "Save Changes"
        Me.btnSaveCrop.UseVisualStyleBackColor = true
        '
        'btnCropReset
        '
        Me.btnCropReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnCropReset.Enabled = false
        Me.btnCropReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCropReset.Location = New System.Drawing.Point(521, 443)
        Me.btnCropReset.Name = "btnCropReset"
        Me.btnCropReset.Size = New System.Drawing.Size(131, 32)
        Me.btnCropReset.TabIndex = 9
        Me.btnCropReset.Text = "Reset Image"
        Me.btnCropReset.UseVisualStyleBackColor = true
        '
        'btnCrop
        '
        Me.btnCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnCrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCrop.Location = New System.Drawing.Point(521, 405)
        Me.btnCrop.Name = "btnCrop"
        Me.btnCrop.Size = New System.Drawing.Size(131, 32)
        Me.btnCrop.TabIndex = 7
        Me.btnCrop.Text = "Enable Crop"
        Me.btnCrop.UseVisualStyleBackColor = true
        '
        'txtScreenshotTime
        '
        Me.txtScreenshotTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.txtScreenshotTime.CausesValidation = false
        Me.txtScreenshotTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtScreenshotTime.HidePromptOnLeave = true
        Me.txtScreenshotTime.Location = New System.Drawing.Point(218, 530)
        Me.txtScreenshotTime.Mask = "00000"
        Me.txtScreenshotTime.Name = "txtScreenshotTime"
        Me.txtScreenshotTime.Size = New System.Drawing.Size(60, 26)
        Me.txtScreenshotTime.TabIndex = 6
        Me.txtScreenshotTime.ValidatingType = GetType(Integer)
        '
        'btnScreenshotMinus
        '
        Me.btnScreenshotMinus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnScreenshotMinus.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnScreenshotMinus.Location = New System.Drawing.Point(177, 523)
        Me.btnScreenshotMinus.Name = "btnScreenshotMinus"
        Me.btnScreenshotMinus.Size = New System.Drawing.Size(35, 36)
        Me.btnScreenshotMinus.TabIndex = 5
        Me.btnScreenshotMinus.Text = "-"
        Me.btnScreenshotMinus.UseVisualStyleBackColor = true
        '
        'btnScreenshotPlus
        '
        Me.btnScreenshotPlus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnScreenshotPlus.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnScreenshotPlus.Location = New System.Drawing.Point(284, 523)
        Me.btnScreenshotPlus.Name = "btnScreenshotPlus"
        Me.btnScreenshotPlus.Size = New System.Drawing.Size(35, 36)
        Me.btnScreenshotPlus.TabIndex = 4
        Me.btnScreenshotPlus.Text = "+"
        Me.btnScreenshotPlus.UseVisualStyleBackColor = true
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = true
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(2, 519)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(169, 40)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Enter Time in Seconds"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"for Capture:"
        '
        'btnCreateScreenshot
        '
        Me.btnCreateScreenshot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnCreateScreenshot.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCreateScreenshot.Location = New System.Drawing.Point(325, 527)
        Me.btnCreateScreenshot.Name = "btnCreateScreenshot"
        Me.btnCreateScreenshot.Size = New System.Drawing.Size(188, 32)
        Me.btnCreateScreenshot.TabIndex = 2
        Me.btnCreateScreenshot.Text = "Create New Screenshot"
        Me.btnCreateScreenshot.UseVisualStyleBackColor = true
        '
        'pcBxScreenshot
        '
        Me.pcBxScreenshot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pcBxScreenshot.Location = New System.Drawing.Point(6, 6)
        Me.pcBxScreenshot.Name = "pcBxScreenshot"
        Me.pcBxScreenshot.Size = New System.Drawing.Size(646, 393)
        Me.pcBxScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pcBxScreenshot.TabIndex = 0
        Me.pcBxScreenshot.TabStop = false
        '
        'tPPosterScrape
        '
        Me.tPPosterScrape.Controls.Add(Me.btnGoogleSearchPoster)
        Me.tPPosterScrape.Controls.Add(Me.Label18)
        Me.tPPosterScrape.Controls.Add(Me.Label19)
        Me.tPPosterScrape.Controls.Add(Me.Label20)
        Me.tPPosterScrape.Controls.Add(Me.Label21)
        Me.tPPosterScrape.Controls.Add(Me.btnPosterPaste)
        Me.tPPosterScrape.Controls.Add(Me.btnPosterSave)
        Me.tPPosterScrape.Controls.Add(Me.btnPosterReset)
        Me.tPPosterScrape.Controls.Add(Me.btnPosterCrop)
        Me.tPPosterScrape.Controls.Add(Me.pcBxSinglePoster)
        Me.tPPosterScrape.Location = New System.Drawing.Point(4, 22)
        Me.tPPosterScrape.Name = "tPPosterScrape"
        Me.tPPosterScrape.Size = New System.Drawing.Size(967, 578)
        Me.tPPosterScrape.TabIndex = 4
        Me.tPPosterScrape.Text = "Poster"
        Me.tPPosterScrape.UseVisualStyleBackColor = true
        '
        'btnGoogleSearchPoster
        '
        Me.btnGoogleSearchPoster.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnGoogleSearchPoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnGoogleSearchPoster.Location = New System.Drawing.Point(215, 402)
        Me.btnGoogleSearchPoster.Name = "btnGoogleSearchPoster"
        Me.btnGoogleSearchPoster.Size = New System.Drawing.Size(126, 32)
        Me.btnGoogleSearchPoster.TabIndex = 30
        Me.btnGoogleSearchPoster.Text = "Google Search"
        Me.btnGoogleSearchPoster.UseVisualStyleBackColor = true
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = true
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label18.Location = New System.Drawing.Point(59, 438)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 20)
        Me.Label18.TabIndex = 29
        Me.Label18.Text = "Label18"
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = true
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label19.Location = New System.Drawing.Point(59, 405)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(66, 20)
        Me.Label19.TabIndex = 28
        Me.Label19.Text = "Label19"
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = true
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label20.Location = New System.Drawing.Point(-1, 438)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 20)
        Me.Label20.TabIndex = 27
        Me.Label20.Text = "Height:"
        '
        'Label21
        '
        Me.Label21.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label21.AutoSize = true
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label21.Location = New System.Drawing.Point(-1, 405)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(54, 20)
        Me.Label21.TabIndex = 26
        Me.Label21.Text = "Width:"
        '
        'btnPosterPaste
        '
        Me.btnPosterPaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnPosterPaste.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPosterPaste.Location = New System.Drawing.Point(347, 402)
        Me.btnPosterPaste.Name = "btnPosterPaste"
        Me.btnPosterPaste.Size = New System.Drawing.Size(165, 32)
        Me.btnPosterPaste.TabIndex = 25
        Me.btnPosterPaste.Text = "Paste from Clipboard"
        Me.btnPosterPaste.UseVisualStyleBackColor = true
        '
        'btnPosterSave
        '
        Me.btnPosterSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnPosterSave.Enabled = false
        Me.btnPosterSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPosterSave.Location = New System.Drawing.Point(365, 469)
        Me.btnPosterSave.Name = "btnPosterSave"
        Me.btnPosterSave.Size = New System.Drawing.Size(131, 32)
        Me.btnPosterSave.TabIndex = 24
        Me.btnPosterSave.Text = "Save Changes"
        Me.btnPosterSave.UseVisualStyleBackColor = true
        '
        'btnPosterReset
        '
        Me.btnPosterReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnPosterReset.Enabled = false
        Me.btnPosterReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPosterReset.Location = New System.Drawing.Point(215, 488)
        Me.btnPosterReset.Name = "btnPosterReset"
        Me.btnPosterReset.Size = New System.Drawing.Size(131, 32)
        Me.btnPosterReset.TabIndex = 23
        Me.btnPosterReset.Text = "Reset Image"
        Me.btnPosterReset.UseVisualStyleBackColor = true
        '
        'btnPosterCrop
        '
        Me.btnPosterCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnPosterCrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPosterCrop.Location = New System.Drawing.Point(215, 450)
        Me.btnPosterCrop.Name = "btnPosterCrop"
        Me.btnPosterCrop.Size = New System.Drawing.Size(131, 32)
        Me.btnPosterCrop.TabIndex = 22
        Me.btnPosterCrop.Text = "Enable Crop"
        Me.btnPosterCrop.UseVisualStyleBackColor = true
        '
        'pcBxSinglePoster
        '
        Me.pcBxSinglePoster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pcBxSinglePoster.Location = New System.Drawing.Point(3, 3)
        Me.pcBxSinglePoster.Name = "pcBxSinglePoster"
        Me.pcBxSinglePoster.Size = New System.Drawing.Size(509, 393)
        Me.pcBxSinglePoster.TabIndex = 0
        Me.pcBxSinglePoster.TabStop = false
        '
        'tPManualScrape
        '
        Me.tPManualScrape.Controls.Add(Me.chkBxOverWriteArt)
        Me.tPManualScrape.Controls.Add(Me.Label13)
        Me.tPManualScrape.Controls.Add(Me.btnManualScrape)
        Me.tPManualScrape.Controls.Add(Me.WebBrowser1)
        Me.tPManualScrape.Location = New System.Drawing.Point(4, 22)
        Me.tPManualScrape.Name = "tPManualScrape"
        Me.tPManualScrape.Size = New System.Drawing.Size(967, 578)
        Me.tPManualScrape.TabIndex = 3
        Me.tPManualScrape.Text = "Manually find Correct Wiki Entry"
        Me.tPManualScrape.UseVisualStyleBackColor = true
        '
        'chkBxOverWriteArt
        '
        Me.chkBxOverWriteArt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkBxOverWriteArt.AutoSize = true
        Me.chkBxOverWriteArt.Checked = true
        Me.chkBxOverWriteArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBxOverWriteArt.Location = New System.Drawing.Point(780, 531)
        Me.chkBxOverWriteArt.Name = "chkBxOverWriteArt"
        Me.chkBxOverWriteArt.Size = New System.Drawing.Size(172, 30)
        Me.chkBxOverWriteArt.TabIndex = 3
        Me.chkBxOverWriteArt.Text = "Un-check if you don't want MC"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"to overwrite your current art. "
        Me.chkBxOverWriteArt.UseVisualStyleBackColor = true
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = true
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label13.Location = New System.Drawing.Point(322, 534)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(367, 20)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Browse to the correct Wikipedia Page and Click Go"
        '
        'btnManualScrape
        '
        Me.btnManualScrape.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnManualScrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnManualScrape.Location = New System.Drawing.Point(695, 529)
        Me.btnManualScrape.Name = "btnManualScrape"
        Me.btnManualScrape.Size = New System.Drawing.Size(70, 30)
        Me.btnManualScrape.TabIndex = 1
        Me.btnManualScrape.Text = "Go"
        Me.btnManualScrape.UseVisualStyleBackColor = true
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(964, 520)
        Me.WebBrowser1.TabIndex = 0
        '
        'tPPref
        '
        Me.tPPref.BackColor = System.Drawing.Color.LightGray
        Me.tPPref.Controls.Add(Me.chlstBoxFolders)
        Me.tPPref.Controls.Add(Me.Label8)
        Me.tPPref.Controls.Add(Me.btnRemoveFolder)
        Me.tPPref.Controls.Add(Me.btnAddFolderPath)
        Me.tPPref.Controls.Add(Me.tbFolderPath)
        Me.tPPref.Controls.Add(Me.btnBrowseFolders)
        Me.tPPref.Controls.Add(Me.Label9)
        Me.tPPref.Controls.Add(Me.lstBoxFolders)
        Me.tPPref.Location = New System.Drawing.Point(4, 22)
        Me.tPPref.Name = "tPPref"
        Me.tPPref.Padding = New System.Windows.Forms.Padding(3)
        Me.tPPref.Size = New System.Drawing.Size(967, 578)
        Me.tPPref.TabIndex = 1
        Me.tPPref.Text = "Preferences"
        '
        'chlstBoxFolders
        '
        Me.chlstBoxFolders.AllowDrop = true
        Me.chlstBoxFolders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.chlstBoxFolders.FormattingEnabled = true
        Me.chlstBoxFolders.Location = New System.Drawing.Point(11, 17)
        Me.chlstBoxFolders.Name = "chlstBoxFolders"
        Me.chlstBoxFolders.Size = New System.Drawing.Size(333, 469)
        Me.chlstBoxFolders.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(19, 485)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(197, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Manually add Path to Music Video folder"
        '
        'btnRemoveFolder
        '
        Me.btnRemoveFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnRemoveFolder.Location = New System.Drawing.Point(158, 526)
        Me.btnRemoveFolder.Name = "btnRemoveFolder"
        Me.btnRemoveFolder.Size = New System.Drawing.Size(122, 48)
        Me.btnRemoveFolder.TabIndex = 5
        Me.btnRemoveFolder.Text = "Remove Selected Folder"
        Me.btnRemoveFolder.UseVisualStyleBackColor = true
        '
        'btnAddFolderPath
        '
        Me.btnAddFolderPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnAddFolderPath.Location = New System.Drawing.Point(288, 493)
        Me.btnAddFolderPath.Name = "btnAddFolderPath"
        Me.btnAddFolderPath.Size = New System.Drawing.Size(56, 32)
        Me.btnAddFolderPath.TabIndex = 4
        Me.btnAddFolderPath.Text = "Add"
        Me.btnAddFolderPath.UseVisualStyleBackColor = true
        '
        'tbFolderPath
        '
        Me.tbFolderPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.tbFolderPath.Location = New System.Drawing.Point(11, 500)
        Me.tbFolderPath.Name = "tbFolderPath"
        Me.tbFolderPath.Size = New System.Drawing.Size(269, 20)
        Me.tbFolderPath.TabIndex = 3
        '
        'btnBrowseFolders
        '
        Me.btnBrowseFolders.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnBrowseFolders.Location = New System.Drawing.Point(11, 526)
        Me.btnBrowseFolders.Name = "btnBrowseFolders"
        Me.btnBrowseFolders.Size = New System.Drawing.Size(122, 48)
        Me.btnBrowseFolders.TabIndex = 2
        Me.btnBrowseFolders.Text = "Browse for Music Video Folders"
        Me.btnBrowseFolders.UseVisualStyleBackColor = true
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(8, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Folders"
        '
        'lstBoxFolders
        '
        Me.lstBoxFolders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lstBoxFolders.FormattingEnabled = true
        Me.lstBoxFolders.Location = New System.Drawing.Point(381, 17)
        Me.lstBoxFolders.Name = "lstBoxFolders"
        Me.lstBoxFolders.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstBoxFolders.Size = New System.Drawing.Size(333, 459)
        Me.lstBoxFolders.TabIndex = 0
        '
        'ucMusicVideo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControlMain)
        Me.Name = "ucMusicVideo"
        Me.Size = New System.Drawing.Size(975, 604)
        Me.TabControlMain.ResumeLayout(false)
        Me.tPMainMV.ResumeLayout(false)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.PcBxPoster,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PcBxMusicVideoScreenShot,System.ComponentModel.ISupportInitialize).EndInit
        Me.tPScreenshotMV.ResumeLayout(false)
        Me.tPScreenshotMV.PerformLayout
        CType(Me.pcBxScreenshot,System.ComponentModel.ISupportInitialize).EndInit
        Me.tPPosterScrape.ResumeLayout(false)
        Me.tPPosterScrape.PerformLayout
        CType(Me.pcBxSinglePoster,System.ComponentModel.ISupportInitialize).EndInit
        Me.tPManualScrape.ResumeLayout(false)
        Me.tPManualScrape.PerformLayout
        Me.tPPref.ResumeLayout(false)
        Me.tPPref.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents tPMainMV As System.Windows.Forms.TabPage
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtGenre As System.Windows.Forms.TextBox
    Friend WithEvents txtFullpath As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRuntime As System.Windows.Forms.TextBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents txtStudio As System.Windows.Forms.TextBox
    Friend WithEvents txtAlbum As System.Windows.Forms.TextBox
    Friend WithEvents txtDirector As System.Windows.Forms.TextBox
    Friend WithEvents txtArtist As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents PcBxMusicVideoScreenShot As System.Windows.Forms.PictureBox
    Friend WithEvents btnSearchNew As System.Windows.Forms.Button
    Friend WithEvents lstBxMainList As System.Windows.Forms.ListBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tPPref As System.Windows.Forms.TabPage
    Friend WithEvents btnBrowseFolders As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lstBoxFolders As System.Windows.Forms.ListBox
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents tPScreenshotMV As System.Windows.Forms.TabPage
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnCreateScreenshot As System.Windows.Forms.Button
    Friend WithEvents pcBxScreenshot As System.Windows.Forms.PictureBox
    Friend WithEvents btnScreenshotMinus As System.Windows.Forms.Button
    Friend WithEvents btnScreenshotPlus As System.Windows.Forms.Button
    Friend WithEvents txtScreenshotTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents PcBxPoster As System.Windows.Forms.PictureBox
    Friend WithEvents btnRemoveFolder As System.Windows.Forms.Button
    Friend WithEvents btnAddFolderPath As System.Windows.Forms.Button
    Friend WithEvents tbFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tPManualScrape As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents btnManualScrape As System.Windows.Forms.Button
    Friend WithEvents chkBxOverWriteArt As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tPPosterScrape As System.Windows.Forms.TabPage
    Friend WithEvents btnCrop As System.Windows.Forms.Button
    Friend WithEvents btnSaveCrop As System.Windows.Forms.Button
    Friend WithEvents btnCropReset As System.Windows.Forms.Button
    Friend WithEvents btnPasteFromClipboard As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnGoogleSearch As System.Windows.Forms.Button
    Friend WithEvents pcBxSinglePoster As System.Windows.Forms.PictureBox
    Friend WithEvents btnGoogleSearchPoster As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnPosterPaste As System.Windows.Forms.Button
    Friend WithEvents btnPosterSave As System.Windows.Forms.Button
    Friend WithEvents btnPosterReset As System.Windows.Forms.Button
    Friend WithEvents btnPosterCrop As System.Windows.Forms.Button
    Friend WithEvents chlstBoxFolders As CheckedListBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents btnMVPlay As Button
End Class
