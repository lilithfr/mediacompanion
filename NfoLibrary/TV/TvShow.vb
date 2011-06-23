Imports ProtoXML
Imports System.Net
Imports System.IO
Imports System.Xml
Imports Media_Companion

Public Class TvShow
    Inherits ProtoFile

    Public Property Title As New ProtoProperty(Me, "title")
    Public Property Year As New ProtoProperty(Me, "year")
    Public Property Rating As New ProtoProperty(Me, "rating")
    Public Property Genre As New ProtoProperty(Me, "genre")
    Public Property Id As New ProtoProperty(Me, "id")
    Public Property TvdbId As New ProtoProperty(Me, "tvdbid")
    '    Get
    '        Return Id
    '    End Get
    '    Set(ByVal value As ProtoProperty)
    '        Id = value
    '    End Set
    'End Property
    Public Property ImdbId As New ProtoProperty(Me, "imdbid")
    Public Property SortOrder As New ProtoProperty(Me, "sortorder")
    Public Property Language As New ProtoProperty(Me, "language")
    Public Property Status As New ProtoProperty(Me, "status")
    Public Property Plot As New ProtoProperty(Me, "plot")
    Public Property Runtime As New ProtoProperty(Me, "runtime")
    Public Property Mpaa As New ProtoProperty(Me, "mpaacert")
    Public Property Premiered As New ProtoProperty(Me, "premiered")
    Public Property Studio As New ProtoProperty(Me, "studio")
    Public Property Trailer As New ProtoProperty(Me, "trailer")
    Public Property EpisodeGuideUrl As New ProtoProperty(Me, "episodeguideurl")


    Public Property TvShowActorSource As New ProtoProperty(Me, "tvshowactorsource")

    Public Property EpisodeActorSource As New ProtoProperty(Me, "episodeactorsource")

    Public Property ListActors As New ActorList(Me, "actor")

    Public Property State As New ProtoProperty(Me, "state")

    Public ReadOnly Property TitleAndYear As String
        Get
            Return Title.Value & " " & Year.Value
        End Get
    End Property

    Dim _PossibleShowList As List(Of Media_Companion.Tvdb.Series)
    Public Property PossibleShowList As List(Of Media_Companion.Tvdb.Series)
        Get
            If _PossibleShowList Is Nothing Then
                Me.GetPossibleShows()
            End If

            Return _PossibleShowList
        End Get
        Set(ByVal value As List(Of Media_Companion.Tvdb.Series))
            _PossibleShowList = value
        End Set
    End Property

    Public Sub GetPossibleShows()
        'Dim possibleshows As New List(Of possibleshowlist)
        Dim xmlfile As String
        If String.IsNullOrEmpty(Me.FolderPath) Then
            Exit Sub
        End If
        'TODO: Properly encode URL
        'TODO: remove articles (And, &, The, ext)... Tvdb search is extremly litteral with absolutly no text adjustment

        Dim mirrorsurl As String
        If Title.Value Is Nothing Then
            mirrorsurl = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & Media_Companion.Utilities.GetLastFolder(Me.FolderPath) & "&language=all"
        Else
            mirrorsurl = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & Title.Value & "&language=all"
        End If

        xmlfile = Media_Companion.Utilities.DownloadTextFiles(mirrorsurl)

        Dim ReturnData As New Media_Companion.Tvdb.ShowData

        ReturnData.LoadXml(xmlfile)

        If _PossibleShowList Is Nothing Then _PossibleShowList = New List(Of Media_Companion.Tvdb.Series)
        _PossibleShowList.AddRange(ReturnData.Series.List)
    End Sub

    Public Sub AbsorbTvdbSeries(ByVal Series As Tvdb.Series)
        Me.Id.Value = Series.Id.Value
        Me.TvdbId.Value = Series.Id.Value
        Me.Mpaa.Value = Series.ContentRating.Value
        Me.Genre.Value = Series.Genre.Value
        Me.ImdbId.Value = Series.ImdbId.Value
        Me.Plot.Value = Series.Overview.Value
        Me.Title.Value = Series.SeriesName.Value
        Me.Runtime.Value = Series.RunTimeWithCommercials.Value
        Me.Rating.Value = Series.Rating.Value
        Me.Premiered.Value = Series.FirstAired.Value
        Me.Studio.Value = Series.Network.Value

    End Sub
End Class

Public Enum ShowState
    Open
    Locked
    Unverified

End Enum