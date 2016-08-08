
Public Class Databases

    Property ActorName As String = ""
    Property MovieId   As String = ""

    Sub New

    End Sub

    Sub New( _actorname As String, _movieid As String)
        ActorName = _actorname
        MovieId   = _movieid
    End Sub

End Class

Public Class DirectorDatabase

    Property ActorName As String = ""
    Property MovieId   As String = ""

    Sub New

    End Sub

    Sub New( _actorname As String, _movieid As String)
        ActorName = _actorname
        MovieId   = _movieid
    End Sub

End Class

Public Class MovieSetDatabase

    Public tmdbid As String
    Public title As String
    Public present As Boolean

    'Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
    '    tmdbid = ""
    '    title = ""
    '    present = False
    'End Sub

    Sub New()

    End Sub
End Class

Public Class TagDatabase

    Property TagName    As String = ""
    Property MovieId    As String = ""

    Sub New()

    End Sub

    Sub New( _tagname As String, _movieid As String)
        TagName     = _tagname
        MovieId     = _movieid
    End Sub

End Class
