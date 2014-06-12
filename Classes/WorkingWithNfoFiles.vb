Imports System.Xml
Imports System.IO
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports Media_Companion
Imports System.Text


Public Class WorkingWithNfoFiles
    Const SetDefaults = True
    'Public Shared MyCulture As New System.Globalization.CultureInfo("en-US")

    Public Shared Function util_NfoValidate(ByVal nfopath As String, Optional ByVal homemovie As Boolean = False)
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

    Public Shared Sub ConvertFileToUTF8IfNotAlready(FileName As String)
        If Not IO.File.Exists(FileName) Then Exit Sub
        Dim _Detected As Encoding

        Dim r = New StreamReader(FileName, Encoding.Default)
        Dim s = r.ReadToEnd

        _Detected = r.CurrentEncoding

        r.Close

        If _Detected.ToString <> "System.Text.UTF8Encoding" Then
            Try
                File.WriteAllText(FileName, s, Encoding.UTF8)
            Catch ex As Exception
                MsgBox("Error [" & ex.Message & "] occurred trying to convert file [" & FileName & "] from format [" & _Detected.ToString & "] to UTF-8. Recommended Action: Either delete the NFO and have MC re-create it or open it in NotePad and 'Save As... UFT-8" )
            End Try
        End If
    End Sub

    Public Function util_CharsConvert(ByVal line As String)
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

    '  All Tv Load/Save Routines
