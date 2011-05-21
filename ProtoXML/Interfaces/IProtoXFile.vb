Public Interface IProtoXFile
    Inherits IProtoXBase

    Property Doc As XDocument


    Property NfoFilePath As String


    Sub Save()
    Sub Save(ByVal Path As String)

    Sub Load()
    Sub Load(ByVal Path As String)

End Interface
