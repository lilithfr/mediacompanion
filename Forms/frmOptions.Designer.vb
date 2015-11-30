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
        Me.cmbx_MovMaxActors = New System.Windows.Forms.ComboBox()
        Me.cbShowAllAudioTracks = New System.Windows.Forms.CheckBox()
        Me.cbDisplayMediaInfoOverlay = New System.Windows.Forms.CheckBox()
        Me.cbDisplayRatingOverlay = New System.Windows.Forms.CheckBox()
        Me.gbExcludeFolders = New System.Windows.Forms.GroupBox()
        Me.tbExcludeFolders = New System.Windows.Forms.TextBox()
        Me.cbXbmcTmdbActorDL = New System.Windows.Forms.CheckBox()
        Me.cb_keywordlimit = New System.Windows.Forms.ComboBox()
        Me.cbMovieBasicSave = New System.Windows.Forms.CheckBox()
        Me.cbGetMovieSetFromTMDb = New System.Windows.Forms.CheckBox()
        Me.cmbxMovieScraper_MaxStudios = New System.Windows.Forms.ComboBox()
        Me.cmbxMovScraper_MaxGenres = New System.Windows.Forms.ComboBox()
        Me.cbMovCreateFanartjpg = New System.Windows.Forms.CheckBox()
        Me.cbMoviePosterInFolder = New System.Windows.Forms.CheckBox()
        Me.cbMovieFanartInFolders = New System.Windows.Forms.CheckBox()
        Me.cbMovXtraFanart = New System.Windows.Forms.CheckBox()
        Me.cbMovXtraThumbs = New System.Windows.Forms.CheckBox()
        Me.cbMovFanartTvScrape = New System.Windows.Forms.CheckBox()
        Me.cbDlXtraFanart = New System.Windows.Forms.CheckBox()
        Me.cbMovFanartScrape = New System.Windows.Forms.CheckBox()
        Me.cbMoviePosterScrape = New System.Windows.Forms.CheckBox()
        Me.tbDateFormat = New System.Windows.Forms.TextBox()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.rbRenameFullStop = New System.Windows.Forms.RadioButton()
        Me.rbRenameUnderscore = New System.Windows.Forms.RadioButton()
        Me.cbMovTitleCase = New System.Windows.Forms.CheckBox()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.cbPreferredTrailerResolution = New System.Windows.Forms.ComboBox()
        Me.cbDlTrailerDuringScrape = New System.Windows.Forms.CheckBox()
        Me.cbMovieRuntimeFallbackToFile = New System.Windows.Forms.CheckBox()
        Me.cbMovRootFolderCheck = New System.Windows.Forms.CheckBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.ScrapeFullCertCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Button74 = New System.Windows.Forms.Button()
        Me.Button75 = New System.Windows.Forms.Button()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.lb_IMDBCertPriority = New System.Windows.Forms.ListBox()
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
        Me.Label185 = New System.Windows.Forms.Label()
        Me.AutoScrnShtDelay = New System.Windows.Forms.TextBox()
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
        Me.tcMoviePreferences = New System.Windows.Forms.TabControl()
        Me.tpMoviePreferences_Scraper = New System.Windows.Forms.TabPage()
        Me.GroupBox25 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_Use_XBMC_Scraper = New System.Windows.Forms.CheckBox()
        Me.GroupBox_TMDB_Scraper_Preferences = New System.Windows.Forms.GroupBox()
        Me.cmbxTMDBPreferredCertCountry = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox46 = New System.Windows.Forms.GroupBox()
        Me.cbXbmcTmdbAkasFromImdb = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbCertFromImdb = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbVotesFromImdb = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbTop250FromImdb = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbIMDBRatings = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbStarsFromImdb = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbOutlineFromImdb = New System.Windows.Forms.CheckBox()
        Me.cbXbmcTmdbRename = New System.Windows.Forms.CheckBox()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.cmbxXbmcTmdbTitleLanguage = New System.Windows.Forms.ComboBox()
        Me.cbXbmcTmdbFanart = New System.Windows.Forms.CheckBox()
        Me.cmbxXbmcTmdbHDTrailer = New System.Windows.Forms.ComboBox()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.GroupBox_MovieIMDBMirror = New System.Windows.Forms.GroupBox()
        Me.cbImdbPrimaryPlot = New System.Windows.Forms.CheckBox()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.cbImdbgetTMDBActor = New System.Windows.Forms.CheckBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.lb_IMDBMirrors = New System.Windows.Forms.ListBox()
        Me.gpbxPrefScraperImages = New System.Windows.Forms.GroupBox()
        Me.GroupBox44 = New System.Windows.Forms.GroupBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.cb_keywordasTag = New System.Windows.Forms.CheckBox()
        Me.gbMovieBasicSave = New System.Windows.Forms.GroupBox()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.GroupBox30 = New System.Windows.Forms.GroupBox()
        Me.lblMaxStudios = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.gbScraperMisc = New System.Windows.Forms.GroupBox()
        Me.chkbOriginal_Title = New System.Windows.Forms.CheckBox()
        Me.GroupBox34 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.gbCustomLanguage = New System.Windows.Forms.GroupBox()
        Me.llLanguagesFile = New System.Windows.Forms.LinkLabel()
        Me.tbCustomLanguageValue = New System.Windows.Forms.TextBox()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.cbUseCustomLanguage = New System.Windows.Forms.CheckBox()
        Me.comboBoxTMDbSelectedLanguage = New System.Windows.Forms.ComboBox()
        Me.GroupBox24 = New System.Windows.Forms.GroupBox()
        Me.cbMovieAllInFolders = New System.Windows.Forms.CheckBox()
        Me.cbMovieUseFolderNames = New System.Windows.Forms.CheckBox()
        Me.Button82 = New System.Windows.Forms.Button()
        Me.tpMoviePreferences_Artwork = New System.Windows.Forms.TabPage()
        Me.GroupBox47 = New System.Windows.Forms.GroupBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.tbMovSetArtCentralFolder = New System.Windows.Forms.TextBox()
        Me.btnMovSetCentralFolderSelect = New System.Windows.Forms.Button()
        Me.rbMovSetArtSetFolder = New System.Windows.Forms.RadioButton()
        Me.rbMovSetFolder = New System.Windows.Forms.RadioButton()
        Me.GrpbxXtraArtwork = New System.Windows.Forms.GroupBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbxMovXtraFanartQty = New System.Windows.Forms.ComboBox()
        Me.GroupBox38 = New System.Windows.Forms.GroupBox()
        Me.cbMovCreateFolderjpg = New System.Windows.Forms.CheckBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.btn_MovPosterPriorityRemove = New System.Windows.Forms.Button()
        Me.btn_MovPosterPriorityReset = New System.Windows.Forms.Button()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Button61 = New System.Windows.Forms.Button()
        Me.Button73 = New System.Windows.Forms.Button()
        Me.lbPosterSourcePriorities = New System.Windows.Forms.ListBox()
        Me.GroupBox37 = New System.Windows.Forms.GroupBox()
        Me.cbMovFanartNaming = New System.Windows.Forms.CheckBox()
        Me.btnMovFanartTvSelect = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox41 = New System.Windows.Forms.GroupBox()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.cbMovSetArtScrape = New System.Windows.Forms.CheckBox()
        Me.tpMoviePreferences_General = New System.Windows.Forms.TabPage()
        Me.gbMovieFilters = New System.Windows.Forms.GroupBox()
        Me.cbMovieFilters_Sets_Order = New System.Windows.Forms.ComboBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.cbMovieFilters_Directors_Order = New System.Windows.Forms.ComboBox()
        Me.nudMaxDirectorsInFilter = New System.Windows.Forms.NumericUpDown()
        Me.nudMaxSetsInFilter = New System.Windows.Forms.NumericUpDown()
        Me.nudDirectorsFilterMinFilms = New System.Windows.Forms.NumericUpDown()
        Me.nudSetsFilterMinFilms = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbMovieFilters_Actors_Order = New System.Windows.Forms.ComboBox()
        Me.cbDisableNotMatchingRenamePattern = New System.Windows.Forms.CheckBox()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.nudMaxActorsInFilter = New System.Windows.Forms.NumericUpDown()
        Me.nudActorsFilterMinFilms = New System.Windows.Forms.NumericUpDown()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.cbMissingMovie = New System.Windows.Forms.CheckBox()
        Me.GroupBox35 = New System.Windows.Forms.GroupBox()
        Me.cbMovieList_ShowColWatched = New System.Windows.Forms.CheckBox()
        Me.cbMovieList_ShowColPlot = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ShowDateOnMovieList = New System.Windows.Forms.CheckBox()
        Me.GroupBox27 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbMovNewFolderInRootFolder = New System.Windows.Forms.CheckBox()
        Me.cbMovSortIgnArticle = New System.Windows.Forms.CheckBox()
        Me.cbMovTitleIgnArticle = New System.Windows.Forms.CheckBox()
        Me.cbMovSetIgnArticle = New System.Windows.Forms.CheckBox()
        Me.Label197 = New System.Windows.Forms.Label()
        Me.Label196 = New System.Windows.Forms.Label()
        Me.cbMovFolderRename = New System.Windows.Forms.CheckBox()
        Me.cbRenameUnderscore = New System.Windows.Forms.CheckBox()
        Me.lblFolderRename = New System.Windows.Forms.Label()
        Me.tb_MovFolderRename = New System.Windows.Forms.TextBox()
        Me.LblFilename = New System.Windows.Forms.Label()
        Me.ManualRenameChkbox = New System.Windows.Forms.CheckBox()
        Me.cbMovieRenameEnable = New System.Windows.Forms.CheckBox()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.tb_MovieRenameEnable = New System.Windows.Forms.TextBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.TextBox_OfflineDVDTitle = New System.Windows.Forms.TextBox()
        Me.GroupBox26 = New System.Windows.Forms.GroupBox()
        Me.cb_MovDurationAsRuntine = New System.Windows.Forms.CheckBox()
        Me.cbMovThousSeparator = New System.Windows.Forms.CheckBox()
        Me.cbExcludeMpaaRated = New System.Windows.Forms.CheckBox()
        Me.cbXtraFrodoUrls = New System.Windows.Forms.CheckBox()
        Me.cbNoAltTitle = New System.Windows.Forms.CheckBox()
        Me.PanelDisplayRuntime = New System.Windows.Forms.Panel()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.rbRuntimeFile = New System.Windows.Forms.RadioButton()
        Me.rbRuntimeScraper = New System.Windows.Forms.RadioButton()
        Me.CheckBox16 = New System.Windows.Forms.CheckBox()
        Me.CheckBox19 = New System.Windows.Forms.CheckBox()
        Me.cbMovieTrailerUrl = New System.Windows.Forms.CheckBox()
        Me.grpNameMode = New System.Windows.Forms.GroupBox()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.lblNameMode = New System.Windows.Forms.Label()
        Me.cbxCleanFilenameIgnorePart = New System.Windows.Forms.CheckBox()
        Me.lblNameModeEg = New System.Windows.Forms.Label()
        Me.cbxNameMode = New System.Windows.Forms.CheckBox()
        Me.tpMoviePreferences_Advanced = New System.Windows.Forms.TabPage()
        Me.gb_MovieIdentifier = New System.Windows.Forms.GroupBox()
        Me.btn_MovSepReset = New System.Windows.Forms.Button()
        Me.Label198 = New System.Windows.Forms.Label()
        Me.btn_MovSepRem = New System.Windows.Forms.Button()
        Me.btn_MovSepAdd = New System.Windows.Forms.Button()
        Me.tb_MovSeptb = New System.Windows.Forms.TextBox()
        Me.lb_MovSepLst = New System.Windows.Forms.ListBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.chkbx_unstackfanartnames = New System.Windows.Forms.CheckBox()
        Me.chkbx_unstackposternames = New System.Windows.Forms.CheckBox()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.imdb_chk = New System.Windows.Forms.CheckBox()
        Me.mpdb_chk = New System.Windows.Forms.CheckBox()
        Me.tmdb_chk = New System.Windows.Forms.CheckBox()
        Me.IMPA_chk = New System.Windows.Forms.CheckBox()
        Me.Label89 = New System.Windows.Forms.Label()
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
        Me.btn_SettingsClose2 = New System.Windows.Forms.Button()
        Me.GroupBox36.SuspendLayout
        Me.GroupBox12.SuspendLayout
        Me.gbExcludeFolders.SuspendLayout
        Me.GroupBox11.SuspendLayout
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
        Me.TPMovPref.SuspendLayout
        Me.tcMoviePreferences.SuspendLayout
        Me.tpMoviePreferences_Scraper.SuspendLayout
        Me.GroupBox25.SuspendLayout
        Me.GroupBox_TMDB_Scraper_Preferences.SuspendLayout
        Me.GroupBox46.SuspendLayout
        Me.GroupBox_MovieIMDBMirror.SuspendLayout
        Me.gpbxPrefScraperImages.SuspendLayout
        Me.GroupBox44.SuspendLayout
        Me.gbMovieBasicSave.SuspendLayout
        Me.GroupBox30.SuspendLayout
        Me.gbScraperMisc.SuspendLayout
        Me.GroupBox34.SuspendLayout
        Me.gbCustomLanguage.SuspendLayout
        Me.GroupBox24.SuspendLayout
        Me.tpMoviePreferences_Artwork.SuspendLayout
        Me.GroupBox47.SuspendLayout
        Me.GrpbxXtraArtwork.SuspendLayout
        Me.GroupBox38.SuspendLayout
        Me.GroupBox10.SuspendLayout
        Me.GroupBox37.SuspendLayout
        Me.GroupBox41.SuspendLayout
        Me.tpMoviePreferences_General.SuspendLayout
        Me.gbMovieFilters.SuspendLayout
        CType(Me.nudMaxDirectorsInFilter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudMaxSetsInFilter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudDirectorsFilterMinFilms,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudSetsFilterMinFilms,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudMaxActorsInFilter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudActorsFilterMinFilms,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox35.SuspendLayout
        Me.GroupBox27.SuspendLayout
        Me.GroupBox9.SuspendLayout
        Me.GroupBox26.SuspendLayout
        Me.PanelDisplayRuntime.SuspendLayout
        Me.grpNameMode.SuspendLayout
        Me.tpMoviePreferences_Advanced.SuspendLayout
        Me.gb_MovieIdentifier.SuspendLayout
        Me.GroupBox8.SuspendLayout
        Me.GroupBox14.SuspendLayout
        Me.GroupBox16.SuspendLayout
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
        'cmbx_MovMaxActors
        '
        Me.cmbx_MovMaxActors.FormattingEnabled = true
        Me.cmbx_MovMaxActors.Items.AddRange(New Object() {"All Available", "None", "5", "10", "15", "20", "25", "30", "40", "50", "70", "90", "110", "125", "150", "175", "200", "250"})
        Me.cmbx_MovMaxActors.Location = New System.Drawing.Point(182, 134)
        Me.cmbx_MovMaxActors.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbx_MovMaxActors.MaxDropDownItems = 30
        Me.cmbx_MovMaxActors.Name = "cmbx_MovMaxActors"
        Me.cmbx_MovMaxActors.Size = New System.Drawing.Size(137, 21)
        Me.cmbx_MovMaxActors.TabIndex = 64
        Me.ToolTip1.SetToolTip(Me.cmbx_MovMaxActors, "Media Companion will not scrape more than the number of actors set using this con"& _ 
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
        'cbXbmcTmdbActorDL
        '
        Me.cbXbmcTmdbActorDL.AutoSize = true
        Me.cbXbmcTmdbActorDL.Location = New System.Drawing.Point(8, 272)
        Me.cbXbmcTmdbActorDL.Name = "cbXbmcTmdbActorDL"
        Me.cbXbmcTmdbActorDL.Size = New System.Drawing.Size(196, 17)
        Me.cbXbmcTmdbActorDL.TabIndex = 75
        Me.cbXbmcTmdbActorDL.Text = "Download Actor Images from TMDB"
        Me.ToolTip1.SetToolTip(Me.cbXbmcTmdbActorDL, "Selection will use actor limit of IMDB Scraper, "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"but actor's Images will be down"& _ 
        "loaded from"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"TheMovieDB.")
        Me.cbXbmcTmdbActorDL.UseVisualStyleBackColor = true
        '
        'cb_keywordlimit
        '
        Me.cb_keywordlimit.FormattingEnabled = true
        Me.cb_keywordlimit.Items.AddRange(New Object() {"All Available", "None", "5", "10", "15", "20", "25", "30", "40", "50", "70", "90", "100", "125", "150", "175", "200", "250", "300", "400", "500"})
        Me.cb_keywordlimit.Location = New System.Drawing.Point(200, 45)
        Me.cb_keywordlimit.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_keywordlimit.MaxDropDownItems = 30
        Me.cb_keywordlimit.Name = "cb_keywordlimit"
        Me.cb_keywordlimit.Size = New System.Drawing.Size(76, 21)
        Me.cb_keywordlimit.TabIndex = 65
        Me.ToolTip1.SetToolTip(Me.cb_keywordlimit, "Media Companion will not scrape more than the number of Keywords set using this c"& _ 
        "ontrol")
        '
        'cbMovieBasicSave
        '
        Me.cbMovieBasicSave.AutoSize = true
        Me.cbMovieBasicSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieBasicSave.Location = New System.Drawing.Point(7, 80)
        Me.cbMovieBasicSave.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovieBasicSave.Name = "cbMovieBasicSave"
        Me.cbMovieBasicSave.Size = New System.Drawing.Size(301, 19)
        Me.cbMovieBasicSave.TabIndex = 46
        Me.cbMovieBasicSave.Text = "Save files as ""movie.nfo"", ""movie.tbn"", && ""fanart.jpg"""
        Me.ToolTip1.SetToolTip(Me.cbMovieBasicSave, resources.GetString("cbMovieBasicSave.ToolTip"))
        Me.cbMovieBasicSave.UseVisualStyleBackColor = true
        '
        'cbGetMovieSetFromTMDb
        '
        Me.cbGetMovieSetFromTMDb.AutoSize = true
        Me.cbGetMovieSetFromTMDb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbGetMovieSetFromTMDb.Location = New System.Drawing.Point(12, 18)
        Me.cbGetMovieSetFromTMDb.Name = "cbGetMovieSetFromTMDb"
        Me.cbGetMovieSetFromTMDb.Size = New System.Drawing.Size(252, 19)
        Me.cbGetMovieSetFromTMDb.TabIndex = 50
        Me.cbGetMovieSetFromTMDb.Text = "TMDb - Where available scrape set name"
        Me.ToolTip1.SetToolTip(Me.cbGetMovieSetFromTMDb, "Get Set names from TheMovieDb during scraping")
        Me.cbGetMovieSetFromTMDb.UseVisualStyleBackColor = true
        '
        'cmbxMovieScraper_MaxStudios
        '
        Me.cmbxMovieScraper_MaxStudios.FormattingEnabled = true
        Me.cmbxMovieScraper_MaxStudios.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbxMovieScraper_MaxStudios.Location = New System.Drawing.Point(173, 46)
        Me.cmbxMovieScraper_MaxStudios.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbxMovieScraper_MaxStudios.MaxDropDownItems = 30
        Me.cmbxMovieScraper_MaxStudios.Name = "cmbxMovieScraper_MaxStudios"
        Me.cmbxMovieScraper_MaxStudios.Size = New System.Drawing.Size(140, 21)
        Me.cmbxMovieScraper_MaxStudios.TabIndex = 64
        Me.ToolTip1.SetToolTip(Me.cmbxMovieScraper_MaxStudios, "Media Companion will not scrape more than"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"tne number of genres set with this con"& _ 
        "trol")
        '
        'cmbxMovScraper_MaxGenres
        '
        Me.cmbxMovScraper_MaxGenres.FormattingEnabled = true
        Me.cmbxMovScraper_MaxGenres.Items.AddRange(New Object() {"All Available", "None", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbxMovScraper_MaxGenres.Location = New System.Drawing.Point(173, 17)
        Me.cmbxMovScraper_MaxGenres.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbxMovScraper_MaxGenres.MaxDropDownItems = 30
        Me.cmbxMovScraper_MaxGenres.Name = "cmbxMovScraper_MaxGenres"
        Me.cmbxMovScraper_MaxGenres.Size = New System.Drawing.Size(140, 21)
        Me.cmbxMovScraper_MaxGenres.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.cmbxMovScraper_MaxGenres, "Media Companion will not scrape more than"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"tne number of genres set with this con"& _ 
        "trol")
        '
        'cbMovCreateFanartjpg
        '
        Me.cbMovCreateFanartjpg.AutoSize = true
        Me.cbMovCreateFanartjpg.Location = New System.Drawing.Point(6, 94)
        Me.cbMovCreateFanartjpg.Name = "cbMovCreateFanartjpg"
        Me.cbMovCreateFanartjpg.Size = New System.Drawing.Size(175, 17)
        Me.cbMovCreateFanartjpg.TabIndex = 52
        Me.cbMovCreateFanartjpg.Text = "Create fanart.jpg for each folder"
        Me.ToolTip1.SetToolTip(Me.cbMovCreateFanartjpg, "If 'Save as 'fanart.jpg', not 'moviename-fanart.jpg' ' is selected,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"then this op"& _ 
        "tion is ignored.")
        Me.cbMovCreateFanartjpg.UseVisualStyleBackColor = true
        '
        'cbMoviePosterInFolder
        '
        Me.cbMoviePosterInFolder.AutoSize = true
        Me.cbMoviePosterInFolder.Location = New System.Drawing.Point(6, 43)
        Me.cbMoviePosterInFolder.Name = "cbMoviePosterInFolder"
        Me.cbMoviePosterInFolder.Size = New System.Drawing.Size(245, 17)
        Me.cbMoviePosterInFolder.TabIndex = 44
        Me.cbMoviePosterInFolder.Text = "Save as poster.jpg', not 'moviename-poster.jpg"
        Me.ToolTip1.SetToolTip(Me.cbMoviePosterInFolder, resources.GetString("cbMoviePosterInFolder.ToolTip"))
        Me.cbMoviePosterInFolder.UseVisualStyleBackColor = true
        '
        'cbMovieFanartInFolders
        '
        Me.cbMovieFanartInFolders.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbMovieFanartInFolders.Location = New System.Drawing.Point(6, 17)
        Me.cbMovieFanartInFolders.Name = "cbMovieFanartInFolders"
        Me.cbMovieFanartInFolders.Size = New System.Drawing.Size(281, 26)
        Me.cbMovieFanartInFolders.TabIndex = 39
        Me.cbMovieFanartInFolders.Text = "Save as 'fanart.jpg', not 'moviename-fanart.jpg'"
        Me.cbMovieFanartInFolders.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.cbMovieFanartInFolders, resources.GetString("cbMovieFanartInFolders.ToolTip"))
        Me.cbMovieFanartInFolders.UseVisualStyleBackColor = true
        '
        'cbMovXtraFanart
        '
        Me.cbMovXtraFanart.AutoSize = true
        Me.cbMovXtraFanart.Location = New System.Drawing.Point(11, 48)
        Me.cbMovXtraFanart.Name = "cbMovXtraFanart"
        Me.cbMovXtraFanart.Size = New System.Drawing.Size(134, 17)
        Me.cbMovXtraFanart.TabIndex = 1
        Me.cbMovXtraFanart.Text = "Allow save ExtraFanart"
        Me.ToolTip1.SetToolTip(Me.cbMovXtraFanart, "Either or both options can be selected")
        Me.cbMovXtraFanart.UseVisualStyleBackColor = true
        '
        'cbMovXtraThumbs
        '
        Me.cbMovXtraThumbs.AutoSize = true
        Me.cbMovXtraThumbs.Location = New System.Drawing.Point(11, 23)
        Me.cbMovXtraThumbs.Name = "cbMovXtraThumbs"
        Me.cbMovXtraThumbs.Size = New System.Drawing.Size(256, 17)
        Me.cbMovXtraThumbs.TabIndex = 0
        Me.cbMovXtraThumbs.Text = "Allow save  ExtraThumbs......Limited to 5 images."
        Me.ToolTip1.SetToolTip(Me.cbMovXtraThumbs, "Either or both options can be selected.")
        Me.cbMovXtraThumbs.UseVisualStyleBackColor = true
        '
        'cbMovFanartTvScrape
        '
        Me.cbMovFanartTvScrape.AutoSize = true
        Me.cbMovFanartTvScrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovFanartTvScrape.Location = New System.Drawing.Point(5, 56)
        Me.cbMovFanartTvScrape.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovFanartTvScrape.Name = "cbMovFanartTvScrape"
        Me.cbMovFanartTvScrape.Size = New System.Drawing.Size(284, 19)
        Me.cbMovFanartTvScrape.TabIndex = 45
        Me.cbMovFanartTvScrape.Text = "Download from Fanart.TV for during autoscrape"
        Me.ToolTip1.SetToolTip(Me.cbMovFanartTvScrape, "Allow to Auto-download From Fanart.Tv, during scraping of movies."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Does not inclu"& _ 
        "de ExtraThumbs or ExtraFanart.")
        Me.cbMovFanartTvScrape.UseVisualStyleBackColor = true
        '
        'cbDlXtraFanart
        '
        Me.cbDlXtraFanart.AutoSize = true
        Me.cbDlXtraFanart.Location = New System.Drawing.Point(6, 15)
        Me.cbDlXtraFanart.Name = "cbDlXtraFanart"
        Me.cbDlXtraFanart.Size = New System.Drawing.Size(177, 17)
        Me.cbDlXtraFanart.TabIndex = 43
        Me.cbDlXtraFanart.Text = "Download Extra Fanart/Thumbs"
        Me.ToolTip1.SetToolTip(Me.cbDlXtraFanart, "This allows download of Extra Thumbs or"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Extra Fanart or both, as selected below."& _ 
        ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"NB:  Movies must be in separate folders!!!")
        Me.cbDlXtraFanart.UseVisualStyleBackColor = true
        '
        'cbMovFanartScrape
        '
        Me.cbMovFanartScrape.AutoSize = true
        Me.cbMovFanartScrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovFanartScrape.Location = New System.Drawing.Point(5, 36)
        Me.cbMovFanartScrape.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovFanartScrape.Name = "cbMovFanartScrape"
        Me.cbMovFanartScrape.Size = New System.Drawing.Size(281, 19)
        Me.cbMovFanartScrape.TabIndex = 38
        Me.cbMovFanartScrape.Text = "Download Fanart for Movies during autoscrape"
        Me.ToolTip1.SetToolTip(Me.cbMovFanartScrape, "Allow to Auto-download Fanart during scraping of movies."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Does not include ExtraT"& _ 
        "humbs or ExtraFanart.")
        Me.cbMovFanartScrape.UseVisualStyleBackColor = true
        '
        'cbMoviePosterScrape
        '
        Me.cbMoviePosterScrape.AutoSize = true
        Me.cbMoviePosterScrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMoviePosterScrape.Location = New System.Drawing.Point(5, 15)
        Me.cbMoviePosterScrape.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMoviePosterScrape.Name = "cbMoviePosterScrape"
        Me.cbMoviePosterScrape.Size = New System.Drawing.Size(281, 19)
        Me.cbMoviePosterScrape.TabIndex = 42
        Me.cbMoviePosterScrape.Text = "Download Poster for Movies during autoscrape"
        Me.ToolTip1.SetToolTip(Me.cbMoviePosterScrape, "Allow to Auto-download Posters during scraping of movies")
        Me.cbMoviePosterScrape.UseVisualStyleBackColor = true
        '
        'tbDateFormat
        '
        Me.tbDateFormat.Location = New System.Drawing.Point(90, 18)
        Me.tbDateFormat.Name = "tbDateFormat"
        Me.tbDateFormat.Size = New System.Drawing.Size(138, 20)
        Me.tbDateFormat.TabIndex = 1
        Me.tbDateFormat.Text = "YYYY-MM-DD"
        Me.ToolTip1.SetToolTip(Me.tbDateFormat, "Valid tokens: YYYY MM DD HH MIN SS")
        '
        'Label179
        '
        Me.Label179.AutoSize = true
        Me.Label179.Location = New System.Drawing.Point(12, 21)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(62, 13)
        Me.Label179.TabIndex = 0
        Me.Label179.Text = "Date format"
        Me.ToolTip1.SetToolTip(Me.Label179, "Valid tokens: YYYY MM DD HH MIN SS")
        '
        'rbRenameFullStop
        '
        Me.rbRenameFullStop.AutoSize = true
        Me.rbRenameFullStop.CheckAlign = System.Drawing.ContentAlignment.TopCenter
        Me.rbRenameFullStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbRenameFullStop.Location = New System.Drawing.Point(214, 257)
        Me.rbRenameFullStop.Name = "rbRenameFullStop"
        Me.rbRenameFullStop.Size = New System.Drawing.Size(25, 32)
        Me.rbRenameFullStop.TabIndex = 85
        Me.rbRenameFullStop.TabStop = true
        Me.rbRenameFullStop.Text = """."""
        Me.rbRenameFullStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.rbRenameFullStop, "Full Stop")
        Me.rbRenameFullStop.UseVisualStyleBackColor = true
        '
        'rbRenameUnderscore
        '
        Me.rbRenameUnderscore.AutoSize = true
        Me.rbRenameUnderscore.CheckAlign = System.Drawing.ContentAlignment.TopCenter
        Me.rbRenameUnderscore.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbRenameUnderscore.Location = New System.Drawing.Point(154, 257)
        Me.rbRenameUnderscore.Name = "rbRenameUnderscore"
        Me.rbRenameUnderscore.Size = New System.Drawing.Size(29, 32)
        Me.rbRenameUnderscore.TabIndex = 84
        Me.rbRenameUnderscore.TabStop = true
        Me.rbRenameUnderscore.Text = """_"""
        Me.rbRenameUnderscore.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.rbRenameUnderscore, "UnderScore")
        Me.rbRenameUnderscore.UseVisualStyleBackColor = true
        '
        'cbMovTitleCase
        '
        Me.cbMovTitleCase.AutoSize = true
        Me.cbMovTitleCase.Location = New System.Drawing.Point(7, 94)
        Me.cbMovTitleCase.Name = "cbMovTitleCase"
        Me.cbMovTitleCase.Size = New System.Drawing.Size(162, 17)
        Me.cbMovTitleCase.TabIndex = 75
        Me.cbMovTitleCase.Text = "Title Case Title and Sort Title"
        Me.ToolTip1.SetToolTip(Me.cbMovTitleCase, "Changes Movie Title and Sort Title to Title Case Format."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Example: "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"The Lion, Th"& _ 
        "e Witch And The Wardrobe"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"instead of"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"The Lion, the Witch and the Wardrobe")
        Me.cbMovTitleCase.UseVisualStyleBackColor = true
        '
        'Label102
        '
        Me.Label102.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label102.Location = New System.Drawing.Point(41, 67)
        Me.Label102.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(261, 31)
        Me.Label102.TabIndex = 72
        Me.Label102.Text = "If selected resolution is unavailable then next resolution down is used."
        Me.ToolTip1.SetToolTip(Me.Label102, "* This option is dependent on 'Add Movie Trailer url to nfo file' being checked*")
        '
        'Label78
        '
        Me.Label78.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label78.Location = New System.Drawing.Point(23, 52)
        Me.Label78.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(170, 15)
        Me.Label78.TabIndex = 71
        Me.Label78.Text = "Preferred movie trailer resolution"
        Me.ToolTip1.SetToolTip(Me.Label78, "* This option is dependent on 'Add Movie Trailer url to nfo file' being checked*")
        '
        'cbPreferredTrailerResolution
        '
        Me.cbPreferredTrailerResolution.FormattingEnabled = true
        Me.cbPreferredTrailerResolution.Items.AddRange(New Object() {"SD", "480", "720", "1080"})
        Me.cbPreferredTrailerResolution.Location = New System.Drawing.Point(228, 34)
        Me.cbPreferredTrailerResolution.Name = "cbPreferredTrailerResolution"
        Me.cbPreferredTrailerResolution.Size = New System.Drawing.Size(74, 21)
        Me.cbPreferredTrailerResolution.TabIndex = 70
        Me.ToolTip1.SetToolTip(Me.cbPreferredTrailerResolution, "* This option is dependent on 'Add Movie Trailer url to nfo file' being checked*")
        '
        'cbDlTrailerDuringScrape
        '
        Me.cbDlTrailerDuringScrape.AutoSize = true
        Me.cbDlTrailerDuringScrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbDlTrailerDuringScrape.Location = New System.Drawing.Point(7, 34)
        Me.cbDlTrailerDuringScrape.Margin = New System.Windows.Forms.Padding(4)
        Me.cbDlTrailerDuringScrape.Name = "cbDlTrailerDuringScrape"
        Me.cbDlTrailerDuringScrape.Size = New System.Drawing.Size(194, 19)
        Me.cbDlTrailerDuringScrape.TabIndex = 69
        Me.cbDlTrailerDuringScrape.Text = "Download trailer during scrape"
        Me.ToolTip1.SetToolTip(Me.cbDlTrailerDuringScrape, "NB Existing trailers are not redownloaded")
        Me.cbDlTrailerDuringScrape.UseVisualStyleBackColor = true
        '
        'cbMovieRuntimeFallbackToFile
        '
        Me.cbMovieRuntimeFallbackToFile.AutoSize = true
        Me.cbMovieRuntimeFallbackToFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieRuntimeFallbackToFile.Location = New System.Drawing.Point(6, 24)
        Me.cbMovieRuntimeFallbackToFile.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovieRuntimeFallbackToFile.Name = "cbMovieRuntimeFallbackToFile"
        Me.cbMovieRuntimeFallbackToFile.Size = New System.Drawing.Size(224, 19)
        Me.cbMovieRuntimeFallbackToFile.TabIndex = 70
        Me.cbMovieRuntimeFallbackToFile.Text = "If runtime not scraped, fallback to file"
        Me.ToolTip1.SetToolTip(Me.cbMovieRuntimeFallbackToFile, "Check this box to display runtime from movie file when it's not available on IMDB"& _ 
        ". Only applicable if 'Scraper' selected above")
        Me.cbMovieRuntimeFallbackToFile.UseVisualStyleBackColor = true
        '
        'cbMovRootFolderCheck
        '
        Me.cbMovRootFolderCheck.AutoSize = true
        Me.cbMovRootFolderCheck.Location = New System.Drawing.Point(6, 23)
        Me.cbMovRootFolderCheck.Name = "cbMovRootFolderCheck"
        Me.cbMovRootFolderCheck.Size = New System.Drawing.Size(142, 17)
        Me.cbMovRootFolderCheck.TabIndex = 0
        Me.cbMovRootFolderCheck.Text = "Enable root folder check"
        Me.ToolTip1.SetToolTip(Me.cbMovRootFolderCheck, "If enabled, checks if movie is in root folder, disabling option of extra artwork,"& _ 
        " specifically for ""Use FolderName for Scraping""")
        Me.cbMovRootFolderCheck.UseVisualStyleBackColor = true
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.ScrapeFullCertCheckBox)
        Me.GroupBox11.Controls.Add(Me.Label178)
        Me.GroupBox11.Controls.Add(Me.Label95)
        Me.GroupBox11.Controls.Add(Me.Button74)
        Me.GroupBox11.Controls.Add(Me.Button75)
        Me.GroupBox11.Controls.Add(Me.Label94)
        Me.GroupBox11.Controls.Add(Me.lb_IMDBCertPriority)
        Me.GroupBox11.Location = New System.Drawing.Point(343, 107)
        Me.GroupBox11.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox11.Size = New System.Drawing.Size(300, 300)
        Me.GroupBox11.TabIndex = 85
        Me.GroupBox11.TabStop = false
        Me.GroupBox11.Text = "Select IMDB Certification Priorities"
        Me.ToolTip1.SetToolTip(Me.GroupBox11, "You can select a specific locations certification to replace the MPAA entry on IM"& _ 
        "DB if you wish,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"The Rating will be scraped by the first available from the abov"& _ 
        "e list.")
        '
        'ScrapeFullCertCheckBox
        '
        Me.ScrapeFullCertCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ScrapeFullCertCheckBox.Location = New System.Drawing.Point(11, 265)
        Me.ScrapeFullCertCheckBox.Name = "ScrapeFullCertCheckBox"
        Me.ScrapeFullCertCheckBox.Size = New System.Drawing.Size(265, 24)
        Me.ScrapeFullCertCheckBox.TabIndex = 8
        Me.ScrapeFullCertCheckBox.Text = "Scrape Full Cert i.e. UK:15 instead of just 15"
        Me.ScrapeFullCertCheckBox.UseVisualStyleBackColor = true
        '
        'Label178
        '
        Me.Label178.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label178.Location = New System.Drawing.Point(9, 55)
        Me.Label178.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(282, 19)
        Me.Label178.TabIndex = 7
        Me.Label178.Text = "The first available certificate in the order you select below"
        '
        'Label95
        '
        Me.Label95.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label95.Location = New System.Drawing.Point(9, 19)
        Me.Label95.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(282, 31)
        Me.Label95.TabIndex = 7
        Me.Label95.Text = "Customise your preference with regards to the"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"MPAA movie rating or Cert."
        '
        'Button74
        '
        Me.Button74.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button74.Location = New System.Drawing.Point(242, 214)
        Me.Button74.Margin = New System.Windows.Forms.Padding(4)
        Me.Button74.Name = "Button74"
        Me.Button74.Size = New System.Drawing.Size(34, 29)
        Me.Button74.TabIndex = 6
        Me.Button74.Text = "↓"
        Me.Button74.UseVisualStyleBackColor = true
        '
        'Button75
        '
        Me.Button75.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button75.Location = New System.Drawing.Point(242, 136)
        Me.Button75.Margin = New System.Windows.Forms.Padding(4)
        Me.Button75.Name = "Button75"
        Me.Button75.Size = New System.Drawing.Size(34, 29)
        Me.Button75.TabIndex = 5
        Me.Button75.Text = "↑"
        Me.Button75.UseVisualStyleBackColor = true
        '
        'Label94
        '
        Me.Label94.AutoSize = true
        Me.Label94.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label94.Location = New System.Drawing.Point(223, 171)
        Me.Label94.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(68, 36)
        Me.Label94.TabIndex = 4
        Me.Label94.Text = "Change Priority"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"of Selected"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Certification"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label94.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lb_IMDBCertPriority
        '
        Me.lb_IMDBCertPriority.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lb_IMDBCertPriority.FormattingEnabled = true
        Me.lb_IMDBCertPriority.ItemHeight = 15
        Me.lb_IMDBCertPriority.Location = New System.Drawing.Point(12, 74)
        Me.lb_IMDBCertPriority.Margin = New System.Windows.Forms.Padding(4)
        Me.lb_IMDBCertPriority.Name = "lb_IMDBCertPriority"
        Me.lb_IMDBCertPriority.Size = New System.Drawing.Size(209, 184)
        Me.lb_IMDBCertPriority.TabIndex = 0
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
        Me.TabControl1.Size = New System.Drawing.Size(1008, 623)
        Me.TabControl1.TabIndex = 15
        '
        'TPCommon
        '
        Me.TPCommon.Controls.Add(Me.TabControl4)
        Me.TPCommon.Location = New System.Drawing.Point(4, 22)
        Me.TPCommon.Name = "TPCommon"
        Me.TPCommon.Size = New System.Drawing.Size(1000, 597)
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
        Me.TabControl4.Size = New System.Drawing.Size(1000, 597)
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
        Me.TPCommonSettings.Size = New System.Drawing.Size(992, 571)
        Me.TPCommonSettings.TabIndex = 0
        Me.TPCommonSettings.Text = "Common Settings"
        Me.TPCommonSettings.UseVisualStyleBackColor = true
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
        Me.grpCleanFilename.Location = New System.Drawing.Point(542, 146)
        Me.grpCleanFilename.Name = "grpCleanFilename"
        Me.grpCleanFilename.Size = New System.Drawing.Size(241, 349)
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
        Me.grpVideoSource.Size = New System.Drawing.Size(197, 491)
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
        Me.gbxXBMCversion.Location = New System.Drawing.Point(259, 146)
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
        Me.TPActors.Controls.Add(Me.cmbx_MovMaxActors)
        Me.TPActors.Controls.Add(Me.GroupBox12)
        Me.TPActors.Controls.Add(Me.GroupBox32)
        Me.TPActors.Location = New System.Drawing.Point(4, 22)
        Me.TPActors.Name = "TPActors"
        Me.TPActors.Padding = New System.Windows.Forms.Padding(3)
        Me.TPActors.Size = New System.Drawing.Size(992, 571)
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
        Me.TPGen.Size = New System.Drawing.Size(1000, 597)
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
        Me.TPMovPref.Controls.Add(Me.tcMoviePreferences)
        Me.TPMovPref.Location = New System.Drawing.Point(4, 22)
        Me.TPMovPref.Name = "TPMovPref"
        Me.TPMovPref.Size = New System.Drawing.Size(1000, 597)
        Me.TPMovPref.TabIndex = 7
        Me.TPMovPref.Text = "Movie Preferences"
        Me.TPMovPref.UseVisualStyleBackColor = true
        '
        'tcMoviePreferences
        '
        Me.tcMoviePreferences.Controls.Add(Me.tpMoviePreferences_Scraper)
        Me.tcMoviePreferences.Controls.Add(Me.tpMoviePreferences_Artwork)
        Me.tcMoviePreferences.Controls.Add(Me.tpMoviePreferences_General)
        Me.tcMoviePreferences.Controls.Add(Me.tpMoviePreferences_Advanced)
        Me.tcMoviePreferences.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMoviePreferences.Location = New System.Drawing.Point(0, 0)
        Me.tcMoviePreferences.Margin = New System.Windows.Forms.Padding(4)
        Me.tcMoviePreferences.Name = "tcMoviePreferences"
        Me.tcMoviePreferences.SelectedIndex = 0
        Me.tcMoviePreferences.Size = New System.Drawing.Size(1000, 597)
        Me.tcMoviePreferences.TabIndex = 51
        '
        'tpMoviePreferences_Scraper
        '
        Me.tpMoviePreferences_Scraper.AutoScroll = true
        Me.tpMoviePreferences_Scraper.AutoScrollMinSize = New System.Drawing.Size(928, 370)
        Me.tpMoviePreferences_Scraper.BackColor = System.Drawing.SystemColors.Control
        Me.tpMoviePreferences_Scraper.Controls.Add(Me.GroupBox25)
        Me.tpMoviePreferences_Scraper.Controls.Add(Me.gpbxPrefScraperImages)
        Me.tpMoviePreferences_Scraper.Controls.Add(Me.GroupBox24)
        Me.tpMoviePreferences_Scraper.Controls.Add(Me.Button82)
        Me.tpMoviePreferences_Scraper.Location = New System.Drawing.Point(4, 22)
        Me.tpMoviePreferences_Scraper.Margin = New System.Windows.Forms.Padding(4)
        Me.tpMoviePreferences_Scraper.Name = "tpMoviePreferences_Scraper"
        Me.tpMoviePreferences_Scraper.Padding = New System.Windows.Forms.Padding(4)
        Me.tpMoviePreferences_Scraper.Size = New System.Drawing.Size(992, 571)
        Me.tpMoviePreferences_Scraper.TabIndex = 0
        Me.tpMoviePreferences_Scraper.Text = "Scraper"
        '
        'GroupBox25
        '
        Me.GroupBox25.Controls.Add(Me.CheckBox_Use_XBMC_Scraper)
        Me.GroupBox25.Controls.Add(Me.GroupBox_TMDB_Scraper_Preferences)
        Me.GroupBox25.Controls.Add(Me.GroupBox_MovieIMDBMirror)
        Me.GroupBox25.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.Size = New System.Drawing.Size(319, 358)
        Me.GroupBox25.TabIndex = 72
        Me.GroupBox25.TabStop = false
        Me.GroupBox25.Text = "Choose Default Scraper"
        '
        'CheckBox_Use_XBMC_Scraper
        '
        Me.CheckBox_Use_XBMC_Scraper.AutoSize = true
        Me.CheckBox_Use_XBMC_Scraper.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckBox_Use_XBMC_Scraper.Location = New System.Drawing.Point(7, 21)
        Me.CheckBox_Use_XBMC_Scraper.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox_Use_XBMC_Scraper.Name = "CheckBox_Use_XBMC_Scraper"
        Me.CheckBox_Use_XBMC_Scraper.Size = New System.Drawing.Size(170, 19)
        Me.CheckBox_Use_XBMC_Scraper.TabIndex = 65
        Me.CheckBox_Use_XBMC_Scraper.Text = "Use TMDB XBMC Scraper"
        Me.CheckBox_Use_XBMC_Scraper.UseVisualStyleBackColor = true
        '
        'GroupBox_TMDB_Scraper_Preferences
        '
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.cmbxTMDBPreferredCertCountry)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.Label5)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.GroupBox46)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.cbXbmcTmdbActorDL)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.cbXbmcTmdbRename)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.Label155)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.cmbxXbmcTmdbTitleLanguage)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.cbXbmcTmdbFanart)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.cmbxXbmcTmdbHDTrailer)
        Me.GroupBox_TMDB_Scraper_Preferences.Controls.Add(Me.Label153)
        Me.GroupBox_TMDB_Scraper_Preferences.Location = New System.Drawing.Point(10, 48)
        Me.GroupBox_TMDB_Scraper_Preferences.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox_TMDB_Scraper_Preferences.Name = "GroupBox_TMDB_Scraper_Preferences"
        Me.GroupBox_TMDB_Scraper_Preferences.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox_TMDB_Scraper_Preferences.Size = New System.Drawing.Size(300, 303)
        Me.GroupBox_TMDB_Scraper_Preferences.TabIndex = 68
        Me.GroupBox_TMDB_Scraper_Preferences.TabStop = false
        Me.GroupBox_TMDB_Scraper_Preferences.Text = "XBMC Scraper Preferences - TMDB"
        '
        'cmbxTMDBPreferredCertCountry
        '
        Me.cmbxTMDBPreferredCertCountry.FormattingEnabled = true
        Me.cmbxTMDBPreferredCertCountry.Location = New System.Drawing.Point(216, 215)
        Me.cmbxTMDBPreferredCertCountry.Name = "cmbxTMDBPreferredCertCountry"
        Me.cmbxTMDBPreferredCertCountry.Size = New System.Drawing.Size(72, 21)
        Me.cmbxTMDBPreferredCertCountry.TabIndex = 84
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(9, 219)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 13)
        Me.Label5.TabIndex = 83
        Me.Label5.Text = "Preferred Country Certificate"
        '
        'GroupBox46
        '
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbAkasFromImdb)
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbCertFromImdb)
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbVotesFromImdb)
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbTop250FromImdb)
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbIMDBRatings)
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbStarsFromImdb)
        Me.GroupBox46.Controls.Add(Me.cbXbmcTmdbOutlineFromImdb)
        Me.GroupBox46.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox46.Location = New System.Drawing.Point(11, 41)
        Me.GroupBox46.Name = "GroupBox46"
        Me.GroupBox46.Size = New System.Drawing.Size(244, 109)
        Me.GroupBox46.TabIndex = 82
        Me.GroupBox46.TabStop = false
        Me.GroupBox46.Text = "Scrape the following from IMDB"
        '
        'cbXbmcTmdbAkasFromImdb
        '
        Me.cbXbmcTmdbAkasFromImdb.AutoSize = true
        Me.cbXbmcTmdbAkasFromImdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbAkasFromImdb.Location = New System.Drawing.Point(22, 82)
        Me.cbXbmcTmdbAkasFromImdb.Name = "cbXbmcTmdbAkasFromImdb"
        Me.cbXbmcTmdbAkasFromImdb.Size = New System.Drawing.Size(78, 19)
        Me.cbXbmcTmdbAkasFromImdb.TabIndex = 82
        Me.cbXbmcTmdbAkasFromImdb.Text = "Aka Titles"
        Me.cbXbmcTmdbAkasFromImdb.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbCertFromImdb
        '
        Me.cbXbmcTmdbCertFromImdb.AutoSize = true
        Me.cbXbmcTmdbCertFromImdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbCertFromImdb.Location = New System.Drawing.Point(117, 42)
        Me.cbXbmcTmdbCertFromImdb.Margin = New System.Windows.Forms.Padding(4)
        Me.cbXbmcTmdbCertFromImdb.Name = "cbXbmcTmdbCertFromImdb"
        Me.cbXbmcTmdbCertFromImdb.Size = New System.Drawing.Size(80, 19)
        Me.cbXbmcTmdbCertFromImdb.TabIndex = 81
        Me.cbXbmcTmdbCertFromImdb.Text = "Certificate"
        Me.cbXbmcTmdbCertFromImdb.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbVotesFromImdb
        '
        Me.cbXbmcTmdbVotesFromImdb.AutoSize = true
        Me.cbXbmcTmdbVotesFromImdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbVotesFromImdb.Location = New System.Drawing.Point(117, 62)
        Me.cbXbmcTmdbVotesFromImdb.Margin = New System.Windows.Forms.Padding(4)
        Me.cbXbmcTmdbVotesFromImdb.Name = "cbXbmcTmdbVotesFromImdb"
        Me.cbXbmcTmdbVotesFromImdb.Size = New System.Drawing.Size(56, 19)
        Me.cbXbmcTmdbVotesFromImdb.TabIndex = 78
        Me.cbXbmcTmdbVotesFromImdb.Text = "Votes"
        Me.cbXbmcTmdbVotesFromImdb.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbTop250FromImdb
        '
        Me.cbXbmcTmdbTop250FromImdb.AutoSize = true
        Me.cbXbmcTmdbTop250FromImdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbTop250FromImdb.Location = New System.Drawing.Point(22, 42)
        Me.cbXbmcTmdbTop250FromImdb.Margin = New System.Windows.Forms.Padding(4)
        Me.cbXbmcTmdbTop250FromImdb.Name = "cbXbmcTmdbTop250FromImdb"
        Me.cbXbmcTmdbTop250FromImdb.Size = New System.Drawing.Size(68, 19)
        Me.cbXbmcTmdbTop250FromImdb.TabIndex = 79
        Me.cbXbmcTmdbTop250FromImdb.Text = "Top250"
        Me.cbXbmcTmdbTop250FromImdb.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbIMDBRatings
        '
        Me.cbXbmcTmdbIMDBRatings.AutoSize = true
        Me.cbXbmcTmdbIMDBRatings.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbIMDBRatings.Location = New System.Drawing.Point(22, 62)
        Me.cbXbmcTmdbIMDBRatings.Margin = New System.Windows.Forms.Padding(4)
        Me.cbXbmcTmdbIMDBRatings.Name = "cbXbmcTmdbIMDBRatings"
        Me.cbXbmcTmdbIMDBRatings.Size = New System.Drawing.Size(62, 19)
        Me.cbXbmcTmdbIMDBRatings.TabIndex = 71
        Me.cbXbmcTmdbIMDBRatings.Text = "Rating"
        Me.cbXbmcTmdbIMDBRatings.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbStarsFromImdb
        '
        Me.cbXbmcTmdbStarsFromImdb.AutoSize = true
        Me.cbXbmcTmdbStarsFromImdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbStarsFromImdb.Location = New System.Drawing.Point(117, 22)
        Me.cbXbmcTmdbStarsFromImdb.Margin = New System.Windows.Forms.Padding(4)
        Me.cbXbmcTmdbStarsFromImdb.Name = "cbXbmcTmdbStarsFromImdb"
        Me.cbXbmcTmdbStarsFromImdb.Size = New System.Drawing.Size(54, 19)
        Me.cbXbmcTmdbStarsFromImdb.TabIndex = 80
        Me.cbXbmcTmdbStarsFromImdb.Text = "Stars"
        Me.cbXbmcTmdbStarsFromImdb.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbOutlineFromImdb
        '
        Me.cbXbmcTmdbOutlineFromImdb.AutoSize = true
        Me.cbXbmcTmdbOutlineFromImdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbXbmcTmdbOutlineFromImdb.Location = New System.Drawing.Point(22, 22)
        Me.cbXbmcTmdbOutlineFromImdb.Name = "cbXbmcTmdbOutlineFromImdb"
        Me.cbXbmcTmdbOutlineFromImdb.Size = New System.Drawing.Size(65, 19)
        Me.cbXbmcTmdbOutlineFromImdb.TabIndex = 77
        Me.cbXbmcTmdbOutlineFromImdb.Text = "Outline"
        Me.cbXbmcTmdbOutlineFromImdb.UseVisualStyleBackColor = true
        '
        'cbXbmcTmdbRename
        '
        Me.cbXbmcTmdbRename.AutoSize = true
        Me.cbXbmcTmdbRename.Location = New System.Drawing.Point(8, 244)
        Me.cbXbmcTmdbRename.Name = "cbXbmcTmdbRename"
        Me.cbXbmcTmdbRename.Size = New System.Drawing.Size(220, 17)
        Me.cbXbmcTmdbRename.TabIndex = 73
        Me.cbXbmcTmdbRename.Text = "Enable Renaming of XBMC TMDB movie"
        Me.cbXbmcTmdbRename.UseVisualStyleBackColor = true
        '
        'Label155
        '
        Me.Label155.AutoSize = true
        Me.Label155.Location = New System.Drawing.Point(8, 167)
        Me.Label155.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(170, 13)
        Me.Label155.TabIndex = 72
        Me.Label155.Text = "Enable trailers from HD-Trailers.net"
        '
        'cmbxXbmcTmdbTitleLanguage
        '
        Me.cmbxXbmcTmdbTitleLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxXbmcTmdbTitleLanguage.FormattingEnabled = true
        Me.cmbxXbmcTmdbTitleLanguage.Location = New System.Drawing.Point(165, 189)
        Me.cmbxXbmcTmdbTitleLanguage.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbxXbmcTmdbTitleLanguage.Name = "cmbxXbmcTmdbTitleLanguage"
        Me.cmbxXbmcTmdbTitleLanguage.Size = New System.Drawing.Size(125, 21)
        Me.cmbxXbmcTmdbTitleLanguage.Sorted = true
        Me.cmbxXbmcTmdbTitleLanguage.TabIndex = 70
        '
        'cbXbmcTmdbFanart
        '
        Me.cbXbmcTmdbFanart.AutoSize = true
        Me.cbXbmcTmdbFanart.Location = New System.Drawing.Point(11, 20)
        Me.cbXbmcTmdbFanart.Margin = New System.Windows.Forms.Padding(4)
        Me.cbXbmcTmdbFanart.Name = "cbXbmcTmdbFanart"
        Me.cbXbmcTmdbFanart.Size = New System.Drawing.Size(89, 17)
        Me.cbXbmcTmdbFanart.TabIndex = 67
        Me.cbXbmcTmdbFanart.Text = "Enable fanart"
        Me.cbXbmcTmdbFanart.UseVisualStyleBackColor = true
        '
        'cmbxXbmcTmdbHDTrailer
        '
        Me.cmbxXbmcTmdbHDTrailer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxXbmcTmdbHDTrailer.FormattingEnabled = true
        Me.cmbxXbmcTmdbHDTrailer.Location = New System.Drawing.Point(216, 164)
        Me.cmbxXbmcTmdbHDTrailer.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbxXbmcTmdbHDTrailer.Name = "cmbxXbmcTmdbHDTrailer"
        Me.cmbxXbmcTmdbHDTrailer.Size = New System.Drawing.Size(74, 21)
        Me.cmbxXbmcTmdbHDTrailer.Sorted = true
        Me.cmbxXbmcTmdbHDTrailer.TabIndex = 6
        '
        'Label153
        '
        Me.Label153.AutoSize = true
        Me.Label153.Location = New System.Drawing.Point(9, 190)
        Me.Label153.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(97, 13)
        Me.Label153.TabIndex = 69
        Me.Label153.Text = "Preferred language"
        '
        'GroupBox_MovieIMDBMirror
        '
        Me.GroupBox_MovieIMDBMirror.Controls.Add(Me.cbImdbPrimaryPlot)
        Me.GroupBox_MovieIMDBMirror.Controls.Add(Me.Label181)
        Me.GroupBox_MovieIMDBMirror.Controls.Add(Me.cbImdbgetTMDBActor)
        Me.GroupBox_MovieIMDBMirror.Controls.Add(Me.Label90)
        Me.GroupBox_MovieIMDBMirror.Controls.Add(Me.Label91)
        Me.GroupBox_MovieIMDBMirror.Controls.Add(Me.lb_IMDBMirrors)
        Me.GroupBox_MovieIMDBMirror.Location = New System.Drawing.Point(10, 48)
        Me.GroupBox_MovieIMDBMirror.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox_MovieIMDBMirror.Name = "GroupBox_MovieIMDBMirror"
        Me.GroupBox_MovieIMDBMirror.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox_MovieIMDBMirror.Size = New System.Drawing.Size(300, 272)
        Me.GroupBox_MovieIMDBMirror.TabIndex = 40
        Me.GroupBox_MovieIMDBMirror.TabStop = false
        Me.GroupBox_MovieIMDBMirror.Text = "MC's IMDB Scraper"
        '
        'cbImdbPrimaryPlot
        '
        Me.cbImdbPrimaryPlot.AutoSize = true
        Me.cbImdbPrimaryPlot.Location = New System.Drawing.Point(10, 200)
        Me.cbImdbPrimaryPlot.Name = "cbImdbPrimaryPlot"
        Me.cbImdbPrimaryPlot.Size = New System.Drawing.Size(136, 17)
        Me.cbImdbPrimaryPlot.TabIndex = 5
        Me.cbImdbPrimaryPlot.Text = "Select only Primary Plot"
        Me.cbImdbPrimaryPlot.UseVisualStyleBackColor = true
        '
        'Label181
        '
        Me.Label181.AutoSize = true
        Me.Label181.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label181.Location = New System.Drawing.Point(21, 183)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(197, 13)
        Me.Label181.TabIndex = 4
        Me.Label181.Text = "Fall-back to IMDB if no images available."
        '
        'cbImdbgetTMDBActor
        '
        Me.cbImdbgetTMDBActor.AutoSize = true
        Me.cbImdbgetTMDBActor.Location = New System.Drawing.Point(9, 163)
        Me.cbImdbgetTMDBActor.Name = "cbImdbgetTMDBActor"
        Me.cbImdbgetTMDBActor.Size = New System.Drawing.Size(228, 17)
        Me.cbImdbgetTMDBActor.TabIndex = 3
        Me.cbImdbgetTMDBActor.Text = "Scrape actors from TMDB instead of IMDB"
        Me.cbImdbgetTMDBActor.UseVisualStyleBackColor = true
        '
        'Label90
        '
        Me.Label90.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label90.Location = New System.Drawing.Point(11, 132)
        Me.Label90.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(279, 32)
        Me.Label90.TabIndex = 2
        Me.Label90.Text = "Using ""www.imdb.de"" will result in an incomplete nfo file since this mirror doesn"& _ 
    "'t contain plot, tagline, or trailer nfo."
        '
        'Label91
        '
        Me.Label91.AutoSize = true
        Me.Label91.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label91.Location = New System.Drawing.Point(6, 16)
        Me.Label91.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(190, 15)
        Me.Label91.TabIndex = 1
        Me.Label91.Text = "Select your preferred IMDB Mirror"
        '
        'lb_IMDBMirrors
        '
        Me.lb_IMDBMirrors.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lb_IMDBMirrors.FormattingEnabled = true
        Me.lb_IMDBMirrors.ItemHeight = 15
        Me.lb_IMDBMirrors.Items.AddRange(New Object() {"http://www.imdb.com/", "http://uk.imdb.com/", "http://us.imdb.com/", "http://akas.imdb.com/", "http://italian.imdb.com/", "http://www.imdb.de/"})
        Me.lb_IMDBMirrors.Location = New System.Drawing.Point(10, 33)
        Me.lb_IMDBMirrors.Margin = New System.Windows.Forms.Padding(4)
        Me.lb_IMDBMirrors.Name = "lb_IMDBMirrors"
        Me.lb_IMDBMirrors.Size = New System.Drawing.Size(282, 94)
        Me.lb_IMDBMirrors.TabIndex = 0
        '
        'gpbxPrefScraperImages
        '
        Me.gpbxPrefScraperImages.Controls.Add(Me.GroupBox11)
        Me.gpbxPrefScraperImages.Controls.Add(Me.GroupBox44)
        Me.gpbxPrefScraperImages.Controls.Add(Me.gbMovieBasicSave)
        Me.gpbxPrefScraperImages.Controls.Add(Me.GroupBox30)
        Me.gpbxPrefScraperImages.Controls.Add(Me.gbScraperMisc)
        Me.gpbxPrefScraperImages.Controls.Add(Me.GroupBox34)
        Me.gpbxPrefScraperImages.Location = New System.Drawing.Point(333, 6)
        Me.gpbxPrefScraperImages.Name = "gpbxPrefScraperImages"
        Me.gpbxPrefScraperImages.Size = New System.Drawing.Size(655, 473)
        Me.gpbxPrefScraperImages.TabIndex = 77
        Me.gpbxPrefScraperImages.TabStop = false
        Me.gpbxPrefScraperImages.Text = "Scraping options"
        '
        'GroupBox44
        '
        Me.GroupBox44.Controls.Add(Me.Label69)
        Me.GroupBox44.Controls.Add(Me.cb_keywordlimit)
        Me.GroupBox44.Controls.Add(Me.cb_keywordasTag)
        Me.GroupBox44.Location = New System.Drawing.Point(343, 22)
        Me.GroupBox44.Name = "GroupBox44"
        Me.GroupBox44.Size = New System.Drawing.Size(300, 78)
        Me.GroupBox44.TabIndex = 84
        Me.GroupBox44.TabStop = false
        Me.GroupBox44.Text = "Keywords As Tags"
        '
        'Label69
        '
        Me.Label69.AutoSize = true
        Me.Label69.Location = New System.Drawing.Point(17, 48)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(149, 13)
        Me.Label69.TabIndex = 66
        Me.Label69.Text = "Maximum number of keywords"
        '
        'cb_keywordasTag
        '
        Me.cb_keywordasTag.AutoSize = true
        Me.cb_keywordasTag.Location = New System.Drawing.Point(13, 23)
        Me.cb_keywordasTag.Name = "cb_keywordasTag"
        Me.cb_keywordasTag.Size = New System.Drawing.Size(180, 17)
        Me.cb_keywordasTag.TabIndex = 0
        Me.cb_keywordasTag.Text = "Store Plot keywords in Tags field"
        Me.cb_keywordasTag.UseVisualStyleBackColor = true
        '
        'gbMovieBasicSave
        '
        Me.gbMovieBasicSave.Controls.Add(Me.cbMovieBasicSave)
        Me.gbMovieBasicSave.Controls.Add(Me.Label162)
        Me.gbMovieBasicSave.Controls.Add(Me.Label109)
        Me.gbMovieBasicSave.Location = New System.Drawing.Point(6, 356)
        Me.gbMovieBasicSave.Name = "gbMovieBasicSave"
        Me.gbMovieBasicSave.Size = New System.Drawing.Size(331, 106)
        Me.gbMovieBasicSave.TabIndex = 83
        Me.gbMovieBasicSave.TabStop = false
        Me.gbMovieBasicSave.Text = "Basic Save Mode"
        '
        'Label162
        '
        Me.Label162.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label162.Location = New System.Drawing.Point(4, 46)
        Me.Label162.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(314, 30)
        Me.Label162.TabIndex = 45
        Me.Label162.Text = "(This is not recommended since it breaks MCs ability to see the  media file, used"& _ 
    " for playback and filedetails information.)"
        '
        'Label109
        '
        Me.Label109.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label109.Location = New System.Drawing.Point(4, 17)
        Me.Label109.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(314, 27)
        Me.Label109.TabIndex = 44
        Me.Label109.Text = "Some people prefer to save their files as movie.nfo, movie.tbn and fanart.jpg to "& _ 
    "keep compatibility with other media managers."
        '
        'GroupBox30
        '
        Me.GroupBox30.Controls.Add(Me.cmbxMovieScraper_MaxStudios)
        Me.GroupBox30.Controls.Add(Me.lblMaxStudios)
        Me.GroupBox30.Controls.Add(Me.Label92)
        Me.GroupBox30.Controls.Add(Me.cmbxMovScraper_MaxGenres)
        Me.GroupBox30.Location = New System.Drawing.Point(6, 196)
        Me.GroupBox30.Name = "GroupBox30"
        Me.GroupBox30.Size = New System.Drawing.Size(330, 80)
        Me.GroupBox30.TabIndex = 75
        Me.GroupBox30.TabStop = false
        Me.GroupBox30.Text = "Scraper Limits"
        '
        'lblMaxStudios
        '
        Me.lblMaxStudios.AutoSize = true
        Me.lblMaxStudios.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMaxStudios.Location = New System.Drawing.Point(6, 49)
        Me.lblMaxStudios.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMaxStudios.Name = "lblMaxStudios"
        Me.lblMaxStudios.Size = New System.Drawing.Size(167, 15)
        Me.lblMaxStudios.TabIndex = 63
        Me.lblMaxStudios.Text = "Maximum number of studios:"
        '
        'Label92
        '
        Me.Label92.AutoSize = true
        Me.Label92.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label92.Location = New System.Drawing.Point(7, 21)
        Me.Label92.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(166, 15)
        Me.Label92.TabIndex = 45
        Me.Label92.Text = "Maximum number of genres:"
        '
        'gbScraperMisc
        '
        Me.gbScraperMisc.Controls.Add(Me.chkbOriginal_Title)
        Me.gbScraperMisc.Controls.Add(Me.cbGetMovieSetFromTMDb)
        Me.gbScraperMisc.Location = New System.Drawing.Point(6, 282)
        Me.gbScraperMisc.Name = "gbScraperMisc"
        Me.gbScraperMisc.Size = New System.Drawing.Size(331, 68)
        Me.gbScraperMisc.TabIndex = 52
        Me.gbScraperMisc.TabStop = false
        Me.gbScraperMisc.Text = " Other options "
        '
        'chkbOriginal_Title
        '
        Me.chkbOriginal_Title.AutoSize = true
        Me.chkbOriginal_Title.Location = New System.Drawing.Point(12, 39)
        Me.chkbOriginal_Title.Name = "chkbOriginal_Title"
        Me.chkbOriginal_Title.Size = New System.Drawing.Size(303, 17)
        Me.chkbOriginal_Title.TabIndex = 51
        Me.chkbOriginal_Title.Text = "IMDB - Where available scrape 'Original title' instead of title"
        Me.chkbOriginal_Title.UseVisualStyleBackColor = true
        '
        'GroupBox34
        '
        Me.GroupBox34.Controls.Add(Me.Label27)
        Me.GroupBox34.Controls.Add(Me.gbCustomLanguage)
        Me.GroupBox34.Controls.Add(Me.Label177)
        Me.GroupBox34.Controls.Add(Me.cbUseCustomLanguage)
        Me.GroupBox34.Controls.Add(Me.comboBoxTMDbSelectedLanguage)
        Me.GroupBox34.Location = New System.Drawing.Point(6, 22)
        Me.GroupBox34.Name = "GroupBox34"
        Me.GroupBox34.Size = New System.Drawing.Size(331, 168)
        Me.GroupBox34.TabIndex = 38
        Me.GroupBox34.TabStop = false
        Me.GroupBox34.Text = "Preferred Language"
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.Location = New System.Drawing.Point(6, 17)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(247, 13)
        Me.Label27.TabIndex = 23
        Me.Label27.Text = "Set language for Images and other info from TMDB"
        '
        'gbCustomLanguage
        '
        Me.gbCustomLanguage.Controls.Add(Me.llLanguagesFile)
        Me.gbCustomLanguage.Controls.Add(Me.tbCustomLanguageValue)
        Me.gbCustomLanguage.Controls.Add(Me.Label174)
        Me.gbCustomLanguage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbCustomLanguage.Location = New System.Drawing.Point(28, 88)
        Me.gbCustomLanguage.Name = "gbCustomLanguage"
        Me.gbCustomLanguage.Size = New System.Drawing.Size(291, 68)
        Me.gbCustomLanguage.TabIndex = 22
        Me.gbCustomLanguage.TabStop = false
        Me.gbCustomLanguage.Text = " Custom string"
        '
        'llLanguagesFile
        '
        Me.llLanguagesFile.AutoSize = true
        Me.llLanguagesFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.llLanguagesFile.Location = New System.Drawing.Point(50, 45)
        Me.llLanguagesFile.Name = "llLanguagesFile"
        Me.llLanguagesFile.Size = New System.Drawing.Size(158, 12)
        Me.llLanguagesFile.TabIndex = 21
        Me.llLanguagesFile.TabStop = true
        Me.llLanguagesFile.Text = "Please read the languages file for help"
        '
        'tbCustomLanguageValue
        '
        Me.tbCustomLanguageValue.Location = New System.Drawing.Point(53, 21)
        Me.tbCustomLanguageValue.Name = "tbCustomLanguageValue"
        Me.tbCustomLanguageValue.Size = New System.Drawing.Size(232, 21)
        Me.tbCustomLanguageValue.TabIndex = 20
        Me.tbCustomLanguageValue.Tag = "M"
        '
        'Label174
        '
        Me.Label174.AutoSize = true
        Me.Label174.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label174.Location = New System.Drawing.Point(6, 24)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(44, 15)
        Me.Label174.TabIndex = 18
        Me.Label174.Text = "Value :"
        '
        'Label177
        '
        Me.Label177.AutoSize = true
        Me.Label177.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label177.Location = New System.Drawing.Point(6, 45)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(102, 15)
        Me.Label177.TabIndex = 21
        Me.Label177.Text = "Select language :"
        '
        'cbUseCustomLanguage
        '
        Me.cbUseCustomLanguage.AutoSize = true
        Me.cbUseCustomLanguage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbUseCustomLanguage.Location = New System.Drawing.Point(7, 71)
        Me.cbUseCustomLanguage.Name = "cbUseCustomLanguage"
        Me.cbUseCustomLanguage.Size = New System.Drawing.Size(137, 19)
        Me.cbUseCustomLanguage.TabIndex = 20
        Me.cbUseCustomLanguage.Text = "Or define your own..."
        Me.cbUseCustomLanguage.UseVisualStyleBackColor = true
        '
        'comboBoxTMDbSelectedLanguage
        '
        Me.comboBoxTMDbSelectedLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxTMDbSelectedLanguage.FormattingEnabled = true
        Me.comboBoxTMDbSelectedLanguage.Location = New System.Drawing.Point(114, 42)
        Me.comboBoxTMDbSelectedLanguage.Name = "comboBoxTMDbSelectedLanguage"
        Me.comboBoxTMDbSelectedLanguage.Size = New System.Drawing.Size(130, 21)
        Me.comboBoxTMDbSelectedLanguage.TabIndex = 19
        '
        'GroupBox24
        '
        Me.GroupBox24.Controls.Add(Me.cbMovieAllInFolders)
        Me.GroupBox24.Controls.Add(Me.cbMovieUseFolderNames)
        Me.GroupBox24.Location = New System.Drawing.Point(7, 370)
        Me.GroupBox24.Name = "GroupBox24"
        Me.GroupBox24.Size = New System.Drawing.Size(319, 80)
        Me.GroupBox24.TabIndex = 78
        Me.GroupBox24.TabStop = false
        Me.GroupBox24.Text = "Individual Movie Folder Options"
        '
        'cbMovieAllInFolders
        '
        Me.cbMovieAllInFolders.AutoSize = true
        Me.cbMovieAllInFolders.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieAllInFolders.Location = New System.Drawing.Point(7, 46)
        Me.cbMovieAllInFolders.Name = "cbMovieAllInFolders"
        Me.cbMovieAllInFolders.Size = New System.Drawing.Size(277, 19)
        Me.cbMovieAllInFolders.TabIndex = 52
        Me.cbMovieAllInFolders.Text = "All Movies are in Folders (allows Extrathumbs)"
        Me.cbMovieAllInFolders.UseVisualStyleBackColor = true
        '
        'cbMovieUseFolderNames
        '
        Me.cbMovieUseFolderNames.AutoSize = true
        Me.cbMovieUseFolderNames.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieUseFolderNames.Location = New System.Drawing.Point(7, 20)
        Me.cbMovieUseFolderNames.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovieUseFolderNames.Name = "cbMovieUseFolderNames"
        Me.cbMovieUseFolderNames.Size = New System.Drawing.Size(316, 19)
        Me.cbMovieUseFolderNames.TabIndex = 50
        Me.cbMovieUseFolderNames.Text = "Use Folder Names for Scraping (allows Extrathumbs)"
        Me.cbMovieUseFolderNames.UseVisualStyleBackColor = true
        '
        'Button82
        '
        Me.Button82.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Button82.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button82.Location = New System.Drawing.Point(431, -1619)
        Me.Button82.Margin = New System.Windows.Forms.Padding(4)
        Me.Button82.Name = "Button82"
        Me.Button82.Size = New System.Drawing.Size(150, 30)
        Me.Button82.TabIndex = 51
        Me.Button82.Text = "Save Changes"
        Me.Button82.UseVisualStyleBackColor = true
        '
        'tpMoviePreferences_Artwork
        '
        Me.tpMoviePreferences_Artwork.Controls.Add(Me.GroupBox47)
        Me.tpMoviePreferences_Artwork.Controls.Add(Me.GrpbxXtraArtwork)
        Me.tpMoviePreferences_Artwork.Controls.Add(Me.GroupBox10)
        Me.tpMoviePreferences_Artwork.Controls.Add(Me.GroupBox37)
        Me.tpMoviePreferences_Artwork.Location = New System.Drawing.Point(4, 22)
        Me.tpMoviePreferences_Artwork.Name = "tpMoviePreferences_Artwork"
        Me.tpMoviePreferences_Artwork.Size = New System.Drawing.Size(184, 48)
        Me.tpMoviePreferences_Artwork.TabIndex = 4
        Me.tpMoviePreferences_Artwork.Text = "Artwork"
        Me.tpMoviePreferences_Artwork.UseVisualStyleBackColor = true
        '
        'GroupBox47
        '
        Me.GroupBox47.Controls.Add(Me.Label37)
        Me.GroupBox47.Controls.Add(Me.tbMovSetArtCentralFolder)
        Me.GroupBox47.Controls.Add(Me.btnMovSetCentralFolderSelect)
        Me.GroupBox47.Controls.Add(Me.rbMovSetArtSetFolder)
        Me.GroupBox47.Controls.Add(Me.rbMovSetFolder)
        Me.GroupBox47.Location = New System.Drawing.Point(348, 226)
        Me.GroupBox47.Name = "GroupBox47"
        Me.GroupBox47.Size = New System.Drawing.Size(327, 175)
        Me.GroupBox47.TabIndex = 83
        Me.GroupBox47.TabStop = false
        Me.GroupBox47.Text = "MovieSet Artwork"
        '
        'Label37
        '
        Me.Label37.AutoSize = true
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label37.Location = New System.Drawing.Point(16, 26)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(278, 52)
        Me.Label37.TabIndex = 5
        Me.Label37.Text = "Please set your preference location for Movie Set Artwork"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"If your Movie is in a "& _ 
    "Set, but not in a Set Folder, or a"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"central folder is not selected, then No Movi"& _ 
    "eSet artwork"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"will be downloaded."
        '
        'tbMovSetArtCentralFolder
        '
        Me.tbMovSetArtCentralFolder.Location = New System.Drawing.Point(5, 139)
        Me.tbMovSetArtCentralFolder.MaxLength = 1000
        Me.tbMovSetArtCentralFolder.Name = "tbMovSetArtCentralFolder"
        Me.tbMovSetArtCentralFolder.ReadOnly = true
        Me.tbMovSetArtCentralFolder.Size = New System.Drawing.Size(294, 20)
        Me.tbMovSetArtCentralFolder.TabIndex = 4
        Me.tbMovSetArtCentralFolder.WordWrap = false
        '
        'btnMovSetCentralFolderSelect
        '
        Me.btnMovSetCentralFolderSelect.Location = New System.Drawing.Point(163, 110)
        Me.btnMovSetCentralFolderSelect.Name = "btnMovSetCentralFolderSelect"
        Me.btnMovSetCentralFolderSelect.Size = New System.Drawing.Size(142, 23)
        Me.btnMovSetCentralFolderSelect.TabIndex = 3
        Me.btnMovSetCentralFolderSelect.Text = "Select Central Folder"
        Me.btnMovSetCentralFolderSelect.UseVisualStyleBackColor = true
        '
        'rbMovSetArtSetFolder
        '
        Me.rbMovSetArtSetFolder.AutoSize = true
        Me.rbMovSetArtSetFolder.Location = New System.Drawing.Point(11, 112)
        Me.rbMovSetArtSetFolder.Name = "rbMovSetArtSetFolder"
        Me.rbMovSetArtSetFolder.Size = New System.Drawing.Size(102, 17)
        Me.rbMovSetArtSetFolder.TabIndex = 2
        Me.rbMovSetArtSetFolder.TabStop = true
        Me.rbMovSetArtSetFolder.Text = "to Central Folder"
        Me.rbMovSetArtSetFolder.UseVisualStyleBackColor = true
        '
        'rbMovSetFolder
        '
        Me.rbMovSetFolder.AutoSize = true
        Me.rbMovSetFolder.Location = New System.Drawing.Point(11, 88)
        Me.rbMovSetFolder.Name = "rbMovSetFolder"
        Me.rbMovSetFolder.Size = New System.Drawing.Size(147, 17)
        Me.rbMovSetFolder.TabIndex = 1
        Me.rbMovSetFolder.TabStop = true
        Me.rbMovSetFolder.Text = "to Movie Collection Folder"
        Me.rbMovSetFolder.UseVisualStyleBackColor = true
        '
        'GrpbxXtraArtwork
        '
        Me.GrpbxXtraArtwork.Controls.Add(Me.Label30)
        Me.GrpbxXtraArtwork.Controls.Add(Me.cmbxMovXtraFanartQty)
        Me.GrpbxXtraArtwork.Controls.Add(Me.GroupBox38)
        Me.GrpbxXtraArtwork.Controls.Add(Me.cbMovXtraFanart)
        Me.GrpbxXtraArtwork.Controls.Add(Me.cbMovXtraThumbs)
        Me.GrpbxXtraArtwork.Location = New System.Drawing.Point(348, 13)
        Me.GrpbxXtraArtwork.Name = "GrpbxXtraArtwork"
        Me.GrpbxXtraArtwork.Size = New System.Drawing.Size(327, 207)
        Me.GrpbxXtraArtwork.TabIndex = 82
        Me.GrpbxXtraArtwork.TabStop = false
        Me.GrpbxXtraArtwork.Text = "If Movies in folders, save extra Artwork"
        '
        'Label30
        '
        Me.Label30.AutoSize = true
        Me.Label30.Location = New System.Drawing.Point(154, 49)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(94, 13)
        Me.Label30.TabIndex = 5
        Me.Label30.Text = "..............Quantity?"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbxMovXtraFanartQty
        '
        Me.cmbxMovXtraFanartQty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxMovXtraFanartQty.FormattingEnabled = true
        Me.cmbxMovXtraFanartQty.Items.AddRange(New Object() {"5", "10", "15", "20"})
        Me.cmbxMovXtraFanartQty.Location = New System.Drawing.Point(254, 46)
        Me.cmbxMovXtraFanartQty.Name = "cmbxMovXtraFanartQty"
        Me.cmbxMovXtraFanartQty.Size = New System.Drawing.Size(64, 21)
        Me.cmbxMovXtraFanartQty.TabIndex = 4
        '
        'GroupBox38
        '
        Me.GroupBox38.Controls.Add(Me.cbMovCreateFanartjpg)
        Me.GroupBox38.Controls.Add(Me.cbMoviePosterInFolder)
        Me.GroupBox38.Controls.Add(Me.cbMovieFanartInFolders)
        Me.GroupBox38.Controls.Add(Me.cbMovCreateFolderjpg)
        Me.GroupBox38.Location = New System.Drawing.Point(7, 78)
        Me.GroupBox38.Name = "GroupBox38"
        Me.GroupBox38.Size = New System.Drawing.Size(311, 119)
        Me.GroupBox38.TabIndex = 3
        Me.GroupBox38.TabStop = false
        Me.GroupBox38.Text = "If Movies In Folders, Save artwork as:"
        '
        'cbMovCreateFolderjpg
        '
        Me.cbMovCreateFolderjpg.AutoSize = true
        Me.cbMovCreateFolderjpg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovCreateFolderjpg.Location = New System.Drawing.Point(6, 69)
        Me.cbMovCreateFolderjpg.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovCreateFolderjpg.Name = "cbMovCreateFolderjpg"
        Me.cbMovCreateFolderjpg.Size = New System.Drawing.Size(216, 19)
        Me.cbMovCreateFolderjpg.TabIndex = 51
        Me.cbMovCreateFolderjpg.Text = "Create folder.jpg file for each folder"
        Me.cbMovCreateFolderjpg.UseVisualStyleBackColor = true
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.btn_MovPosterPriorityRemove)
        Me.GroupBox10.Controls.Add(Me.btn_MovPosterPriorityReset)
        Me.GroupBox10.Controls.Add(Me.Label99)
        Me.GroupBox10.Controls.Add(Me.Label93)
        Me.GroupBox10.Controls.Add(Me.Button61)
        Me.GroupBox10.Controls.Add(Me.Button73)
        Me.GroupBox10.Controls.Add(Me.lbPosterSourcePriorities)
        Me.GroupBox10.Location = New System.Drawing.Point(9, 271)
        Me.GroupBox10.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox10.Size = New System.Drawing.Size(327, 165)
        Me.GroupBox10.TabIndex = 39
        Me.GroupBox10.TabStop = false
        Me.GroupBox10.Text = "Movie Scraper Poster Priority"
        '
        'btn_MovPosterPriorityRemove
        '
        Me.btn_MovPosterPriorityRemove.Location = New System.Drawing.Point(151, 130)
        Me.btn_MovPosterPriorityRemove.Name = "btn_MovPosterPriorityRemove"
        Me.btn_MovPosterPriorityRemove.Size = New System.Drawing.Size(112, 23)
        Me.btn_MovPosterPriorityRemove.TabIndex = 6
        Me.btn_MovPosterPriorityRemove.Text = "Remove from List"
        Me.btn_MovPosterPriorityRemove.UseVisualStyleBackColor = true
        '
        'btn_MovPosterPriorityReset
        '
        Me.btn_MovPosterPriorityReset.Location = New System.Drawing.Point(13, 130)
        Me.btn_MovPosterPriorityReset.Name = "btn_MovPosterPriorityReset"
        Me.btn_MovPosterPriorityReset.Size = New System.Drawing.Size(113, 23)
        Me.btn_MovPosterPriorityReset.TabIndex = 5
        Me.btn_MovPosterPriorityReset.Text = "Reset List"
        Me.btn_MovPosterPriorityReset.UseVisualStyleBackColor = true
        '
        'Label99
        '
        Me.Label99.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label99.Location = New System.Drawing.Point(2, 15)
        Me.Label99.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(293, 20)
        Me.Label99.TabIndex = 4
        Me.Label99.Text = "Prioritise the order that sources are checked below."
        '
        'Label93
        '
        Me.Label93.AutoSize = true
        Me.Label93.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label93.Location = New System.Drawing.Point(227, 58)
        Me.Label93.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(78, 36)
        Me.Label93.TabIndex = 3
        Me.Label93.Text = "Change Priority of"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Default Movie"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Thumbnail Source"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Button61
        '
        Me.Button61.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button61.Location = New System.Drawing.Point(229, 98)
        Me.Button61.Margin = New System.Windows.Forms.Padding(4)
        Me.Button61.Name = "Button61"
        Me.Button61.Size = New System.Drawing.Size(34, 25)
        Me.Button61.TabIndex = 2
        Me.Button61.Text = "↓"
        Me.Button61.UseVisualStyleBackColor = true
        '
        'Button73
        '
        Me.Button73.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button73.Location = New System.Drawing.Point(229, 34)
        Me.Button73.Margin = New System.Windows.Forms.Padding(4)
        Me.Button73.Name = "Button73"
        Me.Button73.Size = New System.Drawing.Size(34, 25)
        Me.Button73.TabIndex = 1
        Me.Button73.Text = "↑"
        Me.Button73.UseVisualStyleBackColor = true
        '
        'lbPosterSourcePriorities
        '
        Me.lbPosterSourcePriorities.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbPosterSourcePriorities.FormattingEnabled = true
        Me.lbPosterSourcePriorities.ItemHeight = 15
        Me.lbPosterSourcePriorities.Location = New System.Drawing.Point(11, 39)
        Me.lbPosterSourcePriorities.Margin = New System.Windows.Forms.Padding(4)
        Me.lbPosterSourcePriorities.Name = "lbPosterSourcePriorities"
        Me.lbPosterSourcePriorities.Size = New System.Drawing.Size(212, 79)
        Me.lbPosterSourcePriorities.TabIndex = 0
        '
        'GroupBox37
        '
        Me.GroupBox37.Controls.Add(Me.cbMovFanartNaming)
        Me.GroupBox37.Controls.Add(Me.btnMovFanartTvSelect)
        Me.GroupBox37.Controls.Add(Me.Label10)
        Me.GroupBox37.Controls.Add(Me.cbMovFanartTvScrape)
        Me.GroupBox37.Controls.Add(Me.GroupBox41)
        Me.GroupBox37.Controls.Add(Me.cbMovSetArtScrape)
        Me.GroupBox37.Controls.Add(Me.cbMovFanartScrape)
        Me.GroupBox37.Controls.Add(Me.cbMoviePosterScrape)
        Me.GroupBox37.Location = New System.Drawing.Point(9, 13)
        Me.GroupBox37.Name = "GroupBox37"
        Me.GroupBox37.Size = New System.Drawing.Size(327, 251)
        Me.GroupBox37.TabIndex = 60
        Me.GroupBox37.TabStop = false
        Me.GroupBox37.Text = "Autoscrape artwork"
        '
        'cbMovFanartNaming
        '
        Me.cbMovFanartNaming.AutoSize = true
        Me.cbMovFanartNaming.Location = New System.Drawing.Point(5, 111)
        Me.cbMovFanartNaming.Name = "cbMovFanartNaming"
        Me.cbMovFanartNaming.Size = New System.Drawing.Size(260, 17)
        Me.cbMovFanartNaming.TabIndex = 48
        Me.cbMovFanartNaming.Text = "Save Fanart.TV artwork as <moviename>-artwork"
        Me.cbMovFanartNaming.UseVisualStyleBackColor = true
        '
        'btnMovFanartTvSelect
        '
        Me.btnMovFanartTvSelect.Location = New System.Drawing.Point(31, 78)
        Me.btnMovFanartTvSelect.Name = "btnMovFanartTvSelect"
        Me.btnMovFanartTvSelect.Size = New System.Drawing.Size(75, 23)
        Me.btnMovFanartTvSelect.TabIndex = 47
        Me.btnMovFanartTvSelect.Text = "Select Art"
        Me.btnMovFanartTvSelect.UseVisualStyleBackColor = true
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(112, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(171, 26)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Choose Fanart.TV art to Download"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Default is All available."
        '
        'GroupBox41
        '
        Me.GroupBox41.Controls.Add(Me.Label189)
        Me.GroupBox41.Controls.Add(Me.cbDlXtraFanart)
        Me.GroupBox41.Location = New System.Drawing.Point(4, 136)
        Me.GroupBox41.Name = "GroupBox41"
        Me.GroupBox41.Size = New System.Drawing.Size(317, 61)
        Me.GroupBox41.TabIndex = 44
        Me.GroupBox41.TabStop = false
        Me.GroupBox41.Text = "Extra Artwork"
        '
        'Label189
        '
        Me.Label189.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label189.Location = New System.Drawing.Point(3, 37)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(293, 18)
        Me.Label189.TabIndex = 4
        Me.Label189.Text = "NB: - Movies must be in folders. Select Type of art below."
        '
        'cbMovSetArtScrape
        '
        Me.cbMovSetArtScrape.AutoSize = true
        Me.cbMovSetArtScrape.Location = New System.Drawing.Point(5, 203)
        Me.cbMovSetArtScrape.Name = "cbMovSetArtScrape"
        Me.cbMovSetArtScrape.Size = New System.Drawing.Size(262, 17)
        Me.cbMovSetArtScrape.TabIndex = 0
        Me.cbMovSetArtScrape.Text = "Download MovieSet Artwork if not already present"
        Me.cbMovSetArtScrape.UseVisualStyleBackColor = true
        '
        'tpMoviePreferences_General
        '
        Me.tpMoviePreferences_General.BackColor = System.Drawing.SystemColors.Control
        Me.tpMoviePreferences_General.Controls.Add(Me.gbMovieFilters)
        Me.tpMoviePreferences_General.Controls.Add(Me.cbMissingMovie)
        Me.tpMoviePreferences_General.Controls.Add(Me.GroupBox35)
        Me.tpMoviePreferences_General.Controls.Add(Me.CheckBox_ShowDateOnMovieList)
        Me.tpMoviePreferences_General.Controls.Add(Me.GroupBox27)
        Me.tpMoviePreferences_General.Controls.Add(Me.GroupBox9)
        Me.tpMoviePreferences_General.Controls.Add(Me.GroupBox26)
        Me.tpMoviePreferences_General.Controls.Add(Me.grpNameMode)
        Me.tpMoviePreferences_General.Location = New System.Drawing.Point(4, 22)
        Me.tpMoviePreferences_General.Name = "tpMoviePreferences_General"
        Me.tpMoviePreferences_General.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMoviePreferences_General.Size = New System.Drawing.Size(184, 48)
        Me.tpMoviePreferences_General.TabIndex = 2
        Me.tpMoviePreferences_General.Text = "General"
        '
        'gbMovieFilters
        '
        Me.gbMovieFilters.Controls.Add(Me.cbMovieFilters_Sets_Order)
        Me.gbMovieFilters.Controls.Add(Me.Label54)
        Me.gbMovieFilters.Controls.Add(Me.cbMovieFilters_Directors_Order)
        Me.gbMovieFilters.Controls.Add(Me.nudMaxDirectorsInFilter)
        Me.gbMovieFilters.Controls.Add(Me.nudMaxSetsInFilter)
        Me.gbMovieFilters.Controls.Add(Me.nudDirectorsFilterMinFilms)
        Me.gbMovieFilters.Controls.Add(Me.nudSetsFilterMinFilms)
        Me.gbMovieFilters.Controls.Add(Me.Label11)
        Me.gbMovieFilters.Controls.Add(Me.Label12)
        Me.gbMovieFilters.Controls.Add(Me.cbMovieFilters_Actors_Order)
        Me.gbMovieFilters.Controls.Add(Me.cbDisableNotMatchingRenamePattern)
        Me.gbMovieFilters.Controls.Add(Me.Label180)
        Me.gbMovieFilters.Controls.Add(Me.Label164)
        Me.gbMovieFilters.Controls.Add(Me.nudMaxActorsInFilter)
        Me.gbMovieFilters.Controls.Add(Me.nudActorsFilterMinFilms)
        Me.gbMovieFilters.Controls.Add(Me.Label165)
        Me.gbMovieFilters.Location = New System.Drawing.Point(681, 172)
        Me.gbMovieFilters.Margin = New System.Windows.Forms.Padding(0)
        Me.gbMovieFilters.Name = "gbMovieFilters"
        Me.gbMovieFilters.Size = New System.Drawing.Size(305, 211)
        Me.gbMovieFilters.TabIndex = 82
        Me.gbMovieFilters.TabStop = false
        Me.gbMovieFilters.Text = " Movie Filters "
        '
        'cbMovieFilters_Sets_Order
        '
        Me.cbMovieFilters_Sets_Order.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieFilters_Sets_Order.FormattingEnabled = true
        Me.cbMovieFilters_Sets_Order.Items.AddRange(New Object() {"Num Movies desc", "A-Z asc"})
        Me.cbMovieFilters_Sets_Order.Location = New System.Drawing.Point(168, 118)
        Me.cbMovieFilters_Sets_Order.Name = "cbMovieFilters_Sets_Order"
        Me.cbMovieFilters_Sets_Order.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieFilters_Sets_Order.TabIndex = 5
        '
        'Label54
        '
        Me.Label54.AutoSize = true
        Me.Label54.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label54.Location = New System.Drawing.Point(2, 119)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(35, 15)
        Me.Label54.TabIndex = 12
        Me.Label54.Text = "Sets"
        '
        'cbMovieFilters_Directors_Order
        '
        Me.cbMovieFilters_Directors_Order.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieFilters_Directors_Order.FormattingEnabled = true
        Me.cbMovieFilters_Directors_Order.Items.AddRange(New Object() {"Num Movies desc", "A-Z asc"})
        Me.cbMovieFilters_Directors_Order.Location = New System.Drawing.Point(168, 87)
        Me.cbMovieFilters_Directors_Order.Name = "cbMovieFilters_Directors_Order"
        Me.cbMovieFilters_Directors_Order.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieFilters_Directors_Order.TabIndex = 11
        '
        'nudMaxDirectorsInFilter
        '
        Me.nudMaxDirectorsInFilter.Location = New System.Drawing.Point(114, 87)
        Me.nudMaxDirectorsInFilter.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudMaxDirectorsInFilter.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMaxDirectorsInFilter.Name = "nudMaxDirectorsInFilter"
        Me.nudMaxDirectorsInFilter.Size = New System.Drawing.Size(48, 20)
        Me.nudMaxDirectorsInFilter.TabIndex = 10
        Me.nudMaxDirectorsInFilter.Value = New Decimal(New Integer() {5000, 0, 0, 0})
        '
        'nudMaxSetsInFilter
        '
        Me.nudMaxSetsInFilter.Location = New System.Drawing.Point(114, 118)
        Me.nudMaxSetsInFilter.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudMaxSetsInFilter.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMaxSetsInFilter.Name = "nudMaxSetsInFilter"
        Me.nudMaxSetsInFilter.Size = New System.Drawing.Size(48, 20)
        Me.nudMaxSetsInFilter.TabIndex = 3
        Me.nudMaxSetsInFilter.Value = New Decimal(New Integer() {5000, 0, 0, 0})
        '
        'nudDirectorsFilterMinFilms
        '
        Me.nudDirectorsFilterMinFilms.Location = New System.Drawing.Point(68, 86)
        Me.nudDirectorsFilterMinFilms.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudDirectorsFilterMinFilms.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudDirectorsFilterMinFilms.Name = "nudDirectorsFilterMinFilms"
        Me.nudDirectorsFilterMinFilms.Size = New System.Drawing.Size(37, 20)
        Me.nudDirectorsFilterMinFilms.TabIndex = 9
        Me.nudDirectorsFilterMinFilms.Value = New Decimal(New Integer() {99, 0, 0, 0})
        '
        'nudSetsFilterMinFilms
        '
        Me.nudSetsFilterMinFilms.Location = New System.Drawing.Point(68, 117)
        Me.nudSetsFilterMinFilms.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudSetsFilterMinFilms.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudSetsFilterMinFilms.Name = "nudSetsFilterMinFilms"
        Me.nudSetsFilterMinFilms.Size = New System.Drawing.Size(37, 20)
        Me.nudSetsFilterMinFilms.TabIndex = 1
        Me.nudSetsFilterMinFilms.Value = New Decimal(New Integer() {99, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(2, 88)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 15)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Directors"
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(2, 58)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 15)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "Actors"
        '
        'cbMovieFilters_Actors_Order
        '
        Me.cbMovieFilters_Actors_Order.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieFilters_Actors_Order.FormattingEnabled = true
        Me.cbMovieFilters_Actors_Order.Items.AddRange(New Object() {"Num Movies desc", "A-Z asc"})
        Me.cbMovieFilters_Actors_Order.Location = New System.Drawing.Point(168, 55)
        Me.cbMovieFilters_Actors_Order.Name = "cbMovieFilters_Actors_Order"
        Me.cbMovieFilters_Actors_Order.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieFilters_Actors_Order.TabIndex = 5
        '
        'cbDisableNotMatchingRenamePattern
        '
        Me.cbDisableNotMatchingRenamePattern.Location = New System.Drawing.Point(10, 152)
        Me.cbDisableNotMatchingRenamePattern.Name = "cbDisableNotMatchingRenamePattern"
        Me.cbDisableNotMatchingRenamePattern.Size = New System.Drawing.Size(297, 38)
        Me.cbDisableNotMatchingRenamePattern.TabIndex = 6
        Me.cbDisableNotMatchingRenamePattern.Text = "Disable 'Not matching rename pattern' (check for better performance)"
        Me.cbDisableNotMatchingRenamePattern.UseVisualStyleBackColor = true
        '
        'Label180
        '
        Me.Label180.AutoSize = true
        Me.Label180.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label180.Location = New System.Drawing.Point(195, 32)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(43, 15)
        Me.Label180.TabIndex = 4
        Me.Label180.Text = "Order"
        '
        'Label164
        '
        Me.Label164.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label164.Location = New System.Drawing.Point(64, 20)
        Me.Label164.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(53, 33)
        Me.Label164.TabIndex = 0
        Me.Label164.Text = "Min Movies"
        '
        'nudMaxActorsInFilter
        '
        Me.nudMaxActorsInFilter.Location = New System.Drawing.Point(114, 57)
        Me.nudMaxActorsInFilter.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudMaxActorsInFilter.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMaxActorsInFilter.Name = "nudMaxActorsInFilter"
        Me.nudMaxActorsInFilter.Size = New System.Drawing.Size(48, 20)
        Me.nudMaxActorsInFilter.TabIndex = 3
        Me.nudMaxActorsInFilter.Value = New Decimal(New Integer() {5000, 0, 0, 0})
        '
        'nudActorsFilterMinFilms
        '
        Me.nudActorsFilterMinFilms.Location = New System.Drawing.Point(68, 56)
        Me.nudActorsFilterMinFilms.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudActorsFilterMinFilms.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudActorsFilterMinFilms.Name = "nudActorsFilterMinFilms"
        Me.nudActorsFilterMinFilms.Size = New System.Drawing.Size(37, 20)
        Me.nudActorsFilterMinFilms.TabIndex = 1
        Me.nudActorsFilterMinFilms.Value = New Decimal(New Integer() {99, 0, 0, 0})
        '
        'Label165
        '
        Me.Label165.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label165.Location = New System.Drawing.Point(121, 20)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(38, 33)
        Me.Label165.TabIndex = 2
        Me.Label165.Text = "Max list"
        '
        'cbMissingMovie
        '
        Me.cbMissingMovie.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbMissingMovie.Location = New System.Drawing.Point(13, 365)
        Me.cbMissingMovie.Name = "cbMissingMovie"
        Me.cbMissingMovie.Size = New System.Drawing.Size(275, 36)
        Me.cbMissingMovie.TabIndex = 81
        Me.cbMissingMovie.Text = "Include Missing video Files when rebuilding cache."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Valid nfo's but missing video"& _ 
    ", will highlighted in Red."
        Me.cbMissingMovie.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbMissingMovie.UseVisualStyleBackColor = true
        '
        'GroupBox35
        '
        Me.GroupBox35.Controls.Add(Me.cbMovieList_ShowColWatched)
        Me.GroupBox35.Controls.Add(Me.cbMovieList_ShowColPlot)
        Me.GroupBox35.Controls.Add(Me.tbDateFormat)
        Me.GroupBox35.Controls.Add(Me.Label179)
        Me.GroupBox35.Location = New System.Drawing.Point(323, 395)
        Me.GroupBox35.Name = "GroupBox35"
        Me.GroupBox35.Size = New System.Drawing.Size(311, 89)
        Me.GroupBox35.TabIndex = 80
        Me.GroupBox35.TabStop = false
        Me.GroupBox35.Text = " Movie List "
        '
        'cbMovieList_ShowColWatched
        '
        Me.cbMovieList_ShowColWatched.AutoSize = true
        Me.cbMovieList_ShowColWatched.Location = New System.Drawing.Point(15, 64)
        Me.cbMovieList_ShowColWatched.Name = "cbMovieList_ShowColWatched"
        Me.cbMovieList_ShowColWatched.Size = New System.Drawing.Size(137, 17)
        Me.cbMovieList_ShowColWatched.TabIndex = 3
        Me.cbMovieList_ShowColWatched.Text = "Show column Watched"
        Me.cbMovieList_ShowColWatched.UseVisualStyleBackColor = true
        '
        'cbMovieList_ShowColPlot
        '
        Me.cbMovieList_ShowColPlot.AutoSize = true
        Me.cbMovieList_ShowColPlot.Location = New System.Drawing.Point(15, 41)
        Me.cbMovieList_ShowColPlot.Name = "cbMovieList_ShowColPlot"
        Me.cbMovieList_ShowColPlot.Size = New System.Drawing.Size(111, 17)
        Me.cbMovieList_ShowColPlot.TabIndex = 2
        Me.cbMovieList_ShowColPlot.Text = "Show column Plot"
        Me.cbMovieList_ShowColPlot.UseVisualStyleBackColor = true
        '
        'CheckBox_ShowDateOnMovieList
        '
        Me.CheckBox_ShowDateOnMovieList.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.CheckBox_ShowDateOnMovieList.Enabled = false
        Me.CheckBox_ShowDateOnMovieList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckBox_ShowDateOnMovieList.Location = New System.Drawing.Point(13, 333)
        Me.CheckBox_ShowDateOnMovieList.Name = "CheckBox_ShowDateOnMovieList"
        Me.CheckBox_ShowDateOnMovieList.Size = New System.Drawing.Size(275, 37)
        Me.CheckBox_ShowDateOnMovieList.TabIndex = 72
        Me.CheckBox_ShowDateOnMovieList.Text = "Display Date on Movie Sort for 'Modified' && 'Date Added'"
        Me.CheckBox_ShowDateOnMovieList.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.CheckBox_ShowDateOnMovieList.UseVisualStyleBackColor = true
        '
        'GroupBox27
        '
        Me.GroupBox27.Controls.Add(Me.Label13)
        Me.GroupBox27.Controls.Add(Me.rbRenameFullStop)
        Me.GroupBox27.Controls.Add(Me.rbRenameUnderscore)
        Me.GroupBox27.Controls.Add(Me.cbMovNewFolderInRootFolder)
        Me.GroupBox27.Controls.Add(Me.cbMovSortIgnArticle)
        Me.GroupBox27.Controls.Add(Me.cbMovTitleIgnArticle)
        Me.GroupBox27.Controls.Add(Me.cbMovSetIgnArticle)
        Me.GroupBox27.Controls.Add(Me.Label197)
        Me.GroupBox27.Controls.Add(Me.Label196)
        Me.GroupBox27.Controls.Add(Me.cbMovFolderRename)
        Me.GroupBox27.Controls.Add(Me.cbRenameUnderscore)
        Me.GroupBox27.Controls.Add(Me.lblFolderRename)
        Me.GroupBox27.Controls.Add(Me.tb_MovFolderRename)
        Me.GroupBox27.Controls.Add(Me.LblFilename)
        Me.GroupBox27.Controls.Add(Me.ManualRenameChkbox)
        Me.GroupBox27.Controls.Add(Me.cbMovieRenameEnable)
        Me.GroupBox27.Controls.Add(Me.Label100)
        Me.GroupBox27.Controls.Add(Me.tb_MovieRenameEnable)
        Me.GroupBox27.Location = New System.Drawing.Point(323, 6)
        Me.GroupBox27.Name = "GroupBox27"
        Me.GroupBox27.Size = New System.Drawing.Size(352, 381)
        Me.GroupBox27.TabIndex = 74
        Me.GroupBox27.TabStop = false
        Me.GroupBox27.Text = "Rename Movie Settings"
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.Location = New System.Drawing.Point(188, 257)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(18, 13)
        Me.Label13.TabIndex = 86
        Me.Label13.Text = "Or"
        '
        'cbMovNewFolderInRootFolder
        '
        Me.cbMovNewFolderInRootFolder.AutoSize = true
        Me.cbMovNewFolderInRootFolder.Location = New System.Drawing.Point(7, 150)
        Me.cbMovNewFolderInRootFolder.Name = "cbMovNewFolderInRootFolder"
        Me.cbMovNewFolderInRootFolder.Size = New System.Drawing.Size(182, 30)
        Me.cbMovNewFolderInRootFolder.TabIndex = 83
        Me.cbMovNewFolderInRootFolder.Text = "Create Folder under Root folder if"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  not in a Movie Set"
        Me.cbMovNewFolderInRootFolder.UseVisualStyleBackColor = true
        '
        'cbMovSortIgnArticle
        '
        Me.cbMovSortIgnArticle.AutoSize = true
        Me.cbMovSortIgnArticle.Location = New System.Drawing.Point(7, 228)
        Me.cbMovSortIgnArticle.Name = "cbMovSortIgnArticle"
        Me.cbMovSortIgnArticle.Size = New System.Drawing.Size(178, 17)
        Me.cbMovSortIgnArticle.TabIndex = 82
        Me.cbMovSortIgnArticle.Text = "Ignore Article to end of Sort Title"
        Me.cbMovSortIgnArticle.UseVisualStyleBackColor = true
        '
        'cbMovTitleIgnArticle
        '
        Me.cbMovTitleIgnArticle.AutoSize = true
        Me.cbMovTitleIgnArticle.Location = New System.Drawing.Point(7, 184)
        Me.cbMovTitleIgnArticle.Name = "cbMovTitleIgnArticle"
        Me.cbMovTitleIgnArticle.Size = New System.Drawing.Size(188, 17)
        Me.cbMovTitleIgnArticle.TabIndex = 81
        Me.cbMovTitleIgnArticle.Text = "Ignore Article to end of Movie Title"
        Me.cbMovTitleIgnArticle.UseVisualStyleBackColor = true
        '
        'cbMovSetIgnArticle
        '
        Me.cbMovSetIgnArticle.AutoSize = true
        Me.cbMovSetIgnArticle.Location = New System.Drawing.Point(7, 206)
        Me.cbMovSetIgnArticle.Name = "cbMovSetIgnArticle"
        Me.cbMovSetIgnArticle.Size = New System.Drawing.Size(184, 17)
        Me.cbMovSetIgnArticle.TabIndex = 80
        Me.cbMovSetIgnArticle.Text = "Ignore Article to end of Movie Set"
        Me.cbMovSetIgnArticle.UseVisualStyleBackColor = true
        '
        'Label197
        '
        Me.Label197.AutoSize = true
        Me.Label197.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label197.Location = New System.Drawing.Point(248, 12)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(67, 15)
        Me.Label197.TabIndex = 79
        Me.Label197.Text = "LEGEND:"
        '
        'Label196
        '
        Me.Label196.AutoSize = true
        Me.Label196.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label196.Location = New System.Drawing.Point(6, 318)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(263, 52)
        Me.Label196.TabIndex = 78
        Me.Label196.Text = resources.GetString("Label196.Text")
        '
        'cbMovFolderRename
        '
        Me.cbMovFolderRename.AutoSize = true
        Me.cbMovFolderRename.Location = New System.Drawing.Point(7, 20)
        Me.cbMovFolderRename.Name = "cbMovFolderRename"
        Me.cbMovFolderRename.Size = New System.Drawing.Size(157, 17)
        Me.cbMovFolderRename.TabIndex = 77
        Me.cbMovFolderRename.Text = "Folder(s) during AutoScrape"
        Me.cbMovFolderRename.UseVisualStyleBackColor = true
        '
        'cbRenameUnderscore
        '
        Me.cbRenameUnderscore.AutoSize = true
        Me.cbRenameUnderscore.Location = New System.Drawing.Point(7, 249)
        Me.cbRenameUnderscore.Name = "cbRenameUnderscore"
        Me.cbRenameUnderscore.Size = New System.Drawing.Size(144, 17)
        Me.cbRenameUnderscore.TabIndex = 76
        Me.cbRenameUnderscore.Text = "Exchange Spaces with..."
        Me.cbRenameUnderscore.UseVisualStyleBackColor = true
        '
        'lblFolderRename
        '
        Me.lblFolderRename.AutoSize = true
        Me.lblFolderRename.Location = New System.Drawing.Point(6, 40)
        Me.lblFolderRename.Name = "lblFolderRename"
        Me.lblFolderRename.Size = New System.Drawing.Size(67, 13)
        Me.lblFolderRename.TabIndex = 75
        Me.lblFolderRename.Text = "Folder Name"
        '
        'tb_MovFolderRename
        '
        Me.tb_MovFolderRename.Location = New System.Drawing.Point(6, 56)
        Me.tb_MovFolderRename.Name = "tb_MovFolderRename"
        Me.tb_MovFolderRename.Size = New System.Drawing.Size(189, 20)
        Me.tb_MovFolderRename.TabIndex = 74
        Me.tb_MovFolderRename.Text = "%N\%T (%Y)"
        '
        'LblFilename
        '
        Me.LblFilename.AutoSize = true
        Me.LblFilename.Location = New System.Drawing.Point(6, 107)
        Me.LblFilename.Name = "LblFilename"
        Me.LblFilename.Size = New System.Drawing.Size(57, 13)
        Me.LblFilename.TabIndex = 73
        Me.LblFilename.Text = "File Name:"
        '
        'ManualRenameChkbox
        '
        Me.ManualRenameChkbox.AutoSize = true
        Me.ManualRenameChkbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ManualRenameChkbox.Location = New System.Drawing.Point(7, 299)
        Me.ManualRenameChkbox.Margin = New System.Windows.Forms.Padding(4)
        Me.ManualRenameChkbox.Name = "ManualRenameChkbox"
        Me.ManualRenameChkbox.Size = New System.Drawing.Size(207, 19)
        Me.ManualRenameChkbox.TabIndex = 72
        Me.ManualRenameChkbox.Text = "Enable Manual Movie Renaming"
        Me.ManualRenameChkbox.UseVisualStyleBackColor = true
        '
        'cbMovieRenameEnable
        '
        Me.cbMovieRenameEnable.AutoSize = true
        Me.cbMovieRenameEnable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieRenameEnable.Location = New System.Drawing.Point(7, 84)
        Me.cbMovieRenameEnable.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovieRenameEnable.Name = "cbMovieRenameEnable"
        Me.cbMovieRenameEnable.Size = New System.Drawing.Size(163, 19)
        Me.cbMovieRenameEnable.TabIndex = 71
        Me.cbMovieRenameEnable.Text = "Movie during AutoScrape"
        Me.cbMovieRenameEnable.UseVisualStyleBackColor = true
        '
        'Label100
        '
        Me.Label100.AutoSize = true
        Me.Label100.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label100.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label100.Location = New System.Drawing.Point(201, 28)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(142, 210)
        Me.Label100.TabIndex = 70
        Me.Label100.Text = resources.GetString("Label100.Text")
        '
        'tb_MovieRenameEnable
        '
        Me.tb_MovieRenameEnable.Location = New System.Drawing.Point(7, 125)
        Me.tb_MovieRenameEnable.Name = "tb_MovieRenameEnable"
        Me.tb_MovieRenameEnable.Size = New System.Drawing.Size(188, 20)
        Me.tb_MovieRenameEnable.TabIndex = 69
        Me.tb_MovieRenameEnable.Text = "%T (%Y)"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label77)
        Me.GroupBox9.Controls.Add(Me.TextBox_OfflineDVDTitle)
        Me.GroupBox9.Location = New System.Drawing.Point(681, 395)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(305, 61)
        Me.GroupBox9.TabIndex = 70
        Me.GroupBox9.TabStop = false
        Me.GroupBox9.Text = "Offline DVD Title Text"
        '
        'Label77
        '
        Me.Label77.AutoSize = true
        Me.Label77.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label77.Location = New System.Drawing.Point(7, 44)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(173, 13)
        Me.Label77.TabIndex = 70
        Me.Label77.Text = "Use Parameters  -  %T - Movie Title"
        '
        'TextBox_OfflineDVDTitle
        '
        Me.TextBox_OfflineDVDTitle.Location = New System.Drawing.Point(7, 20)
        Me.TextBox_OfflineDVDTitle.Name = "TextBox_OfflineDVDTitle"
        Me.TextBox_OfflineDVDTitle.Size = New System.Drawing.Size(286, 20)
        Me.TextBox_OfflineDVDTitle.TabIndex = 69
        Me.TextBox_OfflineDVDTitle.Text = "Please Insert '%T' Media"
        '
        'GroupBox26
        '
        Me.GroupBox26.Controls.Add(Me.cb_MovDurationAsRuntine)
        Me.GroupBox26.Controls.Add(Me.cbMovThousSeparator)
        Me.GroupBox26.Controls.Add(Me.cbExcludeMpaaRated)
        Me.GroupBox26.Controls.Add(Me.cbMovTitleCase)
        Me.GroupBox26.Controls.Add(Me.cbXtraFrodoUrls)
        Me.GroupBox26.Controls.Add(Me.cbNoAltTitle)
        Me.GroupBox26.Controls.Add(Me.Label102)
        Me.GroupBox26.Controls.Add(Me.Label78)
        Me.GroupBox26.Controls.Add(Me.cbPreferredTrailerResolution)
        Me.GroupBox26.Controls.Add(Me.cbDlTrailerDuringScrape)
        Me.GroupBox26.Controls.Add(Me.PanelDisplayRuntime)
        Me.GroupBox26.Controls.Add(Me.CheckBox16)
        Me.GroupBox26.Controls.Add(Me.CheckBox19)
        Me.GroupBox26.Controls.Add(Me.cbMovieTrailerUrl)
        Me.GroupBox26.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox26.Name = "GroupBox26"
        Me.GroupBox26.Size = New System.Drawing.Size(311, 320)
        Me.GroupBox26.TabIndex = 73
        Me.GroupBox26.TabStop = false
        Me.GroupBox26.Text = "General Options"
        '
        'cb_MovDurationAsRuntine
        '
        Me.cb_MovDurationAsRuntine.AutoSize = true
        Me.cb_MovDurationAsRuntine.Location = New System.Drawing.Point(26, 237)
        Me.cb_MovDurationAsRuntine.Name = "cb_MovDurationAsRuntine"
        Me.cb_MovDurationAsRuntine.Size = New System.Drawing.Size(186, 17)
        Me.cb_MovDurationAsRuntine.TabIndex = 78
        Me.cb_MovDurationAsRuntine.Text = "Save Media Duration As Runtime."
        Me.cb_MovDurationAsRuntine.UseVisualStyleBackColor = true
        '
        'cbMovThousSeparator
        '
        Me.cbMovThousSeparator.AutoSize = true
        Me.cbMovThousSeparator.Location = New System.Drawing.Point(7, 196)
        Me.cbMovThousSeparator.Name = "cbMovThousSeparator"
        Me.cbMovThousSeparator.Size = New System.Drawing.Size(268, 17)
        Me.cbMovThousSeparator.TabIndex = 77
        Me.cbMovThousSeparator.Text = "Set , as Thousands separator in Votes saved to nfo"
        Me.cbMovThousSeparator.UseVisualStyleBackColor = true
        '
        'cbExcludeMpaaRated
        '
        Me.cbExcludeMpaaRated.AutoSize = true
        Me.cbExcludeMpaaRated.Location = New System.Drawing.Point(7, 177)
        Me.cbExcludeMpaaRated.Name = "cbExcludeMpaaRated"
        Me.cbExcludeMpaaRated.Size = New System.Drawing.Size(231, 17)
        Me.cbExcludeMpaaRated.TabIndex = 76
        Me.cbExcludeMpaaRated.Text = "Exclude ""Rated"" prefix on MPAA certificate"
        Me.cbExcludeMpaaRated.UseVisualStyleBackColor = true
        '
        'cbXtraFrodoUrls
        '
        Me.cbXtraFrodoUrls.AutoSize = true
        Me.cbXtraFrodoUrls.Location = New System.Drawing.Point(7, 135)
        Me.cbXtraFrodoUrls.Name = "cbXtraFrodoUrls"
        Me.cbXtraFrodoUrls.Size = New System.Drawing.Size(239, 17)
        Me.cbXtraFrodoUrls.TabIndex = 74
        Me.cbXtraFrodoUrls.Text = "Disable extra Frodo Poster and Thumb URL's"
        Me.cbXtraFrodoUrls.UseVisualStyleBackColor = true
        '
        'cbNoAltTitle
        '
        Me.cbNoAltTitle.AutoSize = true
        Me.cbNoAltTitle.Location = New System.Drawing.Point(7, 114)
        Me.cbNoAltTitle.Name = "cbNoAltTitle"
        Me.cbNoAltTitle.Size = New System.Drawing.Size(210, 17)
        Me.cbNoAltTitle.TabIndex = 73
        Me.cbNoAltTitle.Text = "Exclude adding Alternative Titles to nfo"
        Me.cbNoAltTitle.UseVisualStyleBackColor = true
        '
        'PanelDisplayRuntime
        '
        Me.PanelDisplayRuntime.Controls.Add(Me.cbMovieRuntimeFallbackToFile)
        Me.PanelDisplayRuntime.Controls.Add(Me.Label70)
        Me.PanelDisplayRuntime.Controls.Add(Me.rbRuntimeFile)
        Me.PanelDisplayRuntime.Controls.Add(Me.rbRuntimeScraper)
        Me.PanelDisplayRuntime.Enabled = false
        Me.PanelDisplayRuntime.Location = New System.Drawing.Point(7, 262)
        Me.PanelDisplayRuntime.Name = "PanelDisplayRuntime"
        Me.PanelDisplayRuntime.Size = New System.Drawing.Size(268, 49)
        Me.PanelDisplayRuntime.TabIndex = 68
        '
        'Label70
        '
        Me.Label70.AutoSize = true
        Me.Label70.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label70.Location = New System.Drawing.Point(3, 4)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(128, 15)
        Me.Label70.TabIndex = 65
        Me.Label70.Text = "Display Runtime from:"
        '
        'rbRuntimeFile
        '
        Me.rbRuntimeFile.AutoSize = true
        Me.rbRuntimeFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbRuntimeFile.Location = New System.Drawing.Point(207, 2)
        Me.rbRuntimeFile.Name = "rbRuntimeFile"
        Me.rbRuntimeFile.Size = New System.Drawing.Size(45, 19)
        Me.rbRuntimeFile.TabIndex = 67
        Me.rbRuntimeFile.Text = "File"
        Me.rbRuntimeFile.UseVisualStyleBackColor = true
        '
        'rbRuntimeScraper
        '
        Me.rbRuntimeScraper.AutoSize = true
        Me.rbRuntimeScraper.Checked = true
        Me.rbRuntimeScraper.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbRuntimeScraper.Location = New System.Drawing.Point(133, 2)
        Me.rbRuntimeScraper.Name = "rbRuntimeScraper"
        Me.rbRuntimeScraper.Size = New System.Drawing.Size(68, 19)
        Me.rbRuntimeScraper.TabIndex = 66
        Me.rbRuntimeScraper.TabStop = true
        Me.rbRuntimeScraper.Text = "Scraper"
        Me.rbRuntimeScraper.UseVisualStyleBackColor = true
        '
        'CheckBox16
        '
        Me.CheckBox16.AutoSize = true
        Me.CheckBox16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckBox16.Location = New System.Drawing.Point(7, 156)
        Me.CheckBox16.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox16.Name = "CheckBox16"
        Me.CheckBox16.Size = New System.Drawing.Size(205, 19)
        Me.CheckBox16.TabIndex = 63
        Me.CheckBox16.Text = "Display log after scraping Movies"
        Me.CheckBox16.UseVisualStyleBackColor = true
        '
        'CheckBox19
        '
        Me.CheckBox19.AutoSize = true
        Me.CheckBox19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckBox19.Location = New System.Drawing.Point(7, 217)
        Me.CheckBox19.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox19.Name = "CheckBox19"
        Me.CheckBox19.Size = New System.Drawing.Size(251, 19)
        Me.CheckBox19.TabIndex = 64
        Me.CheckBox19.Text = "Enable MC to save Media Tags to nfo file."
        Me.CheckBox19.UseVisualStyleBackColor = true
        '
        'cbMovieTrailerUrl
        '
        Me.cbMovieTrailerUrl.AutoSize = true
        Me.cbMovieTrailerUrl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieTrailerUrl.Location = New System.Drawing.Point(7, 16)
        Me.cbMovieTrailerUrl.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMovieTrailerUrl.Name = "cbMovieTrailerUrl"
        Me.cbMovieTrailerUrl.Size = New System.Drawing.Size(186, 19)
        Me.cbMovieTrailerUrl.TabIndex = 49
        Me.cbMovieTrailerUrl.Text = "Add movie trailer url to nfo file"
        Me.cbMovieTrailerUrl.UseVisualStyleBackColor = true
        '
        'grpNameMode
        '
        Me.grpNameMode.Controls.Add(Me.Label163)
        Me.grpNameMode.Controls.Add(Me.lblNameMode)
        Me.grpNameMode.Controls.Add(Me.cbxCleanFilenameIgnorePart)
        Me.grpNameMode.Controls.Add(Me.lblNameModeEg)
        Me.grpNameMode.Controls.Add(Me.cbxNameMode)
        Me.grpNameMode.Location = New System.Drawing.Point(681, 6)
        Me.grpNameMode.Name = "grpNameMode"
        Me.grpNameMode.Size = New System.Drawing.Size(305, 152)
        Me.grpNameMode.TabIndex = 78
        Me.grpNameMode.TabStop = false
        Me.grpNameMode.Text = "Name Mode"
        '
        'Label163
        '
        Me.Label163.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label163.Location = New System.Drawing.Point(102, 119)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(187, 33)
        Me.Label163.TabIndex = 82
        Me.Label163.Text = "Disable stacking function for movies containing 'Pt' or 'Part' in the filename."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)& _ 
    ""
        '
        'lblNameMode
        '
        Me.lblNameMode.AutoSize = true
        Me.lblNameMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblNameMode.Location = New System.Drawing.Point(48, 52)
        Me.lblNameMode.Name = "lblNameMode"
        Me.lblNameMode.Size = New System.Drawing.Size(8, 12)
        Me.lblNameMode.TabIndex = 5
        Me.lblNameMode.Text = "."
        '
        'cbxCleanFilenameIgnorePart
        '
        Me.cbxCleanFilenameIgnorePart.AutoSize = true
        Me.cbxCleanFilenameIgnorePart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbxCleanFilenameIgnorePart.Location = New System.Drawing.Point(7, 119)
        Me.cbxCleanFilenameIgnorePart.Name = "cbxCleanFilenameIgnorePart"
        Me.cbxCleanFilenameIgnorePart.Size = New System.Drawing.Size(92, 19)
        Me.cbxCleanFilenameIgnorePart.TabIndex = 81
        Me.cbxCleanFilenameIgnorePart.Text = "Ignore 'Part'"
        Me.cbxCleanFilenameIgnorePart.UseVisualStyleBackColor = true
        '
        'lblNameModeEg
        '
        Me.lblNameModeEg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblNameModeEg.Location = New System.Drawing.Point(22, 49)
        Me.lblNameModeEg.Margin = New System.Windows.Forms.Padding(0)
        Me.lblNameModeEg.Name = "lblNameModeEg"
        Me.lblNameModeEg.Size = New System.Drawing.Size(25, 20)
        Me.lblNameModeEg.TabIndex = 4
        Me.lblNameModeEg.Text = "eg."
        '
        'cbxNameMode
        '
        Me.cbxNameMode.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbxNameMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbxNameMode.Location = New System.Drawing.Point(7, 16)
        Me.cbxNameMode.Name = "cbxNameMode"
        Me.cbxNameMode.Size = New System.Drawing.Size(257, 34)
        Me.cbxNameMode.TabIndex = 3
        Me.cbxNameMode.Text = "Determines how anciliary files for multi-part movies are labelled."
        Me.cbxNameMode.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.cbxNameMode.UseVisualStyleBackColor = true
        '
        'tpMoviePreferences_Advanced
        '
        Me.tpMoviePreferences_Advanced.AutoScroll = true
        Me.tpMoviePreferences_Advanced.AutoScrollMinSize = New System.Drawing.Size(928, 370)
        Me.tpMoviePreferences_Advanced.BackColor = System.Drawing.SystemColors.Control
        Me.tpMoviePreferences_Advanced.Controls.Add(Me.gb_MovieIdentifier)
        Me.tpMoviePreferences_Advanced.Controls.Add(Me.GroupBox8)
        Me.tpMoviePreferences_Advanced.Controls.Add(Me.GroupBox14)
        Me.tpMoviePreferences_Advanced.Controls.Add(Me.GroupBox16)
        Me.tpMoviePreferences_Advanced.Location = New System.Drawing.Point(4, 22)
        Me.tpMoviePreferences_Advanced.Margin = New System.Windows.Forms.Padding(4)
        Me.tpMoviePreferences_Advanced.Name = "tpMoviePreferences_Advanced"
        Me.tpMoviePreferences_Advanced.Padding = New System.Windows.Forms.Padding(4)
        Me.tpMoviePreferences_Advanced.Size = New System.Drawing.Size(184, 48)
        Me.tpMoviePreferences_Advanced.TabIndex = 1
        Me.tpMoviePreferences_Advanced.Text = "Advanced"
        '
        'gb_MovieIdentifier
        '
        Me.gb_MovieIdentifier.Controls.Add(Me.btn_MovSepReset)
        Me.gb_MovieIdentifier.Controls.Add(Me.Label198)
        Me.gb_MovieIdentifier.Controls.Add(Me.btn_MovSepRem)
        Me.gb_MovieIdentifier.Controls.Add(Me.btn_MovSepAdd)
        Me.gb_MovieIdentifier.Controls.Add(Me.tb_MovSeptb)
        Me.gb_MovieIdentifier.Controls.Add(Me.lb_MovSepLst)
        Me.gb_MovieIdentifier.Location = New System.Drawing.Point(7, 8)
        Me.gb_MovieIdentifier.Name = "gb_MovieIdentifier"
        Me.gb_MovieIdentifier.Size = New System.Drawing.Size(387, 408)
        Me.gb_MovieIdentifier.TabIndex = 76
        Me.gb_MovieIdentifier.TabStop = false
        Me.gb_MovieIdentifier.Text = "Separate Movie Identifyer"
        '
        'btn_MovSepReset
        '
        Me.btn_MovSepReset.Location = New System.Drawing.Point(215, 281)
        Me.btn_MovSepReset.Name = "btn_MovSepReset"
        Me.btn_MovSepReset.Size = New System.Drawing.Size(75, 23)
        Me.btn_MovSepReset.TabIndex = 5
        Me.btn_MovSepReset.Text = "Reset List"
        Me.btn_MovSepReset.UseVisualStyleBackColor = true
        '
        'Label198
        '
        Me.Label198.Location = New System.Drawing.Point(203, 42)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(178, 226)
        Me.Label198.TabIndex = 4
        Me.Label198.Text = resources.GetString("Label198.Text")
        '
        'btn_MovSepRem
        '
        Me.btn_MovSepRem.Location = New System.Drawing.Point(215, 338)
        Me.btn_MovSepRem.Name = "btn_MovSepRem"
        Me.btn_MovSepRem.Size = New System.Drawing.Size(75, 23)
        Me.btn_MovSepRem.TabIndex = 3
        Me.btn_MovSepRem.Text = "Remove"
        Me.btn_MovSepRem.UseVisualStyleBackColor = true
        '
        'btn_MovSepAdd
        '
        Me.btn_MovSepAdd.Location = New System.Drawing.Point(215, 372)
        Me.btn_MovSepAdd.Name = "btn_MovSepAdd"
        Me.btn_MovSepAdd.Size = New System.Drawing.Size(75, 23)
        Me.btn_MovSepAdd.TabIndex = 2
        Me.btn_MovSepAdd.Text = "Add"
        Me.btn_MovSepAdd.UseVisualStyleBackColor = true
        '
        'tb_MovSeptb
        '
        Me.tb_MovSeptb.Location = New System.Drawing.Point(6, 372)
        Me.tb_MovSeptb.Name = "tb_MovSeptb"
        Me.tb_MovSeptb.Size = New System.Drawing.Size(191, 20)
        Me.tb_MovSeptb.TabIndex = 1
        '
        'lb_MovSepLst
        '
        Me.lb_MovSepLst.FormattingEnabled = true
        Me.lb_MovSepLst.Location = New System.Drawing.Point(6, 42)
        Me.lb_MovSepLst.Name = "lb_MovSepLst"
        Me.lb_MovSepLst.Size = New System.Drawing.Size(191, 316)
        Me.lb_MovSepLst.TabIndex = 0
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.cbMovRootFolderCheck)
        Me.GroupBox8.Location = New System.Drawing.Point(484, 125)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(326, 46)
        Me.GroupBox8.TabIndex = 75
        Me.GroupBox8.TabStop = false
        Me.GroupBox8.Text = "Root folder check"
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.chkbx_unstackfanartnames)
        Me.GroupBox14.Controls.Add(Me.chkbx_unstackposternames)
        Me.GroupBox14.Controls.Add(Me.Label110)
        Me.GroupBox14.Location = New System.Drawing.Point(484, 8)
        Me.GroupBox14.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox14.Size = New System.Drawing.Size(327, 110)
        Me.GroupBox14.TabIndex = 50
        Me.GroupBox14.TabStop = false
        Me.GroupBox14.Text = "Advanced Filename Settings"
        '
        'chkbx_unstackfanartnames
        '
        Me.chkbx_unstackfanartnames.AutoSize = true
        Me.chkbx_unstackfanartnames.Enabled = false
        Me.chkbx_unstackfanartnames.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkbx_unstackfanartnames.Location = New System.Drawing.Point(8, 68)
        Me.chkbx_unstackfanartnames.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbx_unstackfanartnames.Name = "chkbx_unstackfanartnames"
        Me.chkbx_unstackfanartnames.Size = New System.Drawing.Size(311, 19)
        Me.chkbx_unstackfanartnames.TabIndex = 41
        Me.chkbx_unstackfanartnames.Text = "Use unstacked filename method for fanart filenames"
        Me.chkbx_unstackfanartnames.UseVisualStyleBackColor = true
        '
        'chkbx_unstackposternames
        '
        Me.chkbx_unstackposternames.AutoSize = true
        Me.chkbx_unstackposternames.Enabled = false
        Me.chkbx_unstackposternames.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkbx_unstackposternames.Location = New System.Drawing.Point(8, 48)
        Me.chkbx_unstackposternames.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbx_unstackposternames.Name = "chkbx_unstackposternames"
        Me.chkbx_unstackposternames.Size = New System.Drawing.Size(314, 19)
        Me.chkbx_unstackposternames.TabIndex = 39
        Me.chkbx_unstackposternames.Text = "Use unstacked filename method for poster filenames"
        Me.chkbx_unstackposternames.UseVisualStyleBackColor = true
        '
        'Label110
        '
        Me.Label110.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label110.Location = New System.Drawing.Point(8, 18)
        Me.Label110.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(318, 30)
        Me.Label110.TabIndex = 38
        Me.Label110.Text = "If you are having an issue with your version of XBMC seeing your .tbn posters or "& _ 
    "fanart.jpg, then you can change the option below:"
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.Label88)
        Me.GroupBox16.Controls.Add(Me.imdb_chk)
        Me.GroupBox16.Controls.Add(Me.mpdb_chk)
        Me.GroupBox16.Controls.Add(Me.tmdb_chk)
        Me.GroupBox16.Controls.Add(Me.IMPA_chk)
        Me.GroupBox16.Controls.Add(Me.Label89)
        Me.GroupBox16.Location = New System.Drawing.Point(484, 206)
        Me.GroupBox16.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox16.Size = New System.Drawing.Size(463, 115)
        Me.GroupBox16.TabIndex = 49
        Me.GroupBox16.TabStop = false
        Me.GroupBox16.Text = "nfo Poster Options"
        '
        'Label88
        '
        Me.Label88.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label88.Location = New System.Drawing.Point(8, 78)
        Me.Label88.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(447, 28)
        Me.Label88.TabIndex = 5
        Me.Label88.Text = "Urls added to the nfo file can be browsed from within XBMC. Each option added wil"& _ 
    "l slow down the scrape and rescrape functions."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        '
        'imdb_chk
        '
        Me.imdb_chk.AutoSize = true
        Me.imdb_chk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.imdb_chk.Location = New System.Drawing.Point(84, 55)
        Me.imdb_chk.Margin = New System.Windows.Forms.Padding(4)
        Me.imdb_chk.Name = "imdb_chk"
        Me.imdb_chk.Size = New System.Drawing.Size(57, 19)
        Me.imdb_chk.TabIndex = 4
        Me.imdb_chk.Text = "IMDB"
        Me.imdb_chk.UseVisualStyleBackColor = true
        '
        'mpdb_chk
        '
        Me.mpdb_chk.AutoSize = true
        Me.mpdb_chk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.mpdb_chk.Location = New System.Drawing.Point(84, 36)
        Me.mpdb_chk.Margin = New System.Windows.Forms.Padding(4)
        Me.mpdb_chk.Name = "mpdb_chk"
        Me.mpdb_chk.Size = New System.Drawing.Size(62, 19)
        Me.mpdb_chk.TabIndex = 3
        Me.mpdb_chk.Text = "MPDB"
        Me.mpdb_chk.UseVisualStyleBackColor = true
        '
        'tmdb_chk
        '
        Me.tmdb_chk.AutoSize = true
        Me.tmdb_chk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tmdb_chk.Location = New System.Drawing.Point(11, 55)
        Me.tmdb_chk.Margin = New System.Windows.Forms.Padding(4)
        Me.tmdb_chk.Name = "tmdb_chk"
        Me.tmdb_chk.Size = New System.Drawing.Size(61, 19)
        Me.tmdb_chk.TabIndex = 2
        Me.tmdb_chk.Text = "TMDB"
        Me.tmdb_chk.UseVisualStyleBackColor = true
        '
        'IMPA_chk
        '
        Me.IMPA_chk.AutoSize = true
        Me.IMPA_chk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.IMPA_chk.Location = New System.Drawing.Point(11, 36)
        Me.IMPA_chk.Margin = New System.Windows.Forms.Padding(4)
        Me.IMPA_chk.Name = "IMPA_chk"
        Me.IMPA_chk.Size = New System.Drawing.Size(55, 19)
        Me.IMPA_chk.TabIndex = 1
        Me.IMPA_chk.Text = "IMPA"
        Me.IMPA_chk.UseVisualStyleBackColor = true
        '
        'Label89
        '
        Me.Label89.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label89.Location = New System.Drawing.Point(8, 18)
        Me.Label89.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(447, 13)
        Me.Label89.TabIndex = 0
        Me.Label89.Text = "During autoscrape and rescrape, add poster urls to the nfo file from the followin"& _ 
    "g sources:"
        '
        'TPTVPref
        '
        Me.TPTVPref.Location = New System.Drawing.Point(4, 22)
        Me.TPTVPref.Name = "TPTVPref"
        Me.TPTVPref.Size = New System.Drawing.Size(1000, 597)
        Me.TPTVPref.TabIndex = 8
        Me.TPTVPref.Text = "TV Preferences"
        Me.TPTVPref.UseVisualStyleBackColor = true
        '
        'TPProxy
        '
        Me.TPProxy.Controls.Add(Me.UcGenPref_Proxy1)
        Me.TPProxy.Location = New System.Drawing.Point(4, 22)
        Me.TPProxy.Name = "TPProxy"
        Me.TPProxy.Size = New System.Drawing.Size(1000, 597)
        Me.TPProxy.TabIndex = 10
        Me.TPProxy.Text = "Proxy"
        Me.TPProxy.UseVisualStyleBackColor = true
        '
        'UcGenPref_Proxy1
        '
        Me.UcGenPref_Proxy1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcGenPref_Proxy1.Location = New System.Drawing.Point(0, 0)
        Me.UcGenPref_Proxy1.Name = "UcGenPref_Proxy1"
        Me.UcGenPref_Proxy1.Size = New System.Drawing.Size(1000, 597)
        Me.UcGenPref_Proxy1.TabIndex = 0
        '
        'TPXBMCLink
        '
        Me.TPXBMCLink.Controls.Add(Me.UcGenPref_XbmcLink1)
        Me.TPXBMCLink.Location = New System.Drawing.Point(4, 22)
        Me.TPXBMCLink.Name = "TPXBMCLink"
        Me.TPXBMCLink.Size = New System.Drawing.Size(1000, 597)
        Me.TPXBMCLink.TabIndex = 9
        Me.TPXBMCLink.Text = "XBMC Link"
        Me.TPXBMCLink.UseVisualStyleBackColor = true
        '
        'UcGenPref_XbmcLink1
        '
        Me.UcGenPref_XbmcLink1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcGenPref_XbmcLink1.Location = New System.Drawing.Point(0, 0)
        Me.UcGenPref_XbmcLink1.Name = "UcGenPref_XbmcLink1"
        Me.UcGenPref_XbmcLink1.Size = New System.Drawing.Size(1000, 597)
        Me.UcGenPref_XbmcLink1.TabIndex = 0
        '
        'TPPRofCmd
        '
        Me.TPPRofCmd.Controls.Add(Me.TableLayoutPanel1)
        Me.TPPRofCmd.Location = New System.Drawing.Point(4, 22)
        Me.TPPRofCmd.Name = "TPPRofCmd"
        Me.TPPRofCmd.Size = New System.Drawing.Size(1000, 597)
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1000, 597)
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
        Me.btn_SettingsCancel.Location = New System.Drawing.Point(140, 585)
        Me.btn_SettingsCancel.Name = "btn_SettingsCancel"
        Me.btn_SettingsCancel.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsCancel.TabIndex = 22
        Me.btn_SettingsCancel.Text = "Cancel"
        Me.btn_SettingsCancel.UseVisualStyleBackColor = true
        '
        'btn_SettingsClose
        '
        Me.btn_SettingsClose.Location = New System.Drawing.Point(260, 585)
        Me.btn_SettingsClose.Name = "btn_SettingsClose"
        Me.btn_SettingsClose.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsClose.TabIndex = 21
        Me.btn_SettingsClose.Text = "Close"
        Me.btn_SettingsClose.UseVisualStyleBackColor = true
        '
        'btn_SettingsApply
        '
        Me.btn_SettingsApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_SettingsApply.Location = New System.Drawing.Point(20, 585)
        Me.btn_SettingsApply.Name = "btn_SettingsApply"
        Me.btn_SettingsApply.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsApply.TabIndex = 20
        Me.btn_SettingsApply.Text = "Apply"
        Me.btn_SettingsApply.UseVisualStyleBackColor = true
        '
        'btn_SettingsClose2
        '
        Me.btn_SettingsClose2.Location = New System.Drawing.Point(520, 585)
        Me.btn_SettingsClose2.Name = "btn_SettingsClose2"
        Me.btn_SettingsClose2.Size = New System.Drawing.Size(109, 23)
        Me.btn_SettingsClose2.TabIndex = 23
        Me.btn_SettingsClose2.Text = "Close"
        Me.btn_SettingsClose2.UseVisualStyleBackColor = true
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 623)
        Me.Controls.Add(Me.btn_SettingsCancel)
        Me.Controls.Add(Me.btn_SettingsClose)
        Me.Controls.Add(Me.btn_SettingsClose2)
        Me.Controls.Add(Me.btn_SettingsApply)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(1016, 650)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(1016, 650)
        Me.Name = "frmOptions"
        Me.Text = "Media Companion Preferences"
        Me.GroupBox36.ResumeLayout(false)
        Me.GroupBox36.PerformLayout
        Me.GroupBox12.ResumeLayout(false)
        Me.GroupBox12.PerformLayout
        Me.gbExcludeFolders.ResumeLayout(false)
        Me.gbExcludeFolders.PerformLayout
        Me.GroupBox11.ResumeLayout(false)
        Me.GroupBox11.PerformLayout
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
        Me.TPMovPref.ResumeLayout(false)
        Me.tcMoviePreferences.ResumeLayout(false)
        Me.tpMoviePreferences_Scraper.ResumeLayout(false)
        Me.GroupBox25.ResumeLayout(false)
        Me.GroupBox25.PerformLayout
        Me.GroupBox_TMDB_Scraper_Preferences.ResumeLayout(false)
        Me.GroupBox_TMDB_Scraper_Preferences.PerformLayout
        Me.GroupBox46.ResumeLayout(false)
        Me.GroupBox46.PerformLayout
        Me.GroupBox_MovieIMDBMirror.ResumeLayout(false)
        Me.GroupBox_MovieIMDBMirror.PerformLayout
        Me.gpbxPrefScraperImages.ResumeLayout(false)
        Me.GroupBox44.ResumeLayout(false)
        Me.GroupBox44.PerformLayout
        Me.gbMovieBasicSave.ResumeLayout(false)
        Me.gbMovieBasicSave.PerformLayout
        Me.GroupBox30.ResumeLayout(false)
        Me.GroupBox30.PerformLayout
        Me.gbScraperMisc.ResumeLayout(false)
        Me.gbScraperMisc.PerformLayout
        Me.GroupBox34.ResumeLayout(false)
        Me.GroupBox34.PerformLayout
        Me.gbCustomLanguage.ResumeLayout(false)
        Me.gbCustomLanguage.PerformLayout
        Me.GroupBox24.ResumeLayout(false)
        Me.GroupBox24.PerformLayout
        Me.tpMoviePreferences_Artwork.ResumeLayout(false)
        Me.GroupBox47.ResumeLayout(false)
        Me.GroupBox47.PerformLayout
        Me.GrpbxXtraArtwork.ResumeLayout(false)
        Me.GrpbxXtraArtwork.PerformLayout
        Me.GroupBox38.ResumeLayout(false)
        Me.GroupBox38.PerformLayout
        Me.GroupBox10.ResumeLayout(false)
        Me.GroupBox10.PerformLayout
        Me.GroupBox37.ResumeLayout(false)
        Me.GroupBox37.PerformLayout
        Me.GroupBox41.ResumeLayout(false)
        Me.GroupBox41.PerformLayout
        Me.tpMoviePreferences_General.ResumeLayout(false)
        Me.gbMovieFilters.ResumeLayout(false)
        Me.gbMovieFilters.PerformLayout
        CType(Me.nudMaxDirectorsInFilter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudMaxSetsInFilter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudDirectorsFilterMinFilms,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudSetsFilterMinFilms,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudMaxActorsInFilter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudActorsFilterMinFilms,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox35.ResumeLayout(false)
        Me.GroupBox35.PerformLayout
        Me.GroupBox27.ResumeLayout(false)
        Me.GroupBox27.PerformLayout
        Me.GroupBox9.ResumeLayout(false)
        Me.GroupBox9.PerformLayout
        Me.GroupBox26.ResumeLayout(false)
        Me.GroupBox26.PerformLayout
        Me.PanelDisplayRuntime.ResumeLayout(false)
        Me.PanelDisplayRuntime.PerformLayout
        Me.grpNameMode.ResumeLayout(false)
        Me.grpNameMode.PerformLayout
        Me.tpMoviePreferences_Advanced.ResumeLayout(false)
        Me.gb_MovieIdentifier.ResumeLayout(false)
        Me.gb_MovieIdentifier.PerformLayout
        Me.GroupBox8.ResumeLayout(false)
        Me.GroupBox8.PerformLayout
        Me.GroupBox14.ResumeLayout(false)
        Me.GroupBox14.PerformLayout
        Me.GroupBox16.ResumeLayout(false)
        Me.GroupBox16.PerformLayout
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
    Friend WithEvents cmbx_MovMaxActors As ComboBox
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
    Friend WithEvents btn_SettingsClose2 As Button
    Friend WithEvents tcMoviePreferences As TabControl
    Friend WithEvents tpMoviePreferences_Scraper As TabPage
    Friend WithEvents GroupBox25 As GroupBox
    Friend WithEvents CheckBox_Use_XBMC_Scraper As CheckBox
    Friend WithEvents GroupBox_TMDB_Scraper_Preferences As GroupBox
    Friend WithEvents cmbxTMDBPreferredCertCountry As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox46 As GroupBox
    Friend WithEvents cbXbmcTmdbAkasFromImdb As CheckBox
    Friend WithEvents cbXbmcTmdbCertFromImdb As CheckBox
    Friend WithEvents cbXbmcTmdbVotesFromImdb As CheckBox
    Friend WithEvents cbXbmcTmdbTop250FromImdb As CheckBox
    Friend WithEvents cbXbmcTmdbIMDBRatings As CheckBox
    Friend WithEvents cbXbmcTmdbStarsFromImdb As CheckBox
    Friend WithEvents cbXbmcTmdbOutlineFromImdb As CheckBox
    Friend WithEvents cbXbmcTmdbActorDL As CheckBox
    Friend WithEvents cbXbmcTmdbRename As CheckBox
    Friend WithEvents Label155 As Label
    Friend WithEvents cmbxXbmcTmdbTitleLanguage As ComboBox
    Friend WithEvents cbXbmcTmdbFanart As CheckBox
    Friend WithEvents cmbxXbmcTmdbHDTrailer As ComboBox
    Friend WithEvents Label153 As Label
    Friend WithEvents GroupBox_MovieIMDBMirror As GroupBox
    Friend WithEvents cbImdbPrimaryPlot As CheckBox
    Friend WithEvents Label181 As Label
    Friend WithEvents cbImdbgetTMDBActor As CheckBox
    Friend WithEvents Label90 As Label
    Friend WithEvents Label91 As Label
    Friend WithEvents lb_IMDBMirrors As ListBox
    Friend WithEvents gpbxPrefScraperImages As GroupBox
    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents ScrapeFullCertCheckBox As CheckBox
    Friend WithEvents Label178 As Label
    Friend WithEvents Label95 As Label
    Friend WithEvents Button74 As Button
    Friend WithEvents Button75 As Button
    Friend WithEvents Label94 As Label
    Friend WithEvents lb_IMDBCertPriority As ListBox
    Friend WithEvents GroupBox44 As GroupBox
    Friend WithEvents Label69 As Label
    Friend WithEvents cb_keywordlimit As ComboBox
    Friend WithEvents cb_keywordasTag As CheckBox
    Friend WithEvents gbMovieBasicSave As GroupBox
    Friend WithEvents cbMovieBasicSave As CheckBox
    Friend WithEvents Label162 As Label
    Friend WithEvents Label109 As Label
    Friend WithEvents GroupBox30 As GroupBox
    Friend WithEvents cmbxMovieScraper_MaxStudios As ComboBox
    Friend WithEvents lblMaxStudios As Label
    Friend WithEvents Label92 As Label
    Friend WithEvents cmbxMovScraper_MaxGenres As ComboBox
    Friend WithEvents gbScraperMisc As GroupBox
    Friend WithEvents chkbOriginal_Title As CheckBox
    Friend WithEvents cbGetMovieSetFromTMDb As CheckBox
    Friend WithEvents GroupBox34 As GroupBox
    Friend WithEvents Label27 As Label
    Friend WithEvents gbCustomLanguage As GroupBox
    Friend WithEvents llLanguagesFile As LinkLabel
    Friend WithEvents tbCustomLanguageValue As TextBox
    Friend WithEvents Label174 As Label
    Friend WithEvents Label177 As Label
    Friend WithEvents cbUseCustomLanguage As CheckBox
    Friend WithEvents comboBoxTMDbSelectedLanguage As ComboBox
    Friend WithEvents GroupBox24 As GroupBox
    Friend WithEvents cbMovieAllInFolders As CheckBox
    Friend WithEvents cbMovieUseFolderNames As CheckBox
    Friend WithEvents Button82 As Button
    Friend WithEvents tpMoviePreferences_Artwork As TabPage
    Friend WithEvents GroupBox47 As GroupBox
    Friend WithEvents Label37 As Label
    Friend WithEvents tbMovSetArtCentralFolder As TextBox
    Friend WithEvents btnMovSetCentralFolderSelect As Button
    Friend WithEvents rbMovSetArtSetFolder As RadioButton
    Friend WithEvents rbMovSetFolder As RadioButton
    Friend WithEvents GrpbxXtraArtwork As GroupBox
    Friend WithEvents Label30 As Label
    Friend WithEvents cmbxMovXtraFanartQty As ComboBox
    Friend WithEvents GroupBox38 As GroupBox
    Friend WithEvents cbMovCreateFanartjpg As CheckBox
    Friend WithEvents cbMoviePosterInFolder As CheckBox
    Friend WithEvents cbMovieFanartInFolders As CheckBox
    Friend WithEvents cbMovCreateFolderjpg As CheckBox
    Friend WithEvents cbMovXtraFanart As CheckBox
    Friend WithEvents cbMovXtraThumbs As CheckBox
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents btn_MovPosterPriorityRemove As Button
    Friend WithEvents btn_MovPosterPriorityReset As Button
    Friend WithEvents Label99 As Label
    Friend WithEvents Label93 As Label
    Friend WithEvents Button61 As Button
    Friend WithEvents Button73 As Button
    Friend WithEvents lbPosterSourcePriorities As ListBox
    Friend WithEvents GroupBox37 As GroupBox
    Friend WithEvents cbMovFanartNaming As CheckBox
    Friend WithEvents btnMovFanartTvSelect As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents cbMovFanartTvScrape As CheckBox
    Friend WithEvents GroupBox41 As GroupBox
    Friend WithEvents Label189 As Label
    Friend WithEvents cbDlXtraFanart As CheckBox
    Friend WithEvents cbMovSetArtScrape As CheckBox
    Friend WithEvents cbMovFanartScrape As CheckBox
    Friend WithEvents cbMoviePosterScrape As CheckBox
    Friend WithEvents tpMoviePreferences_General As TabPage
    Friend WithEvents gbMovieFilters As GroupBox
    Friend WithEvents cbMovieFilters_Sets_Order As ComboBox
    Friend WithEvents Label54 As Label
    Friend WithEvents cbMovieFilters_Directors_Order As ComboBox
    Friend WithEvents nudMaxDirectorsInFilter As NumericUpDown
    Friend WithEvents nudMaxSetsInFilter As NumericUpDown
    Friend WithEvents nudDirectorsFilterMinFilms As NumericUpDown
    Friend WithEvents nudSetsFilterMinFilms As NumericUpDown
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents cbMovieFilters_Actors_Order As ComboBox
    Friend WithEvents cbDisableNotMatchingRenamePattern As CheckBox
    Friend WithEvents Label180 As Label
    Friend WithEvents Label164 As Label
    Friend WithEvents nudMaxActorsInFilter As NumericUpDown
    Friend WithEvents nudActorsFilterMinFilms As NumericUpDown
    Friend WithEvents Label165 As Label
    Friend WithEvents cbMissingMovie As CheckBox
    Friend WithEvents GroupBox35 As GroupBox
    Friend WithEvents cbMovieList_ShowColWatched As CheckBox
    Friend WithEvents cbMovieList_ShowColPlot As CheckBox
    Friend WithEvents tbDateFormat As TextBox
    Friend WithEvents Label179 As Label
    Friend WithEvents CheckBox_ShowDateOnMovieList As CheckBox
    Friend WithEvents GroupBox27 As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents rbRenameFullStop As RadioButton
    Friend WithEvents rbRenameUnderscore As RadioButton
    Friend WithEvents cbMovNewFolderInRootFolder As CheckBox
    Friend WithEvents cbMovSortIgnArticle As CheckBox
    Friend WithEvents cbMovTitleIgnArticle As CheckBox
    Friend WithEvents cbMovSetIgnArticle As CheckBox
    Friend WithEvents Label197 As Label
    Friend WithEvents Label196 As Label
    Friend WithEvents cbMovFolderRename As CheckBox
    Friend WithEvents cbRenameUnderscore As CheckBox
    Friend WithEvents lblFolderRename As Label
    Friend WithEvents tb_MovFolderRename As TextBox
    Friend WithEvents LblFilename As Label
    Friend WithEvents ManualRenameChkbox As CheckBox
    Friend WithEvents cbMovieRenameEnable As CheckBox
    Friend WithEvents Label100 As Label
    Friend WithEvents tb_MovieRenameEnable As TextBox
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents Label77 As Label
    Friend WithEvents TextBox_OfflineDVDTitle As TextBox
    Friend WithEvents GroupBox26 As GroupBox
    Friend WithEvents cb_MovDurationAsRuntine As CheckBox
    Friend WithEvents cbMovThousSeparator As CheckBox
    Friend WithEvents cbExcludeMpaaRated As CheckBox
    Friend WithEvents cbMovTitleCase As CheckBox
    Friend WithEvents cbXtraFrodoUrls As CheckBox
    Friend WithEvents cbNoAltTitle As CheckBox
    Friend WithEvents Label102 As Label
    Friend WithEvents Label78 As Label
    Friend WithEvents cbPreferredTrailerResolution As ComboBox
    Friend WithEvents cbDlTrailerDuringScrape As CheckBox
    Friend WithEvents PanelDisplayRuntime As Panel
    Friend WithEvents cbMovieRuntimeFallbackToFile As CheckBox
    Friend WithEvents Label70 As Label
    Friend WithEvents rbRuntimeFile As RadioButton
    Friend WithEvents rbRuntimeScraper As RadioButton
    Friend WithEvents CheckBox16 As CheckBox
    Friend WithEvents CheckBox19 As CheckBox
    Friend WithEvents cbMovieTrailerUrl As CheckBox
    Friend WithEvents grpNameMode As GroupBox
    Friend WithEvents Label163 As Label
    Friend WithEvents lblNameMode As Label
    Friend WithEvents cbxCleanFilenameIgnorePart As CheckBox
    Friend WithEvents lblNameModeEg As Label
    Friend WithEvents cbxNameMode As CheckBox
    Friend WithEvents tpMoviePreferences_Advanced As TabPage
    Friend WithEvents gb_MovieIdentifier As GroupBox
    Friend WithEvents btn_MovSepReset As Button
    Friend WithEvents Label198 As Label
    Friend WithEvents btn_MovSepRem As Button
    Friend WithEvents btn_MovSepAdd As Button
    Friend WithEvents tb_MovSeptb As TextBox
    Friend WithEvents lb_MovSepLst As ListBox
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents cbMovRootFolderCheck As CheckBox
    Friend WithEvents GroupBox14 As GroupBox
    Friend WithEvents chkbx_unstackfanartnames As CheckBox
    Friend WithEvents chkbx_unstackposternames As CheckBox
    Friend WithEvents Label110 As Label
    Friend WithEvents GroupBox16 As GroupBox
    Friend WithEvents Label88 As Label
    Friend WithEvents imdb_chk As CheckBox
    Friend WithEvents mpdb_chk As CheckBox
    Friend WithEvents tmdb_chk As CheckBox
    Friend WithEvents IMPA_chk As CheckBox
    Friend WithEvents Label89 As Label
End Class
