Imports System.IO
Imports XBMC.JsonRpc
Imports System.Text.RegularExpressions

Public Class MVComboList
    
    Dim _DisplayCreateDate As String
    Dim _createdate As String
    Dim _TitleUcase As String
    Dim _IntRuntime As Integer=0

    Property nfopathandfilename  As String = ""
    Private _title               As String = ""
    
    Public Property Artist As String
        Get
            Return _artist
        End Get
        Set(value As String)
            _artist = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return _title
        End Get
        Set
            _title = Value.SafeTrim
        End Set
    End Property

    Public ReadOnly Property ArtistTitle As String
        Get
            Return artist & " - " & Title
        End Get
    End Property

    Public ReadOnly Property ArtistTitleYear As String
        Get
            Return ArtistTitle & " (" & If(year < 1900, "1901", year.ToString) & ")"
        End Get
    End Property

    Public ReadOnly Property TitleYear As String
        Get
            Return Title & " (" & If(year < 1900, "1901", year.ToString) & ")"
        End Get
    End Property

    Private _artist               As String = ""
    Property year                 As Integer= 0
    Property filedate             As String = ""
    Property genre                As String = ""
    Property playcount            As String = ""
    Property lastplayed           As String = ""
    Property runtime              As String = ""
    'Public Property createdate           As String = ""
    Property plot                 As String = ""
    Property Resolution           As Integer= -1
    Property Audio                As New List(Of AudioDetails)
    Property track                As String = ""
    Private _thumb                As String = ""
    Property FrodoPosterExists    As Boolean
    Property PreFrodoPosterExists As Boolean
    Property filename             As String = ""
    Property foldername           As String = ""
    
    Public Property thumb As String
        Get
            Return _thumb
        End Get
        Set(value As String)
            _thumb = Value
        End Set
    End Property

    'Private _FolderNameYear As Integer = -1

    'Public ReadOnly Property FolderNameYear As Integer
    '    Get
    '        If _FolderNameYear=-1 Then
    '            Dim m = Regex.Match(foldername,"(\d{4})")
    '            If m.Success Then
    '                _FolderNameYear = Convert.ToInt32(m.Value)
    '            Else
    '                _FolderNameYear = -2
    '            End If
    '        End If
    '        Return _FolderNameYear
    '    End Get
    'End Property

    Public Property CreateDate
        Get
            Return _createdate
        End Get
        Set(ByVal value)
            _createdate = value
        End Set
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
    
    Public ReadOnly Property MissingGenre As Boolean
        Get
            Return genre.ToString.Trim=""
        End Get
    End Property  

    Public ReadOnly Property MissingRuntime As Boolean
        Get
            Return runtime=""
        End Get
    End Property  

    Public ReadOnly Property MissingYear As Boolean
        Get
            Return year=0           '.ToString.Trim=""
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

    Public ReadOnly Property MusicVidPathAndFileName As String
        Get
            Return Utilities.GetFileName(nfopathandfilename,True)
        End Get
    End Property  

    'ReadOnly Property FrodoPosterExists As Boolean
    '    Get
    '        Return Preferences.FrodoPosterExists(nfopathandfilename)
    '    End Get
    'End Property

    ReadOnly Property ActualNfoFileName As String
        Get
            Return Path.GetFileName(nfopathandfilename)
        End Get
    End Property

    Public Property TitleUcase
        Get
            Return _TitleUcase
        End Get
        Set(ByVal value)
            _TitleUcase = value
        End Set
    End Property

    Public Property IntRuntime
        Get
            Return _IntRuntime
        End Get
        Set(ByVal value)
            _IntRuntime = value
        End Set
    End Property

    'ReadOnly Property UserDefinedFileName As String
    '    Get
    '        Dim s As String = Preferences.MovieRenameTemplate
    '        s = s.Replace("%T", title)
    '        s = s.Replace("%Y", year)
    '        s = s.Replace("%I", id)
    '        s = s.Replace("%P", Premiered)     
    '        s = s.Replace("%R", rating)
    '        s = s.Replace("%L", runtime)
    '        s = s.Replace("%S", source)
    '        s = Utilities.cleanFilenameIllegalChars(s)
    '        Return s
    '    End Get
    'End Property

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

    Public Sub Assign(From As MVComboList)
        nfopathandfilename      = From.nfopathandfilename
        filename                = From.filename
        foldername              = From.foldername
        Title                   = From.Title
        Artist                  = From.Artist
        year                    = From.year
        filedate                = From.filedate           
        genre                   = From.genre
        playcount               = From.playcount
        lastplayed              = From.lastplayed
        runtime                 = From.runtime
        Integer.TryParse(runtime.Replace(" min",""),IntRuntime)
        CreateDate              = From.Createdate
        plot                    = From.plot
        TitleUcase              = From.Title.ToUpper            
        Resolution              = From.Resolution
        thumb                   = From.thumb
        track                   = From.track
        FrodoPosterExists       = From.FrodoPosterExists
        PreFrodoPosterExists    = From.PreFrodoPosterExists
        AssignAudio(From.Audio)
    End Sub

    Public Sub Assign(From As FullMovieDetails )

        nfopathandfilename      = From.fileinfo.fullpathandfilename
        filename                = Path.GetFileName(From.fileinfo.fullpathandfilename)
        foldername              = Utilities.GetLastFolder(From.fileinfo.fullpathandfilename)
        Title                   = From.fullmoviebody.title
        Artist                  = From.fullmoviebody.artist
        year                    = From.fullmoviebody.year.ToInt
        Dim filecreation As New IO.FileInfo(From.fileinfo.fullpathandfilename)
        Try
            filedate = Format(filecreation.LastWriteTime, Preferences.datePattern).ToString
        Catch ex As Exception
        End Try            
        genre                   = From.fullmoviebody.genre
        playcount               = From.fullmoviebody.playcount
        lastplayed              = From.fullmoviebody.lastplayed
        runtime                 = From.fullmoviebody.runtime
        If String.IsNullOrEmpty(From.fileinfo.createdate) Then
            Createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
        Else
            Createdate = From.fileinfo.createdate
        End If
        plot                    = From.fullmoviebody.plot 
        TitleUcase              = From.fullmoviebody.title.ToUpper
        Integer.TryParse(runtime.Replace(" min",""),IntRuntime)          
        Resolution              = From.filedetails.filedetails_video.VideoResolution
        thumb                   = From.fullmoviebody.thumb
        track                   = From.fullmoviebody.track 
        AssignMissingData(From)
        AssignAudio(From.filedetails.filedetails_audio)
    End Sub

    Public Sub AssignAudio(From As List(Of AudioDetails))
        Audio.Clear
        Audio.AddRange(From)
    End Sub

    Public Sub AssignMissingData(From As FullMovieDetails )
        FrodoPosterExists = Preferences.FrodoPosterExists(From.fileinfo.fullpathandfilename)
        PreFrodoPosterExists = Preferences.PreFrodoPosterExists(From.fileinfo.fullpathandfilename)
    End Sub
End Class
