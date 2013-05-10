
Public Class ActorDatabase

    Property ActorName As String = ""
    Property MovieId   As String = ""

    Sub New

    End Sub

    Sub New( _actorname As String, _movieid As String)
        ActorName = _actorname
        MovieId   = _movieid
    End Sub

End Class
