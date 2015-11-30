Imports System.ComponentModel
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml


Public Class frmOptions
    Public Const SetDefaults = True
    Private fb As New FolderBrowserDialog
    Dim _Pref As New Pref
    Dim moviefolders As New List(Of str_RootPaths)
    Dim tvfolders As New List(Of String)
    Dim _changed As Boolean
    Dim prefsload As Boolean = False
    Dim videosourceprefchanged As Boolean = False
    Dim cleanfilenameprefchanged As Boolean = False
    
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
                    Pref.ConfigSave()
                    XBMCTMDBConfigSave()
                    'XBMCTVDBConfigSave()
                Else
                    Pref.ConfigLoad()
                End If
            End If
            'For f = 0 To 33
            '    Pref.certificatepriority(f) = ListBox5.Items(f)
            'Next
            'For f = 0 To 3
            '    Pref.moviethumbpriority(f) = ListBox3.Items(f)
            'Next

            'If Pref.videomode = 4 Then
            '    If Not IO.File.Exists(Pref.selectedvideoplayer) Then
            '        MsgBox("You Have Not Selected Your Preferred Media Player")
            '        e.Cancel = True
            '        Exit Sub
            '    End If
            'End If

            'Pref.SaveConfig()
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
            prefsload = True
            PrefInit()
            CommonInit()
            GeneralInit()
            MovieInit()
            
            CmdsNProfilesInit()
            prefsload = False
            Changes = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_SettingsApply_Click(sender As Object, e As EventArgs) Handles btn_SettingsApply.Click
        If cleanfilenameprefchanged OrElse videosourceprefchanged Then
            applyAdvancedLists()
        End If
        Pref.ConfigSave()
        Changes = False
    End Sub

    Private Sub btn_SettingsCancel_Click(sender As Object, e As EventArgs) Handles btn_SettingsCancel.Click
        Changes = False
        Pref.ConfigLoad()
    End Sub

    Private Sub btn_SettingsClose_Click(sender As Object, e As EventArgs) Handles btn_SettingsClose.Click, btn_SettingsClose2.Click
        Me.Close()
    End Sub
    
