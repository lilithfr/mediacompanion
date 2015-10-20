Imports System.ComponentModel 
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Threading 
Imports Media_Companion
Imports System.Text
Imports YouTubeFisher
Imports Media_Companion.Preferences
Imports XBMC.JsonRpc

Public Class Movie

    Public Const ResolutionsFile = "Resolutions.xml"
    Const MSG_PREFIX    = ""
    Public Const MSG_ERROR = "-ERROR! "
    Const MSG_OK    = "-OK "
    Const MSG_DONT_RESIZE = "Don't resize"
    Const YOU_TUBE_URL_PREFIX = "http://www.youtube.com/watch?v="
    Public movRebuildCaches As Boolean = Movies.movRebuildCaches
    
    Public Delegate Sub ActionDelegate()

    Class ScrapeActions
        Property Items As New List(Of ScrapeAction)
    End Class
    
    Class ScrapeAction
        Property Action     As ActionDelegate
        Property ActionName As String
        Property Time       As New Times

        Sub New(ByVal action As ActionDelegate, actionName As String)
            Me.Action     = action
            Me.ActionName = actionName
        End Sub

        Sub Run
            Time.StartTm = DateTime.Now
            _action
            Time.EndTm   = DateTime.Now
        End Sub
    End Class

    Public Event AmountDownloadedChanged (ByVal iNewProgress As Long)
    Public Event FileDownloadSizeObtained(ByVal iFileSize    As Long)
    Public Event FileDownloadComplete    ()
    Public Event FileDownloadFailed      (ByVal ex As Exception)
    Public Event ProgressLogChanged      (ByVal oProgress As Progress)
    
    Private WithEvents _WebFileDownloader As New WebFileDownloader

    Private _nfoFunction              As New WorkingWithNfoFiles
    Private _imdbCounter              As Integer =  0
    Private _imdbBody                 As String  = ""
    Property Scraped                  As Boolean = False
    Property MovieSearchEngine        As String = ""
    Private _scrapedMovie             As New FullMovieDetails
    Private _rescrapedMovie           As FullMovieDetails
    Public  _movieCache               As New ComboList
    Private _certificates             As New List(Of String)
    Private _imdbScraper              As New Classimdb
    Private _scraperFunctions         As New ScraperFunctions
    Private _titleFull                As String = ""
    Private _title                    As String = ""
    Private _parent                   As Movies
    Private _possibleImdb             As String = String.Empty
    Private _youTubeTrailer           As YouTubeVideoFile
    Private _nfoPathAndFilename       As String = ""
    Private _actualNfoPathAndFilename As String
    Private _videotsrootpath          As String =""
    Private _rescrape                 As Boolean = False
    Private _previousCache            As ComboList = Nothing
    Private _triedUrls               As New List(Of String)
    Private _trailerUrl               As String =""
    Public _mvsearchname              As String = ""
     
    Shared Private _availableHeightResolutions As List(Of Integer)

    #Region "Read-write properties"
    Property tmdb                 As TMDb 
    Property mediapathandfilename As String = ""
    Property TimingsLog           As String = ""
    Property ScrapeTimingsLogThreshold As Integer = Preferences.ScrapeTimingsLogThreshold
    Property LogScrapeTimes            As Boolean = Preferences.LogScrapeTimes
'   Property TrailerUrl           As String = ""
    Property PosterUrl            As String = ""
    Property SeparateMovie As String = ""
    Property ThreeDKeep As String = ""
    Property Actions As New ScrapeActions
'    Property nfopathandfilename As String = ""
    Property RenamedBaseName As String = ""

    Property TrailerUrl As String
        Get
            Return _trailerUrl
        End Get
        Set
            _trailerUrl = ""

            If Value="" Then Return

            If _triedUrls.Contains(Value) Then Return

            _triedUrls.Add(Value)   
            _trailerUrl = Value
        End Set
    End Property

    Property GetTrailerUrlAlreadyRun As Boolean = False

    Property Rescrape As Boolean
        Get
            Return _rescrape
        End Get

        Set(value As Boolean)

            _rescrape = value

            If _rescrape Then
                _previousCache = _parent.FindCachedMovie(_actualNfoPathAndFilename)
            Else
                _previousCache = Nothing
            End If
        End Set
    End Property

