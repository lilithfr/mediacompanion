Public Class preferences
    Public intruntime As Boolean
    Public customcounter As Integer
    Public autorenameepisodes As Boolean
    Public autoepisodescreenshot As Boolean
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
    Public commandlist As New List(Of listofcommands)
End Class

Public Structure listofcommands
    Dim title As String
    Dim command As String
End Structure

Public Structure listofposters
    Dim hdposter As String
    Dim ldposter As String
    Dim hdwidth As String
    Dim hdheight As String
    Dim ldwidth As String
    Dim ldheight As String
End Structure



Public Structure tvpostertype
    Dim posterurl As String
    Dim tvpostertype As String
End Structure

Public Structure tvposterseason
    Dim posterurl As String
    Dim posterseason As String
    Dim postertype As String
End Structure

Public Class tvart
    Dim tvfanart As List(Of String)
    Dim tvposterseasons As List(Of tvposterseason)
    Dim tvposters As List(Of String)
End Class

Public Structure actordatabase
    Dim actorname As String
    Dim movieid As String
End Structure


Public Structure basicmovienfo
    Dim title As String
    Dim sortorder As String
    Dim movieset As String
    Dim year As String
    Dim rating As String
    Dim votes As String
    Dim outline As String
    Dim plot As String
    Dim tagline As String
    Dim runtime As String
    Dim mpaa As String
    Dim genre As String
    Dim credits As String
    Dim director As String
    Dim premiered As String
    Dim studio As String
    Dim trailer As String
    Dim playcount As String
    Dim imdbid As String
    Dim top250 As String
    Dim filename As String
    Dim thumbnails As String
    Dim fanart As String
    Dim country As String
End Structure
Public Class basictvshownfo
    Public title As String
    Public year As String
    Public fullpath As String
    Public rating As String
    Public genre As String
    Public tvdbid As String
    Public imdbid As String
    Public sortorder As String
    Public language As String
    Public titleandyear As String
    Public allepisodes As New List(Of basicepisodenfo)
    Public missingepisodes As New List(Of basicepisodenfo)
    Public episodeactorsource As String
    Public status As String
    Public locked As Integer
End Class
Public Class basicepisodenfo
    Public title As String
    Public seasonno As String
    Public episodeno As String
    Public episodepath As String
    Public rating As String
    Public playcount As String
    Public tvdbid As String
    Public imdbid As String
    'Public status As String
End Class
Public Class tvshownfo
    Public path As String
    Public posterpath As String
    Public fanartpath As String
    Public title As String
    Public year As String
    Public rating As String
    Public plot As String
    Public runtime As String
    Public mpaa As String
    Public genre As String
    Public episodeguideurl As String
    Public premiered As String
    Public studio As String
    Public trailer As String
    Public sortorder As String
    Public language As String
    Public episodeactorsource As String
    Public tvshowactorsource As String
    Public imdbid As String
    Public tvdbid As String
    Public listactors As New List(Of movieactors)
    Public posters As New List(Of String)
    Public fanart As New List(Of String)
    Public status As String
    Public locked As Integer
End Class

Public Class episodeinfo
    Public title As String
    Public credits As String
    Public director As String
    Public aired As String
    Public playcount As String
    Public thumb As String
    Public rating As String
    Public seasonno As String
    Public episodeno As String
    Public plot As String
    Public runtime As String
    Public fanartpath As String
    Public genre As String
    Public mediaextension As String
    Public episodepath As String
    Public listactors As New List(Of movieactors)
    Public filedetails As New fullfiledetails

End Class



Public Structure movieactors
    Dim actorname As String
    Dim actorrole As String
    Dim actorthumb As String
    Dim actorid As String
End Structure

Public Class fullmoviedetails
    Public fileinfo As New filedetails
    Public fullmoviebody As New basicmovienfo
    Public alternativetitles As New List(Of String)
    Public listactors As New List(Of movieactors)
    Public listthumbs As New List(Of String)
    Public filedetails As New fullfiledetails
End Class

Public Class fullfiledetails
    Public filedetails_video As medianfo_video
    Public filedetails_audio As New List(Of medianfo_audio)
    Public filedetails_subtitles As New List(Of medianfo_subtitles)
End Class

