
Public Structure str_FileDetails
    Dim fullpathandfilename As String
    Dim filenameandpath As String
    Dim path As String
    Dim filename As String
    Dim foldername As String
    Dim videotspath As String
    Dim fanartpath As String
    Dim posterpath As String
    Dim trailerpath As String
    Dim createdate As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        fullpathandfilename = ""
        filenameandpath = ""
        path = ""
        filename = ""
        foldername = ""
        videotspath =""
        fanartpath = ""
        posterpath = ""
        trailerpath = ""
        createdate = ""
    End Sub
End Structure
