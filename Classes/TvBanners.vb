Public Class TvBanners

    Public Url As String
    Public SmallUrl As String
    Public BannerType As String
    Public Resolution As String
    Public Language As String
    Public Season As String

    Public Shared Widening Operator CType(ByVal Input As Tvdb.Banner) As TvBanners
        Dim Output As New TvBanners

        Dim Preamble As String = "http://thetvdb.com/banners/"

        Output.Url = Preamble & Input.BannerPath.Value
        Output.SmallUrl = Preamble & "_cache/" & Input.BannerPath.Value
        Output.BannerType = Input.Type
        Output.Resolution = Input.Width & "x" & Input.Height
        Output.Language = Input.Language.Value
        Output.Season = Input.Season.Value

        Return Output
    End Operator

End Class
