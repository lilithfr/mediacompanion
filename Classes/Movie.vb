Imports System.ComponentModel 
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
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

                _nfoPathAndFilename = mediapathandfilename.Replace(Extension, ".nfo")

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
                Dim BaseName = mediapathandfilename.Replace(Extension,"")

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
            Return NfoPath_NoDirectorySeparatorChar & IO.Path.DirectorySeparatorChar
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
            Dim q = From actor In _scrapedMovie.listactors Select New ActorDatabase(actor.actorname,_scrapedMovie.fullmoviebody.imdbid)

            Return q.ToList
        End Get
    End Property

    ReadOnly Property MissingLocalActors As Boolean
        Get
            For Each Actor In _scrapedMovie.listactors

                If Actor.actorthumb="" Then Continue For

                Dim ActorPath = GetActorPath(_movieCache.fullpathandfilename, actor.actorname)

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
                Dim rtfolder As Boolean = Preferences.GetRootFolderCheck(NfoPathAndFilename)
                If rtfolder Then
                    Return Path.GetFileNameWithoutExtension(TitleFull)
                Else
                Return Utilities.GetLastFolder(nfopathandfilename)
                End If
            Else
                Return Path.GetFileNameWithoutExtension(TitleFull) 
            End If
        End Get
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
            Return Utilities.CleanFileName(Title)
        End Get 
    End Property

    Public ReadOnly Property NfoPathPrefName As String
        Get
            If Rescrape Then Return ActualNfoPathAndFilename

            If Preferences.basicsavemode  Then
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

        If fileInfo.Extension = "ttt" Then
            Return False
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
        Actions.Items.Add( New ScrapeAction(AddressOf DoRename                    , "Rename"                    ) )
        Actions.Items.Add( New ScrapeAction(AddressOf ImdbScrapeActors            , "IMDB Actors scraper"       ) ) 'GetImdbActors
        Actions.Items.Add( New ScrapeAction(AddressOf AssignTrailerUrl            , "Get trailer URL"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoPosterThumbs        , "Getting extra Frodo Poster thumbs") )
        Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoFanartThumbs        , "Getting extra Frodo Fanart thumbs") )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignPosterUrls            , "Get poster URLs"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf TidyUpAnyUnscrapedFields    , "Tidy up unscraped fields"  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                     , "Save Nfo"                  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadPoster              , "Poster download"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadFanart              , "Fanart download"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadExtraFanart         , "Extra Fanart download"     ) )