Public Structure filedetails
    Dim fullpathandfilename As String
    Dim path As String
    Dim filename As String
    Dim foldername As String
    Dim fanartpath As String
    Dim posterpath As String
    Dim trailerpath As String
    Dim createdate As String
End Structure

Public Structure newmovie
    Dim nfopathandfilename As String
    Dim nfopath As String
    Dim title As String
    Dim mediapathandfilename As String
End Structure



Public Structure combolist
    Dim fullpathandfilename As String
    Dim movieset As String
    Dim filename As String
    Dim foldername As String
    Dim title As String
    Dim titleandyear As String
    Dim year As String
    Dim filedate As String
    Dim id As String
    Dim rating As String
    Dim top250 As String
    Dim genre As String
    Dim playcount As String
    Dim sortorder As String
    Dim outline As String
    Dim runtime As String
    Dim createdate As String
    Dim missingdata1 As Byte
End Structure



Public Structure runningthreads
    Dim thread1 As Boolean
    Dim thread2 As Boolean
    Dim thread3 As Boolean
    Dim thread4 As Boolean
    Dim thread5 As Boolean
    Dim thread6 As Boolean
End Structure

Public Class mediainfodll_complete
    Dim videodetails As New medianfo_video
    Dim audiodetails As New medianfo_audio
    Dim subsdetails As New List(Of medianfo_subtitles)
End Class





Public Structure medianfo_audio
    Dim language As String
    Dim codec As String
    Dim channels As String
    Dim bitrate As String
End Structure
Public Structure medianfo_subtitles
    Dim language As String
End Structure
Public Structure medianfo_video
    Dim width As String
    Dim height As String
    Dim aspect As String
    Dim codec As String
    Dim formatinfo As String
    Dim duration As String
    Dim bitrate As String
    Dim bitratemode As String
    Dim bitratemax As String
    Dim container As String
    Dim codecid As String
    Dim codecinfo As String
    Dim scantype As String
End Structure

Public Structure batchwizard
    Dim title As Boolean
    Dim votes As Boolean
    Dim rating As Boolean
    Dim top250 As Boolean
    Dim runtime As Boolean
    Dim director As Boolean
    Dim year As Boolean
    Dim outline As Boolean
    Dim plot As Boolean
    Dim tagline As Boolean
    Dim genre As Boolean
    Dim studio As Boolean
    Dim premiered As Boolean
    Dim mpaa As Boolean
    Dim trailer As Boolean
    Dim credits As Boolean
    Dim posterurls As Boolean
    Dim country As Boolean
    Dim actors As Boolean

    Dim mediatags As Boolean

    Dim missingposters As Boolean
    Dim missingfanart As Boolean

    Dim activate As Boolean
End Structure

Public Structure tvshowbatchwizard

    Dim sh_year As Boolean
    Dim sh_rating As Boolean
    Dim sh_plot As Boolean
    Dim sh_runtime As Boolean
    Dim sh_mpaa As Boolean
    Dim sh_genre As Boolean
    Dim sh_studio As Boolean
    Dim sh_actor As Boolean
    Dim sh_posters As Boolean
    Dim sh_fanart As Boolean


    Dim ep_streamdetails As Boolean
    Dim ep_aired As Boolean
    Dim ep_plot As Boolean
    Dim ep_director As Boolean
    Dim ep_credits As Boolean
    Dim ep_rating As Boolean
    Dim ep_runtime As Boolean
    Dim ep_actor As Boolean
    Dim ep_screenshot As Boolean
    Dim ep_createscreenshot As Boolean

    Dim includelocked As Boolean
    Dim activate As Boolean

    Dim doepisodes As Boolean
    Dim doshows As Boolean
    Dim doshowbody As Boolean
    Dim doshowart As Boolean
    Dim doshowactors As Boolean
    Dim doepisodebody As Boolean
    Dim doepisodeart As Boolean
    Dim doepisodeactors As Boolean
    Dim doepisodemediatags As Boolean
End Structure

Public Structure listofprofiles
    Dim moviecache As String
    Dim tvcache As String
    Dim actorcache As String
    Dim profilename As String
    Dim regexlist As String
    Dim filters As String
    Dim config As String
End Structure
Public Class profiles
    Public startupprofile As String
    Public defaultprofile As String
    Public workingprofilename As String
    Public profilelist As New List(Of listofprofiles)
End Class





