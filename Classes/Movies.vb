Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Media_Companion
Imports MC_UserControls


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

    Private _certificateMappings  As CertificateMappings
    Private _actorDb              As New List(Of ActorDatabase)
    Public _tmpActorDb            As New List(Of ActorDatabase)
    Public Shared movRebuildCaches         As Boolean = False

    Public Property Bw            As BackgroundWorker = Nothing
    Public Property MovieCache    As New List(Of ComboList)
    Public Property TmpMovieCache As New List(Of ComboList)
    
    Public Property NewMovies     As New List(Of Movie)
    Public Property PercentDone   As Integer = 0

    Private _data_GridViewMovieCache As New List(Of Data_GridViewMovie)


    Public ReadOnly Property CertificateMappings As CertificateMappings
        Get
            If IsNothing(_certificateMappings) Then
                _certificateMappings = New CertificateMappings 
            End If

            Return _certificateMappings
        End Get
    End Property


    Public ReadOnly Property Data_GridViewMovieCache As List(Of Data_GridViewMovie)
        Get
            Return _data_GridViewMovieCache
        End Get
    End Property


    Public ReadOnly Property CertificatesFilter As List(Of String)
        Get
            Dim q = From x In MovieCache Select field=CertificateMappings.GetMapping(x.Certificate)
                        Group By field Into Num=Count
                        Order By field
                        Select If(field="","Missing",field) & " (" & Num.ToString & ")" 

            Return q.AsEnumerable.ToList
        End Get
    End Property    



    Public ReadOnly Property MoviesWithUniqueMovieTitles As List(Of ComboList)
        Get
            Dim q = From x In MovieCache 
                    Join
                       u In UniqueMovieTitles On u Equals x.Title
                    Select
                        x

            Return q.AsEnumerable.ToList
        End Get
    End Property    


    Public ReadOnly Property UniqueMovieTitles As List(Of String)
        Get
            Dim q = From x In MovieCache 
                    Select 
                        x.Title
                    Group By 
                        Title Into Num=Count
                    Select 
                        Title,Num
                    Where
                        Num = 1
                    Select
                        Title

            Return q.AsEnumerable.ToList
        End Get
    End Property    



    Public ReadOnly Property GenresFilter As List(Of String)
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


    Public ReadOnly Property MinVotes As Integer
        Get
            If MovieCache.Count=0 Then Return 0

            Dim q = Aggregate m In MovieCache Into Min(m.Votes)

            Return q
        End Get
    End Property    

    Public ReadOnly Property ListVotes As List(Of Integer)
        Get
            Dim q = From m In MovieCache Select m.Votes

            Return q.AsEnumerable.ToList
        End Get
    End Property    


    Public ReadOnly Property MaxVotes As Integer
        Get
            If MovieCache.Count=0 Then Return 0

            Dim q = Aggregate m In MovieCache Into Max(m.Votes)

            Return q
        End Get
    End Property    


    Public ReadOnly Property MinYear As Integer
        Get
            If MovieCache.Count=0 Then Return 0

            Dim q = Aggregate m In MovieCache Into Min(m.Year)

            Return q
        End Get
    End Property    


    Public ReadOnly Property MaxYear As Integer
        Get
            If MovieCache.Count=0 Then Return 0

            Dim q = Aggregate m In MovieCache Into Max(m.Year)

            Return q
        End Get
    End Property    



    Public ReadOnly Property GeneralFilters As List(Of String)
        Get
            Dim lst As List(Of String) = New List(Of String)

            lst.Add( "All"                    )
            lst.Add( Watched                  )
            lst.Add( Unwatched                )
            lst.Add( Duplicates               )
            If Not Preferences.DisableNotMatchingRenamePattern Then
                lst.Add( NotMatchingRenamePattern )
            End If
            lst.Add( MissingCertificate       )
            lst.Add( MissingFanart            )
            lst.Add( MissingGenre             )
            lst.Add( MissingLocalActors       )
            lst.Add( MissingOutline           )
            lst.Add( MissingPlot              )
            lst.Add( MissingPoster            )
            lst.Add( MissingRating            )
            lst.Add( MissingRuntime           )
            lst.Add( MissingTrailer           )
            lst.Add( MissingVotes             )
            lst.Add( MissingYear              )

            Return lst
        End Get
    End Property    

    Public ReadOnly Property NotMatchingRenamePattern As String
        Get
            Return "Not matching rename pattern (" & (From x In MovieCache Where Not x.ActualNfoFileNameMatchesDesired).Count & ")" 
        End Get
    End Property    


    Public ReadOnly Property MissingCertificate As String
        Get
            Return "Missing Certificate (" & (From x In MovieCache Where x.MissingCertificate).Count & ")" 
        End Get
    End Property    


    Public ReadOnly Property MissingFanart As String
        Get
            Return "Missing Fanart (" & (From x In MovieCache Where x.MissingFanart).Count & ")" 
        End Get
    End Property    


    Public ReadOnly Property MissingTrailer As String
        Get
            Return "Missing Trailer (" & (From x In MovieCache Where x.MissingTrailer).Count & ")" 
        End Get
    End Property  

    Public ReadOnly Property MissingLocalActors As String
        Get
            Return "Missing Local Actors (" & (From x In MovieCache Where x.MissingLocalActors).Count & ")" 
        End Get
    End Property    


    Public ReadOnly Property MissingPoster As String
        Get
            Return "Missing Poster (" & (From x In MovieCache Where x.MissingPoster).Count & ")" 
        End Get
    End Property    
     

    Public ReadOnly Property MissingPlot As String
        Get
            Return "Missing Plot (" & (From x In MovieCache Where x.MissingPlot).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property MissingRating As String
        Get
            Return "Missing Rating (" & (From x In MovieCache Where x.MissingRating).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property MissingGenre As String
        Get
            Return "Missing Genre (" & (From x In MovieCache Where x.MissingGenre).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property MissingOutline As String
        Get
            Return "Missing Outline (" & (From x In MovieCache Where x.MissingOutline).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property MissingRuntime As String
        Get
            Return "Missing Runtime (" & (From x In MovieCache Where x.MissingRuntime).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property MissingVotes As String
        Get
            Return "Missing Votes (" & (From x In MovieCache Where x.MissingVotes).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property MissingYear As String
        Get
            Return "Missing Year (" & (From x In MovieCache Where x.MissingYear).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property Duplicates As String
        Get
            Dim total          As Integer = (From x In MovieCache).Count
            Dim total_distinct As Integer = (From x In MovieCache Select x.id).Distinct.Count

            Dim num_duplicates = total - total_distinct

            Return "Duplicates (" & num_duplicates & ")"
        End Get
    End Property    


    Public ReadOnly Property Watched As String
        Get
            Return "Watched (" & (From x In MovieCache Where x.Watched).Count & ")" 
        End Get
    End Property  


    Public ReadOnly Property Unwatched As String
        Get
            Return "Unwatched (" & (From x In MovieCache Where Not x.Watched).Count & ")" 
        End Get
    End Property    


    Public ReadOnly Property ActorsFilter_Preferences As IEnumerable(Of String)
        Get
            Dim q = From x In ActorDb 
                Group By 
                    x.ActorName Into NumFilms=Count 
                Where 
                    NumFilms>=Preferences.ActorsFilterMinFilms
            
            If Preferences.MovieFilters_Actors_Order=0 Then 
                q = From x In q Order by x.NumFilms  Descending, x.ActorName Ascending
            Else
                q = From x In q Order by x.ActorName Ascending , x.NumFilms  Descending
            End If

            Return From x In q Select x.ActorName & " (" & x.NumFilms.ToString & ")" Take Preferences.MaxActorsInFilter
        End Get
    End Property    



    Public ReadOnly Property ActorsFilter As List(Of String)
        Get
            Dim r = (From x In ActorsFilter_Preferences).Union(From x In ActorsFilter_Extras) 
            Return r.ToList
        End Get
    End Property    


    Public ReadOnly Property ActorsFilter_Extras As IEnumerable(Of String)
        Get
            Dim q = From x In ActorDb 
                Group By 
                    x.ActorName Into NumFilms=Count 
                Where 
                    ActorsFilter_AlsoInclude.Contains(ActorName)
            
            Return From x In q Select x.ActorName & " (" & x.NumFilms.ToString & ")"
        End Get
    End Property    

    Property ActorsFilter_AlsoInclude As New List(Of String)

    Sub ActorsFilter_AddIfMissing(actorName As String)

        If Not ActorsFilter.Contains(actorName) Then
            ActorsFilter_AlsoInclude.Add(actorName)
        End If
    End Sub

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


    Public ReadOnly Property NumAudioTracksFilter As List(Of String)
        Get
            Dim q = From m In MovieCache 
                Group By NumTracks=m.Audio.Count Into NumFilms=Count 
                Order By NumTracks

            Dim r = (From x In q Select x.NumTracks & " (" & x.NumFilms.ToString & ")").ToList

            Return r
        End Get
    End Property    


    Public ReadOnly Property AudioLanguagesFilter As List(Of String)
        Get
            Dim leftOuterJoinTable As IEnumerable = From m In MovieCache From a In m.Audio Select m.fullpathandfilename, field=If(a.Language.Value="","Unknown",a.Language.Value)

            Return QryMovieCache(leftOuterJoinTable)
        End Get
    End Property    


    Public ReadOnly Property AudioChannelsFilter As List(Of String)
        Get
            Dim leftOuterJoinTable = From m In MovieCache From a In m.Audio Select m.fullpathandfilename, field=If(a.Channels.Value="","Unknown",a.Channels.Value)

            Return QryMovieCache(leftOuterJoinTable)
        End Get
    End Property    


    Public ReadOnly Property AudioBitratesFilter As List(Of String)
        Get
            Dim leftOuterJoinTable = From m In MovieCache From a In m.Audio Select m.fullpathandfilename, field=If(a.Bitrate.Value="","Unknown",a.Bitrate.Value)

            Return QryMovieCache(leftOuterJoinTable)
        End Get
    End Property    


    Public ReadOnly Property AudioCodecsFilter As List(Of String)
        Get
            Dim leftOuterJoinTable = From m In MovieCache From a In m.Audio Select m.fullpathandfilename, field=If(a.Codec.Value="","Unknown",a.Codec.Value)

            Return QryMovieCache(leftOuterJoinTable)
        End Get
    End Property    

    Private Function QryMovieCache(leftOuterJoinTable As IEnumerable) As List(Of String)

        Dim q = From m In MovieCache
                    Group Join 
                        a In leftOuterJoinTable On a.fullpathandfilename Equals m.fullpathandfilename
                    Into
                        ResultList = Group
                    From
                        result In ResultList.DefaultIfEmpty
                    Select 
                        field=If( result Is Nothing, "Unassigned", result.field )
                    Group By 
                        field Into Num=Count
                    Order By 
                        field
                    Select 
                        aString = field & " (" & Num.ToString & ")"

        Return q.Cast(Of String)().ToList
    End Function


    Public ReadOnly Property SetsFilter_Extras As IEnumerable(Of String)
        Get
            Dim q = From x In MovieCache 
                Group By 
                    x.MovieSet Into NumFilms=Count
                Where 
                    SetsFilter_AlsoInclude.Contains(MovieSet)
            
            Return From x In q Select x.MovieSet & " (" & x.NumFilms.ToString & ")"
        End Get
    End Property    

    Property SetsFilter_AlsoInclude As New List(Of String)

    Sub SetsFilter_AddIfMissing(name As String)

        If Not SetsFilter.Contains(name) Then
            SetsFilter_AlsoInclude.Add(name)
        End If
    End Sub


    Public ReadOnly Property SetsFilter_Preferences As IEnumerable(Of String)
        Get
            Dim q = From x In MovieCache 
                Group By 
                    x.MovieSet Into NumFilms=Count
                Where 
                    NumFilms>=Preferences.SetsFilterMinFilms 

            If Preferences.MovieFilters_Sets_Order=0 Then 
                q = From x In q Order by x.NumFilms Descending, x.MovieSet Ascending
            Else
                q = From x In q Order by x.MovieSet.Replace("-None-","") Ascending , x.NumFilms Descending
            End If

            Return From x In q Select x.MovieSet & " (" & x.NumFilms.ToString & ")" Take Preferences.MaxSetsInFilter 
        End Get
    End Property    



    Public ReadOnly Property SetsFilter As List(Of String)
        Get
            Dim r = (From x In SetsFilter_Preferences).Union(From x In SetsFilter_Extras) 
            Return r.ToList
        End Get
    End Property    



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

    Public Function FindData_GridViewCachedMovie(fullpathandfilename As String) As Data_GridViewMovie
        
        Dim q = From m In _data_GridViewMovieCache Where m.fullpathandfilename=fullpathandfilename
        
        Return q.Single        
    End Function


    Public Function LoadMovie(fullpathandfilename As String, Optional ByVal Cacheupdate As Boolean = True) As Movie

