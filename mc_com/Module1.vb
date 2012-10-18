Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
Imports System.Management
Imports System.Xml
Imports System.Reflection
Imports System.ComponentModel
Imports System
Imports System.Collections.Generic
Imports IMPA
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.IO.Compression
Imports Media_Companion
Imports Media_Companion.Movies


Module Module1
    Public Const SetDefaults = True
    Dim arguments As New List(Of String)
    Dim mediaexportfile As String = ""
    Dim profileStruct As New Profiles
    Dim listofargs As New List(Of arguments)
    Dim profile As String = "default"
    Dim fullMovieList As New List(Of str_ComboList)
    Dim basictvlist As New List(Of basictvshownfo)
    Dim imdbCounter As Integer = 0
    Dim defaultOfflineArt As String = ""
    Dim actorDB As New List(Of str_ActorDatabase)
    Dim showstoscrapelist As New List(Of String)
    Dim newEpisodeList As New List(Of episodeinfo)
    Dim defaultPoster As String = ""

    Sub Main()
        Dim domovies As Boolean = False
        Dim dotvepisodes As Boolean = False
        Dim domediaexport As Boolean = False
        Dim tempstring As String = ""
        Console.WriteLine("****************************************************")
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
                End If
            Next
        End If
        If listofargs.Count = 0 Then
            Dim arg As New arguments
            arg.switch = "help"
            listofargs.Add(arg)
        End If
        Dim setprofile As String = ""
        If listofargs(0).switch = "help" Then
            Console.WriteLine("Media Companion Command Line Tool")
            Console.WriteLine()
            Console.WriteLine("Useage")
            Console.WriteLine("mc_com.exe [-m] [-e] [-p ProfileName] [-x templatename outputpath]")
            Console.WriteLine("-m to scrape movies")
            Console.WriteLine("-e to scrape episodes")
            Console.WriteLine("-x [templatename] [outputpath] to export media info list ")
            Console.WriteLine()
            Console.WriteLine("Example")
            Console.WriteLine("mc_com.exe -m -e -p billy -x basiclist C:\Movielist\testfile.html")
            Console.WriteLine("will search for and scrape any new movies and episodes")
            Console.WriteLine("using the folders and settings of the 'billy' profile,")
            Console.WriteLine("then create a new media list using the named template")
            Console.WriteLine("Without the profile arg the defauld profile will be used")
            Console.WriteLine()
            Console.WriteLine("Tip: When using profile, template, or filenames that contain")
            Console.WriteLine("spaces, enclose with quotes, eg.")
            Console.WriteLine("mc_com.exe -m -p ""my profile"" -x ""new list"" ""C:\Movie list\test.html""")
            Console.WriteLine()
            Console.WriteLine("****************************************************")
            System.Environment.Exit(0)
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
        Next
        Dim done As Boolean = False
        Preferences.applicationPath = System.AppDomain.CurrentDomain.BaseDirectory
        defaultPoster = IO.Path.Combine(Preferences.applicationPath, "Resources\default_poster.jpg")
        Console.WriteLine("Loading Config")
        Preferences.SetUpPreferences()
        Call InitMediaFileExtensions()
        If IO.File.Exists(Preferences.applicationPath & "\settings\profile.xml") = True Then
            Call loadprofiles()
            If profile = "default" Then
                profile = profileStruct.defaultprofile
            End If
            For Each prof In profileStruct.profilelist
                If prof.profilename = profile Then
                    Preferences.workingProfile.actorcache = prof.actorcache
                    Preferences.workingProfile.config = prof.config
                    Preferences.workingProfile.moviecache = prof.moviecache
                    Preferences.workingProfile.profilename = prof.profilename
                    Preferences.workingProfile.regexlist = prof.regexlist
                    Preferences.workingProfile.filters = prof.filters
                    Preferences.workingProfile.tvcache = prof.tvcache
                    Preferences.workingProfile.profilename = prof.profilename
                    done = True
                    Exit For
                End If
            Next
            If done = False Then
                Console.WriteLine("Unable to find profile name: " & profile)
                Console.WriteLine("****************************************************")
                System.Environment.Exit(1)
            End If
        Else
            Console.WriteLine("Unable to find profile file: " & Preferences.applicationPath & "\settings\profile.xml")
            Console.WriteLine("****************************************************")
            System.Environment.Exit(1)
        End If
        defaultOfflineArt = IO.Path.Combine(Preferences.applicationPath, "Resources\default_offline.jpg")
        Preferences.LoadConfig()
        If domovies = True Or domediaexport = True Then
            If IO.File.Exists(Preferences.workingProfile.moviecache) Then
                Call loadmoviecache()
            End If
        End If
        If domovies = True Then
            Call startnewmovies()
            Call savemoviecache()
            Call saveactorcache()
            Console.WriteLine()
            Console.WriteLine("Movies search completed")
            Console.WriteLine()
            Console.WriteLine()
        End If
        If dotvepisodes = True Then
            If IO.File.Exists(Preferences.workingProfile.tvcache) Then
                Call loadtvcache()
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
                Call savetvcache()
            End If
        End If
        If domediaexport = True Then
            For Each arg In listofargs
                If arg.switch = "-x" Then
                    Console.WriteLine("Starting Media Info Export")
                    Console.WriteLine()
                    Dim mediaInfoExp As New MediaInfoExport
                    Dim mediaCollection As Object = fullMovieList
                    Call mediaInfoExp.addTemplates()
                    Dim templateType As MediaInfoExport.mediaType
                    If mediaInfoExp.setTemplate(arg.argu, templateType) AndAlso templateType = MediaInfoExport.mediaType.Movie Then
                        Call mediaInfoExp.createDocument(mediaexportfile, mediaCollection)
                    Else
                        Console.WriteLine("  Export aborted - template name provided is invalid")
                        Console.WriteLine("  (and only Movies are supported currently)")
                        Console.WriteLine()
                    End If
                    Console.WriteLine("Media Info Export complete")
                    Console.WriteLine()
                End If
            Next

        End If
        Console.WriteLine()
        Console.WriteLine("Tasks Completed")
        Console.WriteLine("****************************************************")
        System.Environment.Exit(0)
    End Sub

    ' Never called...

    'Public Function loadfullmovienfo(ByVal path As String)
    '    Try
    '        Dim newmovie As New fullmoviedetails
    '        Dim thumbstring As String = Nothing
    '        If Not IO.File.Exists(path) Then
    '        Else
    '            Dim movie As New XmlDocument
    '            Try
    '                movie.Load(path)
    '            Catch ex As Exception
    '                Dim errorstring As String
    '                errorstring = ex.Message.ToString & vbCrLf & vbCrLf
    '                errorstring += ex.StackTrace.ToString
    '                newmovie.fullmoviebody.title = Utilities.CleanFileName(IO.Path.GetFileName(path))
    '                newmovie.fullmoviebody.year = "0000"
    '                newmovie.fullmoviebody.top250 = "0"
    '                newmovie.fullmoviebody.playcount = "0"
    '                newmovie.fullmoviebody.credits = ""
    '                newmovie.fullmoviebody.director = ""
    '                newmovie.fullmoviebody.filename = ""
    '                newmovie.fullmoviebody.genre = ""
    '                newmovie.fullmoviebody.imdbid = ""
    '                newmovie.fullmoviebody.mpaa = ""
    '                newmovie.fullmoviebody.outline = "This nfo file could not be loaded"
    '                newmovie.fullmoviebody.playcount = "0"
    '                newmovie.fullmoviebody.plot = errorstring
    '                newmovie.fullmoviebody.premiered = ""
    '                newmovie.fullmoviebody.rating = ""
    '                newmovie.fullmoviebody.runtime = ""
    '                newmovie.fullmoviebody.studio = ""
    '                newmovie.fullmoviebody.tagline = "Rescrapeing the movie should fix the problem"
    '                newmovie.fullmoviebody.trailer = ""
    '                newmovie.fullmoviebody.votes = ""
    '                newmovie.fullmoviebody.sortorder = ""
    '                newmovie.fileinfo.createdate = "99999999"
    '                Return newmovie
    '                Exit Function
    '            End Try
    '            Dim thisresult As XmlNode = Nothing

    '            For Each thisresult In movie("movie")
    '                Select Case thisresult.Name
    '                    Case "alternativetitle"
    '                        newmovie.alternativetitles.Add(thisresult.InnerText)
    '                    Case "set"
    '                        newmovie.fullmoviebody.movieset = thisresult.InnerText
    '                    Case "sortorder"
    '                        newmovie.fullmoviebody.sortorder = thisresult.InnerText
    '                    Case "sorttitle"
    '                        newmovie.fullmoviebody.sortorder = thisresult.InnerText
    '                    Case "votes"
    '                        newmovie.fullmoviebody.votes = thisresult.InnerText
    '                    Case "outline"
    '                        newmovie.fullmoviebody.outline = thisresult.InnerText
    '                    Case "plot"
    '                        newmovie.fullmoviebody.plot = thisresult.InnerText
    '                    Case "tagline"
    '                        newmovie.fullmoviebody.tagline = thisresult.InnerText
    '                    Case "runtime"
    '                        newmovie.fullmoviebody.runtime = thisresult.InnerText
    '                    Case "mpaa"
    '                        newmovie.fullmoviebody.mpaa = thisresult.InnerText
    '                    Case "credits"
    '                        newmovie.fullmoviebody.credits = thisresult.InnerText
    '                    Case "director"
    '                        newmovie.fullmoviebody.director = thisresult.InnerText
    '                    Case "thumb"
    '                        If thisresult.InnerText.IndexOf("&lt;thumbs&gt;") <> -1 Then
    '                            thumbstring = thisresult.InnerText
    '                        Else
    '                            newmovie.listthumbs.Add(thisresult.InnerText)
    '                        End If

    '                    Case "premiered"
    '                        newmovie.fullmoviebody.premiered = thisresult.InnerText
    '                    Case "studio"
    '                        newmovie.fullmoviebody.studio = thisresult.InnerText
    '                    Case "trailer"
    '                        newmovie.fullmoviebody.trailer = thisresult.InnerText
    '                    Case "title"
    '                        newmovie.alternativetitles.Add(thisresult.InnerText)
    '                        newmovie.fullmoviebody.title = thisresult.InnerText
    '                    Case "year"
    '                        newmovie.fullmoviebody.year = thisresult.InnerText
    '                    Case "genre"
    '                        newmovie.fullmoviebody.genre = thisresult.InnerText
    '                    Case "id"
    '                        newmovie.fullmoviebody.imdbid = thisresult.InnerText
    '                    Case "playcount"
    '                        newmovie.fullmoviebody.playcount = thisresult.InnerText
    '                    Case "rating"
    '                        newmovie.fullmoviebody.rating = thisresult.InnerText
    '                        If newmovie.fullmoviebody.rating.IndexOf("/10") <> -1 Then newmovie.fullmoviebody.rating.Replace("/10", "")
    '                        If newmovie.fullmoviebody.rating.IndexOf(" ") <> -1 Then newmovie.fullmoviebody.rating.Replace(" ", "")
    '                    Case "top250"
    '                        newmovie.fullmoviebody.top250 = thisresult.InnerText
    '                    Case "createdate"
    '                        newmovie.fileinfo.createdate = thisresult.InnerText
    '                    Case "actor"
    '                        Dim newactor As New str_MovieActors
    '                        Dim detail As XmlNode = Nothing
    '                        For Each detail In thisresult.ChildNodes
    '                            Select Case detail.Name
    '                                Case "name"
    '                                    newactor.actorname = detail.InnerText
    '                                Case "role"
    '                                    newactor.actorrole = detail.InnerText
    '                                Case "thumb"
    '                                    newactor.actorthumb = detail.InnerText
    '                            End Select
    '                        Next
    '                        newmovie.listactors.Add(newactor)
    '                    Case "fileinfo"
    '                        Dim what As XmlNode = Nothing
    '                        For Each res In thisresult.ChildNodes
    '                            Select Case res.name
    '                                Case "streamdetails"
    '                                    Dim newfilenfo As New FullFileDetails 'fullfiledetails
    '                                    Dim detail As XmlNode = Nothing
    '                                    For Each detail In res.ChildNodes
    '                                        Select Case detail.Name
    '                                            Case "video"
    '                                                Dim videodetails As XmlNode = Nothing
    '                                                For Each videodetails In detail.ChildNodes
    '                                                    Select Case videodetails.Name
    '                                                        Case "width"
    '                                                            newfilenfo.filedetails_video.width = videodetails.InnerText
    '                                                        Case "height"
    '                                                            newfilenfo.filedetails_video.height = videodetails.InnerText
    '                                                        Case "aspect"
    '                                                            newfilenfo.filedetails_video.aspect = videodetails.InnerText
    '                                                        Case "codec"
    '                                                            newfilenfo.filedetails_video.codec = videodetails.InnerText
    '                                                        Case "formatinfo"
    '                                                            newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
    '                                                        Case "duration"
    '                                                            newfilenfo.filedetails_video.duration = videodetails.InnerText
    '                                                        Case "bitrate"
    '                                                            newfilenfo.filedetails_video.bitrate = videodetails.InnerText
    '                                                        Case "bitratemode"
    '                                                            newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
    '                                                        Case "bitratemax"
    '                                                            newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
    '                                                        Case "container"
    '                                                            newfilenfo.filedetails_video.container = videodetails.InnerText
    '                                                        Case "codecid"
    '                                                            newfilenfo.filedetails_video.codecid = videodetails.InnerText
    '                                                        Case "codecidinfo"
    '                                                            newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
    '                                                        Case "scantype"
    '                                                            newfilenfo.filedetails_video.scantype = videodetails.InnerText
    '                                                    End Select
    '                                                Next
    '                                            Case "audio"
    '                                                Dim audiodetails As XmlNode = Nothing
    '                                                Dim audio As New medianfo_audio
    '                                                For Each audiodetails In detail.ChildNodes

    '                                                    Select Case audiodetails.Name
    '                                                        Case "language"
    '                                                            audio.language = audiodetails.InnerText
    '                                                        Case "codec"
    '                                                            audio.codec = audiodetails.InnerText
    '                                                        Case "channels"
    '                                                            audio.channels = audiodetails.InnerText
    '                                                        Case "bitrate"
    '                                                            audio.bitrate = audiodetails.InnerText
    '                                                    End Select
    '                                                Next
    '                                                newfilenfo.filedetails_audio.Add(audio)
    '                                            Case "subtitle"
    '                                                Dim subsdetails As XmlNode = Nothing
    '                                                For Each subsdetails In detail.ChildNodes
    '                                                    Select Case subsdetails.Name
    '                                                        Case "language"
    '                                                            Dim sublang As New medianfo_subtitles
    '                                                            sublang.language = subsdetails.InnerText
    '                                                            newfilenfo.filedetails_subtitles.Add(sublang)
    '                                                    End Select
    '                                                Next
    '                                        End Select
    '                                    Next
    '                                    newmovie.filedetails = newfilenfo
    '                            End Select
    '                        Next
    '                End Select
    '            Next
    '            If thumbstring <> Nothing Then
    '                Do Until thumbstring.ToLower.IndexOf("http") = -1
    '                    Try
    '                        Dim tempstring As String
    '                        tempstring = thumbstring.ToLower.Substring(thumbstring.ToLower.LastIndexOf("http"), (thumbstring.ToLower.LastIndexOf(".jpg") + 4) - thumbstring.ToLower.LastIndexOf("http"))
    '                        thumbstring = thumbstring.ToLower.Replace(tempstring, "")
    '                        newmovie.listthumbs.Add(tempstring)
    '                    Catch
    '                        Exit Do
    '                    End Try
    '                Loop
    '            End If
    '            newmovie.fileinfo.fullpathandfilename = path
    '            newmovie.fileinfo.filename = IO.Path.GetFileName(path)
    '            newmovie.fileinfo.foldername = getlastfolder(path)
    '            newmovie.fileinfo.posterpath = Preferences.GetPosterPath(path)
    '            newmovie.fileinfo.trailerpath = ""
    '            newmovie.fileinfo.fanartpath = Preferences.GetFanartPath(path)

    '            Return newmovie
    '        End If
    '    Catch ex As Exception

    '    End Try
    '    Return Nothing
    'End Function

    Public Function resizeimage(ByVal bmp As Bitmap, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim bm_source As New Bitmap(bmp)
        Dim bm_dest As New Bitmap(width, height)
        Dim gr As Graphics = Graphics.FromImage(bm_dest)
        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
        gr.DrawImage(bm_source, 0, 0, width - 1, height - 1)
        Dim tempbitmap As Bitmap = bm_dest
        Return tempbitmap
    End Function

    Public Sub screenshot(ByVal fullnfopath As String, Optional ByVal overwrite As Boolean = False)
        Dim status As String = "working"
        Try

            Dim thumbpathandfilename As String = fullnfopath.Replace(IO.Path.GetExtension(fullnfopath), ".tbn")
            Dim skip As Boolean = False
            If Not IO.File.Exists(thumbpathandfilename) Or overwrite = True Then
                If IO.File.Exists(thumbpathandfilename) Then
                    Try
                        IO.File.Delete(thumbpathandfilename)
                    Catch
                        status = "nodelete"
                        skip = True
                    End Try
                End If
                If skip = False Then
                    Dim nfofilename As String = IO.Path.GetFileName(fullnfopath)
                    For j = 0 To MediaFileExtensions.Count - 1
                        Dim tempfilename As String = nfofilename
                        tempfilename = nfofilename.Replace(IO.Path.GetExtension(nfofilename), MediaFileExtensions(j))
                        Dim tempstring2 As String = fullnfopath.Replace(IO.Path.GetFileName(fullnfopath), tempfilename)
                        If IO.File.Exists(tempstring2) Then
                            Try
                                Dim seconds As Integer = 10
                                Dim myProcess As Process = New Process
                                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                                myProcess.StartInfo.CreateNoWindow = False
                                myProcess.StartInfo.FileName = Preferences.applicationPath & "ffmpeg.exe"
                                Dim proc_arguments As String = "-y -i """ & tempstring2 & """ -f mjpeg -ss " & seconds.ToString & " -vframes 1 -an " & """" & thumbpathandfilename & """"
                                myProcess.StartInfo.Arguments = proc_arguments
                                myProcess.Start()
                                myProcess.WaitForExit()
                                status = "done"
                                Exit For
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End If
            End If
        Catch
        Finally
        End Try
    End Sub

    Dim newepisodetoadd As New episodeinfo

    Private Sub episodescraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim errorcounter As Integer = 0

        newEpisodeList.Clear()
        Dim newtvfolders As New List(Of String)
        Dim progress As Integer
        progress = 0

        Dim dirpath As String = String.Empty

        Console.WriteLine("Starting TV Folder Scan")


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
                    Console.WriteLine("found " & hg.FullName.ToString)
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
                Console.WriteLine(vbCrLf & "Show Locked, Ignoring: " & tvfolder)
            End If
        Next
        Dim mediacounter As Integer = newEpisodeList.Count
        For g = 0 To newtvfolders.Count - 1
            'For f = 0 To MediaFileExtensions.Count - 1
            'moviepattern = MediaFileExtensions(f)
            dirpath = newtvfolders(g)
            Dim dir_info As New System.IO.DirectoryInfo(dirpath)
            If (dir_info.FullName.EndsWith(".actors")) Then Continue For
            findnewepisodes(dirpath)
            'Next f
            tempint = newEpisodeList.Count - mediacounter
            If tempint > 0 Then
                Console.WriteLine(tempint.ToString & " New episodes found in directory:- " & dirpath)
            End If
            mediacounter = newEpisodeList.Count
        Next g

        If newEpisodeList.Count <= 0 Then
            Console.WriteLine("No new episodes found, exiting scraper" & dirpath)
            Exit Sub
        End If

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
                            Console.WriteLine("Season and Episode information found for : " & fileName)
                        Else
                            Console.WriteLine("Cant extract Season and Episode deatails from filename: " & newepisode.seasonno & "x" & newepisode.episodeno)
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
            episodearray.Add(multieps2)
            Console.WriteLine(vbCrLf & "Working on episode: " & eps.episodepath)

            Dim removal As String = ""
            If eps.seasonno = "-1" Or eps.episodeno = "-1" Then
                eps.title = getfilename(eps.episodepath)
                eps.rating = "0"
                eps.playcount = "0"
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
                    Console.WriteLine("Multipart episode found: ")
                    Console.WriteLine("Season: " & episodearray(0).seasonno & " Episodes, ")
                    For Each ep In episodearray
                        Console.Write(ep.episodeno & ", ")
                    Next
                End If
                Console.WriteLine("Looking up scraper options from tvshow.nfo")

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
                                Console.WriteLine("This episode could not be found on TVDB using DVD sort order")
                                Console.WriteLine("Attempting to find using default sort order")
                                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                            End If
                        End If

                        If Utilities.UrlIsValid(episodeurl) Then


                            Dim tempepisode As String = episodescraper.getepisode(tvdbid, tempsortorder, singleepisode.seasonno, singleepisode.episodeno, language)
                            scrapedok = True
                            If tempepisode = Nothing Then
                                scrapedok = False
                                Console.WriteLine("This episode could not be found on TVDB")
                            End If
                            If scrapedok = True Then
                                Dim scrapedepisode As New XmlDocument
                                Try
                                    Console.WriteLine("Scraping body of episode: " & singleepisode.episodeno)
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
                                                newstring = newstring.TrimEnd("|")
                                                newstring = newstring.TrimStart("|")
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.director = newstring
                                            Case "credits"
                                                Dim newstring As String
                                                newstring = thisresult.InnerText
                                                newstring = newstring.TrimEnd("|")
                                                newstring = newstring.TrimStart("|")
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.credits = newstring
                                            Case "rating"
                                                singleepisode.rating = thisresult.InnerText
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
                                    Console.WriteLine("Error scraping episode body, " & ex.Message.ToString)
                                End Try

                                If actorsource = "imdb" Then
                                    Console.WriteLine("Scraping actors from IMDB")
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
                                                        Console.WriteLine("Error scraping episode actors from IMDB, " & ex.Message.ToString)
                                                    End Try

                                                    If tempactorlist.Count > 0 Then
                                                        Console.WriteLine("Actors scraped from IMDB OK")
                                                        While tempactorlist.Count > Preferences.maxactors
                                                            tempactorlist.RemoveAt(tempactorlist.Count - 1)
                                                        End While
                                                        singleepisode.listactors.Clear()
                                                        For Each actor In tempactorlist
                                                            singleepisode.listactors.Add(actor)
                                                        Next
                                                        tempactorlist.Clear()
                                                    Else
                                                        Console.WriteLine("Actors not scraped from IMDB, reverting to TVDB actorlist")
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
                                        '1h 24mn 48s 546ms
                                        Dim hours As Integer
                                        Dim minutes As Integer
                                        tempstring = singleepisode.filedetails.filedetails_video.duration
                                        tempint = tempstring.IndexOf("h")
                                        If tempint <> -1 Then
                                            hours = Convert.ToInt32(tempstring.Substring(0, tempint))
                                            tempstring = tempstring.Substring(tempint + 1, tempstring.Length - (tempint + 1))
                                            tempstring = Trim(tempstring)
                                        End If
                                        tempint = tempstring.IndexOf("mn")
                                        If tempint <> -1 Then
                                            minutes = Convert.ToInt32(tempstring.Substring(0, tempint))
                                        End If
                                        If hours <> 0 Then
                                            hours = hours * 60
                                        End If
                                        minutes = minutes + hours
                                        singleepisode.runtime = minutes.ToString & " min"
                                    End If
                                Catch
                                End Try
                            End If
                        Else
                            Console.WriteLine("Could not locate this episode on TVDB, or TVDB may be unavailable")
                        End If
                    Else
                        Console.WriteLine("No TVDB ID is available for this show, please scrape the show using the ""TV Show Selector"" TAB")
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
        If listofepisodes.Count = 1 Then
            Dim doc As New XmlDocument
            Dim root As XmlElement
            Dim child As XmlElement
            Dim actorchild As XmlElement
            Dim filedetailschild As XmlElement
            Dim filedetailschildchild As XmlElement
            root = doc.CreateElement("episodedetails")
            Dim thispref As XmlNode = Nothing
            Dim xmlproc As XmlDeclaration

            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            doc.AppendChild(xmlproc)
            Dim anotherchild As XmlNode = Nothing
            If Preferences.enabletvhdtags = True Then
                Try
                    child = doc.CreateElement("fileinfo")

                    anotherchild = doc.CreateElement("streamdetails")

                    filedetailschild = doc.CreateElement("video")
                    If listofepisodes(0).filedetails.filedetails_video.width <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.width <> "" Then
                            filedetailschildchild = doc.CreateElement("width")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.width
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.height <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.height <> "" Then
                            filedetailschildchild = doc.CreateElement("height")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.height
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.aspect <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.aspect <> "" Then
                            filedetailschildchild = doc.CreateElement("aspect")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.aspect
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.codec <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.codec <> "" Then
                            filedetailschildchild = doc.CreateElement("codec")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.codec
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.formatinfo <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.formatinfo <> "" Then
                            filedetailschildchild = doc.CreateElement("format")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.formatinfo
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.duration <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.duration <> "" Then
                            filedetailschildchild = doc.CreateElement("duration")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.duration
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.bitrate <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.bitrate <> "" Then
                            filedetailschildchild = doc.CreateElement("bitrate")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.bitrate
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.bitratemode <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.bitratemode <> "" Then
                            filedetailschildchild = doc.CreateElement("bitratemode")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.bitratemode
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.bitratemax <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.bitratemax <> "" Then
                            filedetailschildchild = doc.CreateElement("bitratemax")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.bitratemax
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.container <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.container <> "" Then
                            filedetailschildchild = doc.CreateElement("container")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.container
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.codecid <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.codecid <> "" Then
                            filedetailschildchild = doc.CreateElement("codecid")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.codecid
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.codecinfo <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.codecinfo <> "" Then
                            filedetailschildchild = doc.CreateElement("codecidinfo")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.codecinfo
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    If listofepisodes(0).filedetails.filedetails_video.scantype <> Nothing Then
                        If listofepisodes(0).filedetails.filedetails_video.scantype <> "" Then
                            filedetailschildchild = doc.CreateElement("scantype")
                            filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.scantype
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If
                    anotherchild.AppendChild(filedetailschild)

                    If listofepisodes(0).filedetails.filedetails_audio.Count > 0 Then
                        For Each item In listofepisodes(0).filedetails.filedetails_audio

                            filedetailschild = doc.CreateElement("audio")
                            If item.language <> Nothing Then
                                If item.language <> "" Then
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = item.language
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                            If item.codec <> Nothing Then
                                If item.codec <> "" Then
                                    filedetailschildchild = doc.CreateElement("codec")
                                    filedetailschildchild.InnerText = item.codec
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                            If item.channels <> Nothing Then
                                If item.channels <> "" Then
                                    filedetailschildchild = doc.CreateElement("channels")
                                    filedetailschildchild.InnerText = item.channels
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                            If item.bitrate <> Nothing Then
                                If item.bitrate <> "" Then
                                    filedetailschildchild = doc.CreateElement("bitrate")
                                    filedetailschildchild.InnerText = item.bitrate
                                    filedetailschild.AppendChild(filedetailschildchild)
                                    anotherchild.AppendChild(filedetailschild)
                                End If
                            End If
                        Next
                        If listofepisodes(0).filedetails.filedetails_subtitles.Count > 0 Then
                            filedetailschild = doc.CreateElement("subtitle")
                            For Each entry In listofepisodes(0).filedetails.filedetails_subtitles
                                If entry.language <> Nothing Then
                                    If entry.language <> "" Then
                                        filedetailschildchild = doc.CreateElement("language")
                                        filedetailschildchild.InnerText = entry.language
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                End If
                            Next
                            anotherchild.AppendChild(filedetailschild)
                        End If
                    End If
                    child.AppendChild(anotherchild)
                    root.AppendChild(child)
                Catch
                End Try
            End If

            child = doc.CreateElement("title")
            child.InnerText = listofepisodes(0).title
            root.AppendChild(child)

            child = doc.CreateElement("season")
            child.InnerText = listofepisodes(0).seasonno
            root.AppendChild(child)

            child = doc.CreateElement("episode")
            child.InnerText = listofepisodes(0).episodeno
            root.AppendChild(child)

            child = doc.CreateElement("aired")
            child.InnerText = listofepisodes(0).aired
            root.AppendChild(child)

            child = doc.CreateElement("plot")
            child.InnerText = listofepisodes(0).plot
            root.AppendChild(child)

            child = doc.CreateElement("playcount")
            child.InnerText = listofepisodes(0).playcount
            root.AppendChild(child)

            child = doc.CreateElement("director")
            child.InnerText = listofepisodes(0).director
            root.AppendChild(child)

            child = doc.CreateElement("credits")
            child.InnerText = listofepisodes(0).credits
            root.AppendChild(child)

            child = doc.CreateElement("rating")
            child.InnerText = listofepisodes(0).rating
            root.AppendChild(child)

            child = doc.CreateElement("runtime")
            child.InnerText = listofepisodes(0).runtime
            root.AppendChild(child)

            child = doc.CreateElement("showid")
            child.InnerText = listofepisodes(0).showid
            root.AppendChild(child)

            Dim actorstosave As Integer = listofepisodes(0).listactors.Count
            If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
            For f = 0 To actorstosave - 1
                child = doc.CreateElement("actor")
                actorchild = doc.CreateElement("name")
                actorchild.InnerText = listofepisodes(0).listactors(f).actorname
                child.AppendChild(actorchild)
                actorchild = doc.CreateElement("role")
                actorchild.InnerText = listofepisodes(0).listactors(f).actorrole
                child.AppendChild(actorchild)
                If listofepisodes(0).listactors(f).actorthumb <> Nothing Then
                    If listofepisodes(0).listactors(f).actorthumb <> "" Then
                        actorchild = doc.CreateElement("thumb")
                        actorchild.InnerText = listofepisodes(0).listactors(f).actorthumb
                        child.AppendChild(actorchild)
                    End If
                End If
                root.AppendChild(child)
            Next
            doc.AppendChild(root)
            Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented

            doc.WriteTo(output)
            output.Close()
        Else
            Dim document As New XmlDocument
            Dim root As XmlElement
            Dim child As XmlElement
            Dim childchild As XmlElement
            Dim childchildchild As XmlElement
            Dim childchildchildchild As XmlElement
            Dim middlechild As XmlElement

            Dim xmlproc As XmlDeclaration
            xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            document.AppendChild(xmlproc)

            root = document.CreateElement("multiepisodenfo")
            Dim done As Boolean = False
            For Each ep In listofepisodes
                child = document.CreateElement("episodedetails")
                If done = False Then
                    'done = True
                    If Preferences.enabletvhdtags = True Then
                        Try
                            middlechild = document.CreateElement("streamdetails")
                            childchild = document.CreateElement("fileinfo")
                            childchildchild = document.CreateElement("video")
                            If ep.filedetails.filedetails_video.width <> Nothing Then
                                If ep.filedetails.filedetails_video.width <> "" Then
                                    childchildchildchild = document.CreateElement("width")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.width
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.height <> Nothing Then
                                If ep.filedetails.filedetails_video.height <> "" Then
                                    childchildchildchild = document.CreateElement("height")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.height
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.codec <> Nothing Then
                                If ep.filedetails.filedetails_video.codec <> "" Then
                                    childchildchildchild = document.CreateElement("codec")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.codec
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.formatinfo <> Nothing Then
                                If ep.filedetails.filedetails_video.formatinfo <> "" Then
                                    childchildchildchild = document.CreateElement("format")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.formatinfo
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.duration <> Nothing Then
                                If ep.filedetails.filedetails_video.duration <> "" Then
                                    childchildchildchild = document.CreateElement("duration")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.duration
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.bitrate <> Nothing Then
                                If ep.filedetails.filedetails_video.bitrate <> "" Then
                                    childchildchildchild = document.CreateElement("width")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.bitrate
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.bitratemode <> Nothing Then
                                If ep.filedetails.filedetails_video.bitratemode <> "" Then
                                    childchildchildchild = document.CreateElement("bitratemode")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.bitratemode
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.bitratemax <> Nothing Then
                                If ep.filedetails.filedetails_video.bitratemax <> "" Then
                                    childchildchildchild = document.CreateElement("bitratemax")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.bitratemax
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.container <> Nothing Then
                                If ep.filedetails.filedetails_video.container <> "" Then
                                    childchildchildchild = document.CreateElement("container")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.container
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.codecid <> Nothing Then
                                If ep.filedetails.filedetails_video.codecid <> "" Then
                                    childchildchildchild = document.CreateElement("codecid")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.codecid
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.codecinfo <> Nothing Then
                                If ep.filedetails.filedetails_video.codecinfo <> "" Then
                                    childchildchildchild = document.CreateElement("codecidinfo")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.codecinfo
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            If ep.filedetails.filedetails_video.scantype <> Nothing Then
                                If ep.filedetails.filedetails_video.scantype <> "" Then
                                    childchildchildchild = document.CreateElement("scantype")
                                    childchildchildchild.InnerText = ep.filedetails.filedetails_video.scantype
                                    childchildchild.AppendChild(childchildchildchild)
                                End If
                            End If
                            childchild.AppendChild(childchildchild)
                            If ep.filedetails.filedetails_audio.Count > 0 Then
                                For Each aud In ep.filedetails.filedetails_audio
                                    childchildchild = document.CreateElement("audio")
                                    If aud.language <> Nothing Then
                                        If aud.language <> "" Then
                                            childchildchildchild = document.CreateElement("language")
                                            childchildchildchild.InnerText = aud.language
                                            childchildchild.AppendChild(childchildchildchild)
                                        End If
                                    End If
                                    If aud.codec <> Nothing Then
                                        If aud.codec <> "" Then
                                            childchildchildchild = document.CreateElement("codec")
                                            childchildchildchild.InnerText = aud.codec
                                            childchildchild.AppendChild(childchildchildchild)
                                        End If
                                    End If
                                    If aud.channels <> Nothing Then
                                        If aud.channels <> "" Then
                                            childchildchildchild = document.CreateElement("channels")
                                            childchildchildchild.InnerText = aud.channels
                                            childchildchild.AppendChild(childchildchildchild)
                                        End If
                                    End If
                                    If aud.bitrate <> Nothing Then
                                        If aud.bitrate <> "" Then
                                            childchildchildchild = document.CreateElement("bitrate")
                                            childchildchildchild.InnerText = aud.bitrate
                                            childchildchild.AppendChild(childchildchildchild)
                                        End If
                                    End If
                                    childchild.AppendChild(childchildchild)
                                Next
                            End If
                            If ep.filedetails.filedetails_subtitles.Count > 0 Then
                                For Each subt In ep.filedetails.filedetails_subtitles
                                    If subt.language <> Nothing Then
                                        If subt.language <> "" Then
                                            childchildchild = document.CreateElement("subtitle")
                                            childchildchildchild = document.CreateElement("language")
                                            childchildchildchild.InnerText = subt.language
                                            childchildchild.AppendChild(childchildchildchild)
                                        End If
                                    End If
                                    childchild.AppendChild(childchildchild)
                                Next
                            End If
                            middlechild.AppendChild(childchild)
                            child.AppendChild(middlechild)
                        Catch
                        End Try
                    End If
                End If

                childchild = document.CreateElement("title")
                childchild.InnerText = ep.title
                child.AppendChild(childchild)

                childchild = document.CreateElement("season")
                childchild.InnerText = ep.seasonno
                child.AppendChild(childchild)

                childchild = document.CreateElement("episode")
                childchild.InnerText = ep.episodeno
                child.AppendChild(childchild)

                childchild = document.CreateElement("playcount")
                childchild.InnerText = ep.playcount
                child.AppendChild(childchild)

                childchild = document.CreateElement("credits")
                childchild.InnerText = ep.credits
                child.AppendChild(childchild)

                childchild = document.CreateElement("director")
                childchild.InnerText = ep.director
                child.AppendChild(childchild)

                childchild = document.CreateElement("rating")
                childchild.InnerText = ep.rating
                child.AppendChild(childchild)

                childchild = document.CreateElement("aired")
                childchild.InnerText = ep.aired
                child.AppendChild(childchild)

                childchild = document.CreateElement("plot")
                childchild.InnerText = ep.plot
                child.AppendChild(childchild)

                childchild = document.CreateElement("thumb")
                childchild.InnerText = ep.thumb
                child.AppendChild(childchild)

                childchild = document.CreateElement("runtime")
                childchild.InnerText = ep.runtime
                child.AppendChild(childchild)

                child = document.CreateElement("showid")
                child.InnerText = listofepisodes(0).showid
                root.AppendChild(child)

                For Each actor In ep.listactors
                    Try
                        childchild = document.CreateElement("actor")
                        childchildchild = document.CreateElement("name")
                        childchildchild.InnerText = actor.actorname
                        childchild.AppendChild(childchildchild)
                        childchildchild = document.CreateElement("role")
                        childchildchild.InnerText = actor.actorrole
                        childchild.AppendChild(childchildchild)
                        childchildchild = document.CreateElement("thumb")
                        childchildchild.InnerText = actor.actorthumb
                        childchild.AppendChild(childchildchild)
                        child.AppendChild(childchild)
                    Catch
                    End Try
                Next
                root.AppendChild(child)
                document.AppendChild(root)
            Next
            Try
                Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
                output.Formatting = Formatting.Indented

                document.WriteTo(output)
                output.Close()
            Catch
            End Try
        End If
    End Sub

    Private Sub addepisode(ByVal alleps As List(Of episodeinfo), ByVal path As String)
        Console.WriteLine("Saving episode")

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
                    newwp.showid = Shows.id
                    '<episodedetails> attributes 'pure' and 'extension' no longer used? - HueyHQ
                    'Dim extension As String = ep.mediaextension
                    'extension = extension.Substring(extension.LastIndexOf("."), extension.Length - extension.LastIndexOf("."))
                    'Dim tempstring As String = ep.episodepath
                    'tempstring = tempstring.Substring(0, tempstring.LastIndexOf("."))
                    'If Not IsNothing(tempstring) Then
                    '    newwp.pure = tempstring
                    'End If
                    'If Not IsNothing(extension) Then
                    '    newwp.extension = extension
                    'End If
                    Shows.allepisodes.Add(newwp)

                Next
            End If
        Next
        Dim ext As String = path.Replace(IO.Path.GetExtension(path), ".tbn")

        If IO.File.Exists(ext) Or alleps(0).thumb = Nothing Then
        Else
            Dim buffer(400000) As Byte
            Dim size As Integer = 0
            Dim bytesRead As Integer = 0
            Dim url As String = alleps(0).thumb
            If url = Nothing Then
            Else
                If url.IndexOf("http") = 0 And url.IndexOf(".jpg") <> -1 Then
                    Try
                        Dim req As HttpWebRequest = WebRequest.Create(url)
                        Dim res As HttpWebResponse = req.GetResponse()
                        Dim contents As Stream = res.GetResponseStream()
                        Dim bytesToRead As Integer = CInt(buffer.Length)
                        While bytesToRead > 0
                            size = contents.Read(buffer, bytesRead, bytesToRead)
                            If size = 0 Then Exit While
                            bytesToRead -= size
                            bytesRead += size
                        End While
                        Try
                            Console.WriteLine("Saving Thumbnail To :- " & ext)
                            Dim fstrm As New FileStream(ext, FileMode.OpenOrCreate, FileAccess.Write)
                            fstrm.Write(buffer, 0, bytesRead)
                            contents.Close()
                            fstrm.Close()
                        Catch ex As Exception
                            Console.WriteLine("Unable to Save Thumb, Error: " & ex.Message.ToString)
                        End Try
                    Catch
                    End Try
                End If
            End If
        End If
        If Not IO.File.Exists(ext) And Preferences.autoepisodescreenshot = True Then
            Call screenshot(ext)
        End If
    End Sub

    'Private Function UrlIsValid(ByVal url As String) As Boolean
    '    Dim is_valid As Boolean = False
    '    If url.ToLower().StartsWith("www.") Then url = "http://" & url
    '    Dim web_response As HttpWebResponse = Nothing
    '    Try
    '        Dim web_request As HttpWebRequest = HttpWebRequest.Create(url)
    '        web_request.Timeout = 5000
    '        web_response = DirectCast(web_request.GetResponse(), HttpWebResponse)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    Finally
    '        If Not (web_response Is Nothing) Then _
    '            web_response.Close()
    '    End Try
    'End Function

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
                        newEpisodeList.Add(newep)
                    End If
                End If
            Catch ex As Exception

            End Try

        Next fs_info

        fs_infos = Nothing
    End Sub

    Private Sub loadtvcache()
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
                                    Case "title"
                                        Dim tempstring As String = ""
                                        tempstring = detail.InnerText
                                        If Preferences.ignorearticle = True Then
                                            If tempstring.ToLower.IndexOf("the ") = 0 Then
                                                tempstring = tempstring.Substring(4, tempstring.Length - 4)
                                                tempstring = tempstring & ", The"
                                            End If
                                        End If
                                        newtvshow.title = tempstring
                                    Case "fullpathandfilename"
                                        newtvshow.fullpath = detail.InnerText
                                    Case "id"
                                        newtvshow.id = detail.InnerText
                                End Select
                            Next
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
                                Case "missing"
                                    newepisode.missing = episodenew.innertext
                            End Select
                        Next
                        unsortedepisodelist.Add(newepisode)
                    End If
            End Select
        Next
        For Each show In basictvlist
            'fill in blanks
            Dim tvshowdata As New XmlDocument
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
                    Case "locked"
                        show.locked = thisresult.InnerText
                End Select
            Next
            For Each ep In unsortedepisodelist
                If ep.showid = show.id Then
                    show.allepisodes.Add(ep)
                End If
            Next
        Next
        'For Each ep In unsortedepisodelist
        '    For Each show In basictvlist
        '        If ep.showid = show.id Then
        '            show.allepisodes.Add(ep)
        '        End If
        '    Next
        'Next
    End Sub

    Private Sub savetvcache()
        Dim fullpath As String = Preferences.workingProfile.tvcache
        If IO.File.Exists(fullpath) Then
            IO.File.Delete(fullpath)
        End If
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("tvcache")
        root.SetAttribute("ver", "3.5")
        Dim childchild As XmlElement
        For Each item In basictvlist
            For Each episode In item.allepisodes
                child = doc.CreateElement("episodedetails")
                child.SetAttribute("NfoPath", episode.episodepath)
                '<episodedetails> attributes 'pure' and 'extension' no longer used? - HueyHQ
                'Dim extension As String = episode.episodepath
                'extension = extension.Substring(extension.LastIndexOf("."), extension.Length - extension.LastIndexOf("."))
                'Dim tempstring As String = episode.episodepath
                'tempstring = tempstring.Substring(0, tempstring.LastIndexOf("."))
                'If Not IsNothing(episode.pure) Then
                '    child.SetAttribute("PureName", episode.pure)
                'End If
                'If Not IsNothing(episode.extension) Then
                '    child.SetAttribute("MediaExtension", episode.extension)
                'End If
                'child.SetAttribute("MultiEpCount", "1")
                childchild = doc.CreateElement("title")
                childchild.InnerText = episode.title
                child.AppendChild(childchild)
                childchild = doc.CreateElement("season")
                childchild.InnerText = episode.seasonno
                child.AppendChild(childchild)
                childchild = doc.CreateElement("episode")
                childchild.InnerText = episode.episodeno
                child.AppendChild(childchild)
                childchild = doc.CreateElement("showid")
                childchild.InnerText = episode.showid
                child.AppendChild(childchild)
                childchild = doc.CreateElement("missing")
                childchild.InnerText = episode.missing
                child.AppendChild(childchild)
                root.AppendChild(child)
            Next
            child = doc.CreateElement("tvshow")
            child.SetAttribute("NfoPath", item.fullpath)
            childchild = doc.CreateElement("title")
            childchild.InnerText = item.title
            child.AppendChild(childchild)
            childchild = doc.CreateElement("id")
            childchild.InnerText = item.id
            child.AppendChild(childchild)
            root.AppendChild(child)
        Next
        doc.AppendChild(root)
        Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
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

    Private Sub loadmoviecache()
        fullMovieList.Clear()
        Dim movielist As New XmlDocument
        Dim objReader As New System.IO.StreamReader(Preferences.workingProfile.moviecache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close()

        movielist.LoadXml(tempstring)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In movielist("movie_cache")
            Select Case thisresult.Name
                Case "movie"
                    Dim newmovie As New str_ComboList
                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            'workingmovie.missingdata1
                            Case "missingdata1"
                                newmovie.missingdata1 = Convert.ToByte(detail.InnerText)
                            Case "set"
                                newmovie.movieset = detail.InnerText
                            Case "sortorder"
                                newmovie.sortorder = detail.InnerText
                            Case "filedate"
                                newmovie.filedate = detail.InnerText
                            Case "filename"
                                newmovie.filename = detail.InnerText
                            Case "foldername"
                                newmovie.foldername = detail.InnerText
                            Case "fullpathandfilename"
                                newmovie.fullpathandfilename = detail.InnerText
                            Case "genre"
                                newmovie.genre = detail.InnerText
                            Case "id"
                                newmovie.id = detail.InnerText
                            Case "playcount"
                                newmovie.playcount = detail.InnerText
                            Case "rating"
                                newmovie.rating = detail.InnerText
                            Case "title"
                                newmovie.title = detail.InnerText
                            Case "titleandyear"
                                newmovie.titleandyear = detail.InnerText
                            Case "top250"
                                newmovie.top250 = detail.InnerText
                            Case "year"
                                newmovie.year = detail.InnerText
                            Case "outline"
                                newmovie.outline = detail.InnerText
                            Case "runtime"
                                newmovie.runtime = detail.InnerText
                        End Select
                    Next
                    If newmovie.movieset = Nothing Then
                        newmovie.movieset = "None"
                    End If
                    If newmovie.movieset = "" Then
                        newmovie.movieset = "None"
                    End If
                    fullmovielist.Add(newmovie)
            End Select
        Next
    End Sub

    Private Sub savemoviecache()
        Dim fullpath As String = Preferences.workingProfile.moviecache
        If IO.File.Exists(fullpath) Then
            Dim don As Boolean = False
            Dim count As Integer = 0
            Do
                Try
                    IO.File.Delete(fullpath)
                    don = True
                Catch ex As Exception
                Finally
                    count += 1
                End Try
            Loop Until don = True
        End If
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("movie_cache")

        Dim childchild As XmlElement
        For Each movie In fullMovieList

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
            If movie.plot = Nothing then
                movie.plot = movie.outline
            End If
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
            '---- aqui....
            Try
                If movie.titleandyear.Length >= 5 Then
                    If movie.titleandyear.ToLower.IndexOf(", the") = movie.titleandyear.Length - 5 Then
                        Dim Temp As String = movie.titleandyear.Replace(", the", String.Empty)
                        movie.titleandyear = "The " & Temp
                    End If
                End If
            Catch ex As Exception
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
        For f = 1 To 100
            Try
                Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
                output.Formatting = Formatting.Indented
                doc.WriteTo(output)
                output.Close()
                Exit For
            Catch
            End Try
        Next
    End Sub

    Public Function decxmlchars(ByVal line As String)
        line = line.Replace("&amp;", "&")
        line = line.Replace("&lt;", "<")
        line = line.Replace("&gt;", ">")
        line = line.Replace("&quot;", "Chr(34)")
        line = line.Replace("&apos;", "'")
        line = line.Replace("&#xA;", vbCrLf)
        Return line
    End Function

    Private Sub loadprofiles()
        profileStruct.profilelist.Clear()
        Dim profilepath As String = IO.Path.Combine(Preferences.applicationPath, "settings")
        profilepath = IO.Path.Combine(profilepath, "profile.xml")

        Dim path As String = profilepath
        If IO.File.Exists(path) Then
            Try
                Dim profilelist As New XmlDocument
                profilelist.Load(path)
                If profilelist.DocumentElement.Name = "profile" Then
                    For Each thisresult In profilelist("profile")
                        Select Case thisresult.Name
                            Case "default"
                                profileStruct.defaultprofile = thisresult.innertext
                            Case "startup"
                                profileStruct.startupprofile = thisresult.innertext
                            Case "profiledetails"
                                Dim currentprofile As New str_ListOfProfiles(SetDefaults)
                                For Each result In thisresult.childnodes
                                    Select Case result.name
                                        Case "actorcache"
                                            currentprofile.actorcache = result.innertext
                                        Case "config"
                                            currentprofile.config = result.innertext
                                        Case "moviecache"
                                            currentprofile.moviecache = result.innertext
                                        Case "profilename"
                                            currentprofile.profilename = result.innertext
                                        Case "regex"
                                            currentprofile.regexlist = result.innertext
                                        Case "filters"
                                            currentprofile.filters = result.innertext
                                        Case "tvcache"
                                            currentprofile.tvcache = result.innertext
                                    End Select
                                Next
                                profileStruct.profilelist.Add(currentprofile)
                        End Select
                    Next
                End If
            Catch

            End Try
        End If
    End Sub

    Public Function getlastfolder(ByVal fullpath As String) As String
        Try
            If fullpath.IndexOf("/") <> -1 And fullpath.IndexOf("\") = -1 Then
                fullpath = fullpath.Replace("/", "\")
            End If
            fullpath = fullpath.Replace(IO.Path.GetFileName(fullpath), "")
            Dim foldername As String = ""
            Dim paths() As String
            paths = fullpath.Split("\")
            For g = UBound(paths) To 0 Step -1
                If paths(g).ToLower.IndexOf("video_ts") = -1 And paths(g) <> "" Then
                    foldername = paths(g)
                    Exit For
                End If
            Next
            Return foldername
        Catch
            Return ""
        End Try
    End Function


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
                For f = 0 To MediaFileExtensions.Count
                    tempfilename = tempfilename.Replace(IO.Path.GetExtension(tempfilename), MediaFileExtensions(f))
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
                    For f = 1 To 23
                        Dim dirpath As String = tempfilename.Replace(IO.Path.GetFileName(tempfilename), "")
                        Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                        Dim pattern As String = "*" & MediaFileExtensions(f)
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

    Public Sub EnumerateDirectories(ByRef directoryList As List(Of String), ByVal root As String)
        If (File.GetAttributes(root) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
            'ignore 
        Else
            If Utilities.ValidMovieDir(root) Then

                If Not (directoryList.Contains(root)) Then
                    directoryList.Add(root)
                    For Each s As String In Directory.GetDirectories(root)
                        EnumerateDirectories(directoryList, s)
                    Next
                End If
            End If
        End If
    End Sub

    Public Function EnumerateDirectory2(ByVal RootDirectory As String, Optional ByVal log As Boolean = False)
        Dim dli As New List(Of String)
        Try
            EnumerateDirectories(dli, RootDirectory)

            Return (dli)
        Catch ex As Exception
            Dim t As String = ex.ToString
            Return (dli)
        End Try
    End Function

    Public Function EnumerateDirectory3(ByVal RootDirectory As String)
        Dim dli As New List(Of String)
        Try
            'dli.Add(RootDirectory)
            For Each s As String In Directory.GetDirectories(RootDirectory)
                If Not (File.GetAttributes(s) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                    If Utilities.ValidMovieDir(s) Then
                        Dim exists As Boolean = False
                        For Each item In dli
                            If item = s Then exists = True
                        Next
                        If exists = False Then
                            dli.Add(s)
                            EnumerateDirectory3(s)
                        End If
                    End If
                End If
            Next s
            Return dli
        Catch ex As Exception
            Dim t As String = ex.ToString

            Return dli
        End Try
    End Function

    'Public Function validmoviedir(ByVal s As String) As Boolean
    '    Dim passed As Boolean = True
    '    Try
    '        For Each t As String In Directory.GetDirectories(s)
    '        Next
    '        Select Case True
    '            Case Strings.Right(s, 7).ToLower = "trailer"
    '                passed = False
    '            Case Strings.Right(s, 8).ToLower = "(noscan)"
    '                passed = False
    '            Case Strings.Right(s, 6).ToLower = "sample"
    '                passed = False
    '            Case Strings.Right(s, 8).ToLower = "recycler"
    '                passed = False
    '            Case s.ToLower.Contains("$recycle.bin")
    '                passed = False
    '            Case Strings.Right(s, 10).ToLower = "lost+found"
    '                passed = False
    '            Case s.ToLower.Contains("system volume information")
    '                passed = False
    '            Case s.Contains("MSOCache")
    '                passed = False
    '            Case s.EndsWith("Thumbnails")
    '                passed = False
    '            Case s.EndsWith(".actors")
    '                passed = False
    '        End Select
    '    Catch ex As Exception
    '        passed = False
    '    End Try
    '    Return passed
    'End Function

    'Public Function cleanfilename(ByVal filename As String, Optional ByVal withextension As Boolean = True)
    '    Dim cleanname As String = filename
    '    Try
    '        If withextension = True Then
    '            Try
    '                cleanname = filename.Replace(IO.Path.GetExtension(cleanname), "")
    '            Catch
    '            End Try
    '        End If
    '        Dim movieyear As String
    '        Dim S As String = cleanname
    '        Dim M As Match
    '        M = Regex.Match(S, "(\([\d]{4}\))")
    '        If M.Success = True Then
    '            movieyear = M.Value
    '        Else
    '            movieyear = Nothing
    '        End If
    '        If movieyear = Nothing Then
    '            M = Regex.Match(S, "(\[[\d]{4}\])")
    '            If M.Success = True Then
    '                movieyear = M.Value
    '            Else
    '                movieyear = Nothing
    '            End If
    '        End If

    '        filename = cleanname
    '        Dim removal(72) As String
    '        removal(1) = "cd1"
    '        removal(2) = "cd.1"
    '        removal(3) = "cd_1"
    '        removal(4) = "cd 1"
    '        removal(5) = "cd-1"
    '        removal(6) = "dvd1"
    '        removal(7) = "dvd.1"
    '        removal(8) = "dvd_1"
    '        removal(9) = "dvd 1"
    '        removal(10) = "dvd-1"
    '        removal(11) = "part1"
    '        removal(12) = "part.1"
    '        removal(13) = "part_1"
    '        removal(14) = "part 1"
    '        removal(15) = "part-1"
    '        removal(16) = "disk1"
    '        removal(17) = "disk.1"
    '        removal(18) = "disk_1"
    '        removal(19) = "disk 1"
    '        removal(20) = "disk-1"
    '        removal(21) = "pt1"
    '        removal(22) = "pt.1"
    '        removal(23) = "pt_1"
    '        removal(24) = "pt 1"
    '        removal(25) = "pt-1"
    '        removal(26) = "ac3"
    '        removal(27) = "divx"
    '        removal(28) = "xvid"
    '        removal(29) = "dvdrip"
    '        removal(30) = "directors cut"
    '        removal(31) = "special edition"
    '        removal(32) = "screener"
    '        removal(33) = "telesync"
    '        removal(34) = "telecine"
    '        removal(35) = "director's cut"
    '        removal(36) = " r5"
    '        removal(37) = " scr"
    '        removal(38) = ".scr"
    '        removal(39) = "_scr"
    '        removal(40) = "-scr"
    '        removal(41) = " ts"
    '        removal(42) = "_ts"
    '        removal(43) = ".ts"
    '        removal(44) = "-ts"
    '        removal(45) = " fs"
    '        removal(46) = ".fs"
    '        removal(47) = "_fs"
    '        removal(48) = "-fs"
    '        removal(49) = " ws"
    '        removal(50) = ".ws"
    '        removal(51) = "_ws"
    '        removal(52) = "-ws"
    '        removal(53) = "-r5"
    '        removal(54) = "_r5"
    '        removal(55) = ".r5"
    '        removal(56) = "bluray"
    '        removal(57) = "720"
    '        removal(58) = "1024"
    '        removal(59) = "fullscreen"
    '        removal(60) = "widescreen"
    '        removal(61) = "dvdscr"
    '        removal(62) = "part01"
    '        removal(63) = "dvd5"
    '        removal(64) = "dvd9"
    '        removal(65) = "dvd 5"
    '        removal(66) = "dvd 9"
    '        removal(67) = "dvd-5"
    '        removal(68) = "dvd-9"
    '        removal(69) = "dvd_5"
    '        removal(70) = "dvd_9"
    '        removal(71) = "dvd.5"
    '        removal(72) = "dvd.9"
    '        Dim currentposition As Integer = filename.Length
    '        Dim newposition As Integer = filename.Length
    '        For f = 1 To 72
    '            If filename.ToLower.IndexOf(removal(f)) <> -1 Then
    '                newposition = filename.ToLower.IndexOf(removal(f))
    '                If newposition < currentposition Then currentposition = newposition
    '            End If
    '        Next
    '        If movieyear <> Nothing Then
    '            If filename.IndexOf(movieyear) <> -1 Then
    '                newposition = filename.IndexOf(movieyear)
    '                If newposition < currentposition Then currentposition = newposition
    '            End If
    '        End If
    '        If currentposition < filename.Length And currentposition > 0 Then
    '            filename = filename.Substring(0, currentposition)
    '            If filename.Substring(filename.Length - 1, 1) = "-" Or filename.Substring(filename.Length - 1, 1) = "_" Or filename.Substring(filename.Length - 1, 1) = "." Or filename.Substring(filename.Length - 1, 1) = " " Then
    '                filename = filename.Substring(0, filename.Length - 1)
    '            End If
    '        End If

    '        If filename <> "" Then
    '            cleanname = filename
    '        End If
    '        cleanname = Trim(cleanname)
    '        Return cleanname
    '    Catch ex As Exception
    '        cleanname = "error"
    '        Return cleanname
    '    End Try
    'End Function

    Public Function get_hdtags(ByVal filename As String)
        Try
            If IO.Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
                Dim temppath As String = filename.Replace(IO.Path.GetFileName(filename), "VTS_01_0.IFO")
                If IO.File.Exists(temppath) Then
                    filename = temppath
                End If
            End If

            Dim playlist As New List(Of String)
            Dim tempstring As String
            tempstring = getfilename(filename)
            playlist = getmedialist(tempstring)

            If Not IO.File.Exists(filename) Then
                Return Nothing
            End If
            Dim workingfiledetails As New fullfiledetails2
            Dim MI As New mediainfo
            'MI = New mediainfo
            MI.Open(filename)
            Dim curVS As Integer = 0
            Dim addVS As Boolean = False
            Dim numOfVideoStreams As Integer = MI.Count_Get(StreamKind.Visual)

            Dim tempmediainfo As String
            Dim tempmediainfo2 As String

            workingfiledetails.filedetails_video.width = MI.Get_(StreamKind.Visual, curVS, "Width")
            workingfiledetails.filedetails_video.height = MI.Get_(StreamKind.Visual, curVS, "Height")
            If workingfiledetails.filedetails_video.width <> Nothing Then
                If IsNumeric(workingfiledetails.filedetails_video.width) Then
                    If workingfiledetails.filedetails_video.height <> Nothing Then
                        If IsNumeric(workingfiledetails.filedetails_video.height) Then
                            Dim tempwidth As Integer = Convert.ToInt32(workingfiledetails.filedetails_video.width)
                            Dim tempheight As Integer = Convert.ToInt32(workingfiledetails.filedetails_video.height)
                            Dim aspect As Decimal
                            Try
                                aspect = tempwidth / tempheight
                                aspect = FormatNumber(aspect, 3)
                                If aspect > 0 Then workingfiledetails.filedetails_video.aspect = aspect.ToString
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            End If
            'workingfiledetails.filedetails_video.aspect = MI.Get_(StreamKind.Visual, 0, 79)

            tempmediainfo = MI.Get_(StreamKind.Visual, curVS, "Format")
            If tempmediainfo.ToLower = "avc" Then
                tempmediainfo2 = "h264"
            Else
                tempmediainfo2 = tempmediainfo
            End If

            'workingfiledetails.filedetails_video.codec = tempmediainfo2
            'workingfiledetails.filedetails_video.formatinfo = tempmediainfo
            workingfiledetails.filedetails_video.codec = MI.Get_(StreamKind.Visual, curVS, "CodecID")
            If workingfiledetails.filedetails_video.codec = "DX50" Then
                workingfiledetails.filedetails_video.codec = "DIVX"
            End If
            '_MPEG4/ISO/AVC
            If workingfiledetails.filedetails_video.codec.ToLower.IndexOf("mpeg4/iso/avc") <> -1 Then
                workingfiledetails.filedetails_video.codec = "h264"
            End If
            workingfiledetails.filedetails_video.formatinfo = MI.Get_(StreamKind.Visual, curVS, "CodecID")
            Dim fs(100) As String
            For f = 1 To 100
                fs(f) = MI.Get_(StreamKind.Visual, 0, f)
            Next

            Try
                If playlist.Count = 1 Then
                    workingfiledetails.filedetails_video.duration = MI.Get_(StreamKind.Visual, 0, 61)
                ElseIf playlist.Count > 1 Then
                    Dim totalmins As Integer = 0
                    For f = 0 To playlist.Count - 1
                        Dim M2 As mediainfo
                        M2 = New mediainfo
                        M2.Open(playlist(f))
                        Dim temptime As String = M2.Get_(StreamKind.Visual, 0, 61)
                        Dim tempint As Integer
                        If temptime <> Nothing Then
                            Try
                                '1h 24mn 48s 546ms
                                Dim hours As Integer = 0
                                Dim minutes As Integer = 0
                                Dim tempstring2 As String = temptime
                                tempint = tempstring2.IndexOf("h")
                                If tempint <> -1 Then
                                    hours = Convert.ToInt32(tempstring2.Substring(0, tempint))
                                    tempstring2 = tempstring2.Substring(tempint + 1, tempstring2.Length - (tempint + 1))
                                    tempstring2 = Trim(tempstring2)
                                End If
                                tempint = tempstring2.IndexOf("mn")
                                If tempint <> -1 Then
                                    minutes = Convert.ToInt32(tempstring2.Substring(0, tempint))
                                End If
                                If hours <> 0 Then
                                    hours = hours * 60
                                End If
                                minutes = minutes + hours
                                totalmins = totalmins + minutes
                            Catch
                            End Try
                        End If
                    Next
                    workingfiledetails.filedetails_video.duration = totalmins & " min"
                End If
            Catch
                workingfiledetails.filedetails_video.duration = MI.Get_(StreamKind.Visual, 0, 57)
            End Try
            workingfiledetails.filedetails_video.bitrate = MI.Get_(StreamKind.Visual, curVS, "BitRate/String")
            workingfiledetails.filedetails_video.bitratemode = MI.Get_(StreamKind.Visual, curVS, "BitRate_Mode/String")

            workingfiledetails.filedetails_video.bitratemax = MI.Get_(StreamKind.Visual, curVS, "BitRate_Maximum/String")

            tempmediainfo = IO.Path.GetExtension(filename) '"This is the extension of the file"
            workingfiledetails.filedetails_video.container = tempmediainfo
            'workingfiledetails.filedetails_video.codecid = MI.Get_(StreamKind.Visual, curVS, "CodecID")

            workingfiledetails.filedetails_video.codecinfo = MI.Get_(StreamKind.Visual, curVS, "CodecID/Info")
            workingfiledetails.filedetails_video.scantype = MI.Get_(StreamKind.Visual, curVS, 102)
            'Video()
            'Format                     : MPEG-4 Visual
            'Format profile             : Streaming Video@L1
            'Format(settings, BVOP)     : Yes()
            'Format(settings, QPel)     : No()
            'Format(settings, GMC)      : No(warppoints)
            'Format(settings, Matrix)   : Custom()
            'Codec(ID)                  : XVID()
            'Codec(ID / Hint)           : XviD()
            'Duration                   : 1h 33mn
            'Bit rate                   : 903 Kbps
            'Width                      : 528 pixels
            'Height                     : 272 pixels
            'Display aspect ratio       : 1.941
            'Frame rate                 : 25.000 fps
            'Resolution                 : 24 bits
            'Colorimetry                : 4:2:0
            'Scan(Type)                 : Progressive()
            'Bits/(Pixel*Frame)         : 0.252
            'Stream size                : 604 MiB (86%)
            'Writing library            : XviD 1.0.3 (UTC 2004-12-20)

            Dim numOfAudioStreams As Integer = MI.Count_Get(StreamKind.Audio)
            Dim curAS As Integer = 0
            Dim addAS As Boolean = False

            'get audio data
            If numOfAudioStreams > 0 Then
                While curAS < numOfAudioStreams
                    Dim audio As New medianfo_audio
                    audio.language = getlangcode(MI.Get_(StreamKind.Audio, curAS, "Language/String"))
                    If MI.Get_(StreamKind.Audio, curAS, "Format") = "MPEG Audio" Then
                        audio.codec = "MP3"
                    Else
                        audio.codec = MI.Get_(StreamKind.Audio, curAS, "Format")
                    End If
                    If audio.codec = "AC-3" Then
                        audio.codec = "AC3"
                    End If
                    audio.channels = MI.Get_(StreamKind.Audio, curAS, "Channel(s)")
                    audio.bitrate = MI.Get_(StreamKind.Audio, curAS, "BitRate/String")
                    workingfiledetails.filedetails_audio.Add(audio)
                    curAS += 1
                End While
            End If


            Dim numOfSubtitleStreams As Integer = MI.Count_Get(StreamKind.Text)
            Dim curSS As Integer = 0
            If numOfSubtitleStreams > 0 Then
                While curSS < numOfSubtitleStreams
                    Dim sublanguage As New medianfo_subtitles
                    sublanguage.language = getlangcode(MI.Get_(StreamKind.Text, curSS, "Language/String"))
                    workingfiledetails.filedetails_subtitles.Add(sublanguage)
                    curSS += 1
                End While
            End If

            Return workingfiledetails
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    Public Function getlangcode(ByVal strLang As String) As String
        Dim monitorobject As New Object
        Monitor.Enter(monitorobject)
        Try
            Select Case strLang.ToLower
                Case "english"
                    Return "eng"
                Case "german"
                    Return "deu"
                Case ""
                    Return ""
                Case "afar"
                    Return "aar"
                Case "abkhazian"
                    Return "abk"
                Case "achinese"
                    Return "ace"
                Case "acoli"
                    Return "ach"
                Case "adangme"
                    Return "ada"
                Case "adyghe", "adygei"
                    Return "ady"
                Case "afro-asiatic (other)"
                    Return "afa"
                Case "afrihili"
                    Return "afh"
                Case "afrikaans"
                    Return "afr"
                Case "ainu"
                    Return "ain"
                Case "akan"
                    Return "aka"
                Case "akkadian"
                    Return "akk"
                Case "albanian"
                    Return "alb"
                Case "aleut"
                    Return "ale"
                Case "algonquian languages"
                    Return "alg"
                Case "southern altai"
                    Return "alt"
                Case "amharic"
                    Return "amh"
                Case "english"
                    Return "ang"
                Case "angika"
                    Return "anp"
                Case "apache languages"
                    Return "apa"
                Case "arabic"
                    Return "ara"
                Case "official aramaic (700-300 bce)", "imperial aramaic (700-300 bce)"
                    Return "arc"
                Case "aragonese"
                    Return "arg"
                Case "armenian"
                    Return "arm"
                Case "mapudungun", "mapuche"
                    Return "arn"
                Case "arapaho"
                    Return "arp"
                Case "artificial (other)"
                    Return "art"
                Case "arawak"
                    Return "arw"
                Case "assamese"
                    Return "asm"
                Case "asturian", "bable", "leonese", "asturleonese"
                    Return "ast"
                Case "athapascan languages"
                    Return "ath"
                Case "australian languages"
                    Return "aus"
                Case "avaric"
                    Return "ava"
                Case "avestan"
                    Return "ave"
                Case "awadhi"
                    Return "awa"
                Case "aymara"
                    Return "aym"
                Case "azerbaijani"
                    Return "aze"
                Case "banda languages"
                    Return "bad"
                Case "bamileke languages"
                    Return "bai"
                Case "bashkir"
                    Return "bak"
                Case "baluchi"
                    Return "bal"
                Case "bambara"
                    Return "bam"
                Case "balinese"
                    Return "ban"
                Case "basque"
                    Return "baq"
                Case "basa"
                    Return "bas"
                Case "baltic (other)"
                    Return "bat"
                Case "beja", "bedawiyet"
                    Return "bej"
                Case "belarusian"
                    Return "bel"
                Case "bemba"
                    Return "bem"
                Case "bengali"
                    Return "ben"
                Case "berber (other)"
                    Return "ber"
                Case "bhojpuri"
                    Return "bho"
                Case "bihari"
                    Return "bih"
                Case "bikol"
                    Return "bik"
                Case "bini", "edo"
                    Return "bin"
                Case "bislama"
                    Return "bis"
                Case "siksika"
                    Return "bla"
                Case "bantu (other)"
                    Return "bnt"
                Case "bosnian"
                    Return "bos"
                Case "braj"
                    Return "bra"
                Case "breton"
                    Return "bre"
                Case "batak languages"
                    Return "btk"
                Case "buriat"
                    Return "bua"
                Case "buginese"
                    Return "bug"
                Case "bulgarian"
                    Return "bul"
                Case "burmese"
                    Return "bur"
                Case "blin", "bilin"
                    Return "byn"
                Case "caddo"
                    Return "cad"
                Case "central american indian (other)"
                    Return "cai"
                Case "galibi carib"
                    Return "car"
                Case "catalan", "valencian"
                    Return "cat"
                Case "caucasian (other)"
                    Return "cau"
                Case "cebuano"
                    Return "ceb"
                Case "celtic (other)"
                    Return "cel"
                Case "chamorro"
                    Return "cha"
                Case "chibcha"
                    Return "chb"
                Case "chechen"
                    Return "che"
                Case "chagatai"
                    Return "chg"
                Case "chinese"
                    Return "chi"
                Case "chuukese"
                    Return "chk"
                Case "mari"
                    Return "chm"
                Case "chinook jargon"
                    Return "chn"
                Case "choctaw"
                    Return "cho"
                Case "chipewyan", "dene suline"
                    Return "chp"
                Case "cherokee"
                    Return "chr"
                Case "church slavic", "old slavonic", "church slavonic", "old bulgarian", "old church slavonic"
                    Return "chu"
                Case "chuvash"
                    Return "chv"
                Case "cheyenne"
                    Return "chy"
                Case "chamic languages"
                    Return "cmc"
                Case "coptic"
                    Return "cop"
                Case "cornish"
                    Return "cor"
                Case "corsican"
                    Return "cos"
                Case "creoles and pidgins"
                    Return "cpe"
                Case "creoles and pidgins"
                    Return "cpf"
                Case "creoles and pidgins"
                    Return "cpp"
                Case "cree"
                    Return "cre"
                Case "crimean tatar", "crimean turkish"
                    Return "crh"
                Case "creoles and pidgins (other)"
                    Return "crp"
                Case "kashubian"
                    Return "csb"
                Case "cushitic (other)"
                    Return "cus"
                Case "czech"
                    Return "cze"
                Case "dakota"
                    Return "dak"
                Case "danish"
                    Return "dan"
                Case "dargwa"
                    Return "dar"
                Case "land dayak languages"
                    Return "day"
                Case "delaware"
                    Return "del"
                Case "slave (athapascan)"
                    Return "den"
                Case "dogrib"
                    Return "dgr"
                Case "dinka"
                    Return "din"
                Case "divehi", "dhivehi", "maldivian"
                    Return "div"
                Case "dogri"
                    Return "doi"
                Case "dravidian (other)"
                    Return "dra"
                Case "lower sorbian"
                    Return "dsb"
                Case "duala"
                    Return "dua"
                Case "dutch"
                    Return "dum"
                Case "dutch", "flemish"
                    Return "dut"
                Case "dyula"
                    Return "dyu"
                Case "dzongkha"
                    Return "dzo"
                Case "efik"
                    Return "efi"
                Case "egyptian (ancient)"
                    Return "egy"
                Case "ekajuk"
                    Return "eka"
                Case "elamite"
                    Return "elx"
                Case "english"
                    Return "eng"
                Case "english"
                    Return "enm"
                Case "esperanto"
                    Return "epo"
                Case "estonian"
                    Return "est"
                Case "ewe"
                    Return "ewe"
                Case "ewondo"
                    Return "ewo"
                Case "fang"
                    Return "fan"
                Case "faroese"
                    Return "fao"
                Case "fanti"
                    Return "fat"
                Case "fijian"
                    Return "fij"
                Case "filipino", "pilipino"
                    Return "fil"
                Case "finnish"
                    Return "fin"
                Case "finno-ugrian (other)"
                    Return "fiu"
                Case "fon"
                    Return "fon"
                Case "french"
                    Return "fre"
                Case "french"
                    Return "frm"
                Case "french"
                    Return "fro"
                Case "northern frisian"
                    Return "frr"
                Case "eastern frisian"
                    Return "frs"
                Case "western frisian"
                    Return "fry"
                Case "fulah"
                    Return "ful"
                Case "friulian"
                    Return "fur"
                Case "ga"
                    Return "gaa"
                Case "gayo"
                    Return "gay"
                Case "gbaya"
                    Return "gba"
                Case "germanic (other)"
                    Return "gem"
                Case "georgian"
                    Return "geo"
                Case "german"
                    Return "ger"
                Case "geez"
                    Return "gez"
                Case "gilbertese"
                    Return "gil"
                Case "gaelic", "scottish gaelic"
                    Return "gla"
                Case "irish"
                    Return "gle"
                Case "galician"
                    Return "glg"
                Case "manx"
                    Return "glv"
                Case "german"
                    Return "gmh"
                Case "german"
                    Return "goh"
                Case "gondi"
                    Return "gon"
                Case "gorontalo"
                    Return "gor"
                Case "gothic"
                    Return "got"
                Case "grebo"
                    Return "grb"
                Case "greek"
                    Return "grc"
                Case "greek"
                    Return "gre"
                Case "guarani"
                    Return "grn"
                Case "swiss german", "alemannic", "alsatian"
                    Return "gsw"
                Case "gujarati"
                    Return "guj"
                Case "gwich'in"
                    Return "gwi"
                Case "haida"
                    Return "hai"
                Case "haitian", "haitian creole"
                    Return "hat"
                Case "hausa"
                    Return "hau"
                Case "hawaiian"
                    Return "haw"
                Case "hebrew"
                    Return "heb"
                Case "herero"
                    Return "her"
                Case "hiligaynon"
                    Return "hil"
                Case "himachali"
                    Return "him"
                Case "hindi"
                    Return "hin"
                Case "hittite"
                    Return "hit"
                Case "hmong"
                    Return "hmn"
                Case "hiri motu"
                    Return "hmo"
                Case "croatian"
                    Return "hrv"
                Case "upper sorbian"
                    Return "hsb"
                Case "hungarian"
                    Return "hun"
                Case "hupa"
                    Return "hup"
                Case "iban"
                    Return "iba"
                Case "igbo"
                    Return "ibo"
                Case "icelandic"
                    Return "ice"
                Case "ido"
                    Return "ido"
                Case "sichuan yi", "nuosu"
                    Return "iii"
                Case "ijo languages"
                    Return "ijo"
                Case "inuktitut"
                    Return "iku"
                Case "interlingue", "occidental"
                    Return "ile"
                Case "iloko"
                    Return "ilo"
                Case "interlingua (international auxiliary language association)"
                    Return "ina"
                Case "indic (other)"
                    Return "inc"
                Case "indonesian"
                    Return "ind"
                Case "indo-european (other)"
                    Return "ine"
                Case "ingush"
                    Return "inh"
                Case "inupiaq"
                    Return "ipk"
                Case "iranian (other)"
                    Return "ira"
                Case "iroquoian languages"
                    Return "iro"
                Case "italian"
                    Return "ita"
                Case "javanese"
                    Return "jav"
                Case "lojban"
                    Return "jbo"
                Case "japanese"
                    Return "jpn"
                Case "judeo-persian"
                    Return "jpr"
                Case "judeo-arabic"
                    Return "jrb"
                Case "kara-kalpak"
                    Return "kaa"
                Case "kabyle"
                    Return "kab"
                Case "kachin", "jingpho"
                    Return "kac"
                Case "kalaallisut", "greenlandic"
                    Return "kal"
                Case "kamba"
                    Return "kam"
                Case "kannada"
                    Return "kan"
                Case "karen languages"
                    Return "kar"
                Case "kashmiri"
                    Return "kas"
                Case "kanuri"
                    Return "kau"
                Case "kawi"
                    Return "kaw"
                Case "kazakh"
                    Return "kaz"
                Case "kabardian"
                    Return "kbd"
                Case "khasi"
                    Return "kha"
                Case "khoisan (other)"
                    Return "khi"
                Case "central khmer"
                    Return "khm"
                Case "khotanese", "sakan"
                    Return "kho"
                Case "kikuyu", "gikuyu"
                    Return "kik"
                Case "kinyarwanda"
                    Return "kin"
                Case "kirghiz", "kyrgyz"
                    Return "kir"
                Case "kimbundu"
                    Return "kmb"
                Case "konkani"
                    Return "kok"
                Case "komi"
                    Return "kom"
                Case "kongo"
                    Return "kon"
                Case "korean"
                    Return "kor"
                Case "kosraean"
                    Return "kos"
                Case "kpelle"
                    Return "kpe"
                Case "karachay-balkar"
                    Return "krc"
                Case "karelian"
                    Return "krl"
                Case "kru languages"
                    Return "kro"
                Case "kurukh"
                    Return "kru"
                Case "kuanyama", "kwanyama"
                    Return "kua"
                Case "kumyk"
                    Return "kum"
                Case "kurdish"
                    Return "kur"
                Case "kutenai"
                    Return "kut"
                Case "ladino"
                    Return "lad"
                Case "lahnda"
                    Return "lah"
                Case "lamba"
                    Return "lam"
                Case "lao"
                    Return "lao"
                Case "latin"
                    Return "lat"
                Case "latvian"
                    Return "lav"
                Case "lezghian"
                    Return "lez"
                Case "limburgan", "limburger", "limburgish"
                    Return "lim"
                Case "lingala"
                    Return "lin"
                Case "lithuanian"
                    Return "lit"
                Case "mongo"
                    Return "lol"
                Case "lozi"
                    Return "loz"
                Case "luxembourgish", "letzeburgesch"
                    Return "ltz"
                Case "luba-lulua"
                    Return "lua"
                Case "luba-katanga"
                    Return "lub"
                Case "ganda"
                    Return "lug"
                Case "luiseno"
                    Return "lui"
                Case "lunda"
                    Return "lun"
                Case "luo (kenya and tanzania)"
                    Return "luo"
                Case "lushai"
                    Return "lus"
                Case "macedonian"
                    Return "mac"
                Case "madurese"
                    Return "mad"
                Case "magahi"
                    Return "mag"
                Case "marshallese"
                    Return "mah"
                Case "maithili"
                    Return "mai"
                Case "makasar"
                    Return "mak"
                Case "malayalam"
                    Return "mal"
                Case "mandingo"
                    Return "man"
                Case "maori"
                    Return "mao"
                Case "austronesian (other)"
                    Return "map"
                Case "marathi"
                    Return "mar"
                Case "masai"
                    Return "mas"
                Case "malay"
                    Return "may"
                Case "moksha"
                    Return "mdf"
                Case "mandar"
                    Return "mdr"
                Case "mende"
                    Return "men"
                Case "irish"
                    Return "mga"
                Case "mi'kmaq", "micmac"
                    Return "mic"
                Case "minangkabau"
                    Return "min"
                Case "uncoded languages"
                    Return "mis"
                Case "mon-khmer (other)"
                    Return "mkh"
                Case "malagasy"
                    Return "mlg"
                Case "maltese"
                    Return "mlt"
                Case "manchu"
                    Return ("mnc")
                Case "manipuri"
                    Return "mni"
                Case "manobo languages"
                    Return "mno"
                Case "mohawk"
                    Return "moh"
                Case "mongolian"
                    Return "mon"
                Case "mossi"
                    Return "mos"
                Case "multiple languages"
                    Return "mul"
                Case "munda languages"
                    Return "mun"
                Case "creek"
                    Return "mus"
                Case "mirandese"
                    Return "mwl"
                Case "marwari"
                    Return "mwr"
                Case "mayan languages"
                    Return "myn"
                Case "erzya"
                    Return "myv"
                Case "nahuatl languages"
                    Return "nah"
                Case "north american indian"
                    Return "nai"
                Case "neapolitan"
                    Return "nap"
                Case "nauru"
                    Return "nau"
                Case "navajo", "navaho"
                    Return "nav"
                Case "ndebele"
                    Return "nbl"
                Case "ndebele"
                    Return "nde"
                Case "ndonga"
                    Return "ndo"
                Case "low german", "low saxon", "german"
                    Return "nds"
                Case "nepali"
                    Return "nep"
                Case "nepal bhasa", "newari"
                    Return "new"
                Case "nias"
                    Return "nia"
                Case "niger-kordofanian (other)"
                    Return "nic"
                Case "niuean"
                    Return "niu"
                Case "norwegian nynorsk", "nynorsk"
                    Return "nno"
                Case "bokmål"
                    Return "nob"
                Case "nogai"
                    Return "nog"
                Case "norse"
                    Return "non"
                Case "norwegian"
                    Return "nor"
                Case "n'ko"
                    Return "nqo"
                Case "pedi", "sepedi", "northern sotho"
                    Return "nso"
                Case "nubian languages"
                    Return "nub"
                Case "classical newari", "old newari", "classical nepal bhasa"
                    Return "nwc"
                Case "chichewa", "chewa", "nyanja"
                    Return "nya"
                Case "nyamwezi"
                    Return "nym"
                Case "nyankole"
                    Return "nyn"
                Case "nyoro"
                    Return "nyo"
                Case "nzima"
                    Return "nzi"
                Case "occitan (post 1500)", "provençal"
                    Return "oci"
                Case "ojibwa"
                    Return "oji"
                Case "oriya"
                    Return "ori"
                Case "oromo"
                    Return "orm"
                Case "osage"
                    Return "osa"
                Case "ossetian", "ossetic"
                    Return "oss"
                Case "turkish"
                    Return "ota"
                Case "otomian languages"
                    Return "oto"
                Case "papuan (other)"
                    Return "paa"
                Case "pangasinan"
                    Return "pag"
                Case "pahlavi"
                    Return "pal"
                Case "pampanga", "kapampangan"
                    Return "pam"
                Case "panjabi", "punjabi"
                    Return "pan"
                Case "papiamento"
                    Return "pap"
                Case "palauan"
                    Return "pau"
                Case "persian"
                    Return "peo"
                Case "persian"
                    Return "per"
                Case "philippine (other)"
                    Return "phi"
                Case "phoenician"
                    Return "phn"
                Case "pali"
                    Return "pli"
                Case "polish"
                    Return "pol"
                Case "pohnpeian"
                    Return "pon"
                Case "portuguese"
                    Return "por"
                Case "prakrit languages"
                    Return "pra"
                Case "provençal"
                    Return "pro"
                Case "pushto", "pashto"
                    Return "pus"
                Case "reserved for local use"
                    Return "qaa-qtz"
                Case "quechua"
                    Return "que"
                Case "rajasthani"
                    Return "raj"
                Case "rapanui"
                    Return "rap"
                Case "rarotongan", "cook islands maori"
                    Return "rar"
                Case "romance (other)"
                    Return "roa"
                Case "romansh"
                    Return "roh"
                Case "romany"
                    Return "rom"
                Case "romanian", "moldavian", "moldovan"
                    Return "rum"
                Case "rundi"
                    Return "run"
                Case "aromanian", "arumanian", "macedo-romanian"
                    Return "rup"
                Case "russian"
                    Return "rus"
                Case "sandawe"
                    Return "sad"
                Case "sango"
                    Return "sag"
                Case "yakut"
                    Return "sah"
                Case "south american indian (other)"
                    Return "sai"
                Case "salishan languages"
                    Return "sal"
                Case "samaritan aramaic"
                    Return "sam"
                Case "sanskrit"
                    Return "san"
                Case "sasak"
                    Return "sas"
                Case "santali"
                    Return "sat"
                Case "sicilian"
                    Return "scn"
                Case "scots"
                    Return "sco"
                Case "selkup"
                    Return "sel"
                Case "semitic (other)"
                    Return "sem"
                Case "irish"
                    Return "sga"
                Case "sign languages"
                    Return "sgn"
                Case "shan"
                    Return "shn"
                Case "sidamo"
                    Return "sid"
                Case "sinhala", "sinhalese"
                    Return "sin"
                Case "siouan languages"
                    Return "sio"
                Case "sino-tibetan (other)"
                    Return "sit"
                Case "slavic (other)"
                    Return "sla"
                Case "slovak"
                    Return "slo"
                Case "slovenian"
                    Return "slv"
                Case "southern sami"
                    Return "sma"
                Case "northern sami"
                    Return "sme"
                Case "sami languages (other)"
                    Return "smi"
                Case "lule sami"
                    Return "smj"
                Case "inari sami"
                    Return "smn"
                Case "samoan"
                    Return "smo"
                Case "skolt sami"
                    Return "sms"
                Case "shona"
                    Return "sna"
                Case "sindhi"
                    Return "snd"
                Case "soninke"
                    Return "snk"
                Case "sogdian"
                    Return "sog"
                Case "somali"
                    Return "som"
                Case "songhai languages"
                    Return "son"
                Case "sotho"
                    Return "sot"
                Case "spanish", "castilian"
                    Return "spa"
                Case "sardinian"
                    Return "srd"
                Case "sranan tongo"
                    Return "srn"
                Case "serbian"
                    Return "srp"
                Case "serer"
                    Return "srr"
                Case "nilo-saharan (other)"
                    Return "ssa"
                Case "swati"
                    Return "ssw"
                Case "sukuma"
                    Return "suk"
                Case "sundanese"
                    Return "sun"
                Case "susu"
                    Return "sus"
                Case "sumerian"
                    Return "sux"
                Case "swahili"
                    Return "swa"
                Case "swedish"
                    Return "swe"
                Case "classical syriac"
                    Return "syc"
                Case "syriac"
                    Return "syr"
                Case "tahitian"
                    Return "tah"
                Case "tai (other)"
                    Return "tai"
                Case "tamil"
                    Return "tam"
                Case "tatar"
                    Return "tat"
                Case "telugu"
                    Return "tel"
                Case "timne"
                    Return "tem"
                Case "tereno"
                    Return "ter"
                Case "tetum"
                    Return "tet"
                Case "tajik"
                    Return "tgk"
                Case "tagalog"
                    Return "tgl"
                Case "thai"
                    Return "tha"
                Case "tibetan"
                    Return "tib"
                Case "tigre"
                    Return "tig"
                Case "tigrinya"
                    Return "tir"
                Case "tiv"
                    Return "tiv"
                Case "tokelau"
                    Return "tkl"
                Case "klingon", "tlhingan-hol"
                    Return "tlh"
                Case "tlingit"
                    Return "tli"
                Case "tamashek"
                    Return "tmh"
                Case "tonga (nyasa)"
                    Return "tog"
                Case "tonga (tonga islands)"
                    Return "ton"
                Case "tok pisin"
                    Return "tpi"
                Case "tsimshian"
                    Return "tsi"
                Case "tswana"
                    Return "tsn"
                Case "tsonga"
                    Return "tso"
                Case "turkmen"
                    Return "tuk"
                Case "tumbuka"
                    Return "tum"
                Case "tupi languages"
                    Return "tup"
                Case "turkish"
                    Return "tur"
                Case "altaic (other)"
                    Return "tut"
                Case "tuvalu"
                    Return "tvl"
                Case "twi"
                    Return "twi"
                Case "tuvinian"
                    Return "tyv"
                Case "udmurt"
                    Return "udm"
                Case "ugaritic"
                    Return "uga"
                Case "uighur", "uyghur"
                    Return "uig"
                Case "ukrainian"
                    Return "ukr"
                Case "umbundu"
                    Return "umb"
                Case "undetermined"
                    Return "und"
                Case "urdu"
                    Return "urd"
                Case "uzbek"
                    Return "uzb"
                Case "vai"
                    Return "vai"
                Case "venda"
                    Return "ven"
                Case "vietnamese"
                    Return "vie"
                Case "volapük"
                    Return "vol"
                Case "votic"
                    Return "vot"
                Case "wakashan languages"
                    Return "wak"
                Case "walamo"
                    Return "wal"
                Case "waray"
                    Return "war"
                Case "washo"
                    Return "was"
                Case "welsh"
                    Return "wel"
                Case "sorbian languages"
                    Return "wen"
                Case "walloon"
                    Return "wln"
                Case "wolof"
                    Return "wol"
                Case "kalmyk", "oirat"
                    Return "xal"
                Case "xhosa"
                    Return "xho"
                Case "yao"
                    Return "yao"
                Case "yapese"
                    Return "yap"
                Case "yiddish"
                    Return "yid"
                Case "yoruba"
                    Return "yor"
                Case "yupik languages"
                    Return "ypk"
                Case "zapotec"
                    Return "zap"
                Case "blissymbols", "blissymbolics", "bliss"
                    Return "zbl"
                Case "zenaga"
                    Return "zen"
                Case "zhuang", "chuang"
                    Return "zha"
                Case "zande languages"
                    Return "znd"
                Case "zulu"
                    Return "zul"
                Case "zuni"
                    Return "zun"
                Case "no linguistic content", "not applicable"
                    Return "zxx"
                Case "zaza", "dimili", "dimli", "kirdki", "kirmanjki", "zazaki"
                    Return "zza"
            End Select
        Catch
        Finally
            Monitor.Exit(monitorobject)
        End Try
        Return "Error"
    End Function

    Public Function getactorthumbpath(Optional ByVal location As String = "")
        Dim actualpath As String = ""
        Try
            If location = Nothing Then
                Return "none"
                Exit Function
            End If
            If location = "" Then
                Return "none"
                Exit Function
            End If

            If location.IndexOf("http") <> -1 Then
                Return location
                Exit Function
            Else
                If location.IndexOf(Preferences.actornetworkpath) <> -1 Then
                    If Preferences.actornetworkpath <> Nothing And Preferences.actorsavepath <> Nothing Then
                        If Preferences.actornetworkpath <> "" And Preferences.actorsavepath <> "" Then
                            Dim filename As String = IO.Path.GetFileName(location)
                            actualpath = IO.Path.Combine(Preferences.actorsavepath, filename)
                            If Not IO.File.Exists(actualpath) Then
                                Dim extension As String = IO.Path.GetExtension(location)
                                Dim purename As String = IO.Path.GetFileName(location)
                                purename = purename.Replace(extension, "")
                                actualpath = Preferences.actorsavepath & "\" & purename.Substring(purename.Length - 2, 2) & "\" & filename
                            End If
                            If Not IO.File.Exists(actualpath) Then
                                actualpath = "none"
                            End If
                        End If
                    Else
                        actualpath = "none"
                    End If
                Else
                    actualpath = "none"
                End If
            End If
            If actualpath = "" Then actualpath = "none"
            Return actualpath
        Catch
            Return "none"
        End Try
    End Function

    Public Function getmedialist(ByVal pathandfilename As String)
        Try
            Dim tempstring As String = pathandfilename
            Dim playlist As New List(Of String)
            If IO.File.Exists(tempstring) Then
                playlist.Add(tempstring)
            End If
            tempstring = tempstring.ToLower
            If tempstring.IndexOf("cd1") <> -1 Then
                tempstring = tempstring.Replace("cd1", "cd2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd2", "cd3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd3", "cd4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd4", "cd5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("cd_1") <> -1 Then
                tempstring = tempstring.Replace("cd_1", "cd_2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd_2", "cd_3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd_3", "cd_4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd_4", "cd_5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("cd 1") <> -1 Then
                tempstring = tempstring.Replace("cd 1", "cd 2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd 2", "cd 3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd 3", "cd 4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd 4", "cd 5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("cd.1") <> -1 Then
                tempstring = tempstring.Replace("cd.1", "cd.2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd.2", "cd.3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd.3", "cd.4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("cd.4", "cd.5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("dvd1") <> -1 Then
                tempstring = tempstring.Replace("dvd1", "dvd2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd2", "dvd3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd3", "dvd4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd4", "dvd5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("dvd_1") <> -1 Then
                tempstring = tempstring.Replace("dvd_1", "dvd_2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd_2", "dvd_3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd_3", "dvd_4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd_4", "dvd_5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("dvd 1") <> -1 Then
                tempstring = tempstring.Replace("dvd 1", "dvd 2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd 2", "dvd 3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd 3", "dvd 4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd 4", "dvd 5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("dvd.1") <> -1 Then
                tempstring = tempstring.Replace("dvd.1", "dvd.2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd.2", "dvd.3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd.3", "dvd.4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("dvd.4", "dvd.5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("part1") <> -1 Then
                tempstring = tempstring.Replace("part1", "part2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part2", "part3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part3", "part4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part4", "part5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("part_1") <> -1 Then
                tempstring = tempstring.Replace("part_1", "part_2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part_2", "part_3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part_3", "part_4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part_4", "part_5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("part 1") <> -1 Then
                tempstring = tempstring.Replace("part 1", "part 2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part 2", "part 3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part 3", "part 4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part 4", "part 5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("part.1") <> -1 Then
                tempstring = tempstring.Replace("part.1", "part.2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part.2", "part.3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part.3", "part.4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("part.4", "part.5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("disk1") <> -1 Then
                tempstring = tempstring.Replace("disk1", "disk2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk2", "disk3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk3", "disk4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk4", "disk5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("disk_1") <> -1 Then
                tempstring = tempstring.Replace("disk_1", "disk_2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk_2", "disk_3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk_3", "disk_4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk_4", "disk_5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("disk 1") <> -1 Then
                tempstring = tempstring.Replace("disk 1", "disk 2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk 2", "disk 3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk 3", "disk 4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk 4", "disk 5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("disk.1") <> -1 Then
                tempstring = tempstring.Replace("disk.1", "disk.2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk.2", "disk.3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk.3", "disk.4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("disk.4", "disk.5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("pt1") <> -1 Then
                tempstring = tempstring.Replace("pt1", "pt2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt2", "pt3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt3", "pt4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt4", "pt5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("pt_1") <> -1 Then
                tempstring = tempstring.Replace("pt_1", "pt_2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt_2", "pt_3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt_3", "pt_4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt_4", "pt_5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("pt 1") <> -1 Then
                tempstring = tempstring.Replace("pt 1", "pt 2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt 2", "pt 3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt 3", "pt 4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt 4", "pt 5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            If tempstring.IndexOf("pt.1") <> -1 Then
                tempstring = tempstring.Replace("pt.1", "pt.2")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt.2", "pt.3")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt.3", "pt.4")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
                tempstring = tempstring.Replace("pt.4", "pt.5")
                If IO.File.Exists(tempstring) Then
                    playlist.Add(tempstring)
                End If
            End If
            Return playlist
        Catch

        End Try
        Return "0"
    End Function

    Dim MediaFileExtensions As List(Of String) = New List(Of String)

    Private Sub InitMediaFileExtensions()
        MediaFileExtensions.Add(".avi")
        MediaFileExtensions.Add(".xvid")
        MediaFileExtensions.Add(".divx")
        MediaFileExtensions.Add(".img")
        MediaFileExtensions.Add(".mpg")
        MediaFileExtensions.Add(".mpeg")
        MediaFileExtensions.Add(".mov")
        MediaFileExtensions.Add(".rm")
        MediaFileExtensions.Add(".3gp")
        MediaFileExtensions.Add(".m4v")
        MediaFileExtensions.Add(".wmv")
        MediaFileExtensions.Add(".asf")
        MediaFileExtensions.Add(".mp4")
        MediaFileExtensions.Add(".mkv")
        MediaFileExtensions.Add(".nrg")
        MediaFileExtensions.Add(".iso")
        MediaFileExtensions.Add(".rmvb")
        MediaFileExtensions.Add(".ogm")
        MediaFileExtensions.Add(".bin")
        MediaFileExtensions.Add(".ts")
        MediaFileExtensions.Add(".vob")
        MediaFileExtensions.Add(".m2ts")
        MediaFileExtensions.Add(".rar")
        MediaFileExtensions.Add(".dvr-ms")
        MediaFileExtensions.Add(".ifo")
        MediaFileExtensions.Add(".ssif")
    End Sub

    Private Function IsMediaExtension(ByVal fileinfo As System.IO.FileInfo) As Boolean
        Dim extension As String = fileinfo.Extension
        Return MediaFileExtensions.Contains(extension.ToLower)
    End Function

    Private Sub loadactorcache()
        actorDB.Clear()
        Dim loadpath As String = Preferences.workingProfile.actorcache
        Dim actorlist As New XmlDocument
        actorlist.Load(loadpath)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In actorlist("actor_cache")
            Select Case thisresult.Name
                Case "actor"
                    Dim newactor As New str_ActorDatabase
                    newactor.actorname = ""
                    newactor.movieid = ""
                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            Case "name"
                                newactor.actorname = detail.InnerText
                            Case "id"
                                newactor.movieid = detail.InnerText
                        End Select
                        If newactor.actorname <> "" And newactor.movieid <> "" Then
                            actorDB.Add(newactor)
                        End If
                    Next
            End Select
        Next
    End Sub

    Private Sub saveactorcache()
        Dim savepath As String = Preferences.workingProfile.actorcache
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("actor_cache")

        Dim childchild As XmlElement
        For Each actor In actorDB
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
        Dim output As New XmlTextWriter(savepath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()
    End Sub

    Private Sub startnewmovies()
        Dim dft As New List(Of String)
        Dim tempint As Integer
        Dim tempstring As String
        Dim errorcounter As Integer = 0
        Dim trailer As String
        Dim newmoviecount As Integer
        Dim dirinfo As String = String.Empty
        newMovieList.Clear()
        Dim newmoviefolders As New List(Of String)
        Dim progress As Integer
        progress = 0
        Dim progresstext As String
        Dim dirpath As String
        Dim log As String = ""  'log added as a precursor to amalgamating similar functions of Form1 and Module1

        Console.WriteLine("*** Starting Folder Scan ***")

        'Dim extension As String
        'Dim filename2 As String


        For Each moviefolder In Preferences.movieFolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                Console.WriteLine("Found " & hg.FullName.ToString)
                Console.WriteLine("Checking for subfolders")
                Try
                    EnumerateDirectories(newmoviefolders, moviefolder)
                    For Each subfolder In newmoviefolders
                        Console.WriteLine("  Subfolder added:- " & subfolder.ToString)
                    Next
                Catch
                End Try
            End If
        Next
        For Each moviefolder In Preferences.offlinefolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                Console.WriteLine("Found" & hg.FullName.ToString)
                'newmoviefolders.Add(moviefolder)
                Console.WriteLine("Checking for subfolders")
                Dim newlist As List(Of String)
                Try
                    newlist = EnumerateDirectory3(moviefolder)
                    For Each subfolder In newlist
                        'If subfolder.IndexOf(".actors") = -1 Then
                        Console.WriteLine("  Subfolder added:- " & subfolder.ToString)
                        Dim temge22 As String = getlastfolder(subfolder & "\whatever") & ".avi"
                        Dim sTempFileName22 As String = IO.Path.Combine(subfolder, temge22)
                        Dim newtemp1 As String = sTempFileName22.Replace(IO.Path.GetExtension(sTempFileName22), ".nfo")
                        If Not IO.File.Exists(newtemp1) Then
                            If Not IO.File.Exists(IO.Path.Combine(subfolder, "tempoffline.ttt")) Then
                                Dim sTempFileName As String = IO.Path.Combine(subfolder, "tempoffline.ttt")
                                Dim fsTemp As New System.IO.FileStream(sTempFileName, IO.FileMode.Create)
                                fsTemp.Close()
                            End If
                            If Not IO.File.Exists(sTempFileName22) Then
                                Dim temge As String = getlastfolder(subfolder & "\whatever") & ".avi"
                                Dim sTempFileName2 As String = IO.Path.Combine(subfolder, temge)
                                Dim fsTemp2 As New System.IO.FileStream(sTempFileName2, IO.FileMode.Create)
                                fsTemp2.Close()
                            End If
                            newmoviefolders.Add(subfolder)
                        End If
                        'End If
                    Next
                Catch
                End Try
            End If
        Next
        Dim mediacounter As Integer = newMovieList.Count
        Console.WriteLine()
        For g = 0 To newmoviefolders.Count - 1
            log = ""
            Try
                For Each ext In Utilities.VideoExtensions
                    Dim moviepattern As String = If((ext = "VIDEO_TS.IFO"), ext, "*" & ext)  'this bit adds the * for the extension search in mov_ListFiles2 if its not the string VIDEO_TS.IFO 
                    dirpath = newmoviefolders(g)
                    Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                    Movies.listMovieFiles(dir_info, moviepattern, log)         'titlename is logged in here
                Next
                tempint = newMovieList.Count - mediacounter
                Console.WriteLine(String.Format("{0} NEW movie{1} found in directory:- {2}", tempint, If(tempint = 1, "", "s"), newmoviefolders(g)))
                Console.WriteLine(log)
                mediacounter = newMovieList.Count
            Catch ex As Exception

            End Try
        Next g
        Console.WriteLine(String.Format("{0} Movie{1} found in all folders", newMovieList.Count, If(newMovieList.Count = 1, "", "s")))

        ' REMOVED THIS SECTION - redundant code, in line with changes made to Form1.mov_StartNew. HueyHQ 18 July 2012
        'Console.WriteLine("Obtaining Title for each movie found, from path and filename")
        'For Each movie In newMovieList
        '    Try
        '        extension = System.IO.Path.GetExtension(movie.nfopathandfilename)
        '        filename2 = System.IO.Path.GetFileName(movie.nfopathandfilename)
        '        Console.WriteLine("")
        '        movie.nfopath = movie.nfopathandfilename.Replace(filename2, "")
        '        movie.title = filename2.Replace(extension, "")
        '        If extension.ToLower <> ".ifo" Then
        '            Try
        '                movie.nfopathandfilename = movie.nfopathandfilename.Replace(extension, ".nfo")
        '            Catch
        '                Console.WriteLine("Unable to get movie title, stage1")
        '                Console.WriteLine("Path is: " & movie.nfopathandfilename)
        '            End Try
        '        End If

        '        'If dvdfolder = True Then
        '        If extension.ToLower = ".ifo" Or Preferences.usefoldernames = True Then
        '            Try
        '                movie.nfopathandfilename = movie.nfopathandfilename.Replace(extension, ".nfo")
        '                movie.title = getlastfolder(movie.nfopathandfilename)
        '            Catch
        '                Console.WriteLine("Unable to get movie title, stage2")
        '                Console.WriteLine("Path is: " & movie.nfopathandfilename)
        '            End Try
        '        End If


        '        If movie.title <> Nothing Then
        '            If movie.title <> "" Then
        '                tempstring = Utilities.CleanFileName(movie.title, False)
        '                If tempstring <> Nothing Then
        '                    If tempstring <> "" Then
        '                        If tempstring <> "error" Then
        '                            movie.title = tempstring
        '                        Else
        '                            Console.WriteLine("Unable to clean title: " & movie.title)
        '                        End If
        '                    Else
        '                        Console.WriteLine("Cleaning title returns blank: " & movie.title)
        '                    End If
        '                Else
        '                    Console.WriteLine("Cleaning title returns nothing: " & movie.title)
        '                End If
        '            End If
        '        End If


        '        Console.WriteLine("Filename is: " & movie.mediapathandfilename)
        '        Console.WriteLine("Title according to settings is: """ & movie.title & """")
        '    Catch

        '    End Try

        'Next

        Console.WriteLine()
        newmoviecount = newMovieList.Count.ToString
        Console.WriteLine("*** Starting Main Scraper Process ***")
        For f = 0 To newMovieList.Count - 1
            Dim stage As Integer = 0
            Dim bodyok As Boolean = True
            'stage 0 = starting scraper
            'Try
            Dim title As String = ""
            Dim nfopath As String = ""
            Dim movieyear As String = String.Empty
            Dim fanartpath As String = ""
            Dim posterpath As String = ""
            Dim year As String = ""
            Dim thumbstring As New XmlDocument
            log = ""
            progress = ((100 / newmoviecount) * (f + 1) * 10)
            progresstext = String.Concat("Scraping Movie " & f + 1 & " of " & newmoviecount)
            If newMovieList(f).title = Nothing Then
                Console.WriteLine("No Filename found for" & newMovieList(f).nfopathandfilename)
            End If
            If newMovieList(f).title <> Nothing Then
                title = Utilities.CleanFileName(newMovieList(f).title, False)
                log = log & "Scraping Title:- " & newMovieList(f).title & vbCrLf
                log = log & "     (cleaned):- " & title & vbCrLf
                progresstext &= " - " & newMovieList(f).title
                nfopath = newMovieList(f).nfopathandfilename
                If Preferences.basicsavemode = True Then
                    nfopath = newMovieList(f).nfopathandfilename.Replace(IO.Path.GetFileName(newMovieList(f).nfopathandfilename), "movie.nfo")
                End If

                posterpath = Preferences.GetPosterPath(nfopath)
                fanartpath = Preferences.GetFanartPath(nfopath)

                Dim extrapossibleID As String = Movies.getExtraIdFromNFO(nfopath, log)
                If extrapossibleID IsNot Nothing Then
                    progresstext &= " - " & extrapossibleID
                Else
                    log &= "  Checking filename for:" & vbCrLf
                    log &= "    IMDB ID - "
                    Dim mat As Match = Nothing
                    Dim T As String = newMovieList(f).nfopathandfilename
                    mat = Regex.Match(T, "(tt\d{7})")
                    If mat.Success = True Then
                        log &= mat.Value
                        progresstext &= " - " & mat.Value
                        extrapossibleID = mat.Value
                    Else
                        log &= "None" & vbCrLf

                        log &= "    Movie Year - "
                        Dim M As Match
                        M = Regex.Match(newMovieList(f).nfopathandfilename, "[\(\[]([\d]{4})[\)\]]")
                        If M.Success = True Then
                            movieyear = M.Groups(1).Value
                            log &= movieyear
                        Else
                            log &= "None"
                        End If
                    End If
                    log &= vbCrLf
                End If
                progresstext &= String.Format(" - using '{0}{1}'", title, If(String.IsNullOrEmpty(movieyear), "", " " & movieyear))
                Console.WriteLine(log)

                Dim newmovie As New fullmoviedetails
                Dim body As String
                Dim actorlist As String
                Dim certificates As New List(Of String)
                stage = 1
                'stage 1 = get movie body

                imdbCounter += 1

                Dim scraperfunction As New Classimdb
                'body = newscraper.getimdbbody(title, movieyear, extrapossibleID, Preferences.imdbmirror, imdbcounter)
                body = scraperfunction.getimdbbody(title, movieyear, extrapossibleID, Preferences.imdbmirror, imdbCounter)
                Dim thisresult As XmlNode = Nothing
                If body = "MIC" Then
                    Console.WriteLine("Unable to scrape body with refs """ & title & """, """ & movieyear & """, """ & extrapossibleID & """, """ & Preferences.imdbmirror & """")
                    If imdbCounter < 50 Then
                        Console.WriteLine("Searching using Google")
                    Else
                        Console.WriteLine("Google session limit reached, Searching using IMDB")
                    End If
                    Console.WriteLine("To add the movie manually, go to the movie edit page and select ""Change Movie"" to manually select the correct movie")
                    newmovie.fullmoviebody.genre = "problem"
                    If newmovie.fullmoviebody.title = Nothing Then
                        newmovie.fullmoviebody.title = "Unknown Title"
                    End If
                    If newmovie.fullmoviebody.title = "" Then
                        newmovie.fullmoviebody.title = "Unknown Title"
                    End If
                    If newmovie.fullmoviebody.year = Nothing Then
                        newmovie.fullmoviebody.year = "0000"
                    End If
                    If newmovie.fullmoviebody.rating = Nothing Then
                        newmovie.fullmoviebody.rating = "0"
                    End If
                    If newmovie.fullmoviebody.top250 = Nothing Then
                        newmovie.fullmoviebody.top250 = "0"
                    End If
                    If newmovie.fullmoviebody.playcount = Nothing Then
                        newmovie.fullmoviebody.playcount = "0"
                    End If
                    If newmovie.fullmoviebody.title = "Unknown Title" Then
                        newmovie.fullmoviebody.plot = "This Movie has could not be identified by Media Companion, to add the movie manually, go to the movie edit page and select ""Change Movie"" to manually select the correct movie"
                        If title <> Nothing Then
                            If title = "" Then
                                title = "Unknown Title"
                            End If
                        Else
                            title = "Unknown Title"
                        End If
                        newmovie.fullmoviebody.title = title
                    End If
                    If newmovie.fullmoviebody.title = "Unknown Title" Then
                        newmovie.fullmoviebody.genre = "Problem"
                    End If

                    Dim myDate2 As Date = System.DateTime.Now
                    Try
                        newmovie.fileinfo.createdate = Format(myDate2, "yyyyMMddHHmmss").ToString
                    Catch ex2 As Exception
                    End Try

                    savemovienfo(nfopath, newmovie, True)


                    Dim movietoadd As New str_ComboList
                    movietoadd.fullpathandfilename = nfopath
                    movietoadd.filename = IO.Path.GetFileName(newMovieList(f).nfopathandfilename)
                    movietoadd.foldername = getlastfolder(newMovieList(f).nfopathandfilename)
                    movietoadd.title = newmovie.fullmoviebody.title
                    If newmovie.fullmoviebody.title <> Nothing Then
                        If newmovie.fullmoviebody.year <> Nothing Then
                            movietoadd.titleandyear = newmovie.fullmoviebody.title & " (" & newmovie.fullmoviebody.year & ")"
                        Else
                            movietoadd.titleandyear = newmovie.fullmoviebody.title & " (0000)"
                        End If
                    Else
                        movietoadd.titleandyear = "Unknown (0000)"
                    End If

                    movietoadd.year = newmovie.fullmoviebody.year

                    Dim filecreation As New IO.FileInfo(newMovieList(f).nfopathandfilename)
                    Dim myDate As Date = filecreation.LastWriteTime
                    Try
                        movietoadd.filedate = Format(myDate, "yyyyMMddHHmmss").ToString
                    Catch ex As Exception
                        'MsgBox(ex.ToString)
                    End Try
                    myDate2 = System.DateTime.Now
                    Try
                        movietoadd.createdate = Format(myDate2, "yyyyMMddHHmmss").ToString
                    Catch ex2 As Exception
                    End Try
                    movietoadd.sortorder = newmovie.fullmoviebody.title
                        movietoadd.originaltitle = newmovie.fullmoviebody.title
                    movietoadd.outline = newmovie.fullmoviebody.outline
                        movietoadd.plot = newmovie.fullmoviebody.plot
                    movietoadd.id = newmovie.fullmoviebody.imdbid
                    movietoadd.rating = newmovie.fullmoviebody.rating
                    movietoadd.top250 = newmovie.fullmoviebody.top250
                    movietoadd.genre = newmovie.fullmoviebody.genre
                    movietoadd.playcount = newmovie.fullmoviebody.playcount
                    movietoadd.missingdata1 = 3
                    movietoadd.runtime = "0"
                    fullMovieList.Add(movietoadd)
                Else
                    Try
                        Console.WriteLine("  Movie Body Scraped OK")
                        thumbstring.LoadXml(body)
                        For Each thisresult In thumbstring("movie")
                            Select Case thisresult.Name
                                Case "title"
                                    If Preferences.keepfoldername = False Then
                                        newmovie.fullmoviebody.title = thisresult.InnerText
                                    Else
                                        If Preferences.usefoldernames = False Then
                                            tempstring = IO.Path.GetFileName(newMovieList(f).nfopathandfilename)
                                            newmovie.fullmoviebody.title = Utilities.CleanFileName(tempstring, False)
                                        Else
                                            newmovie.fullmoviebody.title = Utilities.CleanFileName(getlastfolder(newMovieList(f).nfopathandfilename), False)
                                        End If
                                    End If
                                Case "originaltitle"
                                    newmovie.fullmoviebody.originaltitle = thisresult.InnerText
                                Case "alternativetitle"
                                    newmovie.alternativetitles.Add(thisresult.InnerText)
                                Case "country"
                                    newmovie.fullmoviebody.country = thisresult.InnerText
                                Case "credits"
                                    newmovie.fullmoviebody.credits = thisresult.InnerText
                                Case "director"
                                    newmovie.fullmoviebody.director = thisresult.InnerText
                                Case "stars"
                                    newmovie.fullmoviebody.stars = thisresult.InnerText
                                Case "genre"
                                    Dim strarr() As String
                                    strarr = thisresult.InnerText.Split("/")
                                    For count = 0 To strarr.Length - 1
                                        strarr(count) = strarr(count).Replace(" ", "")
                                    Next
                                    If strarr.Length <= Preferences.maxmoviegenre Then
                                        newmovie.fullmoviebody.genre = thisresult.InnerText
                                    Else
                                        For g = 0 To Preferences.maxmoviegenre - 1
                                            If g = 0 Then
                                                newmovie.fullmoviebody.genre = strarr(g)
                                            Else
                                                newmovie.fullmoviebody.genre += " / " & strarr(g)
                                            End If
                                        Next
                                    End If
                                Case "mpaa"
                                    newmovie.fullmoviebody.mpaa = thisresult.InnerText
                                Case "outline"
                                    newmovie.fullmoviebody.outline = thisresult.InnerText
                                Case "plot"
                                    newmovie.fullmoviebody.plot = thisresult.InnerText
                                Case "premiered"
                                    newmovie.fullmoviebody.premiered = thisresult.InnerText
                                Case "rating"
                                    newmovie.fullmoviebody.rating = thisresult.InnerText
                                Case "runtime"
                                    newmovie.fullmoviebody.runtime = thisresult.InnerText
                                    If newmovie.fullmoviebody.runtime.IndexOf(":") <> -1 Then
                                            Try
                                        newmovie.fullmoviebody.runtime = newmovie.fullmoviebody.runtime.Substring(newmovie.fullmoviebody.runtime.IndexOf(":") + 1, newmovie.fullmoviebody.runtime.Length - newmovie.fullmoviebody.runtime.IndexOf(":") - 1)
                                            Catch ex As Exception
                                            End Try
                                    End If
                                Case "studio"
                                    newmovie.fullmoviebody.studio = thisresult.InnerText
                                Case "tagline"
                                    newmovie.fullmoviebody.tagline = thisresult.InnerText
                                Case "top250"
                                    newmovie.fullmoviebody.top250 = thisresult.InnerText
                                Case "votes"
                                    newmovie.fullmoviebody.votes = thisresult.InnerText
                                Case "year"
                                    newmovie.fullmoviebody.year = thisresult.InnerText
                                Case "id"
                                    newmovie.fullmoviebody.imdbid = thisresult.InnerText
                                Case "cert"
                                    certificates.Add(thisresult.InnerText)
                            End Select
                        Next
                            ' If plot is empty, use outline
                            If newmovie.fullmoviebody.plot = "" Then newmovie.fullmoviebody.plot = newmovie.fullmoviebody.outline

                    Catch ex As Exception
                        Console.WriteLine("Error with " & newMovieList(f).nfopathandfilename)
                        Console.WriteLine("An error was encountered at stage 1, Downloading Movie Body")
                        Console.WriteLine(ex.Message.ToString)
                        errorcounter += 1
                        If Preferences.usefoldernames = False Then
                            tempstring = IO.Path.GetFileName(newMovieList(f).nfopathandfilename)
                            newmovie.fullmoviebody.title = Utilities.CleanFileName(tempstring, False)

                        Else
                            newmovie.fullmoviebody.title = Utilities.CleanFileName(getlastfolder(newMovieList(f).nfopathandfilename), False)

                        End If
                    End Try
                    If newmovie.fullmoviebody.playcount = Nothing Then newmovie.fullmoviebody.playcount = "0"
                    If newmovie.fullmoviebody.top250 = Nothing Then newmovie.fullmoviebody.top250 = "0"

                    Dim done As Boolean = False
                    For g = 0 To UBound(Preferences.certificatepriority)
                        For Each cert In certificates
                            If cert.IndexOf(Preferences.certificatepriority(g)) <> -1 Then
                                newmovie.fullmoviebody.mpaa = cert.Substring(cert.IndexOf("|") + 1, cert.Length - cert.IndexOf("|") - 1)
                                done = True
                                Exit For
                            End If
                        Next
                        If done = True Then Exit For
                    Next
                    If Preferences.keepfoldername = True Then
                        If Preferences.usefoldernames = False Then
                            tempstring = IO.Path.GetFileName(newMovieList(f).nfopathandfilename)
                            newmovie.fullmoviebody.title = Utilities.CleanFileName(tempstring)
                        Else
                            newmovie.fullmoviebody.title = Utilities.CleanFileName(getlastfolder(newMovieList(f).nfopathandfilename))

                        End If
                    End If

                    '******************************** MOVIE FILE RENAME SECTION *************************************
                    If Preferences.MovieRenameEnable = True AndAlso Preferences.usefoldernames = False AndAlso newMovieList(f).nfopathandfilename.ToLower.Contains("video_ts") = False AndAlso Preferences.basicsavemode = False Then
                        Dim movieFileDetails As str_NewMovie = newMovieList(f)
                        log = Movies.fileRename(newmovie.fullmoviebody, movieFileDetails)
                        newMovieList.RemoveAt(f)                        'remove old record
                        newMovieList.Insert(f, movieFileDetails)        'reinsert
                        nfopath = movieFileDetails.nfopathandfilename   'adjust nfopath variables
                        posterpath = Preferences.GetPosterPath(nfopath)
                        fanartpath = Preferences.GetFanartPath(nfopath)
                    End If
                    Console.Write("  " & log)
                    '******************************** END MOVIE FILE RENAME SECTION *************************************

                    stage = 2
                    'stage 2 = get movie actors
                    actorlist = scraperfunction.getimdbactors(Preferences.imdbmirror, newmovie.fullmoviebody.imdbid)
                    Try
                        thumbstring.LoadXml(actorlist)
                        thisresult = Nothing
                        Dim actorcount As Integer = 0
                        For Each thisresult In thumbstring("actorlist")
                            Select Case thisresult.Name
                                Case "actor"
                                    If actorcount > Preferences.maxactors Then
                                        Exit For
                                    End If
                                    actorcount += 1
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
                                                    If detail.InnerText <> "" And Preferences.actorseasy = True Then
                                                        Dim workingpath As String = newMovieList(f).nfopathandfilename.Replace(IO.Path.GetFileName(newMovieList(f).nfopathandfilename), "")
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
                                                            filename = IO.Path.Combine(workingpath, filename)
                                                            If Not IO.File.Exists(filename) Then
                                                                Try
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
                                                                Catch
                                                                End Try
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
                                    newmovie.listactors.Add(newactor)

                            End Select
                        Next

                        Console.WriteLine("  Actors scraped OK")
                        While newmovie.listactors.Count > Preferences.maxactors
                            newmovie.listactors.RemoveAt(newmovie.listactors.Count - 1)
                        End While
                        For Each actor In newmovie.listactors
                            Dim actornew As New str_ActorDatabase
                            actornew.actorname = actor.actorname
                            actornew.movieid = newmovie.fullmoviebody.imdbid
                            actorDB.Add(actornew)
                        Next
                    Catch ex As Exception
                        Console.WriteLine("Error with " & newMovieList(f).nfopathandfilename)
                        Console.WriteLine("An error was encountered at stage 2, Downloading Actors")
                        Console.WriteLine(ex.Message.ToString)
                        errorcounter += 1
                        newmovie.listactors.Clear()
                    End Try


                    stage = 3
                    'stage 3 = get movie trailer
                    Try
                        If Preferences.gettrailer = True Then


                            trailer = ""

                            If Preferences.moviePreferredTrailerResolution.ToUpper() <> "SD" Then
                                trailer = MC_Scraper_Get_HD_Trailer_URL(Preferences.moviePreferredTrailerResolution, newmovie.fullmoviebody.title)

                                If trailer = "" Then
                                    Console.WriteLine("No HD Trailer URL found")
                                Else
                                    Console.WriteLine("HD Trailer URL found")
                                End If
                            End If

                            If trailer = "" Then
                                trailer = gettrailerurl(newmovie.fullmoviebody.imdbid, Preferences.imdbmirror)

                                If trailer = "Error" Then
                                    Console.WriteLine("No SD Trailer URL found")
                                Else
                                    Console.WriteLine("SD Trailer URL found")
                                End If
                            End If

                            If UrlIsValid(trailer) Then
                                newmovie.fullmoviebody.trailer = trailer


                                If Preferences.DownloadTrailerDuringScrape Then
                                    Console.WriteLine("  Downloading Trailer...")

                                    newmovie.fileinfo.trailerpath = GetTrailerPath(nfopath)

                                    DownloadTrailer(newmovie.fileinfo.trailerpath, newmovie.fullmoviebody.trailer)
                                End If

                            Else
                                newmovie.fullmoviebody.trailer = ""
                                Console.WriteLine("Failed to find Trailer URL")
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    'stage 4 = get movie thumblist(for nfo)

                    If Preferences.nfoposterscraper <> 0 Then
                        Dim thumbs As String = ""
                        If Preferences.nfoposterscraper = 1 Or Preferences.nfoposterscraper = 3 Or Preferences.nfoposterscraper = 5 Or Preferences.nfoposterscraper = 7 Or Preferences.nfoposterscraper = 9 Or Preferences.nfoposterscraper = 11 Or Preferences.nfoposterscraper = 13 Or Preferences.nfoposterscraper = 15 Then
                            Dim newobject3 As New IMPA.getimpaposters
                            Dim teststring As New XmlDocument
                            Dim testthumbs As String
                            Try
                                testthumbs = newobject3.getimpathumbs(newmovie.fullmoviebody.title, newmovie.fullmoviebody.year)
                                Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                                teststring.LoadXml(testthumbs2)
                                thumbs = thumbs & testthumbs.ToString
                            Catch ex As Exception
                                Thread.Sleep(1)
                            End Try
                        End If

                        If Preferences.nfoposterscraper = 2 Or Preferences.nfoposterscraper = 3 Or Preferences.nfoposterscraper = 6 Or Preferences.nfoposterscraper = 7 Or Preferences.nfoposterscraper = 10 Or Preferences.nfoposterscraper = 11 Or Preferences.nfoposterscraper = 14 Or Preferences.nfoposterscraper = 15 Then
                            Dim newobject2 As New tmdb_posters.Class1
                            Dim teststring As New XmlDocument
                            Dim testthumbs As String
                            Try
                                testthumbs = newobject2.gettmdbposters_newapi(newmovie.fullmoviebody.imdbid)
                                Dim bannerslist As New XmlDocument
                                bannerslist.LoadXml(testthumbs)
                                Dim templist As String = ""
                                For Each item In bannerslist("tmdb_posterlist")
                                    Select Case item.name
                                        Case "poster"
                                            For Each img In item
                                                If img.childnodes(0).innertext = "original" Then
                                                    templist = templist & "<thumbs>" & img.childnodes(1).innertext & "</thumbs>"
                                                End If
                                            Next
                                    End Select
                                Next
                                thumbs = thumbs & templist.ToString
                            Catch ex As Exception
                                Thread.Sleep(1)
                            End Try
                        End If

                        If Preferences.nfoposterscraper = 4 Or Preferences.nfoposterscraper = 5 Or Preferences.nfoposterscraper = 6 Or Preferences.nfoposterscraper = 7 Or Preferences.nfoposterscraper = 12 Or Preferences.nfoposterscraper = 13 Or Preferences.nfoposterscraper = 14 Or Preferences.nfoposterscraper = 15 Then
                            Dim newobject As New class_mpdb_thumbs.Class1
                            Dim teststring As New XmlDocument
                            Dim testthumbs As String
                            Try
                                testthumbs = newobject.get_mpdb_thumbs(newmovie.fullmoviebody.imdbid)
                                Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                                teststring.LoadXml(testthumbs2)
                                thumbs = thumbs & testthumbs.ToString
                            Catch ex As Exception
                                Thread.Sleep(1)
                            End Try
                        End If

                        If Preferences.nfoposterscraper = 8 Or Preferences.nfoposterscraper = 9 Or Preferences.nfoposterscraper = 10 Or Preferences.nfoposterscraper = 11 Or Preferences.nfoposterscraper = 12 Or Preferences.nfoposterscraper = 13 Or Preferences.nfoposterscraper = 14 Or Preferences.nfoposterscraper = 15 Then
                            Dim thumbscraper As New imdb_thumbs.Class1
                            Dim teststring As New XmlDocument
                            Try
                                Dim testthumbs As String
                                testthumbs = thumbscraper.getimdbthumbs(newmovie.fullmoviebody.title, newmovie.fullmoviebody.year, newmovie.fullmoviebody.imdbid)
                                Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                                teststring.LoadXml(testthumbs2)
                                thumbs = thumbs & testthumbs.ToString
                            Catch ex As Exception
                                Thread.Sleep(1)
                            End Try
                        End If




                        thumbs = "<thumblist>" & thumbs & "</thumblist>"

                        Try
                            thumbstring.LoadXml(thumbs)



                            For Each thisresult In thumbstring("thumblist")
                                Select Case thisresult.Name
                                    Case "thumb"
                                        newmovie.listthumbs.Add(thisresult.InnerText)
                                End Select
                            Next
                            Console.WriteLine("  Poster URLs Scraped OK")
                        Catch ex As Exception
                            Console.WriteLine("Error with " & newMovieList(f).nfopathandfilename)
                            Console.WriteLine("An error was encountered at stage 4, Downloading poster list for nfo file")
                            Console.WriteLine(ex.Message.ToString)
                            errorcounter += 1
                            newmovie.listthumbs.Clear()
                        End Try
                    End If
                    stage = 5
                    'stage 5 = get hd tags
                    Try
                        Dim tempsb As String = newMovieList(f).mediapathandfilename.Replace(IO.Path.GetFileName(newMovieList(f).mediapathandfilename), "")
                        tempsb = IO.Path.Combine(tempsb, "tempoffline.ttt")
                        If Not IO.File.Exists(tempsb) Then
                                'newmovie.filedetails = get_hdtags(newMovieList(f).mediapathandfilename)
                                newmovie.filedetails = Preferences.Get_HdTags(newMovieList(f).mediapathandfilename)
            
                            If newmovie.filedetails.filedetails_video.DurationInSeconds.Value <> Nothing And ((Preferences.movieRuntimeDisplay = "file") or (Preferences.movieRuntimeFallbackToFile and newmovie.fullmoviebody.runtime = Nothing)) Then
                                newmovie.fullmoviebody.runtime = Utilities.cleanruntime(newmovie.filedetails.filedetails_video.DurationInSeconds.Value) & " min"
                                Console.WriteLine("  HD Tags Added OK")
                            End If
                        End If
                    Catch ex As Exception
                        Console.WriteLine("Error getting HD Tags:- " & ex.Message.ToString)
                    End Try


                    If newmovie.fullmoviebody.title = Nothing Then
                        newmovie.fullmoviebody.title = "Unknown Title"
                    End If
                    If newmovie.fullmoviebody.title = "" Then
                        newmovie.fullmoviebody.title = "Unknown Title"
                    End If
                    If newmovie.fullmoviebody.year = Nothing Then
                        newmovie.fullmoviebody.year = "0000"
                    End If
                    If newmovie.fullmoviebody.rating = Nothing Then
                        newmovie.fullmoviebody.rating = "0"
                    End If
                    If newmovie.fullmoviebody.top250 = Nothing Then
                        newmovie.fullmoviebody.top250 = "0"
                    End If
                    If newmovie.fullmoviebody.playcount = Nothing Then
                        newmovie.fullmoviebody.playcount = "0"
                    End If
                    If newmovie.fullmoviebody.title = "Unknown Title" Then
                        newmovie.fullmoviebody.plot = "This Movie has could not be identified by Media Companion, to add the movie manually, go to the movie edit page and select ""Change Movie"" to manually select the correct movie"
                        If title <> Nothing Then
                            If title = "" Then
                                title = "Unknown Title"
                            End If
                        Else
                            title = "Unknown Title"
                        End If
                        newmovie.fullmoviebody.title = title
                    End If
                    If newmovie.fullmoviebody.title = "Unknown Title" Then
                        newmovie.fullmoviebody.genre = "Problem"
                    End If
                    Dim myDate2 As Date = System.DateTime.Now
                    Try
                        newmovie.fileinfo.createdate = Format(myDate2, "yyyyMMddHHmmss").ToString
                    Catch ex2 As Exception
                    End Try
                    savemovienfo(nfopath, newmovie, True)



                    Dim movietoadd As New str_ComboList
                    movietoadd.fullpathandfilename = nfopath
                    movietoadd.filename = IO.Path.GetFileName(newMovieList(f).nfopathandfilename)
                    movietoadd.foldername = getlastfolder(newMovieList(f).nfopathandfilename)
                    movietoadd.title = newmovie.fullmoviebody.title
                        movietoadd.originaltitle = newmovie.fullmoviebody.originaltitle
                    movietoadd.sortorder = newmovie.fullmoviebody.sortorder
                    movietoadd.runtime = newmovie.fullmoviebody.runtime
                    If newmovie.fullmoviebody.title <> Nothing Then
                        If newmovie.fullmoviebody.year <> Nothing Then
                                If newmovie.fullmoviebody.title.ToLower.IndexOf("the") = 0 Then
                                    movietoadd.titleandyear = newmovie.fullmoviebody.title.Substring(4, newmovie.fullmoviebody.title.Length - 4) & ", The (" & newmovie.fullmoviebody.year & ")"
                                Else
                            movietoadd.titleandyear = newmovie.fullmoviebody.title & " (" & newmovie.fullmoviebody.year & ")"
                                End If
                        Else
                            movietoadd.titleandyear = newmovie.fullmoviebody.title & " (0000)"
                        End If
                    Else
                        movietoadd.titleandyear = "Unknown (0000)"
                    End If
                    movietoadd.outline = newmovie.fullmoviebody.outline
                        movietoadd.plot = newmovie.fullmoviebody.plot
                    movietoadd.year = newmovie.fullmoviebody.year




                    Dim filecreation As New IO.FileInfo(newMovieList(f).nfopathandfilename)
                    Dim myDate As Date = filecreation.LastWriteTime
                    Try
                        movietoadd.filedate = Format(myDate, "yyyyMMddHHmmss").ToString
                    Catch ex As Exception
                        'MsgBox(ex.ToString)
                    End Try
                    myDate2 = System.DateTime.Now
                    Try
                        movietoadd.createdate = Format(myDate2, "yyyyMMddHHmmss").ToString
                    Catch ex2 As Exception
                    End Try



                    movietoadd.id = newmovie.fullmoviebody.imdbid
                    movietoadd.rating = newmovie.fullmoviebody.rating
                    movietoadd.top250 = newmovie.fullmoviebody.top250
                    movietoadd.genre = newmovie.fullmoviebody.genre
                    movietoadd.playcount = newmovie.fullmoviebody.playcount




                    stage = 6
                    'stage 6 = download movieposter
                    Dim moviethumburl As String = ""
                    If Preferences.scrapemovieposters = True And Preferences.overwritethumbs = True Or IO.File.Exists(Preferences.GetPosterPath(newMovieList(f).nfopathandfilename)) = False Then
                        Try
                            Select Case Preferences.moviethumbpriority(0)
                                Case "Internet Movie Poster Awards"
                                    moviethumburl = impathumb(newmovie.fullmoviebody.title, newmovie.fullmoviebody.year)
                                Case "IMDB"
                                    moviethumburl = imdbthumb(newmovie.fullmoviebody.imdbid)
                                Case "Movie Poster DB"
                                    moviethumburl = mpdbthumb(newmovie.fullmoviebody.imdbid)
                                Case "themoviedb.org"
                                    moviethumburl = tmdbthumb(newmovie.fullmoviebody.imdbid)
                            End Select
                        Catch
                            moviethumburl = "na"
                        End Try
                        Try
                           ' If moviethumburl = "na" Or moviethumburl = "Error" Then
                                If moviethumburl.Length < 10 Then
                                Select Case Preferences.moviethumbpriority(1)
                                    Case "Internet Movie Poster Awards"
                                        moviethumburl = impathumb(newmovie.fullmoviebody.title, newmovie.fullmoviebody.year)
                                    Case "IMDB"
                                        moviethumburl = imdbthumb(newmovie.fullmoviebody.imdbid)
                                    Case "Movie Poster DB"
                                        moviethumburl = mpdbthumb(newmovie.fullmoviebody.imdbid)
                                    Case "themoviedb.org"
                                        moviethumburl = tmdbthumb(newmovie.fullmoviebody.imdbid)
                                End Select
                            End If
                        Catch
                            moviethumburl = "na"
                        End Try
                        Try
                         '   If moviethumburl = "na" Or moviethumburl = "Error" Then

                                If moviethumburl.Length < 10 Then
                                Select Case Preferences.moviethumbpriority(2)
                                    Case "Internet Movie Poster Awards"
                                        moviethumburl = impathumb(newmovie.fullmoviebody.title, newmovie.fullmoviebody.year)
                                    Case "IMDB"
                                        moviethumburl = imdbthumb(newmovie.fullmoviebody.imdbid)
                                    Case "Movie Poster DB"
                                        moviethumburl = mpdbthumb(newmovie.fullmoviebody.imdbid)
                                    Case "themoviedb.org"
                                        moviethumburl = tmdbthumb(newmovie.fullmoviebody.imdbid)
                                End Select
                            End If
                        Catch
                            moviethumburl = "na"
                        End Try
                        Try
                          '  If moviethumburl = "na" Or moviethumburl = "Error" Then
                                If moviethumburl.Length < 10 Then
                                Select Case Preferences.moviethumbpriority(3)
                                    Case "Internet Movie Poster Awards"
                                        moviethumburl = impathumb(newmovie.fullmoviebody.title, newmovie.fullmoviebody.year)
                                    Case "IMDB"
                                        moviethumburl = imdbthumb(newmovie.fullmoviebody.imdbid)
                                    Case "Movie Poster DB"
                                        moviethumburl = mpdbthumb(newmovie.fullmoviebody.imdbid)
                                    Case "themoviedb.org"
                                        moviethumburl = tmdbthumb(newmovie.fullmoviebody.imdbid)
                                End Select
                            End If
                        Catch
                            moviethumburl = "na"
                        End Try
                        Try

                          '  If moviethumburl <> "" And moviethumburl <> "na" And moviethumburl <> "Error" Then
                                If moviethumburl.Length >= 10 Then
                                Dim newmoviethumbpath As String = Preferences.GetPosterPath(newMovieList(f).nfopathandfilename)
                                Try
                                    Dim buffer(4000000) As Byte
                                    Dim size As Integer = 0
                                    Dim bytesRead As Integer = 0
                                    Dim thumburl As String = moviethumburl
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
                                    'Console.writeline("Downloading Movie Thumbnail at URL :- " & newmoviethumbpath)
                                    'Console.writeline("Unable to Download Thumb")
                                    'Console.writeline("Saving Thumbnail To Path :- " & newmoviethumbpath)
                                    Dim fstrm As New FileStream(posterpath, FileMode.OpenOrCreate, FileAccess.Write)
                                    fstrm.Write(buffer, 0, bytesRead)
                                    contents.Close()
                                    fstrm.Close()
                                    Console.WriteLine("  Poster scraped and saved OK")

                                    Dim temppath As String = newmoviethumbpath.Replace(System.IO.Path.GetFileName(newmoviethumbpath), "folder.jpg")
                                    If Preferences.createfolderjpg = True Then
                                        If Preferences.overwritethumbs = True Or System.IO.File.Exists(temppath) = False Then
                                            Console.WriteLine("  Saving folder.jpg To Path :- " & temppath)
                                            Dim fstrm2 As New FileStream(temppath, FileMode.OpenOrCreate, FileAccess.Write)
                                            fstrm2.Write(buffer, 0, bytesRead)
                                            contents.Close()
                                            fstrm2.Close()
                                            Console.WriteLine("  Poster also saved as ""folder.jpg"" OK")
                                        Else
                                            Console.WriteLine("folder.jpg Not Saved to :- " & temppath & ", file already exists")
                                        End If
                                    End If
                                Catch ex As Exception
                                    Console.WriteLine("Problem Saving Thumbnail")
                                    Console.WriteLine("Error Returned :- " & ex.ToString)
                                End Try
                            End If
                        Catch
                        End Try
                    End If




                    stage = 7
                    'stage 7 = download fanart
                    If Preferences.overwritethumbs = True Or Preferences.overwritethumbs = False And IO.File.Exists(Preferences.GetFanartPath(newMovieList(f).nfopathandfilename)) = False Then
                        If Preferences.savefanart = False Then
                            'Console.writeline("Fanart Not Downloaded - Disabled in preferences, use browser to find and add Fanart")
                        Else
                            Try

                                Dim moviefanartexists As Boolean
                                Dim fanarturlpath As String = Preferences.GetFanartPath(newMovieList(f).nfopathandfilename)

                                moviethumburl = ""
                                moviefanartexists = System.IO.File.Exists(fanarturlpath)
                                If moviefanartexists = False Or Preferences.overwritethumbs = True Then

                                    Dim temp As String = newmovie.fullmoviebody.imdbid
                                    Dim fanarturl As String = "http://api.themoviedb.org/2.1/Movie.imdbLookup/en/xml/3f026194412846e530a208cf8a39e9cb/" & temp
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

                                        Do While Not sLine Is Nothing
                                            fanartlinecount += 1
                                            sLine = objReader.ReadLine
                                            apple2(fanartlinecount) = sLine
                                        Loop

                                        fanartlinecount -= 1
                                        Dim fanartfound As Boolean = False
                                        For g = 1 To fanartlinecount
                                                ' vou mudar para ser compativel com api 2.1'                                           If apple2(g).IndexOf("<backdrop size=""original"">") <> -1 Then
                                                If apple2(g).IndexOf("<image type=""backdrop""") <> -1 Then
                                                    If apple2(g).IndexOf("size=""original""") <> -1 Then
                                                        Dim StartofURL As Integer = apple2(g).IndexOf("url=""") + 5
                                                        Dim EndofURL As Integer = apple2(g).IndexOf("size=""original""") - 2
                                                        apple2(g) = apple2(g).Substring(StartofURL, (EndofURL - StartofURL))
                                                apple2(g) = apple2(g).Trim
                                                If apple2(g).ToLower.IndexOf("http") <> -1 And apple2(g).ToLower.IndexOf(".jpg") <> -1 Or apple2(g).IndexOf(".jpeg") <> -1 Or apple2(g).IndexOf(".png") <> -1 Then
                                                    moviethumburl = apple2(g)
                                                    fanartfound = True
                                                            Exit For
                                                End If
                                                    End If
                                                    '                                                        Exit For
                                            End If
                                        Next
                                        If fanartfound = False Then moviethumburl = ""
                                    Catch
                                    End Try

                                    If moviethumburl <> "" Then
                                        'Console.writeline("Fanart URL is " & fanarturl)
                                        Console.WriteLine("  Saving Fanart As :- " & fanarturlpath)

                                        'need to resize thumbs

                                        Try
                                            Dim buffer(8000000) As Byte
                                            Dim size As Integer = 0
                                            Dim bytesRead As Integer = 0

                                            Dim thumburl As String = moviethumburl
                                            Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                            Dim res As HttpWebResponse = req.GetResponse()
                                            Dim contents As Stream = res.GetResponseStream()
                                            Dim bytesToRead As Integer = CInt(buffer.Length)
                                            Dim bmp As New Bitmap(contents)


                                            While bytesToRead > 0
                                                size = contents.Read(buffer, bytesRead, bytesToRead)
                                                If size = 0 Then Exit While
                                                bytesToRead -= size
                                                bytesRead += size
                                            End While



                                            If Preferences.resizefanart = 1 Then
                                                bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                                                Console.WriteLine("  Fanart not resized")
                                            ElseIf Preferences.resizefanart = 2 Then
                                                If bmp.Width > 1280 Or bmp.Height > 720 Then
                                                    Dim bm_source As New Bitmap(bmp)
                                                    Dim bm_dest As New Bitmap(1280, 720)
                                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                                    gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                                                    bm_dest.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                    Console.WriteLine("  Farart Resized to 1280x720")
                                                Else
                                                    Console.WriteLine("  Fanart not resized, already =< required size")
                                                    bmp.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                End If
                                            ElseIf Preferences.resizefanart = 3 Then
                                                If bmp.Width > 960 Or bmp.Height > 540 Then
                                                    Dim bm_source As New Bitmap(bmp)
                                                    Dim bm_dest As New Bitmap(960, 540)
                                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                                    gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                                                    bm_dest.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                    Console.WriteLine("  Farart Resized to 960x540")
                                                Else
                                                    Console.WriteLine("  Fanart not resized, already =< required size")
                                                    bmp.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                End If

                                            End If
                                        Catch ex As Exception
                                            Try
                                                Console.WriteLine("Fanart Not Saved to :- " & fanarturlpath)
                                                Console.WriteLine("Error received :- " & ex.ToString)
                                            Catch
                                            End Try
                                        End Try

                                    Else
                                        'Console.writeline("No Fanart is Available For This Movie" & moviethumbpath)
                                    End If
                                Else
                                    'Console.writeline("Fanart Not Saved to :- " & moviethumbpath & ", file already exists")
                                End If

                            Catch
                            End Try
                        End If


                    End If
                    Dim tempst As String = movietoadd.fullpathandfilename
                    tempst = tempst.Replace(IO.Path.GetFileName(tempst), "tempoffline.ttt")
                    If IO.File.Exists(tempst) Then
                        IO.File.Delete(tempst)
                        Call offlinedvd(movietoadd.fullpathandfilename, movietoadd.title, getfilename(movietoadd.fullpathandfilename))
                    End If
                    Dim completebyte1 As Byte = 0
                    Dim fanartexists As Boolean = IO.File.Exists(Preferences.GetFanartPath(movietoadd.fullpathandfilename))
                    Dim posterexists As Boolean = IO.File.Exists(Preferences.GetPosterPath(movietoadd.fullpathandfilename))
                    If fanartexists = False Then
                        completebyte1 += 1
                    End If
                    If posterexists = False Then
                        completebyte1 += 2
                    End If
                    movietoadd.missingdata1 = completebyte1
                    fullMovieList.Add(movietoadd)
                End If
                Console.WriteLine("Movie added to list")
            End If
            Console.WriteLine()
            Console.WriteLine()
        Next
    End Sub

	 Private Function GetTrailerPath( s As String )
		return IO.Path.Combine(s.Replace(IO.Path.GetFileName(s), ""), System.IO.Path.GetFileNameWithoutExtension(s) & "-trailer.flv")
	End Function

	 Private Sub DownloadTrailer( trailerPath As String, trailerUrl As string )

		'Check for and delete zero length trailer - created when Url is invalid
		DeleteZeroLengthFile(trailerPath)

      If Not IO.File.Exists(trailerPath) Then
         If UrlIsValid(trailerUrl) Then
            
