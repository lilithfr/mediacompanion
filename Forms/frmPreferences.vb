﻿Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml


Public Class frmPreferences
    Public Const SetDefaults = True
    Private fb As New FolderBrowserDialog
    Public SelectTab As Integer = 0
    Dim _Pref As New Pref
    Dim moviefolders As New List(Of str_RootPaths)
    Dim tvfolders As New List(Of String)
    Dim _changed As Boolean
    Dim XbmcTMDbScraperChanged As Boolean = False
    Dim XbmcTvdbScraperChanged As Boolean = False
    Dim prefsload As Boolean = False
    Dim videosourceprefchanged As Boolean = False
    Dim cleanfilenameprefchanged As Boolean = False

    Private Const WM_USER As Integer = &H400
    Private Const BFFM_SETEXPANDED As Integer = WM_USER + 106
    Private WithEvents Tmr As New Windows.Forms.Timer With {.Interval = 200}
    <DllImport("user32.dll", EntryPoint:="SendMessageW")> _
    Private Shared Function SendMessageW(ByVal hWnd As IntPtr, ByVal msg As UInteger, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindowW")> _
    Private Shared Function FindWindowW(<MarshalAs(UnmanagedType.LPWStr)> ByVal lpClassName As String, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpWindowName As String) As IntPtr
    End Function
    
    Public Property Changes As Boolean
        Get
            Return _changed
        End Get
        Set(value As Boolean)
            _changed = value
        End Set
    End Property

#Region "Form Events"

    Private Sub options_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Changes Then
                Dim tempint As Integer = MessageBox.Show("You appear to have made changes to your preferences," & vbCrLf & "Do wish to save the changes", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.Yes Then
                    'If Pref.videomode = 4 AndAlso Not IO.File.Exists(Pref.selectedvideoplayer) Then
                    '    MsgBox("You Have Not Selected Your Preferred Media Player")
                    '    e.Cancel = True
                    '    Exit Sub
                    'End If
                    Dim aok As Boolean = SaveSettings()
                    If Not aok Then
                        e.Cancel = True
                        Exit Sub
                    End If
                Else
                    Form1.util_ConfigLoad(True)
                    Form1.util_RegexLoad()
                    'Pref.ConfigLoad()
                End If
                Changes = False
                XbmcTvdbScraperChanged = False
                XbmcTMDbScraperChanged = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub frmOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btn_SettingsClose2.Visible = False
            Pref.ConfigLoad()
            prefsload = True
            PrefInit()
            CommonInit()
            GeneralInit()
            MovieInit()
            TVInit()
            CmdsNProfilesInit()
            prefsload = False
            Changes = False
            TabControl1.SelectedIndex = SelectTab
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_SettingsApply_Click(sender As Object, e As EventArgs) Handles btn_SettingsApply.Click
        Dim aok As Boolean = SaveSettings
        'If cleanfilenameprefchanged OrElse videosourceprefchanged Then
        '    applyAdvancedLists()
        'End If
        'Pref.ConfigSave()
        'Changes = False
    End Sub

    Private Sub btn_SettingsCancel_Click(sender As Object, e As EventArgs) Handles btn_SettingsCancel.Click
        Changes = False
        Pref.ConfigLoad()
    End Sub

    Private Sub btn_SettingsClose_Click(sender As Object, e As EventArgs) Handles btn_SettingsClose.Click, btn_SettingsClose2.Click
        Me.Close()
    End Sub
    
#End Region 'Form Events

    Private Function SaveSettings() As Boolean
        If Pref.videomode = 4 AndAlso Not IO.File.Exists(Pref.selectedvideoplayer) Then
            MsgBox("You Have Not Selected Your Preferred Media Player")
            Return False
        End If
        Pref.ExcludeFolders.PopFromTextBox(tbExcludeFolders)
        
        If cleanfilenameprefchanged OrElse videosourceprefchanged Then
            applyAdvancedLists()
        End If
        Form1.util_RegexSave()
        Pref.ConfigSave()

        Form1.UpdateMovieSetDisplayNames

        If XbmcTMDbScraperChanged Then XBMCTMDBConfigSave()
        If XbmcTvdbScraperChanged Then XBMCTVDBConfigSave()
        cleanfilenameprefchanged = False
        videosourceprefchanged = False
        XbmcTMDbScraperChanged = False
        XbmcTvdbScraperChanged = False
        Changes = False
        Return True
    End Function

    Private Sub PrefInit()
        'Initial
        Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
        Dim newFont As System.Drawing.Font
        If Not String.IsNullOrEmpty(Pref.font) Then
            Try
                newFont = CType(tcc.ConvertFromString(Pref.font), System.Drawing.Font)
            Catch ex As Exception
                newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
                Pref.font = "Times New Roman, 9pt"
            End Try
        Else
            newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
            Pref.font = "Times New Roman, 9pt"
        End If
        lbl_FontSample.Font = newFont
        lbl_FontSample.Text = Pref.font
    End Sub

    Private Sub CommonInit()
        'Common Section
        If Pref.XBMC_version = 0 Then
            rbXBMCv_pre.Checked = True
        ElseIf Pref.XBMC_version = 1 Then
            rbXBMCv_both.Checked = True
        ElseIf Pref.XBMC_version = 2 Then
            rbXBMCv_post.Checked = True
        End If
        CheckBox38                  .Checked    = Pref.intruntime
        cb_IgnoreThe                .Checked    = Pref.ignorearticle
        cb_IgnoreA                  .Checked    = Pref.ignoreAarticle
        cb_IgnoreAn                 .Checked    = Pref.ignoreAn
        cbOverwriteArtwork          .Checked    = Not Pref.overwritethumbs
        cbDisplayRatingOverlay      .Checked    = Pref.DisplayRatingOverlay
        cbDisplayMediaInfoOverlay   .Checked    = Pref.DisplayMediainfoOverlay 
        cbDisplayMediaInfoFolderSize.Checked    = Pref.DisplayMediaInfoFolderSize
        cbShowAllAudioTracks        .Checked    = Pref.ShowAllAudioTracks
        AutoScrnShtDelay            .Text       = Pref.ScrShtDelay
        Pref.ExcludeFolders.PopTextBox(tbExcludeFolders)

        Movie.LoadBackDropResolutionOptions(comboBackDropResolutions, Pref.BackDropResolutionSI) 'SI = Selected Index
        Movie.LoadHeightResolutionOptions(comboPosterResolutions, Pref.PosterResolutionSI)
        Movie.LoadHeightResolutionOptions(comboActorResolutions, Pref.ActorResolutionSI)

        lbCleanFilename.Items.Clear()
        lbCleanFilename.Items.AddRange(Pref.moviecleanTags.Split("|"))
        If lbVideoSource.Items.Count <> Pref.releaseformat.Length Then
            lbVideoSource.Items.Clear()
            For f = 0 To Pref.releaseformat.Length - 1
                lbVideoSource.Items.Add(Pref.releaseformat(f))
            Next
        End If

        'Common - Actors Section
        cb_actorseasy               .Checked    = Pref.actorseasy 
        Select Case Pref.maxactors
            Case 9999
                cmbx_MovMaxActors.SelectedItem = "All Available"
            Case 0
                cmbx_MovMaxActors.SelectedItem = "None"
            Case Else
                cmbx_MovMaxActors.SelectedItem = Pref.maxactors.ToString
        End Select
        saveactorchkbx                      .Checked        = Pref.actorsave
        cb_LocalActorSaveAlpha              .Checked        = Pref.actorsavealpha
        localactorpath                      .Text           = Pref.actorsavepath
        xbmcactorpath                       .Text           = Pref.actornetworkpath
        localactorpath                      .Enabled        = Pref.actorsave
        xbmcactorpath                       .Enabled        = Pref.actorsave
    End Sub

    Private Sub GeneralInit()
        'General Section
        If Pref.videomode = 1 Then
            rb_MediaPlayerDefault.Checked = True
        ElseIf Pref.videomode = 2 Then
            rb_MediaPlayerWMP.Checked = True
        ElseIf Pref.videomode = 4 Then
            rb_MediaPlayerUser.Checked = True
        End If

        If Pref.videomode = 4 Then
            lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            lbl_MediaPlayerUser.Visible = True
            btn_MediaPlayerBrowse.Enabled = True
        Else
            btn_MediaPlayerBrowse.Enabled = False
            lbl_MediaPlayerUser.Visible = False
        End If
        txtbx_minrarsize            .Text       = Pref.rarsize.ToString
        cbExternalbrowser           .Checked    = Pref.externalbrowser
        chkbx_disablecache          .Checked    = Not Pref.startupCache
        cbUseMultipleThreads        .Checked    = Pref.UseMultipleThreads
        cbShowLogOnError            .Checked    = Pref.ShowLogOnError
        cbCheckForNewVersion        .Checked    = Pref.CheckForNewVersion
        cbDisplayLocalActor         .Checked    = Pref.LocalActorImage
        cbRenameNFOtoINFO           .Checked    = Pref.renamenfofiles
        cbMultiMonitorEnable        .Checked    = Pref.MultiMonitoEnabled
        tbaltnfoeditor              .Text       = Pref.altnfoeditor
        tbMkvMergeGuiPath           .Text       = Pref.MkvMergeGuiPath
    End Sub

    Private Sub MovieInit()
        prefsload = True
        Form1.displayRuntimeScraper = True
        MovScraperInit()
        MovArtInit()
        MovGenInit
        MovAdvInit()
        'the following code aligns the 2 groupboxes ontop of each other which cannot be done in the GUI
        GroupBox_TMDB_Scraper_Preferences.Location = GroupBox_MovieIMDBMirror.Location
    End Sub
    
    Private Sub MovScraperInit()
        'scraper
        Read_XBMC_TMDB_Scraper_Config
        If Pref.movies_useXBMC_Scraper = True Then
            CheckBox_Use_XBMC_Scraper.CheckState = CheckState.Checked
        Else
            CheckBox_Use_XBMC_Scraper.CheckState = CheckState.Unchecked
            GroupBox_MovieIMDBMirror.Enabled = True
            GroupBox_MovieIMDBMirror.Visible = True
            GroupBox_MovieIMDBMirror.BringToFront()
        End If
        ''XBMC
        cbXbmcTmdbFanart                    .Checked        = Convert.ToBoolean(Pref.XbmcTmdbScraperFanart)
        cbXbmcTmdbOutlineFromImdb           .Checked        = Pref.XbmcTmdbMissingFromImdb
        cbXbmcTmdbTop250FromImdb            .Checked        = Pref.XbmcTmdbTop250FromImdb
        cbXbmcTmdbIMDBRatings               .Checked        = If(Pref.XbmcTmdbScraperRatings.ToLower = "imdb", True, False)
        cbXbmcTmdbAkasFromImdb              .Checked        = Pref.XbmcTmdbAkasFromImdb
        cbXbmcTmdbStarsFromImdb             .Checked        = Pref.XbmcTmdbStarsFromImdb
        cbXbmcTmdbCertFromImdb              .Checked        = Pref.XbmcTmdbCertFromImdb
        cbXbmcTmdbVotesFromImdb             .Checked        = Pref.XbmcTmdbVotesFromImdb
        'TMDB Trailer
        cmbxXbmcTmdbHDTrailer.Items.Clear() 
        For each tra In Pref.XbmcTmdbScraperTrailerQLB
            cmbxXbmcTmdbHDTrailer.Items.Add(tra)
        Next
        cmbxXbmcTmdbHDTrailer.Text = Pref.XbmcTmdbScraperTrailerQ
        'TMDB Pref Language
        cmbxXbmcTmdbTitleLanguage.Items.Clear()
        For Each la In Pref.XbmcTmdbScraperLanguageLB
            cmbxXbmcTmdbTitleLanguage.Items.Add(la)
        Next
        cmbxXbmcTmdbTitleLanguage.Text = Pref.XbmcTmdbScraperLanguage
        'TMDB Pref Cert Country
        cmbxTMDBPreferredCertCountry.Items.Clear()
        For Each thisvalue In Pref.XbmcTmdbScraperCertCountryLB
            cmbxTMDBPreferredCertCountry.Items.Add(thisvalue)
        Next
        cmbxTMDBPreferredCertCountry.Text = Pref.XbmcTmdbScraperCertCountry
        cbXbmcTmdbRename                    .Checked        = Pref.XbmcTmdbRenameMovie
        cbXbmcTmdbActorDL                   .Checked        = Pref.XbmcTmdbActorDL
        
        ''IMDB
        lb_IMDBMirrors                      .SelectedItem   = Pref.imdbmirror
        cbImdbgetTMDBActor                  .Checked        = Pref.TmdbActorsImdbScrape
        cbImdbPrimaryPlot                   .Checked        = Pref.ImdbPrimaryPlot
        cbMovImdbFirstRunTime               .Checked        = Pref.MovImdbFirstRunTime

        'IndividualMovieFolders
        cbMovieUseFolderNames               .Checked        = Pref.usefoldernames
        cbMovieAllInFolders                 .Checked        = Pref.allfolders

        ''Scraping Options
        'Preferred Language
        TMDbControlsIni()
        comboBoxTMDbSelectedLanguage        .Text = Pref.TMDbSelectedLanguageName
        cbUseCustomLanguage                 .Checked = Pref.TMDbUseCustomLanguage
        tbCustomLanguageValue               .Text = Pref.TMDbCustomLanguageValue
        SetLanguageControlsState()

        'Scraper Limits
        Select Case Pref.maxmoviegenre
            Case 99
                cmbxMovScraper_MaxGenres.SelectedItem = "All Available"
            Case 0
                cmbxMovScraper_MaxGenres.SelectedItem = "None"
            Case Else
                cmbxMovScraper_MaxGenres.SelectedItem = Pref.maxmoviegenre.ToString
        End Select
        cmbxMovieScraper_MaxStudios         .SelectedItem   = Pref.MovieScraper_MaxStudios.ToString

        'Other Options
        cbGetMovieSetFromTMDb               .Checked    = Pref.GetMovieSetFromTMDb
        chkbOriginal_Title                  .Checked    = Pref.Original_Title

        'BasicSave Mode
        cbMovieBasicSave                    .Checked    = Pref.basicsavemode

        'Keywords As Tags
        cb_keywordasTag                     .Checked    = Pref.keywordasTag
        Select Case Pref.keywordlimit 
            Case 999
                cb_keywordlimit.SelectedItem = "All Available"
            Case 0
                cb_keywordlimit.SelectedItem = "None"
            Case Else
                cb_keywordlimit.SelectedItem = Pref.keywordlimit.ToString
        End Select

        'IMDB Cert Priority
        lb_IMDBCertPriority.Items.Clear()
        For f = 0 To 33
            lb_IMDBCertPriority.Items.Add(Pref.certificatepriority(f))
        Next
        ScrapeFullCertCheckBox              .Checked        = Pref.scrapefullcert

    End Sub

    Private Sub MovArtInit()
        ''artwork
        'AutoScrape Artwork
        cbMoviePosterScrape                 .Checked        = Pref.scrapemovieposters
        cbMovFanartScrape                   .Checked        = Pref.savefanart
        cbMovFanartTvScrape                 .Checked        = Pref.MovFanartTvscrape
        cbMovFanartNaming                   .Checked        = Pref.MovFanartNaming
        cbDlXtraFanart                      .Checked        = Pref.dlxtrafanart
        cbMovSetArtScrape                   .Checked        = Pref.dlMovSetArtwork

        ''Movie Poster Scrape Priority
        lbPosterSourcePriorities.Items.Clear()
        For f = 0 To Pref.moviethumbpriority.Count-1
            lbPosterSourcePriorities.Items.Add(Pref.moviethumbpriority(f))
        Next

        'Movie in folders, Extra Artwork
        cbMovXtraThumbs                     .Checked        = Pref.movxtrathumb
        cbMovXtraFanart                     .Checked        = Pref.movxtrafanart
        cmbxMovXtraFanartQty                .SelectedIndex  = cmbxMovXtraFanartQty.FindStringExact(Pref.movxtrafanartqty.ToString)
        'movie in folder, save artwork as
        If Not Pref.usefoldernames AndAlso Not Pref.allfolders then
            cbMovCreateFolderjpg    .Enabled    = False
            cbMovCreateFanartjpg    .Enabled    = False
            cbMovieFanartInFolders  .Enabled    = False
            cbMoviePosterInFolder   .Enabled    = False
            Pref.fanartjpg                      = False
            Pref.posterjpg                      = False
            Pref.createfolderjpg                = False
            Pref.createfanartjpg                = False
        Else
            cbMovieFanartInFolders  .Checked    = Pref.fanartjpg
            cbMoviePosterInFolder   .Checked    = Pref.posterjpg
            cbMovCreateFolderjpg    .Checked    = Pref.createfolderjpg
        cbMovCreateFanartjpg        .Checked    = Pref.createfanartjpg
        End If

        ''MovieSet Artwork
        rbMovSetArtSetFolder                .Checked        = Pref.MovSetArtSetFolder
        rbMovSetFolder                      .Checked        = Not Pref.MovSetArtSetFolder 
        btnMovSetCentralFolderSelect        .Enabled        = Pref.MovSetArtSetFolder 
        tbMovSetArtCentralFolder            .Text           = Pref.MovSetArtCentralFolder
    End Sub

    Private Sub MovGenInit()
        ''General
        'General Options
        cbMovieTrailerUrl                   .Checked        = Pref.gettrailer
        cbDlTrailerDuringScrape             .Checked        = Pref.DownloadTrailerDuringScrape
        cbPreferredTrailerResolution        .Text           = Pref.moviePreferredTrailerResolution.ToUpper()
        cbMovTitleCase                      .Checked        = Pref.MovTitleCase
        cbNoAltTitle                        .Checked        = Pref.NoAltTitle
        cbXtraFrodoUrls                     .Checked        = Not Pref.XtraFrodoUrls
        cb_MovDisplayLog                    .Checked        = Not Pref.disablelogfiles
        cbExcludeMpaaRated                  .Checked        = Pref.ExcludeMpaaRated
        cbMovThousSeparator                 .Checked        = Pref.MovThousSeparator
        If Pref.enablehdtags = True Then
            cb_EnableMediaTags.CheckState = CheckState.Checked
            PanelDisplayRuntime.Enabled = True
            If Pref.movieRuntimeDisplay = "file" Then
                rbRuntimeFile.Checked = True
            Else
                rbRuntimeScraper.Checked = True
            End If
        Else
            cb_EnableMediaTags.CheckState = CheckState.Unchecked
            PanelDisplayRuntime.Enabled = False
            rbRuntimeScraper.Checked = True
        End If
        cb_MovDurationAsRuntine             .Checked        = Pref.MovDurationAsRuntine
        cbMovieRuntimeFallbackToFile        .Enabled        = (Pref.movieRuntimeDisplay = "scraper")
        cbMovieRuntimeFallbackToFile        .Checked        = Pref.movieRuntimeFallbackToFile
        cbMovieShowDateOnList               .Checked        = Pref.showsortdate
        cbMissingMovie                      .Checked        = Pref.incmissingmovies
        cbMovRootFolderCheck                .Checked        = Pref.movrootfoldercheck
        cb_SorttitleIgnoreArticles          .Checked        = Pref.sorttitleignorearticle

        'Rename Movie Settings
        cbMovFolderRename                   .Checked        = Pref.MovFolderRename
        tb_MovFolderRename                  .Text           = Pref.MovFolderRenameTemplate
        cbMovieRenameEnable                 .Checked        = Pref.MovieRenameEnable
        tb_MovieRenameTemplate              .Text           = Pref.MovieRenameTemplate
        cbMovNewFolderInRootFolder          .Checked        = Pref.MovNewFolderInRootFolder
        cbMovTitleIgnArticle                .Checked        = Pref.MovTitleIgnArticle
        cbMovSetIgnArticle                  .Checked        = Pref.MovSetIgnArticle
        cbMovSortIgnArticle                 .Checked        = Pref.MovSortIgnArticle
        cbRenameUnderscore                  .Checked        = Pref.MovRenameSpaceCharacter
        If Pref.RenameSpaceCharacter = "_" Then
            rbRenameUnderscore.Checked = True
        Else
            rbRenameFullStop.Checked = True
        End If
        cbMovieManualRename                 .Checked        = Pref.MovieManualRename

        'Movie List
        tbDateFormat                        .Text           = Pref.DateFormat
        cbMovieList_ShowColPlot             .Checked        = Pref.MovieList_ShowColPlot
        cbMovieList_ShowColWatched          .Checked        = Pref.MovieList_ShowColWatched

        'Name Mode
        cbMoviePartsNameMode                .Checked        = Pref.namemode
        lblNameMode                         .Text           = Form1.createNameModeText()
        cbMoviePartsIgnorePart              .Checked        = Pref.movieignorepart

        'Movie Filters
        nudActorsFilterMinFilms             .Text           = Pref.ActorsFilterMinFilms
        nudMaxActorsInFilter                .Text           = Pref.MaxActorsInFilter
        cbMovieFilters_Actors_Order         .SelectedIndex  = Pref.MovieFilters_Actors_Order
        nudDirectorsFilterMinFilms          .Text           = Pref.DirectorsFilterMinFilms
        nudMaxDirectorsInFilter             .Text           = Pref.MaxDirectorsInFilter
        cbMovieFilters_Directors_Order      .SelectedIndex  = Pref.MovieFilters_Directors_Order
        nudSetsFilterMinFilms               .Text           = Pref.SetsFilterMinFilms
        nudMaxSetsInFilter                  .Text           = Pref.MaxSetsInFilter
        cbMovieFilters_Sets_Order           .SelectedIndex  = Pref.MovieFilters_Sets_Order
        cbDisableNotMatchingRenamePattern   .Checked        = Pref.DisableNotMatchingRenamePattern

        'Offline DVD Text
        TextBox_OfflineDVDTitle             .Text           = Pref.OfflineDVDTitle
    End Sub

    Private Sub MovAdvInit()
        ''Advanced Tab
        'Separate Movie Identifier
        If lb_MovSepLst.Items.Count <> Pref.MovSepLst.Count Then
            lb_MovSepLst.Items.Clear()
            For Each t In Pref.MovSepLst 
                lb_MovSepLst.Items.Add(t)
            Next
        End If

        'nfo Poster Options
        IMPA_chk.CheckState = If(Pref.nfoposterscraper And 1, CheckState.Checked, CheckState.Unchecked)
        tmdb_chk.CheckState = If(Pref.nfoposterscraper And 2, CheckState.Checked, CheckState.Unchecked)
        mpdb_chk.CheckState = If(Pref.nfoposterscraper And 4, CheckState.Checked, CheckState.Unchecked)
        imdb_chk.CheckState = If(Pref.nfoposterscraper And 8, CheckState.Checked, CheckState.Unchecked)
    End Sub

    Private Sub TVInit()
        TVSCraperInit()
        TVRegexInit()
    End Sub

    Private Sub TVSCraperInit()
        'Language box
        ListBox12.Items.Clear()
        ListBox12.Items.Add(Pref.TvdbLanguage)
        If ListBox12.Items.Count <> 0 Then
            ListBox12.SelectedIndex = 0
        End If

        'XBMC TVBD Scraper
        rbXBMCTvdbDVDOrder              .Checked    = Pref.XBMCTVDbDvdOrder
        rbXBMCTvdbAbsoluteNumber        .Checked    = Pref.XBMCTVDbAbsoluteNumber
        cbXBMCTvdbFanart                .Checked    = Pref.XBMCTVDbFanart
        cbXBMCTvdbPosters               .Checked    = Pref.XBMCTVDbPoster
        ComboBox_TVDB_Language.Items.Clear()
        For Each item In Pref.XBMCTVDbLanguageLB
            ComboBox_TVDB_Language.Items.Add(item)
        Next
        ComboBox_TVDB_Language          .Text       = Pref.XBMCTVDbLanguage
        cbXBMCTvdbRatingImdb            .Checked    = If(Pref.XBMCTVDbRatings.ToLower = "imdb", True, False)
        cbXBMCTvdbRatingFallback        .Checked    = Pref.XBMCTVDbfallback
        cbXBMCTvdbRatingFallback        .Enabled    = cbXBMCTvdbRatingImdb.Checked

        'MC TVDB Scraper Options
        If Pref.sortorder = "dvd" Then
            RadioButton42.Checked = True
        Else
            RadioButton43.Checked = True
        End If
        CheckBox34                      .Checked    = Pref.copytvactorthumbs

        'TV Autoscrape Options
        cbTvDlPosterArt                 .Checked    = Pref.tvdlposter
        cbTvDlFanart                    .Checked    = Pref.tvdlfanart
        cbTvDlSeasonArt                 .Checked    = Pref.tvdlseasonthumbs
        cbTvDlXtraFanart                .Checked    = Pref.dlTVxtrafanart
        cmbxTvXtraFanartQty.SelectedIndex = cmbxTvXtraFanartQty.FindStringExact(Pref.TvXtraFanartQty.ToString)
        cbTvDlFanartTvArt               .Checked    = Pref.TvDlFanartTvArt
        cbTvFanartTvFirst               .Checked    = Pref.TvFanartTvFirst
        'season-all
        Select Case Pref.seasonall
            Case "none"
                RadioButton41.Checked = True
            Case "poster"
                RadioButton40.Checked = True
            Case "wide"
                RadioButton39.Checked = True
        End Select
        'TV Show Thumbnail
        If Pref.postertype = "poster" Then
            posterbtn.Checked = True
        Else
            bannerbtn.Checked = True
        End If
        'odd Art
        cb_TvFolderJpg                  .Checked    = Pref.tvfolderjpg
        cbSeasonFolderjpg               .Checked    = Pref.seasonfolderjpg

        'TV Ep Renaming
        ComboBox_tv_EpisodeRename.Items.Clear()
        For Each Regex In Pref.tv_RegexRename
            ComboBox_tv_EpisodeRename.Items.Add(Regex)
        Next
        ComboBox_tv_EpisodeRename.SelectedIndex     = If(Pref.tvrename < ComboBox_tv_EpisodeRename.Items.Count, Pref.tvrename, 0)
        CheckBox_tv_EpisodeRenameAuto   .Checked    = Pref.autorenameepisodes
        CheckBox_tv_EpisodeRenameCase   .Checked    = Pref.eprenamelowercase
        
        'Missing Ep Options
        cbTvMissingSpecials             .Checked    = Pref.ignoreMissingSpecials
        cb_TvMissingEpOffset            .Checked    = Pref.TvMissingEpOffset

        'Options
        ComboBox8.SelectedIndex                     = Pref.TvdbActorScrape
        CheckBox_Use_XBMC_TVDB_Scraper  .Checked    = Pref.tvshow_useXBMC_Scraper
        CheckBox20                      .Checked    = Pref.enabletvhdtags
        CheckBox17                      .Checked    = Pref.disabletvlogs
        cbTvQuickAddShow                .Checked    = Pref.tvshowautoquick
        cbTvAutoScreenShot              .Checked    = Pref.autoepisodescreenshot
        cbTvScrShtTVDBResize            .Checked    = Pref.tvscrnshtTVDBResize
        
    End Sub

    Private Sub TVRegexInit()
        lb_tv_RegexScrape.Items.Clear()
        For Each regexc In Pref.tv_RegexScraper
            lb_tv_RegexScrape.Items.Add(regexc)
        Next

        lb_tv_RegexRename.Items.Clear()
        For Each regexc In Pref.tv_RegexRename
            lb_tv_RegexRename.Items.Add(regexc)
        Next
    End Sub

    Private Sub CmdsNProfilesInit()
        'Commands
        lb_CommandTitle.Items.Clear()
        lb_CommandCommand.Items.Clear()
        For Each com In Pref.commandlist
            lb_CommandTitle.Items.Add(com.title)
            lb_CommandCommand.Items.Add(com.command)
        Next
        'Profiles
        For Each prof In Form1.profileStruct.ProfileList
            lb_ProfileList.Items.Add(prof.ProfileName)
        Next
        Label18.Text = "Current Default Profile: " & Form1.profileStruct.DefaultProfile
        Label3.Text = "Current Startup Profile: " & Form1.profileStruct.StartupProfile
    End Sub

#Region "Common Tab"

#Region "Common Settings Tab"

    Private Sub rbXBMCv_pre_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_pre.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_pre.Checked Then
            Pref.XBMC_version = 0
        End If
        Changes = True
    End Sub

    Private Sub rbXBMCv_post_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_post.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_post.Checked Then
            Pref.XBMC_version = 2
        End If
        Changes = True
    End Sub

    Private Sub rbXBMCv_both_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_both.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_both.Checked Then
            Pref.XBMC_version = 1
        End If
        Changes = True
    End Sub



    Private Sub CheckBox38_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox38.CheckedChanged
        If prefsload Then Exit Sub
        Pref.intruntime = CheckBox38.Checked
        Changes = True
    End Sub

    Private Sub cb_IgnoreThe_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreThe.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignorearticle = cb_IgnoreThe.Checked
        Changes = True
    End Sub

    Private Sub cb_IgnoreA_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreA.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignoreAarticle = cb_IgnoreA.Checked
        Changes = True
    End Sub

    Private Sub cb_IgnoreAn_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreAn.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignoreAn = cb_IgnoreAn.Checked
        Changes = True
    End Sub
    
    Private Sub cbOverwriteArtwork_CheckedChanged(sender As Object, e As EventArgs) Handles cbOverwriteArtwork.CheckedChanged
        If prefsload Then Exit Sub
        Pref.overwritethumbs = Not cbOverwriteArtwork.Checked
        Changes = True
    End Sub

    Private Sub cbDisplayRatingOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayRatingOverlay.CheckedChanged
        If prefsload Then Exit Sub
        Pref.DisplayRatingOverlay = cbDisplayRatingOverlay.Checked
        Changes = True
    End Sub

    Private Sub cbDisplayMediaInfoOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayMediaInfoOverlay.CheckedChanged
        If prefsload Then Exit Sub
        Pref.DisplayMediainfoOverlay = cbDisplayMediaInfoOverlay.Checked
        Changes = True
    End Sub

    Private Sub cbDisplayMediaInfoFolderSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayMediaInfoFolderSize.CheckedChanged
        If prefsload Then Exit Sub
        Pref.DisplayMediaInfoFolderSize = cbDisplayMediaInfoFolderSize.Checked
        Changes = True
    End Sub

    Private Sub cbShowAllAudioTracks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowAllAudioTracks.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ShowAllAudioTracks = cbShowAllAudioTracks.Checked
        Changes = True
    End Sub

    Private Sub AutoScrnShtDelay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AutoScrnShtDelay.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If AutoScrnShtDelay.Text <> "" Then
                    e.Handled = True
                Else
                    MsgBox("Please Enter at least 1")
                    AutoScrnShtDelay.Text = "10"
                End If
            End If
            If AutoScrnShtDelay.Text = "" Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                AutoScrnShtDelay.Text = "10"
                Exit Sub
            End If
            If Not IsNumeric(AutoScrnShtDelay.Text) Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                AutoScrnShtDelay.Text = "10"
                Exit Sub
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub AutoScrnShtDelay_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AutoScrnShtDelay.TextChanged
        If prefsload Then Exit Sub
        If IsNumeric(AutoScrnShtDelay.Text) AndAlso Convert.ToInt32(AutoScrnShtDelay.Text)>0 Then
            Pref.ScrShtDelay = Convert.ToInt32(AutoScrnShtDelay.Text)
        Else
            Pref.ScrShtDelay = 10
            AutoScrnShtDelay.Text = "10"
            MsgBox("Please enter a numerical Value that is 1 or more")
        End If
        Changes = True
    End Sub

    Private Sub tbExcludeFolders_Validating( sender As Object,  e As CancelEventArgs) Handles tbExcludeFolders.Validating
        If prefsload Then Exit Sub
        If Pref.ExcludeFolders.Changed(tbExcludeFolders) Then
            Changes = True
        End If
    End Sub

    Private Sub tbExcludeFolders_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbExcludeFolders.TextChanged
        'Preferences.ExcludeFolders = ExcludeFolders.Text
        Changes = True
    End Sub

'Image Resizing
    Private Sub comboActorResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboActorResolutions.SelectedIndexChanged
        Pref.ActorResolutionSI = comboActorResolutions.SelectedIndex
        Changes = True
    End Sub

    Private Sub comboPosterResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboPosterResolutions.SelectedIndexChanged
        Pref.PosterResolutionSI = comboPosterResolutions.SelectedIndex
        Changes = True
    End Sub

    Private Sub comboBackDropResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBackDropResolutions.SelectedIndexChanged
        Pref.BackDropResolutionSI = comboBackDropResolutions.SelectedIndex
        Changes = True
    End Sub

    Private Sub btnCleanFilenameAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnCleanFilenameAdd.Click
        lbCleanFilename.Items.Add(txtCleanFilenameAdd.Text)
        Changes = True
        cleanfilenameprefchanged = True
    End Sub

    Private Sub btnCleanFilenameRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnCleanFilenameRemove.Click
        lbCleanFilename.Items.RemoveAt(lbCleanFilename.SelectedIndex)
        Changes = True
        cleanfilenameprefchanged = True
    End Sub

    Private Sub btnVideoSourceAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnVideoSourceAdd.Click
        If txtVideoSourceAdd.Text <> "" Then        
            lbVideoSource.Items.Add(txtVideoSourceAdd.Text)
            Changes = True
            videosourceprefchanged = True
        End If
    End Sub

    Private Sub btnVideoSourceRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnVideoSourceRemove.Click
        Dim strSelected = lbVideoSource.SelectedItem
        Dim idxSelected = lbVideoSource.SelectedIndex
        Try
            lbVideoSource.Items.RemoveAt(idxSelected)
            Changes = True
            videosourceprefchanged = True
        Catch ex As Exception
        End Try
    End Sub

#End Region 'Common Settings Tab

#Region "Actors Tab"

    Private Sub cb_actorseasy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_actorseasy.CheckedChanged
        If prefsload Then Exit Sub
        Pref.actorseasy = cb_actorseasy.Checked
        Changes = True
    End Sub

    Private Sub cmbx_MovMaxActors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbx_MovMaxActors.SelectedIndexChanged
        Try
            If IsNumeric(cmbx_MovMaxActors.SelectedItem) Then
                Pref.maxactors = Convert.ToInt32(cmbx_MovMaxActors.SelectedItem)
            ElseIf cmbx_MovMaxActors.SelectedItem.ToString.ToLower = "none" Then
                Pref.maxactors = 0
            Else
                Pref.maxactors = 9999
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub saveactorchkbx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveactorchkbx.CheckedChanged
        'If prefsload Then Exit Sub
        If saveactorchkbx.CheckState = CheckState.Checked Then
            Pref.actorsave = True
            localactorpath.Text = Pref.actorsavepath
            xbmcactorpath.Text = Pref.actornetworkpath
            localactorpath.Enabled = True
            xbmcactorpath.Enabled = True
            cb_LocalActorSaveAlpha.Enabled = True
            btn_localactorpathbrowse.Enabled = True
        Else
            Pref.actorsave = False
            localactorpath.Text = ""
            xbmcactorpath.Text = ""
            localactorpath.Enabled = False
            xbmcactorpath.Enabled = False
            cb_LocalActorSaveAlpha.Enabled = False
            btn_localactorpathbrowse.Enabled = False
        End If
        Changes = True
    End Sub

    Private Sub cb_LocalActorSaveAlpha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_LocalActorSaveAlpha.CheckedChanged
        If prefsload Then Exit Sub
        Pref.actorsavealpha = cb_LocalActorSaveAlpha.CheckState
        Changes = True
    End Sub

    Private Sub btn_localactorpathbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_localactorpathbrowse.Click
        Try
            Dim thefoldernames As String
            fb.Description = "Please Select Folder to Save Actor Thumbnails)"
            fb.ShowNewFolderButton = True
            fb.RootFolder = System.Environment.SpecialFolder.Desktop
            fb.SelectedPath = Pref.lastpath
            Tmr.Start()
            If fb.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (fb.SelectedPath)
                localactorpath.Text = thefoldernames
                Pref.lastpath = thefoldernames
                Pref.actorsavepath = thefoldernames
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub localactorpath_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles localactorpath.TextChanged
        If prefsload Then Exit Sub
        Pref.actorsavepath = localactorpath.Text
        Changes = True
    End Sub

    Private Sub xbmcactorpath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xbmcactorpath.TextChanged
        If prefsload Then Exit Sub
        Pref.actornetworkpath = xbmcactorpath.Text
        Changes = True
    End Sub

#End Region 'Actors Tab

#End Region 'Common Tab

#Region "General"

    Private Sub rb_MediaPlayerDefault_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_MediaPlayerDefault.CheckedChanged
        If prefsload Then Exit Sub
        If rb_MediaPlayerDefault.Checked = True Then
            Pref.videomode = 1
        End If
        Changes = True
    End Sub

    Private Sub rb_MediaPlayerWMP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_MediaPlayerWMP.CheckedChanged
        If prefsload Then Exit Sub
        If rb_MediaPlayerWMP.Checked = True Then
            Pref.videomode = 2
        End If
        Changes = True
    End Sub

    Private Sub rb_MediaPlayerUser_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_MediaPlayerUser.CheckedChanged
        If Prefsload Then
            If rb_MediaPlayerUser.Checked AndAlso Not String.IsNullOrEmpty(Pref.selectedvideoplayer) Then
                lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            Else
                lbl_MediaPlayerUser.Text = ""
            End If
            Exit Sub
        End If
        If rb_MediaPlayerUser.Checked = True Then
            Pref.videomode = 4
            btn_MediaPlayerBrowse.Enabled = True
            lbl_MediaPlayerUser.Visible = True
            If Not String.IsNullOrEmpty(Pref.selectedvideoplayer) Then
                lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            Else
                lbl_MediaPlayerUser.Text = ""
            End If
        Else
            lbl_MediaPlayerUser.Visible = False
            btn_MediaPlayerBrowse.Enabled = False
        End If
        Changes = True
    End Sub

    Private Sub btn_MediaPlayerBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_MediaPlayerBrowse.Click
        If prefsload Then Exit Sub
        Try
            Dim filebrowser As New OpenFileDialog
            Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            filebrowser.InitialDirectory = mstrProgramFilesPath
            filebrowser.Filter = "Executable Files|*.exe"
            filebrowser.Title = "Find Executable Of Preferred Media Player"
            If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                Pref.selectedvideoplayer = filebrowser.FileName
                lbl_MediaPlayerUser.Visible = True
                lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            End If
            If prefsload Then Exit Sub
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtbx_minrarsize.KeyPress
        If prefsload Then Exit Sub
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If txtbx_minrarsize.Text <> "" Then
                    e.Handled = True
                Else
                    MsgBox("Please Enter at least 0")
                    txtbx_minrarsize.Text = "8"
                End If
            End If
            If txtbx_minrarsize.Text = "" Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Exit Sub
            End If
            If Not IsNumeric(txtbx_minrarsize.Text) Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Exit Sub
            End If
            Pref.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtbx_minrarsize.TextChanged
        If prefsload Then Exit Sub
        If IsNumeric(txtbx_minrarsize.Text) Then
            Pref.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Else
            Pref.rarsize = 8
            txtbx_minrarsize.Text = "8"
        End If
        
        Changes = True
    End Sub
    
    Private Sub btnFontSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontSelect.Click
        Try
            Dim dlg As FontDialog = New FontDialog()
            Dim res As DialogResult = dlg.ShowDialog()
            If res = Windows.Forms.DialogResult.OK Then
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                Dim fontString As String = tc.ConvertToString(dlg.Font)

                Pref.font = fontString

                Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Pref.font), System.Drawing.Font)

                lbl_FontSample.Font = newFont
                lbl_FontSample.Text = fontString
                Changes = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnFontReset_Click(sender As System.Object, e As System.EventArgs) Handles btnFontReset.Click
        Try
            'Reset Font
            Pref.font = "Times New Roman, 9pt"
            Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
            Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Pref.font), System.Drawing.Font)
            lbl_FontSample.Font = newFont
            lbl_FontSample.Text = "Times New Roman, 9pt"
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMkvMergeGuiPath_Click( sender As Object,  e As EventArgs) Handles btnMkvMergeGuiPath.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        ofd.Filter           = "Executable Files|*.exe"
        ofd.Title            = "Locate mkvmerge GUI (mmg.exe)"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then Pref.MkvMergeGuiPath = ofd.FileName
        Changes = True
    End Sub

    Private Sub llMkvMergeGuiPath_Click( sender As Object,  e As EventArgs) Handles llMkvMergeGuiPath.Click
        Form1.OpenUrl("http://www.downloadbestsoft.com/MKVToolNix.html")
    End Sub

    Private Sub lblaltnfoeditorclear_Click( sender As Object,  e As EventArgs) Handles lblaltnfoeditorclear.Click
        tbaltnfoeditor.Text = ""
        Pref.altnfoeditor = ""
        Changes = True
    End Sub

    Private Sub btnaltnfoeditor_Click( sender As Object,  e As EventArgs) Handles btnaltnfoeditor.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        ofd.Filter           = "Executable Files|*.exe"
        ofd.Title            = "Locate Alternative nfo viewer-editor"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then 
            Pref.altnfoeditor = ofd.FileName
            tbaltnfoeditor.Text = Pref.altnfoeditor 
            Changes = True
        End If
    End Sub

    Private Sub cbExternalbrowser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExternalbrowser.CheckedChanged
        If prefsload Then Exit Sub
        Pref.externalbrowser = cbExternalbrowser.Checked
        btnFindBrowser.Enabled = cbExternalbrowser.Checked
        Changes = True
    End Sub

    Private Sub chkbx_disablecache_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_disablecache.CheckedChanged
        If prefsload Then Exit Sub
        Pref.startupCache = Not chkbx_disablecache.Checked
        Changes = True
    End Sub

    Private Sub cbUseMultipleThreads_CheckedChanged( sender As Object,  e As EventArgs) Handles cbUseMultipleThreads.CheckedChanged
        If prefsload Then Exit Sub
        Pref.UseMultipleThreads = cbUseMultipleThreads.Checked
        If prefsload Then Exit Sub
        Changes = True
    End Sub

    Private Sub cbShowLogOnError_CheckedChanged( sender As Object,  e As EventArgs) Handles cbShowLogOnError.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ShowLogOnError = cbShowLogOnError.Checked
        If prefsload Then Exit Sub
        Changes = True
    End Sub

    Private Sub cbCheckForNewVersion_CheckedChanged( sender As Object,  e As EventArgs) Handles cbCheckForNewVersion.CheckedChanged
        If prefsload Then Exit Sub
        Pref.CheckForNewVersion = cbCheckForNewVersion.Checked
        If prefsload Then Exit Sub
        Changes = True
    End Sub

    Private Sub cbDisplayLocalActor_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbDisplayLocalActor.CheckedChanged
        If prefsload Then Exit Sub
        Pref.LocalActorImage = cbDisplayLocalActor.Checked = True
        Changes = True
    End Sub

    Private Sub cbRenameNFOtoINFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRenameNFOtoINFO.CheckedChanged
        If prefsload Then Exit Sub
        Pref.renamenfofiles = cbRenameNFOtoINFO.Checked
        Changes = True
    End Sub

    Private Sub cbMultiMonitorEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMultiMonitorEnable.CheckedChanged
        If prefsload Then Exit Sub
        Pref.MultiMonitoEnabled = cbMultiMonitorEnable.Checked
        Changes = True
    End Sub
    