'       Dim movie = New Movie(Utilities.GetFileName(fullpathandfilename,True),Me)
        Dim movie = New Movie(Me,fullpathandfilename)

        If IsNothing(movie) Then Return Nothing
        
        movie.LoadNFO(Cacheupdate)

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

    Public Sub AddNewMovies(DirPath As String)   'Search for valid video file

        If Preferences.ExcludeFolders.Match(DirPath) Then 
            ReportProgress(,"Skipping excluded folder [" & DirPath & "] from scrape." & vbCrLf)
            Return
        End If

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
            msg="!!! Validating file " & fileInfo.Name & "(" & i & " of " & files.Count & ")"
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
            ReportProgress(,vbCrLf & vbCrLf & "!!! A total of " & NewMovies.Count & " new movie" & If(NewMovies.Count=1,"","s") & " found -> Starting Main Scraper Process..." & vbCrLf & vbCrLf )
        Else
            ReportProgress(vbCrLf & vbCrLf & "No new movies found" & vbCrLf & vbCrLf,vbCrLf & vbCrLf & "!!! No new movies found" & vbCrLf & vbCrLf)
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

        ReportProgress( ,"!!! " & vbCrLf & "!!! Finished" )
    End Sub

    Sub ScrapeMovie(movie As Movie)
        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.Scrape
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub ChangeMovie(NfoPathAndFilename As String, imdb As String)

  '     Dim movie = New Movie(Utilities.GetFileName(NfoPathAndFilename,True),Me)
        Dim movie = New Movie(Me,NfoPathAndFilename)

        movie.DeleteScrapedFiles(True)

        movie.ScrapedMovie.Init

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.Scrape(imdb)
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub RescrapeSpecificMovie(fullpathandfilename As String,rl As RescrapeList)

