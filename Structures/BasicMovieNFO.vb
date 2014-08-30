
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
    Dim tag As List(Of String)  ' = New List(Of String)
    Dim credits As String
    Dim director As String
    Dim stars As String
    Dim premiered As String
    Dim studio As String
    Dim trailer As String
    Dim playcount As String
    Dim lastplayed As String
    Dim imdbid As String
    Dim tmdbid As String
    Dim top250 As String
    Dim filename As String
    Dim thumbnails As String
    Dim fanart As String
    Dim country As String
    Dim album As String
    Dim artist As String
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
        tag = New List(Of String)  ' = ""
        credits = ""
        director = ""
        stars = ""
        premiered = ""
        studio = ""
        trailer = ""
        playcount = ""
        lastplayed = ""
        imdbid = ""
        tmdbid = ""
        top250 = ""
        filename = ""
        thumbnails = ""
        fanart = ""
        country = ""
        album = ""
        artist = ""
    End Sub

    Sub ClearWatched()
        playcount = "0"
    End Sub 

    Sub SetWatched()
        playcount = "1"
    End Sub 

End Structure
