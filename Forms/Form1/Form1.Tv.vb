Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
Imports Media_Companion.ScraperFunctions

Imports System.Xml
Imports System.Reflection
Imports System.Windows.Forms
Imports System.ComponentModel

Partial Public Class Form1
    Public TvShows As New List(Of TvShow)
    Public workingTvShow As New TvShow
    Public workingEpisode As New List(Of TvEpisode)
    Public tempWorkingTvShow As New TvShow
    Public tempWorkingEpisode As New TvEpisode
    Dim newEpisodeList As New List(Of TvEpisode)
    Dim languageList As New List(Of str_TvShowLanguages)
    Dim listOfShows As New List(Of str_PossibleShowList)

    Dim tvdbposterlist As New List(Of TvBanners)
    Dim imdbposterlist As New List(Of TvBanners)
    Dim tvdbmode As Boolean = False
    Dim usedlist As New List(Of TvBanners)

    Private Sub loadshowlist()
        If TextBox26.Text <> "" Then

            messbox = New frmMessageBox("Please wait,", "", "Getting possible TV Shows from TVDB")
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            messbox.Show()
            Me.Refresh()
            messbox.Refresh()

            'Dim tvdbstuff As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
            Dim tvdbstuff As New TVDBScraper
            Dim mirror As List(Of String) = tvdbstuff.getmirrors()
            Dim showslist As String = tvdbstuff.findshows(TextBox26.Text, mirror(0))

            listOfShows.Clear()
            If showslist = "error" Then
                messbox.Close()
                MsgBox("TVDB seems to have an error with the xml for this search")
                Exit Sub
            End If
            If showslist <> "none" Then
                Dim showlist As New XmlDocument
                showlist.LoadXml(showslist)

                For Each thisresult As XmlNode In showlist("allshows")
                    Select Case thisresult.Name
                        Case "show"
                            Dim results As XmlNode = Nothing
                            Dim lan As New str_PossibleShowList(SetDefaults)
                            For Each results In thisresult.ChildNodes
                                Select Case results.Name
                                    Case "showid"
                                        lan.showid = results.InnerText
                                    Case "showtitle"
                                        lan.showtitle = results.InnerText
                                    Case "showbanner"
                                        lan.showbanner = results.InnerText
                                End Select
                            Next
                            Dim exists As Boolean = False
                            For Each item In listOfShows
                                If item.showid = lan.showid Then
                                    exists = True
                                    Exit For
                                End If
                            Next
                            If exists = False Then
                                listOfShows.Add(lan)
                            End If
                    End Select
                Next
            Else
                Dim lan As New str_PossibleShowList(SetDefaults)
                lan.showid = "none"
                lan.showtitle = "TVDB Search Returned Zero Results"
                lan.showbanner = Nothing
                listOfShows.Add(lan)

                lan.showid = "none"
                lan.showtitle = "Adjust the TV Shows Title & search again"
                lan.showbanner = Nothing
                listOfShows.Add(lan)



            End If
            ListBox3.Items.Clear()
            For Each item In listOfShows
                ListBox3.Items.Add(item.showtitle)
            Next

            ListBox3.SelectedIndex = 0
            If listOfShows(0).showbanner <> Nothing Then
                Try
                    PictureBox9.ImageLocation = listOfShows(0).showbanner
                Catch ex As Exception
                    PictureBox9.Image = Nothing
                End Try
            End If

            Call checklanguage()
            messbox.Close()
        Else
            MsgBox("Please Enter a Search Term")
        End If
    End Sub


    Private Function gettoptvshow(ByVal tvshowname As String)

        templanguage = Preferences.tvdblanguagecode
        Try
            Dim tvdbstuff As New TVDBScraper

            Dim mirror As List(Of String) = tvdbstuff.getmirrors()
            Dim showslist As String = tvdbstuff.findshows(tvshowname, mirror(0))
            If showslist = "error" Then
                Return "error"
                Exit Function
            End If
            Dim newshows As New List(Of str_PossibleShowList)
            newshows.Clear()
            If showslist <> "none" Then
                Dim showlist As New XmlDocument
                showlist.LoadXml(showslist)
                Dim thisresult As XmlNode = Nothing
                For Each thisresult In showlist("allshows")
                    Select Case thisresult.Name
                        Case "show"
                            Dim results As XmlNode = Nothing
                            Dim lan As New str_PossibleShowList(SetDefaults)
                            For Each results In thisresult.ChildNodes
                                Select Case results.Name
                                    Case "showid"
                                        lan.showid = results.InnerText
                                    Case "showtitle"
                                        lan.showtitle = results.InnerText
                                    Case "showbanner"
                                        lan.showbanner = results.InnerText
                                End Select
                            Next
                            If lan.showtitle = tvshowname Then
                                If checktvlanguage(lan.showid, templanguage) = True Then
                                    Return lan.showid
                                End If
                            End If
                            newshows.Add(lan)
                    End Select
                Next
                Dim returnid As String = ""
                If checktvlanguage(newshows(0).showid, templanguage) = True Then
                    Return newshows(0).showid
                Else
                    If templanguage <> "en" Then
                        If checktvlanguage(newshows(0).showid, "en") = True Then
                            templanguage = "en"
                            Return newshows(0).showid
                        Else
                            Return "nolang"
                        End If
                    Else
                        Return "nolang"
                    End If
                End If
            Else
                Return "none"
            End If
        Catch ex As Exception
            Return "error"
        End Try
    End Function

    Private Sub bckgrnd_tvshowscraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bckgrnd_tvshowscraper.ProgressChanged

        'If e.UserState = "i" Then
        ToolStripStatusLabel5.Text = "Scraping TV Shows, " & newTvFolders.Count & " remaining"
        ToolStripStatusLabel5.Visible = True
        'Call updatetree(False)

        Dim NewShow As TvShow = e.UserState


        ListtvFiles(NewShow, "*.NFO")
        realTvPaths.Add(NewShow.FolderPath)

        For Each item In TvShows
            If item.FolderPath = NewShow.FolderPath Then
                Dim cnode As TreeNode = Nothing
                Dim tempstring As String = String.Empty
                Dim tempint As Integer

                totalTvShowCount += 1
                Dim shownode As Integer = -1

                If item.status IsNot Nothing AndAlso item.status.ToLower.IndexOf("xml error") <> -1 Then
                    Call TV_AddTvshowToTreeview(item.fullpath, item.title, True, item.locked)
                Else
                    Call TV_AddTvshowToTreeview(item.fullpath, item.title, False, item.locked)
                End If

                For Each episode In item.allepisodes
                    totalEpisodeCount += 1

                    Dim seasonno As Integer = -10
                    seasonno = Convert.ToInt32(episode.seasonno)

                    For g = 0 To TreeView1.Nodes.Count - 1
                        If TreeView1.Nodes(g).Name.ToString = item.fullpath Then
                            cnode = TreeView1.Nodes(g)
                            shownode = g
                            Exit For
                        End If
                    Next

                    Dim seasonstring As String = Nothing

                    If seasonno <> 0 And seasonno <> -1 Then
                        If seasonno < 10 Then
                            tempstring = "Season 0" & seasonno.ToString
                        Else
                            tempstring = "Season " & seasonno.ToString
                        End If
                    ElseIf seasonno = 0 Then
                        tempstring = "Specials"
                        'ElseIf seasonno = -1 Then
                        '    tempstring = "Unknown"
                    End If
                    Dim node As TreeNode
                    Dim alreadyexists As Boolean = False
                    For Each node In cnode.Nodes
                        If node.Text = tempstring Then
                            alreadyexists = True
                            Exit For
                        End If
                    Next
                    If alreadyexists = False Then cnode.Nodes.Add(tempstring)

                    For Each node In cnode.Nodes
                        If node.Text = tempstring Then
                            tempint = node.Index

                            Exit For
                        End If
                    Next

                    Dim eps As String
                    If episode.episodeno < 10 Then
                        eps = "0" & episode.episodeno.ToString
                    Else
                        eps = episode.episodeno.ToString
                    End If
                    eps = eps & " - " & episode.title
                    If episode.imdbid = Nothing Then
                        episode.imdbid = ""
                    End If

                    If episode.imdbid.ToLower.IndexOf("xml error") <> -1 Then
                        Call TV_AddEpisodeToTreeview(shownode, tempint, episode.episodepath, eps, True)
                    Else
                        Call TV_AddEpisodeToTreeview(shownode, tempint, episode.episodepath, eps, False)
                    End If


                Next
            End If
        Next
        TextBox32.Text = totalTvShowCount.ToString
        TextBox33.Text = totalEpisodeCount.ToString
        Me.BringToFront()
        Me.Activate()
        ';

    End Sub

    Private Sub bckgrnd_tvshowscraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bckgrnd_tvshowscraper.DoWork
        Dim speedy As Boolean = Preferences.tvshowautoquick
        Do While newTvFolders.Count > 0
            Dim NewShow As New TvShow
            NewShow.NfoFilePath = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")

            If NewShow.FileContainsReadableXml Then
                NewShow.Load()
                TvShows.Add(NewShow)

                TvShows.Add(NewShow)
                If Not Preferences.tvFolders.Contains(newTvFolders(0)) Then
                    Preferences.tvFolders.Add(newTvFolders(0))
                End If
                bckgrnd_tvshowscraper.ReportProgress(0, NewShow)
                newTvFolders.RemoveAt(0)

                Continue Do
            End If


            Dim FolderName As String = Utilities.GetLastFolder(newTvFolders(0) & "\hifh")

            Dim showyear As String = ""
            If FolderName.Contains("(") Then
                Dim M As Match
                M = Regex.Match(FolderName, "(\([\d]{4}\))")
                If M.Success = True Then
                    showyear = M.Value
                    showyear = showyear.Replace("(", "")
                    showyear = showyear.Replace(")", "")
                    showyear = showyear.Replace("{", "")
                    showyear = showyear.Replace("}", "")
                End If
                If FolderName.IndexOf("(") <> 0 Then
                    FolderName = FolderName.Substring(0, FolderName.IndexOf("("))
                    FolderName = FolderName.TrimEnd(" ")
                    If showyear <> "" Then
                        FolderName = FolderName & " (" & showyear & ")"
                    End If
                End If
            End If

            Dim tvshowid As String = gettoptvshow(FolderName)

            If IsNumeric(tvshowid) Then
                'tvshow found
                Dim newtvshow As New TvShow
                newtvshow.tvdbid = tvshowid
                newtvshow.path = IO.Path.Combine(newTvFolders(0), "tvshow.nfo")

                Dim tvdbstuff As New TVDBScraper

                Dim posterurl As String = ""
                Dim tempstring As String = ""

                If templanguage = Nothing Then templanguage = "en"
                If templanguage = "" Then templanguage = "en"
                Dim SeriesInfo As Tvdb.ShowData = tvdbstuff.GetShow(tvshowid, templanguage, True)
                NewShow.Id.Value = SeriesInfo.Series.Id
                NewShow.tvdbid = SeriesInfo.Series.Id
                NewShow.mpaa = SeriesInfo.Series.ContentRating
                NewShow.genre = SeriesInfo.Series.Genre
                NewShow.imdbid = SeriesInfo.Series.ImdbId
                NewShow.plot = SeriesInfo.Series.Overview
                NewShow.title = SeriesInfo.Series.SeriesName
                NewShow.runtime = SeriesInfo.Series.RunTimeWithCommercials
                NewShow.rating = SeriesInfo.Series.Rating
                NewShow.premiered = SeriesInfo.Series.FirstAired
                NewShow.studio = SeriesInfo.Series.Network


                Dim TvdbActors As Tvdb.Actors = tvdbstuff.GetActors(tvshowid, templanguage)
                For Each Act As Tvdb.Actor In TvdbActors.Items
                    If NewShow.ListActors.Count >= Preferences.maxactors Then
                        Exit For
                    End If

                    Dim NewAct As New Nfo.Actor
                    NewAct.ActorId = Act.Id
                    NewAct.actorname = Act.Name
                    NewAct.actorrole = Act.Role
                    NewAct.actorthumb = Act.Image

                    If Preferences.tvdbactorscrape = 0 Or Preferences.tvdbactorscrape = 3 Or newtvshow.imdbid = Nothing Then
                        Dim id As String = ""
                        'Dim acts As New MovieActors
                        Dim results As XmlNode = Nothing
                        Dim lan As New str_PossibleShowList(SetDefaults)


                        If Not String.IsNullOrEmpty(NewAct.actorthumb) Then
                            If NewAct.actorthumb <> "" And Preferences.actorseasy = True And speedy = False Then
                                If workingTvShow.tvshowactorsource <> "imdb" Or workingTvShow.imdbid = Nothing Then
                                    Dim workingpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "")
                                    workingpath = workingpath & ".actors\"

                                    Utilities.EnsureFolderExists(workingpath)

                                    Dim hg As New IO.DirectoryInfo(workingpath)
                                    Dim destsorted As Boolean = False
                                    If Not hg.Exists Then

                                        IO.Directory.CreateDirectory(workingpath)
                                        destsorted = True

                                    Else
                                        destsorted = True
                                    End If
                                    If destsorted = True Then
                                        Dim filename As String = NewAct.actorname.Replace(" ", "_")
                                        filename = filename & ".tbn"
                                        filename = IO.Path.Combine(workingpath, filename)

                                        Utilities.DownloadFile(NewAct.actorthumb, filename)
                                    End If
                                End If
                            End If
                            If Preferences.actorsave = True And id <> "" And Preferences.actorseasy = False Then
                                Dim workingpath As String = ""
                                Dim networkpath As String = Preferences.actorsavepath

                                tempstring = networkpath & "\" & id.Substring(id.Length - 2, 2)
                                Dim hg As New IO.DirectoryInfo(tempstring)
                                If Not hg.Exists Then
                                    IO.Directory.CreateDirectory(tempstring)
                                End If
                                workingpath = networkpath & "\" & id.Substring(id.Length - 2, 2) & "\tv" & id & ".jpg"
                                If Not IO.File.Exists(workingpath) Then
                                    Utilities.DownloadFile(NewAct.actorthumb, workingpath)
                                End If
                                NewAct.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, id.Substring(id.Length - 2, 2))
                                If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                    NewAct.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, id.Substring(id.Length - 2, 2) & "/tv" & id & ".jpg")
                                Else
                                    NewAct.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, id.Substring(id.Length - 2, 2) & "\tv" & id & ".jpg")
                                End If


                            End If
                        End If
                        Dim exists As Boolean = False
                        For Each actors In NewShow.ListActors
                            If actors.actorname = NewAct.actorname And actors.actorrole = NewAct.actorrole Then
                                exists = True
                                Exit For
                            End If
                        Next
                        If exists = False Then
                            NewShow.ListActors.Add(NewAct)
                        End If
                    End If





                Next

                If Preferences.tvdbactorscrape = 1 Or Preferences.tvdbactorscrape = 2 And newtvshow.imdbid <> Nothing Then
                    Dim imdbscraper As New Classimdb
                    Dim actorlist As String
                    Dim actorstring As New XmlDocument
                    actorlist = imdbscraper.getimdbactors(Preferences.imdbmirror, newtvshow.imdbid)

                    actorstring.LoadXml(actorlist)

                    For Each thisresult As XmlNode In actorstring("actorlist")
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
                                                If Preferences.actorsave = True And detail.InnerText <> "" And speedy = False Then
                                                    Dim workingpath As String = ""
                                                    Dim networkpath As String = Preferences.actorsavepath

                                                    tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                                    Dim hg As New IO.DirectoryInfo(tempstring)
                                                    If Not hg.Exists Then
                                                        IO.Directory.CreateDirectory(tempstring)
                                                    End If
                                                    workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                    If Not IO.File.Exists(workingpath) Then
                                                        Utilities.DownloadFile(newactor.actorthumb, workingpath)
                                                    End If
                                                    newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                        newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                    Else
                                                        newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                    End If

                                                End If
                                            End If
                                    End Select
                                Next
                                newtvshow.ListActors.Add(newactor)
                        End Select
                    Next
                    While newtvshow.ListActors.Count > Preferences.maxactors
                        newtvshow.ListActors.RemoveAt(newtvshow.ListActors.Count - 1)
                    End While

                End If

                Dim showlist As New XmlDocument
                Dim artlist As New List(Of TvBanners)
                artlist.Clear()
                Dim artdone As Boolean = False
                If Preferences.tvfanart = True Or Preferences.tvposter = True Or Preferences.seasonall <> "none" Then
                    Dim thumblist As Tvdb.Banners = tvdbstuff.GetPosterList(newtvshow.tvdbid, True)
                    'showlist.LoadXml(thumblist)
                    artdone = True

                    For Each Item As Tvdb.Banner In thumblist.Items
                        Dim NewItem As New TvBanners
                        NewItem = Item
                        artlist.Add(NewItem)
                    Next
                End If

                If Not speedy Then


                    If Preferences.downloadtvseasonthumbs = True Then
                        For f = 0 To 1000
                            Dim seasonposter As String = ""
                            For Each Image In artlist
                                If Image.season = f.ToString And Image.language = templanguage Then
                                    seasonposter = Image.url
                                    Exit For
                                End If
                            Next
                            If seasonposter = "" Then
                                For Each Image In artlist
                                    If Image.season = f.ToString And Image.language = "en" Then
                                        seasonposter = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If seasonposter = "" Then
                                For Each Image In artlist
                                    If Image.season = f.ToString Then
                                        seasonposter = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If seasonposter <> "" Then
                                If f < 10 Then
                                    tempstring = "0" & f.ToString
                                Else
                                    tempstring = f.ToString
                                End If
                                Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season" & tempstring & ".tbn")
                                If tempstring = "00" Then
                                    seasonpath = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season-specials.tbn")
                                End If
                                If Not IO.File.Exists(seasonpath) Then

                                    Utilities.DownloadFile(seasonposter, seasonpath)

                                End If
                            End If
                        Next
                    End If

                    If Preferences.tvfanart = True Then
                        Dim fanartposter As String
                        fanartposter = ""
                        If CheckBox7.CheckState = CheckState.Checked Then
                            For Each Image In artlist
                                If Image.language = templanguage And Image.bannerType = "fanart" Then
                                    fanartposter = Image.url
                                    Exit For
                                End If
                            Next
                        End If
                        If fanartposter = "" Then
                            For Each Image In artlist
                                If Image.language = "en" And Image.bannerType = "fanart" Then
                                    fanartposter = Image.url
                                    Exit For
                                End If
                            Next
                        End If
                        If fanartposter = "" Then
                            For Each Image In artlist
                                If Image.bannerType = "fanart" Then
                                    fanartposter = Image.url
                                    Exit For
                                End If
                            Next
                        End If
                        If fanartposter <> "" Then

                            Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "fanart.jpg")
                            If Not IO.File.Exists(seasonpath) Then

                                Dim buffer(4000000) As Byte
                                Dim size As Integer = 0
                                Dim bytesRead As Integer = 0

                                Dim thumburl As String = fanartposter
                                Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                Dim res As HttpWebResponse = req.GetResponse()
                                Dim contents As Stream = res.GetResponseStream()
                                Dim bytesToRead As Integer = CInt(buffer.Length)
                                Dim bmp As New Bitmap(contents)



                                While bytesToRead > 0
                                    size = contents.Read(buffer, bytesRead, bytesToRead)
                                    If size = 0 Then Exit While
                                    bytesToRead -= size
                                    bytesRead += size
                                End While



                                If Preferences.resizefanart = 1 Then
                                    bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    scraperLog = scraperLog & "Fanart not resized" & vbCrLf
                                ElseIf Preferences.resizefanart = 2 Then
                                    If bmp.Width > 1280 Or bmp.Height > 720 Then
                                        Dim bm_source As New Bitmap(bmp)
                                        Dim bm_dest As New Bitmap(1280, 720)
                                        Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                        gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                                        bm_dest.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                        scraperLog = scraperLog & "Farart Resized to 1280x720" & vbCrLf
                                    Else
                                        scraperLog = scraperLog & "Fanart not resized, already =< required size" & vbCrLf
                                        bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    End If
                                ElseIf Preferences.resizefanart = 3 Then
                                    If bmp.Width > 960 Or bmp.Height > 540 Then
                                        Dim bm_source As New Bitmap(bmp)
                                        Dim bm_dest As New Bitmap(960, 540)
                                        Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                        gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                                        bm_dest.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                        scraperLog = scraperLog & "Farart Resized to 960x540" & vbCrLf
                                    Else
                                        scraperLog = scraperLog & "Fanart not resized, already =< required size" & vbCrLf
                                        bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    End If

                                End If
                            End If
                        End If
                    End If


                    Dim seasonallpath As String = ""
                    If Preferences.tvposter = True Then
                        Dim posterurlpath As String = ""

                        If Preferences.postertype = "poster" Then 'poster
                            For Each Image In artlist
                                If Image.language = templanguage And Image.bannerType = "poster" Then
                                    posterurl = Image.url
                                    Exit For
                                End If
                            Next
                            If posterurlpath = "" Then
                                For Each Image In artlist
                                    If Image.language = "en" And Image.bannerType = "poster" Then
                                        posterurlpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If posterurlpath = "" Then
                                For Each Image In artlist
                                    If Image.bannerType = "poster" Then
                                        posterurlpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If posterurlpath <> "" And Preferences.seasonall <> "none" Then
                                seasonallpath = posterurlpath
                            End If
                        ElseIf Preferences.postertype = "banner" Then 'banner
                            For Each Image In artlist
                                If Image.language = templanguage And Image.bannerType = "series" And Image.season = Nothing Then
                                    posterurl = Image.url
                                    Exit For
                                End If
                            Next
                            If posterurlpath = "" Then
                                For Each Image In artlist
                                    If Image.language = "en" And Image.bannerType = "series" And Image.season = Nothing Then
                                        posterurlpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If posterurlpath = "" Then
                                For Each Image In artlist
                                    If Image.bannerType = "series" And Image.season = Nothing Then
                                        posterurlpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If posterurlpath <> "" And RadioButton16.Checked = True Then
                                seasonallpath = posterurlpath
                            End If
                        End If

                        If posterurlpath <> "" And speedy = False Then

                            Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "folder.jpg")
                            If Not IO.File.Exists(seasonpath) Then

                                Utilities.DownloadFile(posterurlpath, seasonpath)

                            End If
                        End If
                    End If

                    If Preferences.seasonall <> "none" And seasonallpath = "" Then
                        If Preferences.seasonall = "poster" Then 'poster
                            For Each Image In artlist
                                If Image.language = templanguage And Image.bannerType = "poster" Then
                                    seasonallpath = Image.url
                                    Exit For
                                End If
                            Next
                            If seasonallpath = "" Then
                                For Each Image In artlist
                                    If Image.language = "en" And Image.bannerType = "poster" Then
                                        seasonallpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If seasonallpath = "" Then
                                For Each Image In artlist
                                    If Image.bannerType = "poster" Then
                                        seasonallpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                        ElseIf Preferences.seasonall = "wide" = True Then 'banner
                            For Each Image In artlist
                                If Image.language = templanguage And Image.bannerType = "series" And Image.season = Nothing Then
                                    seasonallpath = Image.url
                                    Exit For
                                End If
                            Next
                            If seasonallpath = "" Then
                                For Each Image In artlist
                                    If Image.language = "en" And Image.bannerType = "series" And Image.season = Nothing Then
                                        seasonallpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                            If seasonallpath = "" Then
                                For Each Image In artlist
                                    If Image.bannerType = "series" And Image.season = Nothing Then
                                        seasonallpath = Image.url
                                        Exit For
                                    End If
                                Next
                            End If
                        End If

                        If seasonallpath <> "" Then

                            Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season-all.tbn")
                            If Not IO.File.Exists(seasonpath) Or CheckBox6.CheckState = CheckState.Checked Then

                                Utilities.DownloadFile(seasonallpath, seasonallpath)

                            End If
                        End If
                    ElseIf Preferences.seasonall <> "none" And seasonallpath <> "" Then
                        Dim seasonpath As String = newtvshow.path.Replace(IO.Path.GetFileName(newtvshow.path), "season-all.tbn")
                        If Not IO.File.Exists(seasonpath) Then
                            Utilities.DownloadFile(seasonallpath, seasonpath)
                        End If
                    End If
                End If
                If artdone = False Then
                    Dim thumblist As String = tvdbstuff.GetPosterList(newtvshow.tvdbid)
                    showlist.LoadXml(thumblist)
                    artdone = True
                    'CheckBox3 = seasons
                    'CheckBox4 = fanart
                    'CheckBox5 = poster
                    For Each thisresult As XmlNode In showlist("banners")
                        Select Case thisresult.Name
                            Case "banner"
                                Dim individualposter As New TvBanners
                                For Each results In thisresult.ChildNodes
                                    Select Case results.Name
                                        Case "url"
                                            individualposter.url = results.InnerText
                                        Case "bannertype"
                                            individualposter.bannerType = results.InnerText
                                        Case "resolution"
                                            individualposter.resolution = results.InnerText
                                        Case "language"
                                            individualposter.language = results.InnerText
                                        Case "season"
                                            individualposter.season = results.InnerText
                                    End Select
                                Next
                                artlist.Add(individualposter)
                        End Select
                    Next
                End If

                For Each url In artlist
                    If url.bannerType <> "fanart" Then
                        newtvshow.posters.Add(url.url)
                    Else
                        newtvshow.fanart.Add(url.url)
                    End If
                Next
                newtvshow.locked = 2
                newtvshow.language = Preferences.tvdblanguagecode
                If Preferences.tvdbactorscrape = 0 Or Preferences.tvdbactorscrape = 2 Then
                    newtvshow.episodeactorsource = "tvdb"
                Else
                    newtvshow.episodeactorsource = "imdb"
                End If
                If Preferences.tvdbactorscrape = 0 Or Preferences.tvdbactorscrape = 3 Then
                    newtvshow.tvshowactorsource = "tvdb"
                Else
                    newtvshow.tvshowactorsource = "imdb"
                End If

                If tempstring = "0" Or tempstring = "2" Then
                    newtvshow.episodeactorsource = "tvdb"
                Else
                    newtvshow.episodeactorsource = "imdb"
                End If

                newtvshow.sortorder = Preferences.sortorder

                nfoFunction.savetvshownfo(newtvshow.path, newtvshow, True)
            End If
            DownloadMissingArt(NewShow)
            NewShow.locked = 2
            NewShow.Save()

            TvShows.Add(NewShow)
            If Not Preferences.tvFolders.Contains(newTvFolders(0)) Then
                Preferences.tvFolders.Add(newTvFolders(0))
            End If
            bckgrnd_tvshowscraper.ReportProgress(0, NewShow)
            newTvFolders.RemoveAt(0)
        Loop

        TV_CleanFolderList()
    End Sub

    Public Sub TV_CleanFolderList()
        Dim TempList As List(Of String)
        TempList = Preferences.tvFolders
        Dim ReturnList As New List(Of String)

        For Each item In newTvFolders
            If Not ReturnList.Contains(item) Then
                ReturnList.Add(item)
            End If
        Next

        Preferences.tvFolders = TempList
    End Sub

    Private Sub TV_EpisodeScraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
        Dim tempstring As String = ""
        Dim tempint As Integer
        Dim errorcounter As Integer = 0
        newEpisodeList.Clear()
        Dim newtvfolders As New List(Of String)
        Dim progress As Integer
        progress = 0
        Dim progresstext As String = String.Empty
        Preferences.tvScraperLog = ""
        Dim dirpath As String = String.Empty
        Dim moviepattern As String = String.Empty
        Dim showtitle As String = ""
        If bckgroundscanepisodes.CancellationPending Then
            Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation cancelled by user"
            Exit Sub
        End If


        progresstext = String.Concat("Scanning TV Folders For New Episodes")
        bckgroundscanepisodes.ReportProgress(progress, progresstext)


        Preferences.tvScraperLog = "Starting Folder Scan" & vbCrLf & vbCrLf

        Dim extensions(100) As String
        Dim extensioncount As Integer
        extensions(1) = "*.avi"
        extensions(2) = "*.xvid"
        extensions(3) = "*.divx"
        extensions(4) = "*.img"
        extensions(5) = "*.mpg"
        extensions(6) = "*.mpeg"
        extensions(7) = "*.mov"
        extensions(8) = "*.rm"
        extensions(9) = "*.3gp"
        extensions(10) = "*.m4v"
        extensions(11) = "*.wmv"
        extensions(12) = "*.asf"
        extensions(13) = "*.mp4"
        extensions(14) = "*.mkv"
        extensions(15) = "*.nrg"
        extensions(16) = "*.iso"
        extensions(17) = "*.rmvb"
        extensions(18) = "*.ogm"
        extensions(19) = "*.bin"
        extensions(20) = "*.ts"
        extensions(21) = "*.vob"
        extensions(22) = "*.m2ts"
        extensions(23) = "*.rar"
        extensions(24) = "*.flv"
        extensions(25) = "*.dvr-ms"
        extensions(26) = "VIDEO_TS.IFO"

        extensioncount = 26


        For Each tvfolder In listofshowfolders
            Dim add As Boolean = True
            For Each tvshow In TvShows
                If tvshow.fullpath.IndexOf(tvfolder) <> -1 Then
                    If tvshow.locked = True Or tvshow.locked = 2 Then
                        If manual = False Then
                            add = False
                            Exit For
                        End If
                    End If
                End If
            Next
            If add = True Then
                tvfolder = IO.Path.GetDirectoryName(tvfolder)
                progresstext = String.Concat("Adding subfolders: " & tvfolder)
                bckgroundscanepisodes.ReportProgress(progress, progresstext)
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation cancelled by user"
                    Exit Sub
                End If
                tempstring = "" 'tvfolder
                Dim hg As New IO.DirectoryInfo(tvfolder)
                If hg.Exists Then
                    scraperLog = scraperLog & "found" & hg.FullName.ToString & vbCrLf
                    newtvfolders.Add(tvfolder)
                    scraperLog = scraperLog & "Checking for subfolders" & vbCrLf

                    Try
                        For Each strfolder As String In My.Computer.FileSystem.GetDirectories(tvfolder)
                            Try
                                If strfolder.IndexOf("System Volume Information") = -1 Then
                                    Preferences.tvScraperLog = Preferences.tvScraperLog & "Subfolder added :- " & strfolder.ToString & vbCrLf
                                    newtvfolders.Add(strfolder)
                                    For Each strfolder2 As String In My.Computer.FileSystem.GetDirectories(strfolder, FileIO.SearchOption.SearchAllSubDirectories)
                                        Try
                                            If strfolder2.IndexOf("System Volume Information") = -1 Then
                                                Preferences.tvScraperLog = Preferences.tvScraperLog & "Subfolder added :- " & strfolder2.ToString & vbCrLf
                                                newtvfolders.Add(strfolder2)
                                            End If
                                        Catch ex As Exception
                                            MsgBox(ex.Message)
                                        End Try
                                    Next
                                End If
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try
                        Next
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                End If
            Else
                Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Show Locked, Ignoring: " & tvfolder & vbCrLf
            End If
        Next

        scraperLog = scraperLog & vbCrLf
        Application.DoEvents()
        Dim mediacounter As Integer = newEpisodeList.Count
        For g = 0 To newtvfolders.Count - 1
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                Exit Sub
            End If

            bckgroundscanepisodes.ReportProgress(progress, progresstext)
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation cancelled by user"
                Exit Sub
            End If
            progresstext = String.Concat("Searching for episodes in " & newtvfolders(g))
            bckgroundscanepisodes.ReportProgress(progress, progresstext)
            For f = 1 To extensioncount


                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation cancelled by user"
                    Exit Sub
                End If
                moviepattern = extensions(f)
                dirpath = newtvfolders(g)
                Dim dir_info As New System.IO.DirectoryInfo(dirpath)
                findnewepisodes(dirpath, moviepattern)
            Next f
            tempint = newEpisodeList.Count - mediacounter

            Preferences.tvScraperLog = Preferences.tvScraperLog & tempint.ToString & " New episodes found in directory:- " & dirpath & vbCrLf
            mediacounter = newEpisodeList.Count
        Next g

        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf
        If newEpisodeList.Count <= 0 Then
            Preferences.tvScraperLog = Preferences.tvScraperLog & tempint.ToString & "No new episodes found, exiting scraper" & dirpath & vbCrLf
            Exit Sub
        End If

        Dim S As String = ""
        For Each newepisode In newEpisodeList
            S = ""
            Dim newEpisodeToAdd As New TvEpisode
            newEpisodeToAdd.episodeno = ""
            newEpisodeToAdd.episodepath = ""
            'newepisodetoadd.status = ""
            newEpisodeToAdd.imdbid = ""
            newEpisodeToAdd.playcount = ""
            newEpisodeToAdd.rating = ""
            newEpisodeToAdd.seasonno = ""
            newEpisodeToAdd.title = ""
            newEpisodeToAdd.tvdbid = ""

            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                Exit Sub
            End If
            Dim episode As New TvEpisode

            For Each Regexs In tvRegex

                S = newepisode.episodepath '.ToLower
                S = S.Replace("x264", "")
                S = S.Replace("720p", "")
                S = S.Replace("720i", "")
                S = S.Replace("1080p", "")
                S = S.Replace("1080i", "")
                S = S.Replace("X264", "")
                S = S.Replace("720P", "")
                S = S.Replace("720I", "")
                S = S.Replace("1080P", "")
                S = S.Replace("1080I", "")
                Dim M As Match
                M = Regex.Match(S, Regexs)
                If M.Success = True Then
                    Try
                        newepisode.seasonno = M.Groups(1).Value.ToString
                        newepisode.episodeno = M.Groups(2).Value.ToString
                        If newepisode.seasonno <> "-1" And newepisode.episodeno <> "-1" Then
                            Preferences.tvScraperLog = Preferences.tvScraperLog & "Season and Episode information found for : " & newepisode.episodepath & newepisode.seasonno & "x" & newepisode.episodeno & vbCrLf
                        Else
                            Preferences.tvScraperLog = Preferences.tvScraperLog & "Cant extract Season and Episode deatails from filename: " & newepisode.seasonno & "x" & newepisode.episodeno & vbCrLf
                        End If
                        Try
                            newepisode.fanartpath = S.Substring(M.Groups(2).Index + M.Groups(2).Value.Length, S.Length - (M.Groups(2).Index + M.Groups(2).Value.Length))
                        Catch ex As Exception
