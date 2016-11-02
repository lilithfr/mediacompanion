'Imports System.IO
Imports Alphaleonis.Win32.Filesystem
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Xml
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports Media_Companion

Module ext
    <System.Runtime.CompilerServices.Extension()> _
    Public Sub AppendChild(root As XmlElement, doc As XmlDocument, name As String, value As String)

        Dim child As XmlElement = doc.CreateElement(name)

        child.InnerText = value
        root.AppendChild(child)
    End Sub
End Module

Module Module1
    Public Const SetDefaults = True
    Dim arguments As New List(Of String)
    Dim mediaexportfile As String = ""
    Dim oProfiles As New Profiles
    Dim listofargs As New List(Of arguments)
    Dim profile As String = "default"
    Dim basictvlist As New List(Of basictvshownfo)
    Dim showstoscrapelist As New List(Of String)
    Dim newEpisodeList As New List(Of episodeinfo)
    Dim visible As Boolean = True
    Dim sw As IO.StreamWriter
    Dim logfile As String = "mc_com.log"
    Dim logstr As New List(Of String)
    Dim WithEvents scraper As New BackgroundWorker
    Dim WithEvents oMovies As New Movies(scraper)
    Dim tvdb As New TVDBScraper
    Dim EnvExit As Integer = 0
    Dim DoneAEp As Boolean = False
    Dim ShowTrailerDownloadProgess As Boolean = False
    Dim FileDownloadSize As Integer = -1
    Dim CursorLeft As Integer
    Dim CursorTop  As Integer
    Dim CompareType As StringComparison = StringComparison.CurrentCultureIgnoreCase

    Dim domovies            As Boolean = False
    Dim dotvepisodes        As Boolean = False
    Dim dotvmissingepthumb  As Boolean = False
    Dim domediaexport       As Boolean = False
    Dim docacheclean        As Boolean = False

    Private Declare Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Declare Function ShowWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Int32
    
    Dim newepisodetoadd As New episodeinfo

    Sub Main
        
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
        If Not visible Then   ShowWindow(GetConsoleWindow(), 0)  ' value of '0' = hide, '1' = visible
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
        
        ConsoleOrLog("Loading Config")
        Pref.SetUpPreferences()
        Call InitMediaFileExtensions()
        If Not Directory.Exists(Utilities.CacheFolderPath) Then Directory.CreateDirectory(Utilities.CacheFolderPath)

        If File.Exists(Pref.applicationPath & "\settings\profile.xml") = True Then
            oProfiles.Load
            If profile = "default" Then profile = oProfiles.DefaultProfile
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

        If domovies Then DoMoviesStart()

        If dotvepisodes OrElse dotvmissingepthumb Then DoTvEpisodeOrMissingThumb()

        If domediaexport = True Then DoExportMedia()
        If docacheclean Then DoCleanCache()

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
    End Sub

    Public Sub Writelogfile(ByVal log As List(Of String))
        Try
            Using sw As New IO.StreamWriter(logfile, true)
                For Each line In log
                    sw.WriteLine(line.TrimEnd)
                Next
                'sw.Close()
            End Using
        Catch
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

    Public Sub DoExportMedia()
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
    End Sub

    Public Sub DoCleanCache()
        ConsoleOrLog("Tv Cache cleaning commencing")
        Call tvcacheLoad()
        Call tvcacheClean()
        Call tvcacheSave()
        ConsoleOrLog("Tv Cache cleaning complete")
    End Sub

    Public Sub DoTvEpisodeOrMissingThumb()
        If File.Exists(Pref.workingProfile.tvcache) Then
            ConsoleOrLog("Loading Tv cache")
            Call tvcacheLoad()
        End If
        If File.Exists(Pref.workingProfile.regexlist) Then util_RegexLoad()
        If Pref.tv_RegexScraper.Count = 0 Then Pref.util_RegexSetDefaultScraper()
        If Pref.tv_RegexRename.Count = 0 Then Pref.util_RegexSetDefaultRename()

        For Each item In basictvlist
            If item.fullpath.IndexOf("tvshow.nfo", CompareType) <> -1 Then showstoscrapelist.Add(item.fullpath)
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
                Call MissingEpThumbDL()
            End If
                
        End If
    End Sub

    Public Sub DoMoviesStart()
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
    End Sub

    Private Sub episodescraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim language As String = ""
        Dim realshowpath As String = ""

        newEpisodeList.Clear()
        Dim newtvfolders As New List(Of String)

        Dim dirpath As String = String.Empty

        ConsoleOrLog("")
        ConsoleOrLog("")
        ConsoleOrLog("Starting TV Folder Scan")

        For Each tvshow In basictvlist
            If tvshow.locked = Nothing Then tvshow.locked = 0
            If tvshow.language = Nothing Then tvshow.language = "en"
            If tvshow.sortorder = Nothing Then tvshow.sortorder = "default"
        Next

        For Each tvfolder In listofshowfolders
            Dim add As Boolean = True
            For Each tvshow In basictvlist
                If tvshow.fullpath.IndexOf(tvfolder, CompareType) <> -1 Then
                    If tvshow.locked = True Or tvshow.locked = 2 Then
                        If manual = False Then
                            add = False
                            Exit For
                        End If
                    End If
                End If
            Next
            If add = True Then
                tvfolder = Path.GetDirectoryName(tvfolder)
                tempstring = "" 'tvfolder
                Dim hg As New DirectoryInfo(tvfolder)
                If hg.Exists Then
                    newtvfolders.Add(tvfolder)
                    Try
                        For Each strfolder As String In My.Computer.FileSystem.GetDirectories(tvfolder)
                            Try
                                If strfolder.IndexOf("System Volume Information", CompareType) = -1 Then
                                    newtvfolders.Add(strfolder)
                                    For Each strfolder2 As String In My.Computer.FileSystem.GetDirectories(strfolder, FileIO.SearchOption.SearchAllSubDirectories)
                                        Try
                                            If strfolder2.IndexOf("System Volume Information", CompareType) = -1 Then newtvfolders.Add(strfolder2)
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
            Dim dir_info As New DirectoryInfo(dirpath)
            If (dir_info.FullName.EndsWith(".actors", CompareType)) Then Continue For
            findnewepisodes(dirpath)
            tempint = newEpisodeList.Count - mediacounter
            If tempint > 0 Then
                ConsoleOrLog(tempint.ToString() & " New episodes found in directory:- " & dirpath)
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
            
            For Each Regexs In Pref.tv_RegexScraper
                S = newepisode.episodepath
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
                            Dim fileName As String = Path.GetFileNameWithoutExtension(file)
                            newepisode.filename = Path.GetFileName(newepisode.mediaextension)
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
                
                'check for multiepisode files
                Dim M2 As Match
                Dim epcount As Integer = 0
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
                            multieps.filepath = eps.filepath
                            episodearray.Add(multieps)
                            allepisodes(epcount) = Convert.ToDecimal(M2.Groups(3).Value, Utilities.defaultculture)
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
                    If episodearray(0).episodepath.IndexOf(Shows.fullpath.Replace("tvshow.nfo", ""), CompareType) <> -1 Then
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
                    Dim episodenumbers As String = ""
                    For Each ep In episodearray
                        If episodenumbers = "" Then
                            episodenumbers = ep.episodeno
                        Else
                            episodenumbers &= ", " & ep.episodeno
                        End If
                    Next
                    ConsoleOrLog("Season:  " & episodearray(0).seasonno)
                    ConsoleOrLog("Episodes:  " & episodenumbers)
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
                    Dim episodescraper As New TVDBScraper
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
                                End If
                            End If

                            If Pref.enablehdtags = True Then
                                Try
                                    singleepisode.filedetails = get_hdtags(getfilename(singleepisode.episodepath))
                                    If Not singleepisode.filedetails.filedetails_video.duration Is Nothing Then
                                        Dim minutes As Integer
                                        tempstring = singleepisode.filedetails.filedetails_video.duration
                                        If Not String.IsNullOrEmpty(tempstring) Then
                                            minutes =Math.Round(Convert.ToInt32(tempstring, Utilities.defaultculture)/60)
                                            singleepisode.runtime = minutes.ToString(Utilities.defaultculture) & " min"
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
        Dim xmlStreamDetails As XmlElement
        Dim xmlFileInfo As XmlElement
        Dim xmlStreamDetailsType As XmlElement
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
                    xmlStreamDetailsType.AppendChild(doc, "width"               , ep.filedetails.filedetails_video.width)
                    xmlStreamDetailsType.AppendChild(doc, "height"              , ep.filedetails.filedetails_video.height)
                    xmlStreamDetailsType.AppendChild(doc, "aspect"              , ep.filedetails.filedetails_video.aspect)
                    xmlStreamDetailsType.AppendChild(doc, "codec"               , ep.filedetails.filedetails_video.codec)
                    xmlStreamDetailsType.AppendChild(doc, "format"              , ep.filedetails.filedetails_video.formatinfo)
                    xmlStreamDetailsType.AppendChild(doc, "durationinseconds"   , ep.filedetails.filedetails_video.duration)
                    xmlStreamDetailsType.AppendChild(doc, "bitrate"             , ep.filedetails.filedetails_video.bitrate)
                    xmlStreamDetailsType.AppendChild(doc, "bitratemode"         , ep.filedetails.filedetails_video.bitratemode)
                    xmlStreamDetailsType.AppendChild(doc, "bitratemax"          , ep.filedetails.filedetails_video.bitratemax)
                    xmlStreamDetailsType.AppendChild(doc, "container"           , ep.filedetails.filedetails_video.container)
                    xmlStreamDetailsType.AppendChild(doc, "codecid"             , ep.filedetails.filedetails_video.codecid)
                    xmlStreamDetailsType.AppendChild(doc, "codecidinfo"         , ep.filedetails.filedetails_video.codecinfo)
                    xmlStreamDetailsType.AppendChild(doc, "scantype"            , ep.filedetails.filedetails_video.scantype)
                    xmlStreamDetails.AppendChild(xmlStreamDetailsType)

                    If ep.filedetails.filedetails_audio.Count > 0 Then
                        For Each item In ep.filedetails.filedetails_audio
                            xmlStreamDetailsType = doc.CreateElement("audio")
                            xmlStreamDetailsType.AppendChild(doc, "language"    , item.language)
                            xmlStreamDetailsType.AppendChild(doc, "codec"       , item.codec)
                            xmlStreamDetailsType.AppendChild(doc, "channels"    , item.channels)
                            xmlStreamDetailsType.AppendChild(doc, "bitrate"     , item.bitrate)
                            xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                        Next
                    End If
                    If ep.filedetails.filedetails_subtitles.Count > 0 Then
                        xmlStreamDetailsType = doc.CreateElement("subtitle")
                        For Each entry In ep.filedetails.filedetails_subtitles
                            xmlStreamDetailsType.AppendChild(doc, "language"    , entry.language)
                            xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                        Next
                    End If
                    xmlFileInfo.AppendChild(xmlStreamDetails)
                    xmlEpisode.AppendChild(xmlFileInfo)
                Catch
                End Try
            End If
            
            xmlEpisode.AppendChild(doc, "title"         , ep.title      )
            xmlEpisode.AppendChild(doc, "season"        , ep.seasonno   )
            xmlEpisode.AppendChild(doc, "episode"       , ep.episodeno  )
            xmlEpisode.AppendChild(doc, "aired"         , ep.aired      )
            xmlEpisode.AppendChild(doc, "plot"          , ep.plot       )
            xmlEpisode.AppendChild(doc, "playcount"     , ep.playcount  )
            xmlEpisode.AppendChild(doc, "director"      , ep.director   )
            xmlEpisode.AppendChild(doc, "credits"       , ep.credits    )
            xmlEpisode.AppendChild(doc, "rating"        , ep.rating     )
            xmlEpisode.AppendChild(doc, "votes"         , ep.votes      )
            xmlEpisode.AppendChild(doc, "uniqueid"      , ep.uniqueid   )
            xmlEpisode.AppendChild(doc, "runtime"       , ep.runtime    )
            xmlEpisode.AppendChild(doc, "showid"        , ep.showid     )
            
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
            
            'Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
            'output.Formatting = Formatting.Indented

            'doc.WriteTo(output)
            'output.Close()
            WorkingWithNfoFiles.SaveXMLDoc(doc, path)
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
                    Consoleorlog(Pref.tvScraperLog.Replace("!!! ", ""))
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

    Private Sub findnewepisodes(ByVal eppath As String)
        Dim dir_info As New DirectoryInfo(eppath)
        Dim fs_infos As List(Of FileInfo) = dir_info.GetFiles().ToList
        Dim filteredFiles As List(Of FileInfo) = fs_infos.Where(AddressOf IsMediaExtension).ToList
        For Each fs_info As FileInfo In filteredFiles
            Try
                Dim filename As String = Path.Combine(eppath, fs_info.Name)
                Dim filename2 As String = filename.Replace(Path.GetExtension(filename), ".nfo")

                If Not File.Exists(filename2) Then
                    Dim add As Boolean = True
                    If fs_info.Extension = ".vob" Then 'If a vob file is detected, check that it is not part of a dvd file structure
                        Dim name As String = filename2
                        name = name.Replace(Path.GetFileName(name), "VIDEO_TS.IFO")
                        If File.Exists(name) Then
                            add = False
                        End If
                    End If
                    If fs_info.Extension = ".rar" Then
                        Dim tempmovie As String = String.Empty
                        Dim tempint2 As Integer
                        Dim tempmovie2 As String = fs_info.FullName
                        If Path.GetExtension(tempmovie2).ToLower = ".rar" Then
                            If File.Exists(tempmovie2) = True Then
                                If File.Exists(tempmovie) = False Then
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
                                                'Dim stackrarexists As Boolean = False
                                                rarname = tempmovie.Replace(".nfo", ".rar")
                                                If rarname.ToLower.IndexOf(".part1.rar") <> -1 Then
                                                    rarname = rarname.Replace(".part1.rar", ".nfo")
                                                    If File.Exists(rarname) Then
                                                        'stackrarexists = True
                                                        tempmovie = rarname
                                                    Else
                                                        'stackrarexists = False
                                                        tempmovie = rarname
                                                    End If
                                                End If
                                                If rarname.ToLower.IndexOf(".part01.rar") <> -1 Then
                                                    rarname = rarname.Replace(".part01.rar", ".nfo")
                                                    If File.Exists(rarname) Then
                                                        'stackrarexists = True
                                                        tempmovie = rarname
                                                    Else
                                                        'stackrarexists = False
                                                        tempmovie = rarname
                                                    End If
                                                End If
                                                If rarname.ToLower.IndexOf(".part001.rar") <> -1 Then
                                                    rarname = rarname.Replace(".part001.rar", ".nfo")
                                                    If File.Exists(rarname) Then
                                                        tempmovie = rarname
                                                        'stackrarexists = True
                                                    Else
                                                        'stackrarexists = False
                                                        tempmovie = rarname
                                                    End If
                                                End If
                                                If rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                                                    rarname = rarname.Replace(".part0001.rar", ".nfo")
                                                    If File.Exists(rarname) Then
                                                        tempmovie = rarname
                                                        'stackrarexists = True
                                                    Else
                                                        'stackrarexists = False
                                                        tempmovie = rarname
                                                    End If
                                                End If
                                            Else
                                                add = False
                                            End If
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
                        newep.extension = Path.GetExtension(filename)
                        newEpisodeList.Add(newep)
                    End If
                End If
            Catch ex As Exception
            End Try
        Next fs_info
        fs_infos = Nothing
    End Sub

    Private Sub DlEpThumb(ByVal thisep As episodeinfo, ByVal eppath As String)
        Dim aok As Boolean = False
        Dim paths As New List(Of String)
        If Pref.EdenEnabled  Then   paths.Add(eppath.Replace(Path.GetExtension(eppath), ".tbn"))
        If Pref.FrodoEnabled Then   paths.Add(eppath.Replace(Path.GetExtension(eppath), "-thumb.jpg"))
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
            Dim eden As Boolean = Pref.EdenEnabled
            Dim frodo As Boolean = Pref.FrodoEnabled
            Dim artlist As New List(Of TvBanners)
            Dim thumblist As String = tvdb.GetPosterList(thisep.showid, artlist)
            Dim overwriteimage As Boolean = If(Pref.overwritethumbs, True, False)
            Dim doSeason As Boolean = Pref.tvdlseasonthumbs
            Dim isposter As String = Pref.postertype

            Dim Langlist As New List(Of String)
            Langlist.Add(langu)
            If Not Langlist.Contains("en") Then Langlist.Add("en")
            If Not Langlist.Contains(Pref.TvdbLanguageCode) Then Langlist.Add(Pref.TvdbLanguageCode)
            Langlist.Add("")
            
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
                    If Not File.Exists(seasonXXposterpath) Then
                        Utilities.DownloadFile(seasonXXposter, seasonXXposterpath)
                    End If
                    If File.Exists(seasonXXposterpath) And frodo And eden And isposter = "poster" Then
                        Utilities.SafeCopyFile(seasonXXposterpath, seasonXXposterpath.Replace("-poster.jpg", ".tbn"), overwriteimage)
                    End If
                    If Pref.seasonfolderjpg AndAlso thisep.filepath.Replace(realshowpath, "") <> "" Then
                        Dim TrueSeasonFolder As String = thisep.filepath & "folder.jpg"
                        If Not File.Exists(TrueSeasonFolder) AndAlso File.Exists(seasonXXposterpath) Then
                            Utilities.SafeCopyFile(seasonXXposterpath, TrueSeasonFolder)
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
                    If Not File.Exists(seasonXXbannerpath) Then
                        Utilities.DownloadFile(seasonXXbanner, seasonXXbannerpath)
                    End If
                    If File.Exists(seasonXXbannerpath) And frodo And eden And isposter = "banner" Then
                        Utilities.SafeCopyFile(seasonXXbannerpath, seasonXXbannerpath.Replace("-banner.jpg", ".tbn"), overwriteimage)
                    End If
                End If
            End If
        Catch
        End Try
    End Sub
    
    Private Sub MissingEpThumbDL()

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
                        If File.Exists(newtvshow.fullpath) Then
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
                Using tmpstrm As IO.StreamReader = File.OpenText(show.fullpath)
                    tvshowdata.Load(tmpstrm)
                End Using
                
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
        If File.Exists(fullpath) Then
            File.Delete(fullpath)
        End If
        Dim doc As New XmlDocument
        Dim root As XmlElement
        Dim child As XmlElement
        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        root = doc.CreateElement("tvcache")
        root.SetAttribute("ver", "3.5")
        For Each item In basictvlist
            child = doc.CreateElement("tvshow")
            child.SetAttribute("NfoPath", item.fullpath)
            child.AppendChild(doc, "playcount"          , item.playcount)
            child.AppendChild(doc, "state"              , item.locked)
            child.AppendChild(doc, "title"              , item.title)
            child.AppendChild(doc, "id"                 , item.id)
            child.AppendChild(doc, "status"             , item.status)
            child.AppendChild(doc, "sortorder"          , item.sortorder)
            child.AppendChild(doc, "language"           , item.language)
            child.AppendChild(doc, "episodeactorsource" , item.episodeactorsource)
            child.AppendChild(doc, "imdbid"             , item.imdbid)
            child.AppendChild(doc, "hidden"             , item.hidden)
            root.AppendChild(child)
        Next
        For Each item In basictvlist 
            For Each episode In item.allepisodes
                child = doc.CreateElement("episodedetails")
                child.SetAttribute("NfoPath", episode.episodepath)
                child.AppendChild(doc, "missing"        , episode.missing.ToLower)
                child.AppendChild(doc, "title"          , episode.Title)
                child.AppendChild(doc, "season"         , episode.seasonno)
                child.AppendChild(doc, "episode"        , episode.episodeno)
                child.AppendChild(doc, "aired"          , episode.Aired)
                child.AppendChild(doc, "showid"         , episode.showid)
                child.AppendChild(doc, "uniqueid"       , episode.uniqueid)
                child.AppendChild(doc, "epextn"         , episode.extension)
                child.AppendChild(doc, "playcount"      , episode.playcount)
                root.AppendChild(child)
            Next
        Next
        doc.AppendChild(root)
        Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
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
        If File.Exists(path) Then
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

    Public Function getfilename(ByVal eppath As String)
        'todo getfilename
        Dim monitorobject As New Object
        Monitor.Enter(monitorobject)
        Try
            Dim tempstring As String
            Dim tempfilename As String = eppath
            Dim actualpathandfilename As String = ""

            If String.IsNullOrEmpty(eppath) Then Return Nothing

            If File.Exists(tempfilename.Replace(Path.GetFileName(tempfilename), "VIDEO_TS.IFO")) Then
                actualpathandfilename = tempfilename.Replace(Path.GetFileName(tempfilename), "VIDEO_TS.IFO")
            End If

            If actualpathandfilename = "" Then
                For Each extn In MediaFileExtensions
                    tempfilename = tempfilename.Replace(Path.GetExtension(tempfilename), extn)
                    If File.Exists(tempfilename) Then
                        actualpathandfilename = tempfilename
                        Exit For
                    End If
                Next
            End If

            If actualpathandfilename = "" Then
                If tempfilename.IndexOf("movie.nfo", CompareType) <> -1 Then
                    Dim possiblemovies(1000) As String
                    Dim possiblemoviescount As Integer = 0
                    For Each extn In MediaFileExtensions
                        Dim dirpath As String = tempfilename.Replace(Path.GetFileName(tempfilename), "")
                        Dim dir_info As New DirectoryInfo(dirpath)
                        Dim pattern As String = "*" & extn
                        Dim fs_infos() As FileInfo = dir_info.GetFiles(pattern)
                        For Each fs_info As FileInfo In fs_infos
                            If File.Exists(fs_info.FullName) Then
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
                                    If workingtitle.IndexOf(workingstring, CompareType) <> -1 Then
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
            If Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
                Dim temppath As String = filename.Replace(Path.GetFileName(filename), "VTS_01_0.IFO")
                If File.Exists(temppath) Then
                    filename = temppath
                End If
            End If
            Dim newfiledetails As New FullFileDetails 
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
        'Dim webPg As String = String.Join( "" , webpage.ToArray() )
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

    Private Function IsMediaExtension(ByVal fileinfo As FileInfo) As Boolean
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
    End Sub

    Private Sub scraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)  Handles scraper.DoWork
        oMovies.FindNewMovies
    End Sub

    Private Sub scraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles scraper.ProgressChanged
        Dim oProgress As Progress = CType(e.UserState, Progress) 

        If Not IsNothing(oProgress.Log) Then
            If oProgress.Log.Contains("!!! ") Then ConsoleOrLog(oProgress.Log.Replace("!!! ", ""))
        End If
        Thread.Sleep(1)
    End Sub

    Private Sub FileDownload_SizeObtained(ByVal iFileSize As Long) Handles oMovies.FileDownloadSizeObtained
        FileDownloadSize = iFileSize
        If ShowTrailerDownloadProgess AndAlso visible Then
            Console.Write("Trailer download progress ")
            CursorLeft = Console.CursorLeft
            CursorTop  = Console.CursorTop
        End If
    End Sub
    
    Private Sub FileDownload_AmountDownloadedChanged(ByVal iTotalBytesRead As Long) Handles oMovies.AmountDownloadedChanged
        If ShowTrailerDownloadProgess AndAlso visible AndAlso FileDownloadSize>-1 Then
            Console.SetCursorPosition(CursorLeft,CursorTop)
            Console.Write("{0:0.0%}",iTotalBytesRead/FileDownloadSize)
        End If
    End Sub

    Private Sub FileDownload_FileDownloadComplete() Handles oMovies.FileDownloadComplete
        If ShowTrailerDownloadProgess AndAlso visible Then
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
    Public listactors As New List(Of str_MovieActors)
    Public filedetails As New fullfiledetails2

    Sub New()
        title           = ""
        showid          = ""
        credits         = ""
        director        = ""
        aired           = ""
        playcount       = ""
        thumb           = ""
        rating          = ""
        votes           = ""
        seasonno        = ""
        episodeno       = ""
        uniqueid        = ""
        imdbid          = ""
        filename        = ""
        filepath        = ""
        plot            = ""
        runtime         = ""
        fanartpath      = ""
        genre           = ""
        mediaextension  = ""
        episodepath     = ""
        missing         = ""
        extension       = ""
    End Sub
