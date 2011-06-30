Public Structure str_TempMovieActors
    Public Shared actorName As String
    Public Shared actorRole As String
    Public Shared actorThumb As String
    Public Shared actorID As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        actorName = ""
        actorRole = ""
        actorThumb = ""
        actorID = ""
    End Sub
End Structure
