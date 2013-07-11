﻿Imports System
Imports Sanford.StateMachineToolkit
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports XBMC.JsonRpc
Imports log4net
Imports System.Reflection
Imports System.Linq
Imports System.Data.SQLite

Public Class XbmcController : Inherits PassiveStateMachine(Of S, E, EventArgs)

    #Const BatchMode       = False
    #Const GenPseudoErrors = False

    Const Error_Prefix    = "**ERROR** "
    Const Warning_Prefix  = "**WARNING** "
 
    Public Property XbmcTexturesDb       As SQLiteConnection = new SQLiteConnection(Preferences.XBMC_TexturesDb_ConnectionStr)
    Public Property XbmcThumbnailsFolder As String = Preferences.XBMC_Thumbnails_Path

    Dim dtCachedUrls As DataTable

    Public Property MoviesInFolder As New Dictionary(Of String, Integer)

    Property BatchScanFolders As List(Of String) = New List(Of String)

    Public Property MovieFolderMappings As XBMC_MC_FolderMappings = Preferences.XBMC_MC_MovieFolderMappings

#If GenPseudoErrors Then
    Dim ScanRemoved_Count     As Integer =  0
    Dim ScanFinished_Count    As Integer =  0
    Dim ScanRemoved_MissRate  As Integer =  7
    Dim ScanFinished_MissRate As Integer = 13
#End If

    Property LastEvent As BaseEvent = New BaseEvent()

    Property Parent As Form1
    Public Shared log As ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

    Public Property McMoviePath  As String
    Public Property XbMoviePath  As String

    Public Property ErrorCount As Integer

    Private _maxXbmcMovies As List(Of XbmcMovieForCompare)

    Public Event MaxXbmcMoviesChanged
    Public Event XbmcMcMoviesChanged
    Public Event XbmcOnlyMoviesChanged

    Private _xbmcMovies     As Dictionary(Of String, XbmcMovieForCompare)
    Private _mcMovies       As Dictionary(Of String, ComboList          )
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
    

    Public Property MaxXbmcMovies As List(Of XbmcMovieForCompare)
        Get
            Return _maxXbmcMovies
        End Get
        Set
            _maxXbmcMovies = Value
            RaiseEvent MaxXbmcMoviesChanged
        End Set
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

    'ReadOnly Property MC_Only_Movies As List(Of String)
    '    Get
    '        Dim c As List(Of String)= (From
    '                    M In Parent.oMovies.MovieCache
    '                Select
    '                    M.MoviePathAndFileName).ToList

    '        Dim q2 As List(Of String) = (From
    '                    t In c
    '                Select
    '                    t
    '                Where
    '                    Not XBMC_to_MC_MoviePaths.Contains(t.ToUpper)).ToList


    '        Return q2
    '    End Get
    'End Property

    ReadOnly Property MC_Only_Movies As List(Of ComboList)
        Get
            Dim q2 = From
                        M In Parent.oMovies.MovieCache
                     Where
                        Not XBMC_to_MC_MoviePaths.ContainsValue(M.MoviePathAndFileName.ToUpper)

            Return q2.ToList
        End Get
    End Property


    ' Translates XBMC movie paths to their MC equivilant
    Private _XBMC_to_MC_MoviePaths As Dictionary(Of String,String)

    Public ReadOnly Property XBMC_to_MC_MoviePaths As Dictionary(Of String,String)
        Get
            Return _XBMC_to_MC_MoviePaths
        End Get
    End Property    


   Sub UpdateXBMC_to_MC_MoviePaths(XbmcMovies As List(Of MinXbmcMovie))

 '       Dim q2 = (From x In XbmcJson.XbmcMovies).ToDictionary(Function(p) p.file.ToUpper,Function(p) MovieFolderMappings.GetMC_MoviePath(p.file))

        Dim q2 = XbmcJson.XbmcMovies.ToDictionary(Function(p) p.file.ToUpper,Function(p) MovieFolderMappings.GetMC_MoviePath(p.file))

        _XBMC_to_MC_MoviePaths = q2
    End Sub    

    Sub SetXbmcMaxMoviesDirty        
        XbmcMaxMoviesDirty = True
    End Sub


    ReadOnly Property MC_Only_MovieFolders As List(Of String)
        Get
            Dim q2 = From
                        M In MC_Only_Movies
                    Select
                        MovieFolderMappings.GetMC_MovieFolder(M.MoviePathAndFileName)
                    Distinct

            Return q2.ToList
        End Get
    End Property


    Private _GotDbAccess     As Boolean = CanConnect
    Private _GotFolderAccess As Boolean = Directory.Exists(XbmcThumbnailsFolder)

    Public ReadOnly Property CanDeleteCachedImages As Boolean
        Get
            Return _GotDbAccess And _GotFolderAccess
        End Get
    End Property


    Property XbmcMaxMoviesDirty As Boolean = True


    Private TO_Timer       As Timers.Timer = New Timers.Timer()
 '   Private WatchDogTimer As Timers.Timer = New Timers.Timer()
    Private MaxMovies_Idle_Timer     As Timers.Timer = New Timers.Timer()


    Public Enum S
        Any
        NotConnected
        Wf_XBMC_Movies_PreMap
        Wf_XBMC_Movies
        Wf_ReconnectOrNot
        Wf_XBMC_ConnectResult
        Wf_XBMC_Video_Removed
        Wf_XBMC_Video_Updated
        Wf_XBMC_Video_Id
        Wf_XBMC_Video_ScanFinished
        Wf_XBMC_All_Video_Details
        Ready
    End Enum

    Public Enum E

        ConnectReq
        TimeOut
        MaxMovies_Idle_TimeOut
        Success
        Failure
        Yes
        No
        NoMoreScanFolderReqs
  '      WatchDogTimeOut
  '      GetNewMovieIds
        ScanFolder
        TurnOff
        FetchVideoInfo


        MC_Movie_Updated
        MC_Movie_New
        MC_Movie_Removed
        MC_ScanForNewMovies
        MC_ShutDownReq
        MC_ResetErrorCount
 '       MC_FetchAllMovieDetails
        MC_Only_Movies
  '      MC_MaxMovieDetails

        MC_XbmcMcMovies
        MC_XbmcOnlyMovies

        MC_XbmcQuit

        XBMC_Video_Removed
        XBMC_Video_Updated
        XBMC_Video_ScanFinished
        XBMC_System_Quit
        XBMC_Unknown_Event

        JSON_Exception
        JSON_Error
        JSON_Abort
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


        Ini_Timer(TO_Timer)
        AddHandler TO_Timer.Elapsed, AddressOf Timer1sec_Elapsed

  '      Ini_Timer(MaxMovies_Idle_Timer)
  '      AddHandler MaxMovies_Idle_Timer.Elapsed, AddressOf MaxMovies_Idle_Timer_Elapsed

        'Ini_Timer(WatchDogTimer,10000)
        'AddHandler WatchDogTimer.Elapsed, AddressOf WatchDogTimer_Elapsed


