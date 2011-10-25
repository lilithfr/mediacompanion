
Public Class Structures
    Public intruntime As Boolean
    Public customcounter As Integer
    Public autorenameepisodes As Boolean
    Public autoepisodescreenshot As Boolean
    Public ignorearticle As Boolean
    Public movies_useXBMC_Scraper As Boolean
    Public XBMC_Scraper As String = ""
    Public eprenamelowercase As Boolean
    Public tvshowautoquick As Boolean
    Public font As String
    Public moviesortorder As Byte
    Public moviedefaultlist As Byte
    Public startuptab As Byte
    Public tvshowrebuildlog As Boolean
    Public tvrename As Integer
    Public roundminutes As Boolean
    Public locx As Integer
    Public locy As Integer
    Public moviethumbpriority() As String
    Public certificatepriority() As String
    Public keepfoldername As Boolean
    Public startupcache As Boolean
    Public ignoretrailers As Boolean
    Public ignoreactorthumbs As Boolean
    Public maxactors As Integer
    Public maxmoviegenre As Integer
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
    Public rarsize As Integer
    Public actorsave As Boolean
    Public actorsavepath As String
    Public actornetworkpath As String
    Public resizefanart As Integer
    Public overwritethumbs As Boolean
    Public defaulttvthumb As String
    Public imdbmirror As String
    Public backgroundcolour As String
    Public forgroundcolour As String
    Public remembersize As Boolean
    Public formheight As Integer
    Public formwidth As Integer
    Public videoplaybackmode As Integer
    Public usefoldernames As Boolean
    Public createfolderjpg As Boolean
    Public basicsavemode As Boolean
    Public startupdisplaynamemode As Integer
    Public namemode As String
    Public tvdblanguage As String
    Public tvdblanguagecode As String
    'Dim tvdbmode As String
    Public tvdbactorscrape As Integer
    Public usetransparency As Boolean
    Public transparencyvalue As Integer
    Public downloadtvseasonthumbs As Boolean
    Public maximumthumbs As Integer
    Public configpath As String
    Public startupmode As Integer
    Public disabletvlogs As Boolean
    Public savefanart As Boolean
    Public postertype As String
    Public sortorder As String
    Public videomode As Integer ' = 3
    Public selectedvideoplayer As String
    Public maximagecount As Integer
    Public lastpath As String
    Public moviescraper As Integer
    Public nfoposterscraper As Integer
    Public alwaysuseimdbid As Boolean
    Public gettrailer As Boolean
    Public externalbrowser As Boolean
    Public maximised As Boolean
    Public episodeacrorsource As String
    Public copytvactorthumbs As Boolean = False
    Public seasonall As String
    Public splt1 As Integer
    Public splt2 As Integer
    Public splt3 As Integer
    Public splt4 As Integer
    Public splt5 As Integer
    Public moviesets As New List(Of String)
    Public tvposter As Boolean
    Public tvfanart As Boolean
    Public tablesortorder As String
    Public actorseasy As Boolean
    Public tableview As New List(Of String)
    Public offlinefolders As New List(Of String)
    Public commandlist As New List(Of ListOfCommands)
End Class
