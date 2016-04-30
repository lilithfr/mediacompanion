Public Class frmBatchScraper

    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        Form1.rescrapeList.top250 = CheckBox22.Checked
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        Form1.rescrapeList.runtime = CheckBox8.Checked
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        Form1.rescrapeList.director = CheckBox12.Checked
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        Form1.rescrapeList.outline = CheckBox5.Checked
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        Form1.rescrapeList.mpaa = CheckBox9.Checked
    End Sub

    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        Form1.rescrapeList.premiered = CheckBox13.Checked
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Form1.rescrapeList.rating = CheckBox3.Checked
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        Form1.rescrapeList.plot = CheckBox6.Checked
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        Form1.rescrapeList.genre = CheckBox10.Checked
    End Sub

    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        Form1.rescrapeList.studio = CheckBox14.Checked
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        Form1.rescrapeList.votes = CheckBox4.Checked
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        Form1.rescrapeList.tagline = CheckBox7.Checked
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        Form1.rescrapeList.credits = CheckBox11.Checked
    End Sub

    Private Sub CheckBox21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox21.CheckedChanged
        Form1.rescrapeList.trailer = CheckBox21.Checked
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
         Form1.rescrapeList.actors = CheckBox15.Checked
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        Form1.rescrapeList.mediatags = CheckBox19.Checked
    End Sub

    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        Form1.rescrapeList.posterurls = CheckBox16.Checked 
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        Try
            If CheckBox17.CheckState = CheckState.Checked Then
                If Pref.savefanart Then
                    Form1.rescrapeList.missingfanart = True
                Else
                    Form1.rescrapeList.missingfanart = False
                    CheckBox17.Checked = False
                    MsgBox("Movie Preferences set to not download Fanart.", 48, "Movie Preferences")
                End If
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
                If Pref.scrapemovieposters Then
                    Form1.rescrapeList.missingposters = True
                Else
                    Form1.rescrapeList.missingposters = False
                    CheckBox18.Checked = False
                    MsgBox("Movie Preferences set to not download Posters.", 48, "Movie Preferences")
                End If
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
        Form1.rescrapeList.country = CheckBox1.checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Form1.rescrapeList.stars =CheckBox2.checked
    End Sub
    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        Form1.rescrapeList.year = CheckBox20.checked
    End Sub

    Private Sub cbFromTMDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFromTMDB.CheckedChanged
        Form1.rescrapeList.FromTMDB = cbFromTMDB.checked
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
            If Pref.basicsavemode OrElse Not Pref.MovieManualRename Then Return False
            If Pref.usefoldernames Then
                Dim tempint As Integer = MessageBox.Show("You currently have 'UseFolderName' Selected" & vbCrLf & "Are you sure you wish to Rename this Movie file" & vbCrLf & "Folder Renaming will still commence", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.No Then
                    Return False
                Else 
                    Return True
                End If
            End If
            If Pref.MovieManualRename Then Return True
        End Get
    End Property

    ReadOnly Property FoldersRenamable As Boolean
        Get
            Return Pref.MovieManualRename
        End Get
    End Property

    'Disabled controls don't show tool tips (friggin' MS poo), so need to filter invalid changes 
    Private Sub cbRenameFiles_CheckedChanged( sender As Object,  e As EventArgs) Handles cbRenameFiles.CheckedChanged
        If cbRenameFiles.Checked AndAlso FilesRenamable Then
            Form1.rescrapeList.Rename_Files = cbRenameFiles.Checked
        Else
            cbRenameFiles.Checked = False
        End If
    End Sub

    Private Sub cbRenameFolders_CheckedChanged( sender As Object,  e As EventArgs) Handles cbRenameFolders.CheckedChanged
        If FoldersRenamable Then
            Form1.rescrapeList.Rename_Folders = cbRenameFolders.Checked
        Else
            cbRenameFolders.Checked = False
        End If
    End Sub

    Private Sub cbTagsFromKeywords_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbTagsFromKeywords.CheckedChanged
        Form1.rescrapeList.TagsFromKeywords = cbTagsFromKeywords.Checked
    End Sub

    'Fix MS XP tool tip won't display a second time bug (more friggin' MS poo)
    Private Sub ForceToolTipDisplay( sender As System.Object,  e As System.EventArgs) Handles cbRenameFiles.MouseEnter,cbFrodo_Poster_Thumbs.MouseEnter,cbFrodo_Fanart_Thumbs.MouseEnter,cbRenameFolders.MouseEnter
        ttBatchUpdateWizard.Active = False
        ttBatchUpdateWizard.Active = True
    End Sub


    Private Sub cbFrodo_Poster_Thumbs_CheckedChanged( sender As Object,  e As EventArgs) Handles cbFrodo_Poster_Thumbs.CheckedChanged
        If Pref.FrodoEnabled Then
            Form1.rescrapeList.Frodo_Poster_Thumbs = cbFrodo_Poster_Thumbs.Checked
        Else
            cbFrodo_Poster_Thumbs.Checked = False
        End If
    End Sub
    
    Private Sub cbFrodo_Fanart_Thumbs_CheckedChanged( sender As Object,  e As EventArgs) Handles cbFrodo_Fanart_Thumbs.CheckedChanged
        If Pref.FrodoEnabled Then
            Form1.rescrapeList.Frodo_Fanart_Thumbs = cbFrodo_Fanart_Thumbs.Checked
        Else
            cbFrodo_Fanart_Thumbs.Checked = False
        End If
    End Sub

    Private Sub cbXtraFanart_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbXtraFanart.CheckedChanged
        Form1.rescrapeList.dlxtraart = cbXtraFanart.Checked 
    End Sub

    Private Sub cbFanartTv_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbFanartTv.CheckedChanged
        Form1.rescrapeList.ArtFromFanartTv = cbFanartTv.Checked 
    End Sub
    
    Private Sub cbMovSetArt_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbMovSetArt.CheckedChanged
        Form1.rescrapeList.missingmovsetart = cbMovSetArt.Checked 
    End Sub

    Private Sub cbDlTrailer_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbDlTrailer.CheckedChanged
        Form1.rescrapeList.Download_Trailer = cbDlTrailer.Checked 
    End Sub

    Private Sub cbTitle_CheckedChanged( sender As Object,  e As EventArgs) Handles cbTitle.CheckedChanged
        Form1.rescrapeList.title = cbTitle.Checked
    End Sub

    Private Sub frmBatchScraper_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then btnCancel.PerformClick 
    End Sub

    Private Sub cb_ScrapeEmptyTags_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cb_ScrapeEmptyTags.CheckedChanged
        Dim ChkBox As CheckBox = Nothing
        Dim Ischecked As Boolean = cb_ScrapeEmptyTags.CheckState 
        For Each xObject As Object In Me.GroupBox1.Controls
            If TypeOf xObject Is CheckBox Then
                ChkBox = xObject
                If Not ChkBox.Text.ToLower.Contains("tmdb") And Not ChkBox.Text.ToLower.Contains("trailer")Then
                ChkBox.Checked = Ischecked
                End If
            End If
        Next
        Form1.rescrapeList.EmptyMainTags = cb_ScrapeEmptyTags.checked
    End Sub

    Private Sub frmBatchScraper_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Bounds = Screen.AllScreens(Form1.CurrentScreen).Bounds
    End Sub
End Class