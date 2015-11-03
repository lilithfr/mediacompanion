Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Xml 
Imports System.Linq  
Imports Media_Companion


Public Class ucMusicVideo
    Private WithEvents Tmr As New Timer With {.Interval = 200}
    Private fb As New FolderBrowserDialog
    Private Const WM_USER As Integer = &H400
    Private Const BFFM_SETEXPANDED As Integer = WM_USER + 106

    <DllImport("user32.dll", EntryPoint:="SendMessageW")> _
    Private Shared Function SendMessageW(ByVal hWnd As IntPtr, ByVal msg As UInteger, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindowW")> _
    Private Shared Function FindWindowW(<MarshalAs(UnmanagedType.LPWStr)> ByVal lpClassName As String, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpWindowName As String) As IntPtr
    End Function
    
    'Public Shared musicVideoList As New List(Of FullMovieDetails)
    Public Shared Property MVCache As New List(Of MVComboList)
    Private Property tmpMVCache As New List(Of MVComboList)
    Public Shared changeMVList As New List(Of String)
    Private changefields As Boolean = False
    Dim movieGraphicInfo As New GraphicInfo
    Public cropimage As Bitmap
    Dim rescraping As Boolean = False
    Dim mvFoldersChanged As Boolean = False
    Dim AuthorizeCheck As Boolean = False
    Dim Movie As New Movie
    Dim scraper As New Classimdb 
    Dim workingMusicVideo As New FullMovieDetails
    Dim PrevTab As Integer = 0

    Private Property MVPrefChanged As Boolean
        Get
            Return mvfolderschanged
        End Get
        Set(value As Boolean)
            mvFoldersChanged = value
            If mvFoldersChanged Then
                btnMVApply.Enabled = True
            Else
                btnMVApply.Enabled = False
            End If
        End Set
    End Property

'Music_Video_Class

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MVPreferencesLoad()
    End Sub

    Private Sub MVPreferencesLoad()
        clbxMvFolders.Items.Clear()
        For Each item In Preferences.MVidFolders
            AuthorizeCheck = True
            clbxMvFolders.Items.Add(item.rpath, item.selected)
            AuthorizeCheck = False
        Next
        If Preferences.MVScraper = "wiki" Then
            rb_MvScr1.Checked = True
        ElseIf Preferences.MVScraper = "imvdb" Then
            rb_MvScr2.Checked = True
        Else
            rb_MvScr3.Checked = True
        End If
        MVPrefChanged = False
        txtScreenshotTime.Text = "10"
    End Sub

    Private Sub SearchForNewMV()
        Preferences.MusicVidScrape = True
        'Dim TestString As String = scraper.getMVbody("Abba - Dancing Queen")
        'Exit Sub
        Form1.RunBackgroundMovieScrape("SearchForNewMusicVideo")
        While Form1.BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While
        'musicVideoList.Clear()
        loadMusicVideolist()
        'Call searchFornew(False)
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

    Public Shared Function createScreenshot(ByVal fullpathAndFilename As String, Optional ByVal time As Integer = 10, Optional ByVal overwrite As Boolean = False) As String
        
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
    
    Public Function getallfolders() As List(Of String)
        Dim allfolders As New List(Of String)
        oMovies.FindNewMusicVideos()
        Return allfolders
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
        For Each MusicVideo As MVComboList In MVCache 'In musicVideoList
            If MusicVideo.nfopathandfilename Is CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value Then
                Dim nfopath As String = MusicVideo.nfopathandfilename
                nfopath = nfopath.Replace(IO.Path.GetExtension(nfopath), ".nfo")
                workingMusicVideo = WorkingWithNfoFiles.MVloadNfo(nfopath)
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
                Form1.util_ImageLoad(PcBxMusicVideoScreenShot, workingMusicVideo.fileinfo.fanartpath, Utilities.DefaultFanartPath)
                Form1.util_ImageLoad(pcBxScreenshot, workingMusicVideo.fileinfo.fanartpath, Utilities.DefaultFanartPath)
                Label16.Text = pcBxScreenshot.Image.Width
                Label17.Text = pcBxScreenshot.Image.Height
                'Set Media overlay
                Dim video_flags = Form1.VidMediaFlags(workingMusicVideo.filedetails)
                movieGraphicInfo.OverlayInfo(PcBxMusicVideoScreenShot, "", video_flags)
                
                If IO.File.Exists(workingMusicVideo.fileinfo.posterpath) Then
                    Form1.util_ImageLoad(PcBxPoster, workingMusicVideo.fileinfo.posterpath, Utilities.DefaultFanartPath)
                    Form1.util_ImageLoad(pcBxSinglePoster, workingMusicVideo.fileinfo.posterpath, Utilities.DefaultFanartPath)
                    Label19.Text = pcBxSinglePoster.Image.Width
                    Label18.Text = pcBxSinglePoster.Image.Height
                End If
            End If
        Next
    End Sub

    Private Sub lstBxMainList_MouseUp(sender As Object, e As MouseEventArgs) Handles lstBxMainList.MouseUp
        Try
            Dim nfopathandfilename As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
            If String.IsNullOrEmpty(nfopathandfilename) Then Exit Sub
            For each MV In MVCache
                If MV.nfopathandfilename = nfopathandfilename Then
                    Dim TitleName As String = MV.artist & " - " & MV.title
                    tsmiMVName.Text = TitleName
                    Exit Sub
                End If
            Next
        Catch
        End Try
    End Sub

#Region " Music Video Cache Routines"
    Public Sub MusicVideoCacheSave()
        Dim fullpath As String = Preferences.workingProfile.MusicVideoCache
        'If musicVideoList.Count > 0 And Preferences.MVidFolders.Count > 0 Then
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
        'Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration
            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement
        Dim child As XmlElement
        root = doc.CreateElement("music_video_cache")
        Dim childchild As XmlElement

        Dim count2 As Integer = 0

        For Each item In MVCache

            child = doc.CreateElement("musicvideo")
                
            childchild = doc.CreateElement("fullpathandfilename")
            childchild.InnerText = item.nfopathandfilename
            child.AppendChild(childchild)
			
            childchild = doc.CreateElement("filename")
            childchild.InnerText = item.filename
            child.AppendChild(childchild)
			
		    childchild = doc.CreateElement("foldername")
            childchild.InnerText = item.foldername
            child.AppendChild(childchild)
			
		    childchild = doc.CreateElement("title")
            childchild.InnerText = item.title
            child.AppendChild(childchild)
			
		    childchild = doc.CreateElement("artist")
		    childchild.InnerText = item.artist
		    child.AppendChild(childchild)
			
            childchild = doc.CreateElement("year")
            childchild.InnerText = item.year
            child.AppendChild(childchild)			
			
            childchild = doc.CreateElement("filedate")
            childchild.InnerText = item.filedate
            child.AppendChild(childchild)
			
            childchild = doc.CreateElement("createdate")
            childchild.InnerText = item.createdate
            child.AppendChild(childchild)
			
		    childchild = doc.CreateElement("genre")
            childchild.InnerText = item.genre
            child.AppendChild(childchild)

            childchild = doc.CreateElement("plot")
            childchild.InnerText = item.plot
            child.AppendChild(childchild)
			
		    childchild = doc.CreateElement("playcount")
            childchild.InnerText = item.playcount
            child.AppendChild(childchild)
			
		    childchild = doc.CreateElement("runtime")
            childchild.InnerText = item.runtime
            child.AppendChild(childchild)
			
            childchild = doc.CreateElement("Resolution")
            childchild.InnerText = item.Resolution
		    child.AppendChild(childchild)
			
            childchild = doc.CreateElement("FrodoPosterExists")
            childchild.InnerText = item.FrodoPosterExists
		    child.AppendChild(childchild)
			
            childchild = doc.CreateElement("PreFrodoPosterExists")
            childchild.InnerText = item.PreFrodoPosterExists
		    child.AppendChild(childchild)

		    For Each track In item.Audio  
                child.AppendChild(track.GetChild(doc))
            Next
            
            root.AppendChild(child)
        Next

        doc.AppendChild(root)

        Try

            Dim output As New XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
            output.Formatting = Xml.Formatting.Indented
            doc.WriteTo(output)
            output.Close()
            'Exit For
        Catch ex As Exception
#If SilentErrorScream Then
        Throw ex
#End If
        End Try
        'Else
        '    Try
        '        If IO.File.Exists(fullpath) Then
        '            IO.File.Delete(fullpath)
        '        End If
        '    Catch
        '    End Try
        'End If
    End Sub

    Public Sub MusicVideoCacheLoad()
        MVCache.Clear()
        'musicVideoList.Clear()

        Dim musicvideocache As New XmlDocument
        Dim objReader As New System.IO.StreamReader(Preferences.workingProfile.MusicVideoCache)
        Dim tempstring As String = objReader.ReadToEnd
        objReader.Close()



        musicvideocache.LoadXml(tempstring)
        Dim thisresult As XmlNode = Nothing
        For Each thisresult In musicvideocache("music_video_cache")
            Select Case thisresult.Name
                Case "musicvideo"
                    Dim newMV As New MVComboList   

                    Dim detail As XmlNode = Nothing
                    For Each detail In thisresult.ChildNodes
                        Select Case detail.Name
                            'workingmovie.missingdata1

                            Case "fullpathandfilename" : newMV.nfopathandfilename = detail.InnerText
                            Case "filename" : newMV.filename = detail.InnerText
                            Case "foldername" : newMV.foldername = detail.InnerText
                            Case "title" : newMV.title = detail.InnerText
                            Case "artist" : newMV.artist = detail.InnerText
                            Case "year" : newMV.year = detail.InnerText
                            Case "filedate"
                                If detail.InnerText.Length <> 14 Then 'i.e. invalid date
                                        newMV.filedate = "18500101000000" '01/01/1850 00:00:00
                                    Else
                                        newMV.filedate = detail.InnerText
                                    End If
                            Case "createdate"
                                If detail.InnerText.Length <> 14 Then 'i.e. invalid date
                                        newMV.filedate = "18500101000000" '01/01/1850 00:00:00
                                    Else
                                        newMV.filedate = detail.InnerText
                                    End If
                            Case "genre" : newMV.genre = detail.InnerText & newMV.genre
                            Case "plot" : newMV.plot = detail.InnerText
                            Case "playcount" : newMV.playcount = detail.InnerText
                            Case "runtime" : newMV.runtime = detail.InnerText
                            Case "Resolution" : newMV.Resolution = detail.InnerText
                            Case "FrodoPosterExists" : newMV.FrodoPosterExists = detail.InnerText
                            Case "PreFrodoPosterExists" : newMV.PreFrodoPosterExists = detail.InnerText
                            Case "audio"
                                Dim audio As New AudioDetails
                                For Each audiodetails As XmlNode In detail.ChildNodes
                                    Select Case audiodetails.Name
                                        Case "language"
                                            audio.Language.Value = audiodetails.InnerText
                                        Case "codec"
                                            audio.Codec.Value = audiodetails.InnerText
                                        Case "channels"
                                            audio.Channels.Value = audiodetails.InnerText
                                        Case "bitrate"
                                            audio.Bitrate.Value = audiodetails.InnerText
                                    End Select
                                Next
                                newMV.Audio.Add(audio)
                        End Select
                    Next
                    If newMV.playcount = "" Then newMV.playcount = "0"
                    MVCache.Add(newMV)
            End Select
        Next

        Call loadMusicVideolist()
        Try
            If lstBxMainList.Items.Count > 0 Then
                lstBxMainList.SelectedIndex = 0
            End If
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    Public Sub MVCacheLoadFromNfo()
        tmpMVCache.Clear()
        Dim t As New List(Of String)
        For each item In Preferences.MVidFolders
            If item.selected Then
                t.Add(item.rpath)
            End If
        Next
        't.AddRange(Preferences.MVidFolders)
        MV_Load(t)
        loadMusicVideolist()
        '...load datagridview??
    End Sub

    Public Sub MVCacheAddScraped(tmpMV As FullMovieDetails)
        MVCacheadd(tmpMV)
        loadMusicVideolist()
    End Sub

    Shared Sub MVCacheadd(tmpMv As FullMovieDetails)
        Dim tmpMVcb As New MVComboList
        tmpMVcb.Assign(tmpMV)
        MVCache.Add(tmpMVcb)
    End Sub

    Sub MVCacheRemove(nfopath As String)
        MVCache.RemoveAll(Function(c) c.nfopathandfilename = nfopath)
    End Sub

#End Region 

    Sub MV_Load(ByVal folderlist As List(Of String))
        Dim tempint As Integer
        Dim dirinfo As String = String.Empty
        Const pattern = "*.nfo"
        Dim moviePaths As New List(Of String)

        For Each moviefolder In folderlist
            If (New DirectoryInfo(moviefolder)).Exists Then
                moviePaths.Add(moviefolder)
            End If
        Next
        tempint = moviePaths.Count

        'Add sub-folders
        For f = 0 To tempint - 1
            Try
                For Each subfolder In Utilities.EnumerateFolders(moviePaths(f))
                    moviePaths.Add(subfolder)
                Next
            Catch ex As Exception
                ExceptionHandler.LogError(ex,"LastRootPath: [" & Utilities.LastRootPath & "]")
            End Try
        Next
        'Dim i = 0
        For Each Path In moviePaths
            'i += 1
            'PercentDone = CalcPercentDone(i, moviePaths.Count)
            'ReportProgress("Scanning folder " & i & " of " & moviePaths.Count)

            MV_ListFiles(pattern, New DirectoryInfo(Path))
            'If Cancelled Then Exit Sub
        Next
        MVCache.Clear
        MVCache.AddRange(tmpMVCache)
    End Sub

    Private Sub MV_ListFiles(ByVal pattern As String, ByVal dirInfo As DirectoryInfo)
        'Dim incmissing As Boolean = Preferences.incmissingmovies 
        If IsNothing(dirInfo) Then Exit Sub
         
        For Each oFileInfo In dirInfo.GetFiles(pattern)
            Dim tmp As New MVComboList
            Application.DoEvents
            'If Cancelled Then Exit Sub
            If Not File.Exists(oFileInfo.FullName) Then Continue For
            Try
                If Not validateMusicVideoNfo(oFileInfo.FullName) Then Continue For
                Dim mvideo As New FullMovieDetails
                mvideo = WorkingWithNfoFiles.MVloadNfo(oFileInfo.FullName)
                tmp.Assign(mvideo)
                tmpMVCache.Add(tmp)
            Catch
                MsgBox("problem with : " & oFileInfo.FullName & " - Skipped" & vbCrLf & "Please check this file manually")
            End Try
        Next

    End Sub

    Private Sub loadMusicVideolist()
        lstBxMainList.Items.Clear()
        'For Each item In musicVideoList
        '    lstBxMainList.Items.Add(New ValueDescriptionPair(item.fileinfo.fullPathAndFilename, item.fullmoviebody.title))
        'Next
        For Each item In MVCache' musicVideoList
            lstBxMainList.Items.Add(New ValueDescriptionPair(item.nfopathandfilename, item.title))
        Next
        MVDgv1.DataSource = MVCache
        MVDataGridSort()
    End Sub

    Private Sub MVDataGridSort()
       ' Dim dvg As DataGridView = DataGridView.DataSource = MVCache
        Dim b = From f In MVCache
        
    End Sub
    
    Private Sub txtFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        If txtFilter.Text <> "" Then
            lstBxMainList.Items.Clear()
            For Each item In MVCache 'musicVideoList
                If item.title.ToLower.IndexOf(txtFilter.Text.ToLower) <> -1 Then
                    lstBxMainList.Items.Add(New ValueDescriptionPair(item.nfopathandfilename, item.title))
                End If
            Next
        Else
            For Each item In MVCache 'musicVideoList
                lstBxMainList.Items.Add(New ValueDescriptionPair(item.nfopathandfilename, item.title))
            Next
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
    
    Private Sub googleSearch()
        Dim title As String = txtTitle.Text
        Dim artist As String = txtArtist.Text

        Dim url As String = "http://images.google.com/images?q=" & title & "+" & artist
      
        Form1.OpenUrl(url)
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
            If Not Preferences.MVScraper = "wiki" Then
                MsgBox("Wiki scraper is not selected" & vbCrLf & "Unable to open this tab")
                TabControlMain.SelectedIndex = PrevTab
                Exit Sub
            End If
            Dim searchterm As String = getArtistAndTitle(CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value)
            Dim searchurl As String = "http://www.google.co.uk/search?hl=en-US&as_q=" & searchterm & "%20song&as_sitesearch=http://en.wikipedia.org/"

            WebBrowser1.Stop()
            WebBrowser1.ScriptErrorsSuppressed = True
            WebBrowser1.Navigate(searchurl)
            WebBrowser1.Refresh()
        End If
        PrevTab = TabControlMain.SelectedIndex
    End Sub
    
    Private Sub ScrnShtTimeAdjust(ByVal Direction As Boolean)
        Dim number As Integer = CInt(txtScreenshotTime.Text)
        If Direction Then
            number += 1
        Else
            If number > 1 Then
                number -= 1
            Else
                MsgBox("Cant be less than 1")
                Exit Sub
            End If
        End If
        txtScreenshotTime.Text = number.ToString
    End Sub

    Private Sub ManualScrape()
        If WebBrowser1.Url.ToString.ToLower.IndexOf("wikipedia.org") = -1 Or WebBrowser1.Url.ToString.ToLower.IndexOf("google") <> -1 Then
            MsgBox("You Must Browse to a Wikipedia Page")
            Exit Sub
        End If
        changeMVList.Clear()
        Dim messagestring As String = "Changing the movie will Overwrite all the current details"
        messagestring &= vbCrLf & "Do you wish to continue?"
        If MessageBox.Show(messagestring, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
            Exit Sub
        Else
            changefields = chkBxOverWriteArt.CheckState
            'ChangeMusicVideo(WebBrowser1.Url.ToString)  ' ucMusicVideo Routine
            TabControlMain.SelectedIndex = 0
            ChangeMV(WebBrowser1.Url.ToString, changefields)   ' For Testing through scraping routine.
        End If
    End Sub

    Private Sub ChangeMV(ByRef url As String, ByVal overwrite As Boolean)
        Dim videopath As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
        MVCacheRemove(videopath)
        changeMVList.Clear()
        changeMVList.Add(videopath)
        changeMVList.Add(url)
        changeMVList.Add(overwrite)
        Preferences.MusicVidScrape = True
        Form1.RunBackgroundMovieScrape("ChangeMusicVideo")
        While Form1.BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While
        loadMusicVideolist()
        'Call searchFornew(False)
    End Sub

    Private Sub MVPlay()
        Dim tempstring As String = ""
        Dim playlist As New List(Of String)
        Dim fullpathandfilename As String = CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value
        If Not String.IsNullOrEmpty(fullpathandfilename) Then
            tempstring = Utilities.GetFileName(fullpathandfilename)
            If tempstring <> "" Then playlist.Add(tempstring)
        End If
        If playlist.Count > 0 Then
            Call Form1.launchplaylist(playlist)
        End If
    End Sub

    
'All Buttons
#Region "Buttons"  
    
    'Scraping Buttons
    Private Sub btnSearchNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNew.Click
        Call SearchForNewMV()
    End Sub

    Private Sub btnManualScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManualScrape.Click
        ManualScrape()
    End Sub
    
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        lstBxMainList.Items.Clear()
        MVCacheLoadFromNfo
        MusicVideoCacheSave()
    End Sub

    'Path buttons
    Private Sub tPPref_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPPref.Leave
        Try
            If MVPrefChanged Then
                Dim save = MsgBox("You have made changes to some folders" & vbCrLf & "    Do you wish to save these changes?", MsgBoxStyle.YesNo)
                If save = DialogResult.Yes Then
                    btnMVApply.PerformClick()
                Else
                    MVPreferencesLoad()
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMVApply_Click(sender As System.Object, e As System.EventArgs) Handles btnMVApply.Click
        Preferences.MVidFolders.Clear()
        For f = 0 to clbxMvFolders.Items.Count-1
            Dim t As New str_RootPaths 
            t.rpath = clbxMvFolders.Items(f).ToString
            Dim chkstate As CheckState = clbxMvFolders.GetItemCheckState(f)
            t.selected = (chkstate = CheckState.Checked)
            Preferences.MVidFolders.Add(t)
        Next
        If rb_MvScr1.Checked Then
            Preferences.MVScraper = "wiki"
        ElseIf rb_MvScr2.Checked Then
            Preferences.MVScraper = "imvdb"
        Else
            Preferences.MVScraper = "audiodb"
        End If
        Preferences.ConfigSave()
        MVPrefChanged = False
    End Sub

    Private Sub btnAddFolderPath_Click(sender As System.Object, e As System.EventArgs) Handles btnAddFolderPath.Click
        Try
            If String.IsNullOrEmpty(tbFolderPath.Text) Then Exit Sub
            Dim tempstring As String = tbFolderPath.Text
            Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
                tempstring = tempstring.Substring(0, tempstring.Length - 1)
            Loop
            Dim exists As Boolean = False
            For Each item In clbxMvFolders.items
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
                    AuthorizeCheck = True
                    clbxMvFolders.Items.Add(tempstring, True)
                    clbxMvFolders.Refresh()
                    MVPrefChanged = True
                    AuthorizeCheck = False
                    tbFolderPath.Text = ""
                Else
                    Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If tempint = DialogResult.Yes Then
                        AuthorizeCheck = True
                        clbxMvFolders.Items.Add(tempstring, True)
                        clbxMvFolders.Refresh()
                        MVPrefChanged = True
                        AuthorizeCheck = False
                        tbFolderPath.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnBrowseFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFolders.Click
        Try
            Dim allok As Boolean = True
            Dim thefoldernames As String
            fb.Description = "Please Select Folder to Add"
            fb.ShowNewFolderButton = True
            fb.RootFolder = System.Environment.SpecialFolder.Desktop
            fb.SelectedPath = Preferences.lastpath
            Tmr.Start()
            If fb.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (fb.SelectedPath)
                Preferences.lastpath = thefoldernames
                If allok = True Then
                    AuthorizeCheck = True
                    clbxMvFolders.Items.Add(thefoldernames, True)
                    clbxMvFolders.Refresh()
                    MVPrefChanged = True
                    AuthorizeCheck = False
                Else
                    MsgBox("        Folder Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
    Private Sub clbxMvFolders_DragDrop(sender As Object, e As DragEventArgs) Handles clbxMvFolders.DragDrop
        Dim folders() As String
        Form1.droppedItems.Clear()
        folders = e.Data.GetData(DataFormats.filedrop)
        For f = 0 To UBound(folders)
            Dim exists As Boolean = False
            For Each rtpath In Preferences.movieFolders
                If rtpath.rpath = folders(f) Then
                    exists = True
                    Exit For
                End If
            Next
            If exists Then Continue For
            If clbxMvFolders.Items.Contains(folders(f)) Then Continue For
		    Dim skip As Boolean = False
		    For Each item In Form1.droppedItems
			    If item = folders(f) Then
				    skip = True
				    Exit For
			    End If
		    Next
		    If Not skip Then Form1.droppedItems.Add(folders(f))
        Next
        If Form1.droppedItems.Count < 1 Then Exit Sub
        AuthorizeCheck = True
        For Each item In Form1.droppedItems
            clbxMvFolders.Items.Add(item, True)
            MVPrefChanged = True
        Next
        AuthorizeCheck = False
        clbxMvFolders.Refresh()
    End Sub

    Private Sub clbxMvFolders_DragEnter(sender As Object, e As DragEventArgs) Handles clbxMvFolders.DragEnter
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub clbxMvFolders_KeyPress(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles clbxMvFolders.KeyDown
        If e.KeyCode = Keys.Delete AndAlso clbxMvFolders.SelectedItem <> Nothing
            Call btnRemoveFolder.PerformClick()
        ElseIf e.KeyCode = Keys.Space Then
            AuthorizeCheck = True
            Call clbxMvFolderstoggle()
            AuthorizeCheck = False
        End If
    End Sub

    Private Sub clbxMvFolders_MouseDown(sender As Object, e As MouseEventArgs) Handles clbxMvFolders.MouseDown 
        Dim loc As Point = Me.clbxMvFolders.PointToClient(Cursor.Position)
        For i As Integer = 0 To Me.clbxMvFolders.Items.Count - 1
	        Dim rec As Rectangle = Me.clbxMvFolders.GetItemRectangle(i)
	        rec.Width = 16
	        'checkbox itself has a default width of about 16 pixels
	        If rec.Contains(loc) Then
		        AuthorizeCheck = True
		        Dim newValue As Boolean = Not Me.clbxMvFolders.GetItemChecked(i)
		        Me.clbxMvFolders.SetItemChecked(i, newValue)
		        AuthorizeCheck = False
		        Return
	        End If
        Next
    End Sub

    Private Sub clbxMvFolders_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbxMvFolders.ItemCheck
        If Not AuthorizeCheck Then
	        e.NewValue = e.CurrentValue
            Exit Sub
        End If
        Static Updating As Boolean
        If Updating Then Exit Sub
        MVPrefChanged = True
        Updating = False
    End Sub

    Private Sub clbxMvFolderstoggle()
        Dim i = clbxMvFolders.SelectedIndex
        clbxMvFolders.SetItemCheckState(i, If(clbxMvFolders.GetItemCheckState(i) = CheckState.Checked, CheckState.Unchecked, CheckState.Checked))
    End Sub

    Private Sub btnRemoveFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFolder.Click
        Try
            While clbxMvFolders.SelectedItems.Count > 0
                clbxMvFolders.Items.Remove(clbxMvFolders.SelectedItems(0))
                MVPrefChanged = True
            End While
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
    'Poster/Image buttons
    Private Sub btnCreateScreenshot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateScreenshot.Click
        PcBxMusicVideoScreenShot.Image = Nothing
        pcBxScreenshot.Image = Nothing
        If Not lstBxMainList.SelectedItem Is Nothing Then
            For Each MusicVideo As MVComboList In MVCache 'In musicVideoList
                If MusicVideo.nfopathandfilename Is CType(lstBxMainList.SelectedItem, ValueDescriptionPair).Value Then
                    Dim screenshotpath As String = createScreenshot(MusicVideo.nfopathandfilename, txtScreenshotTime.Text, True)
                    
                    Form1.util_ImageLoad(PcBxMusicVideoScreenShot, screenshotpath, Utilities.DefaultTvFanartPath)
                    Form1.util_ImageLoad(pcBxScreenshot, screenshotpath, Utilities.DefaultTvFanartPath)
                End If
            Next
        End If
    End Sub
    
    Private Sub btnScreenshotMinus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScreenshotMinus.Click
        ScrnShtTimeAdjust(False)
    End Sub

    Private Sub btnScreenshotPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScreenshotPlus.Click
        ScrnShtTimeAdjust(True)
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

        WorkingWithNfoFiles.MVsaveNfo(workingMusicVideo.fileinfo.fullpathandfilename, workingMusicVideo)
    End Sub
    
    Private Sub btnCrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrop.Click
        'Form1.cropMode = "mvscreenshot"
        Try
            Using t As New frmMovPosterCrop
                If Preferences.MultiMonitoEnabled Then
                    t.bounds = screen.allscreens(form1.currentscreen).bounds
                    t.startposition = formstartposition.manual
                end if
                t.img = New Bitmap(pcBxScreenshot.Tag.ToString)
                t.cropmode = "fanart"
                t.title = workingMusicVideo.fullmoviebody.title 
                t.Setup()
                t.ShowDialog()
                If Not IsNothing(t.newimg) Then
                    btnSaveCrop.Enabled = True
                    btnCropReset.Enabled = True
                    pcBxScreenshot.Image = t.newimg
                End If
            End Using
            'Dim t As New frmMovPosterCrop
            'If Preferences.MultiMonitoEnabled Then
            '    t.Bounds = Screen.AllScreens(Form1.CurrentScreen).Bounds
            '    t.StartPosition = FormStartPosition.Manual
            'End If
            't.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnCropReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCropReset.Click
        Form1.util_ImageLoad(PcBxMusicVideoScreenShot, workingMusicVideo.fileinfo.fanartpath, Utilities.DefaultFanartPath)
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
    
    Private Sub btnPosterPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterPaste.Click
        If AssignClipboardImage(pcBxSinglePoster) Then
            btnPosterReset.Enabled = True
            btnPosterSave.Enabled = True
            Label16.Text = pcBxSinglePoster.Image.Width
            Label17.Text = pcBxSinglePoster.Image.Height
        End If
    End Sub

    Private Sub btnPasteFromClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPasteFromClipboard.Click
        If AssignClipboardImage(pcBxScreenshot) Then
            btnCropReset.Enabled = True
            btnSaveCrop.Enabled = True
            Label16.Text = pcBxScreenshot.Image.Width
            Label17.Text = pcBxScreenshot.Image.Height
        End If
    End Sub

    Private Sub btnPosterCrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterCrop.Click
        Try
            Using t As New frmMovPosterCrop
                If Preferences.MultiMonitoEnabled Then
                    t.bounds = screen.allscreens(form1.currentscreen).bounds
                    t.startposition = formstartposition.manual
                end if
                t.img = New Bitmap(pcBxSinglePoster.Tag.ToString)
                t.cropmode = "poster"
                t.title = workingMusicVideo.fullmoviebody.title 
                t.Setup()
                t.ShowDialog()
                If Not IsNothing(t.newimg) Then
                    btnPosterSave.Enabled = True
                    btnPosterReset.Enabled = True
                    pcBxSinglePoster.Image = t.newimg
                End If
            End Using
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnPosterReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosterReset.Click
        Form1.util_ImageLoad(pcBxSinglePoster, workingMusicVideo.fileinfo.posterpath, Utilities.DefaultPosterPath)
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
    
    Private Sub PcBxPoster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PcBxPoster.DoubleClick
        Try
            If Not PcBxPoster.Tag Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                Call Form1.util_ZoomImage(PcBxPoster.Tag.ToString)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub pcBxSinglePoster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcBxSinglePoster.DoubleClick
        Try
            If Not pcBxSinglePoster.Tag Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                Call Form1.util_ZoomImage(pcBxSinglePoster.Tag.ToString)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub pcBxScreenshot_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcBxScreenshot.DoubleClick
        Try
            If Not pcBxScreenshot.Tag Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                Call Form1.util_ZoomImage(pcBxScreenshot.Tag.ToString)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub PcBxMusicVideoScreenShot_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PcBxMusicVideoScreenShot.DoubleClick
        Try
            If Not PcBxMusicVideoScreenShot.Tag Is Nothing Then
                Form1.ControlBox = False
                Form1.MenuStrip1.Enabled = False
                Call Form1.util_ZoomImage(PcBxMusicVideoScreenShot.Tag.ToString)
            Else
                MsgBox("No Image Available To Zoom")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    'Misc
    Private Sub btnGoogleSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoogleSearch.Click, btnGoogleSearchPoster.Click
        Call googleSearch()
    End Sub

    Private Sub btnMVPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMVPlay.Click
        MVPlay()
    End Sub
    
#End Region

    Private Sub Tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr.Tick
        Dim hFb As IntPtr = FindWindowW("#32770", "Browse For Folder") '#32770 is the class name of a folderbrowser dialog
        If hFb <> IntPtr.Zero Then
            If SendMessageW(hFb, BFFM_SETEXPANDED, 1, fb.SelectedPath) = IntPtr.Zero Then
                Tmr.Stop()
            End If
        End If
    End Sub

    Private Sub rb_MvScr1_CheckedChanged(sender As Object, e As EventArgs) Handles rb_MvScr1.CheckedChanged, rb_MvScr2.CheckedChanged, rb_MvScr3.CheckedChanged
        MVPrefChanged = True
    End Sub

    Private Sub MV_DeleteNfoArtwork(Optional ByVal DelArtwork As Boolean = True)
        Dim lbxIndex As Integer = lstBxMainList.SelectedIndex
        Dim MVRemoved As Boolean = False
        If lbxIndex < 0 Then Exit Sub
        Dim MVCacheIndex As Integer = Nothing
        If MVCache.RemoveAll(Function(c) c.nfopathandfilename = workingMusicVideo.fileinfo.fullpathandfilename) = 1 Then
            Dim MVArt As String = workingMusicVideo.fileinfo.fanartpath
            If File.Exists(MVArt) Then Utilities.SafeDeleteFile(MVArt)
            MVArt = workingMusicVideo.fileinfo.posterpath
            If File.Exists(MVArt) Then Utilities.SafeDeleteFile(MVArt)
            Utilities.SafeDeleteFile(workingMusicVideo.fileinfo.fullpathandfilename)
            loadMusicVideolist
        End If
    End Sub

#Region "MVContextMenu Items"
    
    Private Sub tsmiMVPlay_Click(sender As Object, e As EventArgs) Handles tsmiMVPlay.Click
        MVPlay()
    End Sub

    Private Sub tsmiMVOpenFolder_Click(sender As Object, e As EventArgs) Handles tsmiMVOpenFolder.Click
        Try
            If Not workingMusicVideo.fileinfo.fullpathandfilename Is Nothing Then
                Call Form1.util_OpenFolder(workingMusicVideo.fileinfo.fullpathandfilename)
            Else
                MsgBox("There is no Movie selected to open")
            End If
        Catch

        End Try
    End Sub

    Private Sub tsmiMVViewNfo_Click(sender As Object, e As EventArgs) Handles tsmiMVViewNfo.Click
        Try
            Utilities.NfoNotepadDisplay(workingMusicVideo.fileinfo.fullpathandfilename, Preferences.altnfoeditor)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tsmiMVDelNfoArt_Click(sender As Object, e As EventArgs) Handles tsmiMVDelNfoArt.Click
        MV_DeleteNfoArtwork()
    End Sub
    
#End Region


#Region "garbage"


#End Region

End Class