'       Dim movie = New Movie(Utilities.GetFileName(fullpathandfilename,True),Me)
        Dim movie = New Movie(Me,fullpathandfilename)

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.RescrapeSpecific  ( rl    )
        RemoveMovieEventHandlers( movie )
    End Sub


    Sub BatchRescrapeSpecific(NfoFilenames As List(Of String), rl As RescrapeList)
        Dim i=0
        Dim NfoFileList As New List(Of String)
        
        For Each item In NfoFilenames
            If IO.File.Exists(item) Then
                NfoFileList.Add(item)
            Else
                ReportProgress("Could Not find " & item & vbCrLf & "Please Refresh All Movies before running Batch Rescraper Wizard" )
            End If
        Next

        If NfoFilenames.count <> NfoFileList.count Then NfoFilenames = NfoFileList
        For Each item In NfoFilenames
            i += 1
            PercentDone = CalcPercentDone(i,NfoFilenames.Count)

'           Dim movie = New Movie(Utilities.GetFileName(item,True),Me)
            Dim movie = New Movie(Me,item)

            AddMovieEventHandlers   ( movie )

            ReportProgress("Batch Rescraping " & i & " of " & NfoFilenames.Count & " [" & movie.Title & "] ")


            movie.Scraped=False
            movie.RescrapeSpecific  ( rl    )
            RemoveMovieEventHandlers( movie )

            If Cancelled then Exit For
        Next
        SaveCaches
    End Sub


    Sub RescrapeAll( NfoFilenames As List(Of String) )
        Dim i=0
        ReportProgress(,"!!! Rescraping all data for:" & vbCrLf & vbCrLf )
        For Each NfoFilename In NfoFilenames
            i += 1
            PercentDone = CalcPercentDone(i,NfoFilenames.Count)
            ReportProgress("Rescraping " & i & " of " & NfoFilenames.Count & " ")
            
            RescrapeMovie(NfoFilename)

            If Cancelled then Exit For
        Next
        SaveCaches
        ReportProgress(,"!!! " & vbCrLf & "!!! Finished")
    End Sub


    Sub RescrapeSpecific( _rescrapeList As RescrapeSpecificParams )
        Dim rl As new RescrapeList(_rescrapeList.Field)

        Dim i=0
        For Each FullPathAndFilename In _rescrapeList.FullPathAndFilenames
            i += 1
            PercentDone = CalcPercentDone(i,_rescrapeList.FullPathAndFilenames.Count)
            ReportProgress("Rescraping '" & CapsFirstLetter(_rescrapeList.Field.Replace("_"," ")) & "' " & i & " of " & _rescrapeList.FullPathAndFilenames.Count & " ")
            RescrapeSpecificMovie(FullPathAndFilename,rl)

            If Cancelled then Exit For
        Next
        SaveCaches
    End Sub

    Function CapsFirstLetter(words As String)
        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words)
    End Function


    Sub RescrapeMovie(NfoFilename as String)
