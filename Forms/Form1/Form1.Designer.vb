﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txt_titlesearch = New System.Windows.Forms.TextBox()
        Me.btnMovieFanartResizeImage = New System.Windows.Forms.Button()
        Me.cbMoviePosterSaveLoRes = New System.Windows.Forms.CheckBox()
        Me.btn_IMPA_posters = New System.Windows.Forms.Button()
        Me.btn_IMDB_posters = New System.Windows.Forms.Button()
        Me.btn_MPDB_posters = New System.Windows.Forms.Button()
        Me.btn_TMDb_posters = New System.Windows.Forms.Button()
        Me.btnTvFanartResize = New System.Windows.Forms.Button()
        Me.btnTvPosterTVDBAll = New System.Windows.Forms.Button()
        Me.btnTvPosterTVDBSpecific = New System.Windows.Forms.Button()
        Me.btn_TvFoldersAddFromRoot = New System.Windows.Forms.Button()
        Me.btn_movTableApply = New System.Windows.Forms.Button()
        Me.ButtonSaveAndQuickRefresh = New System.Windows.Forms.Button()
        Me.panelAvailableMoviePosters = New System.Windows.Forms.Panel()
        Me.tb_Sh_Ep_Title = New System.Windows.Forms.TextBox()
        Me.TvTreeview = New System.Windows.Forms.TreeView()
        Me.TVContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Tv_TreeViewContext_ShowTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_Play_Episode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tv_TreeViewContext_OpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_ViewNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTvDeletenfoart = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTvDelShowNfoArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTvDelShowEpNfoArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTvDelEpNfoArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tv_TreeViewContext_SearchNewEp = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_RefreshShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_ReloadFromCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator29 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tv_TreeViewContext_RenameEp = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_RescrapeShowOrEpisode = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_RescrapeMediaTags = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_MissingEpThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_RescrapeWizard = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator30 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tv_TreeViewContext_WatchedShowOrEpisode = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_UnWatchedShowOrEpisode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tv_TreeViewContext_ShowMissEps = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_TreeViewContext_DispByAiredDate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tv_TreeViewContext_FindMissArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.UnlockAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LockAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExpandSelectedShowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseSelectedShowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpandAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnMovieSetsRepopulateFromUsed = New System.Windows.Forms.Button()
        Me.btnPrevMissingFanart = New System.Windows.Forms.Button()
        Me.btnNextMissingFanart = New System.Windows.Forms.Button()
        Me.btnPrevMissingPoster = New System.Windows.Forms.Button()
        Me.btnNextMissingPoster = New System.Windows.Forms.Button()
        Me.btnMovTagListRefresh = New System.Windows.Forms.Button()
        Me.btnMovTagListRemove = New System.Windows.Forms.Button()
        Me.TagListBox = New System.Windows.Forms.ListBox()
        Me.btnMovTagSavetoNfo = New System.Windows.Forms.Button()
        Me.btnMovTagRemove = New System.Windows.Forms.Button()
        Me.btnMovTagAdd = New System.Windows.Forms.Button()
        Me.roletxt = New System.Windows.Forms.TextBox()
        Me.cbClearCache = New System.Windows.Forms.CheckBox()
        Me.btnMovRefreshAll = New System.Windows.Forms.Button()
        Me.btnMovSearchNew = New System.Windows.Forms.Button()
        Me.PbMovieFanArt = New System.Windows.Forms.PictureBox()
        Me.MovieArtworkContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RescrapeFanartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadFanartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescrapePosterFromTMDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescrapePToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadPosterFromTMDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadPosterFromIMDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadPosterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadPosterFromMPDBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PbMoviePoster = New System.Windows.Forms.PictureBox()
        Me.btnMovSave = New System.Windows.Forms.Button()
        Me.btnMovRescrape = New System.Windows.Forms.Button()
        Me.btn_TMDBSearch = New System.Windows.Forms.Button()
        Me.btn_IMDBSearch = New System.Windows.Forms.Button()
        Me.btnTvRefreshAll = New System.Windows.Forms.Button()
        Me.btnTvSearchNew = New System.Windows.Forms.Button()
        Me.Button44 = New System.Windows.Forms.Button()
        Me.Button_Save_TvShow_Episode = New System.Windows.Forms.Button()
        Me.rbTvMissingPoster = New System.Windows.Forms.RadioButton()
        Me.btnMovFanartToggle = New System.Windows.Forms.Button()
        Me.btnMovPosterToggle = New System.Windows.Forms.Button()
        Me.btn_HMSearch = New System.Windows.Forms.Button()
        Me.btn_HMRefresh = New System.Windows.Forms.Button()
        Me.rbTvListUnKnown = New System.Windows.Forms.RadioButton()
        Me.btn_MovFanartScrnSht = New System.Windows.Forms.Button()
        Me.tagtxt = New System.Windows.Forms.TextBox()
        Me.MovieContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mov_ToolStripMovieName = New System.Windows.Forms.ToolStripMenuItem()
        Me.mov_ToolStripPlayMovie = New System.Windows.Forms.ToolStripMenuItem()
        Me.mov_ToolStripPlayTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mov_ToolStripOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mov_ToolStripViewNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.mov_ToolStripDeleteNfoArtwork = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mov_ToolStripReloadFromCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mov_ToolStripRemoveMovie = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mov_ToolStripRenameMovie = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_RenMovieAndFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_RenMovieOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_RenMovFolderOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mov_ToolStripRescrapeAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mov_ToolStripRescrapeSpecific = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapeCountry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapePremiered = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem21 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTMDbSetName = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapeTop250 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem20 = New System.Windows.Forms.ToolStripMenuItem()
        Me.YearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiRescrapePosterUrls = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapeFrodo_Poster_Thumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapeFrodo_Fanart_Thumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiRescrapeKeyWords = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapeFanartTv = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiRescrapeMovieSetArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDlTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator28 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiSetWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiClearWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.mov_ToolStripFanartBrowserAlt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mov_ToolStripPosterBrowserAlt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mov_ToolStripEditMovieAlt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.mov_ToolStripExportMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiOpenInMkvmergeGUI = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSyncToXBMC = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiConvertToFrodo = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPageLevel2MovMainBrowser = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.cbBtnLink = New System.Windows.Forms.CheckBox()
        Me.TooltipGridViewMovies1 = New Media_Companion.TooltipGridViewMovies()
        Me.DataGridViewMovies = New System.Windows.Forms.DataGridView()
        Me.cbSort = New System.Windows.Forms.ComboBox()
        Me.btnreverse = New System.Windows.Forms.CheckBox()
        Me.DebugSplitter5PosLabel = New System.Windows.Forms.Label()
        Me.btnResetFilters = New System.Windows.Forms.Button()
        Me.LabelCountFilter = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbFolder = New System.Windows.Forms.RadioButton()
        Me.rbFileName = New System.Windows.Forms.RadioButton()
        Me.rbTitleAndYear = New System.Windows.Forms.RadioButton()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmsConfigureMovieFilters = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ConfigureMovieFiltersToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cbFilterSubTitleLang = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterUserRated = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterRootFolder = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterSubTitleLangMode = New System.Windows.Forms.Label()
        Me.lblFilterUserRatedMode = New System.Windows.Forms.Label()
        Me.lblFilterRootFolderMode = New System.Windows.Forms.Label()
        Me.lblFilterSubTitleLang = New System.Windows.Forms.Label()
        Me.lblFilterUserRated = New System.Windows.Forms.Label()
        Me.lblFilterRootFolder = New System.Windows.Forms.Label()
        Me.lblFilterVideoCodec = New System.Windows.Forms.Label()
        Me.lblFilterVideoCodecMode = New System.Windows.Forms.Label()
        Me.cbFilterVideoCodec = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterDirector = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterDirectorMode = New System.Windows.Forms.Label()
        Me.lblFilterDirector = New System.Windows.Forms.Label()
        Me.cbFilterTag = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterTagMode = New System.Windows.Forms.Label()
        Me.lblFilterTag = New System.Windows.Forms.Label()
        Me.lblFilterSourceMode = New System.Windows.Forms.Label()
        Me.cbFilterSource = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterActorMode = New System.Windows.Forms.Label()
        Me.cbFilterActor = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterAudioLanguagesMode = New System.Windows.Forms.Label()
        Me.lblFilterAudioDefaultLanguagesMode = New System.Windows.Forms.Label()
        Me.cbFilterAudioLanguages = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterAudioDefaultLanguages = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterNumAudioTracksMode = New System.Windows.Forms.Label()
        Me.cbFilterNumAudioTracks = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterAudioBitratesMode = New System.Windows.Forms.Label()
        Me.cbFilterAudioBitrates = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterAudioChannelsMode = New System.Windows.Forms.Label()
        Me.cbFilterAudioChannels = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterCertificateMode = New System.Windows.Forms.Label()
        Me.lblFilterAudioCodecsMode = New System.Windows.Forms.Label()
        Me.lblFilterResolutionMode = New System.Windows.Forms.Label()
        Me.lblFilterSetMode = New System.Windows.Forms.Label()
        Me.lblFilterGenreMode = New System.Windows.Forms.Label()
        Me.lblFilterCountriesMode = New System.Windows.Forms.Label()
        Me.lblFilterStudiosMode = New System.Windows.Forms.Label()
        Me.cbFilterAudioCodecs = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterResolution = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterSet = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterCertificate = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterCertificate = New System.Windows.Forms.Label()
        Me.cbFilterGenre = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterCountries = New MC_UserControls.TriStateCheckedComboBox()
        Me.cbFilterStudios = New MC_UserControls.TriStateCheckedComboBox()
        Me.lblFilterYear = New System.Windows.Forms.Label()
        Me.cbFilterYear = New MC_UserControls.SelectionRangeSlider()
        Me.cbFilterVotes = New MC_UserControls.SelectionRangeSlider()
        Me.cbFilterRuntime = New MC_UserControls.SelectionRangeSlider()
        Me.cbFilterFolderSizes = New MC_UserControls.SelectionRangeSlider()
        Me.lblFilterFolderSizes = New System.Windows.Forms.Label()
        Me.lblFilterVotes = New System.Windows.Forms.Label()
        Me.lblFilterRuntime = New System.Windows.Forms.Label()
        Me.lblFilterRating = New System.Windows.Forms.Label()
        Me.lblFilterNumAudioTracks = New System.Windows.Forms.Label()
        Me.lblFilterAudioBitrates = New System.Windows.Forms.Label()
        Me.lblFilterAudioChannels = New System.Windows.Forms.Label()
        Me.lblFilterAudioLanguages = New System.Windows.Forms.Label()
        Me.lblFilterAudioDefaultLanguages = New System.Windows.Forms.Label()
        Me.lblFilterAudioCodecs = New System.Windows.Forms.Label()
        Me.lblFilterResolution = New System.Windows.Forms.Label()
        Me.lblFilterGeneral = New System.Windows.Forms.Label()
        Me.lblFilterActor = New System.Windows.Forms.Label()
        Me.lblFilterSet = New System.Windows.Forms.Label()
        Me.lblFilterSource = New System.Windows.Forms.Label()
        Me.lblFilterCountries = New System.Windows.Forms.Label()
        Me.lblFilterStudios = New System.Windows.Forms.Label()
        Me.lblFilterGenre = New System.Windows.Forms.Label()
        Me.cbFilterGeneral = New System.Windows.Forms.ComboBox()
        Me.cbFilterRating = New MC_UserControls.SelectionRangeSlider()
        Me.ftvArtPicBox = New System.Windows.Forms.PictureBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.movieGraphicInfo = New Media_Companion.GraphicInfo()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.FanTvArtList = New System.Windows.Forms.ListBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tlpMovies = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_movCountry = New System.Windows.Forms.Label()
        Me.rcmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.premiertxt = New System.Windows.Forms.TextBox()
        Me.lbl_movPremiered = New System.Windows.Forms.Label()
        Me.btnMovieDisplay_DirectorFilter = New System.Windows.Forms.Button()
        Me.btnMovieDisplay_CountriesFilter = New System.Windows.Forms.Button()
        Me.lbl_movTags = New System.Windows.Forms.Label()
        Me.lbl_movTop250 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.TextBox34 = New System.Windows.Forms.TextBox()
        Me.titletxt = New System.Windows.Forms.ComboBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxMutisave = New System.Windows.Forms.TextBox()
        Me.plottxt = New System.Windows.Forms.TextBox()
        Me.outlinetxt = New System.Windows.Forms.TextBox()
        Me.taglinetxt = New System.Windows.Forms.TextBox()
        Me.lbl_movTagline = New System.Windows.Forms.Label()
        Me.lbl_movOutline = New System.Windows.Forms.Label()
        Me.PictureBoxActor = New System.Windows.Forms.PictureBox()
        Me.lbl_movPath = New System.Windows.Forms.Label()
        Me.lbl_movSets = New System.Windows.Forms.Label()
        Me.lbl_movRuntime = New System.Windows.Forms.Label()
        Me.lbl_movRating = New System.Windows.Forms.Label()
        Me.lbl_movCert = New System.Windows.Forms.Label()
        Me.lbl_movGenre = New System.Windows.Forms.Label()
        Me.lbl_movStars = New System.Windows.Forms.Label()
        Me.btnMovieDisplay_ActorFilter = New System.Windows.Forms.Button()
        Me.cbMovieDisplay_Actor = New System.Windows.Forms.ComboBox()
        Me.directortxt = New System.Windows.Forms.TextBox()
        Me.creditstxt = New System.Windows.Forms.TextBox()
        Me.studiotxt = New System.Windows.Forms.TextBox()
        Me.cbMovieDisplay_Source = New System.Windows.Forms.ComboBox()
        Me.lbl_movSource = New System.Windows.Forms.Label()
        Me.lbl_movStudio = New System.Windows.Forms.Label()
        Me.lbl_movCredits = New System.Windows.Forms.Label()
        Me.lbl_movDirector = New System.Windows.Forms.Label()
        Me.lbl_movActors = New System.Windows.Forms.Label()
        Me.btnMovieDisplay_SetFilter = New System.Windows.Forms.Button()
        Me.votestxt = New System.Windows.Forms.TextBox()
        Me.lbl_movImdbid = New System.Windows.Forms.Label()
        Me.lbl_movVotes = New System.Windows.Forms.Label()
        Me.pathtxt = New System.Windows.Forms.TextBox()
        Me.cbMovieDisplay_MovieSet = New System.Windows.Forms.ComboBox()
        Me.runtimetxt = New System.Windows.Forms.TextBox()
        Me.ratingtxt = New System.Windows.Forms.TextBox()
        Me.certtxt = New System.Windows.Forms.TextBox()
        Me.genretxt = New System.Windows.Forms.TextBox()
        Me.txtStars = New System.Windows.Forms.TextBox()
        Me.top250txt = New System.Windows.Forms.TextBox()
        Me.countrytxt = New System.Windows.Forms.TextBox()
        Me.tlpMovieButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonTrailer = New System.Windows.Forms.Button()
        Me.btnPlayMovie = New System.Windows.Forms.Button()
        Me.btnMovWatched = New System.Windows.Forms.Button()
        Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_movPlot = New System.Windows.Forms.Label()
        Me.btnMovSelectPlot = New System.Windows.Forms.Button()
        Me.cbUsrRated = New System.Windows.Forms.ComboBox()
        Me.TabPageMovieFanart = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.FanartContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SaveSelectedFanartAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblMovFanartWidth = New System.Windows.Forms.Label()
        Me.btncroptop = New System.Windows.Forms.Button()
        Me.btncropright = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblMovFanartHeight = New System.Windows.Forms.Label()
        Me.btncropleft = New System.Windows.Forms.Button()
        Me.btncropbottom = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnresetimage = New System.Windows.Forms.Button()
        Me.btnSaveCropped = New System.Windows.Forms.Button()
        Me.GroupBoxFanartExtrathumbs = New System.Windows.Forms.GroupBox()
        Me.rbMovThumb5 = New System.Windows.Forms.RadioButton()
        Me.rbMovThumb4 = New System.Windows.Forms.RadioButton()
        Me.rbMovThumb2 = New System.Windows.Forms.RadioButton()
        Me.rbMovThumb3 = New System.Windows.Forms.RadioButton()
        Me.rbMovThumb1 = New System.Windows.Forms.RadioButton()
        Me.rbMovFanart = New System.Windows.Forms.RadioButton()
        Me.lblFanartMissingCount = New System.Windows.Forms.Label()
        Me.btnMovFanartUrlorBrowse = New System.Windows.Forms.Button()
        Me.ButtonFanartSaveLoRes = New System.Windows.Forms.Button()
        Me.ButtonFanartSaveHiRes = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnMovPasteClipboardFanart = New System.Windows.Forms.Button()
        Me.BtnSearchGoogleFanart = New System.Windows.Forms.Button()
        Me.tb_MovFanartScrnShtTime = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPageMoviePoster = New System.Windows.Forms.TabPage()
        Me.panelMoviePosterRHS = New System.Windows.Forms.Panel()
        Me.gbMoviePoster = New System.Windows.Forms.GroupBox()
        Me.PictureBoxAssignedMoviePoster = New System.Windows.Forms.PictureBox()
        Me.lblCurrentLoadedPoster = New System.Windows.Forms.Label()
        Me.gbMoviePosterControls = New System.Windows.Forms.GroupBox()
        Me.BtnGoogleSearchPoster = New System.Windows.Forms.Button()
        Me.btnMovPasteClipboardPoster = New System.Windows.Forms.Button()
        Me.btnMoviePosterEnableCrop = New System.Windows.Forms.Button()
        Me.lblPosterMissingCount = New System.Windows.Forms.Label()
        Me.btnMoviePosterResetImage = New System.Windows.Forms.Button()
        Me.btnMoviePosterSaveCroppedImage = New System.Windows.Forms.Button()
        Me.btnPosterTabs_SaveImage = New System.Windows.Forms.Button()
        Me.tbCurrentMoviePoster = New System.Windows.Forms.TextBox()
        Me.panelMoviePosterLHS = New System.Windows.Forms.Panel()
        Me.gbMoviePostersAvailable = New System.Windows.Forms.GroupBox()
        Me.gbMoviePosterSelection = New System.Windows.Forms.GroupBox()
        Me.btnMovPosterPrev = New System.Windows.Forms.Button()
        Me.btnMovPosterURLorBrowse = New System.Windows.Forms.Button()
        Me.lblMovPosterPages = New System.Windows.Forms.Label()
        Me.btnMovPosterNext = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.tbSelectMoviePoster = New System.Windows.Forms.TextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel26 = New System.Windows.Forms.TableLayoutPanel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.btnChangeMovie = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.WebBrowser2 = New System.Windows.Forms.WebBrowser()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar5 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar2 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar3 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar4 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar6 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar7 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripProgressBar8 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel9 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.bckgroundversion = New System.ComponentModel.BackgroundWorker()
        Me.bckgroundscanepisodes = New System.ComponentModel.BackgroundWorker()
        Me.bckgroundfanart = New System.ComponentModel.BackgroundWorker()
        Me.bckgrounddroppedfiles = New System.ComponentModel.BackgroundWorker()
        Me.bckgroundexit = New System.ComponentModel.BackgroundWorker()
        Me.bckepisodethumb = New System.ComponentModel.BackgroundWorker()
        Me.TabLevel1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage22 = New System.Windows.Forms.TabPage()
        Me.tpMoviesTable = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MovieTableContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MarkAllSelectedAsWatchedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MarkAllSelectedAsUnWatchedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoToSelectedMoviePosterSelectorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoToSelectedMovieFanartSelectorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btn_movTableColumnsSelect = New System.Windows.Forms.Button()
        Me.mov_TableEditDGV = New System.Windows.Forms.DataGridView()
        Me.lbl_movTableMulti = New System.Windows.Forms.Label()
        Me.btn_movTableSave = New System.Windows.Forms.Button()
        Me.lbl_movTableEdit = New System.Windows.Forms.Label()
        Me.tpFanartTv = New System.Windows.Forms.TabPage()
        Me.UcFanartTv1 = New Media_Companion.ucFanartTv()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.SplitContainer8 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieSetRemove = New System.Windows.Forms.Button()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.tbMovSetEntry = New System.Windows.Forms.TextBox()
        Me.btnMovieSetAdd = New System.Windows.Forms.Button()
        Me.dgvmovset = New System.Windows.Forms.DataGridView()
        Me.movsettitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tmdbid = New System.Windows.Forms.DataGridViewImageColumn()
        Me.movsetfanart = New System.Windows.Forms.DataGridViewImageColumn()
        Me.movsetposter = New System.Windows.Forms.DataGridViewImageColumn()
        Me.MovSetsContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiMovSetName = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator31 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiMovSetShowCollection = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiMovSetGetFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiMovSetGetPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.GroupBox40 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.CurrentMovieTags = New System.Windows.Forms.ListBox()
        Me.lblMovTagMulti1 = New System.Windows.Forms.Label()
        Me.lblMovTagMulti2 = New System.Windows.Forms.Label()
        Me.GroupBox39 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.txtbxMovTagEntry = New System.Windows.Forms.TextBox()
        Me.btnMovTagListAdd = New System.Windows.Forms.Button()
        Me.Label186 = New System.Windows.Forms.Label()
        Me.tpMediaStubs = New System.Windows.Forms.TabPage()
        Me.MediaStubs1 = New Media_Companion.MediaStubs()
        Me.TabPage25 = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.SplitContainer7 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label184 = New System.Windows.Forms.Label()
        Me.btnMovieManualPathAdd = New System.Windows.Forms.Button()
        Me.tbMovieManualPath = New System.Windows.Forms.TextBox()
        Me.btn_addmoviefolderdialogue = New System.Windows.Forms.Button()
        Me.btn_removemoviefolder = New System.Windows.Forms.Button()
        Me.clbx_MovieRoots = New System.Windows.Forms.CheckedListBox()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button102 = New System.Windows.Forms.Button()
        Me.Button101 = New System.Windows.Forms.Button()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.ListBox15 = New System.Windows.Forms.ListBox()
        Me.Button108 = New System.Windows.Forms.Button()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.TextBox44 = New System.Windows.Forms.TextBox()
        Me.Button107 = New System.Windows.Forms.Button()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.TabPage26 = New System.Windows.Forms.TabPage()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPageLevel2TVMainBrowser = New System.Windows.Forms.TabPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer10 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox_TotEpisodeCount = New System.Windows.Forms.TextBox()
        Me.TextBox_TotTVShowCount = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.rbTvListContinuing = New System.Windows.Forms.RadioButton()
        Me.rbTvListEnded = New System.Windows.Forms.RadioButton()
        Me.rbTvDisplayUnWatched = New System.Windows.Forms.RadioButton()
        Me.rbTvListAll = New System.Windows.Forms.RadioButton()
        Me.rbTvMissingAiredEp = New System.Windows.Forms.RadioButton()
        Me.rbTvDisplayWatched = New System.Windows.Forms.RadioButton()
        Me.rbTvMissingFanart = New System.Windows.Forms.RadioButton()
        Me.rbTvMissingThumb = New System.Windows.Forms.RadioButton()
        Me.rbTvMissingEpisodes = New System.Windows.Forms.RadioButton()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.pbEpActorImage = New System.Windows.Forms.PictureBox()
        Me.tbEpRole = New System.Windows.Forms.TextBox()
        Me.cmbxEpActor = New System.Windows.Forms.ComboBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.tb_EpPlot = New System.Windows.Forms.TextBox()
        Me.tb_EpFilename = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btn_EpWatched = New System.Windows.Forms.Button()
        Me.tb_EpPath = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.tb_EpCredits = New System.Windows.Forms.TextBox()
        Me.tb_EpAired = New System.Windows.Forms.TextBox()
        Me.tb_EpDirector = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.tb_EpRating = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lb_EpDetails = New System.Windows.Forms.ListBox()
        Me.cbTvSource = New System.Windows.Forms.ComboBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lbl_airepisode = New System.Windows.Forms.Label()
        Me.tb_airepisode = New System.Windows.Forms.TextBox()
        Me.lbl_airbefore = New System.Windows.Forms.Label()
        Me.lbl_airseason = New System.Windows.Forms.Label()
        Me.tb_airseason = New System.Windows.Forms.TextBox()
        Me.pbtvfanarttv = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.tvFanlistbox = New System.Windows.Forms.ListBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Button47 = New System.Windows.Forms.Button()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Button_TV_State = New System.Windows.Forms.Button()
        Me._tv_SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.tv_PictureBoxLeft = New System.Windows.Forms.PictureBox()
        Me.TvEpContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ReScrFanartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelNewFanartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescrapeTvEpThumbToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescrapeTvEpScreenShotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tv_PictureBoxRight = New System.Windows.Forms.PictureBox()
        Me.TvPosterContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsm_TvScrapePoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_TvSelectPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_TvScrapeBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_TvSelectBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.tv_PictureBoxBottom = New System.Windows.Forms.PictureBox()
        Me.tb_ShStudio = New System.Windows.Forms.TextBox()
        Me.tb_ShRating = New System.Windows.Forms.TextBox()
        Me.tb_ShGenre = New System.Windows.Forms.TextBox()
        Me.tb_ShCert = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.tb_ShPlot = New System.Windows.Forms.TextBox()
        Me.tb_ShTvdbId = New System.Windows.Forms.TextBox()
        Me.tb_ShImdbId = New System.Windows.Forms.TextBox()
        Me.tb_ShPremiered = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.tb_ShRunTime = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.gpbxActorSource = New System.Windows.Forms.GroupBox()
        Me.Button46 = New System.Windows.Forms.Button()
        Me.Button45 = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbTvActor = New System.Windows.Forms.ComboBox()
        Me.cbTvActorRole = New System.Windows.Forms.ComboBox()
        Me.lbl_sorttitle = New System.Windows.Forms.Label()
        Me.TextBox_Sorttitle = New System.Windows.Forms.TextBox()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.bnt_TvSeriesStatus = New System.Windows.Forms.Button()
        Me.tpTvScreenshot = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.tv_EpThumbRescrape = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tv_EpThumbScreenShot = New System.Windows.Forms.Button()
        Me.TextBox35 = New System.Windows.Forms.TextBox()
        Me.tpTvFanart = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.TextBox28 = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Button36 = New System.Windows.Forms.Button()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Button35 = New System.Windows.Forms.Button()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Button38 = New System.Windows.Forms.Button()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Button37 = New System.Windows.Forms.Button()
        Me.btnTvFanartResetImage = New System.Windows.Forms.Button()
        Me.btnTvFanartSaveCropped = New System.Windows.Forms.Button()
        Me.btnTvFanartUrl = New System.Windows.Forms.Button()
        Me.btnTvFanartSave = New System.Windows.Forms.Button()
        Me.tpTvPosters = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTvPosterPrev = New System.Windows.Forms.Button()
        Me.btnTvPosterSaveSmall = New System.Windows.Forms.Button()
        Me.btnTvPosterSaveBig = New System.Windows.Forms.Button()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.TextBox31 = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.btnTvPosterNext = New System.Windows.Forms.Button()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.GroupBox23 = New System.Windows.Forms.GroupBox()
        Me.rbTVbanner = New System.Windows.Forms.RadioButton()
        Me.rbTVposter = New System.Windows.Forms.RadioButton()
        Me.btnTvPosterIMDB = New System.Windows.Forms.Button()
        Me.btnTvPosterUrlBrowse = New System.Windows.Forms.Button()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.GroupBox21 = New System.Windows.Forms.GroupBox()
        Me.FrodoImageTrue = New System.Windows.Forms.Label()
        Me.EdenImageTrue = New System.Windows.Forms.Label()
        Me.ArtMode = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.tpTvFanartTv = New System.Windows.Forms.TabPage()
        Me.UcFanartTvTv1 = New Media_Companion.ucFanartTvTv()
        Me.tpTvWall = New System.Windows.Forms.TabPage()
        Me.tpTvSelector = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.cbTvChgShowDLFanartTvArt = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.RadioButton18 = New System.Windows.Forms.RadioButton()
        Me.RadioButton17 = New System.Windows.Forms.RadioButton()
        Me.RadioButton16 = New System.Windows.Forms.RadioButton()
        Me.cbTvChgShowDLImagesLang = New System.Windows.Forms.CheckBox()
        Me.cbTvChgShowOverwriteImgs = New System.Windows.Forms.CheckBox()
        Me.cbTvChgShowDLSeason = New System.Windows.Forms.CheckBox()
        Me.cbTvChgShowDLFanart = New System.Windows.Forms.CheckBox()
        Me.cbTvChgShowDLPoster = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioButton8 = New System.Windows.Forms.RadioButton()
        Me.RadioButton9 = New System.Windows.Forms.RadioButton()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButton10 = New System.Windows.Forms.RadioButton()
        Me.RadioButton11 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioButton12 = New System.Windows.Forms.RadioButton()
        Me.RadioButton13 = New System.Windows.Forms.RadioButton()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.RadioButton14 = New System.Windows.Forms.RadioButton()
        Me.RadioButton15 = New System.Windows.Forms.RadioButton()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.TextBox26 = New System.Windows.Forms.TextBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.btnTvShowSelectorScrape = New System.Windows.Forms.Button()
        Me.Button30 = New System.Windows.Forms.Button()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.tb_TvShSelectSeriesPath = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.tpTvTable = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.STitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SPlot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SPremiered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SRating = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SGenre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SStudio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STVDBId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SIMDBId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SCert = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpTvWeb = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label194 = New System.Windows.Forms.Label()
        Me.WebBrowser4 = New System.Windows.Forms.WebBrowser()
        Me.btn_TvIMDB = New System.Windows.Forms.Button()
        Me.Label195 = New System.Windows.Forms.Label()
        Me.btn_TvTVDb = New System.Windows.Forms.Button()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.btnTvWebStop = New System.Windows.Forms.Button()
        Me.btnTvWebBack = New System.Windows.Forms.Button()
        Me.btnTvWebForward = New System.Windows.Forms.Button()
        Me.btnTvWebRefresh = New System.Windows.Forms.Button()
        Me.tpTvFolders = New System.Windows.Forms.TabPage()
        Me.SplitContainer9 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_TvFoldersRootRemove = New System.Windows.Forms.Button()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.btn_TvFoldersRootAdd = New System.Windows.Forms.Button()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.TextBox39 = New System.Windows.Forms.TextBox()
        Me.btn_TvFoldersRootBrowse = New System.Windows.Forms.Button()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.clbx_TvRootFolders = New System.Windows.Forms.CheckedListBox()
        Me.TvRootFolderContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_tvRtAddSeries = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.bnt_TvChkFolderList = New System.Windows.Forms.Button()
        Me.ListBox6 = New System.Windows.Forms.ListBox()
        Me.btn_TvFoldersAdd = New System.Windows.Forms.Button()
        Me.btn_TvFoldersRemove = New System.Windows.Forms.Button()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.btn_TvFoldersBrowse = New System.Windows.Forms.Button()
        Me.TextBox40 = New System.Windows.Forms.TextBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_TvFoldersUndo = New System.Windows.Forms.Button()
        Me.btn_TvFoldersSave = New System.Windows.Forms.Button()
        Me.TabPage24 = New System.Windows.Forms.TabPage()
        Me.ImageList3 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabMV = New System.Windows.Forms.TabPage()
        Me.UcMusicVideo1 = New Media_Companion.ucMusicVideo()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tp_HmMainBrowser = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel21 = New System.Windows.Forms.TableLayoutPanel()
        Me.pbx_HmPoster = New System.Windows.Forms.PictureBox()
        Me.btnHomeMovieSave = New System.Windows.Forms.Button()
        Me.HmMovPlot = New System.Windows.Forms.TextBox()
        Me.HmMovStars = New System.Windows.Forms.TextBox()
        Me.pbx_HmFanart = New System.Windows.Forms.PictureBox()
        Me.HmMovYear = New System.Windows.Forms.TextBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.HmMovSort = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ListBox18 = New System.Windows.Forms.ListBox()
        Me.HomeMovieContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PlaceHolderforHomeMovieTitleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator()
        Me.PlayHomeMovieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.HmMovTitle = New System.Windows.Forms.TextBox()
        Me.TextBox20 = New System.Windows.Forms.TextBox()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.TextBox23 = New System.Windows.Forms.TextBox()
        Me.Label173 = New System.Windows.Forms.Label()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.TextBox22 = New System.Windows.Forms.TextBox()
        Me.tp_HmScrnSht = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.pbx_HmFanartSht = New System.Windows.Forms.PictureBox()
        Me.btn_HmFanartShot = New System.Windows.Forms.Button()
        Me.tb_HmFanartTime = New System.Windows.Forms.TextBox()
        Me.tp_HmPoster = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.pbx_HmPosterSht = New System.Windows.Forms.PictureBox()
        Me.tb_HmPosterTime = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.btn_HmPosterShot = New System.Windows.Forms.Button()
        Me.tp_HmFolders = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel22 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.btnHomeFoldersRemove = New System.Windows.Forms.Button()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.btnHomeFolderAdd = New System.Windows.Forms.Button()
        Me.btnHomeManualPathAdd = New System.Windows.Forms.Button()
        Me.tbHomeManualPath = New System.Windows.Forms.TextBox()
        Me.btn_HmFolderSaveRefresh = New System.Windows.Forms.Button()
        Me.clbx_HMMovieFolders = New System.Windows.Forms.CheckedListBox()
        Me.tp_HmPref = New System.Windows.Forms.TabPage()
        Me.TabCustTv = New System.Windows.Forms.TabPage()
        Me.Custom_Tv1 = New Media_Companion.Custom_Tv()
        Me.TabPage34 = New System.Windows.Forms.TabPage()
        Me.Button109 = New System.Windows.Forms.Button()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.TextBox45 = New System.Windows.Forms.TextBox()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.TabControlDebug = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxDebugShowTVDBReturnedXML = New System.Windows.Forms.CheckBox()
        Me.Label192 = New System.Windows.Forms.Label()
        Me.CheckBoxDebugShowXML = New System.Windows.Forms.CheckBox()
        Me.ExtraDebugEnable = New System.Windows.Forms.CheckBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.DebugSytemDPITextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox29 = New System.Windows.Forms.GroupBox()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox28 = New System.Windows.Forms.GroupBox()
        Me.RadioButton_Fix_Title = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Fix_Filename = New System.Windows.Forms.RadioButton()
        Me.cbClearMissingFolder = New System.Windows.Forms.CheckBox()
        Me.TabConfigXML = New System.Windows.Forms.TabPage()
        Me.RichTextBoxTabConfigXML = New System.Windows.Forms.RichTextBox()
        Me.TabMovieCacheXML = New System.Windows.Forms.TabPage()
        Me.RichTextBoxTabMovieCache = New System.Windows.Forms.RichTextBox()
        Me.TabTVCacheXML = New System.Windows.Forms.TabPage()
        Me.RichTextBoxTabTVCache = New System.Windows.Forms.RichTextBox()
        Me.TabProfile = New System.Windows.Forms.TabPage()
        Me.RichTextBoxTabProfile = New System.Windows.Forms.RichTextBox()
        Me.TabActorCache = New System.Windows.Forms.TabPage()
        Me.RichTextBoxTabActorCache = New System.Windows.Forms.RichTextBox()
        Me.TabRegex = New System.Windows.Forms.TabPage()
        Me.RichTextBoxTabRegex = New System.Windows.Forms.RichTextBox()
        Me.TabTasks = New System.Windows.Forms.TabPage()
        Me.TasksDontShowCompleted = New System.Windows.Forms.CheckBox()
        Me.TasksTest = New System.Windows.Forms.Button()
        Me.TasksClearCompleted = New System.Windows.Forms.Button()
        Me.TasksRefresh = New System.Windows.Forms.Button()
        Me.TasksDependancies = New System.Windows.Forms.ListBox()
        Me.TasksStateLabel = New System.Windows.Forms.Label()
        Me.TasksSelectedMessage = New System.Windows.Forms.TextBox()
        Me.TasksMessages = New System.Windows.Forms.ListBox()
        Me.TasksArgumentSelector = New System.Windows.Forms.ComboBox()
        Me.TasksArgumentDisplay = New System.Windows.Forms.Panel()
        Me.TasksList = New System.Windows.Forms.ListBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MoviesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchForNewMoviesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatchRescraperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemRebuildMovieCaches = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshMovieNfoFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiMovieSetIdCheck = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReloadMovieCacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportMovieListInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.DownsizeAllFanartsToSelectedSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownsizeAllPostersToSelectedSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TVShowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchForNewEpisodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchALLForNewEpisodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TVShowBrowserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckRootsForToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TV_BatchRescrapeWizardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshShowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_tsmi_CheckDuplicateEpisodes = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReloadShowCacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportTVShowInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.SearchForMissingEpisodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshMissingEpisodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchForNewHomeMoviesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebuildHomeMovieCacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProfilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReloadHtmlTemplatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FixNFOCreateDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmicacheclean = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiCleanCacheOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiCleanSeriesonly = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshGenreListboxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportLibraryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToXBMCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MediaCompanionHelpFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MediaCompanionCodeplexSiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XBMCMCThreadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MediaCompanionForumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiCheckForNewVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_AboutMC = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreferencesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.openFD = New System.Windows.Forms.OpenFileDialog()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.MovieWallContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PlayMovieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditMovieToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFolderToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiWallPlayTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListBox8 = New System.Windows.Forms.ListBox()
        Me.bckgrnd_tvshowscraper = New System.ComponentModel.BackgroundWorker()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.Bckgrndfindmissingepisodes = New System.ComponentModel.BackgroundWorker()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.tvbckrescrapewizard = New System.ComponentModel.BackgroundWorker()
        Me.ForegroundWorkTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TimerToolTip = New System.Windows.Forms.Timer(Me.components)
        Me.ScraperStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsMultiMovieProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsLabelEscCancel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssFileDownload = New System.Windows.Forms.StatusStrip()
        Me.tsFileDownloadlabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsProgressBarFileDownload = New System.Windows.Forms.ToolStripProgressBar()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BasicmovienfoBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BasicmovienfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UcGenPref_XbmcLink1 = New Media_Companion.ucGenPref_XbmcLink()
        Me.TVWallContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiTvWallPosterChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTvWallLargeView = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTvWallOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.btnMovWebStop = New System.Windows.Forms.Button()
        Me.btnMovWebBack = New System.Windows.Forms.Button()
        Me.btnMovWebForward = New System.Windows.Forms.Button()
        Me.btnMovWebRefresh = New System.Windows.Forms.Button()
        Me.btnMovWebTMDb = New System.Windows.Forms.Button()
        Me.btnMovWebIMDb = New System.Windows.Forms.Button()
        Me.TVContextMenu.SuspendLayout
        CType(Me.PbMovieFanArt,System.ComponentModel.ISupportInitialize).BeginInit
        Me.MovieArtworkContextMenu.SuspendLayout
        CType(Me.PbMoviePoster,System.ComponentModel.ISupportInitialize).BeginInit
        Me.MovieContextMenu.SuspendLayout
        Me.TabPageLevel2MovMainBrowser.SuspendLayout
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        CType(Me.SplitContainer5,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer5.Panel1.SuspendLayout
        Me.SplitContainer5.Panel2.SuspendLayout
        Me.SplitContainer5.SuspendLayout
        CType(Me.DataGridViewMovies,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel1.SuspendLayout
        Me.cmsConfigureMovieFilters.SuspendLayout
        CType(Me.ftvArtPicBox,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel6.SuspendLayout
        Me.tlpMovies.SuspendLayout
        Me.TableLayoutPanel3.SuspendLayout
        Me.TableLayoutPanel4.SuspendLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer2.Panel1.SuspendLayout
        Me.SplitContainer2.Panel2.SuspendLayout
        Me.SplitContainer2.SuspendLayout
        Me.TableLayoutPanel2.SuspendLayout
        CType(Me.PictureBoxActor,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tlpMovieButtons.SuspendLayout
        Me.TableLayoutPanel31.SuspendLayout
        Me.TabPageMovieFanart.SuspendLayout
        Me.TableLayoutPanel10.SuspendLayout
        Me.FanartContextMenu.SuspendLayout
        Me.GroupBox1.SuspendLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBoxFanartExtrathumbs.SuspendLayout
        Me.TabPageMoviePoster.SuspendLayout
        Me.panelMoviePosterRHS.SuspendLayout
        Me.gbMoviePoster.SuspendLayout
        CType(Me.PictureBoxAssignedMoviePoster,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbMoviePosterControls.SuspendLayout
        Me.panelMoviePosterLHS.SuspendLayout
        Me.gbMoviePostersAvailable.SuspendLayout
        Me.gbMoviePosterSelection.SuspendLayout
        Me.TabPage4.SuspendLayout
        Me.TableLayoutPanel26.SuspendLayout
        Me.TabPage7.SuspendLayout
        Me.TabPage8.SuspendLayout
        Me.StatusStrip1.SuspendLayout
        Me.TabLevel1.SuspendLayout
        Me.TabPage1.SuspendLayout
        Me.TabControl2.SuspendLayout
        Me.tpMoviesTable.SuspendLayout
        Me.TableLayoutPanel29.SuspendLayout
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.MovieTableContextMenu.SuspendLayout
        CType(Me.mov_TableEditDGV,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpFanartTv.SuspendLayout
        Me.TabPage9.SuspendLayout
        CType(Me.SplitContainer8,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer8.Panel1.SuspendLayout
        Me.SplitContainer8.Panel2.SuspendLayout
        Me.SplitContainer8.SuspendLayout
        Me.TableLayoutPanel11.SuspendLayout
        CType(Me.dgvmovset,System.ComponentModel.ISupportInitialize).BeginInit
        Me.MovSetsContextMenu.SuspendLayout
        Me.TableLayoutPanel14.SuspendLayout
        Me.GroupBox40.SuspendLayout
        Me.TableLayoutPanel12.SuspendLayout
        Me.GroupBox39.SuspendLayout
        Me.TableLayoutPanel13.SuspendLayout
        Me.tpMediaStubs.SuspendLayout
        Me.TabPage25.SuspendLayout
        Me.Panel4.SuspendLayout
        Me.TableLayoutPanel28.SuspendLayout
        Me.Panel5.SuspendLayout
        CType(Me.SplitContainer7,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer7.Panel1.SuspendLayout
        Me.SplitContainer7.Panel2.SuspendLayout
        Me.SplitContainer7.SuspendLayout
        Me.TableLayoutPanel9.SuspendLayout
        Me.TableLayoutPanel8.SuspendLayout
        Me.Panel3.SuspendLayout
        Me.TabPage2.SuspendLayout
        Me.TabControl3.SuspendLayout
        Me.TabPageLevel2TVMainBrowser.SuspendLayout
        CType(Me.SplitContainer3,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer3.Panel1.SuspendLayout
        Me.SplitContainer3.Panel2.SuspendLayout
        Me.SplitContainer3.SuspendLayout
        CType(Me.SplitContainer10,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer10.Panel1.SuspendLayout
        Me.SplitContainer10.Panel2.SuspendLayout
        Me.SplitContainer10.SuspendLayout
        Me.TableLayoutPanel7.SuspendLayout
        Me.Panel11.SuspendLayout
        Me.Panel8.SuspendLayout
        Me.TableLayoutPanel30.SuspendLayout
        CType(Me.pbEpActorImage,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel9.SuspendLayout
        Me.TableLayoutPanel19.SuspendLayout
        CType(Me.pbtvfanarttv,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel20.SuspendLayout
        Me.Panel7.SuspendLayout
        CType(Me._tv_SplitContainer,System.ComponentModel.ISupportInitialize).BeginInit
        Me._tv_SplitContainer.Panel1.SuspendLayout
        Me._tv_SplitContainer.Panel2.SuspendLayout
        Me._tv_SplitContainer.SuspendLayout
        CType(Me.SplitContainer4,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer4.Panel1.SuspendLayout
        Me.SplitContainer4.Panel2.SuspendLayout
        Me.SplitContainer4.SuspendLayout
        CType(Me.tv_PictureBoxLeft,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TvEpContextMenuStrip.SuspendLayout
        CType(Me.tv_PictureBoxRight,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TvPosterContextMenuStrip.SuspendLayout
        CType(Me.tv_PictureBoxBottom,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gpbxActorSource.SuspendLayout
        CType(Me.PictureBox6,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpTvScreenshot.SuspendLayout
        Me.TableLayoutPanel6.SuspendLayout
        CType(Me.PictureBox14,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpTvFanart.SuspendLayout
        Me.TableLayoutPanel18.SuspendLayout
        Me.GroupBox6.SuspendLayout
        Me.Panel12.SuspendLayout
        CType(Me.PictureBox10,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpTvPosters.SuspendLayout
        Me.TableLayoutPanel17.SuspendLayout
        Me.Panel15.SuspendLayout
        CType(Me.PictureBox12,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox23.SuspendLayout
        Me.GroupBox21.SuspendLayout
        Me.tpTvFanartTv.SuspendLayout
        Me.tpTvSelector.SuspendLayout
        Me.TableLayoutPanel16.SuspendLayout
        Me.Panel10.SuspendLayout
        Me.GroupBox7.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.GroupBox5.SuspendLayout
        CType(Me.PictureBox9,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpTvTable.SuspendLayout
        CType(Me.DataGridView2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpTvWeb.SuspendLayout
        Me.TableLayoutPanel15.SuspendLayout
        Me.Panel14.SuspendLayout
        Me.tpTvFolders.SuspendLayout
        CType(Me.SplitContainer9,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer9.Panel1.SuspendLayout
        Me.SplitContainer9.Panel2.SuspendLayout
        Me.SplitContainer9.SuspendLayout
        CType(Me.SplitContainer6,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer6.Panel1.SuspendLayout
        Me.SplitContainer6.Panel2.SuspendLayout
        Me.SplitContainer6.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.TvRootFolderContextMenu.SuspendLayout
        Me.TableLayoutPanel5.SuspendLayout
        Me.TableLayoutPanel25.SuspendLayout
        Me.TabMV.SuspendLayout
        Me.TabPage3.SuspendLayout
        Me.TabControl1.SuspendLayout
        Me.tp_HmMainBrowser.SuspendLayout
        Me.TableLayoutPanel21.SuspendLayout
        CType(Me.pbx_HmPoster,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbx_HmFanart,System.ComponentModel.ISupportInitialize).BeginInit
        Me.HomeMovieContextMenu.SuspendLayout
        Me.tp_HmScrnSht.SuspendLayout
        Me.TableLayoutPanel27.SuspendLayout
        CType(Me.pbx_HmFanartSht,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tp_HmPoster.SuspendLayout
        Me.TableLayoutPanel32.SuspendLayout
        CType(Me.pbx_HmPosterSht,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tp_HmFolders.SuspendLayout
        Me.TableLayoutPanel22.SuspendLayout
        Me.TabCustTv.SuspendLayout
        Me.TabPage34.SuspendLayout
        Me.TabControlDebug.SuspendLayout
        Me.TableLayoutPanel24.SuspendLayout
        Me.GroupBox29.SuspendLayout
        Me.GroupBox28.SuspendLayout
        Me.TabConfigXML.SuspendLayout
        Me.TabMovieCacheXML.SuspendLayout
        Me.TabTVCacheXML.SuspendLayout
        Me.TabProfile.SuspendLayout
        Me.TabActorCache.SuspendLayout
        Me.TabRegex.SuspendLayout
        Me.TabTasks.SuspendLayout
        Me.MenuStrip1.SuspendLayout
        Me.MovieWallContextMenu.SuspendLayout
        Me.ScraperStatusStrip.SuspendLayout
        Me.ssFileDownload.SuspendLayout
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BasicmovienfoBindingSource1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BasicmovienfoBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TVWallContextMenu.SuspendLayout
        Me.TableLayoutPanel23.SuspendLayout
        Me.Panel17.SuspendLayout
        Me.Panel18.SuspendLayout
        Me.SuspendLayout
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 3000
        Me.ToolTip1.AutoPopDelay = 3000
        Me.ToolTip1.InitialDelay = 1000
        Me.ToolTip1.ReshowDelay = 600
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(6, 105)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(41, 20)
        Me.TextBox1.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.TextBox1, "Type here to filter list below for titles matching text")
        Me.TextBox1.Visible = false
        '
        'txt_titlesearch
        '
        Me.txt_titlesearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txt_titlesearch.Location = New System.Drawing.Point(6, 105)
        Me.txt_titlesearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_titlesearch.Name = "txt_titlesearch"
        Me.txt_titlesearch.Size = New System.Drawing.Size(145, 20)
        Me.txt_titlesearch.TabIndex = 54
        Me.ToolTip1.SetToolTip(Me.txt_titlesearch, "Type here to filter list above for titles containing matching text")
        '
        'btnMovieFanartResizeImage
        '
        Me.btnMovieFanartResizeImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.btnMovieFanartResizeImage, 2)
        Me.btnMovieFanartResizeImage.Location = New System.Drawing.Point(872, 239)
        Me.btnMovieFanartResizeImage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovieFanartResizeImage.Name = "btnMovieFanartResizeImage"
        Me.btnMovieFanartResizeImage.Size = New System.Drawing.Size(105, 27)
        Me.btnMovieFanartResizeImage.TabIndex = 127
        Me.btnMovieFanartResizeImage.Text = "Resize"
        Me.ToolTip1.SetToolTip(Me.btnMovieFanartResizeImage, "Resize image according to prefs"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"This may effect the aspect.")
        Me.btnMovieFanartResizeImage.UseVisualStyleBackColor = true
        Me.btnMovieFanartResizeImage.Visible = false
        '
        'cbMoviePosterSaveLoRes
        '
        Me.cbMoviePosterSaveLoRes.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbMoviePosterSaveLoRes.AutoSize = true
        Me.cbMoviePosterSaveLoRes.Enabled = false
        Me.cbMoviePosterSaveLoRes.Location = New System.Drawing.Point(241, 48)
        Me.cbMoviePosterSaveLoRes.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMoviePosterSaveLoRes.Name = "cbMoviePosterSaveLoRes"
        Me.cbMoviePosterSaveLoRes.Size = New System.Drawing.Size(96, 19)
        Me.cbMoviePosterSaveLoRes.TabIndex = 114
        Me.cbMoviePosterSaveLoRes.Text = "Save Lo-Res"
        Me.ToolTip1.SetToolTip(Me.cbMoviePosterSaveLoRes, "The default image saved is from the"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"highest resolution available."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Check this bo"& _ 
        "x to save the lower resolution"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"shown beneath your selected image."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"(HD images a"& _ 
        "re only available on TMDB and IMPA)")
        Me.cbMoviePosterSaveLoRes.UseVisualStyleBackColor = true
        '
        'btn_IMPA_posters
        '
        Me.btn_IMPA_posters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btn_IMPA_posters.Location = New System.Drawing.Point(382, 71)
        Me.btn_IMPA_posters.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_IMPA_posters.Name = "btn_IMPA_posters"
        Me.btn_IMPA_posters.Size = New System.Drawing.Size(99, 29)
        Me.btn_IMPA_posters.TabIndex = 98
        Me.btn_IMPA_posters.Text = "IMP Awards"
        Me.ToolTip1.SetToolTip(Me.btn_IMPA_posters, "Show available Posters from"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"http://www.impawards.com/")
        Me.btn_IMPA_posters.UseVisualStyleBackColor = true
        '
        'btn_IMDB_posters
        '
        Me.btn_IMDB_posters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btn_IMDB_posters.Location = New System.Drawing.Point(267, 71)
        Me.btn_IMDB_posters.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_IMDB_posters.Name = "btn_IMDB_posters"
        Me.btn_IMDB_posters.Size = New System.Drawing.Size(99, 29)
        Me.btn_IMDB_posters.TabIndex = 97
        Me.btn_IMDB_posters.Text = "IMDB"
        Me.ToolTip1.SetToolTip(Me.btn_IMDB_posters, "Show available Posters from"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"http://www.imdb.com/")
        Me.btn_IMDB_posters.UseVisualStyleBackColor = true
        '
        'btn_MPDB_posters
        '
        Me.btn_MPDB_posters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btn_MPDB_posters.Location = New System.Drawing.Point(141, 71)
        Me.btn_MPDB_posters.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_MPDB_posters.Name = "btn_MPDB_posters"
        Me.btn_MPDB_posters.Size = New System.Drawing.Size(105, 29)
        Me.btn_MPDB_posters.TabIndex = 96
        Me.btn_MPDB_posters.Text = "MoviePosterDB"
        Me.ToolTip1.SetToolTip(Me.btn_MPDB_posters, "Show available Posters from"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"http://www.movieposterdb.com/")
        Me.btn_MPDB_posters.UseVisualStyleBackColor = true
        '
        'btn_TMDb_posters
        '
        Me.btn_TMDb_posters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btn_TMDb_posters.Location = New System.Drawing.Point(26, 71)
        Me.btn_TMDb_posters.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TMDb_posters.Name = "btn_TMDb_posters"
        Me.btn_TMDb_posters.Size = New System.Drawing.Size(99, 29)
        Me.btn_TMDb_posters.TabIndex = 95
        Me.btn_TMDb_posters.Text = "TMdB"
        Me.ToolTip1.SetToolTip(Me.btn_TMDb_posters, "Show available Posters from"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"http://themoviedb.org")
        Me.btn_TMDb_posters.UseVisualStyleBackColor = true
        '
        'btnTvFanartResize
        '
        Me.btnTvFanartResize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel18.SetColumnSpan(Me.btnTvFanartResize, 2)
        Me.btnTvFanartResize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvFanartResize.Location = New System.Drawing.Point(899, 441)
        Me.btnTvFanartResize.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvFanartResize.Name = "btnTvFanartResize"
        Me.btnTvFanartResize.Size = New System.Drawing.Size(118, 29)
        Me.btnTvFanartResize.TabIndex = 147
        Me.btnTvFanartResize.Text = "Resize"
        Me.ToolTip1.SetToolTip(Me.btnTvFanartResize, "Resize image according to prefs"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"This may effect the aspect.")
        Me.btnTvFanartResize.UseVisualStyleBackColor = true
        Me.btnTvFanartResize.Visible = false
        '
        'btnTvPosterTVDBAll
        '
        Me.btnTvPosterTVDBAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterTVDBAll.Location = New System.Drawing.Point(312, 25)
        Me.btnTvPosterTVDBAll.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterTVDBAll.Name = "btnTvPosterTVDBAll"
        Me.btnTvPosterTVDBAll.Size = New System.Drawing.Size(100, 29)
        Me.btnTvPosterTVDBAll.TabIndex = 165
        Me.btnTvPosterTVDBAll.Text = "TVDb All"
        Me.ToolTip1.SetToolTip(Me.btnTvPosterTVDBAll, "View All Available Posters"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  And Banners on TVDb")
        Me.btnTvPosterTVDBAll.UseVisualStyleBackColor = true
        '
        'btnTvPosterTVDBSpecific
        '
        Me.btnTvPosterTVDBSpecific.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterTVDBSpecific.Location = New System.Drawing.Point(204, 25)
        Me.btnTvPosterTVDBSpecific.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterTVDBSpecific.Name = "btnTvPosterTVDBSpecific"
        Me.btnTvPosterTVDBSpecific.Size = New System.Drawing.Size(100, 29)
        Me.btnTvPosterTVDBSpecific.TabIndex = 162
        Me.btnTvPosterTVDBSpecific.Text = "TVDb Specific"
        Me.ToolTip1.SetToolTip(Me.btnTvPosterTVDBSpecific, "View Available Posters And Banners"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"on TVDb For the Selected Season")
        Me.btnTvPosterTVDBSpecific.UseVisualStyleBackColor = true
        '
        'btn_TvFoldersAddFromRoot
        '
        Me.btn_TvFoldersAddFromRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_TvFoldersAddFromRoot.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersAddFromRoot.Location = New System.Drawing.Point(11, 529)
        Me.btn_TvFoldersAddFromRoot.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersAddFromRoot.Name = "btn_TvFoldersAddFromRoot"
        Me.btn_TvFoldersAddFromRoot.Size = New System.Drawing.Size(161, 30)
        Me.btn_TvFoldersAddFromRoot.TabIndex = 13
        Me.btn_TvFoldersAddFromRoot.Text = "Add folders from Roots"
        Me.ToolTip1.SetToolTip(Me.btn_TvFoldersAddFromRoot, "This function checks for Shows"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"located within your TV Root folders"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"that have no"& _ 
        "t been added to this list")
        Me.btn_TvFoldersAddFromRoot.UseVisualStyleBackColor = true
        '
        'btn_movTableApply
        '
        Me.btn_movTableApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_movTableApply.Location = New System.Drawing.Point(-24, 35)
        Me.btn_movTableApply.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_movTableApply.Name = "btn_movTableApply"
        Me.btn_movTableApply.Size = New System.Drawing.Size(94, 24)
        Me.btn_movTableApply.TabIndex = 28
        Me.btn_movTableApply.Text = "Apply Edits"
        Me.ToolTip1.SetToolTip(Me.btn_movTableApply, "Copy edits to Selected Rows")
        Me.btn_movTableApply.UseVisualStyleBackColor = true
        '
        'ButtonSaveAndQuickRefresh
        '
        Me.ButtonSaveAndQuickRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonSaveAndQuickRefresh.AutoSize = true
        Me.ButtonSaveAndQuickRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonSaveAndQuickRefresh.Location = New System.Drawing.Point(605, 590)
        Me.ButtonSaveAndQuickRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSaveAndQuickRefresh.Name = "ButtonSaveAndQuickRefresh"
        Me.ButtonSaveAndQuickRefresh.Size = New System.Drawing.Size(101, 25)
        Me.ButtonSaveAndQuickRefresh.TabIndex = 19
        Me.ButtonSaveAndQuickRefresh.Text = "Save && Refresh"
        Me.ToolTip1.SetToolTip(Me.ButtonSaveAndQuickRefresh, "Use this button to update the movie list"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"when folders have been added or removed"& _ 
        "."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Only newly added folders or folders removed"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"will be updated")
        Me.ButtonSaveAndQuickRefresh.UseVisualStyleBackColor = true
        '
        'panelAvailableMoviePosters
        '
        Me.panelAvailableMoviePosters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelAvailableMoviePosters.Location = New System.Drawing.Point(3, 17)
        Me.panelAvailableMoviePosters.Margin = New System.Windows.Forms.Padding(4)
        Me.panelAvailableMoviePosters.Name = "panelAvailableMoviePosters"
        Me.panelAvailableMoviePosters.Size = New System.Drawing.Size(629, 298)
        Me.panelAvailableMoviePosters.TabIndex = 113
        Me.ToolTip1.SetToolTip(Me.panelAvailableMoviePosters, "Double Click a thumbnail to the left to view a large"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"preview Image. For TMDB and"& _ 
        " some IMPA posters"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"this will also display the HD image.")
        '
        'tb_Sh_Ep_Title
        '
        Me.tb_Sh_Ep_Title.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tb_Sh_Ep_Title.BackColor = System.Drawing.SystemColors.Window
        Me.TableLayoutPanel20.SetColumnSpan(Me.tb_Sh_Ep_Title, 4)
        Me.tb_Sh_Ep_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_Sh_Ep_Title.Location = New System.Drawing.Point(9, 4)
        Me.tb_Sh_Ep_Title.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_Sh_Ep_Title.Name = "tb_Sh_Ep_Title"
        Me.tb_Sh_Ep_Title.Size = New System.Drawing.Size(419, 31)
        Me.tb_Sh_Ep_Title.TabIndex = 0
        Me.tb_Sh_Ep_Title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.tb_Sh_Ep_Title, "tt")
        '
        'TvTreeview
        '
        Me.TvTreeview.AllowDrop = true
        Me.TvTreeview.BackColor = System.Drawing.SystemColors.Window
        Me.TableLayoutPanel7.SetColumnSpan(Me.TvTreeview, 9)
        Me.TvTreeview.ContextMenuStrip = Me.TVContextMenu
        Me.TvTreeview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TvTreeview.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TvTreeview.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TvTreeview.HideSelection = false
        Me.TvTreeview.ImageIndex = 4
        Me.TvTreeview.ImageList = Me.ImageList2
        Me.TvTreeview.Location = New System.Drawing.Point(2, 70)
        Me.TvTreeview.Margin = New System.Windows.Forms.Padding(2, 2, 2, 0)
        Me.TvTreeview.MinimumSize = New System.Drawing.Size(290, 4)
        Me.TvTreeview.Name = "TvTreeview"
        Me.TvTreeview.SelectedImageIndex = 4
        Me.TvTreeview.ShowLines = false
        Me.TvTreeview.Size = New System.Drawing.Size(307, 456)
        Me.TvTreeview.StateImageList = Me.ImageList2
        Me.TvTreeview.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.TvTreeview, "Double click an episode to playback,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Use context menu (Right Mouse Click)"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"for a"& _ 
        "dditional options")
        '
        'TVContextMenu
        '
        Me.TVContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Tv_TreeViewContext_ShowTitle, Me.Tv_TreeViewContext_Play_Episode, Me.ToolStripSeparator18, Me.Tv_TreeViewContext_OpenFolder, Me.Tv_TreeViewContext_ViewNfo, Me.tsmiTvDeletenfoart, Me.ToolStripSeparator1, Me.Tv_TreeViewContext_SearchNewEp, Me.Tv_TreeViewContext_RefreshShow, Me.Tv_TreeViewContext_ReloadFromCache, Me.ToolStripSeparator29, Me.Tv_TreeViewContext_RenameEp, Me.Tv_TreeViewContext_RescrapeShowOrEpisode, Me.Tv_TreeViewContext_RescrapeMediaTags, Me.Tv_TreeViewContext_MissingEpThumbs, Me.Tv_TreeViewContext_RescrapeWizard, Me.ToolStripSeparator30, Me.Tv_TreeViewContext_WatchedShowOrEpisode, Me.Tv_TreeViewContext_UnWatchedShowOrEpisode, Me.ToolStripSeparator21, Me.Tv_TreeViewContext_ShowMissEps, Me.Tv_TreeViewContext_DispByAiredDate, Me.ToolStripSeparator22, Me.Tv_TreeViewContext_FindMissArt, Me.ToolStripSeparator2, Me.UnlockAllToolStripMenuItem, Me.LockAllToolStripMenuItem, Me.ToolStripSeparator19, Me.ExpandSelectedShowToolStripMenuItem, Me.CollapseSelectedShowToolStripMenuItem, Me.CollapseAllToolStripMenuItem, Me.ExpandAllToolStripMenuItem})
        Me.TVContextMenu.Name = "ContextMenuStrip2"
        Me.TVContextMenu.Size = New System.Drawing.Size(245, 580)
        Me.TVContextMenu.Text = "Open Folder at file"
        '
        'Tv_TreeViewContext_ShowTitle
        '
        Me.Tv_TreeViewContext_ShowTitle.Name = "Tv_TreeViewContext_ShowTitle"
        Me.Tv_TreeViewContext_ShowTitle.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_ShowTitle.Text = "For Tv Show 'TvShowTitle'..."
        '
        'Tv_TreeViewContext_Play_Episode
        '
        Me.Tv_TreeViewContext_Play_Episode.Name = "Tv_TreeViewContext_Play_Episode"
        Me.Tv_TreeViewContext_Play_Episode.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_Play_Episode.Text = "Play Episode"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(241, 6)
        '
        'Tv_TreeViewContext_OpenFolder
        '
        Me.Tv_TreeViewContext_OpenFolder.Name = "Tv_TreeViewContext_OpenFolder"
        Me.Tv_TreeViewContext_OpenFolder.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_OpenFolder.Text = "Open Folder"
        '
        'Tv_TreeViewContext_ViewNfo
        '
        Me.Tv_TreeViewContext_ViewNfo.Name = "Tv_TreeViewContext_ViewNfo"
        Me.Tv_TreeViewContext_ViewNfo.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_ViewNfo.Text = "View .nfo File"
        '
        'tsmiTvDeletenfoart
        '
        Me.tsmiTvDeletenfoart.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiTvDelShowNfoArt, Me.tsmiTvDelShowEpNfoArt, Me.tsmiTvDelEpNfoArt})
        Me.tsmiTvDeletenfoart.Name = "tsmiTvDeletenfoart"
        Me.tsmiTvDeletenfoart.Size = New System.Drawing.Size(244, 22)
        Me.tsmiTvDeletenfoart.Text = "Delete nfo && Artwork"
        '
        'tsmiTvDelShowNfoArt
        '
        Me.tsmiTvDelShowNfoArt.Name = "tsmiTvDelShowNfoArt"
        Me.tsmiTvDelShowNfoArt.Size = New System.Drawing.Size(170, 22)
        Me.tsmiTvDelShowNfoArt.Text = "Tv Show Only"
        '
        'tsmiTvDelShowEpNfoArt
        '
        Me.tsmiTvDelShowEpNfoArt.Name = "tsmiTvDelShowEpNfoArt"
        Me.tsmiTvDelShowEpNfoArt.Size = New System.Drawing.Size(170, 22)
        Me.tsmiTvDelShowEpNfoArt.Text = "Tv Show && Episodes"
        '
        'tsmiTvDelEpNfoArt
        '
        Me.tsmiTvDelEpNfoArt.Name = "tsmiTvDelEpNfoArt"
        Me.tsmiTvDelEpNfoArt.Size = New System.Drawing.Size(170, 22)
        Me.tsmiTvDelEpNfoArt.Text = "Episodes Only"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(241, 6)
        '
        'Tv_TreeViewContext_SearchNewEp
        '
        Me.Tv_TreeViewContext_SearchNewEp.Name = "Tv_TreeViewContext_SearchNewEp"
        Me.Tv_TreeViewContext_SearchNewEp.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_SearchNewEp.Text = "Search this Show for new episodes"
        '
        'Tv_TreeViewContext_RefreshShow
        '
        Me.Tv_TreeViewContext_RefreshShow.Name = "Tv_TreeViewContext_RefreshShow"
        Me.Tv_TreeViewContext_RefreshShow.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_RefreshShow.Text = "Refresh this Show From .nfo"
        '
        'Tv_TreeViewContext_ReloadFromCache
        '
        Me.Tv_TreeViewContext_ReloadFromCache.Name = "Tv_TreeViewContext_ReloadFromCache"
        Me.Tv_TreeViewContext_ReloadFromCache.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_ReloadFromCache.Text = "Reload Item From Cache"
        '
        'ToolStripSeparator29
        '
        Me.ToolStripSeparator29.Name = "ToolStripSeparator29"
        Me.ToolStripSeparator29.Size = New System.Drawing.Size(241, 6)
        '
        'Tv_TreeViewContext_RenameEp
        '
        Me.Tv_TreeViewContext_RenameEp.Name = "Tv_TreeViewContext_RenameEp"
        Me.Tv_TreeViewContext_RenameEp.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_RenameEp.Text = "Rename Episode(s)"
        '
        'Tv_TreeViewContext_RescrapeShowOrEpisode
        '
        Me.Tv_TreeViewContext_RescrapeShowOrEpisode.Name = "Tv_TreeViewContext_RescrapeShowOrEpisode"
        Me.Tv_TreeViewContext_RescrapeShowOrEpisode.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape This Show"
        '
        'Tv_TreeViewContext_RescrapeMediaTags
        '
        Me.Tv_TreeViewContext_RescrapeMediaTags.Name = "Tv_TreeViewContext_RescrapeMediaTags"
        Me.Tv_TreeViewContext_RescrapeMediaTags.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_RescrapeMediaTags.Text = "Rescrape Media Tags"
        '
        'Tv_TreeViewContext_MissingEpThumbs
        '
        Me.Tv_TreeViewContext_MissingEpThumbs.Name = "Tv_TreeViewContext_MissingEpThumbs"
        Me.Tv_TreeViewContext_MissingEpThumbs.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_MissingEpThumbs.Text = "Rescrape Missing Episode Thumbs"
        '
        'Tv_TreeViewContext_RescrapeWizard
        '
        Me.Tv_TreeViewContext_RescrapeWizard.Name = "Tv_TreeViewContext_RescrapeWizard"
        Me.Tv_TreeViewContext_RescrapeWizard.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_RescrapeWizard.Text = "Use Rescraper Wizard on this Show"
        '
        'ToolStripSeparator30
        '
        Me.ToolStripSeparator30.Name = "ToolStripSeparator30"
        Me.ToolStripSeparator30.Size = New System.Drawing.Size(241, 6)
        '
        'Tv_TreeViewContext_WatchedShowOrEpisode
        '
        Me.Tv_TreeViewContext_WatchedShowOrEpisode.Name = "Tv_TreeViewContext_WatchedShowOrEpisode"
        Me.Tv_TreeViewContext_WatchedShowOrEpisode.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_WatchedShowOrEpisode.Text = "Mark This Show as Watched"
        '
        'Tv_TreeViewContext_UnWatchedShowOrEpisode
        '
        Me.Tv_TreeViewContext_UnWatchedShowOrEpisode.Name = "Tv_TreeViewContext_UnWatchedShowOrEpisode"
        Me.Tv_TreeViewContext_UnWatchedShowOrEpisode.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_UnWatchedShowOrEpisode.Text = "Mark This Show as Un-Watched"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(241, 6)
        '
        'Tv_TreeViewContext_ShowMissEps
        '
        Me.Tv_TreeViewContext_ShowMissEps.Name = "Tv_TreeViewContext_ShowMissEps"
        Me.Tv_TreeViewContext_ShowMissEps.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_ShowMissEps.Text = "Display Missing Episodes"
        '
        'Tv_TreeViewContext_DispByAiredDate
        '
        Me.Tv_TreeViewContext_DispByAiredDate.Name = "Tv_TreeViewContext_DispByAiredDate"
        Me.Tv_TreeViewContext_DispByAiredDate.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_DispByAiredDate.Text = "Display Episodes by Aired Date"
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        Me.ToolStripSeparator22.Size = New System.Drawing.Size(241, 6)
        '
        'Tv_TreeViewContext_FindMissArt
        '
        Me.Tv_TreeViewContext_FindMissArt.Name = "Tv_TreeViewContext_FindMissArt"
        Me.Tv_TreeViewContext_FindMissArt.Size = New System.Drawing.Size(244, 22)
        Me.Tv_TreeViewContext_FindMissArt.Text = "Find Missing Art For This Show"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(241, 6)
        '
        'UnlockAllToolStripMenuItem
        '
        Me.UnlockAllToolStripMenuItem.Name = "UnlockAllToolStripMenuItem"
        Me.UnlockAllToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.UnlockAllToolStripMenuItem.Text = "Unlock All Shows"
        '
        'LockAllToolStripMenuItem
        '
        Me.LockAllToolStripMenuItem.Name = "LockAllToolStripMenuItem"
        Me.LockAllToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.LockAllToolStripMenuItem.Text = "Lock All Shows"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(241, 6)
        '
        'ExpandSelectedShowToolStripMenuItem
        '
        Me.ExpandSelectedShowToolStripMenuItem.Name = "ExpandSelectedShowToolStripMenuItem"
        Me.ExpandSelectedShowToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.ExpandSelectedShowToolStripMenuItem.Text = "Expand Selected Show"
        '
        'CollapseSelectedShowToolStripMenuItem
        '
        Me.CollapseSelectedShowToolStripMenuItem.Name = "CollapseSelectedShowToolStripMenuItem"
        Me.CollapseSelectedShowToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.CollapseSelectedShowToolStripMenuItem.Text = "Collapse Selected Show"
        '
        'CollapseAllToolStripMenuItem
        '
        Me.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem"
        Me.CollapseAllToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.CollapseAllToolStripMenuItem.Text = "Collapse All"
        '
        'ExpandAllToolStripMenuItem
        '
        Me.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem"
        Me.ExpandAllToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.ExpandAllToolStripMenuItem.Text = "Expand All"
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "padlock")
        Me.ImageList2.Images.SetKeyName(1, "imdb-logo")
        Me.ImageList2.Images.SetKeyName(2, "error")
        Me.ImageList2.Images.SetKeyName(3, "qmark")
        Me.ImageList2.Images.SetKeyName(4, "blank")
        Me.ImageList2.Images.SetKeyName(5, "edit")
        Me.ImageList2.Images.SetKeyName(6, "missing.png")
        Me.ImageList2.Images.SetKeyName(7, "watched")
        Me.ImageList2.Images.SetKeyName(8, "lockwatched")
        '
        'btnMovieSetsRepopulateFromUsed
        '
        Me.btnMovieSetsRepopulateFromUsed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovieSetsRepopulateFromUsed.Location = New System.Drawing.Point(218, 394)
        Me.btnMovieSetsRepopulateFromUsed.Name = "btnMovieSetsRepopulateFromUsed"
        Me.btnMovieSetsRepopulateFromUsed.Size = New System.Drawing.Size(145, 29)
        Me.btnMovieSetsRepopulateFromUsed.TabIndex = 11
        Me.btnMovieSetsRepopulateFromUsed.Text = "Repopulate from used"
        Me.ToolTip1.SetToolTip(Me.btnMovieSetsRepopulateFromUsed, "This will repopulate the movie set list with just the used sets")
        Me.btnMovieSetsRepopulateFromUsed.UseVisualStyleBackColor = true
        '
        'btnPrevMissingFanart
        '
        Me.btnPrevMissingFanart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnPrevMissingFanart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPrevMissingFanart.Location = New System.Drawing.Point(718, 240)
        Me.btnPrevMissingFanart.Name = "btnPrevMissingFanart"
        Me.btnPrevMissingFanart.Size = New System.Drawing.Size(56, 27)
        Me.btnPrevMissingFanart.TabIndex = 131
        Me.btnPrevMissingFanart.Text = "< Prev"
        Me.ToolTip1.SetToolTip(Me.btnPrevMissingFanart, "Go to previous movie missing fanart in filtered list")
        Me.btnPrevMissingFanart.UseVisualStyleBackColor = true
        '
        'btnNextMissingFanart
        '
        Me.btnNextMissingFanart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnNextMissingFanart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnNextMissingFanart.Location = New System.Drawing.Point(788, 240)
        Me.btnNextMissingFanart.Name = "btnNextMissingFanart"
        Me.btnNextMissingFanart.Size = New System.Drawing.Size(57, 27)
        Me.btnNextMissingFanart.TabIndex = 129
        Me.btnNextMissingFanart.Text = "Next >"
        Me.ToolTip1.SetToolTip(Me.btnNextMissingFanart, "Go to next movie missing fanart in filtered list")
        Me.btnNextMissingFanart.UseVisualStyleBackColor = true
        '
        'btnPrevMissingPoster
        '
        Me.btnPrevMissingPoster.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPrevMissingPoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPrevMissingPoster.Location = New System.Drawing.Point(115, 13)
        Me.btnPrevMissingPoster.Name = "btnPrevMissingPoster"
        Me.btnPrevMissingPoster.Size = New System.Drawing.Size(57, 27)
        Me.btnPrevMissingPoster.TabIndex = 138
        Me.btnPrevMissingPoster.Text = "< Prev"
        Me.ToolTip1.SetToolTip(Me.btnPrevMissingPoster, "Go to previous movie missing poster in filtered list")
        Me.btnPrevMissingPoster.UseVisualStyleBackColor = true
        '
        'btnNextMissingPoster
        '
        Me.btnNextMissingPoster.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNextMissingPoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnNextMissingPoster.Location = New System.Drawing.Point(178, 12)
        Me.btnNextMissingPoster.Name = "btnNextMissingPoster"
        Me.btnNextMissingPoster.Size = New System.Drawing.Size(57, 27)
        Me.btnNextMissingPoster.TabIndex = 137
        Me.btnNextMissingPoster.Text = "Next >"
        Me.ToolTip1.SetToolTip(Me.btnNextMissingPoster, "Go to next movie missing poster in filtered list")
        Me.btnNextMissingPoster.UseVisualStyleBackColor = true
        '
        'btnMovTagListRefresh
        '
        Me.btnMovTagListRefresh.Location = New System.Drawing.Point(220, 122)
        Me.btnMovTagListRefresh.Name = "btnMovTagListRefresh"
        Me.btnMovTagListRefresh.Size = New System.Drawing.Size(130, 40)
        Me.btnMovTagListRefresh.TabIndex = 28
        Me.btnMovTagListRefresh.Text = "Re-Populate Tags from Movies"
        Me.ToolTip1.SetToolTip(Me.btnMovTagListRefresh, "Selecting this option scans your movie nfo's for"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"tags and add to this list if no"& _ 
        "t already shown."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Does not clear tags not found in nfo's.")
        Me.btnMovTagListRefresh.UseVisualStyleBackColor = true
        '
        'btnMovTagListRemove
        '
        Me.btnMovTagListRemove.Location = New System.Drawing.Point(220, 74)
        Me.btnMovTagListRemove.Name = "btnMovTagListRemove"
        Me.btnMovTagListRemove.Size = New System.Drawing.Size(130, 40)
        Me.btnMovTagListRemove.TabIndex = 27
        Me.btnMovTagListRemove.Text = "Remove Tag(s) from List"
        Me.ToolTip1.SetToolTip(Me.btnMovTagListRemove, "Single or Multiple tag removal from list")
        Me.btnMovTagListRemove.UseVisualStyleBackColor = true
        '
        'TagListBox
        '
        Me.TagListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TagListBox.FormattingEnabled = true
        Me.TagListBox.ItemHeight = 15
        Me.TagListBox.Location = New System.Drawing.Point(3, 37)
        Me.TagListBox.Name = "TagListBox"
        Me.TableLayoutPanel13.SetRowSpan(Me.TagListBox, 4)
        Me.TagListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.TagListBox.Size = New System.Drawing.Size(211, 201)
        Me.TagListBox.Sorted = true
        Me.TagListBox.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.TagListBox, "Add tag(s) to this list from the ""Add Tag"" field to the right")
        '
        'btnMovTagSavetoNfo
        '
        Me.TableLayoutPanel12.SetColumnSpan(Me.btnMovTagSavetoNfo, 2)
        Me.btnMovTagSavetoNfo.Location = New System.Drawing.Point(220, 145)
        Me.btnMovTagSavetoNfo.Name = "btnMovTagSavetoNfo"
        Me.btnMovTagSavetoNfo.Size = New System.Drawing.Size(99, 55)
        Me.btnMovTagSavetoNfo.TabIndex = 26
        Me.btnMovTagSavetoNfo.Text = "Save selected Tag(s) to Movie(s)"
        Me.ToolTip1.SetToolTip(Me.btnMovTagSavetoNfo, "Remove one or multiple tags from Movie")
        Me.btnMovTagSavetoNfo.UseVisualStyleBackColor = true
        '
        'btnMovTagRemove
        '
        Me.TableLayoutPanel12.SetColumnSpan(Me.btnMovTagRemove, 2)
        Me.btnMovTagRemove.Location = New System.Drawing.Point(220, 88)
        Me.btnMovTagRemove.Name = "btnMovTagRemove"
        Me.btnMovTagRemove.Size = New System.Drawing.Size(99, 47)
        Me.btnMovTagRemove.TabIndex = 25
        Me.btnMovTagRemove.Text = "Remove Tag(s) from Movie(s)"
        Me.ToolTip1.SetToolTip(Me.btnMovTagRemove, "Remove one or multiple tags from Movie")
        Me.btnMovTagRemove.UseVisualStyleBackColor = true
        '
        'btnMovTagAdd
        '
        Me.TableLayoutPanel12.SetColumnSpan(Me.btnMovTagAdd, 2)
        Me.btnMovTagAdd.Location = New System.Drawing.Point(220, 31)
        Me.btnMovTagAdd.Name = "btnMovTagAdd"
        Me.btnMovTagAdd.Size = New System.Drawing.Size(99, 47)
        Me.btnMovTagAdd.TabIndex = 24
        Me.btnMovTagAdd.Text = "Add Tag(s) to Movie(s)"
        Me.ToolTip1.SetToolTip(Me.btnMovTagAdd, "Select one or more tags from List Box to be added to Movie(s)")
        Me.btnMovTagAdd.UseVisualStyleBackColor = true
        '
        'roletxt
        '
        Me.roletxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.roletxt.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.roletxt.Location = New System.Drawing.Point(585, 436)
        Me.roletxt.Margin = New System.Windows.Forms.Padding(4, 4, 1, 0)
        Me.roletxt.Name = "roletxt"
        Me.roletxt.ReadOnly = true
        Me.roletxt.Size = New System.Drawing.Size(115, 21)
        Me.roletxt.TabIndex = 215
        Me.ToolTip1.SetToolTip(Me.roletxt, "Character name")
        '
        'cbClearCache
        '
        Me.cbClearCache.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.cbClearCache, 2)
        Me.cbClearCache.Location = New System.Drawing.Point(43, 153)
        Me.cbClearCache.Name = "cbClearCache"
        Me.cbClearCache.Size = New System.Drawing.Size(170, 19)
        Me.cbClearCache.TabIndex = 13
        Me.cbClearCache.Text = "Clear Cache folder on Exit."
        Me.ToolTip1.SetToolTip(Me.cbClearCache, "Selecting this option will clear the cache folder"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"of all files upon exiting Medi"& _ 
        "a Companion."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Note: This is option will be de-selected on restart.")
        Me.cbClearCache.UseVisualStyleBackColor = true
        '
        'btnMovRefreshAll
        '
        Me.btnMovRefreshAll.BackColor = System.Drawing.Color.Transparent
        Me.btnMovRefreshAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMovRefreshAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold)
        Me.btnMovRefreshAll.Image = Global.Media_Companion.My.Resources.Resources.RefreshAll
        Me.btnMovRefreshAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovRefreshAll.Location = New System.Drawing.Point(115, 4)
        Me.btnMovRefreshAll.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovRefreshAll.Name = "btnMovRefreshAll"
        Me.btnMovRefreshAll.Size = New System.Drawing.Size(112, 41)
        Me.btnMovRefreshAll.TabIndex = 184
        Me.btnMovRefreshAll.Text = "Refresh"
        Me.btnMovRefreshAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnMovRefreshAll, "Refresh all movies (or press F5)")
        Me.btnMovRefreshAll.UseVisualStyleBackColor = false
        '
        'btnMovSearchNew
        '
        Me.btnMovSearchNew.BackColor = System.Drawing.Color.Transparent
        Me.btnMovSearchNew.BackgroundImage = Global.Media_Companion.My.Resources.Resources.NewMovies
        Me.btnMovSearchNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMovSearchNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovSearchNew.Location = New System.Drawing.Point(4, 4)
        Me.btnMovSearchNew.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovSearchNew.Name = "btnMovSearchNew"
        Me.btnMovSearchNew.Size = New System.Drawing.Size(105, 41)
        Me.btnMovSearchNew.TabIndex = 179
        Me.btnMovSearchNew.Text = "Search"
        Me.btnMovSearchNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnMovSearchNew, "Search for new movies (or press F3)")
        Me.btnMovSearchNew.UseVisualStyleBackColor = false
        '
        'PbMovieFanArt
        '
        Me.PbMovieFanArt.BackColor = System.Drawing.SystemColors.ControlLight
        Me.PbMovieFanArt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PbMovieFanArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PbMovieFanArt.ContextMenuStrip = Me.MovieArtworkContextMenu
        Me.PbMovieFanArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PbMovieFanArt.Location = New System.Drawing.Point(0, 0)
        Me.PbMovieFanArt.Margin = New System.Windows.Forms.Padding(0)
        Me.PbMovieFanArt.Name = "PbMovieFanArt"
        Me.PbMovieFanArt.Size = New System.Drawing.Size(296, 262)
        Me.PbMovieFanArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PbMovieFanArt.TabIndex = 127
        Me.PbMovieFanArt.TabStop = false
        Me.ToolTip1.SetToolTip(Me.PbMovieFanArt, "Double Click for larger view")
        '
        'MovieArtworkContextMenu
        '
        Me.MovieArtworkContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RescrapeFanartToolStripMenuItem, Me.DownloadFanartToolStripMenuItem, Me.RescrapePosterFromTMDBToolStripMenuItem, Me.PeToolStripMenuItem, Me.RescrapePToolStripMenuItem, Me.RescraToolStripMenuItem, Me.DownloadPosterFromTMDBToolStripMenuItem, Me.DownloadPosterFromIMDBToolStripMenuItem, Me.DownloadPosterToolStripMenuItem, Me.DownloadPosterFromMPDBToolStripMenuItem})
        Me.MovieArtworkContextMenu.Name = "ContextMenuStrip4"
        Me.MovieArtworkContextMenu.Size = New System.Drawing.Size(213, 224)
        '
        'RescrapeFanartToolStripMenuItem
        '
        Me.RescrapeFanartToolStripMenuItem.Name = "RescrapeFanartToolStripMenuItem"
        Me.RescrapeFanartToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RescrapeFanartToolStripMenuItem.Text = "Rescrape Fanart"
        '
        'DownloadFanartToolStripMenuItem
        '
        Me.DownloadFanartToolStripMenuItem.Name = "DownloadFanartToolStripMenuItem"
        Me.DownloadFanartToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DownloadFanartToolStripMenuItem.Text = "Download Fanart"
        '
        'RescrapePosterFromTMDBToolStripMenuItem
        '
        Me.RescrapePosterFromTMDBToolStripMenuItem.Name = "RescrapePosterFromTMDBToolStripMenuItem"
        Me.RescrapePosterFromTMDBToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RescrapePosterFromTMDBToolStripMenuItem.Text = "Rescrape Poster From TMDB"
        '
        'PeToolStripMenuItem
        '
        Me.PeToolStripMenuItem.Name = "PeToolStripMenuItem"
        Me.PeToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.PeToolStripMenuItem.Text = "Rescrape Poster From IMDB"
        '
        'RescrapePToolStripMenuItem
        '
        Me.RescrapePToolStripMenuItem.Name = "RescrapePToolStripMenuItem"
        Me.RescrapePToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RescrapePToolStripMenuItem.Text = "Rescrape Poster From IMPA"
        '
        'RescraToolStripMenuItem
        '
        Me.RescraToolStripMenuItem.Name = "RescraToolStripMenuItem"
        Me.RescraToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RescraToolStripMenuItem.Text = "Rescrape Poster From MPDB"
        '
        'DownloadPosterFromTMDBToolStripMenuItem
        '
        Me.DownloadPosterFromTMDBToolStripMenuItem.Name = "DownloadPosterFromTMDBToolStripMenuItem"
        Me.DownloadPosterFromTMDBToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DownloadPosterFromTMDBToolStripMenuItem.Text = "Download Poster From TMDB"
        '
        'DownloadPosterFromIMDBToolStripMenuItem
        '
        Me.DownloadPosterFromIMDBToolStripMenuItem.Name = "DownloadPosterFromIMDBToolStripMenuItem"
        Me.DownloadPosterFromIMDBToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DownloadPosterFromIMDBToolStripMenuItem.Text = "Download Poster From IMDB"
        '
        'DownloadPosterToolStripMenuItem
        '
        Me.DownloadPosterToolStripMenuItem.Name = "DownloadPosterToolStripMenuItem"
        Me.DownloadPosterToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DownloadPosterToolStripMenuItem.Text = "Download Poster from IMPA"
        '
        'DownloadPosterFromMPDBToolStripMenuItem
        '
        Me.DownloadPosterFromMPDBToolStripMenuItem.Name = "DownloadPosterFromMPDBToolStripMenuItem"
        Me.DownloadPosterFromMPDBToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DownloadPosterFromMPDBToolStripMenuItem.Text = "Download Poster From MPDB"
        '
        'PbMoviePoster
        '
        Me.PbMoviePoster.BackColor = System.Drawing.SystemColors.ControlLight
        Me.PbMoviePoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PbMoviePoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PbMoviePoster.ContextMenuStrip = Me.MovieArtworkContextMenu
        Me.PbMoviePoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PbMoviePoster.Location = New System.Drawing.Point(0, 0)
        Me.PbMoviePoster.Margin = New System.Windows.Forms.Padding(0)
        Me.PbMoviePoster.Name = "PbMoviePoster"
        Me.PbMoviePoster.Size = New System.Drawing.Size(329, 262)
        Me.PbMoviePoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PbMoviePoster.TabIndex = 86
        Me.PbMoviePoster.TabStop = false
        Me.ToolTip1.SetToolTip(Me.PbMoviePoster, "Double Click for larger view")
        '
        'btnMovSave
        '
        Me.btnMovSave.BackColor = System.Drawing.Color.Transparent
        Me.btnMovSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMovSave.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMovSave.Image = Global.Media_Companion.My.Resources.Resources.Save
        Me.btnMovSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMovSave.Location = New System.Drawing.Point(4, 4)
        Me.btnMovSave.Margin = New System.Windows.Forms.Padding(4, 4, 9, 4)
        Me.btnMovSave.Name = "btnMovSave"
        Me.btnMovSave.Size = New System.Drawing.Size(62, 55)
        Me.btnMovSave.TabIndex = 157
        Me.btnMovSave.Text = "Save"
        Me.btnMovSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnMovSave, "Quick Save edits made in the panel below")
        Me.btnMovSave.UseVisualStyleBackColor = false
        '
        'btnMovRescrape
        '
        Me.btnMovRescrape.BackColor = System.Drawing.Color.Transparent
        Me.btnMovRescrape.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMovRescrape.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMovRescrape.Image = Global.Media_Companion.My.Resources.Resources.Clap
        Me.btnMovRescrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMovRescrape.Location = New System.Drawing.Point(4, 67)
        Me.btnMovRescrape.Margin = New System.Windows.Forms.Padding(4, 4, 9, 4)
        Me.btnMovRescrape.Name = "btnMovRescrape"
        Me.btnMovRescrape.Size = New System.Drawing.Size(62, 55)
        Me.btnMovRescrape.TabIndex = 180
        Me.btnMovRescrape.Text = "Rescrape"
        Me.btnMovRescrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnMovRescrape, "Rescrape movie(s)")
        Me.btnMovRescrape.UseVisualStyleBackColor = true
        '
        'btn_TMDBSearch
        '
        Me.btn_TMDBSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_TMDBSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TMDBSearch.Image = Global.Media_Companion.My.Resources.Resources.TMDB_Icon
        Me.btn_TMDBSearch.Location = New System.Drawing.Point(484, 347)
        Me.btn_TMDBSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TMDBSearch.Name = "btn_TMDBSearch"
        Me.btn_TMDBSearch.Size = New System.Drawing.Size(100, 45)
        Me.btn_TMDBSearch.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.btn_TMDBSearch, "Select to search themoviedb.org for"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"correct movie."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"NB:  Uses XBMC TMDB scraper."& _ 
        "")
        Me.btn_TMDBSearch.UseVisualStyleBackColor = true
        '
        'btn_IMDBSearch
        '
        Me.btn_IMDBSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_IMDBSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_IMDBSearch.Image = Global.Media_Companion.My.Resources.Resources.imdb1
        Me.btn_IMDBSearch.Location = New System.Drawing.Point(367, 347)
        Me.btn_IMDBSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_IMDBSearch.Name = "btn_IMDBSearch"
        Me.btn_IMDBSearch.Size = New System.Drawing.Size(100, 45)
        Me.btn_IMDBSearch.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.btn_IMDBSearch, "Select to search IMDB.com for"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"correct movie."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"NB:  Uses MC's Movie scraper.")
        Me.btn_IMDBSearch.UseVisualStyleBackColor = true
        '
        'btnTvRefreshAll
        '
        Me.btnTvRefreshAll.BackColor = System.Drawing.Color.Transparent
        Me.btnTvRefreshAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TableLayoutPanel7.SetColumnSpan(Me.btnTvRefreshAll, 5)
        Me.btnTvRefreshAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold)
        Me.btnTvRefreshAll.Image = Global.Media_Companion.My.Resources.Resources.RefreshAll
        Me.btnTvRefreshAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTvRefreshAll.Location = New System.Drawing.Point(174, 0)
        Me.btnTvRefreshAll.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnTvRefreshAll.Name = "btnTvRefreshAll"
        Me.btnTvRefreshAll.Size = New System.Drawing.Size(133, 41)
        Me.btnTvRefreshAll.TabIndex = 185
        Me.btnTvRefreshAll.Text = "Refresh All"
        Me.btnTvRefreshAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnTvRefreshAll, "Rebuild all Shows, checked all episodes."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"This does not re-scrape data, only chec"& _ 
        "ks"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"for existing nfo's to populate the cache.")
        Me.btnTvRefreshAll.UseVisualStyleBackColor = false
        '
        'btnTvSearchNew
        '
        Me.btnTvSearchNew.BackColor = System.Drawing.Color.Transparent
        Me.btnTvSearchNew.BackgroundImage = Global.Media_Companion.My.Resources.Resources.NewMovies
        Me.btnTvSearchNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TableLayoutPanel7.SetColumnSpan(Me.btnTvSearchNew, 4)
        Me.btnTvSearchNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvSearchNew.Location = New System.Drawing.Point(4, 0)
        Me.btnTvSearchNew.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnTvSearchNew.Name = "btnTvSearchNew"
        Me.btnTvSearchNew.Size = New System.Drawing.Size(161, 41)
        Me.btnTvSearchNew.TabIndex = 180
        Me.btnTvSearchNew.Text = "Search New"
        Me.btnTvSearchNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnTvSearchNew, "Search all Shows for new Episodes")
        Me.btnTvSearchNew.UseVisualStyleBackColor = false
        '
        'Button44
        '
        Me.Button44.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button44.BackgroundImage = CType(resources.GetObject("Button44.BackgroundImage"),System.Drawing.Image)
        Me.Button44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button44.Location = New System.Drawing.Point(446, 4)
        Me.Button44.Margin = New System.Windows.Forms.Padding(4)
        Me.Button44.Name = "Button44"
        Me.Button44.Size = New System.Drawing.Size(30, 30)
        Me.Button44.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.Button44, "Rescrape Selected Item")
        Me.Button44.UseVisualStyleBackColor = true
        '
        'Button_Save_TvShow_Episode
        '
        Me.Button_Save_TvShow_Episode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button_Save_TvShow_Episode.BackgroundImage = CType(resources.GetObject("Button_Save_TvShow_Episode.BackgroundImage"),System.Drawing.Image)
        Me.Button_Save_TvShow_Episode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button_Save_TvShow_Episode.Location = New System.Drawing.Point(493, 4)
        Me.Button_Save_TvShow_Episode.Margin = New System.Windows.Forms.Padding(4)
        Me.Button_Save_TvShow_Episode.Name = "Button_Save_TvShow_Episode"
        Me.Button_Save_TvShow_Episode.Size = New System.Drawing.Size(32, 30)
        Me.Button_Save_TvShow_Episode.TabIndex = 20
        Me.Button_Save_TvShow_Episode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.Button_Save_TvShow_Episode, "Save any edits made below")
        Me.Button_Save_TvShow_Episode.UseVisualStyleBackColor = true
        '
        'rbTvMissingPoster
        '
        Me.rbTvMissingPoster.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvMissingPoster.AutoSize = true
        Me.rbTvMissingPoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvMissingPoster.Location = New System.Drawing.Point(179, 65)
        Me.rbTvMissingPoster.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvMissingPoster.Name = "rbTvMissingPoster"
        Me.rbTvMissingPoster.Size = New System.Drawing.Size(112, 19)
        Me.rbTvMissingPoster.TabIndex = 7
        Me.rbTvMissingPoster.Text = "Missing Posters"
        Me.ToolTip1.SetToolTip(Me.rbTvMissingPoster, "Posters & Banners")
        Me.rbTvMissingPoster.UseVisualStyleBackColor = true
        '
        'btnMovFanartToggle
        '
        Me.btnMovFanartToggle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMovFanartToggle.BackColor = System.Drawing.Color.Lime
        Me.TableLayoutPanel10.SetColumnSpan(Me.btnMovFanartToggle, 2)
        Me.btnMovFanartToggle.Location = New System.Drawing.Point(872, 182)
        Me.btnMovFanartToggle.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovFanartToggle.Name = "btnMovFanartToggle"
        Me.TableLayoutPanel10.SetRowSpan(Me.btnMovFanartToggle, 2)
        Me.btnMovFanartToggle.Size = New System.Drawing.Size(105, 49)
        Me.btnMovFanartToggle.TabIndex = 135
        Me.btnMovFanartToggle.Text = "Show MovieSet Fanart"
        Me.ToolTip1.SetToolTip(Me.btnMovFanartToggle, "Toggle displaying Movie Fanart or"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Movie Set Fanart")
        Me.btnMovFanartToggle.UseVisualStyleBackColor = false
        Me.btnMovFanartToggle.Visible = false
        '
        'btnMovPosterToggle
        '
        Me.btnMovPosterToggle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovPosterToggle.BackColor = System.Drawing.Color.Lime
        Me.btnMovPosterToggle.Location = New System.Drawing.Point(133, 17)
        Me.btnMovPosterToggle.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovPosterToggle.Name = "btnMovPosterToggle"
        Me.btnMovPosterToggle.Size = New System.Drawing.Size(101, 46)
        Me.btnMovPosterToggle.TabIndex = 142
        Me.btnMovPosterToggle.Text = "Show MovieSet Poster"
        Me.ToolTip1.SetToolTip(Me.btnMovPosterToggle, "Toggle displaying Movie Fanart or"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Movie Set Fanart")
        Me.btnMovPosterToggle.UseVisualStyleBackColor = false
        Me.btnMovPosterToggle.Visible = false
        '
        'btn_HMSearch
        '
        Me.btn_HMSearch.BackColor = System.Drawing.Color.Transparent
        Me.btn_HMSearch.BackgroundImage = Global.Media_Companion.My.Resources.Resources.NewMovies
        Me.btn_HMSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btn_HMSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_HMSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_HMSearch.Location = New System.Drawing.Point(10, 14)
        Me.btn_HMSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_HMSearch.Name = "btn_HMSearch"
        Me.btn_HMSearch.Size = New System.Drawing.Size(141, 37)
        Me.btn_HMSearch.TabIndex = 180
        Me.btn_HMSearch.Text = "Search New"
        Me.btn_HMSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btn_HMSearch, "Search for new movies (or press F3)")
        Me.btn_HMSearch.UseVisualStyleBackColor = false
        '
        'btn_HMRefresh
        '
        Me.btn_HMRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btn_HMRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btn_HMRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_HMRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold)
        Me.btn_HMRefresh.Image = Global.Media_Companion.My.Resources.Resources.RefreshAll
        Me.btn_HMRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_HMRefresh.Location = New System.Drawing.Point(176, 14)
        Me.btn_HMRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_HMRefresh.Name = "btn_HMRefresh"
        Me.btn_HMRefresh.Size = New System.Drawing.Size(123, 37)
        Me.btn_HMRefresh.TabIndex = 185
        Me.btn_HMRefresh.Text = "Refresh"
        Me.btn_HMRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btn_HMRefresh, "Refresh all movies (or press F5)")
        Me.btn_HMRefresh.UseVisualStyleBackColor = false
        '
        'rbTvListUnKnown
        '
        Me.rbTvListUnKnown.AutoSize = true
        Me.rbTvListUnKnown.Location = New System.Drawing.Point(228, 4)
        Me.rbTvListUnKnown.Name = "rbTvListUnKnown"
        Me.rbTvListUnKnown.Size = New System.Drawing.Size(79, 19)
        Me.rbTvListUnKnown.TabIndex = 15
        Me.rbTvListUnKnown.TabStop = true
        Me.rbTvListUnKnown.Text = "UnKnown"
        Me.ToolTip1.SetToolTip(Me.rbTvListUnKnown, "List Unknown Series Status")
        Me.rbTvListUnKnown.UseVisualStyleBackColor = true
        '
        'btn_MovFanartScrnSht
        '
        Me.btn_MovFanartScrnSht.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.btn_MovFanartScrnSht, 2)
        Me.btn_MovFanartScrnSht.Location = New System.Drawing.Point(460, 169)
        Me.btn_MovFanartScrnSht.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_MovFanartScrnSht.Name = "btn_MovFanartScrnSht"
        Me.btn_MovFanartScrnSht.Size = New System.Drawing.Size(116, 27)
        Me.btn_MovFanartScrnSht.TabIndex = 136
        Me.btn_MovFanartScrnSht.Text = "Screenshot at..."
        Me.ToolTip1.SetToolTip(Me.btn_MovFanartScrnSht, "Create Screenshot for Fanart."&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Helpful if a Movies doesn't have"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"fanart to downlo"& _ 
        "ad.")
        Me.btn_MovFanartScrnSht.UseVisualStyleBackColor = true
        Me.btn_MovFanartScrnSht.Visible = false
        '
        'tagtxt
        '
        Me.tagtxt.BackColor = System.Drawing.SystemColors.Control
        Me.tlpMovies.SetColumnSpan(Me.tagtxt, 4)
        Me.tagtxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tagtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tagtxt.Location = New System.Drawing.Point(369, 565)
        Me.tagtxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 4)
        Me.tagtxt.Name = "tagtxt"
        Me.tagtxt.ReadOnly = true
        Me.tagtxt.Size = New System.Drawing.Size(211, 20)
        Me.tagtxt.TabIndex = 228
        Me.ToolTip2.SetToolTip(Me.tagtxt, "Tags associated with the selected movie.")
        '
        'MovieContextMenu
        '
        Me.MovieContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mov_ToolStripMovieName, Me.mov_ToolStripPlayMovie, Me.mov_ToolStripPlayTrailer, Me.ToolStripSeparator17, Me.mov_ToolStripOpenFolder, Me.mov_ToolStripViewNfo, Me.ToolStripSeparator27, Me.mov_ToolStripDeleteNfoArtwork, Me.ToolStripSeparator4, Me.mov_ToolStripReloadFromCache, Me.Mov_ToolStripRemoveMovie, Me.Mov_ToolStripRenameMovie, Me.ToolStripSeparator5, Me.mov_ToolStripRescrapeAll, Me.mov_ToolStripRescrapeSpecific, Me.ToolStripSeparator28, Me.tsmiSetWatched, Me.tsmiClearWatched, Me.ToolStripSeparator23, Me.mov_ToolStripFanartBrowserAlt, Me.mov_ToolStripPosterBrowserAlt, Me.mov_ToolStripEditMovieAlt, Me.ToolStripSeparator24, Me.mov_ToolStripExportMovies, Me.tsmiOpenInMkvmergeGUI, Me.tsmiSyncToXBMC, Me.tsmiConvertToFrodo})
        Me.MovieContextMenu.Name = "ContextMenuStrip1"
        Me.MovieContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MovieContextMenu.Size = New System.Drawing.Size(242, 486)
        Me.MovieContextMenu.Text = "whatever"
        '
        'mov_ToolStripMovieName
        '
        Me.mov_ToolStripMovieName.Name = "mov_ToolStripMovieName"
        Me.mov_ToolStripMovieName.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripMovieName.Text = "Placeholder for Movie Name"
        '
        'mov_ToolStripPlayMovie
        '
        Me.mov_ToolStripPlayMovie.Name = "mov_ToolStripPlayMovie"
        Me.mov_ToolStripPlayMovie.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripPlayMovie.Text = "Play Movie"
        '
        'mov_ToolStripPlayTrailer
        '
        Me.mov_ToolStripPlayTrailer.Name = "mov_ToolStripPlayTrailer"
        Me.mov_ToolStripPlayTrailer.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripPlayTrailer.Text = "Play Trailer"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(238, 6)
        '
        'mov_ToolStripOpenFolder
        '
        Me.mov_ToolStripOpenFolder.Name = "mov_ToolStripOpenFolder"
        Me.mov_ToolStripOpenFolder.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripOpenFolder.Text = "Open Folder"
        '
        'mov_ToolStripViewNfo
        '
        Me.mov_ToolStripViewNfo.Name = "mov_ToolStripViewNfo"
        Me.mov_ToolStripViewNfo.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripViewNfo.Text = "View .nfo File"
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(238, 6)
        '
        'mov_ToolStripDeleteNfoArtwork
        '
        Me.mov_ToolStripDeleteNfoArtwork.Name = "mov_ToolStripDeleteNfoArtwork"
        Me.mov_ToolStripDeleteNfoArtwork.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripDeleteNfoArtwork.Text = "Delete movie(s) Nfo and Artwork"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(238, 6)
        '
        'mov_ToolStripReloadFromCache
        '
        Me.mov_ToolStripReloadFromCache.Name = "mov_ToolStripReloadFromCache"
        Me.mov_ToolStripReloadFromCache.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripReloadFromCache.Text = "Reload From Cache"
        '
        'Mov_ToolStripRemoveMovie
        '
        Me.Mov_ToolStripRemoveMovie.Name = "Mov_ToolStripRemoveMovie"
        Me.Mov_ToolStripRemoveMovie.Size = New System.Drawing.Size(241, 22)
        Me.Mov_ToolStripRemoveMovie.Text = "Remove selected movie(s) from list"
        '
        'Mov_ToolStripRenameMovie
        '
        Me.Mov_ToolStripRenameMovie.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RenMovieAndFolder, Me.tsmi_RenMovieOnly, Me.tsmi_RenMovFolderOnly})
        Me.Mov_ToolStripRenameMovie.Name = "Mov_ToolStripRenameMovie"
        Me.Mov_ToolStripRenameMovie.Size = New System.Drawing.Size(241, 22)
        Me.Mov_ToolStripRenameMovie.Text = "Rename selected movie(s) in list"
        Me.Mov_ToolStripRenameMovie.ToolTipText = resources.GetString("Mov_ToolStripRenameMovie.ToolTipText")
        '
        'tsmi_RenMovieAndFolder
        '
        Me.tsmi_RenMovieAndFolder.Name = "tsmi_RenMovieAndFolder"
        Me.tsmi_RenMovieAndFolder.Size = New System.Drawing.Size(156, 22)
        Me.tsmi_RenMovieAndFolder.Text = "Movie and Folder"
        '
        'tsmi_RenMovieOnly
        '
        Me.tsmi_RenMovieOnly.Name = "tsmi_RenMovieOnly"
        Me.tsmi_RenMovieOnly.Size = New System.Drawing.Size(156, 22)
        Me.tsmi_RenMovieOnly.Text = "Movie Only"
        '
        'tsmi_RenMovFolderOnly
        '
        Me.tsmi_RenMovFolderOnly.Name = "tsmi_RenMovFolderOnly"
        Me.tsmi_RenMovFolderOnly.Size = New System.Drawing.Size(156, 22)
        Me.tsmi_RenMovFolderOnly.Text = "Folder Only"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(238, 6)
        '
        'mov_ToolStripRescrapeAll
        '
        Me.mov_ToolStripRescrapeAll.Name = "mov_ToolStripRescrapeAll"
        Me.mov_ToolStripRescrapeAll.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripRescrapeAll.Text = "Rescrape All"
        '
        'mov_ToolStripRescrapeSpecific
        '
        Me.mov_ToolStripRescrapeSpecific.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem15, Me.ToolStripMenuItem8, Me.tsmiRescrapeCountry, Me.ToolStripMenuItem7, Me.ToolStripMenuItem6, Me.ToolStripMenuItem9, Me.ToolStripMenuItem10, Me.ToolStripMenuItem4, Me.tsmiRescrapePremiered, Me.ToolStripMenuItem19, Me.ToolStripMenuItem11, Me.ToolStripMenuItem21, Me.ToolStripMenuItem14, Me.ToolStripMenuItem5, Me.ToolStripMenuItem3, Me.tsmiTMDbSetName, Me.tsmiRescrapeTop250, Me.ToolStripMenuItem1, Me.ToolStripMenuItem20, Me.YearToolStripMenuItem, Me.ToolStripSeparator11, Me.tsmiRescrapePosterUrls, Me.tsmiRescrapeFrodo_Poster_Thumbs, Me.tsmiRescrapeFrodo_Fanart_Thumbs, Me.ToolStripSeparator6, Me.tsmiRescrapeKeyWords, Me.ToolStripMenuItem16, Me.ToolStripMenuItem17, Me.tsmiRescrapeFanartTv, Me.tsmiRescrapeMovieSetArt, Me.ToolStripMenuItem18, Me.tsmiDlTrailer, Me.ToolStripSeparator7, Me.RenameFilesToolStripMenuItem})
        Me.mov_ToolStripRescrapeSpecific.Name = "mov_ToolStripRescrapeSpecific"
        Me.mov_ToolStripRescrapeSpecific.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripRescrapeSpecific.Text = "Rescrape Specific"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem15.Text = "Actors"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem8.Text = "Cert"
        '
        'tsmiRescrapeCountry
        '
        Me.tsmiRescrapeCountry.Name = "tsmiRescrapeCountry"
        Me.tsmiRescrapeCountry.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeCountry.Text = "Country"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem7.Text = "Credits"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem6.Text = "Director"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem9.Text = "Genre"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem10.Text = "Outline"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem4.Text = "Plot"
        '
        'tsmiRescrapePremiered
        '
        Me.tsmiRescrapePremiered.Name = "tsmiRescrapePremiered"
        Me.tsmiRescrapePremiered.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapePremiered.Text = "Premiered"
        '
        'ToolStripMenuItem19
        '
        Me.ToolStripMenuItem19.Name = "ToolStripMenuItem19"
        Me.ToolStripMenuItem19.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem19.Text = "Rating"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem12, Me.ToolStripMenuItem13})
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem11.Text = "Runtime"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(126, 22)
        Me.ToolStripMenuItem12.Text = "From IMDB"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(126, 22)
        Me.ToolStripMenuItem13.Text = "From File"
        '
        'ToolStripMenuItem21
        '
        Me.ToolStripMenuItem21.Name = "ToolStripMenuItem21"
        Me.ToolStripMenuItem21.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem21.Text = "Stars"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem14.Text = "Studio"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem5.Text = "Tagline"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem3.Text = "Title"
        '
        'tsmiTMDbSetName
        '
        Me.tsmiTMDbSetName.Name = "tsmiTMDbSetName"
        Me.tsmiTMDbSetName.Size = New System.Drawing.Size(188, 22)
        Me.tsmiTMDbSetName.Text = "TMDb Set Name"
        '
        'tsmiRescrapeTop250
        '
        Me.tsmiRescrapeTop250.Name = "tsmiRescrapeTop250"
        Me.tsmiRescrapeTop250.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeTop250.Text = "Top250"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem1.Text = "Trailer"
        '
        'ToolStripMenuItem20
        '
        Me.ToolStripMenuItem20.Name = "ToolStripMenuItem20"
        Me.ToolStripMenuItem20.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem20.Text = "Votes"
        '
        'YearToolStripMenuItem
        '
        Me.YearToolStripMenuItem.Name = "YearToolStripMenuItem"
        Me.YearToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.YearToolStripMenuItem.Text = "Year"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(185, 6)
        '
        'tsmiRescrapePosterUrls
        '
        Me.tsmiRescrapePosterUrls.Name = "tsmiRescrapePosterUrls"
        Me.tsmiRescrapePosterUrls.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapePosterUrls.Text = "Poster Urls"
        '
        'tsmiRescrapeFrodo_Poster_Thumbs
        '
        Me.tsmiRescrapeFrodo_Poster_Thumbs.Name = "tsmiRescrapeFrodo_Poster_Thumbs"
        Me.tsmiRescrapeFrodo_Poster_Thumbs.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeFrodo_Poster_Thumbs.Text = "Frodo Poster Thumbs"
        Me.tsmiRescrapeFrodo_Poster_Thumbs.ToolTipText = "Frodo support must be enabled before this option can be selected"
        '
        'tsmiRescrapeFrodo_Fanart_Thumbs
        '
        Me.tsmiRescrapeFrodo_Fanart_Thumbs.Name = "tsmiRescrapeFrodo_Fanart_Thumbs"
        Me.tsmiRescrapeFrodo_Fanart_Thumbs.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeFrodo_Fanart_Thumbs.Text = "Frodo Fanart Thumbs"
        Me.tsmiRescrapeFrodo_Fanart_Thumbs.ToolTipText = "Frodo support must be enabled before this option can be selected"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(185, 6)
        '
        'tsmiRescrapeKeyWords
        '
        Me.tsmiRescrapeKeyWords.Name = "tsmiRescrapeKeyWords"
        Me.tsmiRescrapeKeyWords.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeKeyWords.Text = "Keywords"
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        Me.ToolStripMenuItem16.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem16.Text = "Backdrop"
        Me.ToolStripMenuItem16.Visible = false
        '
        'ToolStripMenuItem17
        '
        Me.ToolStripMenuItem17.Name = "ToolStripMenuItem17"
        Me.ToolStripMenuItem17.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem17.Text = "Poster"
        Me.ToolStripMenuItem17.Visible = false
        '
        'tsmiRescrapeFanartTv
        '
        Me.tsmiRescrapeFanartTv.Name = "tsmiRescrapeFanartTv"
        Me.tsmiRescrapeFanartTv.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeFanartTv.Text = "Artwork from Fanart.Tv"
        '
        'tsmiRescrapeMovieSetArt
        '
        Me.tsmiRescrapeMovieSetArt.Name = "tsmiRescrapeMovieSetArt"
        Me.tsmiRescrapeMovieSetArt.Size = New System.Drawing.Size(188, 22)
        Me.tsmiRescrapeMovieSetArt.Text = "MovieSet Artwork"
        '
        'ToolStripMenuItem18
        '
        Me.ToolStripMenuItem18.Name = "ToolStripMenuItem18"
        Me.ToolStripMenuItem18.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItem18.Text = "Media Tags"
        '
        'tsmiDlTrailer
        '
        Me.tsmiDlTrailer.Name = "tsmiDlTrailer"
        Me.tsmiDlTrailer.Size = New System.Drawing.Size(188, 22)
        Me.tsmiDlTrailer.Text = "Download Trailer"
        Me.tsmiDlTrailer.ToolTipText = "If Trailer url is populated for selected"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"movie(s), then Trailer will be download"& _ 
    "ed."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(185, 6)
        '
        'RenameFilesToolStripMenuItem
        '
        Me.RenameFilesToolStripMenuItem.Name = "RenameFilesToolStripMenuItem"
        Me.RenameFilesToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.RenameFilesToolStripMenuItem.Text = "Rename files"
        Me.RenameFilesToolStripMenuItem.ToolTipText = resources.GetString("RenameFilesToolStripMenuItem.ToolTipText")
        '
        'ToolStripSeparator28
        '
        Me.ToolStripSeparator28.Name = "ToolStripSeparator28"
        Me.ToolStripSeparator28.Size = New System.Drawing.Size(238, 6)
        '
        'tsmiSetWatched
        '
        Me.tsmiSetWatched.Name = "tsmiSetWatched"
        Me.tsmiSetWatched.Size = New System.Drawing.Size(241, 22)
        Me.tsmiSetWatched.Text = "Set Watched"
        '
        'tsmiClearWatched
        '
        Me.tsmiClearWatched.Name = "tsmiClearWatched"
        Me.tsmiClearWatched.Size = New System.Drawing.Size(241, 22)
        Me.tsmiClearWatched.Text = "Clear Watched"
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(238, 6)
        '
        'mov_ToolStripFanartBrowserAlt
        '
        Me.mov_ToolStripFanartBrowserAlt.Name = "mov_ToolStripFanartBrowserAlt"
        Me.mov_ToolStripFanartBrowserAlt.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripFanartBrowserAlt.Text = "Fanart Browser (Alternative)"
        '
        'mov_ToolStripPosterBrowserAlt
        '
        Me.mov_ToolStripPosterBrowserAlt.Name = "mov_ToolStripPosterBrowserAlt"
        Me.mov_ToolStripPosterBrowserAlt.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripPosterBrowserAlt.Text = "Poster Browser (Alternative)"
        '
        'mov_ToolStripEditMovieAlt
        '
        Me.mov_ToolStripEditMovieAlt.Name = "mov_ToolStripEditMovieAlt"
        Me.mov_ToolStripEditMovieAlt.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripEditMovieAlt.Text = "Edit Movie (Alternative)"
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        Me.ToolStripSeparator24.Size = New System.Drawing.Size(238, 6)
        '
        'mov_ToolStripExportMovies
        '
        Me.mov_ToolStripExportMovies.Name = "mov_ToolStripExportMovies"
        Me.mov_ToolStripExportMovies.Size = New System.Drawing.Size(241, 22)
        Me.mov_ToolStripExportMovies.Text = "Export Selected Movies"
        '
        'tsmiOpenInMkvmergeGUI
        '
        Me.tsmiOpenInMkvmergeGUI.Enabled = false
        Me.tsmiOpenInMkvmergeGUI.Name = "tsmiOpenInMkvmergeGUI"
        Me.tsmiOpenInMkvmergeGUI.Size = New System.Drawing.Size(241, 22)
        Me.tsmiOpenInMkvmergeGUI.Text = "Open in mkvmerge GUI"
        '
        'tsmiSyncToXBMC
        '
        Me.tsmiSyncToXBMC.BackgroundImage = CType(resources.GetObject("tsmiSyncToXBMC.BackgroundImage"),System.Drawing.Image)
        Me.tsmiSyncToXBMC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tsmiSyncToXBMC.Name = "tsmiSyncToXBMC"
        Me.tsmiSyncToXBMC.Size = New System.Drawing.Size(241, 22)
        Me.tsmiSyncToXBMC.Text = "Sync to XBMC"
        '
        'tsmiConvertToFrodo
        '
        Me.tsmiConvertToFrodo.Name = "tsmiConvertToFrodo"
        Me.tsmiConvertToFrodo.Size = New System.Drawing.Size(241, 22)
        Me.tsmiConvertToFrodo.Text = "Convert to Frodo only"
        '
        'TabPageLevel2MovMainBrowser
        '
        Me.TabPageLevel2MovMainBrowser.AutoScroll = true
        Me.TabPageLevel2MovMainBrowser.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.TabPageLevel2MovMainBrowser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageLevel2MovMainBrowser.Controls.Add(Me.SplitContainer1)
        Me.TabPageLevel2MovMainBrowser.Location = New System.Drawing.Point(4, 25)
        Me.TabPageLevel2MovMainBrowser.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPageLevel2MovMainBrowser.Name = "TabPageLevel2MovMainBrowser"
        Me.TabPageLevel2MovMainBrowser.Size = New System.Drawing.Size(1041, 628)
        Me.TabPageLevel2MovMainBrowser.TabIndex = 0
        Me.TabPageLevel2MovMainBrowser.Tag = "M"
        Me.TabPageLevel2MovMainBrowser.Text = "Main Browser"
        Me.TabPageLevel2MovMainBrowser.ToolTipText = "Main Movie Browser"
        Me.TabPageLevel2MovMainBrowser.UseVisualStyleBackColor = true
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer1.Panel1MinSize = 325
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = true
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer1.Panel2.Controls.Add(Me.ftvArtPicBox)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label128)
        Me.SplitContainer1.Panel2.Controls.Add(Me.movieGraphicInfo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tlpMovies)
        Me.SplitContainer1.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.SplitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer1.Panel2MinSize = 630
        Me.SplitContainer1.Size = New System.Drawing.Size(1037, 624)
        Me.SplitContainer1.SplitterDistance = 327
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 137
        '
        'SplitContainer5
        '
        Me.SplitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.cbBtnLink)
        Me.SplitContainer5.Panel1.Controls.Add(Me.TooltipGridViewMovies1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnMovRefreshAll)
        Me.SplitContainer5.Panel1.Controls.Add(Me.DataGridViewMovies)
        Me.SplitContainer5.Panel1.Controls.Add(Me.cbSort)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnreverse)
        Me.SplitContainer5.Panel1.Controls.Add(Me.DebugSplitter5PosLabel)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnResetFilters)
        Me.SplitContainer5.Panel1.Controls.Add(Me.LabelCountFilter)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txt_titlesearch)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnMovSearchNew)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.TextBox1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer5.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.SplitContainer5.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.AutoScroll = true
        Me.SplitContainer5.Panel2.AutoScrollMinSize = New System.Drawing.Size(0, 400)
        Me.SplitContainer5.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer5.Panel2.ContextMenuStrip = Me.cmsConfigureMovieFilters
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterSubTitleLang)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterUserRated)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterRootFolder)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterSubTitleLangMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterUserRatedMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterRootFolderMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterSubTitleLang)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterUserRated)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterRootFolder)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterVideoCodec)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterVideoCodecMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterVideoCodec)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterDirector)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterDirectorMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterDirector)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterTag)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterTagMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterTag)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterSourceMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterSource)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterActorMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterActor)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioLanguagesMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioDefaultLanguagesMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterAudioLanguages)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterAudioDefaultLanguages)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterNumAudioTracksMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterNumAudioTracks)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioBitratesMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterAudioBitrates)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioChannelsMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterAudioChannels)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterCertificateMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioCodecsMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterResolutionMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterSetMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterGenreMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterCountriesMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterStudiosMode)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterAudioCodecs)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterResolution)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterSet)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterCertificate)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterCertificate)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterGenre)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterCountries)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterStudios)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterYear)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterYear)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterVotes)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterRuntime)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterFolderSizes)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterFolderSizes)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterVotes)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterRuntime)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterRating)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterNumAudioTracks)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioBitrates)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioChannels)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioLanguages)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioDefaultLanguages)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterAudioCodecs)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterResolution)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterGeneral)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterActor)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterSet)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterSource)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterCountries)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterStudios)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblFilterGenre)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterGeneral)
        Me.SplitContainer5.Panel2.Controls.Add(Me.cbFilterRating)
        Me.SplitContainer5.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.SplitContainer5.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer5.Panel2MinSize = 1
        Me.SplitContainer5.Size = New System.Drawing.Size(327, 624)
        Me.SplitContainer5.SplitterDistance = 346
        Me.SplitContainer5.SplitterWidth = 5
        Me.SplitContainer5.TabIndex = 68
        '
        'cbBtnLink
        '
        Me.cbBtnLink.Appearance = System.Windows.Forms.Appearance.Button
        Me.cbBtnLink.BackColor = System.Drawing.Color.LightGreen
        Me.cbBtnLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold)
        Me.cbBtnLink.Image = CType(resources.GetObject("cbBtnLink.Image"),System.Drawing.Image)
        Me.cbBtnLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbBtnLink.Location = New System.Drawing.Point(233, 4)
        Me.cbBtnLink.Name = "cbBtnLink"
        Me.cbBtnLink.Size = New System.Drawing.Size(85, 41)
        Me.cbBtnLink.TabIndex = 185
        Me.cbBtnLink.Text = "Link"
        Me.cbBtnLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbBtnLink.UseVisualStyleBackColor = false
        '
        'TooltipGridViewMovies1
        '
        Me.TooltipGridViewMovies1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TooltipGridViewMovies1.Location = New System.Drawing.Point(114, 132)
        Me.TooltipGridViewMovies1.Name = "TooltipGridViewMovies1"
        Me.TooltipGridViewMovies1.Size = New System.Drawing.Size(198, 127)
        Me.TooltipGridViewMovies1.TabIndex = 177
        Me.TooltipGridViewMovies1.Visible = false
        '
        'DataGridViewMovies
        '
        Me.DataGridViewMovies.AllowDrop = true
        Me.DataGridViewMovies.AllowUserToAddRows = false
        Me.DataGridViewMovies.AllowUserToDeleteRows = false
        Me.DataGridViewMovies.AllowUserToResizeRows = false
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.DataGridViewMovies.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewMovies.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.DataGridViewMovies.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridViewMovies.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridViewMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DataGridViewMovies.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewMovies.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMovies.ContextMenuStrip = Me.MovieContextMenu
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkSeaGreen
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewMovies.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewMovies.GridColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.DataGridViewMovies.Location = New System.Drawing.Point(4, 132)
        Me.DataGridViewMovies.Name = "DataGridViewMovies"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewMovies.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewMovies.ShowCellErrors = false
        Me.DataGridViewMovies.ShowRowErrors = false
        Me.DataGridViewMovies.Size = New System.Drawing.Size(314, 248)
        Me.DataGridViewMovies.StandardTab = true
        Me.DataGridViewMovies.TabIndex = 174
        '
        'cbSort
        '
        Me.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbSort.FormattingEnabled = true
        Me.cbSort.Items.AddRange(New Object() {"A - Z", "Movie Year", "Modified", "Runtime", "Rating", "User Rated", "Sort Order", "Date Added", "Votes", "Resolution", "Certificate", "Folder Size"})
        Me.cbSort.Location = New System.Drawing.Point(33, 81)
        Me.cbSort.Name = "cbSort"
        Me.cbSort.Size = New System.Drawing.Size(87, 21)
        Me.cbSort.TabIndex = 72
        '
        'btnreverse
        '
        Me.btnreverse.Appearance = System.Windows.Forms.Appearance.Button
        Me.btnreverse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnreverse.Location = New System.Drawing.Point(130, 80)
        Me.btnreverse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(87, 23)
        Me.btnreverse.TabIndex = 71
        Me.btnreverse.Text = "Invert Order"
        Me.btnreverse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreverse.UseVisualStyleBackColor = true
        '
        'DebugSplitter5PosLabel
        '
        Me.DebugSplitter5PosLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.DebugSplitter5PosLabel.AutoSize = true
        Me.DebugSplitter5PosLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.DebugSplitter5PosLabel.ForeColor = System.Drawing.Color.Red
        Me.DebugSplitter5PosLabel.Location = New System.Drawing.Point(140, 326)
        Me.DebugSplitter5PosLabel.Name = "DebugSplitter5PosLabel"
        Me.DebugSplitter5PosLabel.Size = New System.Drawing.Size(185, 16)
        Me.DebugSplitter5PosLabel.TabIndex = 69
        Me.DebugSplitter5PosLabel.Text = "Debug - Splitter5 Size Display"
        Me.DebugSplitter5PosLabel.Visible = false
        '
        'btnResetFilters
        '
        Me.btnResetFilters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnResetFilters.Location = New System.Drawing.Point(225, 80)
        Me.btnResetFilters.Margin = New System.Windows.Forms.Padding(4)
        Me.btnResetFilters.Name = "btnResetFilters"
        Me.btnResetFilters.Size = New System.Drawing.Size(94, 23)
        Me.btnResetFilters.TabIndex = 67
        Me.btnResetFilters.Text = "Reset Filters"
        Me.btnResetFilters.UseVisualStyleBackColor = true
        '
        'LabelCountFilter
        '
        Me.LabelCountFilter.AutoSize = true
        Me.LabelCountFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelCountFilter.Location = New System.Drawing.Point(159, 108)
        Me.LabelCountFilter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCountFilter.Name = "LabelCountFilter"
        Me.LabelCountFilter.Size = New System.Drawing.Size(85, 13)
        Me.LabelCountFilter.TabIndex = 65
        Me.LabelCountFilter.Text = "Displaying 0 of 0"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbFolder)
        Me.Panel1.Controls.Add(Me.rbFileName)
        Me.Panel1.Controls.Add(Me.rbTitleAndYear)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Location = New System.Drawing.Point(2, 47)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(317, 33)
        Me.Panel1.TabIndex = 61
        '
        'rbFolder
        '
        Me.rbFolder.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFolder.Image = Global.Media_Companion.My.Resources.Resources.Folder
        Me.rbFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rbFolder.Location = New System.Drawing.Point(223, 2)
        Me.rbFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.rbFolder.Name = "rbFolder"
        Me.rbFolder.Size = New System.Drawing.Size(94, 25)
        Me.rbFolder.TabIndex = 2
        Me.rbFolder.Text = "Folder Name"
        Me.rbFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbFolder.UseVisualStyleBackColor = true
        '
        'rbFileName
        '
        Me.rbFileName.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFileName.Image = Global.Media_Companion.My.Resources.Resources.Page
        Me.rbFileName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rbFileName.Location = New System.Drawing.Point(128, 2)
        Me.rbFileName.Margin = New System.Windows.Forms.Padding(4)
        Me.rbFileName.Name = "rbFileName"
        Me.rbFileName.Size = New System.Drawing.Size(87, 25)
        Me.rbFileName.TabIndex = 1
        Me.rbFileName.Text = "File Name"
        Me.rbFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbFileName.UseVisualStyleBackColor = true
        '
        'rbTitleAndYear
        '
        Me.rbTitleAndYear.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbTitleAndYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTitleAndYear.Image = Global.Media_Companion.My.Resources.Resources.Clock
        Me.rbTitleAndYear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rbTitleAndYear.Location = New System.Drawing.Point(31, 2)
        Me.rbTitleAndYear.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTitleAndYear.Name = "rbTitleAndYear"
        Me.rbTitleAndYear.Size = New System.Drawing.Size(87, 25)
        Me.rbTitleAndYear.TabIndex = 0
        Me.rbTitleAndYear.Text = "Title && Year"
        Me.rbTitleAndYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbTitleAndYear.UseVisualStyleBackColor = true
        '
        'Label31
        '
        Me.Label31.AutoSize = true
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label31.Location = New System.Drawing.Point(1, 8)
        Me.Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(23, 13)
        Me.Label31.TabIndex = 62
        Me.Label31.Text = "List"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 84)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Sort"
        '
        'cmsConfigureMovieFilters
        '
        Me.cmsConfigureMovieFilters.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigureMovieFiltersToolStripMenuItem1})
        Me.cmsConfigureMovieFilters.Name = "cmsConfigureMovieFilters"
        Me.cmsConfigureMovieFilters.Size = New System.Drawing.Size(185, 26)
        '
        'ConfigureMovieFiltersToolStripMenuItem1
        '
        Me.ConfigureMovieFiltersToolStripMenuItem1.Name = "ConfigureMovieFiltersToolStripMenuItem1"
        Me.ConfigureMovieFiltersToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.ConfigureMovieFiltersToolStripMenuItem1.Text = "Configure Movie Filters"
        '
        'cbFilterSubTitleLang
        '
        Me.cbFilterSubTitleLang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterSubTitleLang.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterSubTitleLang.CheckOnClick = true
        Me.cbFilterSubTitleLang.DisplayWhenNothingSelected = "All"
        Me.cbFilterSubTitleLang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterSubTitleLang.DropDownHeight = 1
        Me.cbFilterSubTitleLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterSubTitleLang.FormattingEnabled = true
        Me.cbFilterSubTitleLang.IntegralHeight = false
        Me.cbFilterSubTitleLang.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterSubTitleLang.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterSubTitleLang.Name = "cbFilterSubTitleLang"
        Me.cbFilterSubTitleLang.QuickSelect = false
        Me.cbFilterSubTitleLang.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterSubTitleLang.TabIndex = 262
        Me.cbFilterSubTitleLang.Tag = "14"
        Me.cbFilterSubTitleLang.ValueSeparator = " "
        '
        'cbFilterUserRated
        '
        Me.cbFilterUserRated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterUserRated.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterUserRated.CheckOnClick = true
        Me.cbFilterUserRated.DisplayWhenNothingSelected = "All"
        Me.cbFilterUserRated.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterUserRated.DropDownHeight = 1
        Me.cbFilterUserRated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterUserRated.FormattingEnabled = true
        Me.cbFilterUserRated.IntegralHeight = false
        Me.cbFilterUserRated.Location = New System.Drawing.Point(147, 1898)
        Me.cbFilterUserRated.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterUserRated.Name = "cbFilterUserRated"
        Me.cbFilterUserRated.QuickSelect = false
        Me.cbFilterUserRated.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterUserRated.TabIndex = 271
        Me.cbFilterUserRated.Tag = "14"
        Me.cbFilterUserRated.ValueSeparator = " "
        '
        'cbFilterRootFolder
        '
        Me.cbFilterRootFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterRootFolder.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterRootFolder.CheckOnClick = true
        Me.cbFilterRootFolder.DisplayWhenNothingSelected = "All"
        Me.cbFilterRootFolder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterRootFolder.DropDownHeight = 1
        Me.cbFilterRootFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterRootFolder.FormattingEnabled = true
        Me.cbFilterRootFolder.IntegralHeight = false
        Me.cbFilterRootFolder.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterRootFolder.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterRootFolder.Name = "cbFilterRootFolder"
        Me.cbFilterRootFolder.QuickSelect = false
        Me.cbFilterRootFolder.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterRootFolder.TabIndex = 268
        Me.cbFilterRootFolder.Tag = "14"
        Me.cbFilterRootFolder.ValueSeparator = " "
        '
        'lblFilterSubTitleLangMode
        '
        Me.lblFilterSubTitleLangMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterSubTitleLangMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterSubTitleLangMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSubTitleLangMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterSubTitleLangMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterSubTitleLangMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterSubTitleLangMode.Name = "lblFilterSubTitleLangMode"
        Me.lblFilterSubTitleLangMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterSubTitleLangMode.TabIndex = 261
        Me.lblFilterSubTitleLangMode.Text = "M"
        Me.lblFilterSubTitleLangMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterUserRatedMode
        '
        Me.lblFilterUserRatedMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterUserRatedMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterUserRatedMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterUserRatedMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterUserRatedMode.Location = New System.Drawing.Point(129, 5781)
        Me.lblFilterUserRatedMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterUserRatedMode.Name = "lblFilterUserRatedMode"
        Me.lblFilterUserRatedMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterUserRatedMode.TabIndex = 272
        Me.lblFilterUserRatedMode.Text = "M"
        Me.lblFilterUserRatedMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterRootFolderMode
        '
        Me.lblFilterRootFolderMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterRootFolderMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterRootFolderMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterRootFolderMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterRootFolderMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterRootFolderMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterRootFolderMode.Name = "lblFilterRootFolderMode"
        Me.lblFilterRootFolderMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterRootFolderMode.TabIndex = 269
        Me.lblFilterRootFolderMode.Text = "M"
        Me.lblFilterRootFolderMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSubTitleLang
        '
        Me.lblFilterSubTitleLang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterSubTitleLang.BackColor = System.Drawing.Color.Gray
        Me.lblFilterSubTitleLang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSubTitleLang.ForeColor = System.Drawing.Color.White
        Me.lblFilterSubTitleLang.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterSubTitleLang.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterSubTitleLang.Name = "lblFilterSubTitleLang"
        Me.lblFilterSubTitleLang.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterSubTitleLang.TabIndex = 260
        Me.lblFilterSubTitleLang.Text = "Subtitle Language"
        Me.lblFilterSubTitleLang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterUserRated
        '
        Me.lblFilterUserRated.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterUserRated.BackColor = System.Drawing.Color.Gray
        Me.lblFilterUserRated.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterUserRated.ForeColor = System.Drawing.Color.White
        Me.lblFilterUserRated.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterUserRated.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterUserRated.Name = "lblFilterUserRated"
        Me.lblFilterUserRated.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterUserRated.TabIndex = 273
        Me.lblFilterUserRated.Text = "User Rating"
        Me.lblFilterUserRated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterRootFolder
        '
        Me.lblFilterRootFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterRootFolder.BackColor = System.Drawing.Color.Gray
        Me.lblFilterRootFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterRootFolder.ForeColor = System.Drawing.Color.White
        Me.lblFilterRootFolder.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterRootFolder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterRootFolder.Name = "lblFilterRootFolder"
        Me.lblFilterRootFolder.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterRootFolder.TabIndex = 270
        Me.lblFilterRootFolder.Text = "Root Folder"
        Me.lblFilterRootFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterVideoCodec
        '
        Me.lblFilterVideoCodec.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterVideoCodec.BackColor = System.Drawing.Color.Gray
        Me.lblFilterVideoCodec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterVideoCodec.ForeColor = System.Drawing.Color.White
        Me.lblFilterVideoCodec.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterVideoCodec.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterVideoCodec.Name = "lblFilterVideoCodec"
        Me.lblFilterVideoCodec.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterVideoCodec.TabIndex = 259
        Me.lblFilterVideoCodec.Text = "Video Codec"
        Me.lblFilterVideoCodec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterVideoCodecMode
        '
        Me.lblFilterVideoCodecMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterVideoCodecMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterVideoCodecMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterVideoCodecMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterVideoCodecMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterVideoCodecMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterVideoCodecMode.Name = "lblFilterVideoCodecMode"
        Me.lblFilterVideoCodecMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterVideoCodecMode.TabIndex = 258
        Me.lblFilterVideoCodecMode.Text = "M"
        Me.lblFilterVideoCodecMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterVideoCodec
        '
        Me.cbFilterVideoCodec.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterVideoCodec.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterVideoCodec.CheckOnClick = true
        Me.cbFilterVideoCodec.DisplayWhenNothingSelected = "All"
        Me.cbFilterVideoCodec.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterVideoCodec.DropDownHeight = 1
        Me.cbFilterVideoCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterVideoCodec.FormattingEnabled = true
        Me.cbFilterVideoCodec.IntegralHeight = false
        Me.cbFilterVideoCodec.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterVideoCodec.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterVideoCodec.Name = "cbFilterVideoCodec"
        Me.cbFilterVideoCodec.QuickSelect = false
        Me.cbFilterVideoCodec.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterVideoCodec.TabIndex = 257
        Me.cbFilterVideoCodec.Tag = "14"
        Me.cbFilterVideoCodec.ValueSeparator = " "
        '
        'cbFilterDirector
        '
        Me.cbFilterDirector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterDirector.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterDirector.CheckOnClick = true
        Me.cbFilterDirector.DisplayWhenNothingSelected = "All"
        Me.cbFilterDirector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterDirector.DropDownHeight = 1
        Me.cbFilterDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterDirector.FormattingEnabled = true
        Me.cbFilterDirector.IntegralHeight = false
        Me.cbFilterDirector.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterDirector.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterDirector.Name = "cbFilterDirector"
        Me.cbFilterDirector.QuickSelect = false
        Me.cbFilterDirector.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterDirector.TabIndex = 256
        Me.cbFilterDirector.Tag = "14"
        Me.cbFilterDirector.ValueSeparator = " "
        '
        'lblFilterDirectorMode
        '
        Me.lblFilterDirectorMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterDirectorMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterDirectorMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterDirectorMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterDirectorMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterDirectorMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterDirectorMode.Name = "lblFilterDirectorMode"
        Me.lblFilterDirectorMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterDirectorMode.TabIndex = 255
        Me.lblFilterDirectorMode.Text = "M"
        Me.lblFilterDirectorMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterDirector
        '
        Me.lblFilterDirector.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterDirector.BackColor = System.Drawing.Color.Gray
        Me.lblFilterDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterDirector.ForeColor = System.Drawing.Color.White
        Me.lblFilterDirector.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterDirector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterDirector.Name = "lblFilterDirector"
        Me.lblFilterDirector.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterDirector.TabIndex = 254
        Me.lblFilterDirector.Text = "Director"
        Me.lblFilterDirector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterTag
        '
        Me.cbFilterTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterTag.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterTag.CheckOnClick = true
        Me.cbFilterTag.DisplayWhenNothingSelected = "All"
        Me.cbFilterTag.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterTag.DropDownHeight = 1
        Me.cbFilterTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterTag.FormattingEnabled = true
        Me.cbFilterTag.IntegralHeight = false
        Me.cbFilterTag.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterTag.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterTag.Name = "cbFilterTag"
        Me.cbFilterTag.QuickSelect = false
        Me.cbFilterTag.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterTag.TabIndex = 250
        Me.cbFilterTag.Tag = "14"
        Me.cbFilterTag.ValueSeparator = " "
        '
        'lblFilterTagMode
        '
        Me.lblFilterTagMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterTagMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterTagMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterTagMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterTagMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterTagMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterTagMode.Name = "lblFilterTagMode"
        Me.lblFilterTagMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterTagMode.TabIndex = 249
        Me.lblFilterTagMode.Text = "M"
        Me.lblFilterTagMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterTag
        '
        Me.lblFilterTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterTag.BackColor = System.Drawing.Color.Gray
        Me.lblFilterTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterTag.ForeColor = System.Drawing.Color.White
        Me.lblFilterTag.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterTag.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterTag.Name = "lblFilterTag"
        Me.lblFilterTag.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterTag.TabIndex = 248
        Me.lblFilterTag.Text = "Tag"
        Me.lblFilterTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSourceMode
        '
        Me.lblFilterSourceMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterSourceMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterSourceMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSourceMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterSourceMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterSourceMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterSourceMode.Name = "lblFilterSourceMode"
        Me.lblFilterSourceMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterSourceMode.TabIndex = 247
        Me.lblFilterSourceMode.Text = "M"
        Me.lblFilterSourceMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterSource
        '
        Me.cbFilterSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterSource.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterSource.CheckOnClick = true
        Me.cbFilterSource.DisplayWhenNothingSelected = "All"
        Me.cbFilterSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterSource.DropDownHeight = 1
        Me.cbFilterSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterSource.FormattingEnabled = true
        Me.cbFilterSource.IntegralHeight = false
        Me.cbFilterSource.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterSource.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterSource.Name = "cbFilterSource"
        Me.cbFilterSource.QuickSelect = false
        Me.cbFilterSource.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterSource.TabIndex = 246
        Me.cbFilterSource.Tag = "14"
        Me.cbFilterSource.ValueSeparator = " "
        '
        'lblFilterActorMode
        '
        Me.lblFilterActorMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterActorMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterActorMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterActorMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterActorMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterActorMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterActorMode.Name = "lblFilterActorMode"
        Me.lblFilterActorMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterActorMode.TabIndex = 245
        Me.lblFilterActorMode.Text = "M"
        Me.lblFilterActorMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterActor
        '
        Me.cbFilterActor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterActor.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterActor.CheckOnClick = true
        Me.cbFilterActor.DisplayWhenNothingSelected = "All"
        Me.cbFilterActor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterActor.DropDownHeight = 1
        Me.cbFilterActor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterActor.FormattingEnabled = true
        Me.cbFilterActor.IntegralHeight = false
        Me.cbFilterActor.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterActor.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterActor.Name = "cbFilterActor"
        Me.cbFilterActor.QuickSelect = false
        Me.cbFilterActor.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterActor.TabIndex = 244
        Me.cbFilterActor.Tag = "14"
        Me.cbFilterActor.ValueSeparator = " "
        '
        'lblFilterAudioLanguagesMode
        '
        Me.lblFilterAudioLanguagesMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioLanguagesMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioLanguagesMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioLanguagesMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioLanguagesMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterAudioLanguagesMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioLanguagesMode.Name = "lblFilterAudioLanguagesMode"
        Me.lblFilterAudioLanguagesMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterAudioLanguagesMode.TabIndex = 243
        Me.lblFilterAudioLanguagesMode.Text = "M"
        Me.lblFilterAudioLanguagesMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioDefaultLanguagesMode
        '
        Me.lblFilterAudioDefaultLanguagesMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioDefaultLanguagesMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioDefaultLanguagesMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioDefaultLanguagesMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioDefaultLanguagesMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterAudioDefaultLanguagesMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioDefaultLanguagesMode.Name = "lblFilterAudioDefaultLanguagesMode"
        Me.lblFilterAudioDefaultLanguagesMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterAudioDefaultLanguagesMode.TabIndex = 243
        Me.lblFilterAudioDefaultLanguagesMode.Text = "M"
        Me.lblFilterAudioDefaultLanguagesMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterAudioLanguages
        '
        Me.cbFilterAudioLanguages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterAudioLanguages.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterAudioLanguages.CheckOnClick = true
        Me.cbFilterAudioLanguages.DisplayWhenNothingSelected = "All"
        Me.cbFilterAudioLanguages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterAudioLanguages.DropDownHeight = 1
        Me.cbFilterAudioLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterAudioLanguages.FormattingEnabled = true
        Me.cbFilterAudioLanguages.IntegralHeight = false
        Me.cbFilterAudioLanguages.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterAudioLanguages.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterAudioLanguages.Name = "cbFilterAudioLanguages"
        Me.cbFilterAudioLanguages.QuickSelect = false
        Me.cbFilterAudioLanguages.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterAudioLanguages.TabIndex = 242
        Me.cbFilterAudioLanguages.Tag = "14"
        Me.cbFilterAudioLanguages.ValueSeparator = " "
        '
        'cbFilterAudioDefaultLanguages
        '
        Me.cbFilterAudioDefaultLanguages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterAudioDefaultLanguages.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterAudioDefaultLanguages.CheckOnClick = true
        Me.cbFilterAudioDefaultLanguages.DisplayWhenNothingSelected = "All"
        Me.cbFilterAudioDefaultLanguages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterAudioDefaultLanguages.DropDownHeight = 1
        Me.cbFilterAudioDefaultLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterAudioDefaultLanguages.FormattingEnabled = true
        Me.cbFilterAudioDefaultLanguages.IntegralHeight = false
        Me.cbFilterAudioDefaultLanguages.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterAudioDefaultLanguages.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterAudioDefaultLanguages.Name = "cbFilterAudioDefaultLanguages"
        Me.cbFilterAudioDefaultLanguages.QuickSelect = false
        Me.cbFilterAudioDefaultLanguages.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterAudioDefaultLanguages.TabIndex = 242
        Me.cbFilterAudioDefaultLanguages.Tag = "14"
        Me.cbFilterAudioDefaultLanguages.ValueSeparator = " "
        '
        'lblFilterNumAudioTracksMode
        '
        Me.lblFilterNumAudioTracksMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterNumAudioTracksMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterNumAudioTracksMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterNumAudioTracksMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterNumAudioTracksMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterNumAudioTracksMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterNumAudioTracksMode.Name = "lblFilterNumAudioTracksMode"
        Me.lblFilterNumAudioTracksMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterNumAudioTracksMode.TabIndex = 241
        Me.lblFilterNumAudioTracksMode.Text = "M"
        Me.lblFilterNumAudioTracksMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterNumAudioTracks
        '
        Me.cbFilterNumAudioTracks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterNumAudioTracks.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterNumAudioTracks.CheckOnClick = true
        Me.cbFilterNumAudioTracks.DisplayWhenNothingSelected = "All"
        Me.cbFilterNumAudioTracks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterNumAudioTracks.DropDownHeight = 1
        Me.cbFilterNumAudioTracks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterNumAudioTracks.FormattingEnabled = true
        Me.cbFilterNumAudioTracks.IntegralHeight = false
        Me.cbFilterNumAudioTracks.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterNumAudioTracks.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterNumAudioTracks.Name = "cbFilterNumAudioTracks"
        Me.cbFilterNumAudioTracks.QuickSelect = false
        Me.cbFilterNumAudioTracks.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterNumAudioTracks.TabIndex = 240
        Me.cbFilterNumAudioTracks.Tag = "14"
        Me.cbFilterNumAudioTracks.ValueSeparator = " "
        '
        'lblFilterAudioBitratesMode
        '
        Me.lblFilterAudioBitratesMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioBitratesMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioBitratesMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioBitratesMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioBitratesMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterAudioBitratesMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioBitratesMode.Name = "lblFilterAudioBitratesMode"
        Me.lblFilterAudioBitratesMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterAudioBitratesMode.TabIndex = 239
        Me.lblFilterAudioBitratesMode.Text = "M"
        Me.lblFilterAudioBitratesMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterAudioBitrates
        '
        Me.cbFilterAudioBitrates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterAudioBitrates.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterAudioBitrates.CheckOnClick = true
        Me.cbFilterAudioBitrates.DisplayWhenNothingSelected = "All"
        Me.cbFilterAudioBitrates.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterAudioBitrates.DropDownHeight = 1
        Me.cbFilterAudioBitrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterAudioBitrates.FormattingEnabled = true
        Me.cbFilterAudioBitrates.IntegralHeight = false
        Me.cbFilterAudioBitrates.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterAudioBitrates.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterAudioBitrates.Name = "cbFilterAudioBitrates"
        Me.cbFilterAudioBitrates.QuickSelect = false
        Me.cbFilterAudioBitrates.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterAudioBitrates.TabIndex = 238
        Me.cbFilterAudioBitrates.Tag = "14"
        Me.cbFilterAudioBitrates.ValueSeparator = " "
        '
        'lblFilterAudioChannelsMode
        '
        Me.lblFilterAudioChannelsMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioChannelsMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioChannelsMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioChannelsMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioChannelsMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterAudioChannelsMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioChannelsMode.Name = "lblFilterAudioChannelsMode"
        Me.lblFilterAudioChannelsMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterAudioChannelsMode.TabIndex = 237
        Me.lblFilterAudioChannelsMode.Text = "M"
        Me.lblFilterAudioChannelsMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterAudioChannels
        '
        Me.cbFilterAudioChannels.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterAudioChannels.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterAudioChannels.CheckOnClick = true
        Me.cbFilterAudioChannels.DisplayWhenNothingSelected = "All"
        Me.cbFilterAudioChannels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterAudioChannels.DropDownHeight = 1
        Me.cbFilterAudioChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterAudioChannels.FormattingEnabled = true
        Me.cbFilterAudioChannels.IntegralHeight = false
        Me.cbFilterAudioChannels.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterAudioChannels.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterAudioChannels.Name = "cbFilterAudioChannels"
        Me.cbFilterAudioChannels.QuickSelect = false
        Me.cbFilterAudioChannels.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterAudioChannels.TabIndex = 236
        Me.cbFilterAudioChannels.Tag = "14"
        Me.cbFilterAudioChannels.ValueSeparator = " "
        '
        'lblFilterCertificateMode
        '
        Me.lblFilterCertificateMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterCertificateMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterCertificateMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterCertificateMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterCertificateMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterCertificateMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterCertificateMode.Name = "lblFilterCertificateMode"
        Me.lblFilterCertificateMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterCertificateMode.TabIndex = 235
        Me.lblFilterCertificateMode.Text = "M"
        Me.lblFilterCertificateMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioCodecsMode
        '
        Me.lblFilterAudioCodecsMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioCodecsMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioCodecsMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioCodecsMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioCodecsMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterAudioCodecsMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioCodecsMode.Name = "lblFilterAudioCodecsMode"
        Me.lblFilterAudioCodecsMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterAudioCodecsMode.TabIndex = 234
        Me.lblFilterAudioCodecsMode.Text = "M"
        Me.lblFilterAudioCodecsMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterResolutionMode
        '
        Me.lblFilterResolutionMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterResolutionMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterResolutionMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterResolutionMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterResolutionMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterResolutionMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterResolutionMode.Name = "lblFilterResolutionMode"
        Me.lblFilterResolutionMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterResolutionMode.TabIndex = 233
        Me.lblFilterResolutionMode.Text = "M"
        Me.lblFilterResolutionMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSetMode
        '
        Me.lblFilterSetMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterSetMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterSetMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSetMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterSetMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterSetMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterSetMode.Name = "lblFilterSetMode"
        Me.lblFilterSetMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterSetMode.TabIndex = 232
        Me.lblFilterSetMode.Text = "M"
        Me.lblFilterSetMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterGenreMode
        '
        Me.lblFilterGenreMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterGenreMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterGenreMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterGenreMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterGenreMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterGenreMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterGenreMode.Name = "lblFilterGenreMode"
        Me.lblFilterGenreMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterGenreMode.TabIndex = 231
        Me.lblFilterGenreMode.Text = "M"
        Me.lblFilterGenreMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterCountriesMode
        '
        Me.lblFilterCountriesMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterCountriesMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterCountriesMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterCountriesMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterCountriesMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterCountriesMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterCountriesMode.Name = "lblFilterCountriesMode"
        Me.lblFilterCountriesMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterCountriesMode.TabIndex = 231
        Me.lblFilterCountriesMode.Text = "M"
        Me.lblFilterCountriesMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterStudiosMode
        '
        Me.lblFilterStudiosMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterStudiosMode.BackColor = System.Drawing.Color.Gray
        Me.lblFilterStudiosMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterStudiosMode.ForeColor = System.Drawing.Color.White
        Me.lblFilterStudiosMode.Location = New System.Drawing.Point(129, 32767)
        Me.lblFilterStudiosMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterStudiosMode.Name = "lblFilterStudiosMode"
        Me.lblFilterStudiosMode.Size = New System.Drawing.Size(17, 21)
        Me.lblFilterStudiosMode.TabIndex = 267
        Me.lblFilterStudiosMode.Text = "M"
        Me.lblFilterStudiosMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterAudioCodecs
        '
        Me.cbFilterAudioCodecs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterAudioCodecs.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterAudioCodecs.CheckOnClick = true
        Me.cbFilterAudioCodecs.DisplayWhenNothingSelected = "All"
        Me.cbFilterAudioCodecs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterAudioCodecs.DropDownHeight = 1
        Me.cbFilterAudioCodecs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterAudioCodecs.FormattingEnabled = true
        Me.cbFilterAudioCodecs.IntegralHeight = false
        Me.cbFilterAudioCodecs.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterAudioCodecs.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterAudioCodecs.Name = "cbFilterAudioCodecs"
        Me.cbFilterAudioCodecs.QuickSelect = false
        Me.cbFilterAudioCodecs.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterAudioCodecs.TabIndex = 230
        Me.cbFilterAudioCodecs.Tag = "14"
        Me.cbFilterAudioCodecs.ValueSeparator = " "
        '
        'cbFilterResolution
        '
        Me.cbFilterResolution.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterResolution.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterResolution.CheckOnClick = true
        Me.cbFilterResolution.DisplayWhenNothingSelected = "All"
        Me.cbFilterResolution.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterResolution.DropDownHeight = 1
        Me.cbFilterResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterResolution.FormattingEnabled = true
        Me.cbFilterResolution.IntegralHeight = false
        Me.cbFilterResolution.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterResolution.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterResolution.Name = "cbFilterResolution"
        Me.cbFilterResolution.QuickSelect = false
        Me.cbFilterResolution.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterResolution.TabIndex = 229
        Me.cbFilterResolution.Tag = "14"
        Me.cbFilterResolution.ValueSeparator = " "
        '
        'cbFilterSet
        '
        Me.cbFilterSet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterSet.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterSet.CheckOnClick = true
        Me.cbFilterSet.DisplayWhenNothingSelected = "All"
        Me.cbFilterSet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterSet.DropDownHeight = 1
        Me.cbFilterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterSet.FormattingEnabled = true
        Me.cbFilterSet.IntegralHeight = false
        Me.cbFilterSet.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterSet.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterSet.Name = "cbFilterSet"
        Me.cbFilterSet.QuickSelect = false
        Me.cbFilterSet.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterSet.TabIndex = 228
        Me.cbFilterSet.Tag = "14"
        Me.cbFilterSet.ValueSeparator = " "
        '
        'cbFilterCertificate
        '
        Me.cbFilterCertificate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterCertificate.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterCertificate.CheckOnClick = true
        Me.cbFilterCertificate.DisplayWhenNothingSelected = "All"
        Me.cbFilterCertificate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterCertificate.DropDownHeight = 1
        Me.cbFilterCertificate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterCertificate.FormattingEnabled = true
        Me.cbFilterCertificate.IntegralHeight = false
        Me.cbFilterCertificate.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterCertificate.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        Me.cbFilterCertificate.Name = "cbFilterCertificate"
        Me.cbFilterCertificate.QuickSelect = false
        Me.cbFilterCertificate.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterCertificate.TabIndex = 227
        Me.cbFilterCertificate.Tag = "14"
        Me.cbFilterCertificate.ValueSeparator = " "
        '
        'lblFilterCertificate
        '
        Me.lblFilterCertificate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterCertificate.BackColor = System.Drawing.Color.Gray
        Me.lblFilterCertificate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterCertificate.ForeColor = System.Drawing.Color.White
        Me.lblFilterCertificate.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterCertificate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterCertificate.Name = "lblFilterCertificate"
        Me.lblFilterCertificate.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterCertificate.TabIndex = 226
        Me.lblFilterCertificate.Text = "Certificate"
        Me.lblFilterCertificate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterGenre
        '
        Me.cbFilterGenre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterGenre.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterGenre.CheckOnClick = true
        Me.cbFilterGenre.DisplayWhenNothingSelected = "All"
        Me.cbFilterGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterGenre.DropDownHeight = 1
        Me.cbFilterGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterGenre.FormattingEnabled = true
        Me.cbFilterGenre.IntegralHeight = false
        Me.cbFilterGenre.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterGenre.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Normal
        Me.cbFilterGenre.Name = "cbFilterGenre"
        Me.cbFilterGenre.QuickSelect = false
        Me.cbFilterGenre.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterGenre.TabIndex = 224
        Me.cbFilterGenre.Tag = "1"
        Me.cbFilterGenre.ValueSeparator = " "
        '
        'cbFilterCountries
        '
        Me.cbFilterCountries.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterCountries.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterCountries.CheckOnClick = true
        Me.cbFilterCountries.DisplayWhenNothingSelected = "All"
        Me.cbFilterCountries.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterCountries.DropDownHeight = 1
        Me.cbFilterCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterCountries.FormattingEnabled = true
        Me.cbFilterCountries.IntegralHeight = false
        Me.cbFilterCountries.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterCountries.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Normal
        Me.cbFilterCountries.Name = "cbFilterCountries"
        Me.cbFilterCountries.QuickSelect = false
        Me.cbFilterCountries.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterCountries.TabIndex = 224
        Me.cbFilterCountries.Tag = "1"
        Me.cbFilterCountries.ValueSeparator = " "
        '
        'cbFilterStudios
        '
        Me.cbFilterStudios.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterStudios.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterStudios.CheckOnClick = true
        Me.cbFilterStudios.DisplayWhenNothingSelected = "All"
        Me.cbFilterStudios.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cbFilterStudios.DropDownHeight = 1
        Me.cbFilterStudios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterStudios.FormattingEnabled = true
        Me.cbFilterStudios.IntegralHeight = false
        Me.cbFilterStudios.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterStudios.Mode = MC_UserControls.TriStateCheckedComboBox.OperationMode.Normal
        Me.cbFilterStudios.Name = "cbFilterStudios"
        Me.cbFilterStudios.QuickSelect = false
        Me.cbFilterStudios.Size = New System.Drawing.Size(0, 22)
        Me.cbFilterStudios.TabIndex = 265
        Me.cbFilterStudios.Tag = "1"
        Me.cbFilterStudios.ValueSeparator = " "
        '
        'lblFilterYear
        '
        Me.lblFilterYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterYear.BackColor = System.Drawing.Color.Gray
        Me.lblFilterYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterYear.ForeColor = System.Drawing.Color.White
        Me.lblFilterYear.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterYear.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterYear.Name = "lblFilterYear"
        Me.lblFilterYear.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterYear.TabIndex = 223
        Me.lblFilterYear.Text = "Year"
        Me.lblFilterYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterYear
        '
        Me.cbFilterYear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterYear.Format = "D"
        Me.cbFilterYear.InternalSelectedMax = 1R
        Me.cbFilterYear.InternalSelectedMin = 0R
        Me.cbFilterYear.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterYear.Max = 1R
        Me.cbFilterYear.Min = 0R
        Me.cbFilterYear.Mode = MC_UserControls.SelectionRangeSlider.OperatingMode.MinMax
        Me.cbFilterYear.Name = "cbFilterYear"
        Me.cbFilterYear.PointedValue = -1R
        Me.cbFilterYear.SelectedMax = 1R
        Me.cbFilterYear.SelectedMin = 0R
        Me.cbFilterYear.Size = New System.Drawing.Size(0, 21)
        Me.cbFilterYear.TabIndex = 222
        Me.cbFilterYear.Tag = "13"
        Me.cbFilterYear.Values = CType(resources.GetObject("cbFilterYear.Values"),System.Collections.Generic.List(Of Integer))
        '
        'cbFilterVotes
        '
        Me.cbFilterVotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterVotes.Format = "D"
        Me.cbFilterVotes.InternalSelectedMax = 0R
        Me.cbFilterVotes.InternalSelectedMin = 0R
        Me.cbFilterVotes.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterVotes.Max = 0R
        Me.cbFilterVotes.Min = 0R
        Me.cbFilterVotes.Mode = MC_UserControls.SelectionRangeSlider.OperatingMode.Values
        Me.cbFilterVotes.Name = "cbFilterVotes"
        Me.cbFilterVotes.PointedValue = -1R
        Me.cbFilterVotes.SelectedMax = 0R
        Me.cbFilterVotes.SelectedMin = 0R
        Me.cbFilterVotes.Size = New System.Drawing.Size(0, 21)
        Me.cbFilterVotes.TabIndex = 221
        Me.cbFilterVotes.Tag = "12"
        Me.cbFilterVotes.Values = CType(resources.GetObject("cbFilterVotes.Values"),System.Collections.Generic.List(Of Integer))
        '
        'cbFilterRuntime
        '
        Me.cbFilterRuntime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterRuntime.Format = "D"
        Me.cbFilterRuntime.InternalSelectedMax = 0R
        Me.cbFilterRuntime.InternalSelectedMin = 0R
        Me.cbFilterRuntime.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterRuntime.Max = 0R
        Me.cbFilterRuntime.Min = 0R
        Me.cbFilterRuntime.Mode = MC_UserControls.SelectionRangeSlider.OperatingMode.Values
        Me.cbFilterRuntime.Name = "cbFilterRuntime"
        Me.cbFilterRuntime.PointedValue = -1R
        Me.cbFilterRuntime.SelectedMax = 0R
        Me.cbFilterRuntime.SelectedMin = 0R
        Me.cbFilterRuntime.Size = New System.Drawing.Size(0, 21)
        Me.cbFilterRuntime.TabIndex = 221
        Me.cbFilterRuntime.Tag = "12"
        Me.cbFilterRuntime.Values = CType(resources.GetObject("cbFilterRuntime.Values"),System.Collections.Generic.List(Of Integer))
        '
        'cbFilterFolderSizes
        '
        Me.cbFilterFolderSizes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterFolderSizes.Format = "f1"
        Me.cbFilterFolderSizes.InternalSelectedMax = 0R
        Me.cbFilterFolderSizes.InternalSelectedMin = 0R
        Me.cbFilterFolderSizes.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterFolderSizes.Max = 0R
        Me.cbFilterFolderSizes.Min = 0R
        Me.cbFilterFolderSizes.Mode = MC_UserControls.SelectionRangeSlider.OperatingMode.MinMax
        Me.cbFilterFolderSizes.Name = "cbFilterFolderSizes"
        Me.cbFilterFolderSizes.PointedValue = -1R
        Me.cbFilterFolderSizes.SelectedMax = 0R
        Me.cbFilterFolderSizes.SelectedMin = 0R
        Me.cbFilterFolderSizes.Size = New System.Drawing.Size(0, 21)
        Me.cbFilterFolderSizes.TabIndex = 263
        Me.cbFilterFolderSizes.Tag = "12"
        Me.cbFilterFolderSizes.Values = CType(resources.GetObject("cbFilterFolderSizes.Values"),System.Collections.Generic.List(Of Integer))
        '
        'lblFilterFolderSizes
        '
        Me.lblFilterFolderSizes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterFolderSizes.BackColor = System.Drawing.Color.Gray
        Me.lblFilterFolderSizes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterFolderSizes.ForeColor = System.Drawing.Color.White
        Me.lblFilterFolderSizes.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterFolderSizes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterFolderSizes.Name = "lblFilterFolderSizes"
        Me.lblFilterFolderSizes.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterFolderSizes.TabIndex = 264
        Me.lblFilterFolderSizes.Text = "Folder Size"
        Me.lblFilterFolderSizes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterVotes
        '
        Me.lblFilterVotes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterVotes.BackColor = System.Drawing.Color.Gray
        Me.lblFilterVotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterVotes.ForeColor = System.Drawing.Color.White
        Me.lblFilterVotes.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterVotes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterVotes.Name = "lblFilterVotes"
        Me.lblFilterVotes.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterVotes.TabIndex = 220
        Me.lblFilterVotes.Text = "Votes"
        Me.lblFilterVotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterRuntime
        '
        Me.lblFilterRuntime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterRuntime.BackColor = System.Drawing.Color.Gray
        Me.lblFilterRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterRuntime.ForeColor = System.Drawing.Color.White
        Me.lblFilterRuntime.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterRuntime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterRuntime.Name = "lblFilterRuntime"
        Me.lblFilterRuntime.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterRuntime.TabIndex = 220
        Me.lblFilterRuntime.Text = "Runtime"
        Me.lblFilterRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterRating
        '
        Me.lblFilterRating.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterRating.BackColor = System.Drawing.Color.Gray
        Me.lblFilterRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterRating.ForeColor = System.Drawing.Color.White
        Me.lblFilterRating.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterRating.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterRating.Name = "lblFilterRating"
        Me.lblFilterRating.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterRating.TabIndex = 218
        Me.lblFilterRating.Text = "Rating"
        Me.lblFilterRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterNumAudioTracks
        '
        Me.lblFilterNumAudioTracks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterNumAudioTracks.BackColor = System.Drawing.Color.Gray
        Me.lblFilterNumAudioTracks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterNumAudioTracks.ForeColor = System.Drawing.Color.White
        Me.lblFilterNumAudioTracks.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterNumAudioTracks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterNumAudioTracks.Name = "lblFilterNumAudioTracks"
        Me.lblFilterNumAudioTracks.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterNumAudioTracks.TabIndex = 217
        Me.lblFilterNumAudioTracks.Text = "Num Audio Tracks"
        Me.lblFilterNumAudioTracks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioBitrates
        '
        Me.lblFilterAudioBitrates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioBitrates.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioBitrates.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioBitrates.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioBitrates.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterAudioBitrates.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioBitrates.Name = "lblFilterAudioBitrates"
        Me.lblFilterAudioBitrates.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterAudioBitrates.TabIndex = 215
        Me.lblFilterAudioBitrates.Text = "Audio bitrate"
        Me.lblFilterAudioBitrates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioChannels
        '
        Me.lblFilterAudioChannels.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioChannels.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioChannels.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioChannels.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioChannels.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterAudioChannels.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioChannels.Name = "lblFilterAudioChannels"
        Me.lblFilterAudioChannels.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterAudioChannels.TabIndex = 213
        Me.lblFilterAudioChannels.Text = "Audio channels"
        Me.lblFilterAudioChannels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioLanguages
        '
        Me.lblFilterAudioLanguages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioLanguages.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioLanguages.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioLanguages.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioLanguages.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterAudioLanguages.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioLanguages.Name = "lblFilterAudioLanguages"
        Me.lblFilterAudioLanguages.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterAudioLanguages.TabIndex = 211
        Me.lblFilterAudioLanguages.Text = "Language"
        Me.lblFilterAudioLanguages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioDefaultLanguages
        '
        Me.lblFilterAudioDefaultLanguages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioDefaultLanguages.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioDefaultLanguages.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioDefaultLanguages.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioDefaultLanguages.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterAudioDefaultLanguages.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioDefaultLanguages.Name = "lblFilterAudioDefaultLanguages"
        Me.lblFilterAudioDefaultLanguages.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterAudioDefaultLanguages.TabIndex = 211
        Me.lblFilterAudioDefaultLanguages.Text = "Default Language"
        Me.lblFilterAudioDefaultLanguages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterAudioCodecs
        '
        Me.lblFilterAudioCodecs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterAudioCodecs.BackColor = System.Drawing.Color.Gray
        Me.lblFilterAudioCodecs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterAudioCodecs.ForeColor = System.Drawing.Color.White
        Me.lblFilterAudioCodecs.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterAudioCodecs.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterAudioCodecs.Name = "lblFilterAudioCodecs"
        Me.lblFilterAudioCodecs.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterAudioCodecs.TabIndex = 209
        Me.lblFilterAudioCodecs.Text = "Audio codec"
        Me.lblFilterAudioCodecs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterResolution
        '
        Me.lblFilterResolution.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterResolution.BackColor = System.Drawing.Color.Gray
        Me.lblFilterResolution.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterResolution.ForeColor = System.Drawing.Color.White
        Me.lblFilterResolution.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterResolution.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterResolution.Name = "lblFilterResolution"
        Me.lblFilterResolution.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterResolution.TabIndex = 207
        Me.lblFilterResolution.Text = "Resolution"
        Me.lblFilterResolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterGeneral
        '
        Me.lblFilterGeneral.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterGeneral.BackColor = System.Drawing.Color.Gray
        Me.lblFilterGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterGeneral.ForeColor = System.Drawing.Color.White
        Me.lblFilterGeneral.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterGeneral.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterGeneral.Name = "lblFilterGeneral"
        Me.lblFilterGeneral.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterGeneral.TabIndex = 204
        Me.lblFilterGeneral.Text = "General"
        Me.lblFilterGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterActor
        '
        Me.lblFilterActor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterActor.BackColor = System.Drawing.Color.Gray
        Me.lblFilterActor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold)
        Me.lblFilterActor.ForeColor = System.Drawing.Color.White
        Me.lblFilterActor.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterActor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterActor.Name = "lblFilterActor"
        Me.lblFilterActor.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterActor.TabIndex = 202
        Me.lblFilterActor.Text = "Actor"
        Me.lblFilterActor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSet
        '
        Me.lblFilterSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterSet.BackColor = System.Drawing.Color.Gray
        Me.lblFilterSet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSet.ForeColor = System.Drawing.Color.White
        Me.lblFilterSet.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterSet.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterSet.Name = "lblFilterSet"
        Me.lblFilterSet.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterSet.TabIndex = 200
        Me.lblFilterSet.Text = "Set"
        Me.lblFilterSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSource
        '
        Me.lblFilterSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterSource.BackColor = System.Drawing.Color.Gray
        Me.lblFilterSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSource.ForeColor = System.Drawing.Color.White
        Me.lblFilterSource.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterSource.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterSource.Name = "lblFilterSource"
        Me.lblFilterSource.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterSource.TabIndex = 199
        Me.lblFilterSource.Text = "Source"
        Me.lblFilterSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterCountries
        '
        Me.lblFilterCountries.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterCountries.BackColor = System.Drawing.Color.Gray
        Me.lblFilterCountries.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterCountries.ForeColor = System.Drawing.Color.White
        Me.lblFilterCountries.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFilterCountries.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterCountries.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblFilterCountries.Name = "lblFilterCountries"
        Me.lblFilterCountries.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterCountries.TabIndex = 198
        Me.lblFilterCountries.Text = "Countries"
        Me.lblFilterCountries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterStudios
        '
        Me.lblFilterStudios.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterStudios.BackColor = System.Drawing.Color.Gray
        Me.lblFilterStudios.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterStudios.ForeColor = System.Drawing.Color.White
        Me.lblFilterStudios.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFilterStudios.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterStudios.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblFilterStudios.Name = "lblFilterStudios"
        Me.lblFilterStudios.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterStudios.TabIndex = 266
        Me.lblFilterStudios.Text = "Studios"
        Me.lblFilterStudios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterGenre
        '
        Me.lblFilterGenre.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblFilterGenre.BackColor = System.Drawing.Color.Gray
        Me.lblFilterGenre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterGenre.ForeColor = System.Drawing.Color.White
        Me.lblFilterGenre.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFilterGenre.Location = New System.Drawing.Point(4, 32767)
        Me.lblFilterGenre.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblFilterGenre.Name = "lblFilterGenre"
        Me.lblFilterGenre.Size = New System.Drawing.Size(124, 21)
        Me.lblFilterGenre.TabIndex = 198
        Me.lblFilterGenre.Text = "Genre"
        Me.lblFilterGenre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbFilterGeneral
        '
        Me.cbFilterGeneral.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterGeneral.BackColor = System.Drawing.SystemColors.Control
        Me.cbFilterGeneral.DropDownHeight = 145
        Me.cbFilterGeneral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cbFilterGeneral.IntegralHeight = false
        Me.cbFilterGeneral.Items.AddRange(New Object() {"All", "Watched", "Unwatched", "UnScraped", "Duplicates", "Missing Fanart", "Missing Poster", "Missing Plot", "Missing Trailer"})
        Me.cbFilterGeneral.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterGeneral.Margin = New System.Windows.Forms.Padding(4)
        Me.cbFilterGeneral.MaxDropDownItems = 5
        Me.cbFilterGeneral.Name = "cbFilterGeneral"
        Me.cbFilterGeneral.Size = New System.Drawing.Size(0, 21)
        Me.cbFilterGeneral.TabIndex = 205
        Me.cbFilterGeneral.Tag = "0"
        '
        'cbFilterRating
        '
        Me.cbFilterRating.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbFilterRating.Format = "f1"
        Me.cbFilterRating.InternalSelectedMax = 5R
        Me.cbFilterRating.InternalSelectedMin = 0R
        Me.cbFilterRating.Location = New System.Drawing.Point(147, 32767)
        Me.cbFilterRating.Max = 10R
        Me.cbFilterRating.Min = 0R
        Me.cbFilterRating.Mode = MC_UserControls.SelectionRangeSlider.OperatingMode.MinMax
        Me.cbFilterRating.Name = "cbFilterRating"
        Me.cbFilterRating.PointedValue = -1R
        Me.cbFilterRating.SelectedMax = 5R
        Me.cbFilterRating.SelectedMin = 0R
        Me.cbFilterRating.Size = New System.Drawing.Size(0, 21)
        Me.cbFilterRating.TabIndex = 219
        Me.cbFilterRating.Tag = "11"
        Me.cbFilterRating.Values = CType(resources.GetObject("cbFilterRating.Values"),System.Collections.Generic.List(Of Integer))
        '
        'ftvArtPicBox
        '
        Me.ftvArtPicBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ftvArtPicBox.BackColor = System.Drawing.Color.Transparent
        Me.ftvArtPicBox.Location = New System.Drawing.Point(180, 45)
        Me.ftvArtPicBox.Name = "ftvArtPicBox"
        Me.ftvArtPicBox.Size = New System.Drawing.Size(206, 224)
        Me.ftvArtPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ftvArtPicBox.TabIndex = 128
        Me.ftvArtPicBox.TabStop = false
        Me.ftvArtPicBox.Visible = false
        '
        'Label128
        '
        Me.Label128.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label128.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold)
        Me.Label128.Location = New System.Drawing.Point(98, 80)
        Me.Label128.Margin = New System.Windows.Forms.Padding(40, 40, 4, 0)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(517, 163)
        Me.Label128.TabIndex = 164
        Me.Label128.Text = resources.GetString("Label128.Text")
        Me.Label128.Visible = false
        '
        'movieGraphicInfo
        '
        Me.movieGraphicInfo.BackColor = System.Drawing.Color.Transparent
        Me.movieGraphicInfo.Location = New System.Drawing.Point(0, 0)
        Me.movieGraphicInfo.Name = "movieGraphicInfo"
        Me.movieGraphicInfo.Size = New System.Drawing.Size(63, 36)
        Me.movieGraphicInfo.TabIndex = 226
        Me.movieGraphicInfo.Visible = false
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.FanTvArtList)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Location = New System.Drawing.Point(2, 131)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(67, 165)
        Me.Panel6.TabIndex = 186
        '
        'FanTvArtList
        '
        Me.FanTvArtList.BackColor = System.Drawing.SystemColors.MenuBar
        Me.FanTvArtList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FanTvArtList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FanTvArtList.FormattingEnabled = true
        Me.FanTvArtList.Location = New System.Drawing.Point(1, 35)
        Me.FanTvArtList.Name = "FanTvArtList"
        Me.FanTvArtList.Size = New System.Drawing.Size(62, 119)
        Me.FanTvArtList.TabIndex = 1
        Me.FanTvArtList.TabStop = false
        Me.FanTvArtList.UseTabStops = false
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.Location = New System.Drawing.Point(5, 2)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 28)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Extra"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Artwork"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tlpMovies
        '
        Me.tlpMovies.ColumnCount = 11
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.03851!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.80231!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.78306!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.37612!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24!))
        Me.tlpMovies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118!))
        Me.tlpMovies.Controls.Add(Me.lbl_movCountry, 5, 8)
        Me.tlpMovies.Controls.Add(Me.premiertxt, 1, 5)
        Me.tlpMovies.Controls.Add(Me.lbl_movPremiered, 0, 5)
        Me.tlpMovies.Controls.Add(Me.btnMovieDisplay_DirectorFilter, 9, 5)
        Me.tlpMovies.Controls.Add(Me.btnMovieDisplay_CountriesFilter, 9, 8)
        Me.tlpMovies.Controls.Add(Me.tagtxt, 6, 9)
        Me.tlpMovies.Controls.Add(Me.lbl_movTags, 5, 9)
        Me.tlpMovies.Controls.Add(Me.lbl_movTop250, 2, 6)
        Me.tlpMovies.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.tlpMovies.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.tlpMovies.Controls.Add(Me.plottxt, 1, 2)
        Me.tlpMovies.Controls.Add(Me.outlinetxt, 1, 1)
        Me.tlpMovies.Controls.Add(Me.taglinetxt, 1, 3)
        Me.tlpMovies.Controls.Add(Me.lbl_movTagline, 0, 3)
        Me.tlpMovies.Controls.Add(Me.lbl_movOutline, 0, 1)
        Me.tlpMovies.Controls.Add(Me.PictureBoxActor, 10, 5)
        Me.tlpMovies.Controls.Add(Me.lbl_movPath, 0, 10)
        Me.tlpMovies.Controls.Add(Me.lbl_movSets, 0, 9)
        Me.tlpMovies.Controls.Add(Me.lbl_movRuntime, 0, 8)
        Me.tlpMovies.Controls.Add(Me.lbl_movRating, 0, 7)
        Me.tlpMovies.Controls.Add(Me.lbl_movCert, 0, 6)
        Me.tlpMovies.Controls.Add(Me.lbl_movGenre, 0, 4)
        Me.tlpMovies.Controls.Add(Me.lbl_movStars, 7, 3)
        Me.tlpMovies.Controls.Add(Me.btnMovieDisplay_ActorFilter, 9, 4)
        Me.tlpMovies.Controls.Add(Me.cbMovieDisplay_Actor, 6, 4)
        Me.tlpMovies.Controls.Add(Me.directortxt, 6, 5)
        Me.tlpMovies.Controls.Add(Me.creditstxt, 6, 6)
        Me.tlpMovies.Controls.Add(Me.studiotxt, 6, 7)
        Me.tlpMovies.Controls.Add(Me.cbMovieDisplay_Source, 3, 5)
        Me.tlpMovies.Controls.Add(Me.lbl_movSource, 2, 5)
        Me.tlpMovies.Controls.Add(Me.lbl_movStudio, 5, 7)
        Me.tlpMovies.Controls.Add(Me.lbl_movCredits, 5, 6)
        Me.tlpMovies.Controls.Add(Me.lbl_movDirector, 5, 5)
        Me.tlpMovies.Controls.Add(Me.lbl_movActors, 5, 4)
        Me.tlpMovies.Controls.Add(Me.btnMovieDisplay_SetFilter, 4, 9)
        Me.tlpMovies.Controls.Add(Me.votestxt, 3, 7)
        Me.tlpMovies.Controls.Add(Me.lbl_movImdbid, 2, 8)
        Me.tlpMovies.Controls.Add(Me.lbl_movVotes, 2, 7)
        Me.tlpMovies.Controls.Add(Me.pathtxt, 1, 10)
        Me.tlpMovies.Controls.Add(Me.cbMovieDisplay_MovieSet, 1, 9)
        Me.tlpMovies.Controls.Add(Me.runtimetxt, 1, 8)
        Me.tlpMovies.Controls.Add(Me.ratingtxt, 1, 7)
        Me.tlpMovies.Controls.Add(Me.certtxt, 1, 6)
        Me.tlpMovies.Controls.Add(Me.genretxt, 1, 4)
        Me.tlpMovies.Controls.Add(Me.txtStars, 8, 3)
        Me.tlpMovies.Controls.Add(Me.roletxt, 10, 4)
        Me.tlpMovies.Controls.Add(Me.top250txt, 3, 6)
        Me.tlpMovies.Controls.Add(Me.countrytxt, 6, 8)
        Me.tlpMovies.Controls.Add(Me.tlpMovieButtons, 5, 10)
        Me.tlpMovies.Controls.Add(Me.TableLayoutPanel31, 0, 2)
        Me.tlpMovies.Controls.Add(Me.cbUsrRated, 3, 8)
        Me.tlpMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMovies.Location = New System.Drawing.Point(0, 0)
        Me.tlpMovies.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpMovies.Name = "tlpMovies"
        Me.tlpMovies.RowCount = 11
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.tlpMovies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.tlpMovies.Size = New System.Drawing.Size(701, 620)
        Me.tlpMovies.TabIndex = 174
        '
        'lbl_movCountry
        '
        Me.lbl_movCountry.BackColor = System.Drawing.Color.Gray
        Me.lbl_movCountry.ContextMenuStrip = Me.rcmenu
        Me.lbl_movCountry.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movCountry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movCountry.ForeColor = System.Drawing.Color.White
        Me.lbl_movCountry.Location = New System.Drawing.Point(309, 539)
        Me.lbl_movCountry.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movCountry.Name = "lbl_movCountry"
        Me.lbl_movCountry.Size = New System.Drawing.Size(60, 20)
        Me.lbl_movCountry.TabIndex = 232
        Me.lbl_movCountry.Text = "Country"
        Me.lbl_movCountry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rcmenu
        '
        Me.rcmenu.Name = "rcmenu"
        Me.rcmenu.Size = New System.Drawing.Size(61, 4)
        '
        'premiertxt
        '
        Me.premiertxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.premiertxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.premiertxt.Location = New System.Drawing.Point(70, 461)
        Me.premiertxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.premiertxt.Name = "premiertxt"
        Me.premiertxt.Size = New System.Drawing.Size(75, 20)
        Me.premiertxt.TabIndex = 231
        '
        'lbl_movPremiered
        '
        Me.lbl_movPremiered.BackColor = System.Drawing.Color.Gray
        Me.lbl_movPremiered.ContextMenuStrip = Me.rcmenu
        Me.lbl_movPremiered.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movPremiered.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movPremiered.ForeColor = System.Drawing.Color.White
        Me.lbl_movPremiered.Location = New System.Drawing.Point(4, 461)
        Me.lbl_movPremiered.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movPremiered.Name = "lbl_movPremiered"
        Me.lbl_movPremiered.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.lbl_movPremiered.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movPremiered.TabIndex = 230
        Me.lbl_movPremiered.Text = "Premiered"
        Me.lbl_movPremiered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMovieDisplay_DirectorFilter
        '
        Me.btnMovieDisplay_DirectorFilter.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnMovieDisplay_DirectorFilter.BackgroundImage = CType(resources.GetObject("btnMovieDisplay_DirectorFilter.BackgroundImage"),System.Drawing.Image)
        Me.btnMovieDisplay_DirectorFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovieDisplay_DirectorFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMovieDisplay_DirectorFilter.Location = New System.Drawing.Point(557, 460)
        Me.btnMovieDisplay_DirectorFilter.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.btnMovieDisplay_DirectorFilter.Name = "btnMovieDisplay_DirectorFilter"
        Me.btnMovieDisplay_DirectorFilter.Size = New System.Drawing.Size(24, 23)
        Me.btnMovieDisplay_DirectorFilter.TabIndex = 229
        Me.ToolTip2.SetToolTip(Me.btnMovieDisplay_DirectorFilter, "Filter by movies by this director")
        Me.btnMovieDisplay_DirectorFilter.UseVisualStyleBackColor = false
        '
        'btnMovieDisplay_CountriesFilter
        '
        Me.btnMovieDisplay_CountriesFilter.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnMovieDisplay_CountriesFilter.BackgroundImage = CType(resources.GetObject("btnMovieDisplay_CountriesFilter.BackgroundImage"),System.Drawing.Image)
        Me.btnMovieDisplay_CountriesFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovieDisplay_CountriesFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMovieDisplay_CountriesFilter.Location = New System.Drawing.Point(557, 538)
        Me.btnMovieDisplay_CountriesFilter.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.btnMovieDisplay_CountriesFilter.Name = "btnMovieDisplay_CountriesFilter"
        Me.btnMovieDisplay_CountriesFilter.Size = New System.Drawing.Size(24, 23)
        Me.btnMovieDisplay_CountriesFilter.TabIndex = 229
        Me.ToolTip2.SetToolTip(Me.btnMovieDisplay_CountriesFilter, "Filter by movies by this country")
        Me.btnMovieDisplay_CountriesFilter.UseVisualStyleBackColor = false
        '
        'lbl_movTags
        '
        Me.lbl_movTags.BackColor = System.Drawing.Color.Gray
        Me.lbl_movTags.ContextMenuStrip = Me.rcmenu
        Me.lbl_movTags.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movTags.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movTags.ForeColor = System.Drawing.Color.White
        Me.lbl_movTags.Location = New System.Drawing.Point(309, 565)
        Me.lbl_movTags.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movTags.Name = "lbl_movTags"
        Me.lbl_movTags.Size = New System.Drawing.Size(60, 21)
        Me.lbl_movTags.TabIndex = 227
        Me.lbl_movTags.Text = "Tag(s)"
        Me.lbl_movTags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movTop250
        '
        Me.lbl_movTop250.BackColor = System.Drawing.Color.Gray
        Me.lbl_movTop250.ContextMenuStrip = Me.rcmenu
        Me.lbl_movTop250.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movTop250.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movTop250.ForeColor = System.Drawing.Color.White
        Me.lbl_movTop250.Location = New System.Drawing.Point(150, 487)
        Me.lbl_movTop250.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movTop250.Name = "lbl_movTop250"
        Me.lbl_movTop250.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movTop250.TabIndex = 225
        Me.lbl_movTop250.Text = "Top 250"
        Me.lbl_movTop250.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.tlpMovies.SetColumnSpan(Me.TableLayoutPanel3, 10)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.SplitContainer2, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(70, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(631, 297)
        Me.TableLayoutPanel3.TabIndex = 224
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label75, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TextBox34, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.titletxt, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 3)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(631, 29)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.Gray
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label75.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold)
        Me.Label75.ForeColor = System.Drawing.Color.White
        Me.Label75.Location = New System.Drawing.Point(385, 0)
        Me.Label75.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(55, 20)
        Me.Label75.TabIndex = 154
        Me.Label75.Text = "Sort"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox34
        '
        Me.TextBox34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.HelpProvider1.SetHelpKeyword(Me.TextBox34, "210")
        Me.HelpProvider1.SetHelpNavigator(Me.TextBox34, System.Windows.Forms.HelpNavigator.TopicId)
        Me.HelpProvider1.SetHelpString(Me.TextBox34, "")
        Me.TextBox34.Location = New System.Drawing.Point(440, 0)
        Me.TextBox34.Margin = New System.Windows.Forms.Padding(0, 0, 1, 4)
        Me.TextBox34.Name = "TextBox34"
        Me.HelpProvider1.SetShowHelp(Me.TextBox34, true)
        Me.TextBox34.Size = New System.Drawing.Size(190, 20)
        Me.TextBox34.TabIndex = 153
        '
        'titletxt
        '
        Me.titletxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.titletxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.titletxt.FormattingEnabled = true
        Me.titletxt.Location = New System.Drawing.Point(0, 0)
        Me.titletxt.Margin = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.titletxt.Name = "titletxt"
        Me.titletxt.Size = New System.Drawing.Size(377, 28)
        Me.titletxt.TabIndex = 161
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 35)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer2.Panel1.Controls.Add(Me.PbMovieFanArt)
        Me.SplitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.PbMoviePoster)
        Me.SplitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer2.Size = New System.Drawing.Size(630, 262)
        Me.SplitContainer2.SplitterDistance = 296
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 151
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnMovSave, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnMovRescrape, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBoxMutisave, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(70, 299)
        Me.TableLayoutPanel2.TabIndex = 223
        '
        'TextBoxMutisave
        '
        Me.TextBoxMutisave.BackColor = System.Drawing.Color.Maroon
        Me.TextBoxMutisave.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxMutisave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TextBoxMutisave.ForeColor = System.Drawing.Color.White
        Me.TextBoxMutisave.Location = New System.Drawing.Point(4, 130)
        Me.TextBoxMutisave.Margin = New System.Windows.Forms.Padding(4, 4, 6, 4)
        Me.TextBoxMutisave.Multiline = true
        Me.TextBoxMutisave.Name = "TextBoxMutisave"
        Me.TextBoxMutisave.Size = New System.Drawing.Size(65, 55)
        Me.TextBoxMutisave.TabIndex = 181
        Me.TextBoxMutisave.Text = "Multisave Mode Activated"
        Me.TextBoxMutisave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBoxMutisave.Visible = false
        '
        'plottxt
        '
        Me.tlpMovies.SetColumnSpan(Me.plottxt, 10)
        Me.plottxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plottxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.plottxt.Location = New System.Drawing.Point(70, 345)
        Me.plottxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.plottxt.Multiline = true
        Me.plottxt.Name = "plottxt"
        Me.plottxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.plottxt.Size = New System.Drawing.Size(630, 60)
        Me.plottxt.TabIndex = 222
        '
        'outlinetxt
        '
        Me.tlpMovies.SetColumnSpan(Me.outlinetxt, 10)
        Me.outlinetxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.outlinetxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.outlinetxt.Location = New System.Drawing.Point(70, 303)
        Me.outlinetxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.outlinetxt.Multiline = true
        Me.outlinetxt.Name = "outlinetxt"
        Me.outlinetxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.outlinetxt.Size = New System.Drawing.Size(630, 38)
        Me.outlinetxt.TabIndex = 221
        '
        'taglinetxt
        '
        Me.tlpMovies.SetColumnSpan(Me.taglinetxt, 6)
        Me.taglinetxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.taglinetxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.taglinetxt.Location = New System.Drawing.Point(70, 409)
        Me.taglinetxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.taglinetxt.Name = "taglinetxt"
        Me.taglinetxt.Size = New System.Drawing.Size(325, 20)
        Me.taglinetxt.TabIndex = 220
        '
        'lbl_movTagline
        '
        Me.lbl_movTagline.BackColor = System.Drawing.Color.Gray
        Me.lbl_movTagline.ContextMenuStrip = Me.rcmenu
        Me.lbl_movTagline.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movTagline.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movTagline.ForeColor = System.Drawing.Color.White
        Me.lbl_movTagline.Location = New System.Drawing.Point(4, 409)
        Me.lbl_movTagline.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movTagline.Name = "lbl_movTagline"
        Me.lbl_movTagline.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movTagline.TabIndex = 219
        Me.lbl_movTagline.Text = "Tagline"
        Me.lbl_movTagline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movOutline
        '
        Me.lbl_movOutline.BackColor = System.Drawing.Color.Gray
        Me.lbl_movOutline.ContextMenuStrip = Me.rcmenu
        Me.lbl_movOutline.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movOutline.ForeColor = System.Drawing.Color.White
        Me.lbl_movOutline.Location = New System.Drawing.Point(4, 314)
        Me.lbl_movOutline.Margin = New System.Windows.Forms.Padding(4, 15, 0, 0)
        Me.lbl_movOutline.Name = "lbl_movOutline"
        Me.lbl_movOutline.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movOutline.TabIndex = 218
        Me.lbl_movOutline.Text = "Outline"
        Me.lbl_movOutline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBoxActor
        '
        Me.PictureBoxActor.BackColor = System.Drawing.SystemColors.ControlLight
        Me.PictureBoxActor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxActor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxActor.Location = New System.Drawing.Point(585, 461)
        Me.PictureBoxActor.Margin = New System.Windows.Forms.Padding(4, 4, 1, 4)
        Me.PictureBoxActor.Name = "PictureBoxActor"
        Me.tlpMovies.SetRowSpan(Me.PictureBoxActor, 6)
        Me.PictureBoxActor.Size = New System.Drawing.Size(115, 155)
        Me.PictureBoxActor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxActor.TabIndex = 216
        Me.PictureBoxActor.TabStop = false
        '
        'lbl_movPath
        '
        Me.lbl_movPath.BackColor = System.Drawing.Color.Gray
        Me.lbl_movPath.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movPath.ForeColor = System.Drawing.Color.White
        Me.lbl_movPath.Location = New System.Drawing.Point(4, 591)
        Me.lbl_movPath.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movPath.Name = "lbl_movPath"
        Me.lbl_movPath.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movPath.TabIndex = 214
        Me.lbl_movPath.Text = "Path"
        Me.lbl_movPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movSets
        '
        Me.lbl_movSets.BackColor = System.Drawing.Color.Gray
        Me.lbl_movSets.ContextMenuStrip = Me.rcmenu
        Me.lbl_movSets.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movSets.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movSets.ForeColor = System.Drawing.Color.White
        Me.lbl_movSets.Location = New System.Drawing.Point(4, 565)
        Me.lbl_movSets.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movSets.Name = "lbl_movSets"
        Me.lbl_movSets.Size = New System.Drawing.Size(66, 21)
        Me.lbl_movSets.TabIndex = 213
        Me.lbl_movSets.Text = "Set"
        Me.lbl_movSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movRuntime
        '
        Me.lbl_movRuntime.BackColor = System.Drawing.Color.Gray
        Me.lbl_movRuntime.ContextMenuStrip = Me.rcmenu
        Me.lbl_movRuntime.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movRuntime.ForeColor = System.Drawing.Color.White
        Me.lbl_movRuntime.Location = New System.Drawing.Point(4, 539)
        Me.lbl_movRuntime.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movRuntime.Name = "lbl_movRuntime"
        Me.lbl_movRuntime.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movRuntime.TabIndex = 212
        Me.lbl_movRuntime.Text = "Runtime"
        Me.lbl_movRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip2.SetToolTip(Me.lbl_movRuntime, "Click to alternate between"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Scraper or User-defined Runtime,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"or Media File Durat"& _ 
        "ion.")
        '
        'lbl_movRating
        '
        Me.lbl_movRating.BackColor = System.Drawing.Color.Gray
        Me.lbl_movRating.ContextMenuStrip = Me.rcmenu
        Me.lbl_movRating.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movRating.ForeColor = System.Drawing.Color.White
        Me.lbl_movRating.Location = New System.Drawing.Point(4, 513)
        Me.lbl_movRating.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movRating.Name = "lbl_movRating"
        Me.lbl_movRating.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movRating.TabIndex = 211
        Me.lbl_movRating.Text = "Rating"
        Me.lbl_movRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movCert
        '
        Me.lbl_movCert.BackColor = System.Drawing.Color.Gray
        Me.lbl_movCert.ContextMenuStrip = Me.rcmenu
        Me.lbl_movCert.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movCert.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movCert.ForeColor = System.Drawing.Color.White
        Me.lbl_movCert.Location = New System.Drawing.Point(4, 487)
        Me.lbl_movCert.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movCert.Name = "lbl_movCert"
        Me.lbl_movCert.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movCert.TabIndex = 210
        Me.lbl_movCert.Text = "Cert"
        Me.lbl_movCert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movGenre
        '
        Me.lbl_movGenre.BackColor = System.Drawing.Color.Gray
        Me.lbl_movGenre.ContextMenuStrip = Me.rcmenu
        Me.lbl_movGenre.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movGenre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movGenre.ForeColor = System.Drawing.Color.White
        Me.lbl_movGenre.Location = New System.Drawing.Point(4, 436)
        Me.lbl_movGenre.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movGenre.Name = "lbl_movGenre"
        Me.lbl_movGenre.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movGenre.TabIndex = 209
        Me.lbl_movGenre.Text = "Genre"
        Me.lbl_movGenre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movStars
        '
        Me.lbl_movStars.BackColor = System.Drawing.Color.Gray
        Me.lbl_movStars.ContextMenuStrip = Me.rcmenu
        Me.lbl_movStars.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movStars.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movStars.ForeColor = System.Drawing.Color.White
        Me.lbl_movStars.Location = New System.Drawing.Point(400, 409)
        Me.lbl_movStars.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movStars.Name = "lbl_movStars"
        Me.lbl_movStars.Size = New System.Drawing.Size(72, 20)
        Me.lbl_movStars.TabIndex = 208
        Me.lbl_movStars.Text = "Stars"
        Me.lbl_movStars.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMovieDisplay_ActorFilter
        '
        Me.btnMovieDisplay_ActorFilter.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnMovieDisplay_ActorFilter.BackgroundImage = CType(resources.GetObject("btnMovieDisplay_ActorFilter.BackgroundImage"),System.Drawing.Image)
        Me.btnMovieDisplay_ActorFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovieDisplay_ActorFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMovieDisplay_ActorFilter.Location = New System.Drawing.Point(557, 435)
        Me.btnMovieDisplay_ActorFilter.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.btnMovieDisplay_ActorFilter.Name = "btnMovieDisplay_ActorFilter"
        Me.btnMovieDisplay_ActorFilter.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieDisplay_ActorFilter.TabIndex = 207
        Me.ToolTip2.SetToolTip(Me.btnMovieDisplay_ActorFilter, "Filter movies by this actor")
        Me.btnMovieDisplay_ActorFilter.UseVisualStyleBackColor = false
        '
        'cbMovieDisplay_Actor
        '
        Me.cbMovieDisplay_Actor.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tlpMovies.SetColumnSpan(Me.cbMovieDisplay_Actor, 3)
        Me.cbMovieDisplay_Actor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieDisplay_Actor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieDisplay_Actor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieDisplay_Actor.FormattingEnabled = true
        Me.cbMovieDisplay_Actor.Location = New System.Drawing.Point(369, 436)
        Me.cbMovieDisplay_Actor.Margin = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.cbMovieDisplay_Actor.MaxDropDownItems = 25
        Me.cbMovieDisplay_Actor.Name = "cbMovieDisplay_Actor"
        Me.cbMovieDisplay_Actor.Size = New System.Drawing.Size(188, 21)
        Me.cbMovieDisplay_Actor.TabIndex = 206
        '
        'directortxt
        '
        Me.tlpMovies.SetColumnSpan(Me.directortxt, 3)
        Me.directortxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.directortxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.directortxt.Location = New System.Drawing.Point(369, 461)
        Me.directortxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.directortxt.Name = "directortxt"
        Me.directortxt.Size = New System.Drawing.Size(187, 20)
        Me.directortxt.TabIndex = 205
        '
        'creditstxt
        '
        Me.tlpMovies.SetColumnSpan(Me.creditstxt, 4)
        Me.creditstxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.creditstxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.creditstxt.Location = New System.Drawing.Point(369, 487)
        Me.creditstxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.creditstxt.Name = "creditstxt"
        Me.creditstxt.Size = New System.Drawing.Size(211, 20)
        Me.creditstxt.TabIndex = 204
        '
        'studiotxt
        '
        Me.tlpMovies.SetColumnSpan(Me.studiotxt, 4)
        Me.studiotxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.studiotxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.studiotxt.Location = New System.Drawing.Point(369, 513)
        Me.studiotxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 4)
        Me.studiotxt.Name = "studiotxt"
        Me.studiotxt.Size = New System.Drawing.Size(211, 20)
        Me.studiotxt.TabIndex = 203
        '
        'cbMovieDisplay_Source
        '
        Me.tlpMovies.SetColumnSpan(Me.cbMovieDisplay_Source, 2)
        Me.cbMovieDisplay_Source.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieDisplay_Source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieDisplay_Source.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbMovieDisplay_Source.FormattingEnabled = true
        Me.cbMovieDisplay_Source.Location = New System.Drawing.Point(216, 461)
        Me.cbMovieDisplay_Source.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.cbMovieDisplay_Source.MaxDropDownItems = 25
        Me.cbMovieDisplay_Source.Name = "cbMovieDisplay_Source"
        Me.cbMovieDisplay_Source.Size = New System.Drawing.Size(88, 21)
        Me.cbMovieDisplay_Source.Sorted = true
        Me.cbMovieDisplay_Source.TabIndex = 202
        Me.ToolTip2.SetToolTip(Me.cbMovieDisplay_Source, "Source")
        '
        'lbl_movSource
        '
        Me.lbl_movSource.BackColor = System.Drawing.Color.Gray
        Me.lbl_movSource.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movSource.ForeColor = System.Drawing.Color.White
        Me.lbl_movSource.Location = New System.Drawing.Point(150, 461)
        Me.lbl_movSource.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movSource.Name = "lbl_movSource"
        Me.lbl_movSource.Size = New System.Drawing.Size(66, 21)
        Me.lbl_movSource.TabIndex = 198
        Me.lbl_movSource.Text = "Source"
        Me.lbl_movSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movStudio
        '
        Me.lbl_movStudio.BackColor = System.Drawing.Color.Gray
        Me.lbl_movStudio.ContextMenuStrip = Me.rcmenu
        Me.lbl_movStudio.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movStudio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movStudio.ForeColor = System.Drawing.Color.White
        Me.lbl_movStudio.Location = New System.Drawing.Point(309, 513)
        Me.lbl_movStudio.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movStudio.Name = "lbl_movStudio"
        Me.lbl_movStudio.Size = New System.Drawing.Size(60, 20)
        Me.lbl_movStudio.TabIndex = 197
        Me.lbl_movStudio.Text = "Studio"
        Me.lbl_movStudio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movCredits
        '
        Me.lbl_movCredits.BackColor = System.Drawing.Color.Gray
        Me.lbl_movCredits.ContextMenuStrip = Me.rcmenu
        Me.lbl_movCredits.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movCredits.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movCredits.ForeColor = System.Drawing.Color.White
        Me.lbl_movCredits.Location = New System.Drawing.Point(309, 487)
        Me.lbl_movCredits.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movCredits.Name = "lbl_movCredits"
        Me.lbl_movCredits.Size = New System.Drawing.Size(60, 20)
        Me.lbl_movCredits.TabIndex = 196
        Me.lbl_movCredits.Text = "Writers"
        Me.lbl_movCredits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movDirector
        '
        Me.lbl_movDirector.BackColor = System.Drawing.Color.Gray
        Me.lbl_movDirector.ContextMenuStrip = Me.rcmenu
        Me.lbl_movDirector.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movDirector.ForeColor = System.Drawing.Color.White
        Me.lbl_movDirector.Location = New System.Drawing.Point(309, 461)
        Me.lbl_movDirector.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movDirector.Name = "lbl_movDirector"
        Me.lbl_movDirector.Size = New System.Drawing.Size(60, 20)
        Me.lbl_movDirector.TabIndex = 195
        Me.lbl_movDirector.Text = "Director"
        Me.lbl_movDirector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_movActors
        '
        Me.lbl_movActors.BackColor = System.Drawing.Color.Gray
        Me.lbl_movActors.ContextMenuStrip = Me.rcmenu
        Me.lbl_movActors.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold)
        Me.lbl_movActors.ForeColor = System.Drawing.Color.White
        Me.lbl_movActors.Location = New System.Drawing.Point(309, 436)
        Me.lbl_movActors.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movActors.Name = "lbl_movActors"
        Me.lbl_movActors.Size = New System.Drawing.Size(60, 21)
        Me.lbl_movActors.TabIndex = 194
        Me.lbl_movActors.Text = "Actors"
        Me.lbl_movActors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMovieDisplay_SetFilter
        '
        Me.btnMovieDisplay_SetFilter.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnMovieDisplay_SetFilter.BackgroundImage = CType(resources.GetObject("btnMovieDisplay_SetFilter.BackgroundImage"),System.Drawing.Image)
        Me.btnMovieDisplay_SetFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovieDisplay_SetFilter.Location = New System.Drawing.Point(282, 564)
        Me.btnMovieDisplay_SetFilter.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.btnMovieDisplay_SetFilter.Name = "btnMovieDisplay_SetFilter"
        Me.btnMovieDisplay_SetFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieDisplay_SetFilter.TabIndex = 193
        Me.ToolTip2.SetToolTip(Me.btnMovieDisplay_SetFilter, "List all the movies belonging to this Set")
        Me.btnMovieDisplay_SetFilter.UseVisualStyleBackColor = false
        '
        'votestxt
        '
        Me.tlpMovies.SetColumnSpan(Me.votestxt, 2)
        Me.votestxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.votestxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.votestxt.Location = New System.Drawing.Point(216, 513)
        Me.votestxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.votestxt.Name = "votestxt"
        Me.votestxt.Size = New System.Drawing.Size(88, 20)
        Me.votestxt.TabIndex = 191
        '
        'lbl_movImdbid
        '
        Me.lbl_movImdbid.BackColor = System.Drawing.Color.Gray
        Me.lbl_movImdbid.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movImdbid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movImdbid.ForeColor = System.Drawing.Color.White
        Me.lbl_movImdbid.Location = New System.Drawing.Point(150, 539)
        Me.lbl_movImdbid.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movImdbid.Name = "lbl_movImdbid"
        Me.lbl_movImdbid.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movImdbid.TabIndex = 190
        Me.lbl_movImdbid.Text = "Usr Rate"
        Me.lbl_movImdbid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_movVotes
        '
        Me.lbl_movVotes.BackColor = System.Drawing.Color.Gray
        Me.lbl_movVotes.ContextMenuStrip = Me.rcmenu
        Me.lbl_movVotes.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movVotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movVotes.ForeColor = System.Drawing.Color.White
        Me.lbl_movVotes.Location = New System.Drawing.Point(150, 513)
        Me.lbl_movVotes.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movVotes.Name = "lbl_movVotes"
        Me.lbl_movVotes.Size = New System.Drawing.Size(66, 20)
        Me.lbl_movVotes.TabIndex = 189
        Me.lbl_movVotes.Text = "Votes"
        Me.lbl_movVotes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pathtxt
        '
        Me.pathtxt.BackColor = System.Drawing.SystemColors.Window
        Me.tlpMovies.SetColumnSpan(Me.pathtxt, 4)
        Me.pathtxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pathtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pathtxt.Location = New System.Drawing.Point(70, 591)
        Me.pathtxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.pathtxt.Name = "pathtxt"
        Me.pathtxt.ReadOnly = true
        Me.pathtxt.Size = New System.Drawing.Size(234, 20)
        Me.pathtxt.TabIndex = 188
        '
        'cbMovieDisplay_MovieSet
        '
        Me.cbMovieDisplay_MovieSet.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tlpMovies.SetColumnSpan(Me.cbMovieDisplay_MovieSet, 3)
        Me.cbMovieDisplay_MovieSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieDisplay_MovieSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieDisplay_MovieSet.DropDownWidth = 158
        Me.cbMovieDisplay_MovieSet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cbMovieDisplay_MovieSet.FormattingEnabled = true
        Me.cbMovieDisplay_MovieSet.Location = New System.Drawing.Point(70, 565)
        Me.cbMovieDisplay_MovieSet.Margin = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.cbMovieDisplay_MovieSet.MaxDropDownItems = 25
        Me.cbMovieDisplay_MovieSet.Name = "cbMovieDisplay_MovieSet"
        Me.cbMovieDisplay_MovieSet.Size = New System.Drawing.Size(212, 21)
        Me.cbMovieDisplay_MovieSet.TabIndex = 187
        '
        'runtimetxt
        '
        Me.runtimetxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.runtimetxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.runtimetxt.Location = New System.Drawing.Point(70, 539)
        Me.runtimetxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.runtimetxt.Name = "runtimetxt"
        Me.runtimetxt.Size = New System.Drawing.Size(75, 20)
        Me.runtimetxt.TabIndex = 173
        '
        'ratingtxt
        '
        Me.ratingtxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ratingtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ratingtxt.Location = New System.Drawing.Point(70, 513)
        Me.ratingtxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.ratingtxt.Name = "ratingtxt"
        Me.ratingtxt.Size = New System.Drawing.Size(75, 20)
        Me.ratingtxt.TabIndex = 172
        '
        'certtxt
        '
        Me.certtxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.certtxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.certtxt.Location = New System.Drawing.Point(70, 487)
        Me.certtxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.certtxt.Name = "certtxt"
        Me.certtxt.Size = New System.Drawing.Size(75, 20)
        Me.certtxt.TabIndex = 171
        '
        'genretxt
        '
        Me.tlpMovies.SetColumnSpan(Me.genretxt, 4)
        Me.genretxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.genretxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.genretxt.Location = New System.Drawing.Point(70, 436)
        Me.genretxt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.genretxt.Name = "genretxt"
        Me.genretxt.Size = New System.Drawing.Size(234, 20)
        Me.genretxt.TabIndex = 170
        '
        'txtStars
        '
        Me.tlpMovies.SetColumnSpan(Me.txtStars, 3)
        Me.txtStars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStars.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtStars.Location = New System.Drawing.Point(472, 409)
        Me.txtStars.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.txtStars.Name = "txtStars"
        Me.txtStars.Size = New System.Drawing.Size(228, 20)
        Me.txtStars.TabIndex = 168
        '
        'top250txt
        '
        Me.tlpMovies.SetColumnSpan(Me.top250txt, 2)
        Me.top250txt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.top250txt.Location = New System.Drawing.Point(216, 487)
        Me.top250txt.Margin = New System.Windows.Forms.Padding(0, 4, 1, 0)
        Me.top250txt.Name = "top250txt"
        Me.top250txt.Size = New System.Drawing.Size(88, 20)
        Me.top250txt.TabIndex = 226
        '
        'countrytxt
        '
        Me.tlpMovies.SetColumnSpan(Me.countrytxt, 3)
        Me.countrytxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.countrytxt.Location = New System.Drawing.Point(372, 538)
        Me.countrytxt.Name = "countrytxt"
        Me.countrytxt.Size = New System.Drawing.Size(182, 20)
        Me.countrytxt.TabIndex = 233
        '
        'tlpMovieButtons
        '
        Me.tlpMovieButtons.ColumnCount = 5
        Me.tlpMovies.SetColumnSpan(Me.tlpMovieButtons, 5)
        Me.tlpMovieButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMovieButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tlpMovieButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMovieButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tlpMovieButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMovieButtons.Controls.Add(Me.ButtonTrailer, 0, 0)
        Me.tlpMovieButtons.Controls.Add(Me.btnPlayMovie, 2, 0)
        Me.tlpMovieButtons.Controls.Add(Me.btnMovWatched, 4, 0)
        Me.tlpMovieButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMovieButtons.Location = New System.Drawing.Point(306, 588)
        Me.tlpMovieButtons.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpMovieButtons.Name = "tlpMovieButtons"
        Me.tlpMovieButtons.RowCount = 1
        Me.tlpMovieButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpMovieButtons.Size = New System.Drawing.Size(274, 31)
        Me.tlpMovieButtons.TabIndex = 234
        '
        'ButtonTrailer
        '
        Me.ButtonTrailer.BackColor = System.Drawing.Color.Transparent
        Me.ButtonTrailer.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonTrailer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonTrailer.Location = New System.Drawing.Point(0, 0)
        Me.ButtonTrailer.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonTrailer.Name = "ButtonTrailer"
        Me.ButtonTrailer.Size = New System.Drawing.Size(127, 31)
        Me.ButtonTrailer.TabIndex = 199
        Me.ButtonTrailer.Text = "Download Trailer"
        Me.ButtonTrailer.UseVisualStyleBackColor = false
        '
        'btnPlayMovie
        '
        Me.btnPlayMovie.BackColor = System.Drawing.Color.Transparent
        Me.btnPlayMovie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPlayMovie.Font = New System.Drawing.Font("Microsoft Sans Serif", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnPlayMovie.Image = Global.Media_Companion.My.Resources.Resources.Movie
        Me.btnPlayMovie.Location = New System.Drawing.Point(128, 0)
        Me.btnPlayMovie.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPlayMovie.Name = "btnPlayMovie"
        Me.btnPlayMovie.Size = New System.Drawing.Size(72, 31)
        Me.btnPlayMovie.TabIndex = 200
        Me.btnPlayMovie.Text = "Play"
        Me.btnPlayMovie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPlayMovie.UseVisualStyleBackColor = false
        '
        'btnMovWatched
        '
        Me.btnMovWatched.AutoSize = true
        Me.btnMovWatched.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnMovWatched.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWatched.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMovWatched.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovWatched.Location = New System.Drawing.Point(202, 0)
        Me.btnMovWatched.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMovWatched.Name = "btnMovWatched"
        Me.btnMovWatched.Size = New System.Drawing.Size(72, 31)
        Me.btnMovWatched.TabIndex = 201
        Me.btnMovWatched.Text = "Un&watched"
        Me.btnMovWatched.UseVisualStyleBackColor = false
        '
        'TableLayoutPanel31
        '
        Me.TableLayoutPanel31.ColumnCount = 1
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel31.Controls.Add(Me.lbl_movPlot, 0, 0)
        Me.TableLayoutPanel31.Controls.Add(Me.btnMovSelectPlot, 0, 1)
        Me.TableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel31.Location = New System.Drawing.Point(3, 344)
        Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
        Me.TableLayoutPanel31.RowCount = 2
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel31.Size = New System.Drawing.Size(64, 58)
        Me.TableLayoutPanel31.TabIndex = 235
        '
        'lbl_movPlot
        '
        Me.lbl_movPlot.BackColor = System.Drawing.Color.Gray
        Me.lbl_movPlot.ContextMenuStrip = Me.rcmenu
        Me.lbl_movPlot.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_movPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movPlot.ForeColor = System.Drawing.Color.White
        Me.lbl_movPlot.Location = New System.Drawing.Point(4, 4)
        Me.lbl_movPlot.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lbl_movPlot.Name = "lbl_movPlot"
        Me.lbl_movPlot.Size = New System.Drawing.Size(60, 20)
        Me.lbl_movPlot.TabIndex = 217
        Me.lbl_movPlot.Text = "Plot"
        Me.lbl_movPlot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMovSelectPlot
        '
        Me.btnMovSelectPlot.Location = New System.Drawing.Point(3, 32)
        Me.btnMovSelectPlot.Name = "btnMovSelectPlot"
        Me.btnMovSelectPlot.Size = New System.Drawing.Size(58, 23)
        Me.btnMovSelectPlot.TabIndex = 218
        Me.btnMovSelectPlot.Text = "Select"
        Me.ToolTip2.SetToolTip(Me.btnMovSelectPlot, "Choose from a list of plots found"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"on IMDB and TMDB")
        Me.btnMovSelectPlot.UseVisualStyleBackColor = true
        '
        'cbUsrRated
        '
        Me.tlpMovies.SetColumnSpan(Me.cbUsrRated, 2)
        Me.cbUsrRated.FormattingEnabled = true
        Me.cbUsrRated.Items.AddRange(New Object() {"None", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cbUsrRated.Location = New System.Drawing.Point(219, 538)
        Me.cbUsrRated.Name = "cbUsrRated"
        Me.cbUsrRated.Size = New System.Drawing.Size(83, 21)
        Me.cbUsrRated.TabIndex = 236
        '
        'TabPageMovieFanart
        '
        Me.TabPageMovieFanart.AutoScroll = true
        Me.TabPageMovieFanart.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.TabPageMovieFanart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageMovieFanart.Controls.Add(Me.TableLayoutPanel10)
        Me.TabPageMovieFanart.Location = New System.Drawing.Point(4, 25)
        Me.TabPageMovieFanart.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPageMovieFanart.Name = "TabPageMovieFanart"
        Me.TabPageMovieFanart.Size = New System.Drawing.Size(192, 71)
        Me.TabPageMovieFanart.TabIndex = 2
        Me.TabPageMovieFanart.Tag = "M"
        Me.TabPageMovieFanart.Text = "Fanart"
        Me.TabPageMovieFanart.ToolTipText = "Browse and Edit Available Fanart"
        Me.TabPageMovieFanart.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 16
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 445!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 61!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel10.Controls.Add(Me.btn_MovFanartScrnSht, 2, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TextBox3, 2, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.btnPrevMissingFanart, 8, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.btnNextMissingFanart, 10, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.GroupBox1, 2, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.Label13, 2, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.Label14, 2, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.lblMovFanartWidth, 3, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.btncroptop, 5, 3)
        Me.TableLayoutPanel10.Controls.Add(Me.btncropright, 6, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.Label10, 5, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.lblMovFanartHeight, 3, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.btncropleft, 4, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.btncropbottom, 5, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.Label12, 2, 3)
        Me.TableLayoutPanel10.Controls.Add(Me.btnMovieFanartResizeImage, 12, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.btnresetimage, 12, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.btnSaveCropped, 12, 6)
        Me.TableLayoutPanel10.Controls.Add(Me.GroupBoxFanartExtrathumbs, 3, 7)
        Me.TableLayoutPanel10.Controls.Add(Me.lblFanartMissingCount, 8, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.btnMovFanartUrlorBrowse, 12, 7)
        Me.TableLayoutPanel10.Controls.Add(Me.ButtonFanartSaveLoRes, 12, 8)
        Me.TableLayoutPanel10.Controls.Add(Me.ButtonFanartSaveHiRes, 12, 9)
        Me.TableLayoutPanel10.Controls.Add(Me.Label7, 2, 6)
        Me.TableLayoutPanel10.Controls.Add(Me.btnMovPasteClipboardFanart, 8, 6)
        Me.TableLayoutPanel10.Controls.Add(Me.BtnSearchGoogleFanart, 8, 3)
        Me.TableLayoutPanel10.Controls.Add(Me.btnMovFanartToggle, 12, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.tb_MovFanartScrnShtTime, 4, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.Label2, 5, 2)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 11
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(956, 450)
        Me.TableLayoutPanel10.TabIndex = 133
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = true
        Me.Panel2.AutoScrollMargin = New System.Drawing.Size(0, 5)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.ContextMenuStrip = Me.FanartContextMenu
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.TableLayoutPanel10.SetRowSpan(Me.Panel2, 11)
        Me.Panel2.Size = New System.Drawing.Size(437, 442)
        Me.Panel2.TabIndex = 95
        '
        'FanartContextMenu
        '
        Me.FanartContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveSelectedFanartAsToolStripMenuItem})
        Me.FanartContextMenu.Name = "ContextMenuStrip5"
        Me.FanartContextMenu.Size = New System.Drawing.Size(192, 26)
        '
        'SaveSelectedFanartAsToolStripMenuItem
        '
        Me.SaveSelectedFanartAsToolStripMenuItem.Name = "SaveSelectedFanartAsToolStripMenuItem"
        Me.SaveSelectedFanartAsToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.SaveSelectedFanartAsToolStripMenuItem.Text = "Save Selected Fanart as"
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.TextBox3, 13)
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox3.Location = New System.Drawing.Point(457, 4)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = true
        Me.TextBox3.Size = New System.Drawing.Size(475, 31)
        Me.TextBox3.TabIndex = 103
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.TableLayoutPanel10.SetColumnSpan(Me.GroupBox1, 13)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(457, 41)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(475, 120)
        Me.GroupBox1.TabIndex = 102
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Current Fanart"
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.Location = New System.Drawing.Point(4, 18)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.MinimumSize = New System.Drawing.Size(124, 124)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(467, 124)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = false
        Me.PictureBox2.WaitOnLoad = true
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label13.Location = New System.Drawing.Point(457, 235)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Padding = New System.Windows.Forms.Padding(4, 10, 0, 0)
        Me.Label13.Size = New System.Drawing.Size(52, 26)
        Me.Label13.TabIndex = 122
        Me.Label13.Text = "Width : "
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = true
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.Location = New System.Drawing.Point(457, 270)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Padding = New System.Windows.Forms.Padding(0, 0, 0, 20)
        Me.Label14.Size = New System.Drawing.Size(53, 35)
        Me.Label14.TabIndex = 123
        Me.Label14.Text = "Height : "
        '
        'lblMovFanartWidth
        '
        Me.lblMovFanartWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblMovFanartWidth.AutoSize = true
        Me.lblMovFanartWidth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovFanartWidth.Location = New System.Drawing.Point(518, 244)
        Me.lblMovFanartWidth.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMovFanartWidth.Name = "lblMovFanartWidth"
        Me.lblMovFanartWidth.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.lblMovFanartWidth.Size = New System.Drawing.Size(56, 26)
        Me.lblMovFanartWidth.TabIndex = 125
        Me.lblMovFanartWidth.Text = "Label16"
        '
        'btncroptop
        '
        Me.btncroptop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btncroptop.Font = New System.Drawing.Font("Microsoft Sans Serif", 6!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btncroptop.Location = New System.Drawing.Point(626, 204)
        Me.btncroptop.Margin = New System.Windows.Forms.Padding(4)
        Me.btncroptop.Name = "btncroptop"
        Me.btncroptop.Size = New System.Drawing.Size(30, 27)
        Me.btncroptop.TabIndex = 117
        Me.btncroptop.Text = "V"
        Me.btncroptop.UseVisualStyleBackColor = true
        '
        'btncropright
        '
        Me.btncropright.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btncropright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btncropright.Location = New System.Drawing.Point(672, 239)
        Me.btncropright.Margin = New System.Windows.Forms.Padding(4)
        Me.btncropright.Name = "btncropright"
        Me.btncropright.Size = New System.Drawing.Size(30, 27)
        Me.btncropright.TabIndex = 115
        Me.btncropright.Text = "<"
        Me.btncropright.UseVisualStyleBackColor = true
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = true
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(626, 244)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.Label10.Size = New System.Drawing.Size(37, 26)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "Crop"
        '
        'lblMovFanartHeight
        '
        Me.lblMovFanartHeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblMovFanartHeight.AutoSize = true
        Me.lblMovFanartHeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovFanartHeight.Location = New System.Drawing.Point(518, 270)
        Me.lblMovFanartHeight.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMovFanartHeight.Name = "lblMovFanartHeight"
        Me.lblMovFanartHeight.Padding = New System.Windows.Forms.Padding(0, 0, 0, 20)
        Me.lblMovFanartHeight.Size = New System.Drawing.Size(56, 35)
        Me.lblMovFanartHeight.TabIndex = 126
        Me.lblMovFanartHeight.Text = "Label17"
        '
        'btncropleft
        '
        Me.btncropleft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btncropleft.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btncropleft.Location = New System.Drawing.Point(584, 239)
        Me.btncropleft.Margin = New System.Windows.Forms.Padding(4)
        Me.btncropleft.Name = "btncropleft"
        Me.btncropleft.Size = New System.Drawing.Size(30, 27)
        Me.btncropleft.TabIndex = 114
        Me.btncropleft.Text = ">"
        Me.btncropleft.UseVisualStyleBackColor = true
        '
        'btncropbottom
        '
        Me.btncropbottom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btncropbottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btncropbottom.Location = New System.Drawing.Point(626, 274)
        Me.btncropbottom.Margin = New System.Windows.Forms.Padding(4)
        Me.btncropbottom.Name = "btncropbottom"
        Me.btncropbottom.Size = New System.Drawing.Size(30, 27)
        Me.btncropbottom.TabIndex = 116
        Me.btncropbottom.Text = "^"
        Me.btncropbottom.UseVisualStyleBackColor = true
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.Label12, 2)
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(457, 219)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(104, 16)
        Me.Label12.TabIndex = 121
        Me.Label12.Text = "Image Details"
        '
        'btnresetimage
        '
        Me.btnresetimage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.btnresetimage, 2)
        Me.btnresetimage.Enabled = false
        Me.btnresetimage.Location = New System.Drawing.Point(872, 274)
        Me.btnresetimage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnresetimage.Name = "btnresetimage"
        Me.btnresetimage.Size = New System.Drawing.Size(105, 27)
        Me.btnresetimage.TabIndex = 119
        Me.btnresetimage.Text = "Reset Image"
        Me.btnresetimage.UseVisualStyleBackColor = true
        '
        'btnSaveCropped
        '
        Me.btnSaveCropped.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.btnSaveCropped, 2)
        Me.btnSaveCropped.Enabled = false
        Me.btnSaveCropped.Location = New System.Drawing.Point(872, 309)
        Me.btnSaveCropped.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveCropped.Name = "btnSaveCropped"
        Me.btnSaveCropped.Size = New System.Drawing.Size(105, 27)
        Me.btnSaveCropped.TabIndex = 120
        Me.btnSaveCropped.Text = "Save Changed"
        Me.btnSaveCropped.UseVisualStyleBackColor = true
        '
        'GroupBoxFanartExtrathumbs
        '
        Me.TableLayoutPanel10.SetColumnSpan(Me.GroupBoxFanartExtrathumbs, 6)
        Me.GroupBoxFanartExtrathumbs.Controls.Add(Me.rbMovThumb5)
        Me.GroupBoxFanartExtrathumbs.Controls.Add(Me.rbMovThumb4)
        Me.GroupBoxFanartExtrathumbs.Controls.Add(Me.rbMovThumb2)
        Me.GroupBoxFanartExtrathumbs.Controls.Add(Me.rbMovThumb3)
        Me.GroupBoxFanartExtrathumbs.Controls.Add(Me.rbMovThumb1)
        Me.GroupBoxFanartExtrathumbs.Controls.Add(Me.rbMovFanart)
        Me.GroupBoxFanartExtrathumbs.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBoxFanartExtrathumbs.Location = New System.Drawing.Point(625, 343)
        Me.GroupBoxFanartExtrathumbs.Name = "GroupBoxFanartExtrathumbs"
        Me.GroupBoxFanartExtrathumbs.Padding = New System.Windows.Forms.Padding(3, 3, 30, 3)
        Me.TableLayoutPanel10.SetRowSpan(Me.GroupBoxFanartExtrathumbs, 3)
        Me.GroupBoxFanartExtrathumbs.Size = New System.Drawing.Size(209, 99)
        Me.GroupBoxFanartExtrathumbs.TabIndex = 130
        Me.GroupBoxFanartExtrathumbs.TabStop = false
        Me.GroupBoxFanartExtrathumbs.Text = "Fanart ( Extrathumbs )"
        '
        'rbMovThumb5
        '
        Me.rbMovThumb5.AutoSize = true
        Me.rbMovThumb5.Location = New System.Drawing.Point(121, 71)
        Me.rbMovThumb5.Name = "rbMovThumb5"
        Me.rbMovThumb5.Size = New System.Drawing.Size(71, 19)
        Me.rbMovThumb5.TabIndex = 5
        Me.rbMovThumb5.Text = "Thumb5"
        Me.rbMovThumb5.UseVisualStyleBackColor = true
        '
        'rbMovThumb4
        '
        Me.rbMovThumb4.AutoSize = true
        Me.rbMovThumb4.Location = New System.Drawing.Point(121, 45)
        Me.rbMovThumb4.Name = "rbMovThumb4"
        Me.rbMovThumb4.Size = New System.Drawing.Size(71, 19)
        Me.rbMovThumb4.TabIndex = 4
        Me.rbMovThumb4.Text = "Thumb4"
        Me.rbMovThumb4.UseVisualStyleBackColor = true
        '
        'rbMovThumb2
        '
        Me.rbMovThumb2.AutoSize = true
        Me.rbMovThumb2.Location = New System.Drawing.Point(7, 71)
        Me.rbMovThumb2.Name = "rbMovThumb2"
        Me.rbMovThumb2.Size = New System.Drawing.Size(71, 19)
        Me.rbMovThumb2.TabIndex = 3
        Me.rbMovThumb2.Text = "Thumb2"
        Me.rbMovThumb2.UseVisualStyleBackColor = true
        '
        'rbMovThumb3
        '
        Me.rbMovThumb3.AutoSize = true
        Me.rbMovThumb3.Location = New System.Drawing.Point(121, 21)
        Me.rbMovThumb3.Name = "rbMovThumb3"
        Me.rbMovThumb3.Size = New System.Drawing.Size(71, 19)
        Me.rbMovThumb3.TabIndex = 2
        Me.rbMovThumb3.Text = "Thumb3"
        Me.rbMovThumb3.UseVisualStyleBackColor = true
        '
        'rbMovThumb1
        '
        Me.rbMovThumb1.AutoSize = true
        Me.rbMovThumb1.Location = New System.Drawing.Point(7, 45)
        Me.rbMovThumb1.Name = "rbMovThumb1"
        Me.rbMovThumb1.Size = New System.Drawing.Size(71, 19)
        Me.rbMovThumb1.TabIndex = 1
        Me.rbMovThumb1.Text = "Thumb1"
        Me.rbMovThumb1.UseVisualStyleBackColor = true
        '
        'rbMovFanart
        '
        Me.rbMovFanart.AutoSize = true
        Me.rbMovFanart.Checked = true
        Me.rbMovFanart.Location = New System.Drawing.Point(7, 21)
        Me.rbMovFanart.Name = "rbMovFanart"
        Me.rbMovFanart.Size = New System.Drawing.Size(60, 19)
        Me.rbMovFanart.TabIndex = 0
        Me.rbMovFanart.TabStop = true
        Me.rbMovFanart.Text = "Fanart"
        Me.rbMovFanart.UseVisualStyleBackColor = true
        '
        'lblFanartMissingCount
        '
        Me.lblFanartMissingCount.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.lblFanartMissingCount, 3)
        Me.lblFanartMissingCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFanartMissingCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFanartMissingCount.ForeColor = System.Drawing.Color.SeaGreen
        Me.lblFanartMissingCount.Location = New System.Drawing.Point(718, 270)
        Me.lblFanartMissingCount.Name = "lblFanartMissingCount"
        Me.lblFanartMissingCount.Padding = New System.Windows.Forms.Padding(20, 6, 0, 0)
        Me.lblFanartMissingCount.Size = New System.Drawing.Size(127, 35)
        Me.lblFanartMissingCount.TabIndex = 132
        Me.lblFanartMissingCount.Text = "9999 Missing"
        '
        'btnMovFanartUrlorBrowse
        '
        Me.btnMovFanartUrlorBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMovFanartUrlorBrowse.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.btnMovFanartUrlorBrowse, 2)
        Me.btnMovFanartUrlorBrowse.Location = New System.Drawing.Point(877, 344)
        Me.btnMovFanartUrlorBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovFanartUrlorBrowse.Name = "btnMovFanartUrlorBrowse"
        Me.btnMovFanartUrlorBrowse.Size = New System.Drawing.Size(100, 27)
        Me.btnMovFanartUrlorBrowse.TabIndex = 99
        Me.btnMovFanartUrlorBrowse.Text = "URL or Browse"
        Me.btnMovFanartUrlorBrowse.UseVisualStyleBackColor = true
        '
        'ButtonFanartSaveLoRes
        '
        Me.ButtonFanartSaveLoRes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ButtonFanartSaveLoRes.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.ButtonFanartSaveLoRes, 2)
        Me.ButtonFanartSaveLoRes.Location = New System.Drawing.Point(890, 379)
        Me.ButtonFanartSaveLoRes.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonFanartSaveLoRes.Name = "ButtonFanartSaveLoRes"
        Me.ButtonFanartSaveLoRes.Size = New System.Drawing.Size(87, 27)
        Me.ButtonFanartSaveLoRes.TabIndex = 128
        Me.ButtonFanartSaveLoRes.Text = "Save Lo-Res"
        Me.ButtonFanartSaveLoRes.UseVisualStyleBackColor = true
        '
        'ButtonFanartSaveHiRes
        '
        Me.ButtonFanartSaveHiRes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ButtonFanartSaveHiRes.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.ButtonFanartSaveHiRes, 2)
        Me.ButtonFanartSaveHiRes.Location = New System.Drawing.Point(892, 414)
        Me.ButtonFanartSaveHiRes.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonFanartSaveHiRes.Name = "ButtonFanartSaveHiRes"
        Me.ButtonFanartSaveHiRes.Size = New System.Drawing.Size(85, 27)
        Me.ButtonFanartSaveHiRes.TabIndex = 97
        Me.ButtonFanartSaveHiRes.Text = "Save Hi-Res"
        Me.ButtonFanartSaveHiRes.UseVisualStyleBackColor = true
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.Label7, 3)
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(457, 311)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.TableLayoutPanel10.SetRowSpan(Me.Label7, 4)
        Me.Label7.Size = New System.Drawing.Size(143, 134)
        Me.Label7.TabIndex = 96
        Me.Label7.Text = "To Change Your Fanart Selection, Check The Radio Button Of The Fanart You Wish To"& _ 
    " Use And Click On The ""Save Lo-Res"" OR ""Save Hi-Res"" Button. "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMovPasteClipboardFanart
        '
        Me.btnMovPasteClipboardFanart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovPasteClipboardFanart.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.btnMovPasteClipboardFanart, 4)
        Me.btnMovPasteClipboardFanart.Location = New System.Drawing.Point(718, 308)
        Me.btnMovPasteClipboardFanart.Name = "btnMovPasteClipboardFanart"
        Me.btnMovPasteClipboardFanart.Size = New System.Drawing.Size(130, 29)
        Me.btnMovPasteClipboardFanart.TabIndex = 133
        Me.btnMovPasteClipboardFanart.Text = "Paste from clipboard"
        Me.btnMovPasteClipboardFanart.UseVisualStyleBackColor = true
        '
        'BtnSearchGoogleFanart
        '
        Me.BtnSearchGoogleFanart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel10.SetColumnSpan(Me.BtnSearchGoogleFanart, 3)
        Me.BtnSearchGoogleFanart.Location = New System.Drawing.Point(718, 203)
        Me.BtnSearchGoogleFanart.Name = "BtnSearchGoogleFanart"
        Me.BtnSearchGoogleFanart.Size = New System.Drawing.Size(127, 29)
        Me.BtnSearchGoogleFanart.TabIndex = 134
        Me.BtnSearchGoogleFanart.Text = "Google Search"
        Me.BtnSearchGoogleFanart.UseVisualStyleBackColor = true
        '
        'tb_MovFanartScrnShtTime
        '
        Me.tb_MovFanartScrnShtTime.Location = New System.Drawing.Point(584, 173)
        Me.tb_MovFanartScrnShtTime.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.tb_MovFanartScrnShtTime.Name = "tb_MovFanartScrnShtTime"
        Me.tb_MovFanartScrnShtTime.Size = New System.Drawing.Size(34, 21)
        Me.tb_MovFanartScrnShtTime.TabIndex = 137
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.TableLayoutPanel10.SetColumnSpan(Me.Label2, 2)
        Me.Label2.Location = New System.Drawing.Point(626, 175)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 10, 4, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 138
        Me.Label2.Text = "Seconds"
        '
        'TabPageMoviePoster
        '
        Me.TabPageMoviePoster.AutoScroll = true
        Me.TabPageMoviePoster.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.TabPageMoviePoster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageMoviePoster.Controls.Add(Me.panelMoviePosterRHS)
        Me.TabPageMoviePoster.Controls.Add(Me.panelMoviePosterLHS)
        Me.TabPageMoviePoster.Location = New System.Drawing.Point(4, 25)
        Me.TabPageMoviePoster.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPageMoviePoster.Name = "TabPageMoviePoster"
        Me.TabPageMoviePoster.Size = New System.Drawing.Size(192, 71)
        Me.TabPageMoviePoster.TabIndex = 3
        Me.TabPageMoviePoster.Tag = "M"
        Me.TabPageMoviePoster.Text = "Posters"
        Me.TabPageMoviePoster.ToolTipText = "Browse and Edit Available Posters"
        Me.TabPageMoviePoster.UseVisualStyleBackColor = true
        '
        'panelMoviePosterRHS
        '
        Me.panelMoviePosterRHS.Controls.Add(Me.gbMoviePoster)
        Me.panelMoviePosterRHS.Controls.Add(Me.gbMoviePosterControls)
        Me.panelMoviePosterRHS.Controls.Add(Me.tbCurrentMoviePoster)
        Me.panelMoviePosterRHS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMoviePosterRHS.Location = New System.Drawing.Point(635, 0)
        Me.panelMoviePosterRHS.Margin = New System.Windows.Forms.Padding(4)
        Me.panelMoviePosterRHS.Name = "panelMoviePosterRHS"
        Me.panelMoviePosterRHS.Size = New System.Drawing.Size(321, 450)
        Me.panelMoviePosterRHS.TabIndex = 94
        '
        'gbMoviePoster
        '
        Me.gbMoviePoster.Controls.Add(Me.PictureBoxAssignedMoviePoster)
        Me.gbMoviePoster.Controls.Add(Me.lblCurrentLoadedPoster)
        Me.gbMoviePoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMoviePoster.Location = New System.Drawing.Point(0, 26)
        Me.gbMoviePoster.Margin = New System.Windows.Forms.Padding(4)
        Me.gbMoviePoster.Name = "gbMoviePoster"
        Me.gbMoviePoster.Padding = New System.Windows.Forms.Padding(4)
        Me.gbMoviePoster.Size = New System.Drawing.Size(321, 318)
        Me.gbMoviePoster.TabIndex = 137
        Me.gbMoviePoster.TabStop = false
        Me.gbMoviePoster.Text = "Current Poster"
        '
        'PictureBoxAssignedMoviePoster
        '
        Me.PictureBoxAssignedMoviePoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxAssignedMoviePoster.Location = New System.Drawing.Point(4, 18)
        Me.PictureBoxAssignedMoviePoster.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxAssignedMoviePoster.Name = "PictureBoxAssignedMoviePoster"
        Me.PictureBoxAssignedMoviePoster.Size = New System.Drawing.Size(313, 280)
        Me.PictureBoxAssignedMoviePoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxAssignedMoviePoster.TabIndex = 0
        Me.PictureBoxAssignedMoviePoster.TabStop = false
        '
        'lblCurrentLoadedPoster
        '
        Me.lblCurrentLoadedPoster.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblCurrentLoadedPoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCurrentLoadedPoster.Location = New System.Drawing.Point(4, 298)
        Me.lblCurrentLoadedPoster.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrentLoadedPoster.Name = "lblCurrentLoadedPoster"
        Me.lblCurrentLoadedPoster.Size = New System.Drawing.Size(313, 16)
        Me.lblCurrentLoadedPoster.TabIndex = 93
        Me.lblCurrentLoadedPoster.Text = "Current Loaded Poster - 1000 x 1000"
        Me.lblCurrentLoadedPoster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbMoviePosterControls
        '
        Me.gbMoviePosterControls.Controls.Add(Me.BtnGoogleSearchPoster)
        Me.gbMoviePosterControls.Controls.Add(Me.btnMovPasteClipboardPoster)
        Me.gbMoviePosterControls.Controls.Add(Me.btnMoviePosterEnableCrop)
        Me.gbMoviePosterControls.Controls.Add(Me.lblPosterMissingCount)
        Me.gbMoviePosterControls.Controls.Add(Me.btnPrevMissingPoster)
        Me.gbMoviePosterControls.Controls.Add(Me.cbMoviePosterSaveLoRes)
        Me.gbMoviePosterControls.Controls.Add(Me.btnNextMissingPoster)
        Me.gbMoviePosterControls.Controls.Add(Me.btnMoviePosterResetImage)
        Me.gbMoviePosterControls.Controls.Add(Me.btnMoviePosterSaveCroppedImage)
        Me.gbMoviePosterControls.Controls.Add(Me.btnPosterTabs_SaveImage)
        Me.gbMoviePosterControls.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbMoviePosterControls.Location = New System.Drawing.Point(0, 344)
        Me.gbMoviePosterControls.Name = "gbMoviePosterControls"
        Me.gbMoviePosterControls.Size = New System.Drawing.Size(321, 106)
        Me.gbMoviePosterControls.TabIndex = 138
        Me.gbMoviePosterControls.TabStop = false
        '
        'BtnGoogleSearchPoster
        '
        Me.BtnGoogleSearchPoster.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.BtnGoogleSearchPoster.Location = New System.Drawing.Point(8, 17)
        Me.BtnGoogleSearchPoster.Name = "BtnGoogleSearchPoster"
        Me.BtnGoogleSearchPoster.Size = New System.Drawing.Size(104, 23)
        Me.BtnGoogleSearchPoster.TabIndex = 94
        Me.BtnGoogleSearchPoster.Text = "Google Search"
        Me.BtnGoogleSearchPoster.UseVisualStyleBackColor = true
        '
        'btnMovPasteClipboardPoster
        '
        Me.btnMovPasteClipboardPoster.Location = New System.Drawing.Point(8, 44)
        Me.btnMovPasteClipboardPoster.Name = "btnMovPasteClipboardPoster"
        Me.btnMovPasteClipboardPoster.Size = New System.Drawing.Size(138, 23)
        Me.btnMovPasteClipboardPoster.TabIndex = 141
        Me.btnMovPasteClipboardPoster.Text = "Paste from Clipboard"
        Me.btnMovPasteClipboardPoster.UseVisualStyleBackColor = true
        '
        'btnMoviePosterEnableCrop
        '
        Me.btnMoviePosterEnableCrop.Location = New System.Drawing.Point(8, 73)
        Me.btnMoviePosterEnableCrop.Name = "btnMoviePosterEnableCrop"
        Me.btnMoviePosterEnableCrop.Size = New System.Drawing.Size(92, 29)
        Me.btnMoviePosterEnableCrop.TabIndex = 140
        Me.btnMoviePosterEnableCrop.Text = "Enable Crop"
        Me.btnMoviePosterEnableCrop.UseVisualStyleBackColor = true
        '
        'lblPosterMissingCount
        '
        Me.lblPosterMissingCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPosterMissingCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPosterMissingCount.ForeColor = System.Drawing.Color.SeaGreen
        Me.lblPosterMissingCount.Location = New System.Drawing.Point(125, 46)
        Me.lblPosterMissingCount.Name = "lblPosterMissingCount"
        Me.lblPosterMissingCount.Size = New System.Drawing.Size(120, 18)
        Me.lblPosterMissingCount.TabIndex = 139
        Me.lblPosterMissingCount.Text = "9999 Missing"
        Me.lblPosterMissingCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMoviePosterResetImage
        '
        Me.btnMoviePosterResetImage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMoviePosterResetImage.Location = New System.Drawing.Point(80, 73)
        Me.btnMoviePosterResetImage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMoviePosterResetImage.Name = "btnMoviePosterResetImage"
        Me.btnMoviePosterResetImage.Size = New System.Drawing.Size(92, 29)
        Me.btnMoviePosterResetImage.TabIndex = 133
        Me.btnMoviePosterResetImage.Text = "Reset Image"
        Me.btnMoviePosterResetImage.UseVisualStyleBackColor = true
        '
        'btnMoviePosterSaveCroppedImage
        '
        Me.btnMoviePosterSaveCroppedImage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMoviePosterSaveCroppedImage.Location = New System.Drawing.Point(180, 73)
        Me.btnMoviePosterSaveCroppedImage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMoviePosterSaveCroppedImage.Name = "btnMoviePosterSaveCroppedImage"
        Me.btnMoviePosterSaveCroppedImage.Size = New System.Drawing.Size(97, 29)
        Me.btnMoviePosterSaveCroppedImage.TabIndex = 134
        Me.btnMoviePosterSaveCroppedImage.Text = "Save Changed"
        Me.btnMoviePosterSaveCroppedImage.UseVisualStyleBackColor = true
        '
        'btnPosterTabs_SaveImage
        '
        Me.btnPosterTabs_SaveImage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPosterTabs_SaveImage.Location = New System.Drawing.Point(296, 73)
        Me.btnPosterTabs_SaveImage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPosterTabs_SaveImage.Name = "btnPosterTabs_SaveImage"
        Me.btnPosterTabs_SaveImage.Size = New System.Drawing.Size(52, 29)
        Me.btnPosterTabs_SaveImage.TabIndex = 99
        Me.btnPosterTabs_SaveImage.Text = "Save"
        Me.btnPosterTabs_SaveImage.UseVisualStyleBackColor = true
        '
        'tbCurrentMoviePoster
        '
        Me.tbCurrentMoviePoster.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbCurrentMoviePoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tbCurrentMoviePoster.Location = New System.Drawing.Point(0, 0)
        Me.tbCurrentMoviePoster.Margin = New System.Windows.Forms.Padding(4)
        Me.tbCurrentMoviePoster.Name = "tbCurrentMoviePoster"
        Me.tbCurrentMoviePoster.ReadOnly = true
        Me.tbCurrentMoviePoster.Size = New System.Drawing.Size(321, 26)
        Me.tbCurrentMoviePoster.TabIndex = 136
        Me.tbCurrentMoviePoster.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'panelMoviePosterLHS
        '
        Me.panelMoviePosterLHS.Controls.Add(Me.gbMoviePostersAvailable)
        Me.panelMoviePosterLHS.Controls.Add(Me.gbMoviePosterSelection)
        Me.panelMoviePosterLHS.Controls.Add(Me.tbSelectMoviePoster)
        Me.panelMoviePosterLHS.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelMoviePosterLHS.Location = New System.Drawing.Point(0, 0)
        Me.panelMoviePosterLHS.Margin = New System.Windows.Forms.Padding(4)
        Me.panelMoviePosterLHS.Name = "panelMoviePosterLHS"
        Me.panelMoviePosterLHS.Size = New System.Drawing.Size(635, 450)
        Me.panelMoviePosterLHS.TabIndex = 114
        '
        'gbMoviePostersAvailable
        '
        Me.gbMoviePostersAvailable.Controls.Add(Me.panelAvailableMoviePosters)
        Me.gbMoviePostersAvailable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMoviePostersAvailable.Location = New System.Drawing.Point(0, 26)
        Me.gbMoviePostersAvailable.Name = "gbMoviePostersAvailable"
        Me.gbMoviePostersAvailable.Size = New System.Drawing.Size(635, 318)
        Me.gbMoviePostersAvailable.TabIndex = 138
        Me.gbMoviePostersAvailable.TabStop = false
        Me.gbMoviePostersAvailable.Text = "Available posters"
        '
        'gbMoviePosterSelection
        '
        Me.gbMoviePosterSelection.Controls.Add(Me.btnMovPosterToggle)
        Me.gbMoviePosterSelection.Controls.Add(Me.btnMovPosterPrev)
        Me.gbMoviePosterSelection.Controls.Add(Me.btn_TMDb_posters)
        Me.gbMoviePosterSelection.Controls.Add(Me.btn_MPDB_posters)
        Me.gbMoviePosterSelection.Controls.Add(Me.btn_IMDB_posters)
        Me.gbMoviePosterSelection.Controls.Add(Me.btnMovPosterURLorBrowse)
        Me.gbMoviePosterSelection.Controls.Add(Me.btn_IMPA_posters)
        Me.gbMoviePosterSelection.Controls.Add(Me.lblMovPosterPages)
        Me.gbMoviePosterSelection.Controls.Add(Me.btnMovPosterNext)
        Me.gbMoviePosterSelection.Controls.Add(Me.Label24)
        Me.gbMoviePosterSelection.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbMoviePosterSelection.Location = New System.Drawing.Point(0, 344)
        Me.gbMoviePosterSelection.Name = "gbMoviePosterSelection"
        Me.gbMoviePosterSelection.Size = New System.Drawing.Size(635, 106)
        Me.gbMoviePosterSelection.TabIndex = 115
        Me.gbMoviePosterSelection.TabStop = false
        '
        'btnMovPosterPrev
        '
        Me.btnMovPosterPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovPosterPrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovPosterPrev.Location = New System.Drawing.Point(26, 17)
        Me.btnMovPosterPrev.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovPosterPrev.Name = "btnMovPosterPrev"
        Me.btnMovPosterPrev.Size = New System.Drawing.Size(99, 29)
        Me.btnMovPosterPrev.TabIndex = 106
        Me.btnMovPosterPrev.Text = "< Prev"
        Me.btnMovPosterPrev.UseVisualStyleBackColor = true
        '
        'btnMovPosterURLorBrowse
        '
        Me.btnMovPosterURLorBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovPosterURLorBrowse.Location = New System.Drawing.Point(497, 71)
        Me.btnMovPosterURLorBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovPosterURLorBrowse.Name = "btnMovPosterURLorBrowse"
        Me.btnMovPosterURLorBrowse.Size = New System.Drawing.Size(99, 29)
        Me.btnMovPosterURLorBrowse.TabIndex = 112
        Me.btnMovPosterURLorBrowse.Text = "URL or Browse"
        Me.btnMovPosterURLorBrowse.UseVisualStyleBackColor = true
        '
        'lblMovPosterPages
        '
        Me.lblMovPosterPages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblMovPosterPages.Location = New System.Drawing.Point(193, 19)
        Me.lblMovPosterPages.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMovPosterPages.Name = "lblMovPosterPages"
        Me.lblMovPosterPages.Size = New System.Drawing.Size(296, 22)
        Me.lblMovPosterPages.TabIndex = 108
        Me.lblMovPosterPages.Text = "Label18"
        Me.lblMovPosterPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMovPosterNext
        '
        Me.btnMovPosterNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovPosterNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovPosterNext.Location = New System.Drawing.Point(497, 16)
        Me.btnMovPosterNext.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovPosterNext.Name = "btnMovPosterNext"
        Me.btnMovPosterNext.Size = New System.Drawing.Size(99, 29)
        Me.btnMovPosterNext.TabIndex = 107
        Me.btnMovPosterNext.Text = "Next >"
        Me.btnMovPosterNext.UseVisualStyleBackColor = true
        '
        'Label24
        '
        Me.Label24.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label24.Location = New System.Drawing.Point(26, 46)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(570, 21)
        Me.Label24.TabIndex = 100
        Me.Label24.Text = "Select Poster Source"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbSelectMoviePoster
        '
        Me.tbSelectMoviePoster.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbSelectMoviePoster.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tbSelectMoviePoster.Location = New System.Drawing.Point(0, 0)
        Me.tbSelectMoviePoster.Margin = New System.Windows.Forms.Padding(4)
        Me.tbSelectMoviePoster.Name = "tbSelectMoviePoster"
        Me.tbSelectMoviePoster.ReadOnly = true
        Me.tbSelectMoviePoster.Size = New System.Drawing.Size(635, 26)
        Me.tbSelectMoviePoster.TabIndex = 137
        Me.tbSelectMoviePoster.Text = "Select Movie Poster"
        Me.tbSelectMoviePoster.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TabPage4
        '
        Me.TabPage4.AutoScroll = true
        Me.TabPage4.AutoScrollMinSize = New System.Drawing.Size(900, 400)
        Me.TabPage4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage4.Controls.Add(Me.TableLayoutPanel26)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage4.Size = New System.Drawing.Size(192, 71)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Tag = "M"
        Me.TabPage4.Text = "Change Movie"
        Me.TabPage4.ToolTipText = "Use this Tab if the scraper has downloaded information for the wrong movie."
        Me.TabPage4.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel26
        '
        Me.TableLayoutPanel26.CausesValidation = false
        Me.TableLayoutPanel26.ColumnCount = 12
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 214!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 181!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel26.Controls.Add(Me.WebBrowser1, 1, 1)
        Me.TableLayoutPanel26.Controls.Add(Me.CheckBox2, 9, 3)
        Me.TableLayoutPanel26.Controls.Add(Me.btnChangeMovie, 7, 3)
        Me.TableLayoutPanel26.Controls.Add(Me.Button12, 1, 3)
        Me.TableLayoutPanel26.Controls.Add(Me.btn_IMDBSearch, 3, 3)
        Me.TableLayoutPanel26.Controls.Add(Me.btn_TMDBSearch, 5, 3)
        Me.TableLayoutPanel26.Controls.Add(Me.Label15, 6, 3)
        Me.TableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel26.Location = New System.Drawing.Point(4, 4)
        Me.TableLayoutPanel26.Name = "TableLayoutPanel26"
        Me.TableLayoutPanel26.RowCount = 5
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel26.Size = New System.Drawing.Size(900, 400)
        Me.TableLayoutPanel26.TabIndex = 13
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel26.SetColumnSpan(Me.WebBrowser1, 11)
        Me.WebBrowser1.Location = New System.Drawing.Point(8, 8)
        Me.WebBrowser1.Margin = New System.Windows.Forms.Padding(4)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(25, 25)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(888, 326)
        Me.WebBrowser1.TabIndex = 0
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = true
        Me.CheckBox2.Checked = true
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CheckBox2.Location = New System.Drawing.Point(855, 347)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(173, 45)
        Me.CheckBox2.TabIndex = 6
        Me.CheckBox2.Text = "Un-check to keep your"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"current movie art."
        Me.CheckBox2.UseVisualStyleBackColor = true
        '
        'btnChangeMovie
        '
        Me.btnChangeMovie.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnChangeMovie.Location = New System.Drawing.Point(758, 357)
        Me.btnChangeMovie.Margin = New System.Windows.Forms.Padding(4, 14, 4, 4)
        Me.btnChangeMovie.Name = "btnChangeMovie"
        Me.btnChangeMovie.Size = New System.Drawing.Size(70, 29)
        Me.btnChangeMovie.TabIndex = 9
        Me.btnChangeMovie.Text = "Go"
        Me.btnChangeMovie.UseVisualStyleBackColor = true
        '
        'Button12
        '
        Me.Button12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button12.Location = New System.Drawing.Point(8, 357)
        Me.Button12.Margin = New System.Windows.Forms.Padding(4, 14, 4, 4)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(135, 29)
        Me.Button12.TabIndex = 10
        Me.Button12.Text = "Page Back"
        Me.Button12.UseVisualStyleBackColor = true
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.Location = New System.Drawing.Point(592, 355)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 12, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(158, 41)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "Find The Correct Movie and Click Go"
        '
        'TabPage7
        '
        Me.TabPage7.AutoScroll = true
        Me.TabPage7.BackColor = System.Drawing.Color.Transparent
        Me.TabPage7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage7.Controls.Add(Me.TableLayoutPanel23)
        Me.TabPage7.ImageIndex = 0
        Me.TabPage7.Location = New System.Drawing.Point(4, 25)
        Me.TabPage7.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(1041, 628)
        Me.TabPage7.TabIndex = 4
        Me.TabPage7.ToolTipText = "Open this Movie at the IMDB webpage In Your Default Web Browser"
        Me.TabPage7.UseVisualStyleBackColor = true
        '
        'WebBrowser2
        '
        Me.TableLayoutPanel23.SetColumnSpan(Me.WebBrowser2, 4)
        Me.WebBrowser2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser2.Location = New System.Drawing.Point(4, 4)
        Me.WebBrowser2.Margin = New System.Windows.Forms.Padding(4)
        Me.WebBrowser2.MaximumSize = New System.Drawing.Size(0, 6250)
        Me.WebBrowser2.MinimumSize = New System.Drawing.Size(25, 25)
        Me.WebBrowser2.Name = "WebBrowser2"
        Me.WebBrowser2.Size = New System.Drawing.Size(1029, 576)
        Me.WebBrowser2.TabIndex = 0
        '
        'TabPage8
        '
        Me.TabPage8.AutoScroll = true
        Me.TabPage8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage8.Controls.Add(Me.TextBox8)
        Me.TabPage8.Location = New System.Drawing.Point(4, 25)
        Me.TabPage8.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(192, 71)
        Me.TabPage8.TabIndex = 5
        Me.TabPage8.Tag = "M"
        Me.TabPage8.Text = "File Details"
        Me.TabPage8.ToolTipText = "View The Details Of This Media File."
        Me.TabPage8.UseVisualStyleBackColor = true
        '
        'TextBox8
        '
        Me.TextBox8.BackColor = System.Drawing.Color.White
        Me.TextBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox8.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox8.Location = New System.Drawing.Point(0, 0)
        Me.TextBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox8.Multiline = true
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.ReadOnly = true
        Me.TextBox8.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox8.Size = New System.Drawing.Size(188, 67)
        Me.TextBox8.TabIndex = 0
        '
        'ToolTip2
        '
        Me.ToolTip2.AutoPopDelay = 10000
        Me.ToolTip2.InitialDelay = 500
        Me.ToolTip2.IsBalloon = true
        Me.ToolTip2.ReshowDelay = 100
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Honeydew
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel6, Me.ToolStripProgressBar5, Me.ToolStripStatusLabel2, Me.ToolStripProgressBar2, Me.ToolStripStatusLabel3, Me.ToolStripProgressBar3, Me.ToolStripStatusLabel4, Me.ToolStripProgressBar4, Me.ToolStripStatusLabel7, Me.ToolStripProgressBar6, Me.ToolStripStatusLabel5, Me.ToolStripStatusLabel8, Me.ToolStripProgressBar7, Me.ToolStripProgressBar8, Me.ToolStripStatusLabel9})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 721)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 18, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1069, 0)
        Me.StatusStrip1.TabIndex = 46
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripProgressBar1.Maximum = 1000
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripProgressBar1.Step = 1
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar1.Visible = false
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(110, 17)
        Me.ToolStripStatusLabel1.Text = "Movie Scan Progress"
        Me.ToolStripStatusLabel1.ToolTipText = "pooooooo"
        Me.ToolStripStatusLabel1.Visible = false
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel6.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel6.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(99, 17)
        Me.ToolStripStatusLabel6.Text = "Parsing TV Folders"
        Me.ToolStripStatusLabel6.Visible = false
        '
        'ToolStripProgressBar5
        '
        Me.ToolStripProgressBar5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripProgressBar5.Maximum = 1000
        Me.ToolStripProgressBar5.Name = "ToolStripProgressBar5"
        Me.ToolStripProgressBar5.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripProgressBar5.Step = 1
        Me.ToolStripProgressBar5.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar5.Visible = false
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(172, 13)
        Me.ToolStripStatusLabel2.Text = "TV Show Episode Scan In Progress"
        Me.ToolStripStatusLabel2.ToolTipText = "boooooo"
        Me.ToolStripStatusLabel2.Visible = false
        '
        'ToolStripProgressBar2
        '
        Me.ToolStripProgressBar2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripProgressBar2.Maximum = 1000
        Me.ToolStripProgressBar2.Name = "ToolStripProgressBar2"
        Me.ToolStripProgressBar2.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripProgressBar2.Step = 1
        Me.ToolStripProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar2.Visible = false
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(123, 13)
        Me.ToolStripStatusLabel3.Text = "Fanart Scan In Progress"
        Me.ToolStripStatusLabel3.Visible = false
        '
        'ToolStripProgressBar3
        '
        Me.ToolStripProgressBar3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripProgressBar3.Name = "ToolStripProgressBar3"
        Me.ToolStripProgressBar3.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripProgressBar3.Step = 1
        Me.ToolStripProgressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar3.Visible = false
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(122, 13)
        Me.ToolStripStatusLabel4.Text = "Scraping dropped Movie"
        Me.ToolStripStatusLabel4.Visible = false
        '
        'ToolStripProgressBar4
        '
        Me.ToolStripProgressBar4.Name = "ToolStripProgressBar4"
        Me.ToolStripProgressBar4.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripProgressBar4.Step = 1
        Me.ToolStripProgressBar4.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar4.Visible = false
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(153, 13)
        Me.ToolStripStatusLabel7.Text = "Movie Wizard Scan in Progress"
        Me.ToolStripStatusLabel7.Visible = false
        '
        'ToolStripProgressBar6
        '
        Me.ToolStripProgressBar6.Maximum = 1000
        Me.ToolStripProgressBar6.Name = "ToolStripProgressBar6"
        Me.ToolStripProgressBar6.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripProgressBar6.Step = 1
        Me.ToolStripProgressBar6.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar6.Visible = false
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(111, 13)
        Me.ToolStripStatusLabel5.Text = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Visible = false
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(111, 13)
        Me.ToolStripStatusLabel8.Text = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Visible = false
        '
        'ToolStripProgressBar7
        '
        Me.ToolStripProgressBar7.Name = "ToolStripProgressBar7"
        Me.ToolStripProgressBar7.Size = New System.Drawing.Size(125, 19)
        Me.ToolStripProgressBar7.Step = 1
        Me.ToolStripProgressBar7.Visible = false
        '
        'ToolStripProgressBar8
        '
        Me.ToolStripProgressBar8.Name = "ToolStripProgressBar8"
        Me.ToolStripProgressBar8.Size = New System.Drawing.Size(125, 19)
        Me.ToolStripProgressBar8.Step = 1
        Me.ToolStripProgressBar8.Visible = false
        '
        'ToolStripStatusLabel9
        '
        Me.ToolStripStatusLabel9.Name = "ToolStripStatusLabel9"
        Me.ToolStripStatusLabel9.Size = New System.Drawing.Size(111, 13)
        Me.ToolStripStatusLabel9.Text = "ToolStripStatusLabel9"
        Me.ToolStripStatusLabel9.Visible = false
        '
        'bckgroundscanepisodes
        '
        Me.bckgroundscanepisodes.WorkerReportsProgress = true
        Me.bckgroundscanepisodes.WorkerSupportsCancellation = true
        '
        'bckgroundfanart
        '
        Me.bckgroundfanart.WorkerReportsProgress = true
        Me.bckgroundfanart.WorkerSupportsCancellation = true
        '
        'bckgrounddroppedfiles
        '
        Me.bckgrounddroppedfiles.WorkerReportsProgress = true
        Me.bckgrounddroppedfiles.WorkerSupportsCancellation = true
        '
        'bckepisodethumb
        '
        Me.bckepisodethumb.WorkerReportsProgress = true
        Me.bckepisodethumb.WorkerSupportsCancellation = true
        '
        'TabLevel1
        '
        Me.TabLevel1.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabLevel1.Controls.Add(Me.TabPage1)
        Me.TabLevel1.Controls.Add(Me.TabPage2)
        Me.TabLevel1.Controls.Add(Me.TabMV)
        Me.TabLevel1.Controls.Add(Me.TabPage3)
        Me.TabLevel1.Controls.Add(Me.TabCustTv)
        Me.TabLevel1.Controls.Add(Me.TabPage34)
        Me.TabLevel1.Controls.Add(Me.TabControlDebug)
        Me.TabLevel1.Controls.Add(Me.TabConfigXML)
        Me.TabLevel1.Controls.Add(Me.TabMovieCacheXML)
        Me.TabLevel1.Controls.Add(Me.TabTVCacheXML)
        Me.TabLevel1.Controls.Add(Me.TabProfile)
        Me.TabLevel1.Controls.Add(Me.TabActorCache)
        Me.TabLevel1.Controls.Add(Me.TabRegex)
        Me.TabLevel1.Controls.Add(Me.TabTasks)
        Me.TabLevel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabLevel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TabLevel1.ImageList = Me.ImageList1
        Me.TabLevel1.ItemSize = New System.Drawing.Size(40, 20)
        Me.TabLevel1.Location = New System.Drawing.Point(0, 24)
        Me.TabLevel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabLevel1.MinimumSize = New System.Drawing.Size(755, 75)
        Me.TabLevel1.Name = "TabLevel1"
        Me.TabLevel1.SelectedIndex = 0
        Me.TabLevel1.ShowToolTips = true
        Me.TabLevel1.Size = New System.Drawing.Size(1069, 697)
        Me.TabLevel1.TabIndex = 52
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = true
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage1.Controls.Add(Me.TabControl2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1061, 669)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Movies"
        Me.TabPage1.UseVisualStyleBackColor = true
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPageLevel2MovMainBrowser)
        Me.TabControl2.Controls.Add(Me.TabPage22)
        Me.TabControl2.Controls.Add(Me.tpMoviesTable)
        Me.TabControl2.Controls.Add(Me.TabPageMovieFanart)
        Me.TabControl2.Controls.Add(Me.TabPageMoviePoster)
        Me.TabControl2.Controls.Add(Me.tpFanartTv)
        Me.TabControl2.Controls.Add(Me.TabPage9)
        Me.TabControl2.Controls.Add(Me.tpMediaStubs)
        Me.TabControl2.Controls.Add(Me.TabPage7)
        Me.TabControl2.Controls.Add(Me.TabPage8)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage25)
        Me.TabControl2.Controls.Add(Me.TabPage26)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TabControl2.ImageList = Me.ImageList1
        Me.TabControl2.Location = New System.Drawing.Point(4, 4)
        Me.TabControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.ShowToolTips = true
        Me.TabControl2.Size = New System.Drawing.Size(1049, 657)
        Me.TabControl2.TabIndex = 87
        '
        'TabPage22
        '
        Me.TabPage22.AutoScroll = true
        Me.TabPage22.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.TabPage22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage22.Location = New System.Drawing.Point(4, 25)
        Me.TabPage22.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage22.Name = "TabPage22"
        Me.TabPage22.Size = New System.Drawing.Size(192, 71)
        Me.TabPage22.TabIndex = 9
        Me.TabPage22.Text = "Wall"
        Me.TabPage22.UseVisualStyleBackColor = true
        '
        'tpMoviesTable
        '
        Me.tpMoviesTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpMoviesTable.Controls.Add(Me.TableLayoutPanel29)
        Me.tpMoviesTable.Location = New System.Drawing.Point(4, 25)
        Me.tpMoviesTable.Margin = New System.Windows.Forms.Padding(4)
        Me.tpMoviesTable.Name = "tpMoviesTable"
        Me.tpMoviesTable.Size = New System.Drawing.Size(192, 71)
        Me.tpMoviesTable.TabIndex = 13
        Me.tpMoviesTable.Tag = "M"
        Me.tpMoviesTable.Text = "Table"
        Me.tpMoviesTable.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.CausesValidation = false
        Me.TableLayoutPanel29.ColumnCount = 10
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 297!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.Controls.Add(Me.DataGridView1, 1, 1)
        Me.TableLayoutPanel29.Controls.Add(Me.btn_movTableApply, 6, 7)
        Me.TableLayoutPanel29.Controls.Add(Me.btn_movTableColumnsSelect, 4, 7)
        Me.TableLayoutPanel29.Controls.Add(Me.mov_TableEditDGV, 1, 5)
        Me.TableLayoutPanel29.Controls.Add(Me.lbl_movTableMulti, 1, 7)
        Me.TableLayoutPanel29.Controls.Add(Me.btn_movTableSave, 8, 7)
        Me.TableLayoutPanel29.Controls.Add(Me.lbl_movTableEdit, 1, 3)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 9
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32!))
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(188, 67)
        Me.TableLayoutPanel29.TabIndex = 40
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = false
        Me.DataGridView1.AllowUserToResizeRows = false
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel29.SetColumnSpan(Me.DataGridView1, 8)
        Me.DataGridView1.ContextMenuStrip = Me.MovieTableContextMenu
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DataGridView1.Location = New System.Drawing.Point(8, 8)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(172, 1)
        Me.DataGridView1.TabIndex = 0
        '
        'MovieTableContextMenu
        '
        Me.MovieTableContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MarkAllSelectedAsWatchedToolStripMenuItem, Me.MarkAllSelectedAsUnWatchedToolStripMenuItem, Me.GoToToolStripMenuItem, Me.GoToSelectedMoviePosterSelectorToolStripMenuItem, Me.GoToSelectedMovieFanartSelectorToolStripMenuItem})
        Me.MovieTableContextMenu.Name = "ContextMenuStrip_table"
        Me.MovieTableContextMenu.Size = New System.Drawing.Size(259, 114)
        '
        'MarkAllSelectedAsWatchedToolStripMenuItem
        '
        Me.MarkAllSelectedAsWatchedToolStripMenuItem.Name = "MarkAllSelectedAsWatchedToolStripMenuItem"
        Me.MarkAllSelectedAsWatchedToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
        Me.MarkAllSelectedAsWatchedToolStripMenuItem.Text = "Mark All Selected as Watched"
        '
        'MarkAllSelectedAsUnWatchedToolStripMenuItem
        '
        Me.MarkAllSelectedAsUnWatchedToolStripMenuItem.Name = "MarkAllSelectedAsUnWatchedToolStripMenuItem"
        Me.MarkAllSelectedAsUnWatchedToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
        Me.MarkAllSelectedAsUnWatchedToolStripMenuItem.Text = "Mark All Selected as Un-Watched"
        '
        'GoToToolStripMenuItem
        '
        Me.GoToToolStripMenuItem.Name = "GoToToolStripMenuItem"
        Me.GoToToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
        Me.GoToToolStripMenuItem.Text = " Go To Selected Movie in Main Browser"
        '
        'GoToSelectedMoviePosterSelectorToolStripMenuItem
        '
        Me.GoToSelectedMoviePosterSelectorToolStripMenuItem.Name = "GoToSelectedMoviePosterSelectorToolStripMenuItem"
        Me.GoToSelectedMoviePosterSelectorToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
        Me.GoToSelectedMoviePosterSelectorToolStripMenuItem.Text = "Go To Selected Movie Poster Selector"
        '
        'GoToSelectedMovieFanartSelectorToolStripMenuItem
        '
        Me.GoToSelectedMovieFanartSelectorToolStripMenuItem.Name = "GoToSelectedMovieFanartSelectorToolStripMenuItem"
        Me.GoToSelectedMovieFanartSelectorToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
        Me.GoToSelectedMovieFanartSelectorToolStripMenuItem.Text = "Go To Selected Movie Fanart Selector"
        '
        'btn_movTableColumnsSelect
        '
        Me.btn_movTableColumnsSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_movTableColumnsSelect.Location = New System.Drawing.Point(-190, 37)
        Me.btn_movTableColumnsSelect.Name = "btn_movTableColumnsSelect"
        Me.btn_movTableColumnsSelect.Size = New System.Drawing.Size(151, 23)
        Me.btn_movTableColumnsSelect.TabIndex = 37
        Me.btn_movTableColumnsSelect.Text = "Select Columns to View"
        Me.btn_movTableColumnsSelect.UseVisualStyleBackColor = true
        '
        'mov_TableEditDGV
        '
        Me.mov_TableEditDGV.AllowUserToAddRows = false
        Me.mov_TableEditDGV.AllowUserToResizeRows = false
        Me.mov_TableEditDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.mov_TableEditDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel29.SetColumnSpan(Me.mov_TableEditDGV, 8)
        Me.mov_TableEditDGV.ContextMenuStrip = Me.MovieTableContextMenu
        Me.mov_TableEditDGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mov_TableEditDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.mov_TableEditDGV.Location = New System.Drawing.Point(8, -25)
        Me.mov_TableEditDGV.Margin = New System.Windows.Forms.Padding(4)
        Me.mov_TableEditDGV.Name = "mov_TableEditDGV"
        Me.mov_TableEditDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.mov_TableEditDGV.RowTemplate.Height = 24
        Me.mov_TableEditDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.mov_TableEditDGV.Size = New System.Drawing.Size(172, 48)
        Me.mov_TableEditDGV.TabIndex = 38
        '
        'lbl_movTableMulti
        '
        Me.lbl_movTableMulti.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lbl_movTableMulti.AutoSize = true
        Me.lbl_movTableMulti.Location = New System.Drawing.Point(8, 33)
        Me.lbl_movTableMulti.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_movTableMulti.Name = "lbl_movTableMulti"
        Me.lbl_movTableMulti.Size = New System.Drawing.Size(280, 30)
        Me.lbl_movTableMulti.TabIndex = 32
        Me.lbl_movTableMulti.Text = "Edit multiple movies at once using the cells above,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"and Apply Edits. (Blank cell"& _ 
    "s will be ignored.)"
        '
        'btn_movTableSave
        '
        Me.btn_movTableSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_movTableSave.Enabled = false
        Me.btn_movTableSave.Location = New System.Drawing.Point(86, 35)
        Me.btn_movTableSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_movTableSave.Name = "btn_movTableSave"
        Me.btn_movTableSave.Size = New System.Drawing.Size(94, 24)
        Me.btn_movTableSave.TabIndex = 1
        Me.btn_movTableSave.Text = "Save Changes"
        Me.btn_movTableSave.UseVisualStyleBackColor = true
        '
        'lbl_movTableEdit
        '
        Me.lbl_movTableEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lbl_movTableEdit.AutoSize = true
        Me.lbl_movTableEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_movTableEdit.Location = New System.Drawing.Point(7, -48)
        Me.lbl_movTableEdit.Name = "lbl_movTableEdit"
        Me.lbl_movTableEdit.Size = New System.Drawing.Size(133, 15)
        Me.lbl_movTableEdit.TabIndex = 39
        Me.lbl_movTableEdit.Text = "Multi-Select Editing"
        '
        'tpFanartTv
        '
        Me.tpFanartTv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpFanartTv.Controls.Add(Me.UcFanartTv1)
        Me.tpFanartTv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tpFanartTv.Location = New System.Drawing.Point(4, 25)
        Me.tpFanartTv.Name = "tpFanartTv"
        Me.tpFanartTv.Size = New System.Drawing.Size(192, 71)
        Me.tpFanartTv.TabIndex = 15
        Me.tpFanartTv.Text = "Fanart.Tv"
        Me.tpFanartTv.UseVisualStyleBackColor = true
        '
        'UcFanartTv1
        '
        Me.UcFanartTv1.BackColor = System.Drawing.SystemColors.ControlText
        Me.UcFanartTv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFanartTv1.Location = New System.Drawing.Point(0, 0)
        Me.UcFanartTv1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UcFanartTv1.Name = "UcFanartTv1"
        Me.UcFanartTv1.Padding = New System.Windows.Forms.Padding(6)
        Me.UcFanartTv1.Size = New System.Drawing.Size(188, 67)
        Me.UcFanartTv1.TabIndex = 0
        '
        'TabPage9
        '
        Me.TabPage9.AutoScroll = true
        Me.TabPage9.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.TabPage9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage9.Controls.Add(Me.SplitContainer8)
        Me.TabPage9.Location = New System.Drawing.Point(4, 25)
        Me.TabPage9.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(192, 71)
        Me.TabPage9.TabIndex = 10
        Me.TabPage9.Text = "MovieSets & Tags"
        Me.TabPage9.UseVisualStyleBackColor = true
        '
        'SplitContainer8
        '
        Me.SplitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer8.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer8.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer8.Name = "SplitContainer8"
        '
        'SplitContainer8.Panel1
        '
        Me.SplitContainer8.Panel1.Controls.Add(Me.TableLayoutPanel11)
        '
        'SplitContainer8.Panel2
        '
        Me.SplitContainer8.Panel2.Controls.Add(Me.TableLayoutPanel14)
        Me.SplitContainer8.Size = New System.Drawing.Size(956, 450)
        Me.SplitContainer8.SplitterDistance = 475
        Me.SplitContainer8.TabIndex = 13
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 7
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel11.Controls.Add(Me.btnMovieSetRemove, 2, 6)
        Me.TableLayoutPanel11.Controls.Add(Me.Label126, 2, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.Label68, 1, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.btnMovieSetsRepopulateFromUsed, 4, 6)
        Me.TableLayoutPanel11.Controls.Add(Me.Label79, 1, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.tbMovSetEntry, 1, 3)
        Me.TableLayoutPanel11.Controls.Add(Me.btnMovieSetAdd, 5, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.dgvmovset, 1, 4)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 8
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(471, 446)
        Me.TableLayoutPanel11.TabIndex = 13
        '
        'btnMovieSetRemove
        '
        Me.btnMovieSetRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovieSetRemove.Location = New System.Drawing.Point(69, 393)
        Me.btnMovieSetRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovieSetRemove.Name = "btnMovieSetRemove"
        Me.btnMovieSetRemove.Size = New System.Drawing.Size(119, 29)
        Me.btnMovieSetRemove.TabIndex = 5
        Me.btnMovieSetRemove.Text = "Remove Selected"
        Me.btnMovieSetRemove.UseVisualStyleBackColor = true
        '
        'Label126
        '
        Me.Label126.AutoSize = true
        Me.TableLayoutPanel11.SetColumnSpan(Me.Label126, 4)
        Me.Label126.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label126.Location = New System.Drawing.Point(69, 0)
        Me.Label126.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label126.Name = "Label126"
        Me.Label126.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label126.Size = New System.Drawing.Size(307, 47)
        Me.Label126.TabIndex = 12
        Me.Label126.Text = "Your Movie Sets"
        '
        'Label68
        '
        Me.Label68.AutoSize = true
        Me.TableLayoutPanel11.SetColumnSpan(Me.Label68, 6)
        Me.Label68.Location = New System.Drawing.Point(33, 61)
        Me.Label68.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(424, 60)
        Me.Label68.TabIndex = 1
        Me.Label68.Text = resources.GetString("Label68.Text")
        '
        'Label79
        '
        Me.Label79.AutoSize = true
        Me.TableLayoutPanel11.SetColumnSpan(Me.Label79, 4)
        Me.Label79.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label79.Location = New System.Drawing.Point(33, 138)
        Me.Label79.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(171, 15)
        Me.Label79.TabIndex = 2
        Me.Label79.Text = "Enter Name of New Movie Set"
        '
        'tbMovSetEntry
        '
        Me.TableLayoutPanel11.SetColumnSpan(Me.tbMovSetEntry, 4)
        Me.tbMovSetEntry.Location = New System.Drawing.Point(33, 164)
        Me.tbMovSetEntry.Margin = New System.Windows.Forms.Padding(4)
        Me.tbMovSetEntry.Name = "tbMovSetEntry"
        Me.tbMovSetEntry.Size = New System.Drawing.Size(329, 21)
        Me.tbMovSetEntry.TabIndex = 4
        '
        'btnMovieSetAdd
        '
        Me.btnMovieSetAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnMovieSetAdd.Location = New System.Drawing.Point(370, 153)
        Me.btnMovieSetAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovieSetAdd.Name = "btnMovieSetAdd"
        Me.TableLayoutPanel11.SetRowSpan(Me.btnMovieSetAdd, 2)
        Me.HelpProvider1.SetShowHelp(Me.btnMovieSetAdd, true)
        Me.btnMovieSetAdd.Size = New System.Drawing.Size(70, 29)
        Me.btnMovieSetAdd.TabIndex = 3
        Me.btnMovieSetAdd.Text = "Add Set"
        Me.btnMovieSetAdd.UseVisualStyleBackColor = true
        '
        'dgvmovset
        '
        Me.dgvmovset.AllowUserToAddRows = false
        Me.dgvmovset.AllowUserToDeleteRows = false
        Me.dgvmovset.AllowUserToResizeColumns = false
        Me.dgvmovset.AllowUserToResizeRows = false
        Me.dgvmovset.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgvmovset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvmovset.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.movsettitle, Me.tmdbid, Me.movsetfanart, Me.movsetposter})
        Me.TableLayoutPanel11.SetColumnSpan(Me.dgvmovset, 5)
        Me.dgvmovset.ContextMenuStrip = Me.MovSetsContextMenu
        Me.dgvmovset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvmovset.Location = New System.Drawing.Point(32, 189)
        Me.dgvmovset.MultiSelect = false
        Me.dgvmovset.Name = "dgvmovset"
        Me.dgvmovset.ReadOnly = true
        Me.dgvmovset.RowHeadersVisible = false
        Me.dgvmovset.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvmovset.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvmovset.ShowCellToolTips = false
        Me.dgvmovset.ShowEditingIcon = false
        Me.dgvmovset.Size = New System.Drawing.Size(409, 175)
        Me.dgvmovset.TabIndex = 13
        '
        'movsettitle
        '
        Me.movsettitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.movsettitle.HeaderText = "Movie Set Name"
        Me.movsettitle.Name = "movsettitle"
        Me.movsettitle.ReadOnly = true
        Me.movsettitle.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'tmdbid
        '
        Me.tmdbid.HeaderText = "TmdbId"
        Me.tmdbid.MinimumWidth = 52
        Me.tmdbid.Name = "tmdbid"
        Me.tmdbid.ReadOnly = true
        Me.tmdbid.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.tmdbid.Width = 52
        '
        'movsetfanart
        '
        Me.movsetfanart.HeaderText = "Fanart"
        Me.movsetfanart.MinimumWidth = 48
        Me.movsetfanart.Name = "movsetfanart"
        Me.movsetfanart.ReadOnly = true
        Me.movsetfanart.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.movsetfanart.Width = 48
        '
        'movsetposter
        '
        Me.movsetposter.HeaderText = "Poster"
        Me.movsetposter.MinimumWidth = 48
        Me.movsetposter.Name = "movsetposter"
        Me.movsetposter.ReadOnly = true
        Me.movsetposter.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.movsetposter.Width = 48
        '
        'MovSetsContextMenu
        '
        Me.MovSetsContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiMovSetName, Me.ToolStripSeparator31, Me.tsmiMovSetShowCollection, Me.tsmiMovSetGetFanart, Me.tsmiMovSetGetPoster})
        Me.MovSetsContextMenu.Name = "MovSetsContextMenu"
        Me.MovSetsContextMenu.Size = New System.Drawing.Size(199, 98)
        '
        'tsmiMovSetName
        '
        Me.tsmiMovSetName.Name = "tsmiMovSetName"
        Me.tsmiMovSetName.Size = New System.Drawing.Size(198, 22)
        '
        'ToolStripSeparator31
        '
        Me.ToolStripSeparator31.Name = "ToolStripSeparator31"
        Me.ToolStripSeparator31.Size = New System.Drawing.Size(195, 6)
        '
        'tsmiMovSetShowCollection
        '
        Me.tsmiMovSetShowCollection.Name = "tsmiMovSetShowCollection"
        Me.tsmiMovSetShowCollection.Size = New System.Drawing.Size(198, 22)
        Me.tsmiMovSetShowCollection.Text = "Show Movies In Collection"
        '
        'tsmiMovSetGetFanart
        '
        Me.tsmiMovSetGetFanart.Name = "tsmiMovSetGetFanart"
        Me.tsmiMovSetGetFanart.Size = New System.Drawing.Size(198, 22)
        Me.tsmiMovSetGetFanart.Text = "Scrape Collection Fanart"
        '
        'tsmiMovSetGetPoster
        '
        Me.tsmiMovSetGetPoster.Name = "tsmiMovSetGetPoster"
        Me.tsmiMovSetGetPoster.Size = New System.Drawing.Size(198, 22)
        Me.tsmiMovSetGetPoster.Text = "Scrape Collection Poster"
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 3
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 538!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel14.Controls.Add(Me.Label127, 1, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.GroupBox40, 1, 2)
        Me.TableLayoutPanel14.Controls.Add(Me.GroupBox39, 1, 1)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 4
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 285!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 283!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(473, 446)
        Me.TableLayoutPanel14.TabIndex = 25
        '
        'Label127
        '
        Me.Label127.AutoSize = true
        Me.Label127.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label127.Location = New System.Drawing.Point(12, 0)
        Me.Label127.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(491, 42)
        Me.Label127.TabIndex = 13
        Me.Label127.Text = "Tag Addition and Selection"
        Me.Label127.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox40
        '
        Me.GroupBox40.Controls.Add(Me.TableLayoutPanel12)
        Me.GroupBox40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox40.Location = New System.Drawing.Point(11, 332)
        Me.GroupBox40.Name = "GroupBox40"
        Me.GroupBox40.Size = New System.Drawing.Size(532, 277)
        Me.GroupBox40.TabIndex = 24
        Me.GroupBox40.TabStop = false
        Me.GroupBox40.Text = "Adding/Removing Tag(s) from Movie(s)"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 5
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 217!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel12.Controls.Add(Me.Label187, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnMovTagSavetoNfo, 1, 3)
        Me.TableLayoutPanel12.Controls.Add(Me.CurrentMovieTags, 0, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.btnMovTagRemove, 1, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.btnMovTagAdd, 1, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.lblMovTagMulti1, 3, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.lblMovTagMulti2, 3, 1)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 6
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(526, 257)
        Me.TableLayoutPanel12.TabIndex = 28
        '
        'Label187
        '
        Me.Label187.AutoSize = true
        Me.TableLayoutPanel12.SetColumnSpan(Me.Label187, 2)
        Me.Label187.Location = New System.Drawing.Point(3, 0)
        Me.Label187.Name = "Label187"
        Me.Label187.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label187.Size = New System.Drawing.Size(266, 21)
        Me.Label187.TabIndex = 27
        Me.Label187.Text = "Current or Selected Tag(s) for selected Movie(s)"
        '
        'CurrentMovieTags
        '
        Me.CurrentMovieTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrentMovieTags.FormattingEnabled = true
        Me.CurrentMovieTags.ItemHeight = 15
        Me.CurrentMovieTags.Location = New System.Drawing.Point(3, 31)
        Me.CurrentMovieTags.Name = "CurrentMovieTags"
        Me.TableLayoutPanel12.SetRowSpan(Me.CurrentMovieTags, 5)
        Me.CurrentMovieTags.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.CurrentMovieTags.Size = New System.Drawing.Size(211, 223)
        Me.CurrentMovieTags.Sorted = true
        Me.CurrentMovieTags.TabIndex = 23
        '
        'lblMovTagMulti1
        '
        Me.lblMovTagMulti1.AutoSize = true
        Me.lblMovTagMulti1.BackColor = System.Drawing.Color.LightCoral
        Me.lblMovTagMulti1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovTagMulti1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovTagMulti1.Location = New System.Drawing.Point(328, 2)
        Me.lblMovTagMulti1.Margin = New System.Windows.Forms.Padding(6, 2, 3, 0)
        Me.lblMovTagMulti1.Name = "lblMovTagMulti1"
        Me.lblMovTagMulti1.Size = New System.Drawing.Size(181, 20)
        Me.lblMovTagMulti1.TabIndex = 28
        Me.lblMovTagMulti1.Text = "Multi-Selected Movies."
        '
        'lblMovTagMulti2
        '
        Me.lblMovTagMulti2.AutoSize = true
        Me.lblMovTagMulti2.Location = New System.Drawing.Point(328, 31)
        Me.lblMovTagMulti2.Margin = New System.Windows.Forms.Padding(6, 3, 3, 0)
        Me.lblMovTagMulti2.Name = "lblMovTagMulti2"
        Me.TableLayoutPanel12.SetRowSpan(Me.lblMovTagMulti2, 3)
        Me.lblMovTagMulti2.Size = New System.Drawing.Size(183, 180)
        Me.lblMovTagMulti2.TabIndex = 29
        Me.lblMovTagMulti2.Text = resources.GetString("lblMovTagMulti2.Text")
        '
        'GroupBox39
        '
        Me.GroupBox39.Controls.Add(Me.TableLayoutPanel13)
        Me.GroupBox39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox39.Location = New System.Drawing.Point(11, 47)
        Me.GroupBox39.Name = "GroupBox39"
        Me.GroupBox39.Size = New System.Drawing.Size(532, 279)
        Me.GroupBox39.TabIndex = 23
        Me.GroupBox39.TabStop = false
        Me.GroupBox39.Text = "Adding custom or existing Tag(s)"
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 5
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 217!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 206!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel13.Controls.Add(Me.Label188, 1, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.txtbxMovTagEntry, 1, 2)
        Me.TableLayoutPanel13.Controls.Add(Me.btnMovTagListAdd, 3, 2)
        Me.TableLayoutPanel13.Controls.Add(Me.TagListBox, 0, 2)
        Me.TableLayoutPanel13.Controls.Add(Me.btnMovTagListRemove, 1, 3)
        Me.TableLayoutPanel13.Controls.Add(Me.btnMovTagListRefresh, 1, 4)
        Me.TableLayoutPanel13.Controls.Add(Me.Label186, 0, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 7
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(526, 259)
        Me.TableLayoutPanel13.TabIndex = 31
        '
        'Label188
        '
        Me.TableLayoutPanel13.SetColumnSpan(Me.Label188, 3)
        Me.Label188.Location = New System.Drawing.Point(220, 0)
        Me.Label188.Name = "Label188"
        Me.TableLayoutPanel13.SetRowSpan(Me.Label188, 2)
        Me.Label188.Size = New System.Drawing.Size(252, 34)
        Me.Label188.TabIndex = 30
        Me.Label188.Text = "Enter single Tag and select ""Add Tag"" button or press ""Enter"""
        '
        'txtbxMovTagEntry
        '
        Me.txtbxMovTagEntry.AcceptsReturn = true
        Me.txtbxMovTagEntry.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtbxMovTagEntry.Location = New System.Drawing.Point(220, 47)
        Me.txtbxMovTagEntry.Name = "txtbxMovTagEntry"
        Me.txtbxMovTagEntry.Size = New System.Drawing.Size(200, 21)
        Me.txtbxMovTagEntry.TabIndex = 26
        '
        'btnMovTagListAdd
        '
        Me.btnMovTagListAdd.Location = New System.Drawing.Point(436, 38)
        Me.btnMovTagListAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMovTagListAdd.Name = "btnMovTagListAdd"
        Me.HelpProvider1.SetShowHelp(Me.btnMovTagListAdd, true)
        Me.btnMovTagListAdd.Size = New System.Drawing.Size(64, 29)
        Me.btnMovTagListAdd.TabIndex = 25
        Me.btnMovTagListAdd.Text = "Add Tag"
        Me.btnMovTagListAdd.UseVisualStyleBackColor = true
        '
        'Label186
        '
        Me.Label186.AutoSize = true
        Me.Label186.Location = New System.Drawing.Point(3, 0)
        Me.Label186.Name = "Label186"
        Me.Label186.Padding = New System.Windows.Forms.Padding(6, 8, 0, 0)
        Me.TableLayoutPanel13.SetRowSpan(Me.Label186, 2)
        Me.Label186.Size = New System.Drawing.Size(135, 23)
        Me.Label186.TabIndex = 29
        Me.Label186.Text = "List of available Tag(s)"
        '
        'tpMediaStubs
        '
        Me.tpMediaStubs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpMediaStubs.Controls.Add(Me.MediaStubs1)
        Me.tpMediaStubs.Location = New System.Drawing.Point(4, 25)
        Me.tpMediaStubs.Name = "tpMediaStubs"
        Me.tpMediaStubs.Size = New System.Drawing.Size(192, 71)
        Me.tpMediaStubs.TabIndex = 14
        Me.tpMediaStubs.Text = "Media Stubs"
        Me.tpMediaStubs.UseVisualStyleBackColor = true
        '
        'MediaStubs1
        '
        Me.MediaStubs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MediaStubs1.Location = New System.Drawing.Point(0, 0)
        Me.MediaStubs1.Name = "MediaStubs1"
        Me.MediaStubs1.Size = New System.Drawing.Size(188, 67)
        Me.MediaStubs1.TabIndex = 0
        '
        'TabPage25
        '
        Me.TabPage25.AutoScroll = true
        Me.TabPage25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage25.Controls.Add(Me.Panel4)
        Me.TabPage25.Controls.Add(Me.Panel3)
        Me.TabPage25.Location = New System.Drawing.Point(4, 25)
        Me.TabPage25.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage25.Name = "TabPage25"
        Me.TabPage25.Size = New System.Drawing.Size(1041, 628)
        Me.TabPage25.TabIndex = 11
        Me.TabPage25.Text = "Folders"
        Me.TabPage25.UseVisualStyleBackColor = true
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.TableLayoutPanel28)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(327, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(710, 624)
        Me.Panel4.TabIndex = 15
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 2
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.87324!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.12676!))
        Me.TableLayoutPanel28.Controls.Add(Me.Label147, 0, 1)
        Me.TableLayoutPanel28.Controls.Add(Me.Panel5, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.ButtonSaveAndQuickRefresh, 1, 1)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 2
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.22916!))
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.770833!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(710, 624)
        Me.TableLayoutPanel28.TabIndex = 0
        '
        'Label147
        '
        Me.Label147.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label147.AutoSize = true
        Me.Label147.Location = New System.Drawing.Point(15, 587)
        Me.Label147.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(317, 30)
        Me.Label147.TabIndex = 18
        Me.Label147.Text = "These only create the folders, you will still need to use the"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"'Search for new Mo"& _ 
    "vies' option to add the movie data."
        '
        'Panel5
        '
        Me.TableLayoutPanel28.SetColumnSpan(Me.Panel5, 2)
        Me.Panel5.Controls.Add(Me.SplitContainer7)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(704, 575)
        Me.Panel5.TabIndex = 20
        '
        'SplitContainer7
        '
        Me.SplitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer7.IsSplitterFixed = true
        Me.SplitContainer7.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer7.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer7.Name = "SplitContainer7"
        '
        'SplitContainer7.Panel1
        '
        Me.SplitContainer7.Panel1.Controls.Add(Me.TableLayoutPanel9)
        '
        'SplitContainer7.Panel2
        '
        Me.SplitContainer7.Panel2.Controls.Add(Me.TableLayoutPanel8)
        Me.SplitContainer7.Panel2.Controls.Add(Me.Label145)
        Me.SplitContainer7.Size = New System.Drawing.Size(704, 575)
        Me.SplitContainer7.SplitterDistance = 360
        Me.SplitContainer7.TabIndex = 15
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 4
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62!))
        Me.TableLayoutPanel9.Controls.Add(Me.Label134, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.Label184, 0, 3)
        Me.TableLayoutPanel9.Controls.Add(Me.btnMovieManualPathAdd, 3, 4)
        Me.TableLayoutPanel9.Controls.Add(Me.tbMovieManualPath, 0, 4)
        Me.TableLayoutPanel9.Controls.Add(Me.btn_addmoviefolderdialogue, 0, 5)
        Me.TableLayoutPanel9.Controls.Add(Me.btn_removemoviefolder, 2, 5)
        Me.TableLayoutPanel9.Controls.Add(Me.clbx_MovieRoots, 0, 1)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 6
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(358, 573)
        Me.TableLayoutPanel9.TabIndex = 14
        '
        'Label134
        '
        Me.Label134.AutoSize = true
        Me.TableLayoutPanel9.SetColumnSpan(Me.Label134, 3)
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label134.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label134.Location = New System.Drawing.Point(4, 6)
        Me.Label134.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label134.Name = "Label134"
        Me.Label134.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Label134.Size = New System.Drawing.Size(288, 24)
        Me.Label134.TabIndex = 10
        Me.Label134.Text = "Movie Folders"
        '
        'Label184
        '
        Me.Label184.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label184.AutoSize = true
        Me.TableLayoutPanel9.SetColumnSpan(Me.Label184, 3)
        Me.Label184.Location = New System.Drawing.Point(3, 473)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(227, 15)
        Me.Label184.TabIndex = 12
        Me.Label184.Text = "Manually Add path to Movie Root Folder."
        '
        'btnMovieManualPathAdd
        '
        Me.btnMovieManualPathAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnMovieManualPathAdd.Location = New System.Drawing.Point(299, 492)
        Me.btnMovieManualPathAdd.Name = "btnMovieManualPathAdd"
        Me.btnMovieManualPathAdd.Size = New System.Drawing.Size(54, 29)
        Me.btnMovieManualPathAdd.TabIndex = 13
        Me.btnMovieManualPathAdd.Text = "Add"
        Me.btnMovieManualPathAdd.UseVisualStyleBackColor = true
        '
        'tbMovieManualPath
        '
        Me.TableLayoutPanel9.SetColumnSpan(Me.tbMovieManualPath, 3)
        Me.tbMovieManualPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbMovieManualPath.Location = New System.Drawing.Point(3, 496)
        Me.tbMovieManualPath.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
        Me.tbMovieManualPath.Name = "tbMovieManualPath"
        Me.tbMovieManualPath.Size = New System.Drawing.Size(290, 21)
        Me.tbMovieManualPath.TabIndex = 11
        '
        'btn_addmoviefolderdialogue
        '
        Me.btn_addmoviefolderdialogue.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_addmoviefolderdialogue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_addmoviefolderdialogue.Location = New System.Drawing.Point(18, 528)
        Me.btn_addmoviefolderdialogue.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_addmoviefolderdialogue.Name = "btn_addmoviefolderdialogue"
        Me.btn_addmoviefolderdialogue.Size = New System.Drawing.Size(145, 41)
        Me.btn_addmoviefolderdialogue.TabIndex = 4
        Me.btn_addmoviefolderdialogue.Text = "Add Movie Folder Browser"
        Me.btn_addmoviefolderdialogue.UseVisualStyleBackColor = true
        '
        'btn_removemoviefolder
        '
        Me.TableLayoutPanel9.SetColumnSpan(Me.btn_removemoviefolder, 2)
        Me.btn_removemoviefolder.Dock = System.Windows.Forms.DockStyle.Left
        Me.btn_removemoviefolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_removemoviefolder.Location = New System.Drawing.Point(199, 528)
        Me.btn_removemoviefolder.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_removemoviefolder.Name = "btn_removemoviefolder"
        Me.btn_removemoviefolder.Size = New System.Drawing.Size(146, 41)
        Me.btn_removemoviefolder.TabIndex = 3
        Me.btn_removemoviefolder.Text = "Remove Selected Folder's"
        Me.btn_removemoviefolder.UseVisualStyleBackColor = true
        '
        'clbx_MovieRoots
        '
        Me.clbx_MovieRoots.AllowDrop = true
        Me.TableLayoutPanel9.SetColumnSpan(Me.clbx_MovieRoots, 4)
        Me.clbx_MovieRoots.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbx_MovieRoots.FormattingEnabled = true
        Me.clbx_MovieRoots.Location = New System.Drawing.Point(3, 33)
        Me.clbx_MovieRoots.Name = "clbx_MovieRoots"
        Me.TableLayoutPanel9.SetRowSpan(Me.clbx_MovieRoots, 2)
        Me.clbx_MovieRoots.Size = New System.Drawing.Size(352, 436)
        Me.clbx_MovieRoots.Sorted = true
        Me.clbx_MovieRoots.TabIndex = 14
        Me.clbx_MovieRoots.ThreeDCheckBoxes = true
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 6
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59!))
        Me.TableLayoutPanel8.Controls.Add(Me.Button102, 0, 7)
        Me.TableLayoutPanel8.Controls.Add(Me.Button101, 4, 7)
        Me.TableLayoutPanel8.Controls.Add(Me.Label133, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.ListBox15, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.Button108, 2, 6)
        Me.TableLayoutPanel8.Controls.Add(Me.Label146, 0, 5)
        Me.TableLayoutPanel8.Controls.Add(Me.TextBox44, 1, 4)
        Me.TableLayoutPanel8.Controls.Add(Me.Button107, 5, 4)
        Me.TableLayoutPanel8.Controls.Add(Me.Label144, 0, 3)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 8
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(338, 573)
        Me.TableLayoutPanel8.TabIndex = 19
        '
        'Button102
        '
        Me.Button102.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button102.AutoSize = true
        Me.TableLayoutPanel8.SetColumnSpan(Me.Button102, 3)
        Me.Button102.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button102.Location = New System.Drawing.Point(4, 539)
        Me.Button102.Margin = New System.Windows.Forms.Padding(4)
        Me.Button102.Name = "Button102"
        Me.Button102.Size = New System.Drawing.Size(143, 30)
        Me.Button102.TabIndex = 12
        Me.Button102.Text = "Add Movie Folder Browser"
        Me.Button102.UseVisualStyleBackColor = true
        '
        'Button101
        '
        Me.Button101.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button101.AutoSize = true
        Me.TableLayoutPanel8.SetColumnSpan(Me.Button101, 2)
        Me.Button101.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button101.Location = New System.Drawing.Point(165, 539)
        Me.Button101.Margin = New System.Windows.Forms.Padding(4)
        Me.Button101.Name = "Button101"
        Me.Button101.Size = New System.Drawing.Size(166, 30)
        Me.Button101.TabIndex = 11
        Me.Button101.Text = "Remove Selected Folder(s)"
        Me.Button101.UseVisualStyleBackColor = true
        '
        'Label133
        '
        Me.Label133.AutoSize = true
        Me.TableLayoutPanel8.SetColumnSpan(Me.Label133, 3)
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label133.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label133.Location = New System.Drawing.Point(4, 0)
        Me.Label133.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label133.Name = "Label133"
        Me.Label133.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Label133.Size = New System.Drawing.Size(143, 32)
        Me.Label133.TabIndex = 9
        Me.Label133.Text = "Offline Movie Folders"
        '
        'ListBox15
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.ListBox15, 6)
        Me.ListBox15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ListBox15.FormattingEnabled = true
        Me.ListBox15.ItemHeight = 15
        Me.ListBox15.Location = New System.Drawing.Point(4, 36)
        Me.ListBox15.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBox15.Name = "ListBox15"
        Me.TableLayoutPanel8.SetRowSpan(Me.ListBox15, 2)
        Me.ListBox15.Size = New System.Drawing.Size(330, 355)
        Me.ListBox15.Sorted = true
        Me.ListBox15.TabIndex = 8
        '
        'Button108
        '
        Me.Button108.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button108.AutoSize = true
        Me.TableLayoutPanel8.SetColumnSpan(Me.Button108, 3)
        Me.Button108.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button108.Location = New System.Drawing.Point(114, 502)
        Me.Button108.Margin = New System.Windows.Forms.Padding(4)
        Me.Button108.Name = "Button108"
        Me.Button108.Size = New System.Drawing.Size(144, 29)
        Me.Button108.TabIndex = 17
        Me.Button108.Text = "Load offline movie list..."
        Me.Button108.UseVisualStyleBackColor = true
        '
        'Label146
        '
        Me.Label146.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel8.SetColumnSpan(Me.Label146, 6)
        Me.Label146.Location = New System.Drawing.Point(4, 448)
        Me.Label146.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(330, 50)
        Me.Label146.TabIndex = 16
        Me.Label146.Text = "You can also create folders from a list in a text file that has each movie on a s"& _ 
    "eperate line. Use the browse button below to load the text file."
        Me.Label146.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBox44
        '
        Me.TextBox44.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel8.SetColumnSpan(Me.TextBox44, 4)
        Me.TextBox44.Location = New System.Drawing.Point(79, 420)
        Me.TextBox44.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox44.Name = "TextBox44"
        Me.TextBox44.Size = New System.Drawing.Size(196, 21)
        Me.TextBox44.TabIndex = 14
        '
        'Button107
        '
        Me.Button107.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button107.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button107.Location = New System.Drawing.Point(285, 417)
        Me.Button107.Margin = New System.Windows.Forms.Padding(4)
        Me.Button107.Name = "Button107"
        Me.Button107.Size = New System.Drawing.Size(49, 24)
        Me.Button107.TabIndex = 15
        Me.Button107.Text = "Add"
        Me.Button107.UseVisualStyleBackColor = true
        '
        'Label144
        '
        Me.Label144.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label144.AutoSize = true
        Me.TableLayoutPanel8.SetColumnSpan(Me.Label144, 6)
        Me.Label144.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label144.Location = New System.Drawing.Point(4, 398)
        Me.Label144.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(329, 15)
        Me.Label144.TabIndex = 13
        Me.Label144.Text = "Use this control to add a movie to the selected folder above."
        '
        'Label145
        '
        Me.Label145.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label145.AutoSize = true
        Me.Label145.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label145.Location = New System.Drawing.Point(8, 17175)
        Me.Label145.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(66, 15)
        Me.Label145.TabIndex = 11
        Me.Label145.Text = "Movie Title"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label86)
        Me.Panel3.Controls.Add(Me.Label136)
        Me.Panel3.Controls.Add(Me.Label87)
        Me.Panel3.Controls.Add(Me.Label135)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(327, 624)
        Me.Panel3.TabIndex = 20
        '
        'Label86
        '
        Me.Label86.AutoSize = true
        Me.Label86.Location = New System.Drawing.Point(10, 11)
        Me.Label86.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(302, 120)
        Me.Label86.TabIndex = 5
        Me.Label86.Text = resources.GetString("Label86.Text")
        '
        'Label136
        '
        Me.Label136.AutoSize = true
        Me.Label136.Location = New System.Drawing.Point(10, 283)
        Me.Label136.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(312, 255)
        Me.Label136.TabIndex = 14
        Me.Label136.Text = resources.GetString("Label136.Text")
        '
        'Label87
        '
        Me.Label87.AutoSize = true
        Me.Label87.Location = New System.Drawing.Point(10, 147)
        Me.Label87.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(286, 105)
        Me.Label87.TabIndex = 6
        Me.Label87.Text = resources.GetString("Label87.Text")
        '
        'Label135
        '
        Me.Label135.AutoSize = true
        Me.Label135.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label135.Location = New System.Drawing.Point(10, 266)
        Me.Label135.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(127, 13)
        Me.Label135.TabIndex = 13
        Me.Label135.Text = "Offline Movie Folders"
        '
        'TabPage26
        '
        Me.TabPage26.AutoScroll = true
        Me.TabPage26.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.TabPage26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage26.Location = New System.Drawing.Point(4, 25)
        Me.TabPage26.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage26.Name = "TabPage26"
        Me.TabPage26.Size = New System.Drawing.Size(192, 71)
        Me.TabPage26.TabIndex = 12
        Me.TabPage26.Text = "Movie Preferences"
        Me.TabPage26.UseVisualStyleBackColor = true
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "imdb.png")
        Me.ImageList1.Images.SetKeyName(1, "TMDB_sm2.jpg")
        '
        'TabPage2
        '
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage2.Controls.Add(Me.TabControl3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Size = New System.Drawing.Size(1061, 669)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TV Shows"
        Me.TabPage2.UseVisualStyleBackColor = true
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPageLevel2TVMainBrowser)
        Me.TabControl3.Controls.Add(Me.tpTvScreenshot)
        Me.TabControl3.Controls.Add(Me.tpTvFanart)
        Me.TabControl3.Controls.Add(Me.tpTvPosters)
        Me.TabControl3.Controls.Add(Me.tpTvFanartTv)
        Me.TabControl3.Controls.Add(Me.tpTvWall)
        Me.TabControl3.Controls.Add(Me.tpTvSelector)
        Me.TabControl3.Controls.Add(Me.tpTvTable)
        Me.TabControl3.Controls.Add(Me.tpTvWeb)
        Me.TabControl3.Controls.Add(Me.tpTvFolders)
        Me.TabControl3.Controls.Add(Me.TabPage24)
        Me.TabControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl3.ImageList = Me.ImageList3
        Me.TabControl3.Location = New System.Drawing.Point(4, 4)
        Me.TabControl3.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.ShowToolTips = true
        Me.TabControl3.Size = New System.Drawing.Size(1049, 657)
        Me.TabControl3.TabIndex = 0
        '
        'TabPageLevel2TVMainBrowser
        '
        Me.TabPageLevel2TVMainBrowser.AutoScroll = true
        Me.TabPageLevel2TVMainBrowser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPageLevel2TVMainBrowser.Controls.Add(Me.SplitContainer3)
        Me.TabPageLevel2TVMainBrowser.Location = New System.Drawing.Point(4, 25)
        Me.TabPageLevel2TVMainBrowser.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPageLevel2TVMainBrowser.Name = "TabPageLevel2TVMainBrowser"
        Me.TabPageLevel2TVMainBrowser.Size = New System.Drawing.Size(1041, 628)
        Me.TabPageLevel2TVMainBrowser.TabIndex = 0
        Me.TabPageLevel2TVMainBrowser.Text = "Main Browser"
        Me.TabPageLevel2TVMainBrowser.UseVisualStyleBackColor = true
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer10)
        Me.SplitContainer3.Panel1MinSize = 315
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel8)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel9)
        Me.SplitContainer3.Panel2.Controls.Add(Me.pbtvfanarttv)
        Me.SplitContainer3.Panel2.Controls.Add(Me.TableLayoutPanel20)
        Me.SplitContainer3.Size = New System.Drawing.Size(1037, 624)
        Me.SplitContainer3.SplitterDistance = 315
        Me.SplitContainer3.SplitterWidth = 5
        Me.SplitContainer3.TabIndex = 1
        '
        'SplitContainer10
        '
        Me.SplitContainer10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer10.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer10.IsSplitterFixed = true
        Me.SplitContainer10.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer10.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer10.Name = "SplitContainer10"
        Me.SplitContainer10.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer10.Panel1
        '
        Me.SplitContainer10.Panel1.Controls.Add(Me.TableLayoutPanel7)
        '
        'SplitContainer10.Panel2
        '
        Me.SplitContainer10.Panel2.Controls.Add(Me.Panel11)
        Me.SplitContainer10.Panel2MinSize = 92
        Me.SplitContainer10.Size = New System.Drawing.Size(311, 620)
        Me.SplitContainer10.SplitterDistance = 526
        Me.SplitContainer10.SplitterWidth = 2
        Me.SplitContainer10.TabIndex = 0
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 9
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel7.Controls.Add(Me.btnTvSearchNew, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.TvTreeview, 0, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.TextBox_TotEpisodeCount, 7, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.btnTvRefreshAll, 4, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.TextBox_TotTVShowCount, 2, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.Label71, 3, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.Label74, 0, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(311, 526)
        Me.TableLayoutPanel7.TabIndex = 187
        '
        'TextBox_TotEpisodeCount
        '
        Me.TableLayoutPanel7.SetColumnSpan(Me.TextBox_TotEpisodeCount, 2)
        Me.TextBox_TotEpisodeCount.Location = New System.Drawing.Point(256, 46)
        Me.TextBox_TotEpisodeCount.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox_TotEpisodeCount.Name = "TextBox_TotEpisodeCount"
        Me.TextBox_TotEpisodeCount.ReadOnly = true
        Me.TextBox_TotEpisodeCount.Size = New System.Drawing.Size(51, 21)
        Me.TextBox_TotEpisodeCount.TabIndex = 4
        Me.TextBox_TotEpisodeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox_TotTVShowCount
        '
        Me.TextBox_TotTVShowCount.Location = New System.Drawing.Point(106, 46)
        Me.TextBox_TotTVShowCount.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox_TotTVShowCount.Name = "TextBox_TotTVShowCount"
        Me.TextBox_TotTVShowCount.ReadOnly = true
        Me.TextBox_TotTVShowCount.Size = New System.Drawing.Size(41, 21)
        Me.TextBox_TotTVShowCount.TabIndex = 3
        Me.TextBox_TotTVShowCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label71
        '
        Me.Label71.AutoSize = true
        Me.TableLayoutPanel7.SetColumnSpan(Me.Label71, 4)
        Me.Label71.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label71.Location = New System.Drawing.Point(155, 42)
        Me.Label71.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label71.Name = "Label71"
        Me.Label71.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label71.Size = New System.Drawing.Size(90, 21)
        Me.Label71.TabIndex = 1
        Me.Label71.Text = "Episode Count:"
        '
        'Label74
        '
        Me.Label74.AutoSize = true
        Me.TableLayoutPanel7.SetColumnSpan(Me.Label74, 2)
        Me.Label74.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label74.Location = New System.Drawing.Point(4, 42)
        Me.Label74.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label74.Size = New System.Drawing.Size(93, 21)
        Me.Label74.TabIndex = 2
        Me.Label74.Text = "TV Show Count:"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.rbTvListUnKnown)
        Me.Panel11.Controls.Add(Me.rbTvListContinuing)
        Me.Panel11.Controls.Add(Me.rbTvListEnded)
        Me.Panel11.Controls.Add(Me.rbTvDisplayUnWatched)
        Me.Panel11.Controls.Add(Me.rbTvListAll)
        Me.Panel11.Controls.Add(Me.rbTvMissingAiredEp)
        Me.Panel11.Controls.Add(Me.rbTvDisplayWatched)
        Me.Panel11.Controls.Add(Me.rbTvMissingFanart)
        Me.Panel11.Controls.Add(Me.rbTvMissingPoster)
        Me.Panel11.Controls.Add(Me.rbTvMissingThumb)
        Me.Panel11.Controls.Add(Me.rbTvMissingEpisodes)
        Me.Panel11.Location = New System.Drawing.Point(0, 1)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(311, 91)
        Me.Panel11.TabIndex = 182
        '
        'rbTvListContinuing
        '
        Me.rbTvListContinuing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvListContinuing.AutoSize = true
        Me.rbTvListContinuing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvListContinuing.Location = New System.Drawing.Point(138, 4)
        Me.rbTvListContinuing.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvListContinuing.Name = "rbTvListContinuing"
        Me.rbTvListContinuing.Size = New System.Drawing.Size(84, 19)
        Me.rbTvListContinuing.TabIndex = 14
        Me.rbTvListContinuing.Text = "Continuing"
        Me.rbTvListContinuing.UseVisualStyleBackColor = true
        '
        'rbTvListEnded
        '
        Me.rbTvListEnded.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvListEnded.AutoSize = true
        Me.rbTvListEnded.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvListEnded.Location = New System.Drawing.Point(73, 4)
        Me.rbTvListEnded.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvListEnded.Name = "rbTvListEnded"
        Me.rbTvListEnded.Size = New System.Drawing.Size(61, 19)
        Me.rbTvListEnded.TabIndex = 13
        Me.rbTvListEnded.Text = "Ended"
        Me.rbTvListEnded.UseVisualStyleBackColor = true
        '
        'rbTvDisplayUnWatched
        '
        Me.rbTvDisplayUnWatched.AutoSize = true
        Me.rbTvDisplayUnWatched.Location = New System.Drawing.Point(83, 26)
        Me.rbTvDisplayUnWatched.Name = "rbTvDisplayUnWatched"
        Me.rbTvDisplayUnWatched.Size = New System.Drawing.Size(89, 19)
        Me.rbTvDisplayUnWatched.TabIndex = 12
        Me.rbTvDisplayUnWatched.Text = "UnWatched"
        Me.rbTvDisplayUnWatched.UseVisualStyleBackColor = true
        '
        'rbTvListAll
        '
        Me.rbTvListAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvListAll.AutoSize = true
        Me.rbTvListAll.Checked = true
        Me.rbTvListAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvListAll.Location = New System.Drawing.Point(9, 4)
        Me.rbTvListAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvListAll.Name = "rbTvListAll"
        Me.rbTvListAll.Size = New System.Drawing.Size(60, 19)
        Me.rbTvListAll.TabIndex = 5
        Me.rbTvListAll.TabStop = true
        Me.rbTvListAll.Text = "List All"
        Me.rbTvListAll.UseVisualStyleBackColor = true
        '
        'rbTvMissingAiredEp
        '
        Me.rbTvMissingAiredEp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvMissingAiredEp.AutoSize = true
        Me.rbTvMissingAiredEp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvMissingAiredEp.Location = New System.Drawing.Point(9, 65)
        Me.rbTvMissingAiredEp.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvMissingAiredEp.Name = "rbTvMissingAiredEp"
        Me.rbTvMissingAiredEp.Size = New System.Drawing.Size(153, 19)
        Me.rbTvMissingAiredEp.TabIndex = 10
        Me.rbTvMissingAiredEp.Text = "Missing Aired Episodes"
        Me.rbTvMissingAiredEp.UseVisualStyleBackColor = true
        '
        'rbTvDisplayWatched
        '
        Me.rbTvDisplayWatched.AutoSize = true
        Me.rbTvDisplayWatched.Location = New System.Drawing.Point(9, 26)
        Me.rbTvDisplayWatched.Name = "rbTvDisplayWatched"
        Me.rbTvDisplayWatched.Size = New System.Drawing.Size(73, 19)
        Me.rbTvDisplayWatched.TabIndex = 11
        Me.rbTvDisplayWatched.Text = "Watched"
        Me.rbTvDisplayWatched.UseVisualStyleBackColor = true
        '
        'rbTvMissingFanart
        '
        Me.rbTvMissingFanart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvMissingFanart.AutoSize = true
        Me.rbTvMissingFanart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvMissingFanart.Location = New System.Drawing.Point(179, 26)
        Me.rbTvMissingFanart.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvMissingFanart.Name = "rbTvMissingFanart"
        Me.rbTvMissingFanart.Size = New System.Drawing.Size(106, 19)
        Me.rbTvMissingFanart.TabIndex = 6
        Me.rbTvMissingFanart.Text = "Missing Fanart"
        Me.rbTvMissingFanart.UseVisualStyleBackColor = true
        '
        'rbTvMissingThumb
        '
        Me.rbTvMissingThumb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.rbTvMissingThumb.AutoSize = true
        Me.rbTvMissingThumb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvMissingThumb.Location = New System.Drawing.Point(179, 46)
        Me.rbTvMissingThumb.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvMissingThumb.Name = "rbTvMissingThumb"
        Me.rbTvMissingThumb.Size = New System.Drawing.Size(128, 19)
        Me.rbTvMissingThumb.TabIndex = 8
        Me.rbTvMissingThumb.Text = "Missing Ep Thumb"
        Me.rbTvMissingThumb.UseVisualStyleBackColor = true
        '
        'rbTvMissingEpisodes
        '
        Me.rbTvMissingEpisodes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.rbTvMissingEpisodes.AutoSize = true
        Me.rbTvMissingEpisodes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbTvMissingEpisodes.Location = New System.Drawing.Point(9, 46)
        Me.rbTvMissingEpisodes.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTvMissingEpisodes.Name = "rbTvMissingEpisodes"
        Me.rbTvMissingEpisodes.Size = New System.Drawing.Size(122, 19)
        Me.rbTvMissingEpisodes.TabIndex = 9
        Me.rbTvMissingEpisodes.Text = "Missing Episodes"
        Me.rbTvMissingEpisodes.UseVisualStyleBackColor = true
        '
        'Panel8
        '
        Me.Panel8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Panel8.Controls.Add(Me.TableLayoutPanel30)
        Me.Panel8.Location = New System.Drawing.Point(532, 354)
        Me.Panel8.MaximumSize = New System.Drawing.Size(179, 267)
        Me.Panel8.MinimumSize = New System.Drawing.Size(179, 267)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(179, 267)
        Me.Panel8.TabIndex = 130
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.ColumnCount = 4
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.797101!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94.2029!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel30.Controls.Add(Me.Label50, 1, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.Label51, 1, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.pbEpActorImage, 1, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.tbEpRole, 2, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.cmbxEpActor, 2, 0)
        Me.TableLayoutPanel30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 5
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.79365!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.20635!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6!))
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(179, 267)
        Me.TableLayoutPanel30.TabIndex = 0
        '
        'Label50
        '
        Me.Label50.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label50.AutoSize = true
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label50.Location = New System.Drawing.Point(3, 10)
        Me.Label50.Margin = New System.Windows.Forms.Padding(0)
        Me.Label50.Name = "Label50"
        Me.Label50.Padding = New System.Windows.Forms.Padding(0, 0, 0, 7)
        Me.Label50.Size = New System.Drawing.Size(40, 22)
        Me.Label50.TabIndex = 11
        Me.Label50.Text = "Actor :"
        '
        'Label51
        '
        Me.Label51.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label51.AutoSize = true
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label51.Location = New System.Drawing.Point(4, 41)
        Me.Label51.Margin = New System.Windows.Forms.Padding(0)
        Me.Label51.Name = "Label51"
        Me.Label51.Padding = New System.Windows.Forms.Padding(0, 0, 0, 7)
        Me.Label51.Size = New System.Drawing.Size(39, 22)
        Me.Label51.TabIndex = 12
        Me.Label51.Text = "Role :"
        '
        'pbEpActorImage
        '
        Me.pbEpActorImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pbEpActorImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel30.SetColumnSpan(Me.pbEpActorImage, 2)
        Me.pbEpActorImage.Location = New System.Drawing.Point(7, 67)
        Me.pbEpActorImage.Margin = New System.Windows.Forms.Padding(4)
        Me.pbEpActorImage.Name = "pbEpActorImage"
        Me.TableLayoutPanel30.SetRowSpan(Me.pbEpActorImage, 2)
        Me.pbEpActorImage.Size = New System.Drawing.Size(163, 189)
        Me.pbEpActorImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbEpActorImage.TabIndex = 15
        Me.pbEpActorImage.TabStop = false
        '
        'tbEpRole
        '
        Me.tbEpRole.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tbEpRole.BackColor = System.Drawing.Color.White
        Me.tbEpRole.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tbEpRole.Location = New System.Drawing.Point(47, 38)
        Me.tbEpRole.Margin = New System.Windows.Forms.Padding(4)
        Me.tbEpRole.Name = "tbEpRole"
        Me.tbEpRole.ReadOnly = true
        Me.tbEpRole.Size = New System.Drawing.Size(123, 21)
        Me.tbEpRole.TabIndex = 14
        '
        'cmbxEpActor
        '
        Me.cmbxEpActor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbxEpActor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxEpActor.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbxEpActor.FormattingEnabled = true
        Me.cmbxEpActor.Location = New System.Drawing.Point(47, 5)
        Me.cmbxEpActor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbxEpActor.Name = "cmbxEpActor"
        Me.cmbxEpActor.Size = New System.Drawing.Size(123, 23)
        Me.cmbxEpActor.TabIndex = 13
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Panel9.AutoScroll = true
        Me.Panel9.Controls.Add(Me.TableLayoutPanel19)
        Me.Panel9.Location = New System.Drawing.Point(3, 388)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel9.MinimumSize = New System.Drawing.Size(527, 225)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(527, 225)
        Me.Panel9.TabIndex = 34
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.ColumnCount = 9
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel19.Controls.Add(Me.Label160, 0, 5)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpPlot, 1, 1)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpFilename, 1, 4)
        Me.TableLayoutPanel19.Controls.Add(Me.Label40, 0, 4)
        Me.TableLayoutPanel19.Controls.Add(Me.Label36, 0, 3)
        Me.TableLayoutPanel19.Controls.Add(Me.btn_EpWatched, 7, 2)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpPath, 1, 3)
        Me.TableLayoutPanel19.Controls.Add(Me.Label49, 0, 2)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpCredits, 5, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpAired, 1, 2)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpDirector, 1, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.Label45, 3, 2)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_EpRating, 4, 2)
        Me.TableLayoutPanel19.Controls.Add(Me.Label47, 4, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.Label46, 0, 1)
        Me.TableLayoutPanel19.Controls.Add(Me.lb_EpDetails, 6, 3)
        Me.TableLayoutPanel19.Controls.Add(Me.cbTvSource, 1, 5)
        Me.TableLayoutPanel19.Controls.Add(Me.Label48, 0, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.lbl_airepisode, 7, 5)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_airepisode, 8, 5)
        Me.TableLayoutPanel19.Controls.Add(Me.lbl_airbefore, 4, 5)
        Me.TableLayoutPanel19.Controls.Add(Me.lbl_airseason, 5, 5)
        Me.TableLayoutPanel19.Controls.Add(Me.tb_airseason, 6, 5)
        Me.TableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel19.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 6
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(527, 225)
        Me.TableLayoutPanel19.TabIndex = 24
        '
        'Label160
        '
        Me.Label160.AutoSize = true
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label160.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label160.Location = New System.Drawing.Point(30, 198)
        Me.Label160.Margin = New System.Windows.Forms.Padding(0)
        Me.Label160.Name = "Label160"
        Me.Label160.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label160.Size = New System.Drawing.Size(52, 27)
        Me.Label160.TabIndex = 23
        Me.Label160.Text = "Source :"
        '
        'tb_EpPlot
        '
        Me.tb_EpPlot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpPlot, 8)
        Me.tb_EpPlot.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpPlot.Location = New System.Drawing.Point(86, 33)
        Me.tb_EpPlot.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpPlot.Multiline = true
        Me.tb_EpPlot.Name = "tb_EpPlot"
        Me.tb_EpPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tb_EpPlot.Size = New System.Drawing.Size(437, 73)
        Me.tb_EpPlot.TabIndex = 7
        '
        'tb_EpFilename
        '
        Me.tb_EpFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tb_EpFilename.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpFilename, 5)
        Me.tb_EpFilename.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpFilename.Location = New System.Drawing.Point(86, 173)
        Me.tb_EpFilename.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpFilename.Name = "tb_EpFilename"
        Me.tb_EpFilename.ReadOnly = true
        Me.tb_EpFilename.Size = New System.Drawing.Size(287, 21)
        Me.tb_EpFilename.TabIndex = 18
        '
        'Label40
        '
        Me.Label40.AutoSize = true
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label40.Location = New System.Drawing.Point(17, 167)
        Me.Label40.Margin = New System.Windows.Forms.Padding(0)
        Me.Label40.Name = "Label40"
        Me.Label40.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label40.Size = New System.Drawing.Size(65, 31)
        Me.Label40.TabIndex = 19
        Me.Label40.Text = "Filename :"
        '
        'Label36
        '
        Me.Label36.AutoSize = true
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label36.Location = New System.Drawing.Point(44, 137)
        Me.Label36.Margin = New System.Windows.Forms.Padding(0)
        Me.Label36.Name = "Label36"
        Me.Label36.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label36.Size = New System.Drawing.Size(38, 30)
        Me.Label36.TabIndex = 16
        Me.Label36.Text = "Path :"
        '
        'btn_EpWatched
        '
        Me.btn_EpWatched.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_EpWatched.BackColor = System.Drawing.Color.Red
        Me.TableLayoutPanel19.SetColumnSpan(Me.btn_EpWatched, 2)
        Me.btn_EpWatched.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_EpWatched.Location = New System.Drawing.Point(431, 110)
        Me.btn_EpWatched.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_EpWatched.Name = "btn_EpWatched"
        Me.btn_EpWatched.Size = New System.Drawing.Size(96, 27)
        Me.btn_EpWatched.TabIndex = 20
        Me.btn_EpWatched.UseVisualStyleBackColor = false
        '
        'tb_EpPath
        '
        Me.tb_EpPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tb_EpPath.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpPath, 5)
        Me.tb_EpPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpPath.Location = New System.Drawing.Point(86, 142)
        Me.tb_EpPath.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpPath.Name = "tb_EpPath"
        Me.tb_EpPath.ReadOnly = true
        Me.tb_EpPath.Size = New System.Drawing.Size(287, 21)
        Me.tb_EpPath.TabIndex = 17
        '
        'Label49
        '
        Me.Label49.AutoSize = true
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label49.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label49.Location = New System.Drawing.Point(41, 110)
        Me.Label49.Margin = New System.Windows.Forms.Padding(0)
        Me.Label49.Name = "Label49"
        Me.Label49.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label49.Size = New System.Drawing.Size(41, 27)
        Me.Label49.TabIndex = 4
        Me.Label49.Text = "Aired :"
        '
        'tb_EpCredits
        '
        Me.tb_EpCredits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tb_EpCredits.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpCredits, 4)
        Me.tb_EpCredits.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpCredits.Location = New System.Drawing.Point(325, 4)
        Me.tb_EpCredits.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpCredits.Name = "tb_EpCredits"
        Me.tb_EpCredits.Size = New System.Drawing.Size(198, 21)
        Me.tb_EpCredits.TabIndex = 9
        '
        'tb_EpAired
        '
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpAired, 2)
        Me.tb_EpAired.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpAired.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpAired.Location = New System.Drawing.Point(86, 114)
        Me.tb_EpAired.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpAired.Name = "tb_EpAired"
        Me.tb_EpAired.Size = New System.Drawing.Size(112, 21)
        Me.tb_EpAired.TabIndex = 10
        '
        'tb_EpDirector
        '
        Me.tb_EpDirector.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.tb_EpDirector.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpDirector, 3)
        Me.tb_EpDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpDirector.Location = New System.Drawing.Point(86, 4)
        Me.tb_EpDirector.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpDirector.Name = "tb_EpDirector"
        Me.tb_EpDirector.Size = New System.Drawing.Size(150, 21)
        Me.tb_EpDirector.TabIndex = 8
        '
        'Label45
        '
        Me.Label45.AutoSize = true
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label45.Location = New System.Drawing.Point(202, 110)
        Me.Label45.Margin = New System.Windows.Forms.Padding(0)
        Me.Label45.Name = "Label45"
        Me.Label45.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label45.Size = New System.Drawing.Size(46, 27)
        Me.Label45.TabIndex = 0
        Me.Label45.Text = "Rating :"
        '
        'tb_EpRating
        '
        Me.tb_EpRating.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel19.SetColumnSpan(Me.tb_EpRating, 2)
        Me.tb_EpRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_EpRating.Location = New System.Drawing.Point(252, 114)
        Me.tb_EpRating.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_EpRating.Name = "tb_EpRating"
        Me.tb_EpRating.Size = New System.Drawing.Size(121, 21)
        Me.tb_EpRating.TabIndex = 6
        '
        'Label47
        '
        Me.Label47.AutoSize = true
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label47.Location = New System.Drawing.Point(270, 0)
        Me.Label47.Margin = New System.Windows.Forms.Padding(0)
        Me.Label47.Name = "Label47"
        Me.Label47.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label47.Size = New System.Drawing.Size(51, 28)
        Me.Label47.TabIndex = 2
        Me.Label47.Text = "Credits :"
        '
        'Label46
        '
        Me.Label46.AutoSize = true
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label46.Location = New System.Drawing.Point(48, 28)
        Me.Label46.Margin = New System.Windows.Forms.Padding(0)
        Me.Label46.Name = "Label46"
        Me.Label46.Padding = New System.Windows.Forms.Padding(0, 15, 0, 0)
        Me.Label46.Size = New System.Drawing.Size(34, 82)
        Me.Label46.TabIndex = 1
        Me.Label46.Text = "Plot :"
        '
        'lb_EpDetails
        '
        Me.TableLayoutPanel19.SetColumnSpan(Me.lb_EpDetails, 3)
        Me.lb_EpDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lb_EpDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lb_EpDetails.FormattingEnabled = true
        Me.lb_EpDetails.Location = New System.Drawing.Point(380, 140)
        Me.lb_EpDetails.Name = "lb_EpDetails"
        Me.TableLayoutPanel19.SetRowSpan(Me.lb_EpDetails, 2)
        Me.lb_EpDetails.Size = New System.Drawing.Size(144, 55)
        Me.lb_EpDetails.TabIndex = 24
        '
        'cbTvSource
        '
        Me.TableLayoutPanel19.SetColumnSpan(Me.cbTvSource, 2)
        Me.cbTvSource.FormattingEnabled = true
        Me.cbTvSource.Location = New System.Drawing.Point(85, 201)
        Me.cbTvSource.Name = "cbTvSource"
        Me.cbTvSource.Size = New System.Drawing.Size(114, 23)
        Me.cbTvSource.TabIndex = 25
        '
        'Label48
        '
        Me.Label48.AutoSize = true
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label48.Location = New System.Drawing.Point(26, 0)
        Me.Label48.Margin = New System.Windows.Forms.Padding(0)
        Me.Label48.Name = "Label48"
        Me.Label48.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label48.Size = New System.Drawing.Size(56, 28)
        Me.Label48.TabIndex = 3
        Me.Label48.Text = "Director :"
        '
        'lbl_airepisode
        '
        Me.lbl_airepisode.AutoSize = true
        Me.lbl_airepisode.Location = New System.Drawing.Point(426, 198)
        Me.lbl_airepisode.Name = "lbl_airepisode"
        Me.lbl_airepisode.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.lbl_airepisode.Size = New System.Drawing.Size(52, 21)
        Me.lbl_airepisode.TabIndex = 27
        Me.lbl_airepisode.Text = "Episode"
        '
        'tb_airepisode
        '
        Me.tb_airepisode.Location = New System.Drawing.Point(486, 201)
        Me.tb_airepisode.Name = "tb_airepisode"
        Me.tb_airepisode.Size = New System.Drawing.Size(37, 21)
        Me.tb_airepisode.TabIndex = 26
        '
        'lbl_airbefore
        '
        Me.lbl_airbefore.AutoSize = true
        Me.lbl_airbefore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_airbefore.Location = New System.Drawing.Point(251, 198)
        Me.lbl_airbefore.Name = "lbl_airbefore"
        Me.lbl_airbefore.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.lbl_airbefore.Size = New System.Drawing.Size(67, 27)
        Me.lbl_airbefore.TabIndex = 28
        Me.lbl_airbefore.Text = "Airs Before"
        '
        'lbl_airseason
        '
        Me.lbl_airseason.AutoSize = true
        Me.lbl_airseason.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_airseason.Location = New System.Drawing.Point(325, 198)
        Me.lbl_airseason.Name = "lbl_airseason"
        Me.lbl_airseason.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.lbl_airseason.Size = New System.Drawing.Size(49, 27)
        Me.lbl_airseason.TabIndex = 29
        Me.lbl_airseason.Text = "Season"
        '
        'tb_airseason
        '
        Me.tb_airseason.Location = New System.Drawing.Point(380, 201)
        Me.tb_airseason.Name = "tb_airseason"
        Me.tb_airseason.Size = New System.Drawing.Size(40, 21)
        Me.tb_airseason.TabIndex = 30
        '
        'pbtvfanarttv
        '
        Me.pbtvfanarttv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pbtvfanarttv.BackColor = System.Drawing.Color.Transparent
        Me.pbtvfanarttv.Location = New System.Drawing.Point(120, 90)
        Me.pbtvfanarttv.Name = "pbtvfanarttv"
        Me.pbtvfanarttv.Size = New System.Drawing.Size(271, 198)
        Me.pbtvfanarttv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbtvfanarttv.TabIndex = 129
        Me.pbtvfanarttv.TabStop = false
        Me.pbtvfanarttv.Visible = false
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 13
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel20.Controls.Add(Me.Panel7, 11, 5)
        Me.TableLayoutPanel20.Controls.Add(Me.Button47, 11, 1)
        Me.TableLayoutPanel20.Controls.Add(Me.Label67, 8, 1)
        Me.TableLayoutPanel20.Controls.Add(Me.Button_TV_State, 11, 0)
        Me.TableLayoutPanel20.Controls.Add(Me._tv_SplitContainer, 1, 1)
        Me.TableLayoutPanel20.Controls.Add(Me.Button_Save_TvShow_Episode, 6, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.Button44, 5, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_Sh_Ep_Title, 1, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShStudio, 4, 11)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShRating, 4, 12)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShGenre, 4, 13)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShCert, 4, 14)
        Me.TableLayoutPanel20.Controls.Add(Me.Label35, 3, 11)
        Me.TableLayoutPanel20.Controls.Add(Me.Label29, 3, 12)
        Me.TableLayoutPanel20.Controls.Add(Me.Label25, 3, 13)
        Me.TableLayoutPanel20.Controls.Add(Me.Label33, 3, 14)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShPlot, 2, 9)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShTvdbId, 2, 13)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShImdbId, 2, 14)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShPremiered, 2, 12)
        Me.TableLayoutPanel20.Controls.Add(Me.Label28, 1, 14)
        Me.TableLayoutPanel20.Controls.Add(Me.Label26, 1, 13)
        Me.TableLayoutPanel20.Controls.Add(Me.Label21, 1, 12)
        Me.TableLayoutPanel20.Controls.Add(Me.Label34, 1, 11)
        Me.TableLayoutPanel20.Controls.Add(Me.Label44, 1, 9)
        Me.TableLayoutPanel20.Controls.Add(Me.tb_ShRunTime, 2, 11)
        Me.TableLayoutPanel20.Controls.Add(Me.Label43, 7, 8)
        Me.TableLayoutPanel20.Controls.Add(Me.Label42, 7, 7)
        Me.TableLayoutPanel20.Controls.Add(Me.gpbxActorSource, 8, 6)
        Me.TableLayoutPanel20.Controls.Add(Me.PictureBox6, 8, 9)
        Me.TableLayoutPanel20.Controls.Add(Me.Label8, 8, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.cbTvActor, 9, 7)
        Me.TableLayoutPanel20.Controls.Add(Me.cbTvActorRole, 9, 8)
        Me.TableLayoutPanel20.Controls.Add(Me.lbl_sorttitle, 8, 3)
        Me.TableLayoutPanel20.Controls.Add(Me.TextBox_Sorttitle, 7, 4)
        Me.TableLayoutPanel20.Controls.Add(Me.Label131, 8, 2)
        Me.TableLayoutPanel20.Controls.Add(Me.bnt_TvSeriesStatus, 11, 2)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 16
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(713, 620)
        Me.TableLayoutPanel20.TabIndex = 49
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.tvFanlistbox)
        Me.Panel7.Controls.Add(Me.Label17)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(625, 150)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(80, 128)
        Me.Panel7.TabIndex = 187
        '
        'tvFanlistbox
        '
        Me.tvFanlistbox.BackColor = System.Drawing.SystemColors.MenuBar
        Me.tvFanlistbox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvFanlistbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tvFanlistbox.FormattingEnabled = true
        Me.tvFanlistbox.Location = New System.Drawing.Point(1, 44)
        Me.tvFanlistbox.Name = "tvFanlistbox"
        Me.tvFanlistbox.Size = New System.Drawing.Size(62, 78)
        Me.tvFanlistbox.TabIndex = 1
        Me.tvFanlistbox.TabStop = false
        Me.tvFanlistbox.UseTabStops = false
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.Location = New System.Drawing.Point(7, 5)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 28)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Extra"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Artwork"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button47
        '
        Me.Button47.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button47.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button47.Location = New System.Drawing.Point(626, 42)
        Me.Button47.Margin = New System.Windows.Forms.Padding(4)
        Me.Button47.Name = "Button47"
        Me.Button47.Size = New System.Drawing.Size(78, 22)
        Me.Button47.TabIndex = 43
        Me.Button47.Text = "Default"
        Me.Button47.UseVisualStyleBackColor = true
        '
        'Label67
        '
        Me.Label67.AutoSize = true
        Me.TableLayoutPanel20.SetColumnSpan(Me.Label67, 3)
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label67.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label67.Location = New System.Drawing.Point(553, 38)
        Me.Label67.Margin = New System.Windows.Forms.Padding(0)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(69, 30)
        Me.Label67.TabIndex = 40
        Me.Label67.Text = "Sort Order :"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button_TV_State
        '
        Me.Button_TV_State.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button_TV_State.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button_TV_State.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_TV_State.ImageIndex = 0
        Me.Button_TV_State.ImageList = Me.ImageList2
        Me.Button_TV_State.Location = New System.Drawing.Point(629, 4)
        Me.Button_TV_State.Margin = New System.Windows.Forms.Padding(4)
        Me.Button_TV_State.Name = "Button_TV_State"
        Me.Button_TV_State.Size = New System.Drawing.Size(75, 30)
        Me.Button_TV_State.TabIndex = 45
        Me.Button_TV_State.Text = "Open"
        Me.Button_TV_State.UseVisualStyleBackColor = true
        '
        '_tv_SplitContainer
        '
        Me._tv_SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TableLayoutPanel20.SetColumnSpan(Me._tv_SplitContainer, 6)
        Me._tv_SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me._tv_SplitContainer.Location = New System.Drawing.Point(8, 41)
        Me._tv_SplitContainer.Name = "_tv_SplitContainer"
        Me._tv_SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '_tv_SplitContainer.Panel1
        '
        Me._tv_SplitContainer.Panel1.Controls.Add(Me.SplitContainer4)
        '
        '_tv_SplitContainer.Panel2
        '
        Me._tv_SplitContainer.Panel2.Controls.Add(Me.tv_PictureBoxBottom)
        Me.TableLayoutPanel20.SetRowSpan(Me._tv_SplitContainer, 7)
        Me._tv_SplitContainer.Size = New System.Drawing.Size(518, 340)
        Me._tv_SplitContainer.SplitterDistance = 156
        Me._tv_SplitContainer.TabIndex = 11
        '
        'SplitContainer4
        '
        Me.SplitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.tv_PictureBoxLeft)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.tv_PictureBoxRight)
        Me.SplitContainer4.Size = New System.Drawing.Size(518, 156)
        Me.SplitContainer4.SplitterDistance = 279
        Me.SplitContainer4.SplitterWidth = 5
        Me.SplitContainer4.TabIndex = 50
        '
        'tv_PictureBoxLeft
        '
        Me.tv_PictureBoxLeft.ContextMenuStrip = Me.TvEpContextMenuStrip
        Me.tv_PictureBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_PictureBoxLeft.Location = New System.Drawing.Point(0, 0)
        Me.tv_PictureBoxLeft.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_PictureBoxLeft.Name = "tv_PictureBoxLeft"
        Me.tv_PictureBoxLeft.Size = New System.Drawing.Size(275, 152)
        Me.tv_PictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.tv_PictureBoxLeft.TabIndex = 0
        Me.tv_PictureBoxLeft.TabStop = false
        Me.tv_PictureBoxLeft.WaitOnLoad = true
        '
        'TvEpContextMenuStrip
        '
        Me.TvEpContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReScrFanartToolStripMenuItem, Me.SelNewFanartToolStripMenuItem, Me.RescrapeTvEpThumbToolStripMenuItem, Me.RescrapeTvEpScreenShotToolStripMenuItem})
        Me.TvEpContextMenuStrip.Name = "TvEpContextMenuStrip"
        Me.TvEpContextMenuStrip.Size = New System.Drawing.Size(206, 92)
        '
        'ReScrFanartToolStripMenuItem
        '
        Me.ReScrFanartToolStripMenuItem.Name = "ReScrFanartToolStripMenuItem"
        Me.ReScrFanartToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ReScrFanartToolStripMenuItem.Text = "Rescrape Show Fanart"
        '
        'SelNewFanartToolStripMenuItem
        '
        Me.SelNewFanartToolStripMenuItem.Name = "SelNewFanartToolStripMenuItem"
        Me.SelNewFanartToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.SelNewFanartToolStripMenuItem.Text = "Select New Fanart"
        '
        'RescrapeTvEpThumbToolStripMenuItem
        '
        Me.RescrapeTvEpThumbToolStripMenuItem.Name = "RescrapeTvEpThumbToolStripMenuItem"
        Me.RescrapeTvEpThumbToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.RescrapeTvEpThumbToolStripMenuItem.Text = "Rescrape Episode Thumb"
        '
        'RescrapeTvEpScreenShotToolStripMenuItem
        '
        Me.RescrapeTvEpScreenShotToolStripMenuItem.Name = "RescrapeTvEpScreenShotToolStripMenuItem"
        Me.RescrapeTvEpScreenShotToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.RescrapeTvEpScreenShotToolStripMenuItem.Text = "Create Episode ScreenShot"
        '
        'tv_PictureBoxRight
        '
        Me.tv_PictureBoxRight.ContextMenuStrip = Me.TvPosterContextMenuStrip
        Me.tv_PictureBoxRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_PictureBoxRight.Location = New System.Drawing.Point(0, 0)
        Me.tv_PictureBoxRight.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_PictureBoxRight.Name = "tv_PictureBoxRight"
        Me.tv_PictureBoxRight.Size = New System.Drawing.Size(230, 152)
        Me.tv_PictureBoxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.tv_PictureBoxRight.TabIndex = 0
        Me.tv_PictureBoxRight.TabStop = false
        '
        'TvPosterContextMenuStrip
        '
        Me.TvPosterContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_TvScrapePoster, Me.tsm_TvSelectPoster, Me.tsm_TvScrapeBanner, Me.tsm_TvSelectBanner})
        Me.TvPosterContextMenuStrip.Name = "TvPosterContextMenuStrip"
        Me.TvPosterContextMenuStrip.Size = New System.Drawing.Size(171, 92)
        '
        'tsm_TvScrapePoster
        '
        Me.tsm_TvScrapePoster.Name = "tsm_TvScrapePoster"
        Me.tsm_TvScrapePoster.Size = New System.Drawing.Size(170, 22)
        Me.tsm_TvScrapePoster.Text = "Scrape poster"
        '
        'tsm_TvSelectPoster
        '
        Me.tsm_TvSelectPoster.Name = "tsm_TvSelectPoster"
        Me.tsm_TvSelectPoster.Size = New System.Drawing.Size(170, 22)
        Me.tsm_TvSelectPoster.Text = "Select from Posters"
        '
        'tsm_TvScrapeBanner
        '
        Me.tsm_TvScrapeBanner.Name = "tsm_TvScrapeBanner"
        Me.tsm_TvScrapeBanner.Size = New System.Drawing.Size(170, 22)
        Me.tsm_TvScrapeBanner.Text = "Scrape Banner"
        '
        'tsm_TvSelectBanner
        '
        Me.tsm_TvSelectBanner.Name = "tsm_TvSelectBanner"
        Me.tsm_TvSelectBanner.Size = New System.Drawing.Size(170, 22)
        Me.tsm_TvSelectBanner.Text = "Select from Banners"
        '
        'tv_PictureBoxBottom
        '
        Me.tv_PictureBoxBottom.ContextMenuStrip = Me.TvPosterContextMenuStrip
        Me.tv_PictureBoxBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_PictureBoxBottom.Location = New System.Drawing.Point(0, 0)
        Me.tv_PictureBoxBottom.Margin = New System.Windows.Forms.Padding(2)
        Me.tv_PictureBoxBottom.Name = "tv_PictureBoxBottom"
        Me.tv_PictureBoxBottom.Size = New System.Drawing.Size(514, 176)
        Me.tv_PictureBoxBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.tv_PictureBoxBottom.TabIndex = 47
        Me.tv_PictureBoxBottom.TabStop = false
        '
        'tb_ShStudio
        '
        Me.TableLayoutPanel20.SetColumnSpan(Me.tb_ShStudio, 3)
        Me.tb_ShStudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShStudio.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShStudio.Location = New System.Drawing.Point(302, 501)
        Me.tb_ShStudio.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShStudio.Name = "tb_ShStudio"
        Me.tb_ShStudio.Size = New System.Drawing.Size(223, 21)
        Me.tb_ShStudio.TabIndex = 15
        '
        'tb_ShRating
        '
        Me.TableLayoutPanel20.SetColumnSpan(Me.tb_ShRating, 3)
        Me.tb_ShRating.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShRating.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShRating.Location = New System.Drawing.Point(302, 528)
        Me.tb_ShRating.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShRating.Name = "tb_ShRating"
        Me.tb_ShRating.Size = New System.Drawing.Size(223, 21)
        Me.tb_ShRating.TabIndex = 6
        '
        'tb_ShGenre
        '
        Me.tb_ShGenre.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel20.SetColumnSpan(Me.tb_ShGenre, 3)
        Me.tb_ShGenre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShGenre.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShGenre.Location = New System.Drawing.Point(302, 558)
        Me.tb_ShGenre.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShGenre.Name = "tb_ShGenre"
        Me.tb_ShGenre.Size = New System.Drawing.Size(223, 21)
        Me.tb_ShGenre.TabIndex = 4
        '
        'tb_ShCert
        '
        Me.TableLayoutPanel20.SetColumnSpan(Me.tb_ShCert, 3)
        Me.tb_ShCert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShCert.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShCert.Location = New System.Drawing.Point(302, 589)
        Me.tb_ShCert.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShCert.Name = "tb_ShCert"
        Me.tb_ShCert.Size = New System.Drawing.Size(223, 21)
        Me.tb_ShCert.TabIndex = 7
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoSize = true
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label35.Location = New System.Drawing.Point(246, 505)
        Me.Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(48, 15)
        Me.Label35.TabIndex = 21
        Me.Label35.Text = "Studio :"
        '
        'Label29
        '
        Me.Label29.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = true
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label29.Location = New System.Drawing.Point(245, 535)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(49, 15)
        Me.Label29.TabIndex = 12
        Me.Label29.Text = "Rating :"
        '
        'Label25
        '
        Me.Label25.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label25.AutoSize = true
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label25.Location = New System.Drawing.Point(247, 566)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(47, 15)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Genre :"
        '
        'Label33
        '
        Me.Label33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label33.AutoSize = true
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label33.Location = New System.Drawing.Point(259, 593)
        Me.Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(35, 15)
        Me.Label33.TabIndex = 13
        Me.Label33.Text = "Cert :"
        '
        'tb_ShPlot
        '
        Me.TableLayoutPanel20.SetColumnSpan(Me.tb_ShPlot, 5)
        Me.tb_ShPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShPlot.Location = New System.Drawing.Point(85, 415)
        Me.tb_ShPlot.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.tb_ShPlot.Multiline = true
        Me.tb_ShPlot.Name = "tb_ShPlot"
        Me.TableLayoutPanel20.SetRowSpan(Me.tb_ShPlot, 2)
        Me.tb_ShPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tb_ShPlot.Size = New System.Drawing.Size(440, 82)
        Me.tb_ShPlot.TabIndex = 32
        '
        'tb_ShTvdbId
        '
        Me.tb_ShTvdbId.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.tb_ShTvdbId.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShTvdbId.Location = New System.Drawing.Point(89, 560)
        Me.tb_ShTvdbId.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShTvdbId.Name = "tb_ShTvdbId"
        Me.tb_ShTvdbId.Size = New System.Drawing.Size(117, 21)
        Me.tb_ShTvdbId.TabIndex = 2
        '
        'tb_ShImdbId
        '
        Me.tb_ShImdbId.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.tb_ShImdbId.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShImdbId.Location = New System.Drawing.Point(89, 589)
        Me.tb_ShImdbId.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShImdbId.Name = "tb_ShImdbId"
        Me.tb_ShImdbId.Size = New System.Drawing.Size(117, 21)
        Me.tb_ShImdbId.TabIndex = 5
        '
        'tb_ShPremiered
        '
        Me.tb_ShPremiered.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.tb_ShPremiered.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShPremiered.Location = New System.Drawing.Point(89, 529)
        Me.tb_ShPremiered.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_ShPremiered.Name = "tb_ShPremiered"
        Me.tb_ShPremiered.Size = New System.Drawing.Size(117, 21)
        Me.tb_ShPremiered.TabIndex = 3
        '
        'Label28
        '
        Me.Label28.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label28.AutoSize = true
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.Location = New System.Drawing.Point(37, 593)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(44, 15)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "IMDB :"
        '
        'Label26
        '
        Me.Label26.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label26.AutoSize = true
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.Location = New System.Drawing.Point(37, 566)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(44, 15)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "TVDB :"
        '
        'Label21
        '
        Me.Label21.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label21.AutoSize = true
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label21.Location = New System.Drawing.Point(10, 535)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(71, 15)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "Premiered :"
        '
        'Label34
        '
        Me.Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label34.AutoSize = true
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label34.Location = New System.Drawing.Point(21, 505)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 4)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(60, 15)
        Me.Label34.TabIndex = 20
        Me.Label34.Text = "Runtime :"
        '
        'Label44
        '
        Me.Label44.AutoSize = true
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label44.Location = New System.Drawing.Point(51, 415)
        Me.Label44.Margin = New System.Windows.Forms.Padding(0)
        Me.Label44.Name = "Label44"
        Me.Label44.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label44.Size = New System.Drawing.Size(34, 57)
        Me.Label44.TabIndex = 33
        Me.Label44.Text = "Plot :"
        '
        'tb_ShRunTime
        '
        Me.tb_ShRunTime.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_ShRunTime.Location = New System.Drawing.Point(89, 501)
        Me.tb_ShRunTime.Margin = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.tb_ShRunTime.Name = "tb_ShRunTime"
        Me.tb_ShRunTime.Size = New System.Drawing.Size(117, 21)
        Me.tb_ShRunTime.TabIndex = 14
        '
        'Label43
        '
        Me.Label43.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label43.AutoSize = true
        Me.TableLayoutPanel20.SetColumnSpan(Me.Label43, 2)
        Me.Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label43.Location = New System.Drawing.Point(537, 393)
        Me.Label43.Margin = New System.Windows.Forms.Padding(0)
        Me.Label43.Name = "Label43"
        Me.Label43.Padding = New System.Windows.Forms.Padding(0, 0, 0, 7)
        Me.Label43.Size = New System.Drawing.Size(39, 22)
        Me.Label43.TabIndex = 30
        Me.Label43.Text = "Role :"
        '
        'Label42
        '
        Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label42.AutoSize = true
        Me.TableLayoutPanel20.SetColumnSpan(Me.Label42, 2)
        Me.Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label42.Location = New System.Drawing.Point(536, 362)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Padding = New System.Windows.Forms.Padding(0, 0, 0, 7)
        Me.Label42.Size = New System.Drawing.Size(40, 22)
        Me.Label42.TabIndex = 28
        Me.Label42.Text = "Actor :"
        '
        'gpbxActorSource
        '
        Me.gpbxActorSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel20.SetColumnSpan(Me.gpbxActorSource, 4)
        Me.gpbxActorSource.Controls.Add(Me.Button46)
        Me.gpbxActorSource.Controls.Add(Me.Button45)
        Me.gpbxActorSource.Controls.Add(Me.Label41)
        Me.gpbxActorSource.Controls.Add(Me.Label66)
        Me.gpbxActorSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gpbxActorSource.Location = New System.Drawing.Point(564, 284)
        Me.gpbxActorSource.Name = "gpbxActorSource"
        Me.gpbxActorSource.Size = New System.Drawing.Size(141, 66)
        Me.gpbxActorSource.TabIndex = 48
        Me.gpbxActorSource.TabStop = false
        Me.gpbxActorSource.Text = "Actor Source"
        '
        'Button46
        '
        Me.Button46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button46.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button46.Location = New System.Drawing.Point(65, 39)
        Me.Button46.Margin = New System.Windows.Forms.Padding(4)
        Me.Button46.Name = "Button46"
        Me.Button46.Size = New System.Drawing.Size(70, 22)
        Me.Button46.TabIndex = 42
        Me.Button46.Text = "IMDB"
        Me.Button46.UseVisualStyleBackColor = true
        '
        'Button45
        '
        Me.Button45.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button45.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button45.Location = New System.Drawing.Point(65, 13)
        Me.Button45.Margin = New System.Windows.Forms.Padding(4)
        Me.Button45.Name = "Button45"
        Me.Button45.Size = New System.Drawing.Size(70, 22)
        Me.Button45.TabIndex = 41
        Me.Button45.Text = "TVDB"
        Me.Button45.UseVisualStyleBackColor = true
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label41.AutoSize = true
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label41.Location = New System.Drawing.Point(6, 16)
        Me.Label41.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(61, 15)
        Me.Label41.TabIndex = 38
        Me.Label41.Text = "TV Show :"
        '
        'Label66
        '
        Me.Label66.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label66.AutoSize = true
        Me.Label66.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label66.Location = New System.Drawing.Point(9, 41)
        Me.Label66.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(58, 15)
        Me.Label66.TabIndex = 39
        Me.Label66.Text = "Episode :"
        '
        'PictureBox6
        '
        Me.PictureBox6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel20.SetColumnSpan(Me.PictureBox6, 4)
        Me.PictureBox6.Location = New System.Drawing.Point(554, 419)
        Me.PictureBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox6.Name = "PictureBox6"
        Me.TableLayoutPanel20.SetRowSpan(Me.PictureBox6, 6)
        Me.PictureBox6.Size = New System.Drawing.Size(150, 189)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox6.TabIndex = 31
        Me.PictureBox6.TabStop = false
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = true
        Me.TableLayoutPanel20.SetColumnSpan(Me.Label8, 3)
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.Location = New System.Drawing.Point(581, 0)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(41, 27)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "State :"
        '
        'cbTvActor
        '
        Me.cbTvActor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbTvActor.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel20.SetColumnSpan(Me.cbTvActor, 3)
        Me.cbTvActor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTvActor.Font = New System.Drawing.Font("Times New Roman", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbTvActor.FormattingEnabled = true
        Me.cbTvActor.Location = New System.Drawing.Point(580, 357)
        Me.cbTvActor.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTvActor.Name = "cbTvActor"
        Me.cbTvActor.Size = New System.Drawing.Size(124, 23)
        Me.cbTvActor.TabIndex = 27
        Me.cbTvActor.TabStop = false
        '
        'cbTvActorRole
        '
        Me.TableLayoutPanel20.SetColumnSpan(Me.cbTvActorRole, 3)
        Me.cbTvActorRole.FormattingEnabled = true
        Me.cbTvActorRole.Location = New System.Drawing.Point(579, 387)
        Me.cbTvActorRole.Name = "cbTvActorRole"
        Me.cbTvActorRole.Size = New System.Drawing.Size(125, 23)
        Me.cbTvActorRole.TabIndex = 49
        '
        'lbl_sorttitle
        '
        Me.lbl_sorttitle.AutoSize = true
        Me.TableLayoutPanel20.SetColumnSpan(Me.lbl_sorttitle, 4)
        Me.lbl_sorttitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_sorttitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_sorttitle.Location = New System.Drawing.Point(548, 104)
        Me.lbl_sorttitle.Margin = New System.Windows.Forms.Padding(10, 6, 3, 0)
        Me.lbl_sorttitle.Name = "lbl_sorttitle"
        Me.lbl_sorttitle.Size = New System.Drawing.Size(157, 16)
        Me.lbl_sorttitle.TabIndex = 50
        Me.lbl_sorttitle.Text = "Show Sort Title"
        '
        'TextBox_Sorttitle
        '
        Me.TableLayoutPanel20.SetColumnSpan(Me.TextBox_Sorttitle, 6)
        Me.TextBox_Sorttitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Sorttitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox_Sorttitle.Location = New System.Drawing.Point(532, 123)
        Me.TextBox_Sorttitle.Name = "TextBox_Sorttitle"
        Me.TextBox_Sorttitle.Size = New System.Drawing.Size(178, 21)
        Me.TextBox_Sorttitle.TabIndex = 51
        '
        'Label131
        '
        Me.Label131.AutoSize = true
        Me.TableLayoutPanel20.SetColumnSpan(Me.Label131, 3)
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label131.Location = New System.Drawing.Point(544, 70)
        Me.Label131.Margin = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(78, 28)
        Me.Label131.TabIndex = 188
        Me.Label131.Text = "Show Status:"
        Me.Label131.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'bnt_TvSeriesStatus
        '
        Me.bnt_TvSeriesStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.bnt_TvSeriesStatus.Location = New System.Drawing.Point(625, 71)
        Me.bnt_TvSeriesStatus.Name = "bnt_TvSeriesStatus"
        Me.bnt_TvSeriesStatus.Size = New System.Drawing.Size(80, 24)
        Me.bnt_TvSeriesStatus.TabIndex = 189
        Me.bnt_TvSeriesStatus.Text = "Continuing"
        Me.bnt_TvSeriesStatus.UseVisualStyleBackColor = true
        '
        'tpTvScreenshot
        '
        Me.tpTvScreenshot.AutoScroll = true
        Me.tpTvScreenshot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvScreenshot.Controls.Add(Me.TableLayoutPanel6)
        Me.tpTvScreenshot.Location = New System.Drawing.Point(4, 25)
        Me.tpTvScreenshot.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvScreenshot.Name = "tpTvScreenshot"
        Me.tpTvScreenshot.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvScreenshot.TabIndex = 7
        Me.tpTvScreenshot.Text = "Screenshot"
        Me.tpTvScreenshot.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 8
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 239!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel6.Controls.Add(Me.PictureBox14, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.tv_EpThumbRescrape, 1, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.Label6, 3, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.tv_EpThumbScreenShot, 3, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.TextBox35, 5, 2)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 5
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1037, 624)
        Me.TableLayoutPanel6.TabIndex = 6
        '
        'PictureBox14
        '
        Me.PictureBox14.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel6.SetColumnSpan(Me.PictureBox14, 5)
        Me.PictureBox14.Location = New System.Drawing.Point(24, 24)
        Me.PictureBox14.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox14.Name = "PictureBox14"
        Me.PictureBox14.Size = New System.Drawing.Size(981, 505)
        Me.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox14.TabIndex = 2
        Me.PictureBox14.TabStop = false
        '
        'tv_EpThumbRescrape
        '
        Me.tv_EpThumbRescrape.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.tv_EpThumbRescrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tv_EpThumbRescrape.Location = New System.Drawing.Point(24, 570)
        Me.tv_EpThumbRescrape.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_EpThumbRescrape.Name = "tv_EpThumbRescrape"
        Me.tv_EpThumbRescrape.Size = New System.Drawing.Size(191, 37)
        Me.tv_EpThumbRescrape.TabIndex = 0
        Me.tv_EpThumbRescrape.Text = "Rescrape Episode Thumbnail"
        Me.tv_EpThumbRescrape.UseVisualStyleBackColor = true
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = true
        Me.TableLayoutPanel6.SetColumnSpan(Me.Label6, 2)
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(803, 536)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 30)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Location within media in"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"seconds for Screen Shot"
        '
        'tv_EpThumbScreenShot
        '
        Me.TableLayoutPanel6.SetColumnSpan(Me.tv_EpThumbScreenShot, 3)
        Me.tv_EpThumbScreenShot.Dock = System.Windows.Forms.DockStyle.Right
        Me.tv_EpThumbScreenShot.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tv_EpThumbScreenShot.Location = New System.Drawing.Point(757, 570)
        Me.tv_EpThumbScreenShot.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_EpThumbScreenShot.Name = "tv_EpThumbScreenShot"
        Me.tv_EpThumbScreenShot.Size = New System.Drawing.Size(248, 37)
        Me.tv_EpThumbScreenShot.TabIndex = 1
        Me.tv_EpThumbScreenShot.Text = "Create Screen Shot Using ffmpeg"
        Me.tv_EpThumbScreenShot.UseVisualStyleBackColor = true
        '
        'TextBox35
        '
        Me.TextBox35.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBox35.Location = New System.Drawing.Point(951, 541)
        Me.TextBox35.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox35.Name = "TextBox35"
        Me.TextBox35.Size = New System.Drawing.Size(54, 21)
        Me.TextBox35.TabIndex = 3
        Me.TextBox35.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpTvFanart
        '
        Me.tpTvFanart.AutoScroll = true
        Me.tpTvFanart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvFanart.Controls.Add(Me.TableLayoutPanel18)
        Me.tpTvFanart.Location = New System.Drawing.Point(4, 25)
        Me.tpTvFanart.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvFanart.Name = "tpTvFanart"
        Me.tpTvFanart.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvFanart.TabIndex = 1
        Me.tpTvFanart.Text = "Fanart"
        Me.tpTvFanart.ToolTipText = "Use this option to select from available fanart"
        Me.tpTvFanart.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 11
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 441!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel18.Controls.Add(Me.Panel13, 0, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.TextBox28, 2, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.GroupBox6, 2, 1)
        Me.TableLayoutPanel18.Controls.Add(Me.Label62, 2, 2)
        Me.TableLayoutPanel18.Controls.Add(Me.Button36, 5, 4)
        Me.TableLayoutPanel18.Controls.Add(Me.Label63, 5, 3)
        Me.TableLayoutPanel18.Controls.Add(Me.Label61, 2, 3)
        Me.TableLayoutPanel18.Controls.Add(Me.Button35, 5, 2)
        Me.TableLayoutPanel18.Controls.Add(Me.Label59, 3, 3)
        Me.TableLayoutPanel18.Controls.Add(Me.Button38, 4, 3)
        Me.TableLayoutPanel18.Controls.Add(Me.Label60, 2, 4)
        Me.TableLayoutPanel18.Controls.Add(Me.Label58, 3, 4)
        Me.TableLayoutPanel18.Controls.Add(Me.Button37, 6, 3)
        Me.TableLayoutPanel18.Controls.Add(Me.btnTvFanartResetImage, 8, 2)
        Me.TableLayoutPanel18.Controls.Add(Me.btnTvFanartResize, 8, 3)
        Me.TableLayoutPanel18.Controls.Add(Me.btnTvFanartSaveCropped, 8, 4)
        Me.TableLayoutPanel18.Controls.Add(Me.btnTvFanartUrl, 8, 5)
        Me.TableLayoutPanel18.Controls.Add(Me.btnTvFanartSave, 8, 6)
        Me.TableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 9
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(1037, 624)
        Me.TableLayoutPanel18.TabIndex = 151
        '
        'Panel13
        '
        Me.Panel13.AutoScroll = true
        Me.Panel13.AutoScrollMargin = New System.Drawing.Size(0, 5)
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(4, 4)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel13.Name = "Panel13"
        Me.TableLayoutPanel18.SetRowSpan(Me.Panel13, 9)
        Me.Panel13.Size = New System.Drawing.Size(433, 616)
        Me.Panel13.TabIndex = 128
        '
        'TextBox28
        '
        Me.TextBox28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel18.SetColumnSpan(Me.TextBox28, 8)
        Me.TextBox28.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox28.Location = New System.Drawing.Point(479, 4)
        Me.TextBox28.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox28.Name = "TextBox28"
        Me.TextBox28.ReadOnly = true
        Me.TextBox28.Size = New System.Drawing.Size(538, 31)
        Me.TextBox28.TabIndex = 134
        Me.TextBox28.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox6
        '
        Me.TableLayoutPanel18.SetColumnSpan(Me.GroupBox6, 8)
        Me.GroupBox6.Controls.Add(Me.Panel12)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(479, 45)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Size = New System.Drawing.Size(538, 350)
        Me.GroupBox6.TabIndex = 148
        Me.GroupBox6.TabStop = false
        Me.GroupBox6.Text = "Current Fanart"
        '
        'Panel12
        '
        Me.Panel12.AutoScroll = true
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel12.Controls.Add(Me.Label64)
        Me.Panel12.Controls.Add(Me.PictureBox10)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(4, 18)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(530, 328)
        Me.Panel12.TabIndex = 94
        '
        'Label64
        '
        Me.Label64.AutoSize = true
        Me.Label64.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label64.Location = New System.Drawing.Point(129, 175)
        Me.Label64.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(281, 25)
        Me.Label64.TabIndex = 0
        Me.Label64.Text = "No Local Fanart is Available"
        Me.Label64.Visible = false
        '
        'PictureBox10
        '
        Me.PictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox10.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox10.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(526, 324)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox10.TabIndex = 1
        Me.PictureBox10.TabStop = false
        Me.PictureBox10.WaitOnLoad = true
        '
        'Label62
        '
        Me.Label62.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label62.AutoSize = true
        Me.Label62.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label62.Location = New System.Drawing.Point(479, 421)
        Me.Label62.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(96, 15)
        Me.Label62.TabIndex = 142
        Me.Label62.Text = "Image Details"
        '
        'Button36
        '
        Me.Button36.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Button36.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button36.Location = New System.Drawing.Point(702, 480)
        Me.Button36.Margin = New System.Windows.Forms.Padding(4)
        Me.Button36.Name = "Button36"
        Me.Button36.Size = New System.Drawing.Size(30, 29)
        Me.Button36.TabIndex = 137
        Me.Button36.Text = "^"
        Me.Button36.UseVisualStyleBackColor = true
        '
        'Label63
        '
        Me.Label63.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label63.AutoSize = true
        Me.Label63.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label63.Location = New System.Drawing.Point(702, 459)
        Me.Label63.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(33, 15)
        Me.Label63.TabIndex = 139
        Me.Label63.Text = "Crop"
        '
        'Label61
        '
        Me.Label61.AutoSize = true
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label61.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label61.Location = New System.Drawing.Point(528, 436)
        Me.Label61.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Padding = New System.Windows.Forms.Padding(4, 10, 0, 0)
        Me.Label61.Size = New System.Drawing.Size(51, 38)
        Me.Label61.TabIndex = 143
        Me.Label61.Text = "Width : "
        '
        'Button35
        '
        Me.Button35.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Button35.Font = New System.Drawing.Font("Microsoft Sans Serif", 6!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button35.Location = New System.Drawing.Point(702, 403)
        Me.Button35.Margin = New System.Windows.Forms.Padding(4)
        Me.Button35.Name = "Button35"
        Me.Button35.Size = New System.Drawing.Size(30, 29)
        Me.Button35.TabIndex = 138
        Me.Button35.Text = "V"
        Me.Button35.UseVisualStyleBackColor = true
        '
        'Label59
        '
        Me.Label59.AutoSize = true
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label59.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label59.Location = New System.Drawing.Point(587, 436)
        Me.Label59.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label59.Size = New System.Drawing.Size(52, 38)
        Me.Label59.TabIndex = 145
        Me.Label59.Text = "Label59"
        '
        'Button38
        '
        Me.Button38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Button38.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button38.Location = New System.Drawing.Point(661, 441)
        Me.Button38.Margin = New System.Windows.Forms.Padding(4)
        Me.Button38.Name = "Button38"
        Me.Button38.Size = New System.Drawing.Size(30, 29)
        Me.Button38.TabIndex = 135
        Me.Button38.Text = ">"
        Me.Button38.UseVisualStyleBackColor = true
        '
        'Label60
        '
        Me.Label60.AutoSize = true
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label60.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label60.Location = New System.Drawing.Point(527, 474)
        Me.Label60.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label60.Size = New System.Drawing.Size(52, 39)
        Me.Label60.TabIndex = 144
        Me.Label60.Text = "Height : "
        '
        'Label58
        '
        Me.Label58.AutoSize = true
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label58.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label58.Location = New System.Drawing.Point(587, 474)
        Me.Label58.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label58.Size = New System.Drawing.Size(52, 39)
        Me.Label58.TabIndex = 146
        Me.Label58.Text = "Label58"
        '
        'Button37
        '
        Me.Button37.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Button37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button37.Location = New System.Drawing.Point(744, 441)
        Me.Button37.Margin = New System.Windows.Forms.Padding(4)
        Me.Button37.Name = "Button37"
        Me.Button37.Size = New System.Drawing.Size(30, 29)
        Me.Button37.TabIndex = 136
        Me.Button37.Text = "<"
        Me.Button37.UseVisualStyleBackColor = true
        '
        'btnTvFanartResetImage
        '
        Me.btnTvFanartResetImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel18.SetColumnSpan(Me.btnTvFanartResetImage, 2)
        Me.btnTvFanartResetImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvFanartResetImage.Location = New System.Drawing.Point(899, 403)
        Me.btnTvFanartResetImage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvFanartResetImage.Name = "btnTvFanartResetImage"
        Me.btnTvFanartResetImage.Size = New System.Drawing.Size(118, 29)
        Me.btnTvFanartResetImage.TabIndex = 140
        Me.btnTvFanartResetImage.Text = "Reset Image"
        Me.btnTvFanartResetImage.UseVisualStyleBackColor = true
        Me.btnTvFanartResetImage.Visible = false
        '
        'btnTvFanartSaveCropped
        '
        Me.btnTvFanartSaveCropped.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel18.SetColumnSpan(Me.btnTvFanartSaveCropped, 2)
        Me.btnTvFanartSaveCropped.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvFanartSaveCropped.Location = New System.Drawing.Point(899, 480)
        Me.btnTvFanartSaveCropped.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvFanartSaveCropped.Name = "btnTvFanartSaveCropped"
        Me.btnTvFanartSaveCropped.Size = New System.Drawing.Size(118, 29)
        Me.btnTvFanartSaveCropped.TabIndex = 141
        Me.btnTvFanartSaveCropped.Text = "Save Cropped"
        Me.btnTvFanartSaveCropped.UseVisualStyleBackColor = true
        Me.btnTvFanartSaveCropped.Visible = false
        '
        'btnTvFanartUrl
        '
        Me.btnTvFanartUrl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel18.SetColumnSpan(Me.btnTvFanartUrl, 2)
        Me.btnTvFanartUrl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvFanartUrl.Location = New System.Drawing.Point(899, 531)
        Me.btnTvFanartUrl.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvFanartUrl.Name = "btnTvFanartUrl"
        Me.btnTvFanartUrl.Size = New System.Drawing.Size(118, 29)
        Me.btnTvFanartUrl.TabIndex = 132
        Me.btnTvFanartUrl.Text = "URL or Browse"
        Me.btnTvFanartUrl.UseVisualStyleBackColor = true
        '
        'btnTvFanartSave
        '
        Me.btnTvFanartSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel18.SetColumnSpan(Me.btnTvFanartSave, 2)
        Me.btnTvFanartSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvFanartSave.Location = New System.Drawing.Point(899, 573)
        Me.btnTvFanartSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvFanartSave.Name = "btnTvFanartSave"
        Me.btnTvFanartSave.Size = New System.Drawing.Size(118, 29)
        Me.btnTvFanartSave.TabIndex = 130
        Me.btnTvFanartSave.Text = "Save Selected"
        Me.btnTvFanartSave.UseVisualStyleBackColor = true
        '
        'tpTvPosters
        '
        Me.tpTvPosters.AutoScroll = true
        Me.tpTvPosters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvPosters.Controls.Add(Me.TableLayoutPanel17)
        Me.tpTvPosters.Location = New System.Drawing.Point(4, 25)
        Me.tpTvPosters.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvPosters.Name = "tpTvPosters"
        Me.tpTvPosters.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvPosters.TabIndex = 6
        Me.tpTvPosters.Text = "Posters"
        Me.tpTvPosters.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 9
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 304!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel17.Controls.Add(Me.btnTvPosterPrev, 1, 5)
        Me.TableLayoutPanel17.Controls.Add(Me.btnTvPosterSaveSmall, 7, 8)
        Me.TableLayoutPanel17.Controls.Add(Me.btnTvPosterSaveBig, 7, 7)
        Me.TableLayoutPanel17.Controls.Add(Me.Label72, 2, 5)
        Me.TableLayoutPanel17.Controls.Add(Me.Panel16, 1, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.TextBox31, 5, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.Label73, 5, 3)
        Me.TableLayoutPanel17.Controls.Add(Me.btnTvPosterNext, 3, 5)
        Me.TableLayoutPanel17.Controls.Add(Me.Panel15, 5, 2)
        Me.TableLayoutPanel17.Controls.Add(Me.GroupBox23, 1, 7)
        Me.TableLayoutPanel17.Controls.Add(Me.GroupBox21, 7, 3)
        Me.TableLayoutPanel17.Controls.Add(Me.Label76, 5, 4)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 10
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(1037, 624)
        Me.TableLayoutPanel17.TabIndex = 171
        '
        'btnTvPosterPrev
        '
        Me.btnTvPosterPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnTvPosterPrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterPrev.Location = New System.Drawing.Point(12, 492)
        Me.btnTvPosterPrev.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterPrev.Name = "btnTvPosterPrev"
        Me.btnTvPosterPrev.Size = New System.Drawing.Size(125, 27)
        Me.btnTvPosterPrev.TabIndex = 155
        Me.btnTvPosterPrev.Text = "Previous"
        Me.btnTvPosterPrev.UseVisualStyleBackColor = true
        Me.btnTvPosterPrev.Visible = false
        '
        'btnTvPosterSaveSmall
        '
        Me.btnTvPosterSaveSmall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnTvPosterSaveSmall.Enabled = false
        Me.btnTvPosterSaveSmall.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterSaveSmall.Location = New System.Drawing.Point(848, 572)
        Me.btnTvPosterSaveSmall.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterSaveSmall.Name = "btnTvPosterSaveSmall"
        Me.btnTvPosterSaveSmall.Size = New System.Drawing.Size(175, 28)
        Me.btnTvPosterSaveSmall.TabIndex = 148
        Me.btnTvPosterSaveSmall.Text = "Save Small"
        Me.btnTvPosterSaveSmall.UseVisualStyleBackColor = true
        Me.btnTvPosterSaveSmall.Visible = false
        '
        'btnTvPosterSaveBig
        '
        Me.btnTvPosterSaveBig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnTvPosterSaveBig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterSaveBig.Location = New System.Drawing.Point(848, 536)
        Me.btnTvPosterSaveBig.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterSaveBig.Name = "btnTvPosterSaveBig"
        Me.btnTvPosterSaveBig.Size = New System.Drawing.Size(175, 28)
        Me.btnTvPosterSaveBig.TabIndex = 154
        Me.btnTvPosterSaveBig.Text = "Save Big"
        Me.btnTvPosterSaveBig.UseVisualStyleBackColor = true
        Me.btnTvPosterSaveBig.Visible = false
        '
        'Label72
        '
        Me.Label72.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label72.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label72.Location = New System.Drawing.Point(206, 503)
        Me.Label72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(296, 20)
        Me.Label72.TabIndex = 157
        Me.Label72.Text = "Label72"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label72.Visible = false
        '
        'Panel16
        '
        Me.Panel16.AutoScroll = true
        Me.TableLayoutPanel17.SetColumnSpan(Me.Panel16, 3)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(12, 24)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel16.Name = "Panel16"
        Me.TableLayoutPanel17.SetRowSpan(Me.Panel16, 4)
        Me.Panel16.Size = New System.Drawing.Size(638, 460)
        Me.Panel16.TabIndex = 167
        '
        'TextBox31
        '
        Me.TextBox31.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel17.SetColumnSpan(Me.TextBox31, 3)
        Me.TextBox31.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox31.Location = New System.Drawing.Point(666, 24)
        Me.TextBox31.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox31.Name = "TextBox31"
        Me.TextBox31.ReadOnly = true
        Me.TextBox31.Size = New System.Drawing.Size(357, 31)
        Me.TextBox31.TabIndex = 166
        Me.TextBox31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label73
        '
        Me.TableLayoutPanel17.SetColumnSpan(Me.Label73, 2)
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label73.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label73.Location = New System.Drawing.Point(666, 384)
        Me.Label73.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(173, 28)
        Me.Label73.TabIndex = 145
        Me.Label73.Text = "Label73"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTvPosterNext
        '
        Me.btnTvPosterNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnTvPosterNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterNext.Location = New System.Drawing.Point(510, 492)
        Me.btnTvPosterNext.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterNext.Name = "btnTvPosterNext"
        Me.btnTvPosterNext.Size = New System.Drawing.Size(125, 27)
        Me.btnTvPosterNext.TabIndex = 156
        Me.btnTvPosterNext.Text = "Next"
        Me.btnTvPosterNext.UseVisualStyleBackColor = true
        Me.btnTvPosterNext.Visible = false
        '
        'Panel15
        '
        Me.TableLayoutPanel17.SetColumnSpan(Me.Panel15, 3)
        Me.Panel15.Controls.Add(Me.PictureBox12)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Location = New System.Drawing.Point(666, 60)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(357, 320)
        Me.Panel15.TabIndex = 146
        '
        'PictureBox12
        '
        Me.PictureBox12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox12.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox12.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(357, 320)
        Me.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox12.TabIndex = 0
        Me.PictureBox12.TabStop = false
        '
        'GroupBox23
        '
        Me.GroupBox23.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel17.SetColumnSpan(Me.GroupBox23, 3)
        Me.GroupBox23.Controls.Add(Me.rbTVbanner)
        Me.GroupBox23.Controls.Add(Me.rbTVposter)
        Me.GroupBox23.Controls.Add(Me.btnTvPosterTVDBAll)
        Me.GroupBox23.Controls.Add(Me.btnTvPosterTVDBSpecific)
        Me.GroupBox23.Controls.Add(Me.btnTvPosterIMDB)
        Me.GroupBox23.Controls.Add(Me.btnTvPosterUrlBrowse)
        Me.GroupBox23.Controls.Add(Me.ComboBox2)
        Me.GroupBox23.Location = New System.Drawing.Point(11, 540)
        Me.GroupBox23.Name = "GroupBox23"
        Me.TableLayoutPanel17.SetRowSpan(Me.GroupBox23, 2)
        Me.GroupBox23.Size = New System.Drawing.Size(637, 61)
        Me.GroupBox23.TabIndex = 169
        Me.GroupBox23.TabStop = false
        Me.GroupBox23.Text = "Select Source to view Available Posters"
        '
        'rbTVbanner
        '
        Me.rbTVbanner.AutoSize = true
        Me.rbTVbanner.Location = New System.Drawing.Point(125, 37)
        Me.rbTVbanner.Name = "rbTVbanner"
        Me.rbTVbanner.Size = New System.Drawing.Size(65, 19)
        Me.rbTVbanner.TabIndex = 170
        Me.rbTVbanner.Text = "Banner"
        Me.rbTVbanner.UseVisualStyleBackColor = true
        '
        'rbTVposter
        '
        Me.rbTVposter.AutoSize = true
        Me.rbTVposter.Location = New System.Drawing.Point(125, 18)
        Me.rbTVposter.Name = "rbTVposter"
        Me.rbTVposter.Size = New System.Drawing.Size(60, 19)
        Me.rbTVposter.TabIndex = 169
        Me.rbTVposter.Text = "Poster"
        Me.rbTVposter.UseVisualStyleBackColor = true
        '
        'btnTvPosterIMDB
        '
        Me.btnTvPosterIMDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterIMDB.Location = New System.Drawing.Point(420, 25)
        Me.btnTvPosterIMDB.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterIMDB.Name = "btnTvPosterIMDB"
        Me.btnTvPosterIMDB.Size = New System.Drawing.Size(100, 29)
        Me.btnTvPosterIMDB.TabIndex = 147
        Me.btnTvPosterIMDB.Text = "IMDB (All)"
        Me.btnTvPosterIMDB.UseVisualStyleBackColor = true
        '
        'btnTvPosterUrlBrowse
        '
        Me.btnTvPosterUrlBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvPosterUrlBrowse.Location = New System.Drawing.Point(528, 25)
        Me.btnTvPosterUrlBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvPosterUrlBrowse.Name = "btnTvPosterUrlBrowse"
        Me.btnTvPosterUrlBrowse.Size = New System.Drawing.Size(100, 29)
        Me.btnTvPosterUrlBrowse.TabIndex = 161
        Me.btnTvPosterUrlBrowse.Text = "URL or Browse"
        Me.btnTvPosterUrlBrowse.UseVisualStyleBackColor = true
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = true
        Me.ComboBox2.Location = New System.Drawing.Point(7, 26)
        Me.ComboBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(110, 23)
        Me.ComboBox2.TabIndex = 163
        '
        'GroupBox21
        '
        Me.GroupBox21.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox21.Controls.Add(Me.FrodoImageTrue)
        Me.GroupBox21.Controls.Add(Me.EdenImageTrue)
        Me.GroupBox21.Controls.Add(Me.ArtMode)
        Me.GroupBox21.Location = New System.Drawing.Point(847, 387)
        Me.GroupBox21.Name = "GroupBox21"
        Me.TableLayoutPanel17.SetRowSpan(Me.GroupBox21, 2)
        Me.GroupBox21.Size = New System.Drawing.Size(177, 98)
        Me.GroupBox21.TabIndex = 170
        Me.GroupBox21.TabStop = false
        Me.GroupBox21.Text = "Current Artwork Present"
        '
        'FrodoImageTrue
        '
        Me.FrodoImageTrue.AutoSize = true
        Me.FrodoImageTrue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FrodoImageTrue.Location = New System.Drawing.Point(6, 74)
        Me.FrodoImageTrue.MinimumSize = New System.Drawing.Size(160, 15)
        Me.FrodoImageTrue.Name = "FrodoImageTrue"
        Me.FrodoImageTrue.Size = New System.Drawing.Size(160, 15)
        Me.FrodoImageTrue.TabIndex = 2
        Me.FrodoImageTrue.Text = "Frodo Image Present"
        Me.FrodoImageTrue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'EdenImageTrue
        '
        Me.EdenImageTrue.AutoSize = true
        Me.EdenImageTrue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.EdenImageTrue.Location = New System.Drawing.Point(6, 46)
        Me.EdenImageTrue.MinimumSize = New System.Drawing.Size(160, 15)
        Me.EdenImageTrue.Name = "EdenImageTrue"
        Me.EdenImageTrue.Size = New System.Drawing.Size(160, 15)
        Me.EdenImageTrue.TabIndex = 1
        Me.EdenImageTrue.Text = "Eden Image Present"
        Me.EdenImageTrue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ArtMode
        '
        Me.ArtMode.AutoSize = true
        Me.ArtMode.Location = New System.Drawing.Point(16, 23)
        Me.ArtMode.MinimumSize = New System.Drawing.Size(150, 15)
        Me.ArtMode.Name = "ArtMode"
        Me.ArtMode.Size = New System.Drawing.Size(150, 15)
        Me.ArtMode.TabIndex = 0
        Me.ArtMode.Text = "ArtMode"
        Me.ArtMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label76
        '
        Me.Label76.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label76.Font = New System.Drawing.Font("Microsoft Sans Serif", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label76.Location = New System.Drawing.Point(666, 423)
        Me.Label76.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 65)
        Me.Label76.TabIndex = 151
        Me.Label76.Text = "Double Click a Poster for a larger view of the full resolution image."
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tpTvFanartTv
        '
        Me.tpTvFanartTv.Controls.Add(Me.UcFanartTvTv1)
        Me.tpTvFanartTv.Location = New System.Drawing.Point(4, 25)
        Me.tpTvFanartTv.Name = "tpTvFanartTv"
        Me.tpTvFanartTv.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvFanartTv.TabIndex = 10
        Me.tpTvFanartTv.Text = "Fanart.Tv"
        Me.tpTvFanartTv.UseVisualStyleBackColor = true
        '
        'UcFanartTvTv1
        '
        Me.UcFanartTvTv1.BackColor = System.Drawing.SystemColors.ControlText
        Me.UcFanartTvTv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFanartTvTv1.Location = New System.Drawing.Point(0, 0)
        Me.UcFanartTvTv1.Name = "UcFanartTvTv1"
        Me.UcFanartTvTv1.Padding = New System.Windows.Forms.Padding(7)
        Me.UcFanartTvTv1.Size = New System.Drawing.Size(1041, 628)
        Me.UcFanartTvTv1.TabIndex = 0
        '
        'tpTvWall
        '
        Me.tpTvWall.AutoScroll = true
        Me.tpTvWall.AutoScrollMinSize = New System.Drawing.Size(956, 450)
        Me.tpTvWall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpTvWall.Location = New System.Drawing.Point(4, 25)
        Me.tpTvWall.Name = "tpTvWall"
        Me.tpTvWall.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvWall.TabIndex = 11
        Me.tpTvWall.Text = "Wall"
        Me.tpTvWall.UseVisualStyleBackColor = true
        '
        'tpTvSelector
        '
        Me.tpTvSelector.AutoScroll = true
        Me.tpTvSelector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvSelector.Controls.Add(Me.TableLayoutPanel16)
        Me.tpTvSelector.Location = New System.Drawing.Point(4, 25)
        Me.tpTvSelector.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvSelector.Name = "tpTvSelector"
        Me.tpTvSelector.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvSelector.TabIndex = 2
        Me.tpTvSelector.Text = "TV Show Selector"
        Me.tpTvSelector.ToolTipText = "Use this tab to select or change TV Show"
        Me.tpTvSelector.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 8
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 424!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13!))
        Me.TableLayoutPanel16.Controls.Add(Me.Panel10, 4, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.PictureBox9, 0, 4)
        Me.TableLayoutPanel16.Controls.Add(Me.Label57, 0, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.TextBox26, 0, 2)
        Me.TableLayoutPanel16.Controls.Add(Me.ListBox3, 0, 3)
        Me.TableLayoutPanel16.Controls.Add(Me.btnTvShowSelectorScrape, 6, 7)
        Me.TableLayoutPanel16.Controls.Add(Me.Button30, 3, 2)
        Me.TableLayoutPanel16.Controls.Add(Me.Label125, 0, 1)
        Me.TableLayoutPanel16.Controls.Add(Me.tb_TvShSelectSeriesPath, 1, 1)
        Me.TableLayoutPanel16.Controls.Add(Me.Label56, 0, 6)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 8
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 239!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(1037, 624)
        Me.TableLayoutPanel16.TabIndex = 26
        '
        'Panel10
        '
        Me.Panel10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel16.SetColumnSpan(Me.Panel10, 3)
        Me.Panel10.Controls.Add(Me.cbTvChgShowDLFanartTvArt)
        Me.Panel10.Controls.Add(Me.Label22)
        Me.Panel10.Controls.Add(Me.GroupBox7)
        Me.Panel10.Controls.Add(Me.cbTvChgShowDLImagesLang)
        Me.Panel10.Controls.Add(Me.cbTvChgShowOverwriteImgs)
        Me.Panel10.Controls.Add(Me.cbTvChgShowDLSeason)
        Me.Panel10.Controls.Add(Me.cbTvChgShowDLFanart)
        Me.Panel10.Controls.Add(Me.cbTvChgShowDLPoster)
        Me.Panel10.Controls.Add(Me.GroupBox4)
        Me.Panel10.Controls.Add(Me.Label52)
        Me.Panel10.Controls.Add(Me.GroupBox3)
        Me.Panel10.Controls.Add(Me.GroupBox2)
        Me.Panel10.Controls.Add(Me.ListBox1)
        Me.Panel10.Controls.Add(Me.GroupBox5)
        Me.Panel10.Controls.Add(Me.Label53)
        Me.Panel10.Controls.Add(Me.Label55)
        Me.Panel10.Location = New System.Drawing.Point(336, 4)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel10.Name = "Panel10"
        Me.TableLayoutPanel16.SetRowSpan(Me.Panel10, 7)
        Me.Panel10.Size = New System.Drawing.Size(684, 580)
        Me.Panel10.TabIndex = 12
        '
        'cbTvChgShowDLFanartTvArt
        '
        Me.cbTvChgShowDLFanartTvArt.AutoSize = true
        Me.cbTvChgShowDLFanartTvArt.Location = New System.Drawing.Point(417, 354)
        Me.cbTvChgShowDLFanartTvArt.Name = "cbTvChgShowDLFanartTvArt"
        Me.cbTvChgShowDLFanartTvArt.Size = New System.Drawing.Size(152, 19)
        Me.cbTvChgShowDLFanartTvArt.TabIndex = 35
        Me.cbTvChgShowDLFanartTvArt.Text = "Download Fanart.Tv Art"
        Me.cbTvChgShowDLFanartTvArt.UseVisualStyleBackColor = true
        '
        'Label22
        '
        Me.Label22.AutoSize = true
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label22.Location = New System.Drawing.Point(517, 6)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(143, 30)
        Me.Label22.TabIndex = 34
        Me.Label22.Text = "Some options not valid if "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"using the XBMC Scraper"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.RadioButton18)
        Me.GroupBox7.Controls.Add(Me.RadioButton17)
        Me.GroupBox7.Controls.Add(Me.RadioButton16)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(544, 84)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(129, 109)
        Me.GroupBox7.TabIndex = 33
        Me.GroupBox7.TabStop = false
        Me.GroupBox7.Text = "season-all.tbn"
        '
        'RadioButton18
        '
        Me.RadioButton18.AutoSize = true
        Me.RadioButton18.Checked = true
        Me.RadioButton18.Location = New System.Drawing.Point(8, 82)
        Me.RadioButton18.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton18.Name = "RadioButton18"
        Me.RadioButton18.Size = New System.Drawing.Size(59, 20)
        Me.RadioButton18.TabIndex = 2
        Me.RadioButton18.TabStop = true
        Me.RadioButton18.Text = "None"
        Me.RadioButton18.UseVisualStyleBackColor = true
        '
        'RadioButton17
        '
        Me.RadioButton17.AutoSize = true
        Me.RadioButton17.Location = New System.Drawing.Point(8, 54)
        Me.RadioButton17.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton17.Name = "RadioButton17"
        Me.RadioButton17.Size = New System.Drawing.Size(65, 20)
        Me.RadioButton17.TabIndex = 1
        Me.RadioButton17.Text = "Poster"
        Me.RadioButton17.UseVisualStyleBackColor = true
        '
        'RadioButton16
        '
        Me.RadioButton16.AutoSize = true
        Me.RadioButton16.Location = New System.Drawing.Point(8, 25)
        Me.RadioButton16.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton16.Name = "RadioButton16"
        Me.RadioButton16.Size = New System.Drawing.Size(58, 20)
        Me.RadioButton16.TabIndex = 0
        Me.RadioButton16.Text = "Wide"
        Me.RadioButton16.UseVisualStyleBackColor = true
        '
        'cbTvChgShowDLImagesLang
        '
        Me.cbTvChgShowDLImagesLang.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbTvChgShowDLImagesLang.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbTvChgShowDLImagesLang.Location = New System.Drawing.Point(227, 415)
        Me.cbTvChgShowDLImagesLang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTvChgShowDLImagesLang.Name = "cbTvChgShowDLImagesLang"
        Me.cbTvChgShowDLImagesLang.Size = New System.Drawing.Size(337, 49)
        Me.cbTvChgShowDLImagesLang.TabIndex = 32
        Me.cbTvChgShowDLImagesLang.Text = "Check if you want to attempt to download posters in your selected language. If le"& _ 
    "ft unchecked, or if posters are not available, then they will be downloaded in E"& _ 
    "nglish."
        Me.cbTvChgShowDLImagesLang.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cbTvChgShowDLImagesLang.UseVisualStyleBackColor = true
        '
        'cbTvChgShowOverwriteImgs
        '
        Me.cbTvChgShowOverwriteImgs.AutoSize = true
        Me.cbTvChgShowOverwriteImgs.Location = New System.Drawing.Point(227, 386)
        Me.cbTvChgShowOverwriteImgs.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTvChgShowOverwriteImgs.Name = "cbTvChgShowOverwriteImgs"
        Me.cbTvChgShowOverwriteImgs.Size = New System.Drawing.Size(185, 19)
        Me.cbTvChgShowOverwriteImgs.TabIndex = 31
        Me.cbTvChgShowOverwriteImgs.Text = "Overwrite existing image files"
        Me.cbTvChgShowOverwriteImgs.UseVisualStyleBackColor = true
        '
        'cbTvChgShowDLSeason
        '
        Me.cbTvChgShowDLSeason.AutoSize = true
        Me.cbTvChgShowDLSeason.Location = New System.Drawing.Point(417, 325)
        Me.cbTvChgShowDLSeason.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTvChgShowDLSeason.Name = "cbTvChgShowDLSeason"
        Me.cbTvChgShowDLSeason.Size = New System.Drawing.Size(220, 19)
        Me.cbTvChgShowDLSeason.TabIndex = 30
        Me.cbTvChgShowDLSeason.Text = "Download Season Posters/Banners"
        Me.cbTvChgShowDLSeason.UseVisualStyleBackColor = true
        '
        'cbTvChgShowDLFanart
        '
        Me.cbTvChgShowDLFanart.AutoSize = true
        Me.cbTvChgShowDLFanart.Location = New System.Drawing.Point(227, 354)
        Me.cbTvChgShowDLFanart.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTvChgShowDLFanart.Name = "cbTvChgShowDLFanart"
        Me.cbTvChgShowDLFanart.Size = New System.Drawing.Size(120, 19)
        Me.cbTvChgShowDLFanart.TabIndex = 29
        Me.cbTvChgShowDLFanart.Text = "Download Fanart"
        Me.cbTvChgShowDLFanart.UseVisualStyleBackColor = true
        '
        'cbTvChgShowDLPoster
        '
        Me.cbTvChgShowDLPoster.AutoSize = true
        Me.cbTvChgShowDLPoster.Location = New System.Drawing.Point(227, 325)
        Me.cbTvChgShowDLPoster.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTvChgShowDLPoster.Name = "cbTvChgShowDLPoster"
        Me.cbTvChgShowDLPoster.Size = New System.Drawing.Size(163, 19)
        Me.cbTvChgShowDLPoster.TabIndex = 28
        Me.cbTvChgShowDLPoster.Text = "Download Poster/Banner"
        Me.cbTvChgShowDLPoster.UseVisualStyleBackColor = true
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.RadioButton8)
        Me.GroupBox4.Controls.Add(Me.RadioButton9)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(420, 179)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(116, 88)
        Me.GroupBox4.TabIndex = 27
        Me.GroupBox4.TabStop = false
        Me.GroupBox4.Text = "Art Style"
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = true
        Me.RadioButton8.Location = New System.Drawing.Point(8, 55)
        Me.RadioButton8.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(69, 20)
        Me.RadioButton8.TabIndex = 1
        Me.RadioButton8.TabStop = true
        Me.RadioButton8.Text = "Banner"
        Me.RadioButton8.UseVisualStyleBackColor = true
        '
        'RadioButton9
        '
        Me.RadioButton9.AutoSize = true
        Me.RadioButton9.Location = New System.Drawing.Point(8, 26)
        Me.RadioButton9.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(65, 20)
        Me.RadioButton9.TabIndex = 0
        Me.RadioButton9.TabStop = true
        Me.RadioButton9.Text = "Poster"
        Me.RadioButton9.UseVisualStyleBackColor = true
        '
        'Label52
        '
        Me.Label52.AutoSize = true
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label52.Location = New System.Drawing.Point(223, 281)
        Me.Label52.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(219, 32)
        Me.Label52.TabIndex = 26
        Me.Label52.Text = "If you wish, you can also change the"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"following settings from the default."
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RadioButton10)
        Me.GroupBox3.Controls.Add(Me.RadioButton11)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(226, 84)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(186, 88)
        Me.GroupBox3.TabIndex = 24
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "episode.nfo actor source"
        '
        'RadioButton10
        '
        Me.RadioButton10.AutoSize = true
        Me.RadioButton10.Location = New System.Drawing.Point(9, 55)
        Me.RadioButton10.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton10.Name = "RadioButton10"
        Me.RadioButton10.Size = New System.Drawing.Size(59, 20)
        Me.RadioButton10.TabIndex = 1
        Me.RadioButton10.TabStop = true
        Me.RadioButton10.Text = "IMDB"
        Me.RadioButton10.UseVisualStyleBackColor = true
        '
        'RadioButton11
        '
        Me.RadioButton11.AutoSize = true
        Me.RadioButton11.Location = New System.Drawing.Point(9, 28)
        Me.RadioButton11.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton11.Name = "RadioButton11"
        Me.RadioButton11.Size = New System.Drawing.Size(63, 20)
        Me.RadioButton11.TabIndex = 0
        Me.RadioButton11.TabStop = true
        Me.RadioButton11.Text = "TVDB"
        Me.RadioButton11.UseVisualStyleBackColor = true
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.RadioButton12)
        Me.GroupBox2.Controls.Add(Me.RadioButton13)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(226, 179)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(186, 88)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "tvshow.nfo actor source"
        '
        'RadioButton12
        '
        Me.RadioButton12.AutoSize = true
        Me.RadioButton12.Location = New System.Drawing.Point(8, 55)
        Me.RadioButton12.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton12.Name = "RadioButton12"
        Me.RadioButton12.Size = New System.Drawing.Size(59, 20)
        Me.RadioButton12.TabIndex = 1
        Me.RadioButton12.TabStop = true
        Me.RadioButton12.Text = "IMDB"
        Me.RadioButton12.UseVisualStyleBackColor = true
        '
        'RadioButton13
        '
        Me.RadioButton13.AutoSize = true
        Me.RadioButton13.Location = New System.Drawing.Point(8, 26)
        Me.RadioButton13.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton13.Name = "RadioButton13"
        Me.RadioButton13.Size = New System.Drawing.Size(63, 20)
        Me.RadioButton13.TabIndex = 0
        Me.RadioButton13.TabStop = true
        Me.RadioButton13.Text = "TVDB"
        Me.RadioButton13.UseVisualStyleBackColor = true
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Control
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ListBox1.FormattingEnabled = true
        Me.ListBox1.ItemHeight = 16
        Me.ListBox1.Location = New System.Drawing.Point(4, 84)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(205, 452)
        Me.ListBox1.TabIndex = 19
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox5.Controls.Add(Me.RadioButton14)
        Me.GroupBox5.Controls.Add(Me.RadioButton15)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox5.Location = New System.Drawing.Point(420, 84)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(116, 88)
        Me.GroupBox5.TabIndex = 22
        Me.GroupBox5.TabStop = false
        Me.GroupBox5.Text = "Sort Order"
        '
        'RadioButton14
        '
        Me.RadioButton14.AutoSize = true
        Me.RadioButton14.Location = New System.Drawing.Point(8, 52)
        Me.RadioButton14.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton14.Name = "RadioButton14"
        Me.RadioButton14.Size = New System.Drawing.Size(55, 20)
        Me.RadioButton14.TabIndex = 1
        Me.RadioButton14.TabStop = true
        Me.RadioButton14.Text = "DVD"
        Me.RadioButton14.UseVisualStyleBackColor = true
        '
        'RadioButton15
        '
        Me.RadioButton15.AutoSize = true
        Me.RadioButton15.Location = New System.Drawing.Point(8, 26)
        Me.RadioButton15.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton15.Name = "RadioButton15"
        Me.RadioButton15.Size = New System.Drawing.Size(68, 20)
        Me.RadioButton15.TabIndex = 0
        Me.RadioButton15.TabStop = true
        Me.RadioButton15.Text = "Default"
        Me.RadioButton15.UseVisualStyleBackColor = true
        '
        'Label53
        '
        Me.Label53.AutoSize = true
        Me.Label53.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label53.Location = New System.Drawing.Point(4, 6)
        Me.Label53.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(399, 32)
        Me.Label53.TabIndex = 20
        Me.Label53.Text = "The options in this panel can be changed from the default and will"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"be applied on"& _ 
    "ly to this TV Show and any episodes it has available"
        '
        'Label55
        '
        Me.Label55.AutoSize = true
        Me.Label55.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label55.Location = New System.Drawing.Point(4, 60)
        Me.Label55.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(222, 16)
        Me.Label55.TabIndex = 21
        Me.Label55.Text = "Default Language for TV Shows is :- "
        '
        'PictureBox9
        '
        Me.TableLayoutPanel16.SetColumnSpan(Me.PictureBox9, 4)
        Me.PictureBox9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox9.Location = New System.Drawing.Point(4, 356)
        Me.PictureBox9.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox9.Name = "PictureBox9"
        Me.TableLayoutPanel16.SetRowSpan(Me.PictureBox9, 2)
        Me.PictureBox9.Size = New System.Drawing.Size(324, 128)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox9.TabIndex = 13
        Me.PictureBox9.TabStop = false
        '
        'Label57
        '
        Me.TableLayoutPanel16.SetColumnSpan(Me.Label57, 4)
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label57.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label57.Location = New System.Drawing.Point(4, 0)
        Me.Label57.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label57.Size = New System.Drawing.Size(324, 55)
        Me.Label57.TabIndex = 7
        Me.Label57.Text = "The TV Show Search Will Be Based On The Text Entry Below, Edit if Neccesary And P"& _ 
    "ress The Search Button"
        '
        'TextBox26
        '
        Me.TextBox26.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBox26.BackColor = System.Drawing.SystemColors.Window
        Me.TableLayoutPanel16.SetColumnSpan(Me.TextBox26, 2)
        Me.TextBox26.Location = New System.Drawing.Point(4, 86)
        Me.TextBox26.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox26.Name = "TextBox26"
        Me.TextBox26.Size = New System.Drawing.Size(229, 21)
        Me.TextBox26.TabIndex = 8
        '
        'ListBox3
        '
        Me.ListBox3.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel16.SetColumnSpan(Me.ListBox3, 4)
        Me.ListBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ListBox3.FormattingEnabled = true
        Me.ListBox3.ItemHeight = 16
        Me.ListBox3.Location = New System.Drawing.Point(4, 117)
        Me.ListBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(324, 231)
        Me.ListBox3.TabIndex = 10
        '
        'btnTvShowSelectorScrape
        '
        Me.btnTvShowSelectorScrape.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnTvShowSelectorScrape.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnTvShowSelectorScrape.Location = New System.Drawing.Point(782, 592)
        Me.btnTvShowSelectorScrape.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTvShowSelectorScrape.Name = "btnTvShowSelectorScrape"
        Me.btnTvShowSelectorScrape.Size = New System.Drawing.Size(238, 28)
        Me.btnTvShowSelectorScrape.TabIndex = 25
        Me.btnTvShowSelectorScrape.Text = "Scrape Show with Selected Options"
        Me.btnTvShowSelectorScrape.UseVisualStyleBackColor = true
        '
        'Button30
        '
        Me.Button30.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button30.Location = New System.Drawing.Point(255, 86)
        Me.Button30.Margin = New System.Windows.Forms.Padding(4)
        Me.Button30.Name = "Button30"
        Me.Button30.Size = New System.Drawing.Size(73, 23)
        Me.Button30.TabIndex = 9
        Me.Button30.Text = "Search"
        Me.Button30.UseVisualStyleBackColor = true
        '
        'Label125
        '
        Me.Label125.AutoSize = true
        Me.Label125.BackColor = System.Drawing.Color.DarkGray
        Me.Label125.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label125.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label125.Location = New System.Drawing.Point(3, 59)
        Me.Label125.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(76, 23)
        Me.Label125.TabIndex = 26
        Me.Label125.Text = "Series Path:"
        '
        'tb_TvShSelectSeriesPath
        '
        Me.TableLayoutPanel16.SetColumnSpan(Me.tb_TvShSelectSeriesPath, 3)
        Me.tb_TvShSelectSeriesPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_TvShSelectSeriesPath.Enabled = false
        Me.tb_TvShSelectSeriesPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tb_TvShSelectSeriesPath.ForeColor = System.Drawing.Color.Black
        Me.tb_TvShSelectSeriesPath.Location = New System.Drawing.Point(85, 58)
        Me.tb_TvShSelectSeriesPath.Name = "tb_TvShSelectSeriesPath"
        Me.tb_TvShSelectSeriesPath.ReadOnly = true
        Me.tb_TvShSelectSeriesPath.Size = New System.Drawing.Size(244, 21)
        Me.tb_TvShSelectSeriesPath.TabIndex = 27
        '
        'Label56
        '
        Me.TableLayoutPanel16.SetColumnSpan(Me.Label56, 4)
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label56.Location = New System.Drawing.Point(4, 488)
        Me.Label56.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(324, 100)
        Me.Label56.TabIndex = 11
        Me.Label56.Text = resources.GetString("Label56.Text")
        '
        'tpTvTable
        '
        Me.tpTvTable.AutoScroll = true
        Me.tpTvTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvTable.Controls.Add(Me.DataGridView2)
        Me.tpTvTable.Location = New System.Drawing.Point(4, 25)
        Me.tpTvTable.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvTable.Name = "tpTvTable"
        Me.tpTvTable.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvTable.TabIndex = 4
        Me.tpTvTable.Text = "Table View"
        Me.tpTvTable.ToolTipText = "Open IMDB in default browser at selected show"
        Me.tpTvTable.UseVisualStyleBackColor = true
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = false
        Me.DataGridView2.AllowUserToDeleteRows = false
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.STitle, Me.SPlot, Me.SPremiered, Me.SRating, Me.SGenre, Me.SStudio, Me.STVDBId, Me.SIMDBId, Me.SCert})
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.MultiSelect = false
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = false
        Me.DataGridView2.Size = New System.Drawing.Size(1037, 624)
        Me.DataGridView2.TabIndex = 0
        '
        'STitle
        '
        Me.STitle.FillWeight = 10!
        Me.STitle.HeaderText = "Show Title"
        Me.STitle.Name = "STitle"
        '
        'SPlot
        '
        Me.SPlot.FillWeight = 40!
        Me.SPlot.HeaderText = "Plot"
        Me.SPlot.Name = "SPlot"
        Me.SPlot.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'SPremiered
        '
        Me.SPremiered.FillWeight = 10!
        Me.SPremiered.HeaderText = "Premiered"
        Me.SPremiered.Name = "SPremiered"
        '
        'SRating
        '
        Me.SRating.FillWeight = 6!
        Me.SRating.HeaderText = "Rating"
        Me.SRating.MinimumWidth = 50
        Me.SRating.Name = "SRating"
        '
        'SGenre
        '
        Me.SGenre.FillWeight = 12!
        Me.SGenre.HeaderText = "Genre"
        Me.SGenre.Name = "SGenre"
        '
        'SStudio
        '
        Me.SStudio.FillWeight = 8!
        Me.SStudio.HeaderText = "Studio"
        Me.SStudio.Name = "SStudio"
        '
        'STVDBId
        '
        Me.STVDBId.FillWeight = 6!
        Me.STVDBId.HeaderText = "TVDB ID"
        Me.STVDBId.MinimumWidth = 60
        Me.STVDBId.Name = "STVDBId"
        '
        'SIMDBId
        '
        Me.SIMDBId.FillWeight = 6!
        Me.SIMDBId.HeaderText = "IMDB ID"
        Me.SIMDBId.MinimumWidth = 60
        Me.SIMDBId.Name = "SIMDBId"
        '
        'SCert
        '
        Me.SCert.FillWeight = 7!
        Me.SCert.HeaderText = "Certificate"
        Me.SCert.Name = "SCert"
        '
        'tpTvWeb
        '
        Me.tpTvWeb.AutoScroll = true
        Me.tpTvWeb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvWeb.Controls.Add(Me.TableLayoutPanel15)
        Me.tpTvWeb.ImageIndex = 0
        Me.tpTvWeb.Location = New System.Drawing.Point(4, 25)
        Me.tpTvWeb.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvWeb.Name = "tpTvWeb"
        Me.tpTvWeb.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvWeb.TabIndex = 5
        Me.tpTvWeb.ToolTipText = "Open TVDB in default browser at selected show"
        Me.tpTvWeb.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 6
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 263!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 153!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel15.Controls.Add(Me.Label194, 1, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.WebBrowser4, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.btn_TvIMDB, 4, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.Label195, 3, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.btn_TvTVDb, 2, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.Panel14, 0, 1)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 2
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.94847!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.05153!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(1037, 624)
        Me.TableLayoutPanel15.TabIndex = 5
        '
        'Label194
        '
        Me.Label194.AutoSize = true
        Me.Label194.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label194.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label194.Location = New System.Drawing.Point(266, 573)
        Me.Label194.Name = "Label194"
        Me.Label194.Padding = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.Label194.Size = New System.Drawing.Size(147, 51)
        Me.Label194.TabIndex = 3
        Me.Label194.Text = "Select page from :"
        '
        'WebBrowser4
        '
        Me.TableLayoutPanel15.SetColumnSpan(Me.WebBrowser4, 6)
        Me.WebBrowser4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser4.Location = New System.Drawing.Point(4, 4)
        Me.WebBrowser4.Margin = New System.Windows.Forms.Padding(4)
        Me.WebBrowser4.MinimumSize = New System.Drawing.Size(25, 25)
        Me.WebBrowser4.Name = "WebBrowser4"
        Me.WebBrowser4.Size = New System.Drawing.Size(1029, 565)
        Me.WebBrowser4.TabIndex = 0
        '
        'btn_TvIMDB
        '
        Me.btn_TvIMDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_TvIMDB.Image = Global.Media_Companion.My.Resources.Resources.imdb1
        Me.btn_TvIMDB.Location = New System.Drawing.Point(560, 576)
        Me.btn_TvIMDB.Name = "btn_TvIMDB"
        Me.btn_TvIMDB.Size = New System.Drawing.Size(113, 45)
        Me.btn_TvIMDB.TabIndex = 2
        Me.btn_TvIMDB.UseVisualStyleBackColor = true
        '
        'Label195
        '
        Me.Label195.AutoSize = true
        Me.Label195.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label195.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label195.Location = New System.Drawing.Point(523, 573)
        Me.Label195.Name = "Label195"
        Me.Label195.Padding = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.Label195.Size = New System.Drawing.Size(31, 51)
        Me.Label195.TabIndex = 4
        Me.Label195.Text = "Or"
        '
        'btn_TvTVDb
        '
        Me.btn_TvTVDb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_TvTVDb.Image = Global.Media_Companion.My.Resources.Resources.TVDB_sm
        Me.btn_TvTVDb.Location = New System.Drawing.Point(419, 576)
        Me.btn_TvTVDb.Name = "btn_TvTVDb"
        Me.btn_TvTVDb.Size = New System.Drawing.Size(98, 45)
        Me.btn_TvTVDb.TabIndex = 1
        Me.btn_TvTVDb.UseVisualStyleBackColor = true
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.btnTvWebStop)
        Me.Panel14.Controls.Add(Me.btnTvWebBack)
        Me.Panel14.Controls.Add(Me.btnTvWebForward)
        Me.Panel14.Controls.Add(Me.btnTvWebRefresh)
        Me.Panel14.Location = New System.Drawing.Point(3, 576)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(257, 45)
        Me.Panel14.TabIndex = 5
        '
        'btnTvWebStop
        '
        Me.btnTvWebStop.BackgroundImage = Global.Media_Companion.My.Resources.Resources.incorrect
        Me.btnTvWebStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTvWebStop.Location = New System.Drawing.Point(17, 5)
        Me.btnTvWebStop.Name = "btnTvWebStop"
        Me.btnTvWebStop.Size = New System.Drawing.Size(36, 36)
        Me.btnTvWebStop.TabIndex = 3
        Me.btnTvWebStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnTvWebStop.UseVisualStyleBackColor = true
        '
        'btnTvWebBack
        '
        Me.btnTvWebBack.BackgroundImage = Global.Media_Companion.My.Resources.Resources.arrow_roll_Back
        Me.btnTvWebBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTvWebBack.Location = New System.Drawing.Point(128, 5)
        Me.btnTvWebBack.Name = "btnTvWebBack"
        Me.btnTvWebBack.Size = New System.Drawing.Size(36, 36)
        Me.btnTvWebBack.TabIndex = 2
        Me.btnTvWebBack.UseVisualStyleBackColor = true
        '
        'btnTvWebForward
        '
        Me.btnTvWebForward.BackgroundImage = Global.Media_Companion.My.Resources.Resources.arrow_roll_Forward
        Me.btnTvWebForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTvWebForward.Location = New System.Drawing.Point(189, 5)
        Me.btnTvWebForward.Name = "btnTvWebForward"
        Me.btnTvWebForward.Size = New System.Drawing.Size(36, 36)
        Me.btnTvWebForward.TabIndex = 1
        Me.btnTvWebForward.UseVisualStyleBackColor = true
        '
        'btnTvWebRefresh
        '
        Me.btnTvWebRefresh.BackgroundImage = Global.Media_Companion.My.Resources.Resources.RefreshAll
        Me.btnTvWebRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTvWebRefresh.Location = New System.Drawing.Point(72, 5)
        Me.btnTvWebRefresh.Name = "btnTvWebRefresh"
        Me.btnTvWebRefresh.Size = New System.Drawing.Size(36, 36)
        Me.btnTvWebRefresh.TabIndex = 0
        Me.btnTvWebRefresh.UseVisualStyleBackColor = true
        '
        'tpTvFolders
        '
        Me.tpTvFolders.AutoScroll = true
        Me.tpTvFolders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpTvFolders.Controls.Add(Me.SplitContainer9)
        Me.tpTvFolders.Location = New System.Drawing.Point(4, 25)
        Me.tpTvFolders.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTvFolders.Name = "tpTvFolders"
        Me.tpTvFolders.Size = New System.Drawing.Size(1041, 628)
        Me.tpTvFolders.TabIndex = 8
        Me.tpTvFolders.Text = "Folders"
        Me.tpTvFolders.UseVisualStyleBackColor = true
        '
        'SplitContainer9
        '
        Me.SplitContainer9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer9.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer9.IsSplitterFixed = true
        Me.SplitContainer9.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer9.Name = "SplitContainer9"
        Me.SplitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer9.Panel1
        '
        Me.SplitContainer9.Panel1.Controls.Add(Me.SplitContainer6)
        '
        'SplitContainer9.Panel2
        '
        Me.SplitContainer9.Panel2.Controls.Add(Me.TableLayoutPanel25)
        Me.SplitContainer9.Size = New System.Drawing.Size(1037, 624)
        Me.SplitContainer9.SplitterDistance = 572
        Me.SplitContainer9.TabIndex = 9
        '
        'SplitContainer6
        '
        Me.SplitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.IsSplitterFixed = true
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer6.Name = "SplitContainer6"
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.TableLayoutPanel5)
        Me.SplitContainer6.Size = New System.Drawing.Size(1037, 572)
        Me.SplitContainer6.SplitterDistance = 500
        Me.SplitContainer6.SplitterWidth = 5
        Me.SplitContainer6.TabIndex = 6
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_TvFoldersRootRemove, 3, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label80, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_TvFoldersRootAdd, 5, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label82, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox39, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_TvFoldersRootBrowse, 2, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label81, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.clbx_TvRootFolders, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 9
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(496, 568)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'btn_TvFoldersRootRemove
        '
        Me.btn_TvFoldersRootRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_TvFoldersRootRemove, 2)
        Me.btn_TvFoldersRootRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersRootRemove.Location = New System.Drawing.Point(297, 529)
        Me.btn_TvFoldersRootRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersRootRemove.Name = "btn_TvFoldersRootRemove"
        Me.btn_TvFoldersRootRemove.Size = New System.Drawing.Size(143, 30)
        Me.btn_TvFoldersRootRemove.TabIndex = 7
        Me.btn_TvFoldersRootRemove.Text = "Remove Selected Folder(s)"
        Me.btn_TvFoldersRootRemove.UseVisualStyleBackColor = true
        '
        'Label80
        '
        Me.Label80.AutoSize = true
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label80, 2)
        Me.Label80.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label80.Location = New System.Drawing.Point(9, 5)
        Me.Label80.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(112, 15)
        Me.Label80.TabIndex = 1
        Me.Label80.Text = "List of Root Folders"
        '
        'btn_TvFoldersRootAdd
        '
        Me.btn_TvFoldersRootAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_TvFoldersRootAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersRootAdd.Location = New System.Drawing.Point(388, 498)
        Me.btn_TvFoldersRootAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersRootAdd.Name = "btn_TvFoldersRootAdd"
        Me.btn_TvFoldersRootAdd.Size = New System.Drawing.Size(52, 23)
        Me.btn_TvFoldersRootAdd.TabIndex = 4
        Me.btn_TvFoldersRootAdd.Text = "Add"
        Me.btn_TvFoldersRootAdd.UseVisualStyleBackColor = true
        '
        'Label82
        '
        Me.Label82.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label82.AutoSize = true
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label82, 2)
        Me.Label82.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label82.Location = New System.Drawing.Point(9, 479)
        Me.Label82.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(108, 15)
        Me.Label82.TabIndex = 5
        Me.Label82.Text = "Manually add path"
        '
        'TextBox39
        '
        Me.TextBox39.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBox39, 4)
        Me.TextBox39.Location = New System.Drawing.Point(9, 500)
        Me.TextBox39.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox39.Name = "TextBox39"
        Me.TextBox39.Size = New System.Drawing.Size(371, 21)
        Me.TextBox39.TabIndex = 3
        '
        'btn_TvFoldersRootBrowse
        '
        Me.btn_TvFoldersRootBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_TvFoldersRootBrowse, 2)
        Me.btn_TvFoldersRootBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersRootBrowse.Location = New System.Drawing.Point(164, 529)
        Me.btn_TvFoldersRootBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersRootBrowse.Name = "btn_TvFoldersRootBrowse"
        Me.btn_TvFoldersRootBrowse.Size = New System.Drawing.Size(125, 30)
        Me.btn_TvFoldersRootBrowse.TabIndex = 6
        Me.btn_TvFoldersRootBrowse.Text = "Browse for Folder"
        Me.btn_TvFoldersRootBrowse.UseVisualStyleBackColor = true
        '
        'Label81
        '
        Me.Label81.AutoSize = true
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label81, 4)
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label81.Location = New System.Drawing.Point(297, 25)
        Me.Label81.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(193, 429)
        Me.Label81.TabIndex = 2
        Me.Label81.Text = resources.GetString("Label81.Text")
        '
        'clbx_TvRootFolders
        '
        Me.clbx_TvRootFolders.AllowDrop = true
        Me.clbx_TvRootFolders.CheckOnClick = true
        Me.TableLayoutPanel1.SetColumnSpan(Me.clbx_TvRootFolders, 3)
        Me.clbx_TvRootFolders.ContextMenuStrip = Me.TvRootFolderContextMenu
        Me.clbx_TvRootFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbx_TvRootFolders.FormattingEnabled = true
        Me.clbx_TvRootFolders.Location = New System.Drawing.Point(8, 28)
        Me.clbx_TvRootFolders.Name = "clbx_TvRootFolders"
        Me.clbx_TvRootFolders.Size = New System.Drawing.Size(282, 423)
        Me.clbx_TvRootFolders.Sorted = true
        Me.clbx_TvRootFolders.TabIndex = 8
        '
        'TvRootFolderContextMenu
        '
        Me.TvRootFolderContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_tvRtAddSeries})
        Me.TvRootFolderContextMenu.Name = "TvRootFolderContextMenu"
        Me.TvRootFolderContextMenu.Size = New System.Drawing.Size(304, 26)
        '
        'tsmi_tvRtAddSeries
        '
        Me.tsmi_tvRtAddSeries.Name = "tsmi_tvRtAddSeries"
        Me.tsmi_tvRtAddSeries.Size = New System.Drawing.Size(303, 22)
        Me.tsmi_tvRtAddSeries.Text = "Add To Series Folders from Selected RootFolder"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 5
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 173!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label83, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.bnt_TvChkFolderList, 3, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.ListBox6, 1, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.btn_TvFoldersAdd, 3, 6)
        Me.TableLayoutPanel5.Controls.Add(Me.btn_TvFoldersAddFromRoot, 1, 7)
        Me.TableLayoutPanel5.Controls.Add(Me.btn_TvFoldersRemove, 3, 7)
        Me.TableLayoutPanel5.Controls.Add(Me.Label85, 1, 5)
        Me.TableLayoutPanel5.Controls.Add(Me.btn_TvFoldersBrowse, 2, 7)
        Me.TableLayoutPanel5.Controls.Add(Me.TextBox40, 1, 6)
        Me.TableLayoutPanel5.Controls.Add(Me.Label84, 3, 2)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 9
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(528, 568)
        Me.TableLayoutPanel5.TabIndex = 15
        '
        'Label83
        '
        Me.Label83.AutoSize = true
        Me.Label83.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label83.Location = New System.Drawing.Point(11, 5)
        Me.Label83.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(121, 15)
        Me.Label83.TabIndex = 1
        Me.Label83.Text = "List of Series Folders"
        '
        'bnt_TvChkFolderList
        '
        Me.bnt_TvChkFolderList.Dock = System.Windows.Forms.DockStyle.Left
        Me.bnt_TvChkFolderList.Location = New System.Drawing.Point(348, 428)
        Me.bnt_TvChkFolderList.Name = "bnt_TvChkFolderList"
        Me.TableLayoutPanel5.SetRowSpan(Me.bnt_TvChkFolderList, 3)
        Me.bnt_TvChkFolderList.Size = New System.Drawing.Size(120, 56)
        Me.bnt_TvChkFolderList.TabIndex = 14
        Me.bnt_TvChkFolderList.Text = "Check List For"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Removed Folders"
        Me.bnt_TvChkFolderList.UseVisualStyleBackColor = true
        '
        'ListBox6
        '
        Me.ListBox6.AllowDrop = true
        Me.ListBox6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel5.SetColumnSpan(Me.ListBox6, 2)
        Me.ListBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ListBox6.FormattingEnabled = true
        Me.ListBox6.ItemHeight = 15
        Me.ListBox6.Location = New System.Drawing.Point(11, 29)
        Me.ListBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBox6.Name = "ListBox6"
        Me.TableLayoutPanel5.SetRowSpan(Me.ListBox6, 3)
        Me.ListBox6.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox6.Size = New System.Drawing.Size(330, 424)
        Me.ListBox6.Sorted = true
        Me.ListBox6.TabIndex = 0
        '
        'btn_TvFoldersAdd
        '
        Me.btn_TvFoldersAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.btn_TvFoldersAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersAdd.Location = New System.Drawing.Point(349, 491)
        Me.btn_TvFoldersAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersAdd.Name = "btn_TvFoldersAdd"
        Me.btn_TvFoldersAdd.Size = New System.Drawing.Size(45, 30)
        Me.btn_TvFoldersAdd.TabIndex = 12
        Me.btn_TvFoldersAdd.Text = "Add"
        Me.btn_TvFoldersAdd.UseVisualStyleBackColor = true
        '
        'btn_TvFoldersRemove
        '
        Me.btn_TvFoldersRemove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_TvFoldersRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersRemove.Location = New System.Drawing.Point(349, 529)
        Me.btn_TvFoldersRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersRemove.Name = "btn_TvFoldersRemove"
        Me.btn_TvFoldersRemove.Size = New System.Drawing.Size(165, 30)
        Me.btn_TvFoldersRemove.TabIndex = 9
        Me.btn_TvFoldersRemove.Text = "Remove Selected Folder(s)"
        Me.btn_TvFoldersRemove.UseVisualStyleBackColor = true
        '
        'Label85
        '
        Me.Label85.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label85.AutoSize = true
        Me.Label85.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label85.Location = New System.Drawing.Point(11, 472)
        Me.Label85.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(108, 15)
        Me.Label85.TabIndex = 10
        Me.Label85.Text = "Manually add path"
        '
        'btn_TvFoldersBrowse
        '
        Me.btn_TvFoldersBrowse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_TvFoldersBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersBrowse.Location = New System.Drawing.Point(180, 529)
        Me.btn_TvFoldersBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersBrowse.Name = "btn_TvFoldersBrowse"
        Me.btn_TvFoldersBrowse.Size = New System.Drawing.Size(161, 30)
        Me.btn_TvFoldersBrowse.TabIndex = 8
        Me.btn_TvFoldersBrowse.Text = "Browse for Folder"
        Me.btn_TvFoldersBrowse.UseVisualStyleBackColor = true
        '
        'TextBox40
        '
        Me.TextBox40.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel5.SetColumnSpan(Me.TextBox40, 2)
        Me.TextBox40.Location = New System.Drawing.Point(11, 500)
        Me.TextBox40.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox40.Name = "TextBox40"
        Me.TextBox40.Size = New System.Drawing.Size(330, 21)
        Me.TextBox40.TabIndex = 11
        '
        'Label84
        '
        Me.Label84.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label84.AutoSize = true
        Me.Label84.Location = New System.Drawing.Point(353, 25)
        Me.Label84.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(161, 210)
        Me.Label84.TabIndex = 2
        Me.Label84.Text = resources.GetString("Label84.Text")
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.ColumnCount = 3
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190!))
        Me.TableLayoutPanel25.Controls.Add(Me.btn_TvFoldersUndo, 1, 1)
        Me.TableLayoutPanel25.Controls.Add(Me.btn_TvFoldersSave, 2, 1)
        Me.TableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 3
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(1037, 48)
        Me.TableLayoutPanel25.TabIndex = 9
        '
        'btn_TvFoldersUndo
        '
        Me.btn_TvFoldersUndo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_TvFoldersUndo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersUndo.Location = New System.Drawing.Point(663, 9)
        Me.btn_TvFoldersUndo.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersUndo.MaximumSize = New System.Drawing.Size(180, 30)
        Me.btn_TvFoldersUndo.MinimumSize = New System.Drawing.Size(180, 30)
        Me.btn_TvFoldersUndo.Name = "btn_TvFoldersUndo"
        Me.btn_TvFoldersUndo.Size = New System.Drawing.Size(180, 30)
        Me.btn_TvFoldersUndo.TabIndex = 8
        Me.btn_TvFoldersUndo.Text = "Undo Changes"
        Me.btn_TvFoldersUndo.UseVisualStyleBackColor = true
        '
        'btn_TvFoldersSave
        '
        Me.btn_TvFoldersSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_TvFoldersSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_TvFoldersSave.Location = New System.Drawing.Point(853, 9)
        Me.btn_TvFoldersSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_TvFoldersSave.MaximumSize = New System.Drawing.Size(180, 30)
        Me.btn_TvFoldersSave.MinimumSize = New System.Drawing.Size(180, 30)
        Me.btn_TvFoldersSave.Name = "btn_TvFoldersSave"
        Me.btn_TvFoldersSave.Size = New System.Drawing.Size(180, 30)
        Me.btn_TvFoldersSave.TabIndex = 7
        Me.btn_TvFoldersSave.Text = "Save Changes"
        Me.btn_TvFoldersSave.UseVisualStyleBackColor = true
        '
        'TabPage24
        '
        Me.TabPage24.AutoScroll = true
        Me.TabPage24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage24.Location = New System.Drawing.Point(4, 25)
        Me.TabPage24.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage24.Name = "TabPage24"
        Me.TabPage24.Size = New System.Drawing.Size(1041, 628)
        Me.TabPage24.TabIndex = 9
        Me.TabPage24.Text = "TV Preferences"
        Me.TabPage24.UseVisualStyleBackColor = true
        '
        'ImageList3
        '
        Me.ImageList3.ImageStream = CType(resources.GetObject("ImageList3.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ImageList3.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList3.Images.SetKeyName(0, "TVDB_sm.jpg")
        Me.ImageList3.Images.SetKeyName(1, "imdb.png")
        '
        'TabMV
        '
        Me.TabMV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TabMV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabMV.Controls.Add(Me.UcMusicVideo1)
        Me.TabMV.Location = New System.Drawing.Point(4, 24)
        Me.TabMV.Name = "TabMV"
        Me.TabMV.Size = New System.Drawing.Size(747, 72)
        Me.TabMV.TabIndex = 15
        Me.TabMV.Text = "Music Videos"
        Me.TabMV.UseVisualStyleBackColor = true
        '
        'UcMusicVideo1
        '
        Me.UcMusicVideo1.AutoScroll = true
        Me.UcMusicVideo1.AutoSize = true
        Me.UcMusicVideo1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcMusicVideo1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcMusicVideo1.Location = New System.Drawing.Point(0, 0)
        Me.UcMusicVideo1.Name = "UcMusicVideo1"
        Me.UcMusicVideo1.Size = New System.Drawing.Size(743, 68)
        Me.UcMusicVideo1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage3.Controls.Add(Me.TabControl1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(747, 72)
        Me.TabPage3.TabIndex = 13
        Me.TabPage3.Text = "Home Movies"
        Me.TabPage3.UseVisualStyleBackColor = true
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabControl1.Controls.Add(Me.tp_HmMainBrowser)
        Me.TabControl1.Controls.Add(Me.tp_HmScrnSht)
        Me.TabControl1.Controls.Add(Me.tp_HmPoster)
        Me.TabControl1.Controls.Add(Me.tp_HmFolders)
        Me.TabControl1.Controls.Add(Me.tp_HmPref)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(743, 68)
        Me.TabControl1.TabIndex = 0
        '
        'tp_HmMainBrowser
        '
        Me.tp_HmMainBrowser.Controls.Add(Me.TableLayoutPanel21)
        Me.tp_HmMainBrowser.Controls.Add(Me.TextBox20)
        Me.tp_HmMainBrowser.Controls.Add(Me.Label169)
        Me.tp_HmMainBrowser.Controls.Add(Me.TextBox23)
        Me.tp_HmMainBrowser.Controls.Add(Me.Label173)
        Me.tp_HmMainBrowser.Controls.Add(Me.Label172)
        Me.tp_HmMainBrowser.Controls.Add(Me.TextBox22)
        Me.tp_HmMainBrowser.Location = New System.Drawing.Point(4, 27)
        Me.tp_HmMainBrowser.Name = "tp_HmMainBrowser"
        Me.tp_HmMainBrowser.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_HmMainBrowser.Size = New System.Drawing.Size(735, 37)
        Me.tp_HmMainBrowser.TabIndex = 0
        Me.tp_HmMainBrowser.Text = "Main Browser"
        Me.tp_HmMainBrowser.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel21
        '
        Me.TableLayoutPanel21.ColumnCount = 13
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196!))
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel21.Controls.Add(Me.btn_HMRefresh, 3, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.btn_HMSearch, 1, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.pbx_HmPoster, 11, 7)
        Me.TableLayoutPanel21.Controls.Add(Me.btnHomeMovieSave, 12, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.HmMovPlot, 6, 3)
        Me.TableLayoutPanel21.Controls.Add(Me.HmMovStars, 6, 6)
        Me.TableLayoutPanel21.Controls.Add(Me.pbx_HmFanart, 6, 10)
        Me.TableLayoutPanel21.Controls.Add(Me.HmMovYear, 6, 8)
        Me.TableLayoutPanel21.Controls.Add(Me.Label113, 5, 8)
        Me.TableLayoutPanel21.Controls.Add(Me.Label168, 9, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.HmMovSort, 10, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.Label39, 5, 6)
        Me.TableLayoutPanel21.Controls.Add(Me.ListBox18, 1, 2)
        Me.TableLayoutPanel21.Controls.Add(Me.Label167, 5, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.Label32, 5, 3)
        Me.TableLayoutPanel21.Controls.Add(Me.HmMovTitle, 6, 1)
        Me.TableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel21.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel21.Name = "TableLayoutPanel21"
        Me.TableLayoutPanel21.RowCount = 13
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 218!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55!))
        Me.TableLayoutPanel21.Size = New System.Drawing.Size(729, 31)
        Me.TableLayoutPanel21.TabIndex = 37
        '
        'pbx_HmPoster
        '
        Me.pbx_HmPoster.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pbx_HmPoster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pbx_HmPoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbx_HmPoster.Location = New System.Drawing.Point(496, 256)
        Me.pbx_HmPoster.Name = "pbx_HmPoster"
        Me.TableLayoutPanel21.SetRowSpan(Me.pbx_HmPoster, 4)
        Me.pbx_HmPoster.Size = New System.Drawing.Size(190, 283)
        Me.pbx_HmPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_HmPoster.TabIndex = 37
        Me.pbx_HmPoster.TabStop = false
        '
        'btnHomeMovieSave
        '
        Me.btnHomeMovieSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnHomeMovieSave.BackgroundImage = CType(resources.GetObject("btnHomeMovieSave.BackgroundImage"),System.Drawing.Image)
        Me.btnHomeMovieSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHomeMovieSave.Location = New System.Drawing.Point(696, 13)
        Me.btnHomeMovieSave.Name = "btnHomeMovieSave"
        Me.btnHomeMovieSave.Size = New System.Drawing.Size(30, 30)
        Me.btnHomeMovieSave.TabIndex = 28
        Me.btnHomeMovieSave.UseVisualStyleBackColor = true
        '
        'HmMovPlot
        '
        Me.TableLayoutPanel21.SetColumnSpan(Me.HmMovPlot, 6)
        Me.HmMovPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmMovPlot.Location = New System.Drawing.Point(384, 82)
        Me.HmMovPlot.Multiline = true
        Me.HmMovPlot.Name = "HmMovPlot"
        Me.TableLayoutPanel21.SetRowSpan(Me.HmMovPlot, 2)
        Me.HmMovPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.HmMovPlot.Size = New System.Drawing.Size(302, 119)
        Me.HmMovPlot.TabIndex = 31
        '
        'HmMovStars
        '
        Me.TableLayoutPanel21.SetColumnSpan(Me.HmMovStars, 2)
        Me.HmMovStars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmMovStars.Location = New System.Drawing.Point(384, 225)
        Me.HmMovStars.Name = "HmMovStars"
        Me.HmMovStars.Size = New System.Drawing.Size(15, 21)
        Me.HmMovStars.TabIndex = 33
        '
        'pbx_HmFanart
        '
        Me.pbx_HmFanart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pbx_HmFanart.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pbx_HmFanart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TableLayoutPanel21.SetColumnSpan(Me.pbx_HmFanart, 4)
        Me.pbx_HmFanart.Location = New System.Drawing.Point(384, 327)
        Me.pbx_HmFanart.Name = "pbx_HmFanart"
        Me.pbx_HmFanart.Size = New System.Drawing.Size(86, 212)
        Me.pbx_HmFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_HmFanart.TabIndex = 27
        Me.pbx_HmFanart.TabStop = false
        '
        'HmMovYear
        '
        Me.HmMovYear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmMovYear.Location = New System.Drawing.Point(384, 272)
        Me.HmMovYear.Name = "HmMovYear"
        Me.HmMovYear.Size = New System.Drawing.Size(123, 21)
        Me.HmMovYear.TabIndex = 35
        '
        'Label113
        '
        Me.Label113.AutoSize = true
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Location = New System.Drawing.Point(343, 269)
        Me.Label113.Name = "Label113"
        Me.Label113.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label113.Size = New System.Drawing.Size(35, 30)
        Me.Label113.TabIndex = 36
        Me.Label113.Text = "Year:"
        '
        'Label168
        '
        Me.Label168.AutoSize = true
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label168.Location = New System.Drawing.Point(438, 10)
        Me.Label168.Name = "Label168"
        Me.Label168.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label168.Size = New System.Drawing.Size(32, 45)
        Me.Label168.TabIndex = 22
        Me.Label168.Text = "Sort:"
        '
        'HmMovSort
        '
        Me.HmMovSort.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel21.SetColumnSpan(Me.HmMovSort, 2)
        Me.HmMovSort.Location = New System.Drawing.Point(476, 13)
        Me.HmMovSort.Name = "HmMovSort"
        Me.HmMovSort.Size = New System.Drawing.Size(210, 21)
        Me.HmMovSort.TabIndex = 20
        '
        'Label39
        '
        Me.Label39.AutoSize = true
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Location = New System.Drawing.Point(335, 222)
        Me.Label39.Name = "Label39"
        Me.Label39.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label39.Size = New System.Drawing.Size(43, 31)
        Me.Label39.TabIndex = 34
        Me.Label39.Text = "Starring:"
        '
        'ListBox18
        '
        Me.TableLayoutPanel21.SetColumnSpan(Me.ListBox18, 3)
        Me.ListBox18.ContextMenuStrip = Me.HomeMovieContextMenu
        Me.ListBox18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox18.FormattingEnabled = true
        Me.ListBox18.ItemHeight = 15
        Me.ListBox18.Location = New System.Drawing.Point(9, 58)
        Me.ListBox18.Name = "ListBox18"
        Me.TableLayoutPanel21.SetRowSpan(Me.ListBox18, 10)
        Me.ListBox18.Size = New System.Drawing.Size(291, 1)
        Me.ListBox18.TabIndex = 18
        '
        'HomeMovieContextMenu
        '
        Me.HomeMovieContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlaceHolderforHomeMovieTitleToolStripMenuItem, Me.ToolStripSeparator25, Me.PlayHomeMovieToolStripMenuItem, Me.ToolStripSeparator26, Me.OpenFolderToolStripMenuItem, Me.OpenFileToolStripMenuItem})
        Me.HomeMovieContextMenu.Name = "HomeMovieContextMenu"
        Me.HomeMovieContextMenu.Size = New System.Drawing.Size(220, 104)
        '
        'PlaceHolderforHomeMovieTitleToolStripMenuItem
        '
        Me.PlaceHolderforHomeMovieTitleToolStripMenuItem.Name = "PlaceHolderforHomeMovieTitleToolStripMenuItem"
        Me.PlaceHolderforHomeMovieTitleToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.PlaceHolderforHomeMovieTitleToolStripMenuItem.Text = "PlaceHolderforHomeMovieTitle"
        '
        'ToolStripSeparator25
        '
        Me.ToolStripSeparator25.Name = "ToolStripSeparator25"
        Me.ToolStripSeparator25.Size = New System.Drawing.Size(216, 6)
        '
        'PlayHomeMovieToolStripMenuItem
        '
        Me.PlayHomeMovieToolStripMenuItem.Name = "PlayHomeMovieToolStripMenuItem"
        Me.PlayHomeMovieToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.PlayHomeMovieToolStripMenuItem.Text = "Play Home Movie"
        '
        'ToolStripSeparator26
        '
        Me.ToolStripSeparator26.Name = "ToolStripSeparator26"
        Me.ToolStripSeparator26.Size = New System.Drawing.Size(216, 6)
        '
        'OpenFolderToolStripMenuItem
        '
        Me.OpenFolderToolStripMenuItem.Name = "OpenFolderToolStripMenuItem"
        Me.OpenFolderToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.OpenFolderToolStripMenuItem.Text = "Open Folder"
        '
        'OpenFileToolStripMenuItem
        '
        Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
        Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.OpenFileToolStripMenuItem.Text = "Open File"
        '
        'Label167
        '
        Me.Label167.AutoSize = true
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label167.Location = New System.Drawing.Point(345, 10)
        Me.Label167.Name = "Label167"
        Me.Label167.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label167.Size = New System.Drawing.Size(33, 45)
        Me.Label167.TabIndex = 21
        Me.Label167.Text = "Title:"
        '
        'Label32
        '
        Me.Label32.AutoSize = true
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label32.Location = New System.Drawing.Point(347, 79)
        Me.Label32.Name = "Label32"
        Me.Label32.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label32.Size = New System.Drawing.Size(31, 77)
        Me.Label32.TabIndex = 32
        Me.Label32.Text = "Plot:"
        '
        'HmMovTitle
        '
        Me.HmMovTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel21.SetColumnSpan(Me.HmMovTitle, 2)
        Me.HmMovTitle.Location = New System.Drawing.Point(384, 13)
        Me.HmMovTitle.Name = "HmMovTitle"
        Me.HmMovTitle.Size = New System.Drawing.Size(15, 21)
        Me.HmMovTitle.TabIndex = 19
        '
        'TextBox20
        '
        Me.TextBox20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBox20.Location = New System.Drawing.Point(608, -2248)
        Me.TextBox20.Name = "TextBox20"
        Me.TextBox20.Size = New System.Drawing.Size(121, 21)
        Me.TextBox20.TabIndex = 30
        '
        'Label169
        '
        Me.Label169.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label169.AutoSize = true
        Me.Label169.Location = New System.Drawing.Point(567, -2245)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(35, 15)
        Me.Label169.TabIndex = 29
        Me.Label169.Text = "Year:"
        '
        'TextBox23
        '
        Me.TextBox23.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBox23.Location = New System.Drawing.Point(364, -2248)
        Me.TextBox23.Name = "TextBox23"
        Me.TextBox23.Size = New System.Drawing.Size(555, 21)
        Me.TextBox23.TabIndex = 26
        '
        'Label173
        '
        Me.Label173.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label173.AutoSize = true
        Me.Label173.Location = New System.Drawing.Point(305, -2245)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(53, 15)
        Me.Label173.TabIndex = 25
        Me.Label173.Text = "Starring:"
        '
        'Label172
        '
        Me.Label172.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label172.AutoSize = true
        Me.Label172.Location = New System.Drawing.Point(309, -2370)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(49, 15)
        Me.Label172.TabIndex = 24
        Me.Label172.Text = "Outline:"
        '
        'TextBox22
        '
        Me.TextBox22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBox22.Location = New System.Drawing.Point(364, -2370)
        Me.TextBox22.Multiline = true
        Me.TextBox22.Name = "TextBox22"
        Me.TextBox22.Size = New System.Drawing.Size(555, 116)
        Me.TextBox22.TabIndex = 23
        '
        'tp_HmScrnSht
        '
        Me.tp_HmScrnSht.Controls.Add(Me.TableLayoutPanel27)
        Me.tp_HmScrnSht.Location = New System.Drawing.Point(4, 27)
        Me.tp_HmScrnSht.Name = "tp_HmScrnSht"
        Me.tp_HmScrnSht.Size = New System.Drawing.Size(180, 36)
        Me.tp_HmScrnSht.TabIndex = 3
        Me.tp_HmScrnSht.Text = "Screenshot"
        Me.tp_HmScrnSht.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.ColumnCount = 9
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel27.Controls.Add(Me.Label170, 2, 3)
        Me.TableLayoutPanel27.Controls.Add(Me.pbx_HmFanartSht, 1, 1)
        Me.TableLayoutPanel27.Controls.Add(Me.btn_HmFanartShot, 6, 3)
        Me.TableLayoutPanel27.Controls.Add(Me.tb_HmFanartTime, 4, 3)
        Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 6
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(180, 36)
        Me.TableLayoutPanel27.TabIndex = 4
        '
        'Label170
        '
        Me.Label170.AutoSize = true
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label170.Location = New System.Drawing.Point(124, -12)
        Me.Label170.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(144, 34)
        Me.Label170.TabIndex = 3
        Me.Label170.Text = "Location within media in"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"seconds for Screen Shot"
        '
        'pbx_HmFanartSht
        '
        Me.TableLayoutPanel27.SetColumnSpan(Me.pbx_HmFanartSht, 7)
        Me.pbx_HmFanartSht.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbx_HmFanartSht.Location = New System.Drawing.Point(7, 7)
        Me.pbx_HmFanartSht.Name = "pbx_HmFanartSht"
        Me.pbx_HmFanartSht.Size = New System.Drawing.Size(166, 1)
        Me.pbx_HmFanartSht.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_HmFanartSht.TabIndex = 0
        Me.pbx_HmFanartSht.TabStop = false
        '
        'btn_HmFanartShot
        '
        Me.btn_HmFanartShot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btn_HmFanartShot.Location = New System.Drawing.Point(391, -13)
        Me.btn_HmFanartShot.Name = "btn_HmFanartShot"
        Me.btn_HmFanartShot.Size = New System.Drawing.Size(130, 32)
        Me.btn_HmFanartShot.TabIndex = 1
        Me.btn_HmFanartShot.Text = "Create Screen Shot"
        Me.btn_HmFanartShot.UseVisualStyleBackColor = true
        '
        'tb_HmFanartTime
        '
        Me.tb_HmFanartTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_HmFanartTime.Location = New System.Drawing.Point(284, -6)
        Me.tb_HmFanartTime.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
        Me.tb_HmFanartTime.Name = "tb_HmFanartTime"
        Me.tb_HmFanartTime.Size = New System.Drawing.Size(93, 21)
        Me.tb_HmFanartTime.TabIndex = 2
        '
        'tp_HmPoster
        '
        Me.tp_HmPoster.Controls.Add(Me.TableLayoutPanel32)
        Me.tp_HmPoster.Location = New System.Drawing.Point(4, 27)
        Me.tp_HmPoster.Name = "tp_HmPoster"
        Me.tp_HmPoster.Size = New System.Drawing.Size(180, 36)
        Me.tp_HmPoster.TabIndex = 5
        Me.tp_HmPoster.Text = " Poster "
        Me.tp_HmPoster.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 10
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 342!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 264!))
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel32.Controls.Add(Me.Label38, 1, 4)
        Me.TableLayoutPanel32.Controls.Add(Me.pbx_HmPosterSht, 1, 1)
        Me.TableLayoutPanel32.Controls.Add(Me.tb_HmPosterTime, 3, 4)
        Me.TableLayoutPanel32.Controls.Add(Me.Label65, 7, 1)
        Me.TableLayoutPanel32.Controls.Add(Me.btn_HmPosterShot, 5, 4)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 7
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 187!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(180, 36)
        Me.TableLayoutPanel32.TabIndex = 5
        '
        'Label38
        '
        Me.Label38.AutoSize = true
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label38.Location = New System.Drawing.Point(7, -12)
        Me.Label38.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(204, 34)
        Me.Label38.TabIndex = 3
        Me.Label38.Text = "Location within media in"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"seconds for Poster Shot"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pbx_HmPosterSht
        '
        Me.TableLayoutPanel32.SetColumnSpan(Me.pbx_HmPosterSht, 6)
        Me.pbx_HmPosterSht.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbx_HmPosterSht.Location = New System.Drawing.Point(7, 7)
        Me.pbx_HmPosterSht.Name = "pbx_HmPosterSht"
        Me.TableLayoutPanel32.SetRowSpan(Me.pbx_HmPosterSht, 2)
        Me.pbx_HmPosterSht.Size = New System.Drawing.Size(1, 1)
        Me.pbx_HmPosterSht.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_HmPosterSht.TabIndex = 0
        Me.pbx_HmPosterSht.TabStop = false
        '
        'tb_HmPosterTime
        '
        Me.tb_HmPosterTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_HmPosterTime.Location = New System.Drawing.Point(226, -6)
        Me.tb_HmPosterTime.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
        Me.tb_HmPosterTime.Name = "tb_HmPosterTime"
        Me.tb_HmPosterTime.Size = New System.Drawing.Size(54, 21)
        Me.tb_HmPosterTime.TabIndex = 2
        '
        'Label65
        '
        Me.Label65.AutoSize = true
        Me.Label65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label65.Location = New System.Drawing.Point(-427, 4)
        Me.Label65.Name = "Label65"
        Me.Label65.Padding = New System.Windows.Forms.Padding(10, 15, 0, 0)
        Me.Label65.Size = New System.Drawing.Size(328, 107)
        Me.Label65.TabIndex = 4
        Me.Label65.Text = resources.GetString("Label65.Text")
        '
        'btn_HmPosterShot
        '
        Me.btn_HmPosterShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_HmPosterShot.Location = New System.Drawing.Point(294, -15)
        Me.btn_HmPosterShot.Name = "btn_HmPosterShot"
        Me.btn_HmPosterShot.Size = New System.Drawing.Size(1, 34)
        Me.btn_HmPosterShot.TabIndex = 1
        Me.btn_HmPosterShot.Text = "Create Poster Shot"
        Me.btn_HmPosterShot.UseVisualStyleBackColor = true
        '
        'tp_HmFolders
        '
        Me.tp_HmFolders.Controls.Add(Me.TableLayoutPanel22)
        Me.tp_HmFolders.Location = New System.Drawing.Point(4, 27)
        Me.tp_HmFolders.Name = "tp_HmFolders"
        Me.tp_HmFolders.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_HmFolders.Size = New System.Drawing.Size(180, 36)
        Me.tp_HmFolders.TabIndex = 1
        Me.tp_HmFolders.Text = "Folders"
        Me.tp_HmFolders.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel22
        '
        Me.TableLayoutPanel22.ColumnCount = 7
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 325!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 193!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66!))
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8!))
        Me.TableLayoutPanel22.Controls.Add(Me.Label166, 0, 1)
        Me.TableLayoutPanel22.Controls.Add(Me.btnHomeFoldersRemove, 4, 2)
        Me.TableLayoutPanel22.Controls.Add(Me.Label114, 2, 3)
        Me.TableLayoutPanel22.Controls.Add(Me.btnHomeFolderAdd, 2, 2)
        Me.TableLayoutPanel22.Controls.Add(Me.btnHomeManualPathAdd, 5, 4)
        Me.TableLayoutPanel22.Controls.Add(Me.tbHomeManualPath, 2, 4)
        Me.TableLayoutPanel22.Controls.Add(Me.btn_HmFolderSaveRefresh, 4, 6)
        Me.TableLayoutPanel22.Controls.Add(Me.clbx_HMMovieFolders, 2, 1)
        Me.TableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel22.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel22.Name = "TableLayoutPanel22"
        Me.TableLayoutPanel22.RowCount = 8
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 438!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56!))
        Me.TableLayoutPanel22.Size = New System.Drawing.Size(174, 30)
        Me.TableLayoutPanel22.TabIndex = 17
        '
        'Label166
        '
        Me.Label166.AutoSize = true
        Me.Label166.Location = New System.Drawing.Point(4, 20)
        Me.Label166.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(302, 120)
        Me.Label166.TabIndex = 10
        Me.Label166.Text = resources.GetString("Label166.Text")
        '
        'btnHomeFoldersRemove
        '
        Me.TableLayoutPanel22.SetColumnSpan(Me.btnHomeFoldersRemove, 2)
        Me.btnHomeFoldersRemove.Location = New System.Drawing.Point(539, 461)
        Me.btnHomeFoldersRemove.Name = "btnHomeFoldersRemove"
        Me.btnHomeFoldersRemove.Size = New System.Drawing.Size(181, 21)
        Me.btnHomeFoldersRemove.TabIndex = 13
        Me.btnHomeFoldersRemove.Text = "Remove Selected Folder"
        Me.btnHomeFoldersRemove.UseVisualStyleBackColor = true
        '
        'Label114
        '
        Me.Label114.AutoSize = true
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label114.Location = New System.Drawing.Point(333, 485)
        Me.Label114.Margin = New System.Windows.Forms.Padding(0)
        Me.Label114.Name = "Label114"
        Me.Label114.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label114.Size = New System.Drawing.Size(150, 27)
        Me.Label114.TabIndex = 16
        Me.Label114.Text = "Home Movie Manual Path"
        '
        'btnHomeFolderAdd
        '
        Me.btnHomeFolderAdd.Location = New System.Drawing.Point(336, 461)
        Me.btnHomeFolderAdd.Name = "btnHomeFolderAdd"
        Me.btnHomeFolderAdd.Size = New System.Drawing.Size(187, 21)
        Me.btnHomeFolderAdd.TabIndex = 12
        Me.btnHomeFolderAdd.Text = "Add Home Movie Folder Browser"
        Me.btnHomeFolderAdd.UseVisualStyleBackColor = true
        '
        'btnHomeManualPathAdd
        '
        Me.btnHomeManualPathAdd.Location = New System.Drawing.Point(660, 515)
        Me.btnHomeManualPathAdd.Name = "btnHomeManualPathAdd"
        Me.btnHomeManualPathAdd.Size = New System.Drawing.Size(56, 28)
        Me.btnHomeManualPathAdd.TabIndex = 15
        Me.btnHomeManualPathAdd.Text = "Add"
        Me.btnHomeManualPathAdd.UseVisualStyleBackColor = true
        '
        'tbHomeManualPath
        '
        Me.TableLayoutPanel22.SetColumnSpan(Me.tbHomeManualPath, 3)
        Me.tbHomeManualPath.Location = New System.Drawing.Point(336, 515)
        Me.tbHomeManualPath.Name = "tbHomeManualPath"
        Me.tbHomeManualPath.Size = New System.Drawing.Size(318, 21)
        Me.tbHomeManualPath.TabIndex = 14
        '
        'btn_HmFolderSaveRefresh
        '
        Me.TableLayoutPanel22.SetColumnSpan(Me.btn_HmFolderSaveRefresh, 2)
        Me.btn_HmFolderSaveRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_HmFolderSaveRefresh.Location = New System.Drawing.Point(539, 561)
        Me.btn_HmFolderSaveRefresh.Name = "btn_HmFolderSaveRefresh"
        Me.btn_HmFolderSaveRefresh.Size = New System.Drawing.Size(181, 33)
        Me.btn_HmFolderSaveRefresh.TabIndex = 17
        Me.btn_HmFolderSaveRefresh.Text = "Save and Refresh"
        Me.btn_HmFolderSaveRefresh.UseVisualStyleBackColor = true
        '
        'clbx_HMMovieFolders
        '
        Me.clbx_HMMovieFolders.AllowDrop = true
        Me.clbx_HMMovieFolders.CheckOnClick = true
        Me.TableLayoutPanel22.SetColumnSpan(Me.clbx_HMMovieFolders, 4)
        Me.clbx_HMMovieFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbx_HMMovieFolders.FormattingEnabled = true
        Me.clbx_HMMovieFolders.Location = New System.Drawing.Point(336, 23)
        Me.clbx_HMMovieFolders.Name = "clbx_HMMovieFolders"
        Me.clbx_HMMovieFolders.Size = New System.Drawing.Size(384, 432)
        Me.clbx_HMMovieFolders.Sorted = true
        Me.clbx_HMMovieFolders.TabIndex = 18
        '
        'tp_HmPref
        '
        Me.tp_HmPref.Location = New System.Drawing.Point(4, 27)
        Me.tp_HmPref.Name = "tp_HmPref"
        Me.tp_HmPref.Size = New System.Drawing.Size(180, 36)
        Me.tp_HmPref.TabIndex = 6
        Me.tp_HmPref.Text = "HomeMovie Preferences"
        Me.tp_HmPref.UseVisualStyleBackColor = true
        '
        'TabCustTv
        '
        Me.TabCustTv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabCustTv.Controls.Add(Me.Custom_Tv1)
        Me.TabCustTv.Location = New System.Drawing.Point(4, 24)
        Me.TabCustTv.Name = "TabCustTv"
        Me.TabCustTv.Size = New System.Drawing.Size(747, 72)
        Me.TabCustTv.TabIndex = 14
        Me.TabCustTv.Text = "Custom Tv Shows"
        Me.TabCustTv.UseVisualStyleBackColor = true
        '
        'Custom_Tv1
        '
        Me.Custom_Tv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Custom_Tv1.Location = New System.Drawing.Point(0, 0)
        Me.Custom_Tv1.Name = "Custom_Tv1"
        Me.Custom_Tv1.Size = New System.Drawing.Size(743, 68)
        Me.Custom_Tv1.TabIndex = 0
        '
        'TabPage34
        '
        Me.TabPage34.Controls.Add(Me.Button109)
        Me.TabPage34.Controls.Add(Me.Label151)
        Me.TabPage34.Controls.Add(Me.Label150)
        Me.TabPage34.Controls.Add(Me.TextBox45)
        Me.TabPage34.Controls.Add(Me.Label149)
        Me.TabPage34.Location = New System.Drawing.Point(4, 24)
        Me.TabPage34.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage34.Name = "TabPage34"
        Me.TabPage34.Size = New System.Drawing.Size(747, 72)
        Me.TabPage34.TabIndex = 4
        Me.TabPage34.Text = "Export"
        Me.TabPage34.UseVisualStyleBackColor = true
        '
        'Button109
        '
        Me.Button109.Location = New System.Drawing.Point(366, 594)
        Me.Button109.Margin = New System.Windows.Forms.Padding(4)
        Me.Button109.Name = "Button109"
        Me.Button109.Size = New System.Drawing.Size(106, 29)
        Me.Button109.TabIndex = 4
        Me.Button109.Text = "Save Changes"
        Me.Button109.UseVisualStyleBackColor = true
        '
        'Label151
        '
        Me.Label151.AutoSize = true
        Me.Label151.Location = New System.Drawing.Point(10, 144)
        Me.Label151.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(370, 30)
        Me.Label151.TabIndex = 3
        Me.Label151.Text = "On some setups - the XBMC path to a file will differ from that of MC,"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"edit the x"& _ 
    "ml below to reflect these differences"
        '
        'Label150
        '
        Me.Label150.AutoSize = true
        Me.Label150.Location = New System.Drawing.Point(10, 115)
        Me.Label150.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(99, 15)
        Me.Label150.TabIndex = 2
        Me.Label150.Text = "Path Substitution"
        '
        'TextBox45
        '
        Me.TextBox45.Location = New System.Drawing.Point(14, 180)
        Me.TextBox45.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox45.Multiline = true
        Me.TextBox45.Name = "TextBox45"
        Me.TextBox45.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox45.Size = New System.Drawing.Size(458, 405)
        Me.TextBox45.TabIndex = 1
        '
        'Label149
        '
        Me.Label149.AutoSize = true
        Me.Label149.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label149.Location = New System.Drawing.Point(10, 11)
        Me.Label149.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(959, 64)
        Me.Label149.TabIndex = 0
        Me.Label149.Text = resources.GetString("Label149.Text")
        '
        'TabControlDebug
        '
        Me.TabControlDebug.Controls.Add(Me.TableLayoutPanel24)
        Me.TabControlDebug.Location = New System.Drawing.Point(4, 24)
        Me.TabControlDebug.Name = "TabControlDebug"
        Me.TabControlDebug.Padding = New System.Windows.Forms.Padding(3)
        Me.TabControlDebug.Size = New System.Drawing.Size(747, 72)
        Me.TabControlDebug.TabIndex = 5
        Me.TabControlDebug.Text = "Debug"
        Me.TabControlDebug.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 6
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 613!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel24.Controls.Add(Me.Label3, 2, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.CheckBoxDebugShowTVDBReturnedXML, 2, 8)
        Me.TableLayoutPanel24.Controls.Add(Me.Label192, 2, 6)
        Me.TableLayoutPanel24.Controls.Add(Me.CheckBoxDebugShowXML, 2, 7)
        Me.TableLayoutPanel24.Controls.Add(Me.ExtraDebugEnable, 2, 2)
        Me.TableLayoutPanel24.Controls.Add(Me.cbClearCache, 2, 4)
        Me.TableLayoutPanel24.Controls.Add(Me.Label23, 2, 3)
        Me.TableLayoutPanel24.Controls.Add(Me.DebugSytemDPITextBox, 3, 3)
        Me.TableLayoutPanel24.Controls.Add(Me.GroupBox29, 1, 9)
        Me.TableLayoutPanel24.Controls.Add(Me.cbClearMissingFolder, 2, 5)
        Me.TableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 11
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 260!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(741, 66)
        Me.TableLayoutPanel24.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.Label3, 2)
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(43, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(351, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Debug Settings are not saved after a reboot..."
        '
        'CheckBoxDebugShowTVDBReturnedXML
        '
        Me.CheckBoxDebugShowTVDBReturnedXML.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.CheckBoxDebugShowTVDBReturnedXML, 2)
        Me.CheckBoxDebugShowTVDBReturnedXML.Location = New System.Drawing.Point(43, 321)
        Me.CheckBoxDebugShowTVDBReturnedXML.Name = "CheckBoxDebugShowTVDBReturnedXML"
        Me.CheckBoxDebugShowTVDBReturnedXML.Size = New System.Drawing.Size(358, 19)
        Me.CheckBoxDebugShowTVDBReturnedXML.TabIndex = 6
        Me.CheckBoxDebugShowTVDBReturnedXML.Text = "Check to Display returned text from MC TVDB episode scrape"
        Me.CheckBoxDebugShowTVDBReturnedXML.UseVisualStyleBackColor = true
        '
        'Label192
        '
        Me.Label192.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.Label192, 2)
        Me.Label192.Location = New System.Drawing.Point(52, 202)
        Me.Label192.Margin = New System.Windows.Forms.Padding(12, 0, 3, 0)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(187, 30)
        Me.Label192.TabIndex = 14
        Me.Label192.Text = "NB: On Restart the above options"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"will be deselected."
        '
        'CheckBoxDebugShowXML
        '
        Me.CheckBoxDebugShowXML.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.CheckBoxDebugShowXML, 2)
        Me.CheckBoxDebugShowXML.Location = New System.Drawing.Point(43, 288)
        Me.CheckBoxDebugShowXML.Name = "CheckBoxDebugShowXML"
        Me.CheckBoxDebugShowXML.Size = New System.Drawing.Size(233, 19)
        Me.CheckBoxDebugShowXML.TabIndex = 5
        Me.CheckBoxDebugShowXML.Text = "Show tabs to show MC's config XML's"
        Me.CheckBoxDebugShowXML.UseVisualStyleBackColor = true
        '
        'ExtraDebugEnable
        '
        Me.ExtraDebugEnable.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.ExtraDebugEnable, 2)
        Me.ExtraDebugEnable.Location = New System.Drawing.Point(43, 74)
        Me.ExtraDebugEnable.Name = "ExtraDebugEnable"
        Me.ExtraDebugEnable.Size = New System.Drawing.Size(223, 19)
        Me.ExtraDebugEnable.TabIndex = 2
        Me.ExtraDebugEnable.Text = "Enable Debug Labels on other Tabs"
        Me.ExtraDebugEnable.UseVisualStyleBackColor = true
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Location = New System.Drawing.Point(103, 109)
        Me.Label23.Name = "Label23"
        Me.Label23.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label23.Size = New System.Drawing.Size(70, 41)
        Me.Label23.TabIndex = 1
        Me.Label23.Text = "System DPI"
        '
        'DebugSytemDPITextBox
        '
        Me.DebugSytemDPITextBox.Location = New System.Drawing.Point(179, 112)
        Me.DebugSytemDPITextBox.Name = "DebugSytemDPITextBox"
        Me.DebugSytemDPITextBox.Size = New System.Drawing.Size(100, 21)
        Me.DebugSytemDPITextBox.TabIndex = 0
        '
        'GroupBox29
        '
        Me.TableLayoutPanel24.SetColumnSpan(Me.GroupBox29, 4)
        Me.GroupBox29.Controls.Add(Me.Label148)
        Me.GroupBox29.Controls.Add(Me.Button2)
        Me.GroupBox29.Controls.Add(Me.GroupBox28)
        Me.GroupBox29.Location = New System.Drawing.Point(23, 360)
        Me.GroupBox29.Name = "GroupBox29"
        Me.GroupBox29.Size = New System.Drawing.Size(953, 124)
        Me.GroupBox29.TabIndex = 12
        Me.GroupBox29.TabStop = false
        Me.GroupBox29.Text = "Fix for Season or Episode set as '-1'"
        '
        'Label148
        '
        Me.Label148.AutoSize = true
        Me.Label148.Location = New System.Drawing.Point(209, 38)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(722, 15)
        Me.Label148.TabIndex = 8
        Me.Label148.Text = "Searchs through episodes && if it finds a season or episode set at -1, will try &"& _ 
    "& get correct season && episode from title via MC regexes. "
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(212, 76)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(197, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Process Fix"
        Me.Button2.UseVisualStyleBackColor = true
        '
        'GroupBox28
        '
        Me.GroupBox28.Controls.Add(Me.RadioButton_Fix_Title)
        Me.GroupBox28.Controls.Add(Me.RadioButton_Fix_Filename)
        Me.GroupBox28.Location = New System.Drawing.Point(6, 29)
        Me.GroupBox28.Name = "GroupBox28"
        Me.GroupBox28.Size = New System.Drawing.Size(200, 70)
        Me.GroupBox28.TabIndex = 11
        Me.GroupBox28.TabStop = false
        Me.GroupBox28.Text = "Choose source for regex match"
        '
        'RadioButton_Fix_Title
        '
        Me.RadioButton_Fix_Title.AutoSize = true
        Me.RadioButton_Fix_Title.Checked = true
        Me.RadioButton_Fix_Title.Location = New System.Drawing.Point(11, 20)
        Me.RadioButton_Fix_Title.Name = "RadioButton_Fix_Title"
        Me.RadioButton_Fix_Title.Size = New System.Drawing.Size(96, 19)
        Me.RadioButton_Fix_Title.TabIndex = 9
        Me.RadioButton_Fix_Title.TabStop = true
        Me.RadioButton_Fix_Title.Text = "Title from nfo"
        Me.RadioButton_Fix_Title.UseVisualStyleBackColor = true
        '
        'RadioButton_Fix_Filename
        '
        Me.RadioButton_Fix_Filename.AutoSize = true
        Me.RadioButton_Fix_Filename.Location = New System.Drawing.Point(11, 45)
        Me.RadioButton_Fix_Filename.Name = "RadioButton_Fix_Filename"
        Me.RadioButton_Fix_Filename.Size = New System.Drawing.Size(110, 19)
        Me.RadioButton_Fix_Filename.TabIndex = 10
        Me.RadioButton_Fix_Filename.Text = "Filename of nfo"
        Me.RadioButton_Fix_Filename.UseVisualStyleBackColor = true
        '
        'cbClearMissingFolder
        '
        Me.cbClearMissingFolder.AutoSize = true
        Me.TableLayoutPanel24.SetColumnSpan(Me.cbClearMissingFolder, 2)
        Me.cbClearMissingFolder.Location = New System.Drawing.Point(43, 180)
        Me.cbClearMissingFolder.Name = "cbClearMissingFolder"
        Me.cbClearMissingFolder.Size = New System.Drawing.Size(179, 19)
        Me.cbClearMissingFolder.TabIndex = 15
        Me.cbClearMissingFolder.Text = "Clear Missing Folder on Exit"
        Me.cbClearMissingFolder.UseVisualStyleBackColor = true
        '
        'TabConfigXML
        '
        Me.TabConfigXML.Controls.Add(Me.RichTextBoxTabConfigXML)
        Me.TabConfigXML.Location = New System.Drawing.Point(4, 24)
        Me.TabConfigXML.Name = "TabConfigXML"
        Me.TabConfigXML.Size = New System.Drawing.Size(747, 72)
        Me.TabConfigXML.TabIndex = 6
        Me.TabConfigXML.Text = "Config"
        Me.TabConfigXML.UseVisualStyleBackColor = true
        '
        'RichTextBoxTabConfigXML
        '
        Me.RichTextBoxTabConfigXML.BackColor = System.Drawing.Color.White
        Me.RichTextBoxTabConfigXML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxTabConfigXML.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RichTextBoxTabConfigXML.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxTabConfigXML.Name = "RichTextBoxTabConfigXML"
        Me.RichTextBoxTabConfigXML.ReadOnly = true
        Me.RichTextBoxTabConfigXML.Size = New System.Drawing.Size(747, 72)
        Me.RichTextBoxTabConfigXML.TabIndex = 0
        Me.RichTextBoxTabConfigXML.Text = ""
        '
        'TabMovieCacheXML
        '
        Me.TabMovieCacheXML.Controls.Add(Me.RichTextBoxTabMovieCache)
        Me.TabMovieCacheXML.Location = New System.Drawing.Point(4, 24)
        Me.TabMovieCacheXML.Name = "TabMovieCacheXML"
        Me.TabMovieCacheXML.Size = New System.Drawing.Size(747, 72)
        Me.TabMovieCacheXML.TabIndex = 7
        Me.TabMovieCacheXML.Text = "MovieCache"
        Me.TabMovieCacheXML.UseVisualStyleBackColor = true
        '
        'RichTextBoxTabMovieCache
        '
        Me.RichTextBoxTabMovieCache.BackColor = System.Drawing.Color.White
        Me.RichTextBoxTabMovieCache.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxTabMovieCache.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RichTextBoxTabMovieCache.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxTabMovieCache.Name = "RichTextBoxTabMovieCache"
        Me.RichTextBoxTabMovieCache.ReadOnly = true
        Me.RichTextBoxTabMovieCache.Size = New System.Drawing.Size(747, 72)
        Me.RichTextBoxTabMovieCache.TabIndex = 0
        Me.RichTextBoxTabMovieCache.Text = ""
        '
        'TabTVCacheXML
        '
        Me.TabTVCacheXML.Controls.Add(Me.RichTextBoxTabTVCache)
        Me.TabTVCacheXML.Location = New System.Drawing.Point(4, 24)
        Me.TabTVCacheXML.Name = "TabTVCacheXML"
        Me.TabTVCacheXML.Size = New System.Drawing.Size(747, 72)
        Me.TabTVCacheXML.TabIndex = 8
        Me.TabTVCacheXML.Text = "TVCache"
        Me.TabTVCacheXML.UseVisualStyleBackColor = true
        '
        'RichTextBoxTabTVCache
        '
        Me.RichTextBoxTabTVCache.BackColor = System.Drawing.Color.White
        Me.RichTextBoxTabTVCache.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxTabTVCache.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RichTextBoxTabTVCache.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxTabTVCache.Name = "RichTextBoxTabTVCache"
        Me.RichTextBoxTabTVCache.ReadOnly = true
        Me.RichTextBoxTabTVCache.Size = New System.Drawing.Size(747, 72)
        Me.RichTextBoxTabTVCache.TabIndex = 0
        Me.RichTextBoxTabTVCache.Text = ""
        '
        'TabProfile
        '
        Me.TabProfile.Controls.Add(Me.RichTextBoxTabProfile)
        Me.TabProfile.Location = New System.Drawing.Point(4, 24)
        Me.TabProfile.Name = "TabProfile"
        Me.TabProfile.Size = New System.Drawing.Size(747, 72)
        Me.TabProfile.TabIndex = 9
        Me.TabProfile.Text = "Profile"
        Me.TabProfile.UseVisualStyleBackColor = true
        '
        'RichTextBoxTabProfile
        '
        Me.RichTextBoxTabProfile.BackColor = System.Drawing.Color.White
        Me.RichTextBoxTabProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxTabProfile.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RichTextBoxTabProfile.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxTabProfile.Name = "RichTextBoxTabProfile"
        Me.RichTextBoxTabProfile.ReadOnly = true
        Me.RichTextBoxTabProfile.Size = New System.Drawing.Size(747, 72)
        Me.RichTextBoxTabProfile.TabIndex = 0
        Me.RichTextBoxTabProfile.Text = ""
        '
        'TabActorCache
        '
        Me.TabActorCache.Controls.Add(Me.RichTextBoxTabActorCache)
        Me.TabActorCache.Location = New System.Drawing.Point(4, 24)
        Me.TabActorCache.Name = "TabActorCache"
        Me.TabActorCache.Size = New System.Drawing.Size(747, 72)
        Me.TabActorCache.TabIndex = 10
        Me.TabActorCache.Text = "ActorCache"
        Me.TabActorCache.UseVisualStyleBackColor = true
        '
        'RichTextBoxTabActorCache
        '
        Me.RichTextBoxTabActorCache.BackColor = System.Drawing.Color.White
        Me.RichTextBoxTabActorCache.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxTabActorCache.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RichTextBoxTabActorCache.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxTabActorCache.Name = "RichTextBoxTabActorCache"
        Me.RichTextBoxTabActorCache.ReadOnly = true
        Me.RichTextBoxTabActorCache.Size = New System.Drawing.Size(747, 72)
        Me.RichTextBoxTabActorCache.TabIndex = 0
        Me.RichTextBoxTabActorCache.Text = ""
        '
        'TabRegex
        '
        Me.TabRegex.Controls.Add(Me.RichTextBoxTabRegex)
        Me.TabRegex.Location = New System.Drawing.Point(4, 24)
        Me.TabRegex.Name = "TabRegex"
        Me.TabRegex.Size = New System.Drawing.Size(747, 72)
        Me.TabRegex.TabIndex = 11
        Me.TabRegex.Text = "Regex"
        Me.TabRegex.UseVisualStyleBackColor = true
        '
        'RichTextBoxTabRegex
        '
        Me.RichTextBoxTabRegex.BackColor = System.Drawing.Color.White
        Me.RichTextBoxTabRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxTabRegex.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RichTextBoxTabRegex.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxTabRegex.Name = "RichTextBoxTabRegex"
        Me.RichTextBoxTabRegex.ReadOnly = true
        Me.RichTextBoxTabRegex.Size = New System.Drawing.Size(747, 72)
        Me.RichTextBoxTabRegex.TabIndex = 0
        Me.RichTextBoxTabRegex.Text = ""
        '
        'TabTasks
        '
        Me.TabTasks.Controls.Add(Me.TasksDontShowCompleted)
        Me.TabTasks.Controls.Add(Me.TasksTest)
        Me.TabTasks.Controls.Add(Me.TasksClearCompleted)
        Me.TabTasks.Controls.Add(Me.TasksRefresh)
        Me.TabTasks.Controls.Add(Me.TasksDependancies)
        Me.TabTasks.Controls.Add(Me.TasksStateLabel)
        Me.TabTasks.Controls.Add(Me.TasksSelectedMessage)
        Me.TabTasks.Controls.Add(Me.TasksMessages)
        Me.TabTasks.Controls.Add(Me.TasksArgumentSelector)
        Me.TabTasks.Controls.Add(Me.TasksArgumentDisplay)
        Me.TabTasks.Controls.Add(Me.TasksList)
        Me.TabTasks.Location = New System.Drawing.Point(4, 24)
        Me.TabTasks.Name = "TabTasks"
        Me.TabTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.TabTasks.Size = New System.Drawing.Size(747, 72)
        Me.TabTasks.TabIndex = 12
        Me.TabTasks.Text = "Tasks"
        Me.TabTasks.UseVisualStyleBackColor = true
        '
        'TasksDontShowCompleted
        '
        Me.TasksDontShowCompleted.AutoSize = true
        Me.TasksDontShowCompleted.Checked = true
        Me.TasksDontShowCompleted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TasksDontShowCompleted.Location = New System.Drawing.Point(9, 389)
        Me.TasksDontShowCompleted.Name = "TasksDontShowCompleted"
        Me.TasksDontShowCompleted.Size = New System.Drawing.Size(179, 19)
        Me.TasksDontShowCompleted.TabIndex = 10
        Me.TasksDontShowCompleted.Text = "Don't show completed tasks"
        Me.TasksDontShowCompleted.UseVisualStyleBackColor = true
        '
        'TasksTest
        '
        Me.TasksTest.Location = New System.Drawing.Point(242, 416)
        Me.TasksTest.Name = "TasksTest"
        Me.TasksTest.Size = New System.Drawing.Size(75, 23)
        Me.TasksTest.TabIndex = 9
        Me.TasksTest.Text = "Add Tests"
        Me.TasksTest.UseVisualStyleBackColor = true
        '
        'TasksClearCompleted
        '
        Me.TasksClearCompleted.Location = New System.Drawing.Point(90, 417)
        Me.TasksClearCompleted.Name = "TasksClearCompleted"
        Me.TasksClearCompleted.Size = New System.Drawing.Size(146, 23)
        Me.TasksClearCompleted.TabIndex = 8
        Me.TasksClearCompleted.Text = "Clear Completed"
        Me.TasksClearCompleted.UseVisualStyleBackColor = true
        '
        'TasksRefresh
        '
        Me.TasksRefresh.Location = New System.Drawing.Point(8, 417)
        Me.TasksRefresh.Name = "TasksRefresh"
        Me.TasksRefresh.Size = New System.Drawing.Size(75, 23)
        Me.TasksRefresh.TabIndex = 7
        Me.TasksRefresh.Text = "Refresh"
        Me.TasksRefresh.UseVisualStyleBackColor = true
        '
        'TasksDependancies
        '
        Me.TasksDependancies.FormattingEnabled = true
        Me.TasksDependancies.ItemHeight = 15
        Me.TasksDependancies.Location = New System.Drawing.Point(324, 33)
        Me.TasksDependancies.Name = "TasksDependancies"
        Me.TasksDependancies.Size = New System.Drawing.Size(120, 334)
        Me.TasksDependancies.TabIndex = 6
        '
        'TasksStateLabel
        '
        Me.TasksStateLabel.AutoSize = true
        Me.TasksStateLabel.Location = New System.Drawing.Point(325, 7)
        Me.TasksStateLabel.Name = "TasksStateLabel"
        Me.TasksStateLabel.Size = New System.Drawing.Size(52, 15)
        Me.TasksStateLabel.TabIndex = 5
        Me.TasksStateLabel.Text = "Label78"
        '
        'TasksSelectedMessage
        '
        Me.TasksSelectedMessage.Location = New System.Drawing.Point(807, 107)
        Me.TasksSelectedMessage.Multiline = true
        Me.TasksSelectedMessage.Name = "TasksSelectedMessage"
        Me.TasksSelectedMessage.Size = New System.Drawing.Size(220, 192)
        Me.TasksSelectedMessage.TabIndex = 4
        '
        'TasksMessages
        '
        Me.TasksMessages.FormattingEnabled = true
        Me.TasksMessages.ItemHeight = 15
        Me.TasksMessages.Location = New System.Drawing.Point(807, 7)
        Me.TasksMessages.Name = "TasksMessages"
        Me.TasksMessages.Size = New System.Drawing.Size(220, 94)
        Me.TasksMessages.TabIndex = 3
        '
        'TasksArgumentSelector
        '
        Me.TasksArgumentSelector.FormattingEnabled = true
        Me.TasksArgumentSelector.Location = New System.Drawing.Point(567, 6)
        Me.TasksArgumentSelector.Name = "TasksArgumentSelector"
        Me.TasksArgumentSelector.Size = New System.Drawing.Size(121, 23)
        Me.TasksArgumentSelector.TabIndex = 2
        '
        'TasksArgumentDisplay
        '
        Me.TasksArgumentDisplay.Location = New System.Drawing.Point(567, 33)
        Me.TasksArgumentDisplay.Name = "TasksArgumentDisplay"
        Me.TasksArgumentDisplay.Size = New System.Drawing.Size(200, 100)
        Me.TasksArgumentDisplay.TabIndex = 1
        '
        'TasksList
        '
        Me.TasksList.FormattingEnabled = true
        Me.TasksList.ItemHeight = 15
        Me.TasksList.Location = New System.Drawing.Point(3, 3)
        Me.TasksList.Name = "TasksList"
        Me.TasksList.Size = New System.Drawing.Size(315, 364)
        Me.TasksList.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoviesToolStripMenuItem, Me.TVShowsToolStripMenuItem, Me.HToolStripMenuItem, Me.ProfilesToolStripMenuItem, Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ExportToXBMCToolStripMenuItem, Me.HelpToolStripMenuItem, Me.PreferencesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1069, 24)
        Me.MenuStrip1.TabIndex = 53
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MoviesToolStripMenuItem
        '
        Me.MoviesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchForNewMoviesToolStripMenuItem, Me.BatchRescraperToolStripMenuItem, Me.ToolStripSeparator8, Me.ToolStripMenuItemRebuildMovieCaches, Me.RefreshMovieNfoFilesToolStripMenuItem, Me.ToolStripSeparator20, Me.tsmiMovieSetIdCheck, Me.ToolStripSeparator9, Me.ReloadMovieCacheToolStripMenuItem, Me.ToolStripSeparator10, Me.ExportMovieListInfoToolStripMenuItem, Me.ToolStripSeparator12, Me.DownsizeAllFanartsToSelectedSizeToolStripMenuItem, Me.DownsizeAllPostersToSelectedSizeToolStripMenuItem})
        Me.MoviesToolStripMenuItem.Name = "MoviesToolStripMenuItem"
        Me.MoviesToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.MoviesToolStripMenuItem.Tag = "M"
        Me.MoviesToolStripMenuItem.Text = "Movies"
        '
        'SearchForNewMoviesToolStripMenuItem
        '
        Me.SearchForNewMoviesToolStripMenuItem.Name = "SearchForNewMoviesToolStripMenuItem"
        Me.SearchForNewMoviesToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.SearchForNewMoviesToolStripMenuItem.Text = "Search for New Movies"
        '
        'BatchRescraperToolStripMenuItem
        '
        Me.BatchRescraperToolStripMenuItem.Name = "BatchRescraperToolStripMenuItem"
        Me.BatchRescraperToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.BatchRescraperToolStripMenuItem.Text = "Batch Rescraper Wizard"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(269, 6)
        '
        'ToolStripMenuItemRebuildMovieCaches
        '
        Me.ToolStripMenuItemRebuildMovieCaches.Name = "ToolStripMenuItemRebuildMovieCaches"
        Me.ToolStripMenuItemRebuildMovieCaches.Size = New System.Drawing.Size(272, 22)
        Me.ToolStripMenuItemRebuildMovieCaches.Text = "Refresh All Movies"
        '
        'RefreshMovieNfoFilesToolStripMenuItem
        '
        Me.RefreshMovieNfoFilesToolStripMenuItem.Name = "RefreshMovieNfoFilesToolStripMenuItem"
        Me.RefreshMovieNfoFilesToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.RefreshMovieNfoFilesToolStripMenuItem.Text = "Rebuild Movie nfo files"
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(269, 6)
        '
        'tsmiMovieSetIdCheck
        '
        Me.tsmiMovieSetIdCheck.Name = "tsmiMovieSetIdCheck"
        Me.tsmiMovieSetIdCheck.Size = New System.Drawing.Size(272, 22)
        Me.tsmiMovieSetIdCheck.Text = "Movie Set ID Check"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(269, 6)
        '
        'ReloadMovieCacheToolStripMenuItem
        '
        Me.ReloadMovieCacheToolStripMenuItem.Name = "ReloadMovieCacheToolStripMenuItem"
        Me.ReloadMovieCacheToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ReloadMovieCacheToolStripMenuItem.Text = "Reload Movie From Cache"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(269, 6)
        '
        'ExportMovieListInfoToolStripMenuItem
        '
        Me.ExportMovieListInfoToolStripMenuItem.Name = "ExportMovieListInfoToolStripMenuItem"
        Me.ExportMovieListInfoToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ExportMovieListInfoToolStripMenuItem.Text = "Export Movie List Info"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(269, 6)
        '
        'DownsizeAllFanartsToSelectedSizeToolStripMenuItem
        '
        Me.DownsizeAllFanartsToSelectedSizeToolStripMenuItem.Name = "DownsizeAllFanartsToSelectedSizeToolStripMenuItem"
        Me.DownsizeAllFanartsToSelectedSizeToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.DownsizeAllFanartsToSelectedSizeToolStripMenuItem.Text = "Downsize all fanarts to selected size"
        '
        'DownsizeAllPostersToSelectedSizeToolStripMenuItem
        '
        Me.DownsizeAllPostersToSelectedSizeToolStripMenuItem.Name = "DownsizeAllPostersToSelectedSizeToolStripMenuItem"
        Me.DownsizeAllPostersToSelectedSizeToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.DownsizeAllPostersToSelectedSizeToolStripMenuItem.Text = "Downsize all posters to selected size"
        '
        'TVShowsToolStripMenuItem
        '
        Me.TVShowsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchForNewEpisodesToolStripMenuItem, Me.TVShowBrowserToolStripMenuItem, Me.CheckRootsForToolStripMenuItem, Me.TV_BatchRescrapeWizardToolStripMenuItem, Me.ToolStripSeparator13, Me.RefreshShowsToolStripMenuItem, Me.Tv_tsmi_CheckDuplicateEpisodes, Me.ToolStripSeparator15, Me.ReloadShowCacheToolStripMenuItem, Me.ToolStripSeparator16, Me.ExportTVShowInfoToolStripMenuItem, Me.ToolStripSeparator14, Me.SearchForMissingEpisodesToolStripMenuItem, Me.RefreshMissingEpisodesToolStripMenuItem})
        Me.TVShowsToolStripMenuItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TVShowsToolStripMenuItem.Name = "TVShowsToolStripMenuItem"
        Me.TVShowsToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.TVShowsToolStripMenuItem.Text = "TV Shows"
        '
        'SearchForNewEpisodesToolStripMenuItem
        '
        Me.SearchForNewEpisodesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchALLForNewEpisodesToolStripMenuItem})
        Me.SearchForNewEpisodesToolStripMenuItem.Name = "SearchForNewEpisodesToolStripMenuItem"
        Me.SearchForNewEpisodesToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.SearchForNewEpisodesToolStripMenuItem.Text = "Search for New Episodes"
        '
        'SearchALLForNewEpisodesToolStripMenuItem
        '
        Me.SearchALLForNewEpisodesToolStripMenuItem.Name = "SearchALLForNewEpisodesToolStripMenuItem"
        Me.SearchALLForNewEpisodesToolStripMenuItem.Size = New System.Drawing.Size(236, 22)
        Me.SearchALLForNewEpisodesToolStripMenuItem.Text = "Search ALL for New Episodes"
        Me.SearchALLForNewEpisodesToolStripMenuItem.ToolTipText = "Includes Locked Shows"
        '
        'TVShowBrowserToolStripMenuItem
        '
        Me.TVShowBrowserToolStripMenuItem.Enabled = false
        Me.TVShowBrowserToolStripMenuItem.Name = "TVShowBrowserToolStripMenuItem"
        Me.TVShowBrowserToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.TVShowBrowserToolStripMenuItem.Text = "TV Show Browser"
        Me.TVShowBrowserToolStripMenuItem.Visible = false
        '
        'CheckRootsForToolStripMenuItem
        '
        Me.CheckRootsForToolStripMenuItem.Name = "CheckRootsForToolStripMenuItem"
        Me.CheckRootsForToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.CheckRootsForToolStripMenuItem.Text = "Check Roots for New TV Shows"
        '
        'TV_BatchRescrapeWizardToolStripMenuItem
        '
        Me.TV_BatchRescrapeWizardToolStripMenuItem.Name = "TV_BatchRescrapeWizardToolStripMenuItem"
        Me.TV_BatchRescrapeWizardToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.TV_BatchRescrapeWizardToolStripMenuItem.Text = "Batch Rescraper Wizard"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(242, 6)
        '
        'RefreshShowsToolStripMenuItem
        '
        Me.RefreshShowsToolStripMenuItem.Name = "RefreshShowsToolStripMenuItem"
        Me.RefreshShowsToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.RefreshShowsToolStripMenuItem.Text = "Refresh All TV Shows"
        '
        'Tv_tsmi_CheckDuplicateEpisodes
        '
        Me.Tv_tsmi_CheckDuplicateEpisodes.Name = "Tv_tsmi_CheckDuplicateEpisodes"
        Me.Tv_tsmi_CheckDuplicateEpisodes.Size = New System.Drawing.Size(245, 22)
        Me.Tv_tsmi_CheckDuplicateEpisodes.Text = "Check for Duplicate Episodes"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(242, 6)
        '
        'ReloadShowCacheToolStripMenuItem
        '
        Me.ReloadShowCacheToolStripMenuItem.Name = "ReloadShowCacheToolStripMenuItem"
        Me.ReloadShowCacheToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.ReloadShowCacheToolStripMenuItem.Text = "Reload TV Show Cache"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(242, 6)
        '
        'ExportTVShowInfoToolStripMenuItem
        '
        Me.ExportTVShowInfoToolStripMenuItem.Name = "ExportTVShowInfoToolStripMenuItem"
        Me.ExportTVShowInfoToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.ExportTVShowInfoToolStripMenuItem.Text = "Export TV Show Info"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(242, 6)
        '
        'SearchForMissingEpisodesToolStripMenuItem
        '
        Me.SearchForMissingEpisodesToolStripMenuItem.CheckOnClick = true
        Me.SearchForMissingEpisodesToolStripMenuItem.Name = "SearchForMissingEpisodesToolStripMenuItem"
        Me.SearchForMissingEpisodesToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.SearchForMissingEpisodesToolStripMenuItem.Text = "Display Missing Episodes"
        '
        'RefreshMissingEpisodesToolStripMenuItem
        '
        Me.RefreshMissingEpisodesToolStripMenuItem.Name = "RefreshMissingEpisodesToolStripMenuItem"
        Me.RefreshMissingEpisodesToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.RefreshMissingEpisodesToolStripMenuItem.Text = "Refresh Missing Episodes"
        '
        'HToolStripMenuItem
        '
        Me.HToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchForNewHomeMoviesToolStripMenuItem, Me.RebuildHomeMovieCacheToolStripMenuItem})
        Me.HToolStripMenuItem.Name = "HToolStripMenuItem"
        Me.HToolStripMenuItem.Size = New System.Drawing.Size(91, 20)
        Me.HToolStripMenuItem.Text = "Home Movies"
        '
        'SearchForNewHomeMoviesToolStripMenuItem
        '
        Me.SearchForNewHomeMoviesToolStripMenuItem.Name = "SearchForNewHomeMoviesToolStripMenuItem"
        Me.SearchForNewHomeMoviesToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.SearchForNewHomeMoviesToolStripMenuItem.Text = "Search for New Home Movies"
        '
        'RebuildHomeMovieCacheToolStripMenuItem
        '
        Me.RebuildHomeMovieCacheToolStripMenuItem.Name = "RebuildHomeMovieCacheToolStripMenuItem"
        Me.RebuildHomeMovieCacheToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.RebuildHomeMovieCacheToolStripMenuItem.Text = "Rebuild Home Movie Cache"
        '
        'ProfilesToolStripMenuItem
        '
        Me.ProfilesToolStripMenuItem.Name = "ProfilesToolStripMenuItem"
        Me.ProfilesToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.ProfilesToolStripMenuItem.Text = "Profiles"
        Me.ProfilesToolStripMenuItem.Visible = false
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Enabled = false
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(36, 20)
        Me.FileToolStripMenuItem.Text = "File"
        Me.FileToolStripMenuItem.Visible = false
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Enabled = false
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        Me.EditToolStripMenuItem.Visible = false
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReloadHtmlTemplatesToolStripMenuItem, Me.FixNFOCreateDateToolStripMenuItem, Me.tsmicacheclean, Me.RefreshGenreListboxToolStripMenuItem, Me.ExportLibraryToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        Me.ToolsToolStripMenuItem.ToolTipText = "You can add commands here"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"to run external programs. "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Goto General Preferences -"& _ 
    " Custom Commands"
        '
        'ReloadHtmlTemplatesToolStripMenuItem
        '
        Me.ReloadHtmlTemplatesToolStripMenuItem.Name = "ReloadHtmlTemplatesToolStripMenuItem"
        Me.ReloadHtmlTemplatesToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.ReloadHtmlTemplatesToolStripMenuItem.Text = "Reload Html Templates"
        '
        'FixNFOCreateDateToolStripMenuItem
        '
        Me.FixNFOCreateDateToolStripMenuItem.Name = "FixNFOCreateDateToolStripMenuItem"
        Me.FixNFOCreateDateToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.FixNFOCreateDateToolStripMenuItem.Text = "Fix NFO Create Date"
        '
        'tsmicacheclean
        '
        Me.tsmicacheclean.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiCleanCacheOnly, Me.tsmiCleanSeriesonly})
        Me.tsmicacheclean.Name = "tsmicacheclean"
        Me.tsmicacheclean.Size = New System.Drawing.Size(273, 22)
        Me.tsmicacheclean.Text = "Empty Caches && Series Folders"
        '
        'tsmiCleanCacheOnly
        '
        Me.tsmiCleanCacheOnly.Name = "tsmiCleanCacheOnly"
        Me.tsmiCleanCacheOnly.Size = New System.Drawing.Size(168, 22)
        Me.tsmiCleanCacheOnly.Text = "Clean Cache Only"
        '
        'tsmiCleanSeriesonly
        '
        Me.tsmiCleanSeriesonly.Name = "tsmiCleanSeriesonly"
        Me.tsmiCleanSeriesonly.Size = New System.Drawing.Size(168, 22)
        Me.tsmiCleanSeriesonly.Text = "Clean Series Only"
        '
        'RefreshGenreListboxToolStripMenuItem
        '
        Me.RefreshGenreListboxToolStripMenuItem.Name = "RefreshGenreListboxToolStripMenuItem"
        Me.RefreshGenreListboxToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.RefreshGenreListboxToolStripMenuItem.Text = "Refresh Genre Listbox"
        '
        'ExportLibraryToolStripMenuItem
        '
        Me.ExportLibraryToolStripMenuItem.Name = "ExportLibraryToolStripMenuItem"
        Me.ExportLibraryToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.ExportLibraryToolStripMenuItem.Text = "Export Library to Kodi Import format"
        '
        'ExportToXBMCToolStripMenuItem
        '
        Me.ExportToXBMCToolStripMenuItem.Name = "ExportToXBMCToolStripMenuItem"
        Me.ExportToXBMCToolStripMenuItem.Size = New System.Drawing.Size(108, 20)
        Me.ExportToXBMCToolStripMenuItem.Text = "Export To XBMC"
        Me.ExportToXBMCToolStripMenuItem.Visible = false
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MediaCompanionHelpFileToolStripMenuItem, Me.MediaCompanionCodeplexSiteToolStripMenuItem, Me.XBMCMCThreadToolStripMenuItem, Me.MediaCompanionForumToolStripMenuItem, Me.tsmiCheckForNewVersion, Me.TSMI_AboutMC})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'MediaCompanionHelpFileToolStripMenuItem
        '
        Me.MediaCompanionHelpFileToolStripMenuItem.Name = "MediaCompanionHelpFileToolStripMenuItem"
        Me.MediaCompanionHelpFileToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.MediaCompanionHelpFileToolStripMenuItem.Text = "Media Companion Help File"
        '
        'MediaCompanionCodeplexSiteToolStripMenuItem
        '
        Me.MediaCompanionCodeplexSiteToolStripMenuItem.Name = "MediaCompanionCodeplexSiteToolStripMenuItem"
        Me.MediaCompanionCodeplexSiteToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.MediaCompanionCodeplexSiteToolStripMenuItem.Text = "Media Companion Codeplex Site"
        '
        'XBMCMCThreadToolStripMenuItem
        '
        Me.XBMCMCThreadToolStripMenuItem.Name = "XBMCMCThreadToolStripMenuItem"
        Me.XBMCMCThreadToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.XBMCMCThreadToolStripMenuItem.Text = "Kodi MC Thread"
        '
        'MediaCompanionForumToolStripMenuItem
        '
        Me.MediaCompanionForumToolStripMenuItem.Name = "MediaCompanionForumToolStripMenuItem"
        Me.MediaCompanionForumToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.MediaCompanionForumToolStripMenuItem.Text = "Media Companion Forum"
        Me.MediaCompanionForumToolStripMenuItem.Visible = false
        '
        'tsmiCheckForNewVersion
        '
        Me.tsmiCheckForNewVersion.Name = "tsmiCheckForNewVersion"
        Me.tsmiCheckForNewVersion.Size = New System.Drawing.Size(248, 22)
        Me.tsmiCheckForNewVersion.Text = "Check for new version"
        '
        'TSMI_AboutMC
        '
        Me.TSMI_AboutMC.Name = "TSMI_AboutMC"
        Me.TSMI_AboutMC.Size = New System.Drawing.Size(248, 22)
        Me.TSMI_AboutMC.Text = "About Media Companion"
        '
        'PreferencesToolStripMenuItem
        '
        Me.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem"
        Me.PreferencesToolStripMenuItem.Size = New System.Drawing.Size(84, 20)
        Me.PreferencesToolStripMenuItem.Text = "Preferences"
        '
        'openFD
        '
        Me.openFD.FileName = "OpenFileDialog1"
        '
        'Timer2
        '
        Me.Timer2.Interval = 150
        '
        'Timer4
        '
        Me.Timer4.Interval = 75
        '
        'MovieWallContextMenu
        '
        Me.MovieWallContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlayMovieToolStripMenuItem, Me.ToolStripSeparator3, Me.EditMovieToolStripMenuItem1, Me.DToolStripMenuItem, Me.OpenFolderToolStripMenuItem1, Me.tsmiWallPlayTrailer})
        Me.MovieWallContextMenu.Name = "ContextMenuStrip3"
        Me.MovieWallContextMenu.Size = New System.Drawing.Size(177, 120)
        '
        'PlayMovieToolStripMenuItem
        '
        Me.PlayMovieToolStripMenuItem.Name = "PlayMovieToolStripMenuItem"
        Me.PlayMovieToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.PlayMovieToolStripMenuItem.Text = "Play Movie"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(173, 6)
        '
        'EditMovieToolStripMenuItem1
        '
        Me.EditMovieToolStripMenuItem1.Name = "EditMovieToolStripMenuItem1"
        Me.EditMovieToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.EditMovieToolStripMenuItem1.Text = "Change Movie Poster"
        '
        'DToolStripMenuItem
        '
        Me.DToolStripMenuItem.Name = "DToolStripMenuItem"
        Me.DToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.DToolStripMenuItem.Text = "Large Image View"
        '
        'OpenFolderToolStripMenuItem1
        '
        Me.OpenFolderToolStripMenuItem1.Name = "OpenFolderToolStripMenuItem1"
        Me.OpenFolderToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.OpenFolderToolStripMenuItem1.Text = "Open Folder"
        '
        'tsmiWallPlayTrailer
        '
        Me.tsmiWallPlayTrailer.Name = "tsmiWallPlayTrailer"
        Me.tsmiWallPlayTrailer.Size = New System.Drawing.Size(176, 22)
        Me.tsmiWallPlayTrailer.Text = "Play Trailer"
        '
        'ListBox8
        '
        Me.ListBox8.FormattingEnabled = true
        Me.ListBox8.Location = New System.Drawing.Point(174, 11)
        Me.ListBox8.Name = "ListBox8"
        Me.ListBox8.Size = New System.Drawing.Size(399, 290)
        Me.ListBox8.TabIndex = 0
        '
        'bckgrnd_tvshowscraper
        '
        Me.bckgrnd_tvshowscraper.WorkerReportsProgress = true
        '
        'FontDialog1
        '
        Me.FontDialog1.Color = System.Drawing.SystemColors.ControlText
        '
        'Bckgrndfindmissingepisodes
        '
        Me.Bckgrndfindmissingepisodes.WorkerReportsProgress = true
        Me.Bckgrndfindmissingepisodes.WorkerSupportsCancellation = true
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "media_companion.chm"
        '
        'tvbckrescrapewizard
        '
        Me.tvbckrescrapewizard.WorkerReportsProgress = true
        Me.tvbckrescrapewizard.WorkerSupportsCancellation = true
        '
        'ForegroundWorkTimer
        '
        Me.ForegroundWorkTimer.Enabled = true
        Me.ForegroundWorkTimer.Interval = 500
        '
        'TimerToolTip
        '
        Me.TimerToolTip.Interval = 2000
        '
        'ScraperStatusStrip
        '
        Me.ScraperStatusStrip.BackColor = System.Drawing.SystemColors.Control
        Me.ScraperStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsMultiMovieProgressBar, Me.tsLabelEscCancel, Me.tsStatusLabel})
        Me.ScraperStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ScraperStatusStrip.Location = New System.Drawing.Point(0, 696)
        Me.ScraperStatusStrip.Name = "ScraperStatusStrip"
        Me.ScraperStatusStrip.Size = New System.Drawing.Size(1069, 24)
        Me.ScraperStatusStrip.TabIndex = 180
        Me.ScraperStatusStrip.Text = "StatusStrip1"
        Me.ScraperStatusStrip.Visible = false
        '
        'tsMultiMovieProgressBar
        '
        Me.tsMultiMovieProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsMultiMovieProgressBar.Name = "tsMultiMovieProgressBar"
        Me.tsMultiMovieProgressBar.Size = New System.Drawing.Size(100, 18)
        '
        'tsLabelEscCancel
        '
        Me.tsLabelEscCancel.BackColor = System.Drawing.SystemColors.Control
        Me.tsLabelEscCancel.Name = "tsLabelEscCancel"
        Me.tsLabelEscCancel.Size = New System.Drawing.Size(72, 13)
        Me.tsLabelEscCancel.Text = "ESC to cancel"
        '
        'tsStatusLabel
        '
        Me.tsStatusLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tsStatusLabel.ForeColor = System.Drawing.Color.Green
        Me.tsStatusLabel.Name = "tsStatusLabel"
        Me.tsStatusLabel.Size = New System.Drawing.Size(0, 0)
        '
        'ssFileDownload
        '
        Me.ssFileDownload.BackColor = System.Drawing.SystemColors.Control
        Me.ssFileDownload.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsFileDownloadlabel, Me.tsProgressBarFileDownload})
        Me.ssFileDownload.Location = New System.Drawing.Point(0, 689)
        Me.ssFileDownload.Name = "ssFileDownload"
        Me.ssFileDownload.Size = New System.Drawing.Size(1069, 22)
        Me.ssFileDownload.SizingGrip = false
        Me.ssFileDownload.TabIndex = 181
        Me.ssFileDownload.Visible = false
        '
        'tsFileDownloadlabel
        '
        Me.tsFileDownloadlabel.Name = "tsFileDownloadlabel"
        Me.tsFileDownloadlabel.Size = New System.Drawing.Size(230, 17)
        Me.tsFileDownloadlabel.Text = "Downloading trailer - Press Control-C to cancel"
        '
        'tsProgressBarFileDownload
        '
        Me.tsProgressBarFileDownload.AutoSize = false
        Me.tsProgressBarFileDownload.Name = "tsProgressBarFileDownload"
        Me.tsProgressBarFileDownload.Size = New System.Drawing.Size(100, 16)
        '
        'UcGenPref_XbmcLink1
        '
        Me.UcGenPref_XbmcLink1.Location = New System.Drawing.Point(0, 0)
        Me.UcGenPref_XbmcLink1.Name = "UcGenPref_XbmcLink1"
        Me.UcGenPref_XbmcLink1.Size = New System.Drawing.Size(404, 434)
        Me.UcGenPref_XbmcLink1.TabIndex = 0
        '
        'TVWallContextMenu
        '
        Me.TVWallContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiTvWallPosterChange, Me.tsmiTvWallLargeView, Me.tsmiTvWallOpenFolder})
        Me.TVWallContextMenu.Name = "ContextMenuStrip3"
        Me.TVWallContextMenu.Size = New System.Drawing.Size(161, 70)
        '
        'tsmiTvWallPosterChange
        '
        Me.tsmiTvWallPosterChange.Name = "tsmiTvWallPosterChange"
        Me.tsmiTvWallPosterChange.Size = New System.Drawing.Size(160, 22)
        Me.tsmiTvWallPosterChange.Text = "Change TV Poster"
        '
        'tsmiTvWallLargeView
        '
        Me.tsmiTvWallLargeView.Name = "tsmiTvWallLargeView"
        Me.tsmiTvWallLargeView.Size = New System.Drawing.Size(160, 22)
        Me.tsmiTvWallLargeView.Text = "Large Image View"
        '
        'tsmiTvWallOpenFolder
        '
        Me.tsmiTvWallOpenFolder.Name = "tsmiTvWallOpenFolder"
        Me.tsmiTvWallOpenFolder.Size = New System.Drawing.Size(160, 22)
        Me.tsmiTvWallOpenFolder.Text = "Open Folder"
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 4
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90!))
        Me.TableLayoutPanel23.Controls.Add(Me.WebBrowser2, 0, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.Panel17, 2, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.Panel18, 0, 1)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 2
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(1037, 624)
        Me.TableLayoutPanel23.TabIndex = 1
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.btnMovWebIMDb)
        Me.Panel17.Controls.Add(Me.btnMovWebTMDb)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(341, 587)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(256, 34)
        Me.Panel17.TabIndex = 1
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.btnMovWebStop)
        Me.Panel18.Controls.Add(Me.btnMovWebBack)
        Me.Panel18.Controls.Add(Me.btnMovWebForward)
        Me.Panel18.Controls.Add(Me.btnMovWebRefresh)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Location = New System.Drawing.Point(3, 587)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(284, 34)
        Me.Panel18.TabIndex = 2
        '
        'btnMovWebStop
        '
        Me.btnMovWebStop.BackgroundImage = Global.Media_Companion.My.Resources.Resources.incorrect
        Me.btnMovWebStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWebStop.Location = New System.Drawing.Point(25, -1)
        Me.btnMovWebStop.Name = "btnMovWebStop"
        Me.btnMovWebStop.Size = New System.Drawing.Size(36, 36)
        Me.btnMovWebStop.TabIndex = 7
        Me.btnMovWebStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnMovWebStop.UseVisualStyleBackColor = true
        '
        'btnMovWebBack
        '
        Me.btnMovWebBack.BackgroundImage = Global.Media_Companion.My.Resources.Resources.arrow_roll_Back
        Me.btnMovWebBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWebBack.Location = New System.Drawing.Point(136, -1)
        Me.btnMovWebBack.Name = "btnMovWebBack"
        Me.btnMovWebBack.Size = New System.Drawing.Size(36, 36)
        Me.btnMovWebBack.TabIndex = 6
        Me.btnMovWebBack.UseVisualStyleBackColor = true
        '
        'btnMovWebForward
        '
        Me.btnMovWebForward.BackgroundImage = Global.Media_Companion.My.Resources.Resources.arrow_roll_Forward
        Me.btnMovWebForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWebForward.Location = New System.Drawing.Point(197, -1)
        Me.btnMovWebForward.Name = "btnMovWebForward"
        Me.btnMovWebForward.Size = New System.Drawing.Size(36, 36)
        Me.btnMovWebForward.TabIndex = 5
        Me.btnMovWebForward.UseVisualStyleBackColor = true
        '
        'btnMovWebRefresh
        '
        Me.btnMovWebRefresh.BackgroundImage = Global.Media_Companion.My.Resources.Resources.RefreshAll
        Me.btnMovWebRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWebRefresh.Location = New System.Drawing.Point(80, -1)
        Me.btnMovWebRefresh.Name = "btnMovWebRefresh"
        Me.btnMovWebRefresh.Size = New System.Drawing.Size(36, 36)
        Me.btnMovWebRefresh.TabIndex = 4
        Me.btnMovWebRefresh.UseVisualStyleBackColor = true
        '
        'btnMovWebTMDb
        '
        Me.btnMovWebTMDb.BackgroundImage = Global.Media_Companion.My.Resources.Resources.TMDB_Icon
        Me.btnMovWebTMDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWebTMDb.Location = New System.Drawing.Point(19, 2)
        Me.btnMovWebTMDb.Name = "btnMovWebTMDb"
        Me.btnMovWebTMDb.Size = New System.Drawing.Size(75, 30)
        Me.btnMovWebTMDb.TabIndex = 0
        Me.btnMovWebTMDb.UseVisualStyleBackColor = true
        '
        'btnMovWebIMDb
        '
        Me.btnMovWebIMDb.BackgroundImage = Global.Media_Companion.My.Resources.Resources.imdb1
        Me.btnMovWebIMDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMovWebIMDb.Location = New System.Drawing.Point(159, 2)
        Me.btnMovWebIMDb.Name = "btnMovWebIMDb"
        Me.btnMovWebIMDb.Size = New System.Drawing.Size(75, 30)
        Me.btnMovWebIMDb.TabIndex = 1
        Me.btnMovWebIMDb.UseVisualStyleBackColor = true
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = true
        Me.AutoSize = true
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1069, 721)
        Me.Controls.Add(Me.TabLevel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ScraperStatusStrip)
        Me.Controls.Add(Me.ssFileDownload)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = true
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.KeyPreview = true
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(1077, 747)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Media Companion"
        Me.TVContextMenu.ResumeLayout(false)
        CType(Me.PbMovieFanArt,System.ComponentModel.ISupportInitialize).EndInit
        Me.MovieArtworkContextMenu.ResumeLayout(false)
        CType(Me.PbMoviePoster,System.ComponentModel.ISupportInitialize).EndInit
        Me.MovieContextMenu.ResumeLayout(false)
        Me.TabPageLevel2MovMainBrowser.ResumeLayout(false)
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.SplitContainer5.Panel1.ResumeLayout(false)
        Me.SplitContainer5.Panel1.PerformLayout
        Me.SplitContainer5.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer5,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer5.ResumeLayout(false)
        CType(Me.DataGridViewMovies,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.cmsConfigureMovieFilters.ResumeLayout(false)
        CType(Me.ftvArtPicBox,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel6.ResumeLayout(false)
        Me.Panel6.PerformLayout
        Me.tlpMovies.ResumeLayout(false)
        Me.tlpMovies.PerformLayout
        Me.TableLayoutPanel3.ResumeLayout(false)
        Me.TableLayoutPanel4.ResumeLayout(false)
        Me.TableLayoutPanel4.PerformLayout
        Me.SplitContainer2.Panel1.ResumeLayout(false)
        Me.SplitContainer2.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer2.ResumeLayout(false)
        Me.TableLayoutPanel2.ResumeLayout(false)
        Me.TableLayoutPanel2.PerformLayout
        CType(Me.PictureBoxActor,System.ComponentModel.ISupportInitialize).EndInit
        Me.tlpMovieButtons.ResumeLayout(false)
        Me.tlpMovieButtons.PerformLayout
        Me.TableLayoutPanel31.ResumeLayout(false)
        Me.TabPageMovieFanart.ResumeLayout(false)
        Me.TableLayoutPanel10.ResumeLayout(false)
        Me.TableLayoutPanel10.PerformLayout
        Me.FanartContextMenu.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBoxFanartExtrathumbs.ResumeLayout(false)
        Me.GroupBoxFanartExtrathumbs.PerformLayout
        Me.TabPageMoviePoster.ResumeLayout(false)
        Me.panelMoviePosterRHS.ResumeLayout(false)
        Me.panelMoviePosterRHS.PerformLayout
        Me.gbMoviePoster.ResumeLayout(false)
        CType(Me.PictureBoxAssignedMoviePoster,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbMoviePosterControls.ResumeLayout(false)
        Me.gbMoviePosterControls.PerformLayout
        Me.panelMoviePosterLHS.ResumeLayout(false)
        Me.panelMoviePosterLHS.PerformLayout
        Me.gbMoviePostersAvailable.ResumeLayout(false)
        Me.gbMoviePosterSelection.ResumeLayout(false)
        Me.TabPage4.ResumeLayout(false)
        Me.TableLayoutPanel26.ResumeLayout(false)
        Me.TableLayoutPanel26.PerformLayout
        Me.TabPage7.ResumeLayout(false)
        Me.TabPage8.ResumeLayout(false)
        Me.TabPage8.PerformLayout
        Me.StatusStrip1.ResumeLayout(false)
        Me.StatusStrip1.PerformLayout
        Me.TabLevel1.ResumeLayout(false)
        Me.TabPage1.ResumeLayout(false)
        Me.TabControl2.ResumeLayout(false)
        Me.tpMoviesTable.ResumeLayout(false)
        Me.TableLayoutPanel29.ResumeLayout(false)
        Me.TableLayoutPanel29.PerformLayout
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.MovieTableContextMenu.ResumeLayout(false)
        CType(Me.mov_TableEditDGV,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpFanartTv.ResumeLayout(false)
        Me.TabPage9.ResumeLayout(false)
        Me.SplitContainer8.Panel1.ResumeLayout(false)
        Me.SplitContainer8.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer8,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer8.ResumeLayout(false)
        Me.TableLayoutPanel11.ResumeLayout(false)
        Me.TableLayoutPanel11.PerformLayout
        CType(Me.dgvmovset,System.ComponentModel.ISupportInitialize).EndInit
        Me.MovSetsContextMenu.ResumeLayout(false)
        Me.TableLayoutPanel14.ResumeLayout(false)
        Me.TableLayoutPanel14.PerformLayout
        Me.GroupBox40.ResumeLayout(false)
        Me.TableLayoutPanel12.ResumeLayout(false)
        Me.TableLayoutPanel12.PerformLayout
        Me.GroupBox39.ResumeLayout(false)
        Me.TableLayoutPanel13.ResumeLayout(false)
        Me.TableLayoutPanel13.PerformLayout
        Me.tpMediaStubs.ResumeLayout(false)
        Me.TabPage25.ResumeLayout(false)
        Me.Panel4.ResumeLayout(false)
        Me.TableLayoutPanel28.ResumeLayout(false)
        Me.TableLayoutPanel28.PerformLayout
        Me.Panel5.ResumeLayout(false)
        Me.SplitContainer7.Panel1.ResumeLayout(false)
        Me.SplitContainer7.Panel2.ResumeLayout(false)
        Me.SplitContainer7.Panel2.PerformLayout
        CType(Me.SplitContainer7,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer7.ResumeLayout(false)
        Me.TableLayoutPanel9.ResumeLayout(false)
        Me.TableLayoutPanel9.PerformLayout
        Me.TableLayoutPanel8.ResumeLayout(false)
        Me.TableLayoutPanel8.PerformLayout
        Me.Panel3.ResumeLayout(false)
        Me.Panel3.PerformLayout
        Me.TabPage2.ResumeLayout(false)
        Me.TabControl3.ResumeLayout(false)
        Me.TabPageLevel2TVMainBrowser.ResumeLayout(false)
        Me.SplitContainer3.Panel1.ResumeLayout(false)
        Me.SplitContainer3.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer3,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer3.ResumeLayout(false)
        Me.SplitContainer10.Panel1.ResumeLayout(false)
        Me.SplitContainer10.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer10,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer10.ResumeLayout(false)
        Me.TableLayoutPanel7.ResumeLayout(false)
        Me.TableLayoutPanel7.PerformLayout
        Me.Panel11.ResumeLayout(false)
        Me.Panel11.PerformLayout
        Me.Panel8.ResumeLayout(false)
        Me.TableLayoutPanel30.ResumeLayout(false)
        Me.TableLayoutPanel30.PerformLayout
        CType(Me.pbEpActorImage,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel9.ResumeLayout(false)
        Me.TableLayoutPanel19.ResumeLayout(false)
        Me.TableLayoutPanel19.PerformLayout
        CType(Me.pbtvfanarttv,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel20.ResumeLayout(false)
        Me.TableLayoutPanel20.PerformLayout
        Me.Panel7.ResumeLayout(false)
        Me.Panel7.PerformLayout
        Me._tv_SplitContainer.Panel1.ResumeLayout(false)
        Me._tv_SplitContainer.Panel2.ResumeLayout(false)
        CType(Me._tv_SplitContainer,System.ComponentModel.ISupportInitialize).EndInit
        Me._tv_SplitContainer.ResumeLayout(false)
        Me.SplitContainer4.Panel1.ResumeLayout(false)
        Me.SplitContainer4.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer4,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer4.ResumeLayout(false)
        CType(Me.tv_PictureBoxLeft,System.ComponentModel.ISupportInitialize).EndInit
        Me.TvEpContextMenuStrip.ResumeLayout(false)
        CType(Me.tv_PictureBoxRight,System.ComponentModel.ISupportInitialize).EndInit
        Me.TvPosterContextMenuStrip.ResumeLayout(false)
        CType(Me.tv_PictureBoxBottom,System.ComponentModel.ISupportInitialize).EndInit
        Me.gpbxActorSource.ResumeLayout(false)
        Me.gpbxActorSource.PerformLayout
        CType(Me.PictureBox6,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpTvScreenshot.ResumeLayout(false)
        Me.TableLayoutPanel6.ResumeLayout(false)
        Me.TableLayoutPanel6.PerformLayout
        CType(Me.PictureBox14,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpTvFanart.ResumeLayout(false)
        Me.TableLayoutPanel18.ResumeLayout(false)
        Me.TableLayoutPanel18.PerformLayout
        Me.GroupBox6.ResumeLayout(false)
        Me.Panel12.ResumeLayout(false)
        Me.Panel12.PerformLayout
        CType(Me.PictureBox10,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpTvPosters.ResumeLayout(false)
        Me.TableLayoutPanel17.ResumeLayout(false)
        Me.TableLayoutPanel17.PerformLayout
        Me.Panel15.ResumeLayout(false)
        CType(Me.PictureBox12,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox23.ResumeLayout(false)
        Me.GroupBox23.PerformLayout
        Me.GroupBox21.ResumeLayout(false)
        Me.GroupBox21.PerformLayout
        Me.tpTvFanartTv.ResumeLayout(false)
        Me.tpTvSelector.ResumeLayout(false)
        Me.TableLayoutPanel16.ResumeLayout(false)
        Me.TableLayoutPanel16.PerformLayout
        Me.Panel10.ResumeLayout(false)
        Me.Panel10.PerformLayout
        Me.GroupBox7.ResumeLayout(false)
        Me.GroupBox7.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox5.ResumeLayout(false)
        Me.GroupBox5.PerformLayout
        CType(Me.PictureBox9,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpTvTable.ResumeLayout(false)
        CType(Me.DataGridView2,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpTvWeb.ResumeLayout(false)
        Me.TableLayoutPanel15.ResumeLayout(false)
        Me.TableLayoutPanel15.PerformLayout
        Me.Panel14.ResumeLayout(false)
        Me.tpTvFolders.ResumeLayout(false)
        Me.SplitContainer9.Panel1.ResumeLayout(false)
        Me.SplitContainer9.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer9,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer9.ResumeLayout(false)
        Me.SplitContainer6.Panel1.ResumeLayout(false)
        Me.SplitContainer6.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer6,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer6.ResumeLayout(false)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.TvRootFolderContextMenu.ResumeLayout(false)
        Me.TableLayoutPanel5.ResumeLayout(false)
        Me.TableLayoutPanel5.PerformLayout
        Me.TableLayoutPanel25.ResumeLayout(false)
        Me.TabMV.ResumeLayout(false)
        Me.TabMV.PerformLayout
        Me.TabPage3.ResumeLayout(false)
        Me.TabControl1.ResumeLayout(false)
        Me.tp_HmMainBrowser.ResumeLayout(false)
        Me.tp_HmMainBrowser.PerformLayout
        Me.TableLayoutPanel21.ResumeLayout(false)
        Me.TableLayoutPanel21.PerformLayout
        CType(Me.pbx_HmPoster,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbx_HmFanart,System.ComponentModel.ISupportInitialize).EndInit
        Me.HomeMovieContextMenu.ResumeLayout(false)
        Me.tp_HmScrnSht.ResumeLayout(false)
        Me.TableLayoutPanel27.ResumeLayout(false)
        Me.TableLayoutPanel27.PerformLayout
        CType(Me.pbx_HmFanartSht,System.ComponentModel.ISupportInitialize).EndInit
        Me.tp_HmPoster.ResumeLayout(false)
        Me.TableLayoutPanel32.ResumeLayout(false)
        Me.TableLayoutPanel32.PerformLayout
        CType(Me.pbx_HmPosterSht,System.ComponentModel.ISupportInitialize).EndInit
        Me.tp_HmFolders.ResumeLayout(false)
        Me.TableLayoutPanel22.ResumeLayout(false)
        Me.TableLayoutPanel22.PerformLayout
        Me.TabCustTv.ResumeLayout(false)
        Me.TabPage34.ResumeLayout(false)
        Me.TabPage34.PerformLayout
        Me.TabControlDebug.ResumeLayout(false)
        Me.TableLayoutPanel24.ResumeLayout(false)
        Me.TableLayoutPanel24.PerformLayout
        Me.GroupBox29.ResumeLayout(false)
        Me.GroupBox29.PerformLayout
        Me.GroupBox28.ResumeLayout(false)
        Me.GroupBox28.PerformLayout
        Me.TabConfigXML.ResumeLayout(false)
        Me.TabMovieCacheXML.ResumeLayout(false)
        Me.TabTVCacheXML.ResumeLayout(false)
        Me.TabProfile.ResumeLayout(false)
        Me.TabActorCache.ResumeLayout(false)
        Me.TabRegex.ResumeLayout(false)
        Me.TabTasks.ResumeLayout(false)
        Me.TabTasks.PerformLayout
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        Me.MovieWallContextMenu.ResumeLayout(false)
        Me.ScraperStatusStrip.ResumeLayout(false)
        Me.ScraperStatusStrip.PerformLayout
        Me.ssFileDownload.ResumeLayout(false)
        Me.ssFileDownload.PerformLayout
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BasicmovienfoBindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BasicmovienfoBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.TVWallContextMenu.ResumeLayout(false)
        Me.TableLayoutPanel23.ResumeLayout(false)
        Me.Panel17.ResumeLayout(false)
        Me.Panel18.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents bckgroundversion As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar2 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents bckgroundscanepisodes As System.ComponentModel.BackgroundWorker
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents bckgroundfanart As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar3 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar4 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents bckgrounddroppedfiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents bckgroundexit As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar5 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents bckepisodethumb As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar6 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents TabLevel1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents MovieContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mov_ToolStripOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mov_ToolStripViewNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mov_ToolStripFanartBrowserAlt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mov_ToolStripPosterBrowserAlt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mov_ToolStripEditMovieAlt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MediaCompanionForumToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents XBMCMCThreadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoviesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemRebuildMovieCaches As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BatchRescraperToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageLevel2MovMainBrowser As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnResetFilters As System.Windows.Forms.Button
    Friend WithEvents LabelCountFilter As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents rbFolder As System.Windows.Forms.RadioButton
    Friend WithEvents rbFileName As System.Windows.Forms.RadioButton
    Friend WithEvents rbTitleAndYear As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txt_titlesearch As System.Windows.Forms.TextBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPageMovieFanart As System.Windows.Forms.TabPage
    Friend WithEvents TabPageMoviePoster As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents btnMovFanartUrlorBrowse As System.Windows.Forms.Button
    Friend WithEvents ButtonFanartSaveHiRes As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents openFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btncroptop As System.Windows.Forms.Button
    Friend WithEvents btncropbottom As System.Windows.Forms.Button
    Friend WithEvents btncropright As System.Windows.Forms.Button
    Friend WithEvents btncropleft As System.Windows.Forms.Button
    Friend WithEvents btnSaveCropped As System.Windows.Forms.Button
    Friend WithEvents btnresetimage As System.Windows.Forms.Button
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents lblMovFanartHeight As System.Windows.Forms.Label
    Friend WithEvents lblMovFanartWidth As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnMovieFanartResizeImage As System.Windows.Forms.Button
    Friend WithEvents ExportMovieListInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblMovPosterPages As System.Windows.Forms.Label
    Friend WithEvents btnMovPosterNext As System.Windows.Forms.Button
    Friend WithEvents btnMovPosterPrev As System.Windows.Forms.Button
    Friend WithEvents lblCurrentLoadedPoster As System.Windows.Forms.Label
    Friend WithEvents panelMoviePosterRHS As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnPosterTabs_SaveImage As System.Windows.Forms.Button
    Friend WithEvents btn_IMPA_posters As System.Windows.Forms.Button
    Friend WithEvents btn_IMDB_posters As System.Windows.Forms.Button
    Friend WithEvents btn_MPDB_posters As System.Windows.Forms.Button
    Friend WithEvents btn_TMDb_posters As System.Windows.Forms.Button
    Friend WithEvents btnMovPosterURLorBrowse As System.Windows.Forms.Button
    Friend WithEvents panelAvailableMoviePosters As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxAssignedMoviePoster As System.Windows.Forms.PictureBox
    Friend WithEvents cbMoviePosterSaveLoRes As System.Windows.Forms.CheckBox
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents btnChangeMovie As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnMoviePosterSaveCroppedImage As System.Windows.Forms.Button
    Friend WithEvents btnMoviePosterResetImage As System.Windows.Forms.Button
    Friend WithEvents tbCurrentMoviePoster As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageLevel2TVMainBrowser As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents tpTvFanart As System.Windows.Forms.TabPage
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents tb_Sh_Ep_Title As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShCert As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShRating As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShImdbId As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShGenre As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShPremiered As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShTvdbId As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents tb_ShStudio As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShRunTime As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents cbTvActor As System.Windows.Forms.ComboBox
    Friend WithEvents tb_ShPlot As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents tb_EpAired As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpCredits As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpDirector As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpRating As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents tbEpRole As System.Windows.Forms.TextBox
    Friend WithEvents cmbxEpActor As System.Windows.Forms.ComboBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents TVContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Tv_TreeViewContext_ViewNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpandAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpandSelectedShowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseSelectedShowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_ReloadFromCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpTvSelector As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents Button30 As System.Windows.Forms.Button
    Friend WithEvents TextBox26 As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents cbTvChgShowDLSeason As System.Windows.Forms.CheckBox
    Friend WithEvents cbTvChgShowDLFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbTvChgShowDLPoster As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton8 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton9 As System.Windows.Forms.RadioButton
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnTvShowSelectorScrape As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton10 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton11 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton12 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton13 As System.Windows.Forms.RadioButton
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton14 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton15 As System.Windows.Forms.RadioButton
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents ReloadMovieCacheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TVShowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshShowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReloadShowCacheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbTvChgShowDLImagesLang As System.Windows.Forms.CheckBox
    Friend WithEvents cbTvChgShowOverwriteImgs As System.Windows.Forms.CheckBox
    Friend WithEvents pbEpActorImage As System.Windows.Forms.PictureBox
    Friend WithEvents Tv_TreeViewContext_OpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tpTvTable As System.Windows.Forms.TabPage
    Friend WithEvents tpTvWeb As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser4 As System.Windows.Forms.WebBrowser
    Friend WithEvents btnTvFanartResize As System.Windows.Forms.Button
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Button35 As System.Windows.Forms.Button
    Friend WithEvents Button36 As System.Windows.Forms.Button
    Friend WithEvents Button37 As System.Windows.Forms.Button
    Friend WithEvents Button38 As System.Windows.Forms.Button
    Friend WithEvents btnTvFanartSaveCropped As System.Windows.Forms.Button
    Friend WithEvents btnTvFanartResetImage As System.Windows.Forms.Button
    Friend WithEvents TextBox28 As System.Windows.Forms.TextBox
    Friend WithEvents btnTvFanartUrl As System.Windows.Forms.Button
    Friend WithEvents btnTvFanartSave As System.Windows.Forms.Button
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tb_EpFilename As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpPath As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Button_Save_TvShow_Episode As System.Windows.Forms.Button
    Friend WithEvents Button44 As System.Windows.Forms.Button
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Button47 As System.Windows.Forms.Button
    Friend WithEvents Button46 As System.Windows.Forms.Button
    Friend WithEvents Button45 As System.Windows.Forms.Button
    Friend WithEvents Tv_TreeViewContext_RenameEp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btn_EpWatched As System.Windows.Forms.Button
    Friend WithEvents tpTvPosters As System.Windows.Forms.TabPage
    Friend WithEvents btnTvPosterTVDBAll As System.Windows.Forms.Button
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents btnTvPosterTVDBSpecific As System.Windows.Forms.Button
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents btnTvPosterNext As System.Windows.Forms.Button
    Friend WithEvents btnTvPosterPrev As System.Windows.Forms.Button
    Friend WithEvents btnTvPosterSaveBig As System.Windows.Forms.Button
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents btnTvPosterSaveSmall As System.Windows.Forms.Button
    Friend WithEvents btnTvPosterIMDB As System.Windows.Forms.Button
    Friend WithEvents btnTvPosterUrlBrowse As System.Windows.Forms.Button
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents TextBox31 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox12 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton17 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton16 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton18 As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox_TotEpisodeCount As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_TotTVShowCount As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents tpTvScreenshot As System.Windows.Forms.TabPage
    Friend WithEvents RefreshMovieNfoFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBox35 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox14 As System.Windows.Forms.PictureBox
    Friend WithEvents tv_EpThumbScreenShot As System.Windows.Forms.Button
    Friend WithEvents tv_EpThumbRescrape As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabPage22 As System.Windows.Forms.TabPage
    Friend WithEvents MovieWallContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PlayMovieToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditMovieToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFolderToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mov_ToolStripReloadFromCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Button_TV_State As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Tv_TreeViewContext_SearchNewEp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents btnMovieSetAdd As System.Windows.Forms.Button
    Friend WithEvents btnMovieSetRemove As System.Windows.Forms.Button
    Friend WithEvents tbMovSetEntry As System.Windows.Forms.TextBox
    Friend WithEvents rbTvListAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbTvMissingThumb As System.Windows.Forms.RadioButton
    Friend WithEvents rbTvMissingPoster As System.Windows.Forms.RadioButton
    Friend WithEvents rbTvMissingFanart As System.Windows.Forms.RadioButton
    Friend WithEvents tpTvFolders As System.Windows.Forms.TabPage
    Friend WithEvents TabPage24 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage25 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage26 As System.Windows.Forms.TabPage
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents btn_TvFoldersRootAdd As System.Windows.Forms.Button
    Friend WithEvents TextBox39 As System.Windows.Forms.TextBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents btn_TvFoldersRootRemove As System.Windows.Forms.Button
    Friend WithEvents btn_TvFoldersRootBrowse As System.Windows.Forms.Button
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents ListBox6 As System.Windows.Forms.ListBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents btn_TvFoldersRemove As System.Windows.Forms.Button
    Friend WithEvents btn_TvFoldersBrowse As System.Windows.Forms.Button
    Friend WithEvents TextBox40 As System.Windows.Forms.TextBox
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents btn_TvFoldersAdd As System.Windows.Forms.Button
    Friend WithEvents ListBox8 As System.Windows.Forms.ListBox
    Friend WithEvents btn_addmoviefolderdialogue As System.Windows.Forms.Button
    Friend WithEvents btn_removemoviefolder As System.Windows.Forms.Button
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents DownsizeAllFanartsToSelectedSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckRootsForToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btn_TvFoldersSave As System.Windows.Forms.Button
    Friend WithEvents btn_TvFoldersAddFromRoot As System.Windows.Forms.Button
    Friend WithEvents btn_TvFoldersUndo As System.Windows.Forms.Button
    Friend WithEvents SearchForNewEpisodesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TVShowBrowserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchForNewMoviesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bckgrnd_tvshowscraper As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProfilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMoviesTable As System.Windows.Forms.TabPage
    Friend WithEvents BasicmovienfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BasicmovienfoBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents btn_movTableSave As System.Windows.Forms.Button
    Friend WithEvents MovieTableContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MarkAllSelectedAsWatchedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MarkAllSelectedAsUnWatchedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents btn_movTableApply As System.Windows.Forms.Button
    Friend WithEvents GoToToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoToSelectedMoviePosterSelectorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoToSelectedMovieFanartSelectorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_movTableMulti As System.Windows.Forms.Label
    Friend WithEvents mov_ToolStripRescrapeSpecific As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MovieArtworkContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RescrapePToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescrapeFanartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadFanartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadPosterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescrapePosterFromTMDBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadPosterFromTMDBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadPosterFromMPDBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadPosterFromIMDBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListBox15 As System.Windows.Forms.ListBox
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Label134 As System.Windows.Forms.Label
    Friend WithEvents Button101 As System.Windows.Forms.Button
    Friend WithEvents Button102 As System.Windows.Forms.Button
    Friend WithEvents Label136 As System.Windows.Forms.Label
    Friend WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer7 As System.Windows.Forms.SplitContainer
    Friend WithEvents Tv_TreeViewContext_FindMissArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ButtonFanartSaveLoRes As System.Windows.Forms.Button
    Friend WithEvents ExportToXBMCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchForMissingEpisodesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Bckgrndfindmissingepisodes As System.ComponentModel.BackgroundWorker
    Friend WithEvents Tv_TreeViewContext_RefreshShow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_ShowMissEps As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rbTvMissingEpisodes As System.Windows.Forms.RadioButton
    Friend WithEvents LockAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnlockAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents TextBox44 As System.Windows.Forms.TextBox
    Friend WithEvents Button107 As System.Windows.Forms.Button
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents Button108 As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents TV_BatchRescrapeWizardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tvbckrescrapewizard As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripStatusLabel8 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar7 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents mov_ToolStripExportMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage34 As System.Windows.Forms.TabPage
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents TextBox45 As System.Windows.Forms.TextBox
    Friend WithEvents Button109 As System.Windows.Forms.Button
    Friend WithEvents ButtonSaveAndQuickRefresh As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TabControlDebug As System.Windows.Forms.TabPage
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents DebugSytemDPITextBox As System.Windows.Forms.TextBox
    Friend WithEvents DebugSplitter5PosLabel As System.Windows.Forms.Label
    Friend WithEvents ExtraDebugEnable As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents MediaCompanionCodeplexSiteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportTVShowInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabConfigXML As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBoxTabConfigXML As System.Windows.Forms.RichTextBox
    Friend WithEvents TabMovieCacheXML As System.Windows.Forms.TabPage
    Friend WithEvents TabTVCacheXML As System.Windows.Forms.TabPage
    Friend WithEvents TabProfile As System.Windows.Forms.TabPage
    Friend WithEvents TabActorCache As System.Windows.Forms.TabPage
    Friend WithEvents TabRegex As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBoxTabMovieCache As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBoxTabTVCache As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBoxTabProfile As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBoxTabActorCache As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBoxTabRegex As System.Windows.Forms.RichTextBox
    Friend WithEvents CheckBoxDebugShowXML As System.Windows.Forms.CheckBox
    Friend WithEvents btnNextMissingFanart As System.Windows.Forms.Button
    Friend WithEvents CheckBoxDebugShowTVDBReturnedXML As System.Windows.Forms.CheckBox
    Friend WithEvents MediaCompanionHelpFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_DispByAiredDate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mov_ToolStripPlayMovie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_RescrapeShowOrEpisode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_WatchedShowOrEpisode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_UnWatchedShowOrEpisode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents YearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox23 As System.Windows.Forms.GroupBox
    Friend WithEvents rbTVbanner As System.Windows.Forms.RadioButton
    Friend WithEvents rbTVposter As System.Windows.Forms.RadioButton
    Friend WithEvents FanartContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SaveSelectedFanartAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabTasks As System.Windows.Forms.TabPage
    Friend WithEvents TasksArgumentDisplay As System.Windows.Forms.Panel
    Friend WithEvents TasksList As System.Windows.Forms.ListBox
    Friend WithEvents ForegroundWorkTimer As System.Windows.Forms.Timer
    Friend WithEvents TasksArgumentSelector As System.Windows.Forms.ComboBox
    Friend WithEvents TasksSelectedMessage As System.Windows.Forms.TextBox
    Friend WithEvents TasksMessages As System.Windows.Forms.ListBox
    Friend WithEvents TasksDependancies As System.Windows.Forms.ListBox
    Friend WithEvents TasksStateLabel As System.Windows.Forms.Label
    Friend WithEvents TasksTest As System.Windows.Forms.Button
    Friend WithEvents TasksClearCompleted As System.Windows.Forms.Button
    Friend WithEvents TasksRefresh As System.Windows.Forms.Button
    Friend WithEvents TasksDontShowCompleted As System.Windows.Forms.CheckBox
    Friend WithEvents rbTvMissingAiredEp As System.Windows.Forms.RadioButton
    Friend WithEvents Tv_TreeViewContext_ShowTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox28 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton_Fix_Title As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Fix_Filename As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox29 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStripSeparator23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mov_ToolStripPlayTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBoxFanartExtrathumbs As System.Windows.Forms.GroupBox
    Friend WithEvents rbMovThumb4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbMovThumb2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbMovThumb3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbMovThumb1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbMovFanart As System.Windows.Forms.RadioButton
    Friend WithEvents Tv_TreeViewContext_Play_Episode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents mov_ToolStripRescrapeAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchALLForNewEpisodesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mov_ToolStripMovieName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents setsTxt As System.Windows.Forms.TextBox
    Friend WithEvents btnreverse As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tp_HmMainBrowser As System.Windows.Forms.TabPage
    Friend WithEvents TextBox20 As System.Windows.Forms.TextBox
    Friend WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents btnHomeMovieSave As System.Windows.Forms.Button
    Friend WithEvents pbx_HmFanart As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox23 As System.Windows.Forms.TextBox
    Friend WithEvents Label173 As System.Windows.Forms.Label
    Friend WithEvents Label172 As System.Windows.Forms.Label
    Friend WithEvents TextBox22 As System.Windows.Forms.TextBox
    Friend WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents Label167 As System.Windows.Forms.Label
    Friend WithEvents HmMovSort As System.Windows.Forms.TextBox
    Friend WithEvents HmMovTitle As System.Windows.Forms.TextBox
    Friend WithEvents ListBox18 As System.Windows.Forms.ListBox
    Friend WithEvents tp_HmFolders As System.Windows.Forms.TabPage
    Friend WithEvents btnHomeFoldersRemove As System.Windows.Forms.Button
    Friend WithEvents btnHomeFolderAdd As System.Windows.Forms.Button
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents ToolStripProgressBar8 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel9 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents HToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchForNewHomeMoviesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Mov_ToolStripRemoveMovie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Mov_ToolStripRenameMovie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RebuildHomeMovieCacheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tp_HmScrnSht As System.Windows.Forms.TabPage
    Friend WithEvents Label170 As System.Windows.Forms.Label
    Friend WithEvents tb_HmFanartTime As System.Windows.Forms.TextBox
    Friend WithEvents btn_HmFanartShot As System.Windows.Forms.Button
    Friend WithEvents pbx_HmFanartSht As System.Windows.Forms.PictureBox
    Friend WithEvents HomeMovieContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PlaceHolderforHomeMovieTitleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator25 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PlayHomeMovieToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewMovies As System.Windows.Forms.DataGridView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents cbSort As System.Windows.Forms.ComboBox
    Friend WithEvents btnMovSearchNew As System.Windows.Forms.Button
    Friend WithEvents TimerToolTip As System.Windows.Forms.Timer
    Friend WithEvents TvTreeview As System.Windows.Forms.TreeView
    Friend WithEvents ScraperStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents tsLabelEscCancel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsMultiMovieProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tsStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ssFileDownload As System.Windows.Forms.StatusStrip
    Friend WithEvents tsFileDownloadlabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsProgressBarFileDownload As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents DownsizeAllPostersToSelectedSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tb_EpPlot As System.Windows.Forms.TextBox
    Friend WithEvents gpbxActorSource As System.Windows.Forms.GroupBox
    Friend WithEvents _tv_SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents tv_PictureBoxBottom As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents tv_PictureBoxLeft As System.Windows.Forms.PictureBox
    Friend WithEvents tv_PictureBoxRight As System.Windows.Forms.PictureBox
    Friend WithEvents tsmiTMDbSetName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMovieSetsRepopulateFromUsed As System.Windows.Forms.Button
    Friend WithEvents btnMovRefreshAll As System.Windows.Forms.Button
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer8 As System.Windows.Forms.SplitContainer
    Friend WithEvents tsmiRescrapeCountry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiRescrapePremiered As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiRescrapeTop250 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiRescrapePosterUrls As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiRescrapeFrodo_Poster_Thumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiRescrapeFrodo_Fanart_Thumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox21 As System.Windows.Forms.GroupBox
    Friend WithEvents FrodoImageTrue As System.Windows.Forms.Label
    Friend WithEvents EdenImageTrue As System.Windows.Forms.Label
    Friend WithEvents ArtMode As System.Windows.Forms.Label
    Friend WithEvents btnMovieManualPathAdd As System.Windows.Forms.Button
    Friend WithEvents Label184 As System.Windows.Forms.Label
    Friend WithEvents tbMovieManualPath As System.Windows.Forms.TextBox
    Friend WithEvents btnTvRefreshAll As System.Windows.Forms.Button
    Friend WithEvents btnTvSearchNew As System.Windows.Forms.Button
    Friend WithEvents btnPrevMissingFanart As System.Windows.Forms.Button
    Friend WithEvents lblFanartMissingCount As System.Windows.Forms.Label
    Friend WithEvents lblPosterMissingCount As System.Windows.Forms.Label
    Friend WithEvents btnPrevMissingPoster As System.Windows.Forms.Button
    Friend WithEvents btnNextMissingPoster As System.Windows.Forms.Button
    Friend WithEvents gbMoviePosterControls As System.Windows.Forms.GroupBox
    Friend WithEvents gbMoviePoster As System.Windows.Forms.GroupBox
    Friend WithEvents panelMoviePosterLHS As System.Windows.Forms.Panel
    Friend WithEvents gbMoviePosterSelection As System.Windows.Forms.GroupBox
    Friend WithEvents tbSelectMoviePoster As System.Windows.Forms.TextBox
    Friend WithEvents gbMoviePostersAvailable As System.Windows.Forms.GroupBox
    Friend WithEvents cmsConfigureMovieFilters As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ConfigureMovieFiltersToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bnt_TvChkFolderList As System.Windows.Forms.Button
    Friend WithEvents TvEpContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ReScrFanartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelNewFanartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescrapeTvEpThumbToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescrapeTvEpScreenShotToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiOpenInMkvmergeGUI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiCheckForNewVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents HmMovPlot As System.Windows.Forms.TextBox
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents HmMovYear As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents HmMovStars As System.Windows.Forms.TextBox
    Friend WithEvents ReloadHtmlTemplatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents btnHomeManualPathAdd As System.Windows.Forms.Button
    Friend WithEvents tbHomeManualPath As System.Windows.Forms.TextBox
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents GroupBox40 As System.Windows.Forms.GroupBox
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents btnMovTagSavetoNfo As System.Windows.Forms.Button
    Friend WithEvents btnMovTagRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovTagAdd As System.Windows.Forms.Button
    Friend WithEvents CurrentMovieTags As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox39 As System.Windows.Forms.GroupBox
    Friend WithEvents Label188 As System.Windows.Forms.Label
    Friend WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents btnMovTagListRefresh As System.Windows.Forms.Button
    Friend WithEvents btnMovTagListRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovTagListAdd As System.Windows.Forms.Button
    Friend WithEvents txtbxMovTagEntry As System.Windows.Forms.TextBox
    Friend WithEvents TagListBox As System.Windows.Forms.ListBox
    Friend WithEvents cbFilterGenre As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents cbFilterCountries As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterYear As System.Windows.Forms.Label
    Friend WithEvents cbFilterYear As MC_UserControls.SelectionRangeSlider
    Friend WithEvents cbFilterVotes As MC_UserControls.SelectionRangeSlider
    Friend WithEvents lblFilterVotes As System.Windows.Forms.Label

    Friend WithEvents cbFilterRuntime As MC_UserControls.SelectionRangeSlider
    Friend WithEvents lblFilterRuntime As System.Windows.Forms.Label

    Friend WithEvents cbFilterFolderSizes As MC_UserControls.SelectionRangeSlider
    Friend WithEvents lblFilterFolderSizes As System.Windows.Forms.Label


    Friend WithEvents lblFilterRating As System.Windows.Forms.Label
    Friend WithEvents lblFilterNumAudioTracks As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioBitrates As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioChannels As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioLanguages As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioDefaultLanguages As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioCodecs As System.Windows.Forms.Label
    Friend WithEvents lblFilterResolution As System.Windows.Forms.Label
    Friend WithEvents lblFilterGeneral As System.Windows.Forms.Label
    Friend WithEvents lblFilterActor As System.Windows.Forms.Label
    Friend WithEvents lblFilterSet As System.Windows.Forms.Label
    Friend WithEvents lblFilterSource As System.Windows.Forms.Label
    Friend WithEvents lblFilterGenre As System.Windows.Forms.Label
    Friend WithEvents lblFilterCountries As System.Windows.Forms.Label
    Friend WithEvents cbFilterGeneral As System.Windows.Forms.ComboBox
    Friend WithEvents cbFilterRating As MC_UserControls.SelectionRangeSlider
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Friend WithEvents TooltipGridViewMovies1 As Media_Companion.TooltipGridViewMovies
    Friend WithEvents cbFilterCertificate As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterCertificate As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tlpMovies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents PbMovieFanArt As System.Windows.Forms.PictureBox
    Friend WithEvents PbMoviePoster As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnMovSave As System.Windows.Forms.Button
    Friend WithEvents btnMovRescrape As System.Windows.Forms.Button
    Friend WithEvents TextBoxMutisave As System.Windows.Forms.TextBox
    Friend WithEvents plottxt As System.Windows.Forms.TextBox
    Friend WithEvents outlinetxt As System.Windows.Forms.TextBox
    Friend WithEvents taglinetxt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_movTagline As System.Windows.Forms.Label
    Friend WithEvents lbl_movOutline As System.Windows.Forms.Label
    Friend WithEvents lbl_movPlot As System.Windows.Forms.Label
    Friend WithEvents PictureBoxActor As System.Windows.Forms.PictureBox
    Friend WithEvents roletxt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_movPath As System.Windows.Forms.Label
    Friend WithEvents lbl_movSets As System.Windows.Forms.Label
    Friend WithEvents lbl_movRuntime As System.Windows.Forms.Label
    Friend WithEvents lbl_movRating As System.Windows.Forms.Label
    Friend WithEvents lbl_movCert As System.Windows.Forms.Label
    Friend WithEvents lbl_movGenre As System.Windows.Forms.Label
    Friend WithEvents lbl_movStars As System.Windows.Forms.Label
    Friend WithEvents btnMovieDisplay_ActorFilter As System.Windows.Forms.Button
    Friend WithEvents cbMovieDisplay_Actor As System.Windows.Forms.ComboBox
    Friend WithEvents directortxt As System.Windows.Forms.TextBox
    Friend WithEvents creditstxt As System.Windows.Forms.TextBox
    Friend WithEvents studiotxt As System.Windows.Forms.TextBox
    Friend WithEvents cbMovieDisplay_Source As System.Windows.Forms.ComboBox
    Friend WithEvents btnMovWatched As System.Windows.Forms.Button
    Friend WithEvents btnPlayMovie As System.Windows.Forms.Button
    Friend WithEvents ButtonTrailer As System.Windows.Forms.Button
    Friend WithEvents lbl_movSource As System.Windows.Forms.Label
    Friend WithEvents lbl_movStudio As System.Windows.Forms.Label
    Friend WithEvents lbl_movCredits As System.Windows.Forms.Label
    Friend WithEvents lbl_movDirector As System.Windows.Forms.Label
    Friend WithEvents lbl_movActors As System.Windows.Forms.Label
    Friend WithEvents btnMovieDisplay_SetFilter As System.Windows.Forms.Button
    Friend WithEvents votestxt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_movImdbid As System.Windows.Forms.Label
    Friend WithEvents lbl_movVotes As System.Windows.Forms.Label
    Friend WithEvents pathtxt As System.Windows.Forms.TextBox
    Friend WithEvents cbMovieDisplay_MovieSet As System.Windows.Forms.ComboBox
    Friend WithEvents runtimetxt As System.Windows.Forms.TextBox
    Friend WithEvents ratingtxt As System.Windows.Forms.TextBox
    Friend WithEvents certtxt As System.Windows.Forms.TextBox
    Friend WithEvents genretxt As System.Windows.Forms.TextBox
    Friend WithEvents txtStars As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents TextBox34 As System.Windows.Forms.TextBox
    Friend WithEvents titletxt As System.Windows.Forms.ComboBox
    Friend WithEvents cbFilterSet As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents cbFilterResolution As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents cbFilterAudioCodecs As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterGenreMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterCountriesMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterCertificateMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioCodecsMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterResolutionMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterSetMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioChannelsMode As System.Windows.Forms.Label
    Friend WithEvents cbFilterAudioChannels As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents cbFilterAudioBitrates As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterAudioBitratesMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterNumAudioTracksMode As System.Windows.Forms.Label
    Friend WithEvents cbFilterNumAudioTracks As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterAudioLanguagesMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterAudioDefaultLanguagesMode As System.Windows.Forms.Label
    Friend WithEvents cbFilterAudioLanguages As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents cbFilterAudioDefaultLanguages As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterActorMode As System.Windows.Forms.Label
    Friend WithEvents cbFilterActor As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterSourceMode As System.Windows.Forms.Label
    Friend WithEvents cbFilterSource As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents btn_TMDBSearch As System.Windows.Forms.Button
    Friend WithEvents btn_IMDBSearch As System.Windows.Forms.Button
    Friend WithEvents Label192 As System.Windows.Forms.Label
    Friend WithEvents cbClearCache As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_movTop250 As System.Windows.Forms.Label
    Friend WithEvents top250txt As System.Windows.Forms.TextBox
    Friend WithEvents UcGenPref_XbmcLink1 As Media_Companion.ucGenPref_XbmcLink
    Friend WithEvents cbBtnLink As System.Windows.Forms.CheckBox
    Friend WithEvents btn_TvIMDB As System.Windows.Forms.Button
    Friend WithEvents btn_TvTVDb As System.Windows.Forms.Button
    Friend WithEvents Label195 As System.Windows.Forms.Label
    Friend WithEvents Label194 As System.Windows.Forms.Label
    Friend WithEvents WebBrowser2 As System.Windows.Forms.WebBrowser
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents tsmiSyncToXBMC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiConvertToFrodo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FixNFOCreateDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tagtxt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_movTags As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel16 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel15 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainer9 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel17 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel18 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel20 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel19 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel21 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel22 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel24 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Tv_TreeViewContext_RescrapeMediaTags As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_RescrapeWizard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_RenMovieAndFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_RenMovieOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_RenMovFolderOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel25 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents STitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SPlot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SPremiered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SRating As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SGenre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SStudio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STVDBId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SIMDBId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SCert As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TvPosterContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsm_TvScrapePoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_TvSelectPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_TvScrapeBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_TvSelectBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMoviePosterEnableCrop As System.Windows.Forms.Button
    Friend WithEvents btnMovPasteClipboardPoster As System.Windows.Forms.Button
    Friend WithEvents btnMovPasteClipboardFanart As System.Windows.Forms.Button
    Friend WithEvents cbFilterTag As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterTagMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterTag As System.Windows.Forms.Label
    Friend WithEvents BtnSearchGoogleFanart As System.Windows.Forms.Button
    Friend WithEvents BtnGoogleSearchPoster As System.Windows.Forms.Button
    Friend WithEvents cbTvActorRole As System.Windows.Forms.ComboBox
    Friend WithEvents TabCustTv As System.Windows.Forms.TabPage
    Friend WithEvents Custom_Tv1 As Media_Companion.Custom_Tv
    Friend WithEvents tpMediaStubs As System.Windows.Forms.TabPage
    Friend WithEvents MediaStubs1 As Media_Companion.MediaStubs
    Friend WithEvents ToolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mov_ToolStripDeleteNfoArtwork As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel26 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel27 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel28 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnMovieDisplay_DirectorFilter As System.Windows.Forms.Button
    Friend WithEvents btnMovieDisplay_CountriesFilter As System.Windows.Forms.Button
    Friend WithEvents cbFilterDirector As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterDirectorMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterDirector As System.Windows.Forms.Label
    Friend WithEvents lbl_sorttitle As System.Windows.Forms.Label
    Friend WithEvents TextBox_Sorttitle As System.Windows.Forms.TextBox
    Friend WithEvents RefreshMissingEpisodesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabMV As System.Windows.Forms.TabPage
    Friend WithEvents UcMusicVideo1 As Media_Companion.ucMusicVideo
    Friend WithEvents premiertxt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_movPremiered As System.Windows.Forms.Label
    Friend WithEvents cbClearMissingFolder As System.Windows.Forms.CheckBox
    Friend WithEvents lb_EpDetails As System.Windows.Forms.ListBox
    Friend WithEvents cbTvSource As System.Windows.Forms.ComboBox
    Friend WithEvents PreferencesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiDlTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblFilterVideoCodec As System.Windows.Forms.Label
    Friend WithEvents lblFilterVideoCodecMode As System.Windows.Forms.Label
    Friend WithEvents cbFilterVideoCodec As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents btn_movTableColumnsSelect As System.Windows.Forms.Button
    Friend WithEvents lbl_movTableEdit As System.Windows.Forms.Label
    Friend WithEvents mov_TableEditDGV As System.Windows.Forms.DataGridView
    Friend WithEvents TableLayoutPanel29 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_movCountry As System.Windows.Forms.Label
    Friend WithEvents countrytxt As System.Windows.Forms.TextBox
    Friend WithEvents tsmiSetWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiClearWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator28 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlpMovieButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tsmicacheclean As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiRescrapeKeyWords As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiWallPlayTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpFanartTv As System.Windows.Forms.TabPage
    Friend WithEvents UcFanartTv1 As Media_Companion.ucFanartTv
    Friend WithEvents RefreshGenreListboxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiRescrapeFanartTv As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents movieGraphicInfo As Media_Companion.GraphicInfo
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents FanTvArtList As System.Windows.Forms.ListBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ftvArtPicBox As System.Windows.Forms.PictureBox
    Friend WithEvents tpTvFanartTv As System.Windows.Forms.TabPage
    Friend WithEvents UcFanartTvTv1 As Media_Companion.ucFanartTvTv
    Friend WithEvents cbTvChgShowDLFanartTvArt As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents tvFanlistbox As System.Windows.Forms.ListBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pbtvfanarttv As System.Windows.Forms.PictureBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel30 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tsmiTvDeletenfoart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiTvDelShowNfoArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiTvDelShowEpNfoArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiTvDelEpNfoArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rbMovThumb5 As System.Windows.Forms.RadioButton
    Friend WithEvents Tv_tsmi_CheckDuplicateEpisodes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rcmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TableLayoutPanel31 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnMovSelectPlot As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator29 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator30 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExportLibraryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tv_TreeViewContext_MissingEpThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbFilterSubTitleLang As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterSubTitleLangMode As System.Windows.Forms.Label
    Friend WithEvents lblFilterSubTitleLang As System.Windows.Forms.Label
    Friend WithEvents SplitContainer10 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents rbTvDisplayUnWatched As System.Windows.Forms.RadioButton
    Friend WithEvents rbTvDisplayWatched As System.Windows.Forms.RadioButton
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiMovieSetIdCheck As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMovFanartToggle As System.Windows.Forms.Button
    Friend WithEvents btnMovPosterToggle As System.Windows.Forms.Button
    Friend WithEvents tsmiRescrapeMovieSetArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvmovset As System.Windows.Forms.DataGridView
    Friend WithEvents movsettitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tmdbid As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents movsetfanart As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents movsetposter As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents lblMovTagMulti1 As System.Windows.Forms.Label
    Friend WithEvents lblMovTagMulti2 As System.Windows.Forms.Label
    Friend WithEvents lbl_airepisode As System.Windows.Forms.Label
    Friend WithEvents tb_airepisode As System.Windows.Forms.TextBox
    Friend WithEvents lbl_airbefore As System.Windows.Forms.Label
    Friend WithEvents lbl_airseason As System.Windows.Forms.Label
    Friend WithEvents tb_airseason As System.Windows.Forms.TextBox
    Friend WithEvents tp_HmPoster As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel32 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents pbx_HmPosterSht As System.Windows.Forms.PictureBox
    Friend WithEvents btn_HmPosterShot As System.Windows.Forms.Button
    Friend WithEvents tb_HmPosterTime As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As Label
    Friend WithEvents pbx_HmPoster As PictureBox
    Friend WithEvents clbx_MovieRoots As System.Windows.Forms.CheckedListBox
    Friend WithEvents clbx_TvRootFolders As CheckedListBox
    Friend WithEvents TSMI_AboutMC As ToolStripMenuItem
    Friend WithEvents Label125 As Label
    Friend WithEvents tb_TvShSelectSeriesPath As TextBox
    Friend WithEvents lblFilterStudiosMode As Label
    Friend WithEvents cbFilterStudios As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterStudios As Label
    Friend WithEvents btn_HmFolderSaveRefresh As Button
    Friend WithEvents clbx_HMMovieFolders As CheckedListBox
    Friend WithEvents btn_HMRefresh As Button
    Friend WithEvents btn_HMSearch As Button
    Friend WithEvents TvRootFolderContextMenu As ContextMenuStrip
    Friend WithEvents tsmi_tvRtAddSeries As ToolStripMenuItem
    Friend WithEvents Label131 As Label
    Friend WithEvents bnt_TvSeriesStatus As Button
    Friend WithEvents rbTvListContinuing As RadioButton
    Friend WithEvents rbTvListEnded As RadioButton
    Friend WithEvents rbTvListUnKnown As RadioButton
    Friend WithEvents MovSetsContextMenu As ContextMenuStrip
    Friend WithEvents tsmiMovSetShowCollection As ToolStripMenuItem
    Friend WithEvents tsmiMovSetGetFanart As ToolStripMenuItem
    Friend WithEvents tsmiMovSetGetPoster As ToolStripMenuItem
    Friend WithEvents tsmiMovSetName As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator31 As ToolStripSeparator
    Friend WithEvents tsmiCleanCacheOnly As ToolStripMenuItem
    Friend WithEvents tsmiCleanSeriesonly As ToolStripMenuItem
    Friend WithEvents ImageList3 As ImageList
    Friend WithEvents cbFilterRootFolder As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterRootFolderMode As Label
    Friend WithEvents lblFilterRootFolder As Label
    Friend WithEvents tp_HmPref As TabPage
    Friend WithEvents cbUsrRated As ComboBox
    Friend WithEvents cbFilterUserRated As MC_UserControls.TriStateCheckedComboBox
    Friend WithEvents lblFilterUserRatedMode As Label
    Friend WithEvents lblFilterUserRated As Label
    Friend WithEvents btn_MovFanartScrnSht As Button
    Friend WithEvents tb_MovFanartScrnShtTime As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tpTvWall As TabPage
    Friend WithEvents TVWallContextMenu As ContextMenuStrip
    Friend WithEvents tsmiTvWallLargeView As ToolStripMenuItem
    Friend WithEvents tsmiTvWallOpenFolder As ToolStripMenuItem
    Friend WithEvents tsmiTvWallPosterChange As ToolStripMenuItem
    Friend WithEvents Panel14 As Panel
    Friend WithEvents btnTvWebStop As Button
    Friend WithEvents btnTvWebBack As Button
    Friend WithEvents btnTvWebForward As Button
    Friend WithEvents btnTvWebRefresh As Button
    Friend WithEvents TableLayoutPanel23 As TableLayoutPanel
    Friend WithEvents Panel17 As Panel
    Friend WithEvents btnMovWebIMDb As Button
    Friend WithEvents btnMovWebTMDb As Button
    Friend WithEvents Panel18 As Panel
    Friend WithEvents btnMovWebStop As Button
    Friend WithEvents btnMovWebBack As Button
    Friend WithEvents btnMovWebForward As Button
    Friend WithEvents btnMovWebRefresh As Button
End Class