End Class

Public Class fullfiledetails2
    Public filedetails_video As New medianfo_video
    Public filedetails_audio As New List(Of medianfo_audio)
    Public filedetails_subtitles As New List(Of medianfo_subtitles)
End Class

Public Structure medianfo_audio
    Dim language As String
    Dim codec As String
    Dim channels As String
    Dim bitrate As String

    Sub New(SetDefaults As Boolean)
        language    = ""
        codec       = ""
        channels    = ""
        bitrate     = ""
    End Sub
End Structure

Public Structure medianfo_subtitles
    Dim language As String

    Sub New(SetDefaults As Boolean)
        language    = ""
    End Sub
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

    Sub New(SetDefaults As Boolean)
        width = ""
        height = ""
        aspect = ""
        codec = ""
        formatinfo = ""
        duration = ""
        bitrate = ""
        bitratemode = ""
        bitratemax = ""
        container = ""
        codecid = ""
        codecinfo = ""
        scantype = ""
    End Sub
End Structure

'Public Enum StreamKind As UInteger
'    General
'    Visual
'    Audio
'    Text
'    Chapters
'    Image
'    Menu
'    Max
'End Enum

'Public Enum InfoKind As UInteger
'    Name
'    Text
'    Measure
'    Options
'    NameText
'    MeasureText
'    Info
'    HowTo
'    Max
'End Enum