'       Dim movie = New Movie(Utilities.GetFileName(NfoFilename,True),Me)
        Dim movie = New Movie(Me,NfoFilename)

        'movie.DeleteScrapedFiles
        movie.Rescrape=True

        ScrapeMovie(movie)
    End Sub



    Function CalcPercentDone(onNumber As Integer, total As Integer) As Integer
        Try
            If total = 0 Then total=onNumber
            Return Math.Min( (100/total)*onNumber , 100 )
        Catch
            Return 1
        End Try
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
                                newmovie.rating = detail.InnerText.ToString.ToRating
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
                                Try
                                    newmovie.Votes = detail.InnerText
                                Catch
                                    newmovie.Votes = 0
                                End Try

                            Case "Resolution" : newmovie.Resolution = detail.InnerText


                            Case "audio"
                    '               newmovie.Audio.Clear

                                    Dim audio As New AudioDetails
                                    For Each audiodetails As XmlNode In detail.ChildNodes

                                        Select Case audiodetails.Name
                                            Case "language"
                                                audio.Language.Value = audiodetails.InnerText
                                            Case "codec"
                                                audio.Codec.Value = audiodetails.InnerText
                                            Case "channels"
                                                audio.Channels.Value = audiodetails.InnerText
                                            Case "bitrate"
                                                audio.Bitrate.Value = audiodetails.InnerText
                                        End Select
                                    Next
                                    newmovie.Audio.Add(audio)

                            Case "Premiered"   : newmovie.Premiered   = detail.InnerText
                            Case "Certificate" : newmovie.Certificate = detail.InnerText

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

            'If movie.Votes <> Nothing And movie.Votes <> "" Then
            '    childchild = doc.CreateElement("votes")
            '    childchild.InnerText = movie.Votes
            '    child.AppendChild(childchild)
            'Else
            '    childchild = doc.CreateElement("votes")
            '    childchild.InnerText = ""
            '    child.AppendChild(childchild)
            'End If

            child.AppendChild(doc, "votes"     , movie.Votes     )     
            child.AppendChild(doc, "Resolution", movie.Resolution)     
            
   '        childchild = doc.CreateElement("audio")

            For Each item in movie.Audio
                child.AppendChild(item.GetChild(doc))
            Next

            child.AppendChild(doc, "Premiered"  , movie.Premiered  )
            child.AppendChild(doc, "Certificate", movie.Certificate)
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
        If movRebuildCaches Then _actorDb.Clear : _tmpActorDb.Clear

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

        If movRebuildCaches Then
            Dim q = From item In _tmpActorDb Select item.ActorName, item.MovieId

            For Each item In q.Distinct()
                _actorDb.Add(New ActorDatabase(item.ActorName, item.MovieId))
            Next
            SaveActorCache()
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
        'If movRebuildCaches Then _actorDb.Clear : _tmpActorDb.Clear

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
            Threading.Thread.Sleep(100)

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

            For Each item As BackgroundWorker in BWs
                Try
                    Busy = Busy Or item.IsBusy
                    If Busy Then Exit For
                Catch
                End Try
            Next

        End While
      
        If Cancelled Then Exit Sub

        'If movRebuildCaches Then
        '    Dim q = From item In _tmpActorDb Select item.ActorName, item.MovieId

        '    For Each item In q.Distinct()
        '        _actorDb.Add(New ActorDatabase(item.ActorName, item.MovieId))
        '    Next
	    '    SaveActorCache()
        'End If

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

            Case MovieProgress.MsgType.DoneSome        : NumberOfFoldersDone += mp.Data
                                                         PercentDone = CalcPercentDone(NumberOfFoldersDone, TotalNumberOfFolders)
                                                         ReportProgress("Active threads : [" & NumActiveThreads & "] - Scanning folder " & NumberOfFoldersDone & " of " & TotalNumberOfFolders)

        End Select
    End Sub


    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Threading.Monitor.Enter(Me)
        NumActiveThreads -= 1

        If IsNothing(e.Result) Then Exit Sub

        Dim Cache As List(Of ComboList) = CType(e.Result, List(Of ComboList))
 
        TmpMovieCache.AddRange(Cache) 
        Threading.Monitor.Exit(Me)
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


        Dim progress As MovieProgress = New MovieProgress

        progress.ProgressEvent = MovieProgress.MsgType.GotFoldersCount
        progress.Data          = moviePaths.Count

        bw.ReportProgress( -1, progress.Clone )

        Dim   i           As Integer = 0
        Const ReportEvery As Integer = 1

        progress.ProgressEvent = MovieProgress.MsgType.DoneSome
        progress.Data          = ReportEvery

        For Each Path In moviePaths
            i += 1
            If i Mod ReportEvery=0 Then bw.ReportProgress( -1, progress.Clone )

            MT_mov_ListFiles( pattern, New DirectoryInfo(Path), Cache )
            If Cancelled Then Exit Sub
        Next

        progress.Data = i Mod ReportEvery

        bw.ReportProgress( -1, progress.Clone )

        If Not Preferences.usefoldernames Then
            For Each movie In Cache
                If movie.filename <> Nothing Then movie.filename = movie.filename.Replace(".nfo", "")
            Next
        End If
    End Sub



