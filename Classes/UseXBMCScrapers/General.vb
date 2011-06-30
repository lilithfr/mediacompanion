﻿Imports System.Net
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Xml


Module General
    Const SetDefaults = True
    Dim TempEpisode As New TvEpisode
    Dim episodeInformation As New List(Of TvEpisode)
    Dim TempXMLEpisode As New TvEpisode
    Dim episodeXMLinformation As New List(Of TvEpisode)




#Region "Fields"
    Public mScraperManager As ScraperManager
    Public mLastQuery As ScraperQuery
    Public ParametersForScraper(10) As String
    Public FinalScrapResult As String
#End Region

#Region "Code Lines not Used...saved for Future Reference"
    'FinalScrapResult = DoScrape(Scraper, "CreateSearchUrl", ParametersForScraper, False)
    'FinalScrapResult = FinalScrapResult.Replace("<url>", "")
    'FinalScrapResult = FinalScrapResult.Replace("</url>", "")
    'FinalScrapResult = FinalScrapResult.Replace(" ", "%20")
    '' 2st stage
    'ParametersForScraper(0) = FinalScrapResult
    'ParametersForScraper(1) = FinalScrapResult
    'FinalScrapResult = DoScrape(Scraper, "GetSearchResults", ParametersForScraper, True)
    'Dim m_xmld As XmlDocument
    'Dim m_nodelist As XmlNodeList
    'Dim m_node As XmlNode
    'm_xmld = New XmlDocument()
    'm_xmld.LoadXml(FinalScrapResult)
    'm_nodelist = m_xmld.SelectNodes("/results/entity")
    'Dim GetDetailsURLS(2) As String
    'For Each m_node In m_nodelist
    '    Dim Title = m_node.ChildNodes.Item(0).InnerText
    '    Dim url = m_node.ChildNodes.Item(2).InnerText
    '    Dim id = m_node.ChildNodes.Item(3).InnerText
    '    GetDetailsURLS(0) = url.Substring(0, url.LastIndexOf("/"))
    '    GetDetailsURLS(0) = GetDetailsURLS(0).Substring(0, GetDetailsURLS(0).LastIndexOf("/")) & "/en.xml"
    '    GetDetailsURLS(1) = GetDetailsURLS(0).Substring(0, GetDetailsURLS(0).LastIndexOf("/")) & "/banners.xml"
    '    GetDetailsURLS(2) = GetDetailsURLS(0).Substring(0, GetDetailsURLS(0).LastIndexOf("/")) & "/actors.xml"
    '    ParametersForScraper(9) = id
    '    Exit For
    'Next


    'ParametersForScraper(4) = GetDetailsURLS(0)
    'GetDetailsURLS(0) = DoScrape(Scraper, "GetDetails", ParametersForScraper, True, 5)
    'ParametersForScraper(4) = GetDetailsURLS(1)
    'GetDetailsURLS(1) = DoScrape(Scraper, "GetDetails", ParametersForScraper, True, 5)
    'ParametersForScraper(4) = GetDetailsURLS(2)
    'GetDetailsURLS(2) = DoScrape(Scraper, "GetDetails", ParametersForScraper, True, 5)

    'GetDetailsURLS(0) = GetDetailsURLS(0).Substring(0, GetDetailsURLS(0).LastIndexOf("<fanart url="))
    'GetDetailsURLS(1) = GetDetailsURLS(1).Substring(GetDetailsURLS(1).IndexOf("<thumb>"), (GetDetailsURLS(1).LastIndexOf("</details>") - GetDetailsURLS(1).IndexOf("<thumb>")))
    'GetDetailsURLS(2) = GetDetailsURLS(2).Substring(GetDetailsURLS(2).IndexOf("<actor>"), (GetDetailsURLS(2).LastIndexOf("<fanart url=") - GetDetailsURLS(2).IndexOf("<actor>"))) &  "</details>"
    'Dim teste As String = GetDetailsURLS(0) & GetDetailsURLS(1) & GetDetailsURLS(2)
    'ParametersForScraper(0) = New WebClient().DownloadString("http://www.thetvdb.com/api/1D62F2F90030C444/series/82066/all/en.xml")
    'ParametersForScraper(1) = "http://www.thetvdb.com/api/1D62F2F90030C444/series/82066/all/en.zip"
    'ParametersForScraper(3) = GetDetailsURLS(0)
    'ParametersForScraper(4) = Nothing

#End Region