'Public Class mediainfo
'    Private Declare Unicode Function MediaInfo_New Lib "MediaInfo.DLL" () As IntPtr
'    Private Declare Unicode Sub MediaInfo_Delete Lib "MediaInfo.DLL" (ByVal Handle As IntPtr)
'    Private Declare Unicode Function MediaInfo_Open Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal FileName As String) As UIntPtr
'    Private Declare Unicode Sub MediaInfo_Close Lib "MediaInfo.DLL" (ByVal Handle As IntPtr)
'    Private Declare Unicode Function MediaInfo_Inform Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal Reserved As UIntPtr) As IntPtr
'    Private Declare Unicode Function MediaInfo_GetI Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As UIntPtr, ByVal KindOfInfo As UIntPtr) As IntPtr 'See MediaInfoDLL.h for enumeration values
'    Private Declare Unicode Function MediaInfo_Get Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As String, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
'    Private Declare Unicode Function MediaInfo_Option Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal Option_ As String, ByVal Value As String) As IntPtr
'    Private Declare Unicode Function MediaInfo_State_Get Lib "MediaInfo.DLL" (ByVal Handle As IntPtr) As UIntPtr 'see MediaInfo.h for details
'    Private Declare Unicode Function MediaInfo_Count_Get Lib "MediaInfo.DLL" (ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As IntPtr) As UIntPtr 'see MediaInfoDLL.h for enumeration values

