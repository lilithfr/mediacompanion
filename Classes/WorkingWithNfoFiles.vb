Imports System.Xml
'Imports System.IO
Imports Alphaleonis.Win32.Filesystem
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports Media_Companion
Imports System.Text
Imports System.Linq

Public Class WorkingWithNfoFiles
    Const SetDefaults = True
    'Public Shared MyCulture As New System.Globalization.CultureInfo("en-US")

    Public Shared Function util_NfoValidate(ByVal nfopath As String, Optional ByVal homemovie As Boolean = False)
        Dim tempstring As String
        Using filechck As IO.StreamReader = File.OpenText(nfopath)
            tempstring = filechck.ReadToEnd.ToLower
        End Using
        If tempstring = Nothing Then
            Return False
        End If
        If tempstring.IndexOf("<movie") <> -1 And tempstring.IndexOf("</movie>") <> -1 And tempstring.IndexOf("<title>") <> -1 And tempstring.IndexOf("</title>") <> -1 Then
            Return True
            Exit Function
        End If
        Return False
    End Function

    Public Shared Sub ConvertFileToUTF8IfNotAlready(FileName As String)
        If Not File.Exists(FileName) Then Exit Sub
        Dim _Detected As Encoding
        Dim s As String = ""
        Using r As IO.StreamReader = File.Opentext(FileName)
            s = r.ReadToEnd
            _Detected = r.CurrentEncoding
        End Using

        If _Detected.ToString <> "System.Text.UTF8Encoding" AndAlso _Detected.ToString <> "System.Text.SBCSCodePageEncoding" Then
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

    Public Shared Function SaveXMLDoc(ByVal doc As XmlDocument, ByVal Filename As String) As Boolean
        Dim aok As Boolean = False
        Try
            ' Get around Long Path for XMLTextWriter by saving to cache first
            ' then moving to final destination.
            Dim tmpxml As String = Utilities.CacheFolderPath & "tmpxml.nfo"
            If File.Exists(tmpxml) Then
                File.Delete(tmpxml, True)
            End If
            Dim output As New XmlTextWriter(tmpxml, System.Text.Encoding.UTF8)

            output.Formatting = Formatting.Indented
            output.Indentation = 4
            doc.WriteTo(output)
            output.Close()

            aok = True
            If aok Then
                If File.Exists(Filename) Then File.Delete(Filename)
                File.Move(tmpxml, Filename)
            End If
        Catch
        End Try
        Return aok
    End Function

    Public Shared Function AppendVideo(ByRef doc As XmlDocument, ByVal Video As VideoDetails) As XmlElement
        Dim VideoElements As XmlElement = doc.CreateElement("video")
        If Not String.IsNullOrEmpty(Video.Width.Value)             Then VideoElements.AppendChild(doc, "width"               , Video.width.Value)
        If Not String.IsNullOrEmpty(Video.Height.Value)            Then VideoElements.AppendChild(doc, "height"              , Video.height.Value)
        If Not String.IsNullOrEmpty(Video.Aspect.Value)            Then VideoElements.AppendChild(doc, "aspect"              , Video.aspect.Value)
        If Not String.IsNullOrEmpty(Video.Codec.Value)             Then VideoElements.AppendChild(doc, "codec"               , Video.codec.Value)
        If Not String.IsNullOrEmpty(Video.FormatInfo.Value)        Then VideoElements.AppendChild(doc, "format"              , Video.formatinfo.Value)
        If Not String.IsNullOrEmpty(Video.DurationInSeconds.Value) Then VideoElements.AppendChild(doc, "durationinseconds"   , Video.DurationInSeconds.Value)
        If Not String.IsNullOrEmpty(Video.Bitrate.Value)           Then VideoElements.AppendChild(doc, "bitrate"             , Video.bitrate.Value)
        If Not String.IsNullOrEmpty(Video.BitrateMode.Value)       Then VideoElements.AppendChild(doc, "bitratemode"         , Video.bitratemode.Value)
        If Not String.IsNullOrEmpty(Video.BitrateMax.Value)        Then VideoElements.AppendChild(doc, "bitratemax"          , Video.bitratemax.Value)
        If Not String.IsNullOrEmpty(Video.Container.Value)         Then VideoElements.AppendChild(doc, "container"           , Video.container.Value)
        If Not String.IsNullOrEmpty(Video.CodecId.Value)           Then VideoElements.AppendChild(doc, "codecid"             , Video.codecid.Value)
        If Not String.IsNullOrEmpty(Video.CodecInfo.Value)         Then VideoElements.AppendChild(doc, "codecidinfo"         , Video.codecinfo.Value)
        If Not String.IsNullOrEmpty(Video.ScanType.Value)          Then VideoElements.AppendChild(doc, "scantype"            , Video.scantype.Value)
        Return VideoElements
    End Function

    Public Shared Function AppendAudio(ByRef doc As XmlDocument, ByVal Audio As AudioDetails) As XmlElement
        Dim AudioElement As XmlElement = doc.CreateElement("audio")
        If Not String.IsNullOrEmpty(Audio.Language.Value)     Then AudioElement.AppendChild(doc, "language"       , Audio.Language.Value)
        If Not String.IsNullOrEmpty(Audio.DefaultTrack.Value) Then AudioElement.AppendChild(doc, "DefaultTrack"   , Audio.DefaultTrack.Value)
        If Not String.IsNullOrEmpty(Audio.Codec.Value)        Then AudioElement.AppendChild(doc, "codec"          , Audio.Codec.Value)
        If Not String.IsNullOrEmpty(Audio.Channels.Value)     Then AudioElement.AppendChild(doc, "channels"       , Audio.Channels.Value)
        If Not String.IsNullOrEmpty(Audio.Bitrate.Value)      Then AudioElement.AppendChild(doc, "bitrate"        , Audio.Bitrate.Value)
        Return AudioElement
    End Function

    Public Shared Function AppendSub(ByRef doc As XmlDocument, ByVal SubTitle As SubtitleDetails) As XmlElement
        Dim SubElement As XmlElement = doc.CreateElement("subtitle")
        SubElement.AppendChild(doc, "language"      , SubTitle.language.Value)
        SubElement.AppendChild(doc, "default"       , SubTitle.Primary)
        SubElement.AppendChild(doc, "forced"        , SubTitle.Forced)
        Return SubElement
    End Function

    Public Shared Function SetCreateDate(ByVal videodate As String) As String
        If String.IsNullOrEmpty(videodate) Then
            Try
                Dim myDate2 As Date = System.DateTime.Now
                Return Format(myDate2, Pref.datePattern).ToString
            Catch
            End Try
        End If
        Return videodate
    End Function


    '  All Tv Load/Save Routines
