Imports System.Linq

Public Class ucFanartTv
    Dim WithEvents tvposterpicboxes As PictureBox
    Dim WithEvents tvpostercheckboxes As RadioButton
    Dim WithEvents tvposterlabels As Label
    Dim WithEvents tvreslabel As Label
    Dim nodata As Boolean = False
    Dim FanarttvMovielist As New FanartTvMovieList
    Public messbox As New frmMessageBox("blank", "", "")
    Dim usedlist As New List(Of str_FanartList)
    Public workingMovDetails As New FullMovieDetails
    Dim MovfieldNames = GetType(FanarttvMovielist).GetFields().[Select](Function(field) field.Name).ToList()
    Public movFriendlyname() As String = {"HiDef ClearArt", "HiDef Logo", "Movie Logo", "Movie Art", "Background", "Movie Disc", 
                                          "Movie Banner", "Movie Thumb", "Movie Poster"}
    Public tvFriendlyname() As String = {"HiDef Tv Logo", "HiDef ClearArt", "Clear Logo", "Clear Art", "Tv Poster", "Tv Thumb", 
                                         "Tv Banner", "Show Background", "Season Poster", "Season Thumb",  "Character Art"}

    Public arttype(10,1) As String


    Public Sub ucFanartTv_Refresh(ByVal moviedetails As FullMovieDetails)
        If workingMovDetails.fullmoviebody.title <> moviedetails.fullmoviebody.title Then
            workingMovDetails = Form1.workingMovieDetails
        Else
            Exit Sub
        End If
        Dim ID As String = ""
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
        LoadResults()
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

    Private Sub LoadResults()
        'Dim fieldnames() = FanarttvMovielist.GetType().GetFields().Select(f => f.Name).ToList()
        'Dim fieldNames = GetType(FanarttvMovielist).GetFields().[Select](Function(field) field.Name).ToList()
        arttypeload()

    End Sub

    Sub arttypeload()
        Dim i = MovfieldNames.Count-2
        Dim j = movFriendlyname.Count

        lblftvgroups.Items.Clear()
        For x = 0 to i
            'Dim tally As Integer = CallByName(FanarttvMovielist, MovfieldNames(x), CallType.Get)
            lblftvgroups.Items.Add(movFriendlyname(x) & ": (" & "1" & ")")
        Next


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

        For i = Panel1.Controls.Count - 1 To 0 Step -1
            Panel1.Controls.RemoveAt(i)
        Next
        
        'Dim location As Integer = 0
        'Dim itemcounter As Integer = 0
        'Dim tempboolean As Boolean = True
        ''If CheckBox8.Checked = True Or CheckBox8.Visible = False Then
        'If rbTVposter.Checked = True Or rbTVbanner.Enabled = False Then
        '    For f = tempint - 1 To tempint2 - 1
        '        If tempboolean = True Then
        '            tvposterpicboxes() = New PictureBox()
        '            With tvposterpicboxes
        '                .Location = New Point(location, 0)
        '                .Width = 123
        '                .Height = 168
        '                .SizeMode = PictureBoxSizeMode.Zoom
        '                .ImageLocation = usedlist(f).SmallUrl
        '                .Tag = usedlist(f).Url
        '                .Visible = True
        '                .BorderStyle = BorderStyle.Fixed3D
        '                .Name = "poster" & itemcounter.ToString
        '                AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
        '                'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
        '            End With

        '            tvpostercheckboxes() = New RadioButton()
        '            With tvpostercheckboxes
        '                .Location = New Point(location + 50, 166) '179
        '                .Name = "postercheckbox" & itemcounter.ToString
        '                .SendToBack()
        '                .Text = " "
        '                AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
        '            End With

        '            itemcounter += 1


        '            Me.Panel1.Controls.Add(tvposterpicboxes())
        '            Me.Panel1.Controls.Add(tvpostercheckboxes())
        '        End If
        '        If tempboolean = False Then
        '            tvposterpicboxes() = New PictureBox()
        '            With tvposterpicboxes
        '                .Location = New Point(location, 192) '210
        '                .Width = 123
        '                .Height = 168
        '                .SizeMode = PictureBoxSizeMode.Zoom
        '                .ImageLocation = usedlist(f).SmallUrl
        '                .Tag = usedlist(f).Url
        '                .Visible = True
        '                .BorderStyle = BorderStyle.Fixed3D
        '                .Name = "poster" & itemcounter.ToString
        '                AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
        '            End With

        '            tvpostercheckboxes() = New RadioButton()
        '            With tvpostercheckboxes
        '                .Location = New Point(location + 50, 358) '389
        '                .Name = "postercheckbox" & itemcounter.ToString
        '                .SendToBack()
        '                .Text = " "
        '                AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
        '            End With

        '            itemcounter += 1


        '            Me.Panel1.Controls.Add(tvposterpicboxes())
        '            Me.Panel1.Controls.Add(tvpostercheckboxes())
        '        End If
        '        Me.Refresh()
        '        Application.DoEvents()
        '        If tempboolean = False Then location += 120
        '        tempboolean = Not tempboolean
        '    Next
        'Else
        '    For f = tempint - 1 To tempint2 - 1
        '        If tempboolean = True Then
        '            tvposterpicboxes() = New PictureBox()
        '            With tvposterpicboxes
        '                .Location = New Point(0, location)
        '                .Width = 600
        '                .Height = 114
        '                .SizeMode = PictureBoxSizeMode.Zoom
        '                .ImageLocation = usedlist(f).SmallUrl
        '                .Tag = usedlist(f).Url
        '                .Visible = True
        '                .BorderStyle = BorderStyle.Fixed3D
        '                .Name = "poster" & itemcounter.ToString
        '                AddHandler tvposterpicboxes.DoubleClick, AddressOf tv_PosterDoubleClick
        '                'AddHandler tvposterpicboxes.LoadCompleted, AddressOf imageres
        '            End With

        '            tvpostercheckboxes() = New RadioButton()
        '            With tvpostercheckboxes
        '                .Location = New Point(290, location + 110)
        '                .Name = "postercheckbox" & itemcounter.ToString
        '                .SendToBack()
        '                .Text = " "
        '                AddHandler tvpostercheckboxes.CheckedChanged, AddressOf tv_PosterRadioChanged
        '            End With
        '            itemcounter += 1
        '            location += 140

        '            Me.Panel1.Controls.Add(tvposterpicboxes())
        '            Me.Panel1.Controls.Add(tvpostercheckboxes())
        '        End If
        '    Next
        'End If
        
        ''Me.Refresh()
        'Application.DoEvents()
        'If rbTVbanner.Checked AndAlso Me.Panel1.Controls.Count > 0 Then EnableTvBannerScrolling
        Me.Refresh()
    End Sub

    Private Sub Button1_Click( sender As Object,  e As EventArgs) Handles Button1.Click
            If nodata Then Exit Sub
    End Sub
End Class
