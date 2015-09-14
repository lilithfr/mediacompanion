Imports System.Linq
Imports System.Collections.Generic
Imports System.IO

Public Class TMDb

    Public Const Key          = "3f026194412846e530a208cf8a39e9cb"
    Public Const TMDB_EXC_MSG = "TMDb is unavailable!"

    Public Shared LanguagesFile               = Preferences.applicationPath & "\classes\tmdb_languages.xml"
    Public Const  TMDbConfigImagesBaseUrlFile = "tmdb_config_images_base_url.txt"
    Public Const  TMDbConfigFileMaxAgeInDays  = 14

    Enum Resolution
        FullHD=1920
        HD=1280
    End Enum

    #Region "Read-write Properties"

    Property __Serialising  As Boolean=False

    Private _languages       As List(Of String) = New List(Of String)
    Private _lookupLanguages As List(Of String) = New List(Of String)

    Public Property Imdb As String
    Public Property TmdbId As String
    Public Property CollectionSearch As Boolean = False

    Public Property Languages As List(Of String)
        Get
            Return _languages
        End Get 
        Set
            _languages.Clear
            _languages.AddRange(Value)

            _lookupLanguages.Clear
            For Each language In _languages
                _lookupLanguages.Add( language.ToLower.Replace("no language","xx").Replace("language not set","?") )
            Next
        End Set
    End Property


    Public Property ValidBackDrops As New List(Of WatTmdb.V3.Backdrop)
    Public Property ValidPosters   As New List(Of WatTmdb.V3.Poster  )
    Public Property ValidKeyWords  As WatTmdb.V3.TmdbMovieKeywords
    Public Property MaxGenres      As Integer = Media_Companion.Preferences.maxmoviegenre


    #End Region 'Read-write properties

    #Region "Read-only Properties"

    Private _api                    As WatTmdb.V3.Tmdb
    Private _config_images_base_url As String
    Private _movie                  As New WatTmdb.V3.TmdbMovie
    Private _collection             As New WatTmdb.V3.TmdbCollection 
    Private _movieImages            As WatTmdb.V3.TmdbMovieImages
    Private _collectionImages       As WatTmdb.V3.TmdbCollectionImages 
    Private _trailers               As WatTmdb.V3.TmdbMovieTrailers
    Private _releases               As WatTmdb.V3.TmdbMovieReleases
    Private _genrelist              As WatTmdb.V3.TmdbGenre 
    
    Private _mc_posters             As New List(Of str_ListOfPosters)
    Private _mc_backdrops           As New List(Of str_ListOfPosters)
    Private _mc_collection          As New List(Of MovieSetsList)
    Private _thumbs                 As New List(Of String)
    Private _keywords               As New List(Of String)
    Private _alternateTitles        As WatTmdb.V3.TmdbMovieAlternateTitles
    Private _mcAlternateTitles      As New List(Of String)
    Private _cast                   As WatTmdb.V3.TmdbMovieCast
    Private _director               As String
    Private _frodoPosterThumbs      As New List(Of FrodoPosterThumb)
    Private _frodoFanartThumbs      As New FrodoFanartThumbs

    Private _fetched                As Boolean = False

 
    Shared Public ReadOnly Property LanguageCodes As List(Of String)
        Get
            If Media_Companion.Preferences.TMDbUseCustomLanguage and Media_Companion.Preferences.TMDbCustomLanguageValue<>"" then        
                Return Media_Companion.Preferences.TMDbCustomLanguageValue.Split(",").ToList
            Else
                Return GetLanguageCodes(Media_Companion.Preferences.TMDbSelectedLanguageName)
            End If
        End Get
    End Property
    
    Shared Public ReadOnly Property AvailableLanguages As XDocument
        Get
            Return XDocument.Load(LanguagesFile)
        End Get 
    End Property

    Public ReadOnly Property LookupLanguages As List(Of String)
        Get
            Return _lookupLanguages
        End Get 
    End Property

    Public ReadOnly Property Api As WatTmdb.V3.Tmdb
        Get
            Return _api
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
                Dim x = _cast.cast.Count
                If x < 1 Then Return alist
                For i = 0 to Preferences.maxactors-1
                    If x = i Then Exit For
                    Dim newact As New str_MovieActors
                    newact.actorid      = _cast.cast(i).id
                    newact.actorname    = _cast.cast(i).name
                    newact.actorrole    = _cast.cast(i).character
                    newact.actorthumb   = If(_cast.cast(i).profile_path = Nothing, "", "http://image.tmdb.org/t/p/original" &_cast.cast(i).profile_path)
                    newact.order        = _cast.cast(i).order
                    alist.Add(newact)
                    
                Next
            Catch
            End Try
            Return alist
        End Get
    End Property

    Public ReadOnly Property Genrelist As List(Of String)
        Get
            Fetch
            FetchGenreList
            Dim genres As New List(Of String)
            For Each g In _genrelist.genres 
                genres.Add(g.ToString)
            Next
            Return genres
        End Get
    End Property

    Public ReadOnly Property Movie As WatTmdb.V3.TmdbMovie
        Get
            Fetch
            Return _movie
        End Get 
    End Property

    Public ReadOnly Property Collection As List(Of MovieSetsList) 
        Get
            Fetch
            Return _mc_collection
        End Get 
    End Property

    Public ReadOnly Property releasedate As String
        Get
            Fetch
            Return movie.release_date 
        End Get
    End Property

    Public ReadOnly Property Genres As String
        Get
            Fetch

            Dim s As String = ""
            Dim i As Integer
            For Each genre In _movie.Genres
                s += genre.name + ", "

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

    'Don't think TMDb returns this info...
    Public ReadOnly Property Stars As String
        Get
            Fetch
            FetchCast

            Dim s As String = ""
            
            For Each person In _cast.crew
                If person.job.ToLower = "star" then
                    s += person.name + ", "
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
            FetchCast

            For Each person In _cast.crew
                If person.job.ToLower = "director" then
                    Return person.name
                End If
            Next
            Return ""
        End Get 
    End Property
    
    Public ReadOnly Property Certification As String
        Get
            Fetch
            FetchReleases
            Try
                For Each country In _releases.countries
                    If country.iso_3166_1.ToLower = LookupLanguages.Item(0) then
                        Return country.certification
                    End If
                Next

                For Each country In _releases.countries
                    If country.iso_3166_1.ToLower = Preferences.XbmcTmdbScraperCertCountry Then
                        Return country.certification
                    End If
                Next

                For Each country In _releases.countries
                    If country.certification <> "" then
                        Return country.certification
                    End If
                Next
                Return ""
            Catch
                Return ""
            End Try
        End Get 
    End Property

    Function GetMovieCast As Boolean
        _cast = _api.GetMovieCast(_movie.id)
        Return Not IsNothing(_cast)
    End Function
    
    Private Sub FetchCast
        If IsNothing(_cast) then
            If Not (new RetryHandler(AddressOf GetMovieCast)).Execute Then Throw New Exception(TMDB_EXC_MSG)
        End If
    End Sub

    Function GetGenreList As Boolean
        _genrelist = _api.GetGenreList(LookupLanguages.Item(0))
        Return Not IsNothing(_genrelist)
    End Function

    Private Sub FetchGenreList
        If IsNothing(_genrelist) Then
            If Not (New RetryHandler(AddressOf GetGenreList)).Execute Then Throw New Exception(TMDB_EXC_MSG)
        End If
    End Sub
    
    Function GetMovieReleases As Boolean
        _releases = _api.GetMovieReleases(_movie.id)
        Return Not IsNothing(_releases)
    End Function

    Private Sub FetchReleases
        If IsNothing(_releases) then
            If Not (new RetryHandler(AddressOf GetMovieReleases)).Execute Then Throw New Exception(TMDB_EXC_MSG)
        End If
    End Sub
    
    Function GetMovieAlternateTitles As Boolean
        _alternateTitles = _api.GetMovieAlternateTitles(Movie.id,LookupLanguages.Item(0))
        Return Not IsNothing(_alternateTitles)
    End Function

    Public ReadOnly Property AlternateTitles As List(Of String)
        Get
            Fetch
            If IsNothing(_alternateTitles) then 
                If Not (new RetryHandler(AddressOf GetMovieAlternateTitles)).Execute Then Throw New Exception(TMDB_EXC_MSG)
            End If

            _mcAlternateTitles.Clear

            For Each item In _alternateTitles.titles
                _mcAlternateTitles.Add(item.title)
            Next

            Return _mcAlternateTitles
        End Get 
    End Property
    
    Public ReadOnly Property MovieImages As WatTmdb.V3.TmdbMovieImages
        Get
            Fetch
            Return _movieImages
        End Get 
    End Property

    Public ReadOnly Property MC_Posters As List(Of str_ListOfPosters)
        Get
            Fetch
            Return _mc_posters
        End Get 
    End Property

    Public ReadOnly Property Fanart As List(Of str_ListOfPosters) 
        Get
            Fetch
            Return _mc_backdrops
        End Get 
    End Property

    Public ReadOnly Property Thumbs As List(Of String)
        Get
            Fetch
            Return _thumbs
        End Get 
    End Property

    Public ReadOnly Property FirstOriginalPosterUrl As String
        Get
            Fetch

            If ValidPosters.Count=0 then
                Return ""
            End If

            Return Me.HdPath + ValidPosters(0).file_path
        End Get 
    End Property

    Public ReadOnly Property HdPath As String
        Get
            Return _config_images_base_url + "original"
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

    Public ReadOnly Property Trailers As WatTmdb.V3.TmdbMovieTrailers
        Get
            Fetch
            Return _trailers
        End Get 
    End Property

    Public ReadOnly Property Keywords As List(Of String)
        Get
            Fetch
            Return _keywords 
        End Get
    End Property

    #End Region  'Read-only properties

    Sub new( Optional imdb As String=Nothing )
        _api         = New WatTmdb.V3.Tmdb(Key)
        AssignConfig_images_base_url
        Languages    = LanguageCodes
        _imdb        = imdb
    End Sub

    Public Shared Sub DeleteConfigFile
        Dim fi As IO.FileInfo = New IO.FileInfo(TMDbConfigImagesBaseUrlFile)

        If fi.Exists then
            fi.Delete
        End If
    End Sub
    
    Function GetConfiguration As Boolean
        _config_images_base_url = _api.GetConfiguration().images.base_url
        Return Not IsNothing(_config_images_base_url)
    End Function

    Private Sub AssignConfig_images_base_url

        Dim fi As IO.FileInfo = New IO.FileInfo(TMDbConfigImagesBaseUrlFile)

        Dim expired As Boolean = True

        _config_images_base_url = Nothing

        Try
            If fi.Exists then
                expired = (DateTime.Now-fi.LastWriteTime).TotalDays>TMDbConfigFileMaxAgeInDays

                If Not expired then
                    _config_images_base_url = File.ReadAllText(TMDbConfigImagesBaseUrlFile)
                    Return  
                End If
            End If
        Catch
        End Try
 

        Dim Ok As Boolean = (new RetryHandler(AddressOf GetConfiguration)).Execute

        If Ok Then
            Try
                If fi.Exists then fi.Delete
                File.WriteAllText(TMDbConfigImagesBaseUrlFile, _config_images_base_url)
                Return
            Catch
            End Try
        End If
        
       
        'Fallback on expired file
        Try
            If fi.Exists Then _config_images_base_url = File.ReadAllText(TMDbConfigImagesBaseUrlFile)
            Return
        Catch
        End Try


        'If all else fails -> Write a default one
        Try
            _config_images_base_url = "http://d3gtl9l2a4fn1j.cloudfront.net/t/p/"

            If fi.Exists then fi.Delete
            File.WriteAllText(TMDbConfigImagesBaseUrlFile, _config_images_base_url)
        Catch
            Throw New Exception("AssignConfig_images_base_url failed")
        End Try
    End Sub
    
    Function GetMovieBy As Boolean
        If Imdb <> "" Then
            Return GetMovieByIMDB
        ElseIf TmdbId <> "" Then
            Return GetMovieByTmdbId
        End If
        Return False
    End Function

    Function GetMovieByIMDB As Boolean
        _movie = _api.GetMovieByIMDB( Imdb, _lookupLanguages.Item(0) )
        Return Not IsNothing(_movie)
    End Function

    Function GetMovieByTmdbId As Boolean
        If Not CollectionSearch Then
            _movie = _api.GetMovieInfo(ToInt(TmdbId), _lookupLanguages.Item(0))
            Return Not IsNothing(_movie)
        Else
            _collection = _api.GetCollectionInfo(TmdbId.ToInt, _lookupLanguages.Item(0))   '_api.GetMovieInfo(ToInt(TmdbId), _lookupLanguages.Item(0))
         Return Not IsNothing(_collection)
        End If
    End Function

    Function GetMovieImages As Boolean
        If Not CollectionSearch Then
             _movieImages = _api.GetMovieImages  (_movie.id)
            Return Not IsNothing(_movieImages)
        Else
            _collectionImages = _api.GetCollectionImages(TmdbId.ToInt)  ', _lookupLanguages.Item(0))  '_api.GetMovieImages  (_movie.id)
            Return Not IsNothing(_collectionImages)
        End If
    End Function

    Function GetMovieTrailers As Boolean
        _trailers    = _api.GetMovieTrailers(_movie.id)
        Return Not IsNothing(_trailers)
    End Function

    Function GetMovieKeywords As Boolean
        ValidKeyWords = _api.GetMovieKeywords(_movie.id)
        Return Not IsNothing(ValidKeyWords)
    End Function

    Function GetMoviesInCollection As Boolean
        _collection = _api.GetCollectionInfo(TmdbId.ToInt, _lookupLanguages.Item(0))   '_api.GetMovieInfo(ToInt(TmdbId), _lookupLanguages.Item(0))
        Return Not IsNothing(_collection)
    End Function

    Private Sub Fetch
        Try
            If _movie.id = 0 And Not __Serialising And Not _fetched Then

                _fetched = True

                Dim rhs As List(Of RetryHandler) = New List(Of RetryHandler)

                If Not CollectionSearch Then
                    rhs.Add(New RetryHandler(AddressOf GetMovieBy))
                    rhs.Add(New RetryHandler(AddressOf GetMovieImages))
                    rhs.Add(New RetryHandler(AddressOf GetMovieTrailers))
                    rhs.Add(New RetryHandler(AddressOf GetMovieKeywords))
                Else
                    rhs.Add(New RetryHandler(AddressOf GetMoviesInCollection))
                    rhs.Add(New RetryHandler(AddressOf GetMovieImages))
                End If

                If Not Utilities.UrlIsValid("http://api.themoviedb.org") Then
                    Throw New Exception("TMDB is offline")
                End If

                For Each rh In rhs
                    If Not rh.Execute Then Throw New Exception(TMDB_EXC_MSG)
                Next

                'Set TMDB ID from scraped data
                Try
                If _movie.id > 1 AndAlso TmdbId = "" Then TmdbId = _movie.id.ToString
                Catch
                End Try

                'If movie isn't found -> Create empty child objects
                If Not CollectionSearch Then
                    If IsNothing(_movieImages.backdrops) Then _movieImages.backdrops = New List(Of WatTmdb.V3.Backdrop)
                    If IsNothing(_movieImages.posters) Then _movieImages.posters = New List(Of WatTmdb.V3.Poster)
                    If IsNothing(_trailers.youtube) Then _trailers.youtube = New List(Of WatTmdb.V3.Youtube)
                    FixUpMovieImages()

                    AssignValidBackDrops()
                    AssignValidPosters()
                    AssignMC_Posters()
                    AssignMC_Thumbs()
                    AssignMC_Backdrops()
                    AssignFrodoExtraPosterThumbs()
                    AssignFrodoExtraFanartThumbs()
                    AssignKeywords()
                Else
                    FixUpCollectionImages()
                    AssignValidBackDrops()
                    AssignValidPosters()
                    AssignMC_Posters()
                    'AssignMC_Thumbs()
                    AssignMC_Backdrops()
                    AssignMC_Collections()
                End If
            End If
        Catch ex As Exception
            Throw New Exception (ex.Message)
        End Try

    End Sub

    Private Sub AssignFrodoExtraPosterThumbs

        For Each item In ValidPosters
            _frodoPosterThumbs.Add(New FrodoPosterThumb("poster",HdPath+item.file_path))
        Next
    End Sub

    Private Sub AssignFrodoExtraFanartThumbs
        For Each item In ValidBackDrops
            _frodoFanartThumbs.Thumbs.Add(New FrodoFanartThumb( LdBackDropPath+item.file_path ,HdPath+item.file_path))
        Next
    End Sub

    Private Sub AssignKeywords
        If IsNothing(ValidKeyWords.keywords) Then Exit Sub
        For Each keywd In ValidKeyWords.keywords
            _keywords.Add(keywd.ToString)
        Next
    End Sub

    Private Sub FixUpMovieImages
        FixUpMovieBackDrops
        FixUpMoviePosters
    End Sub

    Private Sub FixUpMovieBackDrops
        For Each item In _movieImages.backdrops
            If IsNothing(item.iso_639_1) then
                item.iso_639_1 = "?"
            End If
        Next
    End Sub

    Private Sub FixUpMoviePosters
        For Each item In _movieImages.posters
            If IsNothing(item.iso_639_1) then
                item.iso_639_1 = "?"
            End If
        Next
    End Sub

    Private Sub FixUpCollectionImages
        FixUpCollectionBackDrops
        FixUpCollectionPosters
    End Sub

    Private Sub FixUpCollectionBackDrops
        For Each item In _collectionImages.backdrops
            If IsNothing(item.iso_639_1) then
                item.iso_639_1 = "?"
            End If
        Next
    End Sub

    Private Sub FixUpCollectionPosters
        For Each item In _collectionImages.posters
            If IsNothing(item.iso_639_1) then
                item.iso_639_1 = "?"
            End If
        Next
    End Sub

    Private Sub AssignValidBackDrops
        For Each language In _lookupLanguages                                                                                           '_lookupLanguages are already in preferred language order
            Dim lang = language
            If Not CollectionSearch Then
                Dim q  = From b In _movieImages.backdrops Where b.iso_639_1.ToLower.IndexOf(lang) = 0 Order By b.vote_average Descending    'Filter out back drops in unwanted languages
                                                                                                                                            'in practice though, no language gets assigned
                For each item In q
                    If Not ValidBackDrops.Contains(item) then
                        ValidBackDrops.Add(item)
                    End if
                Next
            Else
                Dim q  = From b In _collectionImages.backdrops  Where b.iso_639_1.ToLower.IndexOf(lang) = 0 Order By b.height Descending    'Filter out back drops in unwanted languages
                                                                                                                                            'in practice though, no language gets assigned
                For each item In q
                    Dim it As New WatTmdb.V3.Backdrop
                    it.aspect_ratio = item.aspect_ratio
                    it.file_path    = item.file_path
                    it.height       = item.height
                    it.iso_639_1    = item.iso_639_1
                    it.width        = item.width 
                    If Not ValidBackDrops.Contains(it) then
                        ValidBackDrops.Add(it)
                    End if
                Next
            End If
        Next
    End Sub

    Private Sub AssignValidPosters
        For Each language In _lookupLanguages                                                         'Already in preferred language order
            Dim lang = language
            If Not CollectionSearch Then
                Dim q    = From b In _movieImages.posters Where b.iso_639_1.ToLower.IndexOf(lang) = 0 Order By b.vote_average Descending    'Filter out back drops in unwanted languages

                For each item In q
                    If Not ValidPosters.Contains(item) then
                        ValidPosters.Add(item)
                    End if
                Next
            Else
                Dim q    = From b In _collectionImages.posters Where b.iso_639_1.ToLower.IndexOf(lang) = 0 Order By b.Height Descending    'Filter out back drops in unwanted languages

                For each item In q
                    Dim it As New WatTmdb.V3.Poster
                    it.aspect_ratio = item.aspect_ratio 
                    it.file_path    = item.file_path
                    it.height       = item.height
                    it.iso_639_1    = item.iso_639_1
                    it.width        = item.width
                    If Not ValidPosters.Contains(it) then
                        ValidPosters.Add(it)
                    End if
                Next
            End If
        Next
    End Sub

    Private Sub AssignMC_Posters
        For each item In ValidPosters
            Dim mc_poster As New str_ListOfPosters(True)

            mc_poster.hdUrl     = HdPath       + item.file_path
            mc_poster.hdheight  = item.height
            mc_poster.hdwidth   = item.width 
            mc_poster.ldUrl     = LdPosterPath + item.file_path

            _mc_posters.Add(mc_poster)
        Next
    End Sub

    Private Sub AssignMC_Backdrops
        For Each item In ValidBackDrops
            Dim mc_backdrop As New str_ListOfPosters(True)

            mc_backdrop.hdUrl    = HdPath + item.file_path
            mc_backdrop.hdwidth  = item.width .ToString
            mc_backdrop.hdheight = item.height.ToString

            mc_backdrop.ldUrl    = LdBackDropPath + item.file_path
            mc_backdrop.ldwidth  = "1280"
            mc_backdrop.ldheight = "720"

            _mc_backdrops.Add(mc_backdrop)
        Next
    End Sub
    
    Private Sub AssignMC_Thumbs   
        For Each item In ValidBackDrops
            _thumbs.Add( HdPath + item.file_path )
        Next
    End Sub

    Private Sub AssignMC_Collections
        For each item In _collection.parts
            Dim tmpitem As New MovieSetsList
            tmpitem.title = item.title
            tmpitem.tmdbid = item.id
            _mc_collection.Add(tmpitem)
        Next
    End Sub

    Function SelectBackDrop( width As Integer ) As WatTmdb.V3.Backdrop
        Fetch
        Dim q = From b In ValidBackDrops Where b.width = width Order By b.vote_average Descending

        If q.Count = 0 then
            q = From b In ValidBackDrops Where b.width > width Order By b.vote_average Descending
        End If

        If q.Count = 0 then
            q = From b In ValidBackDrops Order By b.width Descending, b.vote_average Descending
        End If

        If q.Count = 0 then
            return Nothing
        End If

        Return q.First
    End Function

    Function GetBackDropUrl( Optional resolution As Resolution=Resolution.FullHD ) As String
        Fetch
        Dim BackDrop As WatTmdb.V3.Backdrop = SelectBackDrop( CInt(resolution) )

        If IsNothing(BackDrop) then
            Return Nothing
        End If

        If resolution=Resolution.FullHD then
            Return HdPath         + BackDrop.file_path
        Else
            Return LdBackDropPath + BackDrop.file_path
        End if
    End Function

    Function SaveBackDrop( destination As String, Optional resolution As Resolution=Resolution.FullHD ) As Boolean
        Dim url As String=GetBackDropUrl(resolution)

        If IsNothing(url) then
            Return False
        End If

        Using wc As New System.Net.WebClient()
            wc.DownloadFile(url, destination)
        End Using
 
        Return True
    End Function

    Function GetTrailerUrl(FailedUrls As List(Of String), Optional resolution As String="1080" ) As String
        Fetch
            
        Dim q = From t In _trailers.youtube Where t.size=resolution+"p" And Not FailedUrls.Contains(t.source)

        If q.Count = 0 then
            q = From t In _trailers.youtube Where Not FailedUrls.Contains(t.source) Order By t.size Descending
        End If

        If q.Count = 0 then
            return ""
        End If

        Return q.First.source
    End Function

    Shared Sub LoadLanguages(ByRef cb As ComboBox)
        cb.Items.Clear
        
        Dim q = From x In AvailableLanguages.Descendants("language")
                            Select name = x.Attribute("name").Value
                            Order By name
                    
        For Each element In q
            cb.Items.Add(element)
        Next
    End Sub
    
    Shared Function GetLanguageCodes(name As String) As List(Of String)
        Dim q = From x In AvailableLanguages.Descendants("language")
                            Select value   = x.Attribute("value").Value, 
                                   attName = x.Attribute("name" ).Value
                            Where attName = name 

        Return q.Single().value.Split(",").ToList
    End Function
End Class