'    Private Sub MT_mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo, Cache As List(Of ComboList))

'        Dim nfoFunction  As New WorkingWithNfoFiles
'        Dim workingMovie As ComboList

'        For Each oFileInfo In dirInfo.GetFiles(pattern)
''            Application.DoEvents()

'            If Cancelled Then Exit Sub

'            If Not File.Exists(oFileInfo.FullName) Then Continue For


'            workingMovie = nfoFunction.mov_NfoLoadBasic(oFileInfo.FullName, "movielist")

'            If workingMovie.title = "Error"                    Then Continue For
'            If workingMovie.genre.IndexOf("skipthisfile") > -1 Then Continue For

'            workingMovie.foldername   = Utilities  .GetLastFolder (workingMovie.fullpathandfilename)
'            workingMovie.missingdata1 = Preferences.GetMissingData(workingMovie.fullpathandfilename)

'            Cache.Add(workingMovie)
'        Next
'    End Sub

    Private Sub MT_mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo, Cache As List(Of ComboList))

        For Each oFileInfo In dirInfo.GetFiles(pattern)

            Application.DoEvents

            If Cancelled Then Exit Sub

            If Not File.Exists(oFileInfo.FullName) Then Continue For

            Dim movie = New Movie(Me,oFileInfo.FullName)
        
            If movie.mediapathandfilename = "none" Then Continue For
       
            movie.LoadNFO(False)

            If movie.ScrapedMovie.fullmoviebody.outline = "This nfo file could not be loaded" Then Continue For

            Cache.Add(movie.Cache)
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


    'Private Sub mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo)

    '    Dim nfoFunction As New WorkingWithNfoFiles

    '    Dim workingMovie As ComboList

    '    For Each oFileInfo In dirInfo.GetFiles(pattern)
    '        Application.DoEvents()

    '        If Cancelled Then Exit Sub

    '        If Not File.Exists(oFileInfo.FullName) Then Continue For

    '        workingMovie = nfoFunction.mov_NfoLoadBasic(oFileInfo.FullName, "movielist")

    '        If workingMovie.title = "Error" Then Continue For

    '        workingMovie.foldername = Utilities.GetLastFolder(workingMovie.fullpathandfilename)

    '        If workingMovie.genre.IndexOf("skipthisfile") = -1 Then

    '            workingMovie.missingdata1 = Preferences.GetMissingData(workingMovie.fullpathandfilename)

    '            TmpMovieCache.Add(workingMovie)
    '        End If
    '    Next
    'End Sub

    Private Sub mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo)

        If IsNothing(dirInfo) Then Exit Sub

        For Each oFileInfo In dirInfo.GetFiles(pattern)

            Application.DoEvents

            If Cancelled Then Exit Sub

            If Not File.Exists(oFileInfo.FullName) Then Continue For

            Dim movie = New Movie(Me,oFileInfo.FullName)
        
            If movie.mediapathandfilename = "none" Then Continue For

            movie.LoadNFO(False)

            If movie.ScrapedMovie.fullmoviebody.outline = "This nfo file could not be loaded" Then Continue For

            TmpMovieCache.Add(movie.Cache)
        Next

    End Sub

    Function xbmcTmdbRenameMovie(ByVal aMovie As Movie, ByVal filename As String) As String
        Dim NewFilenameandPath As String = filename
        Try
            If Preferences.MovieRenameEnable AndAlso Not Preferences.usefoldernames AndAlso Not filename.ToLower.Contains("video_ts") AndAlso Not Preferences.basicsavemode Then  'Preferences.GetRootFolderCheck(NfoPathAndFilename) OrElse 
            'Dim ExtensionPosition As Integer = filename.LastIndexOf(".")
            'Dim nfoFilename As String = filename.Remove(ExtensionPosition, (filename.Length - ExtensionPosition))
            'nfoFilename &= ".nfo"
            'Dim thismovie = New Movie(Me, nfoFilename)
            'thismovie = LoadMovie(nfoFilename, False)
            'thismovie.Scraped=False

            AddMovieEventHandlers   ( aMovie )
            aMovie.fileRename(aMovie.ScrapedMovie.fullmoviebody, aMovie)
            RemoveMovieEventHandlers( aMovie )
            NewFilenameandPath = aMovie.mediapathandfilename

            End If
        Catch ex As Exception
            Return NewFilenameandPath
        End Try

        Return NewFilenameandPath
    End Function

    Function XbmcTmdbDlPosterFanart(ByVal aMovie as Movie) as Boolean
        Try
            If Not Preferences.scrapemovieposters then
            Return False
        End If
            AddMovieEventHandlers ( aMovie )
            aMovie.IniTmdb 
            aMovie.DoDownloadPoster
            aMovie.DoDownloadFanart 
            RemoveMovieEventHandlers ( aMovie )

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Function XbmcTmdbChangeMovieCleanup(NfoPathAndFilename As String) As Boolean
        Try
            Dim movie = New Movie(Me,NfoPathAndFilename)

            movie.DeleteScrapedFiles(True)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

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
        If Preferences.UseMultipleThreads Then
            movRebuildCaches = False
        Else
            movRebuildCaches = True
        End If
        RebuildMovieCache
        If Cancelled Then Exit Sub
        If Not movRebuildCaches Then
            RebuildActorCache
        Else
            movRebuildCaches = False
        End If
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