#Region " Tv Routines "
    
    Public Shared Function ep_NfoLoad(ByVal loadpath As String)
        Dim episodelist As New List(Of TvEpisode)
        Dim fixmulti As Boolean = False
        Dim newtvshow As New TvEpisode
        If Not File.Exists(loadpath) Then
            Return episodelist '"Error"
        Else
            Dim tvshow As New XmlDocument
            Try
                Using tmpstrm As IO.StreamReader = File.OpenText(loadpath)
                    tvshow.Load(tmpstrm)
                End Using
            Catch ex As Exception
                Try
                newtvshow.Title.Value = Path.GetFileName(loadpath)
                Catch
                    newtvshow.Title.Value = loadpath
                End Try
                newtvshow.ImdbId.Value = "xml error"
                newtvshow.NfoFilePath = loadpath
                newtvshow.TvdbId.Value = ""

                If newtvshow.Episode.Value = Nothing Or newtvshow.Episode.Value = Nothing Then
                    For Each regexp In Pref.tv_RegexScraper

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
                        newtvepisode.NfoFilePath = loadpath
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
                                Dim rating As String = ""
                                rating = thisresult.InnerText  'newtvepisode.Rating.Value = thisresult.InnerText
                                If rating.IndexOf("/10") <> -1  Then rating.Replace("/10", "")
                                If rating.IndexOf(" ") <> -1    Then rating.Replace(" ", "")
                                If rating.IndexOf(".") <> -1 OrElse rating.IndexOf(",") <> -1Then
                                    rating = rating.Substring(0,3)
                                End If
                                newtvepisode.Rating.Value = rating
                            Case "votes"
                                newtvepisode.Votes.Value = thisresult.InnerText
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
                            Case "displayseason"
                                newtvepisode.DisplaySeason.Value = thisresult.InnerText
                            Case "displayepisode"
                                newtvepisode.DisplayEpisode.Value = thisresult.InnerText 
                            Case "videosource"
                                newtvepisode.Source.Value = thisresult.InnerText 
                            Case "showid"
                                newtvepisode.ShowId.Value = thisresult.InnerText 
                            Case "uniqueid"
                                newtvepisode.UniqueId.Value = thisresult.InnerText
                            Case "epbookmark"
                                newtvepisode.EpBookmark.Value = thisresult.InnerText
                            Case "runtime"
                                newtvepisode.Runtime.Value = thisresult.InnerText
                            Case "actor"
                                Dim actordetail As XmlNode = Nothing
                                Dim newactor As New str_MovieActors(SetDefaults)
                                For Each actordetail In thisresult.ChildNodes
                                    Select Case actordetail.Name
                                        Case "name"
                                            newactor.actorname = actordetail.InnerText
                                        Case "role"
                                            newactor.actorrole = actordetail.InnerText
                                        Case "order"
                                            newactor.order = actordetail.InnerText 
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
                                            Dim detail As XmlNode = Nothing
                                            For Each detail In detail2.ChildNodes
                                                Select Case detail.Name
                                                    Case "video"
                                                        Dim videodetails As XmlNode = Nothing
                                                        For Each videodetails In detail.ChildNodes
                                                            Select Case videodetails.Name
                                                                Case "width"
                                                                    newtvepisode.StreamDetails.Video.Width.Value = videodetails.InnerText
                                                                Case "height"
                                                                    newtvepisode.StreamDetails.Video.Height.Value = videodetails.InnerText
                                                                Case "aspect"
                                                                    newtvepisode.StreamDetails.Video.Aspect.Value = Utilities.FixIntlAspectRatio(videodetails.InnerText)
                                                                Case "codec"
                                                                    newtvepisode.StreamDetails.Video.Codec.Value = videodetails.InnerText
                                                                Case "format"
                                                                    If newtvepisode.StreamDetails.Video.FormatInfo.Value = "" Then
                                                                        newtvepisode.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
                                                                    End If
                                                                Case "formatinfo"
                                                                    newtvepisode.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
                                                                Case "durationinseconds"
                                                                    newtvepisode.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
                                                                Case "bitrate"
                                                                    newtvepisode.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
                                                                Case "bitratemode"
                                                                    newtvepisode.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
                                                                Case "bitratemax"
                                                                    newtvepisode.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
                                                                Case "container"
                                                                    newtvepisode.StreamDetails.Video.Container.Value = videodetails.InnerText
                                                                Case "codecid"
                                                                    newtvepisode.StreamDetails.Video.CodecId.Value = videodetails.InnerText
                                                                Case "codecidinfo"
                                                                    newtvepisode.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
                                                                Case "scantype"
                                                                    newtvepisode.StreamDetails.Video.ScanType.Value = videodetails.InnerText
                                                            End Select
                                                        Next
                                                    Case "audio"
                                                        Dim audiodetails As XmlNode = Nothing
                                                        Dim audio As New AudioDetails
                                                        For Each audiodetails In detail.ChildNodes
                                                            Select Case audiodetails.Name
                                                                Case "language"
                                                                    audio.Language.Value = audiodetails.InnerText
                                                                Case "DefaultTrack"
                                                                    audio.DefaultTrack.Value = audiodetails.InnerText
                                                                Case "codec"
                                                                    audio.Codec.Value = audiodetails.InnerText
                                                                Case "channels"
                                                                    audio.Channels.Value = audiodetails.InnerText
                                                                Case "bitrate"
                                                                    audio.Bitrate.Value = audiodetails.InnerText
                                                            End Select
                                                        Next
                                                        newtvepisode.StreamDetails.Audio.Add(audio)
                                                    Case "subtitle"
                                                        Dim subsdetails As XmlNode = Nothing
                                                        Dim sublang As New SubtitleDetails
                                                        For Each subsdetails In detail.ChildNodes
                                                            Select Case subsdetails.Name
                                                                Case "language"
                                                                    sublang.Language.Value = subsdetails.InnerText
                                                                Case "default"
                                                                    sublang.Primary = subsdetails.InnerXml 
                                                            End Select
                                                        Next
                                                        newtvepisode.StreamDetails.Subtitles.Add(sublang)
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
                    For Each regexp In Pref.tv_RegexScraper

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
                If newtvepisode.Votes = Nothing Then newtvepisode.Votes.Value = ""
                If String.IsNullOrEmpty(newtvepisode.PlayCount.Value) Then newtvepisode.PlayCount.Value = "0"
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
                                            Dim rating As String = ""
                                            rating = thisresult.ChildNodes(f).InnerText
                                            If rating.IndexOf("/10") <> -1  Then rating.Replace("/10", "")
                                            If rating.IndexOf(" ") <> -1    Then rating.Replace(" ", "")
                                            If rating.IndexOf(".") <> -1 OrElse rating.IndexOf(",") <> -1 Then
                                                rating = rating.Substring(0,3)
                                            End If
                                            anotherepisode.Rating.Value = rating
                                        Case "votes"
                                            anotherepisode.Votes.Value = thisresult.ChildNodes(f).InnerText
                                        Case "playcount"
                                            anotherepisode.PlayCount.Value = thisresult.ChildNodes(f).InnerText
                                        Case "plot"
                                            anotherepisode.Plot.Value = thisresult.ChildNodes(f).InnerText
                                        Case "director"
                                            anotherepisode.Director.Value = thisresult.ChildNodes(f).InnerText
                                        Case "credits"
                                            anotherepisode.Credits.Value = thisresult.ChildNodes(f).InnerText
                                        Case "displayseason"
                                            anotherepisode.DisplaySeason.Value = thisresult.ChildNodes(f).InnerText
                                        Case "displayepisode"
                                            anotherepisode.DisplayEpisode.Value = thisresult.ChildNodes(f).InnerText
                                        Case "aired"
                                            anotherepisode.Aired.Value = thisresult.ChildNodes(f).InnerText
                                        Case "videosource"
                                            anotherepisode.Source.Value = thisresult.ChildNodes(f).InnerText 
                                        Case "showid"
                                            anotherepisode.ShowId.Value = thisresult.ChildNodes(f).InnerText
                                        Case "uniqueid"
                                            anotherepisode.UniqueId.Value = thisresult.ChildNodes(f).InnerText
                                        Case "epbookmark"
                                            anotherepisode.EpBookmark.Value = thisresult.ChildNodes(f).InnerText
                                        Case "actor"
                                            Dim actordetail As XmlNode = Nothing
                                            Dim newactor As New str_MovieActors(SetDefaults)
                                            For Each actordetail In thisresult.ChildNodes(f)
                                                Select Case actordetail.Name
                                                    Case "name"
                                                        newactor.actorname = actordetail.InnerText
                                                    Case "role"
                                                        newactor.actorrole = actordetail.InnerText
                                                    Case "thumb"
                                                        newactor.actorthumb = actordetail.InnerText
                                                    Case "order"
                                                        newactor.order = actordetail.Innertext
                                                End Select
                                            Next
                                            anotherepisode.ListActors.Add(newactor)
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
                                                                                    anotherepisode.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
                                                                                Else
                                                                                    anotherepisode.StreamDetails.Video.Width.Value = videodetails.InnerText
                                                                                End If
                                                                            Case "height"
                                                                                anotherepisode.StreamDetails.Video.Height.Value = videodetails.InnerText
                                                                            Case "aspect"
                                                                                anotherepisode.StreamDetails.Video.Aspect.Value = Utilities.FixIntlAspectRatio(videodetails.InnerText)
                                                                            Case "codec"
                                                                                anotherepisode.StreamDetails.Video.Codec.Value = videodetails.InnerText
                                                                            Case "format"
                                                                                If anotherepisode.StreamDetails.Video.FormatInfo.Value = "" Then
                                                                                    anotherepisode.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
                                                                                End If
                                                                            Case "formatinfo"
                                                                                anotherepisode.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
                                                                            Case "durationinseconds"
                                                                                anotherepisode.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
                                                                            Case "bitrate"
                                                                                anotherepisode.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
                                                                            Case "bitratemode"
                                                                                anotherepisode.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
                                                                            Case "bitratemax"
                                                                                anotherepisode.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
                                                                            Case "container"
                                                                                anotherepisode.StreamDetails.Video.Container.Value = videodetails.InnerText
                                                                            Case "codecid"
                                                                                anotherepisode.StreamDetails.Video.CodecId.Value = videodetails.InnerText
                                                                            Case "codecidinfo"
                                                                                anotherepisode.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
                                                                            Case "scantype"
                                                                                anotherepisode.StreamDetails.Video.ScanType.Value = videodetails.InnerText
                                                                        End Select
                                                                    Next
                                                                Case "audio"
                                                                    Dim audiodetails As XmlNode = Nothing
                                                                    Dim audio2 As New AudioDetails
                                                                    For Each audiodetails In detail.ChildNodes
                                                                        Select Case audiodetails.Name
                                                                            Case "language"
                                                                                audio2.Language.Value = audiodetails.InnerText
                                                                            Case "DefaultTrack"
                                                                                audio2.DefaultTrack.Value = audiodetails.InnerText
                                                                            Case "codec"
                                                                                audio2.Codec.Value = audiodetails.InnerText
                                                                            Case "channels"
                                                                                audio2.Channels.Value = audiodetails.InnerText
                                                                            Case "bitrate"
                                                                                audio2.Bitrate.Value = audiodetails.InnerText
                                                                        End Select
                                                                    Next
                                                                    anotherepisode.StreamDetails.Audio.Add(audio2)
                                                                Case "subtitle"
                                                                    Dim subsdetails As XmlNode = Nothing
                                                                    For Each subsdetails In detail.ChildNodes
                                                                        Select Case subsdetails.Name
                                                                            Case "language"
                                                                                Dim sublang As New SubtitleDetails
                                                                                sublang.Language.Value = subsdetails.InnerText
                                                                                anotherepisode.StreamDetails.Subtitles.Add(sublang)
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
                                anotherepisode.NfoFilePath = loadpath
                                If anotherepisode.Episode.Value = Nothing Or anotherepisode.Episode.Value = Nothing Then
                                    For Each regexp In Pref.tv_RegexScraper

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
                                If anotherepisode.Votes = Nothing Then anotherepisode.Votes.Value = ""
                                If String.IsNullOrEmpty(anotherepisode.PlayCount.Value) Then anotherepisode.PlayCount.Value = "0"
                                episodelist.Add(anotherepisode)
                            Catch ex As Exception
                                MsgBox(ex.ToString)
                            End Try
                    End Select
                Next
                If fixmulti Then
                    ep_NfoSave(episodelist, loadpath)
                End If
            End If

            Return episodelist
        End If
    End Function

    Public Shared Sub ep_NfoSave(ByVal listofepisodes As List(Of TvEpisode), ByVal path As String)
        Dim doc As New XmlDocument
        Dim root As XmlElement
        Dim xmlEpisode As XmlElement
        Dim xmlStreamDetails As XmlElement
        Dim xmlFileInfo As XmlElement
        Dim xmlActor As XmlElement

        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)

        root = doc.CreateElement("multiepisodenfo")    'root starts out as multiepisode, but tested for single episode after initial population.
        For Each ep In listofepisodes

            xmlEpisode = doc.CreateElement("episodedetails")
            If Pref.enabletvhdtags = True Then
                xmlFileInfo = doc.CreateElement("fileinfo")
                xmlStreamDetails = doc.CreateElement("streamdetails")
                xmlStreamDetails.AppendChild(AppendVideo(doc, ep.streamdetails.Video))

                If ep.StreamDetails.Audio.Count > 0 Then
                    For Each aud In ep.StreamDetails.Audio
                        xmlStreamDetails.AppendChild(AppendAudio(doc, aud))
                    Next
                End If

                If ep.StreamDetails.Subtitles.Count > 0 Then
                    For Each subt In ep.StreamDetails.Subtitles
                        If Not String.IsNullOrEmpty(subt.Language.Value) Then
                            xmlStreamDetails.AppendChild(AppendSub(doc, subt))
                        End If
                        
                    Next
                End If
                xmlFileInfo.AppendChild(xmlStreamDetails)
                xmlEpisode.AppendChild(xmlFileInfo)
            End If

            xmlEpisode.AppendChild(doc, "title"         , ep.Title.value        )
            xmlEpisode.AppendChild(doc, "season"        , ep.Season.Value       )
            xmlEpisode.AppendChild(doc, "episode"       , ep.Episode.Value      )
            xmlEpisode.AppendChild(doc, "aired"         , ep.Aired.Value        )
            xmlEpisode.AppendChild(doc, "plot"          , ep.plot.Value         )
            xmlEpisode.AppendChild(doc, "playcount"     , ep.playcount.Value    )
            xmlEpisode.AppendChild(doc, "director"      , ep.director.Value     )
            xmlEpisode.AppendChild(doc, "credits"       , ep.credits.Value      )
            xmlEpisode.AppendChild(doc, "rating"        , ep.rating.Value       )
            xmlEpisode.AppendChild(doc, "votes"         , ep.votes.Value        )
            xmlEpisode.AppendChild(doc, "uniqueid"      , ep.uniqueid.Value     )
            xmlEpisode.AppendChild(doc, "runtime"       , ep.runtime.Value      )
            xmlEpisode.AppendChild(doc, "showid"        , ep.showid.Value       )
            xmlEpisode.AppendChild(doc, "displayseason" , ep.DisplaySeason.Value)
            xmlEpisode.AppendChild(doc, "displayepisode", ep.DisplayEpisode.Value)
            xmlEpisode.AppendChild(doc, "runtime"       , ep.Runtime.Value      )
            xmlEpisode.AppendChild(doc, "epbookmark"    , ep.EpBookmark.Value   )
            xmlEpisode.AppendChild(doc, "videosource"   , ep.Source.Value       )
            
            Dim actorstosave As Integer = ep.ListActors.Count
            If actorstosave > Pref.maxactors Then actorstosave = Pref.maxactors
            For f = 0 To actorstosave - 1
                xmlActor = doc.CreateElement("actor")
                xmlActor.AppendChild(doc, "name", ep.ListActors(f).actorname)
                xmlActor.AppendChild(doc, "role", ep.ListActors(f).actorrole)
                If Not String.IsNullOrEmpty(ep.ListActors(f).actorthumb) Then
                    xmlActor.AppendChild(doc, "thumb", ep.ListActors(f).actorthumb)
                End If
                xmlEpisode.AppendChild(xmlActor)
            Next
            If listofepisodes.Count = 1 Then    'file is a single episode
                root = xmlEpisode               'root now equals 'episodedetails'
                Exit For                        'now append to XML doc as a single episode
            End If
            root.AppendChild(xmlEpisode)        'otherwise, each episode is appended to the 'multiepisode' element
        Next

        doc.AppendChild(root)
        Try
            SaveXMLDoc(doc, path)
        Catch
        End Try

    End Sub

    Public Function tv_NfoLoadFull(ByVal path As String) As TvShow


        Dim newtvshow As New TvShow
        If Not File.Exists(path) Then
            newtvshow.Title.Value = Utilities.GetLastFolder(path)
            'newtvshow.Year.Value = newtvshow.Title.Value & " (0000)"
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
            If IsNothing(newtvshow.Year.Value) Then
                If newtvshow.Premiered.Value.Length = 10 Then
                    newtvshow.Year.Value = newtvshow.Premiered.Value.Substring(0,4)
                End If
            ElseIf newtvshow.Year.Value.ToInt = 0 AndAlso newtvshow.Premiered.Value.Length = 10 Then
                newtvshow.Year.Value = newtvshow.Premiered.Value.Substring(0,4)
            End If
        End If
        For Each season As TvSeason In newtvshow.Seasons.Values
            For Each episode In season.Episodes
                episode.ShowId = newtvshow.TvdbId
            Next
        Next
        Return newtvshow

    End Function

    Public Function tv_NfoLoad(ByVal path As String) As TvShow
        Dim newtvshow As New TvShow
        If Not File.Exists(path) Then
            newtvshow.Title.Value = Utilities.GetLastFolder(path)
            'newtvshow.Year.Value = newtvshow.Title.Value & " (0000)"
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
            If newtvshow.Year.Value.ToInt = 0 AndAlso newtvshow.Premiered.Value.Length = 10 Then
                newtvshow.Year.Value = newtvshow.Premiered.Value.Substring(0,4)
            End If
        End If
        Return newtvshow
    End Function

    Public Sub tv_NfoSave(ByVal Path As String, ByRef Show As TvShow, Optional ByVal overwrite As Boolean = True, Optional ByVal forceunlocked As String = "")
        If File.Exists(Path) And Not overwrite Then Exit Sub

        Show.Save(Path)
    End Sub

    Public Function tv_NfoLoadCheck(ByVal Path As String) As Boolean
        'Check if XBMC nfo and correct some entries before loading into Media Companion.
        Dim aok As Boolean = True
        Try
            Dim thistvshow As New XmlDocument
            Using tmpstrm As IO.StreamReader = File.OpenText(Path)
                thistvshow.Load(tmpstrm)
            End Using
            Dim nodeToFind As XmlNode
		    Dim root As XmlElement = thistvshow.DocumentElement

		    ' Selects all the title elements that have an attribute named lang
		    nodeToFind = root.SelectSingleNode("epbookmark")
            If nodeToFind IsNot Nothing Then aok = False
            If Not aok Then 
                Dim tempshow As New TvShow
                tempshow = tvshow_NfoLoad(Path)
                tvshow_NfoSave(tempshow, True, "0")
                aok = True
            End If
        Catch ex As Exception
            aok = False
        End Try
        Return aok
    End Function
    
    Public Sub tvshow_NfoSave(ByVal tvshowtosave As TvShow, Optional ByVal overwrite As Boolean = True, Optional ByVal lock As String = "")

        Monitor.Enter(Me)
        Dim stage As Integer = 1
        Try
            Dim filenameandpath As String = tvshowtosave.NfoFilePath

            If tvshowtosave Is Nothing Then Exit Sub
            If File.Exists(filenameandpath) AndAlso Not overwrite Then Exit Sub
            Dim doc As New XmlDocument
            Dim root As XmlElement
            Dim child As XmlElement
            Dim actorchild As XmlElement
            root = doc.CreateElement("tvshow")
            Dim thumbnailstring As String = ""
            stage = 2
            Dim thispref As XmlNode = Nothing
            Dim xmlproc As XmlDeclaration

            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            doc.AppendChild(xmlproc)

            stage = 3
            child = doc.CreateElement("id") : child.InnerText = tvshowtosave.tvdbid.Value
            root.AppendChild(child)

            stage = 4
            child = doc.CreateElement("state")
            If lock = "" Then
                child.InnerText = tvshowtosave.State 
            Else
                child.InnerText = Media_Companion.ShowState.Open
            End If
            root.AppendChild(child)

            stage = 5
            root.AppendChild(doc, "title"           , tvshowtosave.Title.Value      )
            'child = doc.CreateElement("title") : child.InnerText = tvshowtosave.title.Value
            'root.AppendChild(child)
                             
            child = doc.CreateElement("showtitle") : child.InnerText = tvshowtosave.title.Value
            root.AppendChild(child)

            stage = 6
            child = doc.CreateElement("mpaa") : child.InnerText = tvshowtosave.mpaa.Value
            root.AppendChild(child)

            stage = 7
            child = doc.CreateElement("plot") : child.InnerText = tvshowtosave.plot.Value
            root.AppendChild(child)

            stage = 8
            child = doc.CreateElement("imdbid") : child.InnerText = tvshowtosave.imdbid.Value
            root.AppendChild(child)

            stage = 9
            child = doc.CreateElement("status") : child.InnerText = tvshowtosave.Status.Value
            root.AppendChild(child)

            stage = 10
            child = doc.CreateElement("runtime")
            If tvshowtosave.runtime.Value <> Nothing Then
                Dim minutes As String = tvshowtosave.runtime.Value.ToMin
                'minutes = minutes.Replace("minutes", "")
                'minutes = minutes.Replace("mins", "")
                'minutes = minutes.Replace("min", "")
                'minutes = minutes.Replace(" ", "")
                Try
                    Do While minutes.IndexOf("0") = 0
                        minutes = minutes.Substring(1, minutes.Length - 1)
                    Loop
                    If minutes = "" Then minutes = "00"
                    If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Pref.roundminutes = True Then
                        minutes = "0" & minutes & " min"
                    ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Pref.roundminutes = True Then
                        minutes = "00" & minutes & " min"
                    Else
                        minutes = tvshowtosave.runtime.Value
                    End If
                Catch
                    minutes = "00"
                End Try
                child.InnerText = minutes
            Else
                child.InnerText = tvshowtosave.Runtime.Value
            End If
            root.AppendChild(child)

            stage = 11
            child = doc.CreateElement("rating") : child.InnerText = tvshowtosave.rating.Value
            root.AppendChild(child)
            child = doc.CreateElement("votes") : child.InnerText = tvshowtosave.Votes.Value
            root.AppendChild(child)

            stage = 12
            child = doc.CreateElement("year") : child.InnerText = tvshowtosave.year.Value
            root.AppendChild(child)

            stage = 13
            child = doc.CreateElement("premiered") : child.InnerText = tvshowtosave.premiered.Value
            root.AppendChild(child)

            stage = 14
            child = doc.CreateElement("studio") : child.InnerText = tvshowtosave.studio.Value
            root.AppendChild(child)

            stage = 15
            If tvshowtosave.genre.Value <> "" Then
                Dim strArr2() As String
                strArr2 = tvshowtosave.genre.Value.Split("/")
                For count = 0 To strArr2.Length - 1
                    child = doc.CreateElement("genre")
                    strArr2(count) = strArr2(count).Trim
                    child.InnerText = strArr2(count)
                    root.AppendChild(child)
                Next
            End If
            'child = doc.CreateElement("genre") : child.InnerText = tvshowtosave.genre.Value
            'root.AppendChild(child)

            stage = 16
            child = doc.CreateElement("episodeguide")
            Dim childchild As XmlElement
            childchild = doc.CreateElement("url")
            Dim tempppp As String = tvshowtosave.tvdbid.value & ".xml"
            Dim Attr As XmlAttribute
            Attr = doc.CreateAttribute("cache")
            Attr.Value = tempppp
            childchild.Attributes.Append(Attr)
            If Not IsNothing(tvshowtosave.episodeguideurl) Then
                '"http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
                If tvshowtosave.tvdbid.Value <> Nothing Then
                    If IsNumeric(tvshowtosave.tvdbid.Value) Then
                        If tvshowtosave.language.Value <> Nothing Then
                            If tvshowtosave.language.Value <> "" Then
                                childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid.Value & "/all/" & tvshowtosave.language.Value & ".zip"
                                child.AppendChild(childchild)
                            Else
                                childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid.Value & "/all/en.zip"
                                child.AppendChild(childchild)
                            End If
                        Else
                            childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvshowtosave.tvdbid.Value & "/all/en.zip"
                            child.AppendChild(childchild)
                        End If
                    End If
                End If
                root.AppendChild(child)
            End If

            stage = 17
            child = doc.CreateElement("language") : child.InnerText = tvshowtosave.language.Value
            root.AppendChild(child)

            stage = 18
            For Each thumbnail In tvshowtosave.posters
                child = doc.CreateElement("thumb")
                child.InnerText = thumbnail
                root.AppendChild(child)
            Next

            stage = 19
            For Each thumbnail In tvshowtosave.fanart
                child = doc.CreateElement("fanart")
                child.InnerText = thumbnail
                root.AppendChild(child)
            Next

            stage = 20
            For each act As Actor In tvshowtosave.ListActors
                child = doc.CreateElement("actor")
                actorchild = doc.CreateElement("actorid")
                actorchild.InnerText = act.actorid 
                child.AppendChild(actorchild)
                actorchild = doc.CreateElement("name")
                actorchild.InnerText = act.actorname
                child.AppendChild(actorchild)
                actorchild = doc.CreateElement("role")
                actorchild.InnerText = act.actorrole
                child.AppendChild(actorchild)
                If act.actorthumb <> Nothing Then
                    If act.actorthumb <> "" Then
                        actorchild = doc.CreateElement("thumb")
                        actorchild.InnerText = act.actorthumb
                        child.AppendChild(actorchild)
                    End If
                End If
                actorchild = doc.CreateElement("order")
                actorchild.InnerText = act.order
                child.AppendChild(actorchild)
                root.AppendChild(child)
            Next
            
            stage = 21
            child = doc.CreateElement("episodeactorsource") : child.InnerText = tvshowtosave.episodeactorsource.Value
            root.AppendChild(child)

            child = doc.CreateElement("tvshowactorsource") : child.InnerText = tvshowtosave.tvshowactorsource.Value
            root.AppendChild(child)

            stage = 22
            child = doc.CreateElement("sortorder") : child.InnerText = tvshowtosave.sortorder.Value
            root.AppendChild(child)

            stage = 23
            If tvshowtosave.SortTitle.Value <> "" Then 
                child = doc.CreateElement("sorttitle") : child.InnerText = tvshowtosave.SortTitle.Value
                root.AppendChild(child)
            End If

            doc.AppendChild(root)

            stage = 34
            SaveXMLDoc(doc, filenameandpath)
            ''Dim output As New XmlTextWriter(filenameandpath, System.Text.Encoding.UTF8)
            ''output.Formatting = Formatting.Indented
            ''output.Indentation = 4
            'Dim settings As New XmlWriterSettings()
            'settings.Encoding = New UTF8Encoding(False) 
            'settings.Indent = True
            'settings.IndentChars = (ControlChars.Tab)
            'settings.NewLineHandling = NewLineHandling.None
            'Dim writer As XmlWriter = XmlWriter.Create(filenameandpath, settings)
            'stage = 35
            ''doc.WriteTo(output)
            ''output.Close()
            'doc.Save(writer)
            'writer.Close()
        Catch ex As Exception
            MsgBox("Error Encountered at stage " & stage.ToString & vbCrLf & vbCrLf & ex.ToString)
        Finally
            Monitor.Exit(Me)
        End Try
    End Sub

    Public Function tvshow_NfoLoad(ByVal path As String)
        Try
            Dim newtvshow As New TvShow
            Dim tvshow As New XmlDocument
            Try
                Using tmpstrm As IO.StreamReader = File.OpenText(path)
                    tvshow.Load(tmpstrm)
                End Using
            Catch ex As Exception
                Return blanktvshow(path)
                Exit Function
            End Try
            newtvshow.State = Media_Companion.ShowState.Unverified 
            Dim thisresult As XmlNode = Nothing
            Dim tempid As String = ""
            For Each thisresult In tvshow("tvshow")
                Try
                    Select Case thisresult.Name
                        Case "id"
                            newtvshow.TvdbId.Value = thisresult.InnerText
                        Case "state"
                            newtvshow.State = thisresult.InnerText
                        Case "title"
                            Dim tempstring As String = ""
                            tempstring = thisresult.InnerText
                            ' Ignore Article here??
                            newtvshow.Title.Value = tempstring
                        Case "episodeguide"
                            tempid = thisresult.InnerText
                        Case "imdbid"
                            newtvshow.ImdbId.Value = thisresult.InnerText
                        Case "status"
                            newtvshow.Status.Value = thisresult.InnerText 
                        Case "mpaa"
                            newtvshow.Mpaa.Value = thisresult.InnerText
                        Case "plot"
                            newtvshow.Plot.Value = thisresult.InnerText
                        Case "runtime"
                            newtvshow.Runtime.Value = thisresult.InnerText
                        Case "rating"
                            Dim tmpstr As String = thisresult.InnerText
                            If tmpstr.IndexOf("/10") <> -1 Then tmpstr.Replace("/10", "")
                            If tmpstr.IndexOf(" ") <> -1 Then tmpstr.Replace(" ", "")
                            newtvshow.Rating.Value = tmpstr
                        Case "votes"
                            newtvshow.Votes.Value = thisresult.InnerText
                        Case "year"
                            newtvshow.Year.Value = thisresult.InnerText
                        Case "premiered"
                            newtvshow.Premiered.Value = thisresult.InnerText
                        Case "studio"
                            newtvshow.Studio.Value = thisresult.InnerText
                        Case "genre"
                            If newtvshow.genre.Value = Nothing Then                     'genres in nfo's may be individual elements if from XBMC nfo,
                                newtvshow.genre.Value = thisresult.InnerText       'in MC cache they are one string seperated by " / "
                            Else
                                newtvshow.genre.Value = newtvshow.genre.Value & " / " & thisresult.InnerText
                            End If
                        Case "language"
                            newtvshow.Language.Value = thisresult.InnerText
                        Case "episodeactorsource"
                            newtvshow.EpisodeActorSource.Value = thisresult.InnerText
                        Case "tvshowactorsource"
                            newtvshow.TvShowActorSource.Value = thisresult.InnerText
                        Case "sortorder"
                            newtvshow.SortOrder.Value = thisresult.InnerText
                        Case "sorttitle"
                            newtvshow.SortTitle.Value = thisresult.InnerText 
                        Case "actor"
                            Dim newactor As New str_MovieActors(SetDefaults)
                            Dim detail As XmlNode = Nothing
                            For Each detail In thisresult.ChildNodes
                                Select Case detail.Name
                                    Case "actorid"
                                        newactor.actorid = detail.InnerText
                                    Case "name"
                                        newactor.actorname = detail.InnerText
                                    Case "role"
                                        newactor.actorrole = detail.InnerText
                                    Case "thumb"
                                        newactor.actorthumb = detail.InnerText
                                    Case "order"
                                        newactor.order = detail.InnerText 
                                End Select
                            Next
                            newtvshow.listactors.Add(newactor)
                    End Select
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            Next
            
            newtvshow.NfoFilePath = path

            Dim filecreation As FileInfo = New FileInfo(path)
            Dim myDate As Date = filecreation.LastWriteTime

            If newtvshow.tvdbid = Nothing Then newtvshow.TvdbId.Value = ""
            If newtvshow.ImdbId.Value = Nothing Then newtvshow.ImdbId.Value = "0"
            'If newtvshow.genre = Nothing Then newtvshow.Genre.Value = ""
            'If newtvshow.rating = Nothing Then newtvshow.Rating.Value = ""
            If newtvshow.Mpaa.Value = Nothing Then newtvshow.Mpaa.Value = "na"
            If newtvshow.Studio.Value = Nothing Then newtvshow.Studio.Value = "-"
            If newtvshow.Runtime.Value = Nothing Then newtvshow.Runtime.Value = "0"
            If newtvshow.Year.Value <> "" AndAlso newtvshow.Year.Value.ToInt <> 0 AndAlso newtvshow.Year.Value <> "0000" AndAlso Not IsNothing(newtvshow.Premiered.Value) Then
                If newtvshow.Premiered.Value.Length = 10 Then
                    Dim tmp As String = newtvshow.Premiered.Value.Substring(0,4)
                    newtvshow.Year.Value = tmp
                End If
            ElseIf String.IsNullOrEmpty(newtvshow.Year.Value) Then
                newtvshow.Year.Value = "0000"
            End If
            If newtvshow.TvShowActorSource.Value = Nothing Then
                If Pref.TvdbActorScrape = 0 Or Pref.TvdbActorScrape = 3 Then
                    newtvshow.TvShowActorSource.Value = "tvdb"
                Else
                    newtvshow.TvShowActorSource.Value = "imdb"
                End If
            End If
            If newtvshow.EpisodeActorSource.Value = Nothing Then
                If Pref.TvdbActorScrape = 0 Or Pref.TvdbActorScrape = 2 Then
                    newtvshow.EpisodeActorSource.Value = "tvdb"
                Else
                    newtvshow.EpisodeActorSource.Value = "imdb"
                End If
            End If
            If tempid <> "" Then
                Dim i As Integer = tempid.LastIndexOf("/")+1
                tempid = tempid.Substring(i, (tempid.length-i))
                tempid = tempid.Replace(".zip", "")
                newtvshow.Language.Value = tempid
            End If
            If newtvshow.SortOrder.Value = Nothing Then newtvshow.SortOrder.Value = Pref.sortorder 

            Return newtvshow
        Catch
        End Try
        Return "Error"
    End Function

    Public Function blanktvshow(ByVal path As String) As TvShow
        blanktvshow = New TvShow
        blanktvshow.Title.Value = Utilities.GetLastFolder(path)
        'blanktvshow.Year.Value = blanktvshow.Title.Value & " (0000)"
        blanktvshow.NfoFilePath = path
        blanktvshow.Year.Value = "0000"
        blanktvshow.TvdbId.Value = ""
        blanktvshow.Status.Value = "missing"
        blanktvshow.State = Media_Companion.ShowState.Locked
        Return blanktvshow
    End Function
