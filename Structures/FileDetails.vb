
Public Structure str_FileDetails
    Dim fullpathandfilename As String
    Dim path As String
    Dim filenameandpath As String
    Dim basepath As String
    Dim filename As String
    Dim foldername As String
    Dim videotspath As String
    Dim rootfolder As String
    Dim fanartpath As String
    Dim posterpath As String
    Dim trailerpath As String
    Dim createdate As String
    Dim movsetfanartpath As String
    Dim movsetposterpath As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        fullpathandfilename = ""
        path = ""
        filenameandpath = ""
        basepath = ""
        filename = ""
        foldername = ""
        videotspath =""
        rootfolder = ""
        fanartpath = ""
        posterpath = ""
        trailerpath = ""
        createdate = ""
        movsetfanartpath = ""
        movsetposterpath = ""
    End Sub
End Structure
