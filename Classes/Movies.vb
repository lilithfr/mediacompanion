Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Media_Companion


Public Class Movies

    Public Event AmountDownloadedChanged (ByVal iNewProgress As Long)
    Public Event FileDownloadSizeObtained(ByVal iFileSize    As Long)
    Public Event FileDownloadComplete    ()
    Public Event FileDownloadFailed      (ByVal ex As Exception)

    Private _actorDb As New List(Of ActorDatabase)
    Public _tmpActorDb As New List(Of ActorDatabase)

    Public Property Bw            As BackgroundWorker = Nothing
    Public Property MovieCache    As New List(Of ComboList)
    
    Public Property NewMovies     As New List(Of Movie)
    Public Property PercentDone   As Integer = 0

    Private _data_GridViewMovieCache As New List(Of Data_GridViewMovie)

    Public ReadOnly Property Data_GridViewMovieCache As List(Of Data_GridViewMovie)
        Get
            Return _data_GridViewMovieCache
        End Get
    End Property
    
    Private Sub Rebuild_Data_GridViewMovieCache
        _data_GridViewMovieCache.Clear

        For Each item In MovieCache
            _data_GridViewMovieCache.Add(New Data_GridViewMovie(item))
        Next
    End Sub



    Public ReadOnly Property ActorDb As List(Of ActorDatabase)
        Get
            Return _actorDb
        End Get
    End Property

    Public ReadOnly Property Cancelled As Boolean
        Get
            Application.DoEvents
            If Not IsNothing(_bw) AndAlso _bw.WorkerSupportsCancellation AndAlso _bw.CancellationPending Then
                ReportProgress("Cancelled!",vbCrLf & "!!! Operation cancelled by user")
                Return True
            End If
            Return False
        End Get
    End Property


    Sub New(Optional bw As BackgroundWorker=Nothing)
        _bw = bw
    End Sub


    Sub newMovie_AmountDownloadedChanged(ByVal iNewProgress As Long)
        RaiseEvent AmountDownloadedChanged(iNewProgress)
    End Sub

    Sub newMovie_FileDownloadSizeObtained(ByVal iFileSize As Long)
        RaiseEvent FileDownloadSizeObtained(iFileSize)
    End Sub

    Sub newMovie_FileDownloadComplete
        RaiseEvent FileDownloadComplete
    End Sub

    Sub newMovie_FileDownloadFailed(ByVal ex As Exception)
        RaiseEvent FileDownloadFailed(ex)
    End Sub

    Sub ReportProgress( Optional progressText As String=Nothing, Optional log As String=Nothing, Optional command As Progress.Commands=Progress.Commands.SetIt )
        ReportProgress(New Progress(progressText,log,command))
    End Sub

    Sub ReportProgress( ByVal oProgress As Progress )
        If Not IsNothing(_bw) AndAlso _bw.WorkerReportsProgress AndAlso Not (String.IsNullOrEmpty(oProgress.Log) and String.IsNullOrEmpty(oProgress.Message)) Then
            _bw.ReportProgress(PercentDone, oProgress)
        End If
    End Sub

    Public Function FindCachedMovie(fullpathandfilename As String) As ComboList

        Dim q = From m In _movieCache Where m.fullpathandfilename=fullpathandfilename

        Return q.Single
    End Function



    Public Sub FindNewMovies(Optional scrape=True)
        NewMovies.Clear
        PercentDone = 0

        Dim folders As New List(Of String)

        AddOnlineFolders ( folders )
        AddOfflineFolders( folders )

        Dim i = 0
        For Each folder In folders
            i += 1
            PercentDone = CalcPercentDone(i,folders.Count)
            ReportProgress("Scanning folder " & i & " of " & folders.Count)

            AddNewMovies(folder)
            
            If Cancelled then 
                NewMovies.Clear
                Exit Sub
            End If
        Next

        If scrape then ScrapeNewMovies
    End Sub

    Private Sub AddMovieEventHandlers( oMovie As Movie )
        AddHandler    oMovie.ProgressLogChanged,       AddressOf ReportProgress
        AddHandler    oMovie.AmountDownloadedChanged,  AddressOf newMovie_AmountDownloadedChanged
        AddHandler    oMovie.FileDownloadSizeObtained, AddressOf newMovie_FileDownloadSizeObtained
        AddHandler    oMovie.FileDownloadComplete    , AddressOf newMovie_FileDownloadComplete
        AddHandler    oMovie.FileDownloadFailed      , AddressOf newMovie_FileDownloadFailed
    End Sub
    
    Private Sub RemoveMovieEventHandlers( oMovie As Movie )
        RemoveHandler oMovie.ProgressLogChanged,       AddressOf ReportProgress
        RemoveHandler oMovie.AmountDownloadedChanged,  AddressOf newMovie_AmountDownloadedChanged
        RemoveHandler oMovie.FileDownloadSizeObtained, AddressOf newMovie_FileDownloadSizeObtained
        RemoveHandler oMovie.FileDownloadComplete    , AddressOf newMovie_FileDownloadComplete
        RemoveHandler oMovie.FileDownloadFailed      , AddressOf newMovie_FileDownloadFailed
    End Sub
    
    Public Sub AddOnlineFolders( folders As List(Of String) )
        For Each moviefolder In Preferences.movieFolders

            Dim dirInfo As New DirectoryInfo(moviefolder)

            If dirInfo.Exists Then
                folders.Add(moviefolder)
                ReportProgress("Searching movie Folder: " & dirInfo.FullName.ToString & vbCrLf)

                For Each subfolder In Utilities.EnumerateFolders(moviefolder, 6)
                    folders.Add(subfolder)
                Next
            End If
            
            If Cancelled then Exit Sub
        Next
    End Sub

    Public Sub AddOfflineFolders( folders As List(Of String) )
        For Each moviefolder In Preferences.offlinefolders

            Dim dirInfo As New DirectoryInfo(moviefolder)

            If dirInfo.Exists Then
                ReportProgress(,"Found Offline Folder: " & dirInfo.FullName.ToString & vbCrLf & "Checking for subfolders" & vbCrLf)

                For Each subfolder In Utilities.EnumerateFolders(moviefolder, 0)
