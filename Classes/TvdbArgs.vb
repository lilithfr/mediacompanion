Public Class TvdbArgs
    Property tvdbid As String
    Property lang As String

    Sub New(Optional tvdbid As String = "", Optional lang As String = "")
        Me.tvdbid = tvdbid
        Me.lang = lang
    End Sub
End Class
