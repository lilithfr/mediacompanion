
Public Class ProtoProperty
    Inherits ProtoXChildBase


    Public Property NotAttached As Boolean

    Private _value As String

    Public Property Value As String
        Get
            If _value Is Nothing Then Return Nothing

            Return _value
        End Get
        Set(ByVal value As String)
            If Me.Node Is Nothing Then
                Me.Node = <hold><%= value %></hold>
                Me.Node.Name = Me.NodeName

                If ParentNode IsNot Nothing Then
                    ParentNode.Add(Me.Node)
                Else
                    NotAttached = True
                End If
            Else
                Me.Node.Value = CType(value, String)
            End If

            If value Is Nothing Then
                If Me.Node IsNot Nothing And Me.Node.Parent IsNot Nothing Then
                    Me.Node.Remove()
                    Me.Node = Nothing
                End If
            End If

            _value = value
        End Set
    End Property


    Public Sub New()
        MyBase.New(Nothing, Nothing)
        Throw New NotImplementedException()
    End Sub

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)
    End Sub

    Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)
        If Element.Name <> Me.NodeName Then
            Throw New InvalidExpressionException("Wrong element passed")
        End If

        Me.Node = Element
        Me.Value = Element.Value
    End Sub

    Public Overrides Sub ResolveAttachment(ByRef ParentClass As IProtoXBase)
        If ParentClass Is Nothing Then
            Throw New Exception("Parent Class not set")
        End If

        If ParentNode Is Nothing Then
            If ParentClass.Node Is Nothing Then
                Exit Sub
            End If
            ParentNode = ParentClass.Node
        End If


        If Not ParentNode.Nodes.Contains(Node) Then
            ParentNode.Add(Node)
        End If
    End Sub

    Public Shared Narrowing Operator CType(ByVal Left As ProtoProperty) As String
        Return Left.Value
    End Operator


End Class