#Region "XBMC Scraper Internal Routines"

    Private Function RetrieveUrls(ByVal URLAddress As String) As String
        Dim Saida As String = ""
        Try

            Saida = New WebClient().DownloadString(URLAddress)
            Return Saida
        Catch ex As WebException
            MessageBox.Show("ERROR:  " + ex.Message + vbCrLf + vbCrLf + "URL: " + URLAddress, "Error retrieving URL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "ERROR" 'SK: added 
            'Throw ex   'SK : this was originally uncommented 
        End Try
    End Function
    Private Function ChooseScraper(ByVal ScraperIdentification As String) As Scraper
        Dim TempScraper As Scraper = Nothing
        For Each c In mScraperManager.Scrapers
            If c.ID = ScraperIdentification Then
                TempScraper = c
                Exit For
            End If
        Next
        Return TempScraper
    End Function
    Private Function DoScrape(ByVal ScraperName As String, ByVal WhatFunction As String, ByVal InputParameters() As String, ByVal GetURL As Boolean, Optional ByVal InputBuffer As Integer = 1)
        Dim ChoosedScraper As Scraper
        InputBuffer -= 1
        'Dim ScraperFunctions As List(Of System.Xml.Linq.XElement)
        Dim ScraperFunctiontoExecute As XElement
        If GetURL = True Then
            Dim PrimaryString As String = RetrieveUrls(InputParameters(InputBuffer))
            If PrimaryString = "ERROR" Then 'SK: added this test but calling functions don't test for "error"
                Return "Error"
                Exit Function
            End If
            InputParameters(InputBuffer) = PrimaryString 'SK: Primary string was returning as Nothing when the Throw Ex was commented out with a failed causing an exception
            If PrimaryString.Length = 0 Then 'SK: this test already existed
                Return "Error"
                Exit Function
            End If
        End If
        ChoosedScraper = ChooseScraper(ScraperName)
        'ScraperFunctions = ChoosedScraper.Functions
        ScraperFunctiontoExecute = ChoosedScraper.Func(WhatFunction)



        mLastQuery = New ScraperQuery(ChoosedScraper)
        Dim ScrapeResult As String = mLastQuery.Execute(ScraperFunctiontoExecute.Name.LocalName, InputParameters)
        If ScrapeResult.Length = 0 Then
            Return "Error"
            Exit Function
        End If
        Return ScrapeResult
    End Function

#End Region

#Region "Misc.Movies Routines"

    Public Function GetYearByFilename(ByVal filename As String, Optional ByVal withextension As Boolean = True)
        Dim cleanname As String = filename
        Try
            If withextension = True Then
                Try
                    cleanname = filename.Replace(IO.Path.GetExtension(cleanname), "")
                Catch
                End Try
            End If
            Dim movieyear As String
            Dim S As String = cleanname
            Dim M As Match
            M = Regex.Match(S, "(\([\d]{4}\))")
            If M.Success = True Then
                movieyear = M.Value
            Else
                movieyear = Nothing
            End If
            If movieyear = Nothing Then
                M = Regex.Match(S, "(\[[\d]{4}\])")
                If M.Success = True Then
                    movieyear = M.Value
                Else
                    movieyear = Nothing
                End If
            End If
            Try
                movieyear = movieyear.Trim
                If movieyear.Length = 6 Then
                    movieyear = movieyear.Remove(0, 1)
                    movieyear = movieyear.Remove(4, 1)
                End If
            Catch
            End Try
            Return movieyear
        Catch
        End Try
        Return "Error"
    End Function
    Public Function cleanfilename(ByVal filename As String, Optional ByVal withextension As Boolean = True)
        Dim cleanname As String = filename
        Try
            If withextension = True Then
                Try
                    cleanname = filename.Replace(IO.Path.GetExtension(cleanname), "")
                Catch
                End Try
            End If
            Dim movieyear As String
            Dim S As String = cleanname
            Dim M As Match
            M = Regex.Match(S, "(\([\d]{4}\))")
            If M.Success = True Then
                movieyear = M.Value
            Else
                movieyear = Nothing
            End If
            If movieyear = Nothing Then
                M = Regex.Match(S, "(\[[\d]{4}\])")
                If M.Success = True Then
                    movieyear = M.Value
                Else
                    movieyear = Nothing
                End If
            End If
            filename = cleanname
            Dim removal(75) As String
            removal(1) = "cd1"
            removal(2) = "cd.1"
            removal(3) = "cd_1"
            removal(4) = "cd 1"
            removal(5) = "cd-1"
            removal(6) = "dvd1"
            removal(7) = "dvd.1"
            removal(8) = "dvd_1"
            removal(9) = "dvd 1"
            removal(10) = "dvd-1"
            removal(11) = "part1"
            removal(12) = "part.1"
            removal(13) = "part_1"
            removal(14) = "part 1"
            removal(15) = "part-1"
            removal(16) = "disk1"
            removal(17) = "disk.1"
            removal(18) = "disk_1"
            removal(19) = "disk 1"
            removal(20) = "disk-1"
            removal(21) = "pt1"
            removal(22) = "pt.1"
            removal(23) = "pt_1"
            removal(24) = "pt 1"
            removal(25) = "pt-1"
            removal(26) = "ac3"
            removal(27) = "divx"
            removal(28) = "xvid"
            removal(29) = "dvdrip"
            removal(30) = "directors cut"
            removal(31) = "special edition"
            removal(32) = "screener"
            removal(33) = "telesync"
            removal(34) = "telecine"
            removal(35) = "director's cut"
            removal(36) = " r5"
            removal(37) = " scr"
            removal(38) = ".scr"
            removal(39) = "_scr"
            removal(40) = "-scr"
            removal(41) = " ts"
            removal(42) = "_ts"
            removal(43) = ".ts"
            removal(44) = "-ts"
            removal(45) = " fs"
            removal(46) = ".fs"
            removal(47) = "_fs"
            removal(48) = "-fs"
            removal(49) = " ws"
            removal(50) = ".ws"
            removal(51) = "_ws"
            removal(52) = "-ws"
            removal(53) = "-r5"
            removal(54) = "_r5"
            removal(55) = ".r5"
            removal(56) = "bluray"
            removal(57) = "720"
            removal(58) = "1024"
            removal(59) = "fullscreen"
            removal(60) = "widescreen"
            removal(61) = "dvdscr"
            removal(62) = "part01"
            removal(63) = "dvd5"
            removal(64) = "dvd9"
            removal(65) = "dvd 5"
            removal(66) = "dvd 9"
            removal(67) = "dvd-5"
            removal(68) = "dvd-9"
            removal(69) = "dvd_5"
            removal(70) = "dvd_9"
            removal(71) = "dvd.5"
            removal(72) = "dvd.9"
            removal(73) = "x264"
            removal(74) = "dts"
            removal(75) = "bluray"
            Dim currentposition As Integer = filename.Length
            Dim newposition As Integer = filename.Length
            For f = 1 To 75
                If filename.ToLower.IndexOf(removal(f)) <> -1 Then
                    newposition = filename.ToLower.IndexOf(removal(f))
                    If newposition < currentposition Then currentposition = newposition
                End If
            Next
            If movieyear <> Nothing Then
                If filename.IndexOf(movieyear) <> -1 Then
                    newposition = filename.IndexOf(movieyear)
                    If newposition < currentposition Then currentposition = newposition
                End If
            End If
            If currentposition < filename.Length And currentposition > 0 Then
                filename = filename.Substring(0, currentposition)
                If filename.Substring(filename.Length - 1, 1) = "-" Or filename.Substring(filename.Length - 1, 1) = "_" Or filename.Substring(filename.Length - 1, 1) = "." Or filename.Substring(filename.Length - 1, 1) = " " Then
                    filename = filename.Substring(0, filename.Length - 1)
                End If
            End If

            If filename <> "" Then
                cleanname = filename
            End If
            cleanname = Trim(cleanname)
            Return cleanname
        Catch ex As Exception
            cleanname = "error"
            Return cleanname
        Finally
        End Try
    End Function
    Public Function ReplaceCharactersinXML(ByVal Entrada As String) As String
        Dim StringOriginaltoXML As New XmlDocument
        Dim InicialTrailerPosition As Integer
        Dim FinalTrailerPosition As Integer
        Dim TempString As String = ""
        If Entrada.IndexOf("<trailer>") <> -1 Then
            InicialTrailerPosition = Entrada.IndexOf("<trailer>")
            FinalTrailerPosition = Entrada.LastIndexOf("</trailer>")
            TempString = Entrada.Substring(InicialTrailerPosition, (FinalTrailerPosition + 10 - InicialTrailerPosition))
            Entrada = Entrada.Remove(InicialTrailerPosition, (FinalTrailerPosition + 10 - InicialTrailerPosition))
            Entrada = ReplaceSpecialCharacters(Entrada)
            Entrada = Entrada.Insert(Entrada.LastIndexOf("</details>") - 1, TempString & Chr(13))
            Return Entrada

        Else
            Return ReplaceSpecialCharacters(Entrada)
        End If

    End Function
    Public Function ReplaceSpecialCharacters(ByVal filterstring As String) As String
        Try
            If filterstring.IndexOf("&#919;") <> -1 Then filterstring = filterstring.Replace("&#919;", "Η")
            If filterstring.IndexOf("&#918;") <> -1 Then filterstring = filterstring.Replace("&#918;", "Ζ")
            If filterstring.IndexOf("&#917;") <> -1 Then filterstring = filterstring.Replace("&#917;", "Ε")
            If filterstring.IndexOf("&#916;") <> -1 Then filterstring = filterstring.Replace("&#916;", "Δ")
            If filterstring.IndexOf("&#915;") <> -1 Then filterstring = filterstring.Replace("&#915;", "Γ")
            If filterstring.IndexOf("&#914;") <> -1 Then filterstring = filterstring.Replace("&#914;", "Β")
            If filterstring.IndexOf("&#913;") <> -1 Then filterstring = filterstring.Replace("&#913;", "Α")
            If filterstring.IndexOf("&#732;") <> -1 Then filterstring = filterstring.Replace("&#732;", "˜")
            If filterstring.IndexOf("&#710;") <> -1 Then filterstring = filterstring.Replace("&#710;", "ˆ")
            If filterstring.IndexOf("&#402;") <> -1 Then filterstring = filterstring.Replace("&#402;", "ƒ")
            If filterstring.IndexOf("&#376;") <> -1 Then filterstring = filterstring.Replace("&#376;", "Ÿ")
            If filterstring.IndexOf("&#353;") <> -1 Then filterstring = filterstring.Replace("&#353;", "š")
            If filterstring.IndexOf("&#352;") <> -1 Then filterstring = filterstring.Replace("&#352;", "Š")
            If filterstring.IndexOf("&#339;") <> -1 Then filterstring = filterstring.Replace("&#339;", "œ")
            If filterstring.IndexOf("&#338;") <> -1 Then filterstring = filterstring.Replace("&#338;", "Œ")
            If filterstring.IndexOf("&#937;") <> -1 Then filterstring = filterstring.Replace("&#937;", "Ω")
            If filterstring.IndexOf("&#936;") <> -1 Then filterstring = filterstring.Replace("&#936;", "Ψ")
            If filterstring.IndexOf("&#935;") <> -1 Then filterstring = filterstring.Replace("&#935;", "Χ")
            If filterstring.IndexOf("&#934;") <> -1 Then filterstring = filterstring.Replace("&#934;", "Φ")
            If filterstring.IndexOf("&#933;") <> -1 Then filterstring = filterstring.Replace("&#933;", "Υ")
            If filterstring.IndexOf("&#932;") <> -1 Then filterstring = filterstring.Replace("&#932;", "Τ")
            If filterstring.IndexOf("&#931;") <> -1 Then filterstring = filterstring.Replace("&#931;", "Σ")
            If filterstring.IndexOf("&#929;") <> -1 Then filterstring = filterstring.Replace("&#929;", "Ρ")
            If filterstring.IndexOf("&#928;") <> -1 Then filterstring = filterstring.Replace("&#928;", "Π")
            If filterstring.IndexOf("&#927;") <> -1 Then filterstring = filterstring.Replace("&#927;", "Ο")
            If filterstring.IndexOf("&#926;") <> -1 Then filterstring = filterstring.Replace("&#926;", "Ξ")
            If filterstring.IndexOf("&#924;") <> -1 Then filterstring = filterstring.Replace("&#924;", "Μ")
            If filterstring.IndexOf("&#923;") <> -1 Then filterstring = filterstring.Replace("&#923;", "Λ")
            If filterstring.IndexOf("&#922;") <> -1 Then filterstring = filterstring.Replace("&#922;", "Κ")
            If filterstring.IndexOf("&#921;") <> -1 Then filterstring = filterstring.Replace("&#921;", "Ι")
            If filterstring.IndexOf("&#920;") <> -1 Then filterstring = filterstring.Replace("&#920;", "Θ")
            If filterstring.IndexOf("&#955;") <> -1 Then filterstring = filterstring.Replace("&#955;", "λ")
            If filterstring.IndexOf("&#954;") <> -1 Then filterstring = filterstring.Replace("&#954;", "κ")
            If filterstring.IndexOf("&#953;") <> -1 Then filterstring = filterstring.Replace("&#953;", "ι")
            If filterstring.IndexOf("&#952;") <> -1 Then filterstring = filterstring.Replace("&#952;", "θ")
            If filterstring.IndexOf("&#951;") <> -1 Then filterstring = filterstring.Replace("&#951;", "η")
            If filterstring.IndexOf("&#950;") <> -1 Then filterstring = filterstring.Replace("&#950;", "ζ")
            If filterstring.IndexOf("&#949;") <> -1 Then filterstring = filterstring.Replace("&#949;", "ε")
            If filterstring.IndexOf("&#948;") <> -1 Then filterstring = filterstring.Replace("&#948;", "δ")
            If filterstring.IndexOf("&#947;") <> -1 Then filterstring = filterstring.Replace("&#947;", "γ")
            If filterstring.IndexOf("&#946;") <> -1 Then filterstring = filterstring.Replace("&#946;", "β")
            If filterstring.IndexOf("&#945;") <> -1 Then filterstring = filterstring.Replace("&#945;", "α")
            If filterstring.IndexOf("&#969;") <> -1 Then filterstring = filterstring.Replace("&#969;", "ω")
            If filterstring.IndexOf("&#968;") <> -1 Then filterstring = filterstring.Replace("&#968;", "ψ")
            If filterstring.IndexOf("&#967;") <> -1 Then filterstring = filterstring.Replace("&#967;", "χ")
            If filterstring.IndexOf("&#966;") <> -1 Then filterstring = filterstring.Replace("&#966;", "φ")
            If filterstring.IndexOf("&#965;") <> -1 Then filterstring = filterstring.Replace("&#965;", "υ")
            If filterstring.IndexOf("&#964;") <> -1 Then filterstring = filterstring.Replace("&#964;", "τ")
            If filterstring.IndexOf("&#963;") <> -1 Then filterstring = filterstring.Replace("&#963;", "σ")
            If filterstring.IndexOf("&#962;") <> -1 Then filterstring = filterstring.Replace("&#962;", "ς")
            If filterstring.IndexOf("&#961;") <> -1 Then filterstring = filterstring.Replace("&#961;", "ρ")
            If filterstring.IndexOf("&#960;") <> -1 Then filterstring = filterstring.Replace("&#960;", "π")
            If filterstring.IndexOf("&#959;") <> -1 Then filterstring = filterstring.Replace("&#959;", "ο")
            If filterstring.IndexOf("&#958;") <> -1 Then filterstring = filterstring.Replace("&#958;", "ξ")
            If filterstring.IndexOf("&#957;") <> -1 Then filterstring = filterstring.Replace("&#957;", "ν")
            If filterstring.IndexOf("&#956;") <> -1 Then filterstring = filterstring.Replace("&#956;", "μ")
            If filterstring.IndexOf("&#8240;") <> -1 Then filterstring = filterstring.Replace("&#8240;", "‰")
            If filterstring.IndexOf("&#8230;") <> -1 Then filterstring = filterstring.Replace("&#8230;", "…")
            If filterstring.IndexOf("&#8226;") <> -1 Then filterstring = filterstring.Replace("&#8226;", "•")
            If filterstring.IndexOf("&#8225;") <> -1 Then filterstring = filterstring.Replace("&#8225;", "‡")
            If filterstring.IndexOf("&#8224;") <> -1 Then filterstring = filterstring.Replace("&#8224;", "†")
            If filterstring.IndexOf("&#8222;") <> -1 Then filterstring = filterstring.Replace("&#8222;", "„")
            If filterstring.IndexOf("&#8218;") <> -1 Then filterstring = filterstring.Replace("&#8218;", "‚")
            If filterstring.IndexOf("&#8217;") <> -1 Then filterstring = filterstring.Replace("&#8217;", "’")
            If filterstring.IndexOf("&#8216;") <> -1 Then filterstring = filterstring.Replace("&#8216;", "‘")
            If filterstring.IndexOf("&#8212;") <> -1 Then filterstring = filterstring.Replace("&#8212;", "—")
            If filterstring.IndexOf("&#8211;") <> -1 Then filterstring = filterstring.Replace("&#8211;", "–")
            If filterstring.IndexOf("&#8805;") <> -1 Then filterstring = filterstring.Replace("&#8805;", "≥")
            If filterstring.IndexOf("&#8804;") <> -1 Then filterstring = filterstring.Replace("&#8804;", "≤")
            If filterstring.IndexOf("&#8801;") <> -1 Then filterstring = filterstring.Replace("&#8801;", "≡")
            If filterstring.IndexOf("&#8800;") <> -1 Then filterstring = filterstring.Replace("&#8800;", "≠")
            If filterstring.IndexOf("&#8776;") <> -1 Then filterstring = filterstring.Replace("&#8776;", "≈")
            If filterstring.IndexOf("&#8747;") <> -1 Then filterstring = filterstring.Replace("&#8747;", "∫")
            If filterstring.IndexOf("&#8745;") <> -1 Then filterstring = filterstring.Replace("&#8745;", "∩")
            If filterstring.IndexOf("&#8734;") <> -1 Then filterstring = filterstring.Replace("&#8734;", "∞")
            If filterstring.IndexOf("&#8730;") <> -1 Then filterstring = filterstring.Replace("&#8730;", "√")
            If filterstring.IndexOf("&#8721;") <> -1 Then filterstring = filterstring.Replace("&#8721;", "∑")
            If filterstring.IndexOf("&#8719;") <> -1 Then filterstring = filterstring.Replace("&#8719;", "∏")
            If filterstring.IndexOf("&#8706;") <> -1 Then filterstring = filterstring.Replace("&#8706;", "∂")
            If filterstring.IndexOf("&#8596;") <> -1 Then filterstring = filterstring.Replace("&#8596;", "↔")
            If filterstring.IndexOf("&#8595;") <> -1 Then filterstring = filterstring.Replace("&#8595;", "↓")
            If filterstring.IndexOf("&#8594;") <> -1 Then filterstring = filterstring.Replace("&#8594;", "→")
            If filterstring.IndexOf("&#8593;") <> -1 Then filterstring = filterstring.Replace("&#8593;", "↑")
            If filterstring.IndexOf("&#8592;") <> -1 Then filterstring = filterstring.Replace("&#8592;", "←")
            If filterstring.IndexOf("&#8482;") <> -1 Then filterstring = filterstring.Replace("&#8482;", "™")
            If filterstring.IndexOf("&#8364;") <> -1 Then filterstring = filterstring.Replace("&#8364;", "€")
            If filterstring.IndexOf("&#8260;") <> -1 Then filterstring = filterstring.Replace("&#8260;", "⁄")
            If filterstring.IndexOf("&#8254;") <> -1 Then filterstring = filterstring.Replace("&#8254;", "‾")
            If filterstring.IndexOf("&#8250;") <> -1 Then filterstring = filterstring.Replace("&#8250;", "›")
            If filterstring.IndexOf("&#8249;") <> -1 Then filterstring = filterstring.Replace("&#8249;", "‹")
            If filterstring.IndexOf("&#161;") <> -1 Then filterstring = filterstring.Replace("&#161;", Chr(161))
            If filterstring.IndexOf("&#162;") <> -1 Then filterstring = filterstring.Replace("&#162;", Chr(162))
            If filterstring.IndexOf("&#163;") <> -1 Then filterstring = filterstring.Replace("&#163;", Chr(163))
            If filterstring.IndexOf("&#164;") <> -1 Then filterstring = filterstring.Replace("&#164;", Chr(164))
            If filterstring.IndexOf("&#165;") <> -1 Then filterstring = filterstring.Replace("&#165;", Chr(165))
            If filterstring.IndexOf("&#166;") <> -1 Then filterstring = filterstring.Replace("&#166;", Chr(166))
            If filterstring.IndexOf("&#167;") <> -1 Then filterstring = filterstring.Replace("&#167;", Chr(167))
            If filterstring.IndexOf("&#168;") <> -1 Then filterstring = filterstring.Replace("&#168;", Chr(168))
            If filterstring.IndexOf("&#170;") <> -1 Then filterstring = filterstring.Replace("&#170;", Chr(170))
            If filterstring.IndexOf("&#171;") <> -1 Then filterstring = filterstring.Replace("&#171;", Chr(171))
            If filterstring.IndexOf("&#172;") <> -1 Then filterstring = filterstring.Replace("&#172;", Chr(172))
            If filterstring.IndexOf("&#173;") <> -1 Then filterstring = filterstring.Replace("&#173;", Chr(173))
            If filterstring.IndexOf("&#174;") <> -1 Then filterstring = filterstring.Replace("&#174;", Chr(174))
            If filterstring.IndexOf("&#175;") <> -1 Then filterstring = filterstring.Replace("&#175;", Chr(175))
            If filterstring.IndexOf("&#176;") <> -1 Then filterstring = filterstring.Replace("&#176;", Chr(176))
            If filterstring.IndexOf("&#177;") <> -1 Then filterstring = filterstring.Replace("&#177;", Chr(177))
            If filterstring.IndexOf("&#178;") <> -1 Then filterstring = filterstring.Replace("&#178;", Chr(178))
            If filterstring.IndexOf("&#179;") <> -1 Then filterstring = filterstring.Replace("&#179;", Chr(179))
            If filterstring.IndexOf("&#198;") <> -1 Then filterstring = filterstring.Replace("&#198;", Chr(198))
            If filterstring.IndexOf("&amp;") <> -1 Then filterstring = filterstring.Replace("&amp;", "&")
            If filterstring.IndexOf("&quot;") <> -1 Then filterstring = filterstring.Replace("&quot;", Chr(34))
            If filterstring.IndexOf("&lt;") <> -1 Then filterstring = filterstring.Replace("&lt;", "<")
            If filterstring.IndexOf("&gt;") <> -1 Then filterstring = filterstring.Replace("&gt;", ">")
            If filterstring.IndexOf("&#138;") <> -1 Then filterstring = filterstring.Replace("&#138;", Chr(138))
            If filterstring.IndexOf("&#140;") <> -1 Then filterstring = filterstring.Replace("&#140;", Chr(140))
            If filterstring.IndexOf("&#142;") <> -1 Then filterstring = filterstring.Replace("&#142;", Chr(142))
            If filterstring.IndexOf("&#153;") <> -1 Then filterstring = filterstring.Replace("&#153;", Chr(153))
            If filterstring.IndexOf("&#154;") <> -1 Then filterstring = filterstring.Replace("&#154;", Chr(154))
            If filterstring.IndexOf("&#156;") <> -1 Then filterstring = filterstring.Replace("&#156;", Chr(156))
            If filterstring.IndexOf("&#158;") <> -1 Then filterstring = filterstring.Replace("&#158;", Chr(158))
            If filterstring.IndexOf("&#159;") <> -1 Then filterstring = filterstring.Replace("&#159;", Chr(159))
            If filterstring.IndexOf("&#160;") <> -1 Then filterstring = filterstring.Replace("&#160;", Chr(160))
            If filterstring.IndexOf("&#169;") <> -1 Then filterstring = filterstring.Replace("&#169;", Chr(169))
            If filterstring.IndexOf("&#178;") <> -1 Then filterstring = filterstring.Replace("&#178;", Chr(178))
            If filterstring.IndexOf("&#179;") <> -1 Then filterstring = filterstring.Replace("&#179;", Chr(179))
            If filterstring.IndexOf("&#181;") <> -1 Then filterstring = filterstring.Replace("&#181;", Chr(181))
            If filterstring.IndexOf("&#183;") <> -1 Then filterstring = filterstring.Replace("&#183;", Chr(183))
            If filterstring.IndexOf("&#188;") <> -1 Then filterstring = filterstring.Replace("&#188;", Chr(188))
            If filterstring.IndexOf("&#189;") <> -1 Then filterstring = filterstring.Replace("&#189;", Chr(189))
            If filterstring.IndexOf("&#190;") <> -1 Then filterstring = filterstring.Replace("&#190;", Chr(190))
            If filterstring.IndexOf("&#192;") <> -1 Then filterstring = filterstring.Replace("&#192;", Chr(192))
            If filterstring.IndexOf("&#193;") <> -1 Then filterstring = filterstring.Replace("&#193;", Chr(193))
            If filterstring.IndexOf("&#194;") <> -1 Then filterstring = filterstring.Replace("&#194;", Chr(194))
            If filterstring.IndexOf("&#195;") <> -1 Then filterstring = filterstring.Replace("&#195;", Chr(195))
            If filterstring.IndexOf("&#196;") <> -1 Then filterstring = filterstring.Replace("&#196;", Chr(196))
            If filterstring.IndexOf("&#197;") <> -1 Then filterstring = filterstring.Replace("&#197;", Chr(197))
            If filterstring.IndexOf("&#199;") <> -1 Then filterstring = filterstring.Replace("&#199;", Chr(199))
            If filterstring.IndexOf("&#200;") <> -1 Then filterstring = filterstring.Replace("&#200;", Chr(200))
            If filterstring.IndexOf("&#201;") <> -1 Then filterstring = filterstring.Replace("&#201;", Chr(201))
            If filterstring.IndexOf("&#203;") <> -1 Then filterstring = filterstring.Replace("&#203;", Chr(203))
            If filterstring.IndexOf("&#204;") <> -1 Then filterstring = filterstring.Replace("&#204;", Chr(204))
            If filterstring.IndexOf("&#205;") <> -1 Then filterstring = filterstring.Replace("&#205;", Chr(205))
            If filterstring.IndexOf("&#206;") <> -1 Then filterstring = filterstring.Replace("&#206;", Chr(206))
            If filterstring.IndexOf("&#207;") <> -1 Then filterstring = filterstring.Replace("&#207;", Chr(207))
            If filterstring.IndexOf("&#208;") <> -1 Then filterstring = filterstring.Replace("&#208;", Chr(208))
            If filterstring.IndexOf("&#209;") <> -1 Then filterstring = filterstring.Replace("&#209;", Chr(209))
            If filterstring.IndexOf("&#210;") <> -1 Then filterstring = filterstring.Replace("&#210;", Chr(210))
            If filterstring.IndexOf("&#211;") <> -1 Then filterstring = filterstring.Replace("&#211;", Chr(211))
            If filterstring.IndexOf("&#212;") <> -1 Then filterstring = filterstring.Replace("&#212;", Chr(212))
            If filterstring.IndexOf("&#213;") <> -1 Then filterstring = filterstring.Replace("&#213;", Chr(213))
            If filterstring.IndexOf("&#214;") <> -1 Then filterstring = filterstring.Replace("&#214;", Chr(214))
            If filterstring.IndexOf("&#215;") <> -1 Then filterstring = filterstring.Replace("&#215;", Chr(215))
            If filterstring.IndexOf("&#216;") <> -1 Then filterstring = filterstring.Replace("&#216;", Chr(216))
            If filterstring.IndexOf("&#217;") <> -1 Then filterstring = filterstring.Replace("&#217;", Chr(217))
            If filterstring.IndexOf("&#218;") <> -1 Then filterstring = filterstring.Replace("&#218;", Chr(218))
            If filterstring.IndexOf("&#219;") <> -1 Then filterstring = filterstring.Replace("&#219;", Chr(219))
            If filterstring.IndexOf("&#220;") <> -1 Then filterstring = filterstring.Replace("&#220;", Chr(220))
            If filterstring.IndexOf("&#221;") <> -1 Then filterstring = filterstring.Replace("&#221;", Chr(221))
            If filterstring.IndexOf("&#222;") <> -1 Then filterstring = filterstring.Replace("&#222;", Chr(222))
            If filterstring.IndexOf("&#223;") <> -1 Then filterstring = filterstring.Replace("&#223;", Chr(223))
            If filterstring.IndexOf("&#224;") <> -1 Then filterstring = filterstring.Replace("&#224;", Chr(224))
            If filterstring.IndexOf("&#225;") <> -1 Then filterstring = filterstring.Replace("&#225;", Chr(225))
            If filterstring.IndexOf("&#226;") <> -1 Then filterstring = filterstring.Replace("&#226;", Chr(226))
            If filterstring.IndexOf("&#227;") <> -1 Then filterstring = filterstring.Replace("&#227;", Chr(227))
            If filterstring.IndexOf("&#228;") <> -1 Then filterstring = filterstring.Replace("&#228;", Chr(228))
            If filterstring.IndexOf("&#229;") <> -1 Then filterstring = filterstring.Replace("&#229;", Chr(229))
            If filterstring.IndexOf("&#230;") <> -1 Then filterstring = filterstring.Replace("&#230;", Chr(230))
            If filterstring.IndexOf("&#231;") <> -1 Then filterstring = filterstring.Replace("&#231;", Chr(231))
            If filterstring.IndexOf("&#232;") <> -1 Then filterstring = filterstring.Replace("&#232;", Chr(232))
            If filterstring.IndexOf("&#233;") <> -1 Then filterstring = filterstring.Replace("&#233;", Chr(233))
            If filterstring.IndexOf("&#234;") <> -1 Then filterstring = filterstring.Replace("&#234;", Chr(234))
            If filterstring.IndexOf("&#235;") <> -1 Then filterstring = filterstring.Replace("&#235;", Chr(235))
            If filterstring.IndexOf("&#236;") <> -1 Then filterstring = filterstring.Replace("&#236;", Chr(236))
            If filterstring.IndexOf("&#237;") <> -1 Then filterstring = filterstring.Replace("&#237;", Chr(237))
            If filterstring.IndexOf("&#238;") <> -1 Then filterstring = filterstring.Replace("&#238;", Chr(238))
            If filterstring.IndexOf("&#239;") <> -1 Then filterstring = filterstring.Replace("&#239;", Chr(239))
            If filterstring.IndexOf("&#240;") <> -1 Then filterstring = filterstring.Replace("&#240;", Chr(240))
            If filterstring.IndexOf("&#241;") <> -1 Then filterstring = filterstring.Replace("&#241;", Chr(241))
            If filterstring.IndexOf("&#242;") <> -1 Then filterstring = filterstring.Replace("&#242;", Chr(242))
            If filterstring.IndexOf("&#243;") <> -1 Then filterstring = filterstring.Replace("&#243;", Chr(243))
            If filterstring.IndexOf("&#244;") <> -1 Then filterstring = filterstring.Replace("&#244;", Chr(244))
            If filterstring.IndexOf("&#245;") <> -1 Then filterstring = filterstring.Replace("&#245;", Chr(245))
            If filterstring.IndexOf("&#246;") <> -1 Then filterstring = filterstring.Replace("&#246;", Chr(246))
            If filterstring.IndexOf("&#247;") <> -1 Then filterstring = filterstring.Replace("&#247;", Chr(247))
            If filterstring.IndexOf("&#248;") <> -1 Then filterstring = filterstring.Replace("&#248;", Chr(248))
            If filterstring.IndexOf("&#249;") <> -1 Then filterstring = filterstring.Replace("&#249;", Chr(249))
            If filterstring.IndexOf("&#250;") <> -1 Then filterstring = filterstring.Replace("&#250;", Chr(250))
            If filterstring.IndexOf("&#251;") <> -1 Then filterstring = filterstring.Replace("&#251;", Chr(251))
            If filterstring.IndexOf("&#252;") <> -1 Then filterstring = filterstring.Replace("&#252;", Chr(252))
            If filterstring.IndexOf("&#253;") <> -1 Then filterstring = filterstring.Replace("&#253;", Chr(253))
            If filterstring.IndexOf("&#254;") <> -1 Then filterstring = filterstring.Replace("&#254;", Chr(254))
            If filterstring.IndexOf("&#255;") <> -1 Then filterstring = filterstring.Replace("&#255;", Chr(255))
            If filterstring.IndexOf("&#38;") <> -1 Then filterstring = filterstring.Replace("&#38;", Chr(38))
            If filterstring.IndexOf("&#34;") <> -1 Then filterstring = filterstring.Replace("&#34;", Chr(34))
            If filterstring.IndexOf("&#x20;") <> -1 Then filterstring = filterstring.Replace("&#x27;", " ")
            If filterstring.IndexOf("&#x21;") <> -1 Then filterstring = filterstring.Replace("&#x21;", "!")
            If filterstring.IndexOf("&#x22;") <> -1 Then filterstring = filterstring.Replace("&#x22;", Chr(34))
            If filterstring.IndexOf("&#x23;") <> -1 Then filterstring = filterstring.Replace("&#x23;", "#")
            If filterstring.IndexOf("&#x24;") <> -1 Then filterstring = filterstring.Replace("&#x24;", "$")
            If filterstring.IndexOf("&#x25;") <> -1 Then filterstring = filterstring.Replace("&#x25;", "%")
            If filterstring.IndexOf("&#x26;") <> -1 Then filterstring = filterstring.Replace("&#x26;", "&")
            If filterstring.IndexOf("&#x27;") <> -1 Then filterstring = filterstring.Replace("&#x27;", "'")
            If filterstring.IndexOf("&#x28;") <> -1 Then filterstring = filterstring.Replace("&#x28;", "(")
            If filterstring.IndexOf("&#x29;") <> -1 Then filterstring = filterstring.Replace("&#x29;", ")")
            If filterstring.IndexOf("&#x2A;") <> -1 Then filterstring = filterstring.Replace("&#x2A;", "*")
            If filterstring.IndexOf("&#x2B;") <> -1 Then filterstring = filterstring.Replace("&#x2B;", "+")
            If filterstring.IndexOf("&#x2C;") <> -1 Then filterstring = filterstring.Replace("&#x2C;", ",")
            If filterstring.IndexOf("&#x2D;") <> -1 Then filterstring = filterstring.Replace("&#x2D;", "-")
            If filterstring.IndexOf("&#x2E;") <> -1 Then filterstring = filterstring.Replace("&#x2E;", ".")
            If filterstring.IndexOf("&#x2F;") <> -1 Then filterstring = filterstring.Replace("&#x2F;", "/")


            If filterstring.IndexOf("&#x3A;") <> -1 Then filterstring = filterstring.Replace("&#x3A;", ":")
            If filterstring.IndexOf("&#x3B;") <> -1 Then filterstring = filterstring.Replace("&#x3B;", ";")
            If filterstring.IndexOf("&#x3C;") <> -1 Then filterstring = filterstring.Replace("&#x3C;", "<")
            If filterstring.IndexOf("&#x3D;") <> -1 Then filterstring = filterstring.Replace("&#x3D;", "=")
            If filterstring.IndexOf("&#x3E;") <> -1 Then filterstring = filterstring.Replace("&#x3E;", ">")
            If filterstring.IndexOf("&#x3F;") <> -1 Then filterstring = filterstring.Replace("&#x3F;", "?")


            If filterstring.IndexOf("&#x40;") <> -1 Then filterstring = filterstring.Replace("&#x40;", "@")
            If filterstring.IndexOf("&#x5B;") <> -1 Then filterstring = filterstring.Replace("&#x5B;", "[")
            If filterstring.IndexOf("&#x5C;") <> -1 Then filterstring = filterstring.Replace("&#x5C;", "\")
            If filterstring.IndexOf("&#x5D;") <> -1 Then filterstring = filterstring.Replace("&#x5D;", "]")
            If filterstring.IndexOf("&#x5E;") <> -1 Then filterstring = filterstring.Replace("&#x5E;", "^")
            If filterstring.IndexOf("&#x5F;") <> -1 Then filterstring = filterstring.Replace("&#x5F;", "_")
            If filterstring.IndexOf("&#x60;") <> -1 Then filterstring = filterstring.Replace("&#x60;", "`")
            If filterstring.IndexOf("&#x7B;") <> -1 Then filterstring = filterstring.Replace("&#x7B;", "{")
            If filterstring.IndexOf("&#x7C;") <> -1 Then filterstring = filterstring.Replace("&#x7C;", "|")
            If filterstring.IndexOf("&#x7D;") <> -1 Then filterstring = filterstring.Replace("&#x7D;", "}")
            If filterstring.IndexOf("&#x7E;") <> -1 Then filterstring = filterstring.Replace("&#x7E;", "~")


            If filterstring.IndexOf("&#x80;") <> -1 Then filterstring = filterstring.Replace("&#x80;", "€")
            If filterstring.IndexOf("&#x82;") <> -1 Then filterstring = filterstring.Replace("&#x82;", "‚")
            If filterstring.IndexOf("&#x83;") <> -1 Then filterstring = filterstring.Replace("&#x83;", "ƒ")
            If filterstring.IndexOf("&#x84;") <> -1 Then filterstring = filterstring.Replace("&#x84;", "„")
            If filterstring.IndexOf("&#x85;") <> -1 Then filterstring = filterstring.Replace("&#x85;", "…")
            If filterstring.IndexOf("&#x86;") <> -1 Then filterstring = filterstring.Replace("&#x86;", "†")
            If filterstring.IndexOf("&#x87;") <> -1 Then filterstring = filterstring.Replace("&#x87;", "‡")
            If filterstring.IndexOf("&#x88;") <> -1 Then filterstring = filterstring.Replace("&#x88;", "ˆ")
            If filterstring.IndexOf("&#x89;") <> -1 Then filterstring = filterstring.Replace("&#x89;", "‰")
            If filterstring.IndexOf("&#x8A;") <> -1 Then filterstring = filterstring.Replace("&#x8A;", "Š")
            If filterstring.IndexOf("&#x8B;") <> -1 Then filterstring = filterstring.Replace("&#x8B;", "‹")
            If filterstring.IndexOf("&#x8C;") <> -1 Then filterstring = filterstring.Replace("&#x8C;", "Œ")
            If filterstring.IndexOf("&#x8E;") <> -1 Then filterstring = filterstring.Replace("&#x8E;", "Ž")


            If filterstring.IndexOf("&#x91;") <> -1 Then filterstring = filterstring.Replace("&#x91;", "‘")
            If filterstring.IndexOf("&#x92;") <> -1 Then filterstring = filterstring.Replace("&#x92;", "’")
            If filterstring.IndexOf("&#x93;") <> -1 Then filterstring = filterstring.Replace("&#x93;", """")
            If filterstring.IndexOf("&#x94;") <> -1 Then filterstring = filterstring.Replace("&#x94;", """")
            If filterstring.IndexOf("&#x95;") <> -1 Then filterstring = filterstring.Replace("&#x95;", "•")
            If filterstring.IndexOf("&#x96;") <> -1 Then filterstring = filterstring.Replace("&#x96;", "–")
            If filterstring.IndexOf("&#x97;") <> -1 Then filterstring = filterstring.Replace("&#x97;", "—")
            If filterstring.IndexOf("&#x98;") <> -1 Then filterstring = filterstring.Replace("&#x98;", "˜")
            If filterstring.IndexOf("&#x99;") <> -1 Then filterstring = filterstring.Replace("&#x99;", "™")
            If filterstring.IndexOf("&#x9A;") <> -1 Then filterstring = filterstring.Replace("&#x9A;", "š")
            If filterstring.IndexOf("&#x9B;") <> -1 Then filterstring = filterstring.Replace("&#x9B;", "›")
            If filterstring.IndexOf("&#x9C;") <> -1 Then filterstring = filterstring.Replace("&#x9C;", "œ")
            If filterstring.IndexOf("&#x9E;") <> -1 Then filterstring = filterstring.Replace("&#x9E;", "ž")
            If filterstring.IndexOf("&#x9F;") <> -1 Then filterstring = filterstring.Replace("&#x9F;", "Ÿ")


            If filterstring.IndexOf("&#xA0;") <> -1 Then filterstring = filterstring.Replace("&#xA0;", " ")
            If filterstring.IndexOf("&#xA1;") <> -1 Then filterstring = filterstring.Replace("&#xA1;", "¡")
            If filterstring.IndexOf("&#xA2;") <> -1 Then filterstring = filterstring.Replace("&#xA2;", "¢")
            If filterstring.IndexOf("&#xA3;") <> -1 Then filterstring = filterstring.Replace("&#xA3;", "£")
            If filterstring.IndexOf("&#xA4;") <> -1 Then filterstring = filterstring.Replace("&#xA4;", "¤")
            If filterstring.IndexOf("&#xA5;") <> -1 Then filterstring = filterstring.Replace("&#xA5;", "¥")
            If filterstring.IndexOf("&#xA6;") <> -1 Then filterstring = filterstring.Replace("&#xA6;", "¦")
            If filterstring.IndexOf("&#xA7;") <> -1 Then filterstring = filterstring.Replace("&#xA7;", "§")
            If filterstring.IndexOf("&#xA8;") <> -1 Then filterstring = filterstring.Replace("&#xA8;", "¨")
            If filterstring.IndexOf("&#xA9;") <> -1 Then filterstring = filterstring.Replace("&#xA9;", "©")
            If filterstring.IndexOf("&#xAA;") <> -1 Then filterstring = filterstring.Replace("&#xAA;", "ª")
            If filterstring.IndexOf("&#xAB;") <> -1 Then filterstring = filterstring.Replace("&#xAB;", "«")
            If filterstring.IndexOf("&#xAC;") <> -1 Then filterstring = filterstring.Replace("&#xAC;", "¬")
            If filterstring.IndexOf("&#xAD;") <> -1 Then filterstring = filterstring.Replace("&#xAD;", " ")
            If filterstring.IndexOf("&#xAE;") <> -1 Then filterstring = filterstring.Replace("&#xAE;", "®")
            If filterstring.IndexOf("&#xAF;") <> -1 Then filterstring = filterstring.Replace("&#xAF;", "¯")


            If filterstring.IndexOf("&#xB0;") <> -1 Then filterstring = filterstring.Replace("&#xB0;", "°")
            If filterstring.IndexOf("&#xB1;") <> -1 Then filterstring = filterstring.Replace("&#xB1;", "±")
            If filterstring.IndexOf("&#xB2;") <> -1 Then filterstring = filterstring.Replace("&#xB2;", "²")
            If filterstring.IndexOf("&#xB3;") <> -1 Then filterstring = filterstring.Replace("&#xB3;", "³")
            If filterstring.IndexOf("&#xB4;") <> -1 Then filterstring = filterstring.Replace("&#xB4;", "´")
            If filterstring.IndexOf("&#xB5;") <> -1 Then filterstring = filterstring.Replace("&#xB5;", "µ")
            If filterstring.IndexOf("&#xB6;") <> -1 Then filterstring = filterstring.Replace("&#xB6;", "¶")
            If filterstring.IndexOf("&#xB7;") <> -1 Then filterstring = filterstring.Replace("&#xB7;", "·")
            If filterstring.IndexOf("&#xB8;") <> -1 Then filterstring = filterstring.Replace("&#xB8;", "¸")
            If filterstring.IndexOf("&#xB9;") <> -1 Then filterstring = filterstring.Replace("&#xB9;", "¹")
            If filterstring.IndexOf("&#xBA;") <> -1 Then filterstring = filterstring.Replace("&#xBA;", "º")
            If filterstring.IndexOf("&#xBB;") <> -1 Then filterstring = filterstring.Replace("&#xBB;", "»")
            If filterstring.IndexOf("&#xBC;") <> -1 Then filterstring = filterstring.Replace("&#xBC;", "¼")
            If filterstring.IndexOf("&#xBD;") <> -1 Then filterstring = filterstring.Replace("&#xBD;", "½")
            If filterstring.IndexOf("&#xBE;") <> -1 Then filterstring = filterstring.Replace("&#xBE;", "¾")
            If filterstring.IndexOf("&#xBF;") <> -1 Then filterstring = filterstring.Replace("&#xBF;", "¿")


            If filterstring.IndexOf("&#xC0;") <> -1 Then filterstring = filterstring.Replace("&#xC0;", "À")
            If filterstring.IndexOf("&#xC1;") <> -1 Then filterstring = filterstring.Replace("&#xC1;", "Á")
            If filterstring.IndexOf("&#xC2;") <> -1 Then filterstring = filterstring.Replace("&#xC2;", "Â")
            If filterstring.IndexOf("&#xC3;") <> -1 Then filterstring = filterstring.Replace("&#xC3;", "Ã")
            If filterstring.IndexOf("&#xC4;") <> -1 Then filterstring = filterstring.Replace("&#xC4;", "Ä")
            If filterstring.IndexOf("&#xC5;") <> -1 Then filterstring = filterstring.Replace("&#xC5;", "Å")
            If filterstring.IndexOf("&#xC6;") <> -1 Then filterstring = filterstring.Replace("&#xC6;", "Æ")
            If filterstring.IndexOf("&#xC7;") <> -1 Then filterstring = filterstring.Replace("&#xC7;", "Ç")
            If filterstring.IndexOf("&#xC8;") <> -1 Then filterstring = filterstring.Replace("&#xC8;", "È")
            If filterstring.IndexOf("&#xC9;") <> -1 Then filterstring = filterstring.Replace("&#xC9;", "É")
            If filterstring.IndexOf("&#xCA;") <> -1 Then filterstring = filterstring.Replace("&#xCA;", "Ê")
            If filterstring.IndexOf("&#xCB;") <> -1 Then filterstring = filterstring.Replace("&#xCB;", "Ë")
            If filterstring.IndexOf("&#xCC;") <> -1 Then filterstring = filterstring.Replace("&#xCC;", "Ì")
            If filterstring.IndexOf("&#xCD;") <> -1 Then filterstring = filterstring.Replace("&#xCD;", "Í")
            If filterstring.IndexOf("&#xCE;") <> -1 Then filterstring = filterstring.Replace("&#xCE;", "Î")
            If filterstring.IndexOf("&#xCF;") <> -1 Then filterstring = filterstring.Replace("&#xCF;", "Ï")


            If filterstring.IndexOf("&#xD0;") <> -1 Then filterstring = filterstring.Replace("&#xD0;", "Ð")
            If filterstring.IndexOf("&#xD1;") <> -1 Then filterstring = filterstring.Replace("&#xD1;", "Ñ")
            If filterstring.IndexOf("&#xD2;") <> -1 Then filterstring = filterstring.Replace("&#xD2;", "Ò")
            If filterstring.IndexOf("&#xD3;") <> -1 Then filterstring = filterstring.Replace("&#xD3;", "Ó")
            If filterstring.IndexOf("&#xD4;") <> -1 Then filterstring = filterstring.Replace("&#xD4;", "Ô")
            If filterstring.IndexOf("&#xD5;") <> -1 Then filterstring = filterstring.Replace("&#xD5;", "Õ")
            If filterstring.IndexOf("&#xD6;") <> -1 Then filterstring = filterstring.Replace("&#xD6;", "Ö")
            If filterstring.IndexOf("&#xD7;") <> -1 Then filterstring = filterstring.Replace("&#xD7;", "×")
            If filterstring.IndexOf("&#xD8;") <> -1 Then filterstring = filterstring.Replace("&#xD8;", "Ø")
            If filterstring.IndexOf("&#xD9;") <> -1 Then filterstring = filterstring.Replace("&#xD9;", "Ù")
            If filterstring.IndexOf("&#xDA;") <> -1 Then filterstring = filterstring.Replace("&#xDA;", "Ú")
            If filterstring.IndexOf("&#xDB;") <> -1 Then filterstring = filterstring.Replace("&#xDB;", "Û")
            If filterstring.IndexOf("&#xDC;") <> -1 Then filterstring = filterstring.Replace("&#xDC;", "Ü")
            If filterstring.IndexOf("&#xDD;") <> -1 Then filterstring = filterstring.Replace("&#xDD;", "Ý")
            If filterstring.IndexOf("&#xDE;") <> -1 Then filterstring = filterstring.Replace("&#xDE;", "Þ")
            If filterstring.IndexOf("&#xDF;") <> -1 Then filterstring = filterstring.Replace("&#xDF;", "ß")


            If filterstring.IndexOf("&#xE0;") <> -1 Then filterstring = filterstring.Replace("&#xE0;", "à")
            If filterstring.IndexOf("&#xE1;") <> -1 Then filterstring = filterstring.Replace("&#xE1;", "á")
            If filterstring.IndexOf("&#xE2;") <> -1 Then filterstring = filterstring.Replace("&#xE2;", "â")
            If filterstring.IndexOf("&#xE3;") <> -1 Then filterstring = filterstring.Replace("&#xE3;", "ã")
            If filterstring.IndexOf("&#xE4;") <> -1 Then filterstring = filterstring.Replace("&#xE4;", "ä")
            If filterstring.IndexOf("&#xE5;") <> -1 Then filterstring = filterstring.Replace("&#xE5;", "å")
            If filterstring.IndexOf("&#xE6;") <> -1 Then filterstring = filterstring.Replace("&#xE6;", "æ")
            If filterstring.IndexOf("&#xE7;") <> -1 Then filterstring = filterstring.Replace("&#xE7;", "ç")
            If filterstring.IndexOf("&#xE8;") <> -1 Then filterstring = filterstring.Replace("&#xE8;", "è")
            If filterstring.IndexOf("&#xE9;") <> -1 Then filterstring = filterstring.Replace("&#xE9;", "é")
            If filterstring.IndexOf("&#xEA;") <> -1 Then filterstring = filterstring.Replace("&#xEA;", "ê")
            If filterstring.IndexOf("&#xEB;") <> -1 Then filterstring = filterstring.Replace("&#xEB;", "ë")
            If filterstring.IndexOf("&#xEC;") <> -1 Then filterstring = filterstring.Replace("&#xEC;", "ì")
            If filterstring.IndexOf("&#xED;") <> -1 Then filterstring = filterstring.Replace("&#xED;", "í")
            If filterstring.IndexOf("&#xEE;") <> -1 Then filterstring = filterstring.Replace("&#xEE;", "î")
            If filterstring.IndexOf("&#xEF;") <> -1 Then filterstring = filterstring.Replace("&#xEF;", "ï")


            If filterstring.IndexOf("&#xF0;") <> -1 Then filterstring = filterstring.Replace("&#xF0;", "ð")
            If filterstring.IndexOf("&#xF1;") <> -1 Then filterstring = filterstring.Replace("&#xF1;", "ñ")
            If filterstring.IndexOf("&#xF2;") <> -1 Then filterstring = filterstring.Replace("&#xF2;", "ò")
            If filterstring.IndexOf("&#xF3;") <> -1 Then filterstring = filterstring.Replace("&#xF3;", "ó")
            If filterstring.IndexOf("&#xF4;") <> -1 Then filterstring = filterstring.Replace("&#xF4;", "ô")
            If filterstring.IndexOf("&#xF5;") <> -1 Then filterstring = filterstring.Replace("&#xF5;", "õ")
            If filterstring.IndexOf("&#xF6;") <> -1 Then filterstring = filterstring.Replace("&#xF6;", "ö")
            If filterstring.IndexOf("&#xF7;") <> -1 Then filterstring = filterstring.Replace("&#xF7;", "÷")
            If filterstring.IndexOf("&#xF8;") <> -1 Then filterstring = filterstring.Replace("&#xF8;", "ø")
            If filterstring.IndexOf("&#xF9;") <> -1 Then filterstring = filterstring.Replace("&#xF9;", "ù")
            If filterstring.IndexOf("&#xFA;") <> -1 Then filterstring = filterstring.Replace("&#xFA;", "ú")
            If filterstring.IndexOf("&#xFB;") <> -1 Then filterstring = filterstring.Replace("&#xFB;", "û")
            If filterstring.IndexOf("&#xFC;") <> -1 Then filterstring = filterstring.Replace("&#xFC;", "ü")
            If filterstring.IndexOf("&#xFD;") <> -1 Then filterstring = filterstring.Replace("&#xFD;", "ý")
            If filterstring.IndexOf("&#xFE;") <> -1 Then filterstring = filterstring.Replace("&#xFE;", "þ")
            If filterstring.IndexOf("&#xFF;") <> -1 Then filterstring = filterstring.Replace("&#xFF;", "ÿ")
            If filterstring.IndexOf("&oacute;") <> -1 Then filterstring = filterstring.Replace("&oacute;", "ó")
            If filterstring.IndexOf("&eacute;") <> -1 Then filterstring = filterstring.Replace("&eacute;", "é")
            '

            Return filterstring
        Catch
        Finally
        End Try
        Return "Error"
    End Function
    Public Function get_hdtags(ByVal filename As String) As FullFileDetails

        Dim workingfiledetails As New FullFileDetails
        Try
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

            If Not IO.File.Exists(filename) Then
                Return workingfiledetails
            End If
            Dim MI As New mediainfo
            'MI = New mediainfo
            MI.Open(filename)
            Dim curVS As Integer = 0
            Dim addVS As Boolean = False
            Dim numOfVideoStreams As Integer = MI.Count_Get(StreamKind.Visual)

            Dim tempmediainfo As String
            Dim tempmediainfo2 As String

            workingfiledetails.filedetails_video.width = MI.Get_(StreamKind.Visual, curVS, "Width")
            workingfiledetails.filedetails_video.height = MI.Get_(StreamKind.Visual, curVS, "Height")
            If workingfiledetails.filedetails_video.width <> Nothing Then
                If IsNumeric(workingfiledetails.filedetails_video.width) Then
                    If workingfiledetails.filedetails_video.height <> Nothing Then
                        If IsNumeric(workingfiledetails.filedetails_video.height) Then
                            '                            Dim tempwidth As Integer = Convert.ToInt32(workingfiledetails.filedetails_video.width)
                            '                            Dim tempheight As Integer = Convert.ToInt32(workingfiledetails.filedetails_video.height)
                            '                            Dim aspect As Decimal
                            Try
                                '                                aspect = tempwidth / tempheight  'Next three line are wrong for getting display aspect ratio
                                '                                aspect = FormatNumber(aspect, 3)
                                '                                If aspect > 0 Then workingfiledetails.filedetails_video.aspect = aspect.ToString

                                Dim Information As String = MI.Inform
                                Dim BeginString As Integer = Information.ToLower.IndexOf(":", Information.ToLower.IndexOf("display aspect ratio"))
                                Dim EndString As Integer = Information.ToLower.IndexOf("frame rate")
                                Dim SizeofString As Integer = EndString - BeginString
                                Dim DisplayAspectRatio As String = Information.Substring(BeginString, SizeofString).Trim(" ", ":", Chr(10), Chr(13))
                                'DisplayAspectRatio = DisplayAspectRatio.Substring(0, Len(DisplayAspectRatio) - 1)
                                If Len(DisplayAspectRatio) > 0 Then
                                    workingfiledetails.filedetails_video.aspect = DisplayAspectRatio
                                Else
                                    workingfiledetails.filedetails_video.aspect = "Unknown"
                                End If

                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            End If
            'workingfiledetails.filedetails_video.aspect = MI.Get_(StreamKind.Visual, 0, 79)


            tempmediainfo = MI.Get_(StreamKind.Visual, curVS, "Format")
            If tempmediainfo.ToLower = "avc" Then
                tempmediainfo2 = "h264"
            Else
                tempmediainfo2 = tempmediainfo
            End If

            'workingfiledetails.filedetails_video.codec = tempmediainfo2
            'workingfiledetails.filedetails_video.formatinfo = tempmediainfo
            workingfiledetails.filedetails_video.codec = MI.Get_(StreamKind.Visual, curVS, "CodecID")
            If workingfiledetails.filedetails_video.codec = "DX50" Then
                workingfiledetails.filedetails_video.codec = "DIVX"
            End If
            '_MPEG4/ISO/AVC
            If workingfiledetails.filedetails_video.codec.ToLower.IndexOf("mpeg4/iso/avc") <> -1 Then
                workingfiledetails.filedetails_video.codec = "h264"
            End If
            workingfiledetails.filedetails_video.formatinfo = MI.Get_(StreamKind.Visual, curVS, "CodecID")
            Dim fs(100) As String
            For f = 1 To 100
                fs(f) = MI.Get_(StreamKind.Visual, 0, f)
            Next

            Try
                If playlist.Count = 1 Then
                    workingfiledetails.filedetails_video.duration = MI.Get_(StreamKind.Visual, 0, 61)
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
                    workingfiledetails.filedetails_video.duration = totalmins & " min"
                End If
            Catch
                workingfiledetails.filedetails_video.duration = MI.Get_(StreamKind.Visual, 0, 57)
            End Try
            workingfiledetails.filedetails_video.bitrate = MI.Get_(StreamKind.Visual, curVS, "BitRate/String")
            workingfiledetails.filedetails_video.bitratemode = MI.Get_(StreamKind.Visual, curVS, "BitRate_Mode/String")

            workingfiledetails.filedetails_video.bitratemax = MI.Get_(StreamKind.Visual, curVS, "BitRate_Maximum/String")

            tempmediainfo = IO.Path.GetExtension(filename) '"This is the extension of the file"
            workingfiledetails.filedetails_video.container = tempmediainfo
            'workingfiledetails.filedetails_video.codecid = MI.Get_(StreamKind.Visual, curVS, "CodecID")

            workingfiledetails.filedetails_video.codecinfo = MI.Get_(StreamKind.Visual, curVS, "CodecID/Info")
            workingfiledetails.filedetails_video.scantype = MI.Get_(StreamKind.Visual, curVS, 102)
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
                    Dim audio As New str_MediaNFOAudio(SetDefaults)
                    audio.language = Utilities.GetLangCode(MI.Get_(StreamKind.Audio, curAS, "Language/String"))
                    If MI.Get_(StreamKind.Audio, curAS, "Format") = "MPEG Audio" Then
                        audio.codec = "MP3"
                    Else
                        audio.codec = MI.Get_(StreamKind.Audio, curAS, "Format")
                    End If
                    If audio.codec = "AC-3" Then
                        audio.codec = "AC3"
                    End If
                    If audio.codec = "DTS" Then
                        audio.codec = "dca"
                    End If
                    audio.channels = MI.Get_(StreamKind.Audio, curAS, "Channel(s)")
                    audio.bitrate = MI.Get_(StreamKind.Audio, curAS, "BitRate/String")
                    workingfiledetails.filedetails_audio.Add(audio)
                    curAS += 1
                End While
            End If


            Dim numOfSubtitleStreams As Integer = MI.Count_Get(StreamKind.Text)
            Dim curSS As Integer = 0
            If numOfSubtitleStreams > 0 Then
                While curSS < numOfSubtitleStreams
                    Dim sublanguage As New str_MediaNFOSubtitles(SetDefaults)
                    sublanguage.language = Utilities.GetLangCode(MI.Get_(StreamKind.Text, curSS, "Language/String"))
                    workingfiledetails.filedetails_subtitles.Add(sublanguage)
                    curSS += 1
                End While
            End If

            Return workingfiledetails
        Catch ex As Exception

        Finally
        End Try
        Return workingfiledetails
    End Function
    Public Function InsertFileInformationTags(ByVal Entrada As String, ByVal Filename As String) As String
        Dim WorkingFileDetails As FullFileDetails = get_hdtags(Filename)
        Dim FileInfoString As String = "<movie>" & vbLf & "<fileinfo>" & vbLf & "<streamdetails>" & vbLf & "<video>" & vbLf

        If WorkingFileDetails.filedetails_video.width <> Nothing Then FileInfoString &= "<width>" & WorkingFileDetails.filedetails_video.width & "</width>" & vbLf
        If WorkingFileDetails.filedetails_video.height <> Nothing Then FileInfoString &= "<height>" & WorkingFileDetails.filedetails_video.height & "</height>" & vbLf
        If WorkingFileDetails.filedetails_video.aspect <> Nothing Then FileInfoString &= "<aspect>" & WorkingFileDetails.filedetails_video.aspect & "</aspect>" & vbLf
        If WorkingFileDetails.filedetails_video.codec <> Nothing Then FileInfoString &= "<codec>" & WorkingFileDetails.filedetails_video.codec & "</codec>" & vbLf
        If WorkingFileDetails.filedetails_video.formatinfo <> Nothing Then FileInfoString &= "<format>" & WorkingFileDetails.filedetails_video.formatinfo & "</format>" & vbLf
        If WorkingFileDetails.filedetails_video.duration <> Nothing Then FileInfoString &= "<duration>" & Utilities.cleanruntime(WorkingFileDetails.filedetails_video.duration) & "</duration>" & vbLf
        If WorkingFileDetails.filedetails_video.bitrate <> Nothing Then FileInfoString &= "<bitrate>" & WorkingFileDetails.filedetails_video.bitrate & "</bitrate>" & vbLf
        If WorkingFileDetails.filedetails_video.bitratemode <> Nothing Then FileInfoString &= "<bitratemode>" & WorkingFileDetails.filedetails_video.bitratemode & "</bitratemode>" & vbLf
        If WorkingFileDetails.filedetails_video.bitratemax <> Nothing Then FileInfoString &= "<bitratemax>" & WorkingFileDetails.filedetails_video.bitratemax & "</bitratemax>" & vbLf
        If WorkingFileDetails.filedetails_video.container <> Nothing Then FileInfoString &= "<container>" & WorkingFileDetails.filedetails_video.container & "</container>" & vbLf
        If WorkingFileDetails.filedetails_video.codecid <> Nothing Then FileInfoString &= "<codecid>" & WorkingFileDetails.filedetails_video.codecid & "</codecid>" & vbLf
        If WorkingFileDetails.filedetails_video.codecinfo <> Nothing Then FileInfoString &= "<codecinfo>" & WorkingFileDetails.filedetails_video.codecinfo & "</codecinfo>" & vbLf
        If WorkingFileDetails.filedetails_video.scantype <> Nothing Then FileInfoString &= "<scantype>" & WorkingFileDetails.filedetails_video.scantype & "</scantype>" & vbLf
        FileInfoString &= "</video>" & vbLf
        If WorkingFileDetails.filedetails_audio.Count > 0 Then
            For Each item In WorkingFileDetails.filedetails_audio
                FileInfoString &= "<audio>" & vbLf
                If item.language <> Nothing Then FileInfoString &= "<language>" & item.language & "</language>" & vbLf
                If item.codec <> Nothing Then FileInfoString &= "<codec>" & item.codec & "</codec>" & vbLf
                If item.channels <> Nothing Then FileInfoString &= "<channels>" & item.channels & "</channels>" & vbLf
                If item.bitrate <> Nothing Then FileInfoString &= "<bitrate>" & item.bitrate & "</bitrate>" & vbLf
                FileInfoString &= "</audio>" & vbLf
            Next
            If WorkingFileDetails.filedetails_subtitles.Count > 0 Then
                FileInfoString &= "<subtitle>" & vbLf
                For Each item In WorkingFileDetails.filedetails_subtitles
                    If item.language <> Nothing Then FileInfoString &= "<language>" & item.language & "</language>" & vbLf
                Next
                FileInfoString &= "</subtitle>" & vbLf
            End If
            FileInfoString &= "</streamdetails>" & vbLf & "</fileinfo>" & vbLf
        Else
            FileInfoString &= "</streamdetails>" & vbLf & "</fileinfo>" & vbLf
        End If
        FileInfoString &= "<playcount>0</playcount>" & vbLf
        FileInfoString &= "<createdate>" & Format(System.DateTime.Now, "yyyyMMddHHmmss").ToString & "</createdate>" & vbLf
        Dim TempString As String = Entrada.Remove(0, Entrada.IndexOf("<id>"))
        TempString = FileInfoString & TempString
        TempString = TempString.Remove(TempString.LastIndexOf("</details>"), 10)
        TempString &= "</movie>"
        Return TempString
    End Function
    Public Function MoviePosterandFanartDownload(ByVal FullString As String, ByVal Filename As String) As Boolean
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.LoadXml(FullString)
        m_nodelist = m_xmld.SelectNodes("/details")
        Dim NodeChild As XmlNode
        Dim Nodechild1 As XmlNode
        Dim Nodechild2 As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""

        For Each m_node In m_nodelist
            For Each NodeChild In m_node.ChildNodes
                If NodeChild.Name.ToLower = "thumb" Then
                    For Each Nodechild1 In NodeChild
                        MoviePosterURL = Nodechild1.InnerText
                        Exit For
                    Next
                ElseIf NodeChild.Name.ToLower = "fanart" Then
                    For Each Nodechild2 In NodeChild
                        MovieFanartURL = Nodechild2.InnerText
                        Exit For
                    Next

                End If
            Next
        Next
        Dim ExtensionPosition As Integer = Filename.LastIndexOf(".")
        Dim SlashPosition As Integer = Filename.LastIndexOf("\")
        Dim ImageFilename As String = Filename.Remove(ExtensionPosition, (Filename.Length - ExtensionPosition))
        ImageFilename &= ".tbn"
        Dim ImageFilename2 As String = Filename.Remove(SlashPosition, (Filename.Length - SlashPosition))
        ImageFilename2 &= "\folder.jpg"
        Dim ImageFilename3 As String = Filename.Remove(ExtensionPosition, (Filename.Length - ExtensionPosition))
        ImageFilename3 &= "-fanart.jpg"
        Dim myWebClient As New System.Net.WebClient()
        On Error Resume Next
        myWebClient.DownloadFile(MoviePosterURL, ImageFilename)
        File.Copy(ImageFilename, ImageFilename2)
        myWebClient.DownloadFile(MovieFanartURL, ImageFilename3)
        On Error GoTo 0
        '-----------------Start Resize Fanart
        If Preferences.resizefanart = 2 Then
            Dim FanartToBeResized As New Bitmap(ImageFilename3)
            If (FanartToBeResized.Width > 1280) Or (FanartToBeResized.Height > 960) Then
                Dim ResizedFanart As Bitmap = Utilities.ResizeImage(FanartToBeResized, 1280, 960)
                ResizedFanart.Save(ImageFilename3, Imaging.ImageFormat.Jpeg)
            Else
                'scraperlog = scraperlog & "Fanart not resized, already =< required size" & vbCrLf
            End If
        ElseIf Preferences.resizefanart = 3 Then
            Dim FanartToBeResized As New Bitmap(ImageFilename3)
            If (FanartToBeResized.Width > 960) Or (FanartToBeResized.Height > 540) Then
                Dim ResizedFanart As Bitmap = Utilities.ResizeImage(FanartToBeResized, 960, 540)
                ResizedFanart.Save(ImageFilename3, Imaging.ImageFormat.Jpeg)
            Else
                'scraperlog = scraperlog & "Fanart not resized, already =< required size" & vbCrLf
            End If

        End If
        '-----------------End Resize Fanart
        Return True
    End Function
    Public Function SearchExtraIDinNFO(ByVal Filename As String) As String
        Dim extrapossibleID As String = Nothing
        Filename = Filename.Remove(Filename.Length - 3, 3) & "nfo"
        If Not IO.File.Exists(Filename) Then
            Filename = Filename.Remove(Filename.LastIndexOf("\"), (Filename.Length - Filename.LastIndexOf("\"))) & "\movie.nfo"
        End If
        extrapossibleID = Nothing
        Dim T As String
        Dim mat As Match

        If IO.File.Exists(Filename) Then
            '            scraperlog = scraperlog & "nfo file exists, checking for IMDB ID" & vbCrLf
            Dim tempinfo As String = ""
            Dim objReader As New System.IO.StreamReader(Filename)
            tempinfo = objReader.ReadToEnd
            objReader.Close()
            extrapossibleID = Nothing
            T = tempinfo
            mat = Nothing
            mat = Regex.Match(T, "(tt\d{7})")
            If mat.Success = True Then
                '                scraperlog = scraperlog & "IMDB ID found in nfo file:- " & mat.Value & vbCrLf
                extrapossibleID = mat.Value
                Try
                    If Not IO.File.Exists(Filename.Replace(".nfo", ".info")) Then
                        IO.File.Move(Filename, Filename.Replace(".nfo", ".info"))
                        '                    scraperlog = scraperlog & "renaming nfo file to:- " & Filename.Replace(".nfo", ".info") & vbCrLf
                    Else
                        '                    scraperlog = scraperlog & "Unable to rename file, """ & Filename & """ already exists" & vbCrLf
                    End If
                Catch
                    '                scraperlog = scraperlog & "Unable to rename file, """ & Filename & """ already exists" & vbCrLf
                End Try
            Else
                '                scraperlog = scraperlog & "No IMDB ID found" & vbCrLf
                extrapossibleID = Nothing
            End If
        Else
            Dim stackname As String = Utilities.GetStackName(Filename, Filename.Replace(IO.Path.GetFileName(Filename), ""))
            Dim path As String = stackname & ".nfo"
            If IO.File.Exists(path) Then
                '                scraperlog = scraperlog & "nfo file exists, checking for IMDB ID" & vbCrLf
                Dim tempinfo As String = ""
                Dim objReader As New System.IO.StreamReader(path)
                tempinfo = objReader.ReadToEnd
                objReader.Close()
                extrapossibleID = Nothing
                T = tempinfo
                mat = Nothing
                mat = Regex.Match(T, "(tt\d{7})")
                If mat.Success = True Then
                    '                    scraperlog = scraperlog & "IMDB ID found in nfo file:- " & mat.Value & vbCrLf
                    extrapossibleID = mat.Value
                Else
                    '                    scraperlog = scraperlog & "No IMDB ID found" & vbCrLf
                    extrapossibleID = Nothing
                End If
            Else
                '                scraperlog = scraperlog & "NFO does not exist" & vbCrLf
                extrapossibleID = Nothing
            End If

        End If
        Return extrapossibleID
    End Function

    Public Sub Read_XBMC_IMDB_Scraper_Config()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.imdb.com\resources\settings.xml")
        m_nodelist = m_xmld.SelectNodes("/settings")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        If Form1.ComboBox_IMDB_HD_Trailer.Items.Count > 0 Then Form1.ComboBox_IMDB_HD_Trailer.Items.Clear()
        If Form1.ComboBox_IMDB_Poster_Actor_Size.Items.Count > 0 Then Form1.ComboBox_IMDB_Poster_Actor_Size.Items.Clear()
        If Form1.ComboBox_IMDB_Title_Language.Items.Count > 0 Then Form1.ComboBox_IMDB_Title_Language.Items.Clear()
        Try
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If (NodeChild.Name.ToLower = "setting") Then
                        If NodeChild.Attributes.Count > 0 Then
                            Try
                                Select Case NodeChild.Attributes("id").Value.ToLower
                                    Case "tmdbthumbs"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_IMDB_Posters_MovieDB.Checked = Test
                                    Case "impawards"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_IMDB_Posters_IMPAwards.Checked = Test
                                    Case "movieposterdb"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_IMDB_Posters_MoviePosterDB.Checked = Test
                                    Case "fanart"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_IMDB_Fanart.Checked = Test
                                    Case "trailerq"
                                        Dim Test As String = NodeChild.Attributes("default").Value
                                        Dim AllValues As String = NodeChild.Attributes("values").Value
                                        Dim GetOut As Boolean = False
                                        Do
                                            Dim Position As Integer = AllValues.LastIndexOf("|")
                                            Dim TempValue As String = ""
                                            If Position = -1 Then
                                                TempValue = Trim(AllValues.Substring(0, AllValues.Length))
                                                GetOut = True
                                            Else
                                                TempValue = Trim(AllValues.Substring(Position + 1, (AllValues.Length - Position - 1)))
                                                AllValues = AllValues.Remove(Position, (AllValues.Length - Position))
                                            End If
                                            Form1.ComboBox_IMDB_HD_Trailer.Items.Add(TempValue)
                                        Loop Until GetOut = True
                                        Form1.ComboBox_IMDB_HD_Trailer.Text = Test
                                    Case "imdbtrailer"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_IMDB_Trailer.Checked = Test
                                    Case "akatitles"
                                        Dim Test As String = NodeChild.Attributes("default").Value
                                        Dim AllValues As String = NodeChild.Attributes("values").Value
                                        Dim GetOut As Boolean = False
                                        Do
                                            Dim Position As Integer = AllValues.LastIndexOf("|")
                                            Dim TempValue As String = ""
                                            If Position = -1 Then
                                                TempValue = Trim(AllValues.Substring(0, AllValues.Length))
                                                GetOut = True
                                            Else
                                                TempValue = Trim(AllValues.Substring(Position + 1, (AllValues.Length - Position - 1)))
                                                AllValues = AllValues.Remove(Position, (AllValues.Length - Position))
                                            End If
                                            Form1.ComboBox_IMDB_Title_Language.Items.Add(TempValue)
                                        Loop Until GetOut = True
                                        Form1.ComboBox_IMDB_Title_Language.Text = Test
                                    Case "fullcredits"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_IMDB_FullCredits.Checked = Test
                                    Case "imdbscale"
                                        Dim Test As String = NodeChild.Attributes("default").Value
                                        Dim AllValues As String = NodeChild.Attributes("values").Value
                                        Dim GetOut As Boolean = False
                                        Do
                                            Dim Position As Integer = AllValues.LastIndexOf("|")
                                            Dim TempValue As String = ""
                                            If Position = -1 Then
                                                TempValue = Trim(AllValues.Substring(0, AllValues.Length))
                                                GetOut = True
                                            Else
                                                TempValue = Trim(AllValues.Substring(Position + 1, (AllValues.Length - Position - 1)))
                                                AllValues = AllValues.Remove(Position, (AllValues.Length - Position))
                                            End If
                                            Form1.ComboBox_IMDB_Poster_Actor_Size.Items.Add(TempValue)
                                        Loop Until GetOut = True
                                        Form1.ComboBox_IMDB_Poster_Actor_Size.Text = Test

                                End Select
                            Catch
                                'empty node
                            End Try
                        End If
                    End If
                Next
            Next
        Catch
        End Try

    End Sub
    Public Function Save_XBMC_IMDB_Scraper_Config(ByVal KeyToBeChanged As String, ByVal ChangeValue As String) As Boolean
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.imdb.com\resources\settings.xml")
        m_nodelist = m_xmld.SelectNodes("/settings")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        Try
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If (NodeChild.Name.ToLower = "setting") Then
                        If NodeChild.Attributes.Count > 0 Then
                            Try
                                If KeyToBeChanged.ToLower = NodeChild.Attributes("id").Value.ToLower Then
                                    Select Case KeyToBeChanged
                                        Case "tmdbthumbs"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "impawards"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "movieposterdb"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "fanart"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "trailerq"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "imdbtrailer"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "akatitles"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "fullcredits"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "imdbscale"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                    End Select
                                End If
                            Catch
                                'empty node
                            End Try
                        End If
                    End If
                Next
            Next
        Catch
        End Try
        m_xmld.Save(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.imdb.com\resources\settings.xml")
    End Function
    Public Sub Read_XBMC_TMDB_Scraper_Config()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.themoviedb.org\resources\settings.xml")
        m_nodelist = m_xmld.SelectNodes("/settings")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        If Form1.ComboBox_TMDB_HD_Trailer.Items.Count > 0 Then Form1.ComboBox_TMDB_HD_Trailer.Items.Clear()
        If Form1.ComboBox_TMDB_Title_Language.Items.Count > 0 Then Form1.ComboBox_TMDB_Title_Language.Items.Clear()
        Try
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If (NodeChild.Name.ToLower = "setting") Then
                        If NodeChild.Attributes.Count > 0 Then
                            Try
                                Select Case NodeChild.Attributes("id").Value.ToLower
                                    Case "fanart"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_TMDB_Fanart.Checked = Test
                                    Case "trailerq"
                                        Dim Test As String = NodeChild.Attributes("default").Value
                                        Dim AllValues As String = NodeChild.Attributes("values").Value
                                        Dim GetOut As Boolean = False
                                        Do
                                            Dim Position As Integer = AllValues.LastIndexOf("|")
                                            Dim TempValue As String = ""
                                            If Position = -1 Then
                                                TempValue = Trim(AllValues.Substring(0, AllValues.Length))
                                                GetOut = True
                                            Else
                                                TempValue = Trim(AllValues.Substring(Position + 1, (AllValues.Length - Position - 1)))
                                                AllValues = AllValues.Remove(Position, (AllValues.Length - Position))
                                            End If
                                            Form1.ComboBox_TMDB_HD_Trailer.Items.Add(TempValue)
                                        Loop Until GetOut = True
                                        Form1.ComboBox_TMDB_HD_Trailer.Text = Test
                                    Case "language"
                                        Dim Test As String = NodeChild.Attributes("default").Value
                                        Dim AllValues As String = NodeChild.Attributes("values").Value
                                        Dim GetOut As Boolean = False
                                        Do
                                            Dim Position As Integer = AllValues.LastIndexOf("|")
                                            Dim TempValue As String = ""
                                            If Position = -1 Then
                                                TempValue = Trim(AllValues.Substring(0, AllValues.Length))
                                                GetOut = True
                                            Else
                                                TempValue = Trim(AllValues.Substring(Position + 1, (AllValues.Length - Position - 1)))
                                                AllValues = AllValues.Remove(Position, (AllValues.Length - Position))
                                            End If
                                            Form1.ComboBox_TMDB_Title_Language.Items.Add(TempValue)
                                        Loop Until GetOut = True
                                        Form1.ComboBox_TMDB_Title_Language.Text = Test
                                    Case "imdbrating"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_TMDB_IMDBRatings.Checked = Test
                                End Select
                            Catch
                                'empty node
                            End Try
                        End If
                    End If
                Next
            Next
        Catch
        End Try

    End Sub
    Public Function Save_XBMC_TMDB_Scraper_Config(ByVal KeyToBeChanged As String, ByVal ChangeValue As String) As Boolean
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.themoviedb.org\resources\settings.xml")
        m_nodelist = m_xmld.SelectNodes("/settings")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        Try
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If (NodeChild.Name.ToLower = "setting") Then
                        If NodeChild.Attributes.Count > 0 Then
                            Try
                                If KeyToBeChanged.ToLower = NodeChild.Attributes("id").Value.ToLower Then
                                    Select Case KeyToBeChanged
                                        Case "imdbrating"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "fanart"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "trailerq"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "language"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                    End Select
                                End If
                            Catch
                                'empty node
                            End Try
                        End If
                    End If
                Next
            Next
        Catch
        End Try
        m_xmld.Save(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.themoviedb.org\resources\settings.xml")
    End Function

#End Region

#Region "Misc.TVShows Routines"

    Public Function NeededConversion(ByVal entrada As TvEpisode) As TvEpisode
        'Dim Teste As New EpisodeInfo

        'Teste.aired = entrada.aired
        'Teste.credits = entrada.credits
        'Teste.director = entrada.director
        'Teste.episodeno = entrada.episodeNO
        'Teste.episodepath = entrada.episodePath
        'Teste.fanartpath = entrada.fanartPath
        'Teste.filedetails = entrada.fileDetails
        'Teste.genre = entrada.genre
        'For Each merda As Nfo.Actor In entrada.Actors

        '    Teste.Actors.Add(merda1)
        'Next
        ''Teste.listactors = entrada.listactors
        ''Teste.listactors.Item(0).actorid = entrada.listactors.Item(0).actorid.ToString
        'Teste.mediaextension = entrada.mediaExtension
        'Teste.playcount = entrada.playCount
        'Teste.plot = entrada.plot
        'Teste.rating = entrada.rating
        'Teste.runtime = entrada.runtime
        'Teste.seasonno = entrada.seasonNO
        'Teste.thumb = entrada.thumb
        'Teste.title = entrada.title
        Return entrada
    End Function

    Public Function Clean_AddTVShowExtraFields(ByVal Entrada As String, ByVal Language As String, ByVal IMDB_ID As String) As String
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim TvDBId As String = ""
        Dim Year As String = ""
        Dim ExtraFields As Boolean = False
        Dim FinalString As String = "<tvshow>"

        m_xmld = New XmlDocument()
        m_xmld.LoadXml(Entrada)
        m_nodelist = m_xmld.SelectNodes("/details")
        For Each m_node In m_nodelist
            If m_node.HasChildNodes Then
                For Each node1 As XmlNode In m_node
                    If (node1.InnerText <> "") Then
                        If node1.Name.ToLower = "actor" Then
                            For Each node2 As XmlNode In node1
                                Select Case node2.Name.ToLower
                                    Case "thumb"
                                        If node2.InnerText <> "" Then FinalString &= node1.OuterXml.ToString
                                        Exit For
                                End Select
                            Next
                        Else
                            Select Case node1.Name.ToLower
                                Case "id"
                                    FinalString &= "<tvdbid>" & node1.InnerText & "</tvdbid>"
                                    node1.InnerText = IMDB_ID
                                Case "premiered"
                                    FinalString &= "<year>" & node1.InnerText.Substring(0, 4) & "</year>"
                                Case "genre"
                                    If ExtraFields = False Then
                                        Dim sortorder As String = String.Empty
                                        If Form1.RadioButton14.Checked Then sortorder = "dvd"
                                        If Form1.RadioButton15.Checked Then sortorder = "default"
                                        FinalString &= "<top250>0</top250><season>-1</season><episode>-1</episode><displayseason>-1</displayseason><displayepisode>-1</displayepisode>" _
                                            & "<votes></votes><outline></outline><tagline></tagline><episodeactorsource>tvdb</episodeactorsource><tvshowactorsource>tvdb</tvshowactorsource>"
                                        FinalString &= "<runtime>60</runtime><sortorder>" & sortorder & "</sortorder><playcount>0</playcount><lastplayed></lastplayed><status></status>" _
                                            & "<code></code><language>" & Language & "</language><locked>0</locked><trailer></trailer>"
                                        ExtraFields = True
                                    End If

                            End Select
                            FinalString &= node1.OuterXml.ToString
                        End If
                    End If
                Next
            End If
        Next
        FinalString &= "</tvshow>"
        Return FinalString
    End Function

    Public Function TVShowPosterandFanartDownload(ByVal FullString As String, ByVal ArtforDownload() As String, ByVal Path As String) As Boolean
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.LoadXml(FullString)
        m_nodelist = m_xmld.SelectNodes("/tvshow")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        If Preferences.downloadtvseasonthumbs = True Then
            Try
                For Each m_node In m_nodelist
                    For Each NodeChild In m_node.ChildNodes
                        If (NodeChild.Name.ToLower = "thumb") Then
                            If NodeChild.Attributes.Count > 0 Then
                                If (NodeChild.Attributes("type").Value = "season") Then
                                    If SeasonPosters.Length < CInt(NodeChild.Attributes("season").Value) Then
                                        ReDim Preserve SeasonPosters(NodeChild.Attributes("season").Value + 1)
                                    End If
                                    If CInt(NodeChild.Attributes("season").Value) >= 0 Then
                                        If SeasonPosters(NodeChild.Attributes("season").Value) = Nothing Then
                                            SeasonPosters(NodeChild.Attributes("season").Value) = NodeChild.InnerText
                                        End If
                                    Else
                                        If Seasonall = Nothing Then Seasonall = NodeChild.InnerText
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            Catch
            End Try
        End If
        Dim myWebClient As New System.Net.WebClient()
        If Preferences.tvposter = True Then
            If Preferences.postertype = "banner" Then
                Dim ImageFilename As String = Path & "\folder.jpg"
                If ArtforDownload(0) <> Nothing Then
                    myWebClient.DownloadFile("http://thetvdb.com/banners/" & ArtforDownload(0), ImageFilename)
                End If
            ElseIf Preferences.postertype = "poster" Then
                Dim ImageFilename As String = Path & "\folder.jpg"
                If ArtforDownload(1) <> Nothing Then
                    myWebClient.DownloadFile("http://thetvdb.com/banners/" & ArtforDownload(1), ImageFilename)
                End If
            End If
        End If
        If Preferences.tvfanart = True Then
            Dim ImageFilename As String = Path & "\fanart.jpg"
            If ArtforDownload(2) <> Nothing Then
                myWebClient.DownloadFile("http://thetvdb.com/banners/" & ArtforDownload(2), ImageFilename)
                '-----------------Start Resize Fanart
                If Preferences.resizefanart = 2 Then
                    Dim FanartToBeResized As New Bitmap(ImageFilename)
                    If (FanartToBeResized.Width > 1280) Or (FanartToBeResized.Height > 960) Then
                        Dim ResizedFanart As Bitmap = Utilities.ResizeImage(FanartToBeResized, 1280, 960)
                        ResizedFanart.Save(ImageFilename, Imaging.ImageFormat.Jpeg)
                    Else
                        'scraperlog = scraperlog & "Fanart not resized, already =< required size" & vbCrLf
                    End If
                ElseIf Preferences.resizefanart = 3 Then
                    Dim FanartToBeResized As New Bitmap(ImageFilename)
                    If (FanartToBeResized.Width > 960) Or (FanartToBeResized.Height > 540) Then
                        Dim ResizedFanart As Bitmap = Utilities.ResizeImage(FanartToBeResized, 960, 540)
                        ResizedFanart.Save(ImageFilename, Imaging.ImageFormat.Jpeg)
                    Else
                        'scraperlog = scraperlog & "Fanart not resized, already =< required size" & vbCrLf
                    End If
                End If
                '-----------------End Resize Fanart
            End If
        End If

        If Preferences.downloadtvseasonthumbs = True Then
            For n As Integer = 0 To SeasonPosters.Length - 1
                Dim SeasonTemp As String = ""
                If n <= 9 Then
                    SeasonTemp = "0" & n.ToString
                Else
                    SeasonTemp = n.ToString
                End If
                Dim ImageFilename As String = Path & "\season" & SeasonTemp & ".tbn"
                If SeasonPosters(n) <> Nothing Then
                    myWebClient.DownloadFile(SeasonPosters(n), ImageFilename)
                    If n = 0 Then
                        File.Copy(ImageFilename, Path & "\season-specials.tbn")
                    End If
                End If

            Next
            Dim ImageFilename1 As String = Path & "\season-all.tbn"
            If Seasonall <> Nothing Then
                myWebClient.DownloadFile(Seasonall, ImageFilename1)
            End If
        End If
        Return True
    End Function

    Public Function InsertFileEpisodeInformationTags(ByVal Entrada() As String, ByVal Filename As String) As String
        Dim WorkingFileDetails As FullFileDetails = get_hdtags(Filename)
        Dim FileInfoString As String = ""
        Dim TempString As String = ""

        For n As Integer = 0 To Entrada.Length - 1
            FileInfoString &= "<episodedetails>" & vbLf & "<fileinfo>" & vbLf & "<streamdetails>" & vbLf & "<video>" & vbLf

            If WorkingFileDetails.filedetails_video.width <> Nothing Then FileInfoString &= "<width>" & WorkingFileDetails.filedetails_video.width & "</width>" & vbLf
            If WorkingFileDetails.filedetails_video.height <> Nothing Then FileInfoString &= "<height>" & WorkingFileDetails.filedetails_video.height & "</height>" & vbLf
            If WorkingFileDetails.filedetails_video.aspect <> Nothing Then FileInfoString &= "<aspect>" & WorkingFileDetails.filedetails_video.aspect & "</aspect>" & vbLf
            If WorkingFileDetails.filedetails_video.codec <> Nothing Then FileInfoString &= "<codec>" & WorkingFileDetails.filedetails_video.codec & "</codec>" & vbLf
            If WorkingFileDetails.filedetails_video.formatinfo <> Nothing Then FileInfoString &= "<format>" & WorkingFileDetails.filedetails_video.formatinfo & "</format>" & vbLf
            If WorkingFileDetails.filedetails_video.duration <> Nothing Then FileInfoString &= "<duration>" & Utilities.cleanruntime(WorkingFileDetails.filedetails_video.duration) & "</duration>" & vbLf
            If WorkingFileDetails.filedetails_video.duration <> Nothing Then TempEpisode.Runtime.Value = Utilities.cleanruntime(WorkingFileDetails.filedetails_video.duration).ToString
            If WorkingFileDetails.filedetails_video.bitrate <> Nothing Then FileInfoString &= "<bitrate>" & WorkingFileDetails.filedetails_video.bitrate & "</bitrate>" & vbLf
            If WorkingFileDetails.filedetails_video.bitratemode <> Nothing Then FileInfoString &= "<bitratemode>" & WorkingFileDetails.filedetails_video.bitratemode & "</bitratemode>" & vbLf
            If WorkingFileDetails.filedetails_video.bitratemax <> Nothing Then FileInfoString &= "<bitratemax>" & WorkingFileDetails.filedetails_video.bitratemax & "</bitratemax>" & vbLf
            If WorkingFileDetails.filedetails_video.container <> Nothing Then FileInfoString &= "<container>" & WorkingFileDetails.filedetails_video.container & "</container>" & vbLf
            If WorkingFileDetails.filedetails_video.codecid <> Nothing Then FileInfoString &= "<codecid>" & WorkingFileDetails.filedetails_video.codecid & "</codecid>" & vbLf
            If WorkingFileDetails.filedetails_video.codecinfo <> Nothing Then FileInfoString &= "<codecinfo>" & WorkingFileDetails.filedetails_video.codecinfo & "</codecinfo>" & vbLf
            If WorkingFileDetails.filedetails_video.scantype <> Nothing Then FileInfoString &= "<scantype>" & WorkingFileDetails.filedetails_video.scantype & "</scantype>" & vbLf
            FileInfoString &= "</video>" & vbLf
            If WorkingFileDetails.filedetails_audio.Count > 0 Then
                For Each item In WorkingFileDetails.filedetails_audio
                    FileInfoString &= "<audio>" & vbLf
                    If item.language <> Nothing Then FileInfoString &= "<language>" & item.language & "</language>" & vbLf
                    If item.codec <> Nothing Then FileInfoString &= "<codec>" & item.codec & "</codec>" & vbLf
                    If item.channels <> Nothing Then FileInfoString &= "<channels>" & item.channels & "</channels>" & vbLf
                    If item.bitrate <> Nothing Then FileInfoString &= "<bitrate>" & item.bitrate & "</bitrate>" & vbLf
                    FileInfoString &= "</audio>" & vbLf
                Next
                If WorkingFileDetails.filedetails_subtitles.Count > 0 Then
                    FileInfoString &= "<subtitle>" & vbLf
                    For Each item In WorkingFileDetails.filedetails_subtitles
                        If item.language <> Nothing Then FileInfoString &= "<language>" & item.language & "</language>" & vbLf
                    Next
                    FileInfoString &= "</subtitle>" & vbLf
                End If
            End If
            FileInfoString &= "</streamdetails>" & vbLf & "</fileinfo>" & vbLf
            FileInfoString &= "<createdate>" & Format(System.DateTime.Now, "yyyyMMddHHmmss").ToString & "</createdate>" & vbLf
            TempString = ""
            TempString = Entrada(n).Substring(0, Entrada(n).IndexOf("</details>"))
            TempString = TempString.Replace("<details>", "")
            TempString = FileInfoString & TempString & "</episodedetails>"
            If Entrada.Length > 1 Then TempString &= vbLf
            FileInfoString = TempString
        Next
        If Entrada.Length > 1 Then TempString = "<multiepisodenfo>" & vbLf & TempString & vbLf & "</multiepisodenfo>"
        Return TempString
    End Function

    Public Function ProcessEpisodeFile(ByVal Entrada As String, ByVal HowManyEpisodes As Integer) As List(Of TvEpisode)
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim newActor As New Nfo.Actor

        episodeXMLinformation.Clear()
        m_xmld = New XmlDocument()
        m_xmld.LoadXml(Entrada)
        If HowManyEpisodes > 1 Then
            m_nodelist = m_xmld.SelectNodes("/multiepisodenfo/episodedetails")
        Else
            m_nodelist = m_xmld.SelectNodes("/episodedetails")
        End If
        Dim NodeChild As XmlNode
        Dim Nodechild1 As XmlNode
        Dim Counter As Integer = 0

        For Each m_node In m_nodelist
            TempXMLEpisode.aired = Nothing
            TempXMLEpisode.credits = Nothing
            TempXMLEpisode.director = Nothing
            TempXMLEpisode.episodeNO = Nothing
            TempXMLEpisode.genre = Nothing
            TempXMLEpisode.plot = Nothing
            TempXMLEpisode.rating = Nothing
            TempXMLEpisode.seasonNO = Nothing
            TempXMLEpisode.thumb = Nothing
            TempXMLEpisode.title = Nothing
            TempXMLEpisode.listactors.Clear()
            For Each NodeChild In m_node.ChildNodes
                Select Case NodeChild.Name.ToLower
                    Case "aired"
                        TempXMLEpisode.aired = NodeChild.InnerText
                    Case "credits"
                        If TempXMLEpisode.credits = Nothing Then
                            TempXMLEpisode.credits = NodeChild.InnerText
                        Else
                            TempXMLEpisode.credits &= " / " & NodeChild.InnerText
                        End If
                    Case "director"
                        TempXMLEpisode.director = NodeChild.InnerText
                    Case "genre"
                        If TempXMLEpisode.genre = Nothing Then
                            TempXMLEpisode.genre = NodeChild.InnerText
                        Else
                            TempXMLEpisode.genre &= " / " & NodeChild.InnerText
                        End If
                    Case "plot"
                        TempXMLEpisode.plot = NodeChild.InnerText
                    Case "rating"
                        TempXMLEpisode.rating = NodeChild.InnerText
                    Case "thumb"
                        TempXMLEpisode.thumb = NodeChild.InnerText
                    Case "title"
                        TempXMLEpisode.title = NodeChild.InnerText
                    Case "season"
                        TempXMLEpisode.seasonNO = NodeChild.InnerText
                    Case "episode"
                        TempXMLEpisode.episodeNO = NodeChild.InnerText
                    Case "actor"
                        For Each Nodechild1 In NodeChild.ChildNodes
                            Select Case Nodechild1.Name
                                Case "name"
                                    newActor.Name.Value = Nodechild1.InnerText
                                Case "role"
                                    newActor.Role.Value = Nodechild1.InnerText
                                Case "thumb"
                                    newActor.Thumb.Value = Nodechild1.InnerText
                                Case "actorid"
                            End Select
                        Next
                        If newActor.Name.Value <> Nothing Then TempXMLEpisode.listactors.Add(newActor)
                End Select
            Next
            episodeXMLinformation.Add(TempXMLEpisode)
        Next
        Dim Teste As New List(Of TvEpisode)
        Teste = episodeXMLinformation.ConvertAll(New Converter(Of TvEpisode, TvEpisode)(AddressOf NeededConversion))
        Return Teste
    End Function

    Public Sub Read_XBMC_TVDB_Scraper_Config()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.tvdb.com\resources\settings.xml")
        m_nodelist = m_xmld.SelectNodes("/settings")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        If Form1.ComboBox_TVDB_Language.Items.Count > 0 Then Form1.ComboBox_TVDB_Language.Items.Clear()
        Try
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If (NodeChild.Name.ToLower = "setting") Then
                        If NodeChild.Attributes.Count > 0 Then
                            Try
                                Select Case NodeChild.Attributes("id").Value.ToLower
                                    Case "dvdorder"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.RadioButton_XBMC_Scraper_TVDB_DVDOrder.Checked = Test
                                    Case "absolutenumber"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.RadioButton_XBMC_Scraper_TVDB_AbsoluteNumber.Checked = Test
                                    Case "fanart"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_TVDB_Fanart.Checked = Test
                                    Case "posters"
                                        Dim Test As Boolean = NodeChild.Attributes("default").Value
                                        Form1.CheckBox_XBMC_Scraper_TVDB_Posters.Checked = Test
                                    Case "language"
                                        Dim Test As String = NodeChild.Attributes("default").Value
                                        Dim AllValues As String = NodeChild.Attributes("values").Value
                                        Dim GetOut As Boolean = False
                                        Do
                                            Dim Position As Integer = AllValues.LastIndexOf("|")
                                            Dim TempValue As String = ""
                                            If Position = -1 Then
                                                TempValue = Trim(AllValues.Substring(0, AllValues.Length))
                                                GetOut = True
                                            Else
                                                TempValue = Trim(AllValues.Substring(Position + 1, (AllValues.Length - Position - 1)))
                                                AllValues = AllValues.Remove(Position, (AllValues.Length - Position))
                                            End If
                                            Form1.ComboBox_TVDB_Language.Items.Add(TempValue)
                                        Loop Until GetOut = True
                                        Form1.ComboBox_TVDB_Language.Text = Test
                                End Select
                            Catch
                                'empty node
                            End Try
                        End If
                    End If
                Next
            Next
        Catch
        End Try

    End Sub
    Public Function Save_XBMC_TVDB_Scraper_Config(ByVal KeyToBeChanged As String, ByVal ChangeValue As String) As Boolean
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_xmld = New XmlDocument()
        m_xmld.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.tvdb.com\resources\settings.xml")
        m_nodelist = m_xmld.SelectNodes("/settings")
        Dim NodeChild As XmlNode
        Dim MoviePosterURL As String = ""
        Dim MovieFanartURL As String = ""
        Dim SeasonPosters(0) As String
        Dim Seasonall As String = Nothing

        Try
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If (NodeChild.Name.ToLower = "setting") Then
                        If NodeChild.Attributes.Count > 0 Then
                            Try
                                If KeyToBeChanged.ToLower = NodeChild.Attributes("id").Value.ToLower Then

                                    Select Case KeyToBeChanged 'NodeChild.Attributes("id").Value.ToLower
                                        Case "dvdorder"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "absolutenumber"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "fanart"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "posters"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                        Case "language"
                                            NodeChild.Attributes("default").Value = ChangeValue
                                    End Select
                                End If
                            Catch
                                'empty node
                            End Try
                        End If
                    End If
                Next
            Next
        Catch
        End Try
        m_xmld.Save(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\")) & "\assets\scrapers\metadata.tvdb.com\resources\settings.xml")
    End Function
#End Region


#Region "nfoFileFunctions"

    Public Function CreateMovieNfo(ByVal Filename As String, ByVal FileContent As String) As Boolean
        Try
            Dim ExtensionPosition As Integer = Filename.LastIndexOf(".")
            Dim nfoFilename As String = Filename.Remove(ExtensionPosition, (Filename.Length - ExtensionPosition))
            nfoFilename &= ".nfo"
            Dim doc As New XmlDocument
            doc.LoadXml(FileContent)
            Dim xmlproc As XmlDeclaration
            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            Dim root As XmlElement = doc.DocumentElement
            doc.InsertBefore(xmlproc, root)
            Dim output As New XmlTextWriter(nfoFilename, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
            output.Close()

            Return True
        Catch
            Return False
        End Try
    End Function

#End Region


    Public Function XBMCScrape_TVShow_EpisodeDetails(ByVal TVDBId As String, ByVal SortOrder As String, ByVal EpisodeArray As List(Of TvEpisode), ByVal Language As String) As List(Of TvEpisode)
        episodeInformation.Clear()
        Dim EpisodeInfoContent(EpisodeArray.Count - 1) As String

        For n As Integer = 0 To EpisodeArray.Count - 1
            EpisodeArray(n).seasonno = CInt(EpisodeArray(n).seasonno)
            EpisodeArray(n).episodeno = CInt(EpisodeArray(n).episodeno)
            TempXMLEpisode.episodePath = EpisodeArray(n).mediaextension.Substring(0, EpisodeArray(n).mediaextension.LastIndexOf(".")) & ".nfo"
            TempXMLEpisode.episodeNO = EpisodeArray(n).episodeno
            TempXMLEpisode.seasonNO = EpisodeArray(n).seasonno
            TempXMLEpisode.mediaExtension = EpisodeArray(n).mediaextension
            TempXMLEpisode.playCount = "0"
            ParametersForScraper(1) = TVDBId
            ParametersForScraper(3) = "http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVDBId & "/" & Language & ".xml"
            ParametersForScraper(4) = Nothing
            Try
                For x As Integer = 0 To 20
                    ParametersForScraper(7) = Utilities.DownloadTextFiles("http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVDBId & "/" & SortOrder & "/" & EpisodeArray(n).seasonno & "/" & EpisodeArray(n).episodeno & "/" & Language & ".xml")
                    'ParametersForScraper(7) = New WebClient().DownloadString("http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVDBId & "/" & SortOrder & "/" & EpisodeArray(n).seasonno & "/" & EpisodeArray(n).episodeno & "/" & Language & ".xml")
                    If ParametersForScraper(7).Substring(0, 5).ToLower = "<?xml" Then
                        Exit For
                    Else
                        If x = 20 Then
                            episodeInformation.Clear()
                            Return episodeInformation
                            Exit Function
                        End If
                    End If
                Next
                EpisodeInfoContent(n) = DoScrape("metadata.tvdb.com", "GetEpisodeDetails", ParametersForScraper, False)
            Catch
                episodeInformation.Clear()
                Return episodeInformation
                Exit Function
                'não achou o episodio
            End Try
        Next


        FinalScrapResult = InsertFileEpisodeInformationTags(EpisodeInfoContent, EpisodeArray(0).mediaextension)
        episodeInformation = ProcessEpisodeFile(FinalScrapResult, EpisodeArray.Count)
        If episodeInformation(0).thumb <> Nothing Then
            Dim myWebClient As New System.Net.WebClient()
            Dim ImageFilename As String = EpisodeArray(0).mediaextension.Substring(0, EpisodeArray(0).mediaextension.LastIndexOf(".")) & ".tbn"
            myWebClient.DownloadFile(episodeInformation(0).thumb, ImageFilename)
        End If
        Dim DidItWork As Boolean = CreateMovieNfo(TempXMLEpisode.episodePath, FinalScrapResult)

        Return episodeInformation
    End Function

    Public Function XBMCScrape_TVShow_General_Info(ByVal Scraper As String, ByVal TVShowid As String, ByVal Language As String, ByVal Path As String) As String
        Try
            Dim Parameters(2) As String
            Dim ParametersForScraper(9) As String
            ParametersForScraper(0) = Utilities.DownloadTextFiles("http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVShowid & "/" & Language & ".xml")
            'ParametersForScraper(0) = New WebClient().DownloadString("http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVShowid & "/" & Language & ".xml")
            Dim m_xmld As XmlDocument
            Dim m_nodelist As XmlNodeList
            Dim m_node As XmlNode
            m_xmld = New XmlDocument()
            m_xmld.LoadXml(ParametersForScraper(0))
            m_nodelist = m_xmld.SelectNodes("/Data/Series")
            Dim ArtforDownload(2) As String
            For Each m_node In m_nodelist
                For Each NodeChild In m_node.ChildNodes
                    If NodeChild.Name.ToLower = "banner" Then
                        ArtforDownload(0) = NodeChild.InnerText
                    ElseIf NodeChild.Name.ToLower = "poster" Then
                        ArtforDownload(1) = NodeChild.InnerText
                    ElseIf NodeChild.Name.ToLower = "fanart" Then
                        ArtforDownload(2) = NodeChild.InnerText
                    End If
                Next
            Next
            Dim IMDB_ID As String = ParametersForScraper(0).Substring(ParametersForScraper(0).ToLower.IndexOf("<imdb_id>") + 9, (ParametersForScraper(0).ToLower.LastIndexOf("</imdb_id>") - ParametersForScraper(0).ToLower.IndexOf("<imdb_id>") - 9))
            ParametersForScraper(1) = TVShowid
            ParametersForScraper(9) = TVShowid
            Parameters(0) = "http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVShowid & "/" & Language & ".xml"
            Parameters(1) = "http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVShowid & "/banners.xml"
            Parameters(2) = "http://www.thetvdb.com/api/1D62F2F90030C444/series/" & TVShowid & "/actors.xml"
            For n As Integer = 0 To 2
                ParametersForScraper(4) = Parameters(n)
                Parameters(n) = DoScrape(Scraper, "GetDetails", ParametersForScraper, True, 5)
            Next
            Parameters(0) = Parameters(0).Substring(0, Parameters(0).LastIndexOf("<fanart url="))
            Parameters(1) = Parameters(1).Substring(Parameters(1).IndexOf("<thumb>"), (Parameters(1).LastIndexOf("</details>") - Parameters(1).IndexOf("<thumb>")))
            Parameters(2) = Parameters(2).Substring(Parameters(2).IndexOf("<actor>"), (Parameters(2).LastIndexOf("<fanart url=") - Parameters(2).IndexOf("<actor>"))) & "</details>"
            Dim Temp As String = Parameters(0) & Parameters(1) & Parameters(2)
            Temp = Clean_AddTVShowExtraFields(Temp, Language, IMDB_ID)
            Path = Path.Substring(0, Path.LastIndexOf("\"))
            Dim Downloads As Boolean = TVShowPosterandFanartDownload(Temp, ArtforDownload, Path)
            Return Temp
        Catch
            Return "error"
        End Try
    End Function

    Public Function Start_XBMC_MoviesScraping(ByVal Scraper As String, ByVal MovieName As String, ByVal Filename As String) As String
        ' 1st stage
        Dim ExtraID As String = SearchExtraIDinNFO(Filename)
        If (ExtraID = Nothing) Or (Scraper.ToLower <> "imdb") Then
            If Scraper.ToLower = "imdb" Then Scraper = "metadata.imdb.com"
            If Scraper.ToLower = "tmdb" Then Scraper = "metadata.themoviedb.org"
            ParametersForScraper(0) = cleanfilename(MovieName, False)
            ParametersForScraper(1) = GetYearByFilename(MovieName, False)
            FinalScrapResult = DoScrape(Scraper, "CreateSearchUrl", ParametersForScraper, False)
            FinalScrapResult = FinalScrapResult.Replace("<url>", "")
            FinalScrapResult = FinalScrapResult.Replace("</url>", "")
            FinalScrapResult = FinalScrapResult.Replace(" ", "%20")
            ' 2st stage
            ParametersForScraper(0) = FinalScrapResult
            FinalScrapResult = DoScrape(Scraper, "GetSearchResults", ParametersForScraper, True)
            Dim m_xmld As XmlDocument
            Dim m_nodelist As XmlNodeList
            Dim m_node As XmlNode
            m_xmld = New XmlDocument()
            m_xmld.LoadXml(FinalScrapResult)
            m_nodelist = m_xmld.SelectNodes("/results/entity")
            If Scraper = "metadata.imdb.com" Then
                For Each m_node In m_nodelist
                    Dim Title = m_node.ChildNodes.Item(0).InnerText
                    Dim year = m_node.ChildNodes.Item(1).InnerText
                    Dim url = m_node.ChildNodes.Item(2).InnerText
                    Dim id = m_node.ChildNodes.Item(3).InnerText
                    ParametersForScraper(0) = url
                    ParametersForScraper(1) = id
                    Exit For
                Next
            ElseIf Scraper = "metadata.themoviedb.org" Then
                For Each m_node In m_nodelist
                    Dim Title = m_node.ChildNodes.Item(0).InnerText
                    Dim id = m_node.ChildNodes.Item(1).InnerText
                    Dim year = m_node.ChildNodes.Item(2).InnerText
                    Dim url = m_node.ChildNodes.Item(3).InnerText
                    ParametersForScraper(0) = url
                    ParametersForScraper(1) = id
                    Exit For
                Next
            End If
        Else
            Scraper = "metadata.imdb.com"
            ParametersForScraper(0) = "http://akas.imdb.com/title/" & ExtraID
            ParametersForScraper(1) = ExtraID
        End If
        ' 3st stage
        FinalScrapResult = DoScrape(Scraper, "GetDetails", ParametersForScraper, True)
        Dim Teste As Boolean = MoviePosterandFanartDownload(FinalScrapResult, Filename)
        FinalScrapResult = ReplaceCharactersinXML(FinalScrapResult)
        FinalScrapResult = InsertFileInformationTags(FinalScrapResult, Filename)
        Return FinalScrapResult
    End Function

    Public Function Start_XBMC_MoviesReScraping(ByVal Scraper As String, ByVal MovieID As String, ByVal Filename As String) As String
        ' 1st stage
        If Scraper.ToLower = "imdb" Then
            Scraper = "metadata.imdb.com"
            If MovieID.Substring(0, 2) = "tt" Then
                ParametersForScraper(0) = "http://akas.imdb.com/title/" & MovieID & "/"
                ParametersForScraper(1) = MovieID
            Else
                MsgBox("Can't rescrape this movie because it was scraped with a scraper different from IMDB" & vbCrLf & "Delete the nfo file from the movie folder, rebuild movie database, and try again", MsgBoxStyle.OkOnly, "Error")
                Return "Error"
                Exit Function
            End If
        ElseIf Scraper.ToLower = "tmdb" Then
            Scraper = "metadata.themoviedb.org"
            If MovieID.Substring(0, 2) <> "tt" Then
                ParametersForScraper(0) = "http://api.themoviedb.org/2.1/Movie.getInfo/en/xml/3f026194412846e530a208cf8a39e9cb/" & MovieID
                ParametersForScraper(1) = MovieID
            Else
                MsgBox("Can't rescrape this movie because it was scraped with a scraper different from TheMovieDB" & vbCrLf & "Delete the nfo file from the movie folder, rebuild movie database, and try again", MsgBoxStyle.OkOnly, "Error")
                Return "Error"
                Exit Function
            End If
        End If
        ' 3st stage
        FinalScrapResult = DoScrape(Scraper, "GetDetails", ParametersForScraper, True)
        FinalScrapResult = ReplaceCharactersinXML(FinalScrapResult)
        FinalScrapResult = InsertFileInformationTags(FinalScrapResult, Filename)
        Return FinalScrapResult
    End Function

End Module
