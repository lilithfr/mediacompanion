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
    Dim basictvlist As New List(Of basictvshownfo)
    Dim defaultOfflineArt As String = ""
    Dim actorDB As New List(Of Databases)
    Dim showstoscrapelist As New List(Of String)
    Dim newEpisodeList As New List(Of episodeinfo)
    Dim defaultPoster As String = ""
    Dim visible As Boolean = True
    Dim sw As StreamWriter
    Dim logfile As String = "mc_com.log"
    Dim logstr As New List(Of String)
    Dim WithEvents scraper As New BackgroundWorker
    Dim WithEvents oMovies As New Movies(scraper)
    Dim EnvExit As Integer = 0
    Dim DoneAEp As Boolean = False
    Dim ShowTrailerDownloadProgess As Boolean = False
    Dim FileDownloadSize As Integer = -1
    Dim CursorLeft As Integer
    Dim CursorTop  As Integer
    Private Declare Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Declare Function ShowWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Int32
    
    Dim newepisodetoadd As New episodeinfo

    Sub Main
        Dim domovies            As Boolean = False
        Dim dotvepisodes        As Boolean = False
        Dim dotvmissingepthumb  As Boolean = False
        Dim domediaexport       As Boolean = False
        Dim docacheclean        As Boolean = False
        
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
                ElseIf arguments(f) = "-ex" Then
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
                'Show trailer download progress               
                ElseIf arguments(f) = "-d" Then
                    ShowTrailerDownloadProgess = True
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
            ConsoleOrLog("-ex to Scan and download missing episodes Thumbnails.")
            ConsoleOrLog("-x [templatename] [outputpath] to export media info list ")
            ConsoleOrLog("-v to run with no Console window. All information will be written")
            ConsoleOrLog("    to a log file in Media Companion's folder.  Log is overwritten")
            ConsoleOrLog("    each run of mc_com.exe")
            ConsoleOrLog("-d Shows trailer download progress to console. Ignored if -v specified.")
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
        Pref.applicationPath = AppDomain.CurrentDomain.BaseDirectory
        If Not visible Then 
            ShowWindow(GetConsoleWindow(), 0)  ' value of '0' = hide, '1' = visible
        End If
        LogStart
        
        For Each arg In listofargs
            If arg.switch   = "-m"  Then domovies = True
            If arg.switch   = "-e"  Then dotvepisodes = True
            If arg.switch   = "-p"  Then profile = arg.argu
            If arg.switch   = "-x"  Then domediaexport = True
            If arg.switch   = "-c"  Then docacheclean = True
            If arg.switch   = "-ex" Then dotvmissingepthumb = True
        Next

        Dim done As Boolean = False
        
        defaultPoster = Path.Combine(Pref.applicationPath, "Resources\default_poster.jpg")

        ConsoleOrLog("Loading Config")
        Pref.SetUpPreferences()
        Call InitMediaFileExtensions()
        If Not Directory.Exists(Utilities.CacheFolderPath) Then Directory.CreateDirectory(Utilities.CacheFolderPath)

        If File.Exists(Pref.applicationPath & "\settings\profile.xml") = True Then
             
            oProfiles.Load

            If profile = "default" Then
                profile = oProfiles.DefaultProfile
            End If
            For Each prof In oProfiles.ProfileList
                If prof.ProfileName = profile Then

                    prof.Assign(Pref.workingProfile)
                    
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
            ConsoleOrLog("Unable to find profile file: " & Pref.applicationPath & "\settings\profile.xml")
            ConsoleOrLog("****************************************************")
            EnvExit = 1
            Environment.Exit(EnvExit)
        End If
        defaultOfflineArt = Path.Combine(Pref.applicationPath, "Resources\default_offline.jpg")
        Pref.ConfigLoad()

        If domovies Or domediaexport Then
            If File.Exists(Pref.workingProfile.moviecache) Then
                ConsoleOrLog("Loading Movie cache")
                oMovies.LoadMovieCache
                oMovies.Rebuild_Data_GridViewMovieCache()
            End If
        End If

        Try
            ConsoleOrLog("Loading Movie Database caches")
            oMovies.LoadPeopleCaches
            oMovies.LoadMovieSetCache()
            oMovies.LoadTagCache()
        Catch
            oMovies.RebuildMoviePeopleCaches
        End Try

        If domovies Then

            StartNewMovies
            If Pref.DoneAMov Then
                EnvExit +=2
                oMovies.SaveMovieCache
                oMovies.SaveActorCache
                oMovies.SaveDirectorCache
                oMovies.SaveMovieSetCache 
                oMovies.SaveTagCache()
            End If
            ConsoleOrLog("")
        End If
        If dotvepisodes OrElse dotvmissingepthumb Then
            If IO.File.Exists(Pref.workingProfile.tvcache) Then
                ConsoleOrLog("Loading Tv cache")
                Call tvcacheLoad()
            End If
            If IO.File.Exists(Pref.workingProfile.regexlist) Then
                Call util_RegexLoad()
            End If
            If Pref.tv_RegexScraper.Count = 0 Then
                Pref.tv_RegexScraper.Add("[Ss]([\d]{1,4}).?[Ee]([\d]{1,4})")
                Pref.tv_RegexScraper.Add("([\d]{1,4}) ?[xX] ?([\d]{1,4})")
                Pref.tv_RegexScraper.Add("([0-9]+)([0-9][0-9])")
            End If
            If Pref.tv_RegexRename.Count = 0 Then
                Pref.tv_RegexRename.Add("Show Title - S01E01 - Episode Title.ext")
                Pref.tv_RegexRename.Add("S01E01 - Episode Title.ext")
                Pref.tv_RegexRename.Add("Show Title - 1x01 - Episode Title.ext")
                Pref.tv_RegexRename.Add("1x01 - Episode Title.ext")
                Pref.tv_RegexRename.Add("Show Title - 101 - Episode Title.ext")
                Pref.tv_RegexRename.Add("101 - Episode Title.ext")
            End If
            For Each item In basictvlist
                If item.fullpath.ToLower.IndexOf("tvshow.nfo") <> -1 Then
                    showstoscrapelist.Add(item.fullpath)
                End If
            Next
            If showstoscrapelist.Count > 0 Then
                If Not dotvmissingepthumb Then
                    Renamer.setRenamePref(Pref.tv_RegexRename.Item(Pref.tvrename), Pref.tv_RegexScraper)
                    Call episodescraper(showstoscrapelist, False)
                    If DoneAEp Then
                        Call tvcacheClean()
                        Call tvcacheSave()
                        EnvExit +=4
                    End If
                Else
                    Call MissingEpThumbDL(showstoscrapelist)
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
                        Dim title As String = Pref.RemoveIgnoredArticles(movie.title)
                        movie.title = title
                        If Pref.sorttitleignorearticle Then
                            Dim sorttitle As String = Pref.RemoveIgnoredArticles(movie.sortorder)
                            movie.sortorder = sorttitle
                        End If
                        Dim appendIncr As String = String.Empty
                        For strIncr = 1 To 5
	                        Select Case Pref.moviesortorder
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
                                Case 4
				                    key = String.Format("{0}{1}{2}{3}", movie.usrrated, movie.title, movie.id, appendIncr)
			                    Case 6
				                    key = String.Format("{0}{1}{2}{3}", movie.sortorder, movie.year, movie.id, appendIncr)
			                    Case 7
				                    key = String.Format("{0}{1}{2}{3}", movie.createdate, movie.title, movie.id, appendIncr)
			                    Case 8
				                    key = String.Format("{0}{1}{2}{3}", movie.Votes, movie.title, movie.id, appendIncr)
	                        End Select
                            If Not setMovies.ContainsKey(key) Then
			                    setMovies.Add(key, movie)
			                    Exit For
		                    End If
		                    appendIncr = strIncr
	                    Next
                    Next
                    Dim mediaCollection As Object = If(Pref.movieinvertorder, If(Pref.moviesortorder = 7, setMovies.Values.ToList, setMovies.Values.Reverse.ToList), If(Pref.moviesortorder = 7, setMovies.Values.Reverse.ToList, setMovies.Values.ToList))
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
        Writelogfile(logstr)
        If Not visible Then exitsound
        System.Environment.Exit(EnvExit)
    End Sub

    Public Sub LogStart
        logfile = Pref.applicationPath & logfile
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
        End If
        logstr.Add(str)
        'Using sw As New StreamWriter(logfile, true)
        '    sw.WriteLine(str.TrimEnd)
        '    sw.Close()
        'End Using
    End Sub

    Public Sub Writelogfile(ByVal log As List(Of String))
        Try
            Using sw As New StreamWriter(logfile, true)
                For Each line In log
                    sw.WriteLine(line.TrimEnd)
                Next
                sw.Close()
            End Using
        Catch ex As Exception
            sw.Close()
        End Try
    End Sub

    Public Sub exitsound
        'To Be completed to notify user mc_com has finished if not visible.
        Try
            My.Computer.Audio.Play(Path.Combine(Pref.applicationPath, "Resources\chimes.wav"))
            Threading.Thread.Sleep(500)
        Catch
        End Try
    End Sub

    Private Sub episodescraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim errorcounter As Integer = 0
        Dim language As String = ""
        Dim realshowpath As String = ""

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
            newepisodetoadd.showid = ""
            newepisodetoadd.playcount = ""
            newepisodetoadd.rating = ""
            newepisodetoadd.votes = ""
            newepisodetoadd.seasonno = ""
            newepisodetoadd.title = ""

            Dim episode As New episodeinfo

            For Each Regexs In Pref.tv_RegexScraper
                S = newepisode.episodepath '.ToLower
                S = S.Replace("x264", "")
                S = S.Replace("x265", "")
                S = S.Replace("720p", "")
                S = S.Replace("720i", "")
                S = S.Replace("1080p", "")
                S = S.Replace("1080i", "")
                S = S.Replace("X264", "")
                S = S.Replace("X265", "")
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
                            newepisode.filename = System.IO.Path.GetFileName(newepisode.mediaextension)
                            newepisode.filepath = newepisode.mediaextension.Replace(newepisode.filename, "")
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
            multieps2.filename = eps.filename
            multieps2.filepath = eps.filepath 
            episodearray.Add(multieps2)
            ConsoleOrLog(vbCrLf & "Working on episode: " & eps.episodepath)

            Dim removal As String = ""
            If eps.seasonno = "-1" Or eps.episodeno = "-1" Then
                eps.title = getfilename(eps.episodepath)
                eps.rating = "0"
                eps.votes = "0"
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
                Dim sortorder As String = ""
                Dim tvdbid As String = ""
                Dim imdbid As String = ""
                Dim actorsource As String = ""

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
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.director = newstring
                                            Case "credits"
                                                Dim newstring As String
                                                newstring = thisresult.InnerText
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.credits = newstring
                                            Case "rating"
                                                singleepisode.rating = thisresult.InnerText
                                            Case "ratedcount"
                                                singleepisode.votes = thisresult.InnerText
                                            Case "uniqueid"
                                                singleepisode.uniqueid = thisresult.InnerText 
                                            Case "imdbid"
                                                singleepisode.imdbid = thisresult.InnerText 
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
                                    Dim epid As String = ""
                                    If singleepisode.imdbid <> "" Then
                                        epid = singleepisode.imdbid
                                    Else
                                        'url = "http://www.imdb.com/title/" & imdbid & "/episodes?season=" & singleepisode.Season.Value
                                        epid = GetEpImdbId(imdbid, singleepisode.seasonno, singleepisode.episodeno)
                                    End If
                                    If epid.contains("tt") Then
                                        Dim scraperfunction As New Classimdb
                                        Dim tempactorlist As List(Of str_MovieActors) = scraperfunction.GetImdbActorsList(Pref.imdbmirror, epid, Pref.maxactors)
                                        If tempactorlist.Count > 0 Then
                                            ConsoleOrLog("Actors scraped from IMDB OK")
                                            While tempactorlist.Count > Pref.maxactors
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
                                    Else
                                        ConsoleOrLog("Actors not scraped from IMDB, reverting to TVDB actorlist")
                                    End If
                                    'Dim url As String
                                    'url = "http://www.imdb.com/title/" & imdbid & "/episodes"
                                    'Dim tvfblinecount As Integer = 0
                                    'Dim tvdbwebsource(10000)
                                    'tvfblinecount = 0
                                    'Try
                                    '    Dim wrGETURL As WebRequest
                                    '    wrGETURL = WebRequest.Create(url)
                                    '    Dim myProxy As New WebProxy("myproxy", 80)
                                    '    myProxy.BypassProxyOnLocal = True
                                    '    Dim objStream As Stream
                                    '    objStream = wrGETURL.GetResponse.GetResponseStream()
                                    '    Dim objReader As New StreamReader(objStream)
                                    '    Dim tvdbsLine As String = ""
                                    '    tvfblinecount = 0

                                    '    Do While Not tvdbsLine Is Nothing
                                    '        tvfblinecount += 1
                                    '        tvdbsLine = objReader.ReadLine
                                    '        If Not tvdbsLine Is Nothing Then
                                    '            tvdbwebsource(tvfblinecount) = tvdbsLine
                                    '        End If
                                    '    Loop
                                    '    objReader.Close()
                                    '    tvfblinecount -= 1
                                    'Catch ex As WebException
                                    '    tvdbwebsource(0) = "404"
                                    'End Try

                                    'If tvfblinecount <> 0 Then
                                    '    Dim tvtempstring As String
                                    '    tvtempstring = "Season " & singleepisode.seasonno & ", Episode " & singleepisode.episodeno & ":"
                                    '    For g = 1 To tvfblinecount
                                    '        If tvdbwebsource(g).indexof(tvtempstring) <> -1 Then
                                    '            Dim tvtempint As Integer
                                    '            tvtempint = tvdbwebsource(g).indexof("<a href=""/title/")
                                    '            If tvtempint <> -1 Then
                                    '                tvtempstring = tvdbwebsource(g).substring(tvtempint + 16, 9)
                                    '                Dim scraperfunction As New Classimdb
                                    '                Dim actorlist As String = ""
                                    '                actorlist = scraperfunction.getimdbactors(Pref.imdbmirror, tvtempstring)
                                    '                Dim tempactorlist As New List(Of str_MovieActors)
                                    '                Dim thumbstring As New XmlDocument
                                    '                Dim thisresult As XmlNode = Nothing
                                    '                Try
                                    '                    thumbstring.LoadXml(actorlist)
                                    '                    thisresult = Nothing
                                    '                    Dim countactors As Integer = 0
                                    '                    For Each thisresult In thumbstring("actorlist")
                                    '                        Select Case thisresult.Name
                                    '                            Case "actor"
                                    '                                If countactors >= Pref.maxactors Then
                                    '                                    Exit For
                                    '                                End If
                                    '                                countactors += 1
                                    '                                Dim newactor As New str_MovieActors
                                    '                                Dim detail As XmlNode = Nothing
                                    '                                For Each detail In thisresult.ChildNodes
                                    '                                    Select Case detail.Name
                                    '                                        Case "name"
                                    '                                            newactor.actorname = detail.InnerText
                                    '                                        Case "role"
                                    '                                            newactor.actorrole = detail.InnerText
                                    '                                        Case "thumb"
                                    '                                            newactor.actorthumb = detail.InnerText
                                    '                                        Case "actorid"
                                    '                                            If newactor.actorthumb <> Nothing Then
                                    '                                                If Pref.actorseasy = True And detail.InnerText <> "" Then
                                    '                                                    Dim workingpath As String = episodearray(0).episodepath.Replace(IO.Path.GetFileName(episodearray(0).episodepath), "")
                                    '                                                    workingpath = workingpath & ".actors\"
                                    '                                                    Dim hg As New IO.DirectoryInfo(workingpath)
                                    '                                                    Dim destsorted As Boolean = False
                                    '                                                    If Not hg.Exists Then
                                    '                                                        Try
                                    '                                                            IO.Directory.CreateDirectory(workingpath)
                                    '                                                            destsorted = True
                                    '                                                        Catch ex As Exception

                                    '                                                        End Try
                                    '                                                    Else
                                    '                                                        destsorted = True
                                    '                                                    End If
                                    '                                                    If destsorted = True Then
                                    '                                                        Dim filename As String = newactor.actorname.Replace(" ", "_")
                                    '                                                        filename = filename & ".tbn"
                                    '                                                        Dim tvshowactorpath As String = realshowpath
                                    '                                                        tvshowactorpath = tvshowactorpath.Replace(IO.Path.GetFileName(tvshowactorpath), "")
                                    '                                                        tvshowactorpath = IO.Path.Combine(tvshowactorpath, ".actors\")
                                    '                                                        tvshowactorpath = IO.Path.Combine(tvshowactorpath, filename)
                                    '                                                        filename = IO.Path.Combine(workingpath, filename)
                                    '                                                        If IO.File.Exists(tvshowactorpath) Then
                                    '                                                            Try
                                    '                                                                IO.File.Copy(tvshowactorpath, filename, True)
                                    '                                                            Catch
                                    '                                                            End Try
                                    '                                                        End If
                                    '                                                        If Not IO.File.Exists(filename) Then
                                    '                                                            Dim buffer(4000000) As Byte
                                    '                                                            Dim size As Integer = 0
                                    '                                                            Dim bytesRead As Integer = 0
                                    '                                                            Dim thumburl As String = newactor.actorthumb
                                    '                                                            Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                    '                                                            Dim res As HttpWebResponse = req.GetResponse()
                                    '                                                            Dim contents As Stream = res.GetResponseStream()
                                    '                                                            Dim bytesToRead As Integer = CInt(buffer.Length)
                                    '                                                            While bytesToRead > 0
                                    '                                                                size = contents.Read(buffer, bytesRead, bytesToRead)
                                    '                                                                If size = 0 Then Exit While
                                    '                                                                bytesToRead -= size
                                    '                                                                bytesRead += size
                                    '                                                            End While

                                    '                                                            Dim fstrm As New FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write)
                                    '                                                            fstrm.Write(buffer, 0, bytesRead)
                                    '                                                            contents.Close()
                                    '                                                            fstrm.Close()
                                    '                                                        End If
                                    '                                                    End If
                                    '                                                End If
                                    '                                                If Pref.actorsave = True And detail.InnerText <> "" And Pref.actorseasy = False Then
                                    '                                                    Dim workingpath As String = ""
                                    '                                                    Dim networkpath As String = Pref.actorsavepath
                                    '                                                    Try
                                    '                                                        tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                    '                                                        Dim hg As New IO.DirectoryInfo(tempstring)
                                    '                                                        If Not hg.Exists Then
                                    '                                                            IO.Directory.CreateDirectory(tempstring)
                                    '                                                        End If
                                    '                                                        workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                    '                                                        If Not IO.File.Exists(workingpath) Then
                                    '                                                            Dim buffer(4000000) As Byte
                                    '                                                            Dim size As Integer = 0
                                    '                                                            Dim bytesRead As Integer = 0
                                    '                                                            Dim thumburl As String = newactor.actorthumb
                                    '                                                            Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                    '                                                            Dim res As HttpWebResponse = req.GetResponse()
                                    '                                                            Dim contents As Stream = res.GetResponseStream()
                                    '                                                            Dim bytesToRead As Integer = CInt(buffer.Length)
                                    '                                                            While bytesToRead > 0
                                    '                                                                size = contents.Read(buffer, bytesRead, bytesToRead)
                                    '                                                                If size = 0 Then Exit While
                                    '                                                                bytesToRead -= size
                                    '                                                                bytesRead += size
                                    '                                                            End While
                                    '                                                            Dim fstrm As New FileStream(workingpath, FileMode.OpenOrCreate, FileAccess.Write)
                                    '                                                            fstrm.Write(buffer, 0, bytesRead)
                                    '                                                            contents.Close()
                                    '                                                            fstrm.Close()
                                    '                                                        End If
                                    '                                                        newactor.actorthumb = IO.Path.Combine(Pref.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                    '                                                        If Pref.actornetworkpath.IndexOf("/") <> -1 Then
                                    '                                                            newactor.actorthumb = Pref.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                    '                                                        Else
                                    '                                                            newactor.actorthumb = Pref.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                    '                                                        End If
                                    '                                                    Catch
                                    '                                                    End Try
                                    '                                                End If
                                    '                                            End If
                                    '                                    End Select
                                    '                                Next
                                    '                                tempactorlist.Add(newactor)
                                    '                        End Select
                                    '                    Next
                                    '                Catch ex As Exception
                                    '                    ConsoleOrLog("Error scraping episode actors from IMDB, " & ex.Message.ToString)
                                    '                End Try

                                    '                If tempactorlist.Count > 0 Then
                                    '                    ConsoleOrLog("Actors scraped from IMDB OK")
                                    '                    While tempactorlist.Count > Pref.maxactors
                                    '                        tempactorlist.RemoveAt(tempactorlist.Count - 1)
                                    '                    End While
                                    '                    singleepisode.listactors.Clear()
                                    '                    For Each actor In tempactorlist
                                    '                        singleepisode.listactors.Add(actor)
                                    '                    Next
                                    '                    tempactorlist.Clear()
                                    '                Else
                                    '                    ConsoleOrLog("Actors not scraped from IMDB, reverting to TVDB actorlist")
                                    '                End If

                                    '                Exit For
                                    '            End If
                                    '        End If
                                    '    Next
                                    'End If
                                End If
                            End If

                            If Pref.enablehdtags = True Then
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
                DlMissingSeasonArt(episodearray(0), language, realshowpath.Replace("tvshow.nfo", ""))
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
                                    newep.votes = epis.votes
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
            If Pref.enabletvhdtags = True Then
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

            xmlEpisodechild = doc.CreateElement("votes")
            xmlEpisodechild.InnerText = ep.votes
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
            If actorstosave > Pref.maxactors Then actorstosave = Pref.maxactors
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
        doc.AppendChild(root)
        Try
            'Dim settings As New XmlWriterSettings()
            'settings.Encoding = New UTF8Encoding(False)
            'settings.Indent = True
            'settings.IndentChars = (ControlChars.Tab)
            'settings.NewLineHandling = NewLineHandling.None
            'Dim writer As XmlWriter = XmlWriter.Create(path, settings)
            'doc.Save(writer)
            'writer.Close()
            'doc.AppendChild(root)
            Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented

            doc.WriteTo(output)
            output.Close()
        Catch
        End Try
    End Sub

    Private Sub addepisode(ByVal alleps As List(Of episodeinfo), ByVal path As String)
        ConsoleOrLog("Saving episode")

        If Pref.autorenameepisodes = True Then
            For Each show In basictvlist
                If alleps(0).episodepath.IndexOf(show.fullpath.Replace("\tvshow.nfo", "")) <> -1 Then
                    Dim eps As New List(Of String)
                    eps.Clear()
                    For Each ep In alleps
                        eps.Add(ep.episodeno)
                    Next
                    Pref.tvScraperLog = String.Empty
                    Dim tempspath As String = TVShows.episodeRename(path, alleps(0).seasonno, eps, show.title, alleps(0).title, Pref.TvRenameReplaceSpace, Pref.TvRenameReplaceSpaceDot)
                    Console.Write(Pref.tvScraperLog.Replace("!!! ", ""))
                    If tempspath <> "false" Then
                        path = tempspath
                        For each ep In alleps
                            ep.episodepath = path
                            ep.mediaextension = path.Replace(".nfo", ep.extension)
                            ep.filename = path.Replace(ep.filepath, "")
                        Next
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

        DlEpThumb(alleps(0), path)

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
                                    tempint2 = Convert.ToInt32(Pref.rarsize) * 1048576
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

    Private Sub DlEpThumb(ByVal thisep As episodeinfo, ByVal path As String)
        Dim aok As Boolean = False
        Dim paths As New List(Of String)
        If Pref.EdenEnabled  Then   paths.Add(path.Replace(IO.Path.GetExtension(path), ".tbn"))
        If Pref.FrodoEnabled Then   paths.Add(path.Replace(IO.Path.GetExtension(path), "-thumb.jpg"))
        Dim url As String = thisep.thumb
        If Not url = Nothing AndAlso url <> "http://www.thetvdb.com/banners/" Then
            aok = DownloadCache.SaveImageToCacheAndPaths(url, paths, True, 0, 0, True)
            If aok Then
                ConsoleOrLog("Thumbnail downloaded successfully")
            Else
                ConsoleOrLog("Failed to download Thumbnail")
            End If
        End If
        If Not aok AndAlso Pref.autoepisodescreenshot Then
            ConsoleOrLog("No Episode Thumb, AutoCreating ScreenShot from Episode file")
            Dim cachepathandfilename As String = Utilities.CreateScrnShotToCache(thisep.mediaextension, paths(0), Pref.ScrShtDelay)
            If Not cachepathandfilename = "" Then
                Dim imagearr() As Integer = GetAspect(thisep)
                If Pref.tvscrnshtTVDBResize AndAlso Not imagearr(0) = 0 Then
                    DownloadCache.CopyAndDownSizeImage(cachepathandfilename, paths(0), imagearr(0), imagearr(1))
                Else
                    File.Copy(cachepathandfilename, paths(0))
                End If
                If paths.Count > 1 Then File.Copy(paths(0), paths(1))
                ConsoleOrLog("Screenshot Saved O.K.")
            Else
                ConsoleOrLog("Screenshot save Failed!")
            End If
        End If
    End Sub

    Private Sub DlMissingSeasonArt(ByVal thisep As episodeinfo, langu As String, realshowpath As String)
        Try
            If realshowpath = "" Then Exit Sub
            Dim thisepseasonno As String = thisep.seasonno 
            Dim thisepseason As String = realshowpath + "season" + (If(thisepseasonno.ToInt < 10, "0" + thisepseasonno, thisepseasonno)) + (If(Pref.FrodoEnabled, "-poster.jpg", ".tbn"))
            If thisepseasonno = "0" Then thisepseason = realshowpath & "season-specials" & (If(Pref.FrodoEnabled, "-poster.jpg", ".tbn"))
            If File.Exists(thisepseason) Then Exit Sub
            Dim success As Boolean = False
            Dim showlist As New XmlDocument
            Dim eden As Boolean = Pref.EdenEnabled
            Dim frodo As Boolean = Pref.FrodoEnabled
            Dim thumblist As String = GetPosterList(thisep.showid)
            Dim overwriteimage As Boolean = If(Pref.overwritethumbs, True, False)
            Dim doPoster As Boolean = Pref.tvdlposter
            Dim doFanart As Boolean = Pref.tvdlfanart
            Dim doSeason As Boolean = Pref.tvdlseasonthumbs
            Dim isposter As String = Pref.postertype
            Dim isseasonall As String = Pref.seasonall

            Dim Langlist As New List(Of String)
            Langlist.Add(langu)
            If Not Langlist.Contains("en") Then Langlist.Add("en")
            If Not Langlist.Contains(Pref.TvdbLanguageCode) Then Langlist.Add(Pref.TvdbLanguageCode)
            Langlist.Add("")

            showlist.LoadXml(thumblist)
            Dim thisresult As XmlNode = Nothing
            Dim artlist As New List(Of TvBanners)
            artlist.Clear()
            For Each thisresult In showlist("banners")
                Select Case thisresult.Name
                    Case "banner"
                        Dim individualposter As New TvBanners
                        For Each results In thisresult.ChildNodes
                            Select Case results.Name
                                Case "id"
                                    individualposter.id = results.InnerText
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
                        artlist.Add(individualposter)
                End Select
            Next
            If artlist.Count = 0 Then
                Exit Sub
            End If
            Dim f As Integer = thisepseasonno.ToInt
            If (isposter = "poster" Or frodo) And doSeason Then 'poster
                Dim seasonXXposter As String = Nothing
                For Each lang In Langlist 
                    For Each Image In artlist
                        If Image.Season = f.ToString AndAlso (Image.Language = lang Or lang = "") Then
                            seasonXXposter = Image.Url
                            Exit For
                        End If
                    Next
                    If Not IsNothing(seasonXXposter) Then Exit For
                Next
                If Not IsNothing(seasonXXposter) Then
                    Dim tempstring As String = ""
                    If f < 10 Then
                        tempstring = "0" & f.ToString
                    Else
                        tempstring = f.ToString
                    End If
                    If tempstring = "00" Then tempstring = "-specials"
                    Dim seasonXXposterpath As String = ""
                    If frodo Then
                        seasonXXposterpath = realshowpath & "season" & tempstring & "-poster.jpg"
                    ElseIf eden Then
                        seasonXXposterpath = realshowpath & "season" & tempstring & ".tbn"
                    End If
                    If Not IO.File.Exists(seasonXXposterpath) Then
                        success = Utilities.DownloadFile(seasonXXposter, seasonXXposterpath)
                    End If
                    If IO.File.Exists(seasonXXposterpath) And frodo And eden And isposter = "poster" Then
                        success = Utilities.SafeCopyFile(seasonXXposterpath, seasonXXposterpath.Replace("-poster.jpg", ".tbn"), overwriteimage)
                    End If
                    If Pref.seasonfolderjpg AndAlso thisep.filepath.Replace(realshowpath, "") <> "" Then
                        Dim TrueSeasonFolder As String = thisep.filepath & "folder.jpg"
                        If Not File.Exists(TrueSeasonFolder) AndAlso File.Exists(seasonXXposterpath) Then
                            Utilities.SafeCopyFile(seasonXXposterpath, TrueSeasonFolder)
                            'Exit Sub
                        End If
                    End If
                End If
            End If

                'SeasonXX Banner
                If (isposter = "banner" Or frodo) And doSeason Then 'banner
                    Dim seasonXXbanner As String = Nothing
                    For Each lang In Langlist 
                        For Each Image In artlist
                            If Image.Season = f.ToString AndAlso (Image.Language = lang Or lang = "") AndAlso Image.Resolution = "seasonwide" Then
                                seasonXXbanner = Image.Url
                                Exit For
                            End If
                        Next
                        If Not IsNothing(seasonXXbanner) Then Exit For
                    Next
                    If seasonXXbanner <> "" Then
                        Dim tempstring As String = ""
                        If f < 10 Then
                            tempstring = "0" & f.ToString
                        Else
                            tempstring = f.ToString
                        End If
                        If tempstring = "00" Then tempstring = "-specials"
                        Dim seasonXXbannerpath As String = ""
                        If frodo Then
                            seasonXXbannerpath = realshowpath & "season" & tempstring & "-banner.jpg"
                        ElseIf eden Then
                            seasonXXbannerpath = realshowpath & "season" & tempstring & ".tbn"
                        End If
                        If Not IO.File.Exists(seasonXXbannerpath) Then
                            success = Utilities.DownloadFile(seasonXXbanner, seasonXXbannerpath)
                        End If
                        If IO.File.Exists(seasonXXbannerpath) And frodo And eden And isposter = "banner" Then
                            success = Utilities.SafeCopyFile(seasonXXbannerpath, seasonXXbannerpath.Replace("-banner.jpg", ".tbn"), overwriteimage)
                        End If
                    End If
                End If
            'Next
        Catch
        End Try
    End Sub

    Public Function getposterlist(ByVal tvdbid As String)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim wrGETURL As WebRequest
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/banners.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            wrGETURL.Proxy = Utilities.MyProxy
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

                                    bannerlist = bannerlist & "<url>http://www.thetvdb.com/banners/" & bannerselection.InnerXml & "</url>"
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
        Catch ex As WebException
            Return ex.ToString
        Catch EX As Exception
            Return EX.ToString
        End Try
    End Function

    Private Sub MissingEpThumbDL(ByVal listofshowfolders As List(Of String))

        Dim thumbextn As New List(Of String)
        If Pref.EdenEnabled Then thumbextn.Add(".tbn")
        If Pref.FrodoEnabled Then thumbextn.Add("-thumb.jpg")

        ConsoleOrLog("")
        ConsoleOrLog("")
        ConsoleOrLog("Starting Missing Thumbnail Scan" & vbcrlf)

        For Each show In basictvlist
            If show.locked = True OrElse show.locked > 0 Then Continue For
            For Each ep In show.allepisodes
                ep.mediaextension = ep.episodepath.Replace(".nfo", ep.extension)
                If Not File.Exists(ep.mediaextension) Then
                    ConsoleOrLog("Video file is missing, please complete a Full Refresh in Media Companion" & vbCrLf)
                    Continue For
                End If
                Dim imagepresent As Boolean = True
                For each extn In thumbextn
                    If Not File.Exists(ep.episodepath.Replace(".nfo", extn)) Then imagepresent = False
                Next
                If Not imagepresent Then 
                    If show.id = "" OrElse ep.uniqueid = "" Then 
                        ConsoleOrLog("Missing Show Id or Episode Id for: " & vbCrLf & ep.episodepath)
                        ConsoleOrLog(" *** Not able to download Thumbnail for this episode" & vbCrLf)
                        Continue For
                    End If
                    ConsoleOrLog("Missing Thumbnail found for: " & vbCrLf & show.title & " - " & "Season: " & ep.seasonno & ", Episode: " & ep.episodeno & ", Title: " & ep.title)
                    ep.thumb = String.Format("http://www.thetvdb.com/banners/episodes/{0}/{1}.jpg", show.id, ep.uniqueid)
                    DlEpThumb(ep, ep.episodepath)
                    ConsoleOrLog(vbCrLf)
                End If
            Next
        Next
    End Sub

    Private Sub tvcacheLoad()
        Dim unsortedepisodelist As New List(Of episodeinfo)
        unsortedepisodelist.Clear()
        basictvlist.Clear()

        Dim tvlist As New XmlDocument
        tvlist.Load(Pref.workingProfile.tvcache)
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
                                        newtvshow.title = Pref.RemoveIgnoredArticles(tempstring)
                                    Case "id"
                                        newtvshow.id = detail.InnerText
                                    Case "status"
                                        newtvshow.status = detail.InnerText 
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
                                    Case "hidden"
                                        newtvshow.hidden = detail.InnerText
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
        Dim fullpath As String = Pref.workingProfile.tvcache
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
            childchild = document.CreateElement("status")             : childchild.InnerText = item.status              : child.AppendChild(childchild)
            childchild = document.CreateElement("sortorder")          : childchild.InnerText = item.sortorder           : child.AppendChild(childchild)
            childchild = document.CreateElement("language")           : childchild.InnerText = item.language            : child.AppendChild(childchild)
            childchild = document.CreateElement("episodeactorsource") : childchild.InnerText = item.episodeactorsource  : child.AppendChild(childchild)
            childchild = document.CreateElement("imdbid")             : childchild.InnerText = item.imdbid              : child.AppendChild(childchild)
            childchild = document.CreateElement("hidden")             : childchild.InnerText = item.hidden              : child.AppendChild(childchild)
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
        tempstring = Pref.workingProfile.regexlist
        Pref.tv_RegexScraper.Clear()
        Pref.tv_RegexRename.Clear()
        Dim path As String = tempstring
        If IO.File.Exists(path) Then
            Try
                Dim regexlist As New XmlDocument
                regexlist.Load(path)
                If regexlist.DocumentElement.Name = "regexlist" Then
                    For Each result In regexlist("regexlist")
                        Select Case result.Name
                            Case "tvregex"                              'This is the old tag before custom renamer was introduced,
                                Pref.tv_RegexScraper.Add(result.InnerText)   'so add it to the scraper regex list in case there are custom regexs.
                            Case "tvregexscrape"
                                Pref.tv_RegexScraper.Add(result.InnerText)
                            Case "tvregexrename"
                                Pref.tv_RegexRename.Add(result.InnerText)
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
            newfiledetails = Pref.Get_HdTags(filename)
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

    Private Function GetAspect(ep As episodeinfo)
        Dim thisarray(2) As Integer
        thisarray(0) = 400
        thisarray(1) = 225
        Try
            Dim epw As Integer = ep.filedetails.filedetails_video.width.ToInt
            Dim eph As Integer= ep.filedetails.filedetails_video.height.ToInt
            Dim ThisAsp As Double = epw/eph
            If ThisAsp < 1.37 Then  'aspect greater than Industry Standard of 1.37:1 is classed as WideScreen
                thisarray(1) = 300
            End If
        Catch
            thisarray(0) = 0
        End Try
        Return thisarray
    End Function

    Private Function GetEpImdbId(ByVal ImdbId As String, ByVal SeasonNo As String, ByVal EpisodeNo As String) As String
        Dim url = "http://www.imdb.com/title/" & ImdbId & "/episodes?season=" & SeasonNo
        Dim webpage As New List(Of String)
        Dim s As New Classimdb
        webpage.Clear()
        webpage = s.loadwebpage(Pref.proxysettings, url,False,10)
        Dim webPg As String = String.Join( "" , webpage.ToArray() )
        Dim matchstring As String = "<strong><a href=""/title/tt"
        For f = 0 to webpage.Count -1
            Dim m As Match = Regex.Match(webpage(f), matchstring)
            If m.Success AndAlso webpage(f).Contains("ttep_ep"&EpisodeNo) Then
                Dim tmp As String = webpage(f)
                Dim n As Match = Regex.Match(tmp, "(tt\d{7})")
                If n.Success = True Then
                    url = n.Value
                    Exit For
                End If
            End If
        Next
        Return url
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
        'If Not visible Then
        '    For Each lgstr In logstr
        '        ConsoleOrLog(lgstr)
        '    Next
        'End If
    End Sub