#If SilentErrorScream Then
                                Throw ex
#End If
                        End Try
                        Exit For
                    Catch
                        newepisode.seasonno = "-1"
                        newepisode.episodeno = "-1"
                    End Try
                End If
            Next
            If newepisode.seasonno = Nothing Then newepisode.seasonno = "-1"
            If newepisode.episodeno = Nothing Then newepisode.episodeno = "-1"
        Next
        Dim savepath As String = ""
        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf
        Dim scrapedok As Boolean
        For Each eps In newEpisodeList
            tempTVDBiD = ""
            Dim episodearray As New List(Of TvEpisode)
            episodearray.Clear()
            Dim multieps2 As New TvEpisode
            multieps2.seasonno = eps.seasonno
            multieps2.episodeno = eps.episodeno
            multieps2.episodepath = eps.episodepath
            multieps2.mediaextension = eps.mediaextension
            episodearray.Add(multieps2)
            If bckgroundscanepisodes.CancellationPending Then
                Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                Exit Sub
            End If
            Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Working on episode: " & eps.episodepath & vbCrLf
            progresstext = String.Concat("Scraping " & IO.Path.GetFileName(eps.episodepath))
            bckgroundscanepisodes.ReportProgress(progress, progresstext)

            Dim removal As String = ""
            If eps.seasonno = "-1" Or eps.episodeno = "-1" Then
                eps.title = Utilities.GetFileName(eps.episodepath)
                eps.rating = "0"
                eps.playcount = "0"
                eps.genre = "Unknown Episode Season and/or Episode Number"
                eps.filedetails = Utilities.Get_HdTags(eps.mediaextension)
                episodearray.Add(eps)
                savepath = episodearray(0).episodepath
            Else

                Dim temppath As String = eps.episodepath
                'check for multiepisode files
                Dim M2 As Match
                Dim epcount As Integer = 0
                Dim multiepisode As Boolean = False
                Dim allepisodes(100) As Integer
                S = eps.fanartpath
                eps.fanartpath = ""
                Do
                    'S = temppath '.ToLower
                    '<tvregex>[Ss]([\d]{1,2}).?[Ee]([\d]{3})</tvregex>
                    M2 = Regex.Match(S, "(([EeXx])([\d]{1,4}))")
                    If M2.Success = True Then
                        Dim skip As Boolean = False
                        For Each epso In episodearray
                            If epso.episodeno = M2.Groups(3).Value Then skip = True
                        Next
                        If skip = False Then
                            Dim multieps As New TvEpisode
                            multieps.seasonno = eps.seasonno
                            multieps.episodeno = M2.Groups(3).Value
                            multieps.episodepath = eps.episodepath
                            multieps.mediaextension = eps.mediaextension
                            episodearray.Add(multieps)
                            allepisodes(epcount) = Convert.ToDecimal(M2.Groups(3).Value)
                        End If
                        Try
                            S = S.Substring(M2.Groups(3).Index + M2.Groups(3).Value.Length, S.Length - (M2.Groups(3).Index + M2.Groups(3).Value.Length))
                        Catch ex As Exception
