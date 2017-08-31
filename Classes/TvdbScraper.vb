Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports Alphaleonis.Win32.Filesystem
Imports System.Net
Imports System.Threading
Imports System.Xml
Imports Media_Companion
Imports System.Linq

Public Class TVDBScraper
    Const SetDefaults As Boolean = True
    Const msg_append As Progress.Commands = Progress.Commands.Append
    Const MSG_PREFIX As String = ""
    Const MSG_OK As String = "-OK "
    'Private eden As Boolean = Pref.EdenEnabled
    'Private frodo As Boolean = Pref.FrodoEnabled
    Public Const MSG_ERROR As String = "-ERROR! "
    Dim nfoFunction As New WorkingWithNfoFiles
    Private _possibleTvdb As String = String.Empty
    Private _folder As String
    Private _lang As String
    Private _isepisodes As String
    Public NewShow As New TvShow
    Public Property TvBw As BackgroundWorker = Nothing
    Public Property PercentDone As Integer = 0
    Public Property ProgressStart As String = String.Empty
    Property tvdb As TVDBScraper2
    Property Actions As New ScrapeActions
    Property Scraped As Boolean = False
    Property TimingsLog As String = ""
    Property TimingsLogThreshold As Integer = Pref.ScrapeTimingsLogThreshold
    Property LogScrapeTimes As Boolean = Pref.LogScrapeTimes

    Public ReadOnly Property PossibleTVdb As String
        Get
            Return _possibleTvdb
        End Get
    End Property

    Public Delegate Sub ActionDelegate()

    Class ScrapeActions
        Property Items As New List(Of ScrapeAction)
    End Class

    Class ScrapeAction
        Property Action As ActionDelegate
        Property ActionName As String
        Property Time As New Times

        Sub New(ByVal action As ActionDelegate, actionName As String)
            Me.Action = action
            Me.ActionName = actionName
        End Sub

        Sub Run
            Time.StartTm = DateTime.Now
            _Action
            Time.EndTm = DateTime.Now
        End Sub
    End Class

    Public ReadOnly Property Cancelled As Boolean
        Get
            Application.DoEvents()

            If Not IsNothing(_TvBw) AndAlso _TvBw.WorkerSupportsCancellation AndAlso _TvBw.CancellationPending Then
                ReportProgress("Cancelled!", vbCrLf & "!!! Operation cancelled by user", msg_append)
                Return True
            End If
            Return False
        End Get
    End Property

    Sub New(Optional tvbw As BackgroundWorker = Nothing)
        _TvBw = tvbw
    End Sub

    Sub ReportProgress(Optional progressText As String = Nothing, Optional log As String = Nothing, Optional command As Progress.Commands = Progress.Commands.SetIt)
        ReportProgress(New Progress(progressText, log, command))
    End Sub

    Sub ReportProgress(ByVal oProgress As Progress)
        If Not IsNothing(_TvBw) AndAlso _TvBw.WorkerReportsProgress AndAlso Not (String.IsNullOrEmpty(oProgress.Log) And String.IsNullOrEmpty(oProgress.Message)) Then
            Try
                _TvBw.ReportProgress(PercentDone, oProgress)
            Catch
            End Try
        End If
    End Sub

    Private Sub AppendSeriesScrapeSuccessActions
        Actions.Items.Add(New ScrapeAction(AddressOf AssignScrapedSeries, "Assign scraped movie"))
        Actions.Items.Add(New ScrapeAction(AddressOf DoRenameTVFolders, "Rename Folders"))
        Actions.Items.Add(New ScrapeAction(AddressOf GetTVActors, "Actors scraper"))
        Actions.Items.Add(New ScrapeAction(AddressOf TVTidyUpAnyUnscrapedFields, "Tidy up unscraped fields"))
        Actions.Items.Add(New ScrapeAction(AddressOf SaveTVNFO, "Save Nfo"))
        Actions.Items.Add(New ScrapeAction(AddressOf DownloadTvArt, "Download Series Art"))
        Actions.Items.Add(New ScrapeAction(AddressOf DownloadTVPoster, "Poster download"))
        Actions.Items.Add(New ScrapeAction(AddressOf DownloadTVFanart, "Fanart download"))
        Actions.Items.Add(New ScrapeAction(AddressOf DownloadTVArtFromFanartTv, "Fanart.Tv download"))
        Actions.Items.Add(New ScrapeAction(AddressOf DownloadTVExtraFanart, "Extra Fanart download"))
        'Actions.Items.Add( New ScrapeAction(AddressOf UpdateMovieSetCache          , "Updating movie set cache"  ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf AssignHdTags                 , "Assign HD Tags"            ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf GetKeyWords                  , "Get Keywords for tags"     ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf DoRenameFiles                , "Rename Files"              ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf AssignTrailerUrl             , "Get trailer URL"           ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoPosterThumbs         , "Getting extra Frodo Poster thumbs") )
        'Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoFanartThumbs         , "Getting extra Frodo Fanart thumbs") )
        'Actions.Items.Add( New ScrapeAction(AddressOf AssignPosterUrls             , "Get poster URLs"           ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf DownloadTrailer              , "Trailer download"          ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf AssignMovieToCache           , "Assigning movie to cache"  ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf HandleOfflineFile            , "Handle offline file"       ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf UpdateCaches                 , "Updating caches"           ) )
    End Sub

    Sub AppendScrapeSeriesFailedActions
        Actions.Items.Add(New ScrapeAction(AddressOf TVTidyUpAnyUnscrapedFields, "Tidy up unscraped fields"))
        Actions.Items.Add(New ScrapeAction(AddressOf SaveTVNFO, "Save Nfo"))
        'Actions.Items.Add( New ScrapeAction(AddressOf UpdateCaches                 , "Updating caches"             ) )
    End Sub

    Sub AppendScraperSeriesSpecific
        Actions.Items.Add(New ScrapeAction(AddressOf SeriesScraper_GetBody, "Scrape TVDb Series Main Body"))
        Actions.Items.Add(New ScrapeAction(AddressOf SeriesCheckTVDbBodyScrape, "Checking TVDb Series Main body scrape"))
    End Sub

    Sub IniTVdb
        IniTVdb(PossibleTVdb)
    End Sub

    Sub IniTVdb(tvdbid As String)
        tvdb = New TVDBScraper2(tvdbid)
    End Sub

    Sub Scrape(args As TvdbArgs) ' As String)
        _possibleTvdb = args.tvdbid
        _folder = args.folder
        _isepisodes = args.episode
        _lang = args.lang
        Scrape
    End Sub

    Sub Scrape
        If Not Scraped Then
            Scraped = True
            'General
            Actions.Items.Add(New ScrapeAction(AddressOf IniTVdb, "Initialising TMDb"))

            If _isepisodes Then
                'AppendScraperEpisodesSpecific
            Else
                AppendScraperSeriesSpecific
            End If
            RunScrapeActions
        End If
    End Sub

    Sub RunScrapeActions
        While Actions.Items.Count > 0
            Dim action = Actions.Items(0)
            Try
                action.Run

                If LogScrapeTimes And action.Time.ElapsedTimeMs > TimingsLogThreshold Then
                    TimingsLog &= vbCrLf & "[" & action.ActionName & "] took " & action.Time.ElapsedTimeMs & "ms to run"
                End If

                Actions.Items.RemoveAt(0)

                If Cancelled Then Exit Sub
            Catch ex As Exception
                If ex.Message.ToString.ToLower.Contains("offline") Then
                    ReportProgress(, "!!! Error - Running action [" & action.ActionName & "] threw [" & ex.Message.ToString & "]" & vbCrLf)
                    Actions.Items.RemoveAt(0)
                Else
                    ReportProgress(MSG_ERROR, "!!! Error - Running action [" & action.ActionName & "] threw [" & ex.Message.ToString & "]" & vbCrLf)
                    Actions.Items.Clear
                End If
            End Try
        End While
        ReportProgress(, vbCrLf & vbCrLf)
    End Sub

    Sub AssignScrapedSeries

    End Sub

    Sub DoRenameTVFolders

    End Sub

    Sub GetTVActors
        If Pref.TvdbActorScrape = 0 Or Pref.TvdbActorScrape = 3 Or NewShow.ImdbId.Value = Nothing Then
            NewShow.TvShowActorSource.Value = "tvdb"
            ReportProgress("TVDb Actors ", "Scraping actors from TVDb" & vbCrLf, msg_append)
            TvGetActorTvdb()
        End If
        If (Pref.TvdbActorScrape = 1 Or Pref.TvdbActorScrape = 2) And NewShow.ImdbId.Value <> Nothing Then
            NewShow.TvShowActorSource.Value = "imdb"
            ReportProgress("IMDB Actors ", "Scraping actors from IMDb" & vbCrLf, msg_append)
            'success = TvGetActorImdb(NewShow)

        End If
    End Sub

    Function TvGetActorTvdb()
        Dim success As Boolean = True
        Dim actors As List(Of str_MovieActors) = tvdb.Cast
        Dim actorpaths As New List(Of String)
        Dim workingpath As String = ""
        Dim tempstring As String = ""
        If Pref.actorseasy AndAlso Not Pref.tvshowautoquick Then
            workingpath = NewShow.NfoFilePath.Replace(Path.GetFileName(NewShow.NfoFilePath), "") & ".actors\"
            Utilities.EnsureFolderExists(workingpath)
        End If
        For Each NewAct In actors
            actorpaths.Clear()
            If Pref.ExcludeActorNoThumb AndAlso String.IsNullOrEmpty(NewAct.actorthumb) Then Continue For
            Dim id As String = If(NewAct.actorid = Nothing, "", NewAct.actorid)
            Dim results As XmlNode = Nothing
            Dim filename As String = Utilities.cleanFilenameIllegalChars(NewAct.actorname)
            filename = filename.Replace(" ", "_")
            If Not String.IsNullOrEmpty(NewAct.actorthumb) And NewAct.actorthumb <> "http://thetvdb.com/banners/" Then
                Dim actorurl As String = NewAct.actorthumb
                'Save to .actor folder
                If Pref.actorseasy = True And Pref.tvshowautoquick = False Then
                    If NewShow.TvShowActorSource.Value <> "imdb" Or NewShow.ImdbId = Nothing Then
                        Dim ActorFilename As String = Path.Combine(workingpath, filename)
                        If Pref.FrodoEnabled Then actorpaths.Add(ActorFilename & ".jpg")
                        If Pref.EdenEnabled Then actorpaths.Add(ActorFilename & ".tbn")
                    End If
                End If

                'Save to Local actor folder
                If Pref.actorsave = True And id <> "" Then 'Allow Local folder save, separate from .actor folder saving 
                    Dim workingpath2 As String = ""
                    Dim networkpath As String = Pref.actorsavepath
                    filename = filename & "_" & id
                    workingpath2 = networkpath & "\" & filename.Substring(0, 1) & "\" & filename
                    If Pref.FrodoEnabled Then actorpaths.Add(workingpath2 & ".jpg")
                    If Pref.EdenEnabled Then actorpaths.Add(workingpath2 & ".tbn")
                    NewAct.actorthumb = actorpaths(0)
                    If Pref.actornetworkpath.IndexOf("/") <> -1 Then
                        NewAct.actorthumb = actorpaths(0).Replace(networkpath, Pref.actornetworkpath).Replace("\", "/")
                    ElseIf Pref.actornetworkpath.IndexOf("\") <> -1
                        NewAct.actorthumb = actorpaths(0).Replace(networkpath, Pref.actornetworkpath).Replace("/", "\")
                    End If
                    DoDownloadActorImage(actorurl, actorpaths)
                End If
            End If
            Dim exists As Boolean = False
            NewShow.ListActors.Add(NewAct)
        Next
        Return success
    End Function

    Function TvGetActorImdb() As Boolean
        Dim imdbscraper As New Classimdb
        Dim success As Boolean = False
        If String.IsNullOrEmpty(NewShow.ImdbId.Value) Then Return success
        Dim actorlist As List(Of str_MovieActors) = imdbscraper.GetImdbActorsList(Pref.imdbmirror, NewShow.ImdbId.Value, Pref.maxactors)
        Dim workingpath As String = ""
        If Pref.actorseasy And Not Pref.tvshowautoquick Then
            workingpath = NewShow.NfoFilePath.Replace(Path.GetFileName(NewShow.NfoFilePath), "")
            workingpath = workingpath & ".actors\"
            Utilities.EnsureFolderExists(workingpath)
        End If

        Dim listofactors As List(Of str_MovieActors) = IMDbActors(actorlist, success, workingpath)

        Dim i As Integer = 0
        For Each listact In listofactors
            i += 1
            NewShow.ListActors.Add(listact)
            If i > Pref.maxactors Then Exit For
        Next
        Return success
    End Function

    Private Function IMDbActors(ByVal actorlist As List(Of str_MovieActors), ByRef success As Boolean, ByVal workingpath As String) As List(Of str_MovieActors)
        Dim actcount As Integer = 0
        Dim totalactors As New List(Of str_MovieActors)
        Dim actorpaths As New List(Of String)
        For Each thisresult As str_MovieActors In actorlist
            actorpaths.Clear()
            Dim actorimageurl As String = thisresult.actorthumb
            If Pref.ExcludeActorNoThumb AndAlso String.IsNullOrEmpty(thisresult.actorthumb) Then Continue For
            If Not String.IsNullOrEmpty(thisresult.actorthumb) AndAlso Not String.IsNullOrEmpty(thisresult.actorid) AndAlso actcount < (Pref.maxactors + 1) Then
                If Pref.actorseasy And Not Pref.tvshowautoquick Then
                    Dim filename As String = Utilities.cleanFilenameIllegalChars(thisresult.actorname)
                    filename = Path.Combine(workingpath, filename.Replace(" ", "_"))
                    If Pref.FrodoEnabled Then actorpaths.Add(filename & ".jpg")
                    If Pref.EdenEnabled Then actorpaths.Add(filename & ".tbn")
                    'Dim cachename As String = Utilities.Download2Cache(thisresult.actorthumb)
                    'If cachename <> "" Then
                    '    For Each p In actorpaths
                    '        Utilities.SafeCopyFile(cachename, p, Pref.overwritethumbs)
                    '    Next
                    'End If
                End If
                If Pref.actorsave AndAlso Not Pref.tvshowautoquick Then
                    Dim tempstring As String = Pref.actorsavepath
                    Dim workingpath2 As String = ""
                    If Pref.actorsavealpha Then
                        Dim actorfilename As String = thisresult.actorname.Replace(" ", "_") & "_" & If(Pref.LocalActorSaveNoId, "", thisresult.actorid) ' & ".tbn"
                        tempstring = tempstring & "\" & actorfilename.Substring(0, 1) & "\"
                        workingpath2 = tempstring & actorfilename
                    Else
                        tempstring = tempstring & "\" & thisresult.actorid.Substring(thisresult.actorid.Length - 2, 2) & "\"
                        workingpath2 = tempstring & thisresult.actorid ' & ".tbn"
                    End If
                    If Pref.FrodoEnabled Then actorpaths.Add(workingpath2 & ".jpg")
                    If Pref.EdenEnabled Then actorpaths.Add(workingpath2 & ".tbn")
                    'Utilities.EnsureFolderExists(tempstring)
                    'Dim cachename As String = Utilities.Download2Cache(thisresult.actorthumb)
                    'If cachename <> "" Then
                    '    For Each p In actorpaths
                    '        Utilities.SafeCopyFile(cachename, p, Pref.overwritethumbs)
                    '    Next
                    'End If
                    If Not String.IsNullOrEmpty(Pref.actornetworkpath) Then
                        If Pref.actornetworkpath.IndexOf("/") <> -1 Then
                            thisresult.actorthumb = actorpaths(0).Replace(Pref.actorsavepath, Pref.actornetworkpath).Replace("\", "/")
                        Else
                            thisresult.actorthumb = actorpaths(0).Replace(Pref.actorsavepath, Pref.actornetworkpath).Replace("/", "\")
                        End If
                    End If
                End If
                DoDownloadActorImage(actorimageurl, actorpaths)
            End If
            totalactors.Add(thisresult)
            success = True
            actcount += 1
        Next
        Return totalactors
    End Function

    Sub DoDownloadActorImage(ByVal url As String, actorpaths As List(Of String))
        If actorpaths.Count = 0 Then Exit Sub
        Dim cachename As String = Utilities.Download2Cache(url)
        If cachename <> "" Then
            For Each p In actorpaths
                Utilities.EnsureFolderExists(p)
                Utilities.SafeCopyFile(cachename, p, Pref.overwritethumbs)
            Next
        End If

    End Sub

    Sub TVTidyUpAnyUnscrapedFields
        If Pref.TvdbActorScrape = 0 Or Pref.TvdbActorScrape = 2 Then
            NewShow.EpisodeActorSource.Value = "tvdb"
        Else
            NewShow.EpisodeActorSource.Value = "imdb"
        End If
        NewShow.SortOrder.Value = Pref.sortorder
        GetRatings()
    End Sub

    Sub SaveTVNFO
        ReportProgress("Saving tvshow.nfo", "Saving tvshow.nfo ", msg_append)
        nfoFunction.tvshow_NfoSave(NewShow, True)
        ReportProgress(, MSG_OK & vbCrLf)
    End Sub

    Sub GetRatings()
        If Pref.tvdbIMDbRating Then
            ReportProgress(" Ratings from IMDb", "Attempt to get ratings from IMDb ", msg_append)
            Dim ratingdone As Boolean = False
            Dim rating As String = ""
            Dim votes As String = ""
            If ep_getIMDbRating(NewShow.ImdbId.Value, rating, votes) Then
                If rating <> "" Then
                    ratingdone = True
                    NewShow.Rating.Value = rating
                    NewShow.Votes.Value = votes
                    ReportProgress(MSG_OK, MSG_OK & vbCrLf, msg_append)
                End If
            End If
            If Not ratingdone Then
                NewShow.Rating.Value = tvdb.Rating 'SeriesInfo.Series(0).Rating.Value
                NewShow.Votes.Value = tvdb.Votes 'SeriesInfo.Series(0).RatingCount.Value
                ReportProgress("-fallback to TVDb", "-fallback to TVDb" & vbCrLf, msg_append)
            End If
        End If
    End Sub

    Sub DownloadTvArt()
        Dim success As Boolean
        If Pref.TvFanartTvFirst Then
            If Pref.TvDlFanartTvArt OrElse Pref.TvChgShowDlFanartTvArt Then
                ReportProgress(" - Getting FanartTv Artwork", "Getting artwork from FanartTv", msg_append)
                'TvFanartTvArt(NewShow, False)
            End If
            If Pref.tvdlfanart Or Pref.tvdlposter Or Pref.tvdlseasonthumbs Then
                ReportProgress(" - Getting TVDB artwork", "Getting artwork from TVDb", msg_append)
                success = TvGetArtwork(True, True, True, Pref.dlTVxtrafanart)
                If success Then
                    ReportProgress(": OK!",, msg_append)
                Else
                    ReportProgress(": error!!", ,msg_append)
                End If
            End If
        Else
            If Pref.tvdlfanart Or Pref.tvdlposter Or Pref.tvdlseasonthumbs Then
                ReportProgress(" - Getting TVDB artwork", "Getting artwork from TVDb", msg_append)
                success = TvGetArtwork(True, True, True, Pref.dlTVxtrafanart)
                If success Then
                    ReportProgress(": OK!", ": OK!",msg_append)
                Else
                    ReportProgress(": error!!", ": error!!",msg_append)
                End If
            End If
            If Pref.TvDlFanartTvArt OrElse Pref.TvChgShowDlFanartTvArt Then
                ReportProgress(" - Getting FanartTv Artwork", "Getting artwork from FanartTv", msg_append)
                'TvFanartTvArt(NewShow, Pref.TvChgShowDlFanartTvArt)
            End If
        End If
    End Sub
    Sub DownloadTVPoster

    End Sub

    Sub DownloadTVFanart

    End Sub

    Sub DownloadTVArtFromFanartTv

    End Sub

    Sub DownloadTVExtraFanart

    End Sub

    Sub SeriesScraper_GetBody
        NewShow = New TvShow
        Dim tvprogresstxt As String = ""
        Dim haveTVDbID As Boolean = Not String.IsNullOrEmpty(_possibleTvdb)
        NewShow.NfoFilePath = Path.Combine(_folder, "tvshow.nfo")
        NewShow.TvdbId.Value = _possibleTvdb
        NewShow.State = Media_Companion.ShowState.Unverified
        tvprogresstxt = ""
        If Not haveTVDbID And NewShow.FileContainsReadableXml Then
            Dim validcheck As Boolean = nfoFunction.tv_NfoLoadCheck(NewShow.NfoFilePath)
            If validcheck Then
                NewShow = nfoFunction.tvshow_NfoLoad(NewShow.NfoFilePath)
                ReportProgress("Series '" & NewShow.Title.Value & "' found with existing tvshow.nfo", "Series '" & NewShow.Title.Value & "' found with existing tvshow.nfo - Added", msg_append)
                Actions.Items.RemoveAt(1)
            End If
        Else
            If haveTVDbID Then
                NewShow.State = Media_Companion.ShowState.Open
            Else
                'Resolve show name from folder
                Dim FolderName As String = Utilities.GetLastFolder(_folder & "\")
                If FolderName.ToLower.Contains(Pref.excludefromshowfoldername.ToLower) Then
                    Dim indx As Integer = FolderName.ToLower.IndexOf(Pref.excludefromshowfoldername.ToLower)
                    Dim excludearticle As String = FolderName.Substring(indx - 1, Pref.excludefromshowfoldername.Length + 1)
                    FolderName = FolderName.Replace(excludearticle, "")
                End If
                FolderName = FolderName.Replace(Pref.excludefromshowfoldername, "")
                Dim M As Match
                M = Regex.Match(FolderName, "\s*[\(\{\[](?<date>[\d]{4})[\)\}\]]")
                If M.Success = True Then
                    FolderName = String.Format("{0} ({1})", FolderName.Substring(0, M.Index), M.Groups("date").Value)
                End If
                NewShow.Title.Value = FolderName
                ReportProgress(ProgressStart & " possibly - " & FolderName, "Searching for series: " & FolderName & vbCrLf)

                tvdb.LookupLang = _lang
                tvdb.Title = FolderName
                If tvdb.PossibleShowList IsNot Nothing Then
                    Dim tempseries As New TheTvDB.TvdbSeries
                    tempseries = tvdb.FindBestPossibleShow(tvdb.PossibleShowList, FolderName, _lang)
                    If tempseries.Similarity > .9 Then
                        NewShow.State = Media_Companion.ShowState.Open
                    End If
                    _possibleTvdb = tempseries.Identity
                    tempseries = Nothing
                End If
            End If
            If Not String.IsNullOrEmpty(_possibleTvdb) Then
                Dim Series As New TheTvDB.TvdbSeries
                tvdb.TvdbId = _possibleTvdb
                Series = tvdb.Series
                If tvdb.SeriesNotFound Then
                    NewShow.FailedLoad = True
                    ReportProgress(MSG_ERROR, "Please adjust the TV Show title And try again for: " & tvdb.Title & "' - No Show Returned", Progress.Commands.SetIt)
                    Exit Sub
                End If
                ReportProgress(ProgressStart & "Show Title: " & Series.SeriesName & " ", "!!! Series Found: " & Series.SeriesName & " : Id: (" & Series.Identity & ")" & vbCrLf)
                NewShow.AbsorbTvdbSeries(Series)
                NewShow.Language.Value = _lang
            End If
        End If
    End Sub

    Sub SeriesCheckTVDbBodyScrape
        'Failed...
        If NewShow.FailedLoad Then
            ReportProgress(MSG_ERROR, "!!! Unable to scrape body with refs """ & _folder & vbCrLf & "TVDB may not be available or Series Title is invalid" & vbCrLf, msg_append)
            AppendScrapeSeriesFailedActions
        Else
            ReportProgress(MSG_OK, "!!! Series Body Scraped OK" & vbCrLf, msg_append)
            AppendSeriesScrapeSuccessActions
        End If
    End Sub

    Function ep_getIMDbRating(ByVal IMDbId As String, ByRef rating As String, ByRef votes As String) As Boolean
        Dim aok As Boolean = True
        If String.IsNullOrEmpty(IMDbId) Then Return False
        Dim url As String = String.Format("http://akas.imdb.com/title/{0}/", IMDbId)
        Dim IMDbpage As String = Utilities.DownloadTextFiles(url, True)
        Dim m As Match
        m = Regex.Match(IMDbpage, "<span itemprop=""ratingValue"">(.*?)</span>")
        If m.Success Then rating = m.Groups(1).Value
        Dim n As Match
        n = Regex.Match(IMDbpage, "itemprop=""ratingCount"">(.*?)</span")
        If n.Success Then votes = n.Groups(1).Value
        Return rating <> ""
    End Function

    Function TvGetArtwork(ByVal shFanart As Boolean, ByVal shPosters As Boolean, ByVal shSeason As Boolean, ByVal shXtraFanart As Boolean, Optional ByVal force As Boolean = True) As Boolean
        Dim success As Boolean = False
        Try
            Dim MaxSeasonNo As Integer = 1
            Dim eden As Boolean     = Pref.EdenEnabled
            Dim frodo As Boolean    = Pref.FrodoEnabled
            'Dim tvdbstuff           As New TVDBScraper
            Dim currentshowpath As String = NewShow.NfoFilePath.Replace("tvshow.nfo", "")
            Dim doPoster As Boolean = If(Pref.tvdlposter OrElse Pref.TvChgShowDlPoster, True, False)
            Dim doFanart As Boolean = If(Pref.tvdlfanart OrElse Pref.TvChgShowDlFanart, True, False)
            Dim doSeason As Boolean = If(Pref.tvdlseasonthumbs OrElse Pref.TvChgShowDlSeasonthumbs, True, False)
            Dim artlist As New List(Of McImage)
            Dim artlist2 As New List(Of TvBanners)
            artlist.Clear()
            'artlist.AddRange(tvdb)
            'Dim thumblist           As String   = tvdbstuff.GetPosterList(currentshow.TvdbId.Value, artlist)
            Dim isposter As String = Pref.postertype
            Dim isseasonall As String = Pref.seasonall

            Dim overwriteimage As Boolean = If(Pref.overwritethumbs OrElse Pref.TvChgShowOverwriteImgs, True, False)
            If Not force Then overwriteimage = False    'Over-ride overwrite if we force not to overwrite, ie: missing artwork

            Dim Langlist As New List(Of String)
            If Not _lang = "" Then Langlist.Add(_lang)
            Langlist.Add(NewShow.Language.Value)
            If Not Langlist.Contains("en") Then Langlist.Add("en")
            If Not Langlist.Contains(Pref.TvdbLanguageCode) Then Langlist.Add(Pref.TvdbLanguageCode)
            Langlist.Add("")
            If tvdb.McPosters.Count = 0 AndAlso tvdb.McFanart.Count = 0 AndAlso tvdb.McSeason.Count = 0 Then Return success

            For Each art In tvdb.McSeason
                If Not IsNothing(art.Season) AndAlso art.Season.ToInt > MaxSeasonNo Then MaxSeasonNo = art.Season.ToInt
            Next

            'Posters, Main and Season Including Banners
            If shPosters Then
                artlist.AddRange(tvdb.McPosters)
                'Main Poster
                If artlist.Count > 0 AndAlso (isposter = "poster" Or frodo Or isseasonall = "poster") AndAlso doPoster Then 'poster
                    Dim mainposter As String = Nothing
                    Dim q = From b In artlist Order By b.votes Descending
                    If Not q.count = 0 Then
                        Dim tmpimg As McImage = q(0)
                        mainposter = q(0).hdUrl
                    End If
                    If Not IsNothing(mainposter) Then
                        Dim imgpaths As New List(Of String)
                        If frodo Then imgpaths.Add(currentshowpath & "poster.jpg")
                        If eden AndAlso isposter = "poster" Then imgpaths.Add(currentshowpath & "folder.jpg")
                        If frodo AndAlso isseasonall <> "none" Then imgpaths.Add(currentshowpath & "season-all-poster.jpg")
                        If eden AndAlso isseasonall = "poster" Then imgpaths.Add(currentshowpath & "season-all.tbn")
                        success = DownloadCache.SaveImageToCacheAndPaths(mainposter, imgpaths, False, , , overwriteimage)
                        'Dim popath As String = Utilities.save2postercache(NewShow.NfoFilePath, NewShow.ImagePoster.Path, Form1.WallPicWidth, Form1.WallPicHeight)
                        'updateTvPosterWall(popath, currentshow.NfoFilePath)
                    End If
                End If
                artlist.Clear()
                artlist.AddRange(tvdb.McSeries)
                'Main Banner
                If (isposter = "banner" Or frodo Or isseasonall = "wide") And doPoster Then 'banner
                    Dim mainbanner As String = Nothing
                    Dim q = From b In artlist Order By b.votes Descending
                    If Not q.count = 0 Then
                        Dim tmpimg As McImage = q(0)
                        mainbanner = tmpimg.hdUrl
                    End If
                    If Not IsNothing(mainbanner) Then
                        Dim imgpaths As New List(Of String)
                        If frodo Then imgpaths.Add(currentshowpath & "banner.jpg")
                        If eden AndAlso isposter = "banner" Then imgpaths.Add(currentshowpath & "folder.jpg")
                        If frodo AndAlso isseasonall <> "none" Then imgpaths.Add(currentshowpath & "season-all-banner.jpg")
                        If eden AndAlso isseasonall = "wide" Then imgpaths.Add(currentshowpath & "season-all.tbn")
                        success = DownloadCache.SaveImageToCacheAndPaths(mainbanner, imgpaths, False, , , overwriteimage)
                    End If
                End If
            End If

            artlist.Clear()
            artlist.AddRange(tvdb.McSeason)
            artlist.AddRange(tvdb.McSeasonWide)
            If shSeason Then
                'SeasonXX Poster
                For f = 0 To MaxSeasonNo + 1
                    Dim seasonnumber As String = f.ToString
                    If (isposter = "poster" Or frodo) And doSeason Then 'poster
                        Dim seasonXXposter As String = Nothing
                        Dim q = From b In artlist Where b.type = "season" AndAlso b.Season = seasonnumber Order By b.votes Descending
                        If Not q.count = 0 Then
                            Dim tmpimg As McImage = q(0)
                            seasonXXposter = tmpimg.hdUrl
                        End If
                        'For Each lang In Langlist
                        '    For Each Image In artlist2
                        '        If Image.Season = f.ToString AndAlso (Image.Language = lang Or lang = "") Then
                        '            seasonXXposter = Image.Url
                        '            Exit For
                        '        End If
                        '    Next
                        '    If Not IsNothing(seasonXXposter) Then Exit For
                        'Next
                        If Not IsNothing(seasonXXposter) Then
                            Dim tempstring As String = ""
                            If f < 10 Then
                                tempstring = "0" & f.ToString
                            Else
                                tempstring = f.ToString
                            End If
                            If tempstring = "00" Then tempstring = "-specials"
                            Dim imgpaths As New List(Of String)
                            If frodo Then imgpaths.Add(currentshowpath & "season" & tempstring & "-poster.jpg")
                            If eden Then imgpaths.Add(currentshowpath & "season" & tempstring & ".tbn")
                            success = DownloadCache.SaveImageToCacheAndPaths(seasonXXposter, imgpaths, False, , , overwriteimage)
                        End If
                    End If

                    'SeasonXX Banner
                    If (isposter = "banner" Or frodo) And doSeason Then 'banner
                        Dim seasonXXbanner As String = Nothing
                        Dim q = From b In artlist Where b.type = "seasonwide" AndAlso b.Season = seasonnumber Order By b.votes Descending
                        If Not q.count = 0 Then
                            Dim tmpimg As McImage = q(0)
                            seasonXXbanner = tmpimg.hdUrl
                        End If
                        'For Each lang In Langlist
                        '    For Each Image In artlist2
                        '        If Image.Season = f.ToString AndAlso (Image.Language = lang Or lang = "") AndAlso Image.Resolution = "seasonwide" Then
                        '            seasonXXbanner = Image.Url
                        '            Exit For
                        '        End If
                        '    Next
                        '    If Not IsNothing(seasonXXbanner) Then Exit For
                        'Next
                        If Not IsNothing(seasonXXbanner) Then
                            Dim tempstring As String = ""
                            If f < 10 Then
                                tempstring = "0" & f.ToString
                            Else
                                tempstring = f.ToString
                            End If
                            If tempstring = "00" Then tempstring = "-specials"
                            Dim imgpaths As New List(Of String)
                            If frodo Then imgpaths.Add(currentshowpath & "season" & tempstring & "-banner.jpg")
                            If eden Then imgpaths.Add(currentshowpath & "season" & tempstring & ".tbn")
                            success = DownloadCache.SaveImageToCacheAndPaths(seasonXXbanner, imgpaths, False, , , overwriteimage)
                        End If
                    End If
                Next
                TvCheckfolderjpgart()
            End If

            artlist.Clear()
            artlist.AddRange(tvdb.McFanart)
            'Main Fanart
            If shFanart AndAlso doFanart Then
                Dim fanartposter As String = Nothing
                Dim q = From b In artlist Order By b.votes Descending
                If Not q.count = 0 Then
                    Dim tmpimg As McImage = q(0)
                    fanartposter = tmpimg.hdUrl
                End If
                If Not IsNothing(fanartposter) Then
                    Dim imgpaths As New List(Of String)
                    imgpaths.Add(currentshowpath & "fanart.jpg")
                    If frodo And isseasonall <> "none" Then imgpaths.Add(currentshowpath & "season-all-fanart.jpg")
                    success = DownloadCache.SaveImageToCacheAndPaths(fanartposter, imgpaths, False, , , overwriteimage)
                End If
            End If

            'ExtraFanart
            If shXtraFanart Then
                Dim xfanart As String = currentshowpath & "extrafanart\"
                Dim fanartposter As New List(Of McImage)
                Dim q = From b In artlist Order By b.votes Descending
                If Not q.count = 0 Then
                    fanartposter.AddRange(q.ToList)
                    If fanartposter.Count > 1 Then fanartposter.RemoveAt(0) 'Remove first fanart as used for main fanart
                End If
                If fanartposter.Count > 0 Then
                    Dim x As Integer = 0
                    Do Until x = Pref.TvXtraFanartQty
                        If x = fanartposter.Count Then Exit Do
                        success = Utilities.DownloadFile(fanartposter(x).hdUrl, (xfanart & fanartposter(x).id & ".jpg"))
                        x = x + 1
                    Loop
                End If
            End If
        Catch
        End Try
        Return success

    End Function

    Sub TvCheckfolderjpgart()
        Dim currentshowpath As String = NewShow.FolderPath
        If Pref.tvfolderjpg AndAlso Not File.Exists(currentshowpath & "folder.jpg") Then
            If File.Exists(currentshowpath & "poster.jpg") AndAlso Pref.FrodoEnabled Then
                Utilities.SafeCopyFile(currentshowpath & "poster.jpg", currentshowpath & "folder.jpg", False)
            End If
        End If
        Dim I = 1
        If Pref.seasonfolderjpg Then
            For Each Seas In NewShow.Seasons.Values
                If Seas.FolderPath <> NewShow.FolderPath Then
                    Dim seasonfile As String = Seas.Poster.FileName   '= Seas.SeasonLabel.ToLower.Replace(" ", "")& "-poster.jpg"
                    If File.Exists(NewShow.FolderPath & seasonfile) AndAlso Not File.Exists(Seas.FolderPath & "folder.jpg") Then
                        Utilities.SafeCopyFile(NewShow.FolderPath & seasonfile, Seas.FolderPath & "folder.jpg", False)
                    End If
                End If
            Next
        End If
    End Sub

#Region "original routines"

    Public Function GetPosterList(ByVal TvdbId As String, ByVal ReturnPoster As Boolean) As Tvdb.Banners
        If Not ReturnPoster Then Return Nothing

        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/banners.xml"
        Dim XmlFile As String

        XmlFile = Utilities.DownloadTextFiles(mirrorsurl)

        Dim BannerList As New Tvdb.Banners
        BannerList.LoadXml(XmlFile)
        Return BannerList
    End Function

    Public Function getposterlist(ByVal tvdbid As String, ByRef ListOfBanners As List(Of TvBanners)) As String
        Monitor.Enter(Me)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim wrGETURL As WebRequest
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/banners.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            wrGETURL.Proxy = Utilities.MyProxy
            Dim objStream As IO.Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New IO.StreamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim bannerslist As New XmlDocument
            bannerslist.LoadXml(xmlfile)
            For Each thisresult As XmlNode In bannerslist("Banners")

                Select Case thisresult.Name
                    Case "Banner"
                        Dim banner As New TvBanners
                        Dim bannerselection As XmlNode = Nothing
                        For Each bannerselection In thisresult.ChildNodes
                            Select Case bannerselection.Name
                                Case "id"
                                    banner.id = bannerselection.InnerXml
                                Case "BannerPath"
                                    banner.Url = "http://www.thetvdb.com/banners/" & bannerselection.InnerXml
                                    banner.SmallUrl = "http://www.thetvdb.com/banners/_cache/" & bannerselection.InnerXml
                                Case "BannerType"
                                    banner.BannerType = bannerselection.InnerXml
                                Case "BannerType2"
                                    banner.Resolution = bannerselection.InnerXml
                                Case "Language"
                                    banner.Language = bannerselection.InnerXml
                                Case "Season"
                                    banner.Season = bannerselection.InnerXml
                                Case "Rating"
                                    banner.Rating = bannerselection.InnerText.ToRating
                                Case ""
                            End Select
                        Next
                        ListOfBanners.Add(banner)
                End Select
            Next
            Return "ok"
        Catch ex As WebException
            Return ex.ToString
        Catch EX As Exception
            Return EX.ToString
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function getmirrors()
        Monitor.Enter(Me)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/mirrors.xml"
            Dim wrGETURL As WebRequest = WebRequest.Create(mirrorsurl)

            wrGETURL.Proxy = Utilities.MyProxy
            Dim objStream As IO.Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New IO.StreamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim mirrorslist As New XmlDocument
            'Try
            mirrorslist.LoadXml(xmlfile)
            For Each thisresult As XmlNode In mirrorslist("Mirrors")

                Select Case thisresult.Name
                    Case "Mirror"
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "mirrorpath"
                                    If mirrorselection.InnerText <> Nothing Then
                                        mirrors.Add(mirrorselection.InnerText)
                                    End If
                            End Select
                        Next
                End Select
            Next
            Return mirrors

        Catch EX As Exception
            Return EX.ToString
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function findshows(ByVal title As String, ByVal mirror As String, ByRef showslist As List(Of str_PossibleShowList)) As Boolean
        Monitor.Enter(Me)
        Dim possibleshows As New List(Of str_PossibleShowList)
        Dim xmlfile As String

        title = title.Replace(".", " ")  'Replace periods in foldernames with spaces (linux OS support)
        Dim mirrorsurl As String = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & title & "&language=all"
        Dim wrGETURL As WebRequest = WebRequest.Create(mirrorsurl)
        wrGETURL.Proxy = Utilities.MyProxy
        Dim objStream As IO.Stream
        objStream = wrGETURL.GetResponse.GetResponseStream()
        Dim objReader As New IO.StreamReader(objStream)
        xmlfile = objReader.ReadToEnd
        Dim showlist As New XmlDocument
        Try
            showlist.LoadXml(xmlfile)
            For Each thisresult As XmlNode In showlist("Data")
                Select Case thisresult.Name
                    Case "Series"
                        Dim newshow As New str_PossibleShowList(SetDefaults)
                        For Each mirrorselection As XmlNode In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "seriesid"
                                    newshow.showid = mirrorselection.InnerXml
                                Case "SeriesName"
                                    newshow.showtitle = mirrorselection.InnerXml
                                Case "banner"
                                    newshow.showbanner = "http://www.thetvdb.com/banners/" & mirrorselection.InnerXml
                            End Select
                        Next
                        possibleshows.Add(newshow)
                End Select
            Next

            For Each show In possibleshows
                If show.showid <> Nothing Then showslist.Add(show)
            Next
            Return True
        Catch EX As Exception
            Return False
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function GetShow(ByVal TvdbId As String, ByVal Language As String, ByVal SeriesXmlPath As String) As Tvdb.ShowData

        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/" & Language & ".xml"
        Dim mirrorsurl2 As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/" & "/all/" & Language & ".xml"
        Dim success As Boolean = DownloadCache.Savexmltopath(mirrorsurl2, SeriesXmlPath, TvdbId & ".xml", True)

        Dim showlist As New Tvdb.ShowData
        If success Then     'If download series xml successful
            showlist.Load(SeriesXmlPath & TvdbId & ".xml")
        Else                'Else get just the Show's data.
            Dim xmlfile As String
            xmlfile = Utilities.DownloadTextFiles(mirrorsurl)
            If String.IsNullOrEmpty(xmlfile) Then
                showlist.FailedLoad = True
            Else
                showlist.LoadXml(xmlfile)
            End If
        End If
        If showlist.FailedLoad Then
            Dim xmlfile As String
            xmlfile = Utilities.DownloadTextFiles(mirrorsurl)
            If String.IsNullOrEmpty(xmlfile) Then
                showlist.FailedLoad = True
            Else
                showlist.FailedLoad = False
                showlist.LoadXml(xmlfile)
            End If
        End If
        If Not showlist.FailedLoad Then
            Dim strsplt As New List(Of String)
            Try
                strsplt.AddRange(showlist.Series(0).Genre.Value.Trim("|"c).Split("|"))
            Catch
            End Try
            If strsplt.Count > 0 Then
                Dim NewGenre As String = ""
                For i = 0 To strsplt.Count - 1
                    If i = Pref.TvMaxGenres Then Exit For
                    If NewGenre = "" Then
                        NewGenre = strsplt.Item(i)
                    Else
                        NewGenre &= "|" & strsplt.Item(i)
                    End If
                Next
                showlist.Series(0).Genre.Value = NewGenre
            End If
        End If
        Return showlist
    End Function

    Public Function GetSeriesXml(ByVal TvdbId As String, ByVal Lan As String, ByVal SeriesXmlPath As String) As Boolean
        Dim url As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/" & "/all/" & Lan & ".xml"
        Return DownloadCache.Savexmltopath(url, SeriesXmlPath, TvdbId & ".xml", True)
    End Function

    Public Function GetActors(ByVal TvdbId As String, Optional ByVal maxactors As Integer = 9999) As List(Of str_MovieActors) 'Tvdb.Actors
        If maxactors = 9999 Then
            maxactors = Pref.maxactors
        End If
        Dim results As New List(Of str_MovieActors)
        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/actors.xml"

        Dim xmlfile As String
        xmlfile = Utilities.DownloadTextFiles(mirrorsurl)
        Utilities.CheckForXMLIllegalChars(xmlfile)
        Dim showlist As New Tvdb.Actors
        'Try
        showlist.LoadXml(xmlfile)
        For Each Mc In showlist.Items
            Dim actor As str_MovieActors = New str_MovieActors
            actor.actorid = Mc.Id.Value
            actor.actorname = Utilities.cleanSpecChars(Mc.Name.Value).Trim
            Dim newstring As String
            newstring = Mc.Role.Value
            newstring = newstring.TrimEnd("|")
            newstring = newstring.TrimStart("|")
            newstring = newstring.Replace("|", ", ")
            actor.actorrole = newstring.TrimStart.TrimEnd
            If Mc.Image.Value <> "" Then
                actor.actorthumb = "http://thetvdb.com/banners/_cache/" & Mc.Image.Value
            Else
                actor.actorthumb = ""
            End If
            actor.order = Mc.SortOrder.Value
            results.Add(actor)
        Next
        Return results
    End Function

    Public Function getepisode(ByVal tvdbid As String, ByVal sortorder As String, ByVal seasonno As String, ByVal episodeno As String, ByVal language As String, Optional ByVal forcedownload As Boolean = False)
        Monitor.Enter(Me)
        Dim episodestring As String = ""
        Dim episodeurl As String = ""
        Try
            'http://thetvdb.com/api/6E82FED600783400/series/70726/default/1/1/en.xml
            Dim SeriesXmlPath As String = Utilities.SeriesXmlPath

            Dim xmlfile As String = Nothing
            If language.ToLower.IndexOf(".xml") = -1 Then
                language = language & ".xml"
            End If
            episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seasonno & "/" & episodeno & "/" & language

            'First try seriesxml data
            'check if present, download if not
            Dim gotseriesxml As Boolean = False
            Dim url As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language
            Dim xmlfile2 As String = SeriesXmlPath & tvdbid & ".xml"
            Dim SeriesInfo As New Tvdb.ShowData
            If Not File.Exists(xmlfile2) Then
                gotseriesxml = DownloadCache.Savexmltopath(url, SeriesXmlPath, tvdbid & ".xml", True)
            Else
                'Check series xml isn't older than Five days.  If so, re-download it.
                Dim dtCreationDate As DateTime = File.GetLastWriteTime(xmlfile2)
                Dim datenow As DateTime = Date.Now()
                Dim dif As Long = DateDiff(DateInterval.Day, dtCreationDate, datenow)
                If dif > 5 Then
                    gotseriesxml = DownloadCache.Savexmltopath(url, SeriesXmlPath, tvdbid & ".xml", True)
                Else
                    gotseriesxml = True
                End If
            End If

            If Not gotseriesxml Then
                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seasonno & "/" & episodeno & "/" & language
                xmlfile = Utilities.DownloadTextFiles(episodeurl)
            Else
                SeriesInfo.Load(xmlfile2)
                Dim gotEpxml As Boolean = False
                'check episode is present in seriesxml file, else, re-download it (update to latest)
                If sortorder = "default" Then
                    For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                        If NewEpisode.EpisodeNumber.Value = episodeno AndAlso NewEpisode.SeasonNumber.Value = seasonno Then
                            xmlfile = NewEpisode.Node.ToString
                            xmlfile = "<Data>" & xmlfile & "</Data>"
                            gotEpxml = True
                            Exit For
                        End If
                    Next
                ElseIf sortorder = "dvd" Then
                    For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                        If NewEpisode.DvdEpisodeNumber.Value = (episodeno & ".0") AndAlso NewEpisode.DvdSeason.Value = seasonno Then
                            xmlfile = NewEpisode.Node.ToString
                            xmlfile = "<Data>" & xmlfile & "</Data>"
                            gotEpxml = True
                            Exit For
                        End If
                    Next
                End If
                ' Finally, if not in seriesxml file, go old-school
                If Not gotEpxml Then
                    episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seasonno & "/" & episodeno & "/" & language
                    xmlfile = Utilities.DownloadTextFiles(episodeurl)
                End If
            End If

            episodestring = "<episodedetails>"
            episodestring = episodestring & "<url>" & episodeurl & "</url>"
            Dim mirrorslist As New XmlDocument

            mirrorslist.LoadXml(xmlfile)
            For Each thisresult As XmlNode In mirrorslist("Data")
                Select Case thisresult.Name
                    Case "Episode"
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "EpisodeName"
                                    episodestring = episodestring & "<title>" & mirrorselection.InnerXml & "</title>"
                                Case "FirstAired"
                                    episodestring = episodestring & "<premiered>" & mirrorselection.InnerXml & "</premiered>"
                                Case "GuestStars"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    Try
                                        tempstring = tempstring.TrimStart("|")
                                        tempstring = tempstring.TrimEnd("|")
                                        Dim tvtempstring2 As String
                                        Dim tvtempint As Integer
                                        Dim a() As String
                                        Dim j As Integer
                                        tvtempstring2 = ""
                                        a = tempstring.Split("|")
                                        tvtempint = a.GetUpperBound(0)
                                        tvtempstring2 = a(0)
                                        If tvtempint >= 0 Then
                                            For j = 0 To tvtempint
                                                Try
                                                    episodestring = episodestring & "<actor>" & "<name>" & a(j) & "</name></actor>"
                                                Catch
                                                End Try
                                            Next
                                        End If
                                    Catch
                                    End Try
                                Case "Director"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.TrimEnd("|")
                                    episodestring = episodestring & "<director>" & tempstring & "</director>"
                                Case "Writer"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.TrimEnd("|")
                                    episodestring = episodestring & "<credits>" & tempstring & "</credits>"
                                Case "Overview"
                                    episodestring = episodestring & "<plot>" & mirrorselection.InnerXml & "</plot>"
                                Case "Rating"
                                    episodestring = episodestring & "<rating>" & mirrorselection.InnerXml & "</rating>"
                                Case "RatingCount"
                                    episodestring = episodestring & "<ratingcount>" & mirrorselection.InnerXml & "</ratingcount>"
                                Case "id"
                                    episodestring = episodestring & "<uniqueid>" & mirrorselection.InnerXml & "</uniqueid>"
                                Case "IMDB_ID"
                                    episodestring = episodestring & "<imdbid>" & mirrorselection.InnerXml & "</imdbid>"
                                Case "filename"
                                    episodestring = episodestring & "<thumb>http://www.thetvdb.com/banners/" & mirrorselection.InnerXml & "</thumb>"
                                Case "airsbefore_episode"
                                    episodestring = episodestring & "<displayepisode>" & mirrorselection.InnerXml & "</displayepisode>"
                                Case "airsbefore_season"
                                    episodestring = episodestring & "<displayseason>" & mirrorselection.InnerXml & "</displayseason>"
                            End Select
                        Next
                End Select
            Next
            episodestring = episodestring & "</episodedetails>"
            Return episodestring
        Catch ex As Exception
            Return "ERROR - <url>" & episodeurl & "</url>"
        Finally
            Monitor.Exit(Me)
        End Try

    End Function

    Public Function getepisodefromxml(ByVal tvdbid As String, ByVal sortorder As String, ByVal seriesno As String, ByVal episodeno As String, ByVal language As String, Optional ByVal forcedownload As Boolean = False) As Tvdb.Episode
        Monitor.Enter(Me)
        Dim episodeurl As String = ""
        Dim thisepisode As New Tvdb.Episode
        Try
            'http://thetvdb.com/api/6E82FED600783400/series/70726/default/1/1/en.xml

            Dim xmlfile As String
            If language.ToLower.IndexOf(".xml") = -1 Then language = language & ".xml"
            episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seriesno & "/" & episodeno & "/" & language

            xmlfile = Utilities.DownloadTextFiles(episodeurl, forcedownload) 'this function has gzip detection in it 
            Dim xmlOK As Boolean = Utilities.CheckForXMLIllegalChars(xmlfile)
            Dim mirrorslist As New XmlDocument

            mirrorslist.LoadXml(xmlfile)
            For Each thisresult As XmlNode In mirrorslist("Data")
                Select Case thisresult.Name
                    Case "Episode"
                        For Each thisresult2 As XmlNode In thisresult.ChildNodes
                            Select Case thisresult2.Name
                                Case "EpisodeName"
                                    thisepisode.EpisodeName.Value = thisresult2.InnerXml
                                Case "FirstAired"
                                    thisepisode.FirstAired.Value = thisresult2.InnerXml
                                Case "GuestStars"
                                    thisepisode.GuestStars.Value = thisresult2.InnerXml
                                Case "Director"
                                    thisepisode.Director.Value = thisresult2.InnerXml
                                Case "Writer"
                                    thisepisode.Writer.Value = thisresult2.InnerXml
                                Case "Overview"
                                    thisepisode.Overview.Value = thisresult2.InnerXml
                                Case "Rating"
                                    thisepisode.Rating.Value = thisresult2.InnerXml
                                Case "RatingCount"
                                    thisepisode.Votes.Value = thisresult2.InnerXml
                                Case "id"
                                    thisepisode.Id.Value = thisresult2.InnerXml
                                Case "filename"
                                    thisepisode.ThumbNail.Value = "http://www.thetvdb.com/banners/" & thisresult2.InnerXml
                            End Select
                        Next
                End Select
            Next
        Catch ex As Exception
            thisepisode.FailedLoad = True
        Finally
            Monitor.Exit(Me)
        End Try
        Return thisepisode
    End Function
#End Region
End Class
