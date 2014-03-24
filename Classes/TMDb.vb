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
    Public Property MaxGenres      As Integer = Media_Companion.Preferences.maxmoviegenre


    #End Region 'Read-write properties


    #Region "Read-only Properties"

    Private _api                    As WatTmdb.V3.Tmdb
    Private _config_images_base_url As String
    Private _movie                  As New WatTmdb.V3.TmdbMovie
    Private _movieImages            As WatTmdb.V3.TmdbMovieImages
    Private _trailers               As WatTmdb.V3.TmdbMovieTrailers
    Private _releases               As WatTmdb.V3.TmdbMovieReleases
    
    Private _mc_posters             As New List(Of str_ListOfPosters)
    Private _mc_backdrops           As New List(Of str_ListOfPosters)
    Private _thumbs                 As New List(Of String)
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


    Public ReadOnly Property Movie As WatTmdb.V3.TmdbMovie
        Get
            Fetch
            Return _movie
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

            For Each country In _releases.countries
                If country.iso_3166_1.ToLower = LookupLanguages.Item(0) then
                    Return country.certification
                End If
            Next

            For Each country In _releases.countries
                If country.certification <> "" then
                    Return country.certification
                End If
            Next
            Return ""
        End Get 
    End Property


    'Private Sub FetchCast
    '    If IsNothing(_cast) then
    '        Dim tries=0
    '        Dim ok=False

    '        While tries<3 And Not ok
    '            Try
    '                _cast = _api.GetMovieCast(_movie.id)
    '            Catch
    '                Threading.Thread.Sleep(500)
    '                tries &= 1
    '                Continue While
    '            End Try
    '            ok=True
    '        End While

    '        If Not ok Then
    '            Throw New Exception("TMDb is unavailale!")
    '        End If
    '    End If
    'End Sub



    Function GetMovieCast As Boolean
        _cast = _api.GetMovieCast(_movie.id)
        Return Not IsNothing(_cast)
    End Function


    Private Sub FetchCast
        If IsNothing(_cast) then
            If Not (new RetryHandler(AddressOf GetMovieCast)).Execute Then Throw New Exception(TMDB_EXC_MSG)
        End If
    End Sub




    'Private Sub FetchReleases
    '    If IsNothing(_releases) then
    '        Dim tries=0
    '        Dim ok=False

    '        While tries<3 And Not ok
    '            Try
    '               _releases = _api.GetMovieReleases(_movie.id)
    '            Catch
    '                Threading.Thread.Sleep(500)
    '                tries &= 1
    '                Continue While
    '            End Try
    '            ok=True
    '        End While

    '        If Not ok Then
    '            Throw New Exception(TMDB_EXC_MSG)
    '        End If
    '    End If
    'End Sub


    Function GetMovieReleases As Boolean
        _releases = _api.GetMovieReleases(_movie.id)
        Return Not IsNothing(_releases)
    End Function


    Private Sub FetchReleases
        If IsNothing(_releases) then
            If Not (new RetryHandler(AddressOf GetMovieReleases)).Execute Then Throw New Exception(TMDB_EXC_MSG)
        End If
    End Sub


    'Public ReadOnly Property AlternateTitles As List(Of String)
    '    Get
    '        Fetch
    '        If IsNothing(_alternateTitles) then 

    '            Dim tries=0
    '            Dim ok=False

    '            While tries<3 And Not ok
    '                Try
    '                   _alternateTitles = _api.GetMovieAlternateTitles(Movie.id,LookupLanguages.Item(0))
    '                Catch
    '                    Threading.Thread.Sleep(500)
    '                    tries &= 1
    '                    Continue While
    '                End Try
    '                ok=True
    '            End While

    '            If Not ok Then
    '                Throw New Exception(TMDB_EXC_MSG)
    '            End If


    '            For Each item In _alternateTitles.titles
    '                _mcAlternateTitles.Add(item.title)
    '            Next
    '        End If

    '        Return _mcAlternateTitles
    '    End Get 
    'End Property


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



    'Public ReadOnly Property AlternateTitles As List(Of String)
    '    Get
    '        Fetch
    '        If IsNothing(_alternateTitles) then 

    '            Dim tries=0
    '            Dim ok=False

    '            While tries<3 And Not ok
    '                Try
    '                   _alternateTitles = _api.GetMovieAlternateTitles(Movie.id,LookupLanguages.Item(0))
    '                Catch
    '                    Threading.Thread.Sleep(500)
    '                    tries &= 1
    '                    Continue While
    '                End Try
    '                ok=True
    '            End While

    '            If Not ok Then
    '                Throw New Exception(TMDB_EXC_MSG)
    '            End If


    '            For Each item In _alternateTitles.titles
    '                _mcAlternateTitles.Add(item.title)
    '            Next
    '        End If

    '        Return _mcAlternateTitles
    '    End Get 
    'End Property

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
    #End Region  'Read-only properties

    'Called during deserialisation
    'Sub new()
    '    _api         = New WatTmdb.V3.Tmdb
    ''   _config      = New WatTmdb.V3.TmdbConfiguration  
    '    _movieImages = New WatTmdb.V3.TmdbMovieImages
    '    _trailers    = New WatTmdb.V3.TmdbMovieTrailers
    'End Sub


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


    'Private Sub AssignConfig_images_base_url

    '    Dim fi As IO.FileInfo = New IO.FileInfo(TMDbConfigImagesBaseUrlFile)

    '    Dim expired As Boolean = True
     

    '    _config_images_base_url = Nothing


    '    Try
    '        If fi.Exists then
    '            expired = (DateTime.Now-fi.LastWriteTime).TotalDays>TMDbConfigFileMaxAgeInDays

    '            If Not expired then
    '                _config_images_base_url = File.ReadAllText(TMDbConfigImagesBaseUrlFile)
    '                Return  
    '            End If
    '        End If
    '    Catch
    '    End Try
 

    '    Dim tries As Integer=0


    '    While tries<3
    '        Try
    '            _config_images_base_url = _api.GetConfiguration().images.base_url

    '            If fi.Exists then fi.Delete

    '            File.WriteAllText(TMDbConfigImagesBaseUrlFile, _config_images_base_url)
    '            Return
    '        Catch
    '            System.Threading.Thread.Sleep(500)
    '            tries += 1
    '        End Try
    '    End While
        

    '    If Not IsNothing(_config_images_base_url) Then Return
        

    '    'Fallback on expired file
    '    Try
    '        If fi.Exists Then
    '            _config_images_base_url = File.ReadAllText(TMDbConfigImagesBaseUrlFile)
    '        End If
    '        Return
    '    Catch
    '    End Try


    '    'If all else fails -> Write a default one
    '    Try
    '        _config_images_base_url = "http://d3gtl9l2a4fn1j.cloudfront.net/t/p/"

    '        If fi.Exists then fi.Delete

    '        File.WriteAllText(TMDbConfigImagesBaseUrlFile, _config_images_base_url)
    '    Catch
    '        Throw New Exception("AssignConfig_images_base_url failed")
    '    End Try
    'End Sub



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



    'Public Sub JsonSerialize(Of T)(sFileName As String, ByVal obj As T )

    '    Dim json As String = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented)

    '    File.WriteAllText(sFileName, json)
    'End Sub

    'Public Function JsonDeserialize(Of T)(sFileName As String) As T

    '    Dim json As String = File.ReadAllText(sFileName)

    '    Return JsonConvert.DeserializeObject(Of T)(json)
    'End Function


    'Private Sub Fetch
    '    If _movie.id=0 and Not __Serialising and Not _fetched then
    '        _fetched     = True

    '        Dim tries=0
    '        Dim ok=False

    '        While tries<3 And Not ok
    '            Try
    '                _movie = _api.GetMovieByIMDB( Imdb, _lookupLanguages.Item(0) )
    '            Catch
    '                Threading.Thread.Sleep(500)
    '                tries &= 1
    '                Continue While
    '            End Try

    '            If IsNothing(_movie) Then
    '                Threading.Thread.Sleep(500)
    '                tries &= 1
    '                Continue While
    '            End If

    '            Try
    '                _movieImages = _api.GetMovieImages  (_movie.id)
    '            Catch
    '            End Try
    '            Try
    '                _trailers    = _api.GetMovieTrailers(_movie.id)
    '            Catch
    '            End Try
    '            ok = True
    '        End While
            
    '        If Not ok Then
    '            Throw New Exception(TMDB_EXC_MSG)
    '        End If
            
    '        'If movie isn't found -> Create empty child objects
    '        If IsNothing(_movieImages.backdrops) then _movieImages.backdrops = New List(Of WatTmdb.v3.Backdrop)
    '        If IsNothing(_movieImages.posters  ) then _movieImages.posters   = New List(Of WatTmdb.v3.Poster  )
    '        If IsNothing(_trailers.youtube     ) then _trailers.youtube      = New List(Of WatTmdb.V3.Youtube )

    '        FixUpMovieImages

    '        AssignValidBackDrops
    '        AssignValidPosters
    '        AssignMC_Posters
    '        AssignMC_Thumbs
    '        AssignMC_Backdrops
    '        AssignFrodoExtraPosterThumbs
    '        AssignFrodoExtraFanartThumbs
    '    End If
    'End Sub

    Function GetMovieByIMDB As Boolean
        _movie = _api.GetMovieByIMDB( Imdb, _lookupLanguages.Item(0) )
        Return Not IsNothing(_movie)
    End Function

    Function GetMovieImages As Boolean
         _movieImages = _api.GetMovieImages  (_movie.id)
        Return Not IsNothing(_movieImages)
    End Function

    Function GetMovieTrailers As Boolean
        _trailers    = _api.GetMovieTrailers(_movie.id)
        Return Not IsNothing(_trailers)
    End Function


    Private Sub Fetch
        Try
            If _movie.id = 0 And Not __Serialising And Not _fetched Then

                _fetched = True

                Dim rhs As List(Of RetryHandler) = New List(Of RetryHandler)

                rhs.Add(New RetryHandler(AddressOf GetMovieByIMDB))
                rhs.Add(New RetryHandler(AddressOf GetMovieImages))
                rhs.Add(New RetryHandler(AddressOf GetMovieTrailers))

                For Each rh In rhs
                    If Not rh.Execute Then Throw New Exception(TMDB_EXC_MSG)
                Next

                'If movie isn't found -> Create empty child objects
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
            End If
        Catch ex As Exception
            Throw New Exception (ex.Message)
        End Try

    End Sub



    Private Sub AssignFrodoExtraPosterThumbs

        For Each item In ValidPosters
            _frodoPosterThumbs.Add(New FrodoPosterThumb("poster",HdPath+item.file_path))
        Next

        'For Each item In ValidBackDrops
        '    _frodoThumbs.Add(New Thumb("thumb",HdPath+item.file_path))
        'Next
    End Sub


    Private Sub AssignFrodoExtraFanartThumbs
        For Each item In ValidBackDrops
            _frodoFanartThumbs.Thumbs.Add(New FrodoFanartThumb( LdBackDropPath+item.file_path ,HdPath+item.file_path))
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


    '
    'Builds a filtered by language, ordered by preferred language list of back drops
    '
    Private Sub AssignValidBackDrops
        For Each language In _lookupLanguages                                                                                           '_lookupLanguages are already in preferred language order
            Dim lang = language
            Dim q  = From b In _movieImages.backdrops Where b.iso_639_1.ToLower.IndexOf(lang) = 0 Order By b.vote_average Descending    'Filter out back drops in unwanted languages
                                                                                                                                        'in practice though, no language gets assigned
            For each item In q
                If Not ValidBackDrops.Contains(item) then
                    ValidBackDrops.Add(item)
                End if
            Next
        Next
    End Sub

    '
    'Builds a filtered by language, ordered by preferred language list of posters
    '
    Private Sub AssignValidPosters
        For Each language In _lookupLanguages                                                         'Already in preferred language order
            Dim lang = language
            Dim q    = From b In _movieImages.posters Where b.iso_639_1.ToLower.IndexOf(lang) = 0 Order By b.vote_average Descending    'Filter out back drops in unwanted languages

            For each item In q
                If Not ValidPosters.Contains(item) then
                    ValidPosters.Add(item)
                End if
            Next
        Next
    End Sub


    Private Sub AssignMC_Posters
        For each item In ValidPosters
            Dim mc_poster As New str_ListOfPosters(True)

            mc_poster.hdUrl = HdPath       + item.file_path
            mc_poster.ldUrl = LdPosterPath + item.file_path

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
