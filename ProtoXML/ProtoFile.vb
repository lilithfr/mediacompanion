Public Class ProtoFile
    Implements IProtoXFile



    Friend Property Doc As System.Xml.Linq.XDocument = <?xml version="1.0" encoding="UTF-8" standalone="yes"?><tvshow></tvshow> Implements IProtoXFile.Doc

    Private _node As XElement = Doc.Root
    Public Property Node As System.Xml.Linq.XElement Implements IProtoXBase.Node
        Get
            Return _node
        End Get
        Set(ByVal value As System.Xml.Linq.XElement)
            _node = value
        End Set
    End Property

    Public Property NodeName As String Implements IProtoXBase.NodeName
        Get
            Return _node.Name.ToString
        End Get
        Set(ByVal value As String)
            _node.Name = value
        End Set
    End Property

    Friend Sub AddChildForLoad(ByRef NewChild As IProtoXChild) Implements IProtoXBase.AddChildForLoad
        If Me.ChildrenLookup.ContainsKey(NewChild.NodeName) Then
            Throw New Exception("Already contains lookup for this node name)")
        End If

        Me.ChildrenLookup.Add(NewChild.NodeName, NewChild)
    End Sub

    Friend Property ChildrenLookup As New System.Collections.Generic.Dictionary(Of String, IProtoXChild) Implements IProtoXBase.ChildrenLookup



#Region "File Access"
    Public Property NfoFilePath As String Implements IProtoXFile.NfoFilePath

    Public Sub Load() Implements IProtoXFile.Load
        Me.Load(Me.NfoFilePath)
    End Sub

    Public Sub Load(ByVal Path As String) Implements IProtoXFile.Load
        Me.Doc = XDocument.Load(Path)

        LoadDoc()
    End Sub

    Public Sub LoadXml(ByVal Input As String)
        Me.Doc = XDocument.Parse(Input)

        LoadDoc()
    End Sub

    Private Sub LoadDoc()
        If Me.Doc.Root Is Nothing Then Throw New Exception("Invalid NFO file")
        Me._node = Me.Doc.Root
        Dim Root As XElement = Me.Doc.Root

        Dim ChildProperty As IProtoXChild
        For Each Child As XElement In Root.Nodes
            If Me.ChildrenLookup.ContainsKey(Child.Name.ToString.ToLower) Then
                ChildProperty = Me.ChildrenLookup.Item(Child.Name.ToString.ToLower)

                ChildProperty.ProcessNode(Child)
            End If
        Next

        Me.CleanDoc()
    End Sub

    Public Sub Save() Implements IProtoXFile.Save
        Me.Save(Me.NfoFilePath)
    End Sub

    Public Sub Save(ByVal Path As String) Implements IProtoXFile.Save
        Me.CleanDoc()

        Doc.Save(Path)
    End Sub

    Private Sub CleanDoc()
        CleanNode(Doc.Root)
    End Sub

    Private Sub CleanNode(ByVal Element As XElement)
        Dim Cursor As XElement
        Dim NextOne As XNode
        If Element.FirstNode Is Nothing Then
            Element.Remove()
            Exit Sub
        End If

        If Element.FirstNode.NodeType = Xml.XmlNodeType.Text Then
            Exit Sub
        End If

        Cursor = Element.FirstNode

        Do
            NextOne = Cursor.NextNode
            If Cursor.Nodes.Count = 0 AndAlso Cursor.Attributes.Count = 0 Then
                Cursor.Remove()
            Else
                CleanNode(Cursor)
            End If
            Cursor = NextOne
        Loop Until Cursor Is Nothing

        If Element.Nodes.Count = 0 AndAlso Element.Attributes.Count = 0 Then
            Element.Remove()
        End If
    End Sub

#End Region
End Class
