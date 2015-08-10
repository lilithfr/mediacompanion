Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Media_Companion
Imports MC_UserControls
Imports XBMC.JsonRpc

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

    Private _certificateMappings   As CertificateMappings
    Private _actorDb               As New List(Of ActorDatabase)
    Public _tmpActorDb             As New List(Of ActorDatabase)
    Private _directorDb            As New List(Of DirectorDatabase)
    Public _tmpDirectorDb          As New List(Of DirectorDatabase)
    Private _moviesetDb            As New List(Of MovieSetDatabase)
    Public _tmpMoviesetDb          As New List(Of MovieSetDatabase)
    Public Shared movRebuildCaches As Boolean = False

    Public Property Bw            As BackgroundWorker = Nothing
    Public Property MovieCache    As New List(Of ComboList)
    Public Property TmpMovieCache As New List(Of ComboList)
    
    Public Property NewMovies     As New List(Of Movie)
    Public Property PercentDone   As Integer = 0

    Private _data_GridViewMovieCache As New List(Of Data_GridViewMovie)


    Public Event XbmcMcMoviesChanged
    Public Event XbmcOnlyMoviesChanged


    Private _xbmcMcMovies   As Dictionary(Of String, XbmcMovieForCompare)
    Private _xbmcOnlyMovies As List      (Of         XbmcMovieForCompare)

    Property XbmcMcMovies As Dictionary(Of String, XbmcMovieForCompare)
        Get
            Return _xbmcMcMovies
        End Get
        Set
            _xbmcMcMovies = Value
            RaiseEvent XbmcMcMoviesChanged
        End Set
    End Property

    Property XbmcOnlyMovies As List(Of XbmcMovieForCompare)
        Get
            Return _xbmcOnlyMovies
        End Get
        Set
            _xbmcOnlyMovies = Value
            RaiseEvent XbmcOnlyMoviesChanged
        End Set
    End Property

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
                        Select field.IfBlankMissing & " (" & Num.ToString & ")" 

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

    Public ReadOnly Property TagFilter As List(Of String)
        Get
            Dim q2 = From x In Preferences.movietags
           
            Return q2.AsEnumerable.ToList
        End Get
    End Property


    Public ReadOnly Property CountriesFilter As List(Of String)
        Get
            'Dim q = From x In MovieCache Select ms=x.countriesList
             
            'Dim lst = q.SelectMany(Function(m) m).ToList

            'Dim q2 = From x In lst
            '            Group By xx=x.Trim Into Num=Count
            '            Order By xx
            '            Select xx.IfBlankMissing & " (" & Num.ToString & ")" 

            Dim q = From x In MovieCache Select ms=x.countries.Split(",")
             
            Dim lst = q.SelectMany(Function(m) m).ToList

            lst.RemoveAll(Function(v) v="" )
            lst.RemoveAll(Function(v) v=",")

            Dim q2 = From x In lst
                        Group By x Into Num=Count
                        Order By x
                        Select x & " (" & Num.ToString & ")"
                        
            Return q2.AsEnumerable.ToList
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
     
    Public ReadOnly Property ListRuntimes As List(Of Integer)
        Get
            Dim q = From m In MovieCache Select m.IntRuntime

            Return q.AsEnumerable.ToList
        End Get
    End Property    

    

    Public ReadOnly Property ListFolderSizes As List(Of Double)
        Get
