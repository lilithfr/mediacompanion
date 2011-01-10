Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Text.RegularExpressions


Public Class frmTVShowBrowser
    Dim actorcount As Integer = 0
    Dim languages(100, 1) As String
    Dim languagecount As Integer = 0
    Dim url As String
    Dim websource(10000) As String
    Dim urllinecount As Integer = 0
    Dim returnedresults(100, 2) As String
    Dim resultcount As Integer = 0
    Dim languagecode As String = Form1.userprefs.tvdblanguagecode
    Dim actors(1000, 3) As String




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call loadresults()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call loadresults()
        End If

    End Sub

    Private Sub loadresults()
        PictureBox1.Image = Nothing
        ListBox1.Items.Clear()
        ReDim returnedresults(100, 2)
        resultcount = 0
        url = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & TextBox1.Text & "&language=all"
        Call loadwebpage()
        For f = 1 To urllinecount
            If websource(f).IndexOf("<seriesid>") <> -1 Then
                resultcount += 1
                websource(f) = websource(f).Replace("<seriesid>", "")
                websource(f) = websource(f).Replace("</seriesid>", "")
                websource(f) = websource(f).Replace("  ", "")
                returnedresults(resultcount, 1) = websource(f)
                websource(f + 2) = websource(f + 2).Replace("<SeriesName>", "")
                websource(f + 2) = websource(f + 2).Replace("</SeriesName>", "")
                websource(f + 2) = websource(f + 2).Replace("  ", "")
                returnedresults(resultcount, 0) = websource(f + 2)
                If websource(f + 3).IndexOf("<banner>") <> -1 Then
                    websource(f + 3) = websource(f + 3).Replace("<banner>", "")
                    websource(f + 3) = websource(f + 3).Replace("</banner>", "")
                    websource(f + 3) = websource(f + 3).Replace("  ", "")
                    returnedresults(resultcount, 2) = websource(f + 3)
                    returnedresults(resultcount, 2) = "http://images.thetvdb.com/banners/_cache/" & returnedresults(resultcount, 2)
                End If
            End If
        Next

        If resultcount > 0 Then
            Panel1.Visible = True
            For f = 1 To resultcount
                ListBox1.Items.Add(returnedresults(f, 0))
            Next
            Try
                ListBox1.SelectedIndex = 0
            Catch
            End Try
        Else
            MsgBox("Zero Results Returned For " & """" & TextBox1.Text & """" & vbCrLf & "Please edit the search Term and search again")
        End If
    End Sub

    Private Sub loadwebpage()
        'For f = 0 To 10000
        '    websource(f) = Nothing
        'Next
        ReDim websource(10000)
        urllinecount = 0



        Try
            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(url)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            urllinecount = 0

            Do While Not sLine Is Nothing
                urllinecount += 1
                sLine = objReader.ReadLine
                If Not sLine Is Nothing Then
                    websource(urllinecount) = sLine
                End If
            Loop
            objReader.Close()
            urllinecount -= 1

        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub





    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

        Dim counter As Integer = ListBox1.SelectedIndex + 1
        If returnedresults(counter, 2) <> Nothing Then
            PictureBox1.ImageLocation = returnedresults(counter, 2)
        Else
            PictureBox1.Image = Nothing
        End If


        Call checklanguage()





    End Sub
    Private Sub checklanguage()
        url = "http://thetvdb.com/api/6E82FED600783400/series/" & returnedresults(ListBox1.SelectedIndex + 1, 1) & "/" & languagecode & ".xml"
        Call loadwebpage()

        For f = 1 To urllinecount
            If websource(f).IndexOf("<Language>") <> -1 Then
                websource(f) = websource(f).Replace("<Language>", "")
                websource(f) = websource(f).Replace("</Language>", "")
                websource(f) = websource(f).Replace("  ", "")
                If websource(f).ToLower <> languagecode Then
                    Label5.Text = ListBox1.SelectedItem.ToString & " is not available in " & ListBox2.SelectedItem.ToString
                Else
                    Label5.Text = ListBox1.SelectedItem.ToString & " is available in " & ListBox2.SelectedItem.ToString
                End If
            End If
        Next
    End Sub



    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim count As Integer = ListBox2.SelectedIndex + 1
        languagecode = languages(count, 1)
        Call checklanguage()
    End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    If Label5.Text.IndexOf("not available") <> -1 Then
    '        MsgBox("This Show is not Available in your Selected Language" & vbCrLf & vbCrLf & "               Please Select Another Language")
    '        Exit Sub
    '    End If
    '    Form1.messbox.TextBox1.Text = "Please Wait"
    '    Form1.messbox.TextBox2.Text = ""
    '    Form1.messbox.TextBox3.Text = "Scraping TV Show"
    '    Form1.messbox.Refresh()
    '    Form1.messbox.Visible = True


    '    Dim path As String = Form1.TextBox16.Text
    '    Dim episodeguideurl As String = ""
    '    Dim title As String = ""
    '    Dim tvdbid As String = ""
    '    Dim imdbid As String = ""
    '    Dim cert As String = ""
    '    Dim premiered As String = ""
    '    Dim genre As String = ""
    '    Dim language As String = ""
    '    Dim rating As String = ""
    '    Dim runtime As String = ""
    '    Dim studio As String = ""
    '    Dim plot As String = ""
    '    Dim episodeactors As String = ""
    '    Dim tvshowactors As String = ""
    '    Dim sortorder As String = ""
    '    Dim fanarturl As String = ""
    '    Dim posterurl As String = ""
    '    Dim seasonurl(100) As String
    '    Dim artstyle As String = ""
    '    Dim episodeactorsource As String = ""


    '    If RadioButton7.Checked = True Then
    '        artstyle = "poster"
    '    Else
    '        artstyle = "banner"
    '    End If

    '    If RadioButton5.Checked = True Then
    '        episodeactorsource = "tvdb"
    '    Else
    '        episodeactorsource = "imdb"
    '    End If


    '    If RadioButton1.Checked = True Then
    '        sortorder = "default"
    '    Else
    '        sortorder = "dvd"
    '    End If

    '    If RadioButton3.Checked = True Then
    '        tvshowactors = "tvdb"
    '    Else
    '        tvshowactors = "imdb"
    '    End If
    '    If RadioButton5.Checked = True Then
    '        episodeactors = "tvdb"
    '    Else
    '        episodeactors = "imdb"
    '    End If

    '    url = "http://thetvdb.com/api/6E82FED600783400/series/" & returnedresults(ListBox1.SelectedIndex + 1, 1) & "/" & languagecode & ".xml"

    '    Dim wrGETURL As WebRequest
    '    wrGETURL = WebRequest.Create(url)
    '    Dim myProxy As New WebProxy("myproxy", 80)
    '    myProxy.BypassProxyOnLocal = True
    '    Dim objStream As Stream
    '    objStream = wrGETURL.GetResponse.GetResponseStream()
    '    Dim objReader As New StreamReader(objStream)
    '    Dim sLine As String = ""
    '    urllinecount = 0
    '    sLine = objReader.ReadToEnd
    '    objReader.Close()

    '    If sLine = "" Then
    '        MsgBox("Unable to Download Show Info")
    '        Exit Sub
    '    End If

    '    Dim matched() As String = {"<id>([^<]*)</id>", "<ContentRating>([^<]*)</ContentRating>", "<FirstAired>([^<]*)</FirstAired>", "<Genre>([^<]*)</Genre>", "<IMDB_ID>([^<]*)</IMDB_ID>", "<Language>([^<]*)</Language>", "<Network>([^<]*)</Network>", "<Overview>([^<]*)</Overview>", "<Rating>([^<]*)</Rating>", "<Runtime>([^<]*)</Runtime>", "<SeriesName>([^<]*)</SeriesName>"}



    '    Dim M2 As Match

    '    For f = 0 To UBound(matched)
    '        M2 = Regex.Match(sLine, matched(f))
    '        If M2.Success = True Then
    '            If matched(f).IndexOf("</SeriesName>") <> -1 Then
    '                title = M2.Groups(1).Value
    '                title = specchars(title)
    '                title = encodespecialchrs(title)
    '            ElseIf matched(f).IndexOf("</Rating>") <> -1 Then
    '                rating = M2.Groups(1).Value
    '                rating = specchars(rating)
    '                rating = encodespecialchrs(rating)
    '            ElseIf matched(f).IndexOf("</FirstAired>") <> -1 Then
    '                premiered = M2.Groups(1).Value
    '                premiered = specchars(premiered)
    '                premiered = encodespecialchrs(premiered)
    '            ElseIf matched(f).IndexOf("</Overview>") <> -1 Then
    '                plot = M2.Groups(1).Value
    '                plot = specchars(plot)
    '                plot = encodespecialchrs(plot)
    '            ElseIf matched(f).IndexOf("</Runtime>") <> -1 Then
    '                runtime = M2.Groups(1).Value
    '                runtime = specchars(runtime)
    '                runtime = encodespecialchrs(runtime)
    '            ElseIf matched(f).IndexOf("</Genre>") <> -1 Then
    '                genre = M2.Groups(1).Value
    '                genre = specchars(genre)
    '                genre = encodespecialchrs(genre)
    '                genre = genre.TrimStart("|")
    '                genre = genre.TrimEnd("|")
    '                genre = genre.Replace("|", "/")
    '            ElseIf matched(f).IndexOf("</Network>") <> -1 Then
    '                studio = M2.Groups(1).Value
    '                studio = specchars(studio)
    '                studio = encodespecialchrs(studio)
    '            ElseIf matched(f).IndexOf("</id>") <> -1 Then
    '                tvdbid = M2.Groups(1).Value
    '            ElseIf matched(f).IndexOf("</IMDB_ID>") <> -1 Then
    '                imdbid = M2.Groups(1).Value
    '            ElseIf matched(f).IndexOf("</ContentRating>") <> -1 Then
    '                cert = M2.Groups(1).Value
    '            ElseIf matched(f).IndexOf("</Language>") <> -1 Then
    '                language = M2.Groups(1).Value
    '            End If
    '        End If
    '    Next

    '    If imdbid <> "" Then
    '        If imdbid.IndexOf("tt") = -1 Then
    '            If IsNumeric(imdbid) Then
    '                If imdbid.Length = 7 Then
    '                    imdbid = "tt" & imdbid
    '                End If
    '            Else
    '                imdbid = ""
    '            End If
    '        End If
    '    End If
    '    episodeguideurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
    '    'http://thetvdb.com/api/6E82FED600783400/series/73255/all/en.zip
    '    If tvshowactors = "imdb" And imdbid <> "" Then
    '        Call gettvdbactors(tvdbid)
    '    End If

    '    If tvshowactors = "tvdb" Or imdbid = "" Or actorcount = 0 Then
    '        Call gettvdbactors(tvdbid)
    '    End If

    '    Dim tempstring As String
    '    tempstring = path & "tvshow.nfo"
    '    Dim file As IO.StreamWriter = IO.File.CreateText(tempstring)

    '    file.WriteLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
    '    file.WriteLine("<tvshow>")
    '    file.WriteLine("    <title>" & title & "</title>")
    '    file.WriteLine("    <rating>" & rating & "</rating>")
    '    file.WriteLine("    <season>-1</season>")
    '    file.WriteLine("    <episode>0</episode>")
    '    file.WriteLine("    <displayseason>-1</displayseason>")
    '    file.WriteLine("    <displayepisode>-1</displayepisode>")
    '    file.WriteLine("    <episodeguideurl>" & episodeguideurl & "</episodeguideurl>")
    '    file.WriteLine("    <plot>" & plot & "</plot>")
    '    file.WriteLine("    <runtime>" & runtime & "</runtime>")
    '    file.WriteLine("    <mpaa>" & cert & "</mpaa>")
    '    file.WriteLine("    <id>" & imdbid & "</id>")
    '    file.WriteLine("    <tvdbid>" & tvdbid & "</tvdbid>")
    '    file.WriteLine("    <genre>" & genre & "</genre>")
    '    file.WriteLine("    <premiered>" & premiered & "</premiered>")
    '    file.WriteLine("    <studio>" & studio & "</studio>")
    '    file.WriteLine("    <sortorder>" & sortorder & "</sortorder>")
    '    file.WriteLine("    <language>" & language & "</language>")
    '    file.WriteLine("    <episodeactorsource>" & episodeactorsource & "</episodeactorsource>")

    '    If actorcount > Form1.userprefs.maxactors Then actorcount = Form1.userprefs.maxactors

    '    For f = 1 To actorcount
    '        file.WriteLine("    <actor>")
    '        file.WriteLine("        <name>" & actors(f, 0) & "</name>")
    '        file.WriteLine("        <role>" & actors(f, 1) & "</role>")
    '        If actors(f, 2) <> Nothing Then file.WriteLine("        <thumb>" & actors(f, 2) & "</thumb>")
    '        file.WriteLine("    </actor>")
    '    Next

    '    file.WriteLine("</tvshow>")


    '    file.Close()

    '    If CheckBox1.CheckState = CheckState.Checked Or CheckBox2.CheckState = CheckState.Checked Then
    '        url = "http://thetvdb.com/api/6E82FED600783400/series/" & returnedresults(ListBox1.SelectedIndex + 1, 1) & "/" & languagecode & ".xml"


    '        wrGETURL = WebRequest.Create(url)
    '        myProxy.BypassProxyOnLocal = True
    '        objStream = wrGETURL.GetResponse.GetResponseStream()
    '        Dim objReader2 As New StreamReader(objStream)
    '        urllinecount = 0
    '        sLine = objReader2.ReadToEnd
    '        objReader2.Close()

    '        Dim match As String = ""
    '        If CheckBox1.CheckState = CheckState.Checked Then
    '            If RadioButton7.Checked = True Then
    '                'download poster
    '                match = "<poster>([^<]*)</poster>"
    '                M2 = Regex.Match(sLine, match)
    '                If M2.Success = True And M2.Groups(1).Value <> "" Then
    '                    match = "http://images.thetvdb.com/banners/" & M2.Groups(1).Value
    '                    Try
    '                        Dim buffer(4000000) As Byte
    '                        Dim size As Integer = 0
    '                        Dim bytesRead As Integer = 0
    '                        Dim req As HttpWebRequest = req.Create(match)
    '                        Dim res As HttpWebResponse = req.GetResponse()
    '                        Dim contents As Stream = res.GetResponseStream()
    '                        Dim bytesToRead As Integer = CInt(buffer.Length)
    '                        While bytesToRead > 0
    '                            size = contents.Read(buffer, bytesRead, bytesToRead)
    '                            If size = 0 Then Exit While
    '                            bytesToRead -= size
    '                            bytesRead += size
    '                        End While
    '                        Dim showthumbpath As String = path & "folder.jpg"
    '                        Dim fstrm As New FileStream(showthumbpath, FileMode.OpenOrCreate, FileAccess.Write)
    '                        fstrm.Write(buffer, 0, bytesRead)
    '                        contents.Close()
    '                        fstrm.Close()
    '                    Catch
    '                    End Try
    '                End If
    '            End If
    '            If RadioButton8.Checked = True Then
    '                'download banner
    '                match = "<banner>([^<]*)</banner>"
    '                M2 = Regex.Match(sLine, match)
    '                If M2.Success = True And M2.Groups(1).Value <> "" Then
    '                    match = "http://images.thetvdb.com/banners/" & M2.Groups(1).Value
    '                    Try
    '                        Dim buffer(4000000) As Byte
    '                        Dim size As Integer = 0
    '                        Dim bytesRead As Integer = 0
    '                        Dim req As HttpWebRequest = req.Create(match)
    '                        Dim res As HttpWebResponse = req.GetResponse()
    '                        Dim contents As Stream = res.GetResponseStream()
    '                        Dim bytesToRead As Integer = CInt(buffer.Length)
    '                        While bytesToRead > 0
    '                            size = contents.Read(buffer, bytesRead, bytesToRead)
    '                            If size = 0 Then Exit While
    '                            bytesToRead -= size
    '                            bytesRead += size
    '                        End While
    '                        Dim showthumbpath As String = path & "folder.jpg"
    '                        Dim fstrm As New FileStream(showthumbpath, FileMode.OpenOrCreate, FileAccess.Write)
    '                        fstrm.Write(buffer, 0, bytesRead)
    '                        contents.Close()
    '                        fstrm.Close()
    '                    Catch
    '                    End Try
    '                End If
    '            End If
    '        End If
    '        If CheckBox2.CheckState = CheckState.Checked Then
    '            match = "<fanart>([^<]*)</fanart>"
    '            M2 = Regex.Match(sLine, match)
    '            If M2.Success = True And M2.Groups(1).Value <> "" Then
    '                match = "http://images.thetvdb.com/banners/" & M2.Groups(1).Value
    '                Try
    '                    Dim buffer(4000000) As Byte
    '                    Dim size As Integer = 0
    '                    Dim bytesRead As Integer = 0
    '                    Dim req As HttpWebRequest = req.Create(match)
    '                    Dim res As HttpWebResponse = req.GetResponse()
    '                    Dim contents As Stream = res.GetResponseStream()
    '                    Dim bytesToRead As Integer = CInt(buffer.Length)
    '                    Dim bmp As New Bitmap(contents)
    '                    While bytesToRead > 0
    '                        size = contents.Read(buffer, bytesRead, bytesToRead)
    '                        If size = 0 Then Exit While
    '                        bytesToRead -= size
    '                        bytesRead += size
    '                    End While
    '                    Dim showthumbpath As String = path & "fanart.jpg"

    '                    If Form1.resizefanart = 1 Then
    '                        bmp.Save(showthumbpath, Imaging.ImageFormat.Jpeg)
    '                    ElseIf Form1.resizefanart = 2 Then
    '                        If bmp.Width > 1280 Or bmp.Height > 720 Then
    '                            Dim bm_source As New Bitmap(bmp)
    '                            Dim bm_dest As New Bitmap(1280, 720)
    '                            Dim gr As Graphics = Graphics.FromImage(bm_dest)
    '                            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
    '                            gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
    '                            bm_dest.Save(showthumbpath, Imaging.ImageFormat.Jpeg)
    '                        Else
    '                            bmp.Save(showthumbpath, Imaging.ImageFormat.Jpeg)
    '                        End If
    '                    ElseIf Form1.resizefanart = 3 Then
    '                        If bmp.Width > 960 Or bmp.Height > 540 Then
    '                            Dim bm_source As New Bitmap(bmp)
    '                            Dim bm_dest As New Bitmap(960, 540)
    '                            Dim gr As Graphics = Graphics.FromImage(bm_dest)
    '                            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
    '                            gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
    '                            bm_dest.Save(showthumbpath, Imaging.ImageFormat.Jpeg)
    '                        Else
    '                            bmp.Save(showthumbpath, Imaging.ImageFormat.Jpeg)
    '                        End If
    '                    End If
    '                Catch
    '                End Try
    '            End If
    '        End If
    '        If CheckBox3.CheckState = CheckState.Checked Then
    '            ReDim websource(10000)
    '            url = "http://thetvdb.com/api/6E82FED600783400/series/" & returnedresults(ListBox1.SelectedIndex + 1, 1) & "/banners.xml"
    '            wrGETURL = WebRequest.Create(url)
    '            myProxy.BypassProxyOnLocal = True
    '            objStream = wrGETURL.GetResponse.GetResponseStream()
    '            Dim objReader3 As New StreamReader(objStream)
    '            urllinecount = 0
    '            sLine = ""
    '            Do While Not sLine Is Nothing
    '                urllinecount += 1
    '                sLine = objReader3.ReadLine
    '                If Not sLine Is Nothing Then
    '                    websource(urllinecount) = sLine
    '                End If
    '            Loop
    '            urllinecount -= 1
    '            objReader3.Close()
    '            '<Season>" & series.ToString & "</Season>
    '            Dim done As Boolean = False
    '            For f = 0 To 50
    '                Dim preferredurl(1) As String
    '                preferredurl(0) = ""
    '                preferredurl(1) = ""
    '                For g = 2 To urllinecount
    '                    If websource(g).IndexOf("<Season>" & f.ToString & "</Season>") <> -1 And websource(g - 1).IndexOf("<Language>" & languagecode & "</Language>") <> -1 Then
    '                        Dim urlstring As String = websource(g - 4)
    '                        urlstring = urlstring.Replace("<BannerPath>", "")
    '                        urlstring = urlstring.Replace("</BannerPath>", "")
    '                        urlstring = urlstring.Replace("  ", "")
    '                        preferredurl(0) = "http://images.thetvdb.com/banners/" & urlstring
    '                    ElseIf websource(g).IndexOf("<Season>" & f.ToString & "</Season>") <> -1 And websource(g - 1).IndexOf("<Language>" & languagecode & "</Language>") = -1 Then
    '                        Dim urlstring As String = websource(g - 4)
    '                        urlstring = urlstring.Replace("<BannerPath>", "")
    '                        urlstring = urlstring.Replace("</BannerPath>", "")
    '                        urlstring = urlstring.Replace("  ", "")
    '                        preferredurl(1) = "http://images.thetvdb.com/banners/" & urlstring
    '                    End If
    '                    If preferredurl(0) <> "" Then
    '                        Dim urlstring As String = preferredurl(0)
    '                        Try
    '                            Dim buffer(4000000) As Byte
    '                            Dim size As Integer = 0
    '                            Dim bytesRead As Integer = 0
    '                            Dim req As HttpWebRequest = req.Create(urlstring)
    '                            Dim res As HttpWebResponse = req.GetResponse()
    '                            Dim contents As Stream = res.GetResponseStream()
    '                            Dim bytesToRead As Integer = CInt(buffer.Length)
    '                            While bytesToRead > 0
    '                                size = contents.Read(buffer, bytesRead, bytesToRead)
    '                                If size = 0 Then Exit While
    '                                bytesToRead -= size
    '                                bytesRead += size
    '                            End While
    '                            Dim showthumbpath As String = ""
    '                            If f < 10 Then
    '                                showthumbpath = path & "season0" & f.ToString & ".tbn"
    '                            Else
    '                                showthumbpath = path & "season" & f.ToString & ".tbn"
    '                            End If
    '                            Dim fstrm As New FileStream(showthumbpath, FileMode.OpenOrCreate, FileAccess.Write)
    '                            fstrm.Write(buffer, 0, bytesRead)
    '                            contents.Close()
    '                            fstrm.Close()
    '                        Catch
    '                        End Try
    '                        done = True
    '                        Exit For
    '                    End If
    '                Next
    '                If done = False And preferredurl(1) <> "" Then
    '                    Dim urlstring As String = preferredurl(1)
    '                    Try
    '                        Dim buffer(4000000) As Byte
    '                        Dim size As Integer = 0
    '                        Dim bytesRead As Integer = 0
    '                        Dim req As HttpWebRequest = req.Create(urlstring)
    '                        Dim res As HttpWebResponse = req.GetResponse()
    '                        Dim contents As Stream = res.GetResponseStream()
    '                        Dim bytesToRead As Integer = CInt(buffer.Length)
    '                        While bytesToRead > 0
    '                            size = contents.Read(buffer, bytesRead, bytesToRead)
    '                            If size = 0 Then Exit While
    '                            bytesToRead -= size
    '                            bytesRead += size
    '                        End While
    '                        Dim showthumbpath As String = ""
    '                        If f < 10 Then
    '                            showthumbpath = path & "season0" & f.ToString & ".tbn"
    '                        Else
    '                            showthumbpath = path & "season" & f.ToString & ".tbn"
    '                        End If
    '                        Dim fstrm As New FileStream(showthumbpath, FileMode.OpenOrCreate, FileAccess.Write)
    '                        fstrm.Write(buffer, 0, bytesRead)
    '                        contents.Close()
    '                        fstrm.Close()
    '                    Catch
    '                    End Try
    '                End If

    '            Next



    '        End If






    '    End If
    '    objReader.Close()


    '    For f = 1 To Form1.ammountoftvpaths
    '        If Form1.seriesname(f, 0) & "\" = path Then
    '            Form1.seriesname(f, 1) = title
    '            Form1.seriesname(f, 2) = tvdbid
    '            Form1.seriesname(f, 3) = imdbid
    '            Form1.seriesname(f, 5) = sortorder
    '            Form1.seriesname(f, 6) = languagecode
    '            Form1.seriesname(f, 7) = episodeactorsource
    '        End If
    '    Next

    '    System.Windows.Forms.Cursor.Current = Cursors.Default
    '    'newforem.Close()
    '    Form1.messbox.Visible = False





    '    Me.Close()


    'End Sub

    Private Sub getimdbactors(ByVal imdbid)

        Dim tempint As Integer
        Dim actorcode(200) As String
        Dim first As Integer
        Dim last As Integer
        actorcount = 0

        url = "http://www.imdb.com/title/" & imdbid
        Call loadwebpage()

        Dim tempstring As String = ""
        For f = 1 To urllinecount
            If websource(f).IndexOf("<h3>Cast</h3>") <> -1 Then
                If websource(f).IndexOf("<tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>") <> -1 Then
                    websource(f) = websource(f).Replace("</td></tr> <tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>", "</td></tr><tr class")
                End If
                tempint = 0
                tempstring = websource(f)
                Do Until tempstring.IndexOf("</td></tr><tr class") = -1
                    tempint = tempint + 1
                    actors(tempint, 0) = tempstring.Substring(0, tempstring.IndexOf("</td></tr><tr class") + 19)
                    tempstring = tempstring.Replace(actors(tempint, 0), "")
                    actors(tempint, 0) = specchars(actors(tempint, 0))
                Loop
                tempint = tempint + 1
                actors(tempint, 0) = tempstring
                actorcount = tempint
                For g = 1 To actorcount
                    actors(g, 3) = actors(g, 0).Substring(actors(g, 0).IndexOf("<a href=""/name/nm") + 15, 9)
                    If actors(g, 0).IndexOf("http://resume.imdb.com") <> -1 Then actors(g, 0) = actors(g, 0).Replace("http://resume.imdb.com", "")
                    If actors(g, 0).IndexOf("http://i.media-imdb.com/images/tn15/addtiny.gif") <> -1 Then actors(g, 0) = actors(g, 0).Replace("http://i.media-imdb.com/images/tn15/addtiny.gif", "")
                    If actors(g, 0).IndexOf("</td></tr></table>") <> -1 Then
                        tempint = actors(g, 0).IndexOf("</td></tr></table>")
                        tempstring = actors(g, 0).Substring(tempint, actors(g, 0).Length - tempint)
                        actors(g, 0) = actors(g, 0).Replace(tempstring, "</td></tr><tr class")
                    End If
                    If actors(g, 0).IndexOf("link=/name/") <> -1 And actors(g, 0).IndexOf("/';""><img") <> -1 Then
                        actors(g, 2) = actors(g, 0).Substring(actors(g, 0).IndexOf("link=/name/") + 11, actors(g, 0).IndexOf("/';""><img") - actors(g, 0).IndexOf("link=/name/") - 11)
                        actorcode(g) = actors(g, 2)
                        actors(g, 2) = "http://www.imdb.com/name/" & actors(g, 2)
                    End If
                    If actors(g, 0).IndexOf("a href=""/character") <> -1 Then
                        actors(g, 1) = actors(g, 0).Substring(actors(g, 0).IndexOf("a href=""/character") + 19, actors(g, 0).IndexOf("</td></tr><tr class") - actors(g, 0).IndexOf("a href=""/character") - 19)
                        If actors(g, 1).IndexOf("</a>") <> -1 Then
                            actors(g, 1) = actors(g, 1).Substring(12, actors(g, 1).IndexOf("</a>") - 12)
                        ElseIf actors(g, 1).IndexOf("</a>") = -1 Then
                            actors(g, 1) = actors(g, 1).Substring(12, actors(g, 1).Length - 12)
                        End If
                        tempstring = actors(g, 0).Substring(actors(g, 0).IndexOf("a href=""/character"), actors(g, 0).Length - actors(g, 0).IndexOf("a href=""/character"))
                        actors(g, 0) = actors(g, 0).Replace(tempstring, "")
                        first = actors(g, 0).IndexOf("/"">")
                        last = actors(g, 0).IndexOf("</a></td>")
                        actors(g, 0) = actors(g, 0).Substring(first + 3, (last) - (first + 3))
                    ElseIf actors(g, 0).IndexOf("a href=""/character") = -1 Then
                        first = actors(g, 0).IndexOf("<td class=""char"">")
                        last = actors(g, 0).IndexOf("</td></tr><tr class")
                        actors(g, 1) = actors(g, 0).Substring(first + 17, last - first - 17)
                        first = actors(g, 0).IndexOf("/"">")
                        last = actors(g, 0).IndexOf("</a></td>")
                        actors(g, 0) = actors(g, 0).Substring(first + 3, (last) - (first + 3))
                    End If
                Next
            End If
        Next
        For f = 1 To actorcount
            Application.DoEvents()
            If actors(f, 2) <> Nothing Then
                url = actors(f, 2)
                If url.IndexOf("http") <> -1 Then
                    Call loadwebpage()
                    For g = 1 To urllinecount
                        If websource(g).IndexOf("<div class=""photo"">") <> -1 Then
                            tempint = 0
                            tempstring = websource(g + tempint)
                            Do While tempstring.IndexOf("http") = -1
                                tempint += 1
                                tempstring = websource(g + tempint)
                            Loop
                            If tempstring.IndexOf("http") <> -1 And tempstring.IndexOf("jpg") <> -1 Then
                                actors(f, 2) = tempstring.Substring(tempstring.IndexOf("http"), tempstring.IndexOf("jpg") - tempstring.IndexOf("http") + 3)
                                Exit For
                                If Form1.userprefs.actorsave = True Then
                                    Dim workingpath As String = ""
                                    Dim networkpath As String = Form1.userprefs.actorsavepath
                                    If actors(f, 2) <> Nothing Then
                                        If actors(f, 2) <> "" Then
                                            Try
                                                workingpath = networkpath & "\" & actors(f, 3) & ".jpg"
                                                If Not IO.File.Exists(workingpath) Then
                                                    Dim buffer(4000000) As Byte
                                                    Dim size As Integer = 0
                                                    Dim bytesRead As Integer = 0
                                                    Dim thumburl As String = actors(f, 2)
                                                    Dim req As HttpWebRequest = req.Create(thumburl)
                                                    Dim res As HttpWebResponse = req.GetResponse()
                                                    Dim contents As Stream = res.GetResponseStream()
                                                    Dim bytesToRead As Integer = CInt(buffer.Length)
                                                    While bytesToRead > 0
                                                        size = contents.Read(buffer, bytesRead, bytesToRead)
                                                        If size = 0 Then Exit While
                                                        bytesToRead -= size
                                                        bytesRead += size
                                                    End While

                                                    Dim fstrm As New FileStream(workingpath, FileMode.OpenOrCreate, FileAccess.Write)
                                                    fstrm.Write(buffer, 0, bytesRead)
                                                    contents.Close()
                                                    fstrm.Close()
                                                End If
                                                actors(f, 2) = IO.Path.Combine(Form1.userprefs.actornetworkpath, actors(f, 3) & ".jpg")
                                            Catch
                                            End Try
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next
        If actorcount > 0 Then
            For f = 1 To actorcount
                actors(f, 0) = specchars(actors(f, 0))
                actors(f, 1) = specchars(actors(f, 1))
                actors(f, 0) = encodespecialchrs(actors(f, 0))
                actors(f, 1) = encodespecialchrs(actors(f, 1))
            Next
        End If



    End Sub





    Private Sub gettvdbactors(ByVal tvdbid)
        If IsNumeric(tvdbid) And tvdbid.Length = 4 Or tvdbid.Length = 5 Or tvdbid.Length = 6 Or tvdbid.Length = 7 Then
        Else
            MsgBox("A Valid TVDB ID is needed to Perform this action")
            Exit Sub
        End If
        actorcount = 0

        Application.DoEvents()

        url = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/actors.xml"
        Call loadwebpage()

        For f = 1 To urllinecount
            If websource(f).IndexOf("  <Image>") <> -1 Then
                actorcount = actorcount + 1
                websource(f) = websource(f).Replace("  <Image>", "")
                websource(f) = websource(f).Replace("</Image>", "")
                If websource(f).IndexOf("/") <> -1 And websource(f).IndexOf(".jpg") <> -1 Then
                    actors(actorcount, 3) = websource(f).Substring(websource(f).LastIndexOf("/") + 1, websource(f).LastIndexOf(".jpg") - websource(f).LastIndexOf("/") - 1)
                End If

                actors(actorcount, 2) = websource(f)
                If actors(actorcount, 2) <> "" Then actors(actorcount, 2) = "http://images.thetvdb.com/banners/" & actors(actorcount, 2)
                websource(f + 1) = websource(f + 1).Replace("  <Name>", "")
                websource(f + 1) = websource(f + 1).Replace("</Name>", "")
                actors(actorcount, 0) = websource(f + 1)
                websource(f + 2) = websource(f + 2).Replace("  <Role>", "")
                websource(f + 2) = websource(f + 2).Replace("</Role>", "")
                actors(actorcount, 1) = websource(f + 2)
            End If
        Next

        If actorcount > 0 Then
            For f = 1 To actorcount
                actors(f, 0) = specchars(actors(f, 0))
                actors(f, 1) = specchars(actors(f, 1))
                actors(f, 0) = encodespecialchrs(actors(f, 0))
                actors(f, 1) = encodespecialchrs(actors(f, 1))
                If Form1.userprefs.actorsave = True Then
                    Dim workingpath As String = ""
                    Dim networkpath As String = Form1.userprefs.actorsavepath
                    If actors(f, 2) <> Nothing Then
                        If actors(f, 2) <> "" Then
                            'Try
                            workingpath = IO.Path.Combine(networkpath, actors(f, 3))
                            workingpath += ".jpg"
                            If Not IO.File.Exists(workingpath) Then
                                Dim buffer(4000000) As Byte
                                Dim size As Integer = 0
                                Dim bytesRead As Integer = 0
                                Dim thumburl As String = actors(f, 2)
                                Dim req As HttpWebRequest = req.Create(thumburl)
                                Dim res As HttpWebResponse = req.GetResponse()
                                Dim contents As Stream = res.GetResponseStream()
                                Dim bytesToRead As Integer = CInt(buffer.Length)
                                While bytesToRead > 0
                                    size = contents.Read(buffer, bytesRead, bytesToRead)
                                    If size = 0 Then Exit While
                                    bytesToRead -= size
                                    bytesRead += size
                                End While

                                Dim fstrm As New FileStream(workingpath, FileMode.OpenOrCreate, FileAccess.Write)
                                fstrm.Write(buffer, 0, bytesRead)
                                contents.Close()
                                fstrm.Close()
                            End If
                            actors(f, 2) = IO.Path.Combine(Form1.userprefs.actornetworkpath, actors(f, 3) & ".jpg")
                            'Catch
                            'End Try
                        End If
                    End If
                End If
            Next
        End If
    End Sub





    Private Function encodespecialchrs(ByVal text As String)
        If text.IndexOf("&") <> -1 Then text = text.Replace("&", "&amp;")
        If text.IndexOf("<") <> -1 Then text = text.Replace("", "&lt;")
        If text.IndexOf(">") <> -1 Then text = text.Replace("", "&gt;")
        If text.IndexOf(Chr(34)) <> -1 Then text = text.Replace(Chr(34), "&quot;")
        If text.IndexOf("'") <> -1 Then text = text.Replace("'", "&apos;")
        Return text
    End Function

    Private Function specchars(ByVal filterstring As String)
        Try
            If filterstring.IndexOf("&#919;") <> -1 Then filterstring = filterstring.Replace("&#919;", "Η")
            If filterstring.IndexOf("&#918;") <> -1 Then filterstring = filterstring.Replace("&#918;", "Ζ")
            If filterstring.IndexOf("&#917;") <> -1 Then filterstring = filterstring.Replace("&#917;", "Ε")
            If filterstring.IndexOf("&#916;") <> -1 Then filterstring = filterstring.Replace("&#916;", "Δ")
            If filterstring.IndexOf("&#915;") <> -1 Then filterstring = filterstring.Replace("&#915;", "Γ")
            If filterstring.IndexOf("&#914;") <> -1 Then filterstring = filterstring.Replace("&#914;", "Β")
            If filterstring.IndexOf("&#913;") <> -1 Then filterstring = filterstring.Replace("&#913;", "Α")
            If filterstring.IndexOf("&#732;") <> -1 Then filterstring = filterstring.Replace("&#732;", "˜")
            If filterstring.IndexOf("&#710;") <> -1 Then filterstring = filterstring.Replace("&#710;", "ˆ")
            If filterstring.IndexOf("&#402;") <> -1 Then filterstring = filterstring.Replace("&#402;", "ƒ")
            If filterstring.IndexOf("&#376;") <> -1 Then filterstring = filterstring.Replace("&#376;", "Ÿ")
            If filterstring.IndexOf("&#353;") <> -1 Then filterstring = filterstring.Replace("&#353;", "š")
            If filterstring.IndexOf("&#352;") <> -1 Then filterstring = filterstring.Replace("&#352;", "Š")
            If filterstring.IndexOf("&#339;") <> -1 Then filterstring = filterstring.Replace("&#339;", "œ")
            If filterstring.IndexOf("&#338;") <> -1 Then filterstring = filterstring.Replace("&#338;", "Œ")
            If filterstring.IndexOf("&#937;") <> -1 Then filterstring = filterstring.Replace("&#937;", "Ω")
            If filterstring.IndexOf("&#936;") <> -1 Then filterstring = filterstring.Replace("&#936;", "Ψ")
            If filterstring.IndexOf("&#935;") <> -1 Then filterstring = filterstring.Replace("&#935;", "Χ")
            If filterstring.IndexOf("&#934;") <> -1 Then filterstring = filterstring.Replace("&#934;", "Φ")
            If filterstring.IndexOf("&#933;") <> -1 Then filterstring = filterstring.Replace("&#933;", "Υ")
            If filterstring.IndexOf("&#932;") <> -1 Then filterstring = filterstring.Replace("&#932;", "Τ")
            If filterstring.IndexOf("&#931;") <> -1 Then filterstring = filterstring.Replace("&#931;", "Σ")
            If filterstring.IndexOf("&#929;") <> -1 Then filterstring = filterstring.Replace("&#929;", "Ρ")
            If filterstring.IndexOf("&#928;") <> -1 Then filterstring = filterstring.Replace("&#928;", "Π")
            If filterstring.IndexOf("&#927;") <> -1 Then filterstring = filterstring.Replace("&#927;", "Ο")
            If filterstring.IndexOf("&#926;") <> -1 Then filterstring = filterstring.Replace("&#926;", "Ξ")
            If filterstring.IndexOf("&#924;") <> -1 Then filterstring = filterstring.Replace("&#924;", "Μ")
            If filterstring.IndexOf("&#923;") <> -1 Then filterstring = filterstring.Replace("&#923;", "Λ")
            If filterstring.IndexOf("&#922;") <> -1 Then filterstring = filterstring.Replace("&#922;", "Κ")
            If filterstring.IndexOf("&#921;") <> -1 Then filterstring = filterstring.Replace("&#921;", "Ι")
            If filterstring.IndexOf("&#920;") <> -1 Then filterstring = filterstring.Replace("&#920;", "Θ")
            If filterstring.IndexOf("&#955;") <> -1 Then filterstring = filterstring.Replace("&#955;", "λ")
            If filterstring.IndexOf("&#954;") <> -1 Then filterstring = filterstring.Replace("&#954;", "κ")
            If filterstring.IndexOf("&#953;") <> -1 Then filterstring = filterstring.Replace("&#953;", "ι")
            If filterstring.IndexOf("&#952;") <> -1 Then filterstring = filterstring.Replace("&#952;", "θ")
            If filterstring.IndexOf("&#951;") <> -1 Then filterstring = filterstring.Replace("&#951;", "η")
            If filterstring.IndexOf("&#950;") <> -1 Then filterstring = filterstring.Replace("&#950;", "ζ")
            If filterstring.IndexOf("&#949;") <> -1 Then filterstring = filterstring.Replace("&#949;", "ε")
            If filterstring.IndexOf("&#948;") <> -1 Then filterstring = filterstring.Replace("&#948;", "δ")
            If filterstring.IndexOf("&#947;") <> -1 Then filterstring = filterstring.Replace("&#947;", "γ")
            If filterstring.IndexOf("&#946;") <> -1 Then filterstring = filterstring.Replace("&#946;", "β")
            If filterstring.IndexOf("&#945;") <> -1 Then filterstring = filterstring.Replace("&#945;", "α")
            If filterstring.IndexOf("&#969;") <> -1 Then filterstring = filterstring.Replace("&#969;", "ω")
            If filterstring.IndexOf("&#968;") <> -1 Then filterstring = filterstring.Replace("&#968;", "ψ")
            If filterstring.IndexOf("&#967;") <> -1 Then filterstring = filterstring.Replace("&#967;", "χ")
            If filterstring.IndexOf("&#966;") <> -1 Then filterstring = filterstring.Replace("&#966;", "φ")
            If filterstring.IndexOf("&#965;") <> -1 Then filterstring = filterstring.Replace("&#965;", "υ")
            If filterstring.IndexOf("&#964;") <> -1 Then filterstring = filterstring.Replace("&#964;", "τ")
            If filterstring.IndexOf("&#963;") <> -1 Then filterstring = filterstring.Replace("&#963;", "σ")
            If filterstring.IndexOf("&#962;") <> -1 Then filterstring = filterstring.Replace("&#962;", "ς")
            If filterstring.IndexOf("&#961;") <> -1 Then filterstring = filterstring.Replace("&#961;", "ρ")
            If filterstring.IndexOf("&#960;") <> -1 Then filterstring = filterstring.Replace("&#960;", "π")
            If filterstring.IndexOf("&#959;") <> -1 Then filterstring = filterstring.Replace("&#959;", "ο")
            If filterstring.IndexOf("&#958;") <> -1 Then filterstring = filterstring.Replace("&#958;", "ξ")
            If filterstring.IndexOf("&#957;") <> -1 Then filterstring = filterstring.Replace("&#957;", "ν")
            If filterstring.IndexOf("&#956;") <> -1 Then filterstring = filterstring.Replace("&#956;", "μ")
            If filterstring.IndexOf("&#8240;") <> -1 Then filterstring = filterstring.Replace("&#8240;", "‰")
            If filterstring.IndexOf("&#8230;") <> -1 Then filterstring = filterstring.Replace("&#8230;", "…")
            If filterstring.IndexOf("&#8226;") <> -1 Then filterstring = filterstring.Replace("&#8226;", "•")
            If filterstring.IndexOf("&#8225;") <> -1 Then filterstring = filterstring.Replace("&#8225;", "‡")
            If filterstring.IndexOf("&#8224;") <> -1 Then filterstring = filterstring.Replace("&#8224;", "†")
            If filterstring.IndexOf("&#8222;") <> -1 Then filterstring = filterstring.Replace("&#8222;", "„")
            If filterstring.IndexOf("&#8218;") <> -1 Then filterstring = filterstring.Replace("&#8218;", "‚")
            If filterstring.IndexOf("&#8217;") <> -1 Then filterstring = filterstring.Replace("&#8217;", "’")
            If filterstring.IndexOf("&#8216;") <> -1 Then filterstring = filterstring.Replace("&#8216;", "‘")
            If filterstring.IndexOf("&#8212;") <> -1 Then filterstring = filterstring.Replace("&#8212;", "—")
            If filterstring.IndexOf("&#8211;") <> -1 Then filterstring = filterstring.Replace("&#8211;", "–")
            If filterstring.IndexOf("&#8805;") <> -1 Then filterstring = filterstring.Replace("&#8805;", "≥")
            If filterstring.IndexOf("&#8804;") <> -1 Then filterstring = filterstring.Replace("&#8804;", "≤")
            If filterstring.IndexOf("&#8801;") <> -1 Then filterstring = filterstring.Replace("&#8801;", "≡")
            If filterstring.IndexOf("&#8800;") <> -1 Then filterstring = filterstring.Replace("&#8800;", "≠")
            If filterstring.IndexOf("&#8776;") <> -1 Then filterstring = filterstring.Replace("&#8776;", "≈")
            If filterstring.IndexOf("&#8747;") <> -1 Then filterstring = filterstring.Replace("&#8747;", "∫")
            If filterstring.IndexOf("&#8745;") <> -1 Then filterstring = filterstring.Replace("&#8745;", "∩")
            If filterstring.IndexOf("&#8734;") <> -1 Then filterstring = filterstring.Replace("&#8734;", "∞")
            If filterstring.IndexOf("&#8730;") <> -1 Then filterstring = filterstring.Replace("&#8730;", "√")
            If filterstring.IndexOf("&#8721;") <> -1 Then filterstring = filterstring.Replace("&#8721;", "∑")
            If filterstring.IndexOf("&#8719;") <> -1 Then filterstring = filterstring.Replace("&#8719;", "∏")
            If filterstring.IndexOf("&#8706;") <> -1 Then filterstring = filterstring.Replace("&#8706;", "∂")
            If filterstring.IndexOf("&#8596;") <> -1 Then filterstring = filterstring.Replace("&#8596;", "↔")
            If filterstring.IndexOf("&#8595;") <> -1 Then filterstring = filterstring.Replace("&#8595;", "↓")
            If filterstring.IndexOf("&#8594;") <> -1 Then filterstring = filterstring.Replace("&#8594;", "→")
            If filterstring.IndexOf("&#8593;") <> -1 Then filterstring = filterstring.Replace("&#8593;", "↑")
            If filterstring.IndexOf("&#8592;") <> -1 Then filterstring = filterstring.Replace("&#8592;", "←")
            If filterstring.IndexOf("&#8482;") <> -1 Then filterstring = filterstring.Replace("&#8482;", "™")
            If filterstring.IndexOf("&#8364;") <> -1 Then filterstring = filterstring.Replace("&#8364;", "€")
            If filterstring.IndexOf("&#8260;") <> -1 Then filterstring = filterstring.Replace("&#8260;", "⁄")
            If filterstring.IndexOf("&#8254;") <> -1 Then filterstring = filterstring.Replace("&#8254;", "‾")
            If filterstring.IndexOf("&#8250;") <> -1 Then filterstring = filterstring.Replace("&#8250;", "›")
            If filterstring.IndexOf("&#8249;") <> -1 Then filterstring = filterstring.Replace("&#8249;", "‹")
            If filterstring.IndexOf("&#161;") <> -1 Then filterstring = filterstring.Replace("&#161;", Chr(161))
            If filterstring.IndexOf("&#162;") <> -1 Then filterstring = filterstring.Replace("&#162;", Chr(162))
            If filterstring.IndexOf("&#163;") <> -1 Then filterstring = filterstring.Replace("&#163;", Chr(163))
            If filterstring.IndexOf("&#164;") <> -1 Then filterstring = filterstring.Replace("&#164;", Chr(164))
            If filterstring.IndexOf("&#165;") <> -1 Then filterstring = filterstring.Replace("&#165;", Chr(165))
            If filterstring.IndexOf("&#166;") <> -1 Then filterstring = filterstring.Replace("&#166;", Chr(166))
            If filterstring.IndexOf("&#167;") <> -1 Then filterstring = filterstring.Replace("&#167;", Chr(167))
            If filterstring.IndexOf("&#168;") <> -1 Then filterstring = filterstring.Replace("&#168;", Chr(168))
            If filterstring.IndexOf("&#170;") <> -1 Then filterstring = filterstring.Replace("&#170;", Chr(170))
            If filterstring.IndexOf("&#171;") <> -1 Then filterstring = filterstring.Replace("&#171;", Chr(171))
            If filterstring.IndexOf("&#172;") <> -1 Then filterstring = filterstring.Replace("&#172;", Chr(172))
            If filterstring.IndexOf("&#173;") <> -1 Then filterstring = filterstring.Replace("&#173;", Chr(173))
            If filterstring.IndexOf("&#174;") <> -1 Then filterstring = filterstring.Replace("&#174;", Chr(174))
            If filterstring.IndexOf("&#175;") <> -1 Then filterstring = filterstring.Replace("&#175;", Chr(175))
            If filterstring.IndexOf("&#176;") <> -1 Then filterstring = filterstring.Replace("&#176;", Chr(176))
            If filterstring.IndexOf("&#177;") <> -1 Then filterstring = filterstring.Replace("&#177;", Chr(177))
            If filterstring.IndexOf("&#178;") <> -1 Then filterstring = filterstring.Replace("&#178;", Chr(178))
            If filterstring.IndexOf("&#179;") <> -1 Then filterstring = filterstring.Replace("&#179;", Chr(179))
            If filterstring.IndexOf("&#198;") <> -1 Then filterstring = filterstring.Replace("&#198;", Chr(198))
            If filterstring.IndexOf("&amp;") <> -1 Then filterstring = filterstring.Replace("&amp;", "&")
            If filterstring.IndexOf("&quot;") <> -1 Then filterstring = filterstring.Replace("&quot;", Chr(34))
            If filterstring.IndexOf("&lt;") <> -1 Then filterstring = filterstring.Replace("&lt;", "<")
            If filterstring.IndexOf("&gt;") <> -1 Then filterstring = filterstring.Replace("&gt;", ">")
            If filterstring.IndexOf("&#138;") <> -1 Then filterstring = filterstring.Replace("&#138;", Chr(138))
            If filterstring.IndexOf("&#140;") <> -1 Then filterstring = filterstring.Replace("&#140;", Chr(140))
            If filterstring.IndexOf("&#142;") <> -1 Then filterstring = filterstring.Replace("&#142;", Chr(142))
            If filterstring.IndexOf("&#153;") <> -1 Then filterstring = filterstring.Replace("&#153;", Chr(153))
            If filterstring.IndexOf("&#154;") <> -1 Then filterstring = filterstring.Replace("&#154;", Chr(154))
            If filterstring.IndexOf("&#156;") <> -1 Then filterstring = filterstring.Replace("&#156;", Chr(156))
            If filterstring.IndexOf("&#158;") <> -1 Then filterstring = filterstring.Replace("&#158;", Chr(158))
            If filterstring.IndexOf("&#159;") <> -1 Then filterstring = filterstring.Replace("&#159;", Chr(159))
            If filterstring.IndexOf("&#160;") <> -1 Then filterstring = filterstring.Replace("&#160;", Chr(160))
            If filterstring.IndexOf("&#169;") <> -1 Then filterstring = filterstring.Replace("&#169;", Chr(169))
            If filterstring.IndexOf("&#178;") <> -1 Then filterstring = filterstring.Replace("&#178;", Chr(178))
            If filterstring.IndexOf("&#179;") <> -1 Then filterstring = filterstring.Replace("&#179;", Chr(179))
            If filterstring.IndexOf("&#181;") <> -1 Then filterstring = filterstring.Replace("&#181;", Chr(181))
            If filterstring.IndexOf("&#183;") <> -1 Then filterstring = filterstring.Replace("&#183;", Chr(183))
            If filterstring.IndexOf("&#188;") <> -1 Then filterstring = filterstring.Replace("&#188;", Chr(188))
            If filterstring.IndexOf("&#189;") <> -1 Then filterstring = filterstring.Replace("&#189;", Chr(189))
            If filterstring.IndexOf("&#190;") <> -1 Then filterstring = filterstring.Replace("&#190;", Chr(190))
            If filterstring.IndexOf("&#192;") <> -1 Then filterstring = filterstring.Replace("&#192;", Chr(192))
            If filterstring.IndexOf("&#193;") <> -1 Then filterstring = filterstring.Replace("&#193;", Chr(193))
            If filterstring.IndexOf("&#194;") <> -1 Then filterstring = filterstring.Replace("&#194;", Chr(194))
            If filterstring.IndexOf("&#195;") <> -1 Then filterstring = filterstring.Replace("&#195;", Chr(195))
            If filterstring.IndexOf("&#196;") <> -1 Then filterstring = filterstring.Replace("&#196;", Chr(196))
            If filterstring.IndexOf("&#197;") <> -1 Then filterstring = filterstring.Replace("&#197;", Chr(197))
            If filterstring.IndexOf("&#199;") <> -1 Then filterstring = filterstring.Replace("&#199;", Chr(199))
            If filterstring.IndexOf("&#200;") <> -1 Then filterstring = filterstring.Replace("&#200;", Chr(200))
            If filterstring.IndexOf("&#201;") <> -1 Then filterstring = filterstring.Replace("&#201;", Chr(201))
            If filterstring.IndexOf("&#203;") <> -1 Then filterstring = filterstring.Replace("&#203;", Chr(203))
            If filterstring.IndexOf("&#204;") <> -1 Then filterstring = filterstring.Replace("&#204;", Chr(204))
            If filterstring.IndexOf("&#205;") <> -1 Then filterstring = filterstring.Replace("&#205;", Chr(205))
            If filterstring.IndexOf("&#206;") <> -1 Then filterstring = filterstring.Replace("&#206;", Chr(206))
            If filterstring.IndexOf("&#207;") <> -1 Then filterstring = filterstring.Replace("&#207;", Chr(207))
            If filterstring.IndexOf("&#208;") <> -1 Then filterstring = filterstring.Replace("&#208;", Chr(208))
            If filterstring.IndexOf("&#209;") <> -1 Then filterstring = filterstring.Replace("&#209;", Chr(209))
            If filterstring.IndexOf("&#210;") <> -1 Then filterstring = filterstring.Replace("&#210;", Chr(210))
            If filterstring.IndexOf("&#211;") <> -1 Then filterstring = filterstring.Replace("&#211;", Chr(211))
            If filterstring.IndexOf("&#212;") <> -1 Then filterstring = filterstring.Replace("&#212;", Chr(212))
            If filterstring.IndexOf("&#213;") <> -1 Then filterstring = filterstring.Replace("&#213;", Chr(213))
            If filterstring.IndexOf("&#214;") <> -1 Then filterstring = filterstring.Replace("&#214;", Chr(214))
            If filterstring.IndexOf("&#215;") <> -1 Then filterstring = filterstring.Replace("&#215;", Chr(215))
            If filterstring.IndexOf("&#216;") <> -1 Then filterstring = filterstring.Replace("&#216;", Chr(216))
            If filterstring.IndexOf("&#217;") <> -1 Then filterstring = filterstring.Replace("&#217;", Chr(217))
            If filterstring.IndexOf("&#218;") <> -1 Then filterstring = filterstring.Replace("&#218;", Chr(218))
            If filterstring.IndexOf("&#219;") <> -1 Then filterstring = filterstring.Replace("&#219;", Chr(219))
            If filterstring.IndexOf("&#220;") <> -1 Then filterstring = filterstring.Replace("&#220;", Chr(220))
            If filterstring.IndexOf("&#221;") <> -1 Then filterstring = filterstring.Replace("&#221;", Chr(221))
            If filterstring.IndexOf("&#222;") <> -1 Then filterstring = filterstring.Replace("&#222;", Chr(222))
            If filterstring.IndexOf("&#223;") <> -1 Then filterstring = filterstring.Replace("&#223;", Chr(223))
            If filterstring.IndexOf("&#224;") <> -1 Then filterstring = filterstring.Replace("&#224;", Chr(224))
            If filterstring.IndexOf("&#225;") <> -1 Then filterstring = filterstring.Replace("&#225;", Chr(225))
            If filterstring.IndexOf("&#226;") <> -1 Then filterstring = filterstring.Replace("&#226;", Chr(226))
            If filterstring.IndexOf("&#227;") <> -1 Then filterstring = filterstring.Replace("&#227;", Chr(227))
            If filterstring.IndexOf("&#228;") <> -1 Then filterstring = filterstring.Replace("&#228;", Chr(228))
            If filterstring.IndexOf("&#229;") <> -1 Then filterstring = filterstring.Replace("&#229;", Chr(229))
            If filterstring.IndexOf("&#230;") <> -1 Then filterstring = filterstring.Replace("&#230;", Chr(230))
            If filterstring.IndexOf("&#231;") <> -1 Then filterstring = filterstring.Replace("&#231;", Chr(231))
            If filterstring.IndexOf("&#232;") <> -1 Then filterstring = filterstring.Replace("&#232;", Chr(232))
            If filterstring.IndexOf("&#233;") <> -1 Then filterstring = filterstring.Replace("&#233;", Chr(233))
            If filterstring.IndexOf("&#234;") <> -1 Then filterstring = filterstring.Replace("&#234;", Chr(234))
            If filterstring.IndexOf("&#235;") <> -1 Then filterstring = filterstring.Replace("&#235;", Chr(235))
            If filterstring.IndexOf("&#236;") <> -1 Then filterstring = filterstring.Replace("&#236;", Chr(236))
            If filterstring.IndexOf("&#237;") <> -1 Then filterstring = filterstring.Replace("&#237;", Chr(237))
            If filterstring.IndexOf("&#238;") <> -1 Then filterstring = filterstring.Replace("&#238;", Chr(238))
            If filterstring.IndexOf("&#239;") <> -1 Then filterstring = filterstring.Replace("&#239;", Chr(239))
            If filterstring.IndexOf("&#240;") <> -1 Then filterstring = filterstring.Replace("&#240;", Chr(240))
            If filterstring.IndexOf("&#241;") <> -1 Then filterstring = filterstring.Replace("&#241;", Chr(241))
            If filterstring.IndexOf("&#242;") <> -1 Then filterstring = filterstring.Replace("&#242;", Chr(242))
            If filterstring.IndexOf("&#243;") <> -1 Then filterstring = filterstring.Replace("&#243;", Chr(243))
            If filterstring.IndexOf("&#244;") <> -1 Then filterstring = filterstring.Replace("&#244;", Chr(244))
            If filterstring.IndexOf("&#245;") <> -1 Then filterstring = filterstring.Replace("&#245;", Chr(245))
            If filterstring.IndexOf("&#246;") <> -1 Then filterstring = filterstring.Replace("&#246;", Chr(246))
            If filterstring.IndexOf("&#247;") <> -1 Then filterstring = filterstring.Replace("&#247;", Chr(247))
            If filterstring.IndexOf("&#248;") <> -1 Then filterstring = filterstring.Replace("&#248;", Chr(248))
            If filterstring.IndexOf("&#249;") <> -1 Then filterstring = filterstring.Replace("&#249;", Chr(249))
            If filterstring.IndexOf("&#250;") <> -1 Then filterstring = filterstring.Replace("&#250;", Chr(250))
            If filterstring.IndexOf("&#251;") <> -1 Then filterstring = filterstring.Replace("&#251;", Chr(251))
            If filterstring.IndexOf("&#252;") <> -1 Then filterstring = filterstring.Replace("&#252;", Chr(252))
            If filterstring.IndexOf("&#253;") <> -1 Then filterstring = filterstring.Replace("&#253;", Chr(253))
            If filterstring.IndexOf("&#254;") <> -1 Then filterstring = filterstring.Replace("&#254;", Chr(254))
            If filterstring.IndexOf("&#255;") <> -1 Then filterstring = filterstring.Replace("&#255;", Chr(255))
            If filterstring.IndexOf("&#38;") <> -1 Then filterstring = filterstring.Replace("&#38;", Chr(38))
            If filterstring.IndexOf("&#34;") <> -1 Then filterstring = filterstring.Replace("&#34;", Chr(34))
            Return filterstring
        Catch
        End Try
    End Function ' Replace special IMDB chrs with ascii



End Class