#End Region 'Read-write properties


    #Region "Read-only properties"
    
    ReadOnly Property NfoPathAndFilename As String
        Get
           If RenamedBaseName <>"" Then Return RenamedBaseName & ".nfo"

           If _nfoPathAndFilename = "" Then
                Dim movieStackName = mediapathandfilename
                Dim firstPart As Boolean
                Dim i As Integer = mediapathandfilename.LastIndexOf(Extension)

                _nfoPathAndFilename = mediapathandfilename.Substring(0, i) & ".nfo"    'Replace(Extension, ".nfo")

                If Utilities.isMultiPartMedia(movieStackName, False, firstPart) Then
                    If Preferences.namemode <> "1" Then
                        _nfoPathAndFilename = NfoPath & movieStackName & ".nfo"
                    End If
                End If
            End If

            Return _nfoPathAndFilename
        End Get
    End Property

    ReadOnly Property PosterCachePath As String
        Get
            Return Path.Combine(Preferences.applicationPath, "settings\postercache\" & Utilities.GetCRC32(NfoPathPrefName) & ".jpg")
        End Get
    End Property

    Public ReadOnly Property YouTubeTrailer As YouTubeVideoFile
        Get
            Return _youTubeTrailer
        End Get
    End Property

    Shared Public ReadOnly Property Resolutions As XDocument
        Get
            Return XDocument.Load(Preferences.applicationPath & "\Assets\" & ResolutionsFile)
        End Get 
    End Property

    Shared Public ReadOnly Property AvailableHeightResolutions As List(Of Integer)
        Get
            If IsNothing(_availableHeightResolutions) then
                _availableHeightResolutions = From x In Resolutions.Descendants("Resolution")
                    Select 
                        height = Convert.ToInt32(x.Attribute("height").Value)
                    Order By
                        height
                    Distinct.ToList
            End If

            Return _availableHeightResolutions
        End Get
    End Property

    Shared Sub LoadBackDropResolutionOptions(ByRef cb As ComboBox, Optional selectedIndex As Integer=0)
        cb.Items.Clear
        cb.Items.Add(MSG_DONT_RESIZE)
        
        Dim qry = From x In Resolutions.Descendants("Resolution")
           Select name = x.Attribute("name").Value + " (" + x.Attribute("aspectRatio").Value + ") (" + x.Attribute("width").Value + "," + x.Attribute("height").Value + ")"

        For Each item In qry
            cb.Items.Add( item )
        Next

        cb.SelectedIndex = selectedIndex
    End Sub

    Shared Function GetBackDropResolution(selectedIndex As Integer) As Point

        'Don't resize selected
        If selectedIndex=0 then Return New Point(0,0)

        Dim row = Resolutions.Descendants("Resolution").ElementAt(selectedIndex-1)
        
        Dim width  = Convert.ToInt32(row.Attribute("width" ).Value)
        Dim height = Convert.ToInt32(row.Attribute("height").Value)

        Return New Point(width,height)

    End Function

    Shared Function GetHeightResolution(selectedIndex As Integer) As Integer

        'Don't resize selected
        If selectedIndex=0 then Return 0

        Return AvailableHeightResolutions(selectedIndex-1)
    End Function
 
    Shared Sub LoadHeightResolutionOptions(ByRef cb As ComboBox, Optional selectedIndex As Integer=0)
        cb.Items.Clear
        cb.Items.Add(MSG_DONT_RESIZE)

        'Dim qry = From x In Resolutions.Descendants("Resolution")
        '            Select 
        '                height = Convert.ToInt16(x.Attribute("height").Value)
        '            Order By
        '                height
        '            Distinct

        For Each item In AvailableHeightResolutions
            cb.Items.Add(item)
        Next

        cb.SelectedIndex = selectedIndex
    End Sub

    'Shared Function GetHeightResolution(selectedIndex As Integer) As Integer

    '    Dim row = Resolutions.Descendants("Resolution").ElementAt(selectedIndex)

    '    Return Convert.ToInt32(row.Attribute("height").Value)

    'End Function

    ReadOnly Property TrailerExists As Boolean
        Get
            Return File.Exists(ActualTrailerPath)
        End Get
    End Property

    ReadOnly Property ActualTrailerPath As String
        Get
            Dim s = NfoPathPrefName
            Dim FileName As String = ""
        
            For Each item In Utilities.TrailerExtensions '"mp4,flv,webm,mov,m4v".Split(",")
                FileName = IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), Path.GetFileNameWithoutExtension(s) & "-trailer" & item)

                If File.Exists(FileName) Then Return FileName
            Next

            Return IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), Path.GetFileNameWithoutExtension(s) & "-trailer.flv")
        End Get
    End Property

    ReadOnly Property ActualBaseName As String
        Get
            Dim pos As Integer=0
            Try
                
                Dim BaseName = mediapathandfilename.Substring(0, mediapathandfilename.LastIndexOf(Extension))              'Replace(Extension,"")

                pos = 1

                'Long name
                If File.Exists(BaseName & ".nfo") Then Return BaseName

                Dim movieStackName = mediapathandfilename
                Dim firstPart As Boolean

                pos = 2

                Utilities.isMultiPartMedia(movieStackName, False, firstPart)

                pos = 3

                'Short name
                If File.Exists(NfoPath & movieStackName & ".nfo") Then Return NfoPath & movieStackName

                pos = 4

                Return "unknown"
            Catch ex As Exception
                Dim paramInfo As String = "mediapathandfilename: [" & mediapathandfilename & "] Pos: [" & pos & "] Extension : [" & Extension & "]"
                ReportProgress(MSG_ERROR,"!!! Exception thrown in ReadOnly Property ActualBaseName" & vbCrLf & "!!! Exception : [" & ex.ToString & "]" & vbCrLf & vbCrLf & "Param info : [" & paramInfo & "]" & vbCrLf & vbCrLf)
                Return "unknown"
            End Try
        End Get
    End Property

    ReadOnly Property ActualNfoPathAndFilename As String
        Get
            If Not IsNothing(_actualNfoPathAndFilename) Then Return _actualNfoPathAndFilename

            Return ActualBaseName & ".nfo"
        End Get
    End Property

    Public ReadOnly Property ActualPosterPath As String
        Get
            Dim s = Preferences.GetPosterPath(NfoPathPrefName)

            If File.Exists(s) Then Return s
            If Preferences.basicsavemode then
                s= NfoPathPrefName.Replace(".nfo",".tbn")
                Return s
            End If

            Return ActualBaseName & ".tbn"
        End Get 
    End Property

    Public ReadOnly Property ActualPosterPaths As List(Of String)
        Get
            Return Preferences.GetAllPosters(ScrapedMovie.fileinfo.fullpathandfilename)

            'Dim PostPaths As List(Of String) = New List(Of String)

            'Try
            '    For Each item In Preferences.GetPosterPaths(ScrapedMovie.fileinfo.fullpathandfilename,ScrapedMovie.fileinfo.videotspath)
            '        If File.Exists(item) Then PostPaths.Add(item)
            '    Next
            'Catch ex As Exception
            '    Dim x = ex
            'End Try

            'Return PostPaths
        End Get 
    End Property

    Public ReadOnly Property ActualFanartPath As String
        Get
            Dim s = Preferences.GetFanartPath(NfoPathPrefName)

            If File.Exists(s) Then Return s
            If Preferences.basicsavemode then
                s= NfoPathPrefName.Replace("movie.nfo","fanart.jpg")
                Return s
            End If
            'If Preferences.fanartjpg Then
            '    s=IO.Path.GetDirectoryName(NfoPathPrefName) & "\fanart.jpg
            'End If

            Return ActualBaseName & "-fanart.jpg"
        End Get 
    End Property

    ReadOnly Property NfoPath As String
        Get
            Dim result As String = NfoPath_NoDirectorySeparatorChar
            Dim dirsepchar As String = IO.Path.DirectorySeparatorChar
            If result.LastIndexOf(dirsepchar) <> result.Length-1 Then result = result & dirsepchar
            'Return NfoPath_NoDirectorySeparatorChar & IO.Path.DirectorySeparatorChar
            Return result
        End Get
    End Property

    ReadOnly Property NfoPath_NoDirectorySeparatorChar As String
        Get
            Return Path.GetDirectoryName(mediapathandfilename)
        End Get
    End Property

    ReadOnly Property ActorPath As String
        Get
            Return nfopathandfilename.Replace(IO.Path.GetFileName(nfopathandfilename), "") & ".actors\"
        End Get
    End Property

    ReadOnly Property TempOfflinePathAndFileName As String
        Get
            Return Path.Combine(mediapathandfilename.Replace(Path.GetFileName(mediapathandfilename), ""), "tempoffline.ttt")
        End Get
    End Property

    ReadOnly Property Actors As List(Of ActorDatabase)
        Get
            Dim x As New List(Of ActorDatabase )
            Try
                Dim q = From actor In _scrapedMovie.listactors Select New ActorDatabase(actor.actorname,_scrapedMovie.fullmoviebody.imdbid)

                Return q.ToList
            Catch
                Return x
            End Try
        End Get
    End Property

    ReadOnly Property Director As DirectorDatabase
        Get
            'Dim x As New List(Of DirectorDatabase)
            Dim x As New DirectorDatabase 
            Try
                If String.IsNullOrEmpty(_scrapedMovie.fullmoviebody.director) Then Return Nothing
                Return New DirectorDatabase(_scrapedMovie.fullmoviebody.director,_scrapedMovie.fullmoviebody.imdbid)
            Catch
                Return x
            End Try
        End Get
    End Property

    ReadOnly Property MovieSet As MovieSetDatabase 
        Get
            Try
                If _scrapedMovie.fullmoviebody.movieset.MovieSetName = "-None-" Then Return Nothing
                Return New MovieSetDatabase(_scrapedMovie.fullmoviebody.movieset.MovieSetName,_scrapedMovie.fullmoviebody.movieset.MovieSetId, New List(Of CollectionMovie))
            Catch
                Return Nothing
            End Try
        End Get
    End Property

    ReadOnly Property MissingLocalActors As Boolean
        Get
            For Each Actor In _scrapedMovie.listactors

                If Actor.actorthumb="" Then Continue For

                Dim ActorPath = GetActorPath(_movieCache.fullpathandfilename, actor.actorname, Actor.actorid)

                If Not File.Exists(ActorPath) Then Return True
            Next

            Return False
        End Get
    End Property

    ReadOnly Property TitleFull As String
        Get
            If _titleFull = "" Then
                Dim movieStackName As String = mediapathandfilename
                Dim firstPart As Boolean

                _titleFull = mediapathandfilename
                If Utilities.isMultiPartMedia(movieStackName, False, firstPart) Then
                    If Preferences.namemode <> "1" Then
                        _titleFull = NfoPath & movieStackName & Extension
                    End If
                End If
            End If

            Return _titleFull
        End Get
    End Property

    ReadOnly Property Extension As String
        Get
            Return Path.GetExtension(mediapathandfilename)
        End Get
    End Property

    ReadOnly Property Title As String
        Get
            If Preferences.usefoldernames Or Extension.ToLower = ".ifo" Or Extension.ToLower = ".bdmv" Then
                If Preferences.GetRootFolderCheck(NfoPathAndFilename) Then
                    Return Path.GetFileNameWithoutExtension(TitleFull)
                Else
                    Return Utilities.GetLastFolder(nfopathandfilename)
                End If
            Else
                Dim tmpTitle As String = TitleFull
                If Extension.ToLower = ".disc" Then
                    tmpTitle = tmpTitle.Replace(".disc","")
                    Dim p = tmpTitle.LastIndexOf(".")
                    If p >1 Then
                        tmpTitle = tmpTitle.Substring(0, p)
                    End If
                End If
                Return Path.GetFileNameWithoutExtension(tmpTitle)
                
                'Return _title 'Path.GetFileNameWithoutExtension(TitleFull) 
            End If
        End Get
    End Property

    Public Property MVSearchName
         Get
            Return _mvsearchname
        End Get
        Set(value)
            _mvsearchname = value
        End Set
    End Property

    Public ReadOnly Property Certificates As List(Of String)
        Get
            Scrape
            Return _certificates
        End Get 
    End Property
    
    Public ReadOnly Property Cache As ComboList
        Get
            Scrape
            Return _movieCache
        End Get 
    End Property
    
    Public ReadOnly Property SearchName As String
        Get
            Return Utilities.CleanFileName(Title, If(Preferences.movies_useXBMC_Scraper, "tmdb", ""))
        End Get 
    End Property

    Public ReadOnly Property NfoPathPrefName As String
        Get
            If Rescrape Then Return ActualNfoPathAndFilename

            If Preferences.basicsavemode Then
                Return nfopathandfilename.Replace(Path.GetFileName(nfopathandfilename), "movie.nfo")
            Else
                Return nfopathandfilename
            End If
        End Get 
    End Property

    Public ReadOnly Property PosterPath As String
        Get
            If Rescrape Then Return ActualPosterPath

            Return Preferences.GetPosterPath(NfoPathPrefName)
        End Get 
    End Property

    Public ReadOnly Property PosterDVDFrodo As String
        Get
            Return Preferences.FrodoPosterPath(NfoPathPrefName)
        End Get
    End Property

    ReadOnly Property FrodoPosterExists As Boolean
        Get
            Return Preferences.FrodoPosterExists(NfoPathPrefName)
        End Get
    End Property

    ReadOnly Property PreFrodoPosterExists As Boolean
        Get
            Return Preferences.PreFrodoPosterExists(NfoPathPrefName)
        End Get
    End Property

    Public ReadOnly Property FanartPath As String
        Get
            If Rescrape Then Return ActualFanartPath

            Return Preferences.GetFanartPath(NfoPathPrefName)
        End Get 
    End Property

    Public ReadOnly Property FanartDVDFrodo As String
        Get
            Return Preferences.FrodoFanartPath(NfoPathPrefName)
        End Get
    End Property

    Public ReadOnly Property PossibleImdb As String
        Get
            If String.IsNullOrEmpty(_possibleImdb) Then
                Dim log = ""
                _possibleImdb = getExtraIdFromNFO(NfoPathAndFilename, log)

                ReportProgress(, log)
                If String.IsNullOrEmpty(_possibleImdb) Then
                    Dim mat As Match = Regex.Match(NfoPathAndFilename, "(tt\d{7})")

                    If mat.Success Then
                        _possibleImdb = mat.Value
                    End If
                End If
            End If
                 
            Return _possibleImdb        
        End Get
    End Property

    Public ReadOnly Property PossibleYear As String
        Get
            Dim s As String = ""
            If Preferences.usefoldernames Then
                s = Title
            Else
                s = nfopathandfilename 
            End If
            Dim M As Match = Regex.Match(s, "[\(\[]([\d]{4})[\)\]]")
            If M.Success = True Then
                Return M.Groups(1).Value
            End If
            Dim N As Match = Regex.Match(s, "\.([\d]{4})\.")
            If N.Success = True Then
                Return N.Groups(1).Value
            End If

            Return ""
        End Get
    End Property

    Public ReadOnly Property ImdbBody As String
        Get
            Scrape
            Return _imdbBody
        End Get
    End Property

    Public ReadOnly Property TrailerPath As String
        Get
            Return Path.Combine(NfoPathPrefName.Replace(Path.GetFileName(NfoPathPrefName),""), Path.GetFileNameWithoutExtension(NfoPathPrefName) & "-trailer." & TrailerPathExtension)
        End Get
    End Property

    Public ReadOnly Property TrailerPathExtension As String
        Get
            If Not IsNothing(_youTubeTrailer) Then
                Return _youTubeTrailer.Extension
            End If
            Return "flv"
        End Get
    End Property

    Public ReadOnly Property ScrapedMovie As FullMovieDetails
        Get
            Scrape
            Return _scrapedMovie
        End Get
    End Property

    Public ReadOnly Property TempOffLineFileName As String
        Get
            Return _movieCache.fullpathandfilename.Replace(Path.GetFileName(_movieCache.fullpathandfilename), "tempoffline.ttt")
        End Get
    End Property
    #End Region 'Read-only Properties


    #Region "Shared functions"

    Public Shared Function IsValidMovieFile(fileInfo As IO.FileInfo, Optional ByRef log As String = "") As Boolean

        Dim titleDir As String = fileInfo.Directory.ToString & IO.Path.DirectorySeparatorChar

        If fileInfo.Extension.ToLower = ".vob" Then  'Check if DVD Structure
            If IO.File.Exists(titleDir & "video_ts.ifo") Then
                Return False
            End If

            Dim PartNumberIndex = fileInfo.Name.LastIndexOf(".") - 1
            If PartNumberIndex > 0 Then
                Dim PartNumberChar = fileInfo.Name.Substring(PartNumberIndex, 1)
                If Integer.TryParse(PartNumberChar, 0) Then
                    Dim PreviousPartNumber = Convert.ToInt32(PartNumberChar) - 1

                    If PreviousPartNumber >= 0 Then
                        Dim PreviousPartFileName = ChangeCharacter(fileInfo.Name, Convert.ToChar(Convert.ToChar(PreviousPartNumber.ToString)), PartNumberIndex)
                        Dim PreviousPartFileExists = File.Exists(titleDir & PreviousPartFileName)

                        If PreviousPartFileExists Then Return False
                    End If    

                    Return Not Utilities.findFileOfType(fileInfo.FullName, ".nfo")
                End If
                log &= "Part number not found - Assuming just the one part!"
                'Test if matching nfo is present
                Dim movieNfoFilevob As String = fileInfo.FullName
                If Utilities.findFileOfType(movieNfoFilevob, ".nfo",Preferences.basicsavemode) Then
                    Try
                        Dim filechck As StreamReader = File.OpenText(movieNfoFilevob)
                        Dim tempstring As String
                        Do
                            tempstring = filechck.ReadLine
                            If tempstring = Nothing Then Exit Do
                            If tempstring.IndexOf("<movie>") <> -1 Then
                                log &= " - valid MC .nfo found - scrape skipped!"
                                Return False
                            End If
                        Loop Until filechck.EndOfStream
                        filechck.Close()
                        filechck = Nothing
                    Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                    End Try
                End If
                Return True
            End If
            log &= "??? File extension missing!"
            Return False
        End If

        If fileInfo.Name.Substring(0,2)="._" Then 
            log &= fileInfo.Name & " ignored"
            Return False 
        End If

        If Preferences.usefoldernames = True Then
            log &= "  '" & fileInfo.Directory.Name.ToString & "'"     'log directory name as Title due to use FOLDERNAMES
        Else
            log &= "  '" & fileInfo.ToString & "'"                    'log title name
        End If

        Dim movieNfoFile As String = fileInfo.FullName
        If Utilities.findFileOfType(movieNfoFile, ".nfo",Preferences.basicsavemode) Then
            Try
                Dim filechck As StreamReader = File.OpenText(movieNfoFile)
                Dim Searchstring As String = "<movie>"
                If Preferences.MusicVidScrape Then Searchstring = "<musicvideo>"
                Dim tempstring As String
                Do
                    tempstring = filechck.ReadLine
                    If tempstring = Nothing Then Exit Do
                    If tempstring.IndexOf(Searchstring) <> -1 Then
                        log &= " - valid MC .nfo found - scrape skipped!"
                        Return False
                    End If
                Loop Until filechck.EndOfStream
                filechck.Close()
                filechck = Nothing
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        End If

        'ignore trailers
        Dim M As Match
        M = Regex.Match(fileInfo.FullName, "[-_.]trailer")
        If M.Success Then
            log &= " - ignore trailer"
            Return False
        End If

        'ignore whatever this is meant to be!
        If fileInfo.FullName.ToLower.IndexOf("sample") <> -1 And fileInfo.FullName.ToLower.IndexOf("people") = -1 Then 
            Return False
        End If

        If fileInfo.FullName.ToLower.Contains(".bdmv") Then
            If fileInfo.Name.ToString.ToLower <> "index.bdmv" Then Return False
        End If

        If fileInfo.Extension = ".ttt" Then
            Return False
        End If
        If fileInfo.Extension = ".rar" Then
            Dim SizeOfFile As Integer = FileLen(fileInfo.FullName)
            Dim tempint2 = Convert.ToInt32(Preferences.rarsize) * 1048576
            If SizeOfFile < tempint2 Then Return False
        End If
        Dim movieStackName As String = fileInfo.FullName
        Dim firstPart      As Boolean

        If Utilities.isMultiPartMedia(movieStackName, False, firstPart) Then
            If Not firstPart Then 
             Return False
            End If
        End If

        Return true
    End Function
    #End Region 'Shared functions


    #Region "Constructors"   
    Sub New
    End Sub
 

    Sub New( parent As Movies, NfoName As String )
        Me.New
        _parent                   = parent
        _actualNfoPathAndFilename = NfoName
        mediapathandfilename      = Utilities.GetFileName(NfoName,True)
    End Sub


    Sub New( FullName As String, parent As Movies )
        Me.New
        _parent              = parent
        mediapathandfilename = FullName
      '  nfopathandfilename = mediapathandfilename.Replace(Extension, ".nfo")
    End Sub

    Sub New( parent As ucMusicVideo, NfoName As String )
        Me.New
        '_parent                   = parent
        _actualNfoPathAndFilename = NfoName
        mediapathandfilename      = Utilities.GetFileName(NfoName,True)
    End Sub
    #End Region 'Constructors


    #Region "Subs"

    Public Shared Function ChangeCharacter(s As String, replaceWith As Char, idx As Integer) As String
        Dim sb As New StringBuilder(s)

        sb(idx) = replaceWith

        Return sb.ToString
    End Function

    Sub AppendScrapeSuccessActions
        Actions.Items.Add( New ScrapeAction(AddressOf AssignScrapedMovie          , "Assign scraped movie"      ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignHdTags                , "Assign HD Tags"            ) )
        Actions.Items.Add( New ScrapeAction(AddressOf GetKeyWords                 , "Get Keywords for tags"     ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DoRename                    , "Rename"                    ) )
        Actions.Items.Add( New ScrapeAction(AddressOf GetActors                   , "Actors scraper"            ) ) 'GetImdbActors
        Actions.Items.Add( New ScrapeAction(AddressOf AssignTrailerUrl            , "Get trailer URL"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoPosterThumbs        , "Getting extra Frodo Poster thumbs") )
        Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoFanartThumbs        , "Getting extra Frodo Fanart thumbs") )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignPosterUrls            , "Get poster URLs"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf TidyUpAnyUnscrapedFields    , "Tidy up unscraped fields"  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                     , "Save Nfo"                  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadPoster              , "Poster download"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadFanart              , "Fanart download"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadMovieSetArt         , "MovieSet Art download"     ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadFromFanartTv        , "Fanart.Tv download"        ) )  'Download images from Fanart.Tv site
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadExtraFanart         , "Extra Fanart download"     ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadTrailer             , "Trailer download"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignMovieToCache          , "Assigning movie to cache"  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf HandleOfflineFile           , "Handle offline file"       ) )
        Actions.Items.Add( New ScrapeAction(AddressOf UpdateCaches                , "Updating caches"           ) )
    End Sub
    
    Sub AppendMVScrapeSuccessActions    'Add only these actions when scraping Music Videos
        Actions.Items.Add( New ScrapeAction(AddressOf AssignScrapedMovie          , "Assign scraped movie"      ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignHdTags                , "Assign HD Tags"                  ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf DoRename                    , "Rename"                          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignMVToCache             , "Assigning music Video to cache"  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                     , "Save Nfo"                        ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadPoster              , "Poster download"                 ) )
        'Actions.Items.Add( New ScrapeAction(AddressOf DownloadFanart              , "Fanart download"                 ) )
        
    End Sub
 
    Sub AppendScrapeFailedActions
        Actions.Items.Add( New ScrapeAction(AddressOf TidyUpAnyUnscrapedFields  , "Tidy up unscraped fields"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                   , "Save Nfo"                          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignUnknownMovieToCache , "Assign unknown new movie to cache" ) )
        Actions.Items.Add( New ScrapeAction(AddressOf UpdateCaches              , "Updating caches"                   ) )
    End Sub

    Sub AppendMVScrapeFailedActions
        Actions.Items.Add( New ScrapeAction(AddressOf TidyMVUnscraped           , "Tidy up unscraped fields"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                   , "Save Nfo"                          ) )
    End Sub

    Sub AppendScraperIMDBSpecific
        Actions.Items.Add( New ScrapeAction(AddressOf ImdbScraper_GetBody , "Scrape IMDB Main body"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf CheckImdbBodyScrape , "Checking IMDB Main body scrape" ) )
    End Sub

    Sub AppendScraperTMDBSpecific
        Actions.Items.Add( New ScrapeAction(AddressOf TmdbScraper_GetBody , "Scrape TMDB Main Body"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf CheckTmdbBodyScrape , "Checking TMDB Main body scrape" ) ) 
    End Sub

    Sub AppendScraperMusicVidSpecific
        Actions.Items.Add( New ScrapeAction(AddressOf musicVid_GetBody         , "Scrape MV Main Body"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf CheckMusicVidBodyScrape  , "Checking MV Main body scrape" ) )
    End Sub

    Sub Scrape(imdb As String)
        _possibleImdb = imdb
        Scrape
    End Sub

    Sub Scrape
        If Not Scraped then
            Scraped  = True
            'General
            Actions.Items.Add( New ScrapeAction(AddressOf IniTmdb             , "Initialising TMDb"              ) )
            Actions.Items.Add( New ScrapeAction(AddressOf getspecialMovie     , "Check if special version"       ) )
            If (Preferences.movies_useXBMC_Scraper Or MovieSearchEngine = "tmdb") AndAlso Not Preferences.MusicVidScrape
                AppendScraperTMDBSpecific 
            ElseIf Not Preferences.movies_useXBMC_Scraper AndAlso Not MovieSearchEngine = "tmdb" AndAlso Not Preferences.MusicVidScrape
                AppendScraperIMDBSpecific 
            Else If Preferences.MusicVidScrape
                AppendScraperMusicVidSpecific 
            End If
            RunScrapeActions
        End if
    End Sub

    Sub RunScrapeActions
        While Actions.Items.Count>0

            Dim action = Actions.Items(0)

            Try
                action.Run

                If LogScrapeTimes And action.Time.ElapsedTimeMs > ScrapeTimingsLogThreshold then
                    TimingsLog &= vbCrLf & "[" & action.ActionName & "] took " & action.Time.ElapsedTimeMs & "ms to run"
                End if

                Actions.Items.RemoveAt(0)

                If Cancelled then Exit Sub

            Catch ex As Exception
                If ex.Message.ToString.ToLower.Contains("offline") Then
                    ReportProgress(,"!!! Error - Running action [" & action.ActionName & "] threw [" & ex.Message.ToString & "]" & vbCrLf)
                    Actions.Items.RemoveAt(0)
                Else
                    ReportProgress(MSG_ERROR,"!!! Error - Running action [" & action.ActionName & "] threw [" & ex.Message.ToString & "]" & vbCrLf)                
                Actions.Items.Clear
                End If
            End Try

        End While
    End Sub

    Public Function Cancelled As Boolean

        Application.DoEvents

        If Not IsNothing(_parent.Bw) AndAlso _parent.Bw.CancellationPending Then
            Return True
        End If

        Return False
    End Function

    Sub ReportProgress( Optional progressText As String=Nothing, Optional log As String=Nothing, Optional command As Progress.Commands=Progress.Commands.Append )
        RaiseEvent ProgressLogChanged ( New Progress(FormatProgressText(progressText),log,command) )
    End Sub

    Function FormatProgressText(Optional progressText As String=Nothing) As String
        If IsNothing(progressText) then Return Nothing

        If progressText.Length = 0 then Return Nothing

        Return MSG_PREFIX + progressText
    End Function

    Sub getspecialMovie
        SeparateMovie = Utilities.checktitle(TitleFull, Preferences.MovSepLst)
        If SeparateMovie = "3D" Then
            ThreeDKeep = Utilities.checktitle(TitleFull, Preferences.ThreeDKeyWords)
        End If
    End Sub

    Sub ImdbScraper_GetBody
        _imdbBody = ImdbScrapeBody(Utilities.CleanReleaseFormat(SearchName, Preferences.releaseformat), PossibleYear, PossibleImdb)
    End Sub

    'Sub ImdbScraper_GetBodyByImdbOnly
    '    _imdbBody = ImdbScrapeBody(, , PossibleImdb)
    'End Sub
    
    Function ImdbScrapeBody(Optional Title As String=Nothing, Optional PossibleYear As String=Nothing, Optional PossibleImdb  As String=Nothing) As String

        If Not IsNothing(Title) then
            ReportProgress(, String.Format("!!! {0}!!! Scraping Title: {1}{0}", vbCrLf, Title))
        End If

        If PossibleImdb <> "" then
            ReportProgress( ,"Using IMDB : " & PossibleImdb & vbCrLf )
        End If

        ReportProgress( String.Format(" - Using '{0}{1}'", title, If(String.IsNullOrEmpty(PossibleYear), "", " " & PossibleYear)) & " " )

        ReportProgress( "- Main body " )
   
        Return _imdbScraper.getimdbbody(Title, PossibleYear, PossibleImdb, Preferences.googlecount)
    End Function

    Sub IniTmdb
        IniTmdb(PossibleImdb)
    End Sub

    Sub IniTmdb( imdb As String )
        tmdb = New TMDb(imdb)
    End Sub

    Sub CheckImdbBodyScrape
        'Failed...
        If ImdbBody = "MIC" Then   
            ReportProgress(MSG_ERROR,"!!! Unable to scrape body with refs """ & Title & """, """ & PossibleYear & """, """ & PossibleImdb & """, """ & Preferences.imdbmirror & """" & vbCrLf & "IMDB may not be available or Movie Title is invalid" & vbCrLf )
            AppendScrapeFailedActions
        Else
            ReportProgress(MSG_OK,"!!! Movie Body Scraped OK" & vbCrLf)
            AppendScrapeSuccessActions
        End If
    End Sub

    Sub musicVid_GetBody()
        Dim s As New WikipediaMusivVideoScraper 
        Dim serchtitle As String = Utilities.CleanReleaseFormat(SearchName, Preferences.releaseformat)
        If Not IsNothing(serchtitle) then
            ReportProgress(, String.Format("!!! {0}!!! Scraping Title: {1}{0}", vbCrLf, serchtitle))
            ReportProgress( String.Format(" - Using '{0}'", serchtitle ))
            ReportProgress( "- Main body " )
        End If
        _imdbBody = _imdbScraper.getMVbodyADB(mediapathandfilename, MVSearchName)
        'AppendMVScrapeSuccessActions
    End Sub
    
    Sub CheckMusicVidBodyScrape()
        If ImdbBody.ToLower = "error" Then
            ReportProgress(MSG_ERROR,"!!! Unable to scrape body with refs :" & MVSearchName & vbCrLf )
            AppendMVScrapeFailedActions
        Else
            ReportProgress(MSG_OK,"!!! Music Video Body Scraped OK" & vbCrLf)
            AppendMVScrapeSuccessActions
        End If
    End Sub

    Sub TmdbScraper_GetBody()
        _imdbBody = TmdbScrapeBody(Utilities.CleanReleaseFormat(SearchName, Preferences.releaseformat), PossibleYear, PossibleImdb)
    End Sub

    Sub CheckTmdbBodyScrape()
        If ImdbBody.ToLower = "error" Then   'Failed...
            ReportProgress(MSG_ERROR,"!!! Unable to scrape body with refs """ & Title & """, """ & PossibleYear & """" & vbCrLf & "TMDB may not be available or Movie Title is invalid" & vbCrLf )
            AppendScrapeFailedActions
        Else
            ReportProgress(MSG_OK,"!!! Movie Body Scraped OK" & vbCrLf)
            AppendScrapeSuccessActions
        End If
    End Sub

    Function TmdbScrapeBody(Optional Title As String=Nothing, Optional PossibleYear As String=Nothing, Optional PossibleImdb  As String=Nothing) As String
        If Not IsNothing(Title) then
            ReportProgress(, String.Format("!!! {0}!!! Scraping Title: {1}{0}", vbCrLf, Title))
        End If

        If PossibleImdb <> "" then
            ReportProgress( ,"Using TMDB : " & PossibleImdb & vbCrLf )
        End If

        ReportProgress( String.Format(" - Using '{0}{1}'", title, If(String.IsNullOrEmpty(PossibleYear), "", " " & PossibleYear)) & " " )

        ReportProgress( "- Main body " )
        
        Return _imdbScraper.gettmdbbody(Title, PossibleYear, PossibleImdb, Preferences.googlecount)
    End Function


    Sub TidyUpAnyUnscrapedFields
        Dim success As Boolean = True
        If _scrapedMovie.fullmoviebody.title = Nothing or _scrapedMovie.fullmoviebody.title = "" Then
            _scrapedMovie.fullmoviebody.title = Title
            _scrapedMovie.fullmoviebody.plot  = "This movie could not be identified, or IMDB is un-available. To add the movie manually, either go to the movie edit page and select ""Change Movie"", then select the correct movie, or change movie scraper to XBMC-TMDB and Rescrape Movie."
            _scrapedMovie.fullmoviebody.genre = "Problem"
            success = False
        End If

        If _scrapedMovie.fullmoviebody.year =       Nothing Then _scrapedMovie.fullmoviebody.year = "1901"
        If _scrapedMovie.fullmoviebody.rating =     Nothing Then _scrapedMovie.fullmoviebody.rating = "0"
        If _scrapedMovie.fullmoviebody.top250 =     Nothing Then _scrapedMovie.fullmoviebody.top250 = "0"
        If _scrapedMovie.fullmoviebody.playcount =  Nothing Then _scrapedMovie.fullmoviebody.playcount = "0"
        If _scrapedMovie.fullmoviebody.lastplayed = Nothing Then _scrapedMovie.fullmoviebody.lastplayed = ""

        If String.IsNullOrEmpty(_scrapedMovie.fileinfo.createdate) Then _scrapedMovie.fileinfo.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString

        If success AndAlso Preferences.movies_useXBMC_Scraper Then
            tmdb.Imdb                               = _scrapedMovie.fullmoviebody.imdbid 
            tmdb.TmdbId                             = _scrapedMovie.fullmoviebody.tmdbid 
            '_scrapedMovie.fullmoviebody.mpaa        = tmdb.Certification
            _scrapedMovie.fullmoviebody.premiered   = tmdb.releasedate
            If Not Preferences.XbmcTmdbVotesFromImdb Then _scrapedMovie.fullmoviebody.votes = tmdb.Movie.vote_count
        End If
        _scrapedMovie.fileinfo.movsetfanartpath = Preferences.GetMovSetFanartPath(NfoPathAndFilename, _scrapedMovie.fullmoviebody.movieset.MovieSetName)
        _scrapedMovie.fileinfo.movsetposterpath = Preferences.GetMovSetPosterPath(NfoPathAndFilename, _scrapedMovie.fullmoviebody.movieset.MovieSetName)
        If Not Preferences.ExcludeMpaaRated Then
            Dim mpaa As String = _scrapedMovie.fullmoviebody.mpaa
            If mpaa <> "" AndAlso Not mpaa.ToLower.Contains("rated") Then
                _scrapedMovie.fullmoviebody.mpaa = "Rated " & _scrapedMovie.fullmoviebody.mpaa
            End If
        End If
        Try     'Set TMDB Id in _scrapedMovie if not already set.
            If _scrapedMovie.fullmoviebody.tmdbid = "" AndAlso tmdb.TmdbId <> "" Then _scrapedMovie.fullmoviebody.tmdbid = tmdb.TmdbId
        Catch
        End Try
    End Sub

    Sub TidyMVUnscraped()
        If ImdbBody.ToLower = "error" Then

        End If
    End Sub

    Sub LoadNFO(Optional bUpdateCaches As Boolean=True)
        If Preferences.MusicVidScrape Then
            _scrapedMovie = WorkingWithNfoFiles.MVloadNfo(ActualNfoPathAndFilename)  'NfoPathPrefName
        Else
            _scrapedMovie = WorkingWithNfoFiles.mov_NfoLoadFull(ActualNfoPathAndFilename)  'NfoPathPrefName
        End If
        
        _nfoPathAndFilename = ActualNfoPathAndFilename
        Scraped=True
        Try
            If Not Preferences.MusicVidScrape Then
                AssignMovieToCache
                If bUpdateCaches Then UpdateCaches
            End If
        Catch
        End Try
    End Sub

    Sub SaveNFO
        Try
            SaveNFO(NfoPathPrefName, _scrapedMovie, mediapathandfilename)
        Catch ex As Exception
            ReportProgress(MSG_ERROR, "!!! [SaveNFO - Writing to XBMC Controller Queue] threw [" & ex.Message & "]" & vbCrLf)
        End Try
    End Sub


    Shared Sub SaveNFO(Nfo As String, fmd As FullMovieDetails,Optional media As String=Nothing)

        'Dim MovieUpdated As Boolean = File.Exists(Nfo)
        If Preferences.MusicVidScrape Then
            WorkingWithNfoFiles.MVsaveNfo(nfo, fmd)
        Else
            WorkingWithNfoFiles.mov_NfoSave(Nfo, fmd, True)    
        End If
        If Preferences.MusicVidScrape Then Exit Sub

        If Preferences.XbmcLinkReady Then

            If IsNothing(media) Then
                media = Utilities.GetFileName(Nfo,True)
            End If

            Dim evt As New BaseEvent

            'evt.E = IIf(MovieUpdated, XbmcController.E.MC_Movie_Updated, XbmcController.E.MC_Movie_New)

            evt.E    = XbmcController.E.MC_Movie_Updated
            evt.Args = New VideoPathEventArgs(media, PriorityQueue.Priorities.medium)

            Form1.XbmcControllerQ.Write(evt)

        End If
    End Sub



    Sub AssignUnknownMovieToCache
        AssignMovieToCache
        _movieCache.runtime = "0"
    End Sub

    Sub AssignMovieToCache
        'If _scrapedMovie.fullmoviebody.title = "Error" AndAlso _scrapedMovie.fullmoviebody.originaltitle = "" Then Exit Sub 
        'If Preferences.MusicVidScrape Then
        '    ucMusicVideo.MVCacheAdd(_scrapedMovie)
        '    Exit Sub
        'End If
        _movieCache.fullpathandfilename = If(movRebuildCaches, ActualNfoPathAndFilename, NfoPathPrefName) 'ActualNfoPathAndFilename 
        _actualNfoPathAndFilename    = NfoPathPrefName 
        _movieCache.MovieSet.absorb(scrapedMovie.fullmoviebody.movieset)
        _movieCache.source              = _scrapedMovie.fullmoviebody.source
        _movieCache.director            = _scrapedMovie.fullmoviebody.director 
        _movieCache.filename            = Path.GetFileName(nfopathandfilename)

        
        If movRebuildCaches Then 
            UpdateActorCacheFromEmpty
            UpdateDirectorCacheFromEmpty
            UpdateMovieSetCacheFromEmpty 
        End If
        

        If Not Preferences.usefoldernames Then
            If _movieCache.filename <> Nothing Then _movieCache.filename = _movieCache.filename.Replace(".nfo", "")
        End If    

        _movieCache.foldername          = Utilities.GetLastFolder(nfopathandfilename)

        _movieCache.FolderSize = Utilities.GetFolderSize(NfoPath)


        _movieCache.title               = _scrapedMovie.fullmoviebody.title
        _movieCache.originaltitle       = _scrapedMovie.fullmoviebody.originaltitle
        _movieCache.sortorder           = _scrapedMovie.fullmoviebody.sortorder
        _movieCache.runtime             = _scrapedMovie.fullmoviebody.runtime
        _movieCache.Votes               = _scrapedMovie.fullmoviebody.votes.Replace(".","").ToInt
        _movieCache.outline             = _scrapedMovie.fullmoviebody.outline
        _movieCache.plot                = _scrapedMovie.fullmoviebody.plot
        _movieCache.tagline             = _scrapedMovie.fullmoviebody.tagline 
        _movieCache.year                = _scrapedMovie.fullmoviebody.year.ToInt
        _movieCache.Resolution          = _scrapedMovie.filedetails.filedetails_video.VideoResolution
        _movieCache.VideoCodec          = _scrapedMovie.filedetails.filedetails_video.Codec.Value 
        _movieCache.AssignAudio(_scrapedMovie.filedetails.filedetails_audio)
        _movieCache.Premiered           = _scrapedMovie.fullmoviebody.premiered
        _movieCache.movietag            = _scrapedMovie.fullmoviebody.tag
        _movieCache.stars               = _scrapedMovie.fullmoviebody.stars 
        Dim filecreation As New IO.FileInfo(nfopathandfilename)

        Try
            _movieCache.filedate = Format(filecreation.LastWriteTime, Preferences.datePattern).ToString
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

        If String.IsNullOrEmpty(_scrapedMovie.fileinfo.createdate) Then
            _movieCache.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
        Else
            _movieCache.createdate = _scrapedMovie.fileinfo.createdate
        End If
        _movieCache.id          = _scrapedMovie.fullmoviebody.imdbid
        _movieCache.tmdbid      = _scrapedMovie.fullmoviebody.tmdbid 
        _movieCache.rating      = _scrapedMovie.fullmoviebody.rating.ToRating
        _movieCache.top250      = _scrapedMovie.fullmoviebody.top250
        _movieCache.genre       = _scrapedMovie.fullmoviebody.genre
        _movieCache.countries   = _scrapedMovie.fullmoviebody.country
        _movieCache.studios     = _scrapedMovie.fullmoviebody.studio 
        _movieCache.playcount   = _scrapedMovie.fullmoviebody.playcount
        _movieCache.lastplayed  = _scrapedMovie.fullmoviebody.lastplayed 
        _movieCache.Certificate = _scrapedMovie.fullmoviebody.mpaa
        _movieCache.movietag    = _scrapedMovie.fullmoviebody.tag
        _movieCache.Container   = _scrapedMovie.filedetails.filedetails_video.Container.Value
        _movieCache.Actorlist   = _scrapedMovie.listactors 
        If Preferences.incmissingmovies Then
            Dim Fileandpath As String = Utilities.GetFileName(_movieCache.fullpathandfilename, , _movieCache.Container)
            _movieCache.VideoMissing = Not File.Exists(Fileandpath)
        End If
        _movieCache.AssignSubtitleLang(_scrapedMovie.filedetails.filedetails_subtitles)
        AssignMovieToAddMissingData
    End Sub

    Sub AssignMVToCache
        Dim _mvcache As New MVComboList 
        _mvcache.fullpathandfilename = NfoPathPrefName
        _mvcache.filename            = Path.GetFileName(nfopathandfilename)
        _mvcache.foldername          = Utilities.GetLastFolder(nfopathandfilename)
        _mvcache.title               = _scrapedMovie.fullmoviebody.title
        _mvcache.runtime             = _scrapedMovie.fullmoviebody.runtime
        _mvcache.plot                = _scrapedMovie.fullmoviebody.plot
        _mvcache.year                = _scrapedMovie.fullmoviebody.year.ToInt
        _mvcache.Resolution          = _scrapedMovie.filedetails.filedetails_video.VideoResolution
        _mvcache.thumb               = _scrapedMovie.fullmoviebody.thumb
        _mvcache.track               = _scrapedMovie.fullmoviebody.track 
        _mvcache.AssignAudio(_scrapedMovie.filedetails.filedetails_audio)
        Dim filecreation As New IO.FileInfo(nfopathandfilename)
        Try
            _movieCache.filedate = Format(filecreation.LastWriteTime, Preferences.datePattern).ToString
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

        If String.IsNullOrEmpty(_scrapedMovie.fileinfo.createdate) Then
            _movieCache.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
        Else
            _movieCache.createdate = _scrapedMovie.fileinfo.createdate
        End If
        _mvcache.genre       = _scrapedMovie.fullmoviebody.genre
        _mvcache.playcount   = _scrapedMovie.fullmoviebody.playcount
        _mvcache.lastplayed  = _scrapedMovie.fullmoviebody.lastplayed 
        ucMusicVideo.MVCache.Add(_mvcache)
    End Sub


    Sub AssignScrapedMovie
        AssignScrapedMovie(_scrapedMovie)
    End Sub
    
    Sub AssignScrapedMovie(_scrapedMovie As FullMovieDetails)
        Dim thumbstring As New XmlDocument
        Dim xmltype As String = "movie"
        If Preferences.MusicVidScrape Then xmltype = "musicvideo"

        thumbstring.LoadXml(ImdbBody)
        Dim thisresult As XmlElement = Nothing
        For Each thisresult In thumbstring(xmltype)
            Select Case thisresult.Name
                Case "title"
                    Dim sepmov As String = ""
                    If SeparateMovie <> "" Then
                        sepmov = " " & SeparateMovie & If(ThreeDKeep <> "", " " & ThreeDKeep, "")
                    End If
                    Dim temptitle As String = thisresult.InnerText.ToString.SafeTrim & sepmov
                    _scrapedMovie.fullmoviebody.title = If(Preferences.MovTitleCase, Utilities.TitleCase(temptitle), temptitle)
                Case "originaltitle"
                    _scrapedMovie.fullmoviebody.originaltitle = thisresult.InnerText.ToString.SafeTrim
                Case "alternativetitle"
                    If Not Preferences.NoAltTitle Then
                        _scrapedMovie.alternativetitles.Add(thisresult.InnerText)
                    End If
                Case "country"
                    _scrapedMovie.fullmoviebody.country = thisresult.InnerText
                Case "credits"
                    _scrapedMovie.fullmoviebody.credits = thisresult.InnerText
                Case "director"
                    _scrapedMovie.fullmoviebody.director = thisresult.InnerText
                Case "stars"
                    _scrapedMovie.fullmoviebody.stars = thisresult.InnerText.ToString.Replace(", See full cast and crew","")
                Case "genre"
                    Dim strarr() As String
                    strarr = thisresult.InnerText.Split("/")
                    If strarr.Length <= Preferences.maxmoviegenre Then
                        _scrapedMovie.fullmoviebody.genre = thisresult.InnerText
                    Else
                        For g = 0 To Preferences.maxmoviegenre - 1
                            If g = 0 Then
                                _scrapedMovie.fullmoviebody.genre = strarr(g).Trim
                            Else
                                _scrapedMovie.fullmoviebody.genre += " / " & strarr(g).Trim
                            End If
                        Next
                    End If
                Case "mpaa"
                    _scrapedMovie.fullmoviebody.mpaa = thisresult.InnerText
                Case "outline"
                    _scrapedMovie.fullmoviebody.outline = Utilities.RemoveEscapeCharacter(thisresult.InnerText)
                Case "plot"
                    _scrapedMovie.fullmoviebody.plot = Utilities.RemoveEscapeCharacter(thisresult.InnerText)
                Case "premiered"
                    _scrapedMovie.fullmoviebody.premiered = thisresult.InnerText
                Case "rating"
                    _scrapedMovie.fullmoviebody.rating = thisresult.InnerText.ToString.ToRating.ToString
                Case "runtime"
                    _scrapedMovie.fullmoviebody.runtime = thisresult.InnerText
                    If _scrapedMovie.fullmoviebody.runtime.IndexOf(":") <> -1 Then
                        Try
                            _scrapedMovie.fullmoviebody.runtime = _scrapedMovie.fullmoviebody.runtime.Substring(_scrapedMovie.fullmoviebody.runtime.IndexOf(":") + 1, _scrapedMovie.fullmoviebody.runtime.Length - _scrapedMovie.fullmoviebody.runtime.IndexOf(":") - 1)
                        Catch
                        End Try
                    End If
                Case "studio"
                    Dim strarr() As String
                    strarr = thisresult.InnerText.Split(",")
                    If strarr.Length <= Preferences.MovieScraper_MaxStudios Then
                        _scrapedMovie.fullmoviebody.studio = thisresult.InnerText
                    Else
                        For g = 0 To Preferences.MovieScraper_MaxStudios - 1
                            If g = 0 Then
                                _scrapedMovie.fullmoviebody.studio = strarr(g).Trim
                            Else
                                _scrapedMovie.fullmoviebody.studio += ", " & strarr(g).Trim
                            End If
                        Next
                    End If
                Case "tagline"
                    _scrapedMovie.fullmoviebody.tagline = thisresult.InnerText
                Case "top250"
                    _scrapedMovie.fullmoviebody.top250 = thisresult.InnerText
                Case "votes"
                    _scrapedMovie.fullmoviebody.votes = thisresult.InnerText
                Case "year"
                    _scrapedMovie.fullmoviebody.year = thisresult.InnerText
                Case "id"
                    Dim thisid As String = thisresult.InnerText
                    If thisid <> "" Then
                        If thisid.Contains("tt") Then
                            _scrapedMovie.fullmoviebody.imdbid = thisid
                            If String.IsNullOrEmpty(_possibleImdb) Then _possibleImdb = thisid 
                        Else
                            _scrapedMovie.fullmoviebody.tmdbid = thisid
                        End If
                    End If
                Case "cert"
                    _certificates.Add(thisresult.InnerText)
                Case "actor"
                    If Preferences.movies_useXBMC_Scraper Then
                        Dim newactor As New str_MovieActors(SetDefaults)
                        Dim detail As XmlNode = Nothing
                        For Each detail In thisresult.ChildNodes
                            Select Case detail.Name
                                Case "id"
                                    newactor.actorid = detail.InnerText
                                Case "name"
                                    newactor.actorname = detail.InnerText
                                Case "role"
                                    newactor.actorrole = detail.InnerText
                                Case "thumb"
                                    newactor.actorthumb = detail.InnerText
                                    If newactor.actorthumb.Contains("original/") Then
                                        newactor.actorthumb = "http://image.tmdb.org/t/p/" & detail.InnerText
                                    End If
                            End Select
                        Next
                        _scrapedMovie.listactors.Add(newactor)
                    End If
                Case "album"
                    _scrapedMovie.fullmoviebody.album = thisresult.InnerText
                Case "artist"
                    _scrapedMovie.fullmoviebody.artist = thisresult.InnerText
                Case "track"
                    _scrapedMovie.fullmoviebody.track = thisresult.InnerText
                Case "thumb"
                    _scrapedMovie.fullmoviebody.thumb = thisresult.InnerText 
            End Select
        Next

        If Preferences.MusicVidScrape Then Exit Sub     'If Music Video Scraping, finished at this point so exit

        If Preferences.sorttitleignorearticle Then                              'add ignored articles to end of
            Dim titletext As String = _scrapedMovie.fullmoviebody.title         'sort title. Over-rides independent The or A settings.
            _scrapedMovie.fullmoviebody.sortorder = Preferences.RemoveIgnoredArticles(titletext)
        Else
            _scrapedMovie.fullmoviebody.sortorder = _scrapedMovie.fullmoviebody.title               'Sort order defaults to title
        End If
        If Preferences.MovTitleCase Then
            _scrapedMovie.fullmoviebody.sortorder = Utilities.TitleCase(_scrapedMovie.fullmoviebody.sortorder)
        End If
        If _scrapedMovie.fullmoviebody.plot = "" Then _scrapedMovie.fullmoviebody.plot = _scrapedMovie.fullmoviebody.outline ' If plot is empty, use outline
        If _scrapedMovie.fullmoviebody.playcount = Nothing Then _scrapedMovie.fullmoviebody.playcount = "0"
        If _scrapedMovie.fullmoviebody.lastplayed = Nothing Then _scrapedMovie.fullmoviebody.lastplayed = ""
        If _scrapedMovie.fullmoviebody.top250 = Nothing Then _scrapedMovie.fullmoviebody.top250 = "0"
        'check search name for movie source
        Dim searchtitle As String = Title ' SearchName 
        If searchtitle <> "" Then
            For i = 0 to Preferences.releaseformat.Length -1
                If searchtitle.ToLower.Contains(Preferences.releaseformat(i).ToLower) Then
                    _scrapedMovie.fullmoviebody.source = Preferences.releaseformat(i)
                    Exit For
                End If
            Next
        End If
        ' Assign certificate
        Dim done As Boolean = False
        For g = 0 To UBound(Preferences.certificatepriority)
            For Each cert In Certificates
                If cert.IndexOf(Preferences.certificatepriority(g)) <> -1 Then
                    _scrapedMovie.fullmoviebody.mpaa = cert.Substring(cert.IndexOf("|") + 1, cert.Length - cert.IndexOf("|") - 1)
                    done = True
                    Exit For
                End If
            Next
            If done = True Then Exit For
        Next
        If Not Preferences.ExcludeMpaaRated Then
            Dim mpaa As String = _scrapedMovie.fullmoviebody.mpaa
            If mpaa <> "" AndAlso Not mpaa.ToLower.Contains("rated") Then
                _scrapedMovie.fullmoviebody.mpaa = "Rated " & _scrapedMovie.fullmoviebody.mpaa
            End If
        End If
        If Rescrape Then
            _scrapedMovie.fullmoviebody.source = _previousCache.source
            _scrapedMovie.fullmoviebody.playcount = _previousCache.playcount
            _scrapedMovie.fullmoviebody.lastplayed = _previousCache.lastplayed 
            _scrapedMovie.fileinfo.createdate = _previousCache.createdate
            _scrapedMovie.fullmoviebody.movieset = _previousCache.MovieSet
        Else
            tmdb.Imdb = _scrapedMovie.fullmoviebody.imdbid
            tmdb.TmdbId = _scrapedMovie.fullmoviebody.tmdbid 
            If Certificates.Count = 0 Then
                _scrapedMovie.fullmoviebody.mpaa = If(Preferences.ExcludeMpaaRated, "", "Rated ") & tmdb.Certification 
                If _scrapedMovie.fullmoviebody.mpaa = "Rated " Then _scrapedMovie.fullmoviebody.mpaa = ""
            End If
            _scrapedMovie.fullmoviebody.movieset.MovieSetName = "-None-"
            If Preferences.GetMovieSetFromTMDb AndAlso Not IsNothing(tmdb.Movie.belongs_to_collection) Then
                _scrapedMovie.fullmoviebody.movieset.MovieSetName = tmdb.Movie.belongs_to_collection.name
                _scrapedMovie.fullmoviebody.movieset.MovieSetId = tmdb.Movie.belongs_to_collection.id 
            End If
        End If
    End Sub
    
    Sub DoRename
        If Preferences.MovieRenameEnable AndAlso Not Preferences.basicsavemode AndAlso Not nfopathandfilename.ToLower.Contains("video_ts") AndAlso Not nfopathandfilename.ToLower.Contains("bdmv") Then 'AndAlso Not Preferences.usefoldernames AndAlso Not nfopathandfilename.ToLower.Contains("video_ts")  ''Preferences.GetRootFolderCheck(NfoPathAndFilename) OrElse 
            ReportProgress(,fileRename(me))
        End If
        If Preferences.MovFolderRename Then
            ReportProgress(,folderRename(me))
        End If
    End Sub

    Sub GetActors
        _scrapedMovie.listactors.Clear
        If (Preferences.XbmcTmdbActorDL AndAlso Preferences.movies_useXBMC_Scraper) OrElse Preferences.TmdbActorsImdbScrape Then
            _scrapedMovie.listactors = GetTmdbactors
        Else
            _scrapedMovie.listactors = GetImdbActors
        End If
    End Sub

    Function GetImdbActors

        ReportProgress("IMDB Actors")

        Dim actors As List(Of str_MovieActors) = _imdbScraper.GetImdbActorsList(Preferences.imdbmirror, _scrapedMovie.fullmoviebody.imdbid)
        Dim actors2 As New List(Of str_MovieActors)
        For Each actor In actors
            Try
                actor.SaveActor(ActorPath)
                actors2.Add(actor)
            Catch ex As Exception
                ReportProgress(MSG_ERROR,"!!! Error with " & nfopathandfilename & vbCrLf & "!!! An error was encountered while trying to add a scraped Actor" & vbCrLf & ex.Message & vbCrLf & vbCrLf)
            End Try
        Next

        ReportProgress(MSG_OK,"Actors scraped OK" & vbCrLf)
        If Not Preferences.actorseasy AndAlso Not Preferences.actorsave Then ReportProgress(MSG_OK,"Actor images not set to download" & vbCrLf)

        Return actors2
    End Function

    Function GetTmdbActors
        ReportProgress("TMDB Actors")
        'Get actors from tmdb
        Dim maxactors As Integer = Preferences.maxactors
        Dim actors As List(Of str_MovieActors) = tmdb.cast 
        Dim actors2 As New List(Of str_MovieActors)
        If maxactors <> 0 Then
            Dim count As Integer = 0
            For Each actor In actors
                Try
                    actor.SaveActor(ActorPath)
                    actors2.Add(actor)
                    count += 1
                Catch ex As Exception
                    ReportProgress(MSG_ERROR,"!!! Error with " & nfopathandfilename & vbCrLf & "!!! An error was encountered while trying to add a scraped Actor" & vbCrLf & ex.Message & vbCrLf & vbCrLf)
                End Try
                If count = maxactors Then Exit For
            Next
        End If
        ReportProgress(MSG_OK,"Actors scraped OK" & vbCrLf)
        If Not Preferences.actorseasy AndAlso Not Preferences.actorsave Then ReportProgress(MSG_OK,"Actor images not set to download" & vbCrLf)
        Return actors2
    End Function


    Sub AssignTrailerUrl
        If Not Preferences.gettrailer Then
            Exit Sub
        End If

        If TrailerExists Then
            ReportProgress("Trailer already exists ","Trailer already exists - To download again, delete the existing one first i.e. this file : [" & ActualTrailerPath & "]" & vbCrLf)
            Exit Sub
        End If

        _scrapedMovie.fullmoviebody.trailer = GetTrailerUrl(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.imdbid)
    End Sub

    Function GetTrailerUrl( title As String, imdb As String ) As String
        ReportProgress("Trailer URL")

        TrailerUrl = ""
        _youTubeTrailer = Nothing
        
        If Preferences.movies_useXBMC_Scraper Then 
            If Preferences.XbmcTmdbScraperTrailerQ.ToLower <> "no" Then
                If TrailerUrl = "" Then TrailerUrl = TrailerHDTrailer(title)
            End If
        Else
            'Try HDTrailers.Net
            If TrailerUrl = "" Then TrailerUrl = TrailerHDTrailer(title)
        End If
        
        'Try TMDB Youtube Trailer
        If TrailerUrl = "" Then TrailerUrl = TrailerTMDBYouTube()
        
        'Try YouTube for HD and SD...
        If TrailerUrl = "" Then TrailerUrl = TrailerYouTube()
        
        'Try IMDB last Resort.
        If TrailerUrl = "" And Not String.IsNullOrEmpty(_scrapedMovie.fullmoviebody.imdbid) Then
            TrailerUrl = _imdbScraper.gettrailerurl(imdb, Preferences.imdbmirror)
        End If

        'Report status
        If TrailerUrl = "" Then
            ReportProgress("-None found ","No trailer URL found" & vbCrLf)
        ElseIf TrailerUrl.Contains("plugin://plugin") Then
            ReportProgress(MSG_OK,"Youtube URL OK" & vbCrLf)
        Else
            If Utilities.UrlIsValid(TrailerUrl) Then
                ReportProgress(MSG_OK,"Trailer URL Scraped OK" & vbCrLf)
            Else
                ReportProgress("-Invalid! ","!!! The Scraped Trailer URL is either invalid or not available [" & TrailerUrl & "]" & vbCrLf)
            End If
        End If

        Return TrailerUrl
    End Function

    Function TrailerHDTrailer(ByVal title As String) As String
        Dim TrailerUrl2 As String = ""
        If Preferences.moviePreferredTrailerResolution.ToUpper()<>"SD" And Not GetTrailerUrlAlreadyRun Then
            Try
                GetTrailerUrlAlreadyRun = True
                Dim TraRes As String = If(Preferences.movies_useXBMC_Scraper, Preferences.XbmcTmdbScraperTrailerQ.Replace("p",""), Preferences.moviePreferredTrailerResolution)
                TrailerUrl2 = MC_Scraper_Get_HD_Trailer_URL(TraRes, title)
            Catch ex As Exception
                Dim paramInfo As String = ""
            
                Try
                    paramInfo = "Title: [" & title & "]"
                Catch ex2 As Exception
                    ExceptionHandler.LogError(ex2)
                End Try

                ExceptionHandler.LogError(ex,paramInfo)                
            End Try
        End If
        Return TrailerUrl2 
    End Function

    Function TrailerTMDBYouTube() As String
        Dim loopcount = 0
        Dim TrailerUrl2 As String = ""
        Try
            If TrailerUrl2 = "" AndAlso Not IsNothing(tmdb.Trailers) AndAlso tmdb.Trailers.youtube.Count > 0 then
                Dim tryAgain = True
                While tryAgain
                    TrailerUrl2 = tmdb.GetTrailerUrl(_triedUrls, Preferences.moviePreferredTrailerResolution)
                    If TrailerUrl2 <> "" then
                        Try
                            Dim yts as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+TrailerUrl2)

                            If yts.AvailableVideoFormat.Length>0 Then

                                _youTubeTrailer = yts.selectTrailer(Preferences.moviePreferredTrailerResolution)

                                If Not IsNothing(_youTubeTrailer) Then
                                    TrailerUrl2 = "plugin://plugin.video.youtube/?action=play_video&videoid=" & TrailerUrl2   '_youTubeTrailer.VideoUrl
                                    tryAgain = False
                                End If
                            Else
                                TrailerUrl2 = ""
                            End If
                        Catch       'Timed out...
                        End Try
                    Else
                        tryAgain = False
                    End If
                    If loopcount = 15 Then tryAgain = False
                    loopcount += 1
                End While
            End If
        Catch
        End Try
        Return TrailerUrl2
    End Function

    Function TrailerYouTube() As String
        Dim TrailerUrl2 As String = ""
        Dim Ids As List(Of String) = GetYouTubeIds()
        For Each Id In Ids
            Dim yts as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+Id)
            If yts.AvailableVideoFormat.Length>0 Then
                _youTubeTrailer = yts.selectTrailer(Preferences.moviePreferredTrailerResolution)
                If Not IsNothing(_youTubeTrailer) Then
                    TrailerUrl2 = "plugin://plugin.video.youtube/?action=play_video&videoid=" & Id   '_youTubeTrailer.VideoUrl '
                    Exit For
                End If
            End If
        Next
        Return TrailerUrl2 
    End Function

    Sub GetFrodoPosterThumbs
        _scrapedMovie.frodoPosterThumbs.Clear

        If Preferences.XtraFrodoUrls AndAlso Preferences.FrodoEnabled Then
            _scrapedMovie.frodoPosterThumbs.AddRange(tmdb.FrodoPosterThumbs)
            ReportProgress("Extra Frodo Poster thumbs: " & tmdb.FrodoPosterThumbs.count & " ", "Extra Frodo Poster thumbs: " & tmdb.FrodoPosterThumbs.count & vbCrLf)
        'Else
            'ReportProgress(,"Frodo extra URL scraping not selected" & vbCrLf)
        End If
    End Sub


    Sub GetFrodoFanartThumbs
        _scrapedMovie.frodoFanartThumbs.Thumbs.Clear

        If Preferences.XtraFrodoUrls AndAlso Preferences.FrodoEnabled Then
            _scrapedMovie.frodoFanartThumbs.Thumbs.AddRange(tmdb.FrodoFanartThumbs.Thumbs)
            ReportProgress("Extra Frodo Fanart thumbs: " & tmdb.FrodoFanartThumbs.Thumbs.count & " ", "Extra Frodo Fanart thumbs: " & tmdb.FrodoFanartThumbs.Thumbs.count & vbCrLf)
        End If
    End Sub

    

    Sub AssignPosterUrls
        If Preferences.EdenEnabled Then
            If Preferences.nfoposterscraper = 0 Then
                ReportProgress(,"Extra poster URLs scraping not selected" & vbCrLf)
                Exit Sub
            End If

            GetPosterUrls
        Else
            ReportProgress(,"Pre-Frodo poster URLs scraping not enabled" & vbCrLf)
        End If
    End Sub

    Sub GetPosterUrls
        ReportProgress("Extra Poster URLs")
        GetPosterUrlsPart2

        If _scrapedMovie.listthumbs.Count > 0 then
            ReportProgress(MSG_OK,"Extra Poster URLs Scraped OK" & vbCrLf)
        Else
            ReportProgress("-None found ","No extra Poster URLs found" + NfoPosterScraperInfo & vbCrLf)
        End If
    End Sub

    Function NfoPosterScraperInfo As String
        If Preferences.nfoposterscraper <> 15 then
            Return " - Try [X] checking more more\all NfoPosterScraper sources or edit config.xml and set nfoposterscraper to 15 (enable all sources)"
        End If
        Return ""
    End Function

    Sub GetPosterUrlsPart2

        If Preferences.nfoposterscraper And 1 then
            Try
                If AddThumbs( (New IMPA.getimpaposters).getimpathumbs(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year) ) then Exit Sub
            Catch
            End Try
        End If

        If Preferences.nfoposterscraper And 4 then
            Try
                If AddThumbs( (New class_mpdb_thumbs.Class1).get_mpdb_thumbs(_scrapedMovie.fullmoviebody.imdbid) ) then Exit Sub
            Catch
            End Try
        End If

        If Preferences.nfoposterscraper And 8 then
            Try
                AddThumbs( (New imdb_thumbs.Class1).getimdbthumbs(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year, _scrapedMovie.fullmoviebody.imdbid) )
            Catch
            End Try
        End If

        If Preferences.nfoposterscraper And 2 then
            Try
                If AddThumbs( tmdb.Thumbs ) then Exit Sub
            Catch
            End Try
        End If
    End Sub


    Function AddThumbs( thumbs As List(Of String) ) As Boolean

        If _scrapedMovie.listthumbs.Count+_scrapedMovie.frodoPosterThumbs.count >= Preferences.maximumthumbs then
            Return True
        End If

        For Each thumb In thumbs
            _scrapedMovie.listthumbs.Add(thumb)

            If _scrapedMovie.listthumbs.Count+_scrapedMovie.frodoPosterThumbs.count >= Preferences.maximumthumbs then
                Return True
            End If
        Next

        Return False
    End Function

    Function AddThumbs( thumbs As String ) As Boolean

        If _scrapedMovie.listthumbs.Count+_scrapedMovie.frodoPosterThumbs.count >= Preferences.maximumthumbs then
            Return True
        End If

        Try
            Dim xmlDoc As New XmlDocument

            xmlDoc.LoadXml("<thumblist>" & thumbs & "</thumblist>")

            For Each thisresult In xmlDoc("thumblist")
                Select Case thisresult.Name
                    Case "thumb"
                        _scrapedMovie.listthumbs.Add(thisresult.InnerText)
                End Select

                If _scrapedMovie.listthumbs.Count+_scrapedMovie.frodoPosterThumbs.count >= Preferences.maximumthumbs then
                    Return True
                End If
            Next
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

        Return False
    End Function

    Sub AssignHdTags
        GetHdTags(_scrapedMovie)
    End Sub

    Function GetHdTags(sm As FullMovieDetails) As Boolean
        If File.Exists(TempOfflinePathAndFileName) Then Return False

        Try
            ReportProgress("HD Tags")
            sm.FileDetails = Preferences.Get_HdTags(mediapathandfilename)
            ReportProgress(MSG_OK,"HD Tags Added OK" & vbCrLf)
            AssignRuntime(sm)
            Return True
        Catch ex As Exception
            ReportProgress(MSG_ERROR,"!!! Error getting HD Tags:- " & ex.Message.ToString & vbCrLf)
            Return False
        End Try
    End Function


    Sub AssignRuntime(sm As FullMovieDetails, Optional runtime_file As Boolean=False)
        If sm.FileDetails.filedetails_video.DurationInSeconds.Value <> Nothing And (runtime_file or (Preferences.movieRuntimeFallbackToFile and sm.fullmoviebody.runtime = "")) Then
            sm.fullmoviebody.runtime = Math.Round(sm.FileDetails.filedetails_video.DurationInSeconds.Value/60).ToString & " min"
        End If
    End Sub


    Sub DownloadTrailer
        DownloadTrailer(TrailerUrl)
    End Sub

    Sub DownloadTrailer(TrailerUrl As String, Optional forceTrailerDl As Boolean=False)
        'Check for and delete zero length trailer - created when Url is invalid
        DeleteZeroLengthFile(ActualTrailerPath)
        Dim TempTrailerUrl As String = TrailerUrl
        If Not Preferences.DownloadTrailerDuringScrape And Not forceTrailerDl Then
            Exit Sub
        End If
        
        If TrailerUrl = "" then
            Exit Sub
        End if

        'Don't re-download if trailer exists and not Forced
        If File.Exists(ActualTrailerPath) And Not forceTrailerDl then
            Exit Sub
        Else 
            Utilities.SafeDeleteFile(ActualTrailerPath)
        End if

        'create download url if youtube trailer path.
        If TrailerUrl.Contains("plugin://plugin.video.youtube") Then
            Dim x As Integer = TrailerUrl.LastIndexOf("=")+1
            Dim id As String = TrailerUrl.Substring(x, (TrailerUrl.Length)-x)

            Dim yts as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+Id)
            If yts.AvailableVideoFormat.Length>0 Then
                _youTubeTrailer = yts.selectTrailer(Preferences.moviePreferredTrailerResolution)
                If Not IsNothing(_youTubeTrailer) Then
                    TrailerUrl = _youTubeTrailer.VideoUrl
                    'Exit For
                End If
            End If
            If Not Utilities.UrlIsValid(TrailerUrl) Then
                Dim Ids As List(Of String) = GetYouTubeIds()
                If Ids.Contains(id) Then Ids.Remove(id)
                For Each utubeid In Ids
                    Dim yturl as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+Id)
                    If yturl.AvailableVideoFormat.Length>0 Then
                        _youTubeTrailer = yturl.selectTrailer(Preferences.moviePreferredTrailerResolution)
                        If Not IsNothing(_youTubeTrailer) Then
                            TrailerUrl = _youTubeTrailer.VideoUrl
                            Exit For
                        End If
                    End If
                Next
            End If
        End If

        If Not Utilities.UrlIsValid(TrailerUrl) Then
            ReportProgress("Invalid Url","Invalid Trailer Url detected")
            Exit Sub
        End if

        ReportProgress("DL Trailer","Downloading trailer...")
        TrailerDownloaded = _WebFileDownloader.DownloadFileWithProgress(TrailerUrl, TrailerPath,_parent.Bw)

        DeleteZeroLengthFile(ActualTrailerPath)

        If Cancelled then Exit Sub

        If TrailerDownloaded Then
            If IO.File.Exists(ActualTrailerPath) Then
                ReportProgress(MSG_OK,"Trailer downloaded OK : [" & ActualTrailerPath & "]" & vbCrLf)
            Else
                ReportProgress("Aborted by User" & vbCrLf, vbCrLf & "!!! Aborted By User - Trailer not saved" & vbCrLf)
            End If
        Else
            ReportProgress(MSG_OK,"Trailer download failed - Searching for an alternative one" & vbCrLf)
            If Actions.Items.Count>0 Then
                Actions.Items.Insert(1, New ScrapeAction(AddressOf AssignTrailerUrl , "Get trailer URL" ) )
                Actions.Items.Insert(2, New ScrapeAction(AddressOf SaveNFO          , "Save Nfo"        ) )
                Actions.Items.Insert(3, New ScrapeAction(AddressOf DownloadTrailer  , "Trailer download") )
            End If
        End If
    End Sub

    Property TrailerDownloaded As Boolean = False

    Private Sub DownloadPoster
        If Not Preferences.MusicVidScrape AndAlso Not Preferences.scrapemovieposters then
            Exit Sub
        End If
        If Preferences.MovieChangeMovie AndAlso Preferences.MovieChangeKeepExistingArt Then Exit Sub
        DoDownloadPoster
    End Sub
 
    Sub DoDownloadPoster(Optional ByVal batch As Boolean = False)
        Dim imageexistspath As String = ""
        If IO.Path.GetFileName(NfoPathPrefName).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(NfoPathPrefName).ToLower = "index.nfo" Then
            _videotsrootpath = Utilities.RootVideoTsFolder(NfoPathPrefName)
        End If
        Dim paths As List(Of String) = Preferences.GetPosterPaths(NfoPathPrefName, If(_videotsrootpath <> "", _videotsrootpath, ""))

        If Not Rescrape Then DeletePoster()

        If Not Preferences.overwritethumbs Then
            Dim lst As New List(Of String)
            For Each filepath In paths
                If File.Exists(filepath) Then
                    imageexistspath = filepath
                    lst.Add(filepath)
                End If
            Next
            If lst.Count > 0 Then
                For Each filepath In lst
                    paths.Remove(filepath)
                Next
            End If
            If paths.Count = 0 Then
                ReportProgress(,"Poster(s) already exists -> Skipping" & vbCrLf)
                Exit Sub
            End If
        End If

        Dim validUrl = False

        If imageexistspath = "" Then
            If Not Preferences.MusicVidScrape Then
                For Each scraper In Preferences.moviethumbpriority
                    Try
                        Select Case scraper
                            Case "Internet Movie Poster Awards"
                                PosterUrl = _scraperFunctions.impathumb(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year)
                            Case "IMDB"
                                PosterUrl = _scraperFunctions.imdbthumb(_scrapedMovie.fullmoviebody.imdbid)
                            Case "Movie Poster DB"
                                PosterUrl = _scraperFunctions.mpdbthumb(_scrapedMovie.fullmoviebody.imdbid)
                            Case "themoviedb.org"
                                'moviethumburl = scraperFunction2.tmdbthumb(_scrapedMovie.fullmoviebody.imdbid)
                                PosterUrl = tmdb.FirstOriginalPosterUrl
                        End Select
                    Catch
                        PosterUrl = "na"
                    End Try

                    validUrl = Utilities.UrlIsValid(PosterUrl)
                    If validUrl Or batch Then
                        Exit For
                    End If
                Next
            Else
                PosterUrl = _scrapedMovie.fullmoviebody.thumb
                validUrl = Utilities.UrlIsValid(PosterUrl)
            End If
        End If
        'If Preferences.MusicVidScrape AndAlso _scrapedMovie.listthumbs.Count > 0 Then 
        '    PosterUrl = _scrapedMovie.listthumbs(0).ToString
        '    validUrl = Utilities.UrlIsValid(PosterUrl)
        'End If

        If validUrl Or imageexistspath <> "" Then

            ReportProgress("Poster")
            If imageexistspath <> "" Then PosterUrl = imageexistspath 
            Try

                SavePosterImageToCacheAndPaths(PosterUrl, paths)
                If Not Preferences.MusicVidScrape Then SavePosterToPosterWallCache()
                ReportProgress(MSG_OK, "!!! Poster(s) scraped OK" & vbCrLf)


            Catch ex As Exception
                ReportProgress(MSG_ERROR, "!!! Problem Saving Poster" & vbCrLf & "!!! Error Returned :- " & ex.Message & vbCrLf & vbCrLf)
            End Try
        End If
    End Sub

    Sub DownloadFanart()
        If Not Preferences.savefanart Then
            ReportProgress(, "Fanart scraping not enabled" & vbCrLf)
            Exit Sub
        End If
        If Preferences.MovieChangeMovie AndAlso Preferences.MovieChangeKeepExistingArt Then Exit Sub

        If Preferences.MusicVidScrape Then
            ucMusicVideo.createScreenshot(mediapathandfilename, , True)
        Else
            DoDownloadFanart()
        End If
    End Sub

    Sub DoDownloadFanart()
        Dim imageexistspath As String = ""
        Dim FanartUrl As String = ""
        Dim MoviePath As String = NfoPathPrefName
        'Dim isfanartjpg As String = IO.Path.GetDirectoryName(MoviePath) & "\fanart.jpg"
        Dim isMovieFanart As String = MoviePath.Replace(".nfo", "-fanart.jpg")
        If IO.Path.GetFileName(MoviePath).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(MoviePath).ToLower = "index.nfo" Then
            _videotsrootpath = Utilities.RootVideoTsFolder(MoviePath)
        End If

        If Not Rescrape Then DeleteFanart()

        Dim paths As List(Of String) = Preferences.GetfanartPaths(MoviePath, If(_videotsrootpath <> "", _videotsrootpath, ""))
        If Not Preferences.overwritethumbs Then
            Dim lst As New List(Of String)
            For Each filepath In paths
                If File.Exists(filepath) Then
                    imageexistspath = filepath
                    lst.Add(filepath)
                End If
            Next
            If lst.Count > 0 Then
                For Each filepath In lst
                    paths.Remove(filepath)
                Next
            End If
            If paths.Count = 0 Then
                ReportProgress(, "Fanart already exists -> Skipping" & vbCrLf)
                Exit Sub
            End If
        End If
        If imageexistspath = "" Then
            FanartUrl = tmdb.GetBackDropUrl
        Else
            FanartUrl = imageexistspath
        End If

        If IsNothing(FanartUrl) Then
            ReportProgress("-Not available ", "Fanart not available for this movie on TMDb" & vbCrLf)
        Else
            ReportProgress("Fanart", )
            Try
                SaveFanartImageToCacheAndPaths(FanartUrl, paths)

                ReportProgress(MSG_OK, "!!! Fanart URL Scraped OK" & vbCrLf)
            Catch ex As Exception
                ReportProgress(MSG_ERROR, "!!! Problem Saving Fanart" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
            End Try

        End If
    End Sub

    Sub DownloadFromFanartTv(Optional rescrapelist As Boolean = False)
        If Preferences.MovieChangeMovie AndAlso Preferences.MovieChangeKeepExistingArt Then Exit Sub
        If Preferences.MovFanartTvscrape OrElse rescrapelist Then   'AndAlso (Preferences.allfolders Or Preferences.usefoldernames) 
            DoDownloadFromFanartTv(rescrapelist)
        Else
            ReportProgress(, "Scraping from Fanart.Tv, not selected" & vbCrLf)
            Exit Sub
        End If
    End Sub

    Sub DoDownloadFromFanartTv(Optional isRescrapelist As Boolean = False)
        Try
            Dim fcount As Integer = 0
            If Not Preferences.GetRootFolderCheck(ActualNfoPathAndFilename) Then
                Dim clearartLD As String = Nothing : Dim logoLD As String = Nothing: Dim clearart As String = Nothing : Dim logo As String = Nothing
                Dim poster As String = Nothing : Dim fanart As String = Nothing 
                Dim disc As String = Nothing : Dim banner As String = Nothing : Dim landscape As String = Nothing
                Dim lang As New List(Of String)
                Try
                    lang.Add(TMDb.LanguageCodes(0))
                Catch
                End Try
                lang.Add("en")
                Dim Overwrite As Boolean = If(isRescrapelist, Preferences.overwritethumbs, True)
                If IO.Path.GetFileName(NfoPathPrefName).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(NfoPathPrefName).ToLower = "index.nfo" Then
                    _videotsrootpath = Utilities.RootVideoTsFolder(NfoPathPrefName)
                End If
                Dim DestPath As String = Path.GetDirectoryName(NfoPathAndFilename) & "\"   'Preferences.GetFanartTvMoviePath(NfoPathAndFilename, If(_videotsrootpath <> "", _videotsrootpath, ""))
                Dim ID = If(_scrapedMovie.fullmoviebody.imdbid.Contains("tt"), _scrapedMovie.fullmoviebody.imdbid, If(_scrapedMovie.fullmoviebody.tmdbid<>"", _scrapedMovie.fullmoviebody.tmdbid,""))
                If ID = "" Then
                    ReportProgress(,"!!! Abort Fanart,Tv artwork, no IMDB or TMDBID Found" &vbCrLf)
                    Exit Sub
                End If
                Dim newobj As New FanartTv
                newobj.ID = ID
                newobj.src = "movie"
                Dim FanarttvMovielist As New FanartTvMovieList
                Try
                    FanarttvMovielist = newobj.FanarttvMovieresults
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
                For Each lan In lang
                    If IsNothing(clearart) Then
                        For Each Art In FanarttvMovielist.hdmovieclearart
                            If Art.lang = lan Then 
                                clearart = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(clearartLD) Then
                        For Each Art In FanarttvMovielist.movieart
                            If Art.lang = lan Then
                                clearartLD = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(logo) Then
                        For Each Art In FanarttvMovielist.hdmovielogo 
                            If Art.lang = lan Then
                                logo = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(logoLD) Then
                        For Each Art In FanarttvMovielist.movielogo 
                            If Art.lang = lan Then
                                logoLD = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(poster) Then
                        For Each Art In FanarttvMovielist.movieposter
                            If Art.lang = lan Then
                                poster = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(fanart) Then
                        For Each Art In FanarttvMovielist.moviebackground 
                            If Art.lang = lan Then
                                fanart = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(disc) Then
                        For Each Art In FanarttvMovielist.moviedisc 
                            If Art.lang = lan Then
                                disc = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(banner) Then
                        For Each Art In FanarttvMovielist.moviebanner
                            If Art.lang = lan Then
                                banner = Art.url
                                Exit For
                            End If
                        Next
                    End If
                    If IsNothing(landscape) Then
                        For Each Art In FanarttvMovielist.moviethumb 
                            If Art.lang = lan Then
                                landscape = Art.url
                                Exit For
                            End If
                        Next
                    End If
                Next
                If IsNothing(clearart) AndAlso Not IsNothing(clearartld) Then clearart = clearartLD 
                If IsNothing(logo) AndAlso Not IsNothing(logold) Then logo = logoLD
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlClearArt Then Utilities.DownloadImage(clearart, DestPath & "clearart.png", Overwrite)
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlClearLogo Then Utilities.DownloadImage(logo, DestPath & "logo.png", Overwrite)
                'Utilities.DownloadFile(poster, DestPath & "poster.jpg")
                'Utilities.DownloadFile(fanart, DestPath & "fanart.jpg")
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlDisc Then Utilities.DownloadImage(disc, DestPath & "disc.png", Overwrite)
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlBanner Then Utilities.DownloadImage(banner, DestPath & "banner.jpg", Overwrite)
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlLandscape Then Utilities.DownloadImage(landscape, DestPath & "landscape.jpg", Overwrite)
                'Fanart
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlFanart Then
                    Dim fanartpaths As List(Of String) = Preferences.GetfanartPaths(NfoPathPrefName, If(_videotsrootpath <> "", _videotsrootpath, ""))
                    If Not fanartpaths.Contains(DestPath & "fanart.jpg") Then fanartpaths.Add(DestPath & "fanart.jpg")
                    DownloadCache.SaveImageToCacheAndPaths(fanart, fanartpaths, False, 0, 0, Overwrite)
                End If
                'Poster
                If Preferences.MovFanartTvDlAll OrElse Preferences.MovFanartTvDlPoster Then
                    Dim posterpaths As List(Of String) = Preferences.GetPosterPaths(NfoPathPrefName, If(_videotsrootpath <> "", _videotsrootpath, ""))
                    If Not posterpaths.Contains(DestPath & "poster.jpg") Then posterpaths.Add(DestPath & "poster.jpg")
                    DownloadCache.SaveImageToCacheAndPaths(poster, posterpaths, False, 0, 0, Overwrite)
                End If

                ReportProgress(MSG_OK, "!!! Artwork from Fanart.Tv Downloaded OK" & vbCrLf)
            Else
                ReportProgress(, "!!! Artwork from Fanart.Tv bypassed as Movie is in Root Folder." & vbCrLf & "Artwork Downloaded requires Movies to be in separate folders." & vbCrLf)
            End If

        Catch ex As Exception
            ReportProgress(MSG_ERROR, "!!! Problem downloading from Fanart.Tv" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
        End Try
    End Sub

    Sub DownloadExtraFanart()
        If Preferences.MovieChangeMovie AndAlso Preferences.MovieChangeKeepExistingArt Then Exit Sub
        If Preferences.dlxtrafanart AndAlso (Preferences.allfolders Or Preferences.usefoldernames) Then
            DoDownloadExtraFanart()
        Else
            ReportProgress(, "Scraping Extra Fanart-Thumbs not selected" & vbCrLf)
        End If
    End Sub

    Sub DoDownloadExtraFanart()
        Try
            Dim fcount As Integer = 0
            If Not Preferences.GetRootFolderCheck(ActualNfoPathAndFilename) Then
                Dim fanartarray As New List(Of str_ListOfPosters)
                Dim xfanart As String = Strings.Left(FanartPath, FanartPath.LastIndexOf("\")) & "\extrafanart\"
                Dim xthumb As String = Strings.Left(FanartPath, FanartPath.LastIndexOf("\")) & "\extrathumbs\thumb"
                Dim xf As Boolean = Preferences.movxtrafanart
                Dim xt As Boolean = Preferences.movxtrathumb
                If xf Then Directory.CreateDirectory(xfanart)
                If xt Then Directory.CreateDirectory(xthumb.Replace("\thumb", ""))
                Dim owrite As Boolean = Preferences.overwritethumbs
                Dim tmpUrl As String = ""
                Dim xtraart As New List(Of String)
                fanartarray.Clear()
                fanartarray.AddRange(tmdb.Fanart)
                fcount = fanartarray.Count
                If fcount > 1 Then
                    'Dim x As Integer = If(fcount > 4, 4, fcount)
                    Dim i As Integer = 1
                    Do
                        xtraart.Clear()
                        tmpUrl = fanartarray(i).hdUrl
                        If Utilities.UrlIsValid(tmpUrl) Then
                            If xf Then
                                Dim fanartfilename As String = tmpUrl.Substring(tmpUrl.LastIndexOf("/")+1, (tmpUrl.Length - tmpUrl.LastIndexOf("/")-1))
                                If Not (IO.File.Exists(xfanart & fanartfilename) AndAlso Not owrite) Then
                                    xtraart.Add(xfanart & fanartfilename)
                                End If
                            End If
                            If xt AndAlso i < 6 Then
                                If Not (IO.File.Exists((xthumb & i.ToString & ".jpg")) AndAlso Not owrite) Then
                                    xtraart.Add((xthumb & i.ToString & ".jpg"))
                                End If
                            End If
                            If xtraart.Count > 0 Then
                                SaveFanartImageToCacheAndPaths(tmpUrl, xtraart)
                            End If
                        End If
                        i += 1
                    Loop Until i = (Preferences.movxtrafanartqty+1) or i > fcount-1
                End If
            Else
                ReportProgress(MSG_OK, "!!! ExtraFanart\ExtraThumbs not downloaded as movie is in Root Folder." & vbCrLf)
                Exit Sub
            End If
            If fcount > 1 Then
                ReportProgress(MSG_OK, "!!! ExtraFanart\ExtraThumbs Downloaded OK" & vbCrLf)
            Else
                ReportProgress(MSG_OK, "!!! Insufficient Fanart to download as ExtraFanart\ExtraThumbs" & vbCrLf)
            End If
        Catch ex As Exception
            ReportProgress(MSG_ERROR, "!!! Problem Saving ExtraFanart or ExtraThumbs" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
        End Try
    End Sub

    Sub DownloadMovieSetArt()
        If Preferences.dlMovSetArtwork AndAlso _scrapedMovie.fullmoviebody.movieset.MovieSetId <> "" Then
            DoDownloadMovieSetArtwork()
        End If
    End Sub

    Sub DoDownloadMovieSetArtwork()
        If _scrapedMovie.fileinfo.movsetposterpath <> "" Then
            Dim _api As New TMDb
            _api.Imdb = ""
            _api.TmdbId = _scrapedMovie.fullmoviebody.movieset.MovieSetId
            _api.CollectionSearch = True
            Dim MsetFanart As New str_ListOfPosters
            Dim MsetPoster As New str_ListOfPosters
            If _api.Fanart.Count > 0 Then
                MsetFanart = _api.Fanart(0)
                If Not File.Exists(_scrapedMovie.fileinfo.movsetfanartpath) OrElse Preferences.overwritethumbs Then
                    Try
                        SaveFanartImageToCacheAndPath(MsetFanart.hdUrl, _scrapedMovie.fileinfo.movsetfanartpath)
                        ReportProgress(MSG_OK, "!!! MovieSet Fanart Downloaded OK" & vbCrLf)
                    Catch ex As Exception
                        ReportProgress(MSG_ERROR, "!!! Problem Saving MovieSet Fanart" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
                    End Try
                Else
                    ReportProgress(MSG_OK, "!!! MovieSet Fanart Already Exists" & vbCrLf)
                End If
            Else
                ReportProgress(, "!!! No Fanart available for this MovieSet" & vbCrLf)
            End If
            If _api.MC_Posters.Count > 0 Then
                MsetPoster = _api.MC_Posters(0)
                If Not File.Exists(_scrapedMovie.fileinfo.movsetposterpath) OrElse Preferences.overwritethumbs Then
                    Try
                        SavePosterImageToCacheAndPath(MsetPoster.hdUrl, _scrapedMovie.fileinfo.movsetposterpath)
                        ReportProgress(MSG_OK, "!!! MovieSet Poster Downloaded OK" & vbCrLf)
                    Catch ex As Exception
                        ReportProgress(MSG_ERROR, "!!! Problem Saving MovieSet Poster" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
                    End Try
                Else
                    ReportProgress(MSG_OK, "!!! MovieSet Poster Already Exists" & vbCrLf)
                End If
            Else
                ReportProgress(, "!!! No Poster available for this MovieSet" & vbCrLf)
            End If
            _api = Nothing
        Else
            ReportProgress(, "MovieSet Artwork scraping Failed.  No Folder for saving Artwork" & vbCrLf)
        End If
    End Sub

    Sub DeleteScrapedFiles(ByVal incTrailer As Boolean, Optional ByVal DelArtwork As Boolean = True)
        Try
            LoadNFO
            If Not Preferences.MusicVidScrape Then
                If DelArtwork Then DeleteActors     'remove actor images if present

                RemoveActorsFromCache(_scrapedMovie.fullmoviebody.imdbid        )
                RemoveMovieFromCache (_scrapedMovie.fileinfo.fullpathandfilename)
                If incTrailer Then DeleteTrailer
            End If
            If Not Preferences.MovieChangeMovie AndAlso Preferences.MovieChangeKeepExistingArt OrElse Preferences.MovieDeleteNfoArtwork Then
                If DelArtwork Then
                    DeletePoster
                    DeleteFanart
                End If
            End If

            DeleteNFO
        Catch ex As Exception
            ReportProgress(MSG_ERROR,"!!! Problem deleting scraped files" & vbCrLf & "!!! Error Returned :- " & ex.Message & vbCrLf & vbCrLf) 
        End Try
    End Sub

    Sub DeleteActors()
        Try
            'Only delete actors if movies are in separate folders
            If Not Preferences.GetRootFolderCheck(NfoPathPrefName) Then
                Dim thispath As String = IO.Path.GetDirectoryName(NfoPathAndFilename)
                thispath &= "\.actors"
                If IO.Directory.Exists(thispath) Then
                    Try
                        IO.Directory.Delete(thispath, True)
                        Exit Sub
                    Catch
                    End Try
                End If
            End If
            Dim ap As String = ActorPath
            For Each act In Actors
                Dim actorfilename As String = GetActorFileName(act.ActorName)
                If File.Exists(actorfilename) Then
                    Utilities.SafeDeleteFile(actorfilename)
                End If
                If File.Exists(actorfilename.Replace(".tbn", ".jpg")) Then
                    Utilities.SafeDeleteFile(actorfilename.Replace(".tbn", ".jpg"))
                End If
            Next
        Catch
        End Try

        'To Do : Delete from networkpath = Preferences.actorsavepath
    End Sub

    Sub DeleteNFO
        Utilities.SafeDeleteFile(ActualNfoPathAndFilename)
    End Sub

    Sub DeletePoster
        DeleteFile(PosterPath)
        If Preferences.createfolderjpg Then DeleteFile(PosterPath.Replace(Path.GetFileName(PosterPath),"folder.jpg"))
        DeleteFile(PosterDVDFrodo)
        DeleteFile(ActualPosterPath)
    End Sub

    Sub DeleteFanart
        DeleteFile(FanartPath)
        DeleteFile(FanartDVDFrodo)
        DeleteFile(ActualFanartPath)
    End Sub

    Sub DeleteExtraFiles
        DeleteFolder(NfoPath & "extrafanart")
        DeleteFolder(NfoPath & "extrathumbs")
    End Sub

    Sub DeleteFanarTvFiles
        For Each filetype In Utilities.fanarttvfiles
            DeleteFile(NfoPath & filetype)
        Next
    End Sub

    Sub DeleteTrailer
        DeleteFile(ActualTrailerPath)
    End Sub

    Sub DeleteFile(fileName As String)
        If Not IO.File.Exists(fileName) then Exit Sub
        Try
            File.Delete(fileName)
        Catch ex As Exception
            Dim answer = MsgBox("It appears you don't have ownership of all your movie files (it's a Windows thing from Vista onwards, even if you're an Administrator)." & vbCrLf & vbCrLf & "Would you like help on resolving this problem?", MsgBoxStyle.YesNo)
            If answer=MsgBoxResult.Yes then
                ShowTakeOwnsershipHelp
            End If
        End Try
    End Sub

    Sub DeleteFolder(foldername As String)
        If Not IO.Directory.Exists(foldername) Then Exit Sub
        Try
            IO.Directory.Delete(foldername, True)
        Catch ex As Exception
            Dim answer = MsgBox("It appears you don't have ownership of all your movie files (it's a Windows thing from Vista onwards, even if you're an Administrator)." & vbCrLf & vbCrLf & "Would you like help on resolving this problem?", MsgBoxStyle.YesNo)
            If answer=MsgBoxResult.Yes then
                ShowTakeOwnsershipHelp
            End If
        End Try
    End Sub

    Private Sub DeleteZeroLengthFile(fileName As String)
        If File.Exists(fileName) Then
            If (New IO.FileInfo(fileName)).Length = 0 Then
                File.Delete(fileName)
                ReportProgress("-Zero length trailer deleted ", "Zero length trailer deleted : [" & fileName & "]")
            End If
        End If
    End Sub

    Sub ShowTakeOwnsershipHelp
       Try
            Dim FileName = Preferences.applicationPath & "\Assets\TakeOwnership.htm"
            Dim helpFile =  "file:///" & FileName.Replace(" ", "%20").Replace("\","/")

            If Preferences.selectedBrowser <> "" then
                Process.Start(Preferences.selectedBrowser,helpFile)
            Else
                Try
                    Process.Start(helpFile)
                Catch ex As Exception
                    MessageBox.Show( "An error occurred while trying to launch the default browser - Using the 'Locate browser' button under 'General Preferences' to select the browser should resolve this error", "", MessageBoxButtons.OK )
                End Try
            End If 
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Sub HandleOfflineFile
        If File.Exists(TempOffLineFileName) Then
            File.Delete(TempOffLineFileName)
            Call mov_OfflineDvdProcess(_movieCache.fullpathandfilename, _movieCache.title, Utilities.GetFileName(_movieCache.fullpathandfilename))
        End If
    End Sub

    Public Sub AssignMovieToAddMissingData

        _movieCache.missingdata1         = GetMissingData
        _movieCache.FrodoPosterExists    = FrodoPosterExists
        _movieCache.PreFrodoPosterExists = PreFrodoPosterExists
    End Sub


    Public Function GetMissingData

        Dim missingdata As Byte = 0

        If Preferences.CheckmissingFanart(NfoPathPrefName) Then 'Not File.Exists(FanartPath) Then
            missingdata += 1
        End If

        If Preferences.CheckmissingPoster(NfoPathPrefName) Then 'Not File.Exists(PosterPath) Then
            missingdata += 2
        End If

        'Not used yet
        If Not TrailerExists Then
            missingdata += 4
        End If

        If MissingLocalActors Then
            missingdata += 8
        End If

        Return missingdata
    End Function

    Private Sub mov_OfflineDvdProcess(ByVal nfopath As String, ByVal title As String, ByVal mediapath As String)
 
        Dim tempint2   As Integer = 2097152
        Dim SizeOfFile As Integer = FileLen(mediapath)

        If SizeOfFile > tempint2 Then
            Exit Sub
        End If

        Try
            Dim fanartpath As String = ""

            If File.Exists(Preferences.GetFanartPath(nfopath)) Then
                fanartpath = Preferences.GetFanartPath(nfopath)
            Else
                fanartpath = Utilities.DefaultOfflineArtPath
            End If

            Dim curImage As Image = Image.FromFile(fanartpath)

            Dim tempstring As String = Preferences.OfflineDVDTitle.Replace("%T", title)

            Dim g As System.Drawing.Graphics

            g = Graphics.FromImage(curImage)
            Dim semiTransBrush As New SolidBrush(Color.FromArgb(80, 0, 0, 0))

            Dim drawString As String = tempstring

            Dim drawFont As New System.Drawing.Font("Arial", 40)
            Dim drawBrush As New SolidBrush(Color.White)

            Dim StringSize As New SizeF
            StringSize = g.MeasureString(drawString, drawFont)
            Dim width As Single = StringSize.Width + 5
            Dim height As Single = StringSize.Height + 5


            If height < (curImage.Height / 100) * 8 Then
                Do
                    Dim newsize As Integer = drawFont.Size + 1
                    drawFont = New System.Drawing.Font("Arial", newsize)
                    StringSize = g.MeasureString(drawString, drawFont)
                    height = StringSize.Height
                Loop Until height > (curImage.Height / 100) * 8
            End If
            If height > (curImage.Height / 100) * 8 Then
                Do
                    Dim newsize As Integer = drawFont.Size - 1
                    drawFont = New System.Drawing.Font("Arial", newsize)
                    StringSize = g.MeasureString(drawString, drawFont)
                    height = StringSize.Height
                Loop Until height < (curImage.Height / 100) * 8
            End If
            StringSize = g.MeasureString(drawString, drawFont)
            width = StringSize.Width
            height = StringSize.Height
            If width > curImage.Width - 30 Then
                Do
                    Dim newsize As Integer = drawFont.Size - 1
                    drawFont = New System.Drawing.Font("Arial", newsize)
                    StringSize = g.MeasureString(drawString, drawFont)
                    width = StringSize.Width + 20
                Loop Until width < curImage.Width - 30
            End If
            StringSize = g.MeasureString(drawString, drawFont)
            width = StringSize.Width + 5
            height = StringSize.Height + 5
            Dim x As Integer = (curImage.Width / 2) - (width / 2)
            Dim y As Integer = (curImage.Height - StringSize.Height) - ((curImage.Height / 100) * 2)
            Dim drawRect As New RectangleF(x, y, width, height)


            g.FillRectangle(semiTransBrush, New Rectangle(x, y, width, height))

            Dim drawFormat As New StringFormat
            drawFormat.Alignment = StringAlignment.Center

            g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat)
            For f = 1 To 16
                Dim path As String
                If f < 10 Then
                    path = Preferences.applicationPath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    path = Preferences.applicationPath & "\Settings\0" & f.ToString & ".jpg"
                End If
                curImage.Save(path, Drawing.Imaging.ImageFormat.Jpeg)
            Next

            Dim myProcess As Process = New Process
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.FileName = Preferences.applicationPath & "\Assets\ffmpeg.exe"
            Dim proc_arguments As String = "-r 1 -b:v 1800 -qmax 6 -i """ & Preferences.applicationPath & "\Settings\%03d.jpg"" -vcodec msmpeg4v2 -y """ & mediapath & """"
            myProcess.StartInfo.Arguments = proc_arguments
            myProcess.Start()
            myProcess.WaitForExit()

            For f = 1 To 16
                Dim tempstring4 As String
                If f < 10 Then
                    tempstring4 = Preferences.applicationPath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    tempstring4 = Preferences.applicationPath & "\Settings\0" & f.ToString & ".jpg"
                End If
                Try
                    IO.File.Delete(tempstring4)
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
            Next
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub
    #End Region 'Subs


    #Region "Events"
    Private Sub _WebFileDownloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles _WebFileDownloader.FileDownloadSizeObtained
        RaiseEvent FileDownloadSizeObtained(iFileSize)
    End Sub

    Private Sub _WebFileDownloader_FileDownloadComplete() Handles _WebFileDownloader.FileDownloadComplete
        RaiseEvent FileDownloadComplete()
    End Sub

    Private Sub _WebFileDownloader_FileDownloadFailed(ByVal ex As System.Exception) Handles _WebFileDownloader.FileDownloadFailed
        RaiseEvent FileDownloadFailed(ex)
    End Sub

    Private Sub _WebFileDownloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles _WebFileDownloader.AmountDownloadedChanged
        RaiseEvent AmountDownloadedChanged(iNewProgress)
    End Sub
    #End Region 'Events


    #Region "TMDB Test scrape"
    'Just for speed testing vs IMDB - Results: TMDb ~ 2 secs, IMDB ~ 5 secs.
    'IMDB is more complete & IMO worth the little extra time
    Sub TMDbScrape
        Actions.Items.Clear
        Actions.Items.Add( New ScrapeAction(AddressOf IniTmdb               ,"Ini TMDb"                    ) )
        Actions.Items.Add( New ScrapeAction(AddressOf TMDbAssignScrapedMovie,"TMDbAssignScrapedMovie"      ) )
        RunScrapeActions
    End Sub

    Sub TMDbAssignScrapedMovie

        _scrapedMovie.fullmoviebody.title = tmdb.Movie.title
        _scrapedMovie.fullmoviebody.originaltitle = tmdb.Movie.original_title
        
        _scrapedMovie.alternativetitles.AddRange(tmdb.AlternateTitles)

        If tmdb.Movie.production_countries.Count > 0 then
            _scrapedMovie.fullmoviebody.country = tmdb.Movie.production_countries(0).name
        End If

 '       _scrapedMovie.fullmoviebody.credits   = "Unknown"
        _scrapedMovie.fullmoviebody.director  = tmdb.Director
        _scrapedMovie.fullmoviebody.stars     = tmdb.Stars
        _scrapedMovie.fullmoviebody.genre     = tmdb.Genres
        _scrapedMovie.fullmoviebody.mpaa      = tmdb.Certification
        _scrapedMovie.fullmoviebody.outline   = tmdb.Movie.overview
        _scrapedMovie.fullmoviebody.plot      = tmdb.Movie.overview
        _scrapedMovie.fullmoviebody.premiered = tmdb.Movie.release_date
        _scrapedMovie.fullmoviebody.rating    = tmdb.Movie.vote_average
        _scrapedMovie.fullmoviebody.runtime   = tmdb.Movie.runtime

        If tmdb.Movie.production_companies.Count > 0 then
            _scrapedMovie.fullmoviebody.studio = tmdb.Movie.production_companies(0).name
        End If

        _scrapedMovie.fullmoviebody.tagline   = tmdb.Movie.tagline
'       _scrapedMovie.fullmoviebody.top250    = tmdb.Movie.t
        _scrapedMovie.fullmoviebody.votes     = tmdb.Movie.vote_count
        _scrapedMovie.fullmoviebody.year      = tmdb.Movie.release_date
        _scrapedMovie.fullmoviebody.imdbid    = tmdb.Imdb


        If _scrapedMovie.fullmoviebody.plot      = ""      Then _scrapedMovie.fullmoviebody.plot      = _scrapedMovie.fullmoviebody.outline
        If _scrapedMovie.fullmoviebody.playcount = Nothing Then _scrapedMovie.fullmoviebody.playcount = "0"
        If _scrapedMovie.fullmoviebody.lastplayed = Nothing Then _scrapedMovie.fullmoviebody.lastplayed = ""
        If _scrapedMovie.fullmoviebody.top250    = Nothing Then _scrapedMovie.fullmoviebody.top250    = "0"

    End Sub
    #End Region 'TMDB Test scrape


    Public Shared Function getExtraIdFromNFO(ByVal fullPath As String, Optional log As String="") As String
        Dim extrapossibleID As String = String.Empty
        Dim fileNFO As String = fullPath
        If Utilities.findFileOfType(fileNFO, ".nfo") Then
            Dim objReader As New StreamReader(fileNFO)
            Dim tempInfo As String = objReader.ReadToEnd
            objReader.Close()
            objReader = Nothing
            Dim M As Match = Regex.Match(tempInfo, "(tt\d{7})")
            If M.Success = True Then
                extrapossibleID = M.Value
                log &= "IMDB ID found in nfo file:- " & extrapossibleID & vbCrLf
            Else
                log &= "No IMDB ID found in NFO" & vbCrLf
            End If
            If Preferences.renamenfofiles And Not IsMCNfoFile(fileNFO) Then   'reenabled choice as per user preference
                Try
                    If Not File.Exists(fileNFO.Replace(".nfo", ".info")) Then
                        File.Move(fileNFO, fileNFO.Replace(".nfo", ".info"))
                        log &= "renaming nfo file to:- " & fileNFO.Replace(".nfo", ".info") & vbCrLf
                    Else
                        log &= "!!! Unable to rename file, """ & fileNFO & """ already exists" & vbCrLf
                    End If
                Catch
                    log &= "!!! Unable to rename file, """ & fileNFO & """ already exists" & vbCrLf
                End Try
            Else
                log &= "Current nfo file will be overwritten" & vbCrLf
            End If
        End If
        Return extrapossibleID
    End Function


    Public Function fileRename(ByRef movieFileInfo As Movie) As String 'ByVal movieDetails As str_BasicMovieNFO, ByRef movieFileInfo As Movie) As String
        If Preferences.MusicVidScrape Then Return ""  ' Temporary till get music vid posters scraping.
        Dim log As String = ""
        Dim newpath As String = movieFileInfo.NfoPath
        Dim mediaFile As String = movieFileInfo.mediapathandfilename
        Dim movieStackList As New List(Of String)(New String() {mediaFile})
        Dim stackName As String = mediaFile
        Dim isStack As Boolean = False
        Dim isSubStack As Boolean = False
        Dim isFirstPart As Boolean = True
        Dim nextStackPart As String = ""
        Dim stackdesignator As String = ""
        Dim newextension As String = IO.Path.GetExtension(mediaFile)
        Dim subName1 As String = mediaFile.Substring(0, mediaFile.LastIndexOf(newextension))         'Replace(newextension,"")
        Dim subName As String = subName1
        Dim newfilename As String = UserDefinedBaseFileName
        Dim targetMovieFile As String = ""
        Dim targetNfoFile As String = ""
        Dim subextn As List(Of String) = Utilities.ListSubtitleFilesExtensions(subName)
        Dim subStackList As New List(Of String)'(New String() {subName})
        'If Not subextn = "" Then subStackList.Add((subName & subextn))
        
        If newextension.ToLower = ".disc" Then
            log &= "!!! Media Stub files are not to be renamed." & vbCrLf
            Return log
        End If

        Dim aFileExists As Boolean = False
        Try
            ''create new filename (hopefully removing invalid chars first else Move (rename) will fail)
            
            targetMovieFile = newpath & newfilename
            targetNfoFile = targetMovieFile
            If Utilities.testForFileByName(targetMovieFile, newextension) Then
                aFileExists = True
            Else
                'determine if any 'part' names are in the original title - if so, compile a list of stacked media files for renaming
                Do While Utilities.isMultiPartMedia(stackName, False, isFirstPart, stackdesignator, nextStackPart)
                    If isFirstPart Then
                        isStack = True                    'this media file has already been added to the list, but check for existing file with new name
                        Dim i As Integer                  'sacrificial variable to appease the TryParseosaurus Checks
                        targetMovieFile = newpath & newfilename & stackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
                        If Utilities.testForFileByName(targetMovieFile, newextension) Then
                            aFileExists = True
                            Exit Do
                        End If
                        If Preferences.namemode = "1" Then targetNfoFile = targetMovieFile
                    Else
                        movieStackList.Add(mediaFile)
                    End If
                    stackName = newpath & stackName & stackdesignator & nextStackPart & newextension
                    mediaFile = stackName
                Loop
                'Check for multi-part subtitle files
                If subextn.Count > 0 Then    'If no subtitle file, then skip
                    For Each subex In subextn
                    subName = subName1 & subex
                        subStackList.Add(subName)
                        Do While Utilities.isMultiPartMedia(subName, False, isFirstPart, stackdesignator, nextStackPart)
                            If isFirstPart Then
                                isSubStack = True                    'this media file has already been added to the list, but check for existing file with new name
                                Dim i As Integer                  'sacrificial variable to appease the TryParseosaurus Checks
                                targetMovieFile = newpath & newfilename & stackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
                                If Utilities.testForFileByName(targetMovieFile, subex) Then
                                    aFileExists = True
                                    Exit Do
                                End If
                                'If Preferences.namemode = "1" Then targetNfoFile = targetMovieFile
                            Else
                                subStackList.Add(mediaFile)
                            End If
                            subName = newpath & subName & stackdesignator & nextStackPart & subex
                            mediaFile = subName
                        Loop
                    Next
                End If
            End If

            If aFileExists = False Then         'if none of the possible renamed files already exist then we rename found media files
                Dim logRename As String = ""    'used to build up a string of the renamed files for the log
                movieStackList.Sort()           'we're sure hoping the originals were labelled correctly, ie only incremental numbers changing!

                    For i = 0 To movieStackList.Count - 1
                        Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), newextension)
                        File.Move(movieStackList(i), newpath & changename)
                        logRename &= If(i, " and ", "") & changename
                    Next
                    log &= "Renamed Movie File to " & logRename & vbCrLf
                logRename = ""
                    
                    'rename subtitle files if any
                'Dim oldname = 
                    For i = 0 To subStackList.Count - 1
                        Dim oldname = subStackList(i)
                        Dim newsubextn As String = IO.Path.GetExtension(oldname)
                        'Utilities.isMultiPartMedia(oldname, True) ', isFirstPart, stackdesignator, nextStackPart)
                        Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), newsubextn)
                        'Dim changename As String = subStackList(i).Replace(oldname,newfilename)
                        File.Move(subStackList(i), newpath & changename)
                        logRename &= If(i, " and ", "") & changename
                    Next
                    log &= "Renamed Subtitle File to " & logRename & vbCrLf
                'Next

                    For Each anciliaryFile As String In Utilities.acceptedAnciliaryExts 'rename any anciliary files with the same name as the movie
                        If File.Exists(subName1 & anciliaryFile) Then
                            File.Move(subName1 & anciliaryFile, targetMovieFile & anciliaryFile)
                            log &= "Renamed '" & anciliaryFile & "' File" & vbCrLf
                        End If
                    Next

                'update the new movie structure with the new data
                movieFileInfo.mediapathandfilename = targetMovieFile & newextension 'this is the new full path & filname to the rename media file
                RenamedBaseName = targetNfoFile

            Else
                log &= String.Format("A file exists with the target filename of '{0}' - RENAME SKIPPED{1}", newfilename, vbCrLf)
            End If
        Catch ex As Exception
            log &= "!!! !!Rename Movie File FAILED !!!" & vbCrLf
        End Try
        Return log
    End Function

    Public Function folderRename(ByRef movieFileInfo As Movie) As String
        If Preferences.MusicVidScrape Then Return "" ' Temporary till get music vid posters scraping.
        Dim Log As String = ""
        Dim NoDel As Boolean = False
        Dim FilePath As String = movieFileInfo.nfopath   'current path
        Dim Filename As String = Path.GetFileNameWithoutExtension(movieFileInfo.NfoPathAndFilename)
        Dim currentroot As String = ""
        Dim stackname As String = mediapathandfilename
        Dim newextension As String = IO.Path.GetExtension(mediapathandfilename)
        Dim isStack         = False
        Dim isSubStack      = False
        Dim isFirstPart     = True
        Dim nextStackPart   = ""
        Dim stackdesignator = ""
         
        'Get current root folder
        For Each rtfold In Preferences.movieFolders
            If FilePath.Contains(rtfold.rpath) Then currentroot = rtfold.rpath
        Next
        If currentroot.LastIndexOf("\") <> currentroot.Length-1 Then currentroot = currentroot & "\"

        Dim inrootfolder As Boolean = (currentroot = FilePath)
        Dim newFolder As String = UserDefinedBaseFolderName
        Dim newpatharr As New List(Of String)
        newpatharr.AddRange(newFolder.Split("\"))
        
        'Remove -none- if no Movieset
        If newpatharr.Count > 0 Then
            Dim badfolder As Integer = 0
            Do Until badfolder = -1
                badfolder = -1
                For num = 0 to newpatharr.Count-1
                    If newpatharr(num).ToLower = "-none-" Then 
                        badfolder = num
                        Exit For
                    End If
                Next
                If badfolder >= 0 Then
                    newpatharr.RemoveAt(badfolder)
                End If
            Loop
        Else If newpatharr.Count = 0 Then
            log &= "!!!No Folder string set in Preferences" & vbCrLf 
            Return log
        End If
        
        'Check if new path already exists and if not, Create new directory/s
        FilePath = FilePath.Replace("VIDEO_TS\","")             'If DVD VIDEO_TS folder, step back one folder so we copy folder as well.
        FilePath = FilePath.Replace("BDMV\","")             'If BD BDMV folder, step back one folder so we copy folder as well.
        Dim checkfolder As String = currentroot.Substring(0, currentroot.Length -1)
        If newpatharr.Count = 1 And Not inrootfolder Then                       'If only one folder in new folder pattern,
            Dim lastfolder As String = Utilities.GetLastFolderInPath(FilePath)  'Create in current directory, excluding if
            checkfolder = FilePath.Replace((lastfolder & "\"), newpatharr(0))   'movie is in root folder already
        Else
            For Each folder In newpatharr
                checkfolder &= "\" & folder
            Next
        End If
        If Not Directory.Exists(checkfolder) Then
                Directory.CreateDirectory(checkfolder)
                log &= "!!! New path created:- " & checkfolder & vbCrLf 
        Else
            If (checkfolder & "\") = FilePath Then
                log &= "!!! Path for: " & checkfolder & vbCrLf 
                log &= "!!! already Exists, no need to move files" & vbCrLf & vbcrlf
                Return log
            End If
        End If
        
        'If not in root, move files to new path and any sub folders
        If Not inrootfolder Then
            Dim toPathInfo = New DirectoryInfo(checkfolder)
            Dim fromPathInfo = New DirectoryInfo(FilePath)
            ''move all files
            For Each file As IO.FileInfo In fromPathInfo.GetFiles()
                file.MoveTo(Path.Combine(checkfolder, file.Name))
            Next
            ''move any sub directories
            For Each dir As DirectoryInfo In fromPathInfo.GetDirectories()
                dir.MoveTo(Path.Combine(checkfolder, dir.Name))
            Next
            If Utilities.IsDirectoryEmpty(FilePath) Then
                Try
                    IO.Directory.Delete(FilePath)
                Catch ex As IOException 
                    log &= "!!! Could not delete original folder:- " & FilePath & vbCrLf 
                    NoDel = True
                End Try
            End If
            log &= "!!! All files/Folders moved to new path" & vbCrLf 
        Else
            'Else if in Root folder, moved to new folder movie and ancillary files.
            Dim Moviename As String = ""
            If Utilities.isMultiPartMedia(stackname, False, isFirstPart, stackdesignator, nextStackPart) Then
                Moviename = stackname
            Else
                Moviename = stackname  '_movieCache.filename.Replace(".nfo","")
            End If
            Dim di As DirectoryInfo = New DirectoryInfo((currentroot))
            For Each fi As IO.FileInfo In di.GetFiles((Moviename & "*.*"))
                fi.MoveTo(Path.Combine(checkfolder, fi.Name))
            Next
            log &= "Movie moved from Root folder into new path" & vbCrLf  
        End If

        'update path info
        movieFileInfo.mediapathandfilename = checkfolder & "\" & movieFileInfo.mediapathandfilename.Replace(FilePath,"")
        RenamedBaseName = (checkfolder & "\" & movieFileInfo.NfoPathAndFilename.Replace(FilePath,"")).Replace(".nfo","")
        log &= "!!! Folder structure created successfully" & vbCrLf &vbCrLf 
        Try

        Catch ex As Exception
            log &= "!!! !!Rename Movie File FAILED !!!" & vbCrLf
        End Try
        Return log
    End Function

    Function NeedTMDb(rl As RescrapeList)
        Return rl.trailer Or rl.Download_Trailer Or rl.posterurls Or rl.missingposters Or rl.missingfanart Or rl.tmdb_set_name Or rl.tmdb_set_id Or
               rl.Frodo_Poster_Thumbs Or rl.Frodo_Fanart_Thumbs or rl.dlxtraart Or rl.TagsFromKeywords or rl.actors or rl.ArtFromFanartTv Or rl.missingmovsetart
    End Function

    Function RescrapeBody(rl As RescrapeList)
        Return rl.credits or rl.director or rl.stars   or rl.genre   or rl.mpaa   or rl.plot  or rl.premiered or rl.rating or 
               rl.runtime or rl.studio   or rl.tagline or rl.outline or rl.top250 or rl.votes or rl.country   or rl.year   or
               rl.title
    End Function
  
    Public Sub RescrapeSpecific(rl As RescrapeList)

        Rescrape = True
        _rescrapedMovie = New FullMovieDetails
        
        'Loads previously scraped details from NFO into _scrapedMovie
        LoadNFO
        ReportProgress(, "!!! Rescraping data for --> " & _scrapedMovie.fullmoviebody.title & vbCrLf)
        If Cancelled() Then Exit Sub

        If RescrapeBody(rl) then  
            
            Scraped  = True
            If Preferences.movies_useXBMC_Scraper OrElse rl.FromTMDB Then
                Dim useID As String = If(_scrapedMovie.fullmoviebody.tmdbid <> "", _scrapedMovie.fullmoviebody.tmdbid, _scrapedMovie.fullmoviebody.imdbid)
                _imdbBody = TmdbScrapeBody(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year, useID)
                If ImdbBody.ToLower = "error" Then   'Failed...
                    ReportProgress(MSG_ERROR,"!!! - ERROR! - Rescrape TMDB body with refs """ & _scrapedMovie.fullmoviebody.title & """, """ & _scrapedMovie.fullmoviebody.year & """, """ & useID & """" & vbCrLf )
                Else
                    ReportProgress(MSG_OK,"!!! Movie Body Scraped OK" & vbCrLf)
                    AssignScrapedMovie(_rescrapedMovie)
                End If
            Else
                _imdbBody = ImdbScrapeBody(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year, _scrapedMovie.fullmoviebody.imdbid)

                If _imdbBody = "MIC" Then                        
                    ReportProgress(MSG_ERROR, "!!! - ERROR! - Rescrape IMDB body failed with refs """ & _scrapedMovie.fullmoviebody.title & """, """ & _scrapedMovie.fullmoviebody.year & """, """ & _scrapedMovie.fullmoviebody.imdbid & """, """ & Preferences.imdbmirror & """" & vbCrLf)
                Else
                    ReportProgress(MSG_OK,"!!! Movie Body Scraped OK" & vbCrLf)
                    AssignScrapedMovie(_rescrapedMovie)
                End If
            End If
            
            UpdateProperty( _rescrapedMovie.fullmoviebody.tmdbid   , _scrapedMovie.fullmoviebody.tmdbid   , True         , True)
            UpdateProperty( _rescrapedMovie.fullmoviebody.credits  , _scrapedMovie.fullmoviebody.credits  , rl.credits   , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.director , _scrapedMovie.fullmoviebody.director , rl.director  , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.stars    , _scrapedMovie.fullmoviebody.stars    , rl.stars     , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.genre    , _scrapedMovie.fullmoviebody.genre    , rl.genre     , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.mpaa     , _scrapedMovie.fullmoviebody.mpaa     , rl.mpaa      , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.plot     , _scrapedMovie.fullmoviebody.plot     , rl.plot      , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.premiered, _scrapedMovie.fullmoviebody.premiered, rl.premiered , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.rating   , _scrapedMovie.fullmoviebody.rating   , rl.rating    , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.runtime  , _scrapedMovie.fullmoviebody.runtime  , rl.runtime   , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.studio   , _scrapedMovie.fullmoviebody.studio   , rl.studio    , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.tagline  , _scrapedMovie.fullmoviebody.tagline  , rl.tagline   , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.outline  , _scrapedMovie.fullmoviebody.outline  , rl.outline   , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.top250   , _scrapedMovie.fullmoviebody.top250   , rl.top250    , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.votes    , _scrapedMovie.fullmoviebody.votes    , rl.votes     , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.country  , _scrapedMovie.fullmoviebody.country  , rl.country   , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.year     , _scrapedMovie.fullmoviebody.year     , rl.year      , rl.EmptyMainTags)  
            UpdateProperty( _rescrapedMovie.fullmoviebody.title    , _scrapedMovie.fullmoviebody.title    , rl.title     , rl.EmptyMainTags)

            'If Preferences.movies_useXBMC_Scraper Then
            '    If Preferences.XbmcTmdbVotesFromImdb Then UpdateProperty(IMDB_Votes, _scrapedMovie.fullmoviebody.votes, rl.votes, rl.EmptyMainTags)
            '    If Preferences.XbmcTmdbCertFromImdb Then UpdateProperty(IMDB_Mpaa, _scrapedMovie.fullmoviebody.mpaa, rl.mpaa, rl.EmptyMainTags)
            'End If

            If rl.title 
                If Preferences.sorttitleignorearticle Then                 'add ignored articles to end of
                    Dim x As Boolean = False
                    Dim titletext As String = _scrapedMovie.fullmoviebody.title         'sort title. Over-rides independent The or A settings.                                
                    If titletext.ToLower.IndexOf("the ") = 0 Then                       'But only on Scraping or Rescrape Specific etc
                        titletext = titletext.Substring(4, titletext.Length - 4) & ", The"
                        x = True
                    End If
                    If titletext.ToLower.IndexOf("a ") = 0 Then
                        titletext = titletext.Substring(2, titletext.Length - 2) & ", A"
                        x = True
                    End If
                    If titletext.ToLower.IndexOf("an ") = 0 Then
                        titletext = titletext.Substring(3, titletext.Length - 3) & ", An"
                        x = True
                    End If
                    If x Then _scrapedMovie.fullmoviebody.sortorder = titletext
                Else 
                    _scrapedMovie.fullmoviebody.sortorder = _scrapedMovie.fullmoviebody.title
                End If
            End If
        End If

        If Preferences.MovTitleCase Then
            _scrapedMovie.fullmoviebody.title = Utilities.TitleCase(_scrapedMovie.fullmoviebody.title)
            _scrapedMovie.fullmoviebody.sortorder = Utilities.TitleCase(_scrapedMovie.fullmoviebody.sortorder)
        End If

        If rl.TagsFromKeywords AndAlso (Not Preferences.movies_useXBMC_Scraper Or rl.FromTMDB) Then GetKeyWords(True)
        If Cancelled() Then Exit Sub

        If NeedTMDb(rl) OrElse (Preferences.movies_useXBMC_Scraper AndAlso rl.premiered) Then

            IniTmdb(_scrapedMovie.fullmoviebody.imdbid)
            tmdb.Imdb = If(_scrapedMovie.fullmoviebody.imdbid.Contains("tt"), _scrapedMovie.fullmoviebody.imdbid, "")
            tmdb.TmdbId = _scrapedMovie.fullmoviebody.tmdbid
            If rl.trailer Or rl.Download_Trailer Then
                If TrailerExists Then
                    ReportProgress("Trailer already exists ", "Trailer already exists - To download again, delete the existing one first i.e. this file : [" & ActualTrailerPath & "]" & vbCrLf)
                Else
                    _triedUrls.Clear()
                    GetTrailerUrlAlreadyRun = False
                    Dim more As Boolean = Not File.Exists(ActualTrailerPath)
                    While more
                        If Not rl.trailer AndAlso rl.Download_Trailer AndAlso _scrapedMovie.fullmoviebody.trailer = "" Then
                            ReportProgress("No Trailer URL Present", "Download of Trailer skipped as no url pre-scraped")
                            Exit While
                        End If
                        If rl.trailer Or _scrapedMovie.fullmoviebody.trailer = "" Then
                            If Cancelled() Then Exit Sub
                            _rescrapedMovie.fullmoviebody.trailer = GetTrailerUrl(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.imdbid)
                            UpdateProperty(_rescrapedMovie.fullmoviebody.trailer, _scrapedMovie.fullmoviebody.trailer)
                        Else
                            TrailerUrl = _scrapedMovie.fullmoviebody.trailer
                        End If
                        If Preferences.DownloadTrailerDuringScrape Or rl.Download_Trailer Then
                            DownloadTrailer(TrailerUrl, rl.Download_Trailer)
                            more = (TrailerUrl <> "") And Not TrailerDownloaded
                            If more Then _scrapedMovie.fullmoviebody.trailer = ""
                        Else
                            more = False
                        End If
                    End While
                End If
            End If
            If Cancelled() Then Exit Sub

            If rl.premiered AndAlso Preferences.movies_useXBMC_Scraper Then
                UpdateProperty(tmdb.releasedate, _scrapedMovie.fullmoviebody.premiered, True , rl.EmptyMainTags)
            End If

            If rl.TagsFromKeywords AndAlso (Preferences.movies_useXBMC_Scraper Or rl.FromTMDB) Then GetKeyWords(True, tmdb.Movie.id)

            If rl.Frodo_Poster_Thumbs Then GetFrodoPosterThumbs()
            If Cancelled() Then Exit Sub

            If rl.Frodo_Fanart_Thumbs Then GetFrodoFanartThumbs()
            If Cancelled() Then Exit Sub

            'Clears the existing poster urls and adds the rescraped ones directly into _scrapedMovie
            If rl.posterurls Then
                _scrapedMovie.listthumbs.Clear()
                GetPosterUrls()
            End If
            If Cancelled() Then Exit Sub

            If rl.missingposters Then DoDownloadPoster(rl.missingposters)
            If Cancelled() Then Exit Sub

            If rl.missingfanart Then DownloadFanart()
            If Cancelled() Then Exit Sub

            If rl.dlxtraart Then DownloadExtraFanart()
            If Cancelled() Then Exit Sub

            If rl.ArtFromFanartTv Then DownloadFromFanartTv(True)
            If Cancelled() Then Exit Sub

            If rl.tmdb_set_name OrElse rl.tmdb_set_id Then
                Try
                    _rescrapedMovie.fullmoviebody.movieset.MovieSetName = "-None-"
                    If Not IsNothing(tmdb.Movie.belongs_to_collection) Then
                        If rl.tmdb_set_name Then
                            _rescrapedMovie.fullmoviebody.movieset.MovieSetName = tmdb.Movie.belongs_to_collection.name
                        Else
                            _rescrapedMovie.fullmoviebody.movieset.MovieSetName = _scrapedMovie.fullmoviebody.movieset.MovieSetName 
                        End If
                        _rescrapedMovie.fullmoviebody.movieset.MovieSetId = tmdb.Movie.belongs_to_collection.id
                    Else
                        _rescrapedMovie.fullmoviebody.movieset.MovieSetName = _scrapedMovie.fullmoviebody.movieset.MovieSetName
                        _rescrapedMovie.fullmoviebody.movieset.MovieSetId = _scrapedMovie.fullmoviebody.movieset.MovieSetId 
                    End If
                    UpdateProperty(_rescrapedMovie.fullmoviebody.movieset, _scrapedMovie.fullmoviebody.movieset, , rl.EmptyMainTags)
                Catch
                End Try
            End If
            If Cancelled() Then Exit Sub

            If rl.missingmovsetart AndAlso _scrapedMovie.fullmoviebody.movieset.MovieSetId <> "" Then DoDownloadMovieSetArtwork()
            If Cancelled() Then Exit Sub

            If rl.actors Then
                _rescrapedMovie.listactors.Clear()
                If Preferences.TmdbActorsImdbScrape Then
                    _rescrapedMovie.listactors = GetTmdbActors
                Else
                    _rescrapedMovie.listactors = GetImdbActors()
                End If
                If _rescrapedMovie.listactors.Count > 0 Then
                    _scrapedMovie.listactors.Clear()
                    _scrapedMovie.listactors.AddRange(_rescrapedMovie.listactors)
                End If
            End If
            If _scrapedMovie.fullmoviebody.tmdbid = "" AndAlso tmdb.TmdbId <> "" Then _scrapedMovie.fullmoviebody.tmdbid = tmdb.TmdbId 
        End If
        If Cancelled() Then Exit Sub

        If rl.runtime_file Or rl.mediatags Or (rl.runtime And ((Preferences.movieRuntimeDisplay = "file") Or (Preferences.movieRuntimeFallbackToFile And _rescrapedMovie.fullmoviebody.runtime = ""))) Then
            If GetHdTags(_rescrapedMovie) Then
                UpdateProperty(_rescrapedMovie.filedetails, _scrapedMovie.filedetails)

                If rl.runtime_file Or (rl.runtime And ((Preferences.movieRuntimeDisplay = "file") Or (Preferences.movieRuntimeFallbackToFile And _rescrapedMovie.fullmoviebody.runtime = ""))) Then
                    AssignRuntime(_rescrapedMovie, rl.runtime_file)
                    UpdateProperty(_rescrapedMovie.fullmoviebody.runtime, _scrapedMovie.fullmoviebody.runtime)
                End If
            End If
        End If

        If rl.Convert_To_Frodo Then ConvertToFrodo()

        If rl.SetWatched       Then _scrapedMovie.fullmoviebody.SetWatched  ()
        If rl.ClearWatched     Then _scrapedMovie.fullmoviebody.ClearWatched()

        If rl.rebuildnfo Then Fixupnfo

        AssignMovieToCache()
        '		AssignMovieToAddMissingData
        HandleOfflineFile()             ' Do we need this?
        SaveNFO()

        If rl.Rename_Files Then
            ReportProgress(, RenameExistingMetaFiles)
        End If

        If rl.Rename_Folders Then ReportProgress(, RenameMovFolder)
        
        ReportProgress(, vbCrLf)
        UpdateCaches()
    End Sub

    Sub UpdateProperty(Of T) (ByVal fromField As T, ByRef toField As T,  Optional rescrape As Boolean=True, Optional ifempty As Boolean = False )  
        If Not rescrape         Then Exit Sub
        If IsNothing(fromField) Then Exit Sub
        If ifempty AndAlso Not String.IsNullOrEmpty(toField.ToString) Then Exit Sub
        toField = fromField
    End Sub    

    Sub UpdateCaches
        UpdateActorCache
        UpdateDirectorCache
        UpdateMovieCache
        UpdateMovieSetCache 
    End Sub

    Sub RemoveMovieFromCaches
        RemoveActorsFromCache
        RemoveDirectorFromCache
        RemoveMovieFromCache
    End Sub

    Sub UpdateActorCache
        RemoveActorsFromCache
        _parent.ActorDb.AddRange(Actors)
    End Sub

    Sub UpdateDirectorCache
        RemoveDirectorFromCache

        If Not IsNothing(Director) Then
            _parent.DirectorDb.Add(Director)
        End If
    End Sub

    Sub UpdateMovieSetCache
        If IsNothing(MovieSet) Then Exit Sub
        RemoveMovieSetFromCache

        If MovieSet.MovieSetName <> "" Or MovieSet.MovieSetName.ToLower <> "-none-" Then
            _parent.MovieSetDB.Add(MovieSet)
        End If
    End Sub

    Sub RemoveActorsFromCache
        If Actors.Count = 0 Then Exit Sub
        RemoveActorsFromCache(Actors(0).MovieId)
    End Sub

    Sub RemoveActorsFromCache(MovieId)
        _parent.ActorDb.RemoveAll(Function(c) c.MovieId = MovieId)
    End Sub


    Sub RemoveDirectorFromCache
        If IsNothing(Director) Then Exit Sub
        RemoveDirectorFromCache(Director.MovieId)
    End Sub


    Sub RemoveDirectorFromCache(MovieId)
        _parent.DirectorDb.RemoveAll(Function(c) c.MovieId = MovieId)
    End Sub

    Sub RemoveMovieSetFromCache
        'If MovieSet.count = 0 Then Exit Sub
        '_parent.MovieSetDB.Remove(MovieSet)

        If IsNothing(MovieSet) Then Exit Sub
        RemoveMovieSetFromCache(MovieSet.MovieSetId)
    End Sub

    Sub RemoveMovieSetFromCache(MovieSetID)
        _parent.MovieSetDB.RemoveAll(Function(c) c.MovieSetId = MovieSetID)
    End Sub

    Sub UpdateActorCacheFromEmpty
        If Actors.Count = 0 Then Exit Sub
        Try
            _parent._tmpActorDb.AddRange(Actors)
        Catch
        End Try
    End Sub

    Sub UpdateDirectorCacheFromEmpty
        If IsNothing(Director) Then Exit Sub
        _parent._tmpDirectorDb.Add(Director)
    End Sub

    Sub UpdateMovieSetCacheFromEmpty
        If IsNothing(MovieSet) Then Exit Sub
        '_parent._tmpMoviesetDb.Clear()
        _parent._tmpMoviesetDb.Add(MovieSet)
    End Sub

    Sub UpdateMovieCache
        Dim key=_movieCache.fullpathandfilename
        Dim c As ComboList = Nothing
        
        Try
            c = _parent.FindCachedMovie(key)
        Catch ex As Exception
        End Try
        
        If IsNothing(c) Then
            key = ActualNfoPathAndFilename
            Try
                c = _parent.FindCachedMovie(key)
            Catch
            End Try
        End If

        If Not IsNothing(c) Then
            c.Assign(_movieCache)
            Try
            Dim dgv_c As Data_GridViewMovie = _parent.FindData_GridViewCachedMovie(key)
            
            dgv_c.Assign(_movieCache)
            Catch
            End Try
            Return
        End If
        
        RemoveMovieFromCache

        _parent.MovieCache.Add(_movieCache)
        _parent.Data_GridViewMovieCache.Add(New Data_GridViewMovie(_movieCache))
    End Sub
 
    Sub RemoveMovieFromCache
        RemoveMovieFromCache(_movieCache.fullpathandfilename)
    End Sub
 
    Sub RemoveMovieFromCache(fullpathandfilename)
        If fullpathandfilename = "" Then Exit Sub

        Dim key=fullpathandfilename

        If _parent.MovieCache.RemoveAll(Function(c) c.fullpathandfilename = key) = 0 Then
            key = ActualNfoPathAndFilename
            _parent.MovieCache.RemoveAll(Function(c) c.fullpathandfilename = key)
        End If

        _parent.Data_GridViewMovieCache.RemoveAll(Function(c) c.fullpathandfilename = key)
    End Sub
  
    Shared Function SaveFanartImageToCacheAndPaths(url As String, paths As List(Of String))

        If Not Preferences.savefanart Then Return False

        Dim point = Movie.GetBackDropResolution(Preferences.BackDropResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPaths(url, paths, Preferences.overwritethumbs, point.X, point.Y)
    End Function

    Shared Function SaveFanartImageToCacheAndPath(url As String, path As String)

        If Not Preferences.savefanart Then Return False

        Dim point = Movie.GetBackDropResolution(Preferences.BackDropResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPath(url, path, Preferences.overwritethumbs, point.X, point.Y)
    End Function

    Shared Function SaveActorImageToCacheAndPath(url As String, path As String)
    
        'If Not Preferences.actorsave Then Return False

        Dim height = GetHeightResolution(Preferences.ActorResolutionSI)

'        Return DownloadCache.SaveImageToCacheAndPath(url, path, Preferences.overwritethumbs, , height  )
        Return DownloadCache.SaveImageToCacheAndPath(url, path, True, , height  )
    End Function

    Shared Function SavePosterImageToCacheAndPath(url As String, path As String) As Boolean

        Dim height = GetHeightResolution(Preferences.PosterResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPath(url, path, Preferences.overwritethumbs, , height  )
    End Function

    Shared Function SavePosterImageToCacheAndPaths(url As String, paths As List(Of String)) As Boolean

        Dim Height = GetHeightResolution(Preferences.PosterResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPaths(url, paths, Preferences.overwritethumbs, , height)
    End Function

    Sub SavePosterToPosterWallCache
        If File.Exists(PosterPath) Then
            Try
                Dim bm As New MemoryStream(My.Computer.FileSystem.ReadAllBytes(PosterPath)) 'New Bitmap(PosterPath)
                Dim bm2 As New Bitmap(bm)
                bm.Dispose()
                bm2 = Utilities.ResizeImage(bm2, 150, 200)

                Utilities.SaveImage(bm2, PosterCachePath)

                bm2.Dispose()
            Catch
                'Invalid file
                Utilities.SafeDeleteFile(PosterPath     )
                Utilities.SafeDeleteFile(PosterCachePath)
            End Try
        End If
    End Sub

    'Sub LoadPosterFromPosterCache(picBox As PictureBox)

    '    If Not File.Exists(PosterCachePath) Then SavePosterToPosterWallCache

    '    picBox.Tag = Nothing

    '    If File.Exists(PosterCachePath) Then
    '        Try
    '            picBox.Image = Utilities.LoadImage(PosterCachePath)
    '            picBox.Tag = PosterPath
    '        Catch
    '            'Invalid file
    '            Utilities.SafeDeleteFile(PosterPath     )
    '            Utilities.SafeDeleteFile(PosterCachePath)
    '        End Try
    '    Else
    '        Try
    '            picBox.Image = Utilities.LoadImage(Utilities.DefaultPosterPath)
    '        Catch
    '        End Try
    '    End If

    'End Sub


    Function GetActorFileName( actorName As String) As String
        Return IO.Path.Combine(ActorPath, actorName.Replace(" ", "_") & ".tbn")
    End Function


    Public Function RenameExistingMetaFiles As String

        Dim log                 = ""
        'Test for unknown movies and abort renaming of moviefile.
        If _scrapedMovie.fullmoviebody.genre = "Problem" AndAlso _scrapedMovie.fullmoviebody.year = "0000" Then
            log &= "!!! Scrape Error movies should not be renamed. File renaming Aborted" & vbCrLf
            Return log
        End If

        Dim targetMovieFile     = ""
        Dim targetNfoFile       = "" 
        Dim oldName             = "" 
        Dim newName             = "" 
        Dim nextStackPart       = ""
        Dim stackdesignator     = ""
        Dim substackdesignator  = ""
        Dim newpath             = NfoPath
        Dim mediaFile           = mediapathandfilename
        Dim stackName           = mediaFile
        Dim isStack             = False
        Dim isSubStack          = False
        Dim isFirstPart         = True
        Dim isSubFirstPart      = True
        Dim newextension        = IO.Path.GetExtension(mediaFile)
        Dim newfilename         = UserDefinedBaseFileName
        Dim subName1 As String  = mediaFile.Substring(0, mediaFile.LastIndexOf(newextension))  'Replace(newextension,"")
        Dim subName As String   = subName1
        Dim subextn As List(Of String) = Utilities.ListSubtitleFilesExtensions(subName)
        Dim subStackList As New List(Of String)

        If newextension.ToLower = ".disc" Then
            log &= "!!! Media Stub files are not to be renamed." & vbCrLf
            Return log
        End If
        For Each rtfold In Preferences.offlinefolders
            If mediaFile.Contains(rtfold) Then 
                log &= "!!! Movie is in an offline folder.  We can not change offline movie's Filename!" & vbCrLf 
                Return log
            End If
        Next
        Dim movieStackList As New List(Of String)(New String() {mediaFile})
        
        Try
            If Not NfoPathAndFilename.ToLower.Contains("video_ts") AndAlso Not NfoPathAndFilename.ToLower.Contains("bdmv") AndAlso Not Preferences.basicsavemode Then
                targetMovieFile = newpath & newfilename
                targetNfoFile   = targetMovieFile


                '
                'PART 1 - Rename movie file(s):
                '

                '1.1 - Get movie file name(s):
                Do While Utilities.isMultiPartMedia(stackName, False, isFirstPart, stackdesignator, nextStackPart)
                    If isFirstPart Then
                        isStack = True    
                        Dim i As Integer
                        If Not Preferences.MovieRenameEnable Then
                            newfilename=stackName
                        End If
                        targetMovieFile = newpath & newfilename & stackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
                        If Preferences.namemode = "1" Then targetNfoFile = targetMovieFile
                    Else
                        movieStackList.Add(mediaFile)
                    End If
                    stackName = newpath & stackName & stackdesignator & nextStackPart & newextension
                    mediaFile = stackName
                Loop

                '1.2 - Get Multi-part subtitle(s):
                If subextn.Count > 0 Then    'If no subtitle file, then skip
                    For Each subex In subextn 
                        subName = subName1 & subex
                        subStackList.Add(subName)
                        nextStackPart = ""
                        isSubStack = False
                        Dim Stackpart As Integer = 0
                        Dim newStackPart As String = ""
                        Do While Utilities.isMultiPartMedia(subName, isSubStack, isSubFirstPart, substackdesignator, nextStackPart)
                            If isSubFirstPart Then
                                mediaFile = subName1 & subex
                                subStackList.Remove(mediaFile)
                                newStackPart = nextStackPart 
                                isSubStack = True                  'this media file has already been added to the list, but check for existing file with new name
                                'Dim i As Integer                  'sacrificial variable to appease the TryParseosaurus Checks
                                'targetMovieFile = newpath & newfilename & substackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
                            Else
                                Stackpart = nextStackPart.ToInt - 1
                                newStackPart = Stackpart.ToString
                                'subStackList.Add(mediaFile)
                            End If
                            Dim i As Integer                  'sacrificial variable to appease the TryParseosaurus Checks
                            targetMovieFile = newpath & newfilename & substackdesignator & (If(isSubFirstPart, If(Integer.TryParse(newStackPart, i), "1".PadLeft(newStackPart.Length, "0"), "A"), newStackPart))
                            RenameFile(mediaFile, (targetMovieFile & subex), log)
                            subName = newpath & subName & stackdesignator & nextStackPart & subex
                            mediaFile = subName
                            If Not File.Exists(subName) Then Exit Do
                        Loop
                    Next
                End If

                'Part 2.1 - Rename movie file name(s):
                movieStackList.Sort()
                For i = 0 To movieStackList.Count - 1
                    Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), newextension)
                    oldName = movieStackList(i)
                    newName = newpath & changename
                    RenameFile(oldName, newName, log)
                Next
                    log &= "!!! Movie Renamed as:- " & newfilename & vbCrLf 

                'Part 2.2 - Rename Subtitle file name(s):
                subStackList.Sort()
                For i = 0 To subStackList.Count - 1
                    Dim oldname1 = subStackList(i)
                    If Utilities.isMultiPartMedia(oldname1, False) Then
                        Utilities.isMultiPartMedia(oldname1, True)
                    'Else

                    End If
                    Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), subextn(i))
                    'Dim changename As String = subStackList(i).Replace(oldname1, newfilename)
                    oldName = subStackList(i)
                    newName = newpath & changename
                    RenameFile(oldName, newName, log)
                Next
                    log &= "!!! Subtitle Renamed as:- " & newfilename & vbCrLf 

                '
                'PART 3 - Rename anciliary file(s) (nfo,poster,fanart & trailer):
                '
                For Each anciliaryFile As String In Utilities.acceptedAnciliaryExts

                    newName = targetNfoFile & anciliaryFile

                    oldName = GetActualName(anciliaryFile)

                    If oldName<>"unknown" AndAlso anciliaryFile=".nfo" AndAlso oldName <> newName Then
                        RemoveMovieFromCache
                    End If

                    If RenameFile(oldName, newName, log) And anciliaryFile=".nfo" Then
                        _movieCache.fullpathandfilename = newName
                        _movieCache.filename            = Path.GetFileName(newName)
                        _movieCache.foldername          = Utilities.GetLastFolder(newName)
                        UpdateMovieCache
                    End If
                 Next

                mediapathandfilename = targetMovieFile & newextension

                RenamedBaseName = targetNfoFile
            Else
                log & = "!!! " & _scrapedMovie.fullmoviebody.originaltitle & vbCrLf 
                log &= "!!! Rename bypassed as either Use Foldernames, Basic Save" & vbCrLf 
                log &= "!!! selected, or files in DVD or Bluray folders" & vbCrLf 
            End If
            
            log &= vbCrLf 
        Catch ex As Exception
            log &= "!!!Rename Movie File FAILED !!!" & vbCrLf & vbCrLf
        End Try
        Return log
    End Function

    Public Function RenameMovFolder As String
        Dim log As String = ""

        'Test for unknown movies and abort renaming of movie folder.
        If _scrapedMovie.fullmoviebody.genre = "Problem" AndAlso _scrapedMovie.fullmoviebody.year = "0000" Then
            log &= "!!! Scrape Error movies should not be renamed. Folder Renaming Aborted" & vbCrLf
            Return log
        End If

        Dim NoDel As Boolean = False
        Dim FilePath As String = nfopath   'current path
        Dim currentroot As String = ""
        Dim stackname As String = mediapathandfilename
        Dim newextension    = IO.Path.GetExtension(mediapathandfilename)
        Dim isStack         = False
        Dim isSubStack      = False
        Dim isFirstPart     = True
        Dim nextStackPart   = ""
        Dim stackdesignator = ""
        Dim lastfolder As String = Utilities.GetLastFolderInPath(FilePath)

        
        'Get current root folder
        For Each rtfold In Preferences.offlinefolders
            If FilePath.Contains(rtfold) Then 
                log &= "!!! Movie is in an offline folder.  We can not change offline movie's folder!" & vbCrLf 
                Return log
            End If
        Next
        For Each rtfold In Preferences.movieFolders
            If FilePath.Contains(rtfold.rpath) Then currentroot = rtfold.rpath
        Next
        Dim inrootfolder As Boolean = ((currentroot & "\") = FilePath)
        Dim newFolder As String = UserDefinedBaseFolderName
        Dim newpatharr As New List(Of String)
        newpatharr.AddRange(newFolder.Split("\"))
        
        'Remove -none- if no Movieset
        If newpatharr.Count > 0 Then
            Dim badfolder As Integer = 0
            Do Until badfolder = -1
                badfolder = -1
                For num = 0 to newpatharr.Count-1
                    If newpatharr(num).ToLower = "-none-" Then 
                        badfolder = num
                        Exit For
                    End If
                Next
                If badfolder >= 0 Then
                    newpatharr.RemoveAt(badfolder)
                End If
            Loop
        Else If newpatharr.Count = 0 Then
            log &= "!!!No Folder string set in Preferences" & vbCrLf 
            Return log
        End If
        
        'Check if new path already exists and if not, Create new directory/s
        FilePath = FilePath.Replace("VIDEO_TS\","")             'If DVD VIDEO_TS folder, step back one folder so we copy folder as well.
        FilePath = FilePath.Replace("BDMV\","")             'If BD BDMV folder, step back one folder so we copy folder as well.
        Dim checkfolder As String = currentroot
        If newpatharr.Count = 1 And Not inrootfolder Then                       'If only one folder in new folder pattern,
            If Preferences.MovNewFolderInRootFolder Then
                checkfolder &= "\" & newpatharr(0)
            Else
                If lastfolder.ToLower.Contains("video_ts") OrElse lastfolder.ToLower.Contains("bdmv") Then
                    lastfolder = Utilities.GetLastFolderInPath(FilePath)  'Create in current directory, excluding if
                End If
                checkfolder = FilePath.Replace((lastfolder & "\"), newpatharr(0))   'movie is in root folder already
            End If
            
        Else
            For Each folder In newpatharr
                checkfolder &= "\" & folder
            Next
        End If

        If Not Directory.Exists(checkfolder) Then
                Directory.CreateDirectory(checkfolder)
                log &= "!!! New path created:- " & checkfolder & vbCrLf 
        Else
            Dim chkfldr As String = checkfolder & "\"
            If chkfldr.ToLower = FilePath.ToLower Then
                log &= "!!! Movie already exists in : " & checkfolder & vbCrLf 
                log &= "!!! Rename of this Movie folder skipped" & vbCrLf & vbcrlf
                Return log
            Else
                log &= "!!! Path for: " & checkfolder & vbCrLf 
                log &= "!!! already Exists" & vbCrLf & vbcrlf
                Dim filename As String = stackname 
                Utilities.isMultiPartMedia(filename, False, isFirstPart, stackdesignator, nextStackPart)
                Dim moviename = chkfldr & filename & newextension 
                If IO.File.Exists(moviename) Then
                    log &= "!!! Movie of same filename already exists in: " & checkfolder & vbCrLf 
                    log &= "!!! Aborting Movie move into existing folder!" & vbCrLf & vbcrlf
                    Return log
                End If
            End If
        End If
        
        RemoveMovieFromCache         'Due to path changes, remove from Cache beforehand.

        'If not in root, move files to new path and any sub folders
        If Not inrootfolder Then
            Dim toPathInfo = New DirectoryInfo(checkfolder)
            Dim fromPathInfo = New DirectoryInfo(FilePath)
            For Each file As IO.FileInfo In fromPathInfo.GetFiles()             ''move all files
                file.MoveTo(Path.Combine(checkfolder, file.Name))
            Next
            For Each dir As DirectoryInfo In fromPathInfo.GetDirectories()      ''move any sub directories
                dir.MoveTo(Path.Combine(checkfolder, dir.Name))
            Next
            If Utilities.IsDirectoryEmpty(FilePath) Then
                Try
                    IO.Directory.Delete(FilePath)
                Catch ex As IOException 
                    log &= "!!! Could not delete original folder:- " & FilePath & vbCrLf 
                    NoDel = True
                End Try
            End If
            log &= "!!! All files/Folders moved to new path" & vbCrLf 
        Else
            'Else if in Root folder, moved to new folder movie and ancillary files.
            Dim Moviename As String = ""
            If Utilities.isMultiPartMedia(stackname, False, isFirstPart, stackdesignator, nextStackPart) Then
                Moviename = stackname   'if multipart then returns excluding stackdesignator
            Else
                Moviename = stackname   
            End If
            Dim di As DirectoryInfo = New DirectoryInfo((currentroot & "\"))
            For Each fi As IO.FileInfo In di.GetFiles((Moviename & "*"))
                fi.MoveTo(Path.Combine(checkfolder, fi.Name))
            Next
            log &= "Movie moved from Root folder into new path" & vbCrLf 
            
            'Copy actor images from root .actor folder to new folder's .actor folder
            Dim actorsource As String= currentroot & "\.actors\"
            If IO.Directory.Exists(actorsource) Then
                Dim NewActorFolder As String = checkfolder & "\.actors"
                If Not Directory.Exists(NewActorFolder) Then Directory.CreateDirectory(NewActorFolder)
                NewActorFolder &= "\"
                For Each act In Actors
                    Dim actorfilename As String = act.ActorName.Replace(" ","_") & ".tbn"
                    Dim sourceactor As String = actorsource & actorfilename
                    If File.Exists(sourceactor) Then
                        Utilities.SafeCopyFile(sourceactor, (NewActorFolder & actorfilename), True)
                    End If
                    Dim Frodoactorsource As String = sourceactor.Replace(".tbn",".jpg")
                    actorfilename = actorfilename.Replace(".tbn",".jpg")
                    If File.Exists(Frodoactorsource)
                        Utilities.SafeCopyFile(Frodoactorsource, (NewActorFolder & actorfilename), True)
                    End If
                Next
                log &= "Actors copied into new Movie's actor folder" & vbCrLf
            End If
        End If

        ' tidyup empty remaining folder
        Dim oldpath As New List(Of String)
        oldpath.AddRange(FilePath.Split("\"))
        Dim opcount As Integer = oldpath.Count -1
        If oldpath(opcount) = "" Then
            oldpath.RemoveAt(opcount)
            opcount = opcount -1
        End If
        Dim testpath As String = Filepath
        For num = opcount to 0 Step -1
            testpath = testpath.Replace((oldpath(num).ToString & "\"),"")
            If testpath = (currentroot & "\") Then Exit For
            If IO.Directory.Exists(testpath) Then
                Dim isitempty as DirectoryInfo = New DirectoryInfo(testpath)
                If Not (isitempty.EnumerateFiles().Any()) And Not (isitempty.EnumerateDirectories().Any()) Then
                    IO.Directory.Delete(testpath)
                End If
            End If
        Next


        'update cache info
        _movieCache.fullpathandfilename = checkfolder & "\" & NfoPathAndFilename.Replace(FilePath,"")
        _movieCache.foldername = Utilities.GetLastFolder(_movieCache.fullpathandfilename)
        mediapathandfilename = _movieCache.fullpathandfilename
        RenamedBaseName = mediapathandfilename
        UpdateMovieCache
        log &= "!!! Folder structure created successfully" & vbCrLf &vbCrLf 

        Try
            If NoDel Then
                IO.Directory.Delete(FilePath)
            End If
        Catch ex As Exception
            log &= "!!! Could not Repeat delete original folder:- " & FilePath & vbCrLf
        End Try

        Return log
  End Function

    Public Function GetActualName(anciliaryFile As String) As String
      
        Dim stackName       As String = mediapathandfilename
        Dim stackDesignator As String = ""
        Dim testName        As String = "" 

        'Get stack name
        Utilities.isMultiPartMedia(stackName, False, , stackDesignator)

        testName = mediapathandfilename.Substring(0, mediapathandfilename.LastIndexOf(Extension)) & anciliaryFile  'Replace(IO.Path.GetExtension(mediapathandfilename), anciliaryFile)
        If File.Exists(testName) Then Return testName
 
        testName = NfoPathAndFilename.Replace(".nfo", anciliaryFile)
        If File.Exists(testName) Then Return testName

        'If stack part name in old name, try removing it...
        If testName.Length-(stackDesignator.Length+1+anciliaryFile.Length) = testName.IndexOf(stackDesignator) then
            testName = testName.Substring(0,testName.Length-(stackDesignator.Length+1+anciliaryFile.Length))+anciliaryFile
            If File.Exists(testName) Then Return testName
        End If

        If stackName<>mediapathandfilename Then
            testName = NfoPath & stackName & anciliaryFile
            If File.Exists(testName) Then Return testName
        End If

        Return "unknown"
    End Function


    ReadOnly Property UserDefinedBaseFileName As String
        Get
            Dim s As String = Path.GetFileNameWithoutExtension(NfoPathAndFilename)
            Dim ac1 As String = AudioCodecChannels.ToLower
            Dim ach As String = AudioChannels.ToLower
            Dim vc As String = VideoCodec.ToLower
            Dim vr As String = VideoResolution.ToLower
            
            Try
                If Preferences.MovieRenameEnable Or Preferences.MovieManualRename Then
                    s = Preferences.MovieRenameTemplate
                    s = s.Replace("%T", If(Preferences.MovTitleIgnArticle, Preferences.RemoveIgnoredArticles(_scrapedMovie.fullmoviebody.title.SafeTrim), _scrapedMovie.fullmoviebody.title.SafeTrim))
                    s = s.Replace("%Z", If(Preferences.MovSortIgnArticle, Preferences.RemoveIgnoredArticles(_scrapedMovie.fullmoviebody.sortorder.SafeTrim), _scrapedMovie.fullmoviebody.sortorder.SafeTrim))
                    s = s.Replace("%Y", _scrapedMovie.fullmoviebody.year)          
                    s = s.Replace("%I", _scrapedMovie.fullmoviebody.imdbid)        
                    s = s.Replace("%P", _scrapedMovie.fullmoviebody.premiered)     
                    s = s.Replace("%R", _scrapedMovie.fullmoviebody.rating)
                    s = s.Replace("%M", _scrapedMovie.fullmoviebody.mpaa)
                    s = s.Replace("%V", vr)        
                    s = s.Replace("%A", ac1)
                    s = s.Replace("%O", ach)
                    s = s.Replace("%C", vc)
                    s = s.Replace("%L", _scrapedMovie.fullmoviebody.runtime)       
                    s = s.Replace("%S", _scrapedMovie.fullmoviebody.source) 
                    s = Utilities.cleanFilenameIllegalChars(s)
                    If Preferences.MovRenameSpaceCharacter Then
                        s = Utilities.SpacesToCharacter(s, Preferences.RenameSpaceCharacter)
                    End If
                End If
            Catch
            End Try

            Return s.Trim()
        End Get
    End Property

    ReadOnly Property UserDefinedBaseFolderName As String
        Get
            Dim s As String = NfoPath
            Dim ac1 As String = AudioCodecChannels.ToLower
            Dim ach As String = AudioChannels.ToLower
            Dim vc As String = VideoCodec.ToLower
            Dim vr As String = VideoResolution.ToLower
            Dim vgenre As String = _scrapedMovie.fullmoviebody.genre.ToString
            If vgenre.IndexOf("/") <> -1 Then
                Dim vg () As String = vgenre.Split("/")
                vgenre = vg(0).Trim()
            End If
            
            Try
                If Preferences.MovFolderRename or Preferences.MovieManualRename Then
                    s = Preferences.MovFolderRenameTemplate
                    s = s.Replace("%T", If(Preferences.MovTitleIgnArticle, Preferences.RemoveIgnoredArticles(_scrapedMovie.fullmoviebody.title.SafeTrim), _scrapedMovie.fullmoviebody.title.SafeTrim))
                    s = s.Replace("%Z", If(Preferences.MovSortIgnArticle, Preferences.RemoveIgnoredArticles(_scrapedMovie.fullmoviebody.sortorder.SafeTrim), _scrapedMovie.fullmoviebody.sortorder.SafeTrim))
                    s = s.Replace("%Y", _scrapedMovie.fullmoviebody.year)          
                    s = s.Replace("%I", _scrapedMovie.fullmoviebody.imdbid)        
                    s = s.Replace("%P", _scrapedMovie.fullmoviebody.premiered)     
                    s = s.Replace("%R", _scrapedMovie.fullmoviebody.rating)
                    s = s.Replace("%M", _scrapedMovie.fullmoviebody.mpaa)
                    s = s.Replace("%G", vgenre)
                    s = s.Replace("%N", If(Preferences.MovSetIgnArticle, Preferences.RemoveIgnoredArticles(_scrapedMovie.fullmoviebody.movieset.MovieSetName), _scrapedMovie.fullmoviebody.movieset.MovieSetName))
                    s = s.Replace("%V", vr)        
                    s = s.Replace("%A", ac1)
                    s = s.Replace("%O", ach)
                    s = s.Replace("%C", vc)
                    s = s.Replace("%L", _scrapedMovie.fullmoviebody.runtime)       
                    s = s.Replace("%S", _scrapedMovie.fullmoviebody.source) 
                    s = Utilities.cleanFoldernameIllegalChars(s)
                    If Preferences.MovRenameSpaceCharacter Then
                        s = Utilities.SpacesToCharacter(s, Preferences.RenameSpaceCharacter)
                    End If
                End If
            Catch
            End Try

            Return s.Trim()
        End Get
    End Property

    ReadOnly Property AudioCodecChannels As String
        Get
            Dim ac As String = ""
            Try
                Dim ac1 As String = _scrapedMovie.filedetails.filedetails_audio.Item(0).Codec.Value
                Dim ac2 As String = _scrapedMovie.filedetails.filedetails_audio.Item(0).Channels.Value.RemoveWhitespace.Replace("/","-")
                If ac1.Contains("dts") Then ac1 = "DTS"
                
                ac = ac1 & " " & ac2 & "CH"
            Catch ex As Exception

            End Try
            Return ac
        End Get
    End Property

    ReadOnly Property AudioChannels As String
        Get
            Dim ach As String = ""
            Try
                Dim ach1 As String = _scrapedMovie.filedetails.filedetails_audio.Item(0).Channels.Value.RemoveWhitespace.Replace("/", "-")
                ach = ach1 & "CH"
            Catch ex As Exception

            End Try
            Return ach
        End Get
    End Property

    ReadOnly Property VideoCodec As String
        Get
            Dim vc As String = ""
            Try
                Dim vc1 = _scrapedMovie.filedetails.filedetails_video.Codec.value
                If Not String.IsNullOrEmpty(vc1) Then vc = vc1.ToUpper
            Catch ex As Exception

            End Try
            Return vc
        End Get
    End Property

    ReadOnly Property VideoResolution As String
        Get
            Dim vr As String
            Try
                Dim vr1 As String = If(_scrapedMovie.filedetails.filedetails_video.VideoResolution < 0, "", _scrapedMovie.filedetails.filedetails_video.VideoResolution.ToString)
                Dim vr2 As String = If(_scrapedMovie.filedetails.filedetails_video.ScanType.Value is Nothing, "", _scrapedMovie.filedetails.filedetails_video.ScanType.Value)
                If vr2.ToLower = "progressive" Then
                    vr2 = "P"
                Else If vr2.ToLower = "interlaced" or vr2.ToLower = "mbaff" or vr2.ToLower = "paff" Then
                    vr2 = "I"
                Else
                    If vr1 <> "" AndAlso vr1.ToInt > 700 Then
                        vr2 = "P"
                    Else
                        vr2 = ""
                    End If
                End If
                vr = vr1 & vr2
            Catch ex As Exception
                Return ""
            End Try
            Return vr
        End Get
    End Property

    Function RenameFile(oldName As String, newName As String, Optional ByRef log As String="") As Boolean

        If oldName<>newName AndAlso File.Exists(oldName) Then
            File.Move(oldName,newName)
            log &= "Renamed '" & Path.GetFileName(oldName) & "' to '" & Path.GetFileName(newName) & "'" & vbCrLf
            Return True
        End If

        Return False
    End Function


    Shared Function IsMCNfoFile( movieNfoFile As String )
        Try
            Dim filechck As StreamReader = File.OpenText(movieNfoFile)
            Dim s As String
            Do
                s = filechck.ReadLine

                If s = Nothing Then Exit Do

                If s.IndexOf("<movie>") <> -1 Then
                    Return True
                End If
            Loop Until filechck.EndOfStream

            filechck.Close
            filechck = Nothing

        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

        Return False
    End Function


    Shared Function GetMissingDataText(missingdata1 As Int32) As String

        Dim missingstring As String = ""
        If missingdata1 - 8 > -1 Then missingstring &= "Local Actor(s)" : missingdata1 = missingdata1 - 8
        If missingdata1 - 4 > -1 Then
            If Not missingstring = "" Then
                missingstring &= ", "
            End If
            missingstring &= "Trailer" : missingdata1 = missingdata1 - 4
        End If
        If missingdata1 - 2 > -1 Then
            If Not missingstring = "" Then
                missingstring &= ", "
            End If
            missingstring &= "Poster" : missingdata1 = missingdata1 - 2
        End If
        If missingdata1 - 1 > -1 Then
            If Not missingstring = "" Then
                missingstring &= ", "
            End If
            missingstring &= "Fanart" : missingdata1 = missingdata1 - 1
        End If
        If missingdata1 = 0 Then
            If missingstring = "" Then
                missingstring &= "None"
            End If
            Return missingstring
        Else
            Return "Error in GetMissingDataText - Passed : [" & missingdata1 & "]"
        End If
        'If missingdata1 = 0 Then Return "None"
        'If missingdata1 = 1 Then Return "Fanart"
        'If missingdata1 = 2 Then Return "Poster"
        'If missingdata1 = 3 Then Return "Fanart & Poster"
        'If missingdata1 = 4 Then Return "Trailer"
        'If missingdata1 = 5 Then Return "Fanart & Trailer"
        'If missingdata1 = 6 Then Return "Poster & Trailer"
        'If missingdata1 = 7 Then Return "Fanart, Poster & Trailer"
        'If missingdata1 = 8 Then Return "Local Actor(s)"
        'If missingdata1 = 9 Then Return "Local Actor(s), Fanart"
        'If missingdata1 = 10 Then Return "Local Actor(s), Poster"
        'If missingdata1 = 11 Then Return "Local Actor(s), Fanart & Poster"
        'If missingdata1 = 12 Then Return "Trailer & Local Actor(s)"

        'Return "Error in GetMissingDataText - Passed : [" & missingdata1 & "]"

    End Function


    Function GetYouTubeIds As List(Of String)

        Dim Results As List(Of String) = New List(Of String)

        Dim url = "http://www.youtube.com/results?search_query=" + _scrapedMovie.fullmoviebody.title + "+" + _scrapedMovie.fullmoviebody.year + "+trailer&filters=short&lclk=short"
        
        Dim RegExPattern = "href=""/watch[?]v=(?<id>.*?)"""

        Dim s As New Classimdb

        Dim html As String = s.loadwebpage(Preferences.proxysettings, url,True,10).ToString

        For Each m As Match In Regex.Matches(html, RegExPattern, RegexOptions.Singleline) 

            Dim id As String = Net.WebUtility.HtmlDecode(m.Groups("id").Value)

            If Not Results.Contains(id) Then
                Results.Add(id)
            End If
        Next 

        Return Results  
    End Function

    Sub ConvertToFrodo

        If Not PreFrodoPosterExists Then Return

        Try
            If FrodoPosterExists
                Dim s = ActualPosterPath

                If IO.Path.GetExtension(s).ToUpper=".TBN" Then Utilities.SafeDeleteFile(s)
            Else 
                IO.File.Move(ActualPosterPath,NfoPathPrefName.Substring(0, NfoPathPrefName.LastIndexOf(IO.Path.GetExtension(NfoPathPrefName))) & "-poster.jpg")
            End If 
        Catch ex As Exception
            Dim x = ex
        End Try
    End Sub

    Sub GetKeyWords(Optional ByVal Force As Boolean = False, Optional ByVal tmdbid As String = "")
        If (Force Or Preferences.keywordasTag) AndAlso Preferences.keywordlimit > 0 Then
            Dim keywords As New List(Of String)
            If Preferences.movies_useXBMC_Scraper Then
                If tmdbid <> "" Then
                    keywords =  tmdb.Keywords  '_imdbScraper.GetTmdbkeywords(tmdbid , Preferences.keywordlimit) 'if given by rescrape specific
                Else
                    keywords = _imdbScraper.GetTmdbkeywords(_possibleImdb , Preferences.keywordlimit) 'if during initial movie scrape
                End If
                
            Else
                keywords = _imdbScraper.GetImdbKeyWords(Preferences.keywordlimit, Preferences.imdbmirror, _scrapedMovie.fullmoviebody.imdbid)
            End If
            If keywords.Count > 0 AndAlso keywords.Count > Preferences.keywordlimit Then
                _scrapedMovie.fullmoviebody.tag.Clear()
                Dim i As Integer = 0
                For Each wd In keywords
                    i = i + 1
                    _scrapedMovie.fullmoviebody.tag.Add(wd)
                    If i = Preferences.keywordlimit Then Exit For
                Next
            ElseIf keywords.Count > 0
                _scrapedMovie.fullmoviebody.tag = keywords
            End If
        End If
    End Sub

    Sub Fixupnfo()
        'If XBMC networkpath changed, update actor thumb path
        Dim listactors2 As New List(Of str_MovieActors )
        For Each movactor In _scrapedMovie.listactors
            If Preferences.actorsave AndAlso movactor.actorid <> "" Then
                If Not String.IsNullOrEmpty(Preferences.actorsavepath) Then
                    Dim tempstring As String = Preferences.actorsavepath
                    Dim workingpath As String = ""
                    If Preferences.actorsavealpha Then
                        Dim actorfilename As String = movactor.actorname.Replace(" ", "_") & "_" & movactor.actorid & ".jpg"
                        tempstring = tempstring & "\" & actorfilename.Substring(0,1) & "\"
                        workingpath = tempstring & actorfilename 
                    Else
                        tempstring = tempstring & "\" & movactor.actorid.Substring(movactor.actorid.Length - 2, 2) & "\"
                        workingpath = tempstring & movactor.actorid & ".jpg"
                    End If
                    If Not String.IsNullOrEmpty(Preferences.actornetworkpath) Then
                        If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                            movactor.actorthumb = workingpath.Replace(Preferences.actorsavepath, Preferences.actornetworkpath).Replace("\", "/")
                        Else
                            movactor.actorthumb = workingpath.Replace(Preferences.actorsavepath, Preferences.actornetworkpath).Replace("/", "\")
                        End If
                    End If
                End If
            End If
            listactors2.Add(movactor)
        Next
        _scrapedMovie.listactors.Clear()
        _scrapedMovie.listactors.AddRange(listactors2)
    End Sub           'Any code in here to fix up movie nfo's

End Class
