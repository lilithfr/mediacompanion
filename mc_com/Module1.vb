Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
Imports System.Xml
Imports System.ComponentModel
Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.IO.Compression
Imports Media_Companion



Module Module1
    Public Const SetDefaults = True
    Dim arguments As New List(Of String)
    Dim mediaexportfile As String = ""
    Dim oProfiles As New Profiles
    Dim listofargs As New List(Of arguments)
    Dim profile As String = "default"
'    Dim fullMovieList As New List(Of ComboList)
    Dim basictvlist As New List(Of basictvshownfo)
    Dim defaultOfflineArt As String = ""
    Dim actorDB As New List(Of ActorDatabase)
    Dim showstoscrapelist As New List(Of String)
    Dim newEpisodeList As New List(Of episodeinfo)
    Dim defaultPoster As String = ""
    Dim visible As Boolean = True
    Dim sw As StreamWriter
    Dim logfile As String = "mc_com.log"
    Dim logstr As New List(Of String)
    Dim WithEvents scraper As New BackgroundWorker
    Dim oMovies As New Movies(scraper)
    Dim EnvExit As Integer = 0
    Dim DoneAEp As Boolean = False
    Private Declare Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Declare Function ShowWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Int32
    

    Dim newepisodetoadd As New episodeinfo


    Sub Main
        Dim domovies      As Boolean = False
        Dim dotvepisodes  As Boolean = False
        Dim domediaexport As Boolean = False
        Dim docacheclean  As Boolean = False
        

        For Each arg As String In Environment.GetCommandLineArgs()
            arguments.Add(arg)
        Next

        If arguments.Count = 1 Then
            Dim arg As New arguments
            arg.switch = "help"
            listofargs.Add(arg)
        Else
            For f = 1 To arguments.Count - 1
                If arguments(f) = "-m" Then
                    Dim arg As New arguments
                    arg.switch = arguments(f)
                    listofargs.Add(arg)
                ElseIf arguments(f) = "-e" Then
                    Dim arg As New arguments
                    arg.switch = arguments(f)
                    listofargs.Add(arg)
                ElseIf arguments(f) = "-v" Then
                    visible = False
                ElseIf arguments(f) = "-p" Then
                    Dim arg As New arguments
                    arg.switch = arguments(f)
                    Try
                        arg.argu = arguments(f + 1)
                        listofargs.Add(arg)
                    Catch
                        listofargs.Clear()
                        Dim arg2 As New arguments
                        arg2.switch = "help"
                        listofargs.Add(arg2)
                        Exit For
                    End Try
                ElseIf arguments(f) = "-x" Then
                    Dim arg As New arguments
                    arg.switch = arguments(f)
                    Try
                        arg.argu = arguments(f + 1)
                        listofargs.Add(arg)
                        Try
                            mediaexportfile = arguments(f + 2)
                            domediaexport = True
                        Catch ex As Exception
                            listofargs.Clear()
                            Dim arg2 As New arguments
                            arg2.switch = "help"
                            listofargs.Add(arg2)
                            Exit For
                        End Try
                    Catch
                        listofargs.Clear()
                        Dim arg2 As New arguments
                        arg2.switch = "help"
                        listofargs.Add(arg2)
                        Exit For
                    End Try
                ElseIf arguments(f) = "-c" Then
                    Dim arg As New arguments
                    arg.switch = arguments(f)
                    listofargs.Add(arg)
                End If
            Next
        End If

        If listofargs.Count = 0 Then
            Dim arg As New arguments
            arg.switch = "help"
            listofargs.Add(arg)
        End If

        
        If listofargs(0).switch = "help" Then
            ConsoleOrLog("****************************************************")
            ConsoleOrLog("Media Companion Command Line Tool")
            ConsoleOrLog("")
            ConsoleOrLog("Useage")
            ConsoleOrLog("mc_com.exe [-m] [-e] [-p ProfileName] [-x templatename outputpath] [-v]")
            ConsoleOrLog("-m to scrape movies")
            ConsoleOrLog("-e to scrape episodes")
            ConsoleOrLog("-x [templatename] [outputpath] to export media info list ")
            ConsoleOrLog("-v to run with no Console window. All information will be written")
            ConsoleOrLog("    to a log file in Media Companion's folder.  Log is overwritten")
            ConsoleOrLog("    each run of mc_com.exe")
            ConsoleOrLog("")
            ConsoleOrLog("Example")
            ConsoleOrLog("mc_com.exe -m -e -p billy -x basiclist C:\Movielist\testfile.html")
            ConsoleOrLog("will search for and scrape any new movies and episodes")
            ConsoleOrLog("using the folders and settings of the 'billy' profile,")
            ConsoleOrLog("then create a new media list using the named template")
            ConsoleOrLog("Without the profile arg the default profile will be used")
            ConsoleOrLog("")
            ConsoleOrLog("Tip: When using profile, template or filenames that contain")
            ConsoleOrLog("spaces, enclose with quotes, eg.")
            ConsoleOrLog("mc_com.exe -m -p ""my profile"" -x ""new list"" ""C:\Movie list\test.html""")
            ConsoleOrLog("")
            ConsoleOrLog("****************************************************")
            EnvExit = 0
            Environment.Exit(EnvExit)
        End If
        Preferences.applicationPath = AppDomain.CurrentDomain.BaseDirectory
        If Not visible Then 
            ShowWindow(GetConsoleWindow(), 0)  ' value of '0' = hide, '1' = visible
            LogStart
        End If
        

        For Each arg In listofargs
            If arg.switch = "-m" Then
                domovies = True
            End If
            If arg.switch = "-e" Then
                dotvepisodes = True
            End If
            If arg.switch = "-p" Then
                profile = arg.argu
            End If
            If arg.switch = "-x" Then
                domediaexport = True
            End If
            If arg.switch = "-c" Then
                docacheclean = True
            End If
        Next

        Dim done As Boolean = False
        
        defaultPoster = Path.Combine(Preferences.applicationPath, "Resources\default_poster.jpg")

        ConsoleOrLog("Loading Config")
        Preferences.SetUpPreferences()
        Call InitMediaFileExtensions()

        If File.Exists(Preferences.applicationPath & "\settings\profile.xml") = True Then
             
            oProfiles.Load

            If profile = "default" Then
                profile = oProfiles.DefaultProfile
            End If
            For Each prof In oProfiles.ProfileList
                If prof.ProfileName = profile Then

                    prof.Assign(Preferences.workingProfile)
                    
                    done = True
                    Exit For
                End If
            Next
            If Not done Then
                ConsoleOrLog("Unable to find profile name: " & profile)
                ConsoleOrLog("****************************************************")
                EnvExit = 1
                Environment.Exit(EnvExit)
            End If
        Else
            ConsoleOrLog("Unable to find profile file: " & Preferences.applicationPath & "\settings\profile.xml")
            ConsoleOrLog("****************************************************")
            EnvExit = 1
            Environment.Exit(EnvExit)
        End If
        defaultOfflineArt = Path.Combine(Preferences.applicationPath, "Resources\default_offline.jpg")
        Preferences.LoadConfig

        If domovies Or domediaexport Then
            If File.Exists(Preferences.workingProfile.moviecache) Then
                ConsoleOrLog("Loading Movie cache")
                oMovies.LoadMovieCache
            End If
        End If


        Try
            ConsoleOrLog("Loading Movie Database caches")
            oMovies.LoadPeopleCaches
        Catch
            oMovies.RebuildMoviePeopleCaches
        End Try

        If domovies Then

            StartNewMovies
            If Preferences.DoneAMov Then
                EnvExit +=2
                oMovies.SaveMovieCache
                oMovies.SaveActorCache
                oMovies.SaveDirectorCache
            End If
            ConsoleOrLog("")
        End If
        If dotvepisodes = True Then
            If IO.File.Exists(Preferences.workingProfile.tvcache) Then
                ConsoleOrLog("Loading Tv cache")
                Call tvcacheLoad()
            End If
            If IO.File.Exists(Preferences.workingProfile.regexlist) Then
                Call util_RegexLoad()
            End If
            If Preferences.tv_RegexScraper.Count = 0 Then
                Preferences.tv_RegexScraper.Add("[Ss]([\d]{1,4}).?[Ee]([\d]{1,4})")
                Preferences.tv_RegexScraper.Add("([\d]{1,4}) ?[xX] ?([\d]{1,4})")
                Preferences.tv_RegexScraper.Add("([0-9]+)([0-9][0-9])")
            End If
            If Preferences.tv_RegexRename.Count = 0 Then
                Preferences.tv_RegexRename.Add("Show Title - S01E01 - Episode Title.ext")
                Preferences.tv_RegexRename.Add("S01E01 - Episode Title.ext")
                Preferences.tv_RegexRename.Add("Show Title - 1x01 - Episode Title.ext")
                Preferences.tv_RegexRename.Add("1x01 - Episode Title.ext")
                Preferences.tv_RegexRename.Add("Show Title - 101 - Episode Title.ext")
                Preferences.tv_RegexRename.Add("101 - Episode Title.ext")
            End If
            For Each item In basictvlist
                If item.fullpath.ToLower.IndexOf("tvshow.nfo") <> -1 Then
                    showstoscrapelist.Add(item.fullpath)
                End If
            Next
            If showstoscrapelist.Count > 0 Then
                Renamer.setRenamePref(Preferences.tv_RegexRename.Item(Preferences.tvrename), Preferences.tv_RegexScraper)
                Call episodescraper(showstoscrapelist, False)
                If DoneAEp Then
                    Call tvcacheClean()
                    Call tvcacheSave()
                    EnvExit +=4
                End If
            End If
        End If
        If domediaexport = True Then
            For Each arg In listofargs
                If arg.switch = "-x" Then
                    ConsoleOrLog("Starting Media Info Export")
                    ConsoleOrLog("")
                    Dim mediaInfoExp As New MediaInfoExport
                    Dim setMovies = New SortedList(Of String, Media_Companion.ComboList)
                    Dim key As String = String.Empty
                    For Each movie In oMovies.MovieCache
                        Dim title As String = Preferences.RemoveIgnoredArticles(movie.title)
                        movie.title = title
                        If Preferences.sorttitleignorearticle Then
                            Dim sorttitle As String = Preferences.RemoveIgnoredArticles(movie.sortorder)
                            movie.sortorder = sorttitle
                        End If
                        Dim appendIncr As String = String.Empty
                        For strIncr = 1 To 5
	                        Select Case Preferences.moviesortorder
		                        Case 0
				                    key = String.Format("{0}{1}{2}{3}", movie.title, movie.year, movie.id, appendIncr)
			                    Case 1
				                    key = String.Format("{0}{1}{2}{3}", movie.year, movie.title, movie.id, appendIncr)
			                    Case 2
				                    key = String.Format("{0}{1}{2}{3}", movie.filedate, movie.title, movie.id, appendIncr)
			                    Case 3
				                    key = String.Format("{0}{1}{2}{3}", movie.runtime, movie.title, movie.id, appendIncr)
			                    Case 4
				                    key = String.Format("{0}{1}{2}{3}", movie.rating, movie.title, movie.id, appendIncr)
			                    Case 5
				                    key = String.Format("{0}{1}{2}{3}", movie.sortorder, movie.year, movie.id, appendIncr)
			                    Case 6
				                    key = String.Format("{0}{1}{2}{3}", movie.createdate, movie.title, movie.id, appendIncr)
			                    Case 7
				                    key = String.Format("{0}{1}{2}{3}", movie.Votes, movie.title, movie.id, appendIncr)
	                        End Select
                            If Not setMovies.ContainsKey(key) Then
			                    setMovies.Add(key, movie)
			                    Exit For
		                    End If
		                    appendIncr = strIncr
	                    Next
                    Next
                    Dim mediaCollection As Object = If(Preferences.movieinvertorder, setMovies.Values.Reverse.ToList, setMovies.Values.ToList)
                    Call mediaInfoExp.addTemplates()
                    Dim templateType As MediaInfoExport.mediaType
                    If mediaInfoExp.setTemplate(arg.argu, templateType) AndAlso templateType = MediaInfoExport.mediaType.Movie Then
                        Call mediaInfoExp.createDocument(mediaexportfile, mediaCollection)
                    Else
                        ConsoleOrLog("  Export aborted - template name provided is invalid")
                        ConsoleOrLog("  (and only Movies are supported currently)")
                        ConsoleOrLog("")
                    End If
                    ConsoleOrLog("Media Info Export complete")
                    ConsoleOrLog("")
                End If
            Next

        End If
        If docacheclean Then
            ConsoleOrLog("Tv Cache cleaning commencing")
            Call tvcacheLoad()
            Call tvcacheClean()
            Call tvcacheSave()
            ConsoleOrLog("Tv Cache cleaning complete")
        End If
        ConsoleOrLog("")
        ConsoleOrLog("Tasks Completed")
        ConsoleOrLog("****************************************************")
        If Not visible Then exitsound
        System.Environment.Exit(EnvExit)
    End Sub

    Public Sub LogStart
        logfile = Preferences.applicationPath & logfile
        If File.Exists(logfile) Then
            File.Delete(logfile)
        End If

        Dim logstr As String = ""
        
        logstr &= "****************************************************"
        ConsoleOrLog(logstr)
        ConsoleOrLog("New Log Started :  " & DateTime.Now)
        ConsoleOrLog("")
    End Sub

    Public Sub ConsoleOrLog(ByVal str As String)
        If visible Then
            Console.WriteLine(str)
        Else
            Using sw As New StreamWriter(logfile, true)
                sw.WriteLine(str.TrimEnd) ' & vbCrlf)
                'sw.WriteLine(vbCrLf)
                sw.Close()
            End Using
        End If

    End Sub

    Public Sub exitsound
        'To Be completed to notify user mc_com has finished if not visible.
        Try
            My.Computer.Audio.Play(Path.Combine(Preferences.applicationPath, "Resources\chimes.wav"))
            Threading.Thread.Sleep(500)
        Catch
        End Try
    End Sub

    'Public Sub screenshot(ByVal fullpathandfilename As String, ByVal SavePath As String, Optional ByVal overwrite As Boolean = False)
    '    Dim status As String = "working"
    '    Try
    '        Dim skip As Boolean = False
    '        If Not IO.File.Exists(SavePath) Or overwrite = True Then
    '            If IO.File.Exists(SavePath) Then
    '                Try
    '                    IO.File.Delete(SavePath)
    '                Catch
    '                    status = "nodelete"
    '                    skip = True
    '                End Try
    '            End If
    '            If skip = False Then
    '                If IO.File.Exists(fullpathandfilename) Then
    '                    Try
    '                        Dim seconds As Integer = Preferences.ScrShtDelay
    '                        Dim myProcess As Process = New Process
    '                        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
    '                        myProcess.StartInfo.CreateNoWindow = False
    '                        myProcess.StartInfo.FileName = Preferences.applicationPath & "\Assets\ffmpeg.exe"
    '                        Dim proc_arguments As String = "-y -i """ & fullpathandfilename & """ -f mjpeg -ss " & seconds.ToString & " -vframes 1 -an " & """" & SavePath & """"
    '                        myProcess.StartInfo.Arguments = proc_arguments
    '                        myProcess.Start()
    '                        myProcess.WaitForExit()
    '                        status = "done"
    '                    Catch ex As Exception

    '                    End Try
    '                End If
    '            End If
    '        End If
    '    Catch
    '    Finally
    '    End Try
    'End Sub

    Private Sub episodescraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim errorcounter As Integer = 0

        newEpisodeList.Clear()
        Dim newtvfolders As New List(Of String)
        Dim progress As Integer
        progress = 0

        Dim dirpath As String = String.Empty

        ConsoleOrLog("")
        ConsoleOrLog("")
        ConsoleOrLog("Starting TV Folder Scan")


        For Each tvshow In basictvlist
            If tvshow.locked = Nothing Then
                tvshow.locked = 0
            End If
            If tvshow.language = Nothing Then
                tvshow.language = "en"
            End If
            If tvshow.sortorder = Nothing Then
                tvshow.sortorder = "default"
            End If
        Next

        For Each tvfolder In listofshowfolders
            Dim add As Boolean = True
            For Each tvshow In basictvlist
                If tvshow.fullpath.IndexOf(tvfolder) <> -1 Then
                    If tvshow.locked = True Or tvshow.locked = 2 Then
                        If manual = False Then
                            add = False
                            Exit For
                        End If
                    End If
                End If
            Next
            If add = True Then
                tvfolder = IO.Path.GetDirectoryName(tvfolder)
                tempstring = "" 'tvfolder
                Dim hg As New IO.DirectoryInfo(tvfolder)
                If hg.Exists Then
