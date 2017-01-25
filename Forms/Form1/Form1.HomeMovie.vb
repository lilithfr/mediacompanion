Imports System.Text.RegularExpressions
Imports Media_Companion.WorkingWithNfoFiles
Imports Media_Companion
Imports Media_Companion.Pref
Imports System.Linq

Partial Public Class Form1
    Public Shared homemovielist As New List(Of str_BasicHomeMovie)
    Public WorkingHomeMovie As New HomeMovieDetails
	Public hmfolderschanged             As Boolean = False


#Region "Browser Tab"

	Private Sub PlayHomeMovieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayHomeMovieToolStripMenuItem.Click
		mov_Play("HomeMovie")
	End Sub

	Private Sub OpenFolderToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFolderToolStripMenuItem.Click
		Try
			If Not WorkingHomeMovie.fileinfo.fullpathandfilename Is Nothing Then
				Call util_OpenFolder(WorkingHomeMovie.fileinfo.fullpathandfilename)
			Else
				MsgBox("There is no Movie selected to open")
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub OpenFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFileToolStripMenuItem.Click
		Try
			Utilities.NfoNotepadDisplay(WorkingHomeMovie.fileinfo.fullpathandfilename, Pref.altnfoeditor)
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub pbx_HmFanart_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbx_HmFanart.DoubleClick
		Try
			If WorkingHomeMovie.fileinfo.fanartpath <> Nothing Then
				If File.Exists(WorkingHomeMovie.fileinfo.fanartpath) Then
					Me.ControlBox = False
					MenuStrip1.Enabled = False
					util_ZoomImage(WorkingHomeMovie.fileinfo.fanartpath)
				End If
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub pbx_HmPoster_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbx_HmPoster.DoubleClick
		Try
			If WorkingHomeMovie.fileinfo.posterpath <> Nothing Then
				If File.Exists(WorkingHomeMovie.fileinfo.posterpath) Then
					Me.ControlBox = False
					MenuStrip1.Enabled = False
					util_ZoomImage(WorkingHomeMovie.fileinfo.posterpath)
				End If
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub SearchForNewHomeMoviesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchForNewHomeMoviesToolStripMenuItem.Click
        Pref.HomeVidScrape = True
		'Call homeMovieScan()
        RunBackgroundMovieScrape("HomeVidScrape")
        While BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While
        Pref.HomeVidScrape = False
	    loadhomemovielist()
	End Sub

	Private Sub RebuildHomeMovieCacheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RebuildHomeMovieCacheToolStripMenuItem.Click
		Call rebuildHomeMovies()
	End Sub

	Private Sub lb_HomeMovies_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_HomeMovies.DoubleClick
		mov_Play("HomeMovie")
	End Sub

	Private Sub lb_HomeMovies_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lb_HomeMovies.MouseDown

	End Sub

	Private Sub lb_HomeMovies_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lb_HomeMovies.MouseUp
		Try
			Dim ptIndex As Integer = lb_HomeMovies.IndexFromPoint(e.X, e.Y)
			If e.Button = MouseButtons.Right AndAlso ptIndex > -1 AndAlso lb_HomeMovies.SelectedItems.Count > 0 Then
				Dim newSelection As Boolean = True
				'If more than one movie is selected, check if right-click is on the selection.
				If lb_HomeMovies.SelectedItems.Count > 1 And lb_HomeMovies.GetSelected(ptIndex) Then
					newSelection = False
				End If
				'Otherwise, bring up the context menu for a single movie
                
				If newSelection Then
					lb_HomeMovies.SelectedIndex = ptIndex
					'update context menu with movie name & also if we show the 'Play Trailer' menu item
					PlaceHolderforHomeMovieTitleToolStripMenuItem.BackColor = Color.Honeydew
					PlaceHolderforHomeMovieTitleToolStripMenuItem.Text = "'" & lb_HomeMovies.Text & "'"
					PlaceHolderforHomeMovieTitleToolStripMenuItem.Font = New Font("Arial", 10, FontStyle.Bold)
				End If
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub lb_HomeMovies_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_HomeMovies.SelectedValueChanged
		If lb_HomeMovies.SelectedIndex < 0 Then Exit Sub
		Try
			For Each homemovie In homemovielist
				If homemovie.FullPathAndFilename Is CType(lb_HomeMovies.SelectedItem, ValueDescriptionPair).Value Then
					WorkingHomeMovie.fileinfo.fullpathandfilename = CType(lb_HomeMovies.SelectedItem, ValueDescriptionPair).Value
					Call loadhomemoviedetails()
				End If
			Next
		Catch
		End Try

	End Sub

	Private Sub btnHomeMovieSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeMovieSave.Click
		If HmMovTitle.Text <> "" Then
			WorkingHomeMovie.fullmoviebody.title = HmMovTitle.Text
		End If
		If HmMovSort.Text <> "" Then
			WorkingHomeMovie.fullmoviebody.sortorder = HmMovSort.Text
		End If
		WorkingHomeMovie.fullmoviebody.year     = HmMovYear.Text
		WorkingHomeMovie.fullmoviebody.plot     = HmMovPlot.Text
		WorkingHomeMovie.fullmoviebody.stars    = HmMovStars.Text
        WorkingHomeMovie.fullmoviebody.genre    = HmMovGenre.Text
		WorkingWithNfoFiles.nfoSaveHomeMovie(WorkingHomeMovie.fileinfo.fullpathandfilename, WorkingHomeMovie)
	End Sub

	Private Sub btn_HMSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_HMSearch.Click
		Pref.HomeVidScrape = True
		'Call homeMovieScan()
        RunBackgroundMovieScrape("HomeVidScrape")
        While BckWrkScnMovies.IsBusy
            Application.DoEvents()
        End While
        Pref.HomeVidScrape = False
        loadhomemovielist()
	End Sub

	Private Sub btn_HMRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_HMRefresh.Click
		Call rebuildHomeMovies()
	End Sub

    Private Sub HmMovGenre_MouseDown(sender As Object, e As MouseEventArgs) Handles HmMovGenre.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Try
                Dim item() As String = WorkingHomeMovie.fullmoviebody.genre.Split("/")
                Dim genre As String = ""
                Dim listof As New List(Of str_genre)
                listof.Clear()
                For Each i In item
                    If i = "" Then Continue For
                    Dim g As str_genre
                    g.genre = i.Trim
                    g.count = 1
                    listof.Add(g)
                Next
                Dim frm As New frmGenreSelect 
                frm.multicount = 1
                frm.SelectedGenres = listof
                frm.Init()
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    listof.Clear()
                    listof.AddRange(frm.SelectedGenres)
                    For each g In listof
                        If g.count = 0 Then Continue For
                        If genre = "" Then
                            genre = g.genre
                        Else
                            genre += " / " & g.genre
                        End If
                    Next
                    WorkingHomeMovie.fullmoviebody.genre = genre
                    HmMovGenre.Text = genre
                    WorkingWithNfoFiles.nfoSaveHomeMovie(WorkingHomeMovie.fileinfo.fullpathandfilename, WorkingHomeMovie, True)
                End If
            Catch
            End Try
        End If
    End Sub