'		Actions.Items.Add( New ScrapeAction(AddressOf AssignMovieToAddMissingData , "Assign missing data"       ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadTrailer             , "Trailer download"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignMovieToCache          , "Assigning movie to cache"  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf HandleOfflineFile           , "Handle offline file"       ) )
        Actions.Items.Add( New ScrapeAction(AddressOf UpdateCaches                , "Updating caches"           ) )
    End Sub
 
    Sub AppendScrapeFailedActions
        Actions.Items.Add( New ScrapeAction(AddressOf TidyUpAnyUnscrapedFields  , "Tidy up unscraped fields"          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                   , "Save Nfo"                          ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignUnknownMovieToCache , "Assign unknown new movie to cache" ) )
        Actions.Items.Add( New ScrapeAction(AddressOf UpdateCaches              , "Updating caches"                   ) )
    End Sub

    Sub Scrape(imdb As String)
        _possibleImdb = imdb
        Scrape
    End Sub

    Sub Scrape
        If Not Scraped then
            Scraped  = True
            Actions.Items.Add( New ScrapeAction(AddressOf IniTmdb             , "Initialising TMDb"              ) )
            Actions.Items.Add( New ScrapeAction(AddressOf getspecialMovie     , "Check if special version"       ) )
            Actions.Items.Add( New ScrapeAction(AddressOf ImdbScraper_GetBody , "Scrape IMDB Main body"          ) )
            Actions.Items.Add( New ScrapeAction(AddressOf CheckImdbBodyScrape , "Checking IMDB Main body scrape" ) )            
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
                ReportProgress(MSG_ERROR,"!!! Error - Running action [" & action.ActionName & "] threw [" & ex.Message.ToString & "]" & vbCrLf)                
                Actions.Items.Clear         
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
    End Sub

    Sub ImdbScraper_GetBody
        _imdbBody = ImdbScrapeBody(SearchName, PossibleYear, PossibleImdb)
    End Sub

    Sub ImdbScraper_GetBodyByImdbOnly
        _imdbBody = ImdbScrapeBody(, , PossibleImdb)
    End Sub


    Function ImdbScrapeBody(Optional Title As String=Nothing, Optional PossibleYear As String=Nothing, Optional PossibleImdb  As String=Nothing) As String

        If Not IsNothing(Title) then
            ReportProgress(, String.Format("!!! {0}!!! Scraping Title: {1}{0}", vbCrLf, Title))
        End If

        If PossibleImdb <> "" then
            ReportProgress( ,"Using IMDB : " & PossibleImdb & vbCrLf )
        End If

        ReportProgress( String.Format(" - Using '{0}{1}'", title, If(String.IsNullOrEmpty(PossibleYear), "", " " & PossibleYear)) & " " )

        ReportProgress( "- Main body " )
   
        Return _imdbScraper.getimdbbody(Title, PossibleYear, PossibleImdb, Preferences.imdbmirror, Preferences.googlecount)
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
            ReportProgress(MSG_ERROR,"!!! Unable to scrape body with refs """ & Title & """, """ & PossibleYear & """, """ & PossibleImdb & """, """ & Preferences.imdbmirror & """" & vbCrLf & "IMDB may not be available" & vbCrLf )
            AppendScrapeFailedActions
        Else
            ReportProgress(MSG_OK,"!!! Movie Body Scraped OK" & vbCrLf)
            AppendScrapeSuccessActions
        End If
    End Sub


    Sub TidyUpAnyUnscrapedFields
        If _scrapedMovie.fullmoviebody.title = Nothing or _scrapedMovie.fullmoviebody.title = "" Then
            _scrapedMovie.fullmoviebody.title = Title
            _scrapedMovie.fullmoviebody.plot  = "This movie could not be identified, or IMDB is un-available. To add the movie manually, either go to the movie edit page and select ""Change Movie"", then select the correct movie, or change movie scraper to XBMC-TMDB and Rescrape Movie."
            _scrapedMovie.fullmoviebody.genre = "Problem"
        End If

        If _scrapedMovie.fullmoviebody.year = Nothing Then
            _scrapedMovie.fullmoviebody.year = "0000"
        End If
        If _scrapedMovie.fullmoviebody.rating = Nothing Then
            _scrapedMovie.fullmoviebody.rating = "0"
        End If
        If _scrapedMovie.fullmoviebody.top250 = Nothing Then
            _scrapedMovie.fullmoviebody.top250 = "0"
        End If
        If _scrapedMovie.fullmoviebody.playcount = Nothing Then
            _scrapedMovie.fullmoviebody.playcount = "0"
        End If
        If _scrapedMovie.fullmoviebody.lastplayed = Nothing Then
            _scrapedMovie.fullmoviebody.lastplayed = ""
        End If

        If String.IsNullOrEmpty(_scrapedMovie.fileinfo.createdate) Then
            _scrapedMovie.fileinfo.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
        End If

    End Sub

    Sub LoadNFO(Optional bUpdateCaches As Boolean=True)
        _scrapedMovie = WorkingWithNfoFiles.mov_NfoLoadFull(ActualNfoPathAndFilename)  'NfoPathPrefName
        _nfoPathAndFilename=ActualNfoPathAndFilename
        Scraped=True
        Try
            AssignMovieToCache
            If bUpdateCaches Then UpdateCaches
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

        WorkingWithNfoFiles.mov_NfoSave(Nfo, fmd, True)

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
        _movieCache.fullpathandfilename = NfoPathPrefName
        _movieCache.MovieSet            = _scrapedMovie.fullmoviebody.movieset
        _movieCache.source              = _scrapedMovie.fullmoviebody.source
        _movieCache.filename            = Path.GetFileName(nfopathandfilename)
        
        If movRebuildCaches Then ME.UpdateActorCacheFromEmpty()
        
        If Not Preferences.usefoldernames Then
            If _movieCache.filename <> Nothing Then _movieCache.filename = _movieCache.filename.Replace(".nfo", "")
        End If    

        _movieCache.foldername          = Utilities.GetLastFolder(nfopathandfilename)
        _movieCache.title               = _scrapedMovie.fullmoviebody.title
        _movieCache.originaltitle       = _scrapedMovie.fullmoviebody.originaltitle
        _movieCache.sortorder           = _scrapedMovie.fullmoviebody.sortorder
        _movieCache.runtime             = _scrapedMovie.fullmoviebody.runtime
        _movieCache.Votes               = _scrapedMovie.fullmoviebody.votes.ToInt
        _movieCache.outline             = _scrapedMovie.fullmoviebody.outline
        _movieCache.plot                = _scrapedMovie.fullmoviebody.plot
        _movieCache.year                = _scrapedMovie.fullmoviebody.year.ToInt
        _movieCache.Resolution          = _scrapedMovie.filedetails.filedetails_video.VideoResolution
        _movieCache.AssignAudio(_scrapedMovie.filedetails.filedetails_audio)
        _movieCache.Premiered           = _scrapedMovie.fullmoviebody.premiered
        _movieCache.movietag = _scrapedMovie.fullmoviebody.tag
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
        _movieCache.rating      = _scrapedMovie.fullmoviebody.rating.ToRating
        _movieCache.top250      = _scrapedMovie.fullmoviebody.top250
        _movieCache.genre       = _scrapedMovie.fullmoviebody.genre
        _movieCache.playcount   = _scrapedMovie.fullmoviebody.playcount
        _movieCache.lastplayed  = _scrapedMovie.fullmoviebody.lastplayed 
        _movieCache.Certificate = _scrapedMovie.fullmoviebody.mpaa
        _movieCache.movietag = _scrapedMovie.fullmoviebody.tag
        AssignMovieToAddMissingData
    End Sub


    Sub AssignScrapedMovie
        AssignScrapedMovie(_scrapedMovie)
    End Sub
    
    Sub AssignScrapedMovie(_scrapedMovie As FullMovieDetails)
        Dim thumbstring As New XmlDocument

        thumbstring.LoadXml(ImdbBody)

        For Each thisresult In thumbstring("movie")
            Select Case thisresult.Name
                Case "title"
                    Dim sepmov As String = ""
                    If SeparateMovie <> "" Then
                        sepmov = " " & SeparateMovie
                    End If
                    _scrapedMovie.fullmoviebody.title = thisresult.InnerText.ToString.SafeTrim & sepmov
                Case "originaltitle"
                    _scrapedMovie.fullmoviebody.originaltitle = thisresult.InnerText.ToString.SafeTrim
                Case "alternativetitle"
                    _scrapedMovie.alternativetitles.Add(thisresult.InnerText)
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
                    For count = 0 To strarr.Length - 1
                        strarr(count) = strarr(count).Replace(" ", "")
                    Next
                    If strarr.Length <= Preferences.maxmoviegenre Then
                        _scrapedMovie.fullmoviebody.genre = thisresult.InnerText
                    Else
                        For g = 0 To Preferences.maxmoviegenre - 1
                            If g = 0 Then
                                _scrapedMovie.fullmoviebody.genre = strarr(g)
                            Else
                                _scrapedMovie.fullmoviebody.genre += " / " & strarr(g)
                            End If
                        Next
                    End If
                Case "mpaa"
                    _scrapedMovie.fullmoviebody.mpaa = thisresult.InnerText
                Case "outline"
                    _scrapedMovie.fullmoviebody.outline = thisresult.InnerText
                Case "plot"
                    _scrapedMovie.fullmoviebody.plot = thisresult.InnerText
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
                    _scrapedMovie.fullmoviebody.studio = thisresult.InnerText
                Case "tagline"
                    _scrapedMovie.fullmoviebody.tagline = thisresult.InnerText
                Case "top250"
                    _scrapedMovie.fullmoviebody.top250 = thisresult.InnerText
                Case "votes"
                    _scrapedMovie.fullmoviebody.votes = thisresult.InnerText
                Case "year"
                    _scrapedMovie.fullmoviebody.year = thisresult.InnerText
                Case "id"
                    _scrapedMovie.fullmoviebody.imdbid = thisresult.InnerText

                    If String.IsNullOrEmpty(_possibleImdb) Then
                        _possibleImdb = _scrapedMovie.fullmoviebody.imdbid
                    End If
                Case "cert"
                    _certificates.Add(thisresult.InnerText)
            End Select
        Next

        If Preferences.sorttitleignorearticle Then                              'add ignored articles to end of
            Dim titletext As String = _scrapedMovie.fullmoviebody.title         'sort title. Over-rides independent The or A settings.
            ''If Preferences.ignorearticle Then                                 'But only on Scraping or Rescrape Specific
            'If titletext.ToLower.IndexOf("the ") = 0 Then
            '    titletext = titletext.Substring(4, titletext.Length - 4) & ", The"
            'End If
            ''End If
            ''If Preferences.ignoreAarticle Then
            'If titletext.ToLower.IndexOf("a ") = 0 Then
            '    titletext = titletext.Substring(2, titletext.Length - 2) & ", A"
            'End If
            'If titletext.ToLower.IndexOf("an ") = 0 Then
            '    titletext = titletext.Substring(3, titletext.Length - 3) & ", An"
            'End If
            ''End If
            _scrapedMovie.fullmoviebody.sortorder = Preferences.RemoveIgnoredArticles(titletext)
        Else
            _scrapedMovie.fullmoviebody.sortorder = _scrapedMovie.fullmoviebody.title               'Sort order defaults to title
        End If
        If _scrapedMovie.fullmoviebody.plot = "" Then _scrapedMovie.fullmoviebody.plot = _scrapedMovie.fullmoviebody.outline ' If plot is empty, use outline
        If _scrapedMovie.fullmoviebody.playcount = Nothing Then _scrapedMovie.fullmoviebody.playcount = "0"
        If _scrapedMovie.fullmoviebody.lastplayed = Nothing Then _scrapedMovie.fullmoviebody.lastplayed = ""
        If _scrapedMovie.fullmoviebody.top250 = Nothing Then _scrapedMovie.fullmoviebody.top250 = "0"

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


        If Rescrape Then
            _scrapedMovie.fullmoviebody.source = _previousCache.source
            _scrapedMovie.fullmoviebody.playcount = _previousCache.playcount
            _scrapedMovie.fullmoviebody.lastplayed = _previousCache.lastplayed 
            _scrapedMovie.fileinfo.createdate = _previousCache.createdate
            _scrapedMovie.fullmoviebody.movieset = _previousCache.MovieSet
        Else
            tmdb.Imdb = _possibleImdb
            _scrapedMovie.fullmoviebody.movieset = "-None-"
            If Preferences.GetMovieSetFromTMDb And Not IsNothing(tmdb.Movie.belongs_to_collection) Then
                _scrapedMovie.fullmoviebody.movieset = tmdb.Movie.belongs_to_collection.name
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

    Sub ImdbScrapeActors
        _scrapedMovie.listactors.Clear
        _scrapedMovie.listactors = GetImdbActors
    End Sub


    Function GetImdbActors

        ReportProgress("Actors")

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
        If Not Preferences.actorseasy Then ReportProgress(MSG_OK,"Actor images not set to download" & vbCrLf)

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
        

        If Preferences.moviePreferredTrailerResolution.ToUpper()<>"SD" And Not GetTrailerUrlAlreadyRun Then
            Try
                GetTrailerUrlAlreadyRun = True
                TrailerUrl = MC_Scraper_Get_HD_Trailer_URL(Preferences.moviePreferredTrailerResolution, title)
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


        'Try YouTube for HD and SD...
        '
        If TrailerUrl = "" and tmdb.Trailers.youtube.Count > 0 then

            Dim tryAgain = True

            While tryAgain
                TrailerUrl = tmdb.GetTrailerUrl(_triedUrls, Preferences.moviePreferredTrailerResolution)
                If TrailerUrl <> "" then
                    Try
                        Dim yts as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+TrailerUrl)

                        If yts.AvailableVideoFormat.Length>0 Then

                            _youTubeTrailer = yts.selectTrailer(Preferences.moviePreferredTrailerResolution)

                            If Not IsNothing(_youTubeTrailer) Then
                                TrailerUrl = _youTubeTrailer.VideoUrl
                                tryAgain = False
                            End If
                        Else
                            TrailerUrl = ""
                        End If
                    Catch       'Timed out...
                    End Try
                Else
                    tryAgain = False
                End If
            End While
        End If


        If TrailerUrl = "" Then
            Dim Ids As List(Of String) = GetYouTubeIds()

            For Each Id In Ids

                Dim yts as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+Id)

                If yts.AvailableVideoFormat.Length>0 Then
                    _youTubeTrailer = yts.selectTrailer(Preferences.moviePreferredTrailerResolution)

                    If Not IsNothing(_youTubeTrailer) Then
                        TrailerUrl = _youTubeTrailer.VideoUrl
                        Exit For
                    End If
                End If
            Next
        End If


        If TrailerUrl = "" And Not String.IsNullOrEmpty(_scrapedMovie.fullmoviebody.imdbid) Then
            TrailerUrl = _imdbScraper.gettrailerurl(imdb, Preferences.imdbmirror)
        End If

        If TrailerUrl = "" Then
            ReportProgress("-None found ","No trailer URL found" & vbCrLf)
        Else
            If Utilities.UrlIsValid(TrailerUrl) Then
                ReportProgress(MSG_OK,"Trailer URL Scraped OK" & vbCrLf)
            Else
                ReportProgress("-Invalid! ","!!! The Scraped Trailer URL is either invalid or not available [" & TrailerUrl & "]" & vbCrLf)
            End If
        End If

        Return TrailerUrl
    End Function


    Sub GetFrodoPosterThumbs
        _scrapedMovie.frodoPosterThumbs.Clear

        If Preferences.FrodoEnabled Then
            _scrapedMovie.frodoPosterThumbs.AddRange(tmdb.FrodoPosterThumbs)
            ReportProgress("Extra Frodo Poster thumbs: " & tmdb.FrodoPosterThumbs.count & " ", "Extra Frodo Poster thumbs: " & tmdb.FrodoPosterThumbs.count & vbCrLf)
        Else
            ReportProgress(,"Frodo extra URL scraping not selected" & vbCrLf)
        End If
    End Sub


    Sub GetFrodoFanartThumbs
        _scrapedMovie.frodoFanartThumbs.Thumbs.Clear

        If Preferences.FrodoEnabled Then
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

    Private Sub DeleteZeroLengthFile( fileName As String )
        If File.Exists(fileName) then
            If (New IO.FileInfo(fileName)).Length = 0 then
                File.Delete(fileName)
                ReportProgress("-Zero length trailer deleted ","Zero length trailer deleted : [" & fileName & "]")
            End If
        End If
    End sub

    Private Sub DownloadPoster
        If Not Preferences.scrapemovieposters then
            Exit Sub
        End If

        DoDownloadPoster
    End Sub
 
    Sub DoDownloadPoster(Optional ByVal batch As Boolean = False)
        Dim eden As Boolean = Preferences.EdenEnabled
        Dim frodo As Boolean = Preferences.FrodoEnabled
        If IO.Path.GetFileName(NfoPathPrefName).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(NfoPathPrefName).ToLower = "index.nfo" Then
            _videotsrootpath = Utilities.RootVideoTsFolder(NfoPathPrefName)
        End If
        Dim edenart As String = NfoPathPrefName.Replace(".nfo", ".tbn")
        Dim frodoart As String = edenart.Replace(".tbn", "-poster.jpg")
        If _videotsrootpath <> "" Then
            frodoart = _videotsrootpath + "poster.jpg"
        End If
        If Not Preferences.overwritethumbs Then
            If eden And File.Exists(edenart) And Not frodo Then
                ReportProgress(, "Eden Poster already exists -> Skipping" & vbCrLf)
                Exit Sub
            ElseIf frodo And File.Exists(frodoart) And Not eden Then
                ReportProgress(, "Frodo Poster already exists -> Skipping" & vbCrLf)
                Exit Sub
            ElseIf frodo And eden And File.Exists(edenart) And File.Exists(frodoart) Then
                ReportProgress(, "Frodo & Eden Posters already exists -> Skipping" & vbCrLf)
                Exit Sub
            End If
        End If

        If Not Rescrape Then DeletePoster()

        Dim validUrl = False

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


        If validUrl Then

            ReportProgress("Poster")

            Try
                Dim paths As List(Of String) = Preferences.GetPosterPaths(NfoPathPrefName, If(_videotsrootpath <> "", _videotsrootpath, ""))

                SavePosterImageToCacheAndPaths(PosterUrl, paths)
                SavePosterToPosterWallCache()
                ReportProgress(MSG_OK, "!!! Poster(s) scraped OK" & vbCrLf)


            Catch ex As Exception
                ReportProgress(MSG_ERROR, "!!! Problem Saving Poster" & vbCrLf & "!!! Error Returned :- " & ex.Message & vbCrLf & vbCrLf)
            End Try
        End If
    End Sub


    Sub DeleteScrapedFiles(Optional incTrailer As Boolean=False)
        Try
            LoadNFO
            DeleteActors     'remove actor images if present

            RemoveActorsFromCache(_scrapedMovie.fullmoviebody.imdbid        )
            RemoveMovieFromCache (_scrapedMovie.fileinfo.fullpathandfilename)

            
            DeletePoster
            DeleteFanart

            If incTrailer Then DeleteTrailer

            DeleteNFO
        Catch ex As Exception
            ReportProgress(MSG_ERROR,"!!! Problem deleting scraped files" & vbCrLf & "!!! Error Returned :- " & ex.Message & vbCrLf & vbCrLf) 
        End Try
    End Sub

   Sub DeleteActors
        Try
            'Only delete actors if movies are in separate folders
            Dim ap As String = ActorPath
            For Each act In Actors
                Dim actorfilename As String = GetActorFileName(act.ActorName)
                If File.Exists(actorfilename) Then
                    Utilities.SafeDeleteFile(actorfilename)
                End If
                If File.Exists(actorfilename.Replace(".tbn",".jpg"))
                    Utilities.SafeDeleteFile(actorfilename.Replace(".tbn",".jpg"))
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
        DeleteFile(PosterPath.Replace(Path.GetFileName(PosterPath),"folder.jpg"))
        DeleteFile(ActualPosterPath)
    End Sub


    Sub DeleteFanart
        DeleteFile(FanartPath)
        DeleteFile(ActualFanartPath)
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

    Sub DownloadFanart
        If Not Preferences.savefanart then
            ReportProgress(,"Fanart scraping not enabled" & vbCrLf)
            Exit Sub
        End If

        DoDownloadFanart

    End Sub

    Sub DoDownloadFanart
        Dim isfanartjpg As String = IO.Path.GetDirectoryName(NfoPathPrefName) & "\fanart.jpg
        Dim isMovieFanart As String = NfoPathPrefName.Replace(".nfo","-fanart.jpg")
        Dim frodoart As String =""
        If IO.Path.GetFileName(NfoPathPrefName).ToLower="video_ts.nfo" Or IO.Path.GetFileName(NfoPathPrefName).ToLower="index.nfo"Then
            _videotsrootpath = Utilities.RootVideoTsFolder(NfoPathPrefName)
            frodoart = _videotsrootpath+"fanart.jpg"
        End If

        If Not Preferences.overwritethumbs and (File.Exists(isMovieFanart) AndAlso File.Exists(isfanartjpg)) Then
            ReportProgress(,"Fanart already exists -> Skipping" & vbCrLf)
            Exit Sub
        End If

        Dim FanartUrl As String=tmdb.GetBackDropUrl

        If IsNothing(FanartUrl) then
            ReportProgress("-Not available ","Fanart not available for this movie on TMDb" & vbCrLf)
        Else
            ReportProgress("Fanart",)
            Try
                Dim paths As List(Of String) = Preferences.GetfanartPaths(NfoPathPrefName,If(_videotsrootpath<>"",_videotsrootpath,""))

                SaveFanartImageToCacheAndPaths(FanartUrl, paths)

                ReportProgress(MSG_OK,"!!! Fanart URL Scraped OK" & vbCrLf)
            Catch ex As Exception
                ReportProgress(MSG_ERROR,"!!! Problem Saving Fanart" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
            End Try
                    
        End If
    End Sub

    Sub DownloadExtraFanart
        If Preferences.dlxtrafanart AndAlso (Preferences.allfolders Or Preferences.usefoldernames) Then
            DoDownloadExtraFanart
        Else
            ReportProgress(,"Scraping Extra Fanart-Thumbs not selected" & vbCrLf)
            Exit Sub
        End If
    End Sub
    Sub DoDownloadExtraFanart
        Try
            Dim fcount As Integer = 0
            If Not Preferences.GetRootFolderCheck(ActualNfoPathAndFilename) Then
                Dim fanartarray As New List(Of str_ListOfPosters)
                Dim xfanart As String = Strings.Left(FanartPath, FanartPath.LastIndexOf("\")) & "\extrafanart\fanart"
                Dim xthumb As String = Strings.Left(FanartPath, FanartPath.LastIndexOf("\")) & "\extrathumbs\thumb"
                Dim xf As Boolean = Preferences.movxtrafanart
                Dim xt As Boolean = Preferences.movxtrathumb
                Dim owrite As Boolean = Preferences.overwritethumbs
                Dim tmpUrl As String = ""
                Dim xtraart As New List(Of String)
                fanartarray.Clear()
                fanartarray.AddRange(tmdb.Fanart)
                fcount = fanartarray.Count
                If fcount > 1 Then
                    Dim x As Integer = If(fcount > 4, 4, fcount)
                    For i = 1 To x
                        xtraart.Clear()
                        tmpUrl = fanartarray(i).hdUrl
                        If Utilities.UrlIsValid(tmpUrl) Then
                            If xf Then
                                If Not (IO.File.Exists((xfanart & i.ToString & ".jpg")) AndAlso Not owrite) Then
                                    xtraart.Add((xfanart & i.ToString & ".jpg"))
                                End If
                            End If
                            If xt Then
                                If Not (IO.File.Exists((xthumb & i.ToString & ".jpg")) AndAlso Not owrite) Then
                                    xtraart.Add((xthumb & i.ToString & ".jpg"))
                                End If
                            End If
                            If xtraart.Count > 0 Then
                                SaveFanartImageToCacheAndPaths(tmpUrl, xtraart)
                            End If
                        End If
                        If i = fanartarray.Count - 1 Then Exit For
                    Next
                End If
            Else
                ReportProgress(MSG_OK, "!!! Extra Fanart not downloaded as movie is in Root Folder." & vbCrLf)
                Exit Sub
            End If
            If fcount > 1 Then
                ReportProgress(MSG_OK, "!!! Extra Fanart Downloaded OK" & vbCrLf)
            Else
                ReportProgress(MSG_OK, "!!! Insufficient Fanart to download as Extrafanart" & vbCrLf)
            End If
        Catch ex As Exception
            ReportProgress(MSG_ERROR, "!!! Problem Saving Extra Fanart" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
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

        If Not File.Exists(FanartPath) Then
            missingdata += 1
        End If

        If Not File.Exists(PosterPath) Then
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
        Dim subName1 As String = mediafile.Replace(newextension,"")
        Dim subName As String = subName1
        Dim newfilename As String = UserDefinedBaseFileName
        Dim targetMovieFile As String = ""
        Dim targetNfoFile As String = ""
        Dim subextn As List(Of String) = Utilities.ListSubtitleFilesExtensions(subName)
        Dim subStackList As New List(Of String)'(New String() {subName})
        'If Not subextn = "" Then subStackList.Add((subName & subextn))

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
                        Utilities.isMultiPartMedia(oldname, True) ', isFirstPart, stackdesignator, nextStackPart)
                        'Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), subextn)
                        Dim changename As String = subStackList(i).Replace(oldname,newfilename)
                        File.Move(subStackList(i), changename)
                        logRename &= If(i, " and ", "") & changename
                    Next
                    log &= "Renamed Subtitle File to " & logRename & vbCrLf
                'Next

                    For Each anciliaryFile As String In Utilities.acceptedAnciliaryExts 'rename any anciliary files with the same name as the movie
                        If File.Exists(movieFileInfo.mediapathandfilename.Replace(newextension, anciliaryFile)) Then
                            File.Move(movieFileInfo.mediapathandfilename.Replace(newextension, anciliaryFile), targetMovieFile & anciliaryFile)
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
        Dim Log As String = ""
        Dim NoDel As Boolean = False
        Dim FilePath As String = movieFileInfo.nfopath   'current path
        Dim Filename As String = Path.GetFileNameWithoutExtension(movieFileInfo.NfoPathAndFilename)
        Dim currentroot As String = ""
         
        'Get current root folder
        For Each rtfold In Preferences.movieFolders
            If FilePath.Contains(rtfold) Then currentroot = rtfold
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
            Dim Moviename As String = Path.GetFileNameWithoutExtension(movieFileInfo.NfoPathAndFilename)
            Dim di As DirectoryInfo = New DirectoryInfo((currentroot & "\"))
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
        Return rl.trailer Or rl.Download_Trailer Or rl.posterurls Or rl.missingposters Or rl.missingfanart Or rl.tmdb_set_name Or
               rl.Frodo_Poster_Thumbs Or rl.Frodo_Fanart_Thumbs or rl.dlxtraart 
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

        If Cancelled() Then Exit Sub

        If RescrapeBody(rl) then  
            
            Scraped  = True

            _imdbBody = ImdbScrapeBody(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year, _scrapedMovie.fullmoviebody.imdbid)

            If _imdbBody = "MIC" Then                        
                ReportProgress(MSG_ERROR, "!!! - ERROR! - Rescrape IMDB body failed with refs """ & _scrapedMovie.fullmoviebody.title & """, """ & _scrapedMovie.fullmoviebody.year & """, """ & _scrapedMovie.fullmoviebody.imdbid & """, """ & Preferences.imdbmirror & """" & vbCrLf)
            Else
                ReportProgress(MSG_OK,"!!! Movie Body Scraped OK" & vbCrLf)
                AssignScrapedMovie(_rescrapedMovie)
            End If
        
            UpdateProperty( _rescrapedMovie.fullmoviebody.credits  , _scrapedMovie.fullmoviebody.credits  , rl.credits   )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.director , _scrapedMovie.fullmoviebody.director , rl.director  )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.stars    , _scrapedMovie.fullmoviebody.stars    , rl.stars     )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.genre    , _scrapedMovie.fullmoviebody.genre    , rl.genre     )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.mpaa     , _scrapedMovie.fullmoviebody.mpaa     , rl.mpaa      )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.plot     , _scrapedMovie.fullmoviebody.plot     , rl.plot      )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.premiered, _scrapedMovie.fullmoviebody.premiered, rl.premiered )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.rating   , _scrapedMovie.fullmoviebody.rating   , rl.rating    )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.runtime  , _scrapedMovie.fullmoviebody.runtime  , rl.runtime   )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.studio   , _scrapedMovie.fullmoviebody.studio   , rl.studio    )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.tagline  , _scrapedMovie.fullmoviebody.tagline  , rl.tagline   )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.outline  , _scrapedMovie.fullmoviebody.outline  , rl.outline   )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.top250   , _scrapedMovie.fullmoviebody.top250   , rl.top250    )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.votes    , _scrapedMovie.fullmoviebody.votes    , rl.votes     )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.country  , _scrapedMovie.fullmoviebody.country  , rl.country   )  
            UpdateProperty( _rescrapedMovie.fullmoviebody.year     , _scrapedMovie.fullmoviebody.year     , rl.year      )  
            UpdateProperty(_rescrapedMovie.fullmoviebody.title     , _scrapedMovie.fullmoviebody.title    , rl.title)

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
                    If x Then 
                        _scrapedMovie.fullmoviebody.sortorder = titletext
                    End If
                Else 
                    _scrapedMovie.fullmoviebody.sortorder = _scrapedMovie.fullmoviebody.title
                End If
            End If
        End If

        If Cancelled() Then Exit Sub

        If NeedTMDb(rl) Then

            IniTmdb(_scrapedMovie.fullmoviebody.imdbid)

            If rl.trailer Or rl.Download_Trailer Then
                If TrailerExists Then
                    ReportProgress("Trailer already exists ", "Trailer already exists - To download again, delete the existing one first i.e. this file : [" & ActualTrailerPath & "]" & vbCrLf)
                Else

                    _triedUrls.Clear()
                    GetTrailerUrlAlreadyRun = False

                    Dim more As Boolean = Not File.Exists(ActualTrailerPath)

                    While more
                        If rl.trailer Or _scrapedMovie.fullmoviebody.trailer = "" Then
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


            If rl.Frodo_Poster_Thumbs Then
                GetFrodoPosterThumbs()
            End If


            If rl.Frodo_Fanart_Thumbs Then
                GetFrodoFanartThumbs()
            End If


            'Clears the existing poster urls and adds the rescraped ones directly into _scrapedMovie
            If rl.posterurls Then
                _scrapedMovie.listthumbs.Clear()
                GetPosterUrls()
            End If

            If Cancelled() Then Exit Sub

            If rl.missingposters Then           'Download Missing Posters
                DoDownloadPoster(rl.missingposters)
            End If

            If Cancelled() Then Exit Sub

            If rl.missingfanart Then            'Download Missing Fanart
                DownloadFanart()
            End If

            If rl.dlxtraart Then                'Download Missing Extra Thumbs or Extra Fanart
                DownloadExtraFanart()
            End If

            If rl.tmdb_set_name Then
                Try
                    _rescrapedMovie.fullmoviebody.movieset = "-None-"
                    If Not IsNothing(tmdb.Movie.belongs_to_collection) Then
                        _rescrapedMovie.fullmoviebody.movieset = tmdb.Movie.belongs_to_collection.name
                    End If
                    UpdateProperty(_rescrapedMovie.fullmoviebody.movieset, _scrapedMovie.fullmoviebody.movieset)
                Catch
                End Try
            End If

        End If

        If Cancelled() Then Exit Sub

        If rl.actors Then
            _rescrapedMovie.listactors.Clear()
            _rescrapedMovie.listactors = GetImdbActors()

            If _rescrapedMovie.listactors.Count > 0 Then
                _scrapedMovie.listactors.Clear()
                _scrapedMovie.listactors.AddRange(_rescrapedMovie.listactors)
            End If
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

        AssignMovieToCache()
        '		AssignMovieToAddMissingData
        HandleOfflineFile()             ' Do we need this?
        SaveNFO()

        If rl.Rename_Files Then   'And Not Preferences.usefoldernames AndAlso Not NfoPathAndFilename.ToLower.Contains("video_ts") AndAlso Not Preferences.basicsavemode Then
            ReportProgress(, RenameExistingMetaFiles)
            'SaveNFO
        End If

        If rl.Rename_Folders Then
            ReportProgress(, RenameMovFolder)
        End If

        UpdateCaches()
    End Sub

    Sub UpdateProperty(Of T) (ByVal fromField As T, ByRef toField As T,  Optional rescrape As Boolean=True )  
        If Not rescrape         Then Exit Sub
        If IsNothing(fromField) Then Exit Sub
        toField = fromField
    End Sub    

    Sub UpdateCaches
        UpdateActorCache
        UpdateMovieCache
    End Sub

    Sub RemoveMovieFromCaches
        RemoveActorsFromCache
        RemoveMovieFromCache
    End Sub

    Sub UpdateActorCache
        RemoveActorsFromCache
        _parent.ActorDb.AddRange(Actors)
    End Sub

    Sub RemoveActorsFromCache
        If Actors.Count = 0 Then Exit Sub
        RemoveActorsFromCache(Actors(0).MovieId)
    End Sub

    Sub RemoveActorsFromCache(MovieId)
        _parent.ActorDb.RemoveAll(Function(c) c.MovieId = MovieId)
    End Sub

    Sub UpdateActorCacheFromEmpty
        If Actors.Count = 0 Then Exit Sub
        _parent._tmpActorDb.AddRange(Actors)
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
                Dim bm As New Bitmap(PosterPath)

                bm = Utilities.ResizeImage(bm, 150, 200)

                Utilities.SaveImage(bm, PosterCachePath)

                bm.Dispose
            Catch
                'Invalid file
                Utilities.SafeDeleteFile(PosterPath     )
                Utilities.SafeDeleteFile(PosterCachePath)
            End Try
            Try
                For Each poster As PictureBox In Form1.TabPage22.Controls
            If poster.Tag = _scrapedMovie.fileinfo.fullpathandfilename Then
                poster.ImageLocation = posterpath
                poster.Load()
                poster.Tag = _scrapedMovie.fileinfo.fullpathandfilename
            End If
        Next
            Catch
            End Try
        End If
    End Sub

    Sub LoadPosterFromPosterCache(picBox As PictureBox)

        If Not File.Exists(PosterCachePath) Then SavePosterToPosterWallCache

        picBox.Tag = Nothing

        If File.Exists(PosterCachePath) Then
            Try
                picBox.Image = Utilities.LoadImage(PosterCachePath)
                picBox.Tag = PosterPath
            Catch
                'Invalid file
                Utilities.SafeDeleteFile(PosterPath     )
                Utilities.SafeDeleteFile(PosterCachePath)
            End Try
        Else
            Try
                picBox.Image = Utilities.LoadImage(Utilities.DefaultPosterPath)
            Catch
            End Try
        End If

    End Sub


    Function GetActorFileName( actorName As String) As String
        Return IO.Path.Combine(ActorPath, actorName.Replace(" ", "_") & ".tbn")
    End Function


    Public Function RenameExistingMetaFiles As String

        Dim log             = ""
        Dim targetMovieFile = ""
        Dim targetNfoFile   = "" 
        Dim oldName         = "" 
        Dim newName         = "" 
        Dim nextStackPart   = ""
        Dim stackdesignator = ""
        Dim newpath         = NfoPath
        Dim mediaFile       = mediapathandfilename
        Dim stackName       = mediaFile
        Dim isStack         = False
        Dim isSubStack      = False
        Dim isFirstPart     = True
        Dim newextension    = IO.Path.GetExtension(mediaFile)
        Dim newfilename     = UserDefinedBaseFileName
        Dim subName1 As String = mediafile.Replace(newextension,"")
        Dim subName As String = subName1
        Dim subextn As List(Of String) = Utilities.ListSubtitleFilesExtensions(subName)
        Dim subStackList As New List(Of String)

        Dim movieStackList As New List(Of String)(New String() {mediaFile})
        
        Try
            If Not Preferences.usefoldernames AndAlso Not NfoPathAndFilename.ToLower.Contains("video_ts") AndAlso Not NfoPathAndFilename.ToLower.Contains("bdmv") AndAlso Not Preferences.basicsavemode Then
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
                        Do While Utilities.isMultiPartMedia(subName, False, isFirstPart, stackdesignator, nextStackPart)
                            If isFirstPart Then
                                isSubStack = True                    'this media file has already been added to the list, but check for existing file with new name
                                Dim i As Integer                  'sacrificial variable to appease the TryParseosaurus Checks
                                targetMovieFile = newpath & newfilename & stackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
                                'If Utilities.testForFileByName(targetMovieFile, subextn) Then
                                '    aFileExists = True
                                '    Exit Do
                                'End If
                                'If Preferences.namemode = "1" Then targetNfoFile = targetMovieFile
                            Else
                                subStackList.Add(mediaFile)
                            End If
                            subName = newpath & subName & stackdesignator & nextStackPart & subex
                            mediaFile = subName
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
                    Utilities.isMultiPartMedia(oldname1, True)
                    'Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), subextn)
                    Dim changename As String = subStackList(i).Replace(oldname1, newfilename)
                    oldName = subStackList(i)
                    newName = changename
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
        Dim NoDel As Boolean = False
        Dim FilePath As String = nfopath   'current path
        Dim currentroot As String = ""
         
        'Get current root folder
        For Each rtfold In Preferences.movieFolders
            If FilePath.Contains(rtfold) Then currentroot = rtfold
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
            Dim chkfldr As String = checkfolder & "\"
            If chkfldr.ToLower = FilePath.ToLower Then
                log &= "!!! Path for: " & checkfolder & vbCrLf 
                log &= "!!! already Exists, no need to move files" & vbCrLf & vbcrlf
                Return log
            End If
        End If
        
        RemoveMovieFromCache         'Due to path changes, remove from Cache beforehand.

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
            Dim Moviename As String = _movieCache.filename.Replace(".nfo","")
            Dim di As DirectoryInfo = New DirectoryInfo((currentroot & "\"))
            For Each fi As IO.FileInfo In di.GetFiles((Moviename & "*"))
                fi.MoveTo(Path.Combine(checkfolder, fi.Name))
            Next
            log &= "Movie moved from Root folder into new path" & vbCrLf 
            
            'Copy actor images from root .actor folder to new folder's .actor folder

            Dim actorsource As String= currentroot & "\.actors\"
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

        testName = mediapathandfilename.Replace(IO.Path.GetExtension(mediapathandfilename), anciliaryFile)
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
                    s = s.Replace("%T", _scrapedMovie.fullmoviebody.title.SafeTrim)
                    s = s.Replace("%Z", _scrapedMovie.fullmoviebody.sortorder.SafeTrim)
                    s = s.Replace("%Y", _scrapedMovie.fullmoviebody.year)          
                    s = s.Replace("%I", _scrapedMovie.fullmoviebody.imdbid)        
                    s = s.Replace("%P", _scrapedMovie.fullmoviebody.premiered)     
                    s = s.Replace("%R", _scrapedMovie.fullmoviebody.rating)
                    s = s.Replace("%V", vr)        
                    s = s.Replace("%A", ac1)
                    s = s.Replace("%O", ach)
                    s = s.Replace("%C", vc)
                    s = s.Replace("%L", _scrapedMovie.fullmoviebody.runtime)       
                    s = s.Replace("%S", _scrapedMovie.fullmoviebody.source) 
                    s = Utilities.cleanFilenameIllegalChars(s)
                    If Preferences.MovRenameUnderscore Then
                        s = Utilities.SpacesToUnderscores(s)
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
                    s = s.Replace("%T", _scrapedMovie.fullmoviebody.title.SafeTrim)
                    s = s.Replace("%Z", _scrapedMovie.fullmoviebody.sortorder.SafeTrim)
                    s = s.Replace("%Y", _scrapedMovie.fullmoviebody.year)          
                    s = s.Replace("%I", _scrapedMovie.fullmoviebody.imdbid)        
                    s = s.Replace("%P", _scrapedMovie.fullmoviebody.premiered)     
                    s = s.Replace("%R", _scrapedMovie.fullmoviebody.rating)
                    s = s.Replace("%G", vgenre)
                    s = s.Replace("%N", Preferences.RemoveIgnoredArticles(_scrapedMovie.fullmoviebody.movieset))
                    s = s.Replace("%V", vr)        
                    s = s.Replace("%A", ac1)
                    s = s.Replace("%O", ach)
                    s = s.Replace("%C", vc)
                    s = s.Replace("%L", _scrapedMovie.fullmoviebody.runtime)       
                    s = s.Replace("%S", _scrapedMovie.fullmoviebody.source) 
                    s = Utilities.cleanFoldernameIllegalChars(s)
                    If Preferences.MovRenameUnderscore Then
                        s = Utilities.SpacesToUnderscores(s)
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
                vc = vc1.ToUpper
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
                Dim vr2 As String = _scrapedMovie.filedetails.filedetails_video.ScanType.Value
                If vr2.ToLower = "progressive" Then
                    vr2 = "P"
                Else If vr2.ToLower = "interlaced" or vr2.ToLower = "mbaff" or vr2.ToLower = "paff" Then
                    vr2 = "I"
                Else
                    vr2 = ""
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

        Dim html As String = s.loadwebpage(url,True,10).ToString

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
                IO.File.Move(ActualPosterPath,NfoPathPrefName.Replace(IO.Path.GetExtension(NfoPathPrefName), "-poster.jpg"))
            End If 
        Catch ex As Exception
            Dim x = ex
        End Try
    End Sub


End Class
