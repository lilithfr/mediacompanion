Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
'Imports System.Threading
'Imports Media_Companion.ScraperFunctions

Imports System.Xml
'Imports System.Reflection
'Imports System.Windows.Forms
'Imports System.ComponentModel

Partial Public Class Form1
    Public TvCache As New TvCache

    Public TvShows As New List(Of TvShow)
    'Public workingTvShow As New TvShow
    'Public workingEpisode As New List(Of TvEpisode)
    'Public tempWorkingTvShow As New TvShow
    'Public tempWorkingEpisode As New TvEpisode
    Dim newEpisodeList As New List(Of TvEpisode)
    Dim languageList As New List(Of str_TvShowLanguages)
    Dim listOfShows As New List(Of str_PossibleShowList)

    Dim tvdbposterlist As New List(Of TvBanners)
    Dim imdbposterlist As New List(Of TvBanners)
    Dim tvdbmode As Boolean = False
    Dim usedlist As New List(Of TvBanners)

    Dim tvobjects As New List(Of String)

    Public Sub ResetTvView()
        'RenameTVShowsToolStripMenuItem.Enabled = False
        Button43.Enabled = True
        'RenameTVShowsToolStripMenuItem.Visible = False
        RebuildThisShowToolStripMenuItem.Enabled = False
        RebuildThisShowToolStripMenuItem.Visible = False
        MissingepisodesToolStripMenuItem.Enabled = False
        MissingepisodesToolStripMenuItem.Visible = False
        DisplayEpisodesByAiredDateToolStripMenuItem.Enabled = False
        DisplayEpisodesByAiredDateToolStripMenuItem.Visible = False
        SearchThisShowForNewEpisodesToolStripMenuItem.Enabled = False
        SearchThisShowForNewEpisodesToolStripMenuItem.Visible = False
        DownloadAvaileableMissingArtForShowToolStripMenuItem.Enabled = False
        DownloadAvaileableMissingArtForShowToolStripMenuItem.Visible = False

        ToolStripMenuItem1.Enabled = False
        ExpandSelectedShowToolStripMenuItem.Enabled = False
        CollapseSelectedShowToolStripMenuItem.Enabled = False
        ExpandAllToolStripMenuItem.Enabled = False
        CollapseAllToolStripMenuItem.Enabled = False
        ReloadItemToolStripMenuItem.Enabled = False
        OpenFolderToolStripMenuItem.Enabled = False

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
        PictureBox4.Image = Nothing
        PictureBox5.Image = Nothing

        tvdbposterlist.Clear()
        PictureBox6.Image = Nothing
        PictureBox4.Image = Nothing
        PictureBox5.Image = Nothing
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



        TextBox2.Text = ""
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

        PictureBox6.Image = Nothing
        PictureBox4.Image = Nothing
        PictureBox5.Image = Nothing
        ComboBox4.Items.Clear()
        ComboBox4.Text = ""
        For i = Panel13.Controls.Count - 1 To 0 Step -1
            Panel13.Controls.RemoveAt(i)
        Next
    End Sub
    Private Sub TvTreeview_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvTreeview.AfterSelect
        If TvTreeview.SelectedNode Is Nothing Then Exit Sub

        If TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvShow Then
            TvShowSelected(TvTreeview.SelectedNode.Tag)
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvSeason Then
            SeasonSelected(TvTreeview.SelectedNode.Tag)
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvEpisode Then
            EpisodeSelected(TvTreeview.SelectedNode.Tag)
        Else
            MsgBox("None")
        End If

        tv_SplitContainerAutoPosition("TVTreeView_AfterSelect")
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

    Public Sub TvShowSelected(ByRef SelectedTvShow As Nfo.TvShow)
        LoadTvShow(SelectedTvShow)

    End Sub

    Private Sub LoadTvShow(ByVal Show As Nfo.TvShow)
        Dim hg As New IO.DirectoryInfo(Show.FolderPath)
        If Not hg.Exists Then
            TextBox19.Text = "Unable to find folder: " & Show.FolderPath
            TextBox2.Text = "Unable to find folder: " & Show.FolderPath
        Else



            If TabControl3.TabPages(1).Text = "Screenshot" Then
                TabControl3.TabPages.RemoveAt(1)
            End If

            ToolStripMenuItem1.Enabled = True
            ExpandSelectedShowToolStripMenuItem.Enabled = True
            ExpandAllToolStripMenuItem.Enabled = True
            CollapseAllToolStripMenuItem.Enabled = True
            CollapseSelectedShowToolStripMenuItem.Enabled = True
            ReloadItemToolStripMenuItem.Enabled = True
            OpenFolderToolStripMenuItem.Enabled = True
            'load tvshow.nfo
            ListBox3.Items.Clear()
            TextBox26.Text = ""
            Dim todo As Boolean = False

            SearchThisShowForNewEpisodesToolStripMenuItem.Text = "Search """ & Show.Title.Value & """ for new episodes"
            SearchThisShowForNewEpisodesToolStripMenuItem.Enabled = True
            SearchThisShowForNewEpisodesToolStripMenuItem.Visible = True
            DownloadAvaileableMissingArtForShowToolStripMenuItem.Text = "Download missing art for """ & Show.Title.Value & """"
            DownloadAvaileableMissingArtForShowToolStripMenuItem.Enabled = True
            DownloadAvaileableMissingArtForShowToolStripMenuItem.Visible = True
            MissingepisodesToolStripMenuItem.Text = "Display missing episodes for """ & Show.Title.Value & """"
            MissingepisodesToolStripMenuItem.Enabled = True
            MissingepisodesToolStripMenuItem.Visible = True
            DisplayEpisodesByAiredDateToolStripMenuItem.Enabled = True
            DisplayEpisodesByAiredDateToolStripMenuItem.Visible = True
            RebuildThisShowToolStripMenuItem.Enabled = True
            RebuildThisShowToolStripMenuItem.Visible = True

            Dim tempstring As String = "Search """ & Show.Title.Value & """ for new episodes"
            SearchThisShowForNewEpisodesToolStripMenuItem.Text = tempstring
            SearchThisShowForNewEpisodesToolStripMenuItem.Enabled = True
            SearchThisShowForNewEpisodesToolStripMenuItem.Visible = True
            tempstring = "Download missing art for """ & Show.Title.Value & """"
            DownloadAvaileableMissingArtForShowToolStripMenuItem.Text = tempstring
            DownloadAvaileableMissingArtForShowToolStripMenuItem.Enabled = True
            DownloadAvaileableMissingArtForShowToolStripMenuItem.Visible = True
            tempstring = "Display missing episodes for """ & Show.Title.Value & """"
            MissingepisodesToolStripMenuItem.Text = tempstring
            MissingepisodesToolStripMenuItem.Enabled = True
            MissingepisodesToolStripMenuItem.Visible = True
            DisplayEpisodesByAiredDateToolStripMenuItem.Enabled = True
            DisplayEpisodesByAiredDateToolStripMenuItem.Visible = True
            RebuildThisShowToolStripMenuItem.Enabled = True
            RebuildThisShowToolStripMenuItem.Visible = True
            If Show.State.Value = Nfo.ShowState.Locked Then
                Button60.Text = "Locked"
                Button60.BackColor = Color.Red
            ElseIf Show.State.Value = Nfo.ShowState.Open Then
                Button60.Text = "Open"
                Button60.BackColor = Color.LawnGreen
            ElseIf Show.State.Value = Nfo.ShowState.Unverified Then
                Button60.Text = "Un-Verified"
                Button60.BackColor = Color.Yellow
            Else
                Button60.Text = "Error"
                Button60.BackColor = Color.Gray
            End If
            Button60.Tag = Show


            If Preferences.postertype = "banner" Then
                PictureBox5.Image = Show.ImageBanner.Image
            Else
                PictureBox5.Image = Show.ImagePoster.Image
            End If

            PictureBox4.Image = Show.ImageFanart.Image

            Panel9.Visible = False

            If Show.Title <> Nothing Then TextBox2.Text = Show.Title.Value
            If Show.Premiered <> Nothing Then TextBox10.Text = Show.Premiered.Value
            If Show.Genre <> Nothing Then TextBox11.Text = Show.Genre.Value
            If Show.TvdbId <> Nothing Then TextBox9.Text = Show.TvdbId.Value
            If Show.ImdbId <> Nothing Then TextBox12.Text = Show.ImdbId.Value
            If Show.Rating <> Nothing Then TextBox13.Text = Show.Rating.Value
            If Show.Mpaa <> Nothing Then TextBox14.Text = Show.Mpaa.Value
            If Show.Runtime <> Nothing Then TextBox15.Text = Show.Runtime.Value
            If Show.Studio <> Nothing Then TextBox16.Text = Show.Studio.Value
            If Show.Plot <> Nothing Then TextBox19.Text = Show.Plot.Value

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
                If Preferences.tvdbactorscrape = "0" Or Preferences.tvdbactorscrape = "2" Then
                    Show.EpisodeActorSource.Value = "tvdb"
                Else
                    Show.EpisodeActorSource.Value = "imdb"
                End If
            End If

            If String.IsNullOrEmpty(Show.EpisodeActorSource.Value) Then
                If Preferences.tvdbactorscrape = "0" Or Preferences.tvdbactorscrape = "2" Then
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
                If Preferences.tvdbactorscrape = "0" Or Preferences.tvdbactorscrape = "3" Then
                    Show.TvShowActorSource.Value = "tvdb"
                Else
                    Show.TvShowActorSource.Value = "imdb"
                End If
            End If

            If String.IsNullOrEmpty(Show.TvShowActorSource.Value) Then
                If Preferences.tvdbactorscrape = "0" Or Preferences.tvdbactorscrape = "3" Then
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

            For Each actor In Show.ListActors
                ComboBox4.Items.Add(actor.actorname)
            Next

            If Not ComboBox4.Items.Count = 0 Then
                ComboBox4.SelectedIndex = 0
            End If
        End If
        Panel9.Visible = False
    End Sub


    Public Sub SeasonSelected(ByRef SelectedSeason As Nfo.TvSeason)
        Dim Show As Nfo.TvShow
        If SelectedSeason.SeasonNode.Parent.Tag IsNot Nothing Then
            Show = SelectedSeason.SeasonNode.Parent.Tag
        Else
            MsgBox("Show tag not set")
            Exit Sub
        End If


        If TabControl3.TabPages(1).Text = "Screenshot" Then
            TabControl3.TabPages.RemoveAt(1)
        End If
        Panel9.Visible = False
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
        RenameTVShowsToolStripMenuItem.Enabled = True
        RenameTVShowsToolStripMenuItem.Visible = True

        PictureBox5.Image = Nothing
        PictureBox4.Image = Nothing
        'MsgBox("Season")
        Dim season As String = SelectedSeason.SeasonLabel
        Dim trueseason As Integer = SelectedSeason.SeasonNumber
        Dim PaddedSeason As String = Utilities.PadNumber(SelectedSeason.SeasonNumber, 2)


        If trueseason = -1 Then
            If SelectedSeason.Poster.Image IsNot Nothing Then
                PictureBox5.Image = SelectedSeason.Poster.Image
            Else
                If Preferences.postertype = "banner" Then
                    PictureBox5.Image = Show.ImagePoster.Image
                Else
                    PictureBox5.Image = Show.ImageBanner.Image
                End If
            End If
        ElseIf trueseason = 0 Then
            If IO.File.Exists(Show.NfoFilePath.ToLower.Replace("tvshow.nfo", "season-specials.tbn")) Then
                Try
                    PictureBox5.ImageLocation = Show.NfoFilePath.Replace("tvshow.nfo", "season-specials.tbn")
                Catch
                    PictureBox5.Image = Nothing
                End Try
            Else
                Try
                    PictureBox5.ImageLocation = Show.NfoFilePath.Replace("tvshow.nfo", "folder.jpg")
                Catch
                    PictureBox5.Image = Nothing
                End Try
            End If
        Else
            PictureBox5.Image = SelectedSeason.Poster.Image
            If PictureBox5.Image Is Nothing Then
                PictureBox5.Image = Show.ImageAllSeasons.Image
            End If


        End If

        If Show.NfoFilePath <> Nothing Then
            Try
                PictureBox4.ImageLocation = Show.NfoFilePath.Replace("tvshow.nfo", "fanart.jpg")
            Catch
                PictureBox4.Image = Nothing
            End Try
        End If

    End Sub

    Public Sub EpisodeSelected(ByRef SelectedEpisode As Nfo.TvEpisode)
        'loadepisode
        If TabControl3.TabPages(1).Text <> "Screenshot" Then
            If screenshotTab IsNot Nothing Then
                TabControl3.TabPages.Insert(1, screenshotTab)
                TabControl3.Refresh()
            End If
        End If
        Panel9.Visible = True
        'If workingTvShow.plot IsNot Nothing AndAlso workingTvShow.plot.Contains("Unable to find folder:") Then
        '    TvTreeview.SelectedNode.Parent.Parent.ForeColor = Color.Red
        '    TvTreeview.SelectedNode.Parent.Parent.Collapse()
        '    ExpandSelectedShowToolStripMenuItem.Enabled = True
        '    ExpandAllToolStripMenuItem.Enabled = True
        '    CollapseAllToolStripMenuItem.Enabled = True
        '    CollapseSelectedShowToolStripMenuItem.Enabled = True
        '    ReloadItemToolStripMenuItem.Enabled = True
        '    Exit Sub
        'Else
        '    'TvTreeview.SelectedNode.ForeColor = Color.Black
        'End Ifr


        Dim season As Integer = SelectedEpisode.Season.Value
        Dim episode As Integer = SelectedEpisode.Episode.Value
        Dim SeasonObj As Nfo.TvSeason
        If SelectedEpisode.EpisodeNode.Parent IsNot Nothing Then
            SeasonObj = SelectedEpisode.EpisodeNode.Parent.Tag
            If season = -1 Then season = SeasonObj.SeasonLabel
        End If


        Call LoadTvEpisode(SelectedEpisode)

        ToolStripMenuItem1.Enabled = True
        ExpandSelectedShowToolStripMenuItem.Enabled = True
        ExpandAllToolStripMenuItem.Enabled = True
        CollapseAllToolStripMenuItem.Enabled = True
        CollapseSelectedShowToolStripMenuItem.Enabled = True
        ReloadItemToolStripMenuItem.Enabled = True
        OpenFolderToolStripMenuItem.Enabled = True
        'If SelectedEpisode.Plot.IndexOf("xml error") = -1 And SelectedEpisode.Plot.IndexOf("missing file") = -1 Then

        '    If TvTreeview.SelectedNode.ForeColor = Color.Red Then
        '        For Each Sh In TvShows
        '            If TvTreeview.SelectedNode.Name.IndexOf(Sh.fullpath.Substring(0, Sh.fullpath.Length - 10)) <> -1 Then
        '                For Each ep In Sh.allepisodes

        '                Next

        '                'Call savetvdata()
        '                Exit For
        '            End If
        '        Next
        '        'TvTreeview.SelectedNode.ForeColor = Color.Black
        '        Call reloadtvshow()
        '    End If
        'Else
        '    If TvTreeview.SelectedNode.ForeColor = Color.Black Then
        '        For Each Sh In TvShows
        '            If TvTreeview.SelectedNode.Name.ToLower.IndexOf(Sh.fullpath.ToLower.Substring(0, Sh.fullpath.Length - 10)) <> -1 Then
        '                For Each ep In Sh.allepisodes
        '                    If ep.VideoFilePath = TvTreeview.SelectedNode.Name Then
        '                        'ep.status = "xml error"
        '                        Exit For
        '                    End If
        '                Next
        '                'Call savetvdata()
        '                Exit For
        '            End If
        '        Next
        '        'TvTreeview.SelectedNode.ForeColor = Color.Black
        '        'Call reloadtvshow()
        '        TvTreeview.SelectedNode.ForeColor = Color.Red
        '    End If
        'End If

    End Sub

    Private Sub LoadTvEpisode(ByRef Episode As Nfo.TvEpisode)
        Dim tempstring As String = ""
        TextBox2.Text = ""
        TextBox20.Text = ""
        TextBox21.Text = ""
        TextBox22.Text = ""
        TextBox23.Text = ""
        TextBox24.Text = ""
        TextBox25.Text = ""
        ComboBox5.Items.Clear()
        ComboBox5.Text = ""

        TextBox17.Text = ""
        TextBox29.Text = ""
        TextBox29.Text = IO.Path.GetFileName(Episode.NfoFilePath)
        TextBox17.Text = Episode.FolderPath
        If Not IO.File.Exists(Episode.NfoFilePath) Then
            TextBox2.Text = "Unable to find episode: " & Episode.NfoFilePath
            TextBox21.Text = "Unable to find episode: " & Episode.NfoFilePath
            Panel9.Visible = True
            Episode.EpisodeNode.BackColor = Color.Red
            Exit Sub
        Else
            Episode.EpisodeNode.BackColor = Color.Transparent   'i.e. back to normal
        End If
        TextBox2.Text = Episode.Title.Value
        TextBox20.Text = Episode.Rating.Value
        TextBox21.Text = Episode.Plot.Value
        TextBox22.Text = Episode.Director.Value
        TextBox23.Text = Episode.Credits.Value
        TextBox24.Text = Episode.Aired.Value

        For Each actor In Episode.ListActors
            If actor.actorname <> Nothing Then
                ComboBox5.Items.Add(actor.actorname)
            End If
        Next
        If ComboBox5.Items.Count = 0 Then
            For Each actor In Episode.ListActors
                If actor.actorname <> Nothing Then
                    ComboBox5.Items.Add(actor.actorname)
                End If
            Next
        Else
            ComboBox5.SelectedIndex = 0
        End If
        '        Try


        '        Catch ex As Exception
        '#If SilentErrorScream Then
        '            Throw ex
        '#End If
        '        End Try

        'tempstring = Episode.NfoFilePath.Substring(0, Episode.NfoFilePath.Length - 3)
        'tempstring = tempstring & "tbn"
        If Episode.Thumbnail.Image IsNot Nothing Then

            'Dim bitmap2 As New Bitmap(tempstring)
            'Dim bitmap3 As New Bitmap(bitmap2)
            'bitmap2.Dispose()
            PictureBox4.Image = Episode.Thumbnail.Image
            PictureBox14.Image = Episode.Thumbnail.Image

        Else

            PictureBox14.Image = Nothing

            PictureBox4.Image = Nothing

        End If

        'If Episode.Season.Value <> 0 Then
        '    If IO.File.Exists(workingTvShow.path.ToLower.Replace("tvshow.nfo", "season" & Episode.Season.Value.ToString & ".tbn")) Then
        '        PictureBox5.ImageLocation = workingTvShow.path.Replace("tvshow.nfo", "season" & Episode.Season.Value.ToString & ".tbn")
        '    End If
        'Else
        '    If IO.File.Exists(workingTvShow.path.ToLower.Replace("tvshow.nfo", "season" & Episode.Season.Value.ToString & ".tbn")) Then

        '        Dim fi As New FileInfo(workingTvShow.path.Replace("tvshow.nfo", "season00.tbn"))
        '        Dim rename2 As String = workingTvShow.path.Replace("tvshow.nfo", "season-specials.tbn")
        '        fi.MoveTo(rename2)

        '    End If

        '    If IO.File.Exists(workingTvShow.path.ToLower.Replace("tvshow.nfo", "season-specials.tbn")) Then
        '        PictureBox5.ImageLocation = workingTvShow.path.ToLower.Replace("tvshow.nfo", "season-specials.tbn")
        '    End If

        'End If

        'If Episode.Season.Value <> "Unknown" Then
        '    TextBox2.Text = "S" & Episode.Season.Value & "E" & Episode.Season.Value & " - " & Episode.Title.Value
        'Else
        '    TextBox2.Text = Episode.Title.Value & ": " & IO.Path.GetFileName(Episode.NfoFilePath)
        '    TextBox21.Text = "This media file has been found by Media Companion" & vbCrLf & "Within this TV Shows Folder" & vbCrLf & vbCrLf & "Season and/or episode numbers could" & vbCrLf & "identified from the filename"
        'End If
        Panel9.Visible = True

    End Sub


    Private Sub RebuildTvShows()
        tvrebuildlog("Starting TV Show Rebuild" & vbCrLf & vbCrLf, , True)
        TV_CleanFolderList()
        totalTvShowCount = 0
        totalEpisodeCount = 0
        TextBox32.Text = ""
        TextBox33.Text = ""
        Me.Enabled = False
        TvShows.Clear()
        TvTreeview.Nodes.Clear()
        For Each tvfolder In Preferences.tvFolders
            'tvrebuildlog("Adding " & tvfolder)
            Dim hg As New IO.DirectoryInfo(tvfolder)
            If Not hg.Exists Then
                'Dim newtvshownfo As New basictvshownfo
                'newtvshownfo.title = filefunction.getlastfolder(tvfolder)
                'newtvshownfo.status = "Folder not found"
                'basictvlist.Add(newtvshownfo)
            End If
            'tvrebuildlog("tvshow.nfo path is: " & shownfopath)
            Dim newtvshownfo As New TvShow
            newtvshownfo.NfoFilePath = IO.Path.Combine(tvfolder, "tvshow.nfo")
            newtvshownfo.Load()
            If newtvshownfo.title IsNot Nothing Then
                If newtvshownfo.status Is Nothing OrElse (newtvshownfo.status IsNot Nothing AndAlso Not newtvshownfo.status.Contains("skipthisfile")) Then
                    'Dim skip As Boolean = False
                    'For Each tvshow In TvShows
                    '    If newtvshownfo.fullpath = tvshow.fullpath Then
                    '        skip = True
                    '        Exit For
                    '    End If
                    'Next
                    'If skip = False Then
                    TvShows.Add(newtvshownfo)
                    'newtvshownfo.SearchForEpisodesInFolder()
                    TvTreeview.Nodes.Add(newtvshownfo.ShowNode)
                    'End If
                End If
            End If
            realTvPaths.Add(tvfolder)
            'End If
        Next

        For Each tv In TvShows
            ListtvFiles(tv, "*.NFO")
        Next


        TV_CleanFolderList()
        'Call populatetvtree()

        Me.Enabled = True

        'Call savetvdata()

    End Sub

    Private Sub loadshowlist()
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
                    PictureBox9.ImageLocation = listOfShows(0).showbanner
                Catch ex As Exception
                    PictureBox9.Image = Nothing
                End Try
            End If

            Call checklanguage()
            messbox.Close()
        Else
            MsgBox("Please Enter a Search Term")
        End If
    End Sub



    Private Function gettoptvshow(ByVal tvshowname As String)

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
                                If checktvlanguage(lan.showid, templanguage) = True Then
                                    Return lan.showid
                                End If
                            End If
                            newshows.Add(lan)
                    End Select
                Next
                Dim returnid As String = ""
                If checktvlanguage(newshows(0).showid, templanguage) = True Then
                    Return newshows(0).showid
                Else
                    If templanguage <> "en" Then
                        If checktvlanguage(newshows(0).showid, "en") = True Then
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

        'If e.UserState = "i" Then
        ToolStripStatusLabel5.Text = "Scraping TV Shows, " & newTvFolders.Count & " remaining"
        ToolStripStatusLabel5.Visible = True
        'Call updatetree(False)

        Dim NewShow As TvShow = e.UserState


        ListtvFiles(NewShow, "*.NFO")
        realTvPaths.Add(NewShow.FolderPath)
        TvTreeview.Nodes.Add(NewShow.ShowNode)

        'CleanFolderList()
        ''For Each item In TvShows
        ''    If item.FolderPath = NewShow.FolderPath Then
        ''        Dim cnode As TreeNode = Nothing
        ''        Dim tempstring As String = String.Empty
        ''        Dim tempint As Integer

        ''        totalTvShowCount += 1
        ''        Dim shownode As Integer = -1

        ''        If item.status IsNot Nothing AndAlso item.status.ToLower.IndexOf("xml error") <> -1 Then
        '            Call TV_AddTvshowToTreeview(item.fullpath, item.title, True, item.locked)
        ''        Else
        '            Call TV_AddTvshowToTreeview(item.fullpath, item.title, False, item.locked)
        ''        End If

        ''        For Each episode In item.allepisodes
        ''            totalEpisodeCount += 1

        ''            Dim seasonno As Integer = -10
        ''            seasonno = Convert.ToInt32(episode.Season.value)

        ''            For g = 0 To TvTreeview.Nodes.Count - 1
        ''                If TvTreeview.Nodes(g).Name.ToString = item.fullpath Then
        ''                    cnode = TvTreeview.Nodes(g)
        ''                    shownode = g
        ''                    Exit For
        ''                End If
        ''            Next

        ''            Dim seasonstring As String = Nothing

        ''            If seasonno <> 0 And seasonno <> -1 Then
        ''                If seasonno < 10 Then
        ''                    tempstring = "Season 0" & seasonno.ToString
        ''                Else
        ''                    tempstring = "Season " & seasonno.ToString
        ''                End If
        ''            ElseIf seasonno = 0 Then
        ''                tempstring = "Specials"
        ''                'ElseIf seasonno = -1 Then
        ''                '    tempstring = "Unknown"
        ''            End If
        ''            Dim node As TreeNode
        ''            Dim alreadyexists As Boolean = False
        ''            For Each node In cnode.Nodes
        ''                If node.Text = tempstring Then
        ''                    alreadyexists = True
        ''                    Exit For
        ''                End If
        ''            Next
        ''            If alreadyexists = False Then cnode.Nodes.Add(tempstring)

        ''            For Each node In cnode.Nodes
        ''                If node.Text = tempstring Then
        ''                    tempint = node.Index

        ''                    Exit For
        ''                End If
        ''            Next

        ''            Dim eps As String
        ''            If episode.episodeno < 10 Then
        ''                eps = "0" & episode.episodeno.ToString
        ''            Else
        ''                eps = episode.episodeno.ToString
        ''            End If
        ''            eps = eps & " - " & episode.title
        ''            If episode.imdbid = Nothing Then
        ''                episode.imdbid = ""
        ''            End If

        ''            If episode.imdbid.ToLower.IndexOf("xml error") <> -1 Then
        '                Call TV_AddEpisodeToTreeview(shownode, tempint, episode.VideoFilePath, eps, True)
        ''            Else
        '                Call TV_AddEpisodeToTreeview(shownode, tempint, episode.VideoFilePath, eps, False)
        ''            End If


        '        Next
        '    End If
        'Next
        TextBox32.Text = totalTvShowCount.ToString
        TextBox33.Text = totalEpisodeCount.ToString
        Me.BringToFront()
        Me.Activate()
        ';

    End Sub

    Private Sub bckgrnd_tvshowscraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bckgrnd_tvshowscraper.DoWork
        Dim speedy As Boolean = Preferences.tvshowautoquick
        Do While newTvFolders.Count > 0
            Dim NewShow As New TvShow
            NewShow.NfoFilePath = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")

            If NewShow.FileContainsReadableXml Then
                NewShow.Load()
                NewShow.UpdateTreenode()
                TvShows.Add(NewShow)

                'TvShows.Add(NewShow)
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
                    NewShow.locked = Nfo.ShowState.Unverified

                    tvshowid = "none"
                ElseIf NewShow.PossibleShowList.Count = 1 Then
                    NewShow.locked = Nfo.ShowState.Open

                    tvshowid = NewShow.PossibleShowList.Item(0).Id.Value
                Else
                    NewShow.locked = Nfo.ShowState.Unverified

                    tvshowid = NewShow.PossibleShowList.Item(0).Id.Value
                End If
            Else
                tvshowid = "none"
            End If
            'tvshowid = gettoptvshow(FolderName)

            If IsNumeric(tvshowid) Then
                'tvshow found
                Dim newtvshow As New TvShow
                newtvshow.tvdbid = tvshowid
                newtvshow.path = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")

                Dim tvdbstuff As New TVDBScraper

                Dim posterurl As String = ""
                Dim tempstring As String = ""

                If templanguage = Nothing Then templanguage = "en"
                If templanguage = "" Then templanguage = "en"
                Dim SeriesInfo As Tvdb.ShowData = tvdbstuff.GetShow(tvshowid, templanguage, True)

                NewShow.AbsorbTvdbSeries(SeriesInfo.Series(0))


                Dim TvdbActors As Tvdb.Actors = tvdbstuff.GetActors(tvshowid, templanguage)
                For Each Act As Tvdb.Actor In TvdbActors.Items
                    If NewShow.ListActors.Count >= Preferences.maxactors Then
                        Exit For
                    End If

                    Dim NewAct As New Nfo.Actor
                    NewAct.ActorId = Act.Id
                    NewAct.actorname = Act.Name.Value
                    NewAct.actorrole = Act.Role.Value
                    NewAct.actorthumb = Act.Image.Value

                    If Preferences.tvdbactorscrape = 0 Or Preferences.tvdbactorscrape = 3 Or newtvshow.imdbid = Nothing Then
                        Dim id As String = ""
                        'Dim acts As New MovieActors
                        Dim results As XmlNode = Nothing
                        Dim lan As New str_PossibleShowList(SetDefaults)


                        If Not String.IsNullOrEmpty(NewAct.actorthumb) Then
                            If NewAct.actorthumb <> "" And Preferences.actorseasy = True And speedy = False Then
                                If NewShow.tvshowactorsource <> "imdb" Or NewShow.imdbid = Nothing Then
                                    Dim workingpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "")
                                    workingpath = workingpath & ".actors\"

                                    Utilities.EnsureFolderExists(workingpath)

                                    Dim hg As New IO.DirectoryInfo(workingpath)
                                    Dim destsorted As Boolean = False
                                    If Not hg.Exists Then

                                        IO.Directory.CreateDirectory(workingpath)
                                        destsorted = True

                                    Else
                                        destsorted = True
                                    End If
                                    If destsorted = True Then
                                        Dim filename As String = NewAct.actorname.Replace(" ", "_")
                                        filename = filename & ".tbn"
                                        filename = IO.Path.Combine(workingpath, filename)

                                        Utilities.DownloadFile(NewAct.actorthumb, filename)
                                    End If
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

                If Preferences.tvdbactorscrape = 1 Or Preferences.tvdbactorscrape = 2 And newtvshow.imdbid <> Nothing Then
                    Dim imdbscraper As New Classimdb
                    Dim actorlist As String
                    Dim actorstring As New XmlDocument
                    actorlist = imdbscraper.getimdbactors(Preferences.imdbmirror, newtvshow.imdbid)

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
                Dim ArtList As Tvdb.Banners = tvdbstuff.GetPosterList(newtvshow.tvdbid, True)
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
                                Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season" & tempstring & ".tbn")
                                If tempstring = "00" Then
                                    seasonpath = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season-specials.tbn")
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

                            Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "fanart.jpg")
                            If Not IO.File.Exists(seasonpath) Then

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

                            Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "folder.jpg")
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

                            Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season-all.tbn")
                            If Not IO.File.Exists(seasonpath) Or CheckBox6.CheckState = CheckState.Checked Then

                                Utilities.DownloadFile(seasonallpath, seasonallpath)

                            End If
                        End If
                    ElseIf Preferences.seasonall <> "none" And seasonallpath <> "" Then
                        Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season-all.tbn")
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
                If Preferences.tvdbactorscrape = 0 Or Preferences.tvdbactorscrape = 2 Then
                    newtvshow.episodeactorsource = "tvdb"
                Else
                    newtvshow.episodeactorsource = "imdb"
                End If
                If Preferences.tvdbactorscrape = 0 Or Preferences.tvdbactorscrape = 3 Then
                    newtvshow.tvshowactorsource = "tvdb"
                Else
                    newtvshow.tvshowactorsource = "imdb"
                End If

                If tempstring = "0" Or tempstring = "2" Then
                    newtvshow.episodeactorsource = "tvdb"
                Else
                    newtvshow.episodeactorsource = "imdb"
                End If

                newtvshow.sortorder = Preferences.sortorder

                'nfoFunction.savetvshownfo(newtvshow.path, newtvshow, True)

            End If
            'DownloadMissingArt(NewShow)
            NewShow.Save()
            NewShow.UpdateTreenode()
            TvShows.Add(NewShow)
            If Not Preferences.tvFolders.Contains(newTvFolders(0)) Then
                Preferences.tvFolders.Add(newTvFolders(0))
            End If
            bckgrnd_tvshowscraper.ReportProgress(0, NewShow)
            newTvFolders.RemoveAt(0)
        Loop

        TV_CleanFolderList()
    End Sub

    Public Sub TV_CleanFolderList()
        Dim TempList As List(Of String)
        TempList = Preferences.tvFolders
        Dim ReturnList As New List(Of String)

        For Each item In newTvFolders
            If Not ReturnList.Contains(item) Then
                ReturnList.Add(item)
            End If
        Next

        Preferences.tvFolders = TempList
    End Sub

    Private Sub TV_EpisodeScraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
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
            Preferences.tvScraperLog &= vbCrLf & "Operation cancelled by user"
            Exit Sub
        End If
        If Preferences.tvshow_useXBMC_Scraper = True Then
            Preferences.tvScraperLog &= "---Using XBMC TVDB Scraper---" & vbCrLf
        Else
            Preferences.tvScraperLog &= "---Using MC TVDB Scraper---" & vbCrLf
        End If

        progresstext = String.Concat("Scanning TV Folders For New Episodes")
        bckgroundscanepisodes.ReportProgress(progress, progresstext)


        Preferences.tvScraperLog &= "Starting Folder Scan" & vbCrLf & vbCrLf

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
        extensions(25) = "*.dvr-ms"
        extensions(26) = "VIDEO_TS.IFO"

        extensioncount = 26


        Dim TvFolder As String
        For Each TvShow As Nfo.TvShow In TvShows
            TvFolder = IO.Path.GetDirectoryName(TvShow.FolderPath)
            Dim Add As Boolean = True
            If TvShow.State.Value <> Nfo.ShowState.Open Then
                If manual = False Then
                    Add = False
                    Exit For
                End If
            End If

            If Add = True Then
                progresstext = String.Concat("Adding subfolders: " & TvFolder)
                bckgroundscanepisodes.ReportProgress(progress, progresstext)
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "Operation cancelled by user"
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
                    newtvfolders.Add(TvFolder)
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
        For g = 0 To newtvfolders.Count - 1
            Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
            bckgroundscanepisodes.ReportProgress(progress, progresstext)
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog &= vbCrLf & "Operation cancelled by user"
                Exit Sub
            End If
            progresstext = String.Concat("Searching for episodes in " & newtvfolders(g))
            bckgroundscanepisodes.ReportProgress(progress, progresstext)
            For Each f In Utilities.VideoExtensions
                'If bckgroundscanepisodes.CancellationPending Then
                '    Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation cancelled by user"
                '    Exit Sub
                Preferences.tvScraperLog &= vbCrLf & "Operation cancelled by user"
                moviepattern = f
                dirpath = newtvfolders(g)
                Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                findnewepisodes(dirpath, moviepattern)
            Next f
            tempint = newEpisodeList.Count - mediacounter

            Preferences.tvScraperLog &= tempint.ToString & " New episodes found in directory " & dirpath & vbCrLf
            mediacounter = newEpisodeList.Count
        Next g

        Preferences.tvScraperLog &= vbCrLf
        If newEpisodeList.Count <= 0 Then
            Preferences.tvScraperLog &= "No new episodes found, exiting scraper." & vbCrLf
            Exit Sub
        End If

        Dim S As String = ""
        For Each newepisode In newEpisodeList

            S = ""

            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
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
                        newepisode.Season.value = "-1"
                        newepisode.Episode.Value = "-1"
                    End Try
                End If
            Next
            If newepisode.Season.value = Nothing Then newepisode.Season.value = "-1"
            If newepisode.Episode.Value = Nothing Then newepisode.Episode.Value = "-1"
        Next
        Dim savepath As String = ""

        Dim scrapedok As Boolean
        Dim epscount As Integer = 0
        For Each eps In newEpisodeList
            epscount += 1
            Preferences.tvScraperLog &= "********** WORKING ON: " & eps.VideoFilePath & " **********" & vbCrLf
            If eps.Season.value <> "-1" And eps.Episode.Value <> "-1" Then
                Preferences.tvScraperLog &= "Season : " & eps.Season.value & vbCrLf
                Preferences.tvScraperLog &= "Episode: " & eps.Episode.Value & vbCrLf
            Else
                Preferences.tvScraperLog &= "WARNING: Cant extract Season and Episode details from filename: " & vbCrLf
            End If

            tempTVDBiD = ""
            Dim episodearray As New List(Of TvEpisode)
            episodearray.Clear()
            Dim multieps2 As New TvEpisode
            multieps2.Season.value = eps.Season.value
            multieps2.Episode.Value = eps.Episode.Value
            multieps2.VideoFilePath = eps.VideoFilePath
            multieps2.mediaextension = eps.mediaextension
            episodearray.Add(multieps2)
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                Exit Sub
            End If

            Dim WhichScraper As String = ""
            If Preferences.tvshow_useXBMC_Scraper = True Then
                WhichScraper = " XBMC TVDB Scraper - "
            Else
                WhichScraper = " MC TVDB Scraper - "
            End If

            progresstext = String.Concat(WhichScraper & "Scraping " & epscount & " of " & newEpisodeList.Count & " - " & IO.Path.GetFileName(eps.VideoFilePath))
            bckgroundscanepisodes.ReportProgress(progress, progresstext)

            Dim removal As String = ""
            If eps.Season.value = "-1" Or eps.Episode.Value = "-1" Then
                eps.Title.Value = Utilities.GetFileName(eps.VideoFilePath)
                eps.Rating.Value = "0"
                eps.PlayCount.Value = "0"
                eps.Genre.Value = "Unknown Episode Season and/or Episode Number"
                eps.Details = Preferences.Get_HdTags(eps.MediaExtension)
                episodearray.Add(eps)
                savepath = episodearray(0).VideoFilePath
            Else

                Dim temppath As String = eps.VideoFilePath
                'check for multiepisode files
                Dim M2 As Match
                Dim epcount As Integer = 0
                Dim multiepisode As Boolean = False
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
                            multieps.Season.value = eps.Season.value
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
                        Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                Loop Until M2.Success = False
                Dim language As String = ""
                Dim sortorder As String = ""
                Dim tvdbid As String = ""
                Dim imdbid As String = ""
                Dim actorsource As String = ""
                Dim realshowpath As String = ""

                savepath = episodearray(0).VideoFilePath
                Dim EpisodeName As String = ""
                For Each Shows In TvShows
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    If episodearray(0).VideoFilePath.IndexOf(Shows.fullpath.Replace("tvshow.nfo", "")) <> -1 Then
                        language = Shows.language
                        sortorder = Shows.sortorder
                        tvdbid = Shows.tvdbid
                        tempTVDBiD = Shows.tvdbid
                        imdbid = Shows.imdbid
                        showtitle = Shows.title
                        EpisodeName = Shows.title
                        realshowpath = Shows.fullpath
                        actorsource = Shows.episodeactorsource
                    End If
                Next
                If episodearray.Count > 1 Then
                    Preferences.tvScraperLog &= "Multipart episode found: " & vbCrLf
                    Preferences.tvScraperLog &= "Season: " & episodearray(0).Season.value & " Episodes, "
                    For Each ep In episodearray
                        Preferences.tvScraperLog &= ep.Episode.Value & ", "
                    Next
                    Preferences.tvScraperLog &= vbCrLf
                End If
                Preferences.tvScraperLog &= "Looking up scraper options from tvshow.nfo" & vbCrLf

                For Each singleepisode In episodearray

                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    If singleepisode.Season.value.Length > 0 Or singleepisode.Season.value.IndexOf("0") = 0 Then
                        Do Until singleepisode.Season.value.IndexOf("0") <> 0 Or singleepisode.Season.value.Length = 1
                            singleepisode.Season.value = singleepisode.Season.value.Substring(1, singleepisode.Season.value.Length - 1)
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
                        Dim episodeurl As String = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & singleepisode.Season.value & "/" & singleepisode.Episode.Value & "/" & language & ".xml"
                        'Preferences.tvScraperLog &= "Trying Episode URL: " & episodeurl & vbCrLf
                        If Not UrlIsValid(episodeurl) Then
                            If sortorder.ToLower = "dvd" Then
                                tempsortorder = "default"
                                Preferences.tvScraperLog &= "WARNING: This episode could not be found on TVDB using DVD sort order" & vbCrLf
                                Preferences.tvScraperLog &= "Attempting to find using default sort order" & vbCrLf
                                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.Season.value & "/" & singleepisode.Episode.Value & "/" & language & ".xml"
                                Preferences.tvScraperLog &= "Now Trying Episode URL: " & episodeurl & vbCrLf
                            End If
                        End If

                        If UrlIsValid(episodeurl) Then


                            If Preferences.tvshow_useXBMC_Scraper = True Then
                                Dim FinalResult As String = ""
                                episodearray = XBMCScrape_TVShow_EpisodeDetails(tvdbid, tempsortorder, episodearray, language)
                                If episodearray.Count >= 1 Then
                                    For x As Integer = 0 To episodearray.Count - 1
                                        Preferences.tvScraperLog &= "Scraping body of episode: " & episodearray(x).Episode.Value & " - OK" & vbCrLf
                                    Next
                                    scrapedok = True
                                Else
                                    Preferences.tvScraperLog &= "WARNING: Could not locate this episode on TVDB, or TVDB may be unavailable" & vbCrLf
                                    scrapedok = False
                                End If
                                Exit For
                            End If

                            'Dim tempepisode As String = episodescraper.getepisode(tvdbid, tempsortorder, singleepisode.Season.value, singleepisode.episodeno, language)
                            Dim tempepisode As String = getepisode(tvdbid, tempsortorder, singleepisode.Season.value, singleepisode.Episode.Value, language)
                            scrapedok = True

                            '                            Exit For
                            If tempepisode = Nothing Then
                                scrapedok = False
                                Preferences.tvScraperLog &= "WARNING: This episode could not be found on TVDB" & vbCrLf
                            End If
                            If scrapedok = True Then
                                progresstext &= " OK."
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

                                progresstext &= " " & singleepisode.Title.Value
                                bckgroundscanepisodes.ReportProgress(progress, progresstext)

                                If actorsource = "imdb" Then
                                    Preferences.tvScraperLog &= "Scraping actors from IMDB" & vbCrLf
                                    progresstext &= " Actors..."
                                    bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                    Dim url As String
                                    url = "http://www.imdb.com/title/" & imdbid & "/episodes"
                                    Dim tvfblinecount As Integer = 0
                                    Dim tvdbwebsource(10000)
                                    tvfblinecount = 0
                                    If bckgroundscanepisodes.CancellationPending Then
                                        Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                        Exit Sub
                                    End If

                                    Dim wrGETURL As WebRequest
                                    wrGETURL = WebRequest.Create(url)
                                    Dim myProxy As New WebProxy("myproxy", 80)
                                    myProxy.BypassProxyOnLocal = True
                                    Dim objStream As Stream
                                    objStream = wrGETURL.GetResponse.GetResponseStream()
                                    Dim objReader As New StreamReader(objStream)
                                    Dim tvdbsLine As String = ""
                                    tvfblinecount = 0

                                    Do While Not tvdbsLine = ""   'is Nothing Changed by SK 18/5/2011     to avoid System.NullReferenceException was with new install & 1 new tvshow scraped & tried to serach for new episodes with 1 in the folder
                                        tvfblinecount += 1
                                        tvdbsLine = objReader.ReadLine
                                        If Not tvdbsLine = "" Then   '   Is Nothing Then Changed by SK 18/5/2011
                                            tvdbwebsource(tvfblinecount) = tvdbsLine
                                        End If
                                        If bckgroundscanepisodes.CancellationPending Then
                                            Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                            Exit Sub
                                        End If
                                    Loop
                                    objReader.Close()
                                    objStream.Close()
                                    tvfblinecount -= 1





                                    If tvfblinecount <> 0 Then
                                        Dim tvtempstring As String
                                        tvtempstring = "Season " & singleepisode.Season.value & ", Episode " & singleepisode.Episode.Value & ":"
                                        For g = 1 To tvfblinecount
                                            If tvdbwebsource(g).indexof(tvtempstring) <> -1 Then
                                                Dim tvtempint As Integer
                                                tvtempint = tvdbwebsource(g).indexof("<a href=""/title/")
                                                If tvtempint <> -1 Then
                                                    tvtempstring = tvdbwebsource(g).substring(tvtempint + 16, 9)
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
                                                            Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
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
                                                                                    Dim workingpath As String = episodearray(0).VideoFilePath.Replace(IO.Path.GetFileName(episodearray(0).VideoFilePath), "")
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

                                                                                End If
                                                                            End If
                                                                    End Select
                                                                    If bckgroundscanepisodes.CancellationPending Then
                                                                        Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                                        Exit Sub
                                                                    End If
                                                                Next
                                                                tempactorlist.Add(newactor)
                                                        End Select
                                                        If bckgroundscanepisodes.CancellationPending Then
                                                            Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                            Exit Sub
                                                        End If
                                                    Next





                                                    If tempactorlist.Count > 0 Then
                                                        Preferences.tvScraperLog &= "Actors scraped from IMDB OK" & vbCrLf
                                                        progresstext &= " OK."
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
                                                        Preferences.tvScraperLog &= "WARNING: Actors not scraped from IMDB, reverting to TVDB actorlist" & vbCrLf
                                                    End If

                                                    Exit For
                                                End If
                                            End If
                                            If bckgroundscanepisodes.CancellationPending Then
                                                Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                Exit Sub
                                            End If
                                        Next

                                    Else
                                        'tvscraperlog = tvscraperlog & "Unable To Get Actors From IMDB, Defaulting To TVDB" & vbCrLf
                                    End If
                                End If

                            End If

                            If Preferences.enablehdtags = True Then
                                progresstext &= " HD Tags..."
                                bckgroundscanepisodes.ReportProgress(progress, progresstext)

                                If Not singleepisode.Details.StreamDetails.Video.DurationInSeconds.Value Is Nothing Then

                                    '1h 24mn 48s 546ms
                                    Dim hours As Integer
                                    Dim minutes As Integer
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
                                    progresstext &= " OK."
                                    bckgroundscanepisodes.ReportProgress(progress, progresstext)
                                End If

                            End If
                        Else
                            Preferences.tvScraperLog &= "WARNING: Could not locate this episode on TVDB, or TVDB may be unavailable" & vbCrLf
                        End If
                    Else
                        Preferences.tvScraperLog &= "WARNING: No TVDB ID is available for this show, please scrape the show using the ""TV Show Selector"" TAB" & vbCrLf
                    End If

                Next

            End If
            If savepath <> "" And scrapedok = True Then
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                Dim newnamepath As String = ""



                '' Commented out the lines below so that the episodes are renamed irrelavent of scraper (MC or XBMC scraper) - not sure of their purpose. 
                'If Preferences.tvshow_useXBMC_Scraper = True Then
                '    newnamepath = savepath
                'Else
                newnamepath = addepisode(episodearray, savepath, showtitle)
                ''9999999                                                               'This was already commented out, it must be a note of some sort.
                For Each ep In episodearray
                    ep.VideoFilePath = newnamepath
                Next
                'End If
                bckgroundscanepisodes.ReportProgress(9999999, episodearray)
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                For Each Shows In TvShows
                    If episodearray(0).VideoFilePath.IndexOf(Shows.fullpath.Replace("\tvshow.nfo", "")) <> -1 Then
                        'workingtvshow = nfofunction.loadfulltnshownfo(Shows.fullpath)
                        For Each ept In episodearray
                            For j = Shows.missingepisodes.Count - 1 To 0 Step -1
                                If Shows.missingepisodes(j).title = ept.title Then
                                    Shows.missingepisodes.RemoveAt(j)
                                    Exit For
                                End If
                            Next
                        Next
                        For Each ep In episodearray
                            Dim newwp As New TvEpisode
                            newwp.Episode.Value = ep.Episode.Value
                            newwp.VideoFilePath = newnamepath
                            newwp.PlayCount.Value = "0"
                            newwp.Rating.Value = ep.Rating.Value
                            newwp.Season.value = ep.Season.value
                            newwp.title = ep.title
                            Shows.allepisodes.Add(newwp)
                        Next
                    End If
                Next
            End If
            Preferences.tvScraperLog &= vbCrLf
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

    Private Sub ListtvFiles(ByVal tvshow As TvShow, ByVal pattern As String)

        Dim episode As New List(Of TvEpisode)
        Dim propfile As Boolean = False
        Dim allok As Boolean = False


        Dim newlist As New List(Of String)
        newlist.Clear()

        newlist = Utilities.EnumerateFolders(tvshow.FolderPath, 6) 'TODO: Restore loging functions

        newlist.Insert(0, tvshow.FolderPath)
        If newlist.Count > 0 Then
            tvrebuildlog(newlist.Count - 1.ToString & " subfolders found in: " & newlist(0) & vbCrLf)
        End If
        For Each folder In newlist
            tvrebuildlog("Searching: " & vbCrLf & folder & vbCrLf & "for episodes")
            Dim dir_info As New System.IO.DirectoryInfo(folder)
            tvrebuildlog("Looking in " & folder)
            Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(pattern, SearchOption.TopDirectoryOnly)
            For Each fs_info As System.IO.FileInfo In fs_infos

                Try
                    Application.DoEvents()
                    If IO.Path.GetFileName(fs_info.FullName.ToLower) <> "tvshow.nfo" Then
                        tvrebuildlog("possible episode nfo found: " & fs_info.FullName)
                        episode = nfoFunction.loadbasicepisodenfo(fs_info.FullName)
                        If Not episode Is Nothing Then
                            For Each ep In episode
                                If ep.title <> Nothing Then
                                    Dim skip As Boolean = False
                                    For Each eps In tvshow.allepisodes
                                        If eps.Season.value = ep.Season.value And eps.Episode.Value = ep.Episode.Value And eps.VideoFilePath = ep.VideoFilePath Then
                                            skip = True
                                            Exit For
                                        End If
                                    Next
                                    If skip = False Then
                                        tvshow.allepisodes.Add(ep)
                                        tvrebuildlog("Episode appears to have loaded ok")
                                    End If
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    tvrebuildlog(ex.ToString)
                End Try
            Next fs_info
        Next
        tvrebuildlog(vbCrLf & vbCrLf & vbCrLf)

    End Sub

    Public Function tvCurrentlySelectedShow() As Nfo.TvShow
        If TvTreeview.SelectedNode Is Nothing Then Return Nothing
        Dim Show As Nfo.TvShow = Nothing
        Dim Season As Nfo.TvSeason = Nothing
        Dim Episode As Nfo.TvEpisode = Nothing

        If TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvShow Then
            Show = TvTreeview.SelectedNode.Tag
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvSeason Then
            Season = TvTreeview.SelectedNode.Tag
            Show = Season.GetParentShow
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvEpisode Then
            Episode = TvTreeview.SelectedNode.Tag
            Season = Episode.SeasonObj
            Show = Episode.ShowObj
        End If

        Return Show
    End Function

    Public Function tvCurrentlySelectedSeason() As Nfo.TvSeason
        If TvTreeview.SelectedNode Is Nothing Then Return Nothing

        Dim Show As Nfo.TvShow = Nothing
        Dim Season As Nfo.TvSeason = Nothing
        Dim Episode As Nfo.TvEpisode = Nothing

        If TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvShow Then
            Show = TvTreeview.SelectedNode.Tag
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvSeason Then
            Season = TvTreeview.SelectedNode.Tag
            Show = Season.GetParentShow
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvEpisode Then
            Episode = TvTreeview.SelectedNode.Tag
            Season = Episode.SeasonObj
            Show = Episode.ShowObj
        End If

        Return Season
    End Function

    Public Function tvCurrentlySelectedEpisode() As Nfo.TvEpisode
        If TvTreeview.SelectedNode Is Nothing Then Return Nothing

        Dim Show As Nfo.TvShow = Nothing
        Dim Season As Nfo.TvSeason = Nothing
        Dim Episode As Nfo.TvEpisode = Nothing

        If TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvShow Then
            Show = TvTreeview.SelectedNode.Tag
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvSeason Then
            Season = TvTreeview.SelectedNode.Tag
            Show = Season.GetParentShow
        ElseIf TypeOf TvTreeview.SelectedNode.Tag Is Nfo.TvEpisode Then
            Episode = TvTreeview.SelectedNode.Tag
            Season = Episode.SeasonObj
            Show = Episode.ShowObj
        End If

        Return Episode
    End Function

    Private Sub DownloadAvaileableMissingArtForShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadAvaileableMissingArtForShowToolStripMenuItem.Click
        DownloadMissingArt(tvCurrentlySelectedShow)
    End Sub

    Public Sub DownloadMissingArt(ByVal BrokenShow As TvShow)
        'Dim messbox As New frmMessageBox("Attempting to download art", "", "       Please Wait")
        'messbox.Show()
        'messbox.Refresh()
        Application.DoEvents()
        Try
            'Dim tvdbstuff As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
            Dim tvdbstuff As New TVDBScraper
            Dim showlist As New XmlDocument
            Dim thumblist As String = tvdbstuff.GetPosterList(BrokenShow.tvdbid)
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
                    Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season" & tempstring & ".tbn")
                    If tempstring = "00" Then
                        seasonpath = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season-specials.tbn")
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

                Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "fanart.jpg")
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

                Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "folder.jpg")
                If Not IO.File.Exists(seasonpath) Then
                    Try
                        Dim buffer(4000000) As Byte
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0
                        Dim thumburl As String = posterurlpath
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
                        Dim fstrm As New FileStream(seasonpath, FileMode.OpenOrCreate, FileAccess.Write)
                        fstrm.Write(buffer, 0, bytesRead)
                        contents.Close()
                        fstrm.Close()
                    Catch ex As WebException
#If SilentErrorScream Then
                        Throw ex
#End If
                        'MsgBox("Error Downloading main poster from TVDB")
                    End Try
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

                    Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season-all.tbn")
                    If Not IO.File.Exists(seasonpath) Or CheckBox6.CheckState = CheckState.Checked Then
                        Try
                            Dim buffer(4000000) As Byte
                            Dim size As Integer = 0
                            Dim bytesRead As Integer = 0
                            Dim thumburl As String = seasonallpath
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
                            Dim fstrm As New FileStream(seasonpath, FileMode.OpenOrCreate, FileAccess.Write)
                            fstrm.Write(buffer, 0, bytesRead)
                            contents.Close()
                            fstrm.Close()
                        Catch ex As WebException
#If SilentErrorScream Then
                            Throw ex
#End If
                            'MsgBox("Error Downloading main poster from TVDB")
                        End Try
                    End If
                End If
            ElseIf Preferences.seasonall <> "none" And seasonallpath <> "" Then
                Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season-all.tbn")
                If Not IO.File.Exists(seasonpath) Then
                    Try
                        Dim buffer(4000000) As Byte
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0
                        Dim thumburl As String = seasonallpath
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
                        Dim fstrm As New FileStream(seasonpath, FileMode.OpenOrCreate, FileAccess.Write)
                        fstrm.Write(buffer, 0, bytesRead)
                        contents.Close()
                        fstrm.Close()
                    Catch ex As WebException
                        'MsgBox("Error Downloading main poster from TVDB")
                    End Try
                End If
            End If
        Catch
        End Try
        Call LoadTvShow(BrokenShow)
        messbox.Close()

        TV_CleanFolderList()
    End Sub

    Private Sub TV_TvFilter(ByVal butt As String)
        If Not startup = True Then
            If butt = "missingeps" Then
                For Each item As Nfo.TvShow In TvShows
                    For Each Season As Nfo.TvSeason In item.Seasons.Values
                        For Each episode As Nfo.TvEpisode In Season.Episodes
                            If Not episode.IsMissing Then
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
            ElseIf butt = "screenshot" Then
                For Each item As Nfo.TvShow In TvShows
                    For Each Season As Nfo.TvSeason In item.Seasons.Values
                        For Each episode As Nfo.TvEpisode In Season.Episodes
                            If String.IsNullOrEmpty(episode.Thumbnail.FileName) Then
                                episode.Visible = False
                                episode.EpisodeNode.EnsureVisible()
                            Else
                                episode.Visible = True
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
                For Each item As Nfo.TvShow In TvShows
                    item.Visible = True
                    For Each Season As Nfo.TvSeason In item.Seasons.Values
                        For Each episode As Nfo.TvEpisode In Season.Episodes

                            episode.Visible = True

                        Next
                        Season.Visible = True
                    Next
                    item.Visible = True
                Next
            ElseIf butt = "fanart" Then
                For Each item As Nfo.TvShow In TvShows
                    item.Visible = True
                    For Each Season As Nfo.TvSeason In item.Seasons.Values
                        For Each episode As Nfo.TvEpisode In Season.Episodes
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
                For Each item As Nfo.TvShow In TvShows


                    For Each Season As Nfo.TvSeason In item.Seasons.Values
                        If Season.Poster.Exists Then
                            Season.Visible = False
                        Else
                            Season.Visible = True
                        End If
                        For Each Episode As Nfo.TvEpisode In Season.Episodes
                            Episode.Visible = False
                        Next
                    Next
                    item.Visible = Not item.ImagePoster.Exists

                Next
            End If
        End If
    End Sub

    Private Sub Bckgrndfindmissingepisodes_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Bckgrndfindmissingepisodes.DoWork
        Call findmissingepisodes()
    End Sub

    Private Sub Bckgrndfindmissingepisodes_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles Bckgrndfindmissingepisodes.ProgressChanged
        If e.ProgressPercentage = 1 Then
            If TypeOf e.UserState Is Nfo.TvEpisode Then
                Dim MissingEpisode As Nfo.TvEpisode = e.UserState

                MissingEpisode.ShowObj.AddEpisode(MissingEpisode)
            End If

            'Dim newshow As New Nfo.TvEpisode
            'newshow = e.UserState
            'For Each item In TvShows
            '    If item.fullpath = newshow.Episode.Then Then
            '        If Convert.ToInt32(newshow.Season.value) > 0 And newshow.Title <> "" And newshow.TvdbId = "true" Then
            '            Dim exists As Boolean = False
            '            For Each ep In item.allepisodes
            '                If ep.episodeno = newshow.episodeno And ep.Season.value = newshow.Season.value Then
            '                    exists = True
            '                    Exit For
            '                End If
            '            Next
            '            If exists = False Then
            '                Dim cnode As TreeNode = Nothing
            '                Dim shownode As Integer = -1
            '                For g = 0 To TvTreeview.Nodes.Count - 1
            '                    If TvTreeview.Nodes(g).Name.ToString = item.fullpath Then
            '                        cnode = TvTreeview.Nodes(g)
            '                        shownode = g
            '                        Exit For
            '                    End If
            '                Next

            '                Dim seasonstring As String = Nothing
            '                Dim seasonno As Integer = Convert.ToInt32(newshow.Season.value)
            '                Dim tempstring As String = String.Empty
            '                If seasonno <> 0 And seasonno <> -1 Then
            '                    If seasonno < 10 Then
            '                        tempstring = "Season 0" & seasonno.ToString
            '                    Else
            '                        tempstring = "Season " & seasonno.ToString
            '                    End If
            '                ElseIf seasonno = 0 Then
            '                    tempstring = "Specials"
            '                End If
            '                Dim node As TreeNode
            '                Dim alreadyexists As Boolean = False
            '                For Each node In cnode.Nodes
            '                    If node.Text = tempstring Then
            '                        alreadyexists = True
            '                        Exit For
            '                    End If
            '                Next
            '                If alreadyexists = False Then cnode.Nodes.Add(tempstring)
            '                Dim tempint As Integer
            '                For Each node In cnode.Nodes
            '                    If node.Text = tempstring Then
            '                        tempint = node.Index
            '                        Exit For
            '                    End If
            '                Next

            '                Dim eps As String
            '                Dim episodeno As Integer = Convert.ToInt32(newshow.episodeno)
            '                If episodeno < 10 Then
            '                    eps = "0" & episodeno.ToString
            '                Else
            '                    eps = episodeno.ToString
            '                End If
            '                eps = eps & " - " & newshow.Title
            '                Dim ccnode As TreeNode
            '                ccnode = TvTreeview.Nodes(shownode).Nodes(tempint)
            '                Dim tempstring2 As String = "Missing: " & eps
            '                alreadyexists = False
            '                For Each node In ccnode.Nodes
            '                    If node.Text = eps Then
            '                        alreadyexists = True
            '                        Exit For
            '                    End If
            '                Next
            '                If alreadyexists = False Then
            '                    ccnode.Nodes.Add(tempstring2, eps)
            '                    For Each no As TreeNode In ccnode.Nodes
            '                        If no.Name = tempstring2 Then
            '                            no.ForeColor = Color.Blue
            '                            no.Parent.ForeColor = Color.Blue
            '                            no.Parent.Parent.ForeColor = Color.Blue
            '                            Exit For
            '                        End If
            '                    Next
            '                    newshow.VideoFilePath = tempstring2
            '                    item.missingepisodes.Add(newshow)
            '                    ToolStripStatusLabel2.Text = "Adding: " & eps
            '                End If
            '            End If
            '        End If
            '    End If
            'Next
        Else
            ToolStripStatusLabel2.Text = e.UserState
        End If
    End Sub

    Private Sub Bckgrndfindmissingepisodes_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bckgrndfindmissingepisodes.RunWorkerCompleted
        ToolStripStatusLabel2.Visible = False
        ToolStripStatusLabel2.Text = "TV Show Episode Scan In Progress"

        TvTreeview.Sort()
    End Sub

    Private Sub findmissingepisodes()
        Utilities.EnsureFolderExists(IO.Path.Combine(Preferences.applicationPath, "missing\"))
        For Each item In TvShows

            Bckgrndfindmissingepisodes.ReportProgress(0, "Downloading episode data for: " & item.title)
            'If item.locked = Nfo.ShowState.Open Then
            Dim showid As String = item.tvdbid
            If IsNumeric(showid) Then
                'http://www.thetvdb.com/api/6E82FED600783400/series/85137/all/en.xml
                Dim language As String = ""
                If item.language <> "" Then
                    language = item.language
                Else
                    language = "en"
                End If
                Dim sortorder As String = item.sortorder
                Dim url As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & showid & "/all/" & language & ".xml"
                If sortorder = "" Then
                    sortorder = "default"
                End If
                Dim xmlfile As String

                xmlfile = Utilities.DownloadTextFiles(url)

                Dim SeriesInfo As New Tvdb.ShowData
                SeriesInfo.LoadXml(xmlfile)

                For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                    Dim AlreadyExists As Boolean = False
                    For Each ExistingEpisode As Nfo.TvEpisode In item.Episodes
                        If ExistingEpisode.Season.Value = NewEpisode.SeasonNumber.Value AndAlso ExistingEpisode.Episode.Value = NewEpisode.EpisodeNumber.Value Then
                            AlreadyExists = True
                            Exit For
                        End If
                    Next

                    If Not AlreadyExists Then
                        Dim MissingEpisode As New Nfo.TvEpisode

                        MissingEpisode.NfoFilePath = IO.Path.Combine(Preferences.applicationPath, "missing\" & item.tvdbid & "." & NewEpisode.SeasonNumber.Value & "." & NewEpisode.EpisodeNumber.Value & ".nfo")
                        MissingEpisode.AbsorbTvdbEpisode(NewEpisode)
                        MissingEpisode.IsMissing = True
                        MissingEpisode.ShowObj = item
                        MissingEpisode.Save()
                        Bckgrndfindmissingepisodes.ReportProgress(1, MissingEpisode)
                    End If
                Next
                'Try
                '    Dim ShowList As New XmlDocument
                '    ShowList.LoadXml(xmlfile)
                '    Dim thisresult As XmlNode = Nothing
                '    For Each thisresult In ShowList("Data")
                '        Select Case thisresult.Name
                '            Case "Episode"
                '                Dim newshow As New TvEpisode
                '                Dim premdate As String = String.Empty
                '                Dim aired As Boolean = True
                '                Dim mirrorselection As XmlNode = Nothing
                '                For Each mirrorselection In thisresult.ChildNodes
                '                    Select Case mirrorselection.Name
                '                        Case "DVD_episodenumber"
                '                            If sortorder = "dvd" Then
                '                                If mirrorselection.InnerText <> "" Then
                '                                    Dim temp As String = mirrorselection.InnerText
                '                                    If temp.IndexOf(".") <> -1 Then
                '                                        temp = temp.Substring(0, temp.IndexOf("."))
                '                                    End If
                '                                    newshow.episodeno = Convert.ToInt32(temp).ToString
                '                                Else
                '                                    sortorder = "default"
                '                                End If
                '                            End If
                '                        Case "EpisodeNumber"
                '                            If sortorder = "default" Then
                '                                newshow.episodeno = mirrorselection.InnerText
                '                            End If
                '                        Case "SeasonNumber"
                '                            newshow.Season.value = mirrorselection.InnerText
                '                        Case "EpisodeName"
                '                            newshow.title = mirrorselection.InnerText
                '                        Case "FirstAired"
                '                            premdate = mirrorselection.InnerText
                '                    End Select
                '                Next
                '                If premdate = "" Then
                '                    aired = False
                '                Else
                '                    If premdate <> "0000-00-00" Then
                '                        Try
                '                            Dim myDate2 As Date = System.DateTime.Now
                '                            Dim epdate As Date = CDate(premdate)
                '                            newshow.playcount = premdate
                '                            Dim strepdate As String
                '                            strepdate = Format(epdate, "yyyyMMdd")
                '                            Dim strcurrentdate As String
                '                            strcurrentdate = Format(myDate2, "yyyyMMdd")
                '                            Dim oldint As Integer = Convert.ToInt32(strepdate)
                '                            Dim newint As Integer = Convert.ToInt32(strcurrentdate)
                '                            If oldint > newint Then
                '                                aired = False
                '                            End If
                '                        Catch ex2 As Exception
                '                        End Try
                '                    Else
                '                        'MsgBox("boo")
                '                    End If
                '                End If

                '                If aired = True Then newshow.tvdbid = "true"
                '                If aired = False Then newshow.tvdbid = "false"
                '                newshow.VideoFilePath = item.fullpath
                '                Bckgrndfindmissingepisodes.ReportProgress(1, newshow)
                '        End Select
                '    Next
            End If
            ''End If
        Next
    End Sub

    Public Sub LoadTvCache(ByVal Text As String)
        TvCache.TvCachePath = Preferences.workingProfile.tvcache

        TvCache.Load()

        'Dirty work around until TvShows is repalced with TvCache.Shows universally
        For Each TvShow As Nfo.TvShow In TvCache.Shows
            'Dim NewShow As New TvShow
            'NewShow.LoadXml(TvShow.Node)
            'NewShow.NfoFilePath = TvShow.NfoFilePath
            'NewShow.UpdateTreenode()
            TvShows.Add(TvShow)

            'For Each Episode As Nfo.TvEpisode In TvShow.Episodes
            '    NewShow.AddEpisode(Episode)
            'Next

            TvTreeview.Nodes.Add(TvShow.ShowNode)
        Next
        TvTreeview.Sort()
    End Sub

    Public Sub TV_SaveTvData(ByVal Text As String)
        TvCache.TvCachePath = Preferences.workingProfile.tvcache
        TvCache.Clear()
        For Each TvShow In TvShows
            TvCache.Add(TvShow)
            'For Each Season As Nfo.TvSeason In TvShow.Seasons.Values
            '    TvCache.Add(Season)
            For Each Episode As Nfo.TvEpisode In TvShow.Episodes
                TvCache.Add(Episode)
            Next
            'Next
        Next


        TvCache.Save()
    End Sub
End Class
