Public Structure str_FanartList
    Dim bigUrl As String
    Dim smallUrl As String
    Dim type As String
    Dim resolution As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        bigUrl = ""
        smallUrl = ""
        type = ""
        resolution = ""
    End Sub
End Structure
