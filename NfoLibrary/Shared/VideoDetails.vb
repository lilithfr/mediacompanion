Imports ProtoXML

Public Class VideoDetails
    Inherits ProtoPropertyGroup


    Public Property Codec As New ProtoProperty(Me, "codec")
    Public Property Aspect As New ProtoProperty(Me, "aspect")
    Public Property Width As New ProtoProperty(Me, "width")
    Public Property Height As New ProtoProperty(Me, "height")
    Public Property DurationInSeconds As New ProtoProperty(Me, "durationinseconds")



    Private Sub New()
        MyBase.New(Nothing, Nothing)
        Throw New NotImplementedException()
    End Sub

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)
    End Sub
End Class
