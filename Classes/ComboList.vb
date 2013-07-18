Imports System.IO
Imports XBMC.JsonRpc
Imports System.Text.RegularExpressions

Public Class ComboList

    Property fullpathandfilename  As String = "" 
    Property MovieSet             As String = ""
    Property filename             As String = ""
    Property foldername           As String = ""
    Property _title               As String = ""
    Property _originaltitle       As String = ""
    Property year                 As Integer= 0
    Property filedate             As String = ""
    Property id                   As String = ""
    Property rating               As Double = 0
    Property top250               As String = 0
    Property genre                As String = ""
    Property tag                  As List(of String) = New List(Of String)
    Property playcount            As String = ""
    Property sortorder            As String = ""
    Property outline              As String = ""
    Property runtime              As String = ""
    Property createdate           As String = ""
    Property missingdata1         As Byte   = 0
    Property plot                 As String = ""
    Property source               As String = ""
    Property Votes                As Integer= 0
    Property Resolution           As Integer= -1
    Property Audio                As New List(Of AudioDetails)
    Property Premiered            As String = ""
    Property Certificate          As String = ""
    Property XbmcMovie            As XbmcMovieForCompare
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

    Public Property originaltitle As String
        Get
            Return _originaltitle
        End Get
        Set
            _originaltitle = Value.SafeTrim
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

    Public ReadOnly Property ImdbInFolderName As Boolean
        Get
            Return Regex.Match(foldername,"(tt\d{7})").Success
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


    Public ReadOnly Property MissingRating As Boolean
        Get
            Return rating=0     '.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingCertificate As Boolean
        Get
            Return Certificate.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingGenre As Boolean
        Get
            Return genre.ToString.Trim=""
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
            Return Votes=0          '.ToString.Trim=""
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

    Public Sub Assign(From As ComboList)

        Me.fullpathandfilename  = From.fullpathandfilename
        Me.MovieSet             = From.MovieSet           
        Me.filename             = From.filename           
        Me.foldername           = From.foldername         
        Me.title                = From.title
        Me.originaltitle        = From.originaltitle    
        Me.year                 = From.year               
        Me.filedate             = From.filedate           
        Me.id                   = From.id                 
        Me.rating               = From.rating             
        Me.top250               = From.top250             
        Me.genre                = From.genre  
        Me.tag                  = From.tag    
        Me.playcount            = From.playcount          
        Me.sortorder            = From.sortorder          
        Me.outline              = From.outline            
        Me.runtime              = From.runtime            
        Me.createdate           = From.createdate         
        Me.missingdata1         = From.missingdata1       
        Me.plot                 = From.plot               
        Me.source               = From.source             
        Me.Votes                = From.Votes              
        Me.Resolution           = From.Resolution    
        Me.Premiered            = From.Premiered
        Me.Certificate          = From.Certificate
        Me.FrodoPosterExists    = From.FrodoPosterExists
        Me.PreFrodoPosterExists = From.PreFrodoPosterExists

        AssignAudio(From.Audio)
    End Sub

    Public Sub AssignAudio(From As List(Of AudioDetails))
        Me.Audio.Clear
        Me.Audio.AddRange(From)
    End Sub

End Class
