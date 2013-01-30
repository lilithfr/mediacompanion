Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Media_Companion
Imports System.Text
Imports YouTubeFisher

Public Class Movie

    Public Const ResolutionsFile = "Resolutions.xml"
    Const MSG_PREFIX = " - "
    Const MSG_ERROR = "ERROR!"
    Const MSG_OK    = "OK"
    Const MSG_DONT_RESIZE = "Don't resize"
    Const YOU_TUBE_URL_PREFIX = "http://www.youtube.com/watch?v="

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

    Private _nfoFunction          As New WorkingWithNfoFiles
    Private _imdbCounter          As Integer =  0
    Private _imdbBody             As String  = ""
    Private _scraped              As Boolean = False
    Private _scrapedMovie         As New FullMovieDetails
    Private _rescrapedMovie       As FullMovieDetails
    Private _movieCache           As New ComboList
    Private _certificates         As New List(Of String)
    Private _imdbScraper          As New Classimdb
    Private _scraperFunctions     As New ScraperFunctions
    Private _titleFull            As String = ""
    Private _title                As String = ""
    Private _parent               As Movies
    Private _possibleImdb         As String = "-1"
    Private _youTubeTrailer       As YouTubeVideoFile

    Shared Private _availableHeightResolutions As List(Of Integer)

    #Region "Read-write properties"
    Property tmdb                 As TMDb 
    Property mediapathandfilename As String = ""
    Property TimingsLog           As String = ""
    Property ScrapeTimingsLogThreshold As Integer = Preferences.ScrapeTimingsLogThreshold
    Property LogScrapeTimes            As Boolean = Preferences.LogScrapeTimes
    Property TrailerUrl           As String = ""
    Property PosterUrl            As String = ""
    Property Actions As New ScrapeActions
    Property nfopathandfilename As String = ""

