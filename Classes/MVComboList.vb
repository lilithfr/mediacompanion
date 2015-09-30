Imports System.IO
Imports XBMC.JsonRpc
Imports System.Text.RegularExpressions

Public Class MVComboList

    Property fullpathandfilename  As String = ""
    Property filename             As String = ""
    Property foldername           As String = ""
    Private _title               As String = ""
    Property artist               As String = ""
    Property year                 As Integer= 0
    Property filedate             As String = ""
    'Property rating               As Double = 0
    Property genre                As String = ""
    Property playcount            As String = ""
    Property lastplayed           As String = ""
    Property runtime              As String = ""
    Property createdate           As String = ""
    Property plot                 As String = ""
    'Property Votes                As Integer= 0
    Property Resolution           As Integer= -1
    Property Audio                As New List(Of AudioDetails)
    Property track                As String = ""
    Private _thumb                As String = ""
    Property FrodoPosterExists    As Boolean
    Property PreFrodoPosterExists As Boolean

    Public Property title As String
        Get
            Return _title
        End Get
        Set
            _title = Value.SafeTrim
        End Set
    End Property

    Public Property thumb As String
        Get
            Return _thumb
        End Get
        Set(value As String)
            _thumb = Value
        End Set
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
    
    'Public ReadOnly Property MissingRating As Boolean
    '    Get
    '        Return rating=0     '.ToString.Trim=""
    '    End Get
    'End Property  

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

    Public ReadOnly Property MoviePathAndFileName As String
        Get
            Return Utilities.GetFileName(fullpathandfilename,True)
        End Get
    End Property  

    'ReadOnly Property FrodoPosterExists As Boolean
    '    Get
    '        Return Preferences.FrodoPosterExists(fullpathandfilename)
    '    End Get
    'End Property

    ReadOnly Property ActualNfoFileName As String
        Get
            Return Path.GetFileName(fullpathandfilename)
        End Get
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

    Public Sub Assign(From As MVComboList)

        Me.fullpathandfilename  = From.fullpathandfilename          
        Me.filename             = From.filename           
        Me.foldername           = From.foldername         
        Me.title                = From.title
        Me.artist               = From.artist
        Me.year                 = From.year               
        Me.filedate             = From.filedate                 
        'Me.rating               = From.rating             
        Me.genre                = From.genre
        Me.playcount            = From.playcount 
        Me.lastplayed           = From.lastplayed           
        Me.runtime              = From.runtime            
        Me.createdate           = From.createdate        
        Me.plot                 = From.plot            
        'Me.Votes                = From.Votes              
        Me.Resolution           = From.Resolution
        Me.thumb                = From.thumb
        Me.track                = From.track
        Me.FrodoPosterExists    = From.FrodoPosterExists
        Me.PreFrodoPosterExists = From.PreFrodoPosterExists

        AssignAudio(From.Audio)
    End Sub

    Public Sub Assign(From As FullMovieDetails )

        Me.fullpathandfilename  = From.fileinfo.fullpathandfilename
        Me.filename             = Path.GetFileName(From.fileinfo.fullpathandfilename)
        Me.foldername           = Utilities.GetLastFolder(From.fileinfo.fullpathandfilename)
        Me.title                = From.fullmoviebody.title
        Me.artist               = From.fullmoviebody.artist
        Me.year                 = From.fullmoviebody.year
        Dim filecreation As New IO.FileInfo(From.fileinfo.fullpathandfilename)
        Try
            Me.filedate = Format(filecreation.LastWriteTime, Preferences.datePattern).ToString
        Catch ex As Exception
        End Try
        'Me.rating               = From.rating             
        Me.genre                = From.fullmoviebody.genre
        Me.playcount            = From.fullmoviebody.playcount
        Me.lastplayed           = From.fullmoviebody.lastplayed
        Me.runtime              = From.fullmoviebody.runtime
        If String.IsNullOrEmpty(From.fileinfo.createdate) Then
            Me.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
        Else
            Me.createdate = From.fileinfo.createdate
        End If
        Me.plot                 = From.fullmoviebody.plot
        'Me.Votes                = From.Votes              
        Me.Resolution           = From.filedetails.filedetails_video.VideoResolution
        Me.thumb                = From.fullmoviebody.thumb
        Me.track                = From.fullmoviebody.track 
        AssignMissingData(From)
        AssignAudio(From.filedetails.filedetails_audio)
    End Sub

    Public Sub AssignAudio(From As List(Of AudioDetails))
        Me.Audio.Clear
        Me.Audio.AddRange(From)
    End Sub

    Public Sub AssignMissingData(From As FullMovieDetails )
        Me.FrodoPosterExists = Preferences.FrodoPosterExists(From.fileinfo.fullpathandfilename)
        Me.PreFrodoPosterExists = Preferences.PreFrodoPosterExists(From.fileinfo.fullpathandfilename)
    End Sub


End Class