'            Dim wc As New Net.WebClient()

            Try
						Dim FileToBeDownloaded as WebFileDownloader
                  FileToBeDownloaded = New WebFileDownloader
                  FileToBeDownloaded.DownloadFileWithProgress(trailerurl, trailerPath)
            Catch
            End Try
         End If
		End If
	End Sub


	Private sub DeleteZeroLengthFile( fileName )

		If IO.File.Exists(fileName) then
			If (New IO.FileInfo(fileName)).Length = 0 then
				IO.File.Delete(fileName)
			End If
		End If

	End sub


	Private Function UrlIsValid(ByVal url As String) As Boolean
		 Dim is_valid As Boolean = False
		 If url.ToLower().StartsWith("www.") Then url = _
			  "http://" & url

		 Dim web_response As HttpWebResponse = Nothing
		 Try
			  Dim web_request As HttpWebRequest = _
					HttpWebRequest.Create(url)
			  web_response = _
					DirectCast(web_request.GetResponse(), _
					HttpWebResponse)
			  Return True
		 Catch ex As Exception
			  Return False
		 Finally
			  If Not (web_response Is Nothing) Then _
					web_response.Close()
		 End Try
	End Function





    'Date  : Nov11 
    'By    : AnotherPhil 
    'Blurb : This uses a revised hdtrailers.xml copied from metadata.common.hdtrailers.net
    '        Main changes
    '           - Scraping from http://www.hd-trailers.net/movie/movie-name instead of http://www.hd-trailers.net/blog/?s=&quot;movie-name_(Theatrical_Trailer)
    '           - Adds RegExs for the following sources:
    '               - http://videos.hd-trailers.net
    '               - http://pdl.stream
    '           - Fixed RegExs for:
    '               - http://playlist.yahoo.com (working versions copied from here: http://code.google.com/p/xbmchuscraper/source/browse/trunk/HDTrailersForEMM/hdtrailers.xml?spec=svn53&r=53) 
    '
    Public Function MC_Scraper_Get_HD_Trailer_URL( ByVal Resolution As String, ByVal MovieTitle As String ) As String

        Dim theScraper    As Scraper = ChooseScraper("metadata.mc.hdtrailers.net")
        Dim funcName      As String  = "GetHDTrailersnet" & Resolution.ToUpper() & "p"
        Dim funcParams(0) As String  

        'Prep movie title for HD-Trailers.net
        MovieTitle = Regex.Replace( MovieTitle, "[ ,']"    , "-"   )
        MovieTitle = Regex.Replace( MovieTitle, "[?!.;:@]" , ""    )
        MovieTitle = Regex.Replace( MovieTitle, "[&]"      , "and" )
        MovieTitle = Regex.Replace( MovieTitle, "-{2,}"    , "-"   )

        funcParams(0) = MovieTitle

        Dim url As String = ScraperQuery.ExecuteQuery(theScraper, funcName, funcParams)

        Dim match As Match = Regex.Match(url, "<trailer>(.*?)</trailer>")

        If match.Groups.Count = 2 Then
	        url = match.Groups(1).Value
            url = Replace( url, "&amp;", "&" )
        Else
            url = ""
        End If

        Return url

    End Function


    Private Function ChooseScraper(ByVal ScraperIdentification As String) As Scraper
		  Dim mScraperManager As ScraperManager

		  mScraperManager = New ScraperManager(IO.Path.Combine(My.Application.Info.DirectoryPath, "Assets\scrapers"))

        Dim TempScraper As Scraper = Nothing
        For Each c In mScraperManager.Scrapers
            If c.ID = ScraperIdentification Then
                TempScraper = c
                Exit For
            End If
        Next
        Return TempScraper
    End Function




    Public Function gettrailerurl(ByVal imdbid As String, ByVal imdbmirror As String)
        Dim allok As Boolean = False
        Dim first As Integer
        Dim last As Integer

        Dim tempstring As String = ""
        Try
            Dim webpage As List(Of String)
            tempstring = imdbmirror & "title/" & imdbid & "/trailers"
            webpage = loadwebpage(tempstring, False)
            For f = 0 To webpage.Count - 1
                If webpage(f).IndexOf("/screenplay/") <> -1 Then
                    first = webpage(f).IndexOf("")
                    Dim S As String = webpage(f)
                    Dim M As Match
                    M = Regex.Match(S, "\d{12}")
                    If M.Success = True Then
                        tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                    Else
                        M = Regex.Match(S, "\d{11}")
                        If M.Success = True Then
                            tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                        Else
                            M = Regex.Match(S, "\d{10}")
                            If M.Success = True Then
                                tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                            Else
                                M = Regex.Match(S, "\d{9}")
                                If M.Success = True Then
                                    tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                                Else
                                    M = Regex.Match(S, "\d{8}")
                                    If M.Success = True Then
                                        tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                                    Else
                                        M = Regex.Match(S, "\d{7}")
                                        If M.Success = True Then
                                            tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    webpage(f) = webpage(f).Substring(webpage(f).IndexOf("screenplay-") + 11, 12)

                    allok = True
                    Exit For
                End If
            Next
            If allok = True Then
                allok = False
                webpage.Clear()
                webpage = loadwebpage(tempstring, False)
                For f = 0 To webpage.Count - 1
                    If webpage(f).IndexOf("www.totaleclips.com") <> -1 Then
                        first = webpage(f).IndexOf("www.totaleclips.com")
                        last = webpage(f).IndexOf(""");")
                        webpage(f) = webpage(f).Substring(first, last - first)
                        allok = True
                        webpage(f) = webpage(f).Replace("%3A", ":")
                        webpage(f) = webpage(f).Replace("%2F", "/")
                        webpage(f) = webpage(f).Replace("%3F", "?")
                        webpage(f) = webpage(f).Replace("%3D", "=")
                        webpage(f) = webpage(f).Replace("%26", "&")
                        tempstring = webpage(f)
                        tempstring = "http://" & tempstring
                        Dim totalinfo As String = "<trailer>" & tempstring & "</trailer>" & vbCrLf
                        Return tempstring
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
        Finally
        End Try
        Return "Error"
    End Function

    Private Sub offlinedvd(ByVal nfopath As String, ByVal title As String, ByVal mediapath As String)
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
                fanartpath = defaultOfflineArt
            End If
            Dim curImage As Image = Image.FromFile(fanartpath)
            Dim tempstring As String = "Please Insert '" & title & "' DVD"

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
                    path = Preferences.applicationPath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    path = Preferences.applicationPath & "\Settings\0" & f.ToString & ".jpg"
                End If
                curImage.Save(path, Drawing.Imaging.ImageFormat.Jpeg)
            Next

            Dim myProcess As Process = New Process
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.FileName = Preferences.applicationPath & "\ffmpeg.exe"
            Dim proc_arguments As String = "-r 1 -b 1800 -qmax 6 -i """ & Preferences.applicationPath & "\Settings\%03d.jpg"" -vcodec msmpeg4v2 """ & mediapath & """"
            myProcess.StartInfo.Arguments = proc_arguments
            myProcess.Start()
            myProcess.WaitForExit()

            For f = 1 To 16
                Dim tempstring4 As String
                If f < 10 Then
                    tempstring4 = Preferences.applicationPath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    tempstring4 = Preferences.applicationPath & "\Settings\0" & f.ToString & ".jpg"
                End If
                Try
                    IO.File.Delete(tempstring4)
                Catch ex As Exception

                End Try
            Next
        Catch
        End Try
    End Sub

    Dim tvdburl As String
    Dim tvfblinecount As Integer
    Dim tvdbwebsource(3000) As String

    'Public Function getimdbactors(ByVal imdbmirror As String, Optional ByVal imdbid As String = "", Optional ByVal title As String = "", Optional ByVal maxactors As Integer = 9999)
    '    Dim webpage As New List(Of String)
    '    Dim actors(5000, 3)
    '    Dim tempstring As String
    '    Dim filterstring As String
    '    Dim actorcount As Integer
    '    Dim totalinfo As String = "<actorlist>"

    '    Try
    '        tempstring = imdbmirror & "title/" & imdbid & "/fullcredits#cast"
    '        webpage.Clear()
    '        webpage = loadwebpage(tempstring, False)

    '        Dim scrapertempint As Integer
    '        Dim scrapertempstring As String

    '        For Each line In webpage
    '            If line.IndexOf("Cast</a>") <> -1 Then
    '                line = line.Substring(line.IndexOf("Cast</a>"), line.Length - line.IndexOf("Cast</a>"))
    '                If line.IndexOf("<tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>") <> -1 Then
    '                    line = line.Replace("</td></tr> <tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>", "</td></tr><tr class")
    '                End If
    '                line = line.Substring(0, line.IndexOf("</table>"))
    '                scrapertempint = 0
    '                scrapertempstring = line
    '                Do Until scrapertempstring.IndexOf("<tr class=""") = -1
    '                    scrapertempint = scrapertempint + 1
    '                    actors(scrapertempint, 0) = scrapertempstring.Substring(0, scrapertempstring.IndexOf("</td></tr>") + 10)
    '                    scrapertempstring = scrapertempstring.Replace(actors(scrapertempint, 0), "")
    '                    filterstring = actors(scrapertempint, 0)
    '                    If filterstring <> actors(scrapertempint, 0) Then actors(scrapertempint, 0) = filterstring
    '                    If actors(scrapertempint, 0).IndexOf("other cast") <> -1 Then
    '                        actors(scrapertempint, 0) = Nothing
    '                        scrapertempint -= 1
    '                    End If
    '                Loop
    '                If scrapertempstring <> "" Then
    '                    scrapertempint = scrapertempint + 1
    '                    actors(scrapertempint, 0) = scrapertempstring
    '                End If
    '                actorcount = scrapertempint
    '                If actorcount > maxactors Then
    '                    actorcount = maxactors
    '                End If
    '                For g = 1 To actorcount
    '                    Try
    '                        actors(g, 3) = actors(g, 0).substring(actors(g, 0).indexof("<a href=""/name/nm") + 15, 9)
    '                        If actors(g, 0).IndexOf("http://resume.imdb.com") <> -1 Then actors(g, 0) = actors(g, 0).Replace("http://resume.imdb.com", "")
    '                        If actors(g, 0).IndexOf("http://i.media-imdb.com/images/tn15/addtiny.gif") <> -1 Then actors(g, 0) = actors(g, 0).Replace("http://i.media-imdb.com/images/tn15/addtiny.gif", "")
    '                        If actors(g, 0).indexof("http://ia.media-imdb.com/images/") <> -1 Then
    '                            Dim tempint6 As Integer
    '                            Dim tempint7 As Integer
    '                            tempint6 = actors(g, 0).indexof("http://ia.media-imdb.com/images/")
    '                            tempint7 = actors(g, 0).indexof("._V1._")
    '                            actors(g, 2) = actors(g, 0).substring(tempint6, tempint7 - tempint6 + 3)
    '                            actors(g, 2) = actors(g, 2) & "._V1._SY400_SX300_.jpg"
    '                        End If

    '                        If actors(g, 0).IndexOf("</td></tr></table>") <> -1 Then
    '                            scrapertempint = actors(g, 0).IndexOf("</td></tr></table>")
    '                            scrapertempstring = actors(g, 0).Substring(scrapertempint, actors(g, 0).Length - scrapertempint)
    '                            actors(g, 0) = actors(g, 0).Replace(scrapertempstring, "</td></tr><tr class")
    '                        End If

    '                        If actors(g, 0).IndexOf("a href=""/character") <> -1 Then
    '                            actors(g, 1) = actors(g, 0).Substring(actors(g, 0).IndexOf("a href=""/character") + 19, actors(g, 0).lastIndexOf("</td></tr>") - actors(g, 0).IndexOf("a href=""/character") - 19)
    '                            If actors(g, 1).IndexOf("</a>") <> -1 Then
    '                                actors(g, 1) = actors(g, 1).Substring(12, actors(g, 1).IndexOf("</a>") - 12)
    '                            ElseIf actors(g, 1).IndexOf("</a>") = -1 Then
    '                                actors(g, 1) = actors(g, 1).Substring(12, actors(g, 1).Length - 12)
    '                            End If
    '                            scrapertempstring = actors(g, 0).Substring(actors(g, 0).IndexOf("a href=""/character"), actors(g, 0).Length - actors(g, 0).IndexOf("a href=""/character"))
    '                            actors(g, 0) = actors(g, 0).Replace(scrapertempstring, "")
    '                            actors(g, 0) = actors(g, 0).Substring(0, actors(g, 0).lastindexof("</a>"))
    '                            actors(g, 0) = actors(g, 0).substring(actors(g, 0).lastindexof(">") + 1, actors(g, 0).length - actors(g, 0).lastindexof(">") - 1)
    '                        ElseIf actors(g, 0).IndexOf("a href=""/character") = -1 Then
    '                            actors(g, 0) = actors(g, 0).substring(0, actors(g, 0).length - 10)
    '                            actors(g, 1) = actors(g, 0).substring(actors(g, 0).lastindexof(">") + 1, actors(g, 0).length - actors(g, 0).lastindexof(">") - 1)
    '                            actors(g, 0) = actors(g, 0).Substring(0, actors(g, 0).lastindexof("</a>"))
    '                            actors(g, 0) = actors(g, 0).substring(actors(g, 0).lastindexof(">") + 1, actors(g, 0).length - actors(g, 0).lastindexof(">") - 1)
    '                        End If
    '                    Catch
    '                        Exit For
    '                    End Try
    '                Next
    '            End If

    '        Next
    '        For f = 1 To actorcount
    '            If actors(f, 0) <> Nothing Then
    '                totalinfo = totalinfo & "<actor>" & vbCrLf
    '                actors(f, 0) = cleanSpecChars(actors(f, 0))
    '                actors(f, 0) = encodespecialchrs(actors(f, 0))
    '                totalinfo = totalinfo & "<name>" & actors(f, 0) & "</name>" & vbCrLf
    '                If actors(f, 1) <> Nothing Then
    '                    actors(f, 1) = cleanSpecChars(actors(f, 1))
    '                    actors(f, 1) = encodespecialchrs(actors(f, 1))
    '                    totalinfo = totalinfo & "<role>" & actors(f, 1) & "</role>" & vbCrLf
    '                End If
    '                If actors(f, 2) <> Nothing Then
    '                    actors(f, 2) = encodespecialchrs(actors(f, 2))
    '                    totalinfo = totalinfo & "<thumb>" & actors(f, 2) & "</thumb>" & vbCrLf
    '                End If
    '                If actors(f, 3) <> Nothing Then
    '                    totalinfo = totalinfo & "<actorid>" & actors(f, 3) & "</actorid>" & vbCrLf
    '                End If
    '                totalinfo = totalinfo & "</actor>" & vbCrLf
    '            End If
    '        Next
    '        totalinfo = totalinfo & "</actorlist>"
    '        Return totalinfo
    '    Catch ex As Exception

    '    Finally
    '    End Try

    '    Return "Error"
    'End Function

    Private Function loadwebpage(ByVal url As String, ByVal method As Boolean)
        Dim webpage As New List(Of String)
        Try
            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(url)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream, System.Text.UTF8Encoding.UTF7)
            Dim sLine As String = ""

            If method = False Then
                Do While Not sLine Is Nothing

                    sLine = objReader.ReadLine
                    If Not sLine Is Nothing Then
                        webpage.Add(sLine)
                    End If
                Loop
            Else
                sLine = objReader.ReadToEnd
            End If
            objReader.Close()

            If method = False Then
                Return webpage
            Else
                Return sLine
            End If

        Catch ex As WebException
            'MsgBox("Unable to load webpage " & url & vbCrLf & vbCrLf & ex.ToString)
            If webpage.Count > 0 Then
                Return webpage
            Else
                webpage.Add("error")
                Return webpage
            End If
        Finally
        End Try
    End Function

    Public Function tmdbthumb(ByVal posterimdbid As String)
        Dim Scraper As ScraperFunctions = New ScraperFunctions()
        Return Scraper.tmdbthumb(posterimdbid)
    End Function

    Public Function mpdbthumb(ByVal posterimdbid As String)
        Dim scraper As ScraperFunctions = New ScraperFunctions()
        Return scraper.mpdbthumb(posterimdbid)
    End Function

    Public Function impathumb(ByVal title As String, ByVal year As String)
        Dim scraper As ScraperFunctions = New ScraperFunctions()
        Return scraper.impathumb(title, year)
    End Function

    Public Function imdbthumb(ByVal posterimdbid As String)
        Dim Scraper As ScraperFunctions = New ScraperFunctions()
        Return Scraper.imdbthumb(posterimdbid)
    End Function


    ' same as mov_NfoSave...
    Public Sub savemovienfo(ByVal filenameandpath As String, ByVal movietosave As FullMovieDetails, Optional ByVal overwrite As Boolean = True)

        Dim stage As Integer = 1
        Try
            If movietosave Is Nothing Then Exit Sub
            If Not IO.File.Exists(filenameandpath) Or overwrite = True Then
                'Try
                Dim doc As New XmlDocument
                Dim thumbnailstring As String = ""
                stage = 2
                Dim thispref As XmlNode = Nothing
                Dim xmlproc As XmlDeclaration

                xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                doc.AppendChild(xmlproc)
                Dim root As XmlElement = Nothing
                Dim child As XmlElement = Nothing
                Dim actorchild As XmlElement = Nothing
                Dim filedetailschild As XmlElement = Nothing
                Dim filedetailschildchild As XmlElement = Nothing
                Dim anotherchild As XmlElement = Nothing

                root = doc.CreateElement("movie")
                stage = 3
                If Preferences.enablehdtags = True Then
                    Try
                        child = doc.CreateElement("fileinfo")
                    Catch
                    End Try
                    Try
                        anotherchild = doc.CreateElement("streamdetails")
                    Catch ex As Exception

                    End Try
                    Try
                        filedetailschild = doc.CreateElement("video")
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.Width.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Width.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("width")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Width.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    Try
                        If movietosave.filedetails.filedetails_video.Height.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Height.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("height")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Height.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.Aspect.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Aspect.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("aspect")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Aspect.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 5
                    Try
                        If movietosave.filedetails.filedetails_video.Codec.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Codec.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codec")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Codec.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 6
                    Try
                        If movietosave.filedetails.filedetails_video.FormatInfo.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.FormatInfo.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("format")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.FormatInfo.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 7
                    Try
                        If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("duration")
                                Dim temptemp As String = movietosave.filedetails.filedetails_video.DurationInSeconds.Value
                                If Preferences.intruntime = True Then
                                    temptemp = Utilities.cleanruntime(movietosave.filedetails.filedetails_video.DurationInSeconds.Value)
                                    If IsNumeric(temptemp) Then
                                        filedetailschildchild.InnerText = temptemp
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    Else
                                        filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.DurationInSeconds.Value
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                Else
                                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.DurationInSeconds.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If

                            End If
                        End If
                    Catch
                    End Try
                    stage = 8
                    Try
                        If movietosave.filedetails.filedetails_video.Bitrate.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Bitrate.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitrate")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Bitrate.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 9
                    Try
                        If movietosave.filedetails.filedetails_video.BitrateMode.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.BitrateMode.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemode")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.BitrateMode.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 10
                    Try
                        If movietosave.filedetails.filedetails_video.BitrateMax.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.BitrateMax.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemax")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.BitrateMax.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 11
                    Try
                        If movietosave.filedetails.filedetails_video.Container.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Container.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("container")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Container.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 12
                    Try
                        If movietosave.filedetails.filedetails_video.CodecId.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.CodecId.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codecid")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.CodecId.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 13
                    Try
                        If movietosave.filedetails.filedetails_video.CodecInfo.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.CodecInfo.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codecidinfo")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.CodecInfo.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 14
                    Try
                        If movietosave.filedetails.filedetails_video.ScanType.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.ScanType.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("scantype")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.ScanType.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 15
                    Try
                        anotherchild.AppendChild(filedetailschild)
                    Catch
                    End Try

                    stage = 16

                    For Each item In movietosave.filedetails.filedetails_audio
                        Try
                            filedetailschild = doc.CreateElement("audio")
                        Catch
                        End Try
                        Try
                            If item.Language.Value <> Nothing Then
                                If item.Language.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = item.Language.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.Codec.Value <> Nothing Then
                                If item.Codec.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("codec")
                                    filedetailschildchild.InnerText = item.Codec.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.Channels.Value <> Nothing Then
                                If item.Channels.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("channels")
                                    filedetailschildchild.InnerText = item.Channels.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.Bitrate.Value <> Nothing Then
                                If item.Bitrate.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("bitrate")
                                    filedetailschildchild.InnerText = item.Bitrate.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        anotherchild.AppendChild(filedetailschild)
                    Next
                    stage = 17
                    Try
                        filedetailschild = doc.CreateElement("subtitle")
                    Catch
                    End Try
                    Dim tempint As Integer = 0
                    For Each entry In movietosave.filedetails.filedetails_subtitles
                        Try
                            If entry.language <> Nothing Then
                                If entry.Language.Value <> "" Then
                                    tempint += 1
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = entry.Language.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                    Next
                    stage = 18
                    Try
                        If tempint > 0 Then
                            anotherchild.AppendChild(filedetailschild)
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        child.AppendChild(anotherchild)
                    Catch
                    End Try
                    Try
                        root.AppendChild(child)
                    Catch
                    End Try
                End If



                Try
                    child = doc.CreateElement("title")
                    child.InnerText = movietosave.fullmoviebody.title
                    root.AppendChild(child)
                Catch
                End Try
                child = doc.CreateElement("originaltitle")
                If movietosave.fullmoviebody.originaltitle = Nothing Or movietosave.fullmoviebody.originaltitle = "" Then
                    child.InnerText = movietosave.fullmoviebody.title
                Else
                    child.InnerText = movietosave.fullmoviebody.originaltitle
                End If

                root.AppendChild(child)

                If movietosave.alternativetitles.Count > 0 Then
                    Try
                        For Each title In movietosave.alternativetitles
                            If title <> movietosave.fullmoviebody.title Then
                                Try
                                    child = doc.CreateElement("alternativetitle")
                                    child.InnerText = title
                                    root.AppendChild(child)
                                Catch ex As Exception

                                End Try
                            End If
                        Next
                    Catch
                    End Try
                End If

                'Try
                '    If movietosave.fullmoviebody.movieset <> Nothing Then
                '        If movietosave.fullmoviebody.movieset <> "-None-" Then
                '            child = doc.CreateElement("set")
                '            child.InnerText = movietosave.fullmoviebody.movieset
                '            root.AppendChild(child)
                '        End If
                '    End If
                'Catch ex As Exception

                'End Try

                Try
                    If movietosave.fullmoviebody.movieset <> "-None-" Then
                        Dim strArr() As String
                        strArr = movietosave.fullmoviebody.movieset.Split("/")
                        For count = 0 To strArr.Length - 1
                            child = doc.CreateElement("set")
                            strArr(count) = strArr(count).Trim
                            child.InnerText = strArr(count)
                            root.AppendChild(child)
                        Next
                    End If
                Catch
                End Try














                Try
                    If movietosave.fullmoviebody.sortorder = Nothing Then
                        movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                    End If
                    If movietosave.fullmoviebody.sortorder = "" Then
                        movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                    End If
                    child = doc.CreateElement("sorttitle")
                    child.InnerText = movietosave.fullmoviebody.sortorder
                    root.AppendChild(child)
                Catch
                End Try
                stage = 19
                Try
                    child = doc.CreateElement("year")
                    child.InnerText = movietosave.fullmoviebody.year
                    root.AppendChild(child)
                Catch
                End Try
                stage = 20
                Try
                    child = doc.CreateElement("premiered")
                    child.InnerText = movietosave.fullmoviebody.premiered
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("rating")
                    child.InnerText = movietosave.fullmoviebody.rating
                    root.AppendChild(child)
                Catch
                End Try
                stage = 21
                Try
                    child = doc.CreateElement("votes")
                    child.InnerText = movietosave.fullmoviebody.votes
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("top250")
                    child.InnerText = movietosave.fullmoviebody.top250
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("outline")
                    child.InnerText = movietosave.fullmoviebody.outline
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("plot")
                    child.InnerText = movietosave.fullmoviebody.plot
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("tagline")
                    child.InnerText = movietosave.fullmoviebody.tagline
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("country")
                    child.InnerText = movietosave.fullmoviebody.country
                    root.AppendChild(child)
                Catch
                End Try
                stage = 22
                Try
                    For Each thumbnail In movietosave.listthumbs
                        Try
                            child = doc.CreateElement("thumb")
                            child.InnerText = thumbnail
                            root.AppendChild(child)
                        Catch
                        End Try
                    Next
                Catch
                End Try
                stage = 23
                Try
                    If thumbnailstring <> "" Then
                        child = doc.CreateElement("thumb")
                        child.InnerText = thumbnailstring
                        root.AppendChild(child)
                    End If
                Catch
                End Try
                stage = 24
                Try
                    child = doc.CreateElement("runtime")
                    If movietosave.fullmoviebody.runtime <> Nothing Then
                        Dim minutes As String = movietosave.fullmoviebody.runtime
                        minutes = minutes.Replace("minutes", "")
                        minutes = minutes.Replace("mins", "")
                        minutes = minutes.Replace("min", "")
                        minutes = minutes.Replace(" ", "")
                        'If Preferences.intruntime = True And Not IsNumeric(minutes) Then
                        '    Dim tempstring As String = Form1.filefunction.cleanruntime(minutes)
                        '    If IsNumeric(tempstring) Then
                        '        minutes = tempstring
                        '    End If
                        'End If
                        Try
                            Do While minutes.IndexOf("0") = 0 And minutes.Length > 0
                                minutes = minutes.Substring(1, minutes.Length - 1)
                            Loop
                            If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Preferences.roundminutes = True Then
                                minutes = "0" & minutes
                            ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Preferences.roundminutes = True Then
                                minutes = "00" & minutes
                            End If
                            If Preferences.intruntime = False And IsNumeric(minutes) Then
                                minutes = minutes & " min"
                            End If
                        Catch ex As Exception
                            minutes = movietosave.fullmoviebody.runtime
                        End Try
                        child.InnerText = minutes
                    Else
                        child.InnerText = movietosave.fullmoviebody.runtime
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 25
                Try
                    child = doc.CreateElement("mpaa")
                    child.InnerText = movietosave.fullmoviebody.mpaa
                    root.AppendChild(child)
                Catch
                End Try
                stage = 26
                Try
                    If movietosave.fullmoviebody.genre <> "" Then
                        Dim strArr() As String
                        strArr = movietosave.fullmoviebody.genre.Split("/")
                        For count = 0 To strArr.Length - 1
                            child = doc.CreateElement("genre")
                            strArr(count) = strArr(count).Trim
                            child.InnerText = strArr(count)
                            root.AppendChild(child)
                        Next
                    End If
                Catch
                End Try
                stage = 27
                Try
                    child = doc.CreateElement("credits")
                    child.InnerText = movietosave.fullmoviebody.credits
                    root.AppendChild(child)
                Catch
                End Try
                stage = 28
                Try
                    child = doc.CreateElement("director")
                    child.InnerText = movietosave.fullmoviebody.director
                    root.AppendChild(child)
                Catch
                End Try
                stage = 29
                Try
                    child = doc.CreateElement("studio")
                    child.InnerText = movietosave.fullmoviebody.studio
                    root.AppendChild(child)
                Catch
                End Try
                stage = 30
                Try
                    child = doc.CreateElement("trailer")
                    child.InnerText = movietosave.fullmoviebody.trailer
                    root.AppendChild(child)
                Catch
                End Try
                stage = 31
                Try
                    child = doc.CreateElement("playcount")
                    child.InnerText = movietosave.fullmoviebody.playcount
                    root.AppendChild(child)
                Catch
                End Try
                stage = 32
                Try
                    If movietosave.fullmoviebody.imdbid <> Nothing Then
                        If movietosave.fullmoviebody.imdbid <> "" Then
                            child = doc.CreateElement("id")
                            child.InnerText = movietosave.fullmoviebody.imdbid
                            root.AppendChild(child)
                        Else

                        End If
                    Else

                    End If
                Catch
                End Try
                Try
                    If movietosave.fullmoviebody.source <> Nothing Then
                        If movietosave.fullmoviebody.source <> "" Then
                            child = doc.CreateElement("videosource")
                            child.InnerText = movietosave.fullmoviebody.source
                            root.AppendChild(child)
                        End If
                    End If
                Catch ex As Exception

                End Try
                Try
                    child = doc.CreateElement("createdate")
                    If movietosave.fileinfo.createdate = Nothing Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, Preferences.datePattern).ToString
                        Catch ex2 As Exception
                        End Try
                    ElseIf movietosave.fileinfo.createdate = "" Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, Preferences.datePattern).ToString
                        Catch ex2 As Exception
                        End Try
                    Else
                        child.InnerText = movietosave.fileinfo.createdate
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 33
                Try
                    child = doc.CreateElement("stars")
                    child.InnerText = movietosave.fullmoviebody.stars
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    Dim actorstosave As Integer = movietosave.listactors.Count
                    If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
                    For f = 0 To actorstosave - 1
                        child = doc.CreateElement("actor")
                        actorchild = doc.CreateElement("name")
                        actorchild.InnerText = movietosave.listactors(f).actorname
                        child.AppendChild(actorchild)
                        actorchild = doc.CreateElement("role")
                        actorchild.InnerText = movietosave.listactors(f).actorrole
                        child.AppendChild(actorchild)
                        If movietosave.listactors(f).actorthumb <> Nothing Then
                            If movietosave.listactors(f).actorthumb <> "" Then
                                Dim actorthumb As String = movietosave.listactors(f).actorthumb
                                actorchild = doc.CreateElement("thumb")
                                If Preferences.actorsave Then
                                    Dim uri As Uri
                                    uri = New Uri(actorthumb)

                                    If Len(Preferences.actornetworkpath) > 0 AndAlso Len(Preferences.actorsavepath) > 0 Then
                                        Dim actorThumbFileName As String
                                        Dim localActorThumbFileName As String
                                        actorThumbFileName = System.IO.Path.Combine(Preferences.actornetworkpath, uri.Segments(uri.Segments.GetLength(0) - 1))
                                        localActorThumbFileName = System.IO.Path.Combine(Preferences.actorsavepath, uri.Segments(uri.Segments.GetLength(0) - 1))

                                        Utilities.DownloadImage(uri.OriginalString, localActorThumbFileName, True, False)
                                        actorthumb = actorThumbFileName
                                    End If
                                End If
                                actorchild.InnerText = actorthumb
                                child.AppendChild(actorchild)
                            End If
                        End If
                        root.AppendChild(child)
                    Next
                    doc.AppendChild(root)
                Catch
                End Try
                stage = 34
                Try
                    Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
                    output.Formatting = Formatting.Indented
                    stage = 35
                    doc.WriteTo(output)
                    output.Close()
                Catch
                End Try
                'Catch ex As Exception
                '    MsgBox(ex.Message.ToString)
                'End Try
            Else
                MsgBox("File already exists")
            End If

        Catch ex As Exception
            MsgBox("Error Encountered at stage " & stage.ToString & vbCrLf & vbCrLf & ex.ToString)

        End Try
    End Sub



    Public Sub savemovienfo_old(ByVal filenameandpath As String, ByVal movietosave As fullmoviedetails, Optional ByVal overwrite As Boolean = True)
        Dim stage As Integer = 1
        Try
            If movietosave Is Nothing Then Exit Sub
            If Not IO.File.Exists(filenameandpath) Or overwrite = True Then
                'Try
                Dim doc As New XmlDocument
                Dim thumbnailstring As String = ""
                stage = 2
                Dim thispref As XmlNode = Nothing
                Dim xmlproc As XmlDeclaration

                xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                doc.AppendChild(xmlproc)
                Dim root As XmlElement = Nothing
                Dim child As XmlElement = Nothing
                Dim actorchild As XmlElement = Nothing
                Dim filedetailschild As XmlElement = Nothing
                Dim filedetailschildchild As XmlElement = Nothing
                Dim anotherchild As XmlElement = Nothing

                root = doc.CreateElement("movie")
                stage = 3
                If Preferences.enablehdtags = True Then
                    Try
                        child = doc.CreateElement("fileinfo")
                    Catch
                    End Try
                    Try
                        anotherchild = doc.CreateElement("streamdetails")
                    Catch ex As Exception

                    End Try
                    Try
                        filedetailschild = doc.CreateElement("video")
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.width <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Width.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("width")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Width.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    Try
                        If movietosave.filedetails.filedetails_video.height <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Height.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("height")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Height.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.aspect <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Aspect.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("aspect")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Aspect.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 5
                    Try
                        If movietosave.filedetails.filedetails_video.codec <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Codec.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codec")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Codec.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 6
                    Try
                        If movietosave.filedetails.filedetails_video.formatinfo <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Formatinfo.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("format")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Formatinfo.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 7
                    Try
                        If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("duration")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.DurationInSeconds.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 8
                    Try
                        If movietosave.filedetails.filedetails_video.bitrate <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Bitrate.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitrate")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Bitrate.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 9
                    Try
                        If movietosave.filedetails.filedetails_video.BitrateMode.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.BitrateMode.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemode")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.BitrateMode.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 10
                    Try
                        If movietosave.filedetails.filedetails_video.BitrateMax.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.BitrateMax.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemax")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.BitrateMax.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 11
                    Try
                        If movietosave.filedetails.filedetails_video.Container.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Container.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("container")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Container.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 12
                    Try
                        If movietosave.filedetails.filedetails_video.CodecId.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.CodecId.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codecid")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.CodecId.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 13
                    Try
                        If movietosave.filedetails.filedetails_video.CodecInfo.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.CodecInfo.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codecidinfo")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.CodecInfo.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 14
                    Try
                        If movietosave.filedetails.filedetails_video.ScanType.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.ScanType.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("scantype")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.ScanType.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 15
                    Try
                        anotherchild.AppendChild(filedetailschild)
                    Catch
                    End Try

                    stage = 16

                    For Each item In movietosave.filedetails.filedetails_audio
                        Try
                            filedetailschild = doc.CreateElement("audio")
                        Catch
                        End Try
                        Try
                            If item.Language.Value <> Nothing Then
                                If item.Language.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = item.Language.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.Codec.Value <> Nothing Then
                                If item.Codec.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("codec")
                                    filedetailschildchild.InnerText = item.Codec.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.Channels.Value <> Nothing Then
                                If item.Channels.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("channels")
                                    filedetailschildchild.InnerText = item.Channels.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.Bitrate.Value <> Nothing Then
                                If item.Bitrate.Value <> "" Then
                                    filedetailschildchild = doc.CreateElement("bitrate")
                                    filedetailschildchild.InnerText = item.Bitrate.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                    anotherchild.AppendChild(filedetailschild)
                                End If
                            End If
                        Catch
                        End Try
                    Next
                    stage = 17
                    Try
                        filedetailschild = doc.CreateElement("subtitle")
                    Catch
                    End Try
                    Dim tempint As Integer = 0
                    For Each entry In movietosave.filedetails.filedetails_subtitles
                        Try
                            If entry.Language.Value <> Nothing Then
                                If entry.Language.Value <> "" Then
                                    tempint += 1
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = entry.Language.Value
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                    Next
                    stage = 18
                    Try
                        If tempint > 0 Then
                            anotherchild.AppendChild(filedetailschild)
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        child.AppendChild(anotherchild)
                    Catch
                    End Try
                    Try
                        root.AppendChild(child)
                    Catch
                    End Try
                End If



                Try
                    child = doc.CreateElement("title")
                    child.InnerText = movietosave.fullmoviebody.title
                    root.AppendChild(child)
                Catch
                End Try

                child = doc.CreateElement("originaltitle")
                If movietosave.fullmoviebody.originaltitle = Nothing Or movietosave.fullmoviebody.originaltitle = "" Then
                    child.InnerText = movietosave.fullmoviebody.title
                Else
                    child.InnerText = movietosave.fullmoviebody.originaltitle
                End If

                root.AppendChild(child)

                If movietosave.alternativetitles.Count > 0 Then
                    Try
                        For Each title In movietosave.alternativetitles
                            If title <> movietosave.fullmoviebody.title Then
                                Try
                                    child = doc.CreateElement("alternativetitle")
                                    child.InnerText = title
                                    root.AppendChild(child)
                                Catch ex As Exception

                                End Try
                            End If
                        Next
                    Catch
                    End Try
                End If

                Try
                    If movietosave.fullmoviebody.movieset <> Nothing Then
                        If movietosave.fullmoviebody.movieset <> "-none-" Then
                            child = doc.CreateElement("set")
                            child.InnerText = movietosave.fullmoviebody.movieset
                            root.AppendChild(child)
                        End If
                    End If
                Catch ex As Exception

                End Try
                Try
                    If movietosave.fullmoviebody.sortorder = Nothing Then
                        movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                    End If
                    If movietosave.fullmoviebody.sortorder = "" Then
                        movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                    End If
                    child = doc.CreateElement("sorttitle")
                    child.InnerText = movietosave.fullmoviebody.sortorder
                    root.AppendChild(child)
                Catch
                End Try
                stage = 19
                Try
                    child = doc.CreateElement("year")
                    child.InnerText = movietosave.fullmoviebody.year
                    root.AppendChild(child)
                Catch
                End Try
                stage = 20
                Try
                    child = doc.CreateElement("premiered")
                    child.InnerText = movietosave.fullmoviebody.premiered
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("rating")
                    child.InnerText = movietosave.fullmoviebody.rating
                    root.AppendChild(child)
                Catch
                End Try
                stage = 21
                Try
                    child = doc.CreateElement("votes")
                    child.InnerText = movietosave.fullmoviebody.votes
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("top250")
                    child.InnerText = movietosave.fullmoviebody.top250
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("outline")
                    child.InnerText = movietosave.fullmoviebody.outline
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("plot")
                    child.InnerText = movietosave.fullmoviebody.plot
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("tagline")
                    child.InnerText = movietosave.fullmoviebody.tagline
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("country")
                    child.InnerText = movietosave.fullmoviebody.country
                    root.AppendChild(child)
                Catch
                End Try
                stage = 22
                Try
                    For Each thumbnail In movietosave.listthumbs
                        Try
                            child = doc.CreateElement("thumb")
                            child.InnerText = thumbnail
                            root.AppendChild(child)
                        Catch
                        End Try
                    Next
                Catch
                End Try
                stage = 23
                Try
                    If thumbnailstring <> "" Then
                        child = doc.CreateElement("thumb")
                        child.InnerText = thumbnailstring
                        root.AppendChild(child)
                    End If
                Catch
                End Try
                stage = 24
                Try
                    child = doc.CreateElement("runtime")
                    If movietosave.fullmoviebody.runtime <> Nothing Then
                        Dim minutes As String = movietosave.fullmoviebody.runtime
                        minutes = minutes.Replace("minutes", "")
                        minutes = minutes.Replace("mins", "")
                        minutes = minutes.Replace("min", "")
                        minutes = minutes.Replace(" ", "")
                        Try
                            Do While minutes.IndexOf("0") = 0 And minutes.Length > 0
                                minutes = minutes.Substring(1, minutes.Length - 1)
                            Loop
                            If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Preferences.roundminutes = True Then
                                minutes = "0" & minutes
                            ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Preferences.roundminutes = True Then
                                minutes = "00" & minutes
                            End If
                            If Preferences.intruntime = False And IsNumeric(minutes) Then
                                minutes = minutes & " min"
                            End If
                        Catch ex As Exception
                            minutes = movietosave.fullmoviebody.runtime
                        End Try
                        child.InnerText = minutes
                    Else
                        child.InnerText = movietosave.fullmoviebody.runtime
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 25
                Try
                    child = doc.CreateElement("mpaa")
                    child.InnerText = movietosave.fullmoviebody.mpaa
                    root.AppendChild(child)
                Catch
                End Try
                stage = 26
                Try
                    If movietosave.fullmoviebody.genre <> "" Then
                        Dim strArr() As String
                        strArr = movietosave.fullmoviebody.genre.Split("/")
                        For count = 0 To strArr.Length - 1
                            child = doc.CreateElement("genre")
                            strArr(count) = strArr(count).Trim
                            child.InnerText = strArr(count)
                            root.AppendChild(child)
                        Next
                    End If
                Catch
                End Try
                stage = 27
                Try
                    child = doc.CreateElement("credits")
                    child.InnerText = movietosave.fullmoviebody.credits
                    root.AppendChild(child)
                Catch
                End Try
                stage = 28
                Try
                    child = doc.CreateElement("director")
                    child.InnerText = movietosave.fullmoviebody.director
                    root.AppendChild(child)
                Catch
                End Try
                stage = 29
                Try
                    child = doc.CreateElement("studio")
                    child.InnerText = movietosave.fullmoviebody.studio
                    root.AppendChild(child)
                Catch
                End Try
                stage = 30
                Try
                    child = doc.CreateElement("trailer")
                    child.InnerText = movietosave.fullmoviebody.trailer
                    root.AppendChild(child)
                Catch
                End Try
                stage = 31
                Try
                    child = doc.CreateElement("playcount")
                    child.InnerText = movietosave.fullmoviebody.playcount
                    root.AppendChild(child)
                Catch
                End Try
                stage = 32
                Try
                    If movietosave.fullmoviebody.imdbid <> Nothing Then
                        If movietosave.fullmoviebody.imdbid <> "" Then
                            child = doc.CreateElement("id")
                            child.InnerText = movietosave.fullmoviebody.imdbid
                            root.AppendChild(child)
                        Else

                        End If
                    Else

                    End If
                Catch
                End Try
                Try
                    If movietosave.fullmoviebody.source <> Nothing Then
                        If movietosave.fullmoviebody.source <> "" Then
                            child = doc.CreateElement("videosource")
                            child.InnerText = movietosave.fullmoviebody.source
                            root.AppendChild(child)
                        End If
                    End If
                Catch ex As Exception

                End Try
                Try
                    child = doc.CreateElement("createdate")
                    If movietosave.fileinfo.createdate = Nothing Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, "yyyyMMddHHmmss").ToString
                        Catch ex2 As Exception
                        End Try
                    ElseIf movietosave.fileinfo.createdate = "" Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, "yyyyMMddHHmmss").ToString
                        Catch ex2 As Exception
                        End Try
                    Else
                        child.InnerText = movietosave.fileinfo.createdate
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 33
                Try
                    child = doc.CreateElement("stars")
                    child.InnerText = movietosave.fullmoviebody.stars
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    Dim actorstosave As Integer = movietosave.listactors.Count
                    If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
                    For f = 0 To actorstosave - 1
                        child = doc.CreateElement("actor")
                        actorchild = doc.CreateElement("name")
                        actorchild.InnerText = movietosave.listactors(f).actorname
                        child.AppendChild(actorchild)
                        actorchild = doc.CreateElement("role")
                        actorchild.InnerText = movietosave.listactors(f).actorrole
                        child.AppendChild(actorchild)
                        If movietosave.listactors(f).actorthumb <> Nothing Then
                            If movietosave.listactors(f).actorthumb <> "" Then
                                Dim actorthumb As String = movietosave.listactors(f).actorthumb
                                actorchild = doc.CreateElement("thumb")
                                If Preferences.actorsave Then
                                    Dim uri As Uri
                                    uri = New Uri(actorthumb)

                                    If Len(Preferences.actornetworkpath) > 0 AndAlso Len(Preferences.actorsavepath) > 0 Then
                                        Dim actorThumbFileName As String
                                        Dim localActorThumbFileName As String
                                        actorThumbFileName = System.IO.Path.Combine(Preferences.actornetworkpath, uri.Segments(uri.Segments.GetLength(0) - 1))
                                        localActorThumbFileName = System.IO.Path.Combine(Preferences.actorsavepath, uri.Segments(uri.Segments.GetLength(0) - 1))

                                        Utilities.DownloadImage(uri.OriginalString, localActorThumbFileName, True, False)
                                        actorthumb = actorThumbFileName
                                    End If
                                End If
                                actorchild.InnerText = actorthumb
                                child.AppendChild(actorchild)
                            End If
                        End If
                        root.AppendChild(child)
                    Next
                    doc.AppendChild(root)
                Catch
                End Try
                stage = 34
                Try
                    Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
                    output.Formatting = Formatting.Indented
                    stage = 35
                    doc.WriteTo(output)
                    output.Close()
                Catch
                End Try
                'Catch ex As Exception
                '    MsgBox(ex.Message.ToString)
                'End Try
            Else
                MsgBox("File already exists")
            End If

        Catch ex As Exception
            MsgBox("Error Encountered at stage " & stage.ToString & vbCrLf & vbCrLf & ex.ToString)
        End Try
    End Sub

    Public Function cleanSpecChars(ByVal string2clean As String) As String
        Return WebUtility.HtmlDecode(string2clean)
    End Function

    Private Function encodespecialchrs(ByVal text As String)
        If text.IndexOf("&") <> -1 Then text = text.Replace("&", "&amp;")
        If text.IndexOf("<") <> -1 Then text = text.Replace("<", "&lt;")
        If text.IndexOf(">") <> -1 Then text = text.Replace(">", "&gt;")
        If text.IndexOf(Chr(34)) <> -1 Then text = text.Replace(Chr(34), "&quot;")
        If text.IndexOf("'") <> -1 Then text = text.Replace("'", "&apos;")
        Return text
    End Function

    Public Function CharCount(ByVal OrigString As String, ByVal Chars As String, Optional ByVal CaseSensitive As Boolean = False) As Long
        Dim lLen As Long
        Dim lCharLen As Long
        Dim lAns As Long
        Dim sInput As String
        Dim sChar As String
        Dim lCtr As Long
        Dim lEndOfLoop As Long
        Dim bytCompareType As Byte

        sInput = OrigString
        If sInput = "" Then Exit Function
        lLen = Len(sInput)
        lCharLen = Len(Chars)
        lEndOfLoop = (lLen - lCharLen) + 1
        bytCompareType = IIf(CaseSensitive, vbBinaryCompare, _
           vbTextCompare)

        For lCtr = 1 To lEndOfLoop
            sChar = Mid(sInput, lCtr, lCharLen)
            If StrComp(sChar, Chars, bytCompareType) = 0 Then _
                lAns = lAns + 1
        Next

        CharCount = lAns
    End Function

    Function GetGenres(ByVal webPage As String)
        Dim genres As New List(Of String)
        Dim genre As String

        For Each m As Match In Regex.Matches(webPage, "/genre/.*?>(?<genre>.*?)</a>")

            genre = m.Groups("genre").Value

            If Not genres.Contains(genre) Then
                genres.Add(genre)
            End If

        Next

        Return genres
    End Function


