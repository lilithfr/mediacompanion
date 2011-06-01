
Public Class Structures
    Public customcounter As Integer
    Public tvrename As Integer
    Public locx As Integer
    Public locy As Integer
    Public maxactors As Integer
    Public maxmoviegenre As Integer
    Public rarsize As Integer
    Public splt1 As Integer
    Public splt2 As Integer
    Public splt3 As Integer
    Public splt4 As Integer
    Public splt5 As Integer
    Public resizefanart As Integer
    Public formheight As Integer
    Public formwidth As Integer
    Public videoplaybackmode As Integer
    Public startupdisplaynamemode As Integer
    Public tvdbactorscrape As Integer
    Public transparencyvalue As Integer
    Public maximumthumbs As Integer
    Public startupmode As Integer
    Public videomode As Integer ' = 3
    Public maximagecount As Integer
    Public moviescraper As Integer
    Public nfoposterscraper As Integer

    'Dim tvdbmode As String
    Public XBMC_Scraper As String = ""
    Public font As String
    Public moviethumbpriority() As String
    Public certificatepriority() As String
    Public actorsavepath As String
    Public actornetworkpath As String
    Public defaulttvthumb As String
    Public imdbmirror As String
    Public backgroundcolour As String
    Public forgroundcolour As String
    Public namemode As String
    Public tvdblanguage As String
    Public tvdblanguagecode As String
    Public configpath As String
    Public postertype As String
    Public sortorder As String
    Public selectedvideoplayer As String
    Public lastpath As String
    Public episodeacrorsource As String
    Public seasonall As String
    Public tablesortorder As String

    Public intruntime As Boolean
    Public autorenameepisodes As Boolean
    Public autoepisodescreenshot As Boolean
    Public ignorearticle As Boolean
    Public tvshow_useXBMC_Scraper As Boolean
    Public movies_useXBMC_Scraper As Boolean
    Public eprenamelowercase As Boolean
    Public tvshowautoquick As Boolean
    Public tvshowrebuildlog As Boolean
    Public roundminutes As Boolean
    Public keepfoldername As Boolean
    Public startupCache As Boolean
    Public ignoretrailers As Boolean
    Public ignoreactorthumbs As Boolean
    Public enabletvhdtags As Boolean
    Public enablehdtags As Boolean
    Public renamenfofiles As Boolean
    Public checkinfofiles As Boolean
    Public disablelogfiles As Boolean
    Public fanartnotstacked As Boolean
    Public posternotstacked As Boolean
    Public scrapemovieposters As Boolean
    Public usefanart As Boolean
    Public dontdisplayposter As Boolean
    Public actorsave As Boolean
    Public overwritethumbs As Boolean
    Public remembersize As Boolean
    Public usefoldernames As Boolean
    Public createfolderjpg As Boolean
    Public basicsavemode As Boolean
    Public usetransparency As Boolean
    Public downloadtvseasonthumbs As Boolean
    Public disabletvlogs As Boolean
    Public savefanart As Boolean
    Public tvposter As Boolean
    Public tvfanart As Boolean
    Public alwaysuseimdbid As Boolean
    Public gettrailer As Boolean
    Public externalbrowser As Boolean
    Public maximised As Boolean
    Public copytvactorthumbs As Boolean = False
    Public actorseasy As Boolean
    Public scrapefullcert As Boolean

    Public moviesortorder As Byte
    Public moviedefaultlist As Byte
    Public startuptab As Byte

    Public moviesets As New List(Of String)
    Public tableview As New List(Of String)
    Public offlinefolders As New List(Of String)

    Public commandlist As New List(Of ListOfCommands)
End Class