Private Sub scraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)  Handles scraper.DoWork
    oMovies.FindNewMovies
End Sub

Private Sub scraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles scraper.ProgressChanged
    Dim oProgress As Progress = CType(e.UserState, Progress) 

    If Not IsNothing(oProgress.Log) Then
            If oProgress.Log.Contains("!!! ") Then
                'If Not visible Then
                '    logstr.Add(oProgress.Log.Replace("!!! ", ""))
               ' Else
                    ConsoleOrLog(oProgress.Log.Replace("!!! ", ""))
                'End If
            End If
        End If
        Thread.Sleep(1)
End Sub

Private Sub FileDownload_SizeObtained(ByVal iFileSize As Long) Handles oMovies.FileDownloadSizeObtained
    FileDownloadSize = iFileSize
    If ShowTrailerDownloadProgess and visible Then
        Console.Write("Trailer download progress ")
        CursorLeft = Console.CursorLeft
        CursorTop  = Console.CursorTop
    End If
End Sub


Private Sub FileDownload_AmountDownloadedChanged(ByVal iTotalBytesRead As Long) Handles oMovies.AmountDownloadedChanged
    If ShowTrailerDownloadProgess and visible and FileDownloadSize>-1 Then
        Console.SetCursorPosition(CursorLeft,CursorTop)
        Console.Write("{0:0.0%}",iTotalBytesRead/FileDownloadSize)
    End If