#End Region 'General

#Region "Movie Preferences"
#Region "Movie Preferences -> Scraper Tab"

'Choose Default Scraper
    Private Sub CheckBox_Use_XBMC_Scraper_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Use_XBMC_Scraper.CheckedChanged
        Try
            If CheckBox_Use_XBMC_Scraper.CheckState = CheckState.Checked Then
                Pref.movies_useXBMC_Scraper = True
                GroupBox_MovieIMDBMirror.Enabled = False
                GroupBox_MovieIMDBMirror.Visible = False
                GroupBox_TMDB_Scraper_Preferences.Enabled = True
                GroupBox_TMDB_Scraper_Preferences.Visible = True
                GroupBox_TMDB_Scraper_Preferences.BringToFront()
            Else
                Pref.movies_useXBMC_Scraper = False
                GroupBox_MovieIMDBMirror.Enabled = True
                GroupBox_MovieIMDBMirror.Visible = True
                GroupBox_MovieIMDBMirror.BringToFront()
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

'XBMC Scraper Preferences - TMDB
    Private Sub cbXbmcTmdbFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXbmcTmdbFanart.CheckedChanged
        If cbXbmcTmdbFanart.Checked = True Then
            Pref.XbmcTmdbScraperFanart = "true"
        Else
            Pref.XbmcTmdbScraperFanart = "false"
        End If
        Changes = True
        XbmcTMDbScraperChanged = True
    End Sub

    Private Sub cbXbmcTmdbIMDBRatings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXbmcTmdbIMDBRatings.CheckedChanged, cbXbmcTmdbIMDBRatings.CheckStateChanged
        If prefsload Then Exit Sub
        If cbXbmcTmdbIMDBRatings.Checked = True Then
            Pref.XbmcTmdbScraperRatings = "IMDb"
        Else
            Pref.XbmcTmdbScraperRatings = "TMDb"
        End If
        Changes = True
        XbmcTMDbScraperChanged = True
    End Sub

    Private Sub cbXbmcTmdbOutlineFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbOutlineFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbMissingFromImdb = cbXbmcTmdbOutlineFromImdb.Checked 
        Changes = True
    End Sub

    Private Sub cbXbmcTmdbTop250FromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbTop250FromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbTop250FromImdb = cbXbmcTmdbTop250FromImdb.Checked 
        Changes = True
    End Sub

    Private Sub cbXbmcTmdbVotesFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbVotesFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbVotesFromImdb = cbXbmcTmdbVotesFromImdb.Checked
        Changes = True
    End Sub

    Private Sub cbXbmcTmdbStarsFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbStarsFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbStarsFromImdb = cbXbmcTmdbStarsFromImdb.Checked 
        Changes = True
    End Sub

    Private Sub cbXbmcTmdbAkasFromImdb_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbXbmcTmdbAkasFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbAkasFromImdb = cbXbmcTmdbAkasFromImdb.Checked
        Changes = True
    End Sub

    Private Sub cbXbmcTmdbCertFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbCertFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbCertFromImdb = cbXbmcTmdbCertFromImdb.Checked
        Changes = True
    End Sub

    Private Sub cmbxXbmcTmdbHDTrailer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxXbmcTmdbHDTrailer.SelectedIndexChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbScraperTrailerQ = cmbxXbmcTmdbHDTrailer.Text
        Changes = True
        XbmcTMDbScraperChanged = True
    End Sub

    Private Sub cmbxXbmcTmdbTitleLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxXbmcTmdbTitleLanguage.SelectedIndexChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbScraperLanguage = cmbxXbmcTmdbTitleLanguage.Text
        Changes = True
        XbmcTMDbScraperChanged = True
    End Sub

    Private Sub cmbxTMDBPreferredCertCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxTMDBPreferredCertCountry.SelectedIndexChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbScraperCertCountry = cmbxTMDBPreferredCertCountry.Text
        Changes = True
        XbmcTMDbScraperChanged = True
    End Sub

    Private Sub cbXbmcTmdbRename_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbRename.CheckedChanged
        If prefsload Then Exit Sub
        If cbXbmcTmdbRename.CheckState = CheckState.Checked Then
            If Pref.MovieRenameEnable Then
                Pref.XbmcTmdbRenameMovie = True
            Else
                MsgBox("Please also enable 'Rename Movie'")
                cbXbmcTmdbRename.CheckState = CheckState.Unchecked 
            End If
        Else
            Pref.XbmcTmdbRenameMovie = False
        End If
        Changes = True
    End Sub

    Private Sub cbMovNewFolderInRootFolder_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovNewFolderInRootFolder.CheckedChanged
        If prefsload Then Exit Sub
        Pref.MovNewFolderInRootFolder = cbMovNewFolderInRootFolder.checked 
        Changes = True
    End Sub

    Private Sub cbXbmcTmdbActorDL_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbActorDL.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XbmcTmdbActorDL = cbXbmcTmdbActorDL.checked
        Changes = True
    End Sub

