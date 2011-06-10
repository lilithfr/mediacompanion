Imports ProtoXML

Public Class TvShow
    Inherits ProtoFile

    Public Property Title As New ProtoProperty(Me, "title")
    Public Property Year As New ProtoProperty(Me, "year")
    Public Property Rating As New ProtoProperty(Me, "rating")
    Public Property Genre As New ProtoProperty(Me, "genre")
    Public Property TvdbId As New ProtoProperty(Me, "tvdbid")
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

    Public ReadOnly Property TitleAndYear As String
        Get
            Return Title.Value & " " & Year.Value
        End Get
    End Property
End Class
