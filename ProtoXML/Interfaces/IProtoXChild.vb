Public Interface IProtoXChild
    Inherits IProtoXBase

    Property ParentClass As IProtoXBase
    Property ParentNode As XElement

    Sub ProcessNode(ByRef Element As XElement)

    'Property Value As String

    Sub AttachToParentClass(ByRef ParentClass As IProtoXBase)

    Sub AttachToParentNode(ByRef ParentNode As XElement)

    Sub ResolveAttachment()

End Interface