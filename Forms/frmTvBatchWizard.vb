Public Class tv_batch_wizard

    Private Sub btnTvBatchStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvBatchStart.Click
        
            Form1.tvBatchList.activate = True
            If cbshYear.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshRating.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshPlot.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshRuntime.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshMpaa.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshGenre.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshStudio.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshActor.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowActors = True
            End If
            If cbshStatus.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshPosters.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshFanart.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshXtraFanart.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshFanartTv.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshSeason.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If

            If cbepPlot.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepAired.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepRating.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepDirector.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepCredits.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepActor.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeActors = True
            End If
            If cbepRuntime.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeMediaTags = True
            End If
            If cbepTitle.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepScreenshot.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeArt = True
            End If
            If cbepStreamDetails.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeMediaTags = True
            End If

            Me.Close()
    End Sub

    Private Sub btn_TvBatchCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvBatchCancel.Click
        Me.Close()
    End Sub

    
    'tv settings
    Private Sub cbshYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshYear.CheckedChanged
        'year
        Form1.tvBatchList.shYear = cbshYear.Checked 
    End Sub
    Private Sub cbshRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshRating.CheckedChanged
        'rating
        Form1.tvBatchList.shRating = cbshRating.Checked 
    End Sub
    Private Sub cbshRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshRuntime.CheckedChanged
        'runtime
        Form1.tvBatchList.shRuntime = cbshRuntime.Checked 
    End Sub
    Private Sub cbshMpaa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshMpaa.CheckedChanged
        'mpaa
        Form1.tvBatchList.shMpaa = cbshMpaa.Checked 
    End Sub
    Private Sub cbshStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshStudio.CheckedChanged
        'studio
        Form1.tvBatchList.shStudio = cbshStudio.Checked 
    End Sub
    Private Sub cbshActor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshActor.CheckedChanged
        'actors
        Form1.tvBatchList.shActor = cbshActor.Checked 
    End Sub
    Private Sub cbshStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshStatus.CheckedChanged
        'Status
        Form1.tvBatchList.shStatus = cbshStatus.Checked
    End Sub
    Private Sub cbshGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshGenre.CheckedChanged
        'genre
        Form1.tvBatchList.shGenre = cbshGenre.checked
    End Sub
    Private Sub cbshPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshPlot.CheckedChanged
        'plot
        Form1.tvBatchList.shPlot = cbshPlot.Checked 
    End Sub

    'Tv Show Art
    Private Sub cbshPosters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshPosters.CheckedChanged
        'missing posters
        Dim IsChecked As Boolean = cbshPosters.checked
        Form1.tvBatchList.shPosters = IsChecked
        If Not IsChecked Then
            cbshBannerMain.Checked = CheckState.Unchecked
        End If
        cbshBannerMain.Enabled = IsChecked
    End Sub

    Private Sub cbshSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshSeason.CheckedChanged
        'missing season art
        Form1.tvBatchList.shSeason = cbshSeason.Checked 
    End Sub

    Private Sub cbshBannerMain_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshBannerMain.CheckedChanged
        'If season banner is not available, save series banner as season banner.
        Form1.tvBatchList.shBannerMain = cbshBannerMain.Checked
    End Sub

    Private Sub cbshFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshFanart.CheckedChanged
        'missing fanart
        Form1.tvBatchList.shFanart = cbshFanart.Checked 
    End Sub

    Private Sub cbshXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbshXtraFanart.CheckedChanged
        'missing Extra Fanart
        Form1.tvBatchList.shXtraFanart = cbshXtraFanart.Checked 
    End Sub

    Private Sub cbshFanartTv_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbshFanartTv.CheckedChanged
        'missing Fanart.Tv Art
        Form1.tvBatchList.shFanartTvArt = cbshFanartTv.Checked 
    End Sub

    Private Sub cbshDelArtwork_CheckedChanged( sender As Object,  e As EventArgs) Handles cbshDelArtwork.CheckedChanged
        If cbshDelArtwork.Checked = True Then
            cbshPosters.Checked = True
            cbshSeason.Checked = True
            cbshFanart.Checked = True
        End If
        Form1.tvBatchList.shDelArtwork = cbshDelArtwork.Checked
        Label3.Visible = cbshDelArtwork.Checked
    End Sub

    'episode settings
    Private Sub cbepPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepPlot.CheckedChanged
        'plot
        Form1.tvBatchList.epPlot = cbepPlot.Checked 
    End Sub
    Private Sub cbepAired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepAired.CheckedChanged
    'air date
    Form1.tvBatchList.epAired = cbepAired.Checked
    End Sub
    Private Sub cbepDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepDirector.CheckedChanged
        'director
        Form1.tvBatchList.epDirector = cbepDirector.Checked 
    End Sub
    Private Sub cbepCredits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepCredits.CheckedChanged
        'credits
        Form1.tvBatchList.epCredits = cbepCredits.checked
    End Sub
    Private Sub cbepRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepRating.CheckedChanged
        'rating
        Form1.tvBatchList.epRating = cbepRating.checked
    End Sub
    Private Sub cbepActor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepActor.CheckedChanged
        'actors
        Form1.tvBatchList.epActor = cbepActor.checked
    End Sub
    Private Sub cbepRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepRuntime.CheckedChanged
        'runtime
        Form1.tvBatchList.epRuntime = cbepRuntime.Checked 
    End Sub
    Private Sub cbepTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepTitle.CheckedChanged
        'Episode Title
        Form1.tvBatchList.epTitle = cbepTitle.Checked 
    End Sub
    Private Sub cbepStreamDetails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepStreamDetails.CheckedChanged
        'media tags
        Form1.tvBatchList.epStreamDetails = cbepStreamDetails.Checked 
    End Sub
    Private Sub cbepScreenshot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepScreenshot.CheckedChanged
        'download screenshot
        If cbepScreenshot.Checked Then
            Form1.tvBatchList.epScreenshot = True
            cbepCreateScreenshot.Enabled = True
        Else
            Form1.tvBatchList.epScreenshot = False
            cbepCreateScreenshot.Enabled = False
            cbepCreateScreenshot.CheckState = CheckState.Unchecked
            Form1.tvBatchList.epCreateScreenshot = False
        End If
    End Sub
    Private Sub cbepCreateScreenshot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepCreateScreenshot.CheckedChanged
        'create screenshot
        Form1.tvBatchList.epCreateScreenshot = cbepCreateScreenshot.Checked 
    End Sub

    Private Sub cbincludeLocked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbincludeLocked.CheckedChanged
        Form1.tvBatchList.includeLocked = cbincludeLocked.checked
    End Sub

    Private Sub cbRewriteAllNfo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbRewriteAllNfo.CheckedChanged
        If cbRewriteAllNfo.Checked = True Then
            Dim infotext As String = ""
            If Form1.tvBatchList.includeLocked = True Then
                infotext = "all of your"
            Else
                infotext = "your unlocked only"
            End If
            If MsgBox("You have selected " & infotext & " TVShows nfo's" & vbCrLf & "Do you want to rewite all of these nfo's?" & vbCrLf & "MC will read in the current nfo & write back only the data it uses in order to clean the nfo.", MsgBoxStyle.OkCancel, "Question?") = MsgBoxResult.Ok Then
                Form1.tvBatchList.RewriteAllNFOs = True
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                cbShSeries.Checked = CheckState.unchecked
            Else
                cbRewriteAllNfo.Checked = False
            End If
        Else
            Form1.tvBatchList.RewriteAllNFOs = False
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
        End If
    End Sub

    Private Sub cbShSeries_CheckedChanged(sender As Object, e As EventArgs) Handles cbShSeries.CheckedChanged
        If cbShSeries.Checked = True Then
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            Form1.tvBatchList.shSeries = True
            cbRewriteAllNfo.Checked = CheckState.unchecked
        Else
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
            Form1.tvBatchList.shSeries = False
        End If
    End Sub

    Private Sub tv_batch_wizard_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbepStreamDetails.Enabled = Pref.enabletvhdtags
        Dim TitleText As String = "Tv Batch Wizard - "
        If Form1.singleshow Then
            Dim workingshow As TvShow = Form1.tv_ShowSelectedCurrently(Form1.TvTreeview)
            TitleText += workingshow.Title.Value
        Else
            TitleText += "All Shows"
        End If
        Me.Text = TitleText
    End Sub

    Private Sub tv_batch_wizard_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then btn_TvBatchCancel.PerformClick 
    End Sub

End Class