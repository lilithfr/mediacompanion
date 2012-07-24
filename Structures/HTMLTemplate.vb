Public Structure str_HTMLTemplate
    Dim title As String
    Dim path As String
    Dim body As String
    Dim type As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        title = ""
        path = ""
        body = ""
        type = ""
    End Sub
End Structure