'           Dim m As New Movie(movie.fullpathandfilename, Me)
'           Dim m As New Movie(Utilities.GetFileName(movie.fullpathandfilename,True), Me)
            Dim m = New Movie(Me,movie.fullpathandfilename)

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

        

    Public Shared Sub SpinUpDrives
        For each item In Preferences.movieFolders
            
            Dim bw As BackgroundWorker = New BackgroundWorker

            AddHandler bw.DoWork, AddressOf bw_SpinupDrive

            bw.RunWorkerAsync(item)
        Next
    End Sub

    Shared Sub bw_SpinupDrive(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) 
        Try
            Dim file As String = Path.Combine( DirectCast(e.Argument,String) , "delme.tmp" )

            IO.File.WriteAllText(file, "Anything")

            Utilities.SafeDeleteFile(file)
        Catch
        End Try
    End Sub


#Region "Filters"



    'Function ApplyAudioLanguageFilter( b As IEnumerable(Of Data_GridViewMovie), filterValue As String )

    '    Dim leftOuterJoinTable = From m In b From a In m.Audio Select m.fullpathandfilename, field=If(a.Language.Value="","Unknown",a.Language.Value)

    '    Return Filter(b,filterValue,leftOuterJoinTable)
    'End Function


    'Function Filter( b As IEnumerable(Of Data_GridViewMovie), filterValue As String, leftOuterJoinTable As IEnumerable )

    '    Return From m In b
    '                Group Join 
    '                    a In leftOuterJoinTable On a.fullpathandfilename Equals m.fullpathandfilename
    '                Into
    '                    ResultList = Group
    '                From
    '                    result In ResultList.DefaultIfEmpty
    '                Where
    '                    If( result Is Nothing, "Unassigned", result.field ) = filterValue
    '                Select 
    '                    m
    '                Distinct
    'End Function



    Function ApplyGenresFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim i As Integer = 0

        For Each item As CCBoxItem In ccb.Items

            Dim value As String = item.Name.RemoveAfterMatch

            Select ccb.GetItemCheckState(i)
                Case CheckState.Checked   : recs = (From m In recs Where     m.genre.Contains(value)).ToList
                Case CheckState.Unchecked : recs = (From m In recs Where Not m.genre.Contains(value)).ToList
            End Select

            i += 1
        Next

        Return recs
    End Function


    Function ApplyCertificatesFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb,"Missing","")
       
        If fi.Include.Count>0 Then
            recs = recs.Where(  Function(x)     fi.Include.Contains( CertificateMappings.GetMapping(x.Certificate) )  )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where(  Function(x) Not fi.Exclude.Contains( CertificateMappings.GetMapping(x.Certificate) )  )
        End If

        Return recs
    End Function


    Function ApplySetsFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb)
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x)     fi.Include.Contains(x.movieset) )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not fi.Exclude.Contains(x.movieset) )
        End If

        Return recs
    End Function


    Function ApplyResolutionsFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb,"Unknown","-1")
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x)     fi.Include.Contains(x.Resolution) )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not fi.Exclude.Contains(x.Resolution) )
        End If

        Return recs
    End Function


    Function ApplyAudioCodecsFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)

        Dim fi As New FilteredItems(ccb)

        Dim leftOuterJoinTable = From m In recs From a In m.Audio Select m.fullpathandfilename, field=If(a.Codec.Value="","Unknown",a.Codec.Value)
        
        Return Filter(recs, leftOuterJoinTable, fi)
             
    End Function


    Function ApplyAudioChannelsFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )

        Dim fi As New FilteredItems(ccb)

        Dim leftOuterJoinTable = From m In recs From a In m.Audio Select m.fullpathandfilename, field=If(a.Channels.Value="","Unknown",a.Channels.Value)

        Return Filter(recs,leftOuterJoinTable, fi)
    End Function


    Function ApplyAudioBitratesFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )

        Dim fi As New FilteredItems(ccb)

        Dim leftOuterJoinTable = From m In recs From a In m.Audio Select m.fullpathandfilename, field=If(a.Bitrate.Value="","Unknown",a.Bitrate.Value)

        Return Filter(recs,leftOuterJoinTable, fi)
    End Function


    Function ApplyNumAudioTracksFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )

        Dim fi As New FilteredItems(ccb)

        Dim leftOuterJoinTable = From m In recs Select m.fullpathandfilename, field=If(m.Audio.Count.ToString="","Unknown",m.Audio.Count.ToString)

        Return Filter(recs,leftOuterJoinTable, fi)
    End Function


    Function ApplyAudioLanguagesFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )

        Dim fi As New FilteredItems(ccb)

        Dim leftOuterJoinTable = From m In recs From a In m.Audio Select m.fullpathandfilename, field=If(a.Language.Value="","Unknown",a.Language.Value)

        Return Filter(recs,leftOuterJoinTable, fi)
    End Function


    Function ApplyActorsFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )

        Dim fi As New FilteredItems(ccb)

        If fi.Include.Count>0 Then 
            Dim MovieIds = (From a In ActorDb Where fi.Include.Contains(a.ActorName) Select a.MovieId).ToList
                             
            recs = recs.Where( Function(x)     MovieIds.Contains(x.id) )
        End If


        If fi.Exclude.Count>0 Then 
            Dim MovieIds = (From a In ActorDb Where fi.Exclude.Contains(a.ActorName) Select a.MovieId).ToList
                             
            recs = recs.Where( Function(x) Not MovieIds.Contains(x.id) )
        End If

        Return recs
    End Function



    Function ApplySourcesFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb)
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x)     fi.Include.Contains(x.source) )
        End If

        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not fi.Exclude.Contains(x.source) )
        End If

        Return recs
    End Function





    Function Filter(recs As IEnumerable(Of Data_GridViewMovie), leftOuterJoinTable As IEnumerable, fi As FilteredItems)

        If fi.Include.Count>0 Then

            recs = From m In recs
                    Group Join 
                        a In leftOuterJoinTable On a.fullpathandfilename Equals m.fullpathandfilename
                    Into
                        ResultList = Group
                    From
                        result In ResultList.DefaultIfEmpty
                    Where
                        fi.Include.Contains(If( result Is Nothing, "Unassigned", result.field ))
                    Select 
                        m
                    Distinct
        End If

        If fi.Exclude.Count>0 Then

            recs = From m In recs
                    Group Join 
                        a In leftOuterJoinTable On a.fullpathandfilename Equals m.fullpathandfilename
                    Into
                        ResultList = Group
                    From
                        result In ResultList.DefaultIfEmpty
                    Where
                        Not fi.Exclude.Contains(If( result Is Nothing, "Unassigned", result.field ))
                    Select 
                        m
                    Distinct
        End If

        Return recs
    End Function
     

#End Region

 

End Class