#If SilentErrorScream Then
                                Throw ex
#End If
                        End Try
                    End If
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                Loop Until M2.Success = False
                Dim language As String = ""
                Dim sortorder As String = ""
                Dim tvdbid As String = ""
                Dim imdbid As String = ""
                Dim actorsource As String = ""
                Dim realshowpath As String = ""

                savepath = episodearray(0).episodepath
                Dim EpisodeName As String = ""
                For Each Shows In TvShows
                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    If episodearray(0).episodepath.IndexOf(Shows.fullpath.Replace("tvshow.nfo", "")) <> -1 Then
                        language = Shows.language
                        sortorder = Shows.sortorder
                        tvdbid = Shows.tvdbid
                        tempTVDBiD = Shows.tvdbid
                        imdbid = Shows.imdbid
                        showtitle = Shows.title
                        EpisodeName = Shows.title
                        realshowpath = Shows.fullpath
                        actorsource = Shows.episodeactorsource
                    End If
                Next
                If episodearray.Count > 1 Then
                    Preferences.tvScraperLog = Preferences.tvScraperLog & "Multipart episode found: " & vbCrLf
                    Preferences.tvScraperLog = Preferences.tvScraperLog & "Season: " & episodearray(0).seasonno & " Episodes, "
                    For Each ep In episodearray
                        Preferences.tvScraperLog = Preferences.tvScraperLog & ep.episodeno & ", "
                    Next
                    Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf
                End If
                Preferences.tvScraperLog = Preferences.tvScraperLog & "Looking up scraper options from tvshow.nfo" & vbCrLf

                For Each singleepisode In episodearray

                    If bckgroundscanepisodes.CancellationPending Then
                        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                        Exit Sub
                    End If
                    If singleepisode.seasonno.Length > 0 Or singleepisode.seasonno.IndexOf("0") = 0 Then
                        Do Until singleepisode.seasonno.IndexOf("0") <> 0 Or singleepisode.seasonno.Length = 1
                            singleepisode.seasonno = singleepisode.seasonno.Substring(1, singleepisode.seasonno.Length - 1)
                        Loop
                        If singleepisode.episodeno = "00" Then
                            singleepisode.episodeno = "0"
                        End If
                        If singleepisode.episodeno <> "0" Then
                            Do Until singleepisode.episodeno.IndexOf("0") <> 0
                                singleepisode.episodeno = singleepisode.episodeno.Substring(1, singleepisode.episodeno.Length - 1)
                            Loop
                        End If
                    End If
                    'Dim episodescraper As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
                    Dim episodescraper As New TVDBScraper
                    If sortorder = "" Then sortorder = "default"
                    Dim tempsortorder As String = sortorder
                    If language = "" Then language = "en"
                    If actorsource = "" Then actorsource = "tvdb"
                    If tvdbid <> "" Then
                        Dim episodeurl As String = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                        If Not UrlIsValid(episodeurl) Then
                            If sortorder.ToLower = "dvd" Then
                                tempsortorder = "default"
                                Preferences.tvScraperLog = Preferences.tvScraperLog & "This episode could not be found on TVDB using DVD sort order" & vbCrLf
                                Preferences.tvScraperLog = Preferences.tvScraperLog & "Attempting to find using default sort order" & vbCrLf
                                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/default/" & singleepisode.seasonno & "/" & singleepisode.episodeno & "/" & language & ".xml"
                            End If
                        End If

                        If UrlIsValid(episodeurl) Then


                            If Preferences.tvshow_useXBMC_Scraper = True Then
                                Dim FinalResult As String = ""
                                episodearray = XBMCScrape_TVShow_EpisodeDetails(tvdbid, tempsortorder, episodearray, language)
                                If episodearray.Count >= 1 Then
                                    For x As Integer = 0 To episodearray.Count - 1
                                        Preferences.tvScraperLog = Preferences.tvScraperLog & "Scraping body of episode: " & episodearray(x).episodeno & " - OK" & vbCrLf
                                    Next
                                    scrapedok = True
                                Else
                                    Preferences.tvScraperLog = Preferences.tvScraperLog & "Could not locate this episode on TVDB, or TVDB may be unavailable" & vbCrLf
                                    scrapedok = False
                                End If
                                Exit For
                            End If
                            'Dim tempepisode As String = episodescraper.getepisode(tvdbid, tempsortorder, singleepisode.seasonno, singleepisode.episodeno, language)
                            Dim tempepisode As String = getepisode(tvdbid, tempsortorder, singleepisode.seasonno, singleepisode.episodeno, language)
                            scrapedok = True
                            '                            Exit For
                            If tempepisode = Nothing Then
                                scrapedok = False
                                Preferences.tvScraperLog = Preferences.tvScraperLog & "This episode could not be found on TVDB" & vbCrLf
                            End If
                            If scrapedok = True Then
                                Dim scrapedepisode As New XmlDocument

                                Preferences.tvScraperLog = Preferences.tvScraperLog & "Scraping body of episode: " & singleepisode.episodeno & vbCrLf
                                scrapedepisode.LoadXml(tempepisode)

                                For Each thisresult As XmlNode In scrapedepisode("episodedetails")
                                    Select Case thisresult.Name
                                        Case "title"
                                            singleepisode.title = thisresult.InnerText
                                        Case "premiered"
                                            singleepisode.aired = thisresult.InnerText
                                        Case "plot"
                                            singleepisode.plot = thisresult.InnerText
                                        Case "director"
                                            Dim newstring As String
                                            newstring = thisresult.InnerText
                                            newstring = newstring.TrimEnd("|")
                                            newstring = newstring.TrimStart("|")
                                            newstring = newstring.Replace("|", " / ")
                                            singleepisode.director = newstring
                                        Case "credits"
                                            Dim newstring As String
                                            newstring = thisresult.InnerText
                                            newstring = newstring.TrimEnd("|")
                                            newstring = newstring.TrimStart("|")
                                            newstring = newstring.Replace("|", " / ")
                                            singleepisode.credits = newstring
                                        Case "rating"
                                            singleepisode.rating = thisresult.InnerText
                                        Case "thumb"
                                            singleepisode.thumb = thisresult.InnerText
                                        Case "actor"
                                            For Each actorl As XmlNode In thisresult.ChildNodes
                                                Select Case actorl.Name
                                                    Case "name"
                                                        Dim newactor As New str_MovieActors(SetDefaults)
                                                        newactor.actorname = actorl.InnerText
                                                        singleepisode.ListActors.Add(newactor)
                                                End Select
                                            Next
                                    End Select
                                Next
                                singleepisode.playcount = "0"


                                If actorsource = "imdb" Then
                                    Preferences.tvScraperLog = Preferences.tvScraperLog & "Scraping actors from IMDB" & vbCrLf
                                    Dim url As String
                                    url = "http://www.imdb.com/title/" & imdbid & "/episodes"
                                    Dim tvfblinecount As Integer = 0
                                    Dim tvdbwebsource(10000)
                                    tvfblinecount = 0
                                    If bckgroundscanepisodes.CancellationPending Then
                                        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                                        Exit Sub
                                    End If

                                    Dim wrGETURL As WebRequest
                                    wrGETURL = WebRequest.Create(url)
                                    Dim myProxy As New WebProxy("myproxy", 80)
                                    myProxy.BypassProxyOnLocal = True
                                    Dim objStream As Stream
                                    objStream = wrGETURL.GetResponse.GetResponseStream()
                                    Dim objReader As New StreamReader(objStream)
                                    Dim tvdbsLine As String = ""
                                    tvfblinecount = 0

                                    Do While Not tvdbsLine = ""   'is Nothing Changed by SK 18/5/2011     to avoid System.NullReferenceException was with new install & 1 new tvshow scraped & tried to serach for new episodes with 1 in the folder
                                        tvfblinecount += 1
                                        tvdbsLine = objReader.ReadLine
                                        If Not tvdbsLine = "" Then   '   Is Nothing Then Changed by SK 18/5/2011
                                            tvdbwebsource(tvfblinecount) = tvdbsLine
                                        End If
                                        If bckgroundscanepisodes.CancellationPending Then
                                            Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                                            Exit Sub
                                        End If
                                    Loop
                                    objReader.Close()
                                    objStream.Close()
                                    tvfblinecount -= 1





                                    If tvfblinecount <> 0 Then
                                        Dim tvtempstring As String
                                        tvtempstring = "Season " & singleepisode.seasonno & ", Episode " & singleepisode.episodeno & ":"
                                        For g = 1 To tvfblinecount
                                            If tvdbwebsource(g).indexof(tvtempstring) <> -1 Then
                                                Dim tvtempint As Integer
                                                tvtempint = tvdbwebsource(g).indexof("<a href=""/title/")
                                                If tvtempint <> -1 Then
                                                    tvtempstring = tvdbwebsource(g).substring(tvtempint + 16, 9)
                                                    '            Dim scraperfunction As New imdb.Classimdbscraper ' add to comment this one because of changes i made to the Class "Scraper" (ClassimdbScraper)
                                                    Dim scraperfunction As New Classimdb
                                                    Dim actorlist As String = ""
                                                    actorlist = scraperfunction.getimdbactors(Preferences.imdbmirror, tvtempstring, , Preferences.maxactors)
                                                    Dim tempactorlist As New List(Of str_MovieActors)
                                                    Dim thumbstring As New XmlDocument


                                                    thumbstring.LoadXml(actorlist)

                                                    Dim countactors As Integer = 0
                                                    For Each thisresult As XmlNode In thumbstring("actorlist")
                                                        If bckgroundscanepisodes.CancellationPending Then
                                                            Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                            Exit Sub
                                                        End If
                                                        Select Case thisresult.Name
                                                            Case "actor"
                                                                If countactors >= Preferences.maxactors Then
                                                                    Exit For
                                                                End If
                                                                countactors += 1
                                                                Dim newactor As New str_MovieActors(SetDefaults)

                                                                For Each detail As XmlNode In thisresult.ChildNodes
                                                                    Select Case detail.Name
                                                                        Case "name"
                                                                            newactor.actorname = detail.InnerText
                                                                        Case "role"
                                                                            newactor.actorrole = detail.InnerText
                                                                        Case "thumb"
                                                                            newactor.actorthumb = detail.InnerText
                                                                        Case "actorid"
                                                                            If newactor.actorthumb <> Nothing Then
                                                                                If Preferences.actorseasy = True And detail.InnerText <> "" Then
                                                                                    Dim workingpath As String = episodearray(0).episodepath.Replace(IO.Path.GetFileName(episodearray(0).episodepath), "")
                                                                                    workingpath = workingpath & ".actors\"
                                                                                    Dim hg As New IO.DirectoryInfo(workingpath)
                                                                                    Dim destsorted As Boolean = False
                                                                                    If Not hg.Exists Then

                                                                                        IO.Directory.CreateDirectory(workingpath)
                                                                                        destsorted = True

                                                                                    Else
                                                                                        destsorted = True
                                                                                    End If
                                                                                    If destsorted = True Then
                                                                                        Dim filename As String = newactor.actorname.Replace(" ", "_")
                                                                                        filename = filename & ".tbn"
                                                                                        Dim tvshowactorpath As String = realshowpath
                                                                                        tvshowactorpath = tvshowactorpath.Replace(IO.Path.GetFileName(tvshowactorpath), "")
                                                                                        tvshowactorpath = IO.Path.Combine(tvshowactorpath, ".actors\")
                                                                                        tvshowactorpath = IO.Path.Combine(tvshowactorpath, filename)
                                                                                        filename = IO.Path.Combine(workingpath, filename)
                                                                                        If IO.File.Exists(tvshowactorpath) Then

                                                                                            IO.File.Copy(tvshowactorpath, filename, True)

                                                                                        End If
                                                                                        If Not IO.File.Exists(filename) Then
                                                                                            Dim buffer(4000000) As Byte
                                                                                            Dim size As Integer = 0
                                                                                            Dim bytesRead As Integer = 0
                                                                                            Dim thumburl As String = newactor.actorthumb
                                                                                            Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                                                                                            Dim res As HttpWebResponse = req.GetResponse()
                                                                                            Dim contents As Stream = res.GetResponseStream()
                                                                                            Dim bytesToRead As Integer = CInt(buffer.Length)
                                                                                            While bytesToRead > 0
                                                                                                size = contents.Read(buffer, bytesRead, bytesToRead)
                                                                                                If size = 0 Then Exit While
                                                                                                bytesToRead -= size
                                                                                                bytesRead += size
                                                                                            End While

                                                                                            Dim fstrm As New FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write)
                                                                                            fstrm.Write(buffer, 0, bytesRead)
                                                                                            contents.Close()
                                                                                            fstrm.Close()
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                                If Preferences.actorsave = True And detail.InnerText <> "" And Preferences.actorseasy = False Then
                                                                                    Dim workingpath As String = ""
                                                                                    Dim networkpath As String = Preferences.actorsavepath

                                                                                    tempstring = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2)
                                                                                    Dim hg As New IO.DirectoryInfo(tempstring)
                                                                                    If Not hg.Exists Then
                                                                                        IO.Directory.CreateDirectory(tempstring)
                                                                                    End If
                                                                                    workingpath = networkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                                                    If Not IO.File.Exists(workingpath) Then
                                                                                        Dim buffer(4000000) As Byte
                                                                                        Dim size As Integer = 0
                                                                                        Dim bytesRead As Integer = 0
                                                                                        Dim thumburl As String = newactor.actorthumb
                                                                                        Dim req As HttpWebRequest = WebRequest.Create(thumburl)
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
                                                                                    newactor.actorthumb = IO.Path.Combine(Preferences.actornetworkpath, detail.InnerText.Substring(detail.InnerText.Length - 2, 2))
                                                                                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                                                                                        newactor.actorthumb = Preferences.actornetworkpath & "/" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "/" & detail.InnerText & ".jpg"
                                                                                    Else
                                                                                        newactor.actorthumb = Preferences.actornetworkpath & "\" & detail.InnerText.Substring(detail.InnerText.Length - 2, 2) & "\" & detail.InnerText & ".jpg"
                                                                                    End If

                                                                                End If
                                                                            End If
                                                                    End Select
                                                                    If bckgroundscanepisodes.CancellationPending Then
                                                                        Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                                        Exit Sub
                                                                    End If
                                                                Next
                                                                tempactorlist.Add(newactor)
                                                        End Select
                                                        If bckgroundscanepisodes.CancellationPending Then
                                                            Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                            Exit Sub
                                                        End If
                                                    Next





                                                    If tempactorlist.Count > 0 Then
                                                        Preferences.tvScraperLog &= "Actors scraped from IMDB OK" & vbCrLf
                                                        While tempactorlist.Count > Preferences.maxactors
                                                            tempactorlist.RemoveAt(tempactorlist.Count - 1)
                                                        End While
                                                        singleepisode.ListActors.Clear()
                                                        For Each actor In tempactorlist
                                                            singleepisode.ListActors.Add(actor)
                                                        Next
                                                        tempactorlist.Clear()
                                                    Else
                                                        Preferences.tvScraperLog &= "Actors not scraped from IMDB, reverting to TVDB actorlist" & vbCrLf
                                                    End If

                                                    Exit For
                                                End If
                                            End If
                                            If bckgroundscanepisodes.CancellationPending Then
                                                Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                                                Exit Sub
                                            End If
                                        Next

                                    Else
                                        'tvscraperlog = tvscraperlog & "Unable To Get Actors From IMDB, Defaulting To TVDB" & vbCrLf
                                    End If
                                End If

                            End If

                            If Preferences.enablehdtags = True Then

                                singleepisode.filedetails = Utilities.Get_HdTags(Utilities.GetFileName(singleepisode.episodepath))

                                If Not singleepisode.filedetails.filedetails_video.duration Is Nothing Then

                                    '1h 24mn 48s 546ms
                                    Dim hours As Integer
                                    Dim minutes As Integer
                                    tempstring = singleepisode.filedetails.filedetails_video.duration
                                    tempint = tempstring.IndexOf("h")
                                    If tempint <> -1 Then
                                        hours = Convert.ToInt32(tempstring.Substring(0, tempint))
                                        tempstring = tempstring.Substring(tempint + 1, tempstring.Length - (tempint + 1))
                                        tempstring = Trim(tempstring)
                                    End If
                                    tempint = tempstring.IndexOf("mn")
                                    If tempint <> -1 Then
                                        minutes = Convert.ToInt32(tempstring.Substring(0, tempint))
                                    End If
                                    If hours <> 0 Then
                                        minutes += hours * 60
                                    End If
                                    minutes = minutes + hours
                                    singleepisode.Runtime.Value = minutes.ToString & " min"
                                End If

                            End If
                        Else
                            Preferences.tvScraperLog &= "Could not locate this episode on TVDB, or TVDB may be unavailable" & vbCrLf
                        End If
                    Else
                        Preferences.tvScraperLog &= "No TVDB ID is available for this show, please scrape the show using the ""TV Show Selector"" TAB" & vbCrLf
                    End If

                Next

            End If
            If savepath <> "" And scrapedok = True Then
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog &= vbCrLf & "Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                Dim newnamepath As String = ""



                '' Commented out the lines below so that the episodes are renamed irrelavent of scraper (MC or XBMC scraper) - not sure of their purpose. 
                'If Preferences.tvshow_useXBMC_Scraper = True Then
                '    newnamepath = savepath
                'Else
                newnamepath = addepisode(episodearray, savepath, showtitle)
                ''9999999                                                               'This was already commented out, it must be a note of some sort.
                For Each ep In episodearray
                    ep.episodepath = newnamepath
                Next
                'End If
                bckgroundscanepisodes.ReportProgress(9999999, episodearray)
                If bckgroundscanepisodes.CancellationPending Then
                    Preferences.tvScraperLog = Preferences.tvScraperLog & vbCrLf & "Operation Cancelled by user" & vbCrLf
                    Exit Sub
                End If
                For Each Shows In TvShows
                    If episodearray(0).episodepath.IndexOf(Shows.fullpath.Replace("\tvshow.nfo", "")) <> -1 Then
                        'workingtvshow = nfofunction.loadfulltnshownfo(Shows.fullpath)
                        For Each ept In episodearray
                            For j = Shows.missingepisodes.Count - 1 To 0 Step -1
                                If Shows.missingepisodes(j).title = ept.title Then
                                    Shows.missingepisodes.RemoveAt(j)
                                    Exit For
                                End If
                            Next
                        Next
                        For Each ep In episodearray
                            Dim newwp As New TvEpisode
                            newwp.episodeno = ep.episodeno
                            newwp.episodepath = newnamepath
                            newwp.playcount = "0"
                            newwp.rating = ep.rating
                            newwp.seasonno = ep.seasonno
                            newwp.title = ep.title
                            Shows.allepisodes.Add(newwp)
                        Next
                    End If
                Next
            End If

        Next

        bckgroundscanepisodes.ReportProgress(0, progresstext)

    End Sub

    Private Sub TV_AddTvshowToTreeview(ByVal fullpath As String, ByVal title As String, Optional ByVal xmlerror As Boolean = False, Optional ByVal locked As Boolean = True)
        If xmlerror = True Then
            TreeView1.Nodes.Add(fullpath, title)
            For Each tn As TreeNode In TreeView1.Nodes
                If tn.Name = fullpath Then
                    If locked = True Or locked = 2 Then tn.StateImageIndex = 0
                    tn.ForeColor = Color.Red
                End If
            Next
        Else
            TreeView1.Nodes.Add(fullpath, title)
            For Each tn As TreeNode In TreeView1.Nodes
                If tn.Name = fullpath Then
                    tn.ForeColor = Color.Black
                    If locked = True Or locked = 2 Then tn.StateImageIndex = 0
                End If
            Next
        End If
    End Sub

    Private Sub TV_AddEpisodeToTreeview(ByVal rootnode As Integer, ByVal childnode As Integer, ByVal fullpath As String, ByVal title As String, Optional ByVal xmlerror As Boolean = False)
        Try
            Dim ccnode As TreeNode
            ccnode = TreeView1.Nodes(rootnode).Nodes(childnode)
            For Each nod In ccnode.Nodes
                If nod.text = title Then
                    ccnode.Nodes.Remove(nod)
                    Exit For
                End If
            Next
            ccnode.Nodes.Add(fullpath, title)

            If xmlerror = True Then
                For Each no As TreeNode In ccnode.Nodes
                    If no.Name = fullpath Then
                        no.ForeColor = Color.Red
                        Exit For
                    End If
                Next
            Else
                For Each no As TreeNode In ccnode.Nodes
                    If no.Name = fullpath Then
                        no.ForeColor = Color.Black
                        Exit For
                    End If
                Next
            End If
            'TreeView1.Nodes.Remove(node)
        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
            'MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub TV_PopulateTvTree()
        Dim tempint As Integer
        Dim tempstring As String = String.Empty
        Dim cnode As TreeNode = Nothing

        ComboBox4.Items.Clear()
        ComboBox4.Text = String.Empty


        PictureBox6.Image = Nothing
        PictureBox4.Image = Nothing
        PictureBox5.Image = Nothing
        TextBox10.Text = String.Empty
        TextBox11.Text = String.Empty
        TextBox9.Text = String.Empty
        TextBox12.Text = String.Empty
        TextBox13.Text = String.Empty
        TextBox14.Text = String.Empty
        TextBox15.Text = String.Empty
        TextBox16.Text = String.Empty
        TextBox18.Text = String.Empty
        TextBox19.Text = String.Empty
        If Not workingTvShow Is Nothing Then workingTvShow.path = String.Empty
        ComboBox4.Items.Clear()
        ComboBox4.Text = String.Empty
        TextBox20.Text = String.Empty
        TextBox21.Text = String.Empty
        TextBox22.Text = String.Empty
        TextBox23.Text = String.Empty
        TextBox24.Text = String.Empty
        TextBox25.Text = String.Empty
        ComboBox5.Items.Clear()
        ComboBox5.Text = String.Empty
        Panel9.Visible = False
        TextBox2.Text = String.Empty
        totalTvShowCount = 0
        totalEpisodeCount = 0
        TreeView1.Nodes.Clear()

        For Each item In TvShows
            totalTvShowCount += 1
            Dim shownode As Integer = -1

            If item.status IsNot Nothing AndAlso Not item.status.ToLower.Contains("xml error") Then
                Call TV_AddTvshowToTreeview(item.fullpath, item.title, True, item.locked)
            Else
                Call TV_AddTvshowToTreeview(item.fullpath, item.title, False, item.locked)
            End If


            For Each episode In item.allepisodes
                totalEpisodeCount += 1

                Dim seasonno As Integer = -10
                seasonno = Convert.ToInt32(episode.seasonno)

                For g = 0 To TreeView1.Nodes.Count - 1
                    If TreeView1.Nodes(g).Name.ToString = item.fullpath Then
                        cnode = TreeView1.Nodes(g)
                        shownode = g
                        Exit For
                    End If
                Next

                Dim seasonstring As String = Nothing

                If seasonno <> 0 And seasonno <> -1 Then
                    If seasonno < 10 Then
                        tempstring = "Season 0" & seasonno.ToString
                    Else
                        tempstring = "Season " & seasonno.ToString
                    End If
                ElseIf seasonno = 0 Then
                    tempstring = "Specials"
                End If

                Dim node As TreeNode
                Dim alreadyexists As Boolean = False
                For Each node In cnode.Nodes
                    If node.Text = tempstring Then
                        alreadyexists = True
                        Exit For
                    End If
                Next
                If alreadyexists = False Then cnode.Nodes.Add(tempstring)

                For Each node In cnode.Nodes
                    If node.Text = tempstring Then
                        tempint = node.Index
                        Exit For
                    End If
                Next

                Dim eps As String
                If episode.episodeno < 10 Then
                    eps = "0" & episode.episodeno.ToString
                Else
                    eps = episode.episodeno.ToString
                End If

                eps = eps & " - " & episode.title
                If episode.imdbid = Nothing Then
                    episode.imdbid = ""
                End If

                If episode.imdbid.ToLower.IndexOf("xml error") <> -1 Then
                    Call TV_AddEpisodeToTreeview(shownode, tempint, episode.episodepath, eps, True)
                Else
                    Call TV_AddEpisodeToTreeview(shownode, tempint, episode.episodepath, eps, False)
                End If

            Next

            For Each missingep In item.missingepisodes
                For g = 0 To TreeView1.Nodes.Count - 1
                    If TreeView1.Nodes(g).Name.ToString = item.fullpath Then
                        cnode = TreeView1.Nodes(g)
                        shownode = g
                        Exit For
                    End If
                Next

                Dim seasonstring As String = Nothing
                Dim seasonno As Integer = Convert.ToInt32(missingep.seasonno)
                If seasonno <> 0 And seasonno <> -1 Then
                    If seasonno < 10 Then
                        tempstring = "Season 0" & seasonno.ToString
                    Else
                        tempstring = "Season " & seasonno.ToString
                    End If
                ElseIf seasonno = 0 Then
                    tempstring = "Specials"
                End If

                Dim node As TreeNode
                Dim alreadyexists As Boolean = False
                For Each node In cnode.Nodes
                    If node.Text = tempstring Then
                        alreadyexists = True
                        Exit For
                    End If
                Next

                If alreadyexists = False Then cnode.Nodes.Add(tempstring)
                For Each node In cnode.Nodes
                    If node.Text = tempstring Then
                        tempint = node.Index
                        Exit For
                    End If
                Next

                Dim eps As String
                Dim episodeno As Integer = Convert.ToInt32(missingep.episodeno)
                If episodeno < 10 Then
                    eps = "0" & episodeno.ToString
                Else
                    eps = episodeno.ToString
                End If

                eps = eps & " - " & missingep.title
                Dim ccnode As TreeNode
                ccnode = TreeView1.Nodes(shownode).Nodes(tempint)
                Dim tempstring2 As String = "Missing: " & eps
                ccnode.Nodes.Add(tempstring2, eps)

                For Each no As TreeNode In ccnode.Nodes
                    If no.Name = tempstring2 Then
                        no.ForeColor = Color.Blue
                        no.Parent.ForeColor = Color.Blue
                        no.Parent.Parent.ForeColor = Color.Blue
                        Exit For
                    End If
                Next
            Next
        Next



        Dim MyNode As TreeNode
        If Not TreeView1.Nodes.Count = 0 Then
            MyNode = TreeView1.Nodes(0) 'First Level
            'MyNode = MyNode.Nodes(6)  ' Second Level
            TreeView1.SelectedNode = MyNode
            TabLevel1.Focus()
            TabControl3.Focus()
            TreeView1.Focus()
        End If
        TreeView1.Refresh()
        TreeView1.CollapseAll()

        TextBox32.Text = totalTvShowCount.ToString
        TextBox33.Text = totalEpisodeCount.ToString
    End Sub

    Private Sub ListtvFiles(ByVal tvshow As TvShow, ByVal pattern As String)

        Dim episode As New List(Of TvEpisode)
        Dim propfile As Boolean = False
        Dim allok As Boolean = False


        Dim newlist As New List(Of String)
        newlist.Clear()

        newlist = Utilities.EnumerateFolders(tvshow.FolderPath, 6) 'TODO: Restore loging functions

        newlist.Insert(0, tvshow.fullpath.Substring(0, tvshow.fullpath.Length - 11))
        If newlist.Count > 0 Then
            tvrebuildlog(newlist.Count - 1.ToString & " subfolders found in: " & newlist(0) & vbCrLf)
        End If
        For Each folder In newlist
            tvrebuildlog("Searching: " & vbCrLf & folder & vbCrLf & "for episodes")
            Dim dir_info As New System.IO.DirectoryInfo(folder)
            tvrebuildlog("Looking in " & folder)
            Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(pattern, SearchOption.TopDirectoryOnly)
            For Each fs_info As System.IO.FileInfo In fs_infos

                Try
                    Application.DoEvents()
                    If IO.Path.GetFileName(fs_info.FullName.ToLower) <> "tvshow.nfo" Then
                        tvrebuildlog("possible episode nfo found: " & fs_info.FullName)
                        episode = nfoFunction.loadbasicepisodenfo(fs_info.FullName)
                        If Not episode Is Nothing Then
                            For Each ep In episode
                                If ep.title <> Nothing Then
                                    Dim skip As Boolean = False
                                    For Each eps In tvshow.allepisodes
                                        If eps.seasonno = ep.seasonno And eps.episodeno = ep.episodeno And eps.episodepath = ep.episodepath Then
                                            skip = True
                                            Exit For
                                        End If
                                    Next
                                    If skip = False Then
                                        tvshow.allepisodes.Add(ep)
                                        tvrebuildlog("Episode appears to have loaded ok")
                                    End If
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    tvrebuildlog(ex.ToString)
                End Try
            Next fs_info
        Next
        tvrebuildlog(vbCrLf & vbCrLf & vbCrLf)

    End Sub

    Private Sub DownloadAvaileableMissingArtForShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadAvaileableMissingArtForShowToolStripMenuItem.Click
        DownloadMissingArt(workingTvShow)
    End Sub

    Public Sub DownloadMissingArt(ByVal BrokenShow As TvShow)
        'Dim messbox As New frmMessageBox("Attempting to download art", "", "       Please Wait")
        'messbox.Show()
        'messbox.Refresh()
        Application.DoEvents()
        Try
            'Dim tvdbstuff As New TVDB.tvdbscraper 'commented because of removed TVDB.dll
            Dim tvdbstuff As New TVDBScraper
            Dim showlist As New XmlDocument
            Dim thumblist As String = tvdbstuff.GetPosterList(BrokenShow.tvdbid)
            showlist.LoadXml(thumblist)
            Dim thisresult As XmlNode = Nothing
            Dim artlist As New List(Of TvBanners)
            artlist.Clear()
            For Each thisresult In showlist("banners")
                Select Case thisresult.Name
                    Case "banner"
                        Dim individualposter As New TvBanners
                        For Each results In thisresult.ChildNodes
                            Select Case results.Name
                                Case "url"
                                    individualposter.Url = results.InnerText
                                Case "bannertype"
                                    individualposter.BannerType = results.InnerText
                                Case "resolution"
                                    individualposter.Resolution = results.InnerText
                                Case "language"
                                    individualposter.Language = results.InnerText
                                Case "season"
                                    individualposter.Season = results.InnerText
                            End Select
                        Next
                        artlist.Add(individualposter)
                End Select
            Next
            If artlist.Count = 0 Then
                Exit Sub
            End If
            For f = 0 To 1000
                Dim seasonposter As String = ""
                For Each Image In artlist
                    If Image.Season = f.ToString And Image.Language = Preferences.tvdblanguagecode Then
                        seasonposter = Image.Url
                        Exit For
                    End If
                Next
                If seasonposter = "" Then
                    For Each Image In artlist
                        If Image.Season = f.ToString And Image.Language = "en" Then
                            seasonposter = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If seasonposter = "" Then
                    For Each Image In artlist
                        If Image.Season = f.ToString Then
                            seasonposter = Image.Url
                            Exit For
                        End If
                    Next
                End If
                Dim tempstring As String = ""
                If seasonposter <> "" Then
                    If f < 10 Then
                        tempstring = "0" & f.ToString
                    Else
                        tempstring = f.ToString
                    End If
                    Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season" & tempstring & ".tbn")
                    If tempstring = "00" Then
                        seasonpath = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season-specials.tbn")
                    End If
                    If Not IO.File.Exists(seasonpath) Then
                        Utilities.DownloadFile(seasonposter, seasonpath)
                    End If
                End If
            Next
            Dim fanartposter As String
            fanartposter = ""
            For Each Image In artlist
                If Image.Language = Preferences.tvdblanguagecode And Image.BannerType = "fanart" Then
                    fanartposter = Image.Url
                    Exit For
                End If
            Next
            If fanartposter = "" Then
                For Each Image In artlist
                    If Image.Language = "en" And Image.BannerType = "fanart" Then
                        fanartposter = Image.Url
                        Exit For
                    End If
                Next
            End If
            If fanartposter = "" Then
                For Each Image In artlist
                    If Image.BannerType = "fanart" Then
                        fanartposter = Image.Url
                        Exit For
                    End If
                Next
            End If
            If fanartposter <> "" Then

                Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "fanart.jpg")
                If Not IO.File.Exists(seasonpath) Then
                    Try
                        Dim buffer(4000000) As Byte
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0

                        Dim thumburl As String = fanartposter
                        Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                        Dim res As HttpWebResponse = req.GetResponse()
                        Dim contents As Stream = res.GetResponseStream()
                        Dim bytesToRead As Integer = CInt(buffer.Length)
                        Dim bmp As New Bitmap(contents)



                        While bytesToRead > 0
                            size = contents.Read(buffer, bytesRead, bytesToRead)
                            If size = 0 Then Exit While
                            bytesToRead -= size
                            bytesRead += size
                        End While


                        Try
                            If Preferences.resizefanart = 1 Then
                                bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                scraperLog = scraperLog & "Fanart not resized" & vbCrLf
                            ElseIf Preferences.resizefanart = 2 Then
                                If bmp.Width > 1280 Or bmp.Height > 720 Then
                                    Dim bm_source As New Bitmap(bmp)
                                    Dim bm_dest As New Bitmap(1280, 720)
                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                    gr.DrawImage(bm_source, 0, 0, 1280 - 1, 720 - 1)
                                    bm_dest.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    scraperLog = scraperLog & "Farart Resized to 1280x720" & vbCrLf
                                Else
                                    scraperLog = scraperLog & "Fanart not resized, already =< required size" & vbCrLf
                                    bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                End If
                            ElseIf Preferences.resizefanart = 3 Then
                                If bmp.Width > 960 Or bmp.Height > 540 Then
                                    Dim bm_source As New Bitmap(bmp)
                                    Dim bm_dest As New Bitmap(960, 540)
                                    Dim gr As Graphics = Graphics.FromImage(bm_dest)
                                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                                    gr.DrawImage(bm_source, 0, 0, 960 - 1, 540 - 1)
                                    bm_dest.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                    scraperLog = scraperLog & "Farart Resized to 960x540" & vbCrLf
                                Else
                                    scraperLog = scraperLog & "Fanart not resized, already =< required size" & vbCrLf
                                    bmp.Save(seasonpath, Imaging.ImageFormat.Jpeg)
                                End If

                            End If
                        Catch
                        End Try
                    Catch ex As WebException
