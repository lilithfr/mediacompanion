Public Structure TempEpisodeInfo
    Public title As String
    Public credits As String
    Public director As String
    Public aired As String
    Public playCount As String
    Public thumb As String
    Public rating As String
    Public seasonNO As String
    Public episodeNO As String
    Public plot As String
    Public runtime As String
    Public fanartPath As String
    Public genre As String
    Public mediaExtension As String
    Public episodePath As String
    Public Shared listActors As List(Of TempMovieActors) = New List(Of TempMovieActors)
    Public Shared fileDetails As New FullFileDetails
End Structure