'            Dim q = From m In MovieCache Select CInt( m.FolderSize /(1024*1024*1024) )

            Dim q = From m In MovieCache Select m.DisplayFolderSize
           
            Return q.AsEnumerable.ToList
        End Get
    End Property    

    Public ReadOnly Property MinFolderSize As Double
        Get
            If MovieCache.Count=0 Then Return 0

            Dim q = Aggregate m In MovieCache Into Min(m.DisplayFolderSize)

            Return q
        End Get
    End Property    

    Public ReadOnly Property MaxFolderSize As Double
        Get
            If MovieCache.Count=0 Then Return 0

            Dim q = Aggregate m In MovieCache Into Max(m.DisplayFolderSize)

            Return q
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
            lst.Add( ScrapeError              )
            lst.Add( Duplicates               )
            If Not Preferences.DisableNotMatchingRenamePattern Then
                lst.Add( NotMatchingRenamePattern )
            End If
            lst.Add( MissingCertificate       )
            lst.Add( MissingFanart            )
            lst.Add( MissingGenre             )
            lst.Add( MissingLocalActors       )
            lst.Add( MissingOutline           )
            lst.Add( MissingTagline           )
            lst.Add( MissingPlot              )
            lst.Add( MissingPoster            )
            lst.Add( PreFrodoPosterOnly       )
            lst.Add( FrodoPosterOnly          )
            lst.Add( FrodoAndPreFrodoPosters  )
            lst.Add( MissingRating            )
            lst.Add( MissingRuntime           )
            lst.Add( MissingTrailer           )
            lst.Add( MissingVotes             )
            lst.Add( MissingYear              )
            lst.Add( MissingIMDBId            )
            lst.Add( MissingSource            )
            lst.Add( MissingDirector          )
            If Preferences.ShowExtraMovieFilters Then
                lst.Add( "Imdb in folder name ("     &    ImdbInFolderName & ")")
                lst.Add( "Imdb not in folder name (" & NotImdbInFolderName & ")")
                lst.Add( "Imdb not in folder name & year mismatch (" & NotImdbInFolderNameAndYearMisMatch & ")")
                lst.Add( "Plot same as Outline (" &  PlotEqOutline & ")")
            End If
            If Preferences.XBMC_Link Then
                If Not IsNothing(Form1.MC_Only_Movies) Then lst.Add( MC_Only_Movies )
                If Not IsNothing(XbmcMcMovies) Then lst.Add( "Different titles (" & Xbmc_DifferentTitles.Count.ToString & ")"  )
            End If

            Return lst
        End Get
    End Property    

    Public ReadOnly Property PlotEqOutline As Integer
        Get
            Return (From x In MovieCache Where x.PlotEqOutline).Count
        End Get
    End Property  

    Public ReadOnly Property NotImdbInFolderNameAndYearMisMatch As Integer
        Get
            Return (From x In MovieCache Where Not x.ImdbInFolderName And x.year<>x.FolderNameYear).Count
        End Get
    End Property  

    Public ReadOnly Property NotImdbInFolderName As Integer
        Get
            Return (From x In MovieCache Where Not x.ImdbInFolderName).Count
        End Get
    End Property  
  
    Public ReadOnly Property ImdbInFolderName As Integer
        Get
            Return (From x In MovieCache Where x.ImdbInFolderName).Count
        End Get
    End Property    

    Public ReadOnly Property NotMatchingRenamePattern As String
        Get
            Return "Not matching rename pattern (" & (From x In MovieCache Where Not x.ActualNfoFileNameMatchesDesired).Count & ")" 
        End Get
    End Property    

    Public ReadOnly Property PreFrodoPosterOnly As String
        Get
            Return "Pre-Frodo poster only (" & PreFrodoPosterOnlyCount.ToString & ")" 
        End Get
    End Property    

    Public ReadOnly Property PreFrodoPosterOnlyCount As Integer
        Get
            Return (From x In MovieCache Where x.PreFrodoPosterExists And Not x.FrodoPosterExists).Count
        End Get
    End Property    

    Public ReadOnly Property FrodoPosterOnly As String
        Get
            Return "Frodo poster only (" & FrodoPosterOnlyCount.ToString & ")" 
        End Get
    End Property    

    Public ReadOnly Property FrodoPosterOnlyCount As Integer
        Get
            Return (From x In MovieCache Where Not x.PreFrodoPosterExists And x.FrodoPosterExists).Count
        End Get
    End Property    

    Public ReadOnly Property FrodoAndPreFrodoPosters As String
        Get
            Return "Both poster formats (" & FrodoAndPreFrodoPostersCount.ToString & ")" 
        End Get
    End Property    

    Public ReadOnly Property FrodoAndPreFrodoPostersCount As Integer
        Get
            Return (From x In MovieCache Where x.PreFrodoPosterExists And x.FrodoPosterExists).Count
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

    Public ReadOnly Property MissingTagline As String
        Get
            Return "Missing Tagline (" & (From x In MovieCache Where x.MissingTagLine).Count & ")" 
        End Get
    End Property

    Public ReadOnly Property MissingRuntime As String
        Get
            Return "Missing Runtime (" & (From x In MovieCache Where x.MissingRuntime).Count & ")" 
        End Get
    End Property  

    Public ReadOnly Property MissingIMDBId As String
        Get
            Return "Missing IMDB (" & (From x In MovieCache Where x.MissingIMDBId).Count & ")"
        End Get
    End Property

    Public ReadOnly Property MissingSource As String
        Get
            Return "Missing Source (" & (From x In MovieCache Where x.MissingSource).Count & ")"
        End Get
    End Property

    Public ReadOnly Property MissingDirector As String
        Get
            Return "Missing Director (" & (From x In MovieCache Where x.MissingDirector).Count & ")"
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

    Public ReadOnly Property MC_Only_Movies As String
        Get
            Return "Missing from XBMC (" & (From x In Form1.MC_Only_Movies).Count & ")" 
        End Get
    End Property  

    Public ReadOnly Property ScrapeError
        Get
            Return "Scrape Error (" & (From x In MovieCache Where x.genre.ToLower = "problem").Count & ")"
        End Get
    End Property

    Public ReadOnly Property Duplicates As String
        Get
            Dim total           As Integer = (From x In MovieCache).Count
            Dim total_distinct  As Integer = (From x In MovieCache Where x.id <> "0" Select x.id ).Distinct.Count
            Dim total_noId      As Integer = (From x In MovieCache Where x.id = "0" Select x.id).Count 

            Dim num_duplicates = total - (total_distinct + total_noId)

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

    Public ReadOnly Property DirectorsFilter_Preferences As IEnumerable(Of String)
        Get
            Dim q = From x In DirectorDb 
                Group By 
                    x.ActorName Into NumFilms=Count 
                Where 
                    NumFilms>=Preferences.DirectorsFilterMinFilms
                                
            If Preferences.MovieFilters_Directors_Order=0 Then 
                q = From x In q Order by x.NumFilms  Descending, x.ActorName Ascending
            Else
                q = From x In q Order by x.ActorName Ascending , x.NumFilms  Descending
            End If

            Return From x In q Select x.ActorName & " (" & x.NumFilms.ToString & ")" Take Preferences.MaxDirectorsInFilter
        End Get
    End Property    

    Public ReadOnly Property ActorsFilter As List(Of String)
        Get
            Dim r = (From x In ActorsFilter_Preferences).Union(From x In ActorsFilter_Extras) 
            Return r.ToList
        End Get
    End Property    

    Public ReadOnly Property DirectorsFilter As List(Of String)
        Get
            Dim r = (From x In DirectorsFilter_Preferences).Union(From x In DirectorsFilter_Extras) 
            Return r.ToList
        End Get
    End Property    

    Public ReadOnly Property ActorsFilter_Extras As IEnumerable(Of String)
        Get
            Dim q = From x In ActorDb 
                Group By 
                    x.ActorName Into NumFilms=Count 
                Where 
                    ActorsFilter_AlsoInclude.Contains(IIf(IsNothing(ActorName),"N/A",ActorName))
            
            Return From x In q Select x.ActorName & " (" & x.NumFilms.ToString & ")"
        End Get
    End Property    

    Property ActorsFilter_AlsoInclude As New List(Of String)

    Sub ActorsFilter_AddIfMissing(value As String)
        If Not ActorsFilter.Any(Function(x) x.StartsWith(value)) Then
            ActorsFilter_AlsoInclude.Add(value)
        End If
    End Sub

    Public ReadOnly Property DirectorsFilter_Extras As IEnumerable(Of String)
        Get
            Dim q = From x In DirectorDb 
                Group By 
                    x.ActorName Into NumFilms=Count 
                Where 
                    DirectorsFilter_AlsoInclude.Contains(IIf(IsNothing(ActorName),"N/A",ActorName))
            
            Return From x In q Select x.ActorName & " (" & x.NumFilms.ToString & ")"
        End Get
    End Property  

    Property DirectorsFilter_AlsoInclude As New List(Of String)

    Sub DirectorsFilter_AddIfMissing(value As String)
        If Not DirectorsFilter.Any(Function(x) x.StartsWith(value)) Then
            DirectorsFilter_AlsoInclude.Add(value)
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

    Public ReadOnly Property VideoCodecFilter As List(Of String)
        Get
            Dim q = From x In MovieCache 
                Group By x.VideoCodec Into NumFilms=Count 
                Order by Videocodec Descending

            Dim r = (From x In q Select x.VideoCodec & " (" & x.NumFilms.ToString & ")").ToList

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

    Public ReadOnly Property AudioDefaultLanguages As IEnumerable
        Get
            Dim result As IEnumerable = From m In MovieCache Select m.fullpathandfilename, field=If(m.DefaultAudioTrack.Language.Value="","Unknown",m.DefaultAudioTrack.Language.Value)

            Return result
        End Get
    End Property    

    Public ReadOnly Property AudioDefaultLanguagesFilter As List(Of String)
        Get
            Return QryMovieCache(AudioDefaultLanguages)
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
                    SetsFilter_AlsoInclude.Contains(MovieSet.MovieSetName)
            
            Return From x In q Select x.MovieSet.MovieSetName & " (" & x.NumFilms.ToString & ")"
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
                    x.MovieSet.MovieSetName Into NumFilms=Count
                Where 
                    NumFilms>=Preferences.SetsFilterMinFilms 

            If Preferences.MovieFilters_Sets_Order=0 Then 
                q = From x In q Order by x.NumFilms Descending, x.MovieSetName Ascending
            Else
                q = From x In q Order by x.MovieSetName.Replace("-None-","") Ascending , x.NumFilms Descending
            End If

            Return From x In q Select x.MovieSetName & " (" & x.NumFilms.ToString & ")" Take Preferences.MaxSetsInFilter 
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
                Dim q = From x In MovieCache Select ms = x.MovieSet.MovieSetName.Split("^~") Distinct

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

    Public ReadOnly Property SubTitleLangFilter As List(Of String)
        Get
            Dim leftOuterJoinTable As IEnumerable = From m In MovieCache From a In m.SubLang Select m.fullpathandfilename, field=If(a.Language.Value="","Unknown",a.Language.Value)

            Return QryMovieCache(leftOuterJoinTable)
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

    Public ReadOnly Property DirectorDb As List(Of DirectorDatabase)
        Get
            Return _directorDb
        End Get
    End Property

    Public ReadOnly Property MovieSetDB As List(Of MovieSetDatabase)
        Get
            Return _moviesetDb
        End Get
    End Property

    Public Function GetMSetId(mSetName As String) As String
        For Each mset In MovieSetDB
            If mset.MovieSetName = mSetName Then
                Return mset.MovieSetId 
            End If
        Next
        If Not mSetName.ToLower = "-none-" Then
            Dim newmset As New MovieSetDatabase
            newmset.MovieSetName = mSetName
            MovieSetDB.Add(newmset)
        End If
        Return ""
    End Function

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

        AddHandler Me.XbmcMcMoviesChanged   , AddressOf Handle_XbmcMcMoviesChanged
        AddHandler Me.XbmcOnlyMoviesChanged , AddressOf Handle_XbmcOnlyMoviesChanged
       
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
        If q.Count = 0 Then Return Nothing
        Return q.Single
    End Function

    Public Function FindData_GridViewCachedMovie(fullpathandfilename As String) As Data_GridViewMovie
        Dim q = From m In _data_GridViewMovieCache Where m.fullpathandfilename=fullpathandfilename
        Return q.Single        
    End Function

    Public Function LoadMovie(fullpathandfilename As String, Optional ByVal Cacheupdate As Boolean = True) As Movie
        Dim movie = New Movie(Me,fullpathandfilename)
        If IsNothing(movie) Then Return Nothing
        movie.LoadNFO(Cacheupdate)
        Return movie
    End Function

    Public Function SaveAndLoadMovie(fullpathandfilename As String, fmd As FullMovieDetails)
        Movie.SaveNFO(fullpathandfilename,fmd)
        Return oMovies.LoadMovie(fullpathandfilename)
    End Function

    Public Sub FindNewMovies(Optional scrape=True)
        NewMovies.Clear
        PercentDone = 0

        Dim folders As New List(Of String)

        AddOnlineFolders ( folders , Preferences.movieFolders )
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
    
    Public Sub AddOnlineFolders( folders As List(Of String), searchfolders As List(Of String) )
        For Each moviefolder In searchfolders 'Preferences.movieFolders

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
            Preferences.DoneAMov = True
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
            Preferences.googlecount += 1
            Preferences.engineno += 1
            If Preferences.engineno = 3 Then Preferences.engineno = 0
            If newMovie.TimingsLog <> "" then
                ReportProgress(,vbCrLf & "Timings" & vbCrLf & "=======" & newMovie.TimingsLog & vbCrLf & vbCrLf)
            End If
            If Cancelled then Exit Sub
        Next
        Preferences.googlecount = 0
        ReportProgress( ,"!!! " & vbCrLf & "!!! Finished" )
    End Sub

    Sub ScrapeMovie(movie As Movie)
        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.Scrape
        RemoveMovieEventHandlers( movie )
    End Sub

    Sub ChangeMovie(NfoPathAndFilename As String, ChangeMovieId As String, MovieSearchEngine As String)
        Dim movie = New Movie(Me,NfoPathAndFilename)

        movie.DeleteScrapedFiles(True)

        If Not Preferences.MusicVidScrape Then movie.ScrapedMovie.Init

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.MovieSearchEngine = MovieSearchEngine
        movie.Scrape(ChangeMovieId)
        RemoveMovieEventHandlers( movie )
    End Sub

    Sub RescrapeSpecificMovie(fullpathandfilename As String,rl As RescrapeList)

        Dim movie = New Movie(Me,fullpathandfilename)

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.RescrapeSpecific  ( rl    )
        RemoveMovieEventHandlers( movie )
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
            ReportProgress("Rescraping '" & Utilities.TitleCase(_rescrapeList.Field.Replace("_"," ")) & "' " & i & " of " & _rescrapeList.FullPathAndFilenames.Count & " ")
            RescrapeSpecificMovie(FullPathAndFilename,rl)
            If Cancelled then Exit For
        Next
        SaveCaches
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

    Sub RescrapeMovie(NfoFilename as String, Optional ByVal tmdbid As String = "")
        If Not File.Exists(NfoFilename) Then 
            ReportProgress("NFO not found : [" & NfoFilename & "]  ")
            Return
        End If

        Dim movie = New Movie(Me, NfoFilename)

        movie.Rescrape = True

        Dim imdbid As String = movie.PossibleImdb 
        If Preferences.movies_useXBMC_Scraper AndAlso tmdbid <> "" Then  'AndAlso tmdbid <> "" 
            imdbid = tmdbid
        End If
        movie.DeleteScrapedFiles(False)

        movie.ScrapedMovie.Init

        AddMovieEventHandlers   ( movie )
        movie.Scraped=False
        movie.Scrape(imdbid)
        RemoveMovieEventHandlers( movie )
    End Sub

    Function CapsFirstLetter(words As String)
        Return Form1.MyCulture.TextInfo.ToTitleCase(words)
    End Function

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
        LoadPeopleCaches
        LoadMovieSetCaches
    End Sub

    Public Sub SaveCaches
        SaveMovieCache
        SaveMovieSetCache
        SaveActorCache
        SaveDirectorCache
    End Sub

    Public Sub LoadMovieCache

        MovieCache.Clear

        Dim movielist  As New XmlDocument
        Dim objReader  As New StreamReader(Preferences.workingProfile.MovieCache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close
        objReader = Nothing

        movielist.LoadXml(tempstring)

        Try
            For Each thisresult In movielist("movie_cache")
                Select Case thisresult.Name
                    Case "movie"
                        Dim newmovie As New ComboList
                        For Each detail In thisresult.ChildNodes
                            Select Case detail.Name
                                Case "missingdata1"         : newmovie.missingdata1 = Convert.ToByte(detail.InnerText)
                                Case "source"               : newmovie.source = detail.InnerText
                                Case "director"             : newmovie.director = detail.InnerText
                                Case "set"                  : newmovie.MovieSet.MovieSetName = detail.InnerText
                                Case "setid"                : newmovie.MovieSet.MovieSetId = detail.InnerText
                                Case "sortorder"            : newmovie.sortorder = detail.InnerText
                                Case "filedate"
                                    If detail.InnerText.Length <> 14 Then 'i.e. invalid date
                                        newmovie.filedate = "18500101000000" '01/01/1850 00:00:00
                                    Else
                                        newmovie.filedate = detail.InnerText
                                    End If
                                Case "createdate"
                                    If detail.InnerText.Length <> 14 Then 'i.e. invalid date
                                        newmovie.createdate = "18500101000000" '01/01/1850 00:00:00
                                    Else
                                        newmovie.createdate = detail.InnerText
                                    End If
                                Case "tag"                  : newmovie.movietag.Add(detail.innertext)
                                Case "filename"             : newmovie.filename = detail.InnerText
                                Case "foldername"           : newmovie.foldername = detail.InnerText
                                Case "fullpathandfilename"  : newmovie.fullpathandfilename = detail.InnerText
                                Case "genre"                : newmovie.genre = detail.InnerText & newmovie.genre
                                Case "id"                   : newmovie.id = detail.InnerText
                                Case "playcount"            : newmovie.playcount = detail.InnerText
                                Case "rating"               : newmovie.rating = detail.InnerText.ToString.ToRating
                                Case "title"                : newmovie.title = detail.InnerText
                                Case "originaltitle"        : newmovie.originaltitle = detail.InnerText
                                Case "top250"               : newmovie.top250 = detail.InnerText
                                Case "year"                 : newmovie.year = detail.InnerText
                                Case "outline"              : newmovie.outline = detail.InnerText
                                Case "plot"                 : newmovie.plot = detail.InnerText
                                Case "tagline"              : newmovie.tagline = detail.InnerText
                                Case "runtime"              : newmovie.runtime = detail.InnerText
                                Case "votes"
                                    Try
                                        newmovie.Votes = detail.InnerText
                                    Catch
                                        newmovie.Votes = 0
                                    End Try
                                Case "countries"
                                    Dim TmpStr As String = detail.InnerText
                                    newmovie.countries = TmpStr.Replace(", ", ",")
                                Case "Resolution"           : newmovie.Resolution = detail.InnerText
                                Case "VideoCodec"           : newmovie.VideoCodec = detail.InnerText
                                Case "Container"            : newmovie.Container = detail.InnerText
                                Case "audio"
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
                                Case "Premiered"            : newmovie.Premiered = detail.InnerText
                                Case "Certificate"          : newmovie.Certificate = detail.InnerText
                                Case "FrodoPosterExists"    : newmovie.FrodoPosterExists = detail.InnerText
                                Case "PreFrodoPosterExists" : newmovie.PreFrodoPosterExists = detail.InnerText
                                Case "subtitlelang"
                                    Dim subtitle As New SubtitleDetails
                                    subtitle.Language.Value = detail.InnerText
                                    newmovie.SubLang.Add(subtitle)
                                Case "FolderSize"           : 
                                    Try
                                        newmovie.FolderSize = detail.InnerText
                                    Catch
                                        newmovie.FolderSize = -1
                                    End Try                                

                            End Select
                        Next
                        If newmovie.source = Nothing Then
                            newmovie.source = ""
                        End If
                        If newmovie.MovieSet.MovieSetName = Nothing Then
                            newmovie.MovieSet.MovieSetName = "-None-"
                        End If
                        If newmovie.MovieSet.MovieSetName = "" Then
                            newmovie.MovieSet.MovieSetName = "-None-"
                        End If
                        MovieCache.Add(newmovie)
                End Select
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Rebuild_Data_GridViewMovieCache()
    End Sub


    Public Sub SaveMovieCache
        Dim cacheFile As String = Preferences.workingProfile.MovieCache
        If File.Exists(cacheFile) Then
            File.Delete(cacheFile)
        End If
        Dim doc      As New XmlDocument
        Dim xmlproc  As XmlDeclaration
        Dim root  As XmlElement
        Dim child As XmlElement
        Dim childchild As XmlElement
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        
        root = doc.CreateElement("movie_cache")
        For Each movie In MovieCache
            child = doc.CreateElement("movie")
            childchild = doc.CreateElement("filedate") : childchild.InnerText = movie.filedate : child.AppendChild(childchild)
            childchild = doc.CreateElement("createdate") : childchild.InnerText = movie.createdate : child.AppendChild(childchild)
            childchild = doc.CreateElement("missingdata1") : childchild.InnerText = movie.missingdata1.ToString : child.AppendChild(childchild)
            childchild = doc.CreateElement("filename") : childchild.InnerText = movie.filename : child.AppendChild(childchild)
            childchild = doc.CreateElement("foldername") : childchild.InnerText = movie.foldername : child.AppendChild(childchild)
            childchild = doc.CreateElement("fullpathandfilename") : childchild.InnerText = movie.fullpathandfilename : child.AppendChild(childchild)
            childchild = doc.CreateElement("source") :  : childchild.InnerText = If(movie.source = Nothing, "", movie.source) : child.AppendChild(childchild)
            childchild = doc.CreateElement("director") : childchild.InnerText = movie.director  : child.AppendChild(childchild)

            'If movie.MovieSet.MovieSetName <> Nothing Then
            If Not String.IsNullOrEmpty(movie.MovieSet.MovieSetName) AndAlso movie.MovieSet.MovieSetName <> "-None-" Then
                childchild = doc.CreateElement("set")
                childchild.InnerText = movie.MovieSet.MovieSetName
                child.AppendChild(childchild)
                childchild = doc.CreateElement("setid")
                childchild.InnerText = movie.MovieSet.MovieSetId 
                child.AppendChild(childchild)
            Else
                childchild = doc.CreateElement("set")
                childchild.InnerText = ""
                child.AppendChild(childchild)
                childchild = doc.CreateElement("setid")
                childchild.InnerText = ""
                child.AppendChild(childchild)
            End If
            'Else
            '    childchild = doc.CreateElement("set")
            '    childchild.InnerText = ""
            '    child.AppendChild(childchild)
                
            'End If
            childchild = doc.CreateElement("genre"    ) : childchild.InnerText = movie.genre     : child.AppendChild(childchild)
            childchild = doc.CreateElement("countries") : childchild.InnerText = movie.countries : child.AppendChild(childchild)

            For Each item In movie.movietag
                childchild = doc.CreateElement("tag")
                childchild.InnerText = item
                child.AppendChild(childchild)
            Next

            childchild = doc.CreateElement("id") : childchild.InnerText = movie.id : child.AppendChild(childchild)
            childchild = doc.CreateElement("playcount") : childchild.InnerText = movie.playcount : child.AppendChild(childchild)
            childchild = doc.CreateElement("rating") : childchild.InnerText = movie.rating : child.AppendChild(childchild)
            childchild = doc.CreateElement("title") : childchild.InnerText = movie.title : child.AppendChild(childchild)
            childchild = doc.CreateElement("originaltitle") : childchild.InnerText = movie.originaltitle : child.AppendChild(childchild)

            'If Not String.IsNullOrEmpty(movie.sortorder) Then movie.sortorder = movie.title
            childchild = doc.CreateElement("outline") : childchild.InnerText = If(String.IsNullOrEmpty(movie.sortorder), movie.title, movie.outline)
            child.AppendChild(childchild)

            childchild = doc.CreateElement("plot") : childchild.InnerText = Microsoft.VisualBasic.Strings.Left(movie.plot, 100)
            childchild = doc.CreateElement("tagline") : childchild.InnerText = movie.tagline : child.AppendChild(childchild)
            'If movie.plot.Length() > 100 Then
            '    childchild.InnerText = movie.plot.Substring(0, 100)     'Only write first 100 chars to cache- this plot is only used for table view - normal full plot comes from the nfo file (fullbody)
            'Else
            '    childchild.InnerText = movie.plot
            'End If
            child.AppendChild(childchild)

            childchild = doc.CreateElement("sortorder") : childchild.InnerText = movie.sortorder : child.AppendChild(childchild)
            childchild = doc.CreateElement("runtime") : childchild.InnerText = movie.runtime : child.AppendChild(childchild)
            childchild = doc.CreateElement("top250") : childchild.InnerText = movie.top250 : child.AppendChild(childchild)
            childchild = doc.CreateElement("year") : childchild.InnerText = movie.year : child.AppendChild(childchild)
            child.AppendChild(doc, "votes", movie.Votes)
            child.AppendChild(doc, "Resolution", movie.Resolution)
            child.AppendChild(doc, "VideoCodec", movie.VideoCodec)
            child.AppendChild(doc, "Container", movie.Container)

            For Each item In movie.Audio
                child.AppendChild(item.GetChild(doc))
            Next

            For Each item In movie.SubLang 
                child.AppendChild(item.GetChild(doc))
            Next

            child.AppendChild(doc, "Premiered", movie.Premiered)
            child.AppendChild(doc, "Certificate", movie.Certificate)
            child.AppendChild(doc, "FrodoPosterExists", movie.FrodoPosterExists)
            child.AppendChild(doc, "PreFrodoPosterExists", movie.PreFrodoPosterExists)
            child.AppendChild(doc, "FolderSize", movie.FolderSize)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(cacheFile, System.Text.Encoding.UTF8)

        output.Formatting = Xml.Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub

    Public Sub LoadMovieCacheFromNfos
        TmpMovieCache.Clear

        'If movRebuildCaches Then 
            _actorDb        .Clear 
            _tmpActorDb     .Clear
            _directorDb     .Clear 
            _tmpDirectorDb  .Clear
            _moviesetDb     .Clear
            _tmpMoviesetDb  .Clear
       ' End If

        Dim t As New List(Of String)

        t.AddRange(Preferences.movieFolders)
        t.AddRange(Preferences.offlinefolders)

        ReportProgress("Searching movie folders...")
        mov_NfoLoad(t)

        If Cancelled Then Exit Sub

        If Not Preferences.usefoldernames Then
            For Each movie In TmpMovieCache
                If movie.filename <> Nothing Then movie.filename = movie.filename.Replace(".nfo", "")
            Next
        End If

        'If movRebuildCaches Then
            Dim q = From item In _tmpActorDb Select item.ActorName, item.MovieId

            For Each item In q.Distinct()
                _actorDb.Add(New ActorDatabase(item.ActorName, item.MovieId))
            Next
            SaveActorCache()


            Dim q2 = From item In _tmpDirectorDb Select item.ActorName, item.MovieId

            For Each item In q2.Distinct()
                _directorDb.Add(New DirectorDatabase(item.ActorName, item.MovieId))
            Next
            SaveDirectorCache()

            Dim q3 = From item In _tmpMoviesetDb Select item.MovieSetName, item.MovieSetId

            For Each item In q3.Distinct()
                _moviesetDb.Add(New MovieSetDatabase(item.MovieSetName, item.MovieSetId))
            Next
            SaveMovieSetCache()

        'End If

        MovieCache.Clear
        MovieCache.AddRange(TmpMovieCache)
        Rebuild_Data_GridViewMovieCache()

        If Preferences.XbmcLinkReady Then
            Dim evt As BaseEvent = New BaseEvent(XbmcController.E.MC_ScanForNewMovies,PriorityQueue.Priorities.low)
            Form1.XbmcControllerQ.Write(evt)
        End If
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
        'RebuildMoviePeopleCaches

        MovieCache.Clear
        MovieCache.AddRange(TmpMovieCache)

        Rebuild_Data_GridViewMovieCache

        If Preferences.XbmcLinkReady Then
            Dim evt As BaseEvent = New BaseEvent(XbmcController.E.MC_ScanForNewMovies,PriorityQueue.Priorities.low)
            Form1.XbmcControllerQ.Write(evt)
        End If
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

    Private Sub MT_mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo, Cache As List(Of ComboList))
        Dim incmissing As Boolean = Preferences.incmissingmovies 
        For Each oFileInfo In dirInfo.GetFiles(pattern)

            Application.DoEvents

            If Cancelled Then Exit Sub

            If Not File.Exists(oFileInfo.FullName) Then Continue For

            Dim movie = New Movie(Me,oFileInfo.FullName)
        
            If Not incmissing AndAlso movie.mediapathandfilename = "none" Then Continue For
       
            movie.LoadNFO(False)

            If Not Preferences.moviesets.Contains(movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName) Then
                Preferences.moviesets.Add(movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName)
            End If
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

    Private Sub mov_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo)
        Dim incmissing As Boolean = Preferences.incmissingmovies 
        If IsNothing(dirInfo) Then Exit Sub
        For Each oFileInfo In dirInfo.GetFiles(pattern)
            Application.DoEvents
            If Cancelled Then Exit Sub
            If Not File.Exists(oFileInfo.FullName) Then Continue For
            Try
                Dim movie = New Movie(Me,oFileInfo.FullName)

                If Not incmissing AndAlso movie.mediapathandfilename = "none" Then Continue For
                If Not Utilities.NfoValidate(oFileInfo.FullName) Then Continue For
                movie.LoadNFO(False)

                If Not Preferences.moviesets.Contains(movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName) Then
                    Preferences.moviesets.Add(movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName)
                End If
                TmpMovieCache.Add(movie.Cache)
            Catch
                MsgBox("problem with : " & oFileInfo.FullName & " - Skipped" & vbCrLf & "Please check this file manually")
            End Try
        Next
    End Sub

    Function DeleteScrapedFiles(nfoPathAndFilename As String) As Boolean
        Try
            Dim aMovie = New Movie(Me, nfoPathAndFilename)
            aMovie.DeleteScrapedFiles(True)
            Dim isRoot As Boolean = Preferences.GetRootFolderCheck(nfoPathAndFilename)
            If Not isRoot Then
                aMovie.DeleteExtraFiles()
                aMovie.DeleteFanarTvFiles()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub LoadPeopleCaches()
        LoadActorCache()
        LoadDirectorCache()
    End Sub

    Sub LoadMovieSetCaches()
        LoadMovieSetCache(_moviesetDb, "movieset", Preferences.workingProfile.moviesetcache)
    End Sub

    Sub LoadActorCache()
        LoadPersonCache(_actorDb,"actor",Preferences.workingProfile.actorcache)
    End Sub

    Sub LoadDirectorCache()
        LoadPersonCache(_directorDb,"director",Preferences.workingProfile.DirectorCache)
    End Sub

    Sub LoadPersonCache(peopleDb As List(Of ActorDatabase),typeName As String,  fileName As String)
        peopleDb.Clear()
        If Not File.Exists(fileName) Then Exit Sub
        Dim peopleList As New XmlDocument
        peopleList.Load(fileName)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In peopleList(typeName & "_cache")
            Select Case thisresult.Name
                Case typeName
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
                    peopleDb.Add(New ActorDatabase(name, movieId))
            End Select
        Next
    End Sub

    Sub LoadPersonCache(peopleDb As List(Of DirectorDatabase),typeName As String,  fileName As String)
        peopleDb.Clear()
        If Not File.Exists(fileName) Then Exit Sub
        Dim peopleList As New XmlDocument
        peopleList.Load(fileName)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In peopleList(typeName & "_cache")
            Select Case thisresult.Name
                Case typeName
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
                    peopleDb.Add(New DirectorDatabase(name, movieId))
            End Select
        Next
    End Sub

    Sub LoadMovieSetCache(setDb As List(Of MovieSetDatabase),typeName As String,  fileName As String)
        setDb.Clear()
        If Not File.Exists(fileName) Then Exit Sub
        Dim moviesetcache As New XmlDocument
        moviesetcache.Load(fileName)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In moviesetcache(typeName & "_cache")
            Select Case thisresult.Name
                Case typeName
                    Dim movieset = ""
                    Dim moviesetId = ""
                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            Case "moviesetname"
                                movieset = detail.InnerText
                            Case "id"
                                moviesetId = detail.InnerText
                        End Select
                    Next
                    setDb.Add(New MovieSetDatabase(movieset, moviesetId))
            End Select
        Next
    End Sub

    Sub SaveActorCache()
        SavePersonCache(ActorDb,"actor",Preferences.workingProfile.actorcache)
    End Sub

    Sub SaveDirectorCache()
        SavePersonCache(DirectorDb,"director",Preferences.workingProfile.directorCache)
    End Sub

    Sub SaveMovieSetCache()
        SaveMovieSetCache(MovieSetDB, "movieset", Preferences.workingProfile.moviesetcache)
    End Sub

    Sub SavePersonCache(peopleDb As List(Of ActorDatabase), typeName As String, fileName As String)
        'Threading.Monitor.Enter(Me)
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc  As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)

        Dim root  As XmlElement
        Dim child As XmlElement

        root = doc.CreateElement(typeName & "_cache")

        Dim childchild As XmlElement
        Try
        For Each actor In peopleDb
            child = doc.CreateElement(typeName)
            childchild = doc.CreateElement("name")
            childchild.InnerText = actor.actorname
            child.AppendChild(childchild)
            childchild = doc.CreateElement("id")
            childchild.InnerText = actor.movieid
            child.AppendChild(childchild)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
        Catch
        End Try
        'Threading.Monitor.Exit(Me)
    End Sub

    Sub SavePersonCache(peopleDb As List(Of DirectorDatabase), typeName As String, fileName As String)
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc  As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)

        Dim root  As XmlElement
        Dim child As XmlElement

        root = doc.CreateElement(typeName & "_cache")

        Dim childchild As XmlElement

        For Each actor In peopleDb
            child = doc.CreateElement(typeName)
            childchild = doc.CreateElement("name")
            childchild.InnerText = actor.actorname
            child.AppendChild(childchild)
            childchild = doc.CreateElement("id")
            childchild.InnerText = actor.movieid
            child.AppendChild(childchild)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub

    Sub SaveMovieSetCache(setDb As List(Of MovieSetDatabase), typeName As String, fileName As String)
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc  As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)

        Dim root  As XmlElement
        Dim child As XmlElement

        root = doc.CreateElement(typeName & "_cache")

        Dim childchild As XmlElement

        For Each movieset In setDb
            If movieset.MovieSetName.ToLower = "-none-" Then Continue For
            child = doc.CreateElement(typeName)
            childchild = doc.CreateElement("moviesetname")
            childchild.InnerText = movieset.MovieSetName
            child.AppendChild(childchild)
            childchild = doc.CreateElement("id")
            childchild.InnerText = movieset.MovieSetId 
            child.AppendChild(childchild)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Dim output As New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub

    Public Sub RebuildCaches
        'If Preferences.UseMultipleThreads Then
        '    movRebuildCaches = False
        'Else
        '    movRebuildCaches = True
        'End If
        movRebuildCaches = Not Preferences.UseMultipleThreads 
        RebuildMovieCache
        If Cancelled Then Exit Sub
        If Not movRebuildCaches Then
            RebuildMoviePeopleCaches
        Else
            movRebuildCaches = False
        End If
        movRebuildCaches = False
    End Sub

    Public Sub RebuildMovieCache
        If Preferences.UseMultipleThreads Then
            '_actorDB      .Clear()
            '_directorDb   .Clear()
            '_moviesetDb   .Clear()
            '_tmpActorDb   .Clear()
            '_tmpDirectorDb.Clear()
            '_tmpMoviesetDb.Clear()
            MT_LoadMovieCacheFromNfos
        Else
            LoadMovieCacheFromNfos
        End If
        If Cancelled Then Exit Sub
        SaveMovieCache
    End Sub


    Public Sub RebuildMoviePeopleCaches()
        _actorDB      .Clear()
        _directorDb   .Clear()
        _moviesetDb   .Clear()
        _tmpActorDb   .Clear()
        _tmpDirectorDb.Clear()
        _tmpMoviesetDb.Clear()
        Dim i = 0

        For Each movie In MovieCache
            i += 1
            PercentDone = CalcPercentDone(i, MovieCache.Count)
            ReportProgress("Rebuilding caches " & i & " of " & MovieCache.Count)

            'Dim m = New Movie(Me,movie.fullpathandfilename)

            'm.LoadNFO(False)
            'm.UpdateActorCacheFromEmpty()
            'm.UpdateDirectorCacheFromEmpty()
            For Each act In movie.Actorlist
                _actorDb.Add(New ActorDatabase(act.actorname, movie.id))
            Next
            If movie.MovieSet.MovieSetName.ToLower <> "-none-" Then _moviesetDb.Add(movie.MovieSet)
            Dim directors() As String = movie.director.Split("/")
            For Each d In directors
                _directorDb.Add(New DirectorDatabase(d.Trim, movie.id))
            Next

            If Cancelled Then Exit Sub
        Next

        If Cancelled Then Exit Sub

        'Dim q = From item In _tmpActorDb Select item.ActorName, item.MovieId
        'For Each item In q.Distinct()
        '    _actorDb.Add(New ActorDatabase(item.ActorName, item.MovieId))
        'Next

        'Dim q2 = From item In _tmpDirectorDb Select item.ActorName, item.MovieId
        'For Each item In q2.Distinct()
        '    _directorDb.Add(New DirectorDatabase(item.ActorName, item.MovieId))
        'Next

        'Dim q3 = From item In _tmpMoviesetDb Select item.MovieSetName, item.MovieSetId
        'For Each item In q3.Distinct()
        '    _moviesetDb.Add(New MovieSetDatabase(item.MovieSetName, item.MovieSetId))
        'Next
        
        SaveActorCache()
        SaveDirectorCache()
        SaveMovieSetCache()
    End Sub

    Sub RemoveMovieFromCache(fullpathandfilename)

        If fullpathandfilename = "" Then Exit Sub
        MovieCache             .RemoveAll(Function(c) c.fullpathandfilename = fullpathandfilename)
        Data_GridViewMovieCache.RemoveAll(Function(c) c.fullpathandfilename = fullpathandfilename)

        If Preferences.XbmcLinkReady Then
            Dim media As String = Utilities.GetFileName(fullpathandfilename,True)

            If media <> "none" Then
                Dim evt As New BaseEvent

                evt.E    = XbmcController.E.MC_Movie_Removed
                evt.Args = New VideoPathEventArgs(media, PriorityQueue.Priorities.medium)

                Form1.XbmcControllerQ.Write(evt)
            End If
        End If
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

