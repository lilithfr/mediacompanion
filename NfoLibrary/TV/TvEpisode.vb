Imports ProtoXML
Imports Media_Companion
Imports MediaInfoNET


Public Class TvEpisode
    Inherits ProtoFile

    Public Sub New()
        MyBase.New("episodedetails")
    End Sub

    Private _PureName As String
    Protected Property PureName As String
        Get
            Return _PureName
        End Get
        Set(ByVal value As String)
            _PureName = value
            MyBase.NfoFilePath = value & ".nfo"
            If String.IsNullOrEmpty(Me.MediaExtension) Then
                ''  disable videofilepath to speed up cache as increases loading time.
                If Not IsMissing Then
                    If String.IsNullOrEmpty(EpExtn.Value) Then
                        For Each Item As String In Utilities.VideoExtensions
                            If IO.File.Exists(_PureName & Item) Then
                                _VideoFilePath = _PureName & Item
                                EpExtn.value = Item
                                Me.MediaExtension = Item
                                Exit For
                            End If
                        Next
                    Else
                        _VideoFilePath = _PureName & EpExtn.Value 
                    End If
                End If
            Else
                'If IO.File.Exists(_PureName & Me.MediaExtension) Then
                _VideoFilePath = _PureName & Me.MediaExtension
                'End If
            End If

            Me.Thumbnail.Path = _PureName & If(Preferences.FrodoEnabled, "-thumb.jpg", ".tbn")

            ' Me.EditAttribute("PureName", Me.PureName)
            ' Me.EditAttribute("MediaExtension", Me.MediaExtension)
        End Set
    End Property

    Public Shadows Property NfoFilePath As String
        Get
            Return MyBase.NfoFilePath
        End Get
        Set(ByVal value As String)
            Me.PureName = value.Replace(IO.Path.GetExtension(value), "")
            MyBase.NfoFilePath = value
        End Set
    End Property

    Private _VideoFilePath As String
    Public Property VideoFilePath As String
        Get
            Return _VideoFilePath
        End Get
        Set(ByVal value As String)
            Me.MediaExtension = IO.Path.GetExtension(value)
            Me.PureName = value.Replace(Me.MediaExtension, "")
            _VideoFilePath = value
        End Set
    End Property

    Public Property MediaExtension As String



    Public Property Id As New ProtoProperty(Me, "id", CacheMode:=CacheMode.Both)

    Public Property Title As New ProtoProperty(Me, "title", CacheMode:=CacheMode.Both)
    Public Property Rating As New ProtoProperty(Me, "rating")

    Public Property Year As New ProtoProperty(Me, "year")
    Public Property Top250 As New ProtoProperty(Me, "top250")
    Public Property Season As New ProtoProperty(Me, "season", CacheMode:=CacheMode.Both)
    Public Property Episode As New ProtoProperty(Me, "episode", CacheMode:=CacheMode.Both)
    Public Property DisplaySeason As New ProtoProperty(Me, "displayseason")
    Public Property DisplayEpisode As New ProtoProperty(Me, "displayepisode")
    Public Property Votes As New ProtoProperty(Me, "votes")
    Public Property Plot As New ProtoProperty(Me, "plot")
    Public Property TagLine As New ProtoProperty(Me, "tagline")
    Public Property Runtime As New ProtoProperty(Me, "runtime")
    Public Property MpaaCert As New ProtoProperty(Me, "mpaa")
    Public Property PlayCount As New ProtoProperty(Me, "playcount", CacheMode:=CacheMode.Both)
    Public Property LastPlayed As New ProtoProperty(Me, "lastplayed")

    Public Property Credits As New ProtoProperty(Me, "credits")
    Public Property Director As New ProtoProperty(Me, "director")
    Public Property Premiered As New ProtoProperty(Me, "premiered")
    Public Property Status As New ProtoProperty(Me, "status", CacheMode:=CacheMode.Both)
    Public Property Aired As New ProtoProperty(Me, "aired", CacheMode:=CacheMode.Both)
    Public Property Studio As New ProtoProperty(Me, "studio")
    Public Property Trailer As New ProtoProperty(Me, "trailer")
    Public Property Genre As New ProtoProperty(Me, "genre")
    Public Property Missing As New ProtoProperty(Me, "missing", CacheMode:=CacheMode.Both)
    Public Property EpExtn As New ProtoProperty(Me, "epextn", CacheMode:=CacheMode.Both)  'Storing in tvcache so we don't have to search for correct extension every
                                                                                          'time cache is loaded.
    Public Property ImdbId As New ProtoProperty(Me, "imdbid")
    Public Property TvdbId As New ProtoProperty(Me, "tvdbid")
    Public Property UniqueId As New ProtoProperty(Me, "uniqueid", CacheMode:=CacheMode.Both)
    Public Property ShowId As New ProtoProperty(Me, "ShowId", CacheMode:=CacheMode.Both)

    'TODO: Should be a list, used for multiple episodes per file
    Public Property EpBookmark As New ProtoProperty(Me, "epbookmark")


    Public Property Details As New FileInfo(Me, "fileinfo")

    Public Property ListActors As New ActorList(Me, "actor")
    Public Property Source As New ProtoProperty(Me, "videosource")

    Public Property SeasonObj As TvSeason
    Private Property _showObj As TvShow
    Public Property ShowObj As TvShow
        Get
            Return _showObj
        End Get
        Set(ByVal value As TvShow)
            _showObj = value
            ShowId.Value = value.TvdbId.Value
            TvdbId.Value = value.TvdbId.Value
            ImdbId.Value = value.ImdbId.Value
        End Set
    End Property

    Public Property Thumbnail As New ProtoImage(Me, "thumbnail", Utilities.DefaultFanartPath)

    Public Sub AbsorbTvdbEpisode(ByRef TvdbEpisode As Tvdb.Episode)
        Me.TvdbId.Value = TvdbEpisode.Id.Value
        Me.ImdbId.Value = TvdbEpisode.ImdbId.Value
        Me.Title.Value = TvdbEpisode.EpisodeName.Value
        Me.UniqueId.Value = TvdbEpisode.Id.Value
        Me.Rating.Value = TvdbEpisode.Rating.Value
        Me.Plot.Value = TvdbEpisode.Overview.Value
        Me.Director.Value = cleanvalue(TvdbEpisode.Director.Value)
        Me.Credits.Value = cleanvalue(TvdbEpisode.Credits.Value)
        Me.MpaaCert.Value = TvdbEpisode.ProductionCode.Value
        Me.Season.Value = TvdbEpisode.SeasonNumber.Value
        Me.Episode.Value = TvdbEpisode.EpisodeNumber.Value
        Me.Thumbnail.Url = TvdbEpisode.ScreenShotUrl
        Me.Source.Value = TvdbEpisode.Source.Value
        Aired.Value = TvdbEpisode.FirstAired.Value ' Phyonics - Fix for issue #208
        Me.UpdateTreenode()
    End Sub

    Public Sub AbsorbTvEpisode(ByRef TvEp As TvEpisode)
        Me.TvdbId.Value = TvEp.Id.Value
        Me.ImdbId.Value = TvEp.ImdbId.Value
        Me.Title.Value = TvEp.Title.Value
        Me.UniqueId.Value = TvEp.Id.Value
        Me.Rating.Value = TvEp.Rating.Value
        Me.Plot.Value = TvEp.Plot.Value
        Me.Director.Value = cleanvalue(TvEp.Director.Value)
        Me.Credits.Value = cleanvalue(TvEp.Credits.Value)
        Me.MpaaCert.Value = TvEp.MpaaCert.Value
        Me.Season.Value = TvEp.Season.Value
        Me.Episode.Value = TvEp.Episode.Value
        Me.Source.Value = TvEp.Source.Value
        Me.Aired.Value = TvEp.Aired.Value ' Phyonics - Fix for issue #208
        Me.ListActors = TvEp.ListActors
        Me.Details.StreamDetails = TvEp.Details.StreamDetails 
        Me.EpBookmark.Value = TvEp.EpBookmark.Value 
        Me.PlayCount.Value = TvEp.PlayCount.Value
        Me.Runtime.Value = TvEp.Runtime.Value
        Me.Source.Value = TvEp.Source.Value 
        Me.UpdateTreenode()
    End Sub

    Public Property IsMissing As Boolean
        Get
            Missing.SurpressAlters = True
            If Missing.Value Is Nothing Then
                Missing.Value = Boolean.FalseString
            End If
            'If Missing.Value.ToLower = Boolean.TrueString.ToLower Then
            '    Me.EpisodeNode.ForeColor = Drawing.Color.Blue
            'Else
            '    Me.EpisodeNode.ForeColor = Drawing.Color.Black
            'End If
            Return CBool(_Missing.Value)
        End Get
        Set(ByVal value As Boolean)
            Missing.Value = CStr(value)
            If Missing.Value.ToLower = Boolean.TrueString.ToLower Then
                Me.EpisodeNode.ForeColor = Drawing.Color.Blue
            Else
                Me.EpisodeNode.ForeColor = Drawing.Color.Black
            End If
        End Set
    End Property

    Private _Visible As Boolean
    Public Property Visible As Boolean
        Get
            If _Visible Then
                If Not Me.SeasonObj.SeasonNode.Nodes.Contains(Me.EpisodeNode) Then
                    Me.SeasonObj.SeasonNode.Nodes.Add(Me.EpisodeNode)

                End If
            Else
                Me.SeasonObj.SeasonNode.Nodes.Remove(Me.EpisodeNode)
            End If
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
            If _Visible Then
                If Not Me.SeasonObj.SeasonNode.Nodes.Contains(Me.EpisodeNode) Then
                    Me.SeasonObj.SeasonNode.Nodes.Add(Me.EpisodeNode)

                End If
            Else
                Me.SeasonObj.SeasonNode.Nodes.Remove(Me.EpisodeNode)
            End If

        End Set
    End Property

    Public Sub GetFileDetails()
        Dim fileStreamDetails As FullFileDetails = Preferences.Get_HdTags(Me.VideoFilePath)
        If Not IsNothing(fileStreamDetails) Then
            Me.Details.StreamDetails.Video = fileStreamDetails.filedetails_video
            Me.Details.StreamDetails.Audio.Clear()
            For Each audioStream In fileStreamDetails.filedetails_audio
                Me.Details.StreamDetails.Audio.Add(audioStream)
            Next
            Me.Details.StreamDetails.Subtitles.Clear()
            For Each lan In fileStreamDetails.filedetails_subtitles
                Me.Details.StreamDetails.Subtitles.Add(lan)
            Next

            If Not Me.Details.StreamDetails.Video.DurationInSeconds.Value Is Nothing Then
                Dim tempstring As String = ""
                tempstring = Me.Details.StreamDetails.Video.DurationInSeconds.Value
                If tempstring <> "" Then
                    If Preferences.intruntime Then
                        Me.Runtime.Value = Math.Round(tempstring / 60).ToString
                    Else
                        Me.Runtime.Value = Math.Round(tempstring / 60).ToString & " min"
                    End If
                Else
                    Me.Runtime.Value = tempstring
                End If
            End If
        End If
        'Me.GetFileDetails(Me.VideoFilePath)
    End Sub

    'Public Sub GetfileDetails(ByVal filename As String)
    '    If Not IO.File.Exists(filename) Then
    '        Exit Sub
    '    End If

    '    Me.IsAltered = True

    '    If IO.Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
    '        Dim temppath As String = filename.Replace(IO.Path.GetFileName(filename), "VTS_01_0.IFO")
    '        If IO.File.Exists(temppath) Then
    '            filename = temppath
    '        End If
    '    End If

    '    Dim playlist As New List(Of String)
    '    Dim tempstring As String
    '    tempstring = Utilities.GetFileName(filename)
    '    playlist = Utilities.GetMediaList(tempstring)


    '    Dim MI As New mediainfo
    '    MI.Open(filename)
    '    Dim curVS As Integer = 0
    '    Dim addVS As Boolean = False
    '    Dim numOfVideoStreams As Integer = MI.Count_Get(StreamKind.Visual)
    '    Dim aviFile As MediaFile = New MediaFile(filename)

    '    Dim tempmediainfo As String
    '    Dim tempmediainfo2 As String

    '    Me.Details.StreamDetails.Video.Width.Value = MI.Get_(StreamKind.Visual, curVS, "Width")
    '    Me.Details.StreamDetails.Video.Height.Value = MI.Get_(StreamKind.Visual, curVS, "Height")
    '    If Me.Details.StreamDetails.Video.Width <> Nothing Then
    '        If IsNumeric(Me.Details.StreamDetails.Video.Width) Then
    '            If Me.Details.StreamDetails.Video.Height <> Nothing Then
    '                If IsNumeric(Me.Details.StreamDetails.Video.Height) Then
    '                    Dim Information As String = MI.Inform
    '                    Dim BeginString As Integer = Information.ToLower.IndexOf(":", Information.ToLower.IndexOf("display aspect ratio"))
    '                    Dim EndString As Integer = Information.ToLower.IndexOf("frame rate")
    '                    Dim SizeofString As Integer = EndString - BeginString
    '                    Dim DisplayAspectRatio As String = Information.Substring(BeginString, SizeofString).Trim(" ", ":", Chr(10), Chr(13))
    '                    If Len(DisplayAspectRatio) > 0 Then
    '                        Me.Details.StreamDetails.Video.Aspect.Value = DisplayAspectRatio
    '                    Else
    '                        Me.Details.StreamDetails.Video.Aspect.Value = "Unknown"
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Try
    '        tempmediainfo = aviFile.Video(0).Format
    '    Catch
    '        tempmediainfo=""
    '    End Try 
    '    If tempmediainfo.ToLower = "avc" Then
    '        tempmediainfo2 = "h264"
    '    Else
    '        tempmediainfo2 = tempmediainfo
    '    End If

    '    Me.Details.StreamDetails.Video.Codec.Value = tempmediainfo2 
    '    If Me.Details.StreamDetails.Video.Codec.Value = "DX50" Then
    '        Me.Details.StreamDetails.Video.Codec.Value = "DIVX"
    '    End If
    '    '_MPEG4/ISO/AVC
    '    If Me.Details.StreamDetails.Video.Codec.Value.ToLower.IndexOf("mpeg4/iso/avc") <> -1 Then
    '        Me.Details.StreamDetails.Video.Codec.Value = "h264"
    '    End If

    '    Try
    '        tempmediainfo=aviFile.Video(0).CodecID 
    '    Catch 
    '        tempmediainfo=""
    '    End Try
    '    Me.Details.StreamDetails.Video.FormatInfo.Value = tempmediainfo
    '    Dim fs(100) As String
    '    For f = 1 To 100
    '        fs(f) = MI.Get_(StreamKind.Visual, 0, f)
    '    Next

    '    Try
    '        If playlist.Count = 1 Then
    '            Try
    '                Me.Details.StreamDetails.Video.DurationInSeconds.Value = Math.Round(Convert.ToInt32(MI.Get_(StreamKind.Visual, 0, "Duration"))/1000)
    '            Catch
    '                Me.Details.StreamDetails.Video.DurationInSeconds.Value = Math.Round(Convert.ToInt32(MI.Get_(StreamKind.Visual, 0, "Duration"))/1000)
    '            End Try
    '        End If
    '    Catch
    '        Me.Details.StreamDetails.Video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, 57)
    '    End Try
    '    Me.Details.StreamDetails.Video.Bitrate.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate/String")
    '    Me.Details.StreamDetails.Video.BitrateMode.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Mode/String")

    '    Me.Details.StreamDetails.Video.BitrateMax.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Maximum/String")

    '    tempmediainfo = IO.Path.GetExtension(filename) '"This is the extension of the file"
    '    Me.Details.StreamDetails.Video.Container.Value = tempmediainfo

    '    Me.Details.StreamDetails.Video.CodecInfo.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID/Info")
    '    Me.Details.StreamDetails.Video.ScanType.Value = MI.Get_(StreamKind.Visual, curVS, 102)
    '    'Video()
    '    'Format                     : MPEG-4 Visual
    '    'Format profile             : Streaming Video@L1
    '    'Format(settings, BVOP)     : Yes()
    '    'Format(settings, QPel)     : No()
    '    'Format(settings, GMC)      : No(warppoints)
    '    'Format(settings, Matrix)   : Custom()
    '    'Codec(ID)                  : XVID()
    '    'Codec(ID / Hint)           : XviD()
    '    'Duration                   : 1h 33mn
    '    'Bit rate                   : 903 Kbps
    '    'Width                      : 528 pixels
    '    'Height                     : 272 pixels
    '    'Display aspect ratio       : 1.941
    '    'Frame rate                 : 25.000 fps
    '    'Resolution                 : 24 bits
    '    'Colorimetry                : 4:2:0
    '    'Scan(Type)                 : Progressive()
    '    'Bits/(Pixel*Frame)         : 0.252
    '    'Stream size                : 604 MiB (86%)
    '    'Writing library            : XviD 1.0.3 (UTC 2004-12-20)

    '    Dim numOfAudioStreams As Integer = MI.Count_Get(StreamKind.Audio)
    '    Dim curAS As Integer = 0
    '    Dim addAS As Boolean = False

    '    Dim tmpaud As String = ""
    '    'get audio data
    '    Me.Details.StreamDetails.Audio.Clear()
    '    If numOfAudioStreams > 0 Then
    '        While curAS < numOfAudioStreams
    '            Dim audio As New AudioDetails
    '            audio.Language.Value = Utilities.GetLangCode(MI.Get_(StreamKind.Audio, curAS, "Language/String"))
    '            If MI.Get_(StreamKind.Audio, curAS, "Format") = "MPEG Audio" Then
    '                audio.Codec.Value = "MP3"
    '            Else
    '                'audio.Codec.Value = MI.Get_(StreamKind.Audio, curAS, "Format")
    '                Try
    '                    tempmediainfo = aviFile.Audio(curAS).Format
    '                Catch
    '                    tempmediainfo = ""
    '                End Try
    '                audio.Codec.Value = tempmediainfo
    '            End If
    '            If audio.Codec.Value = "AC-3" Then
    '                audio.Codec.Value = "AC3"
    '            End If
    '            tmpaud = aviFile.Audio(curAS).FormatID.ToLower()
    '            If audio.Codec.Value = "DTS" Then
    '                If tmpaud = "dts ma / core" Then
    '                    audio.Codec.Value = "dtshd_ma"
    '                ElseIf tmpaud = "dts hra / core" Then
    '                    audio.Codec.Value = "dtshd_hra"
    '                Else
    '                    audio.Codec.Value = "DTS"
    '                End If
    '            End If
    '            audio.Channels.Value = MI.Get_(StreamKind.Audio, curAS, "Channel(s)")
    '            audio.Bitrate.Value = MI.Get_(StreamKind.Audio, curAS, "BitRate/String")
    '            Me.Details.StreamDetails.Audio.Add(audio)
    '            curAS += 1
    '        End While
    '    End If


    '    Dim numOfSubtitleStreams As Integer = MI.Count_Get(StreamKind.Text)
    '    Dim curSS As Integer = 0
    '    If numOfSubtitleStreams > 0 Then
    '        While curSS < numOfSubtitleStreams
    '            Dim sublanguage As New SubtitleDetails
    '            sublanguage.Language.Value = MI.Get_(StreamKind.Text, curSS, "Language")
    '            Me.Details.StreamDetails.Subtitles.Add(sublanguage)
    '            curSS += 1
    '        End While
    '    End If


    'End Sub

    Public Overrides Function ToString() As String
        Return Me.Title.Value
    End Function

    Public Function cleanvalue(s As String) As String
        If Not String.IsNullOrEmpty(s) Then
            s = s.TrimEnd("|")
            s = s.TrimStart("|")
            s = s.Replace("|", " / ")
        End If
        Return s
    End Function
End Class

