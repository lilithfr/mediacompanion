Imports System.IO
Imports System.Xml
Imports System.Threading


Module Ext
    <System.Runtime.CompilerServices.Extension()> _
    Public Sub AppendChild(root As XmlElement, doc As XmlDocument, name As String, value As String)

        Dim child As XmlElement = doc.CreateElement(name)

        child.InnerText = value
        root.AppendChild(child)
    End Sub
End Module


Public Class Preferences
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
    Public Shared applicationDatapath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Media Companion\"

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
    Public Shared tvFolders As New List(Of String)
    Public Shared tvRootFolders As New List(Of String)
    Public Shared movieFolders As New List(Of String)
    Public Shared offlinefolders As New List(Of String)
    Public Shared homemoviefolders As New List(Of String)

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
    Public Shared maximised As Boolean
    Public Shared startuptab As Byte
    Public Shared logview As Integer
    Public Shared LogScrapeTimes As Boolean = False
    Public Shared ScrapeTimingsLogThreshold As Integer = 100
    Public Shared lastpath As String
    Public Shared maximumthumbs As Integer

    'Saved General Prefs
    Public Shared startupCache As Boolean
    Public Shared renamenfofiles As Boolean
    Public Shared actorseasy As Boolean
    Public Shared overwritethumbs As Boolean
    Public Shared videomode As Integer
    Public Shared selectedvideoplayer As String
    Public Shared externalbrowser As Boolean
    Public Shared selectedBrowser As String
    Public Shared ignorearticle As Boolean
    Public Shared intruntime As Boolean
    Public Shared XBMC_version As Byte
    Public Shared ShowMovieGridToolTip As Boolean = False
    Public Shared ShowLogOnError As Boolean = True
    Public Shared font As String

    'Saved Movie Prefs
    Public Shared DownloadTrailerDuringScrape As Boolean
    Public Shared gettrailer As Boolean
    Public Shared ignoretrailers As Boolean
    Public Shared moviePreferredTrailerResolution As String
    Public Shared moviescraper As Integer
    Public Shared nfoposterscraper As Integer
    Public Shared ignoreactorthumbs As Boolean
    Public Shared maxactors As Integer
    Public Shared maxmoviegenre As Integer
    Public Shared enablehdtags As Boolean
    Public Shared movieRuntimeDisplay As String
    Public Shared movieRuntimeFallbackToFile As Boolean = False
    Public Shared disablelogfiles As Boolean
    Public Shared fanartnotstacked As Boolean
    Public Shared posternotstacked As Boolean
    Public Shared scrapemovieposters As Boolean
    Public Shared usefanart As Boolean
    Public Shared dontdisplayposter As Boolean
    Public Shared usefoldernames As Boolean
    Public Shared allfolders As Boolean
    Public Shared actorsave As Boolean
    Public Shared actorsavepath As String
    Public Shared actornetworkpath As String
    Public Shared imdbmirror As String
    Public Shared createfolderjpg As Boolean
    Public Shared basicsavemode As Boolean
    Public Shared namemode As String
    Public Shared usetransparency As Boolean
    Public Shared transparencyvalue As Integer
    Public Shared keepfoldername As Boolean
    Public Shared savefanart As Boolean
    Public Shared roundminutes As Boolean
    Public Shared moviedefaultlist As Byte
    Public Shared moviesUseXBMCScraper As Boolean = False
    Public Shared movies_useXBMC_Scraper As Boolean
    Public Shared whatXBMCScraper As String = "tmdb"
    Public Shared whatXBMCScraperIMBD As Boolean
    Public Shared whatXBMCScraperTVDB As Boolean
    Public Shared XBMC_Scraper As String = "tmdb"   'Locked TMDb as XBMC Scraper.
    Public Shared scrapefullcert As Boolean
    Public Shared OfflineDVDTitle As String
    Public Shared MovieManualRename As Boolean
    Public Shared MovieRenameEnable As Boolean
    Public Shared MovieRenameTemplate As String
    Public Shared MovieImdbGenreRegEx As String
    Public Shared showsortdate As Boolean
    Public Shared TMDbSelectedLanguageName As String = "English - US"
    Public Shared TMDbUseCustomLanguage As Boolean = False
    Public Shared TMDbCustomLanguageValue As String = ""
    Public Shared GetMovieSetFromTMDb As Boolean = True
    Public Shared ActorResolutionSI As Integer = 2     ' Height  768           SI = Selected Index
    Public Shared PosterResolutionSI As Integer = 9     ' Height  1080  
    Public Shared BackDropResolutionSI As Integer = 15     ' Full HD 1920x1080

    Public Shared ActorsFilterMinFilms      As Integer =   1
    Public Shared MaxActorsInFilter         As Integer = 500
    Public Shared MovieFilters_Actors_Order As Integer =   0        ' 0=Number of films desc 1=A-Z

    Public Shared SetsFilterMinFilms        As Integer =   1             
    Public Shared MaxSetsInFilter           As Integer = 500
    Public Shared MovieFilters_Sets_Order   As Integer =   0        ' 0=Number of films desc 1=A-Z


    Public Shared DateFormat As String = "YYYY-MM-DD"   'Valid tokens: YYYY MM DD HH MIN SS Used in Movie list
    Public Shared MovieList_ShowColPlot As Boolean = False
    Public Shared MovieList_ShowColWatched As Boolean = False
    Public Shared MovieScraper_MaxStudios As Integer = 9     ' 9 = Max
    Public Shared moviesortorder As Integer
    Public Shared movieinvertorder As Boolean
    Public Shared moviesets As New List(Of String)
    Public Shared moviethumbpriority() As String
    Public Shared certificatepriority() As String
    Public Shared releaseformat() As String
    Public Shared tableview As New List(Of String)
    Public Shared tablesortorder As String
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

    'Saved TV Prefs
    Public Shared tvshowautoquick As Boolean
    Public Shared copytvactorthumbs As Boolean = False
    Public Shared displayMissingEpisodes As Boolean = False
    Public Shared sortorder As String
    Public Shared tvposter As Boolean
    Public Shared tvfanart As Boolean
    Public Shared downloadtvseasonthumbs As Boolean
    Public Shared enabletvhdtags As Boolean
    Public Shared disabletvlogs As Boolean
    Public Shared postertype As String
    Public Shared TvdbActorScrape As Integer
    Public Shared seasonall As String
    Public Shared tvrename As Integer
    Public Shared eprenamelowercase As Boolean
    Public Shared tvshowrefreshlog As Boolean
    Public Shared autoepisodescreenshot As Boolean
    Public Shared tvshow_useXBMC_Scraper As Boolean
    Public Shared autorenameepisodes As Boolean
    Public Shared TvdbLanguage As String = "English"
    Public Shared TvdbLanguageCode As String = "en"

    '(Unsure)
    Public Shared maximagecount As Integer
    Public Shared episodeacrorsource As String
    Public Shared alwaysuseimdbid As Boolean


    Public Shared Sub SetUpPreferences()
        'General
        ignorearticle = False
        externalbrowser = False
        selectedBrowser = ""
        backgroundcolour = "Silver"
        forgroundcolour = "#D3D9DC"
        formheight = "600"
        formwidth = "800"
        disablelogfiles = False
        startupCache = True
        rarsize = 8
        renamenfofiles = True
        scrapemovieposters = True
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
        showsortdate = False
        XBMC_version = 0

        'Movies
        movies_useXBMC_Scraper = False
        XBMC_Scraper = "tmdb"
        moviedefaultlist = 0
        moviesortorder = 0
        '      movieinvertorder = 0
        imdbmirror = "http://www.imdb.com/"
        usefoldernames = False
        allfolders = False
        ReDim moviethumbpriority(3)
        maxmoviegenre = 99
        moviethumbpriority(0) = "themoviedb.org"
        moviethumbpriority(1) = "IMDB"
        moviethumbpriority(2) = "Movie Poster DB"
        moviethumbpriority(3) = "Internet Movie Poster Awards"
        movieRuntimeDisplay = "scraper"
        moviePreferredTrailerResolution = "720"
        MovieManualRename = False
        MovieRenameEnable = False
        MovieRenameTemplate = "%T (%Y)"
        MovieImdbGenreRegEx = "/genre/.*?>(?<genre>.*?)</a>"


        'TV
        tvshow_useXBMC_Scraper = False
        autorenameepisodes = False
        autoepisodescreenshot = False
        tvshowautoquick = False
        copytvactorthumbs = True
        enabletvhdtags = True
        tvshowrefreshlog = False
        seasonall = "none"
        tvrename = 0
        tvfanart = True
        tvposter = True
        postertype = "poster"
        downloadtvseasonthumbs = True
        TvdbLanguage = "English"
        TvdbLanguageCode = "en"
        sortorder = "default"
        TvdbActorScrape = 0
        OfflineDVDTitle = "Please Load '%T' Media To Play..."
        fixnfoid = False
        logview = "0"  'first entry in combobox is 'Full' (log view)
        displayMissingEpisodes = False

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
        actornetworkpath = ""
        usefanart = True
        ignoretrailers = False
        keepfoldername = False
        enablehdtags = True
        savefanart = True
        whatXBMCScraper = "tmdb"
        '    resizefanart = 1
        overwritethumbs = False
        maxactors = 9999
        createfolderjpg = False
        basicsavemode = False               'movie.nfo, movie.tbn, fanart.jpg
        namemode = "1"
        maximumthumbs = 10
        gettrailer = False
        DownloadTrailerDuringScrape = False
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
        ReDim releaseformat(12)
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

        movieFolders.Clear()
        tvFolders.Clear()

    End Sub


    Public Shared Sub SaveConfig()
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
            root.AppendChild(doc, "tvrootfolder", path)
        Next

        For Each path In movieFolders
            root.AppendChild(doc, "nfofolder", path)
        Next
        Dim list As New List(Of String)
        For Each path In offlinefolders
            If Not list.Contains(path) Then
                root.AppendChild(doc, "offlinefolder", path)
                list.Add(path)
            End If
        Next

        For Each Path In homemoviefolders
            root.AppendChild(doc, "homemoviefolder", Path)
            list.Add(Path)
        Next


        'Form Settings ------------------------------------------------------------
        root.AppendChild(doc, "backgroundcolour", backgroundcolour)
        root.AppendChild(doc, "forgroundcolour", forgroundcolour)
        root.AppendChild(doc, "remembersize", remembersize)
        root.AppendChild(doc, "locx", locx)
        root.AppendChild(doc, "locy", locy)
        root.AppendChild(doc, "formheight", formheight)
        root.AppendChild(doc, "formwidth", formwidth)
        root.AppendChild(doc, "splitcontainer1", splt1)
        root.AppendChild(doc, "splitcontainer2", splt2)
        root.AppendChild(doc, "splitcontainer3", splt3)
        root.AppendChild(doc, "splitcontainer4", splt4)
        root.AppendChild(doc, "splitcontainer5", splt5)
        root.AppendChild(doc, "maximised", maximised)
        root.AppendChild(doc, "startuptab", startuptab)
        root.AppendChild(doc, "logview", logview)
        root.AppendChild(doc, "LogScrapeTimes", LogScrapeTimes)
        root.AppendChild(doc, "ScrapeTimingsLogThreshold", ScrapeTimingsLogThreshold)
        root.AppendChild(doc, "maximumthumbs", maximumthumbs)
        root.AppendChild(doc, "lastpath", lastpath)
        root.AppendChild(doc, "MovieImdbGenreRegEx", MovieImdbGenreRegEx)


        'General Prefs ------------------------------------------------------------
        root.AppendChild(doc, "startupcache",           startupCache)           'chkbx_disablecache
        root.AppendChild(doc, "renamenfofiles",         renamenfofiles)         'CheckBoxRenameNFOtoINFO
        root.AppendChild(doc, "actorseasy",             actorseasy)             'CheckBox33
        root.AppendChild(doc, "rarsize",                rarsize)                'txtbx_minrarsize
        root.AppendChild(doc, "overwritethumbs",        overwritethumbs)        'cbOverwriteArtwork
        root.AppendChild(doc, "videomode",              videomode)              'RadioButton36-38
        root.AppendChild(doc, "selectedvideoplayer",    selectedvideoplayer)    'btn_custommediaplayer
        root.AppendChild(doc, "externalbrowser",        externalbrowser)        'CheckBox12
        root.AppendChild(doc, "selectedBrowser",        selectedBrowser)        'btnFindBrowser
        root.AppendChild(doc, "ignorearticle",          ignorearticle)          'CheckBox41
        root.AppendChild(doc, "intruntime",             intruntime)             'CheckBox38
        root.AppendChild(doc, "xbmcartwork",            XBMC_version)           'rbXBMCv_pre,rbXBMCv_post,rbXBMCv_both
        root.AppendChild(doc, "ShowMovieGridToolTip",   ShowMovieGridToolTip)   'cbShowMovieGridToolTip
        root.AppendChild(doc, "ShowLogOnError",         ShowLogOnError)         'cbShowLogOnError
        If Not String.IsNullOrEmpty(font) Then
            root.AppendChild(doc, "font", font)                                 'Button96
        End If


        'Movie Prefs ------------------------------------------------------------
        root.AppendChild(doc, "DownloadTrailerDuringScrape",        DownloadTrailerDuringScrape)        'cbDlTrailerDuringScrape
        root.AppendChild(doc, "gettrailer",                         gettrailer)                         'CheckBox4
        root.AppendChild(doc, "keepfoldername",                     keepfoldername)                     'CheckBox10
        root.AppendChild(doc, "ignoretrailers",                     ignoretrailers)                     'set from frmOptions - obsolete
        root.AppendChild(doc, "moviescraper",                       moviescraper)                       'RadioButton3
        root.AppendChild(doc, "nfoposterscraper",                   nfoposterscraper)                   'IMPA_chk,mpdb_chk,tmdb_chk,imdb_chk
        root.AppendChild(doc, "alwaysuseimdbid",                    alwaysuseimdbid)                    'set from frmOptions - obsolete
        root.AppendChild(doc, "ignoreactorthumbs",                  ignoreactorthumbs)                  'set from frmOptions - obsolete
        root.AppendChild(doc, "maxactors",                          maxactors)                          'ComboBox7
        root.AppendChild(doc, "maxmoviegenre",                      maxmoviegenre)                      'ComboBox6
        root.AppendChild(doc, "enablehdtags",                       enablehdtags)                       'CheckBox19
        root.AppendChild(doc, "movieruntimedisplay",                movieRuntimeDisplay)                'rbRuntimeScraper
        root.AppendChild(doc, "movieRuntimeFallbackToFile",         movieRuntimeFallbackToFile)         'cbMovieRuntimeFallbackToFile
        root.AppendChild(doc, "fanartnotstacked",                   fanartnotstacked)                   'set from frmOptions - obsolete
        root.AppendChild(doc, "posternotstacked",                   posternotstacked)                   'set from frmOptions - obsolete
        root.AppendChild(doc, "scrapemovieposters",                 scrapemovieposters)                 'CheckBox18
        root.AppendChild(doc, "usefanart",                          usefanart)                          'set from frmOptions - obsolete
        root.AppendChild(doc, "dontdisplayposter",                  dontdisplayposter)                  'set from frmOptions - obsolete
        root.AppendChild(doc, "usefoldernames",                     usefoldernames)                     'chkbx_usefoldernames
        root.AppendChild(doc, "allfolders",                         allfolders)                         'chkbx_MovieAllFolders
        root.AppendChild(doc, "actorsave",                          actorsave)                          'saveactorchkbx
        root.AppendChild(doc, "actorsavepath",                      actorsavepath)                      'localactorpath
        root.AppendChild(doc, "actornetworkpath",                   actornetworkpath)                   'xbmcactorpath
        root.AppendChild(doc, "imdbmirror",                         imdbmirror)                         'ListBox9
        root.AppendChild(doc, "createfolderjpg",                    createfolderjpg)                    'chkbx_createfolderjpg
        root.AppendChild(doc, "basicsavemode",                      basicsavemode)                      'chkbx_basicsave
        root.AppendChild(doc, "namemode",                           namemode)                           'cbxNameMode
        root.AppendChild(doc, "usetransparency",                    usetransparency)                    'set from frmOptions - obsolete
        root.AppendChild(doc, "transparencyvalue",                  transparencyvalue)                  'set from frmOptions - obsolete
        root.AppendChild(doc, "keepfoldername",                     keepfoldername)                     'set from frmOptions but still in use - obsolete?
        root.AppendChild(doc, "disablelogs",                        disablelogfiles)                    'CheckBox16
        root.AppendChild(doc, "savefanart",                         savefanart)                         'CheckBox3
        root.AppendChild(doc, "roundminutes",                       roundminutes)                       'set from frmOptions - obsolete
        root.AppendChild(doc, "ignoreparts",                        movieignorepart)                    'cbxCleanFilenameIgnorePart
        root.AppendChild(doc, "cleantags",                          moviecleanTags)                     'btnCleanFilenameAdd,btnCleanFilenameRemove
        root.AppendChild(doc, "moviedefaultlist",                   moviedefaultlist)                   'RadioButtonFileName,RadioButtonTitleAndYear,RadioButtonFolder
        root.AppendChild(doc, "moviesUseXBMCScraper",               movies_useXBMC_Scraper)             'CheckBox_Use_XBMC_Scraper
        root.AppendChild(doc, "whatXBMCScraper",                    whatXBMCScraper)                    'RadioButton51
        root.AppendChild(doc, "scrapefullcert",                     scrapefullcert)                     'ScrapeFullCertCheckBox
        root.AppendChild(doc, "offlinemovielabeltext",              OfflineDVDTitle)                    'TextBox_OfflineDVDTitle
        root.AppendChild(doc, "movierenameenable",                  MovieRenameEnable)                  'MovieRenameCheckBox
        root.AppendChild(doc, "movierenametemplate",                MovieRenameTemplate)                'MovieRenameTemplateTextBox
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
        root.AppendChild(doc, "MovieList_ShowColWatched",           MovieList_ShowColWatched)           'cbMovieList_ShowColWatched
        root.AppendChild(doc, "moviesortorder",                     moviesortorder)                     'cbSort
        root.AppendChild(doc, "movieinvertorder",                   movieinvertorder)                   'btnreverse
        root.AppendChild(doc, "MovieScraper_MaxStudios",            MovieScraper_MaxStudios)            'nudMovieScraper_MaxStudios

        root.AppendChild(doc, "ActorsFilterMinFilms"      ,         ActorsFilterMinFilms     )          'nudActorsFilterMinFilms
        root.AppendChild(doc, "MaxActorsInFilter"         ,         MaxActorsInFilter        )          'nudMaxActorsInFilter
        root.AppendChild(doc, "MovieFilters_Actors_Order" ,         MovieFilters_Actors_Order)          'cbMovieFilters_Actors_Order

        root.AppendChild(doc, "SetsFilterMinFilms"        ,         SetsFilterMinFilms       )          'nudSetsFilterMinFilms
        root.AppendChild(doc, "MaxSetsInFilter"           ,         MaxSetsInFilter          )          'nudMaxSetsInFilter
        root.AppendChild(doc, "MovieFilters_Sets_Order"   ,         MovieFilters_Sets_Order  )          'cbMovieFilters_Sets_Order


        tempstring = If(moviethumbpriority.Count, String.Join("|", moviethumbpriority), "")
        root.AppendChild(doc, "moviethumbpriority", tempstring)                                         'Button61,Button73

        tempstring = If(releaseformat.Count, String.Join("|", releaseformat), "")
        root.AppendChild(doc, "releaseformat", tempstring)                                              'btnVideoSourceAdd,btnVideoSourceRemove

        tempstring = If(certificatepriority.Count, String.Join("|", certificatepriority), "")
        root.AppendChild(doc, "certificatepriority", tempstring)                                        'Button74,Button75

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


        'TV Prefs ------------------------------------------------------------
        root.AppendChild(doc, "tvshowautoquick",        tvshowautoquick)        'CheckBox35
        root.AppendChild(doc, "copytvactorthumbs",      copytvactorthumbs)      'CheckBox34
        root.AppendChild(doc, "displayMissingEpisodes", displayMissingEpisodes) 'SearchForMissingEpisodesToolStripMenuItem
        root.AppendChild(doc, "tvdbmode",               sortorder)              'RadioButton42
        root.AppendChild(doc, "tvdbactorscrape",        TvdbActorScrape)        'ComboBox8
        root.AppendChild(doc, "downloadtvfanart",       tvfanart)               'CheckBox10
        root.AppendChild(doc, "downloadtvposter",       tvposter)               'CheckBox14
        root.AppendChild(doc, "downloadtvseasonthumbs", downloadtvseasonthumbs) 'CheckBox15
        root.AppendChild(doc, "hdtvtags",               enabletvhdtags)         'CheckBox20
        root.AppendChild(doc, "disabletvlogs",          disabletvlogs)          'CheckBox17
        root.AppendChild(doc, "postertype",             postertype)             'posterbtn
        root.AppendChild(doc, "seasonall",              seasonall)              'RadioButton39-41
        root.AppendChild(doc, "tvrename",               tvrename)               'ComboBox_tv_EpisodeRename
        root.AppendChild(doc, "eprenamelowercase",      eprenamelowercase)      'CheckBox_tv_EpisodeRenameCase
        root.AppendChild(doc, "tvshowrefreshlog",       tvshowrefreshlog)       'set from frmOptions - obsolete
        root.AppendChild(doc, "autoepisodescreenshot",  autoepisodescreenshot)  'CheckBox36
        root.AppendChild(doc, "TVShowUseXBMCScraper",   tvshow_useXBMC_Scraper) 'CheckBox_Use_XBMC_TVDB_Scraper
        root.AppendChild(doc, "autorenameepisodes",     autorenameepisodes)     'CheckBox_tv_EpisodeRenameAuto

        tempstring = TvdbLanguageCode & "|" & TvdbLanguage
        root.AppendChild(doc, "tvdblanguage", tempstring)                       'ListBox12,Button91



        doc.AppendChild(root)

        If String.IsNullOrEmpty(workingProfile.Config) Then
            workingProfile.Config = IO.Path.Combine(applicationPath, "settings\config.xml")
        End If
        Dim output As New XmlTextWriter(workingProfile.Config, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub



    Public Shared Sub LoadConfig()
        commandlist.Clear()
        moviesets.Clear()
        moviesets.Add("-None-")
        movieFolders.Clear()
        offlinefolders.Clear()
        tvFolders.Clear()
        tvRootFolders.Clear()
        tableview.Clear()


        If Not File.Exists(workingProfile.Config) Then
            Exit Sub
        End If

        Dim prefs As New XmlDocument
        Try
            prefs.Load(workingProfile.Config)
        Catch ex As Exception
            MsgBox("Error : pr24")
        End Try


        For Each thisresult In prefs("xbmc_media_companion_config_v1.0")
            If thisresult.InnerText <> "" Then  'If blank, preference remains at default value

                Select Case thisresult.Name
                    Case "moviesets"

                        For Each thisset In thisresult.ChildNodes
                            Select Case thisset.Name
                                Case "set"
                                    If thisset.InnerText <> "" Then moviesets.Add(thisset.InnerText)
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
                        movieFolders.Add(decodestring)
                    Case "offlinefolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        offlinefolders.Add(decodestring)
                    Case "tvfolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        tvFolders.Add(decodestring)
                    Case "tvrootfolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        tvRootFolders.Add(decodestring)
                    Case "homemoviefolder"
                        Dim decodestring As String = decxmlchars(thisresult.InnerText)
                        homemoviefolders.Add(decodestring)

                    Case "moviethumbpriority"
                        ReDim moviethumbpriority(3)
                        moviethumbpriority = thisresult.InnerXml.Split("|")

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

                    Case "whatXBMCScraper"
                        XBMC_Scraper = thisresult.InnerXml
                        If thisresult.InnerXml = "imdb" Then
                            whatXBMCScraperIMBD = True
                        ElseIf thisresult.InnerXml = "tmdb" Then
                            whatXBMCScraperTVDB = True
                        End If

                    Case "seasonall"                            : seasonall = thisresult.InnerText
                    Case "splitcontainer1"                      : splt1 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer2"                      : splt2 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer3"                      : splt3 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer4"                      : splt4 = Convert.ToInt32(thisresult.InnerText)
                    Case "splitcontainer5"                      : splt5 = Convert.ToInt32(thisresult.InnerText)
                    Case "maximised"                            : maximised = thisresult.InnerXml
                    Case "locx"                                 : locx = Convert.ToInt32(thisresult.InnerText)
                    Case "locy"                                 : locy = Convert.ToInt32(thisresult.InnerText)
                    Case "gettrailer"                           : gettrailer = thisresult.InnerXml
                    Case "DownloadTrailerDuringScrape"          : DownloadTrailerDuringScrape = thisresult.InnerXml
                    Case "tvshowautoquick"                      : tvshowautoquick = thisresult.InnerXml
                    Case "intruntime"                           : intruntime = thisresult.InnerXml
                    Case "keepfoldername"                       : keepfoldername = thisresult.InnerXml
                    Case "startupcache"                         : startupCache = thisresult.InnerXml
                    Case "ignoretrailers"                       : ignoretrailers = thisresult.InnerXml
                    Case "ignoreactorthumbs"                    : ignoreactorthumbs = thisresult.InnerXml
                    Case "font"                                 : font = thisresult.InnerXml
                    Case "maxactors"                            : maxactors = Convert.ToInt32(thisresult.InnerXml)
                    Case "maxmoviegenre"                        : maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)
                    Case "enablehdtags"                         : enablehdtags = thisresult.InnerXml
                    Case "movieruntimedisplay"                  : movieRuntimeDisplay = thisresult.InnerXml
                    Case "movieRuntimeFallbackToFile"           : movieRuntimeFallbackToFile = thisresult.InnerXml
                    Case "hdtvtags"                             : enabletvhdtags = thisresult.InnerXml
                    Case "renamenfofiles"                       : renamenfofiles = thisresult.InnerXml
                    Case "logview"                              : logview = thisresult.InnerXml
                    Case "fanartnotstacked"                     : fanartnotstacked = thisresult.InnerXml
                    Case "posternotstacked"                     : posternotstacked = thisresult.InnerXml
                    Case "downloadfanart"                       : savefanart = thisresult.InnerXml
                    Case "scrapemovieposters"                   : scrapemovieposters = thisresult.InnerXml
                    Case "usefanart"                            : usefanart = thisresult.InnerXml
                    Case "dontdisplayposter"                    : dontdisplayposter = thisresult.InnerXml
                    Case "rarsize"                              : rarsize = Convert.ToInt32(thisresult.InnerXml)
                    Case "actorsave"                            : actorsave = thisresult.InnerXml
                    Case "actorseasy"                           : actorseasy = thisresult.InnerXml
                    Case "copytvactorthumbs"                    : copytvactorthumbs = thisresult.InnerXml
                    Case "displayMissingEpisodes"               : displayMissingEpisodes = thisresult.InnerXml
                    Case "actorsavepath"                        : actorsavepath = decxmlchars(thisresult.InnerText)
                    Case "actornetworkpath"                     : actornetworkpath = decxmlchars(thisresult.InnerText)
                    Case "overwritethumbs"                      : overwritethumbs = thisresult.InnerXml
                    Case "imdbmirror"                           : imdbmirror = thisresult.InnerXml
                    Case "cleantags"                            : moviecleanTags = thisresult.InnerXml
                    Case "ignoreparts"                          : movieignorepart = thisresult.InnerXml
                    Case "backgroundcolour"                     : backgroundcolour = thisresult.InnerXml
                    Case "forgroundcolour"                      : forgroundcolour = thisresult.InnerXml
                    Case "remembersize"                         : remembersize = thisresult.InnerXml
                    Case "formheight"                           : formheight = Convert.ToInt32(thisresult.InnerXml)
                    Case "formwidth"                            : formwidth = Convert.ToInt32(thisresult.InnerXml)
                    Case "usefoldernames"                       : usefoldernames = thisresult.InnerXml
                    Case "allfolders"                           : allfolders = thisresult.InnerXml
                    Case "createfolderjpg"                      : createfolderjpg = thisresult.InnerXml
                    Case "basicsavemode"                        : basicsavemode = thisresult.InnerXml
                    Case "namemode"                             : namemode = thisresult.InnerXml
                    Case "tvdbmode"                             : sortorder = thisresult.InnerXml
                    Case "tvdbactorscrape"                      : TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)
                    Case "usetransparency"                      : usetransparency = thisresult.InnerXml
                    Case "transparencyvalue"                    : transparencyvalue = Convert.ToInt32(thisresult.InnerXml)
                    Case "downloadtvfanart"                     : tvfanart = thisresult.InnerXml
                    Case "roundminutes"                         : roundminutes = thisresult.InnerXml
                    Case "autoepisodescreenshot"                : autoepisodescreenshot = thisresult.InnerXml
                    Case "ignorearticle"                        : ignorearticle = thisresult.InnerXml
                    Case "TVShowUseXBMCScraper"                 : tvshow_useXBMC_Scraper = thisresult.InnerXml
                    Case "moviesUseXBMCScraper"                 : movies_useXBMC_Scraper = thisresult.InnerXml
                    Case "downloadtvposter"                     : tvposter = thisresult.InnerXml
                    Case "downloadtvseasonthumbs"               : downloadtvseasonthumbs = thisresult.InnerXml
                    Case "maximumthumbs"                        : maximumthumbs = Convert.ToInt32(thisresult.InnerXml)
                    Case "hdtags"                               : enablehdtags = thisresult.InnerXml
                    Case "disablelogs"                          : disablelogfiles = thisresult.InnerXml
                    Case "disabletvlogs"                        : disabletvlogs = thisresult.InnerXml
                    Case "folderjpg"                            : createfolderjpg = thisresult.InnerXml
                    Case "savefanart"                           : savefanart = thisresult.InnerXml
                    Case "postertype"                           : postertype = thisresult.InnerXml
                    Case "tvactorscrape"                        : TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)
                    Case "videomode"                            : videomode = Convert.ToInt32(thisresult.InnerXml)
                    Case "selectedvideoplayer"                  : selectedvideoplayer = thisresult.InnerXml
                    Case "maximagecount"                        : maximagecount = Convert.ToInt32(thisresult.InnerXml)
                    Case "lastpath"                             : lastpath = thisresult.InnerXml
                    Case "moviescraper"                         : moviescraper = thisresult.InnerXml
                    Case "nfoposterscraper"                     : nfoposterscraper = thisresult.InnerXml
                    Case "alwaysuseimdbid"                      : alwaysuseimdbid = thisresult.InnerXml
                    Case "externalbrowser"                      : externalbrowser = thisresult.InnerXml
                    Case "selectedBrowser"                      : selectedBrowser = thisresult.InnerXml
                    Case "tvrename"                             : tvrename = Convert.ToInt32(thisresult.InnerText)
                    Case "tvshowrefreshlog"                     : tvshowrefreshlog = thisresult.InnerXml
                    Case "autorenameepisodes"                   : autorenameepisodes = thisresult.InnerXml
                    Case "eprenamelowercase"                    : eprenamelowercase = thisresult.InnerXml
                    Case "moviedefaultlist"                     : moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                    Case "startuptab"                           : startuptab = Convert.ToByte(thisresult.InnerText)
                    Case "offlinemovielabeltext"                : OfflineDVDTitle = thisresult.InnerText
                    Case "moviemanualrename"                    : MovieManualRename = thisresult.InnerXml
                    Case "movierenameenable"                    : MovieRenameEnable = thisresult.InnerXml
                    Case "movierenametemplate"                  : MovieRenameTemplate = thisresult.InnerText
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
                    Case "MovieList_ShowColWatched"             : MovieList_ShowColWatched = thisresult.InnerXml
                    Case "moviesortorder"                       : moviesortorder = thisresult.InnerXml
                    Case "movieinvertorder"                     : movieinvertorder = thisresult.InnerXml
                    Case "MovieScraper_MaxStudios"              : MovieScraper_MaxStudios = thisresult.InnerXml

                    Case "ActorsFilterMinFilms"                 : ActorsFilterMinFilms      = thisresult.InnerXml
                    Case "MaxActorsInFilter"                    : MaxActorsInFilter         = thisresult.InnerXml
                    Case "MovieFilters_Actors_Order"            : MovieFilters_Actors_Order = thisresult.InnerXml

                    Case "SetsFilterMinFilms"                   : SetsFilterMinFilms        = thisresult.InnerXml
                    Case "MaxSetsInFilter"                      : MaxSetsInFilter           = thisresult.InnerXml
                    Case "MovieFilters_Sets_Order"              : MovieFilters_Sets_Order   = thisresult.InnerXml

                End Select
            End If
        Next

    End Sub


    Public Shared Function decxmlchars(ByVal line As String)
        line = line.Replace("&amp;", "&")
        line = line.Replace("&lt;", "<")
        line = line.Replace("&gt;", ">")
        line = line.Replace("&quot;", "Chr(34)")
        line = line.Replace("&apos;", "'")
        line = line.Replace("&#xA;", vbCrLf)
        Return line
    End Function

    Public Shared Function GetPosterPath(ByVal FullPath As String) As String
        Dim posterpath As String = FullPath
        If Not Utilities.findFileOfType(posterpath, ".tbn") Then
            'Check Frodo naming convention
            If Not Utilities.findFileOfType(posterpath, "-poster.jpg") Then
                If IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\folder.jpg") Then
                    posterpath = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                Else
                    posterpath = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
                End If
            End If
        End If
        Return posterpath
    End Function



    Public Shared Function GetPosterPaths(ByVal FullPath As String) As List(Of String)
        Dim lst=New List(Of String)
        Dim path As String = FullPath

        If Preferences.EdenEnabled Then
            If Not Utilities.findFileOfType(path, ".tbn") Then
                If IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\folder.jpg") Then
                    path = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
                Else
                    path = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
                End If
            End If

            lst.Add(path)
        End If

        If Preferences.FrodoEnabled Then
            path = FullPath.Replace(IO.Path.GetExtension(FullPath), "-poster.jpg")
            lst.Add( path )
        End If

        Return lst
    End Function




    Public Shared Function GetFanartPath(ByVal FullPath As String) As String
        Dim fanartPath As String = FullPath
        If Not Utilities.findFileOfType(fanartPath, "-fanart.jpg") Then
            fanartPath = FullPath.Replace(IO.Path.GetExtension(FullPath), "-fanart.jpg")
        End If
        Return fanartPath

    End Function

    Public Shared Function GetActorThumbPath(Optional ByVal location As String = "")
        Dim actualpath As String = ""
        Try
            If location = Nothing Then
                Return "none"
            End If
            If location = "" Then
                Return "none"
            End If

            If location.IndexOf("http") <> -1 Then
                Return location
            Else
                If location.IndexOf(actornetworkpath) <> -1 Then
                    If actornetworkpath <> Nothing And actorsavepath <> Nothing Then
                        If actornetworkpath <> "" And actorsavepath <> "" Then
                            Dim filename As String = IO.Path.GetFileName(location)
                            actualpath = IO.Path.Combine(actorsavepath, filename)
                            If Not IO.File.Exists(actualpath) Then
                                Dim extension As String = IO.Path.GetExtension(location)
                                Dim purename As String = IO.Path.GetFileName(location)
                                purename = purename.Replace(extension, "")
                                actualpath = actorsavepath & "\" & purename.Substring(purename.Length - 2, 2) & "\" & filename
                            End If
                            If Not IO.File.Exists(actualpath) Then
                                actualpath = "none"
                            End If
                        End If
                    Else
                        actualpath = "none"
                    End If
                Else
                    actualpath = "none"
                End If
            End If
            If actualpath = "" Then actualpath = "none"
            Return actualpath
        Catch
            Return "none"
        Finally

        End Try
    End Function

    Public Shared Function Get_HdTags(ByVal filename As String) As FullFileDetails
        Try
            If IO.Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
                Dim temppath As String = filename.Replace(IO.Path.GetFileName(filename), "VTS_01_0.IFO")
                If IO.File.Exists(temppath) Then
                    filename = temppath
                End If
            End If

            Dim playlist As New List(Of String)
            Dim tempstring As String
            tempstring = Utilities.GetFileName(filename)
            playlist = Utilities.GetMediaList(tempstring)

            If Not IO.File.Exists(filename) Then
                Return Nothing
            End If
            Dim workingfiledetails As New FullFileDetails
            Dim MI As New mediainfo
            'MI = New mediainfo
            MI.Open(filename)
            Dim curVS As Integer = 0
            Dim addVS As Boolean = False
            Dim numOfVideoStreams As Integer = MI.Count_Get(StreamKind.Visual)

            Dim tempmediainfo As String
            Dim tempmediainfo2 As String

            workingfiledetails.filedetails_video.Width.Value = MI.Get_(StreamKind.Visual, curVS, "Width")
            workingfiledetails.filedetails_video.Height.Value = MI.Get_(StreamKind.Visual, curVS, "Height")
            If workingfiledetails.filedetails_video.Width <> Nothing Then
                If IsNumeric(workingfiledetails.filedetails_video.Width.Value) Then
                    If workingfiledetails.filedetails_video.Height <> Nothing Then
                        If IsNumeric(workingfiledetails.filedetails_video.Height.Value) Then
                            '                            Dim tempwidth As Integer = Convert.ToInt32(workingfiledetails.filedetails_video.width)
                            '                            Dim tempheight As Integer = Convert.ToInt32(workingfiledetails.filedetails_video.height)
                            '                            Dim aspect As Decimal
                            Try
                                '                                aspect = tempwidth / tempheight  'Next three line are wrong for getting display aspect ratio
                                '                                aspect = FormatNumber(aspect, 3)
                                '                                If aspect > 0 Then workingfiledetails.filedetails_video.aspect = aspect.ToString

                                Dim Information As String = MI.Inform
                                Dim BeginString As Integer = Information.ToLower.IndexOf(":", Information.ToLower.IndexOf("display aspect ratio"))
                                Dim EndString As Integer = Information.ToLower.IndexOf("frame rate")
                                Dim SizeofString As Integer = EndString - BeginString
                                Dim DisplayAspectRatio As String = Information.Substring(BeginString, SizeofString).Trim(" ", ":", Chr(10), Chr(13))
                                'DisplayAspectRatio = DisplayAspectRatio.Substring(0, Len(DisplayAspectRatio) - 1)
                                If Len(DisplayAspectRatio) > 0 Then
                                    workingfiledetails.filedetails_video.Aspect.Value = DisplayAspectRatio
                                Else
                                    workingfiledetails.filedetails_video.Aspect.Value = "Unknown"
                                End If

                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            End If
            'workingfiledetails.filedetails_video.aspect = MI.Get_(StreamKind.Visual, 0, 79)


            tempmediainfo = MI.Get_(StreamKind.Visual, curVS, "Format")
            If tempmediainfo.ToLower = "avc" Then
                tempmediainfo2 = "h264"
            Else
                tempmediainfo2 = tempmediainfo
            End If

            'workingfiledetails.filedetails_video.codec = tempmediainfo2
            'workingfiledetails.filedetails_video.formatinfo = tempmediainfo
            workingfiledetails.filedetails_video.Codec.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID")
            If workingfiledetails.filedetails_video.Codec.Value = "DX50" Then
                workingfiledetails.filedetails_video.Codec.Value = "DIVX"
            End If
            '_MPEG4/ISO/AVC
            If workingfiledetails.filedetails_video.Codec.Value.ToLower.IndexOf("mpeg4/iso/avc") <> -1 Then
                workingfiledetails.filedetails_video.Codec.Value = "h264"
            End If
            workingfiledetails.filedetails_video.FormatInfo.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID")
            Dim fs(100) As String
            For f = 1 To 100
                fs(f) = MI.Get_(StreamKind.Visual, 0, f)
            Next

            Try
                If playlist.Count = 1 Then
                                                                       
