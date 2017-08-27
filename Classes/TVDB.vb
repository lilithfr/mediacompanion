Imports System.Linq
Imports System.Collections.Generic
'Imports System.IO
Imports Alphaleonis.Win32.Filesystem

'Temp name till Get all routines under one umbrella
Public Class TVDBScraper2

    Public Key          = Utilities.TVDBAPI
    Public Const TVDB_EXC_MSG = "TVDb is unavailable!"
        
    #Region "Private Properties"
    
    Private _language           As String
    Private _tvdbId             As String
    Private _imdb               As String
    Private _title              As String
    Dim arrLetters1
    Dim arrLetters2
    Private _api                        As TheTvDB.TvdbAPI = Pref.TVDbapi
    Private _tvdblanguages              As TheTvDB.TvdbLanguagesResult
    Private _config_images_base_url     As String = "http://thetvdb.com/banners/"
    Private _series                     As New TheTvDB.TvdbSeries
    Private _searchresults              As New TheTvDB.TvdbSeriesSearchResult
    Private _notfound                   As Boolean = False
    Private _episode                    As New TheTvDB.TvdbEpisode
    Private _seriesImages               As New List(Of TheTvDB.TvdbBanner)
    Private _seriesImage                As TheTvDB.TvdbImageSummaryResult
    Private _actors                     As TheTvDB.TvdbActorsResult
    Private _actor                      As TheTvDB.TvdbActor
    Private _mcPosters                  As New List(Of McImage)
    Private _mcFanart                   As New List(Of McImage)
    Private _mcSeason                   As New List(Of McImage)
    Private _mcSeasonWide               As New List(Of McImage)
    Private _cast                       As TheTvDB.TvdbActorsResult
    Private _frodoPosterThumbs          As New List(Of FrodoPosterThumb)
    Private _frodoFanartThumbs          As New FrodoFanartThumbs

    Private _PossibleShowList           As List(Of TheTvDB.TvdbSeries)
    Private _fetched                    As Boolean = False

    #End Region

    #Region "Read-write Properties"
    
    Public Property Imdb As String
        Get
            Return _imdb 
        End Get
        Set(ByVal value As String)
            If _imdb <> value Then
                _imdb = value
                _fetched = False
            End If
        End Set
    End Property
    
    Public Property TvdbId As String
        Get
            Return _tvdbId 
        End Get
        Set(ByVal value As String)
            If _tvdbId <> value Then
                _tvdbId = value
                _fetched = False
            End If
        End Set
    End Property
    
    Public Property Title As String
        Get
            Return _title 
        End Get
        Set(ByVal value As String)
            If _title <> value Then
                _title = value
                _fetched = False
            End If
        End Set
    End Property

    Public Property LookupLang
        Get
            Return _language
        End Get
        Set(value)
            _language = value   
        End Set
    End Property
    
    Public Property ValidFanart         As New List(Of TheTvDB.TvdbBanner)
    Public Property ValidPosters        As New List(Of TheTvDB.TvdbBanner)
    Public Property ValidSeason         As New List(Of TheTvDB.TvdbBanner)
    Public Property ValidSeasonWide     As New List(Of TheTvDB.TvdbBanner)
    Public Property MaxGenres           As Integer = Media_Companion.Pref.maxmoviegenre

    #End Region 'Read-write properties

    #Region "Read-only Properties"
    
    Public Readonly Property SeriesNotFound As Boolean
        Get
            Return _notfound
        End Get
    End Property

    Public ReadOnly Property Config_images_base_url As String
        Get
            Return _config_images_base_url
        End Get 
    End Property

    Public ReadOnly Property FrodoPosterThumbs As List(Of FrodoPosterThumb)
        Get
            Fetch
            Return _frodoPosterThumbs
        End Get
    End Property

    Public ReadOnly Property FrodoFanartThumbs As FrodoFanartThumbs
        Get
            Fetch
            Return _frodoFanartThumbs
        End Get
    End Property

    Public ReadOnly Property Cast As List(Of str_MovieActors)
        Get
            Fetch
            FetchCast
            Dim alist As New List(Of str_MovieActors)
            Try
                If Not IsNothing(_series.Actors) AndAlso _series.Actors.Count > 0 Then
                    Dim i As Integer = 0
                    For each c In _series.Actors
                        If i = Pref.maxactors Then Exit For
                        i = i + 1
                        Dim newact As New str_MovieActors
                        newact.actorid      = c.Identity.ToString
                        newact.actorname    = c.Name
                        newact.actorrole    = c.Character
                        newact.actorthumb   = If(String.IsNullOrEmpty(c.Image), "", "http://thetvdb.com/banners/_cache/" & c.Image)
                        newact.order        = c.SortOrder  '_cast.cast(i).order
                        alist.Add(newact)
                    Next
                Else
                    Dim x = _cast.Actors.Count
                    If x < 1 Then Return alist
                    For i = 0 to Pref.maxactors-1
                        If x = i Then Exit For
                        Dim newact As New str_MovieActors
                        newact.actorid      = _cast.Actors(i).Identity   '_cast.cast(i).id
                        newact.actorname    = _cast.Actors(i).Name       '_cast.cast(i).name
                        newact.actorrole    = _cast.Actors(i).Character  '_cast.cast(i).character
                        newact.actorthumb   = If(String.IsNullOrEmpty(_cast.Actors(i).Image), "", "http://thetvdb.com/banners/_cache/" & _cast.Actors(i).Image)
                        'newact.actorthumb   = If(_cast.cast(i).profile_path = Nothing, "", "http://image.tmdb.org/t/p/original" &_cast.cast(i).profile_path)
                        newact.order        = _cast.Actors(i).SortOrder  '_cast.cast(i).order
                        alist.Add(newact)
                    
                    Next
                End If
                
            Catch
            End Try
            Return alist
        End Get
    End Property

    Public ReadOnly Property Series As TheTvDB.TvdbSeries
        Get
            Fetch
            Return _series
        End Get 
    End Property

    Public ReadOnly Property releasedate As String
        Get
            Fetch
            Return Series.FirstAiredString
        End Get
    End Property

    Public ReadOnly Property Genres As String
        Get
            Fetch

            Dim s As String = ""
            Dim i As Integer
            For Each genre In _series.Genres
                s += genre + ", "

                i += 1
                If i >= MaxGenres then
                    Exit For
                End If
            Next

            If s <> "" then
                Return s.Substring(0,s.Length-2)
            End If
            Return ""
        End Get 
    End Property
    
    Public ReadOnly Property Director As String
        Get
            Fetch
            'FetchEpisode
            Return _episode.DirectorsDisplayString
            
            Return ""
        End Get 
    End Property

    Public ReadOnly Property Credits As String
        Get
            Fetch
            'FetchEpisode
            Return _episode.WritersDisplayString
            
            Return ""
        End Get 
    End Property
    
    Public Property PossibleShowList As List(Of TheTvDB.TvdbSeries)
        Get
            If _PossibleShowList Is Nothing Then Me.GetPossibleShows()

            Return _PossibleShowList
        End Get
        Set(ByVal value As List(Of TheTvDB.TvdbSeries))
            _PossibleShowList = value
        End Set
    End Property

    Public ReadOnly Property McPosters As List(Of McImage)
        Get
            Fetch
            Return _mcPosters
        End Get
    End Property

    Public ReadOnly Property McFanart As List(Of McImage)
        Get
            Fetch
            Return _mcFanart
        End Get
    End Property

    Public ReadOnly Property McSeason As List(Of McImage)
        Get
            Fetch
            Return _mcSeason
        End Get
    End Property

    Public ReadOnly Property McSeasonWide As List(Of McImage)
        Get
            Fetch
            Return _mcSeasonWide
        End Get
    End Property

    Public ReadOnly Property FirstOriginalPosterUrl As String
        Get
            Fetch

            If ValidPosters.Count=0 then
                Return ""
            End If

            Return ""
        End Get 
    End Property

    Public ReadOnly Property HdPath As String
        Get
            Return _config_images_base_url
        End Get 
    End Property

    Public ReadOnly Property LdBackDropPath As String
        Get
            Return _config_images_base_url + "w1280"
        End Get 
    End Property

    Public ReadOnly Property LdPosterPath As String
        Get
            Return _config_images_base_url + "w500"
        End Get 
    End Property
    
    'Public ReadOnly Property Genrelist As List(Of String)
    '    Get
    '        Fetch
    '        'FetchGenreList
    '        Dim genres As New List(Of String)
    '        For Each g In _genrelist.genres 
    '            genres.Add(g.ToString)
    '        Next
    '        Return genres
    '    End Get
    'End Property

    'Public ReadOnly Property Certification As String
    '    Get
    '        Fetch
    '        'FetchReleases
    '        Try
    '            If IsNothing(_releases.countries) Then Return ""
    '            For Each country In _releases.countries
    '                If country.iso_3166_1.ToLower = LookupLanguages.Item(0) then
    '                    Return country.certification
    '                End If
    '            Next

    '            For Each country In _releases.countries
    '                If country.iso_3166_1.ToLower = Pref.XbmcTmdbScraperCertCountry Then
    '                    Return country.certification
    '                End If
    '            Next

    '            For Each country In _releases.countries
    '                If country.certification <> "" then
    '                    Return country.certification
    '                End If
    '            Next
    '            Return ""
    '        Catch
    '            Return ""
    '        End Try
    '    End Get 
    'End Property

    'Function GetGenreList As Boolean
    '    _genrelist = _api.GetGenreList(LookupLanguages.Item(0))
    '    Return Not IsNothing(_genrelist)
    'End Function

    'Private Sub FetchGenreList
    '    If IsNothing(_genrelist) Then
    '        If Not (New RetryHandler(AddressOf GetGenreList)).Execute Then Throw New Exception(TVDB_EXC_MSG)
    '    End If
    'End Sub
    
    'Function GetMovieReleases As Boolean
    '    _releases = _api.GetMovieReleases(_movie.id)
    '    Return Not IsNothing(_releases)
    'End Function

    'Private Sub FetchReleases
    '    If IsNothing(_releases) then
    '        If Not (new RetryHandler(AddressOf GetMovieReleases)).Execute Then Throw New Exception(TVDB_EXC_MSG)
    '    End If
    'End Sub
    
    'Function GetMovieAlternateTitles As Boolean
    '    _alternateTitles = _api.GetMovieAlternateTitles(Movie.id,LookupLanguages.Item(0))
    '    Return Not IsNothing(_alternateTitles)
    'End Function

    'Public ReadOnly Property AlternateTitles As List(Of String)
    '    Get
    '        Fetch
    '        If IsNothing(_alternateTitles) then 
    '            If Not (new RetryHandler(AddressOf GetMovieAlternateTitles)).Execute Then Throw New Exception(TVDB_EXC_MSG)
    '        End If

    '        _mcAlternateTitles.Clear

    '        For Each item In _alternateTitles.titles
    '            _mcAlternateTitles.Add(item.title)
    '        Next

    '        Return _mcAlternateTitles
    '    End Get 
    'End Property
    
    'Public ReadOnly Property MovieImages As WatTmdb.V3.TmdbMovieImages
    '    Get
    '        Fetch
    '        Return _movieImages
    '    End Get 
    'End Property
    
    'Public ReadOnly Property Thumbs As List(Of String)
    '    Get
    '        Fetch
    '        Return _thumbs
    '    End Get 
    'End Property

    'Public ReadOnly Property Trailers As WatTmdb.V3.TmdbMovieTrailers
    '    Get
    '        Fetch
    '        Return _trailers
    '    End Get 
    'End Property

    'Public ReadOnly Property Keywords As List(Of String)
    '    Get
    '        Fetch
    '        Return _keywords 
    '    End Get
    'End Property

    #End Region  'Read-only properties

    Sub new( Optional __tvdb As String=Nothing, Optional _lang As String = "en")
        '_api         = New TheTvDB.TvdbAPI(Key)
        'AssignConfig_images_base_url
        LookupLang    = _lang
        TvdbId        = __tvdb
    End Sub
    
    Function GetSeriesCast As Boolean
        _cast = _api.GetSeriesActors(TvdbId, Nothing)
        Return Not IsNothing(_cast)
    End Function
    
    Private Sub FetchCast
        If IsNothing(_cast) AndAlso IsNothing(_series.Actors) Then
            If Not (new RetryHandler(AddressOf GetSeriesCast)).Execute Then Throw New Exception(TVDB_EXC_MSG)
        End If
    End Sub
    
    Function GetSeries As Boolean
        If Title <> "" Then
            Return GetSeriesByTitle
        End If
        Return False
    End Function

    Function GetSeriesByTitle As Boolean
        'Dim reply As Object = Nothing
        '_searchresults  = _api.GetSeries(Title, reply)
        'If IsNothing(_searchresults) Then
        '    _notfound = True
        '    Return True
        'End If

        'Dim tmpseries As TheTvDB.TvdbSeries = FindBestPossibleShow(_searchresults.series.ToList, Title, LookupLang)
        '_tvdbId         = tmpseries.Identity

        Dim tvresults As TheTvDB.TvdbSeriesInfoResult  = _api.GetSeriesDetails(_tvdbid, Nothing, LookupLang)
        tvresults.Series.LoadDetails(_api, LookupLang)
        _series = tvresults.Series
        Return True
    End Function

    Function GetSeriesImages As Boolean
        _series.LoadBanners(_api)
        _seriesImages = _series.Banners.ToList
        Return Not IsNothing(_seriesImages)
    End Function
    
    Function GetTvdbLanguages As TheTvDB.TvdbLanguagesResult
        Return _api.GetTvdbLanguages(Nothing)
    End Function

    Private Sub Fetch
        Try
            If _series.SeriesId = 0 And Not _fetched Then
                _fetched = True
                Dim rhs As List(Of RetryHandler) = New List(Of RetryHandler)
                rhs.Add(New RetryHandler(AddressOf GetSeries        ))
                rhs.Add(New RetryHandler(AddressOf GetSeriesImages  ))

                For Each rh In rhs
                    If Not rh.Execute Then Throw New Exception(TVDB_EXC_MSG)
                Next
                
                If _notfound Then Exit Sub
                AssignValidFanart()
                AssignValidPosters()
                AssignValidSeason()
                AssignValidSeasonWide()
                AssignMcPosters()
                AssignMcFanart()
                AssignMcSeason()
                AssignMcSeasonWide()
                'AssignKeywords()
            End If
        Catch ex As Exception
            Throw New Exception (ex.Message)
        End Try

    End Sub
    
