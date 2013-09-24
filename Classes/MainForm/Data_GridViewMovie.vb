Imports System.IO
Imports System.Text.RegularExpressions

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
    Dim _rating As Double
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
    Dim _votes As Integer=0
    Dim _TitleUcase As String
    'Dim _IntVotes As Integer=0
    Dim _IntRuntime As Integer=0
    Dim _DisplayFileDate   As String
    Dim _DisplayCreateDate As String

    Property Resolution As Integer = -1
    Property Audio      As New List(Of AudioDetails)
    Property Premiered As String
    Property FrodoPosterExists As Boolean
    Property PreFrodoPosterExists As Boolean

    Sub New 
    End Sub

    Sub New(movie As ComboList) 
        Assign(movie)
    End Sub

    Public Sub Assign(movie As ComboList)
        fullpathandfilename = movie.fullpathandfilename
        movieset = movie.MovieSet
        filename = movie.filename
        foldername = movie.foldername
        title = movie.title
        originaltitle = movie.originaltitle
        'titleandyear = movie.titleandyear
        year = movie.year
        filedate = movie.filedate
        id = movie.id
        Rating = movie.rating
        top250 = movie.top250
        genre = movie.genre
        playcount = movie.playcount
        SortOrder = movie.sortorder
        outline = movie.outline
        runtime = movie.runtime
        createdate = movie.createdate
        missingdata1 = movie.missingdata1
        plot = movie.plot.Trim
        source = movie.source
        Votes = movie.Votes
  '      Integer.TryParse(votes.Replace(",",""),IntVotes)
        TitleUcase = movie.title.ToUpper
        Integer.TryParse(runtime.Replace(" min",""),IntRuntime)
        Resolution = movie.Resolution
        AssignAudio(movie.Audio)
        Premiered = movie.Premiered
        Certificate = movie.Certificate
        FrodoPosterExists = movie.FrodoPosterExists
        PreFrodoPosterExists = movie.PreFrodoPosterExists
    End Sub

    Public Sub AssignAudio(From As List(Of AudioDetails))
        Me.Audio.Clear
        Me.Audio.AddRange(From)
    End Sub

    Public Function Export() As ComboList
        Dim convertedMovie As New ComboList With {.fullpathandfilename = Me.fullpathandfilename,
                                                  .MovieSet = Me.movieset,
                                                  .filename = Me.filename,
                                                  .foldername = Me.foldername,
                                                  .title = Me.title,
                                                  .originaltitle = Me.originaltitle,
                                                  .year = Me.year,
                                                  .filedate = Me.filedate,
                                                  .id = Me.id,
                                                  .rating = Me.Rating,
                                                  .top250 = Me.top250,
                                                  .genre = Me.genre,
                                                  .playcount = Me.playcount,
                                                  .sortorder = Me.SortOrder,
                                                  .outline = Me.outline,
                                                  .runtime = Me.runtime,
                                                  .createdate = Me.createdate,
                                                  .missingdata1 = Me.missingdata1,
                                                  .plot = Me.plot.Trim,
                                                  .source = Me.source,
                                                  .Votes = Me.Votes,
                                                  .Resolution  = Me.Resolution,
                                                  .Audio       = Me.Audio,
                                                  .Premiered   = Me.Premiered,
                                                  .Certificate = Me.Certificate,
                                                  .FrodoPosterExists = Me.FrodoPosterExists,
                                                 .PreFrodoPosterExists = Me.PreFrodoPosterExists
                                                 }
        Return convertedMovie
    End Function

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

    ReadOnly Property DisplayTitle As String
        Get
            Dim t As String = If(IsNothing(title),"Unknown",title)
           
            If Preferences.ignorearticle And t.ToLower.IndexOf("the ")=0 Then
                Return t.Substring(4, t.Length - 4) & ", The"
            Else
                Return t
            End If            
        End Get
    End Property

    ReadOnly Property DisplayTitleAndYear As String
        Get
            Return DisplayTitle & " (" & If(IsNothing(year),"0000",year) & ")"
        End Get
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

    'Public Property titleandyear
    '    Get
    '        Return _titleandyear
    '    End Get
    '    Set(ByVal value)
    '        _titleandyear = value
    '    End Set
    'End Property

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

    Public Property Rating As Double
        Get
            Return _rating
        End Get
        Set(ByVal value As Double)
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

    Public Property SortOrder As String
        Get
            Return _sortorder
        End Get
        Set(ByVal value As String)
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

    Public Property missingdata1 As Byte
        Get
            Return _missingdata1
        End Get
        Set(ByVal value As Byte)
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

    Public Property Votes As Integer
        Get
            Return _votes
        End Get
        Set(ByVal value As Integer)
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

    'Public Property IntVotes
    '    Get
    '        Return _IntVotes
    '    End Get
    '    Set(ByVal value)
    '        _IntVotes = value
    '    End Set
    'End Property

    Public Property IntRuntime
        Get
            Return _IntRuntime
        End Get
        Set(ByVal value)
            _IntRuntime = value
        End Set
    End Property

    Public ReadOnly Property DisplayFileDate As String
        Get
            If IsNothing(_DisplayFileDate) Then
                Try
                    _DisplayFileDate = DecodeDateTime(FileDate)
                Catch ex As Exception
                    _DisplayFileDate = FileDate.ToString
                End Try
            End If

            Return _DisplayFileDate
        End Get
    End Property

    Public ReadOnly Property DisplayCreateDate As String
        Get
            If IsNothing(_DisplayCreateDate) Then
                Try
                    _DisplayCreateDate = DecodeDateTime(CreateDate)
                Catch ex As Exception
                    _DisplayCreateDate = CreateDate.ToString
                End Try
            End If

            Return _DisplayCreateDate
        End Get
    End Property

    Public ReadOnly Property MissingFanart As Boolean
        Get
            Return _missingdata1 And 1
        End Get
    End Property

    Public ReadOnly Property MissingPoster As Boolean
        Get
            Return _missingdata1 And 2
        End Get
    End Property

    Public ReadOnly Property MissingTrailer As Boolean
        Get
            Return _missingdata1 And 4
        End Get
    End Property

    Public ReadOnly Property MissingLocalActors As Boolean
        Get
            Return _missingdata1 And 8
        End Get
    End Property

    ReadOnly Property DisplaySortOrder As String
        Get
            Dim t As String = If(IsNothing(SortOrder),"Unknown",SortOrder)
           
            If Preferences.ignorearticle And t.ToLower.IndexOf("the ")=0 Then
                Return t.Substring(4, t.Length - 4) & ", The"
            Else
                Return t
            End If            
        End Get
    End Property


    Public Function DecodeDateTime(s As String) As String

        Dim YYYY As String = s.SubString( 0,4)
        Dim MM   As String = s.SubString( 4,2)
        Dim DD   As String = s.SubString( 6,2)
        Dim HH   As String = s.SubString( 8,2)
        Dim MIN  As String = s.SubString(10,2)
        Dim SS   As String = s.SubString(12,2)

        Dim x As String = Preferences.DateFormat

        x = x.Replace("YYYY", YYYY)
        x = x.Replace("MM"  , MM  )
        x = x.Replace("DD"  , DD  )
        x = x.Replace("HH"  , HH  )
        x = x.Replace("MIN" , MIN )
        x = x.Replace("SS"  , SS  )

        Return x
    End Function

    Sub ClearStoredCalculatedFields
        _DisplayFileDate   = Nothing
        _DisplayCreateDate = Nothing
    End Sub


    Public ReadOnly Property MissingRating As Boolean
        Get
            Return Rating=0     '.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingGenre As Boolean
        Get
            Return genre.ToString.Trim=""
        End Get
    End Property  

    Public ReadOnly Property MissingCertificate As Boolean
        Get
            Return Certificate.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingOutline As Boolean
        Get
            Return outline.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingRuntime As Boolean
        Get
            Return runtime=""
        End Get
    End Property  


    Public ReadOnly Property MissingVotes As Boolean
        Get
            Return Votes = 0        '.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingYear As Boolean
        Get
            Return year = 0     '.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingPlot As Boolean
        Get
            Return plot.ToString.Trim="" Or plot.ToString.Trim="scraper error"
        End Get
    End Property  


    Public ReadOnly Property Watched As Boolean
        Get
            Return playcount<>"0"
        End Get
    End Property  

    Public ReadOnly Property DisplayRating As String
        Get
            Return _rating.ToString("f1")
        End Get
    End Property


    Public ReadOnly Property MoviePathAndFileName As String
        Get
            Return Utilities.GetFileName(fullpathandfilename,True)
        End Get
    End Property  


    ReadOnly Property ActualNfoFileNameMatchesDesired As Boolean
        Get
            Return (ActualNfoFileName=DesiredNfoFileName)
        End Get
    End Property


    '
    ' Returns the Nfo filename based on the user configured rename pattern & name mode (1=include first stack part name e.g CD1)
    '
    ReadOnly Property DesiredNfoFileName As String
        Get
            Dim stackName       = MoviePathAndFileName
            Dim isFirstPart     = True
            Dim stackdesignator = ""
            Dim nextStackPart   = ""
            Dim result          = UserDefinedFileName

            Utilities.isMultiPartMedia(stackName, False, isFirstPart, stackdesignator, nextStackPart)

            If isFirstPart And Preferences.namemode="1" Then
                Dim i As Integer  
                result &= stackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
            End If

            Return result & ".nfo"
        End Get
    End Property


    ReadOnly Property ActualNfoFileName As String
        Get
            Return Path.GetFileName(fullpathandfilename)
        End Get
    End Property


    ReadOnly Property UserDefinedFileName As String
        Get
            Dim s As String = Preferences.MovieRenameTemplate

            s = s.Replace("%T", title)
            s = s.Replace("%Y", year)
            s = s.Replace("%I", id)
            s = s.Replace("%P", Premiered)     
            s = s.Replace("%R", rating)
            s = s.Replace("%L", runtime)
            s = s.Replace("%S", source)
            s = Utilities.cleanFilenameIllegalChars(s)

            Return s
        End Get
    End Property


    Public Property Certificate As String

    Public ReadOnly Property ImdbInFolderName As Boolean
        Get
            Return Regex.Match(foldername,"(tt\d{7})").Success
        End Get
    End Property

    Private _FolderNameYear As Integer = -1

    Public ReadOnly Property FolderNameYear As Integer
        Get
            If _FolderNameYear=-1 Then
                Dim m = Regex.Match(foldername,"(\d{4})")
                If m.Success Then
                    _FolderNameYear = Convert.ToInt32(m.Value)
                Else
                    _FolderNameYear = -2
                End If
            End If
            Return _FolderNameYear
        End Get
    End Property

End Class
