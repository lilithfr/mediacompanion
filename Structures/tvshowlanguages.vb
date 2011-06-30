Public Structure str_TvShowLanguages
    Dim language As String
    Dim abbreviation As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        language = ""
        abbreviation = ""
    End Sub
End Structure
