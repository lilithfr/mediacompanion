Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports Media_Companion.WorkingWithNfoFiles
Imports System.Xml
Imports Media_Companion
Imports Media_Companion.Preferences
Imports System.Linq

Partial Public Class Form1
    Dim newEpisodeList As New List(Of TvEpisode)
    Dim languageList As New List(Of Tvdb.Language)
    Dim listOfShows As New List(Of str_PossibleShowList)

    Dim tvdbposterlist As New List(Of TvBanners)
    Dim imdbposterlist As New List(Of TvBanners)
    Dim tvdbmode As Boolean = False
    Dim usedlist As New List(Of TvBanners)

    Dim tvobjects As New List(Of String)

#Region "Tv Treeview Routines"
    Public Sub tv_ViewReset()
        Button_Save_TvShow_Episode.Enabled = True
        Tv_TreeViewContext_RefreshShow.Enabled = False
        Tv_TreeViewContext_RefreshShow.Visible = False
        Tv_TreeViewContext_ShowMissEps.Enabled = False
        Tv_TreeViewContext_ShowMissEps.Visible = False
        Tv_TreeViewContext_DispByAiredDate.Enabled = False
        Tv_TreeViewContext_DispByAiredDate.Visible = False
        Tv_TreeViewContext_SearchNewEp.Enabled = False
        Tv_TreeViewContext_SearchNewEp.Visible = False
        Tv_TreeViewContext_FindMissArt.Enabled = False
        Tv_TreeViewContext_FindMissArt.Visible = False

        Tv_TreeViewContext_ViewNfo.Enabled = False
        ExpandSelectedShowToolStripMenuItem.Enabled = False
        CollapseSelectedShowToolStripMenuItem.Enabled = False
        ExpandAllToolStripMenuItem.Enabled = False
        CollapseAllToolStripMenuItem.Enabled = False
        Tv_TreeViewContext_ReloadFromCache.Enabled = False
        Tv_TreeViewContext_OpenFolder.Enabled = False

        tb_ShPremiered.Text = ""
        tb_ShGenre.Text = ""
        tb_ShTvdbId.Text = ""
        tb_ShImdbId.Text = ""
        tb_ShRating.Text = ""
        tb_ShCert.Text = ""
        tb_ShRunTime.Text = ""
        tb_ShStudio.Text = ""
        cbTvActorRole.Items.Clear()
        cbTvActorRole.Text = ""

        cbTvActor.Items.Clear()
        cbTvActor.Text = ""
        PictureBox6.Image = Nothing

        tvdbposterlist.Clear()
        PictureBox6.Image = Nothing
        tv_PictureBoxLeft.Image = Nothing
        tv_PictureBoxRight.Image = Nothing
        tv_PictureBoxBottom.Image = Nothing
        tb_ShPremiered.Text = ""
        tb_ShGenre.Text = ""
        tb_ShTvdbId.Text = ""
        tb_ShImdbId.Text = ""
        tb_ShRating.Text = ""
        tb_ShCert.Text = ""
        tb_ShRunTime.Text = ""
        tb_ShStudio.Text = ""
        cbTvActorRole.Items.Clear()
        cbTvActorRole.Text = ""
        tb_ShPlot.Text = ""
        cbTvActor.Items.Clear()
        cbTvActor.Text = ""

        tb_Sh_Ep_Title.Text = ""
        tb_ShPremiered.Text = ""
        tb_ShGenre.Text = ""
        tb_ShTvdbId.Text = ""
        tb_ShImdbId.Text = ""
        tb_ShRating.Text = ""
        tb_ShCert.Text = ""
        tb_ShRunTime.Text = ""
        tb_ShStudio.Text = ""
        cbTvActorRole.Items.Clear()
        cbTvActorRole.Text = ""
        tb_ShPlot.Text = ""
        cbTvActor.Items.Clear()
        cbTvActor.Text = ""

        cbTvActor.Items.Clear()
        cbTvActor.Text = ""
        For i = Panel13.Controls.Count - 1 To 0 Step -1
            Panel13.Controls.RemoveAt(i)
        Next
    End Sub

    Private Sub TvTreeview_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvTreeview.AfterSelect
        TvTreeview_AfterSelect_Do() 'moved this to seperate sub so we can also call this from other locations.
    End Sub

    Private Sub TvTreeview_AfterSelect_Do()
        Try
            'chooses which sub is run to load the relavent tv data to the screen
            'note: context menu items are set during TvTreeView_MouseUp event because we only need to update if right click is done which we check in the mouseup sub
            'mouseup sub selects the node underneath the mouse & then this runs since its an event 'AfterSelect'

            If TvTreeview.SelectedNode Is Nothing Then Exit Sub
            If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
                tv_ShowLoad(TvTreeview.SelectedNode.Tag)
            ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
                tv_SeasonSelected(TvTreeview.SelectedNode.Tag)
            ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
                tv_EpisodeSelected(TvTreeview.SelectedNode.Tag)
            Else
                MsgBox("None")
            End If

            'tv_SplitContainerAutoPosition() 'auto set container splits....after we have loaded data & pictures....
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TvTreeview_DragDrop(sender As Object, e As DragEventArgs) Handles TvTreeview.DragDrop
        Dim files() As String
        files = e.Data.GetData(DataFormats.FileDrop)
        For f = 0 To UBound(files)
            If IO.Directory.Exists(files(f)) Then
                If files(f).ToLower.Contains(".actors") Or files(f).ToLower.Contains("season") Then Continue For
                For each fol In Preferences.tvRootFolders
                    If fol.rpath = files(f) Then Continue For
                    If files(f).Contains(fol.rpath) AndAlso Not fol.selected Then
                        Dim msg As String = "The series dropped is in a root folder that has been unselected"
                        msg &= "To avoid catastrophic failure, please re-select"
                        msg &= "root folder: " & fol.rpath 
                        msg &= "and attempt again"
                        MsgBox (msg)
                        Continue For
                    End If
                Next
                'If Preferences.tvRootFolders.Contains(files(f)) Then Continue For
                Dim di As New IO.DirectoryInfo(files(f))
                If Preferences.tvFolders.Contains(files(f)) Then Continue For
                Dim skip As Boolean = False
                For Each item In droppedItems
                    If item = files(f) Then
                        skip = True
                        Exit For
                    End If
                Next
                If Not skip Then droppedItems.Add(files(f))
            End If
        Next
        If droppedItems.Count < 1 Then Exit Sub
        Dim droppedmsg As String = "You have dropped the following folders onto TV," &vbCrLf & "Are you sure you wish to add these as TvShows?" & vbCrLf
        For Each item In droppedItems
            droppedmsg &= item & vbcrlf
        Next
        Dim x = MsgBox(droppedmsg, MsgBoxStyle.YesNo)
        If x = MsgBoxResult.No Then 
            droppedItems.Clear()
            Exit Sub
        End If
        For Each item In droppedItems
            Preferences.tvFolders.add(item)
            newTvFolders.Add(item)
        Next
        droppedItems.Clear()
        Preferences.ConfigSave()
        tv_ShowScrape()
    End Sub

    Private Sub TvTreeview_DragEnter(sender As Object, e As DragEventArgs) Handles TvTreeview.DragEnter
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TvTreeviewRebuild()
        TvTreeview.Nodes.Clear()              'clear the treeview of old data
        ''Dirty work around until TvShows is repalced with TvCache.Shows universally
        For Each TvShow As Media_Companion.TvShow In Cache.TvCache.Shows
            'TvShow.UpdateTreenode()
            TvTreeview.Nodes.Add(TvShow.ShowNode)
            TvShow.UpdateTreenode()
        Next
        TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
        TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
        TvTreeview.Sort()
    End Sub

    Private Sub Tv_TreeViewContextMenuItemsEnable()        'enable/disable right click context menu items depending on if its show/season/episode
        '                                                  'called from tv_treeview mouseup event where we check for a right click
        If TvTreeview.SelectedNode Is Nothing Then Return
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()  'set WORKINGTVSHOW to show obj irrelavent if we have selected show/season/episode
        Dim showtitle As String = WorkingTvShow.Title.Value       'set our show title



        'now we set the items that have variable text in the context menu using the 'show' text set above
        Tv_TreeViewContext_ShowTitle.BackColor = Color.Honeydew                'SK - same color as the refresh tv show splash - comments required to see if it works or not....
        
        If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
            Tv_TreeViewContext_ShowTitle.Text = "'" & showtitle & "'"
            Tv_TreeViewContext_ShowTitle.Font = New Font("Arial", 10, FontStyle.Bold)
            Tv_TreeViewContext_Play_Episode.Enabled = False
            Tv_TreeViewContext_ViewNfo.Text = "View TVShow .nfo"
            Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape TVShow"
            Tv_TreeViewContext_WatchedShowOrEpisode.Text = "Mark This Show as Watched"
            Tv_TreeViewContext_UnWatchedShowOrEpisode.Text = "Mark This Show as UnWatched"

            Tv_TreeViewContext_OpenFolder.Enabled = True
            Tv_TreeViewContext_ViewNfo.Enabled = True
            Tv_TreeViewContext_RescrapeShowOrEpisode.Enabled = True
            Tv_TreeViewContext_WatchedShowOrEpisode.Enabled = True
            Tv_TreeViewContext_UnWatchedShowOrEpisode.Enabled = True
            Tv_TreeViewContext_RescrapeWizard.Enabled = True
            Tv_TreeViewContext_FindMissArt.Enabled = True
            Tv_TreeViewContext_RefreshShow.Enabled = True
            Tv_TreeViewContext_RefreshShow.Visible = True
            Tv_TreeViewContext_MissingEpThumbs.Enabled = True
            Tv_TreeViewContext_MissingEpThumbs.Visible = True
            Tv_TreeViewContext_ReloadFromCache.Enabled = True
            Tv_TreeViewContext_RenameEp.Enabled = rbTvListAll.Checked     'Only show if Treeview set to 'List All'
            Tv_TreeViewContext_ShowMissEps.Enabled = True
            Tv_TreeViewContext_DispByAiredDate.Enabled = True
            tsmiTvDelShowNfoArt.Enabled = True
            tsmiTvDelShowEpNfoArt.Enabled = True

        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
            Tv_TreeViewContext_ShowTitle.Text = "'" & showtitle & "' - " & tv_SeasonSelectedCurrently.SeasonLabel
            Tv_TreeViewContext_ShowTitle.Font = New Font("Arial", 10, FontStyle.Bold)
            Tv_TreeViewContext_Play_Episode.Enabled = False
            Tv_TreeViewContext_ViewNfo.Text = "View Season .nfo"
            Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape Season"
            Tv_TreeViewContext_WatchedShowOrEpisode.Text = "Mark This Season as Watched"
            Tv_TreeViewContext_UnWatchedShowOrEpisode.Text = "Mark This Season as UnWatched"

            Tv_TreeViewContext_OpenFolder.Enabled = True
            Tv_TreeViewContext_ViewNfo.Enabled = False
            Tv_TreeViewContext_RescrapeShowOrEpisode.Enabled = False
            Tv_TreeViewContext_WatchedShowOrEpisode.Enabled = True
            Tv_TreeViewContext_UnWatchedShowOrEpisode.Enabled = True
            Tv_TreeViewContext_RescrapeWizard.Enabled = False
            Tv_TreeViewContext_FindMissArt.Enabled = False
            Tv_TreeViewContext_RefreshShow.Enabled = False
            Tv_TreeViewContext_RefreshShow.Visible = False
            Tv_TreeViewContext_MissingEpThumbs.Enabled = True
            Tv_TreeViewContext_MissingEpThumbs.Visible = True
            Tv_TreeViewContext_ReloadFromCache.Enabled = False
            Tv_TreeViewContext_RenameEp.Enabled = rbTvListAll.Checked      'Only show if Treeview set to 'List All'
            Tv_TreeViewContext_ShowMissEps.Enabled = True
            Tv_TreeViewContext_DispByAiredDate.Enabled = True
            tsmiTvDelShowNfoArt.Enabled = False
            tsmiTvDelShowEpNfoArt.Enabled = False

        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
            Tv_TreeViewContext_ShowTitle.Text = "'" & showtitle & "' - S" & Utilities.PadNumber(ep_SelectedCurrently.Season.Value, 2) & "E" & Utilities.PadNumber(ep_SelectedCurrently.Episode.Value, 2) & " '" & ep_SelectedCurrently.Title.Value & "'"
            Tv_TreeViewContext_ShowTitle.Font = New Font("Arial", 10, FontStyle.Bold)
            Tv_TreeViewContext_Play_Episode.Enabled = Not DirectCast(TvTreeview.SelectedNode.Tag, Media_Companion.TvEpisode).Ismissing
            Tv_TreeViewContext_ViewNfo.Text = "View Episode .nfo"
            Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape Episode"
            Tv_TreeViewContext_WatchedShowOrEpisode.Text = "Mark Episode as Watched"
            Tv_TreeViewContext_UnWatchedShowOrEpisode.Text = "Mark Episode as UnWatched"

            Tv_TreeViewContext_OpenFolder.Enabled = True
            Tv_TreeViewContext_ViewNfo.Enabled = True
            Tv_TreeViewContext_RescrapeShowOrEpisode.Enabled = Not DirectCast(TvTreeview.SelectedNode.Tag, Media_Companion.TvEpisode).Ismissing
            Tv_TreeViewContext_WatchedShowOrEpisode.Enabled = True
            Tv_TreeViewContext_UnWatchedShowOrEpisode.Enabled = True
            Tv_TreeViewContext_RescrapeWizard.Enabled = False
            Tv_TreeViewContext_FindMissArt.Enabled = False
            Tv_TreeViewContext_RefreshShow.Enabled = False
            Tv_TreeViewContext_RefreshShow.Visible = False
            Tv_TreeViewContext_MissingEpThumbs.Enabled = False
            Tv_TreeViewContext_MissingEpThumbs.Visible = False
            Tv_TreeViewContext_ReloadFromCache.Enabled = False
            Tv_TreeViewContext_RenameEp.Enabled = Not DirectCast(TvTreeview.SelectedNode.Tag, Media_Companion.TvEpisode).Ismissing
            Tv_TreeViewContext_ShowMissEps.Enabled = True
            Tv_TreeViewContext_DispByAiredDate.Enabled = True
            tsmiTvDelShowNfoArt.Enabled = False
            tsmiTvDelShowEpNfoArt.Enabled = False

        Else
            MsgBox("None")
        End If

        'these are the four items at the bottom of the menu to control Expand/Colapse the tv_treeview (always shown)
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
    End Sub