#End Region

#Region "Home fanart"

	Private Sub btn_HmFanartShot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_HmFanartShot.Click
		HmScreenshot_Save()
	End Sub

	Private Sub tb_HmFanartTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tb_HmFanartTime.KeyPress
		If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
			If tb_HmFanartTime.Text <> "" AndAlso Convert.ToInt32(tb_HmFanartTime.Text) > 0 Then
				HmScreenshot_Load()
			End If
		End If
		If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
			e.Handled = True
		End If
	End Sub

	Private Sub tb_HmFanartTime_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_HmFanartTime.Leave
		If tb_HmFanartTime.Text = "" Then
			MsgBox("Please enter a numerical value >0 into the textbox")
			tb_HmFanartTime.Focus()
		ElseIf Convert.ToInt32(tb_HmFanartTime.Text) = 0 Then
			MsgBox("Please enter a numerical value >0 into the textbox")
			tb_HmFanartTime.Focus()
		End If
	End Sub

	Private Sub btn_HmFanartGet_Click(sender As Object, e As EventArgs) Handles btn_HmFanartGet.Click
		HmScreenshot_Load()
	End Sub

	Private Sub HmScreenshot_Save()
		Try
			pbx_HmFanart.Image = Nothing
			Dim screenshotpath As String = WorkingHomeMovie.fileinfo.fanartpath
			If screenshotpath = "" Then screenshotpath = WorkingHomeMovie.fileinfo.fullpathandfilename.Replace(".nfo", "-fanart.jpg")
			Dim cachepathandfilename As String = pbx_HmFanartSht.Tag.ToString
			File.Copy(cachepathandfilename, screenshotpath, True)
			util_ImageLoad(pbx_HmFanart, cachepathandfilename, Utilities.DefaultTvFanartPath)
			Dim video_flags = VidMediaFlags(WorkingHomeMovie.filedetails)
			movieGraphicInfo.OverlayInfo(pbx_HmFanart, "", video_flags)
		Catch
		End Try
	End Sub

	Private Sub HmScreenshot_Load()
		Dim Cachename As String = HmGetScreenShot()
		If Cachename = "" Then
			MsgBox("Unable to get screenshots from HomeVideo file")
			Exit Sub
		End If
		Try
			Dim matches() As Control
			For i = 0 To 4
				matches = Me.Controls.Find("pbHmScrSht" & i, True)
				If matches.Length > 0 Then
					Dim pb As PictureBox = DirectCast(matches(0), PictureBox)
					pb.SizeMode = PictureBoxSizeMode.StretchImage
					Dim image2load As String = Cachename.Substring(0, Cachename.Length - 5) & i.ToString & ".jpg"
					Form1.util_ImageLoad(pb, image2load, Utilities.DefaultTvFanartPath)
				End If
			Next
			If Not IsNothing(pbHmScrSht0.Image) Then Form1.util_ImageLoad(pbx_HmFanartSht, pbHmScrSht0.Tag.ToString, Utilities.DefaultTvFanartPath)
		Catch
		End Try
	End Sub

	Private Function HmGetScreenShot() As String
		Try
			If tb_HmFanartTime.Text = "" OrElse Convert.ToInt32(tb_HmFanartTime.Text) < 1 Then tb_HmFanartTime.Text = Pref.HmFanartTime.ToString
			If IsNumeric(tb_HmFanartTime.Text) Then
				Dim path As String = WorkingHomeMovie.fileinfo.fullpathandfilename.Replace(".nfo", "-fanart.jpg")
				Dim tempstring2 As String = WorkingHomeMovie.fileinfo.filenameandpath
				If File.Exists(tempstring2) Then
					Dim seconds = Convert.ToInt32(tb_HmFanartTime.Text)
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
					Application.DoEvents()
					Dim cachepathandfilename As String = Utilities.CreateScrnShotToCache(tempstring2, path, seconds, 5, 10)
					If cachepathandfilename <> "" Then
						Return cachepathandfilename
					End If
				End If
			End If
		Catch
		End Try
		Return ""
	End Function

	Private Sub pbHmScrSht_click(ByVal sender As Object, ByVal e As EventArgs)
		Dim pb As PictureBox = sender
		If IsNothing(pb.Image) Then Exit Sub
		Form1.util_ImageLoad(pbx_HmFanartSht, pb.Tag, Utilities.DefaultTvFanartPath)
	End Sub

