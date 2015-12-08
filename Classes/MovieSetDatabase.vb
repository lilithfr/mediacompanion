
Public Class MovieSetDatabase

    Private  _movieSetName        As String = ""
    Private  _movieSetDisplayName As String = ""

    Property MovieSetId     As String = ""
    Property collection     As List(Of CollectionMovie)


    Property MovieSetName   As String
        Get
            Return _movieSetName
        End Get
        Set
            _movieSetName = Value
            UpdateMovieSetDisplayName
        End Set
    End Property


    Public ReadOnly Property MovieSetDisplayName As String
        Get
            Return _movieSetDisplayName
        End Get
    End Property


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

    Sub UpdateMovieSetDisplayName
        _movieSetDisplayName = Pref.RemoveIgnoredArticles(MovieSetName)
    End Sub

End Class

Public Class CollectionMovie
    Property MovieTitle As String = ""
    Property MovieID As String = ""

    Sub New

    End Sub

End Class
