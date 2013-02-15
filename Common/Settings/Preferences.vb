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

    Public Shared tv_RegexScraper As New List(Of String)
    Public Shared tv_RegexRename As New List(Of String)

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

    'Saved items
    Public Shared customcounter As Integer
    Public Shared tvrename As Integer
    Public Shared locx As Integer
    Public Shared locy As Integer
    Public Shared maxactors As Integer
    Public Shared maxmoviegenre As Integer
    'Public Shared rarsize As Integer
    Public Shared splt1 As Integer
    Public Shared splt2 As Integer
    Public Shared splt3 As Integer
    Public Shared splt4 As Integer
    Public Shared splt5 As Integer
 '   Public Shared resizefanart As Integer
    Public Shared formheight As Integer
    Public Shared formwidth As Integer
    Public Shared videoplaybackmode As Integer
    Public Shared startupdisplaynamemode As Integer
    Public Shared TvdbActorScrape As Integer
    Public Shared transparencyvalue As Integer
    Public Shared maximumthumbs As Integer
    Public Shared startupmode As Integer
    Public Shared videomode As Integer ' = 3
    Public Shared maximagecount As Integer
    Public Shared moviescraper As Integer
    Public Shared nfoposterscraper As Integer

    'Dim tvdbmode As String
    Public Shared XBMC_Scraper As String = "tmdb"   'Locked TMDb as XBMC Scraper.
    Public Shared font As String
    Public Shared moviethumbpriority() As String
    Public Shared certificatepriority() As String
    Public Shared releaseformat() As String
    Public Shared actorsavepath As String
    Public Shared actornetworkpath As String
    Public Shared defaulttvthumb As String
    Public Shared imdbmirror As String
    Public Shared backgroundcolour As String
    Public Shared forgroundcolour As String
    Public Shared namemode As String
    Public Shared TvdbLanguage As String = "English"
    Public Shared TvdbLanguageCode As String = "en"
    Public Shared configpath As String
    Public Shared postertype As String
    Public Shared sortorder As String
    Public Shared selectedvideoplayer As String
    Public Shared lastpath As String
    Public Shared episodeacrorsource As String
    Public Shared seasonall As String
    Public Shared tablesortorder As String
    Public Shared movieRuntimeDisplay As String
    Public Shared selectedBrowser As String
    Public Shared whatXBMCScraper As String = "tmdb"

    Public Shared intruntime As Boolean
    Public Shared autorenameepisodes As Boolean
    Public Shared autoepisodescreenshot As Boolean
    Public Shared ignorearticle As Boolean
    Public Shared tvshow_useXBMC_Scraper As Boolean
    Public Shared movies_useXBMC_Scraper As Boolean
    Public Shared eprenamelowercase As Boolean
    Public Shared tvshowautoquick As Boolean
    Public Shared tvshowrefreshlog As Boolean
    Public Shared roundminutes As Boolean
    Public Shared keepfoldername As Boolean
    Public Shared startupCache As Boolean
    Public Shared ignoretrailers As Boolean
    Public Shared ignoreactorthumbs As Boolean
    Public Shared enabletvhdtags As Boolean
    Public Shared enablehdtags As Boolean
    Public Shared renamenfofiles As Boolean
    Public Shared checkinfofiles As Boolean
    Public Shared disablelogfiles As Boolean
    Public Shared fanartnotstacked As Boolean
    Public Shared posternotstacked As Boolean
    Public Shared scrapemovieposters As Boolean
    Public Shared usefanart As Boolean
    Public Shared dontdisplayposter As Boolean
    Public Shared actorsave As Boolean
    Public Shared overwritethumbs As Boolean
    Public Shared remembersize As Boolean
    Public Shared usefoldernames As Boolean
    Public Shared allfolders As Boolean
    Public Shared createfolderjpg As Boolean
    Public Shared basicsavemode As Boolean
    Public Shared usetransparency As Boolean
    Public Shared downloadtvseasonthumbs As Boolean
    Public Shared disabletvlogs As Boolean
    Public Shared savefanart As Boolean
    Public Shared tvposter As Boolean
    Public Shared tvfanart As Boolean
    Public Shared alwaysuseimdbid As Boolean
    Public Shared gettrailer As Boolean
    Public Shared externalbrowser As Boolean
    Public Shared maximised As Boolean
    Public Shared copytvactorthumbs As Boolean = False
    Public Shared actorseasy As Boolean
    Public Shared scrapefullcert As Boolean
    Public Shared showsortdate As Boolean
    Public Shared fixnfoid As Boolean
    Public Shared displayMissingEpisodes As Boolean = False
    Public Shared movieRuntimeFallbackToFile As Boolean = False
    Public Shared moviesUseXBMCScraper As Boolean = False

    Public Shared moviesortorder As Byte
    Public Shared movieinvertorder As Byte
    Public Shared moviedefaultlist As Byte
    Public Shared startuptab As Byte

    Public Shared moviesets As New List(Of String)
    Public Shared tableview As New List(Of String)
    Public Shared offlinefolders As New List(Of String)

    Public Shared logview As Integer

    Public Shared tvRootFolders As New List(Of String)

    Public Shared movieFolders As New List(Of String)
    Public Shared tvFolders As New List(Of String)


    Public Shared profiles As New List(Of ListOfProfiles)
    Public Shared workingProfile As New ListOfProfiles
    Public Shared commandlist As New List(Of str_ListOfCommands)

    Public Shared whatXBMCScraperIMBD As Boolean
    Public Shared whatXBMCScraperTVDB As Boolean
    Public Shared OfflineDVDTitle As String
    Public Shared MovieRenameEnable As Boolean
    Public Shared MovieRenameTemplate As String
    Public Shared moviePreferredTrailerResolution As String
    Public Shared MovieImdbGenreRegEx As String
    Public Shared homemoviefolders As New List(Of String)
    Public Shared DownloadTrailerDuringScrape As Boolean
    Public Shared XBMC_version As Byte

    Public Shared TMDbSelectedLanguageName  As String  = "English - US"
    Public Shared TMDbUseCustomLanguage     As Boolean = False
    Public Shared TMDbCustomLanguageValue   As String  = ""
    Public Shared GetMovieSetFromTMDb       As Boolean = True
    Public Shared LogScrapeTimes            As Boolean = False
    Public Shared ScrapeTimingsLogThreshold As Integer = 100

    Public Shared ActorResolutionSI         As Integer =  2     ' Height  768           SI = Selected Index
    Public Shared PosterResolutionSI        As Integer =  9     ' Height  1080  
    Public Shared BackDropResolutionSI      As Integer = 15     ' Full HD 1920x1080
    Public Shared ShowMovieGridToolTip      As Boolean = True
    Public Shared ActorsFilterMinFilms      As Integer =  2
    Public Shared MaxActorsInFilter         As Integer =  100

    Public Shared applicationDatapath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Media Companion\"

    Public Shared Sub SetUpPreferences()
        'General
        ignorearticle = False
        externalbrowser = False
        selectedBrowser = ""
        videoplaybackmode = "1"
        backgroundcolour = "Silver"
        forgroundcolour = "#D3D9DC"
        formheight = "600"
        formwidth = "800"
        disablelogfiles = False
        startupCache = True
        rarsize = 8
        renamenfofiles = True
        checkinfofiles = True
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
        movieinvertorder = 0
        imdbmirror = "http://www.imdb.com/"
        usefoldernames = False
        allfolders = False
        ReDim moviethumbpriority(3)
        maxmoviegenre = 99
        moviethumbpriority(0) = "Internet Movie Poster Awards"
        moviethumbpriority(1) = "themoviedb.org"
        moviethumbpriority(2) = "Movie Poster DB"
        moviethumbpriority(3) = "IMDB"
        movieRuntimeDisplay = "scraper"
        moviePreferredTrailerResolution = "720"
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
        tvdbactorscrape = 0
        defaulttvthumb = "poster"
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
        startupmode = 1
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

        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("xbmc_media_companion_config_v1.0")


        For Each path In tvFolders
            child = doc.CreateElement("tvfolder")
            child.InnerText = path
            root.AppendChild(child)
        Next

        For Each path In tvRootFolders
            child = doc.CreateElement("tvrootfolder")
            child.InnerText = path
            root.AppendChild(child)
        Next

        For Each path In movieFolders
            child = doc.CreateElement("nfofolder")
            child.InnerText = path
            root.AppendChild(child)
        Next
        Dim list As New List(Of String)
        For Each path In offlinefolders
            If Not list.Contains(path) Then
                child = doc.CreateElement("offlinefolder")
                child.InnerText = path
                root.AppendChild(child)
                list.Add(path)
            End If
        Next

        For Each Path In homemoviefolders
            child = doc.CreateElement("homemoviefolder")
            child.InnerText = Path
            root.AppendChild(child)
            list.Add(Path)
        Next

        child = doc.CreateElement("DownloadTrailerDuringScrape")
        child.InnerText = DownloadTrailerDuringScrape.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("gettrailer")
        child.InnerText = gettrailer.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("keepfoldername")
        child.InnerText = keepfoldername.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("startupcache")
        child.InnerText = startupCache.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("ignoretrailers")
        child.InnerText = ignoretrailers.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviescraper")
        child.InnerText = moviescraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("nfoposterscraper")
        child.InnerText = nfoposterscraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("alwaysuseimdbid")
        child.InnerText = alwaysuseimdbid.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignoreactorthumbs")
        child.InnerText = ignoreactorthumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("maxactors")
        child.InnerText = maxactors.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("maxmoviegenre")
        child.InnerText = maxmoviegenre.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("enablehdtags")
        child.InnerText = enablehdtags.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("movieruntimedisplay")
        child.InnerText = movieRuntimeDisplay
        root.AppendChild(child)

        child = doc.CreateElement("movieRuntimeFallbackToFile")
        child.InnerText = movieRuntimeFallbackToFile.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("renamenfofiles")
        child.InnerText = renamenfofiles.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("checkinfofiles")
        child.InnerText = checkinfofiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("tvshowautoquick")
        child.InnerText = tvshowautoquick.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("disablelogfiles")
        child.InnerText = disablelogfiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("actorseasy")
        child.InnerText = actorseasy.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("copytvactorthumbs")
        child.InnerText = copytvactorthumbs.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("displayMissingEpisodes")
        child.InnerText = displayMissingEpisodes.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("fanartnotstacked")
        child.InnerText = fanartnotstacked.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("posternotstacked")
        child.InnerText = posternotstacked.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("lastpath")
        child.InnerText = lastpath
        root.AppendChild(child)

        child = doc.CreateElement("downloadfanart")
        child.InnerText = savefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("scrapemovieposters")
        child.InnerText = scrapemovieposters.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("usefanart")
        child.InnerText = usefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("dontdisplayposter")
        child.InnerText = dontdisplayposter.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("usefoldernames")
        child.InnerText = usefoldernames.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("allfolders")
        child.InnerText = allfolders.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("rarsize")
        child.InnerText = rarsize.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("actorsave")
        child.InnerText = actorsave.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("actorsavepath")
        child.InnerText = actorsavepath
        root.AppendChild(child)


        child = doc.CreateElement("actornetworkpath")
        child.InnerText = actornetworkpath
        root.AppendChild(child)


        'child = doc.CreateElement("resizefanart")
        'child.InnerText = resizefanart.ToString.ToLower
        'root.AppendChild(child)


        'Duplicate
        'child = doc.CreateElement("overwritethumbs")
        'child.InnerText = overwritethumbs.ToString.ToLower
        'root.AppendChild(child)


        child = doc.CreateElement("defaulttvthumb")
        child.InnerText = defaulttvthumb
        root.AppendChild(child)


        child = doc.CreateElement("imdbmirror")
        child.InnerText = imdbmirror
        root.AppendChild(child)


        child = doc.CreateElement("backgroundcolour")
        child.InnerText = backgroundcolour
        root.AppendChild(child)


        child = doc.CreateElement("forgroundcolour")
        child.InnerText = forgroundcolour
        root.AppendChild(child)


        child = doc.CreateElement("remembersize")
        child.InnerText = remembersize.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("locx")
        child.InnerText = locx.ToString
        root.AppendChild(child)

        child = doc.CreateElement("locy")
        child.InnerText = locy.ToString
        root.AppendChild(child)


        child = doc.CreateElement("formheight")
        child.InnerText = formheight.ToString
        root.AppendChild(child)


        child = doc.CreateElement("formwidth")
        child.InnerText = formwidth.ToString
        root.AppendChild(child)


        child = doc.CreateElement("videoplaybackmode")
        child.InnerText = videoplaybackmode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("createfolderjpg")
        child.InnerText = createfolderjpg.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("basicsavemode")
        child.InnerText = basicsavemode.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("startupdisplaynamemode")
        child.InnerText = startupdisplaynamemode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("namemode")
        child.InnerText = namemode
        root.AppendChild(child)


        child = doc.CreateElement("tvdbmode")
        child.InnerText = sortorder
        root.AppendChild(child)


        child = doc.CreateElement("tvdbactorscrape")
        child.InnerText = tvdbactorscrape.ToString
        root.AppendChild(child)


        child = doc.CreateElement("usetransparency")
        child.InnerText = usetransparency.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("transparencyvalue")
        child.InnerText = transparencyvalue.ToString
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvfanart")
        child.InnerText = tvfanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvposter")
        child.InnerText = tvposter.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("keepfoldername")
        child.InnerText = keepfoldername.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvseasonthumbs")
        child.InnerText = downloadtvseasonthumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("maximumthumbs")
        child.InnerText = maximumthumbs.ToString
        root.AppendChild(child)


        child = doc.CreateElement("startupmode")
        child.InnerText = startupmode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("hdtvtags")
        child.InnerText = enabletvhdtags.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("disablelogs")
        child.InnerText = disablelogfiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("disabletvlogs")
        child.InnerText = disabletvlogs.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("overwritethumbs")
        child.InnerText = overwritethumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("savefanart")
        child.InnerText = savefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("postertype")
        child.InnerText = postertype
        root.AppendChild(child)

        child = doc.CreateElement("roundminutes")
        child.InnerText = roundminutes.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("tvactorscrape")
        child.InnerText = tvdbactorscrape.ToString
        root.AppendChild(child)


        child = doc.CreateElement("videomode")
        child.InnerText = videomode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("selectedvideoplayer")
        child.InnerText = selectedvideoplayer
        root.AppendChild(child)

        child = doc.CreateElement("externalbrowser")
        child.InnerText = externalbrowser.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("selectedBrowser")
        child.InnerText = selectedBrowser
        root.AppendChild(child)

        child = doc.CreateElement("ignoreparts")
        child.InnerText = movieignorepart.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("cleantags")
        child.InnerText = moviecleanTags
        root.AppendChild(child)


        child = doc.CreateElement("moviethumbpriority")
        Dim tempstring As String = ""
        If moviethumbpriority IsNot Nothing AndAlso moviethumbpriority.Length > 2 Then
            tempstring = moviethumbpriority(0) & "|" & moviethumbpriority(1) & "|" & moviethumbpriority(2) & "|" & moviethumbpriority(3)
        End If
        child.InnerText = tempstring
        root.AppendChild(child)


        child = doc.CreateElement("releaseformat")
        tempstring = ""
        If releaseformat IsNot Nothing Then
            For f = 0 To releaseformat.Length - 1
                tempstring = tempstring & releaseformat(f) & "|"
            Next
        End If
        child.InnerText = tempstring.TrimEnd("|")
        root.AppendChild(child)


        child = doc.CreateElement("tvdblanguage")
        tempstring = ""
        tempstring = TvdbLanguageCode & "|" & TvdbLanguage
        child.InnerText = tempstring
        root.AppendChild(child)



        child = doc.CreateElement("certificatepriority")
        tempstring = ""
        If certificatepriority IsNot Nothing Then
            For f = 0 To certificatepriority.Length - 1
                tempstring = tempstring & certificatepriority(f) & "|"
            Next
        End If
        child.InnerText = tempstring.TrimEnd("|")
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer1")
        child.InnerText = splt1.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer2")
        child.InnerText = splt2.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer3")
        child.InnerText = splt3.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer4")
        child.InnerText = splt4.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer5")
        child.InnerText = splt5.ToString
        root.AppendChild(child)


        child = doc.CreateElement("seasonall")
        child.InnerText = seasonall
        root.AppendChild(child)

        child = doc.CreateElement("maximised")
        If maximised = True Then
            child.InnerText = "true"
        Else
            child.InnerText = "false"
        End If
        root.AppendChild(child)

        child = doc.CreateElement("tvrename")
        child.InnerText = tvrename.ToString
        root.AppendChild(child)

        child = doc.CreateElement("eprenamelowercase")
        child.InnerText = eprenamelowercase.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("tvshowrefreshlog")
        child.InnerText = tvshowrefreshlog.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesortorder")
        child.InnerText = movieinvertorder.ToString & moviesortorder.ToString
        root.AppendChild(child)

        child = doc.CreateElement("moviedefaultlist")
        child.InnerText = moviedefaultlist.ToString
        root.AppendChild(child)

        child = doc.CreateElement("autoepisodescreenshot")
        child.InnerText = autoepisodescreenshot.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignorearticle")
        child.InnerText = ignorearticle.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("TVShowUseXBMCScraper")
        child.InnerText = tvshow_useXBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesUseXBMCScraper")
        child.InnerText = movies_useXBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("whatXBMCScraper")
        child.InnerText = XBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("startuptab")
        child.InnerText = startuptab.ToString
        root.AppendChild(child)

        child = doc.CreateElement("intruntime")
        child.InnerText = intruntime.ToString.ToLower
        root.AppendChild(child)

        If font <> Nothing Then
            If font <> "" Then
                child = doc.CreateElement("font")
                child.InnerText = font
                root.AppendChild(child)
            End If
        End If

        child = doc.CreateElement("autorenameepisodes")
        child.InnerText = autorenameepisodes.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("scrapefullcert")
        child.InnerText = scrapefullcert.ToString.ToLower
        root.AppendChild(child)

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

        child = doc.CreateElement("offlinemovielabeltext")
        child.InnerText = OfflineDVDTitle
        root.AppendChild(child)



        root.AppendChild(child)
        child = doc.CreateElement("movierenameenable")
        If MovieRenameEnable = True Then
            child.InnerText = "true"
        Else
            child.InnerText = "false"
        End If
        root.AppendChild(child)


        child = doc.CreateElement("movierenametemplate")
        child.InnerText = MovieRenameTemplate
        root.AppendChild(child)



        child = doc.CreateElement("showsortdate")
        child.InnerText = showsortdate
        root.AppendChild(child)

        child = doc.CreateElement("logview")
        child.InnerText = logview
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


        child = doc.CreateElement("moviePreferredHDTrailerResolution")
        child.InnerText = moviePreferredTrailerResolution.ToUpper()
        root.AppendChild(child)

        child = doc.CreateElement("MovieImdbGenreRegEx")
        child.InnerText = MovieImdbGenreRegEx.ToString
        root.AppendChild(child)

        child = doc.CreateElement("xbmcartwork")
        child.InnerText = Preferences.XBMC_version.ToString
        root.AppendChild(child)

        child = doc.CreateElement("GetMovieSetFromTMDb")
        child.InnerText = GetMovieSetFromTMDb
        root.AppendChild(child)

        child = doc.CreateElement("LogScrapeTimes")
        child.InnerText = LogScrapeTimes
        root.AppendChild(child)

        child = doc.CreateElement("ScrapeTimingsLogThreshold")
        child.InnerText = ScrapeTimingsLogThreshold
        root.AppendChild(child)

        root.AppendChild(doc, "TMDbSelectedLanguage"  , TMDbSelectedLanguageName )
        root.AppendChild(doc, "TMDbUseCustomLanguage" , TMDbUseCustomLanguage    )
        root.AppendChild(doc, "TMDbCustomLanguage"    , TMDbCustomLanguageValue  )
       
        root.AppendChild(doc, "ActorResolution"       , ActorResolutionSI        )
        root.AppendChild(doc, "PosterResolution"      , PosterResolutionSI       )
        root.AppendChild(doc, "BackDropResolution"    , BackDropResolutionSI     )
        
        root.AppendChild(doc, "ShowMovieGridToolTip"  , ShowMovieGridToolTip     )
        
            
        doc.AppendChild(root)

        If String.IsNullOrEmpty(workingProfile.Config) Then
            workingProfile.Config = IO.Path.Combine(applicationPath, "settings\config.xml")
        End If
        Dim output As New XmlTextWriter(workingProfile.Config, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
        'Catch ex As Exception
        '    MsgBox("Can't find the following path..." & vbCrLf & workingProfile.config & vbCrLf & "Please Check/Delete Settings Folder & Restart MC")
        'End Try
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
            'Try
            Select Case thisresult.Name
                Case "moviesets"

                    For Each thisset In thisresult.ChildNodes
                        Select Case thisset.Name
                            Case "set"
                                moviesets.Add(thisset.InnerText)
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
                Case "seasonall"
                    If thisresult.InnerText <> "" Then seasonall = thisresult.InnerText
                Case "splitcontainer1"
                    If thisresult.InnerText <> "" Then splt1 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer2"
                    If thisresult.InnerText <> "" Then splt2 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer3"
                    If thisresult.InnerText <> "" Then splt3 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer4"
                    If thisresult.InnerText <> "" Then splt4 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer5"
                    If thisresult.InnerText <> "" Then splt5 = Convert.ToInt32(thisresult.InnerText)
                Case "maximised"
                    If thisresult.InnerText = "true" Then
                        maximised = True
                    Else
                        maximised = False
                    End If
                Case "locx"
                    If thisresult.InnerText <> "" Then locx = Convert.ToInt32(thisresult.InnerText)
                Case "locy"
                    If thisresult.InnerText <> "" Then locy = Convert.ToInt32(thisresult.InnerText)
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
                Case ("homemoviefolder")
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    homemoviefolders.Add(decodestring)
                Case "gettrailer"
                    If thisresult.InnerXml = "true" Then
                        gettrailer = True
                    ElseIf thisresult.InnerXml = "false" Then
                        gettrailer = False
                    End If
                Case "DownloadTrailerDuringScrape"
                    If thisresult.InnerXml = "true" Then
                        DownloadTrailerDuringScrape = True
                    ElseIf thisresult.InnerXml = "false" Then
                        DownloadTrailerDuringScrape = False
                    End If
                Case "tvshowautoquick"
                    If thisresult.InnerXml = "true" Then
                        tvshowautoquick = True
                    ElseIf thisresult.InnerXml = "false" Then
                        tvshowautoquick = False
                    End If
                Case "intruntime"
                    If thisresult.InnerXml = "true" Then
                        intruntime = True
                    ElseIf thisresult.InnerXml = "false" Then
                        intruntime = False
                    End If
                Case "keepfoldername"
                    If thisresult.InnerXml = "true" Then
                        keepfoldername = True
                    ElseIf thisresult.InnerXml = "false" Then
                        keepfoldername = False
                    End If

                Case "startupcache"
                    If thisresult.InnerXml = "true" Then
                        startupCache = True
                    ElseIf thisresult.InnerXml = "false" Then
                        startupCache = False
                    End If

                Case "ignoretrailers"
                    If thisresult.InnerXml = "true" Then
                        ignoretrailers = True
                    ElseIf thisresult.InnerXml = "false" Then
                        ignoretrailers = False
                    End If

                Case "ignoreactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        ignoreactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        ignoreactorthumbs = False
                    End If

                Case "font"
                    If thisresult.InnerXml <> Nothing Then
                        If thisresult.InnerXml <> "" Then
                            font = thisresult.InnerXml
                        End If
                    End If

                Case "maxactors"
                    If thisresult.InnerText <> "" Then maxactors = Convert.ToInt32(thisresult.InnerXml)

                Case "maxmoviegenre"
                    If thisresult.InnerText <> "" Then maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)

                Case "enablehdtags"
                    If thisresult.InnerXml = "true" Then
                        enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        enablehdtags = False
                    End If

                Case "movieruntimedisplay"
                    If thisresult.InnerText <> "" Then movieRuntimeDisplay = thisresult.InnerXml

                Case "movieRuntimeFallbackToFile"
                    movieRuntimeFallbackToFile = thisresult.InnerXml

                Case "hdtvtags"
                    If thisresult.InnerXml = "true" Then
                        enabletvhdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        enabletvhdtags = False
                    End If

                Case "renamenfofiles"
                    If thisresult.InnerXml = "true" Then
                        renamenfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        renamenfofiles = False
                    End If

                Case "checkinfofiles"
                    If thisresult.InnerXml = "true" Then
                        checkinfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        checkinfofiles = False
                    End If

                Case "disablelogfiles"
                    If thisresult.InnerXml = "true" Then
                        disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        disablelogfiles = False
                    End If

                Case "logview"
                    logview = thisresult.InnerXml

                Case "fanartnotstacked"
                    If thisresult.InnerXml = "true" Then
                        fanartnotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        fanartnotstacked = False
                    End If

                Case "posternotstacked"
                    If thisresult.InnerXml = "true" Then
                        posternotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        posternotstacked = False
                    End If

                Case "downloadfanart"
                    If thisresult.InnerXml = "true" Then
                        savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        savefanart = False
                    End If

                Case "scrapemovieposters"
                    If thisresult.InnerXml = "true" Then
                        scrapemovieposters = True
                    ElseIf thisresult.InnerXml = "false" Then
                        scrapemovieposters = False
                    End If

                Case "usefanart"
                    If thisresult.InnerXml = "true" Then
                        usefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        usefanart = False
                    End If

                Case "dontdisplayposter"
                    If thisresult.InnerXml = "true" Then
                        dontdisplayposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        dontdisplayposter = False
                    End If

                Case "rarsize"
                    If thisresult.InnerText <> "" Then rarsize = Convert.ToInt32(thisresult.InnerXml)

                Case "actorsave"
                    If thisresult.InnerXml = "true" Then
                        actorsave = True
                    ElseIf thisresult.InnerXml = "false" Then
                        actorsave = False
                    End If

                Case "actorseasy"
                    If thisresult.InnerXml = "true" Then
                        actorseasy = True
                    ElseIf thisresult.InnerXml = "false" Then
                        actorseasy = False
                    End If

                Case "copytvactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        copytvactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        copytvactorthumbs = False
                    End If

                Case "displayMissingEpisodes"
                    If thisresult.InnerXml = "true" Then
                        displayMissingEpisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        displayMissingEpisodes = False
                    End If

                Case "actorsavepath"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    If thisresult.InnerText <> "" Then actorsavepath = decodestring

                Case "actornetworkpath"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    If thisresult.InnerText <> "" Then actornetworkpath = decodestring

                'Case "resizefanart"
                '    If thisresult.InnerText <> "" Then resizefanart = Convert.ToInt32(thisresult.InnerXml)

                Case "overwritethumbs"
                    If thisresult.InnerXml = "true" Then
                        overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        overwritethumbs = False
                    End If

                Case "defaulttvthumb"
                    If thisresult.InnerText <> "" Then defaulttvthumb = thisresult.InnerXml

                Case "imdbmirror"
                    If thisresult.InnerText <> "" Then imdbmirror = thisresult.InnerXml

                Case "moviethumbpriority"
                    ReDim moviethumbpriority(3)
                    If thisresult.InnerText <> "" Then moviethumbpriority = thisresult.InnerXml.Split("|")

                Case "certificatepriority"
                    ReDim certificatepriority(33)
                    If thisresult.InnerText <> "" Then certificatepriority = thisresult.InnerXml.Split("|")

                Case "releaseformat"
                    If thisresult.InnerText <> "" Then
                        Dim count As Integer = 0
                        Dim index As Integer = 0
                        Do Until index < 0
                            count += 1
                            index = thisresult.InnerText.IndexOf("|"c, index + 1)
                        Loop
                        ReDim releaseformat(count)
                        releaseformat = thisresult.InnerXml.Split("|")
                    End If

                Case "cleantags"
                    If thisresult.InnerText <> "" Then moviecleanTags = thisresult.InnerXml

                Case "ignoreparts"
                    If thisresult.InnerXml = "true" Then
                        movieignorepart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        movieignorepart = False
                    End If

                Case "backgroundcolour"
                    If thisresult.InnerText <> "" Then backgroundcolour = thisresult.InnerXml

                Case "forgroundcolour"
                    If thisresult.InnerText <> "" Then forgroundcolour = thisresult.InnerXml

                Case "remembersize"
                    If thisresult.InnerXml = "true" Then
                        remembersize = True
                    ElseIf thisresult.InnerXml = "false" Then
                        remembersize = False
                    End If

                Case "formheight"
                    If thisresult.InnerText <> "" Then formheight = Convert.ToInt32(thisresult.InnerXml)

                Case "formwidth"
                    If thisresult.InnerText <> "" Then formwidth = Convert.ToInt32(thisresult.InnerXml)
                Case "videoplaybackmode"
                    If thisresult.InnerText <> "" Then videoplaybackmode = Convert.ToInt32(thisresult.InnerXml)

                Case "usefoldernames"
                    If thisresult.InnerXml = "true" Then
                        usefoldernames = True
                    ElseIf thisresult.InnerXml = "false" Then
                        usefoldernames = False
                    End If

                Case "allfolders"
                    If thisresult.InnerXml = "true" Then
                        allfolders = True
                    ElseIf thisresult.InnerXml = "false" Then
                        allfolders = False
                    End If

                Case "createfolderjpg"
                    If thisresult.InnerXml = "true" Then
                        createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        createfolderjpg = False
                    End If

                Case "basicsavemode"
                    If thisresult.InnerXml = "true" Then
                        basicsavemode = True
                    ElseIf thisresult.InnerXml = "false" Then
                        basicsavemode = False
                    End If

                Case "startupdisplaynamemode"
                    If thisresult.InnerText <> "" Then startupdisplaynamemode = Convert.ToInt32(thisresult.InnerXml)

                Case "namemode"
                    If thisresult.InnerText <> "" Then namemode = thisresult.InnerXml

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

                Case "tvdbmode"
                    If thisresult.InnerText <> "" Then sortorder = thisresult.InnerXml
                Case "tvdbactorscrape"
                    If thisresult.InnerText <> "" Then TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)

                Case "usetransparency"
                    If thisresult.InnerXml = "true" Then
                        usetransparency = True
                    ElseIf thisresult.InnerXml = "false" Then
                        usetransparency = False
                    End If

                Case "transparencyvalue"
                    If thisresult.InnerText <> "" Then transparencyvalue = Convert.ToInt32(thisresult.InnerXml)

                Case "downloadtvfanart"
                    If thisresult.InnerXml = "true" Then
                        tvfanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        tvfanart = False
                    End If

                Case "roundminutes"
                    If thisresult.InnerXml = "true" Then
                        roundminutes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        roundminutes = False
                    End If

                Case "autoepisodescreenshot"
                    If thisresult.InnerXml = "true" Then
                        autoepisodescreenshot = True
                    ElseIf thisresult.InnerXml = "false" Then
                        autoepisodescreenshot = False
                    End If

                Case "ignorearticle"
                    If thisresult.InnerXml = "true" Then
                        ignorearticle = True
                    ElseIf thisresult.InnerXml = "false" Then
                        ignorearticle = False
                    End If

                Case "TVShowUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        tvshow_useXBMC_Scraper = True
                    ElseIf thisresult.InnerXml = "false" Then
                        tvshow_useXBMC_Scraper = False
                    End If

                Case "moviesUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        movies_useXBMC_Scraper = True

                    ElseIf thisresult.InnerXml = "false" Then
                        movies_useXBMC_Scraper = False
                    End If
                Case "whatXBMCScraper"
                    XBMC_Scraper = thisresult.InnerXml
                    If thisresult.InnerXml = "imdb" Then
                        whatXBMCScraperIMBD = True

                    ElseIf thisresult.InnerXml = "tmdb" Then
                        whatXBMCScraperTVDB = True
                    End If
                Case "downloadtvposter"
                    If thisresult.InnerXml = "true" Then
                        tvposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        tvposter = False
                    End If

                Case "downloadtvseasonthumbs"
                    If thisresult.InnerXml = "true" Then
                        downloadtvseasonthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        downloadtvseasonthumbs = False
                    End If

                Case "maximumthumbs"
                    If thisresult.InnerText <> "" Then maximumthumbs = Convert.ToInt32(thisresult.InnerXml)

                Case "startupmode"
                    If thisresult.InnerText <> "" Then startupmode = Convert.ToInt32(thisresult.InnerXml)

                Case "hdtags"
                    If thisresult.InnerXml = "true" Then
                        enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        enablehdtags = False
                    End If

                Case "disablelogs"
                    If thisresult.InnerXml = "true" Then
                        disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        disablelogfiles = False
                    End If

                Case "disabletvlogs"
                    If thisresult.InnerXml = "true" Then
                        disabletvlogs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        disabletvlogs = False
                    End If

                'Case "overwritethumb"
                '    If thisresult.InnerXml = "true" Then
                '        overwritethumbs = True
                '    ElseIf thisresult.InnerXml = "false" Then
                '        overwritethumbs = False
                '    End If

                Case "folderjpg"
                    If thisresult.InnerXml = "true" Then
                        createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        createfolderjpg = False
                    End If

                Case "savefanart"
                    If thisresult.InnerXml = "true" Then
                        savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        savefanart = False
                    End If

                Case "postertype"
                    If thisresult.InnerText <> "" Then postertype = thisresult.InnerXml

                Case "tvactorscrape"
                    If thisresult.InnerText <> "" Then TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)

                Case "videomode"
                    If thisresult.InnerText <> "" Then videomode = Convert.ToInt32(thisresult.InnerXml)

                Case "selectedvideoplayer"
                    If thisresult.InnerText <> "" Then selectedvideoplayer = thisresult.InnerXml

                Case "maximagecount"
                    If thisresult.InnerText <> "" Then maximagecount = Convert.ToInt32(thisresult.InnerXml)

                Case "lastpath"
                    If thisresult.InnerText <> "" Then lastpath = thisresult.InnerXml

                Case "moviescraper"
                    If thisresult.InnerText <> "" Then moviescraper = thisresult.InnerXml

                Case "nfoposterscraper"
                    If thisresult.InnerText <> "" Then nfoposterscraper = thisresult.InnerXml

                Case "alwaysuseimdbid"
                    If thisresult.InnerXml = "true" Then
                        alwaysuseimdbid = True
                    ElseIf thisresult.InnerXml = "false" Then
                        alwaysuseimdbid = False
                    End If

                Case "externalbrowser"
                    If thisresult.InnerXml = "true" Then
                        externalbrowser = True
                    ElseIf thisresult.InnerXml = "false" Then
                        externalbrowser = False
                    End If

                Case "selectedBrowser"
                    selectedBrowser = thisresult.InnerXml

                Case "tvrename"
                    If thisresult.InnerText <> "" Then tvrename = Convert.ToInt32(thisresult.InnerText)
                Case "tvshowrefreshlog"
                    If thisresult.InnerXml = "true" Then
                        tvshowrefreshlog = True
                    ElseIf thisresult.InnerXml = "false" Then
                        tvshowrefreshlog = False
                    End If
                    'public shared moviesortorder As Byte
                    'public shared moviedefaultlist As Byte
                    'public shared lasttab As Byte
                Case "autorenameepisodes"
                    If thisresult.InnerXml = "true" Then
                        autorenameepisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        autorenameepisodes = False
                    End If
                Case "eprenamelowercase"
                    If thisresult.InnerXml = "true" Then
                        eprenamelowercase = True
                    ElseIf thisresult.InnerXml = "false" Then
                        eprenamelowercase = False
                    End If
                Case "moviesortorder"
                    If thisresult.InnerText <> "" Then
                        Dim sortOrder() As Char = thisresult.InnerText.ToString.ToArray
                        If sortOrder.Length < 2 Then
                            ReDim Preserve sortOrder(1)
                            sortOrder(1) = "0"
                            Array.Reverse(sortOrder)
                        End If
                        movieinvertorder = Val(sortOrder(0))
                        moviesortorder = Val(sortOrder(1))

                    End If
                Case "moviedefaultlist"
                    If thisresult.InnerText <> "" Then moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                Case "startuptab"
                    If thisresult.InnerText <> "" Then startuptab = Convert.ToByte(thisresult.InnerText)

                Case "offlinemovielabeltext"
                    If thisresult.InnerText <> "" Then OfflineDVDTitle = thisresult.InnerText

                Case "movierenameenable"
                    If thisresult.InnerXml = "true" Then
                        MovieRenameEnable = True
                    ElseIf thisresult.InnerXml = "false" Then
                        MovieRenameEnable = False
                    End If

                Case "movierenametemplate"
                    If thisresult.InnerText <> "" Then MovieRenameTemplate = thisresult.InnerText

                Case "showsortdate"
                    If thisresult.InnerText = Nothing Or thisresult.InnerText = "" Then
                        showsortdate = False
                    Else
                        showsortdate = thisresult.InnerText
                    End If

                Case "scrapefullcert"
                    If thisresult.InnerXml = "true" Then
                        scrapefullcert = True
                    ElseIf thisresult.InnerXml = "false" Then
                        scrapefullcert = False
                    End If

                Case "moviePreferredHDTrailerResolution"
                    If thisresult.InnerText <> "" Then moviePreferredTrailerResolution = thisresult.InnerXml.ToUpper()

                Case "MovieImdbGenreRegEx"
                    If thisresult.InnerText <> "" Then MovieImdbGenreRegEx = decxmlchars(thisresult.InnerXml)

                Case "xbmcartwork"
                    If thisresult.InnerText <> "" Then Preferences.XBMC_version = Convert.ToByte(thisresult.InnerText)

                Case "GetMovieSetFromTMDb"       : GetMovieSetFromTMDb       = thisresult.InnerXml
                Case "LogScrapeTimes"            : LogScrapeTimes            = thisresult.InnerXml
                Case "ScrapeTimingsLogThreshold" : ScrapeTimingsLogThreshold = thisresult.InnerXml
                Case "TMDbSelectedLanguage"      : TMDbSelectedLanguageName  = thisresult.InnerXml
                Case "TMDbUseCustomLanguage"     : TMDbUseCustomLanguage     = thisresult.InnerXml
                Case "TMDbCustomLanguage"        : TMDbCustomLanguageValue   = thisresult.InnerXml

                Case "ActorResolution"           : ActorResolutionSI           = thisresult.InnerXml
                Case "PosterResolution"          : PosterResolutionSI          = thisresult.InnerXml
                Case "BackDropResolution"        : BackDropResolutionSI        = thisresult.InnerXml

                Case "ShowMovieGridToolTip"      : ShowMovieGridToolTip        = thisresult.InnerXml
                

            End Select
            'Catch
            '    'MsgBox("Error : pr278")
            'End Try
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
            If IO.File.Exists(IO.Path.GetDirectoryName(FullPath) & "\folder.jpg") Then
                posterpath = IO.Path.GetDirectoryName(FullPath) & "\folder.jpg" 'where movie-per-folder may use folder.jpg
            Else
                posterpath = FullPath.Replace(IO.Path.GetExtension(FullPath), ".tbn")
            End If
        End If
        Return posterpath

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
                    workingfiledetails.filedetails_video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, 61)
                ElseIf playlist.Count > 1 Then
                    Dim totalmins As Integer = 0
                    For f = 0 To playlist.Count - 1
                        Dim M2 As mediainfo
                        M2 = New mediainfo
                        M2.Open(playlist(f))
                        Dim temptime As String = M2.Get_(StreamKind.Visual, 0, 61)
                        Dim tempint As Integer
                        If temptime <> Nothing Then
                            Try
                                '1h 24mn 48s 546ms
                                Dim hours As Integer = 0
                                Dim minutes As Integer = 0
                                Dim tempstring2 As String = temptime
                                tempint = tempstring2.IndexOf("h")
                                If tempint <> -1 Then
                                    hours = Convert.ToInt32(tempstring2.Substring(0, tempint))
                                    tempstring2 = tempstring2.Substring(tempint + 1, tempstring2.Length - (tempint + 1))
                                    tempstring2 = Trim(tempstring2)
                                End If
                                tempint = tempstring2.IndexOf("mn")
                                If tempint <> -1 Then
                                    minutes = Convert.ToInt32(tempstring2.Substring(0, tempint))
                                End If
                                If hours <> 0 Then
                                    hours = hours * 60
                                End If
                                minutes = minutes + hours
                                totalmins = totalmins + minutes
                            Catch
                            End Try
                        End If
                    Next
                    workingfiledetails.filedetails_video.DurationInSeconds.Value = totalmins & " min"
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
