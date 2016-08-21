
Public Class MovieSetInfo

    Private  _movieSetName        As String = ""
    Private  _movieSetDisplayName As String = ""


    Property TmdbSetId          As String = ""                     ' Defaults to Themoviedb.org ID if found
    Property Collection          As New List(Of CollectionMovie)
    Property LastUpdatedTs       As Date = DateTime.MinValue
    Property UserMovieSetName    As String = ""                     'Stores users preferred set name 
    Property MergeWithMovieSetId As String = ""                     'Merged sets support    


    Property MovieSetName As String
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
            Return If(UserMovieSetName<>"",UserMovieSetName,_movieSetDisplayName)
        End Get
    End Property


    Public ReadOnly Property DaysOld As Integer
        Get
            Return Date.Now.Subtract(LastUpdatedTs).Days
        End Get
    End Property   


    Public ReadOnly Property MissingInfo As Boolean
        Get
            Return TmdbSetId="" OrElse TmdbSetId="0" OrElse IsNothing(Collection) OrElse Collection.Count=0
        End Get
    End Property   

    

    Sub New
    End Sub

    Sub New( _moviesetname As String, _moviesetid As String, _collection As List(Of CollectionMovie), _lastUpdatedTs As Date, Optional _userMovieSetName As String="", Optional _mergeWithMovieSetId As String="")
        MovieSetName        = _moviesetname
        TmdbSetId          = _moviesetid
        Collection          = _collection
        LastUpdatedTs       = _lastUpdatedTs
        UserMovieSetName    = _userMovieSetName
        MergeWithMovieSetId = _mergeWithMovieSetId
    End Sub

    Sub Assign(from As MovieSetInfo)
        MovieSetName        = from.MovieSetName
        TmdbSetId          = from.TmdbSetId
        Collection          = from.Collection
        LastUpdatedTs       = from.LastUpdatedTs

        'Preverse user customisations, if any
        If from.UserMovieSetName   <>"" Then UserMovieSetName    = from.UserMovieSetName
        If from.MergeWithMovieSetId<>"" Then MergeWithMovieSetId = from.MergeWithMovieSetId
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


	 Public ReadOnly Property ReleaseYear As String
		Get
			If IsDate(release_date) Then
				Dim x = Convert.ToDateTime(release_date)

				Return x.Year.ToString
			Else
				Return "unknown"
			End If
		End Get
	 End Property


    Sub New(Optional _MovieTitle As String = "", Optional _MovieID As String = "",
            Optional _backdrop_path As String = "", Optional _poster_path As String = "", Optional _release_date As String = "")

        MovieTitle    = _MovieTitle
        TmdbMovieId   = _MovieID
        backdrop_path = _backdrop_path
        poster_path   = _poster_path  
        release_date  = _release_date 
    End Sub

End Class
