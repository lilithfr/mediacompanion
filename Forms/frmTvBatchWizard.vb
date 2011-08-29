Public Class tv_batch_wizard

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form1.tvBatchList.activate = True
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox2.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox7.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowBody = True
        End If
        If CheckBox8.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowActors = True
        End If
        If CheckBox9.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowArt = True
        End If
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doShows = True
            Form1.tvBatchList.doShowArt = True
        End If

        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeBody = True
        End If
        If CheckBox12.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeBody = True
        End If
        If CheckBox13.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeBody = True
        End If
        If CheckBox14.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeBody = True
        End If
        If CheckBox15.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeBody = True
        End If
        If CheckBox16.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeActors = True
        End If
        If CheckBox17.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeBody = True
        End If
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeArt = True
        End If
        If CheckBox20.CheckState = CheckState.Checked Then
            Form1.tvBatchList.doEpisodes = True
            Form1.tvBatchList.doEpisodeMediaTags = True
        End If
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub



    'tv settings
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        'year
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shYear = True
        Else
            Form1.tvBatchList.shYear = False
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        'rating
        If CheckBox2.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shRating = True
        Else
            Form1.tvBatchList.shRating = False
        End If
    End Sub
    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        'runtime
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shRuntime = True
        Else
            Form1.tvBatchList.shRuntime = False
        End If
    End Sub
    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        'mpaa
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shMpaa = True
        Else
            Form1.tvBatchList.shMpaa = False
        End If
    End Sub
    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        'studio
        If CheckBox7.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shStudio = True
        Else
            Form1.tvBatchList.shStudio = False
        End If
    End Sub
    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        'actors
        If CheckBox8.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shActor = True
        Else
            Form1.tvBatchList.shActor = False
        End If
    End Sub
    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        'genre
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shGenre = True
        Else
            Form1.tvBatchList.shGenre = False
        End If
    End Sub
    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        'plot
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shPlot = True
        Else
            Form1.tvBatchList.shPlot = False
        End If
    End Sub
    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        'missing posters
        If CheckBox9.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shPosters = True
        Else
            Form1.tvBatchList.shPosters = False
        End If
    End Sub
    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        'missing fanart
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.tvBatchList.shFanart = True
        Else
            Form1.tvBatchList.shFanart = False
        End If
    End Sub

    'episode settings
    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        'plot
        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epPlot = True
        Else
            Form1.tvBatchList.epPlot = False
        End If
    End Sub
    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        'air date
        If CheckBox12.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epAired = True
        Else
            Form1.tvBatchList.epAired = False
        End If
    End Sub
    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        'director
        If CheckBox14.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epDirector = True
        Else
            Form1.tvBatchList.epDirector = False
        End If
    End Sub
    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        'credits
        If CheckBox15.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epCredits = True
        Else
            Form1.tvBatchList.epCredits = False
        End If
    End Sub
    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        'rating
        If CheckBox13.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epRating = True
        Else
            Form1.tvBatchList.epRating = False
        End If
    End Sub
    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        'actors
        If CheckBox16.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epActor = True
        Else
            Form1.tvBatchList.epActor = False
        End If
    End Sub
    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        'runtime
        If CheckBox17.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epRuntime = True
        Else
            Form1.tvBatchList.epRuntime = False
        End If
    End Sub
    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        'media tags
        If CheckBox20.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epStreamDetails = True
        Else
            Form1.tvBatchList.epStreamDetails = False
        End If
    End Sub
    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        'download screenshot
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epScreenshot = True
            CheckBox19.Enabled = True
        Else
            Form1.tvBatchList.epScreenshot = False
            CheckBox19.Enabled = False
            CheckBox19.CheckState = CheckState.Unchecked
            Form1.tvBatchList.epCreateScreenshot = False
        End If
    End Sub
    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        'create screenshot
        If CheckBox19.CheckState = CheckState.Checked Then
            Form1.tvBatchList.epCreateScreenshot = True
        Else
            Form1.tvBatchList.epCreateScreenshot = False
        End If
    End Sub
End Class