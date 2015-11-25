Imports System.ComponentModel
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml


Public Class frmOptions
    Public Const SetDefaults = True
    Private fb As New FolderBrowserDialog
    Dim _Pref As New Pref
    Dim moviefolders As New List(Of str_RootPaths)
    Dim tvfolders As New List(Of String)
    Dim _changed As Boolean
    Dim prefsload As Boolean = False
    Dim videosourceprefchanged As Boolean = False
    Dim cleanfilenameprefchanged As Boolean = False
    
    Public Property Changes As Boolean
        Get
            Return _changed
        End Get
        Set(value As Boolean)
            _changed = value
        End Set
    End Property

#Region "Form Events"

    Private Sub options_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Changes Then
                Dim tempint As Integer = MessageBox.Show("You appear to have made changes to your preferences," & vbCrLf & "Do wish to save the changes", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.Yes Then
                    Pref.ConfigSave()
                Else
                    Pref.ConfigLoad()
                End If
            End If
            'For f = 0 To 33
            '    Pref.certificatepriority(f) = ListBox5.Items(f)
            'Next
            'For f = 0 To 3
            '    Pref.moviethumbpriority(f) = ListBox3.Items(f)
            'Next

            'If Pref.videomode = 4 Then
            '    If Not IO.File.Exists(Pref.selectedvideoplayer) Then
            '        MsgBox("You Have Not Selected Your Preferred Media Player")
            '        e.Cancel = True
            '        Exit Sub
            '    End If
            'End If

            'Pref.SaveConfig()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub frmOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            prefsload = True
            PrefInit()
            CommonInit()
            GeneralInit()
            
            prefsload = False
            Changes = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_SettingsApply_Click(sender As Object, e As EventArgs) Handles btn_SettingsApply.Click
        If cleanfilenameprefchanged OrElse videosourceprefchanged Then
            applyAdvancedLists()
        End If
        Pref.ConfigSave()
        Changes = False
    End Sub

    Private Sub btn_SettingsCancel_Click(sender As Object, e As EventArgs) Handles btn_SettingsCancel.Click
        Changes = False
        Pref.ConfigLoad()
    End Sub

    Private Sub btn_SettingsClose_Click(sender As Object, e As EventArgs) Handles btn_SettingsClose.Click
        Me.Close()
    End Sub
    
#End Region 'Form Events

    Private Sub PrefInit()
        'Initial
        Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
        Dim newFont As System.Drawing.Font
        If Not String.IsNullOrEmpty(Pref.font) Then
            Try
                newFont = CType(tcc.ConvertFromString(Pref.font), System.Drawing.Font)
            Catch ex As Exception
                newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
                Pref.font = "Times New Roman, 9pt"
            End Try
        Else
            newFont = CType(tcc.ConvertFromString("Times New Roman, 9pt"), System.Drawing.Font)
            Pref.font = "Times New Roman, 9pt"
        End If
        lbl_FontSample.Font = newFont
        lbl_FontSample.Text = Pref.font
    End Sub

    Private Sub CommonInit()
        'Common Section
        If Pref.XBMC_version = 0 Then
            rbXBMCv_pre.Checked = True
        ElseIf Pref.XBMC_version = 1 Then
            rbXBMCv_both.Checked = True
        ElseIf Pref.XBMC_version = 2 Then
            rbXBMCv_post.Checked = True
        End If
        CheckBox38                  .Checked    = Pref.intruntime
        cb_IgnoreThe                .Checked    = Pref.ignorearticle
        cb_IgnoreA                  .Checked    = Pref.ignoreAarticle
        cb_IgnoreAn                 .Checked    = Pref.ignoreAn
        cb_SorttitleIgnoreArticles  .Checked    = Pref.sorttitleignorearticle
        cbOverwriteArtwork          .Checked    = Not Pref.overwritethumbs
        cbDisplayRatingOverlay      .Checked    = Pref.DisplayRatingOverlay
        cbDisplayMediaInfoOverlay   .Checked    = Pref.DisplayMediainfoOverlay 
        cbDisplayMediaInfoFolderSize.Checked    = Pref.DisplayMediaInfoFolderSize
        cbShowAllAudioTracks        .Checked    = Pref.ShowAllAudioTracks
        AutoScrnShtDelay            .Text       = Pref.ScrShtDelay
        Pref.ExcludeFolders.PopTextBox(tbExcludeFolders)

        Movie.LoadBackDropResolutionOptions(comboBackDropResolutions, Pref.BackDropResolutionSI) 'SI = Selected Index
        Movie.LoadHeightResolutionOptions(comboPosterResolutions, Pref.PosterResolutionSI)
        Movie.LoadHeightResolutionOptions(comboActorResolutions, Pref.ActorResolutionSI)

        lbCleanFilename.Items.Clear()
        lbCleanFilename.Items.AddRange(Pref.moviecleanTags.Split("|"))
        If lbVideoSource.Items.Count <> Pref.releaseformat.Length Then
            lbVideoSource.Items.Clear()
            For f = 0 To Pref.releaseformat.Length - 1
                lbVideoSource.Items.Add(Pref.releaseformat(f))
            Next
        End If

        'Common - Actors Section
        cb_actorseasy               .Checked    = Pref.actorseasy 
        Select Case Pref.maxactors
            Case 9999
                ComboBox7.SelectedItem = "All Available"
            Case 0
                ComboBox7.SelectedItem = "None"
            Case Else
                ComboBox7.SelectedItem = Pref.maxactors.ToString
        End Select
        saveactorchkbx                      .Checked        = Pref.actorsave
        cb_LocalActorSaveAlpha              .Checked        = Pref.actorsavealpha
        localactorpath                      .Text           = Pref.actorsavepath
        xbmcactorpath                       .Text           = Pref.actornetworkpath
    End Sub

    Private Sub GeneralInit()
        'General Section
        If Pref.videomode = 1 Then
            rb_MediaPlayerDefault.Checked = True
        ElseIf Pref.videomode = 2 Then
            rb_MediaPlayerWMP.Checked = True
        ElseIf Pref.videomode = 4 Then
            rb_MediaPlayerUser.Checked = True
        End If

        If Pref.videomode = 4 Then
            lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            lbl_MediaPlayerUser.Visible = True
            btn_MediaPlayerBrowse.Enabled = True
        Else
            btn_MediaPlayerBrowse.Enabled = False
            lbl_MediaPlayerUser.Visible = False
        End If
        txtbx_minrarsize            .Text       = Pref.rarsize.ToString
        cbExternalbrowser           .Checked    = Pref.externalbrowser
        chkbx_disablecache          .Checked    = Not Pref.startupCache
        cbUseMultipleThreads        .Checked    = Pref.UseMultipleThreads
        cbShowLogOnError            .Checked    = Pref.ShowLogOnError
        cbCheckForNewVersion        .Checked    = Pref.CheckForNewVersion
        cbDisplayLocalActor         .Checked    = Pref.LocalActorImage
        cbRenameNFOtoINFO           .Checked    = Pref.renamenfofiles
        cbMultiMonitorEnable        .Checked    = Pref.MultiMonitoEnabled
        tbaltnfoeditor              .Text       = Pref.altnfoeditor
        tbMkvMergeGuiPath           .Text       = Pref.MkvMergeGuiPath
    End Sub
    

#Region "Common Tab"

#Region "Common Settings Tab"

    Private Sub rbXBMCv_pre_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_pre.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_pre.Checked Then
            Pref.XBMC_version = 0
        End If
        Changes = True
    End Sub

    Private Sub rbXBMCv_post_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_post.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_post.Checked Then
            Pref.XBMC_version = 2
        End If
        Changes = True
    End Sub

    Private Sub rbXBMCv_both_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbXBMCv_both.CheckedChanged
        If prefsload Then Exit Sub
        If rbXBMCv_both.Checked Then
            Pref.XBMC_version = 1
        End If
        Changes = True
    End Sub



    Private Sub CheckBox38_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox38.CheckedChanged
        If prefsload Then Exit Sub
        Pref.intruntime = CheckBox38.Checked
        Changes = True
    End Sub

    Private Sub cb_IgnoreThe_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreThe.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignorearticle = cb_IgnoreThe.Checked
        Changes = True
    End Sub

    Private Sub cb_IgnoreA_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreA.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignoreAarticle = cb_IgnoreA.Checked
        Changes = True
    End Sub

    Private Sub cb_IgnoreAn_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IgnoreAn.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ignoreAn = cb_IgnoreAn.Checked
        Changes = True
    End Sub

    Private Sub cb_SorttitleIgnoreArticles_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_SorttitleIgnoreArticles.CheckedChanged
        If prefsload Then Exit Sub
        Pref.sorttitleignorearticle = cb_SorttitleIgnoreArticles.Checked
        Changes = True
    End Sub

    Private Sub cbOverwriteArtwork_CheckedChanged(sender As Object, e As EventArgs) Handles cbOverwriteArtwork.CheckedChanged
        If prefsload Then Exit Sub
        Pref.overwritethumbs = Not cbOverwriteArtwork.Checked
        Changes = True
    End Sub

    Private Sub cbDisplayRatingOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayRatingOverlay.CheckedChanged
        If prefsload Then Exit Sub
        Pref.DisplayRatingOverlay = cbDisplayRatingOverlay.Checked
        Changes = True
    End Sub

    Private Sub cbDisplayMediaInfoOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayMediaInfoOverlay.CheckedChanged
        If prefsload Then Exit Sub
        Pref.DisplayMediainfoOverlay = cbDisplayMediaInfoOverlay.Checked
        Changes = True
    End Sub

    Private Sub cbDisplayMediaInfoFolderSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDisplayMediaInfoFolderSize.CheckedChanged
        If prefsload Then Exit Sub
        Pref.DisplayMediaInfoFolderSize = cbDisplayMediaInfoFolderSize.Checked
        Changes = True
    End Sub

    Private Sub AutoScrnShtDelay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AutoScrnShtDelay.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If AutoScrnShtDelay.Text <> "" Then
                    e.Handled = True
                Else
                    MsgBox("Please Enter at least 1")
                    AutoScrnShtDelay.Text = "10"
                End If
            End If
            If AutoScrnShtDelay.Text = "" Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                AutoScrnShtDelay.Text = "10"
                Exit Sub
            End If
            If Not IsNumeric(AutoScrnShtDelay.Text) Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                AutoScrnShtDelay.Text = "10"
                Exit Sub
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub AutoScrnShtDelay_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AutoScrnShtDelay.TextChanged
        If prefsload Then Exit Sub
        If IsNumeric(AutoScrnShtDelay.Text) AndAlso Convert.ToInt32(AutoScrnShtDelay.Text)>0 Then
            Pref.ScrShtDelay = Convert.ToInt32(AutoScrnShtDelay.Text)
        Else
            Pref.ScrShtDelay = 10
            AutoScrnShtDelay.Text = "10"
            MsgBox("Please enter a numerical Value that is 1 or more")
        End If
        Changes = True
    End Sub

    Private Sub tbExcludeFolders_Validating( sender As Object,  e As CancelEventArgs) Handles tbExcludeFolders.Validating
        If prefsload Then Exit Sub
        If Pref.ExcludeFolders.Changed(tbExcludeFolders) Then
            Changes = True
        End If
    End Sub

    Private Sub tbExcludeFolders_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbExcludeFolders.TextChanged
        'Preferences.ExcludeFolders = ExcludeFolders.Text
        Changes = True
    End Sub

'Image Resizing
    Private Sub comboActorResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboActorResolutions.SelectedIndexChanged
        Pref.ActorResolutionSI = comboActorResolutions.SelectedIndex
        Changes = True
    End Sub

    Private Sub comboPosterResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboPosterResolutions.SelectedIndexChanged
        Pref.PosterResolutionSI = comboPosterResolutions.SelectedIndex
        Changes = True
    End Sub

    Private Sub comboBackDropResolutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBackDropResolutions.SelectedIndexChanged
        Pref.BackDropResolutionSI = comboBackDropResolutions.SelectedIndex
        Changes = True
    End Sub

    Private Sub btnCleanFilenameAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnCleanFilenameAdd.Click
        lbCleanFilename.Items.Add(txtCleanFilenameAdd.Text)
        Changes = True
        cleanfilenameprefchanged = True
    End Sub

    Private Sub btnCleanFilenameRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnCleanFilenameRemove.Click
        lbCleanFilename.Items.RemoveAt(lbCleanFilename.SelectedIndex)
        Changes = True
        cleanfilenameprefchanged = True
    End Sub

    Private Sub btnVideoSourceAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnVideoSourceAdd.Click
        If txtVideoSourceAdd.Text <> "" Then        
            lbVideoSource.Items.Add(txtVideoSourceAdd.Text)
            Changes = True
            videosourceprefchanged = True
        End If
    End Sub

    Private Sub btnVideoSourceRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnVideoSourceRemove.Click
        Dim strSelected = lbVideoSource.SelectedItem
        Dim idxSelected = lbVideoSource.SelectedIndex
        Try
            lbVideoSource.Items.RemoveAt(idxSelected)
            Changes = True
            videosourceprefchanged = True
        Catch ex As Exception
        End Try
    End Sub

#End Region 'Common Settings Tab

#Region "Actors Tab"

    Private Sub cb_actorseasy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_actorseasy.CheckedChanged
        If prefsload Then Exit Sub
        Pref.actorseasy = cb_actorseasy.Checked
        Changes = True
    End Sub

    Private Sub saveactorchkbx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveactorchkbx.CheckedChanged
        'If prefsload Then Exit Sub
        If saveactorchkbx.CheckState = CheckState.Checked Then
            Pref.actorsave = True
            localactorpath.Text = Pref.actorsavepath
            xbmcactorpath.Text = Pref.actornetworkpath
            localactorpath.Enabled = True
            xbmcactorpath.Enabled = True
            cb_LocalActorSaveAlpha.Enabled = True
            btn_localactorpathbrowse.Enabled = True
        Else
            Pref.actorsave = False
            localactorpath.Text = ""
            xbmcactorpath.Text = ""
            localactorpath.Enabled = False
            xbmcactorpath.Enabled = False
            cb_LocalActorSaveAlpha.Enabled = False
            btn_localactorpathbrowse.Enabled = False
        End If
        Changes = True
    End Sub

    Private Sub cb_LocalActorSaveAlpha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_LocalActorSaveAlpha.CheckedChanged
        If prefsload Then Exit Sub
        Pref.actorsavealpha = cb_LocalActorSaveAlpha.CheckState
        Changes = True
    End Sub

    Private Sub btn_localactorpathbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_localactorpathbrowse.Click
        Try
            Dim thefoldernames As String
            fb.Description = "Please Select Folder to Save Actor Thumbnails)"
            fb.ShowNewFolderButton = True
            fb.RootFolder = System.Environment.SpecialFolder.Desktop
            fb.SelectedPath = Pref.lastpath
            If fb.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (fb.SelectedPath)
                localactorpath.Text = thefoldernames
                Pref.lastpath = thefoldernames
                Pref.actorsavepath = thefoldernames
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub localactorpath_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles localactorpath.TextChanged
        If prefsload Then Exit Sub
        Pref.actorsavepath = localactorpath.Text
        Changes = True
    End Sub

    Private Sub xbmcactorpath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xbmcactorpath.TextChanged
        If prefsload Then Exit Sub
        Pref.actornetworkpath = xbmcactorpath.Text
        Changes = True
    End Sub

#End Region 'Actors Tab

#End Region 'Common Tab

#Region "General"

    Private Sub rb_MediaPlayerDefault_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_MediaPlayerDefault.CheckedChanged
        If prefsload Then Exit Sub
        If rb_MediaPlayerDefault.Checked = True Then
            Pref.videomode = 1
        End If
        Changes = True
    End Sub

    Private Sub rb_MediaPlayerWMP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_MediaPlayerWMP.CheckedChanged
        If prefsload Then Exit Sub
        If rb_MediaPlayerWMP.Checked = True Then
            Pref.videomode = 2
        End If
        Changes = True
    End Sub

    Private Sub rb_MediaPlayerUser_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_MediaPlayerUser.CheckedChanged
        If Prefsload Then
            If rb_MediaPlayerUser.Checked AndAlso Not String.IsNullOrEmpty(Pref.selectedvideoplayer) Then
                lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            Else
                lbl_MediaPlayerUser.Text = ""
            End If
            Exit Sub
        End If
        If rb_MediaPlayerUser.Checked = True Then
            Pref.videomode = 4
            btn_MediaPlayerBrowse.Enabled = True
            lbl_MediaPlayerUser.Visible = True
            If Not String.IsNullOrEmpty(Pref.selectedvideoplayer) Then
                lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            Else
                lbl_MediaPlayerUser.Text = ""
            End If
        Else
            lbl_MediaPlayerUser.Visible = False
            btn_MediaPlayerBrowse.Enabled = False
        End If
        Changes = True
    End Sub

    Private Sub btn_MediaPlayerBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_MediaPlayerBrowse.Click
        If prefsload Then Exit Sub
        Try
            Dim filebrowser As New OpenFileDialog
            Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            filebrowser.InitialDirectory = mstrProgramFilesPath
            filebrowser.Filter = "Executable Files|*.exe"
            filebrowser.Title = "Find Executable Of Preferred Media Player"
            If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                Pref.selectedvideoplayer = filebrowser.FileName
                lbl_MediaPlayerUser.Visible = True
                lbl_MediaPlayerUser.Text = Pref.selectedvideoplayer
            End If
            If prefsload Then Exit Sub
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtbx_minrarsize.KeyPress
        If prefsload Then Exit Sub
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If txtbx_minrarsize.Text <> "" Then
                    e.Handled = True
                Else
                    MsgBox("Please Enter at least 0")
                    txtbx_minrarsize.Text = "8"
                End If
            End If
            If txtbx_minrarsize.Text = "" Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Exit Sub
            End If
            If Not IsNumeric(txtbx_minrarsize.Text) Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Exit Sub
            End If
            Pref.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtbx_minrarsize.TextChanged
        If prefsload Then Exit Sub
        If IsNumeric(txtbx_minrarsize.Text) Then
            Pref.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Else
            Pref.rarsize = 8
            txtbx_minrarsize.Text = "8"
        End If
        
        Changes = True
    End Sub
    
    Private Sub btnFontSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontSelect.Click
        Try
            Dim dlg As FontDialog = New FontDialog()
            Dim res As DialogResult = dlg.ShowDialog()
            If res = Windows.Forms.DialogResult.OK Then
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                Dim fontString As String = tc.ConvertToString(dlg.Font)

                Pref.font = fontString

                Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
                Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Pref.font), System.Drawing.Font)

                lbl_FontSample.Font = newFont
                lbl_FontSample.Text = fontString
                Changes = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnFontReset_Click(sender As System.Object, e As System.EventArgs) Handles btnFontReset.Click
        Try
            'Reset Font
            Pref.font = "Times New Roman, 9pt"
            Dim tcc As TypeConverter = TypeDescriptor.GetConverter(GetType(System.Drawing.Font))
            Dim newFont As System.Drawing.Font = CType(tcc.ConvertFromString(Pref.font), System.Drawing.Font)
            lbl_FontSample.Font = newFont
            lbl_FontSample.Text = "Times New Roman, 9pt"
            Changes = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnMkvMergeGuiPath_Click( sender As Object,  e As EventArgs) Handles btnMkvMergeGuiPath.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        ofd.Filter           = "Executable Files|*.exe"
        ofd.Title            = "Locate mkvmerge GUI (mmg.exe)"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then Pref.MkvMergeGuiPath = ofd.FileName
        Changes = True
    End Sub

    Private Sub llMkvMergeGuiPath_Click( sender As Object,  e As EventArgs) Handles llMkvMergeGuiPath.Click
        Form1.OpenUrl("http://www.downloadbestsoft.com/MKVToolNix.html")
    End Sub

    Private Sub lblaltnfoeditorclear_Click( sender As Object,  e As EventArgs) Handles lblaltnfoeditorclear.Click
        tbaltnfoeditor.Text = ""
        Pref.altnfoeditor = ""
        Changes = True
    End Sub

    Private Sub btnaltnfoeditor_Click( sender As Object,  e As EventArgs) Handles btnaltnfoeditor.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        ofd.Filter           = "Executable Files|*.exe"
        ofd.Title            = "Locate Alternative nfo viewer-editor"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then 
            Pref.altnfoeditor = ofd.FileName
            tbaltnfoeditor.Text = Pref.altnfoeditor 
            Changes = True
        End If
    End Sub

    Private Sub cbExternalbrowser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExternalbrowser.CheckedChanged
        If prefsload Then Exit Sub
        Pref.externalbrowser = cbExternalbrowser.Checked
        btnFindBrowser.Enabled = cbExternalbrowser.Checked
        Changes = True
    End Sub

    Private Sub chkbx_disablecache_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_disablecache.CheckedChanged
        If prefsload Then Exit Sub
        Pref.startupCache = Not chkbx_disablecache.Checked
        Changes = True
    End Sub

    Private Sub cbUseMultipleThreads_CheckedChanged( sender As Object,  e As EventArgs) Handles cbUseMultipleThreads.CheckedChanged
        If prefsload Then Exit Sub
        Pref.UseMultipleThreads = cbUseMultipleThreads.Checked
        If prefsload Then Exit Sub
        Changes = True
    End Sub

    Private Sub cbShowLogOnError_CheckedChanged( sender As Object,  e As EventArgs) Handles cbShowLogOnError.CheckedChanged
        If prefsload Then Exit Sub
        Pref.ShowLogOnError = cbShowLogOnError.Checked
        If prefsload Then Exit Sub
        Changes = True
    End Sub

    Private Sub cbCheckForNewVersion_CheckedChanged( sender As Object,  e As EventArgs) Handles cbCheckForNewVersion.CheckedChanged
        If prefsload Then Exit Sub
        Pref.CheckForNewVersion = cbCheckForNewVersion.Checked
        If prefsload Then Exit Sub
        Changes = True
    End Sub

    Private Sub cbDisplayLocalActor_CheckedChanged( sender As System.Object,  e As System.EventArgs) Handles cbDisplayLocalActor.CheckedChanged
        If prefsload Then Exit Sub
        Pref.LocalActorImage = cbDisplayLocalActor.Checked = True
        Changes = True
    End Sub

    Private Sub cbRenameNFOtoINFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRenameNFOtoINFO.CheckedChanged
        If prefsload Then Exit Sub
        Pref.renamenfofiles = cbRenameNFOtoINFO.Checked
        Changes = True
    End Sub

    Private Sub cbMultiMonitorEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMultiMonitorEnable.CheckedChanged
        If prefsload Then Exit Sub
        Pref.MultiMonitoEnabled = cbMultiMonitorEnable.Checked
        Changes = True
    End Sub
    

#End Region 'General

#Region "Movie Preferences"

#End Region 'Movie Preferences

#Region "TV Preferences"

#End Region 'TV Preferences

#Region "Proxy"
    'Handled by user control ucGenPref_Proxy
#End Region 'Proxy

#Region "XBMC Link"
    'Handles by user control ucGenPref_XbmcLink
#End Region 'XBMC Link

#Region "Profiles & Commands"

#End Region 'Profiles & Commands

    Private Sub applyAdvancedLists()
        If cleanfilenameprefchanged Then
            Dim strTemp As String = ""
            For i = 0 To lbCleanFilename.Items.Count - 1
                strTemp &= lbCleanFilename.Items(i) & "|"
            Next
            Pref.moviecleanTags = strTemp.TrimEnd("|")
            cleanfilenameprefchanged = False
        End If
        If videosourceprefchanged Then
            Dim count As Integer = lbVideoSource.Items.Count - 1
            ReDim Pref.releaseformat(count)
            For g = 0 To count
                Pref.releaseformat(g) = lbVideoSource.Items(g)
            Next
            Form1.mov_VideoSourcePopulate()
            Form1.ep_VideoSourcePopulate()
            videosourceprefchanged = False
        End If
    End Sub

End Class