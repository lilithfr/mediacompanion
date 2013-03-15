Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Linq

Public Class Form2
    Const SetDefaults = True
    Dim editsmade As Boolean = False
    Dim thumbeditsmade As Boolean = False
    'Dim actorlist As Integer = Nothing
    'Dim actors(1000, 3)
    'Dim actorcount As Integer

    Dim oldactors(9999, 2)
    Dim actorcount As Integer = 0
    Dim workingmovieedit As New FullMovieDetails
    Dim posterpath As String = ""
    Dim cropstring As String




    Private Sub Form2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Call checkforedits()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub setupdisplay()
        actorcb.Items.Clear()
        If workingmovieedit.fullmoviebody.title <> Nothing Then titletxt.Text = workingmovieedit.fullmoviebody.title
        If workingmovieedit.fullmoviebody.director <> Nothing Then directortxt.Text = workingmovieedit.fullmoviebody.director
        If workingmovieedit.fullmoviebody.stars <> Nothing Then starstxt.Text = workingmovieedit.fullmoviebody.stars
        If workingmovieedit.fullmoviebody.runtime <> Nothing Then runtimetxt.Text = workingmovieedit.fullmoviebody.runtime
        If workingmovieedit.fullmoviebody.credits <> Nothing Then creditstxt.Text = workingmovieedit.fullmoviebody.credits
        If workingmovieedit.fullmoviebody.mpaa <> Nothing Then mpaatxt.Text = workingmovieedit.fullmoviebody.mpaa
        If workingmovieedit.fullmoviebody.studio <> Nothing Then studiotxt.Text = workingmovieedit.fullmoviebody.studio
        If workingmovieedit.fullmoviebody.genre <> Nothing Then genretxt.Text = workingmovieedit.fullmoviebody.genre
        If workingmovieedit.fullmoviebody.year <> Nothing Then yeartxt.Text = workingmovieedit.fullmoviebody.year
        If workingmovieedit.fullmoviebody.rating <> Nothing Then ratingtxt.Text = workingmovieedit.fullmoviebody.rating
        If workingmovieedit.fullmoviebody.imdbid <> Nothing Then idtxt.Text = workingmovieedit.fullmoviebody.imdbid
        If workingmovieedit.fullmoviebody.votes <> Nothing Then votestxt.Text = workingmovieedit.fullmoviebody.votes
        If workingmovieedit.fullmoviebody.outline <> Nothing Then outlinetxt.Text = workingmovieedit.fullmoviebody.outline
        If workingmovieedit.fullmoviebody.plot <> Nothing Then plottxt.Text = workingmovieedit.fullmoviebody.plot
        If workingmovieedit.fullmoviebody.tagline <> Nothing Then taglinetxt.Text = workingmovieedit.fullmoviebody.tagline
        For Each actor In workingmovieedit.listactors
            actorcb.Items.Add(actor.actorname)
        Next

        Try
            actorcb.SelectedItem = workingmovieedit.listactors(0).actorname
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try

        If actorcb.Text <> "" Then
            roletxt.Text = workingmovieedit.listactors(0).actorrole
        End If
        posterpath = Form1.workingMovieDetails.fileinfo.posterpath
        If posterpath <> Nothing Then
            If posterpath <> "" Then
                If posterpath.ToLower.IndexOf(".jpg") <> -1 Or posterpath.ToLower.IndexOf(".tbn") <> -1 Then
                    moviethumb.ImageLocation = posterpath
                Else
                    moviethumb.Image = Nothing
                End If
            Else
                moviethumb.Image = Nothing
            End If
        Else
            moviethumb.Image = Nothing
        End If

    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            workingmovieedit = Form1.workingMovieDetails
            For Each actor In workingmovieedit.listactors
                If actor.actorname <> Nothing Then
                    actorcount += 1
                    oldactors(actorcount, 0) = actor.actorname
                    If actor.actorname <> Nothing Then
                        oldactors(actorcount, 1) = actor.actorrole
                    End If
                    If actor.actorthumb <> Nothing Then
                        oldactors(actorcount, 2) = actor.actorthumb
                    End If
                End If
            Next
            Panel2.Dock = DockStyle.Fill
            Call setupdisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub checkforedits()



    End Sub


    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub actorcb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles actorcb.SelectedIndexChanged
        Try
            roletxt.Text = workingmovieedit.listactors(actorcb.SelectedIndex).actorrole
            If workingmovieedit.listactors(actorcb.SelectedIndex).actorthumb <> Nothing Then
                Try
                    PictureBox1.ImageLocation = Preferences.GetActorThumbPath(workingmovieedit.listactors(actorcb.SelectedIndex).actorthumb)
                Catch ex As Exception
                    PictureBox1.Image = Nothing
                End Try
            Else
                PictureBox1.Image = Nothing
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btneditactor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditactor.Click
        Try

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Edit Actor Button
    Private Sub btndeleteactor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeleteactor.Click
        Try
            If actorcb.Items.Count <> 0 Then
                Dim tempint As Integer
                tempint = actorcb.SelectedIndex
                workingmovieedit.listactors.RemoveAt(actorcb.SelectedIndex)
                actorcb.Items.RemoveAt(actorcb.SelectedIndex)
                If workingmovieedit.listactors.Count <= tempint Then
                    tempint = workingmovieedit.listactors.Count - 1
                End If
                If workingmovieedit.listactors.Count <> 0 Then
                    actorcb.SelectedIndex = tempint
                Else
                    actorcb.Text = ""
                    roletxt.Text = ""
                End If
            Else
                MsgBox("No Actors to delete")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Delete Actor Button
    Private Sub btnaddactor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddactor.Click
        Try

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Add Actor Button


    Private Sub btnrescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrescrape.Click
        Try

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub



    Private Sub btnchangemovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangemovie.Click
        Try
            Dim tempstring As String
            Dim url As String
            If Preferences.usefoldernames = True Then
                tempstring = Form1.workingMovie.foldername
            Else
                tempstring = Utilities.CleanFileName(IO.Path.GetFileName(Form1.workingMovieDetails.fileinfo.fullpathandfilename))
            End If

            tempstring = tempstring.Replace(" ", "+")
            tempstring = tempstring.Replace("&", "%26")


            url = Preferences.imdbmirror & "find?s=tt&q=" & tempstring
            WebBrowser2.Stop()
            WebBrowser2.ScriptErrorsSuppressed = True
            WebBrowser2.Navigate(url)
            WebBrowser2.Refresh()
            Panel2.Visible = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Try
            Dim tempstring As String
            Dim thumbstring As New XmlDocument
            WebBrowser2.ScriptErrorsSuppressed = True
            tempstring = WebBrowser2.Url.ToString
            tempstring = tempstring.Replace(Preferences.imdbmirror & "title/", "")
            tempstring = tempstring.Replace("/", "")
            If tempstring.IndexOf("tt") <> -1 And tempstring.Length = 9 Then
                Dim messbox As frmMessageBox = New frmMessageBox("Please wait, Scraping Alternative Title", "", "This Will Take Longer If Scraping Fanart & Poster")
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                messbox.Show()
                Me.Refresh()
                messbox.Refresh()
                Application.DoEvents()
                titletxt.Text = Nothing
                directortxt.Text = Nothing
                creditstxt.Text = Nothing
                studiotxt.Text = Nothing
                outlinetxt.Text = Nothing
                plottxt.Text = Nothing
                taglinetxt.Text = Nothing
                runtimetxt.Text = Nothing
                mpaatxt.Text = Nothing
                genretxt.Text = Nothing
                yeartxt.Text = Nothing
                idtxt.Text = Nothing
                ratingtxt.Text = Nothing
                votestxt.Text = Nothing
                idtxt.Text = tempstring
                Panel2.Visible = False
                '            Dim scraperfunction As New imdb.Classimdbscraper ' add to comment this one because of changes i made to the Class "Scraper" (ClassimdbScraper)
                Dim scraperfunction As New Classimdb
                Dim body As String = ""
                Dim certificates As New List(Of String)
                body = scraperfunction.getimdbbody("", "", tempstring, Preferences.imdbmirror)
                Dim alternatemovie As New FullMovieDetails
                alternatemovie.fullmoviebody.imdbid = tempstring
                If body = "MIC" Then
                    alternatemovie.fullmoviebody.genre = "problem"
                Else
                    thumbstring.LoadXml(body)
                    For Each thisresult In thumbstring("movie")
                        Select Case thisresult.Name
                            Case "title"
                                If Preferences.keepfoldername = False Then
                                    alternatemovie.fullmoviebody.title = thisresult.InnerText
                                Else
                                    If Preferences.usefoldernames = False Then
                                        Dim tempstring2 As String = IO.Path.GetFileName(Form1.workingMovieDetails.fileinfo.fullpathandfilename)
                                        alternatemovie.fullmoviebody.title = Utilities.CleanFileName(tempstring2)
                                    Else
                                        alternatemovie.fullmoviebody.title = Utilities.CleanFileName(Form1.workingMovieDetails.fileinfo.foldername)
                                    End If
                                End If
                                If Preferences.keepfoldername = False Then
                                    alternatemovie.fullmoviebody.title = thisresult.InnerText
                                Else
                                    alternatemovie.fullmoviebody.title = Form1.workingMovieDetails.fileinfo.foldername
                                End If
                            Case "credits"
                                alternatemovie.fullmoviebody.credits = thisresult.InnerText
                            Case "director"
                                alternatemovie.fullmoviebody.director = thisresult.InnerText
                            Case "stars"
                                alternatemovie.fullmoviebody.stars = thisresult.InnerText
                            Case "genre"
                                alternatemovie.fullmoviebody.genre = thisresult.InnerText
                            Case "mpaa"
                                alternatemovie.fullmoviebody.mpaa = thisresult.InnerText
                            Case "outline"
                                alternatemovie.fullmoviebody.outline = thisresult.InnerText
                            Case "plot"
                                alternatemovie.fullmoviebody.plot = thisresult.InnerText
                            Case "premiered"
                                alternatemovie.fullmoviebody.premiered = thisresult.InnerText
                            Case "rating"
                                alternatemovie.fullmoviebody.rating = thisresult.InnerText
                            Case "runtime"
                                alternatemovie.fullmoviebody.runtime = thisresult.InnerText
                            Case "studio"
                                alternatemovie.fullmoviebody.studio = thisresult.InnerText
                            Case "tagline"
                                alternatemovie.fullmoviebody.tagline = thisresult.InnerText
                            Case "top250"
                                alternatemovie.fullmoviebody.top250 = thisresult.InnerText
                            Case "votes"
                                alternatemovie.fullmoviebody.votes = thisresult.InnerText
                            Case "year"
                                alternatemovie.fullmoviebody.year = thisresult.InnerText
                            Case "id"
                                alternatemovie.fullmoviebody.imdbid = thisresult.InnerText
                            Case "cert"
                                certificates.Add(thisresult.InnerText)
                        End Select
                    Next
                    If alternatemovie.fullmoviebody.playcount = Nothing Then alternatemovie.fullmoviebody.playcount = "0"
                    If alternatemovie.fullmoviebody.top250 = Nothing Then alternatemovie.fullmoviebody.top250 = "0"

                    Dim done As Boolean = False
                    For g = 0 To UBound(Preferences.certificatepriority)
                        For Each cert In certificates
                            If cert.IndexOf(Preferences.certificatepriority(g)) <> -1 Then
                                alternatemovie.fullmoviebody.mpaa = cert.Substring(cert.IndexOf("|") + 1, cert.Length - cert.IndexOf("|") - 1)
                                done = True
                                Exit For
                            End If
                        Next
                        If done = True Then Exit For
                    Next

                    Dim actorlist As String

                    actorlist = scraperfunction.getimdbactors(Preferences.imdbmirror, tempstring, "")
                    If actorlist <> Nothing Then
                        thumbstring.LoadXml(actorlist)

                        For Each thisresult In thumbstring("actorlist")
                            Select Case thisresult.Name
                                Case "actor"
                                    Dim newactor As New str_MovieActors(SetDefaults)
                                    Dim detail As XmlNode = Nothing
                                    For Each detail In thisresult.ChildNodes
                                        Select Case detail.Name
                                            Case "name"
                                                newactor.actorname = detail.InnerText
                                            Case "role"
                                                newactor.actorrole = detail.InnerText
                                            Case "thumb"
                                                newactor.actorthumb = detail.InnerText
                                            Case "actorid"
                                                If newactor.actorthumb <> Nothing Then
                                                    If Preferences.actorsave = True And detail.InnerText <> "" Then
                                                        Dim workingpath As String = ""
                                                        Dim networkpath As String = Preferences.actorsavepath
                                                        Try
                                                            Dim tempstring2 As String = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                                            Dim hg As New IO.DirectoryInfo(tempstring2)
                                                            If Not hg.Exists Then
                                                                IO.Directory.CreateDirectory(tempstring2)
                                                            End If
                                                            workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                            If Not IO.File.Exists(workingpath) Then
                                                                Utilities.DownloadFile(newactor.actorthumb, workingpath)
                                                                'Dim buffer(4000000) As Byte
                                                                'Dim size As Integer = 0
                                                                'Dim bytesRead As Integer = 0
                                                                'Dim thumburl As String = newactor.actorthumb
                                                                'Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                                                'Dim res As HttpWebResponse = req.GetResponse()
                                                                'Dim contents As Stream = res.GetResponseStream()
                                                                'Dim bytesToRead As Integer = CInt(buffer.Length)
                                                                'While bytesToRead > 0
                                                                '    size = contents.Read(buffer, bytesRead, bytesToRead)
                                                                '    If size = 0 Then Exit While
                                                                '    bytesToRead -= size
                                                                '    bytesRead += size
                                                                'End While

                                                                'Dim fstrm As New FileStream(workingpath, FileMode.OpenOrCreate, FileAccess.Write)
                                                                'fstrm.Write(buffer, 0, bytesRead)
                                                                'contents.Close()
                                                                'fstrm.Close()
                                                            End If
                                                            newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                            If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                                newactor.actorthumb = Preferences.actornetworkpath & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                            Else
                                                                newactor.actorthumb = Preferences.actornetworkpath & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                            End If
                                                        Catch
                                                        End Try
                                                    End If
                                                End If
                                        End Select
                                    Next
                                    alternatemovie.listactors.Add(newactor)
                            End Select
                        Next
                        While alternatemovie.listactors.Count > Preferences.maxactors
                            alternatemovie.listactors.RemoveAt(alternatemovie.listactors.Count - 1)
                        End While
                    End If




                    Dim trailer As String = String.Empty
                    If Preferences.gettrailer = True Then

                        trailer = ""

                        If Preferences.moviePreferredTrailerResolution.ToUpper() <> "SD" then
                            trailer = MC_Scraper_Get_HD_Trailer_URL( Preferences.moviePreferredTrailerResolution, alternatemovie.fullmoviebody.title )
                        End If

                        If trailer = "" then
                            trailer = scraperfunction.gettrailerurl(alternatemovie.fullmoviebody.imdbid, Preferences.imdbmirror)
                        End if

                        If trailer <> String.Empty And trailer <> "Error" Then
                            alternatemovie.fullmoviebody.trailer = trailer
                        End If
                    End If

                    Dim tmdb As New TMDb(alternatemovie.fullmoviebody.imdbid)

                    If Preferences.nfoposterscraper <> 0 Then
                        Dim thumbs As String = ""

                        If Preferences.nfoposterscraper = 1 Or Preferences.nfoposterscraper = 3 Or Preferences.nfoposterscraper = 5 Or Preferences.nfoposterscraper = 7 Or Preferences.nfoposterscraper = 9 Or Preferences.nfoposterscraper = 11 Or Preferences.nfoposterscraper = 13 Or Preferences.nfoposterscraper = 15 Then
                            Try
                                Dim newobject3 As New IMPA.getimpaposters
                                Dim teststring As New XmlDocument
                                Dim testthumbs As String = newobject3.getimpathumbs(alternatemovie.fullmoviebody.title, alternatemovie.fullmoviebody.year)
                                Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                                teststring.LoadXml(testthumbs2)
                                thumbs = thumbs & testthumbs
                            Catch ex As Exception

                            End Try
                        End If


                        'If Preferences.nfoposterscraper = 2 Or Preferences.nfoposterscraper = 3 Or Preferences.nfoposterscraper = 6 Or Preferences.nfoposterscraper = 7 Or Preferences.nfoposterscraper = 10 Or Preferences.nfoposterscraper = 11 Or Preferences.nfoposterscraper = 14 Or Preferences.nfoposterscraper = 15 Then
                        '    Try
                        '        Dim newobject2 As New tmdb_posters.Class1
                        '        Dim teststring As New XmlDocument
                        '        Dim testthumbs As String = newobject2.gettmdbposters(alternatemovie.fullmoviebody.imdbid)
                        '        Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                        '        teststring.LoadXml(testthumbs2)
                        '        thumbs = thumbs & testthumbs
                        '    Catch ex As Exception

                        '    End Try
                        'End If

                        If {2,3,6,7,10,11,14,15}.Contains(Preferences.nfoposterscraper)
                            alternatemovie.listthumbs.AddRange(tmdb.Thumbs)
                        End If

                        If Preferences.nfoposterscraper = 4 Or Preferences.nfoposterscraper = 5 Or Preferences.nfoposterscraper = 6 Or Preferences.nfoposterscraper = 7 Or Preferences.nfoposterscraper = 12 Or Preferences.nfoposterscraper = 13 Or Preferences.nfoposterscraper = 14 Or Preferences.nfoposterscraper = 15 Then
                            Try
                                Dim newobject As New class_mpdb_thumbs.Class1
                                Dim teststring As New XmlDocument
                                Dim testthumbs As String = newobject.get_mpdb_thumbs(alternatemovie.fullmoviebody.imdbid)
                                Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                                teststring.LoadXml(testthumbs2)
                                thumbs = thumbs & testthumbs
                            Catch ex As Exception

                            End Try
                        End If

                        If Preferences.nfoposterscraper = 8 Or Preferences.nfoposterscraper = 9 Or Preferences.nfoposterscraper = 10 Or Preferences.nfoposterscraper = 11 Or Preferences.nfoposterscraper = 12 Or Preferences.nfoposterscraper = 13 Or Preferences.nfoposterscraper = 14 Or Preferences.nfoposterscraper = 15 Then
                            Try
                                Dim thumbscraper As New imdb_thumbs.Class1
                                Dim teststring As New XmlDocument
                                Dim testthumbs As String = thumbscraper.getimdbthumbs(alternatemovie.fullmoviebody.title, alternatemovie.fullmoviebody.year, alternatemovie.fullmoviebody.imdbid)
                                Dim testthumbs2 As String = "<totalthumbs>" & testthumbs & "</totalthumbs>"
                                teststring.LoadXml(testthumbs2)
                                thumbs = thumbs & testthumbs
                            Catch ex As Exception

                            End Try
                        End If




                        thumbs = "<thumblist>" & thumbs & "</thumblist>"

                        Try
                            thumbstring.LoadXml(thumbs)



                            For Each thisresult In thumbstring("thumblist")
                                Select Case thisresult.Name
                                    Case "thumb"
                                        alternatemovie.listthumbs.Add(thisresult.InnerText)
                                End Select
                            Next
                        Catch
                        End Try
                    End If



                    If Preferences.enablehdtags = True Then
                        If Form1.workingMovieDetails.filedetails.filedetails_video.Container = Nothing Then
                            alternatemovie.filedetails = Preferences.Get_HdTags(tempstring)
                        End If
                    End If


                    If CheckBox1.CheckState = CheckState.Checked Then
                        Dim posterpath As String = Form1.workingMovieDetails.fileinfo.posterpath
                        Dim jpegpath As String = posterpath.Replace(System.IO.Path.GetFileName(posterpath), "folder.jpg")
                        If IO.File.Exists(jpegpath) Then IO.File.Delete(jpegpath)

                        If posterpath <> Nothing Then
                            If posterpath <> "" Then
                                If IO.File.Exists(posterpath) Then IO.File.Delete(posterpath)
                                Dim moviethumburl As String = ""
                                If Preferences.scrapemovieposters = True Then
                                    Try
                                        Select Case Preferences.moviethumbpriority(0)
                                            Case "Internet Movie Poster Awards"
                                                moviethumburl = Form1.scraperFunction2.impathumb(alternatemovie.fullmoviebody.title, alternatemovie.fullmoviebody.year)
                                            Case "IMDB"
                                                moviethumburl = Form1.scraperFunction2.imdbthumb(alternatemovie.fullmoviebody.imdbid)
                                            Case "Movie Poster DB"
                                                moviethumburl = Form1.scraperFunction2.mpdbthumb(alternatemovie.fullmoviebody.imdbid)
                                            Case "themoviedb.org"
                                                'moviethumburl = Form1.scraperFunction2.tmdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                moviethumburl = tmdb.FirstOriginalPosterUrl
                                        End Select
                                    Catch
                                        moviethumburl = "na"
                                    End Try
                                    Try
                                        If moviethumburl = "na" Then
                                            Select Case Preferences.moviethumbpriority(1)
                                                Case "Internet Movie Poster Awards"
                                                    moviethumburl = Form1.scraperFunction2.impathumb(alternatemovie.fullmoviebody.title, alternatemovie.fullmoviebody.year)
                                                Case "IMDB"
                                                    moviethumburl = Form1.scraperFunction2.imdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                Case "Movie Poster DB"
                                                    moviethumburl = Form1.scraperFunction2.mpdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                Case "themoviedb.org"
                                                    'moviethumburl = Form1.scraperFunction2.tmdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                    moviethumburl = tmdb.FirstOriginalPosterUrl
                                            End Select
                                        End If
                                    Catch
                                        moviethumburl = "na"
                                    End Try
                                    Try
                                        If moviethumburl = "na" Then
                                            Select Case Preferences.moviethumbpriority(2)
                                                Case "Internet Movie Poster Awards"
                                                    moviethumburl = Form1.scraperFunction2.impathumb(alternatemovie.fullmoviebody.title, alternatemovie.fullmoviebody.year)
                                                Case "IMDB"
                                                    moviethumburl = Form1.scraperFunction2.imdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                Case "Movie Poster DB"
                                                    moviethumburl = Form1.scraperFunction2.mpdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                Case "themoviedb.org"
                                                    'moviethumburl = Form1.scraperFunction2.tmdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                    moviethumburl = tmdb.FirstOriginalPosterUrl
                                            End Select
                                        End If
                                    Catch
                                        moviethumburl = "na"
                                    End Try
                                    Try
                                        If moviethumburl = "na" Then
                                            Select Case Preferences.moviethumbpriority(3)
                                                Case "Internet Movie Poster Awards"
                                                    moviethumburl = Form1.scraperFunction2.impathumb(alternatemovie.fullmoviebody.title, alternatemovie.fullmoviebody.year)
                                                Case "IMDB"
                                                    moviethumburl = Form1.scraperFunction2.imdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                Case "Movie Poster DB"
                                                    moviethumburl = Form1.scraperFunction2.mpdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                Case "themoviedb.org"
                                                    'moviethumburl = Form1.scraperFunction2.tmdbthumb(alternatemovie.fullmoviebody.imdbid)
                                                    moviethumburl = tmdb.FirstOriginalPosterUrl
                                            End Select
                                        End If
                                    Catch
                                        moviethumburl = "na"
                                    End Try
                                    Try
                                        If moviethumburl <> "" And moviethumburl <> "na" Then
                                            Utilities.DownloadFile(moviethumburl, posterpath)
                                            If Preferences.createfolderjpg = True Then
                                                Utilities.DownloadFile(moviethumburl, jpegpath)
                                            End If
                                            'Try
                                            '    Dim buffer(4000000) As Byte
                                            '    Dim size As Integer = 0
                                            '    Dim bytesRead As Integer = 0
                                            '    Dim thumburl As String = moviethumburl
                                            '    Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                            '    Dim res As HttpWebResponse = req.GetResponse()
                                            '    Dim contents As Stream = res.GetResponseStream()
                                            '    Dim bytesToRead As Integer = CInt(buffer.Length)
                                            '    While bytesToRead > 0
                                            '        size = contents.Read(buffer, bytesRead, bytesToRead)
                                            '        If size = 0 Then Exit While
                                            '        bytesToRead -= size
                                            '        bytesRead += size
                                            '    End While

                                            '    Dim fstrm As New FileStream(posterpath, FileMode.OpenOrCreate, FileAccess.Write)
                                            '    fstrm.Write(buffer, 0, bytesRead)
                                            '    contents.Close()
                                            '    fstrm.Close()

                                            '    If Preferences.createfolderjpg = True Then
                                            '        Dim fstrm2 As New FileStream(jpegpath, FileMode.OpenOrCreate, FileAccess.Write)
                                            '        fstrm2.Write(buffer, 0, bytesRead)
                                            '        contents.Close()
                                            '        fstrm2.Close()
                                            '    End If
                                            'Catch ex As Exception
                                            'End Try
                                        End If
                                    Catch
                                    End Try
                                End If
                            End If
                        End If

                        Dim FanartPath As String = Form1.workingMovieDetails.fileinfo.fanartpath

                        If FanartPath <> Nothing Then
                            If FanartPath <> "" Then

                                Dim FanartUrl As String=tmdb.GetBackDropUrl()

                                If Not IsNothing(FanartUrl) then
                                   'Utilities.DownloadImage(FanartUrl, FanartPath, True, Preferences.resizefanart)
                                    Movie.SaveFanartImageToCacheAndPath(FanartUrl, FanartPath)
                                End If
                            End If
                        End If
                    End If

                    workingmovieedit = alternatemovie
                    Call setupdisplay()

                End If
                messbox.Close()


            Else
                MsgBox("Please Browse to the webpage of a movie first")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub ' Manual IMDB lookup go button

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Try
            WebBrowser2.GoBack()
            If WebBrowser2.CanGoBack = True Then
                Button13.Enabled = True
            Else
                Button13.Enabled = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Manual IMDB Search Back Button

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Try
            Panel2.Visible = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Cancel Manual IMDB search

    Private Sub WebBrowser2_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser2.NewWindow
        Try
            e.Cancel = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' stop popups in webbrowser from IMDB page



    Private Sub btnrescrapethumbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrescrapethumbs.Click
        Try
            'Form1.fullimdbid = idtxt.Text
            'tempstring = Form1.filefunction.getstackname(filenametxt.Text, pathtxt.Text)
            'If tempstring = "na" Then
            '    tempstring = pathtxt.Text & filenametxt.Text
            '    tempstring = tempstring.Replace(IO.Path.GetExtension(tempstring), ".tbn")
            'Else
            '    tempstring = pathtxt.Text & tempstring & ".tbn"
            'End If
            'Form1.fullposterpath = tempstring



            'Dim t As New coverart
            't.ShowDialog()
            'moviethumb.Image = Form1.moviethumb.Image
            'If Not moviethumb.Image Is Nothing Then
            '    Dim exists As Boolean = False
            '    Label16.Text = moviethumb.Image.Width
            '    Label17.Text = moviethumb.Image.Height
            '    Dim lngSizeOfFile As Decimal
            '    tempstring = Form1.moviethumbpath
            '    Try
            '        lngSizeOfFile = FileLen(Form1.fullposterpath)
            '        lngSizeOfFile = lngSizeOfFile / 1024
            '        lngSizeOfFile = lngSizeOfFile.Round(lngSizeOfFile, 2)
            '        Label18.Text = lngSizeOfFile & "kB"
            '    Catch
            '        Label18.Text = ""
            '    End Try
            'Else
            '    Label16.Text = "na"
            '    Label17.Text = "na"
            '    Label18.Text = "na"
            'End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub



    '______________________________________
    '_____Navigate Availabe Thumbnails_____
    '______________________________________

    'Private Sub btnthumbnavigateright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If thumbcount > thumbnumber Then
    '        thumbnumber = thumbnumber + 1
    '        If thumbs(thumbnumber).IndexOf("http") <> -1 And thumbs(thumbnumber).IndexOf("jpg") <> -1 Then
    '            Dim MyWebClient As New System.Net.WebClient
    '            Try
    '                Dim ImageInBytes() As Byte = MyWebClient.DownloadData(thumbs(thumbnumber))
    '                Dim ImageStream As New IO.MemoryStream(ImageInBytes)
    '                moviethumb.Image = New System.Drawing.Bitmap(ImageStream)
    '                Label7.Text = thumbnumber & " of " & thumbcount
    '                Label16.Text = moviethumb.Image.Width
    '                Label17.Text = moviethumb.Image.Height
    '                tempint = UBound(ImageInBytes)
    '                Dim newtemp As Decimal
    '                newtemp = tempint
    '                newtemp = newtemp / 1024
    '                newtemp = newtemp.Round(newtemp, 2)
    '                Label18.Text = newtemp & "kB"
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    End If
    'End Sub
    'Private Sub btnthumbnavigateleft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If thumbnumber > 1 Then
    '        thumbnumber = thumbnumber - 1
    '        If thumbs(thumbnumber).IndexOf("http") <> -1 And thumbs(thumbnumber).IndexOf("jpg") <> -1 Then
    '            Dim MyWebClient As New System.Net.WebClient
    '            Try
    '                Dim ImageInBytes() As Byte = MyWebClient.DownloadData(thumbs(thumbnumber))
    '                Dim ImageStream As New IO.MemoryStream(ImageInBytes)
    '                moviethumb.Image = New System.Drawing.Bitmap(ImageStream)

    '                Label7.Text = thumbnumber & " of " & thumbcount
    '                Label16.Text = moviethumb.Image.Width
    '                Label17.Text = moviethumb.Image.Height
    '                tempint = UBound(ImageInBytes)
    '                Dim newtemp As Decimal
    '                newtemp = tempint
    '                newtemp = newtemp / 1024
    '                newtemp = newtemp.Round(newtemp, 2)
    '                Label18.Text = newtemp & "kB"
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    End If
    'End Sub


    '_______________________________________________________
    '_________________Crop Thumbnail Code___________________
    '_______________________________________________________

    Private Function CropImage(ByVal SrcBmp As Bitmap, ByVal NewSize As Size, ByVal StartPoint As Point) As Bitmap
        Dim SrcRect As New Rectangle(StartPoint.X, StartPoint.Y, NewSize.Width, NewSize.Height)
        Dim DestRect As New Rectangle(0, 0, NewSize.Width, NewSize.Height)
        Dim DestBmp As New Bitmap(NewSize.Width, NewSize.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(DestBmp)
        g.DrawImage(SrcBmp, DestRect, SrcRect, GraphicsUnit.Pixel)
        Return DestBmp
    End Function ' Crop Image Function

    ' Below sets part of image to crop, top, bottom, left, right
    Private Sub croptop()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth, imageheight - 1), New Point(0, 1)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    Private Sub cropbottom()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth, imageheight - 1), New Point(0, 0)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    Private Sub cropleft()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth - 1, imageheight), New Point(1, 0)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    Private Sub cropright()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        thumbeditsmade = True
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth - 1, imageheight), New Point(0, 0)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    'crop while button is down, and stop when button is up
    Private Sub btncroptop_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncroptop.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            thumbeditsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call croptop()
            cropstring = "top"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncroptop_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncroptop.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropbottom_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropbottom.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            thumbeditsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call cropbottom()
            cropstring = "bottom"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropbottom_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropbottom.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropleft_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropleft.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            thumbeditsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call cropleft()
            cropstring = "left"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropleft_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropleft.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropright_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropright.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            editsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call cropright()
            cropstring = "right"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropright_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropright.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If cropstring = "top" Then Call croptop()
            If cropstring = "bottom" Then Call cropbottom()
            If cropstring = "left" Then Call cropleft()
            If cropstring = "right" Then Call cropright()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Set auto repeat for crop



    Private Sub btnsavechanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavechanges.Click
        Try
            'workingmovieedit.listactors.Clear()
            'Form1.workingmoviedetails.listactors.Clear()

            'For f = 1 To actorcount
            '    If oldactors(f, 0) <> Nothing Then
            '        Dim newactor As New movieactors
            '        newactor.actorname = oldactors(f, 0)
            '        If oldactors(f, 1) <> Nothing Then
            '            newactor.actorrole = oldactors(f, 1)
            '        End If
            '        If oldactors(f, 2) <> Nothing Then
            '            newactor.actorthumb = oldactors(f, 2)
            '        End If
            '        workingmovieedit.listactors.Add(newactor)
            '    End If
            'Next

            workingmovieedit.fullmoviebody.plot = plottxt.Text
            workingmovieedit.fullmoviebody.title = titletxt.Text
            workingmovieedit.fullmoviebody.director = directortxt.Text
            workingmovieedit.fullmoviebody.stars = starstxt.Text
            workingmovieedit.fullmoviebody.runtime = runtimetxt.Text
            workingmovieedit.fullmoviebody.credits = creditstxt.Text
            workingmovieedit.fullmoviebody.mpaa = mpaatxt.Text
            workingmovieedit.fullmoviebody.studio = studiotxt.Text
            workingmovieedit.fullmoviebody.genre = genretxt.Text
            workingmovieedit.fullmoviebody.year = yeartxt.Text
            workingmovieedit.fullmoviebody.rating = ratingtxt.Text
            workingmovieedit.fullmoviebody.votes = votestxt.Text
            workingmovieedit.fullmoviebody.outline = outlinetxt.Text
            workingmovieedit.fullmoviebody.tagline = taglinetxt.Text
            Form1.workingMovieDetails.fullmoviebody = workingmovieedit.fullmoviebody
            Form1.workingMovieDetails.listactors = workingmovieedit.listactors
            Form1.workingMovieDetails.listthumbs = workingmovieedit.listthumbs
            Call Form1.nfoFunction.mov_NfoSave(Form1.workingMovieDetails.fileinfo.fullpathandfilename, Form1.workingMovieDetails)
            Me.Close()
            'Dim oldactors(9999, 2)
            'Dim actorcount As Integer = 0
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub saved()

    End Sub

    Private Sub moviethumb_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles moviethumb.LoadCompleted
        Try
            ' Label13.Text = moviethumb.Image.Width & " X " & moviethumb.Image.Height
            Dim exists As Boolean = False
            Label16.Text = moviethumb.Image.Width
            Label17.Text = moviethumb.Image.Height
            exists = System.IO.File.Exists(Form1.workingMovieDetails.fileinfo.posterpath)
            If exists = True Then
                Dim lngSizeOfFile As Decimal
                lngSizeOfFile = FileLen(Form1.workingMovieDetails.fileinfo.posterpath)
                lngSizeOfFile = lngSizeOfFile / 1024
                lngSizeOfFile = Decimal.Round(lngSizeOfFile, 2)
                Label18.Text = lngSizeOfFile & "kB"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub zoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles zoom.Click
        Try
            If moviethumb.Height = 213 Then
                moviethumb.Location = New Point(371, 12)
                moviethumb.Height = 518
                moviethumb.Width = 362
            ElseIf moviethumb.Height = 518 Then
                moviethumb.Height = 213
                moviethumb.Width = 163
                moviethumb.Location = New Point(570, 12)
            End If
            Application.DoEvents()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnfanart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfanart.Click
        Try
            Dim t As New frmMovieFanart
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub btnresetimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresetimage.Click
        Try
            thumbeditsmade = False
            moviethumb.Image = Form1.moviethumb.Image
            btnresetimage.Enabled = False
            btnSaveCropped.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnsavecropped_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavecropped.Click
        Try
            thumbeditsmade = False
            Dim tempstring As String
            tempstring = Form1.workingMovieDetails.fileinfo.posterpath
            Dim stream As New System.IO.MemoryStream
            moviethumb.Image.Save(tempstring, System.Drawing.Imaging.ImageFormat.Jpeg)
            Form1.moviethumb.Image = moviethumb.Image
            btnresetimage.Enabled = False
            btnSaveCropped.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            titletxt.Text = ""
            directortxt.Text = ""
            creditstxt.Text = ""
            studiotxt.Text = ""
            yeartxt.Text = ""
            outlinetxt.Text = ""
            plottxt.Text = ""
            taglinetxt.Text = ""
            runtimetxt.Text = ""
            mpaatxt.Text = ""
            genretxt.Text = ""
            ratingtxt.Text = ""
            votestxt.Text = ""
            idtxt.Text = ""
            workingmovieedit.listactors.Clear()
            workingmovieedit.listthumbs.Clear()
            workingmovieedit.filedetails.filedetails_audio.Clear()
            workingmovieedit.filedetails.filedetails_subtitles.Clear()
            workingmovieedit.filedetails.filedetails_video.Bitrate = Nothing
            workingmovieedit.filedetails.filedetails_video.BitrateMax = Nothing
            workingmovieedit.filedetails.filedetails_video.BitrateMode = Nothing
            workingmovieedit.filedetails.filedetails_video.Codec = Nothing
            workingmovieedit.filedetails.filedetails_video.CodecId = Nothing
            workingmovieedit.filedetails.filedetails_video.CodecInfo = Nothing
            workingmovieedit.filedetails.filedetails_video.Container = Nothing
            workingmovieedit.filedetails.filedetails_video.DurationInSeconds.Value = Nothing
            workingmovieedit.filedetails.filedetails_video.FormatInfo = Nothing
            workingmovieedit.filedetails.filedetails_video.Height = Nothing
            workingmovieedit.filedetails.filedetails_video.ScanType = Nothing
            workingmovieedit.filedetails.filedetails_video.Width = Nothing
            workingmovieedit.fullmoviebody.title = Nothing
            workingmovieedit.fullmoviebody.director = Nothing
            workingmovieedit.fullmoviebody.stars = Nothing
            workingmovieedit.fullmoviebody.credits = Nothing
            workingmovieedit.fullmoviebody.studio = Nothing
            workingmovieedit.fullmoviebody.outline = Nothing
            workingmovieedit.fullmoviebody.plot = Nothing
            workingmovieedit.fullmoviebody.tagline = Nothing
            workingmovieedit.fullmoviebody.runtime = Nothing
            workingmovieedit.fullmoviebody.mpaa = Nothing
            workingmovieedit.fullmoviebody.genre = Nothing
            workingmovieedit.fullmoviebody.year = Nothing
            workingmovieedit.fullmoviebody.year = Nothing
            workingmovieedit.fullmoviebody.rating = Nothing
            workingmovieedit.fullmoviebody.votes = Nothing
            workingmovieedit.fullmoviebody.imdbid = Nothing
            actorcb.Text = ""
            roletxt.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            workingmovieedit = Nothing
            actorcb.Text = ""
            actorcb.Items.Clear()
            roletxt.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            titletxt.Text = ""
            directortxt.Text = ""
            creditstxt.Text = ""
            studiotxt.Text = ""
            yeartxt.Text = ""
            outlinetxt.Text = ""
            plottxt.Text = ""
            taglinetxt.Text = ""
            runtimetxt.Text = ""
            mpaatxt.Text = ""
            genretxt.Text = ""
            ratingtxt.Text = ""
            votestxt.Text = ""
            idtxt.Text = ""
            workingmovieedit = Form1.workingMovieDetails
            Dim newworkingmovieedit As FullMovieDetails = Form1.workingMovieDetails
            newworkingmovieedit.listactors.Clear()
            For f = 1 To actorcount
                Dim actor As New str_MovieActors(SetDefaults)
                If oldactors(f, 0) <> Nothing Then
                    actor.actorname = oldactors(f, 0)
                    If oldactors(f, 1) <> Nothing Then
                        actor.actorrole = oldactors(f, 1)
                    End If
                    If oldactors(f, 2) <> Nothing Then
                        actor.actorthumb = oldactors(f, 2)
                    End If
                    newworkingmovieedit.listactors.Add(actor)
                End If
            Next
            Call setupdisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

End Class