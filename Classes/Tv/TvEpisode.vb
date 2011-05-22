Public Class TvEpisode
    Inherits Nfo.TvEpisode


    Private _EpisodePath As String
    Public Property episodepath As String
        Get
            Return _EpisodePath
        End Get
        Set(ByVal value As String)
            _EpisodePath = value
            Dim Parts As String() = _EpisodePath.Split(".")
            _MediaExtension = Parts(Parts.GetUpperBound(0))
            MyBase.NfoFilePath = _EpisodePath.Replace("." & _MediaExtension, ".nfo")
        End Set
    End Property

    Private _MediaExtension As String
    Public Property mediaextension As String
        Get
            Return _MediaExtension
        End Get
        Set(ByVal value As String)
            _MediaExtension = value
        End Set
    End Property

    Public Property thumb As String
    Public Property fanartpath As String

    Public Shadows Property title As String
        Get
            Return MyBase.Title.Value
        End Get
        Set(ByVal value As String)
            MyBase.Title.Value = value
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

    Public Shadows Property aired As String
        Get
            Return MyBase.Aired.Value
        End Get
        Set(ByVal value As String)
            MyBase.Aired.Value = value
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

    Public Shadows Property imdbid As String
        Get
            Return MyBase.ImdbId.Value
        End Get
        Set(ByVal value As String)
            MyBase.ImdbId.Value = value
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

    Public Shadows Property episodeno As String
        Get
            Return MyBase.Episode.Value
        End Get
        Set(ByVal value As String)
            MyBase.Episode.Value = value
        End Set
    End Property

    Public Shadows Property seasonno As String
        Get
            Return MyBase.Season.Value
        End Get
        Set(ByVal value As String)
            MyBase.Season.Value = value
        End Set
    End Property

    Public Shadows Property playcount As String
        Get
            Return MyBase.PlayCount.Value
        End Get
        Set(ByVal value As String)
            MyBase.PlayCount.Value = value
        End Set
    End Property

    Public Shadows Property credits As String
        Get
            Return MyBase.Credits.Value
        End Get
        Set(ByVal value As String)
            MyBase.Credits.Value = value
        End Set
    End Property

    Public Shadows Property director As String
        Get
            Return MyBase.Director.Value
        End Get
        Set(ByVal value As String)
            MyBase.Director.Value = value
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

    Public Property listactors As List(Of MovieActors)
        Get
            Dim TempList As New List(Of MovieActors)
            Dim OldActor As New MovieActors
            For Each NewActor As Nfo.Actor In MyBase.Actors
                OldActor.actorid = NewActor.ActorId
                OldActor.actorname = NewActor.Name
                OldActor.actorrole = NewActor.Role
                OldActor.actorthumb = NewActor.Thumb

                TempList.Add(OldActor)
            Next

            Return TempList
        End Get
        Set(ByVal value As List(Of MovieActors))
            Dim NewActor As New Nfo.Actor
            MyBase.Actors.Clear()
            For Each OldActor As MovieActors In value
                NewActor.ActorId.Value = OldActor.actorid
                NewActor.Name.Value = OldActor.actorname
                NewActor.Role.Value = OldActor.actorrole
                NewActor.Thumb.Value = OldActor.actorthumb

                MyBase.Actors.Add(NewActor)
            Next
        End Set
    End Property

    Public Property filedetails As FullFileDetails
        Get
            Dim TempDetails As New FullFileDetails

            TempDetails.filedetails_video = MyBase.Details.StreamDetails.Video
            For Each Audio As Nfo.AudioDetails In MyBase.Details.StreamDetails.Audio
                TempDetails.filedetails_audio.Add(Audio)
            Next
            For Each Subtitle As Nfo.SubtitleDetails In MyBase.Details.StreamDetails.Subtitles
                TempDetails.filedetails_subtitles.Add(Subtitle)
            Next
            Return TempDetails
        End Get
        Set(ByVal value As FullFileDetails)
            MyBase.Details.StreamDetails.Audio.Clear()
            MyBase.Details.StreamDetails.Subtitles.Clear()

            MyBase.Details.StreamDetails.Video = value.filedetails_video
            For Each Audio As MediaNFOAudio In value.filedetails_audio
                MyBase.Details.StreamDetails.Audio.Add(Audio)
            Next
            For Each Subtitle As MediaNFOSubtitles In value.filedetails_subtitles
                MyBase.Details.StreamDetails.Subtitles.Add(Subtitle)
            Next
        End Set
    End Property

End Class
