Public Class TvShow
    Inherits Nfo.TvShow


    Public Shadows Property title As String
        Get
            Return MyBase.Title.Value
        End Get
        Set(ByVal value As String)
            MyBase.Title.Value = value
        End Set
    End Property

    Public Shadows Property year As String
        Get
            Return MyBase.Year.Value
        End Get
        Set(ByVal value As String)
            MyBase.Year.Value = value
        End Set
    End Property

    Public Shadows Property rating As String
        Get
            Return MyBase.Rating.Value
        End Get
        Set(ByVal value As String)
            MyBase.Rating.Value = value
        End Set
    End Property

    Public Shadows Property genre As String
        Get
            Return MyBase.Genre.Value
        End Get
        Set(ByVal value As String)
            MyBase.Genre.Value = value
        End Set
    End Property

    Public Shadows Property tvdbid As String
        Get
            Return MyBase.TvdbId.Value
        End Get
        Set(ByVal value As String)
            MyBase.TvdbId.Value = value
        End Set
    End Property

    Public Shadows Property imdbid As String
        Get
            Return MyBase.ImdbId.Value
        End Get
        Set(ByVal value As String)
            MyBase.ImdbId.Value = value
        End Set
    End Property

    Public Shadows Property sortorder As String
        Get
            Return MyBase.SortOrder.Value
        End Get
        Set(ByVal value As String)
            MyBase.SortOrder.Value = value
        End Set
    End Property

    Public Shadows Property language As String
        Get
            Return MyBase.Language.Value
        End Get
        Set(ByVal value As String)
            MyBase.Language.Value = value
        End Set
    End Property

    Public Shadows Property status As String
        Get
            Return MyBase.Status.Value
        End Get
        Set(ByVal value As String)
            MyBase.Status.Value = value
        End Set
    End Property

    Public Shadows Property plot As String
        Get
            Return MyBase.Plot.Value
        End Get
        Set(ByVal value As String)
            MyBase.Plot.Value = value
        End Set
    End Property

    Public Shadows Property runtime As String
        Get
            Return MyBase.Runtime.Value
        End Get
        Set(ByVal value As String)
            MyBase.Runtime.Value = value
        End Set
    End Property

    Public Shadows Property mpaa As String
        Get
            Return MyBase.Mpaa.Value
        End Get
        Set(ByVal value As String)
            MyBase.Mpaa.Value = value
        End Set
    End Property

    Public Shadows Property premiered As String
        Get
            Return MyBase.Premiered.Value
        End Get
        Set(ByVal value As String)
            MyBase.Premiered.Value = value
        End Set
    End Property

    Public Shadows Property studio As String
        Get
            Return MyBase.Studio.Value
        End Get
        Set(ByVal value As String)
            MyBase.Studio.Value = value
        End Set
    End Property

    Public Shadows Property trailer As String
        Get
            Return MyBase.Trailer.Value
        End Get
        Set(ByVal value As String)
            MyBase.Trailer.Value = value
        End Set
    End Property

    Public Shadows Property episodeguideurl As String
        Get
            Return MyBase.EpisodeGuideUrl.Value
        End Get
        Set(ByVal value As String)
            MyBase.EpisodeGuideUrl.Value = value
        End Set
    End Property

    Public Shadows Property tvshowactorsource As String
        Get
            Return MyBase.TvShowActorSource.Value
        End Get
        Set(ByVal value As String)
            MyBase.TvShowActorSource.Value = value
        End Set
    End Property

    Public Shadows Property episodeactorsource As String
        Get
            Return MyBase.episodeactorsource.Value
        End Get
        Set(ByVal value As String)
            MyBase.episodeactorsource.Value = value
        End Set
    End Property

    Public Property path As String
    Public Property fullpath As String
    Public Property locked As Integer
    Public Property posterpath As String
    Public Property fanartpath As String

    Public Property allepisodes As New List(Of TvEpisode)
    Public Property missingepisodes As New List(Of TvEpisode)

    Public Property posters As New List(Of String)
    Public Property fanart As New List(Of String)
End Class
