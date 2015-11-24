Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml


Public Class frmOptions
    Public Const SetDefaults = True
    'Dim Preferences As New _Pref.Preferences
    Dim moviefolders As New List(Of str_RootPaths)
    Dim tvfolders As New List(Of String)


    Private Sub options_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
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
            Select Case Pref.seasonall
                Case "none"
                    RadioButton6.Checked = True
                Case "poster"
                    RadioButton9.Checked = True
                Case "wide"
                    RadioButton10.Checked = True
            End Select

            If Pref.tvshowrefreshlog = True Then
                CheckBox11.CheckState = CheckState.Checked
            Else
                CheckBox11.CheckState = CheckState.Unchecked
            End If

            For Each Regex In Form1.tv_RegexScraper
                ListBox7.Items.Add(Regex)
            Next
            ComboBox3.SelectedIndex = Pref.tvrename

            If Pref.roundminutes = True Then
                CheckBox6.CheckState = CheckState.Checked
            ElseIf Pref.roundminutes = False Then
                CheckBox6.CheckState = CheckState.Unchecked
            End If

            If Pref.externalbrowser = True Then
                CheckBox5.CheckState = CheckState.Checked
            ElseIf Pref.externalbrowser = False Then
                CheckBox5.CheckState = CheckState.Unchecked
            End If

            If Pref.videomode = 1 Then
                rb_MediaPlayerDefault.Checked = True
            ElseIf Pref.videomode = 2 Then
                rb_MediaPlayerWMP.Checked = True
            ElseIf Pref.videomode = 4 Then
                rb_MediaPlayerUser.Checked = True
            End If
            lbl_MediaPlayerUser.Text = "Custom Player - " & Pref.selectedvideoplayer
            If Pref.overwritethumbs = True Then
                CheckBox1.Checked = CheckState.Unchecked
            Else
                CheckBox1.Checked = CheckState.Checked
            End If

            'If Pref.keepfoldername = True Then
            CheckBox10.CheckState = CheckState.Checked
            'Else
            '    CheckBox10.CheckState = CheckState.Unchecked
            'End If


            If Pref.disabletvlogs = True Then
                CheckBox18.CheckState = CheckState.Checked
            Else
                CheckBox18.CheckState = CheckState.Unchecked
            End If

            If IsNumeric(Pref.rarsize) Then
                Dim tempint As Integer = Pref.rarsize
                If tempint <= 0 Then
                    tempint = 8
                    Pref.rarsize = 8
                End If
                txtbx_minrarsize.Text = tempint.ToString
            End If


            'If Pref.resizefanart = 1 Then
            '    RadioButton17.Checked = True
            'ElseIf Pref.resizefanart = 2 Then
            '    RadioButton18.Checked = True
            'ElseIf Pref.resizefanart = 3 Then
            '    RadioButton19.Checked = True
            'End If

            moviefolders = Pref.movieFolders
            tvfolders = Pref.tvFolders



            ListBox2.Items.Clear()
            For Each item In Pref.movieFolders
                ListBox2.Items.Add(item)
            Next

            For Each item In tvfolders
                ListBox1.Items.Add(item)
            Next

            If Pref.gettrailer = True Then
                CheckBox4.CheckState = CheckState.Checked
            Else
                CheckBox4.CheckState = CheckState.Unchecked
            End If

            If Pref.startupCache = True Then
                chkbx_disablecache.CheckState = CheckState.Unchecked
            Else
                chkbx_disablecache.CheckState = CheckState.Checked
            End If

            If Pref.enablehdtags = True Then
                CheckBox22.CheckState = CheckState.Checked
            Else
                CheckBox22.CheckState = CheckState.Unchecked
            End If

            If IsNumeric(Pref.rarsize) Then
                txtbx_minrarsize.Text = Pref.rarsize.ToString
            Else
                txtbx_minrarsize.Text = "8"
            End If

            If Pref.actorsave = True Then
                localactorpath.Enabled = True
                xbmcactorpath.Enabled = True
                xbmcactorpath.Text = Pref.actornetworkpath
                localactorpath.Text = Pref.actorsavepath
                Button1.Enabled = True
                saveactorchkbx.CheckState = CheckState.Checked
            Else
                'localactorpath.Enabled = False
                'xbmcactorpath.Enabled = False
                'Button1.Enabled = False
                saveactorchkbx.CheckState = CheckState.Unchecked
            End If

            'If Pref.keepfoldername = False Then
            CheckBox10.CheckState = CheckState.Unchecked
            'Else
            '    CheckBox10.CheckState = CheckState.Checked
            'End If

            If Pref.ignoretrailers = False Then
                chkbx_ignoretrailers.CheckState = CheckState.Unchecked
            Else
                chkbx_ignoretrailers.CheckState = CheckState.Checked
            End If

            For f = 0 To 3
                ListBox3.Items.Add(Pref.moviethumbpriority(f))
            Next
            For f = 0 To 33
                ListBox5.Items.Add(Pref.certificatepriority(f))
            Next

            If Pref.tvdlfanart = True Then
                CheckBox9.CheckState = CheckState.Checked
            Else
                CheckBox9.CheckState = CheckState.Unchecked
            End If

            If Pref.tvdlposter = True Then
                CheckBox8.CheckState = CheckState.Checked
            Else
                CheckBox8.CheckState = CheckState.Unchecked
            End If





            If Pref.tvdlseasonthumbs = True Then
                CheckBox7.CheckState = CheckState.Checked
            Else
                CheckBox7.CheckState = CheckState.Unchecked
            End If


            If Pref.posternotstacked = True Then
                chkbx_unstackposternames.CheckState = CheckState.Checked
            Else
                chkbx_unstackposternames.CheckState = CheckState.Unchecked
            End If

            If Pref.fanartnotstacked = True Then
                chkbx_unstackfanartnames.CheckState = CheckState.Checked
            Else
                chkbx_unstackfanartnames.CheckState = CheckState.Unchecked
            End If

            If Pref.scrapemovieposters = False Then
                CheckBox18.CheckState = CheckState.Unchecked
            Else
                CheckBox18.CheckState = CheckState.Checked
            End If

            If Pref.dontdisplayposter = False Then
                Chkbx_fanartnoposter.CheckState = CheckState.Unchecked
            Else
                Chkbx_fanartnoposter.CheckState = CheckState.Checked
            End If


            Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
            If intX <= 861 Or intY <= 580 Then
                Me.AutoScroll = True
            End If

            If Pref.renamenfofiles = False Then
                chkbx_renamnfofiles.Checked = False
            Else
                chkbx_renamnfofiles.Checked = True
            End If

            If Pref.disabletvlogs = True Then
                CheckBox15.CheckState = CheckState.Checked
            Else
                CheckBox15.CheckState = CheckState.Unchecked
            End If

            TPGenOld.BackColor = Form1.BackColor
            TabPage2.BackColor = Form1.BackColor
            TabPage3.BackColor = Form1.BackColor
            ListBox4.SelectedItem = Pref.imdbmirror


            Select Case Pref.maxactors
                Case 9999
                    ComboBox1.SelectedItem = "All Available"
                Case 0
                    ComboBox1.SelectedItem = "None"
                Case 5
                    ComboBox1.SelectedItem = "5"
                Case 10
                    ComboBox1.SelectedItem = "10"
                Case 15
                    ComboBox1.SelectedItem = "15"
                Case 20
                    ComboBox1.SelectedItem = "20"
                Case 25
                    ComboBox1.SelectedItem = "25"
                Case 30
                    ComboBox1.SelectedItem = "30"
                Case 40
                    ComboBox1.SelectedItem = "40"
                Case 50
                    ComboBox1.SelectedItem = "50"
                Case 70
                    ComboBox1.SelectedItem = "70"
                Case 90
                    ComboBox1.SelectedItem = "90"
                Case 110
                    ComboBox1.SelectedItem = "110"
                Case 125
                    ComboBox1.SelectedItem = "125"
                Case 150
                    ComboBox1.SelectedItem = "150"
                Case 175
                    ComboBox1.SelectedItem = "175"
                Case 200
                    ComboBox1.SelectedItem = "200"
                Case 250
                    ComboBox1.SelectedItem = "250"
            End Select

            Select Case Pref.maxmoviegenre
                Case 9999
                    ComboBox2.SelectedItem = "All Available"
                Case 0
                    ComboBox2.SelectedItem = "None"
                Case 1
                    ComboBox2.SelectedItem = "1"
                Case 2
                    ComboBox2.SelectedItem = "2"
                Case 3
                    ComboBox2.SelectedItem = "3"
                Case 4
                    ComboBox2.SelectedItem = "4"
                Case 5
                    ComboBox2.SelectedItem = "5"
                Case 6
                    ComboBox2.SelectedItem = "6"
                Case 7
                    ComboBox2.SelectedItem = "7"
                Case 8
                    ComboBox2.SelectedItem = "8"
                Case 9
                    ComboBox2.SelectedItem = "9"
                Case 10
                    ComboBox2.SelectedItem = "10"
            End Select


            'If Pref.resizefanart = 1 Then
            '    RadioButton17.Checked = True
            'ElseIf Pref.resizefanart = 2 Then
            '    RadioButton18.Checked = True
            'ElseIf Pref.resizefanart = 3 Then
            '    RadioButton19.Checked = True
            'Else
            '    Pref.resizefanart = 1
            '    RadioButton17.Checked = True
            'End If



            If Pref.usefanart = True Then chkbxfanart.CheckState = CheckState.Checked
            If Pref.usefanart = False Then chkbxfanart.CheckState = CheckState.Unchecked
            If Pref.remembersize = True Then chk_rememberformsize.CheckState = CheckState.Checked
            If Pref.remembersize = False Then chk_rememberformsize.CheckState = CheckState.Unchecked


            Me.BackColor = Form1.BackColor


            If Pref.usefoldernames = True Then
                chkbx_usefoldernames.CheckState = CheckState.Checked
                chkbx_createfolderjpg.Enabled = True
                chkbx_basicsave.Enabled = True
                If Pref.createfolderjpg = True Then
                    chkbx_createfolderjpg.CheckState = CheckState.Checked
                Else
                    chkbx_createfolderjpg.CheckState = CheckState.Unchecked
                End If
                If Pref.basicsavemode = True Then
                    chkbx_basicsave.CheckState = CheckState.Checked
                Else
                    chkbx_basicsave.CheckState = CheckState.Unchecked
                End If
            Else
                chkbx_usefoldernames.CheckState = CheckState.Unchecked
                chkbx_createfolderjpg.Enabled = False
                chkbx_basicsave.Enabled = False
                Pref.createfolderjpg = False
                Pref.basicsavemode = False
            End If


            ComboBox5.SelectedIndex = Pref.TvdbActorScrape

            If Pref.postertype = "poster" Then
                poster.Checked = True
            Else
                banner.Checked = True
            End If

            If Pref.usefanart = True Then
                chkbxfanart.Checked = True
            Else
                chkbxfanart.Checked = False
            End If

            If Pref.usetransparency = True Then
                chkbxusealpha.Checked = True
            Else
                chkbxusealpha.Checked = False
            End If

            TrackBar1.Value = Pref.transparencyvalue
            Label13.Text = TrackBar1.Value.ToString


            If Pref.ignoreactorthumbs = True Then
                chkbx_notactorthumbs.CheckState = CheckState.Checked
            Else
                chkbx_notactorthumbs.CheckState = CheckState.Unchecked
            End If

            If Pref.nfoposterscraper = 0 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 1 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 2 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 3 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 4 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 5 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 6 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 7 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Unchecked
            ElseIf Pref.nfoposterscraper = 8 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 9 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 10 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 11 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Unchecked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 12 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 13 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Unchecked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 14 Then
                IMPA_chk.CheckState = CheckState.Unchecked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Checked
            ElseIf Pref.nfoposterscraper = 15 Then
                IMPA_chk.CheckState = CheckState.Checked
                tmdb_chk.CheckState = CheckState.Checked
                mpdb_chk.CheckState = CheckState.Checked
                imdb_chk.CheckState = CheckState.Checked
            End If

            If Pref.savefanart = True Then
                CheckBox3.CheckState = CheckState.Checked
            Else
                CheckBox3.CheckState = CheckState.Unchecked
            End If

            'Call loadregex()
            'call loadprofiles

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btn_setbackcolour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_setbackcolour.Click
        Try
            ColorDialog.Color = ColorTranslator.FromHtml(Pref.backgroundcolour)
            If ColorDialog.ShowDialog() = DialogResult.OK Then
                Pref.backgroundcolour = ColorTranslator.ToHtml(ColorDialog.Color)
                Me.BackColor = ColorDialog.Color
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_setforcolour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_setforcolour.Click
        Try
            ColorDialog.Color = ColorTranslator.FromHtml(Pref.forgroundcolour)
            If ColorDialog.ShowDialog() = DialogResult.OK Then
                Pref.forgroundcolour = ColorTranslator.ToHtml(ColorDialog.Color)
                ListBox2.BackColor = ColorTranslator.FromHtml(Pref.forgroundcolour)
                ListBox1.BackColor = ColorTranslator.FromHtml(Pref.forgroundcolour)
                ListBox8.BackColor = ColorTranslator.FromHtml(Pref.forgroundcolour)
                ComboBox5.BackColor = ColorTranslator.FromHtml(Pref.forgroundcolour)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chk_rememberformsize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_rememberformsize.CheckedChanged
        Try
            If chk_rememberformsize.CheckState = CheckState.Checked Then
                Pref.remembersize = True
            Else
                Pref.remembersize = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub chkbxfanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxfanart.CheckedChanged
        Try
            If chkbxfanart.CheckState = CheckState.Checked Then
                Pref.usefanart = True
            Else
                Pref.usefanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Chkbx_fanartnoposter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chkbx_fanartnoposter.CheckedChanged
        Try
            If Chkbx_fanartnoposter.CheckState = CheckState.Checked Then
                Pref.dontdisplayposter = True
            Else
                Pref.dontdisplayposter = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub chkbxusealpha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxusealpha.CheckedChanged
        Try
            If chkbxusealpha.CheckState = CheckState.Checked Then
                Pref.usetransparency = True
            Else
                Pref.usetransparency = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged
        Try
            Label13.Text = TrackBar1.Value.ToString
            Label13.Refresh()
            Pref.transparencyvalue = TrackBar1.Value.ToString
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_usefoldernames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_usefoldernames.CheckedChanged
        Try
            If chkbx_usefoldernames.CheckState = CheckState.Checked Then
                Pref.usefoldernames = True
                chkbx_createfolderjpg.Enabled = True
                chkbx_basicsave.Enabled = True
            Else
                Pref.usefoldernames = False
                chkbx_createfolderjpg.CheckState = CheckState.Unchecked
                chkbx_basicsave.CheckState = CheckState.Unchecked
                chkbx_createfolderjpg.Enabled = False
                chkbx_basicsave.Enabled = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub



    Private Sub chkbx_createfolderjpg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkbx_createfolderjpg.CheckedChanged
        Try
            If chkbx_createfolderjpg.CheckState = CheckState.Checked Then
                Pref.createfolderjpg = True
            Else
                Pref.createfolderjpg = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub chkbx_basicsave_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkbx_basicsave.CheckedChanged
        Try
            If chkbx_basicsave.CheckState = CheckState.Checked Then
                Pref.basicsavemode = True
            Else
                Pref.basicsavemode = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbx_minrarsize.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If txtbx_minrarsize.Text <> "" Then
                    e.Handled = True
                Else
                    MsgBox("Please Enter at least 0")
                    txtbx_minrarsize.Text = "8"
                    Pref.rarsize = 8
                End If
            End If
            If txtbx_minrarsize.Text = "" Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Pref.rarsize = 8
                Exit Sub
            End If
            If Not IsNumeric(txtbx_minrarsize.Text) Then
                MsgBox("Please enter a numerical Value that is 1 or more")
                txtbx_minrarsize.Text = "8"
                Pref.rarsize = 8
                Exit Sub
            End If

            Pref.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub txtbox_maxposters_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbox_maxposters.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If txtbox_maxposters.Text <> "" Then
                    If Convert.ToDecimal(txtbox_maxposters.Text) >= 1 Then
                        e.Handled = True
                        Pref.maximagecount = Convert.ToInt32(txtbox_maxposters.Text)
                    Else
                        MsgBox("Please Enter at least 1")
                        Pref.maximagecount = 5
                    End If
                Else
                    MsgBox("Please Enter at least 1")
                    txtbox_maxposters.Text = "5"
                    Pref.maximagecount = 5
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_notactorthumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_notactorthumbs.CheckedChanged
        Try
            If chkbx_notactorthumbs.CheckState = CheckState.Checked Then
                Pref.ignoreactorthumbs = True
            Else
                Pref.ignoreactorthumbs = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_ignoretrailers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_ignoretrailers.CheckedChanged
        Try
            If chkbx_ignoretrailers.CheckState = CheckState.Checked Then
                Pref.ignoretrailers = True
            Else
                Pref.ignoretrailers = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_renamnfofiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_renamnfofiles.CheckedChanged
        Try
            If chkbx_renamnfofiles.CheckState = CheckState.Checked Then
                Pref.renamenfofiles = True
            Else
                Pref.renamenfofiles = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub rb_MediaPlayerDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_MediaPlayerDefault.CheckedChanged
        Try
            If rb_MediaPlayerDefault.Checked = True Then
                Pref.videomode = 1
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub rb_MediaPlayerWMP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_MediaPlayerWMP.CheckedChanged
        Try
            If rb_MediaPlayerWMP.Checked = True Then
                Pref.videomode = 2
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub rb_MediaPlayerUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_MediaPlayerUser.CheckedChanged
        Try
            If rb_MediaPlayerUser.Checked = True Then
                Pref.videomode = 4
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_MediaPlayerBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_MediaPlayerBrowse.Click
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
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_disablecache_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_disablecache.CheckedChanged
        Try
            If chkbx_disablecache.CheckState = CheckState.Checked Then
                Pref.startupCache = False
            Else
                Pref.startupCache = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_unstackposternames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_unstackposternames.CheckedChanged
        Try
            If chkbx_unstackposternames.CheckState = CheckState.Checked Then
                Pref.posternotstacked = True
            Else
                Pref.posternotstacked = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub chkbx_unstackfanartnames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_unstackfanartnames.CheckedChanged
        Try
            If chkbx_unstackfanartnames.CheckState = CheckState.Checked Then
                Pref.fanartnotstacked = True
            Else
                Pref.fanartnotstacked = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_addmoviefolderdialogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addmoviefolderdialogue.Click
        Try
            Dim allok As Boolean = True
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Pref.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                Pref.lastpath = thefoldernames
                For Each item As Object In ListBox2.Items
                    If thefoldernames.ToString = item.ToString Then allok = False
                Next

                If allok = True Then
                    Dim t As New str_RootPaths
                    t.rpath = thefoldernames
                    Pref.movieFolders.Add(t)
                    ListBox2.Items.Add(thefoldernames)
                    ListBox2.Refresh()
                Else
                    MsgBox("        Folder Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_removemoviefolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_removemoviefolder.Click
        Try
            Dim folderstoremove As New ArrayList
            For i = 0 To ListBox2.SelectedItems.Count - 1
                Pref.movieFolders.Remove(ListBox2.SelectedItems(i))
            Next
            ListBox2.Items.Clear()
            For Each folder In Pref.movieFolders
                ListBox2.Items.Add(folder)
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        Try
            If CheckBox10.CheckState = CheckState.Checked Then
                'Pref.keepfoldername = True
            Else
                'Pref.keepfoldername = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Try
            If RadioButton3.Checked = True Then
                Pref.moviescraper = 1
            Else
                Pref.moviescraper = 2
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub setnfothumbnailurls()
        If IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 0
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 1
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 2
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 3
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 4
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 5
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 6
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Pref.nfoposterscraper = 7
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 8
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 9
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 10
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 11
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 12
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 13
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 14
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Pref.nfoposterscraper = 15
        End If
    End Sub
    Private Sub IMPA_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPA_chk.CheckedChanged
        Try
            Call setnfothumbnailurls()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub mpdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mpdb_chk.CheckedChanged
        Try
            Call setnfothumbnailurls()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub tmdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmdb_chk.CheckedChanged
        Try
            Call setnfothumbnailurls()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub imdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imdb_chk.CheckedChanged
        Try
            Call setnfothumbnailurls()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Try
            'use imdbid for tmdb
            If CheckBox2.CheckState = CheckState.Checked Then
                Pref.alwaysuseimdbid = True
            Else
                Pref.alwaysuseimdbid = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try

            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.ListBox5.SelectedIndex <> 0 Then
                mSelectedIndex = Me.ListBox5.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                ListBox5.Items.Insert(mSelectedIndex + 1, ListBox5.Items(mOtherIndex))
                ListBox5.Items.RemoveAt(mOtherIndex)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.ListBox5.SelectedIndex <> Me.ListBox5.Items.Count - 1 Then
                mSelectedIndex = Me.ListBox5.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                ListBox5.Items.Insert(mSelectedIndex, ListBox5.Items(mOtherIndex))
                ListBox5.Items.RemoveAt(mOtherIndex + 1)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.ListBox3.SelectedIndex <> 0 Then
                mSelectedIndex = Me.ListBox3.SelectedIndex
                mOtherIndex = mSelectedIndex - 1
                ListBox3.Items.Insert(mSelectedIndex + 1, ListBox3.Items(mOtherIndex))
                ListBox3.Items.RemoveAt(mOtherIndex)
            End If
        Catch
        End Try
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            Dim mSelectedIndex, mOtherIndex As Integer
            If Me.ListBox3.SelectedIndex <> Me.ListBox3.Items.Count - 1 Then
                mSelectedIndex = Me.ListBox3.SelectedIndex
                mOtherIndex = mSelectedIndex + 1
                ListBox3.Items.Insert(mSelectedIndex, ListBox3.Items(mOtherIndex))
                ListBox3.Items.RemoveAt(mOtherIndex + 1)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        Try
            Pref.imdbmirror = ListBox4.SelectedItem
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If ComboBox1.SelectedItem = "None" Then
                Pref.maxactors = 0
            ElseIf ComboBox1.SelectedItem = "All Available" Then
                Pref.maxactors = 9999
            Else
                Pref.maxactors = Convert.ToInt32(ComboBox1.SelectedItem)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        Try
            If CheckBox22.CheckState = CheckState.Checked Then
                Pref.enablehdtags = True
            Else
                Pref.enablehdtags = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        Try
            If CheckBox4.CheckState = CheckState.Checked Then
                Pref.gettrailer = True
            Else
                Pref.gettrailer = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub saveactorchkbx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveactorchkbx.CheckedChanged
        Try
            If saveactorchkbx.CheckState = CheckState.Checked Then
                Pref.actorsave = True
            Else
                Pref.actorsave = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub localactorpath_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles localactorpath.Leave
        Try
            Pref.actorsavepath = localactorpath.Text
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub xbmcactorpath_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles xbmcactorpath.Leave
        Try
            Pref.actornetworkpath = xbmcactorpath.Text
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select Folder to Save Actor Thumbnails)"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Pref.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                localactorpath.Text = thefoldernames
                Pref.lastpath = thefoldernames
                Pref.actorsavepath = thefoldernames
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Try
            If CheckBox3.CheckState = CheckState.Checked Then
                Pref.savefanart = True
            Else
                Pref.savefanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub





    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        Try
            If CheckBox18.CheckState = CheckState.Checked Then
                Pref.scrapemovieposters = True
            Else
                Pref.scrapemovieposters = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        Try
            If CheckBox18.CheckState = CheckState.Checked Then
                Pref.disabletvlogs = True
            Else
                Pref.disabletvlogs = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub txtbx_minrarsize_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbx_minrarsize.TextChanged
        Try
            If IsNumeric(txtbx_minrarsize.Text) Then
                Pref.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
            Else
                Pref.rarsize = 8
                txtbx_minrarsize.Text = "8"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If CheckBox1.CheckState = CheckState.Checked Then
                Pref.overwritethumbs = False
            Else
                Pref.overwritethumbs = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        Try
            If CheckBox5.CheckState = CheckState.Checked Then
                Pref.externalbrowser = True
            Else
                Pref.externalbrowser = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        Try
            If CheckBox6.CheckState = CheckState.Checked Then
                Pref.roundminutes = True
            Else
                Pref.roundminutes = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim allok As Boolean = True
            Dim cancelregex As Boolean = False
            Dim newtvshow As Boolean = False
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            Dim tempstring3 As String
            Dim tempint As Integer
            Dim tempint2 As Integer
            theFolderBrowser.Description = "Please Select Root Folder of the TV Shows You Wish To Add to DB"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Pref.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                Pref.lastpath = thefoldernames
                For Each strfolder As String In My.Computer.FileSystem.GetDirectories(thefoldernames)
                    Try
                        If strfolder.IndexOf("System Volume Information") = -1 Then

                            allok = True
                            For Each item As Object In ListBox1.Items
                                If strfolder = item.ToString Then allok = False
                            Next
                            If allok = True Then
                                If cancelregex = False Then
                                    Dim M As Match
                                    tempstring3 = strfolder.ToLower
                                    M = Regex.Match(tempstring3, "(series ?\d+|season ?\d+|s ?\d+|^\d{1,3}$)")
                                    If M.Success = True Then
                                        tempint = MessageBox.Show(strfolder & " Appears to Contain Season Folders" & vbCrLf & "Are you sure this folder contains multiple" & vbCrLf & "TV Shows, Each in it's own folder?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                        If tempint = DialogResult.Yes Then
                                            ListBox1.Items.Add(strfolder)
                                            Pref.tvFolders.Add(strfolder)
                                            cancelregex = True
                                        End If
                                        If tempint = DialogResult.No Then
                                            tempint2 = MessageBox.Show("Do you wish to add this as a single TV Show Folder?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                            If tempint2 = DialogResult.Yes Then
                                                ListBox1.Items.Add(thefoldernames)
                                                Pref.tvFolders.Add(strfolder)
                                                Exit Sub
                                            End If
                                            If tempint2 = DialogResult.No Then
                                                Exit Sub
                                            End If
                                        End If
                                    Else
                                        ListBox1.Items.Add(strfolder)
                                        Pref.tvFolders.Add(strfolder)
                                    End If
                                Else
                                    ListBox1.Items.Add(strfolder)
                                    Pref.tvFolders.Add(strfolder)
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        MsgBox("error")
                    End Try
                Next
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton7.CheckedChanged
        Try
            If RadioButton7.Checked = True Then
                Pref.sortorder = "default"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton8.CheckedChanged
        Try
            If RadioButton8.Checked = True Then
                Pref.sortorder = "dvd"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Dim languagelist As New List(Of Tvdb.Language)
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If ListBox8.Items.Count = 0 Then
                Try
                    Form1.util_LanguageListLoad()
                    For Each lan In Form1.ListBox1.Items
                        ListBox8.Items.Add(lan.language)
                    Next
                Catch
                End Try
                Try
                    ListBox8.SelectedItem = Pref.TvdbLanguage
                Catch
                End Try
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_removetvfolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_removetvfolder.Click
        Try
            For i = 0 To ListBox1.SelectedItems.Count - 1
                Dim tempboolean As Boolean = False
                If ListBox1.SelectedItems(i) <> Nothing And ListBox1.SelectedItems(i) <> "" Then
                    For Each folder In Pref.tvFolders
                        If folder = ListBox1.SelectedItems(i) Then
                            Pref.tvFolders.Remove(folder)
                            Exit For
                        End If
                    Next
                End If
            Next

            ListBox1.Items.Clear()
            For Each folder In Pref.tvFolders
                ListBox1.Items.Add(folder)
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub btn_addtvfolderdialogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addtvfolderdialogue.Click
        Try
            Dim allok As Boolean = True
            Dim theFolderBrowser As New FolderBrowserDialog
            Dim thefoldernames As String
            theFolderBrowser.Description = "Please Select TV Folder to Add to DB"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Pref.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                thefoldernames = (theFolderBrowser.SelectedPath)
                For Each item As Object In ListBox1.Items
                    If thefoldernames.ToString = item.ToString Then allok = False
                Next
                Pref.lastpath = thefoldernames
                If allok = True Then
                    ListBox1.Items.Add(thefoldernames)
                    Pref.tvFolders.Add(thefoldernames)
                Else
                    MsgBox("        Folder Already Exists")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Try
            Pref.TvdbActorScrape = ComboBox5.SelectedIndex.ToString
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox7.SelectedIndexChanged
        Try
            If ListBox7.SelectedItem <> Nothing Then
                TextBox6.Text = ListBox7.SelectedItem
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            If TextBox6.Text = "" Then
                MsgBox("No Text")
                TextBox6.Text = ListBox7.SelectedItem
                Exit Sub
            End If
            If Not validateregex(TextBox6.Text) Then
                MsgBox("Invalid Regex")
                Exit Sub
            End If
            Dim tempint As Integer = ListBox7.SelectedIndex
            ListBox7.Items.Remove(ListBox7.SelectedItem)
            ListBox7.Items.Insert(tempint, TextBox6.Text)
            ListBox7.SelectedIndex = tempint
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try

            If ListBox7.SelectedItem = Nothing Then
                MsgBox("Please Select a Regex to test")
                Exit Sub
            End If
            If TextBox4.Text = "" Then
                MsgBox("Please Enter a filename or any string to test")
                Exit Sub
            End If
            TextBox5.Text = ""
            Dim tvseries As String
            Dim tvepisode As String
            Dim s As String
            Dim tempstring As String = TextBox4.Text
            s = tempstring '.ToLower
            Dim M As Match


            M = Regex.Match(s, ListBox7.SelectedItem)
            If M.Success = True Then
                Try
                    tvseries = M.Groups(1).Value
                    tvepisode = M.Groups(2).Value
                Catch
                    tvseries = "-1"
                    tvepisode = "-1"
                End Try
                Try
                    If tvseries <> "-1" Then
                        TextBox5.Text = "Series No = " & tvseries & vbCrLf
                    End If
                    If tvepisode <> "-1" Then
                        TextBox5.Text = TextBox5.Text & "Episode No = " & tvepisode
                    End If
                Catch
                End Try
            Else
                TextBox5.Text = "No Matches"
            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If Not validateregex(TextBox3.Text) Then
                MsgBox("Invalid Regex")
                Exit Sub
            End If
            For Each item In ListBox7.Items
                If item.ToString = TextBox3.Text Then
                    MsgBox("Regex already exists")
                    Exit Sub
                End If
            Next
            ListBox7.Items.Add(TextBox3.Text)
            Form1.tv_RegexScraper.Add(TextBox3.Text)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim tempstring = ListBox7.SelectedItem
            Try
                ListBox7.Items.Remove(ListBox7.SelectedItem)
            Catch
            End Try
            For Each regexp In Form1.tv_RegexScraper
                If regexp = tempstring Then
                    Form1.tv_RegexScraper.Remove(regexp)
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Try
            Form1.tv_RegexScraper.Clear()
            Form1.tv_RegexScraper.Add("[Ss]([\d]{1,2}).?[Ee]([\d]{1,2})")
            Form1.tv_RegexScraper.Add("([\d]{1,2}) ?[xX] ?([\d]{1,2})")
            Form1.tv_RegexScraper.Add("([0-9]+)([0-9][0-9])")
            ListBox7.Items.Clear()
            For Each Regex In Form1.tv_RegexScraper
                ListBox7.Items.Add(Regex)
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Function validateregex(ByVal regexs As String)
        Try
            Dim test As Match
            test = Regex.Match("", regexs)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Try
            Pref.tvrename = ComboBox3.SelectedIndex
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub poster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles poster.CheckedChanged
        Try
            If poster.Checked = True Then
                Pref.postertype = "poster"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub banner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles banner.CheckedChanged
        Try
            If banner.Checked = True Then
                Pref.postertype = "banner"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ListBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox8.SelectedIndexChanged
        Try
            For Each lan In languagelist
                If lan.Language.Value = ListBox8.Text Then
                    Pref.TvdbLanguage = lan.Language.Value
                    Pref.TvdbLanguageCode = lan.Abbreviation.Value
                    Exit For
                End If
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        Try
            If RadioButton6.Checked = True Then
                Pref.seasonall = "none"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton9.CheckedChanged
        Try
            If RadioButton9.Checked = True Then
                Pref.seasonall = "poster"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub RadioButton10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton10.CheckedChanged
        Try
            If RadioButton10.Checked = True Then
                Pref.seasonall = "wide"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        Try
            If CheckBox11.CheckState = CheckState.Checked Then
                Pref.tvshowrefreshlog = True
            Else
                Pref.tvshowrefreshlog = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

End Class