#If SilentErrorScream Then
                        Throw ex
#End If
                    End Try
                End If
            End If

            Dim seasonallpath As String = ""
            Dim posterurlpath As String = ""
            Dim posterurl As String = ""
            If Preferences.postertype = "poster" Then 'poster
                For Each Image In artlist
                    If Image.Language = Preferences.tvdblanguagecode And Image.BannerType = "poster" Then
                        posterurl = Image.Url
                        Exit For
                    End If
                Next
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.Language = "en" And Image.BannerType = "poster" Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.BannerType = "poster" Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath <> "" And Preferences.seasonall <> "none" Then
                    seasonallpath = posterurlpath
                End If
            ElseIf Preferences.postertype = "banner" Then 'banner
                For Each Image In artlist
                    If Image.Language = Preferences.tvdblanguagecode And Image.BannerType = "series" And Image.Season = Nothing Then
                        posterurl = Image.Url
                        Exit For
                    End If
                Next
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.Language = "en" And Image.BannerType = "series" And Image.Season = Nothing Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath = "" Then
                    For Each Image In artlist
                        If Image.BannerType = "series" And Image.Season = Nothing Then
                            posterurlpath = Image.Url
                            Exit For
                        End If
                    Next
                End If
                If posterurlpath <> "" And RadioButton16.Checked = True Then
                    seasonallpath = posterurlpath
                End If
            End If

            If posterurlpath <> "" Then

                Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "folder.jpg")
                If Not IO.File.Exists(seasonpath) Then
                    Try
                        Dim buffer(4000000) As Byte
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0
                        Dim thumburl As String = posterurlpath
                        Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                        Dim res As HttpWebResponse = req.GetResponse()
                        Dim contents As Stream = res.GetResponseStream()
                        Dim bytesToRead As Integer = CInt(buffer.Length)
                        While bytesToRead > 0
                            size = contents.Read(buffer, bytesRead, bytesToRead)
                            If size = 0 Then Exit While
                            bytesToRead -= size
                            bytesRead += size
                        End While
                        Dim fstrm As New FileStream(seasonpath, FileMode.OpenOrCreate, FileAccess.Write)
                        fstrm.Write(buffer, 0, bytesRead)
                        contents.Close()
                        fstrm.Close()
                    Catch ex As WebException
