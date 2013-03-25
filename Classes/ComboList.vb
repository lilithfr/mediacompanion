
Public Class ComboList

    Property fullpathandfilename As String = "" 
    Property MovieSet            As String = ""
    Property filename            As String = ""
    Property foldername          As String = ""
    Property title               As String = ""
    Property originaltitle       As String = ""
    Property year                As String = ""
    Property filedate            As String = ""
    Property id                  As String = ""
    Property rating              As String = ""
    Property top250              As String = 0
    Property genre               As String = ""
    Property playcount           As String = ""
    Property sortorder           As String = ""
    Property outline             As String = ""
    Property runtime             As String = ""
    Property createdate          As String = ""
    Property missingdata1        As Byte   = 0
    Property plot                As String = ""
    Property source              As String = ""
    Property votes               As String = ""
    Property Resolution          As Integer= -1
    Property Audio               As New List(Of AudioDetails)

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


    Public ReadOnly Property MissingRating As Boolean
        Get
            Return rating.ToString.Trim=""
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
            Return votes.ToString.Trim=""
        End Get
    End Property  


    Public ReadOnly Property MissingYear As Boolean
        Get
            Return year.ToString.Trim=""
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


    Public Sub Assign(From As ComboList)
        Me.fullpathandfilename = From.fullpathandfilename
        Me.MovieSet            = From.MovieSet           
        Me.filename            = From.filename           
        Me.foldername          = From.foldername         
        Me.title               = From.title              
        Me.originaltitle       = From.originaltitle      
        Me.year                = From.year               
        Me.filedate            = From.filedate           
        Me.id                  = From.id                 
        Me.rating              = From.rating             
        Me.top250              = From.top250             
        Me.genre               = From.genre              
        Me.playcount           = From.playcount          
        Me.sortorder           = From.sortorder          
        Me.outline             = From.outline            
        Me.runtime             = From.runtime            
        Me.createdate          = From.createdate         
        Me.missingdata1        = From.missingdata1       
        Me.plot                = From.plot               
        Me.source              = From.source             
        Me.votes               = From.votes              
        Me.Resolution          = From.Resolution    
        
        AssignAudio(From.Audio)
    End Sub

    Public Sub AssignAudio(From As List(Of AudioDetails))
        Me.Audio.Clear
        Me.Audio.AddRange(From)
    End Sub

End Class
