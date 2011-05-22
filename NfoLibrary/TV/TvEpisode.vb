Imports ProtoXML


Public Class TvEpisode
    Inherits ProtoFile

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

    Public Property ImdbId As New ProtoProperty(Me, "imdbid")
    Public Property TvdbId As New ProtoProperty(Me, "tvdbid")


    Public Details As New FileInfo(Me, "fileinfo")

    Public Actors As New ActorList(Me, "actor")
End Class