End Module

Public Structure listofposters
    Dim hdposter As String
    Dim ldposter As String
End Structure

Public Structure arguments
    Dim switch As String
    Dim argu As String
End Structure

Public Structure tvpostertype
    Dim posterurl As String
    Dim tvpostertype As String
End Structure

Public Structure tvposterseason
    Dim posterurl As String
    Dim posterseason As String
    Dim postertype As String
End Structure

Public Class tvart
    Dim tvfanart As List(Of String)
    Dim tvposterseasons As List(Of tvposterseason)
    Dim tvposters As List(Of String)
End Class

'Public Structure actordatabase
'    Dim actorname As String
'    Dim movieid As String
'End Structure

'Public Structure basicmovienfo
'    Dim title As String
'    Dim originaltitle As String
'    Dim sortorder As String
'    Dim movieset As String
'    Dim source As String
'    Dim year As String
'    Dim rating As String
'    Dim votes As String
'    Dim outline As String
'    Dim plot As String
'    Dim tagline As String
'    Dim runtime As String
'    Dim mpaa As String
'    Dim genre As String
'    Dim credits As String
'    Dim director As String
'    Dim stars As String
'    Dim premiered As String
'    Dim studio As String
'    Dim trailer As String
'    Dim playcount As String
'    Dim imdbid As String
'    Dim top250 As String
'    Dim filename As String
'    Dim thumbnails As String
'    Dim fanart As String
'    Dim country As String
'End Structure

