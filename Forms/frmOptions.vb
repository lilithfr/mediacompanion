Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml


Public Class frmOptions

    'Dim form1.userprefs As New _form1.userprefs.form1.userprefs
    Dim moviefolders As New List(Of String)
    Dim tvfolders As New List(Of String)



    Private Sub options_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        For f = 0 To 33
            Form1.userPrefs.certificatepriority(f) = ListBox5.Items(f)
        Next
        For f = 0 To 3
            Form1.userPrefs.moviethumbpriority(f) = ListBox3.Items(f)
        Next

        If Form1.userPrefs.videomode = 4 Then
            If Not IO.File.Exists(Form1.userPrefs.selectedvideoplayer) Then
                MsgBox("You Have Not Selected Your Preferred Media Player")
                e.Cancel = True
                Exit Sub
            End If
        End If

        Dim save As New Preferences
        Call save.saveconfig()
    End Sub





    Private Sub options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Form1.userPrefs.seasonall
            Case "none"
                RadioButton6.Checked = True
            Case "poster"
                RadioButton9.Checked = True
            Case "wide"
                RadioButton10.Checked = True
        End Select

        If Form1.userPrefs.tvshowrebuildlog = True Then
            CheckBox11.CheckState = CheckState.Checked
        Else
            CheckBox11.CheckState = CheckState.Unchecked
        End If

        For Each Regex In Form1.tvRegex
            ListBox7.Items.Add(Regex)
        Next
        ComboBox3.SelectedIndex = Form1.userPrefs.tvrename

        If Form1.userPrefs.roundminutes = True Then
            CheckBox6.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.roundminutes = False Then
            CheckBox6.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.externalbrowser = True Then
            CheckBox5.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.externalbrowser = False Then
            CheckBox5.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.videomode = 1 Then
            RadioButton1.Checked = True
        ElseIf Form1.userPrefs.videomode = 2 Then
            RadioButton2.Checked = True
        ElseIf Form1.userPrefs.videomode = 4 Then
            RadioButton4.Checked = True
        End If
        Label4.Text = Form1.userPrefs.selectedvideoplayer
        If Form1.userPrefs.overwritethumbs = True Then
            CheckBox1.Checked = CheckState.Unchecked
        Else
            CheckBox1.Checked = CheckState.Checked
        End If

        If Form1.userPrefs.keepfoldername = True Then
            CheckBox10.CheckState = CheckState.Checked
        Else
            CheckBox10.CheckState = CheckState.Unchecked
        End If


        If Form1.userPrefs.disabletvlogs = True Then
            CheckBox18.CheckState = CheckState.Checked
        Else
            CheckBox18.CheckState = CheckState.Unchecked
        End If

        If IsNumeric(Form1.userPrefs.rarsize) Then
            Dim tempint As Integer = Form1.userPrefs.rarsize
            If tempint <= 0 Then
                tempint = 8
                Form1.userPrefs.rarsize = 8
            End If
            txtbx_minrarsize.Text = tempint.ToString
        End If


        If Form1.userPrefs.resizefanart = 1 Then
            RadioButton17.Checked = True
        ElseIf Form1.userPrefs.resizefanart = 2 Then
            RadioButton18.Checked = True
        ElseIf Form1.userPrefs.resizefanart = 3 Then
            RadioButton19.Checked = True
        End If

        moviefolders = Form1.movieFolders
        tvfolders = Form1.tvFolders



        ListBox2.Items.Clear()
        For Each item In Form1.movieFolders
            ListBox2.Items.Add(item)
        Next

        For Each item In tvfolders
            ListBox1.Items.Add(item)
        Next

        If Form1.userPrefs.gettrailer = True Then
            CheckBox4.CheckState = CheckState.Checked
        Else
            CheckBox4.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.startupCache = True Then
            chkbx_disablecache.CheckState = CheckState.Unchecked
        Else
            chkbx_disablecache.CheckState = CheckState.Checked
        End If

        If Form1.userPrefs.enablehdtags = True Then
            CheckBox22.CheckState = CheckState.Checked
        Else
            CheckBox22.CheckState = CheckState.Unchecked
        End If

        If IsNumeric(Form1.userPrefs.rarsize) Then
            txtbx_minrarsize.Text = Form1.userPrefs.rarsize.ToString
        Else
            txtbx_minrarsize.Text = "8"
        End If

        If Form1.userPrefs.actorsave = True Then
            localactorpath.Enabled = True
            xbmcactorpath.Enabled = True
            xbmcactorpath.Text = Form1.userPrefs.actornetworkpath
            localactorpath.Text = Form1.userPrefs.actorsavepath
            Button1.Enabled = True
            saveactorchkbx.CheckState = CheckState.Checked
        Else
            'localactorpath.Enabled = False
            'xbmcactorpath.Enabled = False
            'Button1.Enabled = False
            saveactorchkbx.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.keepfoldername = False Then
            CheckBox10.CheckState = CheckState.Unchecked
        Else
            CheckBox10.CheckState = CheckState.Checked
        End If

        If Form1.userPrefs.ignoretrailers = False Then
            chkbx_ignoretrailers.CheckState = CheckState.Unchecked
        Else
            chkbx_ignoretrailers.CheckState = CheckState.Checked
        End If

        For f = 0 To 3
            ListBox3.Items.Add(Form1.userPrefs.moviethumbpriority(f))
        Next
        For f = 0 To 33
            ListBox5.Items.Add(Form1.userPrefs.certificatepriority(f))
        Next

        If Form1.userPrefs.tvfanart = True Then
            CheckBox9.CheckState = CheckState.Checked
        Else
            CheckBox9.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.tvposter = True Then
            CheckBox8.CheckState = CheckState.Checked
        Else
            CheckBox8.CheckState = CheckState.Unchecked
        End If





        If Form1.userPrefs.downloadtvseasonthumbs = True Then
            CheckBox7.CheckState = CheckState.Checked
        Else
            CheckBox7.CheckState = CheckState.Unchecked
        End If






        If Form1.userPrefs.posternotstacked = True Then
            chkbx_unstackposternames.CheckState = CheckState.Checked
        Else
            chkbx_unstackposternames.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.fanartnotstacked = True Then
            chkbx_unstackfanartnames.CheckState = CheckState.Checked
        Else
            chkbx_unstackfanartnames.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.scrapemovieposters = False Then
            CheckBox18.CheckState = CheckState.Unchecked
        Else
            CheckBox18.CheckState = CheckState.Checked
        End If

        If Form1.userPrefs.dontdisplayposter = False Then
            Chkbx_fanartnoposter.CheckState = CheckState.Unchecked
        Else
            Chkbx_fanartnoposter.CheckState = CheckState.Checked
        End If


        Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        If intX <= 861 Or intY <= 580 Then
            Me.AutoScroll = True
        End If

        If Form1.userPrefs.renamenfofiles = False Then
            chkbx_renamnfofiles.Checked = False
        Else
            chkbx_renamnfofiles.Checked = True
        End If

        If Form1.userPrefs.disabletvlogs = True Then
            CheckBox15.CheckState = CheckState.Checked
        Else
            CheckBox15.CheckState = CheckState.Unchecked
        End If

        TabPage1.BackColor = Form1.BackColor
        TabPage2.BackColor = Form1.BackColor
        TabPage3.BackColor = Form1.BackColor
        ListBox4.SelectedItem = Form1.userPrefs.imdbmirror


        Select Case Form1.userPrefs.maxactors
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

        Select Case Form1.userPrefs.maxmoviegenre
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







        If Form1.userPrefs.resizefanart = 1 Then
            RadioButton17.Checked = True
        ElseIf Form1.userPrefs.resizefanart = 2 Then
            RadioButton18.Checked = True
        ElseIf Form1.userPrefs.resizefanart = 3 Then
            RadioButton19.Checked = True
        Else
            Form1.userPrefs.resizefanart = 1
            RadioButton17.Checked = True
        End If



        If Form1.userPrefs.usefanart = True Then chkbxfanart.CheckState = CheckState.Checked
        If Form1.userPrefs.usefanart = False Then chkbxfanart.CheckState = CheckState.Unchecked
        If Form1.userPrefs.remembersize = True Then chk_rememberformsize.CheckState = CheckState.Checked
        If Form1.userPrefs.remembersize = False Then chk_rememberformsize.CheckState = CheckState.Unchecked


        Me.BackColor = Form1.BackColor


        If Form1.userPrefs.usefoldernames = True Then
            chkbx_usefoldernames.CheckState = CheckState.Checked
            chkbx_createfolderjpg.Enabled = True
            chkbx_basicsave.Enabled = True
            If Form1.userPrefs.createfolderjpg = True Then
                chkbx_createfolderjpg.CheckState = CheckState.Checked
            Else
                chkbx_createfolderjpg.CheckState = CheckState.Unchecked
            End If
            If Form1.userPrefs.basicsavemode = True Then
                chkbx_basicsave.CheckState = CheckState.Checked
            Else
                chkbx_basicsave.CheckState = CheckState.Unchecked
            End If
        Else
            chkbx_usefoldernames.CheckState = CheckState.Unchecked
            chkbx_createfolderjpg.Enabled = False
            chkbx_basicsave.Enabled = False
            Form1.userPrefs.createfolderjpg = False
            Form1.userPrefs.basicsavemode = False
        End If


        ComboBox5.SelectedIndex = Form1.userPrefs.tvdbactorscrape

        If Form1.userPrefs.postertype = "poster" Then
            poster.Checked = True
        Else
            banner.Checked = True
        End If

        If Form1.userPrefs.usefanart = True Then
            chkbxfanart.Checked = True
        Else
            chkbxfanart.Checked = False
        End If

        If Form1.userPrefs.usetransparency = True Then
            chkbxusealpha.Checked = True
        Else
            chkbxusealpha.Checked = False
        End If

        TrackBar1.Value = Form1.userPrefs.transparencyvalue
        Label13.Text = TrackBar1.Value.ToString


        If Form1.userPrefs.ignoreactorthumbs = True Then
            chkbx_notactorthumbs.CheckState = CheckState.Checked
        Else
            chkbx_notactorthumbs.CheckState = CheckState.Unchecked
        End If

        If Form1.userPrefs.nfoposterscraper = 0 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 1 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 2 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 3 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 4 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 5 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 6 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 7 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Unchecked
        ElseIf Form1.userPrefs.nfoposterscraper = 8 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 9 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 10 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 11 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Unchecked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 12 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 13 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Unchecked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 14 Then
            IMPA_chk.CheckState = CheckState.Unchecked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Checked
        ElseIf Form1.userPrefs.nfoposterscraper = 15 Then
            IMPA_chk.CheckState = CheckState.Checked
            tmdb_chk.CheckState = CheckState.Checked
            mpdb_chk.CheckState = CheckState.Checked
            imdb_chk.CheckState = CheckState.Checked
        End If

        If Form1.userPrefs.savefanart = True Then
            CheckBox3.CheckState = CheckState.Checked
        Else
            CheckBox3.CheckState = CheckState.Unchecked
        End If

        'Call loadregex()
        'call loadprofiles

    End Sub

    Private Sub btn_setbackcolour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_setbackcolour.Click
        ColorDialog.Color = ColorTranslator.FromHtml(Form1.userPrefs.backgroundcolour)
        If ColorDialog.ShowDialog() = DialogResult.OK Then
            Form1.userPrefs.backgroundcolour = ColorTranslator.ToHtml(ColorDialog.Color)
            Me.BackColor = ColorDialog.Color
        End If
    End Sub

    Private Sub btn_setforcolour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_setforcolour.Click
        ColorDialog.Color = ColorTranslator.FromHtml(Form1.userPrefs.forgroundcolour)
        If ColorDialog.ShowDialog() = DialogResult.OK Then
            Form1.userPrefs.forgroundcolour = ColorTranslator.ToHtml(ColorDialog.Color)
            ListBox2.BackColor = ColorTranslator.FromHtml(Form1.userPrefs.forgroundcolour)
            ListBox1.BackColor = ColorTranslator.FromHtml(Form1.userPrefs.forgroundcolour)
            ListBox8.BackColor = ColorTranslator.FromHtml(Form1.userPrefs.forgroundcolour)
            ComboBox5.BackColor = ColorTranslator.FromHtml(Form1.userPrefs.forgroundcolour)
        End If
    End Sub

    Private Sub chk_rememberformsize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_rememberformsize.CheckedChanged
        If chk_rememberformsize.CheckState = CheckState.Checked Then
            Form1.userPrefs.remembersize = True
        Else
            Form1.userPrefs.remembersize = False
        End If
    End Sub



    Private Sub chkbxfanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxfanart.CheckedChanged
        If chkbxfanart.CheckState = CheckState.Checked Then
            Form1.userPrefs.usefanart = True
        Else
            Form1.userPrefs.usefanart = False
        End If
    End Sub


    Private Sub Chkbx_fanartnoposter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chkbx_fanartnoposter.CheckedChanged
        If Chkbx_fanartnoposter.CheckState = CheckState.Checked Then
            Form1.userPrefs.dontdisplayposter = True
        Else
            Form1.userPrefs.dontdisplayposter = False
        End If
    End Sub


    Private Sub chkbxusealpha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxusealpha.CheckedChanged
        If chkbxusealpha.CheckState = CheckState.Checked Then
            Form1.userPrefs.usetransparency = True
        Else
            Form1.userPrefs.usetransparency = False
        End If
    End Sub


    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged
        Label13.Text = TrackBar1.Value.ToString
        Label13.Refresh()
        Form1.userPrefs.transparencyvalue = TrackBar1.Value.ToString
    End Sub

    Private Sub chkbx_usefoldernames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_usefoldernames.CheckedChanged
        If chkbx_usefoldernames.CheckState = CheckState.Checked Then
            Form1.userPrefs.usefoldernames = True
            chkbx_createfolderjpg.Enabled = True
            chkbx_basicsave.Enabled = True
        Else
            Form1.userPrefs.usefoldernames = False
            chkbx_createfolderjpg.CheckState = CheckState.Unchecked
            chkbx_basicsave.CheckState = CheckState.Unchecked
            chkbx_createfolderjpg.Enabled = False
            chkbx_basicsave.Enabled = False
        End If
    End Sub



    Private Sub chkbx_createfolderjpg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkbx_createfolderjpg.CheckedChanged
        If chkbx_createfolderjpg.CheckState = CheckState.Checked Then
            Form1.userPrefs.createfolderjpg = True
        Else
            Form1.userPrefs.createfolderjpg = False
        End If
    End Sub


    Private Sub chkbx_basicsave_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkbx_basicsave.CheckedChanged
        If chkbx_basicsave.CheckState = CheckState.Checked Then
            Form1.userPrefs.basicsavemode = True
        Else
            Form1.userPrefs.basicsavemode = False
        End If
    End Sub

    Private Sub txtbx_minrarsize_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbx_minrarsize.KeyPress
        If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
            If txtbx_minrarsize.Text <> "" Then
                e.Handled = True
            Else
                MsgBox("Please Enter at least 0")
                txtbx_minrarsize.Text = "8"
                Form1.userPrefs.rarsize = 8
            End If
        End If
        If txtbx_minrarsize.Text = "" Then
            MsgBox("Please enter a numerical Value that is 1 or more")
            txtbx_minrarsize.Text = "8"
            Form1.userPrefs.rarsize = 8
            Exit Sub
        End If
        If Not IsNumeric(txtbx_minrarsize.Text) Then
            MsgBox("Please enter a numerical Value that is 1 or more")
            txtbx_minrarsize.Text = "8"
            Form1.userPrefs.rarsize = 8
            Exit Sub
        End If

        Form1.userPrefs.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)

    End Sub



    Private Sub txtbox_maxposters_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbox_maxposters.KeyPress
        If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
            If txtbox_maxposters.Text <> "" Then
                If Convert.ToDecimal(txtbox_maxposters.Text) >= 1 Then
                    e.Handled = True
                    Form1.userPrefs.maximagecount = Convert.ToInt32(txtbox_maxposters.Text)
                Else
                    MsgBox("Please Enter at least 1")
                    Form1.userPrefs.maximagecount = 5
                End If
            Else
                MsgBox("Please Enter at least 1")
                txtbox_maxposters.Text = "5"
                Form1.userPrefs.maximagecount = 5
            End If
        End If
    End Sub

    Private Sub chkbx_notactorthumbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_notactorthumbs.CheckedChanged
        If chkbx_notactorthumbs.CheckState = CheckState.Checked Then
            Form1.userPrefs.ignoreactorthumbs = True
        Else
            Form1.userPrefs.ignoreactorthumbs = False
        End If
    End Sub

    Private Sub chkbx_ignoretrailers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_ignoretrailers.CheckedChanged
        If chkbx_ignoretrailers.CheckState = CheckState.Checked Then
            Form1.userPrefs.ignoretrailers = True
        Else
            Form1.userPrefs.ignoretrailers = False
        End If
    End Sub

    Private Sub chkbx_renamnfofiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_renamnfofiles.CheckedChanged
        If chkbx_renamnfofiles.CheckState = CheckState.Checked Then
            Form1.userPrefs.renamenfofiles = True
        Else
            Form1.userPrefs.renamenfofiles = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            Form1.userPrefs.videomode = 1
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Form1.userPrefs.videomode = 2
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            Form1.userPrefs.videomode = 4
        End If
    End Sub

    Private Sub btn_custommediaplayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_custommediaplayer.Click
        Dim filebrowser As New OpenFileDialog
        Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        filebrowser.InitialDirectory = mstrProgramFilesPath
        filebrowser.Filter = "Executable Files|*.exe"
        filebrowser.Title = "Find Executable Of Preferred Media Player"
        If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            Form1.userPrefs.selectedvideoplayer = filebrowser.FileName
            Label4.Visible = True
            Label4.Text = Form1.userPrefs.selectedvideoplayer
        End If
    End Sub

    Private Sub chkbx_disablecache_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_disablecache.CheckedChanged
        If chkbx_disablecache.CheckState = CheckState.Checked Then
            Form1.userPrefs.startupCache = False
        Else
            Form1.userPrefs.startupCache = True
        End If
    End Sub

    Private Sub chkbx_unstackposternames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_unstackposternames.CheckedChanged
        If chkbx_unstackposternames.CheckState = CheckState.Checked Then
            Form1.userPrefs.posternotstacked = True
        Else
            Form1.userPrefs.posternotstacked = False
        End If
    End Sub

    Private Sub chkbx_unstackfanartnames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbx_unstackfanartnames.CheckedChanged
        If chkbx_unstackfanartnames.CheckState = CheckState.Checked Then
            Form1.userPrefs.fanartnotstacked = True
        Else
            Form1.userPrefs.fanartnotstacked = False
        End If
    End Sub

    Private Sub btn_addmoviefolderdialogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addmoviefolderdialogue.Click
        Dim allok As Boolean = True
        Dim theFolderBrowser As New FolderBrowserDialog
        Dim thefoldernames As String
        theFolderBrowser.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
        theFolderBrowser.ShowNewFolderButton = True
        theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
        theFolderBrowser.SelectedPath = Form1.userPrefs.lastpath
        If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            thefoldernames = (theFolderBrowser.SelectedPath)
            Form1.userPrefs.lastpath = thefoldernames
            For Each item As Object In ListBox2.Items
                If thefoldernames.ToString = item.ToString Then allok = False
            Next

            If allok = True Then
                Form1.movieFolders.Add(thefoldernames)
                ListBox2.Items.Add(thefoldernames)
                ListBox2.Refresh()
            Else
                MsgBox("        Folder Already Exists")
            End If
        End If
    End Sub

    Private Sub btn_removemoviefolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_removemoviefolder.Click
        Dim folderstoremove As New ArrayList
        For i = 0 To ListBox2.SelectedItems.Count - 1
            Form1.movieFolders.Remove(ListBox2.SelectedItems(i))
        Next
        ListBox2.Items.Clear()
        For Each folder In Form1.movieFolders
            ListBox2.Items.Add(folder)
        Next
    End Sub




    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.userPrefs.keepfoldername = True
        Else
            Form1.userPrefs.keepfoldername = False
        End If
    End Sub






    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            Form1.userPrefs.moviescraper = 1
        Else
            Form1.userPrefs.moviescraper = 2
        End If
    End Sub




    Private Sub setnfothumbnailurls()
        If IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 0
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 1
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 2
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 3
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 4
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 5
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 6
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Unchecked Then
            Form1.userPrefs.nfoposterscraper = 7
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 8
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 9
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 10
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Unchecked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 11
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 12
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Unchecked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 13
        ElseIf IMPA_chk.CheckState = CheckState.Unchecked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 14
        ElseIf IMPA_chk.CheckState = CheckState.Checked And tmdb_chk.CheckState = CheckState.Checked And mpdb_chk.CheckState = CheckState.Checked And imdb_chk.CheckState = CheckState.Checked Then
            Form1.userPrefs.nfoposterscraper = 15
        End If
    End Sub
    Private Sub IMPA_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPA_chk.CheckedChanged
        Call setnfothumbnailurls()
    End Sub
    Private Sub mpdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mpdb_chk.CheckedChanged
        Call setnfothumbnailurls()
    End Sub
    Private Sub tmdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmdb_chk.CheckedChanged
        Call setnfothumbnailurls()
    End Sub
    Private Sub imdb_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imdb_chk.CheckedChanged
        Call setnfothumbnailurls()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        'use imdbid for tmdb
        If CheckBox2.CheckState = CheckState.Checked Then
            Form1.userPrefs.alwaysuseimdbid = True
        Else
            Form1.userPrefs.alwaysuseimdbid = False
        End If

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
        Catch
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
        Catch
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
        Catch
        End Try
    End Sub


    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        Form1.userPrefs.imdbmirror = ListBox4.SelectedItem
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "None" Then
            Form1.userPrefs.maxactors = 0
        ElseIf ComboBox1.SelectedItem = "All Available" Then
            Form1.userPrefs.maxactors = 9999
        Else
            Form1.userPrefs.maxactors = Convert.ToInt32(ComboBox1.SelectedItem)
        End If
    End Sub

    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        If CheckBox22.CheckState = CheckState.Checked Then
            Form1.userPrefs.enablehdtags = True
        Else
            Form1.userPrefs.enablehdtags = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.userPrefs.gettrailer = True
        Else
            Form1.userPrefs.gettrailer = False
        End If
    End Sub

    Private Sub saveactorchkbx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveactorchkbx.CheckedChanged
        If saveactorchkbx.CheckState = CheckState.Checked Then
            Form1.userPrefs.actorsave = True
        Else
            Form1.userPrefs.actorsave = False
        End If
    End Sub

    Private Sub localactorpath_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles localactorpath.Leave
        Form1.userPrefs.actorsavepath = localactorpath.Text
    End Sub

    Private Sub xbmcactorpath_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles xbmcactorpath.Leave
        Form1.userPrefs.actornetworkpath = xbmcactorpath.Text
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim theFolderBrowser As New FolderBrowserDialog
        Dim thefoldernames As String
        theFolderBrowser.Description = "Please Select Folder to Save Actor Thumbnails)"
        theFolderBrowser.ShowNewFolderButton = True
        theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
        theFolderBrowser.SelectedPath = Form1.userPrefs.lastpath
        If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            thefoldernames = (theFolderBrowser.SelectedPath)
            localactorpath.Text = thefoldernames
            Form1.userPrefs.lastpath = thefoldernames
            Form1.userPrefs.actorsavepath = thefoldernames
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.userPrefs.savefanart = True
        Else
            Form1.userPrefs.savefanart = False
        End If
    End Sub

    Private Sub RadioButton17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton17.CheckedChanged
        'unchanged
        If RadioButton17.Checked = True Then
            Form1.userPrefs.resizefanart = 1
        End If
    End Sub

    Private Sub RadioButton18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton18.CheckedChanged
        '1280x720
        If RadioButton18.Checked = True Then
            Form1.userPrefs.resizefanart = 2
        End If
    End Sub

    Private Sub RadioButton19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton19.CheckedChanged
        '960x540
        If RadioButton19.Checked = True Then
            Form1.userPrefs.resizefanart = 3
        End If
    End Sub

    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.userPrefs.scrapemovieposters = True
        Else
            Form1.userPrefs.scrapemovieposters = False
        End If
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.userPrefs.disabletvlogs = True
        Else
            Form1.userPrefs.disabletvlogs = False
        End If
    End Sub

    Private Sub txtbx_minrarsize_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbx_minrarsize.TextChanged
        If IsNumeric(txtbx_minrarsize.Text) Then
            Form1.userPrefs.rarsize = Convert.ToInt32(txtbx_minrarsize.Text)
        Else
            Form1.userPrefs.rarsize = 8
            txtbx_minrarsize.Text = "8"
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.userPrefs.overwritethumbs = False
        Else
            Form1.userPrefs.overwritethumbs = True
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.userPrefs.externalbrowser = True
        Else
            Form1.userPrefs.externalbrowser = False
        End If


    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.userPrefs.roundminutes = True
        Else
            Form1.userPrefs.roundminutes = False
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
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
        theFolderBrowser.SelectedPath = Form1.userPrefs.lastpath
        If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            thefoldernames = (theFolderBrowser.SelectedPath)
            Form1.userPrefs.lastpath = thefoldernames
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
                                        Form1.tvFolders.Add(strfolder)
                                        cancelregex = True
                                    End If
                                    If tempint = DialogResult.No Then
                                        tempint2 = MessageBox.Show("Do you wish to add this as a single TV Show Folder?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                        If tempint2 = DialogResult.Yes Then
                                            ListBox1.Items.Add(thefoldernames)
                                            Form1.tvFolders.Add(strfolder)
                                            Exit Sub
                                        End If
                                        If tempint2 = DialogResult.No Then
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    ListBox1.Items.Add(strfolder)
                                    Form1.tvFolders.Add(strfolder)
                                End If
                            Else
                                ListBox1.Items.Add(strfolder)
                                Form1.tvFolders.Add(strfolder)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("error")
                End Try
            Next
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then
            Form1.userPrefs.sortorder = "default"
        End If
    End Sub

    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton8.CheckedChanged
        If RadioButton8.Checked = True Then
            Form1.userPrefs.sortorder = "dvd"
        End If
    End Sub
    Dim languagelist As New List(Of TvShowLanguages)
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ListBox8.Items.Count = 0 Then
            Try
                Dim xmlfile As String
                Dim wrGETURL As WebRequest
                Dim mirrorsurl As String = "http://thetvdb.com/api/6E82FED600783400/languages.xml"
                wrGETURL = WebRequest.Create(mirrorsurl)
                Dim myProxy As New WebProxy("myproxy", 80)
                myProxy.BypassProxyOnLocal = True
                Dim objStream As Stream
                objStream = wrGETURL.GetResponse.GetResponseStream()
                Dim objReader As New StreamReader(objStream)
                xmlfile = objReader.ReadToEnd
                Dim showlist As New XmlDocument
                'Try
                showlist.LoadXml(xmlfile)
                objReader.Close()
                objStream.Close()
                Dim thisresult As XmlNode = Nothing
                For Each thisresult In showlist("Languages")
                    Select Case thisresult.Name
                        Case "Language"
                            Dim results As XmlNode = Nothing
                            Dim lan As New TvShowLanguages
                            For Each results In thisresult.ChildNodes
                                Select Case results.Name
                                    Case "name"
                                        lan.language = results.InnerText
                                    Case "abbreviation"
                                        lan.abbreviation = results.InnerText
                                End Select
                            Next
                            languagelist.Add(lan)
                    End Select
                Next
                For Each lan In languagelist
                    ListBox8.Items.Add(lan.language)
                Next
            Catch
            End Try
            Try
                ListBox8.SelectedItem = Form1.userPrefs.tvdblanguage
            Catch
            End Try
        End If
    End Sub








    Private Sub btn_removetvfolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_removetvfolder.Click
        For i = 0 To ListBox1.SelectedItems.Count - 1
            Dim tempboolean As Boolean = False
            If ListBox1.SelectedItems(i) <> Nothing And ListBox1.SelectedItems(i) <> "" Then
                For Each folder In Form1.tvFolders
                    If folder = ListBox1.SelectedItems(i) Then
                        Form1.tvFolders.Remove(folder)
                        Exit For
                    End If
                Next
            End If
        Next

        ListBox1.Items.Clear()
        For Each folder In Form1.tvFolders
            ListBox1.Items.Add(folder)
        Next
    End Sub


    Private Sub btn_addtvfolderdialogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addtvfolderdialogue.Click
        Dim allok As Boolean = True
        Dim theFolderBrowser As New FolderBrowserDialog
        Dim thefoldernames As String
        theFolderBrowser.Description = "Please Select TV Folder to Add to DB"
        theFolderBrowser.ShowNewFolderButton = True
        theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
        theFolderBrowser.SelectedPath = Form1.userPrefs.lastpath
        If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            thefoldernames = (theFolderBrowser.SelectedPath)
            For Each item As Object In ListBox1.Items
                If thefoldernames.ToString = item.ToString Then allok = False
            Next
            Form1.userPrefs.lastpath = thefoldernames
            If allok = True Then
                ListBox1.Items.Add(thefoldernames)
                Form1.tvFolders.Add(thefoldernames)
            Else
                MsgBox("        Folder Already Exists")
            End If
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Form1.userPrefs.tvdbactorscrape = ComboBox5.SelectedIndex.ToString
    End Sub

    Private Sub ListBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox7.SelectedIndexChanged
        If ListBox7.SelectedItem <> Nothing Then
            TextBox6.Text = ListBox7.SelectedItem
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
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
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
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
        Form1.tvRegex.Add(TextBox3.Text)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim tempstring = ListBox7.SelectedItem
        Try
            ListBox7.Items.Remove(ListBox7.SelectedItem)
        Catch
        End Try
        For Each regexp In Form1.tvRegex
            If regexp = tempstring Then
                Form1.tvRegex.Remove(regexp)
                Exit For
            End If
        Next
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Form1.tvRegex.Clear()
        Form1.tvRegex.Add("[Ss]([\d]{1,2}).?[Ee]([\d]{1,2})")
        Form1.tvRegex.Add("([\d]{1,2}) ?[xX] ?([\d]{1,2})")
        Form1.tvRegex.Add("([0-9]+)([0-9][0-9])")
        ListBox7.Items.Clear()
        For Each Regex In Form1.tvRegex
            ListBox7.Items.Add(Regex)
        Next
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
        Form1.userPrefs.tvrename = ComboBox3.SelectedIndex
    End Sub

    Private Sub poster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles poster.CheckedChanged
        If poster.Checked = True Then
            Form1.userPrefs.postertype = "poster"
        End If
    End Sub

    Private Sub banner_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles banner.CheckedChanged
        If banner.Checked = True Then
            Form1.userPrefs.postertype = "banner"
        End If
    End Sub

    Private Sub ListBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox8.SelectedIndexChanged
        For Each lan In languagelist
            If lan.language = ListBox8.Text Then
                Form1.userPrefs.tvdblanguage = lan.language
                Form1.userPrefs.tvdblanguagecode = lan.abbreviation
                Exit For
            End If
        Next
    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            Form1.userPrefs.seasonall = "none"
        End If
    End Sub

    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton9.CheckedChanged
        If RadioButton9.Checked = True Then
            Form1.userPrefs.seasonall = "poster"
        End If
    End Sub

    Private Sub RadioButton10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton10.CheckedChanged
        If RadioButton10.Checked = True Then
            Form1.userPrefs.seasonall = "wide"
        End If
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.userPrefs.tvshowrebuildlog = True
        Else
            Form1.userPrefs.tvshowrebuildlog = False
        End If
    End Sub
End Class