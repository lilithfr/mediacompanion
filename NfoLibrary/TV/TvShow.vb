Imports ProtoXML
Imports System.Net
Imports System.IO
Imports System.Xml
Imports Media_Companion

Public Class TvShow
    Inherits ProtoFile

    Public Property Id As New ProtoProperty(Me, "id")
    Public Property TvdbId As String
        Get
            Return Id.Value
        End Get
        Set(ByVal value As String)
            Id.Value = value
        End Set
    End Property

    Public Property Title As New ProtoProperty(Me, "title")
    Public Property Rating As New ProtoProperty(Me, "rating")
    Public Property Year As New ProtoProperty(Me, "year")
    Public Property Top250 As New ProtoProperty(Me, "top250")

    Public Property Season As New ProtoProperty(Me, "season") 'I can't see what use this is, it's included incase someone figures it out - Perhaps a deault for show that don't include one?
    Public Property EpisodeCount As New ProtoProperty(Me, "episode")
    Public Property DisplaySeason As New ProtoProperty(Me, "displayseason") 'Can't figure this one out either 
    Public Property DisplayEpisode As New ProtoProperty(Me, "displayepisode") 'Or this one
    Public Property Votes As New ProtoProperty(Me, "votes")
    Public Property Plot As New ProtoProperty(Me, "plot")
    Public Property Outline As New ProtoProperty(Me, "outline") 'Same as plot?
    Public Property TagLine As New ProtoProperty(Me, "tagline")
    Public Property Runtime As New ProtoProperty(Me, "runtime")
    Public Property Mpaa As New ProtoProperty(Me, "mpaa")
    Public Property LastPlayed As New ProtoProperty(Me, "lastplayed")
    Public Property Genre As New ProtoProperty(Me, "genre")
    Public Property Credits As New ProtoProperty(Me, "credits")
    Public Property [Set] As New ProtoProperty(Me, "set") 'No clue what this does, not sure if it's a hold over from movies or if show sets are possible (mini-series, spin-offs extra)
    Public Property Director As New ProtoProperty(Me, "director")
    Public Property Premiered As New ProtoProperty(Me, "premiered")
    Public Property Status As New ProtoProperty(Me, "status") 'No clue what this does
    Public Property Code As New ProtoProperty(Me, "code") 'No clue what this does
    Public Property Aired As New ProtoProperty(Me, "aired") '
    Public Property Studio As New ProtoProperty(Me, "studio")
    Public Property Trailer As New ProtoProperty(Me, "trailer") 'Also possible hold over from movies
    Public Property Artist As New ProtoProperty(Me, "Artist") 'Possible hold over from Music?

    Public Property EpisodeGuideUrl As New ProtoProperty(Me, "episodeguideurl")

    Public Property ListActors As New ActorList(Me, "actor")

    Public Property ImageFanart As New ProtoImage(Me, "fanart", Utilities.DefaultFanartPath) With {.FileName = "fanart.jpg"}
    Public Property ImagePoster As New ProtoImage(Me, "poster", Utilities.DefaultPosterPath) With {.FileName = "folder.jpg"}
    Public Property ImageBanner As New ProtoImage(Me, "banner", Utilities.DefaultBannerPath) With {.FileName = "folder.jpg"}
    Public Property ImageAllSeasons As New ProtoImage(Me, "allseasons", Utilities.DefaultPosterPath) With {.FileName = "seasonall.tbn"}
    Public Property ImageClearArt As New ProtoImage(Me, "clearart", Utilities.DefaultBannerPath) With {.FileName = "clearart.png"}
    Public Property ImageLogo As New ProtoImage(Me, "logo", Utilities.DefaultBannerPath) With {.FileName = "logo.png"}

    'Media Companion Specific

    Public Property ImdbId As New ProtoProperty(Me, "imdbid") 'XBMC doesn't seem to use this.
    Public Property SortOrder As New ProtoProperty(Me, "sortorder")
    Public Property Language As New ProtoProperty(Me, "language")
    Public Property TvShowActorSource As New ProtoProperty(Me, "tvshowactorsource")
    Public Property EpisodeActorSource As New ProtoProperty(Me, "episodeactorsource")

    Private Property _State As New ProtoProperty(Me, "state")
    Public Shadows Property State As Nfo.ShowState
        Get
            Select Case _State.Value
                Case Nfo.ShowState.Open
                    ShowNode.ImageKey = "blank"
                    ShowNode.SelectedImageKey = "blank"
                Case Nfo.ShowState.Locked
                    ShowNode.ImageKey = "padlock"
                    ShowNode.SelectedImageKey = "padlock"
                Case Nfo.ShowState.Unverified
                    ShowNode.ImageKey = "qmark"
                    ShowNode.SelectedImageKey = "qmark"
                Case Nfo.ShowState.Error
                    ShowNode.ImageKey = "error"
                    ShowNode.SelectedImageKey = "error"
            End Select
            Return CType(_State.Value, Nfo.ShowState)
        End Get
        Set(ByVal value As Nfo.ShowState)
            _State.Value = value
            Select Case _State.Value
                Case Nfo.ShowState.Open
                    ShowNode.ImageKey = "blank"
                    ShowNode.SelectedImageKey = "blank"
                Case Nfo.ShowState.Locked
                    ShowNode.ImageKey = "padlock"
                    ShowNode.SelectedImageKey = "padlock"
                Case Nfo.ShowState.Unverified
                    ShowNode.ImageKey = "qmark"
                    ShowNode.SelectedImageKey = "qmark"
                Case Nfo.ShowState.Error
                    ShowNode.ImageKey = "error"
                    ShowNode.SelectedImageKey = "error"
            End Select
        End Set
    End Property

    'Non-xml properties
    Public Property Seasons As New Dictionary(Of String, TvSeason)
    Public Property Episodes As New List(Of TvEpisode)

    Public ReadOnly Property MissingEpisodes As List(Of TvEpisode)
        Get
            Dim TempList As New List(Of TvEpisode)

            For Each Ep As TvEpisode In Me.Episodes
                If Ep.IsMissing Then
                    TempList.Add(Ep)
                End If
            Next
            Return TempList
        End Get
    End Property

    Public Property posters As New List(Of String)
    Public Property fanart As New List(Of String)

    

    Sub New()
        MyBase.New("tvshow")
    End Sub

    Public ReadOnly Property TitleAndYear As String
        Get
            Return Title.Value & " " & Year.Value
        End Get
    End Property

    Dim _PossibleShowList As List(Of Media_Companion.Tvdb.Series)
    Public Property PossibleShowList As List(Of Media_Companion.Tvdb.Series)
        Get
            If _PossibleShowList Is Nothing Then
                Me.GetPossibleShows()
            End If

            Return _PossibleShowList
        End Get
        Set(ByVal value As List(Of Media_Companion.Tvdb.Series))
            _PossibleShowList = value
        End Set
    End Property

    Public Sub GetPossibleShows()
        'Dim possibleshows As New List(Of possibleshowlist)
        Dim xmlfile As String
        If String.IsNullOrEmpty(Me.FolderPath) Then
            Exit Sub
        End If
        'TODO: Properly encode URL
        'TODO: remove articles (And, &, The, ext)... Tvdb search is extremly litteral with absolutly no text adjustment

        Dim mirrorsurl As String
        If Title.Value Is Nothing Then
            mirrorsurl = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & Media_Companion.Utilities.GetLastFolder(Me.FolderPath) & "&language=all"
        Else
            mirrorsurl = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & Title.Value & "&language=all"
        End If

        xmlfile = Media_Companion.Utilities.DownloadTextFiles(mirrorsurl)

        Dim ReturnData As New Media_Companion.Tvdb.ShowData

        ReturnData.LoadXml(xmlfile)

        If _PossibleShowList Is Nothing Then _PossibleShowList = New List(Of Media_Companion.Tvdb.Series)
        _PossibleShowList.AddRange(ReturnData.Series.List)
    End Sub

    Public Sub AbsorbTvdbSeries(ByVal Series As Tvdb.Series)
        Me.Id.Value = Series.Id.Value
        Me.TvdbId = Series.Id.Value
        Me.Mpaa.Value = Series.ContentRating.Value
        Me.Genre.Value = Series.Genre.Value
        Me.ImdbId.Value = Series.ImdbId.Value
        Me.Plot.Value = Series.Overview.Value
        Me.Title.Value = Series.SeriesName.Value
        Me.Runtime.Value = Series.RunTimeWithCommercials.Value
        Me.Rating.Value = Series.Rating.Value
        Me.Premiered.Value = Series.FirstAired.Value
        Me.Studio.Value = Series.Network.Value

    End Sub

    Public Sub SearchForEpisodesInFolder()



        Dim newlist As New List(Of String)
        newlist.Clear()

        newlist = Utilities.EnumerateFolders(Me.FolderPath, 6) 'TODO: Restore loging functions

        newlist.Insert(0, Me.FolderPath)
     
        For Each folder In newlist
            Dim dir_info As New System.IO.DirectoryInfo(folder)

            Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles("*.NFO", SearchOption.TopDirectoryOnly)
            For Each fs_info As System.IO.FileInfo In fs_infos
                'Application.DoEvents()
                If IO.Path.GetFileName(fs_info.FullName.ToLower) <> "tvshow.nfo" Then
                    Dim NewEpisode As New TvEpisode
                    NewEpisode.NfoFilePath = fs_info.FullName
                    NewEpisode.Load()

                    Me.AddEpisode(NewEpisode)
                End If

            Next fs_info
        Next
    End Sub

    Public Sub AddEpisode(ByRef Episode As TvEpisode)
        If Not Cache.TvCache.Contains(Episode) Then
            Cache.TvCache.Add(Episode)
        End If

        If Not Me.Episodes.Contains(Episode) Then
            Me.Episodes.Add(Episode)
        Else
            Exit Sub
        End If


        If Episode.Season.Value IsNot Nothing AndAlso Not Me.Seasons.ContainsKey(Episode.Season.Value) Then
            Dim NewSeason As New TvSeason

            If Utilities.IsNumeric(Episode.Season.Value) Then
                NewSeason.SeasonNumber = Episode.Season.Value
                NewSeason.SeasonLabel = "Season " & Utilities.PadNumber(Episode.Season.Value, 2)
                NewSeason.Poster.FolderPath = Me.FolderPath
                NewSeason.Poster.FileName = "season" & Utilities.PadNumber(Episode.Season.Value, 2) & ".tbn"
            Else
                NewSeason.SeasonNumber = -1
                NewSeason.SeasonLabel = Episode.Season.Value
                NewSeason.Poster.FolderPath = Me.FolderPath
                NewSeason.Poster.FileName = "season-all.tbn"
            End If


            Me.ShowNode.Nodes.Add(NewSeason.SeasonNode)
            NewSeason.UpdateTreenode()
            NewSeason.SeasonNode.Nodes.Add(Episode.EpisodeNode)
            NewSeason.Episodes.Add(Episode)
            NewSeason.ShowObj = Me
            Me.Seasons.Add(Episode.Season.Value, NewSeason)
            NewSeason.ShowId.Value = Me.Id.Value
            Episode.SeasonObj = NewSeason
        ElseIf Episode.Season.Value IsNot Nothing Then
            Me.Seasons(Episode.Season.Value).SeasonNode.Nodes.Add(Episode.EpisodeNode)
            Me.Seasons(Episode.Season.Value).Episodes.Add(Episode)
            Me.Seasons(Episode.Season.Value).UpdateTreenode()
            Episode.SeasonObj = Me.Seasons(Episode.Season.Value)

        Else
            Dim Test = False
        End If
        Episode.ShowObj = Me
        Episode.ShowId.Value = Me.Id.Value
    End Sub

    Private _Visible As Boolean
    Public Property Visible As Boolean
        Get
            If _Visible Then
                ShowNode.ForeColor = Drawing.Color.Black
            Else
                ShowNode.ForeColor = Drawing.Color.LightGray
            End If
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
            If _Visible Then
                ShowNode.ForeColor = Drawing.Color.Black
            Else
                ShowNode.ForeColor = Drawing.Color.LightGray
            End If
        End Set
    End Property


    Public ReadOnly Property VisibleEpisodeCount As Integer
        Get
            Dim Count As Integer = 0
            For Each Ep As TvEpisode In Episodes
                If Ep.Visible Then
                    Count += 1
                End If
            Next
            Return Count
        End Get
    End Property
    Public ReadOnly Property VisibleSeasonCount As Integer
        Get
            Dim Count As Integer = 0
            For Each Ep As TvSeason In Seasons.Values
                If Ep.Visible Then
                    Count += 1
                End If
            Next
            Return Count
        End Get
    End Property



    Public Sub SearchForNewEpisodes()
        'Enumerate Files in this shows folder
        '   Does it have an nfo file?
        '       Yes? Is it already added to the tv show?
        '           Load if nececery and skip without comment
        '       No? Create an episode object for the file, add it to the show
        'Enumerate all shows and make sure they are nfoed and if not scrape
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Title.Value
    End Function
End Class

Public Enum ShowState
    Open
    Locked
    Unverified
    [Error]
End Enum