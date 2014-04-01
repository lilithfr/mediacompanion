Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Xml

Public Class ucMusicVideo
    'Dim nfo As New WorkingWithNfoFiles
    Public Shared musicVideoList As New List(Of FullMovieDetails)
    Dim movieGraphicInfo As New GraphicInfo
    Public cropimage As Bitmap
    Dim rescraping As Boolean = False

    Dim workingMusicVideo As New FullMovieDetails 'Music_Video_Class

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For Each item In Preferences.MVidFolders
            lstBoxFolders.Items.Add(item)
        Next
        txtScreenshotTime.Text = "10"
    End Sub


    Private Sub btnBrowseFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFolders.Click
        Dim allok As Boolean = True
        Dim theFolderBrowser As New FolderBrowserDialog
        Dim thefoldernames As String
        theFolderBrowser.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
        theFolderBrowser.ShowNewFolderButton = True
        theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
        theFolderBrowser.SelectedPath = Preferences.lastpath
        If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            thefoldernames = (theFolderBrowser.SelectedPath)
            For Each item As Object In lstBoxFolders.Items
                If thefoldernames.ToString = item.ToString Then allok = False
            Next

            If allok = True Then
                lstBoxFolders.Items.Add(thefoldernames)
                lstBoxFolders.Refresh()
                Preferences.MVidFolders.Add(thefoldernames)
            Else
                MsgBox("        Folder Already Exists")
            End If
        End If
    End Sub

    Private Sub btnRemoveFolder_Click( sender As System.Object,  e As System.EventArgs) Handles btnRemoveFolder.Click
        Try
            While lstBoxFolders.SelectedItems.Count > 0
                Preferences.MVidFolders.Remove(lstBoxFolders.SelectedItems(0))
                lstBoxFolders.Items.Remove(lstBoxFolders.SelectedItems(0))
            End While
        Catch
        End Try
    End Sub

    Private Sub btnSearchNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNew.Click
        'Call SearchForNewMV()    ' For Testing of using Movie Scraper routines.
        Call searchFornew()
    End Sub

    Private Sub SearchForNewMV()
        Preferences.MusicVidScrape = True
        oMovies.FindNewMusicVideos()
        Preferences.MusicVidScrape = False
        Call searchFornew(False)
    End Sub

    Private Sub searchFornew(Optional ByVal scrape As Boolean = True)
        Preferences.MusicVidScrape = True
        Dim fullfolderlist As New List(Of String)
        fullfolderlist.Clear()
        fullfolderlist = listAllFolders()

        Dim FullFileList As New List(Of String)
        Dim filelist As New List(Of String)
        
        For Each folder In fullfolderlist
            For Each extension In Utilities.VideoExtensions 
                Dim dir_info As New System.IO.DirectoryInfo(folder)
                filelist.Clear()
                filelist = Listvideofiles(extension, dir_info)
                For Each item In filelist
                    FullFileList.Add(item)
                Next
            Next
        Next
        Dim newMusicVideoList As New List(Of FullMovieDetails)
        newMusicVideoList.Clear()
        For Each videopath In FullFileList

            Dim musicVideoTitle As New FullMovieDetails
            Dim scraper As New WikipediaMusivVideoScraper

            If Not IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), ".nfo")) And scrape = True Then
                Dim searchterm As String = getArtistAndTitle(videopath)
                musicVideoTitle = scraper.musicVideoScraper(videopath, searchterm)
                musicVideoTitle.fileinfo.fullpathandfilename = videopath.Replace(IO.Path.GetExtension(videopath), ".nfo")
                Try
                    If Not IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), "-poster.jpg")) And musicVideoTitle.listthumbs(0) <> "" And scrape = True Then
                        saveposter(videopath, musicVideoTitle.listthumbs(0))
                    End If
                Catch
                End Try
                Dim newstreamdetails As New FullFileDetails
                musicVideoTitle.filedetails = Preferences.Get_HdTags(videopath)

                Dim seconds As Integer = Convert.ToInt32(musicVideoTitle.filedetails.filedetails_video.DurationInSeconds.Value)
                Dim hms = TimeSpan.FromSeconds(seconds)
                Dim h = hms.Hours.ToString
                Dim m = hms.Minutes.ToString
                Dim s = hms.Seconds.ToString

                If s.Length = 1 Then s = "0" & s

                Dim runtime As String
                runtime = h & ":" & m & ":" & s
                If h = "0" Then
                    runtime = m & ":" & s
                End If
                If h = "0" And m = "0" Then
                    runtime = s
                End If
                musicVideoTitle.fullmoviebody.runtime = runtime

                WorkingWithNfoFiles.MVsaveNfo(musicVideoTitle)

                Dim alreadyloaded As Boolean = False
                For Each item In musicVideoList
                    If item.fileinfo.fullpathandfilename = videopath.Replace(IO.Path.GetExtension(videopath), ".nfo") Then
                        alreadyloaded = True
                        Exit For
                    End If
                Next
                If alreadyloaded = False Then
                    musicVideoTitle = WorkingWithNfoFiles.MVloadNfo(videopath.Replace(IO.Path.GetExtension(videopath), ".nfo"))
                    musicVideoList.Add(musicVideoTitle)

                    lstBxMainList.Items.Add(New ValueDescriptionPair(musicVideoTitle.fileinfo.fullpathandfilename, musicVideoTitle.fullmoviebody.title))
                End If


            ElseIf IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), ".nfo")) Then
                If validateMusicVideoNfo(videopath.Replace(IO.Path.GetExtension(videopath), ".nfo")) = False And scrape = True Then

                    Dim nfopath As String = videopath.Replace(IO.Path.GetExtension(videopath), ".nfo")
                    musicVideoTitle.fileinfo.fullpathandfilename = videopath
                    Dim oldnfopath As String = videopath.Replace(IO.Path.GetExtension(videopath), "_old.nfo")
                    IO.File.Move(nfopath, oldnfopath)

                    Dim searchterm As String = getArtistAndTitle(videopath)
                    musicVideoTitle = scraper.musicVideoScraper(videopath, searchterm)
                    musicVideoTitle.fileinfo.fullpathandfilename = videopath
                    Try
                        If Not IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), "-poster.jpg")) And musicVideoTitle.listthumbs(0) <> "" And scrape = True Then
                            saveposter(videopath, musicVideoTitle.listthumbs(0))
                        End If
                    Catch
                    End Try
                    Dim newstreamdetails As New FullFileDetails
                    musicVideoTitle.filedetails = Preferences.Get_HdTags(videopath)

                    Dim seconds As Integer = Convert.ToInt32(musicVideoTitle.filedetails.filedetails_video.DurationInSeconds.Value)
                    Dim hms = TimeSpan.FromSeconds(seconds)
                    Dim h = hms.Hours.ToString
                    Dim m = hms.Minutes.ToString
                    Dim s = hms.Seconds.ToString

                    If s.Length = 1 Then s = "0" & s

                    Dim runtime As String
                    runtime = h & ":" & m & ":" & s
                    If h = "0" Then
                        runtime = m & ":" & s
                    End If
                    If h = "0" And m = "0" Then
                        runtime = s
                    End If
                    musicVideoTitle.fullmoviebody.runtime = runtime
                    WorkingWithNfoFiles.MVsaveNfo(musicVideoTitle)
                    Dim alreadyloaded As Boolean = False
                    For Each item In musicVideoList
                        If item.fileinfo.fullpathandfilename = videopath.Replace(IO.Path.GetExtension(videopath), ".nfo") Then
                            alreadyloaded = True
                        End If
                    Next
                    If alreadyloaded = False Then
                        musicVideoTitle = WorkingWithNfoFiles.MVloadNfo(videopath.Replace(IO.Path.GetExtension(videopath), ".nfo"))
                        musicVideoList.Add(musicVideoTitle)

                        lstBxMainList.Items.Add(New ValueDescriptionPair(musicVideoTitle.fileinfo.fullpathandfilename, musicVideoTitle.fullmoviebody.title))
                    End If

                Else
                    Dim existingMusicVideonfo As New FullMovieDetails

                    Dim alreadyloaded As Boolean = False
                    For Each item In musicVideoList
                        If item.fileinfo.fullpathandfilename = videopath.Replace(IO.Path.GetExtension(videopath), ".nfo") Then
                            alreadyloaded = True
                        End If
                    Next
                    If alreadyloaded = False Then
                        existingMusicVideonfo = WorkingWithNfoFiles.MVloadNfo(videopath.Replace(IO.Path.GetExtension(videopath), ".nfo"))
                        existingMusicVideonfo.fileinfo.fullpathandfilename = videopath.Replace(IO.Path.GetExtension(videopath), ".nfo")
                        musicVideoList.Add(existingMusicVideonfo)

                        lstBxMainList.Items.Add(New ValueDescriptionPair(existingMusicVideonfo.fileinfo.fullpathandfilename, existingMusicVideonfo.fullmoviebody.title))
                    End If
                End If
            End If
            Try
                If Not IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), "-fanart.jpg")) And scrape = True Then
                    createScreenshot(videopath)
                End If
            Catch
            End Try

        

            Application.DoEvents()
            Me.Refresh()


        Next
        Call MusicVideoCacheSave()
        Preferences.MusicVidScrape = False
    End Sub

    Public Function saveposter(ByVal path As String, ByVal url As String)
        Try
            Dim posterpath As String = ""
            If url.IndexOf(".jpg") <> -1 Then
                posterpath = path.Replace(IO.Path.GetExtension(path), "-poster.jpg")
            ElseIf url.IndexOf(".png") <> -1 Then
                posterpath = path.Replace(IO.Path.GetExtension(path), "-poster.png")
            End If

            Dim web_client As WebClient = New WebClient
            web_client.DownloadFile(url, posterpath)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function createScreenshot(ByVal fullpathAndFilename As String, Optional ByVal time As Integer = 10, Optional ByVal overwrite As Boolean = False) As String
        Try

            Dim applicationpath As String = Preferences.applicationPath 'get application root path
            
            Dim thumbpathandfilename As String = fullpathAndFilename.Replace(IO.Path.GetExtension(fullpathAndFilename), "-fanart.jpg")
            Dim filepath As String = fullpathAndFilename.Replace(IO.Path.GetExtension(fullpathAndFilename), "")
            Dim fileexists As Boolean = False
            For Each extn In Utilities.VideoExtensions
                If IO.File.Exists(filepath & extn) Then
                    filepath = filepath & extn
                    fileexists = True
                End If
            Next
            If Not fileexists Then Return ""

            If overwrite = True Then
                If IO.File.Exists(thumbpathandfilename) Then
                    Try
                        IO.File.Delete(thumbpathandfilename)
                    Catch
                    End Try
                End If
            End If

            If Not IO.File.Exists(thumbpathandfilename) Or overwrite = True Then
                Dim myProcess As Process = New Process
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                myProcess.StartInfo.CreateNoWindow = False
                myProcess.StartInfo.FileName = applicationpath & "\Assets\ffmpeg.exe"
                Dim proc_arguments As String = "-y -i """ & filepath & """ -f mjpeg -ss " & time.ToString & " -vframes 1 -an " & """" & thumbpathandfilename & """"
                myProcess.StartInfo.Arguments = proc_arguments
                myProcess.Start()
                myProcess.WaitForExit()
                Return thumbpathandfilename
            Else
                Return ""
            End If
        Catch
            Return False
        End Try





    End Function

    Private Function validateMusicVideoNfo(ByVal fullPathandFilename As String)
        Dim tempstring As String
        Dim filechck As IO.StreamReader = IO.File.OpenText(fullPathandFilename)
        tempstring = filechck.ReadToEnd.ToLower
        filechck.Close()
        If tempstring = Nothing Then
            Return False
        End If
        If tempstring.IndexOf("<musicvideo>") <> -1 And tempstring.IndexOf("</musicvideo>") <> -1 And tempstring.IndexOf("<title>") <> -1 And tempstring.IndexOf("</title>") <> -1 Then
            Return True
            Exit Function
        End If
        Return False
    End Function

    Private Function listAllFolders()

        Dim allfolders As New List(Of String)
        allfolders.Clear()
        For Each moviefolder In Preferences.MVidFolders
            Dim hg As New IO.DirectoryInfo(moviefolder)
            If hg.Exists Then
                allfolders.Add(moviefolder)
                Dim subfolders As List(Of String)
                subfolders = getSubFolders(moviefolder)
                Try
                    For Each subfolder In subfolders
                        allfolders.Add(subfolder)
                    Next
                Catch
                End Try
            End If
        Next
        Return allfolders
    End Function

    Public Function getallfolders() As List(Of String)
        Dim allfolders As New List(Of String)
        oMovies.FindNewMusicVideos()
        Return allfolders
    End Function

    Public Function getSubFolders(ByVal RootDirectory As String, Optional ByVal log As Boolean = False)


        Dim folders As New List(Of String)

        For Each s As String In Directory.GetDirectories(RootDirectory)
            If (File.GetAttributes(s) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then
            Else
                If Not validmoviedir(s) Then
                Else
                    Dim exists As Boolean = False
                    For Each item In folders
                        If item = s Then exists = True
                    Next
                    If exists = True Then

                    Else

                        folders.Add(s)
                        For Each t As String In Directory.GetDirectories(s)
                            If (File.GetAttributes(t) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then

                            Else
                                If Not validmoviedir(t) Then

                                Else
                                    Dim existst As Boolean = False
                                    For Each item In folders
                                        If item = t Then existst = True
                                    Next
                                    If exists = True Then

                                    Else

                                        folders.Add(t)
                                        For Each u As String In Directory.GetDirectories(t)
                                            If (File.GetAttributes(u) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then

                                            Else
                                                If Not validmoviedir(u) Then

                                                Else
                                                    Dim existsu As Boolean = False
                                                    For Each item In folders
                                                        If item = s Then existsu = True
                                                    Next
                                                    If exists = True Then

                                                    Else

                                                        folders.Add(u)
                                                        For Each v As String In Directory.GetDirectories(u)
                                                            If (File.GetAttributes(v) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then

                                                            Else
                                                                If Not validmoviedir(v) Then

                                                                Else
                                                                    Dim existsv As Boolean = False
                                                                    For Each item In folders
                                                                        If item = v Then existsv = True
                                                                    Next
                                                                    If existsv = True Then

                                                                    Else

                                                                        folders.Add(v)
                                                                        For Each w As String In Directory.GetDirectories(v)
                                                                            If (File.GetAttributes(w) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then

                                                                            Else
                                                                                If Not validmoviedir(w) Then

                                                                                Else
                                                                                    Dim existsw As Boolean = False
                                                                                    For Each item In folders
                                                                                        If item = w Then existsw = True
                                                                                    Next
                                                                                    If existsw = True Then

                                                                                    Else

                                                                                        folders.Add(w)
                                                                                        For Each x As String In Directory.GetDirectories(w)
                                                                                            If (File.GetAttributes(x) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then

                                                                                            Else
                                                                                                If Not validmoviedir(x) Then

                                                                                                Else
                                                                                                    Dim existsx As Boolean = False
                                                                                                    For Each item In folders
                                                                                                        If item = x Then existsx = True
                                                                                                    Next
                                                                                                    If existsx = True Then

                                                                                                    Else

                                                                                                        folders.Add(x)
                                                                                                        For Each y As String In Directory.GetDirectories(x)
                                                                                                            If (File.GetAttributes(y) And FileAttributes.ReparsePoint) = FileAttributes.ReparsePoint Then

                                                                                                            Else
                                                                                                                If Not validmoviedir(y) Then

                                                                                                                Else
                                                                                                                    Dim existsy As Boolean = False
                                                                                                                    For Each item In folders
                                                                                                                        If item = y Then existsy = True
                                                                                                                    Next
                                                                                                                    If existsy = True Then

                                                                                                                    Else

                                                                                                                        folders.Add(y)
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
        Return (folders)
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

    Private Function Listvideofiles(ByVal pattern As String, ByVal dir_info As System.IO.DirectoryInfo)
        Dim mediaList As New List(Of String)


        Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles("*"&pattern)

        For Each fs_info As System.IO.FileInfo In fs_infos
            mediaList.Add(fs_info.FullName)
        Next
        Return mediaList
    End Function

    Private Sub lstBxMainList_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBxMainList.SelectedValueChanged

        txtAlbum.Text = ""
        txtArtist.Text = ""
        txtDirector.Text = ""
        txtFullpath.Text = ""
        txtGenre.Text = ""
        txtPlot.Text = ""
        txtRuntime.Text = ""
        txtStudio.Text = ""
        txtTitle.Text = ""
        txtYear.Text = ""
        PcBxMusicVideoScreenShot.Image = Nothing
        PcBxPoster.Image = Nothing
        pcBxScreenshot.Image = Nothing
        pcBxSinglePoster.Image = Nothing
        For Each MusicVideo In musicVideoList


            If MusicVideo.fileinfo.fullPathAndFilename Is CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value Then

                Dim nfopath As String = MusicVideo.fileinfo.fullPathAndFilename
                nfopath = nfopath.Replace(IO.Path.GetExtension(nfopath), ".nfo")

                'Dim workingMusicVideo As New Music_Video_Class
                workingMusicVideo = WorkingWithNfoFiles.MVloadNfo(nfopath)
                workingMusicVideo.fileinfo.fullPathAndFilename = MusicVideo.fileinfo.fullPathAndFilename
                'populate form
                txtAlbum.Text = workingMusicVideo.fullmoviebody.album
                txtArtist.Text = workingMusicVideo.fullmoviebody.artist
                txtDirector.Text = workingMusicVideo.fullmoviebody.director
                txtFullpath.Text = workingMusicVideo.fileinfo.fullPathAndFilename
                txtPlot.Text = workingMusicVideo.fullmoviebody.plot
                txtRuntime.Text = workingMusicVideo.fullmoviebody.runtime
                txtStudio.Text = workingMusicVideo.fullmoviebody.studio
                txtTitle.Text = workingMusicVideo.fullmoviebody.title
                txtYear.Text = workingMusicVideo.fullmoviebody.year
                txtGenre.Text = workingMusicVideo.fullmoviebody.genre
                txtFullpath.Text = workingMusicVideo.fileinfo.fullPathAndFilename
                'Dim streamdetails As String = "Video:" & vbCrLf
                'streamdetails = streamdetails & "Width: " & workingMusicVideo.filedetails.filedetails_video.Width.Value & vbCrLf
                'streamdetails = streamdetails & "Height: " & workingMusicVideo.filedetails.filedetails_video.Height.Value & vbCrLf
                'streamdetails = streamdetails & "Aspect: " & workingMusicVideo.filedetails.filedetails_video.Aspect.Value & vbCrLf
                'streamdetails = streamdetails & "Codec: " & workingMusicVideo.filedetails.filedetails_video.Codec.Value & vbCrLf
                'streamdetails = streamdetails & "Bitrate: " & workingMusicVideo.filedetails.filedetails_video.Bitrate.Value & vbCrLf
                'streamdetails = streamdetails & "Bitrate Max: " & workingMusicVideo.filedetails.filedetails_video.BitrateMax.Value & vbCrLf
                'streamdetails = streamdetails & "Container: " & workingMusicVideo.filedetails.filedetails_video.Container.Value & vbCrLf
                'streamdetails = streamdetails & "Scantype: " & workingMusicVideo.filedetails.filedetails_video.ScanType.Value & vbCrLf
                'streamdetails = streamdetails & "Duration (Seconds): " & workingMusicVideo.filedetails.filedetails_video.DurationInSeconds.Value & vbCrLf & vbCrLf

                'streamdetails = streamdetails & "Audio:" & vbCrLf
                'For Each audio In workingMusicVideo.filedetails.filedetails_audio
                '    streamdetails = streamdetails & "Codec: " & audio.Codec.Value & vbCrLf
                '    streamdetails = streamdetails & "Channels: " & audio.Channels.Value & vbCrLf
                '    streamdetails = streamdetails & "Bitrate: " & audio.Bitrate.Value & vbCrLf & vbCrLf
                'Next
                'txtStreamDetails.Text = streamdetails

                'load Fanart/Screenshot image
                Dim thumbpath As String = MusicVideo.fileinfo.fullPathAndFilename
                thumbpath = thumbpath.Replace(IO.Path.GetExtension(thumbpath), "-fanart.jpg")
                Form1.util_ImageLoad(PcBxMusicVideoScreenShot, thumbpath, Utilities.DefaultFanartPath)  'PcBxMusicVideoScreenShot.ImageLocation = thumbpath
                Form1.util_ImageLoad(pcBxScreenshot, thumbpath, Utilities.DefaultFanartPath)  'pcBxScreenshot.ImageLocation = thumbpath
                Label16.Text = pcBxScreenshot.Image.Width
                Label17.Text = pcBxScreenshot.Image.Height
                'Set Media overlay
                Dim video_flags = Form1.VidMediaFlags(workingMusicVideo.filedetails)
                movieGraphicInfo.OverlayInfo(PcBxMusicVideoScreenShot, "", video_flags)

                'Load Poster image
                thumbpath = MusicVideo.fileinfo.fullpathandfilename.Replace(IO.Path.GetExtension(MusicVideo.fileinfo.fullpathandfilename), "-poster.jpg")
                If IO.File.Exists(thumbpath) Then
                    Form1.util_ImageLoad(PcBxPoster, thumbpath, Utilities.DefaultFanartPath)  'PcBxPoster.ImageLocation = thumbpath
                    Form1.util_ImageLoad(pcBxSinglePoster, thumbpath, Utilities.DefaultFanartPath)  'PcBxPoster.ImageLocation = thumbpath
                    Label19.Text = pcBxScreenshot.Image.Width
                    Label18.Text = pcBxScreenshot.Image.Height
                Else
                    thumbpath = MusicVideo.fileinfo.fullpathandfilename.Replace(IO.Path.GetExtension(MusicVideo.fileinfo.fullpathandfilename), "-poster.png")
                    If IO.File.Exists(thumbpath) Then
                        Form1.util_ImageLoad(PcBxPoster, thumbpath, Utilities.DefaultFanartPath)  'PcBxPoster.ImageLocation = thumbpath
                        Form1.util_ImageLoad(pcBxSinglePoster, thumbpath, Utilities.DefaultFanartPath)  'PcBxPoster.ImageLocation = thumbpath
                        Label19.Text = pcBxScreenshot.Image.Width
                        Label18.Text = pcBxScreenshot.Image.Height
                    End If
                End If
            End If
        Next
    End Sub

    Public Sub MusicVideoCacheSave()
        Dim fullpath As String = Preferences.workingProfile.MusicVideoCache
        If musicVideoList.Count > 0 And Preferences.MVidFolders.Count > 0 Then

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



            Dim doc As New XmlDocument

            Dim thispref As XmlNode = Nothing
            Dim xmlproc As XmlDeclaration

            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            doc.AppendChild(xmlproc)
            Dim root As XmlElement
            Dim child As XmlElement
            root = doc.CreateElement("music_video_cache")
            Dim childchild As XmlElement

            Dim count2 As Integer = 0

            For Each item In musicVideoList

                child = doc.CreateElement("musicvideo")
                childchild = doc.CreateElement("fullpathandfilename")
                childchild.InnerText = item.fileinfo.fullPathAndFilename
                child.AppendChild(childchild)

                childchild = doc.CreateElement("title")
                childchild.InnerText = item.fullmoviebody.title
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

    Public Sub MusicVideoCacheLoad()
        musicVideoList.Clear()

        Dim musicvideocache As New XmlDocument
        Dim objReader As New System.IO.StreamReader(Preferences.workingProfile.MusicVideoCache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close()



        musicvideocache.LoadXml(tempstring)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In musicvideocache("music_video_cache")
            Select Case thisresult.Name
                Case "musicvideo"
                    Dim newMV As New FullMovieDetails  

                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            'workingmovie.missingdata1

                            Case "fullpathandfilename"
                                newMV.fileinfo.fullPathAndFilename = detail.InnerText
                            Case "title"
                                newMV.fullmoviebody.title = detail.InnerText
                        End Select
                    Next
                    musicVideoList.Add(newMV)
            End Select
        Next

        Call loadMusicVideolist()
        Try
            lstBxMainList.SelectedIndex = 0
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    Private Sub loadMusicVideolist()
        lstBxMainList.Items.Clear()
        For Each item In musicVideoList
            lstBxMainList.Items.Add(New ValueDescriptionPair(item.fileinfo.fullPathAndFilename, item.fullmoviebody.title))
        Next
    End Sub

    Private Sub txtFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        If txtFilter.Text <> "" Then
            lstBxMainList.Items.Clear()
            For Each item In musicVideoList
                If item.fullmoviebody.title.ToLower.IndexOf(txtFilter.Text.ToLower) <> -1 Then
                    lstBxMainList.Items.Add(New ValueDescriptionPair(item.fileinfo.fullPathAndFilename, item.fullmoviebody.title))
                End If
            Next
        Else
            For Each item In musicVideoList
                lstBxMainList.Items.Add(New ValueDescriptionPair(item.fileinfo.fullPathAndFilename, item.fullmoviebody.title))
            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateScreenshot.Click
        PcBxMusicVideoScreenShot.Image = Nothing
        pcBxScreenshot.Image = Nothing
        If Not lstBxMainList.SelectedItem Is Nothing Then
            For Each MusicVideo In musicVideoList
                If MusicVideo.fileinfo.fullPathAndFilename Is CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value Then
                    Dim screenshotpath As String = createScreenshot(MusicVideo.fileinfo.fullPathAndFilename, txtScreenshotTime.Text, True) 'MusicVideo.fileinfo.fullPathAndFilename

                    'createScreenshot(MusicVideo.fileinfo.fullPathAndFilename, txtScreenshotTime.Text, True)
                    Form1.util_ImageLoad(PcBxMusicVideoScreenShot, screenshotpath, Utilities.DefaultTvFanartPath)  'PcBxMusicVideoScreenShot.ImageLocation = screenshotpath '.Replace(IO.Path.GetExtension(screenshotpath), ".jpg")
                    Form1.util_ImageLoad(pcBxScreenshot, screenshotpath, Utilities.DefaultTvFanartPath)  'pcBxScreenshot.ImageLocation = screenshotpath '.Replace(IO.Path.GetExtension(screenshotpath), ".jpg")
                End If
            Next
        End If
    End Sub

    Private Sub btnScreenshotMinus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScreenshotMinus.Click
        Dim number As Integer = CInt(txtScreenshotTime.Text)
        If number > 1 Then
            number -= 1
            txtScreenshotTime.Text = number.ToString
        Else
            MsgBox("Cant be less than 1")
        End If
    End Sub

    Private Sub btnScreenshotPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScreenshotPlus.Click
        Dim number As Integer = CInt(txtScreenshotTime.Text)
        number += 1
        txtScreenshotTime.Text = number.ToString
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'save text routine
        workingMusicVideo.fullmoviebody.title = txtTitle.Text
        workingMusicVideo.fullmoviebody.album = txtAlbum.Text
        workingMusicVideo.fullmoviebody.artist = txtArtist.Text
        workingMusicVideo.fullmoviebody.director = txtDirector.Text
        workingMusicVideo.fullmoviebody.genre = txtGenre.Text
        workingMusicVideo.fullmoviebody.plot = txtPlot.Text
        workingMusicVideo.fullmoviebody.studio = txtStudio.Text
        workingMusicVideo.fullmoviebody.year = txtYear.Text

        WorkingWithNfoFiles.MVsaveNfo(workingMusicVideo)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        musicVideoList.Clear()
        lstBxMainList.Items.Clear()
        Call searchFornew(False)
    End Sub

    
    Private Sub btnAddFolderPath_Click(sender As Object, e As System.EventArgs) Handles btnAddFolderPath.Click
        Try
            If tbFolderPath.Text = Nothing Then
                Exit Sub
            End If
            If tbFolderPath.Text = "" Then
                Exit Sub
            End If
            Dim tempstring As String = tbFolderPath.Text
            Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Dim exists As Boolean = False
            For Each item In lstBoxFolders.Items
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
                    lstBoxFolders.Items.Add(tempstring)
                    tbFolderPath.Text = ""
                    Preferences.MVidFolders.Add(tempstring)
                    lstBoxFolders.Refresh()
                Else
                    Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If tempint = DialogResult.Yes Then
                        lstBoxFolders.Items.Add(tempstring)
                        tbFolderPath.Text = ""
                        Preferences.MVidFolders.Add(tempstring)
                    End If
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnCrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrop.Click
        Form1.cropMode = "mvscreenshot"
        Try
            Dim t As New frmMovPosterCrop
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnCropReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCropReset.Click
        pcBxScreenshot.Image = PcBxMusicVideoScreenShot.Image
        btnCropReset.Enabled = False
        btnSaveCrop.Enabled = False
    End Sub

    Private Sub btnSaveCrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCrop.Click
        Dim bitmap3 As New Bitmap(pcBxScreenshot.Image)
        Dim fullpathandfilename As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
        Dim thumbpathandfilename As String = fullpathAndFilename.Replace(IO.Path.GetExtension(fullpathAndFilename), "-fanart.jpg")
        bitmap3.Save(thumbpathandfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
        bitmap3.Dispose()
        btnCropReset.Enabled = False
        btnSaveCrop.Enabled = False
        PcBxMusicVideoScreenShot.Image = Nothing
        PcBxMusicVideoScreenShot.ImageLocation = thumbpathandfilename
        pcBxScreenshot.ImageLocation = thumbpathandfilename
    End Sub

    Private Sub pcBxScreenshot_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcBxScreenshot.DoubleClick
        Try
            If Not pcBxScreenshot.Image Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                'ToolStrip1.Enabled = False
                Dim newimage As New Bitmap(pcBxScreenshot.Image)
                Call Form1.util_ZoomImage(newimage)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnPasteFromClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPasteFromClipboard.Click
        If AssignClipboardImage(pcBxScreenshot) Then
            btnCropReset.Enabled = True
            btnSaveCrop.Enabled = True
            Label16.Text = pcBxScreenshot.Image.Width
            Label17.Text = pcBxScreenshot.Image.Height
        End If
    End Sub

    Private Function AssignClipboardImage(ByVal picBox As PictureBox) As Boolean
        Try
            If Clipboard.GetDataObject.GetDataPresent(DataFormats.Filedrop) Then
                Dim pth As String = CType(Clipboard.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString
                Dim FInfo As IO.FileInfo = New IO.FileInfo(pth)
                If FInfo.Extension.ToLower() = ".jpg" Or FInfo.Extension.ToLower() = ".tbn" Or FInfo.Extension.ToLower() = ".bmp" Or FInfo.Extension.ToLower() = ".png" Then
                    Form1.util_ImageLoad(picBox, pth, Utilities.DefaultPosterPath)
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

    Private Sub PcBxMusicVideoScreenShot_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PcBxMusicVideoScreenShot.DoubleClick
        Try
            If Not PcBxMusicVideoScreenShot.Image Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                'ToolStrip1.Enabled = False
                Dim newimage As New Bitmap(PcBxMusicVideoScreenShot.Image)
                Call Form1.util_ZoomImage(newimage)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PcBxPoster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PcBxPoster.DoubleClick
        Try
            If Not PcBxPoster.Image Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                'ToolStrip1.Enabled = False
                Dim newimage As New Bitmap(PcBxPoster.Image)
                Call Form1.util_ZoomImage(newimage)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub googleSearch()
        Dim title As String = txtTitle.Text
        Dim artist As String = txtArtist.Text

        Dim url As String = "http://images.google.com/images?q=" & title & "+" & artist
      
        Form1.OpenUrl(url)
    End Sub

    Private Sub btnGoogleSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoogleSearch.Click
        Call googleSearch()
    End Sub

    Private Sub btnGoogleSearchPoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoogleSearchPoster.Click
        Call googleSearch()
    End Sub

    Private Sub btnPosterPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterPaste.Click
        If AssignClipboardImage(pcBxSinglePoster) Then
            btnPosterReset.Enabled = True
            btnPosterSave.Enabled = True
            Label16.Text = pcBxSinglePoster.Image.Width
            Label17.Text = pcBxSinglePoster.Image.Height
        End If
    End Sub

    Private Sub btnPosterCrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterCrop.Click
        Dim bitmap3 As New Bitmap(pcBxSinglePoster.Image)
        Dim fullpathandfilename As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
        Dim thumbpathandfilename As String = fullpathandfilename.Replace(IO.Path.GetExtension(fullpathandfilename), "-poster.jpg")
        bitmap3.Save(thumbpathandfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
        bitmap3.Dispose()
        btnPosterReset.Enabled = False
        btnPosterSave.Enabled = False
        pcBxSinglePoster.Image = Nothing
        pcBxSinglePoster.ImageLocation = thumbpathandfilename
        PcBxPoster.ImageLocation = thumbpathandfilename
    End Sub

    Private Sub btnPosterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterReset.Click
        pcBxSinglePoster.Image = PcBxPoster.Image
        btnPosterReset.Enabled = False
        btnPosterSave.Enabled = False
    End Sub

    Private Sub btnPosterSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterSave.Click
        Dim bitmap3 As New Bitmap(pcBxSinglePoster.Image)
        Dim fullpathandfilename As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
        Dim thumbpathandfilename As String = fullpathandfilename.Replace(IO.Path.GetExtension(fullpathandfilename), "-poster.jpg")
        bitmap3.Save(thumbpathandfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
        bitmap3.Dispose()
        btnCropReset.Enabled = False
        btnSaveCrop.Enabled = False
        pcBxSinglePoster.Image = Nothing
        PcBxPoster.Image = Nothing
        PcBxPoster.ImageLocation = thumbpathandfilename
        pcBxSinglePoster.ImageLocation = thumbpathandfilename
        btnPosterReset.Enabled = False
        btnPosterSave.Enabled = False
    End Sub

    Private Sub pcBxSinglePoster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcBxSinglePoster.DoubleClick
        Try
            If Not pcBxSinglePoster.Image Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                'ToolStrip1.Enabled = False
                Dim newimage As New Bitmap(pcBxSinglePoster.Image)
                Call Form1.util_ZoomImage(newimage)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Function getArtistAndTitle(ByVal fullpathandfilename As String)
        Dim searchTerm As String = ""
        Dim filenameWithoutExtension As String = Path.GetFileNameWithoutExtension(fullpathandfilename)
        If filenameWithoutExtension.IndexOf(" - ") <> -1 Then
            searchTerm = filenameWithoutExtension
        Else 'assume /artist/title.ext convention
            Try
                Dim lastfolder As String = Utilities.GetLastFolder(fullpathandfilename)
                searchTerm = lastfolder & " - " & filenameWithoutExtension
            Catch
            End Try
        End If

        If searchTerm = "" Then
            searchTerm = filenameWithoutExtension
        End If

        Return searchTerm
    End Function

    Private Sub TabControlMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlMain.SelectedIndexChanged
        If TabControlMain.SelectedTab.Text = "Manually find Correct Wiki Entry" Then

            Dim searchterm As String = getArtistAndTitle(CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value)
            Dim searchurl As String = "http://www.google.co.uk/search?hl=en-US&as_q=" & searchterm & "%20song&as_sitesearch=http://en.wikipedia.org/"

            WebBrowser1.Stop()
            WebBrowser1.ScriptErrorsSuppressed = True
            WebBrowser1.Navigate(searchurl)
            WebBrowser1.Refresh()
        End If
    End Sub

   
    Private Sub btnManualScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManualScrape.Click
        If WebBrowser1.Url.ToString.ToLower.IndexOf("wikipedia.org") = -1 Or WebBrowser1.Url.ToString.ToLower.IndexOf("google") <> -1 Then
            MsgBox("You Must Browse to a Wikipedia Page")
            Exit Sub
        End If

        Dim messagestring As String = "Changing the movie will Overwrite all the current details"
        messagestring &= vbCrLf & "Do you wish to continue?"
        If MessageBox.Show(messagestring, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
            Exit Sub
        Else

            Dim videopath As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
            Dim musicVideoTitle As New FullMovieDetails
            Dim scraper As New WikipediaMusivVideoScraper

            Dim searchterm As String = getArtistAndTitle(CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value)
            musicVideoTitle = scraper.musicVideoScraper(getArtistAndTitle(CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value), "", WebBrowser1.Url.ToString)

            'poster
            Try
                If Not IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), "-poster.jpg")) And musicVideoTitle.listthumbs(0) <> "" Then
                    'save available posters if dont already exist
                    saveposter(videopath, musicVideoTitle.listthumbs(0))
                Else
                    If chkBxOverWriteArt.CheckState = CheckState.Checked Then
                        'IF overwrite chkbox ticked then delete and save new
                        IO.File.Delete(videopath.Replace(IO.Path.GetExtension(videopath), "-poster.jpg"))
                        saveposter(videopath, musicVideoTitle.listthumbs(0))
                    End If
                End If
            Catch
            End Try

            'screenshot
            If Not IO.File.Exists(videopath.Replace(IO.Path.GetExtension(videopath), "-fanart.jpg")) Then
                createScreenshot(videopath)
            Else
                If chkBxOverWriteArt.CheckState = CheckState.Checked Then
                    'IF overwrite chkbox ticked then delete and save new
                    IO.File.Delete(videopath.Replace(IO.Path.GetExtension(videopath), "-fanart.jpg"))
                    createScreenshot(videopath)
                End If
            End If

            'streamdetails
            Dim newstreamdetails As New FullFileDetails
            musicVideoTitle.filedetails = Preferences.Get_HdTags(videopath)

            Dim seconds As Integer = Convert.ToInt32(musicVideoTitle.filedetails.filedetails_video.DurationInSeconds.Value)
            Dim hms = TimeSpan.FromSeconds(seconds)
            Dim h = hms.Hours.ToString
            Dim m = hms.Minutes.ToString
            Dim s = hms.Seconds.ToString

            If s.Length = 1 Then s = "0" & s

            Dim runtime As String
            runtime = h & ":" & m & ":" & s
            If h = "0" Then
                runtime = m & ":" & s
            End If
            If h = "0" And m = "0" Then
                runtime = s
            End If
            musicVideoTitle.fullmoviebody.runtime = runtime
            musicVideoTitle.fileinfo.fullpathandfilename = videopath
            WorkingWithNfoFiles.MVsaveNfo(musicVideoTitle)

            For f = 0 To musicVideoList.Count - 1
                If musicVideoList.Item(f).fileinfo.fullpathandfilename.ToLower = videopath.ToLower Then
                    musicVideoList.RemoveAt(f)
                    musicVideoList.Add(musicVideoTitle)
                    Exit For
                End If
            Next

            For f = 0 To lstBxMainList.Items.Count - 1
                If CType(lstBxMainList.Items(f), ValueDescriptionPair).Value = musicVideoTitle.fileinfo.fullpathandfilename Then
                    rescraping = True
                    lstBxMainList.Items.RemoveAt(f)
                    lstBxMainList.Items.Add(New ValueDescriptionPair(musicVideoTitle.fileinfo.fullpathandfilename, musicVideoTitle.fullmoviebody.title))
                    For g = 0 To lstBxMainList.Items.Count - 1
                        If CType(lstBxMainList.Items(g), ValueDescriptionPair).Value = musicVideoTitle.fileinfo.fullpathandfilename Then
                            rescraping = False
                            lstBxMainList.SelectedItem = lstBxMainList.Items(g)
                        End If
                    Next
                    Exit For
                End If
            Next

        End If
        rescraping = False
        TabControlMain.SelectedIndex = 0

    End Sub
End Class