#End Region

#Region " Obsolete "

    'Public Shared Function ep_NfoLoadGeneric(ByVal path As String) ', ByVal season As String, ByVal episode As String)

    '    Dim newepisodelist As New List(Of TvEpisode)
    '    Dim newepisode As New TvEpisode
    '    If Not File.Exists(path) Then
    '        newepisode.Title.Value = Path.GetFileName(path)
    '        newepisode.Plot.Value = "missing file"

    '        newepisode.NfoFilePath = path
    '        If newepisode.Episode.Value = Nothing Or newepisode.Episode.Value = Nothing Then
    '            For Each regexp In Pref.tv_RegexScraper

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
    '            newepisode.Title.Value = Path.GetFileName(path)
    '            newepisode.Plot.Value = "problem / xml error"
    '            newepisode.NfoFilePath = path
    '            'newepisode.VideoFilePath = path
    '            If newepisode.Episode.Value = Nothing Or newepisode.Episode.Value = Nothing Then
    '                For Each regexp In Pref.tv_RegexScraper

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
    '                                                                newtvepisode.StreamDetails.Video.Width.Value = videodetails.InnerText
    '                                                            Case "height"
    '                                                                newtvepisode.StreamDetails.Video.Height.Value = videodetails.InnerText
    '                                                            Case "aspect"
    '                                                                newtvepisode.StreamDetails.Video.Aspect.Value = videodetails.InnerText
    '                                                            Case "codec"
    '                                                                newtvepisode.StreamDetails.Video.Codec.Value = videodetails.InnerText
    '                                                            Case "formatinfo"
    '                                                                newtvepisode.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
    '                                                            Case "durationinseconds"
    '                                                                newtvepisode.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
    '                                                            Case "bitrate"
    '                                                                newtvepisode.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
    '                                                            Case "bitratemode"
    '                                                                newtvepisode.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
    '                                                            Case "bitratemax"
    '                                                                newtvepisode.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
    '                                                            Case "container"
    '                                                                newtvepisode.StreamDetails.Video.Container.Value = videodetails.InnerText
    '                                                            Case "codecid"
    '                                                                newtvepisode.StreamDetails.Video.CodecId.Value = videodetails.InnerText
    '                                                            Case "codecidinfo"
    '                                                                newtvepisode.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
    '                                                            Case "scantype"
    '                                                                newtvepisode.StreamDetails.Video.ScanType.Value = videodetails.InnerText
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
    '                                                    newtvepisode.StreamDetails.Audio.Add(audio)
    '                                                Case "subtitle"
    '                                                    Dim subsdetails As XmlNode = Nothing
    '                                                    For Each subsdetails In detail.ChildNodes
    '                                                        Select Case subsdetails.Name
    '                                                            Case "language"
    '                                                                Dim sublang As New SubtitleDetails
    '                                                                sublang.Language.Value = subsdetails.InnerText
    '                                                                newtvepisode.StreamDetails.Subtitles.Add(sublang)
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
    '                For Each regexp In Pref.tv_RegexScraper

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
    '                                'Public filedetails As New StreamDetails


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
    '                                                                            anotherepisode.StreamDetails.Video.Width.Value = videodetails.InnerText
    '                                                                        Case "height"
    '                                                                            anotherepisode.StreamDetails.Video.Height.Value = videodetails.InnerText
    '                                                                        Case "codec"
    '                                                                            anotherepisode.StreamDetails.Video.Codec.Value = videodetails.InnerText
    '                                                                        Case "formatinfo"
    '                                                                            anotherepisode.StreamDetails.Video.FormatInfo.Value = videodetails.InnerText
    '                                                                        Case "durationinseconds"
    '                                                                            anotherepisode.StreamDetails.Video.DurationInSeconds.Value = videodetails.InnerText
    '                                                                        Case "bitrate"
    '                                                                            anotherepisode.StreamDetails.Video.Bitrate.Value = videodetails.InnerText
    '                                                                        Case "bitratemode"
    '                                                                            anotherepisode.StreamDetails.Video.BitrateMode.Value = videodetails.InnerText
    '                                                                        Case "bitratemax"
    '                                                                            anotherepisode.StreamDetails.Video.BitrateMax.Value = videodetails.InnerText
    '                                                                        Case "container"
    '                                                                            anotherepisode.StreamDetails.Video.Container.Value = videodetails.InnerText
    '                                                                        Case "codecid"
    '                                                                            anotherepisode.StreamDetails.Video.CodecId.Value = videodetails.InnerText
    '                                                                        Case "codecidinfo"
    '                                                                            anotherepisode.StreamDetails.Video.CodecInfo.Value = videodetails.InnerText
    '                                                                        Case "scantype"
    '                                                                            anotherepisode.StreamDetails.Video.ScanType.Value = videodetails.InnerText
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
    '                                                                anotherepisode.StreamDetails.Audio.Add(audio)
    '                                                            Case "subtitle"
    '                                                                Dim subsdetails As XmlNode = Nothing
    '                                                                For Each subsdetails In detail.ChildNodes
    '                                                                    Select Case subsdetails.Name
    '                                                                        Case "language"
    '                                                                            Dim sublang As New SubtitleDetails
    '                                                                            sublang.Language.Value = subsdetails.InnerText
    '                                                                            anotherepisode.StreamDetails.Subtitles.Add(sublang)
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
    '        If Pref.enabletvhdtags = True Then
    '            Try
    '                child = document.CreateElement("fileinfo")

    '                anotherchild = document.CreateElement("streamdetails")

    '                filedetailschild = document.CreateElement("video")
    '                If listofepisodes(0).StreamDetails.Video.Width <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.Width.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("width")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.Width.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.Height <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.Height.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("height")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.Height.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.Aspect <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.Aspect.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("aspect")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.Aspect.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.Codec <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.Codec.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("codec")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.Codec.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.FormatInfo <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.FormatInfo.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("format")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.FormatInfo.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.DurationInSeconds <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.DurationInSeconds.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("durationinseconds")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.DurationInSeconds.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.Bitrate <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.Bitrate.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("bitrate")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.Bitrate.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.BitrateMode <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.BitrateMode.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("bitratemode")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.BitrateMode.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.BitrateMax <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.BitrateMax.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("bitratemax")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.BitrateMax.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.Container <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.Container.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("container")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.Container.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.CodecId <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.CodecId.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("codecid")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.CodecId.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.CodecInfo <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.CodecInfo.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("codecidinfo")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.CodecInfo.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                If listofepisodes(0).StreamDetails.Video.ScanType <> Nothing Then
    '                    If listofepisodes(0).StreamDetails.Video.ScanType.Value <> "" Then
    '                        filedetailschildchild = document.CreateElement("scantype")
    '                        filedetailschildchild.InnerText = listofepisodes(0).StreamDetails.Video.ScanType.Value
    '                        filedetailschild.AppendChild(filedetailschildchild)
    '                    End If
    '                End If
    '                anotherchild.AppendChild(filedetailschild)

    '                If listofepisodes(0).StreamDetails.Audio.Count > 0 Then
    '                    For Each item In listofepisodes(0).StreamDetails.Audio

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
    '                    If listofepisodes(0).StreamDetails.Subtitles.Count > 0 Then
    '                        filedetailschild = document.CreateElement("subtitle")
    '                        For Each entry In listofepisodes(0).StreamDetails.Subtitles
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
    '        If actorstosave > Pref.maxactors Then actorstosave = Pref.maxactors
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
    '                If Pref.enabletvhdtags = True Then
    '                    Try
    '                        xmlStreamDetails = document.CreateElement("streamdetails")
    '                        xmlFileInfo = document.CreateElement("fileinfo")
    '                        xmlFileInfoType = document.CreateElement("video")
    '                        If ep.StreamDetails.Video.Width <> Nothing Then
    '                            If ep.StreamDetails.Video.Width.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("width")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.Width.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.Height <> Nothing Then
    '                            If ep.StreamDetails.Video.Height.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("height")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.Height.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.Aspect <> Nothing Then
    '                            If ep.StreamDetails.Video.Aspect.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("aspect")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.Aspect.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.Codec <> Nothing Then
    '                            If ep.StreamDetails.Video.Codec.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("codec")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.Codec.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.FormatInfo <> Nothing Then
    '                            If ep.StreamDetails.Video.FormatInfo.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("format")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.FormatInfo.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.DurationInSeconds.Value <> Nothing Then
    '                            If ep.StreamDetails.Video.DurationInSeconds.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("durationinseconds")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.DurationInSeconds.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.Bitrate <> Nothing Then
    '                            If ep.StreamDetails.Video.Bitrate.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("bitrate")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.Bitrate.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.BitrateMode <> Nothing Then
    '                            If ep.StreamDetails.Video.BitrateMode.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("bitratemode")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.BitrateMode.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.BitrateMax <> Nothing Then
    '                            If ep.StreamDetails.Video.BitrateMax.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("bitratemax")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.BitrateMax.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.Container <> Nothing Then
    '                            If ep.StreamDetails.Video.Container.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("container")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.Container.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.CodecId <> Nothing Then
    '                            If ep.StreamDetails.Video.CodecId.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("codecid")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.CodecId.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.CodecInfo <> Nothing Then
    '                            If ep.StreamDetails.Video.CodecInfo.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("codecidinfo")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.CodecInfo.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        If ep.StreamDetails.Video.ScanType <> Nothing Then
    '                            If ep.StreamDetails.Video.ScanType.Value <> "" Then
    '                                xmlFileInfoTypechild = document.CreateElement("scantype")
    '                                xmlFileInfoTypechild.InnerText = ep.StreamDetails.Video.ScanType.Value
    '                                xmlFileInfoType.AppendChild(xmlFileInfoTypechild)
    '                            End If
    '                        End If
    '                        xmlFileInfo.AppendChild(xmlFileInfoType)
    '                        If ep.StreamDetails.Audio.Count > 0 Then
    '                            For Each aud In ep.StreamDetails.Audio
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
    '                        If ep.StreamDetails.Subtitles.Count > 0 Then
    '                            For Each subt In ep.StreamDetails.Subtitles
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
    '            If actorstosave > Pref.maxactors Then actorstosave = Pref.maxactors
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
    Public Function mov_NfoLoadBasic(ByVal loadpath As String, ByVal mode As String,_oMovies As Movies) As ComboList
        Dim newmovie As New ComboList
        Try
            If Not File.Exists(loadpath) Then
                newmovie.title = "Error"
                Return newmovie
            End If
            newmovie.oMovies = _oMovies
            If mode = "movielist" Then
                Dim movie As New XmlDocument
                Try
                    Using tmpstrm As IO.StreamReader = File.OpenText(loadpath)
                        movie.Load(tmpstrm)
                    End Using
                Catch ex As Exception
                    If Not util_NfoValidate(loadpath) Then
                        newmovie.title = "Error"
                        Return newmovie
                    End If

                    newmovie.createdate = "999999999999"
                    Dim filecreation2 As New FileInfo(loadpath)
                    Dim myDate2 As Date = filecreation2.LastWriteTime
                    Try
                        newmovie.filedate = Format(myDate2, Pref.datePattern).ToString
                    Catch
                    End Try
                    newmovie.filename = Path.GetFileName(loadpath)
                    newmovie.foldername = Utilities.GetLastFolder(loadpath)
                    newmovie.fullpathandfilename = loadpath
                    newmovie.genre = "problem / xml error"
                    newmovie.movietag.Clear()
                    newmovie.id = ""
                    newmovie.tmdbid = ""
                    newmovie.missingdata1 = 0
                    newmovie.SetName = ""
                    newmovie.TmdbSetId = ""
                    newmovie.source = ""
                    newmovie.director = ""
                    newmovie.credits = ""
                    newmovie.originaltitle = newmovie.title
                    newmovie.outline = ""
                    newmovie.tagline = ""
                    newmovie.playcount = "0"
                    newmovie.lastplayed = ""
                    newmovie.plot = ""
                    newmovie.rating = ""
                    newmovie.runtime = "0"
                    newmovie.sortorder = ""
                    newmovie.title = Path.GetFileName(loadpath)
                    newmovie.top250 = "0"
                    newmovie.year = "1850"
                    newmovie.stars = ""
                    newmovie.usrrated = 0
                    Return newmovie
                End Try

                Dim thisresult As XmlNode = Nothing
                For Each thisresult In movie("movie")
                    Try
                        Select Case thisresult.Name
                            Case "title"
                                newmovie.title = thisresult.InnerText
                            Case "originaltitle"
                                newmovie.originaltitle = thisresult.InnerText
                            Case "set"
                                newmovie.SetName = thisresult.InnerText
                            Case "setid"
                                newmovie.TmdbSetId = thisresult.InnerText 
                            Case "source"
                                newmovie.source = thisresult.InnerText
                            Case "diretor"
                                If newmovie.director = "" Then
                                    newmovie.director = thisresult.InnerText
                                Else
                                    newmovie.director = newmovie.director & " / " & thisresult.InnerText
                                End If
                            Case "credits"
                                If newmovie.credits = "" Then
                                    newmovie.credits = thisresult.InnerText 
                                Else
                                    newmovie.credits = newmovie.credits & " / " & thisresult.InnerText 
                                End If
                            Case "year"
                                Dim ayear As Integer = thisresult.InnerText.ToInt
                                If ayear = 0 Then ayear = 1850
                                newmovie.year = ayear
                            Case "outline"
                                newmovie.outline = thisresult.InnerText
                            Case "plot"
                                newmovie.plot = thisresult.InnerText
                            Case "tagline"
                                newmovie.tagline = thisresult.InnerText 
                            Case "genre"
                                If newmovie.genre = "" Then                     'genres in nfo's are individual elements - in MC cache they are one string seperated by " / "
                                    newmovie.genre = thisresult.InnerText
                                Else
                                    newmovie.genre = newmovie.genre & " / " & thisresult.InnerText
                                End If
                            Case "countries"
                                If newmovie.countries = "" Then
                                    newmovie.countries = thisresult.InnerText
                                Else
                                    newmovie.countries = newmovie.countries & " / " & thisresult.InnerText
                                End If
                            Case "studios"
                                If newmovie.studios = "" Then
                                    newmovie.studios = thisresult.InnerText 
                                Else
                                    newmovie.studios = newmovie.studios & " / " & thisresult.InnerText 
                                End If
                            Case "tag"
                                newmovie.movietag.Add(thisresult.InnerText)
                            Case "id"
                                If thisresult.Attributes.Count = 0 Then newmovie.id = thisresult.InnerText 'ignore any id nodes with attributes
                            Case "stars"
                                newmovie.stars = thisresult.InnerText 
                            Case "tmdbid"
                                newmovie.tmdbid = thisresult.InnerText 
                            Case "playcount"
                                newmovie.playcount = thisresult.InnerText
                            Case "lastplayed"
                                newmovie.lastplayed = thisresult.InnerText
                            Case "rating"
                                newmovie.rating = thisresult.InnerText.ToRating
                            Case "userrating"
                                newmovie.usrrated = thisresult.InnerText.ToInt 
                            Case " metascore"
                                newmovie.metascore = thisresult.InnerText
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
                                Dim vote As String = thisresult.InnerText
                                If Not String.IsNullOrEmpty(vote) Then
                                    vote = vote.Replace(",", "")
                                    newmovie.Votes = vote.toint
                                End If
                            Case "mpaa"    'aka Certificate
                                newmovie.Certificate = thisresult.InnerText
                            Case "fileinfo"
                                Dim gotWidth As Boolean
                                Dim gotHeight As Boolean
                                For Each res In thisresult.ChildNodes
                                    If res.name = "streamdetails" Then
                                        Dim newfilenfo As New StreamDetails
                                        For Each detail In res.ChildNodes
                                            Select Case detail.Name
                                                Case "video"
                                                    For Each videodetails As XmlNode In detail.ChildNodes
                                                        Select Case videodetails.Name
                                                            Case "width"
                                                                newfilenfo.Video.Width.Value = videodetails.InnerText
                                                                gotWidth = True
                                                            Case "height"
                                                                newfilenfo.Video.Height.Value = videodetails.InnerText
                                                                gotHeight = True
                                                            Case "codec"
                                                                newmovie.VideoCodec = videodetails.InnerText
                                                        End Select
                                                        If gotWidth And gotHeight Then
                                                            newmovie.Resolution = newfilenfo.Video.VideoResolution
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
                Dim filecreation As New FileInfo(loadpath)
                Dim myDate As Date = filecreation.LastWriteTime


                If newmovie.title = Nothing Then newmovie.title = "ERR - This Movie Has No TITLE!"
                If newmovie.createdate = "" Or newmovie.createdate = Nothing Then newmovie.createdate = "18000101000000"
                Try
                    newmovie.filedate = Format(myDate, Pref.datePattern)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                newmovie.filename               = Path.GetFileName(loadpath)
                newmovie.foldername             = Utilities.GetLastFolder(loadpath)
                newmovie.fullpathandfilename    = loadpath
                newmovie.rootfolder             = Pref.GetRootFolder(loadpath) & "\"
                If newmovie.genre               = Nothing Then newmovie.genre = ""
                If newmovie.id                  = Nothing Then newmovie.id = ""
                If newmovie.tmdbid              = Nothing Then newmovie.tmdbid = ""
                If newmovie.missingdata1        = Nothing Then newmovie.missingdata1 = 0
                If newmovie.source              = Nothing Then newmovie.source = ""
                If newmovie.director            = Nothing Then newmovie.director = ""
                If newmovie.credits             = Nothing Then newmovie.credits = ""
                If newmovie.SetName             = "" Then newmovie.SetName = "-None-"
                If newmovie.originaltitle       = "" Or newmovie.originaltitle = Nothing Then newmovie.originaltitle = newmovie.title
                If newmovie.playcount           = Nothing Then newmovie.playcount = "0"
                If newmovie.lastplayed          = Nothing Then newmovie.lastplayed = ""
                If newmovie.plot                = Nothing Then newmovie.plot = ""
                If newmovie.rating              = Nothing Then newmovie.rating = 0
                If newmovie.runtime             = Nothing Then newmovie.runtime = ""
                If newmovie.usrrated            = Nothing Then newmovie.usrrated = 0
                If newmovie.sortorder           = Nothing Or newmovie.sortorder = "" Then newmovie.sortorder = newmovie.title
                If newmovie.top250              = Nothing Then newmovie.top250 = "0"
                If newmovie.year                = Nothing Then newmovie.year = "0001"
                
                movie = Nothing
            End If
            Return newmovie


        Catch
        End Try

        newmovie.title = "Error"
        Return newmovie

    End Function

    Public Shared Function mov_NfoLoadFull(ByVal loadpath As String) As FullMovieDetails

        ConvertFileToUTF8IfNotAlready(loadpath)

        Try
            Dim newmovie As New FullMovieDetails
            newmovie.fullmoviebody.genre = ""
            Dim newfilenfo As New StreamDetails
            newmovie.filedetails = newfilenfo
            Dim thumbstring As String = String.Empty
            Dim watched As Boolean = False
            If Not File.Exists(loadpath) Then
            Else
                Dim movie As New XmlDocument
                Try
                    Using tmpstrm As IO.StreamReader = File.OpenText(loadpath)
                        movie.Load(tmpstrm)
                    End Using
                Catch ex As Exception
                    If Not util_NfoValidate(loadpath) Then
                        File.Move(loadpath, loadpath.Replace(".nfo",".info"))
                        newmovie.fullmoviebody.title = "Error"
                        Return newmovie
                    End If
                    Dim errorstring As String
                    errorstring = ex.Message.ToString & vbCrLf & vbCrLf
                    errorstring += ex.StackTrace.ToString
                    newmovie.fullmoviebody.title        = "Unknown" 'Utilities.CleanFileName(Path.GetFileName(workingMovie.fullpathandfilename))
                    newmovie.fullmoviebody.year         = "1850"
                    newmovie.fullmoviebody.top250       = "0"
                    newmovie.fullmoviebody.credits      = ""
                    newmovie.fullmoviebody.director     = ""
                    newmovie.fullmoviebody.stars        = ""
                    newmovie.fullmoviebody.filename     = ""
                    newmovie.fullmoviebody.genre        = ""
                    newmovie.fullmoviebody.tag.Clear()    ' = ""
                    newmovie.fullmoviebody.imdbid       = ""
                    newmovie.fullmoviebody.tmdbid       = ""
                    newmovie.fullmoviebody.mpaa         = ""
                    newmovie.fullmoviebody.outline      = "This nfo file could not be loaded"
                    newmovie.fullmoviebody.playcount    = "0"
                    newmovie.fullmoviebody.lastplayed   = ""
                    newmovie.fullmoviebody.plot         = errorstring
                    newmovie.fullmoviebody.premiered    = ""
                    newmovie.fullmoviebody.rating       = ""
                    newmovie.fullmoviebody.usrrated     = "0"
                    newmovie.fullmoviebody.runtime      = ""
                    newmovie.fullmoviebody.studio       = ""
                    newmovie.fullmoviebody.tagline      = "Rescraping the movie might fix the problem"
                    newmovie.fullmoviebody.trailer      = ""
                    newmovie.fullmoviebody.votes        = ""
                    newmovie.fullmoviebody.sortorder    = ""
                    newmovie.fullmoviebody.country      = ""
                    newmovie.fileinfo.createdate        = ""
                    Return newmovie
                End Try
                Dim thisresult As XmlNode = Nothing
                Dim Flag4resave As Boolean = False

                For Each thisresult In movie("movie")
                    Select Case thisresult.Name
                        Case "alternativetitle"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                        Case "set"
                            newmovie.fullmoviebody.SetName = thisresult.InnerText
                        Case "setid"
                            newmovie.fullmoviebody.TmdbSetId = thisresult.InnerText
                        Case "collectionnumber"
                            'prefer Set Id from <setid>, so don't overwrite if coming from <collectionnumber>
                            If newmovie.fullmoviebody.TmdbSetId = "" Then
                                newmovie.fullmoviebody.TmdbSetId = thisresult.InnerText
                            End If
                        Case "videosource"
                            newmovie.fullmoviebody.source = thisresult.InnerText
                        Case "sortorder"
                            newmovie.fullmoviebody.sortorder = thisresult.InnerText
                        Case "sorttitle"
                            newmovie.fullmoviebody.sortorder = thisresult.InnerText
                        Case "votes"
                            Dim vote As String = thisresult.InnerText
                            If Not String.IsNullOrEmpty(vote) Then vote = vote.Replace(",", "")
                            newmovie.fullmoviebody.votes = vote
                        Case "country"
                            If newmovie.fullmoviebody.country = "" Then
                                newmovie.fullmoviebody.country = thisresult.InnerText
                            Else
                                newmovie.fullmoviebody.country &= " / " & thisresult.InnerText
                            End If
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
                            If newmovie.fullmoviebody.credits = "" Then
                                Dim resultstr As String = thisresult.InnerText
                                If resultstr.Contains(", ") AndAlso Not resultstr.Contains(" / ") Then resultstr = resultstr.Replace(", ", " / ")
                                newmovie.fullmoviebody.credits = resultstr
                            Else
                                newmovie.fullmoviebody.credits = newmovie.fullmoviebody.credits & " / " & thisresult.InnerText
                            End If
                        Case "director"
                            If newmovie.fullmoviebody.director = "" Then
                                newmovie.fullmoviebody.director = thisresult.InnerText
                            Else
                                newmovie.fullmoviebody.director = newmovie.fullmoviebody.director & " / " & thisresult.InnerText
                            End If
                        Case "stars"
                            newmovie.fullmoviebody.stars = thisresult.InnerText.Replace(", ", " / ")

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
                            If newmovie.fullmoviebody.studio = "" Then
                                Dim resultstr As String = thisresult.InnerText
                                If resultstr.Contains(", ") AndAlso Not resultstr.Contains(" / ") Then resultstr = resultstr.Replace(", ", " / ")
                                newmovie.fullmoviebody.studio = resultstr
                            Else
                                newmovie.fullmoviebody.studio &= " / " & thisresult.InnerText
                            End If
                        Case "trailer"
                            newmovie.fullmoviebody.trailer = thisresult.InnerText
                        Case "title"
                            newmovie.alternativetitles.Add(thisresult.InnerText)
                            newmovie.fullmoviebody.title = thisresult.InnerText
                        Case "originaltitle"
                            newmovie.fullmoviebody.originaltitle = thisresult.InnerText
                        Case "year"
                            Dim ayear As String = thisresult.InnerText 
                            If ayear.ToInt = 0 Then ayear = "1850"
                            newmovie.fullmoviebody.year = ayear
                        Case "genre"
                            If newmovie.fullmoviebody.genre = "" Then
                                newmovie.fullmoviebody.genre = thisresult.InnerText
                            Else
                                newmovie.fullmoviebody.genre = newmovie.fullmoviebody.genre & " / " & thisresult.InnerText
                            End If
                        Case "tag", "tags"
                            If thisresult.Name = "tags" Then Flag4resave = True
                            newmovie.fullmoviebody.tag.Add(thisresult.InnerText)
                        Case "id"
                            Dim myresult As String = thisresult.InnerText
                            Dim testAttribute as XmlAttribute = CType(thisresult.Attributes.GetNamedItem("moviedb"),  XmlAttribute)
                            If testAttribute isnot nothing then
                                If testAttribute.InnerText = "imdb" Then newmovie.fullmoviebody.imdbid = thisresult.InnerText
                                If testAttribute.InnerText = "tmdb" Then newmovie.fullmoviebody.tmdbid = thisresult.InnerText
                                If testAttribute.InnerText = "themoviedb" Then newmovie.fullmoviebody.tmdbid = thisresult.InnerText
                            Else
                                If myresult.StartsWith("tt") Then
                                    newmovie.fullmoviebody.imdbid = myresult
                                Else
                                    newmovie.fullmoviebody.tmdbid = myresult
                                    myresult = nothing
                                End If
                                Dim testAttribute2 as XmlAttribute = CType(thisresult.Attributes.GetNamedItem("TMDB"),  XmlAttribute)
                                If testAttribute2 IsNot Nothing Then
                                    newmovie.fullmoviebody.tmdbid = testAttribute2.InnerText
                                End If
                            End If
                        Case "tmdbid"
                            newmovie.fullmoviebody.tmdbid = thisresult.InnerText 
                        Case "playcount"
                            newmovie.fullmoviebody.playcount = thisresult.InnerText
                        Case "watched"
                            watched = thisresult.InnerXml
                        Case "lastplayed"
                            newmovie.fullmoviebody.lastplayed = thisresult.InnerText
                        ''' to compensate for Kodi 17 exported nfo's
                        Case "ratings"
                            Dim what As XmlNode = Nothing
                            For Each what In thisresult.ChildNodes
                                Select Case what.name
                                    Case "rating"
                                        If what.Attributes("name").Value = "default" Then
                                            Dim what2 As XmlNode = Nothing
                                            For each what2 In what.ChildNodes
                                                Select Case what2.Name
                                                    Case "value"
                                                        newmovie.fullmoviebody.rating = what2.InnerText.ToRating.ToString
                                                    Case "votes"
                                                        Dim vote As String = what2.InnerText
                                                        If Not String.IsNullOrEmpty(vote) Then vote = vote.Replace(",", "")
                                                        newmovie.fullmoviebody.votes = vote
                                                End Select
                                            Next
                                        End If
                                End Select
                            Next
                        Case "rating"
                            Dim y As String = thisresult.InnerText
                            newmovie.fullmoviebody.rating = thisresult.InnerText.ToRating.ToString
                        Case "userrating"
                            newmovie.fullmoviebody.usrrated = thisresult.InnerText
                        Case "metascore"
                            newmovie.fullmoviebody.metascore = thisresult.InnerText
                        Case "top250"
                            newmovie.fullmoviebody.top250 = thisresult.InnerText
                        Case "createdate"
                            newmovie.fileinfo.createdate = thisresult.InnerText
                        Case "showlink"
                            newmovie.fullmoviebody.showlink = thisresult.InnerText 
                        Case "LockedFields"
                            newmovie.fullmoviebody.LockedFields = thisresult.InnerText.Split(",").ToList()
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
                                    Case "order"
                                        newactor.order = detail.InnerText
                                End Select
                            Next
                            newmovie.listactors.Add(newactor)
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
                                                                newfilenfo.Video.Width.Value = videodetails.InnerText
                                                            Case "height"
                                                                newfilenfo.Video.Height.Value = videodetails.InnerText
                                                            Case "aspect"
                                                                newfilenfo.Video.Aspect.Value = Utilities.FixIntlAspectRatio(videodetails.InnerText)
                                                            Case "codec"
                                                                newfilenfo.Video.Codec.Value = videodetails.InnerText
                                                            Case "format"
                                                                If newfilenfo.Video.FormatInfo.Value = "" Then
                                                                    newfilenfo.Video.FormatInfo.Value = videodetails.InnerText
                                                                End If
                                                            Case "formatinfo"
                                                                newfilenfo.Video.FormatInfo.Value = videodetails.InnerText
                                                            Case "durationinseconds"
                                                                newfilenfo.Video.DurationInSeconds.Value = videodetails.InnerText
                                                            Case "bitrate"
                                                                newfilenfo.Video.Bitrate.Value = videodetails.InnerText
                                                            Case "bitratemode"
                                                                newfilenfo.Video.BitrateMode.Value = videodetails.InnerText
                                                            Case "bitratemax"
                                                                newfilenfo.Video.BitrateMax.Value = videodetails.InnerText
                                                            Case "container"
                                                                newfilenfo.Video.Container.Value = videodetails.InnerText
                                                            Case "codecid"
                                                                newfilenfo.Video.CodecId.Value = videodetails.InnerText
                                                            Case "codecidinfo"
                                                                newfilenfo.Video.CodecInfo.Value = videodetails.InnerText
                                                            Case "scantype"
                                                                newfilenfo.Video.ScanType.Value = videodetails.InnerText
                                                        End Select
                                                    Next
                                                Case "audio"
                                                    Dim audiodetails As XmlNode = Nothing
                                                    Dim audio As New AudioDetails
                                                    For Each audiodetails In detail.ChildNodes
                                                        Select Case audiodetails.Name
                                                            Case "language"
                                                                audio.Language.Value = audiodetails.InnerText
                                                            Case "DefaultTrack"
                                                                audio.DefaultTrack.Value = audiodetails.InnerText
                                                            Case "codec"
                                                                audio.Codec.Value = audiodetails.InnerText
                                                            Case "channels"
                                                                audio.Channels.Value = audiodetails.InnerText
                                                            Case "bitrate"
                                                                audio.Bitrate.Value = audiodetails.InnerText
                                                        End Select
                                                    Next
                                                    newfilenfo.Audio.Add(audio)
                                                Case "subtitle"
                                                    Dim subsdetails As XmlNode = Nothing
                                                    Dim sublang As New SubtitleDetails
                                                    For Each subsdetails In detail.ChildNodes
                                                        Select Case subsdetails.Name
                                                            Case "language"
                                                                sublang.Language.Value  = subsdetails.InnerText
                                                            Case "default"
                                                                sublang.Primary         = subsdetails.InnerXml
                                                            Case "forced"
                                                                sublang.Forced          = subsdetails.InnerXml
                                                        End Select
                                                    Next
                                                    newfilenfo.Subtitles.Add(sublang)
                                            End Select
                                        Next
                                        If newfilenfo.Audio.Count = 0 Then
                                            Dim audio As New AudioDetails
                                            newfilenfo.Audio.Add(audio)
                                        End If
                                        If newfilenfo.Subtitles.Count = 0 Then
                                            Dim subtitle As New SubtitleDetails
                                            newfilenfo.Subtitles.Add(subtitle)
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
                If watched Then newmovie.fullmoviebody.playcount = "1"
                newmovie.fileinfo.fullpathandfilename   = loadpath
                newmovie.fileinfo.filename              = Path.GetFileName(loadpath)
                newmovie.fileinfo.foldername            = Utilities.GetLastFolder(loadpath)
                If Path.GetFileName(loadpath).ToLower   = "video_ts.nfo" Or Path.GetFileName(loadpath).ToLower = "index.nfo" Then
                    newmovie.fileinfo.videotspath       = Utilities.RootVideoTsFolder(loadpath)
                Else
                    newmovie.fileinfo.videotspath = ""
                End If
                newmovie.fileinfo.rootfolder            = Pref.GetRootFolder(loadpath) & "\"
                newmovie.fileinfo.posterpath            = Pref.GetPosterPath(loadpath, newmovie.fileinfo.filename)
                newmovie.fileinfo.trailerpath           = ""
                newmovie.fileinfo.path                  = Path.GetDirectoryName(loadpath) & "\"
                newmovie.fileinfo.basepath              = Pref.GetMovBasePath(newmovie.fileinfo.path)
                newmovie.fileinfo.fanartpath            = Pref.GetFanartPath(loadpath, newmovie.fileinfo.filename)
                newmovie.fileinfo.movsetfanartpath      = Pref.GetMovSetFanartPath(loadpath, newmovie.fullmoviebody.SetName)
                newmovie.fileinfo.movsetposterpath      = Pref.GetMovSetPosterPath(loadpath, newmovie.fullmoviebody.SetName)

                If Not String.IsNullOrEmpty(newmovie.filedetails.Video.Container.Value) Then
                    Dim container As String             = newmovie.filedetails.Video.Container.Value
                    newmovie.fileinfo.filenameandpath   = loadpath.Replace(".nfo", container)
                End If
                
                If newmovie.fullmoviebody.SetName       = "" Then
                    newmovie.fullmoviebody.SetName      = "-None-"
                End If
                If newmovie.fullmoviebody.usrrated      = "" Then newmovie.fullmoviebody.usrrated = "0"
                movie = Nothing

                If Flag4resave Then mov_NfoSave(loadpath, newmovie, True)    ' Resave if field updated or to correct an nfo error.

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
        Dim doc As New XmlDocument
        Try
            If movietosave Is Nothing Then Exit Sub
            If Not File.Exists(filenameandpath) Or overwrite = True Then
                'Try
                
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
                Dim anotherchild As XmlElement = Nothing

                root = doc.CreateElement("movie")
                stage = 3
                
                child = doc.CreateElement("fileinfo")
                anotherchild = doc.CreateElement("streamdetails")
                filedetailschild = doc.CreateElement("video")
                If Pref.enablehdtags = True Then
                    stage = 4
                    stage = 16
                    anotherchild.AppendChild(AppendVideo(doc, movietosave.filedetails.Video))

                    stage = 17
                    For Each item In movietosave.filedetails.Audio
                        anotherchild.AppendChild(AppendAudio(doc, item))
                    Next
                    stage = 18
                    For Each entry In movietosave.filedetails.Subtitles
                        If Not String.IsNullOrEmpty(entry.Language.Value) Then
                            anotherchild.AppendChild(AppendSub(doc, entry))
                        End If
                    Next
                    stage = 19
                Else
                    stage = 20
                    Dim container As String = movietosave.filedetails.Video.Container.Value
                    filedetailschild.AppendChild(doc, "container", If(String.IsNullOrEmpty(container), "", container))
                    anotherchild.AppendChild(filedetailschild)
                End If

                stage = 21
                child.AppendChild(anotherchild)
                root.AppendChild(child)
                stage = 22
                root.AppendChild(doc, "title", movietosave.fullmoviebody.title)
                stage = 23
                root.AppendChild(doc, "originaltitle", If(String.IsNullOrEmpty(movietosave.fullmoviebody.originaltitle), movietosave.fullmoviebody.title, movietosave.fullmoviebody.originaltitle))

                stage = 24
                If Not Pref.NoAltTitle AndAlso movietosave.alternativetitles.Count > 0 Then
                    For Each title In movietosave.alternativetitles
                        If title <> movietosave.fullmoviebody.title Then
                            root.AppendChild(doc, "alternativetitle", title)
                        End If
                    Next
                End If
                stage = 25
                If movietosave.fullmoviebody.SetName <> "-None-" Then
                    root.AppendChild(doc, "set", movietosave.fullmoviebody.SetName)
				End If

				If movietosave.fullmoviebody.TmdbSetId <> "" Then
                    root.AppendChild(doc, "setid", movietosave.fullmoviebody.TmdbSetId)
                    If Pref.SetIdAsCollectionnumber Then root.AppendChild(doc, "collectionnumber", movietosave.fullmoviebody.TmdbSetId)
                End If

                stage = 26
                If String.IsNullOrEmpty(movietosave.fullmoviebody.sortorder) Then
                    movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                End If
                root.AppendChild(doc, "sorttitle"   , movietosave.fullmoviebody.sortorder)
                stage = 27
                root.AppendChild(doc, "year"        , movietosave.fullmoviebody.year)
                stage = 28
                root.AppendChild(doc, "premiered"   , movietosave.fullmoviebody.premiered)
                root.AppendChild(doc, "rating"      , movietosave.fullmoviebody.rating.ToRating.ToString("0.0", Form1.MyCulture))
                root.AppendChild(doc, "userrating"  , If(movietosave.fullmoviebody.usrrated = "", "0", movietosave.fullmoviebody.usrrated))
                root.AppendChild(doc, "metascore"   , If(movietosave.fullmoviebody.metascore = "", "0", movietosave.fullmoviebody.metascore))
                stage = 29

                If movietosave.fullmoviebody.LockedFields.Count>0 Then
                    child = doc.CreateElement("LockedFields")
                    child.InnerText = String.Join(",", movietosave.fullmoviebody.LockedFields.ToArray())
                    root.AppendChild(child)
                End If
                
                Dim votes As String = movietosave.fullmoviebody.votes
                If Not String.IsNullOrEmpty(votes) then
                    If Not Pref.MovThousSeparator Then
                        votes = votes.Replace(",", "")
                    Else
                        If Not votes.Contains(",") Then
                            If votes.Length > 3 Then
                                votes = votes.Insert(votes.Length-3, ",")
                            End If
                            If votes.Length > 7 Then
                                votes = votes.Insert(votes.Length-7, ",")
                            End If
                        End If
                    End If
                End If
                root.AppendChild(doc, "votes", votes)
                stage = 30
                root.AppendChild(doc, "top250", movietosave.fullmoviebody.top250)
                root.AppendChild(doc, "outline", movietosave.fullmoviebody.outline)
                stage = 31
                root.AppendChild(doc, "plot", movietosave.fullmoviebody.plot)
                stage = 32
                root.AppendChild(doc, "tagline", movietosave.fullmoviebody.tagline)
                stage = 33
                root.AppendChildList(doc, "country", movietosave.fullmoviebody.country)

                stage = 34
                Try
                    If Pref.XtraFrodoUrls AndAlso Pref.FrodoEnabled Then
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
                
                stage = 35
                Try
                    child = doc.CreateElement("runtime")
                    If Pref.MovDurationAsRuntine Then
                        Dim duration As String = movietosave.filedetails.Video.DurationInSeconds.Value
                        Dim durationInt As Integer = If(String.IsNullOrEmpty(duration), 0, duration.ToInt)
                        If durationInt > 60 Then
                            movietosave.fullmoviebody.runtime = Math.Floor(durationInt/60).ToString
                        End If
                    End If
                    If movietosave.fullmoviebody.runtime <> Nothing Then
                        Dim minutes As String = movietosave.fullmoviebody.runtime.ToMin
                        Try
                            If Not String.IsNullOrEmpty(minutes) AndAlso Convert.ToInt32(minutes) > 0 Then
                                Do While minutes.IndexOf("0") = 0 And minutes.Length > 0
                                    minutes = minutes.Substring(1, minutes.Length - 1)
                                Loop
                                If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Pref.roundminutes = True Then
                                    minutes = "0" & minutes
                                ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Pref.roundminutes = True Then
                                    minutes = "00" & minutes
                                End If
                            End If
                            If Pref.intruntime = False And IsNumeric(minutes) Then
                                If minutes = "0" AndAlso Not String.IsNullOrEmpty(movietosave.filedetails.Video.DurationInSeconds.Value) Then
                                    Dim seconds As Integer = Convert.ToInt32(movietosave.filedetails.Video.DurationInSeconds.Value)
                                    If seconds > 0 AndAlso seconds < 60 Then minutes = "1"
                                End If
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
                stage = 36
                root.AppendChild(doc, "mpaa", movietosave.fullmoviebody.mpaa)

                stage = 37
                If movietosave.fullmoviebody.genre <> "" Then
                    Dim strArr2() As String
                    strArr2 = movietosave.fullmoviebody.genre.Split("/")
                    For count = 0 To strArr2.Length - 1
                        child = doc.CreateElement("genre")
                        strArr2(count) = strArr2(count).Trim
                        child.InnerText = strArr2(count)
                        root.AppendChild(child)
                    Next
                End If

                stage = 38
                If movietosave.fullmoviebody.tag.Count <> 0 Then
                    root.AppendChildList(doc, "tag", movietosave.fullmoviebody.tag)
                End If

                stage = 39
                root.AppendChildList(doc, "credits", movietosave.fullmoviebody.credits)

                stage = 40
                root.AppendChildList(doc, "director", movietosave.fullmoviebody.director)

                stage = 41
                root.AppendChildList(doc, "studio", movietosave.fullmoviebody.studio)
                
                stage = 42
                root.AppendChild(doc, "trailer", movietosave.fullmoviebody.trailer)

                stage = 43
                root.AppendChild(doc, "playcount", movietosave.fullmoviebody.playcount)
                If Pref.MovNfoWatchTag Then
                    'child = doc.CreateElement("watched")    : child.InnerXml = If(movietosave.fullmoviebody.playcount = "0", False, True)
                    'root.AppendChild(child)
                    root.AppendChild(doc, "watched", If(movietosave.fullmoviebody.playcount = "0", False, True))
                End If

                stage = 44
                root.AppendChild(doc, "lastplayed", movietosave.fullmoviebody.lastplayed)

                stage = 45
                If Not String.IsNullOrEmpty(movietosave.fullmoviebody.imdbid) Then
                    root.AppendChild(doc, "id", movietosave.fullmoviebody.imdbid)
                End If
                stage = 46
                If Not String.IsNullOrEmpty(movietosave.fullmoviebody.tmdbid) Then
                    root.AppendChild(doc, "tmdbid", movietosave.fullmoviebody.tmdbid)
                End If
                stage = 47
                If Not String.IsNullOrEmpty(movietosave.fullmoviebody.source) Then
                    root.AppendChild(doc, "videosource", movietosave.fullmoviebody.source)
                End If
                stage = 48
                root.AppendChild(doc, "showlink", movietosave.fullmoviebody.showlink)

                stage = 49
                root.AppendChild(doc, "createdate", SetCreateDate(movietosave.fileinfo.createdate))

                stage = 50
                root.AppendChild(doc, "stars", movietosave.fullmoviebody.stars)
                stage = 51
                Dim actorstosave As Integer = movietosave.listactors.Count
                If actorstosave > Pref.maxactors Then actorstosave = Pref.maxactors
                For f = 0 To actorstosave - 1
                    child = doc.CreateElement("actor")
                    child.AppendChild(doc, "id"     , movietosave.listactors(f).actorid      )
                    child.AppendChild(doc, "name"   , movietosave.listactors(f).actorname    )
                    child.AppendChild(doc, "role"   , movietosave.listactors(f).actorrole    )
                    child.AppendChild(doc, "thumb"  , movietosave.listactors(f).actorthumb   )
                    child.AppendChild(doc, "order"  , movietosave.listactors(f).order        )
                    root.AppendChild(child)
                Next

                doc.AppendChild(root)
                stage = 52
                Try
                    stage = 53
                    SaveXMLDoc(doc, filenameandpath)
                    stage = 54
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
        doc = Nothing
    End Sub
#End Region
    
    '  All HomeMovie Load/Save Routines
#Region " Home Movie Routines "
    Public Function nfoLoadHomeMovie(ByVal filepath As String)
        Try
            Dim newmovie As New HomeMovieDetails
            newmovie.fileinfo.fullpathandfilename = filepath
            If Not File.Exists(filepath) Then
                Return "Error"
            Else
                Dim movie As New XmlDocument
                Try
                    Using tmpstrm As IO.StreamReader = File.OpenText(filepath)
                        movie.Load(tmpstrm)
                    End Using
                Catch ex As Exception
                    If Not util_NfoValidate(filepath, True) Then
                        newmovie.fullmoviebody.title = "ERROR"
                        Return "ERROR"
                    End If
                    Return (newmovie)
                End Try

                Dim thisresult As XmlNode = Nothing
                For Each thisresult In movie("movie")
                    Try
                        Select Case thisresult.Name
                            Case "title"
                                Dim tempstring As String = ""
                                tempstring = thisresult.InnerText
                                newmovie.fullmoviebody.title = Pref.RemoveIgnoredArticles(tempstring)
                            Case "set"          : newmovie.fullmoviebody.movieset = thisresult.InnerText
                            Case "stars"        : newmovie.fullmoviebody.stars = thisresult.InnerText
                            Case "year"         : newmovie.fullmoviebody.year = thisresult.InnerText
                            Case "plot"         : newmovie.fullmoviebody.plot = thisresult.InnerText
                            Case "playcount"    : newmovie.fullmoviebody.playcount = thisresult.InnerText
                            Case "sorttitle"    : newmovie.fullmoviebody.sortorder = thisresult.InnerText
                            Case "runtime"      : newmovie.fullmoviebody.runtime = thisresult.InnerText
                                If IsNumeric(newmovie.fullmoviebody.runtime) Then
                                    newmovie.fullmoviebody.runtime = newmovie.fullmoviebody.runtime & " min"
                                End If
                            Case "fileinfo"
                                Dim what As XmlNode = Nothing
                                For Each res In thisresult.ChildNodes
                                    Select Case res.name
                                        Case "streamdetails"
                                            Dim newfilenfo As New StreamDetails
                                            Dim detail As XmlNode = Nothing
                                            For Each detail In res.ChildNodes
                                                Select Case detail.Name
                                                    Case "video"
                                                        Dim videodetails As XmlNode = Nothing
                                                        For Each videodetails In detail.ChildNodes
                                                            Select Case videodetails.Name
                                                                Case "width"
                                                                    newfilenfo.Video.Width.Value = videodetails.InnerText
                                                                Case "height"
                                                                    newfilenfo.Video.Height.Value = videodetails.InnerText
                                                                Case "aspect"
                                                                    newfilenfo.Video.Aspect.Value = Utilities.FixIntlAspectRatio(videodetails.InnerText)
                                                                Case "codec"
                                                                    newfilenfo.Video.Codec.Value = videodetails.InnerText
                                                                Case "format"
                                                                    If newfilenfo.Video.FormatInfo.Value = "" Then
                                                                        newfilenfo.Video.FormatInfo.Value = videodetails.InnerText
                                                                    End If
                                                                Case "formatinfo"
                                                                    newfilenfo.Video.FormatInfo.Value = videodetails.InnerText
                                                                Case "durationinseconds"
                                                                    newfilenfo.Video.DurationInSeconds.Value = videodetails.InnerText
                                                                Case "bitrate"
                                                                    newfilenfo.Video.Bitrate.Value = videodetails.InnerText
                                                                Case "bitratemode"
                                                                    newfilenfo.Video.BitrateMode.Value = videodetails.InnerText
                                                                Case "bitratemax"
                                                                    newfilenfo.Video.BitrateMax.Value = videodetails.InnerText
                                                                Case "container"
                                                                    newfilenfo.Video.Container.Value = videodetails.InnerText
                                                                Case "codecid"
                                                                    newfilenfo.Video.CodecId.Value = videodetails.InnerText
                                                                Case "codecidinfo"
                                                                    newfilenfo.Video.CodecInfo.Value = videodetails.InnerText
                                                                Case "scantype"
                                                                    newfilenfo.Video.ScanType.Value = videodetails.InnerText
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
                                                        newfilenfo.Audio.Add(audio)
                                                    Case "subtitle"
                                                        Dim subsdetails As XmlNode = Nothing
                                                        For Each subsdetails In detail.ChildNodes
                                                            Select Case subsdetails.Name
                                                                Case "language"
                                                                    Dim sublang As New SubtitleDetails
                                                                    sublang.Language.Value = subsdetails.InnerText
                                                                    newfilenfo.Subtitles.Add(sublang)
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

                If newmovie.fullmoviebody.title         = Nothing Then newmovie.fullmoviebody.title = "ERR - This Movie Has No TITLE!"
                newmovie.fullmoviebody.filename         = Path.GetFileName(filepath)
                If newmovie.fullmoviebody.playcount     = Nothing Then newmovie.fullmoviebody.playcount = "0"
                If newmovie.fullmoviebody.plot          = Nothing Then newmovie.fullmoviebody.plot = ""
                If newmovie.fullmoviebody.runtime       = Nothing Then newmovie.fullmoviebody.runtime = ""
                If newmovie.fullmoviebody.sortorder     = Nothing Or newmovie.fullmoviebody.sortorder = "" Then newmovie.fullmoviebody.sortorder = newmovie.fullmoviebody.title

                If newmovie.fullmoviebody.year          = Nothing Then newmovie.fullmoviebody.year = "1901"
                newmovie.fileinfo.fullpathandfilename   = filepath
                newmovie.fileinfo.filename              = Path.GetFileName(filepath)
                newmovie.fileinfo.foldername            = Utilities.GetLastFolder(filepath)
                newmovie.fileinfo.posterpath            = Pref.GetPosterPath(filepath, newmovie.fileinfo.filename)
                newmovie.fileinfo.trailerpath           = ""
                newmovie.fileinfo.rootfolder            = Pref.GetRootFolder(filepath) & "\"
                newmovie.fileinfo.path                  = Path.GetDirectoryName(filepath) & "\"
                newmovie.fileinfo.basepath              = Pref.GetMovBasePath(newmovie.fileinfo.path)
                newmovie.fileinfo.fanartpath            = Pref.GetFanartPath(filepath, newmovie.fileinfo.filename)
                If Not String.IsNullOrEmpty(newmovie.filedetails.Video.Container.Value) Then
                    Dim container As String             = newmovie.filedetails.Video.Container.Value
                    newmovie.fileinfo.filenameandpath   = filepath.Replace(".nfo", container)
                End If
                
                movie = Nothing
                Return newmovie
            End If

        Catch
        End Try
        Return "Error"

    End Function

    Public Sub nfoSaveHomeMovie(ByVal filenameandpath As String, ByVal homemovietosave As HomeMovieDetails, Optional ByVal overwrite As Boolean = True)

        If homemovietosave Is Nothing Then Exit Sub
        If Not File.Exists(filenameandpath) Or overwrite = True Then
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

            If Pref.enablehdtags = True Then
                child = doc.CreateElement("fileinfo")
                anotherchild = doc.CreateElement("streamdetails")
                anotherchild.AppendChild(AppendVideo(doc, homemovietosave.filedetails.Video))
                
                For Each item In homemovietosave.filedetails.Audio
                    anotherchild.AppendChild(AppendAudio(doc, item))
                Next
                
                For Each entry In homemovietosave.filedetails.Subtitles
                    If Not String.IsNullOrEmpty(entry.Language.Value) Then
                        anotherchild.AppendChild(AppendSub(doc, entry))
                    End If
                Next
                child.AppendChild(anotherchild)
                root.AppendChild(child)
            End If
            
            child = doc.CreateElement("title") : child.InnerText = homemovietosave.fullmoviebody.title
            root.AppendChild(child)
            root.AppendChild(doc, "set", homemovietosave.fullmoviebody.movieset)

            If String.IsNullOrEmpty(homemovietosave.fullmoviebody.sortorder) Then
                homemovietosave.fullmoviebody.sortorder = homemovietosave.fullmoviebody.title
            End If
            root.AppendChild(doc, "sorttitle", homemovietosave.fullmoviebody.sortorder)
            root.AppendChild(doc, "year", homemovietosave.fullmoviebody.year)
            root.AppendChild(doc, "plot", homemovietosave.fullmoviebody.plot)
            
            Dim minutes As String = homemovietosave.fullmoviebody.runtime.ToMin
            If homemovietosave.fullmoviebody.runtime <> Nothing AndAlso homemovietosave.fullmoviebody.runtime <> "0" Then
                Try
                    Do While minutes.IndexOf("0") = 0 And minutes.Length > 0
                        minutes = minutes.Substring(1, minutes.Length - 1)
                    Loop
                    If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Pref.roundminutes = True Then
                        minutes = "0" & minutes
                    ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Pref.roundminutes = True Then
                        minutes = "00" & minutes
                    End If
                    If Pref.intruntime = False And IsNumeric(minutes) Then
                        minutes = minutes & " min"
                    End If
                Catch
                    minutes = homemovietosave.fullmoviebody.runtime
                End Try
            End If
            root.AppendChild(doc, "runtime"     , minutes)
            root.AppendChild(doc, "playcount"   , homemovietosave.fullmoviebody.playcount)
            root.AppendChild(doc, "createdate"  , SetCreateDate(homemovietosave.fileinfo.createdate))
            root.AppendChild(doc, "stars"       , homemovietosave.fullmoviebody.stars)
            doc.AppendChild(root)
            SaveXMLDoc(doc, filenameandpath)
        Else
            MsgBox("File already exists")
        End If
    End Sub
#End Region

    '  All Music Video Load/Save Routines
#Region " Music Video Routines "
    Public Shared Sub MVsaveNfo(ByVal filenameandpath As String, ByVal musicvidtosave As FullMovieDetails)
        Dim doc As New XmlDocument
        Dim thumbnailstring As String = ""
        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement = Nothing
        Dim child As XmlElement = Nothing
        Dim anotherchild As XmlElement = Nothing

        root = doc.CreateElement("musicvideo")
        child = doc.CreateElement("fileinfo")
        anotherchild = doc.CreateElement("streamdetails")
        anotherchild.AppendChild(AppendVideo(doc, musicvidtosave.filedetails.Video))

        For Each item In musicvidtosave.filedetails.Audio
            anotherchild.AppendChild(AppendAudio(doc, item))
        Next

        For each item In musicvidtosave.filedetails.Subtitles
            anotherchild.AppendChild(AppendSub(doc, item))
        Next

        child.AppendChild(anotherchild)
        root.AppendChild(child)
        
        root.AppendChild(doc, "tmdbid", musicvidtosave.fullmoviebody.tmdbid)
        root.AppendChild(doc, "title", musicvidtosave.fullmoviebody.title)
        root.AppendChild(doc, "year", musicvidtosave.fullmoviebody.year)
        root.AppendChild(doc, "artist", musicvidtosave.fullmoviebody.artist)
        root.AppendChild(doc, "director", musicvidtosave.fullmoviebody.director)
        root.AppendChild(doc, "album", musicvidtosave.fullmoviebody.album)
        root.AppendChild(doc, "genre", musicvidtosave.fullmoviebody.genre)
        root.AppendChild(doc, "thumb", musicvidtosave.fullmoviebody.thumb)
        root.AppendChild(doc, "track", musicvidtosave.fullmoviebody.track)

        If musicvidtosave.fullmoviebody.runtime = "Unknown" Then
            Try
                Dim seconds As Integer = Convert.ToInt32(musicvidtosave.filedetails.Video.DurationInSeconds.Value)
                Dim hms = TimeSpan.FromSeconds(seconds)
                Dim h = hms.Hours.ToString
                Dim m = hms.Minutes.ToString
                Dim s = hms.Seconds.ToString
                If s.Length = 1 Then s = "0" & s
                Dim runtime As String
                runtime = h & ":" & m & ":" & s
                If h = "0" Then runtime = m & ":" & s
                If h = "0" And m = "0" Then runtime = s
                musicvidtosave.fullmoviebody.runtime = runtime
            Catch
            End Try
        End If
        root.AppendChild(doc, "runtime", musicvidtosave.fullmoviebody.runtime)
        root.AppendChild(doc, "plot", musicvidtosave.fullmoviebody.plot)
        root.AppendChild(doc, "studio", musicvidtosave.fullmoviebody.studio)
        root.AppendChild(doc, "createdate", SetCreateDate(musicvidtosave.fileinfo.createdate))
        doc.AppendChild(root)
        
        Try
            SaveXMLDoc(doc, filenameandpath)
        Catch
        End Try
    End Sub

    Public Shared Function MVloadNfo(ByVal filePath)
        Dim NewMusicVideo As New FullMovieDetails 
        NewMusicVideo.fileinfo.fullPathAndFilename = filePath
        Dim document As New XmlDocument
        Using tmpstrm As IO.StreamReader = File.OpenText(filepath)
            document.Load(tmpstrm)
        End Using
        Dim thisresult As XmlNode = Nothing
        Dim newfilenfo As New StreamDetails
        For Each thisresult In document("musicvideo")
            Select Case thisresult.Name
                Case "album"        : NewMusicVideo.fullmoviebody.album     = thisresult.InnerText
                Case "tmdbid"       : NewMusicVideo.fullmoviebody.tmdbid    = thisresult.InnerText
                Case "title"        : NewMusicVideo.fullmoviebody.title     = thisresult.InnerText
                Case "year"         : NewMusicVideo.fullmoviebody.year      = thisresult.InnerText
                Case "artist"       : NewMusicVideo.fullmoviebody.artist    = thisresult.InnerText
                Case "director"     : NewMusicVideo.fullmoviebody.director  = thisresult.InnerText
                Case "genre"        : NewMusicVideo.fullmoviebody.genre     = thisresult.InnerText
                Case "thumb"        : NewMusicVideo.fullmoviebody.thumb     = thisresult.InnerText
                Case "track"        : NewMusicVideo.fullmoviebody.track     = thisresult.InnerText
                Case "runtime"      : NewMusicVideo.fullmoviebody.runtime   = thisresult.InnerText
                Case "plot"         : NewMusicVideo.fullmoviebody.plot      = thisresult.InnerText
                Case "studio"       : NewMusicVideo.fullmoviebody.studio    = thisresult.InnerText
                Case "createdate"   : NewMusicVideo.fileinfo.createdate     = thisresult.InnerText
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
                                                        newfilenfo.Video.Width.Value = videodetails.InnerText
                                                    Case "height"
                                                        newfilenfo.Video.Height.Value = videodetails.InnerText
                                                    Case "aspect"
                                                        newfilenfo.Video.Aspect.Value = Utilities.FixIntlAspectRatio(videodetails.InnerText)
                                                    Case "codec"
                                                        newfilenfo.Video.Codec.Value = videodetails.InnerText
                                                    Case "format"
                                                        If newfilenfo.Video.FormatInfo.Value = "" Then
                                                            newfilenfo.Video.FormatInfo.Value = videodetails.InnerText
                                                        End If
                                                    Case "formatinfo"
                                                        newfilenfo.Video.FormatInfo.Value = videodetails.InnerText
                                                    Case "durationinseconds"
                                                        newfilenfo.Video.DurationInSeconds.Value = videodetails.InnerText
                                                    Case "bitrate"
                                                        newfilenfo.Video.Bitrate.Value = videodetails.InnerText
                                                    Case "bitratemode"
                                                        newfilenfo.Video.BitrateMode.Value = videodetails.InnerText
                                                    Case "bitratemax"
                                                        newfilenfo.Video.BitrateMax.Value = videodetails.InnerText
                                                    Case "container"
                                                        newfilenfo.Video.Container.Value = videodetails.InnerText
                                                    Case "codecid"
                                                        newfilenfo.Video.CodecId.Value = videodetails.InnerText
                                                    Case "codecidinfo"
                                                        newfilenfo.Video.CodecInfo.Value = videodetails.InnerText
                                                    Case "scantype"
                                                        newfilenfo.Video.ScanType.Value = videodetails.InnerText
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
                                            newfilenfo.Audio.Add(audio)
                                        Case "subtitle"
                                            Dim subsdetails As XmlNode = Nothing
                                            For Each subsdetails In detail.ChildNodes
                                                Select Case subsdetails.Name
                                                    Case "language"
                                                        Dim sublang As New SubtitleDetails
                                                        sublang.Language.Value = subsdetails.InnerText
                                                        newfilenfo.Subtitles.Add(sublang)
                                                End Select
                                            Next
                                    End Select
                                Next
                                If newfilenfo.Audio.Count = 0 Then
                                    Dim audio As New AudioDetails
                                    newfilenfo.Audio.Add(audio)
                                End If
                                NewMusicVideo.filedetails = newfilenfo
                        End Select
                    Next
            End Select
        Next
        NewMusicVideo.fileinfo.fullpathandfilename  = filepath
        NewMusicVideo.fileinfo.filename             = Path.GetFileName(filepath).Replace(".nfo", NewMusicVideo.filedetails.Video.Container.Value)
        NewMusicVideo.fileinfo.foldername           = Utilities.GetLastFolder(filepath)
        NewMusicVideo.fileinfo.posterpath           = Pref.GetPosterPath(filepath, NewMusicVideo.fileinfo.filename)
        NewMusicVideo.fileinfo.trailerpath          = ""
        NewMusicVideo.fileinfo.rootfolder           = Pref.GetRootFolder(filePath) & "\"
        NewMusicVideo.fileinfo.path                 = Path.GetDirectoryName(filepath) & "\"
        NewMusicVideo.fileinfo.basepath             = Pref.GetMovBasePath(NewMusicVideo.fileinfo.path)
        NewMusicVideo.fileinfo.fanartpath           = Pref.GetFanartPath(filepath, NewMusicVideo.fileinfo.filename)
        If Not String.IsNullOrEmpty(NewMusicVideo.filedetails.Video.Container.Value) Then
            Dim container As String = NewMusicVideo.filedetails.Video.Container.Value
            NewMusicVideo.fileinfo.filenameandpath  = filepath.Replace(".nfo", container)
        Else
            NewMusicVideo.fileinfo.filenameandpath = Utilities.GetFileName(filepath,True)
        End If
        Return NewMusicVideo
    End Function
#End Region

End Class



