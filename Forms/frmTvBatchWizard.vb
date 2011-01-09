Public Class tv_batch_wizard

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form1.tvbatchlist.activate = True
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox2.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox7.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowBody = True
        End If
        If CheckBox8.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowActors = True
        End If
        If CheckBox9.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowArt = True
        End If
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doShows = True
            Form1.tvbatchlist.doShowArt = True
        End If

        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeBody = True
        End If
        If CheckBox12.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeBody = True
        End If
        If CheckBox13.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeBody = True
        End If
        If CheckBox14.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeBody = True
        End If
        If CheckBox15.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeBody = True
        End If
        If CheckBox16.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeActors = True
        End If
        If CheckBox17.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeBody = True
        End If
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeArt = True
        End If
        If CheckBox20.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.doEpisodes = True
            Form1.tvbatchlist.doEpisodeMediaTags = True
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
            Form1.tvbatchlist.shYear = True
        Else
            Form1.tvbatchlist.shYear = False
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        'rating
        If CheckBox2.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shRating = True
        Else
            Form1.tvbatchlist.shRating = False
        End If
    End Sub
    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        'runtime
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shRuntime = True
        Else
            Form1.tvbatchlist.shRuntime = False
        End If
    End Sub
    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        'mpaa
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shMpaa = True
        Else
            Form1.tvbatchlist.shMpaa = False
        End If
    End Sub
    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        'studio
        If CheckBox7.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shStudio = True
        Else
            Form1.tvbatchlist.shStudio = False
        End If
    End Sub
    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        'actors
        If CheckBox8.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shActor = True
        Else
            Form1.tvbatchlist.shActor = False
        End If
    End Sub
    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        'genre
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shGenre = True
        Else
            Form1.tvbatchlist.shGenre = False
        End If
    End Sub
    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        'plot
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shPlot = True
        Else
            Form1.tvbatchlist.shPlot = False
        End If
    End Sub
    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        'missing posters
        If CheckBox9.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shPosters = True
        Else
            Form1.tvbatchlist.shPosters = False
        End If
    End Sub
    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        'missing fanart
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.shFanart = True
        Else
            Form1.tvbatchlist.shFanart = False
        End If
    End Sub

    'episode settings
    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        'plot
        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epPlot = True
        Else
            Form1.tvbatchlist.epPlot = False
        End If
    End Sub
    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        'air date
        If CheckBox12.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epAired = True
        Else
            Form1.tvbatchlist.epAired = False
        End If
    End Sub
    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        'director
        If CheckBox14.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epDirector = True
        Else
            Form1.tvbatchlist.epDirector = False
        End If
    End Sub
    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        'credits
        If CheckBox15.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epCredits = True
        Else
            Form1.tvbatchlist.epCredits = False
        End If
    End Sub
    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        'rating
        If CheckBox13.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epRating = True
        Else
            Form1.tvbatchlist.epRating = False
        End If
    End Sub
    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        'actors
        If CheckBox16.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epActor = True
        Else
            Form1.tvbatchlist.epActor = False
        End If
    End Sub
    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        'runtime
        If CheckBox17.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epRuntime = True
        Else
            Form1.tvbatchlist.epRuntime = False
        End If
    End Sub
    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        'media tags
        If CheckBox20.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epStreamDetails = True
        Else
            Form1.tvbatchlist.epStreamDetails = False
        End If
    End Sub
    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        'download screenshot
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epScreenshot = True
            CheckBox19.Enabled = True
        Else
            Form1.tvbatchlist.epScreenshot = False
            CheckBox19.Enabled = False
            CheckBox19.CheckState = CheckState.Unchecked
            Form1.tvbatchlist.epCreateScreenshot = False
        End If
    End Sub
    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        'create screenshot
        If CheckBox19.CheckState = CheckState.Checked Then
            Form1.tvbatchlist.epCreateScreenshot = True
        Else
            Form1.tvbatchlist.epCreateScreenshot = False
        End If
    End Sub
End Class