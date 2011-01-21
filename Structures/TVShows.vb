Public Structure tvshowlanguages
    Dim language As String
    Dim abbreviation As String
End Structure
Public Structure possibleshowlist
    Dim showtitle As String
    Dim showid As String
    Dim showbanner As String
End Structure
Public Structure tvbanners
    Dim url As String
    Dim smallurl As String
    Dim bannertype As String
    Dim resolution As String
    Dim language As String
    Dim season As String
End Structure
Public Structure fanartlist
    Dim bigurl As String
    Dim smallurl As String
    Dim type As String
    Dim resolution As String
End Structure
Public Structure TempMovieActors
    Public Shared actorname As String
    Public Shared actorrole As String
    Public Shared actorthumb As String
    Public Shared actorid As String
End Structure

Public Structure TempEpisodeInfo
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
    Public Shared listactors As List(Of TempMovieActors) = New List(Of TempMovieActors)
    Public Shared filedetails As New FullFileDetails
End Structure
