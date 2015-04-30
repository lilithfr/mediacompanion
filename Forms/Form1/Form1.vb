﻿Option Explicit On

Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports Media_Companion.Preferences
Imports System.Xml
Imports System.Reflection
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Linq
Imports XBMC.JsonRpc

#Const SilentErrorScream = False
#Const Refocus = False


Public Class Form1

    Const HOME_PAGE            = "http://mediacompanion.codeplex.com"
    Const NFO_INDEX As Integer = 6
    Public Const XBMC_Controller_full_log_file  As String = "XBMC-Controller-full-log-file.txt" 
    Public Const XBMC_Controller_brief_log_file As String = "XBMC-Controller-brief-log-file.txt" 
    Public Const MCToolsCommands As Integer = 5          ' increment when adding MC functions to ToolsToolStripMenuItem

    Public Dim WithEvents  BckWrkScnMovies       As BackgroundWorker = New BackgroundWorker
    Public Dim WithEvents  BckWrkCheckNewVersion As BackgroundWorker = New BackgroundWorker
    Public Dim WithEvents  BckWrkXbmcController  As BackgroundWorker = New BackgroundWorker
    Shared Public          XbmcControllerQ       As PriorityQueue    = New PriorityQueue
    Shared Public          XbmcControllerBufferQ As PriorityQueue    = New PriorityQueue
    Shared Public Property MC_Only_Movies        As List(Of ComboList)
    Public Shared Property MaxXbmcMovies As List(Of MaxXbmcMovie)
    Shared Public MyCulture As New System.Globalization.CultureInfo("en-US")

    Public Property        XBMC_Controller_LogLastShownDt  As Date = Now
    Private                XBMC_Link_ErrorLog_Timer As Timers.Timer = New Timers.Timer()
    Private                XBMC_Link_Idle_Timer     As Timers.Timer = New Timers.Timer()
    Private                XBMC_Link_Check_Timer    As Timers.Timer = New Timers.Timer()

    Declare Function AttachConsole Lib "kernel32.dll" (ByVal dwProcessId As Int32) As Boolean

    Shared ReadOnly Property Link_TotalQCount
        Get
            Return XbmcControllerQ.Count + XbmcControllerBufferQ.Count
        End Get
    End Property

    Shared ReadOnly Property MC_Only_Movies_Nfos As List(Of String)
        Get
            If IsNothing(MC_Only_Movies) Then Return New List(Of String)

            Return (From M In MC_Only_Movies Select M.fullpathandfilename).ToList
        End Get
    End Property

    ReadOnly Shared Property NumOfScreens As Integer
        Get
            Return System.Windows.Forms.Screen.AllScreens.GetUpperBound(0)
        End Get
    End Property

    ReadOnly Shared Property CurrentScreen As Integer
        Get
            Try
                Dim display As String = System.Windows.Forms.Screen.FromControl(Form1).DeviceName
                Dim m As String = Regex.Match(display, "DISPLAY[0-9]").Value
                Return ToInt(m.Substring(m.Length - 1)) - 1
            Catch
                Return 0
            End Try 
        End Get
    End Property

    Property frmXBMC_Progress As frmXBMC_Progress = New frmXBMC_Progress


    #Region "Movie scraping related objects"
            Public WithEvents oMovies As New Movies

            Public filteredList As New List(Of ComboList)
            Public rescrapeList As New RescrapeList
            Public workingMovieDetails As FullMovieDetails
            Public _rescrapeList As New RescrapeSpecificParams
            Public ChangeMovieId = ""
            Public droppedItems As New List(Of String)
            Public ControlsToDisableDuringMovieScrape As IEnumerable(Of Control)

            Public Shared blnAbortFileDownload As Boolean
            Public Shared ReadOnly countLock = New Object
            Public ScraperErrorDetected As Boolean

    #End Region 'Movie scraping objects


    Enum ProgramState
        ResettingFilters
	    UpdatingFilteredList
        ResizingSplitterPanel
        MovieControlsDisabled
	    Other
    End Enum

    Shared Public ProgState As ProgramState=ProgramState.Other
    Public StateBefore As ProgramState=ProgramState.Other

    Public DataDirty As Boolean

    Public CopyOfPreferencesIgnoreArticle As Boolean

    Public _yield               As Boolean
    Public LastMovieDisplayed   As String=""
    Public MainFormLoadedStatus As Boolean = False
    Public movieRefreshNeeded As Boolean = True
    Public tvRefreshNeeded As Boolean = True
    Public messbox As New frmMessageBox("blank", "", "")
    Public startup As Boolean = True
    Public tv_RegexScraper As New List(Of String)
    Public tv_RegexRename As New List(Of String)
    Public MissingNfoPath As String 
    Public SeriesXmlPath As String
    Public dList As New List(Of String)
    Public scraperFunction2 As New ScraperFunctions
    Public globalThreadStop As Boolean = False
    Public globalThreadCounter As Integer = 0
    Public nfoFunction As New WorkingWithNfoFiles
    Public mediaInfoExp As New MediaInfoExport
    Shared Public langarray(300, 3) As String
    Public screen As Screen
    Public Shared genrelist As New List(Of String)

    'Replace the list of structure by a list of objects

    Public Data_GridViewMovie As Data_GridViewMovie
    Public DataGridViewBindingSource As New BindingSource

    Public homemovielist As New List(Of str_BasicHomeMovie)
    Public WorkingHomeMovie As New HomeMovieDetails
    Public workingMovie As New ComboList
    Public tvBatchList As New str_TvShowBatchWizard(SetDefaults)
    Public generalprefschanged As Boolean = False
    Public movieprefschanged As Boolean = False
    Public tvprefschanged As Boolean = False
    Public tvfolderschanged As Boolean = False
    Public cleanfilenameprefchanged As Boolean = False
    Public videosourceprefchanged As Boolean = False
    Public scraperLog As String = ""
    Public NewTagList As New List(Of String)
    Public MovieSearchEngine As String = "imdb"
    Dim mov_TableColumnName As String = ""
    Dim MovieSetMissingID As Boolean = False

    Public cropMode As String = "movieposter"

    Public noFanart As Boolean

    Dim WithEvents bigPictureBox As PictureBox
    Dim WithEvents fanartBoxes As PictureBox
    Dim WithEvents fanartCheckBoxes As RadioButton
    Dim WithEvents posterPicBoxes As PictureBox
    Dim WithEvents posterCheckBoxes As RadioButton
    Dim WithEvents posterLabels As Label
    Dim WithEvents resLabel As Label
    Dim WithEvents tvFanartBoxes As PictureBox
    Dim WithEvents tvFanartCheckBoxes As RadioButton
    Dim WithEvents resolutionLabels As Label
    Dim newTvFolders As New List(Of String)
    Dim tvprogresstxt As String = ""
    Dim maximised As Boolean = False
    Public imdbCounter As Integer = 0
    Dim tootip5 As New ToolTip
    Dim prefsload As Boolean = False
    Dim pictureList As New List(Of PictureBox)
    Dim screenshotTab As TabPage
    Dim filterOverride As Boolean = False
    Dim mouseOver As Boolean = False
    Dim bigPanel As Panel
    Dim realMoviePaths As New List(Of String)
    Dim realTvPaths As New List(Of String)
    Dim newTvShows As New List(Of String)
    Dim profileStruct As New Profiles
    Dim frmSplash As New frmSplashscreen
    Dim frmSplash2 As New frmProgressScreen
    Public Shared multimonitor As Boolean = False
    Dim progressmode As Boolean
    Dim overItem As String
    Dim scrapeAndQuit  As Boolean = False
    Dim refreshAndQuit As Boolean = False
    Dim sandq As Integer = 0
    Dim mouseDelta As Integer = 0
    Dim resLabels As Label
    Dim fanartUrls(1000, 1) As String
    Dim fanartArray As New List(Of str_ListOfPosters)
    Dim cropString As String
    Dim thumbedItsMade As Boolean = False
    Dim posterArray As New List(Of str_ListOfPosters)
    Dim pageCount As Integer = 0
    Dim currentPage As Integer = 0
    Dim tab1 As Integer = 0
    Dim actorflag As Boolean = False
    Dim listOfTvFanarts As New List(Of str_FanartList)
    Dim lockedList As Boolean = False
    Dim tempTVDBiD As String = String.Empty
    Dim novaThread As Thread
    Dim newMovieFoundTitle As String = String.Empty
    Dim newMovieFoundFilename As String = String.Empty
    Dim tableSets As New List(Of str_TableItems)
    Dim relativeFolderList As New List(Of str_RelativeFileList)

    Dim templanguage As String

    Dim combostart As String = ""

    Dim currentposterid As String = ""
    Dim workingposterpath As String

    Dim WithEvents tvposterpicboxes As PictureBox
    Dim WithEvents tvpostercheckboxes As RadioButton
    Dim WithEvents tvposterlabels As Label
    Dim WithEvents tvreslabel As Label
    Dim tvposterpage As Integer = 1
    Dim walllocked As Boolean = False
    Dim maxcount As Integer = 0
    Dim moviecount_bak As Integer = 0
    Dim displayRuntimeScraper As Boolean = True
    Dim tv_IMDbID_detected As Boolean = False
    Dim tv_IMDbID_warned As Boolean = False
    Dim tv_IMDbID_detectedMsg As String = String.Format("Media Companion has detected one or more TV Shows has an incorrect ID.{0}", vbCrLf) & _
                            String.Format("To rectify, please select the following:{0}", vbCrLf) & _
                            String.Format("  1. TV Preferences -> Fix NFO id during cache refresh{0}", vbCrLf) & _
                            String.Format("  2. TV Shows -> Refresh Shows{0}", vbCrLf) & _
                            String.Format("(This will only be reported once per session)", vbCrLf)
    Dim TVSearchALL As Boolean = False
    Private ClickedControl As String


    Private WithEvents FileToBeDownloaded As WebFileDownloader
    Private tvCurrentTabIndex As Integer = 0
    Private currentTabIndex As Integer = 0
    Private homeTabIndex As Integer = 0

    Public totalfilesize As Long = 0
    Public listoffilestomove As New List(Of String)
    Dim showstoscrapelist As New List(Of String)
    Dim processnow As Boolean = True
    Dim currenttitle As String
    Public singleshow As Boolean = False
    Public showslist As Object
    Public homemovietabindex As Integer = 0
    

    Dim MoviesFiltersResizeCalled As Boolean = False

    'TODO: (Form1_Load) Need to refactor
#Region "Form1 Events"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBoxAssignedMoviePoster.AllowDrop = True
        AddHandler Preferences.PropertyChanged_MkvMergeGuiPath, AddressOf MkvMergeGuiPath_ChangeHandler
        Try
            'AddRegKey()    'disabled as user's were receiving UAC messages.
            Preferences.movie_filters.FilterPanel = SplitContainer5.Panel2

            Label73.Text = ""

            BckWrkScnMovies.WorkerReportsProgress = True
            BckWrkScnMovies.WorkerSupportsCancellation = True

            oMovies.Bw = BckWrkScnMovies

            For I = 0 To 20
                Common.Tasks.Add(New Tasks.BlankTask())
            Next

            Preferences.applicationPath = Application.StartupPath
            Utilities.applicationPath = Application.StartupPath
            MissingNfoPath = IO.Path.Combine(Utilities.applicationPath, "missing\")
            SeriesXmlPath = IO.Path.Combine(Utilities.applicationPath, "SeriesXml\")
            If Not Utilities.GetFrameworkVersions().IndexOf("4.0") Then
                Dim RequiredNetURL As String = "http://www.microsoft.com/download/en/details.aspx?id=17718"
                If MsgBox("The Client version is available through Windows Updates." & vbCrLf & _
                          "The Full version, while not required, is available from:" & vbCrLf & _
                          RequiredNetURL & vbCrLf & vbCrLf & _
                          "Do you wish to download the Full version?", _
                          MsgBoxStyle.YesNo, "MC Requires .Net 4.0.") = MsgBoxResult.Yes Then
                    'Process.Start(RequiredNetURL)
                    OpenUrl(RequiredNetURL)
                    End
                End If
            End If


            ForegroundWorkTimer.Interval = 500
#If Not Debug Then
           AddHandler ForegroundWorkTimer.Tick, AddressOf ForegroundWorkPumper
#End If

            Dim asm As Assembly = Assembly.GetExecutingAssembly
            Dim InternalResourceNames() As String = asm.GetManifestResourceNames

            For Each Temp In InternalResourceNames
                Dim Temp1 As ManifestResourceInfo = asm.GetManifestResourceInfo(Temp)
            Next

            TvTreeview.Sort()

            For Each arg As String In Environment.GetCommandLineArgs().Skip(1)
                Select Case arg.ToLower
                    Case "sq"
                        scrapeAndQuit = True
                        sandq = 3
                    Case "st"
                        scrapeAndQuit = True
                        sandq = 1
                    Case "sm"
                        scrapeAndQuit = True
                        sandq = 2
                    Case "r"
                        refreshAndQuit = True
                    Case "?"
                        AttachConsole(-1)
                        Console.WriteLine("")
                        Console.WriteLine("")
                        Console.WriteLine("Commandline options")
                        Console.WriteLine("-------------------")
                        Console.WriteLine("sq - Search for & scrape new movies & tv shows")
                        Console.WriteLine("st - Search for & scrape newtv shows")
                        Console.WriteLine("sm - Search for & scrape new movies")
                        Console.WriteLine("r  - Refresh movie & tv caches")
                        Console.WriteLine("?  - Show this page")
                        Console.WriteLine("")
                        Environment.Exit(1)
                        'Me.Close()
                    Case Else
                        AttachConsole(-1)
                        Console.WriteLine("")
                        Console.WriteLine("")
                        Console.WriteLine("Unrecognised commandline option : [" & arg & "]. Type ? for help")
                        Console.WriteLine("")
                        Environment.Exit(1)
                        'Me.Close()
                End Select
            Next

            If scrapeAndQuit Or refreshAndQuit Then
                Me.WindowState = FormWindowState.Minimized
            Else
                Dim scrn As Integer = splashscreenread()
                If multimonitor Then
                    frmSplash.Bounds = screen.AllScreens(scrn).Bounds
                    frmSplash.StartPosition = FormStartPosition.Manual
                    Dim x As Integer = screen.AllScreens(scrn).Bounds.X
                    frmSplash.Location = New Point(x + 250, 250)
                    frmSplash.TopMost = True
                End If
                frmSplash.Show()
                frmSplash.Label3.Text = "Status :- Initialising Program"
                frmSplash.Label3.Refresh()
            End If
            Me.Visible = False

            Me.Refresh()
            Application.DoEvents()
            Dim tempstring As String
            tempstring = applicationPath & "\enablemultiple.set"
            If Not File.Exists(tempstring) Then
                Dim tej As Integer = 0
                Dim processes() As Process
                Dim instance As Process
                Dim process As New Process()
                processes = process.GetProcesses
                For Each instance In processes
                    If instance.ProcessName = "Media Companion" Then                            'If instance.ProcessName.IndexOf("Media Companion - V") <> -1 Then          This should limit the match to only Median Companion running not Visual Studio 2010
                        tej = tej + 1
                        If tej >= 2 Then
                            MsgBox("XBMC Media Companion is already running")
                            End                         'Close MC since another version of the program is running.
                        End If
                    End If
                Next
            End If
            CheckForIllegalCrossThreadCalls = False

            Preferences.maximised = False
            Preferences.SetUpPreferences()  'Set defaults to all userpreferences. We then load the preferences from config.xml this way any missing ones have a default already set
            generalprefschanged = False

            GenreMasterLoad()

            tempstring = applicationPath & "\Settings\" 'read in the config.xml to set the stored preferences (if it exists)
            Dim hg As New IO.DirectoryInfo(tempstring)
            If hg.Exists Then
                Preferences.configpath = tempstring & "config.xml"
                If Not IO.File.Exists(Preferences.configpath) Then
                    Preferences.ConfigSave()
                End If
            Else
                IO.Directory.CreateDirectory(tempstring)
                workingProfile.Config = applicationPath & "\Settings\config.xml"
                Preferences.ConfigSave()
            End If

            If Not IO.File.Exists(applicationPath & "\settings\profile.xml") Then
                profileStruct.WorkingProfileName = "Default"
                profileStruct.DefaultProfile = "Default"
                profileStruct.StartupProfile = "Default"
                Dim currentprofile As New ListOfProfiles
                tempstring = applicationPath & "\Settings\"
                currentprofile.ActorCache = tempstring & "actorcache.xml"
                currentprofile.DirectorCache = tempstring & "directorcache.xml"
                currentprofile.Config = tempstring & "config.xml"
                currentprofile.RegExList = tempstring & "regex.xml"
                currentprofile.TvCache = tempstring & "tvcache.xml"
                currentprofile.MusicVideoCache = tempstring & "musicvideocache.xml"
                currentprofile.Filters = tempstring & "filters.txt"
                currentprofile.Genres = tempstring & "genres.txt"
                currentprofile.MovieCache = tempstring & "moviecache.xml"
                currentprofile.MovieSetCache = tempstring & "moviesetcache.xml"
                currentprofile.ProfileName = "Default"
                profileStruct.ProfileList.Add(currentprofile)
                profileStruct.WorkingProfileName = "Default"
                Call util_ProfileSave()
            End If
            
            'hide debug xml view tabs - unhiden (i.e. added) via debug tab
            TabLevel1.TabPages.Remove(Me.TabConfigXML)
            TabLevel1.TabPages.Remove(Me.TabMovieCacheXML)
            TabLevel1.TabPages.Remove(Me.TabTVCacheXML)
            TabLevel1.TabPages.Remove(Me.TabProfile)
            TabLevel1.TabPages.Remove(Me.TabActorCache)
            TabLevel1.TabPages.Remove(Me.TabRegex)
            TabLevel1.TabPages.Remove(Me.TabCustTv)     'Hide customtv tab while Work-In-Progress
            TabLevel1.TabPages.Remove(Me.TabMV)         'Hide Music Video Tab while Work-In-Progress
            PreferencesToolStripMenuItem.Visible = False
            
            Call util_ProfilesLoad()
            For Each prof In profileStruct.ProfileList
                If prof.ProfileName = profileStruct.StartupProfile Then
                    workingProfile.ActorCache = prof.ActorCache
                    workingProfile.DirectorCache = prof.DirectorCache
                    workingProfile.Config = prof.Config
                    workingProfile.MovieCache = prof.MovieCache
                    workingProfile.ProfileName = prof.ProfileName
                    workingProfile.RegExList = prof.RegExList
                    workingProfile.Filters = prof.Filters
                    workingProfile.Genres = prof.Genres 
                    workingProfile.TvCache = prof.TvCache
                    workingProfile.ProfileName = prof.ProfileName
                    workingProfile.MusicVideoCache = prof.MusicVideoCache
                    workingProfile.MovieSetCache = prof.MovieSetCache 
                    For Each item In ProfilesToolStripMenuItem.DropDownItems
                        If item.text = workingProfile.ProfileName Then
                            With item
                                item.checked = True
                            End With
                        Else
                            item.checked = False
                        End If
                    Next
                End If
            Next

            'add musicvideocache to profile if it doesnt exist
            Dim counter As Int16 = 0
            tempstring = applicationPath & "\Settings\"
            For Each prof In profileStruct.ProfileList
                If counter = 0 Then
                    If prof.MusicVideoCache = "" Then
                        prof.MusicVideoCache = "\Settings\musicvideocache.xml"
                        If prof.ProfileName = workingProfile.ProfileName Then
                            workingProfile.MusicVideoCache = tempstring & "musicvideocache.xml"
                        End If
                    End If
                    If prof.MovieSetCache = "" Then
                        prof.MovieSetCache = "\Settings\moviesetcache.xml"
                        If prof.ProfileName = workingProfile.ProfileName Then
                            workingProfile.MovieSetCache = tempstring & "moviesetcache.xml"
                        End If
                    End If
                Else
                    If prof.MusicVideoCache = "" Then
                        prof.MusicVideoCache = "\Settings\musicvideocache" & counter.ToString & ".xml"
                    End If
                    If prof.ProfileName = workingProfile.ProfileName Then
                        workingProfile.MusicVideoCache = tempstring & "musicvideocache" & counter.ToString & ".xml"
                    End If
                    If prof.MovieSetCache = "" Then
                        prof.MovieSetCache = "\Settings\moviesetcache" & counter.ToString & ".xml"
                    End If
                    If prof.ProfileName = workingProfile.ProfileName Then
                        workingProfile.MovieSetCache = tempstring & "moviesetcache" & counter.ToString & ".xml"
                    End If
                End If
                counter += 1
            Next

            If workingProfile.HomeMovieCache = "" Then workingProfile.HomeMovieCache = tempstring & "homemoviecache.xml"
            'Update Main Form Window Title to show Currrent Version - displays current profile so has to be done after profile is loaded
            util_MainFormTitleUpdate()


            Dim g As New IO.DirectoryInfo(IO.Path.Combine(applicationPath, "settings\postercache\"))
            If Not g.Exists Then
                Try
                    Directory.CreateDirectory(IO.Path.Combine(applicationPath, "settings\postercache\"))
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                    End
                End Try
            End If

            CheckForIllegalCrossThreadCalls = False

            'Try
            '    SplitContainer9.SplitterDistance = SplitContainer9.Height - 61      'Tv Folder Horizontal Split as this keeps moving in designer.
            'Catch
            'End Try

            Try
                If IO.File.Exists(IO.Path.Combine(applicationPath, "\error.log")) Then IO.File.Delete(IO.Path.Combine(applicationPath, "\error.log"))
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try

            tempstring = applicationDatapath & "error.log"
            If IO.File.Exists(tempstring) = True Then
                IO.File.Delete(tempstring)
            End If

            Call util_RegexLoad()

            Call util_PrefsLoad()

            'These lines fixed the associated panel so that they don't automove when the Form1 is resized
            SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1 'Left Panel on Movie tab - Movie Listing 
            SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2 'Bottom Left Panel on Movie Tab - Filters
            SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1 'Left Panel on TV Tab

            Movies.SpinUpDrives()

            If Not (scrapeAndQuit Or refreshAndQuit) Then
                Me.Visible = True
                Dim intX As Integer
                Dim intY As Integer
                If Preferences.MultiMonitoEnabled Then
                    Dim scrn As Integer = If(NumOfScreens > 0, Preferences.preferredscreen, 0)
                    intX = screen.AllScreens(scrn).Bounds.X + screen.AllScreens(scrn).Bounds.Width
                    intY = screen.AllScreens(scrn).Bounds.Height
                Else
                    intX = Screen.PrimaryScreen.Bounds.Width
                    intY = Screen.PrimaryScreen.Bounds.Height
                End If
                SplitContainer1.IsSplitterFixed = True
                SplitContainer2.IsSplitterFixed = True
                SplitContainer3.IsSplitterFixed = True
                SplitContainer4.IsSplitterFixed = True
                SplitContainer5.IsSplitterFixed = True

                If Preferences.locx < 0 Then Preferences.locx = 0
                If Preferences.locy < 0 Then Preferences.locy = 0
                If Preferences.formheight > intY Then Preferences.formheight = intY
                If Preferences.formwidth > intX Then Preferences.formwidth = intX
                If Preferences.locx >= intX Then Preferences.locx = intX - Preferences.formwidth
                If Preferences.locy >= intY Then Preferences.locy = intY - Preferences.formheight
                If Preferences.formheight <> 0 And Preferences.formwidth <> 0 Then
                    Me.Width = Preferences.formwidth
                    Me.Height = Preferences.formheight
                    Me.Location = New Point(Preferences.locx, Preferences.locy)
                End If
                If Preferences.maximised Then
                    Me.WindowState = FormWindowState.Maximized
                End If

                Dim dpi As Graphics = Me.CreateGraphics

                'MessageBox.Show(String.Format("X={0}, Y={1}", dpi.DpiX, dpi.DpiY),
                '"DPI Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)

                DebugSytemDPITextBox.Text = dpi.DpiX

                Me.Refresh()
                Application.DoEvents()

                Me.Refresh()
                Application.DoEvents()

                Application.DoEvents()

                screenshotTab = TabControl3.TabPages(1)

                TabControl3.TabPages.RemoveAt(1)

                If Preferences.splt5 = 0 Then
                    Dim tempint As Integer = SplitContainer1.Height
                    tempint = tempint / 4
                    tempint = tempint * 3
                    If tempint > 275 Then
                        Preferences.splt5 = tempint
                    Else
                        Preferences.splt5 = 275
                    End If
                End If

                SplitContainer1.SplitterDistance = Preferences.splt1
                SplitContainer2.SplitterDistance = Preferences.splt2
                SplitContainer5.SplitterDistance = Preferences.splt5
                SplitContainer3.SplitterDistance = Preferences.splt3
                SplitContainer4.SplitterDistance = Preferences.splt4
                TabLevel1.SelectedIndex = Preferences.startuptab

                If Preferences.startuptab = 0 Then
                    If Not MoviesFiltersResizeCalled Then
                        MoviesFiltersResizeCalled = True
                        Preferences.movie_filters.SetMovieFiltersVisibility
                        UpdateMovieFiltersPanel
                    End If
                End If


                SplitContainer1.IsSplitterFixed = False
                SplitContainer2.IsSplitterFixed = False
                SplitContainer3.IsSplitterFixed = False
                SplitContainer4.IsSplitterFixed = False
                SplitContainer5.IsSplitterFixed = False
            End If

            If scrapeAndQuit Or refreshAndQuit Then
                Do_ScrapeAndQuit()
                Me.Close()
            Else
                Try
                    If cbMovieDisplay_MovieSet.Items.Count <> Preferences.moviesets.Count Then
                        cbMovieDisplay_MovieSet.Items.Clear()
                        For Each mset In Preferences.moviesets
                            cbMovieDisplay_MovieSet.Items.Add(mset)
                        Next
                    End If
                    If Not IsNothing(workingMovieDetails) AndAlso workingMovieDetails.fullmoviebody.movieset.MovieSetName <> "-None-" Then
                        For Each mset In Preferences.moviesets
                            cbMovieDisplay_MovieSet.Items.Add(mset)
                        Next
                        For te = 0 To cbMovieDisplay_MovieSet.Items.Count - 1
                            If cbMovieDisplay_MovieSet.Items(te) = workingMovieDetails.fullmoviebody.movieset Then
                                cbMovieDisplay_MovieSet.SelectedIndex = te
                                Exit For
                            End If
                        Next
                    End If

                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                mov_VideoSourcePopulate()
                ep_VideoSourcePopulate()
                Call util_FontSetup()
                Call langarrsetup()
                Dim mediaDropdown As New SortedList(Of String, String)
                mediaInfoExp.addTemplates(mediaDropdown)
                For Each item In mediaDropdown
                    If item.Value = MediaInfoExport.mediaType.Movie Then
                        ExportMovieListInfoToolStripMenuItem.DropDownItems.Add(item.Key)
                    ElseIf item.Value = MediaInfoExport.mediaType.TV Then
                        ExportTVShowInfoToolStripMenuItem.DropDownItems.Add(item.Key)
                    End If
                Next

                Call util_CommandListLoad()
                startup = False

                frmSplash.Label3.Text = "Status :- Cleaning Cache folder."
                frmSplash.Label3.Refresh()

                CleanCacheFolder()  'Limit cachefolder to max 100 files.  Cleaned on startup and shutdown.

                frmSplash.Close()

                'the following code aligns the 2 groupboxes ontop of each other which cannot be done in the GUI
                GroupBox_TMDB_Scraper_Preferences.Location = GroupBox_MovieIMDBMirror.Location

                'ToolStrip1.Enabled = True

                mov_SplitContainerAutoPosition()
                tv_ShowSelectedCurrently()
                tv_SplitContainerAutoPosition()
            End If

            'Parameters to display the movie grid at startup
            Select Case Preferences.moviedefaultlist
                Case 0
                    rbTitleAndYear.Checked = True
                Case 1
                    rbFileName.Checked = True
                Case 2
                    rbFolder.Checked = True
            End Select

            Try
                cbSort.SelectedIndex = Preferences.moviesortorder
            Catch
                cbSort.SelectedIndex = 0
            End Try
            btnreverse.Checked = Preferences.movieinvertorder
            If btnreverse.Checked Then
                Mc.clsGridViewMovie.GridSort = "Desc"
            Else
                Mc.clsGridViewMovie.GridSort = "Asc"
            End If
            genretxt.ShortcutsEnabled = False

            Read_XBMC_TMDB_Scraper_Config()
            MainFormLoadedStatus = True
            UcFanartTv1.Form1MainFormLoadedStatus = True
            UcFanartTvTv1.Form1MainFormLoadedStatus = True
            ReloadMovieCacheToolStripMenuItem.Visible = False
            ToolStripSeparator9.Visible = False

            ResetFilters()

            UpdateFilteredList()

            If Not IsNothing(Preferences.MovFiltLastSize) Then ResizeBottomLHSPanel(Preferences.MovFiltLastSize, MovieFiltersPanelMaxHeight)

            Common.Tasks.StartTaskEngine()
            ForegroundWorkTimer.Start()

            If Preferences.CheckForNewVersion Then BckWrkCheckNewVersion.RunWorkerAsync(False)

            BckWrkXbmcController.WorkerReportsProgress = True
            ' BckWrkXbmcController.WorkerSupportsCancellation = true

            oMovies.Bw = BckWrkScnMovies

            ' frmXBMC_Progress.Bounds = screen.AllScreens(CurrentScreen).Bounds
            ' frmXBMC_Progress.StartPosition = FormStartPosition.Manual

            AddHandler XBMC_Link_ErrorLog_Timer.Elapsed, AddressOf XBMC_Controller_Log_TO_Timer_Elapsed
            Ini_Timer(XBMC_Link_ErrorLog_Timer, 3000)

            AddHandler XBMC_Link_Idle_Timer.Elapsed, AddressOf XBMC_Link_Idle_Timer_Elapsed
            Ini_Timer(XBMC_Link_Idle_Timer, 3000)

            AddHandler XBMC_Link_Check_Timer.Elapsed, AddressOf XBMC_Link_Check_Timer_Elapsed
            Ini_Timer(XBMC_Link_Check_Timer, 2000, True)
            'XBMC_Link_Check_Timer.Start


            AddHandler BckWrkXbmcController.ProgressChanged, AddressOf BckWrkXbmcController_ReportProgress
            AddHandler BckWrkXbmcController.DoWork, AddressOf BckWrkXbmcController_DoWork

            BckWrkXbmcController.RunWorkerAsync(Me)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
        'SendXbmcConnect
    End Sub

#If Refocus Then
    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            If messbox.Visible = True Then
                messbox.Activate()
                messbox.BringToFront()
                messbox.Focus()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
#End If

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Me.Dispose()
            Me.Finalize()
            CleanCacheFolder()  'Limit cachefolder to max 200 files.  Cleaned on startup and shutdown.
            If cbClearCache.Checked = True Then ClearCacheFolder() ' delete cache folder if option selected.
            If cbClearMissingFolder.Checked = True Then ClearMissingFolder() ' delete missing folder if option selected.
            End
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        BckWrkScnMovies_Cancel()
        While BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While

        Try
            oMovies.SaveCaches()

            If Tv_CacheSave() Then
                e.Cancel = True
                Exit Sub
            End If
            Call HomeMovieCacheSave()

            Call UcMusicVideo1.MusicVideoCacheSave()

            'if we say cancel to save nfo's & exit then we don't want to exit MC if e.cancel= true we abort the closing....

            'Todo: Code a better way to serialize the data

            'Me.LoadConfig()

            Preferences.splt1 = SplitContainer1.SplitterDistance
            Preferences.splt2 = SplitContainer2.SplitterDistance
            Preferences.splt3 = SplitContainer3.SplitterDistance
            Preferences.splt4 = SplitContainer4.SplitterDistance
            Preferences.splt5 = SplitContainer5.SplitterDistance
            Preferences.splt6 = _tv_SplitContainer.SplitterDistance
            Preferences.tvbannersplit = Math.Round(_tv_SplitContainer.SplitterDistance / _tv_SplitContainer.Height, 2)
            Preferences.MovFiltLastSize = SplitContainer5.Height - SplitContainer5.SplitterDistance
            Preferences.preferredscreen = CurrentScreen


            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
                Preferences.formwidth = Me.Width
                Preferences.formheight = Me.Height
                Preferences.locx = Me.Location.X
                Preferences.locy = Me.Location.Y
                Preferences.maximised = False
            End If

            If Me.WindowState = FormWindowState.Normal Then
                Preferences.formwidth = Me.Width
                Preferences.formheight = Me.Height
                Preferences.locx = Me.Location.X
                Preferences.locy = Me.Location.Y
                Preferences.maximised = False
            End If

            If Me.WindowState = FormWindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
                Preferences.maximised = True
            End If

            If DataGridView1.Columns.Count > 0 Then
                Preferences.tableview.Clear()
                For Each column In DataGridView1.Columns
                    Dim tempstring As String = String.Format("{0}|{1}|{2}|{3}", column.name, column.width, column.displayindex, column.visible)
                    Preferences.tableview.Add(tempstring)
                Next
            End If

            Preferences.startuptab = TabLevel1.SelectedIndex

            Preferences.ConfigSave()
            SplashscreenWrite()
            Call util_ProfileSave()
            Dim errpath As String = IO.Path.Combine(applicationPath, "tvrefresh.log")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Exception")
            Environment.Exit(1)
            'ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Form1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
        Try
            If messbox.Visible = True Then
                messbox.Activate()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Try
            If Me.WindowState = FormWindowState.Maximized Then
                mov_SplitContainerAutoPosition()
                tv_SplitContainerAutoPosition()
            End If
            If Not Me.WindowState = FormWindowState.Minimized Then
                tv_SplitContainerAutoPosition()
            End If
            If startup = False Then
                Preferences.locx = Me.Location.X
                Preferences.locy = Me.Location.Y
            End If
            If MainFormLoadedStatus = True Then
                doResizeRefresh()
            End If
        Catch ex As Exception
            Dim paramInfo As String = ""
            Try
                paramInfo = "PbMovieFanArt.Width:" & PbMovieFanArt.Width.ToString & " PbMovieFanArt.Height: " & PbMovieFanArt.Height.ToString & " Rating:" & ratingtxt.Text
            Catch ex2 As Exception
                ExceptionHandler.LogError(ex2)
            End Try
            ExceptionHandler.LogError(ex, paramInfo)
        End Try
    End Sub

    Private Sub Form1_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        Try
            If Preferences.formwidth <> Me.Width Or Preferences.formheight <> Me.Height Then
                Preferences.formwidth = Me.Width
                Preferences.formheight = Me.Height
                Dim maxcount2 As Integer = Convert.ToInt32((TabPage22.Width - 100) / 150)
                If maxcount2 <> maxcount Then
                    maxcount = maxcount2
                    Call mov_WallReset()
                End If

            End If
            mov_SplitContainerAutoPosition()
            tv_SplitContainerAutoPosition()

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Form1_BackgroundImageChanged(sender As Object, e As System.EventArgs) Handles Me.BackgroundImageChanged

    End Sub

    Private Sub Form1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then bckgrndcancel() 'BckWrkScnMovies_Cancel
        If e.KeyCode = Keys.F5 Then doRefresh()
        If e.KeyCode = Keys.F3 Then doSearchNew()
        If e.Control And e.KeyCode = Keys.C Then AbortFileDownload()
    End Sub

#End Region

    Sub Ini_Timer(t As Timers.Timer, Optional Interval As Integer = 1000, Optional Repeating As Boolean = False)
        t.Stop()
        t.Interval = Interval
        t.AutoReset = Repeating
    End Sub

    'Sub SendXbmcConnect
    '    If Preferences.XbmcLinkReady Then 
    '        XbmcControllerQ.Write(XbmcController.E.ConnectReq, PriorityQueue.Priorities.low)
    '    End If
    'End Sub

#Region "XBMC Link"
    Private Sub BckWrkXbmcController_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)

        Dim sm As New XbmcController(e.Argument, bw)

        sm.Go()
    End Sub

    Private Sub XBMC_Controller_Log_TO_Timer_Elapsed()
        If Not BckWrkScnMovies.IsBusy And XbmcControllerBufferQ.Count = 0 Then
            If DateDiff(DateInterval.Second, XBMC_Controller_LogLastShownDt, Now) > 30 Then

                Preferences.OpenFileInAppPath(Form1.XBMC_Controller_full_log_file)
                Preferences.OpenFileInAppPath(Form1.XBMC_Controller_brief_log_file)

                frmXBMC_Progress.Reset()
                Dim ce As New BaseEvent(XbmcController.E.ResetErrorCount, New BaseEventArgs())
                XbmcControllerQ.Write(ce)
            End If
            XBMC_Controller_LogLastShownDt = Now
            XBMC_Link_ErrorLog_Timer.Stop()
        End If
    End Sub

    Private Sub XBMC_Link_Idle_Timer_Elapsed()
        If Not BckWrkScnMovies.IsBusy And XbmcControllerBufferQ.Count = 0 Then
            frmXBMC_Progress.Visible = False
            XBMC_Link_ErrorLog_Timer.Stop()
        End If
    End Sub

    Sub Restart(tmr As Timers.Timer)
        tmr.Stop()
        tmr.Start()
    End Sub

    Private Sub BckWrkXbmcController_ReportProgress(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)

        Dim oProgress As XBMC_Controller_Progress = CType(e.UserState, XBMC_Controller_Progress)

        If XBMC_Link_ErrorLog_Timer.Enabled Then
            Restart(XBMC_Link_ErrorLog_Timer)
        End If

        Restart(XBMC_Link_Idle_Timer)
        Restart(XBMC_Link_Check_Timer)

        frmXBMC_Progress.Visible = True

        If HandleEvents(oProgress) Then Return

        If oProgress.ErrorCount > 0 Then
            If Preferences.ShowLogOnError Then
                XBMC_Link_ErrorLog_Timer.Start()
            End If

            'frmXBMC_Progress.Reset
            'Dim ce As New BaseEvent(XbmcController.E.MC_ResetErrorCount,New BaseEventArgs())
            'XbmcControllerQ.Write(ce)       
        End If

        frmXBMC_Progress.UpdateDetails(oProgress)
    End Sub

    Function HandleEvents(oProgress As XBMC_Controller_Progress) As Boolean

        Select Case oProgress.Evt

            Case XbmcController.E.MC_Only_Movies
                MC_Only_Movies = CType(oProgress.Args, ComboList_EventArgs).XbmcMovies
                Assign_FilterGeneral()
                Return True

            Case XbmcController.E.MC_XbmcMcMovies
                oMovies.XbmcMcMovies = CType(oProgress.Args, XBMC_MC_Movies_EventArgs).XbmcMcMovies
                Assign_FilterGeneral()
                Return True

            Case XbmcController.E.MC_XbmcOnlyMovies
                oMovies.XbmcOnlyMovies = CType(oProgress.Args, XBMC_Only_Movies_EventArgs).XbmcOnlyMovies
                Return True

            Case XbmcController.E.MC_XbmcQuit
                SetcbBtnLink()
                Return True

                'Case XbmcController.E.MC_MaxMovieDetails
                '    MaxXbmcMovies = CType(oProgress.Args, XBMC_MaxMovies_EventArgs).XbmcMovies
                '    Return

        End Select

        Return False

    End Function

    Const MaxConseqFailures As Integer = 3

    Dim ConnectSent As Boolean
    Dim XbmcLastLinkState As Boolean
    Dim ConseqFailures As Integer = 0

    Sub SetcbBtnLink(Optional sender As Object = Nothing)
        XBMC_Link_Check_Timer.Stop()

        'Only check when link is idle
        If Not BckWrkScnMovies.IsBusy And XbmcControllerBufferQ.Count = 0 Then
            Dim passed As Boolean = XBMC_TestsPassed

            '
            ' Sometimes the link test fails. Don't know why yet, maybe XBMC is busy...but anyway this should reduce the number of phantom 
            ' link disablings...
            ' 
            If Not IsNothing(sender) Then
                If cbBtnLink.Enabled And Not passed Then
                    ConseqFailures += 1
                Else
                    ConseqFailures = 0
                End If
            Else
                ConseqFailures = 0
            End If

            If IsNothing(sender) Or ConseqFailures <= MaxConseqFailures Then
                cbBtnLink.Enabled = passed

                If cbBtnLink.Enabled Then
                    cbBtnLink.BackColor = IIf(cbBtnLink.Checked, Color.LightGreen, Color.Transparent)

                    If Preferences.XBMC_Link <> cbBtnLink.Checked Then

                        Preferences.XBMC_Link = cbBtnLink.Checked

                        If Preferences.XbmcLinkReady Then
                            XbmcControllerQ.Write(XbmcController.E.ConnectReq, PriorityQueue.Priorities.low)
                        End If

                        Preferences.ConfigSave()
                    End If
                Else
                    cbBtnLink.Checked = False
                    cbBtnLink.BackColor = Color.Transparent
                End If

                tsmiSyncToXBMC.Enabled = cbBtnLink.Enabled And cbBtnLink.Checked
            End If
        End If


        XBMC_Link_Check_Timer.Start()
    End Sub

    Private Sub XBMC_Link_Check_Timer_Elapsed()
        If ProgState = ProgramState.MovieControlsDisabled Then Return
        'If XbmcControllerBufferQ.Count=0 Then
        SetcbBtnLink(XBMC_Link_Check_Timer)
        'End If
    End Sub

    Sub XbmcLink_UpdateArtwork()
        If Preferences.XBMC_Delete_Cached_Images AndAlso Preferences.XbmcLinkReady Then
            Dim m As Movie = oMovies.LoadMovie(workingMovieDetails.fileinfo.fullpathandfilename)
            m.SaveNFO()
        End If
    End Sub
#End Region

    Private Function splashscreenread() As Integer
        Dim scrn As Integer = 0
        Dim checkpath As String = Preferences.applicationPath & "\Settings\screen.xml"
        If File.Exists(checkpath) Then
            Try
                Dim document As XDocument = XDocument.Load(checkpath)
                Dim sc = From t In document.Descendants("screen") Select t.Value
                Dim mten = From t In document.Descendants("MultiEnabled") Select t.Value
                multimonitor = Convert.ToBoolean(mten.First())
                scrn = sc.First().ToInt
                If scrn > NumOfScreens Then scrn = 0
            Catch
                scrn = 0
                multimonitor = False
            End Try
        End If
        Return scrn
    End Function

    Private Sub SplashscreenWrite()
        Dim doc As New XmlDocument
        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration
        Dim root As XmlElement = Nothing
        Dim child As XmlElement = Nothing
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        root = doc.CreateElement("root")
        child = doc.CreateElement("MultiEnabled")
        child.InnerXml = Preferences.MultiMonitoEnabled
        root.AppendChild(child)
        child = doc.CreateElement("screen")
        child.InnerText = CurrentScreen.ToString
        root.AppendChild(child)
        doc.AppendChild(root)
        Dim screenpath As String = Preferences.applicationPath & "\Settings\screen.xml"
        Try
            Dim output As New XmlTextWriter(screenpath, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
            output.Close()
        Catch
        End Try
    End Sub

    Private Sub util_BatchUpdate()

        rescrapeList.ResetFields()
        rescrapeList.mediatags = True
        'rescrapeList.Rename_Files = True

        _rescrapeList.FullPathAndFilenames.Clear()
        For Each movie As ComboList In oMovies.MovieCache
            _rescrapeList.FullPathAndFilenames.Add(movie.fullpathandfilename)
        Next
        RunBackgroundMovieScrape("BatchRescrape")
        'oMovies.BatchRescrapeSpecific(_rescrapeList.FullPathAndFilenames, rescrapeList)    'filteredList

    End Sub

    Sub ClearCacheFolder            
        Try
            Dim cacheFolder As String = applicationPath & "\cache"
            If IO.Directory.Exists(cacheFolder)
                IO.Directory.Delete(cacheFolder, True)                 ' Delete Cache folder as it is re-created when required.
            End If
            
        Catch ex As Exception

        End Try
    End Sub

    Sub CleanCacheFolder(Optional ByVal All As Boolean = False)
        Dim cachefolder As String = applicationPath & "\cache\"
        If IO.Directory.Exists(cacheFolder) Then
            Dim Files As New IO.DirectoryInfo(cachefolder)
            Dim FileList() = Files.GetFiles().OrderByDescending(Function(f) f.LastWriteTime).ToArray
            Dim limit As Integer = If(All, 0, 199)
            Dim i As Integer = FileList.Count
            Try
                If i > limit Then
                    Do Until i = limit
                        i-=1
                        Dim filepath As String = FileList(i).FullName
                        Utilities.SafeDeleteFile(filepath)
                    Loop
                End If
            Catch
            End Try
        End If
    End Sub

    Sub ClearMissingFolder()
        Try
            Dim missingfolder As String = IO.Path.Combine(Preferences.applicationPath, "missing\")
            If IO.Directory.Exists(missingfolder)
                IO.Directory.Delete(missingfolder, True)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub util_MainFormTitleUpdate()
        'Update Main Form Window Title to show Currrent Version
        Dim sAssemblyVersion As String = Trim(System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(",")(1))
        sAssemblyVersion = Microsoft.VisualBasic.Right(sAssemblyVersion, 7)       'Cuts Version=3.4.0.2 down to just 3.4.0.2
        If workingProfile.profilename.ToLower = "default" Then
            Me.Text = "Media Companion - V" & sAssemblyVersion
        Else
            Me.Text = "Media Companion - V" & sAssemblyVersion & " - " & workingProfile.profilename
        End If

    End Sub

    Sub mov_SplitContainerAutoPosition()
        'Set Movie Splitter Auto Position
        Dim pic1ratio As Decimal
        Dim pic2ratio As Decimal
        Try
            If Not IsNothing(PbMovieFanArt.Image) Then
                Dim pic1ImSzW = PbMovieFanArt.Image.Size.Width        'original picture sizes
                Dim pic1ImszH = PbMovieFanArt.Image.Size.Height
                Dim pic2ImSzW = PbMoviePoster.Image.Size.Width
                Dim pic2ImszH = PbMoviePoster.Image.Size.Height
                pic1ratio = pic1ImSzW / pic1ImszH
                pic2ratio = pic2ImSzW / pic2ImszH
                Dim width As Integer = SplitContainer2.Size.Width
                ' MsgBox(from & " = " & width & ":" & Int(SplitContainer2.Size.Width * (pic1ratio / (pic1ratio + pic2ratio))) - 5 & " - " & pic1ImSzW & "x" & pic1ImszH & " " & pic2ImszH & "x" & pic2ImSzW)
            Else 
                pic1ratio = 2
                pic2ratio = 1
            End If
        Catch ex As Exception
            pic1ratio = 2
            pic2ratio = 1
            'MsgBox("Movie Splitter Exception")
        End Try
        SplitContainer2.SplitterDistance = (SplitContainer2.Size.Width - 8) * (pic1ratio / (pic1ratio + pic2ratio))
    End Sub

    Sub tv_SplitContainerAutoPosition()
        'Set TVShow Splitter Auto Position
        Dim pic3ratio As Decimal
        Dim pic4ratio As Decimal
        Dim HorizontalSplit As Decimal
        Try
            If (tv_PictureBoxLeft.Image IsNot Nothing AndAlso tv_PictureBoxRight.Image IsNot Nothing) Then
                Dim pic3ImSzW = tv_PictureBoxLeft.Image.Size.Width
                Dim pic3ImszH = tv_PictureBoxLeft.Image.Size.Height
                Dim pic4ImSzW = tv_PictureBoxRight.Image.Size.Width
                Dim pic4ImszH = tv_PictureBoxRight.Image.Size.Height
                pic3ratio = pic3ImSzW / pic3ImszH
                pic4ratio = pic4ImSzW / pic4ImszH
                HorizontalSplit = ((SplitContainer4.Size.Width - 8) * (pic3ratio / (pic3ratio + pic4ratio)) / pic3ratio)
            Else
                pic3ratio = 2
                pic4ratio = 1
                HorizontalSplit = 235
            End If
        Catch ex As Exception
            pic3ratio = 2
            pic4ratio = 1
            HorizontalSplit = 235
        End Try
        SplitContainer4.SplitterDistance = (SplitContainer4.Size.Width - 8) * (pic3ratio / (pic3ratio + pic4ratio))
        Try     'Try Catch for minimize of MC when Full-screen
            If _tv_SplitContainer.Height > 100 Then
                If Preferences.tvbannersplit = 0 Then
                    _tv_SplitContainer.SplitterDistance = HorizontalSplit
                Else
                    _tv_SplitContainer.SplitterDistance = _tv_SplitContainer.Height * Preferences.tvbannersplit 
                End If
            End If
        Catch
        End Try
    End Sub

    Public Sub mov_CacheLoad()

        mov_PreferencesDisplay
        oMovies.LoadCaches

        If oMovies.MovieCache.Count = 0 Then
            mov_RebuildMovieCaches
            Return
        End If
        filteredList.Clear
        filteredList.AddRange(oMovies.MovieCache)

        Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)

        If DataGridViewMovies.Rows.Count > 0 then
            DataGridViewMovies.Rows(0).Selected = True
        End If

        mov_FormPopulate
    End Sub

    Private Sub util_RegexSetDefaultScraper()
        tv_RegexScraper.Clear()
        tv_RegexScraper.Add("[Ss]([\d]{1,4}).?[Ee]([\d]{1,4})")
        tv_RegexScraper.Add("([\d]{1,4}) ?[xX] ?([\d]{1,4})")
        tv_RegexScraper.Add("([0-9]+)([0-9][0-9])")
    End Sub

    Private Sub util_RegexSetDefaultRename()
        tv_RegexRename.Clear()
        tv_RegexRename.Add("Show Title - S01E01 - Episode Title.ext")
        tv_RegexRename.Add("S01E01 - Episode Title.ext")
        tv_RegexRename.Add("Show Title - 1x01 - Episode Title.ext")
        tv_RegexRename.Add("1x01 - Episode Title.ext")
        tv_RegexRename.Add("Show Title - 101 - Episode Title.ext")
        tv_RegexRename.Add("101 - Episode Title.ext")
    End Sub

    Private Sub util_RegexLoad()

        Dim tempstring As String
        tempstring = workingProfile.regexlist
        tv_RegexScraper.Clear()
        tv_RegexRename.Clear()
        Dim path As String = tempstring
        Dim createDefaultRegexScrape As Boolean = True
        Dim createDefaultRegexRename As Boolean = True

        If File.Exists(path) Then

            Try
                Dim regexList As New XmlDocument
                regexList.Load(path)

                If regexList.DocumentElement.Name = "regexlist" Then
                    For Each result As XmlElement In regexList("regexlist")
                        Select Case result.Name
                            Case "tvregex"                              'This is the old tag before custom renamer was introduced,
                                tv_RegexScraper.Add(result.InnerText)   'so add it to the scraper regex list in case there are custom regexs.
                                createDefaultRegexScrape = False        'The rename regex will not be flagged so regex.xml will be created as new format.
                            Case "tvregexscrape"
                                tv_RegexScraper.Add(result.InnerText)
                                createDefaultRegexScrape = False
                            Case "tvregexrename"
                                tv_RegexRename.Add(result.InnerText)
                                createDefaultRegexRename = False
                        End Select
                    Next
                End If

            Catch ex As Exception
                Call util_RegexSave(True, True)
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        End If
        If createDefaultRegexScrape Or createDefaultRegexRename Then
            Call util_RegexSave(createDefaultRegexScrape, createDefaultRegexRename) 'Valid regex XML doc not available, so create default one.
        End If
    End Sub

    Private Sub util_RegexSave(Optional ByVal setScraperDefault As Boolean = False, Optional ByVal setRenameDefault As Boolean = False)

        Dim path As String = workingProfile.regexlist
        Dim doc As New XmlDocument
        Dim xmlProc As XmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        Dim root As XmlElement
        Dim child As XmlElement

        If setScraperDefault = True Then util_RegexSetDefaultScraper()
        If setRenameDefault = True Then util_RegexSetDefaultRename()

        doc.AppendChild(xmlProc)
        root = doc.CreateElement("regexlist")

        For Each Regex In tv_RegexScraper
            child = doc.CreateElement("tvregexscrape")
            child.InnerText = Regex
            root.AppendChild(child)
        Next

        For Each Regex In tv_RegexRename
            child = doc.CreateElement("tvregexrename")
            child.InnerText = Regex
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Try
            'TODO: Need to fix XmlTextWriter IO error.
            'Surrounded object in Try...Catch to temporarly fix the error.
            Using output As New XmlTextWriter(path, System.Text.Encoding.UTF8) With {.Formatting = Formatting.Indented}
                '"D:\Dados de Utilizador\Freddy Krueger\Ambiente de Trabalho\MediaCompanion-EVRSOEIRANAS\Settings\regex.xml"
                doc.WriteTo(output)
                output.Close()
            End Using
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

    End Sub

    Private Sub util_FilterLoad()
        'If File.Exists(workingProfile.filters) Or Preferences.startupCache = False Then

        '    Dim line As String = String.Empty
        '    CheckedListBox2.Items.Clear()

        '    Try
        '        Dim userConfig As StreamReader = File.OpenText(workingProfile.filters)

        '        Do
        '            Try
        '                line = userConfig.ReadLine

        '                If line <> Nothing Then
        '                    Dim regexMatch As Match
        '                    regexMatch = Regex.Match(line, "<([\d]{2,3})>")

        '                    If regexMatch.Success = False Then
        '                        CheckedListBox2.Items.Add(line)
        '                    End If
        '                End If

        '            Catch ex As Exception
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        Loop Until line = Nothing
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If
    End Sub

    Private Sub GenreMasterLoad()
        genrelist.Clear()
        genrelist = Utilities.loadGenre
    End Sub

    Private Sub util_GenreLoad()
        If File.Exists(workingProfile.Genres) Or Not Preferences.startupCache Then
            Dim line As String = String.Empty
            Dim listof As New List(Of String)
            listof.Clear()
            Try
                Dim userConfig As StreamReader = File.OpenText(workingProfile.Genres)
                Do
                    Try
                        line = userConfig.ReadLine
                        If line <> Nothing Then
                            Dim regexMatch As Match
                            regexMatch = Regex.Match(line, "<([\d]{2,3})>")
                            If regexMatch.Success = False Then
                                listof.Add(line.trim)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                Loop Until line = Nothing
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            If listof.Count > 0 Then
                Dim add As Boolean
                For Each i In listof
                    add = True
                    For Each it In genrelist
                        If it.ToLower = i.ToLower Then
                            add = False
                            Exit For
                        End If
                    Next
                    If add then genrelist.Add(i)
                Next
                genrelist.Sort()
            End If
        End If
    End Sub

    Private Sub util_PrefsLoad()
        Dim tempstring As String
        For Each prof In profileStruct.ProfileList
            If prof.profilename = workingProfile.profilename Then
                tempstring = prof.Config
                If IO.File.Exists(tempstring) Then Preferences.configpath = tempstring
                Preferences.configpath = tempstring

                Me.util_ConfigLoad()
            End If
        Next
        For Each item In Preferences.moviesets
            cbMovieDisplay_MovieSet.Items.Add(item)
        Next
    End Sub

    Private Sub util_ProfilesLoad()
        profileStruct.ProfileList.Clear()
        Dim profilepath As String = IO.Path.Combine(applicationPath, "settings")
        profilepath = IO.Path.Combine(profilepath, "profile.xml")

        Dim notportable As Boolean = False
        Dim path As String = profilepath
        If IO.File.Exists(path) Then
            Try
                Dim profilelist As New XmlDocument
                profilelist.Load(path)
                If profilelist.DocumentElement.Name = "profile" Then
                    For Each thisresult In profilelist("profile")
                        Select Case thisresult.Name
                            Case "default"
                                profileStruct.DefaultProfile = thisresult.innertext
                            Case "startup"
                                profileStruct.StartupProfile = thisresult.innertext
                            Case "profiledetails"
                                Dim currentprofile As New ListOfProfiles
                                For Each result In thisresult.childnodes
                                    Dim t As Integer = result.innertext.ToString.ToLower.IndexOf("\s")
                                    If t > 0 Then notportable = True
                                    Select Case result.name
                                        Case "actorcache"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.ActorCache = applicationPath & s
                                        Case "directorcache"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.DirectorCache = applicationPath & s
                                        Case "config"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.Config = applicationPath & s
                                        Case "moviecache"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.MovieCache = applicationPath & s
                                        Case "profilename"
                                            currentprofile.ProfileName = result.innertext
                                        Case "regex"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.RegExList = applicationPath & s
                                        Case "filters"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.Filters = applicationPath & s
                                        Case "genres"
                                            Dim s As String = ""
                                            If result.innertext = "" Then 
                                                s = "\settings\genres.txt"  'incase missing from existing profile.xml
                                            Else
                                                s = result.innertext.ToString.Substring(t)
                                            End If
                                            currentprofile.Genres = applicationPath & s
                                        Case "tvcache"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.TvCache = applicationPath & s
                                        Case "musicvideocache"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.MusicVideoCache = applicationPath & s
                                        Case "moviesetcache"
                                            Dim s As String = result.innertext.ToString.Substring(t)
                                            currentprofile.MovieSetCache = applicationPath & s
                                    End Select
                                Next
                profileStruct.ProfileList.Add(currentprofile)
                        End Select
                    Next
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If

            End Try
        Else

        End If

        If notportable Then util_ProfileSave()

        If profileStruct.ProfileList.Count > 1 Then
            ProfilesToolStripMenuItem.Visible = True
            ProfilesToolStripMenuItem.Enabled = True
            ProfilesToolStripMenuItem.DropDownItems.Clear()

            For Each prof In profileStruct.ProfileList
                If prof.ProfileName <> Nothing Then
                    ProfilesToolStripMenuItem.DropDownItems.Add(prof.ProfileName)
                End If
            Next
            For Each item In ProfilesToolStripMenuItem.DropDownItems
                If item.text = workingProfile.profilename Then
                    With item
                        item.checked = True
                    End With
                Else
                    item.checked = False
                End If
            Next
        End If
    End Sub

    Private Sub util_ProfileSave()
        Dim profilepath As String = IO.Path.Combine(applicationPath, "settings")
        profilepath = IO.Path.Combine(profilepath, "profile.xml")

        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        Dim childchild As XmlElement
        root = doc.CreateElement("profile")
        child = doc.CreateElement("default")
        child.InnerText = profileStruct.DefaultProfile
        root.AppendChild(child)
        child = doc.CreateElement("startup")
        child.InnerText = profileStruct.StartupProfile
        root.AppendChild(child)
        doc.AppendChild(root)


        For Each prof In profileStruct.ProfileList
            child = doc.CreateElement("profiledetails")

            childchild = doc.CreateElement("actorcache")
            childchild.InnerText = prof.ActorCache.Replace(applicationPath, "")
            child.AppendChild(childchild)
            
            childchild = doc.CreateElement("directorcache")
            childchild.InnerText = prof.DirectorCache.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("config")
            childchild.InnerText = prof.Config.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("moviecache")
            childchild.InnerText = prof.MovieCache.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("profilename")
            childchild.InnerText = prof.ProfileName
            child.AppendChild(childchild)

            childchild = doc.CreateElement("regex")
            childchild.InnerText = prof.RegExList.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("filters")
            childchild.InnerText = prof.Filters.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("genres")
            childchild.InnerText = prof.Genres.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("tvcache")
            childchild.InnerText = prof.TvCache.Replace(applicationPath, "")
            child.AppendChild(childchild)
            'root.AppendChild(child)
            childchild = doc.CreateElement("musicvideocache")
            childchild.InnerText = prof.MusicVideoCache.Replace(applicationPath, "")
            child.AppendChild(childchild)

            childchild = doc.CreateElement("moviesetcache")
            childchild.InnerText = prof.MovieSetCache.Replace(applicationPath, "")
            child.AppendChild(childchild)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)
        Dim saveing As New XmlTextWriter(profilepath, System.Text.Encoding.UTF8)
        saveing.Formatting = Formatting.Indented
        doc.WriteTo(saveing)
        saveing.Close()

        If profileStruct.ProfileList.Count > 1 Then
            ProfilesToolStripMenuItem.Visible = True
            ProfilesToolStripMenuItem.Enabled = True
            ProfilesToolStripMenuItem.DropDownItems.Clear()
            For Each prof In profileStruct.ProfileList
                If prof.ProfileName <> Nothing Then
                    ProfilesToolStripMenuItem.DropDownItems.Add(prof.ProfileName)
                End If
            Next
            For Each item In ProfilesToolStripMenuItem.DropDownItems
                If item.text = workingProfile.profilename Then
                    With item
                        item.checked = True
                    End With
                Else
                    item.checked = False
                End If
            Next
        End If

    End Sub

    Private Sub util_CommandListLoad()
        For Each com In Preferences.commandlist
            ToolsToolStripMenuItem.DropDownItems.Add(com.title)
        Next
    End Sub

    Sub AddRegKey()
        Dim eventLogName As String = "MediaCompanion"
        Dim sourceName As String = "MediaCompanion"
        Dim eventLog As EventLog
        eventLog = New EventLog()
        eventLog.Log = eventLogName
        eventLog.Source = sourceName

        ' Check whether registry key for source exists
        Dim keyName As String = "SYSTEM\CurrentControlSet\Services\EventLog\" & eventLogName & "\" & sourceName
        Dim rkEventSource As RegistryKey = Registry.LocalMachine.OpenSubKey(keyName)

        ' Check whether keys exists
        If rkEventSource Is Nothing Then
	        ' Key doesnt exist. Create key which represents source
	        Dim Proc As New Process()
	        Dim ProcStartInfo As New ProcessStartInfo("Reg.exe")
	        ProcStartInfo.Arguments = "add HKLM\" & keyName
	        ProcStartInfo.UseShellExecute = True
	        ProcStartInfo.Verb = "runas"
	        Proc.StartInfo = ProcStartInfo
	        Proc.Start()
        End If

        Try
	        eventLog.WriteEntry("With key.... :)")
        Catch

	        Debug.Print("failed to write key")
        End Try
    End Sub

    Private Sub mov_ActorRebuild()
   '    mov_FixUpCorruptActors
        oMovies.RebuildMoviePeopleCaches
    End Sub

    Public Sub mov_FormPopulate(Optional yieldIng As Boolean=False)

        If Not IsNothing(workingMovieDetails) Then
            If workingMovie.fullpathandfilename <> workingMovieDetails.fileinfo.fullpathandfilename Then
                Try
                    For i = panelAvailableMoviePosters.Controls.Count - 1 To 0 Step -1
                        panelAvailableMoviePosters.Controls.RemoveAt(i)
                    Next
                Catch
                End Try
                Try
                    For i = Panel2.Controls.Count - 1 To 0 Step -1
                        Panel2.Controls.RemoveAt(i)
                    Next
                Catch
                End Try
                Try
                    TextBox8.Text = ""
                Catch
                End Try
            End If
        End If

        If Yield(yieldIng) Then Return

        If workingMovie.fullpathandfilename <> Nothing And DataGridViewMovies.Rows.Count > 0 Then
            workingMovieDetails = WorkingWithNfoFiles.mov_NfoLoadFull(workingMovie.fullpathandfilename)

            If Yield(yieldIng) Then Return

            If IsNothing(workingMovieDetails) = False Then
                If workingMovieDetails.fullmoviebody.playcount = Nothing Then workingMovieDetails.fullmoviebody.playcount = "0"
                If workingMovieDetails.fullmoviebody.lastplayed = Nothing Then workingMovieDetails.fullmoviebody.lastplayed = ""
                If workingMovieDetails.fullmoviebody.credits = Nothing Then workingMovieDetails.fullmoviebody.credits = ""
                If workingMovieDetails.fullmoviebody.director = Nothing Then workingMovieDetails.fullmoviebody.director = ""
                If workingMovieDetails.fullmoviebody.stars = Nothing Then workingMovieDetails.fullmoviebody.stars = ""
                If workingMovieDetails.fullmoviebody.filename = Nothing Then workingMovieDetails.fullmoviebody.filename = ""
                If workingMovieDetails.fullmoviebody.genre = Nothing Then workingMovieDetails.fullmoviebody.genre = ""
                If workingMovieDetails.fullmoviebody.imdbid = Nothing Then workingMovieDetails.fullmoviebody.imdbid = ""
                If workingMovieDetails.fullmoviebody.mpaa = Nothing Then workingMovieDetails.fullmoviebody.mpaa = ""
                If workingMovieDetails.fullmoviebody.outline = Nothing Then workingMovieDetails.fullmoviebody.outline = ""
                If workingMovieDetails.fullmoviebody.playcount = Nothing Then workingMovieDetails.fullmoviebody.playcount = ""
                If workingMovieDetails.fullmoviebody.plot = Nothing Then workingMovieDetails.fullmoviebody.plot = ""
                If workingMovieDetails.fullmoviebody.premiered = Nothing Then workingMovieDetails.fullmoviebody.premiered = ""
                If workingMovieDetails.fullmoviebody.rating = Nothing Then workingMovieDetails.fullmoviebody.rating = ""
                If workingMovieDetails.fullmoviebody.runtime = Nothing Then workingMovieDetails.fullmoviebody.runtime = ""
                If workingMovieDetails.fullmoviebody.studio = Nothing Then workingMovieDetails.fullmoviebody.studio = ""
                If workingMovieDetails.fullmoviebody.tagline = Nothing Then workingMovieDetails.fullmoviebody.tagline = ""
                If workingMovieDetails.fullmoviebody.title = Nothing Then workingMovieDetails.fullmoviebody.title = ""
                If workingMovieDetails.fullmoviebody.originaltitle = Nothing Then workingMovieDetails.fullmoviebody.originaltitle = ""
                If workingMovieDetails.fullmoviebody.top250 = Nothing Then workingMovieDetails.fullmoviebody.top250 = ""
                If workingMovieDetails.fullmoviebody.trailer = Nothing Then workingMovieDetails.fullmoviebody.trailer = ""
                If workingMovieDetails.fullmoviebody.votes = Nothing Then workingMovieDetails.fullmoviebody.votes = ""
                If workingMovieDetails.fullmoviebody.year = Nothing Then workingMovieDetails.fullmoviebody.year = ""
                If workingMovieDetails.fullmoviebody.source = Nothing Then workingMovieDetails.fullmoviebody.source = ""

                titletxt.Items.Clear()

                titletxt.Items.Add(workingMovieDetails.fullmoviebody.title)
                For Each title In workingMovieDetails.alternativetitles
                    titletxt.Items.Add(title)
                Next
                titletxt.Text = workingMovieDetails.fullmoviebody.title
                TextBox3.Text = workingMovieDetails.fullmoviebody.title & " (" & workingMovieDetails.fullmoviebody.year & ")"
                tbCurrentMoviePoster.Text = workingMovieDetails.fullmoviebody.title & " (" & workingMovieDetails.fullmoviebody.year & ")"
                Me.ToolTip1.SetToolTip(Me.titletxt, "Original Title: '" & workingMovieDetails.fullmoviebody.originaltitle & "'")
                If workingMovieDetails.fullmoviebody.sortorder = "" Then workingMovieDetails.fullmoviebody.sortorder = workingMovieDetails.fullmoviebody.title
                TextBox34.Text  = workingMovieDetails.fullmoviebody.sortorder
                outlinetxt.Text = workingMovieDetails.fullmoviebody.outline
                plottxt.Text    = workingMovieDetails.fullmoviebody.plot
                taglinetxt.Text = workingMovieDetails.fullmoviebody.tagline
                txtStars.Text   = workingMovieDetails.fullmoviebody.stars
                genretxt.Text   = workingMovieDetails.fullmoviebody.genre
                premiertxt.Text = workingMovieDetails.fullmoviebody.premiered 
                creditstxt.Text = workingMovieDetails.fullmoviebody.credits
                directortxt.Text = workingMovieDetails.fullmoviebody.director
                studiotxt.Text  = workingMovieDetails.fullmoviebody.studio
                countrytxt.Text = workingMovieDetails.fullmoviebody.country 
                pathtxt.Text    = workingMovie.fullpathandfilename
                ratingtxt.Text  = workingMovieDetails.fullmoviebody.rating.FormatRating
                imdbtxt.Text    = workingMovieDetails.fullmoviebody.imdbid
                tagtxt.Text     = ""
                If workingMovieDetails.fullmoviebody.tag.Count <> 0 Then
                    For Each t In workingMovieDetails.fullmoviebody.tag
                        tagtxt.Text &= t & ", "
                    Next
                End If
                'Catch exception thrown when votes is an empty string
                If workingMovieDetails.fullmoviebody.votes <> "" Then
                    votestxt.Text = Double.Parse(workingMovieDetails.fullmoviebody.votes.Replace(".",",")).ToString("N0")
                Else
                    votestxt.Text = workingMovieDetails.fullmoviebody.votes
                End If
                certtxt.Text = workingMovieDetails.fullmoviebody.mpaa
                top250txt.Text = workingMovieDetails.fullmoviebody.top250
                If Preferences.movieRuntimeDisplay = "file" Then
                    displayRuntimeScraper = False
                Else
                    displayRuntimeScraper = True
                End If
                Call mov_SwitchRuntime()

                workingMovieDetails.fileinfo.fullpathandfilename = workingMovie.fullpathandfilename
                workingMovieDetails.fileinfo.filename = IO.Path.GetFileName(workingMovie.fullpathandfilename)
                workingMovieDetails.fileinfo.path = IO.Path.GetFullPath(workingMovie.fullpathandfilename)
                workingMovieDetails.fileinfo.foldername = workingMovie.foldername
                'workingMovieDetails.fileinfo.posterpath = Preferences.GetPosterPath(workingMovie.fullpathandfilename, workingMovie.filename)
                'workingMovieDetails.fileinfo.fanartpath = Preferences.GetFanartPath(workingMovie.fullpathandfilename, workingMovie.ActualNfoFileName) 'filename)

                workingMovieDetails.fileinfo.trailerpath = GetTrailerPath(workingMovieDetails.fileinfo.path)
                If Yield(yieldIng) Then Return
                HandleTrailerBtn(workingMovieDetails)
                If Yield(yieldIng) Then Return
                If workingMovieDetails.fileinfo.posterpath <> Nothing Then

                    If Not File.Exists(workingMovieDetails.fileinfo.posterpath) Then
                        If IO.File.Exists(workingMovieDetails.fileinfo.posterpath.Replace(IO.Path.GetFileName(workingMovieDetails.fileinfo.fanartpath), "folder.jpg")) Then
                            workingMovieDetails.fileinfo.posterpath = workingMovieDetails.fileinfo.posterpath.Replace(IO.Path.GetFileName(workingMovieDetails.fileinfo.posterpath), "folder.jpg")
                        End If
                    End If

                End If
                If Yield(yieldIng) Then Return
                If workingMovieDetails.fileinfo.posterpath <> Nothing Then
                    Dim workingposter As String = workingMovieDetails.fileinfo.posterpath
                    'Dim frodoPath As String
                    'If Preferences.FrodoEnabled Then 
                    '    If workingMovieDetails.fileinfo.videotspath<>"" Then
                    '        frodoPath = workingMovieDetails.fileinfo.videotspath+"poster.jpg"
                    '    Else
                    '        frodoPath = workingposter.Replace(".tbn","-poster.jpg")
                    '    End If
                    '    If File.Exists(frodoPath) Then 
                    '        workingposter = frodoPath
                    '    End If
                    'End If

                    util_ImageLoad(PbMoviePoster, workingposter, Utilities.DefaultPosterPath)
                    If Yield(yieldIng) Then Return
                    'util_ImageLoad(PictureBox3, workingMovieDetails.fileinfo.posterpath, Utilities.DefaultPosterPath)
                    util_ImageLoad(PictureBoxAssignedMoviePoster, workingposter, Utilities.DefaultPosterPath)
                    If Yield(yieldIng) Then Return
                    lblCurrentLoadedPoster.Text = "Width: " & PictureBoxAssignedMoviePoster.Image.Width.ToString & "  Height: " & PictureBoxAssignedMoviePoster.Image.Height.ToString
                    lblMovPosterPages.Visible = False
                End If
                If workingMovieDetails.fileinfo.fanartpath <> Nothing Then
                    Dim workingfanart As String = workingMovieDetails.fileinfo.fanartpath
                    'Dim frodoPath As String
                    'If Preferences.FrodoEnabled Then 
                    '    If workingMovieDetails.fileinfo.videotspath<>"" Then
                    '        frodoPath = workingMovieDetails.fileinfo.videotspath+"fanart.jpg"
                    '    Else
                    '        frodoPath = workingfanart
                    '    End If
                    '    If File.Exists(frodoPath) Then 
                    '        workingfanart = frodoPath
                    '    End If
                    'End If

                    util_ImageLoad(PbMovieFanArt, workingfanart, Utilities.DefaultFanartPath)
                    'Rating1.PictureInit = PbMovieFanArt.Image

                End If

                If Yield(yieldIng) Then Return

                If Convert.ToInt32(workingMovieDetails.fullmoviebody.playcount) > 0 Then
                    btnMovWatched.Text = "&Watched"
                    btnMovWatched.BackColor = Color.LawnGreen
                    btnMovWatched.Refresh()
                Else
                    btnMovWatched.Text = "Un&watched"
                    btnMovWatched.BackColor = Color.Red
                    btnMovWatched.Refresh()
                End If

                cbMovieDisplay_Actor.Items.Clear()
                For Each actor In workingMovieDetails.listactors
                    If actor.actorname <> Nothing Then cbMovieDisplay_Actor.Items.Add(actor.actorname)
                Next

                If cbMovieDisplay_Actor.Items.Count > 0 Then
                    cbMovieDisplay_Actor.SelectedIndex = 0
                Else
                    util_ImageLoad(PictureBoxActor, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                    'PictureBoxActor.ImageLocation = Utilities.DefaultActorPath
                    'PictureBoxActor.Load()
                End If

                Dim fi As New FilteredItems(cbFilterActor)

                fi.SelectFirstMatch(cbMovieDisplay_Actor)


                If Yield(yieldIng) Then Return

                If workingMovieDetails.fullmoviebody.movieset.MovieSetName <> "-None-" And workingMovieDetails.fullmoviebody.movieset.MovieSetName <> "" Then
                    Dim add As Boolean = True
                    For Each item In Preferences.moviesets
                        If item = workingMovieDetails.fullmoviebody.movieset.MovieSetName Then
                            add = False
                            Exit For
                        End If
                    Next
                    If add Then
                        Preferences.moviesets.Add(workingMovieDetails.fullmoviebody.movieset.MovieSetName)
                    End If
                End If

                cbMovieDisplay_MovieSet.SelectedItem=Nothing

                pop_cbMovieDisplay_MovieSet

                For f = 0 To cbMovieDisplay_Source.Items.Count - 1
                    If cbMovieDisplay_Source.Items(f) = workingMovieDetails.fullmoviebody.source Then
                        cbMovieDisplay_Source.SelectedIndex = f
                        Exit For
                    End If
                Next

                btnPlayMovie.Enabled = True
                mov_SplitContainerAutoPosition

                Dim video_flags = VidMediaFlags(workingMovieDetails.filedetails)
                movieGraphicInfo.OverlayInfo(PbMovieFanArt, ratingtxt.Text, video_flags,workingMovie.DisplayFolderSize)
                'FanTvArtList.Items.Clear()
                'Panel6.Visible = CheckforExtraArt()
                MovPanel6Update()
            End If
        Else
            cbMovieDisplay_Actor.Items.Clear()
            PictureBoxActor.CancelAsync()
            PictureBoxActor.Image = Nothing
            PictureBoxActor.Refresh()
            
            btnMoviePosterSaveCroppedImage.Enabled = False
            btnMoviePosterResetImage.Enabled = False
            thumbedItsMade = False
            'posterThumbedItsMade = False
            cbMoviePosterSaveLoRes.Enabled = False
            btnPosterTabs_SaveImage.Enabled = False
            btnMovPosterNext.Visible = False
            btnMovPosterPrev.Visible = False
            lblMovPosterPages.Visible = False
            titletxt.Text = ""
            TextBox3.Text = ""
            outlinetxt.Text = ""
            plottxt.Text = ""
            taglinetxt.Text = ""
            txtStars.Text = ""
            genretxt.Text = ""
            premiertxt.Text = ""
            creditstxt.Text = ""
            directortxt.Text = ""
            studiotxt.Text = ""
            countrytxt.Text = ""
            pathtxt.Text = ""
            imdbtxt.Text = ""
            'actorarray.Clear()

            ratingtxt.Text = ""
            runtimetxt.Text = ""
            votestxt.Text = ""
            top250txt.Text = ""
            certtxt.Text = ""
            PbMovieFanArt.Image = Nothing
            PictureBox2.Image = Nothing
            PbMoviePoster.Image = Nothing
            lblMovFanartWidth.Text = ""
            lblMovFanartHeight.Text = ""
            PictureBoxAssignedMoviePoster.Image = Nothing
            lblCurrentLoadedPoster.Text = ""
            TextBox34.Text = ""
            titletxt.Text = ""
            tagtxt.Text = ""

            roletxt.Text = ""
            PictureBoxActor.Image = Nothing
            Panel6.Visible = False
            FanTvArtList.Items.Clear()
            btnPlayMovie.Enabled = False

            Me.Refresh()
            Application.DoEvents()
        End If
        If ratingtxt.Text.IndexOf("/10") <> -1 Then
            ratingtxt.Text = ratingtxt.Text.Replace("/10", "")
            workingMovieDetails.fullmoviebody.rating = ratingtxt.Text
        End If

        If ratingtxt.Text.Length > 3 Then
            ratingtxt.Text = ratingtxt.Text.Substring(0, 3).Trim
        End If

        If Yield(yieldIng) Then Return

        'mov_SplitContainerAutoPosition()
    End Sub

    Public Function CheckforExtraArt() As Boolean
        Dim confirmedpresent As Boolean = False
        If Not Preferences.GetRootFolderCheck(workingMovieDetails.fileinfo.fullpathandfilename) Then
            Dim MovPath As String = IO.Path.GetDirectoryName(workingMovieDetails.fileinfo.fullpathandfilename) & "\"
            If File.Exists(MovPath & "clearart.png") Then FanTvArtList.Items.Add("ClearArt") : confirmedpresent = True
            If File.Exists(MovPath & "logo.png") Then FanTvArtList.Items.Add("Logo") : confirmedpresent = True
            If File.Exists(MovPath & "banner.jpg") Then FanTvArtList.Items.Add("Banner") : confirmedpresent = True
            If File.Exists(MovPath & "landscape.jpg") Then FanTvArtList.Items.Add("Landscape") : confirmedpresent = True
            If File.Exists(MovPath & "disc.png") Then FanTvArtList.Items.Add("Disc") : confirmedpresent = True
            If File.Exists(MovPath & "poster.jpg") AndAlso Not Preferences.posterjpg Then FanTvArtList.Items.Add("Poster") : confirmedpresent = True
            If File.Exists(MovPath & "fanart.jpg") AndAlso Not Preferences.fanartjpg Then FanTvArtList.Items.Add("Fanart") : confirmedpresent = True
            If File.Exists(MovPath & "folder.jpg") Then FanTvArtList.Items.Add("Folder") : confirmedpresent = True
        End If
        Return confirmedpresent 
    End Function

    Public Sub MovPanel6Update()
        FanTvArtList.Items.Clear()
        Panel6.Visible = CheckforExtraArt()
    End Sub

    Private Sub FanTvArtList_Mouse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FanTvArtList.MouseDown
        Dim imagepath As String = Nothing
        Dim item As String = Nothing
        If e.button = Windows.Forms.MouseButtons.Right Then
            Dim index As Integer = FanTvArtList.IndexFromPoint(New Point(e.X, e.Y))
            If index >= 0 Then FanTvArtList.SelectedItem = FanTvArtList.Items(index)
        End If
        If IsNothing(FanTvArtList.SelectedItem) Then Exit Sub
        item = FanTvArtList.SelectedItem.ToString.ToLower
        If Not String.IsNullOrEmpty(item) Then
            imagepath = IO.Path.GetDirectoryName(workingMovieDetails.fileinfo.fullpathandfilename)
            Dim suffix As String = If((item = "clearart" or item = "logo" or item = "disc"),".png", ".jpg")
            imagepath &= "\" & item & suffix
        End If
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ftvArtPicBox.Visible = True
            If Not IsNothing(imagepath) Then util_ImageLoad(ftvArtPicBox, imagepath, "")
        ElseIf e.button = Windows.Forms.MouseButtons.Right Then
            Dim tempint = MessageBox.show("Do you wish to delete this image from" & vbCrLf & "this Movie?", "Fanart.Tv Artwork Delete", MessageBoxButtons.YesNoCancel)
            If tempint = Windows.Forms.DialogResult.No or tempint = DialogResult.Cancel Then Exit Sub
            If tempint = Windows.Forms.DialogResult.Yes Then
                Utilities.SafeDeleteFile(imagepath)
                MovPanel6Update()
            End If
        End If
    End Sub

    Private Sub FanTvArtList_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FanTvArtList.MouseLeave
        FanTvArtList.ClearSelected()
        ftvArtPicBox.Image = Nothing
        ftvArtPicBox.Visible = False
    End Sub

    Private Sub HandleTrailerBtn(ByVal fmd As FullMovieDetails)

        If IsNothing(fmd) Then
            Return
        End If

        DeleteZeroLengthFile(fmd.fileinfo.trailerpath)

        ButtonTrailer.Enabled = False

        If IO.File.Exists(fmd.fileinfo.trailerpath) Then
            ButtonTrailer.Text = "Play Trailer"
            ButtonTrailer.Enabled = True
        Else
            If fmd.fullmoviebody.trailer = ""
                ButtonTrailer.Text = "No trailer found"
            Else
                ButtonTrailer.Text = "Download Trailer"
                ButtonTrailer.Enabled = True
            End If
        End If
    End Sub
    
    Private Function mov_FileCheckValid(ByVal fullpathandfilename As String) As Boolean
        Dim validfile As Boolean = True
        Dim tempint2 As Integer = 0
        Dim tempstring As String
        Dim extension As String
        Dim tempname As String

        'First check, is it a video file!
        extension = System.IO.Path.GetExtension(fullpathandfilename)
        Dim isvideofile As Boolean = False
        For Each extn In Utilities.VideoExtensions
            If extn = extension Then isvideofile = True
        Next
        If Not isvideofile Then Return False

        'if the file is a .vob then check it is not part of a dvd folder (Stop dvdfolders vobs getting seperate nfos)
        If IO.Path.GetExtension(fullpathandfilename) = ".vob" Then
            If IO.File.Exists(fullpathandfilename.Replace(System.IO.Path.GetFileName(fullpathandfilename), "VIDEO_TS.IFO")) Then
                validfile = False
            End If
        End If

        Dim filename2 As String = System.IO.Path.GetFileName(fullpathandfilename).ToLower
        If filename2.IndexOf("cd2") <> -1 Then validfile = False
        If filename2.IndexOf("cd3") <> -1 Then validfile = False
        If filename2.IndexOf("cd4") <> -1 Then validfile = False
        If filename2.IndexOf("cd5") <> -1 Then validfile = False
        If filename2.IndexOf("cd.2") <> -1 Then validfile = False
        If filename2.IndexOf("cd.3") <> -1 Then validfile = False
        If filename2.IndexOf("cd.4") <> -1 Then validfile = False
        If filename2.IndexOf("cd.5") <> -1 Then validfile = False
        If filename2.IndexOf("cd_2") <> -1 Then validfile = False
        If filename2.IndexOf("cd_3") <> -1 Then validfile = False
        If filename2.IndexOf("cd_4") <> -1 Then validfile = False
        If filename2.IndexOf("cd_5") <> -1 Then validfile = False
        If filename2.IndexOf("dvd2") <> -1 Then validfile = False
        If filename2.IndexOf("dvd3") <> -1 Then validfile = False
        If filename2.IndexOf("dvd4") <> -1 Then validfile = False
        If filename2.IndexOf("dvd5") <> -1 Then validfile = False
        If filename2.IndexOf("dvd.2") <> -1 Then validfile = False
        If filename2.IndexOf("dvd.3") <> -1 Then validfile = False
        If filename2.IndexOf("dvd.4") <> -1 Then validfile = False
        If filename2.IndexOf("dvd.5") <> -1 Then validfile = False
        If filename2.IndexOf("dvd_2") <> -1 Then validfile = False
        If filename2.IndexOf("dvd_3") <> -1 Then validfile = False
        If filename2.IndexOf("dvd_4") <> -1 Then validfile = False
        If filename2.IndexOf("dvd_5") <> -1 Then validfile = False
        If filename2.IndexOf("part2") <> -1 Then validfile = False
        If filename2.IndexOf("part3") <> -1 Then validfile = False
        If filename2.IndexOf("part4") <> -1 Then validfile = False
        If filename2.IndexOf("part5") <> -1 Then validfile = False
        If filename2.IndexOf("part.2") <> -1 Then validfile = False
        If filename2.IndexOf("part.3") <> -1 Then validfile = False
        If filename2.IndexOf("part.4") <> -1 Then validfile = False
        If filename2.IndexOf("part.5") <> -1 Then validfile = False
        If filename2.IndexOf("part_2") <> -1 Then validfile = False
        If filename2.IndexOf("part_3") <> -1 Then validfile = False
        If filename2.IndexOf("part_4") <> -1 Then validfile = False
        If filename2.IndexOf("part_5") <> -1 Then validfile = False
        If filename2.IndexOf("disk2") <> -1 Then validfile = False
        If filename2.IndexOf("disk3") <> -1 Then validfile = False
        If filename2.IndexOf("disk4") <> -1 Then validfile = False
        If filename2.IndexOf("disk5") <> -1 Then validfile = False
        If filename2.IndexOf("disk.2") <> -1 Then validfile = False
        If filename2.IndexOf("disk.3") <> -1 Then validfile = False
        If filename2.IndexOf("disk.4") <> -1 Then validfile = False
        If filename2.IndexOf("disk.5") <> -1 Then validfile = False
        If filename2.IndexOf("disk_2") <> -1 Then validfile = False
        If filename2.IndexOf("disk_3") <> -1 Then validfile = False
        If filename2.IndexOf("disk_4") <> -1 Then validfile = False
        If filename2.IndexOf("disk_5") <> -1 Then validfile = False
        If filename2.IndexOf("cd 2") <> -1 Then validfile = False
        If filename2.IndexOf("cd 3") <> -1 Then validfile = False
        If filename2.IndexOf("cd 4") <> -1 Then validfile = False
        If filename2.IndexOf("cd 5") <> -1 Then validfile = False
        If filename2.IndexOf("cd-2") <> -1 Then validfile = False
        If filename2.IndexOf("cd-3") <> -1 Then validfile = False
        If filename2.IndexOf("cd-4") <> -1 Then validfile = False
        If filename2.IndexOf("cd-5") <> -1 Then validfile = False
        If filename2.IndexOf("dvd 2") <> -1 Then validfile = False
        If filename2.IndexOf("dvd 3") <> -1 Then validfile = False
        If filename2.IndexOf("dvd 4") <> -1 Then validfile = False
        If filename2.IndexOf("dvd 5") <> -1 Then validfile = False
        If filename2.IndexOf("dvd-2") <> -1 Then validfile = False
        If filename2.IndexOf("dvd-3") <> -1 Then validfile = False
        If filename2.IndexOf("dvd-4") <> -1 Then validfile = False
        If filename2.IndexOf("dvd-5") <> -1 Then validfile = False
        If filename2.IndexOf("part-2") <> -1 Then validfile = False
        If filename2.IndexOf("part-3") <> -1 Then validfile = False
        If filename2.IndexOf("part-4") <> -1 Then validfile = False
        If filename2.IndexOf("part-5") <> -1 Then validfile = False
        If filename2.IndexOf("part 2") <> -1 Then validfile = False
        If filename2.IndexOf("part 3") <> -1 Then validfile = False
        If filename2.IndexOf("part 4") <> -1 Then validfile = False
        If filename2.IndexOf("part 5") <> -1 Then validfile = False
        If filename2.IndexOf("disk 2") <> -1 Then validfile = False
        If filename2.IndexOf("disk 3") <> -1 Then validfile = False
        If filename2.IndexOf("disk 4") <> -1 Then validfile = False
        If filename2.IndexOf("disk 5") <> -1 Then validfile = False
        If filename2.IndexOf("disk-2") <> -1 Then validfile = False
        If filename2.IndexOf("disk-3") <> -1 Then validfile = False
        If filename2.IndexOf("disk-4") <> -1 Then validfile = False
        If filename2.IndexOf("disk-5") <> -1 Then validfile = False
        If filename2.IndexOf("pt 2") <> -1 Then validfile = False
        If filename2.IndexOf("pt 3") <> -1 Then validfile = False
        If filename2.IndexOf("pt 4") <> -1 Then validfile = False
        If filename2.IndexOf("pt 5") <> -1 Then validfile = False
        If filename2.IndexOf("pt-2") <> -1 Then validfile = False
        If filename2.IndexOf("pt-3") <> -1 Then validfile = False
        If filename2.IndexOf("pt-4") <> -1 Then validfile = False
        If filename2.IndexOf("pt-5") <> -1 Then validfile = False
        If filename2.IndexOf("pt2") <> -1 Then validfile = False
        If filename2.IndexOf("pt3") <> -1 Then validfile = False
        If filename2.IndexOf("pt4") <> -1 Then validfile = False
        If filename2.IndexOf("pt5") <> -1 Then validfile = False
        If filename2.IndexOf("pt_2") <> -1 Then validfile = False
        If filename2.IndexOf("pt_3") <> -1 Then validfile = False
        If filename2.IndexOf("pt_4") <> -1 Then validfile = False
        If filename2.IndexOf("pt_5") <> -1 Then validfile = False
        If filename2.IndexOf("pt.2") <> -1 Then validfile = False
        If filename2.IndexOf("pt.3") <> -1 Then validfile = False
        If filename2.IndexOf("pt.4") <> -1 Then validfile = False
        If filename2.IndexOf("pt.5") <> -1 Then validfile = False
        If filename2.IndexOf("-trailer") <> -1 Then validfile = False
        If filename2.IndexOf(".trailer") <> -1 Then validfile = False
        If filename2.IndexOf("_trailer") <> -1 Then validfile = False
        If filename2.IndexOf("sample") <> -1 And filename2.IndexOf("people") = -1 Then validfile = False


        'check for movies ending a,b,c, etc (moviea, movieb) for multipart. movieb is multipart if moviea exists
        'extension = System.IO.Path.GetExtension(fullpathandfilename)
        tempname = fullpathandfilename.Replace(extension, "")
        If tempname.Substring(tempname.Length - 1) = "b" Or tempname.Substring(tempname.Length - 1) = "c" Or tempname.Substring(tempname.Length - 1) = "d" Or tempname.Substring(tempname.Length - 1) = "e" Or tempname.Substring(tempname.Length - 1) = "B" Or tempname.Substring(tempname.Length - 1) = "C" Or tempname.Substring(tempname.Length - 1) = "D" Or tempname.Substring(tempname.Length - 1) = "E" Then
            tempname = fullpathandfilename.Substring(0, fullpathandfilename.Length - (1 + extension.Length)) & "a" & extension
            If System.IO.File.Exists(tempname) Then validfile = False
        End If

        'now need to deal with multipart rar files
        Dim tempmovie2 As String = fullpathandfilename.Replace(".nfo", ".rar")
        Dim tempmovie As String = String.Empty
        If IO.File.Exists(tempmovie2) = True Then
            If IO.File.Exists(fullpathandfilename) = False Then
                Dim rarname As String = tempmovie2
                Dim SizeOfFile As Integer = FileLen(rarname)
                tempint2 = Convert.ToInt32(Preferences.rarsize) * 1048576
                If SizeOfFile > tempint2 Then
                    Dim mat As Match
                    mat = Regex.Match(rarname, "\.part[0-9][0-9]?[0-9]?[0-9]?.rar")
                    If mat.Success = True Then
                        rarname = mat.Value
                        If rarname.ToLower.IndexOf(".part1.rar") <> -1 Or rarname.ToLower.IndexOf(".part01.rar") <> -1 Or rarname.ToLower.IndexOf(".part001.rar") <> -1 Or rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                            Dim stackrarexists As Boolean = False
                            rarname = fullpathandfilename.Replace(".nfo", ".rar")
                            If rarname.ToLower.IndexOf(".part1.rar") <> -1 Then
                                rarname = rarname.Replace(".part1.rar", ".nfo")
                                If IO.File.Exists(rarname) Then
                                    stackrarexists = True
                                    tempmovie = rarname
                                Else
                                    stackrarexists = False
                                    tempmovie = rarname
                                End If
                            End If
                            If rarname.ToLower.IndexOf(".part01.rar") <> -1 Then
                                rarname = rarname.Replace(".part01.rar", ".nfo")
                                If IO.File.Exists(rarname) Then
                                    stackrarexists = True
                                    tempmovie = rarname
                                Else
                                    stackrarexists = False
                                    tempmovie = rarname
                                End If
                            End If
                            If rarname.ToLower.IndexOf(".part001.rar") <> -1 Then
                                rarname = rarname.Replace(".part001.rar", ".nfo")
                                If IO.File.Exists(rarname) Then
                                    tempmovie = rarname
                                    stackrarexists = True
                                Else
                                    stackrarexists = False
                                    tempmovie = rarname
                                End If
                            End If
                            If rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                                rarname = rarname.Replace(".part0001.rar", ".nfo")
                                If IO.File.Exists(rarname) Then
                                    tempmovie = rarname
                                    stackrarexists = True
                                Else
                                    stackrarexists = False
                                    tempmovie = rarname
                                End If
                            End If
                            If stackrarexists = True Then
                                Dim allok As Boolean = False
                                Try
                                    Dim filechck As IO.StreamReader = IO.File.OpenText(tempmovie)
                                    Do

                                        tempstring = filechck.ReadLine
                                        If tempstring = Nothing Then Exit Do

                                        If tempstring.IndexOf("<movie>") <> -1 Then
                                            allok = True
                                            Exit Do
                                        End If
                                    Loop Until tempstring.IndexOf("</movie>") <> -1
                                    filechck.Close()
                                Catch ex As Exception
#If SilentErrorScream Then
                                    Throw ex
#End If
                                Finally
                                End Try
                                If allok = True Then
                                    validfile = False
                                End If
                            End If
                        Else
                            validfile = False
                        End If
                    End If
                Else
                    validfile = False
                End If
            End If
        End If

        'rename fullpathandfilename to that of the nfo file
        fullpathandfilename = fullpathandfilename.Replace(fullpathandfilename.Substring(fullpathandfilename.LastIndexOf("."), fullpathandfilename.Length - fullpathandfilename.LastIndexOf(".")), ".nfo")

        'check for both variations of the filename
        Dim nfopaths(1) As String
        nfopaths(0) = fullpathandfilename
        nfopaths(1) = fullpathandfilename.Replace(IO.Path.GetFileName(fullpathandfilename), "movie.nfo")
        'check if the file exists
        For f = 0 To 1
            If IO.File.Exists(nfopaths(f)) Then
                'if it does check if it is a valid xbmc nfo, if it is not then move it or delete it according to prefs
                Try
                    Dim filechck As IO.StreamReader = IO.File.OpenText(nfopaths(f))
                    tempstring = filechck.ReadToEnd
                    filechck.Close()
                    If tempstring.IndexOf("<movie>") = -1 And tempstring.IndexOf("</movie>") = -1 Then
                        If Preferences.renamenfofiles = True Then
                            Dim fi As New IO.FileInfo(nfopaths(f))
                            Dim newpath As String = nfopaths(f).Replace(nfopaths(f).Substring(nfopaths(f).LastIndexOf("."), nfopaths(f).Length - nfopaths(f).LastIndexOf(".")), ".info")
                            fi.MoveTo(newpath)
                        End If
                    Else
                        validfile = False
                    End If


                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If

                Finally
                End Try
            End If
        Next
        Return validfile
    End Function

    Private Sub DeleteZeroLengthFile(ByVal fileName)

        If IO.File.Exists(fileName) Then
            If (New IO.FileInfo(fileName)).Length = 0 Then
                Utilities.SafeDeleteFile(fileName)
            End If
        End If

    End Sub

    Private Sub ReloadMovieCacheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadMovieCacheToolStripMenuItem.Click
        'TO DO - REBUILD JUST SELECTED MOVIES
        mov_CacheLoad()
    End Sub

    Private Sub util_VideoMode1(ByVal tempstring As String)
        Dim action As String
        Dim errors As String
        Try
            Dim myProc As Process = Process.Start(tempstring)
        Catch ex As Exception
            errors = ex.ToString
            action = "Dim myProc As Process = Process.Start(" & tempstring & ")"
            Call util_ErrorLog(action, errors)
        End Try
    End Sub

    Private Sub util_VideoMode2(ByVal tempstring As String)
        Dim action As String
        Dim errors As String
        Try
            Dim thePSI As New System.Diagnostics.ProcessStartInfo("wmplayer")
            thePSI.Arguments = """" & tempstring & """"
            System.Diagnostics.Process.Start(thePSI)
        Catch ex As Exception
            errors = ex.ToString
            action = "Dim thePSI As New System.Diagnostics.ProcessStartInfo(""wmplayer"")" & vbCrLf & "thePSI.Arguments = "" & tempstring & """ & vbCrLf & "System.Diagnostics.Process.Start(thePSI)"
            Call util_ErrorLog(action, errors)
        End Try
    End Sub

    Private Sub util_VideoMode4(ByVal tempstring As String)
        Dim action As String
        Dim errors As String
        Try
            Dim myProc As Process = Process.Start("""" & Preferences.selectedvideoplayer & """", """" & tempstring & """")
        Catch ex As Exception
            errors = ex.ToString
            action = "Dim myProc As Process = Process.Start(""" & Preferences.selectedvideoplayer & """," & """" & tempstring & """)"
            Call util_ErrorLog(action, errors)
        End Try
    End Sub

    Private Sub util_ErrorLog(ByVal action As String, Optional ByVal errors As String = "")
        Dim errpath As String = applicationPath & "\error.log"
        Try

            Dim objWriter As New System.IO.StreamWriter(errpath, True)
            objWriter.WriteLine(errors)
            objWriter.WriteLine(action)
            objWriter.WriteLine() '(Chr(13))
            objWriter.Close()
        Catch ex As Exception
            MsgBox("Error, cant write to " & errpath & vbCrLf & vbCrLf & ex.ToString)
        End Try

    End Sub

    Public Sub tv_RefreshLog(ByVal action As String, Optional ByVal errors As String = "", Optional ByVal clear As Boolean = False)
        If Preferences.tvshowrefreshlog = False Then
            Exit Sub
        End If

        Dim errpath As String = IO.Path.Combine(applicationPath, "tvrefresh.log")
        If clear = True Then
            If IO.File.Exists(errpath) Then
                Try
                    IO.File.Delete(errpath)
                Catch ex As Exception
                    MsgBox("Error deleting: " & errpath & vbCrLf & vbCrLf & ex.ToString)
                End Try
            End If
        End If
        Try

            Dim objWriter As New System.IO.StreamWriter(errpath, True)
            objWriter.WriteLine(action)
            If errors <> "" Then
                objWriter.WriteLine(errors)
            End If
            'objWriter.WriteLine() '(Chr(13))
            'objWriter.WriteLine()
            objWriter.Close()
        Catch ex As Exception
            MsgBox("Error, cant write to " & errpath & vbCrLf & vbCrLf & ex.ToString)
        End Try

    End Sub

    Private Sub util_ThreadsRunningCheck()
        'If globalthreadcounter = 0 Then
        '    ToolStripButton10.Visible = False
        'Else
        '    ToolStripButton10.Visible = True
        'End If
    End Sub

    Private Sub util_ThreadsAllExit()
        Dim busy As Boolean = False
        Try
            If bckgroundscanepisodes.IsBusy Then
                busy = True
                bckgroundscanepisodes.CancelAsync()
            End If
            If BckWrkScnMovies.IsBusy Then
                busy = True
                BckWrkScnMovies.CancelAsync()
            End If
            If bckepisodethumb.IsBusy Then
                busy = True
                bckepisodethumb.CancelAsync()
            End If

            Dim exitnowok As Boolean = False
            If busy = True Then
                messbox.TextBox1.Text = "Please Wait"
                messbox.TextBox2.Text = ""
                messbox.TextBox3.Text = "Stopping threads when it is Safe to do so"
                messbox.Refresh()
                messbox.Visible = True
            End If
            Do Until busy = False
                If Not bckepisodethumb.IsBusy And Not bckgroundscanepisodes.IsBusy And Not BckWrkScnMovies.IsBusy Then
                    busy = False
                    Exit Do
                End If
                Threading.Thread.Sleep(100)
                Application.DoEvents()
            Loop
            messbox.Visible = False
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        Finally

        End Try
    End Sub

    Public Sub util_ZoomImage(ByVal file As String)
        bigPanel = New Panel
        With bigPanel
            .Width = Me.Width
            .Height = Me.Height
            .BringToFront()
            .Dock = DockStyle.Fill
        End With

        bigPictureBox = New PictureBox()
        With bigPictureBox
            .Location = New Point(0, 0)
            .Width = bigPanel.Width
            .Height = bigPanel.Height
            .SizeMode = PictureBoxSizeMode.Zoom
            .Visible = True
            .BorderStyle = BorderStyle.Fixed3D
            AddHandler bigPictureBox.DoubleClick, AddressOf util_PicBoxClose
            .Dock = DockStyle.Fill
        End With
        util_ImageLoad(bigPictureBox, file, Utilities.DefaultPosterPath)

        'Dim sizex As Integer = bigpicbox.Width
        'Dim sizey As Integer = bigpicbox.Height

        Dim bigpanellabel As Label
        bigpanellabel = New Label
        With bigpanellabel
            .Location = New Point(20, 200)
            .Width = 170
            .Height = 75
            .Visible = True
            .Text = "Double Click Image To Return To Browser"
            .Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold)
        End With

        Me.bigPanel.Controls.Add(bigpanellabel)
        bigpanellabel.BringToFront()
        Application.DoEvents()

        If Not bigPictureBox.Image Is Nothing And bigPictureBox.Image.Width > 20 Then
            Dim sizey As Integer = bigPictureBox.Image.Height
            Dim sizex As Integer = bigPictureBox.Image.Width
            Dim tempstring As String
            tempstring = "Full Image Resolution :- " & sizex.ToString & " x " & sizey.ToString
            Dim resolutionlbl As New Label
            With resolutionlbl
                .Location = New Point(20, 450)
                .Width = 200
                .Text = tempstring
                .BackColor = Color.Transparent
            End With

            Me.bigPanel.Controls.Add(resolutionlbl)
            resolutionlbl.BringToFront()
            Me.Refresh()
            Application.DoEvents()
            Dim tempstring2 As String = resolutionlbl.Text
        End If

        Me.Controls.Add(bigPanel)
        bigPanel.BringToFront()
        Me.bigPanel.Controls.Add(bigPictureBox)
        Me.Refresh()
    End Sub

    'Private Sub bigpicbox_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles bigPictureBox.LoadCompleted
    '    Try
    '        Dim bigpanellabel As Label
    '        bigpanellabel = New Label
    '        Dim resolutionlbl As Label
    '        With bigpanellabel
    '            .Location = New Point(20, 200)
    '            .Width = 150
    '            .Height = 50
    '            .Visible = True
    '            .Text = "Double Click Image To" & vbCrLf & "Return To Browser"
    '            '   .BringToFront()
    '        End With

    '        Me.bigPanel.Controls.Add(bigpanellabel)
    '        bigpanellabel.BringToFront()
    '        Application.DoEvents()



    '        If Not bigPictureBox.Image Is Nothing And bigPictureBox.Image.Width > 20 Then

    '            Dim sizey As Integer = bigPictureBox.Image.Height
    '            Dim sizex As Integer = bigPictureBox.Image.Width
    '            Dim tempstring As String
    '            tempstring = "Full Image Resolution :- " & sizex.ToString & " x " & sizey.ToString
    '            resolutionlbl = New Label
    '            With resolutionlbl
    '                .Location = New Point(20, 450)
    '                .Width = 180
    '                .Text = tempstring
    '                .BackColor = Color.Transparent
    '            End With

    '            Me.bigPanel.Controls.Add(resolutionlbl)
    '            resolutionlbl.BringToFront()
    '            Me.Refresh()
    '            Application.DoEvents()
    '            Dim tempstring2 As String = resolutionlbl.Text
    '        Else
    '            'bigpicbox.ImageLocation = posterurls(rememberint + 1, 1)
    '        End If
    '    Catch ex As Exception
    '        ExceptionHandler.LogError(ex)
    '    End Try

    'End Sub

    Private Sub util_PicBoxClose()
        Me.Controls.Remove(bigPanel)
        bigPanel = Nothing
        Me.Controls.Remove(bigPictureBox)
        bigPictureBox.Image = Nothing
        bigPictureBox = Nothing
        Me.ControlBox = True
        MenuStrip1.Enabled = True
        'ToolStrip1.Enabled = True
    End Sub

    Public Sub resetallfilters()
        Try
            ResetFilters()
            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)

            Try
                If DataGridViewMovies.SelectedRows.Count = 1 Then
                    If workingMovieDetails.fileinfo.fullpathandfilename = DataGridViewMovies.SelectedCells(NFO_INDEX).Value.ToString Then Return
                End If
            Catch
            End Try

            DisplayMovie()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Sub ResetFilters()
        ProgState = ProgramState.ResettingFilters

        filterOverride = False
        TextBox1.Text = ""
        txt_titlesearch.Text = ""
        txt_titlesearch.BackColor = Color.White
        TextBox1.BackColor = Color.White

        rbTitleAndYear.Checked = True

        cbFilterGeneral.SelectedIndex = 0

        UpdateMinMaxMovieFilters()


        oMovies.ActorsFilter_AlsoInclude.Clear()
        oMovies.SetsFilter_AlsoInclude.Clear()

        cbFilterActor   .UpdateItems(oMovies.ActorsFilter   )
        cbFilterDirector.UpdateItems(oMovies.DirectorsFilter)
        cbFilterSet     .UpdateItems(oMovies.SetsFilter     )


        Dim query = From c As Control In SplitContainer5.Panel2.Controls Where c.Name.IndexOf("cbFilter") = 0 And c.GetType().Namespace = "MC_UserControls"

        For Each c As Object In query
            c.Reset()
        Next

        ProgState = ProgramState.Other
    End Sub

    Sub UpdateMinMaxMovieFilters()
        '        cbFilterVotes         .Min = oMovies.MinVotes
        '        cbFilterVotes         .Max = oMovies.MaxVotes

        If cbFilterVotes     .Visible Then cbFilterVotes     .Values = oMovies.ListVotes
        'If cbFilterFolderSizes.Visible Then cbFilterFolderSizes.Values = oMovies.ListFolderSizes

        If cbFilterFolderSizes.Visible Then 
            cbFilterFolderSizes.Min = oMovies.MinFolderSize
            cbFilterFolderSizes.Max = oMovies.MaxFolderSize
        End If
        If cbFilterYear.Visible Then 
            cbFilterYear.Min = If(oMovies.MinYear < 1850, 1850, oMovies.MinYear)
            cbFilterYear.Max = oMovies.MaxYear
        End If
    End Sub

    'Medianfo.dll to outputlog
    Private Sub util_FileDetailsGet()
        Try
            Dim tempstring As String = String.Empty
            Dim appPath As String = ""
            Dim exists As Boolean
            Dim movieinfo As String = String.Empty
            Dim medianfoexists As Boolean = False
            tempstring = Utilities.GetFileName(pathtxt.Text)
            If IO.Path.GetFileName(tempstring).ToLower = "video_ts.ifo" Then
                Dim temppath As String = tempstring.Replace(IO.Path.GetFileName(tempstring), "VTS_01_0.IFO")
                If IO.File.Exists(temppath) Then
                    tempstring = temppath
                End If
            End If
            Dim fileisiso As Boolean = (IO.Path.GetExtension(tempstring).ToLower = ".iso")
            If fileisiso Then
                If applicationPath.IndexOf("/") <> -1 Then appPath = applicationPath & "/" & "mediainfo-rar.exe"
                If applicationPath.IndexOf("\") <> -1 Then appPath = applicationPath & "\" & "mediainfo-rar.exe"
                If Not IO.File.Exists(appPath) Then
                    MsgBox("Unable to find th file ""mediainfo-rar.exe""" & vbCrLf & vbCrLf & "Please make sure this file is available in the programs root directory")
                Exit Sub
                End If
                Try
                    Dim NewProcess As New System.Diagnostics.Process()

                    With NewProcess.StartInfo
                        .FileName = appPath
                        .Arguments = tempstring
                        .RedirectStandardOutput = True
                        .RedirectStandardError = True
                        .RedirectStandardInput = True
                        .UseShellExecute = False
                        .WindowStyle = ProcessWindowStyle.Hidden
                        .CreateNoWindow = False 
                    End With
                    Dim To_Display As String = ""
                    NewProcess.Start()
                    movieinfo = NewProcess.StandardOutput.ReadToEnd
                Catch ex As Exception 
                End Try
            Else
            If applicationPath.IndexOf("/") <> -1 Then appPath = applicationPath & "/" & "MediaInfo.dll"
            If applicationPath.IndexOf("\") <> -1 Then appPath = applicationPath & "\" & "MediaInfo.dll"
            exists = IO.File.Exists(appPath)
            If exists = True Then
                medianfoexists = True
            End If
            If medianfoexists = False Then
                MsgBox("Unable to find th file ""MediaInfo.dll""" & vbCrLf & vbCrLf & "Please make sure this file is available in the programs root directory")
                Exit Sub
            End If
            Dim To_Display As String = ""
            Dim tempstring5 As String
            
            Dim MI As mediainfo
            MI = New mediainfo
            tempstring5 = MI.Option_("Info_Version", "0.7.0.0;MediaInfoDLL_Example_MSVB;0.7.0.0")
            If (tempstring5.Length() = 0) Then
                TextBox1.Text = "MediaInfo.Dll: this version of the DLL is not compatible"
                Exit Sub
            End If
            
            If IO.File.Exists(tempstring) Then
                MI.Open(tempstring)
                To_Display = MI.Inform
                movieinfo = To_Display
                MI.Close()
            End If
            End If
            

            TextBox8.Text = movieinfo
        Catch
        End Try
    End Sub

    Private Sub mov_Rescrape()

        If outlinetxt.Text = "MC cannot find this file, either the file no longer exists, or MC cannot access the file path" Then
            MsgBox("MC cannot find this file, either the file no longer exists, or MC cannot access the file path")
            Exit Sub
        End If

        If workingMovieDetails Is Nothing Then Exit Sub

        If workingMovieDetails.fullmoviebody.title = Nothing And workingMovieDetails.fullmoviebody.imdbid = Nothing Then
            MsgBox("Can't rescrape this movie because it doesn't have any NFO File" & vbCrLf & "refresh movie database, and search for new movies", MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Dim tempint = MessageBox.Show("Rescraping the movie will Overwrite all the current details" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If tempint = DialogResult.No Then
            Exit Sub
        End If

        RunBackgroundMovieScrape("RescrapeDisplayedMovie")

    End Sub

    Private Sub mov_SaveQuick()

        If DataGridViewMovies.SelectedRows.Count = 0 Then Return

        If DataGridViewMovies.SelectedRows.Count = 1 Then
            Dim movie As Movie = oMovies.LoadMovie(workingMovieDetails.fileinfo.fullpathandfilename)
            movie.ScrapedMovie.fullmoviebody.title = titletxt.Text.Replace(" (" & workingMovieDetails.fullmoviebody.year & ")", "")
            If movie.ScrapedMovie.fullmoviebody.originaltitle = Nothing Or movie.ScrapedMovie.fullmoviebody.originaltitle = "" Then
                movie.ScrapedMovie.fullmoviebody.originaltitle = movie.ScrapedMovie.fullmoviebody.title
            End If
            movie.ScrapedMovie.fullmoviebody.director = directortxt.Text
            movie.ScrapedMovie.fullmoviebody.playcount = workingMovieDetails.fullmoviebody.playcount
            movie.ScrapedMovie.fullmoviebody.lastplayed = workingMovieDetails.fullmoviebody.lastplayed 
            movie.ScrapedMovie.fullmoviebody.credits = creditstxt.Text
            movie.ScrapedMovie.fullmoviebody.studio = studiotxt.Text
            movie.ScrapedMovie.fullmoviebody.country = countrytxt.Text 
            movie.ScrapedMovie.fullmoviebody.genre = genretxt.Text
            movie.ScrapedMovie.fullmoviebody.premiered = premiertxt.Text
            movie.ScrapedMovie.fullmoviebody.votes = votestxt.Text
            movie.ScrapedMovie.fullmoviebody.top250 = top250txt.Text
            movie.ScrapedMovie.fullmoviebody.rating = ratingtxt.Text
            movie.ScrapedMovie.fullmoviebody.runtime = runtimetxt.Text
            movie.ScrapedMovie.fullmoviebody.outline = outlinetxt.Text
            movie.ScrapedMovie.fullmoviebody.plot = plottxt.Text
            movie.ScrapedMovie.fullmoviebody.tagline = taglinetxt.Text
            movie.ScrapedMovie.fullmoviebody.stars = txtStars.Text.ToString.Replace(", See full cast and crew", "")
            movie.ScrapedMovie.fullmoviebody.mpaa = certtxt.Text
            movie.ScrapedMovie.fullmoviebody.sortorder = TextBox34.Text
            If movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName <> cbMovieDisplay_MovieSet.Items(cbMovieDisplay_MovieSet.SelectedIndex) Then
                movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName = cbMovieDisplay_MovieSet.Items(cbMovieDisplay_MovieSet.SelectedIndex)
                movie.ScrapedMovie.fullmoviebody.movieset.MovieSetId = oMovies.GetMSetId(movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName)
            End If
            movie.ScrapedMovie.fullmoviebody.source = If(cbMovieDisplay_Source.SelectedIndex = 0, Nothing, cbMovieDisplay_Source.Items(cbMovieDisplay_Source.SelectedIndex))
            If TabControl2.SelectedTab.Name = "TabPage9" Then
                movie.ScrapedMovie.fullmoviebody.tag = NewTagList
            End If
            movie.AssignMovieToCache()
            movie.UpdateMovieCache()
            movie.SaveNFO()
            UpdateFilteredList()
        Else
            Dim mess As New frmMessageBox("Saving Selected Movies", , "     Please Wait.     ")  'Multiple movies selected
            mess.TextBox3.Text = "Press ESC to cancel"
            mess.TopMost = True
            mess.Show()
            mess.Refresh()
            Application.DoEvents()
            Dim Startfullpathandfilename As String = ""
            If Not ISNothing(DataGridViewMovies.CurrentRow) Then
                Dim i As Integer = DataGridViewMovies.CurrentRow.Index
                Startfullpathandfilename = DataGridViewMovies.Item(0, i).Value.ToString
                mess.Cancelled = False
                Dim pos As Integer = 0
                Dim NfosToSave As List(Of String) = (From x As datagridviewrow In DataGridViewMovies.SelectedRows Select nfo=x.Cells("fullpathandfilename").Value.ToString).ToList
                For Each nfo As String In NfosToSave
                    If Not File.Exists(nfo) Then Continue For
                    Dim movie As Movie = oMovies.LoadMovie(nfo)
                    If IsNothing(movie) Then Continue For
                    pos += 1
                    mess.TextBox2.Text = pos.ToString + " of " + NfosToSave.Count.ToString
                    If directortxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.director = directortxt.Text
                    If creditstxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.credits = creditstxt.Text
                    If genretxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.genre = genretxt.Text
                    If premiertxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.premiered = premiertxt.Text
                    If certtxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.mpaa = certtxt.Text
                    If outlinetxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.outline = outlinetxt.Text
                    If runtimetxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.runtime = runtimetxt.Text
                    If studiotxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.studio = studiotxt.Text
                    If countrytxt.Text <> "" then movie.ScrapedMovie.fullmoviebody.country = countrytxt.Text 
                    If plottxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.plot = plottxt.Text
                    If taglinetxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.tagline = taglinetxt.Text
                    If txtStars.Text <> "" Then movie.ScrapedMovie.fullmoviebody.stars = txtStars.Text.ToString.Replace(", See full cast and crew", "")
                    If ratingtxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.rating = ratingtxt.Text
                    If votestxt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.votes = votestxt.Text
                    If top250txt.Text <> "" Then movie.ScrapedMovie.fullmoviebody.top250 = top250txt.Text
                    If Not cbMovieDisplay_MovieSet.SelectedIndex = 0 Then 'cbMovieDisplay_MovieSet.SelectedItem = "-None-"
                        movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName = cbMovieDisplay_MovieSet.Items(cbMovieDisplay_MovieSet.SelectedIndex)
                        movie.ScrapedMovie.fullmoviebody.movieset.MovieSetId = oMovies.GetMSetId(movie.ScrapedMovie.fullmoviebody.movieset.MovieSetName)
                    End If
                    movie.ScrapedMovie.fullmoviebody.source = If(cbMovieDisplay_Source.SelectedIndex = 0, Nothing, cbMovieDisplay_Source.Items(cbMovieDisplay_Source.SelectedIndex))
                    If TabControl2.SelectedTab.Name = "TabPage9" Then
                        movie.ScrapedMovie.fullmoviebody.tag = NewTagList
                    End If
                    movie.AssignMovieToCache()
                    movie.UpdateMovieCache()
                    movie.SaveNFO()
                    Application.DoEvents()
                    If mess.Cancelled Then Exit For
                Next

                ProgState = ProgramState.Other

            Else
                mess.Close()
                MsgBox("Must Select an Initial Movie" & vbCrLf & "Save Cancelled")
                Exit Sub
            End If

            workingMovie.fullpathandfilename = Startfullpathandfilename
            mov_FormPopulate()
            mess.Close()
        End If
    End Sub

    Private Sub Mov_OpenMovieFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripOpenFolder.Click
        Try
            'Try
            If Not workingMovieDetails.fileinfo.fullpathandfilename Is Nothing Then
                Call util_OpenFolder(workingMovieDetails.fileinfo.fullpathandfilename)
            Else
                MsgBox("There is no Movie selected to open")
            End If
            'Catch
            'End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Mov_OpenFileToolStripMenuItem2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripViewNfo.Click
        Try
            Utilities.NfoNotepadDisplay(workingMovieDetails.fileinfo.fullpathandfilename, Preferences.altnfoeditor)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub PosterBrowserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripPosterBrowserAlt.Click
        Try
            Dim t As New frmCoverArt
            If Preferences.MultiMonitoEnabled Then
                Dim w As Integer = t.Width
                Dim h As Integer = t.Height
                t.Bounds = screen.AllScreens(CurrentScreen).Bounds
                t.StartPosition = FormStartPosition.Manual
                t.Width = w
                t.Height = h
            End If
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mov_ToolStripFanartBrowserAlt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripFanartBrowserAlt.Click
        Try
            Dim t As New frmMovieFanart
            If Preferences.MultiMonitoEnabled Then
                Dim w As Integer = t.Width
                Dim h As Integer = t.Height
                t.Bounds = screen.AllScreens(CurrentScreen).Bounds
                t.StartPosition = FormStartPosition.Manual
                t.Width = w
                t.Height = h
            End If
            t.ShowDialog()
            Try
                If IO.File.Exists(workingMovieDetails.fileinfo.fanartpath) Then
                    util_ImageLoad(PbMovieFanArt, workingMovieDetails.fileinfo.fanartpath, Utilities.DefaultFanartPath)
                Else
                    PbMovieFanArt.Image = Nothing
                End If

            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub EditMovieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripEditMovieAlt.Click
        Try
            Call mov_Edit()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mov_Edit()
        If outlinetxt.Text = "MC cannot find this file, either the file no longer exists, or MC cannot access the file path" Then
            MsgBox("MC cannot find this file, either the file no longer exists, or MC cannot access the file path")
            Exit Sub
        End If
        Dim oldmovietitle As String = workingMovieDetails.fileinfo.fullpathandfilename
        Dim newmovietitle As String = ""
        Dim MyFormObject As New Form2()
        MyFormObject.ShowDialog()
        newmovietitle = workingMovieDetails.fileinfo.fullpathandfilename


        For f = 0 To oMovies.MovieCache.Count - 1
            If oMovies.MovieCache(f).fullpathandfilename = newmovietitle Then
                Dim newfullmovie As New ComboList

                oMovies.MovieCache.RemoveAt(f)

                newfullmovie = nfoFunction.mov_NfoLoadBasic(workingMovieDetails.fileinfo.fullpathandfilename, "movielist")
                If workingMovie.title <> "ERROR" Then   'if there is a problem with the nfo being invalid we need to skip
                    oMovies.MovieCache.Add(newfullmovie)
                End If
                Exit For
            End If
        Next

        'Todo: need to update all details after edit.

        For f = 0 To filteredList.Count - 1
            If filteredList(f).fullpathandfilename = oldmovietitle Then
                Dim newfullmovie2 As New ComboList
                newfullmovie2 = filteredList(f)
                filteredList.RemoveAt(f)
                Dim filecreation2 As New IO.FileInfo(workingMovieDetails.fileinfo.fullpathandfilename)
                Dim myDate2 As Date = filecreation2.LastWriteTime
                Try
                    newfullmovie2.filedate = Format(myDate2, datePattern).ToString
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                newfullmovie2.filename = workingMovieDetails.fileinfo.filename
                newfullmovie2.foldername = workingMovieDetails.fileinfo.foldername
                newfullmovie2.fullpathandfilename = workingMovieDetails.fileinfo.fullpathandfilename
                newfullmovie2.genre = workingMovieDetails.fullmoviebody.genre
                newfullmovie2.id = workingMovieDetails.fullmoviebody.imdbid
                newfullmovie2.playcount = workingMovieDetails.fullmoviebody.playcount
                newfullmovie2.lastplayed = workingMovieDetails.fullmoviebody.lastplayed 
                newfullmovie2.rating = workingMovieDetails.fullmoviebody.rating.ToRating
                newfullmovie2.year = workingMovieDetails.fullmoviebody.year
                filteredList.Add(newfullmovie2)
                Exit For
            End If
        Next
        Call mov_FormPopulate()
        Call Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
    End Sub

    Private Sub MediaCompanionForumToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MediaCompanionForumToolStripMenuItem.Click
        Try
            Dim webAddress As String = "http://billyad2000.darkbb.com/forum.htm"
            'Process.Start(webAddress)
            OpenUrl(webAddress)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub XBMCMCThreadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XBMCMCThreadToolStripMenuItem.Click
        Try
            Dim webAddress As String = "http://forum.xbmc.org/showthread.php?t=129134"
            'Process.Start(webAddress)
            OpenUrl(webAddress)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListMoviesWithoutFanartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            filterOverride = True
            Dim newlist As New List(Of ComboList)
            newlist.Clear()
            For Each movie In oMovies.MovieCache
                If Not IO.File.Exists(Preferences.GetFanartPath(movie.fullpathandfilename)) Then
                    Dim movietoadd As New ComboList
                    movietoadd.fullpathandfilename = movie.fullpathandfilename
                    '       movietoadd.titleandyear = movie.titleandyear
                    movietoadd.filename = movie.filename
                    movietoadd.year = movie.year
                    movietoadd.filedate = movie.filedate
                    movietoadd.foldername = movie.foldername
                    movietoadd.rating = movie.rating
                    movietoadd.top250 = movie.top250
                    newlist.Add(movietoadd)
                End If
            Next

            filteredList = newlist
            'Call mov_MovieComboLoad()
            mov_CacheLoad()
            filterOverride = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListMoviesWithoutPostersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            filterOverride = True
            Dim newlist As New List(Of ComboList)
            newlist.Clear()
            For Each movie In oMovies.MovieCache
                If Not IO.File.Exists(Preferences.GetPosterPath(movie.fullpathandfilename)) Then
                    Dim movietoadd As New ComboList
                    movietoadd.fullpathandfilename = movie.fullpathandfilename
                    '   movietoadd.titleandyear = movie.titleandyear
                    movietoadd.filename = movie.filename
                    movietoadd.year = movie.year
                    movietoadd.filedate = movie.filedate
                    movietoadd.foldername = movie.foldername
                    movietoadd.rating = movie.rating
                    movietoadd.top250 = movie.top250
                    newlist.Add(movietoadd)
                End If
            Next

            filteredList = newlist
            'Call mov_MovieComboLoad()
            mov_CacheLoad()
            filterOverride = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub BatchRescraperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatchRescraperToolStripMenuItem.Click
       
        rescrapeList.ResetFields()

        Dim displaywizard As New frmBatchScraper
        displaywizard.ShowDialog()

        If rescrapeList.AnyEnabled Then

            _rescrapeList.FullPathAndFilenames.Clear()

            If DataGridViewMovies.SelectedRows.Count > 1 Then   'run batch wizard on multiple selected movies
                For Each row As DataGridViewRow In DataGridViewMovies.SelectedRows
                    Dim fullpath As String = row.Cells("fullpathandfilename").Value.ToString
                    If Not File.Exists(fullpath) Then Continue For
                    _rescrapeList.FullPathAndFilenames.Add(fullpath)
                Next
            Else                                                 'Otherwise run batch wizard on all movies.
                For Each row As DataGridViewRow In DataGridViewMovies.Rows

                    Dim m As Data_GridViewMovie = row.DataBoundItem
                    Dim fullpath As String = m.fullpathandfilename.ToString
                    If Not File.Exists(fullpath) Then Continue For
                    _rescrapeList.FullPathAndFilenames.Add(fullpath)
                Next
            End If

            RunBackgroundMovieScrape("BatchRescrape")
        End If
        
    End Sub

    Public Function GetTrailerPath(ByVal s As String)
        Dim FileName As String = ""

        For Each extension In Utilities.TrailerExtensions  '"mp4,flv,webm".Split(",")
            FileName = IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), System.IO.Path.GetFileNameWithoutExtension(s) & "-trailer" & extension)

            If File.Exists(FileName) Then Return FileName
        Next

        'set default trailer path and filename
        Return IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), System.IO.Path.GetFileNameWithoutExtension(s) & "-trailer.flv")
    End Function

    Private Sub mov_Play(ByVal type As String)
        If DataGridViewMovies.SelectedRows.Count < 1 Then Return
        Dim tempstring As String
        tempstring = DataGridViewMovies.SelectedCells(NFO_INDEX).Value.ToString
        Dim playlist As New List(Of String)
        Select Case type
            Case "Movie"
                If tempstring.IndexOf("index.nfo") <> -1 Then
                    tempstring = tempstring.Replace(".nfo", ".bdmv")
                Else
                    tempstring = Utilities.GetFileName(tempstring)
                End If
                playlist = Utilities.GetMediaList(tempstring)
            Case "Trailer"
                Dim movie = oMovies.LoadMovie(tempstring)
                If movie.TrailerExists Then playlist.Add(movie.ActualTrailerPath)
            Case "HomeMovie"
                tempstring = CType(ListBox18.SelectedItem, ValueDescriptionPair).Value
                tempstring = Utilities.GetFileName(tempstring)
                playlist = Utilities.GetMediaList(tempstring)
        End Select

        If playlist.Count <= 0 Then
            MsgBox("No Media File Found For This nfo")
            Exit Sub
        End If

        LaunchPlayList(playlist)
        
    End Sub

    Private Sub LaunchPlayList(ByVal plist As List(Of String))
        Dim tempstring = applicationPath & "\settings\temp.m3u"
        frmSplash2.Text = "Playing Movie..."
        frmSplash2.Label1.Text = "Creating m3u file....." & vbCrLf & tempstring
        frmSplash2.Label1.Visible = True
        frmSplash2.Label2.Visible = False
        frmSplash2.ProgressBar1.Visible = False
        frmSplash2.Show()
        Application.DoEvents()
        Dim aok As Boolean = True
        If IO.File.Exists(tempstring) Then
            aok = Utilities.SafeDeleteFile(tempstring)
        End If
        If aok Then
            'Dim file As IO.StreamWriter = IO.File.CreateText(tempstring)
            Dim file As New StreamWriter(tempstring, False, Encoding.GetEncoding(1252))

            For Each part In plist
                If part <> Nothing Then file.WriteLine(part)
            Next
            file.Close()

            frmSplash2.Label1.Text = "Launching Player....."

            If Preferences.videomode = 1 Then Call util_VideoMode1(tempstring)
            If Preferences.videomode = 2 Then Call util_VideoMode2(tempstring)
            If Preferences.videomode = 3 Then
                Preferences.videomode = 2
                Call util_VideoMode2(tempstring)
            End If
            If Preferences.videomode >= 4 Then
                If Preferences.selectedvideoplayer <> Nothing Then
                    Call util_VideoMode4(tempstring)
                Else
                    Call util_VideoMode1(tempstring)
                End If
            End If
        Else
            frmSplash2.Hide()
            MsgBox("Failed to create playlist")
        End If

        frmSplash2.Hide()
    End Sub

    Private Sub MovieFormInit()
        workingMovie.filedate = Nothing
        workingMovie.filename = Nothing
        workingMovie.foldername = Nothing
        workingMovie.fullpathandfilename = Nothing
        workingMovie.genre = Nothing
        workingMovie.id = Nothing
        workingMovie.playcount = Nothing
        workingMovie.lastplayed = Nothing
        workingMovie.rating = Nothing
        workingMovie.title = Nothing
        '    workingMovie.titleandyear = Nothing
        workingMovie.top250 = Nothing
        workingMovie.year = Nothing
        workingMovie.MovieSet = Nothing
        titletxt.Text = ""
        TextBox3.Text = ""
        outlinetxt.Text = ""
        TextBox34.Text = ""
        plottxt.Text = ""
        taglinetxt.Text = ""
        txtStars.Text = ""
        genretxt.Text = ""
        premiertxt.Text = ""
        creditstxt.Text = ""
        directortxt.Text = ""
        studiotxt.Text = ""
        countrytxt.Text = ""
        pathtxt.Text = ""
        cbMovieDisplay_Actor.Items.Clear()
        ratingtxt.Text = ""
        runtimetxt.Text = ""
        votestxt.Text = ""
        top250txt.Text = ""
        certtxt.Text = ""
        PbMovieFanArt.Image = Nothing
        PbMoviePoster.Image = Nothing
        roletxt.Text = ""
        PictureBoxActor.Image = Nothing
        btnPlayMovie.Enabled = False
    End Sub

    Public Sub genretxt_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles genretxt.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Try
                Dim item() As String = workingMovieDetails.fullmoviebody.genre.Split("/")
                Dim genre As String = ""
                Dim listof As New List(Of String)
                listof.Clear()
                For Each i In item
                    listof.Add(i.Trim)
                Next
                Dim frm As New frmGenreSelect 
                frm.SelectedGenres = listof
                frm.Init()
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    listof.Clear()
                    listof.AddRange(frm.SelectedGenres)
                    For each g In listof
                        If genre = "" Then
                            genre = g
                        Else
                            genre += " / " & g
                        End If
                    Next
                    workingMovieDetails.fullmoviebody.genre = genre
                    genretxt.Text = genre
                    Call mov_SaveQuick()
                End If
                frm.Dispose()
            Catch
            End Try
        End If
    End Sub

    Public Sub DisplayMovie(Optional yielding As Boolean = False)
        Try
            DisplayMovie(DataGridViewMovies.SelectedCells, DataGridViewMovies.SelectedRows, yielding)
        Catch
            Return
        End Try
    End Sub

    Public Sub DisplayMovie(ByVal selectedCells As DataGridViewSelectedCellCollection, ByVal selectedRows As DataGridViewSelectedRowCollection, yielding As Boolean)

        Try
            If selectedRows.Count = 1 Then
                If LastMovieDisplayed = selectedCells(NFO_INDEX).Value.ToString Then Return
                LastMovieDisplayed = selectedCells(NFO_INDEX).Value.ToString
            Else
                LastMovieDisplayed = ""
            End If
        Catch
            Return
        End Try

        If yielding Then
            _yield = True
            Application.DoEvents()
            _yield = False
        End If

        'Clear all fields of the movie
        MovieFormInit()

        If selectedRows.Count = 0 Then Exit Sub

        Dim needtoload As Boolean = False
        Dim done As Boolean = False
        If selectedRows.Count > 1 Then
            mov_ToolStripPlayMovie.Visible = False
            mov_ToolStripOpenFolder.Visible = False
            mov_ToolStripViewNfo.Visible = False
            ToolStripSeparator17.Visible = False
            ToolStripSeparator24.Visible = False
            'ToolStripSeparator4.Visible = False
            mov_ToolStripFanartBrowserAlt.Visible = False
            mov_ToolStripPosterBrowserAlt.Visible = False
            mov_ToolStripEditMovieAlt.Visible = False
            mov_ToolStripReloadFromCache.Visible = False
        End If

        If Yield(yielding) Then Return

        If selectedRows.Count = 1 Then
            mov_ToolStripPlayMovie.Visible = True
            mov_ToolStripOpenFolder.Visible = True
            mov_ToolStripViewNfo.Visible = True
            ToolStripSeparator17.Visible = True
            ToolStripSeparator24.Visible = True
            ToolStripSeparator4.Visible = True
            mov_ToolStripFanartBrowserAlt.Visible = True
            mov_ToolStripPosterBrowserAlt.Visible = True
            mov_ToolStripEditMovieAlt.Visible = True
            mov_ToolStripReloadFromCache.Visible = True
            If titletxt.Visible = False Then
                needtoload = True
            End If
            titletxt.Visible = True
            TextBoxMutisave.Visible = False
            btnMovRescrape.Visible = True
            SplitContainer2.Visible = True
            Label128.Visible = False
            Label75.Visible = True
            TextBox34.Visible = True

            If Yield(yielding) Then Return

            If Yield(yielding) Then Return

            Dim query = From f In oMovies.Data_GridViewMovieCache Where f.fullpathandfilename = selectedCells(NFO_INDEX).Value.ToString

            Dim queryList As List(Of Data_GridViewMovie) = query.ToList()

            If Yield(yielding) Then Return
            If Not File.Exists(queryList(0).MoviePathAndFileName) Then   'Detect if video file is missing
                If Mov_MissingMovie(queryList) Then Exit Sub
            End If

            If queryList.Count > 0 Then
                workingMovie.filedate = queryList(0).filedate
                workingMovie.filename = queryList(0).filename
                workingMovie.foldername = queryList(0).foldername
                workingMovie.fullpathandfilename = queryList(0).fullpathandfilename
                workingMovie.genre = queryList(0).genre
                workingMovie.id = queryList(0).id
                workingMovie.playcount = queryList(0).playcount
                workingMovie.rating = queryList(0).Rating
                workingMovie.title = queryList(0).title
                workingMovie.top250 = queryList(0).top250
                workingMovie.year = queryList(0).year
                workingMovie.FolderSize = queryList(0).FolderSize
                mov_ToolStripPlayTrailer.Visible = Not queryList(0).MissingTrailer
                Call mov_FormPopulate(yielding)
            Else
                If needtoload = True Then Call mov_FormPopulate(yielding)
            End If
            done = True
        Else
            outlinetxt.Text = ""
            PbMovieFanArt.Image = Nothing
            PbMoviePoster.Image = Nothing
            roletxt.Text = ""
            PictureBoxActor.Image = Nothing
            SplitContainer2.Visible = False
            titletxt.Visible = False
            Label75.Visible = False
            TextBox34.Visible = False
            TextBoxMutisave.Visible = True
            Panel6.Visible = False
            FanTvArtList.Items.Clear()
            btnMovRescrape.Visible = False
            Label128.Visible = True
            cbMovieDisplay_MovieSet.Items.Insert(0, "")
            cbMovieDisplay_MovieSet.SelectedIndex = 0
            Dim add As Boolean = True
            Dim watched As String = ""
            For Each sRow As DataGridViewRow In selectedRows
                Dim old As String = watched
                For Each item In oMovies.MovieCache
                    If item.fullpathandfilename = sRow.Cells(NFO_INDEX).Value.ToString Then
                        If watched = "" Then
                            watched = item.playcount
                            old = watched
                        Else
                            watched = item.playcount
                        End If
                        If watched <> old Then
                            add = False
                        End If
                        Exit For
                    End If

                    If Yield(yielding) Then Return
                Next
            Next
            If add = False Then
                btnMovWatched.Text = ""
                btnMovWatched.BackColor = Color.Gray
            Else
                If watched = "1" Then
                    btnMovWatched.Text = "&Watched"
                    btnMovWatched.BackColor = Color.LawnGreen
                    btnMovWatched.Refresh()
                Else
                    btnMovWatched.Text = "Un&watched"
                    btnMovWatched.BackColor = Color.Red
                    btnMovWatched.Refresh()
                End If
            End If
        End If
    End Sub

#Region "DataGridViewMovies Events"
    Private Sub DataGridViewMovies_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewMovies.CellClick
        _yield = True
        Application.DoEvents()
        _yield = False
        DisplayMovie()
    End Sub

    Private Sub DataGridViewMovies_ColumnHeaderMouseClick( sender As Object,  e As DataGridViewCellMouseEventArgs) Handles DataGridViewMovies.ColumnHeaderMouseClick
        btnreverse.Checked = Not btnreverse.Checked
        btnreverse_CheckedChanged(Nothing,Nothing)
    End Sub

    Private Sub DataGridViewMovies_DoubleClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles DataGridViewMovies.DoubleClick
        Try
            Dim info = DataGridViewMovies.HitTest(e.X, e.Y)

            If info.Type <> DataGridViewHitTestType.ColumnHeader Then
                mov_Play("Movie")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DataGridViewMovies_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridViewMovies.DragDrop

        Dim files() As String

        files = e.Data.GetData(DataFormats.FileDrop)
        For f = 0 To UBound(files)
            If IO.File.Exists(files(f)) Then
                ' This path is a file.
                Dim skip As Boolean = False
                For Each item In oMovies.MovieCache
                    If item.fullpathandfilename = files(f) Then
                        skip = True
                        Exit For
                    End If
                Next
                For Each item In droppedItems
                    If item = files(f) Then
                        skip = True
                        Exit For
                    End If
                Next
                If mov_FileCheckValid(files(f)) = True Then
                    If skip = False Then droppedItems.Add(files(f))
                End If
            Else
                If IO.Directory.Exists(files(f)) Then
                    ' This path is a directory.
                    Dim di As New IO.DirectoryInfo(files(f))
                    Dim diar1 As IO.FileInfo() = di.GetFiles()
                    Dim dra As IO.FileInfo

                    'list the names of all files in the specified directory
                    For Each dra In diar1
                        Dim skip As Boolean = False
                        For Each item In oMovies.MovieCache
                            If item.fullpathandfilename = dra.FullName Then
                                skip = True
                                Exit For
                            End If
                        Next
                        For Each item In droppedItems
                            If item = dra.FullName Then
                                skip = True
                                Exit For
                            End If
                        Next
                        If mov_FileCheckValid(dra.FullName) = True Then
                            If skip = False Then droppedItems.Add(dra.FullName)
                        End If
                    Next
                End If
            End If
        Next

        If droppedItems.Count > 0 Then
            DoScrapeDroppedFiles()
        End If

    End Sub

    Private Sub DataGridViewMovies_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridViewMovies.DragEnter
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DataGridViewMovies_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridViewMovies.KeyUp
        DisplayMovie(True)
    End Sub

    Private Sub DataGridViewMovies_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DataGridViewMovies.Keypress
	    If [Char].IsLetter(e.KeyChar) or [Char].IsDigit(e.KeyChar) Then
            Dim ekey As String = e.KeyChar.ToString.ToLower
		    For i As Integer = 0 To (DataGridViewMovies.Rows.Count) - 1
                Dim rtitle As String = DataGridViewMovies.Rows(i).Cells("DisplayTitle").Value.ToString.ToLower
			    If rtitle.StartsWith(ekey) Then
                    Dim icell As Integer = DataGridViewMovies.CurrentCell.ColumnIndex 
                    DataGridViewMovies.CurrentCell = DataGridViewMovies.Rows(i).Cells(icell)
                    DisplayMovie()
				    Return
			    End If
		    Next
	    End If
    End Sub
    
    Private Sub DataGridViewMovies_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridViewMovies.MouseLeave
        TooltipGridViewMovies1.Visible = False
        TimerToolTip.Enabled = False
    End Sub

    Private Sub DataGridViewMovies_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridViewMovies.MouseHover
        TimerToolTip.Start()
    End Sub

    Private Sub DataGridViewMovies_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridViewMovies.MouseMove
        Try
            Dim MousePos       As Point                    = DataGridViewMovies.PointToClient(Control.MousePosition)
            Dim objHitTestInfo As DataGridView.HitTestInfo = DataGridViewMovies.HitTest      (MousePos.X, MousePos.Y)
            Dim MouseRowIndex  As Integer                  = objHitTestInfo.RowIndex
 
            TimerToolTip.Enabled = True

            If MouseRowIndex > -1 Then
                Dim Runtime       As String = ""
                Dim RatingRuntime As String = ""
                Dim movietitle    As String =              DataGridViewMovies.Rows(MouseRowIndex).Cells("Title" ).Value.ToString
                Dim movieYear     As String =              DataGridViewMovies.Rows(MouseRowIndex).Cells("Year"  ).Value.ToString
                Dim Rating        As String = "Rating: " & DataGridViewMovies.Rows(MouseRowIndex).Cells("Rating").Value.ToString.FormatRating

                If DataGridViewMovies.Rows(MouseRowIndex).Cells("Runtime").Value.ToString.Length > 3 Then
                    Runtime = "Runtime: " & DataGridViewMovies.Rows(MouseRowIndex).Cells("IntRuntime").Value.ToString
                End If

                RatingRuntime = Rating & "     " & Runtime

                Dim Plot As String = DataGridViewMovies.Rows(MouseRowIndex).Cells("Plot").Value.ToString

                If objHitTestInfo.RowY > -1 Then
                    TooltipGridViewMovies1.Visible = Preferences.ShowMovieGridToolTip

                    TooltipGridViewMovies1.Textinfo(Plot)
                    TooltipGridViewMovies1.TextLabelMovieYear(movieYear)
                    TooltipGridViewMovies1.TextMovieName(movietitle)
                    TooltipGridViewMovies1.TextLabelRatingRuntime(RatingRuntime)

                    TooltipGridViewMovies1.Left = MousePos.X+10
                    TooltipGridViewMovies1.Top  = MousePos.Y+TooltipGridViewMovies1.Height+30
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub DataGridViewMovies_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridViewMovies.MouseUp
        mov_ToolStripPlayTrailer.Visible = True

        If e.Button = MouseButtons.Right Then
            Dim multiselect As Boolean = False
            'If more than one movie is selected, check if right-click is on the selection.
            If DataGridViewMovies.SelectedRows.Count > 1 Then
                multiselect = True
            End If
            'Otherwise, bring up the context menu for a single movie

            If multiselect = True Then
                mov_ToolStripMovieName.BackColor = Color.Orange
                mov_ToolStripMovieName.Text = "Multisave Mode"
                mov_ToolStripMovieName.Font = New Font("Arial", 10, FontStyle.Bold)
                mov_ToolStripPlayTrailer.Visible = False    'multisave mode the "Play Trailer' is always hidden
            Else

                Try
                    'update context menu with movie name & also if we show the 'Play Trailer' menu item
                    mov_ToolStripMovieName.BackColor = Color.Honeydew
                    mov_ToolStripMovieName.Text = "'" & DataGridViewMovies.SelectedCells(NFO_INDEX+4).Value.ToString & "'"
                    mov_ToolStripMovieName.Font = New Font("Arial", 10, FontStyle.Bold)

                    Dim movie As Data_GridViewMovie = (From f In oMovies.Data_GridViewMovieCache Where f.fullpathandfilename = DataGridViewMovies.selectedCells(NFO_INDEX).Value.ToString).ToList(0)

                    mov_ToolStripPlayTrailer.Visible = Not movie.MissingTrailer
                Catch
                End Try
            End If
        End If
    End Sub

#End Region ' DataGridViewMovies  Events

    Private Function Mov_MissingMovie(ByVal qrylst As List(Of Data_GridViewMovie)) As Boolean
        Dim missingstr As String
        Dim Filepathandname As String
        Filepathandname = qrylst(0).fullpathandfilename.Replace(".nfo","")
        missingstr = "Video file Missing" & vbCrLf & Filepathandname & vbCrLf & "Do you wish to remove from database?"
        If MsgBox(missingstr,MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Mov_RemoveMovie()
            Return True
        End If
    Return False
    End Function

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        Try
            If filterOverride = False Then
                If TextBox1.Text.Length > 0 Then
                    TextBox1.BackColor = Color.Pink
                Else
                    TextBox1.BackColor = Color.White
                End If
                TextBox1.Refresh()
                Call Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox_GenreFilter_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If lockedList = False Then
                lockedList = True
            Else
                lockedList = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TabControl2_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl2.MouseWheel
        Try
            If TabControl2.SelectedIndex = 1 Then
                mouseDelta = e.Delta / 120
                Try
                    TabPage22.AutoScrollPosition = New Point(0, TabPage22.VerticalScroll.Value - (mouseDelta * 30))
                Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TabControl2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.SelectedIndexChanged

        mov_PreferencesDisplay()
        Dim tempstring As String = ""
        Dim tab As String = TabControl2.SelectedTab.Text

        If tab <> "Main Browser" And tab <> "Folders" And tab <> "Movie Preferences" And tab <> "Media Stubs" Then
            If workingMovieDetails Is Nothing And movieFolders.Count = 0 And Preferences.offlinefolders.Count = 0 Then
                Me.TabControl2.SelectedIndex = currentTabIndex
                MsgBox("There are no movies in your list to work on" & vbCrLf & "Add movie folders in the Folders Tab" & vbCrLf & "Then select the ""Search For New Movies"" Tab")
                Exit Sub
            ElseIf workingMovieDetails Is Nothing And movieFolders.Count > 0 And tab <> "Search for new movies" Then
                Me.TabControl2.SelectedIndex = currentTabIndex
                If oMovies.MovieCache.Count > 0 Then
                    MsgBox("No Movie is selected")
                    Exit Sub
                Else
                    MsgBox("There are no movies in your list to work on" & vbCrLf & "If The Folders You Have Added In The" & vbCrLf & "Preferences Menu Contain Movie Files" & vbCrLf & "Then select the ""Search For New Movies"" Tab")
                    Exit Sub
                End If
            End If
        Else
            currentTabIndex = Me.TabControl2.SelectedIndex
        End If

        If tab = "" Then
            If workingMovieDetails.fullmoviebody.imdbid <> Nothing OrElse workingMovieDetails.fullmoviebody.tmdbid <> "" Then
                Dim weburl As String = ""
                Dim TMDB As String = workingMovieDetails.fullmoviebody.tmdbid 
                Dim IMDB As String = workingMovieDetails.fullmoviebody.imdbid
                Dim TMDBLan As List(Of String) = Utilities.GetTmdbLanguage(Preferences.TMDbSelectedLanguageName)
                If (IMDB <> "" And IMDB <> "0") AndAlso TMDB <> "" Then
                    Dim t As New frmMessageBox("Please Select","your preferred site","","2","1")
                    t.ShowDialog()
                    If Preferences.WebSite = "tmdb" Then
                        weburl = "http://www.themoviedb.org/movie/" & TMDB & "?language=" & TMDBLan(0) 'de"
                    Else
                        weburl = "http://www.imdb.com/title/" & IMDB & "/"
                    End If
                ElseIf (IMDB <> "" And IMDB <> "0") AndAlso TMDB = "" Then
                    weburl = "http://www.imdb.com/title/" & IMDB & "/"
                ElseIf IMDB = "0" AndAlso TMDB <> "" Then
                    weburl = "http://www.themoviedb.org/movie/" & TMDB & "?language=" & TMDBLan(0) 'de"
                End If

                If Preferences.externalbrowser = True Then
                    Me.TabControl2.SelectedIndex = currentTabIndex

                    'AnotherPhil bug fix - If the default browser is <goz> IE <goz/> then not stating the exe throws an exception
                    OpenUrl(weburl)
                Else

                    Try
                        If WebBrowser2.Url.AbsoluteUri.ToLower.ToString <> weburl Then
                            WebBrowser2.Stop()
                            WebBrowser2.ScriptErrorsSuppressed = True

                            WebBrowser2.Navigate(weburl)
                            WebBrowser2.Refresh()
                            currentTabIndex = TabControl2.SelectedIndex
                        End If
                    Catch
                        WebBrowser2.Stop()
                        WebBrowser2.ScriptErrorsSuppressed = True

                        WebBrowser2.Navigate(weburl)
                        WebBrowser2.Refresh()
                        currentTabIndex = TabControl2.SelectedIndex
                    End Try
                End If
            Else
                MsgBox("No IMDB or TMDB ID is available for this movie")
            End If

        ElseIf tab = "Main Browser" Then

            'Need to update displayed movies list as user may have invalidated it by have a 'missing...' filter selected and assigning one or more missing items
            currentTabIndex = TabControl2.SelectedIndex
            UpdateFilteredList()

        ElseIf tab.ToLower = "file details" Then
            currentTabIndex = TabControl2.SelectedIndex
            If TextBox8.Text = "" Then Call util_FileDetailsGet()

        ElseIf tab.ToLower = "fanart" Then
            Dim isrootfolder As Boolean = False
            If Preferences.movrootfoldercheck Then
                For Each moviefolder In movieFolders
                    Dim movfolder As String = workingMovieDetails.fileinfo.fullpathandfilename.Replace("\" & workingMovieDetails.fileinfo.filename, "")
                    If moviefolder = movfolder Then isrootfolder = True 'Check movie isn't in a rootfolder, if so, disable extrathumbs option from displaying
                Next
            End If
            GroupBoxFanartExtrathumbs.Enabled = Not isrootfolder 'Or usefoldernames Or allfolders ' Visible 'hide or show fanart/extrathumbs depending of if we are using foldenames or not (extrathumbs needs foldernames to be used)
            If Panel2.Controls.Count = 0 Then
                Call mov_FanartLoad()
            End If
            currentTabIndex = TabControl2.SelectedIndex
            UpdateMissingFanartNav()
            EnableFanartScrolling()
            ElseIf tab.ToLower = "posters" Then
                currentTabIndex = TabControl2.SelectedIndex
                gbMoviePostersAvailable.Refresh()
                UpdateMissingPosterNav()
            ElseIf tab.ToLower = "change movie" Then
                Call mov_ChangeMovieSetup(MovieSearchEngine)
                currentTabIndex = TabControl2.SelectedIndex
            ElseIf tab.ToLower = "wall" Then
                Call mov_WallSetup()
            ElseIf tab.ToLower = "movie & tag sets" Then
                Call MovieSetsAndTagsSetup()
            ElseIf tab.ToLower = "fanart.tv"
                UcFanartTv1.ucFanartTv_Refresh(workingMovieDetails)
            ElseIf tab.ToLower = "movie preferences" Then
                Call mov_PreferencesSetup()

            ElseIf tab.ToLower = "table" Then
                currentTabIndex = TabControl2.SelectedIndex
                Call mov_TableSetup()
            Else
                currentTabIndex = TabControl2.SelectedIndex
            End If

    End Sub

    Private Sub mov_ChangeMovieSetup(ByVal engine As String)
        Dim tempstring As String = ""
        Dim isroot As Boolean = Preferences.GetRootFolderCheck(workingMovieDetails.fileinfo.fullpathandfilename)
        If Preferences.usefoldernames = False OrElse isroot Then
            tempstring = Utilities.RemoveFilenameExtension(IO.Path.GetFileName(workingMovieDetails.fileinfo.fullpathandfilename))
        Else
            tempstring = Utilities.GetLastFolder(workingMovieDetails.fileinfo.fullpathandfilename)
        End If
        If workingMovieDetails.fileinfo.fullpathandfilename.ToLower.IndexOf("\video_ts\") <> -1 Then
            tempstring = Utilities.GetLastFolder(workingMovieDetails.fileinfo.fullpathandfilename)
        End If
        Dim url As String
        If engine = "imdb" Then
            url = Preferences.imdbmirror & "find?s=tt&q=" & Utilities.CleanFileName(tempstring)
        Else
            url = "http://www.themoviedb.org/search?query=" & Utilities.CleanFileName(tempstring)
        End If
        WebBrowser1.Stop()
        WebBrowser1.ScriptErrorsSuppressed = True
        WebBrowser1.Navigate(url)
        WebBrowser1.Refresh()
        Panel2.Visible = True
    End Sub

    
    Private Sub util_OpenFolder(ByVal path As String)
        Dim tempstring As String = ""
        Dim action As String = String.Empty
        Dim errors As String = String.Empty
        Try
            If path <> "" Then
                tempstring = path
                Try
                    If Not tempstring.ToLower.Contains("nfo") Then
                        errors = "Trying to open Folder :- " & tempstring
                        action = "Command - ""Call Shell(""explorer """ & tempstring & ", AppWinStyle.NormalFocus)"""
                        Call Shell("explorer " & """" & tempstring & """", AppWinStyle.NormalFocus) 'opens the selected folder showing contents, no focus on any file.
                    Else
                        errors = "Trying to open Folder :- " & tempstring
                        action = "Command - ""Call Shell(""explorer /select,""" & tempstring & ", AppWinStyle.NormalFocus)"""
                        Call Shell("explorer /select," & """" & tempstring & """", AppWinStyle.NormalFocus) 'this shows the item as selected provided as tempstring i.e. a folder or a file (.nfo)
                    End If
                    'Process.Start(pathtxt.Text)
                    'errors = "Trying to open Folder :- " & tempstring
                    'action = "Command - ""Call Shell(""explorer /select,""" & tempstring & ", AppWinStyle.NormalFocus)"""
                Catch ex As Exception
                    MsgBox("Can't open folder :- " & path)
                    Call util_ErrorLog(action, errors)
                End Try
            Else
                MsgBox("No Folder To Open")
            End If


        Catch
            MsgBox("No Folder To Open")
        End Try
    End Sub

    Private Sub mov_FanartLoad()
        rbMovFanart.Checked = True
        Dim isfanartpath As String = workingMovieDetails.fileinfo.fanartpath
        Dim isvideotspath As String = If(workingMovieDetails.fileinfo.videotspath = "", "", workingMovieDetails.fileinfo.videotspath + "fanart.jpg")
        Dim movfanartpath As String = Utilities.DefaultFanartPath
        If isfanartpath <> Nothing Or isvideotspath <> "" Then
            Try
                If IO.File.Exists(isvideotspath) Then
                    movfanartpath = isvideotspath 
                    ''Dim OriginalImage As New Bitmap(isvideotspath)
                    ''Dim Image2 As New Bitmap(OriginalImage)
                    ''OriginalImage.Dispose()
                    ''PictureBox2.Image = Image2 'moviethumb - 3
                    'util_ImageLoad(PictureBox2, isvideotspath, Utilities.DefaultFanartPath)
                    'lblMovFanartWidth.Text = PictureBox2.Image.Width
                    'lblMovFanartHeight.Text = PictureBox2.Image.Height
                ElseIf IO.File.Exists(isfanartpath) Then
                    movfanartpath = isfanartpath 
                    ''Dim OriginalImage As New Bitmap(isfanartpath)
                    ''Dim Image2 As New Bitmap(OriginalImage)
                    ''OriginalImage.Dispose()
                    ''PictureBox2.Image = Image2 'moviethumb - 3
                    'util_ImageLoad(PictureBox2, isvideotspath, Utilities.DefaultFanartPath)
                    'lblMovFanartWidth.Text = PictureBox2.Image.Width
                    'lblMovFanartHeight.Text = PictureBox2.Image.Height
                'Else
                '    Dim OriginalImage As New Bitmap(Utilities.DefaultBannerPath)
                '    Dim Image2 As New Bitmap(OriginalImage)
                '    OriginalImage.Dispose()
                '    PictureBox2.Image = Image2 'moviethumb - 3
                '    lblMovFanartWidth.Text = PictureBox2.Image.Width
                '    lblMovFanartHeight.Text = PictureBox2.Image.Height
                End If
                util_ImageLoad(PictureBox2, movfanartpath, Utilities.DefaultFanartPath)
                If movfanartpath = "" Then
                    lblMovFanartWidth.Text = ""
                    lblMovFanartHeight.Text = ""
                Else
                    lblMovFanartWidth.Text = PictureBox2.Image.Width
                    lblMovFanartHeight.Text = PictureBox2.Image.Height
                End If
                'If Not IO.File.Exists(isfanartpath) And Not IO.File.Exists(isvideotspath) Then
                '    PictureBox2.ImageLocation = Utilities.DefaultFanartPath 'moviethumb - 3
                '    lblMovFanartWidth.Text = ""
                '    lblMovFanartHeight.Text = ""
                'End If
            Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
            End Try
        End If

        Me.Refresh()
  '     Application.DoEvents()
        fanartArray.Clear()
        noFanart = False


        Dim tmdb As New TMDb '(workingmoviedetails.fullmoviebody.imdbid)
        tmdb.Imdb = If(workingMovieDetails.fullmoviebody.imdbid.Contains("tt"), workingMovieDetails.fullmoviebody.imdbid, "")
        tmdb.TmdbId = workingMovieDetails.fullmoviebody.tmdbid 
        fanartArray.AddRange(tmdb.Fanart)

        Try
            If fanartArray.Count > 0 Then


                Dim location As Integer = 0
                Dim itemcounter As Integer = 0
                For Each item In fanartArray
                    fanartBoxes() = New PictureBox()

                    With fanartBoxes
                        .Location = New Point(0, location)
                        If fanartArray.Count > 2 Then
                            .Width = 410
                            .Height = 233
                        Else
                            .Width = 424
                            .Height = 243
                        End If
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .ImageLocation = item.ldUrl
                        .Visible = True
                        .BorderStyle = BorderStyle.Fixed3D
                        .Name = "moviefanart" & itemcounter.ToString
                        AddHandler fanartBoxes.DoubleClick, AddressOf util_ZoomImage2
                    End With
                    If fanartArray.Count > 2 Then
                        fanartCheckBoxes() = New RadioButton()
                        With fanartCheckBoxes
                            .BringToFront()
                            .Location = New Point(199, location + 229)
                            .Name = "moviefanartcheckbox" & itemcounter.ToString
                        End With


                        resLabels = New Label()
                        With resLabels
                            .BringToFront()
                            .Location = New Point(0, location + 235)
                            .Name = "label" & itemcounter.ToString
                            .Text = "(" & item.hdwidth & " x " & item.hdheight & ") (" & item.ldwidth & " x " & item.ldheight & ")"
                            .Width = 200
                        End With

                        itemcounter += 1
                        location += 260
                    Else
                        fanartCheckBoxes() = New RadioButton()
                        With fanartCheckBoxes
                            .BringToFront()
                            .Location = New Point(199, location + 243)
                            .Name = "moviefanartcheckbox" & itemcounter.ToString
                        End With


                        resLabels = New Label()
                        With resLabels
                            .BringToFront()
                            .Location = New Point(0, location + 249)
                            .Name = "label" & itemcounter.ToString
                            .Text = "(" & item.hdwidth & " x " & item.hdheight & ") (" & item.ldwidth & " x " & item.ldheight & ")"
                            .Width = 200
                        End With

                        itemcounter += 1
                        location += 275
                    End If
                    Me.Panel2.Controls.Add(fanartBoxes())
                    Me.Panel2.Controls.Add(fanartCheckBoxes())
                    Me.Panel2.Controls.Add(resLabels)
                    Me.Refresh()
          '          Application.DoEvents()
                Next

                EnableFanartScrolling()
            Else
                Dim mainlabel2 As Label
                mainlabel2 = New Label
                With mainlabel2
                    .Location = New Point(0, 100)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Width = 500
                    .Height = 400
                    .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                    .Text = "No Fanart Was Found At" & Environment.NewLine & "www.themoviedb.org For This Movie"
                End With

                Me.Panel2.Controls.Add(mainlabel2)
            End If
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    'Set focus on the first checkbox to enable mouse wheel scrolling 
    Sub EnableFanartScrolling()
        Try
            Dim rb As RadioButton = Panel2.Controls("moviefanartcheckbox0")

            rb.Select()                       'Causes RadioButtons checked state to toggle
            rb.Checked = Not rb.Checked     'Undo unwanted checked state toggling
        Catch
        End Try
    End Sub

    Private Sub util_ZoomImage2(ByVal sender As Object, ByVal e As EventArgs)

        Dim tempstring As String = sender.name
        Dim tempstring2 As String = String.Empty
        Dim tempint As Integer = 0
        If tempstring.IndexOf("poster") <> -1 Then
            tempstring = tempstring.Replace("poster", "")
            tempint = Convert.ToDecimal(tempstring)
            'tempstring2 = posterarray(tempint + 1).hdposter
            If tempstring2 = Nothing Then
                tempint = Convert.ToDecimal(tempstring)
                tempint = tempint + ((currentPage - 1) * 10)
                tempstring2 = posterArray(tempint).hdUrl
            End If
        End If
        If tempstring.IndexOf("picture") <> -1 Then
            tempstring = tempstring.Replace("picture", "")
            tempint = Convert.ToDecimal(tempstring)
            tempstring2 = fanartUrls(tempint + 1, 0)
        End If
        If tempstring.IndexOf("moviefanart") <> -1 Then
            tempstring = tempstring.Replace("moviefanart", "")
            tempint = Convert.ToDecimal(tempstring)
            tempstring2 = fanartArray(tempint).hdUrl
        End If
        If tempstring.IndexOf("tvfanart") <> -1 Then
            tempstring = tempstring.Replace("tvfanart", "")
            tempint = Convert.ToDecimal(tempstring)
            tempstring2 = listOfTvFanarts(tempint).bigUrl
        End If
        Dim buffer(4000000) As Byte
        Dim size As Integer = 0
        Dim bytesRead As Integer = 0

        bigPanel = New Panel
        With bigPanel
            .Width = Me.Width
            .Height = Me.Height
            .BringToFront()
            .Dock = DockStyle.Fill
        End With
        Me.Controls.Add(bigPanel)
        bigPanel.BringToFront()
        bigPictureBox = New PictureBox()

        With bigPictureBox
            .Location = New Point(0, 0)
            .Width = bigPanel.Width
            .Height = bigPanel.Height
            .SizeMode = PictureBoxSizeMode.Zoom
            '.Image = sender.image
            .WaitOnLoad = True

            .Visible = False
            .BorderStyle = BorderStyle.Fixed3D
            AddHandler bigPictureBox.DoubleClick, AddressOf util_PicBoxClose
            .Dock = DockStyle.Fill
        End With
        Try
            'bigPictureBox.ImageLocation = tempstring2
            'bigPictureBox.Load()
            util_ImageLoad(bigPictureBox, tempstring2, "")
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try



        Me.bigPanel.Controls.Add(bigPictureBox)
        Dim bigpanellabel As Label
        bigpanellabel = New Label
        With bigpanellabel
            .Location = New Point(20, 200)
            .Width = 150
            .Height = 50
            .Visible = True
            .Text = "Double Click Image To" & vbCrLf & "Return To Browser"
            '   .BringToFront()
        End With

        Me.bigPanel.Controls.Add(bigpanellabel)
        bigpanellabel.BringToFront()
        Application.DoEvents()


        Me.Refresh()
        Try
            If bigPictureBox.Image Is Nothing Then
                tempstring2 = posterArray(tempint).ldUrl
                util_ImageLoad(bigPictureBox, tempstring2, "")
                'bigPictureBox.ImageLocation = tempstring2
                'bigPictureBox.Load()
            End If
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
        Try
            If bigPictureBox.Image.Width < 20 Then
                tempstring2 = posterArray(tempint).ldUrl
                util_ImageLoad(bigPictureBox, tempstring2, "")
                'bigPictureBox.ImageLocation = tempstring2
                'bigPictureBox.Load()
            End If
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
        Dim sizex As Integer = bigPictureBox.Image.Width
        Dim sizey As Integer = bigPictureBox.Image.Height
        tempstring = "Full Image Resolution :- " & sizex.ToString & " x " & sizey.ToString
        Dim resolutionlbl As New Label
        With resolutionlbl
            .Location = New Point(20, 450)
            .Width = 180
            .Text = tempstring
            .BackColor = Color.Transparent
        End With

        Me.bigPanel.Controls.Add(resolutionlbl)
        resolutionlbl.BringToFront()
        bigPictureBox.Visible = True
        bigPictureBox.Refresh()

    End Sub

    Private Sub SaveFanart(hd As Boolean)
        Try
            messbox = New frmMessageBox("", "Downloading Fanart...", "")
            messbox.Show()

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim tempstring As String = String.Empty
            Dim tempint As Integer = 0
            Dim tempstring2 As String = String.Empty
            Dim allok As Boolean = False

            'Find selected fanart, if any
            For Each button As Control In Me.Panel2.Controls

                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        tempstring = b1.Name
                        tempstring = tempstring.Replace("moviefanartcheckbox", "")
                        tempint = Convert.ToDecimal(tempstring)
                        If hd Then
                            tempstring2 = fanartArray(tempint).hdUrl
                        Else
                            tempstring2 = fanartArray(tempint).ldUrl
                        End If
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If Not allok Then
                MsgBox("No Fanart Is Selected")
            Else
                Try
                    Panel1.Controls.Remove(Label1)
                    Dim issavefanart As Boolean = Preferences.savefanart
                    Dim FanartOrExtraPath As String = mov_FanartORExtrathumbPath
                    Dim xtra As Boolean = False
                    Dim extrfanart As Boolean = False
                    If rbMovThumb1.Checked Or rbMovThumb2.Checked Or rbMovThumb3.Checked Or rbMovThumb4.Checked Or rbMovThumb5.Checked Then xtra = True
                    Preferences.savefanart = True
                    If xtra AndAlso Preferences.movxtrathumb Then extrfanart = Movie.SaveFanartImageToCacheAndPath(tempstring2, FanartOrExtraPath)

                    If xtra OrElse Movie.SaveFanartImageToCacheAndPath(tempstring2, FanartOrExtraPath) Then
                        If Not xtra Then
                            Dim paths As List(Of String) = Preferences.GetfanartPaths(workingMovieDetails.fileinfo.fullpathandfilename,If(workingMovieDetails.fileinfo.videotspath <>"",workingMovieDetails.fileinfo.videotspath,""))
                            Movie.SaveFanartImageToCacheAndPaths(tempstring2, paths)
                        End If
                        Preferences.savefanart = issavefanart
                        mov_DisplayFanart()
                        util_ImageLoad(PbMovieFanArt, workingMovieDetails.fileinfo.fanartpath, Utilities.DefaultFanartPath)
                        Dim video_flags = VidMediaFlags(workingMovieDetails.filedetails)
                        movieGraphicInfo.OverlayInfo(PbMovieFanArt, ratingtxt.Text, video_flags, workingMovie.DisplayFolderSize)

                        For Each paths In Preferences.offlinefolders
                            Dim offlinepath As String = paths & "\"
                            If workingMovieDetails.fileinfo.fanartpath.IndexOf(offlinepath) <> -1 Then
                                Dim mediapath As String
                                mediapath = Utilities.GetFileName(workingMovieDetails.fileinfo.fullpathandfilename)
                                messbox.TextBox1.Text = "Creating Offline Movie..."
                                Call mov_OfflineDvdProcess(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fullmoviebody.title, mediapath)
                            End If
                        Next
                    Else
                        util_ImageLoad(PictureBox2, Utilities.DefaultFanartPath, Utilities.DefaultFanartPath)
                        Preferences.savefanart = issavefanart
                    End If

                    lblMovFanartWidth.Text = PictureBox2.Image.Width
                    lblMovFanartHeight.Text = PictureBox2.Image.Height

                    UpdateMissingFanart()

                    XbmcLink_UpdateArtwork
                    
                Catch ex As WebException
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        Finally
            messbox.Close()
        End Try
    End Sub

    Sub UpdateMissingFanart
        oMovies.LoadMovie(workingMovieDetails.fileinfo.fullpathandfilename)

        ProgState = ProgramState.ResettingFilters
        Assign_FilterGeneral
        ProgState = ProgramState.Other

'       Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        UpdateMissingFanartNav
    End Sub

    Sub UpdateMissingFanartNav

        'Default to selecting first row if non selected
        If DataGridViewMovies.SelectedRows.Count=0 And DataGridViewMovies.Rows.Count>1 Then
            DataGridViewMovies.Rows(0).Selected=True
        End If

        UpdateMissingFanartNextBtn
        UpdateMissingFanartPrevBtn
        UpdatelblFanartMissingCount
    End Sub

    Sub UpdatelblFanartMissingCount
        Dim i As Integer = 0
        Dim x As Integer = 0

        While i<DataGridViewMovies.Rows.Count
            Dim row As Data_GridViewMovie = DataGridViewMovies.DataSource(i)

            If row.MissingFanart Then x = x + 1

            i = i + 1
        End While

        lblFanartMissingCount.Text = x & " Missing" 
    End Sub

    Sub UpdateMissingFanartNextBtn 
        btnNextMissingFanart.Enabled = False

        If DataGridViewMovies.SelectedRows.Count=0 Then Return

        Dim i As Integer = DataGridViewMovies.SelectedRows(0).Index + 1
        While i<DataGridViewMovies.Rows.Count
            Dim row As Data_GridViewMovie = DataGridViewMovies.DataSource(i)

            If row.MissingFanart Then 
                btnNextMissingFanart.Enabled = True
                btnNextMissingFanart.Tag = i  
                Return
            End If

            i = i + 1
        End While
    End Sub

    Sub UpdateMissingFanartPrevBtn
        btnPrevMissingFanart.Enabled = False

        If DataGridViewMovies.SelectedRows.Count=0 Then Return

        Dim i As Integer = DataGridViewMovies.SelectedRows(0).Index - 1
        While i>=0
            Dim row As Data_GridViewMovie = DataGridViewMovies.DataSource(i)

            If row.MissingFanart Then 
                btnPrevMissingFanart.Enabled = True
                btnPrevMissingFanart.Tag = i
                Return
            End If

            i = i - 1
        End While
    End Sub

    Sub UpdateMissingPoster
        oMovies.LoadMovie(workingMovieDetails.fileinfo.fullpathandfilename)

        ProgState = ProgramState.ResettingFilters
        Assign_FilterGeneral
        ProgState = ProgramState.Other

'       Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        UpdateMissingPosterNav             
    End Sub

    Sub UpdateMissingPosterNav

        'Default to selecting first row if non selected
        If DataGridViewMovies.SelectedRows.Count=0 And DataGridViewMovies.Rows.Count>1 Then
            DataGridViewMovies.Rows(0).Selected=True
        End If

        UpdateMissingPosterNextBtn
        UpdateMissingPosterPrevBtn
        UpdatelblPosterMissingCount
    End Sub

    Sub UpdatelblPosterMissingCount
        Dim i As Integer = 0
        Dim x As Integer = 0

        While i<DataGridViewMovies.Rows.Count
            Dim row As Data_GridViewMovie = DataGridViewMovies.DataSource(i)

            If row.MissingPoster Then x = x + 1

            i = i + 1
        End While

        lblPosterMissingCount.Text = x & " Missing" 
    End Sub

    Sub UpdateMissingPosterNextBtn 
        btnNextMissingPoster.Enabled = False

        If DataGridViewMovies.SelectedRows.Count=0 Then Return

        Dim i As Integer = DataGridViewMovies.SelectedRows(0).Index + 1
        While i<DataGridViewMovies.Rows.Count
            Dim row As Data_GridViewMovie = DataGridViewMovies.DataSource(i)

            If row.MissingPoster Then 
                btnNextMissingPoster.Enabled = True
                btnNextMissingPoster.Tag = i
                Return
            End If

            i = i + 1
        End While
    End Sub

    Sub UpdateMissingPosterPrevBtn
        btnPrevMissingPoster.Enabled = False
        If DataGridViewMovies.SelectedRows.Count=0 Then Return
        Dim i As Integer = DataGridViewMovies.SelectedRows(0).Index - 1
        While i>=0
            Dim row As Data_GridViewMovie = DataGridViewMovies.DataSource(i)
            If row.MissingPoster Then 
                btnPrevMissingPoster.Enabled = True
                btnPrevMissingPoster.Tag = i
                Return
            End If
            i = i - 1
        End While
    End Sub

    Private Function util_ImageCrop(ByVal SrcBmp As Bitmap, ByVal NewSize As Size, ByVal StartPoint As Point) As Bitmap
        If NewSize.Width < 1 Or NewSize.Height < 1 Then
            'MsgBox("Cant resize < 1")
            Return SrcBmp
            Exit Function
        End If
        Dim SrcRect As New Rectangle(StartPoint.X, StartPoint.Y, NewSize.Width, NewSize.Height)
        Dim DestRect As New Rectangle(0, 0, NewSize.Width, NewSize.Height)
        Dim DestBmp As New Bitmap(NewSize.Width, NewSize.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(DestBmp)
        g.DrawImage(SrcBmp, DestRect, SrcRect, GraphicsUnit.Pixel)
        Return DestBmp
    End Function ' Crop Image Function

    Private Sub util_ImageCropTop()
        If PictureBox2.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = PictureBox2.Image.Width
        imageheight = PictureBox2.Image.Height
        PictureBox2.Image = util_ImageCrop(PictureBox2.Image, New Size(imagewidth, imageheight - 1), New Point(0, 1)).Clone
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub util_ImageCropBottom()
        If PictureBox2.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = PictureBox2.Image.Width
        imageheight = PictureBox2.Image.Height
        PictureBox2.Image = util_ImageCrop(PictureBox2.Image, New Size(imagewidth, imageheight - 1), New Point(0, 0)).Clone
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub util_ImageCropLeft()
        If PictureBox2.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = PictureBox2.Image.Width
        imageheight = PictureBox2.Image.Height
        PictureBox2.Image = util_ImageCrop(PictureBox2.Image, New Size(imagewidth - 1, imageheight), New Point(1, 0)).Clone
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub util_ImageCropRight()
        If PictureBox2.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        thumbedItsMade = True
        imagewidth = PictureBox2.Image.Width
        imageheight = PictureBox2.Image.Height
        PictureBox2.Image = util_ImageCrop(PictureBox2.Image, New Size(imagewidth - 1, imageheight), New Point(0, 0)).Clone
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub mov_PosterInitialise()
        pageCount = 0
        currentPage = 1
        cbMoviePosterSaveLoRes.Enabled = False
        btnPosterTabs_SaveImage.Enabled = False
        For i = panelAvailableMoviePosters.Controls.Count - 1 To 0 Step -1
            panelAvailableMoviePosters.Controls.RemoveAt(i)
        Next
        If Preferences.maximumthumbs < 1 Then
        Else
            Preferences.maximumthumbs = 10
        End If
        btnPosterTabs_SaveImage.Enabled = False
        cbMoviePosterSaveLoRes.Enabled = False

        posterPicBoxes = Nothing
        posterCheckBoxes = Nothing
        resLabel = Nothing

        Application.DoEvents()

        posterArray.Clear()
    End Sub

    Private Sub mov_PosterSelectionDisplay()
        Dim names As New List(Of String)()
        messbox = New frmMessageBox("Please wait,", "", "Downloading Preview Images")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        messbox.Show()
        Me.Refresh()
        messbox.Refresh()
        Dim itemcounter As Integer = 0
        If posterArray.Count > 0 Then

            If posterArray.Count > Preferences.maximumthumbs Then
                Dim tempmaxthumbs As Integer = posterArray.Count
                Do Until tempmaxthumbs < 1
                    pageCount += 1
                    tempmaxthumbs -= Preferences.maximumthumbs
                Loop
            End If
            If posterArray.Count > 10 Then
                For f = 0 To Preferences.maximumthumbs - 1
                    names.Add(posterArray(f).ldUrl)
                Next
            Else
                For f = 0 To posterArray.Count - 1
                    names.Add(posterArray(f).ldUrl)
                Next
            End If

            'Label7.Visible = True
            If pageCount > 1 Then
                btnMovPosterNext.Visible = True
                btnMovPosterPrev.Visible = True
                If posterArray.Count >= 10 Then
                    lblMovPosterPages.Text = "Displaying 1 to 10 of " & posterArray.Count.ToString & " Images"
                Else
                    lblMovPosterPages.Text = "Displaying 1 to " & posterArray.Count.ToString & " of " & posterArray.Count.ToString & " Images"
                End If
                lblMovPosterPages.Visible = True
                Me.Refresh()
                Application.DoEvents()
                currentPage = 1
                btnMovPosterPrev.Enabled = False
                btnMovPosterNext.Enabled = True
            Else
                btnMovPosterPrev.Visible = False
                btnMovPosterNext.Visible = False
                If posterArray.Count >= 10 Then
                    lblMovPosterPages.Text = "Displaying 1 to " & 10 & " of " & posterArray.Count.ToString & " Images"
                Else
                    lblMovPosterPages.Text = "Displaying 1 to " & posterArray.Count.ToString & " of " & posterArray.Count.ToString & " Images"
                End If
                lblMovPosterPages.Visible = True
                Me.Refresh()
                Application.DoEvents()
            End If
            Dim tempboolean As Boolean = True
            Dim locationX As Integer = 0
            Dim locationY As Integer = 0

            For Each item As String In names
                Dim item2 As String = Utilities.Download2Cache(item)
                Try
                    posterPicBoxes() = New PictureBox()
                    With posterPicBoxes
                        .WaitOnLoad = True
                        .Location = New Point(locationX, locationY)
                        .Width = 123
                        .Height = 168
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .Visible = True
                        .BorderStyle = BorderStyle.Fixed3D
                        .Name = "poster" & itemcounter.ToString
                        AddHandler posterPicBoxes.DoubleClick, AddressOf util_ZoomImage2
                        AddHandler posterPicBoxes.LoadCompleted, AddressOf util_ImageRes
                    End With
                    util_ImageLoad(posterPicBoxes, item2, "")

                    posterCheckBoxes() = New RadioButton()
                    With posterCheckBoxes
                        .Location = New Point(locationX + 50, locationY + 166) '179
                        .Name = "postercheckbox" & itemcounter.ToString
                        .SendToBack()
                        .Text = " "
                        AddHandler posterCheckBoxes.CheckedChanged, AddressOf mov_PosterRadioChanged
                    End With

                    itemcounter += 1

                    Me.panelAvailableMoviePosters.Controls.Add(posterPicBoxes())
                    Me.panelAvailableMoviePosters.Controls.Add(posterCheckBoxes())
                    Me.Refresh()
                    Application.DoEvents()
                    If tempboolean = True Then
                        locationY = 192
                    Else
                        locationX += 120
                        locationY = 0
                    End If
                    tempboolean = Not tempboolean
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
            Next
        Else
            Dim mainlabel2 As Label
            mainlabel2 = New Label
            With mainlabel2
                .Location = New Point(0, 100)
                .Width = 700
                .Height = 100
                .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                .Text = "No Posters Were Found For This Movie"

            End With
            Me.panelAvailableMoviePosters.Controls.Add(mainlabel2)
        End If
        If itemcounter = 0 Then
            btnMovPosterNext.Visible = False
            btnMovPosterPrev.Visible = False
            Dim mainlabel2 As Label
            mainlabel2 = New Label
            With mainlabel2
                .Location = New Point(0, 100)
                .Width = 700
                .Height = 100
                .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                .Text = "No Posters Were Found For This Movie"
            End With
            lblMovPosterPages.Text = "0 of 0 Images"
            Me.panelAvailableMoviePosters.Controls.Add(mainlabel2)
        End If
        messbox.Close()
    End Sub
    
    Private Sub mov_PosterRadioChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim tempstring As String = sender.name
        Dim tempint As Integer = 0
        Dim tempstring2 As String = tempstring
        Dim allok As Boolean = False
        tempstring = tempstring.Replace("postercheckbox", "")
        tempint = Convert.ToDecimal(tempstring)
        For Each button As Control In Me.panelAvailableMoviePosters.Controls
            If button.Name.IndexOf("postercheckbox") <> -1 Then
                Dim b1 As RadioButton = CType(button, RadioButton)
                If b1.Checked = True Then
                    allok = True
                    Exit For
                End If
            End If
        Next
        If allok = True Then
            btnPosterTabs_SaveImage.Enabled = True
            cbMoviePosterSaveLoRes.Enabled =  (  posterArray(0).ldUrl.ToLower.IndexOf("impawards")<>-1  Or  posterArray(0).ldUrl.ToLower.IndexOf("themoviedb")<>-1  ) 
        Else
            cbMoviePosterSaveLoRes.Enabled = False
            btnPosterTabs_SaveImage.Enabled = False
        End If
    End Sub

    Private Sub util_ImageRes(ByVal sender As Object, ByVal e As EventArgs)
        resLabel = New Label
        Dim tempstring As String
        tempstring = sender.image.width.ToString
        tempstring = tempstring & " x "
        tempstring = tempstring & sender.image.height.ToString
        Dim locx As Integer = sender.location.x
        Dim locy As Integer = sender.location.y
        locy = locy + sender.height
        With resLabel
            .Location = New Point(locx + 30, locy)
            .Text = tempstring
            .BringToFront()
        End With
        Me.panelAvailableMoviePosters.Controls.Add(resLabel)
        Me.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub cmbxEpActor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbxEpActor.SelectedIndexChanged
        Try
            Dim Episode As Media_Companion.TvEpisode
            If TvTreeview.SelectedNode IsNot Nothing Then
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    Episode = TvTreeview.SelectedNode.Tag
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            pbEpActorImage.Image = Nothing
            pbEpActorImage.Visible = True
            For Each actor In Episode.ListActors
                If actor.actorname = cmbxEpActor.Text Then
                    tbEpRole.Text = actor.actorrole
                    'pbEpActorImage.ImageLocation = Utilities.DefaultActorPath

                    Dim temppath As String = Episode.ShowObj.FolderPath   'Episode.NfoFilePath.Replace(IO.Path.GetFileName(Episode.NfoFilePath), "")
                    Dim tempname As String = actor.actorname.Replace(" ", "_") & If(Preferences.FrodoEnabled, ".jpg", ".tbn")
                    temppath = temppath & ".actors\" & tempname
                    If IO.File.Exists(temppath) Then
                        util_ImageLoad(pbEpActorImage, temppath, Utilities.DefaultActorPath)
                        Exit Sub
                    End If
                    If actor.actorthumb <> Nothing Then
                        If actor.actorthumb.IndexOf("http") <> -1 Or IO.File.Exists(actor.actorthumb) Then
                            util_ImageLoad(pbEpActorImage, actor.actorthumb, Utilities.DefaultActorPath)
                        Else
                            util_ImageLoad(pbEpActorImage, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                        End If
                    Else
                        util_ImageLoad(pbEpActorImage, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                    End If
                    pbEpActorImage.SizeMode = PictureBoxSizeMode.Zoom
                    'pbEpActorImage.Load()
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub ExpandAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandAllToolStripMenuItem.Click
        Try
            Dim node As TreeNode
            For Each node In TvTreeview.Nodes
                node.ExpandAll()
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CollapseAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollapseAllToolStripMenuItem.Click
        Try
            Dim node As TreeNode
            For Each node In TvTreeview.Nodes
                node.Collapse()
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ExpandSelectedShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandSelectedShowToolStripMenuItem.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()

            WorkingTvShow.ShowNode.ExpandAll()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CollapseSelectedShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollapseSelectedShowToolStripMenuItem.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()

            WorkingTvShow.ShowNode.Collapse()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ReloadItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_ReloadFromCache.Click
        Try
            Call tv_ShowReload(True)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tv_ShowReload(Optional ByVal force As Boolean = False)
        Dim Show As Media_Companion.TvShow = tv_ShowSelectedCurrently()
        Dim Season As Media_Companion.TvSeason = tv_SeasonSelectedCurrently()
        Dim Episode As Media_Companion.TvEpisode = ep_SelectedCurrently()
    End Sub

    Private Sub TabControl3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl3.SelectedIndexChanged
        
        Try
            Dim Show As Media_Companion.TvShow = tv_ShowSelectedCurrently()
            Dim tab As String = TabControl3.SelectedTab.Text
            Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
            If (tab <> "Main Browser" And tab <> "Folders" And tab <> "TV Preferences") AndAlso Show Is Nothing Then
                MsgBox("No TV Show is selected")
                Exit Sub
            End If

            Dim tempstring As String = ""
            If tab <> "Main Browser" And tab <> "Folders" And tab <> "TV Preferences" Then
                If Show.NfoFilePath = "" And tvFolders.Count = 0 Then
                    Me.TabControl3.SelectedIndex = tvCurrentTabIndex
                    MsgBox("There are no TV Shows in your list to work on" & vbCrLf & "Set the Preferences as you want them" & vbCrLf & "Using the Preferences Tab, then" & vbCrLf & "add your TV Folders using the Folders Tab" & vbCrLf & "Once the tvshow has been scraped then" & vbCrLf & "Use the tab, ""Search for new episodes""")
                    If tab <> "TV Preferences" Then Exit Sub
                ElseIf Show.NfoFilePath = "" And tvFolders.Count > 0 And tab <> "Search for new Episodes" And tab <> "TV Preferences" Then
                    Me.TabControl3.SelectedIndex = tvCurrentTabIndex
                    If Cache.TvCache.Shows.Count > 0 Then
                        MsgBox("No TV Show is selected")
                        Exit Sub
                    Else
                        MsgBox("There are no TV Shows in your list to work on")
                        Exit Sub
                    End If
                End If
            ElseIf tab = "TV Preferences" Then
                Call tv_PreferencesSetup()
                Exit Sub
            ElseIf tab.Tolower = "folders" Then
                tvCurrentTabIndex = TabControl3.SelectedIndex
                TabControl3.SelectedIndex = tvCurrentTabIndex
                Call tv_FoldersSetup()
            Else
                tvCurrentTabIndex = 0
                Exit Sub
            End If
            If tab = "TV Show Selector" Then
                If ListBox3.Items.Count = 0 Then
                    tvCurrentTabIndex = TabControl3.SelectedIndex
                    Call tv_ShowChangedRePopulate()
                End If
            ElseIf tab = "Search for new Episodes" Then
                TabControl3.SelectedIndex = tvCurrentTabIndex
                Call ep_Search()
            ElseIf tab = "Cancel Episode Search" Then
                TabControl3.SelectedIndex = tvCurrentTabIndex
                bckgroundscanepisodes.CancelAsync()
            ElseIf tab = "Main Browser" Then
                If TvTreeview.Nodes.Count = 0 Then TvTreeview.SelectedNode = TvTreeview.TopNode
                TvTreeview.Focus()
                tvCurrentTabIndex = 0
            ElseIf tab = "Posters" Then
                tvCurrentTabIndex = TabControl3.SelectedIndex
                Call tv_PosterSetup()
            ElseIf tab = "Table View" Then
                tvCurrentTabIndex = TabControl3.SelectedIndex
                Call tv_TableView()
            ElseIf tab = "TVDB/IMDB" Then
                Dim TvdbId As Integer = 0
                If Not String.IsNullOrEmpty(Show.TvdbId.Value) AndAlso Integer.TryParse(Show.TvdbId.Value, TvdbId) Then
                    If Preferences.externalbrowser = True Then
                        Me.TabControl3.SelectedIndex = tvCurrentTabIndex
                        Dim t As New frmMessageBox("Please Select","your preferred site","","1","1")
                        t.ShowDialog()
                        If Preferences.WebSite = "tvdb" Then
                            tempstring = "http://thetvdb.com/?tab=series&id=" & TvdbId & "&lid=7"
                        Else
                            tempstring = "http://www.imdb.com/title/" & Show.ImdbId.Value & "/"
                        End If
                        OpenUrl(tempstring)
                    Else
                        tvCurrentTabIndex = TabControl3.SelectedIndex
                        Dim url As String
                        url = "http://thetvdb.com/?tab=series&id=" & TvdbId & "&lid=7"
                        Try
                            WebBrowser4.Stop()
                            WebBrowser4.ScriptErrorsSuppressed = True
                            WebBrowser4.Navigate(url)
                        Catch
                            WebBrowser4.Stop()
                            WebBrowser4.ScriptErrorsSuppressed = True
                            WebBrowser4.Navigate(url)
                        End Try
                    End If
                ElseIf String.IsNullOrEmpty(Show.TvdbId.Value) AndAlso String.IsNullOrEmpty(Show.ImdbId.Value) Then
                    TabControl3.SelectedIndex = 0
                    MsgBox("No TVDB or IMDB ID present for selected Show" & vbCrLf & "Use Tv Show Selector Tab, to select" & vbCrLf & "correct show")
                End If
            ElseIf tab.ToLower = "fanart" Then
                Call tv_Fanart_Load()
                tvCurrentTabIndex = TabControl3.SelectedIndex
            ElseIf tab.ToLower = "fanart.tv" Then
                UcFanartTvTv1.ucFanartTv_Refresh(tv_ShowSelectedCurrently())
            ElseIf tab.ToLower = "screenshot" Then
                tvCurrentTabIndex = TabControl3.SelectedIndex
                If Preferences.EdenEnabled Then
                    util_ImageLoad(PictureBox14, WorkingEpisode.VideoFilePath.Replace(IO.Path.GetExtension(WorkingEpisode.VideoFilePath), ".tbn"), Utilities.DefaultScreenShotPath)
                End If
                If Preferences.FrodoEnabled Then
                    util_ImageLoad(PictureBox14, WorkingEpisode.VideoFilePath.Replace(IO.Path.GetExtension(WorkingEpisode.VideoFilePath), "-thumb.jpg"), Utilities.DefaultScreenShotPath)
                End If
                If TextBox35.Text = "" Then
                    TextBox35.Text = Preferences.ScrShtDelay
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tv_TableView()
        Dim availableshows As New List(Of TvShow)

        messbox = New frmMessageBox("Loading all tvshow.nfo")
        messbox.Show

        For Each sh As TvShow In Cache.TvCache.Shows
            Dim shload As New TvShow 
            shload.NfoFilePath = sh.NfoFilePath 
            shload.Load()       '(False)
            availableshows.Add(shload)
        Next
        messbox.Close()
        Dim tvdbase(availableshows.Count, 8) as String
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Item(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Try
            Dim lst As List(Of TvShow) = (From x In availableshows Order By x.Title.Value).ToList
            For Each sh As TvShow In lst 
                Dim n As Integer = DataGridView2.Rows.Add()
                DataGridView2.Rows(n).Cells(0).Value = sh.Title.Value 
                DataGridView2.Rows(n).Cells(1).Value = sh.Plot.Value
                DataGridView2.Rows(n).Cells(2).Value = sh.Premiered.Value
                DataGridView2.Rows(n).Cells(3).Value = sh.Rating.Value
                DataGridView2.Rows(n).Cells(4).Value = sh.Genre.Value
                DataGridView2.Rows(n).Cells(5).Value = sh.Studio.Value
                DataGridView2.Rows(n).Cells(6).Value = sh.TvdbId.Value
                DataGridView2.Rows(n).Cells(7).Value = sh.ImdbId.Value
                DataGridView2.Rows(n).Cells(8).Value = sh.Mpaa.Value 
            Next
        Catch
        End Try
    End Sub

    Public Sub util_LanguageListLoad()
        ListBox12.Items.Add("Working...")
        ListBox12.Invalidate()
        'Me.Refresh()
        Application.DoEvents()

        System.Threading.Thread.Sleep(4000)
        Dim XmlFile As String

        XmlFile = Utilities.DownloadTextFiles("http://thetvdb.com/api/6E82FED600783400/languages.xml")
        Dim LangList As New Tvdb.Languages()
        LangList.LoadXml(XmlFile)

        For Each Lang As Tvdb.Language In LangList.Languages
            languageList.Add(Lang)
        Next

        ListBox12.Items.Clear()
        ListBox1.Items.Clear()

        For Each lan In languageList
            ListBox12.Items.Add(lan.Language.Value)
            ListBox1.Items.Add(lan.Language.Value)
        Next
    End Sub

    Private Sub tv_ShowChangedRePopulate()
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        Try
            If languageList.Count = 0 Then
                util_LanguageListLoad()
            End If
            TextBox26.Text = Utilities.GetLastFolder(WorkingTvShow.NfoFilePath)
            PictureBox9.Image = Nothing
            If workingTvShow.language <> Nothing Then
                For Each language In languageList
                    If language.Abbreviation.Value = WorkingTvShow.Language.Value Then
                        ListBox1.SelectedItem = language.Language.Value
                        Exit For
                    End If
                Next
            Else
                ListBox1.SelectedItem = Preferences.TvdbLanguage
            End If
            Label55.Text = "Default Language for TV Shows is :- " & Preferences.TvdbLanguage
            Call tv_ShowListLoad()
            Try
                If Preferences.sortorder <> Nothing Then
                    If Preferences.sortorder = "dvd" Then
                        RadioButton14.Checked = True
                    Else
                        RadioButton15.Checked = True
                    End If
                Else
                    RadioButton15.Checked = True
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                RadioButton15.Checked = True
            End Try

            Select Case Preferences.seasonall
                Case "none"
                    RadioButton18.Checked = True
                Case "poster"
                    RadioButton17.Checked = True
                Case "wide"
                    RadioButton16.Checked = True
            End Select
            '0 - Everything from TVDB
            '1 - Everything from IMDB
            '2 - TV show Nfo From IMDB, Episode nfo from TVDB
            '3 - TV show Nfo From TVDB, Episode nfo from IMDB
            If Preferences.tvdbactorscrape = 0 Then
                RadioButton13.Checked = True
                RadioButton11.Checked = True
            End If
            If Preferences.tvdbactorscrape = 1 Then
                RadioButton12.Checked = True
                RadioButton10.Checked = True
            End If
            If Preferences.tvdbactorscrape = 2 Then
                RadioButton12.Checked = True
                RadioButton11.Checked = True
            End If
            If Preferences.tvdbactorscrape = 3 Then
                RadioButton13.Checked = True
                RadioButton10.Checked = True
            End If
            If Preferences.postertype = "poster" Then
                RadioButton9.Checked = True
            Else
                RadioButton8.Checked = True
            End If

            cbTvChgShowDLFanart         .Checked    = Preferences.tvdlfanart
            cbTvChgShowDLPoster         .Checked    = Preferences.tvdlposter
            cbTvChgShowDLSeason         .Checked    = Preferences.tvdlseasonthumbs
            cbTvChgShowDLFanartTvArt    .Checked    = Preferences.TvDlFanartTvArt 
            
            If Preferences.tvshow_useXBMC_Scraper = True Then
                GroupBox2.Enabled = False
                GroupBox3.Enabled = False
                GroupBox5.Enabled = False
            Else
                GroupBox2.Enabled = True
                GroupBox3.Enabled = True
                GroupBox5.Enabled = True
            End If
            cbTvChgShowOverwriteImgs.CheckState = CheckState.Checked 'set overwrite images for changing shows.
        Catch ex As WebException
            MsgBox("There seems to be a problem with the tvdb website, please try again later")
            tvCurrentTabIndex = 0
            TabControl3.SelectedIndex = 0
        End Try
    End Sub

    Private Sub util_LanguageCheck()
        Try
            If ListBox1.SelectedIndex < 0 Then ListBox1.SelectedIndex = languageList.FindIndex(Function(index As Tvdb.Language) index.Abbreviation.Value = Preferences.TvdbLanguageCode)
            If ListBox3.SelectedIndex = -1 orElse listOfShows(ListBox3.SelectedIndex).showid = "none" Then Exit Sub
            Dim languagecode As String = languageList(ListBox1.SelectedIndex).Abbreviation.Value
            Dim url As String = "http://thetvdb.com/api/6E82FED600783400/series/" & listOfShows(ListBox3.SelectedIndex).showid & "/" & languagecode & ".xml"
            Dim websource(10000)


            Dim urllinecount As Integer = 0
            Try
                Dim wrGETURL As WebRequest
                wrGETURL = WebRequest.Create(url)
                Dim myProxy As New WebProxy("myproxy", 80)
                myProxy.BypassProxyOnLocal = True
                Dim objStream As Stream
                objStream = wrGETURL.GetResponse.GetResponseStream()
                Dim objReader As New StreamReader(objStream)
                Dim sLine As String = ""
                urllinecount = 0

                Do While Not sLine Is Nothing
                    urllinecount += 1
                    sLine = objReader.ReadLine
                    If Not sLine Is Nothing Then
                        websource(urllinecount) = sLine
                    End If
                Loop
                objReader.Close()
                objStream.Close()
                urllinecount -= 1

            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                'MsgBox(ex.ToString)
            End Try
            For f = 1 To urllinecount
                If websource(f).IndexOf("<Language>") <> -1 Then
                    websource(f) = websource(f).Replace("<Language>", "")
                    websource(f) = websource(f).Replace("</Language>", "")
                    websource(f) = websource(f).Replace("  ", "")
                    If websource(f).ToLower <> languagecode Then
                        Label55.BackColor = Color.Red
                        Label55.Text = ListBox3.SelectedItem.ToString & " is not available in " & ListBox1.SelectedItem.ToString & ", Please try another language"
                    Else
                        Label55.BackColor = Color.Transparent
                        Label55.Text = ListBox3.SelectedItem.ToString & " is available in " & ListBox1.SelectedItem.ToString
                        Label55.Font = New Font(Label55.Font, FontStyle.Bold)
                    End If
                End If
            Next
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    Private Sub RefreshShowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshShowsToolStripMenuItem.Click
        Call tv_CacheRefresh()
    End Sub

    Private Sub Tv_tsmi_CheckDuplicateEpisodes_Click(sender As Object, e As EventArgs) Handles Tv_tsmi_CheckDuplicateEpisodes.Click
        Tv_CacheCheckDuplicates()
    End Sub

    Private Sub ReloadShowCacheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadShowCacheToolStripMenuItem.Click
        Try
            If IO.File.Exists(workingProfile.tvcache) Then
                Call tv_CacheLoad()
            Else
                MsgBox("No Cache exists to load")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Function util_ReplaceXMLChrs(ByVal Text As String)                  'Convert textcodes to real characters
        If Text.IndexOf("â€˜") <> -1 Then Text = Text.Replace("â€˜", "'")
        If Text.IndexOf("â€™") <> -1 Then Text = Text.Replace("â€™", "'")
        If Text.IndexOf("â€™") <> -1 Then Text = Text.Replace("â€™", "'")
        If Text.IndexOf("â€" & Chr(147)) <> -1 Then Text = Text.Replace("â€" & Chr(147), "-")

        Return Text
    End Function

    Private Function ep_add(ByVal alleps As List(Of TvEpisode), ByVal path As String, ByVal show As String)

        tvScraperLog = tvScraperLog & "!!! Saving episode" & vbCrLf

        WorkingWithNfoFiles.ep_NfoSave(alleps, path)

        tvScraperLog &= tv_EpisodeFanartGet(alleps(0), Preferences.autoepisodescreenshot) & vbcrlf

        If Preferences.autorenameepisodes = True Then
            Dim eps As New List(Of String)
            eps.Clear()
            For Each ep In alleps
                eps.Add(ep.Episode.Value)
            Next
            Dim tempspath As String = TVShows.episodeRename(path, alleps(0).Season.Value, eps, show, alleps(0).Title.Value)

            If tempspath <> "false" Then
                path = tempspath
            End If
        End If

        Return path
    End Function

    Private Function ep_NfoValidate(ByVal nfopath As String)
        Dim validated As Boolean = True
        If IO.File.Exists(nfopath) Then
            Dim tvshow As New XmlDocument
            Try
                tvshow.Load(nfopath)
            Catch ex As Exception
                If ex.Message.ToLower.Contains("multiple root elements") Then
                    validated = chkxbmcmultinfo(nfopath)
                Else
                    validated = False
                End If
            End Try
            If validated = True Then
                Try
                    Dim tempstring As String
                    Dim filechck As IO.StreamReader = IO.File.OpenText(nfopath)
                    tempstring = filechck.ReadToEnd.ToLower
                    filechck.Close()
                    If tempstring = Nothing Then
                        validated = False
                    End If
                    Try
                        Dim seasonno As String = tempstring.Substring(tempstring.IndexOf("<season>") + 8, tempstring.IndexOf("</season>") - tempstring.IndexOf("<season>") - 8)
                        If Not IsNumeric(seasonno) Then
                            validated = False
                        End If
                    Catch ex As Exception
                        validated = False
                    End Try
                    Try
                        Dim episodeno As String = tempstring.Substring(tempstring.IndexOf("<episode>") + 9, tempstring.IndexOf("</episode>") - tempstring.IndexOf("<episode>") - 9)
                        If Not IsNumeric(episodeno) Then
                            validated = False
                        End If
                    Catch ex As Exception
                        validated = False
                    End Try
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
            End If
            Return validated
        End If
        Return False
    End Function

    Private Function chkxbmcmultinfo(ByVal xmlpath As String) As Boolean
        Try
            Dim testxml() As String = File.ReadAllLines(xmlpath)
            Dim first As Boolean = True
            Dim finalxml As String = ""
            For Each line In testxml
                If line.Contains("<episodedetails>") AndAlso first Then
                    finalxml &= "<multiepisodenfo>"
                End If
                finalxml &= line
                If line.Contains("</episodedetails>") Then first = False
            Next
            finalxml &= "</multiepisodenfo>"
            Dim Finaldoc As New XmlDocument
            Finaldoc.LoadXml(finalxml)
            Finaldoc.Save(xmlpath)
            Return ep_NfoValidate(xmlpath)
        Catch 
            Return False
        End Try
        Return False
    End Function

    Private Sub tv_NewFind(ByVal path As String, ByVal pattern As String)
        Dim episode As New List(Of TvEpisode)
        Dim propfile As Boolean = False
        Dim allok As Boolean = False
        Dim dir_info As New System.IO.DirectoryInfo(path)

        Dim fs_infos() As String = IO.Directory.GetFiles(path, "*" & pattern, SearchOption.TopDirectoryOnly) 'dir_info.GetFiles(pattern, SearchOption.TopDirectoryOnly)
        Dim counter As Integer = 1
        Dim counter2 As Integer = 1
        For Each FilePath As String In fs_infos

            Dim filename_video As String = FilePath
            Dim filename_nfo As String = filename_video.Replace(IO.Path.GetExtension(filename_video), ".nfo")
            If IO.File.Exists(filename_nfo) Then
                If ep_NfoValidate(filename_nfo) = False And Preferences.renamenfofiles = True Then
                    Dim movefilename As String = filename_nfo.Replace(IO.Path.GetExtension(filename_nfo), ".info")
                    Try
                        If File.Exists(movefilename) Then
                            Utilities.SafeDeleteFile(movefilename)
                        End If
                        IO.File.Move(filename_nfo, movefilename)
                    Catch ex As Exception
                        Utilities.SafeDeleteFile(movefilename)
                    End Try
                End If
            End If
            If Not IO.File.Exists(filename_nfo) Then
                Dim add As Boolean = True
                If pattern = ".vob" Then 'If a vob file is detected, check that it is not part of a dvd file structure
                    Dim name As String = filename_nfo
                    name = name.Replace(IO.Path.GetFileName(name), "VIDEO_TS.IFO")
                    If IO.File.Exists(name) Then
                        add = False
                    End If
                End If
                If pattern = "*.rar" Then
                    Dim tempmovie As String = String.Empty
                    Dim tempint2 As Integer = 0
                    Dim tempmovie2 As String = FilePath
                    If IO.Path.GetExtension(tempmovie2).ToLower = ".rar" Then
                        If IO.File.Exists(tempmovie2) = True Then
                            If IO.File.Exists(tempmovie) = False Then
                                Dim rarname As String = tempmovie2
                                Dim SizeOfFile As Integer = FileLen(rarname)
                                tempint2 = Convert.ToInt32(Preferences.rarsize) * 1048576
                                If SizeOfFile > tempint2 Then
                                    Dim mat As Match
                                    mat = Regex.Match(rarname, "\.part[0-9][0-9]?[0-9]?[0-9]?.rar")
                                    If mat.Success = True Then
                                        rarname = mat.Value
                                        If rarname.ToLower.IndexOf(".part1.rar") <> -1 Or rarname.ToLower.IndexOf(".part01.rar") <> -1 Or rarname.ToLower.IndexOf(".part001.rar") <> -1 Or rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                                            Dim stackrarexists As Boolean = False
                                            rarname = tempmovie.Replace(".nfo", ".rar")
                                            If rarname.ToLower.IndexOf(".part1.rar") <> -1 Then
                                                rarname = rarname.Replace(".part1.rar", ".nfo")
                                                If IO.File.Exists(rarname) Then
                                                    stackrarexists = True
                                                    tempmovie = rarname
                                                Else
                                                    stackrarexists = False
                                                    tempmovie = rarname
                                                End If
                                            End If
                                            If rarname.ToLower.IndexOf(".part01.rar") <> -1 Then
                                                rarname = rarname.Replace(".part01.rar", ".nfo")
                                                If IO.File.Exists(rarname) Then
                                                    stackrarexists = True
                                                    tempmovie = rarname
                                                Else
                                                    stackrarexists = False
                                                    tempmovie = rarname
                                                End If
                                            End If
                                            If rarname.ToLower.IndexOf(".part001.rar") <> -1 Then
                                                rarname = rarname.Replace(".part001.rar", ".nfo")
                                                If IO.File.Exists(rarname) Then
                                                    tempmovie = rarname
                                                    stackrarexists = True
                                                Else
                                                    stackrarexists = False
                                                    tempmovie = rarname
                                                End If
                                            End If
                                            If rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                                                rarname = rarname.Replace(".part0001.rar", ".nfo")
                                                If IO.File.Exists(rarname) Then
                                                    tempmovie = rarname
                                                    stackrarexists = True
                                                Else
                                                    stackrarexists = False
                                                    tempmovie = rarname
                                                End If
                                            End If
                                        Else
                                            add = False
                                        End If
                                    Else
                                        'remove = True
                                    End If
                                Else
                                    add = False
                                End If
                            End If
                        End If
                    End If
                End If
                Dim truefilename As String = Utilities.GetFileNameFromPath(filename_video)
                If truefilename.Substring(0,2)="._" Then add = False
                If add = True Then
                    Dim newep As New TvEpisode
                    newep.NfoFilePath = filename_nfo
                    newep.VideoFilePath = filename_video
                    newep.MediaExtension = IO.Path.GetExtension(filename_video)
                    newEpisodeList.Add(newep)
                End If
            End If


        Next

        fs_infos = Nothing
    End Sub

    Private Sub bckgroundscanepisodes_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bckgroundscanepisodes.RunWorkerCompleted
        Try

            If scrapeAndQuit = True Then
                'Me.Close()
                sandq = sandq -1
                Exit Sub 
            End If


            If Not (e.Error Is Nothing) Then
                tvScraperLog = tvScraperLog & vbCrLf
                tvScraperLog = tvScraperLog & "!!! Error, exiting TV episode scraper" & vbCrLf
                tvScraperLog = tvScraperLog & "Error:-" & vbCrLf
                tvScraperLog = tvScraperLog & e.Error.ToString & vbCrLf
            Else
                tvScraperLog = tvScraperLog & vbCrLf & "!!! Operation Completed" & vbCrLf
            End If


            ToolStripProgressBar5.Value = 0
            ToolStripProgressBar5.ProgressBar.Refresh()
            ToolStripProgressBar5.Visible = False
            ToolStripStatusLabel6.Text = "TV Show Scraper"
            ToolStripStatusLabel6.Visible = False
            TabPage15.Text = "Search for new Episodes"
            TabPage15.ToolTipText = "Searches folders for new episodes"

            If Preferences.disabletvlogs Then
                Dim MyFormObject As New frmoutputlog(tvScraperLog, True)
                Try
                    MyFormObject.ShowDialog()
                Catch ex As ObjectDisposedException
#If SilentErrorScream Then
                Throw ex
#End If
                End Try
            End If
            'Call populatetvtree()
            globalThreadCounter -= 1
            Call util_ThreadsRunningCheck()
            Tv_CacheSave()
            tv_CacheLoad()
            tv_Filter()
            'For Each Show As Nfo.TvShow In TvShows
            '    Show.SearchForEpisodesInFolder()
            'Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub OpenFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_OpenFolder.Click
        Try
            If Not TvTreeview.SelectedNode Is Nothing Then
                Dim Path As String = Nothing 
                Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()  'set WORKINGTVSHOW to show obj irrelavent if we have selected show/season/episode
                Dim WorkingTvEpisode As TvEpisode = ep_SelectedCurrently()
                Dim WorkingTvSeason As TvSeason = tv_SeasonSelectedCurrently()
                If Not IsNothing(WorkingTvEpisode) AndAlso Not WorkingTvEpisode.IsMissing Then
                    Path = WorkingTvEpisode.NfoFilePath
                ElseIf Not IsNothing(WorkingTvSeason) AndAlso Not IsNothing(WorkingTvSeason.FolderPath) Then
                    Path = WorkingTvSeason.FolderPath 
                ElseIf Not WorkingTvShow.NfoFilePath Is Nothing And Not WorkingTvShow.NfoFilePath = "" Then
                    Path = WorkingTvShow.NfoFilePath  'Call util_OpenFolder(WorkingTvShow.NfoFilePath) 'we send the path of the tvshow.nfo, that way in explorer it will be highlighted in the folder
                Else
                    MsgBox("There is no show selected to open")
                End If
                If Not IsNothing(Path) Then
                    Call util_OpenFolder(Path)
                End If
            Else
                MsgBox("There is no show selected to open")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub WebBrowser2_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser2.NewWindow
        Try
            e.Cancel = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub WebBrowser3_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) 
        Try
            e.Cancel = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub WebBrowser4_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser4.NewWindow
        Try
            e.Cancel = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PictureBox_Zoom(ByVal sender As Object, ByVal e As System.EventArgs) Handles tv_PictureBoxBottom.DoubleClick, tv_PictureBoxRight.DoubleClick, tv_PictureBoxLeft.DoubleClick
        Try
            Dim picBox As PictureBox = sender

            Dim imageLocation As String = picBox.tag

            If imageLocation <> Nothing Then
                If IO.File.Exists(imageLocation) Then
                    Me.ControlBox = False
                    MenuStrip1.Enabled = False
                    Call util_ZoomImage(imageLocation)
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tv_Fanart_Load()
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        Me.Panel13.Controls.Clear()
        listOfTvFanarts.Clear()
        btnTvFanartResetImage.Visible = False
        btnTvFanartSaveCropped.Visible = False
        If TvTreeview.SelectedNode.Name.ToLower.IndexOf("tvshow.nfo") <> -1 Or TvTreeview.SelectedNode.Name = "" Then
            If Not tv_PictureBoxLeft.Image Is Nothing Then
                util_ImageLoad(PictureBox10, WorkingTvShow.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)
            Else
                PictureBox10.Image = Nothing
            End If
        Else
            util_ImageLoad(PictureBox10, WorkingTvShow.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)
        End If
        Try
            Label58.Text = PictureBox10.Image.Height.ToString
            Label59.Text = PictureBox10.Image.Width.ToString
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
        TextBox28.Text = WorkingTvShow.Title.Value
        messbox = New frmMessageBox("Please wait,", "", "Querying TVDB for fanart list")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        messbox.Show()
        Me.Refresh()
        messbox.Refresh()
        Dim fanarturl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & WorkingTvShow.TvdbId.Value & "/banners.xml"
        Dim apple2(4000) As String
        Dim fanartlinecount As Integer = 0
        Try
            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(fanarturl)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            fanartlinecount = 0
            sLine = objReader.ReadToEnd
            Dim bannerslist As New XmlDocument
            'Try
            Dim bannerlist As String = "<banners>"
            bannerslist.LoadXml(sLine)
            Dim thisresult As XmlNode = Nothing
            objReader.Close()
            objStream.Close()
            For Each thisresult In bannerslist("Banners")
                Select Case thisresult.Name
                    Case "Banner"
                        Dim fanart As New str_FanartList(SetDefaults)
                        Dim bannerselection As XmlNode = Nothing
                        For Each bannerselection In thisresult.ChildNodes
                            Select Case bannerselection.Name
                                Case "BannerPath"
                                    fanart.bigUrl = "http://thetvdb.com/banners/" & bannerselection.InnerXml
                                    fanart.smallUrl = "http://thetvdb.com/banners/_cache/" & bannerselection.InnerXml
                                Case "BannerType"
                                    fanart.type = bannerselection.InnerXml
                                Case "BannerType2"
                                    fanart.resolution = bannerselection.InnerXml
                            End Select
                        Next
                        If fanart.type = "fanart" Then
                            listOfTvFanarts.Add(fanart)
                        End If
                End Select
            Next
        Catch ex As WebException
            MsgBox("TVDB appears to be down at the moment, please try again later")
        End Try

        If listOfTvFanarts.Count > 0 Then
            Dim location As Integer = 0
            Dim itemcounter As Integer = 0
            For f = 0 To listOfTvFanarts.Count - 1
                tvFanartBoxes() = New PictureBox()
                Dim item As String = Utilities.Download2Cache(listOfTvFanarts(f).smallUrl)
                
                With tvFanartBoxes
                    .Location = New Point(0, location)
                    If listOfTvFanarts.Count > 2 Then
                        .Width = 400
                        .Height = 225
                    Else
                        .Width = 415
                        .Height = 250
                    End If
                    .SizeMode = PictureBoxSizeMode.Zoom
                    '.ImageLocation = listOfTvFanarts(f).smallUrl
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "tvfanart" & f.ToString
                    AddHandler tvFanartBoxes.DoubleClick, AddressOf util_ZoomImage2
                End With
                util_ImageLoad(tvFanartBoxes, item, "")

                tvFanartCheckBoxes() = New RadioButton()
                With tvFanartCheckBoxes
                    .BringToFront()
                    .Location = New Point(199, location + 225)
                    .Name = "checkbox" & itemcounter.ToString
                End With

                resolutionLabels() = New Label
                With resolutionLabels
                    .BringToFront()
                    .Location = New Point(10, location + 225)
                    .Name = listOfTvFanarts(f).resolution
                    .Text = listOfTvFanarts(f).resolution
                End With
                itemcounter += 1
                location += 250
                Me.Panel13.Controls.Add(tvFanartBoxes())
                Me.Panel13.Controls.Add(tvFanartCheckBoxes())
                Me.Panel13.Controls.Add(resolutionLabels())
                Me.Refresh()
                Application.DoEvents()
            Next
            EnableTvFanartScrolling
        Else
            Dim mainlabel2 As Label
            mainlabel2 = New Label
            With mainlabel2
                .Location = New Point(0, 100)
                .Width = 700
                .Height = 100
                .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                .Text = "No Fanart Was Found At TVDB For This Movie"
            End With
            Me.Panel13.Controls.Add(mainlabel2)
        End If
        System.Windows.Forms.Cursor.Current = Cursors.Default
        messbox.Close()
    End Sub

        'Set focus on the first checkbox to enable mouse wheel scrolling 
    Sub EnableTvFanartScrolling
        Try
            Dim rb As RadioButton = Panel13.Controls("checkbox0")
                
            rb.Select                       'Causes RadioButtons checked state to toggle
            rb.Checked = Not rb.Checked     'Undo unwanted checked state toggling
        Catch
        End Try
    End Sub

    Sub EnableTvBannerScrolling
        Try
            Panel16.Focus()
            'Dim rb As RadioButton = Panel16.Controls("postercheckbox0")
            'rb.Select
            'rb.Checked = Not rb.Checked
        Catch
        End Try
    End Sub

    Private Sub Tv_FanartDisplay()
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        If IsNothing(WorkingTvShow) Then Exit Sub
        If TvTreeview.SelectedNode.Name.ToLower.IndexOf("tvshow.nfo") <> -1 Or TvTreeview.SelectedNode.Name = "" Then
            If Not tv_PictureBoxLeft.Image Is Nothing Then
                util_ImageLoad(PictureBox10, WorkingTvShow.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)
            Else
                PictureBox10.Image = Nothing
            End If
        Else
            util_ImageLoad(PictureBox10, WorkingTvShow.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)
        End If
    End Sub
    
    Private Sub tv_FanartCropTop()
        Dim imagewidth As Integer = PictureBox10.Image.Width
        Dim imageheight As Integer = PictureBox10.Image.Height
        PictureBox10.Image = util_ImageCrop(PictureBox10.Image, New Size(imagewidth, imageheight - 1), New Point(0, 1)).Clone
        PictureBox10.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub tv_FanartCropBottom()
        Dim imagewidth As Integer = PictureBox10.Image.Width
        Dim imageheight As Integer = PictureBox10.Image.Height
        PictureBox10.Image = util_ImageCrop(PictureBox10.Image, New Size(imagewidth, imageheight - 1), New Point(0, 0)).Clone
        PictureBox10.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub tv_FanartCropLeft()
        Dim imagewidth As Integer = PictureBox10.Image.Width
        Dim imageheight As Integer = PictureBox10.Image.Height
        PictureBox10.Image = util_ImageCrop(PictureBox10.Image, New Size(imagewidth - 1, imageheight), New Point(1, 0)).Clone
        PictureBox10.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub tv_FanartCropRight()
        Dim imagewidth As Integer = PictureBox10.Image.Width
        Dim imageheight As Integer = PictureBox10.Image.Height
        PictureBox10.Image = util_ImageCrop(PictureBox10.Image, New Size(imagewidth - 1, imageheight), New Point(0, 0)).Clone
        PictureBox10.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Sub tv_Rescrape() 'Panel9 visibility indicates which is selected - a tvshow or an episode
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()

        If IsNothing(WorkingTvShow.TvdbId.Value) = True Then
            WorkingTvShow.TvdbId.Value = ""
        End If
        If WorkingTvShow.TvdbId.Value.IndexOf("tt").Equals(0) Then tv_IMDbID_detected = True
        If Panel9.Visible = False Then 'i.e. rescrape selected TVSHOW else rescrape selected EPISODE
            'its a tv show
            Dim selectednode As Integer = TvTreeview.SelectedNode.Index 
            tv_Rescrape_Show(WorkingTvShow)
            TvTreeview.SelectedNode = TvTreeview.Nodes(selectednode)
        Else
            'its an episode
            tv_Rescrape_Episode(WorkingTvShow, WorkingEpisode)
        End If

        If Not tv_IMDbID_warned And tv_IMDbID_detected Then
            MessageBox.Show(tv_IMDbID_detectedMsg, "TV Show ID", MessageBoxButtons.OK, MessageBoxIcon.Information)
            tv_IMDbID_warned = True
        End If

    End Sub

    Private Sub Tv_TreeViewContext_RenameEp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_RenameEp.Click

        Try
            Dim renamelog As String = ""
            Dim tempint As Integer = 0

            Dim oldname As String = ""

            Dim nfofilestorename As New List(Of String)
            nfofilestorename.Clear()
            Dim donelist As New List(Of String)
            donelist.Clear()
            If TvTreeview.SelectedNode.Name.IndexOf("\missing\") = -1 Then
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    'individual episode
                    tempint = MessageBox.Show("Using this option will rename the selected episode" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If tempint = DialogResult.No Then
                        Exit Sub
                    End If
                    If Not nfofilestorename.Contains(TvTreeview.SelectedNode.Name) And TvTreeview.SelectedNode.Name.IndexOf("Missing: ") <> 0 Then
                        nfofilestorename.Add(TvTreeview.SelectedNode.Name)
                    End If
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                    'season
                    tempint = MessageBox.Show("Using this option will rename all episode nfo's within the selected season" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If tempint = DialogResult.No Then
                        Exit Sub
                    End If
                    Dim childnode As TreeNode
                    For Each childnode In TvTreeview.SelectedNode.Nodes
                        If Not nfofilestorename.Contains(childnode.Name) And childnode.Name.IndexOf("\missing\") = -1 Then
                            nfofilestorename.Add(childnode.Name)
                        End If
                    Next
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
                    'full show
                    tempint = MessageBox.Show("Using this option will rename all episode nfo's within the selected show" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If tempint = DialogResult.No Then
                        Exit Sub
                    End If
                    Dim childnode As TreeNode
                    Dim childchildnode As TreeNode
                    For Each childnode In TvTreeview.SelectedNode.Nodes
                        For Each childchildnode In childnode.Nodes
                            If Not nfofilestorename.Contains(childchildnode.Name) And childchildnode.Name.IndexOf("\missing\") = -1 Then
                                nfofilestorename.Add(childchildnode.Name)
                            End If
                        Next
                    Next
                End If
            Else
                MsgBox("This is a Missing Episode, and can not be renamed!")
                Exit Sub
            End If

            Dim messbox As New frmMessageBox("Renaming episodes,", "", "   Please Wait")
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()
            If nfofilestorename.Count <= 0 Then
                messbox.Close()
                Exit Sub
            End If
            renamelog += "!!! " & nfofilestorename.Count & " nfo's to rename..." & vbCrLf & vbCrLf
            For Each renamefile In nfofilestorename
                Dim seasonno As String = ""
                Dim episodetitle As String = ""
                Dim showtitle As String = ""
                Dim episodeno As New List(Of String)
                episodeno.Clear()
                For Each tvshow In Cache.TvCache.Shows
                    Dim showpath As String = tvshow.NfoFilePath.Replace(IO.Path.GetFileName(tvshow.NfoFilePath), "")
                    If renamefile.IndexOf(showpath) <> -1 Then
                        showtitle = Preferences.RemoveIgnoredArticles(tvshow.Title.Value)
                        For Each episode In tvshow.Episodes
                            If episode.NfoFilePath = renamefile Then
                                If seasonno = "" Then
                                    seasonno = episode.Season.Value
                                End If
                                If episodetitle = "" Then
                                    episodetitle = episode.Title.Value
                                End If
                                episodeno.Add(episode.Episode.Value)
                            End If
                        Next
                        Dim newfilename As String
                        newfilename = ""
                        If seasonno.Length = 1 Then
                            seasonno = "0" & seasonno
                        End If
                        For f = 0 To episodeno.Count - 1
                            If episodeno(f).Length = 1 Then
                                episodeno(f) = "0" & episodeno(f)
                            End If
                        Next
                        newfilename = Renamer.setTVFilename(showtitle, episodetitle, episodeno, seasonno)

                        newfilename = newfilename.Replace("?", "")
                        newfilename = newfilename.Replace("/", "")
                        newfilename = newfilename.Replace("\", "")
                        newfilename = newfilename.Replace("<", "")
                        newfilename = newfilename.Replace(">", "")
                        newfilename = newfilename.Replace(":", "")
                        newfilename = newfilename.Replace("""", "")
                        newfilename = newfilename.Replace("*", "")
                        Dim listtorename As New List(Of String)
                        listtorename.Clear()
                        listtorename.Add(renamefile)
                        For Each ext In Utilities.VideoExtensions
                            If ext = "VIDEO_TS.IFO" Then Continue For
                            Dim temppath2 As String = renamefile.Replace(IO.Path.GetExtension(renamefile), ext)
                            If IO.File.Exists(temppath2) Then
                                listtorename.Add(temppath2)
                            End If
                        Next
                        Dim di As DirectoryInfo = New DirectoryInfo(renamefile.Replace(IO.Path.GetFileName(renamefile), ""))
                        Dim filenama As String = IO.Path.GetFileNameWithoutExtension(renamefile)
                        Dim fils As IO.FileInfo() = di.GetFiles(filenama & ".*")
                        For Each fiNext In fils
                            If Not listtorename.Contains(fiNext.FullName) Then
                                listtorename.Add(fiNext.FullName)
                            End If
                        Next

                        Dim temppath As String = renamefile
                        temppath = temppath.Replace(IO.Path.GetExtension(temppath), ".tbn")
                        If IO.File.Exists(temppath) Then
                            If Not listtorename.Contains(temppath) Then
                                listtorename.Add(temppath)
                            End If
                        End If

                        temppath = temppath.Replace(IO.Path.GetExtension(temppath), ".rar")
                        If IO.File.Exists(temppath) Then
                            If Not listtorename.Contains(temppath) Then
                                listtorename.Add(temppath)
                            End If
                        End If

                        temppath = temppath.Replace(IO.Path.GetExtension(temppath), "-thumb.jpg")
                        If IO.File.Exists(temppath) Then
                            If Not listtorename.Contains(temppath) Then
                                listtorename.Add(temppath)
                            End If
                        End If

                        Dim oldnfofile As String = ""
                        Dim newnfofile As String = ""
                        For Each items In listtorename
                            If IO.Path.GetExtension(items).ToLower = ".nfo" And oldnfofile = "" Then
                                oldnfofile = items
                                newnfofile = items.Replace(IO.Path.GetFileName(items), newfilename) & IO.Path.GetExtension(items)
                                'newnfofile = newnfofile.Replace("..", ".")
                            End If
                            Dim newname As String = items.Replace(filenama, newfilename)
                            'newname = newname.Replace("..", ".")
                            Try
                                Dim pathsep As String = If(items.Contains("/"), "/", "\")
                                Dim origpath As String = items.Substring(0, items.LastIndexOf(pathsep)+1)  ', items.Length-(items.LastIndexOf(pathsep)+1))
                                renamelog += "!!! " & items.Replace(origpath, "") & "  -- to --  " & newname.Replace(origpath, "")
                                Dim fi As New IO.FileInfo(items)
                                If Not IO.File.Exists(newname) Then
                                    fi.MoveTo(newname)
                                    If items.ToLower = IO.Path.Combine(tb_EpPath.Text, tb_EpFilename.Text).ToLower Then
                                        tb_EpFilename.Text = IO.Path.GetFileName(fi.FullName)
                                    End If
                                    renamelog += "  ---Succeeded" & vbCrLf
                                Else
                                    renamelog += " --! Not Renamed - Same" & vbCrLf
                                End If

                            Catch ex As Exception
                                renamelog += "!!! *** Not Succeeded - Please rename all files manually!" & vbCrLf & "!!! Reported Message: " & ex.Message.ToString & vbCrLf
                            End Try
                            
                        Next
                        renamelog += "!!! " & vbCrLf
                        renamelog += "!!! Updating Tables" & vbCrLf
                        Try
                            For Each noder2 In TvTreeview.Nodes
                                If noder2.name = oldnfofile Then
                                    noder2.name = newnfofile
                                End If
                                For Each noder3 In noder2.nodes
                                    If noder3.name = oldnfofile Then
                                        noder3.name = newnfofile
                                    End If
                                    For Each noder4 In noder3.nodes
                                        If noder4.name = oldnfofile Then
                                            noder4.name = newnfofile
                                        End If
                                    Next
                                Next
                            Next
                            For Each episode In tvshow.Episodes
                                If episode.NfoFilePath = oldnfofile Then
                                    episode.NfoFilePath = newnfofile
                                End If
                            Next
                            renamelog += "!!! Tables Updated" & vbCrLf & vbCrLf
                        Catch
                            renamelog += "!!! Failed to update tables, use 'Refresh TV Shows' menu item to fix" & vbCrLf & vbCrLf
                        End Try
                    End If
                Next
            Next
            Call Tv_CacheSave()
            messbox.Close()
            If Preferences.disabletvlogs Then
                Dim MyFormObject As New frmoutputlog(renamelog, True)
                Try
                    MyFormObject.ShowDialog()
                Catch ex As ObjectDisposedException

                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Tv_TreeViewContext_RescrapeWizard_Click(sender As System.Object, e As System.EventArgs) Handles Tv_TreeViewContext_RescrapeWizard.Click
        Try
            singleshow = True
            TV_BatchRescrapeWizardToolStripMenuItem.PerformClick()
            While tvbckrescrapewizard.IsBusy
                Application.DoEvents()
            End While

            singleshow = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Tv_TreeViewContext_RescrapeMediaTags_Click(sender As System.Object, e As System.EventArgs) Handles Tv_TreeViewContext_RescrapeMediaTags.Click
        Try
            Dim tmp As Integer = Utilities.languagelibrary.count
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim tempint As Integer = 0
            Dim nfofilestorename As New List(Of String)
            nfofilestorename.Clear()
            Dim donelist As New List(Of String)
            donelist.Clear()
            If TvTreeview.SelectedNode.Name.IndexOf("\missing\") = -1 Then
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    'individual episode
                    'tempint = MessageBox.Show("This option will Rescrape Media tags for the selected episode" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    'If tempint = DialogResult.No Then
                    '    Exit Sub
                    'End If
                    If Not nfofilestorename.Contains(TvTreeview.SelectedNode.Name) And TvTreeview.SelectedNode.Name.IndexOf("\missing\") = -1 Then
                        nfofilestorename.Add(TvTreeview.SelectedNode.Name)
                    End If
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                    'season
                    tempint = MessageBox.Show("This option will Rescrape Media tags for the selected season" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If tempint = DialogResult.No Then
                        Exit Sub
                    End If
                    Dim childnode As TreeNode
                    For Each childnode In TvTreeview.SelectedNode.Nodes
                        If Not nfofilestorename.Contains(childnode.Name) And childnode.Name.IndexOf("\missing\") = -1 Then
                            nfofilestorename.Add(childnode.Name)
                        End If
                    Next
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
                    'full show
                    tempint = MessageBox.Show("This option will Rescrape Media tags for the selected show" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If tempint = DialogResult.No Then
                        Exit Sub
                    End If
                    Dim childnode As TreeNode
                    Dim childchildnode As TreeNode
                    For Each childnode In TvTreeview.SelectedNode.Nodes
                        For Each childchildnode In childnode.Nodes
                            If Not nfofilestorename.Contains(childchildnode.Name) And childchildnode.Name.IndexOf("\missing\") = -1 Then
                                nfofilestorename.Add(childchildnode.Name)
                            End If
                        Next
                    Next
                End If
            End If

            Dim messbox As New frmMessageBox("Getting Media Tags for episodes,", "", "   Please Wait")
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()
            If nfofilestorename.Count <= 0 Then
                messbox.Close()
                Exit Sub
            End If

            For Each nfo In nfofilestorename
                Dim ThisEp As New List(Of TvEpisode)
                ThisEp.Clear()
                ThisEp = WorkingWithNfoFiles.ep_NfoLoad(nfo)  'nfoFunction.ep_NfoLoadGeneric(nfo)
                For h = ThisEp.Count - 1 To 0 Step -1

                    Dim fileStreamDetails As FullFileDetails = Preferences.Get_HdTags(Utilities.GetFileName(ThisEp(h).VideoFilePath))
                    ThisEp(h).Details.StreamDetails.Video = fileStreamDetails.filedetails_video

                    ThisEp(h).Details.StreamDetails.Audio.Clear()
                    For Each audioStream In fileStreamDetails.filedetails_audio
                        ThisEp(h).Details.StreamDetails.Audio.Add(audioStream)
                    Next

                    If ThisEp(h).Details.StreamDetails.Video.DurationInSeconds.Value <> Nothing Then
                        Try
                            Dim tempstring As String
                            tempstring = ThisEp(h).Details.StreamDetails.Video.DurationInSeconds.Value
                            If Preferences.intruntime Then
                                ThisEp(h).Runtime.Value = Math.Round(tempstring / 60).ToString
                            Else
                                ThisEp(h).Runtime.Value = Math.Round(tempstring / 60).ToString & " min"
                            End If

                        Catch ex As Exception
#If SilentErrorScream Then
                                            Throw ex
#End If
                        End Try
                        'nfoFunction.saveepisodenfo(ThisEp, ThisEp(0).NfoFilePath)
                        WorkingWithNfoFiles.ep_NfoSave(ThisEp, ThisEp(0).NfoFilePath)
                    End If
                Next
            Next
            messbox.Close()
            tv_CacheRefresh(WorkingTvShow)
        Catch ex As Exception
            messbox.Close()
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Tv_TreeViewContext_MissingEpThumbs_Click(sender As System.Object, e As System.EventArgs) Handles Tv_TreeViewContext_MissingEpThumbs.Click
        Try
            Dim tmp As Integer = Utilities.languagelibrary.count
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim tempint As Integer = 0
            Dim seasonnumber As Integer = -1
            Dim nfofilestorename As New List(Of String)
            nfofilestorename.Clear()
            Dim donelist As New List(Of String)
            donelist.Clear()
            If WorkingTvShow.Episodes.Count = 0 Then
                MsgBox("No Episodes in this Show")
                Exit Sub
            End If
            If TvTreeview.SelectedNode.Name.IndexOf("\missing\") = -1 Then
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                    Dim Thisseason As TvSeason = TvTreeview.SelectedNode.Tag
                    seasonnumber = Thisseason.SeasonNumber
                End If
            End If
            Dim messbox As New frmMessageBox("Scanning for Missing episode thumbnails,", "and downloading if available.", "   Please Wait")
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()

            Dim tvdbstuff As New TVDBScraper
            Dim tvseriesdata As New Tvdb.ShowData 
            Dim language As String = WorkingTvShow.Language.Value
            If language = "" Then language = "en"
            tvseriesdata = tvdbstuff.GetShow(WorkingTvShow.TvdbId.Value, language, SeriesXmlPath)
            
            For Each ep As TvEpisode In WorkingTvShow.Episodes
                If ep.IsMissing Then Continue For
                If Not seasonnumber = -1 Then
                    If ep.Season.Value <> seasonnumber.ToString Then Continue For
                End If
                Dim Episodedata As New Tvdb.Episode
                Dim epfound As Boolean = False
                If Not tvseriesdata.FailedLoad Then
                    For Each NewEpisode As Tvdb.Episode In tvseriesdata.Episodes
                        If Not String.IsNullOrEmpty(ep.UniqueId.Value) Then
                            If NewEpisode.Id.Value = ep.UniqueId.Value
                                epfound = True
                            End If
                        ElseIf NewEpisode.SeasonNumber.Value = ep.Season.Value
                            If NewEpisode.EpisodeNumber.Value = ep.Episode.Value
                                epfound = True
                            End If
                        End If
                        If epfound Then
                            Episodedata = NewEpisode
                            Episodedata.ThumbNail.Value = "http://www.thetvdb.com/banners/" & NewEpisode.ThumbNail.value
                            Exit For
                        End If
                    Next
                End If
                If Not epfound Then
                    Dim sortorder As String = WorkingTvShow.SortOrder.Value
                    If sortorder = "" Then sortorder = "default"
                    Dim tvdbid As String = WorkingTvShow.TvdbId.Value
                    Dim imdbid As String = WorkingTvShow.ImdbId.Value
                    Dim seasonno As String = ep.Season.Value
                    Dim episodeno As String = ep.Episode.Value
                    Episodedata = tvdbstuff.getepisodefromxml(tvdbid, sortorder, seasonno, episodeno, language, True)
                    If Episodedata.FailedLoad Then
                        Continue For
                    End If
                End If
                Dim epdata As New TvEpisode 
                epdata.AbsorbTvdbEpisode(Episodedata)
                epdata.NfoFilePath = ep.NfoFilePath
                epdata.VideoFilePath = ep.VideoFilePath 
                epdata.Thumbnail.Url = Episodedata.ThumbNail.Value 
                tv_EpisodeFanartGet(epdata, False)
            Next
            messbox.Close()
        Catch ex As Exception
            messbox.Close()
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub tv_PosterSetup(Optional ByVal IsOfType As String = "")

        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        Dim WorkingSeason As TvSeason = tv_SeasonSelectedCurrently()
        'If workingTvShow.tvdbid = currentposterid Then
        '    Exit Sub
        'End If
        tvposterpage = 0
        imdbposterlist.Clear()
        tvdbposterlist.Clear()
        rbTVposter.Checked = True
        rbTVposter.Enabled = False
        rbTVbanner.Enabled = False
        ComboBox2.Items.Clear()
        tvobjects.Clear()
        TextBox31.Text = WorkingTvShow.Title.Value
        Label72.Text = ""
        For i = Panel16.Controls.Count - 1 To 0 Step -1
            Panel16.Controls.RemoveAt(i)
        Next

        ComboBox2.Items.Add("Main Image")
        ComboBox2.Items.Add("Season All")
        For Each tvshow In Cache.TvCache.Shows
            If tvshow.TvdbId = WorkingTvShow.TvdbId Then
                currentposterid = tvshow.TvdbId.Value
                For Each Season As Media_Companion.TvSeason In tvshow.Seasons.Values
                    For Each ep As Media_Companion.TvEpisode In Season.Episodes
                        Dim seasonstring As String = ""

                        If ep.Season.Value < 1 Then
                            seasonstring = "Specials"
                        Else
                            seasonstring = "Season " & Utilities.PadNumber(ep.Season.Value.ToString, 2)
                        End If

                        If Not ComboBox2.Items.Contains(seasonstring) Then
                            ComboBox2.Items.Add(seasonstring)
                        End If
                    Next
                Next
                Exit For
            End If
        Next

        ComboBox2.SelectedIndex = 0
        
        
        If Not WorkingSeason is Nothing then
            Dim ThisSeason As String = WorkingSeason.ToString 
            If ThisSeason = "Season 00" then ThisSeason = "Specials"
            For i=0 to ComboBox2.Items.Count
                ComboBox2.SelectedIndex = i
                If ComboBox2.text = ThisSeason Then
                    If IsOfType = "banner" Then rbTVbanner.Checked = True
                    Exit For
                End If
            Next

        End If
        '        For Each item In tvobjects
        '            ComboBox2.Items.Add(item)
        '            If item = combostart Then
        '                Try
        '                    ComboBox2.SelectedIndex = ComboBox2.Items.Count - 1
        '                Catch ex As Exception
        '#If SilentErrorScream Then
        '                    Throw ex
        '#End If
        '                End Try
        '            End If
        '        Next

    End Sub

    Public Function BannerAndPosterViewer()
        Try
            Me.Panel16.Hide()
            Label72.Text = ""
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            rbTVposter.Enabled = True
            rbTVbanner.Enabled = True
            btnTvPosterTVDBSpecific.Enabled = True
            Dim eden As Boolean = Preferences.EdenEnabled
            Dim frodo As Boolean = Preferences.FrodoEnabled
            Dim edenpath As String =""
            Dim frodopath As string =""
            Dim tempstring As String = ComboBox2.SelectedItem
            Dim PresentImage As String = ""
            Dim defimg As String = ""
            Dim path As String = ""
            EdenImageTrue.Visible = False
            FrodoImageTrue.Visible=False
            If tempstring = "Main Image" Then
                If eden Then
                    path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "folder.jpg")
                    edenpath = path
                End If
                If frodo Then
                    If rbTVbanner.Checked = True Then
                        path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "banner.jpg")
                        frodopath = path
                    ElseIf rbTVposter.Checked = True Then
                        path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "poster.jpg")
                        frodopath = path
                    End If
                End If

            ElseIf tempstring = "Specials" Then
                If eden Then
                    path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season-specials.tbn")
                    edenpath = path
                End If
                If frodo Then
                    If rbTVbanner.Checked = True Then
                        path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season-specials-banner.jpg")
                        frodopath = path
                    ElseIf rbTVposter.Checked = True Then
                        path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season-specials-poster.jpg")
                        frodopath = path
                    End If
                End If
            ElseIf tempstring.IndexOf("Season") = 0 And tempstring.IndexOf("Season All") = -1 Then
                path = tempstring.Replace("Season ", "")
                path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season" & path & ".tbn")
                edenpath = path
                If frodo Then
                    If rbTVbanner.Checked = True Then
                        path = path.Replace(".tbn", "-banner.jpg")
                        frodopath = path
                    ElseIf rbTVposter.Checked = True Then
                        path = path.Replace(".tbn", "-poster.jpg")
                        frodopath = path
                    End If
                End If


            ElseIf tempstring = "Season All" Then
                btnTvPosterTVDBSpecific.Enabled = False
                If eden Then
                    path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season-all.tbn")
                    edenpath = path
                End If
                If frodo Then
                    If rbTVbanner.Checked = True Then
                        path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season-all-banner.jpg")
                        frodopath = path
                    ElseIf rbTVposter.Checked = True Then
                        path = WorkingTvShow.NfoFilePath.Replace("tvshow.nfo", "season-all-poster.jpg")
                        frodopath = path
                    End If
                End If
            End If

            If (eden and IO.File.Exists(edenpath)) or (frodo and IO.File.Exists(frodopath)) Then
                EdenImageTrue.Visible =False
                EdenImageTrue.Text = "Eden Image Present"
                FrodoImageTrue.Visible=False
                FrodoImageTrue.Text = "Frodo Image Present"
                ArtMode.Text=""
                If eden Then
                    EdenImageTrue.Visible = True
                    ArtMode.Text="Pre-Frodo Enabled
                End If
                If frodo Then
                    FrodoImageTrue.Visible = True
                    ArtMode.Text="Frodo Enabled"
                End If
                If frodo and eden then
                    ArtMode.text="Both Enabled"
                    EdenImageTrue.Visible=True
                    FrodoImageTrue.Visible=True
                End If
                If IO.File.Exists(edenpath) then
                    PresentImage = edenpath 
                    EdenImageTrue.Text = "Eden Image Present"
                Else
                    EdenImageTrue.Text = "No Eden Image
                End If
                If IO.File.Exists(frodopath) then
                    PresentImage = frodopath 
                    FrodoImageTrue.Text="Frodo Image Present"
                Else
                    FrodoImageTrue.Text = "No Frodo Image"
                End If
            Else
                If rbTVbanner.Checked = True Then
                    defimg = Utilities.DefaultBannerPath
                    If eden and not frodo then
                        EdenImageTrue.Text="No Eden Image"
                        EdenImageTrue.Visible=True
                        FrodoImageTrue.Visible=False
                    ElseIf frodo and Not eden then
                        FrodoImageTrue.Text="No Frodo Image"
                        FrodoImageTrue.Visible=True
                        EdenImageTrue.Visible=False
                    ElseIf frodo and eden then
                        EdenImageTrue.Text="No Eden Image"
                        EdenImageTrue.Visible=True
                        FrodoImageTrue.Text="No Frodo Image"
                        FrodoImageTrue.Visible=True
                    End If
                Else
                    defimg = Utilities.DefaultPosterPath
                    If eden and not frodo then
                        EdenImageTrue.Text="No Eden Image"
                        EdenImageTrue.Visible=True
                        FrodoImageTrue.Visible=False
                    ElseIf frodo and Not eden then
                        FrodoImageTrue.Text="No Frodo Image"
                        FrodoImageTrue.Visible=True
                        EdenImageTrue.Visible=False
                    ElseIf frodo and eden then
                        EdenImageTrue.Text="No Eden Image"
                        EdenImageTrue.Visible=True
                        FrodoImageTrue.Text="No Frodo Image"
                        FrodoImageTrue.Visible=True
                    End If
                End If
            End If
            util_ImageLoad(PictureBox12, PresentImage, If(rbTVbanner.Checked, Utilities.DefaultTvBannerPath, Utilities.DefaultTvPosterPath))
            If rbTVbanner.Checked = True Then
                Label73.Text = "Current Banner - " & PictureBox12.Image.Width.ToString & " x " & PictureBox12.Image.Height.ToString
            Else
                Label73.Text = "Current Poster - " & PictureBox12.Image.Width.ToString & " x " & PictureBox12.Image.Height.ToString
            End If

            Return workingposterpath
        Catch ex As Exception
            Return 0
            ExceptionHandler.LogError(ex)
        End Try
    End Function

    Private Sub tv_TvdbThumbsGet()

        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        Dim showlist As New XmlDocument
        'Dim tvdbstuff As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
        Dim tvdbstuff As New TVDBScraper
        Dim thumblist As String = tvdbstuff.GetPosterList(WorkingTvShow.TvdbId.Value)
        Try
            showlist.LoadXml(thumblist)
        Catch ex As Exception
            MsgBox(thumblist, MsgBoxStyle.OkOnly, "TVdb site returned.....")
            'thumblist = "<error>ERROR</error>"
            'showlist.LoadXml(thumblist)
            Exit Sub
        End Try

        'CheckBox3 = seasons
        'CheckBox4 = fanart
        'CheckBox5 = poster
        For Each thisresult In showlist("banners")
            Select Case thisresult.Name
                Case "banner"
                    Dim individualposter As New TvBanners
                    For Each results In thisresult.ChildNodes
                        Select Case results.Name
                            Case "url"
                                individualposter.Url = results.InnerText
                            Case "bannertype"
                                individualposter.BannerType = results.InnerText
                            Case "resolution"
                                individualposter.Resolution = results.InnerText
                            Case "language"
                                individualposter.Language = results.InnerText
                            Case "season"
                                individualposter.Season = results.InnerText

                        End Select
                    Next
                    individualposter.SmallUrl = individualposter.Url.Replace("http://thetvdb.com/banners/", "http://thetvdb.com/banners/_cache/")
                    tvdbposterlist.Add(individualposter)
            End Select
        Next
    End Sub

    Private Sub tv_PosterPanelPopulate()
        Me.Panel16.Show()
        tvposterpage = 1
        If usedlist.Count <= 0 Then
            Label72.Text = "Displaying 0 of 0 Images"
            Label72.Visible = True
            btnTvPosterNext.Visible = False
            btnTvPosterPrev.Visible = False
            Call tv_PosterPanelClear()
            Exit Sub
        End If
        If Not rbTVbanner.Checked Then
            If usedlist.Count > Preferences.maximumthumbs Then
                btnTvPosterNext.Visible = True
                btnTvPosterPrev.Visible = True
                If usedlist.Count >= Preferences.maximumthumbs Then
                    Label72.Text = "Displaying 1 to 10 of " & usedlist.Count.ToString & " Images"
                Else
                    Label72.Text = "Displaying 1 to " & usedlist.Count.ToString & " of " & usedlist.Count.ToString & " Images"
                End If
                Label72.Visible = True
                Me.Refresh()
                Application.DoEvents()
                btnTvPosterPrev.Enabled = False
                btnTvPosterNext.Enabled = True
            Else
                btnTvPosterNext.Visible = False
                btnTvPosterPrev.Visible = False
                If posterArray.Count >= Preferences.maximumthumbs Then
                    Label72.Text = "Displaying 1 to " & Preferences.maximumthumbs & " of " & usedlist.Count.ToString & " Images"
                Else
                    Label72.Text = "Displaying 1 to " & usedlist.Count.ToString & " of " & usedlist.Count.ToString & " Images"
                End If
                Label72.Visible = True
                Me.Refresh()
                Application.DoEvents()
            End If
        Else
            btnTvPosterNext.Visible = False
            btnTvPosterPrev.Visible = False
            Label72.Text = "Displaying 1 to " & usedlist.Count.ToString
        End If
        Call tv_PosterSelectionDisplay()
    End Sub

    Private Sub tv_PosterPanelClear()
        For i = Panel16.Controls.Count - 1 To 0 Step -1
            Panel16.Controls.RemoveAt(i)
        Next
    End Sub

    Private Sub tv_PosterSelectionDisplay()

        For i = Panel16.Controls.Count - 1 To 0 Step -1
            Panel16.Controls.RemoveAt(i)
        Next



        Dim tempint As Integer = (tvposterpage * (Preferences.maximumthumbs) + 1) - Preferences.maximumthumbs
        Dim tempint2 As Integer = tvposterpage * 10

        If tempint2 > usedlist.Count Then
            tempint2 = usedlist.Count
        End If

        If Not rbTVbanner.Checked Then
        Label72.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & usedlist.Count.ToString & " Images"
        Else
            Label72.Text = "Displaying 1 to " & usedlist.Count.ToString
            tempint2 = usedlist.Count
        End If

        Dim locationX As Integer = 0
        Dim locationY As Integer = 0
        Dim itemcounter As Integer = 0
        Dim tempboolean As Boolean = True
        'If CheckBox8.Checked = True Or CheckBox8.Visible = False Then
        If rbTVposter.Checked = True Or rbTVbanner.Enabled = False Then
            For f = tempint - 1 To tempint2 - 1
                Dim item As String = Utilities.Download2Cache(usedlist(f).SmallUrl)
                tvposterpicboxes() = New PictureBox()
                With tvposterpicboxes
                    .Location = New Point(locationX, locationY)
                    .Width = 123
                    .Height = 168
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Tag = usedlist(f).Url
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "poster" & itemcounter.ToString
                    AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
                    'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
                End With
                util_ImageLoad(tvposterpicboxes, item, "")

                tvpostercheckboxes() = New RadioButton()
                With tvpostercheckboxes
                    .Location = New Point(locationX + 50, locationY + 166) '179
                    .Name = "postercheckbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    .Tag = usedlist(f).Url
                    AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
                End With

                itemcounter += 1
                Me.Panel16.Controls.Add(tvposterpicboxes())
                Me.Panel16.Controls.Add(tvpostercheckboxes())
                Me.Refresh()
                Application.DoEvents()
                If tempboolean = True Then
                    locationY = 192
                Else
                    locationX += 120
                    locationY = 0
                End If
                tempboolean = Not tempboolean
            Next
        Else
            For f = tempint - 1 To tempint2 - 1
                Dim item As String = Utilities.Download2Cache(usedlist(f).SmallUrl)
                tvposterpicboxes() = New PictureBox()
                With tvposterpicboxes
                    .Location = New Point(0, locationY)
                    .Width = 600
                    .Height = 114
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Tag = usedlist(f).Url
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "poster" & itemcounter.ToString
                    AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
                    'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
                End With
                util_ImageLoad(tvposterpicboxes, item, "")

                tvpostercheckboxes() = New RadioButton()
                With tvpostercheckboxes
                    .Location = New Point(290, locationY + 110)
                    .Name = "postercheckbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    .Tag = usedlist(f).Url
                    AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
                End With
                itemcounter += 1
                locationY += 140

                Me.Panel16.Controls.Add(tvposterpicboxes())
                Me.Panel16.Controls.Add(tvpostercheckboxes())
            Next
        End If
        
        Application.DoEvents()
        If rbTVbanner.Checked AndAlso Me.Panel16.Controls.Count > 0 Then EnableTvBannerScrolling
        Me.Refresh()
    End Sub

    Private Sub tv_PosterRadioChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim tempstring As String = sender.name
        Dim tempint As Integer = 0
        Dim tempstring2 As String = tempstring
        Dim allok As Boolean = False
        tempstring = tempstring.Replace("postercheckbox", "")
        tempint = Convert.ToDecimal(tempstring)
        Dim hires(1)
        Dim lores(1)
        lores(0) = ""
        hires(0) = ""
        lores(1) = ""
        hires(1) = ""
        For Each cont As Control In Me.Panel16.Controls()
            If cont.Name.Replace("poster", "") = tempint.ToString Then
                Dim picbox As PictureBox = cont
                lores(0) = "Save Image (" & picbox.Image.Width & " x " & picbox.Image.Height & ")"
                lores(1) = picbox.Name
                For Each poster In usedlist
                    If poster.smallUrl = sender.tag Then
                        If Not IsNothing(poster.Resolution) AndAlso IsNumeric(poster.resolution.Replace("x", "")) Then
                            hires(0) = "Save Image (" & poster.resolution & ")"
                            hires(0) = hires(0).replace("x", " x ")
                        Else
                            hires(0) = "Save Image (Hi-Res)"
                        End If
                        hires(1) = poster.url
                        Exit For
                    End If
                Next
                allok = True
                Exit For
            End If
        Next

        If allok = True Then
            btnTvPosterSaveBig.Text = hires(0)
            btnTvPosterSaveBig.Visible = True
            btnTvPosterSaveBig.Tag = hires(1)
        Else
            btnTvPosterSaveBig.Visible = False
        End If
    End Sub

    Private Sub tv_PosterDoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim tempstring As String = sender.name.replace("poster", "postercheckbox")

        For Each Control In Panel16.Controls
            If Control.name = tempstring Then
                Dim rb As RadioButton = Control
                rb.Checked = True
            End If
        Next
        Dim messbox As frmMessageBox = New frmMessageBox("Please wait,", "", "Downloading Full Res Image")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        messbox.Show()
        Me.Refresh()
        messbox.Refresh()
        Dim cachefile As String = Utilities.Download2Cache(sender.Tag.ToString)
        Call util_ZoomImage(cachefile)
        messbox.Close()
    End Sub

    Private Sub TvPosterSave(ByVal imageUrl As String)
        Try
            Dim witherror As Boolean = False
            Dim witherror2 As Boolean = False
            Dim path As String = ""
            Dim eden As Int16=0
            Dim frodo As Int16=0
            Dim imagePaths As New List(Of String)
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim workingposterpath = WorkingTvShow.FolderPath & "folder.jpg"
            If ComboBox2.Text.ToLower = "main image" Then
                If Preferences.EdenEnabled OrElse Preferences.tvfolderjpg Then
                    imagePaths.Add(workingposterpath)
                    eden = 1
                End If
                If Preferences.FrodoEnabled Then
                    If rbTVbanner.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "banner.jpg"))
                        frodo = 1
                    ElseIf rbTVposter.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "poster.jpg"))
                        frodo = 1
                    End If
                End If
            ElseIf ComboBox2.Text.ToLower.IndexOf("season") <> -1 And ComboBox2.Text.ToLower.IndexOf("all") = -1 Then
                Dim temp As String = ComboBox2.Text.ToLower
                temp = temp.Replace(" ", "")
                If Preferences.EdenEnabled Then
                    imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), temp & ".tbn"))
                    eden =1
                End If
                If Preferences.FrodoEnabled Then
                    If rbTVbanner.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), temp & "-banner.jpg"))
                        frodo = 1
                    ElseIf rbTVposter.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), temp & "-poster.jpg"))
                        frodo = 1
                    End If
                End If
                If Preferences.seasonfolderjpg Then
                    temp = temp.Replace("season", "")
                    Dim seasonno As Integer = temp.ToInt
                    Dim seasonpath As String = Nothing
                    For Each ep As TvEpisode In WorkingTvShow.Episodes
                        If ep.Season.Value = seasonno Then
                            seasonpath = ep.FolderPath.Replace(WorkingTvShow.FolderPath, "")
                            Exit For
                        End If
                    Next
                    If Not IsNothing(seasonpath) Then imagePaths.Add(WorkingTvShow.FolderPath & seasonpath & "folder.jpg")
                End If
            ElseIf ComboBox2.Text.ToLower.IndexOf("season") <> -1 And ComboBox2.Text.ToLower.IndexOf("all") <> -1 Then
                If Preferences.EdenEnabled Then
                    If Preferences.seasonall="poster" and rbTVposter.Checked Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-all.tbn"))
                        eden =1
                    ElseIf Preferences.seasonall="wide" and rbTVbanner.Checked Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-all.tbn"))
                        eden =1
                    End If 
                End If
                If Preferences.FrodoEnabled Then
                    If rbTVbanner.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-all-banner.jpg"))
                        frodo = 1
                    ElseIf rbTVposter.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-all-poster.jpg"))
                        frodo = 1
                    End If
                End If
            ElseIf ComboBox2.Text.ToLower = "specials" Then
                If Preferences.EdenEnabled Then
                    imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-specials.tbn"))
                    eden =1
                End If
                If Preferences.FrodoEnabled Then
                    If rbTVbanner.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-specials-banner.jpg"))
                        frodo = 1
                    ElseIf rbTVposter.Checked = True Then
                        imagePaths.Add(workingposterpath.Replace(IO.Path.GetFileName(workingposterpath), "season-specials-poster.jpg"))
                        frodo = 1
                    End If
                End If
            End If

            Dim messbox As frmMessageBox = New frmMessageBox("Please wait,", "", "Downloading Full Resolution Image")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
                
            Try
                If Not IsNothing(imageUrl) Then
                    witherror = Not DownloadCache.SaveImageToCacheAndPaths(imageUrl, ImagePaths, True)
                    If Not witherror Then
                        path = imagePaths(0)
                        If rbTVbanner.Checked Then
                            util_ImageLoad(tv_PictureBoxBottom, path, Utilities.DefaultTvBannerPath)
                        End If
                        If rbTVposter.Checked Then
                            util_ImageLoad(tv_PictureBoxRight, path, Utilities.DefaultTvPosterPath)
                        End If
                        util_ImageLoad(PictureBox12, path, Utilities.DefaultTvPosterPath)
                        Label73.Text = "Current Poster - " & PictureBox12.Image.Width.ToString & " x " & PictureBox12.Image.Height.ToString
                    End If
                End If

                If witherror = True  Then
                    MsgBox("Unable to download image")
                Else
                    If eden =1 then
                        EdenImageTrue.Visible =True
                        EdenImageTrue.Text="Eden Image Present"
                    End if
                    If frodo =1 then
                        FrodoImageTrue.Visible =True
                        FrodoImageTrue.Text="Frodo Image Present"
                    End if
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally

                messbox.Close()
            End Try
            path = ""
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
    
    Private Sub RefreshMovieNfoFilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshMovieNfoFilesToolStripMenuItem.Click
        Try
            Call util_BatchUpdate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Sub ShowMovieFilter(cbFilter As Control)
        If Not cbFilter.Visible Then 
            movie_filters.GetItem(cbFilter.Name).Visible = True
            Preferences.movie_filters.SetMovieFiltersVisibility
            UpdateMovieFiltersPanel
        End If
    End Sub

    Private Sub mov_WallReset()
        For i = TabPage22.Controls.Count - 1 To 0 Step -1
            'If  Is PictureBox(TabPage22.Controls(i)) Then
            TabPage22.Controls.RemoveAt(i)
            'End If
        Next
        walllocked = True
        Dim count As Integer = 0
        Dim locx As Integer = 0
        Dim locy As Integer = 0
        Dim maxcount As Integer = Convert.ToInt32((TabPage22.Width - 50) / 150)

        While (DataGridViewMovies.SelectedRows.Count / maxcount) > 164
            maxcount += 1
        End While      

        Try
            'Panel17.AutoScroll = False
            For Each pic In pictureList
                Try
                    If count = maxcount Then
                        count = 0
                        locx = 0
                        locy += 200
                    End If

                    With pic
                        Dim vscrollPos As Integer = TabPage22.VerticalScroll.Value
                        .Location = New Point(locx, locy - vscrollPos)
                        .ContextMenuStrip = MovieWallContextMenu
                    End With
                    locx += 150
                    count += 1

                    Me.TabPage22.Controls.Add(pic)
                    TabPage22.Refresh()
                    Application.DoEvents()

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            Next

        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        Finally
            walllocked = False
        End Try
    End Sub

    Private Sub mov_WallSetup()
        Dim check As Boolean = True
        Dim count As Integer = 0
        Dim locx As Integer = 0
        Dim locy As Integer = 0

       
        If moviecount_bak <> DataGridViewMovies.RowCount Then moviecount_bak = DataGridViewMovies.RowCount : check = False
        If check = True Then Return

        maxcount = Convert.ToInt32((TabPage22.Width - 50) / 150)

        While (DataGridViewMovies.SelectedRows.Count / maxcount) > 164
            maxcount += 1
        End While        
        

        pictureList.Clear()
        For i = TabPage22.Controls.Count - 1 To 0 Step -1
            If TabPage22.Controls(i).Name = "" Then
                TabPage22.Controls.RemoveAt(i)
            End If
        Next
        TabPage22.Refresh()
        Application.DoEvents()

        'Panel17.AutoScroll = False

        For Each row As DataGridViewRow In DataGridViewMovies.Rows

            Dim m As Data_GridViewMovie = row.DataBoundItem

            bigPictureBox = New PictureBox()
            With bigPictureBox
                '.Location = New Point(0, 0)
                .Width = 150
                .Height = 200
                .SizeMode = PictureBoxSizeMode.StretchImage
                '.Image = sender.image
                Dim filename As String = Utilities.GetCRC32(m.fullpathandfilename)
                Dim posterCache As String = IO.Path.Combine(applicationPath, "settings\postercache\" & filename & ".jpg")
                If Not File.Exists(posterCache) And File.Exists(Preferences.GetPosterPath(m.fullpathandfilename)) Then
                    Try
                        Dim bitmap2 As New Bitmap(Preferences.GetPosterPath(m.fullpathandfilename))
                        bitmap2 = Utilities.ResizeImage(bitmap2, 150, 200)
                        Utilities.SaveImage(bitmap2, IO.Path.Combine(posterCache))
                        bitmap2.Dispose()
                    Catch
                        'Invalid file
                        File.Delete(Preferences.GetPosterPath(m.fullpathandfilename))
                    End Try
                End If
                If File.Exists(posterCache) Then
                    Try
                        .Image = Utilities.LoadImage(posterCache)
                    Catch
                        'Invalid file
                        File.Delete(Preferences.GetPosterPath(m.fullpathandfilename))
                    End Try
                Else
                    .Image = Utilities.LoadImage(Utilities.DefaultPosterPath)
                End If
                


                .Tag = m.fullpathandfilename
                Dim toolTip1 As ToolTip = New ToolTip(Me.components)

                Dim outline As String = m.outline
                Dim newoutline As List(Of String) = util_TextWrap(outline, 50)
                outline = ""
                For Each line In newoutline
                    outline = outline & vbCrLf & line
                Next
                outline.TrimEnd(vbCrLf)
                toolTip1.SetToolTip(bigPictureBox, m.fullpathandfilename & vbCrLf & vbCrLf & m.DisplayTitleAndYear & vbCrLf & outline)
                toolTip1.Active = True
                toolTip1.InitialDelay = 0

                .Visible = True
                .BorderStyle = BorderStyle.None
                .WaitOnLoad = True
                .ContextMenuStrip = MovieWallContextMenu
                AddHandler bigPictureBox.MouseEnter, AddressOf util_MouseEnter
                AddHandler bigPictureBox.DoubleClick, AddressOf mov_WallClicked
                If count = maxcount Then
                    count = 0
                    locx = 0
                    locy += 200
                End If
                walllocked = True
                Dim vscrollPos As Integer = TabPage22.VerticalScroll.Value
                If mouseDelta <> 0 Then
                    vscrollPos = vscrollPos - mouseDelta
                    mouseDelta = 0
                End If
                .Location = New Point(locx, locy - vscrollPos)
                locx += 150
                count += 1

            End With
            Me.TabPage22.Controls.Add(bigPictureBox)
            pictureList.Add(bigPictureBox)
            Me.TabPage22.Refresh()
            Application.DoEvents()
            walllocked = False
        Next

        walllocked = False
    End Sub

    Private Sub mov_WallClicked(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As Windows.Forms.PictureBox = sender
        Dim tempstring As String = item.Tag
        For f = 0 To DataGridViewMovies.RowCount - 1
            If DataGridViewMovies.Rows(f).Cells("fullpathandfilename").ToString = tempstring Then
                DataGridViewMovies.ClearSelection()
                DataGridViewMovies.Rows(f).Selected = True
                Application.DoEvents()
                currentTabIndex = 0
                Me.TabControl2.SelectedIndex = 0
                Exit For
            End If
        Next
    End Sub

    Private Function util_TextWrap(ByVal text As String, ByVal linelength As Integer)
        Dim ReturnValue As New List(Of String)
        text = Trim(text)

        Dim Words As String() = text.Split(" ")

        If Words.Length = 1 And Words(0).Length > linelength Then
            Dim lines As Integer = (Int(text.Length / linelength) + 1)
            text = text.PadRight(lines * linelength)
            For i = 0 To lines - 1
                Dim SliceStart As Integer = i * linelength
                ReturnValue.Add(text.Substring(SliceStart, linelength))
            Next
        Else
            Dim CurrentLine As New System.Text.StringBuilder
            For Each Word As String In Words
                If CurrentLine.Length + Word.Length < linelength Then
                    CurrentLine.Append(Word & " ")
                Else
                    If Word.Length > linelength Then
                        Dim Slice As String = Word.Substring(0, linelength - CurrentLine.Length)
                        CurrentLine.Append(Slice)
                        ReturnValue.Add(CurrentLine.ToString)
                        CurrentLine = New System.Text.StringBuilder()
                        Word = Word.Substring(Slice.Length, Word.Length - Slice.Length)
                        Dim RemainingSlices As Integer = Int(Word.Length / linelength) + 1
                        For LineNumber = 1 To RemainingSlices
                            If LineNumber = RemainingSlices Then
                                CurrentLine.Append(Word & " ")
                            Else
                                Slice = Word.Substring(0, linelength)
                                CurrentLine.Append(Slice)
                                ReturnValue.Add(CurrentLine.ToString)
                                CurrentLine = New System.Text.StringBuilder()
                                Word = Word.Substring(Slice.Length, Word.Length - Slice.Length)
                            End If
                        Next
                    Else
                        ReturnValue.Add(CurrentLine.ToString)
                        CurrentLine = New System.Text.StringBuilder(Word & " ")
                    End If
                End If
            Next

            If CurrentLine.Length > 0 Then
                ReturnValue.Add(CurrentLine.ToString)
            End If
        End If

        Return ReturnValue

    End Function

    Private Shadows Sub util_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClickedControl = sender.tag
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub MovieWallContextMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MovieWallContextMenu.Opening
        Dim tempstring As String = ClickedControl
        If tempstring <> Nothing Then
            Dim trailerpath As String = GetTrailerPath(tempstring)
            If IO.File.Exists(trailerpath) Then
                tsmiWallPlayTrailer.Enabled = True
            Else
                tsmiWallPlayTrailer.Enabled = False
            End If
        End If
    End Sub

    Private Sub PlayMovieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayMovieToolStripMenuItem.Click
        Try
            Dim tempstring As String = ClickedControl
            If tempstring = Nothing Then
                Exit Sub
            End If
            If tempstring = "" Then
                Exit Sub
            End If
            tempstring = Utilities.GetFileName(tempstring)
            Dim playlist As New List(Of String)
            playlist = Utilities.GetMediaList(tempstring)
            
            If playlist.Count <= 0 Then
                MsgBox("No Media File Found For This nfo")
                Exit Sub
            End If

            LaunchPlayList(playlist)

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub EditMovieToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditMovieToolStripMenuItem1.Click
        Try
            Dim tempstring As String = ClickedControl
            For f = 0 To DataGridViewMovies.RowCount - 1
                If DataGridViewMovies.Rows(f).Cells("fullpathandfilename").Value.ToString = tempstring Then
                    DataGridViewMovies.ClearSelection()
                    DataGridViewMovies.Rows(f).Selected = True
                    DisplayMovie()
                    Application.DoEvents()
                    currentTabIndex = 4
                    Me.TabControl2.SelectedIndex = 4
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DToolStripMenuItem.Click
        Try
            Dim tempstring As String = ClickedControl
            If tempstring <> Nothing Then
                Try
                    Dim Temp2 As String = Preferences.GetPosterPath(tempstring)
                    If IO.File.Exists(Temp2) Then
                        Me.ControlBox = False
                        MenuStrip1.Enabled = False
                        'ToolStrip1.Enabled = False
                        'Dim newimage As New Bitmap(Preferences.GetPosterPath(tempstring))
                        'Dim newimage2 As New Bitmap(newimage)
                        'newimage.Dispose()
                        'Call util_ZoomImage(newimage2)
                        util_ZoomImage(Temp2)
                    Else
                        MsgBox("Cant find file:-" & vbCrLf & Preferences.GetPosterPath(tempstring))
                    End If
                Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub OpenFolderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFolderToolStripMenuItem1.Click
        Try
            Dim tempstring As String = ClickedControl
            If tempstring <> Nothing Then
                Try
                    Call util_OpenFolder(tempstring)
                Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub tsmiWallPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiWallPlayTrailer.Click
        Try
            Dim tempstring As String = ClickedControl
            If tempstring <> Nothing Then
                Dim trailerpath As String = GetTrailerPath(tempstring)
                If IO.File.Exists(trailerpath) Then
                    Dim trailerstring = applicationPath & "\settings\temp.m3u"
                    Dim file = IO.File.CreateText(trailerstring)
                    file.WriteLine(trailerpath)
                    file.Close()
                    If Preferences.videomode = 1 Then Call util_VideoMode1(trailerstring)
                    If Preferences.videomode = 2 Or Preferences.videomode = 3 Then Call util_VideoMode2(trailerstring)
                    If Preferences.videomode >= 4 Then
                        If Preferences.selectedvideoplayer <> Nothing Then
                            Call util_VideoMode4(trailerstring)
                        Else
                            Call util_VideoMode1(trailerstring)
                        End If
                    End If
                Else
                    MsgBox("No downloaded trailer present")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

#Region "Media Info Export"
    Dim exportMovieInfo As Boolean = False  'these are used to allow only a single execution of media export functions
    Dim exportTVInfo As Boolean = False     'when there may be mulitple drop-down events. (Found that out the hard way!)

    Private Sub ExportMovieListInfoToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ExportMovieListInfoToolStripMenuItem.DropDownItemClicked
        If Not exportMovieInfo Then exportMovieInfo = mediaInfoExp.setTemplate(e.ClickedItem.Text)
    End Sub

    Private Sub ExportTVShowInfoToolStripMenuItem_DropDownItemClicked(sender As Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ExportTVShowInfoToolStripMenuItem.DropDownItemClicked
        If Not exportTVInfo Then exportTVInfo = mediaInfoExp.setTemplate(e.ClickedItem.Text)
    End Sub

    Private Sub ExportMovieListInfoToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportMovieListInfoToolStripMenuItem.DropDownClosed
        If exportMovieInfo Then
            exportMovieInfo = False
            Call util_ExportMediaListInfo(MediaInfoExport.mediaType.Movie)
        End If
    End Sub

    Private Sub ExportTVShowInfoToolStripMenuItem_DropDownClosed(sender As Object, e As System.EventArgs) Handles ExportTVShowInfoToolStripMenuItem.DropDownClosed
        If exportTVInfo = True Then
            exportTVInfo = False
            Call util_ExportMediaListInfo(MediaInfoExport.mediaType.TV)
        End If
    End Sub

    Private Sub util_ExportMediaListInfo(ByVal mediaType As String)
        Dim savepath As String
        Dim extensions As New Dictionary(Of String, String)
        extensions.Add("html", "Html Documents (*.html)|*.html")
        extensions.Add("xml", "XML Data (*.xml)|*.xml")
        extensions.Add("csv", "CSV (Comma delimited) (*.csv)|*.csv")
        Dim ext As String = mediaInfoExp.getPossibleFileType
        Dim idx As Integer
        For idx = 1 To extensions.Count
            If extensions.Keys(idx - 1) = ext Then Exit For
        Next
        With SaveFileDialog1
            .DefaultExt = ext
            .Filter = String.Join("|", extensions.Values)
            .FilterIndex = idx
            .Title = "ExportMedia File"
            .OverwritePrompt = True
            .CheckPathExists = True
        End With
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            savepath = SaveFileDialog1.FileName
            Dim mediaCollection As Object
            If mediaType = MediaInfoExport.mediaType.Movie Then
                'Dirty hack to get the media export to use the data grid source. Wasn't as straight forward as hoped,
                'and after spending many hours trying to find an elegant solution, I gave up.
                'If anyone comes across this and thinks "Huey, you twat, just do it like this", then please go right
                'ahead! - HueyHQ 15Feb13
                Dim mediaList As New List(Of ComboList)
                For Each mediaItem As Data_GridViewMovie In DataGridViewMovies.DataSource
                    mediaList.Add(mediaItem.Export)
                Next
                mediaCollection = mediaList
            Else
                mediaCollection = Cache.TvCache.Shows
            End If
            mediaInfoExp.createDocument(savepath, mediaCollection)
        End If
    End Sub

#End Region  'Media Info Export


    Private Sub bckgroundscanepisodes_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bckgroundscanepisodes.ProgressChanged
        Try
            If e.ProgressPercentage = 0 Then
                ToolStripStatusLabel6.Text = e.UserState
                ToolStripStatusLabel6.Visible = True
            ElseIf e.ProgressPercentage = 1 Then
                If TypeOf e.UserState Is TvEpisode Then
                    Dim TempEpisode As TvEpisode = CType(e.UserState, TvEpisode)

                    TempEpisode.ShowObj.AddEpisode(TempEpisode)
                    TempEpisode.SeasonObj.UpdateTreenode()
                    TempEpisode.UpdateTreenode(True)
                    'This bit updates the Epsiode Count on the fly when the progress is updated. It has to be done here to avoid thread issues. (GUI wouldn't update properly) 
                    TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub bckgroundscanepisodes_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs, Optional ByVal manual As Boolean = False) Handles bckgroundscanepisodes.DoWork
        Try
            Dim List As List(Of TvShow) = e.Argument(0)
            Dim Force As Boolean = e.Argument(1)

            Call TV_EpisodeScraper(List, Force)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ep_Search()
        Dim ShowList As New List(Of TvShow)

        If Not bckgroundscanepisodes.IsBusy And Not Bckgrndfindmissingepisodes.IsBusy Then
            'ToolStripButton10.Visible = True
            TabPage15.Text = "Cancel Episode Search"
            TabPage15.ToolTipText = "This cancels the episode search" & vbCrLf & "and episode scraper thread"

            For Each item In Cache.TvCache.Shows
                If (item.NfoFilePath.ToLower.IndexOf("tvshow.nfo") <> -1) And ((item.State = Media_Companion.ShowState.Open) Or TVSearchALL = True) Then
                    ShowList.Add(item)
                End If
            Next
            bckgroundscanepisodes.RunWorkerAsync({ShowList, TVSearchALL}) 'if searching all episodes (inc locked) TVSearchALL is true
        ElseIf bckgroundscanepisodes.IsBusy Then
            MsgBox("This Episode Scraper is already running")
        ElseIf Bckgrndfindmissingepisodes.IsBusy Then
            MsgBox("The missing episode search cannot be performed" & vbCrLf & "    while the episode scraper is running")
        End If
    End Sub

    Private Sub SearchThisShowForNewEpisodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_SearchNewEp.Click
        Try
            If TvTreeview.SelectedNode Is Nothing Then Exit Sub

            Dim Season As TvSeason
            Dim Episode As TvEpisode
            Dim ShowList As New List(Of TvShow)
            Dim selectednode As Integer = TvTreeview.SelectedNode.Index
            If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
                ShowList.Add(TvTreeview.SelectedNode.Tag)
            ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then

                Season = TvTreeview.SelectedNode.Tag
                ShowList.Add(Season.ShowObj)
            ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                Episode = TvTreeview.SelectedNode.Tag
                ShowList.Add(Episode.ShowObj)
            End If
            Dim OverrideLock As Boolean = False
            If ShowList(0).State <> 0 Then  'i.e. it is either locked or unverified
                If MsgBox("This show is either 'Locked' or 'Unverified'. Do you want to continue scan?", MsgBoxStyle.YesNo, "Question?") = MsgBoxResult.Yes Then
                    OverrideLock = True
                Else
                    Exit Sub
                End If
            End If

            If Not bckgroundscanepisodes.IsBusy And Not Bckgrndfindmissingepisodes.IsBusy Then
                'ToolStripButton10.Visible = True
                TabPage15.Text = "Cancel Episode Search"
                TabPage15.ToolTipText = "This cancels the episode search" & vbCrLf & "and episode scraper thread"

                bckgroundscanepisodes.RunWorkerAsync({ShowList, OverrideLock})
            ElseIf bckgroundscanepisodes.IsBusy Then
                MsgBox("This Episode Scraper is already running")
            ElseIf Bckgrndfindmissingepisodes.IsBusy Then
                MsgBox("The missing episode search cannot be performed" & vbCrLf & "    while the episode scraper is running")
            End If
            Do Until Not bckgroundscanepisodes.IsBusy
                Application.DoEvents()
            Loop
            TvTreeview.SelectedNode = TvTreeview.Nodes(selectednode)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Public Function ep_Get(ByVal tvdbid As String, ByVal sortorder As String, ByVal seriesno As String, ByVal episodeno As String, ByVal language As String)
        Dim episodestring As String = ""
        Dim episodeurl As String = ""
        Dim xmlfile As String

        If language.ToLower.IndexOf(".xml") = -1 Then
            language = language & ".xml"
        End If
        episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seriesno & "/" & episodeno & "/" & language

        xmlfile = Utilities.DownloadTextFiles(episodeurl)
        If xmlfile.Contains("Could not connect") Then Return xmlfile               ' Added check if TVDB is unavailable.
        Dim xmlOK As Boolean = Utilities.CheckForXMLIllegalChars(xmlfile)
        If xmlOK Then
            episodestring = "<episodedetails>"
            episodestring = episodestring & "<url>" & episodeurl & "</url>"
            Dim mirrorslist As New XmlDocument
            mirrorslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In mirrorslist("Data")
                Select Case thisresult.Name
                    Case "Episode"
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "EpisodeName"
                                    episodestring = episodestring & "<title>" & mirrorselection.InnerXml & "</title>"
                                Case "FirstAired"
                                    episodestring = episodestring & "<premiered>" & mirrorselection.InnerXml & "</premiered>"
                                Case "GuestStars"
                                    Dim gueststars() As String = mirrorselection.InnerXml.Split("|")
                                    For Each guest In gueststars
                                        If Not String.IsNullOrEmpty(guest) Then
                                            episodestring = episodestring & "<actor><name>" & guest & "</name></actor>"
                                        End If
                                    Next
                                Case "Director"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.Trim("|")
                                    episodestring = episodestring & "<director>" & tempstring & "</director>"
                                Case "Writer"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.Trim("|")
                                    episodestring = episodestring & "<credits>" & tempstring & "</credits>"
                                Case "Overview"
                                    episodestring = episodestring & "<plot>" & mirrorselection.InnerXml & "</plot>"
                                Case "Rating"
                                    episodestring = episodestring & "<rating>" & mirrorselection.InnerXml & "</rating>"
                                Case "IMDB_ID"
                                    episodestring = episodestring & "<imdbid>" & mirrorselection.InnerXml & "</imdbid>"
                                Case "id"
                                    episodestring = episodestring & "<uniqueid>" & mirrorselection.InnerXml & "</uniqueid>"
                                Case "seriesid"
                                    episodestring = episodestring & "<showid>" & mirrorselection.InnerXml & "</showid>"
                                Case "filename"
                                    episodestring = episodestring & "<thumb>http://www.thetvdb.com/banners/" & mirrorselection.InnerXml & "</thumb>"
                            End Select
                        Next
                End Select
            Next
            episodestring = episodestring & "</episodedetails>"
        Else
            If CheckBoxDebugShowTVDBReturnedXML.Checked = True Then MsgBox(xmlfile, MsgBoxStyle.OkOnly, "FORM1 getepisode - TVDB returned.....")
            episodestring = "Error"
        End If
        Return episodestring
    End Function

    Private Sub ReloadItemToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripReloadFromCache.Click
        Try
            Call mov_FormPopulate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TabPage22_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage22.LostFocus
        Try
            TabPage22.Focus()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TabPage22_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabPage22.MouseWheel
        Try

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RefreshActorDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 
        Try
            Call mov_ActorRebuild()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub DownsizeAllFanartsToSelectedSizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownsizeAllFanartsToSelectedSizeToolStripMenuItem.Click
        DownSizeAll("Backdrops")
    End Sub

    Private Sub DownsizeAllPostersToSelectedSizeToolStripMenuItem_Click( sender As System.Object,  e As System.EventArgs) Handles DownsizeAllPostersToSelectedSizeToolStripMenuItem.Click
        DownSizeAll("Posters")
    End Sub
    
    Private Sub DownSizeAll(postersOrBackdrops As String) 

        Dim tempint As Integer = oMovies.MovieCache.Count

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim point As Point
        Dim height = 0

        If postersOrBackdrops = "Backdrops" then
            point = Movie.GetBackDropResolution(Preferences.BackDropResolutionSI)
        Else
            height = Movie.GetHeightResolution(Preferences.PosterResolutionSI)
        End If


        Using messbox As frmMessageBox = New frmMessageBox("Please wait - " & postersOrBackdrops & " are being resized", "", tempint.ToString & " remaining")

            messbox.Show
            Me.Refresh
            messbox.Refresh


            Dim path   = ""

            For Each m In oMovies.MovieCache

                If postersOrBackdrops = "Backdrops" then

                    path = Preferences.GetFanartPath(m.fullpathandfilename)

                    If File.Exists(path) then
                        DownloadCache.CopyAndDownSizeImage(path, path, point.x, point.y)
                    End If
                Else

                    path   = Preferences.GetPosterPath(m.fullpathandfilename)

                    If File.Exists(path) then
                        DownloadCache.CopyAndDownSizeImage(path, path, , height  )
                    End If
                End If

                tempint -= 1
                messbox.TextBox3.Text = tempint.ToString & " remaining"
                messbox.TextBox3.Refresh
                
                Application.DoEvents
            Next
        End Using
    End Sub

    Public Sub mov_RebuildMovieCaches

        'Enabled = False

        mov_PreferencesDisplay

        'ProgressAndStatus1.Display()
        'ProgressAndStatus1.Status("Rebuilding Movie caches...")
        'ProgressAndStatus1.ReportProgress(0, "Processing....")
        'Application.DoEvents()

        'oMovies.RebuildCaches
        RunBackgroundMovieScrape("RebuildCaches")

        'filteredList.Clear
        'filteredList.AddRange(oMovies.MovieCache)
        'filteredListObj.Clear
        'filteredListObj.AddRange(oMovies.Data_GridViewMovieCache)


        'ProgressAndStatus1.ReportProgress(0, "Apply Filters...")
        'Mc.clsGridViewMovie.mov_FiltersAndSortApply

        'ProgressAndStatus1.ReportProgress(0, "Reload Main Page...")
        'mov_FormPopulate

        'If DataGridViewMovies.Rows.Count>0 Then
        '    DataGridViewMovies.Rows(0).Selected = True
        '    DisplayMovie
        'End If

        'Activate
        'Enabled = True
        'ProgressAndStatus1.Visible = False
    End Sub

    '    Public Sub updatetree(Optional ByVal addnew As Boolean = True)


    '        Dim oldfolders As New List(Of String)
    '        totalTvShowCount = 0
    '        totalEpisodeCount = 0
    '        TextBox32.Text = ""
    '        TextBox33.Text = ""
    '        'Me.Enabled = False
    '        'basictvlist.Clear()
    '        TvTreeview.Nodes.Clear()

    '        For Each tvshow In TvShows
    '            If tvshow.fullpath IsNot Nothing Then
    '                Dim tempstring As String = tvshow.fullpath.Replace("\tvshow.nfo", "")
    '                If Not tvFolders.Contains(tempstring) Then
    '                    oldfolders.Add(tempstring)
    '                End If
    '            End If
    '        Next
    '        For Each folder In oldfolders
    '            For Each oldshow In TvShows
    '                Dim tempstring As String = oldshow.fullpath.Replace("\tvshow.nfo", "")
    '                If tempstring = folder Then
    '                    TvShows.Remove(oldshow)
    '                    For Each fol In tvFolders
    '                        If oldshow.fullpath.IndexOf(fol) <> -1 Then
    '                            tvFolders.Remove(fol)
    '                            Exit For
    '                        End If
    '                    Next
    '                    Exit For
    '                End If
    '            Next
    '        Next

    '        'get list of new
    '        Dim folderstoadd As New List(Of String)
    '        For Each folder In tvFolders
    '            Dim add As Boolean = True
    '            Dim tempstring2 As String = folder
    '            For Each tvshow In TvShows
    '                Dim tempstring As String = tvshow.fullpath.Replace("\tvshow.nfo", "")
    '                If folder = tempstring Then
    '                    add = False
    '                    Exit For
    '                End If
    '            Next
    '            If add = True Then
    '                folderstoadd.Add(tempstring2)
    '            End If
    '        Next

    '        If folderstoadd.Count > 0 Then
    '            messbox = New frmMessageBox("New TV Folders Found", "Adding to DB", "Please Wait")
    '            'remove old
    '            messbox.Show()
    '            messbox.Refresh()
    '            Application.DoEvents()
    '            messbox.Show()
    '            Try
    '                For Each tvfolder In folderstoadd
    '                    Try
    '                        Dim shownfopath As String = IO.Path.Combine(tvfolder, "tvshow.nfo")
    '                        Dim newtvshownfo As TvShow
    '                        newtvshownfo = nfoFunction.loadbasictvshownfo(shownfopath)
    '                        'Try
    '                        '    If addnew = True Then
    '                        '        If Not IO.File.Exists(shownfopath) Then
    '                        'Call setgoingnewtvshows(shownfopath)
    '                        '        End If
    '                        '    End If
    '                        'Catch
    '                        'End Try
    '                        If newtvshownfo.title <> Nothing Then
    '                            If newtvshownfo.status.IndexOf("skipthisfile") = -1 Then
    '                                Dim skip As Boolean = False
    '                                For Each tvshow In TvShows
    '                                    If newtvshownfo.fullpath = tvshow.fullpath Then
    '                                        skip = True
    '                                        Exit For
    '                                    End If
    '                                Next
    '                                If skip = False Then
    '                                    ListtvFiles(newtvshownfo, "*.NFO")
    '                                    TvShows.Add(newtvshownfo)
    '                                End If
    '                            End If
   '                        End If
    '                        realTvPaths.Add(tvfolder)
    '                    Catch ex As Exception
    '#If SilentErrorScream Then
    '                        Throw ex
    '#End If
    '                    End Try
    '                Next
    '            Catch ex As Exception
    '#If SilentErrorScream Then
    '                Throw ex
    '#End If
    '            End Try
    '            messbox.Close()
    '            Me.Activate()               'bring main form back to front
    '        End If
    '        'For Each tv In basictvlist
    '        '    ListtvFiles(tv, "*.NFO")
    '        'Next
    '        'Call populatetvtree()
    '        'messbox.Close()
    '        Me.Enabled = True

    '        Call TV_SaveTvData("New Function")

    '    End Sub

    Private Sub CheckRootsForToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckRootsForToolStripMenuItem.Click
        Try
            'tv_FoldersSetup()
            tv_ShowFind(Preferences.tvRootFolders, False)
            If newTvFolders.Count > 0 Then
                For Each item In newTvFolders
                    Preferences.tvFolders.Add(item)
                Next
                Preferences.tvFolders.Sort()
                Preferences.ConfigSave()
            End If
            tv_ShowScrape()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    

    Public Sub tv_ShowFind(ByVal rootfolders As List(Of String), Optional ByVal skiplistboxchk As Boolean = True)
        Dim Folders As List(Of String)
        newTvFolders.Clear()
        For Each folder In rootfolders 'ListBox5.Items
            Folders = Utilities.EnumerateFolders(folder, 0)
            For Each strfolder2 As String In Folders
                If Not Preferences.tvFolders.Contains(strfolder2) AndAlso Utilities.ValidMovieDir(strfolder2) Then  'Not ListBox6.Items.Contains(strfolder2)
                    If Not skiplistboxchk AndAlso Not ListBox6.Items.Contains(strfolder2) Then
                        newTvFolders.Add(strfolder2)
                    End If
                End If
            Next
        Next
    End Sub

    Public Sub tv_ShowScrape()
        If Not bckgrnd_tvshowscraper.IsBusy Then
            If newTvFolders.Count > 0 Then
                ToolStripStatusLabel5.Text = "Scraping TV Shows, " & newTvFolders.Count & " remaining"
                ToolStripStatusLabel5.Visible = True
            End If
            Dim selectedLang As String = If(Preferences.tvshow_useXBMC_Scraper, ComboBox_TVDB_Language.Text, Preferences.TvdbLanguageCode)
            Dim args As TvdbArgs = New TvdbArgs("", selectedLang)
            bckgrnd_tvshowscraper.RunWorkerAsync(args) ' Even if no shows scraped, saves tvcache and updates treeview in RunWorkerComplete
        End If
    End Sub

    Private Sub cbImdbgetTMDBActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImdbgetTMDBActor.Click
        Try
            Preferences.TmdbActorsImdbScrape = cbImdbgetTMDBActor.Checked 
            'If cbImdbgetTMDBActor.CheckState = CheckState.Checked Then
            '    Preferences.TmdbActorsImdbScrape = True
            'Else 
            '    Preferences.TmdbActorsImdbScrape = False
            'End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub 

    Private Sub cbImdbPrimaryPlot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImdbPrimaryPlot.Click
        Try
            Preferences.ImdbPrimaryPlot = cbImdbPrimaryPlot.Checked 
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox9_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox9.SelectedIndexChanged
        Try
            Preferences.imdbmirror = ListBox9.SelectedItem
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mov_ThumbNailUrlsSet()
        If IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 0
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 1
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 2
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 3
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 4
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 5
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 6
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Preferences.nfoposterscraper = 7
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 8
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 9
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 10
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 11
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 12
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 13
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 14
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Preferences.nfoposterscraper = 15
        End If
    End Sub

    Private Function util_RegexValidate(ByVal regexs As String)
        Try
            Dim test As Match
            test = Regex.Match("", regexs)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function tv_LanguageCheck(ByVal id As String, ByVal language As String)
        Try
            Dim languagecode As String = language
            Dim url As String = "http://thetvdb.com/api/6E82FED600783400/series/" & id & "/" & languagecode & ".xml"
            Dim websource(10000)
            Dim urllinecount As Integer = 0
            Try
                Dim wrGETURL As WebRequest
                wrGETURL = WebRequest.Create(url)
                Dim myProxy As New WebProxy("myproxy", 80)
                myProxy.BypassProxyOnLocal = True
                Dim objStream As Stream
                objStream = wrGETURL.GetResponse.GetResponseStream()
                Dim objReader As New StreamReader(objStream)
                Dim sLine As String = ""
                urllinecount = 0

                Do While Not sLine Is Nothing
                    urllinecount += 1
                    sLine = objReader.ReadLine
                    If Not sLine Is Nothing Then
                        websource(urllinecount) = sLine
                    End If
                Loop
                objReader.Close()
                objStream.Close()
                urllinecount -= 1

            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                'MsgBox(ex.ToString)
            End Try
            For f = 1 To urllinecount
                If websource(f).IndexOf("<Language>") <> -1 Then
                    websource(f) = websource(f).Replace("<Language>", "")
                    websource(f) = websource(f).Replace("</Language>", "")
                    websource(f) = websource(f).Replace("  ", "")
                    If websource(f).ToLower = languagecode Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Next
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
        Return "Error"
    End Function

    Private Sub ProfilesToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ProfilesToolStripMenuItem.DropDownItemClicked
        Try
            generalprefschanged = False

            Preferences.ConfigSave()

            For Each prof In profileStruct.ProfileList
                If prof.ProfileName = e.ClickedItem.Text Then
                    workingProfile.actorcache = prof.actorcache
                    workingProfile.DirectorCache = prof.DirectorCache
                    workingProfile.config = prof.config
                    workingProfile.filters = prof.filters
                    workingProfile.Genres = prof.Genres 
                    workingProfile.moviecache = prof.moviecache
                    workingProfile.MusicVideoCache = prof.MusicVideoCache 
                    workingProfile.profilename = prof.profilename
                    workingProfile.regexlist = prof.regexlist
                    workingProfile.tvcache = prof.tvcache
                    workingProfile.MovieSetCache = prof.MovieSetCache 
                    Call util_ProfileSetup()
                End If
            Next
            If e.ClickedItem.Text <> workingProfile.profilename Then
                Exit Sub
            End If
            For Each item In ProfilesToolStripMenuItem.DropDownItems
                If item.text = workingProfile.profilename Then
                    With item
                        item.checked = True
                    End With
                Else
                    item.checked = False
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub util_ProfileSetup()

        Dim mess As New frmMessageBox(" Please Wait", , "Loading Profile")
        mess.Show()
        mess.Refresh()
        Application.DoEvents()
        Me.Enabled = False
        If IO.File.Exists(workingProfile.config) Then
            Preferences.moviesets.Clear()
            Me.util_ConfigLoad()
        Else
            Call SetUpPreferences()
        End If

        util_MainFormTitleUpdate()  'creates & shows new title to Form1, also includes current profile name

        If Not IO.File.Exists(workingProfile.moviecache) Or Preferences.startupCache = False Then
            mov_RebuildMovieCaches
        Else
            oMovies.LoadCaches
        End If

        If IO.File.Exists(workingProfile.filters) Then
            Call util_FilterLoad()
        End If

        If IO.File.Exists(workingProfile.Genres) Then
            Call util_GenreLoad()
        End If

        If Not IO.File.Exists(workingProfile.tvcache) Or Preferences.startupCache = False Then
            Call tv_CacheRefresh()
        Else
            Call tv_CacheLoad()
        End If

        If IO.File.Exists(workingProfile.MusicVideoCache) Then
            Call UcMusicVideo1.MusicVideoCacheLoad()
        End If


        'If Preferences.maximised = True Then
        '    Me.WindowState = FormWindowState.Maximized
        'Else
        '    If Preferences.locx <> 0 Then
        '        Me.Location = New Point(Preferences.locx, Preferences.locy)
        '    End If
        '    If Preferences.locy <> 0 Then
        '        Me.Location = New Point(Preferences.locx, Preferences.locy)
        '    End If
        '    If Preferences.formheight <> 0 And Preferences.formwidth <> 0 Then
        '        Me.Width = Preferences.formwidth
        '        Me.Height = Preferences.formheight
        '    End If
        'End If

        Me.Refresh()
        Application.DoEvents()

        If Preferences.splt5 = 0 Then
            Dim tempint As Integer = SplitContainer1.Height
            tempint = tempint / 4
            tempint = tempint * 3
            If tempint > 275 Then
                Preferences.splt5 = tempint
            Else
                Preferences.splt5 = 275
            End If
        End If

        If Preferences.startuptab = 0 Then
            SplitContainer1.SplitterDistance = Preferences.splt1
            SplitContainer2.SplitterDistance = Preferences.splt2
            SplitContainer5.SplitterDistance = Preferences.splt5
            TabLevel1.SelectedIndex = 1
            SplitContainer3.SplitterDistance = Preferences.splt3
            SplitContainer4.SplitterDistance = Preferences.splt4
            _tv_SplitContainer.SplitterDistance = Preferences.splt6 
            TabLevel1.SelectedIndex = 0
        Else
            SplitContainer1.SplitterDistance = Preferences.splt1
            SplitContainer2.SplitterDistance = Preferences.splt2
            SplitContainer5.SplitterDistance = Preferences.splt5
            TabLevel1.SelectedIndex = 1
            SplitContainer3.SplitterDistance = Preferences.splt3
            SplitContainer4.SplitterDistance = Preferences.splt4
            _tv_SplitContainer.SplitterDistance = Preferences.splt6
        End If
        SplitContainer1.IsSplitterFixed = False
        SplitContainer2.IsSplitterFixed = False
        SplitContainer3.IsSplitterFixed = False
        SplitContainer4.IsSplitterFixed = False
        SplitContainer5.IsSplitterFixed = False
        _tv_SplitContainer.IsSplitterFixed = False
        'Dim tempboolean As Boolean = UrlIsValid("http://thetvdb.com/")

        Try
            If cbMovieDisplay_MovieSet.Items.Count <> Preferences.moviesets.Count Then
                cbMovieDisplay_MovieSet.Items.Clear()
                For Each mset In Preferences.moviesets
                    cbMovieDisplay_MovieSet.Items.Add(mset)
                Next
            End If
            If workingMovieDetails.fullmoviebody.movieset.MovieSetName <> "-None-" Then
                For Each mset In Preferences.moviesets
                    cbMovieDisplay_MovieSet.Items.Add(mset)
                Next
                For te = 0 To cbMovieDisplay_MovieSet.Items.Count - 1
                    If cbMovieDisplay_MovieSet.Items(te) = workingMovieDetails.fullmoviebody.movieset.MovieSetName Then
                        cbMovieDisplay_MovieSet.SelectedIndex = te
                        Exit For
                    End If
                Next
                'setsTxt.Text = workingMovieDetails.fullmoviebody.movieset
            End If
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
        mov_VideoSourcePopulate()
        ep_VideoSourcePopulate()
        Try
            TabControl2.SelectedIndex = 0
            currentTabIndex = 0
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
        Try
            TabControl3.SelectedIndex = 0
            tvCurrentTabIndex = 0
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try


        Me.Enabled = True
        mess.Close()
    End Sub

    Private Sub mov_SwitchRuntime()
        If workingMovieDetails Is Nothing Then Exit Sub
        If Preferences.enablehdtags = True And workingMovieDetails.filedetails.filedetails_video.DurationInSeconds <> Nothing And Not displayRuntimeScraper Then
            runtimetxt.Text = Utilities.cleanruntime(workingMovieDetails.filedetails.filedetails_video.DurationInSeconds.Value) & " min"
            runtimetxt.Enabled = False
        Else
            runtimetxt.Text = workingMovieDetails.fullmoviebody.runtime
            runtimetxt.Enabled = True
        End If
    End Sub

    Private Sub SearchForNewMoviesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchForNewMoviesToolStripMenuItem.Click
        SearchForNew
    End Sub
    
    Public Function Yield(yielding As Boolean) As Boolean
        If yielding Then
            Application.DoEvents
            Return _yield
        End If
        Return False
    End Function

    Private Sub util_FontSetup()
        If Preferences.font <> Nothing Then
            If Preferences.font <> "" Then
                Try
                    Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                    Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Preferences.font), System.Drawing.Font)
                    genretxt.Font = newFont
                    premiertxt.Font = newFont 
                    certtxt.Font = newFont
                    directortxt.Font = newFont
                    creditstxt.Font = newFont
                    outlinetxt.Font = newFont
                    runtimetxt.Font = newFont
                    studiotxt.Font = newFont
                    countrytxt.Font = newFont
                    taglinetxt.Font = newFont
                    cbMovieDisplay_Actor.Font = newFont
                    roletxt.Font = newFont
                    pathtxt.Font = newFont
                    'CheckedListBox1.Font = newFont
                    TextBox34.Font = newFont
                    DataGridViewMovies.Font = newFont
                    plottxt.Font = newFont
                    txtStars.Font = newFont
                    'titletxt.Font = newFont
                    'setsTxt.Font = newFont
                    cbMovieDisplay_MovieSet.Font = newFont
                    cmbxEpActor.Font = newFont
                    TvTreeview.Font = newFont
                    tbEpRole.Font = newFont
                    tb_EpDirector.Font = newFont
                    tb_EpCredits.Font = newFont
                    tb_EpPlot.Font = newFont
                    tb_EpAired.Font = newFont
                    tb_EpRating.Font = newFont
                    tb_EpPath.Font = newFont
                    tb_EpFilename.Font = newFont
                    tb_ShPlot.Font = newFont
                    cbTvActor.Font = newFont
                    cbTvActorRole.Font = newFont
                    tb_ShRunTime.Font = newFont
                    tb_ShStudio.Font = newFont
                    tb_ShPremiered.Font = newFont
                    tb_ShRating.Font = newFont
                    tb_ShTvdbId.Font = newFont
                    tb_ShGenre.Font = newFont
                    tb_ShImdbId.Font = newFont
                    tb_ShCert.Font = newFont

                    ratingtxt.Font = newFont
                    votestxt.Font = newFont
                    top250txt.Font = newFont
                    imdbtxt.Font = newFont
                    cbFilterGeneral.Font = newFont
                    cbFilterGenre.Font = newFont
                    cbFilterSet.Font = newFont
                    cbFilterActor.Font = newFont
                    cbFilterTag.Font = newFont
                    cbFilterDirector.Font = newFont
                    cbFilterSource.Font = newFont
                    cbFilterResolution.Font = newFont
                    cbFilterVideoCodec.Font = newFont
                    cbFilterSubTitleLang.Font = newFont
                    cbFilterAudioCodecs.Font = newFont
                    cbFilterAudioLanguages.Font = newFont
                    cbFilterAudioBitrates .Font = newFont
                    cbFilterAudioChannels .Font = newFont
                    cbFilterNumAudioTracks.Font = newFont
                    cbFilterCertificate   .Font = newFont
                    LabelCountFilter.Font = newFont

                    Me.Refresh()
                    Application.DoEvents()
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
            End If
        End If


    End Sub

    Private Sub langarrsetup()
        Dim strfilename As String
        Dim num_rows As Long
        Dim num_cols As Long
        Dim x As Integer
        Dim y As Integer
        strfilename = applicationPath & "\Assets\" & "test.csv"
        If File.Exists(strfilename) Then
            Dim tmpstream As StreamReader = File.OpenText(strfilename)
            Dim strlines() As String
            Dim strline() As String

            strlines = tmpstream.ReadToEnd().Split(Environment.NewLine)

            num_rows = UBound(strlines)
            strline = strlines(0).Split(",")
            num_cols = UBound(strline)

            ' Copy the data into the array.
            For x = 0 To num_rows-1
                strline = strlines(x).Split(",")
                For y = 0 To num_cols
                    langarray(x, y) = strline(y)
                Next
            Next


        End If

    End Sub

#Region "Movie Table"

'tabpage events
    Private Sub tpMoviesTable_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpMoviesTable.Enter
        MovSetsRepopulate()
        'mov_TableSetup()
    End Sub

    Private Sub tpMoviesTable_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpMoviesTable.Leave
        DataGridView1.EndEdit()
        Preferences.tableview.Clear()
        For Each column In DataGridView1.Columns
            Dim tempstring As String = String.Format("{0}|{1}|{2}|{3}", column.name, column.width, column.displayindex, column.visible)
            Preferences.tableview.Add(tempstring)
        Next
        If IsNothing(DataGridView1.SortedColumn) = False Then
            Preferences.tablesortorder = String.Format("{0} | {1}", DataGridView1.SortedColumn.HeaderText, DataGridView1.SortOrder.ToString)
            Preferences.ConfigSave()
        End If

        If DataDirty Then
            Dim tempint As Integer = MessageBox.Show("You appear to have made changes to some of your movie details." & vbCrLf & vbCrLf & "Any changes will be lost if you do not save the changes now." & "                 Do wish to save the changes?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tempint = DialogResult.Yes Then
                Call mov_TableChangesSave()
                MsgBox("Changes Saved")
            End If
        End If

        DataDirty=False
        btn_movTableSave.Enabled = DataDirty
    End Sub

'Table Setup and update
    Private Sub mov_TableViewSetup()
        Preferences.tableview.Clear()
        Preferences.tableview.Add("title|150|0|true")
        Preferences.tableview.Add("year|40|1|true")
        Preferences.tableview.Add("genre|160|2|true")
        Preferences.tableview.Add("rating|50|3|true")
        Preferences.tableview.Add("runtime|60|4|true")
        Preferences.tableview.Add("top250|89|5|false")
        Preferences.tableview.Add("source|150|6|false")
        Preferences.tableview.Add("playcount|120|7|true")
        Preferences.tableview.Add("set|150|8|true")
        Preferences.tableview.Add("certificate|100|9|false")
        Preferences.tableview.Add("sorttitle|100|10|false")
        Preferences.tableview.Add("outline|200|11|false")
        Preferences.tableview.Add("plot|200|12|false")
        Preferences.tableview.Add("id|82|13|false")
        Preferences.tableview.Add("missingdata1|115|14|false")
        Preferences.tableview.Add("fullpathandfilename|300|15|false")
        Preferences.tableview.Add("createdate|104|16|false")
    End Sub

    Private Sub mov_TableSetup()
        DataGridView1.Columns.Clear()
        If Preferences.tablesortorder = Nothing Then Preferences.tablesortorder = "Title|Ascending"
        If Preferences.tablesortorder = "" Then Preferences.tablesortorder = "Title|Ascending"
        If Preferences.tableview.Count < 17 Then
            Call mov_TableViewSetup()
        End If
        tableSets.Clear()
        For Each item In Preferences.tableview
            Dim tempdata() As String
            tempdata = item.Split("|")
            Dim newcolumn As New str_TableItems(SetDefaults)
            newcolumn.title = tempdata(0)
            newcolumn.width = Convert.ToInt32(tempdata(1))
            newcolumn.index = Convert.ToInt32(tempdata(2))
            If tempdata(3).ToLower = "true" Then
                newcolumn.visible = True
            Else
                newcolumn.visible = False
            End If
            tableSets.Add(newcolumn)
        Next

        DataGridView1.AutoGenerateColumns = False
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("movie_cache")

        Dim childchild As XmlElement

        For Each row As DataGridViewRow In DataGridViewMovies.Rows
            Dim movie As Data_GridViewMovie = row.DataBoundItem
            child = doc.CreateElement("movie")
            childchild = doc.CreateElement("filedate") : childchild.InnerText = movie.filedate : child.AppendChild(childchild)
            childchild = doc.CreateElement("missingdata1") : childchild.InnerText = movie.missingdata1.ToString : child.AppendChild(childchild)
            childchild = doc.CreateElement("filename") : childchild.InnerText = movie.filename : child.AppendChild(childchild)
            childchild = doc.CreateElement("foldername") : childchild.InnerText = movie.foldername : child.AppendChild(childchild)
            childchild = doc.CreateElement("fullpathandfilename") : childchild.InnerText = movie.fullpathandfilename : child.AppendChild(childchild)
            If movie.movieset <> Nothing Then
                If movie.movieset <> "" Then
                    childchild = doc.CreateElement("set") : childchild.InnerText = movie.movieset : child.AppendChild(childchild)
                Else
                    childchild = doc.CreateElement("set") : childchild.InnerText = "-None-" : child.AppendChild(childchild)
                End If
            Else
                childchild = doc.CreateElement("set") : childchild.InnerText = "-None-" : child.AppendChild(childchild)
            End If
            If movie.source <> Nothing And movie.source <> "" Then
                childchild = doc.CreateElement("source") : childchild.InnerText = movie.source : child.AppendChild(childchild)
            Else
                childchild = doc.CreateElement("source") : childchild.InnerText = "" : child.AppendChild(childchild)
            End If
            childchild = doc.CreateElement("genre") : childchild.InnerText = movie.genre : child.AppendChild(childchild)
            childchild = doc.CreateElement("id") : childchild.InnerText = movie.id : child.AppendChild(childchild)
            childchild = doc.CreateElement("playcount") : childchild.InnerText = movie.playcount : child.AppendChild(childchild)
            childchild = doc.CreateElement("rating") : childchild.InnerText = movie.Rating : child.AppendChild(childchild)
            childchild = doc.CreateElement("title") : childchild.InnerText = movie.title : child.AppendChild(childchild)
            childchild = doc.CreateElement("certificate") : childchild.InnerText = movie.Certificate : child.AppendChild(childchild)
            If String.IsNullOrEmpty(movie.SortOrder) Then movie.SortOrder = movie.DisplayTitle
            childchild = doc.CreateElement("outline") : childchild.InnerText = movie.outline : child.AppendChild(childchild)
            childchild = doc.CreateElement("plot") : childchild.InnerText = movie.plot : child.AppendChild(childchild)
            childchild = doc.CreateElement("sortorder") : childchild.InnerText = movie.SortOrder : child.AppendChild(childchild)

            childchild = doc.CreateElement("runtime") : childchild.InnerText = movie.runtime : child.AppendChild(childchild)
            childchild = doc.CreateElement("top250") : childchild.InnerText = movie.top250 : child.AppendChild(childchild)
            childchild = doc.CreateElement("year") : childchild.InnerText = movie.year : child.AppendChild(childchild)
            childchild = doc.CreateElement("createdate") : childchild.InnerText = movie.createdate : child.AppendChild(childchild)
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        For Each thisresult In doc("movie_cache")
            'Try
            Select Case thisresult.Name
                Case "movie"
                    Dim chi As XmlElement
                    For Each chi In thisresult.childnodes
                        If chi.Name = "runtime" Then
                            chi.InnerText = chi.InnerText.Replace("minutes", "")
                            chi.InnerText = chi.InnerText.Replace("mins", "")
                            chi.InnerText = chi.InnerText.Replace("min", "")
                            chi.InnerText = chi.InnerText.Replace(" ", "")
                            If chi.InnerText.Length = 1 Then
                                chi.InnerText = "00" & chi.InnerText
                            End If
                            If chi.InnerText.Length = 2 Then
                                chi.InnerText = "0" & chi.InnerText
                            End If
                            chi.InnerText = chi.InnerText & " min"
                        End If
                        If chi.Name = "top250" Then
                            If chi.InnerText = "" Then
                                chi.InnerText = "000" & chi.InnerText
                            End If
                            If IsNumeric(chi.InnerText) Then
                                If chi.InnerText.Length = 0 Then
                                    chi.InnerText = "000" & chi.InnerText
                                End If
                                If chi.InnerText.Length = 1 Then
                                    chi.InnerText = "00" & chi.InnerText
                                End If
                                If chi.InnerText.Length = 2 Then
                                    chi.InnerText = "0" & chi.InnerText
                                End If
                            End If
                        End If
                        If chi.Name = "playcount" Then
                            If chi.InnerText = "" OrElse chi.InnerText = "0" OrElse Not IsNumeric(chi.InnerText) Then
                                chi.InnerText = "UnWatched"
                            Else
                                chi.InnerText = "Watched"
                            End If
                        End If
                        If chi.Name = "missingdata1" Then
                            If chi.InnerText = "" Or Not IsNumeric(chi.InnerText) Then
                                chi.InnerText = "0"
                            Else
                                chi.InnerText = Movie.GetMissingDataText( Convert.ToByte(chi.InnerText) )
                            End If
                        End If
                        If chi.Name = "createdate" Then
                            If chi.InnerText = "" Or Not IsNumeric(chi.InnerText) Then
                                chi.InnerText = "0"
                            Else
                                chi.InnerText = Utilities.DecodeDateTime(chi.InnerText, Preferences.DateFormat)
                            End If
                        End If
                    Next
            End Select
        Next

        Dim movstring As String = doc.InnerXml
        Dim XMLreader2 As StringReader = New System.IO.StringReader(movstring)

        ' Create the dataset
        Dim newDS As DataSet = New DataSet
        newDS.ReadXml(XMLreader2)
        XMLreader2.Dispose()

        DataGridView1.DataSource = Nothing

        Try
            DataGridView1.DataSource = newDS.Tables(0)
        Catch
        End Try

'        DataGridView1.DataSource = DataGridViewMovies.DataSource       TO DO: Replace with this

        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.RowHeadersVisible = True
        DataGridView1.RowHeadersWidth = 25

        Dim titlecolumn As New DataGridViewColumn()
        With titlecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Title"
            .DataPropertyName = "title"
            .Name = "title"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim yearcolumn As New DataGridViewColumn()
        With yearcolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Year"
            .DataPropertyName = "year"
            .Name = "year"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        End With


        Dim idcolumn As New DataGridViewColumn()
        With idcolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "IMDB ID"
            .DataPropertyName = "id"
            .Name = "id"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .ReadOnly = True
        End With

        Dim pathcolumn As New DataGridViewColumn()
        With pathcolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Full Path"
            .DataPropertyName = "fullpathandfilename"
            .Name = "fullpathandfilename"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .ReadOnly = True
        End With

        Dim genrecolumn As New DataGridViewColumn()
        With genrecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Genre"
            .DataPropertyName = "genre"
            .Name = "genre"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .ToolTipText = "Separate Genre's with a ""Space/Space"", ie: Action / Horror"
        End With

        Dim ratingcolumn As New DataGridViewColumn()
        With ratingcolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Rating"
            .DataPropertyName = "rating"
            .Name = "rating"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        End With

        Dim outlinecolumn As New DataGridViewColumn()
        With outlinecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Outline"
            .DataPropertyName = "outline"
            .Name = "outline"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim plotcolumn As New DataGridViewColumn()
        With plotcolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Plot"
            .DataPropertyName = "plot"
            .Name = "plot"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim sorttitlecolumn As New DataGridViewColumn()
        With sorttitlecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Sort Title"
            .DataPropertyName = "sortorder"
            .Name = "sorttitle"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim top250column As New DataGridViewColumn()
        With top250column
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Top 250"
            .DataPropertyName = "top250"
            .Name = "top250"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim watchedcolumn As New DataGridViewComboBoxColumn()
        watchedcolumn.Items.Add("UnWatched")
        watchedcolumn.Items.Add("Watched")
        With watchedcolumn
            .HeaderText = "Watched"
            .Name = "playcount"
            .DataPropertyName = "playcount"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .DefaultCellStyle.NullValue = ""
        End With

        Dim sourcecolumn As New DataGridViewComboBoxColumn()
        For Each src In Preferences.releaseformat
            sourcecolumn.Items.Add(src)
        Next
        With sourcecolumn
            .HeaderText = "Source"
            .Name = "source"
            .DataPropertyName = "source"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .DefaultCellStyle.NullValue = ""
        End With

        Dim runtimecolumn As New DataGridViewColumn()
        With runtimecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Runtime"
            .Name = "runtime"
            .DataPropertyName = "runtime"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim certificatecolumn As New DataGridViewColumn()
        With certificatecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Certificate"
            .Name = "certificate"
            .DataPropertyName = "certificate"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        Dim setscolumn As New DataGridViewComboBoxColumn
        For Each sets In Preferences.moviesets
            setscolumn.Items.Add(sets)
        Next
        With setscolumn
            .HeaderText = "Sets"
            .Name = "set"
            .DataPropertyName = "set"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .DefaultCellStyle.NullValue = "-None-"
        End With

        Dim artcolumn As New DataGridViewColumn()
        With artcolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Missing"
            .Name = "missingdata1"
            .DataPropertyName = "missingdata1"
            .SortMode = DataGridViewColumnSortMode.Automatic
            .ReadOnly = True
        End With

        Dim createdatecolumn As New DataGridViewColumn()
        With createdatecolumn
            Dim oCell As DataGridViewCell = New DataGridViewTextBoxCell
            .CellTemplate = oCell
            .HeaderText = "Date Added"
            .Name = "createdate"
            .DataPropertyName = "createdate"
            .SortMode = DataGridViewColumnSortMode.Automatic
        End With

        For f = 0 To 16
            For Each col In tableSets
                If col.index = f Then
                    Select Case col.title
                        Case "title"
                            titlecolumn.Width = col.width
                            titlecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, titlecolumn)
                            Exit For
                        Case "year"
                            yearcolumn.Width = col.width
                            yearcolumn.Visible =  col.visible
                            DataGridView1.Columns.Insert(f, yearcolumn)
                            Exit For
                        Case "sorttitle"
                            sorttitlecolumn.Width = col.width
                            sorttitlecolumn.Visible =  col.visible
                            DataGridView1.Columns.Insert(f, sorttitlecolumn)
                            Exit For
                        Case "genre"
                            genrecolumn.Width = col.width
                            genrecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, genrecolumn)
                            Exit For
                        Case "rating"
                            ratingcolumn.Width = col.width
                            ratingcolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, ratingcolumn)
                            Exit For
                        Case "runtime"
                            runtimecolumn.Width = col.width
                            runtimecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, runtimecolumn)
                            Exit For
                        Case "top250"
                            top250column.Width = col.width
                            top250column.Visible = col.visible
                            DataGridView1.Columns.Insert(f, top250column)
                            Exit For
                        Case "certificate"
                            certificatecolumn.Width = col.width
                            certificatecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, certificatecolumn)
                        Case "source"
                            sourcecolumn.Width = col.width
                            sourcecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, sourcecolumn)
                            Exit For
                        Case "playcount"
                            watchedcolumn.Width = col.width
                            watchedcolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, watchedcolumn)
                            Exit For
                        Case "set"
                            setscolumn.Width = col.width
                            setscolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, setscolumn)
                            Exit For
                        Case "outline"
                            outlinecolumn.Width = col.width
                            outlinecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, outlinecolumn)
                            Exit For
                        Case "plot"
                            plotcolumn.Width = col.width
                            plotcolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, plotcolumn)
                            Exit For
                        Case "id"
                            idcolumn.Width = col.width
                            idcolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, idcolumn)
                            Exit For
                        Case "missingdata1"
                            artcolumn.Width = col.width
                            artcolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, artcolumn)
                            Exit For
                        Case "fullpathandfilename"
                            pathcolumn.Width = col.width
                            pathcolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, pathcolumn)
                            Exit For
                        Case "createdate"
                            createdatecolumn.Width = col.width
                            createdatecolumn.Visible = col.visible
                            DataGridView1.Columns.Insert(f, createdatecolumn)
                            Exit For
                    End Select
                End If
            Next
        Next f

        Dim sortheader As String
        Dim sortord As String
        Dim tempdata2() As String
        tempdata2 = Preferences.tablesortorder.Split("|")
        sortheader = tempdata2(0)
        sortord = tempdata2(1)

        For Each col In DataGridView1.Columns
            If col.headertext = sortheader Then
                If sortord.ToLower.IndexOf("desc") <> -1 Then
                    DataGridView1.Sort(DataGridView1.Columns(col.index), ListSortDirection.Descending)
                Else
                    DataGridView1.Sort(DataGridView1.Columns(col.index), ListSortDirection.Ascending)
                End If
            End If
        Next

        For Each tempRow As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.Rows
            For Each tempCell As Windows.Forms.DataGridViewCell In tempRow.Cells
                If tempCell.Value = "Fanart" Or tempCell.Value = "Poster" Or tempCell.Value = "Poster & Fanart" Then
                'If TypeName(tempCell.Value) = "Byte" AndAlso tempCell.Value > 0 then
                    tempCell.Style.BackColor = Color.Red
                End If
            Next
        Next

        Call mov_TableEditSetup()
        Try
            For f = 0 To DataGridView1.Rows.Count-1
                If DataGridView1.Rows(f).Cells("fullpathandfilename").Value = workingMovieDetails.fileinfo.fullpathandfilename Then
                    DataGridView1.ClearSelection()
                    'DataGridView1.CurrentCell = DataGridView1.Rows(f).Cells(NFO_INDEX)
                    'DataGridView1.Rows(f).Selected = True
                    DataGridView1.FirstDisplayedScrollingRowIndex = f
                    Exit For
                End If
            Next
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub
    
    Private Sub mov_TableEditSetup()
        mov_TableEditDGV.Columns.Clear()
        mov_TableEditDGV.AutoGenerateColumns = False
        mov_TableEditDGV.DataSource = Nothing
        For Each dgvCol As DataGridViewColumn In DataGridView1.columns

            Dim dgvNewCol As New DataGridViewColumn
            dgvNewCol = DirectCast(dgvCol.Clone(), DataGridViewColumn)
            dgvNewCol.CellTemplate = DirectCast(dgvCol.CellTemplate, DataGridViewCell)
            If dgvNewCol.Name = "plot" Or dgvNewCol.Name = "outline" Or dgvNewCol.Name = "id" Or dgvNewCol.Name = "missingdata1" or dgvNewCol.Name = "fullpathandfilename"Then
                dgvNewCol.ReadOnly = True
                dgvNewCol.DefaultCellStyle.BackColor = System.Drawing.Color.gray
            End If
            mov_TableEditDGV.Columns.Add(dgvNewCol)
        Next

        'add "Unchanged"
        Dim src_add As DataGridViewComboBoxColumn = DirectCast(Me.mov_TableEditDGV.Columns("source"), DataGridViewComboBoxColumn)
        src_add.Items.Insert(0, "UnChanged")
        Dim set_add As DataGridViewComboBoxColumn = DirectCast(Me.mov_TableEditDGV.Columns("set"), DataGridViewComboBoxColumn)
        set_add.Items.Insert(0, "UnChanged")
        Dim play_add As DataGridViewComboBoxColumn = DirectCast(Me.mov_TableEditDGV.Columns("playcount"), DataGridViewComboBoxColumn)
        play_add.Items.Insert(0, "UnChanged")

        mov_TableEditDGV.RowHeadersWidth = DataGridView1.RowHeadersWidth
        mov_TableEditDGV.ClearSelection 
        mov_TableEditDGV.ScrollBars = ScrollBars.None
        Me.mov_TableEditDGV.DefaultCellStyle.SelectionBackColor = Me.mov_TableEditDGV.DefaultCellStyle.BackColor
        Me.mov_TableEditDGV.DefaultCellStyle.SelectionForeColor = Me.mov_TableEditDGV.DefaultCellStyle.ForeColor

        Dim emptydata As String = FillEmptydoc().InnerXml 
        Dim XMLreader3 As StringReader = New System.IO.StringReader(emptydata)

        ' Create the Empty dataset
        Dim emptydataset As DataSet = New DataSet
        emptydataset.ReadXml(XMLreader3)
        XMLreader3.Dispose()
        mov_TableEditDGV.DataSource = emptydataset.tables(0)
        mov_TableEditDGV.CurrentRow.Selected = false
    End Sub

    Private Sub mov_TableChangesSave()

        DataDirty=False

        frmSplash2.Text = "Saving Table Changes..."
        frmSplash2.Label1.Text = "Saving Movie Data....."
        frmSplash2.Label1.Visible = True
        frmSplash2.Label2.Visible = True
        frmSplash2.ProgressBar1.Visible = True
        frmSplash2.ProgressBar1.Maximum = DataGridView1.Rows.Count
        frmSplash2.Show()

        Application.DoEvents

        Dim progcount     As Integer = 0
        Dim changed       As Boolean
        Dim oCachedMovie  As ComboList

        For Each gridrow As DataGridViewRow In DataGridView1.Rows
            changed    = False
            progcount += 1
            frmSplash2.ProgressBar1.Value = progcount
            frmSplash2.Label2.Text = gridrow.Cells("Title").Value
            oCachedMovie = oMovies.FindCachedMovie( gridrow.Cells("fullpathandfilename").Value )

            Try
                If oCachedMovie.title <> gridrow.Cells("Title").Value Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.outline <> gridrow.Cells("Outline").Value Then  changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.genre <> gridrow.Cells("genre").Value Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.rating <> gridrow.Cells("rating").Value Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                'If gridrow.Cells("playcount").Value = True Then
                Dim playstatus As String = gridrow.Cells("playcount").EditedFormattedValue
                Dim plycnt As Integer = 0
                If playstatus.tolower = "watched" Then plycnt = 1
                If oCachedMovie.playcount <> plycnt Then Changed = True
                'If gridrow.Cells("playcount").EditedFormattedValue = "Watched" Then
                '    If oCachedMovie.playcount <= 0 Then
                '        changed = True
                '    End If
                'Else
                '    If oCachedMovie.playcount > 0 Then
                '        changed = True
                '    End If
                'End If
            Catch
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.sortorder <> gridrow.Cells("sorttitle").Value Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.year <> gridrow.Cells("year").Value Then
                    If IsNumeric(gridrow.Cells("year").Value) Then changed = True
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If Convert.ToInt32(oCachedMovie.top250) <> Convert.ToInt32(gridrow.Cells("top250").Value) Then
                    If IsNumeric(gridrow.Cells("top250").Value) Then changed = True
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.Certificate <> gridrow.Cells("certificate").Value Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Dim runtime        As String=""
            Dim intRunTime     As Integer
            Dim runTimeChanged As Boolean
            Dim newRunTime     As String=""

            Try
                runtime = gridrow.Cells("runtime").Value
                runtime = runtime.Replace("min", "")
                runtime = runtime.Trim(" ")
                If IsNumeric(runtime) Then
                    intRunTime = Convert.ToInt32(runtime)
                    newRunTime = intRunTime.ToString & " min"
                    If oCachedMovie.runtime <> newRunTime Then changed = True : runTimeChanged = True
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.source <> If(IsDBNull(gridrow.Cells("source").Value), "", gridrow.Cells("source").Value) Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            Try
                If oCachedMovie.MovieSet <> If(IsDBNull(gridrow.Cells("set").Value), "", gridrow.Cells("set").Value) Then changed = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try

            If changed And IO.File.Exists(oCachedMovie.fullpathandfilename) Then

                Dim oMovie As Movie = oMovies.LoadMovie(oCachedMovie.fullpathandfilename)

                If IsNothing(oMovie) Then Continue For


                Try
                    oCachedMovie.genre = gridrow.Cells("genre").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    oCachedMovie.title = gridrow.Cells("title").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    oCachedMovie.year = gridrow.Cells("year").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    oCachedMovie.sortorder = gridrow.Cells("sorttitle").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    oCachedMovie.rating = gridrow.Cells("rating").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
'                Try
'                    oCachedMovie.outline = gridrow.Cells("outline").Value
'                Catch ex As Exception
'#If SilentErrorScream Then
'                    Throw ex
'#End If
'                End Try
                ' Because plot is truncated to 100 chars to save moviecache.xml length, we don't want the user to overwrite the real plot
                '                            Try
                '                                oCachedMovie.plot = gridrow.Cells("plot").Value
                '                            Catch ex As Exception
                '#If SilentErrorScream Then
                '                                Throw ex
                '#End If
                '                            End Try
                Try
                    oCachedMovie.source = If(IsDBNull(gridrow.Cells("source").Value), "", gridrow.Cells("source").Value)
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try

                Try
                    'oCachedMovie.director = gridrow.Cells("director").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try

                Try
                    oCachedMovie.MovieSet = If(IsDBNull(gridrow.Cells("set").Value), "", gridrow.Cells("set").Value)
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    oCachedMovie.top250 = Convert.ToInt32(gridrow.Cells("top250").Value).ToString
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    If gridrow.Cells("playcount").EditedFormattedValue = "Watched" Then
                        'If Convert.ToInt32(oCachedMovie.playcount) > 0 Then
                        'Else
                            oCachedMovie.playcount = "1"
                        'End If
                    Else
                        oCachedMovie.playcount = "0"
                    End If
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Try
                    oCachedMovie.Certificate = gridrow.Cells("certificate").Value
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try

                If runTimeChanged Then
                    oMovie.ScrapedMovie.fullmoviebody.runtime = newRunTime
                End If
                oMovie.ScrapedMovie.fullmoviebody.title = oCachedMovie.title
                oMovie.ScrapedMovie.fullmoviebody.year = oCachedMovie.year
                oMovie.ScrapedMovie.fullmoviebody.playcount = oCachedMovie.playcount
                oMovie.ScrapedMovie.fullmoviebody.genre = oCachedMovie.genre
                'oMovie.ScrapedMovie.fullmoviebody.outline = oCachedMovie.outline
                'oMovie.ScrapedMovie.fullmoviebody.plot = oCachedMovie.plot
                oMovie.ScrapedMovie.fullmoviebody.rating = oCachedMovie.rating
                oMovie.ScrapedMovie.fullmoviebody.source = oCachedMovie.source
                oMovie.ScrapedMovie.fullmoviebody.movieset = oCachedMovie.MovieSet
                oMovie.ScrapedMovie.fullmoviebody.sortorder = oCachedMovie.sortorder
                oMovie.ScrapedMovie.fullmoviebody.top250 = oCachedMovie.top250
                oMovie.ScrapedMovie.fullmoviebody.director = oCachedMovie.director 
                Dim checkmpaa = oCachedMovie.Certificate 
                If checkmpaa <> "" AndAlso Not checkmpaa.ToLower.Contains("rated") Then
                    checkmpaa = "Rated " & checkmpaa
                End If
                oMovie.ScrapedMovie.fullmoviebody.mpaa = checkmpaa
                oMovie.AssignMovieToCache
                oMovie.SaveNFO
                oMovie.UpdateMovieCache
            End If
        Next

        oMovies.SaveMovieCache
        
        UpdateFilteredList

        frmSplash2.Hide

        Application.DoEvents
        Me.BringToFront
    End Sub

    Private Sub mov_TableUpdate()
        Dim changed As Boolean = False
        For Each row In DataGridView1.SelectedRows
            If mov_TableEditDGV.Columns("title").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("title").Value <> "" Then
                row.cells("title").value = mov_TableEditDGV.Rows(0).Cells("title").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("top250").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("top250").Value <> "" Then
                row.cells("top250").value = mov_TableEditDGV.Rows(0).Cells("top250").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("year").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("year").Value <> "" Then
                row.cells("year").value = mov_TableEditDGV.Rows(0).Cells("year").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("sorttitle").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("sorttitle").Value <> "" Then
                row.cells("sorttitle") = mov_TableEditDGV.Rows(0).Cells("sorttitle").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("runtime").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("runtime").Value <> "" Then
                row.cells("runtime").value = mov_TableEditDGV.Rows(0).Cells("runtime").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("rating").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("rating").Value <> "" Then
                row.cells("rating").value = mov_TableEditDGV.Rows(0).Cells("rating").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("certificate").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("certificate").Value <> "" Then
                row.cells("certificate").value = mov_TableEditDGV.Rows(0).Cells("certificate").Value : changed = True
            End If
            ''Table View is not the best location to edit Outline, Plot, IMDB Id.
            'If mov_TableEditDGV3.Columns("outline").Visible AndAlso mov_TableEditDGV3.Rows(0).Cells("outline").Value <> "" Then
            '    row.cells("outline").value = mov_TableEditDGV3.Rows(0).Cells("outline").Value : changed = True
            'End If
            'If mov_TableEditDGV3.Columns("plot").Visible AndAlso mov_TableEditDGV3.Rows(0).Cells("plot").Value <> "" Then
            '    row.cells("plot").value = mov_TableEditDGV3.Rows(0).Cells("plot").Value : changed = True
            'End If

            If mov_TableEditDGV.Columns("genre").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("genre").Value <> "" Then
                row.cells("genre").value = mov_TableEditDGV.Rows(0).Cells("genre").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("source").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("source").Value <> "UnChanged" Then
                row.cells("source").value = mov_TableEditDGV.Rows(0).Cells("source").Value : changed = True
            End If
            If mov_TableEditDGV.Columns("playcount").Visible AndAlso mov_TableEditDGV.Rows(0).Cells("playcount").Value <> "UnChanged" Then
                If mov_TableEditDGV.Rows(0).Cells("playcount").Value = "Watched" Then
                    row.cells("playcount").value = "Watched"
                Else
                    row.cells("playcount").value = "UnWatched"
                End If
                changed = True
            End If
            If mov_TableEditDGV.Columns("set").Visible AndAlso (IsDBNull(mov_TableEditDGV.Rows(0).Cells("set").Value) OrElse mov_TableEditDGV.Rows(0).Cells("set").Value <> "UnChanged") Then
                row.cells("set").value = If(IsDBNull(mov_TableEditDGV.Rows(0).Cells("set").Value), Nothing, mov_TableEditDGV.Rows(0).Cells("set").Value) : changed = True
            End If
        Next
        DataDirty = changed
        btn_movTableSave.Enabled = changed
    End Sub

    Private Function FillEmptyDoc() As XmlDocument
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("movie_cache")
        Dim childchild As XmlElement
        child = doc.CreateElement("movie")
        childchild = doc.CreateElement("filedate") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("missingdata1") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("filename") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("foldername") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("fullpathandfilename") : childchild.InnerText = "Not Editable in TableView" : child.AppendChild(childchild)
        childchild = doc.CreateElement("set") : childchild.InnerText = "UnChanged" : child.AppendChild(childchild)
        childchild = doc.CreateElement("source") : childchild.InnerText = "UnChanged" : child.AppendChild(childchild)
        childchild = doc.CreateElement("genre") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("id") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("playcount") : childchild.InnerText = "UnChanged" : child.AppendChild(childchild)
        childchild = doc.CreateElement("rating") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("title") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("certificate") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("outline") : childchild.InnerText = "Not Editable in TableView" : child.AppendChild(childchild)
        childchild = doc.CreateElement("plot") : childchild.InnerText = "Not Editable in TableView" : child.AppendChild(childchild)
        childchild = doc.CreateElement("sortorder") : childchild.InnerText = "" : child.AppendChild(childchild)

        'childchild = doc.CreateElement("titleandyear") : childchild.InnerText = movie.titleandyear : child.AppendChild(childchild)

        childchild = doc.CreateElement("runtime") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("top250") : childchild.InnerText = "" : child.AppendChild(childchild)
        childchild = doc.CreateElement("year") : childchild.InnerText = "" : child.AppendChild(childchild)
        root.AppendChild(child)
        doc.AppendChild(root)
        Return doc
    End Function

'Table buttons
    Private Sub btnTableColumnsSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_movTableColumnsSelect.Click
        Try
            Dim frm As New frmConfigureTableColumns
            frm.Init()
            frm.ShowDialog()
            mov_TableSetup()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_movTableSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_movTableSave.Click
        Try
            Call mov_TableChangesSave()
            MsgBox("Changes Saved")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_movTableApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_movTableApply.Click
        Try
            Call mov_TableUpdate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Table - DataGridView1 events
    Private Sub DataGridView1_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged
        Try
            mov_TableEditDGV.Columns(e.Column.Index).Width = e.Column.Width
            Dim offSetValue As Integer = DataGridView1.HorizontalScrollingOffset
            mov_TableEditDGV.HorizontalScrollingOffset = offSetValue
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub dataGridViews1_Scroll(sender As Object, e As ScrollEventArgs) Handles DataGridView1.Scroll
        Try
	        Dim offSetValue As Integer = DataGridView1.HorizontalScrollingOffset
		    mov_TableEditDGV.HorizontalScrollingOffset = offSetValue
	    Catch
	    End Try

	    DataGridView1.Invalidate()
End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Try
            Dim ColIndexFromMouseDown = DataGridView1.HitTest(e.X, e.Y).ColumnIndex 

            If ColIndexFromMouseDown < 0 Then Exit Sub

            mov_TableColumnName = DataGridView1.Columns(ColIndexFromMouseDown).Name

            'Dim hti As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            'If e.Button = Windows.Forms.MouseButtons.Right Then
            '    If DataGridView1.SelectedRows.Count < 2 Then

            '        If hti.Type = DataGridViewHitTestType.Cell Then

            '            If Not DataGridView1.Rows(hti.RowIndex).Selected Then

            '                ' User right clicked a row that is not selected, so throw away all other selections and select this row

            '                DataGridView1.ClearSelection()

            '                DataGridView1.Rows(hti.RowIndex).Selected = True
            '            End If
            '        End If

            '    End If
            'ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
            '    DataGridView1.Rows(hti.RowIndex).Selected = True
            'End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DataGridView1_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DataGridView1.ColumnDisplayIndexChanged
        Dim ColNewIndex = DataGridView1.Columns(mov_TableColumnName).DisplayIndex 
        mov_TableEditDGV.Columns(mov_TableColumnName).DisplayIndex = ColNewIndex 
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged 
        Dim MultiRowsSelected As Boolean = DataGridView1.SelectedRows.Count > 1
        If MultiRowsSelected Then
            mov_TableEditDGV.Visible = True
            lbl_movTableEdit.Visible = True
            lbl_movTableMulti.Visible = True
            btn_movTableApply.Visible = True
        Else
            mov_TableEditDGV.Visible = False
            lbl_movTableEdit.Visible = False
            lbl_movTableMulti.Visible = False
            btn_movTableApply.Visible = False
        End If
    End Sub

    Private Sub DataGridView1_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Sorted
        Try
            For Each tempRow As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.Rows
                For Each tempCell As Windows.Forms.DataGridViewCell In tempRow.Cells
                    If tempCell.Value = "Fanart" Or tempCell.Value = "Poster" Or tempCell.Value = "Poster & Fanart" Then
                        tempCell.Style.BackColor = Color.Red
                    End If
                Next
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
    Private Sub mov_TableEditDGV_CellClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles mov_TableEditDGV.CellClick 
        Try
            mov_TableEditDGV.CurrentRow.Selected = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        DataDirty = True
        btn_movTableSave.Enabled = DataDirty
    End Sub

'Table context toolstrips
    Private Sub MarkAllSelectedAsWatchedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarkAllSelectedAsWatchedToolStripMenuItem.Click
        Try
            Dim selectedrowindex As New List(Of Integer)
            For Each row in DataGridView1.SelectedRows
                selectedrowindex.Add(row.index)
            Next
            If selectedrowindex.Count = 0 Then selectedrowindex.Add(DataGridView1.CurrentRow.Index)
            DataGridView1.ClearSelection()            
            DataGridView1.CurrentCell = DataGridView1.Rows(selectedrowindex.Item(0)).Cells(0)
            For Each row In selectedrowindex
                DataGridView1.rows(row).Selected = True
                DataGridView1.rows(row).Cells("playcount").Value = "Watched"
            Next
            DataDirty = True
            btn_movTableSave.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub MarkAllSelectedAsUnWatchedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarkAllSelectedAsUnWatchedToolStripMenuItem.Click
        Try
            Dim selectedrowindex As New List(Of Integer)
            For Each row in DataGridView1.SelectedRows
                selectedrowindex.Add(row.index)
            Next
            If selectedrowindex.Count = 0 Then selectedrowindex.Add(DataGridView1.CurrentRow.Index)
            DataGridView1.ClearSelection()            
            DataGridView1.CurrentCell = DataGridView1.Rows(selectedrowindex.Item(0)).Cells(0)
            For Each row In selectedrowindex
                DataGridView1.rows(row).Selected = True
                DataGridView1.rows(row).Cells("playcount").Value = "UnWatched"
            Next
            DataDirty = True
            btn_movTableSave.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub GoToToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToToolStripMenuItem.Click
        Try
            Dim tempstring As String = ""
            For Each selecteditem In DataGridView1.SelectedRows
                tempstring = selecteditem.Cells("fullpathandfilename").Value
            Next
            For f = 0 To DataGridViewMovies.Rows.Count - 1
                'If CType(MovieListComboBox.Items(f), ValueDescriptionPair).Value = tempstring Then
                If DataGridViewMovies.Rows(f).Cells("fullpathandfilename").ToString = tempstring Then
                    'MovieListComboBox.SelectedItems.Clear()
                    'MovieListComboBox.SelectedIndex = f
                    DataGridViewMovies.ClearSelection()
                    DataGridViewMovies.Rows(f).Selected = True
                    Application.DoEvents()
                    currentTabIndex = 0
                    Me.TabControl2.SelectedIndex = 0
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
    Private Sub GoToSelectedMoviePosterSelectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToSelectedMoviePosterSelectorToolStripMenuItem.Click
        Try
            Dim tempstring As String = ""
            For Each selecteditem In DataGridView1.SelectedRows
                tempstring = selecteditem.Cells("fullpathandfilename").Value
            Next
            For f = 0 To DataGridViewMovies.Rows.Count - 1
                If DataGridViewMovies.Rows(f).Cells("fullpathandfilename").ToString = tempstring Then
                    DataGridViewMovies.ClearSelection()
                    DataGridViewMovies.Rows(f).Selected = True
                    For Each tabs In TabControl2.TabPages
                        If tabs.text = "Posters" Then
                            currentTabIndex = tabs.tabindex + 1
                            Me.TabControl2.SelectedIndex = tabs.tabindex + 1
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub GoToSelectedMovieFanartSelectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToSelectedMovieFanartSelectorToolStripMenuItem.Click
        Try
            Dim tempstring As String = ""
            For Each selecteditem In DataGridView1.SelectedRows
                tempstring = selecteditem.Cells("fullpathandfilename").Value
            Next
            For f = 0 To DataGridViewMovies.RowCount - 1
                If DataGridViewMovies.Rows(f).Cells("fullpathandfilename").ToString = tempstring Then
                    DataGridViewMovies.ClearSelection()
                    DataGridViewMovies.Rows(f).Selected = True
                    For Each tabs In TabControl2.TabPages
                        If tabs.text = "Fanart" Then
                            currentTabIndex = tabs.tabindex + 1
                            Me.TabControl2.SelectedIndex = tabs.tabindex + 1
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#End Region 'Movie Tableview code

    Private Sub SearchForNewEpisodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchForNewEpisodesToolStripMenuItem.Click
        Try
            'If Not bckgroundscanepisodes.IsBusy Then
            '    'ToolStripButton10.Visible = True
            '    TabPage15.Text = "Cancel Episode Search"
            '    TabPage15.ToolTipText = "This cancels the episode search" & vbCrLf & "and episode scraper thread"
            '    showstoscrapelist.Clear()
            '    For Each item In basictvlist
            '        If (item.fullpath.ToLower.IndexOf("tvshow.nfo") <> -1) And (item.locked = 0) Then
            '            showstoscrapelist.Add(item.fullpath)
            '        End If
            '    Next
            '    bckgroundscanepisodes.RunWorkerAsync()
            'Else
            '    MsgBox("This TV Scraper is already running")
            'End If
            SearchForNewEpisodesToolStripMenuItem.Owner.Hide()
            Call ep_Search()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub SearchALLForNewEpisodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchALLForNewEpisodesToolStripMenuItem.Click
        Try

            TVSearchALL = True
            Call ep_Search()
            TVSearchALL = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub mov_ScrapeSpecific(ByVal field As String)

        _rescrapeList.Field = field
        _rescrapeList.FullPathAndFilenames.Clear

        For Each row As DataGridViewRow In DataGridViewMovies.SelectedRows
            Dim fullpath As String = row.Cells("fullpathandfilename").Value.ToString
            If Not File.Exists(fullpath) Then Continue For
            _rescrapeList.FullPathAndFilenames.Add(fullpath)
        Next

        RunBackgroundMovieScrape("RescrapeSpecific")
    End Sub

    'rescrape fanart
    Private Sub RescrapeFanartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RescrapeFanartToolStripMenuItem.Click
        Try
            'rescrape fanart
            Call mov_FanartGet()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DownloadFanartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadFanartToolStripMenuItem.Click
        Try
            'download fanart
            Call mov_FanartGet()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mov_FanartGet()
        If IsNothing(workingMovieDetails) Then Return
        Dim messbox As New frmMessageBox("      Please Wait,", "", "Attempting to download Fanart")
        messbox.Show() : messbox.Refresh()
        Application.DoEvents()
        Dim tmdb       As New TMDb
        tmdb.Imdb = If(workingMovieDetails.fullmoviebody.imdbid.Contains("tt"), workingMovieDetails.fullmoviebody.imdbid, "")
        tmdb.TmdbId = workingMovieDetails.fullmoviebody.tmdbid 
        Try
            Dim FanartUrl As String = tmdb.GetBackDropUrl()
            Dim isvideotspath As String = If(workingMovieDetails.fileinfo.videotspath="","",workingMovieDetails.fileinfo.videotspath+"fanart.jpg")
            If IsNothing(FanartUrl) then
                MsgBox("No Fanart Found on TMDB")
            Else
                Dim paths As List(Of String) = Preferences.GetfanartPaths(workingMovieDetails.fileinfo.fullpathandfilename,If(workingMovieDetails.fileinfo.videotspath <>"",workingMovieDetails.fileinfo.videotspath,""))
                Dim aok As Boolean = DownloadCache.SaveImageToCacheAndPaths(FanartUrl, paths, True)
                If Not aok Then Throw New Exception("TMDB is offline")
                'For Each thispath In Preferences.offlinefolders
                '    Dim offlinepath As String = thispath & "\"
                '    If workingMovieDetails.fileinfo.fanartpath.IndexOf(offlinepath) <> -1 Then
                '        Dim mediapath As String
                '        mediapath = Utilities.GetFileName(workingMovieDetails.fileinfo.fullpathandfilename)
                '        Call mov_OfflineDvdProcess(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fullmoviebody.title, mediapath)
                '    End If
                'Next
                util_ImageLoad(PbMovieFanArt, paths(0), Utilities.DefaultFanartPath)
                util_ImageLoad(PictureBox2, paths(0), Utilities.DefaultFanartPath)
            End If
        Catch ex As Exception
            If ex.Message = "TMDB is offline" Then
                messbox.Close()
                MsgBox("Unable to connect to TheMovieDb.org." & vbCrLf & "Please confirm site is online")
            End If
        End Try
        messbox.Close()
    End Sub

    'Rescrape Poster
    Private Sub RescrapePToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RescrapePToolStripMenuItem.Click
        Try
            'rescrape poster IMPA
            mov_PosterGet("impa")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RescrapePosterFromTMDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RescrapePosterFromTMDBToolStripMenuItem.Click
        Try
            'rescrape poster tmdb
            mov_PosterGet("tmdb")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RescraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RescraToolStripMenuItem.Click
        Try
            'rescrape poster mpdb
            mov_PosterGet("mpdb")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PeToolStripMenuItem.Click
        Try
            'rescrape poster imdb
            mov_PosterGet("imdb")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DownloadPosterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadPosterToolStripMenuItem.Click
        Try
            'downloadposter impa
            mov_PosterGet("impa")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DownloadPosterFromTMDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadPosterFromTMDBToolStripMenuItem.Click
        Try
            'downloadposter tmdb
            mov_PosterGet("tmdb")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DownloadPosterFromMPDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadPosterFromMPDBToolStripMenuItem.Click
        Try
            'downloadposter mpdb
            mov_PosterGet("mpdb")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DownloadPosterFromIMDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadPosterFromIMDBToolStripMenuItem.Click
        Try
            'downloadposter imdb
            mov_PosterGet("imdb")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mov_PosterGet(ByVal source As String)
        Dim success As Boolean = False
        Try 
            If workingMovieDetails Is Nothing Then Exit Sub
            Dim messbox As New frmMessageBox("          Please Wait,", "", "Attempting to download Poster from " & source.ToUpper)
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()
            Dim isvideotspath As String = ""
            If workingMovieDetails.fileinfo.videotspath<>"" Then
                isvideotspath=workingMovieDetails.fileinfo.videotspath+"poster.jpg"
            End If
            Dim moviethumburl As String = ""
            Dim tmdb As New TMDb 
            tmdb.Imdb = If(workingMovieDetails.fullmoviebody.imdbid.Contains("tt"), workingMovieDetails.fullmoviebody.imdbid, "")
            tmdb.TmdbId = workingMovieDetails.fullmoviebody.tmdbid 
            If tmdb.Imdb = "" AndAlso tmdb.TmdbId = "" Then Exit Sub

            If source = "impa" Then
                If workingMovieDetails.fullmoviebody.title <> "" And workingMovieDetails.fullmoviebody.year <> "" Then
                    moviethumburl = scraperFunction2.impathumb(workingMovieDetails.fullmoviebody.title, workingMovieDetails.fullmoviebody.year)
                End If
            ElseIf source = "tmdb" Then
                If workingMovieDetails.fullmoviebody.imdbid.Contains("tt") OrElse workingMovieDetails.fullmoviebody.tmdbid <> "" Then
                    Try
                    moviethumburl = tmdb.FirstOriginalPosterUrl
                    Catch
                    End Try
                End If
            ElseIf source = "mpdb" Then
                If workingMovieDetails.fullmoviebody.imdbid.Contains("tt") Then
                    moviethumburl = scraperFunction2.mpdbthumb(workingMovieDetails.fullmoviebody.imdbid)
                End If
            ElseIf source = "imdb" Then
                If workingMovieDetails.fullmoviebody.imdbid.Contains("tt") Then
                    moviethumburl = scraperFunction2.imdbthumb(workingMovieDetails.fullmoviebody.imdbid)
                End If
            End If

            If moviethumburl <> "" And moviethumburl <> "na" Then
                Try
                    Dim PostPaths As List(Of String) = Preferences.GetPosterPaths(workingMovieDetails.fileinfo.fullpathandfilename,workingMovieDetails.fileinfo.videotspath)
                    Dim aok As Boolean = DownloadCache.SaveImageToCacheAndPaths(moviethumburl, PostPaths, True)
                    If Not aok Then Throw New Exception()
                    util_ImageLoad(PictureBoxAssignedMoviePoster, PostPaths(0), Utilities.DefaultPosterPath)
                    util_ImageLoad(PbMoviePoster, PostPaths(0), Utilities.DefaultPosterPath)
                    Dim path As String = Utilities.save2postercache(workingMovieDetails.fileinfo.fullpathandfilename, PostPaths(0))
                    updateposterwall(path, workingMovieDetails.fileinfo.fullpathandfilename)
                    success = True
                Catch ex As Exception
                    MsgBox("Error [" & ex.Message & "] occurred while trying to download and save the poster")
                End Try
            Else
                MsgBox("Unable to obtain a Poster from " & source.ToUpper)
            End If
            messbox.Close()
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        Finally
            messbox.Close()
        End Try
        If success then UpdateMissingPoster()
    End Sub

    Public Shared Sub updateposterwall(ByVal path As String, ByVal movie As String)
        For Each poster As PictureBox In Form1.TabPage22.Controls
            If poster.Tag = movie Then
                util_ImageLoad(poster, path, Utilities.DefaultPosterPath)
                poster.Tag = movie
            End If
        Next
    End Sub

    Public Sub mov_OfflineDvdProcess(ByVal nfopath As String, ByVal title As String, ByVal mediapath As String)
        Dim tempint2 As Integer = 2097152
        Dim SizeOfFile As Integer = FileLen(mediapath)
        If SizeOfFile > tempint2 Then
            Exit Sub
        End If
        Try
            Dim fanartpath As String = ""
            If IO.File.Exists(Preferences.GetFanartPath(nfopath)) Then
                fanartpath = Preferences.GetFanartPath(nfopath)
            Else
                fanartpath = Utilities.DefaultOfflineArtPath
            End If
            Dim curImage As Image = Image.FromFile(fanartpath)
            'Dim tempstring As String = "Please Insert '" & title & "' DVD"

            Dim tempstring As String = TextBox_OfflineDVDTitle.Text.Replace("%T", title)

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
                    path = applicationPath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    path = applicationPath & "\Settings\0" & f.ToString & ".jpg"
                End If
                curImage.Save(path, Drawing.Imaging.ImageFormat.Jpeg)
            Next

            Dim myProcess As Process = New Process
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.FileName = applicationPath & "\Assets\ffmpeg.exe"
            Dim proc_arguments As String = "-r 1 -b 1800 -qmax 6 -i """ & applicationPath & "\Settings\%03d.jpg"" -vcodec msmpeg4v2 -y """ & mediapath & """"
            myProcess.StartInfo.Arguments = proc_arguments
            myProcess.Start()
            myProcess.WaitForExit()

            For f = 1 To 16
                Dim tempstring4 As String
                If f < 10 Then
                    tempstring4 = applicationPath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    tempstring4 = applicationPath & "\Settings\0" & f.ToString & ".jpg"
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

    Private Sub ToolsToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolsToolStripMenuItem.DropDownItemClicked
        Try
            For Each temp In Preferences.commandlist
                If temp.title = e.ClickedItem.Text Then
                    Try
                        Process.Start(temp.command)
                    Catch ex As Exception
                    End Try
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PreferencesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreferencesToolStripMenuItem.Click 
        Try
            Dim t As New frmOptions
            If Preferences.MultiMonitoEnabled Then
                t.Bounds = screen.AllScreens(CurrentScreen).Bounds
                t.StartPosition = FormStartPosition.Manual
            End If
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub SearchForMissingEpisodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchForMissingEpisodesToolStripMenuItem.Click
        Try
            If Not Bckgrndfindmissingepisodes.IsBusy And bckgroundscanepisodes.IsBusy = False Then
                Preferences.displayMissingEpisodes = SearchForMissingEpisodesToolStripMenuItem.Checked
                Preferences.ConfigSave()
                If Preferences.displayMissingEpisodes = False 'OrElse MsgBox("If you had previously downloaded missing episodes, do you wish to download them again?", MsgBoxStyle.YesNo, "Confirm Download Missing Episode Details") = Windows.Forms.DialogResult.No Then
                    RefreshMissingEpisodesToolStripMenuItem.Enabled = False
                    rbTvListAll.Checked = True
                    rbTvMissingEpisodes.Enabled = False
                    rbTvMissingAiredEp.Enabled = False
                    RefreshMissingEpisodesToolStripMenuItem.ToolTipText = Nothing
                    tv_CacheRefresh 
                    'tv_Filter()
                    Return
                End If
                RefreshMissingEpisodesToolStripMenuItem.Enabled = True
                RefreshMissingEpisodesToolStripMenuItem.ToolTipText = "Last Refresh: " & Preferences.lastrefreshmissingdate
                rbTvMissingEpisodes.Enabled = True
                rbTvMissingAiredEp.Enabled = True
                'Dim answer = MsgBox("If you had previously downloaded missing episodes, do you wish to download them again?", MsgBoxStyle.YesNo, "Confirm Download Missing Episode Details")
                'If answer = MsgBoxResult.Yes 
                    'Preferences.DlMissingEpData = True
                'Else
                    Preferences.DlMissingEpData = False
                'End If
                tv_EpisodesMissingLoad(False)
            ElseIf Bckgrndfindmissingepisodes.IsBusy Then
                MsgBox("Process is already running")
            Else
                MsgBox("Missing episode search cannot be performed" & vbCrLf & "    when the episode scraper is running")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RefreshMissingEpisodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshMissingEpisodesToolStripMenuItem.Click
        Preferences.DlMissingEpData = True
        Preferences.lastrefreshmissingdate = DateTime.Now.ToString("yyyy-MM-dd")
        tv_EpisodesMissingClean
        tv_EpisodesMissingLoad(True)
        
    End Sub

    Private Sub RefreshThisShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_RefreshShow.Click
        Try
            Dim Show As TvShow = tv_ShowSelectedCurrently()
            Dim selectednode As Integer = TvTreeview.SelectedNode.Index

            If Show IsNot Nothing Then
                Call tv_CacheRefreshSelected(Show)
            Else
                MsgBox("No Show Selected")
            End If
            TvTreeview.SelectedNode = TvTreeview.Nodes(selectednode)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Tv_TreeViewContext_ShowMissEps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_ShowMissEps.Click
        Try
            Dim Show As TvShow = tv_ShowSelectedCurrently()

            If Not Bckgrndfindmissingepisodes.IsBusy Then
                Dim tempstring As String = ""
                For Each sho In Cache.TvCache.Shows
                    If sho.NfoFilePath = TvTreeview.SelectedNode.Name Then
                        tempstring = "Checking """ & sho.Title.Value & """ for missing episodes"
                        Exit For
                    End If
                Next
                If tempstring = "" Then tempstring = "Checking for missing episodes"
                Dim messbox As New frmMessageBox(tempstring, "", "Please Wait")
                messbox.Show()
                messbox.Refresh()
                Application.DoEvents()
                Dim ShowList As New List(Of TvShow)
                ShowList.Add(Show)
                Bckgrndfindmissingepisodes.RunWorkerAsync(ShowList)
                'Call tv_EpisodesMissingFind()
                messbox.Close()
            Else
                MsgBox("The missing episode thread is already running")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub LockAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockAllToolStripMenuItem.Click
        Try
            Dim Show As Media_Companion.TvShow
            For Each Show In Cache.TvCache.Shows 'Removed "As TvShow" from before "In Cache."
                Show.Load()
                Show.State = Media_Companion.ShowState.Locked
                Show.Save()
                Tv_CacheSave()
            Next
            tv_CacheLoad()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub UnlockAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnlockAllToolStripMenuItem.Click
        Try
            Dim Show As Media_Companion.TvShow
            For Each Show In Cache.TvCache.Shows
                Show.Load()
                Show.State = Media_Companion.ShowState.Open
                Show.Save()
                Tv_CacheSave()
            Next
            tv_CacheLoad()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TV_BatchRescrapeWizardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TV_BatchRescrapeWizardToolStripMenuItem.Click
        Try
            If Not tvbckrescrapewizard.IsBusy Then
                tvBatchList.Reset()

                Dim displaywizard As New tv_batch_wizard
                If Preferences.MultiMonitoEnabled Then
                    displaywizard.Bounds = screen.AllScreens(CurrentScreen).Bounds
                    displaywizard.StartPosition = FormStartPosition.Manual
                End If
                displaywizard.ShowDialog()

                If tvBatchList.activate = True Then
                    ToolStripStatusLabel8.Text = "Starting TV Batch Scrape"
                    ToolStripStatusLabel8.Visible = True
                    ToolStripProgressBar7.Value = 0
                    ToolStripProgressBar7.Visible = True
                    'ToolStripProgressBar6.Visible = True
                    tvbckrescrapewizard.RunWorkerAsync()
                End If
            Else
                MsgBox("The update Wizard is Already Running")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub ExportmoviesMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripExportMovies.Click
        Try
            listoffilestomove.Clear()

            If DataGridViewMovies.SelectedRows.Count > 0 Then

                'For Each movie In MovieListComboBox.SelectedItems
                For Each sRow As DataGridViewRow In DataGridViewMovies.SelectedRows
                    'Dim tempstring As String = CType(MovieListComboBox.SelectedItem, ValueDescriptionPair).Value
                    Dim playlist As New List(Of String)
                    'tempstring = Utilities.GetFileName(tempstring)

                    Dim tempstring As String = Utilities.GetFileName(DataGridViewMovies.SelectedCells(NFO_INDEX).Value.ToString)

                    playlist = Utilities.GetMediaList(tempstring)
                    If playlist.Count > 0 Then
                        For Each File In playlist
                            If Not listoffilestomove.Contains(File) Then
                                listoffilestomove.Add(File)
                            End If
                        Next

                        Dim fullpathandfilename As String = sRow.Cells("fullpathandfilename").Value.ToString

                        listoffilestomove.Add(fullpathandfilename)
                        If IO.File.Exists(Preferences.GetFanartPath(fullpathandfilename)) Then
                            listoffilestomove.Add(Preferences.GetFanartPath(fullpathandfilename))
                        End If
                        If IO.File.Exists(Preferences.GetPosterPath(fullpathandfilename)) Then
                            listoffilestomove.Add(Preferences.GetPosterPath(fullpathandfilename))
                        End If
                        Dim di As DirectoryInfo = New DirectoryInfo(fullpathandfilename.Replace(IO.Path.GetFileName(fullpathandfilename), ""))
                        Dim filenama As String = IO.Path.GetFileNameWithoutExtension(fullpathandfilename)
                        Dim fils As IO.FileInfo() = di.GetFiles(filenama & ".*")
                        For Each fiNext In fils
                            If Not listoffilestomove.Contains(fiNext.FullName) Then
                                listoffilestomove.Add(fiNext.FullName)
                            End If
                        Next
                        Dim trailerpath As String = fullpathandfilename.Replace(IO.Path.GetExtension(fullpathandfilename), "-trailer.flv")
                        Dim filenama2 As String = IO.Path.GetFileNameWithoutExtension(trailerpath)
                        Dim fils2 As IO.FileInfo() = di.GetFiles(filenama2 & ".*")
                        For Each fiNext In fils2
                            If Not listoffilestomove.Contains(fiNext.FullName) Then
                                listoffilestomove.Add(fiNext.FullName)
                            End If
                        Next
                    End If
                Next

                totalfilesize = 0
                For Each item In listoffilestomove
                    totalfilesize = totalfilesize + Utilities.GetFileSize(item)
                Next


                With FolderBrowserDialog1
                    .ShowNewFolderButton = True
                    .Description = "Select destination for file copy"
                End With
                Dim drive As String = ""
                Dim savepath As String = ""
                'If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                '    savepath = FolderBrowserDialog1.SelectedPath
                '    drive = IO.Path.GetPathRoot(savepath)
                '    Me.Visible = True
                '    Me.Show()
                '    Me.Refresh()

                '    Dim listoffilestomove2 As New List(Of String)
                '    listoffilestomove2.Clear()
                '    For Each fil In listoffilestomove
                '        listoffilestomove2.Add(fil)
                '    Next

                '    Dim drivespace As Long
                '    drivespace = GetFreeSpace(drive)
                '    Application.DoEvents()
                '    Me.Refresh()
                '    Dim percentages As New List(Of Integer)

                '    If drivespace > totalfilesize Then
                '        'My.Computer.FileSystem.CopyFile("C:\UserFiles\TestFiles\testFile.txt", "C:\UserFiles\TestFiles2\NewFile.txt", FileIO.UIOption.AllDialogs, FileIO.UICancelOption.DoNothing)
                Dim frm As New frmCopyProgress
                If Preferences.MultiMonitoEnabled Then
                    frm.Bounds = screen.AllScreens(CurrentScreen).Bounds
                    frm.StartPosition = FormStartPosition.Manual
                End If
                frm.ShowDialog()
                '    End If
                'End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub frm_ExportTabSetup()
        If TextBox45.Text = "" Then

            Dim tempstring2 As String = workingProfile.config.Replace(IO.Path.GetFileName(workingProfile.config), "pathsubstitution.xml")
            If IO.File.Exists(tempstring2) Then
                relativeFolderList.Clear()
                Dim prefs As New XmlDocument
                Try
                    prefs.Load(tempstring2)
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
                Dim thisresult As XmlNode = Nothing


                For Each thisresult In prefs("relativepaths")

                    Select Case thisresult.Name
                        Case "folder"
                            Dim mc As New str_RelativeFileList(SetDefaults)
                            Dim it2 As XmlNode
                            For Each it2 In thisresult.ChildNodes
                                Select Case it2.Name
                                    Case "mc"
                                        mc.mc = it2.InnerText
                                    Case "xbmc"
                                        mc.xbmc = it2.InnerText
                                        relativeFolderList.Add(mc)
                                End Select
                            Next

                    End Select
                Next
                For Each item In relativeFolderList
                    TextBox45.Text += "<folder>" & vbCrLf
                    TextBox45.Text += "    <mc>" & item.mc & "</mc>" & vbCrLf
                    TextBox45.Text += "    <xbmc>" & item.xbmc & "</xbmc>" & vbCrLf
                    TextBox45.Text += "</folder>" & vbCrLf & vbCrLf
                Next
            End If
            If TextBox45.Text = "" Then
                For Each pat In movieFolders
                    TextBox45.Text += "<folder>" & vbCrLf
                    TextBox45.Text += "    <mc>" & pat & "</mc>" & vbCrLf
                    TextBox45.Text += "    <xbmc>" & pat & "</xbmc>" & vbCrLf
                    TextBox45.Text += "</folder>" & vbCrLf & vbCrLf
                Next
                For Each pat In Preferences.offlinefolders
                    TextBox45.Text += "<folder>" & vbCrLf
                    TextBox45.Text += "    <mc>" & pat & "</mc>" & vbCrLf
                    TextBox45.Text += "    <xbmc>" & pat & "</xbmc>" & vbCrLf
                    TextBox45.Text += "</folder>" & vbCrLf & vbCrLf
                Next
                For Each pat In tvRootFolders
                    TextBox45.Text += "<folder>" & vbCrLf
                    TextBox45.Text += "    <mc>" & pat & "</mc>" & vbCrLf
                    TextBox45.Text += "    <xbmc>" & pat & "</xbmc>" & vbCrLf
                    TextBox45.Text += "</folder>" & vbCrLf & vbCrLf
                Next
            End If
        End If
    End Sub

    Private Sub Button109_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button109.Click
        Try
            relativeFolderList.Clear()
            Dim tempstring2 As String = workingProfile.config.Replace(IO.Path.GetFileName(workingProfile.config), "pathsubstitution.xml")



            Dim temptext As String = ""
            temptext = "<relativepaths>" & TextBox45.Text & "</relativepaths>"
            Dim doc As New XmlDocument
            doc.LoadXml(temptext)
            Dim thisresult As XmlElement
            For Each thisresult In doc("relativepaths")
                Dim newfo As New str_RelativeFileList(SetDefaults)
                For Each innerresult In thisresult
                    Select Case innerresult.Name
                        Case "mc"
                            newfo.mc = innerresult.InnerText
                        Case "xbmc"
                            newfo.xbmc = innerresult.InnerText
                    End Select
                Next
                If newfo.mc <> Nothing Then
                    If newfo.mc <> "" Then
                        If newfo.xbmc <> Nothing Then
                            If newfo.xbmc <> "" Then
                                relativeFolderList.Add(newfo)
                            End If
                        End If
                    End If
                End If
            Next

            If relativeFolderList.Count > 0 Then
                Dim docs As New XmlDocument

                Dim thispref As XmlNode = Nothing
                Dim xmlproc As XmlDeclaration

                xmlproc = docs.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                docs.AppendChild(xmlproc)
                Dim root As XmlElement
                Dim child As XmlElement
                Dim childchild As XmlElement
                root = doc.CreateElement("relativepaths")


                For Each item In relativeFolderList
                    child = doc.CreateElement("folder")
                    childchild = doc.CreateElement("mc")
                    childchild.InnerText = item.mc
                    child.AppendChild(childchild)
                    childchild = doc.CreateElement("xbmc")
                    childchild.InnerText = item.xbmc
                    child.AppendChild(childchild)
                    root.AppendChild(child)
                Next


                Dim output As New XmlTextWriter(tempstring2, System.Text.Encoding.UTF8)
                output.Formatting = Formatting.Indented
                doc.WriteTo(output)
                output.Close()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    
    Private Sub SplitContainer1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved

        Mc.clsGridViewMovie.SetFirstColumnWidth(DataGridViewMovies)
    End Sub

    Private Sub SplitContainer5_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer5.SplitterMoved

        ResizeBottomLHSPanel()
        Try
            DebugSplitter5PosLabel.Text = SplitContainer5.SplitterDistance
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub MediaCompanionCodeplexSiteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MediaCompanionCodeplexSiteToolStripMenuItem.Click
        Try
            Dim webAddress As String = "http://mediacompanion.codeplex.com/"
            'Process.Start(webAddress)
            OpenUrl(webAddress)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBoxDebugShowXML_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxDebugShowXML.CheckedChanged
        Try
            If CheckBoxDebugShowXML.Checked = False Then
                TabLevel1.TabPages.Remove(Me.TabConfigXML)
                TabLevel1.TabPages.Remove(Me.TabMovieCacheXML)
                TabLevel1.TabPages.Remove(Me.TabTVCacheXML)
                TabLevel1.TabPages.Remove(Me.TabProfile)
                TabLevel1.TabPages.Remove(Me.TabActorCache)
                TabLevel1.TabPages.Remove(Me.TabRegex)
            Else
                TabLevel1.TabPages.Add(Me.TabConfigXML)
                TabLevel1.TabPages.Add(Me.TabMovieCacheXML)
                TabLevel1.TabPages.Add(Me.TabTVCacheXML)
                TabLevel1.TabPages.Add(Me.TabProfile)
                TabLevel1.TabPages.Add(Me.TabActorCache)
                TabLevel1.TabPages.Add(Me.TabRegex)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
    Public Sub util_ConfigLoad(ByVal Optional prefs As Boolean =False )
        Preferences.ConfigLoad()
        Preferences.MultiMonitoEnabled = convert.ToBoolean(multimonitor)

        DataGridViewMovies.DataSource = Nothing

        Me.GroupBox22.Visible = Not Preferences.tvshow_useXBMC_Scraper
        Me.GroupBox22.SendToBack()
        Me.GroupBox_TVDB_Scraper_Preferences.Visible = Preferences.tvshow_useXBMC_Scraper
        Me.GroupBox_TVDB_Scraper_Preferences.BringToFront()

        Me.CheckBoxRenameNFOtoINFO.Checked = Preferences.renamenfofiles
        Me.ScrapeFullCertCheckBox.Checked = Preferences.scrapefullcert

        Me.cbMovieRenameEnable.Checked = Preferences.MovieRenameEnable
        Me.cbMovFolderRename.Checked = Preferences.MovFolderRename
        Me.cbMovSetIgnArticle.Checked = Preferences.MovSetIgnArticle 
        Me.cbMovTitleIgnArticle.Checked = Preferences.MovTitleIgnArticle
        Me.cbRenameUnderscore.Checked = Preferences.MovRenameSpaceCharacter
        Me.ManualRenameChkbox.Checked = Preferences.MovieManualRename
        Me.TextBox_OfflineDVDTitle.Text = Preferences.OfflineDVDTitle
        Me.tb_MovieRenameEnable.Text = Preferences.MovieRenameTemplate
        Me.tb_MovFolderRename.Text = Preferences.MovFolderRenameTemplate 
        Me.SearchForMissingEpisodesToolStripMenuItem.Checked = Preferences.displayMissingEpisodes
        Me.RefreshMissingEpisodesToolStripMenuItem.Enabled = Preferences.displayMissingEpisodes
        If Preferences.displayMissingEpisodes Then 
            Me.RefreshMissingEpisodesToolStripMenuItem.ToolTipText = "Last Refresh: " & Preferences.lastrefreshmissingdate 
        End If
        Me.rbTvMissingEpisodes.Enabled = Preferences.displayMissingEpisodes
        Me.rbTvMissingAiredEp.Enabled = Preferences.displayMissingEpisodes 
        Me.TextBox35.Text = Preferences.ScrShtDelay.ToString 
        Me.CheckBox_ShowDateOnMovieList.Checked = Preferences.showsortdate
        Me.cbxCleanFilenameIgnorePart.Checked = Preferences.movieignorepart
        Me.cbxNameMode.Checked = Preferences.namemode
        lblNameMode.Text = createNameModeText()
        Renamer.setRenamePref(tv_RegexRename.Item(Preferences.tvrename), tv_RegexScraper)
        If Not Preferences.XbmcTmdbScraperRatings = Nothing Then
            Save_XBMC_TMDB_Scraper_Config("fanart", Preferences.XbmcTmdbScraperFanart)
            Save_XBMC_TMDB_Scraper_Config("trailerq", Preferences.XbmcTmdbScraperTrailerQ)
            Save_XBMC_TMDB_Scraper_Config("language", Preferences.XbmcTmdbScraperLanguage)
            Save_XBMC_TMDB_Scraper_Config("ratings", Preferences.XbmcTmdbScraperRatings)
            Save_XBMC_TMDB_Scraper_Config("tmdbcertcountry", Preferences.XbmcTmdbScraperCertCountry)
        End If

        'Read_XBMC_IMDB_Scraper_Config()

        '----------------------------------------------------------

        mScraperManager = New ScraperManager(IO.Path.Combine(My.Application.Info.DirectoryPath, "Assets\scrapers"))
        '----------------------------------------------------------
        Dim loadinginfo As String = ""
        If Not IO.File.Exists(workingProfile.moviecache) Or Preferences.startupCache = False Then
            loadinginfo = "Status :- Building Movie caches"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            mov_RebuildMovieCaches()
        Else
            loadinginfo = "Status :- Loading Movie Database"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            mov_CacheLoad()
        End If

        If IO.File.Exists(workingProfile.filters) Then
            loadinginfo = "Status :- Loading Filterlist"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            Call util_FilterLoad()
        End If

        If IO.File.Exists(workingProfile.Genres) Then
            loadinginfo = "Status :- Loading Genre List"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            Call util_GenreLoad()
        End If

        If IO.File.Exists(workingProfile.homemoviecache) Then
            loadinginfo = "Status :- Loading Home Movie Database"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            Call homemovieCacheLoad()
        End If

        If IO.File.Exists(workingProfile.MusicVideoCache) Then
            loadinginfo = "Status :- Loading Music Video Database"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            Call UcMusicVideo1.MusicVideoCacheLoad()
        End If

        If Not prefs then
        If Not IO.File.Exists(workingProfile.tvcache) Or Preferences.startupCache = False Then
            loadinginfo = "Status :- Building TV Database"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            Call tv_CacheRefresh()
        Else
            loadinginfo = "Status :- Loading TV Database"
            frmSplash.Label3.Text = loadinginfo
            frmSplash.Label3.Refresh()
            Call tv_CacheLoad()
        End If
        Call tv_Filter()
        End If

        If homemoviefolders.Count > 0 Then
            ListBox19.Items.Clear()
            For Each folder In homemoviefolders
                ListBox19.Items.Add(folder)
            Next
        End If

        cbBtnLink.Checked = Preferences.XBMC_Link
        SetcbBtnLink
        If Preferences.XbmcLinkReady Then
            XbmcControllerQ.Write(XbmcController.E.ConnectReq, PriorityQueue.Priorities.low)
        End If
    End Sub

    Private Sub MediaCompanionHelpFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MediaCompanionHelpFileToolStripMenuItem.Click
        Try
            Process.Start(applicationPath & "\Media_Companion.chm")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub DisplayEpisodesByAiredDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_DispByAiredDate.Click
        'This function displays in a Form with a fullscreen textbox, a list off all of a TvShows episodes in 'date aired' order, separated by calendar year.
        'It can be called from a TVShow, Season or Episode context menu
        'It handles the following errors - no aired date, episodes on the same aired date, episodes on same date with same series & same episode i.e. a duplicate.... 

        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim NoDateCountUp As Integer = 0
            Dim Abort As Boolean = True     'this is used to verify that we actually have episodes to process
            Dim mySortedList As New SortedList()        'this is our sorted list, we add to the list a key (aired date) & the associated data (episode name), then we sort it & then we read out the data
            Dim childNodeLevel1 As TreeNode

            Select Case TvTreeview.SelectedNode.Level
                Case Is = 0
                    childNodeLevel1 = TvTreeview.SelectedNode
                Case Is = 1
                    childNodeLevel1 = TvTreeview.SelectedNode.Parent
                Case Is = 2
                    childNodeLevel1 = TvTreeview.SelectedNode.Parent.Parent
                Case Else
                    MsgBox("Unsupported TvTreeviewlevel in Aired Date Function", MsgBoxStyle.Exclamation, "Error!")
                    Exit Sub
            End Select




            'this section steps down through the tree to get from the tvshow to each episode
            For Each childNodeLevel2 As TreeNode In childNodeLevel1.Nodes
                For Each childNodeLevel3 As TreeNode In childNodeLevel2.Nodes
                    Abort = False                                          'if we get here then there is at least 1 episode
                    Dim episode As New TvEpisode
                    episode.Load(childNodeLevel3.Name)

                    Dim EpAired As String = episode.Aired.Value  'this holds the 'aired' value

                    If EpAired Is Nothing Then
                        EpAired = "9999-" & Utilities.PadNumber(NoDateCountUp, 5)  'if the aired date is nothing then we add it as 9999-xxxxx where x increments
                        NoDateCountUp += 1
                    End If

                    'Convert episode to 2 digits for formatting
                    Dim episode2digit As New List(Of String)
                    episode2digit.Clear()
                    episode2digit.Add(childNodeLevel3.Tag.Episode.Value)
                    If episode2digit(0).Length = 1 Then episode2digit(0) = "0" & episode2digit(0)

                    'Convert season to 2 digits for formatting
                    Dim season2digit As String = childNodeLevel3.Tag.Season.Value
                    If season2digit.Length = 1 Then season2digit = "0" & season2digit

                    'here we add our data in the order that it is read in the tree - the sorted list will sort it for us
                    'using the key value .aired (date format is yyyy-mm-dd so simple alphabetical sort is all that is required)
                    'FormatTVFilename formats the show title,episode tile, season no & episode no as per the users preferences
                    Dim SameDateLoop As Boolean = True
                    Dim Key As String
                    Key = EpAired & season2digit & episode2digit(0)         'the key index (which is the string used to sort by) is the date+season+episode - this should be unique!

                    Do Until SameDateLoop = False
                        If mySortedList.ContainsKey(Key) Then
                            Key += "^"                          'we add an aditional ^ to the key if its still not unique.....
                        Else
                            SameDateLoop = False
                        End If
                    Loop

                    mySortedList.Add(Key, EpAired & "    " & Renamer.setTVFilename(WorkingTvShow.Title.Value, childNodeLevel3.Tag.title.value, episode2digit, season2digit))


                Next
            Next

            If Not Abort Then   'i.e. we have episodes in this show.... 
                Dim textstring As String = "!!! " & WorkingTvShow.Title.Value & "  Seasons: " & WorkingTvShow.Seasons.Count & "  Episodes: " & WorkingTvShow.Episodes.Count & vbCrLf 'start our text with the show title
                textstring += "!!! " & StrDup(textstring.Length - 2, "-") & vbCrLf              'add an underline of the same length    
                Dim prevkey As String = mySortedList.GetKey(0).Substring(0, 4)                      'load with first year value first four digits of aired date
                For Line = 0 To mySortedList.Count - 1  'read the data from the sorted list
                    If mySortedList.GetKey(Line).Substring(0, 4) <> prevkey Then textstring += "!!! ----------" & vbCrLf 'line break between years...
                    prevkey = mySortedList.GetKey(Line).Substring(0, 4)                             'set so that we can compare with next iteration
                    textstring += "!!! " & mySortedList.GetByIndex(Line) & vbCrLf ' "!!! " allows this to be shown in either brief or Full log modes
                Next

                textstring += "!!! " & vbCrLf & "!!! 9999 episodes have no valid aired date stored" & vbCrLf

                '                                                   'Show Final Listing Screen
                Dim MyFormObject As New frmoutputlog(textstring, True)                                   'create the log form & modify it to suit our needs   
                MyFormObject.TextBox1.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)) 'constant width font
                MyFormObject.btn_savelog.AutoSize = True                                                    'change button size to text will fit automatically
                MyFormObject.btn_savelog.Text = "Save Details..."                                           'change the button text
                MyFormObject.Text = "Episodes in Aired Order for " & WorkingTvShow.Title.Value          'change the form title text
                MyFormObject.ShowDialog()                                                               'show the form

            Else                    'we get here if abort still = true, i.e. no episodes
                MsgBox("There are no Epsiodes or Missing Episodes for this show.", MsgBoxStyle.OkOnly, "No Episodes")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
    Private Sub util_FixSeasonEpisode() 'atleast try...       if season or episode is -1, but title contains a regexable name to retreive season & episode
        Dim textstring As String = "!!! Season Episode -1 fix..." & vbCrLf
        Dim correctionsfound As Integer = 0
        Dim correctionsfixed As Integer = 0
        Dim childNodeLevel1 As TreeNode
        Dim originallabeltext = Label148.Text
        For Each childNodeLevel1 In TvTreeview.Nodes    'step thru each tvshow/season/episode in the treeview
            For Each childNodeLevel2 As TreeNode In childNodeLevel1.Nodes
                Label148.Text = childNodeLevel1.Text & " - " & childNodeLevel2.Text 'display some sort of progress using the text label associated with the fix
                Label148.Invalidate()
                Windows.Forms.Application.DoEvents()    'this refreshes the label whilst we are still in this sub
                For Each childNodeLevel3 As TreeNode In childNodeLevel2.Nodes
                    Dim episode As New TvEpisode
                    episode.Load(childNodeLevel3.Name)  'load the episode from the nfo using the path stored in the treeview

                    If episode.Season.Value = -1 Or episode.Episode.Value = -1 Then ' check if we have the issue
                        textstring += "!!! " & childNodeLevel1.Text & " - " & childNodeLevel3.Name      'add details to the log"
                        correctionsfound += 1   'increment the found issues counter
                        For Each regexp In tv_RegexScraper

                            Dim M As Match
                            Dim sourcetext As String = ""
                            If RadioButton_Fix_Filename.Checked Then
                                sourcetext = childNodeLevel3.Name       'use nfo filename to retrieve season/episode
                            Else
                                sourcetext = episode.Title.Value        'use 'title' node in nfo to retieve season/episode
                            End If
                            M = Regex.Match(episode.Title.Value, regexp)
                            If M.Success = True Then                            'we found a valid regex match
                                Try
                                    episode.Season.Value = M.Groups(1).Value.ToString   'set new values
                                    episode.Episode.Value = M.Groups(2).Value.ToString
                                    correctionsfixed += 1
                                    episode.Save(childNodeLevel3.Name)                  'save episode
                                    textstring += " - Corrected - S" & episode.Season.Value & "E" & episode.Episode.Value
                                    Exit For

                                Catch
                                    textstring += vbCrLf & "**** exception created during nfo save **** - " & childNodeLevel3.Name
                                End Try
                            End If

                        Next
                        textstring += vbCrLf
                    End If
                Next
            Next
        Next
        Label148.Text = originallabeltext           'return the label text back after we have used it to diplay progress
        Dim MyFormObject As New frmoutputlog(textstring, True)                                   'create the log form & modify it to suit our needs   
        MyFormObject.TextBox1.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)) 'constant width font
        MyFormObject.btn_savelog.AutoSize = True                                                    'change button size to text will fit automatically
        MyFormObject.btn_savelog.Text = "Save Details..."                                           'change the button text
        MyFormObject.Font = New System.Drawing.Font("Courier New", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        MyFormObject.Text = "Corrections" & vbCrLf & "Found: " & correctionsfound & vbCrLf & " Fixed: " & correctionsfixed            'change the form title text
        MyFormObject.ShowDialog()                                                               'show the form
        If MsgBox("Corrections" & vbCrLf & "Found: " & correctionsfound & vbCrLf & "Fixed: " & correctionsfixed & vbCrLf & vbCrLf & "Do you want to perform a refresh to reload the corrected nfo's?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            tv_CacheRefresh()   'ask to do a refresh or not, user may want to try both methods before do a refresh.
        End If

    End Sub
    Private Sub PlayMovieToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripPlayMovie.Click
        Try
            mov_Play("Movie")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub PlayTrailerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripPlayTrailer.Click
        Try
            mov_Play("Trailer")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub RescrapeThisShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_RescrapeShowOrEpisode.Click
        tv_Rescrape()
    End Sub
    Private Sub WatchedShowOrEpisodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_WatchedShowOrEpisode.Click
        Tv_MarkAs_Watched_UnWatched("1")
    End Sub
    Private Sub Tv_TreeViewContext_UnWatchedShowOrEpisode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_UnWatchedShowOrEpisode.Click
        Tv_MarkAs_Watched_UnWatched("0")
    End Sub
    Private Sub Tv_TreeViewContext_Play_Episode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_Play_Episode.Click
        Try
            Dim ep As TvEpisode = ep_SelectedCurrently()
            If ep.IsMissing Then Exit Sub
            Dim tempstring As String = ep.VideoFilePath     'DirectCast(TvTreeview.SelectedNode.Tag, Media_Companion.TvEpisode).VideoFilePath
            If Preferences.videomode = 1 Then Call util_VideoMode1(tempstring)
            If Preferences.videomode = 2 Then Call util_VideoMode2(tempstring)
            If Preferences.videomode = 3 Then
                Preferences.videomode = 2
                Call util_VideoMode2(tempstring)
            End If
            If Preferences.videomode >= 4 Then
                If Preferences.selectedvideoplayer <> Nothing Then
                    Call util_VideoMode4(tempstring)
                Else
                    Call util_VideoMode1(tempstring)
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    
    Private Sub Tv_TreeViewContext_ViewNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_ViewNfo.Click
        Try
            If TvTreeview.SelectedNode Is Nothing Then Exit Sub
            If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
                Utilities.NfoNotepadDisplay(DirectCast(TvTreeview.SelectedNode.Tag, Media_Companion.TvShow).NfoFilePath, Preferences.altnfoeditor)
            ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                MsgBox("A Season NFO is invalid so it can't be shown")
            ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                Utilities.NfoNotepadDisplay(DirectCast(TvTreeview.SelectedNode.Tag, Media_Companion.TvEpisode).NfoFilePath, Preferences.altnfoeditor)
            Else
                MsgBox("None")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

        Debug.Print(Me.Controls.Count)
    End Sub

    Private Sub tsmiTvDelShowNfoArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiTvDelShowNfoArt.Click
        Dim Sh As TvShow = tv_ShowSelectedCurrently()
        TvDelShowNfoArt(Sh)
    End Sub

    Private Sub tsmiTvDelShowEpNfoArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiTvDelShowEpNfoArt.Click
        Dim msgstring As String = "Warning:  This will Remove all nfo's and artwork for this Show and Episodes"
        msgstring &= vbcrlf & "and remove the show's folder from MC's ""List Of Separate Folders""." & vbCrLf 
        msgstring &= vbCrLf & "To Rescrape this show, use ""Check Roots for New TV Shows"" or "
        msgstring &= vbCrLf & "Add this show's folder again to your ""List Of Separate Folders""." & vbCrLf
        msgstring &= vbCrLf & "Are your sure you wish to continue?"
        Dim x = MsgBox(msgstring, MsgBoxStyle.OkCancel, "Delete Show and Episode's nfo's and artwork")
        If x = MsgBoxResult.Cancel Then Exit Sub
        Dim Sh As TvShow = tv_ShowSelectedCurrently()
        Dim seas As TvSeason = tv_SeasonSelectedCurrently()
        Dim ep As TvEpisode = ep_SelectedCurrently()
        TvDelEpNfoAst(Sh, seas, ep, True)
        TvDelShowNfoArt(Sh, True)
    End Sub

    Private Sub tsmiTvDelEpNfoArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiTvDelEpNfoArt.Click
        Dim Sh As TvShow = tv_ShowSelectedCurrently()
        Dim seas As TvSeason = tv_SeasonSelectedCurrently()
        Dim ep As TvEpisode = ep_SelectedCurrently()
        TvDelEpNfoAst(Sh, seas, ep)
    End Sub

    Private Sub TvDelShowNfoArt(Show As TvShow, Optional ByVal Ignore As Boolean = False)
        Try
            If Not Ignore Then
                Dim msgstring As String = "Warning:  This will Remove the selected Tv Show's nfo and artwork"
                msgstring &= vbcrlf & "and remove the show's folder from MC's ""List Of Separate Folders""." & vbCrLf 
                msgstring &= vbCrLf & "To Rescrape this show, use ""Check Roots for New TV Shows"" or "
                msgstring &= vbCrLf & "Add this show's folder again to your ""List Of Separate Folders""." & vbCrLf
                msgstring &= vbCrLf & "Are your sure you wish to continue?"
                Dim x = MsgBox(msgstring, MsgBoxStyle.OkCancel, "Delete Show's nfo's and artwork")
                If x = MsgBoxResult.Cancel Then Exit Sub
            End If
            'Dim Show As TvShow = tv_ShowSelectedCurrently()
            TvDeleteShowArt(show)
            Dim showpath As String = Show.FolderPath 
            Utilities.SafeDeleteFile(showpath & "tvshow.nfo")
            showpath = showpath.Substring(0, showpath.Length-1)
            If ListBox6.items.Contains(showpath) Then ListBox6.Items.Remove(showpath)
            If Preferences.tvFolders.Contains(showpath) Then Preferences.tvFolders.Remove(showpath)
            TvTreeview.Nodes.Remove(show.ShowNode)
            Cache.TvCache.Remove(show)
            Tv_CacheSave()
            TvTreeviewRebuild()
            Show.UpdateTreenode()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TvDelEpNfoAst(Show As TvShow, season As TvSeason, ep As TvEpisode, Optional ByVal Ignore As Boolean = False)
        Try
            If Not Ignore Then 
                Dim msgstring As String = "Warning, This operation will delete all Episode nfo's and artwork"
                msgstring &= vbCrLf & "!! Note: will not delete missing episodes." & vbCrLf 
                msgstring &= vbCrLf & "Are your sure you wish to continue?"
                If MsgBox(msgstring, MsgBoxStyle.OkCancel, "Delete episode nfo(s) & artwork") = MsgBoxResult.Cancel Then Exit Sub
            End If

            Dim TheseEpisodes As New List(Of Media_Companion.TvEpisode)
            'Dim Show As TvShow = tv_ShowSelectedCurrently()
            'Dim season As TvSeason = tv_SeasonSelectedCurrently()
            'Dim ep As TvEpisode = ep_SelectedCurrently()
            'Dim IsMissing As Boolean = False
            If Not IsNothing(ep) Then
                For Each epis In season.Episodes
                    If epis.NfoFilePath = ep.NfoFilePath Then TheseEpisodes.Add(epis)
                Next
            Else
                TheseEpisodes.AddRange(Show.Episodes)
            End If

            For Each episode In TheseEpisodes
                If IsNothing(season) OrElse episode.Season.Value = season.SeasonNumber.ToString Then
                    If Not episode.IsMissing Then
                        If episode.FolderPath <> Show.FolderPath AndAlso File.Exists(episode.FolderPath & "folder.jpg") Then 
                            Utilities.SafeDeleteFile(episode.FolderPath & "folder.jpg")
                        End If
                        Dim eppath As String = episode.NfoFilePath
                        Utilities.SafeDeleteFile(eppath)
                        Utilities.SafeDeleteFile(eppath.Replace(".nfo", ".tbn"))
                        Utilities.SafeDeleteFile(eppath.Replace(".nfo", "-thumb.jpg"))
                    End If 
                    Cache.TvCache.Remove(episode)
                    TvTreeview.Nodes.Remove(episode.EpisodeNode)
                    If Not IsNothing(season) Then season.Episodes.Remove(episode)
                End If
            Next

            Dim listofnodes As New List(Of TreeNode)
            For Each n As TreeNode  In TvTreeview.Nodes
                listofnodes.Add(n)
                For Each chn As TreeNode in n.nodes
                    listofnodes.Add(chn)
                Next
            Next
            For Each n In listofnodes
                If n.FullPath.Contains(Show.Title.Value) AndAlso n.FullPath.ToLower.Contains("season") AndAlso n.GetNodeCount(False) = 0 Then
                    TvTreeview.Nodes.Remove(n)
                End If
            Next
            
            Tv_CacheSave()
            TvTreeviewRebuild()
            Show.UpdateTreenode()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Tv_TreeViewContext_FindMissArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tv_TreeViewContext_FindMissArt.Click
        Try
            tv_MissingArtDownload(tv_ShowSelectedCurrently)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub MovieContextMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MovieContextMenu.Opening
        Try
            'If (MovieListComboBox.SelectedItems.Count = 0) Then
            If DataGridViewMovies.SelectedRows.Count = 0 Then
                e.Cancel = True
            End If

            RenameFilesToolStripMenuItem.Enabled = Not Preferences.usefoldernames AndAlso Not Preferences.basicsavemode And Preferences.MovieRenameEnable

            tsmiRescrapeFrodo_Poster_Thumbs.Enabled = Preferences.FrodoEnabled
            tsmiRescrapeFrodo_Fanart_Thumbs.Enabled = Preferences.FrodoEnabled

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TVContextMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TVContextMenu.Opening
        Try
            If (TvTreeview.SelectedNode Is Nothing) Then
                e.Cancel = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub SaveSelectedFanartAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveSelectedFanartAsToolStripMenuItem.Click
        Try
            messbox = New frmMessageBox("Please wait,", "", "Downloading Fanart")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Me.Refresh()
            Application.DoEvents()

            Dim tempstring As String
            Dim tempint As Integer = 0
            Dim tempstring2 As String = String.Empty
            Dim allok As Boolean = False
            For Each button As Control In Me.Panel2.Controls
                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        tempstring = b1.Name
                        tempstring = tempstring.Replace("moviefanartcheckbox", "")
                        tempint = Convert.ToDecimal(tempstring)
                        tempstring2 = fanartArray(tempint).hdUrl
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If allok = False Then
                MsgBox("No Fanart Is Selected")
            Else
                Try
                    Panel1.Controls.Remove(Label1)
                    With SaveFileDialog1
                        .AddExtension = True
                        .DefaultExt = "jpg"
                        .Filter = "Jpg Pictures (*.jpg)|*.jpg"
                        .Title = "Save Hi-Res Fanart as"
                        .OverwritePrompt = True
                        .CheckPathExists = True
                        .InitialDirectory = workingMovieDetails.fileinfo.path
                    End With

                    If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'Utilities.DownloadImage(tempstring2, SaveFileDialog1.FileName, True, Preferences.resizefanart)
                        Movie.SaveFanartImageToCacheAndPath(tempstring2, SaveFileDialog1.FileName)
                    End If

                Catch ex As WebException
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        Finally
            messbox.Close()
        End Try

    End Sub

    Private Sub TasksDontShowCompleted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles TasksDontShowCompleted.CheckedChanged
        Me.TasksOnlyIncompleteTasks = TasksDontShowCompleted.Checked
    End Sub

    Private Function createNameModeText() As String
        Dim txtMovieTitle As String = "Movie (0000)"
        Dim lstNameModeFiles As New List(Of String)(New String() {txtMovieTitle & " CD1.avi", txtMovieTitle & " CD2.avi"})
        If Preferences.namemode = "1" Then txtMovieTitle &= " CD1"
        lstNameModeFiles.Add(txtMovieTitle & ".nfo")
        lstNameModeFiles.Add(txtMovieTitle & ".tbn")
        lstNameModeFiles.Add(txtMovieTitle & "-fanart.jpg")
        lstNameModeFiles.Sort()
        Return String.Join(vbCrLf, lstNameModeFiles)
    End Function

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        util_FixSeasonEpisode()
    End Sub

    Private Sub mov_DisplayFanart()
        If workingMovieDetails Is Nothing Then Exit Sub

        If workingMovieDetails.fileinfo.fanartpath <> Nothing Then
            Try
                Dim fanartpath = mov_FanartORExtrathumbPath()
                If IO.File.Exists(fanartpath) Then
                    util_ImageLoad(PictureBox2, fanartpath, Utilities.DefaultFanartPath)
                    lblMovFanartWidth.Text = PictureBox2.Image.Width
                    lblMovFanartHeight.Text = PictureBox2.Image.Height
                Else
                    util_ImageLoad(PictureBox2, Utilities.DefaultFanartPath, Utilities.DefaultFanartPath)
                    lblMovFanartWidth.Text = ""
                    lblMovFanartHeight.Text = ""
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                                Throw ex
#End If
            End Try
        End If
    End Sub

    Private Function mov_FanartORExtrathumbPath() As String
        Dim fanarttype As String = ""
        For Each rb As RadioButton In GroupBoxFanartExtrathumbs.Controls
            If rb.Checked Then
                fanarttype = rb.Text.ToLower
                Exit For
            End If
        Next
        Dim workingfanartpath As String = workingMovieDetails.fileinfo.fanartpath
        If fanarttype = "fanart" Then
            Return workingfanartpath
        Else
            Return Strings.Left(workingfanartpath, workingfanartpath.LastIndexOf("\")) & "\extrathumbs\" & fanarttype & ".jpg"
        End If
    End Function

    Private Sub TabPageMovMainBrowser_Enter(sender As Object, e As System.EventArgs) Handles TabPageLevel2MovMainBrowser.Enter
        mov_SplitContainerAutoPosition()  
    End Sub

    Private Sub TabPageTVMainBrowser_Enter(sender As Object, e As System.EventArgs) Handles TabPageLevel2TVMainBrowser.Enter
        tv_SplitContainerAutoPosition()
    End Sub

    Private Sub plottxt_DoubleClick(sender As System.Object, e As System.EventArgs) Handles plottxt.DoubleClick
        ShowBigMovieText()
    End Sub

    Private Sub ShowBigMovieText()

        Dim frm As New frmBigMovieText
        If Preferences.MultiMonitoEnabled Then
            frm.Bounds = screen.AllScreens(CurrentScreen).Bounds
            frm.StartPosition = FormStartPosition.Manual
        End If
        frm.ShowDialog(
                        titletxt.Text,
                        directortxt.Text,
                        votestxt.Text,
                        ratingtxt.Text,
                        runtimetxt.Text,
                        genretxt.Text,
                        txtStars.Text,
                        certtxt.Text,
                        plottxt.Text
                        )
    End Sub

    Private Function GetActorThumb(ByRef currentUri As String)
        Dim actorthumb As String = currentUri
        If Preferences.actorsave Then
            Dim uri As Uri
            uri = New Uri(actorthumb)

            If Len(Preferences.actornetworkpath) > 0 AndAlso Len(Preferences.actorsavepath) > 0 Then
                Dim actorThumbFileName As String
                Dim localActorThumbFileName As String
                actorThumbFileName = System.IO.Path.Combine(Preferences.actornetworkpath, uri.Segments(uri.Segments.GetLength(0) - 1))
                localActorThumbFileName = System.IO.Path.Combine(Preferences.actorsavepath, uri.Segments(uri.Segments.GetLength(0) - 1))
                Movie.SaveActorImageToCacheAndPath(uri.OriginalString, localActorThumbFileName)
                actorthumb = actorThumbFileName
            End If
        End If
        Return actorthumb
    End Function

    Private Sub mov_ToolStripRescrapeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mov_ToolStripRescrapeAll.Click
        mov_RescrapeAllSelected()
    End Sub

    Private Sub mov_RescrapeAllSelected()
        _rescrapeList.Field = Nothing
        _rescrapeList.FullPathAndFilenames.Clear()
        For Each row In DataGridViewMovies.SelectedRows
            Dim fullpath As String = row.Cells("fullpathandfilename").Value.ToString
            If Not File.Exists(fullpath) Then Continue For
            _rescrapeList.FullPathAndFilenames.Add(fullpath)
        Next
        RunBackgroundMovieScrape("RescrapeAll")
    End Sub
    
    Private Sub mov_VideoSourcePopulate()
        Try
            cbMovieDisplay_Source.Items.Clear()
            cbMovieDisplay_Source.Items.Add("")
            For Each mset In Preferences.releaseformat
                cbMovieDisplay_Source.Items.Add(mset)
            Next
                
            cbFilterSource.UpdateItems( Preferences.releaseformat.ToList )

            cbMovieDisplay_Source.SelectedIndex = 0
            If IsNothing(workingMovieDetails) = False Then
                If workingMovieDetails.fullmoviebody.source <> "" Then
                    For te = 0 To cbMovieDisplay_Source.Items.Count - 1
                        If cbMovieDisplay_Source.Items(te) = workingMovieDetails.fullmoviebody.source Then
                            cbMovieDisplay_Source.SelectedIndex = te
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    Private Sub applyAdvancedLists()
        If cleanfilenameprefchanged Then
            Dim strTemp As String = ""
            For i = 0 To lbCleanFilename.Items.Count - 1
                strTemp &= lbCleanFilename.Items(i) & "|"
            Next
            Preferences.moviecleanTags = strTemp.TrimEnd("|")
            cleanfilenameprefchanged = False
        End If
        If videosourceprefchanged Then
            Dim count As Integer = lbVideoSource.Items.Count - 1
            ReDim Preferences.releaseformat(count)
            For g = 0 To count
                Preferences.releaseformat(g) = lbVideoSource.Items(g)
            Next
            mov_VideoSourcePopulate()
            ep_VideoSourcePopulate()
            videosourceprefchanged = False
        End If
    End Sub

    'AnotherPhil bug fix - If the default browser is <goz> IE <goz/> then not stating the exe throws an exception
    Public Sub OpenUrl(ByVal url As String)
        Try
            If Preferences.selectedBrowser <> "" Then
                Process.Start(Preferences.selectedBrowser, Uri.EscapeUriString(url))
            Else
                Try
                    Process.Start(url)
                Catch ex As Exception
                    MessageBox.Show("An error occurred while trying to launch the default browser - Under 'General Preferences' check 'Use external Browser...' and then locate your browser to fix this error", "", MessageBoxButtons.OK)
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ZoomActorPictureBox(pictureBox As PictureBox)

        If IsNothing(pictureBox.Tag) orElse  pictureBox.Tag.ToString = Utilities.DefaultActorPath Then
            Exit Sub
        End If

        Me.ControlBox = False
        MenuStrip1.Enabled = False

        Try
            'util_ZoomImage(New Bitmap(pictureBox.Tag.ToString))
            util_ZoomImage(pictureBox.Tag.ToString)
        Catch
            Dim wc As New WebClient()
            Dim ImageInBytes() As Byte = wc.DownloadData(pictureBox.Tag)
            Dim ImageStream As New IO.MemoryStream(ImageInBytes)
            Dim cachefile As String = Utilities.Download2Cache(pictureBox.Tag.ToString)
            'util_ZoomImage(New Bitmap(ImageStream))
            util_ZoomImage(cachefile)
        End Try

    End Sub

    Private Sub Mov_ToolStripRemoveMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Mov_ToolStripRemoveMovie.Click
        Mov_RemoveMovie ()
    End Sub

    Private Sub Mov_RemoveMovie()
        For Each row As DataGridViewRow In DataGridViewMovies.SelectedRows

            oMovies.RemoveMovieFromCache(row.Cells("fullpathandfilename").Value.ToString)

            DataGridViewMovies.Rows.RemoveAt(row.Index)
        Next

        DataGridViewMovies.ClearSelection
        oMovies.SaveMovieCache

        UpdateFilteredList
    End Sub

    Private Sub mov_ToolStripDeleteNfoArtwork_Click( sender As System.Object,  e As System.EventArgs) Handles mov_ToolStripDeleteNfoArtwork.Click
        Mov_DeleteNfoArtwork()
    End Sub

    Private Sub Mov_DeleteNfoArtwork()
        If MsgBox(" Are you sure you wish to delete" & vbCrLf & ".nfo, Fanart, Poster and Actors for" & vbCrLf & "Selected Movie(s)?",MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim movielist As New List(Of String)
            Preferences.MovieDeleteNfoArtwork = True
            For Each row As DataGridViewRow In DataGridViewMovies.SelectedRows
                movielist.Add(row.Cells(NFO_INDEX).Value.ToString)
                oMovies.DeleteScrapedFiles(row.Cells(NFO_INDEX).Value.ToString)
            Next

            'Last remove from dataGridViewMovies and update cache.
            Mov_RemoveMovie()
            Preferences.MovieDeleteNfoArtwork = False
        Else
            MsgBox(" Deletion of .nfo, artwork and Actors " &vbCrLf & "has been Cancelled")
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        If Preferences.homemoviefolders.Count = 0 And homemovielist.Count = 0 And TabControl1.SelectedIndex <> 4 Then
            MsgBox("Please add A Folder containing Home Movies")
            Try
                TabControl1.SelectedIndex = 4
            Catch
            End Try
            homeTabIndex = 1
            Exit Sub
        End If

        Dim tab As String = TabControl1.SelectedTab.Text.ToLower
        If tab = "search for new home movies" Then
            TabControl1.SelectedIndex = homeTabIndex
            Call homeMovieScan()
        ElseIf tab="refresh list" Then
            TabControl1.SelectedIndex = homeTabIndex
            Call rebuildHomeMovies()
        ElseIf tab = "screenshot" Then
            util_ImageLoad(PictureBox5, WorkingHomeMovie.fileinfo.fanartpath, Utilities.DefaultFanartPath)
            homeTabIndex = TabControl1.SelectedIndex
        Else
            homeTabIndex = TabControl1.SelectedIndex
        End If
    End Sub

    Private Sub RebuildHomeMovieCacheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RebuildHomeMovieCacheToolStripMenuItem.Click
        Call rebuildHomeMovies()
    End Sub

    Private Sub btnHomeMovieScreenShot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeMovieScreenShot.Click
        Try

            If IsNumeric(tbHomeMovieScreenShotDelay.Text) Then
                Dim thumbpathandfilename As String = WorkingHomeMovie.fileinfo.fullpathandfilename.Replace(".nfo", "-fanart.jpg")
                Dim pathandfilename As String = WorkingHomeMovie.fileinfo.fullpathandfilename.Replace(".nfo", "")
                Dim messbox As frmMessageBox = New frmMessageBox("ffmpeg is working to capture the desired screenshot", "", "Please Wait")
                For Each ext In Utilities.VideoExtensions
                    Dim tempstring2 As String = pathandfilename & ext
                    If IO.File.Exists(tempstring2) Then
                        Dim seconds As Integer = 10
                        If Convert.ToInt32(tbHomeMovieScreenShotDelay.Text) > 0 Then
                            seconds = Convert.ToInt32(tbHomeMovieScreenShotDelay.Text)
                        End If

                        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                        messbox.Show()
                        messbox.Refresh()
                        Application.DoEvents()

                        Utilities.CreateScreenShot(tempstring2, thumbpathandfilename, seconds, True)

                        If File.Exists(thumbpathandfilename) Then
                            Try
                                util_ImageLoad(PictureBox5, thumbpathandfilename, Utilities.DefaultFanartPath)
                                util_ImageLoad(PictureBox4, thumbpathandfilename, Utilities.DefaultFanartPath)
                            Catch
                                messbox.Close()
                            End Try
                        End If
                        Exit For
                    End If
                Next
                messbox.Close()
            Else
                MsgBox("Please enter a numerical value into the textbox")
                tbHomeMovieScreenShotDelay.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub PlayHomeMovieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayHomeMovieToolStripMenuItem.Click
        mov_Play("HomeMovie")
    End Sub

    Private Sub OpenFolderToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFolderToolStripMenuItem.Click
        Try
            If Not WorkingHomeMovie.fileinfo.fullpathandfilename Is Nothing Then
                Call util_OpenFolder(WorkingHomeMovie.fileinfo.fullpathandfilename)
            Else
                MsgBox("There is no Movie selected to open")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFileToolStripMenuItem.Click
        Try
            Utilities.NfoNotepadDisplay(WorkingHomeMovie.fileinfo.fullpathandfilename, Preferences.altnfoeditor)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PictureBox4_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox4.DoubleClick
        Try
            Try
                If WorkingHomeMovie.fileinfo.fanartpath <> Nothing Then
                    If IO.File.Exists(WorkingHomeMovie.fileinfo.fanartpath) Then
                        Me.ControlBox = False
                        MenuStrip1.Enabled = False
                        'Using newimage As New Bitmap(WorkingHomeMovie.fileinfo.fanartpath)
                            util_ZoomImage(WorkingHomeMovie.fileinfo.fanartpath)
                        'End Using
                    End If
                End If
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TabLevel1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabLevel1.SelectedIndexChanged

        Select Case TabLevel1.SelectedTab.Text.ToLower
            Case "general preferences"
                Call General_PreferencesSetup()
            Case "config.xml"
                RichTextBoxTabConfigXML.Text = Utilities.LoadFullText(workingProfile.config) '   applicationPath & "\settings\config.xml"
            Case "moviecache" 
                RichTextBoxTabMovieCache.Text = Utilities.LoadFullText(workingProfile.moviecache) ' applicationPath & "\settings\moviecache.xml"
            Case = "tvcache" 
                RichTextBoxTabTVCache.Text = Utilities.LoadFullText(workingProfile.tvcache) ' applicationPath & "\settings\tvcache.xml"
            Case = "actorcache" 
                RichTextBoxTabActorCache.Text = Utilities.LoadFullText(workingProfile.actorcache) '  applicationPath & "\settings\actorcache.xml"
            Case = "profile" 
                RichTextBoxTabProfile.Text = Utilities.LoadFullText(applicationPath & "\settings\profile.xml") '  applicationPath & "\settings\profile.xml"
            Case = "regex" 
                RichTextBoxTabRegex.Text = Utilities.LoadFullText(workingProfile.regexlist) '   applicationPath & "\settings\regex.xml"
            Case "export"
                frm_ExportTabSetup()
            Case "movies"
                If Not MoviesFiltersResizeCalled Then
                    MoviesFiltersResizeCalled = True
                    Preferences.movie_filters.SetMovieFiltersVisibility
                    UpdateMovieFiltersPanel
                End If
        End Select

    End Sub

    Sub HandleMovieList_DisplayChange(DisplayField As String)
        Mc.clsGridViewMovie.GridFieldToDisplay1 = DisplayField

        If rbTitleAndYear.Checked Then Preferences.moviedefaultlist = 0
        If rbFileName    .Checked Then Preferences.moviedefaultlist = 1
        If rbFolder      .Checked Then Preferences.moviedefaultlist = 2

        Mc.clsGridViewMovie.GridviewMovieDesign(Me)
        If MainFormLoadedStatus Then
            DisplayMovie()
        End If
    End Sub

    Private Sub cbFilterChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterGeneral.SelectedValueChanged, cbFilterSource.TextChanged,
                                                                                                    cbFilterGenre.TextChanged, cbFilterCertificate.TextChanged,
                                                                                                    cbFilterSet.TextChanged, cbFilterResolution.TextChanged,
                                                                                                    cbFilterAudioCodecs.TextChanged, cbFilterAudioChannels.TextChanged,
                                                                                                    cbFilterAudioBitrates.TextChanged, cbFilterNumAudioTracks.TextChanged,
                                                                                                    cbFilterAudioLanguages.TextChanged, cbFilterActor.TextChanged, cbFilterTag.TextChanged,
                                                                                                    cbFilterDirector.TextChanged, cbFilterVideoCodec.TextChanged, cbFilterSubTitleLang.TextChanged

        If TypeName(sender) = "TriStateCheckedComboBox" Then
            Dim x As MC_UserControls.TriStateCheckedComboBox = sender

            If x.opState <> 0 Then
                Return
            End If
        End If

        ApplyMovieFilters()
    End Sub
     
    Private Sub cbFilterRatingChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterRating.SelectionChanged
        ApplyMovieFilters
    End Sub

    Private Sub cbFilterVotesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterVotes.SelectionChanged
        ApplyMovieFilters
    End Sub

    Private Sub cbFilterFolderSizesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterFolderSizes.SelectionChanged
        ApplyMovieFilters
    End Sub

    Private Sub cbFilterYearChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYear.SelectionChanged
        ApplyMovieFilters
    End Sub

    Private Sub cbFilterBeginSliding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYear.BeginSliding, cbFilterVotes.BeginSliding, cbFilterRating.BeginSliding, cbFilterFolderSizes.BeginSliding
        SplitContainer5.Panel2.ContextMenuStrip = Nothing
    End Sub


    Private Sub cbFilterEndSliding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYear.EndSliding, cbFilterVotes.EndSliding, cbFilterRating.EndSliding, cbFilterFolderSizes.EndSliding
        SplitContainer5.Panel2.ContextMenuStrip = cmsConfigureMovieFilters
    End Sub

    Private Sub ApplyMovieFilters

        tsmiConvertToFrodo.Enabled = (cbFilterGeneral.Text.RemoveAfterMatch="Pre-Frodo poster only") or (cbFilterGeneral.Text.RemoveAfterMatch="Both poster formats")

        If ProgState = ProgramState.Other Then
            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
            DisplayMovie
        End If
    End Sub

    Sub HandleMovieFilter_SelectedValueChanged(cbFilter As ComboBox, ByRef filterValue As String, Optional replaceUnknown As Boolean = False)
        If ProgState = ProgramState.Other Then

            If cbFilter.Text = "All" Then
                filterValue = ""
            Else
                filterValue = cbFilter.Text.RemoveAfterMatch

                If replaceUnknown Then filterValue = filterValue.Replace("Unknown","-1")
            End If

            ApplyMovieFilters
        End If
    End Sub

    Private Sub TimerToolTip_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerToolTip.Tick
        TimerToolTip.Enabled = False
        TooltipGridViewMovies1.Visible = Preferences.ShowMovieGridToolTip
    End Sub


#Region "Movie scraping stuff"


    Sub RunBackgroundMovieScrape(action As String)

        If Not BckWrkScnMovies.IsBusy Then
            scraperLog = ""
            tsStatusLabel.Text = ""
            tsMultiMovieProgressBar.Value = tsMultiMovieProgressBar.Minimum
            tsMultiMovieProgressBar.Visible = Get_MultiMovieProgressBar_Visiblity(action)
            ScraperStatusStrip.Visible = True
            ssFileDownload.Visible = False
            tsProgressBarFileDownload_Resize()
            EnableDisableByTag("M", False)       'Disable all UI options that can't be run while scraper is running   
            ScraperErrorDetected = False

            BckWrkScnMovies.RunWorkerAsync(action)
            While BckWrkScnMovies.IsBusy 
                Application.DoEvents()
            End While
            oMovies.SaveCaches
        Else
            MsgBox("The Movie Scraper is Already Running")
        End If
    End Sub

    Private  cbBtnLink_Checked As Boolean

    Sub EnableDisableByTag(tagQualifier As String, _state As Boolean)

        If Not _state Then
            StateBefore = ProgState
            cbBtnLink_Checked = cbBtnLink.Checked
            ProgState = ProgramState.MovieControlsDisabled
        Else
            cbBtnLink.Checked = cbBtnLink_Checked
            ProgState = StateBefore
        End If

        If IsNothing(ControlsToDisableDuringMovieScrape) Then
            ControlsToDisableDuringMovieScrape = (From c As Control In GetAllMatchingControls("M")).ToList
        End If

        For Each c In ControlsToDisableDuringMovieScrape
            c.Enabled = _state
        Next

        'Not picked up for some unknown reason...
        MoviesToolStripMenuItem.Enabled = _state
    End Sub


    Function GetAllMatchingControls(tagQualifier As String) As List(Of Control)

        Dim allControls As New List(Of Control)

        GetAllMatchingControls(tagQualifier, Me, allControls)

        Return allControls
    End Function


    Sub GetAllMatchingControls(tagQualifier As String, parent As Control, allControls As List(Of Control))

        Dim query = From c As Control In parent.Controls 'Where Not IsNothing(c) AndAlso Not IsNothing(c.Tag) AndAlso TypeName(c.Tag).ToLower="string" AndAlso c.tag=tagQualifier

        For Each c As Control In query

            Try
                If Not IsNothing(c) AndAlso Not IsNothing(c.Tag) AndAlso TypeName(c.Tag).ToLower = "string" AndAlso c.Tag = tagQualifier Then
                    allControls.Add(c)
                End If
            Catch ex As Exception
            End Try

            GetAllMatchingControls(tagQualifier, c, allControls)
        Next

    End Sub

#Region "MC Scraper Calls"
    Function Get_MultiMovieProgressBar_Visiblity(action As String)

        Select Case action
            Case "BatchRescrape"          : Return _rescrapeList.FullPathAndFilenames.Count>1               ' filteredList.Count > 1
            Case "ChangeMovie"            : Return False
            Case "RescrapeAll"            : Return _rescrapeList.FullPathAndFilenames.Count>1
            Case "RescrapeDisplayedMovie" : Return False
            Case "RescrapeSpecific"       : Return _rescrapeList.FullPathAndFilenames.Count>1
            Case "ScrapeDroppedFiles"     : Return droppedItems.Count>1
            Case "SearchForNewMovies"     : Return True
            Case "SearchForNewMusicVideo" : Return True
            Case "ChangeMusicVideo"       : Return True
            Case "RebuildCaches"          : Return True
        End Select

        MsgBox("Unrecognised scrape action : [" + action + "]!", MsgBoxStyle.Exclamation, "Programming Error!")
        Return False
    End Function


    Private Sub BckWrkScnMovies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BckWrkScnMovies.DoWork
        Try
            CallSubByName(DirectCast(e.Argument, String))

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Public Sub CallSubByName(SubName As String)
        Me.GetType.GetMethod(SubName).Invoke(Me, Nothing)
    End Sub


    Public Sub BatchRescrape()
        oMovies.BatchRescrapeSpecific(_rescrapeList.FullPathAndFilenames, rescrapeList)    'filteredList
    End Sub


    Public Sub ChangeMovie
        oMovies.ChangeMovie(workingMovieDetails.fileinfo.fullpathandfilename, ChangeMovieId, MovieSearchEngine)
    End Sub


    Public Sub RescrapeAll
        oMovies.RescrapeAll(_rescrapeList.FullPathAndFilenames)
    End Sub


    Public Sub RebuildCaches
        oMovies.RebuildCaches
    End Sub


    Public Sub RescrapeDisplayedMovie
        oMovies.RescrapeMovie(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fullmoviebody.tmdbid)
        oMovies.SaveCaches
    End Sub


    Public Sub RescrapeSpecific
        oMovies.RescrapeSpecific(_rescrapeList)
    End Sub


    Public Sub ScrapeDroppedFiles
        oMovies.ScrapeFiles(droppedItems)
    End Sub


    Public Sub SearchForNewMovies
        Preferences.googlecount = 0
        oMovies.FindNewMovies
    End Sub

    Public Sub SearchForNewMusicVideo
        oMovies.FindNewMusicVideos()
    End Sub

    Public Sub ChangeMusicVideo
        oMovies.ChangeMovie(ucMusicVideo.changeMVList(0), "", ucMusicVideo.changeMVList(1))
    End Sub

    Private Sub BckWrkScnMovies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BckWrkScnMovies.ProgressChanged

        Dim oProgress As Progress = CType(e.UserState, Progress)

        If e.ProgressPercentage <> -1 Then
            tsMultiMovieProgressBar.Value = e.ProgressPercentage
        End If

        If oProgress.Command = Progress.Commands.Append Then
            tsStatusLabel.Text &= oProgress.Message
        Else
            tsStatusLabel.Text = oProgress.Message
        End If

        If oProgress.Message = Movie.MSG_ERROR then
            ScraperErrorDetected = True
        End If

        If Not IsNothing(oProgress.Log) Then
            scraperLog += oProgress.Log
        End If
    End Sub

    Private Sub BckWrkScnMovies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BckWrkScnMovies.RunWorkerCompleted

        If scrapeAndQuit = True Then
            sandq = sandq -2
            Exit Sub
        End If

        LastMovieDisplayed=""   'Force currently displayed movie details to be re-displayed 
        UpdateFilteredList()

        ScraperStatusStrip.Visible = False
        ssFileDownload.Visible = False
        EnableDisableByTag("M", True)       'Re-enable disabled UI options that couldn't be run while scraper was running
        Preferences.MovieChangeMovie = False
        If Not Preferences.MusicVidScrape Then DisplayLogFile()  ' no need to display log after music video scraping.
        Preferences.MusicVidScrape = False  '  Reset to false only after scrapers complete
    End Sub

#End Region 

#Region "XBMC TMDB Scraper Calls"

    'Private Sub BckWrkXbmcMovies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BckWrkXbmcMovies.DoWork
    '    Try
            

    '    Catch ex As Exception
    '        ExceptionHandler.LogError(ex)
    '    End Try
    'End Sub 

    'Private Sub BckWrkXbmcMovies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BckWrkXbmcMovies.ProgressChanged
    '    Dim oProgress As Progress = CType(e.UserState, Progress)
    '    If e.ProgressPercentage <> -1 Then
    '        tsMultiMovieProgressBar.Value = e.ProgressPercentage
    '    End If
    '    If oProgress.Command = Progress.Commands.Append Then
    '        tsStatusLabel.Text &= oProgress.Message
    '    Else
    '        tsStatusLabel.Text = oProgress.Message
    '    End If
    '    If oProgress.Message = Movie.MSG_ERROR then
    '        ScraperErrorDetected = True
    '    End If
    '    If Not IsNothing(oProgress.Log) Then
    '        scraperLog += oProgress.Log
    '    End If
    'End Sub

    'Private Sub BckWrkXbmcMovies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BckWrkXbmcMovies.RunWorkerCompleted

    '    If scrapeAndQuit = True Then
    '        sandq = sandq -2
    '        Exit Sub
    '    End If

    '    LastMovieDisplayed=""   'Force currently displayed movie details to be re-displayed 
    '    UpdateFilteredList()

    '    ScraperStatusStrip.Visible = False
    '    ssFileDownload.Visible = False
    '    EnableDisableByTag("M", True)       'Re-enable disabled UI options that couldn't be run while scraper was running

    '    DisplayLogFile()

    '    'TabPage14.Text = "Search for new movies"
    '    'TabPage14.ToolTipText = "Scan movie folders for new media files"
    'End Sub

#End Region

    Private Sub UpdateFilteredList

        ProgState = ProgramState.UpdatingFilteredList

        Dim lastSelectedMovie = workingMovie.fullpathandfilename

        filteredList.Clear
        filteredList.AddRange(oMovies.MovieCache)

        Assign_FilterGeneral
'       Assign_FilterActor

        UpdateMinMaxMovieFilters

        If cbFilterGenre         .Visible Then cbFilterGenre         .UpdateItems( oMovies.GenresFilter         )
        If cbFilterCertificate   .Visible Then cbFilterCertificate   .UpdateItems( oMovies.CertificatesFilter   )
        If cbFilterSet           .Visible Then cbFilterSet           .UpdateItems( oMovies.SetsFilter           )
        If cbFilterTag           .Visible Then cbFilterTag           .UpdateItems( oMovies.TagFilter            )
        If cbFilterResolution    .Visible Then cbFilterResolution    .UpdateItems( oMovies.ResolutionFilter     )
        If cbFilterVideoCodec    .Visible Then cbFilterVideoCodec    .UpdateItems( oMovies.VideoCodecFilter     )
        If cbFilterAudioCodecs   .Visible Then cbFilterAudioCodecs   .UpdateItems( oMovies.AudioCodecsFilter    )
        If cbFilterAudioChannels .Visible Then cbFilterAudioChannels .UpdateItems( oMovies.AudioChannelsFilter  )
        If cbFilterAudioBitrates .Visible Then cbFilterAudioBitrates .UpdateItems( oMovies.AudioBitratesFilter  )
        If cbFilterNumAudioTracks.Visible Then cbFilterNumAudioTracks.UpdateItems( oMovies.NumAudioTracksFilter )
        If cbFilterAudioLanguages.Visible Then cbFilterAudioLanguages.UpdateItems( oMovies.AudioLanguagesFilter )
        If cbFilterActor         .Visible Then cbFilterActor         .UpdateItems( oMovies.ActorsFilter         )
        If cbFilterDirector      .Visible Then cbFilterDirector      .UpdateItems( oMovies.DirectorsFilter      )
        If cbFilterSubTitleLang  .Visible Then cbFilterSubTitleLang  .UpdateItems( oMovies.SubTitleLangFilter   )
                                          
        Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)

        Try
            For Each row As DataGridViewRow In DataGridViewMovies.Rows
                row.Selected = (row.Cells("fullpathandfilename").Value.ToString = lastSelectedMovie)
            Next
        Catch
        End Try
        
        If DataGridViewMovies.SelectedRows.Count=0 And DataGridViewMovies.Rows.Count>1 Then
            DataGridViewMovies.Rows(0).Selected=True
        End If

 '       mov_FormPopulate()
        DisplayMovie()

        ProgState = ProgramState.Other
    End Sub



    Private Function TriStateFilter_OnFormatItem(ByVal item As String) As String Handles cbFilterGenre.OnFormatItem, cbFilterCertificate.OnFormatItem,
                                                                                    cbFilterSet.OnFormatItem, cbFilterResolution.OnFormatItem,
                                                                                    cbFilterAudioCodecs.OnFormatItem, cbFilterAudioChannels.OnFormatItem,
                                                                                    cbFilterAudioBitrates.OnFormatItem, cbFilterNumAudioTracks.OnFormatItem,
                                                                                    cbFilterAudioLanguages.OnFormatItem, cbFilterActor.OnFormatItem,
                                                                                    cbFilterSource.OnFormatItem, cbFilterTag.OnFormatItem, cbFilterTag.OnFormatItem,
                                                                                    cbFilterDirector.OnFormatItem, cbFilterVideoCodec.OnFormatItem, cbFilterSubTitleLang.OnFormatItem 
        Return item.RemoveAfterMatch
    End Function

    Sub Assign_FilterGeneral
        If cbFilterGeneral.Visible Then
            Dim selected = cbFilterGeneral.Text

            cbFilterGeneral.Items.Clear
            cbFilterGeneral.Items.AddRange( oMovies.GeneralFilters.ToArray )

            If cbFilterGeneral.Text = "" Then cbFilterGeneral.Text = "All"

            If selected<>"" Then
                For Each item As String In cbFilterGeneral.Items
                    If item.RemoveAfterMatch=selected.RemoveAfterMatch Then
                        cbFilterGeneral.SelectedItem=item    
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub
    
    Private Sub XBMC_ProgressChanged(ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        If Not scrapeAndQuit Then
            If e.ProgressPercentage <> 999999 Then
                ToolStripStatusLabel1.Text = e.UserState
            Else
                Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
            End If
        End If
    End Sub

    Sub FileDownload_SizeObtained(ByVal iFileSize As Long) Handles oMovies.FileDownloadSizeObtained

        Callback_ShowHideFileDownloadProgressBar(True, iFileSize)

    End Sub

    Private Sub FileDownload_AmountDownloadedChanged(ByVal iTotalBytesRead As Long) Handles oMovies.AmountDownloadedChanged
        Me.Invoke(CType(Sub() Safe_FileDownload_AmountDownloadedChanged(iTotalBytesRead), MethodInvoker))
    End Sub


    Private Sub FileDownload_FileDownloadComplete() Handles oMovies.FileDownloadComplete
        Callback_ShowHideFileDownloadProgressBar(False, -1)
    End Sub


    Private Sub FileDownload_FileDownloadFailed() Handles oMovies.FileDownloadFailed
        Callback_ShowHideFileDownloadProgressBar(False, -1)
    End Sub


    'Initiate callback from main UI thread
    Sub Callback_ShowHideFileDownloadProgressBar(ByVal bool As Boolean, iFileSize As Long)

        Me.Invoke(CType(Sub() ShowHideFileDownloadProgressBar(bool, iFileSize), MethodInvoker))
    End Sub


    Sub ShowHideFileDownloadProgressBar(ByVal bool As Boolean, iFileSize As Long)
        ssFileDownload.Visible = bool
        If bool Then tsProgressBarFileDownload.Maximum = iFileSize
    End Sub


    Sub Safe_FileDownload_AmountDownloadedChanged(ByVal iTotalBytesRead As Long)
        tsProgressBarFileDownload.Value = iTotalBytesRead
    End Sub

    Private Sub tsLabelEscCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsLabelEscCancel.Click
        BckWrkScnMovies_Cancel()
    End Sub

    Sub bckgrndcancel
        Dim CurrentTab As String = TabLevel1.SelectedTab.Text.ToLower
        If CurrentTab = "movies" Then BckWrkScnMovies_Cancel
        If CurrentTab = "tv shows" Then bckgroundscanepisodes.CancelAsync()
    End Sub

    Sub BckWrkScnMovies_Cancel
        If BckWrkScnMovies.IsBusy Then
            tsStatusLabel.Text = "* Cancelling... *"
            BckWrkScnMovies.CancelAsync()
        End If
    End Sub

    Sub AbortFileDownload
        tsStatusLabel.Text = "* Aborting trailer download... *"
        Monitor.Enter(countLock)
        blnAbortFileDownload = True
        Monitor.Exit(countLock)
    End Sub
    
    Sub doResizeRefresh
        Try
            Dim CurrentTab As String = TabLevel1.SelectedTab.Text.ToLower
            If CurrentTab = "movies" Then
                mov_DisplayFanart()
                util_ImageLoad(PbMovieFanArt, workingMovieDetails.fileinfo.fanartpath, Utilities.DefaultFanartPath)
                Dim video_flags = VidMediaFlags(workingMovieDetails.filedetails)
                movieGraphicInfo.OverlayInfo(PbMovieFanArt, ratingtxt.Text, video_flags, workingMovie.DisplayFolderSize)
            ElseIf CurrentTab = "tv shows" Then
                TvTreeview_AfterSelect_Do()
            End If
        Catch
        End Try
    End Sub

    Sub doRefresh
        Dim CurrentTab As String = TabLevel1.SelectedTab.Text.ToLower
        If CurrentTab = "movies" Then mov_RebuildMovieCaches()
        If CurrentTab = "tv shows" Then tv_CacheRefresh()
    End Sub

    Sub doSearchNew
        Dim CurrentTab As String = TabLevel1.SelectedTab.Text.ToLower
        If CurrentTab = "movies" Then SearchForNew()
        If CurrentTab = "tv shows" Then ep_Search()
    End Sub

    Private Sub ssFileDownload_Resize(sender As System.Object, e As System.EventArgs) Handles ssFileDownload.Resize
        tsProgressBarFileDownload_Resize()
    End Sub


    Private Sub tsProgressBarFileDownload_Resize()
        tsProgressBarFileDownload.Width = ssFileDownload.Width - tsFileDownloadlabel.Width - 8
    End Sub


#End Region 'Movie scraping stuff

    Sub SearchForNew
        RunBackgroundMovieScrape("SearchForNewMovies")
    End Sub

    Sub Do_ScrapeAndQuit
        If refreshAndQuit Then
            mov_RebuildMovieCaches()
            tv_CacheRefresh()
            While BckWrkScnMovies.IsBusy 
                Thread.Sleep(100)
                Application.DoEvents
            End While
        End If

        Do While sandq <> 0
            If sandq >= 2 Then
                SearchForNew
                Do Until sandq < 2
                    Thread.Sleep(100)
                    Application.DoEvents()
                Loop
            End If
            If sandq = 1 Then
                ep_Search()
                Do Until sandq < 1
                    Thread.Sleep(100)
                    Application.DoEvents()
                Loop
            End If
        Loop
    End Sub

    Sub DoScrapeDroppedFiles()
        RunBackgroundMovieScrape("ScrapeDroppedFiles")
    End Sub

    Private Sub DisplayLogFile()
        If ScraperErrorDetected And Preferences.ShowLogOnError Then
            scraperLog = "******************************************************************************" & vbCrLf &
                         "* One or more errors were detected during scraping. See below for details.   *" & vbCrLf &
                         "* To disable seeing this, turn off General Preferences -'Show log on error'. *" & vbCrLf & 
                         "******************************************************************************" & vbCrLf & vbCrLf & scraperLog
        End If

        If (Not Preferences.disablelogfiles Or (ScraperErrorDetected And Preferences.ShowLogOnError)) And scraperLog <> "" Then
            Dim MyFormObject As New frmoutputlog(scraperLog, True)
            Try
                MyFormObject.ShowDialog()
            Catch ex As Exception
            End Try
        End If

        ScraperErrorDetected=False
    End Sub

    Sub pop_cbMovieDisplay_MovieSet

        Dim previouslySelected = cbMovieDisplay_MovieSet.SelectedItem

        cbMovieDisplay_MovieSet.Sorted = True
        cbMovieDisplay_MovieSet.Items.Clear
        cbMovieDisplay_MovieSet.Items.AddRange( Preferences.moviesets.ToArray )
        cbMovieDisplay_MovieSet.Sorted = False

        If cbMovieDisplay_MovieSet.Items.Count = 0 Then
            cbMovieDisplay_MovieSet.Items.Add("-None-")
        End If
        If cbMovieDisplay_MovieSet.Items(0) <> "-None-" Then
            cbMovieDisplay_MovieSet.Items.Insert(0, "-None-")
        End If

        cbMovieDisplay_MovieSet.SelectedIndex = 0

        If previouslySelected=Nothing Then
            If workingMovieDetails.fullmoviebody.movieset.MovieSetName <> Nothing Then
                If workingMovieDetails.fullmoviebody.movieset.MovieSetName.IndexOf(" / ") = -1 Then
                    cbMovieDisplay_MovieSet.SelectedItem = workingMovieDetails.fullmoviebody.movieset.MovieSetName
                End If
            End If
        Else
            cbMovieDisplay_MovieSet.SelectedItem = previouslySelected
        End If
    End Sub

#Region "ToolStripmenu Movie Rescrape Specific"

    Private Sub ToolStripMenuItem1_Click_1(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Call mov_ScrapeSpecific("trailer")
    End Sub                 'trailer
    Private Sub RenameFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameFilesToolStripMenuItem.Click
        mov_ScrapeSpecific("rename_files")
    End Sub              'rename files
    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Call mov_ScrapeSpecific("title")
    End Sub        'title
    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Call mov_ScrapeSpecific("plot")
    End Sub        'plot
    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Call mov_ScrapeSpecific("tagline")
    End Sub        'tagline
    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        Call mov_ScrapeSpecific("director")
    End Sub        'director
    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        Call mov_ScrapeSpecific("credits")
    End Sub        'Credits
    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        Call mov_ScrapeSpecific("mpaa")
    End Sub        'mpaa
    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        Call mov_ScrapeSpecific("genre")
    End Sub        'genre
    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        Call mov_ScrapeSpecific("outline")
    End Sub      'outline
    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        Call mov_ScrapeSpecific("runtime")
    End Sub      'runtime
    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Call mov_ScrapeSpecific("runtime_file")
    End Sub      'runtime file
    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        Call mov_ScrapeSpecific("studio")
    End Sub      'studio
    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        Call mov_ScrapeSpecific("actors")
    End Sub      'actors
    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem16.Click
        Call mov_ScrapeSpecific("missingfanart")
    End Sub      'missingfanart
    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        Call mov_ScrapeSpecific("missingposters")
    End Sub      'missingposters
    Private Sub ToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem18.Click
        Call mov_ScrapeSpecific("mediatags")
    End Sub      'mediatags
    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        Call mov_ScrapeSpecific("rating")
    End Sub      'rating
    Private Sub ToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem20.Click
        Call mov_ScrapeSpecific("votes")
    End Sub      'votes
    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        Call mov_ScrapeSpecific("stars")
    End Sub      'stars
    Private Sub YearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YearToolStripMenuItem.Click
        Call mov_ScrapeSpecific("year")
    End Sub  'year
    Private Sub tsmiRescrapeKeyWords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiRescrapeKeyWords.Click
        Call mov_ScrapeSpecific("TagsFromKeywords")
    End Sub    'TagsFromKeywords
    Private Sub tsmiTMDbSetName_Click( sender As System.Object,  e As System.EventArgs) Handles tsmiTMDbSetName.Click
        Call mov_ScrapeSpecific("tmdb_set_name")
    End Sub                         'tmdb set name
    Private Sub tsmiSetWatched_Click( sender As System.Object,  e As System.EventArgs) Handles tsmiSetWatched.Click
        Call mov_ScrapeSpecific("SetWatched")
    End Sub                           'set watched
    Private Sub tsmiClearWatched_Click( sender As System.Object,  e As System.EventArgs) Handles tsmiClearWatched.Click
        Call mov_ScrapeSpecific("ClearWatched")
    End Sub                       'clear watched
    Private Sub tsmiDlTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiDlTrailer.Click
        Call mov_ScrapeSpecific("Download_Trailer")
    End Sub                  'Download Trailer
    Private Sub tsmiRescrapeCountry_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapeCountry.Click
        mov_ScrapeSpecific("country")
    End Sub                               'country
    Private Sub tsmiRescrapeTop250_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapeTop250.Click
        mov_ScrapeSpecific("top250")
    End Sub                                 'top250
    Private Sub tsmiRescrapePremiered_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapePremiered.Click
        mov_ScrapeSpecific("Premiered")
    End Sub                           'premiered
    Private Sub tsmiRescrapePosterUrls_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapePosterUrls.Click
        mov_ScrapeSpecific("PosterUrls")
    End Sub                         'poster urls
    Private Sub tsmiRescrapeFrodo_Poster_Thumbs_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapeFrodo_Poster_Thumbs.Click
        mov_ScrapeSpecific("Frodo_Poster_Thumbs")
    End Sub       'Frodo poster thumbs
    Private Sub tsmiRescrapeFrodo_Fanart_Thumbs_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapeFrodo_Fanart_Thumbs.Click
        mov_ScrapeSpecific("Frodo_Fanart_Thumbs")
    End Sub       'Frodo fanart thumbs
    Private Sub tsmiRescrapeFanartTv_Click( sender As Object,  e As EventArgs) Handles tsmiRescrapeFanartTv.Click
        mov_ScrapeSpecific("ArtFromFanartTv")
    End Sub       'Frodo fanart thumbs
#End Region  'ToolStripmenu Movie Rescrape Specific

    
    Private Sub General_PreferencesSetup()

        CopyOfPreferencesIgnoreArticle = Preferences.ignorearticle

        prefsload = True
        generalprefschanged = False

        Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
        Dim newFont As System.Drawing.Font
        If Preferences.font <> Nothing Then
            If Preferences.font <> "" Then
                Try
                    newFont = CType(tcc.ConvertFromString(Preferences.font), System.Drawing.Font)
                Catch ex As Exception
                    newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
                    Preferences.font = "Times New Roman, 9pt"
                End Try
            Else
                newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
                Preferences.font = "Times New Roman, 9pt"
            End If
        Else
            newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
            Preferences.font = "Times New Roman, 9pt"
        End If
        Label130.Font = newFont
        Label130.Text = Preferences.font

        chkbx_disablecache          .Checked    = Not Preferences.startupCache
        cbOverwriteArtwork          .Checked    = Not Preferences.overwritethumbs
        cbDisplayLocalActor         .Checked    = Preferences.LocalActorImage
        CheckBoxRenameNFOtoINFO     .Checked    = Preferences.renamenfofiles
        cb_IgnoreThe                .Checked    = Preferences.ignorearticle
        cb_IgnoreA                  .Checked    = Preferences.ignoreAarticle
        cb_IgnoreAn                 .Checked    = Preferences.ignoreAn
        cb_SorttitleIgnoreArticles  .Checked    = Preferences.sorttitleignorearticle
        CheckBox38                  .Checked    = Preferences.intruntime
        CheckBox33                  .Checked    = Preferences.actorseasy
        txtbx_minrarsize            .Text       = Preferences.rarsize.ToString
        cbExternalbrowser           .Checked    = Preferences.externalbrowser
        btnFindBrowser              .Enabled    = cbExternalbrowser.Checked
        tbaltnfoeditor              .Text       = Preferences.altnfoeditor 
        cbCheckForNewVersion        .Checked    = Preferences.CheckForNewVersion
        cbDisplayRatingOverlay      .Checked    = Preferences.DisplayRatingOverlay
        cbDisplayMediaInfoOverlay   .Checked    = Preferences.DisplayMediainfoOverlay 
        cbDisplayMediaInfoFolderSize.Checked    = Preferences.DisplayMediaInfoFolderSize
        cbMultiMonitorEnable        .Checked    = Preferences.MultiMonitoEnabled 

        If Preferences.videomode = 1 Then
            RadioButton38.Checked = True
        ElseIf Preferences.videomode = 2 Then
            RadioButton37.Checked = True
        ElseIf Preferences.videomode = 4 Then
            RadioButton36.Checked = True
        End If

        If Preferences.videomode = 4 Then
            Label121.Text = Preferences.selectedvideoplayer
            Label121.Visible = True
            btn_custommediaplayer.Enabled = True
        Else
            btn_custommediaplayer.Enabled = False
            Label121.Visible = False
        End If

        If Preferences.XBMC_version = 0 Then
            rbXBMCv_pre.Checked = True
        ElseIf Preferences.XBMC_version = 1 Then
            rbXBMCv_both.Checked = True
        ElseIf Preferences.XBMC_version = 2 Then
            rbXBMCv_post.Checked = True
        End If

        Label112.Text = "Current Default Profile: " & profileStruct.DefaultProfile
        Label108.Text = "Current Startup Profile: " & profileStruct.StartupProfile
        ListBox13.Items.Clear()
        For Each prof In profileStruct.ProfileList
            ListBox13.Items.Add(prof.ProfileName)
        Next

        ListBox16.Items.Clear()
        ListBox17.Items.Clear()
        For Each com In Preferences.commandlist
            ListBox16.Items.Add(com.title)
            ListBox17.Items.Add(com.command)
        Next

        cbShowMovieGridToolTip.Checked = Preferences.ShowMovieGridToolTip
        cbShowLogOnError      .Checked = Preferences.ShowLogOnError
        cbUseMultipleThreads  .Checked = Preferences.UseMultipleThreads
        cbShowAllAudioTracks  .Checked = Preferences.ShowAllAudioTracks

        Preferences.ExcludeFolders.PopTextBox(tbExcludeFolders)

        prefsload = False
        generalprefschanged = False
        btnGeneralPrefsSaveChanges.Enabled = False
    End Sub

    Private Sub tv_PreferencesSetup()
        prefsload = True
        ComboBox_tv_EpisodeRename.Items.Clear()
        For Each Regex In tv_RegexRename
            ComboBox_tv_EpisodeRename.Items.Add(Regex)
        Next

        ListBox_tv_RegexScrape.Items.Clear()
        For Each regexc In tv_RegexScraper
            ListBox_tv_RegexScrape.Items.Add(regexc)
        Next

        ListBox_tv_RegexRename.Items.Clear()
        For Each regexc In tv_RegexRename
            ListBox_tv_RegexRename.Items.Add(regexc)
        Next

        ListBox12.Items.Clear()
        ListBox12.Items.Add(Preferences.TvdbLanguage)
        If ListBox12.Items.Count <> 0 Then
            ListBox12.SelectedIndex = 0
        End If

        ComboBox8.SelectedIndex                     = Preferences.TvdbActorScrape
        ComboBox_tv_EpisodeRename.SelectedIndex     = If(Preferences.tvrename < ComboBox_tv_EpisodeRename.Items.Count, Preferences.tvrename, 0)
        CheckBox17                      .Checked    = Preferences.disabletvlogs
        CheckBox20                      .Checked    = Preferences.enabletvhdtags
        CheckBox_tv_EpisodeRenameCase   .Checked    = Preferences.eprenamelowercase
        CheckBox_tv_EpisodeRenameAuto   .Checked    = Preferences.autorenameepisodes
        cbTvAutoScreenShot              .Checked    = Preferences.autoepisodescreenshot
        cbTvScrShtTVDBResize            .Checked    = Preferences.tvscrnshtTVDBResize 
        cbTvQuickAddShow                .Checked    = Preferences.tvshowautoquick
        CheckBox34                      .Checked    = Preferences.copytvactorthumbs
        cbTvDlPosterArt                 .Checked    = Preferences.tvdlposter
        cbTvDlFanart                    .Checked    = Preferences.tvdlfanart
        cbTvDlSeasonArt                 .Checked    = Preferences.tvdlseasonthumbs
        cbTvDlXtraFanart                .Checked    = Preferences.dlTVxtrafanart
        cbTvDlFanartTvArt               .Checked    = Preferences.TvDlFanartTvArt
        cbTvFanartTvFirst               .Checked    = Preferences.TvFanartTvFirst
        cb_TvFolderJpg                  .Checked    = Preferences.tvfolderjpg
        cbSeasonFolderjpg               .Checked    = Preferences.seasonfolderjpg 
        CheckBox_Use_XBMC_TVDB_Scraper  .Checked    = Preferences.tvshow_useXBMC_Scraper
        cbTvMissingSpecials             .Checked    = Preferences.ignoreMissingSpecials
        AutoScrnShtDelay                .Text       = ScrShtDelay
        cmbxTvXtraFanartQty.SelectedIndex = cmbxTvXtraFanartQty.FindStringExact(Preferences.TvXtraFanartQty.ToString)

        Select Case Preferences.seasonall
            Case "none"
                RadioButton41.Checked = True
            Case "poster"
                RadioButton40.Checked = True
            Case "wide"
                RadioButton39.Checked = True
        End Select

        If Preferences.sortorder = "dvd" Then
            RadioButton42.Checked = True
        Else
            RadioButton43.Checked = True
        End If

        If Preferences.postertype = "poster" Then
            posterbtn.Checked = True
        Else
            bannerbtn.Checked = True
        End If
        prefsload = False
        tvprefschanged = False
        btnTVPrefSaveChanges.Enabled = False
    End Sub

    Private Sub mov_PreferencesSetup()
        prefsload = True
        displayRuntimeScraper = True
        If Preferences.enablehdtags = True Then
            CheckBox19.CheckState = CheckState.Checked
            PanelDisplayRuntime.Enabled = True
            If Preferences.movieRuntimeDisplay = "file" Then
                rbRuntimeFile.Checked = True
            Else
                rbRuntimeScraper.Checked = True
            End If
        Else
            CheckBox19.CheckState = CheckState.Unchecked
            PanelDisplayRuntime.Enabled = False
            rbRuntimeScraper.Checked = True
        End If
        Call mov_SwitchRuntime()

        ListBox9                            .SelectedItem   = Preferences.imdbmirror
        TextBox_OfflineDVDTitle             .Text           = Preferences.OfflineDVDTitle
        tb_MovieRenameEnable                .Text           = Preferences.MovieRenameTemplate
        tb_MovFolderRename                  .Text           = Preferences.MovFolderRenameTemplate 
        localactorpath                      .Text           = Preferences.actorsavepath
        xbmcactorpath                       .Text           = Preferences.actornetworkpath
        cbPreferredTrailerResolution        .Text           = Preferences.moviePreferredTrailerResolution.ToUpper()
        cbMovieRuntimeFallbackToFile        .Enabled        = (Preferences.movieRuntimeDisplay = "scraper")
        cbMovieRuntimeFallbackToFile        .Checked        = Preferences.movieRuntimeFallbackToFile
        tbDateFormat                        .Text           = Preferences.DateFormat
        cbMovieList_ShowColPlot             .Checked        = Preferences.MovieList_ShowColPlot
        cbDisableNotMatchingRenamePattern   .Checked        = Preferences.DisableNotMatchingRenamePattern
        cbMovieList_ShowColWatched          .Checked        = Preferences.MovieList_ShowColWatched
        cmbxMovieScraper_MaxStudios         .SelectedItem   = Preferences.MovieScraper_MaxStudios.ToString
        nudActorsFilterMinFilms             .Text           = Preferences.ActorsFilterMinFilms
        nudMaxActorsInFilter                .Text           = Preferences.MaxActorsInFilter
        cbMovieFilters_Actors_Order         .SelectedIndex  = Preferences.MovieFilters_Actors_Order
        nudDirectorsFilterMinFilms          .Text           = Preferences.DirectorsFilterMinFilms
        nudMaxDirectorsInFilter             .Text           = Preferences.MaxDirectorsInFilter
        cbMovieFilters_Directors_Order      .SelectedIndex  = Preferences.MovieFilters_Directors_Order
        cbMissingMovie                      .Checked        = Preferences.incmissingmovies 
        nudSetsFilterMinFilms               .Text           = Preferences.SetsFilterMinFilms
        nudMaxSetsInFilter                  .Text           = Preferences.MaxSetsInFilter
        cbMovieFilters_Sets_Order           .SelectedIndex  = Preferences.MovieFilters_Sets_Order
        chkbOriginal_Title                  .Checked        = Preferences.Original_Title
        'RadioButton52                      .Checked        = If(Preferences.XBMC_Scraper = "tmdb", True, False ) 
        cbNoAltTitle                        .Checked        = Preferences.NoAltTitle
        cbXtraFrodoUrls                     .Checked        = Not Preferences.XtraFrodoUrls
        CheckBox16                          .Checked        = Not Preferences.disablelogfiles
        cbDlTrailerDuringScrape             .Checked        = Preferences.DownloadTrailerDuringScrape
        cbMovieTrailerUrl                   .Checked        = Preferences.gettrailer
        cbMoviePosterScrape                 .Checked        = Preferences.scrapemovieposters
        cbMovFanartScrape                   .Checked        = Preferences.savefanart
        cbMovFanartTvScrape                 .Checked        = Preferences.MovFanartTvscrape 
        cbMovieUseFolderNames               .Checked        = Preferences.usefoldernames
        cbMovXtraThumbs                     .Checked        = Preferences.movxtrathumb
        cbMovXtraFanart                     .Checked        = Preferences.movxtrafanart
        cbDlXtraFanart                      .Checked        = Preferences.dlxtrafanart
        cbMovSetArtScrape                   .Checked        = Preferences.dlMovSetArtwork
        rbMovSetArtSetFolder                .Checked        = Preferences.MovSetArtSetFolder
        rbMovSetFolder                      .Checked        = Not Preferences.MovSetArtSetFolder 
        btnMovSetCentralFolderSelect        .Enabled        = Preferences.MovSetArtSetFolder 
        tbMovSetArtCentralFolder            .Text           = Preferences.MovSetArtCentralFolder 
        cbMovieAllInFolders                 .Checked        = Preferences.allfolders
        cbMovCreateFolderjpg                .Checked        = Preferences.createfolderjpg
        cbMovCreateFanartjpg                .Checked        = Preferences.createfanartjpg
        cbMovRootFolderCheck                .Checked        = Preferences.movrootfoldercheck
        cbMovieBasicSave                    .Checked        = Preferences.basicsavemode
        cbxNameMode                         .Checked        = Preferences.namemode
        cbxCleanFilenameIgnorePart          .Checked        = Preferences.movieignorepart
        ScrapeFullCertCheckBox              .Checked        = Preferences.scrapefullcert
        cbMovieRenameEnable                 .Checked        = Preferences.MovieRenameEnable
        cbMovNewFolderInRootFolder          .Checked        = Preferences.MovNewFolderInRootFolder
        cbMovFolderRename                   .Checked        = Preferences.MovFolderRename
        cbMovSetIgnArticle                  .Checked        = Preferences.MovSetIgnArticle
        cbMovSortIgnArticle                 .Checked        = Preferences.MovSortIgnArticle
        cbMovTitleIgnArticle                .Checked        = Preferences.MovTitleIgnArticle
        cbMovTitleCase                      .Checked        = Preferences.MovTitleCase
        cbExcludeMpaaRated                  .Checked        = Preferences.ExcludeMpaaRated
        cbMovThousSeparator                 .Checked        = Preferences.MovThousSeparator
        cbRenameUnderscore                  .Checked        = Preferences.MovRenameSpaceCharacter
        If Preferences.RenameSpaceCharacter = "_" Then
            rbRenameUnderscore.Checked = True
        Else
            rbRenameFullStop.Checked = True
        End If
        CheckBox_ShowDateOnMovieList        .Checked        = Preferences.showsortdate
        cbImdbgetTMDBActor                  .Checked        = Preferences.TmdbActorsImdbScrape
        'cbTMDBPreferredCertCountry          .Checked        = Preferences.TMDBPreferredCertCountry
        cbImdbPrimaryPlot                   .Checked        = Preferences.ImdbPrimaryPlot
        cbXbmcTmdbRename                    .Checked        = Preferences.XbmcTmdbRenameMovie
        cbXbmcTmdbOutlineFromImdb           .Checked        = Preferences.XbmcTmdbMissingFromImdb
        cbXbmcTmdbTop250FromImdb            .Checked        = Preferences.XbmcTmdbTop250FromImdb 
        cbXbmcTmdbVotesFromImdb             .Checked        = Preferences.XbmcTmdbVotesFromImdb 
        cbXbmcTmdbCertFromImdb              .Checked        = Preferences.XbmcTmdbCertFromImdb
        cbXbmcTmdbStarsFromImdb             .Checked        = Preferences.XbmcTmdbStarsFromImdb 
        cbXbmcTmdbActorDL                   .Checked        = Preferences.XbmcTmdbActorDL
        saveactorchkbx                      .Checked        = Preferences.actorsave
        cb_LocalActorSaveAlpha              .Checked        = Preferences.actorsavealpha

        localactorpath              .Enabled        = Preferences.actorsave
        xbmcactorpath               .Enabled        = Preferences.actorsave
        Button77                    .Enabled        = Preferences.actorsave

        If Not Preferences.usefoldernames and Not Preferences.allfolders then
            cbMovCreateFolderjpg.Enabled = False
            cbMovCreateFanartjpg.Enabled = False
            cbMovieFanartInFolders.Enabled = False
            cbMoviePosterInFolder.Enabled = False
            Preferences.fanartjpg=False
            Preferences.posterjpg=False
        Else
            cbMovieFanartInFolders  .Checked    = Preferences.fanartjpg
            cbMoviePosterInFolder   .Checked    = Preferences.posterjpg
        End If

        cmbxMovXtraFanartQty.SelectedIndex = cmbxMovXtraFanartQty.FindStringExact(Preferences.movxtrafanartqty.ToString)


        Select Case Preferences.maxactors
            Case 9999
                ComboBox7.SelectedItem = "All Available"
            Case 0
                ComboBox7.SelectedItem = "None"
            Case Else
                ComboBox7.SelectedItem = Preferences.maxactors.ToString
        End Select

        Select Case Preferences.maxmoviegenre
            Case 99
                ComboBox6.SelectedItem = "All Available"
            Case 0
                ComboBox6.SelectedItem = "None"
            Case Else
                ComboBox6.SelectedItem = Preferences.maxmoviegenre.ToString
        End Select

        cb_keywordasTag.Checkstate = If(Preferences.keywordasTag, CheckState.Checked, CheckState.Unchecked)
        Select Case Preferences.keywordlimit 
            Case 999
                cb_keywordlimit.SelectedItem = "All Available"
            Case 0
                cb_keywordlimit.SelectedItem = "None"
            Case Else
                cb_keywordlimit.SelectedItem = Preferences.keywordlimit.ToString
        End Select

        If lbPosterSourcePriorities.Items.Count <> Preferences.moviethumbpriority.Count Then
            lbPosterSourcePriorities.Items.Clear()
            For f = 0 To Preferences.moviethumbpriority.Count-1
                lbPosterSourcePriorities.Items.Add(Preferences.moviethumbpriority(f))
            Next
        End If
        If lb_MovSepLst.Items.Count <> Preferences.MovSepLst.Count Then
            lb_MovSepLst.Items.Clear()
            For Each t In Preferences.MovSepLst 
                lb_MovSepLst.Items.Add(t)
            Next
        End If

        If ListBox11.Items.Count <> Preferences.certificatepriority.Length Then
            ListBox11.Items.Clear()
            For f = 0 To 33
                ListBox11.Items.Add(Preferences.certificatepriority(f))
            Next
        End If

        If lbVideoSource.Items.Count <> Preferences.releaseformat.Length Then
            lbVideoSource.Items.Clear()
            For f = 0 To Preferences.releaseformat.Length - 1
                lbVideoSource.Items.Add(Preferences.releaseformat(f))
            Next
        End If

        lbCleanFilename.Items.Clear()
        lbCleanFilename.Items.AddRange(Preferences.moviecleanTags.Split("|"))

        IMPA_chk.CheckState = If(Preferences.nfoposterscraper And 1, CheckState.Checked, CheckState.Unchecked)
        tmdb_chk.CheckState = If(Preferences.nfoposterscraper And 2, CheckState.Checked, CheckState.Unchecked)
        mpdb_chk.CheckState = If(Preferences.nfoposterscraper And 4, CheckState.Checked, CheckState.Unchecked)
        imdb_chk.CheckState = If(Preferences.nfoposterscraper And 8, CheckState.Checked, CheckState.Unchecked)


        If Preferences.movies_useXBMC_Scraper = True Then
            CheckBox_Use_XBMC_Scraper.CheckState = CheckState.Checked
            Read_XBMC_TMDB_Scraper_Config
        Else
            CheckBox_Use_XBMC_Scraper.CheckState = CheckState.Unchecked
            GroupBox_MovieIMDBMirror.Enabled = True
            GroupBox_MovieIMDBMirror.Visible = True
            GroupBox_MovieIMDBMirror.BringToFront()
        End If

        TMDbControlsIni()

        prefsload = False
        movieprefschanged = False
        btnMoviePrefSaveChanges.Enabled = False
    End Sub

    Private Sub TMDbControlsIni()
        TMDb.LoadLanguages(comboBoxTMDbSelectedLanguage)

        comboBoxTMDbSelectedLanguage.Text = Preferences.TMDbSelectedLanguageName
        cbUseCustomLanguage.Checked = Preferences.TMDbUseCustomLanguage
        tbCustomLanguageValue.Text = Preferences.TMDbCustomLanguageValue

        Movie.LoadBackDropResolutionOptions(comboBackDropResolutions, Preferences.BackDropResolutionSI) 'SI = Selected Index
        Movie.LoadHeightResolutionOptions(comboPosterResolutions, Preferences.PosterResolutionSI)
        Movie.LoadHeightResolutionOptions(comboActorResolutions, Preferences.ActorResolutionSI)

        cbGetMovieSetFromTMDb.Checked = Preferences.GetMovieSetFromTMDb

        SetLanguageControlsState()
    End Sub

    Private Sub SetLanguageControlsState()
        comboBoxTMDbSelectedLanguage.Enabled = Not cbUseCustomLanguage.Checked
        gbCustomLanguage.Enabled = cbUseCustomLanguage.Checked
    End Sub

    Private Sub mov_PreferencesDisplay()
        ListBox7.Items.Clear()
        For Each item In movieFolders
            ListBox7.Items.Add(item)
        Next
        ListBox15.Items.Clear()
        For Each item In Preferences.offlinefolders
            ListBox15.Items.Add(item)
        Next
        lbCleanFilename.Items.Clear()
        lbCleanFilename.Items.AddRange(Preferences.moviecleanTags.Split("|"))
        lb_MovSepLst.Items.Clear()
        For Each t In Preferences.MovSepLst
            lb_MovSepLst.Items.Add(t)
        Next
    End Sub


#Region "General Preferences"
    
    Private Sub TabPage18_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage18.Leave
        Try
            If generalprefschanged = True Then
                Dim tempint As Integer = MessageBox.Show("You appear to have made changes to your preferences," & vbCrLf & "Do wish to save the changes", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.Yes Then
                    btnGeneralPrefsSaveChanges.PerformClick 
                    MsgBox("Changes Saved")
                Else
                    Me.util_ConfigLoad(True)
                End If
                generalprefschanged = False
                btnGeneralPrefsSaveChanges.Enabled = False
            End If
            If Preferences.font <> Nothing Then
                If Preferences.font <> "" Then
                    Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                    Dim newFont As System.Drawing.Font = CType(tc.ConvertFromString(Preferences.font), System.Drawing.Font)
                    Call util_FontSetup()
                End If
            End If

            SetcbBtnLink
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnGeneralPrefsSaveChanges_Click(sender As System.Object, e As System.EventArgs) Handles btnGeneralPrefsSaveChanges.Click
        Preferences.ExcludeFolders.PopFromTextBox(tbExcludeFolders)

        Preferences.ConfigSave()

        If CopyOfPreferencesIgnoreArticle <> Preferences.ignorearticle Then
            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        End If

        generalprefschanged = False
        btnGeneralPrefsSaveChanges.Enabled = False
    End Sub

#Region "General"

    Private Sub RadioButton38_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton38.CheckedChanged
        If RadioButton38.Checked = True Then
            Preferences.videomode = 1
        End If
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub RadioButton37_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton37.CheckedChanged
        If RadioButton37.Checked = True Then
            Preferences.videomode = 2
        End If
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub RadioButton36_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton36.CheckedChanged
        If RadioButton36.Checked = True Then
            Preferences.videomode = 4
            btn_custommediaplayer.Enabled = True
            Label121.Visible = True
            If Preferences.selectedvideoplayer <> Nothing Then
                If Preferences.selectedvideoplayer <> "" Then
                    Label121.Text = Preferences.selectedvideoplayer
                Else
                    Label121.Text = ""
                End If
            Else
                Label121.Text = ""
            End If
        Else
            Label121.Visible = False
            btn_custommediaplayer.Enabled = False
        End If
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub btn_custommediaplayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_custommediaplayer.Click
        Try
            Dim filebrowser As New OpenFileDialog
            Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            filebrowser.InitialDirectory = mstrProgramFilesPath
            filebrowser.Filter = "Executable Files|*.exe"
            filebrowser.Title = "Find Executable Of Preferred Media Player"
            If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                Preferences.selectedvideoplayer = filebrowser.FileName
                Label121.Visible = True
                Label121.Text = Preferences.selectedvideoplayer
            End If
            If prefsload Then Exit Sub
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbx_minrarsize.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If txtbx_minrarsize.Text <> "" Then
                    e.Handled = True
                Else
                    MsgBox("Please Enter at least 0")
                    txtbx_minrarsize.Text = "8"
                End If
            End If
            If txtbx_minrarsize.Text = "" Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Exit Sub
            End If
            If Not IsNumeric(txtbx_minrarsize.Text) Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Exit Sub
            End If
            Preferences.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbx_minrarsize.TextChanged
        If IsNumeric(txtbx_minrarsize.Text) Then
            Preferences.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Else
            Preferences.rarsize = 8
            txtbx_minrarsize.Text = "8"
        End If
        If prefsload Then Exit Sub
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub CheckBox33_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox33.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.actorseasy = CheckBox33.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub Button96_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button96.Click
        Try
            Dim dlg As FontDialog = New FontDialog()
            Dim res As DialogResult = dlg.ShowDialog()
            If res = Windows.Forms.DialogResult.OK Then
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                Dim fontString As String = tc.ConvertToString(dlg.Font)

                Preferences.font = fontString

                Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Preferences.font), System.Drawing.Font)

                Label130.Font = newFont
                Label130.Text = fontString
                generalprefschanged = True
                btnGeneralPrefsSaveChanges.Enabled = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button111_Click(sender As System.Object, e As System.EventArgs) Handles Button111.Click
        Try
            'Reset Font
            'Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
            'Dim fontString As String = tc.ConvertToString("Times New Roman, 9pt")

            Preferences.font = "Times New Roman, 9pt"

            Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
            Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Preferences.font), System.Drawing.Font)

            Label130.Font = newFont
            Label130.Text = "Times New Roman, 9pt"
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub rbXBMCv_pre_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_pre.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_pre.Checked Then
            Preferences.XBMC_version = 0
        End If
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub rbXBMCv_post_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_post.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_post.Checked Then
            Preferences.XBMC_version = 2
        End If
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub rbXBMCv_both_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_both.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_both.Checked Then
            Preferences.XBMC_version = 1
        End If
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub btnMkvMergeGuiPath_Click( sender As Object,  e As EventArgs) Handles btnMkvMergeGuiPath.Click

        Dim ofd As New OpenFileDialog

        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        ofd.Filter           = "Executable Files|*.exe"
        ofd.Title            = "Locate mkvmerge GUI (mmg.exe)"

        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then Preferences.MkvMergeGuiPath = ofd.FileName

        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
        
    End Sub

    Private Sub llMkvMergeGuiPath_Click( sender As Object,  e As EventArgs) Handles llMkvMergeGuiPath.Click
        OpenUrl("http://www.downloadbestsoft.com/MKVToolNix.html")
    End Sub

    Private Sub lblaltnfoeditorclear_Click( sender As Object,  e As EventArgs) Handles lblaltnfoeditorclear.Click
        tbaltnfoeditor.Text = ""
        Preferences.altnfoeditor = ""
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub btnaltnfoeditor_Click( sender As Object,  e As EventArgs) Handles btnaltnfoeditor.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        ofd.Filter           = "Executable Files|*.exe"
        ofd.Title            = "Locate Alternative nfo viewer-editor"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then 
            Preferences.altnfoeditor = ofd.FileName
            tbaltnfoeditor.Text = Preferences.altnfoeditor 
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        End If
    End Sub

    Private Sub CheckBox38_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox38.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.intruntime = CheckBox38.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cb_IgnoreThe_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreThe.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.ignorearticle = cb_IgnoreThe.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cb_IgnoreA_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreA.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.ignoreAarticle = cb_IgnoreA.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cb_IgnoreAn_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreAn.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.ignoreAn = cb_IgnoreAn.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cb_SorttitleIgnoreArticles_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_SorttitleIgnoreArticles.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.sorttitleignorearticle = cb_SorttitleIgnoreArticles.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbOverwriteArtwork_CheckedChanged(sender As Object, e As EventArgs) Handles cbOverwriteArtwork.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.overwritethumbs = Not cbOverwriteArtwork.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbExternalbrowser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExternalbrowser.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.externalbrowser = cbExternalbrowser.Checked
        btnFindBrowser.Enabled      = cbExternalbrowser.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub btnFindBrowser_Click(sender As System.Object, e As System.EventArgs) Handles btnFindBrowser.Click
        Try
            Dim filebrowser As New OpenFileDialog
            Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            filebrowser.InitialDirectory = mstrProgramFilesPath
            filebrowser.Filter = "Executable Files|*.exe"
            filebrowser.Title = "Find Executable Of Preferred Browser"
            If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                Preferences.selectedBrowser = filebrowser.FileName
            End If
            If prefsload Then Exit Sub
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_disablecache_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_disablecache.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.startupCache = Not chkbx_disablecache.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbUseMultipleThreads_CheckedChanged( sender As Object,  e As EventArgs) Handles cbUseMultipleThreads.CheckedChanged
        If MainFormLoadedStatus Then
            Preferences.UseMultipleThreads = cbUseMultipleThreads.Checked
            If prefsload Then Exit Sub
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        End If
    End Sub

    Private Sub cbCheckForNewVersion_CheckedChanged( sender As Object,  e As EventArgs) Handles cbCheckForNewVersion.CheckedChanged
        If MainFormLoadedStatus Then
            Preferences.CheckForNewVersion = cbCheckForNewVersion.Checked
            If prefsload Then Exit Sub
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        End If
    End Sub

    Private Sub cbDisplayLocalActor_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbDisplayLocalActor.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.LocalActorImage = cbDisplayLocalActor.Checked = True
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbShowLogOnError_CheckedChanged( sender As Object,  e As EventArgs) Handles cbShowLogOnError.CheckedChanged
        If MainFormLoadedStatus Then
            Preferences.ShowLogOnError = cbShowLogOnError.Checked
            If prefsload Then Exit Sub
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        End If
    End Sub

    Private Sub cbShowMovieGridToolTip_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbShowMovieGridToolTip.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.ShowMovieGridToolTip = cbShowMovieGridToolTip.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub CheckBoxRenameNFOtoINFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRenameNFOtoINFO.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.renamenfofiles = CheckBoxRenameNFOtoINFO.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbDisplayRatingOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayRatingOverlay.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.DisplayRatingOverlay = cbDisplayRatingOverlay.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbDisplayMediaInfoOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayMediaInfoOverlay.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.DisplayMediainfoOverlay = cbDisplayMediaInfoOverlay.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbDisplayMediaInfoFolderSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayMediaInfoFolderSize.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.DisplayMediaInfoFolderSize = cbDisplayMediaInfoFolderSize.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbMultiMonitorEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMultiMonitorEnable.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.MultiMonitoEnabled = cbMultiMonitorEnable.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub cbShowAllAudioTracks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowAllAudioTracks.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.ShowAllAudioTracks = cbShowAllAudioTracks.Checked
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

    Private Sub tbExcludeFolders_Validating( sender As Object,  e As CancelEventArgs) Handles tbExcludeFolders.Validating
        If prefsload Then Exit Sub
        If Preferences.ExcludeFolders.Changed(tbExcludeFolders) And Not prefsload Then
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        End If
    End Sub

    Private Sub tbExcludeFolders_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbExcludeFolders.TextChanged
        'Preferences.ExcludeFolders = ExcludeFolders.Text
        generalprefschanged = True
        btnGeneralPrefsSaveChanges.Enabled = True
    End Sub

'Profiles
    Private Sub Button79_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button79.Click
        Try
            For Each pro In profileStruct.ProfileList
                If pro.ProfileName.ToLower = TextBox42.Text.ToLower Then
                    MsgBox("This Profile Already Exists" & vbCrLf & "Please Select Another Name")
                    Exit Sub
                End If
            Next
            Dim done As Boolean = False
            Dim tempint As Integer = 0
            For f = 1 To 1000
                Dim tempstring2 As String = applicationPath & "\Settings\"
                Dim configpath As String = tempstring2 & "config" & f.ToString & ".xml"
                Dim actorcachepath As String = tempstring2 & "actorcache" & f.ToString & ".xml"
                Dim directorcachepath As String = tempstring2 & "directorcache" & f.ToString & ".xml"
                Dim filterspath As String = tempstring2 & "filters" & f.ToString & ".txt"
                Dim genrespath As String = tempstring2 & "genres" & f.ToString & ".txt"
                Dim moviecachepath As String = tempstring2 & "moviecache" & f.ToString & ".xml"
                Dim regexpath As String = tempstring2 & "regex" & f.ToString & ".xml"
                Dim tvcachepath As String = tempstring2 & "tvcache" & f.ToString & ".xml"
                Dim musicvideocachepath As String = tempstring2 & "musicvideocache" & f.ToString & ".xml"
                Dim ok As Boolean = True
                If File.Exists(configpath) Then ok = False
                If File.Exists(actorcachepath) Then ok = False
                If File.Exists(directorcachepath) Then ok = False
                If File.Exists(filterspath) Then ok = False
                If File.Exists(genrespath) Then ok = False
                If File.Exists(moviecachepath) Then ok = False
                If File.Exists(regexpath) Then ok = False
                If File.Exists(tvcachepath) Then ok = False
                If File.Exists(musicvideocachepath) Then ok = False
                If ok = True Then
                    tempint = f
                    Exit For
                End If
            Next
            'new profilename
            Dim tempstring As String = applicationPath & "\Settings\"
            Dim moviecachetocopy        As String = String.Empty
            Dim actorcachetocopy        As String = String.Empty
            Dim musiccachetocopy        As String = String.Empty
            Dim musicvideocachetocopy   As String = String.Empty
            Dim directorcachetocopy     As String = String.Empty
            Dim tvcachetocopy           As String = String.Empty
            Dim configtocopy            As String = String.Empty
            Dim filterstocopy           As String = String.Empty
            Dim genrestocopy            As String = String.Empty
            Dim regextocopy             As String = String.Empty
            Dim moviesetcachetocopy     As String = String.Empty
            For Each profs In profileStruct.ProfileList
                If profs.ProfileName = profileStruct.DefaultProfile Then
                    musicvideocachetocopy   = profs.MusicVideoCache
                    moviecachetocopy        = profs.MovieCache
                    actorcachetocopy        = profs.ActorCache
                    directorcachetocopy     = profs.DirectorCache
                    tvcachetocopy           = profs.TvCache
                    configtocopy            = profs.Config
                    filterstocopy           = profs.Filters
                    genrestocopy            = profs.Genres 
                    regextocopy             = profs.RegExList
                    moviesetcachetocopy     = profs.MovieSetCache 
                End If
            Next

            Dim profiletoadd As New ListOfProfiles
            profiletoadd.ActorCache         = tempstring & "actorcache" & tempint.ToString & ".xml"
            profiletoadd.DirectorCache      = tempstring & "directorcache" & tempint.ToString & ".xml"
            profiletoadd.Config             = tempstring & "config" & tempint.ToString & ".xml"
            profiletoadd.Filters            = tempstring & "filters" & tempint.ToString & ".txt"
            profiletoadd.Genres             = tempstring & "genres" & tempint.ToString & ".txt"
            profiletoadd.MovieCache         = tempstring & "moviecache" & tempint.ToString & ".xml"
            profiletoadd.RegExList          = tempstring & "regex" & tempint.ToString & ".xml"
            profiletoadd.TvCache            = tempstring & "tvcache" & tempint.ToString & ".xml"
            profiletoadd.MusicVideoCache    = tempstring & "musicvideocache" & tempint.ToString & ".xml"
            profiletoadd.MovieSetCache      = tempstring & "moviesetcache" & tempint.ToString & ".xml"
            profiletoadd.ProfileName        = TextBox42.Text
            profileStruct.ProfileList.Add(profiletoadd)

            If File.Exists(moviecachetocopy)        Then File.Copy(moviecachetocopy, profiletoadd.MovieCache)
            If File.Exists(musicvideocachetocopy)   Then File.Copy(musicvideocachetocopy, profiletoadd.MusicVideoCache)
            If File.Exists(actorcachetocopy)        Then File.Copy(actorcachetocopy, profiletoadd.ActorCache)
            If File.Exists(directorcachetocopy)     Then File.Copy(directorcachetocopy, profiletoadd.DirectorCache)
            If File.Exists(tvcachetocopy)           Then File.Copy(tvcachetocopy, profiletoadd.TvCache)
            If File.Exists(configtocopy)            Then File.Copy(configtocopy, profiletoadd.Config)
            If File.Exists(filterstocopy)           Then File.Copy(filterstocopy, profiletoadd.Filters)
            If File.Exists(genrestocopy)            Then File.Copy(genrestocopy, profiletoadd.Genres)
            If File.Exists(regextocopy)             Then File.Copy(regextocopy, profiletoadd.RegExList)
            If File.Exists(moviesetcachetocopy)     Then File.Copy(moviesetcachetocopy, profiletoadd.MovieSetCache)
            ListBox13.Items.Add(TextBox42.Text)
            Call util_ProfileSave()
            done = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button78_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button78.Click
        Try
            'setselected profile to default
            For Each prof In profileStruct.ProfileList
                If prof.ProfileName = ListBox13.SelectedItem Then
                    profileStruct.defaultprofile = prof.ProfileName
                    Label112.Text = "Current Default Profile: " & prof.ProfileName
                    Call util_ProfileSave()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button93_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button93.Click
        Try
            'setselected profile to startup
            For Each prof In profileStruct.ProfileList
                If prof.ProfileName = ListBox13.SelectedItem Then
                    profileStruct.startupprofile = prof.ProfileName
                    Label108.Text = "Current Startup Profile: " & prof.ProfileName
                    Call util_ProfileSave()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button80_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button80.Click
        Try
            'remove selected profile
            If ListBox13.SelectedItem = profileStruct.DefaultProfile Then
                MsgBox("You can't delete your default profile" & vbCrLf & "Set another Profile to default then delete it")
                Exit Sub
            End If
            If ListBox13.SelectedItem = profileStruct.StartupProfile Then
                MsgBox("You can't delete your startup profile" & vbCrLf & "Set another Profile to startup then delete it")
                Exit Sub
            End If
            If ListBox13.SelectedItem = workingProfile.profilename Then
                MsgBox("You can't delete a loaded profile" & vbCrLf & "Load another Profile then delete it")
                Exit Sub
            End If
            Dim tempint As Integer = MessageBox.Show("Removing a profile will delete all associated cache files and settings," & vbCrLf & "Are you sure you want to remove the selected profile", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tempint = DialogResult.Yes Then
                Dim tempint2 As Integer = 0
                For f = 0 To profileStruct.ProfileList.Count - 1
                    If profileStruct.profilelist(f).ProfileName = ListBox13.SelectedItem Then
                        tempint2 = f
                        Try
                            File.Delete(profileStruct.profilelist(f).ActorCache)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).DirectorCache)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).Config)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).Filters)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).Genres)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).MovieCache)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).MusicVideoCache)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).RegExList)
                        Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).TvCache)
                        Catch ex As Exception
                        End Try
                        Try
                            File.Delete(profileStruct.profilelist(f).MovieSetCache)
                        Catch ex As Exception
                        End Try
                        Exit For
                    End If
                Next
                profileStruct.ProfileList.RemoveAt(tempint2)
                ListBox13.Items.Clear()
                ProfilesToolStripMenuItem.DropDownItems.Clear()
                If profileStruct.ProfileList.Count > 1 Then
                    ProfilesToolStripMenuItem.Visible = True
                Else
                    ProfilesToolStripMenuItem.Visible = False
                End If
                ProfilesToolStripMenuItem.DropDownItems.Clear()
                For Each prof In profileStruct.ProfileList
                    ListBox13.Items.Add(prof.ProfileName)
                    ProfilesToolStripMenuItem.DropDownItems.Add(prof.ProfileName)
                Next
                util_ProfileSave()

            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

#End Region

#Region "Custom Commands"
    Private Sub btn_ToolsCommandAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ToolsCommandAdd.Click
        Try
            If TextBox41.Text <> "" And TextBox43.Text <> "" Then
                Dim allgood As Boolean = True
                For Each item In ListBox16.Items
                    If TextBox41.Text = item Then
                        allgood = False
                    End If
                Next
                If allgood Then
                    Dim newcom As New str_ListOfCommands(SetDefaults)
                    newcom.command = TextBox43.Text
                    newcom.title = TextBox41.Text
                    Preferences.commandlist.Add(newcom)
                    ListBox16.Items.Add(newcom.title)
                    ListBox17.Items.Add(newcom.command)
                    Dim x As Integer = ToolsToolStripMenuItem.DropDownItems.Count
                    For i = x-1 To MCToolsCommands Step -1
                        ToolsToolStripMenuItem.DropDownItems.RemoveAt(i)
                    Next
                    For Each com In Preferences.commandlist
                        ToolsToolStripMenuItem.DropDownItems.Add(com.title)
                    Next
                    If prefsload Then Exit Sub
                    generalprefschanged = True
                    btnGeneralPrefsSaveChanges.Enabled = True
                Else
                    MsgBox("Title already exists in list")
                End If
            Else
                MsgBox("This feature needs both a title & command")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox16_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox16.SelectedIndexChanged
        Try
            ListBox17.SelectedIndex = ListBox16.SelectedIndex
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox17_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox17.SelectedIndexChanged
        Try
            ListBox16.SelectedIndex = ListBox17.SelectedIndex
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_ToolsCommandRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ToolsCommandRemove.Click
        Try
            If ListBox16.SelectedItem <> "" And ListBox17.SelectedItem <> "" Then
                For Each com In Preferences.commandlist
                    If com.title = ListBox16.SelectedItem And com.command = ListBox17.SelectedItem Then
                        Preferences.commandlist.Remove(com)
                        Exit For
                    End If
                Next
                ListBox16.Items.Clear()
                ListBox17.Items.Clear()
                Dim x As Integer = ToolsToolStripMenuItem.DropDownItems.Count
                For i = x-1 To MCToolsCommands Step -1
                    ToolsToolStripMenuItem.DropDownItems.RemoveAt(i)
                Next
                For Each com In Preferences.commandlist
                    ListBox16.Items.Add(com.title)
                    ListBox17.Items.Add(com.command)
                    ToolsToolStripMenuItem.DropDownItems.Add(com.title)
                Next
            Else
                MsgBox("Nothing selected to remove")
            End If
            If prefsload Then Exit Sub
            generalprefschanged = True
            btnGeneralPrefsSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#End Region 'General Preferences -> Custom Commands Tab

#End Region  'General Preferences Tab

#Region "Tv Preferences"

    Private Sub TabPage24_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage24.Leave
        Try
            If tvprefschanged = True Then
                Dim tempint As Integer = MessageBox.Show("You appear to have made changes to your preferences," & vbCrLf & "Do wish to save the changes", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.Yes Then

                    Call util_RegexSave()
                    Preferences.ConfigSave()
                    MsgBox("Changes Saved")
                Else

                    Me.util_ConfigLoad(True)
                    Call util_RegexLoad()
                End If
                tvprefschanged = False
                btnTVPrefSaveChanges.Enabled = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTVPrefSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVPrefSaveChanges.Click
        Try
            Preferences.ConfigSave()
            Call util_RegexSave()
            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In tv_RegexRename
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            'MsgBox("Changes Saved!" & vbCrLf & vbCrLf & "Please restart the program" & vbCrLf & "for the changes to take effect")
            tvprefschanged = False
            btnTVPrefSaveChanges.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#Region "General/Scraper"

'XBMC TVDB Scraper options
    Private Sub RadioButton_XBMC_Scraper_TVDB_DVDOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_XBMC_Scraper_TVDB_DVDOrder.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If RadioButton_XBMC_Scraper_TVDB_DVDOrder.Checked = True Then
                Save_XBMC_TVDB_Scraper_Config("dvdorder", "true")
                Save_XBMC_TVDB_Scraper_Config("absolutenumber", "false")
            Else
                Save_XBMC_TVDB_Scraper_Config("dvdorder", "false")
                Save_XBMC_TVDB_Scraper_Config("absolutenumber", "true")
            End If
            'Read_XBMC_TVDB_Scraper_Config()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox_XBMC_Scraper_TVDB_Fanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_XBMC_Scraper_TVDB_Fanart.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If CheckBox_XBMC_Scraper_TVDB_Fanart.Checked = True Then
                Save_XBMC_TVDB_Scraper_Config("fanart", "true")
            Else
                Save_XBMC_TVDB_Scraper_Config("fanart", "false")
            End If
            'Read_XBMC_TVDB_Scraper_Config()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox_XBMC_Scraper_TVDB_Posters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_XBMC_Scraper_TVDB_Posters.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If CheckBox_XBMC_Scraper_TVDB_Posters.Checked = True Then
                Save_XBMC_TVDB_Scraper_Config("posters", "true")
            Else
                Save_XBMC_TVDB_Scraper_Config("posters", "false")
            End If
            'Read_XBMC_TVDB_Scraper_Config()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBox_TVDB_Language_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_TVDB_Language.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            Save_XBMC_TVDB_Scraper_Config("language", ComboBox_TVDB_Language.Text)
            'Read_XBMC_TVDB_Scraper_Config()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Endof - XBMC TVDB Scraper options

    Private Sub Button91_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button91.Click
        Try
            ListBox12.Items.Clear()
            languageList.Clear()
            util_LanguageListLoad()

            Try
                ListBox12.SelectedItem = Preferences.TvdbLanguage
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox12_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox12.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            For Each lan In languageList
                If lan.Language.Value = ListBox12.SelectedItem Then
                    Preferences.TvdbLanguage = lan.Language.Value
                    Preferences.TvdbLanguageCode = lan.Abbreviation.Value
                    Exit For
                End If
            Next
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton42_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton42.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If RadioButton42.Checked = True Then
                Preferences.sortorder = "dvd"
            Else
                Preferences.sortorder = "default"
            End If
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    'Private Sub RadioButton43_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton43.CheckedChanged
    '    Try
    '        If RadioButton43.Checked = True Then
    '            Preferences.sortorder = "default"
    '        Else
    '            Preferences.sortorder = "dvd"
    '        End If
    '        tvprefschanged = True
    '        btnTVPrefSaveChanges.Enabled = True
    '    Catch ex As Exception
    '        ExceptionHandler.LogError(ex)
    '    End Try
    'End Sub

    Private Sub CheckBox34_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox34.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If CheckBox34.Checked = True Then
                Preferences.copytvactorthumbs = True
            Else
                Preferences.copytvactorthumbs = False
            End If
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox_Use_XBMC_TVDB_Scraper_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Use_XBMC_TVDB_Scraper.CheckedChanged
        Try
            If CheckBox_Use_XBMC_TVDB_Scraper.CheckState = CheckState.Checked Then
                Preferences.tvshow_useXBMC_Scraper = True
                GroupBox2.Enabled = False
                GroupBox3.Enabled = False
                GroupBox5.Enabled = False
                GroupBox22.Visible = False
                GroupBox22.SendToBack()
                GroupBox_TVDB_Scraper_Preferences.Visible = True
                GroupBox_TVDB_Scraper_Preferences.BringToFront()
            Else
                Preferences.tvshow_useXBMC_Scraper = False
                GroupBox2.Enabled = True
                GroupBox3.Enabled = True
                GroupBox5.Enabled = True
                GroupBox22.Visible = True
                GroupBox22.BringToFront()
                GroupBox_TVDB_Scraper_Preferences.Visible = False
                GroupBox_TVDB_Scraper_Preferences.SendToBack()
            End If
            Read_XBMC_TVDB_Scraper_Config()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'TvShow Auto Scrape Options
    Private Sub cbTvDlPosterArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlPosterArt.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.tvdlposter = cbTvDlPosterArt.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTvDlFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlFanart.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.tvdlfanart = cbTvDlFanart.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTvDlSeasonArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlSeasonArt.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.tvdlseasonthumbs = cbTvDlSeasonArt.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTvDlXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbTvDlXtraFanart.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.dlTVxtrafanart = cbTvDlXtraFanart.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cmbxTvXtraFanartQty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxTvXtraFanartQty.SelectedIndexChanged
        If prefsload Then Exit Sub
        Dim newvalue As String = cmbxTvXtraFanartQty.SelectedItem
        Preferences.TvXtraFanartQty = newvalue.toint
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTvDlFanartTvArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvDlFanartTvArt.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.TvDlFanartTvArt = cbTvDlFanartTvArt.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub   'TvFanartTvFirst

    Private Sub cbTvFanartTvFirst_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvFanartTvFirst.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.TvFanartTvFirst = cbTvFanartTvFirst.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub   'TvFanartTvFirst

    Private Sub RadioButton41_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton41.CheckedChanged
        If prefsload Then Exit Sub
        If RadioButton41.Checked = True Then Preferences.seasonall = "none"
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub RadioButton40_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton40.CheckedChanged
        If prefsload Then Exit Sub
        If RadioButton40.Checked = True Then Preferences.seasonall = "poster"
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub RadioButton39_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton39.CheckedChanged
        If prefsload Then Exit Sub
        If RadioButton39.Checked = True Then Preferences.seasonall = "wide"
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub posterbtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles posterbtn.CheckedChanged
        If prefsload Then Exit Sub
        If posterbtn.Checked = True Then Preferences.postertype = "poster"
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub bannerbtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bannerbtn.CheckedChanged
        If prefsload Then Exit Sub
        If bannerbtn.Checked = True Then Preferences.postertype = "banner"
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cb_TvFolderJpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_TvFolderJpg.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.tvfolderjpg = cb_TvFolderJpg.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbSeasonFolderjpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeasonFolderjpg.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.seasonfolderjpg = cbSeasonFolderjpg.checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

'End Of - TvShow Auto Scrape Options

    Private Sub cbTvQuickAddShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvQuickAddShow.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.tvshowautoquick =cbTvQuickAddShow.Checked 
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.enabletvhdtags =CheckBox20.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTvAutoScreenShot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvAutoScreenShot.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.autoepisodescreenshot = cbTvAutoScreenShot.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTvScrShtTVDBResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvScrShtTVDBResize.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.tvscrnshtTVDBResize = cbTvScrShtTVDBResize.checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub AutoScrnShtDelay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AutoScrnShtDelay.KeyPress
            Try
                If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                    If AutoScrnShtDelay.Text <> "" Then
                        e.Handled = True
                    Else
                        MsgBox("Please Enter at least 1")
                        AutoScrnShtDelay.Text = "10"
                    End If
                End If
                If AutoScrnShtDelay.Text = "" Then
                    MsgBox("Please enter a numerical Value that is 1 or more")
                    AutoScrnShtDelay.Text = "10"
                    Exit Sub
                End If
                If Not IsNumeric(AutoScrnShtDelay.Text) Then
                    MsgBox("Please enter a numerical Value that is 1 or more")
                    AutoScrnShtDelay.Text = "10"
                    Exit Sub
                End If
                'Preferences.ScrShtDelay = Convert.ToInt32(AutoScrnShtDelay.Text)
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try

    End Sub

    Private Sub AutoScrnShtDelay_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoScrnShtDelay.TextChanged
        If prefsload Then Exit Sub
        If IsNumeric(AutoScrnShtDelay.Text) AndAlso Convert.ToInt32(AutoScrnShtDelay.Text)>0 Then
            Preferences.ScrShtDelay = Convert.ToInt32(AutoScrnShtDelay.Text)
        Else
            Preferences.ScrShtDelay = 10
            AutoScrnShtDelay.Text = "10"
            MsgBox("Please enter a numerical Value that is 1 or more")
        End If
        If tvprefschanged = False Then
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        End If
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.disabletvlogs = CheckBox17.Checked
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            Preferences.TvdbActorScrape = ComboBox8.SelectedIndex.ToString
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBox_tv_EpisodeRename_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_tv_EpisodeRename.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try
            If Renamer.setRenamePref(tv_RegexRename.Item(ComboBox_tv_EpisodeRename.SelectedIndex), tv_RegexScraper) Then
                Preferences.tvrename = ComboBox_tv_EpisodeRename.SelectedIndex
                tvprefschanged = True
                btnTVPrefSaveChanges.Enabled = True
            Else
                MsgBox("Format does not match scraper regex" & vbCrLf & "Please check")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox_tv_EpisodeRenameAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_tv_EpisodeRenameAuto.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.autorenameepisodes = CheckBox_tv_EpisodeRenameAuto.Checked 
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub CheckBox_tv_EpisodeRenameCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_tv_EpisodeRenameCase.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If CheckBox_tv_EpisodeRenameCase.CheckState = CheckState.Checked Then
                Preferences.eprenamelowercase = True
            Else
                Preferences.eprenamelowercase = False
            End If
            Renamer.applySeasonEpisodeCase()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvMissingSpecials_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvMissingSpecials.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.ignoreMissingSpecials = cbTvMissingSpecials.Checked 
        tvprefschanged = True
        btnTVPrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbTv_fixNFOid_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbTv_fixNFOid.CheckedChanged
        Preferences.fixnfoid =cbTv_fixNFOid.Checked 
    End Sub

#End Region   'TV Preferences -> General/Scraper Tab

#Region "Regex Tab"

'Scrape
    Private Sub ListBox_tv_RegexScrape_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox_tv_RegexScrape.SelectedIndexChanged
        Try
            If ListBox_tv_RegexScrape.SelectedItem <> Nothing Then
                TextBox_tv_RegexScrape_Edit.Text = ListBox_tv_RegexScrape.SelectedItem
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_MoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_MoveUp.Click
        'up
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.ListBox_tv_RegexScrape.SelectedIndex <> 0 Then
                mSelectedIndex = Me.ListBox_tv_RegexScrape.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                ListBox_tv_RegexScrape.Items.Insert(mSelectedIndex + 1, ListBox_tv_RegexScrape.Items(mOtherIndex))
                ListBox_tv_RegexScrape.Items.RemoveAt(mOtherIndex)
            End If
            tv_RegexScraper.Clear()
            For Each item In ListBox_tv_RegexScrape.Items
                tv_RegexScraper.Add(item)
            Next
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_MoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_MoveDown.Click
        'down
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.ListBox_tv_RegexScrape.SelectedIndex <> Me.ListBox_tv_RegexScrape.Items.Count - 1 Then
                mSelectedIndex = Me.ListBox_tv_RegexScrape.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                ListBox_tv_RegexScrape.Items.Insert(mSelectedIndex, ListBox_tv_RegexScrape.Items(mOtherIndex))
                ListBox_tv_RegexScrape.Items.RemoveAt(mOtherIndex + 1)
            End If
            tv_RegexScraper.Clear()
            For Each item In ListBox_tv_RegexScrape.Items
                tv_RegexScraper.Add(item)
            Next
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_Edit.Click
        Try
            If TextBox_tv_RegexScrape_Edit.Text = "" Then
                MsgBox("No Text")
                Exit Sub
            End If
            If Not util_RegexValidate(TextBox_tv_RegexScrape_Edit.Text) Then
                MsgBox("Invalid Regex")
                Exit Sub
            End If
            Dim tempint As Integer = ListBox_tv_RegexScrape.SelectedIndex
            ListBox_tv_RegexScrape.Items.RemoveAt(tempint)
            ListBox_tv_RegexScrape.Items.Insert(tempint, TextBox_tv_RegexScrape_Edit.Text)
            ListBox_tv_RegexScrape.SelectedIndex = tempint
            tv_RegexScraper.Clear()
            For Each regexp In ListBox_tv_RegexScrape.Items
                tv_RegexScraper.Add(regexp)
            Next
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_Add.Click
        Try
            'add textbox49
            If Not util_RegexValidate(TextBox_tv_RegexScrape_New.Text) Then
                MsgBox("Invalid Regex")
                Exit Sub
            End If
            ListBox_tv_RegexScrape.Items.Add(TextBox_tv_RegexScrape_New.Text)
            tv_RegexScraper.Add(TextBox_tv_RegexScrape_New.Text)

            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_Remove.Click
        Try
            'remove selected
            Dim tempstring = ListBox_tv_RegexScrape.SelectedItem
            Try
                ListBox_tv_RegexScrape.Items.Remove(ListBox_tv_RegexScrape.SelectedItem)
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
            For Each regexp In tv_RegexScraper
                If regexp = tempstring Then
                    tv_RegexScraper.Remove(regexp)
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_Test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_Test.Click
        Try
            If TextBox_tv_RegexScrape_TestString.Text = "" Then
                MsgBox("Please Enter a filename or any string to test")
                Exit Sub
            End If
            If ListBox_tv_RegexScrape.SelectedItem = Nothing Then
                MsgBox("Please Select a Regex to test")
                Exit Sub
            End If
            TextBox_tv_RegexScrape_TestResult.Text = ""
            Dim tvseries As String
            Dim tvepisode As String
            Dim s As String
            Dim tempstring As String = TextBox_tv_RegexScrape_TestString.Text
            s = tempstring '.ToLower
            Dim M As Match


            M = Regex.Match(s, ListBox_tv_RegexScrape.SelectedItem)
            If M.Success = True Then
                Try
                    tvseries = M.Groups(1).Value
                    tvepisode = M.Groups(2).Value
                Catch
                    tvseries = "-1"
                    tvepisode = "-1"
                End Try
                Try
                    If tvseries <> "-1" Then
                        TextBox_tv_RegexScrape_TestResult.Text = "Series No = " & tvseries & vbCrLf
                    End If
                    If tvepisode <> "-1" Then
                        TextBox_tv_RegexScrape_TestResult.Text = TextBox_tv_RegexScrape_TestResult.Text & "Episode No = " & tvepisode
                    End If
                Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                End Try
            Else
                TextBox_tv_RegexScrape_TestResult.Text = "No Matches"
            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexScrape_Restore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_tv_RegexScrape_Restore.Click
        Try
            util_RegexSetDefaultScraper()
            ListBox_tv_RegexScrape.Items.Clear()
            For Each Regex In tv_RegexScraper
                ListBox_tv_RegexScrape.Items.Add(Regex)
            Next
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Rename
    Private Sub ListBox_tv_RegexRename_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox_tv_RegexRename.SelectedIndexChanged
        Try
            If ListBox_tv_RegexRename.SelectedItem <> Nothing Then
                TextBox_tv_RegexRename_Edit.Text = ListBox_tv_RegexRename.SelectedItem
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexRename_MoveDown_Click(sender As System.Object, e As System.EventArgs) Handles Button_tv_RegexRename_MoveDown.Click
        Try
            'down
            Try
                Dim mSelectedIndex, mOtherIndex As Integer
                If Me.ListBox_tv_RegexRename.SelectedIndex <> Me.ListBox_tv_RegexRename.Items.Count - 1 Then
                    mSelectedIndex = Me.ListBox_tv_RegexRename.SelectedIndex
                    mOtherIndex = mSelectedIndex + 1
                    ListBox_tv_RegexRename.Items.Insert(mSelectedIndex, ListBox_tv_RegexRename.Items(mOtherIndex))
                    ListBox_tv_RegexRename.Items.RemoveAt(mOtherIndex + 1)
                End If
                tv_RegexRename.Clear()
                For Each item In ListBox_tv_RegexRename.Items
                    tv_RegexRename.Add(item)
                Next
                tvprefschanged = True
                btnTVPrefSaveChanges.Enabled = True
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexRename_MoveUp_Click(sender As System.Object, e As System.EventArgs) Handles Button_tv_RegexRename_MoveUp.Click
        Try
            'up
            Try
                Dim mSelectedIndex, mOtherIndex As Integer
                If Me.ListBox_tv_RegexRename.SelectedIndex <> 0 Then
                    mSelectedIndex = Me.ListBox_tv_RegexRename.SelectedIndex
                    mOtherIndex = mSelectedIndex - 1
                    ListBox_tv_RegexRename.Items.Insert(mSelectedIndex + 1, ListBox_tv_RegexRename.Items(mOtherIndex))
                    ListBox_tv_RegexRename.Items.RemoveAt(mOtherIndex)
                End If
                tv_RegexRename.Clear()
                For Each item In ListBox_tv_RegexRename.Items
                    tv_RegexRename.Add(item)
                Next
                tvprefschanged = True
                btnTVPrefSaveChanges.Enabled = True
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexRename_Remove_Click(sender As Object, e As System.EventArgs) Handles Button_tv_RegexRename_Remove.Click
        Try
            Dim strRegexSelected = ListBox_tv_RegexRename.SelectedItem
            Dim idxRegexSelected = ListBox_tv_RegexRename.SelectedIndex

            Try
                ListBox_tv_RegexRename.Items.RemoveAt(idxRegexSelected)
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try

            For Each regexp In tv_RegexRename
                If regexp = strRegexSelected Then
                    tv_RegexRename.Remove(regexp)
                    Exit For
                End If
            Next

            TextBox_tv_RegexRename_Edit.Clear()

            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In tv_RegexRename
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.SelectedIndex = If(Preferences.tvrename >= idxRegexSelected, Preferences.tvrename - 1, Preferences.tvrename)

            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexRename_Add_Click(sender As Object, e As System.EventArgs) Handles Button_tv_RegexRename_Add.Click
        Try
            'add
            ListBox_tv_RegexRename.Items.Add(TextBox_tv_RegexRename_New.Text)
            tv_RegexRename.Add(TextBox_tv_RegexRename_New.Text)
            TextBox_tv_RegexRename_New.Clear()
            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In tv_RegexRename
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.SelectedIndex = Preferences.tvrename

            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexRename_Edit_Click(sender As Object, e As System.EventArgs) Handles Button_tv_RegexRename_Edit.Click
        Try
            If TextBox_tv_RegexRename_Edit.Text = "" Then
                MsgBox("No Text")
                Exit Sub
            End If
            Dim tempint As Integer = ListBox_tv_RegexRename.SelectedIndex
            ListBox_tv_RegexRename.Items.RemoveAt(tempint)
            ListBox_tv_RegexRename.Items.Insert(tempint, TextBox_tv_RegexRename_Edit.Text)
            ListBox_tv_RegexRename.SelectedIndex = tempint
            tv_RegexRename.Clear()
            For Each regexp In ListBox_tv_RegexRename.Items
                tv_RegexRename.Add(regexp)
            Next
            TextBox_tv_RegexRename_Edit.Clear()
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button_tv_RegexRename_Restore_Click(sender As Object, e As System.EventArgs) Handles Button_tv_RegexRename_Restore.Click
        Try
            util_RegexSetDefaultRename()
            ListBox_tv_RegexRename.Items.Clear()
            For Each Regex In tv_RegexRename
                ListBox_tv_RegexRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.Items.Clear()
            For Each Regex In tv_RegexRename
                ComboBox_tv_EpisodeRename.Items.Add(Regex)
            Next
            ComboBox_tv_EpisodeRename.SelectedIndex = Preferences.tvrename
            tvprefschanged = True
            btnTVPrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

#End Region 'TV Preferences -> Regex Tab

#End Region  'Tv Preferences Tab

#Region "Movie Preferences"

    Private Sub TabPage26_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage26.Leave
        Try
            If movieprefschanged Then
                Dim tempint As Integer = MessageBox.Show("You appear to have made changes to your preferences," & vbCrLf & "Do wish to save the changes", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.Yes Then
                    applyAdvancedLists()
                    Preferences.ConfigSave()
                Else
                    util_ConfigLoad(True)
                End If
            End If
            movieprefschanged = False
            btnMoviePrefSaveChanges.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMoviePrefSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoviePrefSaveChanges.Click
        Try
            applyAdvancedLists()

            For Each row As DataGridViewRow In DataGridViewMovies.Rows
                Dim m As Data_GridViewMovie = row.DataBoundItem
                m.ClearStoredCalculatedFields()
            Next

            UpdateFilteredList

            Mc.clsGridViewMovie.SetFirstColumnWidth(DataGridViewMovies)
            Mc.clsGridViewMovie.GridviewMovieDesign(Me)

            Preferences.ConfigSave()

            movieprefschanged = False
            btnMoviePrefSaveChanges.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#Region "Movie Preferences -> Scraper Tab"

'Choose Default Scraper
    Private Sub CheckBox_Use_XBMC_Scraper_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Use_XBMC_Scraper.CheckedChanged
        Try
            If CheckBox_Use_XBMC_Scraper.CheckState = CheckState.Checked Then
                Preferences.movies_useXBMC_Scraper = True
                Read_XBMC_TMDB_Scraper_Config()
                GroupBox_MovieIMDBMirror.Enabled = False
                GroupBox_MovieIMDBMirror.Visible = False
                GroupBox_TMDB_Scraper_Preferences.Enabled = True
                GroupBox_TMDB_Scraper_Preferences.Visible = True
                GroupBox_TMDB_Scraper_Preferences.BringToFront()
            Else
                Preferences.movies_useXBMC_Scraper = False
                GroupBox_MovieIMDBMirror.Enabled = True
                GroupBox_MovieIMDBMirror.Visible = True
                GroupBox_MovieIMDBMirror.BringToFront()
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

'XBMC Scraper Preferences - TMDB
    Private Sub cbXbmcTmdbFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXbmcTmdbFanart.CheckedChanged
        Try
            If cbXbmcTmdbFanart.Checked = True Then
                Preferences.XbmcTmdbScraperFanart = "true"
            Else
                Preferences.XbmcTmdbScraperFanart = "false"
            End If
            Save_XBMC_TMDB_Scraper_Config("fanart", Preferences.XbmcTmdbScraperFanart)
            'Read_XBMC_TMDB_Scraper_Config()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbXbmcTmdbIMDBRatings_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXbmcTmdbIMDBRatings.CheckedChanged, cbXbmcTmdbIMDBRatings.CheckStateChanged 
        Try
            If cbXbmcTmdbIMDBRatings.Checked = True Then
                Preferences.XbmcTmdbScraperRatings = "IMDb"
            Else
                Preferences.XbmcTmdbScraperRatings = "TMDb"
            End If
            Save_XBMC_TMDB_Scraper_Config("ratings", Preferences.XbmcTmdbScraperRatings)
            'Read_XBMC_TMDB_Scraper_Config()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbXbmcTmdbOutlineFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbOutlineFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.XbmcTmdbMissingFromImdb = cbXbmcTmdbOutlineFromImdb.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbXbmcTmdbTop250FromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbTop250FromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.XbmcTmdbTop250FromImdb = cbXbmcTmdbTop250FromImdb.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbXbmcTmdbVotesFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbVotesFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.XbmcTmdbVotesFromImdb = cbXbmcTmdbVotesFromImdb.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbXbmcTmdbStarsFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbStarsFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.XbmcTmdbStarsFromImdb = cbXbmcTmdbStarsFromImdb.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbXbmcTmdbCertFromImdb_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbCertFromImdb.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.XbmcTmdbCertFromImdb = cbXbmcTmdbCertFromImdb.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cmbxXbmcTmdbHDTrailer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxXbmcTmdbHDTrailer.SelectedIndexChanged
        Try
            Preferences.XbmcTmdbScraperTrailerQ = cmbxXbmcTmdbHDTrailer.Text
            Save_XBMC_TMDB_Scraper_Config("trailerq", Preferences.XbmcTmdbScraperTrailerQ)
            'Read_XBMC_TMDB_Scraper_Config()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxXbmcTmdbTitleLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxXbmcTmdbTitleLanguage.SelectedIndexChanged
        Try
            Preferences.XbmcTmdbScraperLanguage = cmbxXbmcTmdbTitleLanguage.Text
            Save_XBMC_TMDB_Scraper_Config("language", Preferences.XbmcTmdbScraperLanguage)
            mScraperManager = New ScraperManager(IO.Path.Combine(My.Application.Info.DirectoryPath, "Assets\scrapers"))
            'Read_XBMC_TMDB_Scraper_Config()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxTMDBPreferredCertCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbxTMDBPreferredCertCountry.SelectedIndexChanged
        If prefsload Then Exit Sub
        Try     'tmdbcertcountry
            Preferences.XbmcTmdbScraperCertCountry = cmbxTMDBPreferredCertCountry.Text
            Save_XBMC_TMDB_Scraper_Config("tmdbcertcountry", Preferences.XbmcTmdbScraperCertCountry)
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbXbmcTmdbRename_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbRename.CheckedChanged
        Try
            If cbXbmcTmdbRename.CheckState = CheckState.Checked Then
                If Preferences.MovieRenameEnable Then
                    Preferences.XbmcTmdbRenameMovie = True
                Else
                    MsgBox("Please also enable 'Rename Movie'")
                    cbXbmcTmdbRename.CheckState = CheckState.Unchecked 
                End If
            Else
                Preferences.XbmcTmdbRenameMovie = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch
        End Try
    End Sub

    Private Sub cbMovNewFolderInRootFolder_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovNewFolderInRootFolder.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.MovNewFolderInRootFolder = cbMovNewFolderInRootFolder.checked
            
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbXbmcTmdbActorDL_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXbmcTmdbActorDL.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.XbmcTmdbActorDL = cbXbmcTmdbActorDL.checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub
'End of "Choose Default Scraper"

'IMDB Scraper Limits
    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        Try
            If IsNumeric(ComboBox7.SelectedItem) Then
                Preferences.maxactors = Convert.ToInt32(ComboBox7.SelectedItem)
            ElseIf ComboBox7.SelectedItem.ToString.ToLower = "none" Then
                Preferences.maxactors = 0
            Else
                Preferences.maxactors = 9999
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        Try
            If IsNumeric(ComboBox6.SelectedItem) Then
                Preferences.maxmoviegenre = Convert.ToInt32(ComboBox6.SelectedItem)
            ElseIf ComboBox6.SelectedItem.ToString.ToLower = "none" Then
                Preferences.maxmoviegenre = 0
            Else
                Preferences.maxmoviegenre = 99
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxMovieScraper_MaxStudios_SelectedIndexChanged( sender As Object,  e As EventArgs) Handles cmbxMovieScraper_MaxStudios.SelectedIndexChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MovieScraper_MaxStudios = Convert.ToInt32(cmbxMovieScraper_MaxStudios.SelectedItem)  'nudMovieScraper_MaxStudios.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

'Other Options
    Private Sub cbGetMovieSetFromTMDb_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbGetMovieSetFromTMDb.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.GetMovieSetFromTMDb = cbGetMovieSetFromTMDb.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub chkbOriginal_Title_CheckedChanged( sender As Object,  e As EventArgs) Handles chkbOriginal_Title.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.Original_Title = chkbOriginal_Title.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

'Preferred Language
    Private Sub comboBoxTMDbSelectedLanguage_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles comboBoxTMDbSelectedLanguage.SelectedValueChanged
        Preferences.TMDbSelectedLanguageName = comboBoxTMDbSelectedLanguage.Text
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbUseCustomLanguage_Click(sender As System.Object, e As System.EventArgs) Handles cbUseCustomLanguage.Click
        Preferences.TMDbUseCustomLanguage = cbUseCustomLanguage.Checked
        SetLanguageControlsState()
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub tbCustomLanguageValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbCustomLanguageValue.TextChanged
        Preferences.TMDbCustomLanguageValue = tbCustomLanguageValue.Text
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub llLanguagesFile_Click(sender As System.Object, e As System.EventArgs) Handles llLanguagesFile.Click
        System.Diagnostics.Process.Start(TMDb.LanguagesFile)
    End Sub

'Individual Movie Folder Options
    Private Sub cbMovieUseFolderNames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieUseFolderNames.CheckedChanged
        Try
            If cbMovieUseFolderNames.CheckState = CheckState.Checked Then
                Preferences.usefoldernames = True
                cbMovieAllInFolders.Checked = False
                cbMovCreateFolderjpg.Enabled = True
                cbMovCreateFanartjpg.Enabled = True
                cbDlXtraFanart.Enabled = True
                If Preferences.basicsavemode Then
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Enabled = False
                Else
                    cbMovieFanartInFolders.Enabled = True
                    cbMoviePosterInFolder.Enabled = True
                End If
            Else
                Preferences.usefoldernames = False
                If Not Preferences.allfolders AndAlso Not Preferences.basicsavemode Then
                    cbMovCreateFolderjpg.Checked = False
                    cbMovCreateFolderjpg.Enabled = False
                    cbMovCreateFanartjpg.Enabled = False
                    cbMovCreateFanartjpg.Checked = False
                    cbMovieFanartInFolders.Checked = False
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Checked = False
                    cbMoviePosterInFolder.Enabled = False
                    cbDlXtraFanart.Checked = False
                    cbDlXtraFanart.Enabled = False
                    'Preferences.createfolderjpg = False
                ElseIf Not Preferences.allfolders AndAlso Preferences.basicsavemode Then
                    msgbox("Basic Save option is enabled" & vbCrLf & "Use Folder Name or All Movies in Folders" & vbCrLf & "must be selected!",MsgBoxStyle.Exclamation)
                    cbMovieUseFolderNames.Checked = CheckState.Checked
                End If
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieAllInFolders_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieAllInFolders.CheckedChanged 
        Try
            'Preferences.allfolders = cbMovieAllInFolders.Checked
            If cbMovieAllInFolders.CheckState = CheckState.Checked Then 
                Preferences.allfolders = True
                cbMovieUseFolderNames.Checked = False
                cbMovCreateFolderjpg.Enabled = True
                cbMovCreateFanartjpg.Enabled = True
                cbDlXtraFanart.Enabled = True
                If Preferences.basicsavemode Then
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Enabled = False
                Else
                    cbMovieFanartInFolders.Enabled = True
                    cbMoviePosterInFolder.Enabled = True
                End If               
            Else
                Preferences.allfolders = False
                If Not Preferences.usefoldernames AndAlso Not Preferences.basicsavemode Then
                    
                    cbMovCreateFolderjpg.Enabled = False
                    cbMovCreateFolderjpg.Checked = False
                    cbMovCreateFanartjpg.Enabled = False
                    cbMovCreateFanartjpg.Checked = False
                    cbMovieFanartInFolders.Checked = False
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Checked = False
                    cbMoviePosterInFolder.Enabled = False
                    cbDlXtraFanart.Checked = False
                    cbDlXtraFanart.Enabled = False
                ElseIf Not Preferences.usefoldernames AndAlso Preferences.basicsavemode Then
                    msgbox("Basic Save option is enabled" & vbCrLf & "Use Folder Name or All Movies in Folders" & vbCrLf & "must be selected!",MsgBoxStyle.Exclamation)
                    cbMovieAllInFolders.Checked = CheckState.Checked
                End If
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Basic Movie
    Private Sub cbMovieBasicSave_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieBasicSave.CheckedChanged 
        Try
            If cbMovieBasicSave.CheckState = CheckState.Checked Then
                If Preferences.usefoldernames or Preferences.allfolders Then
                    Preferences.basicsavemode = True
                    cbMovieFanartInFolders.Checked =CheckState.Unchecked
                    cbMovieFanartInFolders.Enabled = False
                    cbMoviePosterInFolder.Checked = CheckState.Unchecked
                    cbMoviePosterInFolder.Enabled = False
                Else
                    If (Not Preferences.usefoldernames AndAlso Not Preferences.allfolders) Then
                    MsgBox("Either Use Foldername or Movies In Folders" & vbCrLf & "must be selected")
                    End If
                    Preferences.basicsavemode = False
                    cbMovieFanartInFolders.Enabled = True 
                    cbMoviePosterInFolder.Enabled = True
                    cbMovieBasicSave.Checked = False
                End If
            Else
                Preferences.basicsavemode = False
                cbMovieFanartInFolders.Enabled = True 
                cbMoviePosterInFolder.Enabled = True 
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Keywords As Tags
    Private Sub cb_keywordasTag_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cb_keywordasTag.CheckedChanged
        Try
            If cb_keywordasTag.CheckState = CheckState.Checked Then
                Preferences.keywordasTag = True
                If Preferences.keywordlimit = 0 Then
                    MsgBox(" Please select a limit above Zero keywords" & vbCrLf & "else no keywords will be stored as Tags")
                End If
            Else
                Preferences.keywordasTag = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cb_keywordlimit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_keywordlimit.SelectedIndexChanged
        Try
            If IsNumeric(cb_keywordlimit.SelectedItem) Then
                Preferences.keywordlimit = Convert.ToInt32(cb_keywordlimit.SelectedItem)
            ElseIf cb_keywordlimit.SelectedItem.ToString.ToLower = "none" Then
                Preferences.keywordlimit = 0
            Else
                Preferences.keywordlimit = 999
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

#End Region  'Movie Preferences -> Scraper Tab

#Region "Movie Preferences -> Artwork Tab"
    
'Scraping Options
    Private Sub cbMoviePosterScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoviePosterScrape.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.scrapemovieposters = cbMoviePosterScrape.checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbMovFanartScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovFanartScrape.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.savefanart = cbMovFanartScrape.Checked

        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbMovFanartTvScrape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovFanartTvScrape.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.MovFanartTvscrape = cbMovFanartTvScrape.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub btnMovFanartTvSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovFanartTvSelect.Click
        Dim frm As New frmFanartTvArtSelect
        frm.Init()
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK AndAlso frm.IsChanged Then
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        End If
        frm.Dispose()
    End Sub

    Private Sub cbDlXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbDlXtraFanart.CheckedChanged
        If prefsload Then Exit Sub
        If cbDlXtraFanart.Checked Then
            If Not Preferences.allfolders AndAlso Not Preferences.usefoldernames Then
                MsgBox("Please select ""Use Foldername"" or ""Movies in Separate Folders""")
                cbDlXtraFanart.Checked = False
            Else
                If Not Preferences.movxtrafanart AndAlso Not Preferences.movxtrathumb Then
                    MsgBox("Please select ""Allow save ExtraThumbs"" And/Or ""Allow save ExtraFanart""")
                    cbDlXtraFanart.Checked = False
                Else 
                    Preferences.dlxtrafanart = True
                End If
            End If
        Else
            Preferences.dlxtrafanart = False
        End If
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

'Movie Scraper Poster Priority
    Private Sub Button73_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button73.Click
        Try
            Try
                Dim mSelectedIndex, mOtherIndex As Integer
                If Me.lbPosterSourcePriorities.SelectedIndex <> 0 Then
                    mSelectedIndex = Me.lbPosterSourcePriorities.SelectedIndex
                    mOtherIndex = mSelectedIndex - 1
                    lbPosterSourcePriorities.Items.Insert(mSelectedIndex + 1, lbPosterSourcePriorities.Items(mOtherIndex))
                    lbPosterSourcePriorities.Items.RemoveAt(mOtherIndex)
                End If
                Dim mothpr As Integer = lbPosterSourcePriorities.Items.Count -1
                Preferences.moviethumbpriority.Clear()
                For f = 0 To mothpr
                    Preferences.moviethumbpriority.Add(lbPosterSourcePriorities.Items(f).ToString)
                Next
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button61_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button61.Click
        Try
            Try
                Dim mSelectedIndex, mOtherIndex As Integer
                If Me.lbPosterSourcePriorities.SelectedIndex <> Me.ListBox3.Items.Count - 1 Then
                    mSelectedIndex = Me.lbPosterSourcePriorities.SelectedIndex
                    mOtherIndex = mSelectedIndex + 1
                    lbPosterSourcePriorities.Items.Insert(mSelectedIndex, lbPosterSourcePriorities.Items(mOtherIndex))
                    lbPosterSourcePriorities.Items.RemoveAt(mOtherIndex + 1)
                End If
                Dim mothpr As Integer = lbPosterSourcePriorities.Items.Count -1
                Preferences.moviethumbpriority.Clear()
                For f = 0 To mothpr
                    Preferences.moviethumbpriority.Add(lbPosterSourcePriorities.Items(f).ToString)
                Next
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_MovPosterPriorityReset_Click( sender As System.Object,  e As System.EventArgs) Handles btn_MovPosterPriorityReset.Click
        Preferences.resetmovthumblist()
        If lbPosterSourcePriorities.Items.Count <> Preferences.moviethumbpriority.Count Then
            lbPosterSourcePriorities.Items.Clear()
            For f = 0 To Preferences.moviethumbpriority.Count-1
                lbPosterSourcePriorities.Items.Add(Preferences.moviethumbpriority(f))
            Next
        End If
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub btn_MovPosterPriorityRemove_Click( sender As System.Object,  e As System.EventArgs) Handles btn_MovPosterPriorityRemove.Click
        Try
            Dim selected As Integer = Me.lbPosterSourcePriorities.SelectedIndex
            If selected = -1 Then Exit Sub
            Me.lbPosterSourcePriorities.Items.RemoveAt(selected)
            Dim mothpr As Integer = lbPosterSourcePriorities.Items.Count -1
            Preferences.moviethumbpriority.Clear()
            For f = 0 To mothpr
                Preferences.moviethumbpriority.Add(lbPosterSourcePriorities.Items(f).ToString)
            Next
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

'Movie's in folders
    Private Sub cbMovXtraThumbs_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovXtraThumbs.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If cbMovXtraThumbs.Checked Then
                Preferences.movxtrathumb = True
            Else
                If Not cbMovXtraFanart.Checked Then
                    cbDlXtraFanart.Checked = False
                    MsgBox("Disabled ""Download Extra Fanart/Thumbs"" as either " & vbCrLf & "Extra Fanart or Extra Thumbs" & vbCrLf & "         must be checked")
                End If
                Preferences.movxtrathumb = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovXtraFanart.CheckedChanged
        If prefsload Then Exit Sub
        Try
            If cbMovXtraFanart.Checked Then
                Preferences.movxtrafanart = True
            Else
                If Not cbMovXtraThumbs.Checked Then
                    cbDlXtraFanart.Checked = False
                    MsgBox("Disabled ""Download Extra Fanart/Thumbs"" as either " & vbCrLf & "Extra Fanart or Extra Thumbs" & vbCrLf & "         must be checked")
                End If
                Preferences.movxtrafanart = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cmbxMovXtraFanartQty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxMovXtraFanartQty.SelectedIndexChanged
        If prefsload Then Exit Sub
        Dim newvalue As String = cmbxMovXtraFanartQty.SelectedItem
        Preferences.movxtrafanartqty = newvalue.toint
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbMoviePosterInFolder_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMoviePosterInFolder.CheckedChanged 
        Try
            If cbMoviePosterInFolder.CheckState = CheckState.Checked Then
                If Preferences.usefoldernames or Preferences.allfolders Then
                    Preferences.posterjpg = True
                Else 
                    Preferences.posterjpg = False
                    cbMoviePosterInFolder.Checked = False
                    MsgBox("Either Use Foldername or All Movies in Folders not selected!")
                End If
            Else
                Preferences.posterjpg = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieFanartInFolders_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovieFanartInFolders.CheckedChanged
        Try
            If cbMovieFanartInFolders.CheckState = CheckState.Checked Then
                If Preferences.usefoldernames or Preferences.allfolders Then
                    Preferences.fanartjpg = True
                Else 
                    Preferences.fanartjpg = False
                    cbMovieFanartInFolders.Checked = False
                    MsgBox("Either Use Foldername or All Movies in Folders not selected!")
                End If
            Else
                Preferences.fanartjpg = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovCreateFolderjpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovCreateFolderjpg.CheckedChanged 
        Try
            If cbMovCreateFolderjpg.CheckState = CheckState.Checked and (Preferences.usefoldernames or Preferences.allfolders) Then
                Preferences.createfolderjpg = True
            Else
                Preferences.createfolderjpg = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovCreateFanartjpg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovCreateFanartjpg.CheckedChanged 
        Try
            If cbMovCreateFanartjpg.CheckState = CheckState.Checked and (Preferences.usefoldernames or Preferences.allfolders) Then
                Preferences.createfanartjpg = True
            Else
                Preferences.createfanartjpg = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Image Resizing
    Private Sub comboActorResolutions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comboActorResolutions.SelectedIndexChanged
        Preferences.ActorResolutionSI = comboActorResolutions.SelectedIndex
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub comboPosterResolutions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comboPosterResolutions.SelectedIndexChanged
        Preferences.PosterResolutionSI = comboPosterResolutions.SelectedIndex
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub comboBackDropResolutions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comboBackDropResolutions.SelectedIndexChanged
        Preferences.BackDropResolutionSI = comboBackDropResolutions.SelectedIndex
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

'Movie Set Artwork
    Private Sub cbMovSetArtScrape_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovSetArtScrape.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.dlMovSetArtwork = cbMovSetArtScrape.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub rbMovSetCentralFolder_CheckedChanged( sender As Object,  e As EventArgs) Handles rbMovSetArtSetFolder.CheckedChanged
        If prefsload Then Exit Sub
        Preferences.MovSetArtSetFolder = rbMovSetArtSetFolder.checked
        btnMovSetCentralFolderSelect.Enabled = rbMovSetArtSetFolder.Checked 
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub btnMovSetCentralFolderSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovSetCentralFolderSelect.Click

    End Sub

#End Region 'Movie Preferences -> Scraper Tab

#Region "Movie Preferences -> General Tab"

'General Options Settings
    Private Sub cbMovieTrailerUrl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieTrailerUrl.CheckedChanged
        Try
            If cbMovieTrailerUrl.CheckState = CheckState.Checked Then
                Preferences.gettrailer = True
            Else
                If cbDlTrailerDuringScrape.CheckState = CheckState.Checked Then
                    cbMovieTrailerUrl.CheckState = CheckState.Checked 
                Else
                    Preferences.gettrailer = False
                End If
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch
        End Try
    End Sub

    Private Sub cbDlTrailerDuringScrape_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbDlTrailerDuringScrape.CheckedChanged
        Try
            If cbDlTrailerDuringScrape.CheckState = CheckState.Checked Then
                Preferences.DownloadTrailerDuringScrape = True
                cbMovieTrailerUrl.CheckState = CheckState.Checked 
            Else 
                Preferences.DownloadTrailerDuringScrape = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch
        End Try
    End Sub

    Private Sub cbPreferredTrailerResolution_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPreferredTrailerResolution.SelectedIndexChanged
        Preferences.moviePreferredTrailerResolution = cbPreferredTrailerResolution.Text.ToUpper()
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbMovTitleCase_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovTitleCase.CheckedChanged
        Preferences.MovTitleCase = cbMovTitleCase.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbExcludeMpaaRated_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbExcludeMpaaRated.CheckedChanged
        Preferences.ExcludeMpaaRated = cbExcludeMpaaRated.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbMovThousSeparator_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovThousSeparator.CheckedChanged
        Preferences.MovThousSeparator = cbMovThousSeparator.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbNoAltTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNoAltTitle.CheckedChanged
        Try
            If cbNoAltTitle.CheckState = CheckState.Checked Then
                Preferences.NoAltTitle = True
            Else
                Preferences.NoAltTitle = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbXtraFrodoUrls_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbXtraFrodoUrls.CheckedChanged
        
        Try
            Preferences.XtraFrodoUrls = Not cbXtraFrodoUrls.Checked 
            'If cbXtraFrodoUrls.CheckState = CheckState.Checked Then
            '    Preferences.XtraFrodoUrls = False
            'Else
            '    Preferences.XtraFrodoUrls = True
            'End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        Try
            If CheckBox16.CheckState = CheckState.Checked Then
                Preferences.disablelogfiles = False
            Else
                Preferences.disablelogfiles = True
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        Try
            displayRuntimeScraper = True
            If CheckBox19.CheckState = CheckState.Checked Then
                Preferences.enablehdtags = True
                PanelDisplayRuntime.Enabled = True
                If Preferences.movieRuntimeDisplay = "file" Then
                    rbRuntimeFile.Checked = True
                    displayRuntimeScraper = False
                Else
                    rbRuntimeScraper.Checked = True
                End If
            Else
                Preferences.enablehdtags = False
                PanelDisplayRuntime.Enabled = False
                rbRuntimeScraper.Checked = True
            End If
            Call mov_SwitchRuntime()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub rbRuntimeScraper_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbRuntimeScraper.CheckedChanged
        Try
            If rbRuntimeScraper.Checked = True Then
                Preferences.movieRuntimeDisplay = "scraper"
                displayRuntimeScraper = True
            Else
                Preferences.movieRuntimeDisplay = "file"
                displayRuntimeScraper = False
            End If

            cbMovieRuntimeFallbackToFile.Enabled = rbRuntimeScraper.Checked

            'Call mov_SwitchRuntime() 'Damn it - this call prevents MC starting, and I have no idea why! HueyHQ
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieRuntimeFallbackToFile_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieRuntimeFallbackToFile.CheckedChanged
        Preferences.movieRuntimeFallbackToFile = cbMovieRuntimeFallbackToFile.Checked
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub TextBox_OfflineDVDTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_OfflineDVDTitle.TextChanged
        Try
            Preferences.OfflineDVDTitle = TextBox_OfflineDVDTitle.Text
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tbDateFormat_TextChanged( sender As System.Object,  e As System.EventArgs) Handles tbDateFormat.TextChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.DateFormat = tbDateFormat.Text
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieList_ShowColPlot_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovieList_ShowColPlot.CheckedChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MovieList_ShowColPlot = cbMovieList_ShowColPlot.Checked
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieList_ShowColWatched_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovieList_ShowColWatched.CheckedChanged
         If MainFormLoadedStatus Then
            Try
                Preferences.MovieList_ShowColWatched = cbMovieList_ShowColWatched.Checked
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub CheckBox_ShowDateOnMovieList_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox_ShowDateOnMovieList.CheckedChanged
        Try
            If CheckBox_ShowDateOnMovieList.Checked = True Then
                Preferences.showsortdate = True
            Else
                Preferences.showsortdate = False
            End If
            Call Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMissingMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMissingMovie.CheckedChanged
        Try
            If cbMissingMovie.CheckState = CheckState.Checked Then
                Preferences.incmissingmovies = True 
            Else
                Preferences.incmissingmovies = False 
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbxCleanFilenameIgnorePart_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxCleanFilenameIgnorePart.CheckedChanged 
        Try
            If cbxCleanFilenameIgnorePart.Checked = True Then
                Preferences.movieignorepart = True
            Else
                Preferences.movieignorepart = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbxNameMode_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxNameMode.CheckedChanged
        If cbxNameMode.Checked Then
            Preferences.namemode = "1"
        Else
            Preferences.namemode = "0"
        End If
        lblNameMode.Text = createNameModeText()
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

'Rename Movie Settings
    Private Sub tb_MovieRenameEnable_TextChanged(sender As System.Object, e As System.EventArgs) Handles tb_MovieRenameEnable.TextChanged
        Try
            Preferences.MovieRenameTemplate = tb_MovieRenameEnable.Text
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tb_MovFolderRename_TextChanged(sender As System.Object, e As System.EventArgs) Handles tb_MovFolderRename.TextChanged
        Try
            Preferences.MovFolderRenameTemplate = tb_MovFolderRename.Text
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieRenameEnable_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovieRenameEnable.CheckedChanged
        Try
            If cbMovieRenameEnable.CheckState = CheckState.Checked Then
                Preferences.MovieRenameEnable = True
            Else
                Preferences.MovieRenameEnable = False
                'Preferences.XbmcTmdbRenameMovie = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovFolderRename_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovFolderRename.CheckedChanged
        Try
            If cbMovFolderRename.CheckState = CheckState.Checked Then
                Preferences.MovFolderRename = True
            Else
                Preferences.MovFolderRename = False
                'Preferences.XbmcTmdbRenameMovie = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbRenameUnderscore_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbRenameUnderscore.CheckedChanged
        Try
            If cbRenameUnderscore.CheckState = CheckState.Checked Then
                Preferences.MovRenameSpaceCharacter = True
            Else
                Preferences.MovRenameSpaceCharacter = False
                'Preferences.XbmcTmdbRenameMovie = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub rbRenameUnderscore_CheckedChanged( sender As Object,  e As EventArgs) Handles rbRenameUnderscore.CheckedChanged
        If prefsload Then Exit Sub
        If rbRenameUnderscore.Checked = True Then
            Preferences.RenameSpaceCharacter = "_"
        Else
            Preferences.RenameSpaceCharacter = "."
        End If
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

    Private Sub cbMovSetIgnArticle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovSetIgnArticle.CheckedChanged
        Try
            If cbMovSetIgnArticle.CheckState = CheckState.Checked Then
                Preferences.MovSetIgnArticle = True
            Else
                Preferences.MovSetIgnArticle = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovTitleIgnArticle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovTitleIgnArticle.CheckedChanged
        Try
            If cbMovTitleIgnArticle.CheckState = CheckState.Checked Then
                Preferences.MovTitleIgnArticle = True
            Else
                Preferences.MovTitleIgnArticle = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovSortIgnArticle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbMovSortIgnArticle.CheckedChanged
        Try
            If cbMovSortIgnArticle.CheckState = CheckState.Checked Then
                Preferences.MovSortIgnArticle = True
            Else
                Preferences.MovSortIgnArticle = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ManualRenameChkbox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ManualRenameChkbox.CheckedChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MovieManualRename = ManualRenameChkbox.Checked
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub ScrapeFullCertCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ScrapeFullCertCheckBox.CheckedChanged
        Try
            If ScrapeFullCertCheckBox.Checked Then
                Preferences.scrapefullcert = True
            Else
                Preferences.scrapefullcert = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
    
    Private Sub Button75_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button75.Click
        Try
            Try
                Dim mSelectedIndex, mOtherIndex As Integer
                If Me.ListBox11.SelectedIndex <> 0 Then
                    mSelectedIndex = Me.ListBox11.SelectedIndex
                    mOtherIndex = mSelectedIndex - 1
                    ListBox11.Items.Insert(mSelectedIndex + 1, ListBox11.Items(mOtherIndex))
                    ListBox11.Items.RemoveAt(mOtherIndex)
                End If
                For f = 0 To 33
                    Preferences.certificatepriority(f) = ListBox11.Items(f)
                Next
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button74_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button74.Click
        Try
            Try
                Dim mSelectedIndex, mOtherIndex As Integer
                If Me.ListBox11.SelectedIndex <> Me.ListBox11.Items.Count - 1 Then
                    mSelectedIndex = Me.ListBox11.SelectedIndex
                    mOtherIndex = mSelectedIndex + 1
                    ListBox11.Items.Insert(mSelectedIndex, ListBox11.Items(mOtherIndex))
                    ListBox11.Items.RemoveAt(mOtherIndex + 1)
                End If
                For f = 0 To 33
                    Preferences.certificatepriority(f) = ListBox11.Items(f)
                Next
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

'Movie Filters settings
    Private Sub nudActorsFilterMinFilms_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudActorsFilterMinFilms.ValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.ActorsFilterMinFilms = nudActorsFilterMinFilms.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudMaxActorsInFilter_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudMaxActorsInFilter.ValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MaxActorsInFilter = nudMaxActorsInFilter.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieFilters_Actors_Order_SelectedValueChanged( sender As Object,  e As EventArgs) Handles cbMovieFilters_Actors_Order.SelectedValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MovieFilters_Actors_Order = cbMovieFilters_Actors_Order.SelectedIndex
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudDirectorsFilterMinFilms_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudDirectorsFilterMinFilms.ValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.DirectorsFilterMinFilms = nudDirectorsFilterMinFilms.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudMaxDirectorsInFilter_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudMaxDirectorsInFilter.ValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MaxDirectorsInFilter = nudMaxDirectorsInFilter.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieFilters_Directors_Order_SelectedValueChanged( sender As Object,  e As EventArgs) Handles cbMovieFilters_Directors_Order.SelectedValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MovieFilters_Directors_Order = cbMovieFilters_Directors_Order.SelectedIndex
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudSetsFilterMinFilms_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudSetsFilterMinFilms.ValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.SetsFilterMinFilms = nudSetsFilterMinFilms.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub nudMaxSetsInFilter_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles nudMaxSetsInFilter.ValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MaxSetsInFilter = nudMaxSetsInFilter.Value
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbMovieFilters_Sets_Order_SelectedValueChanged( sender As Object,  e As EventArgs) Handles cbMovieFilters_Sets_Order.SelectedValueChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.MovieFilters_Sets_Order = cbMovieFilters_Sets_Order.SelectedIndex
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

    Private Sub cbDisableNotMatchingRenamePattern_CheckedChanged( sender As Object,  e As EventArgs) Handles cbDisableNotMatchingRenamePattern.CheckedChanged
        If MainFormLoadedStatus Then
            Try
                Preferences.DisableNotMatchingRenamePattern = cbDisableNotMatchingRenamePattern.Checked
                movieprefschanged = True
                btnMoviePrefSaveChanges.Enabled = True
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
            End Try
        End If
    End Sub

#End Region 'Movie Preferences -> General Tab

#Region "Movie Preferences -> Advanced Tab"
    'Download Actor Thumbs
    Private Sub saveactorchkbx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveactorchkbx.CheckedChanged
        Try
            If saveactorchkbx.CheckState = CheckState.Checked Then
                Preferences.actorsave = True
                localactorpath.Text = Preferences.actorsavepath
                xbmcactorpath.Text = Preferences.actornetworkpath
                localactorpath.Enabled = True
                xbmcactorpath.Enabled = True
                cb_LocalActorSaveAlpha.Enabled = True
                Button77.Enabled = True
            Else
                Preferences.actorsave = False
                localactorpath.Text = ""
                xbmcactorpath.Text = ""
                localactorpath.Enabled = False
                xbmcactorpath.Enabled = False
                cb_LocalActorSaveAlpha.Enabled = False
                Button77.Enabled = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cb_LocalActorSaveAlpha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_LocalActorSaveAlpha.CheckedChanged
        Try
            If cb_LocalActorSaveAlpha.CheckState = CheckState.Checked Then
                Preferences.actorsavealpha = True
            Else
                Preferences.actorsavealpha = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button77_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button77.Click
        Try
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select Folder to Save Actor Thumbnails)"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                localactorpath.Text = thefoldernames
                Preferences.lastpath = thefoldernames
                Preferences.actorsavepath = thefoldernames
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub localactorpath_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles localactorpath.TextChanged
        Try
            Preferences.actorsavepath = localactorpath.Text
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub xbmcactorpath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xbmcactorpath.TextChanged
        Try
            Preferences.actornetworkpath = xbmcactorpath.Text
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    'nfoPoster Options
    Private Sub IMPA_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPA_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub mpdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mpdb_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tmdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmdb_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub imdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imdb_chk.CheckedChanged
        Try
            Call mov_ThumbNailUrlsSet()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovRootFolderCheck_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovRootFolderCheck.CheckedChanged
        Try
            If cbMovRootFolderCheck.CheckState = CheckState.Checked Then
                Preferences.movrootfoldercheck = True
            Else
                Preferences.movrootfoldercheck = False
            End If
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnCleanFilenameAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnCleanFilenameAdd.Click
        lbCleanFilename.Items.Add(txtCleanFilenameAdd.Text)
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
        cleanfilenameprefchanged = True
    End Sub

    Private Sub btnCleanFilenameRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnCleanFilenameRemove.Click
        lbCleanFilename.Items.RemoveAt(lbCleanFilename.SelectedIndex)
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
        cleanfilenameprefchanged = True
    End Sub

    Private Sub btnVideoSourceAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnVideoSourceAdd.Click
        If txtVideoSourceAdd.Text <> "" Then        
            lbVideoSource.Items.Add(txtVideoSourceAdd.Text)
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
            videosourceprefchanged = True
        End If
    End Sub

    Private Sub btnVideoSourceRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnVideoSourceRemove.Click
        Dim strSelected = lbVideoSource.SelectedItem
        Dim idxSelected = lbVideoSource.SelectedIndex

        Try
            If cbMovieDisplay_Source.Text = strSelected Then cbMovieDisplay_Source.SelectedIndex = 0
            lbVideoSource.Items.RemoveAt(idxSelected)
            mov_VideoSourcePopulate()
            ep_VideoSourcePopulate()
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
            videosourceprefchanged = True
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try

    End Sub

#End Region 'Movie Preferences -> Advanced Tab

#Region "Movie Preferences -> Advanced2 Tab""
    Private Sub btn_MovSepAdd_Click(sender As System.Object, e As System.EventArgs) Handles btn_MovSepAdd.Click
        If tb_MovSeptb.Text <> "" Then
            lb_MovSepLst.Items.Add(tb_MovSeptb.Text)
            Preferences.MovSepLst.Add(tb_MovSeptb.Text)
            tb_MovSeptb.Text = ""
            movieprefschanged = True
            btnMoviePrefSaveChanges.Enabled = True
            videosourceprefchanged = True
        End If
    End Sub

    Private Sub btn_MovSepRem_Click(sender As System.Object, e As System.EventArgs) Handles btn_MovSepRem.Click
        lb_MovSepLst.Items.RemoveAt(lb_MovSepLst.SelectedIndex)
        Preferences.MovSepLst.Clear()
        For Each t In lb_MovSepLst.Items
            Preferences.MovSepLst.Add(t)
        Next
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
        'videosourceprefchanged = True
    End Sub

    Private Sub btn_MovSepReset_Click(sender As System.Object, e As System.EventArgs) Handles btn_MovSepReset.Click
        Preferences.ResetMovSepLst()
        lb_MovSepLst.Items.Clear()
        For Each it In MovSepLst
            lb_MovSepLst.Items.Add(it)
        Next
        movieprefschanged = True
        btnMoviePrefSaveChanges.Enabled = True
    End Sub

#End Region 'Movie Preferences -> Advanced2 Tab

#End Region   'Movie Preferences Tab


    Private Sub tv_FoldersSetup()
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
        For Each folder In tvRootFolders
            ListBox5.Items.Add(folder)
        Next
        For Each folder In tvFolders
            ListBox6.Items.Add(folder)
        Next
    End Sub


#Region "Movie Browser Tab"

    Private Sub btnMovSearchNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovSearchNew.Click
        SearchForNew()
    End Sub

    Private Sub btnMovRefreshAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovRefreshAll.Click
        mov_RebuildMovieCaches()
    End Sub

    Private Sub ToolStripMenuItemRebuildMovieCaches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemRebuildMovieCaches.Click
        mov_RebuildMovieCaches()
    End Sub

    Private Sub cbBtnLink_Click(sender As Object, e As EventArgs) Handles cbBtnLink.Click
        SetcbBtnLink()
        LabelCountFilter.Focus()
    End Sub

    Private Sub rbFileName_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFileName.CheckedChanged
        HandleMovieList_DisplayChange("FileName")
        If Not cbSort.Text = "A - Z" Then
            cbSort.Text = "A - Z"
        Else
            cbsort_SelectedIndexChanged(cbsort, EventArgs.Empty)
        End If
    End Sub

    Private Sub rbTitleAndYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTitleAndYear.CheckedChanged
        HandleMovieList_DisplayChange("TitleAndYear")
        cbsort_SelectedIndexChanged(cbsort, EventArgs.Empty)
    End Sub

    Private Sub rbFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFolder.CheckedChanged
        HandleMovieList_DisplayChange("Folder")
        If Not cbSort.Text = "A - Z" Then
            cbSort.Text = "A - Z"
        Else
            cbsort_SelectedIndexChanged(cbsort, EventArgs.Empty)
        End If
    End Sub

    Private Sub cbSort_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbSort.SelectedIndexChanged
        Mc.clsGridViewMovie.GridFieldToDisplay2 = cbSort.Text
        Call Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        DisplayMovie()
        If Not MainFormLoadedStatus Then Exit Sub
        Preferences.moviesortorder = cbSort.SelectedIndex
    End Sub

    Private Sub btnreverse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreverse.CheckedChanged
        If btnreverse.Checked Then
            Mc.clsGridViewMovie.GridSort = "Desc"
        Else
            Mc.clsGridViewMovie.GridSort = "Asc"
        End If
        Preferences.movieinvertorder = btnreverse.Checked

        Call Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        DisplayMovie()
    End Sub

    Private Sub btnResetFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetFilters.Click
        resetallfilters()
    End Sub

    Private Sub txt_titlesearch_KeyDown(ByVal sender As Object, ByVal e As Object) Handles txt_titlesearch.KeyDown, txt_titlesearch.ModifiedChanged
        Try
            If filterOverride = False Then

                _yield = True
                Application.DoEvents()
                _yield = False

                If txt_titlesearch.Text.Length > 0 Then
                    txt_titlesearch.BackColor = Color.DarkOrange
                Else
                    txt_titlesearch.BackColor = Color.White
                End If
                txt_titlesearch.Refresh()
                Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)

                If _yield Then Return


                DisplayMovie(True)
                Cursor.Current = Cursors.Default
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovSave.Click
        Try
            Call mov_SaveQuick()

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovRescrape.Click
        mov_Rescrape()
    End Sub

    Private Sub btnMovieDisplay_ActorFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieDisplay_ActorFilter.Click
        Try
            ProgState = ProgramState.ResettingFilters
            oMovies.ActorsFilter_AddIfMissing(cbMovieDisplay_Actor.Text)

            ShowMovieFilter(cbFilterActor)

            cbFilterActor.UpdateItems(oMovies.ActorsFilter)
            cbFilterActor.SelectItem(cbMovieDisplay_Actor.Text)
            ProgState = ProgramState.Other

            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
            '           LabelCountFilter.Text = "Displaying " & DataGridViewMovies.Rows.Count & " " & cbMovieDisplay_Actor.Text & " movie" & If( DataGridViewMovies.Rows.Count>1, "s", "")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovieDisplay_DirectorFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieDisplay_DirectorFilter.Click
        Try
            ProgState = ProgramState.ResettingFilters
            oMovies.DirectorsFilter_AddIfMissing(directortxt.Text)

            ShowMovieFilter(cbFilterDirector)

            cbFilterDirector.UpdateItems(oMovies.DirectorsFilter)
            cbFilterDirector.SelectItem(directortxt.Text)
            ProgState = ProgramState.Other

            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub titletxt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles titletxt.Enter
        Try
            Try
                processnow = False
                If titletxt.Text.IndexOf(workingMovieDetails.fullmoviebody.year) <> -1 Then
                    Dim tempstring2 As String = " (" & workingMovieDetails.fullmoviebody.year & ")"
                    Dim tempstring As String = titletxt.Text.Replace(tempstring2, "")
                    tempstring = tempstring.TrimEnd(" ")
                    currenttitle = tempstring
                    titletxt.Items.Clear()
                    'titletxt.Items.Add(tempstring)
                    For Each item In workingMovieDetails.alternativetitles
                        If item <> tempstring Then
                            titletxt.Items.Add(item)
                        End If
                    Next
                    titletxt.SelectedIndex = -1
                    titletxt.Text = tempstring

                End If
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub titletxt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles titletxt.Leave
        Try
            Try
                If titletxt.Text.IndexOf(workingMovieDetails.fullmoviebody.year) = -1 Then
                    Dim tempstring As String = titletxt.Text
                    titletxt.Items.Clear()
                    For Each item In workingMovieDetails.alternativetitles
                        If item <> currenttitle Then
                            titletxt.Items.Add(item)
                        End If
                    Next
                    titletxt.Text = tempstring & " (" & workingMovieDetails.fullmoviebody.year & ")"
                End If
                processnow = True
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub titletxt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles titletxt.SelectedIndexChanged
        Try
            Try
                If titletxt.Text.IndexOf(workingMovieDetails.fullmoviebody.year) = -1 Then
                    Dim tempstring As String = titletxt.Text
                    titletxt.Items.Clear()
                    titletxt.Items.Add(tempstring & " (" & workingMovieDetails.fullmoviebody.year & ")")
                    For Each item In workingMovieDetails.alternativetitles
                        If item <> tempstring Then
                            titletxt.Items.Add(item)
                        End If
                    Next
                    titletxt.SelectedIndex = 0
                End If
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PbMovieFanArt_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PbMovieFanArt.MouseUp, PbMovieFanArt.Click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                RescrapePToolStripMenuItem.Visible = False
                RescrapePosterFromTMDBToolStripMenuItem.Visible = False
                RescraToolStripMenuItem.Visible = False
                PeToolStripMenuItem.Visible = False
                DownloadPosterToolStripMenuItem.Visible = False
                DownloadPosterFromTMDBToolStripMenuItem.Visible = False
                DownloadPosterFromMPDBToolStripMenuItem.Visible = False
                DownloadPosterFromIMDBToolStripMenuItem.Visible = False
                RescrapeFanartToolStripMenuItem.Visible = False
                DownloadFanartToolStripMenuItem.Visible = False
                Try
                    If IO.File.Exists(Preferences.GetFanartPath(workingMovieDetails.fileinfo.fullpathandfilename)) Or (workingMovieDetails.fileinfo.videotspath <> "" And IO.File.Exists(workingMovieDetails.fileinfo.videotspath + "fanart.jpg")) Then
                        RescrapeFanartToolStripMenuItem.Visible = True
                    Else
                        DownloadFanartToolStripMenuItem.Visible = True
                    End If
                Catch
                    RescrapePToolStripMenuItem.Visible = False
                    RescrapeFanartToolStripMenuItem.Visible = False
                    DownloadFanartToolStripMenuItem.Visible = False
                    DownloadPosterToolStripMenuItem.Visible = False

                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub PbMovieFanArt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PbMovieFanArt.DoubleClick

        Try
            Try
                If workingMovieDetails.fileinfo.fanartpath <> Nothing Then
                    If IO.File.Exists(workingMovieDetails.fileinfo.fanartpath) Then
                        Me.ControlBox = False
                        MenuStrip1.Enabled = False
                        'Using newimage As New Bitmap(workingMovieDetails.fileinfo.fanartpath)
                            util_ZoomImage(workingMovieDetails.fileinfo.fanartpath)
                        'End Using
                    End If
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PbMoviePoster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PbMoviePoster.DoubleClick
        Try
            Try
                If workingMovieDetails.fileinfo.posterpath <> Nothing Then
                    If IO.File.Exists(workingMovieDetails.fileinfo.posterpath) Then
                        Me.ControlBox = False
                        MenuStrip1.Enabled = False
                        'Using newimage As New Bitmap(workingMovieDetails.fileinfo.posterpath)
                            'util_ZoomImage(newimage)
                            util_ZoomImage(workingMovieDetails.fileinfo.posterpath)
                        'End Using
                    End If
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub PbMoviePoster_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PbMoviePoster.MouseUp
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                RescrapePToolStripMenuItem.Visible = False
                RescrapePosterFromTMDBToolStripMenuItem.Visible = False
                RescraToolStripMenuItem.Visible = False
                PeToolStripMenuItem.Visible = False
                DownloadPosterToolStripMenuItem.Visible = False
                DownloadPosterFromTMDBToolStripMenuItem.Visible = False
                DownloadPosterFromMPDBToolStripMenuItem.Visible = False
                DownloadPosterFromIMDBToolStripMenuItem.Visible = False
                RescrapeFanartToolStripMenuItem.Visible = False
                DownloadFanartToolStripMenuItem.Visible = False
                Try
                    If IO.File.Exists(Preferences.GetPosterPath(workingMovieDetails.fileinfo.fullpathandfilename)) Then
                        RescrapePToolStripMenuItem.Visible = True
                        RescrapePosterFromTMDBToolStripMenuItem.Visible = True
                        RescraToolStripMenuItem.Visible = True
                        PeToolStripMenuItem.Visible = True
                    Else
                        DownloadPosterToolStripMenuItem.Visible = True
                        DownloadPosterFromTMDBToolStripMenuItem.Visible = True
                        DownloadPosterFromMPDBToolStripMenuItem.Visible = True
                        DownloadPosterFromIMDBToolStripMenuItem.Visible = True
                    End If
                Catch
                    RescrapePToolStripMenuItem.Visible = False
                    RescrapePosterFromTMDBToolStripMenuItem.Visible = False
                    RescraToolStripMenuItem.Visible = False
                    PeToolStripMenuItem.Visible = False
                    DownloadPosterToolStripMenuItem.Visible = False
                    DownloadPosterFromTMDBToolStripMenuItem.Visible = False
                    DownloadPosterFromMPDBToolStripMenuItem.Visible = False
                    DownloadPosterFromIMDBToolStripMenuItem.Visible = False
                    RescrapeFanartToolStripMenuItem.Visible = False
                    DownloadFanartToolStripMenuItem.Visible = False
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbMovieDisplay_Actor_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMovieDisplay_Actor.MouseEnter
        Try
            cbMovieDisplay_Actor.Focus()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub cbMovieDisplay_Actor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMovieDisplay_Actor.SelectedIndexChanged
        Try
            For Each actor In workingMovieDetails.listactors
                If actor.actorname = cbMovieDisplay_Actor.SelectedItem Then
                    If actor.actorrole <> "" Then
                        roletxt.Text = actor.actorrole
                    End If
                    Dim temppath = GetActorPath(workingMovieDetails.fileinfo.fullpathandfilename, actor.actorname, actor.actorid)
                    If Not String.IsNullOrEmpty(temppath) AndAlso IO.File.Exists(temppath) Then
                        util_ImageLoad(PictureBoxActor, temppath, Utilities.DefaultActorPath)
                        Exit Sub
                    End If
                    If actor.actorthumb <> Nothing Then 'And Not Preferences.LocalActorImage Then
                        Dim actorthumbpath As String = Preferences.GetActorThumbPath(actor.actorthumb)
                        If actorthumbpath <> "none" Then
                            If Not Preferences.LocalActorImage AndAlso actorthumbpath.IndexOf("http") = 0 Then
                                If IO.File.Exists(actorthumbpath) Or actorthumbpath.ToLower.IndexOf("http") <> -1 Then
                                    util_ImageLoad(PictureBoxActor, actorthumbpath, Utilities.DefaultActorPath)
                                End If
                            Else
                                util_ImageLoad(PictureBoxActor, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                            End If
                        Else
                            util_ImageLoad(PictureBoxActor, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                        End If
                    Else
                        util_ImageLoad(PictureBoxActor, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                    End If
                    Exit For
                Else
                    util_ImageLoad(PictureBoxActor, Utilities.DefaultActorPath, Utilities.DefaultActorPath)
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovSelectPlot_Click(sender As Object, e As EventArgs) Handles btnMovSelectPlot.Click
        Dim newplot As String = GetlistofPlots()
        If Not IsNothing(newplot) Then
            plottxt.Text = newplot
            Call mov_SaveQuick()
        End If
    End Sub

    Private Sub lbl_movRuntime_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbl_movRuntime.MouseDown 
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If runtimetxt.Enabled = True Then
                displayRuntimeScraper = False
            Else
                displayRuntimeScraper = True
            End If
            Call mov_SwitchRuntime()
        End If
    End Sub

    Private Sub btnMovieDisplay_SetFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieDisplay_SetFilter.Click
        Try
            ProgState = ProgramState.ResettingFilters
            oMovies.SetsFilter_AddIfMissing(cbMovieDisplay_MovieSet.Text)

            ShowMovieFilter(cbFilterSet)

            cbFilterSet.UpdateItems(oMovies.SetsFilter)
            cbFilterSet.SelectItem(cbMovieDisplay_MovieSet.Text)

            ProgState = ProgramState.Other

            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ButtonTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTrailer.Click
        If Not IO.File.Exists(workingMovieDetails.fileinfo.trailerpath) Then
            _rescrapeList.Field = "Download_Trailer"
            _rescrapeList.FullPathAndFilenames.Clear()
            _rescrapeList.FullPathAndFilenames.Add(workingMovieDetails.fileinfo.fullpathandfilename)
            RunBackgroundMovieScrape("RescrapeSpecific")
        Else
            Dim tempstring = applicationPath & "\settings\temp.m3u"
            Dim file = IO.File.CreateText(tempstring)
            file.WriteLine(workingMovieDetails.fileinfo.trailerpath)
            file.Close()
            If Preferences.videomode = 1 Then Call util_VideoMode1(tempstring)
            If Preferences.videomode = 2 Then Call util_VideoMode2(tempstring)
            If Preferences.videomode = 3 Then
                Preferences.videomode = 2
                Call util_VideoMode2(tempstring)
            End If
            If Preferences.videomode >= 4 Then
                If Preferences.selectedvideoplayer <> Nothing Then
                    Call util_VideoMode4(tempstring)
                Else
                    Call util_VideoMode1(tempstring)
                End If
            End If
        End If
    End Sub

    Private Sub btnPlayMovie_Click(sender As System.Object, e As System.EventArgs) Handles btnPlayMovie.Click
        mov_Play("Movie")
    End Sub

    Private Sub btnMovWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovWatched.Click
        Try
            If DataGridViewMovies.SelectedRows.Count = 1 Then
                If btnMovWatched.Text = "&Watched" Then
                    btnMovWatched.Text = "Un&watched"
                    btnMovWatched.BackColor = Color.Red
                    btnMovWatched.Refresh()
                    workingMovieDetails.fullmoviebody.playcount = "0"
                Else
                    btnMovWatched.Text = "&Watched"
                    btnMovWatched.BackColor = Color.LawnGreen
                    btnMovWatched.Refresh()
                    workingMovieDetails.fullmoviebody.playcount = "1"
                End If
                Call mov_SaveQuick()
            ElseIf DataGridViewMovies.SelectedRows.Count > 1 Then
                Dim mess As New frmMessageBox("Saving Selected Movies", , "     Please Wait.     ")
                mess.Show()
                mess.Refresh()
                Dim watched As String = ""
                If btnMovWatched.Text = "&Watched" Then
                    btnMovWatched.Text = "Un&watched"
                    btnMovWatched.BackColor = Color.Red
                    btnMovWatched.Refresh()
                    watched = "0"
                ElseIf btnMovWatched.Text = "Un&watched" Then
                    btnMovWatched.Text = "&Watched"
                    btnMovWatched.BackColor = Color.LawnGreen
                    btnMovWatched.Refresh()
                    watched = "1"
                ElseIf btnMovWatched.Text = "" Then
                    btnMovWatched.Text = "&Watched"
                    btnMovWatched.BackColor = Color.LawnGreen
                    btnMovWatched.Refresh()
                    watched = "1"
                End If
                For Each sRow As DataGridViewRow In DataGridViewMovies.SelectedRows
                    Dim filepath As String = sRow.Cells("fullpathandfilename").Value.ToString
                    If (IO.File.Exists(filepath)) Then
                        Dim fmd As New FullMovieDetails
                        fmd = WorkingWithNfoFiles.mov_NfoLoadFull(filepath)
                        If IsNothing(fmd) Then Continue For
                        fmd.fullmoviebody.playcount = watched
                        'WorkingWithNfoFiles.mov_NfoSave(filepath, fmd, True)
                        Movie.SaveNFO(filepath, fmd)
                        For f = 0 To oMovies.MovieCache.Count - 1
                            If oMovies.MovieCache(f).fullpathandfilename = filepath Then
                                Dim newfullmovie As New ComboList
                                newfullmovie = oMovies.MovieCache(f)
                                newfullmovie.playcount = watched
                                oMovies.MovieCache.RemoveAt(f)
                                oMovies.MovieCache.Add(newfullmovie)
                                Exit For
                            End If
                        Next
                    End If
                Next
                mess.Close()
            End If
            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PictureBoxActor_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxActor.DoubleClick
        ZoomActorPictureBox(PictureBoxActor)
    End Sub

    Private Sub rcmenuOption(x, y)
        ' x is equal to the menu item title that was clicked
        ' Create case stament for that to call the correct xmlinteraction passing in y
        Select Case x
            Case "Premiered"
                mov_ScrapeSpecific("Premiered")
            Case "Cert"
                Call mov_ScrapeSpecific("mpaa")
            Case "Plot"
                Call mov_ScrapeSpecific("plot")
            Case "Outline"
                Call mov_ScrapeSpecific("outline")
            Case "Tagline"
                Call mov_ScrapeSpecific("tagline")
            Case "Genre"
                Call mov_ScrapeSpecific("genre")
            Case "Rating"
                Call mov_ScrapeSpecific("rating")
            Case "Runtime"
                Call mov_ScrapeSpecific("runtime")
            Case "Set"
                Call mov_ScrapeSpecific("tmdb_set_name")
            Case "Actors"
                Call mov_ScrapeSpecific("actors")
            Case "Stars"
                Call mov_ScrapeSpecific("stars")
            Case "Director"
                Call mov_ScrapeSpecific("director")
            Case "Credits"
                Call mov_ScrapeSpecific("credits")
            Case "Studio"
                Call mov_ScrapeSpecific("studio")
            Case "Country"
                Call mov_ScrapeSpecific("country")
            Case "Tag(s)"
                Call mov_ScrapeSpecific("TagsFromKeywords")
            Case "Top 250"
                Call mov_ScrapeSpecific("top250")
            Case "Votes"
                Call mov_ScrapeSpecific("votes")
        End Select
    End Sub

    Private Sub rcmenu_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles rcmenu.ItemClicked
        Dim contextMenu = DirectCast(sender, ContextMenuStrip)
        Dim label = DirectCast(contextMenu.SourceControl, Label)
        Dim var As String = label.Text
        Dim toolStripItem = e.ClickedItem
        Dim var2 As String = toolStripItem.Text
        rcmenuOption(var, var2)
    End Sub

    Private Sub rcmenuClicked(ByVal sender As System.Object, ByVal e As CancelEventArgs) Handles rcmenu.Opening
        ' Get the Label clicked from the SourceControl property of the clicked ContextMenuStrip.
        rcmenu.Items.Clear()
        Dim contextMenu = DirectCast(sender, ContextMenuStrip)
        Dim label = DirectCast(contextMenu.SourceControl, Label)
        Dim var2 As String = label.Text
            contextMenu.Items.Add("Rescrape " & var2)
        ' Get the clicked menu strip and update its Text to the Label's Text.
        'Dim toolStripItem = e.ClickedItem
        'Dim var As String = toolStripItem.Text
        'rcmenuOption(var, var2)
    End Sub

#End Region

#Region "Movie Fanart Tab"

    Private Sub PictureBox2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox2.DoubleClick
        Try
            If Not PictureBox2.Tag Is Nothing Then
                Me.ControlBox = False
                MenuStrip1.Enabled = False
                'ToolStrip1.Enabled = False
                'Dim newimage As New Bitmap(PictureBox2.Image)
                Call util_ZoomImage(PictureBox2.Tag.ToString)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncroptop_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncroptop.MouseDown
        Try
            If PictureBox2.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            cropString = "top"
            Timer2.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncropbottom_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropbottom.MouseDown
        Try
            If PictureBox2.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call util_ImageCropTop()
            cropString = "bottom"
            Timer2.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncropleft_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropleft.MouseDown
        Try
            If PictureBox2.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call util_ImageCropTop()
            cropString = "left"
            Timer2.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncropright_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropright.MouseDown
        Try
            If PictureBox2.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call util_ImageCropTop()
            cropString = "right"
            Timer2.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncropbottom_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropbottom.MouseUp
        Try
            Timer2.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncropleft_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropleft.MouseUp
        Timer2.Enabled = False
    End Sub

    Private Sub btncropright_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropright.MouseUp
        Try
            Timer2.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncroptop_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncroptop.MouseUp
        Try
            Timer2.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            If cropString = "top" Then Call util_ImageCropTop()
            If cropString = "bottom" Then Call util_ImageCropBottom()
            If cropString = "left" Then Call util_ImageCropLeft()
            If cropString = "right" Then Call util_ImageCropRight()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnresetimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresetimage.Click
        Try
            thumbedItsMade = False
            util_ImageLoad(PictureBox2, mov_FanartORExtrathumbPath(), Utilities.DefaultFanartPath)
            btnresetimage.Enabled = False
            btnSaveCropped.Enabled = False
            lblMovFanartWidth.Text = PictureBox2.Image.Width
            lblMovFanartHeight.Text = PictureBox2.Image.Height
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnSaveCropped_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCropped.Click
        Try
            thumbedItsMade = False
            Try
                Dim stream As New System.IO.MemoryStream
                Utilities.SaveImage(PictureBox2.Image, mov_FanartORExtrathumbPath)
                lblMovFanartWidth.Text = PictureBox2.Image.Width
                lblMovFanartHeight.Text = PictureBox2.Image.Height
                If rbMovFanart.Checked Then ' i.e. this is a fanart task rather than an extrathumb task
                    PbMovieFanArt.Image = PictureBox2.Image 'if we are saving the main fanart then update the art on the main form view
                    '                Rating1.PictureInit = PbMovieFanArt.Image
                    For Each paths In Preferences.offlinefolders
                        If workingMovieDetails.fileinfo.fanartpath.IndexOf(paths) <> -1 Then
                            Dim mediapath As String
                            mediapath = Utilities.GetFileName(workingMovieDetails.fileinfo.fullpathandfilename)
                            Call mov_OfflineDvdProcess(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fullmoviebody.title, mediapath)
                        End If
                    Next
                End If
                btnresetimage.Enabled = False
                btnSaveCropped.Enabled = False
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovFanartUrlorBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovFanartUrlorBrowse.Click
        Try
            Dim t As New frmImageBrowseOrUrl
            t.Location = Me.PointToScreen(New Point(btnMovFanartUrlorBrowse.Left - 460, btnMovFanartUrlorBrowse.Top + 30))
            t.ShowDialog()
            If t.DialogResult = Windows.Forms.DialogResult.Cancel Or t.tb_PathorUrl.Text = "" Then
                t.Dispose()
                Exit Sub
            End If
            Dim PathOrUrl As String = t.tb_PathorUrl.Text
            t.Dispose()
            t = Nothing
            Dim eh As Boolean = Preferences.savefanart
            Preferences.savefanart = True
            Movie.SaveFanartImageToCacheAndPath(PathOrUrl, mov_FanartORExtrathumbPath)
            Preferences.savefanart = eh
            Dim exists As Boolean = IO.File.Exists(workingMovieDetails.fileinfo.fanartpath)

            If exists Then
                For Each paths In Preferences.offlinefolders
                    If workingMovieDetails.fileinfo.fanartpath.IndexOf(paths) <> -1 Then
                        Dim mediapath As String
                        mediapath = Utilities.GetFileName(workingMovieDetails.fileinfo.fullpathandfilename)
                        Call mov_OfflineDvdProcess(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fullmoviebody.title, mediapath)
                    End If
                Next

                util_ImageLoad(PictureBox2, mov_FanartORExtrathumbPath(), Utilities.DefaultFanartPath)

                util_ImageLoad(PbMovieFanArt, workingMovieDetails.fileinfo.fanartpath, Utilities.DefaultFanartPath)

                mov_SplitContainerAutoPosition()
            End If

            UpdateMissingFanart()
            XbmcLink_UpdateArtwork()
            'Panel3.Visible = True
        Catch ex As Exception
            MsgBox("Unable To Download Image")
        End Try
    End Sub

    Private Sub ButtonFanartSaveLoRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFanartSaveLoRes.Click
        SaveFanart(False)
    End Sub

    Private Sub ButtonFanartSaveHiRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFanartSaveHiRes.Click
        SaveFanart(True)
    End Sub

    Private Sub btnMovPasteClipboardFanart_Click(sender As Object, e As EventArgs) Handles btnMovPasteClipboardFanart.Click

        If AssignClipboardImage(PictureBox2) Then
            btnSaveCropped.Enabled = True
            lblMovFanartWidth.Text = PictureBox2.Image.Width
            lblMovFanartHeight.Text = PictureBox2.Image.Height
        End If

    End Sub

    Private Sub rbMovFanart_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMovFanart.CheckedChanged
        If rbMovFanart.Checked Then mov_DisplayFanart()
    End Sub

    Private Sub rbMovThumb1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMovThumb1.CheckedChanged
        If rbMovThumb1.Checked Then mov_DisplayFanart()
    End Sub

    Private Sub rbMovThumb2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMovThumb2.CheckedChanged
        If rbMovThumb2.Checked Then mov_DisplayFanart()
    End Sub

    Private Sub rbMovThumb3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMovThumb3.CheckedChanged
        If rbMovThumb3.Checked Then mov_DisplayFanart()
    End Sub

    Private Sub rbMovThumb4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMovThumb4.CheckedChanged
        If rbMovThumb4.Checked Then mov_DisplayFanart()
    End Sub

    Private Sub rbMovThumb5_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMovThumb5.CheckedChanged
        If rbMovThumb5.Checked Then mov_DisplayFanart()
    End Sub

    Private Sub BtnSearchGoogleFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchGoogleFanart.Click
        GoogleSearch("+screenshot")
    End Sub

    Private Sub btnPrevNextMissingFanart_Click(sender As System.Object, e As System.EventArgs) Handles btnPrevMissingFanart.Click, btnNextMissingFanart.Click
        btnPrevMissingFanart.Enabled = False
        btnNextMissingFanart.Enabled = False

        DataGridViewMovies.ClearSelection()
        DataGridViewMovies.Rows(sender.Tag).Selected = True
        DisplayMovie()
        UpdateMissingFanartNav()
        mov_FanartLoad()
    End Sub

#End Region

#Region "Movie Poster Tab"

    Private Sub PictureBoxAssignedMoviePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxAssignedMoviePoster.DoubleClick
        Try
            If Not PictureBoxAssignedMoviePoster.Tag Is Nothing Then
                Me.ControlBox = False
                MenuStrip1.Enabled = False
                Call util_ZoomImage(PictureBoxAssignedMoviePoster.Tag.ToString)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PictureBoxAssignedMoviePoster_DragEnter(ByVal sender As System.Object, ByVal e As DragEventArgs) Handles PictureBoxAssignedMoviePoster.DragEnter
        Try
            If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
                e.Effect = DragDropEffects.Copy
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PictureBoxAssignedMoviePoster_DragDrop(ByVal sender As System.Object, ByVal e As DragEventArgs) Handles PictureBoxAssignedMoviePoster.DragDrop
        Try
            Dim Pic As String = CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString  '"FileDrop", False)
            Dim FInfo As IO.FileInfo = New IO.FileInfo(Pic)
            If FInfo.Extension.ToLower() = ".jpg" Or FInfo.Extension.ToLower() = ".tbn" Or FInfo.Extension.ToLower() = ".bmp" Or FInfo.Extension.ToLower() = ".png" Then
                util_ImageLoad(PictureBoxAssignedMoviePoster, Pic, Utilities.DefaultPosterPath)
                lblCurrentLoadedPoster.Text = "Width: " & PictureBoxAssignedMoviePoster.Image.Width.ToString & "  Height: " & PictureBoxAssignedMoviePoster.Image.Height.ToString
                btnMoviePosterSaveCroppedImage.Enabled = True
            Else
                MessageBox.Show("Not a picture")
            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_IMDB_posters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_IMDB_posters.Click
        Try
            If Not workingMovieDetails.fullmoviebody.imdbid.Contains("tt") Then
                MsgBox("No IMDB ID" & vbCrLf & "Searching IMDB for Posters halted")
                Exit Sub
            End If
            messbox = New frmMessageBox("Please wait,", "", "Scraping Movie Poster List")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Call mov_PosterInitialise()
            Dim newobject2 As New imdb_thumbs.Class1
            Dim posters(,) As String = newobject2.getimdbposters(workingMovieDetails.fullmoviebody.imdbid)
            For f = 0 To UBound(posters)
                If posters(f, 0) <> Nothing Then
                    If posters(f, 1) = Nothing Then posters(f, 1) = posters(f, 0)
                    Dim newposters As New str_ListOfPosters(SetDefaults)
                    newposters.hdUrl = posters(f, 1)
                    newposters.ldUrl = posters(f, 0)
                    posterArray.Add(newposters)
                    newposters.ldUrl = Nothing
                    newposters.hdUrl = Nothing
                End If
            Next
            messbox.Close()
            Call mov_PosterSelectionDisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovPosterNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovPosterNext.Click
        Try
            For i = panelAvailableMoviePosters.Controls.Count - 1 To 0 Step -1
                panelAvailableMoviePosters.Controls.RemoveAt(i)
            Next
            messbox = New frmMessageBox("Please wait,", "", "Downloading Preview Images")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            currentPage += 1
            If currentPage = pageCount Then
                btnMovPosterNext.Enabled = False
            End If
            btnMovPosterPrev.Enabled = True
            Dim tempint As Integer = (currentPage * (10) + 1) - 10
            Dim tempint2 As Integer = currentPage * 10
            If tempint2 > posterArray.Count Then
                tempint2 = posterArray.Count
            End If
            Dim names As New List(Of String)()
            For f = tempint - 1 To tempint2 - 1
                names.Add(posterArray(f).ldUrl)
            Next
            lblMovPosterPages.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & posterArray.Count.ToString & " Images"
            Dim locationX As Integer = 0
            Dim locationY As Integer = 0
            Dim itemcounter As Integer = 0
            Dim tempboolean As Boolean = True
            For Each item As String In names
                Dim item2 As String = Utilities.Download2Cache(item)
                Try
                    posterPicBoxes() = New PictureBox()
                    With posterPicBoxes
                        .WaitOnLoad = True
                        .Location = New Point(locationX, locationY)
                        .Width = 123
                        .Height = 168
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .Visible = True
                        .BorderStyle = BorderStyle.Fixed3D
                        .Name = "poster" & itemcounter.ToString
                        AddHandler posterPicBoxes.DoubleClick, AddressOf util_ZoomImage2
                        AddHandler posterPicBoxes.LoadCompleted, AddressOf util_ImageRes
                    End With
                    util_ImageLoad(posterPicBoxes, item2, "")

                    posterCheckBoxes() = New RadioButton()
                    With posterCheckBoxes
                        .Location = New Point(locationX + 50, locationY +166)
                        .Name = "postercheckbox" & itemcounter.ToString
                        .SendToBack()
                        .Text = " "
                        AddHandler posterCheckBoxes.CheckedChanged, AddressOf mov_PosterRadioChanged
                    End With
                    itemcounter += 1
                    Me.panelAvailableMoviePosters.Controls.Add(posterPicBoxes())
                    Me.panelAvailableMoviePosters.Controls.Add(posterCheckBoxes())
                    Me.Refresh()
                    Application.DoEvents()
                    If tempboolean = True Then
                        locationY = 192
                    Else
                        locationX += 120
                        locationY = 0
                    End If
                    tempboolean = Not tempboolean
                Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                End Try
            Next
            messbox.Close()
            Me.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovPosterPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovPosterPrev.Click
        Try
            For i = panelAvailableMoviePosters.Controls.Count - 1 To 0 Step -1
                panelAvailableMoviePosters.Controls.RemoveAt(i)
            Next
            messbox = New frmMessageBox("Please wait,", "", "Downloading Preview Images")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            currentPage -= 1
            If currentPage = 1 Then
                btnMovPosterPrev.Enabled = False
            End If
            btnMovPosterNext.Enabled = True
            Dim tempint As Integer = (currentPage * (10) + 1) - 10
            Dim tempint2 As Integer = currentPage * 10
            If tempint2 > posterArray.Count Then
                tempint2 = posterArray.Count
            End If
            Dim names As New List(Of String)()
            For f = tempint - 1 To tempint2 - 1
                names.Add(posterArray(f).ldUrl)
            Next
            lblMovPosterPages.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & posterArray.Count.ToString & " Images"
            Dim locationX As Integer = 0
            Dim locationY As Integer = 0
            Dim itemcounter As Integer = 0
            Dim tempboolean As Boolean = True
            For Each item As String In names
                Dim item2 As String = Utilities.Download2Cache(item)
                posterPicBoxes() = New PictureBox()
                With posterPicBoxes
                    .WaitOnLoad = True
                    .Location = New Point(locationX, locationY)
                    .Width = 123
                    .Height = 168
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "poster" & itemcounter.ToString
                    AddHandler posterPicBoxes.DoubleClick, AddressOf util_ZoomImage2
                    AddHandler posterPicBoxes.LoadCompleted, AddressOf util_ImageRes
                End With
                util_ImageLoad(posterPicBoxes, item2, "")

                posterCheckBoxes() = New RadioButton()
                With posterCheckBoxes
                    .Location = New Point(locationX + 50, locationY + 166)
                    .Name = "postercheckbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler posterCheckBoxes.CheckedChanged, AddressOf mov_PosterRadioChanged
                End With
                itemcounter += 1
                Me.panelAvailableMoviePosters.Controls.Add(posterPicBoxes())
                Me.panelAvailableMoviePosters.Controls.Add(posterCheckBoxes())
                Me.Refresh()
                Application.DoEvents()
                If tempboolean = True Then
                    locationY = 192
                Else
                    locationX += 120
                    locationY = 0
                End If
                tempboolean = Not tempboolean
            Next
            messbox.Close()
            Me.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_TMDb_posters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TMDb_posters.Click
        Try
            messbox = New frmMessageBox("Please wait,", "", "Scraping Movie Poster List")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Call mov_PosterInitialise()
            Try
                Dim tmdb As New TMDb
                tmdb.Imdb = If(workingMovieDetails.fullmoviebody.imdbid.Contains("tt"), workingMovieDetails.fullmoviebody.imdbid, "")
                tmdb.TmdbId = workingMovieDetails.fullmoviebody.tmdbid
                posterArray.AddRange(tmdb.MC_Posters)
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            Finally
                messbox.Close()
                Call mov_PosterSelectionDisplay()
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_MPDB_posters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_MPDB_posters.Click
        Try
            If Not workingMovieDetails.fullmoviebody.imdbid.Contains("tt") Then
                MsgBox("No IMDB ID" & vbCrLf & "Searching Movie Poster DB halted")
                Exit Sub
            End If
            messbox = New frmMessageBox("Please wait,", "", "Scraping Movie Poster List")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Call mov_PosterInitialise()
            Dim newobject As New class_mpdb_thumbs.Class1
            Dim teststring As New XmlDocument
            Dim testthumbs As String = String.Empty
            Try
                testthumbs = newobject.get_mpdb_thumbs(workingMovieDetails.fullmoviebody.imdbid)
                testthumbs = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                teststring.LoadXml(testthumbs)
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
                Thread.Sleep(1)
            End Try
            Dim thumbstring As New XmlDocument
            Try
                thumbstring.LoadXml(testthumbs)
                For Each thisresult In thumbstring("totalthumbs")
                    Select Case thisresult.Name
                        Case "thumb"
                            Dim newposters As New str_ListOfPosters(SetDefaults)
                            newposters.hdUrl = thisresult.InnerText
                            newposters.ldUrl = thisresult.InnerText
                            posterArray.Add(newposters)
                            newposters.ldUrl = Nothing
                            newposters.hdUrl = Nothing
                    End Select
                Next
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
            messbox.Close()
            Call mov_PosterSelectionDisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    

    Private Sub btn_IMPA_posters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_IMPA_posters.Click
        Try
            messbox = New frmMessageBox("Please wait,", "", "Scraping Movie Poster List")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Call mov_PosterInitialise()
            Dim newobject2 As New IMPA.getimpaposters
            Try
                Dim posters(,) As String = newobject2.getimpaafulllist(workingMovieDetails.fullmoviebody.title, workingMovieDetails.fullmoviebody.year)
                For f = 0 To UBound(posters)
                    If posters(f, 0) <> Nothing Then
                        If posters(f, 1) = Nothing Then posters(f, 1) = posters(f, 0)
                        Dim newposters As New str_ListOfPosters(SetDefaults)
                        newposters.hdUrl = posters(f, 0)
                        newposters.ldUrl = posters(f, 1)
                        posterArray.Add(newposters)
                        newposters.ldUrl = Nothing
                        newposters.hdUrl = Nothing
                    End If
                Next
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
            messbox.Close()
            Call mov_PosterSelectionDisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnPosterTabs_SaveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterTabs_SaveImage.Click
        Try
            Dim tempstring As String = ""
            Dim tempint As Integer = 0
            Dim realnumber As Integer = 0
            Dim tempstring2 As String = ""
            Dim allok As Boolean = False
            Dim backup As String = ""
            messbox = New frmMessageBox("Downloading Poster...")
            messbox.Show()
            For Each button As Control In Me.panelAvailableMoviePosters.Controls
                If button.Name.IndexOf("postercheckbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        tempstring = b1.Name
                        If tempstring.IndexOf("postercheckbox") <> -1 Then
                            tempstring = tempstring.Replace("postercheckbox", "")
                            tempint = Convert.ToDecimal(tempstring)
                            If tempstring2 = Nothing Then
                                tempint = Convert.ToDecimal(tempstring)
                                tempint = tempint + ((currentPage - 1) * 10)
                                If cbMoviePosterSaveLoRes.Enabled = True Then
                                    If cbMoviePosterSaveLoRes.CheckState = CheckState.Checked Then
                                        tempstring2 = posterArray(tempint).ldUrl
                                    Else
                                        tempstring2 = posterArray(tempint).hdUrl
                                        backup = posterArray(tempint).ldUrl
                                    End If
                                Else
                                    tempstring2 = posterArray(tempint).hdUrl
                                End If
                                allok = True
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next
            If allok = False Then
                MsgBox("No Poster Is Selected")
                Return
            End If
            Try
                util_ImageLoad(PictureBoxAssignedMoviePoster, Utilities.DefaultPosterPath, Utilities.DefaultPosterPath)
                Dim Paths As List(Of String) = Preferences.GetPosterPaths(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fileinfo.videotspath)
                Dim success As Boolean = DownloadCache.SaveImageToCacheAndPaths(tempstring2, Paths, False, , ,True)

                Dim path As String = Utilities.save2postercache(workingMovieDetails.fileinfo.fullpathandfilename, Paths(0))
                updateposterwall(path, workingMovieDetails.fileinfo.fullpathandfilename)
                util_ImageLoad(PictureBoxAssignedMoviePoster, Paths(0), Utilities.DefaultPosterPath)
                util_ImageLoad(PbMoviePoster, Paths(0), Utilities.DefaultPosterPath)
                lblCurrentLoadedPoster.Text = "Width: " & PictureBoxAssignedMoviePoster.Image.Width.ToString & "  Height: " & PictureBoxAssignedMoviePoster.Image.Height.ToString
                lblCurrentLoadedPoster.Refresh()

                XbmcLink_UpdateArtwork()
            Catch ex As Exception
                ExceptionHandler.LogError(ex)
#If SilentErrorScream Then
                Throw ex
#End If
            End Try
            UpdateMissingPoster()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
        messbox.Close()
    End Sub

    Private Sub btnMovPosterURLorBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovPosterURLorBrowse.Click
        Try
            Dim t As New frmImageBrowseOrUrl
            t.Location = Me.PointToScreen(New Point(gbMoviePosterSelection.Left + 10, gbMoviePosterSelection.Top + gbMoviePosterSelection.Height))
            t.ShowDialog()
            If t.DialogResult = Windows.Forms.DialogResult.Cancel Or t.tb_PathorUrl.Text = "" Then
                t.Dispose()
                Exit Sub
            End If
            Dim PathOrUrl As String = t.tb_PathorUrl.Text
            t.Dispose()
            t = Nothing
            Dim tempstring As String = ""
            Dim aok As Boolean = True
            Dim cachename As String = Utilities.Download2Cache(PathOrUrl)
            If cachename <> "" Then
                Dim Paths As List(Of String) = Preferences.GetPosterPaths(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fileinfo.videotspath)
                For Each pth As String In Paths
                    Try
                        File.Copy(cachename, pth, True)
                    Catch ex As Exception
                        aok = False
                    End Try
                Next
                If aok Then
                    util_ImageLoad(PictureBoxAssignedMoviePoster, cachename, Utilities.DefaultPosterPath)
                    util_ImageLoad(PbMoviePoster, cachename, Utilities.DefaultPosterPath)
                    Dim path As String = Utilities.save2postercache(workingMovieDetails.fileinfo.fullpathandfilename, cachename)
                    updateposterwall(path, workingMovieDetails.fileinfo.fullpathandfilename)
                End If
            Else
                aok = False
            End If
            If Not aok Then MsgBox("There was a problem downloading the Image")

            UpdateMissingPoster()
            XbmcLink_UpdateArtwork()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub BtnGoogleSearchPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGoogleSearchPoster.Click
        GoogleSearch("+poster")
    End Sub

    Private Sub btnMoviePosterResetImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoviePosterResetImage.Click
        Try
            PictureBoxAssignedMoviePoster.Image = PbMoviePoster.Image
            btnMoviePosterResetImage.Enabled = False
            btnMoviePosterSaveCroppedImage.Enabled = False
            lblCurrentLoadedPoster.Text = "Width: " & PictureBoxAssignedMoviePoster.Image.Width.ToString & "  Height: " & PictureBoxAssignedMoviePoster.Image.Height.ToString
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMoviePosterSaveCroppedImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoviePosterSaveCroppedImage.Click
        Try
            Try
                Dim posterpath As String = ""
                Dim stream As New System.IO.MemoryStream
                Dim PostPaths As List(Of String) = Preferences.GetPosterPaths(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fileinfo.videotspath)
                Dim bitmap3 As New Bitmap(PictureBoxAssignedMoviePoster.Image)
                For Each pth As String In PostPaths
                    bitmap3.Save(pth, System.Drawing.Imaging.ImageFormat.Jpeg)
                    posterpath = pth
                Next
                bitmap3.Dispose()
                GC.Collect()
                util_ImageLoad(PbMoviePoster, posterpath, Utilities.DefaultPosterPath) '.Image = bmp4
                btnMoviePosterResetImage.Enabled = False
                btnMoviePosterSaveCroppedImage.Enabled = False
                Dim path As String = Utilities.save2postercache(workingMovieDetails.fileinfo.fullpathandfilename, workingMovieDetails.fileinfo.posterpath)
                updateposterwall(path, workingMovieDetails.fileinfo.fullpathandfilename)
                XbmcLink_UpdateArtwork()
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovPasteClipboardPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovPasteClipboardPoster.Click
        If AssignClipboardImage(PictureBoxAssignedMoviePoster) Then
            lblCurrentLoadedPoster.Text = "Width: " & PictureBoxAssignedMoviePoster.Image.Width.ToString & "  Height: " & PictureBoxAssignedMoviePoster.Image.Height.ToString
            btnMoviePosterSaveCroppedImage.Enabled = True
        End If
    End Sub

    Private Sub btn_MovEnableCrop_Click(sender As System.Object, e As System.EventArgs) Handles btnMoviePosterEnableCrop.Click
        Try
            cropMode = "movieposter"
            Dim t As New frmMovPosterCrop
            If Preferences.MultiMonitoEnabled Then
                t.Bounds = screen.AllScreens(CurrentScreen).Bounds
                t.StartPosition = FormStartPosition.Manual
            End If
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnPrevNextMissingPoster_Click(sender As System.Object, e As System.EventArgs) Handles btnPrevMissingPoster.Click, btnNextMissingPoster.Click
        btnPrevMissingPoster.Enabled = False
        btnNextMissingPoster.Enabled = False

        PictureBoxAssignedMoviePoster.Image = Nothing
        DataGridViewMovies.ClearSelection()
        DataGridViewMovies.Rows(sender.Tag).Selected = True
        DisplayMovie()
        UpdateMissingPosterNav()
    End Sub

#End Region

#Region "Movie Sets & Tags Tab"

    Private Sub MovieSetsAndTagsSetup()
        Dim MsetCache As New List(Of MovieSetDatabase)
        oMovies.LoadMovieSetCache(MsetCache, "movieset", Preferences.workingProfile.moviesetcache)
        MovieSetMissingID = False
        ListofMovieSets.Items.Clear()
        For Each mset In Preferences.moviesets
            If mset <> "-None-" Then
                ListofMovieSets.Items.Add(mset)
                If MsetCache.Count <> 0 Then
                    Dim q = From x In MsetCache Where x.MovieSetName = mset Select x.MovieSetId
                    If q.ToString = "" Then MovieSetMissingID = True
                End If
            End If
        Next
        TagListBox.Items.Clear()
        For Each mtag In Preferences.movietags
            If Not IsNothing(mtag) Then TagListBox.Items.Add(mtag)
        Next
        CurrentMovieTags.Items.Clear()
        For Each item As DataGridViewRow In DataGridViewMovies.SelectedRows
            Dim filepath As String = item.Cells("fullpathandfilename").Value.ToString
            Dim movie As Movie = oMovies.LoadMovie(filepath)
            For Each ctag In movie.ScrapedMovie.fullmoviebody.tag
                If Not IsNothing(ctag) Then
                    If Not CurrentMovieTags.Items.Contains(ctag) Then CurrentMovieTags.Items.Add(ctag)
                End If
            Next
        Next
    End Sub

    'Tag(s) Section
    Private Sub btnMovTagListAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnMovTagListAdd.Click
        Try
            If txtbxMovTagEntry.Text <> "" Then
                Dim ex As Boolean = False
                For Each mtag In Preferences.movietags
                    If mtag.ToLower = txtbxMovTagEntry.Text.ToLower Then
                        ex = True
                        Exit For
                    End If
                Next
                If ex = False Then
                    Preferences.movietags.Add(txtbxMovTagEntry.Text)
                    TagListBox.Items.Add(txtbxMovTagEntry.Text)
                    txtbxMovTagEntry.Clear()
                Else
                    MsgBox("This Movie Tag Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovTagListRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnMovTagListRemove.Click
        Try
            For i = 0 To TagListBox.SelectedItems.Count - 1
                Dim tempboolean As Boolean = False
                If TagListBox.SelectedItems(i) <> Nothing And TagListBox.SelectedItems(i) <> "" Then
                    For Each mtag In Preferences.movietags
                        If mtag = TagListBox.SelectedItems(i) Then
                            Preferences.movietags.Remove(mtag)
                            Exit For
                        End If
                    Next
                End If
            Next

            TagListBox.Items.Clear()

            For Each mset In Preferences.movietags
                If Not IsNothing(mset) Then TagListBox.Items.Add(mset)
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovTagListRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnMovTagListRefresh.Click
        Try
            TagListBox.Items.Clear()
            For x = 0 To oMovies.MovieCache.Count - 1
                Dim movtag As List(Of String) = oMovies.MovieCache(x).movietag
                For Each mtag In movtag
                    If Not TagListBox.Items.Contains(mtag) Then
                        TagListBox.Items.Add(mtag)
                    End If
                Next
            Next
            Preferences.movietags.Clear()
            For Each mtag In TagListBox.Items
                Preferences.movietags.Add(mtag)
            Next
            UpdateFilteredList()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMovTagAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnMovTagAdd.Click
        Try
            If TagListBox.SelectedIndex <> -1 Then
                For Each item In TagListBox.SelectedItems
                    If item = "" Then Exit For
                    If Not CurrentMovieTags.Items.Contains(item) Then
                        CurrentMovieTags.Items.Add(item)
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMovTagRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnMovTagRemove.Click
        Try
            If CurrentMovieTags.SelectedIndex <> -1 Then
                For Each item In CurrentMovieTags.SelectedItems
                    If workingMovieDetails.fullmoviebody.tag.Contains(item) Then workingMovieDetails.fullmoviebody.tag.Remove(item)
                Next
                CurrentMovieTags.Items.Clear()
                For Each mtag In workingMovieDetails.fullmoviebody.tag
                    CurrentMovieTags.Items.Add(mtag)
                Next

                Call mov_SaveQuick()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMovTagSavetoNfo_Click(sender As System.Object, e As System.EventArgs) Handles btnMovTagSavetoNfo.Click
        Try
            If CurrentMovieTags.Items.Count <> -1 Then
                NewTagList.Clear()
                For Each tags In CurrentMovieTags.Items
                    NewTagList.Add(tags)
                Next
                Call mov_SaveQuick()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtbxMovTagEntry_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtbxMovTagEntry.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then

            btnMovTagListAdd.PerformClick()

            'This tells the system not to process
            'the key, as you've already taken care 
            'of it 
            e.Handled = True
        End If

    End Sub

    'Sets section

    Private Sub btnMovieSetAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetAdd.Click
        Try
            If tbMovSetEntry.Text <> "" Then
                Dim ex As Boolean = False
                For Each mset In Preferences.moviesets
                    If mset.ToLower = tbMovSetEntry.Text.ToLower Then
                        ex = True
                        Exit For
                    End If
                Next
                If ex = False Then
                    Preferences.moviesets.Add(tbMovSetEntry.Text)
                    ListofMovieSets.Items.Add(tbMovSetEntry.Text)
                    pop_cbMovieDisplay_MovieSet()
                    tbMovSetEntry.Clear()
                Else
                    MsgBox("This Movie Set Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tbMovSetEntry_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbMovSetEntry.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            btnMovieSetAdd.PerformClick()
            e.Handled = True
        End If

    End Sub

    Private Sub btnMovieSetRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieSetRemove.Click
        Try
            For i = 0 To ListofMovieSets.SelectedItems.Count - 1
                Dim tempboolean As Boolean = False
                If ListofMovieSets.SelectedItems(i) <> Nothing And ListofMovieSets.SelectedItems(i) <> "" Then
                    For Each mset In Preferences.moviesets
                        If mset = ListofMovieSets.SelectedItems(i) Then
                            If workingMovieDetails.fullmoviebody.movieset.MovieSetName <> mset Then
                                Preferences.moviesets.Remove(mset)
                            Else
                                MsgBox("Unable to remove """ & mset & """, it is being used by the selected Movie")
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next
            ListofMovieSets.Items.Clear()
            For Each mset In Preferences.moviesets
                If mset <> "-None-" Then ListofMovieSets.Items.Add(mset)
            Next
            pop_cbMovieDisplay_MovieSet()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMovieSetsRepopulateFromUsed_Click(sender As System.Object, e As System.EventArgs) Handles btnMovieSetsRepopulateFromUsed.Click
        MovSetsRepopulate()
        ListofMovieSets.Items.Clear()
        ListofMovieSets.Items.AddRange(oMovies.MoviesSetsExNone.ToArray)
        pop_cbMovieDisplay_MovieSet()
    End Sub

    Private Sub MovSetsRepopulate()
        Preferences.moviesets.Clear()
        Preferences.moviesets.Add("-None-")
        Preferences.moviesets.AddRange(oMovies.MoviesSetsExNone)
    End Sub

#End Region

#Region "Movie Change Movie Tab"

    Private Sub WebBrowser1_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow
        Try
            e.Cancel = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnChangeMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeMovie.Click
        Dim messagestring As String = "Changing the movie will Overwrite all the current details"
        messagestring &= vbCrLf & "If this is an offline video, please delete folder and add as New" & vbCrLf & "Do you wish to continue?"
        If MovieSearchEngine = "imdb" Then
            Dim mat = Regex.Match(WebBrowser1.Url.ToString, "(tt\d{7})")
            If mat.Success Then
                ChangeMovieId = mat.Value
            Else
                MsgBox("Please Browse to a Movie page")
                Exit Sub
            End If
            If MessageBox.Show(messagestring, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                Exit Sub
            End If
            Preferences.MovieChangeMovie = True
            RunBackgroundMovieScrape("ChangeMovie")
        ElseIf MovieSearchEngine = "tmdb" Then
            Dim mat As String = WebBrowser1.Url.ToString
            mat = mat.Substring(mat.LastIndexOf("/")+1, mat.Length - mat.LastIndexOf("/")-1)
            Dim urlsplit As String()
            urlsplit = Split(mat, "-")
            If Integer.TryParse(urlsplit(0), Nothing) Then
                ChangeMovieId = urlsplit(0)
            Else
                MsgBox("Please Browse to a Movie page")
                Exit Sub
            End If
            If MessageBox.Show(messagestring, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                Exit Sub
            End If
            Preferences.MovieChangeMovie = True
            RunBackgroundMovieScrape("ChangeMovie")
        End If

        TabControl2.SelectedIndex = 0
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try
            WebBrowser1.GoBack()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_IMDBSearch_Click(sender As System.Object, e As System.EventArgs) Handles btn_IMDBSearch.Click
        Try
            MovieSearchEngine = "imdb"
            mov_ChangeMovieSetup(MovieSearchEngine)
        Catch
        End Try
    End Sub

    Private Sub btn_TMDBSearch_Click(sender As System.Object, e As System.EventArgs) Handles btn_TMDBSearch.Click
        Try
            MovieSearchEngine = "tmdb"
            mov_ChangeMovieSetup(MovieSearchEngine)
        Catch
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Preferences.MovieChangeKeepExistingArt =  Not CheckBox2.Checked
    End Sub

#End Region

#Region "Movie Folder Tab"

    'Movie Folders
    Private Sub btnMovieManualPathAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnMovieManualPathAdd.Click
        Try
            If tbMovieManualPath.Text = Nothing Then
                Exit Sub
            End If
            If tbMovieManualPath.Text = "" Then
                Exit Sub
            End If
            Dim tempstring As String = tbMovieManualPath.Text
            Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Dim exists As Boolean = False
            For Each item In ListBox7.Items
                If item.ToString.ToLower = tempstring.ToLower Then
                    exists = True
                    Exit For
                End If
            Next
            If exists = True Then
                MsgBox("        Folder Already Exists")
            Else
                Dim f As New IO.DirectoryInfo(tempstring)
                If f.Exists Then
                    ListBox7.Items.Add(tempstring)
                    ListBox7.Refresh()
                    tbMovieManualPath.Text = ""
                    'newTvFolders.Add(tempstring)
                Else
                    Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If tempint = DialogResult.Yes Then
                        ListBox7.Items.Add(tempstring)
                        ListBox7.Refresh()
                        tbMovieManualPath.Text = ""
                        'newTvFolders.Add(tempstring)
                    End If
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_addmoviefolderdialogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addmoviefolderdialogue.Click
        Try
            Dim allok As Boolean = True
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                Preferences.lastpath = thefoldernames
                If allok = True Then
                    ListBox7.Items.Add(thefoldernames)
                    ListBox7.Refresh()
                Else
                    MsgBox("        Folder Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox7_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox7.DragDrop
        Dim folders() As String
        droppedItems.Clear()
        folders = e.Data.GetData(DataFormats.filedrop)
        For f = 0 To UBound(folders)
            If Preferences.movieFolders.Contains(folders(f)) Then Continue For
            If ListBox7.Items.Contains(folders(f)) Then Continue For
		    Dim skip As Boolean = False
		    For Each item In droppedItems
			    If item = folders(f) Then
				    skip = True
				    Exit For
			    End If
		    Next
		If Not skip Then droppedItems.Add(folders(f))
        Next
        If droppedItems.Count < 1 Then Exit Sub
        For Each item In droppedItems
            ListBox7.Items.Add(item)
        Next
        ListBox7.Refresh()
    End Sub

    Private Sub ListBox7_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox7.DragEnter
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox7_KeyPress(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ListBox7.KeyDown
        If e.KeyCode = Keys.Delete AndAlso ListBox7.SelectedItem <> Nothing
            Call btn_removemoviefolder.PerformClick()
        End If
    End Sub

    Private Sub btn_removemoviefolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_removemoviefolder.Click
        Try
            While ListBox7.SelectedItems.Count > 0
                ListBox7.Items.Remove(ListBox7.SelectedItems(0))
            End While
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    'Offline Movie Folders
    Private Sub Button107_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button107.Click
        Try
            'listbox15
            If ListBox15.SelectedItem <> Nothing Then
                Dim tempstring As String = TextBox44.Text
                tempstring = tempstring.Replace("?", "")
                tempstring = tempstring.Replace("/", "")
                tempstring = tempstring.Replace("\", "")
                tempstring = tempstring.Replace("<", "")
                tempstring = tempstring.Replace(">", "")
                tempstring = tempstring.Replace(":", "")
                tempstring = tempstring.Replace("""", "")
                tempstring = tempstring.Replace("*", "")
                If tempstring.Length <> 0 Then
                    Try
                        Dim temppath As String = IO.Path.Combine(ListBox15.SelectedItem, tempstring)
                        Dim f As New IO.DirectoryInfo(temppath)
                        If Not f.Exists Then
                            IO.Directory.CreateDirectory(temppath)
                            MsgBox("Folder Created")
                        Else
                            MsgBox("Folder Already Exists")
                        End If
                    Catch ex As Exception
                        MsgBox("Unable to create folder" & vbCrLf & ex.Message.ToString)
                    End Try
                End If
            Else
                MsgBox("Please Select a folder from the above listbox")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button108_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button108.Click
        Try
            If ListBox15.SelectedItem <> Nothing Then
                Dim tempint As Integer = 0
                Dim textfilename As String = ""
                Dim filebrowser As New OpenFileDialog
                Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                filebrowser.InitialDirectory = mstrProgramFilesPath
                filebrowser.Filter = "Text Files|*.txt"
                filebrowser.Title = "Select text file to load"
                If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                    textfilename = filebrowser.FileName
                End If
                If textfilename <> "" Then
                    Using textfile As StreamReader = New StreamReader(textfilename)
                        Dim line As String
                        line = textfile.ReadLine
                        Do While (Not line Is Nothing)
                            ' Add this line to list.
                            Dim tempstring As String = line
                            tempstring = tempstring.Replace("?", "")
                            tempstring = tempstring.Replace("/", "")
                            tempstring = tempstring.Replace("\", "")
                            tempstring = tempstring.Replace("<", "")
                            tempstring = tempstring.Replace(">", "")
                            tempstring = tempstring.Replace(":", "")
                            tempstring = tempstring.Replace("""", "")
                            tempstring = tempstring.Replace("*", "")
                            If tempstring.Length <> 0 Then
                                Try
                                    Dim temppath As String = IO.Path.Combine(ListBox15.SelectedItem, tempstring)
                                    Dim f As New IO.DirectoryInfo(temppath)
                                    If Not f.Exists Then
                                        tempint += 1
                                        IO.Directory.CreateDirectory(temppath)
                                    Else

                                    End If
                                Catch ex As Exception
#If SilentErrorScream Then
                                Throw ex
#End If
                                End Try
                            End If
                            line = textfile.ReadLine
                        Loop
                    End Using
                    MsgBox(tempint.ToString & " Movie Folders added")
                End If
            Else
                MsgBox("Please Select a folder from the above listbox")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button102_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button102.Click
        Try
            'add offline movie folder browser
            Dim allok As Boolean = True
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select a Root Offline DVD Folder to Add to DB"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                Preferences.lastpath = thefoldernames
                For Each item As Object In ListBox15.Items
                    If thefoldernames.ToString = item.ToString Then allok = False
                Next

                If allok = True Then
                    ListBox15.Items.Add(thefoldernames)
                    ListBox15.Refresh()
                Else
                    MsgBox("        Folder Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button101_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button101.Click
        Try
            'remove selected offline movie folders
            While ListBox15.SelectedItems.Count > 0
                ListBox15.Items.Remove(ListBox15.SelectedItems(0))
            End While
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ButtonSaveAndQuickRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveAndQuickRefresh.Click
        'ProgressAndStatus1.Display()
        'Application.DoEvents()

        Dim folderstoadd As New List(Of String)
        Dim offlinefolderstoadd As New List(Of String)
        Dim folderstoremove As New List(Of String)
        Dim offlinefolderstoremove As New List(Of String)
        For Each item In ListBox7.Items
            Dim add As Boolean = True
            For Each folder In movieFolders
                If folder = item Then add = False
            Next
            If add = True Then folderstoadd.Add(item)
        Next
        For Each item In ListBox15.Items
            Dim add As Boolean = True
            For Each folder In Preferences.offlinefolders
                If folder = item Then add = False
            Next
            If add = True Then offlinefolderstoadd.Add(item)
        Next
        For Each item In movieFolders
            Dim remove As Boolean = True
            For Each folder In ListBox7.Items
                If folder = item Then remove = False
            Next
            If remove = True Then folderstoremove.Add(item)
        Next
        For Each item In Preferences.offlinefolders
            Dim remove As Boolean = True
            For Each folder In ListBox15.Items
                If folder = item Then remove = False
            Next
            If remove = True Then offlinefolderstoremove.Add(item)
        Next
        If folderstoremove.Count > 0 Or offlinefolderstoremove.Count > 0 Then
            For Each item In folderstoremove
                For f = oMovies.MovieCache.Count - 1 To 0 Step -1
                    If oMovies.MovieCache(f).fullpathandfilename.IndexOf(item) <> -1 Then
                        oMovies.MovieCache.RemoveAt(f)
                    End If
                Next
            Next
            For Each item In offlinefolderstoremove
                For f = oMovies.MovieCache.Count - 1 To 0 Step -1
                    If oMovies.MovieCache(f).fullpathandfilename.IndexOf(item) <> -1 Then
                        oMovies.MovieCache.RemoveAt(f)
                    End If
                Next
            Next
            For f = movieFolders.Count - 1 To 0 Step -1
                Dim remove As Boolean = False
                For Each folder In folderstoremove
                    If movieFolders(f) = folder Then
                        remove = True
                    End If
                Next
                If remove = True Then movieFolders.RemoveAt(f)
            Next
            For f = Preferences.offlinefolders.Count - 1 To 0 Step -1
                Dim remove As Boolean = False
                For Each folder In offlinefolderstoremove
                    If Preferences.offlinefolders(f) = folder Then
                        remove = True
                    End If
                Next
                If remove = True Then Preferences.offlinefolders.RemoveAt(f)
            Next
            Preferences.ConfigSave()
        End If
        If folderstoadd.Count > 0 Or offlinefolderstoadd.Count > 0 Then
            Application.DoEvents()

            For Each folder In folderstoadd
                movieFolders.Add(folder)
            Next
            For Each folder In offlinefolderstoadd
                Preferences.offlinefolders.Add(folder)
                folderstoadd.Add(folder)
            Next
            If Preferences.usefoldernames = True Then         'use TRUE if folder contains nfo's, False if folder contains folders which contain nfo's
                progressmode = False
            Else
                progressmode = True
            End If
            'Call mov_NfoLoad(folderstoadd, progressmode)
            Preferences.ConfigSave()
            '     messbox.Close 'Where's the open???
        End If

        mov_RebuildMovieCaches()
        TabControl2.SelectedIndex = 0
    End Sub

#End Region   'Controls for Movie Folders tab

#Region "Tv Browser Form"

    Private Sub TvTreeview_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TvTreeview.DoubleClick
        Try
            If TvTreeview.SelectedNode Is Nothing Then Exit Sub
            Dim tempstring2 As String
            Dim tempstring As String = ""
            Dim pathandfilename As String = TvTreeview.SelectedNode.Name
            If pathandfilename.IndexOf("tvshow.nfo") <> -1 Then Exit Sub
            If pathandfilename = "" Then Exit Sub
            If pathandfilename <> Nothing Then
                If pathandfilename.ToLower.Substring(pathandfilename.Length - 4, 4) = ".nfo" Then
                    pathandfilename = pathandfilename.Substring(0, pathandfilename.Length - 4)

                    Dim exists As Boolean = False
                    For Each ext In Utilities.VideoExtensions
                        If ext = "VIDEO_TS.IFO" Then Continue For
                        tempstring2 = pathandfilename & ext

                        If IO.File.Exists(tempstring2) Then
                            exists = True
                            tempstring = applicationPath & "\settings\temp.m3u"
                            Dim file As IO.StreamWriter = IO.File.CreateText(tempstring)
                            file.WriteLine(tempstring2)
                            file.Close()



                            If Preferences.videomode = 1 Then Call util_VideoMode1(tempstring)
                            If Preferences.videomode = 2 Then Call util_VideoMode2(tempstring)

                            If Preferences.videomode = 3 Then
                                Preferences.videomode = 2
                                Call util_VideoMode2(tempstring)
                            End If

                            If Preferences.videomode >= 4 Then
                                If Preferences.selectedvideoplayer <> Nothing Then
                                    Call util_VideoMode4(tempstring)
                                Else
                                    Call util_VideoMode1(tempstring)
                                End If
                            End If
                            Exit For
                        End If
                    Next
                    If exists = False Then
                        MsgBox("Could not find file: """ & pathandfilename & """ with any supported extension")
                    End If
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub TvTreeview_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles TvTreeview.MouseHover
        TvTreeview.Focus()
    End Sub

    Private Sub TvTreeview_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TvTreeview.MouseUp

        If e.Button = MouseButtons.Right Then
            Dim pt As Point
            pt.X = e.X
            pt.Y = e.Y
            'MovieListComboBox.SelectedIndex = MovieListComboBox.IndexFromPoint(pt)

            Dim objMousePosition As Point = DataGridViewMovies.PointToClient(Control.MousePosition)
            Dim objHitTestInfo As DataGridView.HitTestInfo
            objHitTestInfo = DataGridViewMovies.HitTest(pt.X, pt.Y)
            'DataGridViewMovies.Rows(objHitTestInfo.RowIndex).Selected = True

            TvTreeview.SelectedNode = TvTreeview.GetNodeAt(TvTreeview.PointToClient(Cursor.Position)) '***select actual the node 

            'context menu will be shown soon so we modify it to suit...***after*** we make the selection of the node 

            Tv_TreeViewContextMenuItemsEnable()

        End If

    End Sub

    Private Sub rbTvDisplayFiltering_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTvDisplayUnWatched.CheckedChanged, 
                                                                                                                        rbTvDisplayWatched.CheckedChanged,
                                                                                                                        rbTvMissingAiredEp.CheckedChanged,
                                                                                                                        rbTvMissingEpisodes.CheckedChanged,
                                                                                                                        rbTvMissingPoster.CheckedChanged,
                                                                                                                        rbTvListAll.CheckedChanged,
                                                                                                                        rbTvMissingFanart.CheckedChanged,
                                                                                                                        rbTvMissingThumb.CheckedChanged
        Try
            Call tv_Filter()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_EpWatched.Click
        Try
            Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
            Dim multi As Boolean = TestForMultiepisode(ep_SelectedCurrently.NfoFilePath)
            If multi = False Then
                util_EpisodeSetWatched(WorkingEpisode.PlayCount.Value, True)
                WorkingEpisode.Save()
                WorkingEpisode.UpdateTreenode()
            Else
                Dim episodelist As New List(Of TvEpisode)
                episodelist = WorkingWithNfoFiles.ep_NfoLoad(WorkingEpisode.NfoFilePath)
                Dim First As Boolean = True
                Dim done As String = ""
                For Each ep In episodelist
                    If First Then done = ep.PlayCount.Value
                    Dim toggled As String = done
                    util_EpisodeSetWatched(toggled, True)
                    ep.PlayCount.Value = toggled
                    ep.UpdateTreenode()
                    First = False
                Next
                WorkingWithNfoFiles.ep_NfoSave(episodelist, WorkingEpisode.NfoFilePath)
                'WorkingEpisode.Load()
                'WorkingEpisode.UpdateTreenode()
            End If
            Dim ThisSeason As TvSeason = tv_SeasonSelectedCurrently()
            ThisSeason.UpdateTreenode()
            tv_ShowSelectedCurrently.UpdateTreenode()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button44.Click
        Try
            tv_Rescrape()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox_Plot_DoubleClick(sender As System.Object, e As System.EventArgs) Handles tb_EpPlot.DoubleClick
        ShowBigTvEpisodeText()
    End Sub

    Private Sub ShowBigTvEpisodeText()

        Dim frm As New frmBigTvEpisodeText
        If Preferences.MultiMonitoEnabled Then
            frm.Bounds = screen.AllScreens(CurrentScreen).Bounds
            frm.StartPosition = FormStartPosition.Manual
        End If
        frm.ShowDialog(
                        tb_Sh_Ep_Title.Text,
                        tb_EpDirector.Text,
                        tb_EpAired.Text,
                        tb_EpRating.Text,
                        tb_ShRunTime.Text,
                        tb_ShGenre.Text,
                        tb_ShCert.Text,
                        tb_EpPlot.Text
                        )
    End Sub

    Private Sub tb_Sh_Ep_Title_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_Sh_Ep_Title.Enter
        'Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        'Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
        'If Panel9.Visible = False Then
        '    tb_Sh_Ep_Title.Text = WorkingTvShow.Title.Value
        '    If tb_Sh_Ep_Title.Text.ToLower.IndexOf(", the") = tb_Sh_Ep_Title.Text.Length - 5 Then
        '        tb_Sh_Ep_Title.Text = "The " & tb_Sh_Ep_Title.Text.Substring(0, tb_Sh_Ep_Title.Text.Length - 5)
        '    End If
        'Else
        '    tb_Sh_Ep_Title.Text = WorkingEpisode.Title.Value
        'End If
    End Sub

    Private Sub tb_Sh_Ep_Title_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_Sh_Ep_Title.Leave
        'Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()

        'Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
        'On Error Resume Next
        'If Panel9.Visible = False Then
        '    '-------------- Aqui
        '    'If Preferences.ignorearticle = True Then
        '    '    If TextBox_Title.Text.ToLower.IndexOf("the ") = 0 Then
        '    '        TextBox_Title.Text = TextBox_Title.Text.Substring(4, TextBox_Title.Text.Length - 4) & ", The"
        '    '    End If
        '    'End If
        '    'If Preferences.ignoreAarticle Then
        '    '    If TextBox_Title.Text.ToLower.IndexOf("a ") = 0 Then
        '    '        TextBox_Title.Text = TextBox_Title.Text.Substring(2, TextBox_Title.Text.Length - 2) & ", A"
        '    '    End If
        '    'End If
        '    'If Preferences.ignoreAn Then
        '    '    If TextBox_Title.Text.ToLower.IndexOf("an ") = 0 Then
        '    '        TextBox_Title.Text = TextBox_Title.Text.Substring(3, TextBox_Title.Text.Length - 3) & ", An"
        '    '    End If
        '    'End If
        '    WorkingTvShow.Title.Value = Preferences.RemoveIgnoredArticles(tb_Sh_Ep_Title.Text)
        'Else
        '    WorkingEpisode.Title.Value = tb_Sh_Ep_Title.Text
        '    Dim trueseason As String = WorkingEpisode.Season.Value
        '    Dim trueepisode As String = WorkingEpisode.Episode.Value
        '    If trueseason.Length = 1 Then trueseason = "0" & trueseason
        '    If trueepisode.Length = 1 Then trueepisode = "0" & trueepisode
        '    tb_Sh_Ep_Title.Text = "S" & trueseason & "E" & trueepisode & " - " & WorkingEpisode.Title.Value
        'End If
    End Sub

    Private Sub Button_Save_TvShow_Episode_From_Form(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save_TvShow_Episode.Click  'save button
        Try
            Dim Show As Media_Companion.TvShow = Nothing
            Dim Season As Media_Companion.TvSeason = Nothing
            Dim Episode As Media_Companion.TvEpisode = Nothing
            If TvTreeview.SelectedNode IsNot Nothing Then
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
                    Show = TvTreeview.SelectedNode.Tag
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    Episode = TvTreeview.SelectedNode.Tag
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                    Exit Sub
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            Dim tempint As Integer = 0
            Dim tempstring As String = ""
            If Show IsNot Nothing Then
                Dim changed As Integer = 0
                If Utilities.ReplaceNothing(Show.TvdbId.Value) <> tb_ShTvdbId.Text Then
                    changed += 1
                End If
                If Utilities.ReplaceNothing(Show.ImdbId.Value).ToLower <> tb_ShImdbId.Text.ToLower Then
                    changed += 2
                End If
                If changed > 0 Then
                    If changed = 1 Then
                        tempint = MessageBox.Show("It appears that you have changed the TVDB ID" & vbCrLf & "Media Companion depends on this ID for scraping episodes and art" & vbCrLf & vbCrLf & "Are you sure you wish to continue and save this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        If tempint = DialogResult.No Then
                            Exit Sub
                        End If
                    ElseIf changed = 2 Then
                        tempint = MessageBox.Show("It appears that you have changed the IMDB ID" & vbCrLf & "Media Companion depends on this ID for scraping actors from IMDB" & vbCrLf & vbCrLf & "Are you sure you wish to continue and save this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        If tempint = DialogResult.No Then
                            Exit Sub
                        End If
                    ElseIf changed = 3 Then
                        tempint = MessageBox.Show("It appears that you have changed the IMDB ID & TVDB ID" & vbCrLf & "Media Companion depends on these IDs being correct for a number of scraping operations" & vbCrLf & vbCrLf & "Are you sure you wish to continue and save this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        If tempint = DialogResult.No Then
                            Exit Sub
                        End If
                    End If
                End If
                'its a tvshow
                Dim TmpTitle As String = ""
                If tb_Sh_Ep_Title.Text.ToLower.IndexOf(", the") = tb_Sh_Ep_Title.Text.Length - 5 And tb_Sh_Ep_Title.Text.Length > 5 Then
                    TmpTitle = "The " & tb_Sh_Ep_Title.Text.Substring(0, tb_Sh_Ep_Title.Text.Length - 5)
                Else
                    TmpTitle = tb_Sh_Ep_Title.Text
                End If

                If TmpTitle <> Show.Title.Value Then
                    Dim TryTitle As MsgBoxResult = MsgBox(" You have changed this Show's Title " & vbCrLf & "Are you sure you want to accept this change", MsgBoxStyle.YesNo)
                    If TryTitle = MsgBoxResult.No Then
                        tb_Sh_Ep_Title.Text = Preferences.RemoveIgnoredArticles(Show.Title.Value)
                        Exit Sub
                    End If
                    Show.Title.Value = TmpTitle
                End If
                Show.Plot.Value = tb_ShPlot.Text
                Show.Runtime.Value = tb_ShRunTime.Text
                Show.Premiered.Value = tb_ShPremiered.Text
                Show.Studio.Value = tb_ShStudio.Text
                Show.Rating.Value = tb_ShRating.Text
                Show.ImdbId.Value = tb_ShImdbId.Text
                Show.TvdbId.Value = tb_ShTvdbId.Text
                Show.Mpaa.Value = tb_ShCert.Text
                Show.Genre.Value = tb_ShGenre.Text
                Show.SortTitle.Value = If(TextBox_Sorttitle.Text <> Show.Title.Value, TextBox_Sorttitle.Text, "")

                nfoFunction.tvshow_NfoSave(Show, True)   'Show.Save()
                Show.UpdateTreenode()
            Else
                Dim trueseason As String = Utilities.PadNumber(Episode.Season.Value, 2)
                Do While trueseason.Substring(0, 1) = "0" AndAlso trueseason.Length <> 1
                    trueseason = trueseason.Substring(1, trueseason.Length - 1)
                Loop
                Dim trueepisode As String = Utilities.PadNumber(Episode.Episode.Value, 2)
                Do While trueepisode.Substring(0, 1) = "0" AndAlso trueepisode.Length <> 1
                    trueepisode = trueepisode.Substring(1, trueepisode.Length - 1)
                Loop
                Dim episodelist As New List(Of TvEpisode)
                episodelist = WorkingWithNfoFiles.ep_NfoLoad(Episode.NfoFilePath)
                For Each ep In episodelist
                    If ep.Season.Value = trueseason And ep.Episode.Value = trueepisode Then
                        ep.Plot.Value = tb_EpPlot.Text
                        ep.Aired.Value = tb_EpAired.Text
                        ep.Rating.Value = tb_EpRating.Text
                        ep.Credits.Value = tb_EpCredits.Text
                        ep.Director.Value = tb_EpDirector.Text
                        ep.Source.Value = If(cbTvSource.SelectedIndex = 0, "", cbTvSource.Items(cbTvSource.SelectedIndex))
                        'ep.UpdateTreenode()
                    End If
                Next
                WorkingWithNfoFiles.ep_NfoSave(episodelist, Episode.NfoFilePath)
                Episode.UpdateTreenode()
            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button47.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim TVShowNFOContent As String = ""
            If Button47.Text = "Default" Then
                WorkingTvShow.SortOrder.Value = "dvd"
                Button47.Text = "DVD"
            Else
                WorkingTvShow.SortOrder.Value = "Default"
                Button47.Text = "Default"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button45.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()

            Dim TVShowNFOContent As String = ""
            If Button45.Text = "TVDB" Then
                If WorkingTvShow.ImdbId.Value <> "" Then
                    WorkingTvShow.TvShowActorSource.Value = "imdb"
                    Button45.Text = "IMDB"
                Else
                    MsgBox("No IMDB ID allocated to this Show!")
                End If
            Else
                WorkingTvShow.TvShowActorSource.Value = "tvdb"
                Button45.Text = "TVDB"
            End If
            WorkingTvShow.Save()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button46.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim TVShowNFOContent As String = ""
            If Button46.Text = "TVDB" Then
                WorkingTvShow.EpisodeActorSource.Value = "imdb"
                Button46.Text = "IMDB"
            Else
                WorkingTvShow.EpisodeActorSource.Value = "tvdb"
                Button46.Text = "TVDB"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvActor_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTvActor.MouseHover
        Try
            cbTvActor.Focus()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub cbTvActor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTvActor.SelectedIndexChanged
        Try
            If Not actorflag Then
                actorflag = True
                cbTvActorRole.SelectedIndex = cbTvActor.SelectedIndex
                Call tv_ActorDisplay()
                'cbTvActor.Focus()
            Else
                actorflag = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvActorRole_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTvActorRole.MouseHover
        Try
            cbTvActorRole.Focus()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvActorRole_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTvActorRole.SelectedIndexChanged
        Try
            If Not actorflag Then
                actorflag = True
                cbTvActor.SelectedIndex = cbTvActorRole.SelectedIndex
                Call tv_ActorRoleDisplay()
                'cbTvActorRole.Focus()
            Else
                actorflag = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PictureBox6_DoubleClick(sender As System.Object, e As System.EventArgs) Handles PictureBox6.DoubleClick
        ZoomActorPictureBox(PictureBox6)
    End Sub

    Private Sub Button_TV_State_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TV_State.Click
        Try
            Dim Btn As Button = sender
            If TypeOf Btn.Tag Is Media_Companion.TvShow Then
                Dim TempShow As Media_Companion.TvShow = Btn.Tag
                Select Case TempShow.State
                    Case Media_Companion.ShowState.Locked
                        TempShow.State = Media_Companion.ShowState.Open
                    Case Media_Companion.ShowState.Open
                        TempShow.State = Media_Companion.ShowState.Locked
                    Case Media_Companion.ShowState.Error
                    Case Media_Companion.ShowState.Unverified
                        TempShow.State = Media_Companion.ShowState.Open
                        tb_Sh_Ep_Title.BackColor = Color.White
                End Select
                If TempShow.State = Media_Companion.ShowState.Locked Then
                    Button_TV_State.Text = "Locked"
                    Button_TV_State.BackColor = Color.Red
                ElseIf TempShow.State = Media_Companion.ShowState.Open Then
                    Button_TV_State.Text = "Open"
                    Button_TV_State.BackColor = Color.LawnGreen
                ElseIf TempShow.State = Media_Companion.ShowState.Unverified Then
                    Button_TV_State.Text = "Un-Verified"
                    Button_TV_State.BackColor = Color.Yellow
                Else
                    Button_TV_State.Text = "Error"
                    Button_TV_State.BackColor = Color.Gray
                End If
                TempShow.UpdateTreenode()   'update the treenode so we can see the state change
                nfoFunction.tvshow_NfoSave(TempShow, True)
                'TempShow.Save()             'save the nfo immediately (you don't have to press save button)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvSearchNew_Click(sender As System.Object, e As System.EventArgs) Handles btnTvSearchNew.Click
        Try
            Call ep_Search()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnTvRefreshAll_Click(sender As System.Object, e As System.EventArgs) Handles btnTvRefreshAll.Click
        Try
            Call tv_CacheRefresh()
        Catch ex As Exception

        End Try
    End Sub

#Region "Tv PictureBoxes"
    Private Sub ReScrFanartToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReScrFanartToolStripMenuItem.Click
        Try
            Dim Showname As TvShow = tv_ShowSelectedCurrently()
            'Me.tvBatchList.shFanart = True
            TvGetArtwork(Showname, True, False, False, False)
            tv_ShowLoad(Showname)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub SelNewFanartToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelNewFanartToolStripMenuItem.Click
        Try
            Me.TabPage12.Select()
            TabControl3.SelectedIndex = 1

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RescrapeTvEpThumbToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RescrapeTvEpThumbToolStripMenuItem.Click
        Try
            TvEpThumbRescrape()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RescrapeTvEpScreenShotToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RescrapeTvEpScreenShotToolStripMenuItem.Click
        Try
            TvEpThumbScreenShot()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tv_PictureBoxLeft_Click(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_PictureBoxLeft.MouseUp, tv_PictureBoxLeft.Click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                ReScrFanartToolStripMenuItem.Visible = False
                SelNewFanartToolStripMenuItem.Visible = False
                RescrapeTvEpThumbToolStripMenuItem.Visible = False
                RescrapeTvEpScreenShotToolStripMenuItem.Visible = False
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Or TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                    ReScrFanartToolStripMenuItem.Visible = True
                    SelNewFanartToolStripMenuItem.Visible = True
                ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    RescrapeTvEpThumbToolStripMenuItem.Visible = True
                    RescrapeTvEpScreenShotToolStripMenuItem.Visible = True
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub tv_PictureBoxRight_Click(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_PictureBoxRight.MouseUp, tv_PictureBoxRight.Click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                tsm_TvScrapeBanner.Visible = False
                tsm_TvScrapePoster.Visible = False
                tsm_TvSelectBanner.Visible = False
                tsm_TvSelectPoster.Visible = False
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Or TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Or TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    tsm_TvScrapePoster.Visible = True
                    tsm_TvSelectPoster.Visible = True
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub tv_PictureBoxBottom_Click(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_PictureBoxBottom.MouseUp, tv_PictureBoxBottom.Click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                tsm_TvScrapeBanner.Visible = False
                tsm_TvScrapePoster.Visible = False
                tsm_TvSelectBanner.Visible = False
                tsm_TvSelectPoster.Visible = False
                If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Or TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Or TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                    tsm_TvScrapeBanner.Visible = True
                    tsm_TvSelectBanner.Visible = True
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _tv_SplitContainer_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles _tv_SplitContainer.SplitterMoved
        If Not MainFormLoadedStatus Then Exit Sub
        Preferences.tvbannersplit = Math.Round(_tv_SplitContainer.SplitterDistance / _tv_SplitContainer.Height, 2)
    End Sub
    
#End Region 'Tv PictureBoxes    

#End Region  'Tv Browser Form functions

#Region "Tv Screenshot Form"

    Private Sub TextBox35_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox35.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            If TextBox35.Text <> "" AndAlso Convert.ToInt32(TextBox35.Text) > 0 Then
                TvEpThumbScreenShot()
            End If
        End If
        If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tv_EpThumbScreenShot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tv_EpThumbScreenShot.Click
        Try
            TvEpThumbScreenShot()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub TextBox35_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox35.Leave
        If TextBox35.Text = "" Then
            MsgBox("Please enter a numerical value >0 into the textbox")
            TextBox35.Focus()
        ElseIf Convert.ToInt32(TextBox35.Text) = 0 Then
            MsgBox("Please enter a numerical value >0 into the textbox")
            TextBox35.Focus()
        End If
    End Sub

    Private Sub tv_EpThumbRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tv_EpThumbRescrape.Click
        Try
            TvEpThumbRescrape()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#End Region

#Region "Tv Fanart Form"

    Private Sub btnTvFanartSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvFanartSave.Click
        Dim issavefanart As Boolean = Preferences.savefanart
        Preferences.savefanart =true
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            lbl_movVotes.Text = "Please Wait, Trying to Download Fanart"
            Me.Refresh()
            Application.DoEvents()
            Dim miscvar As String = String.Empty
            Dim miscint As Integer = 0
            Dim miscvar2 As String = String.Empty
            Dim allok As Boolean = False
            For Each button As Control In Me.Panel13.Controls
                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        miscvar = b1.Name
                        miscvar = miscvar.Replace("checkbox", "")
                        miscint = Convert.ToDecimal(miscvar)
                        miscvar2 = listOfTvFanarts(miscint).bigUrl
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If allok = False Then
                MsgBox("No Fanart Is Selected")
            Else
                Try
                    Dim savepath As String = WorkingTvShow.NfoFilePath.ToLower.Replace("tvshow.nfo", "fanart.jpg")

                    If Movie.SaveFanartImageToCacheAndPath(miscvar2, savepath) Then
                        Try
                            util_ImageLoad(PictureBox10, savepath, Utilities.DefaultTvFanartPath)
                            If TvTreeview.SelectedNode.Name.ToLower.IndexOf("tvshow.nfo") <> -1 Or TvTreeview.SelectedNode.Name = "" Then
                                util_ImageLoad(tv_PictureBoxLeft, savepath, Utilities.DefaultTvFanartPath)
                            End If
                        Catch ex As Exception
#If SilentErrorScream Then
                            Throw ex
#End If
                        End Try
                    Else
                        PictureBox10.Image = Nothing
                    End If
                    If Preferences.FrodoEnabled Then 
                        Utilities.SafeCopyFile(savepath,savepath.Replace("fanart.jpg","season-all-fanart.jpg"),True)
                    End If
                Catch ex As WebException
                    MsgBox(ex.Message)
                End Try
            End If
            Preferences.savefanart = issavefanart
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
            Preferences.savefanart=issavefanart
        End Try
    End Sub

    Private Sub rbTvFanart_CheckedChanged(sender As System.Object, e As System.EventArgs) 
        Tv_FanartDisplay()
    End Sub

    Private Sub Button35_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button35.MouseDown
        Try
            'up
            If PictureBox10.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnTvFanartResetImage.Visible = True
            btnTvFanartSaveCropped.Visible = True
            cropString = "top"
            Timer4.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button36_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button36.MouseDown
        Try
            'down
            If PictureBox10.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnTvFanartResetImage.Visible = True
            btnTvFanartSaveCropped.Visible = True
            cropString = "bottom"
            Timer4.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button38_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button38.MouseDown
        Try
            If PictureBox10.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnTvFanartResetImage.Visible = True
            btnTvFanartSaveCropped.Visible = True
            cropString = "left"
            Timer4.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try 'left
    End Sub

    Private Sub Button37_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button37.MouseDown
        Try
            'right
            If PictureBox10.Image Is Nothing Then Exit Sub
            thumbedItsMade = True
            btnTvFanartResetImage.Visible = True
            btnTvFanartSaveCropped.Visible = True
            cropString = "right"
            Timer4.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Timer4_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Try
            If cropString = "top" Then Call tv_FanartCropTop()
            If cropString = "bottom" Then Call tv_FanartCropBottom()
            If cropString = "left" Then Call tv_FanartCropLeft()
            If cropString = "right" Then Call tv_FanartCropRight()
            Label58.Text = PictureBox10.Image.Height.ToString
            Label59.Text = PictureBox10.Image.Width.ToString
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button35_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button35.MouseUp
        Try
            Timer4.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button36_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button36.MouseUp
        Try
            Timer4.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button38_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button38.MouseUp
        Try
            Timer4.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button37_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button37.MouseUp
        Try
            Timer4.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvFanartResetImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvFanartResetImage.Click
        Try
            Dim workingtvshow As TvShow = tv_ShowSelectedCurrently()
            util_ImageLoad(PictureBox10, workingtvshow.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)
            Label58.Text = PictureBox10.Image.Height.ToString
            Label59.Text = PictureBox10.Image.Width.ToString
            btnTvFanartResetImage.Visible = False
            btnTvFanartSaveCropped.Visible = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvFanartSaveCropped_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvFanartSaveCropped.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Try
                Dim stream As New System.IO.MemoryStream
                PictureBox10.Image.Save(WorkingTvShow.FolderPath & "fanart.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                If TvTreeview.SelectedNode.Name.ToLower.IndexOf("tvshow.nfo") <> -1 Or TvTreeview.SelectedNode.Name = "" Then
                    util_ImageLoad(tv_PictureBoxLeft, WorkingTvShow.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)
                End If
                Label58.Text = PictureBox10.Image.Height.ToString
                Label59.Text = PictureBox10.Image.Width.ToString
                btnTvFanartResetImage.Visible = False
                btnTvFanartSaveCropped.Visible = False
            Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvFanartUrl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvFanartUrl.Click
        Try
            Dim t As New frmImageBrowseOrUrl
            t.Location = Me.PointToScreen(New Point(btnTvFanartUrl.Left - 480, btnTvFanartUrl.Top + btnTvFanartUrl.Height))
            t.ShowDialog()
            If t.DialogResult = Windows.Forms.DialogResult.Cancel Or t.tb_PathorUrl.Text = "" Then
                t.Dispose()
                Exit Sub
            End If
            Dim PathOrUrl As String = t.tb_PathorUrl.Text
            t.Dispose()
            t = Nothing
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim savepath As String = WorkingTvShow.NfoFilePath.ToLower.Replace("tvshow.nfo", "fanart.jpg")
            Dim eh As Boolean = Preferences.savefanart
            Preferences.savefanart = True
            Movie.SaveFanartImageToCacheAndPath(PathOrUrl, savepath)
            Preferences.savefanart = eh
            Dim exists As Boolean = System.IO.File.Exists(savepath)
            If exists = True Then

                util_ImageLoad(PictureBox10, savepath, Utilities.DefaultTvFanartPath)

                If TvTreeview.SelectedNode.Name.ToLower.IndexOf("tvshow.nfo") <> -1 Or TvTreeview.SelectedNode.Name = "" Then
                    util_ImageLoad(tv_PictureBoxLeft, savepath, Utilities.DefaultTvFanartPath)

                End If

            End If
            Label59.Text = PictureBox10.Image.Width
            Label58.Text = PictureBox10.Image.Height


        Catch ex As Exception
            MsgBox("Unable To Download Image")
        End Try

    End Sub

#End Region

#Region "Tv Poster Form"

    Private Sub btnTvPosterSaveBig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterSaveBig.Click
        Call TvPosterSave(btnTvPosterSaveBig.Tag)
    End Sub

    Private Sub btnTvPosterTVDBAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterTVDBAll.Click
        Try
            'tvdb all
            tvdbmode = True
            usedlist.Clear()
            btnTvPosterSaveBig.Visible = False
            If tvdbposterlist.Count = 0 Then
                Call tv_TvdbThumbsGet()
            End If
            For Each poster In tvdbposterlist
                If rbTVbanner.Enabled = False Then
                    If poster.BannerType <> "fanart" And poster.BannerType <> "series" Then
                        usedlist.Add(poster)
                    End If
                Else
                    If rbTVposter.Checked = False And poster.BannerType = "series" Then
                        usedlist.Add(poster)
                    ElseIf rbTVposter.Checked = True And poster.BannerType <> "fanart" Then
                        If poster.BannerType <> "series" Then usedlist.Add(poster)
                    End If

                End If
            Next
            Call tv_PosterPanelPopulate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvPosterTVDBSpecific_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterTVDBSpecific.Click
        Try
            'tvdb specific
            tvdbmode = True
            usedlist.Clear()
            btnTvPosterSaveBig.Visible = False
            If tvdbposterlist.Count = 0 Then
                Call tv_TvdbThumbsGet()
            End If

            Dim tempseason As String = ""
            If ComboBox2.SelectedItem.indexof("Season ") <> -1 Then
                tempseason = ComboBox2.SelectedItem.replace("Season ", "")
            End If
            If tempseason.IndexOf("0") = 0 And tempseason.Length > 1 Then
                tempseason = tempseason.Substring(1, tempseason.Length - 1)
            End If
            If ComboBox2.SelectedItem.indexof("Specials") <> -1 Then
                tempseason = "0"
            End If
            If ComboBox2.SelectedItem.indexof("Main Image") <> -1 And rbTVposter.Checked = True Then
                tempseason = "poster"
            ElseIf ComboBox2.SelectedItem.indexof("Main Image") <> -1 And rbTVposter.Checked = False Then
                tempseason = "series"
            End If
            If tempseason = "poster" Or tempseason = "series" Then
                For Each poster In tvdbposterlist
                    If poster.BannerType = "poster" And rbTVposter.Checked = True Then
                        If poster.BannerType <> "fanart" Then usedlist.Add(poster)
                    End If
                    If poster.BannerType = "series" And rbTVposter.Checked = False Then
                        If poster.BannerType <> "fanart" Then usedlist.Add(poster)
                    End If
                Next
            Else
                For Each poster In tvdbposterlist
                    If poster.Season = tempseason Then
                        If rbTVbanner.Checked = True Then
                            If poster.Resolution = "seasonwide" And poster.BannerType <> "fanart" And poster.BannerType <> "poster" Then
                                usedlist.Add(poster)
                            End If
                        End If
                        If rbTVposter.Checked = True Then
                            If poster.Resolution <> "seasonwide" And poster.BannerType <> "fanart" And poster.BannerType <> "banner" Then
                                usedlist.Add(poster)
                            End If
                        End If
                    End If
                Next
            End If
            Call tv_PosterPanelPopulate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvPosterNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterNext.Click  'TV Poster Page Next
        Try
            tvposterpage += 1
            btnTvPosterSaveBig.Visible = False
            If usedlist.Count < 10 * tvposterpage Then
                btnTvPosterNext.Enabled = False
            End If
            Call tv_PosterSelectionDisplay()
            btnTvPosterPrev.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvPosterPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterPrev.Click  'TV Poster Page Prev
        Try
            tvposterpage -= 1
            btnTvPosterSaveBig.Visible = False
            If tvposterpage = 1 Then
                btnTvPosterPrev.Enabled = False
            End If
            Call tv_PosterSelectionDisplay()
            btnTvPosterNext.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvPosterIMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterIMDB.Click
        Try
            btnTvPosterSaveBig.Visible = False
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            If WorkingTvShow.ImdbId = Nothing Then
                MsgBox("No IMDB ID is available for this movie, cant scrape posters")
                Exit Sub
            End If
            If WorkingTvShow.ImdbId.Value = "" Then
                MsgBox("No IMDB ID is available for this movie, cant scrape posters")
                Exit Sub
            End If
            Dim ok As Boolean = False
            If WorkingTvShow.ImdbId.Value.ToLower.IndexOf("tt") = 0 Then
                Dim tempstring As String = WorkingTvShow.ImdbId.Value.ToLower.Substring(2, WorkingTvShow.ImdbId.Value.Length - 2)
                If IsNumeric(tempstring) Then
                    ok = True
                End If
            End If
            If IsNumeric(WorkingTvShow.ImdbId) And WorkingTvShow.ImdbId.Value.Length = 7 Then
                WorkingTvShow.ImdbId.Value = "tt" & WorkingTvShow.ImdbId.Value
                ok = True
            End If
            If ok = False Then
                MsgBox("IMDB ID seems to be an invalid format, can't scrape posters")
                Exit Sub
            End If
            tvdbmode = False
            usedlist.Clear()
            If imdbposterlist.Count <= 0 Then
                Dim newobject2 As New imdb_thumbs.Class1
                Dim posters(,) As String = newobject2.getimdbposters(WorkingTvShow.ImdbId.Value)
                For f = 0 To UBound(posters)
                    If posters(f, 0) <> Nothing Then
                        Dim individualposter As New TvBanners
                        individualposter.SmallUrl = posters(f, 0)
                        individualposter.Url = posters(f, 0)
                        imdbposterlist.Add(individualposter)
                    End If
                Next
            End If
            For Each po In imdbposterlist
                usedlist.Add(po)
            Next
            usedlist.Reverse()
            Call tv_PosterPanelPopulate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvPosterUrlBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvPosterUrlBrowse.Click
        Try
            Dim t As New frmImageBrowseOrUrl
            t.Location = Me.PointToScreen(New Point(btnTvPosterUrlBrowse.Left - 480, GroupBox23.Top + GroupBox23.Height))
            t.ShowDialog()
            If t.DialogResult = Windows.Forms.DialogResult.Cancel Or t.tb_PathorUrl.Text = "" Then
                t.Dispose()
                Exit Sub
            End If
            Dim PathOrUrl As String = t.tb_PathorUrl.Text
            t.Dispose()
            t = Nothing
            Try
                Call TvPosterSave(PathOrUrl)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        btnTvPosterSaveBig.Visible = False
        BannerAndPosterViewer()
    End Sub

    Private Sub rbTVbanner_CheckedChanged(sender As Object, e As EventArgs) Handles rbTVbanner.CheckedChanged
        btnTvPosterSaveBig.Visible = False
        BannerAndPosterViewer()
    End Sub

#End Region

#Region "Tv Show Selector Form"

    Private Sub TextBox26_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox26.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call tv_ShowListLoad()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Try
            Call tv_ShowListLoad()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        util_ImageLoad(PictureBox9, listOfShows(ListBox3.SelectedIndex).showbanner, Utilities.DefaultBannerPath)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            Call util_LanguageCheck()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton8.CheckedChanged
        Try
            If RadioButton8.Checked = True Then
                Preferences.postertype = "banner"
            Else
                Preferences.postertype = "poster"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton9.CheckedChanged
        Try
            If RadioButton9.Checked = True Then
                Preferences.postertype = "poster"
            Else
                Preferences.postertype = "banner"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton16.CheckedChanged
        Try
            If RadioButton16.Checked = True Then
                Preferences.seasonall = "wide"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton17.CheckedChanged
        Try
            If RadioButton17.Checked = True Then
                Preferences.seasonall = "poster"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton18.CheckedChanged
        Try
            If RadioButton18.Checked = True Then
                Preferences.seasonall = "none"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvChgShowDLSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvChgShowDLSeason.CheckedChanged
        Try
            Preferences.TvChgShowDlSeasonthumbs = cbTvChgShowDLSeason.Checked 
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvChgShowOverwriteImgs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvChgShowOverwriteImgs.CheckedChanged
        Try
            Preferences.TvChgShowOverwriteImgs = cbTvChgShowOverwriteImgs.Checked 
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvChgShowDLFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvChgShowDLFanart.CheckedChanged
        Try
            Preferences.TvChgShowDlFanart = cbTvChgShowDLFanart.Checked 
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvChgShowDLFanartTvArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvChgShowDLFanartTvArt.CheckedChanged
        Try
            Preferences.TvChgShowDlFanartTvArt = cbTvChgShowDLFanartTvArt.Checked 
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbTvChgShowDLPoster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTvChgShowDLPoster.CheckedChanged
        Try
            Preferences.TvChgShowDlPoster = cbTvChgShowDLPoster.Checked 
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnTvShowSelectorScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvShowSelectorScrape.Click
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            If listOfShows.Count = 1 And listOfShows(0).showtitle = "TVDB Search Returned Zero Results" Then
                MsgBox("No show is selected")
                Exit Sub
            End If
            Dim tempstring As String = ""
            If Label55.Text.IndexOf("is not available in") <> -1 Then
                MsgBox("Please select a language that is available for this show")
                Exit Sub
            End If
            Dim messbox As frmMessageBox = New frmMessageBox("The Selected TV Show is being Scraped", "", "Please Wait")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()

            Dim LanCode As String
            If ListBox1.SelectedIndex = -1 Then
                LanCode = Preferences.TvdbLanguageCode
            Else
                LanCode = languageList(ListBox1.SelectedIndex).Abbreviation.Value
            End If
            If Preferences.tvshow_useXBMC_Scraper = True Then

                Dim TVShowNFOContent As String = XBMCScrape_TVShow_General_Info("metadata.tvdb.com", listOfShows(ListBox3.SelectedIndex).showid, LanCode, WorkingTvShow.NfoFilePath)
                If TVShowNFOContent <> "error" Then CreateMovieNfo(WorkingTvShow.NfoFilePath, TVShowNFOContent)
                Call tv_ShowLoad(WorkingTvShow)
                TvTreeview.Refresh()
                messbox.Close()
                TabControl3.SelectedIndex = 0
            Else
                If Preferences.TvChgShowOverwriteImgs Then TvDeleteShowArt(WorkingTvShow)
                Cache.TvCache.Remove(WorkingTvShow)
                newTvFolders.Add(WorkingTvShow.FolderPath.Substring(0, WorkingTvShow.FolderPath.LastIndexOf("\")))
                Dim args As TvdbArgs = New TvdbArgs(listOfShows(ListBox3.SelectedIndex).showid, LanCode)
                bckgrnd_tvshowscraper.RunWorkerAsync(args)
                While bckgrnd_tvshowscraper.IsBusy
                    Application.DoEvents()
                End While
                TabControl3.SelectedIndex = 0
                messbox.Close()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        Finally
            Preferences.TvChgShowDlFanart = False
            Preferences.TvChgShowDlFanartTvArt = False
            Preferences.TvChgShowDlPoster = False
            Preferences.TvChgShowDlSeasonthumbs = False
            Preferences.TvChgShowOverwriteImgs = False
        End Try
    End Sub

#End Region

#Region "TV TVDB/IMDB Form"

    Private Sub btn_TvTVDb_Click(sender As System.Object, e As System.EventArgs) Handles btn_TvTVDb.Click
        Dim url As String
        Dim Show As Media_Companion.TvShow = tv_ShowSelectedCurrently()
        If Show.TvdbId.Value.Contains("tt") Then
            MsgBox("Invalid Tvdb ID" & vbCrLf & "Unable to load Show's TVDB page")
            Exit Sub
        End If
        If String.IsNullOrEmpty(Show.TvdbId.Value) Then
            MsgBox("Selected Show has no Tvdb ID" & vbCrLf & "Unable to load Show's TVDB page")
            Exit Sub
        End If
        Dim TvdbId As Integer = Show.TvdbId.Value
        url = "http://thetvdb.com/?tab=series&id=" & TvdbId & "&lid=7"
        Try
            WebBrowser4.Stop()
            WebBrowser4.ScriptErrorsSuppressed = True
            WebBrowser4.Navigate(url)
        Catch
            WebBrowser4.Stop()
            WebBrowser4.ScriptErrorsSuppressed = True
            WebBrowser4.Navigate(url)

        End Try
    End Sub

    Private Sub btn_TvIMDB_Click(sender As System.Object, e As System.EventArgs) Handles btn_TvIMDB.Click
        Dim url As String
        Dim Show As Media_Companion.TvShow = tv_ShowSelectedCurrently()
        If String.IsNullOrEmpty(Show.ImdbId.Value) Then
            MsgBox("Selected Show has no IMDB ID" & vbCrLf & "Unable to load Show's IMDB page")
            Exit Sub
        End If
        url = "http://www.imdb.com/title/" & Show.ImdbId.Value & "/"
        Try
            WebBrowser4.Stop()
            WebBrowser4.ScriptErrorsSuppressed = True
            WebBrowser4.Navigate(url)
        Catch
            WebBrowser4.Stop()
            WebBrowser4.ScriptErrorsSuppressed = True
            WebBrowser4.Navigate(url)

        End Try
    End Sub

#End Region

#Region "Tv Folders Form"

    'Private Sub TabPage23_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage23.Enter
    '    tvfolderschanged = False
    'End Sub

    Private Sub TabPage23_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage23.Leave
        Try
            If tvfolderschanged Then
                Dim save = MsgBox("You have made changes to some folders" & vbCrLf & "    Do you wish to save these changes?", MsgBoxStyle.YesNo)
                If save = DialogResult.Yes Then
                    btn_TvFoldersSave.PerformClick()
                Else
                    btn_TvFoldersUndo.PerformClick()
                End If
                tvfolderschanged = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_TvFoldersRootAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersRootAdd.Click
        Try
            If TextBox39.Text = Nothing Then
                Exit Sub
            End If
            If TextBox39.Text = "" Then
                Exit Sub
            End If
            Dim tempstring As String = TextBox39.Text
            Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Dim exists As Boolean = False
            For Each item In ListBox5.Items
                If item.ToString.ToLower = tempstring.ToLower Then
                    exists = True
                    Exit For
                End If
            Next
            If exists = True Then
                MsgBox("        Folder Already Exists")
            Else
                Dim f As New IO.DirectoryInfo(tempstring)
                If f.Exists Then
                    ListBox5.Items.Add(tempstring)
                    TextBox39.Text = ""
                    tvfolderschanged = True
                Else
                    Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If tempint = DialogResult.Yes Then
                        ListBox5.Items.Add(tempstring)
                        TextBox39.Text = ""
                        tvfolderschanged = True
                    End If
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_TvFoldersRootBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersRootBrowse.Click
        Try
            'browse for root tv folder
            Dim allok As Boolean = True
            Dim cancelregex As Boolean = False
            Dim newtvshow As Boolean = False
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim strfolder As String
            Dim tempstring3 As String
            Dim tempint As Integer = 0
            Dim tempint2 As Integer = 0
            theFolderBrowser.Description = "Please Select Root Folder of the TV Shows You Wish To Add to DB"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                strfolder = (theFolderBrowser.SelectedPath)
                Preferences.lastpath = strfolder
                Dim hasseason As Boolean = False
                If Not ListBox5.Items.Contains(strfolder) Then
                    For Each strfolder2 As String In My.Computer.FileSystem.GetDirectories(strfolder)
                        Dim M As Match
                        tempstring3 = strfolder2.ToLower.Replace(strfolder.ToLower,"")
                        M = Regex.Match(tempstring3, "(series ?\d+|season ?\d+|s ?\d+|^\d{1,3}$)")
                        If M.Success = True Then
                            hasseason = True
                            Exit For
                        End If
                    Next
                    If hasseason = True Then
                        tempint = MessageBox.Show(strfolder & " Appears to Contain Season Folders." & vbCrLf & "Are you sure this folder contains multiple" & vbCrLf & "TV Shows, each in its own folder?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If tempint = DialogResult.Yes Then
                            ListBox5.Items.Add(strfolder)
                            tvfolderschanged = True
                        ElseIf tempint = DialogResult.No Then
                            tempint2 = MessageBox.Show("Do you wish to add this as a single TV Show Folder?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If tempint2 = DialogResult.Yes Then
                                If Not ListBox6.Items.Contains(strfolder) Then
                                    ListBox6.Items.Add(strfolder)
                                    tvfolderschanged = True
                                Else
                                    MsgBox("Folder not added, Already exists")
                                End If
                            End If
                        End If
                    Else
                        ListBox5.Items.Add(strfolder)
                        tvfolderschanged = True
                    End If
                Else
                    MsgBox("Root already exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_TvFoldersRootRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersRootRemove.Click
        Try
            While ListBox5.SelectedItems.Count > 0
                ListBox5.Items.Remove(ListBox5.SelectedItems(0))
                tvfolderschanged = True
            End While
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub bnt_TvChkFolderList_Click(sender As System.Object, e As System.EventArgs) Handles bnt_TvChkFolderList.Click
        Try
            tvfolderschanged = tv_Showremovedfromlist(, True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_TvFoldersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersAdd.Click
        Try
            If TextBox40.Text = Nothing Then
                Exit Sub
            End If
            If TextBox40.Text = "" Then
                Exit Sub
            End If
            Dim tempstring As String = TextBox40.Text
            Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Dim exists As Boolean = False
            For Each item In ListBox6.Items
                If item.ToString.ToLower = tempstring.ToLower Then
                    exists = True
                    Exit For
                End If
            Next
            If exists = True Then
                MsgBox("        Folder Already Exists")
            Else
                Dim f As New IO.DirectoryInfo(tempstring)
                If f.Exists Then
                    ListBox6.Items.Add(tempstring)
                    tvfolderschanged = True
                    TextBox40.Text = ""
                    newTvFolders.Add(tempstring)
                Else
                    Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If tempint = DialogResult.Yes Then
                        ListBox6.Items.Add(tempstring)
                        tvfolderschanged = True
                        TextBox40.Text = ""
                        newTvFolders.Add(tempstring)
                    End If
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_TvFoldersAddFromRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersAddFromRoot.Click
        Try
            tv_ShowFind(ListBox5.items.Cast(Of String).ToList, False)
            If newTvFolders.Count > 0 Then
                tvfolderschanged = True
                For Each item In newTvFolders
                    ListBox6.Items.Add(item)
                Next
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_TvFoldersBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersBrowse.Click
        Try
            Dim allok As Boolean = True
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select TV Folder to Add to DB"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                For Each item As Object In ListBox6.Items
                    If thefoldernames.ToString = item.ToString Then allok = False
                Next
                Preferences.lastpath = thefoldernames
                If allok = True Then
                    ListBox6.Items.Add(thefoldernames)
                    newTvFolders.Add(thefoldernames)
                    tvfolderschanged = True
                Else
                    MsgBox("        Folder Already Exists", MsgBoxStyle.OkOnly)
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_TvFoldersRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersRemove.Click
        Try
            Dim Folder As String
            Dim cachechanged As Boolean = False
            While ListBox6.SelectedItems.Count > 0
                tvfolderschanged = True
                Folder = ListBox6.SelectedItems(0)

                For Each Item As Media_Companion.TvShow In Cache.TvCache.Shows
                    If Item.FolderPath.Trim("\") = Folder.Trim("\") Then
                        TvTreeview.Nodes.Remove(Item.ShowNode)
                        For Each ep As TvEpisode In Item.Episodes
                            Cache.TvCache.Remove(ep)
                        Next
                        Cache.TvCache.Remove(Item)
                        cachechanged = True
                        Exit For
                    End If
                Next
                ListBox6.Items.Remove(ListBox6.SelectedItems(0))
            End While
            If cachechanged Then Tv_CacheSave()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_TvFoldersUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersUndo.Click
        Try
            newTvFolders.Clear()
            'Call setuptvfolders()
            tvfolderschanged = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_TvFoldersSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvFoldersSave.Click
        Try
            Preferences.tvRootFolders.Clear()
            For Each item In ListBox5.Items
                Preferences.tvRootFolders.Add(item)
            Next
            newTvFolders.Clear()
            Dim tmplist As New List(Of String)
            tmplist.AddRange(Preferences.tvFolders)
            Preferences.tvFolders.Clear()
            For Each item In ListBox6.Items
                Preferences.tvFolders.Add(item)
                If Not tmplist.Contains(item) Then
                    newTvFolders.Add(item)
                End If
            Next
            tvfolderschanged = False
            Preferences.ConfigSave()
            tv_ShowScrape()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox5_KeyPress(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ListBox5.KeyDown
        If e.KeyCode = Keys.Delete AndAlso ListBox5.SelectedItem <> Nothing
            Call btn_TvFoldersRootRemove.PerformClick()
        End If
    End Sub
        
    Private Sub ListBox6_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox6.DragDrop
        Dim files() As String
        files = e.Data.GetData(DataFormats.FileDrop)
        For f = 0 To UBound(files)
            If IO.Directory.Exists(files(f)) Then
                If files(f).ToLower.Contains(".actors") Or files(f).ToLower.Contains("season") Then Continue For
                If Preferences.tvRootFolders.Contains(files(f)) Then Continue For
                Dim di As New IO.DirectoryInfo(files(f))
                If ListBox6.Items.Contains(files(f)) Then Continue For
                Dim skip As Boolean = False
                For Each item In droppedItems
                    If item = files(f) Then
                        skip = True
                        Exit For
                    End If
                Next
                If Not skip Then droppedItems.Add(files(f))
            End If
        Next
        If droppedItems.Count < 1 Then Exit Sub
        For Each item In droppedItems
            ListBox6.Items.Add(item)
            tvfolderschanged = True
        Next
    End Sub

    Private Sub ListBox6_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox6.DragEnter
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox6_KeyPress(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ListBox6.KeyDown
        If e.KeyCode = Keys.Delete AndAlso ListBox6.SelectedItem <> Nothing
            Call btn_TvFoldersRemove.PerformClick()
        End If
    End Sub

#End Region

#Region "Home Movie routines"

    Private Sub SetupHomeMovies()
        If Preferences.homemoviefolders.Count = 0 And homemovielist.Count = 0 And TabControl1.SelectedIndex <> 4 Then
            MsgBox("Please add A Folder containing Home Movies")
            Try
                TabControl1.SelectedIndex = 4
            Catch
            End Try
        Else
            If homemovielist.Count > 0 Then
                Call loadhomemovielist()
            End If
            If homemoviefolders.Count > 0 Then
                ListBox19.Items.Clear()
                For Each folder In homemoviefolders
                    ListBox19.Items.Add(folder)
                Next
            End If
        End If
    End Sub

    Private Sub SearchForNewHomeMoviesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchForNewHomeMoviesToolStripMenuItem.Click
        Call homeMovieScan()
    End Sub

    Private Sub rebuildHomeMovies()
        homemovielist.Clear()
        ListBox18.Items.Clear()
        Dim newhomemoviefolders As New List(Of String)
        Dim progress As Integer = 0
        progress = 0
        scraperLog = ""
        Dim dirpath As String = String.Empty
        Dim newHomeMovieList As New List(Of str_BasicHomeMovie)
        Dim totalfolders As New List(Of String)
        totalfolders.Clear()
        For Each moviefolder In homemoviefolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                scraperLog &= "Searching Movie Folder: " & hg.FullName.ToString & vbCrLf
                totalfolders.Add(moviefolder)
                Dim newlist As List(Of String)
                Try
                    newlist = Utilities.EnumerateFolders(moviefolder)       'Max levels restriction of 6 deep removed
                    For Each subfolder In newlist
                        scraperLog = scraperLog & "Subfolder added :- " & subfolder.ToString & vbCrLf
                        totalfolders.Add(subfolder)
                    Next
                Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                End Try
            End If
        Next
        For Each homemoviefolder In totalfolders
            Dim returnedhomemovielist As New List(Of str_BasicHomeMovie)
            dirpath = homemoviefolder
            Dim dir_info As New System.IO.DirectoryInfo(dirpath)
            returnedhomemovielist = HomeMovies.listHomeMovieFiles(dir_info, "*.nfo", scraperLog)         'titlename is logged in here
            If returnedhomemovielist.Count > 0 Then
                For Each newhomemovie In returnedhomemovielist
                    Dim existsincache As Boolean = False
                    Dim pathOnly As String = IO.Path.GetDirectoryName(newhomemovie.FullPathAndFilename) & "\"
                    Dim nfopath As String = pathOnly & IO.Path.GetFileNameWithoutExtension(newhomemovie.FullPathAndFilename) & ".nfo"
                    If IO.File.Exists(nfopath) Then
                        Try
                            Dim newexistingmovie As New HomeMovieDetails
                            newexistingmovie = nfoFunction.nfoLoadHomeMovie(nfopath)
                            Dim newexistingbasichomemovie As New str_BasicHomeMovie
                            newexistingbasichomemovie.FullPathAndFilename = newexistingmovie.fileinfo.fullpathandfilename
                            newexistingbasichomemovie.Title = newexistingmovie.fullmoviebody.title

                            homemovielist.Add(newexistingbasichomemovie)
                            ListBox18.Items.Add(New ValueDescriptionPair(newexistingbasichomemovie.FullPathAndFilename, newexistingbasichomemovie.Title))
                        Catch ex As Exception
                        End Try
                    Else
                        newHomeMovieList.Add(newhomemovie)
                    End If
                Next
            End If
        Next
        Call HomeMovieCacheSave()
    End Sub

    Private Sub homeMovieScan()
        'Search for new Home Movies
        Dim moviepattern As String
        Dim newhomemoviefolders As New List(Of String)
        Dim progress As Integer = 0
        progress = 0
        scraperLog = ""
        Dim dirpath As String = String.Empty
        scraperLog &= "MC " & Trim(System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(",")(1)) & vbCrLf
        ToolStripProgressBar8.Value = 0
        ToolStripProgressBar8.ProgressBar.Refresh()
        ToolStripStatusLabel9.Text = "Scanning for Home Movies"
        ToolStripProgressBar8.Visible = True
        ToolStripStatusLabel9.Visible = True
        Dim newHomeMovieList As New List(Of str_BasicHomeMovie)

        'For Each folder In homemoviefolders
        Dim totalfolders As New List(Of String)
        totalfolders.Clear()
        For Each moviefolder In homemoviefolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                scraperLog &= "Found Movie Folder: " & hg.FullName.ToString & vbCrLf
                totalfolders.Add(moviefolder)
                Dim newlist As List(Of String)
                Try
                    newlist = Utilities.EnumerateFolders(moviefolder)       'Max levels restriction of 6 deep removed
                    For Each subfolder In newlist
                        scraperLog = scraperLog & "Subfolder added :- " & subfolder.ToString & vbCrLf
                        totalfolders.Add(subfolder)
                    Next
                Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                End Try
            End If
        Next
        For Each homemoviefolder In totalfolders
            For Each ext In Utilities.VideoExtensions
                Dim returnedhomemovielist As New List(Of str_BasicHomeMovie)
                moviepattern = If((ext = "VIDEO_TS.IFO"), ext, "*" & ext)  'this bit adds the * for the extension search in mov_ListFiles2 if its not the string VIDEO_TS.IFO 
                dirpath = homemoviefolder
                Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                returnedhomemovielist = HomeMovies.listHomeMovieFiles(dir_info, moviepattern, scraperLog)         'titlename is logged in here
                If returnedhomemovielist.Count > 0 Then
                    For Each newhomemovie In returnedhomemovielist
                        Dim existsincache As Boolean = False
                        Dim pathOnly As String = IO.Path.GetDirectoryName(newhomemovie.FullPathAndFilename) & "\"
                        Dim nfopath As String = pathOnly & IO.Path.GetFileNameWithoutExtension(newhomemovie.FullPathAndFilename) & ".nfo"
                        If IO.File.Exists(nfopath) Then
                            Try
                                Dim newexistingmovie As New HomeMovieDetails
                                newexistingmovie = nfoFunction.nfoLoadHomeMovie(nfopath)
                                Dim newexistingbasichomemovie As New str_BasicHomeMovie
                                newexistingbasichomemovie.FullPathAndFilename = newexistingmovie.fileinfo.fullpathandfilename
                                newexistingbasichomemovie.Title = newexistingmovie.fullmoviebody.title
                                homemovielist.Add(newexistingbasichomemovie)
                                ListBox18.Items.Add(New ValueDescriptionPair(newexistingbasichomemovie.FullPathAndFilename, newexistingbasichomemovie.Title))
                            Catch ex As Exception
                            End Try
                        Else
                            newHomeMovieList.Add(newhomemovie)
                        End If
                    Next
                End If
            Next
        Next
        ToolStripStatusLabel9.Text = newHomeMovieList.Count.ToString & " New Home Movies Found"
        Dim counter As Integer = 1
        For Each item In newHomeMovieList
            ToolStripStatusLabel9.Text = "Adding Home Movie " & counter & " of " & newHomeMovieList.Count
            Me.Refresh()
            Application.DoEvents()
            If item.FullPathAndFilename <> "" Then
                Dim newhomemovie As New str_BasicHomeMovie
                newhomemovie.FullPathAndFilename = item.FullPathAndFilename
                newhomemovie.Title = item.Title
                Dim fulldetails As New HomeMovieDetails
                fulldetails.fullmoviebody.title = newhomemovie.Title

                'Get year for home movie using modified time since more accurate (Creation date is reset if a file is copied)
                Dim fileCreatedDate As DateTime = File.GetLastWriteTime(item.FullPathAndFilename)
                Dim format As String = "yyyy"
                Dim yearstring As String = fileCreatedDate.ToString(format)
                fulldetails.fullmoviebody.year = yearstring

                'create fanart for home movie if it does not exist
                Dim thumbpathandfilename As String = Preferences.GetFanartPath(item.FullPathAndFilename)
                If Not IO.File.Exists(thumbpathandfilename) Then
                    Try
                        Utilities.CreateScreenShot(item.FullPathAndFilename, thumbpathandfilename, 10)
                    Catch ex As Exception

                    End Try
                End If
                Dim nfofilename As String = ""
                Dim extension As String = ""
                fulldetails.fullmoviebody.movieset = "Home Movie"
                fulldetails.fileinfo.fullpathandfilename = newhomemovie.FullPathAndFilename
                fulldetails.filedetails = Preferences.Get_HdTags(fulldetails.fileinfo.fullpathandfilename)
                Dim rtime As Integer = (fulldetails.filedetails.filedetails_video.DurationInSeconds.Value / 60)
                fulldetails.fullmoviebody.runtime = rtime.ToString
                Dim pathOnly As String = IO.Path.GetDirectoryName(fulldetails.fileinfo.fullpathandfilename) & "\"
                Dim nfopath As String = pathOnly & IO.Path.GetFileNameWithoutExtension(fulldetails.fileinfo.fullpathandfilename) & ".nfo"
                newhomemovie.FullPathAndFilename = nfopath
                nfoFunction.nfoSaveHomeMovie(nfopath, fulldetails)
                homemovielist.Add(newhomemovie)
                ListBox18.Items.Add(New ValueDescriptionPair(newhomemovie.FullPathAndFilename, newhomemovie.Title))
            End If
            counter += 1
            progress = ((100 / newHomeMovieList.Count) * (counter))
            If progress > 100 Then progress = 100
            ToolStripProgressBar8.Value = progress
        Next
        ToolStripProgressBar8.Visible = False
        ToolStripStatusLabel9.Visible = False
    End Sub

    Private Sub HomeMovieCacheSave()
        Dim fullpath As String = workingProfile.HomeMovieCache
        If homemovielist.Count > 0 And homemoviefolders.Count > 0 Then
            If IO.File.Exists(fullpath) Then
                Dim don As Boolean = False
                Dim count As Integer = 0
                Do
                    Try
                        If IO.File.Exists(fullpath) Then
                            IO.File.Delete(fullpath)
                            don = True
                        Else
                            don = True
                        End If
                    Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
                    Finally
                        count += 1
                    End Try
                Loop Until don = True
            End If
            frmSplash2.Label1.Text = "Creating Home Movie Cache xml....."
            frmSplash2.Label2.Visible = False
            frmSplash2.ProgressBar1.Visible = False
            Dim doc As New XmlDocument
            Dim thispref As XmlNode = Nothing
            Dim xmlproc As XmlDeclaration
            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            doc.AppendChild(xmlproc)
            Dim root As XmlElement
            Dim child As XmlElement
            root = doc.CreateElement("homemovie_cache")
            Dim childchild As XmlElement
            Dim count2 As Integer = 0
            frmSplash2.Label2.Text = "Creating cache xml...."
            For Each movie In homemovielist
                child = doc.CreateElement("movie")
                childchild = doc.CreateElement("fullpathandfilename")
                childchild.InnerText = movie.FullPathAndFilename
                child.AppendChild(childchild)

                childchild = doc.CreateElement("title")
                childchild.InnerText = movie.Title
                child.AppendChild(childchild)
                root.AppendChild(child)
            Next
            doc.AppendChild(root)
            For f = 1 To 100
                Try
                    frmSplash2.Label2.Text = "Saving cache xml...." & f
                    Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
                    output.Formatting = Formatting.Indented
                    doc.WriteTo(output)
                    output.Close()
                    Exit For
                Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
                End Try
            Next
        Else
            Try
                If IO.File.Exists(fullpath) Then
                    IO.File.Delete(fullpath)
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub homemovieCacheLoad()
        homemovielist.Clear()

        Dim movielist As New XmlDocument
        Dim objReader As New System.IO.StreamReader(workingProfile.HomeMovieCache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close()

        movielist.LoadXml(tempstring)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In movielist("homemovie_cache")
            Select Case thisresult.Name
                Case "movie"
                    Dim newmovie As New str_BasicHomeMovie(SetDefaults)
                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            Case "fullpathandfilename"
                                newmovie.FullPathAndFilename = detail.InnerText
                            Case "title"
                                newmovie.Title = detail.InnerText
                        End Select
                    Next
                    homemovielist.Add(newmovie)
            End Select
        Next
        Call loadhomemovielist()
        Try
            ListBox18.SelectedIndex = 0
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    Private Sub loadhomemovielist()
        ListBox18.Items.Clear()
        For Each item In homemovielist
            ListBox18.Items.Add(New ValueDescriptionPair(item.FullPathAndFilename, item.Title))
        Next
    End Sub

    Private Sub ListBox18_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox18.DoubleClick
        mov_Play("HomeMovie")
    End Sub

    Private Sub ListBox18_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox18.MouseDown

    End Sub

    Private Sub ListBox18_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox18.MouseUp
        Try
            Dim ptIndex As Integer = ListBox18.IndexFromPoint(e.X, e.Y)
            If e.Button = MouseButtons.Right AndAlso ptIndex > -1 AndAlso ListBox18.SelectedItems.Count > 0 Then
                Dim newSelection As Boolean = True
                'If more than one movie is selected, check if right-click is on the selection.
                If ListBox18.SelectedItems.Count > 1 And ListBox18.GetSelected(ptIndex) Then
                    newSelection = False
                End If
                'Otherwise, bring up the context menu for a single movie


                If newSelection Then
                    ListBox18.SelectedIndex = ptIndex
                    'update context menu with movie name & also if we show the 'Play Trailer' menu item
                    PlaceHolderforHomeMovieTitleToolStripMenuItem.BackColor = Color.Honeydew
                    PlaceHolderforHomeMovieTitleToolStripMenuItem.Text = "'" & ListBox18.Text & "'"
                    PlaceHolderforHomeMovieTitleToolStripMenuItem.Font = New Font("Arial", 10, FontStyle.Bold)
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox18_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox18.SelectedValueChanged
        Try
            For Each homemovie In homemovielist
                If homemovie.FullPathAndFilename Is CType(ListBox18.SelectedItem, ValueDescriptionPair).Value Then
                    WorkingHomeMovie.fileinfo.fullpathandfilename = CType(ListBox18.SelectedItem, ValueDescriptionPair).Value
                    Call loadhomemoviedetails()
                End If
            Next
        Catch
        End Try

    End Sub

    Private Sub loadhomemoviedetails()
        HmMovTitle.Text = ""
        HmMovSort.Text = ""
        HmMovYear.Text = ""
        HmMovPlot.Text = ""
        HmMovStars.Text = ""
        PictureBox4.Image = Nothing
        WorkingHomeMovie = nfoFunction.nfoLoadHomeMovie(WorkingHomeMovie.fileinfo.fullpathandfilename)
        WorkingHomeMovie.fileinfo.fanartpath = Preferences.GetFanartPath(WorkingHomeMovie.fileinfo.fullpathandfilename)
        HmMovTitle.Text = WorkingHomeMovie.fullmoviebody.title
        HmMovSort.Text = WorkingHomeMovie.fullmoviebody.sortorder
        HmMovPlot.Text = WorkingHomeMovie.fullmoviebody.plot
        HmMovStars.Text = WorkingHomeMovie.fullmoviebody.stars
        HmMovYear.Text = WorkingHomeMovie.fullmoviebody.year
        PlaceHolderforHomeMovieTitleToolStripMenuItem.Text = WorkingHomeMovie.fullmoviebody.title
        PlaceHolderforHomeMovieTitleToolStripMenuItem.BackColor = Color.Honeydew
        PlaceHolderforHomeMovieTitleToolStripMenuItem.Font = New Font("Arial", 10, FontStyle.Bold)
        If IO.File.Exists(WorkingHomeMovie.fileinfo.fanartpath) Then
            util_ImageLoad(PictureBox4, WorkingHomeMovie.fileinfo.fanartpath, Utilities.DefaultFanartPath)
            Dim video_flags = VidMediaFlags(WorkingHomeMovie.filedetails)
            movieGraphicInfo.OverlayInfo(PictureBox4, "", video_flags)
        End If
    End Sub

    Private Sub HomeMovieFoldersRefresh()
        Preferences.homemoviefolders.Clear()
        For Each item In ListBox19.Items
            Preferences.homemoviefolders.Add(item)
        Next
        Call ConfigSave()
        Call rebuildHomeMovies()
    End Sub

    Private Sub btnHomeMovieSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeMovieSave.Click
        If HmMovTitle.Text <> "" Then
            WorkingHomeMovie.fullmoviebody.title = HmMovTitle.Text
        End If
        If HmMovSort.Text <> "" Then
            WorkingHomeMovie.fullmoviebody.sortorder = HmMovSort.Text
        End If
        WorkingHomeMovie.fullmoviebody.year = HmMovYear.Text
        WorkingHomeMovie.fullmoviebody.plot = HmMovPlot.Text
        WorkingHomeMovie.fullmoviebody.stars = HmMovStars.Text
        nfoFunction.nfoSaveHomeMovie(WorkingHomeMovie.fileinfo.fullpathandfilename, WorkingHomeMovie)
    End Sub

    Private Sub btnHomeFolderAdd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeFolderAdd.Click
        Try
            Dim allok As Boolean = True
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                Preferences.lastpath = thefoldernames
                For Each item As Object In ListBox19.Items
                    If thefoldernames.ToString = item.ToString Then allok = False
                Next

                If allok = True Then
                    ListBox19.Items.Add(thefoldernames)
                    ListBox19.Refresh()
                    Call HomeMovieFoldersRefresh()
                Else
                    MsgBox("        Folder Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnHomeFoldersRemove_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeFoldersRemove.Click
        Try
            While ListBox19.SelectedItems.Count > 0
                ListBox19.Items.Remove(ListBox19.SelectedItems(0))
            End While
            Call HomeMovieFoldersRefresh()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnHomeManualPathAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnHomeManualPathAdd.Click
        Try
            If tbHomeManualPath.Text = Nothing Then
                Exit Sub
            End If
            If tbHomeManualPath.Text = "" Then
                Exit Sub
            End If
            Dim tempstring As String = tbHomeManualPath.Text
            Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Dim exists As Boolean = False
            For Each item In ListBox19.Items
                If item.ToString.ToLower = tempstring.ToLower Then
                    exists = True
                    Exit For
                End If
            Next
            If exists = True Then
                MsgBox("        Folder Already Exists")
            Else
                Dim f As New IO.DirectoryInfo(tempstring)
                If f.Exists Then
                    ListBox19.Items.Add(tempstring)
                    ListBox19.Refresh()
                    Call HomeMovieFoldersRefresh()
                    tbHomeManualPath.Text = ""
                Else
                    Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If tempint = DialogResult.Yes Then
                        ListBox19.Items.Add(tempstring)
                        ListBox19.Refresh()
                        Call HomeMovieFoldersRefresh()
                        tbHomeManualPath.Text = ""
                    End If
                    Call HomeMovieFoldersRefresh()
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

#End Region   'Home Movie Routines, buttons etc.


    Sub BckWrkCheckNewVersion_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BckWrkCheckNewVersion.DoWork

        Dim ShowNoNewVersionMsgBox As String = DirectCast(e.Argument, Boolean)

        e.Result = New NewVersionCheckResult(ShowNoNewVersionMsgBox, CheckForNewVersion)
    End Sub

    Private Sub BckWrkCheckNewVersion_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BckWrkCheckNewVersion.RunWorkerCompleted

        Dim Results As NewVersionCheckResult = e.Result

        If IsNothing(Results.NewVersion) Then

            If Results.ShowNoNewVersionMsgBox Then
                MsgBox("You're up-to-date!", MsgBoxStyle.OkOnly, "No new version found")
            End If

            Exit Sub
        End If


        Dim answer = MsgBox("Would you like to download the new version?", MsgBoxStyle.YesNo, "New version " & Results.NewVersion & " available")

        If answer = MsgBoxResult.Yes Then
            OpenUrl(HOME_PAGE)
        End If
    End Sub

#Region "ToolStrip Menus"

    Private Sub tsmi_RenMovieOnly_click(sender As Object, e As EventArgs) Handles tsmi_RenMovieOnly.Click
        Dim ismovrenenabled As Boolean = Preferences.MovieRenameEnable
        Dim isusefolder As Boolean = Preferences.usefoldernames
        If Preferences.MovieManualRename Then
            If isusefolder Then
                Dim tempint As Integer = MessageBox.Show("You currently have 'UseFolderName' Selected" & vbCrLf & "Are you sure you wish to Rename this Movie file", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.No Then
                    Exit Sub
                End If
            End If
            Preferences.MovieRenameEnable = True
            Preferences.usefoldernames = False
            mov_ScrapeSpecific("rename_files")
            While BckWrkScnMovies.IsBusy
                Application.DoEvents()
            End While
        Else
            MsgBox("Manual Movie Rename is not enabled", 0)
        End If
        Preferences.MovieRenameEnable = ismovrenenabled
        Preferences.usefoldernames = isusefolder
    End Sub

    Private Sub tsmi_RenMovFolderOnly_click(sender As Object, e As EventArgs) Handles tsmi_RenMovFolderOnly.Click
        Dim isMovFoldRenEnabled As Boolean = Preferences.MovFolderRename
        If Preferences.MovieManualRename Then
            Preferences.MovFolderRename = True
            mov_ScrapeSpecific("rename_folders")
            While BckWrkScnMovies.IsBusy
                Application.DoEvents()
            End While
        Else
            MsgBox("Manual Movie Rename is not enabled", 0)
        End If
        Preferences.MovFolderRename = isMovFoldRenEnabled
    End Sub

    Private Sub tsmi_RenMovieAndFolder_click(sender As Object, e As EventArgs) Handles tsmi_RenMovieAndFolder.Click
        Dim isMovFoldRenEnabled As Boolean = Preferences.MovFolderRename
        Dim isMovRenEnabled As Boolean = Preferences.MovieRenameEnable
        Dim isusefolder As Boolean = Preferences.usefoldernames
        If Preferences.MovieManualRename Then
            Dim renmov As Boolean = True
            If isusefolder Then
                Dim tempint As Integer = MessageBox.Show("You currently have 'UseFolderName' Selected" & vbCrLf & "Are you sure you wish to Rename this Movie file" & vbCrLf & "Folder Renaming will still commence", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.No Then
                    renmov = False
                End If
            End If
            Preferences.MovFolderRename = True
            Preferences.MovieRenameEnable = True
            Preferences.usefoldernames = False
            _rescrapeList.FullPathAndFilenames.Clear()

            For Each row As DataGridViewRow In DataGridViewMovies.SelectedRows
                _rescrapeList.FullPathAndFilenames.Add(row.Cells("fullpathandfilename").Value.ToString)
            Next
            rescrapeList.ResetFields()
            rescrapeList.Rename_Folders = True
            rescrapeList.Rename_Files = renmov

            RunBackgroundMovieScrape("BatchRescrape")

            While BckWrkScnMovies.IsBusy
                Application.DoEvents()
            End While
        Else
            MsgBox("Manual Movie Rename is not enabled", 0)
        End If
        Preferences.MovFolderRename = isMovFoldRenEnabled
        Preferences.MovieRenameEnable = isMovRenEnabled
        Preferences.usefoldernames = isusefolder
    End Sub

    Private Sub tsmiOpenInMkvmergeGUI_Click(sender As Object, e As EventArgs) Handles tsmiOpenInMkvmergeGUI.Click

        If DataGridViewMovies.SelectedRows.Count > 10 Then
            If MsgBox("Are you sure you want to open that many?", MsgBoxStyle.YesNo, "About to open " & DataGridViewMovies.SelectedRows.Count & " instances of Mkvmerge Gui") <> MsgBoxResult.Ok Then Exit Sub
        End If

        For Each row In DataGridViewMovies.SelectedRows
            Process.Start(Preferences.MkvMergeGuiPath, """" & Utilities.GetFileName(row.Cells("fullpathandfilename").Value) & """")
        Next
    End Sub

    Private Sub tsmiCheckForNewVersion_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCheckForNewVersion.Click
        BckWrkCheckNewVersion.RunWorkerAsync(True)
    End Sub

    Private Sub tsmiConvertToFrodo_Click(sender As Object, e As EventArgs) Handles tsmiConvertToFrodo.Click
        Try
            Call mov_ScrapeSpecific("Convert_To_Frodo")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tsm_TvScrapePoster_Click(sender As System.Object, e As System.EventArgs) Handles tsm_TvScrapePoster.Click
        Try
            TvScrapePosterBanner("poster")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tsm_TvSelectPoster_Click(sender As System.Object, e As System.EventArgs) Handles tsm_TvSelectPoster.Click
        Try
            TvSelectPosterBanner(False)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tsm_TvScrapeBanner_Click(sender As System.Object, e As System.EventArgs) Handles tsm_TvScrapeBanner.Click
        Try
            TvScrapePosterBanner("series")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tsm_TvSelectBanner_Click(sender As System.Object, e As System.EventArgs) Handles tsm_TvSelectBanner.Click
        Try
            TvSelectPosterBanner(True)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ReloadHtmlTemplatesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReloadHtmlTemplatesToolStripMenuItem.Click
        Dim mediaDropdown As New SortedList(Of String, String)
        mediaInfoExp.addTemplates(mediaDropdown)
        ExportMovieListInfoToolStripMenuItem.DropDownItems.Clear()
        ExportTVShowInfoToolStripMenuItem.DropDownItems.Clear()
        For Each item In mediaDropdown
            If item.Value = MediaInfoExport.mediaType.Movie Then
                ExportMovieListInfoToolStripMenuItem.DropDownItems.Add(item.Key)
            ElseIf item.Value = MediaInfoExport.mediaType.TV Then
                ExportTVShowInfoToolStripMenuItem.DropDownItems.Add(item.Key)
            End If
        Next
    End Sub

    Private Sub FixNFOCreateDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FixNFOCreateDateToolStripMenuItem.Click
        Dim fixCreateDate As New frmCreateDateFix
        If Preferences.MultiMonitoEnabled Then
            Dim w As Integer = fixCreateDate.Width
            Dim h As Integer = fixCreateDate.Height
            fixCreateDate.Bounds = screen.AllScreens(CurrentScreen).Bounds
            fixCreateDate.StartPosition = FormStartPosition.Manual
            fixCreateDate.Width = w
            fixCreateDate.Height = h
        End If
        fixCreateDate.ShowDialog()
    End Sub

    Private Sub EmptyCacheFolderToolStripMenuItem_Click( sender As Object,  e As EventArgs) Handles EmptyCacheFolderToolStripMenuItem.Click
        If Not tvbckrescrapewizard.IsBusy AndAlso Not bckgroundscanepisodes.IsBusy AndAlso Not bckgrnd_tvshowscraper.IsBusy AndAlso Not Bckgrndfindmissingepisodes.IsBusy AndAlso Not BckWrkScnMovies.IsBusy Then
            Dim mess As New frmMessageBox("Emptying Cache Folder", , "   Please Wait.   ")
            mess.Show()
            mess.Refresh()
            Application.DoEvents()
            CleanCacheFolder(True)
            mess.Close()
        End If
    End Sub

    Private Sub RefreshGenreListboxToolStripMenuItem_Click( sender As Object,  e As EventArgs) Handles RefreshGenreListboxToolStripMenuItem.Click
        GenreMasterLoad()
        util_GenreLoad()
    End Sub

    Private Sub ExportLibraryToolStripMenuItem_Click( sender As Object,  e As EventArgs) Handles ExportLibraryToolStripMenuItem.Click
        'Dim res As DialogResult = MsgBox("Are you Sure?" &vbCrLf & "This will export your Movies and TV Series" & vbCrLf & "to a format compatible for you to" & vbCrLf & "Import into XBMC/Kodi.",MsgBoxStyle.OkCancel)
        'If res = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'ExportToXbmcdb()
        '''
        'MsgBox("This function is not ready yet")
        'Exit Sub
        '''

        Dim frmxport As New frmXbmcExport
        If Preferences.MultiMonitoEnabled Then
            Dim w As Integer = frmxport.Width
            Dim h As Integer = frmxport.Height
            frmxport.Bounds = screen.AllScreens(CurrentScreen).Bounds
            frmxport.StartPosition = FormStartPosition.Manual
            frmxport.Width = w
            frmxport.Height = h
        End If
        frmxport.ShowDialog()
    End Sub

#End Region 'ToolStrip Menus misc

    Sub MkvMergeGuiPath_ChangeHandler()
        tsmiOpenInMkvmergeGUI.Enabled = True
        tbMkvMergeGuiPath.Text = Preferences.MkvMergeGuiPath
    End Sub

#Region "Movie Filters"

    Private Sub ConfigureMovieFiltersToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfigureMovieFiltersToolStripMenuItem1.Click
        Dim frm As New frmConfigureMovieFilters

        frm.Init(SplitContainer5.Panel2)

        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            UpdateMovieFiltersPanel()
            Preferences.ConfigSave()
            UpdateFilteredList()
        End If
    End Sub

    Sub UpdateMovieFiltersPanel()
        'ResizeBottomLHSPanel
        Preferences.movie_filters.UpdateFromPanel()
        ResizeBottomLHSPanel(MovieFiltersPanelMaxHeight)
        Preferences.movie_filters.PositionMovieFilters()
    End Sub

    ReadOnly Property MovieFiltersPanelMaxHeight As Integer
        Get
            Return Preferences.movie_filters.CalculatedFilterPanelHeight
        End Get
    End Property

    Private Sub SplitContainer5_DoubleClick(sender As Object, e As EventArgs) Handles SplitContainer5.DoubleClick

        If SplitContainer5.Panel2.Height = MovieFiltersPanelMaxHeight - 5 Then
            ResizeBottomLHSPanel(0)
        Else
            ResizeBottomLHSPanel(MovieFiltersPanelMaxHeight)
        End If
    End Sub

    Private Sub ResizeBottomLHSPanel(height As Integer, Optional ByVal MaxHeight As Integer = 0)
        ProgState = ProgramState.ResizingSplitterPanel

        SplitContainer5.SplitterDistance = SplitContainer5.Height - height

        DataGridViewMovies.Height = SplitContainer5.SplitterDistance - 140

        If MaxHeight = 0 Then
            SplitContainer5.Panel2.AutoScrollMinSize = New Size(SplitContainer5.Panel2.AutoScrollMinSize.Width, height - 10)
        Else
            SplitContainer5.Panel2.AutoScrollMinSize = New Size(SplitContainer5.Panel2.AutoScrollMinSize.Width, MaxHeight - 10)
        End If

        ProgState = ProgramState.Other
    End Sub

    Private Sub ResizeBottomLHSPanel()
        If ProgState = ProgramState.ResizingSplitterPanel Then Return

        If Not MainFormLoadedStatus Then Return
        If Not SplitContainer5.Panel2.Visible Then Return

        Dim maxSize = MovieFiltersPanelMaxHeight
        Dim minSize = 2

        If SplitContainer5.Height - SplitContainer5.SplitterDistance > maxSize Then
            SplitContainer5.SplitterDistance = SplitContainer5.Height - maxSize
        End If

        If SplitContainer5.Height - SplitContainer5.SplitterDistance < minSize Then
            SplitContainer5.SplitterDistance = SplitContainer5.Height - minSize
        End If

        'Needed as workaround for splitter panel framework bug:
        Dim h = SplitContainer5.SplitterDistance - 140
        If h < minSize Then h = minSize
        DataGridViewMovies.Height = h
    End Sub

    Private Sub ResetFilter(sender As Object, e As EventArgs) Handles lblFilterSet.Click, lblFilterVotes.Click, lblFilterRating.Click,
                                                                        lblFilterCertificate.Click, lblFilterGenre.Click, lblFilterYear.Click,
                                                                        lblFilterResolution.Click, lblFilterAudioCodecs.Click, lblFilterAudioChannels.Click,
                                                                        lblFilterAudioBitrates.Click, lblFilterNumAudioTracks.Click, lblFilterAudioLanguages.Click,
                                                                        lblFilterActor.Click, lblFilterSource.Click, lblFilterTag.Click,
                                                                        lblFilterDirector.Click, lblFilterVideoCodec.Click, lblFilterSubTitleLang.Click,
                                                                        lblFilterFolderSizes.Click
                                                                        

        Dim filter As Object = GetFilterFromLabel(sender)

        ProgState = ProgramState.ResettingFilters
        filter.Reset()
        ProgState = ProgramState.Other

        UpdateFilteredList()
    End Sub

    Private Sub ChangeFilterMode(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterGenreMode.Click, lblFilterSetMode.Click, lblFilterResolutionMode.Click,
                                                                                       lblFilterAudioCodecsMode.Click, lblFilterCertificateMode.Click, lblFilterAudioChannelsMode.Click,
                                                                                       lblFilterAudioBitratesMode.Click, lblFilterNumAudioTracksMode.Click, lblFilterAudioLanguagesMode.Click,
                                                                                       lblFilterActorMode.Click, lblFilterSourceMode.Click, lblFilterTagMode.Click, lblFilterDirectorMode.Click,
                                                                                       lblFilterVideoCodecMode.Click, lblFilterSubTitleLangMode.Click

        Dim lbl As Label = sender
        Dim filter As MC_UserControls.TriStateCheckedComboBox = GetFilterFromLabel(lbl)

        filter.QuickSelect = Not filter.QuickSelect

        lbl.Text = If(filter.QuickSelect, "S", "M")

        movie_filters.GetItem(filter.Name).QuickSelect = filter.QuickSelect

    End Sub

    Private Function GetFilterFromLabel(ctl As Control)

        Dim name As String = ctl.Name.RemoveAfterMatch("Mode")

        Return ctl.Parent.Controls("cb" + name.Substring(3, name.Length - 3))
    End Function

    Private Sub ResetCbGeneralFilter(sender As Control, e As EventArgs) Handles lblFilterGeneral.Click
        cbFilterGeneral.SelectedIndex = 0
    End Sub

    Private Sub cbFilterGeneral_DropDown(sender As Object, e As EventArgs) Handles cbFilterGeneral.DropDown

        Dim maxWidth As Integer = cbFilterGeneral.DropDownWidth
        Dim g As Graphics = cbFilterGeneral.CreateGraphics
        Dim vertScrollBarWidth As Integer = If(cbFilterGeneral.Items.Count > cbFilterGeneral.MaxDropDownItems, SystemInformation.VerticalScrollBarWidth, 0)
        Dim renderedWidth As Integer
        Dim lbl As Label = New Label()

        lbl.AutoSize = True
        lbl.Font = cbFilterGeneral.Font

        For Each item As String In cbFilterGeneral.Items
            lbl.Text = item
            renderedWidth = lbl.PreferredSize.Width + vertScrollBarWidth
            maxWidth = Math.Max(maxWidth, renderedWidth)
        Next

        cbFilterGeneral.DropDownWidth = maxWidth
    End Sub

#End Region   'Movie Filter code

    Public Shared Function VidMediaFlags(ByVal Vidfiledetails As FullFileDetails) As List(Of KeyValuePair(Of String, String))
        Dim flags As New List(Of KeyValuePair(Of String, String))
        Try
            If Vidfiledetails.filedetails_audio.Count>0 Then

                Dim defaultAudioTrack = (From x In Vidfiledetails.filedetails_audio Where x.DefaultTrack.Value="Yes").FirstOrDefault

                If IsNothing(defaultAudioTrack) Then
                    defaultAudioTrack = Vidfiledetails.filedetails_audio(0)
                End If

                Dim tracks = If(Preferences.ShowAllAudioTracks,Vidfiledetails.filedetails_audio,From x In Vidfiledetails.filedetails_audio Where x=defaultAudioTrack)

                For Each track In tracks
                    flags.Add( New KeyValuePair(Of String, string)("channels"+GetNotDefaultStr(track=defaultAudioTrack), GetNumAudioTracks(track.Channels.Value)))
                    flags.Add( New KeyValuePair(Of String, string)("audio"+GetNotDefaultStr(track=defaultAudioTrack), track.Codec.Value) )               
                Next
            End If

            flags.Add(New KeyValuePair(Of String, string)("aspect", Utilities.GetStdAspectRatio(Vidfiledetails.filedetails_video.Aspect.Value)))
            flags.Add(New KeyValuePair(Of String, string)("codec", Utilities.GetCodecCommonName(Vidfiledetails.filedetails_video.Codec.Value.RemoveWhitespace)))
            flags.Add(New KeyValuePair(Of String, string)("resolution", If(Vidfiledetails.filedetails_video.VideoResolution < 0, "", Vidfiledetails.filedetails_video.VideoResolution.ToString)))
        Catch
        End Try
        Return flags

    End Function

    Private Shared Function GetNotDefaultStr(dflt)
        If dflt Then
            Return ""
        End If
        
        Return "_notdefault"
    End Function

    Private Shared Function GetNumAudioTracks(AudCh)
        Return If((AudCh = "" Or AudCh = "-1"), "0", AudCh.Substring(0, 1))
    End Function

    Private Sub TabPage18_Enter(sender As Object, e As EventArgs) Handles TabPage18.Enter
        UcGenPref_XbmcLink.Pop()
    End Sub

    Private Sub tpPrxy_Enter(sender As Object, e As EventArgs) Handles tpPrxy.Enter
        UcGenPref_Proxy1.pop()
    End Sub

    Private Sub tsmiSyncToXBMC_Click(sender As Object, e As EventArgs) Handles tsmiSyncToXBMC.Click
        Try
            Call mov_ScrapeSpecific("Xbmc_Sync")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
        
#Region "Functions"

    Private Function AssignClipboardImage(picBox As PictureBox) As Boolean
        Try
            If Clipboard.GetDataObject.GetDataPresent(DataFormats.FileDrop) Then
                Dim pth As String = CType(Clipboard.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString
                Dim FInfo As IO.FileInfo = New IO.FileInfo(pth)
                If FInfo.Extension.ToLower() = ".jpg" Or FInfo.Extension.ToLower() = ".tbn" Or FInfo.Extension.ToLower() = ".bmp" Or FInfo.Extension.ToLower() = ".png" Then
                    util_ImageLoad(picBox, pth, Utilities.DefaultPosterPath)
                    Return True
                Else
                    MessageBox.Show("Not a picture")
                End If
            End If

            If Clipboard.GetDataObject.GetDataPresent(DataFormats.Bitmap) Then
                picBox.Image = Clipboard.GetDataObject().GetData(DataFormats.Bitmap)
                Return True
            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

        Return False
    End Function

    Public Function CheckForNewVersion() As String

        Dim MC_Version_RegEx = "<th><span class=""rating_header"">current</span></th>.*?<td>[\s]+.*?([0-9]*\.?[0-9]+).*?[\s]+</td>"

        Dim s As New Classimdb

        Dim html As String = s.loadwebpage(Preferences.proxysettings, HOME_PAGE, True, 10).ToString

        Dim m = Regex.Match(html, MC_Version_RegEx, RegexOptions.Singleline)

        Dim displayVersion As String = m.Groups(1).Value.Trim
        Dim latestVersion As String = displayVersion.Replace(".", "")
        Dim currVersion As String = Trim(System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(",")(1)).Replace(".", "").Replace("Version=", "")

        If latestVersion.Length < 4 Then Return Nothing
        If latestVersion <> currVersion Then Return displayVersion

        Return Nothing
    End Function

#End Region

    Private Sub GoogleSearch(ByVal search As String)
        'Open Webpage at Google image search for movietitle & year
        Dim title As String = workingMovieDetails.fullmoviebody.title
        Dim year As String = workingMovieDetails.fullmoviebody.year

        Dim url As String = ""
        If Not IsNothing(year) AndAlso IsNumeric(year) AndAlso year.Length = 4 Then
            url = "http://images.google.com/images?q=" & title & "+" & year & search
        Else
            url = "http://images.google.com/images?q=" & title & search
        End If
        OpenUrl(url)
    End Sub
    
    Public Function GetlistofPlots() As String
        Dim mess As New frmMessageBox("Gathering plots", , "     Please Wait.     ")
        mess.Show()
        mess.Refresh()
        Dim ListofPlots As New List(Of String)
        Dim _imdbScraper As New Classimdb
        Dim tmdb As New TMDB
        tmdb.Imdb = workingMovieDetails.fullmoviebody.imdbid
        tmdb.TmdbId = workingMovieDetails.fullmoviebody.tmdbid
        Dim tmdbplot As String = Nothing
        Try
            tmdbplot = tmdb.Movie.overview
        Catch 
            tmdbplot = Nothing
        End Try
        ListofPlots = _imdbScraper.GetImdbPlots(workingMovieDetails.fullmoviebody.imdbid)
        If Not IsNothing(tmdbplot) AndAlso tmdbplot <> "" Then ListofPlots.Add(tmdbplot)
        mess.Close()
        If ListofPlots.Count < 2 Then 
            MsgBox("No Extra Plots found for this movie", MsgBoxStyle.Exclamation)
            Return Nothing
        End If
        Dim plotfrm As New frmmovieplotlist(ListofPlots)
        plotfrm.ShowDialog
        If plotfrm.DialogResult = Windows.Forms.DialogResult.Cancel Then
            plotfrm.Dispose()
            Return Nothing
        End If
        Dim plotselected As Integer = plotfrm.cmbxplotnumber.SelectedIndex
        plotfrm.Dispose()
        Return ListofPlots(plotselected)
    End Function
    
    'Public Sub ExportToXbmcdb()
    '    Dim outputfolder As String = Nothing
    '    Dim dialog As New FolderBrowserDialog()
    '    dialog.RootFolder = Environment.SpecialFolder.Desktop
    '    dialog.ShowNewFolderButton = True
    '    dialog.SelectedPath = "C:\"
    '    dialog.Description = "Select Path to save Exported data""
    '    If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        outputfolder = dialog.SelectedPath
    '    End If
    '    If IsNothing(outputfolder) Then
    '        MsgBox("No folder selected, export aborted")
    '        Exit Sub
    '    End If
    '    outputfolder = Validatefolder(outputfolder)
    '    If IsNothing(outputfolder) Then Exit Sub

    '    Dim frmxport As New frmXbmcExport
    '    If Preferences.MultiMonitoEnabled Then
    '        Dim w As Integer = frmxport.Width
    '        Dim h As Integer = frmxport.Height
    '        frmxport.Bounds = screen.AllScreens(CurrentScreen).Bounds
    '        frmxport.StartPosition = FormStartPosition.Manual
    '        frmxport.Width = w
    '        frmxport.Height = h
    '    End If
    '    frmxport.ShowDialog()
    '    'Tally movies to process

    '    'Tally Tv Series to process
        
    'End Sub

    Private Sub tsmiMovieSetIdCheck_Click( sender As Object,  e As EventArgs) Handles tsmiMovieSetIdCheck.Click
        rescrapeList.ResetFields
        _rescrapeList.FullPathAndFilenames.Clear()
        For Each movie As ComboList In oMovies.MovieCache
            If movie.MovieSet.MovieSetName.ToLower <> "-none-" AndAlso movie.MovieSet.MovieSetId = "" Then
                _rescrapeList.FullPathAndFilenames.Add(movie.fullpathandfilename)
            End If
        Next
        If _rescrapeList.FullPathAndFilenames.Count = 0 Then Exit Sub
        rescrapeList.tmdb_set_id = True
        RunBackgroundMovieScrape("BatchRescrape")
        'While BckWrkScnMovies.IsBusy
        '    Application.DoEvents()
        'End While
    End Sub
End Class