#Region " Tv Routines "
    Public Function tv_NfoLoad(ByVal path As String) As TvShow

        Dim newtvshow As New TvShow
        If Not IO.File.Exists(path) Then
            'Form1.tvrebuildlog(path & ", does not appear to exist")
            newtvshow.Title.Value = Utilities.GetLastFolder(path)
            newtvshow.Year.Value = newtvshow.Title.Value & " (0000)"
            newtvshow.NfoFilePath = path
            newtvshow.Year.Value = "0000"
            newtvshow.TvdbId.Value = ""
            newtvshow.Status.Value = "missing"
            newtvshow.State = Media_Companion.ShowState.Locked
            Return newtvshow
            Exit Function
        Else
            newtvshow.NfoFilePath = path
            newtvshow.Load()
        End If
        Return newtvshow


    End Function

    Public Shared Function ep_NfoLoad(ByVal path As String)
        'Try
        Dim episodelist As New List(Of TvEpisode)
        Dim fixmulti As Boolean = False
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
                newtvshow.Title.Value = IO.Path.GetFileName(path)
                'newtvshow.title = newtvshow.title.Replace(IO.Path.GetExtension(newtvshow.title), "")
                newtvshow.ImdbId.Value = "xml error"
                newtvshow.NfoFilePath = path
                newtvshow.TvdbId.Value = ""

                If newtvshow.Episode.Value = Nothing Or newtvshow.Episode.Value = Nothing Then
                    For Each regexp In Preferences.tv_RegexScraper

                        Dim M As Match
                        M = Regex.Match(newtvshow.NfoFilePath, regexp)
                        If M.Success = True Then
                            Try
                                newtvshow.Season.Value = M.Groups(1).Value.ToString
                                newtvshow.Episode.Value = M.Groups(2).Value.ToString
                                Exit For
                            Catch
                                newtvshow.Season.Value = "-1"
                                newtvshow.Episode.Value = "-1"
                            End Try
                        End If
                    Next
                End If
                If newtvshow.Episode.Value = Nothing Then
                    newtvshow.Episode.Value = "-1"
                End If
                If newtvshow.Season.Value = Nothing Then
                    newtvshow.Season.Value = "-1"
                End If
                If newtvshow.Season.Value.IndexOf("0") = 0 Then
                    newtvshow.Season.Value = newtvshow.Season.Value.Substring(1, 1)
                End If
                If newtvshow.Episode.Value.IndexOf("0") = 0 Then
                    newtvshow.Episode.Value = newtvshow.Episode.Value.Substring(1, 1)
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
                        newtvepisode.NfoFilePath = path
                        Select Case thisresult.Name
                            Case "title"
                                newtvepisode.Title.Value = thisresult.InnerText
                            Case "season"
                                newtvepisode.Season.Value = thisresult.InnerText
                            Case "episode"
                                newtvepisode.Episode.Value = thisresult.InnerText
                            Case "tvdbid"
                                newtvepisode.TvdbId.Value = thisresult.InnerText
                            Case "rating"
                                newtvepisode.Rating.Value = thisresult.InnerText
                                If newtvepisode.Rating.IndexOf("/10") <> -1 Then newtvepisode.Rating.Value.Replace("/10", "")
                                If newtvepisode.Rating.IndexOf(" ") <> -1 Then newtvepisode.Rating.Value.Replace(" ", "")
                            Case "playcount"
                                newtvepisode.PlayCount.Value = thisresult.InnerText
                            Case "aired"
                                newtvepisode.Aired.Value = thisresult.InnerText
                            Case "plot"
                                newtvepisode.Plot.Value = thisresult.InnerText
                            Case "director"
                                newtvepisode.Director.Value = thisresult.InnerText
                            Case "credits"
                                newtvepisode.Credits.Value = thisresult.InnerText 
                            Case "videosource"
                                newtvepisode.Source.Value = thisresult.InnerText 
                            Case "showid"
                                newtvepisode.ShowId.Value = thisresult.InnerText 
                            Case "fileinfo"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 In thisresult.ChildNodes
                                    Select Case detail2.Name
                                        Case "streamdetails"
                                            Dim detail As XmlNode = Nothing
                                            For Each detail In detail2.ChildNodes
                                                Select Case detail.Name
                                                    Case "video"
                                                        Dim videodetails As XmlNode = Nothing
                                                        For Each videodetails In detail.ChildNodes
                                                            Select Case videodetails.Name
                                                                Case "width"
                                                                    newtvepisode.Details.StreamDetails.Video.Width.Value = videodetails.InnerText
                                                                Case "height"
                                                                    newtvepisode.Details.StreamDetails.Video.Height.Value = videodetails.InnerText
                                                                Case "aspect"
                                                                    newtvepisode.Details.StreamDetails.Video.Aspect.Value = videodetails.InnerText
                                                                Case "codec"
                                                                    newtvepisode.Details.StreamDetails.Video.Codec.Value = videodetails.InnerText
                                                                Case "formatinfo"
                                                                    newtvepisode.Details.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
                                                                Case "durationinseconds"
                                                                    newtvepisode.Details.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
                                                                Case "bitrate"
                                                                    newtvepisode.Details.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
                                                                Case "bitratemode"
                                                                    newtvepisode.Details.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
                                                                Case "bitratemax"
                                                                    newtvepisode.Details.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
                                                                Case "container"
                                                                    newtvepisode.Details.StreamDetails.Video.Container.Value = videodetails.InnerText
                                                                Case "codecid"
                                                                    newtvepisode.Details.StreamDetails.Video.CodecId.Value = videodetails.InnerText
                                                                Case "codecidinfo"
                                                                    newtvepisode.Details.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
                                                                Case "scantype"
                                                                    newtvepisode.Details.StreamDetails.Video.ScanType.Value = videodetails.InnerText
                                                            End Select
                                                        Next
                                                    Case "audio"
                                                        Dim audiodetails As XmlNode = Nothing
                                                        Dim audio As New AudioDetails
                                                        For Each audiodetails In detail.ChildNodes
                                                            Select Case audiodetails.Name
                                                                Case "language"
                                                                    audio.Language.Value = audiodetails.InnerText
                                                                Case "codec"
                                                                    audio.Codec.Value = audiodetails.InnerText
                                                                Case "channels"
                                                                    audio.Channels.Value = audiodetails.InnerText
                                                                Case "bitrate"
                                                                    audio.Bitrate.Value = audiodetails.InnerText
                                                            End Select
                                                        Next
                                                        newtvepisode.Details.StreamDetails.Audio.Add(audio)
                                                    Case "subtitle"
                                                        Dim subsdetails As XmlNode = Nothing
                                                        For Each subsdetails In detail.ChildNodes
                                                            Select Case subsdetails.Name
                                                                Case "language"
                                                                    Dim sublang As New SubtitleDetails
                                                                    sublang.Language.Value = subsdetails.InnerText
                                                                    newtvepisode.Details.StreamDetails.Subtitles.Add(sublang)
                                                            End Select
                                                        Next
                                                End Select
                                            Next
                                            'newtvepisode.Details = newfilenfo
                                    End Select
                                Next
                        End Select

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Next

                If newtvepisode.Episode.Value = Nothing Or newtvepisode.Episode.Value = Nothing Then
                    For Each regexp In Preferences.tv_RegexScraper

                        Dim M As Match
                        M = Regex.Match(newtvepisode.NfoFilePath, regexp)
                        If M.Success = True Then
                            Try
                                newtvepisode.Season.Value = M.Groups(1).Value.ToString
                                newtvepisode.Episode.Value = M.Groups(2).Value.ToString
                                Exit For
                            Catch
                                newtvepisode.Season.Value = "-1"
                                newtvepisode.Season.Value = "-1"
                            End Try
                        End If
                    Next
                End If
                If newtvepisode.Episode.Value = Nothing Then
                    newtvepisode.Episode.Value = "-1"
                End If
                If newtvepisode.Season.Value = Nothing Then
                    newtvepisode.Season.Value = "-1"
                End If
                If newtvepisode.TvdbId = Nothing Then newtvepisode.TvdbId.Value = ""
                'If newtvepisode.status = Nothing Then newtvepisode.status = ""
                If newtvepisode.Rating = Nothing Then newtvepisode.Rating.Value = ""
                episodelist.Add(newtvepisode)
            ElseIf tvshow.DocumentElement.Name = "multiepisodenfo" Or tvshow.DocumentElement.Name = "xbmcmultiepisode" Then
                Dim temp As String = tvshow.DocumentElement.Name
                For Each thisresult In tvshow(temp)
                    Dim sp As String = thisresult.InnerXml.ToString
                    If sp.Contains("<streamdetails><fileinfo>") Then
                        fixmulti = True
                        Try
                            sp = sp.Replace("<streamdetails><fileinfo>", "<fileinfo><streamdetails>")
                            sp = sp.Replace("</fileinfo></streamdetails>", "</streamdetails></fileinfo>")
                            thisresult.InnerXml = sp
                        'thisresult.InnerXml = thisresult.InnerXml.Replace("<streamdetails><fileinfo>","<fileinfo><streamdetails>")
                        'thisresult.InnerXml = thisresult.InnerXml.Replace("</fileinfo></streamdetails>","</streamdetails></fileinfo>")
                        Catch
                        End Try
                    End If
                    Select Case thisresult.Name
                        Case "episodedetails"
                            Dim newepisodenfo As XmlNode = Nothing
                            Dim anotherepisode As New TvEpisode
                            Dim filedetails As String = ""
                            Dim audio As String = ""

                            Dim tempint As Integer = thisresult.ChildNodes.Count - 1
                            For f = 0 To tempint
                                Try
                                    Select Case thisresult.ChildNodes(f).Name
                                        Case "title"
                                            anotherepisode.Title.Value = thisresult.ChildNodes(f).InnerText
                                        Case "season"
                                            anotherepisode.Season.Value = thisresult.ChildNodes(f).InnerText
                                        Case "episode"
                                            anotherepisode.Episode.Value = thisresult.ChildNodes(f).InnerText
                                        Case "tvdbid"
                                            anotherepisode.TvdbId.Value = thisresult.ChildNodes(f).InnerText
                                        Case "rating"
                                            anotherepisode.Rating.Value = thisresult.ChildNodes(f).InnerText
                                            If anotherepisode.Rating.IndexOf("/10") <> -1 Then anotherepisode.Rating.Value.Replace("/10", "")
                                            If anotherepisode.Rating.IndexOf(" ") <> -1 Then anotherepisode.Rating.Value.Replace(" ", "")
                                        Case "playcount"
                                            anotherepisode.PlayCount.Value = thisresult.ChildNodes(f).InnerText
                                        Case "plot"
                                            anotherepisode.Plot.Value = thisresult.ChildNodes(f).InnerText
                                        Case "director"
                                            anotherepisode.Director.Value = thisresult.ChildNodes(f).InnerText
                                        Case "credits"
                                            anotherepisode.Credits.Value = thisresult.ChildNodes(f).InnerText
                                        Case "aired"
                                            anotherepisode.Aired.Value = thisresult.ChildNodes(f).InnerText
                                        Case "videosource"
                                            anotherepisode.Source.Value = thisresult.ChildNodes(f).InnerText 
                                        Case "showid"
                                            anotherepisode.ShowId.Value = thisresult.ChildNodes(f).InnerText 
                                        Case "fileinfo"
                                            Dim detail2 As XmlNode = Nothing
                                            For Each detail2 In thisresult.ChildNodes(f)
                                                Select Case detail2.Name
                                                    Case "streamdetails"

                                                        Dim detail As XmlNode = Nothing
                                                        For Each detail In detail2.ChildNodes
                                                            Select Case detail.Name
                                                                Case "video"
                                                                    Dim videodetails As XmlNode = Nothing
                                                                    For Each videodetails In detail.ChildNodes
                                                                        Select Case videodetails.Name
                                                                            Case "width"
                                                                                If videodetails.InnerText.ToLower.Contains("kbps") Then   'in place as multieps mediainfo was stored incorrectly.
                                                                                    anotherepisode.Details.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
                                                                                Else
                                                                                    anotherepisode.Details.StreamDetails.Video.Width.Value = videodetails.InnerText
                                                                                End If
                                                                            Case "height"
                                                                                anotherepisode.Details.StreamDetails.Video.Height.Value = videodetails.InnerText
                                                                            Case "aspect"
                                                                                anotherepisode.Details.StreamDetails.Video.Aspect.Value = videodetails.InnerText
                                                                            Case "codec"
                                                                                anotherepisode.Details.StreamDetails.Video.Codec.Value = videodetails.InnerText
                                                                            Case "formatinfo"
                                                                                anotherepisode.Details.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
                                                                            Case "durationinseconds"
                                                                                anotherepisode.Details.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
                                                                            Case "bitrate"
                                                                                anotherepisode.Details.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
                                                                            Case "bitratemode"
                                                                                anotherepisode.Details.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
                                                                            Case "bitratemax"
                                                                                anotherepisode.Details.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
                                                                            Case "container"
                                                                                anotherepisode.Details.StreamDetails.Video.Container.Value = videodetails.InnerText
                                                                            Case "codecid"
                                                                                anotherepisode.Details.StreamDetails.Video.CodecId.Value = videodetails.InnerText
                                                                            Case "codecidinfo"
                                                                                anotherepisode.Details.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
                                                                            Case "scantype"
                                                                                anotherepisode.Details.StreamDetails.Video.ScanType.Value = videodetails.InnerText
                                                                        End Select
                                                                    Next
                                                                Case "audio"
                                                                    Dim audiodetails As XmlNode = Nothing
                                                                    Dim audio2 As New AudioDetails
                                                                    For Each audiodetails In detail.ChildNodes
                                                                        Select Case audiodetails.Name
                                                                            Case "language"
                                                                                audio2.Language.Value = audiodetails.InnerText
                                                                            Case "codec"
                                                                                audio2.Codec.Value = audiodetails.InnerText
                                                                            Case "channels"
                                                                                audio2.Channels.Value = audiodetails.InnerText
                                                                            Case "bitrate"
                                                                                audio2.Bitrate.Value = audiodetails.InnerText
                                                                        End Select
                                                                    Next
                                                                    anotherepisode.Details.StreamDetails.Audio.Add(audio2)
                                                                Case "subtitle"
                                                                    Dim subsdetails As XmlNode = Nothing
                                                                    'Dim subs2 As New subtitle
                                                                    For Each subsdetails In detail.ChildNodes
                                                                        Select Case subsdetails.Name
                                                                            Case "language"
                                                                                Dim sublang As New SubtitleDetails
                                                                                sublang.Language.Value = subsdetails.InnerText
                                                                                anotherepisode.Details.StreamDetails.Subtitles.Add(sublang)
                                                                        End Select
                                                                    Next
                                                            End Select
                                                        Next
                                                        'newtvepisode.Details = newfilenfo
                                                End Select
                                            Next
                                    End Select
                                Catch ex As Exception
                                    MsgBox(ex.ToString)
                                End Try
                            Next f
                            Try
                                anotherepisode.NfoFilePath = path
                                If anotherepisode.Episode.Value = Nothing Or anotherepisode.Episode.Value = Nothing Then
                                    For Each regexp In Preferences.tv_RegexScraper

                                        Dim M As Match
                                        M = Regex.Match(anotherepisode.NfoFilePath, regexp)
                                        If M.Success = True Then
                                            Try
                                                anotherepisode.Season.Value = M.Groups(1).Value.ToString
                                                anotherepisode.Episode.Value = M.Groups(2).Value.ToString
                                                Exit For
                                            Catch
                                                anotherepisode.Season.Value = "-1"
                                                anotherepisode.Season.Value = "-1"
                                            End Try
                                        End If
                                    Next
                                End If
                                If anotherepisode.Episode.Value = Nothing Then
                                    anotherepisode.Episode.Value = "-1"
                                End If
                                If anotherepisode.Season.Value = Nothing Then
                                    anotherepisode.Season.Value = "-1"
                                End If
                                If anotherepisode.TvdbId = Nothing Then anotherepisode.TvdbId.Value = ""
                                'If anotherepisode.status = Nothing Then anotherepisode.status = ""
                                If anotherepisode.Rating = Nothing Then anotherepisode.Rating.Value = ""
                                episodelist.Add(anotherepisode)
                            Catch ex As Exception
                                MsgBox(ex.ToString)
                            End Try
                    End Select
                Next
                If fixmulti Then
                    ep_NfoSave(episodelist, path)
                End If
            End If

            Return episodelist
        End If

        'Catch
        'End Try
    End Function

    Public Shared Sub ep_NfoSave(ByVal listofepisodes As List(Of TvEpisode), ByVal path As String)
        Dim document As New XmlDocument
        Dim root As XmlElement
        Dim xmlEpisode As XmlElement
        Dim xmlEpisodechild As XmlElement
        Dim xmlStreamDetails As XmlElement
        Dim xmlFileInfo As XmlElement
        'Dim xmlFileInfoType As 
        Dim xmlStreamDetailsType As XmlElement
        'Dim xmlFileInfoTypechild As XmlElement
        Dim xmlStreamDetailsTypeChild As XmlElement
        Dim xmlActor As XmlElement
        Dim xmlActorchild As XmlElement

        Dim xmlproc As XmlDeclaration
        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        document.AppendChild(xmlproc)

        root = document.CreateElement("multiepisodenfo")    'root starts out as multiepisode, but tested for single episode after initial population.
        For Each ep In listofepisodes

            xmlEpisode = document.CreateElement("episodedetails")
            If Preferences.enabletvhdtags = True Then
                Try
                    xmlFileInfo = document.CreateElement("fileinfo")
               xmlStreamDetails = document.CreateElement("streamdetails")
                    xmlStreamDetailsType = document.CreateElement("video")
                    If ep.Details.StreamDetails.Video.Width <> Nothing Then
                        If ep.Details.StreamDetails.Video.Width.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("width")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.Width.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.Height <> Nothing Then
                        If ep.Details.StreamDetails.Video.Height.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("height")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.Height.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.Aspect <> Nothing Then
                        If ep.Details.StreamDetails.Video.Aspect.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("aspect")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.Aspect.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.Codec <> Nothing Then
                        If ep.Details.StreamDetails.Video.Codec.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("codec")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.Codec.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.FormatInfo <> Nothing Then
                        If ep.Details.StreamDetails.Video.FormatInfo.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("format")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.FormatInfo.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.DurationInSeconds.Value <> Nothing Then
                        If ep.Details.StreamDetails.Video.DurationInSeconds.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("durationinseconds")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.DurationInSeconds.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.Bitrate <> Nothing Then
                        If ep.Details.StreamDetails.Video.Bitrate.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("bitrate")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.Bitrate.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.BitrateMode <> Nothing Then
                        If ep.Details.StreamDetails.Video.BitrateMode.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("bitratemode")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.BitrateMode.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.BitrateMax <> Nothing Then
                        If ep.Details.StreamDetails.Video.BitrateMax.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("bitratemax")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.BitrateMax.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.Container <> Nothing Then
                        If ep.Details.StreamDetails.Video.Container.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("container")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.Container.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.CodecId <> Nothing Then
                        If ep.Details.StreamDetails.Video.CodecId.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("codecid")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.CodecId.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.CodecInfo <> Nothing Then
                        If ep.Details.StreamDetails.Video.CodecInfo.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("codecidinfo")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.CodecInfo.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    If ep.Details.StreamDetails.Video.ScanType <> Nothing Then
                        If ep.Details.StreamDetails.Video.ScanType.Value <> "" Then
                            xmlStreamDetailsTypeChild = document.CreateElement("scantype")
                            xmlStreamDetailsTypeChild.InnerText = ep.Details.StreamDetails.Video.ScanType.Value
                            xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                        End If
                    End If
                    xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                    If ep.Details.StreamDetails.Audio.Count > 0 Then
                        For Each aud In ep.Details.StreamDetails.Audio
                            xmlStreamDetailsType = document.CreateElement("audio")
                            If aud.Language <> Nothing Then
                                If aud.Language.Value <> "" Then
                                    xmlStreamDetailsTypeChild = document.CreateElement("language")
                                    xmlStreamDetailsTypeChild.InnerText = aud.Language.Value
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                                End If
                            End If
                            If aud.Codec <> Nothing Then
                                If aud.Codec.Value <> "" Then
                                    xmlStreamDetailsTypeChild = document.CreateElement("codec")
                                    xmlStreamDetailsTypeChild.InnerText = aud.Codec.Value
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                                End If
                            End If
                            If aud.Channels <> Nothing Then
                                If aud.Channels.Value <> "" Then
                                    xmlStreamDetailsTypeChild = document.CreateElement("channels")
                                    xmlStreamDetailsTypeChild.InnerText = aud.Channels.Value
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                                End If
                            End If
                            If aud.Bitrate <> Nothing Then
                                If aud.Bitrate.Value <> "" Then
                                    xmlStreamDetailsTypeChild = document.CreateElement("bitrate")
                                    xmlStreamDetailsTypeChild.InnerText = aud.Bitrate.Value
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                                End If
                            End If
                            xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                        Next
                    End If
                    If ep.Details.StreamDetails.Subtitles.Count > 0 Then
                        For Each subt In ep.Details.StreamDetails.Subtitles
                            If subt.Language <> Nothing Then
                                If subt.Language.Value <> "" Then
                                    xmlStreamDetailsType = document.CreateElement("subtitle")
                                    xmlStreamDetailsTypeChild = document.CreateElement("language")
                                    xmlStreamDetailsTypeChild.InnerText = subt.Language.Value
                                    xmlStreamDetailsType.AppendChild(xmlStreamDetailsTypeChild)
                                End If
                            End If
                            xmlStreamDetails.AppendChild(xmlStreamDetailsType)
                        Next
                    End If
                    xmlFileInfo.AppendChild(xmlStreamDetails)
                    xmlEpisode.AppendChild(xmlFileInfo)
                Catch
                End Try
            End If

            xmlEpisodechild = document.CreateElement("title")
            xmlEpisodechild.InnerText = ep.Title.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("season")
            xmlEpisodechild.InnerText = ep.Season.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("episode")
            xmlEpisodechild.InnerText = ep.Episode.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("aired")
            xmlEpisodechild.InnerText = ep.Aired.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("plot")
            xmlEpisodechild.InnerText = ep.Plot.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("playcount")
            xmlEpisodechild.InnerText = ep.PlayCount.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("director")
            xmlEpisodechild.InnerText = ep.Director.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("credits")
            xmlEpisodechild.InnerText = ep.Credits.Value
            xmlEpisode.AppendChild(xmlEpisodechild)


            xmlEpisodechild = document.CreateElement("rating")
            xmlEpisodechild.InnerText = ep.Rating.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            'xmlEpisodechild = document.CreateElement("thumb")
            'xmlEpisodechild.InnerText = ep.Thumbnail.Path
            'xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("runtime")
            xmlEpisodechild.InnerText = ep.Runtime.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("showid")
            xmlEpisodechild.InnerText = ep.ShowId.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            xmlEpisodechild = document.CreateElement("videosource")
            xmlEpisodechild.InnerText = ep.Source.Value
            xmlEpisode.AppendChild(xmlEpisodechild)

            Dim actorstosave As Integer = ep.ListActors.Count
            If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
            For f = 0 To actorstosave - 1
                xmlActor = document.CreateElement("actor")
                xmlActorchild = document.CreateElement("name")
                xmlActorchild.InnerText = ep.ListActors(f).actorname
                xmlActor.AppendChild(xmlActorchild)
                xmlActorchild = document.CreateElement("role")
                xmlActorchild.InnerText = ep.ListActors(f).actorrole
                xmlActor.AppendChild(xmlActorchild)
                If ep.ListActors(f).actorthumb <> Nothing Then
                    If ep.ListActors(f).actorthumb <> "" Then
                        xmlActorchild = document.CreateElement("thumb")
                        xmlActorchild.InnerText = ep.ListActors(f).actorthumb
                        xmlActor.AppendChild(xmlActorchild)
                    End If
                End If
                xmlEpisode.AppendChild(xmlActor)
            Next
            If listofepisodes.Count = 1 Then    'file is a single episode
                root = xmlEpisode               'root now equals 'episodedetails'
                Exit For                        'now append to XML doc as a single episode
            End If
            root.AppendChild(xmlEpisode)        'otherwise, each episode is appended to the 'multiepisode' element
        Next

        document.AppendChild(root)
        Try
            Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented

            document.WriteTo(output)
            output.Close()
        Catch
        End Try

    End Sub

    Public Function tv_NfoLoadFull(ByVal path As String) As TvShow


        Dim newtvshow As New TvShow
        If Not IO.File.Exists(path) Then
            newtvshow.Title.Value = Utilities.GetLastFolder(path)
            newtvshow.Year.Value = newtvshow.Title.Value & " (0000)"
            newtvshow.Plot.Value = "problem loading tvshow.nfo, file does not exist." & vbCrLf & "Use the TV Show Selector Tab to create one"
            newtvshow.Status.Value = "file does not exist"
            newtvshow.NfoFilePath = path
            newtvshow.Year.Value = "0000"
            newtvshow.TvdbId.Value = ""
            newtvshow.State = Media_Companion.ShowState.Locked
            Return newtvshow
            Exit Function
        Else
            newtvshow.NfoFilePath = path
            newtvshow.Load()
            'Fix episodeguide tag
            Dim lang As String = newtvshow.EpisodeGuideUrl.Value
            If String.IsNullOrEmpty(lang) Then
                lang = "en"
            Else
                lang = lang.Substring((lang.LastIndexOf("/") + 1)).Replace(".zip", "")
            End If

            If Not newtvshow.TvdbId.Value = "" Then
            newtvshow.EpisodeGuideUrl.Value = ""
                newtvshow.Url.Value = URLs.EpisodeGuide(newtvshow.TvdbId.Value, lang)
                newtvshow.Url.Node.SetAttributeValue("cache", newtvshow.TvdbId.Value)
            End If
            'end fix
        End If
        For Each season As TvSeason In newtvshow.Seasons.Values
            For Each episode In season.Episodes
                episode.ShowId = newtvshow.TvdbId
            Next
        Next
        Return newtvshow

    End Function

    Public Sub tv_NfoSave(ByVal Path As String, ByRef Show As TvShow, Optional ByVal overwrite As Boolean = True, Optional ByVal forceunlocked As String = "")
        If IO.File.Exists(Path) And Not overwrite Then Exit Sub
        
        Show.Save(Path)
    End Sub
#End Region

