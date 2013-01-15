Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Linq

Public Class frmMovieFanart
    Dim WithEvents bigpicbox As PictureBox
    Dim bigpanel As Panel
    Dim WithEvents picboxes As PictureBox
    Dim WithEvents checkboxes As RadioButton
    Dim WithEvents labels As Label
    Dim WithEvents savebutton As Button
    Dim fanartpath As String = Form1.workingMovieDetails.fileinfo.fanartpath
    Dim fanartList As New List(Of str_ListOfPosters)
    Dim mainfanart As PictureBox
    Dim resolutionlbl As Label


    Private Sub zoomimage(ByVal sender As Object, ByVal e As EventArgs)

        Dim tempstring As String = sender.name
        Dim tempstring2 As String
        Dim tempint As Integer
        tempstring = tempstring.Replace("picture", "")
        tempint = Convert.ToDecimal(tempstring)
        tempstring2 = fanartList(tempint).hdUrl


        Dim buffer(4000000) As Byte
        Dim size As Integer = 0
        Dim bytesRead As Integer = 0

        bigpanel = New Panel
        With bigpanel
            .Width = Me.Width
            .Height = Me.Height
            .BringToFront()
            .Dock = DockStyle.Fill
        End With

        bigpicbox = New PictureBox()

        With bigpicbox
            .Location = New Point(0, 0)
            .Width = bigpanel.Width
            .Height = bigpanel.Height
            .SizeMode = PictureBoxSizeMode.Zoom
            '.Image = sender.image
            .ImageLocation = tempstring2
            .Visible = True
            .BorderStyle = BorderStyle.Fixed3D
            AddHandler bigpicbox.DoubleClick, AddressOf closeimage
            .Dock = DockStyle.Fill
        End With

        Dim sizex As Integer = bigpicbox.Width
        Dim sizey As Integer = bigpicbox.Height


        Me.Controls.Add(bigpanel)
        bigpanel.BringToFront()
        Me.bigpanel.Controls.Add(bigpicbox)
        Me.Refresh()

    End Sub



    Private Sub closeimage()
        Me.Controls.Remove(bigpanel)
        bigpanel = Nothing
        Me.Controls.Remove(bigpicbox)
        bigpicbox = Nothing
    End Sub



    Private Sub bigpicbox_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles bigpicbox.LoadCompleted
        Dim bigpanellabel As Label
        bigpanellabel = New Label
        With bigpanellabel
            .Location = New Point(20, 200)
            .Width = 150
            .Height = 50
            .Visible = True
            .Text = "Double Click Image To" & vbCrLf & "Return To Browser"
            '   .BringToFront()
        End With

        Me.bigpanel.Controls.Add(bigpanellabel)
        bigpanellabel.BringToFront()
        Application.DoEvents()



        If Not bigpicbox.Image Is Nothing And bigpicbox.Image.Width > 20 Then

            Dim sizey As Integer = bigpicbox.Image.Height
            Dim sizex As Integer = bigpicbox.Image.Width
            Dim tempstring As String
            tempstring = "Full Image Resolution :- " & sizex.ToString & " x " & sizey.ToString
            resolutionlbl = New Label
            With resolutionlbl
                .Location = New Point(311, 450)
                .Width = 180
                .Text = tempstring
                .BackColor = Color.Transparent
            End With

            Me.bigpanel.Controls.Add(resolutionlbl)
            resolutionlbl.BringToFront()
            Me.Refresh()
            Application.DoEvents()
            Dim tempstring2 As String = resolutionlbl.Text
        Else
            'bigpicbox.ImageLocation = posterurls(rememberint + 1, 1)
        End If
    End Sub





    Private Sub moviefanart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim exists As Boolean = System.IO.File.Exists(fanartpath)
            If exists = True Then
                mainfanart = New PictureBox
                With mainfanart
                    .Location = New Point(0, 0)
                    .Width = 423
                    .Height = 240
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                End With
                mainfanart.Visible = True
                Dim OriginalImage As New Bitmap(fanartpath)
                Dim Image2 As New Bitmap(OriginalImage)
                mainfanart.Image = Image2
                OriginalImage.Dispose()
                Me.Panel1.Controls.Add(mainfanart)
                Label2.Visible = False
            Else
                mainfanart = New PictureBox
                With mainfanart
                    .Location = New Point(0, 0)
                    .Width = 423
                    .Height = 240
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Visible = False
                    .BorderStyle = BorderStyle.Fixed3D
                End With
                Me.Panel1.Controls.Add(mainfanart)
                Label2.Visible = True
            End If

            fanartList.Clear()

            'Try
            '    Dim tmdbposterscraper As New tmdb_posters.Class1