#End Region

    Sub tv_Rescrape_Show(ByVal WorkingTvShow)
        Dim tempint As Integer = 0
        Dim tempstring As String = ""
        tempint = MessageBox.Show("Rescraping the TV Show will Overwrite all the current details" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If tempint = DialogResult.No Then
            Exit Sub
        End If
        Dim messbox As frmMessageBox = New frmMessageBox("The Selected TV Show is being Rescraped", "", "Please Wait")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        messbox.Show()
        messbox.Refresh()
        Application.DoEvents()
        Dim selectedLang As String = WorkingTvShow.Language.Value
        If selectedLang = "" Then selectedLang = "en"

        If Preferences.tvshow_useXBMC_Scraper = True Then
            Dim TVShowNFOContent As String = XBMCScrape_TVShow_General_Info("metadata.tvdb.com", WorkingTvShow.TvdbId.Value, selectedLang, WorkingTvShow.NfoFilePath)
            If TVShowNFOContent <> "error" Then CreateMovieNfo(WorkingTvShow.NfoFilePath, TVShowNFOContent)
            Call tv_ShowLoad(WorkingTvShow)
        Else
            For Each episode In WorkingTvShow.Episodes
                If Preferences.displayMissingEpisodes AndAlso episode.IsMissing = True Then
                    Cache.TvCache.Remove(episode)
                Else
                    Cache.TvCache.Remove(episode)
                End If
            Next
            Cache.TvCache.Remove(WorkingTvShow)
            newTvFolders.Add(WorkingTvShow.FolderPath.Substring(0, WorkingTvShow.FolderPath.LastIndexOf("\")))
            Dim args As TvdbArgs = New TvdbArgs(WorkingTvShow.TvdbId.Value, selectedLang)
            bckgrnd_tvshowscraper.RunWorkerAsync(args)
            While bckgrnd_tvshowscraper.IsBusy
                Application.DoEvents()
            End While
        End If
        messbox.Close()
        TabControl3.SelectedIndex = 0
    End Sub


    Private Sub tv_ShowLoad(ByVal Show As Media_Companion.TvShow)
        Show.ListActors.Clear()
        Show.Load()
        
        'Fix episodeguide tag
        Dim lang As String = Show.EpisodeGuideUrl.Value
        If String.IsNullOrEmpty(lang) Then 
            lang = "en"
        Else
            lang = lang.Substring((lang.LastIndexOf("/")+1)).Replace(".zip","")
        End If
        
        If Not Show.TvdbId.Value = "" Then
        Show.EpisodeGuideUrl.Value = ""
            Show.Url.Value = URLs.EpisodeGuide(Show.TvdbId.Value, lang) 'Show.Language.Value)
            Show.Url.Node.SetAttributeValue("cache", Show.TvdbId.Value)
        End If
        'end fix

        Dim hg As New IO.DirectoryInfo(Show.FolderPath)
        If Not hg.Exists Then
            tb_ShPlot.Text = "Unable to find folder: " & Show.FolderPath
            tb_Sh_Ep_Title.Text = "Unable to find folder: " & Show.FolderPath
        Else
            If TabControl3.TabPages(1).Text = "Screenshot" Then
                TabControl3.TabPages.RemoveAt(1)
            End If

            'load tvshow.nfo
            ListBox3.Items.Clear()
            TextBox26.Text = ""
            Dim todo As Boolean = False

            If Show.State = Media_Companion.ShowState.Locked Then
                Button_TV_State.Text = "Locked"
                Button_TV_State.BackColor = Color.Red
            ElseIf Show.State = Media_Companion.ShowState.Open Then
                Button_TV_State.Text = "Open"
                Button_TV_State.BackColor = Color.LawnGreen
            ElseIf Show.State = Media_Companion.ShowState.Unverified Then
                Button_TV_State.Text = "Un-Verified"
                Button_TV_State.BackColor = Color.Yellow
            Else
                Button_TV_State.Text = "Error"
                Button_TV_State.BackColor = Color.Gray
            End If
            Button_TV_State.Tag = Show

            If Show.Status.Value = "Ended" Then
                bnt_TvSeriesStatus.Text = "Ended"
                bnt_TvSeriesStatus.BackColor = Color.LightPink
            ElseIf Show.Status.Value = "Continuing" Then
                bnt_TvSeriesStatus.Text = "Continuing"
                bnt_TvSeriesStatus.BackColor = Color.LightGreen
            Else
                bnt_TvSeriesStatus.Text = "Unknown"
                bnt_TvSeriesStatus.BackColor = Color.LightYellow
            End If
            Dim tvpbright As String = Utilities.DefaultTvPosterPath 
            Dim tvpbbottom As String = Utilities.DefaultTvBannerPath

            If Preferences.EdenEnabled AndAlso Not Preferences.FrodoEnabled Then
                If Preferences.postertype = "banner" Then
                    If Utilities.IsBanner(Show.FolderPath & "folder.jpg") Then
                        tvpbbottom = Show.FolderPath & "folder.jpg"
                    Else
                        tvpbright = Show.ImagePoster.path
                    End If
                Else
                    tvpbright = Show.ImagePoster.path
                End If
            End If
            If Preferences.FrodoEnabled Then
                Show.ImagePoster.FileName = "poster.jpg"
                Show.ImageBanner.FileName = "banner.jpg"
                tvpbbottom = Show.ImageBanner.Path
                tvpbright = Show.ImagePoster.path
            End If
            util_ImageLoad(tv_PictureBoxRight, tvpbright, Utilities.DefaultTvPosterPath)
            util_ImageLoad(tv_PictureBoxBottom, tvpbbottom, Utilities.DefaultTvBannerPath)
            util_ImageLoad(tv_PictureBoxLeft, Show.ImageFanart.Path, Utilities.DefaultTvFanartPath)

            Panel9.Visible = False
            Panel8.Visible = False
            lbl_sorttitle.Visible = True
            TextBox_Sorttitle.Visible = True

            tb_Sh_Ep_Title.BackColor = Color.White
            If Show.Title.Value <> Nothing Then
                tb_Sh_Ep_Title.Text = Show.Title.Value

            End If

            ' changed indication of an issue, setting the title means that the title is saved to the nfo if the user exits. Yellow is the same colour as the unverified Button
            If Show.State = ShowState.Unverified Then tb_Sh_Ep_Title.BackColor = Color.Yellow
            If Show.State = ShowState.Error Then tb_Sh_Ep_Title.BackColor = Color.Red

            tb_ShPremiered.Text = Utilities.ReplaceNothing(Show.Premiered.Value)
            tb_ShGenre.Text = Utilities.ReplaceNothing(Show.Genre.Value)
            tb_ShTvdbId.Text = Utilities.ReplaceNothing(Show.TvdbId.Value)
            tb_ShImdbId.Text = Utilities.ReplaceNothing(Show.ImdbId.Value)
            tb_ShRating.Text = Utilities.ReplaceNothing(Show.Rating.Value)
            tb_ShCert.Text = Utilities.ReplaceNothing(Show.Mpaa.Value)
            tb_ShRunTime.Text = Utilities.ReplaceNothing(Show.Runtime.Value)
            tb_ShStudio.Text = Utilities.ReplaceNothing(Show.Studio.Value)
            tb_ShPlot.Text = Utilities.ReplaceNothing(Show.Plot.Value)
            TextBox_Sorttitle.Text = Utilities.ReplaceNothing(If(String.IsNullOrEmpty(Show.SortTitle.Value ), Show.Title.Value, Show.SortTitle.Value))

            If String.IsNullOrEmpty(Show.SortOrder.Value) Then Show.SortOrder.Value = Preferences.sortorder
            If Show.SortOrder.Value = "dvd" Then
                Button47.Text = "DVD"
            ElseIf Show.SortOrder.Value = "default" Then
                Button47.Text = "Default"
            End If
            '0	-	all from tvdb
            '1	-	all from imdb
            '2	-	tv imdb, eps tvdb
            '3	-	tv TVDB, eps IMDB

            If String.IsNullOrEmpty(Show.EpisodeActorSource.Value) Then
                If Preferences.TvdbActorScrape = "0" Or Preferences.TvdbActorScrape = "2" Then
                    Show.EpisodeActorSource.Value = "tvdb"
                Else
                    Show.EpisodeActorSource.Value = "imdb"
                End If
            End If

            Button46.Text = Show.EpisodeActorSource.Value.ToUpper
            If String.IsNullOrEmpty(Show.TvShowActorSource.Value) Then
                If Preferences.TvdbActorScrape = "0" Or Preferences.TvdbActorScrape = "3" Then
                    Show.TvShowActorSource.Value = "tvdb"
                Else
                    Show.TvShowActorSource.Value = "imdb"
                End If
            End If

            TvPanel7Update(Show.FolderPath)
            Button45.Text = Show.TvShowActorSource.Value.ToUpper
            Call tv_ActorsLoad(Show.ListActors)
            Show.UpdateTreenode()
        End If
        Panel9.Visible = False
        Panel8.Visible = False
    End Sub

    Private Sub tb_ShGenre_MouseDown(sender As Object, e As MouseEventArgs) Handles tb_ShGenre.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Try
                Dim thisshow As TvShow = tv_ShowSelectedCurrently 
                Dim item() As String = thisshow.Genre.Value.Split("/")
                Dim genre As String = ""
                Dim listof As New List(Of String)
                listof.Clear()
                For Each i In item
                    listof.Add(i.Trim)
                Next
                Dim frm As New frmGenreSelect 
                frm.SelectedGenres = listof
                frm.Init()
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    listof.Clear()
                    listof.AddRange(frm.SelectedGenres)
                    For each g In listof
                        If genre = "" Then
                            genre = g
                        Else
                            genre += " / " & g
                        End If
                    Next
                    thisshow.Genre.Value = genre
                    tb_ShGenre.Text = genre
                    thisshow.Save()
                End If
            Catch
            End Try
        End If
    End Sub

    Public Sub tv_ActorsLoad(ByVal listActors As Media_Companion.ActorList)
        cbTvActor.Items.Clear()
        cbTvActorRole.Items.Clear()
        For Each actor In ListActors
            If actor.actorname <> Nothing AndAlso Not cbTvActor.Items.Contains(actor.actorname) Then
                cbTvActor.Items.Add(actor.actorname)
                Dim role As String = actor.actorrole
                If String.IsNullOrEmpty(role) Or role = Nothing Then role = actor.actorname
                cbTvActorRole.Items.Add(role)
            End If
        Next

        If cbTvActor.Items.Count = 0 Then
            actorflag = true
            cbTvActorRole.Items.Add("")
            cbTvActorRole.SelectedIndex = 0
            Call tv_ActorDisplay(True)
        Else
            cbTvActor.SelectedIndex = 0
            'Call tv_ActorDisplay()
        End If
    End Sub

    Public Sub tv_ActorDisplay(Optional ByVal useDefault As Boolean = False)
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        If WorkingTvShow Is Nothing Then Exit Sub
        Dim imgLocation As String = Utilities.DefaultActorPath
        Dim eden As Boolean = Preferences.EdenEnabled
        Dim frodo As Boolean = Preferences.FrodoEnabled
        PictureBox6.Image = Nothing
        If useDefault Then
            imgLocation = Utilities.DefaultActorPath
        Else
            For Each actor In WorkingTvShow.ListActors
                If actor.actorname = cbTvActor.Text Then
                    Dim temppath As String = Preferences.GetActorPath(WorkingTvShow.NfoFilePath, actor.actorname, actor.ID.Value)
                    If IO.File.Exists(temppath) Then
                        imgLocation = temppath
                    ElseIf (Not Preferences.LocalActorImage) AndAlso actor.actorthumb <> Nothing AndAlso (actor.actorthumb.IndexOf("http") <> -1 OrElse IO.File.Exists(actor.actorthumb)) Then
                        imgLocation = actor.actorthumb
                    End If
                    Exit For
                End If
            Next
        End If
        Try
            util_ImageLoad(PictureBox6, imgLocation, Utilities.DefaultActorPath)
            PictureBox6.SizeMode = PictureBoxSizeMode.Zoom
        Catch
        End Try
    End Sub

    Public Sub tv_ActorRoleDisplay(Optional ByVal useDefault As Boolean = False)
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        If WorkingTvShow Is Nothing Then Exit Sub
        Dim imgLocation As String = Utilities.DefaultActorPath
        Dim eden As Boolean = Preferences.EdenEnabled
        Dim frodo As Boolean = Preferences.FrodoEnabled
        PictureBox6.Image = Nothing
        If useDefault Then
            imgLocation = Utilities.DefaultActorPath
        Else
            For Each actor In WorkingTvShow.ListActors
                If actor.actorrole = cbTvActorRole.Text Then
                    Dim temppath As String = Preferences.GetActorPath(WorkingTvShow.NfoFilePath, actor.actorname, actor.ID.Value)
                    If IO.File.Exists(temppath) Then
                        imgLocation = temppath
                    ElseIf (Not Preferences.LocalActorImage) AndAlso actor.actorthumb <> Nothing AndAlso (actor.actorthumb.IndexOf("http") <> -1 OrElse IO.File.Exists(actor.actorthumb)) Then
                        imgLocation = actor.actorthumb
                    End If
                    Exit For
                End If
            Next
        End If
        Try
            util_ImageLoad(PictureBox6, imgLocation, Utilities.DefaultActorPath)
            PictureBox6.SizeMode = PictureBoxSizeMode.Zoom
        Catch
        End Try
    End Sub

    Public Function TvCheckforExtraArt(ByVal Showpath As String) As Boolean
        Dim confirmedpresent As Boolean = False
        If Directory.Exists(Showpath) Then
            If File.Exists(Showpath & "clearart.png") Then tvFanlistbox.Items.Add("ClearArt") : confirmedpresent = True
            If File.Exists(Showpath & "logo.png") Then tvFanlistbox.Items.Add("Logo") : confirmedpresent = True
            If File.Exists(Showpath & "landscape.jpg") Then tvFanlistbox.Items.Add("Landscape") : confirmedpresent = True
            If File.Exists(Showpath & "character.png") Then tvFanlistbox.Items.Add("Character") : confirmedpresent = True
        End If
        Return confirmedpresent 
    End Function

    Public Sub TvPanel7Update(ByVal TvShPath As String)
        tvFanlistbox.Items.Clear()
        Panel7.Visible = TvCheckforExtraArt(TvShPath)
    End Sub

    Private Sub tvFanlistbox_Mouse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvFanlistbox.MouseDown
        Dim imagepath As String = Nothing
        Dim item As String = Nothing
        If e.button = Windows.Forms.MouseButtons.Right Then
            Dim index As Integer = tvFanlistbox.IndexFromPoint(New Point(e.X, e.Y))
            If index >= 0 Then tvFanlistbox.SelectedItem = tvFanlistbox.Items(index)
        End If
        If IsNothing(tvFanlistbox.SelectedItem) Then Exit Sub
        item = tvFanlistbox.SelectedItem.ToString.ToLower
        If Not String.IsNullOrEmpty(item) Then
                Dim tmpsh As TvShow = tv_ShowSelectedCurrently()
                imagepath = tmpsh.FolderPath 
                Dim suffix As String = If((item = "clearart" or item = "logo" or item = "character"),".png", ".jpg")
                imagepath &= item & suffix
        End If
        If e.Button = Windows.Forms.MouseButtons.Left Then 
            pbtvfanarttv.Visible = True
            If Not IsNothing(imagepath) Then util_ImageLoad(pbtvfanarttv, imagepath, "")
        ElseIf e.button = Windows.Forms.MouseButtons.Right Then
            Dim tempint = MessageBox.show("Do you wish to delete this image from" & vbCrLf & "this Tv Show?", "Fanart.Tv Artwork Delete", MessageBoxButtons.YesNoCancel)
            If tempint = Windows.Forms.DialogResult.No or tempint = DialogResult.Cancel Then Exit Sub
            If tempint = Windows.Forms.DialogResult.Yes Then
                Utilities.SafeDeleteFile(imagepath)
                TvPanel7Update(tv_ShowSelectedCurrently.FolderPath)
            End If
        End If
    End Sub

    Private Sub tvFanlistbox_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvFanlistbox.MouseLeave
        tvFanlistbox.ClearSelected()
        pbtvfanarttv.Image = Nothing
        pbtvfanarttv.Visible = False
    End Sub

    Public Sub tv_SeasonSelected(ByRef SelectedSeason As Media_Companion.TvSeason)
        SelectedSeason.ShowObj.ListActors.Clear()
        SelectedSeason.ShowObj.Load()
        Panel7.Visible = False
        Dim Show As Media_Companion.TvShow
        If SelectedSeason.SeasonNode.Parent.Tag IsNot Nothing Then
            Show = SelectedSeason.SeasonNode.Parent.Tag
        Else
            MsgBox("Show tag not set")
            Exit Sub
        End If
        tb_Sh_Ep_Title.BackColor = Color.White
        If Show.Title.Value <> Nothing Then
            If SelectedSeason.SeasonNumber = 0 Then
                tb_Sh_Ep_Title.Text = Utilities.ReplaceNothing(Show.Title.Value) & " - Specials"
            Else
                tb_Sh_Ep_Title.Text = Utilities.ReplaceNothing(Show.Title.Value) & " - " & Utilities.ReplaceNothing(SelectedSeason.SeasonNode.Text)
            End If
        End If
        If TabControl3.TabPages(1).Text = "Screenshot" Then
            TabControl3.TabPages.RemoveAt(1)
        End If

        ' changed indication of an issue, setting the title means that the title is saved to the nfo if the user exits. Yellow is the same colour as the unverified Button
        If Show.State = ShowState.Unverified Then tb_Sh_Ep_Title.BackColor = Color.Yellow
        If Show.State = ShowState.Error Then tb_Sh_Ep_Title.BackColor = Color.Red

        tb_ShPremiered.Text = Utilities.ReplaceNothing(Show.Premiered.Value)
        tb_ShGenre.Text = Utilities.ReplaceNothing(Show.Genre.Value)
        tb_ShTvdbId.Text = Utilities.ReplaceNothing(Show.TvdbId.Value)
        tb_ShImdbId.Text = Utilities.ReplaceNothing(Show.ImdbId.Value)
        tb_ShRating.Text = Utilities.ReplaceNothing(Show.Rating.Value)
        tb_ShCert.Text = Utilities.ReplaceNothing(Show.Mpaa.Value)
        tb_ShRunTime.Text = Utilities.ReplaceNothing(Show.Runtime.Value)
        tb_ShStudio.Text = Utilities.ReplaceNothing(Show.Studio.Value)
        tb_ShPlot.Text = Utilities.ReplaceNothing(Show.Plot.Value)
        Panel9.Visible = False
        Panel8.Visible = False
        lbl_sorttitle.Visible = True
        TextBox_Sorttitle.Visible = True
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
        Tv_TreeViewContext_RenameEp.Enabled = True
        Tv_TreeViewContext_RenameEp.Visible = True

        'MsgBox("Season")
        Dim season As String = SelectedSeason.SeasonLabel
        Dim trueseason As Integer = SelectedSeason.SeasonNumber
        Dim PaddedSeason As String = Utilities.PadNumber(SelectedSeason.SeasonNumber, 2)
        Dim tvpbright As String = Utilities.DefaultTvPosterPath 
        Dim tvpbbottom As String = Utilities.DefaultTvBannerPath 
        If trueseason = -1 Then
            If SelectedSeason.Poster.Image IsNot Nothing Then
                tvpbright = SelectedSeason.Poster.Path 
            Else
                If Preferences.postertype = "banner" Then
                    tvpbright = Show.ImagePoster.Path 
                Else
                    tvpbbottom = Show.ImageBanner.Path 
                End If
            End If
        ElseIf trueseason = 0 Then          'Specials
            If Preferences.EdenEnabled AndAlso Not Preferences.FrodoEnabled Then
                tvpbright = Show.FolderPath & "season-specials.tbn"
                If Not File.Exists(tvpbright) Then tvpbright = Show.FolderPath & "folder.jpg"
            End If
            If Preferences.FrodoEnabled Then
                tvpbright = Show.FolderPath & "season-specials-poster.jpg"
                If Not File.Exists(tvpbright) Then tvpbright = Show.FolderPath & "folder.jpg"
                tvpbbottom = Show.FolderPath & "season-specials-banner.jpg"
                If Not File.Exists(tvpbbottom) Then tvpbbottom = Show.FolderPath & "banner.jpg"
            End If
        Else                                'Season01 & up
            tvpbright = SelectedSeason.Poster.Path
            If Preferences.FrodoEnabled Then tvpbbottom = SelectedSeason.Banner.Path
        End If

        util_ImageLoad(tv_PictureBoxRight, tvpbright, Utilities.DefaultTvPosterPath)
        util_ImageLoad(tv_PictureBoxBottom, tvpbbottom, Utilities.DefaultTvBannerPath)

        If Show.NfoFilePath <> Nothing Then util_ImageLoad(tv_PictureBoxLeft, Show.FolderPath & "fanart.jpg", Utilities.DefaultTvFanartPath)

        Call tv_ActorsLoad(Show.ListActors)
        Show.UpdateTreenode()
    End Sub

    Public Sub tv_EpisodeSelected(ByRef SelectedEpisode As Media_Companion.TvEpisode)
        If TabControl3.TabPages(1).Text <> "Screenshot" Then
            If screenshotTab IsNot Nothing Then
                TabControl3.TabPages.Insert(1, screenshotTab)
                TabControl3.Refresh()
            End If
        End If
        Panel9.Visible = True
        Panel8.Visible = True   'set ep actor panel visible, we'll hide later if no actor's in episode.
        cmbxEpActor.Items.Clear()
        tbEpRole.Text = ""

        Dim Show As TvShow = tv_ShowSelectedCurrently()
        Dim season As Integer = SelectedEpisode.Season.Value
        Dim episode As Integer = SelectedEpisode.Episode.Value
        Dim SeasonObj As New Media_Companion.TvSeason
        If SelectedEpisode.EpisodeNode.Parent IsNot Nothing Then
            SeasonObj = SelectedEpisode.EpisodeNode.Parent.Tag
            If season = -1 Then season = SeasonObj.SeasonLabel
        End If

        Call ep_Load(SeasonObj, SelectedEpisode)

        lbl_sorttitle.Visible = False
        TextBox_Sorttitle.Visible = false
        Tv_TreeViewContext_ViewNfo.Enabled = True
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
        Tv_TreeViewContext_ReloadFromCache.Enabled = True
        Tv_TreeViewContext_OpenFolder.Enabled = True

        If SelectedEpisode.ListActors.Count < 1 Then    'If episode actors, don't load show's actors. (save some time)
            If SeasonObj.ShowObj.ListActors.Count = 0 Then Show.Load()
            Call tv_ActorsLoad(Show.ListActors)
        End If
    End Sub

    Private Function TestForMultiepisode(ByVal path As String)
        Dim multiepisode As Boolean = False
        Try
            Dim firstline As String = ""
            If IO.File.Exists(path) Then
                Dim listText As New List(Of String)
                Dim objLine As String = ""
                Using objReader As StreamReader = New StreamReader(path)
                    Do
                        objLine = objReader.ReadLine()
                        If objLine.IndexOf("<multiepisodenfo>") <> -1 Then
                            multiepisode = True
                            Exit Do
                        ElseIf objLine.IndexOf("<episodedetails>") <> -1 Then
                            multiepisode = False
                            Exit Do
                        End If
                    Loop Until objLine Is Nothing
                End Using
            End If
        Catch
        End Try
        Return multiepisode
    End Function

    Private Sub ep_Load(ByRef Season As Media_Companion.TvSeason, ByRef Episode As Media_Companion.TvEpisode, Optional ByVal epupdate As Boolean = False)
        Panel7.Visible = False

        'test if already loaded nfo into treeview, if so, then no need to reload
        If IsNothing(Episode.Plot.Value) OrElse epupdate Then
            'test for multiepisodenfo
            If TestForMultiepisode(Episode.NfoFilePath) Then
                Dim episodelist As New List(Of TvEpisode)
                episodelist = WorkingWithNfoFiles.ep_NfoLoad(Episode.NfoFilePath)
                For Each Ep In episodelist
                    If Ep.Season.Value = Episode.Season.Value And Ep.Episode.Value = Episode.Episode.Value Then
                        Episode.ListActors.Clear()
                        Episode.AbsorbTvEpisode(Ep)   'update treenode
                        Exit For
                    End If
                Next
            Else
                Episode.ListActors.Clear()
                Episode.Load()
            End If
        End If

        Dim tempstring As String = ""
        lb_EpDetails.Items.Clear()
        'lb_EpDetails.Items.Add("Details")

        cmbxEpActor.Items.Clear()
        tb_EpFilename.Text = Utilities.ReplaceNothing(IO.Path.GetFileName(Episode.NfoFilePath))
        tb_EpPath.Text = Utilities.ReplaceNothing(Episode.FolderPath)
        If Not IO.File.Exists(Episode.NfoFilePath) Then
            tb_Sh_Ep_Title.Text = "Unable to find episode: " & Episode.NfoFilePath
            Panel9.Visible = True
            Panel8.Visible = True
            cmbxEpActor.Items.Clear()
            tbEpRole.Text = ""
            Episode.EpisodeNode.BackColor = Color.Red
            Exit Sub
        Else
            Episode.EpisodeNode.BackColor = Color.Transparent   'i.e. back to normal
        End If

        tb_Sh_Ep_Title.Text ="'" &  Utilities.ReplaceNothing(Episode.Title.Value, "?") & "'"
        tb_EpRating.Text = Utilities.ReplaceNothing(Episode.Rating.Value)
        tb_EpPlot.Text = Utilities.ReplaceNothing(Episode.Plot.Value)
        tb_EpDirector.Text = Utilities.ReplaceNothing(Episode.Director.Value)
        tb_EpCredits.Text = Utilities.ReplaceNothing(Episode.Credits.Value)
        tb_EpAired.Text = Utilities.ReplaceNothing(Episode.Aired.Value)
        For f = 0 To cbTvSource.Items.Count - 1
            If cbTvSource.Items(f) = Episode.Source.value Then
                cbTvSource.SelectedIndex = f
                Exit For
            End If
        Next
        If Episode.Season.Value = "0" Then
            lbl_airbefore.Visible = True 
            lbl_airseason.Visible = True
            lbl_airepisode.Visible = True
            tb_airepisode.Visible = True
            tb_airseason.Visible = True
            tb_airseason.Text = Episode.DisplaySeason.Value
            tb_airepisode.Text = Episode.DisplayEpisode.Value 
        Else
            lbl_airbefore.Visible = False
            lbl_airseason.Visible = False
            lbl_airepisode.Visible = False
            tb_airepisode.Visible = False
            tb_airseason.Visible = False
            tb_airepisode.Text = ""
            tb_airseason.Text = ""
        End If

        util_EpisodeSetWatched(Episode.PlayCount.Value)

        Dim epdetails As String = ""
        epdetails += "Video: " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Width.Value, "?") & "x" & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Height.Value, "?")
        epdetails += ", (" & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Aspect.Value, "?") & ")"
        lb_EpDetails.Items.Add(epdetails)
        epdetails = " :- " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Codec.Value, "?")
        epdetails += ", @ " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Bitrate.Value, "?")
        lb_EpDetails.Items.Add(epdetails)
            
        If Episode.Details.StreamDetails.Audio.Count > 0 Then
            epdetails = "Audio: " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Audio(0).Codec.Value, "?")
            lb_EpDetails.Items.Add(epdetails)
            epdetails = Utilities.ReplaceNothing(Episode.Details.StreamDetails.Audio(0).Bitrate.Value, "?")
            epdetails += ", " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Audio(0).Channels.Value, "?") & " Ch"
            lb_EpDetails.Items.Add(epdetails)
        End If

        Dim aActor As Boolean = False
            For Each actor In Episode.ListActors
                If Not String.IsNullOrEmpty(actor.actorname) Then
                    cmbxEpActor.Items.Add(Utilities.ReplaceNothing(actor.actorname))
                    aActor = True
                End If
            Next
        If aActor Then
            cmbxEpActor.SelectedIndex = 0
        Else
            cmbxEpActor.Items.Clear()
            cmbxEpActor.Items.Add("")
            cmbxEpActor.SelectedIndex = 0
            Panel8.Visible = False 
        End If

        If (Episode IsNot Nothing AndAlso Episode.Thumbnail IsNot Nothing) Then
            Dim eptvleft As String = Episode.Thumbnail.Path 
            If Preferences.FrodoEnabled Then eptvleft = Episode.Thumbnail.Path.Replace(".tbn", "-thumb.jpg")
            util_ImageLoad(tv_PictureBoxLeft, eptvleft, Utilities.DefaultTvFanartPath)
        End If
        If (Season IsNot Nothing AndAlso Season.Poster IsNot Nothing) Then
            util_ImageLoad(tv_PictureBoxRight, Season.Poster.Path, Utilities.DefaultTvPosterPath)
            If Preferences.FrodoEnabled Then
                util_ImageLoad(tv_PictureBoxBottom, Season.Banner.Path, Utilities.DefaultTvBannerPath)
            End If
        End If

        Dim video_flags = GetEpMediaFlags()
        movieGraphicInfo.OverlayInfo(tv_PictureBoxLeft, tb_EpRating.Text, video_flags)
        Panel9.Visible = True

    End Sub

    Private Sub ep_VideoSourcePopulate()
        Try
            cbTvSource.Items.Clear()
            cbTvSource.Items.Add("")
            For Each mset In Preferences.releaseformat
                cbTvSource.Items.Add(mset)
            Next

            cbTvSource.SelectedIndex = 0
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        End Try
    End Sub

    ' We need to load images in this way so that they remain unlocked by the OS so we can update the fanart/poster files as needed
    Public Shared Function util_ImageLoad(ByVal PicBox As PictureBox, ByVal ImagePath As String, ByVal DefaultPic As String) As Boolean
        Dim PathToUse As String = DefaultPic
        Dim cachename As String = ""
        PicBox.Tag = Nothing

        If Utilities.UrlIsValid(ImagePath) Then
            cachename = Utilities.Download2Cache(ImagePath)
            If cachename <> "" Then PathToUse = cachename
        ElseIf File.Exists(ImagePath) Then
            PathToUse = ImagePath
        End If
        If PathToUse = "" Then
            PicBox.Image = Nothing
            Exit Function 
        End If
        Try
            Using fs As New System.IO.FileStream(PathToUse, System.IO.FileMode.Open, System.IO.FileAccess.Read), ms As System.IO.MemoryStream = New System.IO.MemoryStream()
                fs.CopyTo(ms)
                ms.Seek(0, System.IO.SeekOrigin.Begin)
                PicBox.Image = Image.FromStream(ms)
            End Using
            PicBox.Tag = PathToUse
        Catch
            'Image is invalid e.g. not downloaded correctly -> Delete it
            Try
                File.Delete(PathToUse)
            Catch
            End Try

            Try
                Using fs As New System.IO.FileStream(DefaultPic, System.IO.FileMode.Open, System.IO.FileAccess.Read), ms As System.IO.MemoryStream = New System.IO.MemoryStream()
                    fs.CopyTo(ms)
                    ms.Seek(0, System.IO.SeekOrigin.Begin)
                    PicBox.Image = Image.FromStream(ms)
                End Using
                PicBox.ImageLocation = DefaultPic
            Catch
                Return False
            End Try
            Return True
        End Try

        Return True
    End Function