'                    ConsoleOrLog("found " & hg.FullName.ToString)
                    newtvfolders.Add(tvfolder)

                    Try
                        For Each strfolder As String In My.Computer.FileSystem.GetDirectories(tvfolder)
                            Try
                                If strfolder.IndexOf("System Volume Information") = -1 Then
                                    newtvfolders.Add(strfolder)
                                    For Each strfolder2 As String In My.Computer.FileSystem.GetDirectories(strfolder, FileIO.SearchOption.SearchAllSubDirectories)
                                        Try
                                            If strfolder2.IndexOf("System Volume Information") = -1 Then
                                                newtvfolders.Add(strfolder2)
                                            End If
                                        Catch ex As Exception
                                            MsgBox(ex.Message)
                                        End Try
                                    Next
                                End If
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try
                        Next


                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                End If
            Else
                ConsoleOrLog(vbCrLf & "Show Locked, Ignoring: " & tvfolder)
            End If
        Next
        Dim mediacounter As Integer = newEpisodeList.Count
        For g = 0 To newtvfolders.Count - 1
            dirpath = newtvfolders(g)
            Dim dir_info As New System.IO.DirectoryInfo(dirpath)
            If (dir_info.FullName.EndsWith(".actors")) Then Continue For
            findnewepisodes(dirpath)
            tempint = newEpisodeList.Count - mediacounter
            If tempint > 0 Then
                ConsoleOrLog(tempint.ToString & " New episodes found in directory:- " & dirpath)
            End If
            mediacounter = newEpisodeList.Count
        Next g

        If newEpisodeList.Count <= 0 Then
            ConsoleOrLog("No new episodes found, exiting scraper")
            Exit Sub
        End If

        DoneAEp = True
        Dim S As String = ""
        For Each newepisode In newEpisodeList
            S = ""
            newepisodetoadd.episodeno = ""
            newepisodetoadd.episodepath = ""
            'newepisodetoadd.status = ""
            newepisodetoadd.showid = ""
            newepisodetoadd.playcount = ""
            newepisodetoadd.rating = ""
            newepisodetoadd.seasonno = ""
            newepisodetoadd.title = ""

            Dim episode As New episodeinfo

            For Each Regexs In Preferences.tv_RegexScraper
                S = newepisode.episodepath '.ToLower
                S = S.Replace("x264", "")
                S = S.Replace("720p", "")
                S = S.Replace("720i", "")
                S = S.Replace("1080p", "")
                S = S.Replace("1080i", "")
                S = S.Replace("X264", "")
                S = S.Replace("720P", "")
                S = S.Replace("720I", "")
                S = S.Replace("1080P", "")
                S = S.Replace("1080I", "")
                Dim M As Match
                M = Regex.Match(S, Regexs)
                If M.Success = True Then
                    Try
                        newepisode.seasonno = M.Groups(1).Value.ToString
                        newepisode.episodeno = M.Groups(2).Value.ToString
                        If newepisode.seasonno <> "-1" And newepisode.episodeno <> "-1" Then
                            Dim file As String = newepisode.episodepath
                            Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(file)
                            ConsoleOrLog("Season and Episode information found for : " & fileName)
                        Else
                            ConsoleOrLog("Cant extract Season and Episode deatails from filename: " & newepisode.seasonno & "x" & newepisode.episodeno)
                        End If
                        Try
                            newepisode.fanartpath = S.Substring(M.Groups(2).Index + M.Groups(2).Value.Length, S.Length - (M.Groups(2).Index + M.Groups(2).Value.Length))
                        Catch
                        End Try
                        Exit For
                    Catch
                        newepisode.seasonno = "-1"
                        newepisode.episodeno = "-1"
                    End Try
                End If
            Next
            If newepisode.seasonno = Nothing Then newepisode.seasonno = "-1"
            If newepisode.episodeno = Nothing Then newepisode.episodeno = "-1"
        Next
        Dim savepath As String = ""
        Dim scrapedok As Boolean
        For Each eps In newEpisodeList

            Dim episodearray As New List(Of episodeinfo)
            episodearray.Clear()
            Dim multieps2 As New episodeinfo
            multieps2.seasonno = eps.seasonno
            multieps2.episodeno = eps.episodeno
            multieps2.episodepath = eps.episodepath
            multieps2.mediaextension = eps.mediaextension
            multieps2.extension = eps.extension 
            episodearray.Add(multieps2)
            ConsoleOrLog(vbCrLf & "Working on episode: " & eps.episodepath)

            Dim removal As String = ""
            If eps.seasonno = "-1" Or eps.episodeno = "-1" Then
                eps.title = getfilename(eps.episodepath)
                eps.rating = "0"
                eps.playcount = "0"
                eps.uniqueid = ""
                eps.genre = "Unknown Episode Season and/or Episode Number"
                eps.filedetails = get_hdtags(eps.mediaextension)
                episodearray.Add(eps)
                savepath = episodearray(0).episodepath
            Else

                Dim temppath As String = eps.episodepath
                'check for multiepisode files
                Dim M2 As Match
                Dim epcount As Integer = 0
                Dim multiepisode As Boolean = False
                Dim allepisodes(100) As Integer
                S = eps.fanartpath
                eps.fanartpath = ""
                Do
                    M2 = Regex.Match(S, "(([EeXx])([\d]{1,4}))")
                    If M2.Success = True Then
                        Dim skip As Boolean = False
                        For Each epso In episodearray
                            If epso.episodeno = M2.Groups(3).Value Then skip = True
                        Next
                        If skip = False Then
                            Dim multieps As New episodeinfo
                            multieps.seasonno = eps.seasonno
                            multieps.episodeno = M2.Groups(3).Value
                            multieps.episodepath = eps.episodepath
                            multieps.mediaextension = eps.mediaextension
                            multieps.extension = eps.extension 
                            episodearray.Add(multieps)
                            allepisodes(epcount) = Convert.ToDecimal(M2.Groups(3).Value)
                        End If
                        Try
                            S = S.Substring(M2.Groups(3).Index + M2.Groups(3).Value.Length, S.Length - (M2.Groups(3).Index + M2.Groups(3).Value.Length))
                        Catch
                        End Try
                    End If
                Loop Until M2.Success = False
                Dim language As String = ""
                Dim sortorder As String = ""
                Dim tvdbid As String = ""
                Dim imdbid As String = ""
                Dim actorsource As String = ""
                Dim realshowpath As String = ""

                savepath = episodearray(0).episodepath

                For Each Shows In basictvlist
                    If episodearray(0).episodepath.IndexOf(Shows.fullpath.Replace("tvshow.nfo", "")) <> -1 Then
                        language = Shows.language
                        sortorder = Shows.sortorder
                        tvdbid = Shows.id
                        imdbid = Shows.imdbid
                        realshowpath = Shows.fullpath
                        actorsource = Shows.episodeactorsource
                        Exit For
                    End If
                Next
                If episodearray.Count > 1 Then
                    ConsoleOrLog("Multipart episode found: ")
                    ConsoleOrLog("Season: " & episodearray(0).seasonno & " Episodes, ")
                    For Each ep In episodearray
                        Console.Write(ep.episodeno & ", ")
                    Next
                End If
                ConsoleOrLog("Looking up scraper options from tvshow.nfo")

                For Each singleepisode In episodearray
                    If singleepisode.seasonno.Length > 0 Or singleepisode.seasonno.IndexOf("0") = 0 Then
                        Do Until singleepisode.seasonno.IndexOf("0") <> 0 Or singleepisode.seasonno.Length = 1
                            singleepisode.seasonno = singleepisode.seasonno.Substring(1, singleepisode.seasonno.Length - 1)
                        Loop
                        If singleepisode.episodeno = "00" Then
                            singleepisode.episodeno = "0"
                        End If
                        If singleepisode.episodeno <> "0" Then
                            Do Until singleepisode.episodeno.IndexOf("0") <> 0
                                singleepisode.episodeno = singleepisode.episodeno.Substring(1, singleepisode.episodeno.Length - 1)
                            Loop
                        End If
                    End If
                    singleepisode.showid = tvdbid
                    Dim episodescraper As New tvdbscraper
                    If sortorder = "" Then sortorder = "default"
                    Dim tempsortorder As String = sortorder
                    If language = "" Then language = "en"
                    If actorsource = "" Then actorsource = "tvdb"
                    If tvdbid <> "" Then
                        Dim episodeurl As String = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                        If Not Utilities.UrlIsValid(episodeurl) Then
                            If sortorder.ToLower = "dvd" Then
                                tempsortorder = "default"
                                ConsoleOrLog("This episode could not be found on TVDB using DVD sort order")
                                ConsoleOrLog("Attempting to find using default sort order")
                                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                            End If
                        End If

                        If Utilities.UrlIsValid(episodeurl) Then


                            Dim tempepisode As String = episodescraper.getepisode(tvdbid, tempsortorder, singleepisode.seasonno, singleepisode.episodeno, language)
                            scrapedok = True
                            If tempepisode = Nothing Then
                                scrapedok = False
                                ConsoleOrLog("This episode could not be found on TVDB")
                            End If
                            If scrapedok = True Then
                                Dim scrapedepisode As New XmlDocument
                                Try
                                    ConsoleOrLog("Scraping body of episode: " & singleepisode.episodeno)
                                    scrapedepisode.LoadXml(tempepisode)
                                    Dim thisresult As XmlNode = Nothing
                                    For Each thisresult In scrapedepisode("episodedetails")
                                        Select Case thisresult.Name
                                            Case "title"
                                                singleepisode.title = thisresult.InnerText
                                            Case "premiered"
                                                singleepisode.aired = thisresult.InnerText
                                            Case "plot"
                                                singleepisode.plot = thisresult.InnerText
                                            Case "director"
                                                Dim newstring As String
                                                newstring = thisresult.InnerText
                                                'newstring = newstring.TrimEnd("|")
                                                'newstring = newstring.TrimStart("|")
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.director = newstring
                                            Case "credits"
                                                Dim newstring As String
                                                newstring = thisresult.InnerText
                                                'newstring = newstring.TrimEnd("|")
                                                'newstring = newstring.TrimStart("|")
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.credits = newstring
                                            Case "rating"
                                                singleepisode.rating = thisresult.InnerText
                                            Case "uniqueid"
                                                singleepisode.uniqueid = thisresult.InnerText 
                                            Case "thumb"
                                                singleepisode.thumb = thisresult.InnerText
                                            Case "actor"
                                                Dim actors As XmlNode = Nothing
                                                For Each actorl In thisresult.ChildNodes
                                                    Select Case actorl.name
                                                        Case "name"
                                                            Dim newactor As New str_MovieActors
                                                            newactor.actorname = actorl.innertext
                                                            singleepisode.listactors.Add(newactor)
                                                    End Select
                                                Next
                                        End Select
                                    Next
                                    singleepisode.playcount = "0"
                                Catch ex As Exception
                                    ConsoleOrLog("Error scraping episode body, " & ex.Message.ToString)
                                End Try

                                If actorsource = "imdb" Then
                                    ConsoleOrLog("Scraping actors from IMDB")
                                    Dim url As String
                                    url = "http://www.imdb.com/title/" & imdbid & "/episodes"
                                    Dim tvfblinecount As Integer = 0
                                    Dim tvdbwebsource(10000)
                                    tvfblinecount = 0
                                    Try
                                        Dim wrGETURL As WebRequest
                                        wrGETURL = WebRequest.Create(url)
                                        Dim myProxy As New WebProxy("myproxy", 80)
                                        myProxy.BypassProxyOnLocal = True
                                        Dim objStream As Stream
                                        objStream = wrGETURL.GetResponse.GetResponseStream()
                                        Dim objReader As New StreamReader(objStream)
                                        Dim tvdbsLine As String = ""
                                        tvfblinecount = 0

                                        Do While Not tvdbsLine Is Nothing
                                            tvfblinecount += 1
                                            tvdbsLine = objReader.ReadLine
                                            If Not tvdbsLine Is Nothing Then
                                                tvdbwebsource(tvfblinecount) = tvdbsLine
                                            End If
                                        Loop
                                        objReader.Close()
                                        tvfblinecount -= 1
                                    Catch ex As WebException
                                        tvdbwebsource(0) = "404"
                                    End Try

                                    If tvfblinecount <> 0 Then
                                        Dim tvtempstring As String
                                        tvtempstring = "Season " & singleepisode.seasonno & ", Episode " & singleepisode.episodeno & ":"
                                        For g = 1 To tvfblinecount
                                            If tvdbwebsource(g).indexof(tvtempstring) <> -1 Then
                                                Dim tvtempint As Integer
                                                tvtempint = tvdbwebsource(g).indexof("<a href=""/title/")
                                                If tvtempint <> -1 Then
                                                    tvtempstring = tvdbwebsource(g).substring(tvtempint + 16, 9)
                                                    Dim scraperfunction As New Classimdb
                                                    Dim actorlist As String = ""
                                                    actorlist = scraperfunction.getimdbactors(Preferences.imdbmirror, tvtempstring)
                                                    Dim tempactorlist As New List(Of str_MovieActors)
                                                    Dim thumbstring As New XmlDocument
                                                    Dim thisresult As XmlNode = Nothing
                                                    Try
                                                        thumbstring.LoadXml(actorlist)
                                                        thisresult = Nothing
                                                        Dim countactors As Integer = 0
                                                        For Each thisresult In thumbstring("actorlist")
                                                            Select Case thisresult.Name
                                                                Case "actor"
                                                                    If countactors >= Preferences.maxactors Then
                                                                        Exit For
                                                                    End If
                                                                    countactors += 1
                                                                    Dim newactor As New str_MovieActors
                                                                    Dim detail As XmlNode = Nothing
                                                                    For Each detail In thisresult.ChildNodes
                                                                        Select Case detail.Name
                                                                            Case "name"
                                                                                newactor.actorname = detail.InnerText
                                                                            Case "role"
                                                                                newactor.actorrole = detail.InnerText
                                                                            Case "thumb"
                                                                                newactor.actorthumb = detail.InnerText
                                                                            Case "actorid"
                                                                                If newactor.actorthumb <> Nothing Then
                                                                                    If Preferences.actorseasy = True And detail.InnerText <> "" Then
                                                                                        Dim workingpath As String = episodearray(0).episodepath.Replace(IO.Path.GetFileName(episodearray(0).episodepath), "")
                                                                                        workingpath = workingpath & ".actors\"
                                                                                        Dim hg As New IO.DirectoryInfo(workingpath)
                                                                                        Dim destsorted As Boolean = False
                                                                                        If Not hg.Exists Then
                                                                                            Try
                                                                                                IO.Directory.CreateDirectory(workingpath)
                                                                                                destsorted = True
                                                                                            Catch ex As Exception

                                                                                            End Try
                                                                                        Else
                                                                                            destsorted = True
                                                                                        End If
                                                                                        If destsorted = True Then
                                                                                            Dim filename As String = newactor.actorname.Replace(" ", "_")
                                                                                            filename = filename & ".tbn"
                                                                                            Dim tvshowactorpath As String = realshowpath
                                                                                            tvshowactorpath = tvshowactorpath.Replace(IO.Path.GetFileName(tvshowactorpath), "")
                                                                                            tvshowactorpath = IO.Path.Combine(tvshowactorpath, ".actors\")
                                                                                            tvshowactorpath = IO.Path.Combine(tvshowactorpath, filename)
                                                                                            filename = IO.Path.Combine(workingpath, filename)
                                                                                            If IO.File.Exists(tvshowactorpath) Then
                                                                                                Try
                                                                                                    IO.File.Copy(tvshowactorpath, filename, True)
                                                                                                Catch
                                                                                                End Try
                                                                                            End If
                                                                                            If Not IO.File.Exists(filename) Then
                                                                                                Dim buffer(4000000) As Byte
                                                                                                Dim size As Integer = 0
                                                                                                Dim bytesRead As Integer = 0
                                                                                                Dim thumburl As String = newactor.actorthumb
                                                                                                Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                                                                                Dim res As HttpWebResponse = req.GetResponse()
                                                                                                Dim contents As Stream = res.GetResponseStream()
                                                                                                Dim bytesToRead As Integer = CInt(buffer.Length)
                                                                                                While bytesToRead > 0
                                                                                                    size = contents.Read(buffer, bytesRead, bytesToRead)
                                                                                                    If size = 0 Then Exit While
                                                                                                    bytesToRead -= size
                                                                                                    bytesRead += size
                                                                                                End While

                                                                                                Dim fstrm As New FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write)
                                                                                                fstrm.Write(buffer, 0, bytesRead)
                                                                                                contents.Close()
                                                                                                fstrm.Close()
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                    If Preferences.actorsave = True And detail.InnerText <> "" And Preferences.actorseasy = False Then
                                                                                        Dim workingpath As String = ""
                                                                                        Dim networkpath As String = Preferences.actorsavepath
                                                                                        Try
                                                                                            tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                                                                            Dim hg As New IO.DirectoryInfo(tempstring)
                                                                                            If Not hg.Exists Then
                                                                                                IO.Directory.CreateDirectory(tempstring)
                                                                                            End If
                                                                                            workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                                                            If Not IO.File.Exists(workingpath) Then
                                                                                                Dim buffer(4000000) As Byte
                                                                                                Dim size As Integer = 0
                                                                                                Dim bytesRead As Integer = 0
                                                                                                Dim thumburl As String = newactor.actorthumb
                                                                                                Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                                                                                Dim res As HttpWebResponse = req.GetResponse()
                                                                                                Dim contents As Stream = res.GetResponseStream()
                                                                                                Dim bytesToRead As Integer = CInt(buffer.Length)
                                                                                                While bytesToRead > 0
                                                                                                    size = contents.Read(buffer, bytesRead, bytesToRead)
                                                                                                    If size = 0 Then Exit While
                                                                                                    bytesToRead -= size
                                                                                                    bytesRead += size
                                                                                                End While
                                                                                                Dim fstrm As New FileStream(workingpath, FileMode.OpenOrCreate, FileAccess.Write)
                                                                                                fstrm.Write(buffer, 0, bytesRead)
                                                                                                contents.Close()
                                                                                                fstrm.Close()
                                                                                            End If
                                                                                            newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                                                            If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                                                                newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                                                            Else
                                                                                                newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                                                            End If
                                                                                        Catch
                                                                                        End Try
                                                                                    End If
                                                                                End If
                                                                        End Select
                                                                    Next
                                                                    tempactorlist.Add(newactor)
                                                            End Select
                                                        Next
                                                    Catch ex As Exception
                                                        ConsoleOrLog("Error scraping episode actors from IMDB, " & ex.Message.ToString)
                                                    End Try

                                                    If tempactorlist.Count > 0 Then
                                                        ConsoleOrLog("Actors scraped from IMDB OK")
                                                        While tempactorlist.Count > Preferences.maxactors
                                                            tempactorlist.RemoveAt(tempactorlist.Count - 1)
                                                        End While
                                                        singleepisode.listactors.Clear()
                                                        For Each actor In tempactorlist
                                                            singleepisode.listactors.Add(actor)
                                                        Next
                                                        tempactorlist.Clear()
                                                    Else
                                                        ConsoleOrLog("Actors not scraped from IMDB, reverting to TVDB actorlist")
                                                    End If

                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                            If Preferences.enablehdtags = True Then
                                Try
                                    singleepisode.filedetails = get_hdtags(getfilename(singleepisode.episodepath))
                                    If Not singleepisode.filedetails.filedetails_video.duration Is Nothing Then
                                        Dim minutes As Integer
                                        tempstring = singleepisode.filedetails.filedetails_video.duration
                                        If Not String.IsNullOrEmpty(tempstring) Then
                                            minutes =Math.Round(Convert.ToInt32(tempstring)/60)
                                            singleepisode.runtime = minutes.ToString & " min"
                                        Else
                                            singleepisode.runtime = ""
                                        End If
                                    End If
                                Catch
                                End Try
                            End If
                        Else
                            ConsoleOrLog("Could not locate this episode on TVDB, or TVDB may be unavailable")
                        End If
                    Else
                        ConsoleOrLog("No TVDB ID is available for this show, please scrape the show using the ""TV Show Selector"" TAB")
                    End If

                Next

            End If

            If savepath <> "" And scrapedok = True Then
                Call addepisode(episodearray, savepath)
                '9999999
            End If
        Next
    End Sub

    Public Sub saveepisodenfo(ByVal listofepisodes As List(Of episodeinfo), ByVal path As String, Optional ByVal seasonno As String = "-2", Optional ByVal episodeno As String = "-2")
        'Monitor.Enter(Me)
        'Try
        If seasonno <> -2 And episodeno <> -2 Then
            Dim timetoexit As Boolean = False
            For Each show In basictvlist
                If show.fullpath = path Then
                    For Each episode In show.allepisodes
                        If episode.episodeno = episodeno And episode.seasonno = seasonno Then
                            For Each epis In listofepisodes
                                If epis.seasonno = seasonno And epis.episodeno = episodeno Then
                                    Dim newep As New episodeinfo
                                    newep.episodepath = epis.episodepath
                                    newep.title = epis.title
                                    newep.seasonno = epis.seasonno
                                    newep.episodeno = epis.episodeno
                                    newep.playcount = epis.playcount
                                    newep.rating = epis.rating
                                    show.allepisodes.Remove(episode)
                                    show.allepisodes.Add(newep)
                                    timetoexit = True
                                    Exit For
                                End If

                            Next
                        End If
                        If timetoexit = True Then Exit For
                    Next
                End If
                If timetoexit = True Then Exit For
            Next
        End If

        Dim doc As New XmlDocument
        Dim root As XmlElement
        Dim xmlEpisode As XmlElement
        Dim xmlEpisodechild As XmlElement
        Dim xmlStreamDetails As XmlElement
        Dim xmlFileInfo As XmlElement
        Dim xmlStreamDetailsType As XmlElement
        Dim xmlStreamDetailsTypeChild As XmlElement 
        Dim xmlActor As XmlElement
        Dim xmlActorchild As XmlElement
        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        root = doc.CreateElement("multiepisodenfo")
        For Each ep In listofepisodes
            xmlEpisode = doc.CreateElement("episodedetails")
            If Preferences.enabletvhdtags = True Then
                Try
                    xmlFileInfo = doc.CreateElement("fileinfo")
                    xmlStreamDetails = doc.CreateElement("streamdetails")
                    xmlStreamDetailsType = doc.CreateElement("video")
                    If ep.filedetails.filedetails_video.width <> Nothing Then
                        If ep.filedetails.filedetails_video.width <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("width")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.width
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.height <> Nothing Then
                        If ep.filedetails.filedetails_video.height <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("height")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.height
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.aspect <> Nothing Then
                        If ep.filedetails.filedetails_video.aspect <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("aspect")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.aspect
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.codec <> Nothing Then
                        If ep.filedetails.filedetails_video.codec <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("codec")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.codec
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.formatinfo <> Nothing Then
                        If ep.filedetails.filedetails_video.formatinfo <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("format")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.formatinfo
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.duration <> Nothing Then
                        If ep.filedetails.filedetails_video.duration <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("durationinseconds")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.duration
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.bitrate <> Nothing Then
                        If ep.filedetails.filedetails_video.bitrate <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("bitrate")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.bitrate
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.bitratemode <> Nothing Then
                        If ep.filedetails.filedetails_video.bitratemode <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("bitratemode")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.bitratemode
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.bitratemax <> Nothing Then
                        If ep.filedetails.filedetails_video.bitratemax <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("bitratemax")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.bitratemax
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.container <> Nothing Then
                        If ep.filedetails.filedetails_video.container <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("container")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.container
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.codecid <> Nothing Then
                        If ep.filedetails.filedetails_video.codecid <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("codecid")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.codecid
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.codecinfo <> Nothing Then
                        If ep.filedetails.filedetails_video.codecinfo <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("codecidinfo")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.codecinfo
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    If ep.filedetails.filedetails_video.scantype <> Nothing Then
                        If ep.filedetails.filedetails_video.scantype <> "" Then
                            xmlStreamDetailsTypechild = doc.CreateElement("scantype")
                            xmlStreamDetailsTypechild.InnerText = ep.filedetails.filedetails_video.scantype
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                        End If
                    End If
                    xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                    If ep.filedetails.filedetails_audio.Count > 0 Then
                        For Each item In ep.filedetails.filedetails_audio

                            xmlStreamDetailsType = doc.CreateElement("audio")
                            If item.language <> Nothing Then
                                If item.language <> "" Then
                                    xmlStreamDetailsTypechild = doc.CreateElement("language")
                                    xmlStreamDetailsTypechild.InnerText = item.language
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                                End If
                            End If
                            If item.codec <> Nothing Then
                                If item.codec <> "" Then
                                    xmlStreamDetailsTypechild = doc.CreateElement("codec")
                                    xmlStreamDetailsTypechild.InnerText = item.codec
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                                End If
                            End If
                            If item.channels <> Nothing Then
                                If item.channels <> "" Then
                                    xmlStreamDetailsTypechild = doc.CreateElement("channels")
                                    xmlStreamDetailsTypechild.InnerText = item.channels
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                                End If
                            End If
                            If item.bitrate <> Nothing Then
                                If item.bitrate <> "" Then
                                    xmlStreamDetailsTypechild = doc.CreateElement("bitrate")
                                    xmlStreamDetailsTypechild.InnerText = item.bitrate
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                                End If
                            End If
                            xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                        Next
                    End If
                    If ep.filedetails.filedetails_subtitles.Count > 0 Then
                        xmlStreamDetailsType = doc.CreateElement("subtitle")
                        For Each entry In ep.filedetails.filedetails_subtitles
                            If entry.language <> Nothing Then
                                If entry.language <> "" Then
                                    xmlStreamDetailsTypechild = doc.CreateElement("language")
                                    xmlStreamDetailsTypechild.InnerText = entry.language
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypechild)
                                End If
                            End If
                            xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                        Next
                    End If
                    xmlFileInfo.AppendChild(xmlStreamDetails)
                    xmlEpisode.AppendChild(xmlFileInfo)
                Catch
                End Try
            End If

            xmlEpisodechild = doc.CreateElement("title")
            xmlEpisodechild.InnerText = ep.title
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("season")
            xmlEpisodechild.InnerText = ep.seasonno
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("episode")
            xmlEpisodechild.InnerText = ep.episodeno
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("aired")
            xmlEpisodechild.InnerText = ep.aired
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("plot")
            xmlEpisodechild.InnerText = ep.plot
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("playcount")
            xmlEpisodechild.InnerText = ep.playcount
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("director")
            xmlEpisodechild.InnerText = ep.director
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("credits")
            xmlEpisodechild.InnerText = ep.credits
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("rating")
            xmlEpisodechild.InnerText = ep.rating
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("uniqueid")
            xmlEpisodechild.InnerText = ep.uniqueid
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("runtime")
            xmlEpisodechild.InnerText = ep.runtime
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = doc.CreateElement("showid")
            xmlEpisodechild.InnerText = ep.showid
            xmlEpisode.AppendChild(xmlEpisodechild)

            Dim actorstosave As Integer = ep.listactors.Count
            If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
            For f = 0 To actorstosave - 1
                xmlActor = doc.CreateElement("actor")
                xmlActorchild = doc.CreateElement("name")
                xmlActorchild.InnerText = ep.listactors(f).actorname
                xmlActor.AppendChild(xmlActorchild)
                xmlActorchild = doc.CreateElement("role")
                xmlActorchild.InnerText = ep.listactors(f).actorrole
                xmlActor.AppendChild(xmlActorchild)
                If ep.listactors(f).actorthumb <> Nothing Then
                    If ep.listactors(f).actorthumb <> "" Then
                        xmlActorchild = doc.CreateElement("thumb")
                        xmlActorchild.InnerText = ep.listactors(f).actorthumb
                        xmlActor.AppendChild(xmlActorchild)
                    End If
                End If
                xmlEpisode.AppendChild(xmlActor)
            Next
            If listofepisodes.Count = 1 Then    'file is a single episode
                root = xmlEpisode               'root now equals 'episodedetails'
                Exit For                        'now append to XML doc as a single episode
            End If
            root.AppendChild(xmlEpisode)        'otherwise, each episode is appended to the 'multiepisode' element
        Next

        Try
            doc.AppendChild(root)
            Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented

            doc.WriteTo(output)
            output.Close()
        Catch
        End Try
    End Sub

    Private Sub addepisode(ByVal alleps As List(Of episodeinfo), ByVal path As String)
        ConsoleOrLog("Saving episode")

        If Preferences.autorenameepisodes = True Then
            For Each show In basictvlist
                If alleps(0).episodepath.IndexOf(show.fullpath.Replace("\tvshow.nfo", "")) <> -1 Then
                    Dim eps As New List(Of String)
                    eps.Clear()
                    For Each ep In alleps
                        eps.Add(ep.episodeno)
                    Next
                    Preferences.tvScraperLog = String.Empty
                    Dim tempspath As String = TVShows.episodeRename(path, alleps(0).seasonno, eps, show.title, alleps(0).title)
                    Console.Write(Preferences.tvScraperLog.Replace("!!! ", ""))
                    If tempspath <> "false" Then
                        path = tempspath
                    End If
                End If
            Next
        End If

        Call saveepisodenfo(alleps, path)
        For Each Shows In basictvlist
            If alleps(0).episodepath.IndexOf(Shows.fullpath.Replace("\tvshow.nfo", "")) <> -1 Then

                For Each ep In alleps
                    Dim newwp As New episodeinfo
                    newwp.episodeno = ep.episodeno
                    newwp.episodepath = path
                    newwp.seasonno = ep.seasonno
                    newwp.title = ep.title
                    newwp.missing = "False"
                    newwp.aired = ep.aired
                    newwp.showid = Shows.id
                    newwp.uniqueid = ep.uniqueid 
                    newwp.extension = ep.extension
                    newwp.playcount = ep.playcount 
                    Shows.allepisodes.Add(newwp)
                Next
            End If
        Next

        Dim eden As Boolean = Preferences.EdenEnabled
        Dim frodo As Boolean = Preferences.FrodoEnabled
        Dim paths As New List(Of String)
        If eden Then paths.Add(path.Replace(IO.Path.GetExtension(path), ".tbn"))
        If frodo Then paths.Add(path.Replace(IO.Path.GetExtension(path), "-thumb.jpg"))
        Dim url As String = alleps(0).thumb
        Dim ok As Boolean = False
        If Not url = Nothing AndAlso url <> "http://www.thetvdb.com/banners/" Then  ' And Not edenart And Not frodoart Then
            ok = DownloadCache.SaveImageToCacheAndPaths(url, paths, True, 0, 0, True)
            If ok Then
                ConsoleOrLog("Thumbnail downloaded successfully")
            Else
                ConsoleOrLog("Failed to download Thumbnail")
            End If
        End If
        If Not ok AndAlso Preferences.autoepisodescreenshot Then
            ConsoleOrLog("No Episode Thumb, AutoCreating ScreenShot from Episode file")
            Dim aok As Boolean = Utilities.CreateScreenShot(alleps(0).mediaextension, paths(0), Preferences.ScrShtDelay)
            'Call screenshot(alleps(0).mediaextension, paths(0))
            If aok AndAlso paths.Count > 1 Then
                File.Copy(paths(0), paths(1))
                ConsoleOrLog("Screenshot Saved O.K.")
            End If
            If Not aok Then
                ConsoleOrLog("Screenshot save Failed!")
            End If
        End If
    End Sub

    Private Sub findnewepisodes(ByVal path As String)
        Dim episode As New List(Of episodeinfo)
        Dim propfile As Boolean = False
        Dim allok As Boolean = False
        Dim dir_info As New System.IO.DirectoryInfo(path)
        Dim fs_infos As List(Of System.IO.FileInfo) = dir_info.GetFiles().ToList
        Dim counter As Integer = 1
        Dim counter2 As Integer = 1
        Dim filteredFiles As List(Of IO.FileInfo) = fs_infos.Where(AddressOf IsMediaExtension).ToList
        For Each fs_info As System.IO.FileInfo In filteredFiles
            Try
                Dim filename As String = IO.Path.Combine(path, fs_info.Name)
                Dim filename2 As String = filename.Replace(IO.Path.GetExtension(filename), ".nfo")

                If Not IO.File.Exists(filename2) Then
                    Dim add As Boolean = True
                    If fs_info.Extension = ".vob" Then 'If a vob file is detected, check that it is not part of a dvd file structure
                        Dim name As String = filename2
                        name = name.Replace(IO.Path.GetFileName(name), "VIDEO_TS.IFO")
                        If IO.File.Exists(name) Then
                            add = False
                        End If
                    End If
                    If fs_info.Extension = ".rar" Then
                        Dim tempmovie As String = String.Empty
                        Dim tempint2 As Integer
                        Dim tempmovie2 As String = fs_info.FullName
                        If IO.Path.GetExtension(tempmovie2).ToLower = ".rar" Then
                            If IO.File.Exists(tempmovie2) = True Then
                                If IO.File.Exists(tempmovie) = False Then
                                    Dim rarname As String = tempmovie2
                                    Dim SizeOfFile As Integer
                                    tempint2 = Convert.ToInt32(Preferences.rarsize) * 1048576
                                    SizeOfFile = FileLen(rarname)
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
                    If add = True Then
                        Dim newep As New episodeinfo
                        newep.episodepath = filename2
                        newep.mediaextension = filename
                        newep.extension = IO.Path.GetExtension(filename)
                        newEpisodeList.Add(newep)
                    End If
                End If
            Catch ex As Exception

            End Try

        Next fs_info

        fs_infos = Nothing
    End Sub

    Private Sub tvcacheLoad()
        Dim unsortedepisodelist As New List(Of episodeinfo)
        unsortedepisodelist.Clear()
        basictvlist.Clear()

        Dim tvlist As New XmlDocument
        tvlist.Load(Preferences.workingProfile.tvcache)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In tvlist("tvcache")
            Select Case thisresult.Name
                Case "tvshow"
                    Dim newtvshow As New basictvshownfo
                    If (thisresult.Attributes.Count > 0) Then
                        newtvshow.fullpath = thisresult.Attributes(0).Value
                        If IO.File.Exists(newtvshow.fullpath) Then
                            Dim detail As XmlNode = Nothing
                            For Each detail In thisresult.ChildNodes
                                Select Case detail.Name
                                    Case "playcount"
                                        newtvshow.playcount = detail.InnerText
                                    Case "state"
                                        newtvshow.locked = detail.InnerText
                                    Case "title"
                                        Dim tempstring As String = ""
                                        tempstring = detail.InnerText
                                        newtvshow.title = Preferences.RemoveIgnoredArticles(tempstring)
                                    Case "id"
                                        newtvshow.id = detail.InnerText
                                    Case "sortorder"
                                        newtvshow.sortorder = detail.InnerText
                                    Case "language"
                                        newtvshow.language = detail.InnerText
                                    Case "episodeactorsource"
                                        newtvshow.episodeactorsource = detail.InnerText
                                    Case "imdbid"
                                        newtvshow.imdbid = detail.InnerText
                                    Case "fullpathandfilename"
                                        newtvshow.fullpath = detail.InnerText
                                End Select
                            Next
                            If newtvshow.playcount = Nothing Then newtvshow.playcount = "0"
                            basictvlist.Add(newtvshow)
                        End If
                    End If
                Case "episodedetails"
                    Dim newepisode As New episodeinfo
                    If (thisresult.Attributes.Count > 0) Then
                        newepisode.episodepath = thisresult.Attributes(0).Value
                        'It seems that multiple <episodedetails> attributes are no longer used in tvcache, NfoPath is the only one - HueyHQ
                        'newepisode.pure = thisresult.Attributes(1).Value
                        'If DirectCast(thisresult, System.Xml.XmlElement).Attributes.Count = 3 Then newepisode.extension = thisresult.Attributes(2).Value
                        For Each episodenew In thisresult.ChildNodes
                            Select Case episodenew.Name
                                Case "title"
                                    newepisode.title = episodenew.InnerText
                                Case "episodepath"
                                    newepisode.episodepath = episodenew.InnerText
                                Case "season"
                                    newepisode.seasonno = episodenew.InnerText
                                Case "episode"
                                    newepisode.episodeno = episodenew.InnerText
                                Case "showid"
                                    newepisode.showid = episodenew.InnerText
                                Case "uniqueid"
                                    newepisode.uniqueid = episodenew.InnerText
                                Case "aired"
                                    newepisode.aired = episodenew.InnerText
                                Case "missing"
                                    newepisode.missing = episodenew.InnerText
                                Case "playcount"
                                    newepisode.playcount = episodenew.InnerText
                                Case "epextn"
                                    newepisode.extension = episodenew.InnerText
                            End Select
                        Next
                        unsortedepisodelist.Add(newepisode)
                    End If
            End Select
        Next
        For Each show In basictvlist
            'fill in blanks
            Dim tvshowdata As New XmlDocument
            If String.IsNullOrEmpty(show.language) Then      'if user still on old tvcache, fill in blanks from tvshow's nfo.
                tvshowdata.Load(show.fullpath)
                For Each thisresult In tvshowdata("tvshow")
                    Select Case thisresult.Name
                        Case "sortorder"
                            show.sortorder = thisresult.InnerText
                        Case "imdbid"
                            show.imdbid = thisresult.InnerText
                        Case "episodeactorsource"
                            show.episodeactorsource = thisresult.InnerText
                        Case "language"
                            show.language = thisresult.InnerText
                        Case "state"
                            show.locked = thisresult.InnerText
                    End Select
                Next
                If show.playcount = Nothing Then show.playcount = "0"

            End If
            For Each ep In unsortedepisodelist
                If ep.showid = show.id Then
                    show.allepisodes.Add(ep)
                End If
            Next
        Next
    End Sub

    Private Sub tvcacheSave()
        Dim fullpath As String = Preferences.workingProfile.tvcache
        If IO.File.Exists(fullpath) Then
            IO.File.Delete(fullpath)
        End If
        Dim document As New XmlDocument
        Dim root As XmlElement
        Dim child As XmlElement
        Dim childchild As XmlElement
        Dim xmlproc As XmlDeclaration
        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        document.AppendChild(xmlproc)
        root = document.CreateElement("tvcache")
        root.SetAttribute("ver", "3.5")
        For Each item In basictvlist
            child = document.CreateElement("tvshow")
            child.SetAttribute("NfoPath", item.fullpath)
            childchild = document.CreateElement("playcount")          : childchild.InnerText = item.playcount           : child.AppendChild(childchild)
            childchild = document.CreateElement("state")              : childchild.InnerText = item.locked              : child.AppendChild(childchild)
            childchild = document.CreateElement("title")              : childchild.InnerText = item.title               : child.AppendChild(childchild)
            childchild = document.CreateElement("id")                 : childchild.InnerText = item.id                  : child.AppendChild(childchild)
            childchild = document.CreateElement("sortorder")          : childchild.InnerText = item.sortorder           : child.AppendChild(childchild)
            childchild = document.CreateElement("language")           : childchild.InnerText = item.language            : child.AppendChild(childchild)
            childchild = document.CreateElement("episodeactorsource") : childchild.InnerText = item.episodeactorsource  : child.AppendChild(childchild)
            childchild = document.CreateElement("imdbid")             : childchild.InnerText = item.imdbid              : child.AppendChild(childchild)
            root.AppendChild(child)
        Next
        For Each item In basictvlist 
            For Each episode In item.allepisodes
                child = document.CreateElement("episodedetails")
                child.SetAttribute("NfoPath", episode.episodepath)

                childchild = document.CreateElement("missing")      : childchild.InnerText = episode.missing.ToString.ToLower   : child.AppendChild(childchild)
                childchild = document.CreateElement("title")        : childchild.InnerText = episode.Title                      : child.AppendChild(childchild)
                childchild = document.CreateElement("season")       : childchild.InnerText = episode.seasonno                   : child.AppendChild(childchild)
                childchild = document.CreateElement("episode")      : childchild.InnerText = episode.episodeno                  : child.AppendChild(childchild)
                childchild = document.CreateElement("aired")        : childchild.InnerText = episode.Aired                      : child.AppendChild(childchild)
                childchild = document.CreateElement("showid")       : childchild.InnerText = episode.showid                     : child.AppendChild(childchild)
                childchild = document.CreateElement("uniqueid")     : childchild.InnerText = episode.uniqueid                   : child.AppendChild(childchild)
                childchild = document.CreateElement("epextn")       : childchild.InnerText = episode.extension                  : child.AppendChild(childchild)
                childchild = document.CreateElement("playcount")    : childchild.InnerText = episode.playcount                  : child.AppendChild(childchild)
                root.AppendChild(child)
            Next
        Next
        document.AppendChild(root)
        Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        document.WriteTo(output)
        output.Close()
    End Sub

    Private Sub tvcacheClean()
        For Each item In basictvlist 
            Dim epcount As Integer = item.allepisodes.Count-1
            If epcount < 2 Then
                Exit For
            End If
            Dim x = 0
            Dim w As New List(Of Integer)
            Do Until x > epcount
                For y = 0 to item.allepisodes.Count-1
                    If item.allepisodes(x).seasonno = item.allepisodes(y).seasonno AndAlso item.allepisodes(x).episodeno = item.allepisodes(y).episodeno Then 'item.allepisodes(x).title = ep.title 
                        w.Add(y)
                    End If
                Next
                If w.Count > 1 Then
                    item.allepisodes.Insert(x, item.allepisodes(w(w.Count-1)))
                    item.allepisodes.RemoveAt(x+1)
                    If w(0) = x Then w.RemoveAt(0)
                    For y = w.Count-1 to 0 Step -1
                        Dim v = w(y)
                        item.allepisodes.RemoveAt(v)
                        epcount = epcount -1
                    Next 
                End If
                x = x +1
                w.Clear()
            Loop
        Next
        ConsoleOrLog("TvCache Cleaned")
    End Sub

    Private Sub util_RegexLoad()
        Dim tempstring As String
        tempstring = Preferences.workingProfile.regexlist
        Preferences.tv_RegexScraper.Clear()
        Preferences.tv_RegexRename.Clear()
        Dim path As String = tempstring
        If IO.File.Exists(path) Then
            Try
                Dim regexlist As New XmlDocument
                regexlist.Load(path)
                If regexlist.DocumentElement.Name = "regexlist" Then
                    For Each result In regexlist("regexlist")
                        Select Case result.Name
                            Case "tvregex"                              'This is the old tag before custom renamer was introduced,
                                Preferences.tv_RegexScraper.Add(result.InnerText)   'so add it to the scraper regex list in case there are custom regexs.
                            Case "tvregexscrape"
                                Preferences.tv_RegexScraper.Add(result.InnerText)
                            Case "tvregexrename"
                                Preferences.tv_RegexRename.Add(result.InnerText)
                        End Select
                    Next
                End If
            Catch
            End Try
        End If
    End Sub

 
    Public Function getfilename(ByVal path As String)
        'todo getfilename
        Dim monitorobject As New Object
        Monitor.Enter(monitorobject)
        Try
            Dim tempstring As String
            Dim tempfilename As String = path
            Dim actualpathandfilename As String = ""

            If String.IsNullOrEmpty(path) Then Return Nothing

            If IO.File.Exists(tempfilename.Replace(IO.Path.GetFileName(tempfilename), "VIDEO_TS.IFO")) Then
                actualpathandfilename = tempfilename.Replace(IO.Path.GetFileName(tempfilename), "VIDEO_TS.IFO")
            End If

            If actualpathandfilename = "" Then
                For Each extn In MediaFileExtensions
                    tempfilename = tempfilename.Replace(IO.Path.GetExtension(tempfilename), extn)
                    If IO.File.Exists(tempfilename) Then
                        actualpathandfilename = tempfilename
                        Exit For
                    End If
                Next
            End If

            If actualpathandfilename = "" Then
                If tempfilename.IndexOf("movie.nfo") <> -1 Then
                    Dim possiblemovies(1000) As String
                    Dim possiblemoviescount As Integer = 0
                    For Each extn In MediaFileExtensions
                        Dim dirpath As String = tempfilename.Replace(IO.Path.GetFileName(tempfilename), "")
                        Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                        Dim pattern As String = "*" & extn
                        Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(pattern)
                        For Each fs_info As System.IO.FileInfo In fs_infos
                            If IO.File.Exists(fs_info.FullName) Then
                                tempstring = fs_info.FullName.ToLower
                                If tempstring.IndexOf("-trailer") = -1 And tempstring.IndexOf("-sample") = -1 And tempstring.IndexOf(".trailer") = -1 And tempstring.IndexOf(".sample") = -1 Then
                                    possiblemoviescount += 1
                                    possiblemovies(possiblemoviescount) = fs_info.FullName
                                End If
                            End If
                        Next
                    Next
                    If possiblemoviescount = 1 Then
                        actualpathandfilename = possiblemovies(possiblemoviescount)
                    ElseIf possiblemoviescount > 1 Then
                        Dim multistrings(6) As String
                        multistrings(1) = "cd"
                        multistrings(2) = "dvd"
                        multistrings(3) = "part"
                        multistrings(4) = "pt"
                        multistrings(5) = "disk"
                        multistrings(6) = "disc"
                        Dim types(5) As String
                        types(1) = ""
                        types(2) = "-"
                        types(3) = "_"
                        types(4) = " "
                        types(5) = "."
                        Dim workingstring As String
                        For f = 1 To 6
                            For g = 1 To 5
                                For h = 1 To possiblemoviescount
                                    workingstring = multistrings(f) & types(h) & "1"
                                    Dim workingtitle As String = possiblemovies(h).ToLower
                                    If workingtitle.IndexOf(workingstring) <> -1 Then
                                        actualpathandfilename = possiblemovies(h)
                                    End If
                                Next
                            Next
                        Next
                    End If
                End If
            End If

            If actualpathandfilename = "" Then
                actualpathandfilename = "none"
            End If


            Return actualpathandfilename
        Catch
        Finally
            Monitor.Exit(monitorobject)
        End Try
        Return "Error"
    End Function

    Public Function get_hdtags(ByVal filename As String)
        Try
            If IO.Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
                Dim temppath As String = filename.Replace(IO.Path.GetFileName(filename), "VTS_01_0.IFO")
                If IO.File.Exists(temppath) Then
                    filename = temppath
                End If
            End If
            Dim newfiledetails As new FullFileDetails 
            newfiledetails = Preferences.Get_HdTags(filename)
            Dim workingfiledetails As New fullfiledetails2
            workingfiledetails.filedetails_video.width = newfiledetails.filedetails_video.Width.Value 
            workingfiledetails.filedetails_video.height = newfiledetails.filedetails_video.Height.Value
            workingfiledetails.filedetails_video.aspect = newfiledetails.filedetails_video.Aspect.Value
            workingfiledetails.filedetails_video.codec = newfiledetails.filedetails_video.Codec.Value
            workingfiledetails.filedetails_video.formatinfo = newfiledetails.filedetails_video.FormatInfo.Value
            workingfiledetails.filedetails_video.duration = newfiledetails.filedetails_video.DurationInSeconds.Value
            workingfiledetails.filedetails_video.bitrate = newfiledetails.filedetails_video.Bitrate.Value
            workingfiledetails.filedetails_video.bitratemode = newfiledetails.filedetails_video.BitrateMode.Value
            workingfiledetails.filedetails_video.bitratemax = newfiledetails.filedetails_video.BitrateMax.Value
            workingfiledetails.filedetails_video.container = newfiledetails.filedetails_video.Container.Value
            workingfiledetails.filedetails_video.codecinfo = newfiledetails.filedetails_video.CodecInfo.Value
            workingfiledetails.filedetails_video.scantype = newfiledetails.filedetails_video.ScanType.Value
            
            Dim NumAudStream As Integer = newfiledetails.filedetails_audio.Count
            Dim CurAuStr As Integer = 0
            If NumAudStream > 0 Then
                While CurAuStr < NumAudStream 
                    Dim audio As New medianfo_audio
                    audio.language = newfiledetails.filedetails_audio(CurAuStr).Language.value
                    audio.codec = newfiledetails.filedetails_audio(CurAuStr).Codec.value
                    audio.bitrate = newfiledetails.filedetails_audio(CurAuStr).Bitrate.value
                    audio.channels = newfiledetails.filedetails_audio(CurAuStr).Channels.value
                    workingfiledetails.filedetails_audio.Add(audio)
                    CurAuStr += 1
                End While
            Else
                Dim audio As New medianfo_audio
                workingfiledetails.filedetails_audio.Add(audio)
            End If
            
            Dim NumSubStream As Integer = newfiledetails.filedetails_subtitles.Count
            Dim CurSbStr As Integer = 0
            If NumSubStream > 0 Then
                While CurSbStr < NumSubStream 
                    Dim sublang As New medianfo_subtitles
                    sublang.language = newfiledetails.filedetails_subtitles(CurSbStr).Language.value
                    workingfiledetails.filedetails_subtitles.Add(sublang)
                    CurSbStr += 1
                End While
            End If

            Return workingfiledetails
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    Dim MediaFileExtensions As List(Of String) = New List(Of String)

    Private Sub InitMediaFileExtensions()
        For Each extn In Utilities.VideoExtensions
            MediaFileExtensions.Add(extn)
        Next
    End Sub

    Private Function IsMediaExtension(ByVal fileinfo As System.IO.FileInfo) As Boolean
        Dim extension As String = fileinfo.Extension
        Return MediaFileExtensions.Contains(extension.ToLower)
    End Function

    'Obsolete...

    'Public Sub EnumerateDirectories(ByRef directoryList As List(Of String), ByVal root As String)
    '    If (File.GetAttributes(root) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
    '        'ignore 
    '    Else
    '        If Utilities.ValidMovieDir(root) Then

    '            If Not (directoryList.Contains(root)) Then
    '                directoryList.Add(root)
    '                For Each s As String In Directory.GetDirectories(root)
    '                    EnumerateDirectories(directoryList, s)
    '                Next
    '            End If
    '        End If
    '    End If
    'End Sub

    'Public Function getlangcode(ByVal strLang As String) As String
    '    Dim monitorobject As New Object
    '    Monitor.Enter(monitorobject)
    '    Try
    '        Select Case strLang.ToLower
    '            Case "english"
    '                Return "eng"
    '            Case "german"
    '                Return "deu"
    '            Case ""
    '                Return ""
    '            Case "afar"
    '                Return "aar"
    '            Case "abkhazian"
    '                Return "abk"
    '            Case "achinese"
    '                Return "ace"
    '            Case "acoli"
    '                Return "ach"
    '            Case "adangme"
    '                Return "ada"
    '            Case "adyghe", "adygei"
    '                Return "ady"
    '            Case "afro-asiatic (other)"
    '                Return "afa"
    '            Case "afrihili"
    '                Return "afh"
    '            Case "afrikaans"
    '                Return "afr"
    '            Case "ainu"
    '                Return "ain"
    '            Case "akan"
    '                Return "aka"
    '            Case "akkadian"
    '                Return "akk"
    '            Case "albanian"
    '                Return "alb"
    '            Case "aleut"
    '                Return "ale"
    '            Case "algonquian languages"
    '                Return "alg"
    '            Case "southern altai"
    '                Return "alt"
    '            Case "amharic"
    '                Return "amh"
    '            Case "english"
    '                Return "ang"
    '            Case "angika"
    '                Return "anp"
    '            Case "apache languages"
    '                Return "apa"
    '            Case "arabic"
    '                Return "ara"
    '            Case "official aramaic (700-300 bce)", "imperial aramaic (700-300 bce)"
    '                Return "arc"
    '            Case "aragonese"
    '                Return "arg"
    '            Case "armenian"
    '                Return "arm"
    '            Case "mapudungun", "mapuche"
    '                Return "arn"
    '            Case "arapaho"
    '                Return "arp"
    '            Case "artificial (other)"
    '                Return "art"
    '            Case "arawak"
    '                Return "arw"
    '            Case "assamese"
    '                Return "asm"
    '            Case "asturian", "bable", "leonese", "asturleonese"
    '                Return "ast"
    '            Case "athapascan languages"
    '                Return "ath"
    '            Case "australian languages"
    '                Return "aus"
    '            Case "avaric"
    '                Return "ava"
    '            Case "avestan"
    '                Return "ave"
    '            Case "awadhi"
    '                Return "awa"
    '            Case "aymara"
    '                Return "aym"
    '            Case "azerbaijani"
    '                Return "aze"
    '            Case "banda languages"
    '                Return "bad"
    '            Case "bamileke languages"
    '                Return "bai"
    '            Case "bashkir"
    '                Return "bak"
    '            Case "baluchi"
    '                Return "bal"
    '            Case "bambara"
    '                Return "bam"
    '            Case "balinese"
    '                Return "ban"
    '            Case "basque"
    '                Return "baq"
    '            Case "basa"
    '                Return "bas"
    '            Case "baltic (other)"
    '                Return "bat"
    '            Case "beja", "bedawiyet"
    '                Return "bej"
    '            Case "belarusian"
    '                Return "bel"
    '            Case "bemba"
    '                Return "bem"
    '            Case "bengali"
    '                Return "ben"
    '            Case "berber (other)"
    '                Return "ber"
    '            Case "bhojpuri"
    '                Return "bho"
    '            Case "bihari"
    '                Return "bih"
    '            Case "bikol"
    '                Return "bik"
    '            Case "bini", "edo"
    '                Return "bin"
    '            Case "bislama"
    '                Return "bis"
    '            Case "siksika"
    '                Return "bla"
    '            Case "bantu (other)"
    '                Return "bnt"
    '            Case "bosnian"
    '                Return "bos"
    '            Case "braj"
    '                Return "bra"
    '            Case "breton"
    '                Return "bre"
    '            Case "batak languages"
    '                Return "btk"
    '            Case "buriat"
    '                Return "bua"
    '            Case "buginese"
    '                Return "bug"
    '            Case "bulgarian"
    '                Return "bul"
    '            Case "burmese"
    '                Return "bur"
    '            Case "blin", "bilin"
    '                Return "byn"
    '            Case "caddo"
    '                Return "cad"
    '            Case "central american indian (other)"
    '                Return "cai"
    '            Case "galibi carib"
    '                Return "car"
    '            Case "catalan", "valencian"
    '                Return "cat"
    '            Case "caucasian (other)"
    '                Return "cau"
    '            Case "cebuano"
    '                Return "ceb"
    '            Case "celtic (other)"
    '                Return "cel"
    '            Case "chamorro"
    '                Return "cha"
    '            Case "chibcha"
    '                Return "chb"
    '            Case "chechen"
    '                Return "che"
    '            Case "chagatai"
    '                Return "chg"
    '            Case "chinese"
    '                Return "chi"
    '            Case "chuukese"
    '                Return "chk"
    '            Case "mari"
    '                Return "chm"
    '            Case "chinook jargon"
    '                Return "chn"
    '            Case "choctaw"
    '                Return "cho"
    '            Case "chipewyan", "dene suline"
    '                Return "chp"
    '            Case "cherokee"
    '                Return "chr"
    '            Case "church slavic", "old slavonic", "church slavonic", "old bulgarian", "old church slavonic"
    '                Return "chu"
    '            Case "chuvash"
    '                Return "chv"
    '            Case "cheyenne"
    '                Return "chy"
    '            Case "chamic languages"
    '                Return "cmc"
    '            Case "coptic"
    '                Return "cop"
    '            Case "cornish"
    '                Return "cor"
    '            Case "corsican"
    '                Return "cos"
    '            Case "creoles and pidgins"
    '                Return "cpe"
    '            Case "creoles and pidgins"
    '                Return "cpf"
    '            Case "creoles and pidgins"
    '                Return "cpp"
    '            Case "cree"
    '                Return "cre"
    '            Case "crimean tatar", "crimean turkish"
    '                Return "crh"
    '            Case "creoles and pidgins (other)"
    '                Return "crp"
    '            Case "kashubian"
    '                Return "csb"
    '            Case "cushitic (other)"
    '                Return "cus"
    '            Case "czech"
    '                Return "cze"
    '            Case "dakota"
    '                Return "dak"
    '            Case "danish"
    '                Return "dan"
    '            Case "dargwa"
    '                Return "dar"
    '            Case "land dayak languages"
    '                Return "day"
    '            Case "delaware"
    '                Return "del"
    '            Case "slave (athapascan)"
    '                Return "den"
    '            Case "dogrib"
    '                Return "dgr"
    '            Case "dinka"
    '                Return "din"
    '            Case "divehi", "dhivehi", "maldivian"
    '                Return "div"
    '            Case "dogri"
    '                Return "doi"
    '            Case "dravidian (other)"
    '                Return "dra"
    '            Case "lower sorbian"
    '                Return "dsb"
    '            Case "duala"
    '                Return "dua"
    '            Case "dutch"
    '                Return "dum"
    '            Case "dutch", "flemish"
    '                Return "dut"
    '            Case "dyula"
    '                Return "dyu"
    '            Case "dzongkha"
    '                Return "dzo"
    '            Case "efik"
    '                Return "efi"
    '            Case "egyptian (ancient)"
    '                Return "egy"
    '            Case "ekajuk"
    '                Return "eka"
    '            Case "elamite"
    '                Return "elx"
    '            Case "english"
    '                Return "eng"
    '            Case "english"
    '                Return "enm"
    '            Case "esperanto"
    '                Return "epo"
    '            Case "estonian"
    '                Return "est"
    '            Case "ewe"
    '                Return "ewe"
    '            Case "ewondo"
    '                Return "ewo"
    '            Case "fang"
    '                Return "fan"
    '            Case "faroese"
    '                Return "fao"
    '            Case "fanti"
    '                Return "fat"
    '            Case "fijian"
    '                Return "fij"
    '            Case "filipino", "pilipino"
    '                Return "fil"
    '            Case "finnish"
    '                Return "fin"
    '            Case "finno-ugrian (other)"
    '                Return "fiu"
    '            Case "fon"
    '                Return "fon"
    '            Case "french"
    '                Return "fre"
    '            Case "french"
    '                Return "frm"
    '            Case "french"
    '                Return "fro"
    '            Case "northern frisian"
    '                Return "frr"
    '            Case "eastern frisian"
    '                Return "frs"
    '            Case "western frisian"
    '                Return "fry"
    '            Case "fulah"
    '                Return "ful"
    '            Case "friulian"
    '                Return "fur"
    '            Case "ga"
    '                Return "gaa"
    '            Case "gayo"
    '                Return "gay"
    '            Case "gbaya"
    '                Return "gba"
    '            Case "germanic (other)"
    '                Return "gem"
    '            Case "georgian"
    '                Return "geo"
    '            Case "german"
    '                Return "ger"
    '            Case "geez"
    '                Return "gez"
    '            Case "gilbertese"
    '                Return "gil"
    '            Case "gaelic", "scottish gaelic"
    '                Return "gla"
    '            Case "irish"
    '                Return "gle"
    '            Case "galician"
    '                Return "glg"
    '            Case "manx"
    '                Return "glv"
    '            Case "german"
    '                Return "gmh"
    '            Case "german"
    '                Return "goh"
    '            Case "gondi"
    '                Return "gon"
    '            Case "gorontalo"
    '                Return "gor"
    '            Case "gothic"
    '                Return "got"
    '            Case "grebo"
    '                Return "grb"
    '            Case "greek"
    '                Return "grc"
    '            Case "greek"
    '                Return "gre"
    '            Case "guarani"
    '                Return "grn"
    '            Case "swiss german", "alemannic", "alsatian"
    '                Return "gsw"
    '            Case "gujarati"
    '                Return "guj"
    '            Case "gwich'in"
    '                Return "gwi"
    '            Case "haida"
    '                Return "hai"
    '            Case "haitian", "haitian creole"
    '                Return "hat"
    '            Case "hausa"
    '                Return "hau"
    '            Case "hawaiian"
    '                Return "haw"
    '            Case "hebrew"
    '                Return "heb"
    '            Case "herero"
    '                Return "her"
    '            Case "hiligaynon"
    '                Return "hil"
    '            Case "himachali"
    '                Return "him"
    '            Case "hindi"
    '                Return "hin"
    '            Case "hittite"
    '                Return "hit"
    '            Case "hmong"
    '                Return "hmn"
    '            Case "hiri motu"
    '                Return "hmo"
    '            Case "croatian"
    '                Return "hrv"
    '            Case "upper sorbian"
    '                Return "hsb"
    '            Case "hungarian"
    '                Return "hun"
    '            Case "hupa"
    '                Return "hup"
    '            Case "iban"
    '                Return "iba"
    '            Case "igbo"
    '                Return "ibo"
    '            Case "icelandic"
    '                Return "ice"
    '            Case "ido"
    '                Return "ido"
    '            Case "sichuan yi", "nuosu"
    '                Return "iii"
    '            Case "ijo languages"
    '                Return "ijo"
    '            Case "inuktitut"
    '                Return "iku"
    '            Case "interlingue", "occidental"
    '                Return "ile"
    '            Case "iloko"
    '                Return "ilo"
    '            Case "interlingua (international auxiliary language association)"
    '                Return "ina"
    '            Case "indic (other)"
    '                Return "inc"
    '            Case "indonesian"
    '                Return "ind"
    '            Case "indo-european (other)"
    '                Return "ine"
    '            Case "ingush"
    '                Return "inh"
    '            Case "inupiaq"
    '                Return "ipk"
    '            Case "iranian (other)"
    '                Return "ira"
    '            Case "iroquoian languages"
    '                Return "iro"
    '            Case "italian"
    '                Return "ita"
    '            Case "javanese"
    '                Return "jav"
    '            Case "lojban"
    '                Return "jbo"
    '            Case "japanese"
    '                Return "jpn"
    '            Case "judeo-persian"
    '                Return "jpr"
    '            Case "judeo-arabic"
    '                Return "jrb"
    '            Case "kara-kalpak"
    '                Return "kaa"
    '            Case "kabyle"
    '                Return "kab"
    '            Case "kachin", "jingpho"
    '                Return "kac"
    '            Case "kalaallisut", "greenlandic"
    '                Return "kal"
    '            Case "kamba"
    '                Return "kam"
    '            Case "kannada"
    '                Return "kan"
    '            Case "karen languages"
    '                Return "kar"
    '            Case "kashmiri"
    '                Return "kas"
    '            Case "kanuri"
    '                Return "kau"
    '            Case "kawi"
    '                Return "kaw"
    '            Case "kazakh"
    '                Return "kaz"
    '            Case "kabardian"
    '                Return "kbd"
    '            Case "khasi"
    '                Return "kha"
    '            Case "khoisan (other)"
    '                Return "khi"
    '            Case "central khmer"
    '                Return "khm"
    '            Case "khotanese", "sakan"
    '                Return "kho"
    '            Case "kikuyu", "gikuyu"
    '                Return "kik"
    '            Case "kinyarwanda"
    '                Return "kin"
    '            Case "kirghiz", "kyrgyz"
    '                Return "kir"
    '            Case "kimbundu"
    '                Return "kmb"
    '            Case "konkani"
    '                Return "kok"
    '            Case "komi"
    '                Return "kom"
    '            Case "kongo"
    '                Return "kon"
    '            Case "korean"
    '                Return "kor"
    '            Case "kosraean"
    '                Return "kos"
    '            Case "kpelle"
    '                Return "kpe"
    '            Case "karachay-balkar"
    '                Return "krc"
    '            Case "karelian"
    '                Return "krl"
    '            Case "kru languages"
    '                Return "kro"
    '            Case "kurukh"
    '                Return "kru"
    '            Case "kuanyama", "kwanyama"
    '                Return "kua"
    '            Case "kumyk"
    '                Return "kum"
    '            Case "kurdish"
    '                Return "kur"
    '            Case "kutenai"
    '                Return "kut"
    '            Case "ladino"
    '                Return "lad"
    '            Case "lahnda"
    '                Return "lah"
    '            Case "lamba"
    '                Return "lam"
    '            Case "lao"
    '                Return "lao"
    '            Case "latin"
    '                Return "lat"
    '            Case "latvian"
    '                Return "lav"
    '            Case "lezghian"
    '                Return "lez"
    '            Case "limburgan", "limburger", "limburgish"
    '                Return "lim"
    '            Case "lingala"
    '                Return "lin"
    '            Case "lithuanian"
    '                Return "lit"
    '            Case "mongo"
    '                Return "lol"
    '            Case "lozi"
    '                Return "loz"
    '            Case "luxembourgish", "letzeburgesch"
    '                Return "ltz"
    '            Case "luba-lulua"
    '                Return "lua"
    '            Case "luba-katanga"
    '                Return "lub"
    '            Case "ganda"
    '                Return "lug"
    '            Case "luiseno"
    '                Return "lui"
    '            Case "lunda"
    '                Return "lun"
    '            Case "luo (kenya and tanzania)"
    '                Return "luo"
    '            Case "lushai"
    '                Return "lus"
    '            Case "macedonian"
    '                Return "mac"
    '            Case "madurese"
    '                Return "mad"
    '            Case "magahi"
    '                Return "mag"
    '            Case "marshallese"
    '                Return "mah"
    '            Case "maithili"
    '                Return "mai"
    '            Case "makasar"
    '                Return "mak"
    '            Case "malayalam"
    '                Return "mal"
    '            Case "mandingo"
    '                Return "man"
    '            Case "maori"
    '                Return "mao"
    '            Case "austronesian (other)"
    '                Return "map"
    '            Case "marathi"
    '                Return "mar"
    '            Case "masai"
    '                Return "mas"
    '            Case "malay"
    '                Return "may"
    '            Case "moksha"
    '                Return "mdf"
    '            Case "mandar"
    '                Return "mdr"
    '            Case "mende"
    '                Return "men"
    '            Case "irish"
    '                Return "mga"
    '            Case "mi'kmaq", "micmac"
    '                Return "mic"
    '            Case "minangkabau"
    '                Return "min"
    '            Case "uncoded languages"
    '                Return "mis"
    '            Case "mon-khmer (other)"
    '                Return "mkh"
    '            Case "malagasy"
    '                Return "mlg"
    '            Case "maltese"
    '                Return "mlt"
    '            Case "manchu"
    '                Return ("mnc")
    '            Case "manipuri"
    '                Return "mni"
    '            Case "manobo languages"
    '                Return "mno"
    '            Case "mohawk"
    '                Return "moh"
    '            Case "mongolian"
    '                Return "mon"
    '            Case "mossi"
    '                Return "mos"
    '            Case "multiple languages"
    '                Return "mul"
    '            Case "munda languages"
    '                Return "mun"
    '            Case "creek"
    '                Return "mus"
    '            Case "mirandese"
    '                Return "mwl"
    '            Case "marwari"
    '                Return "mwr"
    '            Case "mayan languages"
    '                Return "myn"
    '            Case "erzya"
    '                Return "myv"
    '            Case "nahuatl languages"
    '                Return "nah"
    '            Case "north american indian"
    '                Return "nai"
    '            Case "neapolitan"
    '                Return "nap"
    '            Case "nauru"
    '                Return "nau"
    '            Case "navajo", "navaho"
    '                Return "nav"
    '            Case "ndebele"
    '                Return "nbl"
    '            Case "ndebele"
    '                Return "nde"
    '            Case "ndonga"
    '                Return "ndo"
    '            Case "low german", "low saxon", "german"
    '                Return "nds"
    '            Case "nepali"
    '                Return "nep"
    '            Case "nepal bhasa", "newari"
    '                Return "new"
    '            Case "nias"
    '                Return "nia"
    '            Case "niger-kordofanian (other)"
    '                Return "nic"
    '            Case "niuean"
    '                Return "niu"
    '            Case "norwegian nynorsk", "nynorsk"
    '                Return "nno"
    '            Case "bokmål"
    '                Return "nob"
    '            Case "nogai"
    '                Return "nog"
    '            Case "norse"
    '                Return "non"
    '            Case "norwegian"
    '                Return "nor"
    '            Case "n'ko"
    '                Return "nqo"
    '            Case "pedi", "sepedi", "northern sotho"
    '                Return "nso"
    '            Case "nubian languages"
    '                Return "nub"
    '            Case "classical newari", "old newari", "classical nepal bhasa"
    '                Return "nwc"
    '            Case "chichewa", "chewa", "nyanja"
    '                Return "nya"
    '            Case "nyamwezi"
    '                Return "nym"
    '            Case "nyankole"
    '                Return "nyn"
    '            Case "nyoro"
    '                Return "nyo"
    '            Case "nzima"
    '                Return "nzi"
    '            Case "occitan (post 1500)", "provençal"
    '                Return "oci"
    '            Case "ojibwa"
    '                Return "oji"
    '            Case "oriya"
    '                Return "ori"
    '            Case "oromo"
    '                Return "orm"
    '            Case "osage"
    '                Return "osa"
    '            Case "ossetian", "ossetic"
    '                Return "oss"
    '            Case "turkish"
    '                Return "ota"
    '            Case "otomian languages"
    '                Return "oto"
    '            Case "papuan (other)"
    '                Return "paa"
    '            Case "pangasinan"
    '                Return "pag"
    '            Case "pahlavi"
    '                Return "pal"
    '            Case "pampanga", "kapampangan"
    '                Return "pam"
    '            Case "panjabi", "punjabi"
    '                Return "pan"
    '            Case "papiamento"
    '                Return "pap"
    '            Case "palauan"
    '                Return "pau"
    '            Case "persian"
    '                Return "peo"
    '            Case "persian"
    '                Return "per"
    '            Case "philippine (other)"
    '                Return "phi"
    '            Case "phoenician"
    '                Return "phn"
    '            Case "pali"
    '                Return "pli"
    '            Case "polish"
    '                Return "pol"
    '            Case "pohnpeian"
    '                Return "pon"
    '            Case "portuguese"
    '                Return "por"
    '            Case "prakrit languages"
    '                Return "pra"
    '            Case "provençal"
    '                Return "pro"
    '            Case "pushto", "pashto"
    '                Return "pus"
    '            Case "reserved for local use"
    '                Return "qaa-qtz"
    '            Case "quechua"
    '                Return "que"
    '            Case "rajasthani"
    '                Return "raj"
    '            Case "rapanui"
    '                Return "rap"
    '            Case "rarotongan", "cook islands maori"
    '                Return "rar"
    '            Case "romance (other)"
    '                Return "roa"
    '            Case "romansh"
    '                Return "roh"
    '            Case "romany"
    '                Return "rom"
    '            Case "romanian", "moldavian", "moldovan"
    '                Return "rum"
    '            Case "rundi"
    '                Return "run"
    '            Case "aromanian", "arumanian", "macedo-romanian"
    '                Return "rup"
    '            Case "russian"
    '                Return "rus"
    '            Case "sandawe"
    '                Return "sad"
    '            Case "sango"
    '                Return "sag"
    '            Case "yakut"
    '                Return "sah"
    '            Case "south american indian (other)"
    '                Return "sai"
    '            Case "salishan languages"
    '                Return "sal"
    '            Case "samaritan aramaic"
    '                Return "sam"
    '            Case "sanskrit"
    '                Return "san"
    '            Case "sasak"
    '                Return "sas"
    '            Case "santali"
    '                Return "sat"
    '            Case "sicilian"
    '                Return "scn"
    '            Case "scots"
    '                Return "sco"
    '            Case "selkup"
    '                Return "sel"
    '            Case "semitic (other)"
    '                Return "sem"
    '            Case "irish"
    '                Return "sga"
    '            Case "sign languages"
    '                Return "sgn"
    '            Case "shan"
    '                Return "shn"
    '            Case "sidamo"
    '                Return "sid"
    '            Case "sinhala", "sinhalese"
    '                Return "sin"
    '            Case "siouan languages"
    '                Return "sio"
    '            Case "sino-tibetan (other)"
    '                Return "sit"
    '            Case "slavic (other)"
    '                Return "sla"
    '            Case "slovak"
    '                Return "slo"
    '            Case "slovenian"
    '                Return "slv"
    '            Case "southern sami"
    '                Return "sma"
    '            Case "northern sami"
    '                Return "sme"
    '            Case "sami languages (other)"
    '                Return "smi"
    '            Case "lule sami"
    '                Return "smj"
    '            Case "inari sami"
    '                Return "smn"
    '            Case "samoan"
    '                Return "smo"
    '            Case "skolt sami"
    '                Return "sms"
    '            Case "shona"
    '                Return "sna"
    '            Case "sindhi"
    '                Return "snd"
    '            Case "soninke"
    '                Return "snk"
    '            Case "sogdian"
    '                Return "sog"
    '            Case "somali"
    '                Return "som"
    '            Case "songhai languages"
    '                Return "son"
    '            Case "sotho"
    '                Return "sot"
    '            Case "spanish", "castilian"
    '                Return "spa"
    '            Case "sardinian"
    '                Return "srd"
    '            Case "sranan tongo"
    '                Return "srn"
    '            Case "serbian"
    '                Return "srp"
    '            Case "serer"
    '                Return "srr"
    '            Case "nilo-saharan (other)"
    '                Return "ssa"
    '            Case "swati"
    '                Return "ssw"
    '            Case "sukuma"
    '                Return "suk"
    '            Case "sundanese"
    '                Return "sun"
    '            Case "susu"
    '                Return "sus"
    '            Case "sumerian"
    '                Return "sux"
    '            Case "swahili"
    '                Return "swa"
    '            Case "swedish"
    '                Return "swe"
    '            Case "classical syriac"
    '                Return "syc"
    '            Case "syriac"
    '                Return "syr"
    '            Case "tahitian"
    '                Return "tah"
    '            Case "tai (other)"
    '                Return "tai"
    '            Case "tamil"
    '                Return "tam"
    '            Case "tatar"
    '                Return "tat"
    '            Case "telugu"
    '                Return "tel"
    '            Case "timne"
    '                Return "tem"
    '            Case "tereno"
    '                Return "ter"
    '            Case "tetum"
    '                Return "tet"
    '            Case "tajik"
    '                Return "tgk"
    '            Case "tagalog"
    '                Return "tgl"
    '            Case "thai"
    '                Return "tha"
    '            Case "tibetan"
    '                Return "tib"
    '            Case "tigre"
    '                Return "tig"
    '            Case "tigrinya"
    '                Return "tir"
    '            Case "tiv"
    '                Return "tiv"
    '            Case "tokelau"
    '                Return "tkl"
    '            Case "klingon", "tlhingan-hol"
    '                Return "tlh"
    '            Case "tlingit"
    '                Return "tli"
    '            Case "tamashek"
    '                Return "tmh"
    '            Case "tonga (nyasa)"
    '                Return "tog"
    '            Case "tonga (tonga islands)"
    '                Return "ton"
    '            Case "tok pisin"
    '                Return "tpi"
    '            Case "tsimshian"
    '                Return "tsi"
    '            Case "tswana"
    '                Return "tsn"
    '            Case "tsonga"
    '                Return "tso"
    '            Case "turkmen"
    '                Return "tuk"
    '            Case "tumbuka"
    '                Return "tum"
    '            Case "tupi languages"
    '                Return "tup"
    '            Case "turkish"
    '                Return "tur"
    '            Case "altaic (other)"
    '                Return "tut"
    '            Case "tuvalu"
    '                Return "tvl"
    '            Case "twi"
    '                Return "twi"
    '            Case "tuvinian"
    '                Return "tyv"
    '            Case "udmurt"
    '                Return "udm"
    '            Case "ugaritic"
    '                Return "uga"
    '            Case "uighur", "uyghur"
    '                Return "uig"
    '            Case "ukrainian"
    '                Return "ukr"
    '            Case "umbundu"
    '                Return "umb"
    '            Case "undetermined"
    '                Return "und"
    '            Case "urdu"
    '                Return "urd"
    '            Case "uzbek"
    '                Return "uzb"
    '            Case "vai"
    '                Return "vai"
    '            Case "venda"
    '                Return "ven"
    '            Case "vietnamese"
    '                Return "vie"
    '            Case "volapük"
    '                Return "vol"
    '            Case "votic"
    '                Return "vot"
    '            Case "wakashan languages"
    '                Return "wak"
    '            Case "walamo"
    '                Return "wal"
    '            Case "waray"
    '                Return "war"
    '            Case "washo"
    '                Return "was"
    '            Case "welsh"
    '                Return "wel"
    '            Case "sorbian languages"
    '                Return "wen"
    '            Case "walloon"
    '                Return "wln"
    '            Case "wolof"
    '                Return "wol"
    '            Case "kalmyk", "oirat"
    '                Return "xal"
    '            Case "xhosa"
    '                Return "xho"
    '            Case "yao"
    '                Return "yao"
    '            Case "yapese"
    '                Return "yap"
    '            Case "yiddish"
    '                Return "yid"
    '            Case "yoruba"
    '                Return "yor"
    '            Case "yupik languages"
    '                Return "ypk"
    '            Case "zapotec"
    '                Return "zap"
    '            Case "blissymbols", "blissymbolics", "bliss"
    '                Return "zbl"
    '            Case "zenaga"
    '                Return "zen"
    '            Case "zhuang", "chuang"
    '                Return "zha"
    '            Case "zande languages"
    '                Return "znd"
    '            Case "zulu"
    '                Return "zul"
    '            Case "zuni"
    '                Return "zun"
    '            Case "no linguistic content", "not applicable"
    '                Return "zxx"
    '            Case "zaza", "dimili", "dimli", "kirdki", "kirmanjki", "zazaki"
    '                Return "zza"
    '        End Select
    '    Catch
    '    Finally
    '        Monitor.Exit(monitorobject)
    '    End Try
    '    Return "Error"
    'End Function

    'Public Function getmedialist(ByVal pathandfilename As String)
    '    Try
    '        Dim tempstring As String = pathandfilename
    '        Dim playlist As New List(Of String)
    '        If IO.File.Exists(tempstring) Then
    '            playlist.Add(tempstring)
    '        End If
    '        tempstring = tempstring.ToLower
    '        If tempstring.IndexOf("cd1") <> -1 Then
    '            tempstring = tempstring.Replace("cd1", "cd2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd2", "cd3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd3", "cd4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd4", "cd5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("cd_1") <> -1 Then
    '            tempstring = tempstring.Replace("cd_1", "cd_2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd_2", "cd_3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd_3", "cd_4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd_4", "cd_5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("cd 1") <> -1 Then
    '            tempstring = tempstring.Replace("cd 1", "cd 2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd 2", "cd 3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd 3", "cd 4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd 4", "cd 5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("cd.1") <> -1 Then
    '            tempstring = tempstring.Replace("cd.1", "cd.2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd.2", "cd.3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd.3", "cd.4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("cd.4", "cd.5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("dvd1") <> -1 Then
    '            tempstring = tempstring.Replace("dvd1", "dvd2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd2", "dvd3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd3", "dvd4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd4", "dvd5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("dvd_1") <> -1 Then
    '            tempstring = tempstring.Replace("dvd_1", "dvd_2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd_2", "dvd_3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd_3", "dvd_4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd_4", "dvd_5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("dvd 1") <> -1 Then
    '            tempstring = tempstring.Replace("dvd 1", "dvd 2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd 2", "dvd 3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd 3", "dvd 4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd 4", "dvd 5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("dvd.1") <> -1 Then
    '            tempstring = tempstring.Replace("dvd.1", "dvd.2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd.2", "dvd.3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd.3", "dvd.4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("dvd.4", "dvd.5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("part1") <> -1 Then
    '            tempstring = tempstring.Replace("part1", "part2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part2", "part3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part3", "part4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part4", "part5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("part_1") <> -1 Then
    '            tempstring = tempstring.Replace("part_1", "part_2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part_2", "part_3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part_3", "part_4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part_4", "part_5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("part 1") <> -1 Then
    '            tempstring = tempstring.Replace("part 1", "part 2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part 2", "part 3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part 3", "part 4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part 4", "part 5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("part.1") <> -1 Then
    '            tempstring = tempstring.Replace("part.1", "part.2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part.2", "part.3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part.3", "part.4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("part.4", "part.5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("disk1") <> -1 Then
    '            tempstring = tempstring.Replace("disk1", "disk2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk2", "disk3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk3", "disk4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk4", "disk5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("disk_1") <> -1 Then
    '            tempstring = tempstring.Replace("disk_1", "disk_2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk_2", "disk_3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk_3", "disk_4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk_4", "disk_5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("disk 1") <> -1 Then
    '            tempstring = tempstring.Replace("disk 1", "disk 2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk 2", "disk 3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk 3", "disk 4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk 4", "disk 5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("disk.1") <> -1 Then
    '            tempstring = tempstring.Replace("disk.1", "disk.2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk.2", "disk.3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk.3", "disk.4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("disk.4", "disk.5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("pt1") <> -1 Then
    '            tempstring = tempstring.Replace("pt1", "pt2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt2", "pt3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt3", "pt4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt4", "pt5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("pt_1") <> -1 Then
    '            tempstring = tempstring.Replace("pt_1", "pt_2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt_2", "pt_3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt_3", "pt_4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt_4", "pt_5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("pt 1") <> -1 Then
    '            tempstring = tempstring.Replace("pt 1", "pt 2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt 2", "pt 3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt 3", "pt 4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt 4", "pt 5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        If tempstring.IndexOf("pt.1") <> -1 Then
    '            tempstring = tempstring.Replace("pt.1", "pt.2")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt.2", "pt.3")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt.3", "pt.4")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '            tempstring = tempstring.Replace("pt.4", "pt.5")
    '            If IO.File.Exists(tempstring) Then
    '                playlist.Add(tempstring)
    '            End If
    '        End If
    '        Return playlist
    '    Catch

    '    End Try
    '    Return "0"
    'End Function

    'Private Sub loadactorcache()
    '    actorDB.Clear()
    '    Dim loadpath As String = Preferences.workingProfile.actorcache
    '    Dim actorlist As New XmlDocument
    '    actorlist.Load(loadpath)
    '    Dim thisresult As XmlNode = Nothing
    '    For Each thisresult In actorlist("actor_cache")
    '        Select Case thisresult.Name
    '            Case "actor"
    '                Dim newactor As New ActorDatabase
    '                newactor.actorname = ""
    '                newactor.movieid = ""
    '                Dim detail As XmlNode = Nothing
    '                For Each detail In thisresult.ChildNodes
    '                    Select Case detail.Name
    '                        Case "name"
    '                            newactor.actorname = detail.InnerText
    '                        Case "id"
    '                            newactor.movieid = detail.InnerText
    '                    End Select
    '                    If newactor.actorname <> "" And newactor.movieid <> "" Then
    '                        actorDB.Add(newactor)
    '                    End If
    '                Next
    '        End Select
    '    Next
    'End Sub

    'Obsolete
    'Private Sub SaveActorCache()
    '    Dim savepath As String = Preferences.workingProfile.actorcache
    '    Dim doc As New XmlDocument

    '    Dim thispref As XmlNode = Nothing
    '    Dim xmlproc As XmlDeclaration

    '    xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
    '    doc.AppendChild(xmlproc)
    '    Dim root As XmlElement
    '    Dim child As XmlElement
    '    root = doc.CreateElement("actor_cache")

    '    Dim childchild As XmlElement
    '    For Each actor In actorDB
    '        child = doc.CreateElement("actor")
    '        childchild = doc.CreateElement("name")
    '        childchild.InnerText = actor.actorname
    '        child.AppendChild(childchild)
    '        childchild = doc.CreateElement("id")
    '        childchild.InnerText = actor.movieid
    '        child.AppendChild(childchild)
    '        root.AppendChild(child)
    '    Next
    '    doc.AppendChild(root)
    '    Dim output As New XmlTextWriter(savepath, System.Text.Encoding.UTF8)
    '    output.Formatting = Formatting.Indented
    '    doc.WriteTo(output)
    '    output.Close()
    'End Sub

    Private Sub StartNewMovies()

        ConsoleOrLog("")
        ConsoleOrLog("**** Searching for new movies...        ****")
        ConsoleOrLog("**** Press the Escape (Esc) key to quit ****")
        ConsoleOrLog("")

        scraper.WorkerReportsProgress      = True 
        scraper.WorkerSupportsCancellation = True
        scraper.RunWorkerAsync()
        
        While( scraper.IsBusy )
            If (Console.KeyAvailable) AndAlso Console.ReadKey.Key = ConsoleKey.Escape AndAlso Not scraper.CancellationPending then
                ConsoleOrLog("Stopping thread...")
                scraper.CancelAsync()
            End If

            Thread.Sleep(200)
        End While
        If Not visible Then
            For Each lgstr In logstr
                ConsoleOrLog(lgstr)
            Next
        End If
    End Sub