'MC Scraper Options
    Private Sub cbImdbgetTMDBActor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImdbgetTMDBActor.CheckedChanged
        If prefsload Then Exit Sub
        Pref.TmdbActorsImdbScrape = cbImdbgetTMDBActor.Checked
        Changes = True
    End Sub 

    Private Sub cbImdbPrimaryPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles cbImdbPrimaryPlot.CheckedChanged 
        If prefsload Then Exit Sub
        Pref.ImdbPrimaryPlot = cbImdbPrimaryPlot.Checked 
        Changes = True
    End Sub

    Private Sub lb_IMDBMirrors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lb_IMDBMirrors.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            Pref.imdbmirror = lb_IMDBMirrors.SelectedItem
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovImdbFirstRunTime_CheckedChanged(sender As Object, e As EventArgs) Handles cbMovImdbFirstRunTime.CheckedChanged
        If prefsload Then Exit Sub
        Pref.MovImdbFirstRunTime = cbMovImdbFirstRunTime.Checked
        Changes = True
    End Sub

'End of "Choose Default Scraper"

'IMDB Scraper Limits
    Private Sub cmbxMovScraper_MaxGenres_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxMovScraper_MaxGenres.SelectedIndexChanged
        Try
            If IsNumeric(cmbxMovScraper_MaxGenres.SelectedItem) Then
                Pref.maxmoviegenre = Convert.ToInt32(cmbxMovScraper_MaxGenres.SelectedItem)
            ElseIf cmbxMovScraper_MaxGenres.SelectedItem.ToString.ToLower = "none" Then
                Pref.maxmoviegenre = 0
            Else
                Pref.maxmoviegenre = 99
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxMovieScraper_MaxStudios_SelectedIndexChanged( sender As Object,  e As EventArgs) Handles cmbxMovieScraper_MaxStudios.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            Pref.MovieScraper_MaxStudios = Convert.ToInt32(cmbxMovieScraper_MaxStudios.SelectedItem)  'nudMovieScraper_MaxStudios.Value
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Other Options
    Private Sub cbGetMovieSetFromTMDb_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbGetMovieSetFromTMDb.CheckedChanged
        If prefsload Then Exit Sub
        Pref.GetMovieSetFromTMDb = cbGetMovieSetFromTMDb.Checked
        Changes = True
    End Sub

    Private Sub chkbOriginal_Title_CheckedChanged( sender As Object,  e As EventArgs) Handles chkbOriginal_Title.CheckedChanged
        If prefsload Then Exit Sub
        Pref.Original_Title = chkbOriginal_Title.Checked
        Changes = True
    End Sub

