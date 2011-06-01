Imports System.Threading
Imports System.Net
Imports System.IO
Imports System.Xml
Public Class actors
    Public Function EpisodeGetImdbActors(ByVal imbdID As String, ByVal seasonNO As String, _
                                         ByVal episodeNO As String) As List(Of movieactors)
        Dim tempactorlist As New List(Of MovieActors)
        Monitor.Enter(Me)
        Try
            tempactorlist.Clear()
            Try
                Dim url As String
                url = "http://www.imdb.com/title/" & imbdID & "/episodes"
                Dim tvfblinecount As Integer = 0
                Dim tvdbwebsource(10000) As String
                tvfblinecount = 0
                Try
                    Dim wrGETURL As WebRequest
                    wrGETURL = WebRequest.Create(url)
                    Dim myProxy As New WebProxy("myproxy", 80)
                    myProxy.BypassProxyOnLocal = True
                    Dim objStream As Stream
                    objStream = wrGETURL.GetResponse.GetResponseStream()
                    Dim objReader As New StreamReader(objStream)
                    Dim tvdbsLine As String = ""
                    tvfblinecount = 0

                    Do While Not tvdbsLine Is Nothing
                        tvfblinecount += 1
                        tvdbsLine = objReader.ReadLine
                        If Not tvdbsLine Is Nothing Then
                            tvdbwebsource(tvfblinecount) = tvdbsLine
                        End If
                    Loop
                    objReader.Close()
                    tvfblinecount -= 1
                Catch ex As WebException
                    tvdbwebsource(0) = "404"
                End Try




                If tvfblinecount <> 0 Then
                    Dim tvtempstring As String
                    tvtempstring = "Season " & seasonNO & ", Episode " & episodeNO & ":"
                    For g = 1 To tvfblinecount
                        If tvdbwebsource(g).IndexOf(tvtempstring) <> -1 Then
                            Dim tvtempint As Integer
                            tvtempint = tvdbwebsource(g).IndexOf("<a href=""/title/")
                            If tvtempint <> -1 Then
                                tvtempstring = tvdbwebsource(g).Substring(tvtempint + 16, 9)
                                '            Dim scraperfunction As New imdb.Classimdbscraper ' add to comment this one because of changes i made to the Class "Scraper" (ClassimdbScraper)
                                Dim scraperfunction As New Classimdb
                                Dim actorlist As String = ""
                                actorlist = scraperfunction.getimdbactors(Form1.userPrefs.imdbmirror, tvtempstring, , Form1.userPrefs.maxactors)
                                Dim thumbstring As New XmlDocument
                                Dim thisresult As XmlNode = Nothing
                                Try
                                    thumbstring.LoadXml(actorlist)
                                    thisresult = Nothing
                                    Dim countactors As Integer = 0
                                    For Each thisresult In thumbstring("actorlist")
                                        Select Case thisresult.Name
                                            Case "actor"
                                                If countactors >= Form1.userPrefs.maxactors Then
                                                    Exit For
                                                End If
                                                countactors += 1
                                                Dim newactor As New MovieActors
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
                                                            newactor.actorid = detail.InnerText
                                                    End Select
                                                Next
                                                tempactorlist.Add(newactor)
                                        End Select
                                    Next
                                Catch ex As Exception

                                End Try

                                While tempactorlist.Count > Form1.userPrefs.maxactors
                                    tempactorlist.RemoveAt(tempactorlist.Count - 1)
                                End While

                            End If

                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
            Return tempactorlist
        Catch
        Finally
            Monitor.Exit(Me)
        End Try
        Return tempactorlist
    End Function

    Public Sub savelocalactors(ByVal path As String, ByVal listofactors As List(Of MovieActors), Optional ByVal tvshowpath As String = "", Optional ByVal copyactorthumbs As Boolean = False)
        Monitor.Enter(Me)
        Try
            If Form1.userPrefs.actorseasy = True Then
                Dim workingpath As String = path.Replace(IO.Path.GetFileName(path), "")
                workingpath = workingpath & ".actors\"
                Dim hg As New IO.DirectoryInfo(workingpath)
                Dim destsorted As Boolean = False
                If Not hg.Exists Then
                    Try
                        IO.Directory.CreateDirectory(workingpath)
                        destsorted = True
                    Catch ex As Exception

                    End Try
                Else
                    destsorted = True
                End If
                If destsorted = True Then
                    For Each actor In listofactors
                        Dim filename As String = actor.actorname.Replace(" ", "_")
                        filename = filename & ".tbn"
                        Dim filename3 As String = filename
                        filename = IO.Path.Combine(workingpath, filename)

                        If tvshowpath <> "" Then
                            Dim tvshowactorpath As String = tvshowpath
                            tvshowactorpath = tvshowactorpath.Replace(IO.Path.GetFileName(tvshowactorpath), "")
                            tvshowactorpath = IO.Path.Combine(tvshowactorpath, ".actors\")
                            tvshowactorpath = IO.Path.Combine(tvshowactorpath, filename3)

                            'filename = IO.Path.Combine(workingpath, filename)
                            If copyactorthumbs = True Then
                                If IO.File.Exists(tvshowactorpath) Then
                                    Try
                                        IO.File.Copy(tvshowactorpath, filename, True)
                                    Catch
                                    End Try
                                End If
                            End If
                        End If
                        If actor.actorthumb <> Nothing Then
                            If actor.actorthumb <> "" And actor.actorthumb.IndexOf("http") <> -1 And actor.actorthumb.IndexOf(".jpg") <> -1 Then
                                If Not IO.File.Exists(filename) Then
                                    Dim buffer(4000000) As Byte
                                    Dim size As Integer = 0
                                    Dim bytesRead As Integer = 0
                                    Dim thumburl As String = actor.actorthumb
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
                    Next
                End If
            End If




        Catch ex As Exception
        Finally
            Monitor.Exit(Me)
        End Try
    End Sub


End Class