'                    ReportProgress(,"Subfolder added :- " & subfolder.ToString & vbCrLf)

                    Dim DummyFileName As String = Utilities.GetLastFolder(subfolder & "\whatever") & ".avi"
                    Dim DummyFullName As String = Path.Combine(subfolder, DummyFileName)
                    Dim NfoFullName   As String = DummyFullName.Replace(Path.GetExtension(DummyFullName), ".nfo")

                    If Not File.Exists(NfoFullName) Then

                        'Create temporary "tempoffline.ttt" file
                        If Not File.Exists(Path.Combine(subfolder, "tempoffline.ttt")) Then
                            Dim sTempFileName As String = Path.Combine(subfolder, "tempoffline.ttt")
                            Dim fsTemp As New FileStream(sTempFileName, IO.FileMode.Create)
                            fsTemp.Close()
                        End If

                        'Create temporary "whatever.avi"' file
                        If Not File.Exists(DummyFullName) Then
                            Dim fsTemp2 As New FileStream(DummyFullName, FileMode.Create)
                            fsTemp2.Close()
                        End If

                        folders.Add(subfolder)
                    End If
                Next

            End If
        Next
    End Sub

    Public Sub AddNewMovies(DirPath As String)
        Dim dirInfo As New DirectoryInfo(DirPath)
        Dim found   As Integer = 0

        For Each ext In Utilities.VideoExtensions
            
            ext = If((ext = "video_ts.ifo"), ext, "*" & ext)

            Try
                For Each fileInFo In dirInfo.GetFiles(ext)

                    If not ValidateFile(fileInFo) then
                        Continue For
                    End if

                    found += 1
                    NewMovies.Add( New Movie(fileInFo.FullName,Me) )
                Next 
            Catch ex As Exception
                #If SilentErrorScream Then
                    Throw ex
                #End If
            End Try

        Next

        If found > 0 then
           ReportProgress(,String.Format("{0} new movie{1} found in [{2}]", found, If(found=1,"","s"), DirPath) & vbCrLf)
        End IF
    End Sub

    Public Sub ScrapeFiles(files as List(Of String))
        AddNewMovies(files)
        ScrapeNewMovies
        files.Clear
    End Sub

    Public Sub AddNewMovies(files as List(Of String))
        NewMovies.Clear
        PercentDone = 0

        Dim i     = 0
        Dim found = 0
        Dim msg=""
        For Each file In files
            Dim fileInfo = New IO.FileInfo(file)

            i += 1
            PercentDone = CalcPercentDone(i,files.Count)
            msg="Validating file " & fileInfo.Name & "(" & i & " of " & files.Count & ")"
            ReportProgress(msg,msg & vbCrLf)

            If not ValidateFile(fileInFo) then
                Continue For
            End if

            found += 1
            NewMovies.Add( New Movie(fileInFo.FullName,Me) )

            If Cancelled then Exit Sub
        Next

    End Sub

    Sub ScrapeNewMovies
        If NewMovies.Count>0 then
            ReportProgress(,vbCrLf & vbCrLf & "A total of " & NewMovies.Count & " new movie" & If(NewMovies.Count=1,"","s") & " found -> Starting Main Scraper Process..." & vbCrLf & vbCrLf )
        Else
            ReportProgress(,vbCrLf & vbCrLf & "No new movies found" & vbCrLf & vbCrLf)
        End If
 

        Dim i = 0
        For Each newMovie In NewMovies
            i += 1
            PercentDone = CalcPercentDone(i,NewMovies.Count)
            ReportProgress( "Scraping " & i & " of " & NewMovies.Count )
            
            ScrapeMovie(newMovie)

            If newMovie.TimingsLog <> "" then
                ReportProgress(,vbCrLf & "Timings" & vbCrLf & "=======" & newMovie.TimingsLog & vbCrLf & vbCrLf)
            End If

            If Cancelled then Exit Sub
        Next

        ReportProgress( ,"Finished" )
    End Sub

    Sub ScrapeMovie(movie As Movie)
        AddMovieEventHandlers   ( movie )
        movie.Scrape
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub ChangeMovie(NfoPathAndFilename As String, imdb As String)

        Dim movie = New Movie(Utilities.GetFileName(NfoPathAndFilename),Me)

        movie.DeleteScrapedFiles

        AddMovieEventHandlers   ( movie )
        movie.Scrape(imdb)
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub RescrapeSpecificMovie(fullpathandfilename As String,rl As RescrapeList)

        Dim movie = New Movie(Utilities.GetFileName(fullpathandfilename),Me)

        AddMovieEventHandlers   ( movie )
        movie.RescrapeSpecific  ( rl    )
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub BatchRescrapeSpecific(filteredList As List(Of ComboList), rl As RescrapeList)
        Dim i=0
        For Each item In filteredList
            i += 1
            PercentDone = CalcPercentDone(i,filteredList.Count)
            ReportProgress("Batch Rescraping " & i & " of " & filteredList.Count)

            Dim movie = New Movie(Utilities.GetFileName(item.fullpathandfilename),Me)

            AddMovieEventHandlers   ( movie )
            movie.RescrapeSpecific  ( rl    )
            RemoveMovieEventHandlers( movie )

            If Cancelled then Exit Sub
        Next
    End Sub


    Sub RescrapeAll( NfoFilenames As List(Of String) )
        Dim i=0
        For Each NfoFilename In NfoFilenames
            i += 1
            PercentDone = CalcPercentDone(i,NfoFilenames.Count)
            ReportProgress("Rescraping " & i & " of " & NfoFilenames.Count)
            
            RescrapeMovie(NfoFilename)

            If Cancelled then Exit Sub
        Next
    End Sub


    Sub RescrapeSpecific( _rescrapeList As RescrapeSpecificParams )
        Dim rl As new RescrapeList(_rescrapeList.Field)

        Dim i=0
        For Each FullPathAndFilename In _rescrapeList.FullPathAndFilenames
            i += 1
            PercentDone = CalcPercentDone(i,_rescrapeList.FullPathAndFilenames.Count)
            ReportProgress("Rescraping '" & CapsFirstLetter(_rescrapeList.Field) & "' " & i & " of " & _rescrapeList.FullPathAndFilenames.Count)
            RescrapeSpecificMovie(FullPathAndFilename,rl)

            If Cancelled then Exit Sub
        Next
    End Sub

    Function CapsFirstLetter(words As String)
        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words)
    End Function


    Sub RescrapeMovie(NfoFilename as String)

        ScrapeMovie( New Movie(Utilities.GetFileName(NfoFilename),Me) )

    End Sub



    Function CalcPercentDone(onNumber As Integer, total As Integer)
        Return (100 /total) * onNumber
    End Function


    Public Function ValidateFile(fileInFo As IO.FileInfo)

        If AlreadyAdded(fileInFo.FullName) Then
            ReportProgress(," - Already Added!")
            Return False
        End If

        Dim log   = ""
        Dim valid = Movie.IsValidMovieFile(fileInFo, log)

        ReportProgress(log)

        Return valid
    End Function


    Public Function AlreadyAdded(fullName as String) As Boolean
        Dim q = From m In NewMovies Where m.nfopathandfilename.ToLower = fullName.ToLower
        Return (q.Count > 0)
    End Function


    Public Sub LoadCaches
        LoadMovieCache
        LoadActorCache
    End Sub


    Public Sub SaveCaches
        SaveMovieCache
        SaveActorCache
    End Sub


    Public Sub LoadMovieCache

        MovieCache.Clear

        Dim movielist  As New XmlDocument
        Dim objReader  As New StreamReader(Preferences.workingProfile.MovieCache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close

        movielist.LoadXml(tempstring)


        For Each thisresult In movielist("movie_cache")
            Select Case thisresult.Name
                Case "movie"
                    Dim newmovie As New ComboList

                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name

                            Case "missingdata1"
                                newmovie.missingdata1 = Convert.ToByte(detail.InnerText)
                            Case "source"
                                newmovie.source = detail.InnerText
                            Case "set"
                                newmovie.movieset = detail.InnerText
                            Case "sortorder"
                                newmovie.sortorder = detail.InnerText
                            Case "filedate"
                                If detail.InnerText.Length <> 14 Then 'i.e. invalid date
                                    newmovie.filedate = "19000101000000" '01/01/1900 00:00:00
                                Else
                                    newmovie.filedate = detail.InnerText
                                End If
                            Case "createdate"
                                If detail.InnerText.Length <> 14 Then 'i.e. invalid date
                                    newmovie.createdate = "19000101000000" '01/01/1900 00:00:00
                                Else
                                    newmovie.createdate = detail.InnerText
                                End If

                            Case "filename"
                                newmovie.filename = detail.InnerText
                            Case "foldername"
                                newmovie.foldername = detail.InnerText
                            Case "fullpathandfilename"
                                newmovie.fullpathandfilename = detail.InnerText
                            Case "genre"
                                newmovie.genre = detail.InnerText & newmovie.genre
                            Case "id"
                                newmovie.id = detail.InnerText
                            Case "playcount"
                                newmovie.playcount = detail.InnerText
                            Case "rating"
                                newmovie.rating = detail.InnerText
                            Case "title"
                                newmovie.title = detail.InnerText
                            Case "originaltitle"
                                newmovie.originaltitle = detail.InnerText
                            Case "titleandyear"
                                '--------- aqui
                                Dim TempString2 As String = detail.InnerText
                                If Preferences.ignorearticle = True Then
                                    If TempString2.ToLower.IndexOf("the ") = 0 Then
                                        Dim Temp As String = TempString2.Substring(TempString2.Length - 7, 7)
                                        TempString2 = TempString2.Substring(4, TempString2.Length - 11)
                                        TempString2 = TempString2 & ", The" & Temp
                                    End If
                                End If

                                newmovie.titleandyear = TempString2
                            Case "top250"
                                newmovie.top250 = detail.InnerText
                            Case "year"
                                newmovie.year = detail.InnerText
                            Case "outline"
                                newmovie.outline = detail.InnerText
                            Case "plot"
                                newmovie.plot = detail.InnerText
                            Case "runtime"
                                newmovie.runtime = detail.InnerText
                            Case "votes"
                                newmovie.votes = detail.InnerText
                        End Select
                    Next
                    If newmovie.source = Nothing Then
                        newmovie.source = ""
                    End If
                    If newmovie.movieset = Nothing Then
                        newmovie.movieset = "-None-"
                    End If
                    If newmovie.movieset = "" Then
                        newmovie.movieset = "-None-"
                    End If

                    MovieCache.Add(newmovie)
            End Select
        Next

        Rebuild_Data_GridViewMovieCache
    End Sub


    Public Sub SaveMovieCache

        Dim cacheFile As String = Preferences.workingProfile.MovieCache

        If File.Exists(cacheFile) Then
            File.Delete(cacheFile)
        End If


        Dim doc      As New XmlDocument
        Dim xmlproc  As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)

        Dim root  As XmlElement
        Dim child As XmlElement

        root = doc.CreateElement("movie_cache")

        Dim childchild As XmlElement

        For Each movie In MovieCache

            child = doc.CreateElement("movie")
            childchild = doc.CreateElement("filedate")
            childchild.InnerText = movie.filedate
            child.AppendChild(childchild)
            childchild = doc.CreateElement("createdate")
            childchild.InnerText = movie.createdate
            child.AppendChild(childchild)
            childchild = doc.CreateElement("missingdata1")
            childchild.InnerText = movie.missingdata1.ToString
            child.AppendChild(childchild)
            childchild = doc.CreateElement("filename")
            childchild.InnerText = movie.filename
            child.AppendChild(childchild)
            childchild = doc.CreateElement("foldername")
            childchild.InnerText = movie.foldername
            child.AppendChild(childchild)
            childchild = doc.CreateElement("fullpathandfilename")
            childchild.InnerText = movie.fullpathandfilename
            child.AppendChild(childchild)
            If movie.source <> Nothing And movie.source <> "" Then
                childchild = doc.CreateElement("source")
                childchild.InnerText = movie.source
                child.AppendChild(childchild)
            Else
                childchild = doc.CreateElement("source")
                childchild.InnerText = ""
                child.AppendChild(childchild)
            End If
            If movie.movieset <> Nothing Then
                If movie.movieset <> "" Or movie.movieset <> "-None-" Then
                    childchild = doc.CreateElement("set")
                    childchild.InnerText = movie.movieset
                    child.AppendChild(childchild)
                Else
                    childchild = doc.CreateElement("set")
                    childchild.InnerText = ""
                    child.AppendChild(childchild)
                End If
            Else
                childchild = doc.CreateElement("set")
                childchild.InnerText = ""
                child.AppendChild(childchild)
            End If
            childchild = doc.CreateElement("genre")
            childchild.InnerText = movie.genre
            child.AppendChild(childchild)
            childchild = doc.CreateElement("id")
            childchild.InnerText = movie.id
            child.AppendChild(childchild)
            childchild = doc.CreateElement("playcount")
            childchild.InnerText = movie.playcount
            child.AppendChild(childchild)
            childchild = doc.CreateElement("rating")
            childchild.InnerText = movie.rating
            child.AppendChild(childchild)
            childchild = doc.CreateElement("title")
            childchild.InnerText = movie.title
            child.AppendChild(childchild)
            childchild = doc.CreateElement("originaltitle")
            childchild.InnerText = movie.originaltitle
            child.AppendChild(childchild)
            If movie.sortorder = Nothing Then
                movie.sortorder = movie.title
            End If
            If movie.sortorder = "" Then
                movie.sortorder = movie.title
            End If
            childchild = doc.CreateElement("outline")
            childchild.InnerText = movie.outline
            child.AppendChild(childchild)
            childchild = doc.CreateElement("plot")
            If movie.plot.Length() > 100 Then
                childchild.InnerText = movie.plot.Substring(0, 100)     'Only write first 100 chars to cache- this plot is only used for table view - normal full plot comes from the nfo file (fullbody)
            Else
                childchild.InnerText = movie.plot
            End If

            child.AppendChild(childchild)
            childchild = doc.CreateElement("sortorder")
            childchild.InnerText = movie.sortorder
            child.AppendChild(childchild)
            childchild = doc.CreateElement("titleandyear")

            Try
                If movie.titleandyear.Length >= 5 Then
                    If movie.titleandyear.ToLower.IndexOf(", the") = movie.titleandyear.Length - 5 Then
                        Dim Temp As String = movie.titleandyear.Replace(", the", String.Empty)
                        movie.titleandyear = "The " & Temp
                    End If
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            childchild.InnerText = movie.titleandyear
            child.AppendChild(childchild)
            childchild = doc.CreateElement("runtime")
            childchild.InnerText = movie.runtime
            child.AppendChild(childchild)
            childchild = doc.CreateElement("top250")
            childchild.InnerText = movie.top250
            child.AppendChild(childchild)
            childchild = doc.CreateElement("year")
            childchild.InnerText = movie.year
            child.AppendChild(childchild)

            If movie.votes <> Nothing And movie.votes <> "" Then
                childchild = doc.CreateElement("votes")
                childchild.InnerText = movie.votes
                child.AppendChild(childchild)
            Else
                childchild = doc.CreateElement("votes")
                childchild.InnerText = ""
                child.AppendChild(childchild)
            End If

            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(cacheFile, System.Text.Encoding.UTF8)

        output.Formatting = Xml.Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub


    Public Sub LoadMovieCacheFromNfos
        MovieCache.Clear

        Dim t As New List(Of String)

        t.AddRange(Preferences.movieFolders)
        t.AddRange(Preferences.offlinefolders)

        ReportProgress("Searching movie folders...")
        mov_NfoLoad(t)

        If Cancelled Then Exit Sub

        For Each movie In MovieCache
            If Not Preferences.usefoldernames Then
                If movie.filename <> Nothing Then
                    movie.filename = movie.filename.Replace(".nfo", "")
                End If
            End If
        Next

        Rebuild_Data_GridViewMovieCache()
    End Sub


    Private Sub mov_NfoLoad(ByVal folderlist As List(Of String))
        Dim tempint As Integer
        Dim dirinfo As String = String.Empty
        Const pattern = "*.nfo"
        Dim moviePaths As New List(Of String)

        For Each moviefolder In folderlist
            If (New DirectoryInfo(moviefolder)).Exists Then
                moviePaths.Add(moviefolder)
            End If
        Next

        tempint = moviePaths.Count

        'Add sub-folders
        For f = 0 To tempint - 1
            For Each subfolder In Utilities.EnumerateFolders(moviePaths(f), Long.MaxValue)
                moviePaths.Add(subfolder)
            Next
        Next


        Dim i = 0
        For Each Path In moviePaths
            i += 1
            PercentDone = CalcPercentDone(i, moviePaths.Count)
            ReportProgress("Scanning folder " & i & " of " & moviePaths.Count)

            mov_ListFiles(pattern, New DirectoryInfo(Path))
            If Cancelled Then Exit Sub
        Next
    End Sub


    Private Sub mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo)

        Dim nfoFunction As New WorkingWithNfoFiles

        Dim workingMovie

        For Each oFileInfo In dirInfo.GetFiles(pattern)
            Application.DoEvents()

            If Not File.Exists(oFileInfo.FullName) Then Continue For

            workingMovie = nfoFunction.mov_NfoLoadBasic(oFileInfo.FullName, "movielist")

            If workingMovie.title = "Error" Then Continue For

            If workingMovie.movieset <> Nothing Then
                If workingMovie.movieset.IndexOf(" / ") = -1 Then
                    Dim add As Boolean = True
                    For Each item In Preferences.moviesets
                        If item = workingMovie.movieset Then
                            add = False
                            Exit For
                        End If
                    Next
                    If add Then
                        Preferences.moviesets.Add(workingMovie.movieset)
                    End If
                Else
                    Dim strArr() As String
                    strArr = workingMovie.movieset.Split("/")
                    For count = 0 To strArr.Length - 1
                        strArr(count) = strArr(count).Trim
                        Dim add As Boolean = True
                        For Each item In Preferences.moviesets
                            If item = strArr(count) Then
                                add = False
                                Exit For
                            End If
                        Next
                        If add Then
                            Preferences.moviesets.Add(strArr(count))
                        End If
                    Next
                End If
            End If

            workingMovie.foldername = Utilities.GetLastFolder(workingMovie.fullpathandfilename)
            If workingMovie.genre.IndexOf("skipthisfile") = -1 Then
                Dim skip As Boolean = False
                For Each movie In MovieCache
                    If movie.fullpathandfilename = workingMovie.fullpathandfilename Then
                        skip = True
                        Exit For
                    End If
                Next
                If Not skip Then
                    Dim completebyte1 As Byte = 0
                    Dim fanartexists As Boolean = IO.File.Exists(Preferences.GetFanartPath(workingMovie.fullpathandfilename))
                    Dim posterexists As Boolean = IO.File.Exists(Preferences.GetPosterPath(workingMovie.fullpathandfilename))
                    If fanartexists = False Then
                        completebyte1 += 1
                    End If
                    If posterexists = False Then
                        completebyte1 += 2
                    End If
                    workingMovie.missingdata1 = completebyte1
                    MovieCache.Add(workingMovie)
                    Data_GridViewMovieCache.Add(New Data_GridViewMovie(workingMovie))
                End If
            End If
        Next
    End Sub


    Sub LoadActorCache()
        _actorDb.Clear()

        Dim actorlist As New XmlDocument

        actorlist.Load(Preferences.workingProfile.actorcache)

        Dim thisresult As XmlNode = Nothing

        For Each thisresult In actorlist("actor_cache")
            Select Case thisresult.Name
                Case "actor"

                    Dim name = ""
                    Dim movieId = ""
                    Dim detail As XmlNode = Nothing

                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            Case "name"
                                name = detail.InnerText
                            Case "id"
                                movieId = detail.InnerText
                        End Select
                    Next

                    actorDB.Add(New ActorDatabase(name, movieId))
            End Select
        Next
    End Sub


    Sub SaveActorCache()
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)

        Dim root As XmlElement
        Dim child As XmlElement

        root = doc.CreateElement("actor_cache")

        Dim childchild As XmlElement

        For Each actor In _actorDB
            child = doc.CreateElement("actor")
            childchild = doc.CreateElement("name")
            childchild.InnerText = actor.actorname
            child.AppendChild(childchild)
            childchild = doc.CreateElement("id")
            childchild.InnerText = actor.movieid
            child.AppendChild(childchild)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(Preferences.workingProfile.actorcache, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub


    Public Sub RebuildCaches()
        RebuildMovieCache()
        If Cancelled Then Exit Sub
        RebuildActorCache()
    End Sub


    Public Sub RebuildMovieCache()
        LoadMovieCacheFromNfos()
        If Cancelled Then Exit Sub
        SaveMovieCache()
    End Sub


    Public Sub RebuildActorCache()
        'FixUpCorruptActors
        _actorDB.Clear()

        Dim i = 0

        For Each movie In MovieCache
            i += 1
            PercentDone = CalcPercentDone(i, MovieCache.Count)
            ReportProgress("Rebuilding actor cache " & i & " of " & MovieCache.Count)

            Dim m As New Movie(movie.fullpathandfilename, Me)

            m.LoadNFO()
            m.UpdateActorCacheFromEmpty()
            If Cancelled Then Exit Sub
        Next

        If Cancelled Then Exit Sub

        Dim q = From item In _tmpActorDb Select item.ActorName, item.MovieId

        For Each item In q.Distinct()
            _actorDb.Add(New ActorDatabase(item.ActorName, item.MovieId))
        Next

        SaveActorCache()
    End Sub

End Class