#Region " Obsolete "

    'Public Shared Function ep_NfoLoadGeneric(ByVal path As String) ', ByVal season As String, ByVal episode As String)

    '    Dim newepisodelist As New List(Of TvEpisode)
    '    Dim newepisode As New TvEpisode
    '    If Not IO.File.Exists(path) Then
    '        newepisode.Title.Value = IO.Path.GetFileName(path)
    '        newepisode.Plot.Value = "missing file"

    '        newepisode.NfoFilePath = path
    '        If newepisode.Episode.Value = Nothing Or newepisode.Episode.Value = Nothing Then
    '            For Each regexp In Preferences.tv_RegexScraper

    '                Dim M As Match
    '                M = Regex.Match(newepisode.NfoFilePath, regexp)
    '                If M.Success = True Then
    '                    Try
    '                        newepisode.Season.Value = M.Groups(1).Value.ToString
    '                        newepisode.Episode.Value = M.Groups(2).Value.ToString
    '                        Exit For
    '                    Catch
    '                        newepisode.Season.Value = "-1"
    '                        newepisode.Season.Value = "-1"
    '                    End Try
    '                End If
    '            Next
    '        End If
    '        If newepisode.Episode.Value = Nothing Then
    '            newepisode.Episode.Value = "-1"
    '        End If
    '        If newepisode.Season.value = Nothing Then
    '            newepisode.Season.value = "-1"
    '        End If
    '        newepisodelist.Add(newepisode)
    '        Return newepisodelist
    '        Exit Function
    '    Else
    '        Dim tvshow As New XmlDocument
    '        Try
    '            tvshow.Load(path)
    '        Catch ex As Exception
    '            'If Not validate_nfo(path) Then
    '            '    Exit Function
    '            'End If
    '            newepisode.Title.Value = IO.Path.GetFileName(path)
    '            newepisode.Plot.Value = "problem / xml error"
    '            newepisode.NfoFilePath = path
    '            'newepisode.VideoFilePath = path
    '            If newepisode.Episode.Value = Nothing Or newepisode.Episode.Value = Nothing Then
    '                For Each regexp In Preferences.tv_RegexScraper

    '                    Dim M As Match
    '                    M = Regex.Match(newepisode.NfoFilePath, regexp)
    '                    If M.Success = True Then
    '                        Try
    '                            newepisode.Season.Value = M.Groups(1).Value.ToString
    '                            newepisode.Episode.Value = M.Groups(2).Value.ToString
    '                            Exit For
    '                        Catch
    '                            newepisode.Season.Value = "-1"
    '                            newepisode.Season.Value = "-1"
    '                        End Try
    '                    End If
    '                Next
    '            End If
    '            If newepisode.Episode.Value = Nothing Then
    '                newepisode.Episode.Value = "-1"
    '            End If
    '            If newepisode.Season.value = Nothing Then
    '                newepisode.Season.value = "-1"
    '            End If
    '            newepisodelist.Add(newepisode)
    '            Return newepisodelist
    '            Exit Function
    '        End Try

    '        Dim thisresult As XmlNode = Nothing
    '        Dim tempid As String = ""
    '        If tvshow.DocumentElement.Name = "episodedetails" Then
    '            Dim newtvepisode As New TvEpisode
    '            For Each thisresult In tvshow("episodedetails")
    '                Try
    '                    newtvepisode.NfoFilePath = path
    '                    Select Case thisresult.Name
    '                        Case "credits"
    '                            newtvepisode.Credits.Value = thisresult.InnerText
    '                        Case "director"
    '                            newtvepisode.Director.Value = thisresult.InnerText
    '                        Case "aired"
    '                            newtvepisode.Aired.Value = thisresult.InnerText
    '                        Case "plot"
    '                            newtvepisode.Plot.Value = thisresult.InnerText
    '                        Case "title"
    '                            newtvepisode.Title.Value = thisresult.InnerText
    '                        Case "season"
    '                            newtvepisode.Season.value = thisresult.InnerText
    '                        Case "episode"
    '                            newtvepisode.Episode.Value = thisresult.InnerText
    '                        Case "rating"
    '                            newtvepisode.Rating.Value = thisresult.InnerText
    '                            If newtvepisode.Rating.IndexOf("/10") <> -1 Then newtvepisode.Rating.Value.Replace("/10", "")
    '                            If newtvepisode.Rating.IndexOf(" ") <> -1 Then newtvepisode.Rating.Value.Replace(" ", "")
    '                        Case "playcount"
    '                            newtvepisode.PlayCount.Value = thisresult.InnerText
    '                        Case "thumb"
    '                            newtvepisode.Thumbnail.FileName = thisresult.InnerText
    '                        Case "actor"
    '                            Dim actordetail As XmlNode = Nothing
    '                            Dim newactor As New str_MovieActors(SetDefaults)
    '                            For Each actordetail In thisresult.ChildNodes
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
    '                                        Dim detail As XmlNode = Nothing
    '                                        For Each detail In detail2.ChildNodes
    '                                            Select Case detail.Name
    '                                                Case "video"
    '                                                    Dim videodetails As XmlNode = Nothing
    '                                                    For Each videodetails In detail.ChildNodes
    '                                                        Select Case videodetails.Name
    '                                                            Case "width"
    '                                                                newtvepisode.Details.StreamDetails.Video.Width.Value = videodetails.InnerText
    '                                                            Case "height"
    '                                                                newtvepisode.Details.StreamDetails.Video.Height.Value = videodetails.InnerText
    '                                                            Case "aspect"
    '                                                                newtvepisode.Details.StreamDetails.Video.Aspect.Value = videodetails.InnerText
    '                                                            Case "codec"
    '                                                                newtvepisode.Details.StreamDetails.Video.Codec.Value = videodetails.InnerText
    '                                                            Case "formatinfo"
    '                                                                newtvepisode.Details.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
    '                                                            Case "durationinseconds"
    '                                                                newtvepisode.Details.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
    '                                                            Case "bitrate"
    '                                                                newtvepisode.Details.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
    '                                                            Case "bitratemode"
    '                                                                newtvepisode.Details.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
    '                                                            Case "bitratemax"
    '                                                                newtvepisode.Details.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
    '                                                            Case "container"
    '                                                                newtvepisode.Details.StreamDetails.Video.Container.Value = videodetails.InnerText
    '                                                            Case "codecid"
    '                                                                newtvepisode.Details.StreamDetails.Video.CodecId.Value = videodetails.InnerText
    '                                                            Case "codecidinfo"
    '                                                                newtvepisode.Details.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
    '                                                            Case "scantype"
    '                                                                newtvepisode.Details.StreamDetails.Video.ScanType.Value = videodetails.InnerText
    '                                                        End Select
    '                                                    Next
    '                                                Case "audio"
    '                                                    Dim audiodetails As XmlNode = Nothing
    '                                                    Dim audio As New AudioDetails
    '                                                    For Each audiodetails In detail.ChildNodes
    '                                                        Select Case audiodetails.Name
    '                                                            Case "language"
    '                                                                audio.Language.Value = audiodetails.InnerText
    '                                                            Case "codec"
    '                                                                audio.Codec.Value = audiodetails.InnerText
    '                                                            Case "channels"
    '                                                                audio.Channels.Value = audiodetails.InnerText
    '                                                            Case "bitrate"
    '                                                                audio.Bitrate.Value = audiodetails.InnerText
    '                                                        End Select
    '                                                    Next
    '                                                    newtvepisode.Details.StreamDetails.Audio.Add(audio)
    '                                                Case "subtitle"
    '                                                    Dim subsdetails As XmlNode = Nothing
    '                                                    For Each subsdetails In detail.ChildNodes
    '                                                        Select Case subsdetails.Name
    '                                                            Case "language"
    '                                                                Dim sublang As New SubtitleDetails
    '                                                                sublang.Language.Value = subsdetails.InnerText
    '                                                                newtvepisode.Details.StreamDetails.Subtitles.Add(sublang)
    '                                                        End Select
    '                                                    Next
    '                                            End Select
    '                                        Next
    '                                        'newtvepisode.Details = newfilenfo
    '                                End Select
    '                            Next
    '                    End Select
    '                Catch ex As Exception
    '                    MsgBox(ex.ToString)
    '                End Try
    '            Next

    '            If newtvepisode.Episode.Value = Nothing Or newtvepisode.Episode.Value = Nothing Then
    '                For Each regexp In Preferences.tv_RegexScraper

    '                    Dim M As Match
    '                    M = Regex.Match(newtvepisode.NfoFilePath, regexp)
    '                    If M.Success = True Then
    '                        Try
    '                            newtvepisode.Season.Value = M.Groups(1).Value.ToString
    '                            newtvepisode.Episode.Value = M.Groups(2).Value.ToString
    '                            Exit For
    '                        Catch
    '                            newtvepisode.Season.Value = "-1"
    '                            newtvepisode.Season.Value = "-1"
    '                        End Try
    '                    End If
    '                Next
    '            End If
    '            If newtvepisode.Episode.Value = Nothing Then
    '                newtvepisode.Episode.Value = "-1"
    '            End If
    '            If newtvepisode.Season.value = Nothing Then
    '                newtvepisode.Season.value = "-1"
    '            End If
    '            If newtvepisode.Rating = Nothing Then newtvepisode.Rating.Value = ""
    '            newepisodelist.Add(newtvepisode)
    '            Return newepisodelist
    '            Exit Function
    '        ElseIf tvshow.DocumentElement.Name = "multiepisodenfo" Then
    '            For Each thisresult In tvshow("multiepisodenfo")
    '                Select Case thisresult.Name
    '                    Case "episodedetails"
    '                        Dim newepisodenfo As XmlNode = Nothing
    '                        Dim anotherepisode As New TvEpisode

    '                        'anotherepisode.NfoFilePath = Nothing
    '                        'anotherepisode.playcount = Nothing
    '                        'anotherepisode.rating = Nothing
    '                        'anotherepisode.Season.value = Nothing
    '                        'anotherepisode.title = Nothing
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
    '                                        anotherepisode.Credits.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "director"
    '                                        anotherepisode.Director.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "aired"
    '                                        anotherepisode.Aired.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "plot"
    '                                        anotherepisode.Plot.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "title"
    '                                        anotherepisode.Title.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "season"
    '                                        anotherepisode.Season.value = thisresult.ChildNodes(f).InnerText
    '                                    Case "episode"
    '                                        anotherepisode.Episode.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "rating"
    '                                        anotherepisode.Rating.Value = thisresult.ChildNodes(f).InnerText
    '                                        If anotherepisode.Rating.IndexOf("/10") <> -1 Then anotherepisode.Rating.Value.Replace("/10", "")
    '                                        If anotherepisode.Rating.IndexOf(" ") <> -1 Then anotherepisode.Rating.Value.Replace(" ", "")
    '                                    Case "playcount"
    '                                        anotherepisode.PlayCount.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "thumb"
    '                                        anotherepisode.Thumbnail.FileName = thisresult.ChildNodes(f).InnerText
    '                                    Case "runtime"
    '                                        anotherepisode.Runtime.Value = thisresult.ChildNodes(f).InnerText
    '                                    Case "actor"
    '                                        Dim detail As XmlNode = Nothing
    '                                        Dim newactor As New str_MovieActors(SetDefaults)
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

    '                                                    Dim detail As XmlNode = Nothing
    '                                                    For Each detail In detail2.ChildNodes
    '                                                        Select Case detail.Name
    '                                                            Case "video"
    '                                                                Dim videodetails As XmlNode = Nothing
    '                                                                For Each videodetails In detail.ChildNodes
    '                                                                    Select Case videodetails.Name
    '                                                                        Case "width"
    '                                                                            anotherepisode.Details.StreamDetails.Video.Width.Value = videodetails.InnerText
    '                                                                        Case "height"
    '                                                                            anotherepisode.Details.StreamDetails.Video.Height.Value = videodetails.InnerText
    '                                                                        Case "codec"
    '                                                                            anotherepisode.Details.StreamDetails.Video.Codec.Value = videodetails.InnerText
    '                                                                        Case "formatinfo"
    '                                                                            anotherepisode.Details.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
    '                                                                        Case "durationinseconds"
    '                                                                            anotherepisode.Details.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
    '                                                                        Case "bitrate"
    '                                                                            anotherepisode.Details.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
    '                                                                        Case "bitratemode"
    '                                                                            anotherepisode.Details.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
    '                                                                        Case "bitratemax"
    '                                                                            anotherepisode.Details.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
    '                                                                        Case "container"
    '                                                                            anotherepisode.Details.StreamDetails.Video.Container.Value = videodetails.InnerText
    '                                                                        Case "codecid"
    '                                                                            anotherepisode.Details.StreamDetails.Video.CodecId.Value = videodetails.InnerText
    '                                                                        Case "codecidinfo"
    '                                                                            anotherepisode.Details.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
    '                                                                        Case "scantype"
    '                                                                            anotherepisode.Details.StreamDetails.Video.ScanType.Value = videodetails.InnerText
    '                                                                    End Select
    '                                                                Next
    '                                                            Case "audio"
    '                                                                Dim audiodetails As XmlNode = Nothing
    '                                                                Dim audio As New AudioDetails ' str_MediaNFOAudio(SetDefaults)
    '                                                                For Each audiodetails In detail.ChildNodes

    '                                                                    Select Case audiodetails.Name
    '                                                                        Case "language"
    '                                                                            audio.language.value = audiodetails.InnerText
    '                                                                        Case "codec"
    '                                                                            audio.Codec.Value = audiodetails.InnerText
    '                                                                        Case "channels"
    '                                                                            audio.Channels.Value = audiodetails.InnerText
    '                                                                        Case "bitrate"
    '                                                                            audio.Bitrate.Value = audiodetails.InnerText
    '                                                                    End Select
    '                                                                Next
    '                                                                anotherepisode.Details.StreamDetails.Audio.Add(audio)
    '                                                            Case "subtitle"
    '                                                                Dim subsdetails As XmlNode = Nothing
    '                                                                For Each subsdetails In detail.ChildNodes
    '                                                                    Select Case subsdetails.Name
    '                                                                        Case "language"
    '                                                                            Dim sublang As New SubtitleDetails
    '                                                                            sublang.Language.Value = subsdetails.InnerText
    '                                                                            anotherepisode.Details.StreamDetails.Subtitles.Add(sublang)
    '                                                                    End Select
    '                                                                Next
    '                                                        End Select
    '                                                    Next
    '                                                    'anotherepisode.Details = newfilenfo
    '                                            End Select
    '                                        Next
    '                                End Select


    '                            Catch ex As Exception
    '                                MsgBox(ex.ToString)
    '                            End Try
    '                        Next f
    '                        anotherepisode.NfoFilePath = path
    '                        newepisodelist.Add(anotherepisode)
    '                End Select
    '            Next
    '            Return newepisodelist
    '        End If


    '    End If
    '    Return "Error"
    'End Function

    'Public Sub saveepisodenfo(ByVal listofepisodes As List(Of TvEpisode), ByVal path As String, Optional ByVal seasonno As String = "-2", Optional ByVal episodeno As String = "-2", Optional ByVal batch As Boolean = False)
    '    If listofepisodes.Count = 1 Then
    '        'Hack to get ShowID with the data available at this point
    '        Dim ThumbnailPath As String = listofepisodes(0).Thumbnail.FileName
    '        Dim Split As String() = ThumbnailPath.Split("/")
    '        Dim FoundShowID As String = ""
    '        If Split.Length >= 6 Then
    '            FoundShowID = Split(5)
    '        End If
    '        'end hack

    '        Dim document As New XmlDocument
    '        Dim root As XmlElement
    '        Dim child As XmlElement
    '        Dim actorchild As XmlElement
    '        Dim filedetailschild As XmlElement
    '        Dim filedetailschildchild As XmlElement
    '        root = document.CreateElement("episodedetails")
    '        Dim thispref As XmlNode = Nothing
    '        Dim xmlproc As XmlDeclaration

    '        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
    '        document.AppendChild(xmlproc)
    '        Dim anotherchild As XmlNode = Nothing
    '        If Preferences.enabletvhdtags = True Then
    '            Try
    '                child = document.CreateElement("fileinfo")

    '                anotherchild = document.CreateElement("streamdetails")

    '                filedetailschild = document.CreateElement("video")
    '                If listofepisodes(0).Details.StreamDetails.Video.Width <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.Width.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("width")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.Width.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.Height <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.Height.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("height")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.Height.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.Aspect <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.Aspect.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("aspect")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.Aspect.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.Codec <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.Codec.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("codec")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.Codec.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.FormatInfo <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.FormatInfo.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("format")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.FormatInfo.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.DurationInSeconds <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.DurationInSeconds.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("durationinseconds")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.DurationInSeconds.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.Bitrate <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.Bitrate.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("bitrate")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.Bitrate.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.BitrateMode <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.BitrateMode.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("bitratemode")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.BitrateMode.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.BitrateMax <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.BitrateMax.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("bitratemax")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.BitrateMax.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.Container <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.Container.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("container")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.Container.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.CodecId <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.CodecId.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("codecid")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.CodecId.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.CodecInfo <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.CodecInfo.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("codecidinfo")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.CodecInfo.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).Details.StreamDetails.Video.ScanType <> Nothing Then
    '                    If listofepisodes(0).Details.StreamDetails.Video.ScanType.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("scantype")
    '                        filedetailschildchild.InnerText = listofepisodes(0).Details.StreamDetails.Video.ScanType.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                anotherchild.AppendChild(filedetailschild)

    '                If listofepisodes(0).Details.StreamDetails.Audio.Count > 0 Then
    '                    For Each item In listofepisodes(0).Details.StreamDetails.Audio

    '                        filedetailschild = document.CreateElement("audio")
    '                        If item.Language <> Nothing Then
    '                            If item.Language.Value <> "" Then
    '                                filedetailschildchild = document.CreateElement("language")
    '                                filedetailschildchild.InnerText = item.Language.Value
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                            End If
    '                        End If
    '                        If item.Codec <> Nothing Then
    '                            If item.Codec.Value <> "" Then
    '                                filedetailschildchild = document.CreateElement("codec")
    '                                filedetailschildchild.InnerText = item.Codec.Value
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                            End If
    '                        End If
    '                        If item.Channels <> Nothing Then
    '                            If item.Channels.Value <> "" Then
    '                                filedetailschildchild = document.CreateElement("channels")
    '                                filedetailschildchild.InnerText = item.Channels.Value
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                            End If
    '                        End If
    '                        If item.Bitrate <> Nothing Then
    '                            If item.Bitrate.Value <> "" Then
    '                                filedetailschildchild = document.CreateElement("bitrate")
    '                                filedetailschildchild.InnerText = item.Bitrate.Value
    '                                filedetailschild.AppendChild(filedetailschildchild)
    '                                anotherchild.AppendChild(filedetailschild)
    '                            End If
    '                        End If
    '                    Next
    '                    If listofepisodes(0).Details.StreamDetails.Subtitles.Count > 0 Then
    '                        filedetailschild = document.CreateElement("subtitle")
    '                        For Each entry In listofepisodes(0).Details.StreamDetails.Subtitles
    '                            If entry.Language <> Nothing Then
    '                                If entry.Language.Value <> "" Then
    '                                    filedetailschildchild = document.CreateElement("language")
    '                                    filedetailschildchild.InnerText = entry.Language.Value
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


    '        child = document.CreateElement("title")
    '        child.InnerText = listofepisodes(0).Title.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("season")
    '        child.InnerText = listofepisodes(0).Season.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("episode")
    '        child.InnerText = listofepisodes(0).Episode.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("aired")
    '        child.InnerText = listofepisodes(0).Aired.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("plot")
    '        child.InnerText = listofepisodes(0).Plot.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("playcount")
    '        child.InnerText = listofepisodes(0).PlayCount.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("director")
    '        child.InnerText = listofepisodes(0).Director.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("credits")
    '        child.InnerText = listofepisodes(0).Credits.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("rating")
    '        child.InnerText = listofepisodes(0).Rating.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("runtime")
    '        child.InnerText = listofepisodes(0).Runtime.Value
    '        root.AppendChild(child)

    '        child = document.CreateElement("showid")
    '        child.InnerText = FoundShowID
    '        root.AppendChild(child)

    '        Dim actorstosave As Integer = listofepisodes(0).ListActors.Count
    '        If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
    '        For f = 0 To actorstosave - 1
    '            child = document.CreateElement("actor")
    '            actorchild = document.CreateElement("name")
    '            actorchild.InnerText = listofepisodes(0).ListActors(f).actorname
    '            child.AppendChild(actorchild)
    '            actorchild = document.CreateElement("role")
    '            actorchild.InnerText = listofepisodes(0).ListActors(f).actorrole
    '            child.AppendChild(actorchild)
    '            If listofepisodes(0).ListActors(f).actorthumb <> Nothing Then
    '                If listofepisodes(0).ListActors(f).actorthumb <> "" Then
    '                    actorchild = document.CreateElement("thumb")
    '                    actorchild.InnerText = listofepisodes(0).ListActors(f).actorthumb
    '                    child.AppendChild(actorchild)
    '                End If
    '            End If
    '            root.AppendChild(child)
    '        Next
    '        document.AppendChild(root)
    '        Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
    '        output.Formatting = Formatting.Indented

    '        document.WriteTo(output)
    '        output.Close()
    '    Else
    '        Dim document As New XmlDocument
    '        Dim root As XmlElement
    '        Dim xmlEpisode As XmlElement
    '        Dim xmlEpisodechild As XmlElement
    '        Dim xmlStreamDetails As XmlElement
    '        Dim xmlFileInfo As XmlElement
    '        Dim xmlFileInfoType As XmlElement
    '        Dim xmlFileInfoTypechild As XmlElement
    '        Dim xmlActor As XmlElement
    '        Dim xmlActorchild As XmlElement

    '        Dim xmlproc As XmlDeclaration
    '        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
    '        document.AppendChild(xmlproc)

    '        root = document.CreateElement("multiepisodenfo")
    '        Dim done As Boolean = False
    '        For Each ep In listofepisodes

    '            'Hack to get ShowID with the data available at this point
    '            Dim ThumbnailPath As String = ep.Thumbnail.FileName 'this path contains the showID - we just need to pull it out of the string
    '            Dim Split As String() = ThumbnailPath.Split("/")
    '            Dim FoundShowID As String = ""
    '            If Split.Length >= 6 Then
    '                FoundShowID = Split(5) ' ShowID is section 5 from the thumbnail string
    '            End If

    '            'end hack

    '            xmlEpisode = document.CreateElement("episodedetails")
    '            If done = False Then
    '                'done = True
    '                If Preferences.enabletvhdtags = True Then
    '                    Try
    '                        xmlStreamDetails = document.CreateElement("streamdetails")
    '                        xmlFileInfo = document.CreateElement("fileinfo")
    '                        xmlFileInfoType = document.CreateElement("video")
    '                        If ep.Details.StreamDetails.Video.Width <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.Width.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("width")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.Width.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.Height <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.Height.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("height")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.Height.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.Aspect <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.Aspect.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("aspect")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.Aspect.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.Codec <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.Codec.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("codec")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.Codec.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.FormatInfo <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.FormatInfo.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("format")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.FormatInfo.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.DurationInSeconds.Value <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.DurationInSeconds.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("durationinseconds")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.DurationInSeconds.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.Bitrate <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.Bitrate.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("bitrate")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.Bitrate.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.BitrateMode <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.BitrateMode.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("bitratemode")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.BitrateMode.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.BitrateMax <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.BitrateMax.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("bitratemax")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.BitrateMax.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.Container <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.Container.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("container")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.Container.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.CodecId <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.CodecId.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("codecid")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.CodecId.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.CodecInfo <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.CodecInfo.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("codecidinfo")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.CodecInfo.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.Details.StreamDetails.Video.ScanType <> Nothing Then
    '                            If ep.Details.StreamDetails.Video.ScanType.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("scantype")
    '                                xmlFileInfoTypechild.InnerText = ep.Details.StreamDetails.Video.ScanType.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        xmlFileInfo.AppendChild(xmlFileInfoType)
    '                        If ep.Details.StreamDetails.Audio.Count > 0 Then
    '                            For Each aud In ep.Details.StreamDetails.Audio
    '                                xmlFileInfoType = document.CreateElement("audio")
    '                                If aud.Language <> Nothing Then
    '                                    If aud.Language.Value <> "" Then
    '                                        xmlFileInfoTypechild = document.CreateElement("language")
    '                                        xmlFileInfoTypechild.InnerText = aud.Language.Value
    '                                        xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                                    End If
    '                                End If
    '                                If aud.Codec <> Nothing Then
    '                                    If aud.Codec.Value <> "" Then
    '                                        xmlFileInfoTypechild = document.CreateElement("codec")
    '                                        xmlFileInfoTypechild.InnerText = aud.Codec.Value
    '                                        xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                                    End If
    '                                End If
    '                                If aud.Channels <> Nothing Then
    '                                    If aud.Channels.Value <> "" Then
    '                                        xmlFileInfoTypechild = document.CreateElement("channels")
    '                                        xmlFileInfoTypechild.InnerText = aud.Channels.Value
    '                                        xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                                    End If
    '                                End If
    '                                If aud.Bitrate <> Nothing Then
    '                                    If aud.Bitrate.Value <> "" Then
    '                                        xmlFileInfoTypechild = document.CreateElement("bitrate")
    '                                        xmlFileInfoTypechild.InnerText = aud.Bitrate.Value
    '                                        xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                                    End If
    '                                End If
    '                                xmlFileInfo.AppendChild(xmlFileInfoType)
    '                            Next
    '                        End If
    '                        If ep.Details.StreamDetails.Subtitles.Count > 0 Then
    '                            For Each subt In ep.Details.StreamDetails.Subtitles
    '                                If subt.Language <> Nothing Then
    '                                    If subt.Language.Value <> "" Then
    '                                        xmlFileInfoType = document.CreateElement("subtitle")
    '                                        xmlFileInfoTypechild = document.CreateElement("language")
    '                                        xmlFileInfoTypechild.InnerText = subt.Language.Value
    '                                        xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                                    End If
    '                                End If
    '                                xmlFileInfo.AppendChild(xmlFileInfoType)
    '                            Next
    '                        End If
    '                        xmlStreamDetails.AppendChild(xmlFileInfo)
    '                        xmlEpisode.AppendChild(xmlStreamDetails)
    '                    Catch
    '                    End Try
    '                End If
    '            End If


    '            xmlEpisodechild = document.CreateElement("title")
    '            xmlEpisodechild.InnerText = ep.Title.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("season")
    '            xmlEpisodechild.InnerText = ep.Season.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("episode")
    '            xmlEpisodechild.InnerText = ep.Episode.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("playcount")
    '            xmlEpisodechild.InnerText = ep.PlayCount.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("credits")
    '            xmlEpisodechild.InnerText = ep.Credits.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("director")
    '            xmlEpisodechild.InnerText = ep.Director.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("rating")
    '            xmlEpisodechild.InnerText = ep.Rating.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("aired")
    '            xmlEpisodechild.InnerText = ep.Aired.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("plot")
    '            xmlEpisodechild.InnerText = ep.Plot.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            'xmlEpisodechild = document.CreateElement("thumb")
    '            'xmlEpisodechild.InnerText = ep.Thumbnail.Path
    '            'xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("runtime")
    '            xmlEpisodechild.InnerText = ep.Runtime.Value
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            xmlEpisodechild = document.CreateElement("showid")
    '            xmlEpisodechild.InnerText = FoundShowID
    '            xmlEpisode.AppendChild(xmlEpisodechild)

    '            Dim actorstosave As Integer = ep.ListActors.Count
    '            If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
    '            For f = 0 To actorstosave - 1
    '                xmlActor = document.CreateElement("actor")
    '                xmlActorchild = document.CreateElement("name")
    '                xmlActorchild.InnerText = ep.ListActors(f).actorname
    '                xmlActor.AppendChild(xmlActorchild)
    '                xmlActorchild = document.CreateElement("role")
    '                xmlActorchild.InnerText = ep.ListActors(f).actorrole
    '                xmlActor.AppendChild(xmlActorchild)
    '                If ep.ListActors(f).actorthumb <> Nothing Then
    '                    If ep.ListActors(f).actorthumb <> "" Then
    '                        xmlActorchild = document.CreateElement("thumb")
    '                        xmlActorchild.InnerText = ep.ListActors(f).actorthumb
    '                        xmlActor.AppendChild(xmlActorchild)
    '                    End If
    '                End If
    '                xmlEpisode.AppendChild(xmlActor)
    '            Next
    '            root.AppendChild(xmlEpisode)
    '        Next
    '        document.AppendChild(root)
    '        Try
    '            Dim output As New XmlTextWriter(path, System.Text.Encoding.UTF8)
    '            output.Formatting = Formatting.Indented

    '            document.WriteTo(output)
    '            output.Close()
    '        Catch
    '        End Try
    '    End If
    'End Sub