#End Region 'Read-write properties


    #Region "Read-only properties"

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

    Shared Public ReadOnly Property AvailableHeightResolutions
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


    ReadOnly Property TrailerExists
        Get
            Return File.Exists(ActualTrailerPath)
        End Get
    End Property


    ReadOnly Property ActualTrailerPath As String
        Get
            Dim s = NfoPathPrefName
            Dim FileName As String = ""
        
            For Each item In "mp4,flv,webm".Split(",")
                FileName = IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), System.IO.Path.GetFileNameWithoutExtension(s) & "-trailer." & item)

                If File.Exists(FileName) Then Return FileName
            Next

            Return IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), System.IO.Path.GetFileNameWithoutExtension(s) & "-trailer.flv")
        End Get
    End Property




    ReadOnly Property nfopath As String
        Get
            Return Path.GetDirectoryName(mediapathandfilename) & IO.Path.DirectorySeparatorChar
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

    ReadOnly Property TitleFull As String
        Get
            If _titleFull = "" Then
                Dim movieStackName As String = mediapathandfilename
                Dim firstPart As Boolean

                _titleFull = mediapathandfilename
                If Utilities.isMultiPartMedia(movieStackName, False, firstPart) Then
                    If Preferences.namemode <> "1" Then
                        _titleFull = nfopath & movieStackName & Extension
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
            If Preferences.usefoldernames Or Extension.ToLower = ".ifo" Then
                Return Utilities.GetLastFolder(nfopathandfilename)
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
            Return Utilities.CleanFileName(Title, False)
        End Get 
    End Property

    Public ReadOnly Property NfoPathPrefName As String
        Get
            If Preferences.basicsavemode  Then
                Return nfopathandfilename.Replace(Path.GetFileName(nfopathandfilename), "movie.nfo")
            Else
                Return nfopathandfilename
            End If            
        End Get 
    End Property

    Public ReadOnly Property PosterPath As String
        Get
            Return Preferences.GetPosterPath(NfoPathPrefName)
        End Get 
    End Property

    Public ReadOnly Property FanartPath As String
        Get
            Return Preferences.GetFanartPath(NfoPathPrefName)
        End Get 
    End Property

    Public ReadOnly Property PossibleImdb As String
        Get
            If _possibleImdb = "-1" Then
                Dim log=""
                _possibleImdb = getExtraIdFromNFO(nfopath,log)
                   
                ReportProgress(,log)    
                If IsNothing(_possibleImdb) Then
                    Dim mat As Match = Regex.Match(nfopathandfilename, "(tt\d{7})")

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
            Dim M As Match = Regex.Match(nfopathandfilename, "[\(\[]([\d]{4})[\)\]]")
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

	Public ReadOnly Property TrailerPath
        Get
            Return Path.Combine(NfoPathPrefName.Replace(Path.GetFileName(NfoPathPrefName),""), Path.GetFileNameWithoutExtension(NfoPathPrefName) & "-trailer." & TrailerPathExtension)
        End Get
	End Property

	Public ReadOnly Property TrailerPathExtension
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

        If fileInfo.Extension.ToLower = ".vob" Then
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
                Return True
            End If
            log &= "??? File extension missing!"
            Return False
        End If

        If Preferences.usefoldernames = True Then
            log &= "  '" & fileInfo.Directory.Name.ToString & "'"     'log directory name as Title due to use FOLDERNAMES
        Else
            log &= "  '" & fileInfo.ToString & "'"                    'log title name
        End If

        Dim movieNfoFile As String = fileInfo.FullName
        If Utilities.findFileOfType(movieNfoFile, ".nfo") Then
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
 

    Sub New( FullName As String, parent As Movies )
        Me.New
        _parent              = parent
        mediapathandfilename = FullName
        nfopathandfilename = mediapathandfilename.Replace(Extension, ".nfo")
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
        Actions.Items.Add( New ScrapeAction(AddressOf DoRename                    , "Rename"                    ) )
        Actions.Items.Add( New ScrapeAction(AddressOf ImdbScrapeActors            , "IMDB Actors scraper"       ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignTrailerUrl            , "Get trailer URL"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoPosterThumbs        , "Getting extra Frodo Poster thumbs") )
        Actions.Items.Add( New ScrapeAction(AddressOf GetFrodoFanartThumbs        , "Getting extra Frodo Fanart thumbs") )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignPosterUrls            , "Get poster URLs"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignHdTags                , "Assign HD Tags"            ) )
        Actions.Items.Add( New ScrapeAction(AddressOf TidyUpAnyUnscrapedFields    , "Tidy up unscraped fields"  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf SaveNFO                     , "Save Nfo"                  ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignMovieToCache          , "Assign new movie to cache" ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadPoster              , "Poster download"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadFanart              , "Fanart download"           ) )
        Actions.Items.Add( New ScrapeAction(AddressOf AssignMovieToAddMissingData , "Assign missing data"       ) )
        Actions.Items.Add( New ScrapeAction(AddressOf DownloadTrailer             , "Trailer download"          ) )
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
        If Not _scraped then
            _scraped  = True
            Actions.Items.Add( New ScrapeAction(AddressOf IniTmdb             , "Initialising TMDb"              ) )
            Actions.Items.Add( New ScrapeAction(AddressOf ImdbScraper_GetBody , "Scrape IMDB Main body"          ) )
            Actions.Items.Add( New ScrapeAction(AddressOf CheckImdbBodyScrape , "Checking IMDB Main body scrape" ) )            
            RunScrapeActions
        End if
    End Sub

    Sub RunScrapeActions
        While Actions.Items.Count>0

            Dim action = Actions.Items(0)

            action.Run

            If LogScrapeTimes And action.Time.ElapsedTimeMs > ScrapeTimingsLogThreshold then
                TimingsLog &= vbCrLf & "[" & action.ActionName & "] took " & action.Time.ElapsedTimeMs & "ms to run"
            End if

            If Cancelled then Exit Sub

            Actions.Items.RemoveAt(0)
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

        progressText = progressText.Trim

        If progressText.Length = 0 then Return Nothing

        Return MSG_PREFIX + progressText
    End Function

    Sub ImdbScraper_GetBody
        _imdbBody = ImdbScrapeBody(SearchName, PossibleYear, PossibleImdb)
    End Sub

    Sub ImdbScraper_GetBodyByImdbOnly
        _imdbBody = ImdbScrapeBody(, , PossibleImdb)
    End Sub


    Function ImdbScrapeBody(Optional Title As String=Nothing, Optional PossibleYear As String=Nothing, Optional PossibleImdb  As String=Nothing) As String

        If Not IsNothing(Title) then
            ReportProgress( Title, "Scraping Title:- " & Title & vbCrLf )
        End If

        'If PossibleImdb <> "" then
        '    ReportProgress( "IMDB " & PossibleImdb )
        'End If

        'ReportProgress( String.Format("Using '{0}{1}'", title, If(String.IsNullOrEmpty(PossibleYear), "", " " & PossibleYear)) )

        ReportProgress( "Main body" )
   
        Return _imdbScraper.getimdbbody(Title, PossibleYear, PossibleImdb, Preferences.imdbmirror, _imdbCounter)
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
            ReportProgress(MSG_ERROR,"!!! - ERROR!, please add Movie Manually","!!! Unable to scrape body with refs """ & Title & """, """ & PossibleYear & """, """ & PossibleImdb & """, """ & Preferences.imdbmirror & """" & vbCrLf)
            AppendScrapeFailedActions
        Else
            ReportProgress(MSG_OK,"Movie Body Scraped OK" & vbCrLf)
            AppendScrapeSuccessActions
        End If
    End Sub


    Sub TidyUpAnyUnscrapedFields
        If _scrapedMovie.fullmoviebody.title = Nothing or _scrapedMovie.fullmoviebody.title = "" Then
            _scrapedMovie.fullmoviebody.title = Title
            _scrapedMovie.fullmoviebody.plot  = "This movie could not be identified by Media Companion. To add the movie manually, go to the movie edit page and select ""Change Movie"", then select the correct movie."
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

         _scrapedMovie.fileinfo.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
    End Sub

    Sub LoadNFO
        _scrapedMovie = _nfoFunction.mov_NfoLoadFull(NfoPathPrefName)
    End Sub

    Sub SaveNFO
        _nfoFunction.mov_NfoSave(NfoPathPrefName, _scrapedMovie, True)
    End Sub

    Sub AssignUnknownMovieToCache
        AssignMovieToCache
        _movieCache.missingdata1 = 3
        _movieCache.runtime      = "0"
    End Sub

    Sub AssignMovieToCache
        _movieCache.fullpathandfilename = NfoPathPrefName
        _movieCache.filename            = Path.GetFileName(nfopathandfilename)
        _movieCache.foldername          = Utilities.GetLastFolder(nfopathandfilename)
        _movieCache.title               = _scrapedMovie.fullmoviebody.title
        _movieCache.originaltitle       = _scrapedMovie.fullmoviebody.originaltitle
        _movieCache.sortorder           = _scrapedMovie.fullmoviebody.sortorder
        _movieCache.runtime             = _scrapedMovie.fullmoviebody.runtime
        _movieCache.votes               = _scrapedMovie.fullmoviebody.votes

        If _scrapedMovie.fullmoviebody.title <> Nothing Then
            If _scrapedMovie.fullmoviebody.year <> Nothing Then
                If _scrapedMovie.fullmoviebody.title.ToLower.IndexOf("the") = 0 Then
                    _movieCache.titleandyear = _scrapedMovie.fullmoviebody.title.Substring(4, _scrapedMovie.fullmoviebody.title.Length - 4) & ", The (" & _scrapedMovie.fullmoviebody.year & ")"
                Else
                    _movieCache.titleandyear = _scrapedMovie.fullmoviebody.title & " (" & _scrapedMovie.fullmoviebody.year & ")"
                End If
            Else
                _movieCache.titleandyear = _scrapedMovie.fullmoviebody.title & " (0000)"
            End If
        Else
            _movieCache.titleandyear = "Unknown (0000)"
        End If

        _movieCache.outline = _scrapedMovie.fullmoviebody.outline
        _movieCache.plot    = _scrapedMovie.fullmoviebody.plot
        _movieCache.year    = _scrapedMovie.fullmoviebody.year


        Dim filecreation As New IO.FileInfo(nfopathandfilename)

        Try
            _movieCache.filedate = Format(filecreation.LastWriteTime, Preferences.datePattern).ToString
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

        Try
            _movieCache.createdate = Format(System.DateTime.Now, Preferences.datePattern).ToString
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

        _movieCache.id        = _scrapedMovie.fullmoviebody.imdbid
        _movieCache.rating    = _scrapedMovie.fullmoviebody.rating
        _movieCache.top250    = _scrapedMovie.fullmoviebody.top250
        _movieCache.genre     = _scrapedMovie.fullmoviebody.genre
        _movieCache.playcount = _scrapedMovie.fullmoviebody.playcount
    End Sub

    Sub AssignScrapedMovie
        AssignScrapedMovie(_scrapedMovie)
    End Sub
    
    Sub AssignScrapedMovie(_scrapedMovie As FullMovieDetails)
        Dim thumbstring As New XmlDocument

        If Preferences.keepfoldername Then
            If Preferences.usefoldernames Then
                _scrapedMovie.fullmoviebody.title = Utilities.CleanFileName(Utilities.GetLastFolder(nfopathandfilename), False)
            Else
                _scrapedMovie.fullmoviebody.title = Utilities.CleanFileName(Path.GetFileName(nfopathandfilename), False)
            End If
        End If

        thumbstring.LoadXml(ImdbBody)

        For Each thisresult In thumbstring("movie")
            Select Case thisresult.Name
                Case "title"
                    If Not Preferences.keepfoldername Then
                        _scrapedMovie.fullmoviebody.title = thisresult.InnerText
                    End If
                Case "originaltitle"
                    _scrapedMovie.fullmoviebody.originaltitle = thisresult.InnerText
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
                    _scrapedMovie.fullmoviebody.rating = thisresult.InnerText
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

                    If IsNothing(_possibleImdb) Then
                        _possibleImdb = _scrapedMovie.fullmoviebody.imdbid
                        tmdb.Imdb = PossibleImdb
                    End If
                Case "cert"
                    _certificates.Add(thisresult.InnerText)
            End Select
        Next

        _scrapedMovie.fullmoviebody.sortorder = _scrapedMovie.fullmoviebody.title               'Sort order defaults to title

        If _scrapedMovie.fullmoviebody.plot      = ""      Then _scrapedMovie.fullmoviebody.plot      = _scrapedMovie.fullmoviebody.outline     ' If plot is empty, use outline
        If _scrapedMovie.fullmoviebody.playcount = Nothing Then _scrapedMovie.fullmoviebody.playcount = "0"
        If _scrapedMovie.fullmoviebody.top250    = Nothing Then _scrapedMovie.fullmoviebody.top250    = "0"


        If Preferences.GetMovieSetFromTMDb And Not IsNothing(tmdb.Movie.belongs_to_collection) Then
            _scrapedMovie.fullmoviebody.movieset = tmdb.Movie.belongs_to_collection.name
        End If

        ' Assign certificate
        Dim done As Boolean = False
        For g = 0 To UBound(Preferences.certificatepriority)
            For Each cert In certificates
                If cert.IndexOf(Preferences.certificatepriority(g)) <> -1 Then
                    _scrapedMovie.fullmoviebody.mpaa = cert.Substring(cert.IndexOf("|") + 1, cert.Length - cert.IndexOf("|") - 1)
                    done = True
                    Exit For
                End If
            Next
            If done = True Then Exit For
        Next

    End Sub
    
    Sub DoRename
        If Preferences.MovieRenameEnable AndAlso Not Preferences.usefoldernames AndAlso Not nfopathandfilename.ToLower.Contains("video_ts") AndAlso Not Preferences.basicsavemode Then
            ReportProgress(,fileRename(_scrapedMovie.fullmoviebody, me))
        End If
    End Sub

    Sub ImdbScrapeActors
        _scrapedMovie.listactors.Clear
        ImdbScrapeActors(_scrapedMovie.listactors)
    End Sub

    Sub ImdbScrapeActors(actors As List(Of str_MovieActors))

        ReportProgress("Actors")

        Dim actorlist   As String = _imdbScraper.getimdbactors(Preferences.imdbmirror, _scrapedMovie.fullmoviebody.imdbid,,Preferences.maxactors)
        Dim thumbstring As New XmlDocument
        Dim thisresult  As XmlNode
        

        Try
            thumbstring.LoadXml(actorlist)

            For Each thisresult In thumbstring("actorlist")
                Select Case thisresult.Name
                    Case "actor"

                        Dim newactor As New str_MovieActors

                        For Each detail In thisresult.ChildNodes
                            Select Case detail.Name
                                Case "name"
                                    newactor.actorname = detail.InnerText
                                Case "role"
                                    newactor.actorrole = detail.InnerText
                                Case "thumb"
                                    newactor.actorthumb = detail.InnerText
                                Case "actorid"
                                    If newactor.actorthumb<>Nothing and detail.InnerText<>"" Then
                                        Try
                                            If Preferences.actorseasy Then

                                                Dim hg As New IO.DirectoryInfo(ActorPath)
                                                If Not hg.Exists Then
                                                    IO.Directory.CreateDirectory(ActorPath)
                                                End If

                                                SaveActorImageToCacheAndPath(newactor.actorthumb, GetActorFileName(newactor.actorname) )
                                            Else
                                                If Preferences.actorsave then              
                                                    Dim tempstring  = Preferences.actorsavepath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)

                                                    Dim hg As New IO.DirectoryInfo(tempstring)
                                                    If Not hg.Exists Then
                                                        IO.Directory.CreateDirectory(tempstring)
                                                    End If
                                                              
                                                    Dim workingpath = tempstring & "\" & detail.InnerText & ".jpg"

                                                    DownloadCache.SaveImageToCacheAndPath(newactor.actorthumb, workingpath, Preferences.overwritethumbs, , GetHeightResolution(Preferences.ActorResolutionSI))

                                                    newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))

                                                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                        newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                    Else
                                                        newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                    End If
                                                End If
                                            End If

                                            actors.Add(newactor)
                                        Catch ex As Exception
                                            ReportProgress(MSG_ERROR,"!!! Error with " & nfopathandfilename & vbCrLf & "!!! An error was encountered while trying to add a scraped Actor" & vbCrLf & ex.Message & vbCrLf & vbCrLf)
                                        End Try
                                    End If
                            End Select
                        Next
                        

                        If actors.Count >= Preferences.maxactors then
                            Exit For
                        End If
                End Select
            Next


            ReportProgress(MSG_OK,"Actors scraped OK" & vbCrLf)
        Catch ex As Exception
            ReportProgress(MSG_ERROR,"!!! Error with " & nfopathandfilename & vbCrLf & "!!! An error was encountered while trying to scrape Actors" & vbCrLf & ex.Message & vbCrLf & vbCrLf)
            actors.Clear()
        End Try
    End Sub

    Sub AssignTrailerUrl
        If Not Preferences.gettrailer Then
            Exit Sub
        End If

        If TrailerExists Then
            ReportProgress("Trailer already exists","Trailer already exists - To download again, delete the existing one first i.e. this file : [" & ActualTrailerPath & "]" & vbCrLf)
            Exit Sub
        End If

        _scrapedMovie.fullmoviebody.trailer = GetTrailerUrl(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.imdbid)
    End Sub

    Function GetTrailerUrl( title As String, imdb As String ) As String
        ReportProgress("Trailer URL")

        _youTubeTrailer = Nothing
        
        If Preferences.moviePreferredTrailerResolution.ToUpper() <> "SD" Then
            TrailerUrl = MC_Scraper_Get_HD_Trailer_URL(Preferences.moviePreferredTrailerResolution, title)
        End If


        'Try YouTube for HD and SD...
        '
        If TrailerUrl = "" and tmdb.Trailers.youtube.Count > 0 then
            Dim ytVideoPage = tmdb.GetTrailerUrl(Preferences.moviePreferredTrailerResolution)
            If ytVideoPage <> "" then

                Try
			        Dim yts as YouTubeUrlGrabber = YouTubeUrlGrabber.Create(YOU_TUBE_URL_PREFIX+ytVideoPage)

			        If yts.AvailableVideoFormat.Length>0 Then

			            _youTubeTrailer = yts.selectTrailer(Preferences.moviePreferredTrailerResolution)

			            If Not IsNothing(_youTubeTrailer) Then
			                TrailerUrl = _youTubeTrailer.VideoUrl
                        End If
                    End If
                Catch       'Timed out...
                End Try
            End If
        End If


        If TrailerUrl = "" Then
            TrailerUrl = _imdbScraper.gettrailerurl(imdb, Preferences.imdbmirror)
        End If

        If TrailerUrl = "" Then
            ReportProgress("None found","No trailer URL found" & vbCrLf)
        Else
            If Utilities.UrlIsValid(TrailerUrl) Then
                ReportProgress(MSG_OK,"Trailer URL Scraped OK" & vbCrLf)
            Else
                ReportProgress("Invalid!","!!! The Scraped Trailer URL is either invalid or not available [" & TrailerUrl & "]" & vbCrLf)
            End If
        End If

        Return TrailerUrl
    End Function


    Sub GetFrodoPosterThumbs
        _scrapedMovie.frodoPosterThumbs.Clear

        If Preferences.FrodoEnabled Then
            _scrapedMovie.frodoPosterThumbs.AddRange(tmdb.FrodoPosterThumbs)
            ReportProgress("Extra Frodo Poster thumbs: " & tmdb.FrodoPosterThumbs.count, "Extra Frodo Poster thumbs: " & tmdb.FrodoPosterThumbs.count & vbCrLf)
        Else
            ReportProgress(,"Frodo extra URL scraping not selected" & vbCrLf)
        End If
    End Sub


    Sub GetFrodoFanartThumbs
        _scrapedMovie.frodoFanartThumbs.Thumbs.Clear

        If Preferences.FrodoEnabled Then
            _scrapedMovie.frodoFanartThumbs.Thumbs.AddRange(tmdb.FrodoFanartThumbs.Thumbs)
            ReportProgress("Extra Frodo Poster thumbs: " & tmdb.FrodoFanartThumbs.Thumbs.count, "Extra Frodo Poster thumbs: " & tmdb.FrodoFanartThumbs.Thumbs.count & vbCrLf)
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
            ReportProgress("None found","No extra Poster URLs found" + NfoPosterScraperInfo & vbCrLf)
        End If
    End Sub

    Function NfoPosterScraperInfo
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
        If sm.FileDetails.filedetails_video.DurationInSeconds.Value <> Nothing And (runtime_file or (Preferences.movieRuntimeDisplay = "file") or (Preferences.movieRuntimeFallbackToFile and sm.fullmoviebody.runtime = "")) Then
            sm.fullmoviebody.runtime = Utilities.cleanruntime(sm.FileDetails.filedetails_video.DurationInSeconds.Value) & " min"
        End If
    End Sub


    Sub DownloadTrailer
        DownloadTrailer(TrailerUrl)
	End Sub

    Sub DownloadTrailer(TrailerUrl As String)
	    'Check for and delete zero length trailer - created when Url is invalid
	    DeleteZeroLengthFile(ActualTrailerPath)

        If Not Preferences.DownloadTrailerDuringScrape Then
            Exit Sub
        End If

        'Don't re-download if trailer exists
        If File.Exists(ActualTrailerPath) then
            Exit Sub
        End if

        If TrailerUrl = "" then
            Exit Sub
        End if

        If Not Utilities.UrlIsValid(TrailerUrl) Then
            ReportProgress("Invalid Trailer Url detected","Invalid Trailer Url detected")
            Exit Sub
        End if

        ReportProgress("DL Trailer","Downloading trailer...")
        _WebFileDownloader.DownloadFileWithProgress(TrailerUrl, TrailerPath,_parent.Bw)

        DeleteZeroLengthFile(ActualTrailerPath)

        If Cancelled then Exit Sub

        ReportProgress(MSG_OK,"Trailer downloaded OK : [" & ActualTrailerPath & "]" & vbCrLf)
	End Sub

	Private Sub DeleteZeroLengthFile( fileName )
		If File.Exists(fileName) then
			If (New IO.FileInfo(fileName)).Length = 0 then
				File.Delete(fileName)
                ReportProgress("Zero length trailer deleted","Zero length trailer deleted : [" & fileName & "]")
			End If
		End If
	End sub

    Private Sub DownloadPoster
        If Not Preferences.scrapemovieposters then
            Exit Sub
        End If

        DoDownloadPoster
    End Sub
 
    Private Sub DoDownloadPoster
        If Not Preferences.overwritethumbs And File.Exists(PosterPath) Then
            Exit Sub
        End If

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
                        'moviethumburl = scraperFunction2.tmdbthumb(newmovie.fullmoviebody.imdbid)
                        PosterUrl = tmdb.FirstOriginalPosterUrl
                End Select
            Catch
                PosterUrl = "na"
            End Try

            validUrl = Utilities.UrlIsValid(PosterUrl)
            If validUrl Then
                Exit For
            End If
        Next


        If validUrl Then

            ReportProgress("Poster")

            Try
                DownloadCache.SaveImageToCacheAndPath(PosterUrl, PosterPath, Preferences.overwritethumbs, ,GetHeightResolution(Preferences.PosterResolutionSI))

        '          Utilities.DownloadImage(PosterUrl, PosterPath)

                ReportProgress(MSG_OK,"Poster scraped OK" & vbCrLf)

                If Preferences.createfolderjpg Then

                    Dim temppath = PosterPath.Replace(Path.GetFileName(PosterPath), "folder.jpg")

                    If Preferences.overwritethumbs Or Not IO.File.Exists(temppath) Then

                        File.Copy(PosterPath, temppath, True)
                        ReportProgress(,"Poster also saved as ""folder.jpg"" OK" & vbCrLf)
                    Else
                        ReportProgress(,"! ""folder.jpg"" not Saved to :- " & temppath & ", as file already exists" & vbCrLf)
                    End If
                End If

            Catch ex As Exception
                ReportProgress(MSG_ERROR,"!!! Problem Saving Poster" & vbCrLf & "!!! Error Returned :- " & ex.Message & vbCrLf & vbCrLf)
            End Try
        End If
    End Sub


    Sub DeleteScrapedFiles
        DeleteNFO
        DeleteActors
        DeletePoster
        DeleteFanart
        DeleteTrailer
    End Sub

    Sub DeleteActors
        Try
            'If Preferences.XBMC_version = 0 Then
                For Each f In Directory.GetFiles(ActorPath, "*.tbn")
                    File.Delete(f)
                Next
                'Directory.Delete(ActorPath)
            'ElseIf Preferences.XBMC_version = 2 Then
                For Each f In Directory.GetFiles(ActorPath, "*.jpg")
                    File.Delete(f)
                Next
                Directory.Delete(ActorPath)
            'End If
        Catch
        End Try
             
        'To Do : Delete from networkpath = Preferences.actorsavepath
    End Sub     


    Sub DeleteNFO
        Utilities.SafeDeleteFile(NfoPathPrefName)
    End Sub

    Sub DeletePoster
        DeleteFile(PosterPath)
        DeleteFile(PosterPath.Replace(Path.GetFileName(PosterPath),"folder.jpg"))
    End Sub


    Sub DeleteFanart
        DeleteFile(FanartPath)
    End Sub


    Sub DeleteTrailer
        DeleteFile(TrailerPath)
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
        If Not Preferences.overwritethumbs and File.Exists(FanartPath) Then
            ReportProgress(,"Fanart already exists -> Skipping" & vbCrLf)
            Exit Sub
        End If

        Dim FanartUrl As String=tmdb.GetBackDropUrl

        If Not IsNothing(FanartUrl) then
            ReportProgress("Fanart",)
            Try
   '            Utilities.DownloadImage(FanartUrl, FanartPath, True, Preferences.resizefanart)
                SaveFanartImageToCacheAndPath(FanartUrl, FanartPath)

                ReportProgress(MSG_OK,"Fanart URL Scraped OK" & vbCrLf)
            Catch ex As Exception
                ReportProgress(MSG_ERROR,"!!! Problem Saving Poster" & vbCrLf & "!!! Error Returned :- " & ex.ToString & vbCrLf & vbCrLf)
            End Try
        Else
            ReportProgress("Not available","Fanart not available for this movie on TMDb" & vbCrLf)
        End If
    End Sub

    Sub HandleOfflineFile
        If File.Exists(TempOffLineFileName) Then
            File.Delete(TempOffLineFileName)
            Call mov_OfflineDvdProcess(_movieCache.fullpathandfilename, _movieCache.title, Utilities.GetFileName(_movieCache.fullpathandfilename))
        End If
    End Sub

    Private Sub AssignMovieToAddMissingData
        If Not File.Exists(FanartPath) Then
            _movieCache.missingdata1 += 1
        End If

        If Not File.Exists(PosterPath) Then
            _movieCache.missingdata1 += 2
        End If
    End Sub

    Private Sub mov_OfflineDvdProcess(ByVal nfopath As String, ByVal title As String, ByVal mediapath As String)
 
        Dim tempint2   As Integer = 2097152
        Dim SizeOfFile As Integer = FileLen(mediapath)

        If SizeOfFile > tempint2 Then
            Exit Sub
        End If

        Try
            Dim fanartpath As String = ""

            If File.Exists(FanartPath) Then
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
            Dim proc_arguments As String = "-r 1 -b 1800 -qmax 6 -i """ & Preferences.applicationPath & "\Settings\%03d.jpg"" -vcodec msmpeg4v2 """ & mediapath & """"
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
        If Preferences.keepfoldername Then
             If Preferences.usefoldernames Then
                _scrapedMovie.fullmoviebody.title = Utilities.CleanFileName(Utilities.GetLastFolder(nfopathandfilename), False)
            Else
                _scrapedMovie.fullmoviebody.title = Utilities.CleanFileName(Path.GetFileName(nfopathandfilename), False)
            End If
        Else
            _scrapedMovie.fullmoviebody.title = tmdb.Movie.title
        End If

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
        If _scrapedMovie.fullmoviebody.top250    = Nothing Then _scrapedMovie.fullmoviebody.top250    = "0"

    End Sub
    #End Region 'TMDB Test scrape


    Public Shared Function getExtraIdFromNFO(ByVal fullPath As String, Optional log As String="") As String
        Dim extrapossibleID As String = Nothing
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
            If Preferences.renamenfofiles = True Then   'reenabled choice as per user preference
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


    Public Function fileRename(ByVal movieDetails As str_BasicMovieNFO, ByRef movieFileInfo As Movie) As String
        Dim log As String = ""
        Dim newpath As String = movieFileInfo.nfopath
        Dim mediaFile As String = movieFileInfo.mediapathandfilename
        Dim movieStackList As New List(Of String)(New String() {mediaFile})
        Dim stackName As String = mediaFile
        Dim isStack As Boolean = False
        Dim isFirstPart As Boolean = True
        Dim nextStackPart As String = ""
        Dim stackdesignator As String = ""
        Dim newextension As String = IO.Path.GetExtension(mediaFile)
        Dim newfilename As String = Preferences.MovieRenameTemplate
        Dim targetMovieFile As String = ""
        Dim targetNfoFile As String = ""
        Dim aFileExists As Boolean = False
        Try
            'create new filename (hopefully removing invalid chars first else Move (rename) will fail)
            newfilename = newfilename.Replace("%T", movieDetails.title)         'replaces %T with movie title
            newfilename = newfilename.Replace("%Y", movieDetails.year)          'replaces %Y with year   
            newfilename = newfilename.Replace("%I", movieDetails.imdbid)        'replaces %I with imdid 
            newfilename = newfilename.Replace("%P", movieDetails.premiered)     'replaces %P with premiered date 
            newfilename = newfilename.Replace("%R", movieDetails.rating)        'replaces %R with rating 
            newfilename = newfilename.Replace("%L", movieDetails.runtime)       'replaces %L with runtime (length)
            newfilename = newfilename.Replace("%S", movieDetails.source)        'replaces %S with movie source
            newfilename = Utilities.cleanFilenameIllegalChars(newfilename)      'removes chars that can't be in a filename

            'designate the new main movie file (without extension) and test the new filenames do not already exist
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

                For Each subtitle As String In {".sub", ".srt", ".smi", ".idx"} 'rename any subtitle files with the same name as the movie
                    If File.Exists(movieFileInfo.mediapathandfilename.Replace(newextension, subtitle)) Then
                        File.Move(movieFileInfo.mediapathandfilename.Replace(newextension, subtitle), targetMovieFile & subtitle) ' subtitles file with .sub extension
                        log &= "Renamed '" & subtitle & "' subtitle File" & vbCrLf
                    End If
                Next

                'update the new movie structure with the new data
                movieFileInfo.mediapathandfilename = targetMovieFile & newextension 'this is the new full path & filname to the rename media file
                movieFileInfo.nfopathandfilename = targetNfoFile & ".nfo"           'this is the new nfo path (yet to be created)
       '         movieFileInfo.Title = newfilename                                   'new title
            Else
                log &= String.Format("A file exists with the target filename of '{0}' - RENAME SKIPPED{1}", newfilename, vbCrLf)
            End If
        Catch ex As Exception
            log &= "!!!Rename Movie File FAILED !!!" & vbCrLf
        End Try
        Return log
    End Function


    Function RescrapeBody(rl As RescrapeList)
        Return rl.credits or rl.director or rl.stars   or rl.genre   or rl.mpaa   or rl.plot  or rl.premiered or rl.rating or 
               rl.runtime or rl.studio   or rl.tagline or rl.outline or rl.top250 or rl.votes or rl.country   or rl.year
    End Function
  
    
    Public Sub RescrapeSpecific(rl As RescrapeList)

        _rescrapedMovie = New FullMovieDetails
        
        'Loads previously scraped details from NFO into _scrapedMovie
        LoadNFO()

        IniTmdb(_scrapedMovie.fullmoviebody.imdbid)

        If Cancelled then Exit Sub

        If RescrapeBody(rl) then  
            
            _scraped  = True

            _imdbBody = ImdbScrapeBody(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.year, _scrapedMovie.fullmoviebody.imdbid)

            If _imdbBody = "MIC" Then                        
                ReportProgress(MSG_ERROR, "!!! - ERROR! - Rescrape IMDB body failed with refs """ & _scrapedMovie.fullmoviebody.title & """, """ & _scrapedMovie.fullmoviebody.year & """, """ & _scrapedMovie.fullmoviebody.imdbid & """, """ & Preferences.imdbmirror & """" & vbCrLf)
            Else
                ReportProgress(MSG_OK,"Movie Body Scraped OK" & vbCrLf)
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
        End If
        
        If Cancelled then Exit Sub
             
        If rl.trailer Then
            If TrailerExists Then
                ReportProgress("Trailer already exists","Trailer already exists - To download again, delete the existing one first i.e. this file : [" & ActualTrailerPath & "]" & vbCrLf)
            Else
                _rescrapedMovie.fullmoviebody.trailer = GetTrailerUrl(_scrapedMovie.fullmoviebody.title, _scrapedMovie.fullmoviebody.imdbid)
                UpdateProperty(_rescrapedMovie.fullmoviebody.trailer, _scrapedMovie.fullmoviebody.trailer)  
                DownloadTrailer(TrailerUrl)
            End If
        End If
       
        If Cancelled then Exit Sub

        'Clears the existing poster urls and adds the rescraped ones directly into _scrapedMovie
        If rl.posterurls Then 
            _scrapedMovie.listthumbs.Clear
            GetPosterUrls
        End If

        If Cancelled then Exit Sub

        If rl.actors Then 
            _rescrapedMovie.listactors.Clear
            ImdbScrapeActors(_rescrapedMovie.listactors)

            If _rescrapedMovie.listactors.Count>0 then
                _scrapedMovie.listactors.Clear
                _scrapedMovie.listactors.AddRange(_rescrapedMovie.listactors)
            End If
        End If

        If Cancelled then Exit Sub

        If rl.missingposters Then 
            DoDownloadPoster
        End If

        If Cancelled then Exit Sub

        If rl.missingfanart Then 
            DoDownloadFanart
        End If

        If rl.tmdb_set_name Then 
            Try
                _rescrapedMovie.fullmoviebody.movieset = tmdb.Movie.belongs_to_collection.name
                UpdateProperty(_rescrapedMovie.fullmoviebody.movieset,_scrapedMovie.fullmoviebody.movieset)  
            Catch
            End Try
        End If

        If rl.runtime_file or rl.mediatags or (rl.runtime And ((Preferences.movieRuntimeDisplay="file") or (Preferences.movieRuntimeFallbackToFile and _rescrapedMovie.fullmoviebody.runtime=""))) Then 
            If GetHdTags(_rescrapedMovie) then 
                UpdateProperty(_rescrapedMovie.FileDetails, _scrapedMovie.FileDetails) 

                If rl.runtime_file or (rl.runtime And ((Preferences.movieRuntimeDisplay="file") or (Preferences.movieRuntimeFallbackToFile and _rescrapedMovie.fullmoviebody.runtime=""))) Then
                    AssignRuntime(_rescrapedMovie,rl.runtime_file)
                    UpdateProperty(_rescrapedMovie.fullmoviebody.runtime, _scrapedMovie.fullmoviebody.runtime) 
                End If
            End If
        End If

        AssignMovieToCache
        AssignMovieToAddMissingData
        HandleOfflineFile             ' Do we need this?
        SaveNFO
        UpdateCaches
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

    Sub UpdateActorCache()
        If Actors.Count = 0 Then Exit Sub

        _parent.ActorDb.RemoveAll(Function(c) c.MovieId = Actors(0).MovieId)
        _parent.ActorDb.AddRange(Actors)
    End Sub

    Sub UpdateActorCacheFromEmpty()
        If Actors.Count = 0 Then Exit Sub

        '_parent.ActorDb.RemoveAll(Function(c) c.MovieId = Actors(0).MovieId)
        _parent._tmpActorDb.AddRange(Actors)
    End Sub

    Sub UpdateMovieCache()
        If _movieCache.fullpathandfilename = "" Then Exit Sub

        _parent.MovieCache.RemoveAll(Function(c) c.fullpathandfilename = _movieCache.fullpathandfilename)
        _parent.MovieCache.Add(_movieCache)

        _parent.Data_GridViewMovieCache.RemoveAll(Function(c) c.fullpathandfilename = _movieCache.fullpathandfilename)
        _parent.Data_GridViewMovieCache.Add(New Data_GridViewMovie(_movieCache))
    End Sub
 
  
    Shared Function SaveFanartImageToCacheAndPath(url As String, path As String)

        If Not Preferences.savefanart Then Return False

        Dim point = Movie.GetBackDropResolution(Preferences.BackDropResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPath(url, path, Preferences.overwritethumbs, point.x, point.y )
    End Function

    Shared Function SaveActorImageToCacheAndPath(url As String, path As String)
    
        'If Not Preferences.actorsave Then Return False

        Dim height = GetHeightResolution(Preferences.ActorResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPath(url, path, Preferences.overwritethumbs, , height  )
    End Function


    Shared Function SavePosterImageToCacheAndPath(url As String, path As String)

        Dim height = GetHeightResolution(Preferences.PosterResolutionSI)

        Return DownloadCache.SaveImageToCacheAndPath(url, path, Preferences.overwritethumbs, , height  )
    End Function


    Function GetActorFileName( actorName As String) As String
        Return IO.Path.Combine(ActorPath, actorName.Replace(" ", "_") & ".tbn")
    End Function



End Class
