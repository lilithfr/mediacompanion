Public Class tv_batch_wizard

    Private Sub btnTvBatchStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvBatchStart.Click
        Try
            Form1.tvBatchList.activate = True
            If cbshYear.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshRating.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshPlot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshRuntime.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshMpaa.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshGenre.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshStudio.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowBody = True
            End If
            If cbshActor.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowActors = True
            End If
            If cbshPosters.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshFanart.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshXtraFanart.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If
            If cbshSeason.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doShows = True
                Form1.tvBatchList.doShowArt = True
            End If

            If cbepPlot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepAired.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepRating.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepDirector.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepCredits.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeBody = True
            End If
            If cbepActor.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeActors = True
            End If
            If cbepRuntime.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeMediaTags = True
            End If
            If cbepScreenshot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeArt = True
            End If
            If cbepStreamDetails.CheckState = CheckState.Checked Then
                Form1.tvBatchList.doEpisodes = True
                Form1.tvBatchList.doEpisodeMediaTags = True
            End If

            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_TvBatchCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TvBatchCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub



    'tv settings
    Private Sub cbshYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshYear.CheckedChanged
        Try
            'year
            If cbshYear.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shYear = True
            Else
                Form1.tvBatchList.shYear = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshRating.CheckedChanged
        Try
            'rating
            If cbshRating.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shRating = True
            Else
                Form1.tvBatchList.shRating = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshRuntime.CheckedChanged
        Try
            'runtime
            If cbshRuntime.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shRuntime = True
            Else
                Form1.tvBatchList.shRuntime = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshMpaa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshMpaa.CheckedChanged
        Try
            'mpaa
            If cbshMpaa.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shMpaa = True
            Else
                Form1.tvBatchList.shMpaa = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshStudio.CheckedChanged
        Try
            'studio
            If cbshStudio.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shStudio = True
            Else
                Form1.tvBatchList.shStudio = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshActor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshActor.CheckedChanged
        Try
            'actors
            If cbshActor.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shActor = True
            Else
                Form1.tvBatchList.shActor = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshGenre.CheckedChanged
        Try
            'genre
            If cbshGenre.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shGenre = True
            Else
                Form1.tvBatchList.shGenre = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshPlot.CheckedChanged
        Try
            'plot
            If cbshPlot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shPlot = True
            Else
                Form1.tvBatchList.shPlot = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbshPosters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshPosters.CheckedChanged
        Try
            'missing posters
            If cbshPosters.CheckState = CheckState.Checked Then
                If Preferences.tvposter Then
                    Form1.tvBatchList.shPosters = True
                Else
                    Form1.tvBatchList.shPosters = False
                    cbshPosters.Checked = False
                    MsgBox("TV Preferences set to not download Poster.", 48, "TV Preferences Selected!")
                End If
            Else
                Form1.tvBatchList.shPosters = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbshSeason_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshSeason.CheckedChanged
        Try
            'missing season art
            If cbshSeason.CheckState = CheckState.Checked Then
                Form1.tvBatchList.shSeason = True
            Else
                Form1.tvBatchList.shFanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbshFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbshFanart.CheckedChanged
        Try
            'missing fanart
            If cbshFanart.CheckState = CheckState.Checked Then
                If Preferences.tvfanart Then
                    Form1.tvBatchList.shFanart = True
                Else
                    Form1.tvBatchList.shFanart = False
                    cbshFanart.Checked = False
                    MsgBox("TV Preferences set to not download Fanart.", 48, "TV Preferences Selected!")
                End If
            Else
                Form1.tvBatchList.shFanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbshXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbshXtraFanart.CheckedChanged
        Try
            'missing Extra Fanart
            If cbshXtraFanart.CheckState = CheckState.Checked Then
                If Preferences.dlTVxtrafanart Then
                    Form1.tvBatchList.shXtraFanart = True
                Else
                    Form1.tvBatchList.shXtraFanart = False
                    cbshXtraFanart.Checked = False
                    MsgBox("TV Preferences set to not download Extra Fanart.", 48, "TV Preferences Selected!")
                End If
            Else
                Form1.tvBatchList.shXtraFanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub



    'episode settings
    Private Sub cbepPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepPlot.CheckedChanged
        Try
            'plot
            If cbepPlot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epPlot = True
            Else
                Form1.tvBatchList.epPlot = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepAired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepAired.CheckedChanged
        Try
            'air date
            If cbepAired.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epAired = True
            Else
                Form1.tvBatchList.epAired = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbepDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepDirector.CheckedChanged
        Try
            'director
            If cbepDirector.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epDirector = True
            Else
                Form1.tvBatchList.epDirector = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub cbepCredits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepCredits.CheckedChanged
        Try
            'credits
            If cbepCredits.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epCredits = True
            Else
                Form1.tvBatchList.epCredits = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepRating.CheckedChanged
        Try
            'rating
            If cbepRating.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epRating = True
            Else
                Form1.tvBatchList.epRating = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepActor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepActor.CheckedChanged
        Try
            'actors
            If cbepActor.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epActor = True
            Else
                Form1.tvBatchList.epActor = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepRuntime.CheckedChanged
        Try
            'runtime
            If cbepRuntime.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epRuntime = True
            Else
                Form1.tvBatchList.epRuntime = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepStreamDetails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepStreamDetails.CheckedChanged
        Try
            'media tags
            If cbepStreamDetails.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epStreamDetails = True
            Else
                Form1.tvBatchList.epStreamDetails = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepScreenshot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepScreenshot.CheckedChanged
        Try
            'download screenshot
            If cbepScreenshot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epScreenshot = True
                cbepCreateScreenshot.Enabled = True
            Else
                Form1.tvBatchList.epScreenshot = False
                cbepCreateScreenshot.Enabled = False
                cbepCreateScreenshot.CheckState = CheckState.Unchecked
                Form1.tvBatchList.epCreateScreenshot = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbepCreateScreenshot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbepCreateScreenshot.CheckedChanged
        Try
            'create screenshot
            If cbepCreateScreenshot.CheckState = CheckState.Checked Then
                Form1.tvBatchList.epCreateScreenshot = True
            Else
                Form1.tvBatchList.epCreateScreenshot = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub cbincludeLocked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbincludeLocked.CheckedChanged
        If cbincludeLocked.Checked = True Then
            Form1.tvBatchList.includeLocked = True
        Else
            Form1.tvBatchList.includeLocked = False
        End If
    End Sub
    Private Sub cbRewiteAllNfo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbRewiteAllNfo.CheckedChanged
        If cbRewiteAllNfo.Checked = True Then
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
            Else
                cbRewiteAllNfo.Checked = False
            End If
        Else
            Form1.tvBatchList.RewriteAllNFOs = False

            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
        End If
    End Sub
    Private Sub tv_batch_wizard_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbepStreamDetails.Enabled = Preferences.enabletvhdtags
    End Sub


End Class