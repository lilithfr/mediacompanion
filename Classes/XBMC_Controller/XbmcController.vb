Imports System
Imports Sanford.StateMachineToolkit
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports XBMC.JsonRpc
Imports log4net
Imports System.Reflection
Imports System.Linq

Public Class XbmcController : Inherits PassiveStateMachine(Of S, E, EventArgs)

    Const Error_Unknown = "**UNKNOWN**"
 
    Public Property MovieFolderMappings As New Dictionary(Of String, String)

    Property Parent As Form1
    Public Shared log As ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

    Public Property McMoviePath  As String
    Public Property XbMoviePath  As String

    Public Property ErrorCount As Integer

    Public ReadOnly Property MovieFolder As String
        Get
            Return Path.GetDirectoryName(McMoviePath)
        End Get
    End Property

    Public Q       As PriorityQueue '(Of Integer, Object)
    Public BufferQ As PriorityQueue

    Private _xbmcJson As XbmcJson

    Public ReadOnly Property XbmcJson As XbmcJson
        Get
            If IsNothing(_xbmcJson) Then
                _xbmcJson = New XbmcJson
            End If

            Return _xbmcJson
        End Get
    End Property

    'Public Property MoviesToAdd      As List(Of NfoEventArgs) = New List(Of NfoEventArgs)
    'Public Property NewMovieIdsToGet As List(Of NfoEventArgs) = New List(Of NfoEventArgs)
    Public Property Bw               As BackgroundWorker = Nothing
    Public Property LastState        As S

    Public Property LastArgs As TransitionEventArgs(Of S, E, EventArgs)


    'Public ReadOnly Property PercentageToUpdate As Integer
    '    Get
    '        Return Math.Round((MoviesToAdd.Count * 100) / XbmcJson.XbmcMovies.Count)

    '    End Get
    'End Property

    'Public ReadOnly Property PercentageNewMovieIdsToGet As Integer
    '    Get
    '        Return Math.Round((NewMovieIdsToGet.Count * 100) / XbmcJson.XbmcMovies.Count)

    '    End Get
    'End Property

    'Public Property ScanAllThreshold As Integer = 1

    Public Property ShutDownRequested As Boolean = False

'    Private GetNewMovieIds_IdleTimer As Timers.Timer = New Timers.Timer()
    'Private FolderScan_IdleTimer     As Timers.Timer = New Timers.Timer()

    Private _Timer As Timers.Timer = New Timers.Timer()


    Public Enum S
        Any
        NotConnected
        Wf_XBMC_Movies_PreMap
        Wf_XBMC_Movies
        Wf_XBMC_ConnectResult
        Wf_XBMC_Video_Removed
        Wf_XBMC_Video_Updated
        Wf_XBMC_Video_Id
        Wf_XBMC_Video_ScanFinished
        Wf_XBMC_All_Video_Details
        Ready
    End Enum

    Public Enum E
        MC_ConnectReq
        MC_Movie_Updated
        MC_Movie_New
        MC_ShutDownReq
        MC_ResetErrorCount

        MC_FetchAllMovieDetails
        MC_AllMovieDetails

        TimeOut
        Success
        Failure

  '      GetNewMovieIds
        ScanFolder
        TurnOff
        FetchVideoInfo

        XBMC_Video_Removed
        XBMC_Video_Updated
        XBMC_Video_ScanFinished
        XBMC_System_Quit
        XBMC_Unknown_Event

    End Enum


    Sub New(Parent As Form1, Optional bw As BackgroundWorker = Nothing) '(Of Integer, Object))
        Me.Parent  = Parent
        Me.Q       = Form1.XbmcControllerQ
        Me.BufferQ = Form1.XbmcControllerBufferQ
        Me.Bw = bw

        Utilities.SafeDeleteFile( Path.Combine(My.Application.Info.DirectoryPath,Form1.XBMC_Controller_log_file) )
        log4net.Config.XmlConfigurator.Configure

 '       Ini_Timer(GetNewMovieIds_IdleTimer)
 '       Ini_Timer(FolderScan_IdleTimer)

  '      AddHandler GetNewMovieIds_IdleTimer.Elapsed         , AddressOf GetNewMovieIds_IdleTimer_Elapsed
  '      AddHandler FolderScan_IdleTimer    .Elapsed         , AddressOf FolderScan_IdleTimer_Elapsed


        Ini_Timer(_Timer)
        AddHandler _Timer.Elapsed, AddressOf Timer1sec_Elapsed