End Sub

Private Sub FileDownload_FileDownloadComplete() Handles oMovies.FileDownloadComplete
    If ShowTrailerDownloadProgess and visible Then
        Console.WriteLine("")
        Console.WriteLine("")
    End If
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
    Public status As String
    Public sortorder As String
    Public language As String
    Public episodeactorsource As String
    Public locked As String
    Public imdbid As String
    Public playcount As String
    Public hidden As String
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
    Public votes As String
    Public seasonno As String
    Public episodeno As String
    Public uniqueid As String
    Public imdbid As String
    Public filename As String
    Public filepath As String
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
            wrGETURL.Proxy = Utilities.MyProxy
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
            wrGETURL.Proxy = Utilities.MyProxy
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
        wrGETURL.Proxy = Utilities.MyProxy
        'Dim myProxy As New WebProxy("myproxy", 80)
        'myProxy.BypassProxyOnLocal = True
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
            wrGETURL.Proxy = Utilities.MyProxy
            'Dim myProxy As New WebProxy("myproxy", 80)
            'myProxy.BypassProxyOnLocal = True
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
            wrGETURL.Proxy = Utilities.MyProxy
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
            wrGETURL.Proxy = Utilities.MyProxy
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
        'If Pref.proxysettings.Item(0).ToLower = "false" Then
        '    ' Dim myProxy As New WebProxy("myproxy", 80)
        '    webReq.Proxy = Nothing
        'Else
        '    Dim myProxy As New WebProxy(Pref.proxysettings.Item(1), Pref.proxysettings.Item(2).ToInt) 'Convert.ToInt32(Utilities.MCProxy.Item(2)))
        '    myProxy.Credentials = New NetworkCredential(Pref.proxysettings.Item(3), Pref.proxysettings.item(4))
        '    webReq.Proxy = myProxy
        'End If
        webReq.Proxy = Utilities.MyProxy
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
                                Case "id"
                                    episodestring = episodestring & "<uniqueid>" & mirrorselection.InnerXml & "</uniqueid>"
                                Case "IMDB_ID"
                                    episodestring = episodestring & "<imdbid>" & mirrorselection.InnerXml & "</imdbid>"
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