'Preferred Language
    Private Sub comboBoxTMDbSelectedLanguage_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles comboBoxTMDbSelectedLanguage.SelectedValueChanged
        Pref.TMDbSelectedLanguageName = comboBoxTMDbSelectedLanguage.Text
        Changes = True
    End Sub

    Private Sub cbUseCustomLanguage_Click(sender As System.Object, e As System.EventArgs) Handles cbUseCustomLanguage.Click
        Pref.TMDbUseCustomLanguage = cbUseCustomLanguage.Checked
        SetLanguageControlsState()
        Changes = True
    End Sub

    Private Sub tbCustomLanguageValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbCustomLanguageValue.TextChanged
        Pref.TMDbCustomLanguageValue = tbCustomLanguageValue.Text
        Changes = True
    End Sub

    Private Sub llLanguagesFile_Click(sender As System.Object, e As System.EventArgs) Handles llLanguagesFile.Click
        System.Diagnostics.Process.Start(TMDb.LanguagesFile)
    End Sub

'Individual Movie Folder Options
    Private Sub cbMovieUseFolderNames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieUseFolderNames.CheckedChanged
        Try
            If cbMovieUseFolderNames.CheckState = CheckState.Checked Then
                Pref.usefoldernames = True
                cbMovieAllInFolders.Checked = False
                cbMovCreateFolderjpg.Enabled = True
                cbMovCreateFanartjpg.Enabled = True
                cbDlXtraFanart.Enabled = True
                If Pref.basicsavemode Then
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Enabled = False
                Else
                    cbMovieFanartInFolders.Enabled = True
                    cbMoviePosterInFolder.Enabled = True
                End If
            Else
                Pref.usefoldernames = False
                If Not Pref.allfolders AndAlso Not Pref.basicsavemode Then
                    cbMovCreateFolderjpg.Checked = False
                    cbMovCreateFolderjpg.Enabled = False
                    cbMovCreateFanartjpg.Enabled = False
                    cbMovCreateFanartjpg.Checked = False
                    cbMovieFanartInFolders.Checked = False
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Checked = False
                    cbMoviePosterInFolder.Enabled = False
                    cbDlXtraFanart.Checked = False
                    cbDlXtraFanart.Enabled = False
                ElseIf Not Pref.allfolders AndAlso Pref.basicsavemode Then
                    msgbox("Basic Save option is enabled" & vbCrLf & "Use Folder Name or All Movies in Folders" & vbCrLf & "must be selected!",MsgBoxStyle.Exclamation)
                    cbMovieUseFolderNames.Checked = CheckState.Checked
                End If
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieAllInFolders_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieAllInFolders.CheckedChanged
        If cbMovieAllInFolders.CheckState = CheckState.Checked Then 
            Pref.allfolders = True
            cbMovieUseFolderNames.Checked = False
            cbMovCreateFolderjpg.Enabled = True
            cbMovCreateFanartjpg.Enabled = True
            cbDlXtraFanart.Enabled = True
            If Pref.basicsavemode Then
                cbMovieFanartInFolders.Enabled = False
                cbMoviePosterInFolder.Enabled = False
            Else
                cbMovieFanartInFolders.Enabled = True
                cbMoviePosterInFolder.Enabled = True
            End If               
        Else
            Pref.allfolders = False
            If Not Pref.usefoldernames AndAlso Not Pref.basicsavemode Then
                    
                cbMovCreateFolderjpg.Enabled = False
                cbMovCreateFolderjpg.Checked = False
                cbMovCreateFanartjpg.Enabled = False
                cbMovCreateFanartjpg.Checked = False
                cbMovieFanartInFolders.Checked = False
                cbMovieFanartInFolders.Enabled = False
                cbMoviePosterInFolder.Checked = False
                cbMoviePosterInFolder.Enabled = False
                cbDlXtraFanart.Checked = False
                cbDlXtraFanart.Enabled = False
            ElseIf Not Pref.usefoldernames AndAlso Pref.basicsavemode Then
                msgbox("Basic Save option is enabled" & vbCrLf & "Use Folder Name or All Movies in Folders" & vbCrLf & "must be selected!",MsgBoxStyle.Exclamation)
                cbMovieAllInFolders.Checked = CheckState.Checked
            End If
        End If
        Changes = True
    End Sub

'Basic Movie
    Private Sub cbMovieBasicSave_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieBasicSave.CheckedChanged 
        Try
            If cbMovieBasicSave.CheckState = CheckState.Checked Then
                If Pref.usefoldernames or Pref.allfolders Then
                    Pref.basicsavemode = True
                    cbMovieFanartInFolders.Checked =CheckState.Unchecked
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Checked = CheckState.Unchecked
                    cbMoviePosterInFolder.Enabled = False
                Else
                    If (Not Pref.usefoldernames AndAlso Not Pref.allfolders) Then
                    MsgBox("Either Use Foldername or Movies In Folders" & vbCrLf & "must be selected")
                    End If
                    Pref.basicsavemode = False
                    cbMovieFanartInFolders.Enabled = True 
                    cbMoviePosterInFolder.Enabled = True
                    cbMovieBasicSave.Checked = False
                End If
            Else
                Pref.basicsavemode = False
                cbMovieFanartInFolders.Enabled = True 
                cbMoviePosterInFolder.Enabled = True 
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Keywords As Tags
    Private Sub cb_keywordasTag_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cb_keywordasTag.CheckedChanged
        Try
            If cb_keywordasTag.CheckState = CheckState.Checked Then
                Pref.keywordasTag = True
                If Pref.keywordlimit = 0 Then
                    MsgBox(" Please select a limit above Zero keywords" & vbCrLf & "else no keywords will be stored as Tags")
                End If
            Else
                Pref.keywordasTag = False
            End If
            Changes = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cb_keywordlimit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_keywordlimit.SelectedIndexChanged
        Try
            If IsNumeric(cb_keywordlimit.SelectedItem) Then
                Pref.keywordlimit = Convert.ToInt32(cb_keywordlimit.SelectedItem)
            ElseIf cb_keywordlimit.SelectedItem.ToString.ToLower = "none" Then
                Pref.keywordlimit = 0
            Else
                Pref.keywordlimit = 999
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'IMDB Cert Priority
    Private Sub ScrapeFullCertCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ScrapeFullCertCheckBox.CheckedChanged
        Try
            If ScrapeFullCertCheckBox.Checked Then
                Pref.scrapefullcert = True
            Else
                Pref.scrapefullcert = False
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
    
    Private Sub Button75_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button75.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If lb_IMDBCertPriority.SelectedIndex <> 0 Then
                mSelectedIndex = lb_IMDBCertPriority.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                lb_IMDBCertPriority.Items.Insert(mSelectedIndex + 1, lb_IMDBCertPriority.Items(mOtherIndex))
                lb_IMDBCertPriority.Items.RemoveAt(mOtherIndex)
            End If
            For f = 0 To 33
                Pref.certificatepriority(f) = lb_IMDBCertPriority.Items(f)
            Next
            Changes = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button74_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button74.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If lb_IMDBCertPriority.SelectedIndex <> lb_IMDBCertPriority.Items.Count - 1 Then
                mSelectedIndex = lb_IMDBCertPriority.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                lb_IMDBCertPriority.Items.Insert(mSelectedIndex, lb_IMDBCertPriority.Items(mOtherIndex))
                lb_IMDBCertPriority.Items.RemoveAt(mOtherIndex + 1)
            End If
            For f = 0 To 33
                Pref.certificatepriority(f) = lb_IMDBCertPriority.Items(f)
            Next
            Changes = True
        Catch ex As Exception
        End Try
    End Sub

#End Region  'Movie Preferences -> Scraper Tab

#Region "Movie Preferences -> Artwork Tab"
    
