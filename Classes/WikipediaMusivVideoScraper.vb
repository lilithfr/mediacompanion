Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class WikipediaMusivVideoScraper

    Public Function musicVideoScraper(ByVal fullpathandfilename)
        Dim musicVideoTitle As New FullMovieDetails 
        Dim s As New Classimdb
        musicVideoTitle.fileinfo.fullPathAndFilename = fullpathandfilename
        Dim filenameWithoutExtension As String = Path.GetFileNameWithoutExtension(fullpathandfilename)
        Dim strarr() As String
        strarr = filenameWithoutExtension.Split("-"c)
        musicVideoTitle.fullmoviebody.artist = strarr(0).Trim
        musicVideoTitle.fullmoviebody.title = strarr(1).Trim

        filenameWithoutExtension = s.searchurltitle(filenameWithoutExtension)

        Dim searchurl As String = "http://www.google.co.uk/search?hl=en-US&as_q=" & filenameWithoutExtension & "%20song&as_sitesearch=http://en.wikipedia.org/"

        Dim webpage As New List(Of String)
        webpage = s.loadwebpage(Preferences.proxysettings, searchurl, False, 10)

        Dim wikipediaURL As String = ""

        For Each line In webpage
            If line.IndexOf("<a href=""http://en.wikipedia.org/wiki/") <> -1 Then
                wikipediaURL = ""


                Exit For
            ElseIf line.IndexOf("<a href=""/url?q=http://en.wikipedia.org/wiki/") <> -1 Then
                Dim startinteger As Integer = line.IndexOf("<a href=""/url?q=http://en.wikipedia.org/wiki/")
                wikipediaURL = line.Substring(startinteger + 16, 100)
                wikipediaURL = wikipediaURL.Replace("%253F", "%3F")
                wikipediaURL = wikipediaURL.Substring(0, wikipediaURL.IndexOf("&"))
                Exit For
            End If
        Next

        webpage.Clear()

        Dim fullwebpage As String = ""
        If wikipediaURL <> "" Then
            fullwebpage = s.loadwebpage(Preferences.proxysettings,wikipediaURL, True, 10)
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
                        musicVideoTitle.fullmoviebody.director = director
                    Catch
                        musicVideoTitle.fullmoviebody.director = "Unknown"
                    End Try

                    'strip html tags from plot
                    tempstring = Regex.Replace(tempstring, "<.*?>", "")
                    musicVideoTitle.fullmoviebody.plot = tempstring
                End If

                'get year
                '<th scope="row" style="text-align:left;">Released</th>
                If fullwebpage.IndexOf(">Released</th>") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("Released</th>"), fullwebpage.Length - fullwebpage.IndexOf("Released</th>"))
                        tempstring = tempstring.Substring(tempstring.IndexOf("<td>"), tempstring.IndexOf("</td>") - tempstring.IndexOf("<td>"))
                        Dim r As Regex = New Regex("\d{4}")
                        Dim match As Match = r.Match(tempstring)
                        musicVideoTitle.fullmoviebody.year = match.Value
                    Catch
                    End Try
                End If

                'get genre
                If fullwebpage.IndexOf(">Genre</a></th>") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf(">Genre</a></th>"), fullwebpage.Length - fullwebpage.IndexOf(">Genre</a></th>"))
                        tempstring = tempstring.Substring(tempstring.IndexOf("<td>"), tempstring.IndexOf("</td>") - tempstring.IndexOf("<td>"))
                        tempstring = Regex.Replace(tempstring, "<.*?>", "")
                        musicVideoTitle.fullmoviebody.genre = tempstring
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
                        musicVideoTitle.fullmoviebody.album = tempstring
                    Catch
                    End Try
                End If

                'get studio
                If fullwebpage.IndexOf("Label</a></th>") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("Label</a></th>"), fullwebpage.Length - fullwebpage.IndexOf("Label</a></th>"))
                        tempstring = tempstring.Substring(tempstring.IndexOf("<td>"), tempstring.IndexOf("</td>") - tempstring.IndexOf("<td>"))
                        tempstring = Regex.Replace(tempstring, "<.*?>", "")
                        musicVideoTitle.fullmoviebody.studio = tempstring
                    Catch
                    End Try
                End If

                If fullwebpage.IndexOf("class=""image"">") <> -1 Then
                    Try
                        Dim tempstring As String = fullwebpage.Substring(fullwebpage.IndexOf("class=""image"">"), fullwebpage.Length - fullwebpage.IndexOf("class=""image"">"))
                        tempstring = tempstring.Substring(0, tempstring.IndexOf("></a></td>"))
                        If tempstring.IndexOf(".jpg") <> -1 Then
                            tempstring = tempstring.Substring(tempstring.LastIndexOf("//upload"), tempstring.LastIndexOf(".jpg") - tempstring.LastIndexOf("//upload") + 4)
                        ElseIf tempstring.IndexOf(".png") <> -1 Then
                            tempstring = tempstring.Substring(tempstring.LastIndexOf("//upload"), tempstring.LastIndexOf(".png") - tempstring.LastIndexOf("//upload") + 4)
                        End If
                        tempstring = "http:" & tempstring
                        musicVideoTitle.listthumbs.Add(tempstring)
                    Catch
                    End Try
                End If
            Catch
                'If musicVideoTitle.fullmoviebody.album = Nothing Then musicVideoTitle.fullmoviebody.album = "Unknown"
                'If musicVideoTitle.fullmoviebody.year = Nothing Then musicVideoTitle.fullmoviebody.year = "Unknown"
                'If musicVideoTitle.fullmoviebody.director = Nothing Then musicVideoTitle.fullmoviebody.director = "Unknown"
                'If musicVideoTitle.fullmoviebody.genre = Nothing Then musicVideoTitle.fullmoviebody.genre = "Unknown"
                'If musicVideoTitle.fullmoviebody.plot = Nothing Then musicVideoTitle.fullmoviebody.plot = "Unknown"
                'If musicVideoTitle.fullmoviebody.runtime = Nothing Then musicVideoTitle.fullmoviebody.runtime = "Unknown"
                'If musicVideoTitle.fullmoviebody.studio = Nothing Then musicVideoTitle.fullmoviebody.studio = "Unknown"
            End Try
        End If

        If musicVideoTitle.fullmoviebody.album = Nothing Then musicVideoTitle.fullmoviebody.album = "Unknown"
        If musicVideoTitle.fullmoviebody.year = Nothing Then musicVideoTitle.fullmoviebody.year = "Unknown"
        If musicVideoTitle.fullmoviebody.director = Nothing Then musicVideoTitle.fullmoviebody.director = "Unknown"
        If musicVideoTitle.fullmoviebody.genre = Nothing Then musicVideoTitle.fullmoviebody.genre = "Unknown"
        If musicVideoTitle.fullmoviebody.plot = Nothing Then musicVideoTitle.fullmoviebody.plot = "Unknown"
        If musicVideoTitle.fullmoviebody.runtime = Nothing Then musicVideoTitle.fullmoviebody.runtime = "Unknown"
        If musicVideoTitle.fullmoviebody.studio = Nothing Then musicVideoTitle.fullmoviebody.studio = "Unknown"

        Return musicVideoTitle

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
