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
    Public DataGridViewBindingSource As New BindingSource
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
    Dim workingMV As New MVComboList 
    Dim LastMV As String = ""
    Dim PrevTab As Integer = 0
    Dim GridFieldToDisplay1 As String ="ArtistTitle"
    Dim GridFieldToDisplay2 As String ="A-Z"
    Dim GridInvert As Boolean = False

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
        cmbxMVSort.SelectedIndex = Preferences.MVsortorder
        If Preferences.MVdefaultlist = 0 Then
            rbMVArtistAndTitle.Checked = True
        ElseIf Preferences.MVdefaultlist = 1 Then
            rbMVTitleandYear.Checked = True
        ElseIf Preferences.MVdefaultlist = 2 Then
            rbMVFilename.Checked = True
        End If
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
        Form1.RunBackgroundMovieScrape("SearchForNewMusicVideo")
        While Form1.BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While
        loadMVDV1()
    End Sub

    Private Sub RefreshallMV()
        Preferences.MusicVidScrape = True
        Form1.RunBackgroundMovieScrape("RefreshMVCache")
        While Form1.BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While
        'MVCacheLoadFromNfo
        MVCacheSave()
        loadMVDV1()
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
    
    'Public Function getallfolders() As List(Of String)
    '    Dim allfolders As New List(Of String)
    '    oMovies.FindNewMusicVideos()
    '    Return allfolders
    'End Function
    
    Private Sub DisplayMV()
        Try
            DisplayMV(MVDgv1.SelectedCells, MVDgv1.SelectedRows)
        Catch
            Return
        End Try
    End Sub

    Private Sub DisplayMV(ByVal selectedCells As DataGridViewSelectedCellCollection, ByVal selectedRows As DataGridViewSelectedRowCollection)
        Try
            If selectedRows.Count = 1 Then
                If LastMV = selectedCells(0).Value.ToString Then Return
                LastMV = selectedCells(0).Value.ToString
            Else
                LastMV = ""
            End If
        Catch
            Return
        End Try
        If selectedRows.Count = 0 Then Exit Sub
        MVForm_Init()
        Dim query = From f In MVCache Where f.nfopathandfilename = selectedCells(0).Value.ToString
        Dim queryList As List(Of MVComboList) = query.ToList()

        If queryList.Count > 0 Then
            workingMV.nfopathandfilename = queryList(0).nfopathandfilename
            MVForm_Populate()
            'workingMovie.filedate = queryList(0).filedate
            'workingMovie.filename = queryList(0).filename
            'workingMovie.foldername = queryList(0).foldername
            'workingMovie.fullpathandfilename = queryList(0).fullpathandfilename
            'workingMovie.genre = queryList(0).genre
            'workingMovie.id = queryList(0).id
            'workingMovie.playcount = queryList(0).playcount
            'workingMovie.rating = queryList(0).Rating
            'workingMovie.title = queryList(0).title
            'workingMovie.top250 = queryList(0).top250
            'workingMovie.year = queryList(0).year
            'workingMovie.FolderSize = queryList(0).FolderSize
            'mov_ToolStripPlayTrailer.Visible = Not queryList(0).MissingTrailer
            'Call mov_FormPopulate(yielding)
        Else
            MVForm_Populate()
        End If

        'this is where we customize  controls when movie is displayed. ie toolbars visible etc.

    End Sub

    Private Sub MVForm_Populate()
        If Not String.IsNullOrEmpty(workingMV.nfopathandfilename) AndAlso MVDgv1.Rows.Count > 0 Then
            workingMusicVideo = WorkingWithNfoFiles.MVloadNfo(workingMV.nfopathandfilename)
            If IsNothing(workingMusicVideo) = False Then
                txtAlbum.Text = workingMusicVideo.fullmoviebody.album
                txtArtist.Text = workingMusicVideo.fullmoviebody.artist
                txtDirector.Text = workingMusicVideo.fullmoviebody.director
                txtFullpath.Text = workingMusicVideo.fileinfo.fullPathAndFilename
                txtPlot.Text = workingMusicVideo.fullmoviebody.plot
                Dim runtimestr As String = workingMusicVideo.fullmoviebody.runtime
                If runtimestr = "" OrElse runtimestr = "-1" Then
                    If workingMusicVideo.filedetails.filedetails_video.DurationInSeconds.Value <> "" AndAlso workingMusicVideo.filedetails.filedetails_video.DurationInSeconds.Value <> "-1" Then
                        runtimestr = Math.Floor((workingMusicVideo.filedetails.filedetails_video.DurationInSeconds.Value.ToInt)/60).ToString & " min"
                    End If
                Else
                    runtimestr &= " min"
                End If
                txtRuntime.Text = runtimestr   'workingMusicVideo.fullmoviebody.runtime
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
        End If
    End Sub
    
    Private Sub MVForm_Init()
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
    End Sub
    
    
