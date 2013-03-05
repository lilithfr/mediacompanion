Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Media_Companion

Module Ext
    <System.Runtime.CompilerServices.Extension()> _
    Public Sub AppendChild(root As XmlElement, doc As XmlDocument, name As String, value As String)

        Dim child As XmlElement = doc.CreateElement(name)

        child.InnerText = value
        root.AppendChild(child)
    End Sub
End Module

Public Class Movies

    Public Event AmountDownloadedChanged (ByVal iNewProgress As Long)
    Public Event FileDownloadSizeObtained(ByVal iFileSize    As Long)
    Public Event FileDownloadComplete    ()
    Public Event FileDownloadFailed      (ByVal ex As Exception)

    Private _actorDb As New List(Of ActorDatabase)
    Public _tmpActorDb As New List(Of ActorDatabase)

    Public Property Bw            As BackgroundWorker = Nothing
    Public Property MovieCache    As New List(Of ComboList)
    Public Property TmpMovieCache As New List(Of ComboList)
    
    Public Property NewMovies     As New List(Of Movie)
    Public Property PercentDone   As Integer = 0

    Private _data_GridViewMovieCache As New List(Of Data_GridViewMovie)

    Public ReadOnly Property Data_GridViewMovieCache As List(Of Data_GridViewMovie)
        Get
            Return _data_GridViewMovieCache
        End Get
    End Property


    'Public ReadOnly Property Genres_old As List(Of String)
    '    Get
    '        Dim q = From x In MovieCache Select ms=x.genre.Split(" / ") Distinct
             
    '        Dim lst = q.SelectMany(Function(m) m).Distinct.OrderBy(Function(m) m).ToList

    '        lst.RemoveAll(Function(m) m="" )
    '        lst.RemoveAll(Function(m) m="/")

    '        Return lst
    '    End Get
    'End Property    


    Public ReadOnly Property Genres As List(Of String)
        Get
            Dim q = From x In MovieCache Select ms=x.genre.Split(" / ")
             
            Dim lst = q.SelectMany(Function(m) m).ToList

            lst.RemoveAll(Function(v) v="" )
            lst.RemoveAll(Function(v) v="/")

            Dim q2 = From x In lst
                        Group By x Into Num=Count
                        Order By x
                        Select x & " (" & Num.ToString & ")" 


            Return q2.AsEnumerable.ToList
        End Get
    End Property    


    Public ReadOnly Property ActorsFilter As List(Of String)
        Get
            Dim q = From x In ActorDb 
                Group By x.ActorName Into NumFilms=Count 
                Where NumFilms>=Preferences.ActorsFilterMinFilms 
            
            If Preferences.MovieFilters_Actors_Order=0 Then 
                q = From x In q Order by x.NumFilms  Descending, x.ActorName Ascending
            Else
                q = From x In q Order by x.ActorName Ascending , x.NumFilms  Descending
            End If

            Dim r = From x In q Select x.ActorName & " (" & x.NumFilms.ToString & ")" Take Preferences.MaxActorsInFilter

            Return r.ToList
        End Get
    End Property    


    Public ReadOnly Property ResolutionFilter As List(Of String)
        Get
            Dim q = From x In MovieCache 
                Group By x.Resolution Into NumFilms=Count 
                Order by Resolution Descending

            Dim r = (From x In q Select x.Resolution & " (" & x.NumFilms.ToString & ")").ToList

            If r.Count>0 Then r.Item(r.Count-1) = r.Item(r.Count-1).Replace("-1","Unknown")

            Return r
        End Get
    End Property    


    'Public ReadOnly Property ActorsByNumberOfFilmsDescending As List(Of String)
    '    Get
    '        Dim q = From x In ActorDb 
    '            Group By x.ActorName Into NumFilms=Count 
    '            Order by NumFilms Descending, ActorName 
    '            Take Preferences.MaxActorsInFilter 
    '            Where NumFilms>=Preferences.ActorsFilterMinFilms 
    '            Select ActorName & " (" & NumFilms.ToString & ")" 

    '        Return q.ToList
    '    End Get
    'End Property    


    Public ReadOnly Property SetsFilter As List(Of String)
        Get
            Dim q = From x In MovieCache 
                Group By x.MovieSet Into NumFilms=Count
                Where NumFilms>=Preferences.SetsFilterMinFilms 

            If Preferences.MovieFilters_Sets_Order=0 Then 
                q = From x In q Order by x.NumFilms Descending, x.MovieSet Ascending
            Else
                q = From x In q Order by x.MovieSet.Replace("-None-","") Ascending , x.NumFilms Descending
            End If

            Dim r = From x In q Select( x.MovieSet & " (" & x.NumFilms.ToString & ")" ) Take Preferences.MaxSetsInFilter 

            Return r.ToList
        End Get
    End Property    


    'Public ReadOnly Property MoviesSetsByNumberOfFilmsDescending As List(Of String)
    '    Get
    '        Dim q = From x In MovieCache 
    '            Group By x.MovieSet Into NumFilms=Count
    '            Order By NumFilms Descending, MovieSet
    '            Select MovieSet & " (" & NumFilms.ToString & ")" 
             
    '        Return q.ToList
    '    End Get
    'End Property    



    Public ReadOnly Property MoviesSetsIncNone As List(Of String)
        Get
            Try
                Dim q = From x In MovieCache Select ms=x.MovieSet.Split(",") Distinct
             
                Return q.SelectMany(Function(m) m).Distinct.OrderBy(Function(m) m).ToList
            Catch
                Return New List(Of String)
            End Try
        End Get
    End Property    

 
    Public ReadOnly Property MoviesSetsExNone As List(Of String)
        Get
            Dim x = MoviesSetsIncNone

            x.Remove("")
            x.Remove("-None-")

            Return x
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
            Try
                _bw.ReportProgress(PercentDone, oProgress)
            Catch
            End Try
        End If
    End Sub

    Public Function FindCachedMovie(fullpathandfilename As String) As ComboList

        Dim q = From m In _movieCache Where m.fullpathandfilename=fullpathandfilename

        Return q.Single
    End Function

    Public Function LoadMovie(fullpathandfilename As String) As Movie

        Dim movie = New Movie(Utilities.GetFileName(fullpathandfilename),Me)

        If IsNothing(movie) Then Return Nothing
        
        movie.LoadNFO

        Return movie
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

                Try
                    For Each subfolder In Utilities.EnumerateFolders(moviefolder)       'Max levels restriction of 6 deep removed
                        folders.Add(subfolder)
                    Next
                Catch ex As Exception
                    ExceptionHandler.LogError(ex,"LastRootPath: [" & Utilities.LastRootPath & "]")
                End Try
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
        movie.Scraped=False
        movie.Scrape
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub ChangeMovie(NfoPathAndFilename As String, imdb As String)

        Dim movie = New Movie(Utilities.GetFileName(NfoPathAndFilename),Me)

        movie.DeleteScrapedFiles(True)

        movie.ScrapedMovie.Init

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.Scrape(imdb)
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub RescrapeSpecificMovie(fullpathandfilename As String,rl As RescrapeList)

        Dim movie = New Movie(Utilities.GetFileName(fullpathandfilename),Me)

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.RescrapeSpecific  ( rl    )
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub BatchRescrapeSpecific(NfoFilenames As List(Of String), rl As RescrapeList)
        Dim i=0
        For Each item In NfoFilenames
            i += 1
            PercentDone = CalcPercentDone(i,NfoFilenames.Count)
            ReportProgress("Batch Rescraping " & i & " of " & NfoFilenames.Count & " ")

            Dim movie = New Movie(Utilities.GetFileName(item),Me)

            AddMovieEventHandlers   ( movie )
            movie.Scraped=False
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
            ReportProgress("Rescraping " & i & " of " & NfoFilenames.Count & " ")
            
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
            ReportProgress("Rescraping '" & CapsFirstLetter(_rescrapeList.Field.Replace("_"," ")) & "' " & i & " of " & _rescrapeList.FullPathAndFilenames.Count & " ")
            RescrapeSpecificMovie(FullPathAndFilename,rl)

            If Cancelled then Exit Sub
        Next
    End Sub

    Function CapsFirstLetter(words As String)
        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words)
    End Function


    Sub RescrapeMovie(NfoFilename as String)
        Dim movie = New Movie(Utilities.GetFileName(NfoFilename),Me)

        'movie.DeleteScrapedFiles
        movie.Rescrape=True

        ScrapeMovie(movie)
    End Sub



    Function CalcPercentDone(onNumber As Integer, total As Integer) As Integer
        Return ((100 /total) * onNumber)
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
                                newmovie.MovieSet = detail.InnerText
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
                            'Case "titleandyear"
                            '    '--------- aqui
                            '    Dim TempString2 As String = detail.InnerText
                            '    If Preferences.ignorearticle = True Then
                            '        If TempString2.ToLower.IndexOf("the ") = 0 Then
                            '            Dim Temp As String = TempString2.Substring(TempString2.Length - 7, 7)
                            '            TempString2 = TempString2.Substring(4, TempString2.Length - 11)
                            '            TempString2 = TempString2 & ", The" & Temp
                            '        End If
                            '    End If

                            '    newmovie.titleandyear = TempString2
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

                            Case "Resolution" : newmovie.Resolution = detail.InnerText
                        End Select
                    Next
                    If newmovie.source = Nothing Then
                        newmovie.source = ""
                    End If
                    If newmovie.MovieSet = Nothing Then
                        newmovie.MovieSet = "-None-"
                    End If
                    If newmovie.MovieSet = "" Then
                        newmovie.MovieSet = "-None-"
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
            If movie.MovieSet <> Nothing Then
                If movie.MovieSet <> "" Or movie.MovieSet <> "-None-" Then
                    childchild = doc.CreateElement("set")
                    childchild.InnerText = movie.MovieSet
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

