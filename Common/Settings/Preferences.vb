Imports System.IO
Imports System.Xml
Imports System.Threading



Public Class Preferences
    Const SetDefaults = True
    'Not saved items

    Public Shared tv_RegexScraper As New List(Of String)
    Public Shared tv_RegexRename As New List(Of String)

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

    'Saved items
    Public Shared customcounter As Integer
    Public Shared tvrename As Integer
    Public Shared locx As Integer
    Public Shared locy As Integer
    Public Shared maxactors As Integer
    Public Shared maxmoviegenre As Integer
    Public Shared rarsize As Integer
    Public Shared splt1 As Integer
    Public Shared splt2 As Integer
    Public Shared splt3 As Integer
    Public Shared splt4 As Integer
    Public Shared splt5 As Integer
    Public Shared resizefanart As Integer
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
    Public Shared XBMC_Scraper As String = ""
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

    Public Shared moviesortorder As Byte
    Public Shared moviedefaultlist As Byte
    Public Shared startuptab As Byte

    Public Shared moviesets As New List(Of String)
    Public Shared tableview As New List(Of String)
    Public Shared offlinefolders As New List(Of String)

    Public Shared logview As Integer

    Public Shared tvRootFolders As New List(Of String)

    Public Shared movieFolders As New List(Of String)
    Public Shared tvFolders As New List(Of String)


    Public Shared profiles As New List(Of str_ListOfProfiles)
    Public Shared workingProfile As New str_ListOfProfiles(SetDefaults)
    Public Shared commandlist As New List(Of str_ListOfCommands)

    Public Shared whatXBMCScraperIMBD As Boolean
    Public Shared whatXBMCScraperTVDB As Boolean
    Public Shared OfflineDVDTitle As String
    Public Shared MovieRenameEnable As Boolean
    Public Shared MovieRenameTemplate As String
    Public Shared moviePreferredTrailerResolution As String
    Public Shared MovieImdbGenreRegEx As String

    Public Shared applicationDatapath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Media Companion\"
    Public Shared Sub SetUpPreferences()
        'General
        Preferences.ignorearticle = False
        Preferences.externalbrowser = False
        Preferences.videoplaybackmode = "1"
        Preferences.backgroundcolour = "Silver"
        Preferences.forgroundcolour = "#D3D9DC"
        Preferences.formheight = "600"
        Preferences.formwidth = "800"
        Preferences.disablelogfiles = False
        Preferences.startupCache = True
        Preferences.rarsize = True = "8"
        Preferences.renamenfofiles = True
        Preferences.checkinfofiles = True
        Preferences.scrapemovieposters = True
        Preferences.dontdisplayposter = False
        Preferences.usetransparency = False 'not used in gen2
        Preferences.transparencyvalue = 255 'not used in gen2
        Preferences.lastpath = applicationPath ' Application.StartupPath
        Preferences.videomode = 1
        Preferences.locx = 0
        Preferences.locy = 0
        Preferences.formheight = 725
        Preferences.formwidth = 1060
        Preferences.splt5 = 0
        Preferences.showsortdate = False

        'Movies
        Preferences.movies_useXBMC_Scraper = False
        Preferences.moviedefaultlist = 0
        Preferences.moviesortorder = 0
        Preferences.imdbmirror = "http://www.imdb.com/"
        Preferences.usefoldernames = False
        Preferences.allfolders = False
        ReDim Preferences.moviethumbpriority(3)
        Preferences.maxmoviegenre = 99
        Preferences.moviethumbpriority(0) = "Internet Movie Poster Awards"
        Preferences.moviethumbpriority(1) = "themoviedb.org"
        Preferences.moviethumbpriority(2) = "Movie Poster DB"
        Preferences.moviethumbpriority(3) = "IMDB"
        Preferences.movieRuntimeDisplay = "scraper"
        Preferences.moviePreferredTrailerResolution = "720"
        Preferences.MovieRenameEnable = False
        Preferences.MovieRenameTemplate = "%T (%Y)"
        Preferences.MovieImdbGenreRegEx = "/genre/.*?>(?<genre>.*?)</a>"
            

        'TV
        Preferences.tvshow_useXBMC_Scraper = False
        Preferences.autorenameepisodes = False
        Preferences.autoepisodescreenshot = False
        Preferences.tvshowautoquick = False
        Preferences.copytvactorthumbs = True
        Preferences.enabletvhdtags = True
        Preferences.tvshowrefreshlog = False
        Preferences.seasonall = "none"
        Preferences.tvrename = 0
        Preferences.tvfanart = True
        Preferences.tvposter = True
        Preferences.postertype = "poster"
        Preferences.downloadtvseasonthumbs = True
        Preferences.TvdbLanguage = "English"
        Preferences.TvdbLanguageCode = "en"
        Preferences.sortorder = "default"
        Preferences.tvdbactorscrape = 0
        Preferences.defaulttvthumb = "poster"
        Preferences.OfflineDVDTitle = "Please Load '%T' Media To Play..."
        Preferences.fixnfoid = False
        Preferences.logview = "0"  'first entry in combobox is 'Full' (log view)
        Preferences.displayMissingEpisodes = False

        'Unknown - need to be sorted/named better
        Preferences.eprenamelowercase = False
        Preferences.intruntime = False
        Preferences.actorseasy = True
        Preferences.startuptab = 0
        Preferences.font = "Times New Roman, 9pt"
        Preferences.fanartnotstacked = False
        Preferences.posternotstacked = False
        Preferences.ignoreactorthumbs = False
        Preferences.actorsave = False
        Preferences.actorsavepath = ""
        Preferences.actornetworkpath = ""
        Preferences.usefanart = True
        Preferences.ignoretrailers = False
        Preferences.keepfoldername = False
        Preferences.enablehdtags = True
        Preferences.savefanart = True
        Preferences.resizefanart = 1
        Preferences.overwritethumbs = False
        Preferences.startupmode = 1
        Preferences.maxactors = 9999
        Preferences.createfolderjpg = False
        Preferences.basicsavemode = False               'movie.nfo, movie.tbn, fanart.jpg
        Preferences.namemode = "1"
        Preferences.maximumthumbs = 10
        Preferences.gettrailer = False
        ReDim Preferences.certificatepriority(33)
        Preferences.certificatepriority(0) = "MPAA"
        Preferences.certificatepriority(1) = "UK"
        Preferences.certificatepriority(2) = "USA"
        Preferences.certificatepriority(3) = "Ireland"
        Preferences.certificatepriority(4) = "Australia"
        Preferences.certificatepriority(5) = "New Zealand"
        Preferences.certificatepriority(6) = "Norway"
        Preferences.certificatepriority(7) = "Singapore"
        Preferences.certificatepriority(8) = "South Korea"
        Preferences.certificatepriority(9) = "Philippines"
        Preferences.certificatepriority(10) = "Brazil"
        Preferences.certificatepriority(11) = "Netherlands"
        Preferences.certificatepriority(12) = "Malaysia"
        Preferences.certificatepriority(13) = "Argentina"
        Preferences.certificatepriority(14) = "Iceland"
        Preferences.certificatepriority(15) = "Canada (Quebec)"
        Preferences.certificatepriority(16) = "Canada (British Columbia/Ontario)"
        Preferences.certificatepriority(17) = "Canada (Alberta/Manitoba/Nova Scotia)"
        Preferences.certificatepriority(18) = "Peru"
        Preferences.certificatepriority(19) = "Sweden"
        Preferences.certificatepriority(20) = "Portugal"
        Preferences.certificatepriority(21) = "South Africa"
        Preferences.certificatepriority(22) = "Denmark"
        Preferences.certificatepriority(23) = "Hong Kong"
        Preferences.certificatepriority(24) = "Finland"
        Preferences.certificatepriority(25) = "India"
        Preferences.certificatepriority(26) = "Mexico"
        Preferences.certificatepriority(27) = "France"
        Preferences.certificatepriority(28) = "Italy"
        Preferences.certificatepriority(29) = "Switzerland (canton of Vaud)"
        Preferences.certificatepriority(30) = "Switzerland (canton of Geneva)"
        Preferences.certificatepriority(31) = "Germany"
        Preferences.certificatepriority(32) = "Greece"
        Preferences.certificatepriority(33) = "Austria"
        Preferences.maximagecount = 10
        ReDim Preferences.releaseformat(12)
        Preferences.releaseformat(0) = "Cam"
        Preferences.releaseformat(1) = "Telesync"
        Preferences.releaseformat(2) = "Workprint"
        Preferences.releaseformat(3) = "Telecine"
        Preferences.releaseformat(4) = "Pay-Per-View Rip"
        Preferences.releaseformat(5) = "Screener"
        Preferences.releaseformat(6) = "R5"
        Preferences.releaseformat(7) = "DVD-Rip"
        Preferences.releaseformat(8) = "DVD-R"
        Preferences.releaseformat(9) = "HDTV"
        Preferences.releaseformat(10) = "VODRip"
        Preferences.releaseformat(11) = "BRRip"
        Preferences.releaseformat(12) = "BDRip"

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


        For Each path In Preferences.tvFolders
            child = doc.CreateElement("tvfolder")
            child.InnerText = path
            root.AppendChild(child)
        Next

        For Each path In Preferences.tvRootFolders
            child = doc.CreateElement("tvrootfolder")
            child.InnerText = path
            root.AppendChild(child)
        Next

        For Each path In Preferences.movieFolders
            child = doc.CreateElement("nfofolder")
            child.InnerText = path
            root.AppendChild(child)
        Next
        Dim list As New List(Of String)
        For Each path In Preferences.offlinefolders
            If Not list.Contains(path) Then
                child = doc.CreateElement("offlinefolder")
                child.InnerText = path
                root.AppendChild(child)
                list.Add(path)
            End If
        Next


        child = doc.CreateElement("gettrailer")
        child.InnerText = Preferences.gettrailer.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("keepfoldername")
        child.InnerText = Preferences.keepfoldername.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("startupcache")
        child.InnerText = Preferences.startupCache.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("ignoretrailers")
        child.InnerText = Preferences.ignoretrailers.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviescraper")
        child.InnerText = Preferences.moviescraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("nfoposterscraper")
        child.InnerText = Preferences.nfoposterscraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("alwaysuseimdbid")
        child.InnerText = Preferences.alwaysuseimdbid.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignoreactorthumbs")
        child.InnerText = Preferences.ignoreactorthumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("maxactors")
        child.InnerText = Preferences.maxactors.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("maxmoviegenre")
        child.InnerText = Preferences.maxmoviegenre.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("enablehdtags")
        child.InnerText = Preferences.enablehdtags.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("movieruntimedisplay")
        child.InnerText = Preferences.movieRuntimeDisplay
        root.AppendChild(child)


        child = doc.CreateElement("renamenfofiles")
        child.InnerText = Preferences.renamenfofiles.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("checkinfofiles")
        child.InnerText = Preferences.checkinfofiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("tvshowautoquick")
        child.InnerText = Preferences.tvshowautoquick.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("disablelogfiles")
        child.InnerText = Preferences.disablelogfiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("actorseasy")
        child.InnerText = Preferences.actorseasy.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("copytvactorthumbs")
        child.InnerText = Preferences.copytvactorthumbs.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("displayMissingEpisodes")
        child.InnerText = Preferences.displayMissingEpisodes.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("fanartnotstacked")
        child.InnerText = Preferences.fanartnotstacked.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("posternotstacked")
        child.InnerText = Preferences.posternotstacked.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("lastpath")
        child.InnerText = Preferences.lastpath
        root.AppendChild(child)

        child = doc.CreateElement("downloadfanart")
        child.InnerText = Preferences.savefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("scrapemovieposters")
        child.InnerText = Preferences.scrapemovieposters.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("usefanart")
        child.InnerText = Preferences.usefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("dontdisplayposter")
        child.InnerText = Preferences.dontdisplayposter.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("usefoldernames")
        child.InnerText = Preferences.usefoldernames.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("allfolders")
        child.InnerText = Preferences.allfolders.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("rarsize")
        child.InnerText = Preferences.rarsize.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("actorsave")
        child.InnerText = Preferences.actorsave.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("actorsavepath")
        child.InnerText = Preferences.actorsavepath
        root.AppendChild(child)


        child = doc.CreateElement("actornetworkpath")
        child.InnerText = Preferences.actornetworkpath
        root.AppendChild(child)


        child = doc.CreateElement("resizefanart")
        child.InnerText = Preferences.resizefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("overwritethumbs")
        child.InnerText = Preferences.overwritethumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("defaulttvthumb")
        child.InnerText = Preferences.defaulttvthumb
        root.AppendChild(child)


        child = doc.CreateElement("imdbmirror")
        child.InnerText = Preferences.imdbmirror
        root.AppendChild(child)


        child = doc.CreateElement("backgroundcolour")
        child.InnerText = Preferences.backgroundcolour
        root.AppendChild(child)


        child = doc.CreateElement("forgroundcolour")
        child.InnerText = Preferences.forgroundcolour
        root.AppendChild(child)


        child = doc.CreateElement("remembersize")
        child.InnerText = Preferences.remembersize.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("locx")
        child.InnerText = Preferences.locx.ToString
        root.AppendChild(child)

        child = doc.CreateElement("locy")
        child.InnerText = Preferences.locy.ToString
        root.AppendChild(child)


        child = doc.CreateElement("formheight")
        child.InnerText = Preferences.formheight.ToString
        root.AppendChild(child)


        child = doc.CreateElement("formwidth")
        child.InnerText = Preferences.formwidth.ToString
        root.AppendChild(child)


        child = doc.CreateElement("videoplaybackmode")
        child.InnerText = Preferences.videoplaybackmode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("createfolderjpg")
        child.InnerText = Preferences.createfolderjpg.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("basicsavemode")
        child.InnerText = Preferences.basicsavemode.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("startupdisplaynamemode")
        child.InnerText = Preferences.startupdisplaynamemode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("namemode")
        child.InnerText = Preferences.namemode
        root.AppendChild(child)


        child = doc.CreateElement("tvdbmode")
        child.InnerText = Preferences.sortorder
        root.AppendChild(child)


        child = doc.CreateElement("tvdbactorscrape")
        child.InnerText = Preferences.tvdbactorscrape.ToString
        root.AppendChild(child)


        child = doc.CreateElement("usetransparency")
        child.InnerText = Preferences.usetransparency.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("transparencyvalue")
        child.InnerText = Preferences.transparencyvalue.ToString
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvfanart")
        child.InnerText = Preferences.tvfanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvposter")
        child.InnerText = Preferences.tvposter.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("keepfoldername")
        child.InnerText = Preferences.keepfoldername.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvseasonthumbs")
        child.InnerText = Preferences.downloadtvseasonthumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("maximumthumbs")
        child.InnerText = Preferences.maximumthumbs.ToString
        root.AppendChild(child)


        child = doc.CreateElement("startupmode")
        child.InnerText = Preferences.startupmode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("hdtvtags")
        child.InnerText = Preferences.enabletvhdtags.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("disablelogs")
        child.InnerText = Preferences.disablelogfiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("disabletvlogs")
        child.InnerText = Preferences.disabletvlogs.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("overwritethumb")
        child.InnerText = Preferences.overwritethumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("savefanart")
        child.InnerText = Preferences.savefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("postertype")
        child.InnerText = Preferences.postertype
        root.AppendChild(child)

        child = doc.CreateElement("roundminutes")
        child.InnerText = Preferences.roundminutes.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("tvactorscrape")
        child.InnerText = Preferences.tvdbactorscrape.ToString
        root.AppendChild(child)


        child = doc.CreateElement("videomode")
        child.InnerText = Preferences.videomode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("selectedvideoplayer")
        child.InnerText = Preferences.selectedvideoplayer
        root.AppendChild(child)

        child = doc.CreateElement("externalbrowser")
        child.InnerText = Preferences.externalbrowser.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignoreparts")
        child.InnerText = Preferences.movieignorepart.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("cleantags")
        child.InnerText = Preferences.moviecleanTags
        root.AppendChild(child)


        child = doc.CreateElement("moviethumbpriority")
        Dim tempstring As String = ""
        If Preferences.moviethumbpriority IsNot Nothing AndAlso Preferences.moviethumbpriority.Length > 2 Then
            tempstring = Preferences.moviethumbpriority(0) & "|" & Preferences.moviethumbpriority(1) & "|" & Preferences.moviethumbpriority(2) & "|" & Preferences.moviethumbpriority(3)
        End If
        child.InnerText = tempstring
        root.AppendChild(child)


        child = doc.CreateElement("releaseformat")
        tempstring = ""
        If Preferences.releaseformat IsNot Nothing Then
            For f = 0 To Preferences.releaseformat.Length - 1
                tempstring = tempstring & Preferences.releaseformat(f) & "|"
            Next
        End If
        child.InnerText = tempstring.TrimEnd("|")
        root.AppendChild(child)


        child = doc.CreateElement("tvdblanguage")
        tempstring = ""
        tempstring = Preferences.TvdbLanguageCode & "|" & Preferences.TvdbLanguage
        child.InnerText = tempstring
        root.AppendChild(child)



        child = doc.CreateElement("certificatepriority")
        tempstring = ""
        If Preferences.certificatepriority IsNot Nothing Then
            For f = 0 To Preferences.certificatepriority.Length - 1
                tempstring = tempstring & Preferences.certificatepriority(f) & "|"
            Next
        End If
        child.InnerText = tempstring.TrimEnd("|")
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer1")
        child.InnerText = Preferences.splt1.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer2")
        child.InnerText = Preferences.splt2.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer3")
        child.InnerText = Preferences.splt3.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer4")
        child.InnerText = Preferences.splt4.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer5")
        child.InnerText = Preferences.splt5.ToString
        root.AppendChild(child)


        child = doc.CreateElement("seasonall")
        child.InnerText = Preferences.seasonall
        root.AppendChild(child)

        child = doc.CreateElement("maximised")
        If Preferences.maximised = True Then
            child.InnerText = "true"
        Else
            child.InnerText = "false"
        End If
        root.AppendChild(child)

        child = doc.CreateElement("tvrename")
        child.InnerText = Preferences.tvrename.ToString
        root.AppendChild(child)

        child = doc.CreateElement("eprenamelowercase")
        child.InnerText = Preferences.eprenamelowercase.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("tvshowrefreshlog")
        child.InnerText = Preferences.tvshowrefreshlog.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesortorder")
        child.InnerText = Preferences.moviesortorder.ToString
        root.AppendChild(child)

        child = doc.CreateElement("moviedefaultlist")
        child.InnerText = Preferences.moviedefaultlist.ToString
        root.AppendChild(child)

        child = doc.CreateElement("autoepisodescreenshot")
        child.InnerText = Preferences.autoepisodescreenshot.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignorearticle")
        child.InnerText = Preferences.ignorearticle.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("TVShowUseXBMCScraper")
        child.InnerText = Preferences.tvshow_useXBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesUseXBMCScraper")
        child.InnerText = Preferences.movies_useXBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("whatXBMCScraper")
        child.InnerText = Preferences.XBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("startuptab")
        child.InnerText = Preferences.startuptab.ToString
        root.AppendChild(child)

        child = doc.CreateElement("intruntime")
        child.InnerText = Preferences.intruntime.ToString.ToLower
        root.AppendChild(child)

        If Preferences.font <> Nothing Then
            If Preferences.font <> "" Then
                child = doc.CreateElement("font")
                child.InnerText = Preferences.font
                root.AppendChild(child)
            End If
        End If

        child = doc.CreateElement("autorenameepisodes")
        child.InnerText = Preferences.autorenameepisodes.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("scrapefullcert")
        child.InnerText = Preferences.scrapefullcert.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesets")
        Dim childchild As XmlElement
        For Each movieset In Preferences.moviesets
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
        childchild2.InnerText = Preferences.tablesortorder
        child.AppendChild(childchild2)
        For Each tabs In Preferences.tableview
            childchild2 = doc.CreateElement("tab")
            childchild2.InnerText = tabs
            child.AppendChild(childchild2)
        Next
        root.AppendChild(child)

        child = doc.CreateElement("offlinemovielabeltext")
        child.InnerText = Preferences.OfflineDVDTitle
        root.AppendChild(child)



        root.AppendChild(child)
        child = doc.CreateElement("movierenameenable")
        If Preferences.MovieRenameEnable = True Then
            child.InnerText = "true"
        Else
            child.InnerText = "false"
        End If
        root.AppendChild(child)


        child = doc.CreateElement("movierenametemplate")
        child.InnerText = Preferences.MovieRenameTemplate
        root.AppendChild(child)



        child = doc.CreateElement("showsortdate")
        child.InnerText = Preferences.showsortdate
        root.AppendChild(child)

        child = doc.CreateElement("logview")
        child.InnerText = Preferences.logview
        root.AppendChild(child)

        For Each com In Preferences.commandlist
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
        child.InnerText = Preferences.moviePreferredTrailerResolution.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("MovieImdbGenreRegEx")
        child.InnerText = Preferences.MovieImdbGenreRegEx.ToString
        root.AppendChild(child)

        
        doc.AppendChild(root)

        If String.IsNullOrEmpty(Preferences.workingProfile.config) Then
            Preferences.workingProfile.config = IO.Path.Combine(Preferences.applicationPath, "settings\config.xml")
        End If
        Dim output As New XmlTextWriter(Preferences.workingProfile.config, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
        'Catch ex As Exception
        '    MsgBox("Can't find the following path..." & vbCrLf & Preferences.workingProfile.config & vbCrLf & "Please Check/Delete Settings Folder & Restart MC")
        'End Try
    End Sub



    Public Shared Sub LoadConfig()
        Preferences.commandlist.Clear()
        Preferences.moviesets.Clear()
        Preferences.moviesets.Add("-None-")
        Preferences.movieFolders.Clear()
        Preferences.offlinefolders.Clear()
        Preferences.tvFolders.Clear()
        Preferences.tvRootFolders.Clear()
        Preferences.tableview.Clear()


        Dim tempstring As String = Preferences.workingProfile.config
        If Not IO.File.Exists(Preferences.workingProfile.config) Then
            Exit Sub
        End If

        Dim prefs As New XmlDocument
        Try
            prefs.Load(Preferences.workingProfile.config)
        Catch ex As Exception
            MsgBox("Error : pr24")
        End Try

        Dim thisresult As XmlNode = Nothing

        For Each thisresult In prefs("xbmc_media_companion_config_v1.0")
            'Try
            Select Case thisresult.Name
                Case "moviesets"
                    Dim thisset As XmlNode = Nothing
                    For Each thisset In thisresult.ChildNodes
                        Select Case thisset.Name
                            Case "set"
                                Preferences.moviesets.Add(thisset.InnerText)
                        End Select
                    Next
                Case "table"
                    Dim thistable As XmlNode = Nothing
                    For Each thistable In thisresult.ChildNodes
                        Select Case thistable.Name
                            Case "tab"
                                Preferences.tableview.Add(thistable.InnerText)
                            Case "sort"
                                Preferences.tablesortorder = thistable.InnerText
                        End Select
                    Next
                Case "comms"
                    Dim thistable As XmlNode = Nothing
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
                        Preferences.commandlist.Add(newcom)
                    End If
                Case "seasonall"
                    If thisresult.InnerText <> "" Then Preferences.seasonall = thisresult.InnerText
                Case "splitcontainer1"
                    If thisresult.InnerText <> "" Then Preferences.splt1 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer2"
                    If thisresult.InnerText <> "" Then Preferences.splt2 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer3"
                    If thisresult.InnerText <> "" Then Preferences.splt3 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer4"
                    If thisresult.InnerText <> "" Then Preferences.splt4 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer5"
                    If thisresult.InnerText <> "" Then Preferences.splt5 = Convert.ToInt32(thisresult.InnerText)
                Case "maximised"
                    If thisresult.InnerText = "true" Then
                        Preferences.maximised = True
                    Else
                        Preferences.maximised = False
                    End If
                Case "locx"
                    If thisresult.InnerText <> "" Then Preferences.locx = Convert.ToInt32(thisresult.InnerText)
                Case "locy"
                    If thisresult.InnerText <> "" Then Preferences.locy = Convert.ToInt32(thisresult.InnerText)
                Case "nfofolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    Preferences.movieFolders.Add(decodestring)
                Case "offlinefolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    Preferences.offlinefolders.Add(decodestring)
                Case "tvfolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    Preferences.tvFolders.Add(decodestring)
                Case "tvrootfolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    Preferences.tvRootFolders.Add(decodestring)
                Case "gettrailer"
                    If thisresult.InnerXml = "true" Then
                        Preferences.gettrailer = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.gettrailer = False
                    End If
                Case "tvshowautoquick"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvshowautoquick = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvshowautoquick = False
                    End If
                Case "intruntime"
                    If thisresult.InnerXml = "true" Then
                        Preferences.intruntime = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.intruntime = False
                    End If
                Case "keepfoldername"
                    If thisresult.InnerXml = "true" Then
                        Preferences.keepfoldername = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.keepfoldername = False
                    End If

                Case "startupcache"
                    If thisresult.InnerXml = "true" Then
                        Preferences.startupCache = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.startupCache = False
                    End If

                Case "ignoretrailers"
                    If thisresult.InnerXml = "true" Then
                        Preferences.ignoretrailers = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.ignoretrailers = False
                    End If

                Case "ignoreactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.ignoreactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.ignoreactorthumbs = False
                    End If

                Case "font"
                    If thisresult.InnerXml <> Nothing Then
                        If thisresult.InnerXml <> "" Then
                            Preferences.font = thisresult.InnerXml
                        End If
                    End If

                Case "maxactors"
                    If thisresult.InnerText <> "" Then Preferences.maxactors = Convert.ToInt32(thisresult.InnerXml)

                Case "maxmoviegenre"
                    If thisresult.InnerText <> "" Then Preferences.maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)

                Case "enablehdtags"
                    If thisresult.InnerXml = "true" Then
                        Preferences.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.enablehdtags = False
                    End If

                Case "movieruntimedisplay"
                    If thisresult.InnerText <> "" Then Preferences.movieRuntimeDisplay = thisresult.InnerXml

                Case "hdtvtags"
                    If thisresult.InnerXml = "true" Then
                        Preferences.enabletvhdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.enabletvhdtags = False
                    End If

                Case "renamenfofiles"
                    If thisresult.InnerXml = "true" Then
                        Preferences.renamenfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.renamenfofiles = False
                    End If

                Case "checkinfofiles"
                    If thisresult.InnerXml = "true" Then
                        Preferences.checkinfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.checkinfofiles = False
                    End If

                Case "disablelogfiles"
                    If thisresult.InnerXml = "true" Then
                        Preferences.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.disablelogfiles = False
                    End If

                Case "logview"
                    Preferences.logview = thisresult.InnerXml

                Case "fanartnotstacked"
                    If thisresult.InnerXml = "true" Then
                        Preferences.fanartnotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.fanartnotstacked = False
                    End If

                Case "posternotstacked"
                    If thisresult.InnerXml = "true" Then
                        Preferences.posternotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.posternotstacked = False
                    End If

                Case "downloadfanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.savefanart = False
                    End If

                Case "scrapemovieposters"
                    If thisresult.InnerXml = "true" Then
                        Preferences.scrapemovieposters = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.scrapemovieposters = False
                    End If

                Case "usefanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.usefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.usefanart = False
                    End If

                Case "dontdisplayposter"
                    If thisresult.InnerXml = "true" Then
                        Preferences.dontdisplayposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.dontdisplayposter = False
                    End If

                Case "rarsize"
                    If thisresult.InnerText <> "" Then Preferences.rarsize = Convert.ToInt32(thisresult.InnerXml)

                Case "actorsave"
                    If thisresult.InnerXml = "true" Then
                        Preferences.actorsave = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.actorsave = False
                    End If

                Case "actorseasy"
                    If thisresult.InnerXml = "true" Then
                        Preferences.actorseasy = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.actorseasy = False
                    End If

                Case "copytvactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.copytvactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.copytvactorthumbs = False
                    End If

                Case "displayMissingEpisodes"
                    If thisresult.InnerXml = "true" Then
                        Preferences.displayMissingEpisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.displayMissingEpisodes = False
                    End If

                Case "actorsavepath"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    If thisresult.InnerText <> "" Then Preferences.actorsavepath = decodestring

                Case "actornetworkpath"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    If thisresult.InnerText <> "" Then Preferences.actornetworkpath = decodestring

                Case "resizefanart"
                    If thisresult.InnerText <> "" Then Preferences.resizefanart = Convert.ToInt32(thisresult.InnerXml)

                Case "overwritethumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.overwritethumbs = False
                    End If

                Case "defaulttvthumb"
                    If thisresult.InnerText <> "" Then Preferences.defaulttvthumb = thisresult.InnerXml

                Case "imdbmirror"
                    If thisresult.InnerText <> "" Then Preferences.imdbmirror = thisresult.InnerXml

                Case "moviethumbpriority"
                    ReDim Preferences.moviethumbpriority(3)
                    If thisresult.InnerText <> "" Then Preferences.moviethumbpriority = thisresult.InnerXml.Split("|")

                Case "certificatepriority"
                    ReDim Preferences.certificatepriority(33)
                    If thisresult.InnerText <> "" Then Preferences.certificatepriority = thisresult.InnerXml.Split("|")

                Case "releaseformat"
                    If thisresult.InnerText <> "" Then
                        Dim count As Integer = 0
                        Dim index As Integer = 0
                        Do Until index < 0
                            count += 1
                            index = thisresult.InnerText.IndexOf("|"c, index + 1)
                        Loop
                        ReDim Preferences.releaseformat(count)
                        Preferences.releaseformat = thisresult.InnerXml.Split("|")
                    End If

                Case "cleantags"
                    If thisresult.InnerText <> "" Then Preferences.moviecleanTags = thisresult.InnerXml

                Case "ignoreparts"
                    If thisresult.InnerXml = "true" Then
                        Preferences.movieignorepart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.movieignorepart = False
                    End If

                Case "backgroundcolour"
                    If thisresult.InnerText <> "" Then Preferences.backgroundcolour = thisresult.InnerXml

                Case "forgroundcolour"
                    If thisresult.InnerText <> "" Then Preferences.forgroundcolour = thisresult.InnerXml

                Case "remembersize"
                    If thisresult.InnerXml = "true" Then
                        Preferences.remembersize = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.remembersize = False
                    End If

                Case "formheight"
                    If thisresult.InnerText <> "" Then Preferences.formheight = Convert.ToInt32(thisresult.InnerXml)

                Case "formwidth"
                    If thisresult.InnerText <> "" Then Preferences.formwidth = Convert.ToInt32(thisresult.InnerXml)
                Case "videoplaybackmode"
                    If thisresult.InnerText <> "" Then Preferences.videoplaybackmode = Convert.ToInt32(thisresult.InnerXml)

                Case "usefoldernames"
                    If thisresult.InnerXml = "true" Then
                        Preferences.usefoldernames = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.usefoldernames = False
                    End If

                Case "allfolders"
                    If thisresult.InnerXml = "true" Then
                        Preferences.allfolders = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.allfolders = False
                    End If

                Case "createfolderjpg"
                    If thisresult.InnerXml = "true" Then
                        Preferences.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.createfolderjpg = False
                    End If

                Case "basicsavemode"
                    If thisresult.InnerXml = "true" Then
                        Preferences.basicsavemode = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.basicsavemode = False
                    End If

                Case "startupdisplaynamemode"
                    If thisresult.InnerText <> "" Then Preferences.startupdisplaynamemode = Convert.ToInt32(thisresult.InnerXml)

                Case "namemode"
                    If thisresult.InnerText <> "" Then Preferences.namemode = thisresult.InnerXml

                Case "tvdblanguage"
                    Dim partone() As String
                    partone = thisresult.InnerXml.Split("|")
                    For f = 0 To 1
                        If partone(0).Length = 2 Then
                            Preferences.TvdbLanguageCode = partone(0)
                            Preferences.TvdbLanguage = partone(1)
                            Exit For
                        Else
                            Preferences.TvdbLanguageCode = partone(1)
                            Preferences.TvdbLanguage = partone(0)
                        End If
                    Next

                Case "tvdbmode"
                    If thisresult.InnerText <> "" Then Preferences.sortorder = thisresult.InnerXml
                Case "tvdbactorscrape"
                    If thisresult.InnerText <> "" Then Preferences.TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)

                Case "usetransparency"
                    If thisresult.InnerXml = "true" Then
                        Preferences.usetransparency = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.usetransparency = False
                    End If

                Case "transparencyvalue"
                    If thisresult.InnerText <> "" Then Preferences.transparencyvalue = Convert.ToInt32(thisresult.InnerXml)

                Case "downloadtvfanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvfanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvfanart = False
                    End If

                Case "roundminutes"
                    If thisresult.InnerXml = "true" Then
                        Preferences.roundminutes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.roundminutes = False
                    End If

                Case "autoepisodescreenshot"
                    If thisresult.InnerXml = "true" Then
                        Preferences.autoepisodescreenshot = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.autoepisodescreenshot = False
                    End If

                Case "ignorearticle"
                    If thisresult.InnerXml = "true" Then
                        Preferences.ignorearticle = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.ignorearticle = False
                    End If

                Case "TVShowUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvshow_useXBMC_Scraper = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvshow_useXBMC_Scraper = False
                    End If

                Case "moviesUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Preferences.movies_useXBMC_Scraper = True

                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.movies_useXBMC_Scraper = False
                    End If
                Case "whatXBMCScraper"
                    Preferences.XBMC_Scraper = thisresult.InnerXml
                    If thisresult.InnerXml = "imdb" Then
                        Preferences.whatXBMCScraperIMBD = True

                    ElseIf thisresult.InnerXml = "tmdb" Then
                        Preferences.whatXBMCScraperTVDB = True
                    End If
                Case "downloadtvposter"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvposter = False
                    End If

                Case "downloadtvseasonthumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.downloadtvseasonthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.downloadtvseasonthumbs = False
                    End If

                Case "maximumthumbs"
                    If thisresult.InnerText <> "" Then Preferences.maximumthumbs = Convert.ToInt32(thisresult.InnerXml)

                Case "startupmode"
                    If thisresult.InnerText <> "" Then Preferences.startupmode = Convert.ToInt32(thisresult.InnerXml)

                Case "hdtags"
                    If thisresult.InnerXml = "true" Then
                        Preferences.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.enablehdtags = False
                    End If

                Case "disablelogs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.disablelogfiles = False
                    End If

                Case "disabletvlogs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.disabletvlogs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.disabletvlogs = False
                    End If

                Case "overwritethumb"
                    If thisresult.InnerXml = "true" Then
                        Preferences.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.overwritethumbs = False
                    End If

                Case "folderjpg"
                    If thisresult.InnerXml = "true" Then
                        Preferences.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.createfolderjpg = False
                    End If

                Case "savefanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.savefanart = False
                    End If

                Case "postertype"
                    If thisresult.InnerText <> "" Then Preferences.postertype = thisresult.InnerXml

                Case "tvactorscrape"
                    If thisresult.InnerText <> "" Then Preferences.TvdbActorScrape = Convert.ToInt32(thisresult.InnerXml)

                Case "videomode"
                    If thisresult.InnerText <> "" Then Preferences.videomode = Convert.ToInt32(thisresult.InnerXml)

                Case "selectedvideoplayer"
                    If thisresult.InnerText <> "" Then Preferences.selectedvideoplayer = thisresult.InnerXml

                Case "maximagecount"
                    If thisresult.InnerText <> "" Then Preferences.maximagecount = Convert.ToInt32(thisresult.InnerXml)

                Case "lastpath"
                    If thisresult.InnerText <> "" Then Preferences.lastpath = thisresult.InnerXml

                Case "moviescraper"
                    If thisresult.InnerText <> "" Then Preferences.moviescraper = thisresult.InnerXml

                Case "nfoposterscraper"
                    If thisresult.InnerText <> "" Then Preferences.nfoposterscraper = thisresult.InnerXml

                Case "alwaysuseimdbid"
                    If thisresult.InnerXml = "true" Then
                        Preferences.alwaysuseimdbid = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.alwaysuseimdbid = False
                    End If

                Case "externalbrowser"
                    If thisresult.InnerXml = "true" Then
                        Preferences.externalbrowser = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.externalbrowser = False
                    End If
                Case "tvrename"
                    If thisresult.InnerText <> "" Then Preferences.tvrename = Convert.ToInt32(thisresult.InnerText)
                Case "tvshowrefreshlog"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvshowrefreshlog = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvshowrefreshlog = False
                    End If
                    'public shared moviesortorder As Byte
                    'public shared moviedefaultlist As Byte
                    'public shared lasttab As Byte
                Case "autorenameepisodes"
                    If thisresult.InnerXml = "true" Then
                        Preferences.autorenameepisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.autorenameepisodes = False
                    End If
                Case "eprenamelowercase"
                    If thisresult.InnerXml = "true" Then
                        Preferences.eprenamelowercase = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.eprenamelowercase = False
                    End If
                Case "moviesortorder"
                    If thisresult.InnerText <> "" Then Preferences.moviesortorder = Convert.ToByte(thisresult.InnerText)
                Case "moviedefaultlist"
                    If thisresult.InnerText <> "" Then Preferences.moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                Case "startuptab"
                    If thisresult.InnerText <> "" Then Preferences.startuptab = Convert.ToByte(thisresult.InnerText)

                Case "offlinemovielabeltext"
                    If thisresult.InnerText <> "" Then Preferences.OfflineDVDTitle = thisresult.InnerText

                Case "movierenameenable"
                    If thisresult.InnerXml = "true" Then
                        Preferences.MovieRenameEnable = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.MovieRenameEnable = False
                    End If

                Case "movierenametemplate"
                    If thisresult.InnerText <> "" Then Preferences.MovieRenameTemplate = thisresult.InnerText

                Case "showsortdate"
                    If thisresult.InnerText = Nothing Or thisresult.InnerText = "" Then
                        showsortdate = False
                    Else
                        showsortdate = thisresult.InnerText
                    End If

                Case "scrapefullcert"
                    If thisresult.InnerXml = "true" Then
                        Preferences.scrapefullcert = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.scrapefullcert = False
                    End If

                Case "moviePreferredHDTrailerResolution"
                    If thisresult.InnerText <> "" Then Preferences.moviePreferredTrailerResolution = thisresult.InnerXml

                Case "MovieImdbGenreRegEx"
                    If thisresult.InnerText <> "" Then Preferences.MovieImdbGenreRegEx = decxmlchars(thisresult.InnerXml)


            End Select
            'Catch
            '    'MsgBox("Error : pr278")
            'End Try
        Next

    End Sub

    Public Shared Function decxmlchars(ByVal line As String)

        Try
            line = line.Replace("&amp;", "&")
            line = line.Replace("&lt;", "<")
            line = line.Replace("&gt;", ">")
            line = line.Replace("&quot;", "Chr(34)")
            line = line.Replace("&apos;", "'")
            line = line.Replace("&#xA;", vbCrLf)
            Return line
        Catch
        Finally
        End Try
        Return "Error"
    End Function

    Public Shared Function GetPosterPath(ByVal FullPath As String) As String
        Dim posterpath As String = ""
        posterpath = FullPath.Substring(0, FullPath.Length - 4)
        posterpath = posterpath & ".tbn"
        'If Not IO.File.Exists(posterpath) Then

        '********************** disabled this section poster should include stackname *******************
        'Dim stackname As String = Utilities.GetStackName(IO.Path.GetFileName(FullPath), FullPath.Replace(IO.Path.GetFileName(FullPath), ""))
        'stackname = FullPath.Replace(IO.Path.GetFileName(FullPath), stackname)
        'stackname = stackname & ".tbn"
        'If stackname <> "na" And IO.File.Exists(stackname) Then
        '    posterpath = stackname
        'Else
        '    posterpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "")
        '    posterpath = posterpath & "movie.tbn"
        '    If Not IO.File.Exists(posterpath) Then
        '        posterpath = ""
        '    End If
        'End If
        '***********************end disabled section **************************************

        '    Else
        'posterpath = fullpath.Replace("movie.nfo", "movie.tbn")
        'End If
        If posterpath = "" Then
            If FullPath.IndexOf("movie.nfo") <> -1 Then
                posterpath = FullPath.Replace("movie.nfo", "movie.tbn")
            End If
        End If
        If posterpath = "" Then
            If Preferences.posternotstacked = True Then
                posterpath = FullPath.Substring(0, FullPath.Length - 4) & ".tbn"
            Else
                posterpath = Utilities.GetStackName(IO.Path.GetFileName(FullPath), posterpath.Replace(IO.Path.GetFileName(FullPath), "")) & ".tbn"
                If posterpath = "na.tbn" Then
                    posterpath = FullPath.Substring(0, FullPath.Length - 4) & ".tbn"
                Else
                    posterpath = FullPath.Replace(IO.Path.GetFileName(FullPath), posterpath)
                End If
            End If
            If Preferences.basicsavemode = True Then
                posterpath = posterpath.Replace(IO.Path.GetFileName(FullPath), "movie.tbn")
            End If
        End If
        If posterpath = "na" Then
            If IO.File.Exists(FullPath.Replace(IO.Path.GetFileName(FullPath), "folder.jpg")) Then
                posterpath = FullPath.Replace(IO.Path.GetFileName(FullPath), "folder.jpg")
            End If
        End If
        Return posterpath

    End Function

    Public Shared Function GetFanartPath(ByVal FullPath As String) As String
        'fanart = fanart.jpg if basicsave mode on i.e. movie.nfo,movie.tbn,fanart.jpg OR
        'fanart = movie filename - extension + "-fanart.jpg"
        'NOTE parts need to be included in the name & not stripped off

        'If you edit this, please also change getfanart() in Module1 (mc_com.exe)

        Try
            Dim posterpath As String = ""
            If FullPath.IndexOf("movie.nfo") <> -1 Then   'equivalent to If preferences.basicsavemode = True Then but prefrences is not referenced here
                posterpath = FullPath.Replace("movie.nfo", "fanart.jpg")
            Else
                posterpath = FullPath.Substring(0, FullPath.Length - 4) & "-fanart.jpg"
            End If

            Return posterpath
        Catch
            Return ""
        End Try

        '''''''''''''''''''OLD CODE''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'posterpath = FullPath.Substring(0, FullPath.Length - 4)
        'posterpath = posterpath & "-fanart.jpg"

        '********************** disabled this section fanart should include stackname *******************
        'If Not IO.File.Exists(posterpath) Then
        '    Dim stackname As String = Utilities.GetStackName(IO.Path.GetFileName(FullPath), FullPath.Replace(IO.Path.GetFileName(FullPath), ""))

        '    stackname = FullPath.Replace(IO.Path.GetFileName(FullPath), stackname)
        '    stackname = stackname & "-fanart.jpg"
        '    If stackname <> "na" And IO.File.Exists(stackname) Then
        '        posterpath = stackname
        '    Else
        '        posterpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "")
        '        posterpath = posterpath & "fanart.jpg"
        '        If Not IO.File.Exists(posterpath) Then
        '            posterpath = ""
        '        End If
        '    End If
        '    'Else
        '    '    posterpath = fullpath.Replace("movie.nfo", "movie.tbn")
        'End If
        '***********************end disabled section **************************************

        'If posterpath = "" Then
        '    If Preferences.fanartnotstacked = True Then
        '        posterpath = FullPath.Substring(0, FullPath.Length - 4) & "-fanart.jpg"
        '    Else
        '        posterpath = Utilities.GetStackName(IO.Path.GetFileName(FullPath), FullPath) & "-fanart.jpg"
        '        If posterpath = "na-fanart.jpg" Then
        '            posterpath = FullPath.Substring(0, FullPath.Length - 4) & "-fanart.jpg"
        '        Else
        '            posterpath = FullPath.Replace(IO.Path.GetFileName(FullPath), posterpath)
        '        End If
        '    End If
        '    If Preferences.basicsavemode = True Then
        '        posterpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "fanart.jpg")
        '    End If
        'End If


    End Function

    Public Shared Function GetActorThumbPath(Optional ByVal location As String = "")
        Dim actualpath As String = ""
        Try
            If location = Nothing Then
                Return "none"
                Exit Function
            End If
            If location = "" Then
                Return "none"
                Exit Function
            End If

            If location.IndexOf("http") <> -1 Then
                Return location
                Exit Function
            Else
                If location.IndexOf(Preferences.actornetworkpath) <> -1 Then
                    If Preferences.actornetworkpath <> Nothing And Preferences.actorsavepath <> Nothing Then
                        If Preferences.actornetworkpath <> "" And Preferences.actorsavepath <> "" Then
                            Dim filename As String = IO.Path.GetFileName(location)
                            actualpath = IO.Path.Combine(Preferences.actorsavepath, filename)
                            If Not IO.File.Exists(actualpath) Then
                                Dim extension As String = IO.Path.GetExtension(location)
                                Dim purename As String = IO.Path.GetFileName(location)
                                purename = purename.Replace(extension, "")
                                actualpath = Preferences.actorsavepath & "\" & purename.Substring(purename.Length - 2, 2) & "\" & filename
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
