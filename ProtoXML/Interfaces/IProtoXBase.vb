Public Interface IProtoXBase
    Property NodeName As String
    Property Node As XElement

    Property ChildrenLookup As Dictionary(Of String, IProtoXChild)
    Sub AddChildForLoad(ByRef NewChild As IProtoXChild)


End Interface
