Imports System
Imports System.Collections.Generic

Namespace ConsoleApplication1
    Public Class preferences
        ' Fields
        Public actornetworkpath As String
        Public actorsave As Boolean
        Public actorsavepath As String
        Public actorseasy As Boolean
        Public alwaysuseimdbid As Boolean
        Public backgroundcolour As String
        Public basicsavemode As Boolean
        Public certificatepriority As String()
        Public checkinfofiles As Boolean
        Public configpath As String
        Public copytvactorthumbs As Boolean = False
        Public createfolderjpg As Boolean
        Public defaulttvthumb As String
        Public disablelogfiles As Boolean
        Public disabletvlogs As Boolean
        Public dontdisplayposter As Boolean
        Public downloadtvseasonthumbs As Boolean
        Public enablehdtags As Boolean
        Public enabletvhdtags As Boolean
        Public episodeacrorsource As String
        Public externalbrowser As Boolean
        Public fanartnotstacked As Boolean
        Public font As String
        Public forgroundcolour As String
        Public formheight As Integer
        Public formwidth As Integer
        Public gettrailer As Boolean
        Public ignoreactorthumbs As Boolean
        Public ignoretrailers As Boolean
        Public imdbmirror As String
        Public keepfoldername As Boolean
        Public lastpath As String
        Public locx As Integer
        Public locy As Integer
        Public maxactors As Integer
        Public maximagecount As Integer
        Public maximised As Boolean
        Public maximumthumbs As Integer
        Public maxmoviegenre As Integer
        Public moviedefaultlist As Byte
        Public moviescraper As Integer
        Public moviesets As List(Of String) = New List(Of String)
        Public moviesortorder As Byte
        Public moviethumbpriority As String()
        Public namemode As String
        Public nfoposterscraper As Integer
        Public offlinefolders As List(Of String) = New List(Of String)
        Public overwritethumbs As Boolean
        Public posternotstacked As Boolean
        Public postertype As String
        Public rarsize As Integer
        Public remembersize As Boolean
        Public renamenfofiles As Boolean
        Public resizefanart As Integer
        Public roundminutes As Boolean
        Public savefanart As Boolean
        Public scrapemovieposters As Boolean
        Public seasonall As String
        Public selectedvideoplayer As String
        Public sortorder As String
        Public splt1 As Integer
        Public splt2 As Integer
        Public splt3 As Integer
        Public splt4 As Integer
        Public splt5 As Integer
        Public startupcache As Boolean
        Public startupdisplaynamemode As Integer
        Public startupmode As Integer
        Public startuptab As Byte
        Public tablesortorder As String
        Public tableview As List(Of String) = New List(Of String)
        Public transparencyvalue As Integer
        Public tvdbactorscrape As Integer
        Public tvdblanguage As String
        Public tvdblanguagecode As String
        Public tvfanart As Boolean
        Public tvposter As Boolean
        Public tvrename As Integer
        Public tvshowautoquick As Boolean
        Public tvshowrefreshlog As Boolean
        Public usefanart As Boolean
        Public usefoldernames As Boolean
        Public usetransparency As Boolean
        Public videomode As Integer
        Public videoplaybackmode As Integer
    End Class
End Namespace