#Region "Tv Cache Routines"
    Public Sub tv_CacheLoad()
        Cache.TvCache.TvCachePath = Preferences.workingProfile.TvCache
        Cache.TvCache.Load()
        TvTreeview.Nodes.Clear()              'clear the treeview of old data
        ''Dirty work around until TvShows is repalced with TvCache.Shows universally
        For Each TvShow As Media_Companion.TvShow In Cache.TvCache.Shows
            'TvShow.UpdateTreenode()
            TvTreeview.Nodes.Add(TvShow.ShowNode)
            TvShow.UpdateTreenode()
        Next
        TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
        TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
        'TvTreeview.Sort()
     
    End Sub

    Private Sub tv_CacheRefreshSelected(ByVal Show As TvShow)
        tv_CacheRefresh(Show)
    End Sub

    Public Function Tv_CacheSave() As Boolean

        Cache.TvCache.TvCachePath = Preferences.workingProfile.TvCache
        Cache.TvCache.Save()
        Return False
    End Function

    Private Sub tv_CacheRefresh(Optional ByVal TvShowSelected As TvShow = Nothing) 'refresh = clear & recreate cache from nfo's
        frmSplash2.Text = "Refresh TV Shows..."
        frmSplash2.Label1.Text = "Searching TV Folders....."
        frmSplash2.Label1.Visible = True
        frmSplash2.Label2.Visible = True
        frmSplash2.ProgressBar1.Visible = True
        frmSplash2.ProgressBar1.Maximum = Preferences.tvFolders.Count ' - 1
        frmSplash2.Show()
        Application.DoEvents()

        Dim fulltvshowlist As New List(Of TvShow)
        Dim fullepisodelist As New List(Of TvEpisode)

        tv_RefreshLog("Starting TV Show Refresh" & vbCrLf & vbCrLf, , True)
        TextBox_TotTVShowCount.Text = ""
        TextBox_TotEpisodeCount.Text = ""
        Me.Enabled = False

        Dim nofolder As New List(Of String)
        Dim prgCount As Integer = 0
        Dim FolderList As New List(Of String)

        If TvShowSelected IsNot Nothing Then ' if we have provided a tv show, then add just this show to the list, else scan through all of the folders
            FolderList.Add(TvShowSelected.FolderPath) 'add the single show to our list
            Cache.TvCache.Remove(TvShowSelected)
            For Each episode In TvShowSelected.Episodes
                If Preferences.displayMissingEpisodes AndAlso episode.IsMissing = True Then
                    fullepisodelist.Add(episode)
                    Cache.TvCache.Remove(episode)
                Else
                    Cache.TvCache.Remove(episode)
                End If
            Next
            For Each showitem In Cache.TvCache.Shows
                fulltvshowlist.Add(showitem)
            Next
            For Each episodeitem In Cache.TvCache.Episodes
                fullepisodelist.Add(episodeitem)
            Next
        Else
            For Each ep In Cache.TvCache.Episodes
                If Preferences.displayMissingEpisodes AndAlso ep.IsMissing = True Then
                    fullepisodelist.Add(ep)
                End If
            Next
            FolderList = Preferences.tvFolders ' add all folders to list to scan
            Cache.TvCache.Clear() 'Full rescan means clear all old data
            TvTreeview.Nodes.Clear()
            realTvPaths.Clear()
        End If

        For Each tvfolder In FolderList
            frmSplash2.Label2.Text = "(" & prgCount + 1 & "/" & Preferences.tvFolders.Count & ") " & tvfolder
            frmSplash2.ProgressBar1.Value = prgCount
            If Not (Directory.Exists(tvfolder)) Then 
                nofolder.Add(tvfolder)
                Continue For
            End If

            prgCount += 1
            Application.DoEvents()
            Dim newtvshownfo As New TvShow
            newtvshownfo.NfoFilePath = IO.Path.Combine(tvfolder, "tvshow.nfo")

            newtvshownfo.Load() 
            fulltvshowlist.Add(newtvshownfo)
            Dim episodelist As New List(Of TvEpisode)
            episodelist = loadepisodes(newtvshownfo, episodelist)
            For Each ep In episodelist
                ep.ShowId.Value = newtvshownfo.TvdbId.Value
                If Preferences.displayMissingEpisodes Then
                    For i = 0 to fullepisodelist.Count-1        'check to remove missing episode if valid episode now exists.
                        Dim fulep = fullepisodelist.Item(i)
                        If fulep.ShowObj Is Nothing Then
                            fullepisodelist.RemoveAt(i)
                            Exit For
                        End If
                        If fulep.ShowObj.Title.Value = ep.ShowObj.Title.Value AndAlso fulep.Season.Value = ep.Season.Value Then
                            If fulep.Episode.Value = ep.Episode.Value Then
                                fullepisodelist.RemoveAt(i)
                                Exit For
                            End If
                        End If
                    Next
                End If
                fullepisodelist.Add(ep)
            Next
        Next

        If nofolder.Count > 0 Then
            Dim mymsg As String
            mymsg = (nofolder.Count).ToString + " folder/s missing:" + vbCrLf + vbCrLf
            For Each item In nofolder
                mymsg = mymsg + item + vbCrLf
            Next
            mymsg = mymsg + vbCrLf + "Do you wish to remove these folders" + vbCrLf + "from your list of TV Folders?" + vbCrLf
            If MsgBox(mymsg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                tv_Showremovedfromlist(nofolder)
            End If
        End If

        frmSplash2.Label2.Visible = False

        frmSplash2.Label1.Text = "Saving Cache..."
        Windows.Forms.Application.DoEvents()
        Tv_RefreshCacheSave(fulltvshowlist, fullepisodelist)    'save the cache file

        frmSplash2.Label1.Text = "Loading Cache..."
        Windows.Forms.Application.DoEvents()
        tv_CacheLoad()    'reload the cache file to update the treeview
        If Preferences.fixnfoid Then cbTv_fixNFOid.CheckState = CheckState.Unchecked
        Me.Enabled = True
        TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
        TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
        frmSplash2.Hide()
        If Not tv_IMDbID_warned And tv_IMDbID_detected Then
            MessageBox.Show(tv_IMDbID_detectedMsg, "TV Show ID", MessageBoxButtons.OK, MessageBoxIcon.Information)
            tv_IMDbID_warned = True
        End If

        tv_Filter()
        BlinkTaskBar()
    End Sub

    Private Sub Tv_RefreshCacheSave(ByVal tvshowlist As List(Of TvShow), ByVal episodeelist As List(Of TvEpisode))
        Dim tvcachepath As String = Preferences.workingProfile.TvCache
        Dim document As New XmlDocument
        Dim root As XmlElement
        Dim child As XmlElement
        Dim childchild As XmlElement
        Dim xmlproc As XmlDeclaration
        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        document.AppendChild(xmlproc)
        root = document.CreateElement("tvcache")
        root.SetAttribute("ver", "3.5")
        For Each item In tvshowlist
            child = document.CreateElement("tvshow")
            child.SetAttribute("NfoPath", item.NfoFilePath)

            childchild = document.CreateElement("state")
            childchild.InnerText = item.State '"0"
            child.AppendChild(childchild)

            childchild = document.CreateElement("title")
            childchild.InnerText = item.Title.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("id")
            childchild.InnerText = item.TvdbId.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("status")
            childchild.InnerText = item.Status.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("sortorder")
            childchild.InnerText = item.SortOrder.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("language")
            childchild.InnerText = item.Language.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("episodeactorsource")
            childchild.InnerText = item.EpisodeActorSource.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("imdbid")
            childchild.InnerText = item.ImdbId.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("playcount")
            childchild.InnerText = item.Playcount.Value
            child.AppendChild(childchild)

            root.AppendChild(child)
        Next

        For Each item In episodeelist
            child = document.CreateElement("episodedetails")
            child.SetAttribute("NfoPath", item.NfoFilePath)

            childchild = document.CreateElement("missing")
            childchild.InnerText = item.IsMissing
            child.AppendChild(childchild)

            childchild = document.CreateElement("title")
            childchild.InnerText = item.Title.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("season")
            childchild.InnerText = item.Season.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("episode")
            childchild.InnerText = item.Episode.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("aired")
            childchild.InnerText = item.Aired.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("showid")
            childchild.InnerText = item.ShowId.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("uniqueid")
            childchild.InnerText = item.UniqueId.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("epextn")
            childchild.InnerText = item.EpExtn.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("playcount")
            childchild.InnerText = item.PlayCount.Value
            child.AppendChild(childchild)

            root.AppendChild(child)
        Next
        document.AppendChild(root)
        Dim output As New XmlTextWriter(tvcachepath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        document.WriteTo(output)
        output.Close()
    End Sub

    Private Function loadepisodes(ByVal newtvshownfo As TvShow, ByRef episodelist As List(Of TvEpisode))
        Dim missingeppath As String = IO.Path.Combine(Preferences.applicationPath, "missing\")
        Dim newlist As New List(Of String)
        newlist.Clear()
        newlist = Utilities.EnumerateFolders(newtvshownfo.FolderPath) 'TODO: Restore loging functions
        newlist.Add(newtvshownfo.FolderPath)
        For Each folder In newlist
            Dim dir_info As New System.IO.DirectoryInfo(folder)
            Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles("*.NFO", SearchOption.TopDirectoryOnly)
            For Each fs_info As System.IO.FileInfo In fs_infos
                If IO.Path.GetFileName(fs_info.FullName.ToLower) <> "tvshow.nfo" And fs_info.ToString.Substring(0, 2) <> "._" Then
                    Dim EpNfoPath As String = fs_info.FullName
                    If ep_NfoValidate(EpNfoPath) Then
                        Dim multiepisodelist As New List(Of TvEpisode)
                        Dim need2resave As Boolean = False
                        multiepisodelist = ep_NfoLoad(EpNfoPath)
                        For Each Ep In multiepisodelist
                            If Ep.ShowId.Value <> newtvshownfo.TvdbId.Value AndAlso newtvshownfo.TvdbId.Value <> "" Then need2resave = True
                            Ep.ShowObj = newtvshownfo
                            Dim missingNfoPath As String = missingeppath & newtvshownfo.TvdbId.Value & "." & Ep.Season.Value & "." & Ep.Episode.Value & ".nfo"
                            If IO.File.Exists(missingNfoPath) Then Utilities.SafeDeleteFile(missingNfoPath)
                            episodelist.Add(Ep)
                        Next
                        If need2resave Then ep_NfoSave(multiepisodelist, EpNfoPath)    'If new ShowID stored, resave episode nfo.
                    End If
                End If
            Next fs_info
        Next
        Return episodelist
    End Function

    Private Sub tv_ShowListLoad()
        If TextBox26.Text <> "" Then

            messbox = New frmMessageBox("Please wait,", "", "Getting possible TV Shows from TVDB")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()

            Dim tvdbstuff As New TVDBScraper
            Dim mirror As List(Of String) = tvdbstuff.getmirrors()
            Dim showslist As String = tvdbstuff.findshows(TextBox26.Text, mirror(0))

            listOfShows.Clear()
            If showslist = "error" Then
                messbox.Close()
                MsgBox("TVDB seems to have an error with the xml for this search")
                Exit Sub
            End If
            If showslist <> "none" Then
                Dim showlist As New XmlDocument
                showlist.LoadXml(showslist)
                For Each thisresult As XmlNode In showlist("allshows")
                    Select Case thisresult.Name
                        Case "show"
                            Dim results As XmlNode = Nothing
                            Dim lan As New str_PossibleShowList(SetDefaults)
                            For Each results In thisresult.ChildNodes
                                Select Case results.Name
                                    Case "showid"
                                        lan.showid = results.InnerText
                                    Case "showtitle"
                                        lan.showtitle = results.InnerText
                                    Case "showbanner"
                                        lan.showbanner = results.InnerText
                                End Select
                            Next
                            Dim exists As Boolean = False
                            For Each item In listOfShows
                                If item.showid = lan.showid Then
                                    exists = True
                                    Exit For
                                End If
                            Next
                            If exists = False Then
                                listOfShows.Add(lan)
                            End If
                    End Select
                Next
            Else
                Dim lan As New str_PossibleShowList(SetDefaults)
                lan.showid = "none"
                lan.showtitle = "TVDB Search Returned Zero Results"
                lan.showbanner = Nothing
                listOfShows.Add(lan)

                lan.showid = "none"
                lan.showtitle = "Adjust the TV Shows Title & search again"
                lan.showbanner = Nothing
                listOfShows.Add(lan)
            End If
            ListBox3.Items.Clear()
            For Each item In listOfShows
                ListBox3.Items.Add(item.showtitle)
            Next

            ListBox3.SelectedIndex = 0
            If listOfShows(0).showbanner <> Nothing Then
                Try
                    util_ImageLoad(PictureBox9, listOfShows(0).showbanner, Utilities.DefaultTvBannerPath)
                Catch ex As Exception
                    PictureBox9.Image = Nothing
                End Try
            End If

            Call util_LanguageCheck()
            messbox.Close()
        Else
            MsgBox("Please Enter a Search Term")
        End If
    End Sub

    Private Sub Tv_CacheCheckDuplicates()
        Dim progress As String = ""  '"Duplicates Found in:" & vbCrLf & vbcrlf
        Dim Showfound As String = ""
        Dim Episodesfound As String = ""
        Dim Count As Integer = 0
        Dim lasttestedseason As String = ""
        Dim lasttestedepisode As String = ""
        For Each sh In Cache.TvCache.shows
            If Not String.IsNullOrEmpty(sh.TvdbId.Value) Then
                Dim unique As Integer = 0
                Dim shid As Integer = 0
                Showfound = sh.Title.Value
                For Each episo In sh.episodes
                    If String.IsNullOrEmpty(episo.ShowId.Value) Then shid +=1
                    If String.IsNullOrEmpty(episo.UniqueId.Value) Then unique +=1
                    If Not String.IsNullOrEmpty(episo.ShowId.Value) Then
                        Dim Thisseason As String = episo.Season.Value
                        Dim Thisepisode As String = episo.Episode.Value
                        If Thisseason = lasttestedseason AndAlso Thisepisode = lasttestedepisode Then Continue For
                        Dim testShow As String = episo.ShowId.Value
                        For Each testep In sh.episodes
                            If Not String.IsNullOrEmpty(testep.ShowId.Value) AndAlso testep.ShowId.Value = testShow Then
                                If testep.Season.Value = Thisseason Then
                                    If testep.Episode.Value = Thisepisode Then
                                        Count += 1
                                        Episodesfound &= testep.NfoFilePath & vbcrlf
                                    End If
                                End If
                            End If
                        Next
                        If Count > 1 Then
                            progress &= "Duplicates Found in:" & vbCrLf & vbcrlf
                            progress &= Showfound & vbCrLf & Episodesfound & vbCrLf & vbcrlf
                        End If
                        Count = 0
                        Episodesfound = ""
                        lasttestedseason = Thisseason
                        lasttestedepisode = Thisepisode
                    End If
                Next
                If shid > 0 Then
                    progress &= "Show:- " & sh.Title.Value & vbCrLf & "contains: - """ & shid & """  episodes without valid Tvdb Id!" & vbcrlf & vbcrlf
                End If
                If unique > 0 Then
                    progress &= "Show:- " & sh.Title.Value & vbCrLf & "contains: - """ & unique & """  episodes without valid <uniqueid> Tag!" & vbcrlf & vbcrlf
                End If
            Else
                progress &= "Show:- " & sh.Title.Value & vbCrLf & "does not have a TVDB ID.  Skipped for checking Duplicates" & vbcrlf & vbcrlf
            End If
            Showfound = ""
        Next
        If Not progress = "" Then
            Dim MyFormObject As New frmoutputlog(progress, True, True)
            MyFormObject.ShowDialog()
        Else
            MsgBox ("No Duplicates found")
        End If
    End Sub
#End Region

    Private Sub bckgrnd_tvshowscraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bckgrnd_tvshowscraper.DoWork
        Try
            Dim nfoFunction As New WorkingWithNfoFiles
            Dim args As TvdbArgs = e.Argument
            Dim searchTVDbID As String = If(IsNothing(args), "", args.tvdbid)
            Dim searchLanguage As String = If(IsNothing(args), Preferences.TvdbLanguageCode, args.lang)
            Dim haveTVDbID As Boolean = Not String.IsNullOrEmpty(searchTVDbID)
            Dim success As Boolean = False
            Dim i As Integer = 0
            Dim x As String = newTvFolders.Count.ToString
            Do While newTvFolders.Count > 0
                tvprogresstxt = ""
                i += 1
                Dim NewShow As New TvShow
                NewShow.NfoFilePath = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")
                NewShow.TvdbId.Value = searchTVDbID
                NewShow.State = Media_Companion.ShowState.Unverified
                tvprogresstxt &= "Scraping Show " & i.ToString & " of " & x & " : "
                bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                If Not haveTVDbID And NewShow.FileContainsReadableXml Then
                    Dim validcheck As Boolean = nfoFunction.tv_NfoLoadCheck(NewShow.NfoFilePath)
                    If validcheck Then
                        NewShow.Load()
                    End If
                Else
                    If haveTVDbID Then
                        NewShow.State = Media_Companion.ShowState.Open
                    Else
                        'Resolve show name from folder
                        Dim FolderName As String = Utilities.GetLastFolder(newTvFolders(0) & "\")
                        If FolderName.ToLower.Contains(excludefromshowfoldername.ToLower) Then
                            Dim indx As Integer = FolderName.ToLower.IndexOf(excludefromshowfoldername.ToLower)
                            Dim excludearticle As String = FolderName.Substring(indx-1, excludefromshowfoldername.Length+1)
                            FolderName = FolderName.Replace(excludearticle, "")
                        End If
                        FolderName = FolderName.Replace(excludefromshowfoldername, "")
                        Dim M As Match
                        M = Regex.Match(FolderName, "\s*[\(\{\[](?<date>[\d]{4})[\)\}\]]")
                        If M.Success = True Then
                            FolderName = String.Format("{0} ({1})", FolderName.Substring(0, M.Index), M.Groups("date").Value)
                        End If
                        NewShow.Title.Value = FolderName

                        If NewShow.PossibleShowList IsNot Nothing Then
                            Dim TempSeries As New Tvdb.Series
                            TempSeries = Tvdb.FindBestPossibleShow(NewShow.PossibleShowList, NewShow.Title.Value, searchLanguage)
                            If TempSeries.Similarity > 0.9 AndAlso TempSeries.Language.Value = searchLanguage Then
                                NewShow.State = Media_Companion.ShowState.Open
                            End If
                            searchTVDbID = TempSeries.Id.Value
                        End If
                    End If

                    If Not String.IsNullOrEmpty(searchTVDbID) Then
                        Dim tvdbstuff As New TVDBScraper
                        Dim SeriesInfo As Tvdb.ShowData = tvdbstuff.GetShow(searchTVDbID, searchLanguage, SeriesXmlPath)
                        searchTVDbID = ""
                        If SeriesInfo.FailedLoad Then
                            MsgBox("Please adjust the TV Show title and try again", _
                                   MsgBoxStyle.OkOnly, _
                                   String.Format("'{0}' - No Show Returned", NewShow.Title.Value))
                            bckgrnd_tvshowscraper.ReportProgress(1, NewShow)
                            newTvFolders.RemoveAt(0)
                            Continue Do
                        End If
                        tvprogresstxt &= "Show Title: " & SeriesInfo.Series(0).SeriesName.Value & " "
                        bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                        NewShow.AbsorbTvdbSeries(SeriesInfo.Series(0))
                        NewShow.Language.Value = searchLanguage

                        tvprogresstxt &= " - Getting Actors"
                        bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                        If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 3 Or NewShow.ImdbId.Value = Nothing Then
                            success = TvGetActorTvdb(NewShow)
                        End If

                        If (Preferences.TvdbActorScrape = 1 Or Preferences.TvdbActorScrape = 2) And NewShow.ImdbId.Value <> Nothing Then
                            success = TvGetActorImdb(NewShow)
                        End If
                        If success Then 
                            tvprogresstxt &= ": -OK!"
                        Else
                            tvprogresstxt &= ": -error!!"
                        End If

                        If Preferences.TvFanartTvFirst Then
                            If Preferences.TvDlFanartTvArt OrElse Preferences.TvChgShowDlFanartTvArt Then 
                                tvprogresstxt &= " - Getting FanartTv Artwork"
                                bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                                TvFanartTvArt(NewShow, False)
                            End If
                            If Preferences.tvdlfanart Or Preferences.tvdlposter or Preferences.tvdlseasonthumbs Then
                                tvprogresstxt &= " - Getting TVDB artwork"
                                bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                            'End If
                            success = TvGetArtwork(NewShow, True, True, True, Preferences.dlTVxtrafanart, searchLanguage)
                            'If Preferences.tvdlfanart Or Preferences.tvdlposter or Preferences.tvdlseasonthumbs Then
                                If success Then 
                                    tvprogresstxt &= ": OK!"
                                Else
                                    tvprogresstxt &= ": error!!"
                                End If
                            End If
                            'If Preferences.TvDlFanartTvArt OrElse Preferences.TvChgShowDlFanartTvArt Then 
                            '    tvprogresstxt &= " - Getting FanartTv Artwork"
                            '    bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                            '    TvFanartTvArt(NewShow, False)
                            'End If
                        Else
                            If Preferences.tvdlfanart Or Preferences.tvdlposter or Preferences.tvdlseasonthumbs Then
                                tvprogresstxt &= " - Getting TVDB artwork"
                                bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                            'End If
                                success = TvGetArtwork(NewShow, True, True, True, Preferences.dlTVxtrafanart, searchLanguage)
                            'If Preferences.tvdlfanart Or Preferences.tvdlposter or Preferences.tvdlseasonthumbs Then
                                If success Then 
                                    tvprogresstxt &= ": OK!"
                                Else
                                    tvprogresstxt &= ": error!!"
                                End If
                            End If
                            If Preferences.TvDlFanartTvArt OrElse Preferences.TvChgShowDlFanartTvArt Then 
                                tvprogresstxt &= " - Getting FanartTv Artwork"
                                bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)
                                TvFanartTvArt(NewShow, Preferences.TvChgShowDlFanartTvArt)
                            End If
                        End If
                        'If Preferences.tvfolderjpg OrElse Preferences.seasonfolderjpg Then
                        '    TvCheckfolderjpgart(NewShow)
                        'End If

                        tvprogresstxt &= " - Completed. Saving Show."
                        bckgrnd_tvshowscraper.ReportProgress(0, tvprogresstxt)

                        If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 2 Then
                            NewShow.EpisodeActorSource.Value = "tvdb"
                        Else
                            NewShow.EpisodeActorSource.Value = "imdb"
                        End If
                        If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 3 Then
                            NewShow.TvShowActorSource.Value = "tvdb"
                        Else
                            NewShow.TvShowActorSource.Value = "imdb"
                        End If

                        NewShow.SortOrder.Value = Preferences.sortorder

                        nfoFunction.tvshow_NfoSave(NewShow, True)
                        'NewShow.Save()
                    End If
                End If

                Cache.TvCache.Add(NewShow)
                If Not NewShow.FailedLoad Then   'If show fails to scrape, do not add episodes to cache.
                    Dim episodelist As New List(Of TvEpisode)
                    episodelist = loadepisodes(NewShow, episodelist)
                    For Each ep In episodelist
                        NewShow.AddEpisode(ep)
                    Next
                    TvCheckfolderjpgart(NewShow)
                End If
                If Not Preferences.tvFolders.Contains(newTvFolders(0)) Then
                    Preferences.tvFolders.Add(newTvFolders(0))
                End If
                bckgrnd_tvshowscraper.ReportProgress(1, NewShow)
                newTvFolders.RemoveAt(0)
            Loop
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub bckgrnd_tvshowscraper_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bckgrnd_tvshowscraper.RunWorkerCompleted
        Try
            ToolStripStatusLabel5.Text = "Saving data"
            Tv_CacheSave()
            tv_CacheLoad()
            tv_Filter()
            'ToolStripStatusLabel5.Text = "Populating shows"
            'tv_CacheRefresh()
            ToolStripStatusLabel5.Visible = False
            BlinkTaskBar
            GC.Collect()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub bckgrnd_tvshowscraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bckgrnd_tvshowscraper.ProgressChanged
        Try
            If e.ProgressPercentage = 0 Then
                ToolStripStatusLabel5.Text = e.UserState
                ToolStripStatusLabel5.Visible = True
            Else
                Dim NewShow As TvShow = e.UserState
                ToolStripStatusLabel5.Text = "Scraping TV Shows, " & newTvFolders.Count & " remaining"
                ToolStripStatusLabel5.Visible = True

                realTvPaths.Add(NewShow.FolderPath)
                TvTreeview.Nodes.Add(NewShow.ShowNode)
                NewShow.UpdateTreenode()

                TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
                TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
                'Me.BringToFront()
                'Me.Activate()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tvbckrescrapewizard_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles tvbckrescrapewizard.DoWork
        Try
            Dim showprocesscount As Integer = 0
            Dim progresstext As String = ""
            Dim progress As Integer = 0
            Dim progcount As Integer = 0
            'Dim singleshnum As Integer = 0
            Dim shcachecount As Integer = Cache.TvCache.Shows.Count
            Dim done As Integer = 0
            'Dim SelectedShow As TvShow
            If singleshow Then
                showslist = tv_ShowSelectedCurrently()
                For x = Cache.TvCache.Shows.Count - 1 To 0 Step -1
                    If Cache.TvCache.Shows(x).Title.Value = showslist.Title.Value Then
                        shcachecount = x + 1
                        Exit For
                    End If
                Next
            End If
            For f = shcachecount - 1 To 0 Step -1
                If Cache.TvCache.Shows(f).State = Media_Companion.ShowState.Open Or Cache.TvCache.Shows(f).State = -1 Or tvBatchList.includeLocked = True Then
                    If tvBatchList.doEpisodes = True Then
                        showprocesscount += (Cache.TvCache.Shows(f).Episodes.Count - Cache.TvCache.Shows(f).MissingEpisodes.Count)
                        showprocesscount += 1
                        progcount += 1
                    Else
                        showprocesscount += 1
                        progcount += 1
                    End If
                End If
                If singleshow Then Exit For
            Next
            Dim showsdone As Integer = 0
            Dim showcounter As Integer = 0
            For f = shcachecount - 1 To 0 Step -1
                If tvBatchList.RewriteAllNFOs Then
                    If Cache.TvCache.Shows(f).State = 0 Or tvBatchList.includeLocked = True Then
                        Dim SelectedSeries As New TvShow
                        SelectedSeries = nfoFunction.tvshow_NfoLoad(Cache.TvCache.Shows(f).NfoFilePath)
                        Call FixTvActorsNfo(SelectedSeries)
                        Call nfoFunction.tvshow_NfoSave(SelectedSeries, True)
                        For g = Cache.TvCache.Shows(f).Episodes.Count - 1 To 0 Step -1
                            Dim epcount As Integer = Cache.TvCache.Shows(f).Episodes.Count
                            progresstext = "Rewriting nfo's of Show: " & Cache.TvCache.Shows(f).Title.Value & ", Episode: " & epcount - g & " of " & epcount & ", Episode: " & Cache.TvCache.Shows(f).Episodes(g).Season.Value & "x" & Cache.TvCache.Shows(f).Episodes(g).Episode.Value & " - " & Cache.TvCache.Shows(f).Episodes(g).Title.Value
                            If done > 0 Then
                                progress = (100 / showprocesscount) * done
                            Else
                                progress = 0
                            End If
                            If Cache.TvCache.Shows(f).Episodes(g).IsMissing Then
                                progresstext &= "Skip Missing episode"
                                Continue For
                            End If
                            tvbckrescrapewizard.ReportProgress(progress, progresstext)
                            Dim listofepisodes As New List(Of TvEpisode)
                            listofepisodes.Clear()
                            listofepisodes = WorkingWithNfoFiles.ep_NfoLoad(Cache.TvCache.Shows(f).Episodes(g).NfoFilePath)
                            WorkingWithNfoFiles.ep_NfoSave(listofepisodes, listofepisodes(0).NfoFilePath)
                        Next
                    End If
                    If singleshow Then Exit For
                    Continue For
                End If

                If Cache.TvCache.Shows(f).State = Media_Companion.ShowState.Open OrElse Cache.TvCache.Shows(f).State = -1 OrElse tvBatchList.includeLocked = True Then
                    showcounter += 1
                    progresstext = "Working on Show: " & showcounter.ToString & " of " & progcount
                    If done > 0 Then
                        progress = (100 / showprocesscount) * done
                    Else
                        progress = 0
                    End If
                    tvbckrescrapewizard.ReportProgress(progress, progresstext)
                    Dim editshow As New TvShow
                    editshow = nfoFunction.tv_NfoLoadFull(Cache.TvCache.Shows(f).NfoFilePath)
                    If tvBatchList.doShowActors Then
                        editshow.ListActors.Clear()
                    End If
                    
                    Dim tvdbstuff As New TVDBScraper
                    Dim tvseriesdata As New Tvdb.ShowData 
                    Dim language As String = editshow.Language.Value
                    If language = "" Then language = "en"
                    tvseriesdata = tvdbstuff.GetShow(editshow.TvdbId.Value, language, SeriesXmlPath)
                    If tvseriesdata.FailedLoad Then
                        progresstext &= "Failed to load xml data"
                        tvbckrescrapewizard.ReportProgress(progress, progresstext)
                        Continue For
                    End If
                    If tvBatchList.doShows = True Then
                        If tvBatchList.doShowBody = True Or tvBatchList.doShowActors = True Then
                            Try
                                editshow.ImdbId.Value = tvseriesdata.Series(0).ImdbId.Value 
                                If tvBatchList.shMpaa Then editshow.Mpaa.Value = tvseriesdata.Series(0).ContentRating.Value
                                If tvBatchList.shYear Then 
                                    editshow.Premiered.Value =  tvseriesdata.Series(0).FirstAired.Value
                                    editshow.Year.Value = editshow.Premiered.Value.Substring(0,4)
                                End If
                                If tvBatchList.shGenre Then
                                    Dim newstring As String
                                    newstring = tvseriesdata.Series(0).Genre.Value 
                                    newstring = newstring.TrimEnd("|")
                                    newstring = newstring.TrimStart("|")
                                    newstring = newstring.Replace("|", " / ")
                                    editshow.Genre.Value = newstring
                                End If
                                If tvBatchList.shStudio     Then editshow.Studio.Value = tvseriesdata.Series(0).Network.Value
                                If tvBatchList.shPlot       Then editshow.Plot.Value = tvseriesdata.Series(0).Overview.Value
                                If tvBatchList.shRating     Then editshow.Rating.Value = tvseriesdata.Series(0).Rating.Value 
                                If tvBatchList.shRuntime    Then editshow.Runtime.Value =  tvseriesdata.Series(0).RunTime.Value
                                If tvBatchList.shStatus     Then editshow.Status.Value = tvseriesdata.Series(0).Status.Value
                                Dim episodeguideurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & editshow.TvdbId.Value & "/all/" & language & ".zip"
                                    editshow.EpisodeGuideUrl.Value = ""
                                    editshow.Url.Value = episodeguideurl
                                    editshow.Url.Node.SetAttributeValue("cache", editshow.TvdbId.Value)
                                    editshow.Url.AttachToParentNode(editshow.EpisodeGuideUrl.Node)

                                If tvBatchList.doShowActors = True Then
                                    If editshow.TvShowActorSource.Value = Nothing Then 
                                        If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 3 Then
                                            editshow.TvShowActorSource.Value = "tvdb"
                                        Else
                                            editshow.TvShowActorSource.Value = "imdb"
                                        End If
                                    End If
                                    If editshow.TvShowActorSource.Value = "tvdb" Then TvGetActorTvdb(editshow)
                                    If editshow.TvShowActorSource.Value = "imdb" Then TvGetActorImdb(editshow)
                                End If
                                
                            Catch ex As Exception
#If SilentErrorScream Then
                                Throw ex
#End If
                            End Try
                            Call nfoFunction.tvshow_NfoSave(editshow, True)

                            'editshow.IsCache = True          'this doesn't stick so I had to remove the test in show.load

                        End If

                        'Posters, Fanart and Season art
                        If tvBatchList.doShowArt = True Then
                            If tvBatchList.shDelArtwork Then TvDeleteShowArt(Cache.TvCache.Shows(f), False)
                            If tvBatchList.shFanart orElse tvBatchList.shPosters OrElse tvBatchList.shSeason OrElse tvBatchList.shXtraFanart Then
                                TvGetArtwork(Cache.TvCache.Shows(f), tvBatchList.shFanart, tvBatchList.shPosters, tvBatchList.shSeason, tvBatchList.shXtraFanart, force:= False)
                            End If
                            If tvBatchList.shFanartTvArt Then TvFanartTvArt(Cache.TvCache.Shows(f), False) 'We're only looking for missing art from Fanart.Tv
                        End If
                    End If
                    If tvBatchList.doEpisodes = True Then
                        Dim i As Integer = 0
                        Dim TotalEpisodes As Integer = Cache.TvCache.Shows(f).Episodes.Count - Cache.TvCache.Shows(f).MissingEpisodes.count
                        For g = Cache.TvCache.Shows(f).Episodes.Count - 1 To 0 Step -1
                            If Cache.TvCache.Shows(f).Episodes(g).IsMissing Then Continue For
                            i +=  1
                            progresstext = "Working on Show: " & Cache.TvCache.Shows(f).Title.Value & " Episode: " & i & " of " & TotalEpisodes & ", Episode: " & Cache.TvCache.Shows(f).Episodes(g).Season.Value & "x" & Cache.TvCache.Shows(f).Episodes(g).Episode.Value & " - " & Cache.TvCache.Shows(f).Episodes(g).Title.Value
                            
                            If done > 0 Then
                                progress = (100 / showprocesscount) * done
                            Else
                                progress = 0
                            End If
                            tvbckrescrapewizard.ReportProgress(progress, progresstext)
                            Dim actorsource As String = Cache.TvCache.Shows(f).EpisodeActorSource.Value
                            If actorsource = "" Then actorsource = "tvdb"
                            Dim Episodedata As New Tvdb.Episode
                            Dim epfound As Boolean = False
                            For Each NewEpisode As Tvdb.Episode In tvseriesdata.Episodes
                                If NewEpisode.SeasonNumber.Value = Cache.TvCache.Shows(f).Episodes(g).Season.Value
                                    If NewEpisode.EpisodeNumber.Value = Cache.TvCache.Shows(f).Episodes(g).Episode.Value
                                        Episodedata = NewEpisode
                                        Episodedata.ThumbNail.Value = "http://www.thetvdb.com/banners/" & NewEpisode.ThumbNail.value
                                        epfound = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If Not epfound Then
                                Dim sortorder As String = Cache.TvCache.Shows(f).SortOrder.Value
                                If sortorder = "" Then sortorder = "default"
                                Dim tvdbid As String = Cache.TvCache.Shows(f).TvdbId.Value
                                Dim imdbid As String = Cache.TvCache.Shows(f).ImdbId.Value
                                Dim seasonno As String = Cache.TvCache.Shows(f).Episodes(g).Season.Value
                                Dim episodeno As String = Cache.TvCache.Shows(f).Episodes(g).Episode.Value
                                Episodedata = tvdbstuff.getepisodefromxml(tvdbid, sortorder, seasonno, episodeno, language, True)
                                If Episodedata.FailedLoad Then
                                    progresstext = "tvdb was unable to process the following show episode." & vbCrLf & Cache.TvCache.Shows(f).Title.Value & " - S" & Utilities.PadNumber(Cache.TvCache.Shows(f).Episodes(g).Season.Value, 2) & "E" & Utilities.PadNumber(Cache.TvCache.Shows(f).Episodes(g).Episode.Value, 2) & " " & Cache.TvCache.Shows(f).Episodes(g).Title.Value
                                    tvbckrescrapewizard.ReportProgress(progress, progresstext)
                                    Continue For
                                End If
                            End If
                            If tvBatchList.doEpisodeBody Or (tvBatchList.doEpisodeActors And Cache.TvCache.Shows(f).EpisodeActorSource.Value <> "") Or (tvBatchList.doEpisodeArt) Then
                                Dim listofnewepisodes As New List(Of TvEpisode)
                                listofnewepisodes.Clear()
                                listofnewepisodes = WorkingWithNfoFiles.ep_NfoLoad(Cache.TvCache.Shows(f).Episodes(g).NfoFilePath)   'Generic(Cache.TvCache.Shows(f).Episodes(g).NfoFilePath)
                                For h = listofnewepisodes.Count - 1 To 0 Step -1
                                    If listofnewepisodes(h).Season.Value = Cache.TvCache.Shows(f).Episodes(g).Season.Value And listofnewepisodes(h).Episode.Value = Cache.TvCache.Shows(f).Episodes(g).Episode.Value Then
                                        Dim newactors As New List(Of str_MovieActors)
                                        newactors.Clear()
                                        
                                        'its an episode
                                        Dim episodescreenurl As String = ""
                                        Try
                                            If tvBatchList.epAired Then listofnewepisodes(h).Aired.Value = Episodedata.FirstAired.Value
                                            If tvBatchList.epPlot Then listofnewepisodes(h).Plot.Value = Episodedata.Overview.Value
                                            If tvBatchList.epDirector Then listofnewepisodes(h).Director.Value = Utilities.Cleanbraced(Episodedata.Director.Value)
                                            If tvBatchList.epCredits Then listofnewepisodes(h).Credits.Value = Utilities.Cleanbraced(Episodedata.Writer.Value)
                                            If tvBatchList.epRating Then listofnewepisodes(h).Rating.Value = Episodedata.Rating.Value
                                            If tvBatchList.epTitle Then listofnewepisodes(h).Title.Value = Episodedata.EpisodeName.Value
                                            listofnewepisodes(h).UniqueId.Value = Episodedata.Id.Value
                                            listofnewepisodes(h).ShowId.Value = Episodedata.SeriesId.Value
                                            If tvBatchList.epActor Then
                                                If actorsource = "tvdb" Then
                                                    listofnewepisodes(h).ListActors.Clear()
                                                    Dim tempstr As String = Episodedata.GuestStars.Value 
                                                    tempstr = tempstr.TrimStart("|")
                                                    tempstr = tempstr.TrimEnd("|")
                                                    Dim Tmp() As String
                                                    Tmp = tempstr.Split("|")
                                                    For Each act In Tmp
                                                        Dim newactor As New str_MovieActors
                                                        newactor.actorname = act
                                                        listofnewepisodes(h).ListActors.Add(newactor)
                                                    Next
                                                ElseIf actorsource = "imdb" Then
                                                    Dim epid As String = GetEpImdbId(Cache.TvCache.Shows(f).ImdbId.Value, listofnewepisodes(h).Season.Value, listofnewepisodes(h).Episode.Value)
                                                    If epid.Contains("tt") Then
                                                        Dim scraperfunction As New Classimdb
                                                        'Dim ac As New actors
                                                        Dim actorlist As List(Of str_MovieActors) = scraperfunction.GetImdbActorsList(Preferences.imdbmirror, epid, Preferences.maxactors)
                                                        'actorlist = ac.EpisodeGetImdbActors(Cache.TvCache.Shows(f).ImdbId.Value, listofnewepisodes(h).Season.Value, listofnewepisodes(h).Episode.Value)
                                                        'If Preferences.actorseasy = True Then
                                                        'ac.savelocalactors(listofnewepisodes(h).VideoFilePath, actorlist, Cache.TvCache.Shows(f).NfoFilePath, True)
                                                        If actorlist.Count > 0 Then
                                                            listofnewepisodes(h).ListActors.Clear()
                                                            For Each act In actorlist
                                                                listofnewepisodes(h).ListActors.Add(act)
                                                            Next
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If tvBatchList.doEpisodeArt = True Then
                                                listofnewepisodes(h).Thumbnail.FileName = Episodedata.ThumbNail.Value
                                                progresstext = tv_EpisodeFanartGet(listofnewepisodes(h), tvBatchList.epScreenshot).Replace("!!! ","")
                                            End If
                                        Catch ex As Exception
#If SilentErrorScream Then
                                            Throw ex
#End If
                                            'MsgBox("hekp")
                                        End Try
                                        WorkingWithNfoFiles.ep_NfoSave(listofnewepisodes, listofnewepisodes(0).NfoFilePath)
                                        Exit For
                                    End If
                                Next
                            End If

                            If tvBatchList.doEpisodeMediaTags = True Then
                                Dim listofnewepisodes As New List(Of TvEpisode)
                                listofnewepisodes.Clear()
                                listofnewepisodes = WorkingWithNfoFiles.ep_NfoLoad(Cache.TvCache.Shows(f).Episodes(g).NfoFilePath)   'Generic(Cache.TvCache.Shows(f).Episodes(g).NfoFilePath)
                                For h = listofnewepisodes.Count - 1 To 0 Step -1
                                    listofnewepisodes(h).GetFileDetails()
                                    If listofnewepisodes(h).Details.StreamDetails.Video.DurationInSeconds.Value <> Nothing Then
                                        Try
                                            Dim tempstring As String
                                            tempstring = listofnewepisodes(h).Details.StreamDetails.Video.DurationInSeconds.Value
                                            If Preferences.intruntime Then
                                                listofnewepisodes(h).Runtime.Value = Math.Round(tempstring / 60).ToString
                                            Else
                                                listofnewepisodes(h).Runtime.Value = Math.Round(tempstring / 60).ToString & " min"
                                            End If

                                        Catch ex As Exception
#If SilentErrorScream Then
                                            Throw ex
#End If
                                        End Try
                                        WorkingWithNfoFiles.ep_NfoSave(listofnewepisodes, listofnewepisodes(0).NfoFilePath)
                                    End If
                                Next
                            End If
                            done += 1
                        Next
                    End If
                    done += 1
                    If singleshow Then Exit For
                End If
                
            Next
            singleshow = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub tvbckrescrapewizard_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles tvbckrescrapewizard.ProgressChanged
        Try
            If e.ProgressPercentage = 999999 Then
                ToolStripStatusLabel8.Text = e.UserState
            Else
                ToolStripStatusLabel8.Text = e.UserState
                ToolStripProgressBar7.Value = e.ProgressPercentage
                ToolStripProgressBar7.ProgressBar.Refresh()
                ToolStripProgressBar7.ProgressBar.PerformStep()
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tvbckrescrapewizard_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles tvbckrescrapewizard.RunWorkerCompleted
        Try
            ToolStripStatusLabel8.Visible = False
            ToolStripProgressBar7.Visible = False
            TvTreeview_AfterSelect_Do()
            GC.Collect()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TV_EpisodeScraper(ByVal ListOfShows As List(Of TvShow), ByVal manual As Boolean)
        Try
            Dim tempstring As String = ""
            Dim tempint As Integer
            Dim errorcounter As Integer = 0
            newEpisodeList.Clear()
            Dim newtvfolders As New List(Of String)
            Dim progress As Integer
            progress = 0
            Dim progresstext As String = String.Empty
            Preferences.tvScraperLog = ""
            Dim ShowsScanned As Integer = 0
            Dim FoldersScanned As Integer = 0
            Dim ShowsLocked As Integer = 0
            Dim dirpath As String = String.Empty
            Dim moviepattern As String = String.Empty
            Dim showtitle As String = ""
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog &= vbCrLf & "!!! Operation cancelled by user"
                Exit Sub
            End If
            If Preferences.tvshow_useXBMC_Scraper = True Then
                Preferences.tvScraperLog &= "---Using XBMC TVDB Scraper---" & vbCrLf
            Else
                Preferences.tvScraperLog &= "---Using MC TVDB Scraper---" & vbCrLf
            End If
            progresstext = String.Concat("Scanning TV Folders For New Episodes...")
            bckgroundscanepisodes.ReportProgress(progress, progresstext)

            Preferences.tvScraperLog &= "Starting Folder Scan" & vbCrLf & vbCrLf
            
            Dim TvFolder As String
            For Each TvShow As Media_Companion.TvShow In ListOfShows
                TvFolder = IO.Path.GetDirectoryName(TvShow.FolderPath)
                Dim Add As Boolean = True
                If TvShow.State <> Media_Companion.ShowState.Open Then
                    If manual = False Then
                        Add = False
                    End If
                End If

                If Add = True Then
                    ShowsScanned +=  1
                    progresstext = String.Concat("Stage 1 of 3 : Found " & newtvfolders.Count & " : Creating List of Folders From Roots : Searching - '" & TvFolder & "'")
                    bckgroundscanepisodes.ReportProgress(progress, progresstext)
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation cancelled by user"
                        Exit Sub
                    End If
                    tempstring = "" 'tvfolder
                    Dim hg As New IO.DirectoryInfo(TvFolder)
                    If hg.Exists Then
                        scraperLog = scraperLog & "Found " & hg.FullName.ToString & vbCrLf
                        newtvfolders.Add(TvFolder)
                        scraperLog = scraperLog & "Checking for subfolders" & vbCrLf
                        Dim ExtraFolder As List(Of String) = Utilities.EnumerateFolders(TvFolder, 3)
                        For Each Item As String In ExtraFolder
                            If Preferences.ExcludeFolders.Match(Item) Then Continue For
                            newtvfolders.Add(Item)
                            FoldersScanned += 1
                        Next
                    End If
                Else
                    ShowsLocked += 1
                End If
            Next
            
            scraperLog = scraperLog & vbCrLf
            Dim mediacounter As Integer = newEpisodeList.Count
            newtvfolders.Sort()
            For g = 0 To newtvfolders.Count - 1
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "!!! Operation cancelled by user"
                    Exit Sub
                End If
                progresstext = String.Concat("Stage 2 of 3 : Found " & newEpisodeList.Count & " : Searching for New Episodes in Folders " & g + 1 & " of " & newtvfolders.Count & " - '" & newtvfolders(g) & "'")
                bckgroundscanepisodes.ReportProgress(progress, progresstext)
                For Each f In Utilities.VideoExtensions
                    dirpath = newtvfolders(g)
                    Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                    tv_NewFind(dirpath, f)
                Next f
                tempint = newEpisodeList.Count - mediacounter
                mediacounter = newEpisodeList.Count
            Next g
            
            'report so far
            Preferences.tvScraperLog &= "!!! Scanned """ & ShowsScanned.ToString & """ Shows." & vbCrLf 
            If ShowsLocked > 0 Then Preferences.tvScraperLog &= "!!! Skipped """ & ShowsLocked.ToString & " Locked Shows." & vbCrLf
            Preferences.tvScraperLog &= "!!! Scanned """ & (ShowsScanned + FoldersScanned).ToString & """ folders (includes Show and subfolders)." & vbCrLf & vbCrLf

            If newEpisodeList.Count <= 0 Then
                Preferences.tvScraperLog &= "!!! No new episodes found, exiting scraper." & vbCrLf
                Exit Sub
            Else
                Preferences.tvScraperLog &= "!!! """ & newEpisodeList.Count.ToString & """ Episodes found." & vbCrLf & vbCrLf 
            End If
            
            Dim S As String = ""
            For Each newepisode In newEpisodeList
                S = ""
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                For Each Shows In Cache.TvCache.Shows
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    If newepisode.FolderPath.Contains(Shows.FolderPath) Then
                        If Shows.ImdbId.Value Is Nothing OrElse String.IsNullOrEmpty(Shows.Premiered.Value) Then
                            Shows.Load()
                        End If
                        newepisode.ShowLang.Value = Shows.Language.Value
                        newepisode.sortorder.Value = Shows.SortOrder.Value
                        newepisode.Showtvdbid.Value = Shows.TvdbId.Value
                        newepisode.Showimdbid.Value = Shows.ImdbId.Value
                        showtitle = Shows.Title.Value
                        newepisode.ShowYear.Value = Shows.Year.Value
                        If String.IsNullOrEmpty(newepisode.ShowYear.Value) Then
                            If Not String.IsNullOrEmpty(Shows.Premiered.Value) Then
                                Dim yr As String = Shows.Premiered.Value.Substring(0,4)
                                If yr.Length = 4 Then newepisode.ShowYear.Value = yr
                            End If
                        End If
                        newepisode.actorsource.Value = Shows.EpisodeActorSource.Value
                        Exit For
                    End If
                Next
                Dim episode As New TvEpisode
                For Each Regexs In tv_RegexScraper
                    S = newepisode.VideoFilePath '.ToLower
                    S = S.Replace(showtitle, "")
                    If Not String.IsNullOrEmpty(newepisode.ShowYear.Value) Then S = S.Replace(newepisode.ShowYear.Value, "")
                    S = S.Replace("x265", "")
                    S = S.Replace("x264", "")
                    S = S.Replace("720p", "")
                    S = S.Replace("720i", "")
                    S = S.Replace("1080p", "")
                    S = S.Replace("1080i", "")
                    S = S.Replace("X265", "")
                    S = S.Replace("X264", "")
                    S = S.Replace("720P", "")
                    S = S.Replace("720I", "")
                    S = S.Replace("1080P", "")
                    S = S.Replace("1080I", "")
                    Dim M As Match
                    M = Regex.Match(S, Regexs)
                    If M.Success = True Then
                        Try
                            newepisode.Season.Value = M.Groups(1).Value.ToString
                            newepisode.Episode.Value = M.Groups(2).Value.ToString

                            Try
                                Dim matchvalue As String = M.Value
                                newepisode.Thumbnail.FileName = S.Substring(S.LastIndexOf(matchvalue)+matchvalue.Length, S.Length - (S.LastIndexOf(matchvalue) + (matchvalue.Length)))
                                'newepisode.Thumbnail.FileName = S.Substring(M.Groups(2).Index + M.Groups(2).Value.Length, S.Length - (M.Groups(2).Index + M.Groups(2).Value.Length))
                            Catch ex As Exception
        #If SilentErrorScream Then
                                    Throw ex
        #End If
                            End Try
                            Exit For
                        Catch
                            newepisode.Season.Value = "-1"
                            newepisode.Episode.Value = "-1"
                        End Try
                    End If
                Next
                If newepisode.Season.Value = Nothing Then newepisode.Season.Value = "-1"
                If newepisode.Episode.Value = Nothing Then newepisode.Episode.Value = "-1"
            Next
            Dim savepath As String = ""
            Dim scrapedok As Boolean
            Dim epscount As Integer = 0
            For Each eps In newEpisodeList
                epscount += 1
                Preferences.tvScraperLog &= "!!! With File : " & eps.VideoFilePath & vbCrLf
                Preferences.tvScraperLog &= "!!! Detected  : Season : " & eps.Season.Value & " Episode : " & eps.Episode.Value & vbCrLf
                If eps.Season.Value = "-1" And eps.Episode.Value = "-1" Then
                    Preferences.tvScraperLog &= "!!! WARNING: Can't extract Season and Episode details from this filename, file not added!" & vbCrLf
                    Preferences.tvScraperLog &= "!!!" & vbCrLf
                    Continue For    'if we can't get season or episode then skip to next episode
                End If
                
                Dim episodearray As New List(Of TvEpisode)
                episodearray.Clear()
                episodearray.Add(eps)
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                Dim WhichScraper As String = ""
                If Preferences.tvshow_useXBMC_Scraper = True Then
                    WhichScraper = "XBMC TVDB"
                Else
                    WhichScraper = "MC TVDB"
                End If
                progresstext = String.Concat("ESC to Cancel : Stage 3 of 3 : Scraping New Episodes : Using " & WhichScraper & "Scraper : Scraping " & epscount & " of " & newEpisodeList.Count & " - '" & IO.Path.GetFileName(eps.VideoFilePath) & "'")
                bckgroundscanepisodes.ReportProgress(progress, progresstext)
                Dim removal As String = ""
                If eps.Season.Value = "-1" Or eps.Episode.Value = "-1" Then
                    eps.Title.Value = Utilities.GetFileName(eps.VideoFilePath)
                    eps.Rating.Value = "0"
                    eps.PlayCount.Value = "0"
                    eps.Genre.Value = "Unknown Episode Season and/or Episode Number"
                    eps.GetFileDetails()
                    episodearray.Add(eps)
                    savepath = episodearray(0).NfoFilePath
                Else
                    Dim temppath As String = eps.NfoFilePath
                    'check for multiepisode files
                    Dim M2 As Match
                    Dim epcount As Integer = 0
                    Dim allepisodes(100) As Integer
                    S = Regex.Replace(eps.Thumbnail.FileName, "\(.+\)\s", "")   'Remove anything from filename in brackets like resolution ie: (1920x1080) that may give false episode number
                    eps.Thumbnail.FileName = ""
                    Do
                        '<tvregex>[Ss]([\d]{1,2}).?[Ee]([\d]{3})</tvregex>
                        M2 = Regex.Match(S, "(([EeXx])([\d]{1,4}))")
                        If M2.Success = True Then
                            Dim skip As Boolean = False
                            For Each epso In episodearray
                                If epso.Episode.Value = M2.Groups(3).Value Then skip = True
                            Next
                            If skip = False Then
                                Dim multieps As New TvEpisode
                                multieps.Season.Value = eps.Season.Value
                                multieps.Episode.Value = M2.Groups(3).Value
                                multieps.VideoFilePath = eps.VideoFilePath
                                multieps.MediaExtension = eps.MediaExtension
                                episodearray.Add(multieps)
                                allepisodes(epcount) = Convert.ToDecimal(M2.Groups(3).Value)
                            End If
                            Try
                                S = S.Substring(M2.Groups(3).Index + M2.Groups(3).Value.Length, S.Length - (M2.Groups(3).Index + M2.Groups(3).Value.Length))
                            Catch ex As Exception
        #If SilentErrorScream Then
                                    Throw ex
        #End If
                            End Try
                        End If
                        If bckgroundscanepisodes.CancellationPending Then
                            Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                            Exit Sub
                        End If
                    Loop Until M2.Success = False
                    
                    Dim language As String = eps.ShowLang.Value
                    Dim sortorder As String = eps.sortorder.Value
                    Dim tvdbid As String = eps.Showtvdbid.Value
                    Dim imdbid As String = eps.Showimdbid.Value
                    Dim actorsource As String = eps.actorsource.Value

                    savepath = episodearray(0).NfoFilePath
                    
                    If episodearray.Count > 1 Then
                        For I = 1 To episodearray.Count - 1
                            episodearray(I).MakeSecondaryTo(episodearray(0))
                        Next
                        Preferences.tvScraperLog &= "Multipart episode found: " & vbCrLf
                        Preferences.tvScraperLog &= "Season: " & episodearray(0).Season.Value & " Episodes, "
                        For Each ep In episodearray
                            Preferences.tvScraperLog &= ep.Episode.Value & ", "
                        Next
                        Preferences.tvScraperLog &= vbCrLf
                    End If
                    
                    For Each singleepisode In episodearray
                        If bckgroundscanepisodes.CancellationPending Then
                            Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                            Exit Sub
                        End If
                        If singleepisode.Season.Value.Length > 0 Or singleepisode.Season.Value.IndexOf("0") = 0 Then
                            Do Until singleepisode.Season.Value.IndexOf("0") <> 0 Or singleepisode.Season.Value.Length = 1
                                singleepisode.Season.Value = singleepisode.Season.Value.Substring(1, singleepisode.Season.Value.Length - 1)
                            Loop
                            If singleepisode.Episode.Value = "00" Then
                                singleepisode.Episode.Value = "0"
                            End If
                            If singleepisode.Episode.Value <> "0" Then
                                Do Until singleepisode.Episode.Value.IndexOf("0") <> 0
                                    singleepisode.Episode.Value = singleepisode.Episode.Value.Substring(1, singleepisode.Episode.Value.Length - 1)
                                Loop
                            End If
                        End If
                        
                        Dim episodescraper As New TVDBScraper
                        If sortorder = "" Then sortorder = "default"
                        Dim tempsortorder As String = sortorder
                        If language = "" Then language = "en"
                        If actorsource = "" Then actorsource = "tvdb"
                        Preferences.tvScraperLog &= "Using Settings: TVdbID: " & tvdbid & " SortOrder: " & sortorder & " Language: " & language & " Actor Source: " & actorsource & vbCrLf
                        If tvdbid <> "" Then
                            progresstext &= " - Scraping..."
                            bckgroundscanepisodes.ReportProgress(progress, progresstext)
                            Dim episodeurl As String = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & singleepisode.Season.Value & "/" & singleepisode.Episode.Value & "/" & language & ".xml"
                            If Not Utilities.UrlIsValid(episodeurl) Then
                                If sortorder.ToLower = "dvd" Then
                                    tempsortorder = "default"
                                    Preferences.tvScraperLog &= "!!! WARNING: This episode could not be found on TVDB using DVD sort order" & vbCrLf
                                    Preferences.tvScraperLog &= "!!! Attempting to find using default sort order" & vbCrLf
                                    episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.Season.Value & "/" & singleepisode.Episode.Value & "/" & language & ".xml"
                                    Preferences.tvScraperLog &= "Now Trying Episode URL: " & episodeurl & vbCrLf
                                End If
                            End If
                            
                            If Utilities.UrlIsValid(episodeurl) Then
                                If Preferences.tvshow_useXBMC_Scraper = True Then
                                    Dim FinalResult As String = ""
                                    episodearray = XBMCScrape_TVShow_EpisodeDetails(tvdbid, tempsortorder, episodearray, language)
                                    episodearray(0).NfoFilePath = savepath
                                    If episodearray.Count >= 1 Then
                                        For x As Integer = 0 To episodearray.Count - 1
                                            Preferences.tvScraperLog &= "Scraping body of episode: " & episodearray(x).Episode.Value & " - OK" & vbCrLf
                                        Next
                                        scrapedok = True
                                    Else
                                        Preferences.tvScraperLog &= "!!! WARNING: Could not locate this episode on TVDB, or TVDB may be unavailable" & vbCrLf
                                        scrapedok = False
                                    End If
                                    Exit For
                                End If
                                Dim tempepisode As String = ep_Get(tvdbid, tempsortorder, singleepisode.Season.Value, singleepisode.Episode.Value, language)
                                scrapedok = True
                                If tempepisode = Nothing Or tempepisode = "Error" Then
                                    scrapedok = False
                                    Preferences.tvScraperLog &= "!!! WARNING: This episode could not be found on TVDB" & vbCrLf
                                ElseIf tempepisode.Contains("Could not connect") Then     'If TVDB unavailable, advise user to try again later
                                    scrapedok = False
                                    Preferences.tvScraperLog &= "!!! Issue at TheTVDb, Episode could not be retrieve. Try again later" & vbCrLf
                                End If
                                If scrapedok = True Then
                                    progresstext &= "OK."
                                    bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                    Dim scrapedepisode As New XmlDocument
                                    Preferences.tvScraperLog &= "Scraping body of episode: " & singleepisode.Episode.Value & vbCrLf
                                    scrapedepisode.LoadXml(tempepisode)
                                    For Each thisresult As XmlNode In scrapedepisode("episodedetails")
                                        Select Case thisresult.Name
                                            Case "title"
                                                singleepisode.Title.Value = thisresult.InnerText
                                            Case "premiered"
                                                singleepisode.Aired.Value = thisresult.InnerText
                                            Case "plot"
                                                singleepisode.Plot.Value = thisresult.InnerText
                                            Case "director"
                                                Dim newstring As String
                                                newstring = thisresult.InnerText
                                                newstring = newstring.TrimEnd("|")
                                                newstring = newstring.TrimStart("|")
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.Director.Value = newstring
                                            Case "credits"
                                                Dim newstring As String
                                                newstring = thisresult.InnerText
                                                newstring = newstring.TrimEnd("|")
                                                newstring = newstring.TrimStart("|")
                                                newstring = newstring.Replace("|", " / ")
                                                singleepisode.Credits.Value = newstring
                                            Case "rating"
                                                singleepisode.Rating.Value = thisresult.InnerText
                                            Case "uniqueid"
                                                singleepisode.UniqueId.Value = thisresult.InnerText
                                            Case "showid"
                                                singleepisode.ShowId.Value = thisresult.InnerText
                                            Case "imdbid"
                                                singleepisode.ImdbId.Value = thisresult.InnerText
                                            Case "displayseason"
                                                singleepisode.DisplaySeason.Value = thisresult.InnerXml
                                            Case "displayepisode"
                                                singleepisode.DisplayEpisode.Value = thisresult.InnerXml 
                                            Case "thumb"
                                                singleepisode.Thumbnail.FileName = thisresult.InnerText
                                            Case "actor"
                                                For Each actorl As XmlNode In thisresult.ChildNodes
                                                    Select Case actorl.Name
                                                        Case "name"
                                                            Dim newactor As New str_MovieActors(SetDefaults)
                                                            If actorl.InnerText <> "" Then
                                                                newactor.actorname = actorl.InnerText
                                                                singleepisode.ListActors.Add(newactor)
                                                            End If
                                                    End Select
                                                Next
                                        End Select
                                    Next
                                    singleepisode.PlayCount.Value = "0"
                                    singleepisode.ShowId.Value = tvdbid

                                    'check file name for Episode source
                                    Dim searchtitle As String = singleepisode.NfoFilePath
                                    If searchtitle <> "" Then
                                        For i = 0 To Preferences.releaseformat.Length - 1
                                            If searchtitle.ToLower.Contains(Preferences.releaseformat(i).ToLower) Then
                                                singleepisode.Source.Value = Preferences.releaseformat(i)
                                                Exit For
                                            End If
                                        Next
                                    End If

                                    progresstext &= " : Scraped Title - '" & singleepisode.Title.Value & "'"
                                    bckgroundscanepisodes.ReportProgress(progress, progresstext)

                                    If actorsource = "imdb" And (imdbid <> "" OrElse singleepisode.ImdbId.Value <> "") Then
                                        Preferences.tvScraperLog &= "Scraping actors from IMDB" & vbCrLf
                                        progresstext &= " : Actors..."
                                        bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                        Dim epid As String = ""
                                        If singleepisode.ImdbId.Value <> "" Then
                                            epid = singleepisode.ImdbId.Value 
                                        Else
                                            'url = "http://www.imdb.com/title/" & imdbid & "/episodes?season=" & singleepisode.Season.Value
                                            epid = GetEpImdbId(imdbid, singleepisode.Season.Value, singleepisode.Episode.Value)
                                        End If
                                        
                                        If epid.contains("tt") Then
                                            Dim scraperfunction As New Classimdb
                                            Dim tempactorlist As List(Of str_MovieActors) = scraperfunction.GetImdbActorsList(Preferences.imdbmirror, epid, Preferences.maxactors)
                                                If bckgroundscanepisodes.CancellationPending Then
                                                    Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                                                    Exit Sub
                                                End If
                                            If tempactorlist.Count > 0 Then
                                                Preferences.tvScraperLog &= "Actors scraped from IMDB OK" & vbCrLf
                                                progresstext &= "OK."
                                                bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                                While tempactorlist.Count > Preferences.maxactors
                                                    tempactorlist.RemoveAt(tempactorlist.Count - 1)
                                                End While
                                                singleepisode.ListActors.Clear()
                                                For Each actor In tempactorlist
                                                    singleepisode.ListActors.Add(actor)
                                                Next
                                                tempactorlist.Clear()
                                            Else
                                                Preferences.tvScraperLog &= "!!! WARNING: Actors not scraped from IMDB, reverting to TVDB actorlist" & vbCrLf
                                            End If
                                            If bckgroundscanepisodes.CancellationPending Then
                                                Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                                                Exit Sub
                                            End If
                                        Else
                                            tvScraperLog = tvScraperLog & "Unable To Get Actors From IMDB" & vbCrLf
                                        End If
                                    End If
                                    If imdbid = "" Then
                                        Preferences.tvScraperLog &= "Failed Scraping Actors from IMDB!!!  No IMDB Id for Show:  " & showtitle & vbCrLf
                                    End If
                                    If Preferences.enablehdtags = True Then
                                        progresstext &= " : HD Tags..."
                                        bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                        Dim fileStreamDetails As FullFileDetails = Preferences.Get_HdTags(Utilities.GetFileName(singleepisode.VideoFilePath))
                                        If Not IsNothing(fileStreamDetails) Then
                                            singleepisode.Details.StreamDetails.Video = fileStreamDetails.filedetails_video
                                            For Each audioStream In fileStreamDetails.filedetails_audio
                                                singleepisode.Details.StreamDetails.Audio.Add(audioStream)
                                            Next
                                            If Not singleepisode.Details.StreamDetails.Video.DurationInSeconds.Value Is Nothing Then
                                                tempstring = singleepisode.Details.StreamDetails.Video.DurationInSeconds.Value
                                                If Preferences.intruntime Then
                                                    singleepisode.Runtime.Value = Math.Round(tempstring / 60).ToString
                                                Else
                                                    singleepisode.Runtime.Value = Math.Round(tempstring / 60).ToString & " min"
                                                End If
                                                progresstext &= "OK."
                                                bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                Preferences.tvScraperLog &= "!!! WARNING: Could not locate this episode on TVDB, or TVDB may be unavailable" & vbCrLf
                                scrapedok = False
                            End If
                        Else
                            Preferences.tvScraperLog &= "!!! WARNING: No TVDB ID is available for this show, please scrape the show using the ""TV Show Selector"" TAB" & vbCrLf
                            scrapedok = False
                        End If
                    Next
                End If
                
                If savepath <> "" And scrapedok = True Then
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    Dim newnamepath As String = ""

                    newnamepath = ep_add(episodearray, savepath, showtitle)
                    For Each ep In episodearray
                        ep.NfoFilePath = newnamepath
                    Next
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    
                    For Each Shows In Cache.TvCache.Shows
                        If episodearray(0).NfoFilePath.IndexOf(Shows.NfoFilePath.Replace("\tvshow.nfo", "")) <> -1 Then
                            Dim epseason As String = episodearray(0).Season.Value
                            Dim Seasonxx As String = Shows.FolderPath + "season" + (If(epseason.ToInt < 10, "0" + epseason, epseason)) + (If(Preferences.FrodoEnabled, "-poster.jpg", ".tbn"))
                            If epseason = "0" Then Seasonxx = Shows.FolderPath & "season-specials" & (If(Preferences.FrodoEnabled, "-poster.jpg", ".tbn"))
                            If Not IO.File.Exists(Seasonxx) Then
                                TvGetArtwork(Shows, False, False, True, False)
                            End If
                            If Preferences.seasonfolderjpg AndAlso Shows.FolderPath <> episodearray(0).FolderPath AndAlso (Not File.Exists(episodearray(0).FolderPath & "folder.jpg")) Then
                                If File.Exists(Seasonxx) Then Utilities.SafeCopyFile(Seasonxx, (episodearray(0).FolderPath & "folder.jpg"))
                            End If
                            
                            For Each ept In episodearray
                                Dim list = Shows.MissingEpisodes
                                For j = list.Count - 1 To 0 Step -1
                                    If list(j).Title.Value = ept.Title.Value Then
                                        'not sure this has a point.  missingepisodes is a linq list
                                        list.RemoveAt(j)
                                        Exit For
                                    End If
                                Next
                            Next
                            For Each ep In episodearray
                                Dim newwp As New TvEpisode
                                newwp = ep                      'added this kline becuase plot + others were not being dispolay after a new ep was found
                                newwp.NfoFilePath = newnamepath 'left these as they were....
                                newwp.PlayCount.Value = "0"         '
                                newwp.ShowObj = Shows               '
                                bckgroundscanepisodes.ReportProgress(1, newwp)
                            Next
                            Exit For
                        End If
                    Next
                End If
                Preferences.tvScraperLog &= "!!!" & vbCrLf
            Next
            'newEpisodeList 
            tv_EpisodesMissingUpdate(newEpisodeList)
            bckgroundscanepisodes.ReportProgress(0, progresstext)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    
    Sub tv_Rescrape_Episode(ByRef WorkingTvShow, ByRef WorkingEpisode)
        Dim tempint As Integer = 0
        Dim tempstring As String = ""
        If WorkingEpisode.IsMissing Then
            MsgBox("This is a Missing Episode, and can not be rescraped!")
            Exit Sub
        End If
        If Utilities.GetTvEpExtension(WorkingEpisode.NfoFilePath) = "error" Then
            tempint = MessageBox.Show("Video file for this episode does not exist." & vbCrLf & "Please Delete this episode's nfo, and refresh the Show for the selected episode.", "Warning", MessageBoxButtons.OK , MessageBoxIcon.Warning)
            Exit Sub
        End If
        tempint = MessageBox.Show("Rescraping the Episode will Overwrite all the current details" & vbCrLf & "Do you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If tempint = DialogResult.No Then
            Exit Sub
        End If
        Dim messbox As frmMessageBox = New frmMessageBox("The Selected Episode is being Rescraped", "", "Please Wait")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        messbox.Show()
        messbox.Refresh()
        Application.DoEvents()
        Dim newepisode As New TvEpisode
        Dim sortorder As String = WorkingTvShow.SortOrder.Value
        Dim language As String = WorkingTvShow.Language.Value
        Dim actorsource As String = WorkingTvShow.EpisodeActorSource.Value
        Dim tvdbid As String = WorkingTvShow.TvdbId.Value
        Dim imdbid As String = WorkingTvShow.ImdbId.Value
        Dim seasonno As String = WorkingEpisode.Season.Value
        Dim episodeno As String = WorkingEpisode.Episode.Value

        newepisode.NfoFilePath = WorkingEpisode.NfoFilePath
        newepisode.Season.Value = WorkingEpisode.Season.Value
        newepisode.Episode.Value = WorkingEpisode.Episode.Value
        newepisode.ShowId.Value = WorkingTvShow.TvdbId.Value

        Dim episodescraper As New TVDBScraper
        If sortorder = "" Then sortorder = "default"
        If language = "" Then language = "en"
        If actorsource = "" Then actorsource = "tvdb"
        If tvdbid.IndexOf("tt").Equals(0) Then tv_IMDbID_detected = True
        Dim tempepisode As String = episodescraper.getepisode(tvdbid, sortorder, seasonno, episodeno, language, True)

        If tempepisode.Contains("ERROR") Then
            Dim chunkSize As Integer = 40
            Dim chunkSize2 As Integer = 1
            Dim loops As Integer = Math.Round(tempepisode.Length / chunkSize)
            Dim finalString As String = ""

            For i = 0 To loops
                If i * chunkSize + chunkSize > tempepisode.Length Then
                    chunkSize2 = tempepisode.Length - i * chunkSize
                Else
                    chunkSize2 = chunkSize
                End If
                finalString += tempepisode.Substring(i * chunkSize, chunkSize2) & vbCrLf
            Next
            MsgBox("TVDB reported the following error" & vbCrLf & finalString, MsgBoxStyle.OkOnly, "ERROR!")
            messbox.Close()
            Exit Sub
        End If
        Dim scrapedepisode As New XmlDocument
        Try
            scrapedepisode.LoadXml(tempepisode)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In scrapedepisode("episodedetails")
                Select Case thisresult.Name
                    Case "title"
                        newepisode.Title.Value = thisresult.InnerText
                    Case "premiered"
                        newepisode.Aired.Value = thisresult.InnerText
                    Case "plot"
                        newepisode.Plot.Value = thisresult.InnerText
                    Case "director"
                        newepisode.Director.Value = thisresult.InnerText
                        newepisode.Director.Value = newepisode.Director.Value.TrimStart("|")
                        newepisode.Director.Value = newepisode.Director.Value.TrimEnd("|")
                        newepisode.Director.Value = newepisode.Director.Value.Replace("|", " / ")
                    Case "credits"
                        newepisode.Credits.Value = thisresult.InnerText
                        newepisode.Credits.Value = newepisode.Credits.Value.TrimStart("|")
                        newepisode.Credits.Value = newepisode.Credits.Value.TrimEnd("|")
                        newepisode.Credits.Value = newepisode.Credits.Value.Replace("|", " / ")
                    Case "rating"
                        newepisode.Rating.Value = thisresult.InnerText
                    Case "thumb"
                        newepisode.Thumbnail.FileName = thisresult.InnerText
                    Case "genre"
                        newepisode.Genre.Value = thisresult.InnerText
                    Case "imdbid"
                        newepisode.ImdbId.Value = thisresult.InnerText
                    Case "displayseason"
                        newepisode.DisplaySeason.Value = thisresult.InnerXml
                    Case "displayepisode"
                        newepisode.DisplayEpisode.Value = thisresult.InnerXml
                    Case "actor"
                        Dim actors As XmlNode = Nothing
                        For Each actorl In thisresult.ChildNodes
                            Select Case actorl.name
                                Case "name"
                                    Dim newactor As New str_MovieActors(SetDefaults)
                                    newactor.actorname = actorl.innertext
                                    newepisode.ListActors.Add(newactor)
                            End Select
                        Next
                End Select
            Next
            newepisode.PlayCount.Value = "0"
        Catch ex As Exception
        End Try

        If actorsource = "imdb" Then
        '    If newepisode.ListActors.Count > 0 Then
        '        WorkingTvShow.ListActors.Clear() 'Possibly doesn't need to use WorkingTvShow.clearActor() as the NFO is created "manually"
        '        For Each act In newepisode.ListActors
        '            WorkingTvShow.ListActors.Add(act)
        '        Next
        '    End If
            If imdbid <> "" OrElse newepisode.ImdbId.Value <> "" Then
                tvScraperLog = tvScraperLog & "Scraping actors from IMDB" & vbCrLf
                Dim epid As String = ""
                If newepisode.ImdbId.Value <> Nothing AndAlso newepisode.ImdbId.Value.Contains("tt") Then
                    epid = newepisode.ImdbId.Value
                Else
                    epid = GetEpImdbId(imdbid, newepisode.Season.Value, newepisode.Episode.Value)
                End If
                If bckgroundscanepisodes.CancellationPending Then
                    tvScraperLog = tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                If epid.Contains("tt") Then
                    Dim scraperfunction As New Classimdb
                    'Dim ac As New actors
                    Dim actorlist As List(Of str_MovieActors) = scraperfunction.GetImdbActorsList(Preferences.imdbmirror, epid, Preferences.maxactors)
                    'actorlist = ac.EpisodeGetImdbActors(Cache.TvCache.Shows(f).ImdbId.Value, listofnewepisodes(h).Season.Value, listofnewepisodes(h).Episode.Value)
                    'If Preferences.actorseasy = True Then
                    'ac.savelocalactors(listofnewepisodes(h).VideoFilePath, actorlist, Cache.TvCache.Shows(f).NfoFilePath, True)
                    If actorlist.Count > 0 Then
                        newepisode.ListActors.Clear()
                        For Each act In actorlist
                            newepisode.ListActors.Add(act)
                        Next
                    End If
                Else

                End If
            'Dim url As String
            'url = "http://www.imdb.com/title/" & imdbid & "/episodes"
            'Dim tvfblinecount As Integer = 0
            'Dim tvdbwebsource(10000)
            'tvfblinecount = 0
            
'            Try
'                Dim wrGETURL As WebRequest
'                wrGETURL = WebRequest.Create(url)
'                Dim myProxy As New WebProxy("myproxy", 80)
'                myProxy.BypassProxyOnLocal = True
'                Dim objStream As Stream
'                objStream = wrGETURL.GetResponse.GetResponseStream()
'                Dim objReader As New StreamReader(objStream)
'                Dim tvdbsLine As String = ""
'                tvfblinecount = 0

'                Do While Not tvdbsLine Is Nothing
'                    tvfblinecount += 1
'                    tvdbsLine = objReader.ReadLine
'                    If Not tvdbsLine Is Nothing Then
'                        tvdbwebsource(tvfblinecount) = tvdbsLine
'                    End If
'                    If bckgroundscanepisodes.CancellationPending Then
'                        tvScraperLog = tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
'                        Exit Sub
'                    End If
'                Loop
'                objReader.Close()
'                tvfblinecount -= 1
'            Catch ex As WebException
'                tvdbwebsource(0) = "404"
'            End Try
'            If tvfblinecount <> 0 Then
'                Dim tvtempstring As String
'                tvtempstring = "Season " & seasonno & ", Episode " & episodeno & ":"
'                For g = 1 To tvfblinecount
'                    If tvdbwebsource(g).indexof(tvtempstring) <> -1 Then
'                        Dim tvtempint As Integer = 0
'                        tvtempint = tvdbwebsource(g).indexof("<a href=""/title/")
'                        If tvtempint <> -1 Then
'                            tvtempstring = tvdbwebsource(g).substring(tvtempint + 16, 9)
'                            '            Dim scraperfunction As New imdb.Classimdbscraper ' add to comment this one because of changes i made to the Class "Scraper" (ClassimdbScraper)
'                            Dim scraperfunction As New Classimdb
'                            Dim actorlist As String = ""
'                            actorlist = scraperfunction.getimdbactors(Preferences.imdbmirror, tvtempstring, Preferences.maxactors)
'                            Dim tempactorlist As New List(Of str_MovieActors)
'                            Dim thumbstring As New XmlDocument
'                            Dim thisresult As XmlNode = Nothing
'                            Try
'                                thumbstring.LoadXml(actorlist)
'                                thisresult = Nothing
'                                Dim actorcount As Integer = 0
'                                For Each thisresult In thumbstring("actorlist")
'                                    If bckgroundscanepisodes.CancellationPending Then
'                                        tvScraperLog = tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
'                                        Exit Sub
'                                    End If
'                                    Select Case thisresult.Name
'                                        Case "actor"
'                                            If actorcount > Preferences.maxactors Then
'                                                Exit For
'                                            End If
'                                            actorcount += 1

'                                            Dim newactor As New str_MovieActors(SetDefaults)
'                                            Dim detail As XmlNode = Nothing
'                                            For Each detail In thisresult.ChildNodes
'                                                Select Case detail.Name
'                                                    Case "name"
'                                                        newactor.actorname = detail.InnerText
'                                                    Case "role"
'                                                        newactor.actorrole = detail.InnerText
'                                                    Case "thumb"
'                                                        newactor.actorthumb = GetActorThumb(detail.InnerText)
'                                                    Case "actorid"
'                                                        If newactor.actorthumb <> Nothing Then
'                                                            If detail.InnerText <> "" And Preferences.actorseasy = True Then
'                                                                Dim workingpath As String = WorkingEpisode.NfoFilePath.Replace(IO.Path.GetFileName(WorkingEpisode.NfoFilePath), "")
'                                                                workingpath = workingpath & ".actors\"
'                                                                Dim hg As New IO.DirectoryInfo(workingpath)
'                                                                Dim destsorted As Boolean = False
'                                                                If Not hg.Exists Then
'                                                                    Try
'                                                                        IO.Directory.CreateDirectory(workingpath)
'                                                                        destsorted = True
'                                                                    Catch ex As Exception
'#If SilentErrorScream Then
'                                                                        Throw ex
'#End If
'                                                                    End Try
'                                                                Else
'                                                                    destsorted = True
'                                                                End If
'                                                                If destsorted = True Then
'                                                                    Dim filename As String = newactor.actorname.Replace(" ", "_")
'                                                                    filename = filename & ".tbn"
'                                                                    Dim tvshowactorpath As String = WorkingTvShow.NfoFilePath
'                                                                    tvshowactorpath = tvshowactorpath.Replace(IO.Path.GetFileName(tvshowactorpath), "")
'                                                                    tvshowactorpath = IO.Path.Combine(tvshowactorpath, ".actors\")
'                                                                    tvshowactorpath = IO.Path.Combine(tvshowactorpath, filename)

'                                                                    filename = IO.Path.Combine(workingpath, filename)
'                                                                    If Preferences.copytvactorthumbs = True Then
'                                                                        If IO.File.Exists(tvshowactorpath) Then
'                                                                            Try
'                                                                                IO.File.Copy(tvshowactorpath, filename, True)
'                                                                            Catch ex As Exception
'#If SilentErrorScream Then
'                                                                                Throw ex
'#End If
'                                                                            End Try
'                                                                        End If
'                                                                    End If
'                                                                    If Not IO.File.Exists(filename) Then
'                                                                        Utilities.DownloadFile(newactor.actorthumb, filename)
'                                                                    End If
'                                                                End If
'                                                            End If
'                                                            If Preferences.actorsave = True And detail.InnerText <> "" And Preferences.actorseasy = False Then
'                                                                Dim workingpath As String = ""
'                                                                Dim networkpath As String = Preferences.actorsavepath
'                                                                Try
'                                                                    tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
'                                                                    Dim hg As New IO.DirectoryInfo(tempstring)
'                                                                    If Not hg.Exists Then
'                                                                        IO.Directory.CreateDirectory(tempstring)
'                                                                    End If
'                                                                    workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
'                                                                    If Not IO.File.Exists(workingpath) Then
'                                                                        Utilities.DownloadFile(newactor.actorthumb, workingpath)
'                                                                    End If
'                                                                    newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
'                                                                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
'                                                                        newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
'                                                                    Else
'                                                                        newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
'                                                                    End If
'                                                                Catch ex As Exception
'#If SilentErrorScream Then
'                                                                    Throw ex
'#End If
'                                                                End Try
'                                                            End If
'                                                        End If
'                                                End Select
'                                            Next
'                                            tempactorlist.Add(newactor)
'                                    End Select
'                                Next
'                            Catch ex As Exception
'                                tvScraperLog = tvScraperLog & "Error scraping episode actors from IMDB, " & vbCrLf & ex.Message.ToString & vbCrLf & vbCrLf
'                            End Try

'                            If tempactorlist.Count > 0 Then
'                                While tempactorlist.Count > Preferences.maxactors
'                                    tempactorlist.RemoveAt(tempactorlist.Count - 1)
'                                End While
'                                newepisode.ListActors.Clear()
'                                For Each actor In tempactorlist
'                                    newepisode.ListActors.Add(actor)
'                                Next
'                                tempactorlist.Clear()
'                            End If
'                            Exit For
'                        End If
'                    End If
'                Next
            End If
        End If
            
        If Preferences.enablehdtags = True Then
            'newepisode.Details.  = Preferences.Get_HdTags(Utilities.GetFileName(WorkingEpisode.VideoFilePath)).filedetails_video
            Dim fileStreamDetails As FullFileDetails = Preferences.Get_HdTags(Utilities.GetFileName(WorkingEpisode.VideoFilePath))
            newepisode.Details.StreamDetails.Video = fileStreamDetails.filedetails_video
            For Each audioStream In fileStreamDetails.filedetails_audio
                newepisode.Details.StreamDetails.Audio.Add(audioStream)
            Next
            If newepisode.Details.StreamDetails.Video.DurationInSeconds.Value <> Nothing Then
                Try
                    tempstring = newepisode.Details.StreamDetails.Video.DurationInSeconds.Value
                    If Preferences.intruntime Then
                        newepisode.Runtime.Value = Math.Round(tempstring/60).ToString
                    Else
                        newepisode.Runtime.Value = Math.Round(tempstring/60).ToString & " min"
                    End If
                        
                Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                End Try
            End If
        End If
        Dim eps As New List(Of Media_Companion.TvEpisode)
        eps.Add(newepisode)
        Dim multiepisode As Boolean = TestForMultiepisode(newepisode.NfoFilePath)
        If multiepisode = False Then
            'Call nfoFunction.saveepisodenfo(eps, newepisode.NfoFilePath)
            WorkingWithNfoFiles.ep_NfoSave(eps, newepisode.NfoFilePath)
        Else
            Dim episodelist As New List(Of TvEpisode)
            episodelist = WorkingWithNfoFiles.ep_NfoLoad(newepisode.NfoFilePath)
            For Each ep In episodelist
                If newepisode.Season.Value = ep.Season.Value And newepisode.Episode.Value = ep.Episode.Value Then
                    episodelist.Remove(ep)
                    episodelist.Add(newepisode)
                    WorkingWithNfoFiles.ep_NfoSave(episodelist, newepisode.NfoFilePath)
                    Exit For
                End If
            Next
        End If
        
        '''''Get Episode Fanart
        tvScraperLog &= tv_EpisodeFanartGet(newepisode, Preferences.autoepisodescreenshot) & vbcrlf

        '''''Call LoadTvEpisode(WorkingEpisode)
        tv_EpisodeSelected(TvTreeview.SelectedNode.Tag) 'reload the episode after it has been rescraped
        messbox.Close()
    End Sub

    Public Function tv_EpisodeFanartGet(ByVal episode As TvEpisode, ByVal doScreenShot As Boolean) As String
        Dim result As String = "!!!  *** Unable to download Episode Thumb ***"
        Dim fpath As String = episode.NfoFilePath.Replace(".nfo", ".tbn")
        Dim paths As New List(Of String)
        If Preferences.EdenEnabled AndAlso (Preferences.overwritethumbs Or Not File.Exists(fpath)) Then paths.Add(fpath)
        fpath = fpath.Replace(".tbn", "-thumb.jpg")
        If Preferences.FrodoEnabled AndAlso (Preferences.overwritethumbs Or Not File.Exists(fpath)) Then paths.Add(fpath)
        If paths.Count > 0 Then
            Dim downloadok As Boolean = False
            If episode.Thumbnail.FileName <> Nothing AndAlso episode.Thumbnail.FileName <> "http://www.thetvdb.com/banners/" Then
                Dim url As String = episode.Thumbnail.FileName
                If Not url.IndexOf("http") = 0 And url.IndexOf(".jpg") <> -1 Then
                    url = episode.Thumbnail.Url 
                End If
                If url <> Nothing AndAlso url.IndexOf("http") = 0 AndAlso url.IndexOf(".jpg") <> -1 Then
                    downloadok = DownloadCache.SaveImageToCacheAndPaths(url, paths, True, , ,Preferences.overwritethumbs)
                Else
                    result = "!!! No thumbnail to download"
                End If
                If downloadok Then result = "!!! Episode Thumb downloaded"
            Else
                result = "!!! No thumbnail to download"
            End If
            If Not downloadok AndAlso doScreenShot Then
                Dim cachepathandfilename As String = Utilities.CreateScrnShotToCache(episode.VideoFilePath, paths(0), Preferences.ScrShtDelay)
                If Not cachepathandfilename = "" Then
                    Dim imagearr() As Integer = GetAspect(episode)
                    If Preferences.tvscrnshtTVDBResize AndAlso Not imagearr(0) = 0 Then
                        DownloadCache.CopyAndDownSizeImage(cachepathandfilename, paths(0), imagearr(0), imagearr(1))
                    Else
                        File.Copy(cachepathandfilename, paths(0), Preferences.overwritethumbs)
                    End If
                    If paths.Count > 1 Then
                        File.Copy(paths(0), paths(1), Preferences.overwritethumbs)
                    End If 
                    result = "!!! No Episode thumb to download, Screenshot saved"
                Else
                    result = "!!! No Episode thumb to download, Screenshot Could not be saved!"
                End If
            End If
        ElseIf paths.Count = 0 Then
            result = "!!! Episode Thumb(s) already exist and are not set to overwrite"
        End If
        Return result
    End Function

    Public Function tv_ShowSelectedCurrently() As Media_Companion.TvShow
        TvTreeview.Focus()
        If TvTreeview.SelectedNode Is Nothing Then
            If TvTreeview.Nodes.Count = 0 Then Return Nothing
            TvTreeview.SelectedNode = TvTreeview.TopNode
        End If
        If TvTreeview.SelectedNode Is Nothing Then
            TvTreeview.SelectedNode = TvTreeview.Nodes(0)
        End If

        Dim Show As Media_Companion.TvShow = Nothing
        Dim Season As Media_Companion.TvSeason = Nothing
        Dim Episode As Media_Companion.TvEpisode = Nothing

        If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
            Show = TvTreeview.SelectedNode.Tag
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
            Season = TvTreeview.SelectedNode.Tag
            Show = Season.GetParentShow
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then


            Episode = TvTreeview.SelectedNode.Tag
            Season = Episode.SeasonObj
            Show = Episode.ShowObj
        End If


        Return Show
    End Function

    Public Function tv_SeasonSelectedCurrently() As Media_Companion.TvSeason
        If TvTreeview.SelectedNode Is Nothing Then Return Nothing

        Dim Show As Media_Companion.TvShow = Nothing
        Dim Season As Media_Companion.TvSeason = Nothing
        Dim Episode As Media_Companion.TvEpisode = Nothing

        If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
            Show = TvTreeview.SelectedNode.Tag
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
            Season = TvTreeview.SelectedNode.Tag
            Show = Season.GetParentShow
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
            Episode = TvTreeview.SelectedNode.Tag
            Season = Episode.SeasonObj
            Show = Episode.ShowObj
        End If

        Return Season
    End Function

    Public Function ep_SelectedCurrently() As Media_Companion.TvEpisode
        If TvTreeview.SelectedNode Is Nothing Then Return Nothing

        Dim Show As Media_Companion.TvShow = Nothing
        Dim Season As Media_Companion.TvSeason = Nothing
        Dim Episode As Media_Companion.TvEpisode = Nothing

        If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
            Show = TvTreeview.SelectedNode.Tag
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
            Season = TvTreeview.SelectedNode.Tag
            Show = Season.GetParentShow
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
            Episode = TvTreeview.SelectedNode.Tag
            Season = Episode.SeasonObj
            Show = Episode.ShowObj
        End If

        Return Episode
    End Function

    Private Sub tv_Filter()
        tv_Filter(Nothing)
    End Sub
    Private Sub tv_Filter(ByVal overrideShowIsMissing As String) 'ByVal butt As String)
        Dim butt As String = ""
        Dim ThisDate As Date = If(Preferences.TvMissingEpOffset, Now.AddDays(-1), Now)
        Dim eden As Boolean = Preferences.EdenEnabled
        Dim frodo As Boolean = Preferences.FrodoEnabled
        Dim overrideIsMissing As Boolean = overrideShowIsMissing IsNot Nothing

        If rbTvListAll.Checked              Then butt = "all"
        If rbTvMissingFanart.Checked        Then butt = "fanart"
        If rbTvMissingPoster.Checked        Then butt = "posters"
        If rbTvMissingThumb.Checked         Then butt = "screenshot"
        If rbTvMissingEpisodes.Checked      Then butt = "missingeps"
        If rbTvMissingAiredEp.Checked       Then butt = "airedmissingeps"
        If rbTvDisplayWatched.Checked       Then butt = "watched"
        If rbTvDisplayUnWatched.Checked     Then butt = "unwatched"
        If rbTvListContinuing.Checked       Then butt = "continuing"
        If rbTvListEnded.Checked            Then butt = "ended"
        
        If startup = True Then butt = "all"
        If butt = "missingeps" Then
            If Preferences.displayMissingEpisodes Then
                For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                    For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                        For Each episode As Media_Companion.TvEpisode In Season.Episodes
                            If Not episode.IsMissing Then
                                episode.Visible = False
                            Else
                                If String.IsNullOrEmpty(episode.Aired.Value) Then                   ' Change the colour to gray
                                    episode.EpisodeNode.ForeColor = Color.Gray
                                Else
                                    Try
                                        If Convert.ToDateTime(episode.Aired.Value) > ThisDate Then       ' Is the episode in the future?
                                            episode.EpisodeNode.ForeColor = Color.Red               '  Yes, so change its colour to Red
                                        Else
                                            episode.EpisodeNode.ForeColor = Drawing.Color.Blue
                                        End If
                                    Catch ex As Exception
                                        episode.EpisodeNode.ForeColor = Drawing.Color.Blue          ' Set the colour to the missing colour
                                    End Try
                                End If
                                episode.Visible = True
                                episode.EpisodeNode.EnsureVisible()
                            End If
                        Next
                        Season.Visible = Season.VisibleEpisodeCount > 0
                    Next
                    item.Visible = item.VisibleSeasonCount > 0
                Next
            Else
                MsgBox("Enable Display Missing Episodes")
            End If
        ElseIf butt.Contains("watched") Then
            Dim playcount As Integer = If(butt = "watched", 1, 0)
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If episode.PlayCount.Value = playcount.ToString Then
                            episode.Visible = True
                        Else
                            episode.Visible = False
                        End If
                    Next
                    Season.Visible = Season.VisibleEpisodeCount > 0
                Next
                item.Visible = item.VisibleSeasonCount > 0
            Next
        ElseIf butt = "airedmissingeps" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If Not episode.IsMissing Then
                            episode.Visible = False
                        Else
                            If String.IsNullOrEmpty(episode.Aired.Value) Then
                                episode.Visible = False
                            Else
                                Try     ' Has the episode been aired yet?
                                    If Convert.ToDateTime(episode.Aired.Value) <= ThisDate Then
                                        episode.Visible = True
                                        episode.EpisodeNode.EnsureVisible()
                                    Else
                                        episode.Visible = False
                                    End If
                                Catch ex As Exception
                                    episode.Visible = False     ' We failed to convert the aired date to a date, therefore don't show the episode
                                End Try
                            End If
                        End If
                    Next
                    Season.Visible = Season.VisibleEpisodeCount > 0
                Next
                item.Visible = item.VisibleSeasonCount > 0
            Next
        ElseIf butt = "screenshot" Then
            Dim edenart As String = ""
            Dim frodoart As String = ""
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If episode.IsMissing Then
                            episode.Visible = False
                            Continue For
                        End If
                        edenart = episode.Thumbnail.Path
                        frodoart = episode.Thumbnail.Path.Replace(".tbn", "-thumb.jpg")
                        If (Not String.IsNullOrEmpty(episode.Thumbnail.FileName)) Then
                            If ((eden And Not frodo) AndAlso IO.File.Exists(edenart)) Or ((frodo And Not eden) AndAlso IO.File.Exists(frodoart)) Or ((frodo And eden) And (IO.File.Exists(edenart) And IO.File.Exists(frodoart))) Then
                                episode.Visible = False
                            Else
                                episode.Visible = True
                                episode.EpisodeNode.EnsureVisible()
                            End If
                        End If
                    Next
                    Season.Visible = Season.VisibleEpisodeCount > 0
                Next
                item.Visible = item.VisibleSeasonCount > 0
            Next
        ElseIf butt = "all" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                item.Visible = True
                Dim containsVisibleSeason As Boolean = False
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If episode.IsMissing AndAlso Not (Preferences.displayMissingEpisodes Or (overrideIsMissing AndAlso episode.ShowObj.ToString = overrideShowIsMissing)) Then
                            episode.Visible = False
                        Else
                            Try
                            If episode.IsMissing Then
                                If episode.Aired.Value <> "" Then
                                    If Convert.ToDateTime(episode.Aired.Value) > ThisDate Then
                                        episode.EpisodeNode.ForeColor = Color.Red           '  Yes, so change its colour to Red
                                    Else
                                        episode.EpisodeNode.ForeColor = Drawing.Color.Blue
                                    End If
                                Else
                                    episode.EpisodeNode.ForeColor = Drawing.Color.gray
                                End If
                            End If
                            Catch
                                episode.EpisodeNode.ForeColor = Color.Red
                            End Try
                            episode.Visible = True
                        End If
                    Next
                    Season.Visible = Season.VisibleEpisodeCount > 0
                Next
                item.Visible = item.VisibleSeasonCount > 0
            Next
        ElseIf butt = "continuing" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                Dim visible As Boolean = item.Status.Value = "Continuing"
                'item.Visible = If(item.Status.Value = "Continuing", True, False)
                'If Not item.Visible Then Continue For
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        episode.Visible = visible
                    Next
                    Season.Visible = Season.VisibleEpisodeCount > 0
                Next
                item.Visible = item.VisibleSeasonCount > 0
            Next
        ElseIf butt = "ended" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                Dim visible As Boolean = item.Status.Value = "Ended"
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        episode.Visible = visible
                    Next
                    Season.Visible = Season.VisibleEpisodeCount > 0
                Next
                item.Visible = item.VisibleSeasonCount > 0
            Next
        ElseIf butt = "fanart" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                item.Visible = True
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        episode.Visible = False
                    Next
                    Season.Visible = False
                Next
                If item.ImageFanart.Exists Then
                    item.Visible = False
                Else
                    item.Visible = True
                End If
            Next
        ElseIf butt = "posters" Then
            Dim edenpost As String = ""
            Dim frodopost As String = ""
            Dim frodobann As String = ""
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                item.Visible = False
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    edenpost = Season.Poster.Path
                    frodopost = Season.Poster.Path.Replace(".tbn", "-poster.jpg")
                    frodobann = Season.Banner.Path 
                    If ((eden And frodo) AndAlso (IO.File.Exists(edenpost) And (IO.File.Exists(frodopost) AndAlso IO.File.Exists(frodobann)))) Or ((eden And Not frodo) AndAlso IO.File.Exists(edenpost)) Or ((frodo And Not eden) AndAlso (IO.File.Exists(frodopost) Or IO.File.Exists(frodobann))) Then
                        Season.Visible = False
                    Else
                        Season.Visible = True
                    End If
                    For Each Episode As Media_Companion.TvEpisode In Season.Episodes
                        Episode.Visible = False
                    Next
                Next
                If frodo And Not IO.File.Exists(item.FolderPath + "poster.jpg") Then
                    item.Visible = True
                ElseIf eden And Not frodo Then
                    item.Visible = Not item.ImagePoster.Exists
                End If

            Next
        End If
    End Sub

#Region "Tv MissingEpisode Routines"

    Private Sub Bckgrndfindmissingepisodes_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Bckgrndfindmissingepisodes.DoWork
        Try
            Call tv_EpisodesMissingFind(e.Argument)
            e.Result = e.Argument
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Bckgrndfindmissingepisodes_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles Bckgrndfindmissingepisodes.ProgressChanged
        Try
            If e.ProgressPercentage = 1 Then
                If TypeOf e.UserState Is Media_Companion.TvEpisode Then
                    Dim MissingEpisode As Media_Companion.TvEpisode = e.UserState

                    MissingEpisode.ShowObj.AddEpisode(MissingEpisode)
                End If
            Else
                ToolStripStatusLabel2.Text = e.UserState
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Bckgrndfindmissingepisodes_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bckgrndfindmissingepisodes.RunWorkerCompleted
        Try
            Dim ShowList As List(Of TvShow) = e.Result
            Preferences.DlMissingEpData = False
            ToolStripStatusLabel2.Visible = False
            ToolStripStatusLabel2.Text = "TV Show Episode Scan In Progress"
            Application.DoEvents()
            TvTreeview.Sort()
            Dim showToRefresh = Nothing
            If (ShowList.Count = 1) Then
                ' ignore missing episodes checked entry for this forced node refresh
                showToRefresh = ShowList(0).ToString()
            End If
            tv_CacheLoad()
            tv_Filter(showToRefresh)
            'MsgBox("Missing Episode Download Complete!", MsgBoxStyle.OkOnly, "Missing Episode Download.")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tv_EpisodesMissingLoad(ByVal refresh As Boolean)
        Try
            If Not Bckgrndfindmissingepisodes.IsBusy And bckgroundscanepisodes.IsBusy = False Then
                If refresh Then
                    Dim nod As TreeNode
                    For Each nod In TvTreeview.Nodes
                        Dim nod2 As TreeNode
                        For Each nod2 In nod.Nodes
                            Dim nod3 As TreeNode
                            For I = nod2.Nodes.count-1 to 0 Step -1
                                nod3 = nod2.Nodes(I)
                                Dim episo As TvEpisode
                                episo = nod3.Tag
                                If episo.IsMissing Then
                                    nod2.Nodes.RemoveAt(I)
                                End If
                            Next
                        Next
                    Next
                    Dim epcount As Integer = Cache.TvCache.Episodes.Count -1
                    For I = epcount to 0 Step -1 'Each episode In sh.Episodes
                        Dim episode As TvEpisode = Cache.TvCache.Episodes.Item(I)
                        If episode.IsMissing = True Then   'Preferences.displayMissingEpisodes AndAlso episode.IsMissing = True
                            Cache.TvCache.Remove(episode)
                        End If
                    Next
                    Tv_CacheSave()
                    tv_CacheLoad()
                End If
                Dim ShowList As New List(Of TvShow)
                For Each shows In Cache.TvCache.Shows
                    'shows.MissingEpisodes.Clear()        ' - Commented out as MissingEpisodes is a Read-only list.
                    ShowList.Add(shows)
                Next
                ToolStripStatusLabel2.Text = "Starting search for missing episodes"
                ToolStripStatusLabel2.Visible = True
                Bckgrndfindmissingepisodes.RunWorkerAsync(ShowList)
            ElseIf Bckgrndfindmissingepisodes.IsBusy Then
                MsgBox("Process is already running")
            Else
                MsgBox("Missing episode search cannot be performed" & vbCrLf & "    when the episode scraper is running")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tv_EpisodesMissingFind(ByVal ShowList As List(Of TvShow))
        Utilities.EnsureFolderExists(MissingNfoPath)
        For Each item In ShowList
            Bckgrndfindmissingepisodes.ReportProgress(0, "Downloading episode data for: " & item.Title.Value)
            If item.State = Media_Companion.ShowState.Open Then
                Dim showid As String = item.TvdbId.Value
                If IsNumeric(showid) Then
                    'http://www.thetvdb.com/api/6E82FED600783400/series/85137/all/en.xml
                    Dim language As String = ""
                    If item.Language.Value <> "" Then
                        language = item.Language.Value
                    Else
                        language = "en"
                    End If
                    Dim sortorder As String = item.SortOrder.Value
                    Dim url As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & showid & "/all/" & language & ".xml"
                    If sortorder = "" Then
                        sortorder = "default"
                    End If
                    Dim xmlfile As String = SeriesXmlPath & showid & ".xml"
                    Dim SeriesInfo As New Tvdb.ShowData
                    If Not File.Exists(SeriesXmlPath & showid & ".xml") OrElse Preferences.DlMissingEpData Then
                        If Not DownloadCache.Savexmltopath(url, SeriesXmlPath, showid & ".xml", True) Then
                            MsgBox("Error retrieving data from show" & vbCrLf & "--   " & item.Title.Value.ToString & "   --", )
                            Continue For
                        End If
                    End If
                    SeriesInfo.Load(xmlfile)

                    For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                        If Preferences.ignoreMissingSpecials AndAlso NewEpisode.SeasonNumber.Value = "0" Then
                            Dim missingspecialnfo As String = MissingNfoPath & item.TvdbId.Value & "." & NewEpisode.SeasonNumber.Value & "." & NewEpisode.EpisodeNumber.Value & ".nfo"
                            If IO.File.Exists(missingspecialnfo) Then
                                Utilities.SafeDeleteFile(missingspecialnfo)
                            End If
                            Continue For
                        End If
                        Dim Episode As TvEpisode = item.GetEpisode(NewEpisode.SeasonNumber.Value, NewEpisode.EpisodeNumber.Value)
                        If Episode Is Nothing OrElse Not IO.File.Exists(Episode.NfoFilePath) Then
                            Dim MissingEpisode As New Media_Companion.TvEpisode
                            MissingEpisode.NfoFilePath = MissingNfoPath & item.TvdbId.Value & "." & NewEpisode.SeasonNumber.Value & "." & NewEpisode.EpisodeNumber.Value & ".nfo"
                            If Not IO.File.Exists(MissingEpisode.NfoFilePath) OrElse Preferences.DlMissingEpData Then
                                MissingEpisode.AbsorbTvdbEpisode(NewEpisode)
                                MissingEpisode.IsMissing = True
                                MissingEpisode.IsCache = True
                                MissingEpisode.ShowObj = item
                                MissingEpisode.Save()
                            Else
                                MissingEpisode.Load()
                            End If
                            item.AddEpisode(MissingEpisode)
                            Bckgrndfindmissingepisodes.ReportProgress(1, MissingEpisode)
                        End If
                    Next
                End If
            End If
        Next
        Tv_CacheSave()
    End Sub

    Public Function tv_EpisodesMissingUpdate(ByRef newEpList As List(Of TvEpisode)) As Boolean
        Dim Removed As Boolean = False
        Try
            'Dim missingPath = IO.Path.Combine(Preferences.applicationPath, "missing\") '& item.TvdbId.Value & "." & NewEpisode.SeasonNumber.Value & "." & NewEpisode.EpisodeNumber.Value & ".nfo")
            
            Dim Ep2Remove As New TvEpisode 
            For Each Ep In newEpList
                If IO.File.Exists(Ep.NfoFilePath) Then
                    Dim missingEpNfoPath As String = MissingNfoPath & Ep.TvdbId.Value & "." & Ep.Season.Value & "." & Ep.Episode.Value & ".nfo"
                    If IO.File.Exists(missingEpNfoPath) Then
                        IO.File.Delete(missingEpNfoPath)
                        For Each epis As TvEpisode In Cache.TvCache.Episodes 
                            If epis.TvdbId.Value = Ep.TvdbId.Value Then
                                If epis.Season.Value = Ep.Season.Value AndAlso epis.Episode.Value = Ep.Episode.Value AndAlso epis.IsMissing = True Then
                                    Ep2Remove = epis 
                                    Removed = True
                                    Exit For
                                End If
                            End If
                        Next
                        If Removed Then Cache.TvCache.Remove(Ep2Remove)
                    End If
                End If
            Next
        Catch
        End Try
        Return Removed
    End Function

    Public Sub tv_EpisodesMissingClean()
        'Dim missingnfopath As String = IO.Path.Combine(Preferences.applicationPath, "missing\")
        Dim dir_info As New IO.DirectoryInfo(MissingNfoPath)
        For Each File in dir_info.GetFiles(".nfo")
            If IO.File.Exists(File.Fullname) Then
                Try
                    IO.File.Delete(File.FullName)
                Catch
                End Try
            End If
        Next
        
    End Sub

#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Private Sub cmdTasks_Refresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdTasks_Refresh.Click
    '    TasksList.Items.Clear()
    '    'tv_MissingArtDownload(tv_ShowSelectedCurrently)

    '    For Each Item As ITask In Common.Tasks
    '        TasksList.Items.Add(Item)
    '    Next
    'End Sub
#Region "Tasks"
    Private Sub lstTasks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasksList.SelectedIndexChanged
        If TasksList.SelectedItem Is Nothing Then Exit Sub

        Dim SelectedTask As ITask

        SelectedTask = TasksList.SelectedItem

        Select Case SelectedTask.State
            Case TaskState.Completed
                TasksStateLabel.Text = "Completed"
            Case TaskState.BackgroundWorkComplete
                TasksStateLabel.Text = "Background Completed"
            Case TaskState.CriticalFault
                TasksStateLabel.Text = "Critial Fault"
            Case TaskState.Fault
                TasksStateLabel.Text = "Fault"
            Case TaskState.Halted
                TasksStateLabel.Text = "Halted"
            Case TaskState.NotStarted
                TasksStateLabel.Text = "Not Started"
            Case TaskState.WaitingForUserInput
                TasksStateLabel.Text = "Waiting For Input"
            Case TaskState.Running
                TasksStateLabel.Text = "Running"
        End Select

        'lblTask_Attempts.Text = SelectedTask.Attempts

        TasksArgumentSelector.Items.Clear()
        TasksArgumentSelector.Text = ""
        For Each Item In SelectedTask.Arguments
            TasksArgumentSelector.Items.Add(Item)

        Next
        If TasksArgumentSelector.Items.Count > 0 Then TasksArgumentSelector.SelectedIndex = 0

        TasksDependancies.Items.Clear()
        For Each Item In SelectedTask.Dependancies
            TasksDependancies.Items.Add(Item)
        Next

        TasksMessages.Items.Clear()
        For Each Item In SelectedTask.Messages
            TasksMessages.Items.Add(Item)
        Next
    End Sub

    Private Sub lstTasks_Messages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasksMessages.SelectedIndexChanged
        If TasksMessages.SelectedItem Is Nothing Then Exit Sub

        TasksSelectedMessage.Text = TasksMessages.SelectedItem
    End Sub

    Private Sub cmbTasks_Arguments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasksArgumentSelector.SelectedIndexChanged
        If TasksArgumentSelector.SelectedItem Is Nothing Then Exit Sub

        TasksArgumentDisplay.Controls.Clear()

        Dim Item As KeyValuePair(Of String, Object) = TasksArgumentSelector.SelectedItem
        If TypeOf Item.Value Is String Then
            TasksArgumentDisplay.Controls.Add(New TextBox() With {.Text = Item.Value, .Dock = DockStyle.Fill, .Multiline = True, .ScrollBars = ScrollBars.Both})
        ElseIf TypeOf Item.Value Is TvShow Then
            Dim TempShow As TvShow = Item.Value
            TasksArgumentDisplay.Controls.Add(New TextBox() With {.Text = TempShow.TvdbId.Value & " - " & TempShow.Title.Value, .Dock = DockStyle.Fill, .ScrollBars = ScrollBars.Both})
        ElseIf TypeOf Item.Value Is TvEpisode Then
            Dim TempEpisode As TvEpisode = Item.Value
            TasksArgumentDisplay.Controls.Add(New TextBox() With {.Text = TempEpisode.Id.Value & " - " & TempEpisode.Title.Value, .Dock = DockStyle.Fill, .ScrollBars = ScrollBars.Both})
        ElseIf TypeOf Item.Value Is Image Then
            TasksArgumentDisplay.Controls.Add(New PictureBox() With {.Image = Item.Value, .Dock = DockStyle.Fill})
        End If
    End Sub

    Private Sub TasksTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasksTest.Click
        For I = 0 To 20
            Common.Tasks.Add(New Tasks.BlankTask())
        Next

        RefreshTaskList()
    End Sub

    Private Sub TasksRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasksRefresh.Click
        RefreshTaskList()
    End Sub

    Private Sub TasksClearCompleted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasksClearCompleted.Click
        'Manually building a for loop
        Dim Count As Long = Common.Tasks.Count
        Dim Cursor As Long = 0

        Dim CurrentTask As ITask
        Do
            CurrentTask = Common.Tasks(Cursor)
            If CurrentTask.State = TaskState.Completed Then
                Common.Tasks.Remove(CurrentTask)
                Count -= 1
            Else
                Cursor += 1
            End If
            If Count < 1 Or Cursor >= Count Then
                Exit Do
            End If
            System.Windows.Forms.Application.DoEvents()
        Loop

        RefreshTaskList()
    End Sub

    Public TasksOnlyIncompleteTasks As Boolean = True

    Public Sub RefreshTaskList()
        For Each Item As ITask In Common.Tasks
            If Not (TasksOnlyIncompleteTasks AndAlso Item.State = TaskState.Completed) AndAlso Not TasksList.Items.Contains(Item) Then
                TasksList.Items.Add(Item)
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        Dim taskCount = TasksList.Items.Count
        Dim Cursor = 0

        Dim CurrentTask As ITask
        Do
            If taskCount < 1 Or Cursor >= taskCount Then
                Exit Do
            End If

            Try
                CurrentTask = TasksList.Items(Cursor)
                If (TasksOnlyIncompleteTasks AndAlso CurrentTask.State = TaskState.Completed) OrElse Not Common.Tasks.Contains(CurrentTask) Then
                    TasksList.Items.Remove(CurrentTask)
                    taskCount -= 1

                Else
                    Cursor += 1
                End If
            Catch
            End Try

            System.Windows.Forms.Application.DoEvents()
        Loop
    End Sub

#End Region

#Region "Tv Artwork, TV Actor & EP Thumbnail Routines"
    Private Function TvGetArtwork(ByVal currentshow As Media_Companion.TvShow, ByVal shFanart As Boolean, ByVal shPosters As Boolean, ByVal shSeason As Boolean, ByVal shXtraFanart As Boolean, Optional ByVal langu As String = "", Optional ByVal force As Boolean = True) As Boolean
        '(ByVal currentshow As Media_Companion.TvShow, Optional ByVal shFanart As Boolean = True, Optional ByVal shPosters As Boolean = True,Optional ByVal shSeason As Boolean = True)
        Dim success As Boolean = False
        Try

            Dim tvdbstuff As New TVDBScraper
            Dim showlist As New XmlDocument
            Dim currentshowpath As String = currentshow.NfoFilePath.Replace("tvshow.nfo", "") 
            Dim eden As Boolean = Preferences.EdenEnabled
            Dim frodo As Boolean = Preferences.FrodoEnabled
            Dim overwriteimage As Boolean = If(Preferences.overwritethumbs OrElse Preferences.TvChgShowOverwriteImgs, True, False)
            If Not force then overwriteimage = False    'Over-ride overwrite if we force not to overwrite, ie: missing artwork
            Dim doPoster As Boolean = If(Preferences.tvdlposter OrElse Preferences.TvChgShowDlPoster, True, False)
            Dim doFanart As Boolean = If(Preferences.tvdlfanart OrElse Preferences.TvChgShowDlFanart, True, False)
            Dim doSeason As Boolean = If(Preferences.tvdlseasonthumbs OrElse Preferences.TvChgShowDlSeasonthumbs, True, False)
            Dim thumblist As String = tvdbstuff.GetPosterList(currentshow.TvdbId.Value)
            Dim isposter As String = Preferences.postertype
            Dim isseasonall As String = Preferences.seasonall

            Dim Langlist As New List(Of String)
            If Not langu = "" Then Langlist.Add(langu)
            Langlist.Add(currentshow.Language.Value)
            If Not Langlist.Contains("en") Then Langlist.Add("en")
            If Not Langlist.Contains(Preferences.TvdbLanguageCode) Then Langlist.Add(Preferences.TvdbLanguageCode)
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

            If artlist.Count = 0 Then Exit Function

            'Posters, Main and Season Including Banners
            If shPosters Then
                'Main Poster
                If (isposter = "poster" Or frodo Or isseasonall = "poster") And doPoster Then 'poster
                    Dim mainposter As String = Nothing
                    For Each lang In Langlist 
                        For Each Image In artlist
                            If (Image.Language = lang Or lang = "") AndAlso Image.BannerType = "poster" Then
                                mainposter = Image.Url
                                Exit For
                            End If
                        Next
                        If Not IsNothing(mainposter) Then Exit For
                    Next
                    If Not IsNothing(mainposter) Then
                        Dim imgpaths As New List(Of String)
                        If frodo Then imgpaths.Add(currentshowpath & "poster.jpg")
                        If eden AndAlso isposter = "poster" Then imgpaths.Add(currentshowpath & "folder.jpg")
                        If frodo AndAlso isseasonall <> "none" Then imgpaths.Add(currentshowpath & "season-all-poster.jpg")
                        If eden AndAlso isseasonall = "poster" Then imgpaths.Add(currentshowpath & "season-all.tbn")
                        success = DownloadCache.SaveImageToCacheAndPaths(mainposter, imgpaths, False, , ,overwriteimage)
                    End If
                End If

                'Main Banner
                If (isposter = "banner" Or frodo Or isseasonall = "wide") And doPoster Then 'banner
                    Dim mainbanner As String = Nothing
                    For Each lang In Langlist 
                        For Each Image In artlist
                            If (Image.Language = lang Or lang = "") AndAlso Image.BannerType = "series" AndAlso Image.Season = Nothing Then
                                mainbanner = Image.Url
                                Exit For
                            End If
                        Next
                        If Not IsNothing(mainbanner) Then Exit For
                    Next
                    If Not IsNothing(mainbanner) Then
                        Dim imgpaths As New List(Of String)
                        If frodo then imgpaths.Add(currentshowpath & "banner.jpg")
                        If eden AndAlso isposter = "banner" Then imgpaths.Add(currentshowpath & "folder.jpg")
                        If frodo AndAlso isseasonall <> "none" Then imgpaths.Add(currentshowpath & "season-all-banner.jpg")
                        If eden AndAlso isseasonall = "wide" Then imgpaths.Add(currentshowpath & "season-all.tbn")
                        success = DownloadCache.SaveImageToCacheAndPaths(mainbanner, imgpaths, False, , ,overwriteimage)
                    End If
                End If
            End If

            'Dim shSeason As Boolean =True
            If shSeason Then
                'SeasonXX Poster
                For f = 0 To 1000
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
                            Dim imgpaths As New List(Of String)
                            If frodo Then imgpaths.Add(currentshowpath & "season" & tempstring & "-poster.jpg")
                            If eden Then imgpaths.Add(currentshowpath & "season" & tempstring & ".tbn")
                            success = DownloadCache.SaveImageToCacheAndPaths(seasonXXposter, imgpaths, False, , ,overwriteimage)
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
                            Dim imgpaths As New List(Of String)
                            If frodo Then imgpaths.Add(currentshowpath & "season" & tempstring & "-banner.jpg")
                            If eden Then imgpaths.Add(currentshowpath & "season" & tempstring & ".tbn")
                            success = DownloadCache.SaveImageToCacheAndPaths(seasonXXbanner, imgpaths, False, , ,overwriteimage)
                        End If
                    End If
                Next
                TvCheckfolderjpgart(currentshow)
            End If

            'Main Fanart
            If shFanart AndAlso doFanart Then
                Dim fanartposter As String = Nothing
                For Each lang In Langlist 
                    For Each Image In artlist
                        If (Image.Language = lang Or lang = "") AndAlso Image.BannerType = "fanart" Then
                            fanartposter = Image.Url
                            Exit For
                        End If
                    Next
                    If Not IsNothing(fanartposter) Then Exit For
                Next
                If Not IsNothing(fanartposter) Then
                    Dim imgpaths As New List(Of String)
                    imgpaths.Add(currentshowpath & "fanart.jpg")
                    If frodo And isseasonall <> "none" Then imgpaths.Add(currentshowpath & "season-all-fanart.jpg")
                    success = DownloadCache.SaveImageToCacheAndPaths(fanartposter, imgpaths, False, , ,overwriteimage)
                    End If
            End If

            'ExtraFanart
            If shXtraFanart Then
                Dim xfanart As String = currentshowpath & "extrafanart\"
                Dim fanartposter As New List(Of TVBanners)
                For Each lang In Langlist 
                    For Each Image In artlist
                        If (Image.Language = lang Or lang = "") AndAlso Image.BannerType = "fanart" Then
                            fanartposter.Add(Image)
                        End If
                    Next
                Next
                If fanartposter.Count > 0 Then
                    Dim x As Integer = 0
                    Do Until x = Preferences.TvXtraFanartQty
                        If x = fanartposter.Count Then Exit Do
                        success = Utilities.DownloadFile(fanartposter(x).url, (xfanart & fanartposter(x).id & ".jpg"))
                        x = x + 1
                    Loop
                End If
            End If
        Catch
        End Try
        Return success

    End Function

    Private Function TvGetActorTvdb(ByRef NewShow As Media_Companion.TvShow) As Boolean
        Dim success As Boolean = True
        Dim tvdbstuff As New TVDBScraper
        Dim tempstring As String = ""
        Dim TvdbActors As List(Of str_MovieActors) = tvdbstuff.GetActors(NewShow.TvdbId.Value, templanguage)
        Dim workingpath As String = ""
        If Preferences.actorseasy AndAlso Not Preferences.tvshowautoquick Then
            workingpath = NewShow.NfoFilePath.Replace(IO.Path.GetFileName(NewShow.NfoFilePath), "") & ".actors\"
            Utilities.EnsureFolderExists(workingpath)
        End If
        For Each NewAct In TvdbActors
            Dim id As String = If(NewAct.ActorId = Nothing, "", NewAct.ActorId)
            Dim results As XmlNode = Nothing
            Dim filename As String = Utilities.cleanFilenameIllegalChars(NewAct.actorname)
            filename = filename.Replace(" ", "_")
            If Not String.IsNullOrEmpty(NewAct.actorthumb) And NewAct.actorthumb <> "http://thetvdb.com/banners/" Then

                'Save to .actor folder
                If NewAct.actorthumb <> "" And Preferences.actorseasy = True And Preferences.tvshowautoquick = False Then
                    If NewShow.TvShowActorSource.Value <> "imdb" Or NewShow.ImdbId = Nothing Then
                        Dim ActorFilename As String = IO.Path.Combine(workingpath, filename)
                        Dim actorpaths As New List(Of String)
                        If Preferences.FrodoEnabled Then actorpaths.Add(ActorFilename & ".jpg")
                        If Preferences.EdenEnabled Then actorpaths.Add(ActorFilename & ".tbn")
                        Dim cachename As String = Utilities.Download2Cache(NewAct.actorthumb)
                        If cachename <> "" Then
                            For Each p In actorpaths
                                Utilities.SafeCopyFile(cachename, p, Preferences.overwritethumbs)
                            Next
                        End If
                    End If
                End If

                'Save to Local actor folder
                If Preferences.actorsave = True And id <> "" Then 'Allow Local folder save, separate from .actor folder saving 
                    Dim workingpath2 As String = ""
                    Dim networkpath As String = Preferences.actorsavepath
                    filename = filename & "_" & id
                    tempstring = networkpath & "\" & filename.Substring(0,1) & "\"

                    Utilities.EnsureFolderExists(tempstring)
                    workingpath2 = tempstring & filename 
                    Dim actorpaths As New List(Of String)
                    If Preferences.FrodoEnabled Then actorpaths.Add(workingpath2 & ".jpg")
                    If Preferences.EdenEnabled Then actorpaths.Add(workingpath2 & ".tbn")
                    Dim cachename As String = Utilities.Download2Cache(NewAct.actorthumb)
                    If cachename <> "" Then
                        For Each p In actorpaths
                            Utilities.SafeCopyFile(cachename, p, Preferences.overwritethumbs)
                        Next
                    End If
                    NewAct.actorthumb = actorpaths(0)
                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                        NewAct.actorthumb = actorpaths(0).Replace(networkpath, Preferences.actornetworkpath).Replace("\","/")
                    ElseIf Preferences.actornetworkpath.IndexOf("\") <> -1 
                        NewAct.actorthumb = actorpaths(0).Replace(networkpath, Preferences.actornetworkpath).Replace("/","\")
                    End If
                End If
            End If
            Dim exists As Boolean = False
            NewShow.ListActors.Add(NewAct)
        Next
        Return success
    End Function

    Private Function TvGetActorImdb(ByRef NewShow As Media_Companion.TvShow) As Boolean
        Dim imdbscraper As New Classimdb
        Dim success As Boolean = True
        Dim actmax As Integer = Preferences.maxactors
        Dim actcount As Integer = 0
        Dim actorstring As New XmlDocument
        If String.IsNullOrEmpty(NewShow.ImdbId.Value) Then Return success
        Dim actorlist As List(Of str_MovieActors) = imdbscraper.GetImdbActorsList(Preferences.imdbmirror, NewShow.ImdbId.Value, actmax)
        Dim workingpath As String = ""
        If Preferences.actorseasy And Not Preferences.tvshowautoquick Then
            workingpath = NewShow.NfoFilePath.Replace(IO.Path.GetFileName(NewShow.NfoFilePath), "")
            workingpath = workingpath & ".actors\"
            Utilities.EnsureFolderExists(workingpath)
        End If
        For Each thisresult In actorlist
            If Not String.IsNullOrEmpty(thisresult.actorthumb) AndAlso Not String.IsNullOrEmpty(thisresult.actorid) AndAlso actcount < (actmax + 1) Then
                If Preferences.actorseasy And Not Preferences.tvshowautoquick Then
                    Dim actorpaths As New List(Of String)
                    Dim filename As String = Utilities.cleanFilenameIllegalChars(thisresult.actorname)
                    filename = filename.Replace(" ", "_")
                    filename = IO.Path.Combine(workingpath, filename)
                    If Preferences.FrodoEnabled Then actorpaths.Add(filename & ".jpg")
                    If Preferences.EdenEnabled Then actorpaths.Add(filename & ".tbn")
                    Dim cachename As String = Utilities.Download2Cache(thisresult.actorthumb)
                    If cachename <> "" Then
                        For Each p In actorpaths
                            Utilities.SafeCopyFile(cachename, p, Preferences.overwritethumbs)
                        Next
                    End If
                End If
                If Preferences.actorsave = True AndAlso Preferences.tvshowautoquick = False Then
                    Dim tempstring As String = Preferences.actorsavepath
                    Dim workingpath2 As String = ""
                    If Preferences.actorsavealpha Then
                        Dim actorfilename As String = thisresult.actorname.Replace(" ", "_") & "_" & thisresult.actorid ' & ".tbn"
                        tempstring = tempstring & "\" & actorfilename.Substring(0,1) & "\"
                        workingpath2 = tempstring & actorfilename
                    Else
                        tempstring = tempstring & "\" & thisresult.actorid.Substring(thisresult.actorid.Length - 2, 2) & "\"
                        workingpath2 = tempstring & thisresult.actorid ' & ".tbn"
                    End If
                    Dim actorpaths As New List(Of String)
                    If Preferences.FrodoEnabled Then actorpaths.Add(workingpath2 & ".jpg")
                    If Preferences.EdenEnabled Then actorpaths.Add(workingpath2 & ".tbn")
                    Utilities.EnsureFolderExists(tempstring)
                    Dim cachename As String = Utilities.Download2Cache(thisresult.actorthumb)
                    If cachename <> "" Then
                        For Each p In actorpaths
                            Utilities.SafeCopyFile(cachename, p, Preferences.overwritethumbs)
                        Next
                    End If
                    If Not String.IsNullOrEmpty(Preferences.actornetworkpath) Then
                        If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                            thisresult.actorthumb = actorpaths(0).Replace(Preferences.actorsavepath, Preferences.actornetworkpath).Replace("\", "/")
                        Else
                            thisresult.actorthumb = actorpaths(0).Replace(Preferences.actorsavepath, Preferences.actornetworkpath).Replace("/", "\")
                        End If
                    End If
                End If
            End If
            NewShow.ListActors.Add(thisresult)
            success = True
            actcount += 1
        Next
        While NewShow.ListActors.Count > actmax
            NewShow.ListActors.RemoveAt(NewShow.ListActors.Count - 1)
        End While
        Return success
    End Function

    Private Sub FixTvActorsNfo(ByRef TvSeries As TvShow)
        'If XBMC networkpath changed, update actor thumb path
        For Each tvActor In TvSeries.listactors
            If Preferences.actorsave AndAlso tvActor.actorid <> "" Then
                If Not String.IsNullOrEmpty(Preferences.actorsavepath) Then
                    Dim tempstring As String = Preferences.actorsavepath
                    Dim workingpath As String = ""
                    If Preferences.actorsavealpha Then
                        Dim actorfilename As String = tvActor.actorname.Replace(" ", "_") & "_" & tvActor.actorid & ".jpg"
                        tempstring = tempstring & "\" & actorfilename.Substring(0,1) & "\"
                        workingpath = tempstring & actorfilename 
                    Else
                        tempstring = tempstring & "\" & tvActor.actorid.Substring(tvActor.actorid.Length - 2, 2) & "\"
                        workingpath = tempstring & tvActor.actorid & ".jpg"
                    End If
                    If Not String.IsNullOrEmpty(Preferences.actornetworkpath) Then
                        If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                            tvActor.actorthumb = workingpath.Replace(Preferences.actorsavepath, Preferences.actornetworkpath).Replace("\", "/")
                        Else
                            tvActor.actorthumb = workingpath.Replace(Preferences.actorsavepath, Preferences.actornetworkpath).Replace("/", "\")
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub TvDeleteShowArt(ByVal NewShow As Media_Companion.TvShow, Optional ByVal NotActors As Boolean = True)
        Try
            Dim workingpath As String = NewShow.FolderPath 
            
            If NotActors AndAlso Directory.Exists(workingpath & ".actors") Then Directory.Delete(workingpath & ".actors", True)
            
            If Directory.Exists(workingpath & "extrafanart") Then Directory.Delete(workingpath & "extrafanart", True)

            Dim artextn() As String = "*.jpg:*.tbn:*.png".Split(":")
            For Each arttype In artextn
                For Each filepath In Directory.GetFiles(workingpath, arttype, SearchOption.TopDirectoryOnly)
                    File.Delete(filepath)
                Next
            Next

            Dim seasonfolderpath As String = ""
            For Each ep As Media_Companion.TvEpisode In NewShow.Episodes
                seasonfolderpath = ep.FolderPath
                If File.Exists(seasonfolderpath & "folder.jpg") Then Utilities.SafeDeleteFile(seasonfolderpath & "folder.jpg")
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TvDeleteEpisodeNfoAndArt(ByVal NewShow As Media_Companion.TvShow)
        Try
            For Each ep As Media_Companion.TvEpisode In NewShow.Episodes
                Dim eppath As String = ep.NfoFilePath.Replace(".nfo", "")
                If File.Exists(eppath & "-fanart.jpg") Then Utilities.SafeDeleteFile(eppath & "-fanart.jpg")
                If File.Exists(eppath & ".tbn") Then Utilities.SafeDeleteFile(eppath & ".tbn")
                If File.Exists(ep.FolderPath & "folder.jpg") Then Utilities.SafeDeleteFile(ep.FolderPath & "folder.jpg")
                If File.Exists(ep.NfoFilePath) Then Utilities.SafeDeleteFile(ep.NfoFilePath)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub tv_MissingArtDownload(ByVal BrokenShow As TvShow)
        Dim messbox As New frmMessageBox("Attempting to download art", "", "       Please Wait")
        messbox.Show()
        messbox.Refresh()
        Application.DoEvents()
        Try
            If Preferences.TvFanartTvFirst Then
                If Preferences.TvDlFanartTvArt Then TvFanartTvArt(BrokenShow, False)
                TvGetArtwork(BrokenShow, True, True, True, Preferences.dlTVxtrafanart, force:= False)
            Else
                TvGetArtwork(BrokenShow, True, True, True, Preferences.dlTVxtrafanart, force:= False)
                If Preferences.TvDlFanartTvArt Then TvFanartTvArt(BrokenShow, False)
            End If
            If Preferences.tvfolderjpg OrElse Preferences.seasonfolderjpg Then
                TvCheckfolderjpgart(BrokenShow)
            End If
        Catch
        End Try
        Call tv_ShowLoad(BrokenShow)
        messbox.Close()

    End Sub

    Private Sub TvCheckfolderjpgart(ByVal ThisShow As TvShow)
        Dim currentshowpath As String = ThisShow.FolderPath
        If Preferences.tvfolderjpg AndAlso Not File.Exists(currentshowpath & "folder.jpg") Then
            If File.Exists(currentshowpath & "poster.jpg") AndAlso Preferences.FrodoEnabled Then
                Utilities.SafeCopyFile(currentshowpath & "poster.jpg", currentshowpath & "folder.jpg", False)
            End If
        End If
        Dim I = 1
        If Preferences.seasonfolderjpg Then
            For Each Seas In ThisShow.Seasons.Values
                If Seas.FolderPath <> ThisShow.FolderPath Then
                    Dim seasonfile As String = Seas.Poster.FileName   '= Seas.SeasonLabel.ToLower.Replace(" ", "")& "-poster.jpg"
                    If File.Exists(ThisShow.FolderPath & seasonfile) AndAlso Not File.Exists(Seas.FolderPath & "folder.jpg") Then
                        Utilities.SafeCopyFile(ThisShow.FolderPath & seasonfile, Seas.FolderPath & "folder.jpg", False)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub TvEpThumbScreenShot()
        Try
            Dim aok As Boolean = False
            Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
            If WorkingEpisode.IsMissing Then Exit Sub
            If TextBox35.Text = "" Then TextBox35.Text = Preferences.ScrShtDelay
            If IsNumeric(TextBox35.Text) Then
                Dim paths As New List(Of String)
                If Preferences.EdenEnabled Then paths.Add(WorkingEpisode.NfoFilePath.Replace(".nfo", ".tbn"))
                If Preferences.FrodoEnabled Then paths.Add(WorkingEpisode.NfoFilePath.Replace(".nfo", "-thumb.jpg"))
                Dim messbox As frmMessageBox = New frmMessageBox("ffmpeg is working to capture the desired screenshot", "", "Please Wait")
                Dim tempstring2 As String = WorkingEpisode.VideoFilePath 
                If IO.File.Exists(tempstring2) Then
                    Dim seconds As Integer = Preferences.ScrShtDelay
                    If Convert.ToInt32(TextBox35.Text) > 0 Then
                        seconds = Convert.ToInt32(TextBox35.Text)
                    End If
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                    messbox.Show()
                    messbox.Refresh()
                    Application.DoEvents()
                    Dim cachepathandfilename As String = Utilities.CreateScrnShotToCache(tempstring2, paths(0), seconds)
                    If cachepathandfilename <> "" Then
                        aok = True
                        Dim imagearr() As Integer = GetAspect(WorkingEpisode)
                        If Preferences.tvscrnshtTVDBResize AndAlso Not imagearr(0) = 0 Then 
                            DownloadCache.CopyAndDownSizeImage(cachepathandfilename, paths(0), imagearr(0), imagearr(1))
                        Else
                            File.Copy(cachepathandfilename, paths(0), True)
                        End If

                        If paths.Count > 1 Then File.Copy(paths(0), paths(1), True)

                        If File.Exists(paths(0)) Then
                            util_ImageLoad(PictureBox14, paths(0), Utilities.DefaultTvFanartPath)
                            util_ImageLoad(tv_PictureBoxLeft, paths(0), Utilities.DefaultTvFanartPath)
                            Dim Rating As String = tb_EpRating.Text  'WorkingEpisode.Rating.Value
                            If TestForMultiepisode(WorkingEpisode.NfoFilePath) Then
                                Dim episodelist As New List(Of TvEpisode)
                                episodelist = WorkingWithNfoFiles.ep_NfoLoad(WorkingEpisode.NfoFilePath)
                                For Each Ep In episodelist
                                    If Ep.Season.Value = WorkingEpisode.Season.Value And Ep.Episode.Value = WorkingEpisode.Episode.value Then
                                        Dim video_flags = GetMultiEpMediaFlags(ep)
                                        movieGraphicInfo.OverlayInfo(tv_PictureBoxLeft, Rating, video_flags)
                                    End If
                                Next
                            Else
                                Dim video_flags = GetEpMediaFlags()
                                movieGraphicInfo.OverlayInfo(tv_PictureBoxLeft, Rating, video_flags)
                            End If
                        End If
                    End If
                End If
                messbox.Close()
                If Not aok Then MsgBox("Could not create ScreenShot")
            Else
                MsgBox("Please enter a numerical value into the textbox")
                TextBox34.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TvEpThumbRescrape()
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()

            Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
            If WorkingEpisode.IsMissing Then Exit Sub
            Dim messbox As frmMessageBox = New frmMessageBox("Checking TVDB for screenshot", "", "Please Wait")
            Dim episodescraper As New TVDBScraper
            Dim id As String = WorkingTvShow.TvdbId.Value
            Dim sortorder As String = WorkingTvShow.SortOrder.Value
            Dim seasonno As String = WorkingEpisode.Season.Value
            Dim episodeno As String = WorkingEpisode.Episode.Value
            Dim language As String = WorkingTvShow.Language.Value
            Dim eden As Boolean = Preferences.EdenEnabled
            Dim frodo As Boolean = Preferences.FrodoEnabled
            If language = Nothing Then language = "en"
            If language = "" Then language = "en"
            If String.IsNullOrEmpty(sortorder) Then sortorder = "default"
            'If sortorder = Nothing Then sortorder = "default"
            'If sortorder = "" Then sortorder = "default"


            If String.IsNullOrEmpty(id) Then
                MsgBox("No ID is available for this show")
                Exit Sub
            End If
            If String.IsNullOrEmpty(episodeno) Then
                MsgBox("No Episode Number is available for this show")
                Exit Sub
            End If
            If String.IsNullOrEmpty(seasonno) Then
                MsgBox("No Season Number is available for this show")
                Exit Sub
            End If
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()
            Dim tempepisode As String = episodescraper.getepisode(WorkingTvShow.TvdbId.Value, sortorder, seasonno, episodeno, language, True)
            Dim thumburl As String = ""
            messbox.Close()
            Dim scrapedepisode As New XmlDocument
            Try
                If tempepisode <> Nothing Then
                    scrapedepisode.LoadXml(tempepisode)
                    Dim thisresult As XmlNode = Nothing
                    For Each thisresult In scrapedepisode("episodedetails")
                        Select Case thisresult.Name
                            Case "thumb"
                                thumburl = thisresult.InnerText
                                Exit For
                                Exit Select
                        End Select
                    Next
                    If thumburl <> "" And thumburl.ToLower <> "http://www.thetvdb.com/banners/" Then
                        messbox = New frmMessageBox("Screenshot found, downloading now", "", "Please Wait")
                        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                        messbox.Show()
                        messbox.Refresh()
                        Application.DoEvents()
                        Dim tempstring As String = WorkingEpisode.VideoFilePath.Replace(IO.Path.GetExtension(WorkingEpisode.VideoFilePath), ".tbn")
                        Dim cachename As String = Utilities.Download2Cache(thumburl)
                        If cachename <> "" Then
                            If eden Then Utilities.SafeCopyFile(cachename, tempstring, True)
                            If frodo Then
                                tempstring = tempstring.Replace(".tbn", "-thumb.jpg")
                                Utilities.SafeCopyFile(cachename, tempstring, True)
                            End If

                            util_ImageLoad(PictureBox14, tempstring, Utilities.DefaultTvFanartPath)
                            util_ImageLoad(tv_PictureBoxLeft, tempstring, Utilities.DefaultTvFanartPath)

                            Dim Rating As String = tb_EpRating.Text  'WorkingEpisode.Rating.Value
                            If TestForMultiepisode(WorkingEpisode.NfoFilePath) Then
                                Dim episodelist As New List(Of TvEpisode)
                                episodelist = WorkingWithNfoFiles.ep_NfoLoad(WorkingEpisode.NfoFilePath)
                                For Each Ep In episodelist
                                    If Ep.Season.Value = seasonno And Ep.Episode.Value = episodeno Then
                                        Dim video_flags = GetMultiEpMediaFlags(ep)
                                        movieGraphicInfo.OverlayInfo(tv_PictureBoxLeft, Rating, video_flags)
                                        Exit For
                                    End If
                                Next
                            Else
                                Dim video_flags = GetEpMediaFlags()
                                movieGraphicInfo.OverlayInfo(tv_PictureBoxLeft, Rating, video_flags)
                            End If
                            messbox.Close()
                        Else
                            messbox.Close()
                            MsgBox("Unable To Download Image")
                        End If
                    Else
                        MsgBox("No Episode Screenshot Found On TVDB")
                    End If
                Else
                    MsgBox("No Episode Screenshot Found On TVDB")
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                Throw ex
#End If
            Finally
                messbox.Close()
            End Try
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TvScrapePosterBanner(ByVal postertype As String)
        Try
            Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            Dim messbox As frmMessageBox = New frmMessageBox("Searching TVDB for " & If(postertype = "poster", "Poster", "Banner"), "", "Please Wait")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            messbox.Refresh()
            Application.DoEvents()
            Dim id As String = WorkingTvShow.TvdbId.Value
            Dim mainimages As Boolean = TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow
            Dim posterpath As String = ""
            Dim seasonno As String = ""
            Dim seasonpath As String = ""
            If Not mainimages Then
                seasonno = tv_SeasonSelectedCurrently.ToString
                seasonno = seasonno.ToLower.Replace("season ", "")
                Dim tmp As Integer = seasonno.ToInt
                seasonno = tmp.ToString
                If Preferences.seasonfolderjpg Then
                    For Each ep As TvEpisode In WorkingTvShow.Episodes
                        If ep.Season.Value = seasonno Then
                            seasonpath = ep.FolderPath.Replace(WorkingTvShow.FolderPath, "")
                            Exit For
                        End If
                    Next
                End If
            End If
            Dim language As String = WorkingTvShow.Language.Value
            Dim eden As Boolean = Preferences.EdenEnabled
            Dim frodo As Boolean = Preferences.FrodoEnabled
            If String.IsNullOrEmpty(language) Then language = "en"
            Dim tvdbstuff As New TVDBScraper
            Dim showlist As New XmlDocument
            Dim thumblist As String = tvdbstuff.GetPosterList(id)
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
                messbox.Close()
                MsgBox("No " & If(postertype = "poster", "Poster", "Banner") & " found")
                Exit Sub
            End If
            If mainimages Then
                For Each Image In artlist
                    If Image.Language = Preferences.TvdbLanguageCode And Image.BannerType = postertype And Image.Season = Nothing Then
                        posterpath = Image.Url : Exit For
                    End If
                Next
                If posterpath = "" Then
                    For Each Image In artlist
                        If Image.Language = "en" And Image.BannerType = postertype And Image.Season = Nothing Then
                            posterpath = Image.Url : Exit For
                        End If
                    Next
                End If
                If posterpath = "" Then
                    For Each Image In artlist
                        If Image.BannerType = postertype And Image.Season = Nothing Then
                            posterpath = Image.Url : Exit For
                        End If
                    Next
                End If
            Else
                Dim istype As String = ""
                If postertype = "poster" Then
                    istype = "season"
                Else
                    istype = "seasonwide"
                End If
                For Each Image In artlist
                    If Image.Season = seasonno And Image.Language = Preferences.TvdbLanguageCode And Image.Resolution = istype Then posterpath = Image.Url : Exit For
                Next
                If posterpath = "" Then
                    For Each Image In artlist
                        If Image.Season = seasonno And Image.Language = "en" And Image.Resolution = istype Then posterpath = Image.Url : Exit For
                    Next
                End If
                If posterpath = "" Then
                    For Each Image In artlist
                        If Image.Season = seasonno And Image.Resolution = istype Then posterpath = Image.Url : Exit For
                    Next
                End If
            End If
            If posterpath <> "" Then
                Dim imagepath As New List(Of String)
                If mainimages Then
                    If postertype = "poster" Then
                        If frodo Then imagepath.Add(WorkingTvShow.FolderPath & "poster.jpg")
                        If eden Then imagepath.Add(WorkingTvShow.FolderPath & "folder.jpg")
                    Else
                        If frodo Then imagepath.Add(WorkingTvShow.FolderPath & "banner.jpg")
                        If eden And Preferences.postertype = "banner" Then imagepath.Add(WorkingTvShow.FolderPath & "folder.jpg")
                    End If
                Else
                    If seasonno.ToInt < 10 Then seasonno = "0" & seasonno
                    If postertype = "poster" Then
                        If seasonno <> "00" Then
                            If Preferences.seasonfolderjpg AndAlso seasonpath <> "" Then imagepath.Add(WorkingTvShow.FolderPath & seasonpath & "folder.jpg")
                            If eden Then imagepath.Add(WorkingTvShow.FolderPath & "season" & seasonno & ".tbn")
                            If frodo Then imagepath.Add(WorkingTvShow.FolderPath & "season" & seasonno & "-poster.jpg")
                        Else
                            If Preferences.seasonfolderjpg AndAlso seasonpath <> "" Then imagepath.Add(WorkingTvShow.FolderPath & seasonpath & "folder.jpg")
                            If eden Then imagepath.Add(WorkingTvShow.FolderPath & "season-specials" & ".tbn")
                            If frodo Then imagepath.Add(WorkingTvShow.FolderPath & "season-specials" & "-poster.jpg")
                        End If
                    Else
                        If eden And Preferences.postertype = "banner" Then imagepath.Add(WorkingTvShow.FolderPath & "season" & seasonno & ".tbn")
                        If frodo Then imagepath.Add(WorkingTvShow.FolderPath & "season" & seasonno & "-banner.jpg")
                    End If
                End If
                For Each impath In imagepath
                    Utilities.DownloadFile(posterpath, impath)
                Next
                'Dim Showname As TvShow = tv_ShowSelectedCurrently()
                Dim last As Integer = imagepath.Count - 1
                If postertype = "poster" Then
                    util_ImageLoad(tv_PictureBoxRight, imagepath(last), Utilities.DefaultTvPosterPath)
                Else
                    util_ImageLoad(tv_PictureBoxBottom, imagepath(last), Utilities.DefaultTvBannerPath)
                End If
                'tv_ShowLoad(Showname)
            Else
                messbox.Close()
                MsgBox("No " & If(postertype = "poster", "Poster", "Banner") & " found")
            End If

            messbox.Close()
        Catch ex As Exception
            messbox.Close()
        End Try
    End Sub

    Private Sub TvSelectPosterBanner(ByVal poster As Boolean)
        Dim IsOfType As String = "banner"
        Dim tpindex As Integer = 2
        If Not poster Then IsOfType = "poster"
        If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then tpindex = 3
        RemoveHandler TabControl3.SelectedIndexChanged, AddressOf TabControl3_SelectedIndexChanged
        TabControl3.SelectTab(tpindex)
        AddHandler TabControl3.SelectedIndexChanged, AddressOf TabControl3_SelectedIndexChanged
        tv_PosterSetup(IsOfType)
    End Sub

    Private Sub TvFanartTvArt (ByVal ThisShow As TvShow, ByVal force As Boolean)
        Dim clearartLD As String = Nothing  : Dim logoLD As String = Nothing    : Dim clearart As String = Nothing  : Dim logo As String = Nothing
        Dim poster As String = Nothing      : Dim fanart As String = Nothing    : Dim banner As String = Nothing    : Dim landscape As String = Nothing
        Dim character As String = Nothing
        Dim currentshowpath As String = ThisShow.FolderPath
        Dim DestImg As String = ""
        Dim aok As Boolean = True
        Dim frodo As Boolean = Preferences.FrodoEnabled
        Dim eden As Boolean = Preferences.EdenEnabled 
        'Dim Overwrite As Boolean = If(Preferences.overwritethumbs OrElse force OrElse Preferences.TvChgShowOverwriteImgs, True, False)
        Dim ID As String = ThisShow.TvdbId.Value
        Dim TvFanartlist As New FanartTvTvList
        Dim newobj As New FanartTv
        newobj.ID = ID
        newobj.src = "tv"
        Try
            TvFanartlist = newobj.FanarttvTvresults
        Catch ex As Exception
            'ExceptionHandler.LogError(ex)
            aok = False
        End Try
        If Not aok Then Exit Sub
        Dim lang As New List(Of String)
        lang.Add(ThisShow.Language.Value)
        If Not lang.Contains(Preferences.TvdbLanguageCode) Then lang.Add(Preferences.TvdbLanguageCode)
        If Not lang.Contains("en") Then lang.Add("en")
        lang.Add("00")
        For Each lan In lang
            If IsNothing(clearart) Then
                For Each Art In TvFanartlist.hdclearart 
                    If Art.lang = lan Then clearart = Art.url : Exit For
                Next
            End If
            If IsNothing(clearartLD) Then
                For Each Art In TvFanartlist.clearart 
                    If Art.lang = lan Then clearartLD = Art.url : Exit For
                Next
            End If
            If IsNothing(logo) Then
                For Each Art In TvFanartlist.hdtvlogo
                    If Art.lang = lan Then logo = Art.url : Exit For
                Next
            End If
            If IsNothing(logoLD) Then
                For Each Art In TvFanartlist.clearlogo
                    If Art.lang = lan Then logoLD = Art.url : Exit For
                Next
            End If
            If IsNothing(poster) Then
                For Each Art In TvFanartlist.tvposter 
                    If Art.lang = lan Then poster = Art.url : Exit For
                Next
            End If
            If IsNothing(fanart) Then
                For Each Art In TvFanartlist.showbackground  
                    If Art.lang = lan Then fanart = Art.url : Exit For
                Next
            End If
            If IsNothing(banner) Then
                For Each Art In TvFanartlist.tvbanner 
                    If Art.lang = lan Then banner = Art.url : Exit For
                Next
            End If
            If IsNothing(landscape) Then
                For Each Art In TvFanartlist.tvthumb  
                    If Art.lang = lan Then landscape = Art.url : Exit For
                Next
            End If
            If IsNothing(character) Then
                For Each Art In TvFanartlist.characterart   
                    If Art.lang = lan Then character = Art.url : Exit For
                Next
            End If
        Next
        If IsNothing(clearart) AndAlso Not IsNothing(clearartld) Then clearart = clearartLD 
        If IsNothing(logo) AndAlso Not IsNothing(logold) Then logo = logold
            DestImg = currentshowpath & "clearart.png"
        If Not IsNothing(clearart) Then Utilities.DownloadFile(clearart, DestImg, force)  'AndAlso (force OrElse Not File.Exists(DestImg)) 
            DestImg = currentshowpath & "logo.png"
        If Not IsNothing(logo) Then Utilities.DownloadFile(logo, DestImg, force)    'AndAlso (force OrElse Not File.Exists(DestImg)) 
        DestImg = currentshowpath & "character.png"
        If Not IsNothing(character) Then Utilities.DownloadFile(character, DestImg, force)  'AndAlso (force OrElse Not File.Exists(DestImg)) 
        If Not IsNothing(poster) Then
            Dim destpaths As New List(Of String)
            If frodo Then
                destpaths.Add(currentshowpath & "poster.jpg")
                destpaths.Add(currentshowpath & "season-all-poster.jpg")
                'If Preferences.tvfolderjpg Then destpaths.Add(currentshowpath & "folder.jpg")
            End If
            If eden then
                destpaths.Add(currentshowpath & "poster.jpg")
                destpaths.Add(currentshowpath & "season-all.tbn")
            End If
            Dim success As Boolean = DownloadCache.SaveImageToCacheAndPaths(poster, destpaths, False, , , force)
        End If
        If Not IsNothing(fanart) Then
            Dim Destpaths As New List(Of String)
            If frodo Then
                Destpaths.Add(currentshowpath & "fanart.jpg")
                Destpaths.Add(currentshowpath & "season-all-fanart.jpg")
            End If
            If eden Then Destpaths.Add(currentshowpath & "fanart.jpg")
            Dim success As Boolean = DownloadCache.SaveImageToCacheAndPaths(fanart, Destpaths, False, , , force)
        End If
        DestImg = currentshowpath & "landscape.jpg"
        If Not IsNothing(landscape) Then Utilities.DownloadFile(landscape, DestImg, force)  'AndAlso (force OrElse Not File.Exists(DestImg)) 
        DestImg = currentshowpath & "banner.jpg"
        If Not IsNothing(banner) Then   'AndAlso (force OrElse Not File.Exists(DestImg)) 
            Utilities.DownloadFile(banner, DestImg, force)
            If frodo Then
                DestImg = currentshowpath & "season-all-banner.jpg"
                Utilities.DownloadFile(banner, DestImg, force)  'If force OrElse Not File.Exists(DestImg) Then 
            End If
        End If

        Dim firstseason As Integer = 1
        Dim lastseason As Integer = -1
        For Each item In TvFanartlist.seasonposter
            Dim itemseason As Integer = item.season.ToInt
            If itemseason > lastseason Then lastseason = itemseason
            If itemseason < firstseason Then firstseason = itemseason
        Next
        If lastseason >= firstseason Then
            For i = firstseason to lastseason 
                Dim savepaths As New List(Of String)
                Dim seasonurl As String = Nothing
                For Each lan In lang
                    For Each item In TvFanartlist.seasonposter
                        If item.lang = lan AndAlso item.season = i.ToString Then
                            seasonurl = item.url
                            Exit For
                        End If
                    Next
                    If Not IsNothing(seasonurl) Then Exit for
                Next
                If Not IsNothing(seasonurl) Then
                    Dim seasonno As String = i.ToString
                    If seasonno <> "" Then
                        If seasonno.Length = 1 Then seasonno = "0" & seasonno
                        If seasonno = "00" Then
                            seasonno = "-specials"
                        End If
                        destimg = currentshowpath &  "season" & seasonno & "-poster.jpg"
                        If Preferences.FrodoEnabled Then savepaths.Add(destimg)
                        If Preferences.EdenEnabled Then
                            destimg = destimg.Replace("-poster.jpg", ".tbn")
                            savepaths.Add(destimg)
                        End If
                        'If Preferences.seasonfolderjpg AndAlso ThisShow.Episodes.Count > 0 Then
                        '    For Each ep In ThisShow.Episodes
                        '        Dim TrueSeasonFolder As String = Nothing
                        '        Dim folder As Boolean = False
                        '        If ep.Season.Value = i Then
                        '            If ep.FolderPath <> currentshowpath Then
                        '                TrueSeasonFolder = ep.FolderPath & "folder.jpg"
                        '                If Not savepaths.Contains(TrueSeasonFolder) Then
                        '                    savepaths.Add(TrueSeasonFolder)
                        '                    folder = True
                        '                End If
                        '            End If
                        '        End If
                        '        If folder Then Exit For
                        '    Next
                        'End If
                    End If
                End If
                If savepaths.Count > 0 Then DownloadCache.SaveImageToCacheAndPaths(seasonurl, savepaths, False, , , force)
            Next
        End If

        firstseason = 1
        lastseason = -1
        For Each item In TvFanartlist.seasonbanner 
            Dim itemseason As Integer = item.season.ToInt
            If itemseason > lastseason Then lastseason = itemseason
            If itemseason < firstseason Then firstseason = itemseason
        Next
        If lastseason >= firstseason AndAlso frodo Then
            For i = firstseason to lastseason 
                Dim savepaths As New List(Of String)
                Dim seasonurl As String = Nothing
                For Each lan In lang
                    For Each item In TvFanartlist.seasonbanner
                        If item.lang = lan AndAlso item.season = i.ToString Then
                            seasonurl = item.url
                            Exit For
                        End If
                    Next
                    If Not IsNothing(seasonurl) Then Exit for
                Next
                If Not IsNothing(seasonurl) Then
                    Dim seasonno As String = i.ToString
                    If seasonno <> "" Then
                        If seasonno.Length = 1 Then seasonno = "0" & seasonno
                        If seasonno = "00" Then
                            seasonno = "-specials"
                        End If
                        destimg = currentshowpath &  "season" & seasonno & "-banner.jpg"
                        If Preferences.FrodoEnabled Then savepaths.Add(destimg)
                    End If
                End If
                If savepaths.Count > 0 Then DownloadCache.SaveImageToCacheAndPaths(seasonurl, savepaths, False, , , force)
            Next
        End If

        firstseason = 1
        lastseason = -1
        For Each item In TvFanartlist.seasonthumb  
            Dim itemseason As Integer = item.season.ToInt
            If itemseason > lastseason Then lastseason = itemseason
            If itemseason < firstseason Then firstseason = itemseason
        Next
        If lastseason >= firstseason AndAlso frodo Then
            For i = firstseason to lastseason 
                Dim savepaths As New List(Of String)
                Dim seasonurl As String = Nothing
                For Each lan In lang
                    For Each item In TvFanartlist.seasonthumb
                        If item.lang = lan AndAlso item.season = i.ToString Then
                            seasonurl = item.url
                            Exit For
                        End If
                    Next
                    If Not IsNothing(seasonurl) Then Exit for
                Next
                If Not IsNothing(seasonurl) Then
                    Dim seasonno As String = i.ToString
                    If seasonno <> "" Then
                        If seasonno.Length = 1 Then seasonno = "0" & seasonno
                        If seasonno = "00" Then
                            seasonno = "-specials"
                        End If
                        destimg = currentshowpath &  "season" & seasonno & "-landscape.jpg"
                        If Preferences.FrodoEnabled Then savepaths.Add(destimg)
                    End If
                End If
                If savepaths.Count > 0 Then DownloadCache.SaveImageToCacheAndPaths(seasonurl, savepaths, False, , , force)
            Next
        End If
    End Sub

#End Region

    'Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
    '    Try
    '        For Each Task In TaskCache.Tasks
    '            If Task.State = TaskState.BackgroundWorkComplete Then
    '                Task.FinishWork()
    '            End If
    '            Windows.Forms.Application.DoEvents()
    '        Next
    '    Catch

    '    End Try
    'End Sub


    'Private Sub TaskListUpdater_Tick(sender As System.Object, e As System.EventArgs) Handles TaskListUpdater.Tick
    '    'cmdTasks_Refresh_Click(Nothing, Nothing)
    '    TaskListUpdater.Enabled = False
    '    lstTasks.ResetText()

    'End Sub

    'Protected Overrides Sub Finalize()
    '    MyBase.Finalize()
    'End Sub

    Private Sub Label136_AutoSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label136.AutoSizeChanged

    End Sub

    Public Function tv_Showremovedfromlist(Optional ByVal nofolder As List(Of String) = Nothing, Optional ByVal lstboxscan As Boolean = False) As Boolean
        Dim remfolder As New List(Of String)
        Dim notvshownfo As New List(Of String)
        Dim status As Boolean = False
        If IsNothing(nofolder) Then
            For Each item In Preferences.tvFolders
                If Not IO.Directory.Exists(item) Then
                    remfolder.Add(item)
                Else
                    Dim tvnfopath As String = item & "\tvshow.nfo"
                    If Not File.Exists(tvnfopath) Then
                        notvshownfo.Add(item)
                    End If
                End If
            Next
        End If
        If notvshownfo.Count > 0 Then
            Dim notvshmsg As String = "Folders found that did not contain a tvshow.nfo file" & vbCrLf & "Do you wish to also remove these folders?" & vbCrLf
            For Each fo In notvshownfo
                notvshmsg &= fo & vbcrlf
            Next
            Dim x = MsgBox(notvshmsg, MsgBoxStyle.YesNo)
            If x = MsgBoxResult.Yes Then 
                remfolder.AddRange(notvshownfo)
            Else
                notvshownfo.Clear()
            End If
        End If
        If IsNothing(nofolder) AndAlso (IsNothing(remfolder) Or remfolder.Count <= 0) AndAlso notvshownfo.Count = 0 Then
            MsgBox("No Folders Missing or removed")
            Return False
        End If

        If IsNothing(nofolder) And Not IsNothing(remfolder) Then nofolder = remfolder
        If nofolder.Count > 0 Then
            For Each folder In nofolder
                If lstboxscan Then
                    For Each Item As Media_Companion.TvShow In Cache.TvCache.Shows
                        If Item.FolderPath.Trim("\") = folder Then
                            TvTreeview.Nodes.Remove(Item.ShowNode)
                            Cache.TvCache.Remove(Item)
                            Exit For
                        End If
                    Next
                    ListBox6.Items.Remove(folder)
                    status = True
                Else
                    tvFolders.Remove(folder)
                End If
            Next
            'If Not lstboxscan Then tv_CacheRefresh()
            MsgBox((nofolder.Count).ToString + " folder/s removed")
        End If
        Return status
    End Function

#Region "Tv Watched/Unwatched Routines"
    Private Sub Tv_MarkAs_Watched_UnWatched(ByVal toggle As String)
        If TvTreeview.SelectedNode Is Nothing Then Exit Sub
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        Dim WorkingTvSeason As TvSeason = tv_SeasonSelectedCurrently()
        Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
        If WorkingTvShow Is Nothing Then Exit Sub

        If Not IsNothing(WorkingEpisode) Then
            'Dim multi As Boolean = TestForMultiepisode(WorkingEpisode.NfoFilePath)
            'If Not multi Then
            '    WorkingEpisode.Load()
            '    WorkingEpisode.PlayCount.Value = toggle
            '    WorkingEpisode.Save()
            '    WorkingEpisode.UpdateTreenode()
            'Else
            Dim episodelist As New List(Of TvEpisode)
            episodelist = WorkingWithNfoFiles.ep_NfoLoad(WorkingEpisode.NfoFilePath)
            For Each epis In episodelist
                If toggle = "3" Then
                    Dim tmpstr As Integer = If(String.IsNullOrEmpty(epis.PlayCount.Value), 0, epis.PlayCount.Value.ToInt)
                    If tmpstr > 0 Then 
                        epis.PlayCount.Value = "0"
                    Else
                        epis.PlayCount.Value = "1"
                    End If
                    With Me.btn_EpWatched
                        .Text = If(tmpstr = 0, "Watched", "Unwatched")
                        .BackColor = If(tmpstr = 0, Color.LawnGreen, Color.Red)
                    End With
                Else
                    epis.PlayCount.Value = toggle
                End If
            Next
            WorkingWithNfoFiles.ep_NfoSave(episodelist, WorkingEpisode.NfoFilePath)
            WorkingEpisode.Load
            WorkingEpisode.UpdateTreenode()
            'End If
        ElseIf Not IsNothing(WorkingTvSeason) Then
            For Each ep In WorkingTvSeason.Episodes
                If ep.IsMissing Then Continue For
                'Dim multi As Boolean = TestForMultiepisode(ep.NfoFilePath)
                'If Not multi Then
                '    ep.Load()
                '    ep.PlayCount.Value = toggle
                '    ep.Save()
                '    ep.UpdateTreenode()
                'Else
                    Dim episodelist As New List(Of TvEpisode)
                    episodelist = WorkingWithNfoFiles.ep_NfoLoad(ep.NfoFilePath)
                    For Each epis In episodelist
                        epis.PlayCount.Value = toggle
                    Next
                    WorkingWithNfoFiles.ep_NfoSave(episodelist, ep.NfoFilePath)
                    ep.Load
                    ep.UpdateTreenode()
               ' End If
            Next
            WorkingTvSeason.UpdateTreenode()
        ElseIf Not IsNothing(WorkingTvShow) Then
            For Each ep In WorkingTvShow.Episodes
                If ep.IsMissing Then Continue For
                'Dim multi As Boolean = TestForMultiepisode(ep.NfoFilePath)
                'If Not multi Then
                '    ep.Load()
                '    ep.PlayCount.Value = toggle
                '    ep.Save()
                '    ep.UpdateTreenode()
                'Else
                    Dim episodelist As New List(Of TvEpisode)
                    episodelist = WorkingWithNfoFiles.ep_NfoLoad(ep.NfoFilePath)
                    For Each epis In episodelist
                        epis.PlayCount.Value = toggle
                    Next
                    WorkingWithNfoFiles.ep_NfoSave(episodelist, ep.NfoFilePath)
                    ep.Load
                    ep.UpdateTreenode()
                'End If
            Next
          For Each seas In WorkingTvShow.Seasons.keys  
                WorkingTvShow.Seasons(seas).UpdateTreenode()
          Next
        End If
        WorkingTvShow.UpdateTreenode()
    End Sub
    
    Public Shared Sub util_EpisodeSetWatched(ByRef playcount As String, Optional ByVal toggle As Boolean = False)
        Dim watched As Boolean = False
        If IsNumeric(playcount) Then
            watched = Convert.ToInt32(playcount) > 0
            If toggle Then
                watched = Not watched
                playcount = If(watched, "1", "0")
            End If
        End If
        With Form1.btn_EpWatched
            .Text = If(watched, "Watched", "Unwatched")
            .BackColor = If(watched, Color.LawnGreen, Color.Red)
        End With
    End Sub

#End Region

    Private Function GetEpImdbId(ByVal ImdbId As String, ByVal SeasonNo As String, ByVal EpisodeNo As String) As String
        Dim url = "http://www.imdb.com/title/" & ImdbId & "/episodes?season=" & SeasonNo
        Dim webpage As New List(Of String)
        Dim s As New Classimdb
        webpage.Clear()
        webpage = s.loadwebpage(Preferences.proxysettings, url,False,10)
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

    Private Function GetEpMediaFlags() As List(Of KeyValuePair(Of String, String))
        Dim thisep As TvEpisode = ep_SelectedCurrently()
        Dim flags As New List(Of KeyValuePair(Of String, String))
        Try
            If thisep.Details.StreamDetails.Audio.Count > 0 Then

                Dim defaultAudioTrack = (From x In thisep.Details.StreamDetails.Audio Where x.DefaultTrack.Value="Yes").FirstOrDefault

                If IsNothing(defaultAudioTrack) Then
                    defaultAudioTrack = thisep.Details.StreamDetails.Audio(0)
                End If

                Dim tracks = If(Preferences.ShowAllAudioTracks,thisep.Details.StreamDetails.Audio,From x In thisep.Details.StreamDetails.Audio Where x=defaultAudioTrack)

                For Each track In tracks
                    flags.Add(New KeyValuePair(Of String, string)("channels"+GetNotDefaultStr(track=defaultAudioTrack), GetNumAudioTracks(track.Channels.Value) ))
                    flags.Add(New KeyValuePair(Of String, string)("audio"+GetNotDefaultStr(track=defaultAudioTrack), track.Codec.Value))
                Next
            Else
                flags.Add(New KeyValuePair(Of String, string)("channels", ""))
                flags.Add(New KeyValuePair(Of String, string)("audio", ""))
            End If

            flags.Add(New KeyValuePair(Of String, string)("aspect", Utilities.GetStdAspectRatio(thisep.Details.StreamDetails.Video.Aspect.Value)))
            flags.Add(New KeyValuePair(Of String, string)("codec", thisep.Details.StreamDetails.Video.Codec.Value.RemoveWhitespace))
            flags.Add(New KeyValuePair(Of String, string)("resolution", If(thisep.Details.StreamDetails.Video.VideoResolution < 0, "", thisep.Details.StreamDetails.Video.VideoResolution.ToString)))
        Catch
        End Try
        Return flags

    End Function

    Private Function GetMultiEpMediaFlags(ByVal thisep As TvEpisode) As List(Of KeyValuePair(Of String, String))

        Dim flags As New List(Of KeyValuePair(Of String, String))
        Try
            If thisep.Details.StreamDetails.Audio.Count > 0 Then
                
                Dim AudCh As String 
                Dim defaultAudioTrack = (From x In thisep.Details.StreamDetails.Audio Where x.DefaultTrack.Value="Yes").FirstOrDefault

                If IsNothing(defaultAudioTrack) Then
                    defaultAudioTrack = thisep.Details.StreamDetails.Audio(0)
                End If

                Dim tracks = If(Preferences.ShowAllAudioTracks,thisep.Details.StreamDetails.Audio,From x In thisep.Details.StreamDetails.Audio Where x=defaultAudioTrack)

                For Each track In tracks
                    AudCh = track.Channels.Value

                    flags.Add(New KeyValuePair(Of String, string)("channels"+GetNotDefaultStr(track=defaultAudioTrack), GetNumAudioTracks(track.Channels.Value)))
                    flags.Add(New KeyValuePair(Of String, string)("audio"+GetNotDefaultStr(track=defaultAudioTrack), track.Codec.Value))
                Next
 
            Else
                flags.Add(New KeyValuePair(Of String, string)("channels", ""))
                flags.Add(New KeyValuePair(Of String, string)("audio", ""))
            End If
            flags.Add(New KeyValuePair(Of String, string)("aspect", Utilities.GetStdAspectRatio(thisep.Details.StreamDetails.Video.Aspect.Value)))
            flags.Add(New KeyValuePair(Of String, string)("codec", thisep.Details.StreamDetails.Video.Codec.Value.RemoveWhitespace))
            flags.Add(New KeyValuePair(Of String, string)("resolution", If(thisep.Details.StreamDetails.Video.VideoResolution < 0, "", thisep.Details.StreamDetails.Video.VideoResolution.ToString)))
        Catch
        End Try
        Return flags
    End Function

    Private Function GetAspect(ep As TvEpisode)
        Dim thisarray(2) As Integer
        thisarray(0) = 400
        thisarray(1) = 225
        Try
            If ep.Details.StreamDetails.Video.Width.Value is Nothing Then
                ep.GetFileDetails 
            End If
            Dim epw As Integer = ep.Details.StreamDetails.Video.Width.Value.ToInt
            Dim eph As Integer= ep.Details.StreamDetails.Video.Height.Value.ToInt
            Dim ThisAsp As Double = epw/eph
            If ThisAsp < 1.37 Then  'aspect greater than Industry Standard of 1.37:1 is classed as WideScreen
                thisarray(1) = 300
            End If
        Catch
            thisarray(0) = 0
        End Try
        Return thisarray
    End Function
        
End Class
