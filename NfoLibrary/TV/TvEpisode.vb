Imports ProtoXML
Imports Media_Companion


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
                For Each Item As String In Utilities.VideoExtensions
                    If IO.File.Exists(_PureName & Item) Then
                        _VideoFilePath = _PureName & Item
                        Me.MediaExtension = Item
                        Exit For
                    End If
                Next
            Else
                'If IO.File.Exists(_PureName & Me.MediaExtension) Then
                _VideoFilePath = _PureName & Me.MediaExtension
                'End If
            End If

            Me.Thumbnail.Path = _PureName & ".tbn"

            ' Me.EditAttribute("PureName", Me.PureName)
            Me.EditAttribute("MediaExtension", Me.MediaExtension)
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
    Public Property PlayCount As New ProtoProperty(Me, "playcount")
    Public Property LastPlayed As New ProtoProperty(Me, "lastplayed")

    Public Property Credits As New ProtoProperty(Me, "credits")
    Public Property Director As New ProtoProperty(Me, "director")
    Public Property Premiered As New ProtoProperty(Me, "premiered")
    Public Property Status As New ProtoProperty(Me, "status", CacheMode:=CacheMode.Both)
    Public Property Aired As New ProtoProperty(Me, "aired")
    Public Property Studio As New ProtoProperty(Me, "studio")
    Public Property Trailer As New ProtoProperty(Me, "trailer")
    Public Property Genre As New ProtoProperty(Me, "genre")
    Private Property Missing As New ProtoProperty(Me, "missing", CacheMode:=CacheMode.Both)

    Public Property ImdbId As New ProtoProperty(Me, "imdbid")
    Public Property TvdbId As New ProtoProperty(Me, "tvdbid")

    Public Property ShowId As New ProtoProperty(Me, "ShowId", CacheMode:=CacheMode.Both)

    'TODO: Should be a list, used for multiple episodes per file
    Public Property EpBookmark As New ProtoProperty(Me, "epbookmark")


    Public Property Details As New FileInfo(Me, "fileinfo")

    Public Property ListActors As New ActorList(Me, "actor")

    Public Property SeasonObj As TvSeason
    Public Property ShowObj As TvShow

    Public Property Thumbnail As New ProtoImage(Me, "thumbnail", Utilities.DefaultFanartPath)

    Public Sub AbsorbTvdbEpisode(ByRef TvdbEpisode As Tvdb.Episode)
        Me.Title.Value = TvdbEpisode.EpisodeName.Value
        Me.Rating.Value = TvdbEpisode.Rating.Value
        Me.Plot.Value = TvdbEpisode.Overview.Value
        Me.Director.Value = TvdbEpisode.Director.Value
        Me.ImdbId.Value = TvdbEpisode.ImdbId.Value
        Me.MpaaCert.Value = TvdbEpisode.ProductionCode.Value
        Me.TvdbId.Value = TvdbEpisode.Id.Value
        Me.Season.Value = TvdbEpisode.SeasonNumber.Value
        Me.Episode.Value = TvdbEpisode.EpisodeNumber.Value
        Me.Thumbnail.Url = TvdbEpisode.ScreenShotUrl
        Aired.Value = TvdbEpisode.FirstAired.Value ' Phyonics - Fix for issue #208
        Me.UpdateTreenode()
    End Sub

    Public Property IsMissing As Boolean
        Get
            Missing.SurpressAlters = True
            If Missing.Value Is Nothing Then
                Missing.Value = Boolean.FalseString
            End If
            If Missing.Value = Boolean.TrueString Then
                Me.EpisodeNode.ForeColor = Drawing.Color.Blue
            Else
                Me.EpisodeNode.ForeColor = Drawing.Color.Black
            End If
            Return CBool(_Missing.Value)
        End Get
        Set(ByVal value As Boolean)
            Missing.Value = CStr(value)
            If Missing.Value = Boolean.TrueString Then
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
        Me.GetFileDetails(Me.VideoFilePath)
    End Sub

    Public Sub GetfileDetails(ByVal filename As String)
        If Not IO.File.Exists(filename) Then
            Exit Sub
        End If

        Me.IsAltered = True

        If IO.Path.GetFileName(filename).ToLower = "video_ts.ifo" Then
            Dim temppath As String = filename.Replace(IO.Path.GetFileName(filename), "VTS_01_0.IFO")
            If IO.File.Exists(temppath) Then
                filename = temppath
            End If
        End If

        Dim playlist As New List(Of String)
        Dim tempstring As String
        tempstring = Utilities.GetFileName(filename)
        playlist = Utilities.GetMediaList(tempstring)


        Dim MI As New mediainfo
        'MI = New mediainfo
        MI.Open(filename)
        Dim curVS As Integer = 0
        Dim addVS As Boolean = False
        Dim numOfVideoStreams As Integer = MI.Count_Get(StreamKind.Visual)

        Dim tempmediainfo As String
        Dim tempmediainfo2 As String

        Me.Details.StreamDetails.Video.Width.Value = MI.Get_(StreamKind.Visual, curVS, "Width")
        Me.Details.StreamDetails.Video.Height.Value = MI.Get_(StreamKind.Visual, curVS, "Height")
        If Me.Details.StreamDetails.Video.Width <> Nothing Then
            If IsNumeric(Me.Details.StreamDetails.Video.Width) Then
                If Me.Details.StreamDetails.Video.Height <> Nothing Then
                    If IsNumeric(Me.Details.StreamDetails.Video.Height) Then
                        '                            Dim tempwidth As Integer = Convert.ToInt32(Me.Details.StreamDetails.Video.width)
                        '                            Dim tempheight As Integer = Convert.ToInt32(Me.Details.StreamDetails.Video.height)
                        '                            Dim aspect As Decimal
                        '                                aspect = tempwidth / tempheight  'Next three line are wrong for getting display aspect ratio
                        '                                aspect = FormatNumber(aspect, 3)
                        '                                If aspect > 0 Then Me.Details.StreamDetails.Video.aspect = aspect.ToString

                        Dim Information As String = MI.Inform
                        Dim BeginString As Integer = Information.ToLower.IndexOf(":", Information.ToLower.IndexOf("display aspect ratio"))
                        Dim EndString As Integer = Information.ToLower.IndexOf("frame rate")
                        Dim SizeofString As Integer = EndString - BeginString
                        Dim DisplayAspectRatio As String = Information.Substring(BeginString, SizeofString).Trim(" ", ":", Chr(10), Chr(13))
                        'DisplayAspectRatio = DisplayAspectRatio.Substring(0, Len(DisplayAspectRatio) - 1)
                        If Len(DisplayAspectRatio) > 0 Then
                            Me.Details.StreamDetails.Video.Aspect.Value = DisplayAspectRatio
                        Else
                            Me.Details.StreamDetails.Video.Aspect.Value = "Unknown"
                        End If


                    End If
                End If
            End If
        End If
        'Me.Details.StreamDetails.Video.aspect = MI.Get_(StreamKind.Visual, 0, 79)


        tempmediainfo = MI.Get_(StreamKind.Visual, curVS, "Format")
        If tempmediainfo.ToLower = "avc" Then
            tempmediainfo2 = "h264"
        Else
            tempmediainfo2 = tempmediainfo
        End If

        'Me.Details.StreamDetails.Video.codec = tempmediainfo2
        'Me.Details.StreamDetails.Video.formatinfo = tempmediainfo
        Me.Details.StreamDetails.Video.Codec.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID")
        If Me.Details.StreamDetails.Video.Codec.Value = "DX50" Then
            Me.Details.StreamDetails.Video.Codec.Value = "DIVX"
        End If
        '_MPEG4/ISO/AVC
        If Me.Details.StreamDetails.Video.Codec.Value.ToLower.IndexOf("mpeg4/iso/avc") <> -1 Then
            Me.Details.StreamDetails.Video.Codec.Value = "h264"
        End If
        Me.Details.StreamDetails.Video.FormatInfo.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID")
        Dim fs(100) As String
        For f = 1 To 100
            fs(f) = MI.Get_(StreamKind.Visual, 0, f)
        Next

        Try
            If playlist.Count = 1 Then
                Me.Details.StreamDetails.Video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, 61)
            ElseIf playlist.Count > 1 Then
                Dim totalmins As Integer = 0
                For f = 0 To playlist.Count - 1
                    Dim M2 As mediainfo
                    M2 = New mediainfo
                    M2.Open(playlist(f))
                    Dim temptime As String = M2.Get_(StreamKind.Visual, 0, 61)
                    Dim tempint As Integer
                    If temptime <> Nothing Then
                        Try
                            '1h 24mn 48s 546ms
                            Dim hours As Integer = 0
                            Dim minutes As Integer = 0
                            Dim tempstring2 As String = temptime
                            tempint = tempstring2.IndexOf("h")
                            If tempint <> -1 Then
                                hours = Convert.ToInt32(tempstring2.Substring(0, tempint))
                                tempstring2 = tempstring2.Substring(tempint + 1, tempstring2.Length - (tempint + 1))
                                tempstring2 = Trim(tempstring2)
                            End If
                            tempint = tempstring2.IndexOf("mn")
                            If tempint <> -1 Then
                                minutes = Convert.ToInt32(tempstring2.Substring(0, tempint))
                            End If
                            If hours <> 0 Then
                                hours = hours * 60
                            End If
                            minutes = minutes + hours
                            totalmins = totalmins + minutes
                        Catch
                        End Try
                    End If
                Next
                Me.Details.StreamDetails.Video.DurationInSeconds.Value = totalmins & " min"
            End If
        Catch
            Me.Details.StreamDetails.Video.DurationInSeconds.Value = MI.Get_(StreamKind.Visual, 0, 57)
        End Try
        Me.Details.StreamDetails.Video.Bitrate.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate/String")
        Me.Details.StreamDetails.Video.BitrateMode.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Mode/String")

        Me.Details.StreamDetails.Video.BitrateMax.Value = MI.Get_(StreamKind.Visual, curVS, "BitRate_Maximum/String")

        tempmediainfo = IO.Path.GetExtension(filename) '"This is the extension of the file"
        Me.Details.StreamDetails.Video.Container.Value = tempmediainfo
        'Me.Details.StreamDetails.Video.codecid = MI.Get_(StreamKind.Visual, curVS, "CodecID")

        Me.Details.StreamDetails.Video.CodecInfo.Value = MI.Get_(StreamKind.Visual, curVS, "CodecID/Info")
        Me.Details.StreamDetails.Video.ScanType.Value = MI.Get_(StreamKind.Visual, curVS, 102)
        'Video()
        'Format                     : MPEG-4 Visual
        'Format profile             : Streaming Video@L1
        'Format(settings, BVOP)     : Yes()
        'Format(settings, QPel)     : No()
        'Format(settings, GMC)      : No(warppoints)
        'Format(settings, Matrix)   : Custom()
        'Codec(ID)                  : XVID()
        'Codec(ID / Hint)           : XviD()
        'Duration                   : 1h 33mn
        'Bit rate                   : 903 Kbps
        'Width                      : 528 pixels
        'Height                     : 272 pixels
        'Display aspect ratio       : 1.941
        'Frame rate                 : 25.000 fps
        'Resolution                 : 24 bits
        'Colorimetry                : 4:2:0
        'Scan(Type)                 : Progressive()
        'Bits/(Pixel*Frame)         : 0.252
        'Stream size                : 604 MiB (86%)
        'Writing library            : XviD 1.0.3 (UTC 2004-12-20)

        Dim numOfAudioStreams As Integer = MI.Count_Get(StreamKind.Audio)
        Dim curAS As Integer = 0
        Dim addAS As Boolean = False

        'get audio data
        If numOfAudioStreams > 0 Then
            While curAS < numOfAudioStreams
                Dim audio As New AudioDetails
                audio.Language.Value = Utilities.GetLangCode(MI.Get_(StreamKind.Audio, curAS, "Language/String"))
                If MI.Get_(StreamKind.Audio, curAS, "Format") = "MPEG Audio" Then
                    audio.Codec.Value = "MP3"
                Else
                    audio.Codec.Value = MI.Get_(StreamKind.Audio, curAS, "Format")
                End If
                If audio.Codec.Value = "AC-3" Then
                    audio.Codec.Value = "AC3"
                End If
                If audio.Codec.Value = "DTS" Then
                    audio.Codec.Value = "dca"
                End If
                audio.Channels.Value = MI.Get_(StreamKind.Audio, curAS, "Channel(s)")
                audio.Bitrate.Value = MI.Get_(StreamKind.Audio, curAS, "BitRate/String")
                Me.Details.StreamDetails.Audio.Add(audio)
                curAS += 1
            End While
        End If


        Dim numOfSubtitleStreams As Integer = MI.Count_Get(StreamKind.Text)
        Dim curSS As Integer = 0
        If numOfSubtitleStreams > 0 Then
            While curSS < numOfSubtitleStreams
                Dim sublanguage As New SubtitleDetails
                sublanguage.Language.Value = Utilities.GetLangCode(MI.Get_(StreamKind.Text, curSS, "Language/String"))
                Me.Details.StreamDetails.Subtitles.Add(sublanguage)
                curSS += 1
            End While
        End If


    End Sub

    Public Overrides Function ToString() As String
        Return Me.Title.Value
    End Function


End Class