'Scraping Options
    Private Sub cbMoviePosterScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoviePosterScrape.CheckedChanged
        If prefsload Then Exit Sub
        Pref.scrapemovieposters = cbMoviePosterScrape.checked
        Changes = True
    End Sub

    Private Sub cbMovFanartScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovFanartScrape.CheckedChanged
        If prefsload Then Exit Sub
        Pref.savefanart = cbMovFanartScrape.Checked
        Changes = True
    End Sub

    Private Sub cbMovFanartTvScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovFanartTvScrape.CheckedChanged
        If prefsload Then Exit Sub
        Pref.MovFanartTvscrape = cbMovFanartTvScrape.Checked
        Changes = True
    End Sub
    
    Private Sub cbMovFanartNaming_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovFanartNaming.CheckedChanged
        If prefsload Then Exit Sub
        Pref.MovFanartNaming = cbMovFanartNaming.Checked
        Changes = True
    End Sub

    Private Sub btnMovFanartTvSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovFanartTvSelect.Click
        Dim frm As New frmFanartTvArtSelect
        frm.Init()
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK AndAlso frm.IsChanged Then
            Changes = True
        End If
        frm.Dispose()
    End Sub

    Private Sub cbDlXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbDlXtraFanart.CheckedChanged
        If prefsload Then Exit Sub
        If cbDlXtraFanart.Checked Then
            If Not Pref.allfolders AndAlso Not Pref.usefoldernames Then
                MsgBox("Please select ""Use Foldername"" or ""Movies in Separate Folders""")
                cbDlXtraFanart.Checked = False
            Else
                If Not Pref.movxtrafanart AndAlso Not Pref.movxtrathumb Then
                    MsgBox("Please select ""Allow save ExtraThumbs"" And/Or ""Allow save ExtraFanart""")
                    cbDlXtraFanart.Checked = False
                Else 
                    Pref.dlxtrafanart = True
                End If
            End If
        Else
            Pref.dlxtrafanart = False
        End If
        Changes = True
    End Sub

'Movie Scraper Poster Priority
    Private Sub btnMovPosterPriorityUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovPosterPriorityUp.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.lbPosterSourcePriorities.SelectedIndex <> 0 Then
                mSelectedIndex = Me.lbPosterSourcePriorities.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                lbPosterSourcePriorities.Items.Insert(mSelectedIndex + 1, lbPosterSourcePriorities.Items(mOtherIndex))
                lbPosterSourcePriorities.Items.RemoveAt(mOtherIndex)
            End If
            Dim mothpr As Integer = lbPosterSourcePriorities.Items.Count -1
            Pref.moviethumbpriority.Clear()
            For f = 0 To mothpr
                Pref.moviethumbpriority.Add(lbPosterSourcePriorities.Items(f).ToString)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovPosterPriorityDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovPosterPriorityDown.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.lbPosterSourcePriorities.SelectedIndex <> - 1 Then
                mSelectedIndex = Me.lbPosterSourcePriorities.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                lbPosterSourcePriorities.Items.Insert(mSelectedIndex, lbPosterSourcePriorities.Items(mOtherIndex))
                lbPosterSourcePriorities.Items.RemoveAt(mOtherIndex + 1)
            End If
            Dim mothpr As Integer = lbPosterSourcePriorities.Items.Count - 1
            Pref.moviethumbpriority.Clear()
            For f = 0 To mothpr
                Pref.moviethumbpriority.Add(lbPosterSourcePriorities.Items(f).ToString)
            Next
            Changes = True
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btn_MovPosterPriorityReset_Click( sender As System.Object,  e As System.EventArgs) Handles btn_MovPosterPriorityReset.Click
        Pref.resetmovthumblist()
        'If lbPosterSourcePriorities.Items.Count <> Pref.moviethumbpriority.Count Then
            lbPosterSourcePriorities.Items.Clear()
            For f = 0 To Pref.moviethumbpriority.Count-1
                lbPosterSourcePriorities.Items.Add(Pref.moviethumbpriority(f))
            Next
        'End If
        Changes = True
    End Sub

    Private Sub btn_MovPosterPriorityRemove_Click( sender As System.Object,  e As System.EventArgs) Handles btn_MovPosterPriorityRemove.Click
        Try
            Dim selected As Integer = Me.lbPosterSourcePriorities.SelectedIndex
            If selected = -1 Then Exit Sub
            Me.lbPosterSourcePriorities.Items.RemoveAt(selected)
            Dim mothpr As Integer = lbPosterSourcePriorities.Items.Count -1
            Pref.moviethumbpriority.Clear()
            For f = 0 To mothpr
                Pref.moviethumbpriority.Add(lbPosterSourcePriorities.Items(f).ToString)
            Next
            Changes = True
        Catch ex As Exception
        End Try
    End Sub

'Movie's in folders
    Private Sub cbMovXtraThumbs_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovXtraThumbs.CheckedChanged
        If prefsload Then Exit Sub
        If cbMovXtraThumbs.Checked Then
            Pref.movxtrathumb = True
        Else
            If Not cbMovXtraFanart.Checked Then
                cbDlXtraFanart.Checked = False
                MsgBox("Disabled ""Download Extra Fanart/Thumbs"" as either " & vbCrLf & "Extra Fanart or Extra Thumbs" & vbCrLf & "         must be checked")
            End If
            Pref.movxtrathumb = False
        End If
        Changes = True
    End Sub

    Private Sub cbMovXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovXtraFanart.CheckedChanged
        If prefsload Then Exit Sub
        If cbMovXtraFanart.Checked Then
            Pref.movxtrafanart = True
        Else
            If Not cbMovXtraThumbs.Checked Then
                cbDlXtraFanart.Checked = False
                MsgBox("Disabled ""Download Extra Fanart/Thumbs"" as either " & vbCrLf & "Extra Fanart or Extra Thumbs" & vbCrLf & "         must be checked")
            End If
            Pref.movxtrafanart = False
        End If
        Changes = True
    End Sub

    Private Sub cmbxMovXtraFanartQty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxMovXtraFanartQty.SelectedIndexChanged
        If prefsload Then Exit Sub
        Dim newvalue As String = cmbxMovXtraFanartQty.SelectedItem
        Pref.movxtrafanartqty = newvalue.toint
        Changes = True
    End Sub

    Private Sub cbMoviePosterInFolder_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMoviePosterInFolder.CheckedChanged
        If cbMoviePosterInFolder.CheckState = CheckState.Checked Then
            If Pref.usefoldernames or Pref.allfolders Then
                Pref.posterjpg = True
            Else 
                Pref.posterjpg = False
                cbMoviePosterInFolder.Checked = False
                MsgBox("Either Use Foldername or All Movies in Folders not selected!")
            End If
        Else
            Pref.posterjpg = False
        End If
        Changes = True
    End Sub

    Private Sub cbMovieFanartInFolders_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovieFanartInFolders.CheckedChanged
        If cbMovieFanartInFolders.CheckState = CheckState.Checked Then
            If Pref.usefoldernames or Pref.allfolders Then
                Pref.fanartjpg = True
            Else 
                Pref.fanartjpg = False
                cbMovieFanartInFolders.Checked = False
                MsgBox("Either Use Foldername or All Movies in Folders not selected!")
            End If
        Else
            Pref.fanartjpg = False
        End If
        Changes = True
    End Sub

    Private Sub cbMovCreateFolderjpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovCreateFolderjpg.CheckedChanged 
        If cbMovCreateFolderjpg.CheckState = CheckState.Checked and (Pref.usefoldernames or Pref.allfolders) Then
            Pref.createfolderjpg = True
        Else
            Pref.createfolderjpg = False
        End If
        Changes = True
    End Sub

    Private Sub cbMovCreateFanartjpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovCreateFanartjpg.CheckedChanged 
        If cbMovCreateFanartjpg.CheckState = CheckState.Checked and (Pref.usefoldernames or Pref.allfolders) Then
            Pref.createfanartjpg = True
        Else
            Pref.createfanartjpg = False
        End If
        Changes = True
    End Sub
    

'Movie Set Artwork
    Private Sub cbMovSetArtScrape_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovSetArtScrape.CheckedChanged
        If prefsload Then Exit Sub
        Pref.dlMovSetArtwork = cbMovSetArtScrape.Checked 
        Changes = True
    End Sub

    Private Sub rbMovSetCentralFolder_CheckedChanged( sender As Object,  e As EventArgs) Handles rbMovSetArtSetFolder.CheckedChanged, rbMovSetFolder.CheckedChanged 
        If prefsload Then Exit Sub
        Pref.MovSetArtSetFolder = rbMovSetArtSetFolder.checked
        btnMovSetCentralFolderSelect.Enabled = rbMovSetArtSetFolder.Checked
        Changes = True
    End Sub

    Private Sub btnMovSetCentralFolderSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovSetCentralFolderSelect.Click
        Try
            Dim thefoldernames As String
            fb.Description = "Please Select Folder to Save All Collection Artwork"
            fb.ShowNewFolderButton = True
            fb.RootFolder = System.Environment.SpecialFolder.Desktop
            fb.SelectedPath = Pref.lastpath
            Tmr.Start()
            If fb.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (fb.SelectedPath)
                tbMovSetArtCentralFolder.Text = thefoldernames
                Pref.MovSetArtCentralFolder = thefoldernames
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
#End Region  'Movie Preferences -> Artwork Tab

#Region "Movie Preferences - General Tab"
'General Options Settings
    Private Sub cbMovieTrailerUrl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerUrl.CheckedChanged
        Try
            If cbMovieTrailerUrl.CheckState = CheckState.Checked Then
                Pref.gettrailer = True
            Else
                If cbDlTrailerDuringScrape.CheckState = CheckState.Checked Then
                    cbMovieTrailerUrl.CheckState = CheckState.Checked 
                Else
                    Pref.gettrailer = False
                End If
            End If
            Changes = True
            
        Catch
        End Try
    End Sub

    Private Sub cbDlTrailerDuringScrape_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbDlTrailerDuringScrape.CheckedChanged
        Try
            If cbDlTrailerDuringScrape.CheckState = CheckState.Checked Then
                Pref.DownloadTrailerDuringScrape = True
                cbMovieTrailerUrl.CheckState = CheckState.Checked 
            Else 
                Pref.DownloadTrailerDuringScrape = False
            End If
            Changes = True
            
        Catch
        End Try
    End Sub

    Private Sub cbPreferredTrailerResolution_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPreferredTrailerResolution.SelectedIndexChanged
        Pref.moviePreferredTrailerResolution = cbPreferredTrailerResolution.Text.ToUpper()
        Changes = True
        
    End Sub

    Private Sub cbMovTitleCase_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovTitleCase.CheckedChanged
        Pref.MovTitleCase = cbMovTitleCase.Checked
        Changes = True
        
    End Sub

    Private Sub cbExcludeMpaaRated_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbExcludeMpaaRated.CheckedChanged
        Pref.ExcludeMpaaRated = cbExcludeMpaaRated.Checked
        Changes = True
        
    End Sub

    Private Sub cbMovThousSeparator_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovThousSeparator.CheckedChanged
        Pref.MovThousSeparator = cbMovThousSeparator.Checked
        Changes = True
        
    End Sub

    Private Sub cbNoAltTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNoAltTitle.CheckedChanged
        If cbNoAltTitle.CheckState = CheckState.Checked Then
            Pref.NoAltTitle = True
        Else
            Pref.NoAltTitle = False
        End If
        Changes = True
        
    End Sub

    Private Sub cbXtraFrodoUrls_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXtraFrodoUrls.CheckedChanged
        Pref.XtraFrodoUrls = Not cbXtraFrodoUrls.Checked 
        Changes = True
        
    End Sub

    Private Sub cb_MovDisplayLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_MovDisplayLog.CheckedChanged
        Try
            If cb_MovDisplayLog.CheckState = CheckState.Checked Then
                Pref.disablelogfiles = False
            Else
                Pref.disablelogfiles = True
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cb_EnableMediaTags_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_EnableMediaTags.CheckedChanged
        Try
            Form1.displayRuntimeScraper = True
            If cb_EnableMediaTags.CheckState = CheckState.Checked Then
                Pref.enablehdtags = True
                PanelDisplayRuntime.Enabled = True
                If Pref.movieRuntimeDisplay = "file" Then
                    rbRuntimeFile.Checked = True
                    Form1.displayRuntimeScraper = False
                Else
                    rbRuntimeScraper.Checked = True
                End If
            Else
                Pref.enablehdtags = False
                PanelDisplayRuntime.Enabled = False
                rbRuntimeScraper.Checked = True
            End If
            Call Form1.mov_SwitchRuntime()
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cb_MovDurationAsRuntine_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_MovDurationAsRuntine.CheckedChanged
        Pref.MovDurationAsRuntine = cb_MovDurationAsRuntine.Checked
        Changes = True
        
    End Sub

    Private Sub rbRuntimeScraper_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbRuntimeScraper.CheckedChanged
        Try
            If rbRuntimeScraper.Checked = True Then
                Pref.movieRuntimeDisplay = "scraper"
                Form1.displayRuntimeScraper = True
            Else
                Pref.movieRuntimeDisplay = "file"
                Form1.displayRuntimeScraper = False
            End If

            cbMovieRuntimeFallbackToFile.Enabled = rbRuntimeScraper.Checked

            'Call mov_SwitchRuntime() 'Damn it - this call prevents MC starting, and I have no idea why! HueyHQ
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieRuntimeFallbackToFile_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieRuntimeFallbackToFile.CheckedChanged
        Pref.movieRuntimeFallbackToFile = cbMovieRuntimeFallbackToFile.Checked
        Changes = True
        
    End Sub

    Private Sub TextBox_OfflineDVDTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_OfflineDVDTitle.TextChanged
        Try
            Pref.OfflineDVDTitle = TextBox_OfflineDVDTitle.Text
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tbDateFormat_TextChanged( sender As System.Object,  e As System.EventArgs) Handles tbDateFormat.TextChanged
        If PrefsLoad Then
            Try
                Pref.DateFormat = tbDateFormat.Text
                Changes = True
                
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieList_ShowColPlot_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovieList_ShowColPlot.CheckedChanged
        If PrefsLoad Then
            Try
                Pref.MovieList_ShowColPlot = cbMovieList_ShowColPlot.Checked
                Changes = True
                
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieList_ShowColWatched_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovieList_ShowColWatched.CheckedChanged
         If PrefsLoad Then
            Try
                Pref.MovieList_ShowColWatched = cbMovieList_ShowColWatched.Checked
                Changes = True
                
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieShowDateOnList_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieShowDateOnList.CheckedChanged
        Try
            If cbMovieShowDateOnList.Checked = True Then
                Pref.showsortdate = True
            Else
                Pref.showsortdate = False
            End If
            Call Mc.clsGridViewMovie.mov_FiltersAndSortApply(Form1)
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMissingMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMissingMovie.CheckedChanged
        Try
            If cbMissingMovie.CheckState = CheckState.Checked Then
                Pref.incmissingmovies = True 
            Else
                Pref.incmissingmovies = False 
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cb_SorttitleIgnoreArticles_CheckedChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cb_SorttitleIgnoreArticles.CheckedChanged
        If prefsload Then Exit Sub
        Pref.sorttitleignorearticle = cb_SorttitleIgnoreArticles.Checked
        Changes = True
    End Sub

    Private Sub cbMoviePartsIgnorePart_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMoviePartsIgnorePart.CheckedChanged 
        Try
            If cbMoviePartsIgnorePart.Checked = True Then
                Pref.movieignorepart = True
            Else
                Pref.movieignorepart = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMoviePartsNameMode_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMoviePartsNameMode.CheckedChanged
        If cbMoviePartsNameMode.Checked Then
            Pref.namemode = "1"
        Else
            Pref.namemode = "0"
        End If
        lblNameMode.Text = Form1.createNameModeText()
        Changes = True
        
    End Sub

