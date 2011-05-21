Public Class ProtoPropertyGroup
    Inherits ProtoXChildBase

    Public Property Orphan As Boolean

    Public Sub New()
        MyBase.New(Nothing, Nothing)
        Me.Orphan = True

        'Throw New NotImplementedException()
    End Sub

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)

        If Parent Is Nothing Then Exit Sub

        Me.Node = New XElement(NodeName)
        ParentNode.Add(Me.Node)
    End Sub

    Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)
        If Not Element.Name = Node.Name Then
            Throw New Exception("Wrong element sent")
        End If

        Dim ChildProperty As IProtoXChild
        For Each Child As XElement In Element.Nodes
            If Me.ChildrenLookup.ContainsKey(Child.Name.ToString.ToLower) Then
                ChildProperty = Me.ChildrenLookup.Item(Child.Name.ToString.ToLower)

                ChildProperty.ProcessNode(Child)
            End If
        Next
        If Element.Name Is Nothing Then

        End If
    End Sub

    
End Class
