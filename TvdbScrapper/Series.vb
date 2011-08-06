Imports ProtoXML

Namespace Tvdb
    Public Class Series
        Inherits ProtoPropertyGroup


        'Documented
        Public Property Id As New ProtoProperty(Me, "id")
        Public Property ActorsNames As New ProtoProperty(Me, "actors")
        Public Property AirsDayOfWeek As New ProtoProperty(Me, "Airs_DayOfWeek")
        Public Property AirsTime As New ProtoProperty(Me, "Airs_Time")
        Public Property FirstAired As New ProtoProperty(Me, "FirstAired")
        Public Property Genre As New ProtoProperty(Me, "Genre")
        Public Property ImdbId As New ProtoProperty(Me, "IMDB_ID")
        Public Property Language As New ProtoProperty(Me, "Language")
        Public Property Network As New ProtoProperty(Me, "Network")
        Public Property Overview As New ProtoProperty(Me, "Overview")
        Public Property Rating As New ProtoProperty(Me, "Rating")
        Public Property RunTimeWithCommercials As New ProtoProperty(Me, "Runtime")
        Public Property SeriesID As New ProtoProperty(Me, "SeriesID")
        Public Property SeriesName As New ProtoProperty(Me, "SeriesName")
        Public Property Status As New ProtoProperty(Me, "Status")
        Public Property LastUpdatedOnTvdb As New ProtoProperty(Me, "lastupdated")

        'Discovered
        Public Property Banner As New ProtoProperty(Me, "banner")
        Public Property Fanart As New ProtoProperty(Me, "fanart")
        Public Property Poster As New ProtoProperty(Me, "poster")
        Public Property Added As New ProtoProperty(Me, "added")
        Public Property AddedBy As New ProtoProperty(Me, "addedBy")
        Public Property RatingCount As New ProtoProperty(Me, "RatingCount")
        Public Property NetworkId As New ProtoProperty(Me, "NetworkId")
        Public Property ContentRating As New ProtoProperty(Me, "ContentRating")
        Public Property Zap2ItId As New ProtoProperty(Me, "zap2it_id")

        Public Sub New()
            MyBase.New(Nothing, Nothing)

            Me.Node = New XElement("Series")
            For Each item In Me.ChildrenLookup.Values
                item.ParentClass = Me
                item.ResolveAttachment(item.ParentClass)
            Next
            'Throw New NotImplementedException()
        End Sub

        Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
            MyBase.New(Parent, NodeName)
        End Sub

        Public Overrides Function CreateNew() As ProtoXML.IProtoXChild
            Return New Series()
        End Function
    End Class
End Namespace