'Rename Movie Settings
    Private Sub tb_MovieRenameTemplate_TextChanged(sender As System.Object, e As System.EventArgs) Handles tb_MovieRenameTemplate.TextChanged
        Try
            Pref.MovieRenameTemplate = tb_MovieRenameTemplate.Text
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tb_MovFolderRename_TextChanged(sender As System.Object, e As System.EventArgs) Handles tb_MovFolderRename.TextChanged
        Try
            Pref.MovFolderRenameTemplate = tb_MovFolderRename.Text
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieRenameEnable_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieRenameEnable.CheckedChanged
        Try
            If cbMovieRenameEnable.CheckState = CheckState.Checked Then
                Pref.MovieRenameEnable = True
            Else
                Pref.MovieRenameEnable = False
                'Preferences.XbmcTmdbRenameMovie = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovFolderRename_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovFolderRename.CheckedChanged
        Try
            If cbMovFolderRename.CheckState = CheckState.Checked Then
                Pref.MovFolderRename = True
            Else
                Pref.MovFolderRename = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbRenameUnderscore_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbRenameUnderscore.CheckedChanged
        Try
            If cbRenameUnderscore.CheckState = CheckState.Checked Then
                Pref.MovRenameSpaceCharacter = True
            Else
                Pref.MovRenameSpaceCharacter = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub rbRenameUnderscore_CheckedChanged( sender As Object,  e As EventArgs) Handles rbRenameUnderscore.CheckedChanged
        If prefsload Then Exit Sub
        If rbRenameUnderscore.Checked = True Then
            Pref.RenameSpaceCharacter = "_"
        Else
            Pref.RenameSpaceCharacter = "."
        End If
        Changes = True
        
    End Sub

    Private Sub cbMovSetIgnArticle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovSetIgnArticle.CheckedChanged
        Try
            If cbMovSetIgnArticle.CheckState = CheckState.Checked Then
                Pref.MovSetIgnArticle = True
            Else
                Pref.MovSetIgnArticle = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovTitleIgnArticle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovTitleIgnArticle.CheckedChanged
        Try
            If cbMovTitleIgnArticle.CheckState = CheckState.Checked Then
                Pref.MovTitleIgnArticle = True
            Else
                Pref.MovTitleIgnArticle = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovSortIgnArticle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovSortIgnArticle.CheckedChanged
        Try
            If cbMovSortIgnArticle.CheckState = CheckState.Checked Then
                Pref.MovSortIgnArticle = True
            Else
                Pref.MovSortIgnArticle = False
            End If
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieManualRename_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieManualRename.CheckedChanged
        If PrefsLoad Then Exit Sub
        Pref.MovieManualRename = cbMovieManualRename.Checked
        Changes = True
    End Sub


'Movie Filters settings
    Private Sub nudActorsFilterMinFilms_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudActorsFilterMinFilms.ValueChanged
        If PrefsLoad Then
            Try
                Pref.ActorsFilterMinFilms = nudActorsFilterMinFilms.Value
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudMaxActorsInFilter_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudMaxActorsInFilter.ValueChanged
        If PrefsLoad Then
            Try
                Pref.MaxActorsInFilter = nudMaxActorsInFilter.Value
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieFilters_Actors_Order_SelectedValueChanged( sender As Object,  e As EventArgs) Handles cbMovieFilters_Actors_Order.SelectedValueChanged
        If PrefsLoad Then
            Try
                Pref.MovieFilters_Actors_Order = cbMovieFilters_Actors_Order.SelectedIndex
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudDirectorsFilterMinFilms_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudDirectorsFilterMinFilms.ValueChanged
        If PrefsLoad Then
            Try
                Pref.DirectorsFilterMinFilms = nudDirectorsFilterMinFilms.Value
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudMaxDirectorsInFilter_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudMaxDirectorsInFilter.ValueChanged
        If PrefsLoad Then
            Try
                Pref.MaxDirectorsInFilter = nudMaxDirectorsInFilter.Value
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieFilters_Directors_Order_SelectedValueChanged( sender As Object,  e As EventArgs) Handles cbMovieFilters_Directors_Order.SelectedValueChanged
        If PrefsLoad Then
            Try
                Pref.MovieFilters_Directors_Order = cbMovieFilters_Directors_Order.SelectedIndex
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudSetsFilterMinFilms_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudSetsFilterMinFilms.ValueChanged
        If PrefsLoad Then
            Try
                Pref.SetsFilterMinFilms = nudSetsFilterMinFilms.Value
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudMaxSetsInFilter_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudMaxSetsInFilter.ValueChanged
        If PrefsLoad Then
            Try
                Pref.MaxSetsInFilter = nudMaxSetsInFilter.Value
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieFilters_Sets_Order_SelectedValueChanged( sender As Object,  e As EventArgs) Handles cbMovieFilters_Sets_Order.SelectedValueChanged
        If PrefsLoad Then
            Try
                Pref.MovieFilters_Sets_Order = cbMovieFilters_Sets_Order.SelectedIndex
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbDisableNotMatchingRenamePattern_CheckedChanged( sender As Object,  e As EventArgs) Handles cbDisableNotMatchingRenamePattern.CheckedChanged
        If PrefsLoad Then
            Try
                Pref.DisableNotMatchingRenamePattern = cbDisableNotMatchingRenamePattern.Checked
                Changes = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub



#End Region   'Movie Preferences - General Tab

#Region "Movie Preferences - Advanced Tab"
    'Separate Movie Identifier
    Private Sub btn_MovSepAdd_Click(sender As System.Object, e As System.EventArgs) Handles btn_MovSepAdd.Click
        If tb_MovSeptb.Text <> "" Then
            lb_MovSepLst.Items.Add(tb_MovSeptb.Text)
            Pref.MovSepLst.Add(tb_MovSeptb.Text)
            tb_MovSeptb.Text = ""
            Changes = True
            videosourceprefchanged = True
        End If
    End Sub

    Private Sub btn_MovSepRem_Click(sender As System.Object, e As System.EventArgs) Handles btn_MovSepRem.Click
        lb_MovSepLst.Items.RemoveAt(lb_MovSepLst.SelectedIndex)
        Pref.MovSepLst.Clear()
        For Each t In lb_MovSepLst.Items
            Pref.MovSepLst.Add(t)
        Next
        Changes = True
        'videosourceprefchanged = True
    End Sub

    Private Sub btn_MovSepReset_Click(sender As System.Object, e As System.EventArgs) Handles btn_MovSepReset.Click
        Pref.ResetMovSepLst()
        lb_MovSepLst.Items.Clear()
        For Each it In Pref.MovSepLst
            lb_MovSepLst.Items.Add(it)
        Next
        Changes = True
    End Sub

    'nfoPoster Options
    Private Sub IMPA_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPA_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mpdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mpdb_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tmdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmdb_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub imdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imdb_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


#End Region  'Movie Preferences - Advanced Tab

#End Region 'Movie Preferences

#Region "TV Preferences"

#Region "TV Scraping and options"
'XBMC TVDB Scraper options
    Private Sub rbXBMCTvdbDVDOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbXBMCTvdbDVDOrder.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XBMCTVDbDvdOrder = rbXBMCTvdbDVDOrder.Checked
        Pref.XBMCTVDbAbsoluteNumber = Not rbXBMCTvdbDVDOrder.Checked
        Changes = True
        XbmcTvdbScraperChanged = True
    End Sub

    Private Sub cbXBMCTvdbFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXBMCTvdbFanart.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XBMCTVDbFanart = cbXBMCTvdbFanart.Checked
        Changes = True
        XbmcTvdbScraperChanged = True
    End Sub

    Private Sub cbXBMCTvdbPosters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXBMCTvdbPosters.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XBMCTVDbPoster = cbXBMCTvdbPosters.Checked
        Changes = True
        XbmcTvdbScraperChanged = True
    End Sub

    Private Sub ComboBox_TVDB_Language_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_TVDB_Language.SelectedIndexChanged
        If prefsload Then Exit Sub
        Pref.XBMCTVDbLanguage = ComboBox_TVDB_Language.Text
        Changes = True
        XbmcTvdbScraperChanged = True
    End Sub

    Private Sub cbXBMCTvdbRatingImdb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXBMCTvdbRatingImdb.CheckedChanged
        If prefsload Then Exit Sub
        cbXBMCTvdbRatingFallback.Enabled = cbXBMCTvdbRatingImdb.Checked
        If cbXBMCTvdbRatingImdb.Checked = True Then
            Pref.XBMCTVDbRatings = "IMDb"
        Else
            Pref.XBMCTVDbRatings = "TheTVDB"
        End If
        Changes = True
        XbmcTvdbScraperChanged = True
    End Sub

    Private Sub cbXBMCTvdbRatingFallback_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXBMCTvdbRatingFallback.CheckedChanged
        If prefsload Then Exit Sub
        Pref.XBMCTVDbfallback = cbXBMCTvdbRatingFallback.Checked
        Changes = True
        XbmcTvdbScraperChanged = True
    End Sub

'Endof - XBMC TVDB Scraper options

    Private Sub Button91_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button91.Click
        Try
            ListBox12.Items.Clear()
            Form1.languageList.Clear()
            Form1.util_LanguageListLoad()
            For Each lan In Form1.languageList
                ListBox12.Items.Add(lan.Language.Value)
                'ListBox1.Items.Add(lan.Language.Value)
            Next
            Try
                ListBox12.SelectedItem = Pref.TvdbLanguage
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox12_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox12.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            For Each lan In Form1.languageList
                If lan.Language.Value = ListBox12.SelectedItem Then
                    Pref.TvdbLanguage = lan.Language.Value
                    Pref.TvdbLanguageCode = lan.Abbreviation.Value
                    Exit For
                End If
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton42_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton42.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If RadioButton42.Checked = True Then
                Pref.sortorder = "dvd"
            Else
                Pref.sortorder = "default"
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    'Private Sub RadioButton43_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton43.CheckedChanged
    '    Try
    '        If RadioButton43.Checked = True Then
    '            Pref.sortorder = "default"
    '        Else
    '            Pref.sortorder = "dvd"
    '        End If
    '        Changes = True
    '        
    '    Catch ex As Exception
    '        ExceptionHandler.LogError(ex)
    '    End Try
    'End Sub

    Private Sub CheckBox34_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox34.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If CheckBox34.Checked = True Then
                Pref.copytvactorthumbs = True
            Else
                Pref.copytvactorthumbs = False
            End If
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox_Use_XBMC_TVDB_Scraper_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Use_XBMC_TVDB_Scraper.CheckedChanged
        Try
            If CheckBox_Use_XBMC_TVDB_Scraper.CheckState = CheckState.Checked Then
                Pref.tvshow_useXBMC_Scraper = True
                'GroupBox2.Enabled = False
                'GroupBox3.Enabled = False
                'GroupBox5.Enabled = False
                GroupBox22.Visible = False
                GroupBox22.SendToBack()
                GroupBox_TVDB_Scraper_Preferences.Visible = True
                GroupBox_TVDB_Scraper_Preferences.BringToFront()
            Else
                Pref.tvshow_useXBMC_Scraper = False
                'GroupBox2.Enabled = True
                'GroupBox3.Enabled = True
                'GroupBox5.Enabled = True
                GroupBox22.Visible = True
                GroupBox22.BringToFront()
                GroupBox_TVDB_Scraper_Preferences.Visible = False
                GroupBox_TVDB_Scraper_Preferences.SendToBack()
            End If
            'Read_XBMC_TVDB_Scraper_Config()
            Changes = True
            
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'TvShow Auto Scrape Options
    Private Sub cbTvDlPosterArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlPosterArt.CheckedChanged
        If prefsload Then Exit Sub
        Pref.tvdlposter = cbTvDlPosterArt.Checked
        Changes = True
    End Sub

    Private Sub cbTvDlFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlFanart.CheckedChanged
        If prefsload Then Exit Sub
        Pref.tvdlfanart = cbTvDlFanart.Checked
        Changes = True
    End Sub

    Private Sub cbTvDlSeasonArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlSeasonArt.CheckedChanged
        If prefsload Then Exit Sub
        Pref.tvdlseasonthumbs = cbTvDlSeasonArt.Checked
        Changes = True
    End Sub

    Private Sub cbTvDlXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbTvDlXtraFanart.CheckedChanged
        If prefsload Then Exit Sub
        Pref.dlTVxtrafanart = cbTvDlXtraFanart.Checked
        Changes = True
    End Sub

    Private Sub cmbxTvXtraFanartQty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxTvXtraFanartQty.SelectedIndexChanged
        If prefsload Then Exit Sub
        Dim newvalue As String = cmbxTvXtraFanartQty.SelectedItem
        Pref.TvXtraFanartQty = newvalue.toint
        Changes = True
    End Sub

    Private Sub cbTvDlFanartTvArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlFanartTvArt.CheckedChanged
        If prefsload Then Exit Sub
        Pref.TvDlFanartTvArt = cbTvDlFanartTvArt.Checked
        Changes = True
    End Sub

    Private Sub cbTvFanartTvFirst_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvFanartTvFirst.CheckedChanged
        If prefsload Then Exit Sub
        Pref.TvFanartTvFirst = cbTvFanartTvFirst.Checked
        Changes = True
    End Sub

    Private Sub RadioButton41_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton41.CheckedChanged
        If prefsload Then Exit Sub
        If RadioButton41.Checked = True Then Pref.seasonall = "none"
        Changes = True
    End Sub

    Private Sub RadioButton40_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton40.CheckedChanged
        If prefsload Then Exit Sub
        If RadioButton40.Checked = True Then Pref.seasonall = "poster"
        Changes = True
    End Sub

    Private Sub RadioButton39_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton39.CheckedChanged
        If prefsload Then Exit Sub
        If RadioButton39.Checked = True Then Pref.seasonall = "wide"
        Changes = True
    End Sub

    Private Sub posterbtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles posterbtn.CheckedChanged
        If prefsload Then Exit Sub
        If posterbtn.Checked = True Then Pref.postertype = "poster"
        Changes = True
    End Sub

    Private Sub bannerbtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bannerbtn.CheckedChanged
        If prefsload Then Exit Sub
        If bannerbtn.Checked = True Then Pref.postertype = "banner"
        Changes = True
    End Sub

    Private Sub cb_TvFolderJpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_TvFolderJpg.CheckedChanged
        If prefsload Then Exit Sub
        Pref.tvfolderjpg = cb_TvFolderJpg.Checked
        Changes = True
    End Sub

    Private Sub cbSeasonFolderjpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeasonFolderjpg.CheckedChanged
        If prefsload Then Exit Sub
        Pref.seasonfolderjpg = cbSeasonFolderjpg.checked
        Changes = True
    End Sub

