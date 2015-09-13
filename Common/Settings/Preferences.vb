Imports System.IO
Imports System.Xml
Imports System.Threading
Imports System.ComponentModel
'Imports MediaInfoNET
Imports System.Data.SQLite
Imports System.Data
Imports XBMC.JsonRpc

Module Ext
    <System.Runtime.CompilerServices.Extension()> _
    Public Sub AppendChild(root As XmlElement, doc As XmlDocument, name As String, value As String)

        Dim child As XmlElement = doc.CreateElement(name)

        child.InnerText = value
        root.AppendChild(child)
    End Sub

    <System.Runtime.CompilerServices.Extension()> _
    Public Sub AppendChildList(root As XmlElement, doc As XmlDocument, name As String, value() As String, Optional separator As String="|")

        Dim child As XmlElement = doc.CreateElement(name)

        child.InnerText = If(value.Count>0, String.Join(separator,value), "")
        root.AppendChild(child)
    End Sub

End Module


Public Class Preferences 

    Shared Event PropertyChanged_MkvMergeGuiPath

    Public Const SetDefaults = True
    Public Const datePattern As String = "yyyyMMddHHmmss"
    Public Const nfoDatePattern As String = "yyyy-MM-dd"


    'Not saved items
    Public Shared fixnfoid As Boolean
    Public Shared tv_RegexScraper As New List(Of String)
    Public Shared tv_RegexRename As New List(Of String)
    Public Shared profiles As New List(Of ListOfProfiles)
    Public Shared workingProfile As New ListOfProfiles
    Public Shared commandlist As New List(Of str_ListOfCommands)
    Public Shared configpath As String
    Public Shared WebSite As String = "tvdb"
    Public Shared DoneAMov As Boolean = False
    Public Shared MusicVidScrape As Boolean = False
    Public Shared DlMissingEpData As Boolean = False
    Public Shared googlecount As Integer = 0
    Public Shared engineno As Integer = 0
    Public Shared enginefront As New List(Of String)
    Public Shared engineend As New List(Of String)
    Public Shared proxysettings As New List(Of String)
    Public Shared applicationDatapath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Media Companion\"
    'Public Shared XbmcTmdbHDTrailer As String = "No"
    Public Shared MovieChangeKeepExistingArt As Boolean = True
    Public Shared MovieChangeMovie As Boolean = False
    Public Shared MovieDeleteNfoArtwork As Boolean = False
    
    Public Shared TvChgShowDlPoster As Boolean = False
    Public Shared TvChgShowDlFanart As Boolean = False
    Public Shared TvChgShowDlSeasonthumbs As Boolean = False
    Public Shared TvChgShowDlFanartTvArt As Boolean = False
    Public Shared TvChgShowOverwriteImgs As Boolean = False

    Public Shared ReadOnly Property EdenEnabled As Boolean
        Get
            Return Preferences.XBMC_version<>2    '0=Eden only, 1=Both, 2=Frodo only
        End Get
    End Property

    Public Shared ReadOnly Property FrodoEnabled As Boolean
        Get
            Return Preferences.XBMC_version<>0    '0=Eden only, 1=Both, 2=Frodo only
        End Get
    End Property

    Public Shared Property applicationPath As String
        Get
            Return Utilities.applicationPath
        End Get
        Set(ByVal value As String)
            Utilities.applicationPath = value
        End Set
    End Property
    Public Shared Property tvScraperLog As String
        Get
            Return Utilities.tvScraperLog
        End Get
        Set(ByVal value As String)
            Utilities.tvScraperLog = value
        End Set
    End Property

    'Saved Folder Prefs
    Public Shared tvFolders         As New List(Of String)
    Public Shared tvRootFolders     As New List(Of str_RootPaths)
    Public Shared movieFolders      As New List(Of str_RootPaths)
    Public Shared offlinefolders    As New List(Of String)
    Public Shared stubfolder        As String
    Public Shared stubmessage       As String = "Insert Media to Continue"
    Public Shared homemoviefolders  As New List(Of str_RootPaths)
    Public Shared ExcludeFolders    As New Excludes("Folders")
    Public Shared MVidFolders       As New List(Of String)

    'Saved Form Prefs
    Public Shared backgroundcolour As String
    Public Shared forgroundcolour As String
    Public Shared remembersize As Boolean
    Public Shared locx As Integer
    Public Shared locy As Integer
    Public Shared formheight As Integer
    Public Shared formwidth As Integer
    Public Shared splt1 As Integer
    Public Shared splt2 As Integer
    Public Shared splt3 As Integer
    Public Shared splt4 As Integer
    Public Shared splt5 As Integer
    Public Shared splt6 As Integer  'Tv Banner Split distance  -  To be superceeded by tvbannersplit
    Public Shared tvbannersplit As Double  ' Banner as Percentage
    Public Shared maximised As Boolean
    Public Shared startuptab As Byte
    Public Shared logview As Integer
    Public Shared LogScrapeTimes As Boolean = False
    Public Shared ScrapeTimingsLogThreshold As Integer = 100
    Public Shared lastpath As String
    Public Shared maximumthumbs As Integer
    Public Shared preferredscreen As Integer

    'Saved General Prefs
    Public Shared startupCache As Boolean
    Public Shared renamenfofiles As Boolean
    Public Shared actorseasy As Boolean
    Public Shared overwritethumbs As Boolean
    Public Shared LocalActorImage As Boolean = True
    Public Shared videomode As Integer
    Public Shared selectedvideoplayer As String
    Public Shared externalbrowser As Boolean
    Public Shared selectedBrowser As String
    Public Shared altnfoeditor As String
    Public Shared ignorearticle As Boolean
    Public Shared ignoreAarticle As Boolean
    Public Shared ignoreAn As Boolean 
    Public Shared sorttitleignorearticle As Boolean
    Public Shared intruntime As Boolean
    Public Shared XBMC_version As Byte
    Public Shared ShowMovieGridToolTip As Boolean = False
    Public Shared ShowLogOnError As Boolean = True
    Public Shared DisplayRatingOverlay As Boolean
    Public Shared DisplayMediainfoOverlay As Boolean
    Public Shared DisplayMediaInfoFolderSize As Boolean
    Public Shared font As String
    Public Shared MultiMonitoEnabled As Boolean
    Public Shared ShowAllAudioTracks As Boolean
    Private Shared _MkvMergeGuiPath As String

    Shared Property MkvMergeGuiPath As String
        Get
            Return _MkvMergeGuiPath
        End Get
        Set (ByVal value As String)
            If IO.File.Exists(value) Then
                _MkvMergeGuiPath = value
                RaiseEvent PropertyChanged_MkvMergeGuiPath
            End If
        End Set
    End Property

    'Saved General Proxy Prefs
    Public Shared prxyEnabled As Boolean
    Public Shared prxyIp As String
    Public Shared prxyPort As String
    Public Shared prxyUsername As String
    Public Shared prxyPassword As String


    'Saved Movie Prefs
    Public Shared DownloadTrailerDuringScrape As Boolean
    Public Shared NoAltTitle As Boolean
    Public Shared XtraFrodoUrls As Boolean
    Public Shared gettrailer As Boolean
    Public Shared ignoretrailers As Boolean
    Public Shared moviePreferredTrailerResolution As String
    Public Shared moviescraper As Integer
    Public Shared nfoposterscraper As Integer
    Public Shared ignoreactorthumbs As Boolean
    Public Shared maxactors As Integer
    Public Shared keywordasTag As Boolean
    Public Shared keywordlimit As Integer
    Public Shared maxmoviegenre As Integer
    Public Shared enablehdtags As Boolean
    Public Shared MovDurationAsRuntine As Boolean
    Public Shared movieRuntimeDisplay As String
    Public Shared movieRuntimeFallbackToFile As Boolean = False
    Public Shared disablelogfiles As Boolean
    Public Shared incmissingmovies As Boolean
    Public Shared fanartnotstacked As Boolean
    Public Shared posternotstacked As Boolean
    Public Shared scrapemovieposters As Boolean
    Public Shared movrootfoldercheck As Boolean
    Public Shared posterjpg As Boolean
    Public Shared usefanart As Boolean
    Public Shared dontdisplayposter As Boolean
    Public Shared usefoldernames As Boolean
    Public Shared movxtrathumb As Boolean
    Public Shared movxtrafanart As Boolean
    Public Shared movxtrafanartqty As Integer
    Public Shared dlxtrafanart As Boolean
    Public Shared dlMovSetArtwork As Boolean
    Public Shared MovSetArtSetFolder As Boolean
    Public Shared MovSetArtCentralFolder As String
    Public Shared allfolders As Boolean
    Public Shared actorsave As Boolean
    Public Shared actorsavepath As String
    Public Shared actorsavealpha As Boolean
    Public Shared actornetworkpath As String
    Public Shared imdbmirror As String
    Public Shared createfolderjpg As Boolean
    Public Shared createfanartjpg As Boolean    'Use to create fanart.jpg if in a folder
    Public Shared basicsavemode As Boolean
    Public Shared namemode As String
    Public Shared usetransparency As Boolean
    Public Shared transparencyvalue As Integer
    Public Shared savefanart As Boolean
    Public Shared MovFanartTvscrape As Boolean   'cbMovFanartTvScrape
    Public Shared MovFanartTvDlClearArt As Boolean
    Public Shared MovFanartTvDlClearLogo As Boolean
    Public Shared MovFanartTvDlPoster As Boolean
    Public Shared MovFanartTvDlFanart As Boolean
    Public Shared MovFanartTvDlDisc As Boolean
    Public Shared MovFanartTvDlBanner As Boolean
    Public Shared MovFanartTvDlLandscape As Boolean
    Public Shared fanartjpg As Boolean      'Used to create fanart.jpg instead of movie-fanart.jpg
    Public Shared roundminutes As Boolean
    Public Shared moviedefaultlist As Byte
    'Public Shared moviesUseXBMCScraper As Boolean = False
    Public Shared movies_useXBMC_Scraper As Boolean
    'Public Shared whatXBMCScraperIMBD As Boolean
    'Public Shared whatXBMCScraperTVDB As Boolean
    Public Shared TmdbActorsImdbScrape As Boolean
    Public Shared ImdbPrimaryPlot As Boolean
    Public Shared XBMC_Scraper As String = "tmdb"   'Locked TMDb as XBMC Scraper.
    Public Shared XbmcTmdbRenameMovie As Boolean
    Public Shared XbmcTmdbMissingFromImdb As Boolean
    Public Shared XbmcTmdbTop250FromImdb As Boolean
    Public Shared XbmcTmdbVotesFromImdb As Boolean
    Public Shared XbmcTmdbCertFromImdb As Boolean
    Public Shared XbmcTmdbStarsFromImdb As Boolean
    Public Shared XbmcTmdbAkasFromImdb As Boolean
    Public Shared XbmcTmdbActorDL As Boolean
    Public Shared scrapefullcert As Boolean
    Public Shared OfflineDVDTitle As String
    Public Shared MovieManualRename As Boolean
    Public Shared MovieRenameEnable As Boolean
    Public Shared MovieRenameTemplate As String
    Public Shared MovFolderRename As Boolean
    Public Shared MovFolderRenameTemplate As String
    Public Shared MovNewFolderInRootFolder As String
    Public Shared MovRenameSpaceCharacter As Boolean
    Public Shared MovSetIgnArticle As Boolean
    Public Shared MovSortIgnArticle As Boolean
    Public Shared MovTitleIgnArticle As Boolean
    Public Shared MovTitleCase As Boolean
    Public Shared ExcludeMpaaRated As Boolean
    Public Shared MovThousSeparator As Boolean
    Public Shared MovieImdbGenreRegEx As String
    Public Shared showsortdate As Boolean
    Public Shared TMDbSelectedLanguageName As String = "English - US"
    Public Shared TMDbUseCustomLanguage As Boolean = False
    Public Shared TMDbCustomLanguageValue As String = ""
    'Public Shared TMDBPreferredCertCountry As String = ""
    Public Shared GetMovieSetFromTMDb As Boolean = True
    Public Shared ActorResolutionSI As Integer = 2     ' Height  768           SI = Selected Index
    Public Shared PosterResolutionSI As Integer = 9     ' Height  1080  
    Public Shared BackDropResolutionSI As Integer = 15     ' Full HD 1920x1080

    Public Shared ActorsFilterMinFilms         As Integer =   1
    Public Shared MaxActorsInFilter            As Integer = 500
    Public Shared MovieFilters_Actors_Order    As Integer =   0        ' 0=Number of films desc 1=A-Z

    Public Shared DirectorsFilterMinFilms      As Integer =   1
    Public Shared MaxDirectorsInFilter         As Integer = 500
    Public Shared MovieFilters_Directors_Order As Integer =   0        ' 0=Number of films desc 1=A-Z

    Public Shared SetsFilterMinFilms           As Integer =   1             
    Public Shared MaxSetsInFilter              As Integer = 500
    Public Shared MovieFilters_Sets_Order      As Integer =   0        ' 0=Number of films desc 1=A-Z


    Public Shared DateFormat As String = "YYYY-MM-DD"   'Valid tokens: YYYY MM DD HH MIN SS Used in Movie list
    Public Shared DateFormat2 As String = "yyyy-MM-dd HH:mm:ss"   'Valid tokens: YYYY MM DD HH MIN SS Used in Movie list
    Public Shared MovieList_ShowColPlot As Boolean = False
    Public Shared DisableNotMatchingRenamePattern As Boolean = True
    Public Shared MovieList_ShowColWatched As Boolean = False
    Public Shared MovieScraper_MaxStudios As Integer = 9     ' 9 = Max
    Public Shared moviesortorder As Integer
    Public Shared movieinvertorder As Boolean
    Public Shared moviesets As New List(Of String)
    Public Shared movietags As New List(Of String)
    Public Shared moviethumbpriority As New List(Of String)
    Public Shared certificatepriority() As String
    Public Shared releaseformat() As String
    Public Shared tableview As New List(Of String)
    Public Shared tablesortorder As String
    Public Shared MovSepLst As New List(Of String)
    Public Shared MovFiltLastSize As Integer
    Public Shared RenameSpaceCharacter As String

    Public Shared Original_Title     As Boolean=False
    Public Shared UseMultipleThreads As Boolean=False
    Public Shared XbmcTmdbScraperFanart As String = Nothing
    Public Shared XbmcTmdbScraperTrailerQ As String = Nothing
    Public Shared XbmcTmdbScraperLanguage As String = Nothing
    Public Shared XbmcTmdbScraperRatings As String = Nothing
    Public Shared XbmcTmdbScraperCertCountry As String = Nothing

    Public Shared movie_filters As MovieFilters = New MovieFilters

    Public Shared CheckForNewVersion As Boolean=False

    Public Shared Property movieignorepart As Boolean
        Get
            Return Utilities.ignoreParts
        End Get
        Set(value As Boolean)
            Utilities.ignoreParts = value
        End Set
    End Property
    Public Shared Property moviecleanTags As String
        Get
            Return Utilities.userCleanTags
        End Get
        Set(value As String)
            Utilities.userCleanTags = value
        End Set
    End Property
    Public Shared Property rarsize As Integer
        Get
            Return Utilities.RARsize
        End Get
        Set(value As Integer)
            Utilities.RARsize = value
        End Set
    End Property
    Public Shared ReadOnly Property MovFanartTvDlAll As Boolean
        Get
            Return MovFanartTvDlBanner AndAlso MovFanartTvDlClearArt AndAlso MovFanartTvDlClearLogo AndAlso MovFanartTvDlDisc AndAlso MovFanartTvDlFanart AndAlso MovFanartTvDlLandscape AndAlso MovFanartTvDlPoster 
        End Get
    End Property

    'Saved TV Prefs
    Public Shared tvshowautoquick As Boolean
    Public Shared copytvactorthumbs As Boolean = False
    Public Shared displayMissingEpisodes As Boolean = False
    Public Shared ignoreMissingSpecials As Boolean = False
    Public Shared TvMissingEpOffset As Boolean = False
    Public Shared sortorder As String
    Public Shared tvdlposter As Boolean
    Public Shared tvdlfanart As Boolean
    Public Shared tvdlseasonthumbs As Boolean
    Public Shared TvDlFanartTvArt As Boolean
    Public Shared TvFanartTvFirst As Boolean
    Public Shared dlTVxtrafanart As Boolean
    Public Shared TvXtraFanartQty As Integer
    Public Shared tvfolderjpg As Boolean
    Public Shared seasonfolderjpg As Boolean
    Public Shared enabletvhdtags As Boolean
    Public Shared disabletvlogs As Boolean
    Public Shared postertype As String
    Public Shared TvdbActorScrape As Integer
    Public Shared seasonall As String
    Public Shared tvrename As Integer
    Public Shared ScrShtDelay As Integer
    Public Shared eprenamelowercase As Boolean
    Public Shared tvshowrefreshlog As Boolean
    Public Shared autoepisodescreenshot As Boolean
    Public Shared tvscrnshtTVDBResize As Boolean
    Public Shared tvshow_useXBMC_Scraper As Boolean
    Public Shared autorenameepisodes As Boolean
    Public Shared TvdbLanguage As String = "English"
    Public Shared TvdbLanguageCode As String = "en"
    Public Shared lastrefreshmissingdate As String
    Public Shared excludefromshowfoldername As String

    '(Unsure)
    Public Shared maximagecount As Integer
    Public Shared episodeacrorsource As String
    Public Shared alwaysuseimdbid As Boolean


    'XBMC Sync
    Public Shared XBMC_Active As Boolean = False
    Public Shared XBMC_Link                   As Boolean = False
    Public Shared XBMC_Address                As String = "127.0.0.1"
    Public Shared XBMC_Port                   As String = "8080"
    Public Shared XBMC_Username               As String = ""
    Public Shared XBMC_Password               As String = ""
    Public Shared XBMC_UserdataFolder         As String = ""
    Public Shared XBMC_TexturesDb             As String = "Database\Textures13.db"
    Public Shared XBMC_ThumbnailsFolder       As String = "Thumbnails"
    Public Shared XBMC_MC_MovieFolderMappings As New XBMC_MC_FolderMappings("Movie")
    Public Shared XBMC_MC_CompareFields       As New XBMC_MC_CompareFields ("Movie")
    Public Shared XBMC_Link_Use_Forward_Slash As Boolean = False    'This property does not get persisted, it's assigned in XbmcController at runtime
    Public Shared XBMC_Delete_Cached_Images   As Boolean = True


    Public Shared ShowExtraMovieFilters       As Boolean = False

    

    ReadOnly Shared Property AppPath As String
        Get 
            Return My.Application.Info.DirectoryPath
         End Get
    End Property 


    ReadOnly Shared Property XBMC_TestsPassed As Boolean
        Get 
            'Non-db tests...
            Dim result As Boolean = XBMC_Active AndAlso XBMC_MC_MovieFolderMappings.Initialised AndAlso FrodoEnabled AndAlso XBMC_CanPing AndAlso XBMC_CanConnect 

            If Not result Then Return False

            'Db tests...
            If XBMC_Delete_Cached_Images Then

                result = XBMC_UserdataFolder_Valid     AndAlso XBMC_TexturesDbFile_Valid   AndAlso XBMC_TexturesDb_Conn_Valid  AndAlso 
                         XBMC_TexturesDb_Version_Valid AndAlso XBMC_ThumbnailsFolder_Valid    

            End If

            Return result 
         End Get
    End Property  


    ReadOnly Shared Property XBMC_CanConnect As Boolean
        Get
            Dim xbmc As XbmcJsonRpcConnection
            Try
                xbmc = new XbmcJsonRpcConnection(XBMC_Address, XBMC_Port, XBMC_Username, XBMC_Password)
                xbmc.Open
                Dim result As Boolean = xbmc.IsAlive
                xbmc.Close
                Return result
            Catch
            End Try
            Return False
        End Get
    End Property

    ReadOnly Shared Property XBMC_CanPing As Boolean
        Get
            Dim result As Boolean = False
            Try
                result = My.Computer.Network.Ping(XBMC_Address,1000)
            Catch
            End Try
            Return result
        End Get
    End Property

    ReadOnly Shared Property XBMC_TexturesDb_Version_Valid As Boolean
        Get
            Return TexturesDbVersion = 13
        End Get
    End Property

    Public Shared Function TexturesDbVersion As Integer
        Try
            Dim conn As SQLiteConnection = new SQLiteConnection(XBMC_TexturesDb_ReadOnly_ConnectionStr)
            conn.Open
            Dim dt As DataTable = DbUtils.ExecuteReader(conn, "Select idVersion from version" )
            conn.Close
            Return dt.Rows(0)("idVersion").ToString
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ReadOnly Shared Property XBMC_TexturesDb_Conn_Valid As Boolean
        Get
            Try
                Dim conn As SQLiteConnection = new SQLiteConnection(XBMC_TexturesDb_ReadOnly_ConnectionStr)
                conn.Open
                Dim dt As DataTable = DbUtils.ExecuteReader(conn, "Select idVersion from version" )
                conn.Close
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Get
    End Property

    ReadOnly Shared Property XBMC_ThumbnailsFolder_Valid As Boolean
        Get
            Return Directory.Exists(XBMC_Thumbnails_Path)
        End Get
    End Property

    ReadOnly Shared Property XBMC_UserdataFolder_Valid As Boolean
        Get
            Return Directory.Exists(XBMC_UserdataFolder)
        End Get
    End Property

    ReadOnly Shared Property XBMC_TexturesDbFile_Valid As Boolean
        Get
            Return IO.File.Exists(XBMC_TexturesDb_Path)
        End Get
    End Property

    ReadOnly Shared Property XBMC_TexturesDb_ReadOnly_ConnectionStr As String
        Get
            Return XBMC_TexturesDb_ConnectionStr & "Read Only=True;"
        End Get
    End Property

    ReadOnly Shared Property XBMC_TexturesDb_ConnectionStr As String
        Get
            Return "Data Source=" + Preferences.XBMC_TexturesDb_Path + ";Version=3;New=False;Compress=True;FailIfMissing=True;"
        End Get
    End Property

    ReadOnly Shared Property XBMC_Thumbnails_Path As String
        Get
            Return Path.Combine(XBMC_UserdataFolder,XBMC_ThumbnailsFolder)
        End Get
    End Property

    ReadOnly Shared Property XBMC_TexturesDb_Path As String
        Get
            Return Path.Combine(XBMC_UserdataFolder,XBMC_TexturesDb)
        End Get
    End Property

    ReadOnly Shared Property XbmcLinkReady As Boolean
        Get
            Return XBMC_Link And XBMC_TestsPassed
        End Get
    End Property

    Public Shared Sub SetUpPreferences()
        'General
        ignorearticle = False
        ignoreAarticle = False
        ignoreAn = False
        sorttitleignorearticle = False
        externalbrowser = False
        selectedBrowser = ""
        altnfoeditor = ""
        backgroundcolour = "Silver"
        forgroundcolour = "#D3D9DC"
        formheight = "600"
        formwidth = "800"
        disablelogfiles = False
        DisplayRatingOverlay = True
        DisplayMediainfoOverlay = True
        DisplayMediaInfoFolderSize = False
        ShowAllAudioTracks = True
        incmissingmovies = False
        startupCache = True
        rarsize = 8
        renamenfofiles = True
        scrapemovieposters = True
        movrootfoldercheck = True
        posterjpg = False
        dontdisplayposter = False
        usetransparency = False 'not used in gen2
        transparencyvalue = 255 'not used in gen2
        lastpath = applicationPath ' Application.StartupPath
        videomode = 1
        locx = 0
        locy = 0
        formheight = 725
        formwidth = 1060
        splt5 = 0
        splt6 = 230
        tvbannersplit = 0
        showsortdate = False
        MultiMonitoEnabled = False
        XBMC_version = 2
        'Proxy settings
        prxyEnabled = False
        prxyIp = "127.0.0.1"
        prxyPort = "8099"
        prxyUsername = "username"
        prxyPassword = "password"

        'Movies
        movies_useXBMC_Scraper = False
        TmdbActorsImdbScrape = False
        'TMDBPreferredCertCountry = "us"
        ImdbPrimaryPlot = False
        XBMC_Scraper = "tmdb"
        XbmcTmdbRenameMovie = False
        XbmcTmdbMissingFromImdb = False
        XbmcTmdbTop250FromImdb = False
        XbmcTmdbVotesFromImdb = False
        XbmcTmdbCertFromImdb = False
        XbmcTmdbStarsFromImdb = False
        XbmcTmdbAkasFromImdb = False
        XbmcTmdbActorDL = False
        moviedefaultlist = 0
        moviesortorder = 0
        'movieinvertorder = 0
        imdbmirror = "http://www.imdb.com/"
        usefoldernames = False
        movxtrafanart = True
        movxtrafanartqty = 0
        movxtrathumb = False
        dlxtrafanart = False
        dlMovSetArtwork = False
        MovSetArtSetFolder = False
        MovSetArtCentralFolder = ""
        allfolders = False
        'ReDim moviethumbpriority(3)
        maxmoviegenre = 99
        moviethumbpriority.Add("themoviedb.org")
        moviethumbpriority.Add("IMDB")
        moviethumbpriority.Add("Movie Poster DB")
        moviethumbpriority.Add("Internet Movie Poster Awards")
        MovSepLst.Add("3DSBS")
        MovSepLst.Add("3DTAB")
        MovSepLst.Add("3D")
        MovSepLst.Add("Directors-Cut")
        MovSepLst.Add("Extended-Edition")
        MovSepLst.Add("Theatrical-Version")
        MovSepLst.Add("Unrated-Version")
        movieRuntimeDisplay = "scraper"
        moviePreferredTrailerResolution = "720"
        MovieManualRename = True
        MovieRenameEnable = False
        MovieRenameTemplate = "%T (%Y)"
        MovFolderRename = False
        MovRenameSpaceCharacter = False
        MovSetIgnArticle = False
        MovSortIgnArticle = False
        MovTitleIgnArticle = False
        MovTitleCase = False
        ExcludeMpaaRated = False
        MovThousSeparator = False
        MovFolderRenameTemplate = "%N\%T (%Y)"
        MovNewFolderInRootFolder = False
        MovieImdbGenreRegEx = "/genre/.*?>(?<genre>.*?)</a>"
        MovFiltLastSize = 384
        RenameSpaceCharacter = "_"


        'TV
        tvshow_useXBMC_Scraper = False
        autorenameepisodes = False
        autoepisodescreenshot = False
        tvscrnshtTVDBResize = False
        tvshowautoquick = False
        copytvactorthumbs = True
        enabletvhdtags = True
        tvshowrefreshlog = False
        seasonall = "none"
        tvrename = 0
        tvdlfanart = True
        tvdlposter = True
        tvdlseasonthumbs = True
        TvDlFanartTvArt = False
        TvFanartTvFirst = False
        dlTVxtrafanart = False
        TvXtraFanartQty = 0
        tvfolderjpg = False
        seasonfolderjpg = False
        postertype = "poster"
        TvdbLanguage = "English"
        TvdbLanguageCode = "en"
        lastrefreshmissingdate = ""   'DateTime.Now.ToString("yyyy-MM-dd")
        sortorder = "default"
        TvdbActorScrape = 0
        OfflineDVDTitle = "Please Load '%T' Media To Play..."
        fixnfoid = False
        logview = "0"  'first entry in combobox is 'Full' (log view)
        displayMissingEpisodes = False
        ignoreMissingSpecials = False
        TvMissingEpOffset = False
        ScrShtDelay = 10
        excludefromshowfoldername = "[ended]"

        'Unknown - need to be sorted/named better
        eprenamelowercase = False
        intruntime = False
        actorseasy = True
        startuptab = 0
        font = "Microsoft Sans Serif, 9pt"
        fanartnotstacked = False
        posternotstacked = False
        ignoreactorthumbs = False
        actorsave = False
        actorsavepath = ""
        actorsavealpha = False
        actornetworkpath = ""
        usefanart = True
        ignoretrailers = False
        enablehdtags = True
        MovDurationAsRuntine = False
        savefanart = True
        MovFanartTvscrape = False
        MovFanartTvDlClearArt = True
        MovFanartTvDlClearLogo = True
        MovFanartTvDlPoster = True
        MovFanartTvDlFanart = True
        MovFanartTvDldisc = True
        MovFanartTvDlBanner = True
        MovFanartTvDlLandscape = True
        fanartjpg = False
        overwritethumbs = False
        LocalActorImage = True
        maxactors = 9999
        keywordasTag = False
        keywordlimit = 5
        createfolderjpg = False
        createfanartjpg = False
        basicsavemode = False               'movie.nfo, movie.tbn, fanart.jpg
        namemode = "1"
        maximumthumbs = 10
        preferredscreen = 0
        gettrailer = False
        DownloadTrailerDuringScrape = False
        NoAltTitle = False
        XtraFrodoUrls = True
        ReDim certificatepriority(33)
        certificatepriority(0) = "MPAA"
        certificatepriority(1) = "UK"
        certificatepriority(2) = "USA"
        certificatepriority(3) = "Ireland"
        certificatepriority(4) = "Australia"
        certificatepriority(5) = "New Zealand"
        certificatepriority(6) = "Norway"
        certificatepriority(7) = "Singapore"
        certificatepriority(8) = "South Korea"
        certificatepriority(9) = "Philippines"
        certificatepriority(10) = "Brazil"
        certificatepriority(11) = "Netherlands"
        certificatepriority(12) = "Malaysia"
        certificatepriority(13) = "Argentina"
        certificatepriority(14) = "Iceland"
        certificatepriority(15) = "Canada (Quebec)"
        certificatepriority(16) = "Canada (British Columbia/Ontario)"
        certificatepriority(17) = "Canada (Alberta/Manitoba/Nova Scotia)"
        certificatepriority(18) = "Peru"
        certificatepriority(19) = "Sweden"
        certificatepriority(20) = "Portugal"
        certificatepriority(21) = "South Africa"
        certificatepriority(22) = "Denmark"
        certificatepriority(23) = "Hong Kong"
        certificatepriority(24) = "Finland"
        certificatepriority(25) = "India"
        certificatepriority(26) = "Mexico"
        certificatepriority(27) = "France"
        certificatepriority(28) = "Italy"
        certificatepriority(29) = "Switzerland (canton of Vaud)"
        certificatepriority(30) = "Switzerland (canton of Geneva)"
        certificatepriority(31) = "Germany"
        certificatepriority(32) = "Greece"
        certificatepriority(33) = "Austria"
        maximagecount = 10
        ReDim releaseformat(14)
        releaseformat(0) = "Cam"
        releaseformat(1) = "Telesync"
        releaseformat(2) = "Workprint"
        releaseformat(3) = "Telecine"
        releaseformat(4) = "Pay-Per-View Rip"
        releaseformat(5) = "Screener"
        releaseformat(6) = "R5"
        releaseformat(7) = "DVD-Rip"
        releaseformat(8) = "DVD-R"
        releaseformat(9) = "HDTV"
        releaseformat(10) = "VODRip"
        releaseformat(11) = "BRRip"
        releaseformat(12) = "BDRip"
        releaseformat(13) = "Bluray"
        releaseformat(14) = "DVD"

        movieFolders.Clear()
        tvRootFolders.Clear()
        tvFolders.Clear()
        MVidFolders.Clear()

    End Sub

    Public Shared Sub resetmovthumblist
        moviethumbpriority.Clear()
        moviethumbpriority.Add("themoviedb.org")
        moviethumbpriority.Add("IMDB")
        moviethumbpriority.Add("Movie Poster DB")
        moviethumbpriority.Add("Internet Movie Poster Awards")
    End Sub

    Public Shared Sub ResetMovSepLst
        MovSepLst.Clear()
        MovSepLst.Add("3DSBS")
        MovSepLst.Add("3DTAB")
        MovSepLst.Add("3D")
        MovSepLst.Add("Directors-Cut")
        MovSepLst.Add("Extended-Edition")
        MovSepLst.Add("Theatrical-Version")
        MovSepLst.Add("Unrated-Version")
        MovSepLst.Add("DVD")
        MovSepLst.Add("Bluray")
    End Sub

    Public Shared Sub Proxyreload()
        proxysettings.Clear()
        proxysettings.Add(prxyEnabled)
        proxysettings.Add(prxyIp)
        proxysettings.Add(prxyPort)
        proxysettings.Add(prxyUsername)
        proxysettings.Add(prxyPassword)
        
    End Sub

    Public Shared Sub engineupdate
        enginefront.Clear()
        engineend.Clear()
        enginefront.Add("http://www.google.co.uk/search?hl=en-US&as_q=")
        engineend.Add("&as_sitesearch=www.imdb.com")
        enginefront.Add("http://www.bing.com/search?q=")
        engineend.Add("+movie+site%3Aimdb.com")
        enginefront.Add("http://www.ask.com/web?qsrc=1&o=0&l=dir&q=")
        engineend.Add("&qo=serpSearchTopBox")
        'enginefront.Add("http://search.yahoo.com/search?p=")    'Yahoo isn't allowing searching. 
        'engineend.Add("+movie+site%3Aimdb.com")
    End Sub
    Public Shared Sub ConfigSave()
        
        Dim tempstring As String = String.Empty
        Dim doc As New XmlDocument
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("xbmc_media_companion_config_v1.0")


        'Folders In Use ------------------------------------------------------
        For Each path In tvFolders
            root.AppendChild(doc, "tvfolder", path)
        Next

        For Each path In tvRootFolders
            Dim t As String = path.rpath & "|" & path.selected 
            root.AppendChild(doc, "tvrootfolder", t)
        Next

        For Each path In movieFolders
            Dim t As String = path.rpath & "|" & path.selected 
            root.AppendChild(doc, "nfofolder", t)
        Next
        root.AppendChild(doc, "stubfolder", stubfolder)
        root.AppendChild(doc, "stubmessage", stubmessage)
        'Dim list As New List(Of String)
        For Each path In offlinefolders
            'If Not list.Contains(path) Then
            root.AppendChild(doc, "offlinefolder", path)
                'list.Add(path)
            'End If
        Next

        For Each path In homemoviefolders
            Dim t As String = path.rpath & "|" & path.selected
            root.AppendChild(doc, "homemoviefolder", t)
            'list.Add(Path)
        Next

        For Each Path In MVidFolders
            root.AppendChild(doc, "MVidFolders", Path)
        Next

        root.AppendChild(ExcludeFolders.GetChild(doc))


        'Form Settings ------------------------------------------------------------
        root.AppendChild(doc, "backgroundcolour",           backgroundcolour)
        root.AppendChild(doc, "forgroundcolour",            forgroundcolour)
        root.AppendChild(doc, "remembersize",               remembersize)
        root.AppendChild(doc, "locx",                       locx)
        root.AppendChild(doc, "locy",                       locy)
        root.AppendChild(doc, "formheight",                 formheight)
        root.AppendChild(doc, "formwidth",                  formwidth)
        root.AppendChild(doc, "splitcontainer1",            splt1)
        root.AppendChild(doc, "splitcontainer2",            splt2)
        root.AppendChild(doc, "splitcontainer3",            splt3)
        root.AppendChild(doc, "splitcontainer4",            splt4)
        root.AppendChild(doc, "splitcontainer5",            splt5)
        root.AppendChild(doc, "splitcontainer6",            splt6)
        root.AppendChild(doc, "tvbannersplit",              tvbannersplit)
        root.AppendChild(doc, "maximised",                  maximised)
        root.AppendChild(doc, "startuptab",                 startuptab)
        root.AppendChild(doc, "logview",                    logview)
        root.AppendChild(doc, "LogScrapeTimes",             LogScrapeTimes)
        root.AppendChild(doc, "ScrapeTimingsLogThreshold",  ScrapeTimingsLogThreshold)
        root.AppendChild(doc, "maximumthumbs",              maximumthumbs)
        root.AppendChild(doc, "preferredscreen",            preferredscreen)
        root.AppendChild(doc, "lastpath",                   lastpath)
        root.AppendChild(doc, "MovieImdbGenreRegEx",        MovieImdbGenreRegEx)
        root.AppendChild(doc, "moviedefaultlist",           moviedefaultlist)           'RadioButtonFileName,RadioButtonTitleAndYear,RadioButtonFolder
        root.AppendChild(doc, "moviesortorder",             moviesortorder)             'cbSort
        root.AppendChild(doc, "movieinvertorder",           movieinvertorder)           'btnreverse
        root.AppendChild(doc, "displayMissingEpisodes",     displayMissingEpisodes)     'SearchForMissingEpisodesToolStripMenuItem
        root.AppendChild(doc, "ignoreMissingSpecials",      ignoreMissingSpecials)      'cbTvMissingSpecials
        root.AppendChild(doc, "TvMissingEpOffset",          TvMissingEpOffset)          'cb_TvMissingEpOffset


        'Still to do
        child = doc.CreateElement("moviesets")
        Dim childchild As XmlElement
        For Each movieset In moviesets
            If movieset <> "-None-" Then
                childchild = doc.CreateElement("set")
                childchild.InnerText = movieset
                child.AppendChild(childchild)
            End If
        Next
        root.AppendChild(child)

        child = doc.CreateElement("movietags")  'preparing new movie tags
        Dim childchild3 As XmlElement
        For Each movietag In movietags
            If movietag <> "" And Not IsNothing(movietag) Then
                childchild3 = doc.CreateElement("tag")
                childchild3.InnerText = movietag
                child.AppendChild(childchild3)
            End If
        Next
        root.AppendChild(child)

        child = doc.CreateElement("table")
        Dim childchild2 As XmlElement
        childchild2 = doc.CreateElement("sort")
        childchild2.InnerText = tablesortorder
        child.AppendChild(childchild2)
        For Each tabs In tableview
            childchild2 = doc.CreateElement("tab")
            childchild2.InnerText = tabs
            child.AppendChild(childchild2)
        Next
        root.AppendChild(child)


        'General Prefs ------------------------------------------------------------
        root.AppendChild(doc, "startupcache",           startupCache)           'chkbx_disablecache
        root.AppendChild(doc, "renamenfofiles",         renamenfofiles)         'CheckBoxRenameNFOtoINFO
        root.AppendChild(doc, "actorseasy",             actorseasy)             'CheckBox33
        root.AppendChild(doc, "rarsize",                rarsize)                'txtbx_minrarsize
        root.AppendChild(doc, "overwritethumbs",        overwritethumbs)        'cbOverwriteArtwork - does not appear to be used?
        root.AppendChild(doc, "LocalActorImage",        LocalActorImage)        'cbDisplayLocalActor
        root.AppendChild(doc, "videomode",              videomode)              'RadioButton36-38
        root.AppendChild(doc, "selectedvideoplayer",    selectedvideoplayer)    'btn_custommediaplayer
        root.AppendChild(doc, "externalbrowser",        externalbrowser)        'CheckBox12
        root.AppendChild(doc, "selectedBrowser",        selectedBrowser)        'btnFindBrowser
        root.AppendChild(doc, "altnfoeditor",           altnfoeditor)           'btnaltnfoeditor
        root.AppendChild(doc, "ignorearticle",          ignorearticle)          'cb_IgnoreThe
        root.AppendChild(doc, "ignoreAarticle",         ignoreAarticle)         'cb_IgnoreA
        root.AppendChild(doc, "ignoreAn",               ignoreAn)               'cb_IgnoreAn
        root.AppendChild(doc, "sorttitleignorearticle", sorttitleignorearticle) 'cb_SorttitleIgnoreArticles
        root.AppendChild(doc, "intruntime",             intruntime)             'CheckBox38
        root.AppendChild(doc, "xbmcartwork",            XBMC_version)           'rbXBMCv_pre,rbXBMCv_post,rbXBMCv_both
        root.AppendChild(doc, "ShowMovieGridToolTip" ,  ShowMovieGridToolTip )  'cbShowMovieGridToolTip
        root.AppendChild(doc, "ShowLogOnError"       ,  ShowLogOnError       )  'cbShowLogOnError
        root.AppendChild(doc, "CheckForNewVersion"   ,  CheckForNewVersion   )
        root.AppendChild(doc, "MkvMergeGuiPath"      ,  MkvMergeGuiPath      )  'tbMkvMergeGuiPath
        root.AppendChild(doc, "prxyEnabled"          ,  prxyEnabled          )  'tbMkvMergeGuiPath
        root.AppendChild(doc, "prxyIp"               ,  prxyIp               )  'tbMkvMergeGuiPath
        root.AppendChild(doc, "prxyPort"             ,  prxyPort             )  'tbMkvMergeGuiPath
        root.AppendChild(doc, "prxyUsername"         ,  prxyUsername         )  'tbMkvMergeGuiPath
        root.AppendChild(doc, "prxyPassword"         ,  prxyPassword         )  'tbMkvMergeGuiPath
        root.AppendChild(doc, "ShowAllAudioTracks"   ,  ShowAllAudioTracks   )  'cbShowAllAudioTracks
        

        If Not String.IsNullOrEmpty(font) Then
            root.AppendChild(doc, "font", font)                                 'Button96
        End If

        For Each com In commandlist
            If com.command <> "" And com.title <> "" Then
                child = doc.CreateElement("comms")
                childchild = doc.CreateElement("title")
                childchild.InnerText = com.title
                child.AppendChild(childchild)
                childchild = doc.CreateElement("command")
                childchild.InnerText = com.command
                child.AppendChild(childchild)
                root.AppendChild(child)
            End If
        Next

        
        
        'Movie Prefs ------------------------------------------------------------
        root.AppendChild(doc, "DownloadTrailerDuringScrape",        DownloadTrailerDuringScrape)        'cbDlTrailerDuringScrape
        root.AppendChild(doc, "gettrailer",                         gettrailer)                         'CheckBox11
        root.AppendChild(doc, "ignoretrailers",                     ignoretrailers)                     'set from frmOptions - obsolete
        root.AppendChild(doc, "moviescraper",                       moviescraper)                       'set from frmOptions - obsolete
        root.AppendChild(doc, "nfoposterscraper",                   nfoposterscraper)                   'IMPA_chk,mpdb_chk,tmdb_chk,imdb_chk
        root.AppendChild(doc, "alwaysuseimdbid",                    alwaysuseimdbid)                    'set from frmOptions - obsolete
        root.AppendChild(doc, "ignoreactorthumbs",                  ignoreactorthumbs)                  'set from frmOptions - obsolete
        root.AppendChild(doc, "maxactors",                          maxactors)                          'ComboBox7
        root.AppendChild(doc, "keywordasTag",                       keywordasTag)                       'cb_keywordasTag
        root.AppendChild(doc, "keywordlimit",                       keywordlimit)                       'cb_keywordlimit
        root.AppendChild(doc, "maxmoviegenre",                      maxmoviegenre)                      'ComboBox6
        root.AppendChild(doc, "enablehdtags",                       enablehdtags)                       'CheckBox19
        root.AppendChild(doc, "MovDurationAsRuntine",               MovDurationAsRuntine)               'cb_MovDurationAsRuntine
        root.AppendChild(doc, "movieruntimedisplay",                movieRuntimeDisplay)                'rbRuntimeScraper
        root.AppendChild(doc, "movieRuntimeFallbackToFile",         movieRuntimeFallbackToFile)         'cbMovieRuntimeFallbackToFile
        root.AppendChild(doc, "fanartnotstacked",                   fanartnotstacked)                   'set from frmOptions - obsolete
        root.AppendChild(doc, "posternotstacked",                   posternotstacked)                   'set from frmOptions - obsolete
        root.AppendChild(doc, "scrapemovieposters",                 scrapemovieposters)                 'cbMoviePosterScrape
        root.AppendChild(doc, "movrootfoldercheck",                 movrootfoldercheck)                 'cbMovRootFolderCheck
        root.AppendChild(doc, "posterjpg",                          posterjpg)                          'cbMoviePosterInFolder
        root.AppendChild(doc, "usefanart",                          usefanart)                          'set from frmOptions - obsolete
        root.AppendChild(doc, "dontdisplayposter",                  dontdisplayposter)                  'set from frmOptions - obsolete
        root.AppendChild(doc, "usefoldernames",                     usefoldernames)                     'chkbx_usefoldernames
        root.AppendChild(doc, "movxtrathumb",                       movxtrathumb)                       'cbMovXtraThumb
        root.AppendChild(doc, "movxtrafanart",                      movxtrafanart)                      'cbMovXtraFanart
        root.AppendChild(doc, "movxtrafanartqty",                   movxtrafanartqty)                   'cbMovXtraFanartQty
        root.AppendChild(doc, "dlxtrafanart",                       dlxtrafanart)                       'cbDlXtraFanart
        root.AppendChild(doc, "dlMovSetArtwork",                    dlMovSetArtwork)                    'cbMovSetArtScrape
        root.AppendChild(doc, "MovSetArtSetFolder",                 MovSetArtSetFolder)                 'rbMovSetFolder
        root.AppendChild(doc, "MovSetArtCentralFolder",             MovSetArtCentralFolder)             'tbMovSetArtCentralFolder
        root.AppendChild(doc, "allfolders",                         allfolders)                         'chkbx_MovieAllFolders
        root.AppendChild(doc, "actorsave",                          actorsave)                          'saveactorchkbx
        root.AppendChild(doc, "actorsavepath",                      actorsavepath)                      'localactorpath
        root.AppendChild(doc, "actorsavealpha",                     actorsavealpha)                     'actorsavealpha
        root.AppendChild(doc, "actornetworkpath",                   actornetworkpath)                   'xbmcactorpath
        root.AppendChild(doc, "imdbmirror",                         imdbmirror)                         'ListBox9
        root.AppendChild(doc, "createfolderjpg",                    createfolderjpg)                    'cbMovCreateFolderjpg
        root.AppendChild(doc, "createfanartjpg",                    createfanartjpg)                    'cbMovCreateFanartjpg
        root.AppendChild(doc, "basicsavemode",                      basicsavemode)                      'chkbx_basicsave
        root.AppendChild(doc, "namemode",                           namemode)                           'cbxNameMode
        root.AppendChild(doc, "usetransparency",                    usetransparency)                    'set from frmOptions - obsolete
        root.AppendChild(doc, "transparencyvalue",                  transparencyvalue)                  'set from frmOptions - obsolete
        root.AppendChild(doc, "NoAltTitle",                         NoAltTitle)                         'cbNoAltTitle
        root.AppendChild(doc, "XtraFrodoUrls",                      XtraFrodoUrls)                      'cbXtraFrodoUrls
        root.AppendChild(doc, "disablelogs",                        disablelogfiles)                    'CheckBox16
        root.AppendChild(doc, "DisplayRatingOverlay",               DisplayRatingOverlay)               'cbDisplayRatingOverlay
        root.AppendChild(doc, "DisplayMediainfoOverlay",            DisplayMediainfoOverlay)            'cbDisplayMediainfoOverlay
        root.AppendChild(doc, "DisplayMediaInfoFolderSize",         DisplayMediaInfoFolderSize)         'cbDisplayMediaInfoFolderSize
        root.AppendChild(doc, "incmissingmovies",                   incmissingmovies)                   'cbMissingMovie
        root.AppendChild(doc, "savefanart",                         savefanart)                         'cbMovFanartScrape
        root.AppendChild(doc, "movfanarttvscrape",                  MovFanartTvscrape)                  'cbMovFanartTvScrape
        root.AppendChild(doc, "MovFanartTvDlClearArt",              MovFanartTvDlClearArt)              
        root.AppendChild(doc, "MovFanartTvDlClearLogo",             MovFanartTvDlClearLogo)
        root.AppendChild(doc, "MovFanartTvDlPoster",                MovFanartTvDlPoster)
        root.AppendChild(doc, "MovFanartTvDlFanart",                MovFanartTvDlFanart)
        root.AppendChild(doc, "MovFanartTvDlDisc",                  MovFanartTvDlDisc)
        root.AppendChild(doc, "MovFanartTvDlBanner",                MovFanartTvDlBanner)
        root.AppendChild(doc, "MovFanartTvDlLandscape",             MovFanartTvDlLandscape)
        root.AppendChild(doc, "fanartjpg",                          fanartjpg)                          'cbMovieFanartInFolders
        root.AppendChild(doc, "roundminutes",                       roundminutes)                       'set from frmOptions - obsolete
        root.AppendChild(doc, "ignoreparts",                        movieignorepart)                    'cbxCleanFilenameIgnorePart
        root.AppendChild(doc, "cleantags",                          moviecleanTags)                     'btnCleanFilenameAdd,btnCleanFilenameRemove
        root.AppendChild(doc, "moviesUseXBMCScraper",               movies_useXBMC_Scraper)             'CheckBox_Use_XBMC_Scraper
        root.AppendChild(doc, "TmdbActorsImdbScrape",               TmdbActorsImdbScrape)               'cbImdbgetTMDBActor 
        'root.AppendChild(doc, "TMDBPreferredCertCountry",           TMDBPreferredCertCountry)           'cmbxTMDBPreferredCertCountry
        root.AppendChild(doc, "ImdbPrimaryPlot",                    ImdbPrimaryPlot)                    'cbImdbPrimaryPlot  
        root.AppendChild(doc, "xbmcscraper",                        XBMC_Scraper)                       
        root.AppendChild(doc, "XbmcTmdbRenameMovie",                XbmcTmdbRenameMovie)                'cbXbmcTmdbRename
        root.AppendChild(doc, "XbmcTmdbMissingFromImdb",            XbmcTmdbMissingFromImdb)            'cbXbmcTmdbMissingFromImdb
        root.AppendChild(doc, "XbmcTmdbTop250FromImdb",             XbmcTmdbTop250FromImdb)             'cbXbmcTmdbMissingTop250FromImdb
        root.AppendChild(doc, "XbmcTmdbVotesFromImdb",              XbmcTmdbVotesFromImdb)              'cbXbmcTmdbImdbVotes
        root.AppendChild(doc, "XbmcTmdbCertFromImdb",               XbmcTmdbCertFromImdb)               'cbXbmcTmdbCertFromImdb
        root.AppendChild(doc, "XbmcTmdbStarsFromImdb",              XbmcTmdbStarsFromImdb)              'cbXbmcTmdbStarsFromImdb
        root.AppendChild(doc, "XbmcTmdbAkasFromImdb",               XbmcTmdbAkasFromImdb)               'cbXbmcTmdbAkasFromImdb
        root.AppendChild(doc, "XbmcTmdbActorDL",                    XbmcTmdbActorDL)                    'cbXbmcTmdbActorDL
        root.AppendChild(doc, "scrapefullcert",                     scrapefullcert)                     'ScrapeFullCertCheckBox
        root.AppendChild(doc, "offlinemovielabeltext",              OfflineDVDTitle)                    'TextBox_OfflineDVDTitle
        root.AppendChild(doc, "moviemanualrename",                  MovieManualRename)                  'MovieManualRename
        root.AppendChild(doc, "MovieRenameEnable",                  MovieRenameEnable)                  'cbMovieRenameEnable
        root.AppendChild(doc, "movierenametemplate",                MovieRenameTemplate)                'tb_MovieRenameEnable
        root.AppendChild(doc, "MovFolderRename",                    MovFolderRename)                    'cbMovFolderRename
        root.AppendChild(doc, "MovFolderRenameTemplate",            MovFolderRenameTemplate)            'tb_MovFolderRename
        root.AppendChild(doc, "MovNewFolderInRootFolder",           MovNewFolderInRootFolder)           'cbMovNewFolderInRootFolder
        root.AppendChild(doc, "MovRenameUnderscore",                MovRenameSpaceCharacter)            'cbRenameUnderscore
        root.AppendChild(doc, "MovSetIgnArticle",                   MovSetIgnArticle)                   'cbMovSetIgnArticle
        root.AppendChild(doc, "MovSortIgnArticle",                  MovSortIgnArticle)                  'cbMovSortIgnArticle
        root.AppendChild(doc, "MovTitleIgnArticle",                 MovTitleIgnArticle)                 'cbMovTitleIgnArticle
        root.AppendChild(doc, "MovTitleCase",                       MovTitleCase)                       'cbMovTitleCase
        root.AppendChild(doc, "ExcludeMpaaRated",                   ExcludeMpaaRated)                   'cbExcludeMpaaRated
        root.AppendChild(doc, "MovThousSeparator",                  MovThousSeparator)                  'cbMovThousSeparator
        root.AppendChild(doc, "showsortdate",                       showsortdate)                       'CheckBox_ShowDateOnMovieList
        root.AppendChild(doc, "moviePreferredHDTrailerResolution",  moviePreferredTrailerResolution)    'cbPreferredTrailerResolution
        root.AppendChild(doc, "GetMovieSetFromTMDb",                GetMovieSetFromTMDb)                'cbGetMovieSetFromTMDb
        root.AppendChild(doc, "TMDbSelectedLanguage",               TMDbSelectedLanguageName)           'comboBoxTMDbSelectedLanguage
        root.AppendChild(doc, "TMDbUseCustomLanguage",              TMDbUseCustomLanguage)              'cbUseCustomLanguage
        root.AppendChild(doc, "TMDbCustomLanguage",                 TMDbCustomLanguageValue)            'tbCustomLanguageValue
        root.AppendChild(doc, "ActorResolution",                    ActorResolutionSI)                  'comboActorResolutions
        root.AppendChild(doc, "PosterResolution",                   PosterResolutionSI)                 'comboPosterResolutions
        root.AppendChild(doc, "BackDropResolution",                 BackDropResolutionSI)               'comboBackDropResolutions
        root.AppendChild(doc, "DateFormat",                         DateFormat)                         'tbDateFormat
        root.AppendChild(doc, "MovieList_ShowColPlot",              MovieList_ShowColPlot)              'cbMovieList_ShowColPlot
        root.AppendChild(doc, "DisableNotMatchingRenamePattern",    DisableNotMatchingRenamePattern)    'cDisableNotMatchingRenamePattern
        root.AppendChild(doc, "MovieList_ShowColWatched",           MovieList_ShowColWatched)           'cbMovieList_ShowColWatched
        root.AppendChild(doc, "MovieScraper_MaxStudios",            MovieScraper_MaxStudios)            'nudMovieScraper_MaxStudios

        root.AppendChild(doc, "ActorsFilterMinFilms",               ActorsFilterMinFilms)               'nudActorsFilterMinFilms
        root.AppendChild(doc, "MaxActorsInFilter",                  MaxActorsInFilter)                  'nudMaxActorsInFilter
        root.AppendChild(doc, "MovieFilters_Actors_Order",          MovieFilters_Actors_Order)          'cbMovieFilters_Actors_Order

        root.AppendChild(doc, "DirectorsFilterMinFilms",            DirectorsFilterMinFilms)            'nudDirectorsFilterMinFilms
        root.AppendChild(doc, "MaxDirectorsInFilter",               MaxDirectorsInFilter)               'nudMaxDirectorsInFilter
        root.AppendChild(doc, "MovieFilters_Directors_Order",       MovieFilters_Directors_Order)       'cbMovieFilters_Directors_Order

        root.AppendChild(doc, "SetsFilterMinFilms",                 SetsFilterMinFilms)                 'nudSetsFilterMinFilms
        root.AppendChild(doc, "MaxSetsInFilter",                    MaxSetsInFilter)                    'nudMaxSetsInFilter
        root.AppendChild(doc, "MovieFilters_Sets_Order",            MovieFilters_Sets_Order)            'cbMovieFilters_Sets_Order
        root.AppendChild(doc, "Original_Title",                     Original_Title         )            'chkbOriginal_Title
        root.AppendChild(doc, "UseMultipleThreads",                 UseMultipleThreads     )            'cbUseMultipleThreads

        root.AppendChildList(doc, "moviethumbpriority"  ,           moviethumbpriority.ToArray)         'Button61,Button73
        root.AppendChildList(doc, "releaseformat"       ,           releaseformat         )             'btnVideoSourceAdd,btnVideoSourceRemove
        root.AppendChildList(doc, "certificatepriority" ,           certificatepriority   )             'Button74,Button75
        root.AppendChildList(doc, "movseplst",                      MovSepLst.ToArray)                  'lb_MovSepLst
        root.AppendChild(doc, "MovFiltLastSize",                    MovFiltLastSize)                    'Preference.MovFiltLastSize
        root.AppendChild(doc, "RenameSpaceCharacter",               RenameSpaceCharacter)               'Preference.RenameSpaceCharacter
        root.AppendChild(doc, "XbmcTmdbScraperFanart",              XbmcTmdbScraperFanart)              'cbXbmcTmdbFanart
        root.AppendChild(doc, "XbmcTmdbScraperTrailerQ",            XbmcTmdbScraperTrailerQ)            'cmbxXbmcTmdbHDTrailer
        root.AppendChild(doc, "XbmcTmdbScraperLanguage",            XbmcTmdbScraperLanguage)            'cmbxXbmcTmdbTitleLanguage
        root.AppendChild(doc, "XbmcTmdbScraperRatings",             XbmcTmdbScraperRatings)             'cbXbmcTmdbIMDBRatings
        root.AppendChild(doc, "XbmcTmdbScraperCertCountry",         XbmcTmdbScraperCertCountry)         '

        root.AppendChild(movie_filters.GetChild(doc))


        'TV Prefs ------------------------------------------------------------
        root.AppendChild(doc, "tvshowautoquick",        tvshowautoquick)        'cbTvQuickAddShow
        root.AppendChild(doc, "copytvactorthumbs",      copytvactorthumbs)      'CheckBox34
        root.AppendChild(doc, "tvdbmode",               sortorder)              'RadioButton42
        root.AppendChild(doc, "tvdbactorscrape",        TvdbActorScrape)        'ComboBox8
        root.AppendChild(doc, "tvfolderjpg",            tvfolderjpg)            'cb_TvFolderJpg
        root.AppendChild(doc, "seasonfolderjpg",        seasonfolderjpg)        'cbseasonfolderjpg
        root.AppendChild(doc, "downloadtvfanart",       tvdlfanart)             'cbTvDlFanart
        root.AppendChild(doc, "downloadtvposter",       tvdlposter)             'cbTvDlPosterArt
        root.AppendChild(doc, "downloadtvseasonthumbs", tvdlseasonthumbs)       'cbTvDlSeasonArt
        root.AppendChild(doc, "TvDlFanartTvArt",        TvDlFanartTvArt)        'cbTvDlFanartTvArt
        root.AppendChild(doc, "TvFanartTvFirst",        TvFanartTvFirst)        'cbTvFanartTvFirst
        root.AppendChild(doc, "dlTVxtrafanart",         dlTVxtrafanart)         'cbDlTVxtrafanart
        root.AppendChild(doc, "TvXtraFanartQty",        TvXtraFanartQty)        'cmbxTvXtraFanartQty
        root.AppendChild(doc, "hdtvtags",               enabletvhdtags)         'CheckBox20
        root.AppendChild(doc, "disabletvlogs",          disabletvlogs)          'CheckBox17
        root.AppendChild(doc, "postertype",             postertype)             'posterbtn
        root.AppendChild(doc, "seasonall",              seasonall)              'RadioButton39-41
        root.AppendChild(doc, "tvrename",               tvrename)               'ComboBox_tv_EpisodeRename
        root.AppendChild(doc, "eprenamelowercase",      eprenamelowercase)      'CheckBox_tv_EpisodeRenameCase
        root.AppendChild(doc, "tvshowrefreshlog",       tvshowrefreshlog)       'set from frmOptions - obsolete
        root.AppendChild(doc, "autoepisodescreenshot",  autoepisodescreenshot)  'cbTvAutoScreenShot
        root.AppendChild(doc, "tvscrnshtTVDBResize",    tvscrnshtTVDBResize)    'cbTvScrnShtTVDBResize
        root.AppendChild(doc, "TVShowUseXBMCScraper",   tvshow_useXBMC_Scraper) 'CheckBox_Use_XBMC_TVDB_Scraper
        root.AppendChild(doc, "autorenameepisodes",     autorenameepisodes)     'CheckBox_tv_EpisodeRenameAuto
        root.AppendChild(doc, "ScrShtDelay",            ScrShtDelay)            'AutoScrShtDelay
        root.AppendChild(doc, "lastrefreshmissingdate", lastrefreshmissingdate)
        root.AppendChild(doc, "excludefromshowfoldername", excludefromshowfoldername)

        tempstring = TvdbLanguageCode & "|" & TvdbLanguage
        root.AppendChild(doc, "tvdblanguage", tempstring)                       'ListBox12,Button91

        root.AppendChild(doc, "XBMC_Active", XBMC_Active)
        root.AppendChild( doc, "XBMC_Link"                   , XBMC_Link                 )
        root.AppendChild( doc, "XBMC_Address"                , XBMC_Address              )
        root.AppendChild(doc, "XBMC_Port", XBMC_Port) 'cbXBMC_Active
        root.AppendChild( doc, "XBMC_Username"               , XBMC_Username             )
        root.AppendChild( doc, "XBMC_Password"               , XBMC_Password             )
        root.AppendChild( doc, "XBMC_UserdataFolder"         , XBMC_UserdataFolder       )
        root.AppendChild( doc, "XBMC_TexturesDb"             , XBMC_TexturesDb           )
        root.AppendChild( doc, "XBMC_ThumbnailFolders"       , XBMC_ThumbnailsFolder     )
        root.AppendChild( doc, "XBMC_Delete_Cached_Images"   , XBMC_Delete_Cached_Images )
        
        root.AppendChild( doc, "ShowExtraMovieFilters"       , ShowExtraMovieFilters     )
        

        root.AppendChild(XBMC_MC_MovieFolderMappings.GetChild(doc))
        root.AppendChild(XBMC_MC_CompareFields      .GetChild(doc))
        
        doc.AppendChild(root)

        If String.IsNullOrEmpty(workingProfile.Config) Then
            workingProfile.Config = IO.Path.Combine(applicationPath, "settings\config.xml")
        End If


        Dim output As XmlTextWriter = Nothing


        Try
            output = New XmlTextWriter(workingProfile.Config, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
        Catch ex As Exception
            Dim x = ex
        Finally
            If Not IsNothing(output) Then output.Close
        End Try
        Proxyreload()
    End Sub



    Public Shared Sub ConfigLoad()
        commandlist.Clear()
        moviesets.Clear()
        moviesets.Add("-None-")
        movietags.Clear()
        movieFolders.Clear()
        offlinefolders.Clear()
        stubfolder = ""
        tvFolders.Clear()
        tvRootFolders.Clear()
        tableview.Clear()
        moviethumbpriority.Clear()
        'MovSepLst.Clear()
        homemoviefolders.Clear() 
        MVidFolders.Clear()
        movie_filters.Reset()
        engineupdate()
        ExcludeFolders.Clear()


        If Not IO.File.Exists(workingProfile.Config) Then
            Exit Sub
        End If

        Dim prefs As New XmlDocument
        Try
            prefs.Load(workingProfile.Config)
        Catch ex As Exception
            MsgBox("Error : pr24")
        End Try


        For Each thisresult As XmlNode In prefs("xbmc_media_companion_config_v1.0")
            '            If thisresult.InnerText <> "" Then  'If blank, preference remains at default value
            If thisresult.InnerXml <> "" Then  'If blank, preference remains at default value

                Select Case thisresult.Name
                    Case "moviesets"

                        For Each thisset In thisresult.ChildNodes
                            Select Case thisset.Name
                                Case "set"
                                    If thisset.InnerText <> "" Then moviesets.Add(thisset.InnerText)
                            End Select
                        Next
                    Case "movietags"

                        For Each thisset In thisresult.ChildNodes
                            Select Case thisset.Name
                                Case "tag"
                                    If thisset.InnerText <> "" Then movietags.Add(thisset.InnerText)
                            End Select
                        Next
                    Case "table"

                        For Each thistable In thisresult.ChildNodes
                            Select Case thistable.Name
                                Case "tab"
                                    tableview.Add(thistable.InnerText)
                                Case "sort"
                                    tablesortorder = thistable.InnerText
                            End Select
                        Next
                    Case "comms"
                        Dim newcom As New str_ListOfCommands(SetDefaults)
                        For Each thistable In thisresult.ChildNodes
                            Select Case thistable.Name
                                Case "title"
                                    newcom.title = thistable.InnerText
                                Case "command"
                                    newcom.command = thistable.InnerText
                            End Select
                        Next
                        If newcom.command <> "" And newcom.title <> "" Then
                            commandlist.Add(newcom)
                        End If

                    Case "nfofolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        Dim t() As String = decodestring.Split("|")
                        Dim u As New str_RootPaths
                        u.rpath = t(0)
                        If t.Count > 1 Then u.selected = t(1)
                        movieFolders.Add(u)
                    Case "stubfolder"
                        stubfolder = thisresult.InnerText 
                    Case "stubmessage"
                        stubmessage = thisresult.InnerText
                    Case "offlinefolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        offlinefolders.Add(decodestring)
                    Case "tvfolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        tvFolders.Add(decodestring)
                    Case "tvrootfolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        Dim t() As String = decodestring.Split("|")
                        Dim u As New str_RootPaths
                        u.rpath = t(0)
                        If t.Count > 1 Then u.selected = t(1)
                        tvRootFolders.Add(u)
                    Case "homemoviefolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        Dim t() As String = decodestring.Split("|")
                        Dim u As New str_RootPaths
                        u.rpath = t(0)
                        If t.Count > 1 Then u.selected = t(1)
                        homemoviefolders.Add(u)
                    Case "MVidFolders"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        MVidFolders.Add(decodestring)

                    Case "ExcludeFolders" : ExcludeFolders.Load(thisresult)

                    Case "moviethumbpriority"
                        'ReDim moviethumbpriority(3)
                        Dim tmp() As String = thisresult.InnerXml.Split("|")
                        For Each t In tmp
                            moviethumbpriority.Add(t)
                        Next
                        'moviethumbpriority = thisresult.InnerXml.Split("|")
                    Case "movseplst"
                        Dim tmp() As String = thisresult.InnerXml.Split("|")
                        MovSepLst.Clear()
                        For Each t In tmp
                            MovSepLst.Add(t)
                        Next

                    Case "certificatepriority"
                        ReDim certificatepriority(33)
                        certificatepriority = thisresult.InnerXml.Split("|")

                    Case "releaseformat"
                        Dim count As Integer = 0
                        Dim index As Integer = 0
                        Do Until index < 0
                            count += 1
                            index = thisresult.InnerText.IndexOf("|"c, index + 1)
                        Loop
                        ReDim releaseformat(count)
                        releaseformat = thisresult.InnerXml.Split("|")

                    Case "tvdblanguage"
                        Dim partone() As String
                        partone = thisresult.InnerXml.Split("|")
                        For f = 0 To 1
                            If partone(0).Length = 2 Then
                                TvdbLanguageCode = partone(0)
                                TvdbLanguage = partone(1)
                                Exit For
                            Else
                                TvdbLanguageCode = partone(1)
                                TvdbLanguage = partone(0)
                            End If
                        Next

                    Case "TmdbActorsImdbScrape"                 : TmdbActorsImdbScrape = thisresult.InnerXml
                    Case "ImdbPrimaryPlot"                      : ImdbPrimaryPlot = thisresult.InnerXml 
                    'Case "xbmcscraper"                          : XBMC_Scraper = thisresult.InnerText    -  locked at "tmdb"
                    Case "XbmcTmdbRenameMovie"                  : XbmcTmdbRenameMovie = thisresult.InnerText 
                    Case "XbmcTmdbMissingFromImdb"              : XbmcTmdbMissingFromImdb = thisresult.InnerText
                    Case "XbmcTmdbTop250FromImdb"               : XbmcTmdbTop250FromImdb = thisresult.InnerText 
                    Case "XbmcTmdbVotesFromImdb"                : XbmcTmdbVotesFromImdb = thisresult.InnerText
                    Case "XbmcTmdbCertFromImdb"                 : XbmcTmdbCertFromImdb = thisresult.InnerText 
                    Case "XbmcTmdbStarsFromImdb"                : XbmcTmdbStarsFromImdb = thisresult.InnerText
                    Case "XbmcTmdbAkasFromImdb"                 : XbmcTmdbAkasFromImdb = thisresult.InnerText
                    Case "XbmcTmdbActorDL"                      : XbmcTmdbActorDL = thisresult.InnerText
                    Case "seasonall"                            : seasonall = thisresult.InnerText
                    Case "splitcontainer1"                      : splt1 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer2"                      : splt2 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer3"                      : splt3 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer4"                      : splt4 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer5"                      : splt5 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer6"                      : splt6 = Convert.ToInt32(thisresult.InnerText)
                    Case "tvbannersplit"                        : tvbannersplit = Convert.ToDouble(thisresult.InnerText)
                    Case "maximised"                            : maximised = thisresult.InnerXml
                    Case "locx"                                 : locx = Convert.ToInt32(thisresult.InnerText)
                    Case "locy"                                 : locy = Convert.ToInt32(thisresult.InnerText)
                    Case "gettrailer"                           : gettrailer = thisresult.InnerXml
                    Case "DownloadTrailerDuringScrape"          : DownloadTrailerDuringScrape = thisresult.InnerXml
                    Case "tvshowautoquick"                      : tvshowautoquick = thisresult.InnerXml
                    Case "intruntime"                           : intruntime = thisresult.InnerXml
                    Case "startupcache" : startupCache = thisresult.InnerXml
                    Case "ignoretrailers"                       : ignoretrailers = thisresult.InnerXml
                    Case "ignoreactorthumbs"                    : ignoreactorthumbs = thisresult.InnerXml
                    Case "font"                                 : font = thisresult.InnerXml
                    Case "maxactors"                            : maxactors = Convert.ToInt32(thisresult.InnerXml)
                    Case "keywordasTag"                         : keywordasTag = thisresult.InnerXml
                    Case "keywordlimit"                         : keywordlimit = Convert.ToInt32(thisresult.InnerXml)
                    Case "maxmoviegenre"                        : maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)
                    Case "enablehdtags"                         : enablehdtags = thisresult.InnerXml
                    Case "MovDurationAsRuntine"                 : MovDurationAsRuntine = thisresult.InnerXml 
                    Case "movieruntimedisplay"                  : movieRuntimeDisplay = thisresult.InnerXml
                    Case "movieRuntimeFallbackToFile"           : movieRuntimeFallbackToFile = thisresult.InnerXml
                    Case "hdtvtags"                             : enabletvhdtags = thisresult.InnerXml
                    Case "renamenfofiles"                       : renamenfofiles = thisresult.InnerXml
                    Case "logview"                              : logview = thisresult.InnerXml
                    Case "fanartnotstacked"                     : fanartnotstacked = thisresult.InnerXml
                    Case "posternotstacked"                     : posternotstacked = thisresult.InnerXml
