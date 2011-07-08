Imports System.Xml
Imports System.IO
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class WorkingWithNfoFiles
    Const SetDefaults = True
    'Public Function ChangeFieldTVShow(ByVal Filename As String, ByVal Field As String, ByVal ValueToAssign As String, Optional ByVal CreateIfMissing As Boolean = False) As String
    '    Dim m_xmld As XmlDocument
    '    Dim m_nodelist As XmlNodeList
    '    Dim m_node As XmlNode
    '    Dim FindField As Boolean = False
    '    Dim FinalString As String = "<tvshow>"

    '    m_xmld = New XmlDocument()
    '    m_xmld.Load(Filename)
    '    m_nodelist = m_xmld.SelectNodes("/tvshow")
    '    For Each m_node In m_nodelist
    '        If m_node.HasChildNodes Then
    '            For Each node1 As XmlNode In m_node
    '                If node1.Name.ToLower = Field.ToLower Then
    '                    node1.InnerText = ValueToAssign
    '                    FindField = True
    '                End If
    '                FinalString &= node1.OuterXml.ToString
    '            Next
    '        End If
    '    Next
    '    If (FindField = False) And (CreateIfMissing = True) Then
    '        FinalString &= "<" & Field & ">" & ValueToAssign & "</" & Field & ">"
    '    End If
    '    FinalString &= "</tvshow>"
    '    Return FinalString
    'End Function
    'Public Function ChangeAllFieldsTVShow(ByVal TempTVShow As TvShow) As String
    '    Dim m_xmld As XmlDocument
    '    Dim m_nodelist As XmlNodeList
    '    Dim m_node As XmlNode
    '    Dim FinalString As String = "<tvshow>"

    '    m_xmld = New XmlDocument()
    '    m_xmld.Load(TempTVShow.path)
    '    m_nodelist = m_xmld.SelectNodes("/tvshow")
    '    For Each m_node In m_nodelist
    '        If m_node.HasChildNodes Then
    '            For Each node1 As XmlNode In m_node
    '                Select Case node1.Name.ToLower
    '                    Case "title"
    '                        node1.InnerText = TempTVShow.title
    '                    Case "plot"
    '                        node1.InnerText = TempTVShow.plot
    '                    Case "runtime"
    '                        node1.InnerText = TempTVShow.runtime
    '                    Case "premiered"
    '                        node1.InnerText = TempTVShow.premiered
    '                    Case "studio"
    '                        node1.InnerText = TempTVShow.studio
    '                    Case "rating"
    '                        node1.InnerText = TempTVShow.rating
    '                    Case "id"
    '                        node1.InnerText = TempTVShow.imdbid
    '                    Case "tvdbid"
    '                        node1.InnerText = TempTVShow.tvdbid
    '                    Case "mpaa"
    '                        node1.InnerText = TempTVShow.mpaa
    '                End Select
    '                FinalString &= node1.OuterXml.ToString
    '            Next
    '        End If
    '    Next
    '    FinalString &= "</tvshow>"
    '    Return FinalString
    'End Function
    'Public Function ChangeFieldEpisodeTVShow(ByVal Filename As String, ByVal Field As String, ByVal ValueToAssign As String) As String
    '    Dim m_xmld As XmlDocument
    '    Dim m_nodelist As XmlNodeList
    '    Dim m_node As XmlNode
    '    Dim FinalString As String = "<episodedetails>"

    '    m_xmld = New XmlDocument()
    '    m_xmld.Load(Filename)
    '    m_nodelist = m_xmld.SelectNodes("/episodedetails")
    '    For Each m_node In m_nodelist
    '        If m_node.HasChildNodes Then
    '            For Each node1 As XmlNode In m_node
    '                If node1.Name.ToLower = Field.ToLower Then
    '                    node1.InnerText = ValueToAssign
    '                End If
    '                FinalString &= node1.OuterXml.ToString
    '            Next
    '        End If
    '    Next
    '    FinalString &= "</episodedetails>"
    '    Return FinalString
    'End Function
    'Public Function ChangeAllFieldsEpisodeTVShow(ByVal TempEpisode As TvEpisode) As String
    '    Dim m_xmld As XmlDocument
    '    Dim m_nodelist As XmlNodeList
    '    Dim m_node As XmlNode
    '    Dim FinalString As String = "<episodedetails>"

    '    m_xmld = New XmlDocument()
    '    m_xmld.Load(TempEpisode.episodepath)
    '    m_nodelist = m_xmld.SelectNodes("/episodedetails")
    '    For Each m_node In m_nodelist
    '        If m_node.HasChildNodes Then
    '            For Each node1 As XmlNode In m_node
    '                'If node1.Name.ToLower = Field.ToLower Then
    '                '    node1.InnerText = ValueToAssign
    '                'End If
    '                'FinalString &= node1.OuterXml.ToString
    '                Select Case node1.Name.ToLower
    '                    Case "title"
    '                        node1.InnerText = TempEpisode.title
    '                    Case "plot"
    '                        node1.InnerText = TempEpisode.plot
    '                    Case "aired"
    '                        node1.InnerText = TempEpisode.aired
    '                    Case "rating"
    '                        node1.InnerText = TempEpisode.rating
    '                End Select
    '                FinalString &= node1.OuterXml.ToString
    '            Next
    '        End If
    '    Next
    '    FinalString &= "</episodedetails>"
    '    Return FinalString
    'End Function

    Public Function validate_nfo(ByVal nfopath As String)
        Dim tempstring As String
        Dim filechck As IO.StreamReader = IO.File.OpenText(nfopath)
        tempstring = filechck.ReadToEnd.ToLower
        filechck.Close()
        If tempstring = Nothing Then
            Return False
        End If
        If tempstring.IndexOf("<movie>") <> -1 And tempstring.IndexOf("</movie>") <> -1 And tempstring.IndexOf("<title>") <> -1 And tempstring.IndexOf("</title>") <> -1 Then
            Return True
            Exit Function
        End If
        Return False
    End Function

    Public Function loadbasictvshownfo(ByVal path As String) As TvShow

        Dim newtvshow As New TvShow
        If Not IO.File.Exists(path) Then
            'Form1.tvrebuildlog(path & ", does not appear to exist")
            newtvshow.title = Utilities.GetLastFolder(path)
            newtvshow.year = newtvshow.title & " (0000)"
            newtvshow.fullpath = path
            newtvshow.year = "0000"
            newtvshow.tvdbid = ""
            newtvshow.status = "missing"
            newtvshow.locked = 1
            Return newtvshow
            Exit Function
        Else
            newtvshow.NfoFilePath = path
            newtvshow.Load()
            '    Dim tvshow As New XmlDocument
            '    Try
            '        tvshow.Load(path)
            '    Catch ex As Exception
            '        'Form1.tvrebuildlog(path & ", seems to be an invalid xml file")
            '        'If Not validate_nfo(path) Then
            '        '    Exit Function
            '        'End If
            '        newtvshow.title = Utilities.GetLastFolder(path)
            '        newtvshow.year = newtvshow.title & " (0000)"
            '        newtvshow.status = "xml error"
            '        newtvshow.fullpath = path
            '        newtvshow.year = "0000"
            '        newtvshow.tvdbid = ""
            '        newtvshow.status = "xml error"
            '        newtvshow.locked = True
            '        Return newtvshow




            '        Exit Function
            '    End Try
            '    newtvshow.status = "ok"
            '    newtvshow.locked = False
            '    Dim thisresult As XmlNode = Nothing
            '    Dim tempid As String = ""
            '    For Each thisresult In tvshow("tvshow")
            '        Try
            '            Select Case thisresult.Name
            '                Case "title"
            '                    Dim tempstring As String = ""
            '                    tempstring = thisresult.InnerText
            '                    '-------------- Aqui
            '                    If Preferences.ignorearticle = True Then
            '                        If tempstring.ToLower.IndexOf("the ") = 0 Then
            '                            tempstring = tempstring.Substring(4, tempstring.Length - 4)
            '                            tempstring = tempstring & ", The"
            '                        End If
            '                    End If
            '                    newtvshow.title = tempstring
            '                Case "episodeguideurl"
            '                    tempid = thisresult.InnerText
            '                Case "year"
            '                    newtvshow.year = thisresult.InnerText
            '                Case "episodeactorsource"
            '                    newtvshow.episodeactorsource = thisresult.InnerText
            '                Case "genre"
            '                    newtvshow.genre = thisresult.InnerText
            '                Case "tvdbid"
            '                    newtvshow.tvdbid = thisresult.InnerText
            '                Case "id"
            '                    newtvshow.imdbid = thisresult.InnerText
            '                Case "rating"
            '                    newtvshow.rating = thisresult.InnerText
            '                    If newtvshow.rating.IndexOf("/10") <> -1 Then newtvshow.rating.Replace("/10", "")
            '                    If newtvshow.rating.IndexOf(" ") <> -1 Then newtvshow.rating.Replace(" ", "")
            '                Case "sortorder"
            '                    newtvshow.sortorder = thisresult.InnerText
            '                Case "language"
            '                    newtvshow.language = thisresult.InnerText
            '                Case "locked"
            '                    If thisresult.InnerText = "true" Then
            '                        newtvshow.locked = 1
            '                    ElseIf thisresult.InnerText = "false" Then
            '                        newtvshow.locked = 0
            '                    ElseIf thisresult.InnerText = "2" Then
            '                        newtvshow.locked = 2
            '                    Else
            '                        If IsNumeric(thisresult.InnerText) Then
            '                            newtvshow.locked = Convert.ToInt32(thisresult.InnerText)
            '                        Else
            '                            newtvshow.locked = 1
            '                        End If
            '                    End If
            '            End Select
            '        Catch ex As Exception
            '            MsgBox(ex.ToString)
            '        End Try
            '    Next
            '    'Form1.tvrebuildlog(path & ", loaded ok")
            '    'Form1.tvrebuildlog("title= " & newtvshow.title)
            '    'Form1.tvrebuildlog("tvdbid= " & newtvshow.tvdbid)
            '    If newtvshow.tvdbid = Nothing Then
            '        If tempid <> Nothing Then
            '            Dim tempstring As String = tempid.Substring(tempid.LastIndexOf("series/") + 7, tempid.Length - (tempid.LastIndexOf("series/") + 7))
            '            tempstring = tempstring.Substring(0, tempstring.IndexOf("/"))
            '            If tempstring.Length = 5 Then
            '                If IsNumeric(tempstring) Then
            '                    newtvshow.tvdbid = tempstring
            '                End If
            '            End If
            '        End If
            '    End If
            '    newtvshow.fullpath = path

            '    Dim filecreation As New FileInfo(path)
            '    Dim myDate As Date = filecreation.LastWriteTime

            '    If Not (newtvshow.title <> Nothing And newtvshow.year <> Nothing) Then

            '        newtvshow.year = "(0000)"
            '    End If

            '    If newtvshow.tvdbid = Nothing Then newtvshow.tvdbid = ""
            '    If newtvshow.genre = Nothing Then newtvshow.genre = ""
            '    If newtvshow.rating = Nothing Then newtvshow.rating = ""
        End If
        Return newtvshow


    End Function

    Public Function loadbasicepisodenfo(ByVal path As String)
        'Try
        Dim episodelist As New List(Of TvEpisode)

        Dim newtvshow As New TvEpisode
        If Not IO.File.Exists(path) Then
            Return "Error"
        Else
            Dim tvshow As New XmlDocument
            Try
                tvshow.Load(path)
            Catch ex As Exception
                'If Not validate_nfo(path) Then
                '    Exit Function
                'End If
                newtvshow.title = IO.Path.GetFileName(path)
                newtvshow.title = newtvshow.title.Replace(IO.Path.GetExtension(newtvshow.title), "")
                newtvshow.imdbid = "xml error"
                newtvshow.episodepath = path
                newtvshow.tvdbid = ""
                newtvshow.episodepath = path
                If newtvshow.episodeno = Nothing Or newtvshow.episodeno = Nothing Then
                    For Each regexp In Form1.tv_RegexScraper

                        Dim M As Match
                        M = Regex.Match(newtvshow.episodepath, regexp)
                        If M.Success = True Then
                            Try
                                newtvshow.seasonno = M.Groups(1).Value.ToString
                                newtvshow.episodeno = M.Groups(2).Value.ToString
                                Exit For
                            Catch
                                newtvshow.seasonno = "-1"
                                newtvshow.seasonno = "-1"
                            End Try
                        End If
                    Next
                End If
                If newtvshow.episodeno = Nothing Then
                    newtvshow.episodeno = "-1"
                End If
                If newtvshow.seasonno = Nothing Then
                    newtvshow.seasonno = "-1"
                End If
                If newtvshow.seasonno.IndexOf("0") = 0 Then
                    newtvshow.seasonno = newtvshow.seasonno.Substring(1, 1)
                End If
                If newtvshow.episodeno.IndexOf("0") = 0 Then
                    newtvshow.episodeno = newtvshow.episodeno.Substring(1, 1)
                End If

                episodelist.Add(newtvshow)





                Return episodelist




                Exit Function
            End Try

            Dim thisresult As XmlNode = Nothing
            Dim tempid As String = ""
            If tvshow.DocumentElement.Name = "episodedetails" Then
                Dim newtvepisode As New TvEpisode
                For Each thisresult In tvshow("episodedetails")
                    Try
                        newtvepisode.episodepath = path
                        Select Case thisresult.Name
                            Case "title"
                                newtvepisode.title = thisresult.InnerText
                            Case "season"
                                newtvepisode.seasonno = thisresult.InnerText
                            Case "episode"
                                newtvepisode.episodeno = thisresult.InnerText
                            Case "tvdbid"
                                newtvepisode.tvdbid = thisresult.InnerText
                            Case "rating"
                                newtvepisode.rating = thisresult.InnerText
                                If newtvepisode.rating.IndexOf("/10") <> -1 Then newtvepisode.rating.Replace("/10", "")
                                If newtvepisode.rating.IndexOf(" ") <> -1 Then newtvepisode.rating.Replace(" ", "")
                            Case "playcount"
                                newtvepisode.playcount = thisresult.InnerText
                            Case "aired"
                                newtvepisode.aired = thisresult.InnerText
                        End Select

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Next

                If newtvepisode.episodeno = Nothing Or newtvepisode.episodeno = Nothing Then
                    For Each regexp In Form1.tv_RegexScraper

                        Dim M As Match
                        M = Regex.Match(newtvepisode.episodepath, regexp)
                        If M.Success = True Then
                            Try
                                newtvepisode.seasonno = M.Groups(1).Value.ToString
                                newtvepisode.episodeno = M.Groups(2).Value.ToString
                                Exit For
                            Catch
                                newtvepisode.seasonno = "-1"
                                newtvepisode.seasonno = "-1"
                            End Try
                        End If
                    Next
                End If
                If newtvepisode.episodeno = Nothing Then
                    newtvepisode.episodeno = "-1"
                End If
                If newtvepisode.seasonno = Nothing Then
                    newtvepisode.seasonno = "-1"
                End If
                If newtvepisode.tvdbid = Nothing Then newtvepisode.tvdbid = ""
                'If newtvepisode.status = Nothing Then newtvepisode.status = ""
                If newtvepisode.rating = Nothing Then newtvepisode.rating = ""
                episodelist.Add(newtvepisode)
            ElseIf tvshow.DocumentElement.Name = "multiepisodenfo" Or tvshow.DocumentElement.Name = "xbmcmultiepisode" Then
                Dim temp As String = tvshow.DocumentElement.Name
                For Each thisresult In tvshow(temp)
                    Select Case thisresult.Name
                        Case "episodedetails"
                            Dim newepisodenfo As XmlNode = Nothing
                            Dim anotherepisode As New TvEpisode

                            anotherepisode.episodepath = ""                 'Nothing
                            'anotherepisode.status = ""                     'Nothing
                            anotherepisode.playcount = ""                   'Nothing
                            anotherepisode.rating = ""                      'Nothing
                            anotherepisode.seasonno = ""                    'Nothing
                            anotherepisode.title = ""                       'Nothing
                            anotherepisode.tvdbid = ""                      'Nothing
                            ' For Each newepisodenfo In thisresult.ChildNodes
                            Dim tempint As Integer = thisresult.ChildNodes.Count - 1
                            For f = 0 To tempint
                                Try
                                    Select Case thisresult.ChildNodes(f).Name
                                        Case "title"
                                            anotherepisode.title = thisresult.ChildNodes(f).InnerText
                                        Case "season"
                                            anotherepisode.seasonno = thisresult.ChildNodes(f).InnerText
                                        Case "episode"
                                            anotherepisode.episodeno = thisresult.ChildNodes(f).InnerText
                                        Case "tvdbid"
                                            anotherepisode.tvdbid = thisresult.ChildNodes(f).InnerText
                                        Case "rating"
                                            anotherepisode.rating = thisresult.ChildNodes(f).InnerText
                                            If anotherepisode.rating.IndexOf("/10") <> -1 Then anotherepisode.rating.Replace("/10", "")
                                            If anotherepisode.rating.IndexOf(" ") <> -1 Then anotherepisode.rating.Replace(" ", "")
                                        Case "playcount"
                                            anotherepisode.playcount = thisresult.ChildNodes(f).InnerText

                                    End Select
                                Catch ex As Exception
                                    MsgBox(ex.ToString)
                                End Try
                            Next f
                            Try
                                anotherepisode.episodepath = path
                                If anotherepisode.episodeno = Nothing Or anotherepisode.episodeno = Nothing Then
                                    For Each regexp In Form1.tv_RegexScraper

                                        Dim M As Match
                                        M = Regex.Match(anotherepisode.episodepath, regexp)
                                        If M.Success = True Then
                                            Try
                                                anotherepisode.seasonno = M.Groups(1).Value.ToString
                                                anotherepisode.episodeno = M.Groups(2).Value.ToString
                                                Exit For
                                            Catch
                                                anotherepisode.seasonno = "-1"
                                                anotherepisode.seasonno = "-1"
                                            End Try
                                        End If
                                    Next
                                End If
                                If anotherepisode.episodeno = Nothing Then
                                    anotherepisode.episodeno = "-1"
                                End If
                                If anotherepisode.seasonno = Nothing Then
                                    anotherepisode.seasonno = "-1"
                                End If
                                If anotherepisode.tvdbid = Nothing Then anotherepisode.tvdbid = ""
                                'If anotherepisode.status = Nothing Then anotherepisode.status = ""
                                If anotherepisode.rating = Nothing Then anotherepisode.rating = ""
                                episodelist.Add(anotherepisode)
                            Catch ex As Exception
                                MsgBox(ex.ToString)
                            End Try
                    End Select
                Next
            End If

            Return episodelist
        End If

        'Catch
        'End Try
    End Function

    'Public Function loadfullepisodenfo(ByVal path As String) ', ByVal season As String, ByVal episode As String)

    '    Form1.workingEpisode.Clear()
    '    Dim newepisode As New TvEpisode
    '    If Not IO.File.Exists(path) Then
    '        newepisode.title = IO.Path.GetFileName(path)
    '        newepisode.plot = "missing file"
    '        newepisode.episodepath = path
    '        newepisode.episodepath = path
    '        If newepisode.episodeno = Nothing Or newepisode.episodeno = Nothing Then
    '            For Each regexp In Form1.tvRegex

    '                Dim M As Match
    '                M = Regex.Match(newepisode.episodepath, regexp)
    '                If M.Success = True Then
    '                    Try
    '                        newepisode.seasonno = M.Groups(1).Value.ToString
    '                        newepisode.episodeno = M.Groups(2).Value.ToString
    '                        Exit For
    '                    Catch
    '                        newepisode.seasonno = "-1"
    '                        newepisode.seasonno = "-1"
    '                    End Try
    '                End If
    '            Next
    '        End If
    '        If newepisode.episodeno = Nothing Then
    '            newepisode.episodeno = "-1"
    '        End If
    '        If newepisode.seasonno = Nothing Then
    '            newepisode.seasonno = "-1"
    '        End If
    '        Form1.workingEpisode.Add(newepisode)
    '        Return ""
    '        Exit Function
    '    Else
    '        Dim tvshow As New XmlDocument
    '        Try
    '            tvshow.Load(path)
    '        Catch ex As Exception
    '            'If Not validate_nfo(path) Then
    '            '    Exit Function
    '            'End If
    '            newepisode.title = IO.Path.GetFileName(path)
    '            newepisode.plot = "problem / xml error"
    '            newepisode.episodepath = path
    '            newepisode.episodepath = path
    '            If newepisode.episodeno = Nothing Or newepisode.episodeno = Nothing Then
    '                For Each regexp In Form1.tvRegex

    '                    Dim M As Match
    '                    M = Regex.Match(newepisode.episodepath, regexp)
    '                    If M.Success = True Then
    '                        Try
    '                            newepisode.seasonno = M.Groups(1).Value.ToString
    '                            newepisode.episodeno = M.Groups(2).Value.ToString
    '                            Exit For
    '                        Catch
    '                            newepisode.seasonno = "-1"
    '                            newepisode.seasonno = "-1"
    '                        End Try
    '                    End If
    '                Next
    '            End If
    '            If newepisode.episodeno = Nothing Then
    '                newepisode.episodeno = "-1"
    '            End If
    '            If newepisode.seasonno = Nothing Then
    '                newepisode.seasonno = "-1"
    '            End If
    '            Form1.workingEpisode.Add(newepisode)
    '            Return ""
    '            Exit Function
    '        End Try

    '        Dim thisresult As XmlNode = Nothing
    '        Dim tempid As String = ""
    '        If tvshow.DocumentElement.Name = "episodedetails" Then
    '            Dim newtvepisode As New TvEpisode
    '            For Each thisresult In tvshow("episodedetails")
    '                Try
    '                    newtvepisode.episodepath = path
    '                    Select Case thisresult.Name
    '                        Case "credits"
    '                            newtvepisode.credits = thisresult.InnerText
    '                        Case "director"
    '                            newtvepisode.director = thisresult.InnerText
    '                        Case "aired"
    '                            newtvepisode.aired = thisresult.InnerText
    '                        Case "plot"
    '                            newtvepisode.plot = thisresult.InnerText
    '                        Case "title"
    '                            newtvepisode.title = thisresult.InnerText
    '                        Case "season"
    '                            newtvepisode.seasonno = thisresult.InnerText
    '                        Case "episode"
    '                            newtvepisode.episodeno = thisresult.InnerText
    '                        Case "rating"
    '                            newtvepisode.rating = thisresult.InnerText
    '                            If newtvepisode.rating.IndexOf("/10") <> -1 Then newtvepisode.rating.Replace("/10", "")
    '                            If newtvepisode.rating.IndexOf(" ") <> -1 Then newtvepisode.rating.Replace(" ", "")
    '                        Case "playcount"
    '                            newtvepisode.playcount = thisresult.InnerText
    '                        Case "thumb"
    '                            newtvepisode.thumb = thisresult.InnerText
    '                        Case "actor"
    ''                            Dim actordetail As XmlNode = Nothing
    '                            Dim newactor As New str_MovieActors(SetDefaults)
    ''                            For Each actordetail In thisresult.ChildNodes
    '                                Select Case actordetail.Name
    '                                    Case "name"
    '                                        newactor.actorname = actordetail.InnerText
    '                                    Case "role"
    '                                        newactor.actorrole = actordetail.InnerText
    '                                    Case "thumb"
    '                                        newactor.actorthumb = actordetail.InnerText
    '                                End Select
    '                            Next
    '                            newtvepisode.ListActors.Add(newactor)
    '                        Case "fileinfo"
    '                            Dim detail2 As XmlNode = Nothing
    '                            For Each detail2 In thisresult.ChildNodes
    '                                Select Case detail2.Name
    '                                    Case "streamdetails"
    '                                        Dim newfilenfo As New FullFileDetails
    '                                        Dim detail As XmlNode = Nothing
    '                                        For Each detail In detail2.ChildNodes
    '                                            Select Case detail.Name
    '                                                Case "video"
    '                                                    Dim videodetails As XmlNode = Nothing
    '                                                    For Each videodetails In detail.ChildNodes
    '                                                        Select Case videodetails.Name
    '                                                            Case "width"
    '                                                                newfilenfo.filedetails_video.width = videodetails.InnerText
    '                                                            Case "height"
    '                                                                newfilenfo.filedetails_video.height = videodetails.InnerText
    '                                                            Case "aspect"
    '                                                                newfilenfo.filedetails_video.aspect = videodetails.InnerText
    '                                                            Case "codec"
    '                                                                newfilenfo.filedetails_video.codec = videodetails.InnerText
    '                                                            Case "formatinfo"
    '                                                                newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
    '                                                            Case "duration"
    '                                                                newfilenfo.filedetails_video.duration = videodetails.InnerText
    '                                                            Case "bitrate"
    '                                                                newfilenfo.filedetails_video.bitrate = videodetails.InnerText
    '                                                            Case "bitratemode"
    '                                                                newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
    '                                                            Case "bitratemax"
    '                                                                newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
    '                                                            Case "container"
    '                                                                newfilenfo.filedetails_video.container = videodetails.InnerText
    '                                                            Case "codecid"
    '                                                                newfilenfo.filedetails_video.codecid = videodetails.InnerText
    '                                                            Case "codecidinfo"
    '                                                                newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
    '                                                            Case "scantype"
    '                                                                newfilenfo.filedetails_video.scantype = videodetails.InnerText
    '                                                        End Select
    ''                                                    Next
    ''                                                Case "audio"
    ''                                                    Dim audiodetails As XmlNode = Nothing
    '                                                    Dim audio As New str_MediaNFOAudio(SetDefaults)
    ''                                                    For Each audiodetails In detail.ChildNodes
    ''                                                        Select Case audiodetails.Name
    ''                                                            Case "language"
    ''                                                                audio.language = audiodetails.InnerText
    ''                                                            Case "codec"
    ''                                                                audio.codec = audiodetails.InnerText
    ''                                                            Case "channels"
    ''                                                                audio.channels = audiodetails.InnerText
    ''                                                            Case "bitrate"
    ''                                                                audio.bitrate = audiodetails.InnerText
    ''                                                        End Select
    ''                                                    Next
    ''                                                    newfilenfo.filedetails_audio.Add(audio)
    ''                                                Case "subtitle"
    ''                                                    Dim subsdetails As XmlNode = Nothing
    ''                                                    For Each subsdetails In detail.ChildNodes
    ''                                                        Select Case subsdetails.Name
    ''                                                            Case "language"
    '                                                                Dim sublang As New str_MediaNFOSubtitles(SetDefaults)
    ''                                                                sublang.language = subsdetails.InnerText
    ''                                                                newfilenfo.filedetails_subtitles.Add(sublang)
    '                                                        End Select
    '                                                    Next
    '                                            End Select
    '                                        Next
    '                                        newtvepisode.filedetails = newfilenfo
    '                                End Select
    '                            Next
    '                    End Select
    '                Catch ex As Exception
    '                    MsgBox(ex.ToString)
    '                End Try
    '            Next

    '            If newtvepisode.episodeno = Nothing Or newtvepisode.episodeno = Nothing Then
    '                For Each regexp In Form1.tvRegex

    '                    Dim M As Match
    '                    M = Regex.Match(newtvepisode.episodepath, regexp)
    '                    If M.Success = True Then
    '                        Try
    '                            newtvepisode.seasonno = M.Groups(1).Value.ToString
    '                            newtvepisode.episodeno = M.Groups(2).Value.ToString
    '                            Exit For
    '                        Catch
    '                            newtvepisode.seasonno = "-1"
    '                            newtvepisode.seasonno = "-1"
    '                        End Try
    '                    End If
    '                Next
    '            End If
    '            If newtvepisode.episodeno = Nothing Then
    '                newtvepisode.episodeno = "-1"
    '            End If
    '            If newtvepisode.seasonno = Nothing Then
    '                newtvepisode.seasonno = "-1"
    '            End If
    '            If newtvepisode.rating = Nothing Then newtvepisode.rating = ""
    '            Form1.workingEpisode.Add(newtvepisode)
    '            Return ""
    '            Exit Function
    '        ElseIf tvshow.DocumentElement.Name = "multiepisodenfo" Then
    '            For Each thisresult In tvshow("multiepisodenfo")
    '                Select Case thisresult.Name
    '                    Case "episodedetails"
    '                        Dim newepisodenfo As XmlNode = Nothing
    '                        Dim anotherepisode As New TvEpisode

    '                        anotherepisode.episodepath = ""
    '                        anotherepisode.playcount = ""
    '                        anotherepisode.rating = ""
    '                        anotherepisode.seasonno = ""
    '                        anotherepisode.title = ""
    '                        ' For Each newepisodenfo In thisresult.ChildNodes
    '                        Dim tempint As Integer = thisresult.ChildNodes.Count - 1
    '                        For f = 0 To tempint
    '                            Try


    '                                'Public credits As String
    '                                'Public director As String
    '                                'Public aired As String
    '                                'Public plot As Integer
    '                                'Public fanartpath As String
    '                                'Public listactors As New List(Of movieactors)
    '                                'Public filedetails As New fullfiledetails


    '                                Select Case thisresult.ChildNodes(f).Name
    '                                    Case "credits"
    '                                        anotherepisode.credits = thisresult.ChildNodes(f).InnerText
    '                                    Case "director"
    '                                        anotherepisode.director = thisresult.ChildNodes(f).InnerText
    '                                    Case "aired"
    '                                        anotherepisode.aired = thisresult.ChildNodes(f).InnerText
    '                                    Case "plot"
    '                                        anotherepisode.plot = thisresult.ChildNodes(f).InnerText
    '                                    Case "title"
    '                                        anotherepisode.title = thisresult.ChildNodes(f).InnerText
    '                                    Case "season"
    '                                        anotherepisode.seasonno = thisresult.ChildNodes(f).InnerText
    '                                    Case "episode"
    '                                        anotherepisode.episodeno = thisresult.ChildNodes(f).InnerText
    '                                    Case "rating"
    '                                        anotherepisode.rating = thisresult.ChildNodes(f).InnerText
    '                                        If anotherepisode.rating.IndexOf("/10") <> -1 Then anotherepisode.rating.Replace("/10", "")
    '                                        If anotherepisode.rating.IndexOf(" ") <> -1 Then anotherepisode.rating.Replace(" ", "")
    '                                    Case "playcount"
    '                                        anotherepisode.playcount = thisresult.ChildNodes(f).InnerText
    '                                    Case "thumb"
    '                                        anotherepisode.thumb = thisresult.ChildNodes(f).InnerText
    '                                    Case "runtime"
    '                                        anotherepisode.Runtime.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "actor"
    '                                        Dim detail As XmlNode = Nothing
                                            Dim newactor As New str_MovieActors(SetDefaults)
    '                                        For Each detail In thisresult.ChildNodes(f).ChildNodes
    '                                            Select Case detail.Name
    '                                                Case "name"
    '                                                    newactor.actorname = detail.InnerText
    '                                                Case "role"
    '                                                    newactor.actorrole = detail.InnerText
    '                                                Case "thumb"
    '                                                    newactor.actorthumb = detail.InnerText
    '                                            End Select
    '                                        Next
    '                                        anotherepisode.ListActors.Add(newactor)
    '                                    Case "streamdetails"
    '                                        Dim detail2 As XmlNode = Nothing
    '                                        For Each detail2 In thisresult.ChildNodes(f).ChildNodes
    '                                            Select Case detail2.Name
    '                                                Case "fileinfo"
    '                                                    Dim newfilenfo As New FullFileDetails
    '                                                    Dim detail As XmlNode = Nothing
    '                                                    For Each detail In detail2.ChildNodes
    '                                                        Select Case detail.Name
    '                                                            Case "video"
    '                                                                Dim videodetails As XmlNode = Nothing
    '                                                                For Each videodetails In detail.ChildNodes
    '                                                                    Select Case videodetails.Name
    '                                                                        Case "width"
    '                                                                            newfilenfo.filedetails_video.width = videodetails.InnerText
    '                                                                        Case "height"
    '                                                                            newfilenfo.filedetails_video.height = videodetails.InnerText
    '                                                                        Case "codec"
    '                                                                            newfilenfo.filedetails_video.codec = videodetails.InnerText
    '                                                                        Case "formatinfo"
    '                                                                            newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
    '                                                                        Case "duration"
    '                                                                            newfilenfo.filedetails_video.duration = videodetails.InnerText
    '                                                                        Case "bitrate"
    '                                                                            newfilenfo.filedetails_video.bitrate = videodetails.InnerText
    '                                                                        Case "bitratemode"
    '                                                                            newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
    '                                                                        Case "bitratemax"
    '                                                                            newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
    '                                                                        Case "container"
    '                                                                            newfilenfo.filedetails_video.container = videodetails.InnerText
    '                                                                        Case "codecid"
    '                                                                            newfilenfo.filedetails_video.codecid = videodetails.InnerText
    '                                                                        Case "codecidinfo"
    '                                                                            newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
    '                                                                        Case "scantype"
    '                                                                            newfilenfo.filedetails_video.scantype = videodetails.InnerText
    ''                                                                    End Select
    ''                                                                Next
    ''                                                            Case "audio"
    ''                                                                Dim audiodetails As XmlNode = Nothing
    '                                                                Dim audio As New str_MediaNFOAudio(SetDefaults)
    ''                                                                For Each audiodetails In detail.ChildNodes

    '                                                                    Select Case audiodetails.Name
    '                                                                        Case "language"
    '                                                                            audio.language = audiodetails.InnerText
    '                                                                        Case "codec"
    '                                                                            audio.codec = audiodetails.InnerText
    '                                                                        Case "channels"
    '                                                                            audio.channels = audiodetails.InnerText
    '                                                                        Case "bitrate"
    '                                                                            audio.bitrate = audiodetails.InnerText
    '                                                                    End Select
    '                                                                Next
    '                                                                newfilenfo.filedetails_audio.Add(audio)
    '                                                            Case "subtitle"
    '                                                                Dim subsdetails As XmlNode = Nothing
    '                                                                For Each subsdetails In detail.ChildNodes
    '                                                                    Select Case subsdetails.Name
    ''                                                                        Case "language"
    '                                                                            Dim sublang As New str_MediaNFOSubtitles(SetDefaults)
    ''                                                                            sublang.language = subsdetails.InnerText
    ''                                                                            newfilenfo.filedetails_subtitles.Add(sublang)
    '                                                                    End Select
    '                                                                Next
    '                                                        End Select
    '                                                    Next
    '                                                    anotherepisode.filedetails = newfilenfo
    '                                            End Select
    '                                        Next
    '                                End Select


    '                            Catch ex As Exception
    '                                MsgBox(ex.ToString)
    '                            End Try
    '                        Next f
    '                        anotherepisode.episodepath = path
    '                        Form1.workingEpisode.Add(anotherepisode)
    '                End Select
    '            Next
    '        End If


    '    End If
    '    Return "Error"
    'End Function

    Public Function loadfulltnshownfo(ByVal path As String) As TvShow


        Dim newtvshow As New TvShow
        If Not IO.File.Exists(path) Then
            newtvshow.title = Utilities.GetLastFolder(path)
            newtvshow.year = newtvshow.title & " (0000)"
            newtvshow.plot = "problem loading tvshow.nfo, file does not exist." & vbCrLf & "Use the TV Show Selector Tab to create one"
            newtvshow.status = "file does not exist"
            newtvshow.path = path
            newtvshow.year = "0000"
            newtvshow.tvdbid = ""
            newtvshow.path = path
            newtvshow.locked = 1
            Return newtvshow
            Exit Function
        Else
            newtvshow.NfoFilePath = path
            newtvshow.Load()
            'Dim tvshow As New XmlDocument

            'Try
            '    tvshow.Load(path)
            'Catch ex As Exception
            '    'If Not validate_nfo(path) Then
            '    '    Exit Function
            '    'End If
            '    newtvshow.title = Utilities.GetLastFolder(path)
            '    newtvshow.year = newtvshow.title & " (0000)"
            '    newtvshow.plot = "problem loading tvshow.nfo / xml error"
            '    newtvshow.status = "xml error"
            '    newtvshow.path = path
            '    newtvshow.year = "0000"
            '    newtvshow.tvdbid = ""
            '    newtvshow.path = path
            '    newtvshow.locked = 1
            '    Return newtvshow




            '    Exit Function
            'End Try
            'newtvshow.genre = ""
            'newtvshow.status = "ok"
            'newtvshow.locked = False
            'Dim thisresult As XmlNode = Nothing
            'Dim tempid As String = ""
            'For Each thisresult In tvshow("tvshow")
            '    Try
            '        Select Case thisresult.Name
            '            Case "title"
            '                Dim tempstring As String = ""
            '                tempstring = thisresult.InnerText
            '                '-------------- Aqui
            '                If Preferences.ignorearticle = True Then
            '                    If tempstring.ToLower.IndexOf("the ") = 0 Then
            '                        tempstring = tempstring.Substring(4, tempstring.Length - 4)
            '                        tempstring = tempstring & ", The"
            '                    End If
            '                End If
            '                newtvshow.title = tempstring
            '            Case "episodeguide"
            '                newtvshow.episodeguideurl = thisresult.InnerText
            '            Case "plot"
            '                newtvshow.plot = thisresult.InnerText
            '            Case "year"
            '                newtvshow.year = thisresult.InnerText
            '            Case "genre"
            '                If newtvshow.genre = "" Then
            '                    newtvshow.genre = thisresult.InnerText
            '                Else
            '                    newtvshow.genre &= " / " & thisresult.InnerText
            '                End If
            '            Case "tvdbid"
            '                newtvshow.tvdbid = thisresult.InnerText
            '            Case "id"
            '                newtvshow.imdbid = thisresult.InnerText
            '            Case "rating"
            '                newtvshow.rating = thisresult.InnerText
            '                If newtvshow.rating.IndexOf("/10") <> -1 Then newtvshow.rating.Replace("/10", "")
            '                If newtvshow.rating.IndexOf(" ") <> -1 Then newtvshow.rating.Replace(" ", "")
            '            Case "mpaa"
            '                newtvshow.mpaa = thisresult.InnerText
            '            Case "runtime"
            '                newtvshow.runtime = thisresult.InnerText
            '            Case "genre"
            '                newtvshow.genre = thisresult.InnerText
            '            Case "premiered"
            '                newtvshow.premiered = thisresult.InnerText
            '            Case "studio"
            '                newtvshow.studio = thisresult.InnerText
            '            Case "language"
            '                newtvshow.language = thisresult.InnerText
            '            Case "sortorder"
            '                newtvshow.sortorder = thisresult.InnerText
            '            Case "episodeactorsource"
            '                newtvshow.episodeactorsource = thisresult.InnerText
            '            Case "tvshowactorsource"
            '                newtvshow.tvshowactorsource = thisresult.InnerText
            '            Case "actor"
            '                Dim detail As XmlNode = Nothing
            '                Dim newactor As New MovieActors
            '                For Each detail In thisresult.ChildNodes
            '                    Select Case detail.Name
            '                        Case "name"
            '                            newactor.actorname = detail.InnerText
            '                        Case "role"
            '                            newactor.actorrole = detail.InnerText
            '                        Case "thumb"
            '                            newactor.actorthumb = detail.InnerText
            '                    End Select
            '                Next
            '                newtvshow.listactors.Add(newactor)
            '            Case "locked"
            '                If thisresult.InnerText = "false" Then
            '                    newtvshow.locked = 0
            '                ElseIf thisresult.InnerText = "true" Then
            '                    newtvshow.locked = 1
            '                ElseIf thisresult.InnerText = "2" Then
            '                    newtvshow.locked = 2
            '                Else
            '                    If IsNumeric(thisresult.InnerText) Then
            '                        newtvshow.locked = Convert.ToInt32(thisresult.InnerText)
            '                    Else
            '                        newtvshow.locked = 1
            '                    End If
            '                End If
            '        End Select
            '    Catch ex As Exception
            '        MsgBox(ex.ToString)
            '    End Try
            'Next

            'If newtvshow.tvdbid = Nothing Then
            '    If tempid <> Nothing Then
            '        Dim tempstring As String = tempid.Substring(tempid.LastIndexOf("series/") + 7, tempid.Length - (tempid.LastIndexOf("series/") + 7))
            '        tempstring = tempstring.Substring(0, tempstring.IndexOf("/"))
            '        If tempstring.Length = 5 Then
            '            If IsNumeric(tempstring) Then
            '                newtvshow.tvdbid = tempstring
            '            End If
            '        End If
            '    End If
            'End If
            'If newtvshow.tvdbid = Nothing Then newtvshow.tvdbid = ""
            'If newtvshow.genre = Nothing Then newtvshow.genre = ""
            'If newtvshow.rating = Nothing Then newtvshow.rating = ""
        End If
        Return newtvshow



    End Function
    Public Sub SaveTvShowNfo(ByVal Path As String, ByRef Show As TvShow, Optional ByVal overwrite As Boolean = True, Optional ByVal forceunlocked As String = "")
        If IO.File.Exists(Path) And Not overwrite Then Exit Sub


        Show.Save(Path)
    End Sub

    'Public Sub savetvshownfo(ByVal filenameandpath As String, ByVal tvshowtosave As TvShow, Optional ByVal overwrite As Boolean = True, Optional ByVal forceunlocked As String = "")


    '    Try
    '        Dim newshow As Boolean = True
    '        For f = Form1.TvShows.Count - 1 To 0 Step -1
    '            If Form1.TvShows(f).fullpath.ToLower = filenameandpath.ToLower Then
    '                newshow = False
    '                Form1.TvShows(f).episodeactorsource = tvshowtosave.episodeactorsource
    '                Form1.TvShows(f).fullpath = tvshowtosave.path
    '                Form1.TvShows(f).genre = tvshowtosave.genre
    '                Form1.TvShows(f).imdbid = tvshowtosave.imdbid
    '                Form1.TvShows(f).language = tvshowtosave.language
    '                Form1.TvShows(f).rating = tvshowtosave.rating
    '                Form1.TvShows(f).sortorder = tvshowtosave.sortorder
    '                Form1.TvShows(f).title = tvshowtosave.title
    '                Form1.TvShows(f).tvdbid = tvshowtosave.tvdbid
    '                Form1.TvShows(f).year = tvshowtosave.year
    '                Form1.TvShows(f).locked = tvshowtosave.locked
    '                If forceunlocked = "unlock" Then
    '                    Form1.TvShows(f).locked = 0
    '                End If
    '                Exit For
    '            End If
    '        Next
    '        If newshow = True Then
    '            Dim newtvnfo As New TvShow
    '            newtvnfo.episodeactorsource = tvshowtosave.episodeactorsource
    '            newtvnfo.fullpath = tvshowtosave.path
    '            newtvnfo.genre = tvshowtosave.genre
    '            newtvnfo.imdbid = tvshowtosave.imdbid
    '            newtvnfo.language = tvshowtosave.language
    '            newtvnfo.rating = tvshowtosave.rating
    '            newtvnfo.sortorder = tvshowtosave.sortorder
    '            newtvnfo.title = tvshowtosave.title
    '            newtvnfo.tvdbid = tvshowtosave.tvdbid
    '            newtvnfo.year = tvshowtosave.year
    '            newtvnfo.locked = tvshowtosave.locked
    '            Form1.TvShows.Add(newtvnfo)
    '        End If
    '    Catch ex As Exception

    '    End Try

    '    Monitor.Enter(Me)
    '    Try
    '        Dim stage As Integer = 1

    '        If tvshowtosave Is Nothing Then Exit Sub
    '        If Not IO.File.Exists(filenameandpath) Or overwrite = True Then
    '            'Try
    '            Dim doc As New XmlDocument
    '            Dim root As XmlElement
    '            Dim child As XmlElement
    '            Dim actorchild As XmlElement
    '            root = doc.CreateElement("tvshow")
    '            Dim thumbnailstring As String = ""
    '            stage = 2
    '            Dim thispref As XmlNode = Nothing
    '            Dim xmlproc As XmlDeclaration

    '            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
    '            doc.AppendChild(xmlproc)

    '            child = doc.CreateElement("episodeguide")
    '            Dim childchild As XmlElement
    '            childchild = doc.CreateElement("url")
    '            Dim tempppp As String = tvshowtosave.tvdbid & ".xml"
    '            Dim Attr As XmlAttribute
    '            Attr = doc.CreateAttribute("cache")
    '            Attr.Value = tempppp
    '            childchild.Attributes.Append(Attr)
    '            If tvshowtosave.episodeguideurl <> Nothing Then
    '                If tvshowtosave.episodeguideurl <> "" Then
    '                    childchild.InnerText = tvshowtosave.episodeguideurl
    '                    child.AppendChild(childchild)
    '                Else
    '                    '"http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
    '                    If tvshowtosave.tvdbid <> Nothing Then
    '                        If IsNumeric(tvshowtosave.tvdbid) Then
    '                            If tvshowtosave.language <> Nothing Then
    '                                If tvshowtosave.language <> "" Then
    '                                    childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid & "/all/" & tvshowtosave.language & ".zip"
    '                                    child.AppendChild(childchild)
    '                                Else
    '                                    childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid & "/all/en.zip"
    '                                    child.AppendChild(childchild)
    '                                End If
    '                            Else
    '                                childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid & "/all/en.zip"
    '                                child.AppendChild(childchild)
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '                root.AppendChild(child)
    '            Else
    '                '"http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
    '                If tvshowtosave.tvdbid <> Nothing Then
    '                    If IsNumeric(tvshowtosave.tvdbid) Then
    '                        If tvshowtosave.language <> Nothing Then
    '                            If tvshowtosave.language <> "" Then
    '                                child.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid & "/all/" & tvshowtosave.language & ".zip"
    '                                root.AppendChild(child)
    '                            Else
    '                                child.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid & "/all/en.zip"
    '                                root.AppendChild(child)
    '                            End If
    '                        Else
    '                            child.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid & "/all/en.zip"
    '                            root.AppendChild(child)
    '                        End If
    '                    End If
    '                End If
    '            End If


    '            child = doc.CreateElement("title")
    '            child.InnerText = tvshowtosave.title
    '            root.AppendChild(child)

    '            stage = 19

    '            child = doc.CreateElement("year")
    '            child.InnerText = tvshowtosave.year
    '            root.AppendChild(child)

    '            stage = 20

    '            child = doc.CreateElement("rating")
    '            child.InnerText = tvshowtosave.rating
    '            root.AppendChild(child)

    '            stage = 21


    '            child = doc.CreateElement("plot")
    '            child.InnerText = tvshowtosave.plot
    '            root.AppendChild(child)

    '            stage = 22

    '            For Each thumbnail In tvshowtosave.posters

    '                child = doc.CreateElement("thumb")
    '                child.InnerText = thumbnail
    '                root.AppendChild(child)
    '            Next


    '            For Each thumbnail In tvshowtosave.fanart

    '                child = doc.CreateElement("fanart")
    '                child.InnerText = thumbnail
    '                root.AppendChild(child)
    '            Next

    '            stage = 23



    '            child = doc.CreateElement("runtime")
    '            If tvshowtosave.runtime <> Nothing Then
    '                Dim minutes As String = tvshowtosave.runtime
    '                minutes = minutes.Replace("minutes", "")
    '                minutes = minutes.Replace("mins", "")
    '                minutes = minutes.Replace("min", "")
    '                minutes = minutes.Replace(" ", "")

    '                Try
    '                    Do While minutes.IndexOf("0") = 0
    '                        minutes = minutes.Substring(1, minutes.Length - 1)
    '                    Loop
    '                    If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Preferences.roundminutes = True Then
    '                        minutes = "0" & minutes & " min"
    '                    ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Preferences.roundminutes = True Then
    '                        minutes = "00" & minutes & " min"
    '                    Else
    '                        minutes = tvshowtosave.runtime
    '                    End If
    '                Catch
    '                    minutes = "00"
    '                End Try
    '                child.InnerText = minutes
    '            Else
    '                child.InnerText = tvshowtosave.runtime
    '            End If
    '            root.AppendChild(child)

    '            stage = 25

    '            child = doc.CreateElement("mpaa")
    '            child.InnerText = tvshowtosave.mpaa
    '            root.AppendChild(child)

    '            stage = 26

    '            child = doc.CreateElement("genre")
    '            child.InnerText = tvshowtosave.genre
    '            root.AppendChild(child)

    '            child = doc.CreateElement("premiered")
    '            child.InnerText = tvshowtosave.premiered
    '            root.AppendChild(child)

    '            stage = 27

    '            child = doc.CreateElement("studio")
    '            child.InnerText = tvshowtosave.studio
    '            root.AppendChild(child)

    '            stage = 30

    '            child = doc.CreateElement("id")
    '            child.InnerText = tvshowtosave.imdbid
    '            root.AppendChild(child)

    '            child = doc.CreateElement("tvdbid")
    '            child.InnerText = tvshowtosave.tvdbid
    '            root.AppendChild(child)

    '            child = doc.CreateElement("sortorder")
    '            child.InnerText = tvshowtosave.sortorder
    '            root.AppendChild(child)

    '            child = doc.CreateElement("language")
    '            child.InnerText = tvshowtosave.language
    '            root.AppendChild(child)

    '            child = doc.CreateElement("episodeactorsource")
    '            child.InnerText = tvshowtosave.episodeactorsource
    '            root.AppendChild(child)
    '            child = doc.CreateElement("tvshowactorsource")
    '            child.InnerText = tvshowtosave.tvshowactorsource
    '            root.AppendChild(child)

    '            child = doc.CreateElement("locked")
    '            child.InnerText = tvshowtosave.locked.ToString
    '            root.AppendChild(child)

    '            Dim actorstosave As Integer = tvshowtosave.ListActors.Count
    '            If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
    '            For f = 0 To actorstosave - 1
    '                child = doc.CreateElement("actor")
    '                actorchild = doc.CreateElement("name")
    '                actorchild.InnerText = tvshowtosave.ListActors(f).actorname
    '                child.AppendChild(actorchild)
    '                actorchild = doc.CreateElement("role")
    '                actorchild.InnerText = tvshowtosave.ListActors(f).actorrole
    '                child.AppendChild(actorchild)
    '                If tvshowtosave.ListActors(f).actorthumb <> Nothing Then
    '                    If tvshowtosave.ListActors(f).actorthumb <> "" Then
    '                        actorchild = doc.CreateElement("thumb")
    '                        actorchild.InnerText = tvshowtosave.ListActors(f).actorthumb
    '                        child.AppendChild(actorchild)
    '                    End If
    '                End If
    '                root.AppendChild(child)
    '            Next
    '            doc.AppendChild(root)

    '            stage = 34

    '            Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
    '            output.Formatting = Formatting.Indented
    '            stage = 35
    '            doc.WriteTo(output)
    '            output.Close()

    '            'Catch ex As Exception
    '            '    MsgBox(ex.Message.ToString)
    '            'End Try
    '        Else
    '            MsgBox("File already exists")
    '        End If

    '    Catch ex As Exception
    '        'MsgBox("Error Encountered at stage " & stage.ToString & vbCrLf & vbCrLf & ex.ToString)
    '    Finally
    '        Monitor.Exit(Me)
    '    End Try
    'End Sub

    'Public Sub saveepisodenfo(ByVal listofepisodes As List(Of TvEpisode), ByVal path As String, Optional ByVal seasonno As String = "-2", Optional ByVal episodeno As String = "-2", Optional ByVal batch As Boolean = False)
    '    'Monitor.Enter(Me)
    '    'Try
    '    Dim WorkingTvShow As TvShow = Form1.tvCurrentlySelectedShow()
    '    If seasonno <> -2 And episodeno <> -2 Then
    '        If batch = False Then
    '            Dim timetoexit As Boolean = False
    '            For Each show In Form1.TvShows
    '                If show.fullpath = WorkingTvShow.path Then
    '                    For Each episode In show.allepisodes
    '                        If episode.episodeno = episodeno And episode.seasonno = seasonno Then

    '                            For Each epis In listofepisodes
    '                                If epis.seasonno = seasonno And epis.episodeno = episodeno Then
    '                                    Dim newep As New TvEpisode
    '                                    newep.episodepath = epis.episodepath
    '                                    newep.title = epis.title
    '                                    newep.seasonno = epis.seasonno
    '                                    newep.episodeno = epis.episodeno
    '                                    newep.playcount = epis.playcount
    '                                    newep.rating = epis.rating
    '                                    show.allepisodes.Remove(episode)
    '                                    show.allepisodes.Add(newep)
    '                                    timetoexit = True
    '                                    Exit For
    '                                End If

    '                            Next
    '                        End If
    '                        If timetoexit = True Then Exit For
    '                    Next
    '                End If
    '                If timetoexit = True Then Exit For
    '            Next
    '        End If
    '    End If
    '    If listofepisodes.Count = 1 Then
    '        Dim doc As New XmlDocument
    '        Dim root As XmlElement
    '        Dim child As XmlElement
    '        Dim actorchild As XmlElement
    '        Dim filedetailschild As XmlElement
    '        Dim filedetailschildchild As XmlElement
    '        root = doc.CreateElement("episodedetails")
    '        Dim thispref As XmlNode = Nothing
    '        Dim xmlproc As XmlDeclaration

    '        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
    '        doc.AppendChild(xmlproc)
    '        Dim anotherchild As XmlNode = Nothing
    '        If Preferences.enabletvhdtags = True Then
    '            Try
    '                child = doc.CreateElement("fileinfo")

    '                anotherchild = doc.CreateElement("streamdetails")

    '                filedetailschild = doc.CreateElement("video")
    '                If listofepisodes(0).filedetails.filedetails_video.width <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.width <> "" Then
    '                        filedetailschildchild = doc.CreateElement("width")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.width
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.height <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.height <> "" Then
    '                        filedetailschildchild = doc.CreateElement("height")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.height
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.aspect <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.aspect <> "" Then
    '                        filedetailschildchild = doc.CreateElement("aspect")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.aspect
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.codec <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.codec <> "" Then
    '                        filedetailschildchild = doc.CreateElement("codec")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.codec
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.formatinfo <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.formatinfo <> "" Then
    '                        filedetailschildchild = doc.CreateElement("format")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.formatinfo
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.duration <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.duration <> "" Then
    '                        filedetailschildchild = doc.CreateElement("duration")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.duration
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.bitrate <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.bitrate <> "" Then
    '                        filedetailschildchild = doc.CreateElement("bitrate")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.bitrate
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.bitratemode <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.bitratemode <> "" Then
    '                        filedetailschildchild = doc.CreateElement("bitratemode")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.bitratemode
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.bitratemax <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.bitratemax <> "" Then
    '                        filedetailschildchild = doc.CreateElement("bitratemax")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.bitratemax
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.container <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.container <> "" Then
    '                        filedetailschildchild = doc.CreateElement("container")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.container
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.codecid <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.codecid <> "" Then
    '                        filedetailschildchild = doc.CreateElement("codecid")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.codecid
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.codecinfo <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.codecinfo <> "" Then
    '                        filedetailschildchild = doc.CreateElement("codecidinfo")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.codecinfo
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).filedetails.filedetails_video.scantype <> Nothing Then
    '                    If listofepisodes(0).filedetails.filedetails_video.scantype <> "" Then
    '                        filedetailschildchild = doc.CreateElement("scantype")
    '                        filedetailschildchild.InnerText = listofepisodes(0).filedetails.filedetails_video.scantype
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                anotherchild.AppendChild(filedetailschild)

    '                If listofepisodes(0).filedetails.filedetails_audio.Count > 0 Then
    '                    For Each item In listofepisodes(0).filedetails.filedetails_audio

    '                        filedetailschild = doc.CreateElement("audio")
    '                        If item.language <> Nothing Then
    '                            If item.language <> "" Then
    '                                filedetailschildchild = doc.CreateElement("language")
    '                                filedetailschildchild.InnerText = item.language
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                            End If
    '                        End If
    '                        If item.codec <> Nothing Then
    '                            If item.codec <> "" Then
    '                                filedetailschildchild = doc.CreateElement("codec")
    '                                filedetailschildchild.InnerText = item.codec
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                            End If
    '                        End If
    '                        If item.channels <> Nothing Then
    '                            If item.channels <> "" Then
    '                                filedetailschildchild = doc.CreateElement("channels")
    '                                filedetailschildchild.InnerText = item.channels
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                            End If
    '                        End If
    '                        If item.bitrate <> Nothing Then
    '                            If item.bitrate <> "" Then
    '                                filedetailschildchild = doc.CreateElement("bitrate")
    '                                filedetailschildchild.InnerText = item.bitrate
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                                anotherchild.AppendChild(filedetailschild)
    '                            End If
    '                        End If
    '                    Next
    '                    If listofepisodes(0).filedetails.filedetails_subtitles.Count > 0 Then
    '                        filedetailschild = doc.CreateElement("subtitle")
    '                        For Each entry In listofepisodes(0).filedetails.filedetails_subtitles
    '                            If entry.language <> Nothing Then
    '                                If entry.language <> "" Then
    '                                    filedetailschildchild = doc.CreateElement("language")
    '                                    filedetailschildchild.InnerText = entry.language
    '                                    filedetailschild.AppendChild(filedetailschildchild)
    '                                End If
    '                            End If
    '                        Next
    '                        anotherchild.AppendChild(filedetailschild)
    '                    End If
    '                End If
    '                child.AppendChild(anotherchild)
    '                root.AppendChild(child)
    '            Catch
    '            End Try
    '        End If


    '        child = doc.CreateElement("title")
    '        child.InnerText = listofepisodes(0).title
    '        root.AppendChild(child)

    '        child = doc.CreateElement("season")
    '        child.InnerText = listofepisodes(0).seasonno
    '        root.AppendChild(child)

    '        child = doc.CreateElement("episode")
    '        child.InnerText = listofepisodes(0).episodeno
    '        root.AppendChild(child)

    '        child = doc.CreateElement("aired")
    '        child.InnerText = listofepisodes(0).aired
    '        root.AppendChild(child)

    '        child = doc.CreateElement("plot")
    '        child.InnerText = listofepisodes(0).plot
    '        root.AppendChild(child)

    '        child = doc.CreateElement("playcount")
    '        child.InnerText = listofepisodes(0).playcount
    '        root.AppendChild(child)

    '        child = doc.CreateElement("director")
    '        child.InnerText = listofepisodes(0).director
    '        root.AppendChild(child)

    '        child = doc.CreateElement("credits")
    '        child.InnerText = listofepisodes(0).credits
    '        root.AppendChild(child)

    '        child = doc.CreateElement("rating")
    '        child.InnerText = listofepisodes(0).rating
    '        root.AppendChild(child)

    '        child = doc.CreateElement("runtime")
    '        child.InnerText = listofepisodes(0).Runtime
    '        root.AppendChild(child)

    '        Dim actorstosave As Integer = listofepisodes(0).ListActors.Count
    '        If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
    '        For f = 0 To actorstosave - 1
    '            child = doc.CreateElement("actor")
    '            actorchild = doc.CreateElement("name")
    '            actorchild.InnerText = listofepisodes(0).ListActors(f).actorname
    '            child.AppendChild(actorchild)
    '            actorchild = doc.CreateElement("role")
    '            actorchild.InnerText = listofepisodes(0).ListActors(f).actorrole
    '            child.AppendChild(actorchild)
    '            If listofepisodes(0).ListActors(f).actorthumb <> Nothing Then
    '                If listofepisodes(0).ListActors(f).actorthumb <> "" Then
    '                    actorchild = doc.CreateElement("thumb")
    '                    actorchild.InnerText = listofepisodes(0).ListActors(f).actorthumb
    '                    child.AppendChild(actorchild)
    '                End If
    '            End If
    '            root.AppendChild(child)
    '        Next
    '        doc.AppendChild(root)
    '        Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
    '        output.Formatting = Formatting.Indented

    '        doc.WriteTo(output)
    '        output.Close()
    '    Else
    '        Dim document As New XmlDocument
    '        Dim root As XmlElement
    '        Dim child As XmlElement
    '        Dim childchild As XmlElement
    '        Dim childchildchild As XmlElement
    '        Dim childchildchildchild As XmlElement
    '        Dim middlechild As XmlElement

    '        Dim xmlproc As XmlDeclaration
    '        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
    '        document.AppendChild(xmlproc)

    '        root = document.CreateElement("multiepisodenfo")
    '        Dim done As Boolean = False
    '        For Each ep In listofepisodes
    '            child = document.CreateElement("episodedetails")
    '            If done = False Then
    '                'done = True
    '                If Preferences.enabletvhdtags = True Then
    '                    Try
    '                        middlechild = document.CreateElement("streamdetails")
    '                        childchild = document.CreateElement("fileinfo")
    '                        childchildchild = document.CreateElement("video")
    '                        If ep.filedetails.filedetails_video.width <> Nothing Then
    '                            If ep.filedetails.filedetails_video.width <> "" Then
    '                                childchildchildchild = document.CreateElement("width")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.width
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.height <> Nothing Then
    '                            If ep.filedetails.filedetails_video.height <> "" Then
    '                                childchildchildchild = document.CreateElement("height")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.height
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.codec <> Nothing Then
    '                            If ep.filedetails.filedetails_video.codec <> "" Then
    '                                childchildchildchild = document.CreateElement("codec")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.codec
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.formatinfo <> Nothing Then
    '                            If ep.filedetails.filedetails_video.formatinfo <> "" Then
    '                                childchildchildchild = document.CreateElement("format")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.formatinfo
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.duration <> Nothing Then
    '                            If ep.filedetails.filedetails_video.duration <> "" Then
    '                                childchildchildchild = document.CreateElement("duration")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.duration
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.bitrate <> Nothing Then
    '                            If ep.filedetails.filedetails_video.bitrate <> "" Then
    '                                childchildchildchild = document.CreateElement("width")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.bitrate
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.bitratemode <> Nothing Then
    '                            If ep.filedetails.filedetails_video.bitratemode <> "" Then
    '                                childchildchildchild = document.CreateElement("bitratemode")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.bitratemode
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.bitratemax <> Nothing Then
    '                            If ep.filedetails.filedetails_video.bitratemax <> "" Then
    '                                childchildchildchild = document.CreateElement("bitratemax")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.bitratemax
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.container <> Nothing Then
    '                            If ep.filedetails.filedetails_video.container <> "" Then
    '                                childchildchildchild = document.CreateElement("container")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.container
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.codecid <> Nothing Then
    '                            If ep.filedetails.filedetails_video.codecid <> "" Then
    '                                childchildchildchild = document.CreateElement("codecid")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.codecid
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.codecinfo <> Nothing Then
    '                            If ep.filedetails.filedetails_video.codecinfo <> "" Then
    '                                childchildchildchild = document.CreateElement("codecidinfo")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.codecinfo
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        If ep.filedetails.filedetails_video.scantype <> Nothing Then
    '                            If ep.filedetails.filedetails_video.scantype <> "" Then
    '                                childchildchildchild = document.CreateElement("scantype")
    '                                childchildchildchild.InnerText = ep.filedetails.filedetails_video.scantype
    '                                childchildchild.AppendChild(childchildchildchild)
    '                            End If
    '                        End If
    '                        childchild.AppendChild(childchildchild)
    '                        If ep.filedetails.filedetails_audio.Count > 0 Then
    '                            For Each aud In ep.filedetails.filedetails_audio
    '                                childchildchild = document.CreateElement("audio")
    '                                If aud.language <> Nothing Then
    '                                    If aud.language <> "" Then
    '                                        childchildchildchild = document.CreateElement("language")
    '                                        childchildchildchild.InnerText = aud.language
    '                                        childchildchild.AppendChild(childchildchildchild)
    '                                    End If
    '                                End If
    '                                If aud.codec <> Nothing Then
    '                                    If aud.codec <> "" Then
    '                                        childchildchildchild = document.CreateElement("codec")
    '                                        childchildchildchild.InnerText = aud.codec
    '                                        childchildchild.AppendChild(childchildchildchild)
    '                                    End If
    '                                End If
    '                                If aud.channels <> Nothing Then
    '                                    If aud.channels <> "" Then
    '                                        childchildchildchild = document.CreateElement("channels")
    '                                        childchildchildchild.InnerText = aud.channels
    '                                        childchildchild.AppendChild(childchildchildchild)
    '                                    End If
    '                                End If
    '                                If aud.bitrate <> Nothing Then
    '                                    If aud.bitrate <> "" Then
    '                                        childchildchildchild = document.CreateElement("bitrate")
    '                                        childchildchildchild.InnerText = aud.bitrate
    '                                        childchildchild.AppendChild(childchildchildchild)
    '                                    End If
    '                                End If
    '                                childchild.AppendChild(childchildchild)
    '                            Next
    '                        End If
    '                        If ep.filedetails.filedetails_subtitles.Count > 0 Then
    '                            For Each subt In ep.filedetails.filedetails_subtitles
    '                                If subt.language <> Nothing Then
    '                                    If subt.language <> "" Then
    '                                        childchildchild = document.CreateElement("subtitle")
    '                                        childchildchildchild = document.CreateElement("language")
    '                                        childchildchildchild.InnerText = subt.language
    '                                        childchildchild.AppendChild(childchildchildchild)
    '                                    End If
    '                                End If
    '                                childchild.AppendChild(childchildchild)
    '                            Next
    '                        End If
    '                        middlechild.AppendChild(childchild)
    '                        child.AppendChild(middlechild)
    '                    Catch
    '                    End Try
    '                End If
    '            End If


    '            childchild = document.CreateElement("title")
    '            childchild.InnerText = ep.title
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("season")
    '            childchild.InnerText = ep.seasonno
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("episode")
    '            childchild.InnerText = ep.episodeno
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("playcount")
    '            childchild.InnerText = ep.playcount
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("credits")
    '            childchild.InnerText = ep.credits
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("director")
    '            childchild.InnerText = ep.director
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("rating")
    '            childchild.InnerText = ep.rating
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("aired")
    '            childchild.InnerText = ep.aired
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("plot")
    '            childchild.InnerText = ep.plot
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("thumb")
    '            childchild.InnerText = ep.thumb
    '            child.AppendChild(childchild)

    '            childchild = document.CreateElement("runtime")
    '            childchild.InnerText = ep.Runtime
    '            child.AppendChild(childchild)

    '            For Each actor In ep.ListActors
    '                Try
    '                    childchild = document.CreateElement("actor")
    '                    childchildchild = document.CreateElement("name")
    '                    childchildchild.InnerText = actor.actorname
    '                    childchild.AppendChild(childchildchild)
    '                    childchildchild = document.CreateElement("role")
    '                    childchildchild.InnerText = actor.actorrole
    '                    childchild.AppendChild(childchildchild)
    '                    childchildchild = document.CreateElement("thumb")
    '                    childchildchild.InnerText = actor.actorthumb
    '                    childchild.AppendChild(childchildchild)
    '                    child.AppendChild(childchild)
    '                Catch
    '                End Try
    '            Next
    '            root.AppendChild(child)
    '            document.AppendChild(root)
    '        Next
    '        Try
    '            Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
    '            output.Formatting = Formatting.Indented

    '            document.WriteTo(output)
    '            output.Close()
    '        Catch
    '        End Try


    '    End If





    '    'Catch ex As Exception

    '    'Finally
    '    '    Monitor.Exit(Me)
    '    'End Try

    'End Sub

    Public Function loadbasicmovienfo(ByVal path As String, ByVal mode As String)

        Try
            Dim newmovie As New str_ComboList(SetDefaults)
            If Not IO.File.Exists(path) Then
                Return "Error"
                Exit Function
            Else
                If mode = "movielist" Then
                    Dim movie As New XmlDocument
                    Try
                        movie.Load(path)
                    Catch ex As Exception
                        If Not validate_nfo(path) Then
                            newmovie.title = "ERROR"
                            Return "ERROR"
                            Exit Function
                        End If

                        newmovie.createdate = "999999999999"
                        Dim filecreation2 As New FileInfo(path)
                        Dim myDate2 As Date = filecreation2.LastWriteTime
                        Try
                            newmovie.filedate = Format(myDate2, "yyyyMMddHHmmss").ToString
                        Catch ex2 As Exception
                        End Try
                        newmovie.filename = IO.Path.GetFileName(path)
                        newmovie.foldername = Utilities.GetLastFolder(path)
                        newmovie.fullpathandfilename = path
                        newmovie.genre = "problem / xml error"
                        newmovie.id = ""
                        newmovie.missingdata1 = 0
                        newmovie.movieset = ""
                        newmovie.originaltitle = newmovie.title
                        newmovie.outline = ""
                        newmovie.playcount = "0"
                        newmovie.plot = ""
                        newmovie.rating = ""
                        newmovie.runtime = "0"
                        newmovie.sortorder = ""
                        newmovie.title = IO.Path.GetFileName(path)
                        newmovie.titleandyear = newmovie.title & " (0000)"
                        newmovie.top250 = "0"
                        newmovie.year = "0000"

                        Return (newmovie)
                        Exit Function
                    End Try

                    Dim thisresult As XmlNode = Nothing

                    For Each thisresult In movie("movie")
                        Try
                            Select Case thisresult.Name
                                Case "title"
                                    Dim tempstring As String = ""
                                    tempstring = thisresult.InnerText
                                    '-------------- Aqui
                                    If Preferences.ignorearticle = True Then
                                        If tempstring.ToLower.IndexOf("the ") = 0 Then
                                            tempstring = tempstring.Substring(4, tempstring.Length - 4)
                                            tempstring = tempstring & ", The"
                                        End If
                                    End If
                                    newmovie.title = tempstring
                                Case "originaltitle"
                                    newmovie.originaltitle = thisresult.InnerText
                                Case "set"
                                    newmovie.movieset = thisresult.InnerText
                                Case "year"
                                    newmovie.year = thisresult.InnerText
                                Case "outline"
                                    newmovie.outline = thisresult.InnerText
                                Case "plot"
                                    newmovie.plot = thisresult.InnerText
                                Case "genre"
                                    If newmovie.genre = "" Then                     'genres in nfo's are individual elements - in MC cache they are one string seperated by " / "
                                        newmovie.genre = thisresult.InnerText
                                    Else
                                        newmovie.genre = newmovie.genre & " / " & thisresult.InnerText
                                    End If
                                Case "id"
                                    newmovie.id = thisresult.InnerText
                                Case "playcount"
                                    newmovie.playcount = thisresult.InnerText
                                Case "rating"
                                    newmovie.rating = thisresult.InnerText
                                    If newmovie.rating.IndexOf("/10") <> -1 Then newmovie.rating.Replace("/10", "")
                                    If newmovie.rating.IndexOf(" ") <> -1 Then newmovie.rating.Replace(" ", "")
                                Case "top250"
                                    newmovie.top250 = thisresult.InnerText
                                Case "sortorder"
                                    newmovie.sortorder = thisresult.InnerText
                                Case "sorttitle"
                                    newmovie.sortorder = thisresult.InnerText
                                Case "runtime"
                                    newmovie.runtime = thisresult.InnerText
                                    If IsNumeric(newmovie.runtime) Then
                                        newmovie.runtime = newmovie.runtime & " min"
                                    End If
                                Case "createdate"
                                    newmovie.createdate = thisresult.InnerText
                            End Select
                        Catch ex As Exception
                            MsgBox(ex.ToString)
                        End Try
                    Next

                    'Now we need to make sure no varibles are still set to NOTHING before returning....
                    Dim filecreation As New FileInfo(path)
                    Dim myDate As Date = filecreation.LastWriteTime


                    If newmovie.title = Nothing Then newmovie.title = "ERR - This Movie Has No TITLE!"
                    If newmovie.createdate = "" Or newmovie.createdate = Nothing Then newmovie.createdate = "18000101000000"
                    Try
                        newmovie.filedate = Format(myDate, "yyyyMMddHHmmss")
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                    newmovie.filename = IO.Path.GetFileName(path)
                    newmovie.foldername = Utilities.GetLastFolder(path)
                    newmovie.fullpathandfilename = path
                    If newmovie.genre = Nothing Then newmovie.genre = ""
                    If newmovie.id = Nothing Then newmovie.id = ""
                    If newmovie.missingdata1 = Nothing Then newmovie.missingdata1 = 0
                    If newmovie.movieset = "" Or newmovie.movieset = Nothing Then newmovie.movieset = "-None-"
                    'if there is no entry for originaltitle, then use the current title. this should only come into use
                    'for old movies since new ones will have the originaltitle created when scraped
                    If newmovie.originaltitle = "" Or newmovie.originaltitle = Nothing Then newmovie.originaltitle = newmovie.title
                    If newmovie.playcount = Nothing Then newmovie.playcount = "0"
                    If newmovie.plot = Nothing Then newmovie.plot = ""
                    If newmovie.rating = Nothing Then newmovie.rating = ""
                    If newmovie.runtime = Nothing Then newmovie.runtime = ""
                    If newmovie.sortorder = Nothing Or newmovie.sortorder = "" Then newmovie.sortorder = newmovie.title
                    If newmovie.title <> Nothing And newmovie.year <> Nothing Then
                        newmovie.titleandyear = newmovie.title & " (" & newmovie.year & ")"
                    Else
                        newmovie.titleandyear = newmovie.title & "(0000)"
                    End If
                    If newmovie.top250 = Nothing Then newmovie.top250 = "0"
                    If newmovie.year = Nothing Then newmovie.year = "0001"

                    'MsgBox(Format(myDate, "MMddyy"))
                    'MsgBox(myDate.ToString("MMddyy"))

                End If
                Return newmovie
            End If

        Catch
        End Try
        Return "Error"
    End Function


    Public Function loadfullmovienfo(ByVal path As String)
        Monitor.Enter(Me)
        Try
            Dim newmovie As New FullMovieDetails
            newmovie.fullmoviebody.genre = ""
            Dim thumbstring As String = String.Empty
            If Not IO.File.Exists(path) Then
            Else
                Dim movie As New XmlDocument
                Try
                    movie.Load(path)
                Catch ex As Exception
                    Dim errorstring As String
                    errorstring = ex.Message.ToString & vbCrLf & vbCrLf
                    errorstring += ex.StackTrace.ToString
                    newmovie.fullmoviebody.title = Utilities.CleanFileName(IO.Path.GetFileName(Form1.workingMovie.fullpathandfilename))
                    newmovie.fullmoviebody.year = "0000"
                    newmovie.fullmoviebody.top250 = "0"
                    newmovie.fullmoviebody.playcount = "0"
                    newmovie.fullmoviebody.credits = ""
                    newmovie.fullmoviebody.director = ""
                    newmovie.fullmoviebody.stars = ""
                    newmovie.fullmoviebody.filename = ""
                    newmovie.fullmoviebody.genre = ""
                    newmovie.fullmoviebody.imdbid = ""
                    newmovie.fullmoviebody.mpaa = ""
                    newmovie.fullmoviebody.outline = "This nfo file could not be loaded"
                    newmovie.fullmoviebody.playcount = "0"
                    newmovie.fullmoviebody.plot = errorstring
                    newmovie.fullmoviebody.premiered = ""
                    newmovie.fullmoviebody.rating = ""
                    newmovie.fullmoviebody.runtime = ""
                    newmovie.fullmoviebody.studio = ""
                    newmovie.fullmoviebody.tagline = "Rescrapeing the movie should fix the problem"
                    newmovie.fullmoviebody.trailer = ""
                    newmovie.fullmoviebody.votes = ""
                    newmovie.fullmoviebody.sortorder = ""
                    newmovie.fullmoviebody.country = ""
                    newmovie.fileinfo.createdate = "99999999"
                    Return newmovie
                    Exit Function
                End Try
                Dim thisresult As XmlNode = Nothing

                For Each thisresult In movie("movie")
                    Select Case thisresult.Name
                        Case "alternativetitle"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                        Case "set"
                            newmovie.fullmoviebody.movieset = thisresult.InnerText
                        Case "sortorder"
                            newmovie.fullmoviebody.sortorder = thisresult.InnerText
                        Case "sorttitle"
                            newmovie.fullmoviebody.sortorder = thisresult.InnerText
                        Case "votes"
                            newmovie.fullmoviebody.votes = thisresult.InnerText
                        Case "country"
                            newmovie.fullmoviebody.country = thisresult.InnerText
                        Case "outline"
                            newmovie.fullmoviebody.outline = thisresult.InnerText
                        Case "plot"
                            newmovie.fullmoviebody.plot = thisresult.InnerText
                        Case "tagline"
                            newmovie.fullmoviebody.tagline = thisresult.InnerText
                        Case "runtime"
                            newmovie.fullmoviebody.runtime = thisresult.InnerText
                            If IsNumeric(newmovie.fullmoviebody.runtime) Then
                                newmovie.fullmoviebody.runtime = newmovie.fullmoviebody.runtime & " min"
                            End If
                        Case "mpaa"
                            newmovie.fullmoviebody.mpaa = thisresult.InnerText
                        Case "credits"
                            newmovie.fullmoviebody.credits = thisresult.InnerText
                        Case "director"
                            newmovie.fullmoviebody.director = thisresult.InnerText
                        Case "stars"
                            newmovie.fullmoviebody.stars = thisresult.InnerText
                        Case "thumb"
                            If thisresult.InnerText.IndexOf("&lt;thumbs&gt;") <> -1 Then
                                thumbstring = thisresult.InnerText
                            Else
                                newmovie.listthumbs.Add(thisresult.InnerText)
                            End If
                        Case "premiered"
                            newmovie.fullmoviebody.premiered = thisresult.InnerText
                        Case "studio"
                            newmovie.fullmoviebody.studio = thisresult.InnerText
                        Case "trailer"
                            newmovie.fullmoviebody.trailer = thisresult.InnerText
                        Case "title"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                            newmovie.fullmoviebody.title = thisresult.InnerText
                        Case "originaltitle"
                            newmovie.fullmoviebody.originaltitle = thisresult.InnerText
                        Case "year"
                            newmovie.fullmoviebody.year = thisresult.InnerText
                        Case "genre"
                            If newmovie.fullmoviebody.genre = "" Then
                                newmovie.fullmoviebody.genre = thisresult.InnerText
                            Else
                                newmovie.fullmoviebody.genre = newmovie.fullmoviebody.genre & " / " & thisresult.InnerText
                            End If
                        Case "id"
                            newmovie.fullmoviebody.imdbid = thisresult.InnerText
                        Case "playcount"
                            newmovie.fullmoviebody.playcount = thisresult.InnerText
                        Case "rating"
                            newmovie.fullmoviebody.rating = thisresult.InnerText
                            If newmovie.fullmoviebody.rating.IndexOf("/10") <> -1 Then newmovie.fullmoviebody.rating.Replace("/10", "")
                            If newmovie.fullmoviebody.rating.IndexOf(" ") <> -1 Then newmovie.fullmoviebody.rating.Replace(" ", "")
                        Case "top250"
                            newmovie.fullmoviebody.top250 = thisresult.InnerText
                        Case "createdate"
                            newmovie.fileinfo.createdate = thisresult.InnerText
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
                                End Select
                            Next
                            newmovie.listactors.Add(newactor)
                        Case "fileinfo"
                            Dim what As XmlNode = Nothing
                            For Each res In thisresult.ChildNodes
                                Select Case res.name
                                    Case "streamdetails"
                                        Dim newfilenfo As New FullFileDetails
                                        Dim detail As XmlNode = Nothing
                                        For Each detail In res.ChildNodes
                                            Select Case detail.Name
                                                Case "video"
                                                    Dim videodetails As XmlNode = Nothing
                                                    For Each videodetails In detail.ChildNodes
                                                        Select Case videodetails.Name
                                                            Case "width"
                                                                newfilenfo.filedetails_video.width = videodetails.InnerText
                                                            Case "height"
                                                                newfilenfo.filedetails_video.height = videodetails.InnerText
                                                            Case "aspect"
                                                                newfilenfo.filedetails_video.aspect = videodetails.InnerText
                                                            Case "codec"
                                                                newfilenfo.filedetails_video.codec = videodetails.InnerText
                                                            Case "formatinfo"
                                                                newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
                                                            Case "duration"
                                                                newfilenfo.filedetails_video.duration = videodetails.InnerText
                                                            Case "bitrate"
                                                                newfilenfo.filedetails_video.bitrate = videodetails.InnerText
                                                            Case "bitratemode"
                                                                newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
                                                            Case "bitratemax"
                                                                newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
                                                            Case "container"
                                                                newfilenfo.filedetails_video.container = videodetails.InnerText
                                                            Case "codecid"
                                                                newfilenfo.filedetails_video.codecid = videodetails.InnerText
                                                            Case "codecidinfo"
                                                                newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
                                                            Case "scantype"
                                                                newfilenfo.filedetails_video.scantype = videodetails.InnerText
                                                        End Select
                                                    Next
                                                Case "audio"
                                                    Dim audiodetails As XmlNode = Nothing
                                                    Dim audio As New str_MediaNFOAudio(SetDefaults)
                                                    For Each audiodetails In detail.ChildNodes

                                                        Select Case audiodetails.Name
                                                            Case "language"
                                                                audio.language = audiodetails.InnerText
                                                            Case "codec"
                                                                audio.codec = audiodetails.InnerText
                                                            Case "channels"
                                                                audio.channels = audiodetails.InnerText
                                                            Case "bitrate"
                                                                audio.bitrate = audiodetails.InnerText
                                                        End Select
                                                    Next
                                                    newfilenfo.filedetails_audio.Add(audio)
                                                Case "subtitle"
                                                    Dim subsdetails As XmlNode = Nothing
                                                    For Each subsdetails In detail.ChildNodes
                                                        Select Case subsdetails.Name
                                                            Case "language"
                                                                Dim sublang As New str_MediaNFOSubtitles(SetDefaults)
                                                                sublang.language = subsdetails.InnerText
                                                                newfilenfo.filedetails_subtitles.Add(sublang)
                                                        End Select
                                                    Next
                                            End Select
                                        Next
                                        newmovie.filedetails = newfilenfo
                                End Select
                            Next
                    End Select
                Next
                If thumbstring <> Nothing Then
                    Do Until thumbstring.ToLower.IndexOf("http") = -1
                        Try
                            Dim tempstring As String
                            tempstring = thumbstring.ToLower.Substring(thumbstring.ToLower.LastIndexOf("http"), (thumbstring.ToLower.LastIndexOf(".jpg") + 4) - thumbstring.ToLower.LastIndexOf("http"))
                            thumbstring = thumbstring.ToLower.Replace(tempstring, "")
                            newmovie.listthumbs.Add(tempstring)
                        Catch
                            Exit Do
                        End Try
                    Loop
                End If
                newmovie.fileinfo.fullpathandfilename = path
                newmovie.fileinfo.filename = IO.Path.GetFileName(path)
                newmovie.fileinfo.foldername = Utilities.GetLastFolder(path)
                newmovie.fileinfo.posterpath = Preferences.GetPosterPath(path)
                newmovie.fileinfo.trailerpath = ""
                newmovie.fileinfo.fanartpath = Preferences.GetFanartPath(path)

                Return newmovie
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        Finally
            Monitor.Exit(Me)
        End Try
        Return "Error"
    End Function


    Public Sub savemovienfo(ByVal filenameandpath As String, ByVal movietosave As FullMovieDetails, Optional ByVal overwrite As Boolean = True)
        Monitor.Enter(Me)
        Dim stage As Integer = 1
        Try
            If movietosave Is Nothing Then Exit Sub
            If Not IO.File.Exists(filenameandpath) Or overwrite = True Then
                'Try
                Dim doc As New XmlDocument
                Dim thumbnailstring As String = ""
                stage = 2
                Dim thispref As XmlNode = Nothing
                Dim xmlproc As XmlDeclaration

                xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                doc.AppendChild(xmlproc)
                Dim root As XmlElement = Nothing
                Dim child As XmlElement = Nothing
                Dim actorchild As XmlElement = Nothing
                Dim filedetailschild As XmlElement = Nothing
                Dim filedetailschildchild As XmlElement = Nothing
                Dim anotherchild As XmlElement = Nothing

                root = doc.CreateElement("movie")
                stage = 3
                If Preferences.enablehdtags = True Then
                    Try
                        child = doc.CreateElement("fileinfo")
                    Catch
                    End Try
                    Try
                        anotherchild = doc.CreateElement("streamdetails")
                    Catch ex As Exception

                    End Try
                    Try
                        filedetailschild = doc.CreateElement("video")
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.width <> Nothing Then
                            If movietosave.filedetails.filedetails_video.width <> "" Then
                                filedetailschildchild = doc.CreateElement("width")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.width
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    Try
                        If movietosave.filedetails.filedetails_video.height <> Nothing Then
                            If movietosave.filedetails.filedetails_video.height <> "" Then
                                filedetailschildchild = doc.CreateElement("height")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.height
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.aspect <> Nothing Then
                            If movietosave.filedetails.filedetails_video.aspect <> "" Then
                                filedetailschildchild = doc.CreateElement("aspect")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.aspect
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 5
                    Try
                        If movietosave.filedetails.filedetails_video.codec <> Nothing Then
                            If movietosave.filedetails.filedetails_video.codec <> "" Then
                                filedetailschildchild = doc.CreateElement("codec")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codec
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 6
                    Try
                        If movietosave.filedetails.filedetails_video.formatinfo <> Nothing Then
                            If movietosave.filedetails.filedetails_video.formatinfo <> "" Then
                                filedetailschildchild = doc.CreateElement("format")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.formatinfo
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 7
                    Try
                        If movietosave.filedetails.filedetails_video.duration <> Nothing Then
                            If movietosave.filedetails.filedetails_video.duration <> "" Then
                                filedetailschildchild = doc.CreateElement("duration")
                                Dim temptemp As String = movietosave.filedetails.filedetails_video.duration
                                If Preferences.intruntime = True Then
                                    temptemp = Utilities.cleanruntime(movietosave.filedetails.filedetails_video.duration)
                                    If IsNumeric(temptemp) Then
                                        filedetailschildchild.InnerText = temptemp
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    Else
                                        filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.duration
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                Else
                                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.duration
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If

                            End If
                        End If
                    Catch
                    End Try
                    stage = 8
                    Try
                        If movietosave.filedetails.filedetails_video.bitrate <> Nothing Then
                            If movietosave.filedetails.filedetails_video.bitrate <> "" Then
                                filedetailschildchild = doc.CreateElement("bitrate")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitrate
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 9
                    Try
                        If movietosave.filedetails.filedetails_video.bitratemode <> Nothing Then
                            If movietosave.filedetails.filedetails_video.bitratemode <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemode")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitratemode
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 10
                    Try
                        If movietosave.filedetails.filedetails_video.bitratemax <> Nothing Then
                            If movietosave.filedetails.filedetails_video.bitratemax <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemax")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitratemax
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 11
                    Try
                        If movietosave.filedetails.filedetails_video.container <> Nothing Then
                            If movietosave.filedetails.filedetails_video.container <> "" Then
                                filedetailschildchild = doc.CreateElement("container")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.container
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 12
                    Try
                        If movietosave.filedetails.filedetails_video.codecid <> Nothing Then
                            If movietosave.filedetails.filedetails_video.codecid <> "" Then
                                filedetailschildchild = doc.CreateElement("codecid")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codecid
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 13
                    Try
                        If movietosave.filedetails.filedetails_video.codecinfo <> Nothing Then
                            If movietosave.filedetails.filedetails_video.codecinfo <> "" Then
                                filedetailschildchild = doc.CreateElement("codecidinfo")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codecinfo
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 14
                    Try
                        If movietosave.filedetails.filedetails_video.scantype <> Nothing Then
                            If movietosave.filedetails.filedetails_video.scantype <> "" Then
                                filedetailschildchild = doc.CreateElement("scantype")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.scantype
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 15
                    Try
                        anotherchild.AppendChild(filedetailschild)
                    Catch
                    End Try

                    stage = 16

                    For Each item In movietosave.filedetails.filedetails_audio
                        Try
                            filedetailschild = doc.CreateElement("audio")
                        Catch
                        End Try
                        Try
                            If item.language <> Nothing Then
                                If item.language <> "" Then
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = item.language
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.codec <> Nothing Then
                                If item.codec <> "" Then
                                    filedetailschildchild = doc.CreateElement("codec")
                                    filedetailschildchild.InnerText = item.codec
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.channels <> Nothing Then
                                If item.channels <> "" Then
                                    filedetailschildchild = doc.CreateElement("channels")
                                    filedetailschildchild.InnerText = item.channels
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        Try
                            If item.bitrate <> Nothing Then
                                If item.bitrate <> "" Then
                                    filedetailschildchild = doc.CreateElement("bitrate")
                                    filedetailschildchild.InnerText = item.bitrate
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                        anotherchild.AppendChild(filedetailschild)
                    Next
                    stage = 17
                    Try
                        filedetailschild = doc.CreateElement("subtitle")
                    Catch
                    End Try
                    Dim tempint As Integer = 0
                    For Each entry In movietosave.filedetails.filedetails_subtitles
                        Try
                            If entry.language <> Nothing Then
                                If entry.language <> "" Then
                                    tempint += 1
                                    filedetailschildchild = doc.CreateElement("language")
                                    filedetailschildchild.InnerText = entry.language
                                    filedetailschild.AppendChild(filedetailschildchild)
                                End If
                            End If
                        Catch
                        End Try
                    Next
                    stage = 18
                    Try
                        If tempint > 0 Then
                            anotherchild.AppendChild(filedetailschild)
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        child.AppendChild(anotherchild)
                    Catch
                    End Try
                    Try
                        root.AppendChild(child)
                    Catch
                    End Try
                End If



                Try
                    child = doc.CreateElement("title")
                    child.InnerText = movietosave.fullmoviebody.title
                    root.AppendChild(child)
                Catch
                End Try
                child = doc.CreateElement("originaltitle")
                If movietosave.fullmoviebody.originaltitle = Nothing Or movietosave.fullmoviebody.originaltitle = "" Then
                    child.InnerText = movietosave.fullmoviebody.title
                Else
                    child.InnerText = movietosave.fullmoviebody.originaltitle
                End If

                root.AppendChild(child)

                If movietosave.alternativetitles.Count > 0 Then
                    Try
                        For Each title In movietosave.alternativetitles
                            If title <> movietosave.fullmoviebody.title Then
                                Try
                                    child = doc.CreateElement("alternativetitle")
                                    child.InnerText = title
                                    root.AppendChild(child)
                                Catch ex As Exception

                                End Try
                            End If
                        Next
                    Catch
                    End Try
                End If

                Try
                    If movietosave.fullmoviebody.movieset <> Nothing Then
                        If movietosave.fullmoviebody.movieset <> "None" Then
                            child = doc.CreateElement("set")
                            child.InnerText = movietosave.fullmoviebody.movieset
                            root.AppendChild(child)
                        End If
                    End If
                Catch ex As Exception

                End Try
                Try
                    If movietosave.fullmoviebody.sortorder = Nothing Then
                        movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                    End If
                    If movietosave.fullmoviebody.sortorder = "" Then
                        movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                    End If
                    child = doc.CreateElement("sorttitle")
                    child.InnerText = movietosave.fullmoviebody.sortorder
                    root.AppendChild(child)
                Catch
                End Try
                stage = 19
                Try
                    child = doc.CreateElement("year")
                    child.InnerText = movietosave.fullmoviebody.year
                    root.AppendChild(child)
                Catch
                End Try
                stage = 20
                Try
                    child = doc.CreateElement("premiered")
                    child.InnerText = movietosave.fullmoviebody.premiered
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("rating")
                    child.InnerText = movietosave.fullmoviebody.rating
                    root.AppendChild(child)
                Catch
                End Try
                stage = 21
                Try
                    child = doc.CreateElement("votes")
                    child.InnerText = movietosave.fullmoviebody.votes
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("top250")
                    child.InnerText = movietosave.fullmoviebody.top250
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("outline")
                    child.InnerText = movietosave.fullmoviebody.outline
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("plot")
                    child.InnerText = movietosave.fullmoviebody.plot
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("tagline")
                    child.InnerText = movietosave.fullmoviebody.tagline
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    child = doc.CreateElement("country")
                    child.InnerText = movietosave.fullmoviebody.country
                    root.AppendChild(child)
                Catch
                End Try
                stage = 22
                Try
                    For Each thumbnail In movietosave.listthumbs
                        Try
                            child = doc.CreateElement("thumb")
                            child.InnerText = thumbnail
                            root.AppendChild(child)
                        Catch
                        End Try
                    Next
                Catch
                End Try
                stage = 23
                Try
                    If thumbnailstring <> "" Then
                        child = doc.CreateElement("thumb")
                        child.InnerText = thumbnailstring
                        root.AppendChild(child)
                    End If
                Catch
                End Try
                stage = 24
                Try
                    child = doc.CreateElement("runtime")
                    If movietosave.fullmoviebody.runtime <> Nothing Then
                        Dim minutes As String = movietosave.fullmoviebody.runtime
                        minutes = minutes.Replace("minutes", "")
                        minutes = minutes.Replace("mins", "")
                        minutes = minutes.Replace("min", "")
                        minutes = minutes.Replace(" ", "")
                        'If Preferences.intruntime = True And Not IsNumeric(minutes) Then
                        '    Dim tempstring As String = Form1.filefunction.cleanruntime(minutes)
                        '    If IsNumeric(tempstring) Then
                        '        minutes = tempstring
                        '    End If
                        'End If
                        Try
                            Do While minutes.IndexOf("0") = 0 And minutes.Length > 0
                                minutes = minutes.Substring(1, minutes.Length - 1)
                            Loop
                            If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Preferences.roundminutes = True Then
                                minutes = "0" & minutes
                            ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Preferences.roundminutes = True Then
                                minutes = "00" & minutes
                            End If
                            If Preferences.intruntime = False And IsNumeric(minutes) Then
                                minutes = minutes & " min"
                            End If
                        Catch ex As Exception
                            minutes = movietosave.fullmoviebody.runtime
                        End Try
                        child.InnerText = minutes
                    Else
                        child.InnerText = movietosave.fullmoviebody.runtime
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 25
                Try
                    child = doc.CreateElement("mpaa")
                    child.InnerText = movietosave.fullmoviebody.mpaa
                    root.AppendChild(child)
                Catch
                End Try
                stage = 26
                Try
                    If movietosave.fullmoviebody.genre <> "" Then
                        Dim strArr() As String
                        strArr = movietosave.fullmoviebody.genre.Split("/")
                        For count = 0 To strArr.Length - 1
                            child = doc.CreateElement("genre")
                            strArr(count) = strArr(count).Trim
                            child.InnerText = strArr(count)
                            root.AppendChild(child)
                        Next
                    End If
                Catch
                End Try
                stage = 27
                Try
                    child = doc.CreateElement("credits")
                    child.InnerText = movietosave.fullmoviebody.credits
                    root.AppendChild(child)
                Catch
                End Try
                stage = 28
                Try
                    child = doc.CreateElement("director")
                    child.InnerText = movietosave.fullmoviebody.director
                    root.AppendChild(child)
                Catch
                End Try
                stage = 29
                Try
                    child = doc.CreateElement("studio")
                    child.InnerText = movietosave.fullmoviebody.studio
                    root.AppendChild(child)
                Catch
                End Try
                stage = 30
                Try
                    child = doc.CreateElement("trailer")
                    child.InnerText = movietosave.fullmoviebody.trailer
                    root.AppendChild(child)
                Catch
                End Try
                stage = 31
                Try
                    child = doc.CreateElement("playcount")
                    child.InnerText = movietosave.fullmoviebody.playcount
                    root.AppendChild(child)
                Catch
                End Try
                stage = 32
                Try
                    If movietosave.fullmoviebody.imdbid <> Nothing Then
                        If movietosave.fullmoviebody.imdbid <> "" Then
                            child = doc.CreateElement("id")
                            child.InnerText = movietosave.fullmoviebody.imdbid
                            root.AppendChild(child)
                        Else

                        End If
                    Else

                    End If
                Catch
                End Try
                Try
                    child = doc.CreateElement("createdate")
                    If movietosave.fileinfo.createdate = Nothing Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, "yyyyMMddHHmmss").ToString
                        Catch ex2 As Exception
                        End Try
                    ElseIf movietosave.fileinfo.createdate = "" Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, "yyyyMMddHHmmss").ToString
                        Catch ex2 As Exception
                        End Try
                    Else
                        child.InnerText = movietosave.fileinfo.createdate
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 33
                Try
                    child = doc.CreateElement("stars")
                    child.InnerText = movietosave.fullmoviebody.stars
                    root.AppendChild(child)
                Catch
                End Try
                Try
                    Dim actorstosave As Integer = movietosave.listactors.Count
                    If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
                    For f = 0 To actorstosave - 1
                        child = doc.CreateElement("actor")
                        actorchild = doc.CreateElement("name")
                        actorchild.InnerText = movietosave.listactors(f).actorname
                        child.AppendChild(actorchild)
                        actorchild = doc.CreateElement("role")
                        actorchild.InnerText = movietosave.listactors(f).actorrole
                        child.AppendChild(actorchild)
                        If movietosave.listactors(f).actorthumb <> Nothing Then
                            If movietosave.listactors(f).actorthumb <> "" Then
                                actorchild = doc.CreateElement("thumb")
                                actorchild.InnerText = movietosave.listactors(f).actorthumb
                                child.AppendChild(actorchild)
                            End If
                        End If
                        root.AppendChild(child)
                    Next
                    doc.AppendChild(root)
                Catch
                End Try
                stage = 34
                Try
                    Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
                    output.Formatting = Formatting.Indented
                    stage = 35
                    doc.WriteTo(output)
                    output.Close()
                Catch
                End Try
                'Catch ex As Exception
                '    MsgBox(ex.Message.ToString)
                'End Try
            Else
                MsgBox("File already exists")
            End If

        Catch ex As Exception
            MsgBox("Error Encountered at stage " & stage.ToString & vbCrLf & vbCrLf & ex.ToString)
        Finally
            Monitor.Exit(Me)
        End Try
    End Sub
    Public Function decxmlchars(ByVal line As String)
        Monitor.Enter(Me)
        Try
            line = line.Replace("&amp;", "&")
            line = line.Replace("&lt;", "<")
            line = line.Replace("&gt;", ">")
            line = line.Replace("&quot;", "Chr(34)")
            line = line.Replace("&apos;", "'")
            line = line.Replace("&#xA;", vbCrLf)
            line = line.Replace("â€˜", "'")
            Return line
        Catch
        Finally
            Monitor.Exit(Me)
        End Try
        Return "Error"
    End Function


    Public Function loadfullepisodenfogeneric(ByVal path As String) ', ByVal season As String, ByVal episode As String)

        Dim newepisodelist As New List(Of TvEpisode)
        Dim newepisode As New TvEpisode
        If Not IO.File.Exists(path) Then
            newepisode.title = IO.Path.GetFileName(path)
            newepisode.plot = "missing file"
            newepisode.episodepath = path
            newepisode.episodepath = path
            If newepisode.episodeno = Nothing Or newepisode.episodeno = Nothing Then
                For Each regexp In Form1.tv_RegexScraper

                    Dim M As Match
                    M = Regex.Match(newepisode.episodepath, regexp)
                    If M.Success = True Then
                        Try
                            newepisode.seasonno = M.Groups(1).Value.ToString
                            newepisode.episodeno = M.Groups(2).Value.ToString
                            Exit For
                        Catch
                            newepisode.seasonno = "-1"
                            newepisode.seasonno = "-1"
                        End Try
                    End If
                Next
            End If
            If newepisode.episodeno = Nothing Then
                newepisode.episodeno = "-1"
            End If
            If newepisode.seasonno = Nothing Then
                newepisode.seasonno = "-1"
            End If
            newepisodelist.Add(newepisode)
            Return newepisodelist
            Exit Function
        Else
            Dim tvshow As New XmlDocument
            Try
                tvshow.Load(path)
            Catch ex As Exception
                'If Not validate_nfo(path) Then
                '    Exit Function
                'End If
                newepisode.title = IO.Path.GetFileName(path)
                newepisode.plot = "problem / xml error"
                newepisode.episodepath = path
                newepisode.episodepath = path
                If newepisode.episodeno = Nothing Or newepisode.episodeno = Nothing Then
                    For Each regexp In Form1.tv_RegexScraper

                        Dim M As Match
                        M = Regex.Match(newepisode.episodepath, regexp)
                        If M.Success = True Then
                            Try
                                newepisode.seasonno = M.Groups(1).Value.ToString
                                newepisode.episodeno = M.Groups(2).Value.ToString
                                Exit For
                            Catch
                                newepisode.seasonno = "-1"
                                newepisode.seasonno = "-1"
                            End Try
                        End If
                    Next
                End If
                If newepisode.episodeno = Nothing Then
                    newepisode.episodeno = "-1"
                End If
                If newepisode.seasonno = Nothing Then
                    newepisode.seasonno = "-1"
                End If
                newepisodelist.Add(newepisode)
                Return newepisodelist
                Exit Function
            End Try

            Dim thisresult As XmlNode = Nothing
            Dim tempid As String = ""
            If tvshow.DocumentElement.Name = "episodedetails" Then
                Dim newtvepisode As New TvEpisode
                For Each thisresult In tvshow("episodedetails")
                    Try
                        newtvepisode.episodepath = path
                        Select Case thisresult.Name
                            Case "credits"
                                newtvepisode.credits = thisresult.InnerText
                            Case "director"
                                newtvepisode.director = thisresult.InnerText
                            Case "aired"
                                newtvepisode.aired = thisresult.InnerText
                            Case "plot"
                                newtvepisode.plot = thisresult.InnerText
                            Case "title"
                                newtvepisode.title = thisresult.InnerText
                            Case "season"
                                newtvepisode.seasonno = thisresult.InnerText
                            Case "episode"
                                newtvepisode.episodeno = thisresult.InnerText
                            Case "rating"
                                newtvepisode.rating = thisresult.InnerText
                                If newtvepisode.rating.IndexOf("/10") <> -1 Then newtvepisode.rating.Replace("/10", "")
                                If newtvepisode.rating.IndexOf(" ") <> -1 Then newtvepisode.rating.Replace(" ", "")
                            Case "playcount"
                                newtvepisode.playcount = thisresult.InnerText
                            Case "thumb"
                                newtvepisode.thumb = thisresult.InnerText
                            Case "actor"
                                Dim actordetail As XmlNode = Nothing
                                Dim newactor As New str_MovieActors(SetDefaults)
                                For Each actordetail In thisresult.ChildNodes
                                    Select Case actordetail.Name
                                        Case "name"
                                            newactor.actorname = actordetail.InnerText
                                        Case "role"
                                            newactor.actorrole = actordetail.InnerText
                                        Case "thumb"
                                            newactor.actorthumb = actordetail.InnerText
                                    End Select
                                Next
                                newtvepisode.ListActors.Add(newactor)
                            Case "fileinfo"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 In thisresult.ChildNodes
                                    Select Case detail2.Name
                                        Case "streamdetails"
                                            Dim newfilenfo As New FullFileDetails
                                            Dim detail As XmlNode = Nothing
                                            For Each detail In detail2.ChildNodes
                                                Select Case detail.Name
                                                    Case "video"
                                                        Dim videodetails As XmlNode = Nothing
                                                        For Each videodetails In detail.ChildNodes
                                                            Select Case videodetails.Name
                                                                Case "width"
                                                                    newfilenfo.filedetails_video.width = videodetails.InnerText
                                                                Case "height"
                                                                    newfilenfo.filedetails_video.height = videodetails.InnerText
                                                                Case "aspect"
                                                                    newfilenfo.filedetails_video.aspect = videodetails.InnerText
                                                                Case "codec"
                                                                    newfilenfo.filedetails_video.codec = videodetails.InnerText
                                                                Case "formatinfo"
                                                                    newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
                                                                Case "duration"
                                                                    newfilenfo.filedetails_video.duration = videodetails.InnerText
                                                                Case "bitrate"
                                                                    newfilenfo.filedetails_video.bitrate = videodetails.InnerText
                                                                Case "bitratemode"
                                                                    newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
                                                                Case "bitratemax"
                                                                    newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
                                                                Case "container"
                                                                    newfilenfo.filedetails_video.container = videodetails.InnerText
                                                                Case "codecid"
                                                                    newfilenfo.filedetails_video.codecid = videodetails.InnerText
                                                                Case "codecidinfo"
                                                                    newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
                                                                Case "scantype"
                                                                    newfilenfo.filedetails_video.scantype = videodetails.InnerText
                                                            End Select
                                                        Next
                                                    Case "audio"
                                                        Dim audiodetails As XmlNode = Nothing
                                                        Dim audio As New str_MediaNFOAudio(SetDefaults)
                                                        For Each audiodetails In detail.ChildNodes
                                                            Select Case audiodetails.Name
                                                                Case "language"
                                                                    audio.language = audiodetails.InnerText
                                                                Case "codec"
                                                                    audio.codec = audiodetails.InnerText
                                                                Case "channels"
                                                                    audio.channels = audiodetails.InnerText
                                                                Case "bitrate"
                                                                    audio.bitrate = audiodetails.InnerText
                                                            End Select
                                                        Next
                                                        newfilenfo.filedetails_audio.Add(audio)
                                                    Case "subtitle"
                                                        Dim subsdetails As XmlNode = Nothing
                                                        For Each subsdetails In detail.ChildNodes
                                                            Select Case subsdetails.Name
                                                                Case "language"
                                                                    Dim sublang As New str_MediaNFOSubtitles(SetDefaults)
                                                                    sublang.language = subsdetails.InnerText
                                                                    newfilenfo.filedetails_subtitles.Add(sublang)
                                                            End Select
                                                        Next
                                                End Select
                                            Next
                                            newtvepisode.filedetails = newfilenfo
                                    End Select
                                Next
                        End Select
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Next

                If newtvepisode.episodeno = Nothing Or newtvepisode.episodeno = Nothing Then
                    For Each regexp In Form1.tv_RegexScraper

                        Dim M As Match
                        M = Regex.Match(newtvepisode.episodepath, regexp)
                        If M.Success = True Then
                            Try
                                newtvepisode.seasonno = M.Groups(1).Value.ToString
                                newtvepisode.episodeno = M.Groups(2).Value.ToString
                                Exit For
                            Catch
                                newtvepisode.seasonno = "-1"
                                newtvepisode.seasonno = "-1"
                            End Try
                        End If
                    Next
                End If
                If newtvepisode.episodeno = Nothing Then
                    newtvepisode.episodeno = "-1"
                End If
                If newtvepisode.seasonno = Nothing Then
                    newtvepisode.seasonno = "-1"
                End If
                If newtvepisode.rating = Nothing Then newtvepisode.rating = ""
                newepisodelist.Add(newtvepisode)
                Return newepisodelist
                Exit Function
            ElseIf tvshow.DocumentElement.Name = "multiepisodenfo" Then
                For Each thisresult In tvshow("multiepisodenfo")
                    Select Case thisresult.Name
                        Case "episodedetails"
                            Dim newepisodenfo As XmlNode = Nothing
                            Dim anotherepisode As New TvEpisode

                            anotherepisode.episodepath = Nothing
                            anotherepisode.playcount = Nothing
                            anotherepisode.rating = Nothing
                            anotherepisode.seasonno = Nothing
                            anotherepisode.title = Nothing
                            ' For Each newepisodenfo In thisresult.ChildNodes
                            Dim tempint As Integer = thisresult.ChildNodes.Count - 1
                            For f = 0 To tempint
                                Try


                                    'Public credits As String
                                    'Public director As String
                                    'Public aired As String
                                    'Public plot As Integer
                                    'Public fanartpath As String
                                    'Public listactors As New List(Of movieactors)
                                    'Public filedetails As New fullfiledetails


                                    Select Case thisresult.ChildNodes(f).Name
                                        Case "credits"
                                            anotherepisode.credits = thisresult.ChildNodes(f).InnerText
                                        Case "director"
                                            anotherepisode.director = thisresult.ChildNodes(f).InnerText
                                        Case "aired"
                                            anotherepisode.aired = thisresult.ChildNodes(f).InnerText
                                        Case "plot"
                                            anotherepisode.plot = thisresult.ChildNodes(f).InnerText
                                        Case "title"
                                            anotherepisode.title = thisresult.ChildNodes(f).InnerText
                                        Case "season"
                                            anotherepisode.seasonno = thisresult.ChildNodes(f).InnerText
                                        Case "episode"
                                            anotherepisode.episodeno = thisresult.ChildNodes(f).InnerText
                                        Case "rating"
                                            anotherepisode.rating = thisresult.ChildNodes(f).InnerText
                                            If anotherepisode.rating.IndexOf("/10") <> -1 Then anotherepisode.rating.Replace("/10", "")
                                            If anotherepisode.rating.IndexOf(" ") <> -1 Then anotherepisode.rating.Replace(" ", "")
                                        Case "playcount"
                                            anotherepisode.playcount = thisresult.ChildNodes(f).InnerText
                                        Case "thumb"
                                            anotherepisode.thumb = thisresult.ChildNodes(f).InnerText
                                        Case "runtime"
                                            anotherepisode.Runtime.Value = thisresult.ChildNodes(f).InnerText
                                        Case "actor"
                                            Dim detail As XmlNode = Nothing
                                            Dim newactor As New str_MovieActors(SetDefaults)
                                            For Each detail In thisresult.ChildNodes(f).ChildNodes
                                                Select Case detail.Name
                                                    Case "name"
                                                        newactor.actorname = detail.InnerText
                                                    Case "role"
                                                        newactor.actorrole = detail.InnerText
                                                    Case "thumb"
                                                        newactor.actorthumb = detail.InnerText
                                                End Select
                                            Next
                                            anotherepisode.ListActors.Add(newactor)
                                        Case "streamdetails"
                                            Dim detail2 As XmlNode = Nothing
                                            For Each detail2 In thisresult.ChildNodes(f).ChildNodes
                                                Select Case detail2.Name
                                                    Case "fileinfo"
                                                        Dim newfilenfo As New FullFileDetails
                                                        Dim detail As XmlNode = Nothing
                                                        For Each detail In detail2.ChildNodes
                                                            Select Case detail.Name
                                                                Case "video"
                                                                    Dim videodetails As XmlNode = Nothing
                                                                    For Each videodetails In detail.ChildNodes
                                                                        Select Case videodetails.Name
                                                                            Case "width"
                                                                                newfilenfo.filedetails_video.width = videodetails.InnerText
                                                                            Case "height"
                                                                                newfilenfo.filedetails_video.height = videodetails.InnerText
                                                                            Case "codec"
                                                                                newfilenfo.filedetails_video.codec = videodetails.InnerText
                                                                            Case "formatinfo"
                                                                                newfilenfo.filedetails_video.formatinfo = videodetails.InnerText
                                                                            Case "duration"
                                                                                newfilenfo.filedetails_video.duration = videodetails.InnerText
                                                                            Case "bitrate"
                                                                                newfilenfo.filedetails_video.bitrate = videodetails.InnerText
                                                                            Case "bitratemode"
                                                                                newfilenfo.filedetails_video.bitratemode = videodetails.InnerText
                                                                            Case "bitratemax"
                                                                                newfilenfo.filedetails_video.bitratemax = videodetails.InnerText
                                                                            Case "container"
                                                                                newfilenfo.filedetails_video.container = videodetails.InnerText
                                                                            Case "codecid"
                                                                                newfilenfo.filedetails_video.codecid = videodetails.InnerText
                                                                            Case "codecidinfo"
                                                                                newfilenfo.filedetails_video.codecinfo = videodetails.InnerText
                                                                            Case "scantype"
                                                                                newfilenfo.filedetails_video.scantype = videodetails.InnerText
                                                                        End Select
                                                                    Next
                                                                Case "audio"
                                                                    Dim audiodetails As XmlNode = Nothing
                                                                    Dim audio As New str_MediaNFOAudio(SetDefaults)
                                                                    For Each audiodetails In detail.ChildNodes

                                                                        Select Case audiodetails.Name
                                                                            Case "language"
                                                                                audio.language = audiodetails.InnerText
                                                                            Case "codec"
                                                                                audio.codec = audiodetails.InnerText
                                                                            Case "channels"
                                                                                audio.channels = audiodetails.InnerText
                                                                            Case "bitrate"
                                                                                audio.bitrate = audiodetails.InnerText
                                                                        End Select
                                                                    Next
                                                                    newfilenfo.filedetails_audio.Add(audio)
                                                                Case "subtitle"
                                                                    Dim subsdetails As XmlNode = Nothing
                                                                    For Each subsdetails In detail.ChildNodes
                                                                        Select Case subsdetails.Name
                                                                            Case "language"
                                                                                Dim sublang As New str_MediaNFOSubtitles(SetDefaults)
                                                                                sublang.language = subsdetails.InnerText
                                                                                newfilenfo.filedetails_subtitles.Add(sublang)
                                                                        End Select
                                                                    Next
                                                            End Select
                                                        Next
                                                        anotherepisode.filedetails = newfilenfo
                                                End Select
                                            Next
                                    End Select


                                Catch ex As Exception
                                    MsgBox(ex.ToString)
                                End Try
                            Next f
                            anotherepisode.episodepath = path
                            newepisodelist.Add(anotherepisode)
                    End Select
                Next
                Return newepisodelist
            End If


        End If
        Return "Error"
    End Function


End Class



