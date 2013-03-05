
Public Class MovieProgress

    Enum MsgType 
        GotFoldersCount
        NextOne
    End Enum

    Property ProgressEvent As MsgType
    Property Data          As Object

    Sub New(ProgressEvent As MsgType, Data As Object)
        Me.ProgressEvent = ProgressEvent
        Me.Data          = Data
    End Sub

End Class