'                   Case "downloadfanart"                       : savefanart = thisresult.InnerXml
                    Case "scrapemovieposters"                   : scrapemovieposters = thisresult.InnerXml
                    Case "movrootfoldercheck"                   : movrootfoldercheck = thisresult.InnerXml 
                    Case "posterjpg"                            : posterjpg = thisresult.InnerXml 
                    Case "usefanart"                            : usefanart = thisresult.InnerXml
                    Case "dontdisplayposter"                    : dontdisplayposter = thisresult.InnerXml
                    Case "rarsize"                              : rarsize = Convert.ToInt32(thisresult.InnerXml)
                    Case "actorsave"                            : actorsave = thisresult.InnerXml
                    Case "actorseasy"                           : actorseasy = thisresult.InnerXml
                    Case "copytvactorthumbs"                    : copytvactorthumbs = thisresult.InnerXml
                    Case "displayMissingEpisodes"               : displayMissingEpisodes = thisresult.InnerXml
                    Case "ignoreMissingSpecials"                : ignoreMissingSpecials = thisresult.InnerXml 
                    Case "TvMissingEpOffset"                    : TvMissingEpOffset = thisresult.InnerXml
                    Case "actorsavepath"                        : actorsavepath = decxmlchars(thisresult.InnerText)
                    Case "actorsavealpha"                       : actorsavealpha = thisresult.InnerXml
                    Case "actornetworkpath"                     : actornetworkpath = decxmlchars(thisresult.InnerText)
                    Case "overwritethumbs"                      : overwritethumbs = thisresult.InnerXml
                    Case "LocalActorImage"                      : LocalActorImage = thisresult.InnerText 
                    Case "imdbmirror"                           : imdbmirror = thisresult.InnerXml
                    Case "cleantags"                            : moviecleanTags = thisresult.InnerXml
                    Case "ignoreparts"                          : movieignorepart = thisresult.InnerXml
                    Case "backgroundcolour"                     : backgroundcolour = thisresult.InnerXml
                    Case "forgroundcolour"                      : forgroundcolour = thisresult.InnerXml
                    Case "remembersize"                         : remembersize = thisresult.InnerXml
                    Case "formheight"                           : formheight = Convert.ToInt32(thisresult.InnerXml)
                    Case "formwidth"                            : formwidth = Convert.ToInt32(thisresult.InnerXml)
                    Case "usefoldernames"                       : usefoldernames = thisresult.InnerXml
                    Case "movxtrathumb"                         : movxtrathumb = thisresult.InnerXml
                    Case "movxtrafanart"                        : movxtrafanart = thisresult.InnerXml
                    Case "movxtrafanartqty"                     : movxtrafanartqty = thisresult.InnerXml 
                    Case "dlxtrafanart"                         : dlxtrafanart = thisresult.InnerXml
                    Case "dlMovSetArtwork"                      : dlMovSetArtwork = thisresult.InnerXml
                    Case "MovSetArtSetFolder"                   : MovSetArtSetFolder = thisresult.InnerXml 
                    Case "MovSetArtCentralFolder"               : MovSetArtCentralFolder = thisresult.InnerText  
                    Case "dlTVxtrafanart"                       : dlTVxtrafanart = thisresult.InnerXml
                    Case "TvXtraFanartQty"                      : TvXtraFanartQty = thisresult.InnerXml 
                    Case "TvDlFanartTvArt"                      : TvDlFanartTvArt = thisresult.InnerXml 
                    Case "TvFanartTvFirst"                      : TvFanartTvFirst = thisresult.InnerXml 
                    Case "allfolders"                           : allfolders = thisresult.InnerXml
                    Case "createfolderjpg"                      : createfolderjpg = thisresult.InnerXml
                    Case "createfanartjpg"                      : createfanartjpg = thisresult.InnerXml 
                    Case "basicsavemode"                        : basicsavemode = thisresult.InnerXml
                    Case "namemode"                             : namemode = thisresult.InnerXml
                    Case "tvdbmode"                             : sortorder = thisresult.InnerXml
                    Case "tvdbactorscrape"                      : TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)
                    Case "usetransparency"                      : usetransparency = thisresult.InnerXml
                    Case "transparencyvalue"                    : transparencyvalue = Convert.ToInt32(thisresult.InnerXml)
                    Case "downloadtvfanart"                     : tvdlfanart = thisresult.InnerXml
                    Case "tvfolderjpg"                          : tvfolderjpg = thisresult.InnerXml
                    Case "seasonfolderjpg"                      : seasonfolderjpg = thisresult.InnerXml 
                    Case "roundminutes"                         : roundminutes = thisresult.InnerXml
                    Case "autoepisodescreenshot"                : autoepisodescreenshot = thisresult.InnerXml
                    Case "tvscrnshtTVDBResize"                  : tvscrnshtTVDBResize = thisresult.InnerXml 
                    Case "ignorearticle"                        : ignorearticle = thisresult.InnerXml
                    Case "ignoreAarticle"                       : ignoreAarticle = thisresult.InnerXml
                    Case "ignoreAn"                             : ignoreAn = thisresult.InnerXml 
                    Case "sorttitleignorearticle"               : sorttitleignorearticle = thisresult.InnerXml
                    Case "TVShowUseXBMCScraper"                 : tvshow_useXBMC_Scraper = thisresult.InnerXml
                    Case "moviesUseXBMCScraper"                 : movies_useXBMC_Scraper = thisresult.InnerXml
                    Case "downloadtvposter"                     : tvdlposter = thisresult.InnerXml
                    Case "downloadtvseasonthumbs"               : tvdlseasonthumbs = thisresult.InnerXml
                    Case "maximumthumbs"                        : maximumthumbs = Convert.ToInt32(thisresult.InnerXml)
                    Case "lastrefreshmissingdate"               : lastrefreshmissingdate = thisresult.InnerText 
                    Case "excludefromshowfoldername"            : excludefromshowfoldername = thisresult.InnerText 
                    Case "preferredscreen"                      : preferredscreen = Convert.ToInt32(thisresult.InnerXml)
                    Case "hdtags"                               : enablehdtags = thisresult.InnerXml
                    Case "NoAltTitle"                           : NoAltTitle = thisresult.InnerXml 
                    Case "XtraFrodoUrls"                        : XtraFrodoUrls = thisresult.InnerXml
                    Case "disablelogs"                          : disablelogfiles = thisresult.InnerXml
                    Case "DisplayRatingOverlay"                 : DisplayRatingOverlay = thisresult.InnerXml
                    Case "DisplayMediainfoOverlay"              : DisplayMediainfoOverlay = thisresult.InnerXml
                    Case "DisplayMediaInfoFolderSize"           : DisplayMediaInfoFolderSize = thisresult.InnerXml
                    Case "incmissingmovies"                     : incmissingmovies = thisresult.InnerText
                    Case "disabletvlogs"                        : disabletvlogs = thisresult.InnerXml
                    Case "folderjpg"                            : createfolderjpg = thisresult.InnerXml
                    Case "savefanart"                           : savefanart = thisresult.InnerXml
                    Case "movfanarttvscrape"                    : MovFanartTvscrape = thisresult.InnerXml 
                    Case "MovFanartTvDlClearArt"                : MovFanartTvDlClearArt = thisresult.InnerXml
                    Case "MovFanartTvDlClearLogo"               : MovFanartTvDlClearLogo = thisresult.InnerXml
                    Case "MovFanartTvDlPoster"                  : MovFanartTvDlPoster = thisresult.InnerXml
                    Case "MovFanartTvDlFanart"                  : MovFanartTvDlFanart = thisresult.InnerXml
                    Case "MovFanartTvDlDisc"                    : MovFanartTvDlDisc = thisresult.InnerXml
                    Case "MovFanartTvDlBanner"                  : MovFanartTvDlBanner = thisresult.InnerXml
                    Case "MovFanartTvDlLandscape"               : MovFanartTvDlLandscape = thisresult.InnerXml
                    Case "fanartjpg"                            : fanartjpg = thisresult.InnerXml
                    Case "postertype"                           : postertype = thisresult.InnerXml
                    Case "videomode"                            : videomode = Convert.ToInt32(thisresult.InnerXml)
                    Case "selectedvideoplayer"                  : selectedvideoplayer = thisresult.InnerXml
                    Case "maximagecount"                        : maximagecount = Convert.ToInt32(thisresult.InnerXml)
                    Case "lastpath"                             : lastpath = thisresult.InnerXml
                    Case "moviescraper"                         : moviescraper = thisresult.InnerXml
                    Case "nfoposterscraper"                     : nfoposterscraper = thisresult.InnerXml
                    Case "alwaysuseimdbid"                      : alwaysuseimdbid = thisresult.InnerXml
                    Case "externalbrowser"                      : externalbrowser = thisresult.InnerXml
                    Case "selectedBrowser"                      : selectedBrowser = thisresult.InnerXml
                    Case "altnfoeditor"                         : altnfoeditor = thisresult.InnerXml
                    Case "tvrename"                             : tvrename = Convert.ToInt32(thisresult.InnerText)
                    Case "tvshowrefreshlog"                     : tvshowrefreshlog = thisresult.InnerXml
                    Case "autorenameepisodes"                   : autorenameepisodes = thisresult.InnerXml
                    Case "ScrShtDelay"                          : ScrShtDelay = Convert.ToInt32(thisresult.InnerXml)
                    Case "eprenamelowercase"                    : eprenamelowercase = thisresult.InnerXml
                    Case "moviedefaultlist"                     : moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                    Case "startuptab"                           : startuptab = Convert.ToByte(thisresult.InnerText)
                    Case "offlinemovielabeltext"                : OfflineDVDTitle = thisresult.InnerText
                    Case "moviemanualrename"                    : MovieManualRename = thisresult.InnerXml
                    Case "MovieRenameEnable"                    : MovieRenameEnable = thisresult.InnerXml
                    Case "movierenametemplate"                  : MovieRenameTemplate = thisresult.InnerText
                    Case "MovFolderRename"                      : MovFolderRename = thisresult.InnerText 
                    Case "MovFolderRenameTemplate"              : MovFolderRenameTemplate = thisresult.InnerText 
                    Case "MovNewFolderInRootFolder"             : MovNewFolderInRootFolder = thisresult.InnerXml 
                    Case "MovRenameUnderscore"                  : MovRenameSpaceCharacter = thisresult.InnerText 
                    Case "MovSetIgnArticle"                     : MovSetIgnArticle = thisresult.InnerXml 
                    Case "MovSortIgnArticle"                    : MovSortIgnArticle = thisresult.InnerXml 
                    Case "MovTitleIgnArticle"                   : MovTitleIgnArticle = thisresult.InnerXml
                    Case "MovTitleCase"                         : MovTitleCase = thisresult.InnerXml
                    Case "ExcludeMpaaRated"                     : ExcludeMpaaRated = thisresult.InnerXml 
                    Case "MovThousSeparator"                    : MovThousSeparator = thisresult.InnerXml 
                    Case "showsortdate"                         : showsortdate = thisresult.InnerText
                    Case "scrapefullcert"                       : scrapefullcert = thisresult.InnerXml
                    Case "moviePreferredHDTrailerResolution"    : moviePreferredTrailerResolution = thisresult.InnerXml.ToUpper()
                    Case "MovieImdbGenreRegEx"                  : MovieImdbGenreRegEx = decxmlchars(thisresult.InnerXml)
                    Case "xbmcartwork"                          : XBMC_version = Convert.ToByte(thisresult.InnerText)
                    Case "GetMovieSetFromTMDb"                  : GetMovieSetFromTMDb = thisresult.InnerXml
                    Case "LogScrapeTimes"                       : LogScrapeTimes = thisresult.InnerXml
                    Case "ScrapeTimingsLogThreshold"            : ScrapeTimingsLogThreshold = thisresult.InnerXml
                    Case "TMDbSelectedLanguage"                 : TMDbSelectedLanguageName = thisresult.InnerXml
                    Case "TMDbUseCustomLanguage"                : TMDbUseCustomLanguage = thisresult.InnerXml
                    Case "TMDbCustomLanguage"                   : TMDbCustomLanguageValue = thisresult.InnerXml
                    Case "ActorResolution"                      : ActorResolutionSI = thisresult.InnerXml
                    Case "PosterResolution"                     : PosterResolutionSI = thisresult.InnerXml
                    Case "BackDropResolution"                   : BackDropResolutionSI = thisresult.InnerXml
                    Case "ShowMovieGridToolTip"                 : ShowMovieGridToolTip = thisresult.InnerXml
                    Case "ShowLogOnError"                       : ShowLogOnError = thisresult.InnerXml
                    Case "DateFormat"                           : DateFormat = thisresult.InnerXml
                    Case "MovieList_ShowColPlot"                : MovieList_ShowColPlot = thisresult.InnerXml
                    Case "DisableNotMatchingRenamePattern"      : DisableNotMatchingRenamePattern = thisresult.InnerXml
                    Case "MovieList_ShowColWatched"             : MovieList_ShowColWatched = thisresult.InnerXml
                    Case "moviesortorder"                       : moviesortorder = thisresult.InnerXml
                    Case "movieinvertorder"                     : movieinvertorder = thisresult.InnerXml
                    Case "MovieScraper_MaxStudios"              : MovieScraper_MaxStudios = thisresult.InnerXml
                    Case "MovFiltLastSize"                      : MovFiltLastSize = thisresult.InnerXml 
                    Case "RenameSpaceCharacter"                 : RenameSpaceCharacter = thisresult.InnerText
                    Case "XbmcTmdbScraperFanart"                : XbmcTmdbScraperFanart = thisresult.InnerText
                    Case "XbmcTmdbScraperTrailerQ"              : XbmcTmdbScraperTrailerQ = thisresult.InnerText
                    Case "XbmcTmdbScraperLanguage"              : XbmcTmdbScraperLanguage = thisresult.InnerText
                    Case "XbmcTmdbScraperRatings"               : XbmcTmdbScraperRatings = thisresult.InnerText
                    Case "XbmcTmdbScraperCertCountry"           : XbmcTmdbScraperCertCountry = thisresult.InnerText

                    Case "ActorsFilterMinFilms"                 : ActorsFilterMinFilms      = thisresult.InnerXml
                    Case "MaxActorsInFilter"                    : MaxActorsInFilter         = thisresult.InnerXml
                    Case "MovieFilters_Actors_Order"            : MovieFilters_Actors_Order = thisresult.InnerXml

                    Case "DirectorsFilterMinFilms"              : DirectorsFilterMinFilms      = thisresult.InnerXml
                    Case "MaxDirectorsInFilter"                 : MaxDirectorsInFilter         = thisresult.InnerXml
                    Case "MovieFilters_Directors_Order"         : MovieFilters_Directors_Order = thisresult.InnerXml

                    Case "SetsFilterMinFilms"                   : SetsFilterMinFilms        = thisresult.InnerXml
                    Case "MaxSetsInFilter"                      : MaxSetsInFilter           = thisresult.InnerXml
                    Case "MovieFilters_Sets_Order"              : MovieFilters_Sets_Order   = thisresult.InnerXml

                    Case "Original_Title"                       : Original_Title            = thisresult.InnerXml
                    Case "UseMultipleThreads"                   : UseMultipleThreads        = thisresult.InnerXml
                    Case "movie_filters"                        : movie_filters.Load(thisresult)
                    Case "CheckForNewVersion"                   : CheckForNewVersion        = thisresult.InnerXml
                    Case "MkvMergeGuiPath"                      : MkvMergeGuiPath           = thisresult.InnerXml

                    Case "prxyEnabled"                          : prxyEnabled               = thisresult.InnerXml
                    Case "prxyIp"                               : prxyIp                    = thisresult.InnerText
                    Case "prxyPort"                             : prxyPort                  = thisresult.InnerText
                    Case "prxyUsername"                         : prxyUsername              = thisresult.InnerText
                    Case "prxyPassword"                         : prxyPassword              = thisresult.InnerText

                        'Link properties
                    Case "XBMC_Active" : XBMC_Active = thisresult.InnerXml
                    Case "XBMC_Link"                            : XBMC_Link                 = thisresult.InnerXml 
                    Case "XBMC_Address"                         : XBMC_Address              = thisresult.InnerXml 
                    Case "XBMC_Port"                            : XBMC_Port                 = thisresult.InnerXml 
                    Case "XBMC_Username"                        : XBMC_Username             = thisresult.InnerXml 
                    Case "XBMC_Password"                        : XBMC_Password             = thisresult.InnerXml 
                    Case "XBMC_UserdataFolder"                  : XBMC_UserdataFolder	    = thisresult.InnerXml 
                    Case "XBMC_TexturesDb"                      : XBMC_TexturesDb           = thisresult.InnerXml 
                    Case "XBMC_ThumbnailsFolder"                : XBMC_ThumbnailsFolder     = thisresult.InnerXml 
                    Case "XBMC_Delete_Cached_Images"            : XBMC_Delete_Cached_Images = thisresult.InnerXml 
                    Case "XBMC_MC_MovieFolderMappings"          : XBMC_MC_MovieFolderMappings.Load(thisresult)
                    Case "XBMC_MC_CompareFields"                : XBMC_MC_CompareFields      .Load(thisresult)

                    Case "ShowExtraMovieFilters"                : ShowExtraMovieFilters = thisresult.InnerXml 
                    Case "ShowAllAudioTracks"                   : ShowAllAudioTracks    = thisresult.InnerXml 

                    Case Else : Dim x = thisresult
                End Select
            End If
        Next
        'If MovSepLst.Count = 0 Then Call ResetMovSepLst() 
        If Not MovSepLst.Contains("3DTAB") Then MovSepLst.Insert(0,"3DTAB")
        If Not MovSepLst.Contains("3DSBS") Then MovSepLst.Insert(0,"3DSBS")
        If maxmoviegenre > 99 Then maxmoviegenre = 99     'Fix original setting of maxmoviegenre All Available was 9999
        Proxyreload()
        XBMC_MC_MovieFolderMappings.IniFolders
    End Sub


    Public Shared Function decxmlchars(ByVal line As String) As String
        line = line.Replace("&amp;", "&")
        line = line.Replace("&lt;", "<")
        line = line.Replace("&gt;", ">")
        line = line.Replace("&quot;", "Chr(34)")
        line = line.Replace("&apos;", "'")
        line = line.Replace("&#xA;", vbCrLf)
        Return line
    End Function

    Public Shared Function RemoveIgnoredArticles(ByVal s As String) As String
        If String.IsNullOrEmpty(s) Then Return s
        If ignorearticle AndAlso s.ToLower.IndexOf("the ") = 0 Then 
            s = s.Substring(4, s.Length - 4) & ", The"
        End If
        If ignoreAn AndAlso s.ToLower.IndexOf("an ") = 0 Then
            s = s.Substring(3, s.Length - 3) & ", An"
        End If
        If ignoreAarticle AndAlso s.ToLower.IndexOf("a ") = 0 Then
            s = s.Substring(2, s.Length - 2) & ", A"
        End If
        Return s
    End Function

    Public Shared Function TrailerExists(NfoPathPrefName As String) As Boolean

        Return IO.File.Exists(ActualTrailerPath(NfoPathPrefName))
    End Function


    Public Shared Function FanartExists(NfoPathPrefName As String) As Boolean
        If Preferences.FrodoEnabled AndAlso IO.Path.GetFileName(NfoPathPrefName).ToLower = "video_ts.nfo" Then
            NfoPathPrefName = Utilities.RootVideoTsFolder(NfoPathPrefName)
            Return IO.File.Exists(NfoPathPrefName + "fanart.jpg")
        End If
        Return IO.File.Exists(Preferences.GetFanartPath(NfoPathPrefName))
    End Function


    Public Shared Function PosterExists(NfoPathPrefName As String) As Boolean
        If Preferences.FrodoEnabled AndAlso IO.Path.GetFileName(NfoPathPrefName).ToLower = "video_ts.nfo" Then
            NfoPathPrefName = Utilities.RootVideoTsFolder(NfoPathPrefName)
            Return IO.File.Exists(NfoPathPrefName + "poster.jpg")
        End If
        If Not Preferences.EdenEnabled AndAlso Preferences.posterjpg AndAlso Not GetRootFolderCheck(NfoPathPrefName) Then
            Return IO.File.Exists(IO.Path.GetDirectoryName(NfoPathPrefName) & "\poster.jpg")
        End If

        Return IO.File.Exists(Preferences.GetPosterPath(NfoPathPrefName))
    End Function


    Public Shared Function GetMissingData(NfoPathPrefName As String) As Byte

        Dim MissingData As Byte = 0
        
        If Not Preferences.FanartExists (NfoPathPrefName) Then MissingData += 1
        If Not Preferences.PosterExists (NfoPathPrefName) Then MissingData += 2
        If Not Preferences.TrailerExists(NfoPathPrefName) Then MissingData += 4

        Return MissingData
    End Function


    Public Shared Function ActualTrailerPath(NfoPathPrefName As String) As String

        Dim s = NfoPathPrefName
        Dim FileName As String = ""

        For Each item In "mp4,flv,webm,mov,m4v".Split(",")
            FileName = IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), Path.GetFileNameWithoutExtension(s) & "-trailer." & item)

            If IO.File.Exists(FileName) Then Return FileName
        Next

        Return IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), Path.GetFileNameWithoutExtension(s) & "-trailer.flv")

    End Function


    Public Shared Function GetActorPath(ByVal FullPath As String, ByVal ActorName As String, ByVal actorid As String) As String
        If String.IsNullOrEmpty(FullPath) or String.IsNullOrEmpty(ActorName) Then Return ""
        Dim Path As String = FullPath.Replace(IO.Path.GetFileName(FullPath), "") & ".actors\" & ActorName.Replace(" ", "_")
        Dim Path2 As String = ""
        If Preferences.actorsave AndAlso actorid <> "" Then
            If Preferences.actorsavealpha Then
                'Dim actorfilename = ActorName.Replace(" ", "_") & "_" & actorid
                Path2 = Preferences.actorsavepath & "\" & ActorName.Substring(0,1) & "\" & ActorName.Replace(" ", "_") & "_" & actorid
            Else
                Path2 = Preferences.actorsavepath & "\" & actorid.Substring(actorid.Length - 2, 2) & "\" & actorid
            End If
            If IO.File.Exists(Path2 & ".jpg") Then Return Path2 & ".jpg"
            If IO.File.Exists(Path2 & ".tbn") Then Return Path2 & ".tbn"
        End If
        If Preferences.FrodoEnabled And IO.File.Exists(Path & ".jpg") Then Return Path & ".jpg"  
        If Preferences.EdenEnabled  And IO.File.Exists(Path & ".tbn") Then Return Path & ".tbn"  
          
        If IO.File.Exists(Path & ".jpg") Then Return Path & ".jpg"  
        
        Return Path & ".tbn"  
    End Function


    Public Shared Function GetPosterPath(ByVal FullPath As String, Optional ByVal MovFilePath As String = Nothing) As String
        Dim posterpath As String = FullPath
        If Not String.IsNullOrEmpty(MovFilePath) AndAlso (Preferences.FrodoEnabled AndAlso MovFilePath.ToLower.Contains("video_ts.nfo") OrElse MovFilePath.ToLower.Contains("index.nfo")) Then
            Dim dvdbdpath As String = Utilities.RootVideoTsFolder(FullPath)
            If IO.File.Exists(dvdbdpath & "poster.jpg") Then Return dvdbdpath & "poster.jpg"
        End If
        If Not Utilities.findFileOfType(posterpath, "-poster.jpg", Preferences.basicsavemode) Then
            'Check Frodo naming convention
            If Not Utilities.findFileOfType(posterpath, ".tbn", Preferences.basicsavemode) Then
                If IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\folder.jpg") Then
                    posterpath = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                ElseIf IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\poster.jpg") Then
                    posterpath = IO.Path.GetDirectoryName(FullPath) & "\poster.jpg"
                Else
                    Dim postertype As String = ".tbn"
                    If Preferences.FrodoEnabled Then postertype = "-poster.jpg"
                    posterpath = FullPath.Replace(IO.Path.GetExtension(FullPath), postertype)
                End If
            End If
        End If
        Try
            If Preferences.posterjpg AndAlso Not IsNothing(MovFilePath) AndAlso Not GetRootFolderCheck(FullPath) AndAlso MovFilePath.ToLower <> "video_ts.nfo" Then
                Dim ispath As Boolean = IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\poster.jpg")
                If ispath Then posterpath = IO.Path.GetDirectoryName(FullPath) & "\poster.jpg"
                'posterpath = FullPath.Replace(MovFilePath,"") & "poster.jpg"
            End If
        Catch
        End Try
        Return posterpath
    End Function


    Public Shared Function FrodoPosterExists(ByVal nfoFile As String) As Boolean

        If IO.File.Exists(nfoFile.Replace(IO.Path.GetExtension(nfoFile), "-poster.jpg")) Then Return True

        Dim dir As String = IO.Path.GetDirectoryName(nfoFile)

        If IO.Path.GetFileName(nfoFile).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(nfoFile).ToLower = "index.nfo" Then
            dir = Utilities.RootVideoTsFolder(nfoFile)
            dir = dir.Substring(0,dir.Length-1)
        End If
        If IO.File.Exists(dir & "\poster.jpg") Then Return True
        If IO.File.Exists(dir & "\folder.jpg") Then Return True
        

        Return False
    End Function

    Public Shared Function FrodoPosterPath(ByVal nfoFile As String) As String
        If IO.File.Exists(nfoFile.Replace(IO.Path.GetExtension(nfoFile), "-poster.jpg")) Then Return nfoFile.Replace(IO.Path.GetExtension(nfoFile), "-poster.jpg")

        Dim dir As String = IO.Path.GetDirectoryName(nfoFile)

        If IO.Path.GetFileName(nfoFile).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(nfoFile).ToLower = "index.nfo" Then
            dir = Utilities.RootVideoTsFolder(nfoFile)
            dir = dir.Substring(0,dir.Length-1)
        End If
        If IO.File.Exists(dir & "\poster.jpg") Then Return dir & "\poster.jpg"
        If IO.File.Exists(dir & "\folder.jpg") Then Return dir & "\folder.jpg"
        Return ""
    End Function

    Public Shared Function FrodoFanartPath(ByVal nfoFile As String) As String
        If IO.File.Exists(nfoFile.Replace(IO.Path.GetExtension(nfoFile), "-fanart.jpg")) Then Return nfoFile.Replace(IO.Path.GetExtension(nfoFile), "-fanart.jpg")

        Dim dir As String = IO.Path.GetDirectoryName(nfoFile)

        If IO.Path.GetFileName(nfoFile).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(nfoFile).ToLower = "index.nfo" Then
            dir = Utilities.RootVideoTsFolder(nfoFile)
            dir = dir.Substring(0,dir.Length-1)
        End If
        If IO.File.Exists(dir & "\fanart.jpg") Then Return dir & "\fanart.jpg"
        'If IO.File.Exists(dir & "\folder.jpg") Then Return dir & "\folder.jpg"
        Return ""
    End Function


    Public Shared Function PreFrodoPosterExists(ByVal nfoFile As String) As Boolean

        Return IO.File.Exists(nfoFile.Replace(IO.Path.GetExtension(nfoFile), ".tbn"))

    End Function


    Public Shared Function GetAllPosters(ByVal nfoFile As String) As List(Of String)

        Dim Results As List(Of String) = New List(Of String)

        Dim dir As String = IO.Path.GetDirectoryName(nfoFile)

        AddIfExistsAndNew( Results, dir & "\folder.jpg")
        AddIfExistsAndNew( Results, dir & "\poster.jpg")
        AddIfExistsAndNew( Results, nfoFile.Replace(IO.Path.GetExtension(nfoFile), "-poster.jpg") )
        AddIfExistsAndNew( Results, nfoFile.Replace(IO.Path.GetExtension(nfoFile), ".tbn"       ) )

        Return Results
    End Function

    Public Shared Sub AddIfExistsAndNew( lst As List(Of String), fName As String)

        If lst.Contains(fName) Then Return

        If Not IO.File.Exists(fName) Then Return

        lst.Add(fName)
    End Sub

    Public Shared Function GetPosterPaths(ByVal FullPath As String, Optional ByVal videots As String = "") As List(Of String)
        Dim lst = New List(Of String)
        Dim path As String = FullPath
        Dim isroot As Boolean = Preferences.GetRootFolderCheck(FullPath)
        Dim posterjpg As Boolean = Preferences.posterjpg

        If (posterjpg Or Preferences.basicsavemode) AndAlso Not isroot Then
            If videots <> "" Then
                If Preferences.EdenEnabled Then
                    path = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
                    lst.Add(path)
                End If
                If Preferences.FrodoEnabled Then
                    path = videots + "poster.jpg"
                    lst.Add(path)
                End If
            Else
                If posterjpg And Preferences.FrodoEnabled Then
                    path = IO.Path.GetDirectoryName(FullPath) & "\poster.jpg"
                    lst.Add(path)
                End If
                If Preferences.basicsavemode Then
                    'path = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                    'lst.Add(path)
                End If
                If Preferences.createfolderjpg Then
                    path = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                    lst.Add(path)
                End If
                If Preferences.EdenEnabled Or Preferences.basicsavemode Then
                    path = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
                    lst.Add(path)
                End If
            End If



        Else
            If Preferences.EdenEnabled Then
                'If Not Preferences.basicsavemode Then
                path = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
                lst.Add(path)
                'End If

                'If Not Utilities.findFileOfType(path, ".tbn") Then
                '    If IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\folder.jpg") Then
                '        path = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                '    Else
                '        path = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
                '    End If
                'End If

                'lst.Add(path)
            End If
            If Preferences.basicsavemode Or Preferences.createfolderjpg And Not isroot Then
                path = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                lst.Add(path)
            End If

            If Preferences.FrodoEnabled Then
                If videots = "" Then
                    path = FullPath.Replace(IO.Path.GetExtension(FullPath), "-poster.jpg")
                    lst.Add(path)
                Else
                    path = videots + "poster.jpg"
                    lst.Add(path)
                End If
            End If

        End If
        Return lst
    End Function

    Public Shared Function CheckmissingPoster(ByVal FullPath As String) As Boolean  'Return True if any missing Posters
        Dim videotsrootpath As String = ""
        If IO.Path.GetFileName(FullPath).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(FullPath).ToLower = "index.nfo" Then
            videotsrootpath = Utilities.RootVideoTsFolder(FullPath)
        End If
        Dim posterlist As New List(Of String)
        posterlist = GetPosterPaths(FullPath, videotsrootpath)
        For Each item In posterlist
            If Not IO.File.Exists(item) Then CheckmissingPoster = True
        Next
        Return CheckmissingPoster 
    End Function

    Public Shared Function GetFanartPaths(ByVal FullPath As String, Optional ByVal videots As String = "") As List(Of String)
        Dim lst = New List(Of String)
        Dim path As String = FullPath
        Dim isroot As Boolean = Preferences.GetRootFolderCheck(FullPath)
        Dim fanartjpg As Boolean = Preferences.fanartjpg

        If (fanartjpg Or Preferences.basicsavemode) AndAlso Not isroot Then
            If videots <> "" Then
                If Preferences.EdenEnabled Then
                    path = FullPath.Replace(".nfo", "-fanart.jpg")
                    lst.Add(path)
                End If
                If Preferences.FrodoEnabled Then
                    path = videots + "fanart.jpg"
                    lst.Add(path)
                End If
            Else
                If (fanartjpg And Preferences.FrodoEnabled) Or Preferences.basicsavemode Or Preferences.createfanartjpg Then
                    path = IO.Path.GetDirectoryName(FullPath) & "\fanart.jpg"
                    lst.Add(path)
                End If
                If Not Preferences.basicsavemode AndAlso Preferences.EdenEnabled Then
                    path = FullPath.Replace(".nfo", "-fanart.jpg")
                    lst.Add(path)
                End If
            End If

        Else
            If Preferences.EdenEnabled Then
                path = FullPath.Replace(IO.Path.GetExtension(FullPath), "-fanart.jpg")
                lst.Add(path)
            End If
            If Preferences.FrodoEnabled Then
                If Not Preferences.EdenEnabled Then
                    If Not videots = "" Then
                        path = videots + "fanart.jpg"
                        lst.Add(path)
                    Else
                        path = FullPath.Replace(IO.Path.GetExtension(FullPath), "-fanart.jpg")
                        lst.Add(path)
                    End If
                 Else
                    If Not videots = "" Then
                        path = videots + "fanart.jpg"
                        lst.Add(path)
                    End If
                End If
                'If Not videots = "" Then
                    
                'End If
            End If
            If Preferences.basicsavemode OrElse Preferences.createfanartjpg Then
                path = IO.Path.GetDirectoryName(FullPath) & "\fanart.jpg"
                lst.Add(path)
            End If
            End If

            Return lst
    End Function

    Public Shared Function CheckmissingFanart(ByVal FullPath As String) As Boolean  'Return True if any missing Fanart
        Dim videotsrootpath As String = ""
        If IO.Path.GetFileName(FullPath).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(FullPath).ToLower = "index.nfo" Then
            videotsrootpath = Utilities.RootVideoTsFolder(FullPath)
        End If
        Dim fanartlist As New List(Of String)
        fanartlist = GetFanartPaths(FullPath, videotsrootpath)
        For Each item In fanartlist
            If Not IO.File.Exists(item) Then CheckmissingFanart = True
        Next
        Return CheckmissingFanart 
    End Function

    Public Shared Function GetFanartPath(ByVal FullPath As String, Optional ByVal MovFilePath As String = "") As String
        Dim fanartPath As String = FullPath
        If Preferences.FrodoEnabled AndAlso MovFilePath.ToLower.Contains("video_ts.nfo") OrElse MovFilePath.ToLower.Contains("index.nfo") Then
            Dim dvdbdpath As String = Utilities.RootVideoTsFolder(FullPath)
            If IO.File.Exists(dvdbdpath & "fanart.jpg") Then Return dvdbdpath & "fanart.jpg"
        End If
        'If MovFilePath = "" Then MovFilePath = Nothing
        'If Not IsNothing(MovFilePath) Then
        'Dim MovPath As String = FullPath.Replace(MovFilePath,"")
        'End If
        If Not Utilities.findFileOfType(fanartPath, "-fanart.jpg", Preferences.basicsavemode, Preferences.fanartjpg, False) Then
            If Not GetRootFolderCheck(FullPath) AndAlso Preferences.fanartjpg AndAlso MovFilePath <> "" Then
                Dim MovPath As String = FullPath.Replace(MovFilePath, "") & "fanart.jpg"
                Return MovPath
            Else
                fanartPath = FullPath.Replace(IO.Path.GetExtension(FullPath), "-fanart.jpg")
            End If
            'fanartPath = FullPath.Replace(IO.Path.GetExtension(FullPath), "-fanart.jpg")
        Else
            If Not GetRootFolderCheck(FullPath) AndAlso Preferences.fanartjpg AndAlso MovFilePath <> "" AndAlso MovFilePath.ToLower <> "video_ts.nfo" Then
                Dim MovPath As String = FullPath.Replace(MovFilePath, "") & "fanart.jpg"
                If IO.File.Exists(MovPath) Then Return MovPath
                'Return MovPath
            End If
        End If
        Return fanartPath

    End Function

    Public Shared Function GetFanartTvMoviePath(ByVal FullPath As String, Optional ByVal MovFilePath As String = "") As String
        Dim fanartTvPath As String = FullPath

        Return fanartTvPath 
    End Function

    Public Shared Function GetMovBasePath(ByVal path As String) As String
        Dim basepath As String = ""
        If path.ToLower.Contains("video_ts") or path.ToLower.Contains("bdmv") Then
            If path.ToLower.Contains("video_ts") Then
                basepath = path.substring(0, path.Length-9)
            Else 
                basepath = path.substring(0, path.Length-5)
            End If
        Else
            basepath = path
        End If
        Return basepath
    End Function

    Public Shared Function GetMovSetFanartPath(ByVal MovPath As String, ByVal MovSetName As String) As String
        Dim movsetfanartpath As String = ""
        If MovSetName = "" Then Return ""
        MovSetName = Utilities.cleanFoldernameIllegalChars(MovSetName)
        Dim foldername As String = Utilities.GetLastFolder(MovPath)
        Dim filename As String = IO.Path.GetFileName(MovPath)
        If Preferences.MovSetArtSetFolder Then              'Central folder for all Movie Set Artwork
            movsetfanartpath = Preferences.MovSetArtCentralFolder & "\" & MovSetName & "-fanart.jpg"
        Else                                                'or Save to movieset folder if exists.
           ' Dim MovPath As String = workingMovieDetails.fileinfo.fullpathandfilename
            If MovPath.Contains(MovSetName) AndAlso foldername = MovSetName Then
                movsetfanartpath = MovPath.Replace(filename, "fanart.jpg")
            ElseIf MovPath.Contains(MovSetName) Then
                Dim splitpath() = MovPath.Split("\")
                For Each p In splitpath
                    movsetfanartpath &= p & "\"
                    If p = MovSetName then
                        movsetfanartpath &="fanart.jpg"
                        Exit For
                    End If
                Next
            End If
        End If
        Return movsetfanartpath 
    End Function

    Public Shared Function GetMovSetPosterPath(ByVal MovPath As String, ByVal MovSetName As String) As String
        Dim movsetposterpath As String = ""
        If MovSetName = "" Then Return ""
        MovSetName = Utilities.cleanFoldernameIllegalChars(MovSetName)
        Dim foldername As String = Utilities.GetLastFolder(MovPath)
        Dim filename As String = IO.Path.GetFileName(MovPath)
        If Preferences.MovSetArtSetFolder Then              'Central folder for all Movie Set Artwork
            movsetposterpath = Preferences.MovSetArtCentralFolder & "\" & MovSetName & "-poster.jpg"
        Else                                                'or Save to movieset folder if exists.
           ' Dim MovPath As String = workingMovieDetails.fileinfo.fullpathandfilename
            If MovPath.Contains(MovSetName) AndAlso foldername = MovSetName Then
                movsetposterpath = MovPath.Replace(filename, "poster.jpg")
            ElseIf MovPath.Contains(MovSetName) Then
                Dim splitpath() = MovPath.Split("\")
                For Each p In splitpath
                    movsetposterpath &= p & "\"
                    If p = MovSetName then
                        movsetposterpath &="poster.jpg"
                        Exit For
                    End If
                Next
            End If
        End If
        Return movsetposterpath 
    End Function

    Public Shared Function GetActorThumbPath(Optional ByVal location As String = "")
        Dim actualpath As String = ""
        Try
            If String.IsNullOrEmpty(location) Then
                Return "none"
            End If

            If location.IndexOf("http") <> -1 Then
                Return location
            Else
                If location.IndexOf(actornetworkpath) <> -1 Then
                    If Not String.IsNullOrEmpty(actornetworkpath) Or Not String.IsNullOrEmpty(actorsavepath) Then
                        Dim filename As String = IO.Path.GetFileName(location)
                        actualpath = IO.Path.Combine(actorsavepath, filename)
                        If Not IO.File.Exists(actualpath) Then
                            Dim extension As String = IO.Path.GetExtension(location)
                            Dim purename As String = IO.Path.GetFileName(location)
                            purename = purename.Replace(extension, "")
                            If actorsavealpha Then
                                actualpath = actorsavepath & "\" & purename.Substring(0,1) & "\" & filename
                            Else
                                actualpath = actorsavepath & "\" & purename.Substring(purename.Length - 2, 2) & "\" & filename
                            End If
                            
                        End If
                        If Not IO.File.Exists(actualpath) Then
                            actualpath = "none"
                        End If
                    Else
                        actualpath = "none"
                    End If
                Else
                    actualpath = "none"
                End If
            End If
            If String.IsNullOrEmpty(actualpath) Then actualpath = "none"
            Return actualpath
        Catch
            Return "none"
        Finally

        End Try
    End Function

    Public Shared Function GetRootFolderCheck(ByVal fullpath) As Boolean
        Dim isroot As Boolean = False
        If Preferences.movrootfoldercheck Then
            Dim lastfolder As String = Utilities.GetLastFolder(fullpath)
            Dim rtfolder As String = Nothing
            For Each rfolder In Preferences.movieFolders
                rtfolder = Path.GetFileName(rfolder.rpath)
                If rtfolder = lastfolder Then isroot = True
            Next
        End If
        Return isroot
    End Function

    Public Shared Function Get_HdTags(ByVal filename As String) As FullFileDetails
        Try
            If IO.Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
                Dim temppath As String = Utilities.GetDvdLargestVobSet(filename)
                'Dim temppath As String = filename.Replace(IO.Path.GetFileName(filename), "VTS_01_0.IFO")
                If IO.File.Exists(temppath) Then
                    filename = temppath
                End If
            End If
            Dim tmpaud As String = ""
            Dim possibleISO As String = String.Empty 
            Dim workingfiledetails As New FullFileDetails
            If IO.Path.GetExtension(filename).ToLower = ".iso" Then
                possibleISO = Get_ISO_HDTags(filename)
                If possibleISO <> "" AndAlso Not possibleISO.ToLower.Contains("unable to get image file") Then
                    Dim MInform As New XmlDocument
                    MInform.LoadXml(possibleISO)
                    For Each thisresult In MInform("File")
                        Select Case thisresult.name
                            Case "track"
                                Dim check As String = thisresult.outerxml.ToString
                                If check.Contains("""Video""") Then
                                    For Each result In thisresult
                                        Select Case result.name
                                            Case "Format"
                                                workingfiledetails.filedetails_video.Codec.Value = result.InnerText
                                            Case "Format_version"
                                                workingfiledetails.filedetails_video.FormatInfo.Value = result.InnerText
                                            Case "Width"
                                                workingfiledetails.filedetails_video.Width.Value = result.InnerText
                                            Case "Height"
                                                workingfiledetails.filedetails_video.Height.Value = result.InnerText
                                            Case "Bit_rate_mode"
                                                workingfiledetails.filedetails_video.BitrateMode.Value = result.InnerText
                                            Case "Maximum_bit_rate"
                                                workingfiledetails.filedetails_video.BitrateMax.Value = result.InnerText
                                            Case "Display_aspect_ratio"
                                                Dim Asp As String = result.InnerText
                                                If Not Asp = "" Then
                                                    If Asp = "16:9" Then Asp = "1.56:1"
                                                    workingfiledetails.filedetails_video.Aspect.Value = Asp.Substring(0, Asp.IndexOf(":"))
                                                End If
                                            Case "Scan_type"
                                                workingfiledetails.filedetails_video.ScanType.Value = result.InnerText
                                        End Select
                                    Next
                                    If workingfiledetails.filedetails_video.Codec.Value.ToLower = "mpeg video" AndAlso workingfiledetails.filedetails_video.FormatInfo.Value.Contains("2") Then
                                        workingfiledetails.filedetails_video.Codec.Value = "MPEG2VIDEO"
                                    End If
                                    workingfiledetails.filedetails_video.Width.Value = workingfiledetails.filedetails_video.Width.Value.Replace(" pixels", "")
                                    workingfiledetails.filedetails_video.Height.Value = workingfiledetails.filedetails_video.Height.Value.Replace(" pixels", "")
                                    workingfiledetails.filedetails_video.Container.Value = IO.Path.GetExtension(filename).ToLower
                                    workingfiledetails.filedetails_video.DurationInSeconds.Value = -1  'unable to get duration from ISO
                                End If
                                
                                If check.Contains("""Audio""") Then
                                    tmpaud = ""
                                    Dim audio As New AudioDetails
                                    For Each result In thisresult
                                        Select Case result.name
                                            Case "Format"
                                                audio.Codec.Value = result.InnerText
                                            Case "Format_Info"
                                                tmpaud = result.InnerText
                                                tmpaud = tmpaud.ToLower
                                            Case "Channel_s_"
                                                audio.Channels.Value = result.InnerText
                                            Case "Bit_rate"
                                                audio.Bitrate.Value = result.InnerText
                                            Case "Language"
                                                audio.Language.Value = result.InnerText
                                            Case "Default"
                                                audio.DefaultTrack.Value = result.InnerText
                                        End Select
                                    Next
                                    If audio.Codec.Value = "TrueHD / AC-3" Then audio.Codec.Value = "truehd"
                                    If audio.Codec.Value = "DTS" Then
                                        If tmpaud.ToLower = "dts ma / core" Then
                                            audio.Codec.Value = "dtshd_ma"
                                        ElseIf tmpaud.ToLower = "dts hra / core" Then
                                            audio.Codec.Value = "dtshd_hra"
                                        ElseIf tmpaud.ToLower = "dts es" Then
                                            audio.Codec.Value = "dts"
                                        Else
                                            audio.Codec.Value = "dts"
                                        End If
                                    End If
                                    If audio.Codec.Value = "AC-3" Then audio.Codec.Value = "AC3"
                                    workingfiledetails.filedetails_audio.Add(audio)
                                End If
                        End Select
                    Next
                    If workingfiledetails.filedetails_audio.Count = 0 Then
                        Dim audio As New AudioDetails
                        workingfiledetails.filedetails_audio.Add(audio)    'Must have at least one audio track, even if it's blank
                    End If
                    Return workingfiledetails
                Else
                    Return workingfiledetails 
                End If
            End If
            Dim playlist As New List(Of String)
            Dim tempstring As String
            tempstring = Utilities.GetFileName(filename)
            playlist = Utilities.GetMediaList(tempstring)
            If Not filename.ToLower.Contains("vts") AndAlso filename <> tempstring Then
                filename = tempstring 
            End If

            If Not IO.File.Exists(filename) Then
                Return Nothing
            End If
            Dim MI As New mediainfo
            MI.Open(filename)
            Dim curVS As Integer = 0
            Dim addVS As Boolean = False
            Dim numOfVideoStreams As Integer = MI.Count_Get(StreamKind.Visual)
            Dim aviFile As MediaFile = New MediaFile(filename)
            Dim tmpstr As String = ""

            Dim tempmediainfo As String

            workingfiledetails.filedetails_video.Width.Value = If(aviFile.Video.Count = 0, "", aviFile.Video(0).Width)  'tempmediainfo
            workingfiledetails.filedetails_video.Height.Value = If(aviFile.Video.Count = 0, "", aviFile.Video(0).Height)  'tempmediainfo

            Try
                Dim tmp As Double = If(aviFile.Video.Count = 0, 0, aviFile.Video(0).AspectRatio)
                If tmp <> 0 AndAlso tmp < 4 Then
                    workingfiledetails.filedetails_video.Aspect.Value = tmp.ToString("F2")
                Else
                    Dim DisplayAspectRatio As String = MI.Get_(StreamKind.Visual, curVS, "AspectRatio")
                    If Not DisplayAspectRatio = "" Then
                        workingfiledetails.filedetails_video.Aspect.Value = Convert.ToDouble(DisplayAspectRatio).ToString("F2")
                    End If
                End If
            Catch ex As Exception
                Try
                    If workingfiledetails.filedetails_video.Width.Value <> "" AndAlso workingfiledetails.filedetails_video.Height.Value <> "" Then
                        Dim Aspect As Double = 0
                        Dim wi As Double = Convert.ToDouble(aviFile.Video(0).Width)
                        Dim he As Double = Convert.ToDouble(aviFile.Video(0).Height)
                        Aspect = wi/he
                        workingfiledetails.filedetails_video.Aspect.Value = Aspect.ToString("F2")
                    Else
                        workingfiledetails.filedetails_video.Aspect.Value = "Unknown"
                    End If
                Catch exc As Exception
                    workingfiledetails.filedetails_video.Aspect.Value = "Unknown"
                End Try
            End Try

            'Try
            tempmediainfo = If(aviFile.Video.Count = 0, "", aviFile.Video(0).Format)
                If tempmediainfo.ToLower = "mpeg video" Then
                    Dim Temp1 As String = If(aviFile.Video.Count = 0, 0, aviFile.Video(0).FormatID)
                    If Temp1 <> "" Then tempmediainfo = Temp1.ToLower
                End If

                If tempmediainfo.EndsWith("avc", StringComparison.CurrentCultureIgnoreCase) Then
                    tempmediainfo = "h264"
                ElseIf tempmediainfo = "DX50" Then
                    tempmediainfo = "divx"
                End If
            workingfiledetails.filedetails_video.Codec.Value = tempmediainfo

            tempmediainfo = If(aviFile.Video.Count = 0, "", aviFile.Video(0).CodecID)
                If tempmediainfo.ToLower = "xvid" OrElse tempmediainfo.ToLower.Contains("div") OrElse tempmediainfo.ToLower = "dx50" Then
                    workingfiledetails.filedetails_video.FormatInfo.Value = workingfiledetails.filedetails_video.Codec.Value
                    workingfiledetails.filedetails_video.Codec.Value = tempmediainfo.ToLower
                Else 
                    If tempmediainfo.ToLower.Contains("mp4v") OrElse tempmediainfo.ToLower.Contains("20") Then
                        workingfiledetails.filedetails_video.FormatInfo.Value = "mp4v" 
                    Else
                        workingfiledetails.filedetails_video.FormatInfo.Value = tempmediainfo 
                    End If
                    
                End If
            
            'Dim fs(100) As String
            'For f = 1 To 100
            '    fs(f) = MI.Get_(StreamKind.Visual, 0, f)
            'Next

            Try
                If playlist.Count = 1 Then
                    Dim duration As String = MI.Get_(StreamKind.Visual, 0, "Duration")
                    If Not String.IsNullOrEmpty(duration) Then
                        workingfiledetails.filedetails_video.DurationInSeconds.Value = Math.Round(Convert.ToInt32(duration) / 1000)
                    Else
                        workingfiledetails.filedetails_video.DurationInSeconds.Value = -1
                    End If
                ElseIf playlist.Count > 1 Then
                    Dim total As Integer = 0
                    For f = 0 To playlist.Count - 1

                        Dim M2 As mediainfo = New mediainfo

                        M2.Open(playlist(f))
                        Dim duration As String = M2.Get_(StreamKind.Visual, 0, "Duration")
                        If Not String.IsNullOrEmpty(duration) Then
                            total += Math.Round(Convert.ToInt32(duration) / 1000)
                        End If
                    Next

                    If total = 0 Then total = -1
                    workingfiledetails.filedetails_video.DurationInSeconds.Value = total
                End If
            Catch
                workingfiledetails.filedetails_video.DurationInSeconds.Value = -1
            End Try
            workingfiledetails.filedetails_video.Bitrate.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate/String")
            workingfiledetails.filedetails_video.BitrateMode.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Mode/String")
            workingfiledetails.filedetails_video.BitrateMax.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Maximum/String")

            workingfiledetails.filedetails_video.Container.Value = IO.Path.GetExtension(filename) '"This is the extension of the file"

            workingfiledetails.filedetails_video.ScanType.Value = MI.Get_(StreamKind.Visual, curVS, "ScanType")    'MI.Get_(StreamKind.Visual, curVS, 102) 
            'Video()
            'Format                     : MPEG-4 Visual
            'Format profile             : Streaming Video@L1
            'Format(settings, BVOP)     : Yes()
            'Format(settings, QPel)     : No()
            'Format(settings, GMC)      : No(warppoints)
            'Format(settings, Matrix)   : Custom()
            'Codec(ID)                  : XVID()
            'Codec(ID / Hint)           : XviD()
            'Duration                   : 1h 33mn
            'Bit rate                   : 903 Kbps
            'Width                      : 528 pixels
            'Height                     : 272 pixels
            'Display aspect ratio       : 1.941
            'Frame rate                 : 25.000 fps
            'Resolution                 : 24 bits
            'Colorimetry                : 4:2:0
            'Scan(Type)                 : Progressive()
            'Bits/(Pixel*Frame)         : 0.252
            'Stream size                : 604 MiB (86%)
            'Writing library            : XviD 1.0.3 (UTC 2004-12-20)

            Dim numOfAudioStreams As Integer = MI.Count_Get(StreamKind.Audio)
            Dim curAS As Integer = 0
            Dim addAS As Boolean = False
            tmpaud = ""
            

            'get audio data
            If numOfAudioStreams > 0 Then
                While curAS < numOfAudioStreams
                    Dim audio As New AudioDetails
                    audio.Language.Value = Utilities.GetLangCode(MI.Get_(StreamKind.Audio, curAS, "Language/String"))
                    If MI.Get_(StreamKind.Audio, curAS, "Format") = "MPEG Audio" Then
                        audio.Codec.Value = "MP3"
                    Else
                        'audio.Codec.Value = MI.Get_(StreamKind.Audio, curAS, "Format")
                        Try
                            tempmediainfo = aviFile.Audio(curAS).Format
                        Catch
                            tempmediainfo = ""
                        End Try
                        audio.Codec.Value = tempmediainfo
                    End If
                    If audio.Codec.Value = "AC-3" Then audio.Codec.Value = "ac3"
                    If audio.Codec.Value = "TrueHD / AC-3" Then audio.Codec.Value = "truehd"
                    tmpaud = aviFile.Audio(curAS).FormatID.ToLower()
                    If audio.Codec.Value = "DTS" Then
                        If tmpaud.ToLower = "dts ma / core" Then
                            audio.Codec.Value = "dtshd_ma"
                        ElseIf tmpaud.ToLower = "dts hra / core" Then
                            audio.Codec.Value = "dtshd_hra"
                        ElseIf tmpaud.ToLower = "dts es" Then
                            audio.Codec.Value = "dts"
                        End If
                        audio.Codec.Value = audio.Codec.Value.ToLower
                    End If
                    audio.Channels.Value = MI.Get_(StreamKind.Audio, curAS, "Channel(s)")
                    audio.Bitrate.Value = MI.Get_(StreamKind.Audio, curAS, "BitRate/String")
                    If audio.Bitrate.Value = "" Then
                        Dim tmpaud1 As String = ""
                        tmpaud1 = MI.Get_ (StreamKind.Audio, curAS, "BitRate_Maximum/String")
                        If tmpaud1 <> "" Then audio.Bitrate.Value = tmpaud1
                    End If

                    audio.DefaultTrack.Value = MI.Get_(StreamKind.Audio, curAS, "Default")

                    workingfiledetails.filedetails_audio.Add(audio)
                    curAS += 1
                End While
            Else
                Dim audio As New AudioDetails
                'audio.Codec.Value = ""
                'audio.Channels.Value = ""
                'audio.Bitrate.Value = ""
                workingfiledetails.filedetails_audio.Add(audio)
            End If


            Dim numOfSubtitleStreams As Integer = MI.Count_Get(StreamKind.Text)
            Dim curSS As Integer = 0
            If numOfSubtitleStreams > 0 Then
                While curSS < numOfSubtitleStreams
                    Dim sublanguage As New SubtitleDetails
                    sublanguage.Language.Value = Utilities.GetLangCode(MI.Get_(StreamKind.Text, curSS, "Language/String"))
                    workingfiledetails.filedetails_subtitles.Add(sublanguage)
                    curSS += 1
                End While
            End If

            Return workingfiledetails
         Catch ex As Exception

        Finally
        End Try
        Return Nothing
    End Function

    Public Shared Function Get_ISO_HDTags(ByVal filename As String) As String
        Dim something As String = ""
        Dim tempstring As String = String.Empty 
        If applicationPath.IndexOf("/") <> -1 Then tempstring = applicationPath & "/" & "mediainfo-rar.exe"
        If applicationPath.IndexOf("\") <> -1 Then tempstring = applicationPath & "\" & "mediainfo-rar.exe"
        If Not IO.File.Exists(tempstring) Then Return ""
        Try
        Dim NewProcess As New System.Diagnostics.Process()

        With NewProcess.StartInfo
            .FileName = tempstring
            .Arguments = " --Output=XML """ & filename
            .RedirectStandardOutput = True
            .RedirectStandardError = True
            .RedirectStandardInput = True
            .UseShellExecute = False
                .WindowStyle = ProcessWindowStyle.Hidden
                .CreateNoWindow = True
        End With
        Dim To_Display As String = ""
        NewProcess.Start()
        something = NewProcess.StandardOutput.ReadToEnd
        Catch ex As Exception 
        End Try

        Return something
    End Function
    Shared Sub OpenFileInAppPath(file As String)
        OpenFile( Path.Combine(AppPath,file) )
    End Sub


    Shared Sub OpenFile(file As String)
        Try
            System.Diagnostics.Process.Start(file)
        Catch ex As Exception
            MsgBox("Failed to open file [" & file & "] Error message [" & ex.Message & "]")
        End Try
    End Sub

    Public Shared Function stubofflinefolder(ByVal isfolderinlist As String) As Boolean
        Dim match As Boolean = False
        Try
            For Each folder In movieFolders
                If isfolderinlist.ToLower = folder.rpath.ToLower Then
                    match = True
                    Return match
                End If
            Next
            Dim t As New str_RootPaths 
            t.rpath = isfolderinlist
            movieFolders.Add(t)
            Return True
        Catch ex As Exception
            MsgBox("Problem adding [" & isfolderinlist &"] to MovieFolder List. Error Message [" & ex.Message & "]")
            match = False
        End Try
        Return match
    End Function
End Class