#If SilentErrorScream Then
                        Throw ex
#End If
                        'MsgBox("Error Downloading main poster from TVDB")
                    End Try
                End If
            End If



            If Preferences.seasonall <> "none" And seasonallpath = "" Then
                If Preferences.seasonall = "poster" Then 'poster
                    For Each Image In artlist
                        If Image.Language = Preferences.tvdblanguagecode And Image.BannerType = "poster" Then
                            seasonallpath = Image.Url
                            Exit For
                        End If
                    Next
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.Language = "en" And Image.BannerType = "poster" Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.BannerType = "poster" Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                ElseIf Preferences.seasonall = "wide" = True Then 'banner
                    For Each Image In artlist
                        If Image.Language = Preferences.tvdblanguagecode And Image.BannerType = "series" And Image.Season = Nothing Then
                            seasonallpath = Image.Url
                            Exit For
                        End If
                    Next
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.Language = "en" And Image.BannerType = "series" And Image.Season = Nothing Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                    If seasonallpath = "" Then
                        For Each Image In artlist
                            If Image.BannerType = "series" And Image.Season = Nothing Then
                                seasonallpath = Image.Url
                                Exit For
                            End If
                        Next
                    End If
                End If

                If seasonallpath <> "" Then

                    Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season-all.tbn")
                    If Not IO.File.Exists(seasonpath) Or CheckBox6.CheckState = CheckState.Checked Then
                        Try
                            Dim buffer(4000000) As Byte
                            Dim size As Integer = 0
                            Dim bytesRead As Integer = 0
                            Dim thumburl As String = seasonallpath
                            Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                            Dim res As HttpWebResponse = req.GetResponse()
                            Dim contents As Stream = res.GetResponseStream()
                            Dim bytesToRead As Integer = CInt(buffer.Length)
                            While bytesToRead > 0
                                size = contents.Read(buffer, bytesRead, bytesToRead)
                                If size = 0 Then Exit While
                                bytesToRead -= size
                                bytesRead += size
                            End While
                            Dim fstrm As New FileStream(seasonpath, FileMode.OpenOrCreate, FileAccess.Write)
                            fstrm.Write(buffer, 0, bytesRead)
                            contents.Close()
                            fstrm.Close()
                        Catch ex As WebException
