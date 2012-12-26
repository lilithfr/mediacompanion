
Public Structure str_ListOfProfiles
    Dim moviecache As String
    Dim tvcache As String
    Dim actorcache As String
    Dim profilename As String
    Dim regexlist As String
    Dim filters As String
    Dim config As String
    Dim homemoviecache As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        moviecache = ""
        tvcache = ""
        actorcache = ""
        profilename = ""
        regexlist = ""
        filters = ""
        config = ""
    End Sub
End Structure