#End Region

    '  All Movie Load/Save Routines
#Region " Movie Routines "    
    Public Function mov_NfoLoadBasic(ByVal path As String, ByVal mode As String) As ComboList

        Dim newmovie As New ComboList

        Try
            If Not IO.File.Exists(path) Then
                newmovie.title = "Error"
                Return newmovie
            End If

            If mode = "movielist" Then
                Dim movie As New XmlDocument
                Try
                    movie.Load(path)
                'Catch
                '    newmovie.title = "Error"
                '    Return newmovie
                'End Try
                Catch ex As Exception
                    If Not util_NfoValidate(path) Then
                        newmovie.title = "Error"
                        Return newmovie
                    End If

                    newmovie.createdate = "999999999999"
                    Dim filecreation2 As New IO.FileInfo(path)
                    Dim myDate2 As Date = filecreation2.LastWriteTime
                    Try
                        newmovie.filedate = Format(myDate2, Preferences.datePattern).ToString
                    Catch
                    End Try
                    newmovie.filename = IO.Path.GetFileName(path)
                    newmovie.foldername = Utilities.GetLastFolder(path)
                    newmovie.fullpathandfilename = path
                    newmovie.genre = "problem / xml error"
                    newmovie.movietag.Clear()
                    newmovie.id = ""
                    newmovie.tmdbid = ""
                    newmovie.missingdata1 = 0
                    newmovie.MovieSet = ""
                    newmovie.source = ""
                    newmovie.director = ""
                    newmovie.originaltitle = newmovie.title
                    newmovie.outline = ""
                    newmovie.playcount = "0"
                    newmovie.lastplayed = ""
                    newmovie.plot = ""
                    newmovie.rating = ""
                    newmovie.runtime = "0"
                    newmovie.sortorder = ""
                    newmovie.title = IO.Path.GetFileName(path)
            '           newmovie.titleandyear = newmovie.title & " (0000)"
                    newmovie.top250 = "0"
                    newmovie.year = "0000"

                    Return newmovie
                End Try

                Dim thisresult As XmlNode = Nothing

                For Each thisresult In movie("movie")
                    Try
                        Select Case thisresult.Name
                            Case "title" : newmovie.title = thisresult.InnerText


                                'Dim tempstring As String = ""
                                'tempstring = thisresult.InnerText
                                ''-------------- Aqui
                                'If Preferences.ignorearticle = True Then
                                '    If tempstring.ToLower.IndexOf("the ") = 0 Then
                                '        tempstring = tempstring.Substring(4, tempstring.Length - 4)
                                '        tempstring = tempstring & ", The"
                                '    End If
                                'End If
                                'newmovie.title = tempstring

                            Case "originaltitle"
                                newmovie.originaltitle = thisresult.InnerText
                            Case "set"
                                If newmovie.MovieSet = "" Then                     'set in nfo's are individual elements - in MC cache they are one string seperated by " / "
                                    newmovie.MovieSet = thisresult.InnerText
                                Else
                                    newmovie.MovieSet = newmovie.MovieSet & " / " & thisresult.InnerText
                                End If
                            Case "source"
                                newmovie.source = thisresult.InnerText
                            Case "diretor"
                                newmovie.director = thisresult.InnerText 
                            Case "year"
                                newmovie.year = thisresult.InnerText.ToInt
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
                            Case "tag"
                                newmovie.movietag.Add(thisresult.InnerText)
                                'If newmovie.tag.add = "" Then                       'tag in nfo's are individual elements - in MC cache they are one string seperated by " / "
                                '    newmovie.tag = thisresult.InnerText
                                'Else
                                '    newmovie.genre = newmovie.tag & " / " & thisresult.InnerText
                                'End If
                            Case "id"
                                If thisresult.Attributes.Count = 0 Then newmovie.id = thisresult.InnerText 'ignore any id nodes with attributes
                            Case "tmdbid"
                                newmovie.tmdbid = thisresult.InnerText 
                            Case "playcount"
                                newmovie.playcount = thisresult.InnerText
                            Case "lastplayed"
                                newmovie.lastplayed = thisresult.InnerText
                            Case "rating"
                                'Dim tempStr As String = thisresult.InnerText
                                'If tempStr.IndexOf("/10") <> -1 Then tempStr.Replace("/10", "")
                                'If tempStr.IndexOf(" "  ) <> -1 Then tempStr.Replace(" "  , "")
                                'newmovie.rating = tempStr.ToRating
                                newmovie.rating = thisresult.InnerText.ToRating
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
                            Case "votes"
                                newmovie.Votes = thisresult.InnerText.ToInt

                            Case "mpaa"    'aka Certificate
                                newmovie.Certificate = thisresult.InnerText

                            Case "fileinfo"

                                Dim gotWidth As Boolean
                                Dim gotHeight As Boolean

                                For Each res In thisresult.ChildNodes
                                    If res.name = "streamdetails" Then

                                        Dim newfilenfo As New FullFileDetails

                                        For Each detail In res.ChildNodes
                                            Select Case detail.Name

                                                Case "video"
                                                    For Each videodetails As XmlNode In detail.ChildNodes
                                                        Select Case videodetails.Name
                                                            Case "width"
                                                                newfilenfo.filedetails_video.Width.Value = videodetails.InnerText
                                                                gotWidth = True
                                                            Case "height"
                                                                newfilenfo.filedetails_video.Height.Value = videodetails.InnerText
                                                                gotHeight = True
                                                            Case "codec"
                                                                newmovie.VideoCodec = videodetails.InnerText
                                                        End Select

                                                        If gotWidth And gotHeight Then
                                                            newmovie.Resolution = newfilenfo.filedetails_video.VideoResolution
                                                            Exit For
                                                        End If
                                                    Next

                                                Case "audio"
                                                        Dim audio As New AudioDetails
                                                        For Each audiodetails As XmlNode In detail.ChildNodes

                                                            Select Case audiodetails.Name
                                                                Case "language"
                                                                    audio.Language.Value = audiodetails.InnerText
                                                                Case "codec"
                                                                    audio.Codec.Value = audiodetails.InnerText
                                                                Case "channels"
                                                                    audio.Channels.Value = audiodetails.InnerText
                                                                Case "bitrate"
                                                                    audio.Bitrate.Value = audiodetails.InnerText
                                                            End Select
                                                        Next
                                                        newmovie.Audio.Add(audio)
                                            End Select
                                        Next

                                        If gotWidth And gotHeight Then
                                            Exit For
                                        End If
                                    End If
                                Next

    '                       Case "Resolution" : newmovie.Resolution = thisresult.InnerText          'Add later?...to replace above...
                        End Select
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Next

                'Now we need to make sure no varibles are still set to NOTHING before returning....
                Dim filecreation As New IO.FileInfo(path)
                Dim myDate As Date = filecreation.LastWriteTime


                If newmovie.title = Nothing Then newmovie.title = "ERR - This Movie Has No TITLE!"
                If newmovie.createdate = "" Or newmovie.createdate = Nothing Then newmovie.createdate = "18000101000000"
                Try
                    newmovie.filedate = Format(myDate, Preferences.datePattern)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                newmovie.filename = IO.Path.GetFileName(path)
                newmovie.foldername = Utilities.GetLastFolder(path)
                newmovie.fullpathandfilename = path
                If newmovie.genre = Nothing Then newmovie.genre = ""
                If newmovie.id = Nothing Then newmovie.id = ""
                If newmovie.tmdbid = Nothing Then newmovie.tmdbid = ""
                If newmovie.missingdata1 = Nothing Then newmovie.missingdata1 = 0
                If newmovie.source = Nothing Then newmovie.source = ""
                If newmovie.director = Nothing Then newmovie.director = ""
                If newmovie.MovieSet = "" Or newmovie.MovieSet = Nothing Then newmovie.MovieSet = "-None-"
                'If newmovie.tag = Nothing Then newmovie.tag = ""
                'if there is no entry for originaltitle, then use the current title. this should only come into use
                'for old movies since new ones will have the originaltitle created when scraped
                If newmovie.originaltitle = "" Or newmovie.originaltitle = Nothing Then newmovie.originaltitle = newmovie.title
                If newmovie.playcount = Nothing Then newmovie.playcount = "0"
                If newmovie.lastplayed = Nothing Then newmovie.lastplayed = ""
                If newmovie.plot = Nothing Then newmovie.plot = ""
                If newmovie.rating = Nothing Then newmovie.rating = 0
                If newmovie.runtime = Nothing Then newmovie.runtime = ""
                If newmovie.sortorder = Nothing Or newmovie.sortorder = "" Then newmovie.sortorder = newmovie.title
                'If newmovie.title <> Nothing And newmovie.year <> Nothing Then
                '    newmovie.titleandyear = newmovie.title & " (" & newmovie.year & ")"
                'Else
                '    newmovie.titleandyear = newmovie.title & "(0000)"
                'End If
                If newmovie.top250 = Nothing Then newmovie.top250 = "0"
                If newmovie.year = Nothing Then newmovie.year = "0001"

                'MsgBox(Format(myDate, "MMddyy"))
                'MsgBox(myDate.ToString("MMddyy"))

            End If
            Return newmovie


        Catch
        End Try

        newmovie.title = "Error"
        Return newmovie

    End Function

    Public Shared Function mov_NfoLoadFull(ByVal path As String) As FullMovieDetails

        ConvertFileToUTF8IfNotAlready(path)

        Try
            Dim newmovie As New FullMovieDetails
            newmovie.fullmoviebody.genre = ""
            Dim newfilenfo As New FullFileDetails
            'Dim audio As New AudioDetails
            'newfilenfo.filedetails_audio.Add(audio)
            newmovie.filedetails = newfilenfo
            Dim thumbstring As String = String.Empty
            If Not IO.File.Exists(path) Then
            Else
                Dim movie As New XmlDocument
                Try
                    movie.Load(path)
                Catch ex As Exception
                    If Not util_NfoValidate(path) Then
                        IO.File.Move(path,path.Replace(".nfo",".info"))
                        newmovie.fullmoviebody.title = "Error"
                        Return newmovie
                    End If
                    Dim errorstring As String
                    errorstring = ex.Message.ToString & vbCrLf & vbCrLf
                    errorstring += ex.StackTrace.ToString
                    newmovie.fullmoviebody.title = "Unknown" 'Utilities.CleanFileName(IO.Path.GetFileName(workingMovie.fullpathandfilename))
                    newmovie.fullmoviebody.year = "0000"
                    newmovie.fullmoviebody.top250 = "0"
                    newmovie.fullmoviebody.credits = ""
                    newmovie.fullmoviebody.director = ""
                    newmovie.fullmoviebody.stars = ""
                    newmovie.fullmoviebody.filename = ""
                    newmovie.fullmoviebody.genre = ""
                    newmovie.fullmoviebody.tag.Clear()    ' = ""
                    newmovie.fullmoviebody.imdbid = ""
                    newmovie.fullmoviebody.tmdbid = ""
                    newmovie.fullmoviebody.mpaa = ""
                    newmovie.fullmoviebody.outline = "This nfo file could not be loaded"
                    newmovie.fullmoviebody.playcount = "0"
                    newmovie.fullmoviebody.lastplayed = ""
                    newmovie.fullmoviebody.plot = errorstring
                    newmovie.fullmoviebody.premiered = ""
                    newmovie.fullmoviebody.rating = ""
                    newmovie.fullmoviebody.runtime = ""
                    newmovie.fullmoviebody.studio = ""
                    newmovie.fullmoviebody.tagline = "Rescraping the movie might fix the problem"
                    newmovie.fullmoviebody.trailer = ""
                    newmovie.fullmoviebody.votes = ""
                    newmovie.fullmoviebody.sortorder = ""
                    newmovie.fullmoviebody.country = ""
                    newmovie.fileinfo.createdate = ""
                    Return newmovie
                End Try
                Dim thisresult As XmlNode = Nothing

                For Each thisresult In movie("movie")
                    Select Case thisresult.Name
                        Case "alternativetitle"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                        Case "set"
                            If newmovie.fullmoviebody.movieset = "" Then
                                newmovie.fullmoviebody.movieset = thisresult.InnerText
                            Else
                                newmovie.fullmoviebody.movieset = newmovie.fullmoviebody.movieset & " / " & thisresult.InnerText
                            End If
                        Case "videosource"
                            newmovie.fullmoviebody.source = thisresult.InnerText
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


                        'Case "thumb"
                        '    If thisresult.InnerText.IndexOf("&lt;thumbs&gt;") <> -1 Then
                        '        thumbstring = thisresult.InnerText
                        '    Else
                        '        newmovie.listthumbs.Add(thisresult.InnerText)
                        '    End If


                        Case "thumb"
                            If thisresult.InnerText.IndexOf("&lt;thumbs&gt;") <> -1 Then
                                thumbstring = thisresult.InnerText
                            Else
                                'Frodo - aspect="poster"
                                If Not IsNothing(thisresult.Attributes.ItemOf("aspect")) Then
                                    newmovie.frodoPosterThumbs.Add(New FrodoPosterThumb(thisresult.Attributes("aspect").Value, thisresult.InnerText))
                                Else
                                    newmovie.listthumbs.Add(thisresult.InnerText)
                                End If
                            End If

                        'Frodo <fanart url="">
                        Case "fanart"
                            newmovie.frodoFanartThumbs.Load(thisresult)

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
                        Case "tag"
                            newmovie.fullmoviebody.tag.Add(thisresult.InnerText)
                            'If newmovie.fullmoviebody.tag = "" Then
                            '    newmovie.fullmoviebody.tag = thisresult.InnerText
                            'Else
                            '    newmovie.fullmoviebody.tag = newmovie.fullmoviebody.tag & " / " & thisresult.InnerText
                            'End If
                        Case "id"
                            newmovie.fullmoviebody.imdbid = thisresult.InnerText
                        Case "tmdbid"
                            newmovie.fullmoviebody.tmdbid = thisresult.InnerText 
                        Case "playcount"
                            newmovie.fullmoviebody.playcount = thisresult.InnerText
                        Case "lastplayed"
                            newmovie.fullmoviebody.lastplayed = thisresult.InnerText
                        Case "rating"
                            Dim y As String = thisresult.InnerText
                            newmovie.fullmoviebody.rating = thisresult.InnerText.ToRating.ToString ' ("0.00", MyCulture)
                            'If newmovie.fullmoviebody.rating.IndexOf("/10") <> -1 Then newmovie.fullmoviebody.rating.Replace("/10", "")
                            'If newmovie.fullmoviebody.rating.IndexOf(" ") <> -1 Then newmovie.fullmoviebody.rating.Replace(" ", "")
                        Case "top250"
                            newmovie.fullmoviebody.top250 = thisresult.InnerText
                        Case "createdate"
                            newmovie.fileinfo.createdate = thisresult.InnerText
                        Case "actor"
                            Dim newactor As New str_MovieActors(SetDefaults)
                            Dim detail As XmlNode = Nothing
                            For Each detail In thisresult.ChildNodes
                                Select Case detail.Name
                                    Case "id"
                                        newactor.actorid = detail.InnerText
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
                                        'Dim newfilenfo As New FullFileDetails
                                        Dim detail As XmlNode = Nothing
                                        For Each detail In res.ChildNodes
                                            Select Case detail.Name
                                                Case "video"
                                                    Dim videodetails As XmlNode = Nothing
                                                    For Each videodetails In detail.ChildNodes
                                                        Select Case videodetails.Name
                                                            Case "width"
                                                                newfilenfo.filedetails_video.Width.Value = videodetails.InnerText
                                                            Case "height"
                                                                newfilenfo.filedetails_video.Height.Value = videodetails.InnerText
                                                            Case "aspect"
                                                                newfilenfo.filedetails_video.Aspect.Value = videodetails.InnerText
                                                            Case "codec"
                                                                newfilenfo.filedetails_video.Codec.Value = videodetails.InnerText
                                                            Case "formatinfo"
                                                                newfilenfo.filedetails_video.FormatInfo.Value = videodetails.InnerText
                                                            Case "durationinseconds"
                                                                newfilenfo.filedetails_video.DurationInSeconds.Value = videodetails.InnerText
                                                            Case "bitrate"
                                                                newfilenfo.filedetails_video.Bitrate.Value = videodetails.InnerText
                                                            Case "bitratemode"
                                                                newfilenfo.filedetails_video.BitrateMode.Value = videodetails.InnerText
                                                            Case "bitratemax"
                                                                newfilenfo.filedetails_video.BitrateMax.Value = videodetails.InnerText
                                                            Case "container"
                                                                newfilenfo.filedetails_video.Container.Value = videodetails.InnerText
                                                            Case "codecid"
                                                                newfilenfo.filedetails_video.CodecId.Value = videodetails.InnerText
                                                            Case "codecidinfo"
                                                                newfilenfo.filedetails_video.CodecInfo.Value = videodetails.InnerText
                                                            Case "scantype"
                                                                newfilenfo.filedetails_video.ScanType.Value = videodetails.InnerText
                                                        End Select
                                                    Next
                                                Case "audio"
                                                    Dim audiodetails As XmlNode = Nothing
                                                    Dim audio As New AudioDetails
                                                    For Each audiodetails In detail.ChildNodes

                                                        Select Case audiodetails.Name
                                                            Case "language"
                                                                audio.Language.Value = audiodetails.InnerText
                                                            Case "codec"
                                                                audio.Codec.Value = audiodetails.InnerText
                                                            Case "channels"
                                                                audio.Channels.Value = audiodetails.InnerText
                                                            Case "bitrate"
                                                                audio.Bitrate.Value = audiodetails.InnerText
                                                        End Select
                                                    Next
                                                    newfilenfo.filedetails_audio.Add(audio)
                                                Case "subtitle"
                                                    Dim subsdetails As XmlNode = Nothing
                                                    For Each subsdetails In detail.ChildNodes
                                                        Select Case subsdetails.Name
                                                            Case "language"
                                                                Dim sublang As New SubtitleDetails
                                                                sublang.Language.Value = subsdetails.InnerText
                                                                newfilenfo.filedetails_subtitles.Add(sublang)
                                                        End Select
                                                    Next
                                            End Select
                                        Next
                                        If newfilenfo.filedetails_audio.Count = 0 Then
                                            Dim audio As New AudioDetails
                                            newfilenfo.filedetails_audio.Add(audio)
                                        End If
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
                If String.IsNullOrEmpty(newmovie.fullmoviebody.imdbid) Then
                    newmovie.fullmoviebody.imdbid = "0"
                End If
                newmovie.fileinfo.fullpathandfilename = path
                newmovie.fileinfo.filename = IO.Path.GetFileName(path)
                newmovie.fileinfo.foldername = Utilities.GetLastFolder(path)
                If IO.Path.GetFileName(path).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(path).ToLower = "index.nfo" Then
                    newmovie.fileinfo.videotspath = Utilities.RootVideoTsFolder(path)
                Else
                    newmovie.fileinfo.videotspath = ""
                End If
                newmovie.fileinfo.posterpath = Preferences.GetPosterPath(path, newmovie.fileinfo.filename)
                newmovie.fileinfo.trailerpath = ""
                newmovie.fileinfo.path = IO.Path.GetDirectoryName(path)
                newmovie.fileinfo.fanartpath = Preferences.GetFanartPath(path, newmovie.fileinfo.filename)

                If newmovie.fullmoviebody.movieset = "" Then
                    newmovie.fullmoviebody.movieset = "-None-"
                End If

                Return newmovie
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        Finally
            'Monitor.Exit(Me)
        End Try
        Return Nothing
    End Function

    Public Shared Sub mov_NfoSave(ByVal filenameandpath As String, ByVal movietosave As FullMovieDetails, Optional ByVal overwrite As Boolean = True)
        'Monitor.Enter(Me)
        Dim stage As Integer = 1
        Try
            If movietosave Is Nothing Then Exit Sub
            If Not IO.File.Exists(filenameandpath) Or overwrite = True Then
                'Try
                Dim doc As New XmlDocument
                'Dim thumbnailstring As String = "" Test code?
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
                        If movietosave.filedetails.filedetails_video.Width.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Width.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("width")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Width.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 4
                    Try
                        If movietosave.filedetails.filedetails_video.Height.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Height.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("height")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Height.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    Try
                        If movietosave.filedetails.filedetails_video.Aspect.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Aspect.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("aspect")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Aspect.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 5
                    Try
                        If movietosave.filedetails.filedetails_video.Codec.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Codec.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codec")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Codec.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 6
                    Try
                        If movietosave.filedetails.filedetails_video.FormatInfo.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.FormatInfo.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("format")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.FormatInfo.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 7
                    Try
                        If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("durationinseconds")
                                    filedetailschildchild.InnerText = If(movietosave.filedetails.filedetails_video.DurationInSeconds.Value = "-1", "", movietosave.filedetails.filedetails_video.DurationInSeconds.Value)
                                    filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 8
                    Try
                        If movietosave.filedetails.filedetails_video.Bitrate.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Bitrate.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitrate")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Bitrate.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 9
                    Try
                        If movietosave.filedetails.filedetails_video.BitrateMode.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.BitrateMode.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemode")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.BitrateMode.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 10
                    Try
                        If movietosave.filedetails.filedetails_video.BitrateMax.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.BitrateMax.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("bitratemax")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.BitrateMax.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 11
                    Try
                        If movietosave.filedetails.filedetails_video.Container.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.Container.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("container")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Container.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 12
                    Try
                        If movietosave.filedetails.filedetails_video.CodecId.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.CodecId.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codecid")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.CodecId.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 13
                    Try
                        If movietosave.filedetails.filedetails_video.CodecInfo.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.CodecInfo.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("codecidinfo")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.CodecInfo.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        End If
                    Catch
                    End Try
                    stage = 14
                    Try
                        If movietosave.filedetails.filedetails_video.ScanType.Value <> Nothing Then
                            If movietosave.filedetails.filedetails_video.ScanType.Value <> "" Then
                                filedetailschildchild = doc.CreateElement("scantype")
                                filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.ScanType.Value
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
                    Try
                        For Each item In movietosave.filedetails.filedetails_audio
                            Try
                                filedetailschild = doc.CreateElement("audio")
                            Catch
                            End Try
                            Try
                                If item.Language.Value <> Nothing Then
                                    If item.Language.Value <> "" Then
                                        filedetailschildchild = doc.CreateElement("language")
                                        filedetailschildchild.InnerText = item.Language.Value
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                End If
                            Catch
                            End Try
                            Try
                                If item.Codec.Value <> Nothing Then
                                    If item.Codec.Value <> "" Then
                                        filedetailschildchild = doc.CreateElement("codec")
                                        filedetailschildchild.InnerText = item.Codec.Value
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                End If
                            Catch
                            End Try
                            Try
                                If item.Channels.Value <> Nothing Then
                                    If item.Channels.Value <> "" Then
                                        filedetailschildchild = doc.CreateElement("channels")
                                        filedetailschildchild.InnerText = item.Channels.Value
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                End If
                            Catch
                            End Try
                            Try
                                If item.Bitrate.Value <> Nothing Then
                                    If item.Bitrate.Value <> "" Then
                                        filedetailschildchild = doc.CreateElement("bitrate")
                                        filedetailschildchild.InnerText = item.Bitrate.Value
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                End If
                            Catch
                            End Try
                            anotherchild.AppendChild(filedetailschild)
                        Next
                    Catch
                    End Try
                    stage = 17
                    Dim tempint As Integer = 0
                    Try
                        filedetailschild = doc.CreateElement("subtitle")
                        For Each entry In movietosave.filedetails.filedetails_subtitles
                            Try
                                If entry.Language <> Nothing Then
                                    If entry.Language.Value <> "" Then
                                        tempint += 1
                                        filedetailschildchild = doc.CreateElement("language")
                                        filedetailschildchild.InnerText = entry.Language.Value
                                        filedetailschild.AppendChild(filedetailschildchild)
                                    End If
                                End If
                            Catch
                            End Try
                        Next
                    Catch
                    End Try
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
                If String.IsNullOrEmpty(movietosave.fullmoviebody.originaltitle) Then
                    child.InnerText = movietosave.fullmoviebody.title
                Else
                    child.InnerText = movietosave.fullmoviebody.originaltitle
                End If

                root.AppendChild(child)

                If Not Preferences.NoAltTitle AndAlso movietosave.alternativetitles.Count > 0 Then
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
                    If movietosave.fullmoviebody.movieset <> "-None-" Then
                        Dim strArr() As String
                        strArr = movietosave.fullmoviebody.movieset.Split("/")
                        For count = 0 To strArr.Length - 1
                            child = doc.CreateElement("set")
                            strArr(count) = strArr(count).Trim
                            child.InnerText = strArr(count)
                            root.AppendChild(child)
                        Next
                    End If
                Catch
                End Try


                Try
                    If String.IsNullOrEmpty(movietosave.fullmoviebody.sortorder) Then
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
                    child.InnerText = movietosave.fullmoviebody.rating.ToRating.ToString("0.0", Form1.MyCulture)
                    root.AppendChild(child)
                Catch
                End Try
                stage = 21
                Try
                    child = doc.CreateElement("votes")
                    Dim votes As String = movietosave.fullmoviebody.votes
                    If Not String.IsNullOrEmpty(votes) then votes = votes.Replace(",", "")
                    child.InnerText = votes   'movietosave.fullmoviebody.votes
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
                    If Preferences.XtraFrodoUrls AndAlso Preferences.FrodoEnabled Then
                        For Each item In movietosave.frodoPosterThumbs

                            child = doc.CreateElement("thumb")

                            child.SetAttribute("aspect", item.Aspect)
                            child.InnerText = item.Url
                            root.AppendChild(child)
                        Next

                        root.AppendChild(movietosave.frodoFanartThumbs.GetChild(doc))

                        For Each thumbnail In movietosave.listthumbs
                            Try
                                child = doc.CreateElement("thumb")
                                child.InnerText = thumbnail
                                root.AppendChild(child)
                            Catch
                            End Try
                        Next
                    End If
                Catch
                End Try
                stage = 23
                'Try                                                    Must be test code
                '    If thumbnailstring <> "" Then
                '        child = doc.CreateElement("thumb")
                '        child.InnerText = thumbnailstring
                '        root.AppendChild(child)
                '    End If
                'Catch
                'End Try
                stage = 24
                Try
                    child = doc.CreateElement("runtime")
                    If movietosave.fullmoviebody.runtime <> Nothing Then
                        Dim minutes As String = movietosave.fullmoviebody.runtime
                        minutes = minutes.Replace("minutes", "")
                        minutes = minutes.Replace("mins", "")
                        minutes = minutes.Replace("min", "")
                        minutes = minutes.Replace(" ", "")
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
                    If movietosave.fullmoviebody.tag.Count <> 0 Then
                        For Each tags In movietosave.fullmoviebody.tag
                            child = doc.CreateElement("tag")
                            child.InnerText = tags
                            root.AppendChild(child)
                        Next
                    End If
                Catch
                End Try
                stage = 28
                Try
                    child = doc.CreateElement("credits")
                    child.InnerText = movietosave.fullmoviebody.credits
                    root.AppendChild(child)
                Catch
                End Try
                stage = 29
                Try
                    child = doc.CreateElement("director")
                    child.InnerText = movietosave.fullmoviebody.director
                    root.AppendChild(child)
                Catch
                End Try
                stage = 30
                Try
                    child = doc.CreateElement("studio")
                    child.InnerText = movietosave.fullmoviebody.studio
                    root.AppendChild(child)
                Catch
                End Try
                stage = 31
                Try
                    If Not String.IsNullOrEmpty(movietosave.fullmoviebody.trailer) Then
                        child = doc.CreateElement("trailer")
                        child.InnerText = movietosave.fullmoviebody.trailer
                        root.AppendChild(child)
                    End If
                Catch
                End Try
                stage = 32
                Try
                    child = doc.CreateElement("playcount")
                    child.InnerText = movietosave.fullmoviebody.playcount
                    root.AppendChild(child)
                Catch
                End Try
                stage = 32
                Try
                    child = doc.CreateElement("lastplayed")
                    child.InnerText = movietosave.fullmoviebody.lastplayed
                    root.AppendChild(child)
                Catch
                End Try
                stage = 33
                Try
                    If Not String.IsNullOrEmpty(movietosave.fullmoviebody.imdbid) Then
                        child = doc.CreateElement("id")
                        child.InnerText = movietosave.fullmoviebody.imdbid
                        root.AppendChild(child)
                    End If
                Catch
                End Try
                Try
                    If Not String.IsNullOrEmpty(movietosave.fullmoviebody.tmdbid) Then
                        child = doc.CreateElement("tmdbid")
                        child.InnerText = movietosave.fullmoviebody.tmdbid
                        root.AppendChild(child)
                    End If
                Catch
                End Try
                Try
                    If Not String.IsNullOrEmpty(movietosave.fullmoviebody.source) Then
                        child = doc.CreateElement("videosource")
                        child.InnerText = movietosave.fullmoviebody.source
                        root.AppendChild(child)
                    End If
                Catch ex As Exception

                End Try
                Try
                    child = doc.CreateElement("createdate")
                    If String.IsNullOrEmpty(movietosave.fileinfo.createdate) Then
                        Dim myDate2 As Date = System.DateTime.Now
                        Try
                            child.InnerText = Format(myDate2, Preferences.datePattern).ToString
                        Catch ex2 As Exception
                        End Try
                    Else
                        child.InnerText = movietosave.fileinfo.createdate
                    End If
                    root.AppendChild(child)
                Catch
                End Try
                stage = 34
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
                        actorchild = doc.CreateElement("id")
                        actorchild.InnerText = movietosave.listactors(f).actorid
                        child.AppendChild(actorchild)
                        actorchild = doc.CreateElement("name")
                        actorchild.InnerText = movietosave.listactors(f).actorname
                        child.AppendChild(actorchild)
                        actorchild = doc.CreateElement("role")
                        actorchild.InnerText = movietosave.listactors(f).actorrole
                        child.AppendChild(actorchild)
                        actorchild = doc.CreateElement("thumb")
                        actorchild.InnerText = movietosave.listactors(f).actorthumb
                        child.AppendChild(actorchild)
                        root.AppendChild(child)
                    Next
                Catch
                End Try
                doc.AppendChild(root)
                stage = 35
                Try
                    Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
                    output.Formatting = Formatting.Indented
                    stage = 36
                    doc.WriteTo(output)
                    output.Close()
                Catch
                End Try
            Else
                MsgBox("File already exists")
            End If

        Catch ex As Exception
            MsgBox("Error Encountered at stage " & stage.ToString & vbCrLf & vbCrLf & ex.ToString)
        Finally
            'Monitor.Exit(Me)
        End Try
    End Sub
