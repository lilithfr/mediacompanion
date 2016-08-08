
Public Class MovieSetInfo

    Private  _movieSetName        As String = ""
    Private  _movieSetDisplayName As String = ""


    Property MovieSetId   As String = ""   ' Defaults to Themoviedb.org ID if found
    Property Collection   As New List(Of CollectionMovie)
    Property LastUpdatedTs As Date = DateTime.MinValue

    Property MovieSetName   As String
        Get
            Return _movieSetName
        End Get
        Set
            If _movieSetName <> Value Then
                _movieSetName = Value
                UpdateMovieSetDisplayName
            End If
        End Set
    End Property

    Public ReadOnly Property MovieSetDisplayName As String
        Get
            Return _movieSetDisplayName
        End Get
    End Property


    Private newPropertyValue As String
    Public ReadOnly Property DaysOld As Integer
        Get
            Return Date.Now.Subtract(LastUpdatedTs).Days
        End Get
    End Property   


    Sub New
    End Sub

    Sub New( _moviesetname As String, _moviesetid As String, _collection As List(Of CollectionMovie), _lastUpdatedTs As Date)
        MovieSetName  = _moviesetname
        MovieSetId    = _moviesetid
        Collection    = _collection
        LastUpdatedTs = _lastUpdatedTs
    End Sub


    Sub Assign(from As MovieSetInfo)
        MovieSetName  = from.MovieSetName
        MovieSetId    = from.MovieSetId
        Collection    = from.Collection
        LastUpdatedTs = from.LastUpdatedTs
    End Sub

    Sub UpdateMovieSetDisplayName
        _movieSetDisplayName = If(Pref.MovSetTitleIgnArticle, Pref.RemoveIgnoredArticles(MovieSetName), MovieSetName)
    End Sub

End Class

Public Class CollectionMovie

    Public Property MovieTitle    As String = ""
    Public Property TmdbMovieId   As String = ""
    Public Property backdrop_path As String = ""
    Public Property poster_path   As String = ""
    Public Property release_date  As String = ""



    Sub New(Optional _MovieTitle As String = "", Optional _MovieID As String = "",
            Optional _backdrop_path As String = "", Optional _poster_path As String = "", Optional _release_date As String = "")

        MovieTitle    = _MovieTitle
        TmdbMovieId   = _MovieID
        backdrop_path = _backdrop_path
        poster_path   = _poster_path  
        release_date  = _release_date 
    End Sub

End Class
