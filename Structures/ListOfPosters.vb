
Public Structure str_ListOfPosters
    Dim hdUrl As String
    Dim ldUrl As String
    Dim hdwidth As String
    Dim hdheight As String
    Dim ldwidth As String
    Dim ldheight As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        hdUrl = ""
        ldUrl = ""
        hdwidth = ""
        hdheight = ""
        ldwidth = ""
        ldheight = ""
    End Sub
End Structure
