
Imports System.Net
Imports System.IO


Public Class frmMovieFanart
    Dim WithEvents bigpicbox As PictureBox
    Dim bigpanel As Panel
    Dim WithEvents picboxes As PictureBox
    Dim WithEvents checkboxes As RadioButton
    Dim WithEvents labels As Label
    Dim WithEvents savebutton As Button
    Dim fanarturls(1000, 1) As String
    Dim fanartpath As String = Form1.workingMovieDetails.fileinfo.fanartpath
    Dim mainfanart As PictureBox
    Dim resolutionlbl As Label


    Private Sub zoomimage(ByVal sender As Object, ByVal e As EventArgs)

        Dim tempstring As String = sender.name
        Dim tempstring2 As String
        Dim tempint As Integer
        tempstring = tempstring.Replace("picture", "")
        tempint = Convert.ToDecimal(tempstring)
        tempstring2 = fanarturls(tempint + 1, 0)


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

            Dim tmdbid As String = String.Empty
            Dim temp As String = Form1.workingMovieDetails.fullmoviebody.imdbid
            Dim fanarturl As String = URLs.TMdbMovieLookup(temp)
            Dim apple2(2000) As String
            Dim fanartlinecount As Integer = 0
                Dim wrGETURL As WebRequest

                wrGETURL = WebRequest.Create(fanarturl)
                Dim myProxy As New WebProxy("myproxy", 80)
                myProxy.BypassProxyOnLocal = True
                Dim objStream As Stream
                objStream = wrGETURL.GetResponse.GetResponseStream()
                Dim objReader As New StreamReader(objStream)
                Dim sLine As String = ""
                fanartlinecount = 0

                Do While Not sLine Is Nothing
                    fanartlinecount += 1
                    sLine = objReader.ReadLine
                    apple2(fanartlinecount) = sLine
                Loop

                fanartlinecount -= 1
                For f = 1 To fanartlinecount
                    If apple2(f).IndexOf("<id>") <> -1 Then
                        tmdbid = apple2(f)
                        tmdbid = tmdbid.Replace("<id>", "")
                        tmdbid = tmdbid.Replace("</id>", "")
                        tmdbid = tmdbid.Replace("  ", "")
                        tmdbid = tmdbid.Trim
                        Exit For
                    End If
            Next
        ReDim apple2(2000)
        fanartlinecount = 0

            fanarturl = URLs.TMdbGetInfo(tmdbid)


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

            Dim wrGETURL2 As WebRequest
            wrGETURL2 = WebRequest.Create(fanarturl)
            Dim myProxy2 As New WebProxy("myproxy", 80)
            myProxy2.BypassProxyOnLocal = True
            Dim objStream2 As Stream
            objStream2 = wrGETURL2.GetResponse.GetResponseStream()
            Dim objReader2 As New StreamReader(objStream2)
            Dim sLine2 As String = ""
            fanartlinecount = 0

            Do While Not sLine2 Is Nothing
                fanartlinecount += 1
                sLine2 = objReader2.ReadLine
                apple2(fanartlinecount) = sLine2
            Loop
            fanartlinecount -= 1
            Dim count As Integer = 0
            For f = 1 To fanartlinecount
                If apple2(f).IndexOf("<backdrop size=""original"">") <> -1 Then
                    count += 1
                    fanarturls(count, 0) = apple2(f)
                    If apple2(f + 1).IndexOf("<backdrop size=""mid"">") <> -1 Then
                        fanarturls(count, 1) = apple2(f + 1)
                    ElseIf apple2(f + 2).IndexOf("<backdrop size=""mid"">") <> -1 Then
                        fanarturls(count, 1) = apple2(f + 2)
                    ElseIf apple2(f - 1).IndexOf("<backdrop size=""mid"">") <> -1 Then
                        fanarturls(count, 1) = apple2(f - 1)
                    ElseIf apple2(f - 2).IndexOf("<backdrop size=""mid"">") <> -1 Then
                        fanarturls(count, 1) = apple2(f - 2)
                    End If

                    fanarturls(count, 0) = fanarturls(count, 0).Replace("<backdrop size=""original"">", "")
                    fanarturls(count, 0) = fanarturls(count, 0).Replace("</backdrop>", "")
                    fanarturls(count, 1) = fanarturls(count, 1).Replace("<backdrop size=""mid"">", "")
                    fanarturls(count, 1) = fanarturls(count, 1).Replace("</backdrop>", "")
                    'Exit For
                End If
            Next

            Dim names As New List(Of String)()

            If count > 0 Then

                For f = 1 To count

                    names.Add(fanarturls(f, 1))
                Next


                Dim location As Integer = 0
                Dim itemcounter As Integer = 0
                For Each item As String In names
                    picboxes() = New PictureBox()

                    With picboxes
                        .Location = New Point(location, 0)
                        If count > 2 Then
                            .Width = 315
                            .Height = 179
                        Else
                            .Width = 353
                            .Height = 200
                        End If
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .ImageLocation = item
                        .Visible = True
                        .BorderStyle = BorderStyle.Fixed3D
                        .Name = "picture" & itemcounter.ToString
                        AddHandler picboxes.DoubleClick, AddressOf zoomimage
                    End With

                    checkboxes() = New RadioButton()
                    With checkboxes
                        If count > 2 Then
                            .Location = New Point(location + 150, 176)
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
                        tempstring2 = fanarturls(tempint + 1, 0)
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

                    Dim buffer(4000000) As Byte
                    Dim size As Integer = 0
                    Dim bytesRead As Integer = 0

                    Dim fanartthumburl As String = tempstring2
                    Dim req As HttpWebRequest = WebRequest.Create(fanartthumburl)
                    Dim res As HttpWebResponse = req.GetResponse()
                    Dim contents As Stream = res.GetResponseStream()
                    Dim bmp As New Bitmap(contents)


                    Dim bytesToRead As Integer = CInt(buffer.Length)

                    While bytesToRead > 0
                        size = contents.Read(buffer, bytesRead, bytesToRead)
                        If size = 0 Then Exit While
                        bytesToRead -= size
                        bytesRead += size
                    End While
                    If Preferences.resizefanart = 1 Then
                        Try
                            Dim tempbitmap As Bitmap = bmp
                            tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                        Catch ex As Exception
                            tempstring = ex.Message.ToString
                        End Try
                    ElseIf Preferences.resizefanart = 2 Then
                        If bmp.Width > 1280 Or bmp.Height > 720 Then
                            Dim bm_source As New Bitmap(bmp)
                            Dim bm_dest As New Bitmap(1280, 720)
                            Dim gr As Graphics = Graphics.FromImage(bm_dest)
                            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                            gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                            Dim tempbitmap As Bitmap = bm_dest
                            tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                        Else
                            Threading.Thread.Sleep(30)
                            bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                        End If
                    ElseIf Preferences.resizefanart = 3 Then
                        If bmp.Width > 960 Or bmp.Height > 540 Then
                            Dim bm_source As New Bitmap(bmp)
                            Dim bm_dest As New Bitmap(960, 540)
                            Dim gr As Graphics = Graphics.FromImage(bm_dest)
                            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                            gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                            Dim tempbitmap As Bitmap = bm_dest
                            tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                        Else
                            Threading.Thread.Sleep(30)
                            bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                        End If
                    End If
                    Dim exists As Boolean = System.IO.File.Exists(fanartpath)
                    If exists = True Then


                        'mainfanart = New PictureBox
                        Dim OriginalImage As New Bitmap(fanartpath)
                        Dim Image2 As New Bitmap(OriginalImage)
                        OriginalImage.Dispose()
                        With mainfanart
                            .Visible = True
                            .Location = New Point(0, 0)
                            .Width = 423
                            .Height = 240
                            .SizeMode = PictureBoxSizeMode.Zoom
                            .Image = Image2
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
            'set thumb
            Dim MyWebClient As New System.Net.WebClient
            Try
                Dim ImageInBytes() As Byte = MyWebClient.DownloadData(TextBox5.Text)
                Dim ImageStream As New IO.MemoryStream(ImageInBytes)

                mainfanart.Image = New System.Drawing.Bitmap(ImageStream)


                Dim tempstring As String

                Dim bmp As New Bitmap(ImageStream)


                If Preferences.resizefanart = 1 Then
                    Try
                        Dim tempbitmap As Bitmap = bmp
                        tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                    Catch ex As Exception
                        tempstring = ex.Message.ToString
                    End Try
                ElseIf Preferences.resizefanart = 2 Then
                    If bmp.Width > 1280 Or bmp.Height > 720 Then
                        Dim bm_source As New Bitmap(bmp)
                        Dim bm_dest As New Bitmap(1280, 720)
                        Dim gr As Graphics = Graphics.FromImage(bm_dest)
                        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                        gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                        Dim tempbitmap As Bitmap = bm_dest
                        tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                    Else
                        Threading.Thread.Sleep(30)
                        bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                    End If
                ElseIf Preferences.resizefanart = 3 Then
                    If bmp.Width > 960 Or bmp.Height > 540 Then
                        Dim bm_source As New Bitmap(bmp)
                        Dim bm_dest As New Bitmap(960, 540)
                        Dim gr As Graphics = Graphics.FromImage(bm_dest)
                        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                        gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                        Dim tempbitmap As Bitmap = bm_dest
                        tempbitmap.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                    Else
                        Threading.Thread.Sleep(30)
                        bmp.Save(fanartpath, Imaging.ImageFormat.Jpeg)
                    End If
                End If

                Me.Close()
            Catch ex As Exception
                MsgBox("Unable To Download Image")
            End Try
            Panel3.Visible = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

End Class