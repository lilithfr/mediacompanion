Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class MediaInfoExport

    Dim templateList As New List(Of str_HTMLTemplate)
    Dim fullhtmlstring As String
    Dim htmlNfoFunction As New WorkingWithNfoFiles

    Public Sub addTemplates()
        templateList.Clear()
        Dim folder As String = Path.Combine(Preferences.applicationPath, "html_templates\")
        Dim dir_info As New DirectoryInfo(folder)
        Dim fs_infos() As IO.FileInfo = dir_info.GetFiles("*.txt", SearchOption.TopDirectoryOnly)
        For Each info In fs_infos
            Try
                Dim fullstring As String
                Dim htmlType As String = ""
                Dim cfg2 As IO.StreamReader = File.OpenText(info.FullName)
                fullstring = cfg2.ReadToEnd
                If fullstring.ToLower.IndexOf("<<mc html page>>") <> -1 And fullstring.ToLower.IndexOf("<</mc html page>>") <> -1 Then
                    htmlType = "movie"
                ElseIf fullstring.ToLower.IndexOf("<<mc tv html page>>") <> -1 And fullstring.ToLower.IndexOf("<</mc tv html page>>") <> -1 Then
                    htmlType = "tv"
                Else
                    Continue For
                End If
                Dim tempstring As String = fullstring.Substring(fullstring.IndexOf("<title>") + 7, fullstring.IndexOf("</title>") - 7)
                Dim template As New str_HTMLTemplate(True)
                Dim add As Boolean = True
                For Each temp In templateList
                    If temp.title = tempstring Then
                        add = False
                        Exit For
                    End If
                Next
                If add = True Then
                    template.title = tempstring
                    template.path = info.FullName
                    template.body = fullstring
                    If htmlType = "movie" Then
                        Form1.OutputMovieListAsHTMLToolStripMenuItem.DropDownItems.Add(template.title)
                    Else
                        Form1.OutputTVShowsAsHTMLToolStripMenuItem.DropDownItems.Add(template.title)
                    End If
                    templateList.Add(template)
                End If
            Catch ex As Exception
                Dim t As Integer = 0
            End Try
        Next
    End Sub

    Private Sub createDocument(ByVal htmlType As String, ByVal savePath As String, ByVal mediaCollection As Object)
        Dim frmhtmloutput As New frmDialog1
        frmhtmloutput.Label3.Text = "Exporting Media Info"
        frmhtmloutput.Label4.Text = ""
        frmhtmloutput.Label3.Refresh()
        frmhtmloutput.Label4.Refresh()
        Application.DoEvents()
        frmhtmloutput.Show()
        Dim tempstring As String = ""
        Dim cssbody As String
        Dim csspath As String
        Dim counter As Integer = 0

        If fullhtmlstring.IndexOf("<<css>>") <> -1 And fullhtmlstring.IndexOf("<</css>>") <> -1 Then
            tempstring = fullhtmlstring.Substring(fullhtmlstring.IndexOf("<<css>>") + 9, fullhtmlstring.IndexOf("<</css>>") - fullhtmlstring.IndexOf("<<css>>") - 9)
            If tempstring.IndexOf("<filename>") <> -1 And tempstring.IndexOf("</filename>") <> -1 Then
                Dim tempstring2 As String
                tempstring2 = tempstring.Substring(tempstring.IndexOf("<filename>") + 10, tempstring.IndexOf("</filename>") - tempstring.IndexOf("<filename>") - 10)
                csspath = savePath.Replace(IO.Path.GetFileName(savePath), tempstring2)
                cssbody = tempstring.Substring(tempstring.IndexOf("</filename>") + 13, tempstring.Length - tempstring.IndexOf("</filename>") - 13)
                Try
                    Dim objWriter2 As New System.IO.StreamWriter(csspath, False, Encoding.UTF8)
                    objWriter2.Write(cssbody)
                    objWriter2.Close()
                Catch ex As Exception

                Finally

                End Try
            End If

        End If
        Dim temphtml As String = ""
        Dim headerTagPresent As Boolean = False
        Dim tempBody As String = ""
        Dim bodyTagPresent As Boolean = False
        Dim mediaTagpresent As Boolean = False
        Dim overallcancel As Boolean = False
        Dim pathstring As String = ""
        Dim imageFolder As String = If(htmlType = "Movies", "images\", "tvimages\")

        If fullhtmlstring.ToLower.IndexOf("<<header>>") <> -1 And fullhtmlstring.ToLower.IndexOf("<</header>>") <> -1 Then
            headerTagPresent = True
            tempstring = fullhtmlstring.Substring(fullhtmlstring.IndexOf("<<header>>") + 12, (fullhtmlstring.IndexOf("<</header>>") - fullhtmlstring.IndexOf("<<header>>")) - 12)
            If htmlType = "Movies" Then
                tempstring = getTagsMovies(tempstring, mediaCollection(0), counter)
            Else
                tempstring = getTagsTV(tempstring, Cache.TvCache.Shows(0), counter, "!HEADER!")
            End If

            temphtml = "<html><head>" & tempstring & "</head>"
        End If

        If fullhtmlstring.IndexOf("<<smallimage>>") <> -1 Or fullhtmlstring.IndexOf("<<createimage") <> -1 Then
            pathstring = savePath.Replace(IO.Path.GetFileName(savePath), "")
            pathstring = pathstring & imageFolder
            Dim fso As New IO.DirectoryInfo(pathstring)
            If fso.Exists = False Then
                IO.Directory.CreateDirectory(pathstring)
            End If
        End If

        If fullhtmlstring.ToLower.IndexOf("<<body>>") <> -1 And fullhtmlstring.ToLower.IndexOf("<</body>>") <> -1 Then
            bodyTagPresent = True
            temphtml = temphtml & "<body>"
            tempstring = fullhtmlstring.Substring(fullhtmlstring.ToLower.IndexOf("<<body>>") + 8, fullhtmlstring.ToLower.IndexOf("<</body>>") - (fullhtmlstring.ToLower.IndexOf("<<body>>") + 8))
        Else
            tempstring = fullhtmlstring.Substring(fullhtmlstring.ToLower.IndexOf("<<mc html page>>") + 17, fullhtmlstring.ToLower.IndexOf("<</mc html page>>") - (fullhtmlstring.ToLower.IndexOf("<<mc html page>>") + 17))
        End If

        If fullhtmlstring.ToLower.IndexOf("<<media_item>>") <> -1 And fullhtmlstring.ToLower.IndexOf("<</media_item>>") <> -1 Then
            mediaTagpresent = True
            temphtml = temphtml & tempstring.Substring(0, tempstring.ToLower.IndexOf("<<media_item>>")).Trim
            tempBody = tempstring.Substring(tempstring.ToLower.IndexOf("<</media_item>>") + 15, tempstring.Length - (tempstring.ToLower.IndexOf("<</media_item>>") + 15))
            tempstring = fullhtmlstring.Substring(fullhtmlstring.ToLower.IndexOf("<<media_item>>") + 14, fullhtmlstring.ToLower.IndexOf("<</media_item>>") - (fullhtmlstring.ToLower.IndexOf("<<media_item>>") + 14)).TrimEnd
        End If

        For Each mediaItem In mediaCollection
            If frmhtmloutput.IsDisposed Then
                MsgBox("Operation Canceled")
                overallcancel = True
                Exit For
            End If
            frmhtmloutput.Label3.Text = "Processing: " & If(htmlType = "Movies", mediaItem.title, mediaItem.title.Value)
            Dim tempint As Integer = mediaCollection.Count - (counter + 1)
            frmhtmloutput.Label4.Text = tempint.ToString & " " & htmlType & " Remaining"
            frmhtmloutput.Label3.Refresh()
            frmhtmloutput.Label4.Refresh()
            Application.DoEvents()
            Try
                If htmlType = "Movies" Then
                    temphtml = temphtml & getTagsMovies(tempstring, mediaItem, counter, pathstring)
                Else
                    temphtml = temphtml & getTagsTV(tempstring, mediaItem, counter, pathstring)
                End If
            Catch ex As Exception
#If SilentErrorScream Then
                    Throw ex
#End If
            End Try
            counter += 1
        Next
        If overallcancel = False Then
            If mediaTagpresent Then
                temphtml = temphtml & tempBody
            End If
            If bodyTagPresent Then
                temphtml = temphtml & "</body>"
            End If
            If headerTagPresent Then
                temphtml = temphtml & "</html>"
            End If

            Try
                Dim objWriter As New System.IO.StreamWriter(savePath, False, Encoding.UTF8)
                objWriter.Write(temphtml)
                objWriter.Close()
                frmhtmloutput.Close()
                Dim tempint As Integer = MessageBox.Show("Your list is now complete" & vbCrLf & " Do You wish to view it now?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tempint = DialogResult.Yes Then
                    Process.Start(savePath)
                End If
            Catch ex As Exception
                frmhtmloutput.Close()
                MsgBox(ex.ToString)
            Finally

            End Try
        End If
    End Sub

    Private Function getTagsMovies(ByVal text As String, ByVal movie As str_ComboList, ByVal counter As Integer, Optional ByVal moviecount As Integer = 0, Optional ByVal thumbpath As String = "")
        Dim tokenCollection As MatchCollection
        Dim tokenRegExp As New Regex("<<[\w_:]+>>")
        tokenCollection = tokenRegExp.Matches(text)
        Dim token As Match

        For Each token In tokenCollection
            Dim strNFOprop As String = ""
            Dim valToken As String = token.Value.Substring(2, token.Value.Length - 4)
            Dim tokenInstr() As String = valToken.Split(":")
            Select Case tokenInstr(0)
                Case "smallimage", "createimage"
                    If thumbpath <> "" Then
                        Dim origImage = Preferences.GetPosterPath(movie.fullpathandfilename)
                        Try
                            If (tokenInstr(UBound(tokenInstr)) <> "nopath") Then strNFOprop &= "images/"
                            strNFOprop &= Utilities.createImage(origImage, If(tokenInstr(0) = "createimage" And tokenInstr.Length > 1, Val(tokenInstr(1)), 200), thumbpath)
                        Catch ex As Exception
                            MsgBox(ex.ToString)
                        End Try
                    End If

                Case "moviecount"
                    strNFOprop = If(moviecount, moviecount.ToString, "0000")

                Case "counter"
                    strNFOprop = counter.ToString

                Case "imdb_id"
                    strNFOprop = If(movie.id <> Nothing, movie.id, "")

                Case "imdb_url"
                    strNFOprop = If(movie.id <> Nothing, Preferences.imdbmirror & "title/" & movie.id & "/", Preferences.imdbmirror)

                Case "title"
                    strNFOprop = If(movie.title <> Nothing, movie.title, "")

                Case "movieyear"
                    strNFOprop = If(movie.year <> Nothing, movie.year, "0000")

                Case "movietitleandyear"
                    strNFOprop = If(movie.title <> Nothing, movie.title, "") & " (" & If(movie.year <> Nothing, movie.year, "0000") & ")"

                Case "rating"
                    strNFOprop = If(movie.rating <> Nothing, movie.rating, "")

                Case "runtime"
                    strNFOprop = If(movie.runtime <> Nothing, movie.runtime, "")

                Case "outline"
                    strNFOprop = If(movie.outline <> Nothing, movie.outline, "")

                Case "fullpathandfilename"
                    strNFOprop = If(movie.fullpathandfilename <> Nothing, movie.fullpathandfilename, "")

                    ' The tokens "fullplot", "director", "stars", "writer", "moviegenre" and "releasedate" are included for backwards compatibility
                Case "fullplot", "director", "stars", "writer", "moviegenre", "releasedate", "format", "filename", "nfo"
                    Dim newplotdetails As New FullMovieDetails
                    newplotdetails = htmlNfoFunction.mov_NfoLoadFull(movie.fullpathandfilename)
                    If Not IsNothing(newplotdetails) Then
                        If tokenInstr(0) = "fullplot" Then
                            strNFOprop = newplotdetails.fullmoviebody.plot
                        End If
                        If tokenInstr(0) = "director" Then
                            strNFOprop = newplotdetails.fullmoviebody.director
                        End If
                        If tokenInstr(0) = "stars" Then
                            strNFOprop = newplotdetails.fullmoviebody.stars
                        End If
                        If tokenInstr(0) = "writer" Then
                            strNFOprop = newplotdetails.fullmoviebody.credits
                        End If
                        If tokenInstr(0) = "moviegenre" Then
                            strNFOprop = newplotdetails.fullmoviebody.genre
                        End If
                        If tokenInstr(0) = "releasedate" Then
                            strNFOprop = newplotdetails.fullmoviebody.premiered
                        End If
                        If tokenInstr(0) = "format" Then
                            Dim idxEndParam As Integer = tokenInstr.Length - 1
                            If idxEndParam > 0 Then
                                Dim arrlstFormat As New ArrayList
                                Dim separator As String = " "
                                For i = 0 To idxEndParam - 1
                                    Select Case tokenInstr(i + 1).ToLower
                                        Case "container"
                                            Dim container As String = newplotdetails.filedetails.filedetails_video.Container.Value
                                            If container <> Nothing And container <> "" Then
                                                If tokenInstr(i + 1).ToLower <> tokenInstr(i + 1) Then container = container.ToUpper
                                                arrlstFormat.Add(container.TrimStart("."))
                                            End If
                                        Case "source"
                                            Dim source As String = newplotdetails.fullmoviebody.source
                                            If source <> Nothing And source <> "" Then
                                                arrlstFormat.Add(source)
                                            End If
                                        Case "resolution"
                                            Dim width, height As Integer
                                            width = newplotdetails.filedetails.filedetails_video.Width.Value
                                            height = newplotdetails.filedetails.filedetails_video.Height.Value
                                            If width AndAlso height Then
                                                If (width <= 720 And height <= 480) Then
                                                    arrlstFormat.Add("480")
                                                ElseIf (width <= 768 And height <= 576) Then
                                                    arrlstFormat.Add("576")
                                                ElseIf (width <= 960 And height <= 544) Then
                                                    arrlstFormat.Add("540")
                                                ElseIf (width <= 1280 And height <= 720) Then
                                                    arrlstFormat.Add("720")
                                                Else
                                                    arrlstFormat.Add("1080")
                                                End If
                                            End If
                                    End Select
                                Next
                                Dim arrFormat As String() = CType(arrlstFormat.ToArray(GetType(String)), String())
                                strNFOprop = String.Join(separator, arrFormat)
                            Else
                                strNFOprop = newplotdetails.filedetails.filedetails_video.Container.Value
                            End If
                        End If
                        If tokenInstr(0) = "filename" Then
                            Dim tempFileAndPath As String = Utilities.GetFileName(movie.fullpathandfilename)
                            Dim idxEndParam As Integer = tokenInstr.Length - 1
                            If idxEndParam > 0 Then
                                Dim arrlstFormat As New ArrayList
                                Dim separator As String = ""
                                Dim tempFilePathOnly As String = String.Concat(IO.Path.GetDirectoryName(tempFileAndPath), IO.Path.DirectorySeparatorChar)
                                Dim tempRoot As String = IO.Path.GetPathRoot(tempFileAndPath)
                                For i = 0 To idxEndParam - 1
                                    Select Case tokenInstr(i + 1).ToLower
                                        Case "root"
                                            arrlstFormat.Add(tempRoot)
                                        Case "path"
                                            arrlstFormat.Add(tempFilePathOnly.Replace(tempRoot, ""))
                                        Case "file"
                                            arrlstFormat.Add(IO.Path.GetFileNameWithoutExtension(tempFileAndPath))
                                        Case "ext"
                                            arrlstFormat.Add(IO.Path.GetExtension(tempFileAndPath))
                                    End Select
                                Next
                                Dim arrFormat As String() = CType(arrlstFormat.ToArray(GetType(String)), String())
                                strNFOprop = String.Join(separator, arrFormat)
                            Else
                                strNFOprop = tempFileAndPath
                            End If
                        End If
                        If tokenInstr(0) = "nfo" Then
                            Try
                                Select Case tokenInstr(1)

                                    Case "file"
                                        Select Case tokenInstr(2)
                                            Case "video"
                                                Select Case tokenInstr(3)
                                                    Case "width"
                                                        strNFOprop = newplotdetails.filedetails.filedetails_video.Width.Value
                                                    Case "height"
                                                        strNFOprop = newplotdetails.filedetails.filedetails_video.Height.Value
                                                    Case "aspect"
                                                        strNFOprop = newplotdetails.filedetails.filedetails_video.Aspect.Value
                                                    Case "codec"
                                                        strNFOprop = newplotdetails.filedetails.filedetails_video.Codec.Value
                                                    Case "duration"
                                                        strNFOprop = newplotdetails.filedetails.filedetails_video.DurationInSeconds.Value
                                                    Case "container"
                                                        strNFOprop = newplotdetails.filedetails.filedetails_video.Container.Value
                                                    Case Else
                                                        strNFOprop = tokenInstr(3) & "not supported"
                                                End Select
                                                'strNFOprop = CallByName(newplotdetails.filedetails.filedetails_video, tokenInstr(3), vbGet)
                                            Case "audio"
                                                Dim i As Integer = 1
                                                For Each audioStream In newplotdetails.filedetails.filedetails_audio
                                                    Select Case tokenInstr(3)
                                                        Case "language"
                                                            strNFOprop &= audioStream.Language.Value
                                                        Case "channels"
                                                            strNFOprop &= audioStream.Channels.Value
                                                        Case "bitrate"
                                                            strNFOprop &= audioStream.Bitrate.Value
                                                        Case "codec"
                                                            strNFOprop &= audioStream.Codec.Value
                                                    End Select
                                                    'strNFOprop = strNFOprop & CallByName(audioStream, tokenInstr(3), vbGet)
                                                    If (newplotdetails.filedetails.filedetails_audio.Count > 1 And i <> newplotdetails.filedetails.filedetails_audio.Count) Then
                                                        strNFOprop = strNFOprop & " / "
                                                    End If
                                                    i += 1
                                                Next
                                            Case "subtitles"
                                                Dim i As Integer = 1
                                                For Each subLang In newplotdetails.filedetails.filedetails_subtitles
                                                    Select Case tokenInstr(3)
                                                        Case "language"
                                                            strNFOprop &= subLang.Language.Value
                                                    End Select
                                                    'strNFOprop = strNFOprop & CallByName(subLang, tokenInstr(3), vbGet)
                                                    If (newplotdetails.filedetails.filedetails_subtitles.Count > 1 And i <> newplotdetails.filedetails.filedetails_subtitles.Count) Then
                                                        strNFOprop = strNFOprop & " / "
                                                    End If
                                                    i += 1
                                                Next
                                        End Select
                                    Case "title"
                                        strNFOprop = newplotdetails.fullmoviebody.title
                                    Case "originaltitle"
                                        strNFOprop = newplotdetails.fullmoviebody.originaltitle
                                    Case "sorttitle"
                                        strNFOprop = newplotdetails.fullmoviebody.sortorder
                                    Case "year"
                                        strNFOprop = newplotdetails.fullmoviebody.year
                                    Case "set"
                                        strNFOprop = newplotdetails.fullmoviebody.movieset
                                    Case "createdate", "premiered"
                                        Dim newDate, localDatePattern As String
                                        If tokenInstr(1) = "createdate" Then
                                            newDate = newplotdetails.fileinfo.createdate
                                            localDatePattern = Preferences.datePattern
                                        Else
                                            newDate = newplotdetails.fullmoviebody.premiered
                                            localDatePattern = Preferences.nfoDatePattern
                                        End If
                                        Try
                                            Dim result As Date
                                            result = DateTime.ParseExact(newDate, localDatePattern, Nothing)
                                            If tokenInstr.Length > 2 Then
                                                strNFOprop = Format(result, tokenInstr(2)).ToString
                                                If tokenInstr.Length > 3 Then
                                                    Dim separator As String = "!"
                                                    Select Case tokenInstr(3).ToLower
                                                        Case "space"
                                                            separator = " "
                                                        Case "colon"
                                                            separator = ":"
                                                        Case "dash"
                                                            separator = "-"
                                                        Case "slash"
                                                            separator = "/"
                                                    End Select
                                                    strNFOprop = strNFOprop.Replace("_", separator)
                                                End If
                                            Else
                                                strNFOprop = Format(result, Preferences.datePattern).ToString
                                            End If
                                        Catch ex As Exception
                                            strNFOprop = "Error in date format"
                                        End Try
                                    Case "rating"
                                        strNFOprop = newplotdetails.fullmoviebody.rating
                                    Case "votes"
                                        strNFOprop = newplotdetails.fullmoviebody.votes
                                    Case "top250"
                                        strNFOprop = newplotdetails.fullmoviebody.top250
                                    Case "outline"
                                        strNFOprop = newplotdetails.fullmoviebody.outline
                                    Case "plot"
                                        strNFOprop = newplotdetails.fullmoviebody.plot
                                    Case "tagline"
                                        strNFOprop = newplotdetails.fullmoviebody.tagline
                                    Case "country"
                                        strNFOprop = newplotdetails.fullmoviebody.country
                                    Case "runtime"
                                        strNFOprop = newplotdetails.fullmoviebody.runtime
                                    Case "mpaa"
                                        strNFOprop = newplotdetails.fullmoviebody.mpaa
                                    Case "genre"
                                        strNFOprop = newplotdetails.fullmoviebody.genre
                                    Case "credits"
                                        strNFOprop = newplotdetails.fullmoviebody.credits
                                    Case "director"
                                        strNFOprop = newplotdetails.fullmoviebody.director
                                    Case "studio"
                                        strNFOprop = newplotdetails.fullmoviebody.studio
                                    Case "trailer"
                                        strNFOprop = newplotdetails.fullmoviebody.trailer
                                    Case "playcount"
                                        strNFOprop = newplotdetails.fullmoviebody.playcount
                                    Case "id"
                                        strNFOprop = newplotdetails.fullmoviebody.imdbid
                                    Case "stars"
                                        strNFOprop = newplotdetails.fullmoviebody.stars
                                    Case "source"
                                        strNFOprop = newplotdetails.fullmoviebody.source
                                    Case Else
                                        strNFOprop = "No support for " & tokenInstr(1)
                                End Select

                                Select Case tokenInstr(1)
                                    Case "file", "createdate", "premiered", "filename"
                                        'Do nothing
                                    Case Else
                                        If tokenInstr.Length > 2 Then
                                            Dim intCharLimit = CInt(tokenInstr(2))
                                            If strNFOprop.Length > intCharLimit Then
                                                strNFOprop = strNFOprop.Substring(0, strNFOprop.LastIndexOf(" ", intCharLimit - 3)) & "<font class=dim>...</font>"
                                            End If
                                        End If
                                End Select

                            Catch
                                strNFOprop = "Error in token"
                            End Try
                        End If
                    End If

            End Select
            Try
                strNFOprop = strNFOprop.Replace(Chr(34), "&quot;")
                text = text.Replace(token.Value, strNFOprop)
            Catch
                text = text.Replace(token.Value, "")
            End Try
        Next

        Return text
    End Function

    Private Function getTagsTV(ByVal text As String, ByVal tvShow As Media_Companion.TvShow, ByVal showCounter As Integer, Optional ByVal imagepath As String = "")
        Dim inclShow As Boolean = False
        If imagepath.Equals("!HEADER!") Then    'A hack to process the header
            inclShow = True
            imagepath = ""                      'No images allowed in header!
        End If

        Dim blockShow As String = text
        Dim blockSeason As String = ""
        Dim blockEpisode As String = ""
        Dim strHTML As String = ""
        Dim counterSeason = 0
        If text.IndexOf("<<season") <> -1 And text.IndexOf("<</season>>") <> -1 Or text.IndexOf("<<episode") <> -1 And text.IndexOf("<</episode>>") <> -1 Then
            Dim setTVshows = New SortedList(Of String, TvEpisode)(New SeasonEpisodeComparer)
            Dim keySE As String
            Dim arrSeasonPresent(0 To 0) As Boolean
            Dim firstSeason As Integer = 99999
            Dim inclSeason As Boolean = False
            Dim inclEpisode As Boolean = False
            Dim inclMissingSeason As Boolean = False
            Dim inclMissingEpisode As Boolean = Preferences.displayMissingEpisodes
            If text.IndexOf("<<season>>") <> -1 Or text.IndexOf("<<season:all>>") <> -1 Or text.IndexOf("<<episode>>") <> -1 Or text.IndexOf("<<episode:all>>") <> -1 Then
                If text.IndexOf("<<season") <> -1 Then inclSeason = True
                If text.IndexOf("<<episode") <> -1 Then inclEpisode = True
                For Each episode In tvShow.Episodes
                    If episode.Season.Value <> "-1" And episode.Episode.Value <> "-1" Then
                        keySE = episode.Season.Value & "-" & episode.Episode.Value
                        episode.IsMissing = False
                        If Not setTVshows.ContainsKey(keySE) Then setTVshows.Add(keySE, episode)
                        If episode.Season.Value > UBound(arrSeasonPresent) Then
                            ReDim Preserve arrSeasonPresent(episode.Season.Value)
                            arrSeasonPresent(episode.Season.Value) = True
                        End If
                        If episode.Season.Value < firstSeason Then
                            firstSeason = episode.Season.Value
                            If episode.Season.Value = 0 Then arrSeasonPresent(0) = True
                        End If
                    End If
                Next
            End If
            If text.IndexOf("<<season:missing>>") <> -1 Or text.IndexOf("<<season:all>>") <> -1 Or text.IndexOf("<<episode:missing>>") <> -1 Or text.IndexOf("<<episode:all>>") <> -1 Then
                If tvShow.MissingEpisodes.Count > 0 Then
                    If text.IndexOf("<<season") <> -1 Then inclMissingSeason = True
                    If text.IndexOf("<<episode") <> -1 Then inclMissingEpisode = True
                    For Each episode In tvShow.MissingEpisodes
                        If episode.Season.Value <> "-1" And episode.Episode.Value <> "-1" Then
                            keySE = episode.Season.Value & "-" & episode.Episode.Value
                            episode.IsMissing = True
                            If Not setTVshows.ContainsKey(keySE) Then setTVshows.Add(keySE, episode)
                            If episode.Season.Value > UBound(arrSeasonPresent) Then
                                ReDim Preserve arrSeasonPresent(episode.Season.Value)
                                arrSeasonPresent(episode.Season.Value) = True
                            End If
                            If episode.Season.Value < firstSeason Then
                                firstSeason = episode.Season.Value
                                If episode.Season.Value = 0 Then arrSeasonPresent(0) = True
                            End If
                        End If
                    Next
                End If
            End If
            If setTVshows.Count Then
                Dim separator As String = "<<|separator|>>"
                If inclSeason Or inclMissingSeason Then
                    blockSeason = text.Substring(text.IndexOf("<<season"), text.IndexOf("<</season>>") - text.IndexOf("<<season") + 11)
                    blockShow = blockShow.Replace(blockSeason, separator)
                End If
                If inclEpisode Or inclMissingEpisode Then
                    blockEpisode = text.Substring(text.IndexOf("<<episode"), text.IndexOf("<</episode>>") - text.IndexOf("<<episode") + 12)
                    If blockSeason <> "" Then
                        blockSeason = blockSeason.Replace(blockEpisode, separator)
                    Else
                        blockShow = blockShow.Replace(blockEpisode, separator)
                    End If
                    blockEpisode = blockEpisode.Substring(blockEpisode.IndexOf(">>") + 2, blockEpisode.IndexOf("<</episode>>") - blockEpisode.IndexOf(">>") - 2)
                End If
                If blockSeason <> "" Then
                    blockSeason = blockSeason.Substring(blockSeason.IndexOf(">>") + 2, blockSeason.IndexOf("<</season>>") - blockSeason.IndexOf(">>") - 2)
                End If
                Dim strHTMLseason As String = ""
                Dim strHTMLepisode As String = ""
                Dim strTempEpisode As String = ""
                Dim strHTMLseasonSpecials As String = ""
                Dim currSeason As Integer = firstSeason
                Dim counterSeasonEpisodes = 0
                Dim counterSeasonMissingEpisodes = 0
                Dim counterSeasonTotalEpisodes = 0
                Dim lastEpisode As Boolean = False
                'Build string
                For Each episode In setTVshows
                    If Not (episode.Value.IsMissing And Not inclMissingEpisode) Then
                        strTempEpisode = If(inclEpisode Or inclMissingEpisode, getTagsTVEpisode(blockEpisode, episode.Value, showCounter, counterSeasonTotalEpisodes), "")
                        inclShow = True
                    End If
                    If setTVshows.IndexOfKey(episode.Key) = setTVshows.Count - 1 Then   'This is the last episode
                        If strTempEpisode <> "" Then
                            strHTMLepisode = strHTMLepisode & strTempEpisode
                            counterSeasonTotalEpisodes += 1
                            If episode.Value.IsMissing Then
                                counterSeasonMissingEpisodes += 1
                            Else
                                counterSeasonEpisodes += 1
                            End If
                        End If
                        lastEpisode = True
                    End If
                    ' If season changes or reach end of sorted list, aggregate season and episode HTML
                    If lastEpisode Or episode.Value.Season.Value > currSeason Then
                        If inclSeason Or inclMissingSeason Then
                            strHTMLseason = getTagsTVSeason(blockSeason, tvShow, showCounter, currSeason, counterSeasonEpisodes, counterSeasonMissingEpisodes, _
                                                            counterSeasonTotalEpisodes, arrSeasonPresent(currSeason), imagepath)
                            strHTMLseason = strHTMLseason.Replace(separator, If(inclMissingSeason And Not inclMissingEpisode Or inclSeason And Not inclEpisode, _
                                                                                "", strHTMLepisode))
                            inclShow = True
                        Else
                            strHTMLseason = strHTMLepisode
                        End If
                        If currSeason = 0 Then
                            strHTMLseasonSpecials = strHTMLseason
                        Else
                            strHTML = strHTML & strHTMLseason
                        End If
                        strHTMLseason = ""
                        strHTMLepisode = ""
                        currSeason = episode.Value.Season.Value
                        counterSeason += 1
                        counterSeasonEpisodes = 0
                        counterSeasonMissingEpisodes = 0
                        counterSeasonTotalEpisodes = 0
                    End If
                    If episode.Value.IsMissing Then
                        counterSeasonMissingEpisodes += 1
                    Else
                        counterSeasonEpisodes += 1
                    End If
                    counterSeasonTotalEpisodes += 1
                    strHTMLepisode = strHTMLepisode & strTempEpisode
                Next
                strHTML = strHTML & strHTMLseasonSpecials
                blockShow = blockShow.Replace(separator, strHTML)
            End If

        End If

        blockShow = getTagsTVShow(blockShow, tvShow, showCounter, counterSeason, imagepath)
        If Not inclShow Then blockShow = ""
        Return blockShow
    End Function
    Private Function getTagsTVShow(ByRef text As String, ByVal tvShow As TvShow, ByVal counter As Integer, ByVal numSeasons As Integer, Optional ByVal imagepath As String = "")
        Dim tokenCol As MatchCollection
        Dim tokenRegExp As New Regex("<<[\w_:]+>>")
        tokenCol = tokenRegExp.Matches(text)
        Dim token As Match

        For Each token In tokenCol
            Dim strNFOprop As String = ""
            Dim valToken As String = token.Value.Substring(2, token.Value.Length - 4)
            Dim tokenInstr() As String = valToken.Split(":")
            Dim addText As Boolean = False
            Dim padNumber As Boolean = False
            If tokenInstr.Length > 1 Then
                If tokenInstr(1).IndexOf("text") <> -1 Then addText = True
                If tokenInstr(1).IndexOf("pad") <> -1 Then padNumber = True
            End If
            Select Case tokenInstr(0)
                Case "createimage"
                    If imagepath <> "" And tokenInstr.Length > 1 Then
                        Dim origImage = Preferences.GetPosterPath(tvShow.NfoFilePath)
                        origImage = origImage.Replace(IO.Path.GetFileName(origImage), "folder.jpg")
                        Dim imageType As String = "poster"
                        Dim imgTest As Image = Image.FromFile(origImage)
                        If tokenInstr.Length > 2 And tokenInstr(2) = "banner" Then
                            imageType = "banner"
                            If imgTest.Height / imgTest.Width > 1 Then
                                origImage = origImage.Replace(IO.Path.GetFileName(origImage), "season-all.tbn")
                            End If
                        Else
                            If imgTest.Height / imgTest.Width < 1 Then
                                origImage = origImage.Replace(IO.Path.GetFileName(origImage), "season-all.tbn")
                            End If
                        End If

                        If (tokenInstr(UBound(tokenInstr)) <> "nopath") Then strNFOprop &= "tvimages/"
                        strNFOprop &= Utilities.createImage(origImage, tokenInstr(1), imagepath, imageType)
                    End If

                Case "show_title"
                    strNFOprop = tvShow.Title.Value

                Case "show_year"
                    strNFOprop = tvShow.Year.Value

                Case "show_titleandyear"
                    strNFOprop = tvShow.TitleAndYear

                Case "show_imdbid"
                    strNFOprop = tvShow.ImdbId.Value

                Case "show_imdburl"
                    strNFOprop = If(tvShow.ImdbId <> Nothing, Preferences.imdbmirror & "title/" & tvShow.ImdbId.Value & "/", Preferences.imdbmirror)

                Case "show_tvdbid"
                    strNFOprop = tvShow.TvdbId.Value

                Case "show_tvdburl"
                    strNFOprop = If(tvShow.TvdbId <> Nothing, "http://thetvdb.com/?tab=series&id=" & tvShow.TvdbId.Value, "http://thetvdb.com/")

                Case "show_genre"
                    strNFOprop = tvShow.Genre.Value

                Case "show_episodeactorsource"
                    strNFOprop = tvShow.EpisodeActorSource.Value

                Case "show_language"
                    strNFOprop = tvShow.Language.Value

                Case "show_locked"
                    strNFOprop = tvShow.State

                Case "show_rating"
                    strNFOprop = If(tvShow.Rating <> Nothing, tvShow.Rating.Value & If(tvShow.Rating.Value.IndexOf(".") <> -1, "", ".0"), "")

                Case "show_sortorder"
                    strNFOprop = tvShow.SortOrder.Value

                Case "show_status"
                    strNFOprop = tvShow.Status.Value

                Case "show_count"
                    strNFOprop = If(Cache.TvCache.Shows.Count, Cache.TvCache.Shows.Count.ToString, "00")

                Case "show_counter"
                    strNFOprop = counter.ToString

                Case "show_seasons"
                    strNFOprop = numSeasons.ToString
                    If addText Then strNFOprop = strNFOprop & " Season" & If(numSeasons <> 1, "s", "")

                Case "class"
                    If tokenInstr.Length > 1 Then
                        If tokenInstr(1).IndexOf("row") <> -1 Then
                            strNFOprop = " class=show_row_" & If(counter Mod 2, "odd", "even")
                        Else
                            strNFOprop = " class=" & tokenInstr(1)
                        End If
                    End If

                Case "show_nfo"
                    Dim fullTVShowDetails As New TvShow
                    fullTVShowDetails.Load(tvShow.NfoFilePath)
                    Try
                        Select Case tokenInstr(1)
                            Case "id"
                                strNFOprop = fullTVShowDetails.ImdbId.Value
                            Case "episodeguide"
                                strNFOprop = fullTVShowDetails.EpisodeGuideUrl.Value
                            Case "actor"                                        ' No support for actor list
                                strNFOprop = "No support"
                            Case "thumb"                                        ' No support for thumbnail list
                                strNFOprop = "No support"
                            Case "fanart"                                       ' No support for fanart list
                                strNFOprop = "No support"
                            Case Else
                                strNFOprop = CallByName(fullTVShowDetails, tokenInstr(1), vbGet)
                        End Select
                        If tokenInstr(1) <> "episodeguide" And tokenInstr.Length > 2 Then
                            Dim intCharLimit = CInt(tokenInstr(2))
                            If strNFOprop.Length > intCharLimit Then
                                strNFOprop = strNFOprop.Substring(0, strNFOprop.LastIndexOf(" ", intCharLimit - 3)) & "<font class=dim>...</font>"
                            End If
                        End If

                    Catch
                        strNFOprop = "Error in token"
                    End Try

            End Select

            Try
                strNFOprop = strNFOprop.Replace(Chr(34), "&quot;")
                text = text.Replace(token.Value, strNFOprop)
            Catch
                text = text.Replace(token.Value, "")
            End Try
        Next
        Return text
    End Function
    Private Function getTagsTVSeason(ByVal text As String, ByVal tvShow As TvShow, ByVal showCounter As Integer, _
                                     ByVal currSeason As Integer, ByVal numEpisodes As Integer, ByVal numMissingEpisodes As Integer, _
                                     ByVal numTotalEpisodes As Integer, ByVal seasonPresent As Boolean, Optional ByVal imagepath As String = "")
        Dim tokenCol As MatchCollection
        Dim tokenRegExp As New Regex("<<[\w_:]+>>")
        tokenCol = tokenRegExp.Matches(text)
        Dim token As Match

        For Each token In tokenCol
            Dim strNFOprop As String = ""
            Dim valToken As String = token.Value.Substring(2, token.Value.Length - 4)
            Dim tokenInstr() As String = valToken.Split(":")
            Dim addText As Boolean = False
            Dim padNumber As Boolean = False
            Dim specials As Boolean = False
            If tokenInstr.Length > 1 Then
                If tokenInstr(1).IndexOf("text") <> -1 Then addText = True
                If tokenInstr(1).IndexOf("pad") <> -1 Then padNumber = True
                If tokenInstr(1).IndexOf("special") <> -1 Then specials = True
            End If
            Select Case tokenInstr(0)
                Case "createimage"
                    If imagepath <> "" And tokenInstr.Length > 1 Then
                        Dim origImage = Preferences.GetPosterPath(tvShow.NfoFilePath)
                        Dim imageName As String = "season" & If(currSeason >= 10, "", "0") & currSeason.ToString & ".tbn"
                        If currSeason = 0 Then imageName = "season-specials.tbn"
                        origImage = origImage.Replace(IO.Path.GetFileName(origImage), imageName)

                        If (tokenInstr(UBound(tokenInstr)) <> "nopath") Then strNFOprop &= "tvimages/"
                        strNFOprop &= Utilities.createImage(origImage, tokenInstr(1), imagepath)
                    End If

                Case "show_counter"
                    strNFOprop = showCounter.ToString
                Case "seas_number"
                    If currSeason = 0 And specials Then
                        Dim endOfPrevTag = text.LastIndexOf(">", text.IndexOf(token.Value)) + 1
                        text = text.Remove(endOfPrevTag, text.IndexOf(token.Value) - endOfPrevTag)
                        strNFOprop = "Specials"
                    Else
                        strNFOprop = If(currSeason.ToString.Length = 1 And padNumber, "0", "") & currSeason.ToString
                        If addText Then strNFOprop = strNFOprop & " Season" & If(numTotalEpisodes <> 1, "s", "")
                    End If
                Case "seas_episodes"
                    strNFOprop = If(numTotalEpisodes.ToString.Length = 1 And padNumber, "0", "") & numTotalEpisodes.ToString
                    If addText Then strNFOprop = strNFOprop & " Episode" & If(numTotalEpisodes <> 1, "s", "")
                Case "seas_episodesof"
                    strNFOprop = numEpisodes.ToString
                    If numEpisodes = numTotalEpisodes Then
                        ' Do nothing
                    ElseIf numMissingEpisodes Then
                        strNFOprop = strNFOprop & " of " & numTotalEpisodes.ToString
                    End If
                    If addText Then strNFOprop = strNFOprop & " Episode" & If(numTotalEpisodes <> 1, "s", "")
                Case "class"
                    If tokenInstr.Length > 1 Then
                        If tokenInstr(1).IndexOf("missing") <> -1 Then
                            ' Special case where we want to know if the season contains missing episodes
                            If tokenInstr(1).IndexOf("episode") <> -1 And numMissingEpisodes Then
                                strNFOprop = " class=missingseason"
                            ElseIf Not seasonPresent Then
                                strNFOprop = " class=" & tokenInstr(1)
                            End If
                        ElseIf tokenInstr(1).IndexOf("row") <> -1 Then
                            strNFOprop = " class=seas_row_" & If(currSeason Mod 2, "odd", "even")
                        Else
                            strNFOprop = " class=" & tokenInstr(1)
                        End If
                    End If
            End Select

            Try
                strNFOprop = strNFOprop.Replace(Chr(34), "&quot;")
                text = text.Replace(token.Value, strNFOprop)
            Catch
                text = text.Replace(token.Value, "")
            End Try
        Next
        Return text
    End Function
    Private Function getTagsTVEpisode(ByVal text As String, ByVal tvEpisode As TvEpisode, ByVal showCounter As Integer, ByVal episodeCounter As Integer, Optional ByVal imagepath As String = "")
        Dim tokenCol As MatchCollection
        Dim tokenRegExp As New Regex("<<[\w_:]+>>")
        tokenCol = tokenRegExp.Matches(text)
        Dim token As Match

        For Each token In tokenCol
            Dim strNFOprop As String = ""
            Dim valToken As String = token.Value.Substring(2, token.Value.Length - 4)
            Dim tokenInstr() As String = valToken.Split(":")
            Dim addText As Boolean = False
            Dim padNumber As Boolean = False
            If tokenInstr.Length > 1 Then
                If tokenInstr(1).IndexOf("text") <> -1 Then addText = True
                If tokenInstr(1).IndexOf("pad") <> -1 Then padNumber = True
            End If
            Select Case tokenInstr(0)
                Case "show_counter"
                    strNFOprop = showCounter.ToString
                Case "ep_title"
                    strNFOprop = tvEpisode.Title.Value

                Case "ep_season"
                    strNFOprop = If(tvEpisode.Season.Value.Length = 1 And padNumber, "0", "") & tvEpisode.Season.Value

                Case "ep_number"
                    strNFOprop = If(tvEpisode.Episode.Value.Length = 1 And padNumber, "0", "") & tvEpisode.Episode.Value

                Case "ep_rating"
                    strNFOprop = If(tvEpisode.Rating.Value <> Nothing, tvEpisode.Rating.Value & If(tvEpisode.Rating.Value.IndexOf(".") <> -1, "", ".0"), "")

                Case "ep_playcount"
                    strNFOprop = tvEpisode.PlayCount.Value

                Case "ep_imdbid"
                    strNFOprop = tvEpisode.ImdbId.Value

                Case "ep_imdburl"
                    strNFOprop = If(tvEpisode.ImdbId.Value <> Nothing, Preferences.imdbmirror & "title/" & tvEpisode.ImdbId.Value & "/", Preferences.imdbmirror)

                Case "ep_tvdbid"
                    strNFOprop = tvEpisode.TvdbId.Value

                Case "class"
                    If tokenInstr.Length > 1 Then
                        If tokenInstr(1).IndexOf("missing") <> -1 And Not tvEpisode.IsMissing Then
                            ' Do nothing
                        ElseIf tokenInstr(1).IndexOf("row") <> -1 Then
                            strNFOprop = " class=ep_row_" & If(episodeCounter Mod 2, "odd", "even")
                        Else
                            strNFOprop = " class=" & tokenInstr(1)
                        End If
                    End If

                Case "ep_nfo"
                    Dim TVEpisodeNFO As List(Of TvEpisode) = htmlNfoFunction.ep_NfoLoadGeneric(tvEpisode.NfoFilePath)
                    Dim fullTVEpisodeDetails As TvEpisode = TVEpisodeNFO(0)
                    Try
                        Select Case tokenInstr(1)
                            Case "file"
                                Select Case tokenInstr(2)
                                    Case "video"
                                        Select Case tokenInstr(3)
                                            Case "width"
                                                strNFOprop = fullTVEpisodeDetails.Details.StreamDetails.Video.Width.Value
                                            Case "height"
                                                strNFOprop = fullTVEpisodeDetails.Details.StreamDetails.Video.Height.Value
                                            Case "aspect"
                                                strNFOprop = fullTVEpisodeDetails.Details.StreamDetails.Video.Aspect.Value
                                            Case "codec"
                                                strNFOprop = fullTVEpisodeDetails.Details.StreamDetails.Video.Codec.Value
                                            Case "duration"
                                                strNFOprop = fullTVEpisodeDetails.Details.StreamDetails.Video.DurationInSeconds.Value
                                            Case "container"
                                                strNFOprop = fullTVEpisodeDetails.Details.StreamDetails.Video.Container.Value
                                            Case Else
                                                strNFOprop = tokenInstr(3) & "not supported"
                                        End Select
                                        'strNFOprop = CallByName(fullTVEpisodeDetails.Details.StreamDetails.Video, tokenInstr(3), vbGet)
                                    Case "audio"
                                        Dim i As Integer = 1
                                        For Each audioStream In fullTVEpisodeDetails.Details.StreamDetails.Audio
                                            Select Case tokenInstr(3)
                                                Case "language"
                                                    strNFOprop &= audioStream.Language.Value
                                                Case "channels"
                                                    strNFOprop &= audioStream.Channels.Value
                                                Case "bitrate"
                                                    strNFOprop &= audioStream.Bitrate.Value
                                                Case "codec"
                                                    strNFOprop &= audioStream.Codec.Value
                                            End Select
                                            'strNFOprop = strNFOprop & CallByName(audioStream, tokenInstr(3), vbGet)
                                            If (fullTVEpisodeDetails.Details.StreamDetails.Audio.Count > 1 And i <> fullTVEpisodeDetails.Details.StreamDetails.Audio.Count) Then
                                                strNFOprop = strNFOprop & " / "
                                            End If
                                            i += 1
                                        Next
                                    Case "subtitles"
                                        Dim i As Integer = 1
                                        For Each subLang In fullTVEpisodeDetails.Details.StreamDetails.Subtitles
                                            Select Case tokenInstr(3)
                                                Case "language"
                                                    strNFOprop &= subLang.Language.Value
                                            End Select
                                            'strNFOprop = strNFOprop & CallByName(subLang, tokenInstr(3), vbGet)
                                            If (fullTVEpisodeDetails.Details.StreamDetails.Subtitles.Count > 1 And i <> fullTVEpisodeDetails.Details.StreamDetails.Subtitles.Count) Then
                                                strNFOprop = strNFOprop & " / "
                                            End If
                                            i += 1
                                        Next
                                End Select
                            Case "title"
                                strNFOprop = fullTVEpisodeDetails.Title.Value
                            Case "season"
                                strNFOprop = fullTVEpisodeDetails.Season.Value
                            Case "episode"
                                strNFOprop = fullTVEpisodeDetails.Episode.Value
                            Case "aired"
                                Dim newDate As String = fullTVEpisodeDetails.Aired.Value
                                If tokenInstr.Length > 2 Then
                                    Try
                                        Dim result As Date
                                        result = DateTime.ParseExact(newDate, Preferences.nfoDatePattern, Nothing)
                                        strNFOprop = Format(result, tokenInstr(2)).ToString
                                        If tokenInstr.Length > 3 Then
                                            Dim separator As String = "!"
                                            Select Case tokenInstr(3).ToLower
                                                Case "space"
                                                    separator = " "
                                                Case "colon"
                                                    separator = ":"
                                                Case "dash"
                                                    separator = "-"
                                                Case "slash"
                                                    separator = "/"
                                            End Select
                                            strNFOprop = strNFOprop.Replace("_", separator)
                                        End If
                                    Catch ex As Exception
                                        strNFOprop = "Error in date format"
                                    End Try
                                Else
                                    strNFOprop = newDate
                                End If
                                'strNFOprop = fullTVEpisodeDetails.Aired.Value
                            Case "plot"
                                strNFOprop = fullTVEpisodeDetails.Plot.Value
                            Case "playcount"
                                strNFOprop = fullTVEpisodeDetails.PlayCount.Value
                            Case "director"
                                strNFOprop = fullTVEpisodeDetails.Director.Value
                            Case "credits"
                                strNFOprop = fullTVEpisodeDetails.Credits.Value
                            Case "rating"
                                strNFOprop = fullTVEpisodeDetails.Rating.Value
                            Case "runtime"
                                strNFOprop = fullTVEpisodeDetails.Runtime.Value
                            Case "showid"
                                strNFOprop = fullTVEpisodeDetails.Id.Value
                            Case "actor"                                        ' No support for actor list
                                strNFOprop = "No support"
                            Case "thumb"                                        ' No support for thumbnail list
                                strNFOprop = "No support"
                            Case "fanart"                                       ' No support for fanart list
                                strNFOprop = "No support"
                            Case Else
                                strNFOprop = "No support"
                        End Select
                        If tokenInstr.Length > 2 And tokenInstr(1) <> "file" And tokenInstr(1) <> "aired" Then
                            Dim intCharLimit = CInt(tokenInstr(2))
                            If strNFOprop.Length > intCharLimit Then
                                strNFOprop = strNFOprop.Substring(0, strNFOprop.LastIndexOf(" ", intCharLimit - 3)) & "<font class=dim>...</font>"
                            End If
                        End If

                    Catch
                        strNFOprop = "Error in token"
                    End Try
            End Select

            Try
                strNFOprop = strNFOprop.Replace(Chr(34), "&quot;")
                text = text.Replace(token.Value, strNFOprop)
            Catch
                text = text.Replace(token.Value, "")
            End Try
        Next
        Return text
    End Function

End Class