'       AddHandler XbmcJson.xbmc.Library.Video.Updated, AddressOf XBMC_Video_Updated
        AddHandler Me.XbmcJson.xbmc.Library.Video.Removed      , AddressOf XBMC_Video_Removed
        AddHandler Me.XbmcJson.xbmc.Library.Video.ScanFinished , AddressOf XBMC_Video_ScanFinished
        AddHandler Me.XbmcJson.xbmc.System.Quit                , AddressOf XBMC_System_Quit
        AddHandler Me.XbmcJson.xbmc.Log                        , AddressOf XBMC_System_Log
        AddHandler Me.XbmcJson.xbmc.LogError                   , AddressOf XBMC_System_LogError
        AddHandler Me.XbmcJson.xbmc.Aborted                    , AddressOf XBMC_System_Aborted


        AddHandler Me.TransitionDeclined                       , AddressOf UnexpectedEvent
        AddHandler Me.TransitionCompleted                      , AddressOf HandleTransitionCompleted
        AddHandler Me.BeginDispatch                            , AddressOf BegnDispatch 
        AddHandler Me.ExceptionThrown                          , AddressOf ExceptionThrownHandler

        AddHandler Me.XbmcJson.XbmcMovies_OnChange             , AddressOf UpdateXBMC_to_MC_MoviePaths
        AddHandler Me.XbmcJson.MovieRemoved                    , AddressOf SetXbmcMaxMoviesDirty
        AddHandler Me.MaxXbmcMoviesChanged                     , AddressOf Handle_MaxXbmcMoviesChanged
        AddHandler Me.XbmcMcMoviesChanged                      , AddressOf Handle_XbmcMcMoviesChanged
        AddHandler Me.XbmcOnlyMoviesChanged                    , AddressOf Handle_XbmcOnlyMoviesChanged






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
  '     AddTransition( S.Any                        , E.WatchDogTimeOut         , S.Wf_XBMC_ConnectResult      , AddressOf Connect              )
        AddTransition( S.Any                        , E.MC_ResetErrorCount      , S.Any                        , AddressOf ResetErrorCount      )
        AddTransition( S.Any                        , E.XBMC_System_Quit        , S.NotConnected               , AddressOf Raise_XbmcQuit       )  
        AddTransition( S.Any                        , E.JSON_Abort              , S.Wf_XBMC_ConnectResult      , AddressOf Connect              )  

        AddTransition( S.NotConnected               , E.ConnectReq              , S.Wf_XBMC_ConnectResult      , AddressOf Connect              )
        AddTransition( S.NotConnected               , E.TimeOut                 , S.Wf_ReconnectOrNot          , AddressOf ReconnectOrNot       )

        AddTransition( S.Wf_ReconnectOrNot          , E.Yes                     , S.Wf_XBMC_ConnectResult      , AddressOf Connect              )
        AddTransition( S.Wf_ReconnectOrNot          , E.No                      , S.NotConnected               , AddressOf Ready                )

        AddTransition( S.Wf_XBMC_ConnectResult      , E.Failure                 , S.NotConnected               , AddressOf Start1SecTimer       )      
        AddTransition( S.Wf_XBMC_ConnectResult      , E.Success                 , S.Wf_XBMC_Movies             , AddressOf FetchMoviesInfo      )
        AddTransition( S.Wf_XBMC_ConnectResult      , E.JSON_Error              , S.Wf_XBMC_ConnectResult      , AddressOf Ready                )


'       AddTransition( S.Wf_XBMC_Movies_PreMap      , E.Failure                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )
'       AddTransition( S.Wf_XBMC_Movies_PreMap      , E.TimeOut                 , S.Wf_XBMC_Movies_PreMap      , AddressOf FetchMoviesInfo      )
''      AddTransition( S.Wf_XBMC_Movies_PreMap      , E.Success                 , S.Ready                      , AddressOf FetchMaxMovies       )
'       AddTransition( S.Wf_XBMC_Movies_PreMap      , E.Success                 , S.Ready                      , AddressOf AutoMapMovieFolders _

        AddTransition( S.Wf_XBMC_Movies             , E.Failure                 , S.Wf_XBMC_ConnectResult      , AddressOf ConnectAndRetry      )  
        AddTransition( S.Wf_XBMC_Movies             , E.TimeOut                 , S.Wf_XBMC_Movies             , AddressOf FetchMoviesInfo      )
        AddTransition( S.Wf_XBMC_Movies             , E.Success                 , S.Ready                      , AddressOf SendMcOnlyMovies     _
                                                                                                               , AddressOf Ready                )
                                                                                                                                              
        AddTransition( S.Ready                      , E.MC_Movie_Updated        , S.Wf_XBMC_Video_Removed      , AddressOf RemoveVideoThenAdd   )
        AddTransition( S.Wf_XBMC_Video_Removed      , E.TimeOut                 , S.Ready                      , AddressOf Retry                )
        AddTransition( S.Wf_XBMC_Video_Removed      , E.JSON_Exception          , S.Wf_XBMC_ConnectResult      , AddressOf ConnectAndRetry      )  

        AddTransition( S.Wf_XBMC_Video_Removed      , E.XBMC_Video_Removed      , S.Ready                      , AddressOf DeleteCachedImages   _
                                                                                                               , AddressOf Ready                )

        AddTransition( S.Ready                      , E.MC_Movie_New            , S.Ready                      , AddressOf AddFolderToScan      _
                                                                                                               , AddressOf Ready                )

        AddTransition( S.Ready                      , E.MC_Movie_Removed        , S.Wf_XBMC_Video_Removed      , AddressOf RemoveVideo          )
        AddTransition( S.Ready                      , E.MaxMovies_Idle_TimeOut  , S.Ready                      , AddressOf FetchMaxMovies       _
                                                                                                               , AddressOf Ready                )
                                                                                                                                       
