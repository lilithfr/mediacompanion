
Public Structure str_ListOfPosters
    Dim hdposter As String
    Dim ldposter As String
    Dim hdwidth As String
    Dim hdheight As String
    Dim ldwidth As String
    Dim ldheight As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        hdposter = ""
        ldposter = ""
        hdwidth = ""
        hdheight = ""
        ldwidth = ""
        ldheight = ""
    End Sub
End Structure
