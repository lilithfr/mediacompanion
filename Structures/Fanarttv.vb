Public Structure str_fanarttvart
    Dim id As String
    Dim url As String
    Dim urlpreview As String
    Dim lang As String
    Dim likes As Integer
    Dim disctype As String
    Sub New(SetDefaults As Boolean)
        id = ""
        url = ""
        urlpreview = ""
        lang = ""
        likes = 0
        disctype = ""
    End Sub
End Structure

