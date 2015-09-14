
Public Class MovieSetDatabase

    Property MovieSetName   As String = ""
    Property MovieSetId     As String = ""
    Property collection     As List(Of CollectionMovie)

    Sub New

    End Sub

    Sub New( _moviesetname As String, _moviesetid As String, _collection As List(Of CollectionMovie))
        MovieSetName    = _moviesetname
        MovieSetId      = _moviesetid
        collection      = _collection
    End Sub

    Sub absorb(from As MovieSetDatabase)
        MovieSetName = From.MovieSetName
        MovieSetId = From.MovieSetId 
    End Sub

End Class

Public Class CollectionMovie
    Property MovieTitle As String = ""
    Property MovieID As String = ""

    Sub New

    End Sub

End Class
