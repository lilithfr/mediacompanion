
Public Structure str_BasicMovieNFO
    Dim title As String
    Dim originaltitle As String
    Dim sortorder As String
    Dim movieset As String
    Dim source As String
    Dim year As String
    Dim rating As String
    Dim votes As String
    Dim outline As String
    Dim plot As String
    Dim tagline As String
    Dim runtime As String
    Dim mpaa As String
    Dim genre As String
    Dim tag As String
    Dim credits As String
    Dim director As String
    Dim stars As String
    Dim premiered As String
    Dim studio As String
    Dim trailer As String
    Dim playcount As String
    Dim imdbid As String
    Dim top250 As String
    Dim filename As String
    Dim thumbnails As String
    Dim fanart As String
    Dim country As String
    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        title = ""
        originaltitle = ""
        sortorder = ""
        movieset = ""
        source = ""
        year = ""
        rating = ""
        votes = ""
        outline = ""
        plot = ""
        tagline = ""
        runtime = ""
        mpaa = ""
        genre = ""
        tag = ""
        credits = ""
        director = ""
        stars = ""
        premiered = ""
        studio = ""
        trailer = ""
        playcount = ""
        imdbid = ""
        top250 = ""
        filename = ""
        thumbnails = ""
        fanart = ""
        country = ""
    End Sub
End Structure
