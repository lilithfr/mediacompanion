Public Class Data_GridViewMovie
    Dim _fullpathandfilename As String
    Dim _movieset As String
    Dim _filename As String
    Dim _foldername As String
    Dim _title As String
    Dim _originaltitle As String
    Dim _titleandyear As String
    Dim _year As String
    Dim _filedate As String
    Dim _id As String
    Dim _rating As String
    Dim _top250 As String
    Dim _genre As String
    Dim _playcount As String
    Dim _sortorder As String
    Dim _outline As String
    Dim _runtime As String
    Dim _createdate As String
    Dim _missingdata1 As Byte
    Dim _plot As String
    Dim _source As String
    Dim _votes As String
    Dim _TitleUcase As String


    Sub New 

    End Sub

    Sub New(movie As ComboList) 
        Assign(movie)
    End Sub

    Public Sub Assign(movie As ComboList)
        fullpathandfilename = movie.fullpathandfilename
        movieset = movie.movieset
        filename = movie.filename
        foldername = movie.foldername
        title = movie.title
        originaltitle = movie.originaltitle
        titleandyear = movie.titleandyear
        year = movie.year
        filedate = movie.filedate
        id = movie.id
        rating = movie.rating
        top250 = movie.top250
        genre = movie.genre
        playcount = movie.playcount
        sortorder = movie.sortorder
        outline = movie.outline
        runtime = movie.runtime
        createdate = movie.createdate
        missingdata1 = movie.missingdata1
        plot = movie.plot.Trim
        source = movie.source
        votes = movie.votes
        TitleUcase = movie.title.ToUpper
    End Sub


    Public Property fullpathandfilename
        Get
            Return _fullpathandfilename
        End Get
        Set(ByVal value)
            _fullpathandfilename = value
        End Set
    End Property


    Public Property movieset
        Get
            Return _movieset
        End Get
        Set(ByVal value)
            _movieset = value
        End Set
    End Property

    Public Property filename
        Get
            Return _filename
        End Get
        Set(ByVal value)
            _filename = value
        End Set
    End Property

    Public Property foldername
        Get
            Return _foldername
        End Get
        Set(ByVal value)
            _foldername = value
        End Set
    End Property

    Public Property title
        Get
            Return _title
        End Get
        Set(ByVal value)
            _title = value
        End Set
    End Property

    Public Property originaltitle
        Get
            Return _originaltitle
        End Get
        Set(ByVal value)
            _originaltitle = value
        End Set
    End Property

    Public Property titleandyear
        Get
            Return _titleandyear
        End Get
        Set(ByVal value)
            _titleandyear = value
        End Set
    End Property

    Public Property year
        Get
            Return _year
        End Get
        Set(ByVal value)
            _year = value
        End Set
    End Property

    Public Property filedate
        Get
            Return _filedate
        End Get
        Set(ByVal value)
            _filedate = value
        End Set
    End Property

    Public Property id
        Get
            Return _id
        End Get
        Set(ByVal value)
            _id = value
        End Set
    End Property

    Public Property rating
        Get
            Return _rating
        End Get
        Set(ByVal value)
            _rating = value
        End Set
    End Property

    Public Property top250
        Get
            Return _top250
        End Get
        Set(ByVal value)
            _top250 = value
        End Set
    End Property

    Public Property genre
        Get
            Return _genre
        End Get
        Set(ByVal value)
            _genre = value
        End Set
    End Property

    Public Property playcount
        Get
            Return _playcount
        End Get
        Set(ByVal value)
            _playcount = value
        End Set
    End Property

    Public Property sortorder
        Get
            Return _sortorder
        End Get
        Set(ByVal value)
            _sortorder = value
        End Set
    End Property

    Public Property outline
        Get
            Return _outline
        End Get
        Set(ByVal value)
            _outline = value
        End Set
    End Property

    Public Property runtime
        Get
            Return _runtime
        End Get
        Set(ByVal value)
            _runtime = value
        End Set
    End Property

    Public Property createdate
        Get
            Return _createdate
        End Get
        Set(ByVal value)
            _createdate = value
        End Set
    End Property

    Public Property missingdata1
        Get
            Return _missingdata1
        End Get
        Set(ByVal value)
            _missingdata1 = value
        End Set
    End Property

    Public Property plot
        Get
            Return _plot
        End Get
        Set(ByVal value)
            _plot = value
        End Set
    End Property

    Public Property source
        Get
            Return _source
        End Get
        Set(ByVal value)
            _source = value
        End Set
    End Property

    Public Property votes
        Get
            Return _votes
        End Get
        Set(ByVal value)
            _votes = value
        End Set
    End Property

    Public Property TitleUcase
        Get
            Return _TitleUcase
        End Get
        Set(ByVal value)
            _TitleUcase = value
        End Set
    End Property
End Class
