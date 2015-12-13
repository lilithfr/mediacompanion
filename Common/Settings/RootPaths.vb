
Public Class str_RootPaths
    Property rpath As String
    Property selected As Boolean
    Sub New() 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        rpath = ""
        selected = True
    End Sub
End Class