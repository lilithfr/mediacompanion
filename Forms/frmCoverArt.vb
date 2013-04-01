Imports System.Net
Imports System.IO
Imports System.Linq


Public Class frmCoverArt
    Dim bigpanel As Panel
    Dim WithEvents picboxes As PictureBox
    Dim WithEvents checkboxes As RadioButton
    Dim WithEvents labels As Label
    Dim WithEvents reslabel As Label
    Dim resolutionlbl As Label
    Dim panel2 As New Panel
    Dim posterurls(1000, 1) As String
    Dim posterpath As String
    Dim WithEvents mainposter As New PictureBox
    Dim WithEvents bigpicbox As PictureBox
    Dim count As Integer = 0
    Dim title As String = Form1.workingMovieDetails.fullmoviebody.title
    Dim itemnumber As Integer
    Dim rememberint As Integer
    Dim maxthumbs As Integer = Preferences.maximumthumbs
    Dim pagecount As Integer = 0
    Dim currentpage As Integer = 1
    Dim movieyear As String
    Dim folderjpgpath As String
    Dim tmdbid As String = Form1.workingMovieDetails.fullmoviebody.imdbid


    Private Sub coverart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            posterpath = Form1.workingMovieDetails.fileinfo.posterpath

            folderjpgpath = posterpath.Replace(IO.Path.GetFileName(posterpath), "folder.jpg")

            If Form1.workingMovieDetails.fullmoviebody.year <> Nothing Then
                movieyear = Form1.workingMovieDetails.fullmoviebody.year

            End If

            TextBox1.Text = Preferences.maximumthumbs.ToString

            Dim exists As Boolean = System.IO.File.Exists(posterpath)
            If exists = True Then

                Dim tempstring As String
                mainposter = New PictureBox
                Try
                    Dim OriginalImage As New Bitmap(posterpath)
                    Dim Image2 As New Bitmap(OriginalImage)
                    OriginalImage.Dispose()

                    With mainposter
                        .Location = New Point(0, 0)
                        .Width = 250
                        .Height = 240
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .Image = Image2
                        .Visible = True
                        .BorderStyle = BorderStyle.Fixed3D
                    End With
                    Me.Panel1.Controls.Add(mainposter)
                    tempstring = mainposter.Image.Width.ToString & " x " & mainposter.Image.Height.ToString
                    Label6.Text = tempstring
                Catch
                    mainposter = New PictureBox
                    With mainposter
                        .Location = New Point(0, 0)
                        .Width = 250
                        .Height = 240
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .Visible = False
                        .BorderStyle = BorderStyle.Fixed3D
                    End With
                    Me.Panel1.Controls.Add(mainposter)
                    Dim mainlabel As Label
                    mainlabel = New Label
                    With mainlabel
                        .Location = New Point(0, 100)
                        .Width = 423
                        .Height = 100
                        .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                        .Text = "No Local Poster Is Available For This Movie"
                        .BringToFront()
                    End With
                    Me.Panel1.Controls.Add(mainlabel)
                    Label6.Visible = False
                End Try
            Else
                mainposter = New PictureBox
                With mainposter
                    .Location = New Point(0, 0)
                    .Width = 250
                    .Height = 240
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Visible = False
                    .BorderStyle = BorderStyle.Fixed3D
                End With
                Me.Panel1.Controls.Add(mainposter)
                Dim mainlabel As Label
                mainlabel = New Label
                With mainlabel
                    .Location = New Point(0, 100)
                    .Width = 423
                    .Height = 100
                    .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                    .Text = "No Local Poster Is Available For This Movie"
                    .BringToFront()
                End With
                Me.Panel1.Controls.Add(mainlabel)
                Label6.Visible = False
            End If

            panel2 = New Panel
            With panel2
                .Width = 782
                .Height = 237
                .Location = New Point(2, 2)
                .AutoScroll = True
            End With
            Me.Controls.Add(panel2)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub radiochanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim tempstring As String = sender.name
            Dim tempint As Integer
            Dim tempstring2 As String = tempstring
            Dim allok As Boolean = False
            tempstring = tempstring.Replace("checkbox", "")
            tempint = Convert.ToDecimal(tempstring)
            For Each button As Control In Me.panel2.Controls
                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If allok = True Then
                Button5.Visible = True
                Button6.Visible = True
            Else
                Button5.Visible = False
                Button6.Visible = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub zoomimage(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim tempstring As String = sender.name
            Dim tempint As Integer
            Dim tempstring2 As String = tempstring
            tempstring = tempstring.Replace("picture", "")
            tempint = Convert.ToDecimal(tempstring)
            tempint = tempint + ((currentpage - 1) * maxthumbs)
            rememberint = tempint
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
                .ImageLocation = posterurls(tempint + 1, 0)
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
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub imageres(ByVal sender As Object, ByVal e As EventArgs)
        Try
            reslabel = New Label
            Dim tempstring As String
            tempstring = sender.image.width.ToString
            tempstring = tempstring & " x "
            tempstring = tempstring & sender.image.height.ToString
            Dim locx As Integer = sender.location.x
            Dim locy As Integer = sender.location.y
            locy = locy + sender.height
            With reslabel
                .Location = New Point(locx + 30, locy)
                .Text = tempstring
                .BringToFront()
            End With
            Me.panel2.Controls.Add(reslabel)
            Me.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub closeimage()
        Try
            Me.Controls.Remove(bigpanel)
            bigpanel = Nothing
            Me.Controls.Remove(bigpicbox)
            bigpicbox = Nothing
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Call initialise()

            'Dim tempsimdbid As String = String.Empty
          
            'Dim fanarturl As String = URLs.TMdbMovieLookup(tmdbid)
            'Dim apple2(3000) As String
            'Dim fanartlinecount As Integer = 0

            'Dim wrGETURL As WebRequest

            'wrGETURL = WebRequest.Create(fanarturl)
            'Dim myProxy As New WebProxy("myproxy", 80)
            'myProxy.BypassProxyOnLocal = True
            'Dim objStream As Stream
            'objStream = wrGETURL.GetResponse.GetResponseStream()
            'Dim objReader As New StreamReader(objStream)
            'Dim sLine As String = ""
            'fanartlinecount = 0

            'Do While Not sLine Is Nothing
            '    fanartlinecount += 1
            '    sLine = objReader.ReadLine
            '    apple2(fanartlinecount) = sLine
            'Loop

            'fanartlinecount -= 1
            'For f = 1 To fanartlinecount
            '    If apple2(f).IndexOf("<id>") <> -1 Then
            '        tempsimdbid = apple2(f)
            '        tempsimdbid = tempsimdbid.Replace("<id>", "")
            '        tempsimdbid = tempsimdbid.Replace("</id>", "")
            '        tempsimdbid = tempsimdbid.Replace("  ", "")
            '        tempsimdbid = tempsimdbid.Trim
            '        Exit For
            '    End If
            'Next

            'ReDim apple2(3000)
            'fanartlinecount = 0

            'fanarturl = URLs.TMdbGetInfo(tempsimdbid)


            'Dim wrGETURL2 As WebRequest
            'wrGETURL2 = WebRequest.Create(fanarturl)
            'Dim myProxy2 As New WebProxy("myproxy", 80)
            'myProxy2.BypassProxyOnLocal = True
            'Dim objStream2 As Stream
            'objStream2 = wrGETURL2.GetResponse.GetResponseStream()
            'Dim objReader2 As New StreamReader(objStream2)
            'Dim sLine2 As String = ""
            'fanartlinecount = 0

            'Do While Not sLine2 Is Nothing
            '    fanartlinecount += 1
            '    sLine2 = objReader2.ReadLine
            '    apple2(fanartlinecount) = sLine2
            'Loop
            'fanartlinecount -= 1
            'pagecount = 0
            'count = 0

            'For f = 1 To fanartlinecount
            '    If apple2(f).IndexOf("<poster size=""original"">") <> -1 Then
            '        count += 1
            '        posterurls(count, 0) = apple2(f)
            '        If apple2(f + 1).IndexOf("<poster size=""mid"">") <> -1 Then
            '            posterurls(count, 1) = apple2(f + 1)
            '        ElseIf posterurls(count, 1) = Nothing And apple2(f + 2).IndexOf("<poster size=""mid"">") <> -1 Then
            '            posterurls(count, 1) = apple2(f + 2)
            '        ElseIf posterurls(count, 1) = Nothing And apple2(f - 1).IndexOf("<poster size=""mid"">") <> -1 Then
            '            posterurls(count, 1) = apple2(f - 1)
            '        ElseIf posterurls(count, 1) = Nothing And apple2(f - 2).IndexOf("<poster size=""mid"">") <> -1 Then
            '            posterurls(count, 1) = apple2(f - 2)
            '        End If
            '        posterurls(count, 0) = posterurls(count, 0).Replace("<poster size=""original"">", "")
            '        posterurls(count, 0) = posterurls(count, 0).Replace("</poster>", "")
            '        posterurls(count, 1) = posterurls(count, 1).Replace("<poster size=""mid"">", "")
            '        posterurls(count, 1) = posterurls(count, 1).Replace("</poster>", "")
            '    End If
            'Next

            count = 0

            Dim tmdb As New TMDb(tmdbid)

            For Each item In tmdb.MC_Posters
                 posterurls(count, 0) = item.hdUrl   
                 posterurls(count, 1) = item.ldUrl 
                 count += 1
            Next

            Call displayselection()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Call initialise()

            Dim fanarturl As String
            Dim fanartlinecount As Integer = 0
            Dim allok As Boolean = True
            Dim apple2(10000)
            Dim first As Integer
            Dim last As Integer

            fanarturl = URLs.MoviePosterDBMovie
            Dim temp As String = tmdbid
            fanarturl = fanarturl & temp.Replace("tt", "")

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

            For f = 1 To fanartlinecount
                If apple2(f).IndexOf("<title>MoviePosterDB.com - Internet Movie Poster DataBase</title>") <> -1 Then
                    allok = False
                End If
            Next

            If allok = True Then
                For f = 1 To fanartlinecount
                    If apple2(f).IndexOf("<img src=" & URLs.MoviePosterDBPoster) <> -1 Then
                        count = count + 1
                        first = apple2(f).IndexOf("http")
                        last = apple2(f).IndexOf("jpg")
                        posterurls(count, 0) = apple2(f).Substring(first, (last + 3) - first)
                        If posterurls(count, 0).IndexOf("t_") <> -1 Then
                            posterurls(count, 0) = posterurls(count, 0).Replace("t_", "l_")
                        End If
                        If posterurls(count, 0).IndexOf("s_") <> -1 Then
                            posterurls(count, 0) = posterurls(count, 0).Replace("s_", "l_")
                        End If
                    End If
                Next

                Dim group(1000) As String
                Dim groupcount As Integer = 0
                For f = 1 To fanartlinecount
                    If apple2(f) <> Nothing Then
                        If apple2(f).IndexOf(URLs.MoviePosterDBGroup) <> -1 Then
                            If apple2(f).IndexOf("http") <> -1 And apple2(f).IndexOf(""">") <> -1 Then
                                groupcount = groupcount + 1
                                first = apple2(f).IndexOf("http")
                                last = apple2(f).IndexOf(""">")
                                group(groupcount) = apple2(f).Substring(first, last - first)
                            End If
                        End If
                    End If
                Next

                If groupcount > 0 Then
                    For g = 1 To groupcount
                        fanarturl = group(g)

                        Dim wrGETURL3 As WebRequest
                        wrGETURL3 = WebRequest.Create(fanarturl)
                        Dim myProxy3 As New WebProxy("myproxy", 80)
                        myProxy3.BypassProxyOnLocal = True
                        ReDim apple2(10000)
                        Dim objStream3 As Stream
                        objStream3 = wrGETURL3.GetResponse.GetResponseStream()
                        Dim objReader3 As New StreamReader(objStream3)
                        Dim sLine3 As String = ""
                        fanartlinecount = 0
                        fanartlinecount = 0
                        Do While Not sLine3 Is Nothing
                            fanartlinecount += 1
                            sLine3 = objReader3.ReadLine
                            apple2(fanartlinecount) = sLine3
                        Loop
                        fanartlinecount -= 1

                        For f = 1 To fanartlinecount
                            If apple2(f) <> Nothing Then
                                If apple2(f).IndexOf("<img src=" & URLs.MoviePosterDBPoster) <> -1 Then
                                    count = count + 1
                                    first = apple2(f).IndexOf("http")
                                    last = apple2(f).IndexOf("jpg")
                                    posterurls(count, 0) = apple2(f).Substring(first, (last + 3) - first)
                                    If posterurls(count, 0).IndexOf("t_") <> -1 Then
                                        posterurls(count, 0) = posterurls(count, 0).Replace("t_", "l_")
                                    End If
                                    If posterurls(count, 0).IndexOf("s_") <> -1 Then
                                        posterurls(count, 0) = posterurls(count, 0).Replace("s_", "l_")
                                    End If
                                End If
                            End If
                        Next
                    Next
                End If
            Else

            End If

            For f = 1 To count
                posterurls(f, 1) = posterurls(f, 0)
            Next
            Call displayselection()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Call initialise()
            Dim fanarturl As String
            Dim fanartlinecount As Integer = 0
            Dim allok As Boolean = True
            Dim apple2(10000)

            fanarturl = URLs.IMDBMediaIndex(tmdbid)

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

            Dim totalpages As Integer
            Dim tempint As Integer
            Dim reached As Boolean = False
            For f = 1 To fanartlinecount
                If apple2(f).IndexOf("<a href=""?page=") <> -1 Then
                    apple2(f) = apple2(f).Replace("<a href=""?page=", "")
                    apple2(f) = apple2(f).Substring(0, 1)
                    tempint = Convert.ToString(apple2(f))
                    If tempint > totalpages Then totalpages = tempint
                End If
                If apple2(f).IndexOf("<div class=""thumb_list""") <> -1 Then
                    reached = True
                End If
                If reached = True Then
                    If apple2(f).IndexOf("</div>") <> -1 Then
                        reached = False
                        Exit For
                    End If
                    If apple2(f).IndexOf("src=""http://") <> -1 Then
                        apple2(f) = apple2(f).Substring(apple2(f).IndexOf("src=""") - 1, apple2(f).Length - apple2(f).IndexOf("src=""") - 1)
                        apple2(f).TrimStart()
                        apple2(f) = apple2(f).Replace("src=""", "")
                        count = count + 1
                        posterurls(count, 0) = apple2(f).Substring(1, apple2(f).IndexOf("._V1._"))
                    End If
                End If
            Next
            For g = 2 To totalpages
                fanarturl = URLs.IMDBMediaIndexPage(tmdbid, g)
                ReDim apple2(10000)
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
                    If apple2(f).IndexOf("<div class=""thumb_list""") <> -1 Then
                        reached = True
                    End If
                    If reached = True Then
                        If apple2(f).IndexOf("</div>") <> -1 Then
                            reached = False
                            Exit For
                        End If
                        If apple2(f).IndexOf("src=""http://") <> -1 Then
                            apple2(f) = apple2(f).Substring(apple2(f).IndexOf("src=""") - 1, apple2(f).Length - apple2(f).IndexOf("src=""") - 1)
                            apple2(f).TrimStart()
                            apple2(f) = apple2(f).Replace("src=""", "")
                            count = count + 1
                            posterurls(count, 0) = apple2(f).Substring(1, apple2(f).IndexOf("._V1._"))
                        End If
                    End If
                Next
            Next
            Dim imdbcounter As Integer = 0
            For f = count To 1 Step -1
                imdbcounter += 1
                posterurls(imdbcounter, 1) = posterurls(f, 0) & "_V1._SX1000_SY1000_.jpg"
            Next
            For f = 1 To count
                posterurls(f, 0) = posterurls(f, 1)
            Next

            Call displayselection()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Call initialise()

            Dim fanarturl As String
            Dim fanartlinecount As Integer = 0
            Dim allok As Boolean = True
            Dim apple2(10000)


            fanarturl = URLs.GoogleFanArt

            title = title.ToLower
            title = title.Replace(" ", "+")
            title = title.Replace("&", "%26")
            title = title.Replace("À", "%c0")
            title = title.Replace("Á", "%c1")
            title = title.Replace("Â", "%c2")
            title = title.Replace("Ã", "%c3")
            title = title.Replace("Ä", "%c4")
            title = title.Replace("Å", "%c5")
            title = title.Replace("Æ", "%c6")
            title = title.Replace("Ç", "%c7")
            title = title.Replace("È", "%c8")
            title = title.Replace("É", "%c9")
            title = title.Replace("Ê", "%ca")
            title = title.Replace("Ë", "%cb")
            title = title.Replace("Ì", "%cc")
            title = title.Replace("Í", "%cd")
            title = title.Replace("Î", "%ce")
            title = title.Replace("Ï", "%cf")
            title = title.Replace("Ð", "%d0")
            title = title.Replace("Ñ", "%d1")
            title = title.Replace("Ò", "%d2")
            title = title.Replace("Ó", "%d3")
            title = title.Replace("Ô", "%d4")
            title = title.Replace("Õ", "%d5")
            title = title.Replace("Ö", "%d6")
            title = title.Replace("Ø", "%d8")
            title = title.Replace("Ù", "%d9")
            title = title.Replace("Ú", "%da")
            title = title.Replace("Û", "%db")
            title = title.Replace("Ü", "%dc")
            title = title.Replace("Ý", "%dd")
            title = title.Replace("Þ", "%de")
            title = title.Replace("ß", "%df")
            title = title.Replace("à", "%e0")
            title = title.Replace("á", "%e1")
            title = title.Replace("â", "%e2")
            title = title.Replace("ã", "%e3")
            title = title.Replace("ä", "%e4")
            title = title.Replace("å", "%e5")
            title = title.Replace("æ", "%e6")
            title = title.Replace("ç", "%e7")
            title = title.Replace("è", "%e8")
            title = title.Replace("é", "%e9")
            title = title.Replace("ê", "%ea")
            title = title.Replace("ë", "%eb")
            title = title.Replace("ì", "%ec")
            title = title.Replace("í", "%ed")
            title = title.Replace("î", "%ee")
            title = title.Replace("ï", "%ef")
            title = title.Replace("ð", "%f0")
            title = title.Replace("ñ", "%f1")
            title = title.Replace("ò", "%f2")
            title = title.Replace("ó", "%f3")
            title = title.Replace("ô", "%f4")
            title = title.Replace("õ", "%f5")
            title = title.Replace("ö", "%f6")
            title = title.Replace("÷", "%f7")
            title = title.Replace("ø", "%f8")
            title = title.Replace("ù", "%f9")
            title = title.Replace("ú", "%fa")
            title = title.Replace("û", "%fb")
            title = title.Replace("ü", "%fc")
            title = title.Replace("ý", "%fd")
            title = title.Replace("þ", "%fe")
            title = title.Replace("ÿ", "%ff")
            title = title.Replace(" ", "+")
            title = title.Replace("&", "%26")
            fanarturl = fanarturl & title & "+" & movieyear
            fanarturl = fanarturl & "&sitesearch=www.impawards.com"
            ReDim apple2(10000)
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

            For f = 1 To fanartlinecount
                If apple2(f).indexof("http://www.impawards.com/") <> -1 Then
                    Dim first As Integer = apple2(f).indexof("http://www.impawards.com/")
                    apple2(f) = apple2(f).substring(first, apple2(f).length - first)
                    fanarturl = apple2(f).substring(0, apple2(f).indexof("html") + 4)
                End If
            Next



            Dim tempint As Integer
            Dim tempstring As String
            tempstring = fanarturl.Replace("http://", "")
            tempint = tempstring.LastIndexOf("/")
            If tempint - 5 = tempstring.IndexOf("/") Then
                allok = True
            Else
                'fanarturl = "http://www.impawards.com/googlesearch.html?cx=partner-pub-6811780361519631%3A48v46vdqqnk&cof=FORID%3A9&ie=ISO-8859-1&q="
                fanarturl = "http://www.google.com/custom?hl=en&cof=FORID%3A1%3BGL%3A1%3BLBGC%3A000000%3BBGC%3A%23000000%3BT%3A%23cccccc%3BLC%3A%2333cc33%3BVLC%3A%2333ff33%3BGALT%3A%2333CC33%3BGFNT%3A%23ffffff%3BGIMP%3A%23ffffff%3B&domains=www.impawards.com&ie=ISO-8859-1&oe=ISO-8859-1&q="
                fanarturl = fanarturl & title
                fanarturl = fanarturl & "&sitesearch=www.impawards.com"
                ReDim apple2(3000)
                fanartlinecount = 0
                Dim wrGETURL4 As WebRequest
                wrGETURL4 = WebRequest.Create(fanarturl)
                Dim myProxy4 As New WebProxy("myproxy", 80)
                myProxy4.BypassProxyOnLocal = True
                Dim objStream4 As Stream
                objStream4 = wrGETURL4.GetResponse.GetResponseStream()
                Dim objReader4 As New StreamReader(objStream4)
                Dim sLine4 As String = ""
                fanartlinecount = 0

                Do While Not sLine4 Is Nothing
                    fanartlinecount += 1
                    sLine4 = objReader4.ReadLine
                    apple2(fanartlinecount) = sLine4
                Loop
                fanartlinecount -= 1
                Dim first As Integer
                For g = 1 To fanartlinecount
                    If apple2(g).IndexOf("http://www.impawards.com/") <> -1 Then
                        first = apple2(g).IndexOf("http://www.impawards.com/")
                        apple2(g) = apple2(g).Substring(first, apple2(g).Length - first)
                        fanarturl = apple2(g).Substring(0, apple2(g).IndexOf("html") + 4)
                        tempstring = fanarturl
                        tempstring = tempstring.Replace("http://", "")
                        tempint = tempstring.LastIndexOf("/")
                        If tempint - 5 = tempstring.IndexOf("/") Then
                            allok = True
                        Else
                            allok = False
                        End If
                    End If
                Next
            End If

            If fanarturl.IndexOf("art_machine") = -1 And allok = True Then
                count = 1
                ReDim apple2(10000)
                fanartlinecount = 0
                Dim wrGETURL As WebRequest
                wrGETURL = WebRequest.Create(fanarturl)
                Dim myProxy As New WebProxy("myproxy", 80)
                myProxy2.BypassProxyOnLocal = True
                Dim objStream As Stream
                objStream = wrGETURL.GetResponse.GetResponseStream()
                Dim objReader As New StreamReader(objStream)
                Dim sLine As String = ""
                fanartlinecount = -1
                Dim vertest As Boolean = False
                Do While Not sLine Is Nothing
                    fanartlinecount += 1
                    sLine = objReader.ReadLine
                    apple2(fanartlinecount) = sLine
                Loop
                fanartlinecount -= 1
                Dim highest As Integer = 0
                Dim version As Boolean = False
                For f = 1 To fanartlinecount
                    If apple2(f).indexof("ver") <> -1 Then
                        For g = 1 To 50
                            Dim tempstring2 As String = "ver" & g.ToString & "."
                            If apple2(f).IndexOf(tempstring2) <> -1 Then
                                If g = 1 Then
                                    version = True
                                End If
                                If g > highest Then
                                    highest = g
                                End If
                            End If
                        Next
                    End If
                Next
                Dim tempstring3 As String
                Dim tempstring4 As String
                tempstring3 = fanarturl.Substring(0, fanarturl.LastIndexOf("/") + 1)
                tempstring4 = fanarturl.Substring(fanarturl.LastIndexOf("/") + 1, fanarturl.Length - fanarturl.LastIndexOf("/") - 1)
                fanarturl = tempstring3 & "posters/" & tempstring4
                fanarturl = fanarturl.Replace(".html", "")
                If fanarturl.IndexOf("_ver") <> -1 Then
                    fanarturl = fanarturl.Substring(0, fanarturl.IndexOf("_ver"))
                End If

                If highest > count Then count = highest
                If version = True Then
                    posterurls(1, 1) = fanarturl & "_ver1.jpg"
                    posterurls(1, 0) = fanarturl & "_ver1_xlg.jpg"
                Else
                    posterurls(1, 1) = fanarturl & ".jpg"
                    posterurls(1, 0) = fanarturl & "_xlg.jpg"
                End If

                For f = 2 To count
                    posterurls(f, 1) = fanarturl & "_ver" & f.ToString & ".jpg"
                    posterurls(f, 0) = fanarturl & "_ver" & f.ToString & "_xlg.jpg"
                Next
            End If

            Call displayselection()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub bigpicbox_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles bigpicbox.LoadCompleted
        Try
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
                bigpicbox.ImageLocation = posterurls(rememberint + 1, 1)
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub



    Private Sub mainposter_BackgroundImageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mainposter.BackgroundImageChanged
        Try
            If Not mainposter.Image Is Nothing And mainposter.Image.Width > 20 Then
                Dim tempstring As String
                Label6.Visible = True
                tempstring = mainposter.Image.Width.ToString & " x " & mainposter.Image.Height.ToString
                Label6.Text = tempstring
            Else
                Label6.Visible = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Try
            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> Chr(8) Then
                If TextBox1.Text <> "" Then
                    If Convert.ToDecimal(TextBox1.Text) >= 1 Then
                        e.Handled = True
                        maxthumbs = Convert.ToDecimal(TextBox1.Text)
                        Preferences.maximumthumbs = maxthumbs
                    Else
                        MsgBox("Please Enter A Number More Than 0")
                    End If
                Else
                    MsgBox("Please Enter A Number More Than 0")
                End If
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            Button5.Visible = False
            Me.Controls.Remove(panel2)
            panel2 = Nothing
            picboxes = Nothing
            checkboxes = Nothing

            panel2 = New Panel
            With panel2
                .Width = 782
                .Height = 232
                .Location = New Point(2, 2)
                .AutoScroll = True
            End With
            Me.Controls.Add(panel2)

            currentpage += 1
            Button7.Enabled = True

            If currentpage = pagecount Then
                Button8.Enabled = False
            Else
                Button8.Enabled = True
            End If


            Dim tempint As Integer = (currentpage * (maxthumbs) + 1) - maxthumbs
            Dim tempint2 As Integer = currentpage * maxthumbs

            If tempint2 > count Then
                tempint2 = count
            End If

            Dim names As New List(Of String)()

            For f = tempint To tempint2
                names.Add(posterurls(f, 1))
            Next
            Label7.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & count.ToString & " Images"

            Dim location As Integer = 0
            Dim itemcounter As Integer = 0
            For Each item As String In names


                picboxes() = New PictureBox()
                With picboxes
                    .Location = New Point(location, 0)
                    .Width = 140
                    .Height = 180
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "picture" & itemcounter.ToString
                    AddHandler picboxes.DoubleClick, AddressOf zoomimage
                    AddHandler picboxes.LoadCompleted, AddressOf imageres
                End With

                checkboxes() = New RadioButton()
                With checkboxes
                    .Location = New Point(location + 60, 195)
                    .Name = "checkbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler checkboxes.CheckedChanged, AddressOf radiochanged
                End With

                itemcounter += 1
                location += 160

                Me.panel2.Controls.Add(picboxes())
                Me.panel2.Controls.Add(checkboxes())
                Me.Refresh()
                Application.DoEvents()
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Button5.Visible = False
            Me.Controls.Remove(panel2)
            panel2 = Nothing
            picboxes = Nothing
            checkboxes = Nothing

            panel2 = New Panel
            With panel2
                .Width = 782
                .Height = 232
                .Location = New Point(2, 2)
                .AutoScroll = True
            End With
            Me.Controls.Add(panel2)

            currentpage -= 1
            Button7.Enabled = True
            If currentpage <= 1 Then
                Button7.Enabled = False
            Else
                Button7.Enabled = True
            End If
            If currentpage = pagecount Then
                Button8.Enabled = False
            Else
                Button8.Enabled = True
            End If
            Dim tempint As Integer = (currentpage * (maxthumbs) + 1) - maxthumbs
            Dim tempint2 As Integer = currentpage * maxthumbs
            If tempint2 > count Then
                tempint2 = count
            End If

            Dim names As New List(Of String)()

            For f = tempint To tempint2
                names.Add(posterurls(f, 1))
            Next
            Label7.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & count.ToString & " Images"

            Dim location As Integer = 0
            Dim itemcounter As Integer = 0
            For Each item As String In names
                picboxes() = New PictureBox()
                With picboxes
                    .Location = New Point(location, 0)
                    .Width = 140
                    .Height = 180
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "picture" & itemcounter.ToString
                    AddHandler picboxes.DoubleClick, AddressOf zoomimage
                    AddHandler picboxes.LoadCompleted, AddressOf imageres
                End With

                checkboxes() = New RadioButton()
                With checkboxes
                    .Location = New Point(location + 60, 195)
                    .Name = "checkbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler checkboxes.CheckedChanged, AddressOf radiochanged
                End With

                itemcounter += 1
                location += 160

                Me.panel2.Controls.Add(picboxes())
                Me.panel2.Controls.Add(checkboxes())
                Me.Refresh()
                Application.DoEvents()
            Next
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub displayselection()
        Dim names As New List(Of String)()

        If count > 0 Then

            If count > maxthumbs Then
                Dim tempmaxthumbs As Integer = count

                Do Until tempmaxthumbs < 1
                    pagecount += 1
                    tempmaxthumbs -= maxthumbs
                Loop
            End If


            If count > maxthumbs Then
                For f = 1 To maxthumbs
                    names.Add(posterurls(f, 1))
                Next
            Else
                For f = 1 To count
                    names.Add(posterurls(f, 1))
                Next
            End If

            Label7.Visible = True
            If pagecount > 1 Then
                Button7.Visible = True
                Button8.Visible = True
                If count >= maxthumbs Then
                    Label7.Text = "Displaying 1 to " & maxthumbs.ToString & " of " & count.ToString & " Images"
                Else
                    Label7.Text = "Displaying 1 to " & count.ToString & " of " & count.ToString & " Images"
                End If
                currentpage = 1
                Button7.Enabled = False
                Button8.Enabled = True
            Else
                Button7.Visible = False
                Button8.Visible = False
                If count >= maxthumbs Then
                    Label7.Text = "Displaying 1 to " & maxthumbs.ToString & " of " & count.ToString & " Images"
                Else
                    Label7.Text = "Displaying 1 to " & count.ToString & " of " & count.ToString & " Images"
                End If
            End If

            Dim location As Integer = 0
            Dim itemcounter As Integer = 0
            For Each item As String In names


                picboxes() = New PictureBox()
                With picboxes
                    .Location = New Point(location, 0)
                    .Width = 123
                    .Height = 180
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "picture" & itemcounter.ToString
                    AddHandler picboxes.DoubleClick, AddressOf zoomimage
                    AddHandler picboxes.LoadCompleted, AddressOf imageres
                End With

                checkboxes() = New RadioButton()
                With checkboxes
                    .Location = New Point(location + 50, 195)
                    .Name = "checkbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler checkboxes.CheckedChanged, AddressOf radiochanged
                End With

                itemcounter += 1
                location += 120

                Me.panel2.Controls.Add(picboxes())
                Me.panel2.Controls.Add(checkboxes())
                Me.Refresh()
                Application.DoEvents()
            Next
        Else
            Dim mainlabel2 As Label
            mainlabel2 = New Label
            With mainlabel2
                .Location = New Point(0, 100)
                .Width = 700
                .Height = 100
                .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                .Text = "No Posters Were Found For This Movie"

            End With
            Me.panel2.Controls.Add(mainlabel2)
        End If
    End Sub

    Private Sub initialise()
        If TextBox1.Text <> "" Then
            If IsNumeric(TextBox1.Text) And Convert.ToDecimal(TextBox1.Text) <> 0 Then
                maxthumbs = Convert.ToDecimal(TextBox1.Text)
                Preferences.maximumthumbs = maxthumbs
            Else
                MsgBox("Invalid Maximum Thumb Value" & vbCrLf & "Setting to default Value of 10")
                maxthumbs = 10
                TextBox1.Text = "10"
                Preferences.maximumthumbs = 10
            End If
        Else
            MsgBox("Invalid Maximum Thumb Value" & vbCrLf & "Setting to default Value of 10")
            maxthumbs = 10
            TextBox1.Text = "10"
            Preferences.maximumthumbs = 10
        End If

        Button5.Visible = False
        Button6.Visible = False
        Me.Controls.Remove(panel2)
        panel2 = Nothing
        picboxes = Nothing
        checkboxes = Nothing
        reslabel = Nothing

        panel2 = New Panel
        With panel2
            .Width = Me.Width
            .Height = 237
            .Location = New Point(2, 2)
            .AutoScroll = True
        End With
        Me.Controls.Add(panel2)

        panel2.Refresh()
        Application.DoEvents()

        ReDim posterurls(1000, 1)
        count = 0
        pagecount = 0
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim tempstring As String
            Dim realnumber As Integer = 0
            Dim tempint As Integer = 0
            Dim tempstring2 As String = ""
            Dim allok As Boolean = False
            For Each button As Control In Me.panel2.Controls
                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        tempstring = b1.Name
                        tempstring = tempstring.Replace("checkbox", "")
                        tempint = Convert.ToDecimal(tempstring)
                        realnumber = tempint + ((currentpage - 1) * maxthumbs)
                        tempstring2 = posterurls(realnumber, 1)
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If allok = False Then
                MsgBox("No Fanart Is Selected")
            End If
            If allok = True Then
                For Each PictureBox2 As Control In Me.panel2.Controls
                    If PictureBox2.Name.IndexOf("picture") <> -1 And PictureBox2.Name.IndexOf(tempint) <> -1 Then
                        Dim b1 As PictureBox = CType(PictureBox2, PictureBox)
                        If Not b1.Image Is Nothing Then
                            If b1.Image.Width > 20 Then
                                b1.Image.Save(posterpath)
                                If Preferences.createfolderjpg = True Then
                                    b1.Image.Save(folderjpgpath)
                                End If
                                If Preferences.FrodoEnabled and Preferences.EdenEnabled then
                                    Dim frodopath As String = posterpath.Replace(".tbn","-poster.jpg")
                                    b1.Image.Save(frodopath)
                                    posterpath=frodopath
                                End If
                                Form2.moviethumb.Image = b1.Image
                                Form1.moviethumb.Image = b1.Image
                                mainposter.Image = b1.Image
                                Label6.Visible = True
                                tempstring = b1.Image.Width.ToString & " x " & b1.Image.Height.ToString
                                Label6.Text = tempstring
                                mainposter.Visible = True
                                Me.Close()
                                Exit For
                            Else
                                Label6.Visible = False
                            End If
                        Else
                            Label6.Visible = False
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim tempstring As String
            Dim tempint As Integer = 0
            Dim realnumber As Integer = 0
            Dim tempstring2 As String = ""
            Dim allok As Boolean = False
            For Each button As Control In Me.panel2.Controls
                If button.Name.IndexOf("checkbox") <> -1 Then
                    Dim b1 As RadioButton = CType(button, RadioButton)
                    If b1.Checked = True Then
                        tempstring = b1.Name
                        tempstring = tempstring.Replace("checkbox", "")
                        tempint = Convert.ToDecimal(tempstring)
                        realnumber = tempint + ((currentpage - 1) * maxthumbs)
                        tempstring2 = posterurls(realnumber, 1)
                        allok = True
                        Exit For
                    End If
                End If
            Next
            If allok = False Then
                MsgBox("No Fanart Is Selected")
            End If
            If allok = True Then
                For Each PictureBox2 As Control In Me.panel2.Controls
                    If PictureBox2.Name.IndexOf("picture") <> -1 And PictureBox2.Name.IndexOf(tempint.ToString) <> -1 Then
                        Dim b1 As PictureBox = CType(PictureBox2, PictureBox)
                        If Not b1.Image Is Nothing Then
                            If b1.Image.Width > 20 Then
                                With b1
                                    .WaitOnLoad = True
                                    Try
                                        .ImageLocation = (posterurls(realnumber + 1, 0))
                                    Catch
                                        .ImageLocation = (posterurls(realnumber + 1, 1))
                                    End Try
                                End With
                                b1.Image.Save(posterpath)
                                If Preferences.createfolderjpg = True Then
                                    b1.Image.Save(folderjpgpath)
                                End If
                                If Preferences.FrodoEnabled and Preferences.EdenEnabled then
                                    Dim frodopath As String = posterpath.Replace(".tbn","-poster.jpg")
                                    b1.Image.Save(frodopath)
                                    posterpath=frodopath
                                End If
                                Form2.moviethumb.Image = b1.Image
                                Form1.moviethumb.Image = b1.Image
                                mainposter.Image = b1.Image
                                Label6.Visible = True
                                tempstring = b1.Image.Width.ToString & " x " & b1.Image.Height.ToString
                                Label6.Text = tempstring
                                mainposter.Visible = True
                                With b1
                                    .WaitOnLoad = True
                                    .ImageLocation = (posterurls(realnumber + 1, 1))
                                End With
                                Me.Close()
                                Exit For
                            Else
                                Label6.Visible = False
                            End If
                        Else
                            Label6.Visible = False
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub




    Private Sub coverart_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            '77%
            panel2.Width = Me.Width
            mainposter.Width = Panel1.Width
            mainposter.Height = Panel1.Height
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub btnthumbbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnthumbbrowse.Click
        Try
            openFD.InitialDirectory = Form1.workingMovieDetails.fileinfo.fullpathandfilename.Replace(IO.Path.GetFileName(Form1.workingMovieDetails.fileinfo.fullpathandfilename), "")
            openFD.Title = "Select a jpeg image file File"
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
            Dim MyWebClient As New System.Net.WebClient
            Try
                Dim ImageInBytes() As Byte = MyWebClient.DownloadData(TextBox5.Text)
                Dim ImageStream As New IO.MemoryStream(ImageInBytes)

                mainposter.Image = New System.Drawing.Bitmap(ImageStream)
                mainposter.Image.Save(posterpath)
                Form2.moviethumb.Image = mainposter.Image
                Form1.moviethumb.Image = mainposter.Image
            Catch ex As Exception
                MsgBox("Unable To Download Image")
            End Try
            Panel3.Visible = False
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

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Panel3.Visible = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


End Class