#If BatchMode Then
        AddTransition( S.Ready                      , E.ScanFolder              , S.Ready                      , AddressOf AddFolderToBatchScan )
        AddTransition( S.Ready                      , E.NoMoreScanFolderReqs    , S.Wf_XBMC_Video_ScanFinished , AddressOf SendBatchScan        )
#Else
        AddTransition( S.Ready                      , E.ScanFolder              , S.Wf_XBMC_Video_ScanFinished , AddressOf ScanFolder           )
#End If



'       AddTransition( S.Wf_XBMC_Video_ScanFinished , E.TimeOut                 , S.Wf_XBMC_Video_ScanFinished , AddressOf AddFetchVideoInfo    ) '...ScanFolder...    )
        AddTransition( S.Wf_XBMC_Video_ScanFinished , E.TimeOut                 , S.Ready                      , AddressOf Retry                ) 
        AddTransition( S.Wf_XBMC_Video_ScanFinished , E.XBMC_Video_ScanFinished , S.Ready                      , AddressOf AddFetchVideoInfo    )
        AddTransition( S.Wf_XBMC_Video_ScanFinished , E.JSON_Exception          , S.Wf_XBMC_ConnectResult      , AddressOf ConnectAndRetry      ) 
                                                                                                                                              
        AddTransition( S.Ready                      , E.FetchVideoInfo          , S.Wf_XBMC_Movies             , AddressOf FetchMoviesInfo      )
                                                                                                                                              
 '       AddTransition( S.Ready                      , E.MC_FetchAllMovieDetails , S.Ready                      , AddressOf FetchMaxMovies       )
        AddTransition( S.Ready                      , E.MC_ScanForNewMovies     , S.Ready                      , AddressOf ScanNewMovies        )            
                                                                                                                                              
                                                                                                                                              
        '                                                                                                                                     