'                Dim tmdbimageresults As String = tmdbposterscraper.gettmdbposters_newapi(Form1.workingMovieDetails.fullmoviebody.imdbid)
'                Dim bannerslist As New XmlDocument
'                bannerslist.LoadXml(tmdbimageresults)
'                Dim thisresult As XmlNode = Nothing
'                For Each item In bannerslist("tmdb_posterlist")
'                    Select Case item.name
'                        Case "fanart"
'                            Dim newfanart As New str_ListOfPosters(True)
'                            For Each backdrop In item
'                                If backdrop.childnodes(0).innertext = "original" Then
'                                    newfanart.hdposter = backdrop.childnodes(1).innertext
'                                    newfanart.hdwidth = backdrop.childnodes(2).innertext
'                                    newfanart.hdheight = backdrop.childnodes(3).innertext
'                                End If
'                                If backdrop.childnodes(0).innertext = "poster" Then
'                                    newfanart.ldposter = backdrop.childnodes(1).innertext
'                                    newfanart.ldwidth = backdrop.childnodes(2).innertext
'                                    newfanart.ldheight = backdrop.childnodes(3).innertext
'                                End If
'                                If newfanart.hdposter <> Nothing And newfanart.ldposter <> Nothing Then
'                                    If newfanart.hdposter <> "" And newfanart.ldposter <> "" Then
'                                        If newfanart.hdposter.IndexOf("http") <> -1 And newfanart.ldposter.IndexOf("http") <> -1 Then
'                                            If newfanart.hdposter.IndexOf(".jpg") <> -1 Or newfanart.hdposter.IndexOf(".png") <> -1 Then
'                                                If newfanart.ldposter.IndexOf(".jpg") <> -1 Or newfanart.ldposter.IndexOf(".png") <> -1 Then
'                                                    fanartList.Add(newfanart)
'                                                    Exit For
'                                                End If
'                                            End If
'                                        End If
'                                    End If
'                                End If
'                            Next

