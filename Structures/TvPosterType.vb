
Public Structure str_TvPosterType
    Dim posterurl As String
    Dim tvpostertype As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        posterurl = ""
        tvpostertype = ""
    End Sub
End Structure
