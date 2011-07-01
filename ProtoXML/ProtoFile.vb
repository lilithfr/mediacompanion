Public Class ProtoFile
    Implements IProtoXFile


    Public Sub New(ByVal NodeName As String)
        Me.NodeName = NodeName
    End Sub

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


    Public Property IsCache As Boolean

#Region "File Access"
    Private _NfoFilePath As String
    Public Property NfoFilePath As String Implements IProtoXFile.NfoFilePath
        Get
            Return _NfoFilePath
        End Get
        Set(ByVal value As String)
            Dim Parts() As String
            Parts = value.Split("\")
            _FolderPath = Parts(0)
            For I = 1 To Parts.GetUpperBound(0) - 1
                _FolderPath &= "\" & Parts(I)
            Next
            _FolderPath &= "\"
            _NfoFilePath = value
        End Set
    End Property

    Private _FolderPath As String
    Public ReadOnly Property FolderPath As String
        Get
            Return _FolderPath
        End Get
    End Property

    Public Overridable Sub Load() Implements IProtoXFile.Load
        Me.Load(Me.NfoFilePath)
    End Sub

    Public Overridable Sub Load(ByVal Path As String) Implements IProtoXFile.Load
        Me.CleanDoc()
        If IO.File.Exists(Path) Then
            Me.Doc = XDocument.Load(Path)
        Else
            Exit Sub
        End If
        LoadDoc()
    End Sub


    Public Sub LoadXml(ByVal Input As XNode)

        Me.Doc = New XDocument(Input)

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
        Me.IsCache = False
    End Sub

    Public Sub Save() Implements IProtoXFile.Save
        Me.Save(Me.NfoFilePath)
    End Sub

    Public Sub Save(ByVal Path As String) Implements IProtoXFile.Save
        Me.CleanDoc()

        Doc.Save(Path)

        Me.IsCache = False
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

    Public ReadOnly Property FileContainsReadableXml As Boolean Implements IProtoXFile.FileContainsReadableXml
        Get
            Try
                If Me.FileExists Then
                    Dim Test As XDocument = XDocument.Load(Me.NfoFilePath)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Get
    End Property

    Public ReadOnly Property FileExists As Boolean Implements IProtoXFile.FileExists
        Get
            Return IO.File.Exists(Me.NfoFilePath)
        End Get
    End Property
End Class
