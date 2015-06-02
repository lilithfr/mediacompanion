
Public Class MovieSetDatabase

    Property MovieSetName As String = ""
    Property MovieSetId   As String = ""

    Sub New

    End Sub

    Sub New( _moviesetname As String, _moviesetid As String)
        MovieSetName = _moviesetname
        MovieSetId   = _moviesetid
    End Sub

    Sub absorb(from As MovieSetDatabase)
        MovieSetName = From.MovieSetName
        MovieSetId = From.MovieSetId 
    End Sub

End Class