'End Of - TvShow Auto Scrape Options

    Private Sub cbTvQuickAddShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvQuickAddShow.CheckedChanged
        If prefsload Then Exit Sub
        Pref.tvshowautoquick =cbTvQuickAddShow.Checked 
        Changes = True
    End Sub

    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        If prefsload Then Exit Sub
        Pref.enabletvhdtags =CheckBox20.Checked
        Changes = True
    End Sub

    Private Sub cbTvAutoScreenShot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvAutoScreenShot.CheckedChanged
        If prefsload Then Exit Sub
        Pref.autoepisodescreenshot = cbTvAutoScreenShot.Checked
        Changes = True
    End Sub

    Private Sub cbTvScrShtTVDBResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvScrShtTVDBResize.CheckedChanged
        If prefsload Then Exit Sub
        Pref.tvscrnshtTVDBResize = cbTvScrShtTVDBResize.checked
        Changes = True
    End Sub

    'Private Sub AutoScrnShtDelay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AutoScrnShtDelay.KeyPress
    '        Try
    '            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
    '                If AutoScrnShtDelay.Text <> "" Then
    '                    e.Handled = True
    '                Else
    '                    MsgBox("Please Enter at least 1")
    '                    AutoScrnShtDelay.Text = "10"
    '                End If
    '            End If
    '            If AutoScrnShtDelay.Text = "" Then
    '                MsgBox("Please enter a numerical Value that is 1 or more")
    '                AutoScrnShtDelay.Text = "10"
    '                Exit Sub
    '            End If
    '            If Not IsNumeric(AutoScrnShtDelay.Text) Then
    '                MsgBox("Please enter a numerical Value that is 1 or more")
    '                AutoScrnShtDelay.Text = "10"
    '                Exit Sub
    '            End If
    '            'Preferences.ScrShtDelay = Convert.ToInt32(AutoScrnShtDelay.Text)
    '        Catch ex As Exception
    '            ExceptionHandler.LogError(ex)
    '        End Try

    'End Sub

    'Private Sub AutoScrnShtDelay_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoScrnShtDelay.TextChanged
    '    If prefsload Then Exit Sub
    '    If IsNumeric(AutoScrnShtDelay.Text) AndAlso Convert.ToInt32(AutoScrnShtDelay.Text)>0 Then
    '        Pref.ScrShtDelay = Convert.ToInt32(AutoScrnShtDelay.Text)
    '    Else
    '        Pref.ScrShtDelay = 10
    '        AutoScrnShtDelay.Text = "10"
    '        MsgBox("Please enter a numerical Value that is 1 or more")
    '    End If
    '    If Changes = False Then
    '        Changes = True
            
    '    End If
    'End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        If prefsload Then Exit Sub
        Pref.disabletvlogs = CheckBox17.Checked
        Changes = True
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            Pref.TvdbActorScrape = ComboBox8.SelectedIndex.ToString
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBox_tv_EpisodeRename_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_tv_EpisodeRename.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            If Renamer.setRenamePref(Pref.tv_RegexRename.Item(ComboBox_tv_EpisodeRename.SelectedIndex), Pref.tv_RegexScraper) Then
                Pref.tvrename = ComboBox_tv_EpisodeRename.SelectedIndex
                Changes = True
                
            Else
                MsgBox("Format does not match scraper regex" & vbCrLf & "Please check")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox_tv_EpisodeRenameAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_tv_EpisodeRenameAuto.CheckedChanged
        If prefsload Then Exit Sub
        Pref.autorenameepisodes = CheckBox_tv_EpisodeRenameAuto.Checked 
        Changes = True
    End Sub

    Private Sub CheckBox_tv_EpisodeRenameCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_tv_EpisodeRenameCase.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If CheckBox_tv_EpisodeRenameCase.CheckState = CheckState.Checked Then
                Pref.eprenamelowercase = True
            Else
                Pref.eprenamelowercase = False
            End If
            Renamer.applySeasonEpisodeCase()
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvMissingSpecials_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvMissingSpecials.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignoreMissingSpecials = cbTvMissingSpecials.Checked 
        Changes = True
    End Sub

    Private Sub cb_TvMissingEpOffset_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_TvMissingEpOffset.CheckedChanged
        If prefsload Then Exit Sub
        Pref.TvMissingEpOffset = cb_TvMissingEpOffset.Checked 
        Changes = True
    End Sub

    Private Sub cbTv_fixNFOid_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbTv_fixNFOid.CheckedChanged
        Pref.fixnfoid =cbTv_fixNFOid.Checked 
    End Sub



#End Region  'TV Scraping and options

#Region "TV Regex"

    Private Sub lb_tv_RegexScrape_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lb_tv_RegexScrape.SelectedIndexChanged
        Try
            If lb_tv_RegexScrape.SelectedItem <> Nothing Then
                tb_tv_RegexScrape_Edit.Text = lb_tv_RegexScrape.SelectedItem
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_MoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_MoveUp.Click
        'up
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.lb_tv_RegexScrape.SelectedIndex <> 0 Then
                mSelectedIndex = Me.lb_tv_RegexScrape.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                lb_tv_RegexScrape.Items.Insert(mSelectedIndex + 1, lb_tv_RegexScrape.Items(mOtherIndex))
                lb_tv_RegexScrape.Items.RemoveAt(mOtherIndex)
            End If
            Pref.tv_RegexScraper.Clear()
            For Each item In lb_tv_RegexScrape.Items
                Pref.tv_RegexScraper.Add(item)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_MoveDown_MoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_MoveDown.Click
        'down
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.lb_tv_RegexScrape.SelectedIndex <> Me.lb_tv_RegexScrape.Items.Count - 1 Then
                mSelectedIndex = Me.lb_tv_RegexScrape.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                lb_tv_RegexScrape.Items.Insert(mSelectedIndex, lb_tv_RegexScrape.Items(mOtherIndex))
                lb_tv_RegexScrape.Items.RemoveAt(mOtherIndex + 1)
            End If
            Pref.tv_RegexScraper.Clear()
            For Each item In lb_tv_RegexScrape.Items
                Pref.tv_RegexScraper.Add(item)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_Edit.Click
        Try
            If tb_tv_RegexScrape_Edit.Text = "" Then
                MsgBox("No Text")
                Exit Sub
            End If
            If Not Form1.util_RegexValidate(tb_tv_RegexScrape_Edit.Text) Then
                MsgBox("Invalid Regex")
                Exit Sub
            End If
            Dim tempint As Integer = lb_tv_RegexScrape.SelectedIndex
            lb_tv_RegexScrape.Items.RemoveAt(tempint)
            lb_tv_RegexScrape.Items.Insert(tempint, tb_tv_RegexScrape_Edit.Text)
            lb_tv_RegexScrape.SelectedIndex = tempint
            Pref.tv_RegexScraper.Clear()
            For Each regexp In lb_tv_RegexScrape.Items
                Pref.tv_RegexScraper.Add(regexp)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_Add_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_Add.Click
        Try
            'add textbox49
            If Not Form1.util_RegexValidate(tb_tv_RegexScrape_New.Text) Then
                MsgBox("Invalid Regex")
                Exit Sub
            End If
            lb_tv_RegexScrape.Items.Add(tb_tv_RegexScrape_New.Text)
            Pref.tv_RegexScraper.Add(tb_tv_RegexScrape_New.Text)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_Remove_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_Remove.Click
        Try
            'remove selected
            Dim tempstring = lb_tv_RegexScrape.SelectedItem
            Try
                lb_tv_RegexScrape.Items.Remove(lb_tv_RegexScrape.SelectedItem)
            Catch ex As Exception
            End Try
            For Each regexp In Pref.tv_RegexScraper
                If regexp = tempstring Then
                    Pref.tv_RegexScraper.Remove(regexp)
                    Exit For
                End If
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_Test_Test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_Test.Click
        Try
            If tb_tv_RegexScrape_TestString.Text = "" Then
                MsgBox("Please Enter a filename or any string to test")
                Exit Sub
            End If
            If lb_tv_RegexScrape.SelectedItem = Nothing Then
                MsgBox("Please Select a Regex to test")
                Exit Sub
            End If
            tb_tv_RegexScrape_TestResult.Text = ""
            Dim tvseries As String
            Dim tvepisode As String
            Dim s As String
            Dim tempstring As String = tb_tv_RegexScrape_TestString.Text
            s = tempstring '.ToLower
            Dim M As Match
            M = Regex.Match(s, lb_tv_RegexScrape.SelectedItem)
            If M.Success = True Then
                Try
                    tvseries = M.Groups(1).Value
                    tvepisode = M.Groups(2).Value
                Catch
                    tvseries = "-1"
                    tvepisode = "-1"
                End Try
                Try
                    If tvseries <> "-1" Then
                        tb_tv_RegexScrape_TestResult.Text = "Series No = " & tvseries & vbCrLf
                    End If
                    If tvepisode <> "-1" Then
                        tb_tv_RegexScrape_TestResult.Text = tb_tv_RegexScrape_TestResult.Text & "Episode No = " & tvepisode
                    End If
                Catch ex As Exception
                End Try
            Else
                tb_tv_RegexScrape_TestResult.Text = "No Matches"
            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexScrape_Restore_Restore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tv_RegexScrape_Restore.Click
        Try
           Pref. util_RegexSetDefaultScraper()
            lb_tv_RegexScrape.Items.Clear()
            For Each Regex In Pref.tv_RegexScraper
                lb_tv_RegexScrape.Items.Add(Regex)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Rename
    Private Sub lb_tv_RegexRename_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lb_tv_RegexRename.SelectedIndexChanged
        Try
            If lb_tv_RegexRename.SelectedItem <> Nothing Then
                tb_tv_RegexRename_Edit.Text = lb_tv_RegexRename.SelectedItem
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexRename_MoveDown_Click(sender As System.Object, e As System.EventArgs) Handles btn_tv_RegexRename_MoveDown.Click
        Try
            'down
            Dim mSelectedIndex, mOtherIndex As Integer
            If lb_tv_RegexRename.SelectedIndex <> lb_tv_RegexRename.Items.Count - 1 Then
                mSelectedIndex = lb_tv_RegexRename.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                lb_tv_RegexRename.Items.Insert(mSelectedIndex, lb_tv_RegexRename.Items(mOtherIndex))
                lb_tv_RegexRename.Items.RemoveAt(mOtherIndex + 1)
            End If
            Pref.tv_RegexRename.Clear()
            For Each item In lb_tv_RegexRename.Items
                Pref.tv_RegexRename.Add(item)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexRename_MoveUp_Click(sender As System.Object, e As System.EventArgs) Handles btn_tv_RegexRename_MoveUp.Click
        Try
            'up
            Dim mSelectedIndex, mOtherIndex As Integer
            If lb_tv_RegexRename.SelectedIndex <> 0 Then
                mSelectedIndex = lb_tv_RegexRename.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                lb_tv_RegexRename.Items.Insert(mSelectedIndex + 1, lb_tv_RegexRename.Items(mOtherIndex))
                lb_tv_RegexRename.Items.RemoveAt(mOtherIndex)
            End If
            Pref.tv_RegexRename.Clear()
            For Each item In lb_tv_RegexRename.Items
                Pref.tv_RegexRename.Add(item)
            Next
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexRename_Remove_Click(sender As Object, e As System.EventArgs) Handles btn_tv_RegexRename_Remove.Click
        Try
            Dim strRegexSelected = lb_tv_RegexRename.SelectedItem
            Dim idxRegexSelected = lb_tv_RegexRename.SelectedIndex
            Try
                lb_tv_RegexRename.Items.RemoveAt(idxRegexSelected)
            Catch ex As Exception
            End Try

            For Each regexp In Pref.tv_RegexRename
                If regexp = strRegexSelected Then
                    Pref.tv_RegexRename.Remove(regexp)
                    Exit For
                End If
            Next
            tb_tv_RegexRename_Edit.Clear()
            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In Pref.tv_RegexRename
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.SelectedIndex = If(Pref.tvrename >= idxRegexSelected, Pref.tvrename - 1, Pref.tvrename)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexRename_Add_Click(sender As Object, e As System.EventArgs) Handles btn_tv_RegexRename_Add.Click
        Try
            'add
            lb_tv_RegexRename.Items.Add(tb_tv_RegexRename_New.Text)
            Pref.tv_RegexRename.Add(tb_tv_RegexRename_New.Text)
            tb_tv_RegexRename_New.Clear()
            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In Pref.tv_RegexRename
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.SelectedIndex = Pref.tvrename
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexRename_Edit_Click(sender As Object, e As System.EventArgs) Handles btn_tv_RegexRename_Edit.Click
        Try
            If tb_tv_RegexRename_Edit.Text = "" Then
                MsgBox("No Text")
                Exit Sub
            End If
            Dim tempint As Integer = lb_tv_RegexRename.SelectedIndex
            lb_tv_RegexRename.Items.RemoveAt(tempint)
            lb_tv_RegexRename.Items.Insert(tempint, tb_tv_RegexRename_Edit.Text)
            lb_tv_RegexRename.SelectedIndex = tempint
            Pref.tv_RegexRename.Clear()
            For Each regexp In lb_tv_RegexRename.Items
                Pref.tv_RegexRename.Add(regexp)
            Next
            tb_tv_RegexRename_Edit.Clear()
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_tv_RegexRename_Restore_Click(sender As Object, e As System.EventArgs) Handles btn_tv_RegexRename_Restore.Click
        Try
            Pref.util_RegexSetDefaultRename()
            lb_tv_RegexRename.Items.Clear()
            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In Pref.tv_RegexRename
                lb_tv_RegexRename.Items.Add(Regex)
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.SelectedIndex = Pref.tvrename
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

#End Region  'TV Regex
    
#End Region 'TV Preferences

#Region "Proxy"
    'Handled by user control ucGenPref_Proxy
#End Region 'Proxy

#Region "XBMC Link"
    'Handles by user control ucGenPref_XbmcLink
#End Region 'XBMC Link

#Region "Profiles & Commands"