#End Region

#Region "Home poster"

	Private Sub btn_HmPosterShot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_HmPosterShot.Click
		Try
			If IsNumeric(tb_HmPosterTime.Text) Then
				Dim thumbpathandfilename As String = Path.Combine(Utilities.CacheFolderPath, WorkingHomeMovie.fileinfo.posterpath.Replace(WorkingHomeMovie.fileinfo.path, ""))
				Dim pathandfilename As String = WorkingHomeMovie.fileinfo.fullpathandfilename.Replace(".nfo", "")
				messbox = New frmMessageBox("ffmpeg is working to capture the desired screenshot", "", "Please Wait")
				Dim aok As Boolean = False
				For Each ext In Utilities.VideoExtensions
					Dim tempstring2 As String = pathandfilename & ext
					If File.Exists(tempstring2) Then
						pathandfilename = tempstring2
						aok = True
						Exit For
					End If
				Next
				If aok Then
					Dim seconds As Integer = 10
					If Convert.ToInt32(tb_HmPosterTime.Text) > 0 Then
						seconds = Convert.ToInt32(tb_HmPosterTime.Text)
					End If
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
					messbox.Show()
					messbox.Refresh()
					Application.DoEvents()
					aok = Utilities.CreateScreenShot(pathandfilename, thumbpathandfilename, seconds, True)
					messbox.Close()
					If aok Then
						Dim cancelclicked As Boolean
						Using pbx As New PictureBox
							util_ImageLoad(pbx, thumbpathandfilename, Utilities.DefaultPosterPath)
							Using t As New frmMovPosterCrop
								If Pref.MultiMonitoEnabled Then
									t.Bounds = Screen.AllScreens(Form1.CurrentScreen).Bounds
									t.StartPosition = FormStartPosition.Manual
								End If
								t.img = pbx.Image
								t.cropmode = "poster"
								t.title = WorkingHomeMovie.fullmoviebody.title
								t.Setup()
								t.ShowDialog()
								If Not IsNothing(t.newimg) Then
									Utilities.SaveImage(t.newimg, WorkingHomeMovie.fileinfo.posterpath)
								Else
									cancelclicked = True
								End If
							End Using
						End Using

						Utilities.SafeDeleteFile(thumbpathandfilename)

						If File.Exists(WorkingHomeMovie.fileinfo.posterpath) Then
							Try
								util_ImageLoad(pbx_HmPosterSht, WorkingHomeMovie.fileinfo.posterpath, Utilities.DefaultFanartPath)
								util_ImageLoad(pbx_HmPoster, WorkingHomeMovie.fileinfo.posterpath, Utilities.DefaultFanartPath)
							Catch
							End Try
						End If
					Else
						MsgBox("Failed to get ScreenShot")
					End If
				End If
				If Not IsNothing(messbox) Then messbox.Close()
			Else
				MsgBox("Please enter a numerical value into the textbox")
				tb_HmPosterTime.Focus()
				Exit Sub
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		Finally
			If Not IsNothing(messbox) Then messbox.Close()
		End Try

	End Sub

