Imports System.Linq

Public Class ucFanartTv
    Dim WithEvents tvposterpicboxes As PictureBox
    Dim WithEvents tvpostercheckboxes As RadioButton
    Dim WithEvents tvposterlabels As Label
    Dim WithEvents tvreslabel As Label
    Dim nodata As Boolean = False
    Dim arttype As String = ""
    Dim FanarttvMovielist As New FanartTvMovieList
    Public messbox As New frmMessageBox("blank", "", "")
    Dim usedlist As New List(Of str_fanarttvart)
    Public workingMovDetails As New FullMovieDetails
    Dim MovfieldNames = GetType(FanarttvMovielist).GetFields().[Select](Function(field) field.Name).ToList()
    Public movFriendlyname() As String = {"HiDef ClearArt", "HiDef Logo", "Movie Logo", "Movie Art", "Background", "Movie Disc", 
                                          "Movie Banner", "Movie Thumb", "Movie Poster"}
    Public tvFriendlyname() As String = {"HiDef Tv Logo", "HiDef ClearArt", "Clear Logo", "Clear Art", "Tv Poster", "Tv Thumb", 
                                         "Tv Banner", "Show Background", "Season Poster", "Season Thumb",  "Character Art"}


    Public Sub ucFanartTv_Refresh(ByVal moviedetails As FullMovieDetails)
        If workingMovDetails.fullmoviebody.title <> moviedetails.fullmoviebody.title Then
            workingMovDetails = Form1.workingMovieDetails
        Else
            Exit Sub
        End If
        Dim ID As String = ""
        lblftvgroups.Items.clear
        PanelClear()
        Me.lblTitle.Text = workingMovDetails.fullmoviebody.title 
        If workingMovDetails.fullmoviebody.imdbid.Contains("tt") Then
            ID = workingMovDetails.fullmoviebody.imdbid
        ElseIf workingMovDetails.fullmoviebody.tmdbid <> "" Then
            ID = workingMovDetails.fullmoviebody.tmdbid
        Else
            nodata = True
            Call noID
        End If
        If nodata Then Exit Sub
        GetFanartTvArt(ID)
        If Not ConfirmIfResults() Then Exit Sub
        arttypeload()
    End Sub

    Public Sub GetFanartTvArt(ByVal ID As String)
        Try
            messbox = New frmMessageBox("Please wait,", "", "Gathering image data")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Dim newobject As New FanartTv
            newobject.ID = ID
            newobject.src = "movie"
            FanarttvMovielist = newobject.FanarttvMovieresults
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        Finally
            messbox.Close()
        End Try
    End Sub

    Public Function ConfirmIfResults() As Boolean
        Dim ok As Boolean = True
        If Not FanarttvMovielist.dataloaded Then
            MsgBox("Sorry, there are no results from Fanart.Tv" & vbCrLf & "for movie:  " & workingMovDetails.fullmoviebody.title)
            ok = False
        End If
        Return ok
    End Function

    Public Sub noID()
        MsgBox(" Selected Movie contains no" & vbCrLf & "     IMDB or TMDB ID" & vbCrLf & "Unable to get Fanart TV Data") 
        nodata = True
    End Sub
    
    Sub arttypeload()
        lblftvgroups.Items.Clear()
        lblftvgroups.Items.Add(movFriendlyname(0) & ":" & vbTab & "( " & FanarttvMovielist.hdmovieclearart.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(1) & ":" & vbTab & "( " & FanarttvMovielist.hdmovielogo.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(2) & ":" & vbTab & "( " & FanarttvMovielist.movielogo.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(3) & ":" & vbTab & vbTab & "( " & FanarttvMovielist.movieart.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(4) & ":" & vbTab & "( " & FanarttvMovielist.moviebackground.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(5) & ":" & vbTab & "( " & FanarttvMovielist.moviedisc.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(6) & ":" & vbTab & "( " & FanarttvMovielist.moviebanner.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(7) & ":" & vbTab & "( " & FanarttvMovielist.moviethumb.count & " )")
        lblftvgroups.Items.Add(movFriendlyname(8) & ":" & vbTab & "( " & FanarttvMovielist.movieposter.count & " )")
    End Sub

    Private Sub lblftvgroups_click(ByVal Sender As Object, e As EventArgs) Handles lblftvgroups.MouseDown 
        Dim indx As Integer = lblftvgroups.SelectedIndex
        Select Case indx
            Case "0"
                usedlist = FanarttvMovielist.hdmovieclearart
                arttype = "wide"
            Case "1"
                usedlist = FanarttvMovielist.hdmovielogo
                arttype = "wide"
            Case "2"
                usedlist = FanarttvMovielist.movielogo
                arttype = "wide"
            Case "3"
                usedlist = FanarttvMovielist.movieart
                arttype = "wide"
            Case "4"
                usedlist = FanarttvMovielist.moviebackground
                arttype = "wide"
            Case "5"
                usedlist = FanarttvMovielist.moviedisc
                arttype = "square"
            Case "6"
                usedlist = FanarttvMovielist.moviebanner 
                arttype = "wide"
            Case "7"
                usedlist = FanarttvMovielist.moviethumb
                arttype = "wide"
            Case "8"
                usedlist = FanarttvMovielist.movieposter
                arttype = "high"
        End Select
        PanelPopulate()
    End Sub

    Private Sub PanelPopulate()
        Me.Panel1.Show()
        Call PanelSelectionDisplay()
    End Sub

    Private Sub PanelClear()
        For i = Panel1.Controls.Count - 1 To 0 Step -1
            Panel1.Controls.RemoveAt(i)
        Next
    End Sub

    Private Sub PanelSelectionDisplay()
        ''Movie Image Preview sizes as follows:
        ''200  x  37     Banner
        ''200  x  77     HDLogo & movielogo
        ''200  x  112    Background, HDClearArt & Clearart, and moviethumb
        ''200  x  200    moviedisc
        ''200  x  285    movieposter

        PanelClear()
        If usedlist.Count = 0 Then
            lblnoart.Visible = True
            Exit Sub
        Else
            lblnoart.Visible = false
        End If
        Panel1.VerticalScroll.Visible = True 
        Dim location As Integer = 2
        Dim pbwidth As Integer = 300
        Dim pwheight As Integer = 204
        Dim colwidth As Integer = 20
        Dim columncount = 0
        Dim locHeight = 5
        Dim locOffset = 240  '197
        Dim itemcounter As Integer = 0
        Dim tempboolean As Boolean = True
        If arttype = "high" Or arttype = "square" Then
            For each item In usedlist
                tvposterpicboxes() = New PictureBox()
                With tvposterpicboxes
                    .Location = New Point(location, locHeight)
                    .Width = 150
                    .Height = 204
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item.urlpreview   'usedlist(f).SmallUrl
                    .Tag = item.url 
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "poster" & itemcounter.ToString
                    'AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
                    'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
                End With

                tvpostercheckboxes() = New RadioButton()
                With tvpostercheckboxes
                    .Location = New Point(location + 65, locHeight + 208) '166
                    .Name = "postercheckbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
                End With

                itemcounter += 1

                Me.Panel1.Controls.Add(tvposterpicboxes())
                Me.Panel1.Controls.Add(tvpostercheckboxes())
                Me.Refresh()
                Application.DoEvents()
                location += 156 '120
                columncount += 1
                If columncount = 4 Then
                    location = 2
                    columncount = 0
                    locHeight += locOffset 
                End If
            Next
        ElseIf arttype = "banner" Then
            For each item In usedlist
                tvposterpicboxes() = New PictureBox()
                With tvposterpicboxes
                    .Location = New Point(0, location)
                    .Width = 600
                    .Height = 114
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item.urlpreview 
                    .Tag = item.url
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "poster" & itemcounter.ToString
                    'AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
                    'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
                End With

                tvpostercheckboxes() = New RadioButton()
                With tvpostercheckboxes
                    .Location = New Point(290, location + 110)
                    .Name = "postercheckbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
                End With
                itemcounter += 1
                location += 140

                Me.Panel1.Controls.Add(tvposterpicboxes())
                Me.Panel1.Controls.Add(tvpostercheckboxes())
            Next
        ElseIf arttype = "wide" Then
            For each item In usedlist
                tvposterpicboxes() = New PictureBox()
                With tvposterpicboxes
                    .Location = New Point(0, location)
                    .Width = 600
                    .Height = 114
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item.urlpreview 
                    .Tag = item.url
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "poster" & itemcounter.ToString
                    'AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
                    'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
                End With

                tvpostercheckboxes() = New RadioButton()
                With tvpostercheckboxes
                    .Location = New Point(290, location + 110)
                    .Name = "postercheckbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
                End With
                itemcounter += 1
                location += 140

                Me.Panel1.Controls.Add(tvposterpicboxes())
                Me.Panel1.Controls.Add(tvpostercheckboxes())
            Next
        End If
        
        ''Me.Refresh()
        Application.DoEvents()
        Me.Refresh()
    End Sub

    Private Sub tv_PosterRadioChanged(ByVal sender As Object, ByVal e As EventArgs)

        'PictureBox13.Image = Nothing
        'Dim tempstring As String = sender.name
        'Dim tempint As Integer = 0
        'Dim tempstring2 As String = tempstring
        'Dim allok As Boolean = False
        'tempstring = tempstring.Replace("postercheckbox", "")
        'tempint = Convert.ToDecimal(tempstring)
        ''For Each button As Control In Me.Panel8.Controls
        ''    If button.Name.IndexOf("postercheckbox") <> -1 Then
        ''        Dim b1 As RadioButton = CType(button, RadioButton)
        ''        If b1.Checked = True Then
        ''            allok = True
        ''            Exit For
        ''        End If
        ''    End If
        ''Next

        'Dim hires(1)
        'Dim lores(1)
        'lores(0) = ""
        'hires(0) = ""
        'lores(1) = ""
        'hires(1) = ""
        'For Each cont As Control In Me.Panel16.Controls()
        '    If cont.Name.Replace("poster", "") = tempint.ToString Then
        '        Dim picbox As PictureBox = cont
        '        lores(0) = "Save Image (" & picbox.Image.Width & " x " & picbox.Image.Height & ")"
        '        lores(1) = picbox.Name
        '        If tvdbmode = True Then
        '            For Each poster In usedlist
        '                If poster.smallUrl = picbox.ImageLocation Then
        '                    If IsNumeric(poster.resolution.Replace("x", "")) Then
        '                        hires(0) = "Save Image (" & poster.resolution & ")"
        '                        hires(0) = hires(0).replace("x", " x ")
        '                    Else
        '                        hires(0) = "Save Image (Hi-Res)"
        '                    End If
        '                    hires(1) = poster.url
        '                    Exit For
        '                End If
        '            Next
        '            allok = True
        '            Exit For
        '        Else
        '            allok = True
        '        End If
        '    End If
        'Next

        'If allok = True Then
        '    'Button57.Visible = True
        '    'Button57.Tag = lores(1)
        '    'Button57.Text = lores(0)
        '    If tvdbmode = True Then
        '        btnTvPosterSaveBig.Text = hires(0)
        '        btnTvPosterSaveBig.Visible = True
        '        btnTvPosterSaveBig.Tag = hires(1)
        '    Else
        '        btnTvPosterSaveBig.Visible = False
        '    End If

        'Else
        '    btnTvPosterSaveBig.Visible = False
            'Button57.Visible = False
       ' End If
    End Sub

    Private Sub Button1_Click( sender As Object,  e As EventArgs) Handles Button1.Click
            If nodata Then Exit Sub
    End Sub

    Private Sub panel_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles panel1.MouseWheel
        Try
            'If TabControl2.SelectedIndex = 1 Then
                Dim mouseDelta As Integer = e.Delta / 120
                'Try
                    panel1.AutoScrollPosition = New Point(0, panel1.VerticalScroll.Value - (mouseDelta * 30))
'                Catch ex As Exception
'#If SilentErrorScream Then
'                Throw ex
'#End If
'                End Try
'            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

End Class
