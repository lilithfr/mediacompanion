Public MustInherit Class ProtoXChildBase
    Implements IProtoXChild

    Public Property ParentNode As XElement Implements IProtoXChild.ParentNode
    Public Property ParentClass As IProtoXBase Implements IProtoXChild.ParentClass

    Public Property ChildrenLookup As New System.Collections.Generic.Dictionary(Of String, IProtoXChild) Implements IProtoXBase.ChildrenLookup
    Public Property Node As XElement Implements IProtoXBase.Node
    Private _NodeName As String

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        If Parent Is Nothing Then Exit Sub
        Me.ParentClass = Parent
        Me.ParentNode = Parent.Node

        Me.NodeName = NodeName

        ParentClass.ChildrenLookup.Add(NodeName, Me)
    End Sub

    Public Property NodeName As String Implements IProtoXBase.NodeName
        Get
            Return _NodeName
        End Get
        Set(ByVal value As String)
            _NodeName = value
            If _Node IsNot Nothing Then
                _Node.Name = value
            End If
        End Set
    End Property

 
#Region "Bootstrap procedures"
    Public Sub AttachToParentClass(ByRef ParentClass As IProtoXBase) Implements IProtoXChild.AttachToParentClass
        If ParentClass.ChildrenLookup.ContainsKey(Me.NodeName) AndAlso ParentClass.ChildrenLookup.Item(Me.NodeName) <> Me Then
            Throw New Exception("ChildLookup entry already exists")
        End If

        ParentClass.ChildrenLookup.Add(Me.NodeName, Me)
    End Sub

    Public Sub AttachToParentNode(ByRef ParentNode As System.Xml.Linq.XElement) Implements IProtoXChild.AttachToParentNode
        If ParentNode.Nodes.Contains(Me.Node) Then
            Exit Sub
        End If

        ParentNode.Add(Me.Node)
    End Sub

    Public MustOverride Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement) Implements IProtoXChild.ProcessNode

#End Region

#Region "Operator Overloads"
    Public Shared Operator =(ByVal Left As ProtoXChildBase, ByVal Right As ProtoXChildBase)
        If Object.ReferenceEquals(Left, Right) Then
            Return True
        Else
            Return False
        End If
    End Operator

    Public Shared Operator <>(ByVal Left As ProtoXChildBase, ByVal Right As ProtoXChildBase)
        If Object.ReferenceEquals(Left, Right) Then
            Return False
        Else
            Return True
        End If
    End Operator


#End Region

    Private Sub AddChildForLoad(ByRef NewChild As IProtoXChild) Implements IProtoXBase.AddChildForLoad
        Throw New NotImplementedException()
    End Sub


    Public Overridable Sub ResolveAttachment(ByRef ParentClass As IProtoXBase) Implements IProtoXChild.ResolveAttachment
        For Each item As IProtoXChild In ChildrenLookup.Values
            item.ResolveAttachment(ParentClass)
        Next
    End Sub
End Class