Private Sub scraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)  Handles scraper.DoWork
    oMovies.FindNewMovies
End Sub

Private Sub scraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles scraper.ProgressChanged
    Dim oProgress As Progress = CType(e.UserState, Progress) 

    If Not IsNothing(oProgress.Log) Then
            If oProgress.Log.Contains("!!! ") Then
                If Not visible Then
                    logstr.Add(oProgress.Log.Replace("!!! ", ""))
                Else
                    ConsoleOrLog(oProgress.Log.Replace("!!! ", ""))
                End If
            End If
        End If
        Thread.Sleep(1)
End Sub

Private Sub scraper_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles scraper.RunWorkerCompleted
     
End Sub


End Module


Public Structure arguments
    Dim switch As String
    Dim argu As String
End Structure

Public Class basictvshownfo
    Public fullpath As String
    Public title As String
    Public id As String
    Public sortorder As String
    Public language As String
    Public episodeactorsource As String
    Public locked As String
    Public imdbid As String
    Public playcount As String
    Public allepisodes As New List(Of episodeinfo)
End Class

Public Class episodeinfo
    Public title As String
    Public showid As String
    Public credits As String
    Public director As String
    Public aired As String
    Public playcount As String
    Public thumb As String
    Public rating As String
    Public seasonno As String
    Public episodeno As String
    Public uniqueid As String
    Public plot As String
    Public runtime As String
    Public fanartpath As String
    Public genre As String
    Public mediaextension As String
    Public episodepath As String
    Public missing As String
    Public extension As String
    Public pure As String
    Public listactors As New List(Of str_MovieActors)
    Public filedetails As New fullfiledetails2
