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
        ParentClass.Node.Add(Me.Node)
    End Sub

    Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)
        'If Not Element.Name = Node.Name Then
        '    Throw New Exception("Wrong element sent")
        'End If

        If ParentClass IsNot Nothing AndAlso Not XDocument.ReferenceEquals(Me.Node.Document, Element.Document) Then
            If Element.Parent IsNot Nothing Then
                Element.Remove()
            End If
            ParentClass.Node.Add(Element)
            Me.Node.Remove()
            Me.Node = Element
        End If

        Dim ChildProperty As IProtoXChild
        For Each Child As XElement In Element.Nodes
            If Me.ChildrenLookup.ContainsKey(Child.Name.ToString) Then
                ChildProperty = Me.ChildrenLookup.Item(Child.Name.ToString)

                ChildProperty.ProcessNode(Child)
            End If
        Next

    End Sub

    
End Class