Public Class basictvshownfo
    Public fullpath As String
    Public title As String
    Public id As String
    Public sortorder As String
    Public language As String
    Public episodeactorsource As String
    Public locked As String
    Public imdbid As String
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

'Public Structure movieactors
'    Dim actorname As String
'    Dim actorrole As String
'    Dim actorthumb As String
'End Structure

'Public Class fullmoviedetails
'    Public fileinfo As New str_FileDetails
'    Public fullmoviebody As New str_BasicMovieNFO
'    Public alternativetitles As New List(Of String)
'    Public listactors As New List(Of str_MovieActors)
'    Public listthumbs As New List(Of String)
'    Public filedetails As New FullFileDetails 'fullfiledetails
'End Class

Public Class fullfiledetails2
    Public filedetails_video As medianfo_video
    Public filedetails_audio As New List(Of medianfo_audio)
    Public filedetails_subtitles As New List(Of medianfo_subtitles)
End Class

'Public Structure filedetails
'    Dim fullpathandfilename As String
'    Dim path As String
'    Dim filename As String
'    Dim foldername As String
'    Dim fanartpath As String
'    Dim posterpath As String
'    Dim trailerpath As String
'    Dim createdate As String
'End Structure

'Public Structure newmovie
'    Dim nfopathandfilename As String
'    Dim nfopath As String
'    Dim title As String
'    Dim mediapathandfilename As String
'End Structure