#Region "  Music Video Routines"
        
    Public Sub FindNewMusicVideos(Optional scrape=True)
        NewMovies.Clear
        PercentDone = 0

        Dim folders As New List(Of String)

        AddOnlineFolders ( folders , Preferences.MVidFolders )
        'AddOfflineFolders( folders )

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
#End Region


#Region "Filters"

    Function ApplyCountiesFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb,ModuleExtensions.Missing,"")
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x) x.countriesList.Intersect(fi.Include).Any() )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not x.countriesList.Intersect(fi.Exclude).Any() )
        End If

        Return recs
    End Function


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

        Dim fi As New FilteredItems(ccb, "Missing", "")
       
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

    Function ApplyVideoCodecFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb,"Unknown","-1")
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x)     fi.Include.Contains(x.VideoCodec) )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not fi.Exclude.Contains(x.VideoCodec) )
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

        'Dim i As Integer = 0

        'For Each item As CCBoxItem In ccb.Items

        '    Dim value As String = item.Name.RemoveAfterMatch

        '    Select ccb.GetItemCheckState(i)
        '        Case CheckState.Checked   : recs = recs.Where ( Function(x)     x.Audio.Exists( Function(a) If(a.Language.Value="","Unknown",a.Language.Value)=value ))
        '        Case CheckState.Unchecked : recs = recs.Where ( Function(x) Not x.Audio.Exists( Function(a) If(a.Language.Value="","Unknown",a.Language.Value)=value ))
        '    End Select

        '    i += 1
        'Next

        Dim fi As New FilteredItems(ccb,"Unknown","")
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x) x.Languages.Intersect(fi.Include).Any() )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not x.Languages.Intersect(fi.Exclude).Any() )
        End If

        Return recs
    End Function


    Function ApplyAudioDefaultLanguagesFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )

        Dim fi As New FilteredItems(ccb,"Unknown","")
       
        If fi.Include.Count>0 Then
            recs = recs.Where( Function(x)     fi.Include.Contains(x.DefaultAudioTrack.Language.Value) )
        End If
        If fi.Exclude.Count>0 Then
            recs = recs.Where( Function(x) Not fi.Exclude.Contains(x.DefaultAudioTrack.Language.Value) )
        End If

        Return recs
    End Function


    Function ApplyActorsFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )
        Return ApplyPeopleFilter(ActorDb, recs, ccb)
    End Function

    Function ApplyDirectorsFilter( recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox )
        Return ApplyPeopleFilter(DirectorDb, recs, ccb)
    End Function

    Function ApplyPeopleFilter(PeopleDb As List(Of ActorDatabase), recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)

        Dim fi As New FilteredItems(ccb)

        If fi.Include.Count>0 Then 
            Dim MovieIds = (From a In PeopleDb Where fi.Include.Contains(a.ActorName) Select a.MovieId).ToList
                             
            recs = recs.Where( Function(x)     MovieIds.Contains(x.id) )
        End If

        If fi.Exclude.Count>0 Then 
            Dim MovieIds = (From a In PeopleDb Where fi.Exclude.Contains(a.ActorName) Select a.MovieId).ToList
                             
            recs = recs.Where( Function(x) Not MovieIds.Contains(x.id) )
        End If

        Return recs
    End Function

    Function ApplyPeopleFilter(PeopleDb As List(Of DirectorDatabase), recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)

        Dim fi As New FilteredItems(ccb)

        If fi.Include.Count>0 Then 
            Dim MovieIds = (From a In PeopleDb Where fi.Include.Contains(a.ActorName) Select a.MovieId).ToList
                             
            recs = recs.Where( Function(x)     MovieIds.Contains(x.id) )
        End If

        If fi.Exclude.Count>0 Then 
            Dim MovieIds = (From a In PeopleDb Where fi.Exclude.Contains(a.ActorName) Select a.MovieId).ToList
                             
            recs = recs.Where( Function(x) Not MovieIds.Contains(x.id) )
        End If

        Return recs
    End Function

    Function ApplyTagsFilter(ByVal recs As IEnumerable(Of Data_GridViewMovie), ByVal ccb As TriStateCheckedComboBox)
        Dim i As Integer = 0

        For Each item As CCBoxItem In ccb.Items

            Dim value As String = item.Name.RemoveAfterMatch

            Select Case ccb.GetItemCheckState(i)
                Case CheckState.Checked : recs = (From m In recs Where m.movietag.Contains(value)).ToList
                Case CheckState.Unchecked : recs = (From m In recs Where Not m.movietag.Contains(value)).ToList
            End Select

            i += 1
        Next

        Return recs
    End Function

    Function ApplySourcesFilter(ByVal recs As IEnumerable(Of Data_GridViewMovie), ByVal ccb As TriStateCheckedComboBox)
        Dim fi As New FilteredItems(ccb)

        If fi.Include.Count > 0 Then
            recs = recs.Where(Function(x) fi.Include.Contains(x.source))
        End If

        If fi.Exclude.Count > 0 Then
            recs = recs.Where(Function(x) Not fi.Exclude.Contains(x.source))
        End If

        Return recs
    End Function

    Function ApplySubtitleLangFilter(recs As IEnumerable(Of Data_GridViewMovie), ccb As TriStateCheckedComboBox)
        Dim i As Integer = 0
        For Each item As CCBoxItem In ccb.Items
            Dim value As String = item.Name.RemoveAfterMatch
            Select ccb.GetItemCheckState(i)
                Case CheckState.Checked   : recs = recs.Where ( Function(x)     x.SubLang.Exists( Function(a) If(a.Language.Value="","Unknown",a.Language.Value)=value ))
                Case CheckState.Unchecked : recs = recs.Where ( Function(x) Not x.SubLang.Exists( Function(a) If(a.Language.Value="","Unknown",a.Language.Value)=value ))
            End Select
            i += 1
        Next
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

    Property Xbmc_DifferentTitles As List(Of String)

    Sub Handle_XbmcMcMoviesChanged
        For Each m In MovieCache
            Try
                m.XbmcMovie = XbmcMcMovies(m.MoviePathAndFileName.ToUpper)
            Catch
            End Try
        Next
        Xbmc_DifferentTitles = (From x In MovieCache Where Not IsNothing(x.XbmcMovie) AndAlso Not x.XbmcMovie.title=x.title Select x.MoviePathAndFileName).ToList
    End Sub

    Sub Handle_XbmcOnlyMoviesChanged
        
    End Sub

#End Region

End Class
