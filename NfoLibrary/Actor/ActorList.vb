Imports ProtoXML


Public Class ActorList
    Inherits ProtoFlatList(Of Actor)

    Private _node_TvShow As XElement


    Private Sub New()
        MyBase.New(Nothing, Nothing)
        Throw New NotImplementedException()
    End Sub

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)
    End Sub


    Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)

        Dim NewActor As New Actor()

        NewActor.ProcessNode(Element)

        Me.Add(NewActor)

    End Sub
End Class
