
Public Class ComboList

    Property fullpathandfilename As String = "" 
    Property MovieSet            As String = ""
    Property filename            As String = ""
    Property foldername          As String = ""
    Property title               As String = ""
    Property originaltitle       As String = ""
    Property year                As String  = ""
    Property filedate            As String  = ""
    Property id                  As String  = ""
    Property rating              As String  = ""
    Property top250              As String  = 0
    Property genre               As String  = ""
    Property playcount           As String  = ""
    Property sortorder           As String  = ""
    Property outline             As String  = ""
    Property runtime             As String  = ""
    Property createdate          As String  = ""
    Property missingdata1        As Byte    = 0
    Property plot                As String  = ""
    Property source              As String  = ""
    Property votes               As String  = ""
    Property Resolution          As Integer = -1

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
    End Sub

End Class
