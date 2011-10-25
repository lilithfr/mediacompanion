
Public Structure str_NewMovie
    Dim nfopathandfilename As String
    Dim nfopath As String
    Dim title As String
    Dim mediapathandfilename As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        nfopathandfilename = ""
        nfopath = ""
        title = ""
        mediapathandfilename = ""
    End Sub
End Structure