'            childchild = doc.CreateElement("titleandyear")
'            Try
'                If movie.titleandyear.Length >= 5 Then
'                    If movie.titleandyear.ToLower.IndexOf(", the") = movie.titleandyear.Length - 5 Then
'                        Dim Temp As String = movie.titleandyear.Replace(", the", String.Empty)
'                        movie.titleandyear = "The " & Temp
'                    End If
'                End If
'            Catch ex As Exception
'#If SilentErrorScream Then
'                Throw ex
'#End If
'            End Try
'            childchild.InnerText = movie.titleandyear
'            child.AppendChild(childchild)


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

            child.AppendChild(doc, "Resolution", movie.Resolution)     

            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(cacheFile, System.Text.Encoding.UTF8)

        output.Formatting = Xml.Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub


    Public Sub LoadMovieCacheFromNfos
'        MovieCache.Clear
        TmpMovieCache.Clear

        Dim t As New List(Of String)

        t.AddRange(Preferences.movieFolders)
        t.AddRange(Preferences.offlinefolders)

        ReportProgress("Searching movie folders...")
        mov_NfoLoad(t)

        If Cancelled Then Exit Sub

        If Not Preferences.usefoldernames Then
    '       For Each movie In MovieCache
            For Each movie In TmpMovieCache
                If movie.filename <> Nothing Then movie.filename = movie.filename.Replace(".nfo", "")
            Next
        End If

        'No duplicates found...
        'Dim q = From item In TmpMovieCache Group by item.fullpathandfilename Into Group Select Group

        'For Each item In q
        '    MovieCache.Add(item(0))
        'Next

        MovieCache.Clear
        MovieCache.AddRange(TmpMovieCache)
        Rebuild_Data_GridViewMovieCache()
    End Sub


    Property TotalNumberOfFolders As Integer
    Property NumberOfFoldersDone  As Integer
    Property BWs As New List(Of BackgroundWorker)
    Property NumActiveThreads     As Integer

    Public Sub MT_LoadMovieCacheFromNfos

        TmpMovieCache.Clear
        TotalNumberOfFolders=0
        NumberOfFoldersDone =0

        BWs.Clear

        Dim RootMovieFolders As New List(Of String)

        RootMovieFolders.AddRange(Preferences.movieFolders)
        RootMovieFolders.AddRange(Preferences.offlinefolders)

        ReportProgress("Searching movie folders...")

        For each item In RootMovieFolders

            Dim bw As BackgroundWorker = New BackgroundWorker

            bw.WorkerReportsProgress      = True
            bw.WorkerSupportsCancellation = True

            AddHandler bw.DoWork            , AddressOf bw_DoWork
            AddHandler bw.ProgressChanged   , AddressOf bw_ProgressChanged
            AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted

            BWs.Add(bw)
            NumActiveThreads += 1

            bw.RunWorkerAsync(item)
        Next

        Dim Cancelling As Boolean = False
        Dim Busy       As Boolean = True

        While Busy
            Threading.Thread.Sleep(250)

            If Cancelled And Not Cancelling Then 
                Cancelling = True
                For each item As BackgroundWorker in BWs
                    Try
                        item.CancelAsync
                    Catch
                    End Try
                Next
            End If
            

            Busy = False

            For each item As BackgroundWorker in BWs
                Try
                    Busy = Busy Or item.IsBusy
                Catch
                End Try
            Next

        End While
      
        If Cancelled Then Exit Sub

        MovieCache.Clear
        MovieCache.AddRange(TmpMovieCache)

        Rebuild_Data_GridViewMovieCache
    End Sub


    Sub bw_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) 

        Dim bw     As BackgroundWorker = CType(sender, BackgroundWorker)
        Dim folder As String = DirectCast(e.Argument, String)
        Dim Cache  As New List(Of ComboList)

        MT_mov_NfoLoad(bw,folder,Cache)

        e.Result = Cache
    End Sub


    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)

        Dim mp As MovieProgress = CType(e.UserState, MovieProgress)

        Select mp.ProgressEvent

            Case MovieProgress.MsgType.GotFoldersCount : TotalNumberOfFolders += mp.Data
                                                         ReportProgress("Total number of folders : [" & TotalNumberOfFolders & "]")

            Case MovieProgress.MsgType.NextOne         : NumberOfFoldersDone += 1
                                                         PercentDone = CalcPercentDone(NumberOfFoldersDone, TotalNumberOfFolders)
                                                         ReportProgress("Active threads : [" & NumActiveThreads & "] - Scanning folder " & NumberOfFoldersDone & " of " & TotalNumberOfFolders)

        End Select

    End Sub


    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

        If IsNothing(e.Result) Then Exit Sub

        Dim Cache As List(Of ComboList) = CType(e.Result, List(Of ComboList))
 
        TmpMovieCache.AddRange(Cache) 
        NumActiveThreads -= 1
    End Sub



    Private Sub MT_mov_NfoLoad(bw As BackgroundWorker, folder As String,Cache As List(Of ComboList))

        Const pattern = "*.nfo"
        
        Dim moviePaths As New List(Of String)

        If Not (New DirectoryInfo(folder)).Exists Then Exit Sub

        moviePaths.Add(folder)

        'Add sub-folders
        Try
            For Each subfolder In Utilities.EnumerateFolders(moviePaths(0))
                moviePaths.Add(subfolder)
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex,"LastRootPath: [" & Utilities.LastRootPath & "]")
        End Try

        bw.ReportProgress( -1, New MovieProgress(MovieProgress.MsgType.GotFoldersCount,moviePaths.Count) )


        For Each Path In moviePaths

            bw.ReportProgress( -1, New MovieProgress(MovieProgress.MsgType.NextOne,Nothing) )

            MT_mov_ListFiles( pattern, New DirectoryInfo(Path), Cache )
            If Cancelled Then Exit Sub
        Next


        If Not Preferences.usefoldernames Then
            For Each movie In Cache
                If movie.filename <> Nothing Then movie.filename = movie.filename.Replace(".nfo", "")
            Next
        End If
    End Sub



    Private Sub MT_mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo, Cache As List(Of ComboList))

        Dim nfoFunction  As New WorkingWithNfoFiles
        Dim workingMovie As ComboList

        For Each oFileInfo In dirInfo.GetFiles(pattern)
            Application.DoEvents()

            If Cancelled Then Exit Sub

            If Not File.Exists(oFileInfo.FullName) Then Continue For

            workingMovie = nfoFunction.mov_NfoLoadBasic(oFileInfo.FullName, "movielist")

            If workingMovie.title = "Error"                    Then Continue For
            If workingMovie.genre.IndexOf("skipthisfile") > -1 Then Continue For

            workingMovie.foldername   = Utilities  .GetLastFolder (workingMovie.fullpathandfilename)
            workingMovie.missingdata1 = Preferences.GetMissingData(workingMovie.fullpathandfilename)

            Cache.Add(workingMovie)
        Next
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
            Try
                For Each subfolder In Utilities.EnumerateFolders(moviePaths(f))
                    moviePaths.Add(subfolder)
                Next
            Catch ex As Exception
                ExceptionHandler.LogError(ex,"LastRootPath: [" & Utilities.LastRootPath & "]")
            End Try
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

        Dim workingMovie As ComboList

        For Each oFileInfo In dirInfo.GetFiles(pattern)
            Application.DoEvents()

            If Cancelled Then Exit Sub

            If Not File.Exists(oFileInfo.FullName) Then Continue For

            workingMovie = nfoFunction.mov_NfoLoadBasic(oFileInfo.FullName, "movielist")

            If workingMovie.title = "Error" Then Continue For

            workingMovie.foldername = Utilities.GetLastFolder(workingMovie.fullpathandfilename)

            If workingMovie.genre.IndexOf("skipthisfile") = -1 Then

                workingMovie.missingdata1 = Preferences.GetMissingData(workingMovie.fullpathandfilename)

                TmpMovieCache.Add(workingMovie)
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


    Public Sub RebuildCaches
        RebuildMovieCache
        If Cancelled Then Exit Sub
        RebuildActorCache
    End Sub


    Public Sub RebuildMovieCache
        
        If Preferences.UseMultipleThreads Then
            MT_LoadMovieCacheFromNfos
        Else
            LoadMovieCacheFromNfos
        End If


        If Cancelled Then 
            Exit Sub
        End If

        SaveMovieCache
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

            m.LoadNFO(False)
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

 
    Sub RemoveMovieFromCache(fullpathandfilename)

        If fullpathandfilename = "" Then Exit Sub

        MovieCache             .RemoveAll(Function(c) c.fullpathandfilename = fullpathandfilename)
        Data_GridViewMovieCache.RemoveAll(Function(c) c.fullpathandfilename = fullpathandfilename)
    End Sub


End Class
