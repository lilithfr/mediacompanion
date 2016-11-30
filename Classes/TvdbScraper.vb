'Imports System.IO
Imports Alphaleonis.Win32.Filesystem
Imports System.Net
Imports System.Threading

Imports System.Xml



Public Class TVDBScraper
    Const SetDefaults = True
    Private Structure str_possibleshowlist
        Dim showtitle As String
        Dim showid As String
        Dim showbanner As String
        Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
            showtitle = ""
            showid = ""
            showbanner = ""
        End Sub
    End Structure

    Public Function GetPosterList(ByVal TvdbId As String, ByVal ReturnPoster As Boolean) As Tvdb.Banners
        If Not ReturnPoster Then Return Nothing

        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/banners.xml"
        Dim XmlFile As String

        XmlFile = Utilities.DownloadTextFiles(mirrorsurl)

        Dim BannerList As New Tvdb.Banners
        BannerList.LoadXml(XmlFile)
        Return BannerList
    End Function

    Public Function getposterlist(ByVal tvdbid As String, ByRef ListOfBanners As List(Of TvBanners)) As String
        Monitor.Enter(Me)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim wrGETURL As WebRequest
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/banners.xml"
            wrGETURL = WebRequest.Create(mirrorsurl)
            wrGETURL.Proxy = Utilities.MyProxy
            Dim objStream As IO.stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New IO.streamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim bannerslist As New XmlDocument
            bannerslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In bannerslist("Banners")

                Select Case thisresult.Name
                    Case "Banner"
                        Dim banner As New TvBanners
                        Dim bannerselection As XmlNode = Nothing
                        For Each bannerselection In thisresult.ChildNodes
                            Select Case bannerselection.Name
                                Case "id"
                                    banner.id = bannerselection.InnerXml
                                Case "BannerPath"
                                    banner.Url = "http://www.thetvdb.com/banners/" & bannerselection.InnerXml
                                    banner.SmallUrl = "http://www.thetvdb.com/banners/_cache/" & bannerselection.InnerXml
                                Case "BannerType"
                                    banner.BannerType = bannerselection.InnerXml
                                Case "BannerType2"
                                    banner.Resolution = bannerselection.InnerXml
                                Case "Language"
                                    banner.Language = bannerselection.InnerXml
                                Case "Season"
                                    banner.Season = bannerselection.InnerXml
                                Case "Rating"
                                    banner.Rating = bannerselection.InnerText.ToRating
                                Case ""
                            End Select
                        Next
                        ListOfBanners.Add(banner)
                End Select
            Next
            Return "ok"
        Catch ex As WebException
            Return ex.ToString
        Catch EX As Exception
            Return EX.ToString
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function getmirrors()
        Monitor.Enter(Me)
        Try
            Dim mirrors As New List(Of String)
            Dim xmlfile As String
            Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/mirrors.xml"
            Dim wrGETURL As WebRequest = WebRequest.Create(mirrorsurl)
            
            'wrGETURL = WebRequest.Create(mirrorsurl)
            wrGETURL.Proxy = Utilities.MyProxy
            'Dim myProxy As New WebProxy("myproxy", 80)
            'myProxy.BypassProxyOnLocal = True
            Dim objStream As IO.stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New IO.streamReader(objStream)
            xmlfile = objReader.ReadToEnd
            Dim mirrorslist As New XmlDocument
            'Try
            mirrorslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In mirrorslist("Mirrors")

                Select Case thisresult.Name
                    Case "Mirror"
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "mirrorpath"
                                    If mirrorselection.InnerText <> Nothing Then
                                        mirrors.Add(mirrorselection.InnerText)
                                    End If
                            End Select
                        Next
                End Select
            Next
            Return mirrors

        Catch EX As Exception
            Return EX.ToString
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function findshows(ByVal title As String, Optional ByVal mirror As String = "http://thetvdb.com")
        Monitor.Enter(Me)
        Dim possibleshows As New List(Of str_possibleshowlist)
        Dim xmlfile As String
        
        title = title.Replace(".", " ")  'Replace periods in foldernames with spaces (linux OS support)
        Dim mirrorsurl As String = "http://www.thetvdb.com/api/GetSeries.php?seriesname=" & title & "&language=all"
        Dim wrGETURL As WebRequest = WebRequest.Create(mirrorsurl)
        wrGETURL.Proxy = Utilities.MyProxy
        Dim objStream As IO.stream
        objStream = wrGETURL.GetResponse.GetResponseStream()
        Dim objReader As New IO.streamReader(objStream)
        xmlfile = objReader.ReadToEnd
        Dim showlist As New XmlDocument
        Try
            showlist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In showlist("Data")

                Select Case thisresult.Name
                    Case "Series"
                        Dim newshow As New str_possibleshowlist(SetDefaults)
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "seriesid"
                                    newshow.showid = mirrorselection.InnerXml
                                Case "SeriesName"
                                    newshow.showtitle = mirrorselection.InnerXml
                                Case "banner"
                                    newshow.showbanner = "http://www.thetvdb.com/banners/" & mirrorselection.InnerXml
                            End Select
                        Next
                        possibleshows.Add(newshow)
                End Select
            Next
            Dim returnstring As String
            Dim ok As Boolean = False
            If possibleshows.Count > 0 Then
                returnstring = "<allshows>"
                For Each show In possibleshows
                    If show.showid <> Nothing Then
                        returnstring = returnstring & "<show>"
                        returnstring = returnstring & "<showid>" & show.showid & "</showid>"
                        ok = True
                        If show.showtitle <> Nothing Then
                            returnstring = returnstring & "<showtitle>" & show.showtitle & "</showtitle>"
                        End If
                        If show.showbanner <> Nothing Then
                            returnstring = returnstring & "<showbanner>" & show.showbanner & "</showbanner>"
                        End If
                        returnstring = returnstring & "</show>"
                    End If
                Next
                returnstring = returnstring & "</allshows>"
            Else
                returnstring = "none"
            End If
            If ok = False Then returnstring = "none"
            Return returnstring
        Catch EX As Exception
            Return "error"
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function GetShow(ByVal TvdbId As String, ByVal Language As String, ByVal SeriesXmlPath As String) As Tvdb.ShowData
        
        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/" & Language & ".xml"
        Dim mirrorsurl2 As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/" & "/all/" & Language & ".xml"
        Dim success As Boolean = DownloadCache.Savexmltopath(mirrorsurl2, SeriesXmlPath, TvdbId & ".xml", True)
        
        Dim showlist As New Tvdb.ShowData
        If success Then     'If download series xml successful
            showlist.Load(SeriesXmlPath & TvdbId & ".xml")
        Else                'Else get just the Show's data.
            Dim xmlfile As String
            xmlfile = Utilities.DownloadTextFiles(mirrorsurl)
            If String.IsNullOrEmpty(xmlfile) Then
                showlist.FailedLoad = True
            Else
                showlist.LoadXml(xmlfile)
            End If
        End If
        If Not showlist.FailedLoad Then
            Dim strsplt As New List(Of String)
            Try
                strsplt.AddRange(showlist.Series(0).Genre.Value.Trim("|"c).Split("|"))
            Catch
            End Try
            If strsplt.count > 0 Then
                Dim NewGenre As String = ""
                For i = 0 To strsplt.count -1
                    If i = Pref.TvMaxGenres Then Exit For
                    If NewGenre = "" Then
                        NewGenre = strsplt.Item(i)
                    Else
                        NewGenre &= "|" & strsplt.Item(i)
                    End If
                Next
                showlist.Series(0).Genre.Value = NewGenre
            End If
        End If
        Return showlist
    End Function

    Public Function GetSeriesXml(ByVal TvdbId As String, ByVal Lan As String, ByVal SeriesXmlPath As String) As Boolean
        Dim url As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/" & "/all/" & Lan & ".xml"
        Return DownloadCache.Savexmltopath(url, SeriesXmlPath, TvdbId & ".xml", True)
    End Function

    Public Function GetActors(ByVal TvdbId As String, Optional ByVal maxactors As Integer = 9999) As List(Of str_MovieActors) 'Tvdb.Actors
        If maxactors = 9999 Then 
            maxactors= Pref.maxactors
        End If
        Dim results As New List(Of str_MovieActors)
        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & TvdbId & "/actors.xml"

        Dim xmlfile As String
        xmlfile = Utilities.DownloadTextFiles(mirrorsurl)
        Utilities.CheckForXMLIllegalChars(xmlfile)
        Dim showlist As New Tvdb.Actors
        'Try
        showlist.LoadXml(xmlfile)
        For Each Mc In showlist.items
            Dim actor As str_MovieActors = New str_MovieActors
            actor.actorid = Mc.Id.value
            actor.actorname = Utilities.cleanSpecChars(Mc.Name.Value).Trim
            Dim newstring As String
            newstring = Mc.Role.Value
            newstring = newstring.TrimEnd("|")
            newstring = newstring.TrimStart("|")
            newstring = newstring.Replace("|", ", ")
            actor.actorrole = newstring.TrimStart.TrimEnd
            If Mc.Image.Value <> "" Then
                actor.actorthumb = "http://thetvdb.com/banners/_cache/" & Mc.Image.Value
            Else
                actor.actorthumb = ""
            End If
            actor.order = Mc.SortOrder.Value
            'actor.actorthumb = Mc.Image.Value
            results.Add(actor)
        Next

        'Return showlist
        Return results
    End Function

    'Public Function getshow(ByVal tvdbid As String, ByVal language As String)
    '    Monitor.Enter(Me)
    '    Try
    '        Dim tvshowdetails As String = "<fulltvshow>"
    '        Dim xmlfile As String
    '        Dim wrGETURL As WebRequest
    '        Dim episodeguideurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
    '        Dim mirrorsurl As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & language & ".xml"
    '        wrGETURL = WebRequest.Create(mirrorsurl)
    '        Dim myProxy As New WebProxy("myproxy", 80)
    '        myProxy.BypassProxyOnLocal = True
    '        Dim objStream As IO.stream
    '        objStream = wrGETURL.GetResponse.GetResponseStream()
    '        Dim objReader As New IO.streamReader(objStream)
    '        xmlfile = objReader.ReadToEnd
    '        Dim showlist As New XmlDocument
    '        'Try
    '        showlist.LoadXml(xmlfile)
    '        Dim thisresult As XmlNode = Nothing
    '        For Each thisresult In showlist("Data")

    '            Select Case thisresult.Name
    '                Case "Series"
    '                    Dim newshow As New str_possibleshowlist(SetDefaults)
    '                    Dim mirrorselection As XmlNode = Nothing
    '                    For Each mirrorselection In thisresult.ChildNodes
    '                        Select Case mirrorselection.Name
    '                            Case "SeriesName"
    '                                tvshowdetails = tvshowdetails & "<title>" & mirrorselection.InnerXml & "</title>"
    '                            Case "ContentRating"
    '                                tvshowdetails = tvshowdetails & "<mpaa>" & mirrorselection.InnerXml & "</mpaa>"
    '                            Case "FirstAired"
    '                                tvshowdetails = tvshowdetails & "<premiered>" & mirrorselection.InnerXml & "</premiered>"
    '                            Case "Genre"
    '                                tvshowdetails = tvshowdetails & "<genre>" & mirrorselection.InnerXml & "</genre>"
    '                            Case "IMDB_ID"
    '                                tvshowdetails = tvshowdetails & "<imdbid>" & mirrorselection.InnerXml & "</imdbid>"
    '                            Case "Network"
    '                                tvshowdetails = tvshowdetails & "<studio>" & mirrorselection.InnerXml & "</studio>"
    '                            Case "Overview"
    '                                tvshowdetails = tvshowdetails & "<plot>" & mirrorselection.InnerXml & "</plot>"
    '                            Case "Rating"
    '                                tvshowdetails = tvshowdetails & "<rating>" & mirrorselection.InnerXml & "</rating>"
    '                            Case "Runtime"
    '                                tvshowdetails = tvshowdetails & "<runtime>" & mirrorselection.InnerXml & "</runtime>"
    '                            Case "banner"
    '                                tvshowdetails = tvshowdetails & "<banner>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</banner>"
    '                            Case "fanart"
    '                                tvshowdetails = tvshowdetails & "<fanart>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</fanart>"
    '                            Case "poster"
    '                                tvshowdetails = tvshowdetails & "<poster>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</poster>"
    '                        End Select
    '                    Next
    '            End Select
    '        Next

    '        tvshowdetails = tvshowdetails & "<episodeguideurl>" & episodeguideurl & "</episodeguideurl>"



    '        mirrorsurl = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/actors.xml"
    '        wrGETURL = WebRequest.Create(mirrorsurl)
    '        Dim objStream2 As IO.stream
    '        objStream2 = wrGETURL.GetResponse.GetResponseStream()
    '        Dim objReader2 As New IO.streamReader(objStream2)
    '        xmlfile = objReader2.ReadToEnd
    '        Dim showlist2 As New XmlDocument
    '        'Try
    '        showlist2.LoadXml(xmlfile)
    '        thisresult = Nothing
    '        For Each thisresult In showlist2("Actors")

    '            Select Case thisresult.Name
    '                Case "Actor"
    '                    tvshowdetails = tvshowdetails & "<actor>"
    '                    Dim newshow As New str_possibleshowlist(SetDefaults)
    '                    Dim mirrorselection As XmlNode = Nothing
    '                    For Each mirrorselection In thisresult.ChildNodes
    '                        Select Case mirrorselection.Name
    '                            Case "id"
    '                                tvshowdetails = tvshowdetails & "<actorid>" & mirrorselection.InnerXml & "</actorid>"
    '                            Case "Image"
    '                                If mirrorselection.InnerXml <> Nothing Then
    '                                    If mirrorselection.InnerXml <> "" Then
    '                                        tvshowdetails = tvshowdetails & "<thumb>" & "http://thetvdb.com/banners/" & mirrorselection.InnerXml & "</thumb>"
    '                                    End If
    '                                End If
    '                            Case "Name"
    '                                tvshowdetails = tvshowdetails & "<name>" & mirrorselection.InnerXml & "</name>"
    '                            Case "Role"
    '                                If mirrorselection.InnerXml <> Nothing Then
    '                                    If mirrorselection.InnerXml <> "" Then
    '                                        tvshowdetails = tvshowdetails & "<role>" & mirrorselection.InnerXml & "</role>"
    '                                    End If
    '                                End If
    '                        End Select
    '                    Next
    '                    tvshowdetails = tvshowdetails & "</actor>"
    '            End Select
    '        Next

    '        tvshowdetails = tvshowdetails & "</fulltvshow>"

    '        Return tvshowdetails
    '    Catch
    '        Return "!!!Error!!!"
    '    Finally
    '        Monitor.Exit(Me)
    '    End Try


    'End Function


    Public Function getepisode(ByVal tvdbid As String, ByVal sortorder As String, ByVal seasonno As String, ByVal episodeno As String, ByVal language As String, Optional ByVal forcedownload As Boolean = False)
        Monitor.Enter(Me)
        Dim episodestring As String = ""
        Dim episodeurl As String = ""
        Try
            'http://thetvdb.com/api/6E82FED600783400/series/70726/default/1/1/en.xml
            Dim SeriesXmlPath As String = Utilities.SeriesXmlPath

            Dim xmlfile As String = Nothing
            If language.ToLower.IndexOf(".xml") = -1 Then
                language = language & ".xml"
            End If
            episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seasonno & "/" & episodeno & "/" & language
            
            'First try seriesxml data
            'check if present, download if not
            Dim gotseriesxml As Boolean = False
            Dim url As String = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language
            Dim xmlfile2 As String = SeriesXmlPath & tvdbid & ".xml"
            Dim SeriesInfo As New Tvdb.ShowData
            If Not File.Exists(xmlfile2) Then
                gotseriesxml = DownloadCache.Savexmltopath(url, SeriesXmlPath, tvdbid & ".xml", True)
            Else
                'Check series xml isn't older than Five days.  If so, re-download it.
                Dim dtCreationDate As DateTime = File.GetLastWriteTime(xmlfile2) 
                Dim datenow As DateTime = Date.Now()
                Dim dif As Long = DateDiff(DateInterval.Day, dtCreationDate, datenow)
                If dif > 5 Then
                    gotseriesxml = DownloadCache.Savexmltopath(url, SeriesXmlPath, tvdbid & ".xml", True)
                Else
                    gotseriesxml = True
                End If
            End If
        
            If Not gotseriesxml then
                episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seasonno & "/" & episodeno & "/" & language
                xmlfile = Utilities.DownloadTextFiles(episodeurl)
            Else
                SeriesInfo.Load(xmlfile2)
                Dim gotEpxml As Boolean = False
                'check episode is present in seriesxml file, else, re-download it (update to latest)
                If sortorder = "default" Then
                    For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                        If NewEpisode.EpisodeNumber.Value = episodeno AndAlso NewEpisode.SeasonNumber.Value = seasonno Then
                            xmlfile = NewEpisode.Node.ToString 
                            xmlfile = "<Data>" & xmlfile & "</Data>"
                            gotEpxml = True
                            Exit For
                        End If
                    Next
                ElseIf sortorder = "dvd" Then
                    For Each NewEpisode As Tvdb.Episode In SeriesInfo.Episodes
                        If NewEpisode.DvdEpisodeNumber.value = (episodeno & ".0") AndAlso NewEpisode.DvdSeason.Value = seasonno Then
                            xmlfile = NewEpisode.Node.ToString 
                            xmlfile = "<Data>" & xmlfile & "</Data>"
                            gotEpxml = True
                            Exit For
                        End If
                    Next
                End If
                ' Finally, if not in seriesxml file, go old-school
                If Not gotEpxml Then
                    episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seasonno & "/" & episodeno & "/" & language
                    xmlfile = Utilities.DownloadTextFiles(episodeurl)
                End If
            End If

            episodestring = "<episodedetails>"
            episodestring = episodestring & "<url>" & episodeurl & "</url>"
            Dim mirrorslist As New XmlDocument
            'Try
            mirrorslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In mirrorslist("Data")

                Select Case thisresult.Name
                    Case "Episode"
                        Dim mirrorselection As XmlNode = Nothing
                        For Each mirrorselection In thisresult.ChildNodes
                            Select Case mirrorselection.Name
                                Case "EpisodeName"
                                    episodestring = episodestring & "<title>" & mirrorselection.InnerXml & "</title>"
                                Case "FirstAired"
                                    episodestring = episodestring & "<premiered>" & mirrorselection.InnerXml & "</premiered>"
                                Case "GuestStars"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    Try
                                        tempstring = tempstring.TrimStart("|")
                                        tempstring = tempstring.TrimEnd("|")
                                        Dim tvtempstring2 As String
                                        Dim tvtempint As Integer
                                        Dim a() As String
                                        Dim j As Integer
                                        tvtempstring2 = ""
                                        a = tempstring.Split("|")
                                        tvtempint = a.GetUpperBound(0)
                                        tvtempstring2 = a(0)
                                        If tvtempint >= 0 Then
                                            For j = 0 To tvtempint
                                                Try
                                                    episodestring = episodestring & "<actor>" & "<name>" & a(j) & "</name></actor>"
                                                Catch
                                                End Try
                                            Next
                                        End If
                                    Catch
                                    End Try
                                Case "Director"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.TrimEnd("|")
                                    episodestring = episodestring & "<director>" & tempstring & "</director>"
                                Case "Writer"
                                    Dim tempstring As String = mirrorselection.InnerXml
                                    tempstring = tempstring.TrimStart("|")
                                    tempstring = tempstring.TrimEnd("|")
                                    episodestring = episodestring & "<credits>" & tempstring & "</credits>"
                                Case "Overview"
                                    episodestring = episodestring & "<plot>" & mirrorselection.InnerXml & "</plot>"
                                Case "Rating"
                                    episodestring = episodestring & "<rating>" & mirrorselection.InnerXml & "</rating>"
                                Case "RatingCount"
                                    episodestring = episodestring & "<ratingcount>" & mirrorselection.InnerXml & "</ratingcount>"
                                Case "id"
                                    episodestring = episodestring & "<uniqueid>" & mirrorselection.InnerXml & "</uniqueid>"
                                Case "IMDB_ID"
                                    episodestring = episodestring & "<imdbid>" & mirrorselection.InnerXml & "</imdbid>"
                                Case "filename"
                                    episodestring = episodestring & "<thumb>http://www.thetvdb.com/banners/" & mirrorselection.InnerXml & "</thumb>"
                                Case "airsbefore_episode"
                                    episodestring = episodestring & "<displayepisode>" & mirrorselection.InnerXml & "</displayepisode>"
                                Case "airsbefore_season"
                                    episodestring = episodestring & "<displayseason>" & mirrorselection.InnerXml & "</displayseason>"
                            End Select
                        Next
                End Select
            Next
            episodestring = episodestring & "</episodedetails>"
            Return episodestring
        Catch ex As Exception
            Return "ERROR - <url>" & episodeurl & "</url>"
        Finally
            Monitor.Exit(Me)
        End Try

    End Function

    Public Function getepisodefromxml(ByVal tvdbid As String, ByVal sortorder As String, ByVal seriesno As String, ByVal episodeno As String, ByVal language As String, Optional ByVal forcedownload As Boolean = False) As Tvdb.Episode
        Monitor.Enter(Me)
        Dim episodeurl As String = ""
        Dim thisepisode As New tvdb.Episode  
        Try
            'http://thetvdb.com/api/6E82FED600783400/series/70726/default/1/1/en.xml

            Dim xmlfile As String
            If language.ToLower.IndexOf(".xml") = -1 Then language = language & ".xml"
            episodeurl = "http://thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/" & sortorder & "/" & seriesno & "/" & episodeno & "/" & language

            xmlfile = Utilities.DownloadTextFiles(episodeurl, forcedownload) 'this function has gzip detection in it 
            Dim xmlOK As Boolean = Utilities.CheckForXMLIllegalChars(xmlfile)
            Dim mirrorslist As New XmlDocument
            'Try
            mirrorslist.LoadXml(xmlfile)
            Dim thisresult As XmlNode = Nothing
            For Each thisresult In mirrorslist("Data")
                Select Case thisresult.Name
                    Case "Episode"
                        Dim thisresult2 As XmlNode = Nothing
                        For Each thisresult2 In thisresult.ChildNodes
                            Select Case thisresult2.Name
                                Case "EpisodeName"
                                    thisepisode.EpisodeName.Value  = thisresult2.InnerXml
                                Case "FirstAired"
                                    thisepisode.FirstAired.Value  = thisresult2.InnerXml
                                Case "GuestStars"
                                    thisepisode.GuestStars.Value = thisresult2.InnerXml
                                Case "Director"
                                    thisepisode.Director.Value = thisresult2.InnerXml
                                Case "Writer"
                                    thisepisode.Writer.Value = thisresult2.InnerXml
                                Case "Overview"
                                    thisepisode.Overview.Value = thisresult2.InnerXml
                                Case "Rating"
                                    thisepisode.Rating.Value = thisresult2.InnerXml
                                Case "RatingCount"
                                    thisepisode.Votes.Value = thisresult2.InnerXml
                                Case "id"
                                    thisepisode.id.Value = thisresult2.InnerXml
                                Case "filename"
                                    thisepisode.ThumbNail.Value = "http://www.thetvdb.com/banners/" & thisresult2.InnerXml
                            End Select
                        Next
                End Select
            Next
        Catch ex As Exception
            thisepisode.FailedLoad = True
        Finally
            Monitor.Exit(Me)
        End Try
        Return thisepisode 
    End Function

End Class
