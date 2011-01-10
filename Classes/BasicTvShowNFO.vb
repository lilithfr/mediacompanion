
Public Class BasicTvShowNFO
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
End Class