'                    workingfiledetails.filedetails_video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, 61)
                    workingfiledetails.filedetails_video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, "Duration")
                ElseIf playlist.Count > 1 Then
                    'Dim totalmins As Integer = 0
                    'For f = 0 To playlist.Count - 1
                    '    Dim M2 As mediainfo
                    '    M2 = New mediainfo
                    '    M2.Open(playlist(f))
                    '    'Dim temptime As String = M2.Get_(StreamKind.Visual, 0, 61)
                    '    Dim temptime As String = M2.Get_(StreamKind.Visual, 0, "Duration")
                    '    Dim tempint As Integer
                    '    If temptime <> Nothing Then
                    '        Try
                    '            '1h 24mn 48s 546ms
                    '            Dim hours As Integer = 0
                    '            Dim minutes As Integer = 0
                    '            Dim tempstring2 As String = temptime
                    '            tempint = tempstring2.IndexOf("h")
                    '            If tempint <> -1 Then
                    '                hours = Convert.ToInt32(tempstring2.Substring(0, tempint))
                    '                tempstring2 = tempstring2.Substring(tempint + 1, tempstring2.Length - (tempint + 1))
                    '                tempstring2 = Trim(tempstring2)
                    '            End If
                    '            tempint = tempstring2.IndexOf("mn")
                    '            If tempint <> -1 Then
                    '                minutes = Convert.ToInt32(tempstring2.Substring(0, tempint))
                    '            End If
                    '            If hours <> 0 Then
                    '                hours = hours * 60
                    '            End If
                    '            minutes = minutes + hours
                    '            totalmins = totalmins + minutes
                    '        Catch
                    '        End Try
                    '    End If
                    'Next
                    'workingfiledetails.filedetails_video.DurationInSeconds.Value = totalmins & " min"

                    Dim total As Integer=0
                    For f = 0 To playlist.Count - 1

                        Dim M2 As mediainfo = New mediainfo

                        M2.Open(playlist(f))

                       total += Convert.ToInt32(M2.Get_(StreamKind.Visual, 0, "Duration"))
                    Next

                    workingfiledetails.filedetails_video.DurationInSeconds.Value = total
                End If
            Catch
                workingfiledetails.filedetails_video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, 57)
            End Try
            workingfiledetails.filedetails_video.Bitrate.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate/String")
            workingfiledetails.filedetails_video.BitrateMode.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Mode/String")

            workingfiledetails.filedetails_video.BitrateMax.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Maximum/String")

            tempmediainfo = IO.Path.GetExtension(filename) '"This is the extension of the file"
            workingfiledetails.filedetails_video.Container.Value = tempmediainfo
            'workingfiledetails.filedetails_video.codecid = MI.Get_(StreamKind.Visual, curVS, "CodecID")

            workingfiledetails.filedetails_video.CodecInfo.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID/Info")
            workingfiledetails.filedetails_video.ScanType.Value = MI.Get_(StreamKind.Visual, curVS, 102)
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

            'get audio data
            If numOfAudioStreams > 0 Then
                While curAS < numOfAudioStreams
                    Dim audio As New AudioDetails
                    audio.Language.Value = Utilities.GetLangCode(MI.Get_(StreamKind.Audio, curAS, "Language/String"))
                    If MI.Get_(StreamKind.Audio, curAS, "Format") = "MPEG Audio" Then
                        audio.Codec.Value = "MP3"
                    Else
                        audio.Codec.Value = MI.Get_(StreamKind.Audio, curAS, "Format")
                    End If
                    If audio.Codec.Value = "AC-3" Then
                        audio.Codec.Value = "AC3"
                    End If
                    If audio.Codec.Value = "DTS" Then
                        audio.Codec.Value = "dca"
                    End If
                    audio.Channels.Value = MI.Get_(StreamKind.Audio, curAS, "Channel(s)")
                    audio.Bitrate.Value = MI.Get_(StreamKind.Audio, curAS, "BitRate/String")
                    workingfiledetails.filedetails_audio.Add(audio)
                    curAS += 1
                End While
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
End Class