#Region "Commands"

    Private Sub btn_CommandAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CommandAdd.Click
        Try
            If tb_CommandTitle.Text <> "" And tb_CommandCommand.Text <> "" Then
                Dim allgood As Boolean = True
                For Each item In lb_CommandTitle.Items
                    If tb_CommandTitle.Text = item Then
                        allgood = False
                    End If
                Next
                If allgood Then
                    Dim newcom As New str_ListOfCommands(SetDefaults)
                    newcom.command = tb_CommandCommand.Text
                    newcom.title = tb_CommandTitle.Text
                    Pref.commandlist.Add(newcom)
                    lb_CommandTitle.Items.Add(newcom.title)
                    lb_CommandCommand.Items.Add(newcom.command)
                    Dim x As Integer = Form1.ToolsToolStripMenuItem.DropDownItems.Count
                    For i = x-1 To Form1.MCToolsCommands Step -1
                       Form1. ToolsToolStripMenuItem.DropDownItems.RemoveAt(i)
                    Next
                    For Each com In Pref.commandlist
                       Form1.ToolsToolStripMenuItem.DropDownItems.Add(com.title)
                    Next
                    If prefsload Then Exit Sub
                    Changes = True
                Else
                    MsgBox("Title already exists in list")
                End If
            Else
                MsgBox("This feature needs both a title & command")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub lb_CommandTitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lb_CommandTitle.SelectedIndexChanged
        Try
            lb_CommandCommand.SelectedIndex = lb_CommandTitle.SelectedIndex
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub lb_CommandCommand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lb_CommandCommand.SelectedIndexChanged
        Try
            lb_CommandTitle.SelectedIndex = lb_CommandCommand.SelectedIndex
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_CommandRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CommandRemove.Click
        Try
            If lb_CommandTitle.SelectedItem <> "" And lb_CommandCommand.SelectedItem <> "" Then
                For Each com In Pref.commandlist
                    If com.title = lb_CommandTitle.SelectedItem And com.command = lb_CommandCommand.SelectedItem Then
                        Pref.commandlist.Remove(com)
                        Exit For
                    End If
                Next
                lb_CommandTitle.Items.Clear()
                lb_CommandCommand.Items.Clear()
                Dim x As Integer = Form1.ToolsToolStripMenuItem.DropDownItems.Count
                For i = x-1 To Form1.MCToolsCommands Step -1
                    Form1.ToolsToolStripMenuItem.DropDownItems.RemoveAt(i)
                Next
                For Each com In Pref.commandlist
                    lb_CommandTitle.Items.Add(com.title)
                    lb_CommandCommand.Items.Add(com.command)
                    Form1.ToolsToolStripMenuItem.DropDownItems.Add(com.title)
                Next
            Else
                MsgBox("Nothing selected to remove")
            End If
            If prefsload Then Exit Sub
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#End Region

#Region "Profiles"
    'Profiles
    Private Sub btn_ProfileAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ProfileAdd.Click
        Try
            For Each pro In Form1.profileStruct.ProfileList
                If pro.ProfileName.ToLower = tb_ProfileNew.Text.ToLower Then
                    MsgBox("This Profile Already Exists" & vbCrLf & "Please Select Another Name")
                    Exit Sub
                End If
            Next
            Dim done As Boolean = False
            Dim tempint As Integer = 0
            For f = 1 To 1000
                Dim tempstring2 As String = Pref.applicationPath & "\Settings\"
                Dim configpath As String = tempstring2 & "config" & f.ToString & ".xml"
                Dim actorcachepath As String = tempstring2 & "actorcache" & f.ToString & ".xml"
                Dim directorcachepath As String = tempstring2 & "directorcache" & f.ToString & ".xml"
                Dim filterspath As String = tempstring2 & "filters" & f.ToString & ".txt"
                Dim genrespath As String = tempstring2 & "genres" & f.ToString & ".txt"
                Dim moviecachepath As String = tempstring2 & "moviecache" & f.ToString & ".xml"
                Dim regexpath As String = tempstring2 & "regex" & f.ToString & ".xml"
                Dim tvcachepath As String = tempstring2 & "tvcache" & f.ToString & ".xml"
                Dim musicvideocachepath As String = tempstring2 & "musicvideocache" & f.ToString & ".xml"
                Dim ok As Boolean = True
                If File.Exists(configpath) Then ok = False
                If File.Exists(actorcachepath) Then ok = False
                If File.Exists(directorcachepath) Then ok = False
                If File.Exists(filterspath) Then ok = False
                If File.Exists(genrespath) Then ok = False
                If File.Exists(moviecachepath) Then ok = False
                If File.Exists(regexpath) Then ok = False
                If File.Exists(tvcachepath) Then ok = False
                If File.Exists(musicvideocachepath) Then ok = False
                If ok = True Then
                    tempint = f
                    Exit For
                End If
            Next
            'new profilename
            Dim tempstring As String = Pref.applicationPath & "\Settings\"
            Dim moviecachetocopy        As String = String.Empty
            Dim actorcachetocopy        As String = String.Empty
            Dim musiccachetocopy        As String = String.Empty
            Dim musicvideocachetocopy   As String = String.Empty
            Dim directorcachetocopy     As String = String.Empty
            Dim tvcachetocopy           As String = String.Empty
            Dim configtocopy            As String = String.Empty
            Dim filterstocopy           As String = String.Empty
            Dim genrestocopy            As String = String.Empty
            Dim regextocopy             As String = String.Empty
            Dim moviesetcachetocopy     As String = String.Empty
            For Each profs In Form1.profileStruct.ProfileList
                If profs.ProfileName = Form1.profileStruct.DefaultProfile Then
                    musicvideocachetocopy   = profs.MusicVideoCache
                    moviecachetocopy        = profs.MovieCache
                    actorcachetocopy        = profs.ActorCache
                    directorcachetocopy     = profs.DirectorCache
                    tvcachetocopy           = profs.TvCache
                    configtocopy            = profs.Config
                    filterstocopy           = profs.Filters
                    genrestocopy            = profs.Genres 
                    regextocopy             = profs.RegExList
                    moviesetcachetocopy     = profs.MovieSetCache 
                End If
            Next

            Dim profiletoadd As New ListOfProfiles
            profiletoadd.ActorCache         = tempstring & "actorcache" & tempint.ToString & ".xml"
            profiletoadd.DirectorCache      = tempstring & "directorcache" & tempint.ToString & ".xml"
            profiletoadd.Config             = tempstring & "config" & tempint.ToString & ".xml"
            profiletoadd.Filters            = tempstring & "filters" & tempint.ToString & ".txt"
            profiletoadd.Genres             = tempstring & "genres" & tempint.ToString & ".txt"
            profiletoadd.MovieCache         = tempstring & "moviecache" & tempint.ToString & ".xml"
            profiletoadd.RegExList          = tempstring & "regex" & tempint.ToString & ".xml"
            profiletoadd.TvCache            = tempstring & "tvcache" & tempint.ToString & ".xml"
            profiletoadd.MusicVideoCache    = tempstring & "musicvideocache" & tempint.ToString & ".xml"
            profiletoadd.MovieSetCache      = tempstring & "moviesetcache" & tempint.ToString & ".xml"
            profiletoadd.ProfileName        = tb_ProfileNew.Text
            Form1.profileStruct.ProfileList.Add(profiletoadd)

            If File.Exists(moviecachetocopy)        Then File.Copy(moviecachetocopy, profiletoadd.MovieCache)
            If File.Exists(musicvideocachetocopy)   Then File.Copy(musicvideocachetocopy, profiletoadd.MusicVideoCache)
            If File.Exists(actorcachetocopy)        Then File.Copy(actorcachetocopy, profiletoadd.ActorCache)
            If File.Exists(directorcachetocopy)     Then File.Copy(directorcachetocopy, profiletoadd.DirectorCache)
            If File.Exists(tvcachetocopy)           Then File.Copy(tvcachetocopy, profiletoadd.TvCache)
            If File.Exists(configtocopy)            Then File.Copy(configtocopy, profiletoadd.Config)
            If File.Exists(filterstocopy)           Then File.Copy(filterstocopy, profiletoadd.Filters)
            If File.Exists(genrestocopy)            Then File.Copy(genrestocopy, profiletoadd.Genres)
            If File.Exists(regextocopy)             Then File.Copy(regextocopy, profiletoadd.RegExList)
            If File.Exists(moviesetcachetocopy)     Then File.Copy(moviesetcachetocopy, profiletoadd.MovieSetCache)
            lb_ProfileList.Items.Add(tb_ProfileNew.Text)
            Call Form1.util_ProfileSave()
            done = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_ProfileSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ProfileSetDefault.Click
        Try
            'setselected profile to default
            For Each prof In Form1.profileStruct.ProfileList
                If prof.ProfileName = lb_ProfileList.SelectedItem Then
                    Form1.profileStruct.defaultprofile = prof.ProfileName
                    Label18.Text = "Current Default Profile: " & prof.ProfileName
                    Call Form1.util_ProfileSave()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_ProfileSetStartup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ProfileSetStartup.Click
        Try
            'setselected profile to startup
            For Each prof In Form1.profileStruct.ProfileList
                If prof.ProfileName = lb_ProfileList.SelectedItem Then
                    Form1.profileStruct.startupprofile = prof.ProfileName
                    Label3.Text = "Current Startup Profile: " & prof.ProfileName
                    Call Form1.util_ProfileSave()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_ProfileRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ProfileRemove.Click
        Try
            'remove selected profile
            If lb_ProfileList.SelectedItem = Form1.profileStruct.DefaultProfile Then
                MsgBox("You can't delete your default profile" & vbCrLf & "Set another Profile to default then delete it")
                Exit Sub
            End If
            If lb_ProfileList.SelectedItem = Form1.profileStruct.StartupProfile Then
                MsgBox("You can't delete your startup profile" & vbCrLf & "Set another Profile to startup then delete it")
                Exit Sub
            End If
            If lb_ProfileList.SelectedItem = Pref.workingProfile.profilename Then
                MsgBox("You can't delete a loaded profile" & vbCrLf & "Load another Profile then delete it")
                Exit Sub
            End If
            Dim tempint As Integer = MessageBox.Show("Removing a profile will delete all associated cache files and settings," & vbCrLf & "Are you sure you want to remove the selected profile", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tempint = DialogResult.Yes Then
                Dim tempint2 As Integer = 0
                For f = 0 To Form1.profileStruct.ProfileList.Count - 1
                    If Form1.profileStruct.profilelist(f).ProfileName = lb_ProfileList.SelectedItem Then
                        tempint2 = f
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).ActorCache)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).DirectorCache)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).Config)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).Filters)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).Genres)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).MovieCache)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).MusicVideoCache)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).RegExList)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).TvCache)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(Form1.profileStruct.profilelist(f).MovieSetCache)
                        Catch ex As Exception
                        End Try
                        Exit For
                    End If
                Next
                Form1.profileStruct.ProfileList.RemoveAt(tempint2)
                lb_ProfileList.Items.Clear()
                Form1.ProfilesToolStripMenuItem.DropDownItems.Clear()
                If Form1.profileStruct.ProfileList.Count > 1 Then
                    Form1.ProfilesToolStripMenuItem.Visible = True
                Else
                    Form1.ProfilesToolStripMenuItem.Visible = False
                End If
                Form1.ProfilesToolStripMenuItem.DropDownItems.Clear()
                For Each prof In Form1.profileStruct.ProfileList
                    lb_ProfileList.Items.Add(prof.ProfileName)
                    Form1.ProfilesToolStripMenuItem.DropDownItems.Add(prof.ProfileName)
                Next
                Form1.util_ProfileSave()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
#End Region

#End Region 'Profiles & Commands



    Private Sub applyAdvancedLists()
        If cleanfilenameprefchanged Then
            Dim strTemp As String = ""
            For i = 0 To lbCleanFilename.Items.Count - 1
                strTemp &= lbCleanFilename.Items(i) & "|"
            Next
            Pref.moviecleanTags = strTemp.TrimEnd("|")
            cleanfilenameprefchanged = False
        End If
        If videosourceprefchanged Then
            Dim count As Integer = lbVideoSource.Items.Count - 1
            ReDim Pref.releaseformat(count)
            For g = 0 To count
                Pref.releaseformat(g) = lbVideoSource.Items(g)
            Next
            Form1.mov_VideoSourcePopulate()
            Form1.ep_VideoSourcePopulate()
            videosourceprefchanged = False
        End If
    End Sub

    Private Sub mov_ThumbNailUrlsSet()
        If IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 0
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 1
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 2
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 3
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 4
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 5
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 6
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 7
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 8
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 9
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 10
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 11
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 12
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 13
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 14
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 15
        End If
    End Sub
    
    Private Sub XBMCTMDBConfigSave()
        If Not Pref.XbmcTmdbScraperRatings = Nothing Then
            Save_XBMC_TMDB_Scraper_Config("fanart", Pref.XbmcTmdbScraperFanart)
            Save_XBMC_TMDB_Scraper_Config("trailerq", Pref.XbmcTmdbScraperTrailerQ)
            Save_XBMC_TMDB_Scraper_Config("language", Pref.XbmcTmdbScraperLanguage)
            Save_XBMC_TMDB_Scraper_Config("ratings", Pref.XbmcTmdbScraperRatings)
            Save_XBMC_TMDB_Scraper_Config("tmdbcertcountry", Pref.XbmcTmdbScraperCertCountry)
        End If
    End Sub

    Private Sub XBMCTVDBConfigSave()
        If Not String.IsNullOrEmpty(Pref.XBMCTVDbLanguage) Then
            Save_XBMC_TVDB_Scraper_Config("dvdorder", Pref.XBMCTVDbDvdOrder)
            Save_XBMC_TVDB_Scraper_Config("absolutenumber", Pref.XBMCTVDbAbsoluteNumber)
            Save_XBMC_TVDB_Scraper_Config("fanart", Pref.XBMCTVDbFanart)
            Save_XBMC_TVDB_Scraper_Config("posters", Pref.XBMCTVDbPoster)
            Save_XBMC_TVDB_Scraper_Config("language", Pref.XBMCTVDbLanguage)  'ComboBox_TVDB_Language.Text)
            Save_XBMC_TVDB_Scraper_Config("ratings", Pref.XBMCTVDbRatings)
            Save_XBMC_TVDB_Scraper_Config("fallback", Pref.XBMCTVDbfallback)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Text.ToLower = "xbmc link" OrElse TabControl1.SelectedTab.Text.ToLower = "proxy" Then
            btn_SettingsApply.Visible = False
            btn_SettingsCancel.Visible = False
            btn_SettingsClose.Visible = False
            btn_SettingsClose2.Visible = True
        Else
            btn_SettingsApply.Visible = True
            btn_SettingsCancel.Visible = True
            btn_SettingsClose.Visible = True
            btn_SettingsClose2.Visible = False
        End If
    End Sub

    Private Sub TPXBMCLink_Enter(sender As Object, e As EventArgs) Handles TPXBMCLink.Enter
        UcGenPref_XbmcLink1.Pop()
    End Sub

    Private Sub tpPrxy_Enter(sender As Object, e As EventArgs) 
        UcGenPref_Proxy1.pop()
    End Sub

    Private Sub TMDbControlsIni()
        TMDb.LoadLanguages(comboBoxTMDbSelectedLanguage)
        'SetLanguageControlsState()
    End Sub

    Private Sub SetLanguageControlsState()
        comboBoxTMDbSelectedLanguage.Enabled = Not cbUseCustomLanguage.Checked
        gbCustomLanguage.Enabled = cbUseCustomLanguage.Checked
    End Sub

    Private Sub Tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick
        Dim hFb As IntPtr = FindWindowW("#32770", "Browse For Folder") '#32770 is the class name of a folderbrowser dialog
        If hFb <> IntPtr.Zero Then
            If SendMessageW(hFb, BFFM_SETEXPANDED, 1, fb.SelectedPath) = IntPtr.Zero Then
                Tmr.Stop()
            End If
        End If
    End Sub

    
End Class