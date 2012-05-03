Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
'Imports System.Threading
'Imports Media_Companion.ScraperFunctions

Imports System.Xml
Imports Media_Companion

'Imports System.Reflection
'Imports System.Windows.Forms
'Imports System.ComponentModel

Partial Public Class Form1
    'Public TvCache As New TvCache

    'Public TvShows As New List(Of TvShow)
    'Public workingTvShow As New TvShow
    'Public workingEpisode As New List(Of TvEpisode)
    'Public tempWorkingTvShow As New TvShow
    'Public tempWorkingEpisode As New TvEpisode
    Dim newEpisodeList As New List(Of TvEpisode)
    Dim languageList As New List(Of Tvdb.Language)
    Dim listOfShows As New List(Of str_PossibleShowList)

    Dim tvdbposterlist As New List(Of TvBanners)
    Dim imdbposterlist As New List(Of TvBanners)
    Dim tvdbmode As Boolean = False
    Dim usedlist As New List(Of TvBanners)

    Dim tvobjects As New List(Of String)

    Public Sub tv_ViewReset()
        'RenameTVShowsToolStripMenuItem.Enabled = False
        Button_Save_TvShow_Episode.Enabled = True
        'RenameTVShowsToolStripMenuItem.Visible = False
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

        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox9.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox18.Text = ""

        ComboBox4.Items.Clear()
        ComboBox4.Text = ""
        PictureBox6.Image = Nothing

        tvdbposterlist.Clear()
        PictureBox6.Image = Nothing
        tv_PictureBoxLeft.Image = Nothing
        tv_PictureBoxRight.Image = Nothing
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox9.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox18.Text = ""
        TextBox19.Text = ""
        ComboBox4.Items.Clear()
        ComboBox4.Text = ""



        TextBox_Title.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox9.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox18.Text = ""
        TextBox19.Text = ""
        ComboBox4.Items.Clear()
        ComboBox4.Text = ""



        ComboBox4.Items.Clear()
        ComboBox4.Text = ""
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

            tv_SplitContainerAutoPosition() 'auto set container splits....after we have loaded data & pictures....
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Tv_TreeViewContextMenuItemsEnable()        'enable/disable right click context menu items depending on if its show/season/episode
        '                                                  'called from tv_treeview mouseup event where we check for a right click
        If TvTreeview.SelectedNode Is Nothing Then Return
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()  'set WORKINGTVSHOW to show obj irrelavent if we have selected show/season/episode
        Dim showtitle As String = WorkingTvShow.Title.Value       'set our show title



        'now we set the items that have variable text in the context menu using the 'show' text set above
        Tv_TreeViewContext_ShowTitle.BackColor = Color.Honeydew                'SK - same color as the refresh tv show splash - comments required to see if it works or not....


        'Tv_TreeViewContext_OpenFolder.Text = "Open """ & showtitle & """ Folder"
        'Tv_TreeViewContext_SearchNewEp.Text = "Search """ & showtitle & """ for new episodes"
        'Tv_TreeViewContext_FindMissArt.Text = "Download missing art for """ & showtitle & """"
        'Tv_TreeViewContext_ShowMissEps.Text = "Display missing episodes for """ & showtitle & """"

        'now we display what we need to display depending on what type of node we have selected

        If TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvShow Then
            Tv_TreeViewContext_ShowTitle.Text = "'" & showtitle & "'"
            Tv_TreeViewContext_ShowTitle.Font = New Font("Arial", 10, FontStyle.Bold)
            Tv_TreeViewContext_Play_Episode.Enabled = False
            Tv_TreeViewContext_ViewNfo.Text = "View TVShow .nfo"
            Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape TVShow"

            Tv_TreeViewContext_OpenFolder.Enabled = True
            Tv_TreeViewContext_ViewNfo.Enabled = True
            Tv_TreeViewContext_RescrapeShowOrEpisode.Enabled = True
            Tv_TreeViewContext_FindMissArt.Enabled = True
            Tv_TreeViewContext_RefreshShow.Enabled = True
            Tv_TreeViewContext_ReloadFromCache.Enabled = True
            Tv_TreeViewContext_RenameEp.Enabled = True
            Tv_TreeViewContext_ShowMissEps.Enabled = True
            Tv_TreeViewContext_DispByAiredDate.Enabled = True

        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvSeason Then
            Tv_TreeViewContext_ShowTitle.Text = "'" & showtitle & "' - " & tv_SeasonSelectedCurrently.SeasonLabel
            Tv_TreeViewContext_ShowTitle.Font = New Font("Arial", 10, FontStyle.Bold)
            Tv_TreeViewContext_Play_Episode.Enabled = False
            Tv_TreeViewContext_ViewNfo.Text = "View Season .nfo"
            Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape Season"

            Tv_TreeViewContext_OpenFolder.Enabled = True
            Tv_TreeViewContext_ViewNfo.Enabled = False
            Tv_TreeViewContext_RescrapeShowOrEpisode.Enabled = False
            Tv_TreeViewContext_FindMissArt.Enabled = False
            Tv_TreeViewContext_RefreshShow.Enabled = False
            Tv_TreeViewContext_ReloadFromCache.Enabled = False
            Tv_TreeViewContext_RenameEp.Enabled = True
            Tv_TreeViewContext_ShowMissEps.Enabled = True
            Tv_TreeViewContext_DispByAiredDate.Enabled = True

        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Media_Companion.TvEpisode Then
            Tv_TreeViewContext_ShowTitle.Text = "'" & showtitle & "' - S" & Utilities.PadNumber(ep_SelectedCurrently.Season.Value, 2) & "E" & Utilities.PadNumber(ep_SelectedCurrently.Episode.Value, 2) & " '" & ep_SelectedCurrently.Title.Value & "'"
            Tv_TreeViewContext_ShowTitle.Font = New Font("Arial", 10, FontStyle.Bold)
            Tv_TreeViewContext_Play_Episode.Enabled = True
            Tv_TreeViewContext_ViewNfo.Text = "View Episode .nfo"
            Tv_TreeViewContext_RescrapeShowOrEpisode.Text = "Rescrape Episode"

            Tv_TreeViewContext_OpenFolder.Enabled = True
            Tv_TreeViewContext_ViewNfo.Enabled = True
            Tv_TreeViewContext_RescrapeShowOrEpisode.Enabled = True
            Tv_TreeViewContext_FindMissArt.Enabled = False
            Tv_TreeViewContext_RefreshShow.Enabled = False
            Tv_TreeViewContext_ReloadFromCache.Enabled = False
            Tv_TreeViewContext_RenameEp.Enabled = True
            Tv_TreeViewContext_ShowMissEps.Enabled = True
            Tv_TreeViewContext_DispByAiredDate.Enabled = True

        Else
            MsgBox("None")
        End If

        'these are the four items at the bottom of the menu to control Expand/Colapse the tv_treeview (always shown)
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
    End Sub



    'Private Sub TvTreeview_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvTreeview.AfterSelect
    '    ResetTvView()
    '    NodeSelected()
    'End Sub


    'Public Sub NodeSelected()


    '    ResetTvView()
    '    Dim SelectedName As String = TvTreeview.SelectedNode.Name.ToLower
    '    If SelectedName.Contains("tvshow.nfo") Then
    '        If TabControl3.TabPages(1).Text = "Screenshot" Then
    '            TabControl3.TabPages.RemoveAt(1)
    '        End If

    '        ToolStripMenuItem1.Enabled = True
    '        ExpandSelectedShowToolStripMenuItem.Enabled = True
    '        ExpandAllToolStripMenuItem.Enabled = True
    '        CollapseAllToolStripMenuItem.Enabled = True
    '        CollapseSelectedShowToolStripMenuItem.Enabled = True
    '        ReloadItemToolStripMenuItem.Enabled = True
    '        OpenFolderToolStripMenuItem.Enabled = True
    '        'load tvshow.nfo
    '        ListBox3.Items.Clear()
    '        TextBox26.Text = ""
    '        Dim todo As Boolean = False
    '        If workingTvShow Is Nothing Then todo = True

    '        If workingTvShow.path = TvTreeview.SelectedNode.Name Then
    '            Dim tempstring As String = "Search """ & workingTvShow.title & """ for new episodes"
    '            SearchThisShowForNewEpisodesToolStripMenuItem.Text = tempstring
    '            SearchThisShowForNewEpisodesToolStripMenuItem.Enabled = True
    '            SearchThisShowForNewEpisodesToolStripMenuItem.Visible = True
    '            tempstring = "Download missing art for """ & workingTvShow.title & """"
    '            DownloadAvaileableMissingArtForShowToolStripMenuItem.Text = tempstring
    '            DownloadAvaileableMissingArtForShowToolStripMenuItem.Enabled = True
    '            DownloadAvaileableMissingArtForShowToolStripMenuItem.Visible = True
    '            tempstring = "Display missing episodes for """ & workingTvShow.title & """"
    '            MissingepisodesToolStripMenuItem.Text = tempstring
    '            MissingepisodesToolStripMenuItem.Enabled = True
    '            MissingepisodesToolStripMenuItem.Visible = True
    '            DisplayEpisodesByAiredDateToolStripMenuItem.Enabled = True
    '            DisplayEpisodesByAiredDateToolStripMenuItem.Visible = True
    '            RebuildThisShowToolStripMenuItem.Enabled = True
    '            RebuildThisShowToolStripMenuItem.Visible = True
    '        End If
    '        If todo = False Then
    '            If workingTvShow.path <> TvTreeview.SelectedNode.Name Then todo = True
    '        End If
    '        If todo = True Then
    '            Call LoadTvShow(TvTreeview.SelectedNode.Name)
    '            For tt = 0 To TvShows.Count
    '                If TvShows(tt).fullpath = TvTreeview.SelectedNode.Name Then
    '                    TvShows(tt).imdbid = workingTvShow.imdbid
    '                    TvShows(tt).language = workingTvShow.language
    '                    TvShows(tt).locked = workingTvShow.locked
    '                    TvShows(tt).rating = workingTvShow.rating
    '                    TvShows(tt).sortorder = workingTvShow.sortorder
    '                    TvShows(tt).status = workingTvShow.status
    '                    TvShows(tt).title = workingTvShow.title
    '                    'basicTvList(tt).titleandyear = workingTvShow.title
    '                    TvShows(tt).tvdbid = workingTvShow.tvdbid
    '                    TvShows(tt).year = workingTvShow.year
    '                    TvShows(tt).episodeactorsource = workingTvShow.episodeactorsource
    '                    Exit For
    '                End If
    '            Next
    '            If workingTvShow.locked = 1 Or workingTvShow.locked = 2 Then
    '                For Each indnode As TreeNode In TvTreeview.Nodes
    '                    If indnode.Name.ToLower = workingTvShow.path.ToLower Then
    '                        indnode.StateImageIndex = 0
    '                        Exit For
    '                    End If
    '                Next
    '            Else
    '                For Each indnode As TreeNode In TvTreeview.Nodes
    '                    If indnode.Name.ToLower = workingTvShow.path.ToLower Then
    '                        indnode.StateImageIndex = -1
    '                        Exit For
    '                    End If
    '                Next
    '            End If

    '            Dim tempstring As String = "Search """ & workingTvShow.title & """ for new episodes"
    '            SearchThisShowForNewEpisodesToolStripMenuItem.Text = tempstring
    '            SearchThisShowForNewEpisodesToolStripMenuItem.Enabled = True
    '            SearchThisShowForNewEpisodesToolStripMenuItem.Visible = True
    '            tempstring = "Download missing art for """ & workingTvShow.title & """"
    '            DownloadAvaileableMissingArtForShowToolStripMenuItem.Text = tempstring
    '            DownloadAvaileableMissingArtForShowToolStripMenuItem.Enabled = True
    '            DownloadAvaileableMissingArtForShowToolStripMenuItem.Visible = True
    '            tempstring = "Display missing episodes for """ & workingTvShow.title & """"
    '            MissingepisodesToolStripMenuItem.Text = tempstring
    '            MissingepisodesToolStripMenuItem.Enabled = True
    '            MissingepisodesToolStripMenuItem.Visible = True
    '            DisplayEpisodesByAiredDateToolStripMenuItem.Enabled = True
    '            DisplayEpisodesByAiredDateToolStripMenuItem.Visible = True
    '            RebuildThisShowToolStripMenuItem.Enabled = True
    '            RebuildThisShowToolStripMenuItem.Visible = True
    '            If workingTvShow.locked = 1 Then
    '                Button60.Text = "Locked"
    '                Button60.BackColor = Color.Red
    '            ElseIf workingTvShow.locked = 0 Then
    '                Button60.Text = "Open"
    '                Button60.BackColor = Color.LawnGreen
    '            ElseIf workingTvShow.locked = 2 Then
    '                Button60.Text = "Un-Verified"
    '                Button60.BackColor = Color.Red
    '            End If
    '            If workingTvShow.plot.IndexOf("Unable to find folder:") = 0 Then
    '                TvTreeview.SelectedNode.ForeColor = Color.Red
    '                TvTreeview.SelectedNode.Collapse()
    '                Exit Sub
    '            ElseIf workingTvShow.plot.ToLower.IndexOf("xml error") <> -1 Then
    '                TvTreeview.SelectedNode.ForeColor = Color.Red
    '            Else
    '                If TvTreeview.SelectedNode.ForeColor = Color.Red Then
    '                    For Each Sh In TvShows
    '                        If Sh.fullpath = TvTreeview.SelectedNode.Name Then
    '                            Sh.status = "ok"
    '                            'Call savetvdata()
    '                            Exit For
    '                        End If
    '                    Next
    '                End If
    '                'TvTreeview.SelectedNode.ForeColor = Color.Black
    '            End If
    '        Else
    '            If workingTvShow.plot.IndexOf("Unable to find folder:") = 0 Then
    '                TvTreeview.SelectedNode.ForeColor = Color.Red
    '                TvTreeview.SelectedNode.Collapse()
    '                Exit Sub
    '            Else
    '                'TvTreeview.SelectedNode.ForeColor = Color.Black
    '            End If
    '            If workingTvShow.path <> Nothing Then
    '                TextBox2.Text = workingTvShow.title
    '                Try
    '                    PictureBox5.ImageLocation = workingTvShow.path.Replace("tvshow.nfo", "folder.jpg")
    '                Catch
    '                    PictureBox5.Image = Nothing
    '                End Try
    '                Try
    '                    PictureBox4.ImageLocation = workingTvShow.path.Replace("tvshow.nfo", "fanart.jpg")
    '                Catch
    '                    PictureBox4.Image = Nothing
    '                End Try
    '            End If
    '            Panel9.Visible = False
    '        End If
    '    ElseIf TvTreeview.SelectedNode.Name.ToLower.Contains(".nfo") Then
    '        'Episode
    '    Else
    '        'seasons

    '    End If

    'End Sub



    Private Sub tv_ShowLoad(ByVal Show As Media_Companion.TvShow)
        'If Show.IsCache Then    'disabled this test, iscache=true  would not stick when doing batch wizard......
        Show.Load()
        'End If

        Dim hg As New IO.DirectoryInfo(Show.FolderPath)
        If Not hg.Exists Then
            TextBox19.Text = "Unable to find folder: " & Show.FolderPath
            TextBox_Title.Text = "Unable to find folder: " & Show.FolderPath
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


            If Preferences.postertype = "banner" Then
                util_ImageLoad(tv_PictureBoxRight, Show.ImageBanner.Path, defaultBanner) 'this function resolves file lock issue 'tv_PictureBoxRight.Image = Show.ImageBanner.Image  'this method locks the file so it cannot be replaced
            Else
                util_ImageLoad(tv_PictureBoxRight, Show.ImagePoster.Path, defaultPoster) 'tv_PictureBoxRight.Image = Show.ImagePoster.Image
            End If
            util_ImageLoad(tv_PictureBoxLeft, Show.ImageFanart.Path, defaultFanart) 'tv_PictureBoxLeft.Image = Show.ImageFanart.Image

            Panel9.Visible = False

            TextBox_Title.BackColor = Color.White
            If Show.Title.Value <> Nothing Then
                TextBox_Title.Text = Show.Title.Value

            End If

            ' changed indication of an issue, setting the title means that the title is saved to the nfo if the user exits. Yellow is the same colour as the unverified Button
            If Show.State = ShowState.Unverified Then TextBox_Title.BackColor = Color.Yellow
            If Show.State = ShowState.Error Then TextBox_Title.BackColor = Color.Red


            TextBox10.Text = Utilities.ReplaceNothing(Show.Premiered.Value)
            TextBox11.Text = Utilities.ReplaceNothing(Show.Genre.Value)
            TextBox9.Text = Utilities.ReplaceNothing(Show.TvdbId.Value)
            TextBox12.Text = Utilities.ReplaceNothing(Show.ImdbId.Value)
            TextBox13.Text = Utilities.ReplaceNothing(Show.Rating.Value)
            TextBox14.Text = Utilities.ReplaceNothing(Show.Mpaa.Value)
            TextBox15.Text = Utilities.ReplaceNothing(Show.Runtime.Value)
            TextBox16.Text = Utilities.ReplaceNothing(Show.Studio.Value)
            TextBox19.Text = Utilities.ReplaceNothing(Show.Plot.Value)

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
            If Show.EpisodeActorSource.Value Is Nothing Then
                If Preferences.TvdbActorScrape = "0" Or Preferences.TvdbActorScrape = "2" Then
                    Show.EpisodeActorSource.Value = "tvdb"
                Else
                    Show.EpisodeActorSource.Value = "imdb"
                End If
            End If

            If String.IsNullOrEmpty(Show.EpisodeActorSource.Value) Then
                If Preferences.TvdbActorScrape = "0" Or Preferences.TvdbActorScrape = "2" Then
                    Show.EpisodeActorSource.Value = "tvdb"
                Else
                    Show.EpisodeActorSource.Value = "imdb"
                End If
            End If

            If Show.EpisodeActorSource.Value = "imdb" Then
                Button46.Text = "IMDB"
            ElseIf Show.EpisodeActorSource.Value = "tvdb" Then
                Button46.Text = "TVDB"
            End If

            If String.IsNullOrEmpty(Show.TvShowActorSource.Value) Then
                If Preferences.TvdbActorScrape = "0" Or Preferences.TvdbActorScrape = "3" Then
                    Show.TvShowActorSource.Value = "tvdb"
                Else
                    Show.TvShowActorSource.Value = "imdb"
                End If
            End If

            If String.IsNullOrEmpty(Show.TvShowActorSource.Value) Then
                If Preferences.TvdbActorScrape = "0" Or Preferences.TvdbActorScrape = "3" Then
                    Show.TvShowActorSource.Value = "tvdb"
                Else
                    Show.TvShowActorSource.Value = "imdb"
                End If
            End If

            If Show.TvShowActorSource.Value = "imdb" Then
                Button45.Text = "IMDB"
            ElseIf Show.TvShowActorSource.Value = "tvdb" Then
                Button45.Text = "TVDB"
            End If
            ComboBox4.Items.Clear()
            For Each actor In Show.ListActors
                If actor.actorname <> Nothing AndAlso Not ComboBox4.Items.Contains(actor.actorname) Then
                    ComboBox4.Items.Add(actor.actorname)
                End If
            Next

            If ComboBox4.Items.Count = 0 Then
                Call tv_ActorDisplay(True)
            Else
                ComboBox4.SelectedIndex = 0
            End If
        End If
        Panel9.Visible = False
    End Sub

    Public Sub tv_ActorDisplay(Optional ByVal useDefault As Boolean = False)
        Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
        If WorkingTvShow Is Nothing Then Exit Sub
        Dim imgLocation As String = defaultActor
        TextBox18.Clear()
        PictureBox6.Image = Nothing
        If useDefault Then
            imgLocation = defaultActor
        Else
            For Each actor In WorkingTvShow.ListActors
                If actor.actorname = ComboBox4.Text Then
                    TextBox18.Text = actor.actorrole
                    Dim temppath As String = WorkingTvShow.NfoFilePath.Replace(IO.Path.GetFileName(WorkingTvShow.NfoFilePath), "")
                    Dim tempname As String = actor.actorname.Replace(" ", "_") & ".tbn"
                    temppath = temppath & ".actors\" & tempname
                    If IO.File.Exists(temppath) Then
                        imgLocation = temppath
                    ElseIf actor.actorthumb <> Nothing AndAlso (actor.actorthumb.IndexOf("http") <> -1 OrElse IO.File.Exists(actor.actorthumb)) Then
                        imgLocation = actor.actorthumb
                    End If
                    Exit For
                End If
            Next
        End If
        util_ImageLoad(PictureBox6, imgLocation, defaultActor)
        PictureBox6.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Public Sub tv_SeasonSelected(ByRef SelectedSeason As Media_Companion.TvSeason)
        'If SelectedSeason.ShowObj.IsCache Then
        SelectedSeason.ShowObj.Load()
        'End If
        Dim Show As Media_Companion.TvShow
        If SelectedSeason.SeasonNode.Parent.Tag IsNot Nothing Then
            Show = SelectedSeason.SeasonNode.Parent.Tag
        Else
            MsgBox("Show tag not set")
            Exit Sub
        End If
        TextBox_Title.BackColor = Color.White
        If Show.Title.Value <> Nothing Then
            If SelectedSeason.SeasonNumber = 0 Then
                TextBox_Title.Text = Utilities.ReplaceNothing(Show.Title.Value) & " - Specials"
            Else
                TextBox_Title.Text = Utilities.ReplaceNothing(Show.Title.Value) & " - " & Utilities.ReplaceNothing(SelectedSeason.SeasonNode.Text)
            End If
        End If
        If TabControl3.TabPages(1).Text = "Screenshot" Then
            TabControl3.TabPages.RemoveAt(1)
        End If





        ' changed indication of an issue, setting the title means that the title is saved to the nfo if the user exits. Yellow is the same colour as the unverified Button
        If Show.State = ShowState.Unverified Then TextBox_Title.BackColor = Color.Yellow
        If Show.State = ShowState.Error Then TextBox_Title.BackColor = Color.Red


        TextBox10.Text = Utilities.ReplaceNothing(Show.Premiered.Value)
        TextBox11.Text = Utilities.ReplaceNothing(Show.Genre.Value)
        TextBox9.Text = Utilities.ReplaceNothing(Show.TvdbId.Value)
        TextBox12.Text = Utilities.ReplaceNothing(Show.ImdbId.Value)
        TextBox13.Text = Utilities.ReplaceNothing(Show.Rating.Value)
        TextBox14.Text = Utilities.ReplaceNothing(Show.Mpaa.Value)
        TextBox15.Text = Utilities.ReplaceNothing(Show.Runtime.Value)
        TextBox16.Text = Utilities.ReplaceNothing(Show.Studio.Value)
        TextBox19.Text = Utilities.ReplaceNothing(Show.Plot.Value)
        Panel9.Visible = False
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


        If trueseason = -1 Then
            If SelectedSeason.Poster.Image IsNot Nothing Then
                util_ImageLoad(tv_PictureBoxRight, SelectedSeason.Poster.Path, defaultPoster) 'tv_PictureBoxRight.Image = SelectedSeason.Poster.Image
            Else
                If Preferences.postertype = "banner" Then
                    util_ImageLoad(tv_PictureBoxRight, Show.ImagePoster.Path, defaultPoster) 'tv_PictureBoxRight.Image = Show.ImagePoster.Image
                Else
                    util_ImageLoad(tv_PictureBoxRight, Show.ImageBanner.Path, defaultBanner) 'tv_PictureBoxRight.Image = Show.ImageBanner.Image
                End If
            End If
        ElseIf trueseason = 0 Then          'Specials
            If IO.File.Exists(Show.NfoFilePath.ToLower.Replace("tvshow.nfo", "season-specials.tbn")) Then
                util_ImageLoad(tv_PictureBoxRight, Show.NfoFilePath.Replace("tvshow.nfo", "season-specials.tbn"), defaultPoster)  'tv_PictureBoxRight.ImageLocation = Show.NfoFilePath.Replace("tvshow.nfo", "season-specials.tbn")
            Else
                util_ImageLoad(tv_PictureBoxRight, Show.NfoFilePath.Replace("tvshow.nfo", "folder.jpg"), defaultPoster)     'tv_PictureBoxRight.ImageLocation = Show.NfoFilePath.Replace("tvshow.nfo", "folder.jpg")
            End If
        Else                                'Season01 & up
            util_ImageLoad(tv_PictureBoxRight, SelectedSeason.Poster.Path, defaultPoster)              ' tv_PictureBoxRight.Image = SelectedSeason.Poster.Image
        End If

        If Show.NfoFilePath <> Nothing Then
            util_ImageLoad(tv_PictureBoxLeft, Show.NfoFilePath.Replace("tvshow.nfo", "fanart.jpg"), defaultFanart) 'tv_PictureBoxLeft.ImageLocation = Show.NfoFilePath.Replace("tvshow.nfo", "fanart.jpg")
        End If

    End Sub

    Public Sub tv_EpisodeSelected(ByRef SelectedEpisode As Media_Companion.TvEpisode)
        'loadepisode
        If TabControl3.TabPages(1).Text <> "Screenshot" Then
            If screenshotTab IsNot Nothing Then
                TabControl3.TabPages.Insert(1, screenshotTab)
                TabControl3.Refresh()
            End If
        End If
        Panel9.Visible = True

        Dim season As Integer = SelectedEpisode.Season.Value
        Dim episode As Integer = SelectedEpisode.Episode.Value
        Dim SeasonObj As New Media_Companion.TvSeason
        If SelectedEpisode.EpisodeNode.Parent IsNot Nothing Then
            SeasonObj = SelectedEpisode.EpisodeNode.Parent.Tag
            If season = -1 Then season = SeasonObj.SeasonLabel
        End If


        Call ep_Load(SeasonObj, SelectedEpisode)

        Tv_TreeViewContext_ViewNfo.Enabled = True
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
        Tv_TreeViewContext_ReloadFromCache.Enabled = True
        Tv_TreeViewContext_OpenFolder.Enabled = True


    End Sub

    Private Sub ep_Load(ByRef Season As Media_Companion.TvSeason, ByRef Episode As Media_Companion.TvEpisode)
        'If Episode.IsCache Then
        Episode.Load()
        'End If
        Dim tempstring As String = ""
        'TextBox_Title.Text = ""
        'TextBox_Rating.Text = ""
        'TextBox_Plot.Text = ""
        'TextBox_Director.Text = ""
        'TextBox_Credits.Text = ""
        'TextBox_Aired.Text = ""
        'TextBox25.Text = ""
        'ComboBox5.Text = ""
        'TextBox17.Text = ""
        'TextBox29.Text = ""

        ComboBox5.Items.Clear()
        TextBox29.Text = Utilities.ReplaceNothing(IO.Path.GetFileName(Episode.NfoFilePath))
        TextBox17.Text = Utilities.ReplaceNothing(Episode.FolderPath)
        If Not IO.File.Exists(Episode.NfoFilePath) Then
            TextBox_Title.Text = "Unable to find episode: " & Episode.NfoFilePath
            Panel9.Visible = True
            Episode.EpisodeNode.BackColor = Color.Red
            Exit Sub
        Else
            Episode.EpisodeNode.BackColor = Color.Transparent   'i.e. back to normal
        End If
        TextBox_Title.Text = Utilities.ReplaceNothing(Episode.ShowObj.Title.Value, "?") & " - S" & Utilities.PadNumber(Utilities.ReplaceNothing(Episode.SeasonObj.SeasonNumber), 2) & "E" & Utilities.PadNumber(Utilities.ReplaceNothing(Episode.Episode.Value), 2) & " - '" & Utilities.ReplaceNothing(Episode.Title.Value, "?") & "'"
        TextBox_Rating.Text = Utilities.ReplaceNothing(Episode.Rating.Value)
        TextBox_Plot.Text = Utilities.ReplaceNothing(Episode.Plot.Value)
        TextBox_Director.Text = Utilities.ReplaceNothing(Episode.Director.Value)
        TextBox_Credits.Text = Utilities.ReplaceNothing(Episode.Credits.Value)
        TextBox_Aired.Text = Utilities.ReplaceNothing(Episode.Aired.Value)
        util_EpisodeSetWatched(Episode.PlayCount.Value)


        TextBox_Ep_Details.Text = "Video: " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Width.Value, "?") & "x" & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Height.Value, "?")
        TextBox_Ep_Details.Text += " (" & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Aspect.Value, "?") & ")"
        TextBox_Ep_Details.Text += " " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Codec.Value, "?")
        TextBox_Ep_Details.Text += " " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Video.Bitrate.Value, "?")

        If Episode.Details.StreamDetails.Audio.Count > 0 Then
            TextBox_Ep_Details.Text += "           Audio: " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Audio(0).Codec.Value, "?")
            TextBox_Ep_Details.Text += " " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Audio(0).Bitrate.Value, "?")
            TextBox_Ep_Details.Text += " " & Utilities.ReplaceNothing(Episode.Details.StreamDetails.Audio(0).Channels.Value, "?") & " channels"
        End If


        For Each actor In Episode.ListActors
            If actor.actorname <> Nothing Then
                ComboBox5.Items.Add(Utilities.ReplaceNothing(actor.actorname))
            End If
        Next
        If ComboBox5.Items.Count = 0 Then
            For Each actor In Episode.ListActors
                If actor.actorname <> Nothing Then
                    ComboBox5.Items.Add(Utilities.ReplaceNothing(actor.actorname))
                End If
            Next
        Else
            ComboBox5.SelectedIndex = 0
        End If

        'DISPLAY EPISODE ART - LEFT IS EPISODE SCREENSHOT RIGHT IS SEASON POSTER
        ' We need to do the following since we cannot rename the tbn whilst it is still showing in the picturebox
        ' It could have been why Billy has used two pictureboxes for each single one shown.....

        If (Episode IsNot Nothing AndAlso Episode.Thumbnail IsNot Nothing) Then
            util_ImageLoad(tv_PictureBoxLeft, Episode.Thumbnail.Path, defaultScreenShot)
        End If
        If (Season IsNot Nothing AndAlso Season.Poster IsNot Nothing) Then
            util_ImageLoad(tv_PictureBoxRight, Season.Poster.Path, defaultPoster) 'tv_PictureBoxRight.Image = Season.Poster.Image
        End If
        Panel9.Visible = True

    End Sub
    ' We need to load images in this way so that they remain unlocked by the OS so we can update the fanart/poster files as needed
    Public Shared Function util_ImageLoad(ByVal PicBox As PictureBox, ByVal ImagePath As String, ByVal DefaultPic As String) As Boolean
        Try
            If System.IO.File.Exists(ImagePath) Then
                Dim fs As System.IO.FileStream
                fs = New System.IO.FileStream(ImagePath, IO.FileMode.Open, IO.FileAccess.Read)
                PicBox.Image = System.Drawing.Image.FromStream(fs)
                fs.Close()
            Else
                PicBox.ImageLocation = DefaultPic
            End If
        Catch ex As Exception
            'possibly no file to load or file is corrupt
            PicBox.ImageLocation = DefaultPic
            Return False
        End Try
        Return True
    End Function
    Public Shared Sub util_EpisodeSetWatched(ByRef playcount As String, Optional ByVal toggle As Boolean = False)
        Dim watched As Boolean = False
        If IsNumeric(playcount) Then
            watched = Convert.ToInt32(playcount) > 0
            If toggle Then
                watched = Not watched
                playcount = If(watched, "1", "0")
            End If
        End If
        With Form1.Button48
            .Text = If(watched, "Watched", "Unwatched")
            .BackColor = If(watched, Color.LawnGreen, Color.Red)
        End With
    End Sub
    Private Sub tv_CacheRefresh(Optional ByVal TvShowSelected As TvShow = Nothing) 'refresh = clear & recreate cache from nfo's
        frmSplash2.Text = "Refresh TV Shows..."
        frmSplash2.Label1.Text = "Searching TV Folders....."
        frmSplash2.Label1.Visible = True
        frmSplash2.Label2.Visible = True
        frmSplash2.ProgressBar1.Visible = True
        frmSplash2.ProgressBar1.Maximum = Preferences.tvFolders.Count ' - 1
        frmSplash2.Show()
        Application.DoEvents()


        tv_RefreshLog("Starting TV Show Refresh" & vbCrLf & vbCrLf, , True)
        TextBox_TotTVShowCount.Text = ""
        TextBox_TotEpisodeCount.Text = ""
        Me.Enabled = False

        Dim prgCount As Integer = 0
        Dim FolderList As New List(Of String)
        If TvShowSelected IsNot Nothing Then ' if we have provided a tv show, then add just this show to the list, else scan through all of the folders
            FolderList.Add(TvShowSelected.FolderPath) 'add the single show to our list
            Cache.TvCache.Remove(TvShowSelected)
            For Each episode In TvShowSelected.Episodes
                Cache.TvCache.Remove(episode)
            Next
        Else
            FolderList = Preferences.tvFolders ' add all folders to list to scan
            Cache.TvCache.Clear() 'Full rescan means clear all old data
            TvTreeview.Nodes.Clear()
            realTvPaths.Clear()
        End If
        For Each tvfolder In FolderList
            frmSplash2.Label2.Text = "(" & prgCount + 1 & "/" & Preferences.tvFolders.Count & ") " & tvfolder
            frmSplash2.ProgressBar1.Value = prgCount   'range 0 to count -1
            If Not (Directory.Exists(tvfolder)) Then 'Temporary fix to skip any removed directory. Final fix should be capable of removing info from preferences file
                MessageBox.Show(String.Format("{0} could not found and will be skipped.", tvfolder), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Continue For
            End If

            prgCount += 1
            Application.DoEvents()
            Dim newtvshownfo As New TvShow
            newtvshownfo.NfoFilePath = IO.Path.Combine(tvfolder, "tvshow.nfo")
            newtvshownfo.Load(True)
            DirectCast(newtvshownfo.CacheDoc.FirstNode, System.Xml.Linq.XElement).FirstAttribute.Value = newtvshownfo.NfoFilePath
            If newtvshownfo.Title.Value IsNot Nothing Then
                If newtvshownfo.Status.Value Is Nothing OrElse (newtvshownfo.Status.Value IsNot Nothing AndAlso Not newtvshownfo.Status.Value.Contains("skipthisfile")) Then
                    If newtvshownfo.TvdbId.Value IsNot Nothing AndAlso newtvshownfo.TvdbId.Value.IndexOf("tt").Equals(0) Then
                        tv_IMDbID_detected = True
                        If Preferences.fixnfoid Then 'test if ID value should be fixed - i.e. IMDB value should be replaced by TVDB value
                            newtvshownfo.ImdbId.Value = newtvshownfo.TvdbId.Value
                            newtvshownfo.TvdbId.Value = newtvshownfo.IdTagCatch.Value
                            Call nfoFunction.tv_NfoSave(newtvshownfo.NfoFilePath, newtvshownfo, True) 'save the nfo with the new ID data
                            'Call tv_ShowLoad(newtvshownfo) ' reload the show to display..... SK: I think the current show will refresh anyway so this doesn't have to be called....
                        End If
                    End If

                    Cache.TvCache.Add(newtvshownfo) 'add this show & episode data to the cache
                End If
                ' TvTreeview.Nodes.Add(newtvshownfo.ShowNode) 'Instead of updating the treeview directly we reload the treeview with the created cache at the end....
            End If

            realTvPaths.Add(tvfolder)
        Next

        frmSplash2.Label2.Visible = False

        frmSplash2.Label1.Text = "Saving Cache..."
        Windows.Forms.Application.DoEvents()
        Tv_CacheSave()    'save the cache file
        frmSplash2.Label1.Text = "Loading Cache..."
        Windows.Forms.Application.DoEvents()
        tv_CacheLoad()    'reload the cache file to update the treeview
        If Preferences.fixnfoid Then CheckBox_fixNFOid.CheckState = CheckState.Unchecked
        Me.Enabled = True
        TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
        TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
        frmSplash2.Hide()
        If Not tv_IMDbID_warned And tv_IMDbID_detected Then
            MessageBox.Show(tv_IMDbID_detectedMsg, "TV Show ID", MessageBoxButtons.OK, MessageBoxIcon.Information)
            tv_IMDbID_warned = True
        End If
    End Sub

    Private Sub tv_ShowListLoad()
        If TextBox26.Text <> "" Then

            messbox = New frmMessageBox("Please wait,", "", "Getting possible TV Shows from TVDB")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()

            'Dim tvdbstuff As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
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
                    util_ImageLoad(PictureBox9, listOfShows(0).showbanner, defaultBanner)

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



    Private Function tv_TvShowTopGet(ByVal tvshowname As String)

        templanguage = Preferences.TvdbLanguageCode
        Try
            Dim tvdbstuff As New TVDBScraper

            Dim mirror As List(Of String) = tvdbstuff.getmirrors()
            Dim showslist As String = tvdbstuff.findshows(tvshowname, mirror(0))
            If showslist = "error" Then
                Return "error"
                Exit Function
            End If
            Dim newshows As New List(Of str_PossibleShowList)
            newshows.Clear()
            If showslist <> "none" Then
                Dim showlist As New XmlDocument
                showlist.LoadXml(showslist)
                Dim thisresult As XmlNode = Nothing
                For Each thisresult In showlist("allshows")
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
                            If lan.showtitle = tvshowname Then
                                If tv_LanguageCheck(lan.showid, templanguage) = True Then
                                    Return lan.showid
                                End If
                            End If
                            newshows.Add(lan)
                    End Select
                Next
                Dim returnid As String = ""
                If tv_LanguageCheck(newshows(0).showid, templanguage) = True Then
                    Return newshows(0).showid
                Else
                    If templanguage <> "en" Then
                        If tv_LanguageCheck(newshows(0).showid, "en") = True Then
                            templanguage = "en"
                            Return newshows(0).showid
                        Else
                            Return "nolang"
                        End If
                    Else
                        Return "nolang"
                    End If
                End If
            Else
                Return "none"
            End If
        Catch ex As Exception
            Return "error"
        End Try
    End Function

    Private Sub bckgrnd_tvshowscraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bckgrnd_tvshowscraper.ProgressChanged
        Try
            'If e.UserState = "i" Then
            ToolStripStatusLabel5.Text = "Scraping TV Shows, " & newTvFolders.Count & " remaining"
            ToolStripStatusLabel5.Visible = True
            'Call updatetree(False)

            Dim NewShow As TvShow = e.UserState


            'ListtvFiles(NewShow, "*.NFO")
            realTvPaths.Add(NewShow.FolderPath)
            TvTreeview.Nodes.Add(NewShow.ShowNode)
            NewShow.UpdateTreenode()
            
            TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
            TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
            Me.BringToFront()
            Me.Activate()
            ';
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try


    End Sub

    Private Sub bckgrnd_tvshowscraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bckgrnd_tvshowscraper.DoWork
        Try
            Dim speedy As Boolean = Preferences.tvshowautoquick
            Do While newTvFolders.Count > 0
                Dim NewShow As New TvShow
                NewShow.NfoFilePath = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")

                If NewShow.FileContainsReadableXml Then
                    NewShow.Load()

                    Cache.TvCache.Shows.Add(NewShow)
                    NewShow.SearchForEpisodesInFolder()
                    NewShow.UpdateTreenode()

                    If Not Preferences.tvFolders.Contains(newTvFolders(0)) Then
                        Preferences.tvFolders.Add(newTvFolders(0))
                    End If
                    bckgrnd_tvshowscraper.ReportProgress(0, NewShow)
                    newTvFolders.RemoveAt(0)

                    Continue Do
                End If


                Dim FolderName As String = Utilities.GetLastFolder(newTvFolders(0) & "\hifh")

                Dim showyear As String = ""
                If FolderName.Contains("(") Then
                    Dim M As Match
                    M = Regex.Match(FolderName, "(\([\d]{4}\))")
                    If M.Success = True Then
                        showyear = M.Value
                        showyear = showyear.Replace("(", "")
                        showyear = showyear.Replace(")", "")
                        showyear = showyear.Replace("{", "")
                        showyear = showyear.Replace("}", "")
                    End If
                    If FolderName.IndexOf("(") <> 0 Then
                        FolderName = FolderName.Substring(0, FolderName.IndexOf("("))
                        FolderName = FolderName.TrimEnd(" ")
                        If showyear <> "" Then
                            FolderName = FolderName & " (" & showyear & ")"
                        End If
                    End If
                End If
                NewShow.GetPossibleShows()
                Dim tvshowid As String
                If NewShow.PossibleShowList IsNot Nothing Then
                    If NewShow.PossibleShowList.Count = 0 Then
                        NewShow.FailedLoad = True
                        NewShow.State = Media_Companion.ShowState.Unverified
                        NewShow.Title.Value = FolderName
                        tvshowid = "none"
                        NewShow.State = ShowState.Error
                    ElseIf NewShow.PossibleShowList.Count = 1 Then
                        NewShow.State = Media_Companion.ShowState.Open

                        tvshowid = NewShow.PossibleShowList.Item(0).Id.Value
                    Else
                        Dim TempSeries As Tvdb.Series = Tvdb.FindBestPossibleShow(NewShow.PossibleShowList, FolderName, Preferences.TvdbLanguageCode)

                        If TempSeries.Similarity > 0.9 AndAlso
                            TempSeries.Language.Value = Preferences.TvdbLanguageCode Then
                            NewShow.State = Media_Companion.ShowState.Open
                        Else
                            NewShow.State = Media_Companion.ShowState.Unverified
                        End If

                        tvshowid = TempSeries.Id.Value
                    End If
                Else
                    tvshowid = "none"
                    NewShow.State = ShowState.Error
                End If
                'tvshowid = gettoptvshow(FolderName)

                If IsNumeric(tvshowid) Then
                    'tvshow found
                    Dim newtvshow As New TvShow
                    newtvshow.TvdbId.Value = tvshowid
                    newtvshow.NfoFilePath = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")

                    Dim tvdbstuff As New TVDBScraper

                    Dim posterurl As String = ""
                    Dim tempstring As String = ""

                    If templanguage = Nothing Then templanguage = "en"
                    If templanguage = "" Then templanguage = "en"
                    Dim SeriesInfo As Tvdb.ShowData = tvdbstuff.GetShow(tvshowid, templanguage, True)
                    If SeriesInfo.FailedLoad Then
                        MsgBox("Please adjust the TV Show title and try again", MsgBoxStyle.OkOnly, String.Format("'{0}' - No Show Returned", FolderName))
                        bckgrnd_tvshowscraper.ReportProgress(0, NewShow)
                        newTvFolders.RemoveAt(0)
                        Continue Do
                    End If
                    NewShow.AbsorbTvdbSeries(SeriesInfo.Series(0))


                    Dim TvdbActors As Tvdb.Actors = tvdbstuff.GetActors(tvshowid, templanguage)
                    For Each Act As Tvdb.Actor In TvdbActors.Items
                        If NewShow.ListActors.Count >= Preferences.maxactors Then
                            Exit For
                        End If

                        Dim NewAct As New Media_Companion.Actor
                        NewAct.ActorId = Act.Id
                        NewAct.actorname = Act.Name.Value
                        NewAct.actorrole = Act.Role.Value
                        NewAct.actorthumb = Act.Image.Value

                        If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 3 Or newtvshow.ImdbId = Nothing Then
                            Dim id As String = ""
                            'Dim acts As New MovieActors
                            Dim results As XmlNode = Nothing
                            Dim lan As New str_PossibleShowList(SetDefaults)


                            If Not String.IsNullOrEmpty(NewAct.actorthumb) Then
                                If NewAct.actorthumb <> "" And Preferences.actorseasy = True And speedy = False Then
                                    If NewShow.TvShowActorSource.Value <> "imdb" Or NewShow.ImdbId = Nothing Then
                                        Dim workingpath As String = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "")
                                        workingpath = workingpath & ".actors\"

                                        Utilities.EnsureFolderExists(workingpath)
                                        '**Commented out the following as fairly certain Utilities.EnsureFolderExists() replaces this - Huey
                                        'Dim hg As New IO.DirectoryInfo(workingpath)
                                        'Dim destsorted As Boolean = False
                                        'If Not hg.Exists Then

                                        '    IO.Directory.CreateDirectory(workingpath)
                                        '    destsorted = True

                                        'Else
                                        '    destsorted = True
                                        'End If
                                        'If destsorted = True Then
                                        Dim filename As String = Utilities.cleanFilenameIllegalChars(NewAct.actorname)
                                        filename = filename.Replace(" ", "_")
                                        filename = filename & ".tbn"
                                        filename = IO.Path.Combine(workingpath, filename)
                                        'Prepended the TVDb path as the API image path may have changed - hope this is across the board, tho'. Huey
                                        Utilities.DownloadFile("http://thetvdb.com/banners/_cache/" & NewAct.actorthumb, filename)
                                        'End If
                                    End If
                                End If
                                If Preferences.actorsave = True And id <> "" And Preferences.actorseasy = False Then
                                    Dim workingpath As String = ""
                                    Dim networkpath As String = Preferences.actorsavepath

                                    tempstring = networkpath & "\" & id.Substring(id.Length - 2, 2)
                                    Dim hg As New IO.DirectoryInfo(tempstring)
                                    If Not hg.Exists Then
                                        IO.Directory.CreateDirectory(tempstring)
                                    End If
                                    workingpath = networkpath & "\" & id.Substring(id.Length - 2, 2) & "\tv" & id & ".jpg"
                                    If Not IO.File.Exists(workingpath) Then
                                        Utilities.DownloadFile(NewAct.actorthumb, workingpath)
                                    End If
                                    NewAct.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, id.Substring(id.Length - 2, 2))
                                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                        NewAct.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, id.Substring(id.Length - 2, 2) & "/tv" & id & ".jpg")
                                    Else
                                        NewAct.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, id.Substring(id.Length - 2, 2) & "\tv" & id & ".jpg")
                                    End If


                                End If
                            End If
                            Dim exists As Boolean = False
                            For Each actors In NewShow.ListActors
                                If actors.actorname = NewAct.actorname And actors.actorrole = NewAct.actorrole Then
                                    exists = True
                                    Exit For
                                End If
                            Next
                            If exists = False Then
                                NewShow.ListActors.Add(NewAct)
                            End If
                        End If





                    Next

                    If Preferences.TvdbActorScrape = 1 Or Preferences.TvdbActorScrape = 2 And newtvshow.ImdbId <> Nothing Then
                        Dim imdbscraper As New Classimdb
                        Dim actorlist As String
                        Dim actorstring As New XmlDocument
                        actorlist = imdbscraper.getimdbactors(Preferences.imdbmirror, newtvshow.ImdbId.Value)

                        actorstring.LoadXml(actorlist)

                        For Each thisresult As XmlNode In actorstring("actorlist")
                            Select Case thisresult.Name
                                Case "actor"
                                    Dim newactor As New str_MovieActors(SetDefaults)
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
                                                    If Preferences.actorsave = True And detail.InnerText <> "" And speedy = False Then
                                                        Dim workingpath As String = ""
                                                        Dim networkpath As String = Preferences.actorsavepath

                                                        tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                                        Dim hg As New IO.DirectoryInfo(tempstring)
                                                        If Not hg.Exists Then
                                                            IO.Directory.CreateDirectory(tempstring)
                                                        End If
                                                        workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                        If Not IO.File.Exists(workingpath) Then
                                                            Utilities.DownloadFile(newactor.actorthumb, workingpath)
                                                        End If
                                                        newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                        If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                            newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                        Else
                                                            newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                        End If

                                                    End If
                                                End If
                                        End Select
                                    Next
                                    newtvshow.ListActors.Add(newactor)
                            End Select
                        Next
                        While newtvshow.ListActors.Count > Preferences.maxactors
                            newtvshow.ListActors.RemoveAt(newtvshow.ListActors.Count - 1)
                        End While

                    End If

                    'Dim showlist As New XmlDocument
                    'Dim ArtList As New List(Of TvBanners)
                    'Dim artdone As Boolean = False
                    'If Preferences.tvfanart = True Or Preferences.tvposter = True Or Preferences.seasonall <> "none" Then
                    '    Dim thumblist As Tvdb.Banners = tvdbstuff.GetPosterList(newtvshow.tvdbid, True)
                    '    'showlist.LoadXml(thumblist)
                    '    artdone = True

                    '    For Each Item As Tvdb.Banner In thumblist.Items
                    '        Dim NewItem As New TvBanners
                    '        NewItem = Item
                    '        ArtList.Add(NewItem)
                    '    Next
                    'End If
                    Dim ArtList As Tvdb.Banners = tvdbstuff.GetPosterList(newtvshow.TvdbId.Value, True)
                    If Not speedy AndAlso (Preferences.tvfanart = True OrElse Preferences.tvposter = True OrElse Preferences.seasonall <> "none") Then
                        If Preferences.downloadtvseasonthumbs = True Then
                            For f = 0 To ArtList.Items.SeasonMax
                                Dim seasonposter As String = ""
                                For Each Image In ArtList.Items
                                    If Image.Season.Value = f.ToString And Image.Language.Value = templanguage Then
                                        seasonposter = Image.Url
                                        Exit For
                                    End If
                                Next
                                If seasonposter = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Season.Value = f.ToString And Image.Language.Value = "en" Then
                                            seasonposter = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If seasonposter = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Season.Value = f.ToString Then
                                            seasonposter = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If seasonposter <> "" Then
                                    If f < 10 Then
                                        tempstring = "0" & f.ToString
                                    Else
                                        tempstring = f.ToString
                                    End If
                                    Dim seasonpath As String = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "season" & tempstring & ".tbn")
                                    If tempstring = "00" Then
                                        seasonpath = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "season-specials.tbn")
                                    End If
                                    If Not IO.File.Exists(seasonpath) Then

                                        Utilities.DownloadFile(seasonposter, seasonpath)

                                    End If
                                End If
                            Next
                        End If

                        If Preferences.tvfanart = True Then
                            Dim fanartposter As String
                            fanartposter = ""
                            If CheckBox7.CheckState = CheckState.Checked Then
                                For Each Image In ArtList.Items
                                    If Image.Language.Value = templanguage And Image.Type = Tvdb.ArtType.Fanart Then
                                        fanartposter = Image.Url
                                        Exit For
                                    End If
                                Next
                            End If
                            If fanartposter = "" Then
                                For Each Image In ArtList.Items
                                    If Image.Language.Value = "en" And Image.Type = Tvdb.ArtType.Fanart Then
                                        fanartposter = Image.Url
                                        Exit For
                                    End If
                                Next
                            End If
                            If fanartposter = "" Then
                                For Each Image In ArtList.Items
                                    If Image.Type = Tvdb.ArtType.Fanart Then
                                        fanartposter = Image.Url
                                        Exit For
                                    End If
                                Next
                            End If
                            If fanartposter <> "" Then

                                Dim seasonpath As String = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "fanart.jpg")
                                If Not IO.File.Exists(seasonpath) Then
                                    Utilities.DownloadFile(fanartposter, seasonpath)
                                End If
                            End If
                        End If


                        Dim seasonallpath As String = ""
                        If Preferences.tvposter = True Then
                            Dim posterurlpath As String = ""

                            If Preferences.postertype = "poster" Then 'poster
                                For Each Image In ArtList.Items
                                    If Image.Language.Value = templanguage And Image.Type = Tvdb.ArtType.Poster Then
                                        posterurl = Image.Url
                                        Exit For
                                    End If
                                Next
                                If posterurlpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Language.Value = "en" And Image.Type = Tvdb.ArtType.Poster Then
                                            posterurlpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If posterurlpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Type = Tvdb.ArtType.Poster Then
                                            posterurlpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If posterurlpath <> "" And Preferences.seasonall <> "none" Then
                                    seasonallpath = posterurlpath
                                End If
                            ElseIf Preferences.postertype = "banner" Then 'banner
                                For Each Image In ArtList.Items
                                    If Image.Language.Value = templanguage And Image.Type = Tvdb.ArtType.Banner Then
                                        posterurl = Image.Url
                                        Exit For
                                    End If
                                Next
                                If posterurlpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Language.Value = "en" And Image.Type = Tvdb.ArtType.Banner Then
                                            posterurlpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If posterurlpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Type = Tvdb.ArtType.Banner Then
                                            posterurlpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If posterurlpath <> "" And RadioButton16.Checked = True Then
                                    seasonallpath = posterurlpath
                                End If
                            End If

                            If posterurlpath <> "" And speedy = False Then

                                Dim seasonpath As String = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "folder.jpg")
                                If Not IO.File.Exists(seasonpath) Then

                                    Utilities.DownloadFile(posterurlpath, seasonpath)

                                End If
                            End If
                        End If

                        If Preferences.seasonall <> "none" And seasonallpath = "" Then
                            If Preferences.seasonall = "poster" Then 'poster
                                For Each Image In ArtList.Items
                                    If Image.Language.Value = templanguage And Image.Type = Tvdb.ArtType.Poster Then
                                        seasonallpath = Image.Url
                                        Exit For
                                    End If
                                Next
                                If seasonallpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Language.Value = "en" And Image.Type = Tvdb.ArtType.Poster Then
                                            seasonallpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If seasonallpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Type = Tvdb.ArtType.Poster Then
                                            seasonallpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                            ElseIf Preferences.seasonall = "wide" = True Then 'banner
                                For Each Image In ArtList.Items
                                    If Image.Language.Value = templanguage And Image.Type = Tvdb.ArtType.Banner Then
                                        seasonallpath = Image.Url
                                        Exit For
                                    End If
                                Next
                                If seasonallpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Language.Value = "en" And Image.Type = Tvdb.ArtType.Banner Then
                                            seasonallpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                                If seasonallpath = "" Then
                                    For Each Image In ArtList.Items
                                        If Image.Type = Tvdb.ArtType.Banner Then
                                            seasonallpath = Image.Url
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If

                            If seasonallpath <> "" Then

                                Dim seasonpath As String = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "season-all.tbn")
                                If Not IO.File.Exists(seasonpath) Or CheckBox6.CheckState = CheckState.Checked Then

                                    Utilities.DownloadFile(seasonallpath, seasonallpath)

                                End If
                            End If
                        ElseIf Preferences.seasonall <> "none" And seasonallpath <> "" Then
                            Dim seasonpath As String = newtvshow.NfoFilePath.Replace(IO.Path.GetFileName(newtvshow.NfoFilePath), "season-all.tbn")
                            If Not IO.File.Exists(seasonpath) Then
                                Utilities.DownloadFile(seasonallpath, seasonpath)
                            End If
                        End If
                    End If
                    'If artdone = False Then
                    '    Dim thumblist As String = tvdbstuff.GetPosterList(newtvshow.tvdbid)
                    '    showlist.LoadXml(thumblist)
                    '    artdone = True
                    '    'CheckBox3 = seasons
                    '    'CheckBox4 = fanart
                    '    'CheckBox5 = poster
                    '    For Each thisresult As XmlNode In showlist("banners")
                    '        Select Case thisresult.Name
                    '            Case "banner"
                    '                Dim individualposter As New TvBanners
                    '                For Each results In thisresult.ChildNodes
                    '                    Select Case results.Name
                    '                        Case "url"
                    '                            individualposter.url = results.InnerText
                    '                        Case "bannertype"
                    '                            individualposter.bannerType = results.InnerText
                    '                        Case "resolution"
                    '                            individualposter.resolution = results.InnerText
                    '                        Case "language"
                    '                            individualposter.language = results.InnerText
                    '                        Case "season"
                    '                            individualposter.season = results.InnerText
                    '                    End Select
                    '                Next
                    '                artlist.Add(individualposter)
                    '        End Select
                    '    Next
                    'End If

                    For Each url In ArtList.Items
                        If url.Type = Tvdb.ArtType.Fanart Then
                            newtvshow.posters.Add(url.Url)
                        Else
                            newtvshow.fanart.Add(url.Url)
                        End If
                    Next

                    'newtvshow.language = Preferences.tvdblanguagecode
                    If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 2 Then
                        newtvshow.EpisodeActorSource.Value = "tvdb"
                    Else
                        newtvshow.EpisodeActorSource.Value = "imdb"
                    End If
                    If Preferences.TvdbActorScrape = 0 Or Preferences.TvdbActorScrape = 3 Then
                        newtvshow.TvShowActorSource.Value = "tvdb"
                    Else
                        newtvshow.TvShowActorSource.Value = "imdb"
                    End If

                    If tempstring = "0" Or tempstring = "2" Then
                        newtvshow.EpisodeActorSource.Value = "tvdb"
                    Else
                        newtvshow.EpisodeActorSource.Value = "imdb"
                    End If

                    newtvshow.SortOrder.Value = Preferences.sortorder

                    'nfoFunction.savetvshownfo(newtvshow.path, newtvshow, True)

                End If
                'DownloadMissingArt(NewShow)
                NewShow.Save()
                NewShow.UpdateTreenode()
                'Cache.TvCache.Shows.Add(NewShow)
                Cache.TvCache.Add(NewShow)
                NewShow.SearchForEpisodesInFolder()
                If Not Preferences.tvFolders.Contains(newTvFolders(0)) Then
                    Preferences.tvFolders.Add(newTvFolders(0))
                End If
                bckgrnd_tvshowscraper.ReportProgress(0, NewShow)
                newTvFolders.RemoveAt(0)
            Loop

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub TV_EpisodeScraper(ByVal ListOfShows As List(Of TvShow), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim errorcounter As Integer = 0
        newEpisodeList.Clear()
        Dim newtvfolders As New List(Of String)
        Dim progress As Integer
        progress = 0
        Dim progresstext As String = String.Empty
        Preferences.tvScraperLog = ""
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
                    'Continue For
                End If
            End If

            If Add = True Then
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
                    newtvfolders.AddRange(ExtraFolder)
                    'newtvfolders.Add(TvFolder)
                    For Each Item In ExtraFolder
                        Preferences.tvScraperLog &= "Subfolder added :- " & Item.ToString & vbCrLf
                    Next
                End If
            Else
                Preferences.tvScraperLog &= vbCrLf & "Show Locked, Ignoring: " & TvFolder & vbCrLf
            End If
        Next

        scraperLog = scraperLog & vbCrLf
        'Application.DoEvents()
        Dim mediacounter As Integer = newEpisodeList.Count
        newtvfolders.Sort()
        For g = 0 To newtvfolders.Count - 1
            'Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
            'bckgroundscanepisodes.ReportProgress(progress, progresstext)
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog &= vbCrLf & "!!! Operation cancelled by user"
                Exit Sub
            End If
            progresstext = String.Concat("Stage 2 of 3 : Found " & newEpisodeList.Count & " : Searching for New Episodes in Folders " & g + 1 & " of " & newtvfolders.Count & " - '" & newtvfolders(g) & "'")
            bckgroundscanepisodes.ReportProgress(progress, progresstext)
            For Each f In Utilities.VideoExtensions
                'If bckgroundscanepisodes.CancellationPending Then
                '    Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation cancelled by user"
                '    Exit Sub
                'Preferences.tvScraperLog &= vbCrLf & "Operation cancelled by user"
                'moviepattern = f
                dirpath = newtvfolders(g)
                Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                tv_NewFind(dirpath, f)
            Next f
            tempint = newEpisodeList.Count - mediacounter

            Preferences.tvScraperLog &= tempint.ToString & " New episodes found in directory " & dirpath & vbCrLf
            mediacounter = newEpisodeList.Count
        Next g

        Preferences.tvScraperLog &= vbCrLf
        If newEpisodeList.Count <= 0 Then
            Preferences.tvScraperLog &= "!!! No new episodes found, exiting scraper." & vbCrLf
            Exit Sub
        End If

        Dim S As String = ""
        For Each newepisode In newEpisodeList

            S = ""

            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                Exit Sub
            End If
            Dim episode As New TvEpisode

            For Each Regexs In tv_RegexScraper

                S = newepisode.VideoFilePath '.ToLower
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
                        newepisode.Season.Value = M.Groups(1).Value.ToString
                        newepisode.Episode.Value = M.Groups(2).Value.ToString

                        Try
                            newepisode.Thumbnail.FileName = S.Substring(M.Groups(2).Index + M.Groups(2).Value.Length, S.Length - (M.Groups(2).Index + M.Groups(2).Value.Length))
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

            If eps.Season.Value <> "-1" And eps.Episode.Value <> "-1" Then
                Preferences.tvScraperLog &= "Season : " & eps.Season.Value & vbCrLf
                Preferences.tvScraperLog &= "Episode: " & eps.Episode.Value & vbCrLf
            Else
                Preferences.tvScraperLog &= "!!! WARNING: Can't extract Season and Episode details from filename: " & vbCrLf & vbCrLf
                Continue For    'if we can't get season or episode then skip to next episode
            End If

            tempTVDBiD = ""
            Dim episodearray As New List(Of TvEpisode)
            episodearray.Clear()
            'Dim multieps2 As New TvEpisode
            'multieps2.Season.Value = eps.Season.Value
            'multieps2.Episode.Value = eps.Episode.Value
            'multieps2.VideoFilePath = eps.VideoFilePath
            'multieps2.MediaExtension = eps.MediaExtension
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

            progresstext = String.Concat("Stage 3 of 3 : Scraping New Episodes : Using " & WhichScraper & "Scraper : Scraping " & epscount & " of " & newEpisodeList.Count & " - '" & IO.Path.GetFileName(eps.VideoFilePath) & "'")
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
                S = eps.Thumbnail.FileName
                eps.Thumbnail.FileName = ""
                Do
                    'S = temppath '.ToLower
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
                Dim language As String = ""
                Dim sortorder As String = ""
                Dim tvdbid As String = ""
                Dim imdbid As String = ""
                Dim actorsource As String = ""
                Dim realshowpath As String = ""

                savepath = episodearray(0).NfoFilePath
                Dim EpisodeName As String = ""
                For Each Shows In Cache.TvCache.Shows
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    If episodearray(0).NfoFilePath.IndexOf(Shows.NfoFilePath.Replace("tvshow.nfo", "")) <> -1 Then
                        language = Shows.Language.Value
                        sortorder = Shows.SortOrder.Value
                        tvdbid = Shows.TvdbId.Value
                        tempTVDBiD = Shows.TvdbId.Value
                        imdbid = Shows.ImdbId.Value
                        showtitle = Shows.Title.Value
                        EpisodeName = Shows.Title.Value
                        realshowpath = Shows.NfoFilePath
                        actorsource = Shows.EpisodeActorSource.Value
                    End If
                Next
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
                Preferences.tvScraperLog &= "Looking up scraper options from tvshow.nfo" & vbCrLf

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
                    'Dim episodescraper As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
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
                        'Preferences.tvScraperLog &= "Trying Episode URL: " & episodeurl & vbCrLf
                        If Not util_UrlIsValid(episodeurl) Then
                            If sortorder.ToLower = "dvd" Then
                                tempsortorder = "default"
                                Preferences.tvScraperLog &= "!!! WARNING: This episode could not be found on TVDB using DVD sort order" & vbCrLf
                                Preferences.tvScraperLog &= "!!! Attempting to find using default sort order" & vbCrLf
                                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.Season.Value & "/" & singleepisode.Episode.Value & "/" & language & ".xml"
                                Preferences.tvScraperLog &= "Now Trying Episode URL: " & episodeurl & vbCrLf
                            End If
                        End If

                        If util_UrlIsValid(episodeurl) Then


                            If Preferences.tvshow_useXBMC_Scraper = True Then
                                Dim FinalResult As String = ""
                                episodearray = XBMCScrape_TVShow_EpisodeDetails(tvdbid, tempsortorder, episodearray, language)
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

                            'Dim tempepisode As String = episodescraper.getepisode(tvdbid, tempsortorder, singleepisode.Season.value, singleepisode.episodeno, language)
                            Dim tempepisode As String = ep_Get(tvdbid, tempsortorder, singleepisode.Season.Value, singleepisode.Episode.Value, language)
                            scrapedok = True

                            '                            Exit For
                            If tempepisode = Nothing Or tempepisode = "Error" Then
                                scrapedok = False
                                Preferences.tvScraperLog &= "!!! WARNING: This episode could not be found on TVDB" & vbCrLf
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
                                        Case "thumb"
                                            singleepisode.Thumbnail.FileName = thisresult.InnerText
                                        Case "actor"
                                            For Each actorl As XmlNode In thisresult.ChildNodes
                                                Select Case actorl.Name
                                                    Case "name"
                                                        Dim newactor As New str_MovieActors(SetDefaults)
                                                        newactor.actorname = actorl.InnerText
                                                        singleepisode.ListActors.Add(newactor)
                                                End Select
                                            Next
                                    End Select
                                Next
                                singleepisode.PlayCount.Value = "0"

                                progresstext &= " : Scraped Title - '" & singleepisode.Title.Value & "'"
                                bckgroundscanepisodes.ReportProgress(progress, progresstext)

                                If actorsource = "imdb" Then
                                    Preferences.tvScraperLog &= "Scraping actors from IMDB" & vbCrLf
                                    progresstext &= " : Actors..."
                                    bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                    Dim url As String
                                    url = "http://www.imdb.com/title/" & imdbid & "/episodes"
                                    Dim tvdbsLine As String = ""
                                    tvdbsLine = Utilities.DownloadTextFiles(url)

                                    If tvdbsLine <> "" Then
                                        Dim tvtempstring As String
                                        tvtempstring = "Season " & singleepisode.Season.Value & ", Episode " & singleepisode.Episode.Value & ":"
                                        'For g = 1 To tvfblinecount
                                        If tvdbsLine.IndexOf(tvtempstring) <> -1 Then
                                            Dim tvtempint As Integer
                                            tvtempint = tvdbsLine.IndexOf("<a href=""/title/")
                                            If tvtempint <> -1 Then
                                                tvtempstring = tvdbsLine.Substring(tvtempint + 16, 9)
                                                '            Dim scraperfunction As New imdb.Classimdbscraper ' add to comment this one because of changes i made to the Class "Scraper" (ClassimdbScraper)
                                                Dim scraperfunction As New Classimdb
                                                Dim actorlist As String = ""
                                                actorlist = scraperfunction.getimdbactors(Preferences.imdbmirror, tvtempstring, , Preferences.maxactors)
                                                Dim tempactorlist As New List(Of str_MovieActors)
                                                Dim thumbstring As New XmlDocument


                                                thumbstring.LoadXml(actorlist)

                                                Dim countactors As Integer = 0
                                                For Each thisresult As XmlNode In thumbstring("actorlist")
                                                    If bckgroundscanepisodes.CancellationPending Then
                                                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                                                        Exit Sub
                                                    End If
                                                    Select Case thisresult.Name
                                                        Case "actor"
                                                            If countactors >= Preferences.maxactors Then
                                                                Exit For
                                                            End If
                                                            countactors += 1
                                                            Dim newactor As New str_MovieActors(SetDefaults)

                                                            For Each detail As XmlNode In thisresult.ChildNodes
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
                                                                                Dim workingpath As String = episodearray(0).NfoFilePath.Replace(IO.Path.GetFileName(episodearray(0).NfoFilePath), "")
                                                                                workingpath = workingpath & ".actors\"
                                                                                Dim hg As New IO.DirectoryInfo(workingpath)
                                                                                Dim destsorted As Boolean = False
                                                                                If Not hg.Exists Then

                                                                                    IO.Directory.CreateDirectory(workingpath)
                                                                                    destsorted = True

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

                                                                                        IO.File.Copy(tvshowactorpath, filename, True)

                                                                                    End If
                                                                                    If Not IO.File.Exists(filename) Then
                                                                                        Utilities.DownloadFile(newactor.actorthumb, filename)
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            If Preferences.actorsave = True And detail.InnerText <> "" And Preferences.actorseasy = False Then
                                                                                Dim workingpath As String = ""
                                                                                Dim networkpath As String = Preferences.actorsavepath

                                                                                tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                                                                Dim hg As New IO.DirectoryInfo(tempstring)
                                                                                If Not hg.Exists Then
                                                                                    IO.Directory.CreateDirectory(tempstring)
                                                                                End If
                                                                                workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                                                If Not IO.File.Exists(workingpath) Then
                                                                                    Utilities.DownloadFile(newactor.actorthumb, workingpath)
                                                                                End If
                                                                                newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                                                If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                                                    newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                                                Else
                                                                                    newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                                                End If

                                                                            End If
                                                                        End If
                                                                End Select
                                                                If bckgroundscanepisodes.CancellationPending Then
                                                                    Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                                                                    Exit Sub
                                                                End If
                                                            Next
                                                            tempactorlist.Add(newactor)
                                                    End Select
                                                    If bckgroundscanepisodes.CancellationPending Then
                                                        Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                                                        Exit Sub
                                                    End If
                                                Next





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

                                                'Exit For
                                            End If
                                        End If
                                        If bckgroundscanepisodes.CancellationPending Then
                                            Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                                            Exit Sub
                                        End If
                                        'Next

                                    Else
                                        'tvscraperlog = tvscraperlog & "Unable To Get Actors From IMDB, Defaulting To TVDB" & vbCrLf
                                    End If
                                End If



                                If Preferences.enablehdtags = True Then
                                    progresstext &= " : HD Tags..."
                                    bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                    Dim fileStreamDetails As FullFileDetails = Preferences.Get_HdTags(Utilities.GetFileName(singleepisode.VideoFilePath))
                                    singleepisode.Details.StreamDetails.Video = fileStreamDetails.filedetails_video
                                    For Each audioStream In fileStreamDetails.filedetails_audio
                                        singleepisode.Details.StreamDetails.Audio.Add(audioStream)
                                    Next

                                    If Not singleepisode.Details.StreamDetails.Video.DurationInSeconds.Value Is Nothing Then
                                        '1h 24mn 48s 546ms
                                        Dim hours As Integer = 0
                                        Dim minutes As Integer = 0
                                        tempstring = singleepisode.Details.StreamDetails.Video.DurationInSeconds.Value
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
                                            minutes += hours * 60
                                        End If
                                        minutes = minutes + hours
                                        singleepisode.Runtime.Value = minutes.ToString & " min"
                                        progresstext &= "OK."
                                        bckgroundscanepisodes.ReportProgress(progress, progresstext)
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



                '' Commented out the lines below so that the episodes are renamed irrelavent of scraper (MC or XBMC scraper) - not sure of their purpose. 
                'If Preferences.tvshow_useXBMC_Scraper = True Then
                '    newnamepath = savepath
                'Else
                newnamepath = ep_add(episodearray, savepath, showtitle)
                ''9999999                                                               'This was already commented out, it must be a note of some sort.
                For Each ep In episodearray
                    ep.NfoFilePath = newnamepath
                Next
                'End If
                'bckgroundscanepisodes.ReportProgress(9999999, episodearray)
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "!!! Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                For Each Shows In Cache.TvCache.Shows
                    If episodearray(0).NfoFilePath.IndexOf(Shows.NfoFilePath.Replace("\tvshow.nfo", "")) <> -1 Then
                        'workingtvshow = nfoFunction.loadfulltnshownfo(Shows.fullpath)
                        For Each ept In episodearray
                            Dim list = Shows.MissingEpisodes
                            For j = list.Count - 1 To 0 Step -1
                                If list(j).Title = ept.Title Then
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
                    End If
                Next
            End If
            Preferences.tvScraperLog &= "!!!" & vbCrLf
        Next

        bckgroundscanepisodes.ReportProgress(0, progresstext)

    End Sub


    'Private Sub TV_AddTvshowToTreeview(ByVal fullpath As String, ByVal title As String, Optional ByVal xmlerror As Boolean = False, Optional ByVal locked As Boolean = True)
    '    If xmlerror = True Then
    '        TvTreeview.Nodes.Add(fullpath, title)
    '        For Each tn As TreeNode In TvTreeview.Nodes
    '            If tn.Name = fullpath Then
    '                If locked = True Or locked = 2 Then tn.StateImageIndex = 0
    '                tn.ForeColor = Color.Red
    '            End If
    '        Next
    '    Else
    '        TvTreeview.Nodes.Add(fullpath, title)
    '        For Each tn As TreeNode In TvTreeview.Nodes
    '            If tn.Name = fullpath Then
    '                tn.ForeColor = Color.Black
    '                If locked = True Or locked = 2 Then tn.StateImageIndex = 0
    '            End If
    '        Next
    '    End If
    'End Sub

    'Private Sub TV_AddEpisodeToTreeview(ByVal rootnode As Integer, ByVal childnode As Integer, ByVal fullpath As String, ByVal title As String, Optional ByVal xmlerror As Boolean = False)
    '        Try
    '            Dim ccnode As TreeNode
    '            ccnode = TvTreeview.Nodes(rootnode).Nodes(childnode)
    '            For Each nod In ccnode.Nodes
    '                If nod.text = title Then
    '                    ccnode.Nodes.Remove(nod)
    '                    Exit For
    '                End If
    '            Next
    '            ccnode.Nodes.Add(fullpath, title)

    '            If xmlerror = True Then
    '                For Each no As TreeNode In ccnode.Nodes
    '                    If no.Name = fullpath Then
    '                        no.ForeColor = Color.Red
    '                        Exit For
    '                    End If
    '                Next
    '            Else
    '                For Each no As TreeNode In ccnode.Nodes
    '                    If no.Name = fullpath Then
    '                        no.ForeColor = Color.Black
    '                        Exit For
    '                    End If
    '                Next
    '            End If
    '            'TvTreeview.Nodes.Remove(node)
    '        Catch ex As Exception
    '#If SilentErrorScream Then
    '            Throw ex
    '#End If
    '            'MsgBox(ex.ToString)
    '        End Try
    '    End Sub

    'Private Sub TV_PopulateTvTree()
    '    Dim tempint As Integer
    '    Dim tempstring As String = String.Empty
    '    Dim cnode As TreeNode = Nothing

    '    ComboBox4.Items.Clear()
    '    ComboBox4.Text = String.Empty


    '    PictureBox6.Image = Nothing
    '    PictureBox4.Image = Nothing
    '    PictureBox5.Image = Nothing
    '    TextBox10.Text = String.Empty
    '    TextBox11.Text = String.Empty
    '    TextBox9.Text = String.Empty
    '    TextBox12.Text = String.Empty
    '    TextBox13.Text = String.Empty
    '    TextBox14.Text = String.Empty
    '    TextBox15.Text = String.Empty
    '    TextBox16.Text = String.Empty
    '    TextBox18.Text = String.Empty
    '    TextBox19.Text = String.Empty
    '    If Not workingTvShow Is Nothing Then workingTvShow.path = String.Empty
    '    ComboBox4.Items.Clear()
    '    ComboBox4.Text = String.Empty
    '    TextBox20.Text = String.Empty
    '    TextBox21.Text = String.Empty
    '    TextBox22.Text = String.Empty
    '    TextBox23.Text = String.Empty
    '    TextBox24.Text = String.Empty
    '    TextBox25.Text = String.Empty
    '    ComboBox5.Items.Clear()
    '    ComboBox5.Text = String.Empty
    '    Panel9.Visible = False
    '    TextBox2.Text = String.Empty
    '    totalTvShowCount = 0
    '    totalEpisodeCount = 0
    '    TvTreeview.Nodes.Clear()

    '    For Each item In TvShows
    '        totalTvShowCount += 1
    '        Dim shownode As Integer = -1

    '        If item.status IsNot Nothing AndAlso Not item.status.ToLower.Contains("xml error") Then
    'Call TV_AddTvshowToTreeview(item.fullpath, item.title, True, item.locked)
    ''        Else
    'Call TV_AddTvshowToTreeview(item.fullpath, item.title, False, item.locked)
    ''        End If


    '        For Each episode In item.allepisodes
    '            totalEpisodeCount += 1

    '            Dim seasonno As Integer = -10
    '            seasonno = Convert.ToInt32(episode.Season.value)

    '            For g = 0 To TvTreeview.Nodes.Count - 1
    '                If TvTreeview.Nodes(g).Name.ToString = item.fullpath Then
    '                    cnode = TvTreeview.Nodes(g)
    '                    shownode = g
    '                    Exit For
    '                End If
    '            Next

    '            Dim seasonstring As String = Nothing

    '            If seasonno <> 0 And seasonno <> -1 Then
    '                If seasonno < 10 Then
    '                    tempstring = "Season 0" & seasonno.ToString
    '                Else
    '                    tempstring = "Season " & seasonno.ToString
    '                End If
    '            ElseIf seasonno = 0 Then
    '                tempstring = "Specials"
    '            End If

    '            Dim node As TreeNode
    '            Dim alreadyexists As Boolean = False
    '            For Each node In cnode.Nodes
    '                If node.Text = tempstring Then
    '                    alreadyexists = True
    '                    Exit For
    '                End If
    '            Next
    '            If alreadyexists = False Then cnode.Nodes.Add(tempstring)

    '            For Each node In cnode.Nodes
    '                If node.Text = tempstring Then
    '                    tempint = node.Index
    '                    Exit For
    '                End If
    '            Next

    '            Dim eps As String
    '            If episode.episodeno < 10 Then
    '                eps = "0" & episode.episodeno.ToString
    '            Else
    '                eps = episode.episodeno.ToString
    '            End If

    '            eps = eps & " - " & episode.title
    '            If episode.imdbid = Nothing Then
    '                episode.imdbid = ""
    '            End If

    ''            If episode.imdbid.ToLower.IndexOf("xml error") <> -1 Then
    'Call TV_AddEpisodeToTreeview(shownode, tempint, episode.VideoFilePath, eps, True)
    ''            Else
    'Call TV_AddEpisodeToTreeview(shownode, tempint, episode.VideoFilePath, eps, False)
    ''            End If

    '        Next

    '        For Each missingep In item.missingepisodes
    '            For g = 0 To TvTreeview.Nodes.Count - 1
    '                If TvTreeview.Nodes(g).Name.ToString = item.fullpath Then
    '                    cnode = TvTreeview.Nodes(g)
    '                    shownode = g
    '                    Exit For
    '                End If
    '            Next

    '            Dim seasonstring As String = Nothing
    '            Dim seasonno As Integer = Convert.ToInt32(missingep.Season.value)
    '            If seasonno <> 0 And seasonno <> -1 Then
    '                If seasonno < 10 Then
    '                    tempstring = "Season 0" & seasonno.ToString
    '                Else
    '                    tempstring = "Season " & seasonno.ToString
    '                End If
    '            ElseIf seasonno = 0 Then
    '                tempstring = "Specials"
    '            End If

    '            Dim node As TreeNode
    '            Dim alreadyexists As Boolean = False
    '            For Each node In cnode.Nodes
    '                If node.Text = tempstring Then
    '                    alreadyexists = True
    '                    Exit For
    '                End If
    '            Next

    '            If alreadyexists = False Then cnode.Nodes.Add(tempstring)
    '            For Each node In cnode.Nodes
    '                If node.Text = tempstring Then
    '                    tempint = node.Index
    '                    Exit For
    '                End If
    '            Next

    '            Dim eps As String
    '            Dim episodeno As Integer = Convert.ToInt32(missingep.episodeno)
    '            If episodeno < 10 Then
    '                eps = "0" & episodeno.ToString
    '            Else
    '                eps = episodeno.ToString
    '            End If

    '            eps = eps & " - " & missingep.title
    '            Dim ccnode As TreeNode
    '            ccnode = TvTreeview.Nodes(shownode).Nodes(tempint)
    '            Dim tempstring2 As String = "Missing: " & eps
    '            ccnode.Nodes.Add(tempstring2, eps)

    '            For Each no As TreeNode In ccnode.Nodes
    '                If no.Name = tempstring2 Then
    '                    no.ForeColor = Color.Blue
    '                    no.Parent.ForeColor = Color.Blue
    '                    no.Parent.Parent.ForeColor = Color.Blue
    '                    Exit For
    '                End If
    '            Next
    '        Next
    '    Next



    '    Dim MyNode As TreeNode
    '    If Not TvTreeview.Nodes.Count = 0 Then
    '        MyNode = TvTreeview.Nodes(0) 'First Level
    '        'MyNode = MyNode.Nodes(6)  ' Second Level
    '        TvTreeview.SelectedNode = MyNode
    '        TabLevel1.Focus()
    '        TabControl3.Focus()
    '        TvTreeview.Focus()
    '    End If
    '    TvTreeview.Refresh()
    '    TvTreeview.CollapseAll()

    '    TextBox32.Text = totalTvShowCount.ToString
    '    TextBox33.Text = totalEpisodeCount.ToString
    'End Sub

    'Private Sub ListtvFiles(ByVal tvshow As TvShow, ByVal pattern As String)

    '    Dim episode As New List(Of TvEpisode)
    '    Dim propfile As Boolean = False
    '    Dim allok As Boolean = False


    '    Dim newlist As New List(Of String)
    '    newlist.Clear()

    '    newlist = Utilities.EnumerateFolders(tvshow.FolderPath, 6) 'TODO: Restore loging functions

    '    newlist.Insert(0, tvshow.FolderPath)
    '    If newlist.Count > 0 Then
    '        tvrebuildlog(newlist.Count - 1.ToString & " subfolders found in: " & newlist(0) & vbCrLf)
    '    End If
    '    For Each folder In newlist
    '        tvrebuildlog("Searching: " & vbCrLf & folder & vbCrLf & "for episodes")
    '        Dim dir_info As New System.IO.DirectoryInfo(folder)
    '        tvrebuildlog("Looking in " & folder)
    '        Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(pattern, SearchOption.TopDirectoryOnly)
    '        For Each fs_info As System.IO.FileInfo In fs_infos

    '            Try
    '                Application.DoEvents()
    '                If IO.Path.GetFileName(fs_info.FullName.ToLower) <> "tvshow.nfo" Then
    '                    tvrebuildlog("possible episode nfo found: " & fs_info.FullName)
    '                    episode = nfoFunction.loadbasicepisodenfo(fs_info.FullName)
    '                    If Not episode Is Nothing Then
    '                        For Each ep In episode
    '                            If ep.Title <> Nothing Then
    '                                Dim skip As Boolean = False
    '                                For Each eps In tvshow.Episodes
    '                                    If eps.Season.Value = ep.Season.Value And eps.Episode.Value = ep.Episode.Value And eps.VideoFilePath = ep.VideoFilePath Then
    '                                        skip = True
    '                                        Exit For
    '                                    End If
    '                                Next
    '                                If skip = False Then
    '                                    tvshow.AddEpisode(ep)
    '                                    tvrebuildlog("Episode appears to have loaded ok")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                tvrebuildlog(ex.ToString)
    '            End Try
    '        Next fs_info
    '    Next
    '    tvrebuildlog(vbCrLf & vbCrLf & vbCrLf)

    'End Sub

    Public Function tv_ShowSelectedCurrently() As Media_Companion.TvShow
        TvTreeview.Focus()
        If TvTreeview.SelectedNode Is Nothing Then
            If TvTreeview.Nodes.Count = 0 Then Return Nothing
            TvTreeview.SelectedNode = TvTreeview.TopNode
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



    Public Sub tv_MissingArtDownload(ByVal BrokenShow As TvShow)
        'Dim messbox As New frmMessageBox("Attempting to download art", "", "       Please Wait")
        'messbox.Show()
        'messbox.Refresh()
        Application.DoEvents()
        Try
            'Dim tvdbstuff As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
            Dim tvdbstuff As New TVDBScraper
            Dim showlist As New XmlDocument
            Dim thumblist As String = tvdbstuff.GetPosterList(BrokenShow.TvdbId.Value)
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
                Exit Sub
            End If
            For f = 0 To 1000
                Dim seasonposter As String = ""
                For Each Image In artlist
                    If Image.Season = f.ToString And Image.Language = Preferences.TvdbLanguageCode Then
                        seasonposter = Image.Url
                        Exit For
                    End If
                Next
                If seasonposter = "" Then
                    For Each Image In artlist
                        If Image.Season = f.ToString And Image.Language = "en" Then
                            seasonposter = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If seasonposter = "" Then
                    For Each Image In artlist
                        If Image.Season = f.ToString Then
                            seasonposter = Image.Url
                            Exit For
                        End If
                    Next
                End If
                Dim tempstring As String = ""
                If seasonposter <> "" Then
                    If f < 10 Then
                        tempstring = "0" & f.ToString
                    Else
                        tempstring = f.ToString
                    End If
                    Dim seasonpath As String = BrokenShow.NfoFilePath.Replace(IO.Path.GetFileName(BrokenShow.NfoFilePath), "season" & tempstring & ".tbn")
                    If tempstring = "00" Then
                        seasonpath = BrokenShow.NfoFilePath.Replace(IO.Path.GetFileName(BrokenShow.NfoFilePath), "season-specials.tbn")
                    End If
                    If Not IO.File.Exists(seasonpath) Then
                        Utilities.DownloadFile(seasonposter, seasonpath)
                    End If
                End If
            Next
            Dim fanartposter As String
            fanartposter = ""
            For Each Image In artlist
                If Image.Language = Preferences.TvdbLanguageCode And Image.BannerType = "fanart" Then
                    fanartposter = Image.Url
                    Exit For
                End If
            Next
            If fanartposter = "" Then
                For Each Image In artlist
                    If Image.Language = "en" And Image.BannerType = "fanart" Then
                        fanartposter = Image.Url
                        Exit For
                    End If
                Next
            End If
            If fanartposter = "" Then
                For Each Image In artlist
                    If Image.BannerType = "fanart" Then
                        fanartposter = Image.Url
                        Exit For
                    End If
                Next
            End If
            If fanartposter <> "" Then

                Dim seasonpath As String = BrokenShow.NfoFilePath.Replace(IO.Path.GetFileName(BrokenShow.NfoFilePath), "fanart.jpg")
                If Not IO.File.Exists(seasonpath) Then
                    Try
                        Dim buffer(4000000) As Byte
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0

                        Dim thumburl As String = fanartposter
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


                        Try
                            If Preferences.resizefanart = 1 Then
                                bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                scraperLog = scraperLog & "Fanart not resized" & vbCrLf
                            ElseIf Preferences.resizefanart = 2 Then
                                If bmp.Width > 1280 Or bmp.Height > 720 Then
                                    Dim bm_source As New Bitmap(bmp)
                                    Dim bm_dest As New Bitmap(1280, 720)
                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                    gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                                    bm_dest.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    scraperLog = scraperLog & "Farart Resized to 1280x720" & vbCrLf
                                Else
                                    scraperLog = scraperLog & "Fanart not resized, already =< required size" & vbCrLf
                                    bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                End If
                            ElseIf Preferences.resizefanart = 3 Then
                                If bmp.Width > 960 Or bmp.Height > 540 Then
                                    Dim bm_source As New Bitmap(bmp)
                                    Dim bm_dest As New Bitmap(960, 540)
                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                    gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                                    bm_dest.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    scraperLog = scraperLog & "Farart Resized to 960x540" & vbCrLf
                                Else
                                    scraperLog = scraperLog & "Fanart not resized, already =< required size" & vbCrLf
                                    bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                End If

                            End If
                        Catch
                        End Try
                    Catch ex As WebException
#If SilentErrorScream Then
                        Throw ex
#End If
                    End Try
                End If
            End If

            Dim seasonallpath As String = ""
            Dim posterurlpath As String = ""
            Dim posterurl As String = ""
            If Preferences.postertype = "poster" Then 'poster
                For Each Image In artlist
                    If Image.Language = Preferences.TvdbLanguageCode And Image.BannerType = "poster" Then
                        posterurl = Image.Url
                        Exit For
                    End If
                Next
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.Language = "en" And Image.BannerType = "poster" Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.BannerType = "poster" Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath <> "" And Preferences.seasonall <> "none" Then
                    seasonallpath = posterurlpath
                End If
            ElseIf Preferences.postertype = "banner" Then 'banner
                For Each Image In artlist
                    If Image.Language = Preferences.TvdbLanguageCode And Image.BannerType = "series" And Image.Season = Nothing Then
                        posterurl = Image.Url
                        Exit For
                    End If
                Next
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.Language = "en" And Image.BannerType = "series" And Image.Season = Nothing Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.BannerType = "series" And Image.Season = Nothing Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath <> "" And RadioButton16.Checked = True Then
                    seasonallpath = posterurlpath
                End If
            End If

            If posterurlpath <> "" Then

                Dim seasonpath As String = BrokenShow.NfoFilePath.Replace(IO.Path.GetFileName(BrokenShow.NfoFilePath), "folder.jpg")
                If Not IO.File.Exists(seasonpath) Then
                    Utilities.DownloadFile(posterurlpath, seasonpath)
                End If
            End If



            If Preferences.seasonall <> "none" And seasonallpath = "" Then
                If Preferences.seasonall = "poster" Then 'poster
                    For Each Image In artlist
                        If Image.Language = Preferences.TvdbLanguageCode And Image.BannerType = "poster" Then
                            seasonallpath = Image.Url
                            Exit For
                        End If
                    Next
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.Language = "en" And Image.BannerType = "poster" Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.BannerType = "poster" Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                ElseIf Preferences.seasonall = "wide" = True Then 'banner
                    For Each Image In artlist
                        If Image.Language = Preferences.TvdbLanguageCode And Image.BannerType = "series" And Image.Season = Nothing Then
                            seasonallpath = Image.Url
                            Exit For
                        End If
                    Next
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.Language = "en" And Image.BannerType = "series" And Image.Season = Nothing Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.BannerType = "series" And Image.Season = Nothing Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                End If

                If seasonallpath <> "" Then

                    Dim seasonpath As String = BrokenShow.NfoFilePath.Replace(IO.Path.GetFileName(BrokenShow.NfoFilePath), "season-all.tbn")
                    If Not IO.File.Exists(seasonpath) Or CheckBox6.CheckState = CheckState.Checked Then
                        Utilities.DownloadFile(seasonallpath, seasonpath)
                    End If
                End If
            ElseIf Preferences.seasonall <> "none" And seasonallpath <> "" Then
                Dim seasonpath As String = BrokenShow.NfoFilePath.Replace(IO.Path.GetFileName(BrokenShow.NfoFilePath), "season-all.tbn")
                If Not IO.File.Exists(seasonpath) Then
                    Utilities.DownloadFile(seasonallpath, seasonpath)
                End If
            End If
        Catch
        End Try
        Call tv_ShowLoad(BrokenShow)
        messbox.Close()

    End Sub
    Private Sub tv_Filter()
        tv_Filter(Nothing)
    End Sub
    Private Sub tv_Filter(ByVal overrideShowIsMissing As String) 'ByVal butt As String)
        Dim butt As String = ""
        Dim overrideIsMissing As Boolean = overrideShowIsMissing IsNot Nothing

        If RadioButton29.Checked = True Then butt = "all"
        If RadioButton30.Checked = True Then butt = "fanart"
        If RadioButton31.Checked = True Then butt = "posters"
        If RadioButton32.Checked = True Then butt = "screenshot"
        If RadioButton44.Checked = True Then butt = "missingeps"
        If RadioButton53.Checked = True Then butt = "airedmissingeps"


        'If startup = True Then, issue #275
        If butt = "missingeps" Then
            If Not startup Then
                MessageBox.Show("Ensure that you have previously selected Display Missing Episodes from the TV Shows menu", "Missing Episodes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If Not episode.IsMissing Then
                            episode.Visible = False
                        Else
                            ' Phyonics - Fix for issue #208
                            If String.IsNullOrEmpty(episode.Aired.Value) Then
                                ' Change the colour to gray
                                episode.EpisodeNode.ForeColor = Color.Gray
                            Else
                                Try
                                    ' Is the episode in the future?
                                    If Convert.ToDateTime(episode.Aired.Value) > Now Then
                                        '  Yes, so change its colour to gray
                                        episode.EpisodeNode.ForeColor = Color.Gray
                                    Else
                                        episode.EpisodeNode.ForeColor = Drawing.Color.Blue
                                    End If
                                Catch ex As Exception
                                    ' Set the colour to the missing colour
                                    episode.EpisodeNode.ForeColor = Drawing.Color.Blue
                                End Try
                            End If

                            episode.Visible = True
                            episode.EpisodeNode.EnsureVisible()
                        End If
                    Next
                    If Season.VisibleEpisodeCount = 0 Then
                        Season.Visible = False
                    Else
                        Season.Visible = True
                    End If
                Next
                If item.VisibleSeasonCount = 0 Then
                    item.Visible = False
                Else
                    item.Visible = True
                End If
            Next
        ElseIf butt = "airedmissingeps" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If Not episode.IsMissing Then
                            episode.Visible = False
                        Else
                            ' Phyonics - Fix for issue #208
                            If String.IsNullOrEmpty(episode.Aired.Value) Then
                                episode.Visible = False
                            Else
                                ' Has the episode been aired yet?
                                Try
                                    If Convert.ToDateTime(episode.Aired.Value) <= Now Then
                                        episode.Visible = True
                                        episode.EpisodeNode.EnsureVisible()
                                    Else
                                        episode.Visible = False
                                    End If
                                Catch ex As Exception
                                    ' We failed to convert the aired date to a date, therefore don't show the episode
                                    episode.Visible = False
                                End Try
                            End If
                        End If
                    Next
                    If Season.VisibleEpisodeCount = 0 Then
                        Season.Visible = False
                    Else
                        Season.Visible = True
                    End If
                Next
                If item.VisibleSeasonCount = 0 Then
                    item.Visible = False
                Else
                    item.Visible = True
                End If
            Next
        ElseIf butt = "screenshot" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If (Not String.IsNullOrEmpty(episode.Thumbnail.FileName)) AndAlso IO.File.Exists(episode.Thumbnail.Path) Then
                            episode.Visible = False
                        Else
                            episode.Visible = True
                            episode.EpisodeNode.EnsureVisible()
                        End If
                    Next
                    If Season.VisibleEpisodeCount = 0 Then
                        Season.Visible = False
                    Else
                        Season.Visible = True
                    End If
                Next
                If item.VisibleSeasonCount = 0 Then
                    item.Visible = False
                Else
                    item.Visible = True
                End If
            Next
        ElseIf butt = "all" Then
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                item.Visible = True
                Dim containsVisibleSeason As Boolean = False
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    Dim containsVisibleEpisode As Boolean = False
                    For Each episode As Media_Companion.TvEpisode In Season.Episodes
                        If (episode.IsMissing AndAlso Not (Preferences.displayMissingEpisodes Or (overrideIsMissing AndAlso episode.ShowObj.ToString = overrideShowIsMissing))) Then
                            episode.Visible = False
                        Else
                            episode.Visible = True
                            containsVisibleEpisode = True
                        End If
                    Next
                    Season.Visible = containsVisibleEpisode
                    If containsVisibleEpisode Then containsVisibleSeason = True
                Next
                item.Visible = containsVisibleSeason
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
            For Each item As Media_Companion.TvShow In Cache.TvCache.Shows
                For Each Season As Media_Companion.TvSeason In item.Seasons.Values
                    If Season.Poster.Exists Then
                        Season.Visible = False
                    Else
                        Season.Visible = True
                    End If
                    For Each Episode As Media_Companion.TvEpisode In Season.Episodes
                        Episode.Visible = False
                    Next
                Next
                item.Visible = Not item.ImagePoster.Exists
            Next
        End If
        ' End If
    End Sub

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
            ToolStripStatusLabel2.Visible = False
            ToolStripStatusLabel2.Text = "TV Show Episode Scan In Progress"
            Application.DoEvents()
            TvTreeview.Sort()
            Dim showToRefresh = Nothing
            If (ShowList.Count = 1) Then
                ' ignore missing episodes checked entry for this forced node refresh
                showToRefresh = ShowList(0).ToString()
            End If
            tv_Filter(showToRefresh)
            MsgBox("Missing Episode Download Complete!", MsgBoxStyle.OkOnly, "Missing Episode Download.")
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub tv_EpisodesMissingFind(ByVal ShowList As List(Of TvShow))
        Utilities.EnsureFolderExists(IO.Path.Combine(Preferences.applicationPath, "missing\"))
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
                    Dim xmlfile As String

                    xmlfile = Utilities.DownloadTextFiles(url)

                    Dim SeriesInfo As New Tvdb.ShowData
                    SeriesInfo.LoadXml(xmlfile)

                    For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                        'Dim AlreadyExists As Boolean = False
                        'For Each ExistingEpisode As Media_Companion.TvEpisode In item.Episodes
                        '    If ExistingEpisode.Season.Value = NewEpisode.SeasonNumber.Value AndAlso ExistingEpisode.Episode.Value = NewEpisode.EpisodeNumber.Value Then
                        '        AlreadyExists = True
                        '        Exit For
                        '    End If
                        'Next
                        Dim Episode As TvEpisode = item.GetEpisode(NewEpisode.SeasonNumber.Value, NewEpisode.EpisodeNumber.Value)

                        If Episode Is Nothing Then
                            Dim MissingEpisode As New Media_Companion.TvEpisode

                            MissingEpisode.NfoFilePath = IO.Path.Combine(Preferences.applicationPath, "missing\" & item.TvdbId.Value & "." & NewEpisode.SeasonNumber.Value & "." & NewEpisode.EpisodeNumber.Value & ".nfo")
                            MissingEpisode.AbsorbTvdbEpisode(NewEpisode)
                            MissingEpisode.IsMissing = True
                            MissingEpisode.IsCache = True
                            MissingEpisode.ShowObj = item
                            MissingEpisode.Save()
                            item.AddEpisode(MissingEpisode)
                            Bckgrndfindmissingepisodes.ReportProgress(1, MissingEpisode)
                        Else
                            Episode.ShowObj = item
                            Episode.Save()
                        End If
                    Next

                End If
            End If
        Next
        Tv_CacheSave()

    End Sub

    Public Sub tv_CacheLoad()
        Cache.TvCache.TvCachePath = Preferences.workingProfile.tvcache
        Cache.TvCache.Load()
        TvTreeview.Nodes.Clear()              'clear the treeview of old data
        ''Dirty work around until TvShows is repalced with TvCache.Shows universally
        For Each TvShow As Media_Companion.TvShow In Cache.TvCache.Shows
            TvShow.UpdateTreenode()
            TvTreeview.Nodes.Add(TvShow.ShowNode)
        Next
        TextBox_TotTVShowCount.Text = Cache.TvCache.Shows.Count
        TextBox_TotEpisodeCount.Text = Cache.TvCache.Episodes.Count
        TvTreeview.Sort()
    End Sub

    Public Function Tv_CacheSave() As Boolean

        Cache.TvCache.TvCachePath = Preferences.workingProfile.tvcache
        Cache.TvCache.Save()
        Return False
    End Function

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

    Private Sub lstTasks_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TasksList.SelectedIndexChanged
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

    Private Sub lstTasks_Messages_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TasksMessages.SelectedIndexChanged
        If TasksMessages.SelectedItem Is Nothing Then Exit Sub

        TasksSelectedMessage.Text = TasksMessages.SelectedItem
    End Sub

    Private Sub cmbTasks_Arguments_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TasksArgumentSelector.SelectedIndexChanged
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

    Private Sub TasksTest_Click(sender As System.Object, e As System.EventArgs) Handles TasksTest.Click
        For I = 0 To 20
            Common.Tasks.Add(New Tasks.BlankTask())
        Next

        RefreshTaskList()
    End Sub

    Private Sub TasksRefresh_Click(sender As System.Object, e As System.EventArgs) Handles TasksRefresh.Click
        RefreshTaskList()
    End Sub

    Private Sub TasksClearCompleted_Click(sender As System.Object, e As System.EventArgs) Handles TasksClearCompleted.Click
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

        Dim Count As Long = TasksList.Items.Count
        Dim Cursor As Long = 0

        Dim CurrentTask As ITask
        Do
            If Count < 1 Or Cursor >= Count Then
                Exit Do
            End If

            CurrentTask = TasksList.Items(Cursor)
            If (TasksOnlyIncompleteTasks AndAlso CurrentTask.State = TaskState.Completed) OrElse Not Common.Tasks.Contains(CurrentTask) Then
                TasksList.Items.Remove(CurrentTask)
                Count -= 1

            Else
                Cursor += 1
            End If

            System.Windows.Forms.Application.DoEvents()
        Loop
    End Sub

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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
