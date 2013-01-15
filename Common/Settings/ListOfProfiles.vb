
Public Class ListOfProfiles

    Property MovieCache     As String = ""
    Property TvCache        As String = ""
    Property ActorCache     As String = ""
    Property ProfileName    As String = ""
    Property RegExList      As String = ""
    Property Filters        As String = ""
    Property Config         As String = ""
    Property HomeMovieCache As String = ""

    Public Sub Assign(profileTo As ListOfProfiles)
        profileTo.ActorCache  = ActorCache
        profileTo.Config      = Config
        profileTo.MovieCache  = MovieCache
        profileTo.ProfileName = ProfileName
        profileTo.RegExList   = RegExList
        profileTo.Filters     = Filters
        profileTo.TvCache     = TvCache
        profileTo.ProfileName = ProfileName
    End Sub

End Class
