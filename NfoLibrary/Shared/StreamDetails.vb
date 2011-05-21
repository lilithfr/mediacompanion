Imports ProtoXML

Public Class StreamDetails
    Inherits ProtoPropertyGroup

    Public Property Video As New VideoDetails(Me, "video")
    Public Property Music As New AudioDetails(Me, "audio")
    Public Property Subtitles As New ProtoProperty(Me, "subtitles")

    Private Sub New()
        MyBase.New(Nothing, Nothing)
        Throw New NotImplementedException()
    End Sub

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)
    End Sub


End Class
