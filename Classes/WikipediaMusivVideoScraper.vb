Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class WikipediaMusivVideoScraper

    Public Function musicVideoScraper(ByVal fullpathandfilename As String, Optional ByVal searchterm As String = "", Optional ByVal wikipediaURL As String = "")
        Monitor.Enter(Me)
        Dim totalinfo As String = ""
        totalinfo = "<musicvideo>" & vbCrLf
        'Dim musicVideoTitle As New FullMovieDetails
        Dim s As New Classimdb
        Dim webpage As List(Of String)
        Dim Artist As String = ""
        Dim Title As String = ""
        'musicVideoTitle.fileinfo.fullpathandfilename = fullpathandfilename
        Dim wikiurlpassed As String = wikipediaURL 
        searchterm = getArtistAndTitle(fullpathandfilename)

        If searchterm = "" And wikipediaURL = "" Then
            Dim filenameWithoutExtension As String = Path.GetFileNameWithoutExtension(fullpathandfilename)
            Dim strarr() As String
            strarr = filenameWithoutExtension.Split("-"c)
            Artist = strarr(0).Trim
            Title = strarr(1).Trim
            searchterm = s.searchurltitle(filenameWithoutExtension)
        ElseIf searchterm <> "" And wikipediaURL = "" Then
            Dim strarr() As String
            strarr = searchterm.Split("-"c)
            Artist = strarr(0).Trim
            Title = strarr(1).Trim
        ElseIf wikipediaURL <> "" Then
            Dim strarr() As String
            strarr = fullpathandfilename.Split("-"c)
            Artist = strarr(0).Trim
            Title = strarr(1).Trim
            searchterm = fullpathandfilename
        End If

        Dim searchurl As String = "http://www.google.co.uk/search?hl=en-US&as_q=" & searchterm & "%20song&as_sitesearch=http://en.wikipedia.org/"

        If wikipediaURL = "" Then
            webpage = s.loadwebpage(Preferences.proxysettings, searchurl, False, 10)


            For Each line In webpage
                If line.IndexOf("<a href=""https://en.wikipedia.org/wiki/") <> -1 Then
                    wikipediaURL = ""


                    Exit For
                ElseIf line.IndexOf("<a href=""/url?q=https://en.wikipedia.org/wiki/") <> -1 Then
                    Dim startinteger As Integer = line.IndexOf("<a href=""/url?q=https://en.wikipedia.org/wiki/")
                    wikipediaURL = line.Substring(startinteger + 16, 100)
                    wikipediaURL = wikipediaURL.Replace("%253F", "%3F")
                    wikipediaURL = wikipediaURL.Substring(0, wikipediaURL.IndexOf("&"))
                    Exit For
                End If
            Next

            webpage.Clear()
        End If
        Dim fullwebpage As String = ""
        'If wikipediaURL = "" Then   'set artist and title if a new scrape, not a change musicvideo.
        '    totalinfo.AppendTag("artist", Artist)
        '    totalinfo.AppendTag("title", Title)
        'End If
        If wikipediaURL <> "" Then
            webpage = s.loadwebpage(Preferences.proxysettings, wikipediaURL, False, 10)
            Dim htpage As String = ""
            For each p In webpage
                htpage &= p & vbcrlf
            Next
            Dim webPg As String = String.Join( "" , webpage.ToArray() )
            fullwebpage = webPg
            Try
                'Scrape Plot and Director
                If fullwebpage.IndexOf("<h2><span class=""mw-headline"" id=""Music_video"">") <> -1 Then
                    Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("<h2><span class=""mw-headline"" id=""Music_video"">"), fullwebpage.Length - fullwebpage.IndexOf("<h2><span class=""mw-headline"" id=""Music_video"">"))
                    tempstring = tempstring.Substring(4, tempstring.Length - 4)
                    tempstring = tempstring.Substring(0, tempstring.IndexOf("<h2>"))
                    tempstring = tempstring.Substring(tempstring.IndexOf("<p>"), tempstring.LastIndexOf("</p>") - tempstring.IndexOf("<p>"))

                    'get director before stripping tags
                    Try
                        Dim director As String = tempstring.Substring(tempstring.ToLower.IndexOf("directed by"), tempstring.Length - tempstring.ToLower.IndexOf("directed by"))
                        director = director.Substring(0, director.IndexOf("</a>"))
                        director = director.Substring(director.LastIndexOf(">") + 1, director.Length - director.LastIndexOf(">") - 1)
                        totalinfo.AppendTag("director", director)
                    Catch
                    End Try

                    'strip html tags from plot
                    tempstring = Regex.Replace(tempstring, "<.*?>", "")

                    'strip reference tags
                    tempstring = Regex.Replace(tempstring, "\[(.*?)\]", "")

                    'tidy up
                    tempstring = tempstring.Replace("<edit>", "")
                    tempstring = tempstring.Replace("Synopsis", vbCrLf & "Synopsis" & vbCrLf)
                    tempstring = tempstring.Replace("Background", vbCrLf & "Background" & vbCrLf)
                    totalinfo.AppendTag("plot", tempstring)
                End If

                'get year
                '<th scope="row" style="text-align:left;">Released</th>
                If fullwebpage.IndexOf(">Released</th>") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("Released</th>"), fullwebpage.Length - fullwebpage.IndexOf("Released</th>"))
                        tempstring = tempstring.Substring(tempstring.IndexOf("<td>"), tempstring.IndexOf("</td>") - tempstring.IndexOf("<td>"))
                        Dim r As Regex = New Regex("\d{4}")
                        Dim match As Match = r.Match(tempstring)
                        totalinfo.AppendTag("year", match.Value)
                    Catch
                    End Try
                End If

                'get genre
                If fullwebpage.IndexOf(">Genre</a></th>") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf(">Genre</a></th>"), fullwebpage.Length - fullwebpage.IndexOf(">Genre</a></th>"))
                        tempstring = tempstring.Substring(tempstring.IndexOf("<td>"), tempstring.IndexOf("</td>") - tempstring.IndexOf("<td>"))
                        tempstring = Regex.Replace(tempstring, "<.*?>", "")
                        totalinfo.AppendTag("genre", tempstring)
                    Catch
                    End Try
                End If

                'get album title
                If fullwebpage.IndexOf("from the album") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("from the album"), fullwebpage.Length - fullwebpage.IndexOf("from the album"))
                        tempstring = tempstring.Replace("from the album ", "")
                        tempstring = tempstring.Substring(0, tempstring.IndexOf("</i>"))
                        tempstring = Regex.Replace(tempstring, "<.*?>", "")
                        totalinfo.AppendTag("album", tempstring)
                    Catch
                    End Try
                End If

                'get studio
                If fullwebpage.IndexOf("Label</a></th>") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("Label</a></th>"), fullwebpage.Length - fullwebpage.IndexOf("Label</a></th>"))
                        tempstring = tempstring.Substring(tempstring.IndexOf("<td>"), tempstring.IndexOf("</td>") - tempstring.IndexOf("<td>"))
                        tempstring = Regex.Replace(tempstring, "<.*?>", "")
                        totalinfo.AppendTag("studio", tempstring)
                    Catch
                    End Try
                End If

                If fullwebpage.IndexOf("class=""image"">") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("class=""image"">"), fullwebpage.Length - fullwebpage.IndexOf("class=""image"">"))
                        tempstring = tempstring.Substring(0, tempstring.IndexOf("</td>"))
                        If tempstring.IndexOf(".jpg") <> -1 Then
                            tempstring = tempstring.Substring(tempstring.LastIndexOf("//upload"), tempstring.LastIndexOf(".jpg") - tempstring.LastIndexOf("//upload") + 4)
                        ElseIf tempstring.IndexOf(".png") <> -1 Then
                            tempstring = tempstring.Substring(tempstring.LastIndexOf("//upload"), tempstring.LastIndexOf(".png") - tempstring.LastIndexOf("//upload") + 4)
                        End If
                        tempstring = "http:" & tempstring
                        totalinfo.AppendTag("thumb", tempstring)
                    Catch
                    End Try
                End If

                'If wikipediaURL = wikiurlpassed Then  '  If change music video, get new title and artist.

                    'Get artist
                    If fullwebpage.IndexOf(""">Single</a>") <> -1 Then
                        Try
                            Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf(""">Single</a>"), 150 )
                            tempstring = tempstring.Replace(""">Single</a>", "")
                            tempstring = tempstring.Substring((tempstring.IndexOf(""">")+2), (tempstring.IndexOf("</a></th>") - tempstring.IndexOf(""">"))-2)
                            tempstring = Regex.Replace(tempstring, "<.*?>", "")
                            totalinfo.AppendTag("artist", tempstring)
                        Catch
                        End Try
                    End If

                    'Get Title
                    For f = 0 to webpage.Count-1
                        If webpage(f).IndexOf("""</th>") <> -1 Then
                            Try
                                Dim d = webpage(f).IndexOf(""">")+3
                                Dim w = If(d > 0, webpage(f).IndexOf("""</th>", d), 0)
                                If Not D <= 0 And Not W <= 0 Then
                                    Dim tempstring As String = webpage(f).Substring(d, w - d)
                                    tempstring = Regex.Replace(tempstring, "<.*?>", "")
                                    totalinfo.AppendTag("title", tempstring)
                                End If
                                Exit For
                            Catch
                            End Try
                        End If
                    Next
                'End If
            Catch
            End Try
        End If
        totalinfo = totalinfo & "</musicvideo>" & vbCrLf
        Return totalinfo
        Monitor.Exit(Me)
    End Function

    Private Function getArtistAndTitle(ByVal fullpathandfilename As String)
        Monitor.Enter(Me)
        Dim searchTerm As String = ""
        Dim filenameWithoutExtension As String = Path.GetFileNameWithoutExtension(fullpathandfilename)
        If filenameWithoutExtension.IndexOf(" - ") <> -1 Then
            searchTerm = filenameWithoutExtension
        Else 'assume /artist/title.ext convention
            Try
                Dim lastfolder As String = Utilities.GetLastFolder(fullpathandfilename)
                searchTerm = lastfolder & " - " & filenameWithoutExtension
            Catch
            End Try
        End If

        If searchTerm = "" Then
            searchTerm = filenameWithoutExtension
        End If

        Return searchTerm
        Monitor.Exit(Me)
    End Function

    'Public Function searchurltitle(ByVal title As String) As String
    '    Dim urltitle As String = title

    '    urltitle = urltitle.Replace(".", "+")
    '    urltitle = urltitle.Replace(" ", "+")
    '    urltitle = urltitle.Replace("_", "+")
    '    urltitle = urltitle.Replace("À", "%c0")
    '    urltitle = urltitle.Replace("Á", "%c1")
    '    urltitle = urltitle.Replace("Â", "%c2")
    '    urltitle = urltitle.Replace("Ã", "%c3")
    '    urltitle = urltitle.Replace("Ä", "%c4")
    '    urltitle = urltitle.Replace("Å", "%c5")
    '    urltitle = urltitle.Replace("Æ", "%c6")
    '    urltitle = urltitle.Replace("Ç", "%c7")
    '    urltitle = urltitle.Replace("È", "%c8")
    '    urltitle = urltitle.Replace("É", "%c9")
    '    urltitle = urltitle.Replace("Ê", "%ca")
    '    urltitle = urltitle.Replace("Ë", "%cb")
    '    urltitle = urltitle.Replace("Ì", "%cc")
    '    urltitle = urltitle.Replace("Í", "%cd")
    '    urltitle = urltitle.Replace("Î", "%ce")
    '    urltitle = urltitle.Replace("Ï", "%cf")
    '    urltitle = urltitle.Replace("Ð", "%d0")
    '    urltitle = urltitle.Replace("Ñ", "%d1")
    '    urltitle = urltitle.Replace("Ò", "%d2")
    '    urltitle = urltitle.Replace("Ó", "%d3")
    '    urltitle = urltitle.Replace("Ô", "%d4")
    '    urltitle = urltitle.Replace("Õ", "%d5")
    '    urltitle = urltitle.Replace("Ö", "%d6")
    '    urltitle = urltitle.Replace("Ø", "%d8")
    '    urltitle = urltitle.Replace("Ù", "%d9")
    '    urltitle = urltitle.Replace("Ú", "%da")
    '    urltitle = urltitle.Replace("Û", "%db")
    '    urltitle = urltitle.Replace("Ü", "%dc")
    '    urltitle = urltitle.Replace("Ý", "%dd")
    '    urltitle = urltitle.Replace("Þ", "%de")
    '    urltitle = urltitle.Replace("ß", "%df")
    '    urltitle = urltitle.Replace("à", "%e0")
    '    urltitle = urltitle.Replace("á", "%e1")
    '    urltitle = urltitle.Replace("â", "%e2")
    '    urltitle = urltitle.Replace("ã", "%e3")
    '    urltitle = urltitle.Replace("ä", "%e4")
    '    urltitle = urltitle.Replace("å", "%e5")
    '    urltitle = urltitle.Replace("æ", "%e6")
    '    urltitle = urltitle.Replace("ç", "%e7")
    '    urltitle = urltitle.Replace("è", "%e8")
    '    urltitle = urltitle.Replace("é", "%e9")
    '    urltitle = urltitle.Replace("ê", "%ea")
    '    urltitle = urltitle.Replace("ë", "%eb")
    '    urltitle = urltitle.Replace("ì", "%ec")
    '    urltitle = urltitle.Replace("í", "%ed")
    '    urltitle = urltitle.Replace("î", "%ee")
    '    urltitle = urltitle.Replace("ï", "%ef")
    '    urltitle = urltitle.Replace("ð", "%f0")
    '    urltitle = urltitle.Replace("ñ", "%f1")
    '    urltitle = urltitle.Replace("ò", "%f2")
    '    urltitle = urltitle.Replace("ó", "%f3")
    '    urltitle = urltitle.Replace("ô", "%f4")
    '    urltitle = urltitle.Replace("õ", "%f5")
    '    urltitle = urltitle.Replace("ö", "%f6")
    '    urltitle = urltitle.Replace("÷", "%f7")
    '    urltitle = urltitle.Replace("ø", "%f8")
    '    urltitle = urltitle.Replace("ù", "%f9")
    '    urltitle = urltitle.Replace("ú", "%fa")
    '    urltitle = urltitle.Replace("û", "%fb")
    '    urltitle = urltitle.Replace("ü", "%fc")
    '    urltitle = urltitle.Replace("ý", "%fd")
    '    urltitle = urltitle.Replace("þ", "%fe")
    '    urltitle = urltitle.Replace("ÿ", "%ff")
    '    urltitle = urltitle.Replace("'", "%27")
    '    urltitle = urltitle.Replace("!", "%21")
    '    urltitle = urltitle.Replace("&", "%26")
    '    urltitle = urltitle.Replace(",", "")
    '    urltitle = urltitle.Replace("++", "+")
    '    Return urltitle
    'End Function

    'Private Function loadwebpage(ByVal url As String, Optional ByVal method As Boolean = False)

    '    Dim webpage As New List(Of String)
    '    Dim sLine As String = ""

    '    Try
    '        Dim wrGETURL As WebRequest
    '        wrGETURL = WebRequest.Create(url)
    '        Dim myProxy As New WebProxy("myproxy", 80)
    '        myProxy.BypassProxyOnLocal = True
    '        Dim objStream As Stream
    '        objStream = wrGETURL.GetResponse.GetResponseStream()
    '        Dim objReader As New StreamReader(objStream, System.Text.UTF8Encoding.UTF7)


    '        If method = False Then
    '            Do While Not sLine Is Nothing

    '                sLine = objReader.ReadLine
    '                If Not sLine Is Nothing Then
    '                    webpage.Add(sLine)
    '                End If
    '            Loop
    '        Else
    '            sLine = objReader.ReadToEnd
    '        End If
    '        objReader.Close()

    '        If method = False Then
    '            Return webpage
    '        Else
    '            Return sLine
    '        End If
    '        Return webpage
    '    Catch ex As WebException
    '        If method = False Then
    '            If webpage.Count > 0 Then
    '                Return webpage
    '            Else
    '                webpage.Add("error")
    '                Return webpage
    '            End If
    '        Else
    '            If sLine.length > 0 Then
    '                Return sLine
    '            Else
    '                sLine = "error"
    '                Return sLine
    '            End If
    '        End If
    '    Finally

    '    End Try



    'End Function
End Class
