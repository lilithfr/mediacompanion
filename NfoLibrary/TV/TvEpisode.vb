Imports ProtoXML


Public Class TvEpisode
    Inherits ProtoFile

    Private _PureName As String
    Private Property PureName As String
        Get
            Return _PureName
        End Get
        Set(ByVal value As String)
            _PureName = value
            MyBase.NfoFilePath = value & ".nfo"
            For Each Item As String In Media_Companion.Utilities.VideoExtensions
                If IO.File.Exists(_PureName & Item) Then
                    _VideoFilePath = _PureName & Item
                End If
            Next
            Me.Thumbnail.FileName = _PureName & ".tbn"
        End Set
    End Property

    Public Shadows Property NfoFilePath As String
        Get
            Return MyBase.NfoFilePath
        End Get
        Set(ByVal value As String)
            Me.PureName = value.Replace(IO.Path.GetExtension(value), "")
            MyBase.NfoFilePath = value
        End Set
    End Property

    Private _VideoFilePath As String
    Public Property VideoFilePath As String
        Get
            Return _VideoFilePath
        End Get
        Set(ByVal value As String)
            Me.PureName = value.Replace(IO.Path.GetExtension(value), "")
            _VideoFilePath = value
        End Set
    End Property

    Public Property Title As New ProtoProperty(Me, "title")
    Public Property Rating As New ProtoProperty(Me, "rating")
    Public Property EpBookmark As New ProtoProperty(Me, "epbookmark")
    Public Property Year As New ProtoProperty(Me, "year")
    Public Property Top250 As New ProtoProperty(Me, "top250")
    Public Property Season As New ProtoProperty(Me, "season")
    Public Property Episode As New ProtoProperty(Me, "episode")
    Public Property DisplaySeason As New ProtoProperty(Me, "displayseason")
    Public Property DisplayEpisode As New ProtoProperty(Me, "displayepisode")
    Public Property Votes As New ProtoProperty(Me, "votes")
    Public Property Plot As New ProtoProperty(Me, "plot")
    Public Property TagLine As New ProtoProperty(Me, "tagline")
    Public Property Runtime As New ProtoProperty(Me, "runtime")
    Public Property MpaaCert As New ProtoProperty(Me, "mpaa")
    Public Property PlayCount As New ProtoProperty(Me, "playcount")
    Public Property LastPlayed As New ProtoProperty(Me, "lastplayed")
    Public Property Id As New ProtoProperty(Me, "id")
    Public Property Credits As New ProtoProperty(Me, "credits")
    Public Property Director As New ProtoProperty(Me, "director")
    Public Property Premiered As New ProtoProperty(Me, "premiered")
    Public Property Status As New ProtoProperty(Me, "status")
    Public Property Aired As New ProtoProperty(Me, "aired")
    Public Property Studio As New ProtoProperty(Me, "studio")
    Public Property Trailer As New ProtoProperty(Me, "trailer")
    Public Property Genre As New ProtoProperty(Me, "genre")
    Private Property Missing As New ProtoProperty(Me, "missing")

    Public Property ImdbId As New ProtoProperty(Me, "imdbid")
    Public Property TvdbId As New ProtoProperty(Me, "tvdbid")


    Public Property Details As New FileInfo(Me, "fileinfo")

    Public Property ListActors As New ActorList(Me, "actor")

    Public Property SeasonObj As TvSeason
    Public Property ShowObj As TvShow

    Public Property Thumbnail As New ProtoImage(Me, "thumbnail")

    Public Sub AbsorbTvdbEpisode(ByRef TvdbEpisode As Media_Companion.Tvdb.Episode)
        Me.Title.Value = TvdbEpisode.EpisodeName.Value
        Me.Rating.Value = TvdbEpisode.Rating.Value
        Me.Plot.Value = TvdbEpisode.Overview.Value
        Me.Director.Value = TvdbEpisode.Director.Value
        Me.ImdbId.Value = TvdbEpisode.ImdbId.Value
        Me.MpaaCert.Value = TvdbEpisode.ProductionCode.Value
        Me.TvdbId.Value = TvdbEpisode.Id.Value
        Me.Season.Value = TvdbEpisode.SeasonNumber.Value
        Me.Episode.Value = TvdbEpisode.EpisodeNumber.Value
        Me.UpdateTreenode()
    End Sub

    Public Property IsMissing As Boolean
        Get
            If Missing.Value Is Nothing Then
                Missing.Value = Boolean.FalseString
            End If
            If Missing.Value = Boolean.TrueString Then
                Me.EpisodeNode.ForeColor = Drawing.Color.Blue
            Else
                Me.EpisodeNode.ForeColor = Drawing.Color.Black
            End If
            Return _Missing
        End Get
        Set(ByVal value As Boolean)
            Missing.Value = CStr(value)
            If Missing.Value = Boolean.TrueString Then
                Me.EpisodeNode.ForeColor = Drawing.Color.Blue
            Else
                Me.EpisodeNode.ForeColor = Drawing.Color.Black
            End If
        End Set
    End Property

    Private _Visible As Boolean
    Public Property Visible As Boolean
        Get
            If _Visible Then
                If Not Me.SeasonObj.SeasonNode.Nodes.Contains(Me.EpisodeNode) Then
                    Me.SeasonObj.SeasonNode.Nodes.Add(Me.EpisodeNode)
        
                End If
            Else
                Me.SeasonObj.SeasonNode.Nodes.Remove(Me.EpisodeNode)
            End If
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
            If _Visible Then
                If Not Me.SeasonObj.SeasonNode.Nodes.Contains(Me.EpisodeNode) Then
                    Me.SeasonObj.SeasonNode.Nodes.Add(Me.EpisodeNode)
                    
                End If
            Else
                Me.SeasonObj.SeasonNode.Nodes.Remove(Me.EpisodeNode)
            End If

        End Set
    End Property
End Class
