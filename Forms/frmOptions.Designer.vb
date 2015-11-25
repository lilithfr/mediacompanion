<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ColorDialog = New System.Windows.Forms.ColorDialog()
        Me.FontDialog = New System.Windows.Forms.FontDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox36 = New System.Windows.Forms.GroupBox()
        Me.llMkvMergeGuiPath = New System.Windows.Forms.LinkLabel()
        Me.btnMkvMergeGuiPath = New System.Windows.Forms.Button()
        Me.tbMkvMergeGuiPath = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.tbaltnfoeditor = New System.Windows.Forms.TextBox()
        Me.cbUseMultipleThreads = New System.Windows.Forms.CheckBox()
        Me.btnFindBrowser = New System.Windows.Forms.Button()
        Me.cbDisplayLocalActor = New System.Windows.Forms.CheckBox()
        Me.cbCheckForNewVersion = New System.Windows.Forms.CheckBox()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.cb_LocalActorSaveAlpha = New System.Windows.Forms.CheckBox()
        Me.xbmcactorpath = New System.Windows.Forms.TextBox()
        Me.btn_localactorpathbrowse = New System.Windows.Forms.Button()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.localactorpath = New System.Windows.Forms.TextBox()
        Me.saveactorchkbx = New System.Windows.Forms.CheckBox()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.cbShowAllAudioTracks = New System.Windows.Forms.CheckBox()
        Me.cbDisplayMediaInfoOverlay = New System.Windows.Forms.CheckBox()
        Me.cbDisplayRatingOverlay = New System.Windows.Forms.CheckBox()
        Me.gbExcludeFolders = New System.Windows.Forms.GroupBox()
        Me.tbExcludeFolders = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_MediaPlayerUser = New System.Windows.Forms.Label()
        Me.btn_MediaPlayerBrowse = New System.Windows.Forms.Button()
        Me.rb_MediaPlayerUser = New System.Windows.Forms.RadioButton()
        Me.rb_MediaPlayerWMP = New System.Windows.Forms.RadioButton()
        Me.rb_MediaPlayerDefault = New System.Windows.Forms.RadioButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TPCommon = New System.Windows.Forms.TabPage()
        Me.TabControl4 = New System.Windows.Forms.TabControl()
        Me.TPCommonSettings = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gbImageResizing = New System.Windows.Forms.GroupBox()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.comboActorResolutions = New System.Windows.Forms.ComboBox()
        Me.comboBackDropResolutions = New System.Windows.Forms.ComboBox()
        Me.comboPosterResolutions = New System.Windows.Forms.ComboBox()
        Me.grpCleanFilename = New System.Windows.Forms.GroupBox()
        Me.btnCleanFilenameRemove = New System.Windows.Forms.Button()
        Me.txtCleanFilenameAdd = New System.Windows.Forms.TextBox()
        Me.btnCleanFilenameAdd = New System.Windows.Forms.Button()
        Me.lbCleanFilename = New System.Windows.Forms.ListBox()
        Me.grpVideoSource = New System.Windows.Forms.GroupBox()
        Me.btnVideoSourceRemove = New System.Windows.Forms.Button()
        Me.txtVideoSourceAdd = New System.Windows.Forms.TextBox()
        Me.btnVideoSourceAdd = New System.Windows.Forms.Button()
        Me.lbVideoSource = New System.Windows.Forms.ListBox()
        Me.cbDisplayMediaInfoFolderSize = New System.Windows.Forms.CheckBox()
        Me.cb_IgnoreAn = New System.Windows.Forms.CheckBox()
        Me.cb_SorttitleIgnoreArticles = New System.Windows.Forms.CheckBox()
        Me.cb_IgnoreA = New System.Windows.Forms.CheckBox()
        Me.cbOverwriteArtwork = New System.Windows.Forms.CheckBox()
        Me.cb_IgnoreThe = New System.Windows.Forms.CheckBox()
        Me.CheckBox38 = New System.Windows.Forms.CheckBox()
        Me.gbxXBMCversion = New System.Windows.Forms.GroupBox()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.rbXBMCv_both = New System.Windows.Forms.RadioButton()
        Me.rbXBMCv_post = New System.Windows.Forms.RadioButton()
        Me.rbXBMCv_pre = New System.Windows.Forms.RadioButton()
        Me.TPActors = New System.Windows.Forms.TabPage()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.GroupBox32 = New System.Windows.Forms.GroupBox()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.cb_actorseasy = New System.Windows.Forms.CheckBox()
        Me.TPGen = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbMultiMonitorEnable = New System.Windows.Forms.CheckBox()
        Me.cbRenameNFOtoINFO = New System.Windows.Forms.CheckBox()
        Me.cbShowLogOnError = New System.Windows.Forms.CheckBox()
        Me.cbExternalbrowser = New System.Windows.Forms.CheckBox()
        Me.chkbx_disablecache = New System.Windows.Forms.CheckBox()
        Me.GroupBox45 = New System.Windows.Forms.GroupBox()
        Me.lblaltnfoeditorclear = New System.Windows.Forms.Label()
        Me.btnaltnfoeditor = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox33 = New System.Windows.Forms.GroupBox()
        Me.btnFontSelect = New System.Windows.Forms.Button()
        Me.btnFontReset = New System.Windows.Forms.Button()
        Me.lbl_FontSample = New System.Windows.Forms.Label()
        Me.GroupBox31 = New System.Windows.Forms.GroupBox()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.txtbx_minrarsize = New System.Windows.Forms.TextBox()
        Me.TPMovPref = New System.Windows.Forms.TabPage()
        Me.TPTVPref = New System.Windows.Forms.TabPage()
        Me.TPProxy = New System.Windows.Forms.TabPage()
        Me.UcGenPref_Proxy1 = New Media_Companion.ucGenPref_Proxy()
        Me.TPXBMCLink = New System.Windows.Forms.TabPage()
        Me.UcGenPref_XbmcLink1 = New Media_Companion.ucGenPref_XbmcLink()
        Me.TPPRofCmd = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox42 = New System.Windows.Forms.GroupBox()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.btn_ProfileSetStartup = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lb_ProfileList = New System.Windows.Forms.ListBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btn_ProfileSetDefault = New System.Windows.Forms.Button()
        Me.tb_ProfileNew = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btn_ProfileAdd = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btn_ProfileRemove = New System.Windows.Forms.Button()
        Me.lbl_CommandTitle = New System.Windows.Forms.Label()
        Me.lcl_CommandCommand = New System.Windows.Forms.Label()
        Me.tb_CommandTitle = New System.Windows.Forms.TextBox()
        Me.tb_CommandCommand = New System.Windows.Forms.TextBox()
        Me.btn_CommandRemove = New System.Windows.Forms.Button()
        Me.lb_CommandTitle = New System.Windows.Forms.ListBox()
        Me.lb_CommandCommand = New System.Windows.Forms.ListBox()
        Me.btn_CommandAdd = New System.Windows.Forms.Button()
        Me.btn_SettingsCancel = New System.Windows.Forms.Button()
        Me.btn_SettingsClose = New System.Windows.Forms.Button()
        Me.btn_SettingsApply = New System.Windows.Forms.Button()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.AutoScrnShtDelay = New System.Windows.Forms.TextBox()
        Me.GroupBox36.SuspendLayout
        Me.GroupBox12.SuspendLayout
        Me.gbExcludeFolders.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox3.SuspendLayout
        Me.TabControl1.SuspendLayout
        Me.TPCommon.SuspendLayout
        Me.TabControl4.SuspendLayout
        Me.TPCommonSettings.SuspendLayout
        Me.gbImageResizing.SuspendLayout
        Me.grpCleanFilename.SuspendLayout
        Me.grpVideoSource.SuspendLayout
        Me.gbxXBMCversion.SuspendLayout
        Me.TPActors.SuspendLayout
        Me.GroupBox32.SuspendLayout
        Me.TPGen.SuspendLayout
        Me.GroupBox45.SuspendLayout
        Me.GroupBox33.SuspendLayout
        Me.GroupBox31.SuspendLayout
        Me.TPProxy.SuspendLayout
        Me.TPXBMCLink.SuspendLayout
        Me.TPPRofCmd.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.GroupBox42.SuspendLayout
        Me.GroupBox15.SuspendLayout
        Me.SuspendLayout
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupBox36
        '
        Me.GroupBox36.Controls.Add(Me.llMkvMergeGuiPath)
        Me.GroupBox36.Controls.Add(Me.btnMkvMergeGuiPath)
        Me.GroupBox36.Controls.Add(Me.tbMkvMergeGuiPath)
        Me.GroupBox36.Controls.Add(Me.Label19)
        Me.GroupBox36.Location = New System.Drawing.Point(480, 210)
        Me.GroupBox36.Name = "GroupBox36"
        Me.GroupBox36.Size = New System.Drawing.Size(364, 67)
        Me.GroupBox36.TabIndex = 83
        Me.GroupBox36.TabStop = false
        Me.GroupBox36.Text = "mkvmerge GUI"
        Me.ToolTip1.SetToolTip(Me.GroupBox36, "Use this program to save disk space by removing unwanted extra audio and subtitle"& _ 
        " tracks")
        '
        'llMkvMergeGuiPath
        '
        Me.llMkvMergeGuiPath.AutoSize = true
        Me.llMkvMergeGuiPath.Location = New System.Drawing.Point(43, 45)
        Me.llMkvMergeGuiPath.Name = "llMkvMergeGuiPath"
        Me.llMkvMergeGuiPath.Size = New System.Drawing.Size(74, 13)
        Me.llMkvMergeGuiPath.TabIndex = 3
        Me.llMkvMergeGuiPath.TabStop = true
        Me.llMkvMergeGuiPath.Text = "Download link"
        '
        'btnMkvMergeGuiPath
        '
        Me.btnMkvMergeGuiPath.Location = New System.Drawing.Point(332, 21)
        Me.btnMkvMergeGuiPath.Name = "btnMkvMergeGuiPath"
        Me.btnMkvMergeGuiPath.Size = New System.Drawing.Size(26, 23)
        Me.btnMkvMergeGuiPath.TabIndex = 2
        Me.btnMkvMergeGuiPath.Text = "..."
        Me.btnMkvMergeGuiPath.UseVisualStyleBackColor = true
        '
        'tbMkvMergeGuiPath
        '
        Me.tbMkvMergeGuiPath.Location = New System.Drawing.Point(46, 21)
        Me.tbMkvMergeGuiPath.Name = "tbMkvMergeGuiPath"
        Me.tbMkvMergeGuiPath.ReadOnly = true
        Me.tbMkvMergeGuiPath.Size = New System.Drawing.Size(287, 20)
        Me.tbMkvMergeGuiPath.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.tbMkvMergeGuiPath, "Browse to Program Files\MKVToolNix\mmg.exe")
        '
        'Label19
        '
        Me.Label19.AutoSize = true
        Me.Label19.Location = New System.Drawing.Point(10, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Path:"
        '
        'tbaltnfoeditor
        '
        Me.tbaltnfoeditor.Location = New System.Drawing.Point(46, 17)
        Me.tbaltnfoeditor.Name = "tbaltnfoeditor"
        Me.tbaltnfoeditor.ReadOnly = true
        Me.tbaltnfoeditor.Size = New System.Drawing.Size(287, 20)
        Me.tbaltnfoeditor.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.tbaltnfoeditor, "Browse to Program Files\MKVToolNix\mmg.exe")
        '
        'cbUseMultipleThreads
        '
        Me.cbUseMultipleThreads.AutoSize = true
        Me.cbUseMultipleThreads.Location = New System.Drawing.Point(18, 374)
        Me.cbUseMultipleThreads.Margin = New System.Windows.Forms.Padding(4)
        Me.cbUseMultipleThreads.Name = "cbUseMultipleThreads"
        Me.cbUseMultipleThreads.Size = New System.Drawing.Size(242, 17)
        Me.cbUseMultipleThreads.TabIndex = 94
        Me.cbUseMultipleThreads.Text = "Use multiple threaded version where available"
        Me.ToolTip1.SetToolTip(Me.cbUseMultipleThreads, "Currently only implemented in movies 'Refresh All'. "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Enable for maximum performa"& _ 
        "nce."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Disable if you encounter a problem.")
        Me.cbUseMultipleThreads.UseVisualStyleBackColor = true
        '
        'btnFindBrowser
        '
        Me.btnFindBrowser.Enabled = false
        Me.btnFindBrowser.Location = New System.Drawing.Point(52, 316)
        Me.btnFindBrowser.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFindBrowser.Name = "btnFindBrowser"
        Me.btnFindBrowser.Size = New System.Drawing.Size(112, 26)
        Me.btnFindBrowser.TabIndex = 92
        Me.btnFindBrowser.Text = "Locate browser..."
        Me.ToolTip1.SetToolTip(Me.btnFindBrowser, "Select external browser to use. ")
        Me.btnFindBrowser.UseVisualStyleBackColor = false
        '
        'cbDisplayLocalActor
        '
        Me.cbDisplayLocalActor.AutoSize = true
        Me.cbDisplayLocalActor.Location = New System.Drawing.Point(349, 321)
        Me.cbDisplayLocalActor.Name = "cbDisplayLocalActor"
        Me.cbDisplayLocalActor.Size = New System.Drawing.Size(175, 17)
        Me.cbDisplayLocalActor.TabIndex = 97
        Me.cbDisplayLocalActor.Text = "Display Local Actor images only"
        Me.ToolTip1.SetToolTip(Me.cbDisplayLocalActor, "If selected, MC will not attempt to download actor"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"images from Internet if image"& _ 
        " is not locally stored")
        Me.cbDisplayLocalActor.UseVisualStyleBackColor = true
        '
        'cbCheckForNewVersion
        '
        Me.cbCheckForNewVersion.AutoSize = true
        Me.cbCheckForNewVersion.Location = New System.Drawing.Point(349, 295)
        Me.cbCheckForNewVersion.Margin = New System.Windows.Forms.Padding(4)
        Me.cbCheckForNewVersion.Name = "cbCheckForNewVersion"
        Me.cbCheckForNewVersion.Size = New System.Drawing.Size(179, 17)
        Me.cbCheckForNewVersion.TabIndex = 96
        Me.cbCheckForNewVersion.Text = "Check for new version at startup"
        Me.ToolTip1.SetToolTip(Me.cbCheckForNewVersion, "Currently only implemented in movies 'Refresh All'. "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Enable for maximum performa"& _ 
        "nce."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Disable if you encounter a problem.")
        Me.cbCheckForNewVersion.UseVisualStyleBackColor = true
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.cb_LocalActorSaveAlpha)
        Me.GroupBox12.Controls.Add(Me.xbmcactorpath)
        Me.GroupBox12.Controls.Add(Me.btn_localactorpathbrowse)
        Me.GroupBox12.Controls.Add(Me.Label161)
        Me.GroupBox12.Controls.Add(Me.Label132)
        Me.GroupBox12.Controls.Add(Me.Label104)
        Me.GroupBox12.Controls.Add(Me.Label103)
        Me.GroupBox12.Controls.Add(Me.Label101)
        Me.GroupBox12.Controls.Add(Me.Label96)
        Me.GroupBox12.Controls.Add(Me.Label97)
        Me.GroupBox12.Controls.Add(Me.localactorpath)
        Me.GroupBox12.Controls.Add(Me.saveactorchkbx)
        Me.GroupBox12.Location = New System.Drawing.Point(456, 33)
        Me.GroupBox12.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox12.Size = New System.Drawing.Size(463, 401)
        Me.GroupBox12.TabIndex = 49
        Me.GroupBox12.TabStop = false
        Me.GroupBox12.Text = "Download Actor Thumbs"
        Me.ToolTip1.SetToolTip(Me.GroupBox12, "Downloads actor thumbnails to Local or Network Location")
        '
        'cb_LocalActorSaveAlpha
        '
        Me.cb_LocalActorSaveAlpha.AutoSize = true
        Me.cb_LocalActorSaveAlpha.Location = New System.Drawing.Point(8, 106)
        Me.cb_LocalActorSaveAlpha.Name = "cb_LocalActorSaveAlpha"
        Me.cb_LocalActorSaveAlpha.Size = New System.Drawing.Size(177, 17)
        Me.cb_LocalActorSaveAlpha.TabIndex = 11
        Me.cb_LocalActorSaveAlpha.Text = "Save actor as Actor_Name.extn"
        Me.cb_LocalActorSaveAlpha.UseVisualStyleBackColor = true
        '
        'xbmcactorpath
        '
        Me.xbmcactorpath.Location = New System.Drawing.Point(87, 377)
        Me.xbmcactorpath.Margin = New System.Windows.Forms.Padding(4)
        Me.xbmcactorpath.Name = "xbmcactorpath"
        Me.xbmcactorpath.Size = New System.Drawing.Size(324, 20)
        Me.xbmcactorpath.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.xbmcactorpath, "Enter the path for the actors folder from XBMC."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"This may be a network path.")
        '
        'btn_localactorpathbrowse
        '
        Me.btn_localactorpathbrowse.Location = New System.Drawing.Point(419, 264)
        Me.btn_localactorpathbrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_localactorpathbrowse.Name = "btn_localactorpathbrowse"
        Me.btn_localactorpathbrowse.Size = New System.Drawing.Size(36, 23)
        Me.btn_localactorpathbrowse.TabIndex = 4
        Me.btn_localactorpathbrowse.Text = "..."
        Me.btn_localactorpathbrowse.UseVisualStyleBackColor = true
        '
        'Label161
        '
        Me.Label161.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label161.Location = New System.Drawing.Point(8, 230)
        Me.Label161.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(447, 30)
        Me.Label161.TabIndex = 10
        Me.Label161.Text = "The ""Local Path"" below needs to be the path to where you want the actor thumbs sa"& _ 
    "ved, eg ""C:\MovieStuff\ActorThumbs"""
        '
        'Label132
        '
        Me.Label132.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label132.Location = New System.Drawing.Point(8, 73)
        Me.Label132.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(447, 16)
        Me.Label132.TabIndex = 9
        Me.Label132.Text = "If these are set incorrectly, it could result in XBMC being unable to scrape acto"& _ 
    "r thumbs."
        '
        'Label104
        '
        Me.Label104.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label104.Location = New System.Drawing.Point(8, 18)
        Me.Label104.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(447, 55)
        Me.Label104.TabIndex = 8
        Me.Label104.Text = resources.GetString("Label104.Text")
        '
        'Label103
        '
        Me.Label103.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label103.Location = New System.Drawing.Point(8, 291)
        Me.Label103.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(447, 82)
        Me.Label103.TabIndex = 7
        Me.Label103.Text = resources.GetString("Label103.Text")
        '
        'Label101
        '
        Me.Label101.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label101.Location = New System.Drawing.Point(8, 128)
        Me.Label101.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(447, 94)
        Me.Label101.TabIndex = 6
        Me.Label101.Text = resources.GetString("Label101.Text")
        '
        'Label96
        '
        Me.Label96.AutoSize = true
        Me.Label96.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label96.Location = New System.Drawing.Point(8, 380)
        Me.Label96.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(80, 15)
        Me.Label96.TabIndex = 5
        Me.Label96.Text = "XBMC Path :-"
        '
        'Label97
        '
        Me.Label97.AutoSize = true
        Me.Label97.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label97.Location = New System.Drawing.Point(8, 266)
        Me.Label97.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(75, 15)
        Me.Label97.TabIndex = 2
        Me.Label97.Text = "Local Path :-"
        '
        'localactorpath
        '
        Me.localactorpath.Location = New System.Drawing.Point(87, 266)
        Me.localactorpath.Margin = New System.Windows.Forms.Padding(4)
        Me.localactorpath.Name = "localactorpath"
        Me.localactorpath.Size = New System.Drawing.Size(324, 20)
        Me.localactorpath.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.localactorpath, "The path for Media Companion to save the file")
        '
        'saveactorchkbx
        '
        Me.saveactorchkbx.AutoSize = true
        Me.saveactorchkbx.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.saveactorchkbx.Location = New System.Drawing.Point(9, 88)
        Me.saveactorchkbx.Margin = New System.Windows.Forms.Padding(4)
        Me.saveactorchkbx.Name = "saveactorchkbx"
        Me.saveactorchkbx.Size = New System.Drawing.Size(173, 19)
        Me.saveactorchkbx.TabIndex = 0
        Me.saveactorchkbx.Text = "Enable Save Actor Thumbs"
        Me.saveactorchkbx.UseVisualStyleBackColor = true
        '
        'ComboBox7
        '
        Me.ComboBox7.FormattingEnabled = true
        Me.ComboBox7.Items.AddRange(New Object() {"All Available", "None", "5", "10", "15", "20", "25", "30", "40", "50", "70", "90", "110", "125", "150", "175", "200", "250"})
        Me.ComboBox7.Location = New System.Drawing.Point(182, 134)
        Me.ComboBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox7.MaxDropDownItems = 30
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(137, 21)
        Me.ComboBox7.TabIndex = 64
        Me.ToolTip1.SetToolTip(Me.ComboBox7, "Media Companion will not scrape more than the number of actors set using this con"& _ 
        "trol")
        '
        'cbShowAllAudioTracks
        '
        Me.cbShowAllAudioTracks.AutoSize = true
        Me.cbShowAllAudioTracks.Location = New System.Drawing.Point(7, 263)
        Me.cbShowAllAudioTracks.Name = "cbShowAllAudioTracks"
        Me.cbShowAllAudioTracks.Size = New System.Drawing.Size(233, 17)
        Me.cbShowAllAudioTracks.TabIndex = 98
        Me.cbShowAllAudioTracks.Text = "Show all audio tracks in Media Info Overlay."
        Me.ToolTip1.SetToolTip(Me.cbShowAllAudioTracks, "Unchecked - Shows just the default audio track on the fanart image"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Checked - Sho"& _ 
        "ws all the audio tracks with the non-default ones greyed out.")
        Me.cbShowAllAudioTracks.UseVisualStyleBackColor = true
        '
        'cbDisplayMediaInfoOverlay
        '
        Me.cbDisplayMediaInfoOverlay.AutoSize = true
        Me.cbDisplayMediaInfoOverlay.Location = New System.Drawing.Point(7, 209)
        Me.cbDisplayMediaInfoOverlay.Name = "cbDisplayMediaInfoOverlay"
        Me.cbDisplayMediaInfoOverlay.Size = New System.Drawing.Size(202, 17)
        Me.cbDisplayMediaInfoOverlay.TabIndex = 96
        Me.cbDisplayMediaInfoOverlay.Text = "Display Media Info over Fanart Image"
        Me.ToolTip1.SetToolTip(Me.cbDisplayMediaInfoOverlay, "Shows Movie or Episode Media details"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Overlayed over Movie Fanart or Episode Thum"& _ 
        "b.")
        Me.cbDisplayMediaInfoOverlay.UseVisualStyleBackColor = true
        '
        'cbDisplayRatingOverlay
        '
        Me.cbDisplayRatingOverlay.AutoSize = true
        Me.cbDisplayRatingOverlay.Location = New System.Drawing.Point(7, 182)
        Me.cbDisplayRatingOverlay.Name = "cbDisplayRatingOverlay"
        Me.cbDisplayRatingOverlay.Size = New System.Drawing.Size(183, 17)
        Me.cbDisplayRatingOverlay.TabIndex = 95
        Me.cbDisplayRatingOverlay.Text = "Display Rating over Fanart Image"
        Me.ToolTip1.SetToolTip(Me.cbDisplayRatingOverlay, "Shows Movie or Episode Rating, Overlayed"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"over Movie Fanart or Episode Thumbnail "& _ 
        "Image.")
        Me.cbDisplayRatingOverlay.UseVisualStyleBackColor = true
        '
        'gbExcludeFolders
        '
        Me.gbExcludeFolders.Controls.Add(Me.tbExcludeFolders)
        Me.gbExcludeFolders.Location = New System.Drawing.Point(259, 298)
        Me.gbExcludeFolders.Name = "gbExcludeFolders"
        Me.gbExcludeFolders.Size = New System.Drawing.Size(277, 95)
        Me.gbExcludeFolders.TabIndex = 94
        Me.gbExcludeFolders.TabStop = false
        Me.gbExcludeFolders.Text = "Exclude Folders from scrape"
        Me.ToolTip1.SetToolTip(Me.gbExcludeFolders, "Trailing ""*"" pattern matching supported. E.g. ""._*"" excludes all folders beginnin"& _ 
        "g ""._""")
        '
        'tbExcludeFolders
        '
        Me.tbExcludeFolders.Location = New System.Drawing.Point(6, 22)
        Me.tbExcludeFolders.Multiline = true
        Me.tbExcludeFolders.Name = "tbExcludeFolders"
        Me.tbExcludeFolders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbExcludeFolders.Size = New System.Drawing.Size(265, 62)
        Me.tbExcludeFolders.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(589, 496)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 50)
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = false
        Me.PictureBox1.Visible = false
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.lbl_MediaPlayerUser)
        Me.GroupBox3.Controls.Add(Me.btn_MediaPlayerBrowse)
        Me.GroupBox3.Controls.Add(Me.rb_MediaPlayerUser)
        Me.GroupBox3.Controls.Add(Me.rb_MediaPlayerWMP)
        Me.GroupBox3.Controls.Add(Me.rb_MediaPlayerDefault)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 47)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(456, 136)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Media Player"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(421, 26)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Media Companion can be used to playback media files.  Use the options below to se"& _ 
    "lect"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"what program to use for playback.  Multipart media files utilises a m3u pl"& _ 
    "aylist."
        '
        'lbl_MediaPlayerUser
        '
        Me.lbl_MediaPlayerUser.AutoSize = true
        Me.lbl_MediaPlayerUser.Location = New System.Drawing.Point(6, 111)
        Me.lbl_MediaPlayerUser.Name = "lbl_MediaPlayerUser"
        Me.lbl_MediaPlayerUser.Size = New System.Drawing.Size(11, 13)
        Me.lbl_MediaPlayerUser.TabIndex = 8
        Me.lbl_MediaPlayerUser.Text = "."
        '
        'btn_MediaPlayerBrowse
        '
        Me.btn_MediaPlayerBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_MediaPlayerBrowse.Location = New System.Drawing.Point(168, 88)
        Me.btn_MediaPlayerBrowse.Name = "btn_MediaPlayerBrowse"
        Me.btn_MediaPlayerBrowse.Size = New System.Drawing.Size(185, 23)
        Me.btn_MediaPlayerBrowse.TabIndex = 7
        Me.btn_MediaPlayerBrowse.Text = "Browse to prefferred media player"
        Me.btn_MediaPlayerBrowse.UseVisualStyleBackColor = true
        '
        'rb_MediaPlayerUser
        '
        Me.rb_MediaPlayerUser.AutoSize = true
        Me.rb_MediaPlayerUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rb_MediaPlayerUser.Location = New System.Drawing.Point(6, 91)
        Me.rb_MediaPlayerUser.Name = "rb_MediaPlayerUser"
        Me.rb_MediaPlayerUser.Size = New System.Drawing.Size(129, 17)
        Me.rb_MediaPlayerUser.TabIndex = 6
        Me.rb_MediaPlayerUser.Text = "Select Different player"
        Me.rb_MediaPlayerUser.UseVisualStyleBackColor = true
        '
        'rb_MediaPlayerWMP
        '
        Me.rb_MediaPlayerWMP.AutoSize = true
        Me.rb_MediaPlayerWMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rb_MediaPlayerWMP.Location = New System.Drawing.Point(6, 68)
        Me.rb_MediaPlayerWMP.Name = "rb_MediaPlayerWMP"
        Me.rb_MediaPlayerWMP.Size = New System.Drawing.Size(367, 17)
        Me.rb_MediaPlayerWMP.TabIndex = 1
        Me.rb_MediaPlayerWMP.Text = "Use Windows Media Player --- Launch WMP and play the selected file(s)"
        Me.rb_MediaPlayerWMP.UseVisualStyleBackColor = true
        '
        'rb_MediaPlayerDefault
        '
        Me.rb_MediaPlayerDefault.AutoSize = true
        Me.rb_MediaPlayerDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rb_MediaPlayerDefault.Location = New System.Drawing.Point(6, 45)
        Me.rb_MediaPlayerDefault.Name = "rb_MediaPlayerDefault"
        Me.rb_MediaPlayerDefault.Size = New System.Drawing.Size(446, 17)
        Me.rb_MediaPlayerDefault.TabIndex = 0
        Me.rb_MediaPlayerDefault.Text = "Use Default Player  ---------------  This option will open the default player for"& _ 
    " .m3u playlist files"
        Me.rb_MediaPlayerDefault.UseVisualStyleBackColor = true
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPCommon)
        Me.TabControl1.Controls.Add(Me.TPGen)
        Me.TabControl1.Controls.Add(Me.TPMovPref)
        Me.TabControl1.Controls.Add(Me.TPTVPref)
        Me.TabControl1.Controls.Add(Me.TPProxy)
        Me.TabControl1.Controls.Add(Me.TPXBMCLink)
        Me.TabControl1.Controls.Add(Me.TPPRofCmd)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1008, 581)
        Me.TabControl1.TabIndex = 15
        '
        'TPCommon
        '
        Me.TPCommon.Controls.Add(Me.TabControl4)
        Me.TPCommon.Location = New System.Drawing.Point(4, 22)
        Me.TPCommon.Name = "TPCommon"
        Me.TPCommon.Size = New System.Drawing.Size(1000, 555)
        Me.TPCommon.TabIndex = 5
        Me.TPCommon.Text = "Common"
        Me.TPCommon.UseVisualStyleBackColor = true
        '
        'TabControl4
        '
        Me.TabControl4.Controls.Add(Me.TPCommonSettings)
        Me.TabControl4.Controls.Add(Me.TPActors)
        Me.TabControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl4.Location = New System.Drawing.Point(0, 0)
        Me.TabControl4.Name = "TabControl4"
        Me.TabControl4.SelectedIndex = 0
        Me.TabControl4.Size = New System.Drawing.Size(1000, 555)
        Me.TabControl4.TabIndex = 0
        '
        'TPCommonSettings
        '
        Me.TPCommonSettings.Controls.Add(Me.Label185)
        Me.TPCommonSettings.Controls.Add(Me.AutoScrnShtDelay)
        Me.TPCommonSettings.Controls.Add(Me.Label4)
        Me.TPCommonSettings.Controls.Add(Me.gbImageResizing)
        Me.TPCommonSettings.Controls.Add(Me.grpCleanFilename)
        Me.TPCommonSettings.Controls.Add(Me.grpVideoSource)
        Me.TPCommonSettings.Controls.Add(Me.cbShowAllAudioTracks)
        Me.TPCommonSettings.Controls.Add(Me.cbDisplayMediaInfoOverlay)
        Me.TPCommonSettings.Controls.Add(Me.cbDisplayMediaInfoFolderSize)
        Me.TPCommonSettings.Controls.Add(Me.cbDisplayRatingOverlay)
        Me.TPCommonSettings.Controls.Add(Me.gbExcludeFolders)
        Me.TPCommonSettings.Controls.Add(Me.cb_IgnoreAn)
        Me.TPCommonSettings.Controls.Add(Me.cb_SorttitleIgnoreArticles)
        Me.TPCommonSettings.Controls.Add(Me.cb_IgnoreA)
        Me.TPCommonSettings.Controls.Add(Me.cbOverwriteArtwork)
        Me.TPCommonSettings.Controls.Add(Me.cb_IgnoreThe)
        Me.TPCommonSettings.Controls.Add(Me.CheckBox38)
        Me.TPCommonSettings.Controls.Add(Me.gbxXBMCversion)
        Me.TPCommonSettings.Location = New System.Drawing.Point(4, 22)
        Me.TPCommonSettings.Name = "TPCommonSettings"
        Me.TPCommonSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.TPCommonSettings.Size = New System.Drawing.Size(992, 529)
        Me.TPCommonSettings.TabIndex = 0
        Me.TPCommonSettings.Text = "Common Settings"
        Me.TPCommonSettings.UseVisualStyleBackColor = true
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(348, 26)
        Me.Label4.TabIndex = 102
        Me.Label4.Text = "Common Across All Media Scrapers"
        '
        'gbImageResizing
        '
        Me.gbImageResizing.Controls.Add(Me.Label171)
        Me.gbImageResizing.Controls.Add(Me.Label175)
        Me.gbImageResizing.Controls.Add(Me.Label176)
        Me.gbImageResizing.Controls.Add(Me.comboActorResolutions)
        Me.gbImageResizing.Controls.Add(Me.comboBackDropResolutions)
        Me.gbImageResizing.Controls.Add(Me.comboPosterResolutions)
        Me.gbImageResizing.Location = New System.Drawing.Point(476, 6)
        Me.gbImageResizing.Name = "gbImageResizing"
        Me.gbImageResizing.Size = New System.Drawing.Size(307, 131)
        Me.gbImageResizing.TabIndex = 101
        Me.gbImageResizing.TabStop = false
        Me.gbImageResizing.Text = " Image Resizing "
        '
        'Label171
        '
        Me.Label171.AutoSize = true
        Me.Label171.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label171.Location = New System.Drawing.Point(6, 88)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(112, 15)
        Me.Label171.TabIndex = 54
        Me.Label171.Text = "Fanart dimensions:"
        '
        'Label175
        '
        Me.Label175.AutoSize = true
        Me.Label175.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label175.Location = New System.Drawing.Point(6, 34)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(77, 15)
        Me.Label175.TabIndex = 53
        Me.Label175.Text = "Actor height :"
        '
        'Label176
        '
        Me.Label176.AutoSize = true
        Me.Label176.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label176.Location = New System.Drawing.Point(6, 61)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(85, 15)
        Me.Label176.TabIndex = 52
        Me.Label176.Text = "Poster height :"
        '
        'comboActorResolutions
        '
        Me.comboActorResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboActorResolutions.FormattingEnabled = true
        Me.comboActorResolutions.Location = New System.Drawing.Point(124, 31)
        Me.comboActorResolutions.Name = "comboActorResolutions"
        Me.comboActorResolutions.Size = New System.Drawing.Size(170, 21)
        Me.comboActorResolutions.TabIndex = 51
        '
        'comboBackDropResolutions
        '
        Me.comboBackDropResolutions.FormattingEnabled = true
        Me.comboBackDropResolutions.Location = New System.Drawing.Point(124, 85)
        Me.comboBackDropResolutions.Name = "comboBackDropResolutions"
        Me.comboBackDropResolutions.Size = New System.Drawing.Size(170, 21)
        Me.comboBackDropResolutions.TabIndex = 50
        '
        'comboPosterResolutions
        '
        Me.comboPosterResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboPosterResolutions.FormattingEnabled = true
        Me.comboPosterResolutions.Location = New System.Drawing.Point(124, 58)
        Me.comboPosterResolutions.Name = "comboPosterResolutions"
        Me.comboPosterResolutions.Size = New System.Drawing.Size(170, 21)
        Me.comboPosterResolutions.TabIndex = 49
        '
        'grpCleanFilename
        '
        Me.grpCleanFilename.Controls.Add(Me.btnCleanFilenameRemove)
        Me.grpCleanFilename.Controls.Add(Me.txtCleanFilenameAdd)
        Me.grpCleanFilename.Controls.Add(Me.btnCleanFilenameAdd)
        Me.grpCleanFilename.Controls.Add(Me.lbCleanFilename)
        Me.grpCleanFilename.Location = New System.Drawing.Point(542, 143)
        Me.grpCleanFilename.Name = "grpCleanFilename"
        Me.grpCleanFilename.Size = New System.Drawing.Size(241, 356)
        Me.grpCleanFilename.TabIndex = 100
        Me.grpCleanFilename.TabStop = false
        Me.grpCleanFilename.Text = "Clean Filename Settings"
        '
        'btnCleanFilenameRemove
        '
        Me.btnCleanFilenameRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCleanFilenameRemove.Location = New System.Drawing.Point(124, 289)
        Me.btnCleanFilenameRemove.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCleanFilenameRemove.Name = "btnCleanFilenameRemove"
        Me.btnCleanFilenameRemove.Size = New System.Drawing.Size(100, 21)
        Me.btnCleanFilenameRemove.TabIndex = 8
        Me.btnCleanFilenameRemove.Text = "Remove Selected"
        Me.btnCleanFilenameRemove.UseVisualStyleBackColor = true
        '
        'txtCleanFilenameAdd
        '
        Me.txtCleanFilenameAdd.Location = New System.Drawing.Point(6, 314)
        Me.txtCleanFilenameAdd.Name = "txtCleanFilenameAdd"
        Me.txtCleanFilenameAdd.Size = New System.Drawing.Size(179, 20)
        Me.txtCleanFilenameAdd.TabIndex = 7
        '
        'btnCleanFilenameAdd
        '
        Me.btnCleanFilenameAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCleanFilenameAdd.Location = New System.Drawing.Point(188, 313)
        Me.btnCleanFilenameAdd.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCleanFilenameAdd.Name = "btnCleanFilenameAdd"
        Me.btnCleanFilenameAdd.Size = New System.Drawing.Size(36, 21)
        Me.btnCleanFilenameAdd.TabIndex = 6
        Me.btnCleanFilenameAdd.Text = "Add"
        Me.btnCleanFilenameAdd.UseVisualStyleBackColor = true
        '
        'lbCleanFilename
        '
        Me.lbCleanFilename.FormattingEnabled = true
        Me.lbCleanFilename.Location = New System.Drawing.Point(6, 20)
        Me.lbCleanFilename.Name = "lbCleanFilename"
        Me.lbCleanFilename.Size = New System.Drawing.Size(218, 264)
        Me.lbCleanFilename.TabIndex = 0
        '
        'grpVideoSource
        '
        Me.grpVideoSource.Controls.Add(Me.btnVideoSourceRemove)
        Me.grpVideoSource.Controls.Add(Me.txtVideoSourceAdd)
        Me.grpVideoSource.Controls.Add(Me.btnVideoSourceAdd)
        Me.grpVideoSource.Controls.Add(Me.lbVideoSource)
        Me.grpVideoSource.Location = New System.Drawing.Point(789, 4)
        Me.grpVideoSource.Name = "grpVideoSource"
        Me.grpVideoSource.Size = New System.Drawing.Size(197, 498)
        Me.grpVideoSource.TabIndex = 99
        Me.grpVideoSource.TabStop = false
        Me.grpVideoSource.Text = "Video Source"
        '
        'btnVideoSourceRemove
        '
        Me.btnVideoSourceRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnVideoSourceRemove.Location = New System.Drawing.Point(91, 428)
        Me.btnVideoSourceRemove.Margin = New System.Windows.Forms.Padding(0)
        Me.btnVideoSourceRemove.Name = "btnVideoSourceRemove"
        Me.btnVideoSourceRemove.Size = New System.Drawing.Size(100, 21)
        Me.btnVideoSourceRemove.TabIndex = 4
        Me.btnVideoSourceRemove.Text = "Remove Selected"
        Me.btnVideoSourceRemove.UseVisualStyleBackColor = true
        '
        'txtVideoSourceAdd
        '
        Me.txtVideoSourceAdd.Location = New System.Drawing.Point(6, 453)
        Me.txtVideoSourceAdd.Name = "txtVideoSourceAdd"
        Me.txtVideoSourceAdd.Size = New System.Drawing.Size(145, 20)
        Me.txtVideoSourceAdd.TabIndex = 3
        '
        'btnVideoSourceAdd
        '
        Me.btnVideoSourceAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnVideoSourceAdd.Location = New System.Drawing.Point(155, 452)
        Me.btnVideoSourceAdd.Margin = New System.Windows.Forms.Padding(0)
        Me.btnVideoSourceAdd.Name = "btnVideoSourceAdd"
        Me.btnVideoSourceAdd.Size = New System.Drawing.Size(36, 21)
        Me.btnVideoSourceAdd.TabIndex = 2
        Me.btnVideoSourceAdd.Text = "Add"
        Me.btnVideoSourceAdd.UseVisualStyleBackColor = true
        '
        'lbVideoSource
        '
        Me.lbVideoSource.FormattingEnabled = true
        Me.lbVideoSource.Location = New System.Drawing.Point(6, 17)
        Me.lbVideoSource.Margin = New System.Windows.Forms.Padding(0)
        Me.lbVideoSource.Name = "lbVideoSource"
        Me.lbVideoSource.Size = New System.Drawing.Size(185, 407)
        Me.lbVideoSource.TabIndex = 0
        '
        'cbDisplayMediaInfoFolderSize
        '
        Me.cbDisplayMediaInfoFolderSize.AutoSize = true
        Me.cbDisplayMediaInfoFolderSize.Location = New System.Drawing.Point(7, 236)
        Me.cbDisplayMediaInfoFolderSize.Name = "cbDisplayMediaInfoFolderSize"
        Me.cbDisplayMediaInfoFolderSize.Size = New System.Drawing.Size(204, 17)
        Me.cbDisplayMediaInfoFolderSize.TabIndex = 97
        Me.cbDisplayMediaInfoFolderSize.Text = "Display Folder Size over Fanart Image"
        Me.cbDisplayMediaInfoFolderSize.UseVisualStyleBackColor = true
        '
        'cb_IgnoreAn
        '
        Me.cb_IgnoreAn.AutoSize = true
        Me.cb_IgnoreAn.Location = New System.Drawing.Point(7, 109)
        Me.cb_IgnoreAn.Name = "cb_IgnoreAn"
        Me.cb_IgnoreAn.Size = New System.Drawing.Size(176, 17)
        Me.cb_IgnoreAn.TabIndex = 93
        Me.cb_IgnoreAn.Text = "Ignore article ""An"" when sorting"
        Me.cb_IgnoreAn.UseVisualStyleBackColor = true
        '
        'cb_SorttitleIgnoreArticles
        '
        Me.cb_SorttitleIgnoreArticles.AutoSize = true
        Me.cb_SorttitleIgnoreArticles.Location = New System.Drawing.Point(7, 134)
        Me.cb_SorttitleIgnoreArticles.Name = "cb_SorttitleIgnoreArticles"
        Me.cb_SorttitleIgnoreArticles.Size = New System.Drawing.Size(218, 17)
        Me.cb_SorttitleIgnoreArticles.TabIndex = 92
        Me.cb_SorttitleIgnoreArticles.Text = "Move Ignored articles to end of Sort Title"
        Me.cb_SorttitleIgnoreArticles.UseVisualStyleBackColor = true
        '
        'cb_IgnoreA
        '
        Me.cb_IgnoreA.AutoSize = true
        Me.cb_IgnoreA.Location = New System.Drawing.Point(7, 84)
        Me.cb_IgnoreA.Name = "cb_IgnoreA"
        Me.cb_IgnoreA.Size = New System.Drawing.Size(176, 17)
        Me.cb_IgnoreA.TabIndex = 91
        Me.cb_IgnoreA.Text = "Ignore article ""A ""  when sorting"
        Me.cb_IgnoreA.UseVisualStyleBackColor = true
        '
        'cbOverwriteArtwork
        '
        Me.cbOverwriteArtwork.AutoSize = true
        Me.cbOverwriteArtwork.Location = New System.Drawing.Point(7, 159)
        Me.cbOverwriteArtwork.Name = "cbOverwriteArtwork"
        Me.cbOverwriteArtwork.Size = New System.Drawing.Size(174, 17)
        Me.cbOverwriteArtwork.TabIndex = 90
        Me.cbOverwriteArtwork.Text = "Don’t overwrite existing artwork"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        Me.cbOverwriteArtwork.UseVisualStyleBackColor = true
        '
        'cb_IgnoreThe
        '
        Me.cb_IgnoreThe.AutoSize = true
        Me.cb_IgnoreThe.Location = New System.Drawing.Point(7, 62)
        Me.cb_IgnoreThe.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_IgnoreThe.Name = "cb_IgnoreThe"
        Me.cb_IgnoreThe.Size = New System.Drawing.Size(185, 17)
        Me.cb_IgnoreThe.TabIndex = 89
        Me.cb_IgnoreThe.Text = "Ignore article ""The "" when sorting"
        Me.cb_IgnoreThe.UseVisualStyleBackColor = true
        '
        'CheckBox38
        '
        Me.CheckBox38.AutoSize = true
        Me.CheckBox38.Location = New System.Drawing.Point(7, 41)
        Me.CheckBox38.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox38.Name = "CheckBox38"
        Me.CheckBox38.Size = New System.Drawing.Size(203, 17)
        Me.CheckBox38.TabIndex = 88
        Me.CheckBox38.Text = "Save media runtime as numerical only"
        Me.CheckBox38.UseVisualStyleBackColor = true
        '
        'gbxXBMCversion
        '
        Me.gbxXBMCversion.Controls.Add(Me.Label129)
        Me.gbxXBMCversion.Controls.Add(Me.rbXBMCv_both)
        Me.gbxXBMCversion.Controls.Add(Me.rbXBMCv_post)
        Me.gbxXBMCversion.Controls.Add(Me.rbXBMCv_pre)
        Me.gbxXBMCversion.Location = New System.Drawing.Point(259, 144)
        Me.gbxXBMCversion.Name = "gbxXBMCversion"
        Me.gbxXBMCversion.Size = New System.Drawing.Size(277, 146)
        Me.gbxXBMCversion.TabIndex = 48
        Me.gbxXBMCversion.TabStop = false
        Me.gbxXBMCversion.Text = "Artwork Version"
        '
        'Label129
        '
        Me.Label129.Location = New System.Drawing.Point(6, 17)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(265, 58)
        Me.Label129.TabIndex = 1
        Me.Label129.Text = "With the introduction of XBMCs Frodo comes a new way of defining media artwork."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)& _ 
    "Choose an option below which best defines your setup."
        '
        'rbXBMCv_both
        '
        Me.rbXBMCv_both.AutoSize = true
        Me.rbXBMCv_both.Location = New System.Drawing.Point(9, 118)
        Me.rbXBMCv_both.Name = "rbXBMCv_both"
        Me.rbXBMCv_both.Size = New System.Drawing.Size(47, 17)
        Me.rbXBMCv_both.TabIndex = 0
        Me.rbXBMCv_both.TabStop = true
        Me.rbXBMCv_both.Text = "Both"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        Me.rbXBMCv_both.UseVisualStyleBackColor = true
        '
        'rbXBMCv_post
        '
        Me.rbXBMCv_post.AutoSize = true
        Me.rbXBMCv_post.Checked = true
        Me.rbXBMCv_post.Location = New System.Drawing.Point(9, 98)
        Me.rbXBMCv_post.Name = "rbXBMCv_post"
        Me.rbXBMCv_post.Size = New System.Drawing.Size(157, 17)
        Me.rbXBMCv_post.TabIndex = 0
        Me.rbXBMCv_post.TabStop = true
        Me.rbXBMCv_post.Text = "Frodo and onwards (default)"
        Me.rbXBMCv_post.UseVisualStyleBackColor = true
        '
        'rbXBMCv_pre
        '
        Me.rbXBMCv_pre.AutoSize = true
        Me.rbXBMCv_pre.Location = New System.Drawing.Point(9, 78)
        Me.rbXBMCv_pre.Name = "rbXBMCv_pre"
        Me.rbXBMCv_pre.Size = New System.Drawing.Size(71, 17)
        Me.rbXBMCv_pre.TabIndex = 0
        Me.rbXBMCv_pre.Text = "Pre-Frodo"
        Me.rbXBMCv_pre.UseVisualStyleBackColor = true
        '
        'TPActors
        '
        Me.TPActors.Controls.Add(Me.Label98)
        Me.TPActors.Controls.Add(Me.ComboBox7)
        Me.TPActors.Controls.Add(Me.GroupBox12)
        Me.TPActors.Controls.Add(Me.GroupBox32)
        Me.TPActors.Location = New System.Drawing.Point(4, 22)
        Me.TPActors.Name = "TPActors"
        Me.TPActors.Padding = New System.Windows.Forms.Padding(3)
        Me.TPActors.Size = New System.Drawing.Size(992, 529)
        Me.TPActors.TabIndex = 1
        Me.TPActors.Text = "Actor(s)"
        Me.TPActors.UseVisualStyleBackColor = true
        '
        'Label98
        '
        Me.Label98.AutoSize = true
        Me.Label98.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label98.Location = New System.Drawing.Point(16, 139)
        Me.Label98.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(161, 15)
        Me.Label98.TabIndex = 63
        Me.Label98.Text = "Maximum number of actors:"
        '
        'GroupBox32
        '
        Me.GroupBox32.Controls.Add(Me.Label137)
        Me.GroupBox32.Controls.Add(Me.cb_actorseasy)
        Me.GroupBox32.Location = New System.Drawing.Point(12, 33)
        Me.GroupBox32.Name = "GroupBox32"
        Me.GroupBox32.Size = New System.Drawing.Size(423, 79)
        Me.GroupBox32.TabIndex = 48
        Me.GroupBox32.TabStop = false
        Me.GroupBox32.Text = "Actor Folder"
        '
        'Label137
        '
        Me.Label137.Location = New System.Drawing.Point(7, 17)
        Me.Label137.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(406, 30)
        Me.Label137.TabIndex = 38
        Me.Label137.Text = "Media Companion has the function to scrape actor thumbnails to a folder named '.a"& _ 
    "ctors' (located in the same directory as the movie)."
        '
        'cb_actorseasy
        '
        Me.cb_actorseasy.AutoSize = true
        Me.cb_actorseasy.Location = New System.Drawing.Point(7, 51)
        Me.cb_actorseasy.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_actorseasy.Name = "cb_actorseasy"
        Me.cb_actorseasy.Size = New System.Drawing.Size(222, 17)
        Me.cb_actorseasy.TabIndex = 37
        Me.cb_actorseasy.Text = "Save Actor Thumbs to the '.Actors' Folder"
        Me.cb_actorseasy.UseVisualStyleBackColor = true
        '
        'TPGen
        '
        Me.TPGen.Controls.Add(Me.Label2)
        Me.TPGen.Controls.Add(Me.cbMultiMonitorEnable)
        Me.TPGen.Controls.Add(Me.cbDisplayLocalActor)
        Me.TPGen.Controls.Add(Me.cbCheckForNewVersion)
        Me.TPGen.Controls.Add(Me.cbRenameNFOtoINFO)
        Me.TPGen.Controls.Add(Me.cbUseMultipleThreads)
        Me.TPGen.Controls.Add(Me.cbShowLogOnError)
        Me.TPGen.Controls.Add(Me.btnFindBrowser)
        Me.TPGen.Controls.Add(Me.cbExternalbrowser)
        Me.TPGen.Controls.Add(Me.chkbx_disablecache)
        Me.TPGen.Controls.Add(Me.GroupBox45)
        Me.TPGen.Controls.Add(Me.GroupBox36)
        Me.TPGen.Controls.Add(Me.GroupBox33)
        Me.TPGen.Controls.Add(Me.GroupBox31)
        Me.TPGen.Controls.Add(Me.GroupBox3)
        Me.TPGen.Location = New System.Drawing.Point(4, 22)
        Me.TPGen.Name = "TPGen"
        Me.TPGen.Size = New System.Drawing.Size(1000, 555)
        Me.TPGen.TabIndex = 4
        Me.TPGen.Text = "General"
        Me.TPGen.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(725, 22)
        Me.Label2.TabIndex = 99
        Me.Label2.Text = "Options on this Tab are General Media Companion options not specific to any Video"& _ 
    " type."
        '
        'cbMultiMonitorEnable
        '
        Me.cbMultiMonitorEnable.AutoSize = true
        Me.cbMultiMonitorEnable.Location = New System.Drawing.Point(349, 370)
        Me.cbMultiMonitorEnable.Name = "cbMultiMonitorEnable"
        Me.cbMultiMonitorEnable.Size = New System.Drawing.Size(162, 17)
        Me.cbMultiMonitorEnable.TabIndex = 98
        Me.cbMultiMonitorEnable.Text = "Enable Multi-Monitor Support"
        Me.cbMultiMonitorEnable.UseVisualStyleBackColor = true
        '
        'cbRenameNFOtoINFO
        '
        Me.cbRenameNFOtoINFO.AutoSize = true
        Me.cbRenameNFOtoINFO.Location = New System.Drawing.Point(349, 347)
        Me.cbRenameNFOtoINFO.Name = "cbRenameNFOtoINFO"
        Me.cbRenameNFOtoINFO.Size = New System.Drawing.Size(236, 17)
        Me.cbRenameNFOtoINFO.TabIndex = 95
        Me.cbRenameNFOtoINFO.Text = "Rename Non-Compliant Scene '.nfo' to '.info'"
        Me.cbRenameNFOtoINFO.UseVisualStyleBackColor = true
        '
        'cbShowLogOnError
        '
        Me.cbShowLogOnError.AutoSize = true
        Me.cbShowLogOnError.Location = New System.Drawing.Point(18, 397)
        Me.cbShowLogOnError.Margin = New System.Windows.Forms.Padding(4)
        Me.cbShowLogOnError.Name = "cbShowLogOnError"
        Me.cbShowLogOnError.Size = New System.Drawing.Size(109, 17)
        Me.cbShowLogOnError.TabIndex = 93
        Me.cbShowLogOnError.Text = "Show log on error"
        Me.cbShowLogOnError.UseVisualStyleBackColor = true
        '
        'cbExternalbrowser
        '
        Me.cbExternalbrowser.AutoSize = true
        Me.cbExternalbrowser.Location = New System.Drawing.Point(18, 295)
        Me.cbExternalbrowser.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExternalbrowser.Name = "cbExternalbrowser"
        Me.cbExternalbrowser.Size = New System.Drawing.Size(289, 17)
        Me.cbExternalbrowser.TabIndex = 91
        Me.cbExternalbrowser.Text = "Use external Browser to display IMDB/TVDB webpages"
        Me.cbExternalbrowser.UseVisualStyleBackColor = true
        '
        'chkbx_disablecache
        '
        Me.chkbx_disablecache.AutoSize = true
        Me.chkbx_disablecache.Location = New System.Drawing.Point(18, 350)
        Me.chkbx_disablecache.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbx_disablecache.Name = "chkbx_disablecache"
        Me.chkbx_disablecache.Size = New System.Drawing.Size(275, 17)
        Me.chkbx_disablecache.TabIndex = 90
        Me.chkbx_disablecache.Text = "Disable caching of Media DB (will slow down startup)"
        Me.chkbx_disablecache.UseVisualStyleBackColor = true
        '
        'GroupBox45
        '
        Me.GroupBox45.Controls.Add(Me.lblaltnfoeditorclear)
        Me.GroupBox45.Controls.Add(Me.btnaltnfoeditor)
        Me.GroupBox45.Controls.Add(Me.tbaltnfoeditor)
        Me.GroupBox45.Controls.Add(Me.Label20)
        Me.GroupBox45.Location = New System.Drawing.Point(480, 138)
        Me.GroupBox45.Name = "GroupBox45"
        Me.GroupBox45.Size = New System.Drawing.Size(364, 66)
        Me.GroupBox45.TabIndex = 89
        Me.GroupBox45.TabStop = false
        Me.GroupBox45.Text = "Alternative nfo viewer/editor"
        '
        'lblaltnfoeditorclear
        '
        Me.lblaltnfoeditorclear.AutoSize = true
        Me.lblaltnfoeditorclear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblaltnfoeditorclear.Location = New System.Drawing.Point(55, 41)
        Me.lblaltnfoeditorclear.Name = "lblaltnfoeditorclear"
        Me.lblaltnfoeditorclear.Size = New System.Drawing.Size(57, 15)
        Me.lblaltnfoeditorclear.TabIndex = 5
        Me.lblaltnfoeditorclear.Text = "Clear path"
        '
        'btnaltnfoeditor
        '
        Me.btnaltnfoeditor.Location = New System.Drawing.Point(332, 15)
        Me.btnaltnfoeditor.Name = "btnaltnfoeditor"
        Me.btnaltnfoeditor.Size = New System.Drawing.Size(26, 23)
        Me.btnaltnfoeditor.TabIndex = 4
        Me.btnaltnfoeditor.Text = "..."
        Me.btnaltnfoeditor.UseVisualStyleBackColor = true
        '
        'Label20
        '
        Me.Label20.AutoSize = true
        Me.Label20.Location = New System.Drawing.Point(10, 20)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(32, 13)
        Me.Label20.TabIndex = 4
        Me.Label20.Text = "Path:"
        '
        'GroupBox33
        '
        Me.GroupBox33.Controls.Add(Me.btnFontSelect)
        Me.GroupBox33.Controls.Add(Me.btnFontReset)
        Me.GroupBox33.Controls.Add(Me.lbl_FontSample)
        Me.GroupBox33.Location = New System.Drawing.Point(480, 47)
        Me.GroupBox33.Name = "GroupBox33"
        Me.GroupBox33.Size = New System.Drawing.Size(364, 85)
        Me.GroupBox33.TabIndex = 49
        Me.GroupBox33.TabStop = false
        Me.GroupBox33.Text = "Interface Font"
        '
        'btnFontSelect
        '
        Me.btnFontSelect.Location = New System.Drawing.Point(7, 21)
        Me.btnFontSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFontSelect.Name = "btnFontSelect"
        Me.btnFontSelect.Size = New System.Drawing.Size(90, 26)
        Me.btnFontSelect.TabIndex = 34
        Me.btnFontSelect.Text = "Select"
        Me.btnFontSelect.UseVisualStyleBackColor = true
        '
        'btnFontReset
        '
        Me.btnFontReset.Location = New System.Drawing.Point(104, 21)
        Me.btnFontReset.Name = "btnFontReset"
        Me.btnFontReset.Size = New System.Drawing.Size(75, 26)
        Me.btnFontReset.TabIndex = 41
        Me.btnFontReset.Text = "Reset Font"
        Me.btnFontReset.UseVisualStyleBackColor = true
        '
        'lbl_FontSample
        '
        Me.lbl_FontSample.AutoSize = true
        Me.lbl_FontSample.Location = New System.Drawing.Point(10, 59)
        Me.lbl_FontSample.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_FontSample.Name = "lbl_FontSample"
        Me.lbl_FontSample.Size = New System.Drawing.Size(66, 13)
        Me.lbl_FontSample.TabIndex = 36
        Me.lbl_FontSample.Text = "Sample Font"
        Me.lbl_FontSample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox31
        '
        Me.GroupBox31.Controls.Add(Me.Label116)
        Me.GroupBox31.Controls.Add(Me.Label107)
        Me.GroupBox31.Controls.Add(Me.txtbx_minrarsize)
        Me.GroupBox31.Location = New System.Drawing.Point(8, 189)
        Me.GroupBox31.Name = "GroupBox31"
        Me.GroupBox31.Size = New System.Drawing.Size(456, 88)
        Me.GroupBox31.TabIndex = 46
        Me.GroupBox31.TabStop = false
        Me.GroupBox31.Text = "RAR Archives"
        '
        'Label116
        '
        Me.Label116.Location = New System.Drawing.Point(7, 17)
        Me.Label116.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(442, 33)
        Me.Label116.TabIndex = 32
        Me.Label116.Text = "Media Companion can scrape data for RAR archives, to avoid scraping non-media arc"& _ 
    "hives (eg. subtitles)."
        '
        'Label107
        '
        Me.Label107.AutoSize = true
        Me.Label107.Location = New System.Drawing.Point(81, 57)
        Me.Label107.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(271, 13)
        Me.Label107.TabIndex = 27
        Me.Label107.Text = "File size in MB (archives smaller than this will be ignored)"
        '
        'txtbx_minrarsize
        '
        Me.txtbx_minrarsize.Location = New System.Drawing.Point(10, 54)
        Me.txtbx_minrarsize.Margin = New System.Windows.Forms.Padding(4)
        Me.txtbx_minrarsize.Name = "txtbx_minrarsize"
        Me.txtbx_minrarsize.Size = New System.Drawing.Size(63, 20)
        Me.txtbx_minrarsize.TabIndex = 26
        '
        'TPMovPref
        '
        Me.TPMovPref.Location = New System.Drawing.Point(4, 22)
        Me.TPMovPref.Name = "TPMovPref"
        Me.TPMovPref.Size = New System.Drawing.Size(1000, 555)
        Me.TPMovPref.TabIndex = 7
        Me.TPMovPref.Text = "Movie Preferences"
        Me.TPMovPref.UseVisualStyleBackColor = true
        '
        'TPTVPref
        '
        Me.TPTVPref.Location = New System.Drawing.Point(4, 22)
        Me.TPTVPref.Name = "TPTVPref"
        Me.TPTVPref.Size = New System.Drawing.Size(1000, 555)
        Me.TPTVPref.TabIndex = 8
        Me.TPTVPref.Text = "TV Preferences"
        Me.TPTVPref.UseVisualStyleBackColor = true
        '
        'TPProxy
        '
        Me.TPProxy.Controls.Add(Me.UcGenPref_Proxy1)
        Me.TPProxy.Location = New System.Drawing.Point(4, 22)
        Me.TPProxy.Name = "TPProxy"
        Me.TPProxy.Size = New System.Drawing.Size(1000, 555)
        Me.TPProxy.TabIndex = 10
        Me.TPProxy.Text = "Proxy"
        Me.TPProxy.UseVisualStyleBackColor = true
        '
        'UcGenPref_Proxy1
        '
        Me.UcGenPref_Proxy1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcGenPref_Proxy1.Location = New System.Drawing.Point(0, 0)
        Me.UcGenPref_Proxy1.Name = "UcGenPref_Proxy1"
        Me.UcGenPref_Proxy1.Size = New System.Drawing.Size(1000, 555)
        Me.UcGenPref_Proxy1.TabIndex = 0
        '
        'TPXBMCLink
        '
        Me.TPXBMCLink.Controls.Add(Me.UcGenPref_XbmcLink1)
        Me.TPXBMCLink.Location = New System.Drawing.Point(4, 22)
        Me.TPXBMCLink.Name = "TPXBMCLink"
        Me.TPXBMCLink.Size = New System.Drawing.Size(1000, 555)
        Me.TPXBMCLink.TabIndex = 9
        Me.TPXBMCLink.Text = "XBMC Link"
        Me.TPXBMCLink.UseVisualStyleBackColor = true
        '
        'UcGenPref_XbmcLink1
        '
        Me.UcGenPref_XbmcLink1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcGenPref_XbmcLink1.Location = New System.Drawing.Point(0, 0)
        Me.UcGenPref_XbmcLink1.Name = "UcGenPref_XbmcLink1"
        Me.UcGenPref_XbmcLink1.Size = New System.Drawing.Size(1000, 555)
        Me.UcGenPref_XbmcLink1.TabIndex = 0
        '
        'TPPRofCmd
        '
        Me.TPPRofCmd.Controls.Add(Me.TableLayoutPanel1)
        Me.TPPRofCmd.Location = New System.Drawing.Point(4, 22)
        Me.TPPRofCmd.Name = "TPPRofCmd"
        Me.TPPRofCmd.Size = New System.Drawing.Size(1000, 555)
        Me.TPPRofCmd.TabIndex = 6
        Me.TPPRofCmd.Text = "Profiles && Commands"
        Me.TPPRofCmd.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 328!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 275!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox42, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox15, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_CommandTitle, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lcl_CommandCommand, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_CommandTitle, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_CommandCommand, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_CommandRemove, 5, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.lb_CommandTitle, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lb_CommandCommand, 3, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_CommandAdd, 5, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 11
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1000, 555)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox42
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox42, 3)
        Me.GroupBox42.Controls.Add(Me.Label141)
        Me.GroupBox42.Location = New System.Drawing.Point(11, 11)
        Me.GroupBox42.Name = "GroupBox42"
        Me.GroupBox42.Size = New System.Drawing.Size(445, 113)
        Me.GroupBox42.TabIndex = 13
        Me.GroupBox42.TabStop = false
        '
        'Label141
        '
        Me.Label141.AutoSize = true
        Me.Label141.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label141.Location = New System.Drawing.Point(7, 17)
        Me.Label141.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(417, 90)
        Me.Label141.TabIndex = 0
        Me.Label141.Text = resources.GetString("Label141.Text")
        '
        'GroupBox15
        '
        Me.GroupBox15.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.GroupBox15.Controls.Add(Me.btn_ProfileSetStartup)
        Me.GroupBox15.Controls.Add(Me.Label3)
        Me.GroupBox15.Controls.Add(Me.lb_ProfileList)
        Me.GroupBox15.Controls.Add(Me.Label18)
        Me.GroupBox15.Controls.Add(Me.btn_ProfileSetDefault)
        Me.GroupBox15.Controls.Add(Me.tb_ProfileNew)
        Me.GroupBox15.Controls.Add(Me.Label17)
        Me.GroupBox15.Controls.Add(Me.btn_ProfileAdd)
        Me.GroupBox15.Controls.Add(Me.Label16)
        Me.GroupBox15.Controls.Add(Me.btn_ProfileRemove)
        Me.GroupBox15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox15.Location = New System.Drawing.Point(706, 11)
        Me.GroupBox15.Name = "GroupBox15"
        Me.TableLayoutPanel1.SetRowSpan(Me.GroupBox15, 10)
        Me.GroupBox15.Size = New System.Drawing.Size(268, 491)
        Me.GroupBox15.TabIndex = 12
        Me.GroupBox15.TabStop = false
        Me.GroupBox15.Text = "Profile Manager"
        '
        'btn_ProfileSetStartup
        '
        Me.btn_ProfileSetStartup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_ProfileSetStartup.Location = New System.Drawing.Point(9, 459)
        Me.btn_ProfileSetStartup.Name = "btn_ProfileSetStartup"
        Me.btn_ProfileSetStartup.Size = New System.Drawing.Size(247, 23)
        Me.btn_ProfileSetStartup.TabIndex = 11
        Me.btn_ProfileSetStartup.Text = "Set Startup"
        Me.btn_ProfileSetStartup.UseVisualStyleBackColor = true
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 441)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(191, 15)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Current Startup Profile Is :- Default"
        '
        'lb_ProfileList
        '
        Me.lb_ProfileList.FormattingEnabled = true
        Me.lb_ProfileList.Location = New System.Drawing.Point(9, 121)
        Me.lb_ProfileList.Name = "lb_ProfileList"
        Me.lb_ProfileList.Size = New System.Drawing.Size(247, 225)
        Me.lb_ProfileList.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 387)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(191, 15)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "Current Default Profile Is :- Default"
        '
        'btn_ProfileSetDefault
        '
        Me.btn_ProfileSetDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_ProfileSetDefault.Location = New System.Drawing.Point(9, 405)
        Me.btn_ProfileSetDefault.Name = "btn_ProfileSetDefault"
        Me.btn_ProfileSetDefault.Size = New System.Drawing.Size(247, 23)
        Me.btn_ProfileSetDefault.TabIndex = 8
        Me.btn_ProfileSetDefault.Text = "Set Default"
        Me.btn_ProfileSetDefault.UseVisualStyleBackColor = true
        '
        'tb_ProfileNew
        '
        Me.tb_ProfileNew.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ProfileNew.Location = New System.Drawing.Point(9, 67)
        Me.tb_ProfileNew.Name = "tb_ProfileNew"
        Me.tb_ProfileNew.Size = New System.Drawing.Size(247, 22)
        Me.tb_ProfileNew.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(250, 26)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "The Default Profile can't be deleted, it is used as a"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"template. You can set any "& _ 
    "created profile to default."
        '
        'btn_ProfileAdd
        '
        Me.btn_ProfileAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_ProfileAdd.Location = New System.Drawing.Point(9, 95)
        Me.btn_ProfileAdd.Name = "btn_ProfileAdd"
        Me.btn_ProfileAdd.Size = New System.Drawing.Size(247, 23)
        Me.btn_ProfileAdd.TabIndex = 4
        Me.btn_ProfileAdd.Text = "Add Profile"
        Me.btn_ProfileAdd.UseVisualStyleBackColor = true
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(150, 16)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "Enter New Profile Name"
        '
        'btn_ProfileRemove
        '
        Me.btn_ProfileRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_ProfileRemove.Location = New System.Drawing.Point(9, 352)
        Me.btn_ProfileRemove.Name = "btn_ProfileRemove"
        Me.btn_ProfileRemove.Size = New System.Drawing.Size(247, 23)
        Me.btn_ProfileRemove.TabIndex = 5
        Me.btn_ProfileRemove.Text = "Remove Selected Profile"
        Me.btn_ProfileRemove.UseVisualStyleBackColor = true
        '
        'lbl_CommandTitle
        '
        Me.lbl_CommandTitle.AutoSize = true
        Me.lbl_CommandTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_CommandTitle.Location = New System.Drawing.Point(13, 171)
        Me.lbl_CommandTitle.Margin = New System.Windows.Forms.Padding(5, 7, 3, 0)
        Me.lbl_CommandTitle.Name = "lbl_CommandTitle"
        Me.lbl_CommandTitle.Size = New System.Drawing.Size(40, 17)
        Me.lbl_CommandTitle.TabIndex = 14
        Me.lbl_CommandTitle.Text = "Title"
        '
        'lcl_CommandCommand
        '
        Me.lcl_CommandCommand.AutoSize = true
        Me.lcl_CommandCommand.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lcl_CommandCommand.Location = New System.Drawing.Point(177, 171)
        Me.lcl_CommandCommand.Margin = New System.Windows.Forms.Padding(5, 7, 3, 0)
        Me.lcl_CommandCommand.Name = "lcl_CommandCommand"
        Me.lcl_CommandCommand.Size = New System.Drawing.Size(78, 17)
        Me.lcl_CommandCommand.TabIndex = 15
        Me.lcl_CommandCommand.Text = "Command"
        '
        'tb_CommandTitle
        '
        Me.tb_CommandTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_CommandTitle.Location = New System.Drawing.Point(11, 203)
        Me.tb_CommandTitle.Name = "tb_CommandTitle"
        Me.tb_CommandTitle.Size = New System.Drawing.Size(150, 20)
        Me.tb_CommandTitle.TabIndex = 16
        '
        'tb_CommandCommand
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tb_CommandCommand, 2)
        Me.tb_CommandCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_CommandCommand.Location = New System.Drawing.Point(175, 203)
        Me.tb_CommandCommand.Name = "tb_CommandCommand"
        Me.tb_CommandCommand.Size = New System.Drawing.Size(335, 20)
        Me.tb_CommandCommand.TabIndex = 17
        '
        'btn_CommandRemove
        '
        Me.btn_CommandRemove.Location = New System.Drawing.Point(528, 422)
        Me.btn_CommandRemove.Margin = New System.Windows.Forms.Padding(15, 3, 3, 3)
        Me.btn_CommandRemove.Name = "btn_CommandRemove"
        Me.btn_CommandRemove.Size = New System.Drawing.Size(122, 29)
        Me.btn_CommandRemove.TabIndex = 19
        Me.btn_CommandRemove.Text = "Remove Selected"
        Me.btn_CommandRemove.UseVisualStyleBackColor = true
        '
        'lb_CommandTitle
        '
        Me.lb_CommandTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lb_CommandTitle.FormattingEnabled = true
        Me.lb_CommandTitle.Location = New System.Drawing.Point(11, 238)
        Me.lb_CommandTitle.Name = "lb_CommandTitle"
        Me.lb_CommandTitle.Size = New System.Drawing.Size(150, 170)
        Me.lb_CommandTitle.TabIndex = 20
        '
        'lb_CommandCommand
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.lb_CommandCommand, 3)
        Me.lb_CommandCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lb_CommandCommand.FormattingEnabled = true
        Me.lb_CommandCommand.Location = New System.Drawing.Point(175, 238)
        Me.lb_CommandCommand.Name = "lb_CommandCommand"
        Me.lb_CommandCommand.Size = New System.Drawing.Size(525, 170)
        Me.lb_CommandCommand.TabIndex = 21
        '
        'btn_CommandAdd
        '
        Me.btn_CommandAdd.Location = New System.Drawing.Point(528, 203)
        Me.btn_CommandAdd.Margin = New System.Windows.Forms.Padding(15, 3, 3, 3)
        Me.btn_CommandAdd.Name = "btn_CommandAdd"
        Me.btn_CommandAdd.Size = New System.Drawing.Size(75, 21)
        Me.btn_CommandAdd.TabIndex = 18
        Me.btn_CommandAdd.Text = "Add"
        Me.btn_CommandAdd.UseVisualStyleBackColor = true
        '
        'btn_SettingsCancel
        '
        Me.btn_SettingsCancel.Location = New System.Drawing.Point(140, 535)
        Me.btn_SettingsCancel.Name = "btn_SettingsCancel"
        Me.btn_SettingsCancel.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsCancel.TabIndex = 22
        Me.btn_SettingsCancel.Text = "Cancel"
        Me.btn_SettingsCancel.UseVisualStyleBackColor = true
        '
        'btn_SettingsClose
        '
        Me.btn_SettingsClose.Location = New System.Drawing.Point(260, 535)
        Me.btn_SettingsClose.Name = "btn_SettingsClose"
        Me.btn_SettingsClose.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsClose.TabIndex = 21
        Me.btn_SettingsClose.Text = "Close"
        Me.btn_SettingsClose.UseVisualStyleBackColor = true
        '
        'btn_SettingsApply
        '
        Me.btn_SettingsApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_SettingsApply.Location = New System.Drawing.Point(20, 535)
        Me.btn_SettingsApply.Name = "btn_SettingsApply"
        Me.btn_SettingsApply.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsApply.TabIndex = 20
        Me.btn_SettingsApply.Text = "Apply"
        Me.btn_SettingsApply.UseVisualStyleBackColor = true
        '
        'Label185
        '
        Me.Label185.AutoSize = true
        Me.Label185.Location = New System.Drawing.Point(50, 301)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(197, 13)
        Me.Label185.TabIndex = 104
        Me.Label185.Text = "AutoScreenShot delay (in Seconds only)"
        '
        'AutoScrnShtDelay
        '
        Me.AutoScrnShtDelay.Location = New System.Drawing.Point(7, 298)
        Me.AutoScrnShtDelay.Name = "AutoScrnShtDelay"
        Me.AutoScrnShtDelay.Size = New System.Drawing.Size(37, 20)
        Me.AutoScrnShtDelay.TabIndex = 103
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 581)
        Me.Controls.Add(Me.btn_SettingsCancel)
        Me.Controls.Add(Me.btn_SettingsClose)
        Me.Controls.Add(Me.btn_SettingsApply)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(1016, 608)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(1016, 608)
        Me.Name = "frmOptions"
        Me.Text = "Media Companion Preferences"
        Me.GroupBox36.ResumeLayout(false)
        Me.GroupBox36.PerformLayout
        Me.GroupBox12.ResumeLayout(false)
        Me.GroupBox12.PerformLayout
        Me.gbExcludeFolders.ResumeLayout(false)
        Me.gbExcludeFolders.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.TabControl1.ResumeLayout(false)
        Me.TPCommon.ResumeLayout(false)
        Me.TabControl4.ResumeLayout(false)
        Me.TPCommonSettings.ResumeLayout(false)
        Me.TPCommonSettings.PerformLayout
        Me.gbImageResizing.ResumeLayout(false)
        Me.gbImageResizing.PerformLayout
        Me.grpCleanFilename.ResumeLayout(false)
        Me.grpCleanFilename.PerformLayout
        Me.grpVideoSource.ResumeLayout(false)
        Me.grpVideoSource.PerformLayout
        Me.gbxXBMCversion.ResumeLayout(false)
        Me.gbxXBMCversion.PerformLayout
        Me.TPActors.ResumeLayout(false)
        Me.TPActors.PerformLayout
        Me.GroupBox32.ResumeLayout(false)
        Me.GroupBox32.PerformLayout
        Me.TPGen.ResumeLayout(false)
        Me.TPGen.PerformLayout
        Me.GroupBox45.ResumeLayout(false)
        Me.GroupBox45.PerformLayout
        Me.GroupBox33.ResumeLayout(false)
        Me.GroupBox33.PerformLayout
        Me.GroupBox31.ResumeLayout(false)
        Me.GroupBox31.PerformLayout
        Me.TPProxy.ResumeLayout(false)
        Me.TPXBMCLink.ResumeLayout(false)
        Me.TPPRofCmd.ResumeLayout(false)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.GroupBox42.ResumeLayout(false)
        Me.GroupBox42.PerformLayout
        Me.GroupBox15.ResumeLayout(false)
        Me.GroupBox15.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents FontDialog As System.Windows.Forms.FontDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_MediaPlayerBrowse As System.Windows.Forms.Button
    Friend WithEvents rb_MediaPlayerUser As System.Windows.Forms.RadioButton
    Friend WithEvents rb_MediaPlayerWMP As System.Windows.Forms.RadioButton
    Friend WithEvents rb_MediaPlayerDefault As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TPGen As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_MediaPlayerUser As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_ProfileSetStartup As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lb_ProfileList As System.Windows.Forms.ListBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btn_ProfileSetDefault As System.Windows.Forms.Button
    Friend WithEvents tb_ProfileNew As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btn_ProfileAdd As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btn_ProfileRemove As System.Windows.Forms.Button
    Friend WithEvents GroupBox42 As System.Windows.Forms.GroupBox
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents lbl_CommandTitle As System.Windows.Forms.Label
    Friend WithEvents lcl_CommandCommand As System.Windows.Forms.Label
    Friend WithEvents tb_CommandTitle As System.Windows.Forms.TextBox
    Friend WithEvents tb_CommandCommand As System.Windows.Forms.TextBox
    Friend WithEvents btn_CommandAdd As System.Windows.Forms.Button
    Friend WithEvents btn_CommandRemove As System.Windows.Forms.Button
    Friend WithEvents lb_CommandTitle As System.Windows.Forms.ListBox
    Friend WithEvents lb_CommandCommand As System.Windows.Forms.ListBox
    Friend WithEvents btn_SettingsCancel As System.Windows.Forms.Button
    Friend WithEvents btn_SettingsClose As System.Windows.Forms.Button
    Friend WithEvents btn_SettingsApply As System.Windows.Forms.Button
    Friend WithEvents TPCommon As TabPage
    Friend WithEvents TabControl4 As TabControl
    Friend WithEvents TPCommonSettings As TabPage
    Friend WithEvents TPActors As TabPage
    Friend WithEvents TPMovPref As TabPage
    Friend WithEvents TPTVPref As TabPage
    Friend WithEvents TPPRofCmd As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents cbMultiMonitorEnable As CheckBox
    Friend WithEvents cbDisplayLocalActor As CheckBox
    Friend WithEvents cbCheckForNewVersion As CheckBox
    Friend WithEvents cbRenameNFOtoINFO As CheckBox
    Friend WithEvents cbUseMultipleThreads As CheckBox
    Friend WithEvents cbShowLogOnError As CheckBox
    Friend WithEvents btnFindBrowser As Button
    Friend WithEvents cbExternalbrowser As CheckBox
    Friend WithEvents chkbx_disablecache As CheckBox
    Friend WithEvents GroupBox45 As GroupBox
    Friend WithEvents lblaltnfoeditorclear As Label
    Friend WithEvents btnaltnfoeditor As Button
    Friend WithEvents tbaltnfoeditor As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents GroupBox36 As GroupBox
    Friend WithEvents llMkvMergeGuiPath As LinkLabel
    Friend WithEvents btnMkvMergeGuiPath As Button
    Friend WithEvents tbMkvMergeGuiPath As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents GroupBox33 As GroupBox
    Friend WithEvents btnFontSelect As Button
    Friend WithEvents btnFontReset As Button
    Friend WithEvents lbl_FontSample As Label
    Friend WithEvents GroupBox31 As GroupBox
    Friend WithEvents Label116 As Label
    Friend WithEvents Label107 As Label
    Friend WithEvents txtbx_minrarsize As TextBox
    Friend WithEvents TPProxy As TabPage
    Friend WithEvents UcGenPref_Proxy1 As ucGenPref_Proxy
    Friend WithEvents TPXBMCLink As TabPage
    Friend WithEvents UcGenPref_XbmcLink1 As ucGenPref_XbmcLink
    Friend WithEvents GroupBox32 As GroupBox
    Friend WithEvents Label137 As Label
    Friend WithEvents cb_actorseasy As CheckBox
    Friend WithEvents GroupBox12 As GroupBox
    Friend WithEvents cb_LocalActorSaveAlpha As CheckBox
    Friend WithEvents xbmcactorpath As TextBox
    Friend WithEvents btn_localactorpathbrowse As Button
    Friend WithEvents Label161 As Label
    Friend WithEvents Label132 As Label
    Friend WithEvents Label104 As Label
    Friend WithEvents Label103 As Label
    Friend WithEvents Label101 As Label
    Friend WithEvents Label96 As Label
    Friend WithEvents Label97 As Label
    Friend WithEvents localactorpath As TextBox
    Friend WithEvents saveactorchkbx As CheckBox
    Friend WithEvents Label98 As Label
    Friend WithEvents ComboBox7 As ComboBox
    Friend WithEvents cbShowAllAudioTracks As CheckBox
    Friend WithEvents cbDisplayMediaInfoOverlay As CheckBox
    Friend WithEvents cbDisplayMediaInfoFolderSize As CheckBox
    Friend WithEvents cbDisplayRatingOverlay As CheckBox
    Friend WithEvents gbExcludeFolders As GroupBox
    Friend WithEvents tbExcludeFolders As TextBox
    Friend WithEvents cb_IgnoreAn As CheckBox
    Friend WithEvents cb_SorttitleIgnoreArticles As CheckBox
    Friend WithEvents cb_IgnoreA As CheckBox
    Friend WithEvents cbOverwriteArtwork As CheckBox
    Friend WithEvents cb_IgnoreThe As CheckBox
    Friend WithEvents CheckBox38 As CheckBox
    Friend WithEvents gbxXBMCversion As GroupBox
    Friend WithEvents Label129 As Label
    Friend WithEvents rbXBMCv_both As RadioButton
    Friend WithEvents rbXBMCv_post As RadioButton
    Friend WithEvents rbXBMCv_pre As RadioButton
    Friend WithEvents grpVideoSource As GroupBox
    Friend WithEvents btnVideoSourceRemove As Button
    Friend WithEvents txtVideoSourceAdd As TextBox
    Friend WithEvents btnVideoSourceAdd As Button
    Friend WithEvents lbVideoSource As ListBox
    Friend WithEvents grpCleanFilename As GroupBox
    Friend WithEvents btnCleanFilenameRemove As Button
    Friend WithEvents txtCleanFilenameAdd As TextBox
    Friend WithEvents btnCleanFilenameAdd As Button
    Friend WithEvents lbCleanFilename As ListBox
    Friend WithEvents gbImageResizing As GroupBox
    Friend WithEvents Label171 As Label
    Friend WithEvents Label175 As Label
    Friend WithEvents Label176 As Label
    Friend WithEvents comboActorResolutions As ComboBox
    Friend WithEvents comboBackDropResolutions As ComboBox
    Friend WithEvents comboPosterResolutions As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label185 As Label
    Friend WithEvents AutoScrnShtDelay As TextBox
End Class