End Class

Public Class fullfiledetails2
    Public filedetails_video As medianfo_video
    Public filedetails_audio As New List(Of medianfo_audio)
    Public filedetails_subtitles As New List(Of medianfo_subtitles)
End Class

Public Structure medianfo_audio
    Dim language As String
    Dim codec As String
    Dim channels As String
    Dim bitrate As String
End Structure

Public Structure medianfo_subtitles
    Dim language As String
End Structure

Public Structure medianfo_video
    Dim width As String
    Dim height As String
    Dim aspect As String
    Dim codec As String
    Dim formatinfo As String
    Dim duration As String
    Dim bitrate As String
    Dim bitratemode As String
    Dim bitratemax As String
    Dim container As String
    Dim codecid As String
    Dim codecinfo As String
    Dim scantype As String
End Structure

Public Enum StreamKind As UInteger
    General
    Visual
    Audio
    Text
    Chapters
    Image
    Menu
    Max
End Enum

Public Enum InfoKind As UInteger
    Name
    Text
    Measure
    Options
    NameText
    MeasureText
    Info
    HowTo
    Max
End Enum

Public Class mediainfo
    Private Declare Unicode Function MediaInfo_New Lib "MediaInfo.DLL" () As IntPtr
    Private Declare Unicode Sub MediaInfo_Delete Lib "MediaInfo.DLL" (ByVal Handle As IntPtr)
    Private Declare Unicode Function MediaInfo_Open Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal FileName As String) As UIntPtr
    Private Declare Unicode Sub MediaInfo_Close Lib "MediaInfo.DLL" (ByVal Handle As IntPtr)
    Private Declare Unicode Function MediaInfo_Inform Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal Reserved As UIntPtr) As IntPtr
    Private Declare Unicode Function MediaInfo_GetI Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As UIntPtr, ByVal KindOfInfo As UIntPtr) As IntPtr 'See MediaInfoDLL.h for enumeration values
    Private Declare Unicode Function MediaInfo_Get Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As String, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
    Private Declare Unicode Function MediaInfo_Option Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal Option_ As String, ByVal Value As String) As IntPtr
    Private Declare Unicode Function MediaInfo_State_Get Lib "MediaInfo.DLL" (ByVal Handle As IntPtr) As UIntPtr 'see MediaInfo.h for details
    Private Declare Unicode Function MediaInfo_Count_Get Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As IntPtr) As UIntPtr 'see MediaInfoDLL.h for enumeration values

    Dim Handle As IntPtr

    Sub New()
        Handle = MediaInfo_New()
    End Sub

    Protected Overrides Sub Finalize()
        MediaInfo_Delete(Handle)
    End Sub

    Function Open(ByVal FileName As String) As Integer
        Return MediaInfo_Open(Handle, FileName)
    End Function

    Sub Close()
        MediaInfo_Close(Handle)
    End Sub

    Function Inform() As String
        Return Marshal.PtrToStringUni(MediaInfo_Inform(Handle, 0))
    End Function

    Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As Integer, Optional ByVal KindOfInfo As InfoKind = InfoKind.Text) As String
        Return Marshal.PtrToStringUni(MediaInfo_GetI(Handle, StreamKind, StreamNumber, Parameter, KindOfInfo))
    End Function

    Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As String, Optional ByVal KindOfInfo As InfoKind = InfoKind.Text, Optional ByVal KindOfSearch As InfoKind = InfoKind.Name) As String
        Return Marshal.PtrToStringUni(MediaInfo_Get(Handle, StreamKind, StreamNumber, Parameter, KindOfInfo, KindOfSearch))
    End Function

    Function Option_(ByVal Option__ As String, Optional ByVal Value As String = "") As String
        Return Marshal.PtrToStringUni(MediaInfo_Option(Handle, Option__, Value))
    End Function

    Function State_Get() As Integer
        Return MediaInfo_State_Get(Handle)
    End Function

    Function Count_Get(ByVal StreamKind As StreamKind, Optional ByVal StreamNumber As UInteger = UInteger.MaxValue) As Integer
        If StreamNumber = UInteger.MaxValue Then
            Dim A As Long
            A = 0
            A = A - 1 'If you know how to have (IntPtr)(-1) easier, I am interested ;-)
            Return MediaInfo_Count_Get(Handle, StreamKind, A)
        Else
            Return MediaInfo_Count_Get(Handle, StreamKind, StreamNumber)
        End If
    End Function