'Public Structure combolist
'    Dim fullpathandfilename As String
'    Dim movieset As String
'    Dim filename As String
'    Dim foldername As String
'    Dim title As String
'    Dim titleandyear As String
'    Dim year As String
'    Dim filedate As String
'    Dim id As String
'    Dim rating As String
'    Dim top250 As String
'    Dim genre As String
'    Dim playcount As String
'    Dim sortorder As String
'    Dim outline As String
'    Dim runtime As String
'    Dim createdate As String
'    Dim missingdata1 As Byte
'End Structure

Public Structure runningthreads
    Dim thread1 As Boolean
    Dim thread2 As Boolean
    Dim thread3 As Boolean
    Dim thread4 As Boolean
    Dim thread5 As Boolean
    Dim thread6 As Boolean
End Structure

Public Class mediainfodll_complete
    Dim videodetails As New medianfo_video
    Dim audiodetails As New medianfo_audio
    Dim subsdetails As New List(Of medianfo_subtitles)
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

Public Structure batchwizard
    Dim title As Boolean
    Dim votes As Boolean
    Dim rating As Boolean
    Dim top250 As Boolean
    Dim runtime As Boolean
    Dim director As Boolean
    Dim year As Boolean
    Dim outline As Boolean
    Dim plot As Boolean
    Dim tagline As Boolean
    Dim genre As Boolean
    Dim studio As Boolean
    Dim premiered As Boolean
    Dim mpaa As Boolean
    Dim trailer As Boolean
    Dim credits As Boolean
    Dim posterurls As Boolean

    Dim actors As Boolean

    Dim mediatags As Boolean

    Dim missingposters As Boolean
    Dim missingfanart As Boolean

    Dim activate As Boolean
