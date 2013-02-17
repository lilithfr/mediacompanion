Public Class frmBatchScraper

    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        Try
            If CheckBox22.CheckState = CheckState.Checked Then
                Form1.rescrapeList.top250 = True
            Else
                Form1.rescrapeList.top250 = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        Try
            If CheckBox8.CheckState = CheckState.Checked Then
                Form1.rescrapeList.runtime = True
            Else
                Form1.rescrapeList.runtime = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        Try
            If CheckBox12.CheckState = CheckState.Checked Then
                Form1.rescrapeList.director = True
            Else
                Form1.rescrapeList.director = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        Try
            If CheckBox5.CheckState = CheckState.Checked Then
                Form1.rescrapeList.outline = True
            Else
                Form1.rescrapeList.outline = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        Try
            If CheckBox9.CheckState = CheckState.Checked Then
                Form1.rescrapeList.mpaa = True
            Else
                Form1.rescrapeList.mpaa = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        Try
            If CheckBox13.CheckState = CheckState.Checked Then
                Form1.rescrapeList.premiered = True
            Else
                Form1.rescrapeList.premiered = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Try
            If CheckBox3.CheckState = CheckState.Checked Then
                Form1.rescrapeList.rating = True
            Else
                Form1.rescrapeList.rating = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        Try
            If CheckBox6.CheckState = CheckState.Checked Then
                Form1.rescrapeList.plot = True
            Else
                Form1.rescrapeList.plot = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        Try
            If CheckBox10.CheckState = CheckState.Checked Then
                Form1.rescrapeList.genre = True
            Else
                Form1.rescrapeList.genre = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        Try
            If CheckBox14.CheckState = CheckState.Checked Then
                Form1.rescrapeList.studio = True
            Else
                Form1.rescrapeList.studio = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        Try
            If CheckBox4.CheckState = CheckState.Checked Then
                Form1.rescrapeList.votes = True
            Else
                Form1.rescrapeList.votes = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        Try
            If CheckBox7.CheckState = CheckState.Checked Then
                Form1.rescrapeList.tagline = True
            Else
                Form1.rescrapeList.tagline = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        Try
            If CheckBox11.CheckState = CheckState.Checked Then
                Form1.rescrapeList.credits = True
            Else
                Form1.rescrapeList.credits = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox21.CheckedChanged
        Try
            If CheckBox21.CheckState = CheckState.Checked Then
                Form1.rescrapeList.trailer = True
            Else
                Form1.rescrapeList.trailer = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        Try
            If CheckBox15.CheckState = CheckState.Checked Then
                Form1.rescrapeList.actors = True
            Else
                Form1.rescrapeList.actors = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        Try
            If CheckBox19.CheckState = CheckState.Checked Then
                Form1.rescrapeList.mediatags = True
            Else
                Form1.rescrapeList.mediatags = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        Try
            If CheckBox16.CheckState = CheckState.Checked Then
                Form1.rescrapeList.posterurls = True
            Else
                Form1.rescrapeList.posterurls = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        Try
            If CheckBox17.CheckState = CheckState.Checked Then
                Form1.rescrapeList.missingfanart = True
            Else
                Form1.rescrapeList.missingfanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        Try
            If CheckBox18.CheckState = CheckState.Checked Then
                Form1.rescrapeList.missingposters = True
            Else
                Form1.rescrapeList.missingposters = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Form1.rescrapeList.ResetFields
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If CheckBox1.CheckState = CheckState.Checked Then
                Form1.rescrapeList.country = True
            Else
                Form1.rescrapeList.country = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Try
            If CheckBox2.CheckState = CheckState.Checked Then
                Form1.rescrapeList.stars = True
            Else
                Form1.rescrapeList.stars = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        Try
            If CheckBox20.CheckState = CheckState.Checked Then
                Form1.rescrapeList.year = True
            Else
                Form1.rescrapeList.year = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Public Sub New()
        
        ' This call is required by the designer.
        InitializeComponent()
        
        ' Add any initialization after the InitializeComponent() call.
        
    End Sub

    
    Private Sub cbTmdbSetName_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbTmdbSetName.CheckedChanged
        Form1.rescrapeList.tmdb_set_name = cbTmdbSetName.Checked
    End Sub


    ReadOnly Property FilesRenamable As Boolean
        Get
            Return Not Preferences.usefoldernames And Not Preferences.basicsavemode And Preferences.MovieRenameEnable
        End Get
    End Property




    'Disabled controls don't show tool tips (friggin' MS poo), so need to filter invalid changes 
    ' 
    Private Sub cbRenameFiles_CheckedChanged( sender As Object,  e As EventArgs) Handles cbRenameFiles.CheckedChanged
        If FilesRenamable Then
            Form1.rescrapeList.Rename_Files = cbRenameFiles.Checked
        Else
            cbRenameFiles.Checked = False
        End If
    End Sub

    'Fix MS XP tool tip won't display a second time bug (more friggin' MS poo)
    Private Sub ForceToolTipDisplay( sender As System.Object,  e As System.EventArgs) Handles cbRenameFiles.MouseEnter,cbFrodo_Poster_Thumbs.MouseEnter,cbFrodo_Fanart_Thumbs.MouseEnter
        ttBatchUpdateWizard.Active = False
        ttBatchUpdateWizard.Active = True
    End Sub


    Private Sub cbFrodo_Poster_Thumbs_CheckedChanged( sender As Object,  e As EventArgs) Handles cbFrodo_Poster_Thumbs.CheckedChanged
        If Preferences.FrodoEnabled Then
            Form1.rescrapeList.Frodo_Poster_Thumbs = cbFrodo_Poster_Thumbs.Checked
        Else
            cbFrodo_Poster_Thumbs.Checked = False
        End If
    End Sub


    Private Sub cbFrodo_Fanart_Thumbs_CheckedChanged( sender As Object,  e As EventArgs) Handles cbFrodo_Fanart_Thumbs.CheckedChanged
        If Preferences.FrodoEnabled Then
            Form1.rescrapeList.Frodo_Fanart_Thumbs = cbFrodo_Fanart_Thumbs.Checked
        Else
            cbFrodo_Fanart_Thumbs.Checked = False
        End If
    End Sub


    Private Sub cbTitle_CheckedChanged( sender As Object,  e As EventArgs) Handles cbTitle.CheckedChanged
        Form1.rescrapeList.title = cbTitle.Checked
    End Sub


End Class