#Region "Series Images"

    Private Sub AssignValidFanart
        Dim q = From b In _seriesImages Where b.KeyType = "fanart"
        For each item In q
            ValidFanart.AddIfNew(item)
        Next
    End Sub
    
    Private Sub AssignValidPosters
        Dim q = From b In _seriesImages Where b.KeyType = "poster"
        For each item In q
            ValidPosters.AddIfNew(item)
        Next
    End Sub

    Private Sub AssignValidSeason
        Dim q = From b In _seriesImages Where b.KeyType = "season"
        For each item In q
            ValidSeason.AddIfNew(item)
        Next
    End Sub

    Private Sub AssignValidSeasonWide
        Dim q = From b In _seriesImages Where b.KeyType = "seasonwide"
        For each item In q
            ValidSeasonWide.AddIfNew(item)
        Next
    End Sub
    
    Private Sub AssignMcPosters
        AssignMcPosters(ValidPosters,_mcPosters)
    End Sub
    
    Private Sub AssignMcFanart
        AssignMcPosters(ValidFanart,_mcFanart)
    End Sub
    
    Private Sub AssignMcSeason
        AssignMcPosters(ValidSeason, _mcSeason)
    End Sub

    Private Sub AssignMcSeasonWide
        AssignMcPosters(ValidSeasonWide, _mcSeasonWide)
    End Sub

    Private Sub AssignMcPosters( tmDbImages As Object, mcImages As List(Of McImage))
        Dim tmpimages As New List(Of McImage)
        For Each item In tmDbImages
            tmpimages.Add( McImage.GetFromTvDbBackDrop(item, HdPath))
            'mcImages.Add( McImage.GetFromTmDbBackDrop(item,HdPath,LdPosterPath) )
        Next
        If Not tmpimages.Count = 0 Then
            Dim q = From x In tmpimages Order By x.votes Descending
            mcImages.AddRange(q.ToList)
        End If
    End Sub
 
    Function getyear(ifdate As String) As String
        Dim isyear As String = ""
        Try
            If IsDate(ifdate) Then
				Dim x = Convert.ToDateTime(ifdate)

				isyear = x.Year.ToString
			End If
        Catch
        End Try
        Return isyear
    End Function

    Function SelectBackDrop( width As Integer ) As WatTmdb.V3.Backdrop
        Fetch
        'Dim q = From b In ValidBackDrops Where b.width = width Order By b.vote_average Descending

        'If q.Count = 0 then
        '    q = From b In ValidBackDrops Where b.width > width Order By b.vote_average Descending
        'End If

        'If q.Count = 0 then
        '    q = From b In ValidBackDrops Order By b.width Descending, b.vote_average Descending
        'End If

        'If q.Count = 0 then
        '    return Nothing
        'End If

        'Return q.First
        '''temp
        Return New wattmdb.v3.Backdrop
    End Function
    
#End Region

    Private Sub GetPossibleShows()
        Dim reply As Object = Nothing
        _searchresults  = _api.GetSeries(Title, reply)
        If _searchresults.Series.Count > 0 Then _PossibleShowList = New List(Of TheTvDB.TvdbSeries)
        For each show In _searchresults.series
            _PossibleShowList.Add(show)
        Next

    End Sub
    Public Function FindBestPossibleShow(ByVal ThisList As List(Of TheTvDB.TvdbSeries), ByVal FolderName As String, ByVal PreferedLang As String) As TheTvDB.TvdbSeries
        FolderName = FolderName.Replace(".", " ") ' we remove periods to find the title, we should also do it here to compare
        For Each Item In ThisList
            'Item.Similarity = Item.SeriesName.Value.CompareString(FolderName)
            'Item.Similarity = Tvdb.CompareString(Item.SeriesName.Value, FolderName)
            Item.Similarity = CompareString(Item.SeriesName, FolderName)
        Next

        Dim Search = From Ser As TheTvDB.TvdbSeries In ThisList Order By Ser.Similarity Descending ', Ser.FirstAired Descending

        If Search.Count > 0 Then
            Dim Test As Thetvdb.TvdbSeries = Search.FirstOrDefault()
            Return Test
        End If

        'Catch All
        Return ThisList.Item(0)
    End Function

    Public Function CompareString(String1 As String, String2 As String) As Double
        Dim intLength1
        Dim intLength2
        Dim x
        Dim dblResult


        If UCase(String1) = UCase(String2) Then
            dblResult = 1
        Else
            intLength1 = Len(String1)
            intLength2 = Len(String2)


            If intLength1 = 0 Or intLength2 = 0 Then
                dblResult = 0
            Else
                ReDim arrLetters1(intLength1 - 1)
                ReDim arrLetters2(intLength2 - 1)

                For x = LBound(arrLetters1) To UBound(arrLetters1)
                    arrLetters1(x) = Asc(UCase(Mid(String1, x + 1, 1)))
                Next

                For x = LBound(arrLetters2) To UBound(arrLetters2)
                    arrLetters2(x) = Asc(UCase(Mid(String2, x + 1, 1)))
                Next

                dblResult = SubSim(1, intLength1, 1, intLength2) / (intLength1 + intLength2) * 2
            End If
        End If

        CompareString = dblResult
    End Function

    Private Function SubSim(intStart1, intEnd1, intStart2, intEnd2) As Double
            Dim intMax As Integer = Integer.MinValue

            Try
                Dim y
                Dim z
                Dim ns1 As Integer
                Dim ns2 As Integer
                Dim i

                If (intStart1 > intEnd1) Or (intStart2 > intEnd2) Or (intStart1 <= 0) Or (intStart2 <= 0) Then
                    Return 0
                End If

                For y = intStart1 To intEnd1
                    For z = intStart1 To intEnd2
                        i = 0

                        Do Until arrLetters1(y - 1 + i) <> arrLetters2(z - 1 + i)
                            i = i + 1

                            If i > intMax Then
                                ns1 = y
                                ns2 = z
                                intMax = i
                            End If

                            If ((y + i) > intEnd1) Or ((z + i) > intEnd2) Then
                                Exit Do
                            End If
                        Loop
                    Next
                Next

                intMax = intMax + SubSim(ns1 + intMax, intEnd1, ns2 + intMax, intEnd2)
                intMax = intMax + SubSim(intStart1, ns1 - 1, intStart2, ns2 - 1)
            Catch ex As OverflowException
                Return Nothing
            Catch ex As StackOverflowException
                Return Nothing
            End Try

            Return intMax
        End Function
    
End Class
