Imports ProtoXML


Public Class Actor
    Inherits ProtoPropertyGroup

    Public Property Name As New ProtoProperty(Me, "name")
    Public Property Role As New ProtoProperty(Me, "role")
    Public Property Thumb As New ProtoProperty(Me, "thumb")
    Public Property ActorId As New ProtoProperty(Me, "actorid")
    Public Sub New()
        MyBase.New(Nothing, Nothing)

        Me.Node = New XElement("actor")
        'Throw New NotImplementedException()
    End Sub

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)
    End Sub


    Public Shared Function Builder(ByVal Name As String, ByVal Role As String, ByVal Thumb As String) As Actor
        Dim TempActor As New Actor()

        TempActor.Name.Value = Name
        TempActor.Role.Value = Role
        TempActor.Thumb.Value = Thumb

        Return TempActor
    End Function
End Class