#Region " Music Video Cache Routines"
    Public Sub MVCacheSave()
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

    Public Sub MVCacheLoad()
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
                                        newMV.CreateDate = "18500101000000" '01/01/1850 00:00:00
                                    Else
                                        newMV.CreateDate = detail.InnerText
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

        Call loadMVDV1()
        Try
            If MVDgv1.Rows.Count > 0 Then MVDgv1.Rows(0).Selected = True
            DisplayMV()
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
        MV_Load(t)
        loadMVDV1()
    End Sub

    Public Sub MVCacheAddScraped(tmpMV As FullMovieDetails)
        MVCacheadd(tmpMV)
        loadMVDV1()
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
        MVDgv1.DataSource = MVCache
        MVDgv1.DataSource = Nothing
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

    Private Sub loadMVDV1()
        MVDgv1.DataSource = Nothing
        MVDgv1.DataSource = MVCache
        MVDataGridSort()
        mv_FiltersAndSortApply()
    End Sub

    Private Sub MVDataGridSort()
        GridviewMovieDesign()
    End Sub
    
    Private Sub txtFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.KeyDown, txtFilter.ModifiedChanged
        Try
            Application.DoEvents()
            If txtFilter.Text.Length > 0 Then
                txtFilter.BackColor = Color.DarkOrange
            Else
                txtFilter.BackColor = Color.White
            End If
            txtFilter.Refresh()
            mv_FiltersAndSortApply()
            DisplayMV()
        Catch ex As Exception

        End Try
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
            Dim searchterm As String = getArtistAndTitle(workingMusicVideo.fileinfo.fullpathandfilename)
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
        Dim videopath As String = workingMusicVideo.fileinfo.fullpathandfilename
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
        loadMVDV1()
    End Sub

    Private Sub MVPlay()
        Dim tempstring As String = ""
        Dim playlist As New List(Of String)
        Dim fullpathandfilename As String = workingMusicVideo.fileinfo.fullpathandfilename 
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
        RefreshallMV()
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
        TabControlMain.SelectedIndex = 0
        RefreshallMV()
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
        Try
            PcBxMusicVideoScreenShot.Image = Nothing
            pcBxScreenshot.Image = Nothing
            Dim screenshotpath As String = createScreenshot(workingMusicVideo.fileinfo.fullpathandfilename, txtScreenshotTime.Text, True)
                    
            Form1.util_ImageLoad(PcBxMusicVideoScreenShot, screenshotpath, Utilities.DefaultTvFanartPath)
            Form1.util_ImageLoad(pcBxScreenshot, screenshotpath, Utilities.DefaultTvFanartPath)
        Catch
        End Try
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
        Dim thumbpathandfilename As String = workingMusicVideo.fileinfo.fullpathandfilename.Replace(IO.Path.GetExtension(workingMusicVideo.fileinfo.fullpathandfilename), "-fanart.jpg")
        bitmap3.Save(thumbpathandfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
        bitmap3.Dispose()
        btnCropReset.Enabled = False
        btnSaveCrop.Enabled = False
        Form1.util_ImageLoad(PcBxMusicVideoScreenShot, thumbpathandfilename, Utilities.DefaultTvFanartPath)
        Form1.util_ImageLoad(pcBxScreenshot, thumbpathandfilename, Utilities.DefaultTvFanartPath)
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
        Dim thumbpathandfilename As String = workingMusicVideo.fileinfo.fullpathandfilename.Replace(IO.Path.GetExtension(workingMusicVideo.fileinfo.fullpathandfilename), "-poster.jpg")
        bitmap3.Save(thumbpathandfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
        bitmap3.Dispose()
        btnCropReset.Enabled = False
        btnSaveCrop.Enabled = False
        Form1.util_ImageLoad(PcBxPoster, thumbpathandfilename, Utilities.DefaultPosterPath)
        Form1.util_ImageLoad(pcBxSinglePoster, thumbpathandfilename, Utilities.DefaultPosterPath)
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
    
    Private Sub btn_MVSortReset_Click(sender As Object, e As EventArgs) Handles btn_MVSortReset.Click
        cmbxMVSort.SelectedIndex = 0
        txtFilter.Text = ""
        rbMVArtistAndTitle.Checked = True
    End Sub

    Private Sub cmbxMVSort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxMVSort.SelectedIndexChanged
        GridFieldToDisplay2 = cmbxMVSort.text
        mv_FiltersAndSortApply()
        DisplayMV()
        If Not Form1.MainFormLoadedStatus Then Exit Sub
        Preferences.MVsortorder = cmbxMVSort.SelectedIndex 
    End Sub
    
    Private Sub rbMVArtistAndTitle_CheckedChanged(sender As Object, e As EventArgs) Handles rbMVArtistAndTitle.CheckedChanged
        HandleMovieList_DisplayChange("ArtistTitle")
        If Not cmbxMVSort.SelectedIndex = 0 Then
            cmbxMVSort.SelectedIndex = 0
        Else
            cmbxMVSort_SelectedIndexChanged(cmbxMVSort, EventArgs.Empty)
        End If
    End Sub

    Private Sub rbMVTitleandYear_CheckedChanged(sender As Object, e As EventArgs) Handles rbMVTitleandYear.CheckedChanged
        HandleMovieList_DisplayChange("TitleYear")
        If Not cmbxMVSort.SelectedIndex = 0 Then
            cmbxMVSort.SelectedIndex = 0
        Else
            cmbxMVSort_SelectedIndexChanged(cmbxMVSort, EventArgs.Empty)
        End If
    End Sub

    Private Sub rbMVFilename_CheckedChanged(sender As Object, e As EventArgs) Handles rbMVFilename.CheckedChanged
        HandleMovieList_DisplayChange("FileName")
        If Not cmbxMVSort.SelectedIndex = 0 Then
            cmbxMVSort.SelectedIndex = 0
        Else
            cmbxMVSort_SelectedIndexChanged(cmbxMVSort, EventArgs.Empty)
        End If
    End Sub

#End Region

#Region "MVDGV  Setup"
    Public Sub GridviewMovieDesign()
        'If Not Form1.MainFormLoadedStatus Then Exit Sub
        Dim dgv As DataGridView = MVDgv1
        If dgv.Columns.Count < 27 Then Return
        Cursor.Current = Cursors.WaitCursor
        While dgv.Columns(0).CellType.Name="DataGridViewImageCell"
            dgv.Columns.Remove(dgv.Columns(0))
        End While
        
        Dim header_style As New DataGridViewCellStyle
        header_style.ForeColor = Color.White
        header_style.BackColor = Color.ForestGreen
        header_style.Font      = new Font(dgv.Font, FontStyle.Bold)
        For Each col As DataGridViewcolumn in dgv.Columns
            col.HeaderCell.Style = header_style
        Next
        dgv.EnableHeadersVisualStyles = False
        For Each column As DataGridViewColumn In dgv.Columns
            column.Resizable = DataGridViewTriState.True
            column.ReadOnly  = True
            column.SortMode  = DataGridViewColumnSortMode.Automatic
            column.Visible   = False
        Next

        'Highlight titles in datagridview with missing video files.
        If Preferences.incmissingmovies Then
            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("videomissing").Value = True Then
                    row.DefaultCellStyle.BackColor = Color.Red                
                End If
            Next
        End If
        dgv.RowHeadersVisible = False
        If GridFieldToDisplay1="ArtistTitle" Then
            IniColumn(dgv,"Title"               ,GridFieldToDisplay2= "Year","Title"       )
            IniColumn(dgv,"ArtistTitleYear"     ,GridFieldToDisplay2<>"Year","Title & Year")
        End If
        If GridFieldToDisplay1="TitleYear" Then
            IniColumn(dgv,"Title"         ,GridFieldToDisplay2= "Year","Title"       )
            IniColumn(dgv,"TitleYear"     ,GridFieldToDisplay2<>"Year","Title & Year")
        End If
        IniColumn(dgv,"filename"         ,GridFieldToDisplay1="FileName"    ,"File name"                                                                   )
        IniColumn(dgv,"year"             ,GridFieldToDisplay2="Year"        ,"Movie year"       ,"Year"    , -20                                           )
        IniColumn(dgv,"runtime"          ,GridFieldToDisplay2="Runtime"     ,"Runtime"          ,          , -20, DataGridViewContentAlignment.MiddleRight )
        IniColumn(dgv,"DisplayCreateDate",GridFieldToDisplay2="DateAdded"   ,"Date Added"       ,"Added"                                                   )
        SetFirstColumnWidth(dgv)
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub IniColumn(dgv As DataGridView, name As String, visible As Boolean, Optional toolTip As String=Nothing, Optional headerText As String=Nothing, Optional widthAdjustment As Integer=0, Optional alignment As DataGridViewContentAlignment=Nothing )

        Dim col As DataGridViewColumn = dgv.Columns(name)
        If IsNothing(toolTip) Then toolTip = Utilities.TitleCase(name)  'CapsFirstLetter(name)
        col.Visible     = visible
        col.ToolTipText = toolTip
        col.HeaderText  = If(IsNothing(headerText),toolTip,headerText)
        SetColWidth(col,widthAdjustment)
       
        If Not IsNothing(alignment) Then
            Dim header_style As New DataGridViewCellStyle
            header_style.ForeColor = Color.White
            header_style.BackColor = Color.ForestGreen
            header_style.Font      = new Font(dgv.Font, FontStyle.Bold)
            header_style.Alignment = alignment
            col.HeaderCell.Style = header_style
            col.DefaultCellStyle.Alignment = alignment
        End If
    End Sub
    
    Function CapsFirstLetter(words As String)
        Return Form1.MyCulture.TextInfo.ToTitleCase(words)
    End Function
    
    Sub SetColWidth(col As DataGridViewColumn, Optional widthAdjustment As Integer=0)
        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells    'Set auto-size mode
        Dim initialAutoSizeWidth As Integer = col.Width                 'Save calculated width after auto-sizing
        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet        'Revert sizing mode to default
        col.Width = initialAutoSizeWidth+widthAdjustment                'Set width to calculated auto-size - adjustment needed because header has excess padding
    End Sub
    
    Sub SetFirstColumnWidth(dgvMovies As DataGridView)
        Try
            Dim firstColWidth As Integer = dgvMovies.Width - 17
            
            If Not IsNothing(dgvMovies.Columns("Watched")) AndAlso dgvMovies.Columns("Watched").Visible then firstColWidth -= dgvMovies.Columns("Watched").Width
            
            If GridFieldToDisplay2 = "Year" Then firstColWidth -= dgvMovies.Columns("Year"             ).Width
            If GridFieldToDisplay2 = "Runtime"    Then firstColWidth -= dgvMovies.Columns("runtime"          ).Width
            If GridFieldToDisplay2 = "DateAdded" Then firstColWidth -= dgvMovies.Columns("DisplayCreateDate").Width
            
            If firstColWidth>0 Then
                If Not IsNothing(dgvMovies.Columns("filename"           )) Then dgvMovies.Columns("filename"            ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("ArtistTitle"        )) Then dgvMovies.Columns("ArtistTitle"         ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("ArtistTitleYear"    )) Then dgvMovies.Columns("ArtistTitleYear"     ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("Title"              )) Then dgvMovies.Columns("Title"               ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("TitleYear"          )) Then dgvMovies.Columns("TitleYear"           ).Width = firstColWidth
            End If
        Catch
        End Try
    End Sub

    Public Sub mv_FiltersAndSortApply(Optional ByVal Force As Boolean = False) 'Form1 As Form1)

        If Not Form1.MainFormLoadedStatus AndAlso Not Force Then Exit Sub
 
        Dim b = From f In MVCache
       
        If txtFilter.Text.ToUpper<>"" Then
            If rbMVArtistAndTitle.Checked Then
                b = From f In b Where f.ArtistTitle.ToUpper.Contains(txtFilter.Text.ToUpper)
            ElseIf rbMVTitleandYear.Checked Then
                b = From f In b Where f.TitleUcase.Contains(txtFilter.Text.ToUpper)
            ElseIf Form1.rbFileName.Checked Then
                b = From f In b Where f.filename.ToLower.Contains(txtFilter.Text.ToLower)
            End If
        End If

        'General
        'If Form1.cbFilterGeneral.Visible Then
        '    Select Form1.cbFilterGeneral.Text.RemoveAfterMatch
        '        Case "Watched"                     : b = From f In b Where     f.Watched
        '        Case "Unwatched"                   : b = From f In b Where Not f.Watched
        '        Case "Scrape Error"                : b = From f In b Where f.genre.ToLower = "problem"
        '        Case "Duplicates"                  : Dim sort = b.Where(Function(y) y.id<>"0").GroupBy(Function(f) f.id) : b = sort.Where(Function(x) x.Count>1).SelectMany(Function(x) x).ToList
        '        Case "Missing Poster"              : b = From f In b Where f.MissingPoster
        '        Case "Missing Fanart"              : b = From f In b Where f.MissingFanart
        '        Case "Missing Plot"                : b = From f In b Where f.MissingPlot
        '        Case "Missing Genre"               : b = From f In b Where f.MissingGenre
        '        Case "Missing Runtime"             : b = From f In b Where f.MissingRuntime
        '        Case "Missing Year"                : b = From f In b Where f.MissingYear
        '        Case "Missing Certificate"         : b = From f In b Where f.MissingCertificate
        '        Case "Missing Source"              : b = From f In b Where f.MissingSource
        '        Case "Missing Director"            : b = From f In b Where f.MissingDirector
        '        Case "Missing from XBMC"           : b = b.Where( Function(x) Form1.MC_Only_Movies_Nfos.Contains(x.fullpathandfilename) )
        '        Case "Pre-Frodo poster only"       : b = From f In b Where     f.PreFrodoPosterExists And Not f.FrodoPosterExists
        '        Case "Frodo poster only"           : b = From f In b Where Not f.PreFrodoPosterExists And     f.FrodoPosterExists
        '        Case "Both poster formats"         : b = From f In b Where     f.PreFrodoPosterExists And     f.FrodoPosterExists
        '    End Select
        'End If
   
'        If Form1.cbFilterRuntime .Visible Then b = From f In b Where f.IntRuntime >= Form1.cbFilterRuntime .SelectedMin and f.IntRuntime <= Form1.cbFilterRuntime .SelectedMax   
'        If Form1.cbFilterYear  .Visible Then b = From f In b Where f.year   >= Form1.cbFilterYear  .SelectedMin and f.year   <= Form1.cbFilterYear  .SelectedMax     'Year
'        If Form1.cbFilterCountries             .Visible Then b = Form1.oMovies.ApplyCountiesFilter              ( b , Form1.cbFilterCountries             )
'        If Form1.cbFilterStudios               .Visible Then b = Form1.oMovies.ApplyStudiosFilter               ( b,  Form1.cbFilterStudios               )
'        If Form1.cbFilterGenre                 .Visible Then b = Form1.oMovies.ApplyGenresFilter                ( b , Form1.cbFilterGenre                 )
'        If Form1.cbFilterResolution            .Visible Then b = Form1.oMovies.ApplyResolutionsFilter           ( b , Form1.cbFilterResolution            )
'        If Form1.cbFilterVideoCodec            .Visible Then b = Form1.oMovies.ApplyVideoCodecFilter            ( b , Form1.cbFilterVideoCodec            )
'        If Form1.cbFilterAudioCodecs           .Visible Then b = Form1.oMovies.ApplyAudioCodecsFilter           ( b , Form1.cbFilterAudioCodecs           )
'        If Form1.cbFilterAudioChannels         .Visible Then b = Form1.oMovies.ApplyAudioChannelsFilter         ( b , Form1.cbFilterAudioChannels         )
'        If Form1.cbFilterAudioBitrates         .Visible Then b = Form1.oMovies.ApplyAudioBitratesFilter         ( b , Form1.cbFilterAudioBitrates         )
'        If Form1.cbFilterNumAudioTracks        .Visible Then b = Form1.oMovies.ApplyNumAudioTracksFilter        ( b , Form1.cbFilterNumAudioTracks        )
'        If Form1.cbFilterAudioLanguages        .Visible Then b = Form1.oMovies.ApplyAudioLanguagesFilter        ( b , Form1.cbFilterAudioLanguages        )
'        If Form1.cbFilterAudioDefaultLanguages .Visible Then b = Form1.oMovies.ApplyAudioDefaultLanguagesFilter ( b , Form1.cbFilterAudioDefaultLanguages )
'        If Form1.cbFilterActor                 .Visible Then b = Form1.oMovies.ApplyActorsFilter                ( b , Form1.cbFilterActor                 )
'        If Form1.cbFilterDirector              .Visible Then b = Form1.oMovies.ApplyDirectorsFilter             ( b , Form1.cbFilterDirector              )       
 
        Select Case cmbxMVSort.Text
            Case "A-Z"
                If Not GridInvert Then
                    If GridFieldToDisplay1="ArtistTitle" Then
                        b = From f In b Order By f.ArtistTitle Ascending
                    ElseIf GridFieldToDisplay1="TitleYear" Then
                        b = From f In b Order By f.Title Ascending
                    Else
                        b = From f In b Order By f.filename Ascending
                    End If
                Else
                    If GridFieldToDisplay1="ArtistTitle" Then
                        b = From f In b Order By f.ArtistTitle Descending
                    ElseIf GridFieldToDisplay1="TitleYear" Then
                        b = From f In b Order By f.Title Descending
                    Else
                        b = From f In b Order By f.filename Descending
                    End If
                End If
            Case "Year"
                If Not GridInvert Then
                    b = From f In b Order By f.year Ascending
                Else
                    b = From f In b Order By f.year Descending
                End If
            Case "Runtime"
                If Not GridInvert Then
                    b = From f In b Order By f.IntRuntime Ascending        
                Else
                    b = From f In b Order By f.IntRuntime Descending
                End If
            Case "DateAdded"
                If Not GridInvert Then
                    b = From f In b Order By f.Createdate Descending
                Else
                    b = From f In b Order By f.Createdate Ascending
                End If
            'Case "Modified"
            '    If GridSort = "Asc" Then
            '        b = From f In b Order By f.filedate Ascending
            '    Else
            '        b = From f In b Order By f.filedate Descending
            '    End If
            'Case "Folder Size"
            '    If GridSort = "Asc" Then
            '        b = From f In b Order By f.FolderSize Ascending
            '    Else
            '        b = From f In b Order By f.FolderSize Descending
            '    End If
        End Select
        
        Dim lst = b.ToList
        DataGridViewBindingSource.DataSource = lst
        MVDgv1                   .DataSource = DataGridViewBindingSource
        GridviewMovieDesign()
    End Sub

    Sub HandleMovieList_DisplayChange(DisplayField As String)
        GridFieldToDisplay1 = DisplayField
        If rbMVArtistAndTitle.Checked Then Preferences.moviedefaultlist = 0
        If rbMVTitleandYear  .Checked Then Preferences.moviedefaultlist = 1
        If rbMVFilename      .Checked Then Preferences.moviedefaultlist = 2
        GridviewMovieDesign()
        If Form1.MainFormLoadedStatus Then
            DisplayMV()
        End If
    End Sub

#End Region   'MVDGV Setup

#Region "MVDgv1 Handlers"

    Private Sub MVDgv1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles MVDgv1.CellClick
        DisplayMV()
    End Sub

    Private Sub MVDgv1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MVDgv1.ColumnHeaderMouseClick
        GridInvert = Not GridInvert
        mv_FiltersAndSortApply()
        DisplayMV()
    End Sub

    Private Sub MVDgv1_DoubleClick(sender As Object, e As MouseEventArgs) Handles MVDgv1.DoubleClick
        Try
            Dim info = MVDgv1.HitTest(e.X, e.Y)
            If info.ColumnX = -1 Then Return
            Try
                If IsNumeric(MVDgv1.SelectedCells(1).Value.ToString) Then Return
            Catch
                Return
            End Try
            If info.Type <> DataGridViewHitTestType.ColumnHeader Then
                'mov_Play("Movie")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub MVDgv1_MouseUp(sender As Object, e As MouseEventArgs) Handles MVDgv1.MouseUp
        Try
            If e.Button = MouseButtons.Right AndAlso MVDgv1.RowCount > 0 Then
                tsmiMVName.Text = "'" & MVDgv1.SelectedCells(3).Value.ToString & "'"
            End If
        Catch
        End Try
    End Sub

#End Region     'MVDgv1 Handlers

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
        If MVDgv1.RowCount = 0 AndAlso MVDgv1.SelectedRows.Count < 1 Then Exit Sub
        Dim MVRemoved As Boolean = False
        Dim MVCacheIndex As Integer = Nothing
        Dim MVDGVRowIndex As Integer = MVDgv1.CurrentRow.index
        If MVCache.RemoveAll(Function(c) c.nfopathandfilename = workingMusicVideo.fileinfo.fullpathandfilename) = 1 Then
            Dim MVArt As String = workingMusicVideo.fileinfo.fanartpath
            If File.Exists(MVArt) Then Utilities.SafeDeleteFile(MVArt)
            MVArt = workingMusicVideo.fileinfo.posterpath
            If File.Exists(MVArt) Then Utilities.SafeDeleteFile(MVArt)
            Utilities.SafeDeleteFile(workingMusicVideo.fileinfo.fullpathandfilename)
            MVDgv1.DataSource = Nothing
            loadMVDV1()
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