#End Region 'Form Events

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
        cb_SorttitleIgnoreArticles  .Checked    = Pref.sorttitleignorearticle
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


        Form1.displayRuntimeScraper = True
        If Pref.enablehdtags = True Then
            CheckBox19.CheckState = CheckState.Checked
            PanelDisplayRuntime.Enabled = True
            If Pref.movieRuntimeDisplay = "file" Then
                rbRuntimeFile.Checked = True
            Else
                rbRuntimeScraper.Checked = True
            End If
        Else
            CheckBox19.CheckState = CheckState.Unchecked
            PanelDisplayRuntime.Enabled = False
            rbRuntimeScraper.Checked = True
        End If
        Call Form1.mov_SwitchRuntime()

        
        TextBox_OfflineDVDTitle             .Text           = Pref.OfflineDVDTitle
        tb_MovieRenameEnable                .Text           = Pref.MovieRenameTemplate
        tb_MovFolderRename                  .Text           = Pref.MovFolderRenameTemplate 
        localactorpath                      .Text           = Pref.actorsavepath
        xbmcactorpath                       .Text           = Pref.actornetworkpath
        cbPreferredTrailerResolution        .Text           = Pref.moviePreferredTrailerResolution.ToUpper()
        cb_MovDurationAsRuntine             .Checked        = Pref.MovDurationAsRuntine 
        cbMovieRuntimeFallbackToFile        .Enabled        = (Pref.movieRuntimeDisplay = "scraper")
        cbMovieRuntimeFallbackToFile        .Checked        = Pref.movieRuntimeFallbackToFile
        tbDateFormat                        .Text           = Pref.DateFormat
        cbMovieList_ShowColPlot             .Checked        = Pref.MovieList_ShowColPlot
        cbDisableNotMatchingRenamePattern   .Checked        = Pref.DisableNotMatchingRenamePattern
        cbMovieList_ShowColWatched          .Checked        = Pref.MovieList_ShowColWatched
        
        nudActorsFilterMinFilms             .Text           = Pref.ActorsFilterMinFilms
        nudMaxActorsInFilter                .Text           = Pref.MaxActorsInFilter
        cbMovieFilters_Actors_Order         .SelectedIndex  = Pref.MovieFilters_Actors_Order
        nudDirectorsFilterMinFilms          .Text           = Pref.DirectorsFilterMinFilms
        nudMaxDirectorsInFilter             .Text           = Pref.MaxDirectorsInFilter
        cbMovieFilters_Directors_Order      .SelectedIndex  = Pref.MovieFilters_Directors_Order
        cbMissingMovie                      .Checked        = Pref.incmissingmovies 
        nudSetsFilterMinFilms               .Text           = Pref.SetsFilterMinFilms
        nudMaxSetsInFilter                  .Text           = Pref.MaxSetsInFilter
        cbMovieFilters_Sets_Order           .SelectedIndex  = Pref.MovieFilters_Sets_Order
        
        'RadioButton52                      .Checked        = If(Pref.XBMC_Scraper = "tmdb", True, False ) 
        cbNoAltTitle                        .Checked        = Pref.NoAltTitle
        cbXtraFrodoUrls                     .Checked        = Not Pref.XtraFrodoUrls
        CheckBox16                          .Checked        = Not Pref.disablelogfiles
        cbDlTrailerDuringScrape             .Checked        = Pref.DownloadTrailerDuringScrape
        cbMovieTrailerUrl                   .Checked        = Pref.gettrailer
        cbMoviePosterScrape                 .Checked        = Pref.scrapemovieposters
        cbMovFanartScrape                   .Checked        = Pref.savefanart
        cbMovFanartTvScrape                 .Checked        = Pref.MovFanartTvscrape
        cbMovFanartNaming                   .Checked        = Pref.MovFanartNaming
        
        cbMovXtraThumbs                     .Checked        = Pref.movxtrathumb
        cbMovXtraFanart                     .Checked        = Pref.movxtrafanart
        cbDlXtraFanart                      .Checked        = Pref.dlxtrafanart
        cbMovSetArtScrape                   .Checked        = Pref.dlMovSetArtwork
        rbMovSetArtSetFolder                .Checked        = Pref.MovSetArtSetFolder
        rbMovSetFolder                      .Checked        = Not Pref.MovSetArtSetFolder 
        btnMovSetCentralFolderSelect        .Enabled        = Pref.MovSetArtSetFolder 
        tbMovSetArtCentralFolder            .Text           = Pref.MovSetArtCentralFolder 
        
        cbMovCreateFolderjpg                .Checked        = Pref.createfolderjpg
        cbMovCreateFanartjpg                .Checked        = Pref.createfanartjpg
        cbMovRootFolderCheck                .Checked        = Pref.movrootfoldercheck
        
        cbxNameMode                         .Checked        = Pref.namemode
        cbxCleanFilenameIgnorePart          .Checked        = Pref.movieignorepart
        cbMovieRenameEnable                 .Checked        = Pref.MovieRenameEnable
        cbMovNewFolderInRootFolder          .Checked        = Pref.MovNewFolderInRootFolder
        cbMovFolderRename                   .Checked        = Pref.MovFolderRename
        cbMovSetIgnArticle                  .Checked        = Pref.MovSetIgnArticle
        cbMovSortIgnArticle                 .Checked        = Pref.MovSortIgnArticle
        cbMovTitleIgnArticle                .Checked        = Pref.MovTitleIgnArticle
        cbMovTitleCase                      .Checked        = Pref.MovTitleCase
        cbExcludeMpaaRated                  .Checked        = Pref.ExcludeMpaaRated
        cbMovThousSeparator                 .Checked        = Pref.MovThousSeparator
        cbRenameUnderscore                  .Checked        = Pref.MovRenameSpaceCharacter
        If Pref.RenameSpaceCharacter = "_" Then
            rbRenameUnderscore.Checked = True
        Else
            rbRenameFullStop.Checked = True
        End If
        CheckBox_ShowDateOnMovieList        .Checked        = Pref.showsortdate
        
        'cbTMDBPreferredCertCountry          .Checked        = Pref.TMDBPreferredCertCountry
        
        
        'saveactorchkbx                      .Checked        = Pref.actorsave
        'cb_LocalActorSaveAlpha              .Checked        = Pref.actorsavealpha

        'localactorpath              .Enabled        = Pref.actorsave
        'xbmcactorpath               .Enabled        = Pref.actorsave
        'Button77                    .Enabled        = Pref.actorsave

        If Not Pref.usefoldernames and Not Pref.allfolders then
            cbMovCreateFolderjpg.Enabled = False
            cbMovCreateFanartjpg.Enabled = False
            cbMovieFanartInFolders.Enabled = False
            cbMoviePosterInFolder.Enabled = False
            Pref.fanartjpg=False
            Pref.posterjpg=False
        Else
            cbMovieFanartInFolders  .Checked    = Pref.fanartjpg
            cbMoviePosterInFolder   .Checked    = Pref.posterjpg
        End If

        cmbxMovXtraFanartQty.SelectedIndex = cmbxMovXtraFanartQty.FindStringExact(Pref.movxtrafanartqty.ToString)


        

        

        

        If lbPosterSourcePriorities.Items.Count <> Pref.moviethumbpriority.Count Then
            lbPosterSourcePriorities.Items.Clear()
            For f = 0 To Pref.moviethumbpriority.Count-1
                lbPosterSourcePriorities.Items.Add(Pref.moviethumbpriority(f))
            Next
        End If
        If lb_MovSepLst.Items.Count <> Pref.MovSepLst.Count Then
            lb_MovSepLst.Items.Clear()
            For Each t In Pref.MovSepLst 
                lb_MovSepLst.Items.Add(t)
            Next
        End If

        

        If lbVideoSource.Items.Count <> Pref.releaseformat.Length Then
            lbVideoSource.Items.Clear()
            For f = 0 To Pref.releaseformat.Length - 1
                lbVideoSource.Items.Add(Pref.releaseformat(f))
            Next
        End If

        lbCleanFilename.Items.Clear()
        lbCleanFilename.Items.AddRange(Pref.moviecleanTags.Split("|"))

        IMPA_chk.CheckState = If(Pref.nfoposterscraper And 1, CheckState.Checked, CheckState.Unchecked)
        tmdb_chk.CheckState = If(Pref.nfoposterscraper And 2, CheckState.Checked, CheckState.Unchecked)
        mpdb_chk.CheckState = If(Pref.nfoposterscraper And 4, CheckState.Checked, CheckState.Unchecked)
        imdb_chk.CheckState = If(Pref.nfoposterscraper And 8, CheckState.Checked, CheckState.Unchecked)
        
        'Form1.TMDbControlsIni()
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

    Private Sub cb_SorttitleIgnoreArticles_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_SorttitleIgnoreArticles.CheckedChanged
        If prefsload Then Exit Sub
        Pref.sorttitleignorearticle = cb_SorttitleIgnoreArticles.Checked
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
                'Read_XBMC_TMDB_Scraper_Config()
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
        Try
            If cbXbmcTmdbFanart.Checked = True Then
                Pref.XbmcTmdbScraperFanart = "true"
            Else
                Pref.XbmcTmdbScraperFanart = "false"
            End If
            ''Save_XBMC_TMDB_Scraper_Config("fanart", Pref.XbmcTmdbScraperFanart)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbXbmcTmdbIMDBRatings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXbmcTmdbIMDBRatings.CheckedChanged, cbXbmcTmdbIMDBRatings.CheckStateChanged 
        Try
            If cbXbmcTmdbIMDBRatings.Checked = True Then
                Pref.XbmcTmdbScraperRatings = "IMDb"
            Else
                Pref.XbmcTmdbScraperRatings = "TMDb"
            End If
            'Save_XBMC_TMDB_Scraper_Config("ratings", Pref.XbmcTmdbScraperRatings)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
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
        Try
            Pref.XbmcTmdbScraperTrailerQ = cmbxXbmcTmdbHDTrailer.Text
            'Save_XBMC_TMDB_Scraper_Config("trailerq", Pref.XbmcTmdbScraperTrailerQ)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxXbmcTmdbTitleLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxXbmcTmdbTitleLanguage.SelectedIndexChanged
        Try
            Pref.XbmcTmdbScraperLanguage = cmbxXbmcTmdbTitleLanguage.Text
            'Save_XBMC_TMDB_Scraper_Config("language", Pref.XbmcTmdbScraperLanguage)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxTMDBPreferredCertCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxTMDBPreferredCertCountry.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try     'tmdbcertcountry
            Pref.XbmcTmdbScraperCertCountry = cmbxTMDBPreferredCertCountry.Text
            'Save_XBMC_TMDB_Scraper_Config("tmdbcertcountry", Pref.XbmcTmdbScraperCertCountry)
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbXbmcTmdbRename_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbRename.CheckedChanged
        If prefsload Then Exit Sub
        Try
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
        Catch
        End Try
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

#End Region 'Movie Preferences

#Region "TV Preferences"

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

    Private Sub XBMCTMDBConfigSave()
        If Not Pref.XbmcTmdbScraperRatings = Nothing Then
            Save_XBMC_TMDB_Scraper_Config("fanart", Pref.XbmcTmdbScraperFanart)
            Save_XBMC_TMDB_Scraper_Config("trailerq", Pref.XbmcTmdbScraperTrailerQ)
            Save_XBMC_TMDB_Scraper_Config("language", Pref.XbmcTmdbScraperLanguage)
            Save_XBMC_TMDB_Scraper_Config("ratings", Pref.XbmcTmdbScraperRatings)
            Save_XBMC_TMDB_Scraper_Config("tmdbcertcountry", Pref.XbmcTmdbScraperCertCountry)
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

    Private Sub TMDbControlsIni()
        TMDb.LoadLanguages(comboBoxTMDbSelectedLanguage)
        'SetLanguageControlsState()
    End Sub

    Private Sub SetLanguageControlsState()
        comboBoxTMDbSelectedLanguage.Enabled = Not cbUseCustomLanguage.Checked
        gbCustomLanguage.Enabled = cbUseCustomLanguage.Checked
    End Sub

End Class