#End Region
    
    '  All HomeMovie Load/Save Routines
#Region " Home Movie Routines "
    Public Function nfoLoadHomeMovie(ByVal filenameandpath As String)
        Try
            Dim newmovie As New HomeMovieDetails
            newmovie.fileinfo.fullpathandfilename = filenameandpath
            If Not IO.File.Exists(filenameandpath) Then
                Return "Error"
                Exit Function
            Else

                Dim movie As New XmlDocument

                Try
                    movie.Load(filenameandpath)
                Catch ex As Exception
                    If Not util_NfoValidate(filenameandpath, True) Then
                        newmovie.fullmoviebody.title = "ERROR"
                        Return "ERROR"
                        Exit Function
                    End If



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
                                'If Preferences.ignorearticle = True Then
                                '    If tempstring.ToLower.IndexOf("the ") = 0 Then
                                '        tempstring = tempstring.Substring(4, tempstring.Length - 4)
                                '        tempstring = tempstring & ", The"
                                '    End If
                                'End If
                                'If Preferences.ignoreAarticle Then
                                '    If tempstring.ToLower.IndexOf("a ") = 0 Then
                                '        tempstring = tempstring.Substring(2, tempstring.Length - 2) & ", A"
                                '    End If
                                'End If
                                'If Preferences.ignoreAn Then
                                '    If tempstring.ToLower.IndexOf("an ") = 0 Then
                                '        tempstring = tempstring.Substring(3, tempstring.Length - 3) & ", An"
                                '    End If
                                'End If
                                newmovie.fullmoviebody.title = Preferences.RemoveIgnoredArticles(tempstring)
                            Case "set"
                                newmovie.fullmoviebody.movieset = thisresult.InnerText
                            Case "stars"
                                newmovie.fullmoviebody.stars = thisresult.InnerText
                            Case "year"
                                newmovie.fullmoviebody.year = thisresult.InnerText
                            Case "plot"
                                newmovie.fullmoviebody.plot = thisresult.InnerText
                            Case "playcount"
                                newmovie.fullmoviebody.playcount = thisresult.InnerText
                            Case "sortorder"
                                newmovie.fullmoviebody.sortorder = thisresult.InnerText
                            Case "runtime"
                                newmovie.fullmoviebody.runtime = thisresult.InnerText
                                If IsNumeric(newmovie.fullmoviebody.runtime) Then
                                    newmovie.fullmoviebody.runtime = newmovie.fullmoviebody.runtime & " min"
                                End If
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
                                                                    newfilenfo.filedetails_video.Width.Value = videodetails.InnerText
                                                                Case "height"
                                                                    newfilenfo.filedetails_video.Height.Value = videodetails.InnerText
                                                                Case "aspect"
                                                                    newfilenfo.filedetails_video.Aspect.Value = videodetails.InnerText
                                                                Case "codec"
                                                                    newfilenfo.filedetails_video.Codec.Value = videodetails.InnerText
                                                                Case "formatinfo"
                                                                    newfilenfo.filedetails_video.FormatInfo.Value = videodetails.InnerText
                                                                Case "durationinseconds"
                                                                    newfilenfo.filedetails_video.DurationInSeconds.Value = videodetails.InnerText
                                                                Case "bitrate"
                                                                    newfilenfo.filedetails_video.Bitrate.Value = videodetails.InnerText
                                                                Case "bitratemode"
                                                                    newfilenfo.filedetails_video.BitrateMode.Value = videodetails.InnerText
                                                                Case "bitratemax"
                                                                    newfilenfo.filedetails_video.BitrateMax.Value = videodetails.InnerText
                                                                Case "container"
                                                                    newfilenfo.filedetails_video.Container.Value = videodetails.InnerText
                                                                Case "codecid"
                                                                    newfilenfo.filedetails_video.CodecId.Value = videodetails.InnerText
                                                                Case "codecidinfo"
                                                                    newfilenfo.filedetails_video.CodecInfo.Value = videodetails.InnerText
                                                                Case "scantype"
                                                                    newfilenfo.filedetails_video.ScanType.Value = videodetails.InnerText
                                                            End Select
                                                        Next
                                                    Case "audio"
                                                        Dim audiodetails As XmlNode = Nothing
                                                        Dim audio As New AudioDetails
                                                        For Each audiodetails In detail.ChildNodes

                                                            Select Case audiodetails.Name
                                                                Case "language"
                                                                    audio.Language.Value = audiodetails.InnerText
                                                                Case "codec"
                                                                    audio.Codec.Value = audiodetails.InnerText
                                                                Case "channels"
                                                                    audio.Channels.Value = audiodetails.InnerText
                                                                Case "bitrate"
                                                                    audio.Bitrate.Value = audiodetails.InnerText
                                                            End Select
                                                        Next
                                                        newfilenfo.filedetails_audio.Add(audio)
                                                    Case "subtitle"
                                                        Dim subsdetails As XmlNode = Nothing
                                                        For Each subsdetails In detail.ChildNodes
                                                            Select Case subsdetails.Name
                                                                Case "language"
                                                                    Dim sublang As New SubtitleDetails
                                                                    sublang.Language.Value = subsdetails.InnerText
                                                                    newfilenfo.filedetails_subtitles.Add(sublang)
                                                            End Select
                                                        Next
                                                End Select
                                            Next
                                            newmovie.filedetails = newfilenfo
                                    End Select
                                Next
                        End Select
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Next

                'Now we need to make sure no varibles are still set to NOTHING before returning....

                If newmovie.fullmoviebody.title = Nothing Then newmovie.fullmoviebody.title = "ERR - This Movie Has No TITLE!"
                newmovie.fullmoviebody.filename = IO.Path.GetFileName(filenameandpath)
                If newmovie.fullmoviebody.playcount = Nothing Then newmovie.fullmoviebody.playcount = "0"
                If newmovie.fullmoviebody.plot = Nothing Then newmovie.fullmoviebody.plot = ""
                If newmovie.fullmoviebody.runtime = Nothing Then newmovie.fullmoviebody.runtime = ""
                If newmovie.fullmoviebody.sortorder = Nothing Or newmovie.fullmoviebody.sortorder = "" Then newmovie.fullmoviebody.sortorder = newmovie.fullmoviebody.title

                If newmovie.fullmoviebody.year = Nothing Then newmovie.fullmoviebody.year = "0001"

                'MsgBox(Format(myDate, "yyyy"))
                'MsgBox(myDate.ToString("MMddyy"))


                Return newmovie
            End If

        Catch
        End Try
        Return "Error"

    End Function

    Public Sub nfoSaveHomeMovie(ByVal filenameandpath As String, ByVal homemovietosave As HomeMovieDetails, Optional ByVal overwrite As Boolean = True)

        If homemovietosave Is Nothing Then Exit Sub
        If Not IO.File.Exists(filenameandpath) Or overwrite = True Then
            'Try
            Dim doc As New XmlDocument


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

            If Preferences.enablehdtags = True Then

                child = doc.CreateElement("fileinfo")

                anotherchild = doc.CreateElement("streamdetails")

                filedetailschild = doc.CreateElement("video")

                If homemovietosave.filedetails.filedetails_video.Width.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.Width.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("width")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.Width.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.Height.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.Height.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("height")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.Height.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.Aspect.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.Aspect.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("aspect")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.Aspect.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.Codec.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.Codec.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("codec")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.Codec.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.FormatInfo.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.FormatInfo.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("format")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.FormatInfo.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.DurationInSeconds.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.DurationInSeconds.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("durationinseconds")
                        Dim temptemp As String = homemovietosave.filedetails.filedetails_video.DurationInSeconds.Value
                        If Preferences.intruntime = True Then
                            temptemp = Utilities.cleanruntime(homemovietosave.filedetails.filedetails_video.DurationInSeconds.Value)
                            If IsNumeric(temptemp) Then
                                filedetailschildchild.InnerText = temptemp
                                filedetailschild.AppendChild(filedetailschildchild)
                            Else
                                filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.DurationInSeconds.Value
                                filedetailschild.AppendChild(filedetailschildchild)
                            End If
                        Else
                            filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.DurationInSeconds.Value
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If

                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.Bitrate.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.Bitrate.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("bitrate")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.Bitrate.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.BitrateMode.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.BitrateMode.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("bitratemode")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.BitrateMode.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.BitrateMax.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.BitrateMax.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("bitratemax")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.BitrateMax.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.Container.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.Container.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("container")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.Container.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.CodecId.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.CodecId.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("codecid")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.CodecId.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.CodecInfo.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.CodecInfo.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("codecidinfo")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.CodecInfo.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If

                If homemovietosave.filedetails.filedetails_video.ScanType.Value <> Nothing Then
                    If homemovietosave.filedetails.filedetails_video.ScanType.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("scantype")
                        filedetailschildchild.InnerText = homemovietosave.filedetails.filedetails_video.ScanType.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If



                anotherchild.AppendChild(filedetailschild)




                For Each item In homemovietosave.filedetails.filedetails_audio

                    filedetailschild = doc.CreateElement("audio")

                    If item.Language.Value <> Nothing Then
                        If item.Language.Value <> "" Then
                            filedetailschildchild = doc.CreateElement("language")
                            filedetailschildchild.InnerText = item.Language.Value
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If

                    If item.Codec.Value <> Nothing Then
                        If item.Codec.Value <> "" Then
                            filedetailschildchild = doc.CreateElement("codec")
                            filedetailschildchild.InnerText = item.Codec.Value
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If

                    If item.Channels.Value <> Nothing Then
                        If item.Channels.Value <> "" Then
                            filedetailschildchild = doc.CreateElement("channels")
                            filedetailschildchild.InnerText = item.Channels.Value
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If

                    If item.Bitrate.Value <> Nothing Then
                        If item.Bitrate.Value <> "" Then
                            filedetailschildchild = doc.CreateElement("bitrate")
                            filedetailschildchild.InnerText = item.Bitrate.Value
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If

                    anotherchild.AppendChild(filedetailschild)
                Next


                filedetailschild = doc.CreateElement("subtitle")

                Dim tempint As Integer = 0
                For Each entry In homemovietosave.filedetails.filedetails_subtitles

                    If entry.Language <> Nothing Then
                        If entry.Language.Value <> "" Then
                            tempint += 1
                            filedetailschildchild = doc.CreateElement("language")
                            filedetailschildchild.InnerText = entry.Language.Value
                            filedetailschild.AppendChild(filedetailschildchild)
                        End If
                    End If

                Next


                If tempint > 0 Then
                    anotherchild.AppendChild(filedetailschild)
                End If

                child.AppendChild(anotherchild)

                root.AppendChild(child)

            End If




            child = doc.CreateElement("title")
            child.InnerText = homemovietosave.fullmoviebody.title
            root.AppendChild(child)




            If homemovietosave.fullmoviebody.movieset <> "-None-" Then
                Dim strArr() As String
                strArr = homemovietosave.fullmoviebody.movieset.Split("/")
                For count = 0 To strArr.Length - 1
                    child = doc.CreateElement("set")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If


            If homemovietosave.fullmoviebody.sortorder = Nothing Then
                homemovietosave.fullmoviebody.sortorder = homemovietosave.fullmoviebody.title
            End If
            If homemovietosave.fullmoviebody.sortorder = "" Then
                homemovietosave.fullmoviebody.sortorder = homemovietosave.fullmoviebody.title
            End If
            child = doc.CreateElement("sorttitle")
            child.InnerText = homemovietosave.fullmoviebody.sortorder
            root.AppendChild(child)



            child = doc.CreateElement("year")
            child.InnerText = homemovietosave.fullmoviebody.year
            root.AppendChild(child)



            child = doc.CreateElement("plot")
            child.InnerText = homemovietosave.fullmoviebody.plot
            root.AppendChild(child)

            child = doc.CreateElement("runtime")
            If homemovietosave.fullmoviebody.runtime <> Nothing Then
                Dim minutes As String = homemovietosave.fullmoviebody.runtime
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

                minutes = homemovietosave.fullmoviebody.runtime

                child.InnerText = minutes
            Else
                child.InnerText = homemovietosave.fullmoviebody.runtime
            End If
            root.AppendChild(child)



            child = doc.CreateElement("playcount")
            child.InnerText = homemovietosave.fullmoviebody.playcount
            root.AppendChild(child)

            child = doc.CreateElement("createdate")
            If String.IsNullOrEmpty(homemovietosave.fileinfo.createdate) Then
                Dim myDate2 As Date = System.DateTime.Now
                Try
                    child.InnerText = Format(myDate2, Preferences.datePattern).ToString
                Catch ex2 As Exception
                End Try
            Else
                child.InnerText = homemovietosave.fileinfo.createdate
            End If
            root.AppendChild(child)


            child = doc.CreateElement("stars")
            child.InnerText = homemovietosave.fullmoviebody.stars
            root.AppendChild(child)

            doc.AppendChild(root)

            Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
            output.Close()

        Else
            MsgBox("File already exists")
        End If
    End Sub