#If SilentErrorScream Then
                            Throw ex
#End If
                            'MsgBox("Error Downloading main poster from TVDB")
                        End Try
                    End If
                End If
            ElseIf Preferences.seasonall <> "none" And seasonallpath <> "" Then
                Dim seasonpath As String = BrokenShow.path.Replace(IO.Path.GetFileName(BrokenShow.path), "season-all.tbn")
                If Not IO.File.Exists(seasonpath) Then
                    Try
                        Dim buffer(4000000) As Byte
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0
                        Dim thumburl As String = seasonallpath
                        Dim req As HttpWebRequest = WebRequest.Create(thumburl)
                        Dim res As HttpWebResponse = req.GetResponse()
                        Dim contents As Stream = res.GetResponseStream()
                        Dim bytesToRead As Integer = CInt(buffer.Length)
                        While bytesToRead > 0
                            size = contents.Read(buffer, bytesRead, bytesToRead)
                            If size = 0 Then Exit While
                            bytesToRead -= size
                            bytesRead += size
                        End While
                        Dim fstrm As New FileStream(seasonpath, FileMode.OpenOrCreate, FileAccess.Write)
                        fstrm.Write(buffer, 0, bytesRead)
                        contents.Close()
                        fstrm.Close()
                    Catch ex As WebException
                        'MsgBox("Error Downloading main poster from TVDB")
                    End Try
                End If
            End If
        Catch
        End Try
        Call loadtvshow(BrokenShow.NfoFilePath)
        messbox.Close()

        TV_CleanFolderList()
    End Sub

End Class
