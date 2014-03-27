Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Xml

Public Class ucMusicVideo
Dim nfo As New WorkingWithNfoFiles
    Public musicVideoList As New List(Of Music_Video_Class)

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

    Private Sub btnSearchNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNew.Click
        Dim fullfolderlist As New List(Of String)
        fullfolderlist.Clear()
        fullfolderlist = listAllFolders()

        Dim FullFileList As New List(Of String)
        Dim filelist As New List(Of String)

        Dim extensions As New List(Of String)
        extensions.Add("*.avi")
        extensions.Add("*.xvid")
        extensions.Add("*.divx")
        extensions.Add("*.img")
        extensions.Add("*.mpg")
        extensions.Add("*.mpeg")
        extensions.Add("*.mov")
        extensions.Add("*.rm")
        extensions.Add("*.3gp")
        extensions.Add("*.m4v")
        extensions.Add("*.wmv")
        extensions.Add("*.asf")
        extensions.Add("*.mp4")
        extensions.Add("*.mkv")
        extensions.Add("*.nrg")
        extensions.Add("*.iso")
        extensions.Add("*.rmvb")
        extensions.Add("*.ogm")
        extensions.Add("*.bin")
        extensions.Add("*.ts")
        extensions.Add("*.vob")
        extensions.Add("*.m2ts")

        For Each folder In fullfolderlist
            For Each extension In extensions
                Dim dir_info As New System.IO.DirectoryInfo(folder)
                filelist.Clear()
                filelist = Listvideofiles(extension, dir_info)
                For Each item In filelist
                    FullFileList.Add(item)
                Next
            Next
        Next

        For Each videopath In FullFileList

            Dim musicVideoTitle As New Music_Video_Class
            Dim scraper As New WikipediaMusivVideoScraper

            musicVideoTitle = scraper.musicVideoScraper(videopath)


            createScreenshot(musicVideoTitle.fullPathAndFilename)

            Dim newstreamdetails As New FullFileDetails
            musicVideoTitle.streamdetails = Preferences.Get_HdTags(musicVideoTitle.fullPathAndFilename)

            Dim seconds As Integer = Convert.ToInt32(musicVideoTitle.streamdetails.filedetails_video.DurationInSeconds.Value)
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
            musicVideoTitle.runtime = runtime

            'Dim saveingnfo As New LoadAndSave_nfo
            nfo.MVsaveNfo(musicVideoTitle)

            musicVideoList.Add(musicVideoTitle)

            lstBxMainList.Items.Add(New ValueDescriptionPair(musicVideoTitle.fullPathAndFilename, musicVideoTitle.title))


            Application.DoEvents()
            Me.Refresh()


        Next
        Call MusicVideoCacheSave()
    End Sub
    
    Private Function createScreenshot(ByVal fullpathAndFilename As String, Optional ByVal time As Integer = 10, Optional ByVal overwrite As Boolean = False)
        Try

            Dim applicationpath As String = Application.StartupPath 'get application root path

            Dim thumbpathandfilename As String = fullpathAndFilename.Replace(IO.Path.GetExtension(fullpathAndFilename), ".jpg")

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
                myProcess.StartInfo.FileName = applicationpath & "\ffmpeg.exe"
                Dim proc_arguments As String = "-y -i """ & fullpathAndFilename & """ -f mjpeg -ss " & time & " -vframes 1 -an " & """" & thumbpathandfilename & """"
                myProcess.StartInfo.Arguments = proc_arguments
                myProcess.Start()
                myProcess.WaitForExit()
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try





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


        Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(pattern)

        For Each fs_info As System.IO.FileInfo In fs_infos
            mediaList.Add(fs_info.FullName)
        Next
        Return mediaList
    End Function

    Private Sub lstBxMainList_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBxMainList.SelectedValueChanged
        For Each MusicVideo In musicVideoList
            If MusicVideo.fullPathAndFilename Is CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value Then
                
                Dim nfopath As String = MusicVideo.fullPathAndFilename
                nfopath = nfopath.Replace(IO.Path.GetExtension(nfopath), ".nfo")

                Dim workingMusicVideo As New Music_Video_Class
                workingMusicVideo = nfo.MVloadNfo(nfopath)
                workingMusicVideo.fullPathAndFilename = MusicVideo.fullPathAndFilename
                'populate form
                txtAlbum.Text = workingMusicVideo.album
                txtArtist.Text = workingMusicVideo.artist
                txtDirector.Text = workingMusicVideo.director
                txtFullpath.Text = workingMusicVideo.fullPathAndFilename
                txtPlot.Text = workingMusicVideo.plot
                txtRuntime.Text = workingMusicVideo.runtime
                txtStudio.Text = workingMusicVideo.studio
                txtTitle.Text = workingMusicVideo.title
                txtYear.Text = workingMusicVideo.year
                txtGenre.Text = workingMusicVideo.genre
                txtFullpath.Text = workingMusicVideo.fullPathAndFilename
                'load poster
                Dim thumbpath As String = MusicVideo.fullPathAndFilename
                thumbpath = thumbpath.Replace(IO.Path.GetExtension(thumbpath), ".jpg")
                PcBxMusicVideoScreenShot.ImageLocation = thumbpath
                pcBxScreenshot.ImageLocation = thumbpath

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
                childchild.InnerText = item.fullPathAndFilename
                child.AppendChild(childchild)

                childchild = doc.CreateElement("title")
                childchild.InnerText = item.title
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
                    Dim newMV As New Music_Video_Class

                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            'workingmovie.missingdata1

                            Case "fullpathandfilename"
                                newMV.fullPathAndFilename = detail.InnerText
                            Case "title"
                                newMV.title = detail.InnerText
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
            lstBxMainList.Items.Add(New ValueDescriptionPair(item.fullPathAndFilename, item.title))
        Next
    End Sub

    Private Sub txtFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        If txtFilter.Text <> "" Then
            lstBxMainList.Items.Clear()
            For Each item In musicVideoList
                If item.title.ToLower.IndexOf(txtFilter.Text.ToLower) <> -1 Then
                    lstBxMainList.Items.Add(New ValueDescriptionPair(item.fullPathAndFilename, item.title))
                End If
            Next
        Else
            For Each item In musicVideoList
                lstBxMainList.Items.Add(New ValueDescriptionPair(item.fullPathAndFilename, item.title))
            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PcBxMusicVideoScreenShot.Image = Nothing
        pcBxScreenshot.Image = Nothing
        If Not lstBxMainList.SelectedItem Is Nothing Then
            For Each MusicVideo In musicVideoList
                If MusicVideo.fullPathAndFilename Is CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value Then
                    Dim screenshotpath As String = MusicVideo.fullPathAndFilename

                    createScreenshot(screenshotpath, txtScreenshotTime.Text, True)
                    PcBxMusicVideoScreenShot.ImageLocation = screenshotpath.Replace(IO.Path.GetExtension(screenshotpath), ".jpg")
                    pcBxScreenshot.ImageLocation = screenshotpath.Replace(IO.Path.GetExtension(screenshotpath), ".jpg")
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

    End Sub
End Class
