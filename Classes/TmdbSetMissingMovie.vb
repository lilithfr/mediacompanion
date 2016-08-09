﻿Public Class TmdbSetMissingMovie


'    Property Mso As MovieSetInfo
    Property Movie As CollectionMovie

    Property DgvMovie As New Data_GridViewMovie


    Sub New(_Mso As MovieSetInfo, _Movie As CollectionMovie)

        Movie             = _Movie
        DgvMovie.title    = _Movie.MovieTitle
        DgvMovie.movieset = _Mso
        DgvMovie.filename = _Movie.TmdbMovieId.ToString
        DgvMovie.tmdbid   = _Movie.TmdbMovieId
    End Sub




End Class