#End Region

    '  All Music Video Load/Save Routines
#Region " Music Video Routines "
    Public Shared Sub MVsaveNfo(ByVal movietosave As FullMovieDetails)
        Dim doc As New XmlDocument
        Dim thumbnailstring As String = ""
        Dim thispref As XmlNode = Nothing
        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement = Nothing
        Dim child As XmlElement = Nothing
        Dim filedetailschild As XmlElement = Nothing
        Dim filedetailschildchild As XmlElement = Nothing
        Dim anotherchild As XmlElement = Nothing

        root = doc.CreateElement("musicvideo")
            child = doc.CreateElement("fileinfo")
                anotherchild = doc.CreateElement("streamdetails")
                    filedetailschild = doc.CreateElement("video")
        Try
            If movietosave.filedetails.filedetails_video.width <> Nothing Then
                If movietosave.filedetails.filedetails_video.width.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("width")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.width.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.height <> Nothing Then
                If movietosave.filedetails.filedetails_video.height.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("height")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.height.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try
        Try
            If movietosave.filedetails.filedetails_video.aspect <> Nothing Then
                If movietosave.filedetails.filedetails_video.aspect.value <> "" Then
                    filedetailschildchild = doc.CreateElement("aspect")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.Aspect.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.codec <> Nothing Then
                If movietosave.filedetails.filedetails_video.codec.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("codec")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.codec.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.DurationInSeconds  <> Nothing Then
                If movietosave.filedetails.filedetails_video.DurationInSeconds.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("durationinseconds")
                    Dim temptemp As String = movietosave.filedetails.filedetails_video.DurationInSeconds.Value
                    If IsNumeric(temptemp) Then
                        filedetailschildchild.InnerText = temptemp
                        filedetailschild.AppendChild(filedetailschildchild)
                    Else
                        filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.DurationInSeconds.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.bitrate <> Nothing Then
                If movietosave.filedetails.filedetails_video.bitrate.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("bitrate")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitrate.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.bitratemax <> Nothing Then
                If movietosave.filedetails.filedetails_video.bitratemax.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("bitratemax")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.bitratemax.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.container <> Nothing Then
                If movietosave.filedetails.filedetails_video.container.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("container")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.container.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try

        Try
            If movietosave.filedetails.filedetails_video.scantype <> Nothing Then
                If movietosave.filedetails.filedetails_video.scantype.Value <> "" Then
                    filedetailschildchild = doc.CreateElement("scantype")
                    filedetailschildchild.InnerText = movietosave.filedetails.filedetails_video.scantype.Value
                    filedetailschild.AppendChild(filedetailschildchild)
                End If
            End If
        Catch
        End Try
        anotherchild.AppendChild(filedetailschild)

        For Each item In movietosave.filedetails.filedetails_audio
            filedetailschild = doc.CreateElement("audio")
            
            Try
                If item.codec.Value <> Nothing Then
                    If item.codec.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("codec")
                        filedetailschildchild.InnerText = item.codec.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If
            Catch
            End Try
            Try
                If item.channels <> Nothing Then
                    If item.channels.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("channels")
                        filedetailschildchild.InnerText = item.channels.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If
            Catch
            End Try
            Try
                If item.bitrate <> Nothing Then
                    If item.bitrate.Value <> "" Then
                        filedetailschildchild = doc.CreateElement("bitrate")
                        filedetailschildchild.InnerText = item.bitrate.Value
                        filedetailschild.AppendChild(filedetailschildchild)
                    End If
                End If
            Catch
            End Try
            anotherchild.AppendChild(filedetailschild)
        Next

        child.AppendChild(anotherchild)
        root.AppendChild(child)

        child = doc.CreateElement("title")
        child.InnerText = movietosave.fullmoviebody.title
        root.AppendChild(child)

        child = doc.CreateElement("year")
        child.InnerText = movietosave.fullmoviebody.year
        root.AppendChild(child)

        child = doc.CreateElement("artist")
        child.InnerText = movietosave.fullmoviebody.artist
        root.AppendChild(child)

        child = doc.CreateElement("director")
        child.InnerText = movietosave.fullmoviebody.director
        root.AppendChild(child)

        child = doc.CreateElement("album")
        child.InnerText = movietosave.fullmoviebody.album
        root.AppendChild(child)

        child = doc.CreateElement("genre")
        child.InnerText = movietosave.fullmoviebody.genre
        root.AppendChild(child)

        If movietosave.fullmoviebody.runtime = "Unknown" Then
            Try
                Dim seconds As Integer = Convert.ToInt32(movietosave.filedetails.filedetails_video.DurationInSeconds.Value)
                Dim hms = TimeSpan.FromSeconds(seconds)
                Dim h = hms.Hours.ToString
                Dim m = hms.Minutes.ToString
                Dim s = hms.Seconds.ToString

                If s.Length = 1 Then s = "0" & s

                Dim runtime As String
                runtime = h & ":" & m & ":" & s
                If h = "0" Then
                    runtime = m & ":" & s
                End If
                If h = "0" And m = "0" Then
                    runtime = s
                End If
                movietosave.fullmoviebody.runtime = runtime
            Catch
            End Try
        End If

        child = doc.CreateElement("runtime")
        child.InnerText = movietosave.fullmoviebody.runtime
        root.AppendChild(child)

        child = doc.CreateElement("plot")
        child.InnerText = movietosave.fullmoviebody.plot
        root.AppendChild(child)

        child = doc.CreateElement("studio")
        child.InnerText = movietosave.fullmoviebody.studio
        root.AppendChild(child)

        child = doc.CreateElement("createdate")
        If String.IsNullOrEmpty(movietosave.fileinfo.createdate) Then
            Dim myDate2 As Date = System.DateTime.Now
            Try
                child.InnerText = Format(myDate2, Preferences.datePattern).ToString
            Catch ex2 As Exception
            End Try
        Else
            child.InnerText = movietosave.fileinfo.createdate
        End If
        root.AppendChild(child)
       
        doc.AppendChild(root)

        Dim nfopath As String = movietosave.fileinfo.fullPathAndFilename
        nfopath = nfopath.Replace(IO.Path.GetExtension(nfopath), ".nfo")

        Try
            Dim output As New XmlTextWriter(nfopath, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
            output.Close()
        Catch
        End Try
    End Sub

    Public Shared Function MVloadNfo(ByVal filePath)
        Dim NewMusicVideo As New FullMovieDetails 
        NewMusicVideo.fileinfo.fullPathAndFilename = filePath
        Dim document As New XmlDocument
        document.Load(filePath)
        Dim thisresult As XmlNode = Nothing
        Dim newfilenfo As New FullFileDetails
        For Each thisresult In document("musicvideo")
            Select Case thisresult.Name
                Case "album" : NewMusicVideo.fullmoviebody.album = (thisresult.InnerText)
                Case "title" : NewMusicVideo.fullmoviebody.title = (thisresult.InnerText)
                Case "year" : NewMusicVideo.fullmoviebody.year = (thisresult.InnerText)
                Case "artist" : NewMusicVideo.fullmoviebody.artist = (thisresult.InnerText)
                Case "director" : NewMusicVideo.fullmoviebody.director = (thisresult.InnerText)
                Case "genre" : NewMusicVideo.fullmoviebody.genre = (thisresult.InnerText)
                Case "runtime" : NewMusicVideo.fullmoviebody.runtime = (thisresult.InnerText)
                Case "plot" : NewMusicVideo.fullmoviebody.plot = (thisresult.InnerText)
                Case "studio" : NewMusicVideo.fullmoviebody.studio = (thisresult.InnerText)
                Case "createdate" : NewMusicVideo.fileinfo.createdate = thisresult.InnerText
                Case "fileinfo"
                    Dim what As XmlNode = Nothing
                    For Each res In thisresult.ChildNodes
                        Select Case res.name
                            Case "streamdetails"
                                Dim detail As XmlNode = Nothing
                                For Each detail In res.ChildNodes
                                    Select Case detail.Name
                                        Case "video"
                                            Dim videodetails As XmlNode = Nothing
                                            For Each videodetails In detail.ChildNodes
                                                Select Case videodetails.Name
                                                    Case "width"
                                                        newfilenfo.filedetails_video.Width.Value = videodetails.InnerText
                                                    Case "height"
                                                        newfilenfo.filedetails_video.Height.Value = videodetails.InnerText
                                                    Case "aspect"
                                                        newfilenfo.filedetails_video.Aspect.Value = videodetails.InnerText
                                                    Case "codec"
                                                        newfilenfo.filedetails_video.Codec.Value = videodetails.InnerText
                                                    Case "formatinfo"
                                                        newfilenfo.filedetails_video.FormatInfo.Value = videodetails.InnerText
                                                    Case "durationinseconds"
                                                        newfilenfo.filedetails_video.DurationInSeconds.Value = videodetails.InnerText
                                                    Case "bitrate"
                                                        newfilenfo.filedetails_video.Bitrate.Value = videodetails.InnerText
                                                    Case "bitratemode"
                                                        newfilenfo.filedetails_video.BitrateMode.Value = videodetails.InnerText
                                                    Case "bitratemax"
                                                        newfilenfo.filedetails_video.BitrateMax.Value = videodetails.InnerText
                                                    Case "container"
                                                        newfilenfo.filedetails_video.Container.Value = videodetails.InnerText
                                                    Case "codecid"
                                                        newfilenfo.filedetails_video.CodecId.Value = videodetails.InnerText
                                                    Case "codecidinfo"
                                                        newfilenfo.filedetails_video.CodecInfo.Value = videodetails.InnerText
                                                    Case "scantype"
                                                        newfilenfo.filedetails_video.ScanType.Value = videodetails.InnerText
                                                End Select
                                            Next
                                        Case "audio"
                                            Dim audiodetails As XmlNode = Nothing
                                            Dim audio As New AudioDetails
                                            For Each audiodetails In detail.ChildNodes

                                                Select Case audiodetails.Name
                                                    Case "language"
                                                        audio.Language.Value = audiodetails.InnerText
                                                    Case "codec"
                                                        audio.Codec.Value = audiodetails.InnerText
                                                    Case "channels"
                                                        audio.Channels.Value = audiodetails.InnerText
                                                    Case "bitrate"
                                                        audio.Bitrate.Value = audiodetails.InnerText
                                                End Select
                                            Next
                                            newfilenfo.filedetails_audio.Add(audio)
                                        Case "subtitle"
                                            Dim subsdetails As XmlNode = Nothing
                                            For Each subsdetails In detail.ChildNodes
                                                Select Case subsdetails.Name
                                                    Case "language"
                                                        Dim sublang As New SubtitleDetails
                                                        sublang.Language.Value = subsdetails.InnerText
                                                        newfilenfo.filedetails_subtitles.Add(sublang)
                                                End Select
                                            Next
                                    End Select
                                Next
                                If newfilenfo.filedetails_audio.Count = 0 Then
                                    Dim audio As New AudioDetails
                                    newfilenfo.filedetails_audio.Add(audio)
                                End If
                                NewMusicVideo.filedetails = newfilenfo
                        End Select
                    Next
            End Select
        Next
        Return NewMusicVideo
    End Function
#End Region

End Class



