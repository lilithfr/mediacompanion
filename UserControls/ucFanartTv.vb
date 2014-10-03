Imports System.Linq
Imports System.Drawing
Imports Media_Companion


Public Class ucFanartTv
    Dim WithEvents artposterpicboxes As PictureBox
    Dim WithEvents artcheckboxes As RadioButton
    Dim WithEvents tvposterlabels As Label
    Dim WithEvents tvreslabel As Label
    Dim nodata As Boolean = False
    Public Dim Form1MainFormLoadedStatus As Boolean = False
    Dim isroot As Boolean
    Dim artheight As Integer = 37
    Dim artwidth As Integer = 200
    Dim artType As String = ""
    Dim selectedimageurl As String = Nothing
    Dim FanarttvMovielist As New FanartTvMovieList
    Public messbox As New frmMessageBox("blank", "", "")
    Dim usedlist As New List(Of str_fanarttvart)
    Public workingMovDetails As New FullMovieDetails
    Dim MovfieldNames = GetType(FanarttvMovielist).GetFields().[Select](Function(field) field.Name).ToList()
    Public movFriendlyname() As String = {"HiDef ClearArt", "HiDef Logo", "Movie Art", "Movie Logo", "Movie Poster", "Movie Fanart", 
                                          "Movie Disc", "Movie Banner", "Landscape"}
    Public tvFriendlyname() As String = {"HiDef Tv Logo", "HiDef ClearArt", "Clear Logo", "Clear Art", "Tv Poster", "Tv Thumb", 
                                         "Tv Banner", "Show Background", "Season Poster", "Season Thumb",  "Character Art"}


    Public Sub ucFanartTv_Refresh(ByVal moviedetails As FullMovieDetails)
        nodata = False
        isroot = Preferences.GetRootFolderCheck(moviedetails.fileinfo.fullpathandfilename)
        If workingMovDetails.fullmoviebody.title <> moviedetails.fullmoviebody.title Then
            workingMovDetails = Form1.workingMovieDetails
        Else
            Exit Sub
        End If
        Dim ID As String = ""
        pbexists.Image = Nothing
        lblftvgroups.Items.clear
        PanelClear()
        Me.lblTitle.Text = workingMovDetails.fullmoviebody.title 
        If workingMovDetails.fullmoviebody.imdbid.Contains("tt") Then
            ID = workingMovDetails.fullmoviebody.imdbid
        ElseIf workingMovDetails.fullmoviebody.tmdbid <> "" Then
            ID = workingMovDetails.fullmoviebody.tmdbid
        Else
            Call noID
        End If
        If isroot Then RootFolderMovie()
        If nodata Then Exit Sub
        GetFanartTvArt(ID)
        If Not ConfirmIfResults() Then Exit Sub
        artheightload()
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

    Private Sub RootFolderMovie()
        MsgBox("  Selected Movie is in the Root Folder!" & vbCrLf & "Unable to save artwork from" & vbCrLf & _
               "Fanart.Tv, to the root folder" & vbCrLf & "Suggest rename movie to folder to be" & vbCrLf & _
               "compatible with ""Artwork Downloader""")
        nodata = True
    End Sub
    
    Sub artheightload()
        Dim Tabsgl As String = vbTab
        Dim Tabdbl As String = vbTab & vbTab
        lblftvgroups.Items.Clear()
        lblftvgroups.Items.Add(movFriendlyname(0) & ":" & Tabsgl & "( " & FanarttvMovielist.hdmovieclearart.count   & " )")
        lblftvgroups.Items.Add(movFriendlyname(1) & ":" & Tabsgl & "( " & FanarttvMovielist.hdmovielogo.count       & " )")
        lblftvgroups.Items.Add(movFriendlyname(2) & ":" & Tabsgl & "( " & FanarttvMovielist.movieart.count          & " )")
        lblftvgroups.Items.Add(movFriendlyname(3) & ":" & Tabsgl & "( " & FanarttvMovielist.movielogo.count         & " )")
        lblftvgroups.Items.Add(movFriendlyname(4) & ":" & Tabsgl & "( " & FanarttvMovielist.movieposter.count       & " )")
        lblftvgroups.Items.Add(movFriendlyname(5) & ":" & Tabsgl & "( " & FanarttvMovielist.moviebackground.count   & " )")
        lblftvgroups.Items.Add(movFriendlyname(6) & ":" & Tabsgl & "( " & FanarttvMovielist.moviedisc.count         & " )")
        lblftvgroups.Items.Add(movFriendlyname(7) & ":" & Tabsgl & "( " & FanarttvMovielist.moviebanner.count       & " )")
        lblftvgroups.Items.Add(movFriendlyname(8) & ":" & Tabsgl & "( " & FanarttvMovielist.moviethumb.count        & " )")
    End Sub

    Private Sub lblftvgroups_click(ByVal Sender As Object, e As EventArgs) Handles lblftvgroups.MouseDown 
        Dim indx As Integer = lblftvgroups.SelectedIndex
        Select Case indx
            Case "0"
                usedlist = FanarttvMovielist.hdmovieclearart
                artheight = 112
                artType = "clearart.png"
            Case "1"
                usedlist = FanarttvMovielist.hdmovielogo
                artheight = 77
                artType = "logo.png"
            Case "2"
                usedlist = FanarttvMovielist.movieart
                artheight = 112
                artType = "clearart.png"
            Case "3"
                usedlist = FanarttvMovielist.movielogo
                artheight = 77
                artType = "logo.png"
            Case "4"
                usedlist = FanarttvMovielist.movieposter
                artheight = 285
                artType = "poster.jpg"
            Case "5"
                usedlist = FanarttvMovielist.moviebackground
                artheight = 112
                artType = "fanart.jpg"
            Case "6"
                usedlist = FanarttvMovielist.moviedisc
                artheight = 200
                artType = "disc.png"
            Case "7"
                usedlist = FanarttvMovielist.moviebanner 
                artheight = 37
                artType = "banner.jpg"
            Case "8"
                usedlist = FanarttvMovielist.moviethumb
                artheight = 112
                artType = "landscape.jpg"
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
        Dim picratio As Decimal = 1.25
        Dim locHeight = 5
        Dim colwidth As Integer = 20
        Dim colcount As Integer = 0
        Dim panelw As Integer = Panel1.Width
        Dim pbwidth As Integer = Math.Ceiling(artwidth * picratio)
        Dim pbheight As Integer = Math.Ceiling(artheight  * picratio)
        Dim imgchkbx As Integer = Math.Floor(pbwidth / 2)
        Dim colmax As Integer = Math.Floor(panelw/pbwidth)
        Dim xspace As Integer = Math.Floor((panelw - (pbwidth * colmax)) / (colmax+1))
        Dim xstart As Integer = xspace - 10
        Dim xlocation As Integer = xstart
        Dim ylocOffset = (locHeight + pbheight + 36)
        Dim itemcounter As Integer = 0
        For each item In usedlist
            artposterpicboxes() = New PictureBox()
            With artposterpicboxes
                .Location = New Point(xlocation, locHeight)
                .Width = pbwidth 
                .Height = pbheight
                .BackColor = Color.Transparent 
                .SizeMode = PictureBoxSizeMode.Zoom
                .ImageLocation = item.urlpreview         'Preview Image url
                .Tag = item.url                          'Full Image url
                .Visible = True
                .BorderStyle = BorderStyle.Fixed3D
                .Name = "poster" & itemcounter.ToString
                AddHandler artposterpicboxes.DoubleClick, AddressOf PosterDoubleClick
            End With
            artcheckboxes() = New RadioButton()
            With artcheckboxes
                .Location = New Point(xlocation + imgchkbx, locHeight + pbheight + 4)
                .Name = "imgcheckbox" & itemcounter.ToString
                .SendToBack()
                .Text = " "
                .Tag = item.url
                AddHandler artcheckboxes.CheckedChanged, AddressOf artPosterRadioChanged
            End With
            itemcounter += 1
            Me.Panel1.Controls.Add(artposterpicboxes())
            Me.Panel1.Controls.Add(artcheckboxes())
            Me.Refresh()
            Application.DoEvents()
            xlocation += xspace + pbwidth 
            colcount += 1
            If colcount = colmax Then
                xlocation = xstart
                colcount = 0
                locHeight += ylocOffset 
            End If
        Next
        Application.DoEvents()
        Me.Refresh()
        Button1.Visible = False
        EnableFanartScrolling()
        selectedimageurl = Nothing
        DisplayExistingArt()
    End Sub

    Private Sub artPosterRadioChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim tempstring As String = sender.name
        Dim tempint As Integer = 0
        Dim tempstring2 As String = tempstring
        Dim allok As Boolean = False
        tempstring = tempstring.Replace("imgcheckbox", "")
        tempint = Convert.ToDecimal(tempstring)
        For each button as control in Panel1.controls
            If button.name.indexof("imgcheckbox") <> -1 Then
                Dim b1 As radiobutton = CType(button, radiobutton)
                If b1.Checked = True Then
                    allok = True
                    selectedimageurl = b1.tag
                    Exit For
                End if
            End if
        Next
        Button1.Visible = allok
    End Sub

    Private Sub PosterDoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim tempstring As String = sender.name.replace("poster", "imgcheckbox")
        For Each Control In Panel1.Controls
            If Control.name = tempstring Then
                Dim rb As RadioButton = Control
                rb.Checked = True
            End If
        Next
        Dim messbox As New frmMessageBox("Please wait,", "", "Downloading Full Res Image")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        messbox.Show()
        Me.Refresh()
        messbox.Refresh()
        Dim cachefile As String = Utilities.Download2Cache(sender.Tag.ToString)
        Form1.util_ZoomImage(Nothing, cachefile)
        messbox.Close()
    End Sub

    Sub EnableFanartScrolling()
        Try
            Dim rb As RadioButton = Panel1.Controls("imgcheckbox0")

            rb.Select()                       'Causes RadioButtons checked state to toggle
            rb.Checked =  False  'Not rb.Checked     'Undo unwanted checked state toggling
        Catch
        End Try
    End Sub

    Private Sub DisplayExistingArt()
        Dim LoadPath As String = Nothing
        LoadPath = IO.Path.GetDirectoryName(workingMovDetails.fileinfo.fullpathandfilename) & "\"
        If isroot Then
            LoadPath = workingMovDetails.fileinfo.fullpathandfilename.Replace(".nfo","")
        End If
        LoadPath &= artType
        If IO.File.Exists(LoadPath) Then
            Form1.util_ImageLoad(pbexists, LoadPath, "")
        Else
            pbexists.Image = Nothing
        End If
    End Sub

    Private Sub Button1_Click( sender As Object,  e As EventArgs) Handles Button1.Click
        If nodata Then Exit Sub
        If Not IsNothing(selectedimageurl) Then
            messbox = New frmMessageBox("Please wait,", "Downloading new image", "and saving")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()
            Try
                Dim savepath As String = IO.Path.GetDirectoryName(workingMovDetails.fileinfo.fullpathandfilename) & "\" & artType 
                Dim success As Boolean = Utilities.DownloadFile(selectedimageurl, savepath)
                DisplayExistingArt()
            Catch
            End Try
            messbox.Close()
        End If
    End Sub

    Private Sub panel_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles panel1.MouseWheel
        Try
            Dim mouseDelta As Integer = e.Delta / 120
            panel1.AutoScrollPosition = New Point(0, panel1.VerticalScroll.Value - (mouseDelta * 30))
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub panel_resize() Handles MyBase.resize
        If Not Form1MainFormLoadedStatus Then Exit Sub
        PanelSelectionDisplay()
    End Sub

End Class