'       AddTransition( S.Ready                      , E.MC_Movie_New            , S.Wf_XBMC_Video_ScanFinished , AddressOf AddMovie             )
    '   AddTransition( S.Wf_XBMC_Video_Removed      , E.XBMC_Video_Removed      , S.Wf_XBMC_Video_ScanFinished , AddressOf AddMovie             )
 '      AddTransition( S.Wf_XBMC_Video_Removed      , E.MC_Movie_Updated        , S.Wf_XBMC_Video_Removed      , AddressOf AddMovie             )
  '      AddTransition( S.Ready                 , E.GetNewMovieIds          , S.Ready                 , AddressOf GetNewMovieIds )
                                                                                                                                              
        Initialize(S.NotConnected)   
    End Sub                                                                                                                                  
  



    Sub ExceptionThrownHandler(sender As Object, evt As TransitionErrorEventArgs(Of S, E, EventArgs))
        LogError("ExceptionThrownHandler", evt.Error.InnerException.Message, evt)
        Q.Write(e.JSON_Exception,PriorityQueue.Priorities.critical)
    End Sub


    Sub LogError(Action As String, ErrorMessage As String, evt As TransitionEventArgs(Of S, E, EventArgs) )

        AppendLog(Error_Prefix + ErrorMessage + " reported in " + Action)

        Dim p As New XBMC_Controller_Progress

        ErrorCount+=1

        p.Action       = Action
        p.BufferQcount = BufferQ.Count
        p.ErrorMsg     = ErrorMessage
        p.Evt          = evt.EventID
        p.Args         = evt.EventArgs
        p.LastState    = LastState
        p.CurrentState = evt.SourceStateID
        p.Qcount       = Q.Count
        p.Severity     = "E"
        p.ErrorCount   = ErrorCount

        ReportProgress(p)        
    End Sub


    Sub HandleTransitionCompleted(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))
 '       WatchDogTimer.Stop
        log.Debug("Transition Completed - State [" + e.SourceStateID.ToString + "] Event [" + e.EventID.ToString + "] Args [" + e.EventArgs.ToString + "]")
    End Sub
                                                                                                                                   
    Sub UnexpectedEvent(sender As Object, evt As TransitionEventArgs(Of S, E, EventArgs))

 '       WatchDogTimer.Stop

        If evt.EventID = E.ConnectReq Then Return

        Dim ea As BaseEventArgs = evt.EventArgs

        If evt.EventID.ToString.IndexOf("MC_") = 0 Then
            'ReportProgress("Buffering MC request",e)
            AppendLog("Buffering MC request : [" + evt.EventID.ToString + "] while in State : [" + evt.SourceStateID.ToString + "]")
            BufferQ.Write( New BaseEvent(evt.EventID, evt.EventArgs) )

            If evt.SourceStateID = S.NotConnected Then
                Q.Write(E.ConnectReq, PriorityQueue.Priorities.medium)
            End If
            Return
        End If

        AppendLog("UnexpectedEvent - State : [" + evt.SourceStateID.ToString + "] Missing event handler for : [" + evt.EventID.ToString + "]")
    End Sub


    Sub BegnDispatch(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))
        log.Debug("Begin Dispatch - State [" + e.SourceStateID.ToString + "] Event [" + e.EventID.ToString + "] Args [" + e.EventArgs.ToString + "]")
        LastArgs = e
  '     WatchDogTimer.Start
  '     TO_Timer.Stop
    End Sub 

    Sub Start1SecTimer(sender As Object, e As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("Starting 1 second timer",e)
        StartTimer(1000)
    End Sub 


    Sub StartTimer(Interval As Integer)
        TO_Timer.Stop
        TO_Timer.Interval = Interval
        TO_Timer.Start
    End Sub 

    Sub StartMaxMovies_Idle_Timer
        MaxMovies_Idle_Timer.Stop
        MaxMovies_Idle_Timer.Interval = 1000    '30000
        MaxMovies_Idle_Timer.Start
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
                AppendLog("Unbuffering Event : [" & Evt.Info & "]")
                ProcessEvent(Evt)
            Else
                'If Q.Count = 0 And Me.CurrentStateID = S.Ready And BufferQ.Count = 0 And XbmcMaxMoviesDirty And Not MaxMovies_Idle_Timer.Enabled Then
                '    StartMaxMovies_Idle_Timer
                'Else
                '    MaxMovies_Idle_Timer.Stop
                'End If
                'ProcessEvent(Q.Dequeue)
                ProcessEvent(Q.Read())
            End If

        End While
    End Sub


    Sub ProcessEvent(Evt As BaseEvent)

        If Evt.E.ToString.IndexOf("MC_") = 0 And CurrentStateID <> S.Ready Then

            If BufferQ.Exists(Evt) Then
                AppendLog("Discarding duplicate MC request : [" + Evt.CompareAs + "]")
            Else
                'ReportProgress("Buffering MC request",e)
                AppendLog("Buffering MC request : [" + Evt.Info + "] while in State : [" + CurrentStateID.ToString + "]")
                BufferQ.Write( New BaseEvent(Evt.E, Evt.Args) )
            End If

            If CurrentStateID = S.NotConnected Then
                Q.Write(E.ConnectReq, PriorityQueue.Priorities.medium)
            End If
                
            Return
        End If

        LastState = Me.CurrentStateID
    '    ReportProgress("Dispatching Event")
        AppendLog("Dispatching Event : [" & Evt.Info & "]")

        If Evt.E = E.ScanFolder or Evt.E=E.MC_Movie_Updated or Evt.E=E.MC_Movie_Removed Then
            LastEvent.Assign(Evt)
        End If

        Dispatch(Evt.E, Evt.Args)
    End Sub


    Sub ShutDown(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("Shutting down...",args)
        ShutDownRequested = True
    End Sub


    Sub ReconnectOrNot(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Reconnect or not...",args)
        Q.Write(IIf(Preferences.XBMC_Link, E.Yes, E.No), PriorityQueue.Priorities.high )
    End Sub

    Sub Connect(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Connecting...",args)

        '      Send     (   IIf(XbmcJson.Open,E.Success,E.Failure) )
        '       Q.Enqueue(1, New CompleteEvent(IIf(XbmcJson.Open,E.Success,E.Failure)) )
        Q.Write(IIf(XbmcJson.Open, E.Success, E.Failure), PriorityQueue.Priorities.high )
    End Sub


    Sub ConnectAndRetry(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Connect & retry...",args)
        Retry  (sender,args)

        XbmcJson.xbmc.Close

        Connect(sender,args)
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

        AssignLastEvent(args)

        ReportProgress("Fetching movies info...",args)
        StartTimer(5000)
        Q.Write(IIf(XbmcJson.GetXbmcMovies, E.Success, E.Failure), PriorityQueue.Priorities.medium )
    End Sub

    Sub AssignLastEvent(args As TransitionEventArgs(Of S, E, EventArgs))
        LastEvent.Assign(New BaseEvent(args.EventID, args.EventArgs))
    End Sub

    Sub FetchMaxMovies(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Fetching full movie details for all movies...", args)

        MaxXbmcMovies = XbmcJson.xbmc.Library.Video.GetMaxXbmcMovies(Preferences.XBMC_MC_CompareFields.Get_Xbmc_Fields)

  '      Dim msg = New XBMC_MaxMovies_EventArgs(MaxXbmcMovies)

  '      ReportProgress(E.MC_MaxMovieDetails,msg)
    End Sub

    Sub SendMcOnlyMovies(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Passing back Mc only movies", args)

        Dim msg = New ComboList_EventArgs(MC_Only_Movies)

        ReportProgress(E.MC_Only_Movies,msg)
    End Sub


    Sub ReportProgress(EventId As XbmcController.E,  Optional Args As EventArgs=Nothing)

        Dim p As New XBMC_Controller_Progress

        p.Evt  = EventId
        p.Args = Args

        ReportProgress(p)
    End Sub



    'Sub FetchMovieInfo(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    ReportProgress("Fetching movie info for : " + Title,args)

    '    Q.Write(IIf(XbmcJson.UpdateXbmcMovies(Title), E.Success, E.Failure), PriorityQueue.Priorities.medium )
    'End Sub

    Sub Ignore(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("Ignoring error as already handled elsewhere",args)
    End Sub

    Sub Ready(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("Ready & waiting...",args)
        TO_Timer.Stop
  '      WatchDogTimer.Stop
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

        GetCachedUrls

        Dim Title       As String  = ""
        Dim xbmcMovieId As Integer = -1

        If IsNothing(XbMoviePath) Then
            AppendLog(Error_Prefix + "Failed to map [" + McMoviePath + "] to XBMC folders. Check your XBMC_MC_FolderMappings in Comnfig.XML" )
            Q.Write(E.XBMC_Video_Removed,PriorityQueue.Priorities.high)
            Return
        End If

        Try
            Title = XbmcJson.GetMinXbmcMovie(XbMoviePath).title
        Catch
        End Try

        If Title<>"" Then
            ReportProgress("Removing : " + Title,args)
            
            Try
                'xbmcMovieId = XbmcJson.GetMinXbmcMovie(XbMoviePath).movieid

                Dim movies As List(Of MinXbmcMovie) = XbmcJson.xbmc.Library.Video.GetMinXbmcMovies(title)

                If movies.Count=1 Then
                    xbmcMovieId = movies(0).movieid
                Else
                    If movies.Count>1 Then
                        xbmcMovieId = (From x In movies Where MovieFolderMappings.GetMC_MoviePath(x.file).ToUpper=McMoviePath.ToUpper Select x.movieid).First
                    End If
                End If
            Catch
            End Try
        End If

        If xbmcMovieId > -1 Then
            XbmcJson.xbmc.Library.Video.RemoveMovie(xbmcMovieId)

            StartTimer(5000)

            XbmcJson.RemoveXbmcMovie(XbMoviePath)
        Else
            'ErrorCount = ErrorCount + 1
            ReportProgress("Failed to find movieid for [" & McMoviePath & "] - Probably new to XBMC",args) 'This can happen if not already in XBMC
            Q.Write(E.XBMC_Video_Removed,PriorityQueue.Priorities.high)
        End If
    End Sub

    Sub RemoveVideoThenAdd(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        RemoveVideo    (sender,args)
        AddFolderToScan(sender,args)
    End Sub


    Sub ResetErrorCount(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ErrorCount = 0
    End Sub

    Sub Raise_XbmcQuit(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        ReportProgress("XBMC is not longer running",args)
        ReportProgress(E.MC_XbmcQuit)      
   End Sub


    Sub AddFolderToScan(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        DecodeNfoEventArgs(args)
       
        If IsNothing(XbMoviePath) Then
            AppendLog(Warning_Prefix + "Can't add new movie, as XBMC is missing a source movie folder to access this folder : [" + Path.GetDirectoryName(McMoviePath) + "]" )
            Return
        End If

        Dim XbFolder As String = Path.GetDirectoryName(XbMoviePath)

        Dim got = From i In BufferQ.View Where i.E = E.ScanFolder AndAlso DirectCast(i.Args,FolderEventArgs).Folder=XbFolder
              
        If got.Count=0 Then
            BufferQ.Write(New BaseEvent(E.ScanFolder, New FolderEventArgs(XbFolder,PriorityQueue.Priorities.low)))
        End If

        If Not MoviesInFolder.ContainsKey(XbFolder) Then
            MoviesInFolder.Add(XbFolder,1)
        Else
            MoviesInFolder(XbFolder) = MoviesInFolder(XbFolder)+1
        End If
    End Sub


    Sub Retry(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))
        
        If LastEvent.Retries > 0 Then 
            ReportProgress("Event failed",args)
            Return
        End If


        Dim le As BaseEvent = New BaseEvent

        le.Assign(LastEvent)
        le.Retries = le.Retries + 1

        ReportProgress("Resubmitting last MC event : [" + le.E.ToString +"] Retry number : [" + le.Retries.ToString + "]",args)
        BufferQ.Write(le)
    End Sub


    Sub AddFolderToBatchScan(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Adding folder to batch scan...",args)

        Dim ea As FolderEventArgs = args.EventArgs

        BatchScanFolders.Add( XbmcJson.xbmc.Library.Video.GetCall_AddMovies(ea.Folder).ToString )

        If (From i In BufferQ.View Where i.E = E.ScanFolder).Count=0 Then
            Q.Write(E.NoMoreScanFolderReqs)
        End If
    End Sub


    Sub SendBatchScan(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Sending batch scan for "+ BatchScanFolders.Count.ToString + " folders...",args)

        Dim request As String=""
        Dim timeout = 4000

        For Each job In BatchScanFolders
            request += job+","
            timeout += 1000
        Next

        request = Left(request,Len(request)-1)

        StartTimer(timeout)

        If BatchScanFolders.Count>1 Then
            request = "[" + request + "]"
        End If

        log.Debug(request)

        Try
            XbmcJson.xbmc.Library.client.SendBatchCall(request)
        Catch ex As Exception
            log.Debug(ex.Message)
        End Try
        BatchScanFolders.Clear
    End Sub

    Sub ScanFolder(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        Dim ea As FolderEventArgs = args.EventArgs

        Dim scanFolder = ea.Folder
        Dim mif As Integer = 1


        If Not MoviesInFolder.ContainsKey(scanFolder) Then
            MoviesInFolder.Add(scanFolder,1)
            'LogError("ScanFolder","MoviesInFolder was missing : [" + scanFolder + "]", args)
        End If

        Try
            mif = MoviesInFolder(scanFolder)
        Catch ex As Exception
            LogError("ScanFolder","[" & ex.Message & "] was thrown looking up : [" + scanFolder + "] in MoviesInFolder", args)
        End Try

        Dim Interval = 5000 + (mif*1000)

        MoviesInFolder(scanFolder)=0

        Dim Total = Aggregate c In MoviesInFolder Into Sum(c.Value)

        If Total = 0 Then 
            MoviesInFolder.Clear
        End If

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

    'Property Mapped
    '            Dim moviePaths = (From
    '                            m In Parent.oMovies.MoviesWithUniqueMovieTitles
    '                        Where
    '                            m.fullpathandfilename.ToUpper.Contains(wotEver.ToUpper)
    '                        Join
    '                            x In XbmcJson.MoviesWithUniqueMovieTitles
    '                        On
    '                            x.title Equals m.title And Path.GetFileName(x.file).ToUpper Equals Path.GetFileName(m.MoviePathAndFileName).ToUpper
    '                        Select
    '                            m.MoviePathAndFileName, x.file).First


    Sub DecodeNfoEventArgs(args As TransitionEventArgs(Of S, E, EventArgs))
        Dim ea As VideoPathEventArgs = args.EventArgs

        McMoviePath = ea.McMoviePath

        XbMoviePath = MovieFolderMappings.GetXBMC_MoviePath(McMoviePath)

        If IsNothing(XbMoviePath) Then
            AppendLog(Error_Prefix + "Failed to map [" + McMoviePath + "] to XBMC folders. Check your XBMC_MC_FolderMappings in Comnfig.XML" )
        End If
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


    'Sub ScanNewMovies(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    Dim ea As ScanNewMoviesEventArgs = args.EventArgs

    '    Dim Interval As Integer = 5000 + ((ea.NumMovies)*1000)

    '    ReportProgress("Scanning for new movies in ALL movie folders...",args)

    '    XbmcJson.xbmc.Library.Video.AddMovies()

    '    StartTimer(Interval)
    'End Sub

    Sub ScanNewMovies(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        Try
            For Each movie In MC_Only_Movies

                Dim evt As New BaseEvent

                evt.E    = XbmcController.E.MC_Movie_Updated
                evt.Args = New VideoPathEventArgs(movie.MoviePathAndFileName, PriorityQueue.Priorities.medium)

                Q.Write(evt)
            Next
        Catch ex As Exception
            LogError("ScanNewMovies",ex.Message,args)
        End Try
        
    End Sub

    Function CanConnect As Boolean
        Try
            XbmcTexturesDb.Open
            XbmcTexturesDb.Close
            Return True
        Catch ex As Exception
            LogError("CanConnect - Failed to connect to XBMC Textures database. If this movie is re-added, Fanart & Poster changes will not appear in XBMC as the existing cached versions cannot be deleted",ex.Message,Nothing)
            Return False
        End Try
    End Function


    Sub GetCachedUrls

        If Not CanDeleteCachedImages Then Return

        XbmcTexturesDb.Open

        Dim oMovie As Movie = New Movie(McMoviePath,Me.Parent.oMovies)

        dtCachedUrls = DbUtils.ExecuteReader(XbmcTexturesDb,
                                                    "Select id, cachedurl from texture" +
                                                    " where url='" + DbUtils.Stuff(MovieFolderMappings.GetXBMC_MoviePath(oMovie.ActualPosterPath)) + "'" +
                                                       " or url='" + DbUtils.Stuff(MovieFolderMappings.GetXBMC_MoviePath(oMovie.ActualFanartPath)) + "'"
                                                    )

        XbmcTexturesDb.Close
    End Sub


    'Sub DeleteCachedFiles

    '    If Not CanDeleteCachedImages Then Return

    '    XbmcTexturesDb.Open

    '    Dim oMovie As Movie = New Movie(McMoviePath,Me.Parent.oMovies)

    '    dtCachedUrls = DbUtils.ExecuteReader(XbmcTexturesDb,
    '                                                "Select cachedurl from texture" +
    '                                                " where url='" + FolderMappings.GetXBMC_MoviePath(oMovie.ActualPosterPath) + "'" +
    '                                                   " or url='" + FolderMappings.GetXBMC_MoviePath(oMovie.ActualFanartPath) + "'"
    '                                                )

    '    DeleteCachedImages

    '    XbmcTexturesDb.Close
    'End Sub


    Sub DeleteCachedImages(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

        ReportProgress("Deleting orphaned movie images from thumbnail folder",args)

        If IsNothing(dtCachedUrls) OrElse dtCachedUrls.Rows.Count=0 or dtCachedUrls.Rows.Count>2 Then 
            AppendLog("Skipping cached file delete as expected 1-2 rows to be matched, but actually matched : [" + dtCachedUrls.Rows.Count.ToString + "]")
            dtCachedUrls = Nothing
            Return
        End If

        XbmcTexturesDb.Open

        For Each row In dtCachedUrls.Rows
            Dim filePath As String = Path.Combine(XbmcThumbnailsFolder,row("cachedurl").ToString.Replace("/","\"))

            If File.Exists(filePath) Then
                AppendLog("Deleting : [" + filePath + "]")
                Utilities.SafeDeleteFile(filePath)
            Else
                AppendLog("[" + filePath + "] not found")
            End If

            DbUtils.ExecuteNonQuery(XbmcTexturesDb, "Delete from texture where id=" + row("id").ToString)
        Next

        XbmcTexturesDb.Close

        dtCachedUrls = Nothing
    End Sub


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

        Q.Write(E.TimeOut,PriorityQueue.Priorities.high)
    End Sub


    Private Sub MaxMovies_Idle_Timer_Elapsed(ByVal sender As Object, ByVal ev As Timers.ElapsedEventArgs)

        sender.Stop()
        AppendLog("Idle Timer Elapsed")

        Q.Write(E.MaxMovies_Idle_TimeOut,PriorityQueue.Priorities.low)
    End Sub


    'Private Sub WatchDogTimer_Elapsed(ByVal sender As Object, ByVal ev As Timers.ElapsedEventArgs)

    '    sender.Stop()
    '    AppendLog("WatchDog Timer Elapsed!")

    '    Q.Write(E.WatchDogTimeOut,PriorityQueue.Priorities.high)
    'End Sub

    Sub AppendLog(msg As String)
        log.Debug(" Q: " + Q.Count.ToString + " " + " Buffer Q: " + BufferQ.Count.ToString + " - " + msg)
    End Sub


    Function GetTimeStamp() As String
        Return Format(DateTime.Now, "HH:mm:ss.fff").ToString
    End Function

    Sub ReportProgress(ByVal oProgress As XBMC_Controller_Progress)
        Bw.ReportProgress(0, oProgress)
    End Sub


    Sub XBMC_System_Log(sender As Object, evt As XbmcJsonRpcLogEventArgs)
        AppendLog("XbmcJsonRpc : " & evt.Message)
    End Sub

    Private Sub XBMC_System_LogError(sender As Object, evt As XbmcJsonRpcLogErrorEventArgs)
        'ErrorCount = ErrorCount + 1
'        ReportProgress("XbmcJsonRpc - ERROR : " & e.Message & " Exception : " & e.Exception.Message,LastArgs)
        If IsNothing(evt) Then
            ReportProgress("XbmcJsonRpc - Error received",LastArgs)
        Else
            ReportProgress("XbmcJsonRpc - Error received : [" + evt.Message + "]",LastArgs)
        End If

        Q.Write(e.JSON_Error,PriorityQueue.Priorities.critical)
    End Sub

    Private Sub XBMC_System_Aborted(sender As Object, evt As XbmcJsonRpcLogErrorEventArgs)
        'ErrorCount = ErrorCount + 1
'        ReportProgress("XbmcJsonRpc - ERROR : " & e.Message & " Exception : " & e.Exception.Message,LastArgs)
        If IsNothing(evt) Then
            ReportProgress("XbmcJsonRpc - Abort received",LastArgs)
        Else
            ReportProgress("XbmcJsonRpc - Abort received : [" + evt.Message + "]",LastArgs)
        End If
        
        Q.Write(e.JSON_Abort,PriorityQueue.Priorities.critical)
    End Sub

    Sub XBMC_Video_Updated(sender As Object, ea As EventArgs)
        Q.Write(E.XBMC_Video_Updated,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_Video_Removed(sender As Object, ea As EventArgs)

#If GenPseudoErrors Then
        ScanRemoved_Count += 1
        If ScanRemoved_Count mod ScanRemoved_MissRate = 0 Then
            log.Debug("Faking missed Video_Removed event")
            Return
        End If
#End If

        Q.Write(E.XBMC_Video_Removed,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_Video_ScanFinished(sender As Object, ea As EventArgs)

#If GenPseudoErrors Then
        ScanFinished_Count += 1
        If ScanFinished_Count mod ScanFinished_MissRate = 0 Then
            log.Debug("Faking missed ScanFinished event")
            Return
        End If
#End If

        Q.Write(E.XBMC_Video_ScanFinished,PriorityQueue.Priorities.high)
    End Sub

    Sub XBMC_System_Quit(sender As Object, ea As EventArgs)

        If Preferences.XBMC_Link Then
            Q.Write(E.XBMC_System_Quit,PriorityQueue.Priorities.high)
        End If
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
        t.Stop
        t.Interval = Interval
        t.AutoReset = False
    End Sub

    'Sub Restart_Timer(t As Timers.Timer)
    '    t.Stop()
    '    t.Start()
    'End Sub


    'Sub AutoMapMovieFolders(sender As Object, args As TransitionEventArgs(Of S, E, EventArgs))

    '    ReportProgress("Mapping movie folders : ",args)

    '    MovieFolderMappings.Clear

    '    For Each folder In Preferences.movieFolders
    '        Try
    '            Dim wotEver = folder

    '            Dim moviePaths = (From
    '                            m In Parent.oMovies.MoviesWithUniqueMovieTitles
    '                        Where
    '                            m.fullpathandfilename.ToUpper.Contains(wotEver.ToUpper)
    '                        Join
    '                            x In XbmcJson.MoviesWithUniqueMovieTitles
    '                        On
    '                            x.title Equals m.title And Path.GetFileName(x.file).ToUpper Equals Path.GetFileName(m.MoviePathAndFileName).ToUpper
    '                        Select
    '                            m.MoviePathAndFileName, x.file).First


    '            Dim mc = moviePaths.MoviePathAndFileName.ToUpper
    '            Dim xb = moviePaths.file.ToUpper

    '            'xb = xb.Replace(folder.ToUpper,"X:")       'Test to simulate a drive mapping


    '            If mc = xb Then
    '                MovieFolderMappings_Add(folder, folder)
    '            Else
    '                While mc.Length > folder.Length And mc.Chars(mc.Length - 1) = xb.Chars(xb.Length - 1)

    '                    mc = mc.Remove(mc.Length - 1)
    '                    xb = xb.Remove(xb.Length - 1)

    '                End While

    '                MovieFolderMappings_Add(folder, xb)
    '            End If
    '        Catch ex As Exception
    '           AppendLog(Warning_Prefix + "Check XBMC has a movie mapping equivilant to MC's : [" + folder + "] folder")
    '        End Try
    '    Next
    'End Sub

    'Sub MovieFolderMappings_Add( dirMc As String, dirXb As String )

    '    log.Debug("Mapping MC movie folder : [" + dirMc + "] to XBMC movie folder : [" + dirXb + "]")
    '    MovieFolderMappings.Add(dirMc,dirXb)
    'End Sub





        'public      int     movieid       ;
        'public      string  title         ; 
        'public List<string> genre         ; 
        'public      int     year          ; 
        'public      double  rating        ; 
        'public List<string> director      ;
        'public      string  trailer       ; 
        'public      string  tagline       ; 
        'public      string  plot          ; 
        'public      string  plotoutline   ; 
        'public      string  originalTitle ;
        'public      string  lastPlayed    ; 
        'public      int     playCount     ; 
        'public List<string> writer        ; 
        'public List<string> studio        ; 
        'public      string  mpaa          ;
        'public List<string> country       ; 
        'public      string  imdbnumber    ; 
        'public      int     runtime       ; 
        'public      string  set           ; 
        'public      int     top250        ; 
        'public      int     votes         ; 
        'public      string  file          ; 
        'public      string  sorttitle     ; 
        'public List<string> tag           ; 
        'public      string  dateadded     ; 
        'public List<Cast  > cast          ; 
        'public List<string> showlink      ; 
        'public      string  streamdetails ; 
        'public      string  fanart        ; 
        'public      string  thumbnail     ; 
        'public      string  resume        ; 
        'public      int     setid         ;     
        'public      Art     art           ;

 
    Private _Different As List(Of ComboList) = New List(Of ComboList)
    Private _XBMC_Only As List(Of String)    = New List(Of String)

    'Sub Handle_MaxXbmcMoviesChanged_Old

    '    XbmcMaxMoviesDirty = False

    '    _Different.Clear
    '    _XBMC_Only.Clear


    '    Dim result        As Boolean = False
    '    Dim count         As Integer = 0
    '    Dim mediaFileName As String

    '    For Each xbMovie In MaxXbmcMovies

    '        mediaFileName = MovieFolderMappings.GetMC_MoviePath(xbMovie.file).ToUpper

    '        Dim movieCache As ComboList = (From m In Parent.oMovies.MovieCache Where m.MoviePathAndFileName.ToUpper=mediaFileName).FirstOrDefault

    '        If IsNothing(movieCache) And Not _XBMC_Only.Contains(mediaFileName) Then
    '            _XBMC_Only.Add(mediaFileName)

    '            Dim cl As New ComboList
    '            Dim xb As New XbmcProps
    '            cl.XbmcProps = xb

    '            'For Each item In Preferences.XBMC_MC_CompareFields.Get_Xbmc_Fields.ToList

    '            'Next
 
    '            Parent.oMovies.MovieCache.Add(cl)
    '        Else
    '            If IsDifferent(xbMovie,movieCache) Then

    '                If Not _Different.Contains(movieCache) Then
    '                    _Different.Add(movieCache)
    '                End If
    '            End If
    '        End If
    '    Next

    '    ReportProgress("Number of XBMC only movies : [" + _XBMC_Only.count.ToString + "]",LastArgs)
    '    ReportProgress("Number of XBMC vs MC movies that have different meta data : [" + _Different.count.ToString + "]",LastArgs)
    'End Sub


    Function IsDifferent(xbMovie As MaxXbmcMovie, mcMovie As ComboList) As Boolean

        

    End Function

    Function IsDifferentOld(xbMovie As MaxXbmcMovie, mcMovie As ComboList) As Boolean

        Dim oMovie          As Movie

        Try

            oMovie  = New Movie(Parent.oMovies,mcMovie.fullpathandfilename)

            oMovie.LoadNFO


            'public List<string> genre         ; //  genre

            'public List<string> director      ; //  Not in Combolist

            'public      string  lastPlayed    ; //  NA

            'public List<string> writer        ; //  Not in Combolist
            'public List<string> studio        ; //  Not in Combolist
            'public List<string> country       ; //  Not in Combolist

            'public List<string> tag           ; //  tag

            'public      string  dateadded     ; //  createdate?
            'public List<Cast  > cast          ; //  Not in Combolist
            'public List<string> showlink      ; //  NA
            'public      string  streamdetails ; //  [Resolution]
            'public      string  fanart        ; //  Not in Combolist
            'public      string  thumbnail     ; //  Not in Combolist
            'public      string  resume        ; //  NA
            'public      int     setid         ; //  NA
            'public      Art     art           ; //  Not in Combolist

            Dim fmb As str_BasicMovieNFO = oMovie.ScrapedMovie.fullmoviebody

            If xbMovie.title                                          <> fmb.title                         Then Return True
            If xbMovie.year                                           <> fmb.year                          Then Return True
            If xbMovie.rating.ToString                                <> fmb.rating                        Then Return True
            If xbMovie.tagline                                        <> fmb.tagline                       Then Return True
            If xbMovie.plot                                           <> fmb.plot                          Then Return True
            If xbMovie.plotoutline                                    <> fmb.outline                       Then Return True
            If xbMovie.originalTitle                                  <> fmb.originalTitle                 Then Return True
            If xbMovie.playCount                                      <> fmb.playCount                     Then Return True
            If xbMovie.mpaa                                           <> fmb.mpaa                          Then Return True
            If xbMovie.imdbnumber                                     <> fmb.imdbid                        Then Return True
            If (xbMovie.runtime\60).ToString                          <> fmb.runtime .Replace(" min"  ,"") Then Return True
            If xbMovie.set                                            <> fmb.movieset.Replace("-None-","") Then Return True
            If xbMovie.top250.ToString                                <> fmb.top250                        Then Return True
            If xbMovie.votes.ToString                                 <> fmb.votes.Replace(",","")         Then Return True
            If xbMovie.sorttitle                                      <> fmb.sortorder                     Then Return True
            If MovieFolderMappings.GetMC_MoviePath(xbMovie.trailer)   <> oMovie.ActualTrailerPath.ToUpper  Then Return True


            'If xbMovie. <> fmb. Then Return True
            'If xbMovie. <> oMovie. Then Return True



        Catch ex As Exception
            LogError("IsDifferent", ex.Message,LastArgs)
        End Try

'Parent.oMovies.MovieCache

        Return False
    End Function



    Sub Handle_MaxXbmcMoviesChanged

        XbmcMaxMoviesDirty = False
        _xbmcMovies = MaxXbmcMovies.ToDictionary(Function(x) MovieFolderMappings.GetMC_MoviePath(x.file).ToUpper)
                                                                        
        _mcMovies   = Parent.oMovies.MovieCache.ToDictionary(Function(x) x.MoviePathAndFileName.ToUpper)

        XbmcMcMovies = (From xb In _xbmcMovies
                         From mc In _mcMovies
                         Where mc.Key=xb.key   ).ToDictionary(Function(x) x.mc.Key, Function(x) x.xb.Value)

        XbmcOnlyMovies = (From xb In _xbmcMovies
                        Where Not _xbmcMcMovies.ContainsKey(xb.key)
                        Select xb.Value
                        ).ToList

        'ReportProgress("Number of XBMC only movies : [" + _xbmcOnlyMovies.count.ToString + "]",LastArgs)
    End Sub


    Sub Handle_XbmcMcMoviesChanged
        ReportProgress("Passing back XBMC-MC movies", LastArgs)

        Dim msg = New XBMC_MC_Movies_EventArgs(XbmcMcMovies)

        ReportProgress(E.MC_XbmcMcMovies,msg)                
    End Sub

    Sub Handle_XbmcOnlyMoviesChanged
        ReportProgress("Passing back XBMC only movies", LastArgs)

        Dim msg = New XBMC_Only_Movies_EventArgs(XbmcOnlyMovies)

        ReportProgress(E.MC_XbmcOnlyMovies,msg)      
    End Sub


End Class