'       AddHandler XbmcJson.xbmc.Library.Video.Updated, AddressOf XBMC_Video_Updated
        AddHandler XbmcJson.xbmc.Library.Video.Removed      , AddressOf XBMC_Video_Removed
        AddHandler XbmcJson.xbmc.Library.Video.ScanFinished , AddressOf XBMC_Video_ScanFinished
        AddHandler XbmcJson.xbmc.System.Quit                , AddressOf XBMC_System_Quit
        AddHandler XbmcJson.xbmc.Log                        , AddressOf XBMC_System_Log
        AddHandler XbmcJson.xbmc.LogError                   , AddressOf XBMC_System_LogError

        AddHandler TransitionDeclined                       , AddressOf UnexpectedEvent
        AddHandler BeginDispatch                            , AddressOf BegnDispatch 

        '  Me. (StateId.Connected).EntryHandler += EnterOff
        ' this[StateID.Off].ExitHandler += ExitOff


        SetupSubstates(S.Any, HistoryType.Shallow,  S.NotConnected,
                                                    S.Wf_XBMC_Movies_PreMap,
                                                    S.Wf_XBMC_Movies,
                                                    S.Wf_XBMC_ConnectResult,
                                                    S.Wf_XBMC_Video_Removed,
                                                    S.Wf_XBMC_Video_Updated,
                                                    S.Wf_XBMC_Video_Id,
                                                    S.Wf_XBMC_Video_ScanFinished,
                                                    S.Wf_XBMC_All_Video_Details,
                                                    S.Ready)


        AddTransition( S.Any                        , E.MC_ShutDownReq          , S.Any                        , AddressOf ShutDown             )
        AddTransition( S.Any                        , E.MC_ResetErrorCount      , S.Any                        , AddressOf ResetErrorCount      )
        AddTransition( S.Any                        , E.XBMC_System_Quit        , S.NotConnected               , AddressOf Start1SecTimer       )  

        AddTransition( S.NotConnected               , E.MC_ConnectReq           , S.Wf_XBMC_ConnectResult      , AddressOf Connect              )
        AddTransition( S.NotConnected               , E.TimeOut                 , S.Wf_XBMC_ConnectResult      , AddressOf Connect              )

        AddTransition( S.Wf_XBMC_ConnectResult      , E.Failure                 , S.NotConnected               , AddressOf Start1SecTimer       )      
        AddTransition( S.Wf_XBMC_ConnectResult      , E.Success                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )

        AddTransition( S.Wf_XBMC_Movies_PreMap      , E.Failure                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )
        AddTransition( S.Wf_XBMC_Movies_PreMap      , E.TimeOut                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )
'       AddTransition( S.Wf_XBMC_Movies_PreMap      , E.Success                 , S.Ready                      , AddressOf FetchMaxMovies       )
        AddTransition( S.Wf_XBMC_Movies_PreMap      , E.Success                 , S.Ready                      , AddressOf AutoMapMovieFolders _
                                                                                                               , AddressOf Ready                )

        AddTransition( S.Wf_XBMC_Movies             , E.Failure                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )
        AddTransition( S.Wf_XBMC_Movies             , E.TimeOut                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )
        AddTransition( S.Wf_XBMC_Movies             , E.Success                 , S.Ready                      , AddressOf Ready                )
                                                                                                                                              
        AddTransition( S.Ready                      , E.MC_Movie_Updated        , S.Wf_XBMC_Video_Removed      , AddressOf RemoveVideo          )
        AddTransition( S.Wf_XBMC_Video_Removed      , E.TimeOut                 , S.Wf_XBMC_Video_Removed      , AddressOf RemoveVideo          )
        AddTransition( S.Wf_XBMC_Video_Removed      , E.XBMC_Video_Removed      , S.Ready                      , AddressOf Ready                )
                                                                                                                                              
        AddTransition( S.Ready                      , E.MC_Movie_New            , S.Ready                      , AddressOf AddFolderToScan      )
                                                                                                                                               
        AddTransition( S.Ready                      , E.ScanFolder              , S.Wf_XBMC_Video_ScanFinished , AddressOf ScanFolder           )
        AddTransition( S.Wf_XBMC_Video_ScanFinished , E.TimeOut                 , S.Wf_XBMC_Video_ScanFinished , AddressOf ScanFolder           )
        AddTransition( S.Wf_XBMC_Video_ScanFinished , E.XBMC_Video_ScanFinished , S.Ready                      , AddressOf AddFetchVideoInfo    )
                                                                                                                                              
        AddTransition( S.Ready                      , E.FetchVideoInfo          , S.Wf_XBMC_Movies             , AddressOf FetchMoviesInfo      )
                                                                                                                                              
        AddTransition( S.Ready                      , E.MC_FetchAllMovieDetails , S.Ready                      , AddressOf FetchMaxMovies       )
                                                                                                                                              
        '                                                                                                                                     
