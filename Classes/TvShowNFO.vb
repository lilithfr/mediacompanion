
Public Class TvShowNFO
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
    Public allepisodes As New List(Of BasicEpisodeNFO)
    Public missingepisodes As New List(Of BasicEpisodeNFO)
    Public episodeactorsource As String
    Public status As String
    Public locked As Integer

    Public path As String
    Public posterpath As String
    Public fanartpath As String

    Public plot As String
    Public runtime As String
    Public mpaa As String
    Public episodeguideurl As String
    Public premiered As String
    Public studio As String
    Public trailer As String
    Public tvshowactorsource As String
    Public listactors As New List(Of MovieActors)
    Public posters As New List(Of String)
    Public fanart As New List(Of String)
End Class