#End Region

#Region "Home folders"

	Private Sub tp_HmFolders_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tp_HmFolders.Leave
		Try
			If hmfolderschanged Then
				Dim save = MsgBox("You have made changes to some folders" & vbCrLf & "    Do you wish to save these changes?", MsgBoxStyle.YesNo)
				If save = DialogResult.Yes Then
					Call HomeMovieFoldersRefresh()
				End If
				hmfolderschanged = False
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub btn_HmFolderSaveRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_HmFolderSaveRefresh.Click
		Call HomeMovieFoldersRefresh()
		hmfolderschanged = False
	End Sub

	Private Sub btnHomeFolderAdd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeFolderAdd.Click
		Try
			Dim allok As Boolean = True
			Dim thefoldernames As String
			fb.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
			fb.ShowNewFolderButton = True
			fb.RootFolder = System.Environment.SpecialFolder.Desktop
			fb.SelectedPath = Pref.lastpath
			Tmr.Start()
			If fb.ShowDialog = Windows.Forms.DialogResult.OK Then
				thefoldernames = (fb.SelectedPath)
				Pref.lastpath = thefoldernames
				For Each item As Object In clbx_HMMovieFolders.Items
					If thefoldernames.ToString = item.ToString Then allok = False
				Next

				If allok = True Then
					AuthorizeCheck = True
					clbx_HMMovieFolders.Items.Add(thefoldernames, True)
					clbx_HMMovieFolders.Refresh()
					AuthorizeCheck = False
				Else
					MsgBox("        Folder Already Exists")
				End If
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub btnHomeFoldersRemove_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHomeFoldersRemove.Click
		Try
			While clbx_HMMovieFolders.SelectedItems.Count > 0
				clbx_HMMovieFolders.Items.Remove(clbx_HMMovieFolders.SelectedItems(0))
			End While
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub btnHomeManualPathAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnHomeManualPathAdd.Click
		Try
			If tbHomeManualPath.Text = Nothing Then
				Exit Sub
			End If
			If tbHomeManualPath.Text = "" Then
				Exit Sub
			End If
			Dim tempstring As String = tbHomeManualPath.Text
			Do While tempstring.LastIndexOf("\") = tempstring.Length - 1
				tempstring = tempstring.Substring(0, tempstring.Length - 1)
			Loop
			Do While tempstring.LastIndexOf("/") = tempstring.Length - 1
				tempstring = tempstring.Substring(0, tempstring.Length - 1)
			Loop
			Dim exists As Boolean = False
			For Each item In clbx_HMMovieFolders.Items
                If (item.ToString.Equals(tempstring,  StringComparison.InvariantCultureIgnoreCase)) Then
					exists = True
					Exit For
				End If
			Next
			If exists = True Then
				MsgBox("        Folder Already Exists")
			Else
				Dim f As New DirectoryInfo(tempstring)
				If f.Exists Then
					AuthorizeCheck = True
					clbx_HMMovieFolders.Items.Add(tempstring, True)
					clbx_HMMovieFolders.Refresh()
					AuthorizeCheck = False
					tbHomeManualPath.Text = ""
				Else
					Dim tempint As Integer = MessageBox.Show("This folder does not appear to exist" & vbCrLf & "Are you sure you wish to add it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					If tempint = DialogResult.Yes Then
						AuthorizeCheck = True
						clbx_HMMovieFolders.Items.Add(tempstring, True)
						clbx_HMMovieFolders.Refresh()
						AuthorizeCheck = False
						tbHomeManualPath.Text = ""
					End If
				End If
			End If
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub clbx_HMMovieFolders_DragDrop(sender As Object, e As DragEventArgs) Handles clbx_HMMovieFolders.DragDrop
		Dim folders() As String
		droppedItems.Clear()
		folders = e.Data.GetData(DataFormats.FileDrop)
		For f = 0 To UBound(folders)
			Dim exists As Boolean = False
			For Each rtpath In Pref.homemoviefolders
				If rtpath.rpath = folders(f) Then
					exists = True
					Exit For
				End If
			Next
			If exists OrElse clbx_HMMovieFolders.Items.Contains(folders(f)) Then Continue For
			Dim skip As Boolean = False
			For Each item In droppedItems
				If item = folders(f) Then
					skip = True
					Exit For
				End If
			Next
			If Not skip Then droppedItems.Add(folders(f))
		Next
		If droppedItems.Count < 1 Then Exit Sub
		AuthorizeCheck = True
		For Each item In droppedItems
			clbx_HMMovieFolders.Items.Add(item, True)
			hmfolderschanged = True
		Next
		AuthorizeCheck = False
		clbx_HMMovieFolders.Refresh()
	End Sub

	Private Sub clbx_HMMovieFolders_DragEnter(sender As Object, e As DragEventArgs) Handles clbx_HMMovieFolders.DragEnter
		Try
			e.Effect = DragDropEffects.Copy
		Catch ex As Exception
			ExceptionHandler.LogError(ex)
		End Try
	End Sub

	Private Sub clbx_HMMovieFolders_KeyPress(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles clbx_HMMovieFolders.KeyDown
		If e.KeyCode = Keys.Delete AndAlso clbx_MovieRoots.SelectedItem <> Nothing Then
			Call btnHomeFoldersRemove.PerformClick()
		ElseIf e.KeyCode = Keys.Space Then
			AuthorizeCheck = True
			Call clbx_hmmoviefolderstoggle()
			AuthorizeCheck = False
		End If
	End Sub

	Private Sub clbx_HMMovieFolders_MouseDown(sender As Object, e As MouseEventArgs) Handles clbx_HMMovieFolders.MouseDown
		Dim loc As Point = Me.clbx_HMMovieFolders.PointToClient(Cursor.Position)
		For i As Integer = 0 To Me.clbx_HMMovieFolders.Items.Count - 1
			Dim rec As Rectangle = Me.clbx_HMMovieFolders.GetItemRectangle(i)
			rec.Width = 16
			'checkbox itself has a default width of about 16 pixels
			If rec.Contains(loc) Then
				AuthorizeCheck = True
				Dim newValue As Boolean = Not Me.clbx_HMMovieFolders.GetItemChecked(i)
				Me.clbx_HMMovieFolders.SetItemChecked(i, newValue)
				AuthorizeCheck = False
				Return
			End If
		Next
	End Sub

	Private Sub clbx_HMMovieFolders_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbx_HMMovieFolders.ItemCheck
		If Not AuthorizeCheck Then
			e.NewValue = e.CurrentValue
			Exit Sub
		End If
		hmfolderschanged = True
	End Sub

	Private Sub clbx_hmmoviefolderstoggle()
		Dim i = clbx_HMMovieFolders.SelectedIndex
		clbx_HMMovieFolders.SetItemCheckState(i, If(clbx_HMMovieFolders.GetItemCheckState(i) = CheckState.Checked, CheckState.Unchecked, CheckState.Checked))
	End Sub

	Private Sub HomeFoldersUpdate()
		AuthorizeCheck = True
		clbx_HMMovieFolders.Items.Clear()
		For Each item In Pref.homemoviefolders
			clbx_HMMovieFolders.Items.Add(item.rpath, item.selected)
		Next
		AuthorizeCheck = False
		hmfolderschanged = False
	End Sub
#End Region
    
	
	Private Sub rebuildHomeMovies()
		homemovielist.Clear()
		lb_HomeMovies.Items.Clear()
		Dim newhomemoviefolders As New List(Of String)
		Dim progress As Integer = 0
		progress = 0
		scraperLog = ""
		Dim dirpath As String = String.Empty
		Dim newHomeMovieList As New List(Of str_BasicHomeMovie)
		Dim totalfolders As New List(Of String)
		totalfolders.Clear()
		For Each moviefolder In homemoviefolders
			If Not moviefolder.selected Then Continue For
			Dim hg As New DirectoryInfo(moviefolder.rpath)
			If hg.Exists Then
				scraperLog &= "Searching Movie Folder: " & hg.FullName.ToString & vbCrLf
				totalfolders.Add(moviefolder.rpath)
				Dim newlist As List(Of String)
				Try
					newlist = Utilities.EnumerateFolders(moviefolder.rpath)       'Max levels restriction of 6 deep removed
					For Each subfolder In newlist
						scraperLog = scraperLog & "Subfolder added :- " & subfolder.ToString & vbCrLf
						totalfolders.Add(subfolder)
					Next
				Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
				End Try
			End If
		Next
		For Each homemoviefolder In totalfolders
			Dim returnedhomemovielist As New List(Of str_BasicHomeMovie)
			dirpath = homemoviefolder
			Dim dir_info As New DirectoryInfo(dirpath)
			returnedhomemovielist = HomeMovies.listHomeMovieFiles(dir_info, "*.nfo", scraperLog)         'titlename is logged in here
			If returnedhomemovielist.Count > 0 Then
				For Each newhomemovie In returnedhomemovielist
					Dim existsincache As Boolean = False
					Dim pathOnly As String = Path.GetDirectoryName(newhomemovie.FullPathAndFilename) & "\"
					Dim nfopath As String = pathOnly & Path.GetFileNameWithoutExtension(newhomemovie.FullPathAndFilename) & ".nfo"
					If File.Exists(nfopath) Then
						Try
							Dim newexistingmovie As New HomeMovieDetails
							newexistingmovie = nfoFunction.nfoLoadHomeMovie(nfopath)
							Dim newexistingbasichomemovie As New str_BasicHomeMovie
							newexistingbasichomemovie.FullPathAndFilename = newexistingmovie.fileinfo.fullpathandfilename
							newexistingbasichomemovie.Title = newexistingmovie.fullmoviebody.title

							homemovielist.Add(newexistingbasichomemovie)
							lb_HomeMovies.Items.Add(New ValueDescriptionPair(newexistingbasichomemovie.FullPathAndFilename, newexistingbasichomemovie.Title))
						Catch ex As Exception
						End Try
					Else
						newHomeMovieList.Add(newhomemovie)
					End If
				Next
			End If
		Next
		Call HomeMovieCacheSave()
	End Sub

    Public Shared Sub HomeMovieAdd(ByVal newHomeMovie As str_BasicHomeMovie)
        homemovielist.Add(newHomeMovie)
    End Sub

	Private Sub HomeMovieCacheSave()
		Dim fullpath As String = workingProfile.HomeMovieCache
		If homemovielist.Count > 0 And homemoviefolders.Count > 0 Then
			If File.Exists(fullpath) Then
				Dim don As Boolean = False
				Dim count As Integer = 0
				Do
					Try
						If File.Exists(fullpath) Then
							File.Delete(fullpath)
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
			frmSplash2.Label1.Text = "Creating Home Movie Cache xml....."
			frmSplash2.Label2.Visible = False
			frmSplash2.ProgressBar1.Visible = False
			Dim doc As New XmlDocument
			Dim thispref As XmlNode = Nothing
			Dim xmlproc As XmlDeclaration
			xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
			doc.AppendChild(xmlproc)
			Dim root As XmlElement
			Dim child As XmlElement
			root = doc.CreateElement("homemovie_cache")
			Dim count2 As Integer = 0
			frmSplash2.Label2.Text = "Creating cache xml...."
			For Each movie In homemovielist
				child = doc.CreateElement("movie")
                child.AppendChild(doc, "fullpathandfilename", movie.FullPathAndFilename)
                child.AppendChild(doc, "title"              , movie.Title)
				root.AppendChild(child)
			Next
			doc.AppendChild(root)

			frmSplash2.Label2.Text = "Saving cache xml...."
            WorkingWithNfoFiles.SaveXMLDoc(doc, fullpath)
		Else
			Try
				If File.Exists(fullpath) Then
					File.Delete(fullpath)
				End If
			Catch
			End Try
		End If
	End Sub

	Private Sub homemovieCacheLoad()
		homemovielist.Clear()

		Dim movielist As New XmlDocument
		Dim objReader As New IO.StreamReader(workingProfile.HomeMovieCache)
		Dim tempstring As String = objReader.ReadToEnd
		objReader.Close()
		objReader = Nothing

		movielist.LoadXml(tempstring)
		Dim thisresult As XmlNode = Nothing
		For Each thisresult In movielist("homemovie_cache")
			Select Case thisresult.Name
				Case "movie"
					Dim newmovie As New str_BasicHomeMovie(SetDefaults)
					Dim detail As XmlNode = Nothing
					For Each detail In thisresult.ChildNodes
						Select Case detail.Name
							Case "fullpathandfilename"
								newmovie.FullPathAndFilename = detail.InnerText
							Case "title"
								newmovie.Title = detail.InnerText
						End Select
					Next
					homemovielist.Add(newmovie)
			End Select
		Next
		Call loadhomemovielist()
		Try
			lb_HomeMovies.SelectedIndex = 0
		Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
		End Try
	End Sub

	Private Sub loadhomemovielist()
		lb_HomeMovies.Items.Clear()
		For Each item In homemovielist
			lb_HomeMovies.Items.Add(New ValueDescriptionPair(item.FullPathAndFilename, item.Title))
		Next
	End Sub

	Private Sub loadhomemoviedetails()
		HmMovTitle.Text = ""
		HmMovSort   .Text = ""
		HmMovYear   .Text = ""
		HmMovPlot   .Text = ""
		HmMovStars  .Text = ""
        HmMovGenre  .Text = ""
        HmMovPath   .Text = ""
		pbx_HmFanart.Image = Nothing
		WorkingHomeMovie = nfoFunction.nfoLoadHomeMovie(WorkingHomeMovie.fileinfo.fullpathandfilename)
		HmMovTitle  .Text = WorkingHomeMovie.fullmoviebody.title
		HmMovSort   .Text = WorkingHomeMovie.fullmoviebody.sortorder
		HmMovPlot   .Text = WorkingHomeMovie.fullmoviebody.plot
		HmMovStars  .Text = WorkingHomeMovie.fullmoviebody.stars
		HmMovYear   .Text = WorkingHomeMovie.fullmoviebody.year
        HmMovGenre  .Text = WorkingHomeMovie.fullmoviebody.genre
        HmMovPath   .Text = WorkingHomeMovie.fileinfo.fullpathandfilename
		PlaceHolderforHomeMovieTitleToolStripMenuItem.Text = WorkingHomeMovie.fullmoviebody.title
		PlaceHolderforHomeMovieTitleToolStripMenuItem.BackColor = Color.Honeydew
		PlaceHolderforHomeMovieTitleToolStripMenuItem.Font = New Font("Arial", 10, FontStyle.Bold)
		If File.Exists(WorkingHomeMovie.fileinfo.fanartpath) Then
			util_ImageLoad(pbx_HmFanart, WorkingHomeMovie.fileinfo.fanartpath, Utilities.DefaultFanartPath)
			Dim video_flags = VidMediaFlags(WorkingHomeMovie.filedetails)
			movieGraphicInfo.OverlayInfo(pbx_HmFanart, "", video_flags)
		End If
		util_ImageLoad(pbx_HmPoster, WorkingHomeMovie.fileinfo.posterpath, Utilities.DefaultPosterPath)
	End Sub

	Private Sub HomeMovieFoldersRefresh()
		AuthorizeCheck = True
		Pref.homemoviefolders.Clear()
		For f = 0 To clbx_HMMovieFolders.Items.Count - 1
			Dim t As New str_RootPaths
			t.rpath = clbx_HMMovieFolders.Items(f).ToString
			Dim chkstate As CheckState = clbx_HMMovieFolders.GetItemCheckState(f)
			t.selected = (chkstate = CheckState.Checked)
			Pref.homemoviefolders.Add(t)
		Next
		Call ConfigSave()
		Call rebuildHomeMovies()
		AuthorizeCheck = False
		hmfolderschanged = False
		TabControl1.SelectedIndex = 0
	End Sub
    
End Class