'       AddTransition( S.Ready                      , E.MC_Movie_New            , S.Wf_XBMC_Video_ScanFinished , AddressOf AddMovie             )
    '   AddTransition( S.Wf_XBMC_Video_Removed      , E.XBMC_Video_Removed      , S.Wf_XBMC_Video_ScanFinished , AddressOf AddMovie             )
                                                                                                                                              
 '      AddTransition( S.Wf_XBMC_Video_Removed      , E.MC_Movie_Updated        , S.Wf_XBMC_Video_Removed      , AddressOf AddMovie             )
                                                                                                                                              
                                                                                                                                              
  '      AddTransition( S.Ready                 , E.GetNewMovieIds          , S.Ready                 , AddressOf GetNewMovieIds )
  '      AddTransition( S.Ready                 , E.ScanForContentReq       , S.Ready                 , AddressOf ScanForContent )            
                                                                                                                                              
                                                                                                                                              
        Initialize(S.NotConnected)                                                                                                           
    End Sub                                                                                                                                  
   
                                                                                                                                   
    Sub UnexpectedEvent(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))

        Dim ea As BaseEventArgs = e.EventArgs

        If e.EventID.ToString.IndexOf("MC_") = 0 Then
            'ReportProgress("Buffering MC request",e)
            AppendLog("Buffering MC request : [" + e.EventID.ToString + "] while in State : [" + e.SourceStateID.ToString + "]")
            BufferQ.Write( New BaseEvent(e.EventID, e.EventArgs) )
            Return
        End If

        AppendLog("UnexpectedEvent - State : [" + e.SourceStateID.ToString + "] Missing event handler for : [" + e.EventID.ToString + "]")

    End Sub


    Sub BegnDispatch(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))
        LastArgs = e
    End Sub 

    Sub Start1SecTimer(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))
        StartTimer(1000)
    End Sub 

    Sub Start5SecTimer(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))
        StartTimer(5000)
    End Sub 

    Sub StartTimer(Interval As Integer)
        _Timer.Interval = Interval
        _Timer.Start()
    End Sub 


    Sub ReportProgress(ByVal Action As String, evt As TransitionEventArgs(Of S, E, EventArgs)) ', Optional ErrorMsg As String=Nothing)

        AppendLog(Action)

        Dim p As New XBMC_Controller_Progress

        p.Action       = Action
        p.BufferQcount = BufferQ.Count
     '  p.ErrorMsg     = ErrorMsg
        p.Evt          = evt.EventID
        p.Args         = evt.EventArgs
        p.LastState    = LastState
        p.CurrentState = evt.SourceStateID
        p.Qcount       = Q.Count
        p.Severity     = "I"
        p.ErrorCount   = ErrorCount

        ReportProgress(p)
    End Sub

    Sub Go()
        While Not ShutDownRequested

            If Q.Count = 0 And Me.CurrentStateID = S.Ready And BufferQ.Count > 0 Then
                Dim Evt As BaseEvent = BufferQ.Read() 
                AppendLog("Unbuffering Event : [" & Evt.E.ToString & "]")
                ProcessEvent(Evt)
            Else
                'ProcessEvent(Q.Dequeue)
                ProcessEvent(Q.Read())
            End If

        End While
    End Sub


    Sub ProcessEvent(Evt As BaseEvent)
        LastState = Me.CurrentStateID
    '    ReportProgress("Dispatching Event")
        AppendLog("Dispatching Event : [" & Evt.E.ToString & "]")
        Dispatch(Evt.E, Evt.Args)
    End Sub


    Sub ShutDown(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("Shutting down...",args)
        ShutDownRequested = True
    End Sub

    Sub Connect(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Connecting...",args)

        '      Send     (   IIf(XbmcJson.Open,E.Success,E.Failure) )
        '       Q.Enqueue(1, New CompleteEvent(IIf(XbmcJson.Open,E.Success,E.Failure)) )
        Q.Write(IIf(XbmcJson.Open, E.Success, E.Failure), PriorityQueue.Priorities.high )
    End Sub


    ' 
    ' How many movies in directory?...
    ' 
    ' 1 -> 

    'Sub AddFolderToScan(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    DecodeNfoEventArgs(args)
       
    '    Dim got = From i In Q.View Where i.E = E.ScanFolder AndAlso DirectCast(i.Args,FolderEventArgs).Folder=NfoFolder
              
    '    If got.Count=0 Then
    '        BufferQ.Write(New BaseEvent(E.ScanFolder, New FolderEventArgs(NfoFolder,PriorityQueue.Priorities.low)))
    '    End If
    'End Sub

    Sub AddFetchVideoInfo(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        Dim got = From i In BufferQ.View Where i.E = E.FetchVideoInfo 
      
        If got.Count=0 Then
            BufferQ.Write(E.FetchVideoInfo,PriorityQueue.Priorities.lowest)
        End If
    End Sub

    Sub FetchMoviesInfo(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Fetching movies info...",args)
        Start5SecTimer(sender,args)
        Q.Write(IIf(XbmcJson.GetXbmcMovies, E.Success, E.Failure), PriorityQueue.Priorities.medium )
    End Sub

    Sub FetchMaxMovies(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Fetching full movie details for all movies...", args)

        Dim MaxXbmcMovies As List(Of MaxXbmcMovie) = XbmcJson.xbmc.Library.Video.GetMaxXbmcMovies()

        Dim msg = New XBMC_Movies_EventArgs(MaxXbmcMovies)

        ReportProgress(E.MC_AllMovieDetails,msg)
    End Sub


    Sub ReportProgress(EventId As XbmcController.E,  Args As EventArgs)

        Dim p As New XBMC_Controller_Progress

        p.Evt  = EventId
        p.Args = Args

        ReportProgress(p)
    End Sub







    'Sub FetchMovieInfo(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    ReportProgress("Fetching movie info for : " + Title,args)

    '    Q.Write(IIf(XbmcJson.UpdateXbmcMovies(Title), E.Success, E.Failure), PriorityQueue.Priorities.medium )
    'End Sub


    Sub Ready(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("Ready & waiting...",args)
        _Timer.Stop
    End Sub





    'Sub RemoveThenAddMovie(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    ReportProgress( "RemoveThenAddMovie" )

    '    RemoveMovie(sender, args)
    '    'AddMovie   (sender, args)

    '    'Dim ea As NfoEventArgs = args.EventArgs

    '    'MoviesToAdd.Add(ea)

    '    'If Q.Count = 0 Then

    '    '    If PercentageToUpdate >= ScanAllThreshold Then
    '    '        Q.Write(PriorityQueue.Priorities.low, New CompleteEvent(E.ScanForContent))
    '    '    Else
    '    '        For Each movie In MoviesToAdd
    '    '            Q.Write(PriorityQueue.Priorities.low, New CompleteEvent(E.Movie_New, movie)) 'New NfoEventArgs(movie.Nfo,movie.Title)) ) 
    '    '        Next
    '    '    End If
    '    '    MoviesToAdd.Clear()
    '    'End If
    'End Sub


    Sub RemoveVideo(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        DecodeNfoEventArgs(args)

        Dim Title       As String  = ""
        Dim xbmcMovieId As Integer = -1

        Try
            Title = XbmcJson.GetMinXbmcMovie(XbMoviePath).title
        Catch
        End Try

        If Title<>"" Then
            ReportProgress("Removing : " + Title,args)
            
            Try
                xbmcMovieId = XbmcJson.GetMinXbmcMovie(XbMoviePath).movieid
            Catch
                xbmcMovieId = -1 
            End Try
        End If

        If xbmcMovieId > -1 Then
            XbmcJson.xbmc.Library.Video.RemoveMovie(xbmcMovieId)
            Start5SecTimer(sender,args)
            XbmcJson.RemoveXbmcMovie(XbMoviePath)
        Else
            'ErrorCount = ErrorCount + 1
            ReportProgress("Failed to find movieid for [" & McMoviePath & "]",args) 'This can happen if not already in XBMC
            Q.Write(E.XBMC_Video_Removed,PriorityQueue.Priorities.high)
        End If

        AddFolderToScan(sender,args)
    End Sub


    Sub ResetErrorCount(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ErrorCount = 0
    End Sub


    Sub AddFolderToScan(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        DecodeNfoEventArgs(args)
       
        Dim got = From i In BufferQ.View Where i.E = E.ScanFolder AndAlso DirectCast(i.Args,FolderEventArgs).Folder=MovieFolder
              
        If got.Count=0 Then
            BufferQ.Write(New BaseEvent(E.ScanFolder, New FolderEventArgs(MovieFolder,PriorityQueue.Priorities.low)))
        End If
    End Sub


    Sub ScanFolder(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        Dim ea As FolderEventArgs = args.EventArgs

        Dim scanFolder = ea.Folder
        Dim Interval = 5000

        '
        ' Scanning named source folders doesn't work because it doesn't scan the sub-folders [XBMC bug]
        ' See: http://forum.xbmc.org/showthread.php?tid=158772
        ' MilhouseVH's comments describes this issue exactly.
        '

        'Dim numScanFolderReqs = (From i In BufferQ.View Where i.E = E.ScanFolder).Count 

        ''Dim numDiffFolders    = (From i In Q.View Where i.E = E.ScanFolder _
        ''                        Select 
        ''                            Folder=DirectCast(i.Args,FolderEventArgs).Folder 
        ''                        Group By 
        ''                            Folder Into Num=Count).Count


        'If numScanFolderReqs>10 Then
          
        '    If scanFolder.IndexOf(Path.DirectorySeparatorChar) > 0 Then

        '        numScanFolderReqs = NumMatchingFolders(Directory.GetParent(scanFolder).FullName)
                

        '        If numScanFolderReqs>10  Then

        '            Interval = Math.Min( (numScanFolderReqs*1000)+4000, 120000 )

        '            scanFolder = Directory.GetParent(scanFolder).FullName
        '            BufferQ.Delete(New BaseEvent(E.ScanFolder, New FolderEventArgs(scanFolder,PriorityQueue.Priorities.low)))
        '        End If

        '    End If

        'End If


        'If numFolders>10 Then
          
        '    scanFolder = Nothing
        '    Q      .Delete(E.ScanFolder)
        '    BufferQ.Delete(E.ScanFolder)
        '    ReportProgress("Scanning folders...",args)
        'Else
        '    ReportProgress("Scanning folder: " + scanFolder,args)
        'End If

        'BufferQ.Delete(New BaseEvent(E.ScanFolder, New FolderEventArgs(scanFolder,PriorityQueue.Priorities.low)))


        StartTimer(Interval)

        ReportProgress("Scanning folder: " + scanFolder,args)
        XbmcJson.xbmc.Library.Video.AddMovies(scanFolder)

        '     NewMovieIdsToGet.Add(ea)
        '     Restart_Timer(GetNewMovieIds_IdleTimer)
    End Sub

    'Function GetMatchingScanFolderItems(x As String) 
    '    Return From i In Q.View 
    '           Where 
    '                i.E = E.ScanFolder _
    '            AndAlso
    '                DirectCast(i.Args,FolderEventArgs).Folder.Contains(x)
    'End Function

    'Function NumMatchingFolders(x As String) As Integer
    '    Return (From i In BufferQ.View Where i.E = E.ScanFolder _
    '        Select 
    '            Folder=DirectCast(i.Args,FolderEventArgs).Folder 
    '        Where
    '            Folder.Contains(x)).Count
    'End Function


    Sub DecodeNfoEventArgs(args As TransitionEventArgs(Of S, E, EventArgs))
        Dim ea As VideoPathEventArgs = args.EventArgs

        McMoviePath = ea.McMoviePath

        XbMoviePath = Error_Unknown

        For Each Folder In Preferences.movieFolders
            If McMoviePath.ToUpper.StartsWith(Folder.ToUpper) Then

                Dim file As String = Right(McMoviePath,McMoviePath.Length-Folder.Length)

                If file.StartsWith(Path.DirectorySeparatorChar) Then
                    file = file.Remove(0,1)
                End If

                XbMoviePath = Path.Combine(MovieFolderMappings(Folder),file )
                Return
            End If
        Next

   '     ReportProgress ... ERROR - Missing XBMC movie path
    End Sub

    'Sub GetNewMovieIds(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    AppendLog("GetNewMovieIds")

    '    'If PercentageNewMovieIdsToGet >= ScanAllThreshold Then
    '    XbmcJson.XbmcMovies.Clear()
    '    XbmcJson.UpdateXbmcMovies()
    '    'Else
    '    '    For Each ea As NfoEventArgs In NewMovieIdsToGet
    '    '        XbmcJson.UpdateXbmcMovies(ea.Title)
    '    '    Next
    '    'End If

    'End Sub


    '  'From: http://forum.xbmc.org/showthread.php?pid=1428281
    '  '
    '  ' Batch commands ARE supported, BUT CAN BE EXECUTED IN ANY ORDER.
    '  '
    '  Sub AddMovie(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '      Dim ea As NfoEventArgs = args.EventArgs

    '      Dim folder As String = Path.GetDirectoryName(ea.Nfo)

    '      'JObject GetCall(string method, object args)

    '      Dim call_1 As JObject = XbmcJson.xbmc.Library.Video.GetCall_AddMovies    (folder)
    '      Dim call_2 As JObject = XbmcJson.xbmc.Library.Video.GetCall_MinXbmcMovies(ea.Title)

    '      'Dim main As List(Of String) = New List(Of String)

    '      'main.Add(call_1.ToString)
    '      'main.Add(call_2.ToString)

    ''     Dim json = JsonConvert.SerializeObject(main)

    '      Try
    '          Dim json = "[" + call_1.ToString + "," + call_2.ToString + "]"

    '          Dim o = XbmcJson.xbmc.Library.client.SendCall(json)

    '          'Dim m As JArray = new JArray()
    '          'm.Add(call_1)
    '          'm.Add(call_2)
    '          'Dim o = XbmcJson.xbmc.Library.client.SendCall(m)

    '          Console.Write(o.ToString + Environment.NewLine)
    '      Catch ex As Exception
    '          Console.Write(ex.ToString + Environment.NewLine)
    '      End Try
    '  End Sub





    'Sub ScanForContent(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    AppendLog("ScanForContent")

    '    XbmcJson.xbmc.Library.Video.AddMovies()

    '    '.ScanForContent()

    '    Restart_Timer(GetNewMovieIds_IdleTimer)
    'End Sub


    'Private Sub GetNewMovieIds_IdleTimer_Elapsed(ByVal sender As Object, ByVal ev As Timers.ElapsedEventArgs)

    '    GetNewMovieIds_IdleTimer.Stop()
    '    AppendLog("GetNewMovieIds_IdleTimer Elapsed")

    '    Q.Write(E.GetNewMovieIds,PriorityQueue.Priorities.low)

    'End Sub


    'Private Sub FolderScan_IdleTimer_Elapsed(ByVal sender As Object, ByVal ev As Timers.ElapsedEventArgs)

    '    FolderScan_IdleTimer.Stop()
    '    AppendLog("FolderScan_IdleTimer Elapsed")

    '    Q.Write(E.ScanForContentReq,PriorityQueue.Priorities.low)
    'End Sub



    Private Sub Timer1sec_Elapsed(ByVal sender As Object, ByVal ev As Timers.ElapsedEventArgs)

        sender.Stop()
        AppendLog("Timer Elapsed")

        Q.Write(E.TimeOut,PriorityQueue.Priorities.low)
    End Sub


    Sub AppendLog(msg As String)
        log.Debug(" Q: " + Q.Count.ToString + " " + " Buffer Q: " + BufferQ.Count.ToString + " - " + msg)
    End Sub


    Function GetTimeStamp() As String
        Return Format(DateTime.Now, "HH:mm:ss.fff").ToString
    End Function

    Sub ReportProgress(ByVal oProgress As XBMC_Controller_Progress)
        Bw.ReportProgress(0, oProgress)
    End Sub


    Sub XBMC_System_Log(sender As Object, e As XbmcJsonRpcLogEventArgs)
        AppendLog("XbmcJsonRpc : " & e.Message)
    End Sub

    Private Sub XBMC_System_LogError(sender As Object, e As XbmcJsonRpcLogErrorEventArgs)
        'ErrorCount = ErrorCount + 1
'        ReportProgress("XbmcJsonRpc - ERROR : " & e.Message & " Exception : " & e.Exception.Message,LastArgs)
        ReportProgress(e.Message,LastArgs)
    End Sub

    Sub XBMC_Video_Updated(sender As Object, ea As EventArgs)
        Q.Write(E.XBMC_Video_Updated,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_Video_Removed(sender As Object, ea As EventArgs)
        Q.Write(E.XBMC_Video_Removed,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_Video_ScanFinished(sender As Object, ea As EventArgs)
        Q.Write(E.XBMC_Video_ScanFinished,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_System_Quit(sender As Object, ea As EventArgs)
        Q.Write(E.XBMC_System_Quit,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_UnknownEvent(sender As Object, ea As XbmcJsonRpcUnknownEventArgs)
        '       ReportProgress( "XBMC_UnknownEvent : [" & ea.Event & "]" )
        Q.Write(New BaseEvent(E.XBMC_Unknown_Event, New UnknownEventArgs(ea.Event,PriorityQueue.Priorities.high)))
    End Sub

    'Sub IniGetNewMovieIds_IdleTimer()
    '    GetNewMovieIds_IdleTimer.Stop()
    '    GetNewMovieIds_IdleTimer.Interval = 5000
    '    GetNewMovieIds_IdleTimer.AutoReset = True

    '    AddHandler GetNewMovieIds_IdleTimer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.GetNewMovieIds_IdleTimer_Elapsed)
    'End Sub


    Sub Ini_Timer(t As Timers.Timer,Optional Interval As Integer=1000)
        t.Stop()
        t.Interval = Interval
        t.AutoReset = True
    End Sub

    'Sub Restart_Timer(t As Timers.Timer)
    '    t.Stop()
    '    t.Start()
    'End Sub


    Sub AutoMapMovieFolders(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Mapping movie folders : ",args)

        MovieFolderMappings.Clear

        For Each folder In Preferences.movieFolders
            Dim moviePaths = (From 
                            m In Parent.oMovies.MoviesWithUniqueMovieTitles 
                        Where 
                            m.fullpathandfilename.ToUpper.Contains(folder.ToUpper)
                        Join
                            x In XbmcJson.MoviesWithUniqueMovieTitles 
                        On 
                            x.title Equals m.title And Path.GetFileName(x.file).ToUpper Equals Path.GetFileName(m.MoviePathAndFileName).ToUpper
                        Select
                            m.MoviePathAndFileName, x.file).First


            Dim mc = moviePaths.MoviePathAndFileName.ToUpper
            Dim xb = moviePaths.file.ToUpper

            'xb = xb.Replace(folder.ToUpper,"X:")       'Test to simulate a drive mapping


            If mc = xb Then
                MovieFolderMappings_Add(folder,folder)
            Else
                While mc.Length>folder.Length And mc.Chars(mc.Length-1) = xb.Chars(xb.Length-1)

                    mc = mc.Remove(mc.Length - 1)
                    xb = xb.Remove(xb.Length - 1)
                    
                End While

                MovieFolderMappings_Add(folder,xb)
            End If
        Next
    End Sub

    Sub MovieFolderMappings_Add( dirMc As String, dirXb As String )

        log.Debug("Mapping MC movie folder : [" + dirMc + "] to XBMC movie folder : [" + dirXb + "]")
        MovieFolderMappings.Add(dirMc,dirXb)
    End Sub

End Class


