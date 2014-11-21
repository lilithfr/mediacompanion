Imports ProtoXML
Namespace Tvdb
    Public Class Episode
        Inherits ProtoPropertyGroup

        '<id>332179</id>
        Public Property Id As New ProtoProperty(Me, "id")
        '<DVD_chapter></DVD_chapter>
        Public Property DvdChapter As New ProtoProperty(Me, "DVD_chapter")
        '<DVD_discid></DVD_discid>
        Public Property DvdDiscId As New ProtoProperty(Me, "DVD_discid")
        '<DVD_episodenumber></DVD_episodenumber>
        Public Property DvdEpisodeNumber As New ProtoProperty(Me, "DVD_episodenumber")
        '<DVD_season></DVD_season>
        Public Property DvdSeason As New ProtoProperty(Me, "DVD_season")
        '<Director>|Joseph McGinty Nichol|</Director>
        Public Property Director As New ProtoProperty(Me, "Director")
        '<Credits>Bob Geldof</Credits>
        Public Property Credits As New ProtoProperty(Me, "Credits")
        '<EpisodeName>Chuck Versus the World</EpisodeName>
        Public Property EpisodeName As New ProtoProperty(Me, "EpisodeName")
        '<EpisodeNumber>1</EpisodeNumber>
        Public Property EpisodeNumber As New ProtoProperty(Me, "EpisodeNumber")
        '<FirstAired>2007-09-24</FirstAired>
        Public Property FirstAired As New ProtoProperty(Me, "FirstAired")
        '<GuestStars>|Julia Ling|Vik Sahay|Mieko Hillman|</GuestStars>
        Public Property GuestStars As New ProtoProperty(Me, "GuestStars")
        '<IMDB_ID></IMDB_ID>
        Public Property ImdbId As New ProtoProperty(Me, "IMDB_ID")
        '<Language>English</Language>
        Public Property Language As New ProtoProperty(Me, "Language")
        '<Overview>Chuck Bartowski is an average computer geek...</Overview>
        Public Property Overview As New ProtoProperty(Me, "Overview")
        '<ProductionCode></ProductionCode>
        Public Property ProductionCode As New ProtoProperty(Me, "ProductionCode")
        '<Rating>9.0</Rating>
        Public Property Rating As New ProtoProperty(Me, "Rating")
        '<SeasonNumber>1</SeasonNumber>
        Public Property SeasonNumber As New ProtoProperty(Me, "SeasonNumber")
        '<Writer>|Josh Schwartz|Chris Fedak|</Writer>
        Public Property Writer As New ProtoProperty(Me, "Writer")
        '<absolute_number></absolute_number>
        Public Property AbsoluteNumber As New ProtoProperty(Me, "absolute_number")
        '<airsafter_season></airsafter_season>
        Public Property AirsAfterSeason As New ProtoProperty(Me, "airsafter_season")
        '<airsbefore_episode></airsbefore_episode>
        Public Property AirsBeforeEpsisode As New ProtoProperty(Me, "airsbefore_episode")
        '<airsbefore_season></airsbefore_season>
        Public Property AirsBeforeSeason As New ProtoProperty(Me, "airsbefore_season")
        '<filename>episodes/80348-332179.jpg</filename>
        Private Property FileName As New ProtoProperty(Me, "filename")
        '<lastupdated>1201292806</lastupdated>
        Public Property LastUpdated As New ProtoProperty(Me, "lastupdate")
        '<seasonid>27985</seasonid>
        Public Property SeasonId As New ProtoProperty(Me, "seasonid")
        '<seriesid>80348</seriesid>
        Public Property SeriesId As New ProtoProperty(Me, "seriesid")
        '<videosource>bdrip</videosource>
        Public Property Source As New ProtoProperty(Me, "videosource")
        'Media Companion confirm load success
        Public Property FailedLoad As Boolean

        Public ReadOnly Property ScreenShotUrl As String
            Get
                Return "http://thetvdb.com/banners/_cache/" & Me.FileName.Value
            End Get
        End Property


        Public Overrides Function CreateNew() As ProtoXML.IProtoXChild
            Return New Episode
        End Function
    End Class
End Namespace