End Class

Public Class tvdbscraper
    Private Structure possibleshowlist
        Dim showtitle As String
        Dim showid As String
        Dim showbanner As String
    End Structure

    Public Function getposterlist(ByVal tvdbid As String)
        Monitor.Enter(Me)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim wrGETURL As WebRequest
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/banners.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim bannerslist As New XmlDocument
            'Try
            Dim bannerlist As String = "<banners>"
            bannerslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In bannerslist("Banners")

                Select Case thisresult.Name
                    Case "Banner"
                        bannerlist = bannerlist & "<banner>"
                        Dim bannerselection As XmlNode = Nothing
                        For Each bannerselection In thisresult.ChildNodes
                            Select Case bannerselection.Name
                                Case "BannerPath"

                                    bannerlist = bannerlist & "<url>http://thetvdb.com/banners/" & bannerselection.InnerXml & "</url>"
                                Case "BannerType"
                                    bannerlist = bannerlist & "<bannertype>" & bannerselection.InnerXml & "</bannertype>"
                                Case "BannerType2"
                                    bannerlist = bannerlist & "<resolution>" & bannerselection.InnerXml & "</resolution>"
                                Case "Language"
                                    bannerlist = bannerlist & "<language>" & bannerselection.InnerXml & "</language>"
                                Case "Season"
                                    bannerlist = bannerlist & "<season>" & bannerselection.InnerXml & "</season>"
                                Case ""
                            End Select
                        Next
                        bannerlist = bannerlist & "</banner>"
                End Select
            Next
            bannerlist = bannerlist & "</banners>"
            Return bannerlist

        Catch EX As Exception
            Return EX.ToString
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function getmirrors()
        Monitor.Enter(Me)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim wrGETURL As WebRequest
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/mirrors.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim mirrorslist As New XmlDocument
            'Try
            mirrorslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In mirrorslist("Mirrors")

                Select Case thisresult.Name
                    Case "Mirror"
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "mirrorpath"
                                    If mirrorselection.InnerText <> Nothing Then
                                        mirrors.Add(mirrorselection.InnerText)
                                    End If
                            End Select
                        Next
                End Select
            Next
            Return mirrors

        Catch EX As Exception
            Return EX.ToString
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function findshows(ByVal title As String, Optional ByVal mirror As String = "http://thetvdb.com")
        Monitor.Enter(Me)
        'Try
        Dim possibleshows As New List(Of possibleshowlist)
        Dim xmlfile As String
        Dim wrGETURL As WebRequest
        Dim mirrorsurl As String = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & title & "&language=all"
        wrGETURL = WebRequest.Create(mirrorsurl)
        Dim myProxy As New WebProxy("myproxy", 80)
        myProxy.BypassProxyOnLocal = True
        Dim objStream As Stream
        objStream = wrGETURL.GetResponse.GetResponseStream()
        Dim objReader As New StreamReader(objStream)
        xmlfile = objReader.ReadToEnd
        Dim showlist As New XmlDocument
        Try
            showlist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In showlist("Data")

                Select Case thisresult.Name
                    Case "Series"
                        Dim newshow As New possibleshowlist
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "seriesid"
                                    newshow.showid = mirrorselection.InnerXml
                                Case "SeriesName"
                                    newshow.showtitle = mirrorselection.InnerXml
                                Case "banner"
                                    newshow.showbanner = "http://www.thetvdb.com/banners/" & mirrorselection.InnerXml
                            End Select
                        Next
                        possibleshows.Add(newshow)
                End Select
            Next
            Dim returnstring As String
            Dim ok As Boolean = False
            If possibleshows.Count > 0 Then
                returnstring = "<allshows>"
                For Each show In possibleshows
                    If show.showid <> Nothing Then
                        returnstring = returnstring & "<show>"
                        returnstring = returnstring & "<showid>" & show.showid & "</showid>"
                        ok = True
                        If show.showtitle <> Nothing Then
                            returnstring = returnstring & "<showtitle>" & show.showtitle & "</showtitle>"
                        End If
                        If show.showbanner <> Nothing Then
                            returnstring = returnstring & "<showbanner>" & show.showbanner & "</showbanner>"
                        End If
                        returnstring = returnstring & "</show>"
                    End If
                Next
                returnstring = returnstring & "</allshows>"
            Else
                returnstring = "none"
            End If
            If ok = False Then returnstring = "none"
            Return returnstring
            'Catch EX As Exception
            '    Return EX.ToString
            'End Try
        Catch EX As Exception
            Return "error"
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function getshow(ByVal tvdbid As String, ByVal language As String)
        Monitor.Enter(Me)
        Try
            Dim tvshowdetails As String = "<fulltvshow>"
            Dim xmlfile As String
            Dim wrGETURL As WebRequest
            Dim episodeguideurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & language & ".xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim showlist As New XmlDocument
            'Try
            showlist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In showlist("Data")

                Select Case thisresult.Name
                    Case "Series"
                        Dim newshow As New possibleshowlist
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "SeriesName"
                                    tvshowdetails = tvshowdetails & "<title>" & mirrorselection.InnerXml & "</title>"
                                Case "ContentRating"
                                    tvshowdetails = tvshowdetails & "<mpaa>" & mirrorselection.InnerXml & "</mpaa>"
                                Case "FirstAired"
                                    tvshowdetails = tvshowdetails & "<premiered>" & mirrorselection.InnerXml & "</premiered>"
                                Case "Genre"
                                    tvshowdetails = tvshowdetails & "<genre>" & mirrorselection.InnerXml & "</genre>"
                                Case "IMDB_ID"
                                    tvshowdetails = tvshowdetails & "<imdbid>" & mirrorselection.InnerXml & "</imdbid>"
                                Case "Network"
                                    tvshowdetails = tvshowdetails & "<studio>" & mirrorselection.InnerXml & "</studio>"
                                Case "Overview"
                                    tvshowdetails = tvshowdetails & "<plot>" & mirrorselection.InnerXml & "</plot>"
                                Case "Rating"
                                    tvshowdetails = tvshowdetails & "<rating>" & mirrorselection.InnerXml & "</rating>"
                                Case "Runtime"
                                    tvshowdetails = tvshowdetails & "<runtime>" & mirrorselection.InnerXml & "</runtime>"
                                Case "banner"
                                    tvshowdetails = tvshowdetails & "<banner>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</banner>"
                                Case "fanart"
                                    tvshowdetails = tvshowdetails & "<fanart>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</fanart>"
                                Case "poster"
                                    tvshowdetails = tvshowdetails & "<poster>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</poster>"
                            End Select
                        Next
                End Select
            Next

            tvshowdetails = tvshowdetails & "<episodeguideurl>" & episodeguideurl & "</episodeguideurl>"

            mirrorsurl = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/actors.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            Dim objStream2 As Stream
            objStream2 = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader2 As New StreamReader(objStream2)
            xmlfile = objReader2.ReadToEnd
            Dim showlist2 As New XmlDocument
            'Try
            showlist2.LoadXml(xmlfile)
            thisresult = Nothing
            For Each thisresult In showlist2("Actors")

                Select Case thisresult.Name
                    Case "Actor"
                        tvshowdetails = tvshowdetails & "<actor>"
                        Dim newshow As New possibleshowlist
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "id"
                                    tvshowdetails = tvshowdetails & "<actorid>" & mirrorselection.InnerXml & "</actorid>"
                                Case "Image"
                                    If mirrorselection.InnerXml <> Nothing Then
                                        If mirrorselection.InnerXml <> "" Then
                                            tvshowdetails = tvshowdetails & "<thumb>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</thumb>"
                                        End If
                                    End If
                                Case "Name"
                                    tvshowdetails = tvshowdetails & "<name>" & mirrorselection.InnerXml & "</name>"
                                Case "Role"
                                    If mirrorselection.InnerXml <> Nothing Then
                                        If mirrorselection.InnerXml <> "" Then
                                            tvshowdetails = tvshowdetails & "<role>" & mirrorselection.InnerXml & "</role>"
                                        End If
                                    End If
                            End Select
                        Next
                        tvshowdetails = tvshowdetails & "</actor>"
                End Select
            Next

            mirrorsurl = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/actors.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            Dim objStream3 As Stream
            objStream3 = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader3 As New StreamReader(objStream3)
            xmlfile = objReader3.ReadToEnd
            Dim showlist3 As New XmlDocument
            'Try
            showlist3.LoadXml(xmlfile)
            thisresult = Nothing
            For Each thisresult In showlist3("Actors")

                Select Case thisresult.Name
                    Case "Actor"
                        tvshowdetails = tvshowdetails & "<actor>"
                        Dim newshow As New possibleshowlist
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "id"
                                    tvshowdetails = tvshowdetails & "<actorid>" & mirrorselection.InnerXml & "</actorid>"
                                Case "Image"
                                    If mirrorselection.InnerXml <> Nothing Then
                                        If mirrorselection.InnerXml <> "" Then
                                            tvshowdetails = tvshowdetails & "<thumb>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</thumb>"
                                        End If
                                    End If
                                Case "Name"
                                    tvshowdetails = tvshowdetails & "<name>" & mirrorselection.InnerXml & "</name>"
                                Case "Role"
                                    If mirrorselection.InnerXml <> Nothing Then
                                        If mirrorselection.InnerXml <> "" Then
                                            tvshowdetails = tvshowdetails & "<role>" & mirrorselection.InnerXml & "</role>"
                                        End If
                                    End If
                            End Select
                        Next
                        tvshowdetails = tvshowdetails & "</actor>"
                End Select
            Next

            tvshowdetails = tvshowdetails & "</fulltvshow>"

            Return tvshowdetails
        Catch
            Return "!!!Error!!!"
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Shared Function DownloadFileToString(ByVal URL As String, Optional ByVal ForceDownload As Boolean = False) As String
        Dim webReq As HttpWebRequest = WebRequest.Create(URL)
        Dim html As String
        Using webResp As HttpWebResponse = webReq.GetResponse()
            Dim responseStream As Stream = webResp.GetResponseStream()
            If (webResp.ContentEncoding.ToLower().Contains("gzip")) Then
                responseStream = New GZipStream(responseStream, CompressionMode.Decompress)

            ElseIf (webResp.ContentEncoding.ToLower().Contains("deflate")) Then
                responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
            End If
            Dim reader As StreamReader = New StreamReader(responseStream, Encoding.UTF8)

            html = reader.ReadToEnd()

            responseStream.Close()

        End Using
        Return html
    End Function

    Public Function getepisode(ByVal tvdbid As String, ByVal sortorder As String, ByVal seriesno As String, ByVal episodeno As String, ByVal language As String)
        Monitor.Enter(Me)
        Dim episodestring As String = ""
        Dim episodeurl As String = ""
        Try
            'http://thetvdb.com/api/6E82FED600783400/series/70726/default/1/1/en.xml

            Dim xmlfile As String
            If language.ToLower.IndexOf(".xml") = -1 Then
                language = language & ".xml"
            End If
            episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seriesno & "/" & episodeno & "/" & language
            'wrGETURL = WebRequest.Create(episodeurl)
            'Dim myProxy As New WebProxy("myproxy", 80)
            'myProxy.BypassProxyOnLocal = True
            'Dim objStream As Stream
            'objStream = wrGETURL.GetResponse.GetResponseStream()
            'Dim objReader As New StreamReader(objStream)
            'xmlfile = objReader.ReadToEnd
            xmlfile = DownloadFileToString(episodeurl)
            Dim episode As New XmlDocument

            episode.LoadXml(xmlfile)

            episodestring = "<episodedetails>"
            episodestring = episodestring & "<url>" & episodeurl & "</url>"
            Dim mirrorslist As New XmlDocument
            'Try
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
                                    'Dim tempstring As String = mirrorselection.InnerXml
                                    'Try
                                    '    tempstring = tempstring.TrimStart("|")
                                    '    tempstring = tempstring.TrimEnd("|")
                                    '    Dim tvtempstring2 As String
                                    '    Dim tvtempint As Integer
                                    '    Dim a() As String
                                    '    Dim j As Integer
                                    '    tvtempstring2 = ""
                                    '    a = tempstring.Split("|")
                                    '    tvtempint = a.GetUpperBound(0)
                                    '    tvtempstring2 = a(0)
                                    '    If tvtempint >= 0 Then
                                    '        For j = 0 To tvtempint
                                    '            Try
                                    '                episodestring = episodestring & "<actor>" & "<name>" & a(j) & "</name></actor>"
                                    '            Catch
                                    '            End Try
                                    '        Next
                                    '    End If
                                    'Catch
                                    'End Try
                                Case "Director"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    'tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.Trim("|")
                                    episodestring = episodestring & "<director>" & tempstring & "</director>"
                                Case "Writer"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    'tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.Trim("|")
                                    episodestring = episodestring & "<credits>" & tempstring & "</credits>"
                                Case "Overview"
                                    episodestring = episodestring & "<plot>" & mirrorselection.InnerXml & "</plot>"
                                Case "Rating"
                                    episodestring = episodestring & "<rating>" & mirrorselection.InnerXml & "</rating>"
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
            Return episodestring
        Catch ex As Exception
            Return "ERROR - <url>" & episodeurl & "</url>"
        Finally
            Monitor.Exit(Me)
        End Try

    End Function

End Class