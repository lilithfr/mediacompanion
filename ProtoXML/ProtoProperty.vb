
Public Class ProtoProperty
    Inherits ProtoXChildBase


    Public Property NotAttached As Boolean

    Private _value As String

    Event ValueChanged(ByVal NewValue As String)

    Public Property Value As String
        Get
            If _value Is Nothing Then Return Nothing

            Return _value
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                If Me.Node IsNot Nothing AndAlso Me.Node.Parent IsNot Nothing Then
                    Me.Node.Remove()
                    Me.Node = Nothing
                End If
                Exit Property
            End If

            If Me.Node Is Nothing Then
                Me.Node = <hold><%= value %></hold>
                Me.Node.Name = Me.NodeName

                If ParentClass IsNot Nothing AndAlso ParentClass.Node IsNot Nothing Then
                    If Node.Parent IsNot Nothing Then
                        Node.Remove()
                    End If
                    ParentClass.Node.Add(Me.Node)
                Else
                    NotAttached = True
                End If
            Else
                Me.Node.Value = CType(value, String)
            End If

            RaiseEvent ValueChanged(value)
            _value = value
        End Set
    End Property

    Public Sub New(ByRef Parent As IProtoXBase, ByVal NodeName As String)
        MyBase.New(Parent, NodeName)
    End Sub

    Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)
        If Element.Name.ToString.ToLower <> Me.NodeName Then
            Throw New InvalidExpressionException("Wrong element passed")
        End If

        Me.Node = Element
        Me.Value = Element.Value
    End Sub

    'Public Overrides Sub ResolveAttachment(ByRef ParentClass As IProtoXBase)
    '    If ParentClass Is Nothing Then
    '        Throw New Exception("Parent Class not set")
    '    End If

    '    If ParentClass.Node Is Nothing Then
    '        'Throw New Exception("Parent Class no node")
    '    Else
    '        If Not ParentClass.Node.Nodes.Contains(Node) Then
    '            If Me.Node Is Nothing Then
    '                Me.Node = New XElement(Me.NodeName)
    '                If _value IsNot Nothing Then
    '                    Me.Node.Value = _value
    '                End If
    '            End If
    '            If Node.Parent IsNot Nothing Then
    '                Node.Remove()
    '            End If
    '            ParentClass.Node.Add(Node)
    '        End If
    '    End If
    'End Sub

    Public Shared Narrowing Operator CType(ByVal Left As ProtoProperty) As String
        Return Left.Value
    End Operator

    Public Function IndexOf(ByVal Input As String) As Integer
        If Me.Value Is Nothing Then
            Return -1
        End If

        Return Me.Value.IndexOf(Input)
    End Function
End Class