End Structure

'Public Structure listofprofiles
'    Dim moviecache As String
'    Dim tvcache As String
'    Dim actorcache As String
'    Dim profilename As String
'    Dim regexlist As String
'    Dim filters As String
'    Dim config As String
'End Structure

'Public Class profiles
'    Public startupprofile As String
'    Public defaultprofile As String
'    Public workingprofilename As String
'    Public profilelist As New List(Of listofprofiles)
'End Class

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

Public Enum InfoOptions As UInteger
    ShowInInform
    Reserved
    ShowInSupported
    TypeOfValue
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
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    Try
                                        tempstring = tempstring.TrimStart("|")
                                        tempstring = tempstring.TrimEnd("|")
                                        Dim tvtempstring2 As String
                                        Dim tvtempint As Integer
                                        Dim a() As String
                                        Dim j As Integer
                                        tvtempstring2 = ""
                                        a = tempstring.Split("|")
                                        tvtempint = a.GetUpperBound(0)
                                        tvtempstring2 = a(0)
                                        If tvtempint >= 0 Then
                                            For j = 0 To tvtempint
                                                Try
                                                    episodestring = episodestring & "<actor>" & "<name>" & a(j) & "</name></actor>"
                                                Catch
                                                End Try
                                            Next
                                        End If
                                    Catch
                                    End Try
                                Case "Director"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.TrimEnd("|")
                                    episodestring = episodestring & "<director>" & tempstring & "</director>"
                                Case "Writer"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.TrimEnd("|")
                                    episodestring = episodestring & "<credits>" & tempstring & "</credits>"
                                Case "Overview"
                                    episodestring = episodestring & "<plot>" & mirrorselection.InnerXml & "</plot>"
                                Case "Rating"
                                    episodestring = episodestring & "<rating>" & mirrorselection.InnerXml & "</rating>"
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