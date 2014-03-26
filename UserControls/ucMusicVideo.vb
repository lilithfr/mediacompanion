Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class ucMusicVideo
Dim nfo As New WorkingWithNfoFiles
    Dim musicVideoList As New List(Of Music_Video_Class)

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        musicVideoList.Clear()
        For Each item In Preferences.MVidFolders
            lstBoxFolders.Items.Add(item)
        Next
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

    End Sub
    
    Private Function createScreenshot(ByVal fullpathAndFilename)
        Try
            Dim applicationpath As String = Application.StartupPath 'get application root path

            Dim thumbpathandfilename As String = fullpathAndFilename.Replace(IO.Path.GetExtension(fullpathAndFilename), ".jpg")

            Dim myProcess As Process = New Process
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.FileName = applicationpath & "\ffmpeg.exe"
            Dim proc_arguments As String = "-y -i """ & fullpathAndFilename & """ -f mjpeg -ss " & "30" & " -vframes 1 -an " & """" & thumbpathandfilename & """"
            myProcess.StartInfo.Arguments = proc_arguments
            myProcess.Start()
            myProcess.WaitForExit()

            Return True
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

            End If
        Next
    End Sub

End Class