'                    End Select
'                Next
'            Catch ex As Exception
'#If SilentErrorScream Then
'            Throw ex
'#End If
'            End Try

            Dim tmdb As New TMDb(Form1.workingmoviedetails.fullmoviebody.imdbid)


            fanartList.AddRange(tmdb.Fanart)


            If fanartList.Count > 0 Then
                Dim location As Integer = 0
                Dim itemcounter As Integer = 0
                For Each item In fanartList
                    picboxes() = New PictureBox()

                    With picboxes
                        .Location = New Point(location, 0)
                        If fanartList.Count > 2 Then
                            .Width = 326
                            .Height = 185
                        Else
                            .Width = 353
                            .Height = 200
                        End If
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .ImageLocation = item.ldUrl
                        .Visible = True
                        .BorderStyle = BorderStyle.Fixed3D
                        .Name = "picture" & itemcounter.ToString
                        AddHandler picboxes.DoubleClick, AddressOf zoomimage
                    End With

                    checkboxes() = New RadioButton()
                    With checkboxes
                        If fanartList.Count > 2 Then
                            .Location = New Point(location + 150, 183)
                        Else
                            .Location = New Point(location + 180, 198)
                        End If
                        .Name = "checkbox" & itemcounter.ToString
                        .SendToBack()
                    End With



                    itemcounter += 1
                    location += 325

                    Me.Panel2.Controls.Add(picboxes())
                    Me.Panel2.Controls.Add(checkboxes())

                Next
            Else
                Dim mainlabel2 As Label
                mainlabel2 = New Label
                With mainlabel2
                    .Location = New Point(0, 100)
                    .Width = 700
                    .Height = 100
                    .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                    .Text = "No Fanart Was Found At www.themoviedb.org For This Movie"
                End With
                Me.Panel2.Controls.Add(mainlabel2)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Label2.Text = "Please Wait, Trying to Download Fanart"
            Me.Refresh()
            Application.DoEvents()

            Dim tempstring As String = String.Empty
            Dim tempint As Integer
            Dim tempstring2 As String = String.Empty
            Dim allok As Boolean = False
            For Each button As Control In Me.Panel2.Controls
                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        tempstring = b1.Name
                        tempstring = tempstring.Replace("checkbox", "")
                        tempint = Convert.ToDecimal(tempstring)
                        tempstring2 = fanartList(tempint).hdUrl
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If allok = False Then
                MsgBox("No Fanart Is Selected")
            Else
                Try
                    Panel1.Controls.Remove(Label1)


                 '  If Utilities.DownloadImage(tempstring2, fanartpath, True, Preferences.resizefanart) Then
                    If Movie.SaveFanartImageToCacheAndPath(tempstring2, fanartpath) Then

                        'mainfanart = New PictureBox
                        'Dim OriginalImage As New Bitmap(fanartpath)
                        'Dim Image2 As New Bitmap(OriginalImage)
                        'OriginalImage.Dispose()
                        With mainfanart
                            .Visible = True
                            .Location = New Point(0, 0)
                            .Width = 423
                            .Height = 240
                            .SizeMode = PictureBoxSizeMode.Zoom
                            .Image = Utilities.LoadImage(fanartpath)
                            .Visible = True
                            .BorderStyle = BorderStyle.Fixed3D
                            .BringToFront()
                        End With
                        Me.Panel1.Controls.Add(mainfanart)
                        Label2.Visible = False
                        Me.Close()
                    Else
                        mainfanart.Visible = False
                        Label2.Text = "No Local Fanart Is Available"
                        Label2.Visible = True
                    End If

                Catch ex As WebException
                    MsgBox(ex.Message)
                End Try

            End If

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Panel3.Visible = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btncancelgetthumburl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelgetthumburl.Click
        Try
            Panel3.Visible = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnthumbbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnthumbbrowse.Click
        Try
            'browse pc
            openFD.InitialDirectory = Form1.workingMovieDetails.fileinfo.fullpathandfilename.Replace(IO.Path.GetFileName(Form1.workingMovieDetails.fileinfo.fullpathandfilename), "")
            openFD.Title = "Select a jpeg image File"
            openFD.FileName = ""
            openFD.Filter = "Media Companion Image Files|*.jpg;*.tbn|All Files|*.*"
            openFD.FilterIndex = 0
            openFD.ShowDialog()
            TextBox5.Text = openFD.FileName
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btngetthumb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngetthumb.Click
        Try

            Movie.SaveFanartImageToCacheAndPath(TextBox5.Text,fanartpath)

            ''set thumb
            'Dim MyWebClient As New System.Net.WebClient
            'Try
            '    Dim ImageInBytes() As Byte = MyWebClient.DownloadData(TextBox5.Text)
            '    Dim ImageStream As New IO.MemoryStream(ImageInBytes)

            '    mainfanart.Image = New System.Drawing.Bitmap(ImageStream)


            '    Dim tempstring As String

            '    Dim bmp As New Bitmap(ImageStream)


            '    If Preferences.resizefanart = 1 Then
            '        Try
            '            Dim tempbitmap As Bitmap = bmp
            '            tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
            '        Catch ex As Exception
            '            tempstring = ex.Message.ToString
            '        End Try
            '    ElseIf Preferences.resizefanart = 2 Then
            '        If bmp.Width > 1280 Or bmp.Height > 720 Then
            '            Dim bm_source As New Bitmap(bmp)
            '            Dim bm_dest As New Bitmap(1280, 720)
            '            Dim gr As Graphics = Graphics.FromImage(bm_dest)
            '            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
            '            gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
            '            Dim tempbitmap As Bitmap = bm_dest
            '            tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
            '        Else
            '            Threading.Thread.Sleep(30)
            '            bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
            '        End If
            '    ElseIf Preferences.resizefanart = 3 Then
            '        If bmp.Width > 960 Or bmp.Height > 540 Then
            '            Dim bm_source As New Bitmap(bmp)
            '            Dim bm_dest As New Bitmap(960, 540)
            '            Dim gr As Graphics = Graphics.FromImage(bm_dest)
            '            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
            '            gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
            '            Dim tempbitmap As Bitmap = bm_dest
            '            tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
            '        Else
            '            Threading.Thread.Sleep(30)
            '            bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
            '        End If
            '    End If

                Me.Close()
            Catch ex As Exception
                MsgBox("Unable To Download Image")
            End Try
            Panel3.Visible = False
        'Catch ex As Exception
        '    ExceptionHandler.LogError(ex)
        'End Try

    End Sub

End Class