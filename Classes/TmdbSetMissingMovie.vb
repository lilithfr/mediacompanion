Public Class TmdbSetMissingMovie


'    Property Mso As MovieSetInfo
    Property Movie As CollectionMovie

    Property DgvMovie As New Data_GridViewMovie


    Sub New(_Mso As MovieSetInfo, _Movie As CollectionMovie)
'       Mso = _Mso
        Movie = _Movie
        DgvMovie.title = _Movie.MovieTitle
        DgvMovie.movieset = _Mso
        'DgvMovie.Got = False
        DgvMovie.filename = _Movie.TmdbMovieId.ToString
    End Sub




End Class