'    Dim Handle As IntPtr

'    Sub New()
'        Handle = MediaInfo_New()
'    End Sub

'    Protected Overrides Sub Finalize()
'        MediaInfo_Delete(Handle)
'    End Sub

'    Function Open(ByVal FileName As String) As Integer
'        Return MediaInfo_Open(Handle, FileName)
'    End Function

'    Sub Close()
'        MediaInfo_Close(Handle)
'    End Sub

'    Function Inform() As String
'        Return Marshal.PtrToStringUni(MediaInfo_Inform(Handle, 0))
'    End Function

'    Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As Integer, Optional ByVal KindOfInfo As InfoKind = InfoKind.Text) As String
'        Return Marshal.PtrToStringUni(MediaInfo_GetI(Handle, StreamKind, StreamNumber, Parameter, KindOfInfo))
'    End Function

'    Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As String, Optional ByVal KindOfInfo As InfoKind = InfoKind.Text, Optional ByVal KindOfSearch As InfoKind = InfoKind.Name) As String
'        Return Marshal.PtrToStringUni(MediaInfo_Get(Handle, StreamKind, StreamNumber, Parameter, KindOfInfo, KindOfSearch))
'    End Function

'    Function Option_(ByVal Option__ As String, Optional ByVal Value As String = "") As String
'        Return Marshal.PtrToStringUni(MediaInfo_Option(Handle, Option__, Value))
'    End Function

'    Function State_Get() As Integer
'        Return MediaInfo_State_Get(Handle)
'    End Function

'    Function Count_Get(ByVal StreamKind As StreamKind, Optional ByVal StreamNumber As UInteger = UInteger.MaxValue) As Integer
'        If StreamNumber = UInteger.MaxValue Then
'            Dim A As Long
'            A = 0
'            A = A - 1 'If you know how to have (IntPtr)(-1) easier, I am interested ;-)
'            Return MediaInfo_Count_Get(Handle, StreamKind, A)
'        Else
'            Return MediaInfo_Count_Get(Handle, StreamKind, StreamNumber)
'        End If
'    End Function
'End Class
