Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
Imports System.Management
Imports System.Xml
Imports imdb.Classimdbscraper
Imports System.Reflection
Imports System.ComponentModel
Imports System
Imports System.Collections.Generic
Imports IMPA
Imports System.Runtime.InteropServices
Imports System.Drawing


Module Module1
    Dim arguments As New List(Of String)
    Dim htmlfileoutput As String = ""
    Dim userprefs As New preferences
    Dim applicationpath As String = System.AppDomain.CurrentDomain.BaseDirectory
    Dim moviefolders As New List(Of String)
    Dim tvfolders As New List(Of String)
    Dim profile_structure As New profiles
    Dim workingprofile As New listofprofiles
    Dim listofargs As New List(Of arguments)
    Dim profile As String = "default"
    Dim tvrootfolders As New List(Of String)
    Dim fullmovielist As New List(Of combolist)
    Dim totalepisodecount As Integer = 0
    Dim totaltvshowcount As Integer = 0
    Dim basictvlist As New List(Of basictvshownfo)
    Dim newmovielist As New List(Of newmovie)
    Dim imdbcounter As Integer = 0
    Dim defaultofflineart As String = ""
    Dim actordb As New List(Of actordatabase)
    Dim showstoscrapelist As New List(Of String)
    Dim newepisodelist As New List(Of episodeinfo)
    Dim tvregex As New List(Of String)
    Dim defaultposter As String = ""
    Sub Main()
        Dim domovies As Boolean = False
        Dim dotvepisodes As Boolean = False
        Dim dohtmploutput As Boolean = False
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
                ElseIf arguments(f) = "-h" Then
                    Dim arg As New arguments
                    arg.switch = arguments(f)
                    Try
                        arg.argu = arguments(f + 1)
                        listofargs.Add(arg)
                        Try
                            htmlfileoutput = arguments(f + 2)
                            dohtmploutput = True
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
            Console.WriteLine("mc_com.exe [-m] [-e] [-p ProfileName] [-h templatename outputpath]")
            Console.WriteLine("-m to scrape movies")
            Console.WriteLine("-e to scrape episodes")
            Console.WriteLine("-h [templatename] [outputpath] to output html list ")
            Console.WriteLine()
            Console.WriteLine("Example")
            Console.WriteLine("mc_com.exe -m -e -p billy -h basiclist C:\Movielist\testfile.html")
            Console.WriteLine("will search for and scrape any new movies and episodes")
            Console.WriteLine("using the folders and settings of the 'billy' profile")
            Console.WriteLine("then create a new html list using the named template")
            Console.WriteLine("Without the profile arg the defauld profile will be used")
            Console.WriteLine()
            Console.WriteLine("Tip: When using profile, template, or filenames that contain")
            Console.WriteLine("spaces, enclose with quotes, eg.")
            Console.WriteLine("mc_com.exe -m -p ""my profile"" -h ""new list"" ""C:\Movie list\test.html""")
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
            If arg.switch = "-h" Then
                dohtmploutput = True
            End If
        Next
        Dim done As Boolean = False
        defaultposter = IO.Path.Combine(applicationpath, "Resources\default_poster.jpg")
        Console.WriteLine("Loading Config")
        Call setuppreferences()
        If IO.File.Exists(applicationpath & "\settings\profile.xml") = True Then
            Call loadprofiles()
            If profile = "default" Then
                profile = profile_structure.defaultprofile
            End If
            For Each prof In profile_structure.profilelist
                If prof.profilename = profile Then
                    workingprofile.actorcache = prof.actorcache
                    workingprofile.config = prof.config
                    workingprofile.moviecache = prof.moviecache
                    workingprofile.profilename = prof.profilename
                    workingprofile.regexlist = prof.regexlist
                    workingprofile.filters = prof.filters
                    workingprofile.tvcache = prof.tvcache
                    workingprofile.profilename = prof.profilename
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
            Console.WriteLine("Unable to find profile file: " & applicationpath & "\settings\profile.xml")
            Console.WriteLine("****************************************************")
            System.Environment.Exit(1)
        End If
        defaultofflineart = IO.Path.Combine(applicationpath, "Resources\default_offline.jpg")
        If IO.File.Exists(workingprofile.config) Then
            Call loadconfig()
        End If
        If domovies = True Or dohtmploutput = True Then
            If IO.File.Exists(workingprofile.moviecache) Then
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
            If IO.File.Exists(workingprofile.tvcache) Then
                Call loadtvcache()
            End If
            If IO.File.Exists(workingprofile.regexlist) Then
                Call loadregex()
            End If
            If tvregex.Count = 0 Then
                tvregex.Add("[Ss]([\d]{1,4}).?[Ee]([\d]{1,4})")
                tvregex.Add("([\d]{1,4}) ?[xX] ?([\d]{1,4})")
                tvregex.Add("([0-9]+)([0-9][0-9])")
            End If
            For Each item In basictvlist
                If item.fullpath.ToLower.IndexOf("tvshow.nfo") <> -1 Then
                    showstoscrapelist.Add(item.fullpath)
                End If
            Next
            If showstoscrapelist.Count > 0 Then
                Call episodescraper(showstoscrapelist, False)
                Call savetvcache()
            End If
        End If
        If dohtmploutput = True Then
            For Each arg In listofargs
                If arg.switch = "-h" Then
                    Console.WriteLine("Starting HTML Output")
                    Console.WriteLine()
                    Call addhtmltemplates()
                    Call setuphtml(arg.argu)
                    Console.WriteLine("HTML output complete")
                    Console.WriteLine()
                End If
            Next

        End If
        Console.WriteLine()
        Console.WriteLine("Tasks Completed")
        Console.WriteLine("****************************************************")
        System.Environment.Exit(0)
    End Sub
    Public Function loadfullmovienfo(ByVal path As String)
        Try
            Dim newmovie As New fullmoviedetails
            Dim thumbstring As String
            If Not IO.File.Exists(path) Then
            Else
                Dim movie As New XmlDocument
                Try
                    movie.Load(path)
                Catch ex As Exception
                    Dim errorstring As String
                    errorstring = ex.Message.ToString & vbCrLf & vbCrLf
                    errorstring += ex.StackTrace.ToString
                    newmovie.fullmoviebody.title = cleanfilename(IO.Path.GetFileName(path))
                    newmovie.fullmoviebody.year = "0000"
                    newmovie.fullmoviebody.top250 = "0"
                    newmovie.fullmoviebody.playcount = "0"
                    newmovie.fullmoviebody.credits = ""
                    newmovie.fullmoviebody.director = ""
                    newmovie.fullmoviebody.filename = ""
                    newmovie.fullmoviebody.genre = ""
                    newmovie.fullmoviebody.imdbid = ""
                    newmovie.fullmoviebody.mpaa = ""
                    newmovie.fullmoviebody.outline = "This nfo file could not be loaded"
                    newmovie.fullmoviebody.playcount = "0"
                    newmovie.fullmoviebody.plot = errorstring
                    newmovie.fullmoviebody.premiered = ""
                    newmovie.fullmoviebody.rating = ""
                    newmovie.fullmoviebody.runtime = ""
                    newmovie.fullmoviebody.studio = ""
                    newmovie.fullmoviebody.tagline = "Rescrapeing the movie should fix the problem"
                    newmovie.fullmoviebody.trailer = ""
                    newmovie.fullmoviebody.votes = ""
                    newmovie.fullmoviebody.sortorder = ""
                    newmovie.fileinfo.createdate = "99999999"
                    Return newmovie
                    Exit Function
                End Try
                Dim thisresult As XmlNode = Nothing

                For Each thisresult In movie("movie")
                    Select Case thisresult.Name
                        Case "alternativetitle"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                        Case "set"
                            newmovie.fullmoviebody.movieset = thisresult.InnerText
                        Case "sortorder"
                            newmovie.fullmoviebody.sortorder = thisresult.InnerText
                        Case "sorttitle"
                            newmovie.fullmoviebody.sortorder = thisresult.InnerText
                        Case "votes"
                            newmovie.fullmoviebody.votes = thisresult.InnerText
                        Case "outline"
                            newmovie.fullmoviebody.outline = thisresult.InnerText
                        Case "plot"
                            newmovie.fullmoviebody.plot = thisresult.InnerText
                        Case "tagline"
                            newmovie.fullmoviebody.tagline = thisresult.InnerText
                        Case "runtime"
                            newmovie.fullmoviebody.runtime = thisresult.InnerText
                        Case "mpaa"
                            newmovie.fullmoviebody.mpaa = thisresult.InnerText
                        Case "credits"
                            newmovie.fullmoviebody.credits = thisresult.InnerText
                        Case "director"
                            newmovie.fullmoviebody.director = thisresult.InnerText
                        Case "thumb"
                            If thisresult.InnerText.IndexOf("&lt;thumbs&gt;") <> -1 Then
                                thumbstring = thisresult.InnerText
                            Else
                                newmovie.listthumbs.Add(thisresult.InnerText)
                            End If

                        Case "premiered"
                            newmovie.fullmoviebody.premiered = thisresult.InnerText
                        Case "studio"
                            newmovie.fullmoviebody.studio = thisresult.InnerText
                        Case "trailer"
                            newmovie.fullmoviebody.trailer = thisresult.InnerText
                        Case "title"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                            newmovie.fullmoviebody.title = thisresult.InnerText
                        Case "year"
                            newmovie.fullmoviebody.year = thisresult.InnerText
                        Case "genre"
                            newmovie.fullmoviebody.genre = thisresult.InnerText
                        Case "id"
                            newmovie.fullmoviebody.imdbid = thisresult.InnerText
                        Case "playcount"
                            newmovie.fullmoviebody.playcount = thisresult.InnerText
                        Case "rating"
                            newmovie.fullmoviebody.rating = thisresult.InnerText
                            If newmovie.fullmoviebody.rating.IndexOf("/10") <> -1 Then newmovie.fullmoviebody.rating.Replace("/10", "")
                            If newmovie.fullmoviebody.rating.IndexOf(" ") <> -1 Then newmovie.fullmoviebody.rating.Replace(" ", "")
                        Case "top250"
                            newmovie.fullmoviebody.top250 = thisresult.InnerText
                        Case "createdate"
                            newmovie.fileinfo.createdate = thisresult.InnerText
                        Case "actor"
                            Dim newactor As New movieactors
                            Dim detail As XmlNode = Nothing
                            For Each detail In thisresult.ChildNodes
                                Select Case detail.Name
                                    Case "name"
                                        newactor.actorname = detail.InnerText
                                    Case "role"
                                        newactor.actorrole = detail.InnerText
                                    Case "thumb"
                                        newactor.actorthumb = detail.InnerText
                                End Select
                            Next
                            newmovie.listactors.Add(newactor)
                        Case "fileinfo"
                            Dim what As XmlNode = Nothing
                            For Each res In thisresult.ChildNodes
                                Select Case res.name
                                    Case "streamdetails"
                                        Dim newfilenfo As New fullfiledetails
                                        Dim detail As XmlNode = Nothing
                                        For Each detail In res.ChildNodes
                                            Select Case detail.Name
                                                Case "video"
                                                    Dim videodetails As XmlNode = Nothing
                                                    For Each videodetails In detail.ChildNodes
                                                        Select Case videodetails.Name
                                                            Case "width"
                                                                newfilenfo.filedetails_video.width = videodetails.InnerText
                                                            Case "height"
                                                                newfilenfo.filedetails_video.height = videodetails.InnerText
                                                            Case "aspect"
                                                                newfilenfo.filedetails_video.aspect = videodetails.InnerText
                                                            Case "codec"
                                                                newfilenfo.filedetails_video.codec = videodetails.InnerText
                                                            Case "formatinfo"
                                                                newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
                                                            Case "duration"
                                                                newfilenfo.filedetails_video.duration = videodetails.InnerText
                                                            Case "bitrate"
                                                                newfilenfo.filedetails_video.bitrate = videodetails.InnerText
                                                            Case "bitratemode"
                                                                newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
                                                            Case "bitratemax"
                                                                newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
                                                            Case "container"
                                                                newfilenfo.filedetails_video.container = videodetails.InnerText
                                                            Case "codecid"
                                                                newfilenfo.filedetails_video.codecid = videodetails.InnerText
                                                            Case "codecidinfo"
                                                                newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
                                                            Case "scantype"
                                                                newfilenfo.filedetails_video.scantype = videodetails.InnerText
                                                        End Select
                                                    Next
                                                Case "audio"
                                                    Dim audiodetails As XmlNode = Nothing
                                                    Dim audio As New medianfo_audio
                                                    For Each audiodetails In detail.ChildNodes

                                                        Select Case audiodetails.Name
                                                            Case "language"
                                                                audio.language = audiodetails.InnerText
                                                            Case "codec"
                                                                audio.codec = audiodetails.InnerText
                                                            Case "channels"
                                                                audio.channels = audiodetails.InnerText
                                                            Case "bitrate"
                                                                audio.bitrate = audiodetails.InnerText
                                                        End Select
                                                    Next
                                                    newfilenfo.filedetails_audio.Add(audio)
                                                Case "subtitle"
                                                    Dim subsdetails As XmlNode = Nothing
                                                    For Each subsdetails In detail.ChildNodes
                                                        Select Case subsdetails.Name
                                                            Case "language"
                                                                Dim sublang As New medianfo_subtitles
                                                                sublang.language = subsdetails.InnerText
                                                                newfilenfo.filedetails_subtitles.Add(sublang)
                                                        End Select
                                                    Next
                                            End Select
                                        Next
                                        newmovie.filedetails = newfilenfo
                                End Select
                            Next
                    End Select
                Next
                If thumbstring <> Nothing Then
                    Do Until thumbstring.ToLower.IndexOf("http") = -1
                        Try
                            Dim tempstring As String
                            tempstring = thumbstring.ToLower.Substring(thumbstring.ToLower.LastIndexOf("http"), (thumbstring.ToLower.LastIndexOf(".jpg") + 4) - thumbstring.ToLower.LastIndexOf("http"))
                            thumbstring = thumbstring.ToLower.Replace(tempstring, "")
                            newmovie.listthumbs.Add(tempstring)
                        Catch
                            Exit Do
                        End Try
                    Loop
                End If
                newmovie.fileinfo.fullpathandfilename = path
                newmovie.fileinfo.filename = IO.Path.GetFileName(path)
                newmovie.fileinfo.foldername = getlastfolder(path)
                newmovie.fileinfo.posterpath = getposterpath(path)
                newmovie.fileinfo.trailerpath = ""
                newmovie.fileinfo.fanartpath = getfanartpath(path)

                Return newmovie
            End If
        Catch ex As Exception

        End Try
    End Function
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
            Dim extensions(100) As String
            Dim extensioncount As Integer
            extensions(1) = ".avi"
            extensions(2) = ".xvid"
            extensions(3) = ".divx"
            extensions(4) = ".img"
            extensions(5) = ".mpg"
            extensions(6) = ".mpeg"
            extensions(7) = ".mov"
            extensions(8) = ".rm"
            extensions(9) = ".3gp"
            extensions(10) = ".m4v"
            extensions(11) = ".wmv"
            extensions(12) = ".asf"
            extensions(13) = ".mp4"
            extensions(14) = ".mkv"
            extensions(15) = ".nrg"
            extensions(16) = ".iso"
            extensions(17) = ".rmvb"
            extensions(18) = ".ogm"
            extensions(19) = ".bin"
            extensions(20) = ".ts"
            extensions(21) = ".vob"
            extensions(22) = ".m2ts"
            extensions(23) = ".rar"
            extensions(24) = ".flv"
            extensions(25) = ".dvr-ms"
            extensions(26) = "VIDEO_TS.IFO"
            extensioncount = 26
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
                    For j = 1 To extensioncount
                        Dim tempfilename As String = nfofilename
                        tempfilename = nfofilename.Replace(IO.Path.GetExtension(nfofilename), extensions(j))
                        Dim tempstring2 As String = fullnfopath.Replace(IO.Path.GetFileName(fullnfopath), tempfilename)
                        If IO.File.Exists(tempstring2) Then
                            Try
                                Dim seconds As Integer = 10
                                Dim myProcess As Process = New Process
                                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                                myProcess.StartInfo.CreateNoWindow = False
                                myProcess.StartInfo.FileName = applicationpath & "ffmpeg.exe"
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
    Public Function GetCRC32(ByVal sFileName As String) As String
        Dim oCRC As Vbaccelerator.Components.Algorithms.CRC32 = New Vbaccelerator.Components.Algorithms.CRC32()
        Dim oEnc As System.Text.UTF7Encoding = New System.Text.UTF7Encoding()
        Return (oCRC.GetCrc32(New System.IO.MemoryStream(oEnc.GetBytes(sFileName))))
    End Function
    Private Function getmovietags(ByVal text As String, ByVal movie As combolist, ByVal counter As Integer, Optional ByVal thumbpath As String = "")
        If text.IndexOf("<<smallimage>>") And thumbpath <> "" Then
            Dim filename As String = GetCRC32(movie.fullpathandfilename)
            If IO.File.Exists(IO.Path.Combine(applicationpath, "settings\postercache\" & filename & ".jpg")) Then
                Try
                    IO.File.Copy(IO.Path.Combine(applicationpath, "settings\postercache\" & filename & ".jpg"), IO.Path.Combine(thumbpath, filename & ".jpg"))
                Catch
                End Try
                Try
                    text = text.Replace("<<smallimage>>", "images/" & filename & ".jpg")
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                Dim tempstring As String = text
            Else
                If IO.File.Exists(getposterpath(movie.fullpathandfilename)) Then
                    Try
                        Dim bitmap As New Bitmap(getposterpath(movie.fullpathandfilename))
                        Dim bitmap2 As New Bitmap(bitmap)
                        bitmap.Dispose()
                        bitmap2 = resizeimage(bitmap2, 150, 200)
                        bitmap2.Save(IO.Path.Combine(thumbpath, filename & ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                    Catch
                    End Try
                Else
                    Try
                        Dim bitmap As New Bitmap(defaultposter)
                        Dim bitmap2 As New Bitmap(bitmap)
                        bitmap.Dispose()
                        bitmap2 = resizeimage(bitmap2, 150, 200)
                        bitmap2.Save(IO.Path.Combine(thumbpath, filename & ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg)
                    Catch
                    End Try
                End If
            End If
            '<img border="0" src="images/tt1175491.jpg" width="150" height="200">
            text = text.Replace("<<smallimage>>", "images/" & filename & ".jpg")
        End If


        If text.IndexOf("<<fullplot>>") <> -1 Or text.IndexOf("<<director>>") <> -1 Or text.IndexOf("<<writer>>") <> -1 Or text.IndexOf("<<moviegenre>>") <> -1 Then
            Dim newplotdeetails As fullmoviedetails
            newplotdeetails = loadfullmovienfo(movie.fullpathandfilename)
            If text.IndexOf("<<fullplot>>") <> -1 Then
                text = text.Replace("<<fullplot>>", newplotdeetails.fullmoviebody.plot)
            End If
            If text.IndexOf("<<director>>") <> -1 Then
                text = text.Replace("<<director>>", newplotdeetails.fullmoviebody.director)
            End If
            If text.IndexOf("<<writer>>") <> -1 Then
                text = text.Replace("<<writer>>", newplotdeetails.fullmoviebody.credits)
            End If
            If text.IndexOf("<<moviegenre>>") <> -1 Then
                text = text.Replace("<<moviegenre>>", newplotdeetails.fullmoviebody.genre)
            End If
        End If
        Try
            text = text.Replace("<<moviecount>>", basictvlist.Count.ToString)
        Catch
            text = text.Replace("<<moviecount>>", "0000")
        End Try
        Try
            text = text.Replace("<<counter>>", counter.ToString)
        Catch
        End Try
        If movie.title <> Nothing Then
            text = text.Replace("<<title>>", movie.title)
        Else
            text = text.Replace("<<title>>", "")
        End If
        If movie.title <> Nothing And movie.year <> Nothing Then
            text = text.Replace("<<movietitleandyear>>", movie.title & " (" & movie.year.ToString & ")")
        ElseIf movie.title <> Nothing And movie.year = Nothing Then
            text = text.Replace("<<movietitleandyear>>", movie.title & " (0000)")
        ElseIf movie.title = Nothing And movie.year <> Nothing Then
            text = text.Replace("<<movietitleandyear>>", "" & " (" & movie.year.ToString & ")")
        End If
        If movie.runtime <> Nothing Then
            text = text.Replace("<<runtime>>", movie.runtime)
        Else
            text = text.Replace("<<runtime>>", "")
        End If
        If movie.rating <> Nothing Then
            text = text.Replace("<<rating>>", movie.rating)
        Else
            text = text.Replace("<<rating>>", "")
        End If
        If movie.outline <> Nothing Then
            text = text.Replace("<<outline>>", movie.outline)
        Else
            text = text.Replace("<<outline>>", "")
        End If
        If movie.id <> Nothing Then
            text = text.Replace("<<imdb_url>>", userprefs.imdbmirror & "title/" & movie.id & "/")
        Else
            text = text.Replace("<<imdb_url>>", userprefs.imdbmirror)
        End If
        If movie.fullpathandfilename <> Nothing Then
            text = text.Replace("<<fullpathandfilename>>", movie.fullpathandfilename)
        Else
            text = text.Replace("<<fullpathandfilename>>", "")
        End If
        If movie.year <> Nothing Then
            text = text.Replace("<<movieyear>>", movie.year)
        Else
            text = text.Replace("<<movieyear>>", "")
        End If

        Return text
    End Function
    Dim fullhtmlstring As String
    Private Sub setuphtml(ByVal name As String)
        For Each temp In templatelist
            If temp.title = name Then
                Try
                    Dim fullstring As String
                    Dim cfg2 As IO.StreamReader = IO.File.OpenText(temp.path)
                    fullstring = cfg2.ReadToEnd
                    If fullstring.ToLower.IndexOf("<<mc html page>>") <> -1 And fullstring.ToLower.IndexOf("<</mc html page>>") <> -1 Then
                        fullhtmlstring = fullstring
                    Else
                        fullhtmlstring = Nothing
                    End If
                Catch ex As Exception
                    Dim t As Integer = 0
                End Try
                Exit For
            End If
        Next
        Call htmloutput()
    End Sub
    Private Sub htmloutput()
        If fullhtmlstring = Nothing Then Exit Sub
        If fullhtmlstring = "" Then Exit Sub

        Dim tempstring As String = ""
        Dim savepath As String = htmlfileoutput
        Dim cssbody As String
        Dim csspath As String
        Dim counter As Integer = 0
        savepath = htmlfileoutput
        If fullhtmlstring.IndexOf("<<css>>") <> -1 And fullhtmlstring.IndexOf("<</css>>") <> -1 Then
            tempstring = fullhtmlstring.Substring(fullhtmlstring.IndexOf("<<css>>") + 9, fullhtmlstring.IndexOf("<</css>>") - fullhtmlstring.IndexOf("<<css>>") - 9)
            If tempstring.IndexOf("<filename>") <> -1 And tempstring.IndexOf("</filename>") <> -1 Then
                Dim tempstring2 As String
                tempstring2 = tempstring.Substring(tempstring.IndexOf("<filename>") + 10, tempstring.IndexOf("</filename>") - tempstring.IndexOf("<filename>") - 10)
                csspath = savepath.Replace(IO.Path.GetFileName(savepath), tempstring2)
                cssbody = tempstring.Substring(tempstring.IndexOf("</filename>") + 13, tempstring.Length - tempstring.IndexOf("</filename>") - 13)
                Try
                    Dim objWriter2 As New System.IO.StreamWriter(csspath, False, Encoding.UTF8)
                    objWriter2.Write(cssbody)
                    objWriter2.Close()
                Catch ex As Exception

                Finally

                End Try
            End If

        End If
        tempstring = fullhtmlstring.Substring(fullhtmlstring.IndexOf("<<header>>") + 12, (fullhtmlstring.IndexOf("<</header>>") - fullhtmlstring.IndexOf("<<header>>")) - 12)
        'tempstring = getmovietags(tempstring, filteredlist(0), counter)

        Dim temphtml As String
        temphtml = "<html><head>" & tempstring & "</head>"
        tempstring = fullhtmlstring.Substring(fullhtmlstring.ToLower.IndexOf("<<body>>") + 8, fullhtmlstring.ToLower.IndexOf("<</body>>") - (fullhtmlstring.ToLower.IndexOf("<<body>>") + 8))
        temphtml = temphtml & "<body>"
        Dim pathstring As String = ""
        If tempstring.IndexOf("<<smallimage>>") <> -1 Then
            pathstring = savepath.Replace(IO.Path.GetFileName(savepath), "")
            pathstring = pathstring & "images\"
            Dim fso As New DirectoryInfo(pathstring)
            If fso.Exists = False Then
                IO.Directory.CreateDirectory(pathstring)
            End If
        End If
        Dim overallcancel As Boolean = False
        For Each movie In fullmovielist
            Dim tempint As Integer = fullmovielist.Count - (counter + 1)
            Console.SetCursorPosition(0, Console.CursorTop)
            Console.Write(tempint.ToString & " Movies Remaining         ")
            Try
                temphtml = temphtml & getmovietags(tempstring, movie, counter, pathstring)
            Catch
            End Try
            counter += 1
        Next
        If overallcancel = False Then
            temphtml = temphtml & "</body>"
            temphtml = temphtml & "</html>"

            Try
                Dim objWriter As New System.IO.StreamWriter(savepath, False, Encoding.UTF8)
                objWriter.Write(temphtml)
                objWriter.Close()
            Catch ex As Exception
            Finally

            End Try
        End If
    End Sub
    Private Structure htmltemplate
        Dim title As String
        Dim path As String
        Dim body As String
    End Structure
    Dim templatelist As New List(Of htmltemplate)
    Private Sub addhtmltemplates()
        templatelist.Clear()
        Dim folder As String = IO.Path.Combine(applicationpath, "html_templates\")
        Dim dir_info As New System.IO.DirectoryInfo(folder)
        Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles("*.txt", SearchOption.TopDirectoryOnly)
        For Each info In fs_infos
            Try
                Dim fullstring As String
                Dim cfg2 As IO.StreamReader = IO.File.OpenText(info.FullName)
                fullstring = cfg2.ReadToEnd
                If fullstring.ToLower.IndexOf("<<mc html page>>") <> -1 And fullstring.ToLower.IndexOf("<</mc html page>>") <> -1 Then
                    Dim tempstring As String = fullstring.Substring(fullstring.IndexOf("<title>") + 7, fullstring.IndexOf("</title>") - 7)
                    Dim template As htmltemplate
                    Dim add As Boolean = True
                    For Each temp In templatelist
                        If temp.title = tempstring Then
                            add = False
                            Exit For
                        End If
                    Next
                    If add = True Then
                        template.title = tempstring
                        template.path = info.FullName
                        template.body = fullstring
                        templatelist.Add(template)
                    End If
                End If
            Catch ex As Exception
                Dim t As Integer = 0
            End Try
        Next
    End Sub
    Private Function renameepisode(ByVal path As String, ByVal seasonno As String, ByVal episodeno As List(Of String), ByVal showtitle As String, ByVal episodetitle As String)
        Dim returnpath As String = "false"
        Dim extensioncount As Integer
        Dim extensions(100)
        extensions(1) = ".avi"
        extensions(2) = ".xvid"
        extensions(3) = ".divx"
        extensions(4) = ".img"
        extensions(5) = ".mpg"
        extensions(6) = ".mpeg"
        extensions(7) = ".mov"
        extensions(8) = ".rm"
        extensions(9) = ".3gp"
        extensions(10) = ".m4v"
        extensions(11) = ".wmv"
        extensions(12) = ".asf"
        extensions(13) = ".mp4"
        extensions(14) = ".mkv"
        extensions(15) = ".nrg"
        extensions(16) = ".iso"
        extensions(17) = ".rmvb"
        extensions(18) = ".ogm"
        extensions(19) = ".bin"
        extensions(20) = ".ts"
        extensions(21) = ".vob"
        extensions(22) = ".m2ts"
        extensions(23) = ".flv"
        extensions(24) = ".dvr-ms"
        extensioncount = 24
        Dim medianame As String = path.Replace(IO.Path.GetExtension(path), "")
        For f = 1 To extensioncount
            Dim actualname As String = medianame & extensions(f)
            If IO.File.Exists(actualname) Then
                Dim newfilename As String
                newfilename = ""
                If seasonno.Length = 1 Then
                    seasonno = "0" & seasonno
                End If
                For g = 0 To episodeno.Count - 1
                    If episodeno(g).Length = 1 Then
                        episodeno(g) = "0" & episodeno(g)
                    End If
                Next
                Select Case userprefs.tvrename
                    Case 0
                        'Show Title - S01E01 - Episode Title.ext
                        newfilename = showtitle & " - S" & seasonno
                        For Each ep In episodeno
                            newfilename = newfilename & "E" & ep
                        Next
                        newfilename = newfilename & " - " & episodetitle
                    Case 1
                        'S01E01 - Episode Title.ext
                        newfilename = "S" & seasonno
                        For Each ep In episodeno
                            newfilename = newfilename & "E" & ep
                        Next
                        newfilename = newfilename & " - " & episodetitle
                    Case 2
                        'Show Title - 1x01 - Episode Title.ext
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        newfilename = showtitle & " - " & seasonno
                        For Each ep In episodeno
                            newfilename = newfilename & "x" & ep
                        Next
                        newfilename = newfilename & " - " & episodetitle
                    Case 3
                        '1x01 - Episode Title.ext
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        newfilename = seasonno
                        For Each ep In episodeno
                            newfilename = newfilename & "x" & ep
                        Next
                        newfilename = newfilename & " - " & episodetitle
                    Case 4
                        'Show Title - 101 - Episode Title.ext
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        newfilename = showtitle & " - " & seasonno
                        For g = 0 To episodeno.Count - 1
                            If g = 0 Then
                                newfilename = newfilename & episodeno(g)
                            Else
                                newfilename = newfilename & "x" & episodeno(g)
                            End If
                        Next
                        newfilename = newfilename & " - " & episodetitle
                    Case 5
                        '101 - Episode Title.ext
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        If seasonno.Length > 1 And seasonno.Substring(0, 1) = "0" Then
                            seasonno = seasonno.Substring(1, seasonno.Length - 1)
                        End If
                        newfilename = seasonno
                        For g = 0 To episodeno.Count - 1
                            If g = 0 Then
                                newfilename = newfilename & episodeno(g)
                            Else
                                newfilename = newfilename & "x" & episodeno(g)
                            End If
                        Next
                        newfilename = newfilename & " - " & episodetitle
                End Select
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
                Dim done As String = ""
                Dim temppath As String = path

                listtorename.Add(actualname)

                Dim di As DirectoryInfo = New DirectoryInfo(path.Replace(IO.Path.GetFileName(path), ""))
                Dim filenama As String = IO.Path.GetFileNameWithoutExtension(path)
                Dim fils As FileInfo() = di.GetFiles(filenama & ".*")
                For Each fiNext In fils
                    If Not listtorename.Contains(fiNext.FullName) Then
                        listtorename.Add(fiNext.FullName)
                    End If
                Next

                temppath = temppath.Replace(IO.Path.GetExtension(temppath), ".nfo")
                If IO.File.Exists(temppath) Then
                    If Not listtorename.Contains(temppath) Then
                        listtorename.Add(temppath)
                    End If
                End If
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


                For Each items In listtorename
                    Dim newname As String = items.Replace(IO.Path.GetFileName(items), newfilename) & IO.Path.GetExtension(items)
                    newname = newname.Replace("..", ".")
                    done = newname.Replace(IO.Path.GetExtension(newname), ".nfo")
                    Try
                        Dim fi As New FileInfo(items)
                        If Not IO.File.Exists(newname) Then
                            fi.MoveTo(newname)
                        End If
                    Catch ex As Exception
                        done = path
                    End Try
                Next
                returnpath = done
                Exit For
            End If
        Next
        Return returnpath
    End Function

    Dim newepisodetoadd As New basicepisodenfo
    Private Sub episodescraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim errorcounter As Integer = 0
       
        newepisodelist.Clear()
        Dim newtvfolders As New List(Of String)
        Dim progress As Integer
        progress = 0

        Dim dirpath As String
        Dim moviepattern As String

       


      


        Console.WriteLine("Starting TV Folder Scan")

        Dim extensions(100) As String
        Dim extensioncount As Integer
        extensions(1) = "*.avi"
        extensions(2) = "*.xvid"
        extensions(3) = "*.divx"
        extensions(4) = "*.img"
        extensions(5) = "*.mpg"
        extensions(6) = "*.mpeg"
        extensions(7) = "*.mov"
        extensions(8) = "*.rm"
        extensions(9) = "*.3gp"
        extensions(10) = "*.m4v"
        extensions(11) = "*.wmv"
        extensions(12) = "*.asf"
        extensions(13) = "*.mp4"
        extensions(14) = "*.mkv"
        extensions(15) = "*.nrg"
        extensions(16) = "*.iso"
        extensions(17) = "*.rmvb"
        extensions(18) = "*.ogm"
        extensions(19) = "*.bin"
        extensions(20) = "*.ts"
        extensions(21) = "*.vob"
        extensions(22) = "*.m2ts"
        extensions(23) = "*.rar"
        extensions(24) = "*.flv"
        extensions(25) = "VIDEO_TS.IFO"

        extensioncount = 25

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
                    Dim newlist As List(Of String)

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
        Dim mediacounter As Integer = newepisodelist.Count
        For g = 0 To newtvfolders.Count - 1
            For f = 1 To extensioncount
                moviepattern = extensions(f)
                dirpath = newtvfolders(g)
                Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                findnewepisodes(dirpath, moviepattern)
            Next f
            tempint = newepisodelist.Count - mediacounter
            If tempint > 0 Then
                Console.WriteLine(tempint.ToString & " New episodes found in directory:- " & dirpath)
            End If
            mediacounter = newepisodelist.Count
        Next g

        If newepisodelist.Count <= 0 Then
            Console.WriteLine(tempint.ToString & "No new episodes found, exiting scraper" & dirpath)
            Exit Sub
        End If

        Dim S As String = ""
        For Each newepisode In newepisodelist
            S = ""
            newepisodetoadd.episodeno = ""
            newepisodetoadd.episodepath = ""
            'newepisodetoadd.status = ""
            newepisodetoadd.imdbid = ""
            newepisodetoadd.playcount = ""
            newepisodetoadd.rating = ""
            newepisodetoadd.seasonno = ""
            newepisodetoadd.title = ""
            newepisodetoadd.tvdbid = ""

            Dim episode As New episodeinfo

            For Each Regexs In tvregex
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
                            Console.WriteLine("Season and Episode information found for : " & newepisode.episodepath & newepisode.seasonno & "x" & newepisode.episodeno)
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
        For Each eps In newepisodelist

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
                        tvdbid = Shows.tvdbid
                        imdbid = Shows.imdbid
                        realshowpath = Shows.fullpath
                        actorsource = Shows.episodeactorsource
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
                    Dim episodescraper As New TVDB.tvdbscraper
                    If sortorder = "" Then sortorder = "default"
                    Dim tempsortorder As String = sortorder
                    If language = "" Then language = "en"
                    If actorsource = "" Then actorsource = "tvdb"
                    If tvdbid <> "" Then
                        Dim episodeurl As String = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                        If Not UrlIsValid(episodeurl) Then
                            If sortorder.ToLower = "dvd" Then
                                tempsortorder = "default"
                                Console.WriteLine("This episode could not be found on TVDB using DVD sort order")
                                Console.WriteLine("Attempting to find using default sort order")
                                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                            End If
                        End If

                        If UrlIsValid(episodeurl) Then


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
                                                            Dim newactor As New movieactors
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
                                                    Dim scraperfunction As New imdb.Classimdbscraper
                                                    Dim actorlist As String = ""
                                                    actorlist = scraperfunction.getimdbactors(userprefs.imdbmirror, tvtempstring, , userprefs.maxactors)
                                                    Dim tempactorlist As New List(Of movieactors)
                                                    Dim thumbstring As New XmlDocument
                                                    Dim thisresult As XmlNode = Nothing
                                                    Try
                                                        thumbstring.LoadXml(actorlist)
                                                        thisresult = Nothing
                                                        Dim countactors As Integer = 0
                                                        For Each thisresult In thumbstring("actorlist")
                                                            Select Case thisresult.Name
                                                                Case "actor"
                                                                    If countactors >= userprefs.maxactors Then
                                                                        Exit For
                                                                    End If
                                                                    countactors += 1
                                                                    Dim newactor As New movieactors
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
                                                                                    If userprefs.actorseasy = True And detail.InnerText <> "" Then
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
                                                                                                Dim req As HttpWebRequest = req.Create(thumburl)
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
                                                                                    If userprefs.actorsave = True And detail.InnerText <> "" And userprefs.actorseasy = False Then
                                                                                        Dim workingpath As String = ""
                                                                                        Dim networkpath As String = userprefs.actorsavepath
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
                                                                                                Dim req As HttpWebRequest = req.Create(thumburl)
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
                                                                                            newactor.actorthumb = IO.Path.Combine(userprefs.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                                                            If userprefs.actornetworkpath.IndexOf("/") <> -1 Then
                                                                                                newactor.actorthumb = userprefs.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                                                            Else
                                                                                                newactor.actorthumb = userprefs.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
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
                                                        While tempactorlist.Count > userprefs.maxactors
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

                            If userprefs.enablehdtags = True Then
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
                                    Dim newep As New basicepisodenfo
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
            If userprefs.enabletvhdtags = True Then
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

            Dim actorstosave As Integer = listofepisodes(0).listactors.Count
            If actorstosave > userprefs.maxactors Then actorstosave = userprefs.maxactors
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
                    If userprefs.enabletvhdtags = True Then
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


        If userprefs.autorenameepisodes = True Then
            For Each show In basictvlist
                If alleps(0).episodepath.IndexOf(show.fullpath.Replace("\tvshow.nfo", "")) <> -1 Then
                    Dim eps As New List(Of String)
                    eps.Clear()
                    For Each ep In alleps
                        eps.Add(ep.episodeno)
                    Next
                    Dim tempspath As String = renameepisode(path, alleps(0).seasonno, eps, show.title, alleps(0).title)
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
                    Dim newwp As New basicepisodenfo
                    newwp.episodeno = ep.episodeno
                    newwp.episodepath = path
                    newwp.playcount = "0"
                    newwp.rating = ep.rating
                    newwp.seasonno = ep.seasonno
                    newwp.title = ep.title
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
                        Dim req As HttpWebRequest = req.Create(url)
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
        If Not IO.File.Exists(ext) And userprefs.autoepisodescreenshot = True Then
            Call screenshot(ext)
        End If
    End Sub
    Private Function UrlIsValid(ByVal url As String) As Boolean
        Dim is_valid As Boolean = False
        If url.ToLower().StartsWith("www.") Then url = _
            "http://" & url

        Dim web_response As HttpWebResponse = Nothing

        Try
            Dim web_request As HttpWebRequest = HttpWebRequest.Create(url)
            web_request.Timeout = 5000
            web_response = DirectCast(web_request.GetResponse(), HttpWebResponse)
            Return True
        Catch ex As Exception
            Return False
        Finally
            If Not (web_response Is Nothing) Then _
                web_response.Close()
        End Try
    End Function
    Private Sub findnewepisodes(ByVal path As String, ByVal pattern As String)
        Dim episode As New List(Of basicepisodenfo)
        Dim propfile As Boolean = False
        Dim allok As Boolean = False
        Dim dir_info As New System.IO.DirectoryInfo(path)
        Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(pattern, SearchOption.TopDirectoryOnly)
        Dim counter As Integer = 1
        Dim counter2 As Integer = 1
        For Each fs_info As System.IO.FileInfo In fs_infos
            Try
                Dim filename As String = IO.Path.Combine(path, fs_info.Name)
                Dim filename2 As String = filename.Replace(IO.Path.GetExtension(filename), ".nfo")
                If Not IO.File.Exists(filename2) Then
                    Dim add As Boolean = True
                    If pattern = "*.vob" Then 'If a vob file is detected, check that it is not part of a dvd file structure
                        Dim name As String = filename2
                        name = name.Replace(IO.Path.GetFileName(name), "VIDEO_TS.IFO")
                        If IO.File.Exists(name) Then
                            add = False
                        End If
                    End If
                    If pattern = "*.rar" Then
                        Dim tempmovie As String
                        Dim tempint2 As Integer
                        Dim tempmovie2 As String = fs_info.FullName
                        If IO.Path.GetExtension(tempmovie2).ToLower = ".rar" Then
                            If IO.File.Exists(tempmovie2) = True Then
                                If IO.File.Exists(tempmovie) = False Then
                                    Dim rarname As String = tempmovie2
                                    Dim SizeOfFile As Integer
                                    tempint2 = Convert.ToInt32(userprefs.rarsize) * 1048576
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
                        newepisodelist.Add(newep)
                    End If
                End If
            Catch ex As Exception

            End Try

        Next fs_info

        fs_infos = Nothing
    End Sub
    Private Sub loadtvcache()
        totalepisodecount = 0
        totaltvshowcount = 0
       
        basictvlist.Clear()

        Dim tvlist As New XmlDocument
        tvlist.Load(workingprofile.tvcache)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In tvlist("tv_cache")
            Select Case thisresult.Name
                Case "tvshow"
                    Dim newtvshow As New basictvshownfo
                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            Case "title"
                                Dim tempstring As String = ""
                                tempstring = detail.InnerText
                                If tempstring.ToLower.IndexOf("the ") = 0 Then
                                    tempstring = tempstring.Substring(4, tempstring.Length - 4)
                                    tempstring = tempstring & ", The"
                                End If
                                newtvshow.title = tempstring
                            Case "fullpathandfilename"
                                newtvshow.fullpath = detail.InnerText
                            Case "genre"
                                newtvshow.genre = detail.InnerText
                            Case "locked"
                                newtvshow.locked = detail.InnerText
                            Case "imdbid"
                                newtvshow.imdbid = detail.InnerText
                            Case "tvdbid"
                                newtvshow.tvdbid = detail.InnerText
                            Case "rating"
                                newtvshow.rating = detail.InnerText
                            Case "year"
                                newtvshow.year = detail.InnerText
                            Case "language"
                                newtvshow.language = detail.InnerText
                            Case "sortorder"
                                newtvshow.sortorder = detail.InnerText
                            Case "episodeactorsource"
                                newtvshow.episodeactorsource = detail.InnerText
                            Case "status"
                                newtvshow.status = detail.InnerText
                            Case "episode"
                                Dim newepisode As New basicepisodenfo
                                Dim episodenew As XmlNode = Nothing
                                For Each episodenew In detail.ChildNodes
                                    Select Case episodenew.Name
                                        Case "title"
                                            newepisode.title = episodenew.InnerText
                                        Case "episodepath"
                                            newepisode.episodepath = episodenew.InnerText
                                        Case "seasonno"
                                            newepisode.seasonno = episodenew.InnerText
                                        Case "episodeno"
                                            newepisode.episodeno = episodenew.InnerText
                                            'Case "status"
                                            '    newepisode.status = episodenew.InnerText
                                        Case "playcount"
                                            newepisode.playcount = episodenew.InnerText
                                        Case "rating"
                                            newepisode.rating = episodenew.InnerText
                                        Case "tvdbid"
                                            newepisode.tvdbid = episodenew.InnerText
                                    End Select
                                Next
                                newtvshow.allepisodes.Add(newepisode)
                        End Select
                    Next
                    basictvlist.Add(newtvshow)
            End Select
        Next
    End Sub
    Private Sub savetvcache()
        Dim fullpath As String = workingprofile.tvcache
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
        root = doc.CreateElement("tv_cache")

        Dim childchild As XmlElement
        For Each item In basictvlist
            child = doc.CreateElement("tvshow")
            childchild = doc.CreateElement("title")
            childchild.InnerText = item.title
            child.AppendChild(childchild)
            childchild = doc.CreateElement("fullpathandfilename")
            childchild.InnerText = item.fullpath
            child.AppendChild(childchild)
            childchild = doc.CreateElement("genre")
            childchild.InnerText = item.genre
            child.AppendChild(childchild)
            childchild = doc.CreateElement("imdbid")
            childchild.InnerText = item.imdbid
            child.AppendChild(childchild)
            childchild = doc.CreateElement("tvdbid")
            childchild.InnerText = item.tvdbid
            child.AppendChild(childchild)
            childchild = doc.CreateElement("rating")
            childchild.InnerText = item.rating
            child.AppendChild(childchild)
            child.AppendChild(childchild)
            childchild = doc.CreateElement("year")
            childchild.InnerText = item.year
            child.AppendChild(childchild)
            childchild = doc.CreateElement("language")
            childchild.InnerText = item.language
            child.AppendChild(childchild)
            childchild = doc.CreateElement("status")
            childchild.InnerText = item.status
            child.AppendChild(childchild)
            childchild = doc.CreateElement("sortorder")
            childchild.InnerText = item.sortorder
            child.AppendChild(childchild)
            childchild = doc.CreateElement("episodeactorsource")
            childchild.InnerText = item.episodeactorsource
            child.AppendChild(childchild)
            childchild = doc.CreateElement("locked")
            childchild.InnerText = item.locked.ToString.ToLower
            child.AppendChild(childchild)
            For Each episode In item.allepisodes
                Dim episodechild As XmlElement
                childchild = doc.CreateElement("episode")
                episodechild = doc.CreateElement("title")
                episodechild.InnerText = episode.title
                childchild.AppendChild(episodechild)

                episodechild = doc.CreateElement("episodepath")
                episodechild.InnerText = episode.episodepath
                childchild.AppendChild(episodechild)

                episodechild = doc.CreateElement("seasonno")
                episodechild.InnerText = episode.seasonno
                childchild.AppendChild(episodechild)

                episodechild = doc.CreateElement("episodeno")
                episodechild.InnerText = episode.episodeno
                childchild.AppendChild(episodechild)

                'episodechild = doc.CreateElement("status")
                'episodechild.InnerText = episode.status
                'childchild.AppendChild(episodechild)

                episodechild = doc.CreateElement("playcount")
                episodechild.InnerText = episode.playcount
                childchild.AppendChild(episodechild)

                episodechild = doc.CreateElement("rating")
                episodechild.InnerText = episode.rating
                childchild.AppendChild(episodechild)

                episodechild = doc.CreateElement("tvdbid")
                episodechild.InnerText = episode.tvdbid
                childchild.AppendChild(episodechild)
                child.AppendChild(childchild)
            Next
            root.AppendChild(child)
        Next

        doc.AppendChild(root)
        Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        doc.WriteTo(output)
        output.Close()

    End Sub
    Private Sub loadregex()
        Dim tempstring As String
        tempstring = workingprofile.regexlist
        tvregex.Clear()
        Dim path As String = tempstring
        If IO.File.Exists(path) Then
            Try
                Dim regexlist As New XmlDocument
                regexlist.Load(path)
                If regexlist.DocumentElement.Name = "regexlist" Then
                    For Each thisresult In regexlist("regexlist")
                        Select Case thisresult.Name
                            Case "tvregex"
                                tvregex.Add(thisresult.innertext)
                        End Select
                    Next
                End If
            Catch
            End Try
        End If
    End Sub
    Private Sub loadmoviecache()
        fullmovielist.Clear()
        Dim movielist As New XmlDocument
        Dim objReader As New System.IO.StreamReader(workingprofile.moviecache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close()



        movielist.LoadXml(tempstring)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In movielist("movie_cache")
            Select Case thisresult.Name
                Case "movie"
                    Dim newmovie As New combolist
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
        Dim fullpath As String = workingprofile.moviecache
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
        For Each movie In fullmovielist
            child = doc.CreateElement("movie")
            childchild = doc.CreateElement("filedate")
            childchild.InnerText = movie.filedate
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
            If movie.movieset <> Nothing Then
                If movie.movieset <> "" Then
                    childchild = doc.CreateElement("set")
                    childchild.InnerText = movie.movieset
                    child.AppendChild(childchild)
                Else
                    childchild = doc.CreateElement("set")
                    childchild.InnerText = "None"
                    child.AppendChild(childchild)
                End If
            Else
                childchild = doc.CreateElement("set")
                childchild.InnerText = "None"
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
            If movie.sortorder = Nothing Then
                movie.sortorder = movie.title
            End If
            If movie.sortorder = "" Then
                movie.sortorder = movie.title
            End If
            childchild = doc.CreateElement("outline")
            childchild.InnerText = movie.outline
            child.AppendChild(childchild)
            childchild = doc.CreateElement("sortorder")
            childchild.InnerText = movie.sortorder
            child.AppendChild(childchild)
            childchild = doc.CreateElement("titleandyear")
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


    Public Sub loadconfig()

        userprefs.moviesets.Clear()
        userprefs.moviesets.Add("None")
        moviefolders.Clear()
        tvfolders.Clear()
        tvrootfolders.Clear()
        userprefs.tableview.Clear()
        Dim tempstring As String = workingprofile.config
        If Not IO.File.Exists(workingprofile.config) Then
            Exit Sub
        End If

        Dim prefs As New XmlDocument
        Try
            prefs.Load(workingprofile.config)
        Catch ex As Exception
            MsgBox("Error : pr24")
        End Try

        Dim thisresult As XmlNode = Nothing

        For Each thisresult In prefs("xbmc_media_companion_config_v1.0")
            'Try
            Select Case thisresult.Name
                Case "moviesets"
                    Dim thisset As XmlNode = Nothing
                    For Each thisset In thisresult.ChildNodes
                        Select Case thisset.Name
                            Case "set"
                                userprefs.moviesets.Add(thisset.InnerText)
                        End Select
                    Next
                Case "table"
                    Dim thistable As XmlNode = Nothing
                    For Each thistable In thisresult.ChildNodes
                        Select Case thistable.Name
                            Case "tab"
                                userprefs.tableview.Add(thistable.InnerText)
                            Case "sort"
                                userprefs.tablesortorder = thistable.InnerText
                        End Select
                    Next
                Case "seasonall"
                    userprefs.seasonall = thisresult.InnerText
                Case "splitcontainer1"
                    userprefs.splt1 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer2"
                    userprefs.splt2 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer3"
                    userprefs.splt3 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer4"
                    userprefs.splt4 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer5"
                    userprefs.splt5 = Convert.ToInt32(thisresult.InnerText)
                Case "maximised"
                    If thisresult.InnerText = "true" Then
                        userprefs.maximised = True
                    Else
                        userprefs.maximised = False
                    End If
                Case "locx"
                    userprefs.locx = Convert.ToInt32(thisresult.InnerText)
                Case "locy"
                    userprefs.locy = Convert.ToInt32(thisresult.InnerText)
                Case "nfofolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    moviefolders.Add(decodestring)
                Case "offlinefolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    userprefs.offlinefolders.Add(decodestring)
                Case "tvfolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    tvfolders.Add(decodestring)
                Case "tvrootfolder"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    tvrootfolders.Add(decodestring)
                Case "gettrailer"
                    If thisresult.InnerXml = "true" Then
                        userprefs.gettrailer = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.gettrailer = False
                    End If
                Case "tvshowautoquick"
                    If thisresult.InnerXml = "true" Then
                        userprefs.tvshowautoquick = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.tvshowautoquick = False
                    End If
                Case "keepfoldername"
                    If thisresult.InnerXml = "true" Then
                        userprefs.keepfoldername = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.keepfoldername = False
                    End If

                Case "startupcache"
                    If thisresult.InnerXml = "true" Then
                        userprefs.startupcache = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.startupcache = False
                    End If

                Case "ignoretrailers"
                    If thisresult.InnerXml = "true" Then
                        userprefs.ignoretrailers = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.ignoretrailers = False
                    End If

                Case "ignoreactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        userprefs.ignoreactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.ignoreactorthumbs = False
                    End If

                Case "font"
                    If thisresult.InnerXml <> Nothing Then
                        If thisresult.InnerXml <> "" Then
                            userprefs.font = thisresult.InnerXml
                        End If
                    End If

                Case "maxactors"
                    userprefs.maxactors = Convert.ToInt32(thisresult.InnerXml)

                Case "maxmoviegenre"
                    userprefs.maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)

                Case "enablehdtags"
                    If thisresult.InnerXml = "true" Then
                        userprefs.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.enablehdtags = False
                    End If

                Case "hdtvtags"
                    If thisresult.InnerXml = "true" Then
                        userprefs.enabletvhdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.enabletvhdtags = False
                    End If

                Case "renamenfofiles"
                    If thisresult.InnerXml = "true" Then
                        userprefs.renamenfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.renamenfofiles = False
                    End If

                Case "checkinfofiles"
                    If thisresult.InnerXml = "true" Then
                        userprefs.checkinfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.checkinfofiles = False
                    End If

                Case "disablelogfiles"
                    If thisresult.InnerXml = "true" Then
                        userprefs.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.disablelogfiles = False
                    End If

                Case "fanartnotstacked"
                    If thisresult.InnerXml = "true" Then
                        userprefs.fanartnotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.fanartnotstacked = False
                    End If

                Case "posternotstacked"
                    If thisresult.InnerXml = "true" Then
                        userprefs.posternotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.posternotstacked = False
                    End If

                Case "downloadfanart"
                    If thisresult.InnerXml = "true" Then
                        userprefs.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.savefanart = False
                    End If

                Case "scrapemovieposters"
                    If thisresult.InnerXml = "true" Then
                        userprefs.scrapemovieposters = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.scrapemovieposters = False
                    End If

                Case "usefanart"
                    If thisresult.InnerXml = "true" Then
                        userprefs.usefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.usefanart = False
                    End If

                Case "dontdisplayposter"
                    If thisresult.InnerXml = "true" Then
                        userprefs.dontdisplayposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.dontdisplayposter = False
                    End If

                Case "rarsize"
                    userprefs.rarsize = Convert.ToInt32(thisresult.InnerXml)

                Case "actorsave"
                    If thisresult.InnerXml = "true" Then
                        userprefs.actorsave = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.actorsave = False
                    End If

                Case "actorseasy"
                    If thisresult.InnerXml = "true" Then
                        userprefs.actorseasy = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.actorseasy = False
                    End If

                Case "copytvactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        userprefs.copytvactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.copytvactorthumbs = False
                    End If

                Case "actorsavepath"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    userprefs.actorsavepath = decodestring

                Case "actornetworkpath"
                    Dim decodestring As String = decxmlchars(thisresult.InnerText)
                    userprefs.actornetworkpath = decodestring

                Case "resizefanart"
                    userprefs.resizefanart = Convert.ToInt32(thisresult.InnerXml)

                Case "overwritethumbs"
                    If thisresult.InnerXml = "true" Then
                        userprefs.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.overwritethumbs = False
                    End If

                Case "defaulttvthumb"
                    userprefs.defaulttvthumb = thisresult.InnerXml

                Case "imdbmirror"
                    userprefs.imdbmirror = thisresult.InnerXml

                Case "moviethumbpriority"
                    ReDim userprefs.moviethumbpriority(3)
                    userprefs.moviethumbpriority = thisresult.InnerXml.Split("|")

                Case "certificatepriority"
                    ReDim userprefs.certificatepriority(33)
                    userprefs.certificatepriority = thisresult.InnerXml.Split("|")

                Case "backgroundcolour"
                    userprefs.backgroundcolour = thisresult.InnerXml

                Case "forgroundcolour"
                    userprefs.forgroundcolour = thisresult.InnerXml

                Case "remembersize"
                    If thisresult.InnerXml = "true" Then
                        userprefs.remembersize = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.remembersize = False
                    End If

                Case "formheight"
                    userprefs.formheight = Convert.ToInt32(thisresult.InnerXml)

                Case "formwidth"
                    userprefs.formwidth = Convert.ToInt32(thisresult.InnerXml)
                Case "videoplaybackmode"
                    userprefs.videoplaybackmode = Convert.ToInt32(thisresult.InnerXml)

                Case "usefoldernames"
                    If thisresult.InnerXml = "true" Then
                        userprefs.usefoldernames = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.usefoldernames = False
                    End If

                Case "createfolderjpg"
                    If thisresult.InnerXml = "true" Then
                        userprefs.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.createfolderjpg = False
                    End If

                Case "basicsavemode"
                    If thisresult.InnerXml = "true" Then
                        userprefs.basicsavemode = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.basicsavemode = False
                    End If

                Case "startupdisplaynamemode"
                    userprefs.startupdisplaynamemode = Convert.ToInt32(thisresult.InnerXml)

                Case "namemode"
                    userprefs.namemode = thisresult.InnerXml

                Case "tvdblanguage"
                    Dim partone() As String
                    partone = thisresult.InnerXml.Split("|")
                    For f = 0 To 1
                        If partone(0).Length = 2 Then
                            userprefs.tvdblanguagecode = partone(0)
                            userprefs.tvdblanguage = partone(1)
                            Exit For
                        Else
                            userprefs.tvdblanguagecode = partone(1)
                            userprefs.tvdblanguage = partone(0)
                        End If
                    Next

                Case "tvdbmode"
                    userprefs.sortorder = thisresult.InnerXml
                Case "tvdbactorscrape"
                    userprefs.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "usetransparency"
                    If thisresult.InnerXml = "true" Then
                        userprefs.usetransparency = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.usetransparency = False
                    End If

                Case "transparencyvalue"
                    userprefs.transparencyvalue = Convert.ToInt32(thisresult.InnerXml)

                Case "downloadtvfanart"
                    If thisresult.InnerXml = "true" Then
                        userprefs.tvfanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.tvfanart = False
                    End If

                Case "roundminutes"
                    If thisresult.InnerXml = "true" Then
                        userprefs.roundminutes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.roundminutes = False
                    End If


                Case "downloadtvposter"
                    If thisresult.InnerXml = "true" Then
                        userprefs.tvposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.tvposter = False
                    End If

                Case "downloadtvseasonthumbs"
                    If thisresult.InnerXml = "true" Then
                        userprefs.downloadtvseasonthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.downloadtvseasonthumbs = False
                    End If

                Case "maximumthumbs"
                    userprefs.maximumthumbs = Convert.ToInt32(thisresult.InnerXml)

                Case "startupmode"
                    userprefs.startupmode = Convert.ToInt32(thisresult.InnerXml)

                Case "hdtags"
                    If thisresult.InnerXml = "true" Then
                        userprefs.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.enablehdtags = False
                    End If

                Case "disablelogs"
                    If thisresult.InnerXml = "true" Then
                        userprefs.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.disablelogfiles = False
                    End If

                Case "disabletvlogs"
                    If thisresult.InnerXml = "true" Then
                        userprefs.disabletvlogs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.disabletvlogs = False
                    End If

                Case "overwritethumb"
                    If thisresult.InnerXml = "true" Then
                        userprefs.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.overwritethumbs = False
                    End If

                Case "folderjpg"
                    If thisresult.InnerXml = "true" Then
                        userprefs.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.createfolderjpg = False
                    End If

                Case "savefanart"
                    If thisresult.InnerXml = "true" Then
                        userprefs.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.savefanart = False
                    End If

                Case "postertype"
                    userprefs.postertype = thisresult.InnerXml

                Case "tvactorscrape"
                    userprefs.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "videomode"
                    userprefs.videomode = Convert.ToInt32(thisresult.InnerXml)

                Case "selectedvideoplayer"
                    userprefs.selectedvideoplayer = thisresult.InnerXml

                Case "maximagecount"
                    userprefs.maximagecount = Convert.ToInt32(thisresult.InnerXml)

                Case "lastpath"
                    userprefs.lastpath = thisresult.InnerXml

                Case "moviescraper"
                    userprefs.moviescraper = thisresult.InnerXml

                Case "nfoposterscraper"
                    userprefs.nfoposterscraper = thisresult.InnerXml

                Case "alwaysuseimdbid"
                    If thisresult.InnerXml = "true" Then
                        userprefs.alwaysuseimdbid = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.alwaysuseimdbid = False
                    End If

                Case "externalbrowser"
                    If thisresult.InnerXml = "true" Then
                        userprefs.externalbrowser = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.externalbrowser = False
                    End If
                Case "tvrename"
                    userprefs.tvrename = Convert.ToInt32(thisresult.InnerText)
                Case "tvshowrebuildlog"
                    If thisresult.InnerXml = "true" Then
                        userprefs.tvshowrebuildlog = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.tvshowrebuildlog = False
                    End If
                Case "moviesortorder"
                    userprefs.moviesortorder = Convert.ToByte(thisresult.InnerText)
                Case "moviedefaultlist"
                    userprefs.moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                Case "startuptab"
                    userprefs.startuptab = Convert.ToByte(thisresult.InnerText)
                Case "autoepisodescreenshot"
                    If thisresult.InnerXml = "true" Then
                        userprefs.autoepisodescreenshot = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.autoepisodescreenshot = False
                    End If
                Case "autorenameepisodes"
                    If thisresult.InnerXml = "true" Then
                        userprefs.autorenameepisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        userprefs.autorenameepisodes = False
                    End If
            End Select
            'Catch
            '    'MsgBox("Error : pr278")
            'End Try
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
        profile_structure.profilelist.Clear()
        Dim profilepath As String = IO.Path.Combine(applicationpath, "settings")
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
                                profile_structure.defaultprofile = thisresult.innertext
                            Case "startup"
                                profile_structure.startupprofile = thisresult.innertext
                            Case "profiledetails"
                                Dim currentprofile As New listofprofiles
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
                                profile_structure.profilelist.Add(currentprofile)
                        End Select
                    Next
                End If
            Catch

            End Try
        End If
    End Sub
    Public Sub setuppreferences()
        userprefs.autoepisodescreenshot = True
        userprefs.autorenameepisodes = False
        userprefs.tvshowautoquick = False
        userprefs.actorseasy = True
        userprefs.copytvactorthumbs = True
        userprefs.startuptab = 0
        userprefs.font = "Times New Roman, 9pt"
        userprefs.moviedefaultlist = 0
        userprefs.moviesortorder = 0
        userprefs.enabletvhdtags = True
        userprefs.tvshowrebuildlog = False
        userprefs.seasonall = "none"
        userprefs.tvrename = 0
        userprefs.externalbrowser = False
        userprefs.videoplaybackmode = "1"
        userprefs.backgroundcolour = "Silver"
        userprefs.forgroundcolour = "#D3D9DC"
        userprefs.formheight = "600"
        userprefs.formwidth = "800"
        userprefs.fanartnotstacked = False
        userprefs.posternotstacked = False
        userprefs.disablelogfiles = False
        userprefs.startupcache = True
        userprefs.rarsize = True = "8"
        userprefs.ignoreactorthumbs = False
        userprefs.actorsave = False
        userprefs.actorsavepath = ""
        userprefs.actornetworkpath = ""
        userprefs.tvfanart = True
        userprefs.tvposter = True
        userprefs.postertype = "poster"
        userprefs.downloadtvseasonthumbs = True
        userprefs.usefanart = True
        userprefs.ignoretrailers = False
        userprefs.keepfoldername = False
        userprefs.enablehdtags = True
        userprefs.renamenfofiles = False
        userprefs.checkinfofiles = True
        userprefs.savefanart = True
        userprefs.scrapemovieposters = True
        userprefs.dontdisplayposter = False
        userprefs.resizefanart = 1
        userprefs.overwritethumbs = False
        userprefs.startupmode = 1
        userprefs.maxactors = 9999
        userprefs.maxmoviegenre = 99
        userprefs.usetransparency = False
        userprefs.transparencyvalue = 255
        userprefs.defaulttvthumb = "poster"
        userprefs.imdbmirror = "http://www.imdb.com/"
        userprefs.usefoldernames = False
        userprefs.createfolderjpg = False
        userprefs.basicsavemode = False               'movie.nfo, movie.tbn, fanart.jpg
        userprefs.namemode = "1"
        userprefs.tvdblanguage = "English"
        userprefs.tvdblanguagecode = "en"
        userprefs.sortorder = "default"
        userprefs.tvdbactorscrape = 0
        userprefs.maximumthumbs = 10
        userprefs.gettrailer = False
        ReDim userprefs.certificatepriority(33)
        userprefs.certificatepriority(0) = "MPAA"
        userprefs.certificatepriority(1) = "UK"
        userprefs.certificatepriority(2) = "USA"
        userprefs.certificatepriority(3) = "Ireland"
        userprefs.certificatepriority(4) = "Australia"
        userprefs.certificatepriority(5) = "New Zealand"
        userprefs.certificatepriority(6) = "Norway"
        userprefs.certificatepriority(7) = "Singapore"
        userprefs.certificatepriority(8) = "South Korea"
        userprefs.certificatepriority(9) = "Philippines"
        userprefs.certificatepriority(10) = "Brazil"
        userprefs.certificatepriority(11) = "Netherlands"
        userprefs.certificatepriority(12) = "Malaysia"
        userprefs.certificatepriority(13) = "Argentina"
        userprefs.certificatepriority(14) = "Iceland"
        userprefs.certificatepriority(15) = "Canada (Quebec)"
        userprefs.certificatepriority(16) = "Canada (British Columbia/Ontario)"
        userprefs.certificatepriority(17) = "Canada (Alberta/Manitoba/Nova Scotia)"
        userprefs.certificatepriority(18) = "Peru"
        userprefs.certificatepriority(19) = "Sweden"
        userprefs.certificatepriority(20) = "Portugal"
        userprefs.certificatepriority(21) = "South Africa"
        userprefs.certificatepriority(22) = "Denmark"
        userprefs.certificatepriority(23) = "Hong Kong"
        userprefs.certificatepriority(24) = "Finland"
        userprefs.certificatepriority(25) = "India"
        userprefs.certificatepriority(26) = "Mexico"
        userprefs.certificatepriority(27) = "France"
        userprefs.certificatepriority(28) = "Italy"
        userprefs.certificatepriority(29) = "Switzerland (canton of Vaud)"
        userprefs.certificatepriority(30) = "Switzerland (canton of Geneva)"
        userprefs.certificatepriority(31) = "Germany"
        userprefs.certificatepriority(32) = "Greece"
        userprefs.certificatepriority(33) = "Austria"
        ReDim userprefs.moviethumbpriority(3)
        userprefs.moviethumbpriority(0) = "Internet Movie Poster Awards"
        userprefs.moviethumbpriority(1) = "themoviedb.org"
        userprefs.moviethumbpriority(2) = "Movie Poster DB"
        userprefs.moviethumbpriority(3) = "IMDB"
        userprefs.maximagecount = 10
        userprefs.videomode = 1
        userprefs.locx = 0
        userprefs.locy = 0
        userprefs.formheight = 725
        userprefs.formwidth = 1060
        moviefolders.Clear()
        tvfolders.Clear()
        userprefs.splt5 = 0
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
    Public Function getposterpath(ByVal fullpath As String) As String
        Try
            Dim posterpath As String = ""
            posterpath = fullpath.Substring(0, fullpath.Length - 4)
            posterpath = posterpath & ".tbn"
            'If Not IO.File.Exists(posterpath) Then
            Dim stackname As String = getstackname(IO.Path.GetFileName(fullpath), fullpath.Replace(IO.Path.GetFileName(fullpath), ""))
            stackname = fullpath.Replace(IO.Path.GetFileName(fullpath), stackname)
            stackname = stackname & ".tbn"
            If stackname <> "na" And IO.File.Exists(stackname) Then
                posterpath = stackname
            Else
                posterpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "")
                posterpath = posterpath & "movie.tbn"
                If Not IO.File.Exists(posterpath) Then
                    posterpath = ""
                End If
            End If
            '    Else
            'posterpath = fullpath.Replace("movie.nfo", "movie.tbn")
            'End If
            If posterpath = "" Then
                If fullpath.IndexOf("movie.nfo") <> -1 Then
                    posterpath = fullpath.Replace("movie.nfo", "movie.tbn")
                End If
            End If
            If posterpath = "" Then
                If userprefs.posternotstacked = True Then
                    posterpath = fullpath.Substring(0, fullpath.Length - 4) & ".tbn"
                Else
                    posterpath = getstackname(IO.Path.GetFileName(fullpath), posterpath.Replace(IO.Path.GetFileName(fullpath), "")) & ".tbn"
                    If posterpath = "na.tbn" Then
                        posterpath = fullpath.Substring(0, fullpath.Length - 4) & ".tbn"
                    Else
                        posterpath = fullpath.Replace(IO.Path.GetFileName(fullpath), posterpath)
                    End If
                End If
                If userprefs.basicsavemode = True Then
                    posterpath = posterpath.Replace(IO.Path.GetFileName(fullpath), "movie.tbn")
                End If
            End If
            If posterpath = "na" Then
                If IO.File.Exists(fullpath.Replace(IO.Path.GetFileName(fullpath), "folder.jpg")) Then
                    posterpath = fullpath.Replace(IO.Path.GetFileName(fullpath), "folder.jpg")
                End If
            End If
            Return posterpath
        Catch
            Return ""
        End Try
    End Function
    Public Function getfanartpath(ByVal fullpath As String) As String
        Try
            Dim posterpath As String = ""
            posterpath = fullpath.Substring(0, fullpath.Length - 4)
            posterpath = posterpath & "-fanart.jpg"
            If Not IO.File.Exists(posterpath) Then
                Dim stackname As String = getstackname(IO.Path.GetFileName(fullpath), fullpath.Replace(IO.Path.GetFileName(fullpath), ""))

                stackname = fullpath.Replace(IO.Path.GetFileName(fullpath), stackname)
                stackname = stackname & "-fanart.jpg"
                If stackname <> "na" And IO.File.Exists(stackname) Then
                    posterpath = stackname
                Else
                    posterpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "")
                    posterpath = posterpath & "fanart.jpg"
                    If Not IO.File.Exists(posterpath) Then
                        posterpath = ""
                    End If
                End If
                'Else
                '    posterpath = fullpath.Replace("movie.nfo", "movie.tbn")
            End If
            If posterpath = "" Then
                If fullpath.IndexOf("movie.nfo") <> -1 Then
                    posterpath = fullpath.Replace("movie.nfo", "fanart.jpg")
                End If
            End If
            If posterpath = "" Then
                If userprefs.fanartnotstacked = True Then
                    posterpath = fullpath.Substring(0, fullpath.Length - 4) & "-fanart.jpg"
                Else
                    posterpath = getstackname(IO.Path.GetFileName(fullpath), fullpath) & "-fanart.jpg"
                    If posterpath = "na-fanart.jpg" Then
                        posterpath = fullpath.Substring(0, fullpath.Length - 4) & "-fanart.jpg"
                    Else
                        posterpath = fullpath.Replace(IO.Path.GetFileName(fullpath), posterpath)
                    End If
                End If
                If userprefs.basicsavemode = True Then
                    posterpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "fanart.jpg")
                End If
            End If

            Return posterpath
        Catch
            Return ""
        End Try
    End Function
    Public Function getstackname(ByVal filenames As String, ByVal filepath As String) As String
        Try
            Dim tempboolean As Boolean = False
            Dim truerar As Boolean = False
            Dim filename As String = filenames
            If IO.Path.GetExtension(filename).ToLower = ".rar" Then
                truerar = True
            End If
            filenames = filenames.Replace(System.IO.Path.GetExtension(filenames), "")
            filename = filename.ToLower
            Dim stackname As String = ""
            Dim workingstring As String = "na"


            If filename.IndexOf("cd1") <> -1 Then
                tempboolean = True
                workingstring = "cd1"
            End If

            If filename.IndexOf("cd.1") <> -1 Then
                tempboolean = True
                workingstring = "cd.1"
            End If


            If filename.IndexOf("cd_1") <> -1 Then
                tempboolean = True
                workingstring = "cd_1"
            End If


            If filename.IndexOf("cd 1") <> -1 Then
                tempboolean = True
                workingstring = "cd 1"
            End If


            If filename.IndexOf("cd-1") <> -1 Then
                tempboolean = True
                workingstring = "cd-1"
            End If


            If filename.IndexOf("dvd1") <> -1 Then
                tempboolean = True
                workingstring = "dvd1"
            End If


            If filename.IndexOf("dvd.1") <> -1 Then
                tempboolean = True
                workingstring = "dvd.1"
            End If


            If filename.IndexOf("dvd_1") <> -1 Then
                tempboolean = True
                workingstring = "dvd_1"
            End If


            If filename.IndexOf("dvd 1") <> -1 Then
                tempboolean = True
                workingstring = "dvd 1"
            End If


            If filename.IndexOf("dvd-1") <> -1 Then
                tempboolean = True
                workingstring = "dvd-1"
            End If


            If filename.IndexOf("part1") <> -1 Then
                tempboolean = True
                workingstring = "part1"
            End If


            If filename.IndexOf("part.1") <> -1 Then
                tempboolean = True
                workingstring = "part.1"
            End If

            If filename.IndexOf("part_1") <> -1 Then
                tempboolean = True
                workingstring = "part_1"
            End If

            If filename.IndexOf("part-1") <> -1 Then
                tempboolean = True
                workingstring = "part-1"
            End If

            If filename.IndexOf("part 1") <> -1 Then
                tempboolean = True
                workingstring = "part 1"
            End If

            If filename.IndexOf("disk1") <> -1 Then
                tempboolean = True
                workingstring = "disk1"
            End If

            If filename.IndexOf("disk.1") <> -1 Then
                tempboolean = True
                workingstring = "disk.1"
            End If

            If filename.IndexOf("disk_1") <> -1 Then
                tempboolean = True
                workingstring = "disk_1"
            End If

            If filename.IndexOf("disk 1") <> -1 Then
                tempboolean = True
                workingstring = "disk 1"
            End If

            If filename.IndexOf("disk-1") <> -1 Then
                tempboolean = True
                workingstring = "disk-1"
            End If

            If filename.IndexOf("pt 1") <> -1 Then
                tempboolean = True
                workingstring = "pt 1"
            End If

            If filename.IndexOf("pt-1") <> -1 Then
                tempboolean = True
                workingstring = "pt-1"
            End If

            If filename.IndexOf("pt1") <> -1 Then
                tempboolean = True
                workingstring = "pt1"
            End If

            If filename.IndexOf("pt_1") <> -1 Then
                tempboolean = True
                workingstring = "pt_1"
            End If

            If filename.IndexOf("pt.1") <> -1 Then
                tempboolean = True
                workingstring = "pt.1"
            End If
            Dim extensions(23) As String
            Dim extensioncount As Integer
            extensions(1) = ".avi"
            extensions(2) = ".xvid"
            extensions(3) = ".divx"
            extensions(4) = ".img"
            extensions(5) = ".mpg"
            extensions(6) = ".mpeg"
            extensions(7) = ".mov"
            extensions(8) = ".rm"
            extensions(9) = ".3gp"
            extensions(10) = ".m4v"
            extensions(11) = ".wmv"
            extensions(12) = ".asf"
            extensions(13) = ".mp4"
            extensions(14) = ".mkv"
            extensions(15) = ".nrg"
            extensions(16) = ".iso"
            extensions(17) = ".rmvb"
            extensions(18) = ".ogm"
            extensions(19) = ".bin"
            extensions(20) = ".ts"
            extensions(21) = ".vob"
            extensions(22) = ".m2ts"
            extensions(23) = ".rar"

            extensioncount = 23

            'Dim stackpaths(22) As String


            Dim extension As String = System.IO.Path.GetExtension(filename)
            Dim filenameex As String

            filenameex = filename.Replace(System.IO.Path.GetExtension(filename), "")

            If filenameex.Substring(filenameex.Length - 1).ToLower = "a" Then
                Dim exists As Boolean = False
                Dim tempname As String
                For f = 1 To extensioncount
                    tempname = filepath & filename.Substring(0, filename.Length - (1 + extension.Length)) & "b" & extensions(f)
                    exists = System.IO.File.Exists(tempname)
                    If exists = True Then
                        workingstring = "a"
                        tempboolean = True
                        Exit For
                    End If
                Next
            End If





            If tempboolean = True Then
                Dim tbool As Boolean = False
                If workingstring <> "na" Then

                    filename = filename.Replace(System.IO.Path.GetExtension(filename), "")

                    Dim a() As String = {".", " ", "-", "_"}
                    Dim multipartidentify As String
                    For f = 0 To 3
                        multipartidentify = a(f) & workingstring
                        'filename = filename.Replace(System.IO.Path.GetExtension(filename), "")
                        If filename.IndexOf(multipartidentify) <> -1 Then
                            If multipartidentify.IndexOf(".") <> -1 Then
                                multipartidentify = multipartidentify.Replace(".", "\.")
                            End If
                            'filename = filename.Replace(multipartidentify, "")
                            filename = Regex.Replace(filenames, multipartidentify, "", RegexOptions.IgnoreCase)
                            tbool = True
                            Exit For
                        End If
                    Next
                End If

                If workingstring = "a" And tbool = False Then
                    Dim temp As String = filename
                    Dim temp2 As String
                    If temp.Substring(temp.Length - 1, 1) = "a" Then
                        temp = temp.Substring(0, temp.Length - 1)
                        For f = 1 To extensioncount
                            temp2 = filepath & temp & "b" & extensions(f)
                            If System.IO.File.Exists(temp2) = True Then
                                filename = filenames.Substring(0, filename.Length - 1)
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If

            If truerar = True Then
                If IO.Path.GetExtension(filename).ToLower = ".rar" Then
                    filename = filename.Replace(IO.Path.GetExtension(filename), "")
                End If
                If filename.ToLower.IndexOf(".part1") <> -1 Then
                    filename = filename.Replace(".part1", "")
                    tempboolean = True
                End If
                If filename.ToLower.IndexOf(".part01") <> -1 Then
                    filename = filename.Replace(".part01", "")
                    tempboolean = True
                End If
                If filename.ToLower.IndexOf(".part001") <> -1 Then
                    filename = filename.Replace(".part001", "")
                    tempboolean = True
                End If
                If filename.ToLower.IndexOf(".part0001") <> -1 Then
                    filename = filename.Replace(".part0001", "")
                    tempboolean = True
                End If
            End If

            If tempboolean = False Then filename = "na"
            Dim prefix(3)
            prefix(0) = " "
            prefix(1) = "_"
            prefix(2) = "-"
            prefix(3) = "."
            filename = RTrim(filename)
            If filename.IndexOf("_") = filename.Length - 1 Then filename = filename.Substring(0, filename.Length - 1)
            If filename.IndexOf("-") = filename.Length - 1 Then filename = filename.Substring(0, filename.Length - 1)
            If filename.IndexOf(".") = filename.Length - 1 Then filename = filename.Substring(0, filename.Length - 1)
            filename = RTrim(filename)

            Return filename
        Catch
            Return "na"
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
            Dim extensions(22) As String
            Dim extensioncount As Integer
            extensions(1) = ".avi"
            extensions(2) = ".xvid"
            extensions(3) = ".divx"
            extensions(4) = ".img"
            extensions(5) = ".mpg"
            extensions(6) = ".mpeg"
            extensions(7) = ".mov"
            extensions(8) = ".rm"
            extensions(9) = ".3gp"
            extensions(10) = ".m4v"
            extensions(11) = ".wmv"
            extensions(12) = ".asf"
            extensions(13) = ".mp4"
            extensions(14) = ".mkv"
            extensions(15) = ".nrg"
            extensions(16) = ".iso"
            extensions(17) = ".rmvb"
            extensions(18) = ".ogm"
            extensions(19) = ".bin"
            extensions(20) = ".ts"
            extensions(21) = ".vob"
            extensions(22) = ".m2ts"
            'extensions(23) = ".rar"
            extensioncount = 22

            If IO.File.Exists(tempfilename.Replace(IO.Path.GetFileName(tempfilename), "VIDEO_TS.IFO")) Then
                actualpathandfilename = tempfilename.Replace(IO.Path.GetFileName(tempfilename), "VIDEO_TS.IFO")
            End If

            If actualpathandfilename = "" Then
                For f = 1 To 22
                    tempfilename = tempfilename.Replace(IO.Path.GetExtension(tempfilename), extensions(f))
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
                        Dim pattern As String = "*" & extensions(f)
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
    End Function
  
    Public Function EnumerateDirectory2(ByVal RootDirectory As String, Optional ByVal log As Boolean = False)

        Dim dli As New List(Of String)
        Try
            'dli.Add(RootDirectory)

            For Each s As String In Directory.GetDirectories(RootDirectory)
                If (File.GetAttributes(s) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                Else
                    If Not validmoviedir(s) Then
                    Else
                        Dim exists As Boolean = False
                        For Each item In dli
                            If item = s Then exists = True
                        Next
                        If exists = True Then
                        Else
                            dli.Add(s)
                            For Each t As String In Directory.GetDirectories(s)
                                If (File.GetAttributes(t) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                                Else
                                    If Not validmoviedir(t) Then
                                    Else
                                        Dim existst As Boolean = False
                                        For Each item In dli
                                            If item = t Then existst = True
                                        Next
                                        If exists = True Then
                                        Else
                                            dli.Add(t)
                                            For Each u As String In Directory.GetDirectories(t)
                                                If (File.GetAttributes(u) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                                                Else
                                                    If Not validmoviedir(u) Then
                                                    Else
                                                        Dim existsu As Boolean = False
                                                        For Each item In dli
                                                            If item = s Then existsu = True
                                                        Next
                                                        If exists = True Then
                                                        Else
                                                            dli.Add(u)
                                                            For Each v As String In Directory.GetDirectories(u)
                                                                If (File.GetAttributes(v) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                                                                Else
                                                                    If Not validmoviedir(v) Then
                                                                    Else
                                                                        Dim existsv As Boolean = False
                                                                        For Each item In dli
                                                                            If item = v Then existsv = True
                                                                        Next
                                                                        If existsv = True Then
                                                                        Else
                                                                            dli.Add(v)
                                                                            For Each w As String In Directory.GetDirectories(v)
                                                                                If (File.GetAttributes(w) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                                                                                Else
                                                                                    If Not validmoviedir(w) Then
                                                                                    Else
                                                                                        Dim existsw As Boolean = False
                                                                                        For Each item In dli
                                                                                            If item = w Then existsw = True
                                                                                        Next
                                                                                        If existsw = True Then
                                                                                        Else
                                                                                            dli.Add(w)
                                                                                            For Each x As String In Directory.GetDirectories(w)
                                                                                                If (File.GetAttributes(x) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                                                                                                Else
                                                                                                    If Not validmoviedir(x) Then
                                                                                                    Else
                                                                                                        Dim existsx As Boolean = False
                                                                                                        For Each item In dli
                                                                                                            If item = x Then existsx = True
                                                                                                        Next
                                                                                                        If existsx = True Then
                                                                                                        Else
                                                                                                            dli.Add(x)
                                                                                                            For Each y As String In Directory.GetDirectories(x)
                                                                                                                If (File.GetAttributes(y) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
                                                                                                                Else
                                                                                                                    If Not validmoviedir(y) Then
                                                                                                                    Else
                                                                                                                        Dim existsy As Boolean = False
                                                                                                                        For Each item In dli
                                                                                                                            If item = y Then existsy = True
                                                                                                                        Next
                                                                                                                        If existsy = True Then
                                                                                                                        Else
                                                                                                                            dli.Add(y)
                                                                                                                        End If
                                                                                                                    End If
                                                                                                                End If
                                                                                                            Next
                                                                                                        End If
                                                                                                    End If
                                                                                                End If
                                                                                            Next
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        End If
                                                                    End If
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            Next
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
                    If validmoviedir(s) Then
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
    Public Function validmoviedir(ByVal s As String) As Boolean
        Dim passed As Boolean = True
        Try
            For Each t As String In Directory.GetDirectories(s)
            Next
            Select Case True
                Case Strings.Right(s, 7).ToLower = "trailer"
                    passed = False
                Case Strings.Right(s, 8).ToLower = "(noscan)"
                    passed = False
                Case Strings.Right(s, 6).ToLower = "sample"
                    passed = False
                Case Strings.Right(s, 8).ToLower = "recycler"
                    passed = False
                Case s.ToLower.Contains("$recycle.bin")
                    passed = False
                Case Strings.Right(s, 10).ToLower = "lost+found"
                    passed = False
                Case s.ToLower.Contains("system volume information")
                    passed = False
                Case s.Contains("MSOCache")
                    passed = False
            End Select
        Catch ex As Exception
            passed = False
        End Try
        Return passed
    End Function
    Public Function cleanfilename(ByVal filename As String, Optional ByVal withextension As Boolean = True)
        Dim cleanname As String = filename
        Try
            If withextension = True Then
                Try
                    cleanname = filename.Replace(IO.Path.GetExtension(cleanname), "")
                Catch
                End Try
            End If
            Dim movieyear As String
            Dim S As String = cleanname
            Dim M As Match
            M = Regex.Match(S, "(\([\d]{4}\))")
            If M.Success = True Then
                movieyear = M.Value
            Else
                movieyear = Nothing
            End If
            If movieyear = Nothing Then
                M = Regex.Match(S, "(\[[\d]{4}\])")
                If M.Success = True Then
                    movieyear = M.Value
                Else
                    movieyear = Nothing
                End If
            End If



            filename = cleanname
            Dim removal(72) As String
            removal(1) = "cd1"
            removal(2) = "cd.1"
            removal(3) = "cd_1"
            removal(4) = "cd 1"
            removal(5) = "cd-1"
            removal(6) = "dvd1"
            removal(7) = "dvd.1"
            removal(8) = "dvd_1"
            removal(9) = "dvd 1"
            removal(10) = "dvd-1"
            removal(11) = "part1"
            removal(12) = "part.1"
            removal(13) = "part_1"
            removal(14) = "part 1"
            removal(15) = "part-1"
            removal(16) = "disk1"
            removal(17) = "disk.1"
            removal(18) = "disk_1"
            removal(19) = "disk 1"
            removal(20) = "disk-1"
            removal(21) = "pt1"
            removal(22) = "pt.1"
            removal(23) = "pt_1"
            removal(24) = "pt 1"
            removal(25) = "pt-1"
            removal(26) = "ac3"
            removal(27) = "divx"
            removal(28) = "xvid"
            removal(29) = "dvdrip"
            removal(30) = "directors cut"
            removal(31) = "special edition"
            removal(32) = "screener"
            removal(33) = "telesync"
            removal(34) = "telecine"
            removal(35) = "director's cut"
            removal(36) = " r5"
            removal(37) = " scr"
            removal(38) = ".scr"
            removal(39) = "_scr"
            removal(40) = "-scr"
            removal(41) = " ts"
            removal(42) = "_ts"
            removal(43) = ".ts"
            removal(44) = "-ts"
            removal(45) = " fs"
            removal(46) = ".fs"
            removal(47) = "_fs"
            removal(48) = "-fs"
            removal(49) = " ws"
            removal(50) = ".ws"
            removal(51) = "_ws"
            removal(52) = "-ws"
            removal(53) = "-r5"
            removal(54) = "_r5"
            removal(55) = ".r5"
            removal(56) = "bluray"
            removal(57) = "720"
            removal(58) = "1024"
            removal(59) = "fullscreen"
            removal(60) = "widescreen"
            removal(61) = "dvdscr"
            removal(62) = "part01"
            removal(63) = "dvd5"
            removal(64) = "dvd9"
            removal(65) = "dvd 5"
            removal(66) = "dvd 9"
            removal(67) = "dvd-5"
            removal(68) = "dvd-9"
            removal(69) = "dvd_5"
            removal(70) = "dvd_9"
            removal(71) = "dvd.5"
            removal(72) = "dvd.9"
            Dim currentposition As Integer = filename.Length
            Dim newposition As Integer = filename.Length
            For f = 1 To 72
                If filename.ToLower.IndexOf(removal(f)) <> -1 Then
                    newposition = filename.ToLower.IndexOf(removal(f))
                    If newposition < currentposition Then currentposition = newposition
                End If
            Next
            If movieyear <> Nothing Then
                If filename.IndexOf(movieyear) <> -1 Then
                    newposition = filename.IndexOf(movieyear)
                    If newposition < currentposition Then currentposition = newposition
                End If
            End If
            If currentposition < filename.Length And currentposition > 0 Then
                filename = filename.Substring(0, currentposition)
                If filename.Substring(filename.Length - 1, 1) = "-" Or filename.Substring(filename.Length - 1, 1) = "_" Or filename.Substring(filename.Length - 1, 1) = "." Or filename.Substring(filename.Length - 1, 1) = " " Then
                    filename = filename.Substring(0, filename.Length - 1)
                End If
            End If

            If filename <> "" Then
                cleanname = filename
            End If
            cleanname = Trim(cleanname)
            Return cleanname
        Catch ex As Exception
            cleanname = "error"
            Return cleanname
        End Try
    End Function
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
                Exit Function
            End If
            Dim workingfiledetails As New fullfiledetails
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
                If location.IndexOf(userprefs.actornetworkpath) <> -1 Then
                    If userprefs.actornetworkpath <> Nothing And userprefs.actorsavepath <> Nothing Then
                        If userprefs.actornetworkpath <> "" And userprefs.actorsavepath <> "" Then
                            Dim filename As String = IO.Path.GetFileName(location)
                            actualpath = IO.Path.Combine(userprefs.actorsavepath, filename)
                            If Not IO.File.Exists(actualpath) Then
                                Dim extension As String = IO.Path.GetExtension(location)
                                Dim purename As String = IO.Path.GetFileName(location)
                                purename = purename.Replace(extension, "")
                                actualpath = userprefs.actorsavepath & "\" & purename.Substring(purename.Length - 2, 2) & "\" & filename
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

    End Function

    Private Sub Listmoviefiles(ByVal lst As String, ByVal pattern As String, ByVal dir_info As System.IO.DirectoryInfo)
        Dim moviepattern As String = pattern
        Dim tempint2 As Integer
        Dim tempstring As String
        Try
            Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(moviepattern)
            Dim tempmovie As String
            Dim dofilter As Boolean = False
            Dim dvdfiles As Boolean
            For Each fs_info As System.IO.FileInfo In fs_infos

                Dim newmoviedetails As newmovie
                Dim title As String
                Dim remove As Boolean = False
                dvdfiles = False
                dofilter = False
                tempmovie = fs_info.FullName.Replace(System.IO.Path.GetExtension(fs_info.FullName), ".nfo")
                newmoviedetails.mediapathandfilename = fs_info.FullName
                newmoviedetails.nfopathandfilename = tempmovie
                Dim basicmoviename As String = tempmovie.Replace(IO.Path.GetFileName(tempmovie), "movie.nfo")
                If IO.File.Exists(basicmoviename) Then
                    remove = True
                End If
                basicmoviename = getstackname(IO.Path.GetFileName(fs_info.FullName), fs_info.FullName)
                Dim otherformat As String = tempmovie.Replace(IO.Path.GetFileName(tempmovie), basicmoviename & ".nfo")
                If IO.File.Exists(otherformat) Then
                    Dim allok2 As Boolean = False
                    Try
                        Dim filechck As IO.StreamReader = IO.File.OpenText(otherformat)
                        Do

                            tempstring = filechck.ReadLine
                            If tempstring = Nothing Then Exit Do

                            If tempstring.IndexOf("<movie>") <> -1 Then
                                allok2 = True
                                Exit Do
                            End If
                        Loop Until tempstring.IndexOf("</movie>") <> -1
                        filechck.Close()
                    Catch
                    End Try
                    If allok2 = True Then
                        remove = True
                    End If
                End If

                If moviepattern = "*.vob" Then
                    Dim lonevobfile As String = tempmovie.Replace(System.IO.Path.GetFileName(tempmovie), "VIDEO_TS.IFO")
                    dvdfiles = System.IO.File.Exists(lonevobfile)
                End If

                If dvdfiles = False Then
                    If IO.File.Exists(tempmovie) = True Then


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
                        Catch
                        Finally
                        End Try

                        If allok = False Then
                            dofilter = True
                            title = fs_info.FullName
                        End If
                    Else
                        dofilter = True
                        title = fs_info.FullName
                    End If


                    If dofilter = True Then
                        If title <> Nothing Then
                            Dim searchboolean As Boolean = False
                            Dim filename2 As String = System.IO.Path.GetFileName(title).ToLower
                            If filename2.IndexOf("cd2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd.2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd.3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd.4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd.5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd_9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd5") <> -1 Then
                                Dim tempfile As String = title.Replace("dvd5", "dvd1")
                                If IO.File.Exists(tempfile) Then
                                    searchboolean = True
                                End If
                            End If
                            If filename2.IndexOf("dvd6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd9") <> -1 Then
                                Dim tempfile As String = title.Replace("dvd9", "dvd1")
                                If IO.File.Exists(tempfile) Then
                                    searchboolean = True
                                End If
                            End If
                            If filename2.IndexOf("dvd.2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd.3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd.4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd.5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd_9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part.2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part.3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part.4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part.5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part_9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk.9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk_9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd 9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("cd-9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd 9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("dvd-9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part-9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("part 9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk 9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("disk-9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt 9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt-9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt_9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.2") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.3") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.4") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.5") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.6") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.7") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.8") <> -1 Then searchboolean = True
                            If filename2.IndexOf("pt.9") <> -1 Then searchboolean = True
                            If filename2.IndexOf("-trailer") <> -1 Then searchboolean = True
                            If filename2.IndexOf(".trailer") <> -1 Then searchboolean = True
                            If filename2.IndexOf("_trailer") <> -1 Then searchboolean = True
                            If filename2.IndexOf("sample") <> -1 And filename2.IndexOf("people") = -1 Then searchboolean = True

                            Dim tempname As String
                            Dim extension As String
                            extension = System.IO.Path.GetExtension(filename2)
                            tempname = filename2.Replace(extension, "")
                            If tempname.Substring(tempname.Length - 1) = "b" Or tempname.Substring(tempname.Length - 1) = "c" Or tempname.Substring(tempname.Length - 1) = "d" Or tempname.Substring(tempname.Length - 1) = "e" Or tempname.Substring(tempname.Length - 1) = "B" Or tempname.Substring(tempname.Length - 1) = "C" Or tempname.Substring(tempname.Length - 1) = "D" Or tempname.Substring(tempname.Length - 1) = "E" Then
                                'tempname = tempname.Substring(tempname.Length - 1)
                                tempname = newmoviedetails.nfopathandfilename.Substring(0, newmoviedetails.nfopathandfilename.Length - (1 + extension.Length)) & "a" & extension
                                Dim newtempboolean As Boolean = False
                                newtempboolean = System.IO.File.Exists(tempname)
                                If newtempboolean = True Then searchboolean = True
                                If newtempboolean = False Then
                                End If
                            End If
                            If searchboolean = True Then
                                remove = True
                            End If
                        End If
                    End If
                End If


                Dim tempmovie2 As String = fs_info.FullName
                If IO.Path.GetExtension(tempmovie2).ToLower = ".rar" Then
                    If IO.File.Exists(tempmovie2) = True Then
                        If IO.File.Exists(tempmovie) = False Then
                            Dim rarname As String = tempmovie2
                            Dim SizeOfFile As Integer
                            tempint2 = Convert.ToInt32(userprefs.rarsize) * 1048576
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
                                            Catch
                                            Finally
                                            End Try
                                            If allok = True Then
                                                remove = True
                                            Else
                                                title = tempmovie
                                            End If
                                        Else
                                            title = tempmovie
                                        End If
                                    Else
                                        remove = True
                                    End If
                                Else
                                    'remove = True
                                End If
                            Else
                                remove = True
                            End If
                        End If
                    End If
                End If
                If remove = True Then
                    remove = False
                    title = Nothing
                    newmoviedetails.mediapathandfilename = Nothing
                    newmoviedetails.nfopath = Nothing
                    newmoviedetails.nfopathandfilename = Nothing
                    newmoviedetails.title = Nothing
                Else
                    If title <> Nothing Then

                        Dim extension As String
                        Dim filename2 As String
                        newmoviedetails.nfopathandfilename = title
                        extension = System.IO.Path.GetExtension(title)
                        filename2 = System.IO.Path.GetFileName(title)
                        newmoviedetails.nfopath = title.Replace(filename2, "")
                        newmoviedetails.title = filename2.Replace(extension, "")
                        If extension <> ".IFO" And extension <> "ttt" Then
                            'newmoviedetails.mediapathandfilename = title
                            newmoviedetails.nfopathandfilename = newmoviedetails.nfopathandfilename.Replace(extension, ".nfo")
                        End If
                        'If dvdfolder = True Then
                        If extension.ToLower = ".ifo" Then
                            newmoviedetails.mediapathandfilename = title
                            newmoviedetails.nfopathandfilename = newmoviedetails.mediapathandfilename.Replace(extension, ".nfo")
                            If IO.File.Exists(newmoviedetails.nfopathandfilename) = False Then
                                Dim paths() As String
                                If newmoviedetails.nfopathandfilename.IndexOf("\") <> -1 Then
                                    paths = newmoviedetails.nfopathandfilename.Split("\")
                                ElseIf newmoviedetails.nfopathandfilename.IndexOf("/") <> -1 Then
                                    paths = newmoviedetails.nfopathandfilename.Split("/")
                                End If
                                Dim depthecount As Integer
                                depthecount = paths.GetUpperBound(0)
                                newmoviedetails.title = Nothing

                                For h = depthecount To 0 Step -1
                                    Dim temppath As String
                                    temppath = paths(h)
                                    paths(h) = paths(h).ToLower
                                    If paths(h).IndexOf("video_ts") = -1 Then
                                        newmoviedetails.title = temppath
                                    End If
                                    If newmoviedetails.title <> Nothing Then Exit For
                                Next
                            Else
                                newmoviedetails.nfopathandfilename = Nothing
                                newmoviedetails.title = Nothing
                            End If
                        End If

                        'End If




                        'filenamekeeper = ""
                        If userprefs.usefoldernames = True And newmoviedetails.title <> Nothing Then
                            Dim tempstring4 As String
                            tempstring4 = newmoviedetails.nfopathandfilename.ToLower
                            If tempstring4.IndexOf("video_ts") = -1 Then
                                newmoviedetails.title = newmoviedetails.nfopath.Substring(0, newmoviedetails.nfopath.Length - 1)
                                newmoviedetails.title = newmoviedetails.title.Substring(newmoviedetails.title.LastIndexOf("\") + 1, newmoviedetails.title.Length - newmoviedetails.title.LastIndexOf("\") - 1)
                                newmoviedetails.title = getlastfolder(newmoviedetails.nfopathandfilename)
                            End If
                        End If
                        Dim alreadyadded As Boolean = False
                        For Each newmovie In newmovielist
                            If newmovie.nfopathandfilename = newmoviedetails.nfopathandfilename Then
                                alreadyadded = True
                                Exit For
                            End If
                        Next
                        If alreadyadded = False Then
                            newmovielist.Add(newmoviedetails)
                        Else
                            alreadyadded = False
                        End If
                    End If
                End If
            Next fs_info
            fs_infos = Nothing


        Catch
        End Try

    End Sub
    Private Sub loadactorcache()
        actordb.Clear()
        Dim loadpath As String = workingprofile.actorcache
        Dim actorlist As New XmlDocument
        actorlist.Load(loadpath)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In actorlist("actor_cache")
            Select Case thisresult.Name
                Case "actor"
                    Dim newactor As New actordatabase
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
                            actordb.Add(newactor)
                        End If
                    Next
            End Select
        Next
    End Sub
    Private Sub saveactorcache()
        Dim savepath As String = workingprofile.actorcache
        Dim doc As New XmlDocument

        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration

        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("actor_cache")

        Dim childchild As XmlElement
        For Each actor In actordb
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
        Dim moviepattern As String
        Dim tempint As Integer
        Dim tempstring As String
        Dim errorcounter As Integer = 0
        Dim trailer As String
        Dim newmoviecount As Integer
        Dim dirinfo As String
        newmovielist.Clear()
        Dim newmoviefolders As New List(Of String)
        Dim progress As Integer
        progress = 0
        Dim progresstext As String
        Dim dirpath As String

        Console.WriteLine("Starting Folder Scan")

        Dim extension As String
        Dim filename2 As String
        Dim extensions(100) As String
        Dim extensioncount As Integer
        extensions(1) = "*.avi"
        extensions(2) = "*.xvid"
        extensions(3) = "*.divx"
        extensions(4) = "*.img"
        extensions(5) = "*.mpg"
        extensions(6) = "*.mpeg"
        extensions(7) = "*.mov"
        extensions(8) = "*.rm"
        extensions(9) = "*.3gp"
        extensions(10) = "*.m4v"
        extensions(11) = "*.wmv"
        extensions(12) = "*.asf"
        extensions(13) = "*.mp4"
        extensions(14) = "*.mkv"
        extensions(15) = "*.nrg"
        extensions(16) = "*.iso"
        extensions(17) = "*.rmvb"
        extensions(18) = "*.ogm"
        extensions(19) = "*.bin"
        extensions(20) = "*.ts"
        extensions(21) = "*.vob"
        extensions(22) = "*.m2ts"
        extensions(23) = "*.rar"
        extensions(24) = "*.dvr-ms"
        extensions(25) = "VIDEO_TS.IFO"
        extensioncount = 25

        For Each moviefolder In moviefolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                Console.WriteLine("found" & hg.FullName.ToString)
                newmoviefolders.Add(moviefolder)
                Console.WriteLine("Checking for subfolders")
                Dim newlist As List(Of String)
                Try
                    newlist = EnumerateDirectory2(moviefolder)
                    For Each subfolder In newlist
                        Console.WriteLine("Subfolder added :- " & subfolder.ToString)
                        newmoviefolders.Add(subfolder)
                    Next
                Catch
                End Try
            End If
        Next
        For Each moviefolder In userprefs.offlinefolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                Console.WriteLine("found" & hg.FullName.ToString)
                'newmoviefolders.Add(moviefolder)
                Console.WriteLine("Checking for subfolders")
                Dim newlist As List(Of String)
                Try
                    newlist = EnumerateDirectory3(moviefolder)
                    For Each subfolder In newlist
                        'If subfolder.IndexOf(".actors") = -1 Then
                        Console.WriteLine("Subfolder added :- " & subfolder.ToString)
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
        Dim mediacounter As Integer = newmovielist.Count
        For g = 0 To newmoviefolders.Count - 1
            Try
                For f = 1 To extensioncount
                    moviepattern = extensions(f)
                    dirpath = newmoviefolders(g)
                    Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                    Listmoviefiles(dirinfo, moviepattern, dir_info)
                Next f
                tempint = newmovielist.Count - mediacounter

                Console.WriteLine(tempint.ToString & " New movies found in directory:- " & dirpath)
                mediacounter = newmovielist.Count
            Catch ex As Exception

            End Try

        Next g
        Console.WriteLine(newmovielist.Count & " Movies found in all folders")



        Console.WriteLine("Obtaining Title for each movie found, from path and filename")
        For Each movie In newmovielist
            Try
                extension = System.IO.Path.GetExtension(movie.nfopathandfilename)
                filename2 = System.IO.Path.GetFileName(movie.nfopathandfilename)
                Console.WriteLine("")
                movie.nfopath = movie.nfopathandfilename.Replace(filename2, "")
                movie.title = filename2.Replace(extension, "")
                If extension.ToLower <> ".ifo" Then
                    Try
                        movie.nfopathandfilename = movie.nfopathandfilename.Replace(extension, ".nfo")
                    Catch
                        Console.WriteLine("Unable to get movie title, stage1")
                        Console.WriteLine("Path is: " & movie.nfopathandfilename)
                    End Try
                End If

                'If dvdfolder = True Then
                If extension.ToLower = ".ifo" Or userprefs.usefoldernames = True Then
                    Try
                        movie.nfopathandfilename = movie.nfopathandfilename.Replace(extension, ".nfo")
                        movie.title = getlastfolder(movie.nfopathandfilename)
                    Catch
                        Console.WriteLine("Unable to get movie title, stage2")
                        Console.WriteLine("Path is: " & movie.nfopathandfilename)
                    End Try
                End If


                If movie.title <> Nothing Then
                    If movie.title <> "" Then
                        tempstring = cleanfilename(movie.title, False)
                        If tempstring <> Nothing Then
                            If tempstring <> "" Then
                                If tempstring <> "error" Then
                                    movie.title = tempstring
                                Else
                                    Console.WriteLine("Unable to clean title: " & movie.title)
                                End If
                            Else
                                Console.WriteLine("Cleaning title returns blank: " & movie.title)
                            End If
                        Else
                            Console.WriteLine("Cleaning title returns nothing: " & movie.title)
                        End If
                    End If
                End If


                Console.WriteLine("Filename is: " & movie.mediapathandfilename)
                Console.WriteLine("Title according to settings is: """ & movie.title & """")
            Catch

            End Try

        Next



        Dim movieyear As String
        newmoviecount = newmovielist.Count.ToString
        Console.WriteLine("Starting Main Scraper Process")
        For f = 0 To newmovielist.Count - 1
            Dim stage As Integer = 0
            Dim bodyok As Boolean = True
            'stage 0 = starting scraper
            'Try
            Dim title As String = ""
            Dim nfopath As String = ""
            Dim fanartpath As String = ""
            Dim posterpath As String = ""
            Dim year As String = ""
            Dim thumbstring As New XmlDocument
            progress = ((100 / newmoviecount) * (f + 1) * 10)
            progresstext = String.Concat("Scraping Movie " & f + 1 & " of " & newmoviecount)
            If newmovielist(f).title = Nothing Then
                Console.WriteLine("No Filename found for" & newmovielist(f).nfopathandfilename)
            End If
            Dim extrapossibleID As String = Nothing
            If newmovielist(f).title <> Nothing Then
                title = newmovielist(f).title
                Console.WriteLine("Scraping Title:- " & newmovielist(f).title)
                nfopath = newmovielist(f).nfopathandfilename
                If userprefs.basicsavemode = True Then
                    nfopath = newmovielist(f).nfopathandfilename.Replace(IO.Path.GetFileName(newmovielist(f).nfopathandfilename), "movie.nfo")
                End If
                Console.WriteLine("Output filename:- " & nfopath)
                posterpath = getposterpath(nfopath)
                fanartpath = getfanartpath(nfopath)
                Console.WriteLine("Poster Path:- " & posterpath)
                Console.WriteLine("Fanart Path:- " & fanartpath)

                extrapossibleID = Nothing
                Dim T As String
                Dim mat As Match

                If IO.File.Exists(nfopath) Then
                    Console.WriteLine("nfo file exists, checking for IMDB ID")
                    Dim tempinfo As String = ""
                    Dim objReader As New System.IO.StreamReader(nfopath)
                    tempinfo = objReader.ReadToEnd
                    objReader.Close()
                    extrapossibleID = Nothing
                    T = tempinfo
                    mat = Nothing
                    mat = Regex.Match(T, "(tt\d{7})")
                    If mat.Success = True Then
                        Console.WriteLine("IMDB ID found in nfo file:- " & mat.Value)
                        extrapossibleID = mat.Value
                    Else
                        Console.WriteLine("No IMDB ID found")
                        extrapossibleID = Nothing
                    End If
                    'If userprefs.renamenfofiles = True Then
                    Try
                        If Not IO.File.Exists(nfopath.Replace(".nfo", ".info")) Then
                            IO.File.Move(nfopath, nfopath.Replace(".nfo", ".info"))
                            Console.WriteLine("renaming nfo file to:- " & nfopath.Replace(".nfo", ".info"))
                        Else
                            Console.WriteLine("Unable to rename file, """ & nfopath & """ already exists")
                        End If
                    Catch
                        Console.WriteLine("Unable to rename file, """ & nfopath & """ already exists")
                    End Try
                    'Else
                    '    Console.writeline("Current nfo file will be overwritten")
                    'End If
                Else
                    Dim stackname As String = getstackname(nfopath, nfopath.Replace(IO.Path.GetFileName(nfopath), ""))
                    Dim path As String = stackname & ".nfo"
                    If IO.File.Exists(path) Then
                        Console.WriteLine("nfo file exists, checking for IMDB ID")
                        Dim tempinfo As String = ""
                        Dim objReader As New System.IO.StreamReader(path)
                        tempinfo = objReader.ReadToEnd
                        objReader.Close()
                        extrapossibleID = Nothing
                        T = tempinfo
                        mat = Nothing
                        mat = Regex.Match(T, "(tt\d{7})")
                        If mat.Success = True Then
                            Console.WriteLine("IMDB ID found in nfo file:- " & mat.Value)
                            extrapossibleID = mat.Value
                        Else
                            Console.WriteLine("No IMDB ID found")
                            extrapossibleID = Nothing
                        End If
                    Else
                        Console.WriteLine("NFO does not exist")
                    End If

                End If


                If extrapossibleID = Nothing Then
                    Console.WriteLine("Checking filename for IMDB ID")
                    mat = Nothing
                    T = newmovielist(f).nfopathandfilename
                    mat = Regex.Match(T, "(tt\d{7})")
                    If mat.Success = True Then
                        Console.WriteLine("IMDB ID found in filename:- " & mat.Value)
                        extrapossibleID = mat.Value
                    Else
                        extrapossibleID = Nothing
                        Console.WriteLine("No IMDB ID found")
                    End If
                End If

                If extrapossibleID = Nothing Then
                    Console.WriteLine("Checking for Movie year in filename")
                    If extrapossibleID = Nothing Then
                        Dim M As Match
                        M = Regex.Match(newmovielist(f).nfopathandfilename, "(\([\d]{4}\))")
                        If M.Success = True Then
                            movieyear = M.Value
                        Else
                            movieyear = Nothing
                        End If
                        If movieyear = Nothing Then
                            M = Regex.Match(newmovielist(f).nfopathandfilename, "(\[[\d]{4}\])")
                            If M.Success = True Then
                                movieyear = M.Value
                            Else
                                movieyear = Nothing
                            End If
                        End If
                    End If

                    If movieyear = Nothing Then
                        Console.WriteLine("No year found in filename")
                    Else
                        movieyear = movieyear.Replace("(", "")
                        movieyear = movieyear.Replace(")", "")
                        movieyear = movieyear.Replace("[", "")
                        movieyear = movieyear.Replace("]", "")
                        Console.WriteLine("Year found for movie:- " & movieyear.ToString)
                    End If
                End If
                title = cleanfilename(title, False)
                Console.WriteLine("Cleaned Title for search :- " & title)
                Dim newmovie As New fullmoviedetails
                Dim scraperfunction As New imdb.Classimdbscraper
                Dim body As String
                Dim actorlist As String
                Dim certificates As New List(Of String)
                stage = 1
                'stage 1 = get movie body

                imdbcounter += 1

                'Dim newscraper As New Classimdb
                'body = newscraper.getimdbbody(title, movieyear, extrapossibleID, userprefs.imdbmirror, imdbcounter)
                body = scraperfunction.getimdbbody(title, movieyear, extrapossibleID, userprefs.imdbmirror, imdbcounter)
                Dim thisresult As XmlNode = Nothing
                If body = "MIC" Then
                    Console.WriteLine("Unable to scrape body with refs """ & title & """, """ & movieyear & """, """ & extrapossibleID & """, """ & userprefs.imdbmirror & """")
                    If imdbcounter < 50 Then
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

                    Dim movietoadd As New combolist
                    movietoadd.fullpathandfilename = nfopath
                    movietoadd.filename = IO.Path.GetFileName(newmovielist(f).nfopathandfilename)
                    movietoadd.foldername = getlastfolder(newmovielist(f).nfopathandfilename)
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

                    Dim filecreation As New FileInfo(newmovielist(f).nfopathandfilename)
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
                    movietoadd.outline = newmovie.fullmoviebody.outline
                    movietoadd.id = newmovie.fullmoviebody.imdbid
                    movietoadd.rating = newmovie.fullmoviebody.rating
                    movietoadd.top250 = newmovie.fullmoviebody.top250
                    movietoadd.genre = newmovie.fullmoviebody.genre
                    movietoadd.playcount = newmovie.fullmoviebody.playcount
                    movietoadd.missingdata1 = 3
                    movietoadd.runtime = "0"
                    fullmovielist.Add(movietoadd)
                Else
                    Try
                        Console.WriteLine("Movie Body Scraped OK")
                        thumbstring.LoadXml(body)
                        For Each thisresult In thumbstring("movie")
                            Select Case thisresult.Name
                                Case "title"
                                    If userprefs.keepfoldername = False Then
                                        newmovie.fullmoviebody.title = thisresult.InnerText
                                    Else
                                        If userprefs.usefoldernames = False Then
                                            tempstring = IO.Path.GetFileName(newmovielist(f).nfopathandfilename)
                                            newmovie.fullmoviebody.title = cleanfilename(tempstring, False)
                                        Else
                                            newmovie.fullmoviebody.title = cleanfilename(getlastfolder(newmovielist(f).nfopathandfilename), False)
                                        End If
                                    End If
                                Case "alternativetitle"
                                    newmovie.alternativetitles.Add(thisresult.InnerText)
                                Case "credits"
                                    newmovie.fullmoviebody.credits = thisresult.InnerText
                                Case "director"
                                    newmovie.fullmoviebody.director = thisresult.InnerText
                                Case "genre"
                                    Dim strarr() As String
                                    strarr = thisresult.InnerText.Split("/")
                                    For count = 0 To strarr.Length - 1
                                        strarr(count) = strarr(count).Replace(" ", "")
                                    Next
                                    If strarr.Length <= userprefs.maxmoviegenre Then
                                        newmovie.fullmoviebody.genre = thisresult.InnerText
                                    Else
                                        For g = 0 To userprefs.maxmoviegenre - 1
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
                    Catch ex As Exception
                        Console.WriteLine("Error with " & newmovielist(f).nfopathandfilename)
                        Console.WriteLine("An error was encountered at stage 1, Downloading Movie Body")
                        Console.WriteLine(ex.Message.ToString)
                        errorcounter += 1
                        If userprefs.usefoldernames = False Then
                            tempstring = IO.Path.GetFileName(newmovielist(f).nfopathandfilename)
                            newmovie.fullmoviebody.title = cleanfilename(tempstring, False)
                        Else
                            newmovie.fullmoviebody.title = cleanfilename(getlastfolder(newmovielist(f).nfopathandfilename), False)
                        End If
                    End Try
                    If newmovie.fullmoviebody.playcount = Nothing Then newmovie.fullmoviebody.playcount = "0"
                    If newmovie.fullmoviebody.top250 = Nothing Then newmovie.fullmoviebody.top250 = "0"

                    Dim done As Boolean = False
                    For g = 0 To UBound(userprefs.certificatepriority)
                        For Each cert In certificates
                            If cert.IndexOf(userprefs.certificatepriority(g)) <> -1 Then
                                newmovie.fullmoviebody.mpaa = cert.Substring(cert.IndexOf("|") + 1, cert.Length - cert.IndexOf("|") - 1)
                                done = True
                                Exit For
                            End If
                        Next
                        If done = True Then Exit For
                    Next
                    If userprefs.keepfoldername = True Then
                        If userprefs.usefoldernames = False Then
                            tempstring = IO.Path.GetFileName(newmovielist(f).nfopathandfilename)
                            newmovie.fullmoviebody.title = cleanfilename(tempstring)
                        Else
                            newmovie.fullmoviebody.title = cleanfilename(getlastfolder(newmovielist(f).nfopathandfilename))
                        End If
                    End If
                    stage = 2
                    'stage 2 = get movie actors
                    actorlist = scraperfunction.getimdbactors(userprefs.imdbmirror, newmovie.fullmoviebody.imdbid, newmovie.fullmoviebody.title, userprefs.maxactors)
                    Try
                        thumbstring.LoadXml(actorlist)
                        thisresult = Nothing
                        Dim actorcount As Integer = 0
                        For Each thisresult In thumbstring("actorlist")
                            Select Case thisresult.Name
                                Case "actor"
                                    If actorcount > userprefs.maxactors Then
                                        Exit For
                                    End If
                                    actorcount += 1
                                    Dim newactor As New movieactors
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
                                                    If detail.InnerText <> "" And userprefs.actorseasy = True Then
                                                        Dim workingpath As String = newmovielist(f).nfopathandfilename.Replace(IO.Path.GetFileName(newmovielist(f).nfopathandfilename), "")
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
                                                                    Dim req As HttpWebRequest = req.Create(thumburl)
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
                                                    If userprefs.actorsave = True And detail.InnerText <> "" And userprefs.actorseasy = False Then
                                                        Dim workingpath As String = ""
                                                        Dim networkpath As String = userprefs.actorsavepath
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
                                                                Dim req As HttpWebRequest = req.Create(thumburl)
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
                                                            newactor.actorthumb = IO.Path.Combine(userprefs.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                            If userprefs.actornetworkpath.IndexOf("/") <> -1 Then
                                                                newactor.actorthumb = userprefs.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                            Else
                                                                newactor.actorthumb = userprefs.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
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

                        Console.WriteLine("Actors scraped OK")
                        While newmovie.listactors.Count > userprefs.maxactors
                            newmovie.listactors.RemoveAt(newmovie.listactors.Count - 1)
                        End While
                        For Each actor In newmovie.listactors
                            Dim actornew As New actordatabase
                            actornew.actorname = actor.actorname
                            actornew.movieid = newmovie.fullmoviebody.imdbid
                            actordb.Add(actornew)
                        Next
                    Catch ex As Exception
                        Console.WriteLine("Error with " & newmovielist(f).nfopathandfilename)
                        Console.WriteLine("An error was encountered at stage 2, Downloading Actors")
                        Console.WriteLine(ex.Message.ToString)
                        errorcounter += 1
                        newmovie.listactors.Clear()
                    End Try


                    stage = 3
                    'stage 3 = get movie trailer
                    Try
                        If userprefs.gettrailer = True Then
                            trailer = scraperfunction.gettrailerurl(newmovie.fullmoviebody.imdbid, userprefs.imdbmirror)
                            If trailer <> Nothing Then
                                newmovie.fullmoviebody.trailer = trailer
                                Console.WriteLine("Trailer URL Scraped OK")
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    'stage 4 = get movie thumblist(for nfo)
                   
                    If userprefs.nfoposterscraper <> 0 Then
                        Dim thumbs As String = ""
                        If userprefs.nfoposterscraper = 1 Or userprefs.nfoposterscraper = 3 Or userprefs.nfoposterscraper = 5 Or userprefs.nfoposterscraper = 7 Or userprefs.nfoposterscraper = 9 Or userprefs.nfoposterscraper = 11 Or userprefs.nfoposterscraper = 13 Or userprefs.nfoposterscraper = 15 Then
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


                        If userprefs.nfoposterscraper = 2 Or userprefs.nfoposterscraper = 3 Or userprefs.nfoposterscraper = 6 Or userprefs.nfoposterscraper = 7 Or userprefs.nfoposterscraper = 10 Or userprefs.nfoposterscraper = 11 Or userprefs.nfoposterscraper = 14 Or userprefs.nfoposterscraper = 15 Then
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

                        If userprefs.nfoposterscraper = 4 Or userprefs.nfoposterscraper = 5 Or userprefs.nfoposterscraper = 6 Or userprefs.nfoposterscraper = 7 Or userprefs.nfoposterscraper = 12 Or userprefs.nfoposterscraper = 13 Or userprefs.nfoposterscraper = 14 Or userprefs.nfoposterscraper = 15 Then
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

                        If userprefs.nfoposterscraper = 8 Or userprefs.nfoposterscraper = 9 Or userprefs.nfoposterscraper = 10 Or userprefs.nfoposterscraper = 11 Or userprefs.nfoposterscraper = 12 Or userprefs.nfoposterscraper = 13 Or userprefs.nfoposterscraper = 14 Or userprefs.nfoposterscraper = 15 Then
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
                            Console.WriteLine("Poster URLs Scraped OK")
                        Catch ex As Exception
                            Console.WriteLine("Error with " & newmovielist(f).nfopathandfilename)
                            Console.WriteLine("An error was encountered at stage 4, Downloading poster list for nfo file")
                            Console.WriteLine(ex.Message.ToString)
                            errorcounter += 1
                            newmovie.listthumbs.Clear()
                        End Try
                    End If
                    stage = 5
                    'stage 5 = get hd tags
                    Try
                        Dim tempsa As String = IO.Path.GetFileName(newmovielist(f).mediapathandfilename)
                        Dim tempsb As String = newmovielist(f).mediapathandfilename.Replace(IO.Path.GetFileName(newmovielist(f).mediapathandfilename), "")
                        tempsb = IO.Path.Combine(tempsb, "tempoffline.ttt")
                        If Not IO.File.Exists(tempsb) Then

                            newmovie.filedetails = get_hdtags(newmovielist(f).mediapathandfilename)
                            If newmovie.filedetails.filedetails_video.duration <> Nothing Then
                                Try
                                    '1h 24mn 48s 546ms
                                    Dim hours As Integer
                                    Dim minutes As Integer
                                    tempstring = newmovie.filedetails.filedetails_video.duration
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
                                    If minutes = 0 Then
                                        If tempstring.IndexOf("min") <> -1 Then
                                            tempstring = tempstring.Replace("min", "")
                                            tempstring = tempstring.Replace(" ", "")
                                            If IsNumeric(tempstring) Then
                                                minutes = Convert.ToInt32(tempstring)
                                            End If
                                        End If
                                    End If
                                    newmovie.fullmoviebody.runtime = minutes.ToString & " min"
                                    Console.WriteLine("HD Tags Added OK")
                                Catch ex As Exception
                                    Console.WriteLine("Error getting HD Tags:- " & ex.Message.ToString)
                                End Try
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



                    Dim movietoadd As New combolist
                    movietoadd.fullpathandfilename = nfopath
                    movietoadd.filename = IO.Path.GetFileName(newmovielist(f).nfopathandfilename)
                    movietoadd.foldername = getlastfolder(newmovielist(f).nfopathandfilename)
                    movietoadd.title = newmovie.fullmoviebody.title
                    movietoadd.sortorder = newmovie.fullmoviebody.sortorder
                    movietoadd.runtime = newmovie.fullmoviebody.runtime
                    If newmovie.fullmoviebody.title <> Nothing Then
                        If newmovie.fullmoviebody.year <> Nothing Then
                            movietoadd.titleandyear = newmovie.fullmoviebody.title & " (" & newmovie.fullmoviebody.year & ")"
                        Else
                            movietoadd.titleandyear = newmovie.fullmoviebody.title & " (0000)"
                        End If
                    Else
                        movietoadd.titleandyear = "Unknown (0000)"
                    End If
                    movietoadd.outline = newmovie.fullmoviebody.outline
                    movietoadd.year = newmovie.fullmoviebody.year




                    Dim filecreation As New FileInfo(newmovielist(f).nfopathandfilename)
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
                    If userprefs.scrapemovieposters = True And userprefs.overwritethumbs = True Or IO.File.Exists(getposterpath(newmovielist(f).nfopathandfilename)) = False Then
                        Try
                            Select Case userprefs.moviethumbpriority(0)
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
                            If moviethumburl = "na" Then
                                Select Case userprefs.moviethumbpriority(1)
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

                            If moviethumburl = "na" Then
                                Select Case userprefs.moviethumbpriority(2)
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
                            If moviethumburl = "na" Then
                                Select Case userprefs.moviethumbpriority(3)
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

                            If moviethumburl <> "" And moviethumburl <> "na" Then
                                Dim newmoviethumbpath As String = getposterpath(newmovielist(f).nfopathandfilename)
                                Try
                                    Dim buffer(4000000) As Byte
                                    Dim size As Integer = 0
                                    Dim bytesRead As Integer = 0
                                    Dim thumburl As String = moviethumburl
                                    Dim req As HttpWebRequest = req.Create(thumburl)
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
                                    Console.WriteLine("Poster scraped and saved OK")

                                    Dim temppath As String = newmoviethumbpath.Replace(System.IO.Path.GetFileName(newmoviethumbpath), "folder.jpg")
                                    If userprefs.createfolderjpg = True Then
                                        If userprefs.overwritethumbs = True Or System.IO.File.Exists(temppath) = False Then
                                            Console.WriteLine("Saving folder.jpg To Path :- " & temppath)
                                            Dim fstrm2 As New FileStream(temppath, FileMode.OpenOrCreate, FileAccess.Write)
                                            fstrm2.Write(buffer, 0, bytesRead)
                                            contents.Close()
                                            fstrm2.Close()
                                            Console.WriteLine("Poster also saved as ""folder.jpg"" OK")
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
                    If userprefs.overwritethumbs = True Or userprefs.overwritethumbs = False And IO.File.Exists(getfanartpath(newmovielist(f).nfopathandfilename)) = False Then
                        If userprefs.savefanart = False Then
                            'Console.writeline("Fanart Not Downloaded - Disabled in preferences, use browser to find and add Fanart")
                        Else
                            Try

                                Dim moviefanartexists As Boolean
                                Dim fanarturlpath As String = getfanartpath(newmovielist(f).nfopathandfilename)

                                moviethumburl = ""
                                moviefanartexists = System.IO.File.Exists(fanarturlpath)
                                If moviefanartexists = False Or userprefs.overwritethumbs = True Then

                                    Dim temp As String = newmovie.fullmoviebody.imdbid

                                    Dim fanarturl As String = "http://api.themoviedb.org/2.0/Movie.imdbLookup?imdb_id=" & temp & "&api_key=3f026194412846e530a208cf8a39e9cb"
                                    Dim apple2(2000) As String
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
                                            If apple2(g).IndexOf("<backdrop size=""original"">") <> -1 Then
                                                apple2(g) = apple2(g).Replace("<backdrop size=""original"">", "")
                                                apple2(g) = apple2(g).Replace("</backdrop>", "")
                                                apple2(g) = apple2(g).Replace("  ", "")
                                                apple2(g) = apple2(g).Trim
                                                If apple2(g).ToLower.IndexOf("http") <> -1 And apple2(g).ToLower.IndexOf(".jpg") <> -1 Or apple2(g).IndexOf(".jpeg") <> -1 Or apple2(g).IndexOf(".png") <> -1 Then
                                                    moviethumburl = apple2(g)
                                                    fanartfound = True
                                                End If
                                                Exit For
                                            End If
                                        Next
                                        If fanartfound = False Then moviethumburl = ""
                                    Catch
                                    End Try

                                    If moviethumburl <> "" Then
                                        'Console.writeline("Fanart URL is " & fanarturl)
                                        Console.WriteLine("Saving Fanart As :- " & fanarturlpath)

                                        'need to resize thumbs

                                        Try
                                            Dim buffer(8000000) As Byte
                                            Dim size As Integer = 0
                                            Dim bytesRead As Integer = 0

                                            Dim thumburl As String = moviethumburl
                                            Dim req As HttpWebRequest = req.Create(thumburl)
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



                                            If userprefs.resizefanart = 1 Then
                                                bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                                                Console.WriteLine("Fanart not resized")
                                            ElseIf userprefs.resizefanart = 2 Then
                                                If bmp.Width > 1280 Or bmp.Height > 720 Then
                                                    Dim bm_source As New Bitmap(bmp)
                                                    Dim bm_dest As New Bitmap(1280, 720)
                                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                                    gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                                                    bm_dest.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                    Console.WriteLine("Farart Resized to 1280x720")
                                                Else
                                                    Console.WriteLine("Fanart not resized, already =< required size")
                                                    bmp.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                End If
                                            ElseIf userprefs.resizefanart = 3 Then
                                                If bmp.Width > 960 Or bmp.Height > 540 Then
                                                    Dim bm_source As New Bitmap(bmp)
                                                    Dim bm_dest As New Bitmap(960, 540)
                                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                                    gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                                                    bm_dest.Save(fanarturlpath, Imaging.ImageFormat.Jpeg)
                                                    Console.WriteLine("Farart Resized to 960x540")
                                                Else
                                                    Console.WriteLine("Fanart not resized, already =< required size")
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
                    Dim fanartexists As Boolean = IO.File.Exists(getfanartpath(movietoadd.fullpathandfilename))
                    Dim posterexists As Boolean = IO.File.Exists(getposterpath(movietoadd.fullpathandfilename))
                    If fanartexists = False Then
                        completebyte1 += 1
                    End If
                    If posterexists = False Then
                        completebyte1 += 2
                    End If
                    movietoadd.missingdata1 = completebyte1
                    fullmovielist.Add(movietoadd)
                End If
                Console.WriteLine("Movie added to list")
            End If
            Console.WriteLine()
            Console.WriteLine()
            Console.WriteLine()

        Next

    End Sub

    Private Sub offlinedvd(ByVal nfopath As String, ByVal title As String, ByVal mediapath As String)
        Dim tempint2 As Integer = 2097152
        Dim SizeOfFile As Integer = FileLen(mediapath)
        If SizeOfFile > tempint2 Then
            Exit Sub
        End If
        Try
            Dim fanartpath As String = ""
            If IO.File.Exists(getfanartpath(nfopath)) Then
                fanartpath = getfanartpath(nfopath)
            Else
                fanartpath = defaultofflineart
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
                    path = applicationpath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    path = applicationpath & "\Settings\0" & f.ToString & ".jpg"
                End If
                curImage.Save(path, Drawing.Imaging.ImageFormat.Jpeg)
            Next

            Dim myProcess As Process = New Process
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.FileName = applicationpath & "\ffmpeg.exe"
            Dim proc_arguments As String = "-r 1 -b 1800 -qmax 6 -i """ & applicationpath & "\Settings\%03d.jpg"" -vcodec msmpeg4v2 """ & mediapath & """"
            myProcess.StartInfo.Arguments = proc_arguments
            myProcess.Start()
            myProcess.WaitForExit()

            For f = 1 To 16
                Dim tempstring4 As String
                If f < 10 Then
                    tempstring4 = applicationpath & "\Settings\00" & f.ToString & ".jpg"
                Else
                    tempstring4 = applicationpath & "\Settings\0" & f.ToString & ".jpg"
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
    Public Function getimdbactors(ByVal imdbid As String, ByVal mirror As String)

        Dim tactors(1000, 2)
        Dim tactorcount As Integer = 0

        tvdburl = mirror & "title/" & imdbid & "/fullcredits#cast"
        loadwebpage(tvdburl)
        Try

            If tvdbwebsource(1) <> "404" Then
                For f = 1 To tvfblinecount
                    If tvdbwebsource(f).IndexOf("Episode  Cast</a>") <> -1 Then
                        tvdbwebsource(f) = tvdbwebsource(f).Substring(tvdbwebsource(f).IndexOf("Cast</a>"), tvdbwebsource(f).Length - tvdbwebsource(f).IndexOf("Cast</a>"))
                        If tvdbwebsource(f).IndexOf("<tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>") <> -1 Then
                            tvdbwebsource(f) = tvdbwebsource(f).Replace("</td></tr> <tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>", "</td></tr><tr class")
                        End If
                        Dim tvtempstring As String
                        Dim tvtempint As Integer = 0
                        tvtempstring = tvdbwebsource(f)
                        Do Until tvtempstring.IndexOf("</td></tr><tr class") = -1
                            tvtempint += 1
                            tactors(tvtempint, 0) = tvtempstring.Substring(0, tvtempstring.IndexOf("</td></tr><tr class") + 19)
                            tvtempstring = tvtempstring.Replace(tactors(tvtempint, 0), "")
                        Loop
                        tvtempint += 1
                        tactors(tvtempint, 0) = tvtempstring
                        tactorcount = tvtempint
                        For g = 1 To tactorcount
                            If tactors(g, 0).IndexOf("http://resume.imdb.com") <> -1 Then tactors(g, 0) = tactors(g, 0).Replace("http://resume.imdb.com", "")
                            If tactors(g, 0).IndexOf("http://i.media-imdb.com/images/tn15/addtiny.gif") <> -1 Then tactors(g, 0) = tactors(g, 0).Replace("http://i.media-imdb.com/images/tn15/addtiny.gif", "")
                            If tactors(g, 0).IndexOf("</td></tr></table>") <> -1 Then
                                tvtempint = tactors(g, 0).IndexOf("</td></tr></table>")
                                tvtempstring = tactors(g, 0).Substring(tvtempint, tactors(g, 0).Length - tvtempint)
                                tactors(g, 0) = tactors(g, 0).Replace(tvtempstring, "</td></tr><tr class")
                            End If
                            If tactors(g, 0).indexof("http") <> -1 Then
                                Dim tvfirst As Integer
                                Dim tvlast As Integer
                                tvfirst = tactors(g, 0).IndexOf("http")
                                tvlast = tactors(g, 0).IndexOf("._V1._")
                                tactors(g, 2) = tactors(g, 0).Substring(tvfirst, tvlast - tvfirst + 6)
                                tactors(g, 2) = tactors(g, 2).Replace("._V1._", "._V1._SY200_SX300_.jpg")
                            End If

                            If tactors(g, 0).IndexOf("a href=""/character") <> -1 Then
                                tactors(g, 1) = tactors(g, 0).Substring(tactors(g, 0).IndexOf("a href=""/character") + 19, tactors(g, 0).IndexOf("</td></tr><tr class") - tactors(g, 0).IndexOf("a href=""/character") - 19)
                                If tactors(g, 1).IndexOf("</a>") <> -1 Then
                                    tactors(g, 1) = tactors(g, 1).Substring(12, tactors(g, 1).IndexOf("</a>") - 12)
                                ElseIf tactors(g, 1).IndexOf("</a>") = -1 Then
                                    tactors(g, 1) = tactors(g, 1).Substring(12, tactors(g, 1).Length - 12)
                                End If
                                tvtempstring = tactors(g, 0).Substring(tactors(g, 0).IndexOf("a href=""/character"), tactors(g, 0).Length - tactors(g, 0).IndexOf("a href=""/character"))
                                tactors(g, 0) = tactors(g, 0).Replace(tvtempstring, "")
                                Dim tvfirst As Integer
                                Dim tvlast As Integer
                                tvfirst = tactors(g, 0).IndexOf("/"">")
                                tvlast = tactors(g, 0).IndexOf("</a></td>")
                                tactors(g, 0) = tactors(g, 0).Substring(tvfirst + 3, (tvlast) - (tvfirst + 3))
                            ElseIf tactors(g, 0).IndexOf("a href=""/character") = -1 Then
                                Dim tvfirst As Integer
                                Dim tvlast As Integer
                                tvfirst = tactors(g, 0).IndexOf("<td class=""char"">")
                                tvlast = tactors(g, 0).IndexOf("</td></tr><tr class")
                                tactors(g, 1) = tactors(g, 0).Substring(tvfirst + 17, tvlast - tvfirst - 17)
                                tvfirst = tactors(g, 0).IndexOf("/"">")
                                tvlast = tactors(g, 0).IndexOf("</a></td>")
                                tactors(g, 0) = tactors(g, 0).Substring(tvfirst + 3, (tvlast) - (tvfirst + 3))
                            End If
                        Next
                    End If
                Next
            Else
                tactors(0, 0) = "404"
            End If
        Catch ex As Exception
            tactors(0, 0) = "404"
        End Try







        Return tactors
    End Function
    Public Function loadwebpage(ByVal tvdburl As String)

        Try
            Try
                ReDim tvdbwebsource(10000)
                tvfblinecount = 0


                Dim wrGETURL As WebRequest
                wrGETURL = WebRequest.Create(tvdburl)
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
            Catch
                tvfblinecount = 1
                tvdbwebsource(1) = "404"
            End Try
            Return tvdbwebsource
        Catch
        End Try
    End Function
    Public Function tmdbthumb(ByVal posterimdbid As String)
        Try
            Dim newobject2 As New tmdb_posters.Class1
            Dim thumburl As String
            Dim xmllist As String
            Dim ok As Boolean = False
            Try
                xmllist = newobject2.gettmdbposters_newapi(posterimdbid)
                Dim bannerslist As New XmlDocument
                bannerslist.LoadXml(xmllist)
                For Each item In bannerslist("tmdb_posterlist")
                    Select Case item.name
                        Case "poster"
                            For Each img In item
                                If img.childnodes(0).innertext = "original" Then
                                    thumburl = img.childnodes(1).innertext
                                    ok = True
                                    Exit For
                                End If
                            Next
                            If ok = True Then Exit For
                    End Select
                Next
                Return thumburl
            Catch ex As Exception
                Thread.Sleep(1)
            End Try


        Catch ex As Exception
        End Try
    End Function
    Public Function mpdbthumb(ByVal posterimdbid As String)
        Try
            Dim first As String
            Dim last As String

            Dim allok As Boolean = False
            Dim thumburl As String = "na"
            Dim temp As String = posterimdbid
            temp = temp.Replace("tt", "")
            Dim fanarturl As String = "http://www.movieposterdb.com/movie/" & temp


            Dim apple2(2000) As String
            Dim fanartlinecount As Integer = 0
            'Try
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


            For f = 2 To fanartlinecount
                If apple2(f).IndexOf("<title>") <> -1 Then
                    If apple2(f).IndexOf("<title>MoviePosterDB.com - Internet Movie Poster DataBase</title>") <> -1 Then
                        allok = False
                        Exit For
                    Else
                        allok = True
                        Exit For
                    End If
                End If
            Next


            If allok = True Then
                allok = False
                For f = 2 To fanartlinecount
                    If apple2(f).IndexOf("<img src=""http://www.movieposterdb.com/posters/") <> -1 Then
                        first = apple2(f).IndexOf("http")
                        last = apple2(f).IndexOf("jpg")
                        thumburl = apple2(f).Substring(first, (last + 3) - first)
                        If thumburl.IndexOf("t_") <> -1 Then
                            thumburl = thumburl.Replace("t_", "l_")
                            Exit For
                        End If
                        If thumburl.IndexOf("s_") <> -1 Then
                            thumburl = thumburl.Replace("s_", "l_")
                            Exit For
                        End If
                    End If
                Next
            End If

            If thumburl.IndexOf("http") = 0 And thumburl.IndexOf(".jpg") = thumburl.Length - 4 Then
                allok = True
            Else
                thumburl = "na"
            End If



            'Catch
            'End Try
            Return thumburl
        Catch
        End Try
    End Function
    Public Function impathumb(ByVal title As String, ByVal year As String)
        Try
            Dim tempstring As String
            Dim tempint As Integer
            year = year.Replace("<year>", "")
            year = year.Replace("</year>", "")
            year = year.Replace("    ", "")
            Dim allok As Boolean = False
            Dim thumburl As String = "na"
            Dim temp As String = title

            Dim fanarturl As String = "http://www.google.com/custom?hl=en&client=pub-6811780361519631&cof=FORID%3A1%3BGL%3A1%3BLBGC%3A000000%3BBGC%3A%23000000%3BT%3A%23cccccc%3BLC%3A%2333cc33%3BVLC%3A%2333ff33%3BGALT%3A%2333CC33%3BGFNT%3A%23ffffff%3BGIMP%3A%23ffffff%3B&domains=www.impawards.com&ie=ISO-8859-1&oe=ISO-8859-1&q="

            temp = temp.ToLower
            temp = temp.Replace(" ", "+")
            temp = temp.Replace("&", "%26")
            temp = temp.Replace("À", "%c0")
            temp = temp.Replace("Á", "%c1")
            temp = temp.Replace("Â", "%c2")
            temp = temp.Replace("Ã", "%c3")
            temp = temp.Replace("Ä", "%c4")
            temp = temp.Replace("Å", "%c5")
            temp = temp.Replace("Æ", "%c6")
            temp = temp.Replace("Ç", "%c7")
            temp = temp.Replace("È", "%c8")
            temp = temp.Replace("É", "%c9")
            temp = temp.Replace("Ê", "%ca")
            temp = temp.Replace("Ë", "%cb")
            temp = temp.Replace("Ì", "%cc")
            temp = temp.Replace("Í", "%cd")
            temp = temp.Replace("Î", "%ce")
            temp = temp.Replace("Ï", "%cf")
            temp = temp.Replace("Ð", "%d0")
            temp = temp.Replace("Ñ", "%d1")
            temp = temp.Replace("Ò", "%d2")
            temp = temp.Replace("Ó", "%d3")
            temp = temp.Replace("Ô", "%d4")
            temp = temp.Replace("Õ", "%d5")
            temp = temp.Replace("Ö", "%d6")
            temp = temp.Replace("Ø", "%d8")
            temp = temp.Replace("Ù", "%d9")
            temp = temp.Replace("Ú", "%da")
            temp = temp.Replace("Û", "%db")
            temp = temp.Replace("Ü", "%dc")
            temp = temp.Replace("Ý", "%dd")
            temp = temp.Replace("Þ", "%de")
            temp = temp.Replace("ß", "%df")
            temp = temp.Replace("à", "%e0")
            temp = temp.Replace("á", "%e1")
            temp = temp.Replace("â", "%e2")
            temp = temp.Replace("ã", "%e3")
            temp = temp.Replace("ä", "%e4")
            temp = temp.Replace("å", "%e5")
            temp = temp.Replace("æ", "%e6")
            temp = temp.Replace("ç", "%e7")
            temp = temp.Replace("è", "%e8")
            temp = temp.Replace("é", "%e9")
            temp = temp.Replace("ê", "%ea")
            temp = temp.Replace("ë", "%eb")
            temp = temp.Replace("ì", "%ec")
            temp = temp.Replace("í", "%ed")
            temp = temp.Replace("î", "%ee")
            temp = temp.Replace("ï", "%ef")
            temp = temp.Replace("ð", "%f0")
            temp = temp.Replace("ñ", "%f1")
            temp = temp.Replace("ò", "%f2")
            temp = temp.Replace("ó", "%f3")
            temp = temp.Replace("ô", "%f4")
            temp = temp.Replace("õ", "%f5")
            temp = temp.Replace("ö", "%f6")
            temp = temp.Replace("÷", "%f7")
            temp = temp.Replace("ø", "%f8")
            temp = temp.Replace("ù", "%f9")
            temp = temp.Replace("ú", "%fa")
            temp = temp.Replace("û", "%fb")
            temp = temp.Replace("ü", "%fc")
            temp = temp.Replace("ý", "%fd")
            temp = temp.Replace("þ", "%fe")
            temp = temp.Replace("ÿ", "%ff")
            temp = temp.Replace(" ", "+")
            temp = temp.Replace("&", "%26")
            fanarturl = fanarturl & temp & "+" & year
            fanarturl = fanarturl & "&sitesearch=www.impawards.com"
            'Try
            Dim apple2(2000) As String
            Dim fanartlinecount As Integer = 0
            Dim wrGETURL2 As WebRequest
            wrGETURL2 = WebRequest.Create(fanarturl)
            Dim myProxy2 As New WebProxy("myproxy", 80)
            myProxy2.BypassProxyOnLocal = True
            Dim objStream2 As Stream
            objStream2 = wrGETURL2.GetResponse.GetResponseStream()
            Dim objReader2 As New StreamReader(objStream2)
            Dim sLine2 As String = ""
            fanartlinecount = 0

            Do While Not sLine2 Is Nothing
                fanartlinecount += 1
                sLine2 = objReader2.ReadLine
                apple2(fanartlinecount) = sLine2
            Loop

            fanartlinecount -= 1
            Dim xtralge As Boolean = False
            For f = 1 To fanartlinecount
                If apple2(f).IndexOf("http://www.impawards.com/") <> -1 Then
                    Dim first As Integer = apple2(f).IndexOf("http://www.impawards.com/")
                    apple2(f) = apple2(f).Substring(first, apple2(f).Length - first)
                    fanarturl = apple2(f).Substring(0, apple2(f).IndexOf("html") + 4)
                    tempstring = fanarturl
                    tempstring = tempstring.Replace("http://", "")
                    tempint = tempstring.LastIndexOf("/")
                    If tempint - 5 = tempstring.IndexOf("/") Then
                        allok = True
                    Else

                        fanarturl = "http://www.google.com/custom?hl=en&client=pub-6811780361519631&cof=FORID%3A1%3BGL%3A1%3BLBGC%3A000000%3BBGC%3A%23000000%3BT%3A%23cccccc%3BLC%3A%2333cc33%3BVLC%3A%2333ff33%3BGALT%3A%2333CC33%3BGFNT%3A%23ffffff%3BGIMP%3A%23ffffff%3B&domains=www.impawards.com&ie=ISO-8859-1&oe=ISO-8859-1&q="
                        fanarturl = fanarturl & temp
                        fanarturl = fanarturl & "&sitesearch=www.impawards.com"
                        ReDim apple2(2000)
                        fanartlinecount = 0
                        Dim wrGETURL4 As WebRequest
                        wrGETURL4 = WebRequest.Create(fanarturl)
                        Dim myProxy4 As New WebProxy("myproxy", 80)
                        myProxy4.BypassProxyOnLocal = True
                        Dim objStream4 As Stream
                        objStream4 = wrGETURL4.GetResponse.GetResponseStream()
                        Dim objReader4 As New StreamReader(objStream4)
                        Dim sLine4 As String = ""
                        fanartlinecount = 0

                        Do While Not sLine4 Is Nothing
                            fanartlinecount += 1
                            sLine4 = objReader4.ReadLine
                            apple2(fanartlinecount) = sLine4
                        Loop
                        fanartlinecount -= 1
                        xtralge = False
                        For g = 1 To fanartlinecount
                            If apple2(f).IndexOf("http://www.impawards.com/") <> -1 Then
                                first = apple2(f).IndexOf("http://www.impawards.com/")
                                apple2(f) = apple2(f).Substring(first, apple2(f).Length - first)
                                fanarturl = apple2(f).Substring(0, apple2(f).IndexOf("html") + 4)
                                tempstring = fanarturl
                                tempstring = tempstring.Replace("http://", "")
                                tempint = tempstring.LastIndexOf("/")
                                If tempint - 5 = tempstring.IndexOf("/") Then
                                    allok = True
                                Else
                                    allok = False
                                End If
                            End If
                        Next

                    End If
                    Exit For
                End If
                If apple2(f).IndexOf("xlg.html") <> -1 Then xtralge = True
            Next

            ReDim apple2(2000)
            fanartlinecount = 0
            Dim wrGETURL3 As WebRequest

            wrGETURL3 = WebRequest.Create(fanarturl)
            Dim myProxy3 As New WebProxy("myproxy", 80)
            myProxy3.BypassProxyOnLocal = True
            Dim objStream3 As Stream
            objStream3 = wrGETURL3.GetResponse.GetResponseStream()
            Dim objReader3 As New StreamReader(objStream3)
            Dim sLine3 As String = ""

            Do While Not sLine3 Is Nothing
                fanartlinecount += 1
                sLine3 = objReader3.ReadLine
                apple2(fanartlinecount) = sLine3
            Loop
            fanartlinecount -= 1
            For f = 1 To fanartlinecount
                If apple2(f).IndexOf("xlg.html") <> -1 Then
                    xtralge = True
                    Exit For
                End If
            Next


            If allok = True Then
                Dim tempstring3 As String
                Dim tempstring4 As String
                tempstring3 = fanarturl.Substring(0, fanarturl.LastIndexOf("/") + 1)
                tempstring4 = fanarturl.Substring(fanarturl.LastIndexOf("/") + 1, fanarturl.Length - fanarturl.LastIndexOf("/") - 1)
                fanarturl = tempstring3 & "posters/" & tempstring4
                If xtralge = False Then
                    thumburl = fanarturl.Replace(".html", ".jpg")
                ElseIf xtralge = True Then
                    thumburl = fanarturl.Replace(".html", "_xlg.jpg")
                End If
            Else
                thumburl = "na"
            End If
            If thumburl.IndexOf("art_machine.jpg") <> -1 Then thumburl = "na"


            'Catch
            '    thumburl = "na"
            'End Try

            Return thumburl
        Catch
        End Try
    End Function
    Public Function imdbthumb(ByVal posterimdbid As String)
        Dim thumburl As String = "na"
        Try
            Dim allok As Boolean = False


            Dim temp As String = posterimdbid
            Dim fanarturl As String = "http://www.imdb.com/title/" & temp

            Dim apple2(2000) As String
            Dim fanartlinecount As Integer = 0

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
            For f = 1 To fanartlinecount
                If apple2(f).IndexOf("<div class=""photo"">") <> -1 Then
                    thumburl = apple2(f + 1)
                    thumburl = thumburl.Substring(thumburl.IndexOf("http"), thumburl.IndexOf("._V1") - thumburl.IndexOf("http"))
                    thumburl = thumburl & "._V1._SX1500_SY1000_.jpg"
                End If
            Next

            If thumburl.IndexOf("http") = -1 Or thumburl.IndexOf(".jpg") = -1 Then thumburl = "na"

            Return thumburl
        Catch
            thumburl = "na"
        End Try

    End Function

    Public Sub savemovienfo(ByVal filenameandpath As String, ByVal movietosave As fullmoviedetails, Optional ByVal overwrite As Boolean = True)
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
                Dim root As XmlElement
                Dim child As XmlElement
                Dim actorchild As XmlElement
                Dim filedetailschild As XmlElement
                Dim filedetailschildchild As XmlElement
                Dim anotherchild As XmlElement

                root = doc.CreateElement("movie")
                stage = 3
                If userprefs.enablehdtags = True Then
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
                            If movietosave.filedetails.filedetails_video.width <> "" Then
                                filedetailschildchild = doc.CreateElement("width")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.width
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    Try
                        If movietosave.filedetails.filedetails_video.height <> Nothing Then
                            If movietosave.filedetails.filedetails_video.height <> "" Then
                                filedetailschildchild = doc.CreateElement("height")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.height
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.aspect <> Nothing Then
                            If movietosave.filedetails.filedetails_video.aspect <> "" Then
                                filedetailschildchild = doc.CreateElement("aspect")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.aspect
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 5
                    Try
                        If movietosave.filedetails.filedetails_video.codec <> Nothing Then
                            If movietosave.filedetails.filedetails_video.codec <> "" Then
                                filedetailschildchild = doc.CreateElement("codec")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codec
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 6
                    Try
                        If movietosave.filedetails.filedetails_video.formatinfo <> Nothing Then
                            If movietosave.filedetails.filedetails_video.formatinfo <> "" Then
                                filedetailschildchild = doc.CreateElement("format")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.formatinfo
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 7
                    Try
                        If movietosave.filedetails.filedetails_video.duration <> Nothing Then
                            If movietosave.filedetails.filedetails_video.duration <> "" Then
                                filedetailschildchild = doc.CreateElement("duration")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.duration
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 8
                    Try
                        If movietosave.filedetails.filedetails_video.bitrate <> Nothing Then
                            If movietosave.filedetails.filedetails_video.bitrate <> "" Then
                                filedetailschildchild = doc.CreateElement("bitrate")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitrate
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 9
                    Try
                        If movietosave.filedetails.filedetails_video.bitratemode <> Nothing Then
                            If movietosave.filedetails.filedetails_video.bitratemode <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemode")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitratemode
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 10
                    Try
                        If movietosave.filedetails.filedetails_video.bitratemax <> Nothing Then
                            If movietosave.filedetails.filedetails_video.bitratemax <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemax")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitratemax
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 11
                    Try
                        If movietosave.filedetails.filedetails_video.container <> Nothing Then
                            If movietosave.filedetails.filedetails_video.container <> "" Then
                                filedetailschildchild = doc.CreateElement("container")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.container
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 12
                    Try
                        If movietosave.filedetails.filedetails_video.codecid <> Nothing Then
                            If movietosave.filedetails.filedetails_video.codecid <> "" Then
                                filedetailschildchild = doc.CreateElement("codecid")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codecid
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 13
                    Try
                        If movietosave.filedetails.filedetails_video.codecinfo <> Nothing Then
                            If movietosave.filedetails.filedetails_video.codecinfo <> "" Then
                                filedetailschildchild = doc.CreateElement("codecidinfo")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codecinfo
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 14
                    Try
                        If movietosave.filedetails.filedetails_video.scantype <> Nothing Then
                            If movietosave.filedetails.filedetails_video.scantype <> "" Then
                                filedetailschildchild = doc.CreateElement("scantype")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.scantype
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
                            If item.language <> Nothing Then
                                If item.language <> "" Then
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = item.language
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.codec <> Nothing Then
                                If item.codec <> "" Then
                                    filedetailschildchild = doc.CreateElement("codec")
                                    filedetailschildchild.InnerText = item.codec
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.channels <> Nothing Then
                                If item.channels <> "" Then
                                    filedetailschildchild = doc.CreateElement("channels")
                                    filedetailschildchild.InnerText = item.channels
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.bitrate <> Nothing Then
                                If item.bitrate <> "" Then
                                    filedetailschildchild = doc.CreateElement("bitrate")
                                    filedetailschildchild.InnerText = item.bitrate
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
                            If entry.language <> Nothing Then
                                If entry.language <> "" Then
                                    tempint += 1
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = entry.language
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
                        If movietosave.fullmoviebody.movieset <> "None" Then
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
                            Do While minutes.IndexOf("0") = 0
                                minutes = minutes.Substring(1, minutes.Length - 1)
                            Loop
                            If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And userprefs.roundminutes = True Then
                                minutes = "0" & minutes & " min"
                            ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And userprefs.roundminutes = True Then
                                minutes = "00" & minutes & " min"
                            Else
                                minutes = movietosave.fullmoviebody.runtime
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
                    child = doc.CreateElement("genre")
                    child.InnerText = movietosave.fullmoviebody.genre
                    root.AppendChild(child)
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
                    child = doc.CreateElement("id")
                    child.InnerText = movietosave.fullmoviebody.imdbid
                    root.AppendChild(child)
                Catch
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
                    Dim actorstosave As Integer = movietosave.listactors.Count
                    If actorstosave > userprefs.maxactors Then actorstosave = userprefs.maxactors
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
                                actorchild = doc.CreateElement("thumb")
                                actorchild.InnerText = movietosave.listactors(f).actorthumb
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




End Module



Public Class preferences
    Public autorenameepisodes As Boolean
    Public autoepisodescreenshot As Boolean
    Public tvshowautoquick As Boolean
    Public font As String
    Public moviesortorder As Byte
    Public moviedefaultlist As Byte
    Public startuptab As Byte
    Public tvshowrebuildlog As Boolean
    Public tvrename As Integer
    Public roundminutes As Boolean
    Public locx As Integer
    Public locy As Integer
    Public moviethumbpriority() As String
    Public certificatepriority() As String
    Public keepfoldername As Boolean
    Public startupcache As Boolean
    Public ignoretrailers As Boolean
    Public ignoreactorthumbs As Boolean
    Public maxactors As Integer
    Public maxmoviegenre As Integer
    Public enabletvhdtags As Boolean
    Public enablehdtags As Boolean
    Public renamenfofiles As Boolean
    Public checkinfofiles As Boolean
    Public disablelogfiles As Boolean
    Public fanartnotstacked As Boolean
    Public posternotstacked As Boolean
    Public scrapemovieposters As Boolean
    Public usefanart As Boolean
    Public dontdisplayposter As Boolean
    Public rarsize As Integer
    Public actorsave As Boolean
    Public actorsavepath As String
    Public actornetworkpath As String
    Public resizefanart As Integer
    Public overwritethumbs As Boolean
    Public defaulttvthumb As String
    Public imdbmirror As String
    Public backgroundcolour As String
    Public forgroundcolour As String
    Public remembersize As Boolean
    Public formheight As Integer
    Public formwidth As Integer
    Public videoplaybackmode As Integer
    Public usefoldernames As Boolean
    Public createfolderjpg As Boolean
    Public basicsavemode As Boolean
    Public startupdisplaynamemode As Integer
    Public namemode As String
    Public tvdblanguage As String
    Public tvdblanguagecode As String
    'Dim tvdbmode As String
    Public tvdbactorscrape As Integer
    Public usetransparency As Boolean
    Public transparencyvalue As Integer
    Public downloadtvseasonthumbs As Boolean
    Public maximumthumbs As Integer
    Public configpath As String
    Public startupmode As Integer
    Public disabletvlogs As Boolean
    Public savefanart As Boolean
    Public postertype As String
    Public sortorder As String
    Public videomode As Integer ' = 3
    Public selectedvideoplayer As String
    Public maximagecount As Integer
    Public lastpath As String
    Public moviescraper As Integer
    Public nfoposterscraper As Integer
    Public alwaysuseimdbid As Boolean
    Public gettrailer As Boolean
    Public externalbrowser As Boolean
    Public maximised As Boolean
    Public episodeacrorsource As String
    Public copytvactorthumbs As Boolean = False
    Public seasonall As String
    Public splt1 As Integer
    Public splt2 As Integer
    Public splt3 As Integer
    Public splt4 As Integer
    Public splt5 As Integer
    Public moviesets As New List(Of String)
    Public tvposter As Boolean
    Public tvfanart As Boolean
    Public tablesortorder As String
    Public actorseasy As Boolean
    Public tableview As New List(Of String)
    Public offlinefolders As New List(Of String)
End Class
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
Public Structure actordatabase
    Dim actorname As String
    Dim movieid As String
End Structure
Public Structure basicmovienfo
    Dim title As String
    Dim sortorder As String
    Dim movieset As String
    Dim year As String
    Dim rating As String
    Dim votes As String
    Dim outline As String
    Dim plot As String
    Dim tagline As String
    Dim runtime As String
    Dim mpaa As String
    Dim genre As String
    Dim credits As String
    Dim director As String
    Dim premiered As String
    Dim studio As String
    Dim trailer As String
    Dim playcount As String
    Dim imdbid As String
    Dim top250 As String
    Dim filename As String
    Dim thumbnails As String
    Dim fanart As String
End Structure
Public Class basictvshownfo
    Public title As String
    Public year As String
    Public fullpath As String
    Public rating As String
    Public genre As String
    Public tvdbid As String
    Public imdbid As String
    Public sortorder As String
    Public language As String
    Public titleandyear As String
    Public allepisodes As New List(Of basicepisodenfo)
    Public episodeactorsource As String
    Public status As String
    Public locked As Integer
End Class
Public Class basicepisodenfo
    Public title As String
    Public seasonno As String
    Public episodeno As String
    Public episodepath As String
    Public rating As String
    Public playcount As String
    Public tvdbid As String
    Public imdbid As String
    'Public status As String
End Class
Public Class tvshownfo
    Public path As String
    Public posterpath As String
    Public fanartpath As String
    Public title As String
    Public year As String
    Public rating As String
    Public plot As String
    Public runtime As String
    Public mpaa As String
    Public genre As String
    Public episodeguideurl As String
    Public premiered As String
    Public studio As String
    Public trailer As String
    Public sortorder As String
    Public language As String
    Public episodeactorsource As String
    Public tvshowactorsource As String
    Public imdbid As String
    Public tvdbid As String
    Public listactors As New List(Of movieactors)
    Public posters As New List(Of String)
    Public fanart As New List(Of String)
    Public status As String
    Public locked As Integer
End Class
Public Class episodeinfo
    Public title As String
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
    Public listactors As New List(Of movieactors)
    Public filedetails As New fullfiledetails
End Class
Public Structure movieactors
    Dim actorname As String
    Dim actorrole As String
    Dim actorthumb As String
End Structure
Public Class fullmoviedetails
    Public fileinfo As New filedetails
    Public fullmoviebody As New basicmovienfo
    Public alternativetitles As New List(Of String)
    Public listactors As New List(Of movieactors)
    Public listthumbs As New List(Of String)
    Public filedetails As New fullfiledetails
End Class
Public Class fullfiledetails
    Public filedetails_video As medianfo_video
    Public filedetails_audio As New List(Of medianfo_audio)
    Public filedetails_subtitles As New List(Of medianfo_subtitles)
End Class
Public Structure filedetails
    Dim fullpathandfilename As String
    Dim path As String
    Dim filename As String
    Dim foldername As String
    Dim fanartpath As String
    Dim posterpath As String
    Dim trailerpath As String
    Dim createdate As String
End Structure
Public Structure newmovie
    Dim nfopathandfilename As String
    Dim nfopath As String
    Dim title As String
    Dim mediapathandfilename As String
End Structure
Public Structure combolist
    Dim fullpathandfilename As String
    Dim movieset As String
    Dim filename As String
    Dim foldername As String
    Dim title As String
    Dim titleandyear As String
    Dim year As String
    Dim filedate As String
    Dim id As String
    Dim rating As String
    Dim top250 As String
    Dim genre As String
    Dim playcount As String
    Dim sortorder As String
    Dim outline As String
    Dim runtime As String
    Dim createdate As String
    Dim missingdata1 As Byte
End Structure
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
Public Structure listofprofiles
    Dim moviecache As String
    Dim tvcache As String
    Dim actorcache As String
    Dim profilename As String
    Dim regexlist As String
    Dim filters As String
    Dim config As String
End Structure
Public Class profiles
    Public startupprofile As String
    Public defaultprofile As String
    Public workingprofilename As String
    Public profilelist As New List(Of listofprofiles)
End Class


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

