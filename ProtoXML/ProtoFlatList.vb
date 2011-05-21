Public MustInherit Class ProtoFlatList(Of T As ProtoPropertyGroup)
    Inherits ProtoXChildBase
    Implements IList(Of T)


    'Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)
    '    For Each Child As XElement In Element.Nodes

    '    Next
    'End Sub


    Public Sub New(ByRef ParentClass As IProtoXBase, ByRef ListItemNodeName As String)
        MyBase.New(ParentClass, ListItemNodeName)

        Me.NodeName = ListItemNodeName
        Me.ParentNode = ParentClass.Node
        Me.Node = ParentClass.Node
        Me.ParentClass = ParentClass
    End Sub


    Public List As New List(Of T)

    Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
        item.ParentClass = Me
        item.ParentNode = Me.Node
        item.ResolveAttachment()

        ParentNode.Add(item.Node)

        List.Add(item)
    End Sub

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
        For Each Node As XElement In (From X As XElement In ParentNode.Nodes Where X.Name = Me.NodeName)
            Node.Remove()
        Next

        List.Clear()
    End Sub

    Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
        Return List.Contains(item)
    End Function

    Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
        List.CopyTo(array)
    End Sub

    Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of T).Count
        Get
            Return List.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
        If List.Contains(item) Then
            item.Node.Remove()
            List.Remove(item)

            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        Return List.GetEnumerator
    End Function

    Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
        If item Is Nothing Then Return Nothing

        Return List.IndexOf(item)
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
        List.Item(index).Node.AddBeforeSelf(item.Node)
        List.Insert(index, item)
    End Sub

    Default Public Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
        Get
            If List.Count > index Then
                Return List.Item(index)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As T)
            List.Item(index) = value
        End Set
    End Property

    Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
        List.Item(index).Node.Remove()
        List.RemoveAt(index)
    End Sub

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return List.GetEnumerator()
    End Function

End Class