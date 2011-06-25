Public Interface IProtoXChild
    Inherits IProtoXBase

    Property ParentClass As IProtoXBase

    Sub ProcessNode(ByRef Element As XElement)

    Sub AttachToParentClass(ByRef ParentClass As IProtoXBase)

    Sub AttachToParentNode(ByRef ParentNode As XElement)

    Sub ResolveAttachment(ByRef ParentClass As IProtoXBase)

End Interface